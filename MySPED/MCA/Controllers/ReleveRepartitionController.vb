Imports System.Web.Mvc

Namespace Controllers

    Public Class ReleveRepartitionController
        Inherits Controller

        Private Property UneAssembleeVM As DB.AssembleeVM

        ' GET: ReleveRepartion
        Private MyManager As DB.Manager

        ' GET: Assemblees
        Function Index() As ActionResult

            ' Soit prévient le Spedinaute que la session est finie
            If Session Is Nothing Then
                Return RedirectToAction("Index", "SessionExpiree")
            End If

            '   Récupere Tpujours le Manager courant
            MyManager = System.Web.HttpContext.Current.Session("User")

            If MyManager Is Nothing Then Return RedirectToAction("Index", "Connexion")

            If Not MyManager.UneDeclaration Is Nothing Then

                MyManager.TitreDeLaPageEnCours = String.Empty
                MyManager.UneDeclaration = Nothing

            End If



            Return View()

        End Function

    End Class

End Namespace