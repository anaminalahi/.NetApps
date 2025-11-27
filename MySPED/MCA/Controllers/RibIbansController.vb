Imports System.Data.Entity
Imports System.Net.Mail
Imports DB

Namespace Controllers

    Public Class RibIbansController
        Inherits System.Web.Mvc.Controller

#Region "PROPRIETES"

        'Private IbanControleur As New WebIBANBICMgr.BANBICSoapClient
        Private Property ErrMessage As String
        Private bAddRib As Boolean

        Private MyManager As DB.Manager

        Public Property SelectedRib As RibIban
        Public Property SelectedRibVM As RibIbanVM

#End Region

        <HttpGet>
        Function Index() As ActionResult

            ' Soit prévient le Spedinaute que la session est finie
            If Session Is Nothing Then
                Return RedirectToAction("Index", "SessionExpiree")
            End If

            '   Récupere Tpujours le Manager courant
            MyManager = System.Web.HttpContext.Current.Session("User")
            If MyManager Is Nothing Then Return RedirectToAction("Index", "Connexion")

            '   Mise a Jour des Infos pour le ViewBag
            MyManager.ValeursErreurs = String.Empty

            If Not MyManager.UneDeclaration Is Nothing Then

                MyManager.TitreDeLaPageEnCours = String.Empty
                MyManager.UneDeclaration = Nothing

            End If

            bAddRib = False
            SelectedRib = MyManager.GetTVraiRib(MyManager.IdSpedinaute)

            '   Si on est pas en mode saisie
            If MyManager.bMajEnCoursDuRib = False Then

                '   Si pas de Rib Web - Creation d'un Rib Vierge
                If SelectedRib Is Nothing Then

                    bAddRib = True
                    MyManager.bMajEnCoursDuRib = False

                    SelectedRib = New RibIban
                    SelectedRib.IdSpedinaute = MyManager.IdSpedinaute
                    SelectedRib.Titulaire = MyManager.SpedinauteConnecte
                    SelectedRibVM = AutoMapper.Mapper.Map(Of RibIbanVM)(SelectedRib)

                    MyManager.RibActuel = SelectedRib
                    MyManager.RibEdite = SelectedRibVM

                Else

                    '   Si Rib Web - Clonage pour vue LS
                    bAddRib = False

                    SelectedRib.Titulaire = MyManager.SpedinauteConnecte
                    SelectedRibVM = AutoMapper.Mapper.Map(Of RibIbanVM)(SelectedRib)

                    If Not SelectedRibVM.Id_pays Is Nothing Then
                        SelectedRibVM.Id_pays = SelectedRibVM.Id_pays.Trim()
                        SelectedRibVM.MonPays = MyManager.GetNationalite(SelectedRibVM.Id_pays)
                    End If

                    MyManager.RibActuel = SelectedRib
                    MyManager.RibEdite = SelectedRibVM

                End If

            Else
                '   Si on est en mode saisie

                If Not SelectedRibVM Is Nothing Then

                    If Not SelectedRibVM.Id_pays Is Nothing Then
                        SelectedRibVM.Id_pays = SelectedRibVM.Id_pays.Trim()
                        SelectedRibVM.MonPays = MyManager.GetNationalite(SelectedRibVM.Id_pays)
                    End If

                    SelectedRibVM.Titulaire = MyManager.SpedinauteConnecte

                End If

            End If

            SelectedRibVM.TitreDeLaPageEnCours = "Relevé d'identité Bancaire"

            Return View(SelectedRibVM)

        End Function

        Function AnnulerSaisie() As ActionResult

            ' Soit prévient le Spedinaute que la session est finie
            If Session Is Nothing Then
                Return RedirectToAction("Index", "SessionExpiree")
            End If

            '   Récupere Tpujours le Manager courant
            MyManager = System.Web.HttpContext.Current.Session("User")

            '   Désactivation du Mode Saisie
            MyManager.bMajEnCoursDuRib = False
            MyManager.ValeursErreurs = String.Empty

            Return RedirectToAction("Index", "RibIbans")

        End Function

        <HttpGet>
        Function ChangerRIB() As ActionResult

            ' Soit prévient le Spedinaute que la session est finie
            If Session Is Nothing Then
                Return RedirectToAction("Index", "SessionExpiree")
            End If

            '   Récupere Tpujours le Manager courant
            MyManager = System.Web.HttpContext.Current.Session("User")

            If MyManager Is Nothing Then Return RedirectToAction("Index", "Connexion")

            '   Activation du Mode Saisi
            MyManager.bMajEnCoursDuRib = True
            ViewBag.ErrMessage = MyManager.ValeursErreurs

            If MyManager.RibEdite Is Nothing Then

                SelectedRibVM = New RibIbanVM
                SelectedRibVM.Titulaire = MyManager.SpedinauteConnecte
                SelectedRibVM.IdSpedinaute = MyManager.IdSpedinaute

            Else

                SelectedRibVM = MyManager.RibEdite

                If Not String.IsNullOrEmpty(SelectedRibVM.Id_pays) Then
                    SelectedRibVM.Id_pays = SelectedRibVM.Id_pays.Trim()
                    SelectedRibVM.MonPays = MyManager.GetNationalite(SelectedRibVM.Id_pays)
                End If

                SelectedRibVM.Titulaire = MyManager.SpedinauteConnecte

            End If

            SelectedRibVM.TitreDeLaPageEnCours = "Changement Du RIB"

            Return View(SelectedRibVM)

        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Function ChangerRIB(ficheeEditee As RibIbanVM) As ActionResult

            ' Soit prévient le Spedinaute que la session est finie
            If Session Is Nothing Then
                Return RedirectToAction("Index", "SessionExpiree")
            End If

            '   Récupere Tpujours le Manager courant
            MyManager = System.Web.HttpContext.Current.Session("User")

            If MyManager Is Nothing Then Return RedirectToAction("Index", "Connexion")

            '   Controle des saisies
            If String.IsNullOrEmpty(ficheeEditee.Id_pays) Then
                MyManager.RibEdite = ficheeEditee
                MyManager.ValeursErreurs = "[ veuillez saisir Choisir le [ pays ] svp ]"
                Return RedirectToAction("ChangerRIB", "RibIbans")
            End If

            If String.IsNullOrEmpty(ficheeEditee.BicSwift) Then
                MyManager.RibEdite = ficheeEditee
                MyManager.ValeursErreurs = "[ veuillez saisir le [ BicSwift ] svp ]"
                Return RedirectToAction("ChangerRIB", "RibIbans")
            End If

            If String.IsNullOrEmpty(ficheeEditee.NumeroIBAN) Then
                MyManager.RibEdite = ficheeEditee
                MyManager.ValeursErreurs = "[ veuillez saisir le [ Numéro IBAN ] svp ]"
                Return RedirectToAction("ChangerRIB", "RibIbans")
            End If

            If String.IsNullOrEmpty(ficheeEditee.Domiciliation) Then
                MyManager.RibEdite = ficheeEditee
                MyManager.ValeursErreurs = "[ veuillez saisir la [ Domiciliation ] svp ]"
                Return RedirectToAction("ChangerRIB", "RibIbans")
            End If

            '   Controler le Bic swift avec le webservice
            '   Controle de Validité du RIB
            ficheeEditee.NumeroIBAN = ficheeEditee.NumeroIBAN.ToUpper
            Dim ControleOK = CorrectIBAN(ficheeEditee.NumeroIBAN.ToUpper)

            If Not ControleOK Then

                ' ReCharge la fiche Rib
                MyManager.RibEdite = ficheeEditee
                MyManager.ValeursErreurs = "[ Votre IBAN n'est pas valide. Reprenez svp ]"
                Return RedirectToAction("ChangerRIB", "RibIbans", SelectedRibVM)

            End If

            '   Controle s'il y'a eu un changement de rib dans la saisie en cours
            Dim testRib = String.Empty

            testRib = MyManager.RibActuel.BicSwift.NulleOuNon
            testRib = testRib + MyManager.RibActuel.NumeroIBAN.NulleOuNon
            testRib = testRib + MyManager.RibActuel.Domiciliation.NulleOuNon
            testRib = testRib + MyManager.RibActuel.Id_pays.NulleOuNon
            testRib = testRib + MyManager.RibActuel.Guichet.NulleOuNon
            testRib = testRib + MyManager.RibActuel.NumeroCompte.NulleOuNon
            testRib = testRib + MyManager.RibActuel.CodeBanque.NulleOuNon


            Dim testRibVM = String.Empty

            testRibVM = ficheeEditee.BicSwift.NulleOuNon
            testRibVM = testRibVM + ficheeEditee.NumeroIBAN.NulleOuNon
            testRibVM = testRibVM + ficheeEditee.Domiciliation.NulleOuNon
            testRibVM = testRibVM + ficheeEditee.Id_pays.NulleOuNon
            testRibVM = testRibVM + ficheeEditee.Guichet.NulleOuNon
            testRibVM = testRibVM + ficheeEditee.NumeroCompte.NulleOuNon
            testRibVM = testRibVM + ficheeEditee.CodeBanque.NulleOuNon

            If Not testRib.Trim.Equals(testRibVM.Trim) Then

                Dim LaKleTcheMwen = MyManager.NumeroIdAyant.trim
                Dim LaKleKheulbou = MyManager.IdSpedinaute

                '   ComparaisonPourHistorisation
                Dim OnaFaitQuoi As [String] = [String].Empty

                OnaFaitQuoi = OnaFaitQuoi + "<P>Vos Coordonnées bancaires ont changé. Votre nouveau RIB est le suivant :</p><br></br>"

                OnaFaitQuoi += "<ul>"
                OnaFaitQuoi += "<li>" + " BIC/SWIFT     : " + ficheeEditee.BicSwift + "</li>"
                OnaFaitQuoi += "<li>" + " N° IBAN       : " + ficheeEditee.NumeroIBAN + "</li>"
                OnaFaitQuoi += "<li>" + " DOMICILIATION : " + ficheeEditee.Domiciliation + "</li>"
                OnaFaitQuoi += "<li>" + " PAYS : " + MyManager.GetNationalite(ficheeEditee.Id_pays.Trim) + "</li>"
                OnaFaitQuoi += "</ul><br></br>"

                '   Archivage du Rib Existant
                If Not MyManager.RibActuel Is Nothing AndAlso MyManager.RibActuel.IdRibIban > 0 Then

                    MyManager.RibActuel.Actuel = False

                    MyManager.DbContext.Entry(MyManager.RibActuel).State = EntityState.Modified
                    MyManager.DbContext.SaveChanges()

                End If

                Dim NouveauRib = New RibIban

                NouveauRib.Titulaire = MyManager.SpedinauteConnecte
                NouveauRib.BicSwift = ficheeEditee.BicSwift.NulleOuNon
                NouveauRib.CodeBanque = String.Empty 'ficheeEditee.CodeBanque
                NouveauRib.Guichet = String.Empty 'ficheeEditee.Guichet
                NouveauRib.NumeroCompte = String.Empty 'ficheeEditee.NumeroCompte
                NouveauRib.NumeroIBAN = ficheeEditee.NumeroIBAN.NulleOuNon
                NouveauRib.CleRIB = String.Empty 'ficheeEditee.CleRIB
                NouveauRib.Domiciliation = ficheeEditee.Domiciliation

                NouveauRib.Id_pays = ficheeEditee.Id_pays.Trim

                NouveauRib.DateDeSaisie = Today
                NouveauRib.IdSpedinaute = MyManager.IdSpedinaute
                NouveauRib.IntegreDansBachON = True
                NouveauRib.Actuel = True

                MyManager.DbContext.RibIbans.Add(NouveauRib)
                MyManager.DbContext.SaveChanges()

                '   Chargement du nouveau rib
                SelectedRib = MyManager.GetTVraiRib(MyManager.IdSpedinaute)
                SelectedRib.Titulaire = MyManager.SpedinauteConnecte

                SelectedRibVM = AutoMapper.Mapper.Map(Of RibIbanVM)(SelectedRib)

                '   Mise a jour mu shared myManager
                MyManager.RibActuel = SelectedRib
                MyManager.RibEdite = SelectedRibVM

                '   Désactivation du Mode Saisie
                MyManager.bMajEnCoursDuRib = False
                MyManager.ValeursErreurs = String.Empty

                Dim UneTache = New TacheEffectuee()

                UneTache.TypeDeTache = "M"
                UneTache.DateDuJour = DateTime.Today.ToShortDateString()
                UneTache.HeureCourante = DateTime.Now.TimeOfDay.ToString()

                UneTache.IdAdresseConcernee = "N"
                UneTache.IdModePasseConcerne = "N"
                UneTache.IdRibIbanConcerne = "O"
                UneTache.FicheConcernee = "N"

                UneTache.Commentaire = "NOUVEAU RIB CREE LE " + DateTime.Today.ToShortDateString()
                UneTache.HistoriserON = True

                UneTache.IdSpedinaute = LaKleKheulbou

                MyManager.DbContext.TacheEffectuees.Add(UneTache)
                MyManager.DbContext.SaveChanges()

                NotificationdEmail(OnaFaitQuoi, ficheeEditee, MyManager.MaFiche)

                '   MISE A JOUR DE LA FICHE DANS GESPERE
                If MyManager.DbCtx IsNot Nothing Then

                    MyManager.bDone = True

                    If Not MyManager.SelectedBeneficiaire Is Nothing Then

                        MyManager.SelectedBeneficiaire.nombanqu = MyManager.SpedinauteConnecte

                        MyManager.SelectedBeneficiaire.bic = SelectedRibVM.BicSwift.NulleOuNon
                        MyManager.SelectedBeneficiaire.iban = SelectedRibVM.NumeroIBAN.NulleOuNon
                        MyManager.SelectedBeneficiaire.lb_domic = SelectedRibVM.Domiciliation
                        MyManager.SelectedBeneficiaire.id_modpa = "VIR"

                        '   Parce que dans Gespere le Code du Pays du Rib est Géré par le paybanqu
                        MyManager.SelectedBeneficiaire.paybanqu = SelectedRibVM.Id_pays.Trim

                        MyManager.SelectedBeneficiaire.notes = MyManager.SelectedBeneficiaire.notes + Chr(13) + DateTime.Today.ToShortDateString().Replace("/", "-") + " : NOUVEAU RIB SAISI LE " + DateTime.Today.ToShortDateString()

                        MyManager.DbCtx.Entry(MyManager.SelectedBeneficiaire).State = EntityState.Modified
                        MyManager.DbCtx.SaveChanges()

                    End If

                    If Not MyManager.SelectedAyantDroit Is Nothing Then

                        MyManager.SelectedAyantDroit.dt_maj = DateTime.Today
                        MyManager.SelectedAyantDroit.notes = MyManager.SelectedAyantDroit.notes + Chr(13) + DateTime.Today.ToShortDateString().Replace("/", "-") + " : NOUVEAU RIB DE SAISI LE " + DateTime.Today.ToShortDateString()

                        MyManager.DbCtx.Entry(MyManager.SelectedAyantDroit).State = EntityState.Modified
                        MyManager.DbCtx.SaveChanges()

                    End If

                    '  HISTORISATION DE LA TACHE DANS LA BASE GESPERE

                    Dim ArchiveDuRib = New RibIbanArchive

                    ArchiveDuRib.Titulaire = MyManager.SpedinauteConnecte
                    ArchiveDuRib.BicSwift = SelectedRibVM.BicSwift
                    ArchiveDuRib.CodeBanque = String.Empty 'SelectedRibVM.CodeBanque
                    ArchiveDuRib.Guichet = String.Empty 'SelectedRibVM.Guichet
                    ArchiveDuRib.NumeroCompte = String.Empty 'SelectedRibVM.NumeroCompte
                    ArchiveDuRib.NumeroIBAN = SelectedRibVM.NumeroIBAN
                    ArchiveDuRib.CleRIB = String.Empty 'SelectedRibVM.CleRIB
                    ArchiveDuRib.Domiciliation = SelectedRibVM.Domiciliation
                    ArchiveDuRib.Pays = SelectedRibVM.Id_pays
                    ArchiveDuRib.id_ayant = LaKleTcheMwen
                    ArchiveDuRib.IdSpedinaute = MyManager.IdSpedinaute

                    MyManager.DbCtx.RibIbanArchives.Add(ArchiveDuRib)

                    Dim UneTacheB = New TacheHistorisee()

                    UneTacheB.TypeDeTache = "M"
                    UneTacheB.DateDuJour = DateTime.Today.ToShortDateString()
                    UneTacheB.HeureCourante = DateTime.Now.TimeOfDay

                    UneTacheB.IdAdresseConcernee = "N"
                    UneTacheB.IdModePasseConcerne = "N"
                    UneTacheB.IdRibIbanConcerne = "O"
                    UneTacheB.FicheConcernee = "N"

                    UneTacheB.Commentaire = "NOUVEAU RIB CREE LE " + DateTime.Today.ToShortDateString()
                    UneTacheB.HistoriserON = True

                    UneTacheB.Id_ayant = LaKleTcheMwen
                    UneTache.IdSpedinaute = MyManager.IdSpedinaute
                    UneTacheB.HistoriserON = True

                    MyManager.DbCtx.TacheHistorisees.Add(UneTacheB)
                    MyManager.DbCtx.SaveChanges()

                End If

                '   Prévoir un Popup d'Information

            Else

                '   Prévoir un Popup d'Information
                '   Désactivation du Mode Saisie
                MyManager.bMajEnCoursDuRib = False
                MyManager.ValeursErreurs = String.Empty

            End If

            '  Charge la fiche Rib
            Return RedirectToAction("Index", "RibIbans")

        End Function

        Public Function CorrectIBAN(ByVal objIban As String)

            Dim bOk As Boolean

            Try

                Dim IBAN_chaine = CStr(objIban)
                Dim IBAN_nettoye = ""

                'supprimer les caractères indésirables (garde: 0-9 et A-Z, transforme minuscules en majuscules)
                For IBAN_char = 1 To Len(IBAN_chaine)
                    Dim IBAN_char_test = Mid(IBAN_chaine, IBAN_char, 1)
                    Select Case IBAN_char_test
                        Case 0 To 9, "a" To "z", "A" To "Z"
                            IBAN_nettoye = IBAN_nettoye & UCase(IBAN_char_test)
                        Case Else
                    End Select
                Next IBAN_char

                'replacer les 4 premiers caractères à la fin
                Dim IBAN_position = Right(IBAN_nettoye, Len(IBAN_nettoye) - 4) & Left(IBAN_nettoye, 4)

                'remplacer les lettres par chiffres
                Dim IBAN_numeric = ""
                For IBAN_char2 = 1 To Len(IBAN_position)
                    Dim IBAN_char_test2 = Mid(IBAN_position, IBAN_char2, 1)
                    Select Case IBAN_char_test2
                        Case "A" To "Z"
                            IBAN_numeric = IBAN_numeric & CStr(Asc(IBAN_char_test2) - 55)
                        Case Else
                            IBAN_numeric = IBAN_numeric & IBAN_char_test2
                    End Select
                Next IBAN_char2

                'vérifie le modulo 97 (doit être égal à 1)
                Dim IBAN_final = CDec(IBAN_numeric)
                Dim IBAN_modulo = IBAN_final - Fix(IBAN_final / 97) * 97

                ' résultat de la vérification
                If IBAN_modulo = 1 Then
                    bOk = True
                Else
                    bOk = False
                End If

            Catch ex As Exception
                bOk = False
            End Try

            Return bOk

        End Function

        Private Sub NotificationdEmail(watwedo As String, obj As RibIbanVM, obj2 As Spedinaute)

            Dim lienAlerte As String = "<a href=""https://www.myspedidam.fr/Connexion/Usurpation/" + obj.IdSpedinaute.ToString() + """>lien suivant</a>"
            Dim lienCompte As String = "<a href=""https://www.myspedidam.fr/"">Mon Compte Artiste</a>"

            Dim smtpServer As New SmtpClient()
            Dim mail As New MailMessage()

            smtpServer.Credentials = New System.Net.NetworkCredential("assistance@myspedidam.fr", "BYNIACE01To&19")
            smtpServer.Port = 25
            smtpServer.Host = "mail.myspedidam.fr"

            mail = New MailMessage()
            mail.IsBodyHtml = True
            mail.BodyEncoding = System.Text.Encoding.UTF8
            mail.From = New MailAddress("assistance@myspedidam.fr")

            mail.To.Add(obj2.AdresseEmail)
            mail.CC.Add("assistance@spedidam.fr")
            mail.Bcc.Add("moustapha.thiam@spedidam.fr,xavier.lehir@spedidam.fr")

            mail.Subject = "Notification du " + DateTime.Today.ToShortDateString() + "."

            mail.Body = "<P>Bonjour "
            If Not obj2.Prenom Is Nothing Then
                mail.Body += obj2.Prenom.Trim() + "  "
            End If
            mail.Body += obj2.Nom.Trim().ToUpper()
            mail.Body += "<br /> <br /> Suite à votre session sur MySpedidam.fr, vous avez effectué les modifications suivantes : <br /><br/>"
            mail.Body += watwedo + "<br></br>" + "<br></br>" + "Si vous n'êtes pas à l'origine de cette modification, avertissez nous en cliquant sur le "
            mail.Body += (lienAlerte & Convert.ToString(".")) + "<br></br>"
            mail.Body += "Cordialement .</P>"

            smtpServer.Send(mail)

        End Sub

    End Class

End Namespace
