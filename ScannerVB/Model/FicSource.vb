Public Class FicSource

    Property NomIntFicSource As String
    Property NomExtFicSource As String
    Property Emplacement As String
    Property TypeFicSource As TypeSource.TypeFichier

    Property NombreDeCreateObject As Integer = 0
    Property NombreDApi32 As Integer = 0
    Property NombreDeExcelApp As Integer = 0
    Property NombreDeWordApp As Integer = 0
    Property NombreDeOfficeouAutre As Integer = 0
    Property NombreDeUserControles As Integer = 0
    Property NombreDeLignesActives As Integer = 0

    Property ListofRegeX As New List(Of RegexFound)

    Property NomApplication As String
    Property NomDuFichierVBP As String

End Class
