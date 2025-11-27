Public Class RepartitionVM

    Public Property IdRepartition As Integer
    Public Property Annee As String
    Public Property MoisRepartition As String
    Public Property TableDecodification As String
    Public Property ReglesDeRepartition As String
    Public Property LettreDAccompagnement As String

    Public Overridable Property DetailsDeRepartion As ICollection(Of DetailsDeRepartion) = New HashSet(Of DetailsDeRepartion)

End Class
