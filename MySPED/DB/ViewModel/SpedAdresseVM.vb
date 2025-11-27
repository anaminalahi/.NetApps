Public Class SpedAdresseVM

    Public Property TitreDeLaPageEnCours As String

    Public Property AdresseAChercher As String = String.Empty
    Public Property IdAdresse As Integer
    Public Property Numero As String = String.Empty
    Public Property CodePostal As String = String.Empty
    Public Property Ville As String = String.Empty
    Public Property TypeAdresse As String = String.Empty
    Public Property TypeDeVoie As String = String.Empty
    Public Property LibelleVoie As String = String.Empty
    Public Property Adresse2 As String = String.Empty
    Public Property Bureau As String = String.Empty
    Public Property Pays As String = String.Empty
    Public Property Id_pays As String = String.Empty
    Public Property DateSaisie As Nullable(Of Date)
    Public Property Actuelle As Nullable(Of Boolean)
    Public Property IntegreDansBachON As Nullable(Of Boolean)
    Public Property IdSpedinaute As Integer

    Public Property MonTypeDAdresse As String = String.Empty
    Public Property MonTypeDeVoie As String = String.Empty

    Public Property MonPays As String = String.Empty

    Public Overridable Property Spedinautes As Spedinaute
End Class
