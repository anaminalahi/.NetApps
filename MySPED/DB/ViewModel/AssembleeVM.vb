Public Class AssembleeVM

    Public Property TitreDeLaPageEnCours As String

    Public Property IdAssemblee As Integer
    Public Property Exercice As String
    Public Property DateDuJour As String
    Public Property Horaire As String
    Public Property Lieu As String
    Public Property Adresse As String
    Public Property OrdreDuJourFichier As String
    Public Property LeJourEnTexte As String
    Public Property Actuelle As Nullable(Of Boolean)

    Public Property ListeDesDocuments As List(Of PdfDoc)

End Class

