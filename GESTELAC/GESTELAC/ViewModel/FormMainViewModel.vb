Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Net
Imports System.Threading

Public Class FormMainViewModel
    Inherits ViewModelBase

#Region "VARIABLES"

    Private WithEvents Sherlock As BackgroundWorker
    Private dernierePosition As String = String.Empty

    Private myView As CollectionView

    Private wClient As WebClient
    Private wResponse As WebResponse

    Private currentRH As Xml.XmlNode

    Private bTrouve As Boolean = False
    Private listeDesHomonymes As Xml.XmlNodeList

    Public szDefaultUrl As String = "http://www.scpp.fr/SCPP/Home/LASCPP/BasePhonogrammes/tabid/81/Default.aspx"

    Private Critere01, Critere02, Critere03, Critere04, Critere05 As Boolean

    Private bBusy As Boolean
    Private docBrutHtml2String As String

#End Region

#Region "PROPRIETES"

    Private mForm As FormMain
    Public Property FormAppellante As FormMain
        Get
            Return mForm
        End Get
        Set(value As FormMain)
            mForm = value
            OnPropertyChanged("FormAppellante")
        End Set
    End Property

    Private mszSku As String
    Public Property szSku As String
        Get
            Return mszSku
        End Get
        Set(value As String)
            mszSku = value
            OnPropertyChanged("szSku")
        End Set
    End Property

    Private mszSkuToSearch As String
    Public Property szSkuToSearch As String
        Get
            Return mszSkuToSearch
        End Get
        Set(value As String)
            mszSkuToSearch = value
            OnPropertyChanged("szSkuToSearch")
        End Set
    End Property

    Private m_SelectedAccessoire As Accessoire
    Public Property SelectedAccessoire As Accessoire
        Get
            Return m_SelectedAccessoire
        End Get
        Set(ByVal value As Accessoire)
            m_SelectedAccessoire = value
            OnPropertyChanged("SelectedAccessoire")
        End Set
    End Property

    Private m_lstStatuts As ObservableCollection(Of Statut)
    Public Property ListeDesStatuts As ObservableCollection(Of Statut)
        Get
            Return m_lstStatuts
        End Get
        Set(ByVal value As ObservableCollection(Of Statut))
            m_lstStatuts = value
            OnPropertyChanged("ListeDesStatuts")
        End Set
    End Property

    Private m_Is_BtnChercher_Enabled As Boolean = False
    Public Property Is_BtnChercher_Enabled As Boolean
        Get
            Return m_Is_BtnChercher_Enabled
        End Get
        Set(ByVal value As Boolean)
            m_Is_BtnChercher_Enabled = value
            OnPropertyChanged("Is_BtnChercher_Enabled")
        End Set
    End Property

    Private m_Is_BtnAnnuler_Enabled As Boolean = False
    Public Property Is_BtnAnnuler_Enabled As Boolean
        Get
            Return m_Is_BtnAnnuler_Enabled
        End Get
        Set(ByVal value As Boolean)
            m_Is_BtnAnnuler_Enabled = value
            OnPropertyChanged("Is_BtnAnnuler_Enabled")
        End Set
    End Property

    Private m_IHM_Messages As String
    Public Property IHM_Messages As String
        Get
            Return m_IHM_Messages
        End Get
        Set(ByVal value As String)
            m_IHM_Messages = value
            OnPropertyChanged("IHM_Messages")
        End Set
    End Property

    Private m_IHM_ProgressMax As Double = 0
    Public Property IHM_ProgressMax As Double
        Get
            Return m_IHM_ProgressMax
        End Get
        Set(ByVal value As Double)
            m_IHM_ProgressMax = value
            OnPropertyChanged("IHM_ProgressMax")
        End Set
    End Property

    Private m_IHM_ProgressValeur As Double = 0
    Public Property IHM_ProgressValeur As Double
        Get
            Return m_IHM_ProgressValeur
        End Get
        Set(ByVal value As Double)
            m_IHM_ProgressValeur = value
            OnPropertyChanged("IHM_ProgressValeur")
        End Set
    End Property

    Private _bClicker As Boolean
    Public Property bClicker As Boolean
        Get
            Return _bClicker
        End Get
        Set(value As Boolean)
            _bClicker = value
            OnPropertyChanged("bClicker")
        End Set
    End Property

#End Region

#Region "CONSTRUCTEUR"

    Sub New()

        Try

            If Not DBContext Is Nothing Then

                LesStatuts = From oStatut As Statut In DBContext.Statuts Select oStatut Order By oStatut.IdStatut
                If Not LesStatuts Is Nothing AndAlso LesStatuts.Any Then
                    ListeDesStatuts = LesStatuts.ToList.ToObservableCollection
                End If

            End If

            Sherlock = New BackgroundWorker()
            Sherlock.WorkerReportsProgress = True
            Sherlock.WorkerSupportsCancellation = True

            AddHandler Sherlock.DoWork, AddressOf Sherlock_DoWork
            AddHandler Sherlock.RunWorkerCompleted, AddressOf Sherlock_RunWorkerCompleted
            AddHandler Sherlock.ProgressChanged, AddressOf Sherlock_ProgressChanged

            IHM_Messages = "  Pas de tâche en cours."
            bClicker = False

            Is_BtnChercher_Enabled = True

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Constructeur")
        End Try

    End Sub

#End Region

#Region "BACKGROUNDWORKER"

    Public Sub Sherlock_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        'RECHERCHE_ISRC()
    End Sub

    '   Met a jour la Valeur Value du BGW
    Private Sub Sherlock_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        IHM_ProgressValeur = CDbl(e.ProgressPercentage)
    End Sub

    '   Afficher le Résultat final
    Private Sub Sherlock_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)

        If e.Cancelled Then

            IHM_Messages = "Opération annulée !"

        ElseIf Not e.Error Is Nothing Then

            IHM_Messages = "Une erreur est survenue"
        Else

            IHM_Messages = "Opération terminée"
        End If
    End Sub

#End Region

#Region "COMMANDE ANNULER"

    Private _CMD_ANNULER As ICommand
    Public ReadOnly Property CMD_ANNULER() As ICommand
        Get
            If _CMD_ANNULER Is Nothing Then
                _CMD_ANNULER = New RelayCommand(AddressOf ANNULER_TACHE)
            End If
            Return _CMD_ANNULER
        End Get
    End Property

    Private Sub ANNULER_TACHE()
        Sherlock.CancelAsync()
    End Sub

#End Region

#Region "CMD_CHERCHER_SKU"

    Private _CMD_CHERCHER_SKU As ICommand
    Public ReadOnly Property CMD_CHERCHER_SKU() As ICommand
        Get
            If _CMD_CHERCHER_SKU Is Nothing Then
                _CMD_CHERCHER_SKU = New RelayCommand(AddressOf CHERCHER_SKU)
            End If
            Return _CMD_CHERCHER_SKU
        End Get
    End Property

    Sub CHERCHER_SKU()

        SelectedAccessoire = Nothing

        Dim rq = From oAccessoire As Accessoire In DBContext.Accessoires Where oAccessoire.Sku.Trim.ToUpper.Equals(Me.szSkuToSearch.Trim.ToUpper) Select oAccessoire
        If Not rq Is Nothing AndAlso rq.Any Then
            SelectedAccessoire = rq.First
            szSku = SelectedAccessoire.Sku
            IHM_Messages = "  Recherche terminée avec succès."
        Else
            MsgBox("La recherche de l'accessoire [ " & szSkuToSearch & " ] n'a rien donné comme résultat. Désolé.", vbInformation)
            IHM_Messages = "  Recherche infructueuse."
            szSku = String.Empty
        End If

    End Sub

#End Region

#Region "AJOUTER_IMAGE"

    Private _CMD_AJOUTER_IMAGE As ICommand
    Public ReadOnly Property CMD_AJOUTER_IMAGE() As ICommand
        Get
            If _CMD_AJOUTER_IMAGE Is Nothing Then
                _CMD_AJOUTER_IMAGE = New RelayCommand(AddressOf AJOUTER_IMAGE)
            End If
            Return _CMD_AJOUTER_IMAGE
        End Get
    End Property

    Sub AJOUTER_IMAGE()

        Dim fimg = New FormSelectionImage
        fimg.myViewModel.maFormAppellante = fimg
        fimg.myViewModel.iCle = SelectedAccessoire.IdAccessoire
        fimg.ShowDialog()

    End Sub

#End Region

#Region "FILTRER"

    Private _CMD_FILTRER As ICommand
    Public ReadOnly Property CMD_FILTRER() As ICommand
        Get
            If _CMD_FILTRER Is Nothing Then
                _CMD_FILTRER = New RelayCommand(AddressOf FILTRER)
            End If
            Return _CMD_FILTRER
        End Get
    End Property

    Sub FILTRER()

    End Sub

#End Region

#Region "COMMANDE ANNULER"

    Private _CMD_UPDATE_SKU As ICommand
    Public ReadOnly Property CMD_UPDATE_SKU As ICommand
        Get
            If _CMD_UPDATE_SKU Is Nothing Then
                _CMD_UPDATE_SKU = New RelayCommand(AddressOf UPDATE_SKU)
            End If
            Return _CMD_UPDATE_SKU
        End Get
    End Property

    Private Sub UPDATE_SKU()

        'DBContext.Accessoires.ApplyCurrentValues(SelectedAccessoire)
        DBContext.SaveChanges()


    End Sub

#End Region

#Region "COMMANDE NOUVEAU"

    Private _CMD_NOUVELLE_VALEUR As ICommand
    Public ReadOnly Property CMD_NOUVELLE_VALEUR As ICommand
        Get
            If _CMD_NOUVELLE_VALEUR Is Nothing Then
                _CMD_NOUVELLE_VALEUR = New RelayCommand(AddressOf NOUVELLE_VALEUR)
            End If
            Return _CMD_NOUVELLE_VALEUR
        End Get
    End Property

    Private Sub NOUVELLE_VALEUR()

        Dim fnouveau = New FormNouvelleValeur
        fnouveau.txtCle.Focus()
        fnouveau.ShowDialog()

    End Sub

#End Region

#Region "COMMANDE MODIFIER"

    Private _CMD_MODIFIER_VALEUR As ICommand
    Public ReadOnly Property CMD_MODIFIER_VALEUR As ICommand
        Get
            If _CMD_MODIFIER_VALEUR Is Nothing Then
                _CMD_MODIFIER_VALEUR = New RelayCommand(AddressOf MODIFIER_VALEUR)
            End If
            Return _CMD_MODIFIER_VALEUR
        End Get
    End Property

    Private Sub MODIFIER_VALEUR()

    End Sub

#End Region

#Region "COMMANDE SUPPRIMER"

    Private _CMD_SUPPRIMER_VALEUR As ICommand
    Public ReadOnly Property CMD_SUPPRIMER_VALEUR As ICommand
        Get
            If _CMD_SUPPRIMER_VALEUR Is Nothing Then
                _CMD_SUPPRIMER_VALEUR = New RelayCommand(AddressOf SUPPRIMER_VALEUR)
            End If
            Return _CMD_SUPPRIMER_VALEUR
        End Get
    End Property

    Private Sub SUPPRIMER_VALEUR()

    End Sub

#End Region

#Region "COMMANDE RAPPROCHER"

    Private _CMD_RAPPROCHER As ICommand
    Public ReadOnly Property CMD_RAPPROCHER As ICommand
        Get
            If _CMD_RAPPROCHER Is Nothing Then
                _CMD_RAPPROCHER = New RelayCommand(AddressOf RAPPROCHER)
            End If
            Return _CMD_RAPPROCHER
        End Get
    End Property

    Private Sub RAPPROCHER()

    End Sub

#End Region

#Region "COMMANDE QUITTER"

    '   COMMANDE
    Private _CMD_QUITTER As ICommand
    Public ReadOnly Property CMD_QUITTER() As ICommand
        Get
            If _CMD_QUITTER Is Nothing Then
                _CMD_QUITTER = New RelayCommand(AddressOf FIN_EXECUTION)
            End If
            Return _CMD_QUITTER
        End Get
    End Property

    '   METHODE
    Private Sub FIN_EXECUTION()
        My.Application.Shutdown()
    End Sub

#End Region

End Class
