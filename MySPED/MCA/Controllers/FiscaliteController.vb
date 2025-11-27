Imports System.Web.Mvc

Namespace Views.Fiscalite

    Public Class FiscaliteController
        Inherits Controller

        Private Property AnneeChoisie As String
        Private Property DecFisEnBase As DB.DeclarationFiscale

        Private MyManager As DB.Manager

        <HandleError>
        <HttpGet>
        Function Index() As ActionResult

            ' Soit prévient le Spedinaute que la session est finie
            If Session Is Nothing Then
                Return RedirectToAction("Index", "SessionExpiree")
            End If

            '   Récupere Tpujours le Manager courant
            MyManager = System.Web.HttpContext.Current.Session("User")
            If MyManager Is Nothing Then Return RedirectToAction("Index", "Connexion")

            MyManager.ValeursErreurs = String.Empty

            If MyManager.UneDeclaration Is Nothing Then

                MyManager.TitreDeLaPageEnCours = String.Empty
                MyManager.UneDeclaration = New DB.DeclarationVM

            End If

            MyManager.UneDeclaration.TitreDeLaPageEnCours = "Justificatifs Fiscaux"

            Return View(MyManager.UneDeclaration)

        End Function


        <HandleError>
        <HttpPost>
        <ValidateAntiForgeryToken>
        Function Index(ficheeditee As DB.DeclarationVM) As ActionResult

            '   Récupere Tpujours le Manager courant
            MyManager = System.Web.HttpContext.Current.Session("User")
            MyManager.ValeursErreurs = String.Empty

            If MyManager Is Nothing Then Return RedirectToAction("Index", "Connexion")

            Dim rq = From oDec In MyManager.DbContext.DeclarationFiscales
                     Where (oDec.Annee.Equals(ficheeditee.SelectedAnnee) AndAlso (oDec.NumeroIdAyant.Equals(MyManager.NumeroIdAyant))) Select oDec

            If Not rq Is Nothing AndAlso rq.Any Then

                MyManager.UneDeclaration.FichierPDF = "/declarationsfiscales/" + ficheeditee.SelectedAnnee + "/pdfs/" + rq.ToList.First.NomDuFichier.NulleOuNon + ".pdf"
                MyManager.UneDeclaration.IsFound = True
            Else

                MyManager.UneDeclaration.FichierPDF = "Pas trouvé"
                MyManager.UneDeclaration.IsFound = False

            End If

            Return View("Index", MyManager.UneDeclaration)

        End Function

    End Class

End Namespace