Imports System.Data.Entity
Imports System.IO
Imports System.Net.Mail
Imports DB

Namespace Controllers
    Public Class ConnexionController
        Inherits Controller

#Region "PROPRIETES"

        Private MyManager As DB.Manager

        Public Function CloreApplication() As ActionResult

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

            Return Redirect("https://myspedidam.fr")

        End Function

        Public Function EmailEnvoye(ByVal objFiche As Spedinaute) As ActionResult

            Return View(objFiche)

        End Function

#End Region

#Region "ACTIONS_INDEX"

        <HandleError>
        <HttpGet>
        Public Function Index() As ActionResult
            Return View()
        End Function

        <HandleError>
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Function Index(AdresseEmail As String, PasseMot As String, motDePasseOublie As Boolean, seSouvenirDeMoi As Boolean) As ActionResult

            Try

                If MyManager Is Nothing Then

                    MyManager = New Manager
                    MyManager.SeConnecterAFacilDB()
                    MyManager.SeConnecterAHarmonie()

                    MvcApplication.ListeDesIntruments = MyManager.ListeDesIntruments
                    MvcApplication.ListeDesNationalites = MyManager.ListeDesNationalites
                    MvcApplication.ListeDesTypeDeVoies = MyManager.ListeDesTypeDeVoies
                    MvcApplication.ListeDesTypesAdresses = MyManager.ListeDesTypesAdresses
                    MvcApplication.ListeDesAnneesFiscales = MyManager.ListeDesAnneesFiscales

                End If

                If motDePasseOublie Then
                    Return RedirectToAction("RenvoiOuCreationDeMotDePasse")
                End If

                If String.IsNullOrEmpty(AdresseEmail) Then
                    ViewBag.ErrMessage = "[ Veuillez saisir l'Adresse Email svp ]"
                    Return View()
                End If

                If Not MyManager.IsValideEmail(AdresseEmail) Then
                    ViewBag.ErrMessage = "[ veuillez saisir une Adresse Email valide svp ]"
                    Return View()
                End If

                If String.IsNullOrEmpty(PasseMot) Then
                    ViewBag.ErrMessage = "[ Veuillez saisir le mot de passe svp ]"
                    Return View()
                End If

                If Not MyManager.IsStrongPWD(PasseMot, False) Then
                    ViewBag.ErrMessage = "[ Le mot de passe doit contenir au moins 8 caractères dont au moins une Masjuscule, un chiffre et un Caractère spécial. Reprenez svp ]"
                    Return View()
                End If

                If MyManager.SeConnecter(AdresseEmail, PasseMot) Then

                    '   Trouvé et Authentifié
                    If seSouvenirDeMoi Then
                        FormsAuthentication.SetAuthCookie(MyManager.MaFiche.AdresseEmail, seSouvenirDeMoi)
                    End If

                    Session("isAuth") = True
                    '   Charge les Shared Data
                    Session("user") = MyManager

                    '   Charge la fiche Artiste
                    Return RedirectToAction("Index", "Spedinaute")

                Else

                    Return RedirectToAction("PasTrouve")

                End If

                Return View()

            Catch ex As Exception
                Return RedirectToAction("PasDaccesDB")
            End Try

        End Function

#End Region

#Region "ACTION_UNIQUES"

        Public Function PasDaccesDB() As ActionResult
            Return View()
        End Function

        Public Function PasTrouve() As ActionResult
            Return View()
        End Function

#End Region

#Region "ACTION_RENVOIOUCREATION"

        <HandleError>
        <HttpGet>
        Public Function RenvoiOuCreationDeMotDePasse() As ActionResult
            Return View()
        End Function

        <HandleError>
        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Function RenvoiOuCreationDeMotDePasse(Prenom As String, Nom As String, AdresseEmail As String, RenvoiOuCreation As Boolean, PasseMot As String, PasseMotConfirme As String) As ActionResult

            If MyManager Is Nothing Then

                MyManager = New Manager
                MyManager.SeConnecterAFacilDB()
                MyManager.SeConnecterAHarmonie()

                If Not MyManager.ControlerAccesDB Then
                    Return RedirectToAction("PasDaccesDB")
                End If

            End If

            '   Controle des saisies
            If [String].IsNullOrEmpty(Prenom) Then
                ViewBag.ErrMessage = "[ veuillez saisir le(s) Prénom(s) svp ]"
                Return View()
            End If

            If [String].IsNullOrEmpty(Nom) Then
                ViewBag.ErrMessage = "[ Veuillez saisir votre nom svp ]"
                Return View()
            End If

            If [String].IsNullOrEmpty(AdresseEmail) Then
                ViewBag.ErrMessage = "[ veuillez saisir l'Adresse Email svp ]"
                Return View()
            End If

            If Not MyManager.IsValideEmail(AdresseEmail) Then
                ViewBag.ErrMessage = "[ veuillez saisir une Adresse Email valide svp ]"
                Return View()
            End If

            If Not String.IsNullOrEmpty(PasseMot) Then
                If PasseMot.Length > 10 Then
                    ViewBag.ErrMessage = "[ Le mot de passe ne peut pas dépasser dix (10) caractères. Reprenez svp ]"
                    Return View()
                End If
            End If

            '   Si Creation du mot de passe - Controle des Mots de Passe
            If Not RenvoiOuCreation Then

                If Not MyManager.IsStrongPWD(PasseMot, True) Then
                    ViewBag.ErrMessage = "[ Le mot de passe doit contenir au moins 8 caractères dont au moins une Masjuscule, un chiffre et un Caractère spécial. Reprenez svp ]"
                    Return View()
                End If

                If Not MyManager.IsStrongPWD(PasseMotConfirme, True) Then
                    ViewBag.ErrMessage = "[ Le mot de passe doit contenir au moins 8 caractères dont au moins une Masjuscule, un chiffre et un Caractère spécial. Reprenez svp ]"
                    Return View()
                End If

                If Not PasseMot.Equals(PasseMotConfirme) Then
                    ViewBag.ErrMessage = "[ Le mot de passe et sa confirmation doivent être identiques. Reprenez svp ]"
                    Return View()
                End If

            End If

            '   Controle du Captcha
            Prenom = Prenom.Trim().ToUpper()
            Nom = Nom.Trim().ToUpper()

            '  CHERCHE LE SPEDINAUTE
            If MyManager.DbContext IsNot Nothing AndAlso Not MyManager.DbContext.Spedinautes Is Nothing Then

                Try

                    MyManager.MaFiche = MyManager.DbContext.Spedinautes.FirstOrDefault(Function(s) (s.Nom.Trim().ToUpper() = Nom) AndAlso (s.AdresseEmail.Trim() = AdresseEmail.Trim()))

                    If Not MyManager.MaFiche Is Nothing Then

                        '  RENVOI D'EMAIL
                        If RenvoiOuCreation Then

                            If String.IsNullOrEmpty(MyManager.MaFiche.PasseMot.NulleOuNon) Then
                                ViewBag.ErrMessage = "[ Vous n'avez pas de mot de passe. Veuillez en créer un. Reprenez svp ]"
                                Return View()
                            End If

                            MyManager.AdresseDeCourriel = AdresseEmail

                            '  RENVOI DU MOT DE PASSE PAR EMAIL
                            MyManager.RenvoidEmail(MyManager.MaFiche)
                            Return RedirectToAction("EmailEnvoye", MyManager.MaFiche)

                        Else

                            '  ENVOI D'EMAIL ET SAUVEGARDE DU NOUVEAU MOT DE PASSE
                            MyManager.MaFiche.PasseMot = PasseMot

                            MyManager.DbContext.Entry(MyManager.MaFiche).State = EntityState.Modified
                            MyManager.DbContext.SaveChanges()

                            Dim nouveauPwd = New MotDePasse()

                            nouveauPwd.IdSpedinaute = MyManager.MaFiche.IdSpedinaute
                            nouveauPwd.IntegreDansBachON = False
                            nouveauPwd.DateDeCreation = DateTime.Today
                            nouveauPwd.Actuel = True
                            nouveauPwd.PasseMot = PasseMot
                            nouveauPwd.DateDeRenvoi = DateTime.Today

                            MyManager.DbContext.MotDePasses.Add(nouveauPwd)
                            MyManager.DbContext.SaveChanges()

                            '  HISTORISATION WEB DE LA TACHE
                            Dim UneTache = New TacheEffectuee()

                            UneTache.TypeDeTache = "M"
                            UneTache.DateDuJour = DateTime.Today.ToShortDateString()
                            UneTache.HeureCourante = DateTime.Today.ToShortTimeString()

                            UneTache.IdAdresseConcernee = "N"
                            UneTache.IdModePasseConcerne = "O"
                            UneTache.IdRibIbanConcerne = "N"
                            UneTache.FicheConcernee = "N"

                            UneTache.NomDuDevice = ""
                            UneTache.AdresseMac = ""
                            UneTache.AdresseIP = ""

                            UneTache.Commentaire = "NOUVEAU MOT DE PASSE CREE LE " + DateTime.Today.ToShortDateString()
                            UneTache.HistoriserON = True

                            UneTache.IdSpedinaute = MyManager.MaFiche.IdSpedinaute

                            MyManager.DbContext.TacheEffectuees.Add(UneTache)
                            MyManager.DbContext.SaveChanges()

                            '  ENVOI DU NOUVEAU MOT DE PASSE PAR EMAIL
                            MyManager.EnvoidEmail(MyManager.MaFiche)

                            '   MISE A JOUR DE LA FICHE DANS GESPERE
                            If MyManager.DbCtx IsNot Nothing Then
                                '  HISTORISATION DE LA TACHE DANS LA BASE GESPERE
                                Dim UneTacheB = New TacheHistorisee()

                                UneTacheB.TypeDeTache = "M"
                                UneTacheB.DateDuJour = DateTime.Today.ToShortDateString()
                                UneTacheB.HeureCourante = DateTime.Now.TimeOfDay

                                UneTacheB.IdAdresseConcernee = "N"
                                UneTacheB.IdModePasseConcerne = "O"
                                UneTacheB.IdRibIbanConcerne = "N"
                                UneTacheB.FicheConcernee = "N"

                                UneTacheB.Commentaire = "NOUVEAU MOT DE PASSE CREE LE " + DateTime.Today.ToShortDateString()
                                UneTacheB.HistoriserON = True

                                UneTacheB.IdSpedinaute = MyManager.MaFiche.IdSpedinaute
                                UneTacheB.Id_ayant = MyManager.MaFiche.NumeroIdAyant

                                MyManager.DbCtx.TacheHistorisees.Add(UneTacheB)
                                MyManager.DbCtx.SaveChanges()

                                MyManager.bDone = True

                                If MyManager.DbCtx.AyantDro.Count() > 0 Then

                                    MyManager.SelectedAyantDroit = MyManager.DbCtx.AyantDro.FirstOrDefault(Function(s) (s.id_ayant.Trim() = MyManager.MaFiche.NumeroIdAyant))

                                    If Not MyManager.SelectedAyantDroit Is Nothing Then

                                        MyManager.SelectedAyantDroit.dt_maj = DateTime.Today

                                        MyManager.SelectedAyantDroit.notes = MyManager.SelectedAyantDroit.notes + Chr(13) + DateTime.Today.ToShortDateString().Replace("/", "-") + " : NOUVEAU MOT DE PASSE CREE LE " + DateTime.Today.ToShortDateString()
                                        MyManager.SelectedAyantDroit.password = MyManager.MaFiche.PasseMot.Trim()

                                        MyManager.DbCtx.Entry(MyManager.SelectedAyantDroit).State = EntityState.Modified
                                        MyManager.DbCtx.SaveChanges()

                                    End If

                                End If

                            End If

                            Return RedirectToAction("EmailEnvoye", MyManager.MaFiche)

                            '  FIN DU ELSE
                        End If
                    Else
                        Return RedirectToAction("PasTrouve")
                    End If

                Catch ex As Exception

                    RenvoiParMailDesErreurs("fn Connexion" + ex.Message)

                    If MyManager.bDone Then
                        '  ENVOI DU NOUVEAU MOT DE PASSE PAR EMAIL
                        Return RedirectToAction("EmailEnvoye")
                    Else
                        '  Va au menu de Connexion
                        Return RedirectToAction("Index", "Connexion")
                    End If

                End Try
            End If

            Return View()

        End Function

        Public Function Usurpation(id As Integer) As ActionResult

            '  Mise a jour du compte

            MyManager = New DB.Manager

            If Not MyManager Is Nothing Then

                MyManager.SeConnecterAFacilDB()

                MyManager.MaFiche = MyManager.DbContext.Spedinautes.FirstOrDefault(Function(s) s.IdSpedinaute = id)

                If Not MyManager.MaFiche Is Nothing Then

                    MyManager.MaFiche.CompteUsurpe = True

                    MyManager.DbContext.Entry(MyManager.MaFiche).State = EntityState.Modified
                    MyManager.DbContext.SaveChanges()

                    NotificationdEmailUsurpation(MyManager.MaFiche)

                End If

            End If

            Return View()

        End Function

        Private Sub NotificationdEmailUsurpation(obj As Spedinaute)

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

            mail.To.Add(obj.AdresseEmail)
            mail.CC.Add("assistance@myspedidam.fr, assistance@spedidam.fr")
            mail.Bcc.Add("moustapha.thiam@spedidam.fr,xavier.lehir@spedidam.fr,assistance@spedidam.fr")

            mail.Subject = "Notification du " + DateTime.Today.ToShortDateString() + "."

            mail.Body = "<P>Bonjour "
            mail.Body += obj.Prenom.Trim() + "  "
            mail.Body += obj.Nom.Trim().ToUpper()
            mail.Body += "<br /> <br /> Vous avez signalé l'usurpation de votre compte. <br /><br/>"
            mail.Body += "<br></br>" + "<br></br>" + "Nous en prenons acte et nous vous contacterons sous peu."
            mail.Body += "<br></br>"
            mail.Body += "Cordialement .</P>"

            smtpServer.Send(mail)

        End Sub

        Private Sub RenvoiParMailDesErreurs(Logs As String)

            Dim smtpServer As New SmtpClient()
            Dim mail As New MailMessage()

            smtpServer.Credentials = New System.Net.NetworkCredential("assistance@myspedidam.fr", "BYNIACE01To&19")
            smtpServer.Port = 25
            smtpServer.Host = "mail.myspedidam.fr"

            mail = New MailMessage()
            mail.IsBodyHtml = True
            mail.BodyEncoding = System.Text.Encoding.UTF8
            mail.From = New MailAddress("assistance@myspedidam.fr")
            mail.[To].Add("moustapha.thiam@spedidam.fr")
            mail.Subject = "Une Erreur est survenue le " + Today.ToShortDateString + " à " + Today.ToShortTimeString

            mail.Body = "<P>Logs Erreurs"
            mail.Body += "<br /> <br />"
            mail.Body += Logs + "</P>"

            smtpServer.Send(mail)

        End Sub

#End Region

    End Class

End Namespace