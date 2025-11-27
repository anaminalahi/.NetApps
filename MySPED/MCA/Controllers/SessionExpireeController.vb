Imports System.Web.Mvc

Namespace Controllers
    Public Class SessionExpireeController
        Inherits Controller

        Private MyManager As DB.Manager
        ' GET: SessionExpiree

        Function Index() As ActionResult

            If Not MyManager Is Nothing Then
                MyManager.MesDroits.IsLogged = False
                MyManager.MesDroits.IsMenuGeneralAssembly = False
            End If

            If Not Session Is Nothing Then

                Session("isAuth") = False
                Session("user") = Nothing

                Session.Abandon()
                FormsAuthentication.SignOut()

            End If

            Return View()

        End Function

    End Class

End Namespace