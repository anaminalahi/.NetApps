Public Class RegexFound
    Enum LeMotClef As Byte
        Api32 = 0
        CreateObject = 1
        ExcelApp = 2
        WordApp = 3
        UserCtrl = 4
    End Enum

    Property MotClef As LeMotClef
    Property Position As Integer = 0
    Property ExtraitDeLigne As String

End Class


