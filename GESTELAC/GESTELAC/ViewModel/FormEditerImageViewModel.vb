Imports System.IO
Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Net
Imports System.Threading

Public Class FormEditerImageViewModel
    Inherits ViewModelBase


#Region "VARIABLES LOCALES"
    Private CMF As Microsoft.Win32.OpenFileDialog
    Private oNomDuFichier As String
#End Region

#Region "PROPRIETES"

    Property PrenomsEtNom As String


    Private mnomfichierPJ As String
    Public Property NomDuFichierPJ As String
        Get
            Return mnomfichierPJ
        End Get
        Set(ByVal value As String)
            mnomfichierPJ = value
            OnPropertyChanged("NomDuFichierPJ")
        End Set
    End Property

    Private m_maFormAppellante As FormEditerImage
    Public Property maFormAppellante As FormEditerImage
        Get
            Return m_maFormAppellante
        End Get
        Set(ByVal value As FormEditerImage)
            m_maFormAppellante = value
        End Set
    End Property


    Private mSelectedCarte As Accessoire_Visuel
    Public Property SelectedCarte As Accessoire_Visuel
        Get
            Return mSelectedCarte
        End Get
        Set(value As Accessoire_Visuel)
            mSelectedCarte = value
            OnPropertyChanged("SelectedCarte")
        End Set
    End Property

    Private b_CreationOuModification As Boolean
    Public Property CreationOuModification As Boolean
        Get
            Return b_CreationOuModification
        End Get
        Set(ByVal value As Boolean)
            b_CreationOuModification = value
            OnPropertyChanged("CreationOuModification")
        End Set
    End Property

#End Region

    Sub New()

        '   Instanciation de FileDialog
        CMF = New Microsoft.Win32.OpenFileDialog
        CMF.Filter = "Fichiers Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|Tous les fichiers(*.*)|*.*"
        CMF.InitialDirectory = NomDuDossierFtpLocal
        CMF.FileName = ""
        NomDuFichierPJ = "Veuillez sélectionner la photo d'identité svp."

    End Sub

#Region "CMD_SELECTIONNER_FICHIER"

    Private _CMD_SELECTIONNER_FICHIER As ICommand
    Public ReadOnly Property CMD_SELECTIONNER_FICHIER() As ICommand
        Get
            If _CMD_SELECTIONNER_FICHIER Is Nothing Then
                _CMD_SELECTIONNER_FICHIER = New RelayCommand(AddressOf SelectionnerFichier)
            End If
            Return _CMD_SELECTIONNER_FICHIER
        End Get
    End Property

    Sub SelectionnerFichier()

        '   Ouvre l'Explorateurs de fichiers
        CMF.InitialDirectory = NomDuDossierFtpLocal     '   System.Windows.Forms.Application.StartupPath
        CMF.FileName = ""

        If CMF.ShowDialog() Then
            NomDuFichierPJ = CMF.FileName   '   NomduFichier et Dossier
            oNomDuFichier = CMF.SafeFileName
        Else
            NomDuFichierPJ = "Veuillez sélectionner la photo d'identité svp."
            oNomDuFichier = String.Empty
        End If

    End Sub

#End Region

#Region "COMMANDE MISE_A_JOUR"

    Private _CMD_MISE_A_JOUR As ICommand
    Public ReadOnly Property CMD_MISE_A_JOUR() As ICommand
        Get
            If _CMD_MISE_A_JOUR Is Nothing Then
                _CMD_MISE_A_JOUR = New RelayCommand(AddressOf MajCardTable)
            End If
            Return _CMD_MISE_A_JOUR
        End Get
    End Property

    Sub MajCardTable()

        Dim TempImage = GetPhoto(NomDuFichierPJ)

        If Not TempImage Is Nothing AndAlso Not SelectedCarte.PHOTO_ID Is Nothing Then

            If Not SelectedCarte.PHOTO_ID.Equals(TempImage) Then

                SelectedCarte.PHOTO_ID = TempImage
                RecopiesFichierDansLeDossier(NomDuFichierPJ)

            End If

        ElseIf SelectedCarte.PHOTO_ID Is Nothing Then

            SelectedCarte.PHOTO_ID = TempImage
            RecopiesFichierDansLeDossier(NomDuFichierPJ)

        End If

        CtxSpallDb.CARD_TABLE.ApplyCurrentValues(SelectedCarte)
        CtxSpallDb.SaveChanges()

        MiseAJourRequise = True

        Me.maFormAppellante.Close()

    End Sub


    Public Shared Function GetPhoto(ByVal filePath As String) As Byte()

        Dim photo() As Byte = Nothing

        If My.Computer.FileSystem.FileExists(filePath) _
                         AndAlso My.Computer.FileSystem.GetFileInfo(filePath).Length > 0 Then

            Dim stream As FileStream = New FileStream(filePath, FileMode.Open, FileAccess.Read)
            Dim reader As BinaryReader = New BinaryReader(stream)

            photo = reader.ReadBytes(stream.Length)

            reader.Close()
            stream.Close()

        End If

        Return photo

    End Function


    Public Shared Function GetPhotoFromUrl(ByVal filePath As String) As Byte()

        Dim photo() As Byte = Nothing

        If My.Computer.FileSystem.FileExists(filePath) _
                         AndAlso My.Computer.FileSystem.GetFileInfo(filePath).Length > 0 Then

            Dim stream As FileStream = New FileStream(filePath, FileMode.Open, FileAccess.Read)
            Dim reader As BinaryReader = New BinaryReader(stream)

            photo = reader.ReadBytes(stream.Length)

            reader.Close()
            stream.Close()

        End If

        Return photo

    End Function

    'Private Function CarteExiste(ByVal oAdherent As ADHESION_ARRIVEE) As Boolean
    '    Dim carteTrouve As Boolean = False

    '    Dim rqCard = From oCarte As CARD_TABLE In CtxSpallDb.CARD_TABLE Where oCarte.NUM_ADHERENT.Trim = oAdherent.NUM_ADHERENT.Trim Select oCarte
    '    If Not rqCard Is Nothing AndAlso rqCard.Count > 0 Then
    '        carteTrouve = True
    '    End If

    '    Return carteTrouve
    'End Function

#End Region

#Region "COMMANDE ANNULER"

    Private _CMD_ANNULER As ICommand
    Public ReadOnly Property CMD_ANNULER() As ICommand
        Get
            If _CMD_ANNULER Is Nothing Then
                _CMD_ANNULER = New RelayCommand(AddressOf Annuler)
            End If
            Return _CMD_ANNULER
        End Get
    End Property

    Private Sub Annuler()

        '   ANNULATION 
        'DBContext.Accessoires_Visuels.ApplyOriginalValues(SelectedCarte)
        Me.maFormAppellante.Close()

    End Sub

#End Region

#Region "COMMANDE FERMER"

    Private _CMD_FERMER As ICommand
    Public ReadOnly Property CMD_FERMER() As ICommand
        Get
            If _CMD_FERMER Is Nothing Then
                _CMD_FERMER = New RelayCommand(AddressOf FERMER)
            End If
            Return _CMD_FERMER
        End Get
    End Property

    Private Sub FERMER()

        Me.maFormAppellante.Close()

    End Sub

#End Region

End Class

