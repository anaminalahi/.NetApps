Class Application

    ' Les événements de niveau application, par exemple Startup, Exit et DispatcherUnhandledException
    ' peuvent être gérés dans ce fichier.

    Sub New()

        DBContext = New dbgestelacEntities

        fPrincipale = New FormMain

        If Not fPrincipale Is Nothing Then
            fPrincipale.Show()
        End If

    End Sub

End Class
