Imports System.Web.Mvc

Namespace Controllers

    Public Class AssembleesController
        Inherits Controller

        Private Property UneAssembleeVM As DB.AssembleeVM
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

            MyManager.ValeursErreurs = String.Empty

            If Not MyManager.UneDeclaration Is Nothing Then

                MyManager.TitreDeLaPageEnCours = String.Empty
                MyManager.UneDeclaration = Nothing

            End If

            If Not MyManager.MesDroits.IsMenuGeneralAssembly Then
                Return RedirectToAction("Index", "Spedinaute")
            End If

            '   Recupere l'Assemblee courante et les documents joints
            MyManager.AssembleeGenerale = MyManager.GetAssemblees()
            If MyManager.AssembleeGenerale IsNot Nothing Then

                UneAssembleeVM = AutoMapper.Mapper.Map(Of DB.AssembleeVM)(MyManager.AssembleeGenerale)
                UneAssembleeVM.LeJourEnTexte = CDate(UneAssembleeVM.DateDuJour).ToLongDateString + " à " + UneAssembleeVM.Horaire

                Dim lstDocs As New List(Of DB.PdfDoc)
                For Each elmts In MyManager.AssembleeGenerale.DocumentDisponible

                    Dim unDoc = New DB.PdfDoc

                    unDoc.NumeroOrdre = elmts.NumeroDOrdre
                    unDoc.TitreDuSujet = elmts.TitreDuSujet
                    unDoc.EmplacementDuFichier = elmts.EmplacementDuFichier

                    lstDocs.Add(unDoc)

                Next

                UneAssembleeVM.ListeDesDocuments = (From obj In lstDocs Order By obj.NumeroOrdre).ToList
                UneAssembleeVM.TitreDeLaPageEnCours = "Assemblées Générales de la SPEDIDAM"
            End If

            Return View(UneAssembleeVM)

        End Function

    End Class

End Namespace