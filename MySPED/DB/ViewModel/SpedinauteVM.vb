Imports System.ComponentModel.DataAnnotations

Public Class SpedinauteVM

    Public Property TitreDeLaPageEnCours As String
    Public Property IdSpedinaute As Integer
    Public Property TypeDeProfil As String
    Public Property Civilite As String
    Public Property Prenom As String
    Public Property Nom As String
    Public Property AutrePrenom As String
    Public Property NomDeJeuneFille As String

    <DataType(DataType.[Date])>
    <DisplayFormat(DataFormatString:="{0:dd/MM/yyyy}", ApplyFormatInEditMode:=True)>
    Public Property DateDeNaissance As Date?

    Public Property LieuDeNaissance As String
    Public Property Nationalite As String
    Public Property NumeroDeMobile As String
    Public Property AdresseEmail As String

    Public Property PasseMot As String
    Public Property NumeroIdAyant As String
    Public Property IPN As String
    Public Property NumeroAD As String
    Public Property NumeroNA As String
    Public Property AccepterEmailInfos As String
    Public Property Enligne As String
    Public Property DernierDateDeConnexion As Nullable(Of Date)
    Public Property NomDuDevice As String
    Public Property AdresseIP As String
    Public Property MotDePasseOublie As Nullable(Of Boolean)
    Public Property CompteUsurpe As Nullable(Of Boolean)
    Public Property DateDUsurpation As Nullable(Of Date)
    Public Property Pseudonyme1 As String
    Public Property Pseudonyme2 As String
    Public Property Pseudonyme3 As String
    Public Property Instrument1 As Nullable(Of Integer)
    Public Property Instrument2 As Nullable(Of Integer)
    Public Property Instrument3 As Nullable(Of Integer)
    Public Property Photo As String
    Public Property IntegreDansBachON As Boolean
    Public Property NumeroDeFixe As String

    Public Overridable Property MotDePasses As ICollection(Of MotDePasse) = New HashSet(Of MotDePasse)
    'Public Overridable Property Repartitions As ICollection(Of Repartition) = New HashSet(Of Repartition)
    'Public Overridable Property Repertoires As ICollection(Of Repertoire) = New HashSet(Of Repertoire)
    Public Overridable Property RibIbans As ICollection(Of RibIban) = New HashSet(Of RibIban)
    Public Overridable Property SpedAdresses As ICollection(Of SpedAdresse) = New HashSet(Of SpedAdresse)
    Public Overridable Property TacheEffectuees As ICollection(Of TacheEffectuee) = New HashSet(Of TacheEffectuee)

    Public ReadOnly Property MaCivilite As String
        Get
            Dim szData As String = String.Empty
            If Not Me.Civilite Is Nothing Then
                If Me.Civilite.Trim = "M." Then
                    szData = "MONSIEUR"
                ElseIf Me.Civilite.Trim = "MME" Then
                    szData = "MADAME"
                ElseIf Me.Civilite.Trim = "MLE" Then
                    szData = "MADEMOISELLE"
                End If
            End If
            Return szData
        End Get
    End Property

    Public Property MaNationalite As String
    Public Property MonInstrument1 As String
    Public Property MonInstrument2 As String
    Public Property MonInstrument3 As String

    Public Property MonAdresseFiscale As SpedAdresseVM
    Public Property MonAdressePostale As SpedAdresseVM

    Public Property MonRib As RibIbanVM

End Class
