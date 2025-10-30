Imports System.IO
Imports System.ComponentModel
Imports System.Collections.ObjectModel
Imports System.Net
Imports System.Threading

Public Class FormSelectionImageViewModel
    Inherits ViewModelBase

#Region "VARIABLES LOCALES"
    Private CMF As Microsoft.Win32.OpenFileDialog
    Private oNomDuFichier As String
#End Region

#Region "PROPRIETES"

    Private _bFromDisque As Boolean
    Public Property FromDisque As Boolean
        Get
            Return _bFromDisque
        End Get
        Set(ByVal value As Boolean)
            _bFromDisque = value
            If value = True Then
                FromUrl = False
                szFromUrl = String.Empty
                ImageAccessoire = Nothing
            End If
            OnPropertyChanged("FromDisque")
        End Set
    End Property

    Private _bFromUrl As Boolean
    Public Property FromUrl As Boolean
        Get
            Return _bFromUrl
        End Get
        Set(ByVal value As Boolean)
            _bFromUrl = value
            If value = True Then
                FromDisque = False
                szFromDisque = String.Empty
                ImageAccessoire = Nothing
            End If
            OnPropertyChanged("FromUrl")
        End Set
    End Property

    Private miCle As Integer
    Public Property iCle As Integer
        Get
            Return miCle
        End Get
        Set(ByVal value As Integer)
            miCle = value
            OnPropertyChanged("iCle")
        End Set
    End Property

    Private miTaille As Long
    Public Property iTaille As Long
        Get
            Return miTaille
        End Get
        Set(ByVal value As Long)
            miTaille = value
            OnPropertyChanged("iTaille")
        End Set
    End Property



    Private mszFromDisque As String
    Public Property szFromDisque As String
        Get
            Return mszFromDisque
        End Get
        Set(ByVal value As String)
            mszFromDisque = value
            OnPropertyChanged("szFromDisque")
        End Set
    End Property

    Private mszUrl As String
    Public Property szFromUrl As String
        Get
            Return mszUrl
        End Get
        Set(ByVal value As String)
            mszUrl = value
            OnPropertyChanged("szFromUrl")
        End Set
    End Property


    Private mImageAccessoire As ImageSource
    Public Property ImageAccessoire As ImageSource
        Get
            Return mImageAccessoire
        End Get
        Set(ByVal value As ImageSource)
            mImageAccessoire = value
            OnPropertyChanged("ImageAccessoire")
        End Set
    End Property

    Private mImageFlux As Byte()
    Public Property ImageFlux As Byte()
        Get
            Return mImageFlux
        End Get
        Set(ByVal value As Byte())
            mImageFlux = value
            OnPropertyChanged("ImageFlux")
        End Set
    End Property


    Private m_maFormAppellante As FormSelectionImage
    Public Property maFormAppellante As FormSelectionImage
        Get
            Return m_maFormAppellante
        End Get
        Set(ByVal value As FormSelectionImage)
            m_maFormAppellante = value
        End Set
    End Property


    Private mSelectedImage As Accessoire_Visuel
    Public Property SelectedImage As Accessoire_Visuel
        Get
            Return mSelectedImage
        End Get
        Set(value As Accessoire_Visuel)
            mSelectedImage = value
            OnPropertyChanged("SelectedImage")
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
        CMF.InitialDirectory = System.Windows.Forms.Application.StartupPath
        CMF.FileName = ""

        szFromDisque = String.Empty
        szFromUrl = String.Empty

        FromUrl = True

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

        If FromDisque Then


            szFromUrl = String.Empty

            CMF.FileName = ""
            CMF.InitialDirectory = System.Windows.Forms.Application.StartupPath

            If CMF.ShowDialog() Then

                If FromDisque Then szFromDisque = CMF.FileName
                If FromUrl Then szFromUrl = CMF.FileName

                ChargerLImage()

            Else

                ImageAccessoire = Nothing
                szFromDisque = String.Empty
                szFromUrl = String.Empty

            End If


        ElseIf FromUrl Then

            If Not String.IsNullOrEmpty(szFromUrl) Then

                szFromDisque = String.Empty

                ChargerLImage()

            End If

        End If

    End Sub

    Sub ChargerLImage()

        If FromDisque Then ImageAccessoire = ToImage(GetPhoto(szFromDisque))
        If FromUrl Then ImageAccessoire = ToImage(GetPhotoFromUrl(szFromUrl))

    End Sub

    Public Function GetPhoto(ByVal filePath As String) As Byte()

        Sablier(True)

        Dim photo() As Byte = Nothing

        If My.Computer.FileSystem.FileExists(filePath) _
                         AndAlso My.Computer.FileSystem.GetFileInfo(filePath).Length > 0 Then

            iTaille = My.Computer.FileSystem.GetFileInfo(filePath).Length

            Dim stream As FileStream = New FileStream(filePath, FileMode.Open, FileAccess.Read)
            Dim reader As BinaryReader = New BinaryReader(stream)

            photo = reader.ReadBytes(stream.Length)

            ImageFlux = photo

            reader.Close()
            stream.Close()
            Sablier(False)

        End If

        Return photo

    End Function

    Public Function GetPhotoFromUrl(ByVal filePath As String) As Byte()

        Sablier(True)

        Dim photo() As Byte = Nothing

        Dim stream = New IO.MemoryStream((New Net.WebClient).DownloadData(filePath))
        Dim reader As BinaryReader = New BinaryReader(stream)

        photo = reader.ReadBytes(stream.Length)
        ImageFlux = photo

        reader.Close()
        stream.Close()

        Sablier(False)

        Return photo

    End Function

#End Region

#Region "COMMANDE ENREGISTRER"

    Private _CMD_ENREGISTRER As ICommand
    Public ReadOnly Property CMD_ENREGISTRER() As ICommand
        Get
            If _CMD_ENREGISTRER Is Nothing Then
                _CMD_ENREGISTRER = New RelayCommand(AddressOf Enregistrer_Image)
            End If
            Return _CMD_ENREGISTRER
        End Get
    End Property

    Sub Enregistrer_Image()

        Dim oImg = New Accessoire_Visuel

        oImg.IdAccessoire = iCle
        oImg.ImageAccessoire = ImageFlux
        oImg.DateAjout = Today
        oImg.Taille = iTaille

        DBContext.Accessoires_Visuels.Add(oImg)
        DBContext.SaveChanges()

        Me.maFormAppellante.Close()

    End Sub

#End Region

#Region "COMMANDE ANNULER"

    Private _CMD_ANNULER As ICommand
    Public ReadOnly Property CMD_ANNULER() As ICommand
        Get
            If _CMD_ANNULER Is Nothing Then
                _CMD_ANNULER = New RelayCommand(AddressOf ANNULER)
            End If
            Return _CMD_ANNULER
        End Get
    End Property

    Private Sub ANNULER()

        Me.maFormAppellante.Close()

    End Sub

#End Region

End Class
