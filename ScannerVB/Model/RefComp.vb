Public Class RefComp

    Enum LeType As Byte
        Ocx = 0
        Ref = 1
    End Enum

    Property NomApplication As String
    Property NomDuProjet As String
    Property MotClef As LeType
    Property NomDuFichierRefOCxTlb As String
    Property libelleRefOCxTlb As String
    Property ExtraitDeLigneVBP As String

End Class
