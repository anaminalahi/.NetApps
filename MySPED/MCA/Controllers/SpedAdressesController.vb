Imports System.Web.Mvc
Imports System.Data.Entity
Imports System.Net.Mail
Imports DB

Namespace Controllers


    Public Class SpedAdressesController
        Inherits Controller

#Region "PROPRIETES"

        Private MyManager As DB.Manager

        Private bAddPos, bAddFis As Boolean
        Private Property MonAdresse As MesAdressesVM
        Private Property MonAdresseFis As SpedAdresse

        Public Property SelectedSpedAdresse As SpedAdresse
        Public Property SelectedSpedAdresseFis As SpedAdresse
        Public Property SelectedSpedAdresseVM As SpedAdresseVM
        Public Property SelectedSpedAdresseVMFis As SpedAdresseVM

#End Region

        ' GET: SpedAdresses
        <HttpGet>
        Function Index() As ActionResult

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

            MonAdresse = New MesAdressesVM
            MonAdresse.TitreDeLaPageEnCours = "Adresse Postale / Fiscale"

            bAddPos = False
            SelectedSpedAdresse = MyManager.GetTMonAdresseVraie(MyManager.IdSpedinaute, True)

            If SelectedSpedAdresse Is Nothing Then

                bAddPos = True
                SelectedSpedAdresse = New SpedAdresse
                SelectedSpedAdresse.IdSpedinaute = MyManager.IdSpedinaute
                SelectedSpedAdresseVM = AutoMapper.Mapper.Map(Of SpedAdresseVM)(SelectedSpedAdresse)

                MonAdresse.Postale = SelectedSpedAdresseVM

                MyManager.SpedAdresseActuel = SelectedSpedAdresse
                MyManager.SpedAdresseEdite = SelectedSpedAdresseVM

            Else

                SelectedSpedAdresseVM = AutoMapper.Mapper.Map(Of SpedAdresseVM)(SelectedSpedAdresse)

                If Not String.IsNullOrEmpty(SelectedSpedAdresseVM.TypeAdresse) Then
                    SelectedSpedAdresseVM.MonTypeDAdresse = MyManager.GetTypeAdresse(SelectedSpedAdresseVM.TypeAdresse.Trim)
                End If
                If Not String.IsNullOrEmpty(SelectedSpedAdresseVM.TypeDeVoie) Then
                    SelectedSpedAdresseVM.MonTypeDeVoie = MyManager.GetTypeVoie(SelectedSpedAdresseVM.TypeDeVoie.Trim)
                End If
                If Not String.IsNullOrEmpty(SelectedSpedAdresseVM.Id_pays) Then
                    SelectedSpedAdresseVM.Id_pays = SelectedSpedAdresseVM.Id_pays.Trim
                    SelectedSpedAdresseVM.MonPays = MyManager.GetNationalite(SelectedSpedAdresseVM.Id_pays.Trim)
                End If

                MyManager.SpedAdresseActuel = SelectedSpedAdresse
                MyManager.SpedAdresseEdite = SelectedSpedAdresseVM

                MonAdresse.Postale = SelectedSpedAdresseVM

            End If

            bAddFis = False
            SelectedSpedAdresseFis = MyManager.GetTMonAdresseVraie(MyManager.IdSpedinaute, False)
            If SelectedSpedAdresseFis Is Nothing Then

                bAddFis = True
                SelectedSpedAdresseFis = New SpedAdresse
                SelectedSpedAdresseFis.IdSpedinaute = MyManager.IdSpedinaute

                SelectedSpedAdresseVMFis = AutoMapper.Mapper.Map(Of SpedAdresseVM)(SelectedSpedAdresseFis)

                MonAdresse.Fiscale = SelectedSpedAdresseVMFis

                MyManager.SpedAdresseActuelFis = SelectedSpedAdresseFis
                MyManager.SpedAdresseEditeFis = SelectedSpedAdresseVMFis

            Else

                SelectedSpedAdresseVMFis = AutoMapper.Mapper.Map(Of SpedAdresseVM)(SelectedSpedAdresseFis)

                If Not String.IsNullOrEmpty(SelectedSpedAdresseVMFis.TypeAdresse) Then
                    SelectedSpedAdresseVMFis.MonTypeDAdresse = MyManager.GetTypeAdresse(SelectedSpedAdresseVMFis.TypeAdresse.Trim)
                End If
                If Not String.IsNullOrEmpty(SelectedSpedAdresseVMFis.TypeDeVoie) Then
                    SelectedSpedAdresseVMFis.MonTypeDeVoie = MyManager.GetTypeVoie(SelectedSpedAdresseVMFis.TypeDeVoie.Trim)
                End If
                If Not String.IsNullOrEmpty(SelectedSpedAdresseVMFis.Id_pays) Then
                    SelectedSpedAdresseVMFis.Id_pays = SelectedSpedAdresseVMFis.Id_pays.Trim
                    SelectedSpedAdresseVMFis.MonPays = MyManager.GetNationalite(SelectedSpedAdresseVMFis.Id_pays.Trim)
                End If

                MyManager.SpedAdresseActuelFis = SelectedSpedAdresseFis
                MyManager.SpedAdresseEditeFis = SelectedSpedAdresseVMFis

                MonAdresse.Fiscale = SelectedSpedAdresseVMFis

            End If

            If MonAdresse.Postale Is Nothing Then
                MonAdresse.Postale = New SpedAdresseVM
                MonAdresse.Postale.IdSpedinaute = MyManager.IdSpedinaute
            End If

            If MonAdresse.Fiscale Is Nothing Then
                MonAdresse.Fiscale = New SpedAdresseVM
                MonAdresse.Fiscale.IdSpedinaute = MyManager.IdSpedinaute
            End If

            Return View(MonAdresse)

        End Function

        <HttpGet>
        Function ChangerDAdresse() As ActionResult

            ' Soit prévient le Spedinaute que la session est finie
            If Session Is Nothing Then
                Return RedirectToAction("Index", "SessionExpiree")
            End If

            '   Récupere Tpujours le Manager courant
            MyManager = System.Web.HttpContext.Current.Session("User")
            If MyManager Is Nothing Then Return RedirectToAction("Index", "Connexion")

            ViewBag.ErrMessage = MyManager.ValeursErreurs

            If MyManager.SpedAdresseEdite Is Nothing Then
                SelectedSpedAdresseVM = New SpedAdresseVM
                SelectedSpedAdresseVM.IdSpedinaute = MyManager.IdSpedinaute
            Else
                SelectedSpedAdresseVM = MyManager.SpedAdresseEdite
            End If

            SelectedSpedAdresseVM.TypeAdresse = "POS"
            SelectedSpedAdresseVM.TitreDeLaPageEnCours = "Adresse Postale - Changer"

            Return View(Me.SelectedSpedAdresseVM)

        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Function ChangerDAdresse(ficheEditee As SpedAdresseVM) As ActionResult

            ' Soit prévient le Spedinaute que la session est finie
            If Session Is Nothing Then
                Return RedirectToAction("Index", "SessionExpiree")
            End If

            '   Récupere Tpujours le Manager courant
            MyManager = System.Web.HttpContext.Current.Session("User")
            If MyManager Is Nothing Then Return RedirectToAction("Index", "Connexion")

            '   Controle des saisies
            If String.IsNullOrEmpty(ficheEditee.LibelleVoie) Then
                MyManager.SpedAdresseEdite = ficheEditee
                MyManager.ValeursErreurs = "[ veuillez saisir l 'adresse svp ]"
                Return RedirectToAction("ChangerDAdresse", "SpedAdresses")
            End If

            If String.IsNullOrEmpty(ficheEditee.CodePostal) Then
                MyManager.SpedAdresseEdite = ficheEditee
                MyManager.ValeursErreurs = "[ veuillez saisir le [ Code Postal ] svp ]"
                Return RedirectToAction("ChangerDAdresse", "SpedAdresses")
            End If

            If String.IsNullOrEmpty(ficheEditee.Ville) Then
                MyManager.SpedAdresseEdite = ficheEditee
                MyManager.ValeursErreurs = "[ veuillez saisir la [ Ville ] svp ]"
                Return RedirectToAction("ChangerDAdresse", "SpedAdresses")
            End If

            If Not ficheEditee.Id_pays Is Nothing AndAlso ficheEditee.Id_pays.Trim.Equals(String.Empty) Then
                MyManager.SpedAdresseEdite = ficheEditee
                MyManager.ValeursErreurs = "[ veuillez choisir le [ Pays ] svp ]"
                Return RedirectToAction("ChangerDAdresse", "SpedAdresses")
            End If

            Dim ControleOK = True
            '   a valider avce google maps
            If Not ControleOK Then

                '  Charge la fiche Rib
                MyManager.SpedAdresseEdite = ficheEditee
                MyManager.ValeursErreurs = "[ Votre Adresse n'est pas valide. Reprenez svp ]"
                Return RedirectToAction("ChangerDAdresse", "SpedAdresses")

            Else

                '   ComparaisonPourHistorisation
                Dim OnaFaitQuoi As [String] = [String].Empty

                OnaFaitQuoi = OnaFaitQuoi + "<P>Vous avez changé votre adresse postale. Vous etes domicilié au :</p><br></br>"

                OnaFaitQuoi += "<ul>"
                OnaFaitQuoi += "<li>" + " ADRESSE     : " + ficheEditee.LibelleVoie + "</li>"
                OnaFaitQuoi += "<li>" + " COMPLEMENT  : " + ficheEditee.Adresse2 + "</li>"
                OnaFaitQuoi += "<li>" + " CODE POSTAL : " + ficheEditee.CodePostal + "</li>"
                OnaFaitQuoi += "<li>" + " VILLE       : " + ficheEditee.Ville + "</li>"
                OnaFaitQuoi += "<li>" + " PAYS : " + MyManager.GetNationalite(ficheEditee.Id_pays.Trim) + "</li>"
                OnaFaitQuoi += "</ul><br></br>"

                '   Archivage du L'Adresse Existante
                If Not MyManager.SpedAdresseActuel Is Nothing AndAlso MyManager.SpedAdresseActuel.IdAdresse > 0 Then

                    MyManager.SpedAdresseActuel.Actuelle = False
                    MyManager.DbContext.Entry(MyManager.SpedAdresseActuel).State = EntityState.Modified
                    MyManager.DbContext.SaveChanges()

                End If

                '   cree la nouvelle adresse
                Dim NouvelleAdresse = New SpedAdresse

                NouvelleAdresse.TypeAdresse = "POS"
                NouvelleAdresse.LibelleVoie = ficheEditee.LibelleVoie
                NouvelleAdresse.Adresse2 = ficheEditee.Adresse2
                NouvelleAdresse.Ville = ficheEditee.Ville
                NouvelleAdresse.CodePostal = ficheEditee.CodePostal
                NouvelleAdresse.Id_pays = ficheEditee.Id_pays
                NouvelleAdresse.DateSaisie = Today
                NouvelleAdresse.IdSpedinaute = MyManager.IdSpedinaute
                NouvelleAdresse.IntegreDansBachON = True
                NouvelleAdresse.Actuelle = True

                MyManager.DbContext.SpedAdresses.Add(NouvelleAdresse)
                MyManager.DbContext.SaveChanges()

                '   Chargement de la Nouvelle Adresse Postale
                SelectedSpedAdresse = MyManager.GetTMonAdresseVraie(MyManager.IdSpedinaute, True)
                SelectedSpedAdresseVM = AutoMapper.Mapper.Map(Of SpedAdresseVM)(SelectedSpedAdresse)

                '   Mise a jour du Shared MyManager
                MyManager.SpedAdresseActuel = SelectedSpedAdresse
                MyManager.SpedAdresseEdite = SelectedSpedAdresseVM
                MyManager.ValeursErreurs = String.Empty

                Dim UneTache = New TacheEffectuee()

                UneTache.TypeDeTache = "M"
                UneTache.DateDuJour = DateTime.Today.ToShortDateString()
                UneTache.HeureCourante = DateTime.Today.ToShortTimeString()

                UneTache.IdAdresseConcernee = "O"
                UneTache.IdModePasseConcerne = "N"
                UneTache.IdRibIbanConcerne = "N"
                UneTache.FicheConcernee = "N"

                UneTache.Commentaire = "NOUVELLE ADRESSE POSTALE CREE LE " + DateTime.Today.ToShortDateString()
                UneTache.HistoriserON = True

                UneTache.IdSpedinaute = MyManager.IdSpedinaute

                MyManager.DbContext.TacheEffectuees.Add(UneTache)
                MyManager.DbContext.SaveChanges()

                NotificationdEmail(OnaFaitQuoi, ficheEditee, MyManager.MaFiche)

                '   MISE A JOUR DE LA FICHE DANS GESPERE
                If MyManager.DbCtx IsNot Nothing Then

                    MyManager.bDone = True

                    '   desactivation de l'adresse
                    If Not MyManager.SelectedAdresGespere Is Nothing Then

                        MyManager.SelectedAdresGespere.etat = "SAN"
                        MyManager.SelectedAdresGespere.dt_fin = Today

                        MyManager.DbCtx.Entry(MyManager.SelectedAdresGespere).State = EntityState.Modified
                        MyManager.DbCtx.SaveChanges()

                    End If

                    '   CONSTRUCTION D'UNE ADRESSE 
                    Dim uneAdresse = New Adresse
                    uneAdresse.id_adres = MyManager.MaFiche.NumeroIdAyant.Trim() + "01"
                    uneAdresse.id_adres = (StrDup((16 - uneAdresse.id_adres.Length), "0")) + uneAdresse.id_adres
                    uneAdresse.id_entit = "BENEF"
                    uneAdresse.id_type = "POS"
                    uneAdresse.lb_voie = MyManager.SpedAdresseActuel.LibelleVoie
                    uneAdresse.adr2 = MyManager.SpedAdresseActuel.Adresse2
                    uneAdresse.codposta = MyManager.SpedAdresseActuel.CodePostal
                    uneAdresse.ville = MyManager.SpedAdresseActuel.Ville
                    uneAdresse.id_pays = MyManager.SpedAdresseActuel.Id_pays
                    uneAdresse.id_ayant = MyManager.MaFiche.NumeroIdAyant.Trim()
                    uneAdresse.no_benef = "01"

                    MyManager.DbCtx.Adresses.Add(uneAdresse)
                    MyManager.DbCtx.SaveChanges()


                    If Not MyManager.SelectedAyantDroit Is Nothing Then

                        MyManager.SelectedAyantDroit.dt_maj = DateTime.Today
                        MyManager.SelectedAyantDroit.notes = MyManager.SelectedAyantDroit.notes + Chr(13) + DateTime.Today.ToShortDateString().Replace("/", "-") + " : NOUVELLE ADRESSE POSTALE CREE LE " + DateTime.Today.ToShortDateString()
                        MyManager.DbCtx.Entry(MyManager.SelectedAyantDroit).State = EntityState.Modified
                        MyManager.DbCtx.SaveChanges()

                    End If

                    Dim UneTacheB = New TacheHistorisee()

                    UneTacheB.TypeDeTache = "M"
                    UneTacheB.DateDuJour = DateTime.Today.ToShortDateString()
                    UneTacheB.HeureCourante = DateTime.Now.TimeOfDay

                    UneTacheB.IdAdresseConcernee = "O"
                    UneTacheB.IdModePasseConcerne = "N"
                    UneTacheB.IdRibIbanConcerne = "N"
                    UneTacheB.FicheConcernee = "N"

                    UneTacheB.Commentaire = "NOUVELLE ADRESSE POSTALE CREE LE " + DateTime.Today.ToShortDateString()
                    UneTacheB.HistoriserON = True

                    UneTacheB.Id_ayant = MyManager.MaFiche.NumeroIdAyant.Trim()
                    UneTache.IdSpedinaute = MyManager.MaFiche.IdSpedinaute
                    UneTacheB.HistoriserON = True

                    MyManager.DbCtx.TacheHistorisees.Add(UneTacheB)
                    MyManager.DbCtx.SaveChanges()

                End If

            End If

            '  Charge
            Return RedirectToAction("Index", "SpedAdresses")

        End Function

        <HttpGet>
        Function ChangerDAdresseFiscale() As ActionResult

            ' Soit prévient le Spedinaute que la session est finie
            If Session Is Nothing Then
                Return RedirectToAction("Index", "SessionExpiree")
            End If

            '   Récupere Tpujours le Manager courant
            MyManager = System.Web.HttpContext.Current.Session("User")
            If MyManager Is Nothing Then Return RedirectToAction("Index", "Connexion")

            ViewBag.ErrMessage = MyManager.ValeursErreurs

            If MyManager.SpedAdresseEditeFis Is Nothing Then
                SelectedSpedAdresseVMFis = New SpedAdresseVM
                SelectedSpedAdresseVMFis.IdSpedinaute = MyManager.IdSpedinaute
            Else
                SelectedSpedAdresseVMFis = MyManager.SpedAdresseEditeFis
                SelectedSpedAdresseVMFis.IdSpedinaute = MyManager.IdSpedinaute
            End If

            SelectedSpedAdresseVMFis.TypeAdresse = "FIS"
            SelectedSpedAdresseVMFis.TitreDeLaPageEnCours = "Adresse Fiscale - Changer d'Adresse"

            Return View(Me.SelectedSpedAdresseVMFis)

        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Function ChangerDAdresseFiscale(ficheEditee As SpedAdresseVM) As ActionResult

            ' Soit prévient le Spedinaute que la session est finie
            If Session Is Nothing Then
                Return RedirectToAction("Index", "SessionExpiree")
            End If

            '   Récupere Tpujours le Manager courant
            MyManager = System.Web.HttpContext.Current.Session("User")
            If MyManager Is Nothing Then Return RedirectToAction("Index", "Connexion")

            '   Controle des saisies

            If String.IsNullOrEmpty(ficheEditee.LibelleVoie) Then
                MyManager.SpedAdresseEditeFis = ficheEditee
                MyManager.ValeursErreurs = "[ veuillez saisir l 'adresse svp ]"
                Return RedirectToAction("ChangerDAdresseFiscale", "SpedAdresses")
            End If

            If String.IsNullOrEmpty(ficheEditee.CodePostal) Then
                MyManager.SpedAdresseEditeFis = ficheEditee
                MyManager.ValeursErreurs = "[ veuillez saisir le [ Code Postal ] svp ]"
                Return RedirectToAction("ChangerDAdresseFiscale", "SpedAdresses")
            End If

            If String.IsNullOrEmpty(ficheEditee.Ville) Then
                MyManager.SpedAdresseEditeFis = ficheEditee
                MyManager.ValeursErreurs = "[ veuillez saisir la [ Ville ] svp ]"
                Return RedirectToAction("ChangerDAdresseFiscale", "SpedAdresses")
            End If

            If Not ficheEditee.Id_pays Is Nothing AndAlso ficheEditee.Id_pays.Trim.Equals(String.Empty) Then
                MyManager.SpedAdresseEditeFis = ficheEditee
                MyManager.ValeursErreurs = "[ veuillez choisir le [ Pays ] svp ]"
                Return RedirectToAction("ChangerDAdresseFiscale", "SpedAdresses")
            End If

            Dim ControleOK = True

            '   a valider avce google maps
            If Not ControleOK Then

                '  Charge la fiche Adresse
                MyManager.SpedAdresseEditeFis = ficheEditee
                MyManager.ValeursErreurs = "[ Votre Adresse fiscale n'est pas valide. Reprenez svp ]"
                Return RedirectToAction("ChangerDAdresseFiscale", "SpedAdresses")

            Else

                '   ComparaisonPourHistorisation
                Dim OnaFaitQuoi As [String] = [String].Empty

                OnaFaitQuoi = OnaFaitQuoi + "<P>Vous avez changé votre adresse fiscale. Vous etes domicilié au :</p><br></br>"

                OnaFaitQuoi += "<ul>"
                OnaFaitQuoi += "<li>" + " ADRESSE     : " + ficheEditee.LibelleVoie + "</li>"
                OnaFaitQuoi += "<li>" + " COMPLEMENT  : " + ficheEditee.Adresse2 + "</li>"
                OnaFaitQuoi += "<li>" + " CODE POSTAL : " + ficheEditee.CodePostal + "</li>"
                OnaFaitQuoi += "<li>" + " VILLE       : " + ficheEditee.Ville + "</li>"
                OnaFaitQuoi += "<li>" + " PAYS : " + MyManager.GetNationalite(ficheEditee.Id_pays.Trim) + "</li>"
                OnaFaitQuoi += "</ul><br></br>"

                '   Archivage du L'Adresse fiscale Existante
                If Not MyManager.SpedAdresseActuelFis Is Nothing AndAlso MyManager.SpedAdresseActuelFis.IdAdresse > 0 Then

                    MyManager.SpedAdresseActuelFis.Actuelle = False
                    MyManager.DbContext.Entry(MyManager.SpedAdresseActuelFis).State = EntityState.Modified
                    MyManager.DbContext.SaveChanges()

                End If

                Dim NouvelleAdresse = New SpedAdresse

                NouvelleAdresse.TypeAdresse = "FIS"
                NouvelleAdresse.LibelleVoie = ficheEditee.LibelleVoie
                NouvelleAdresse.Adresse2 = ficheEditee.Adresse2
                NouvelleAdresse.Ville = ficheEditee.Ville
                NouvelleAdresse.CodePostal = ficheEditee.CodePostal
                NouvelleAdresse.Id_pays = ficheEditee.Id_pays
                NouvelleAdresse.DateSaisie = Today
                NouvelleAdresse.IdSpedinaute = MyManager.IdSpedinaute
                NouvelleAdresse.IntegreDansBachON = True
                NouvelleAdresse.Actuelle = True

                MyManager.DbContext.SpedAdresses.Add(NouvelleAdresse)
                MyManager.DbContext.SaveChanges()

                '   Chargement de la Nouvelle Adresse Postale
                SelectedSpedAdresseFis = MyManager.GetTMonAdresseVraie(MyManager.IdSpedinaute, False)
                SelectedSpedAdresseVMFis = AutoMapper.Mapper.Map(Of SpedAdresseVM)(SelectedSpedAdresseFis)

                '   Mise a jour du Shared MyManager
                MyManager.SpedAdresseActuelFis = SelectedSpedAdresseFis
                MyManager.SpedAdresseEditeFis = SelectedSpedAdresseVMFis
                MyManager.ValeursErreurs = String.Empty

                Dim UneTache = New TacheEffectuee()

                UneTache.TypeDeTache = "M"
                UneTache.DateDuJour = DateTime.Today.ToShortDateString()
                UneTache.HeureCourante = DateTime.Today.ToShortTimeString()

                UneTache.IdAdresseConcernee = "O"
                UneTache.IdModePasseConcerne = "N"
                UneTache.IdRibIbanConcerne = "N"
                UneTache.FicheConcernee = "N"

                UneTache.Commentaire = "NOUVELLE ADRESSE FISCALE CREE LE " + DateTime.Today.ToShortDateString()
                UneTache.HistoriserON = True

                UneTache.IdSpedinaute = MyManager.IdSpedinaute

                MyManager.DbContext.TacheEffectuees.Add(UneTache)
                MyManager.DbContext.SaveChanges()

                NotificationdEmail(OnaFaitQuoi, ficheEditee, MyManager.MaFiche)

                '   MISE A JOUR DE LA FICHE DANS GESPERE
                If MyManager.DbCtx IsNot Nothing Then

                    MyManager.bDone = True

                    '   desactivatrion de l'adresse
                    If Not MyManager.SelectedAdresGespereFis Is Nothing Then

                        MyManager.SelectedAdresGespereFis.etat = "SAN"
                        MyManager.SelectedAdresGespereFis.dt_fin = Today

                        MyManager.DbCtx.Entry(MyManager.SelectedAdresGespereFis).State = EntityState.Modified
                        MyManager.DbCtx.SaveChanges()

                    End If

                    '   CONSTRUCTION D'UNE ADRESSE 
                    Dim uneAdresse = New Adresse
                    uneAdresse.id_adres = MyManager.MaFiche.NumeroIdAyant.Trim() + "01"
                    uneAdresse.id_adres = (StrDup((16 - uneAdresse.id_adres.Length), "0")) + uneAdresse.id_adres
                    uneAdresse.id_entit = "BENEF"
                    uneAdresse.id_type = "FIS"
                    uneAdresse.lb_voie = MyManager.SpedAdresseActuelFis.LibelleVoie
                    uneAdresse.adr2 = MyManager.SpedAdresseActuelFis.Adresse2
                    uneAdresse.codposta = MyManager.SpedAdresseActuelFis.CodePostal
                    uneAdresse.ville = MyManager.SpedAdresseActuelFis.Ville
                    uneAdresse.id_pays = MyManager.SpedAdresseActuelFis.Id_pays
                    uneAdresse.id_ayant = MyManager.MaFiche.NumeroIdAyant.Trim()
                    uneAdresse.no_benef = "01"

                    MyManager.DbCtx.Adresses.Add(uneAdresse)
                    MyManager.DbCtx.SaveChanges()


                    If Not MyManager.SelectedAyantDroit Is Nothing Then

                        MyManager.SelectedAyantDroit.dt_maj = DateTime.Today

                        MyManager.SelectedAyantDroit.notes = MyManager.SelectedAyantDroit.notes + Chr(13) + DateTime.Today.ToShortDateString().Replace("/", "-") + " : NOUVELLE ADRESSE FISCALE CREE LE " + DateTime.Today.ToShortDateString()

                        MyManager.DbCtx.Entry(MyManager.SelectedAyantDroit).State = EntityState.Modified
                        MyManager.DbCtx.SaveChanges()

                    End If

                    Dim UneTacheB = New TacheHistorisee()

                    UneTacheB.TypeDeTache = "M"
                    UneTacheB.DateDuJour = DateTime.Today.ToShortDateString()
                    UneTacheB.HeureCourante = DateTime.Now.TimeOfDay

                    UneTacheB.IdAdresseConcernee = "O"
                    UneTacheB.IdModePasseConcerne = "N"
                    UneTacheB.IdRibIbanConcerne = "N"
                    UneTacheB.FicheConcernee = "N"

                    UneTacheB.Commentaire = "NOUVELLE ADRESSE FISCALE CREE LE " + DateTime.Today.ToShortDateString()
                    UneTacheB.HistoriserON = True

                    UneTacheB.Id_ayant = MyManager.MaFiche.NumeroIdAyant.Trim()
                    UneTache.IdSpedinaute = MyManager.MaFiche.IdSpedinaute
                    UneTacheB.HistoriserON = True

                    MyManager.DbCtx.TacheHistorisees.Add(UneTacheB)
                    MyManager.DbCtx.SaveChanges()

                End If

            End If

            '  Charge
            Return RedirectToAction("Index", "SpedAdresses")

        End Function

        Private Sub NotificationdEmail(watwedo As String, obj As SpedAdresseVM, obj2 As Spedinaute)

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