Public Class Formulaire
    Property NomDuFormulaire As String
    Property NomDuFichierFRM As String
    Property NombreDeUserControles As Integer = 0
    Property NombreDeCreateObject As Integer = 0
    Property NombreDApi32 As Integer = 0
    Property NombreDeExcelApp As Integer = 0
    Property NombreDeWordApp As Integer = 0
    Property NombreDeOfficeouAutre As Integer = 0
    Property NombreDeLignesActives As Integer = 0
    Property ListofRegeX As New List(Of RegexFound)

End Class
