Public Class ProjetVBP
    Property NomApplication As String
    Property NomDuFichierVBP As String
    Property TypeDeProjet As String
    Property NomDuProjet As String
    Property NomDeCompilation As String
    Property Startup As String
    Property Emplacement As String


    Property NombreReferenceDLL As Integer = 0
    Property NombreDeFormulaire As Integer = 0
    Property NombreDActiveX As Integer = 0
    Property NombreDeModuleBas As Integer = 0
    Property NombreDeClasse As Integer = 0
    Property NombreDeCreateObject As Integer = 0
    Property NombreDApi32 As Integer = 0
    Property NombreDeExcelApp As Integer = 0
    Property NombreDeWordApp As Integer = 0
    Property NombreDeOfficeouAutre As Integer = 0
    Property NombreDeUserControles As Integer = 0
    Property NombreDeLignesActives As Integer = 0


    Property ListeDeReference As New List(Of ReferenceDLL)
    Property ListeDActiveX As New List(Of ObjectActiveX)
    Property ListeUserControles As New List(Of UserControle)
    Property ListeFormulaire As New List(Of FicSource)
    Property ListeDeModule As New List(Of FicSource)
    Property ListeClasse As New List(Of FicSource)

End Class
