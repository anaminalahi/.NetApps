Imports System.Data.Entity
Imports System.Net.Mail
Imports DB

Namespace Controllers

    Public Class SpedinauteController
        Inherits Controller

#Region "PROPRIETES"

        Private MyManager As DB.Manager

        Private bCivil, bPrenom, bPrenom2, bNom, bNomJF, bDate, bLieu, bNation, bTelMobile, bTelFixe, bEmail, bMusic1, bMusic2, bMusic3, bPseudo1, BPseudo2, bPseudo3, bMotDePasse As Boolean

        Public Property SelectedSpedinaute As Spedinaute
        Public Property SelectedSpedinauteVM As SpedinauteVM
        Public Property OnAFaitQuoi As [String]

        Private AncienPWD As String

#End Region

#Region "ACTIONS"

        <HttpGet>
        Public Function Index() As ActionResult

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

            If MyManager.MaFicheEditee Is Nothing Then
                SelectedSpedinaute = MyManager.MaFiche
                SelectedSpedinauteVM = AutoMapper.Mapper.Map(Of SpedinauteVM)(SelectedSpedinaute)
                MyManager.MaFicheEditee = SelectedSpedinauteVM
            Else
                SelectedSpedinauteVM = MyManager.MaFicheEditee
            End If

            If Not SelectedSpedinauteVM.Nationalite Is Nothing Then
                SelectedSpedinauteVM.MaNationalite = MyManager.GetNationalite(SelectedSpedinauteVM.Nationalite.Trim())
            End If
            If Not SelectedSpedinauteVM.Instrument1 Is Nothing Then
                SelectedSpedinauteVM.MonInstrument1 = MyManager.GetInstrument(SelectedSpedinauteVM.Instrument1)
            End If
            If Not SelectedSpedinauteVM.Instrument2 Is Nothing Then
                SelectedSpedinauteVM.MonInstrument2 = MyManager.GetInstrument(SelectedSpedinauteVM.Instrument2)
            End If
            If Not SelectedSpedinauteVM.Instrument3 Is Nothing Then
                SelectedSpedinauteVM.MonInstrument3 = MyManager.GetInstrument(SelectedSpedinauteVM.Instrument3)
            End If

            SelectedSpedinauteVM.TitreDeLaPageEnCours = "Fiche d'identité"

            Return View(SelectedSpedinauteVM)

        End Function

        Function AnnulerSaisie() As ActionResult

            ' Soit prévient le Spedinaute que la session est finie
            If Session Is Nothing Then
                Return RedirectToAction("Index", "SessionExpiree")
            End If

            '   Récupere Tpujours le Manager courant
            MyManager = System.Web.HttpContext.Current.Session("User")

            '   Désactivation du Mode Saisie
            MyManager.bMajEnCoursFiche = False
            MyManager.MaFicheEditee = Nothing
            MyManager.ValeursErreurs = String.Empty

            Return RedirectToAction("Index", "Spedinaute")

        End Function

        <HttpGet>
        Public Function Edit() As ActionResult

            ' Soit prévient le Spedinaute que la session est finie
            If Session Is Nothing Then
                Return RedirectToAction("Index", "SessionExpiree")
            End If

            '   Récupere Tpujours le Manager courant
            MyManager = System.Web.HttpContext.Current.Session("User")

            If MyManager Is Nothing Then Return RedirectToAction("Index", "Connexion")

            bCivil = False
            bPrenom = False
            bPrenom2 = False
            bNom = False
            bNomJF = False
            bDate = False
            bLieu = False
            bTelMobile = False
            bTelFixe = False
            bNation = False
            bEmail = False
            bMusic1 = False
            bMusic2 = False
            bMusic3 = False
            bPseudo1 = False
            BPseudo2 = False
            bPseudo3 = False
            bMotDePasse = False

            '   Activation du Mode Saisi
            MyManager.bMajEnCoursFiche = True
            ViewBag.ErrMessage = MyManager.ValeursErreurs

            If Not MyManager.MaFicheEditee Is Nothing Then

                SelectedSpedinauteVM = MyManager.MaFicheEditee

                If Not SelectedSpedinauteVM.Nationalite Is Nothing Then

                    SelectedSpedinauteVM.Nationalite = SelectedSpedinauteVM.Nationalite.Trim
                    SelectedSpedinauteVM.MaNationalite = MyManager.GetNationalite(SelectedSpedinauteVM.Nationalite.Trim)

                End If
                If Not SelectedSpedinauteVM.Instrument1 Is Nothing Then
                    SelectedSpedinauteVM.MonInstrument1 = MyManager.GetInstrument(SelectedSpedinauteVM.Instrument1)
                End If
                If Not SelectedSpedinauteVM.Instrument2 Is Nothing Then
                    SelectedSpedinauteVM.MonInstrument2 = MyManager.GetInstrument(SelectedSpedinauteVM.Instrument2)
                End If
                If Not SelectedSpedinauteVM.Instrument3 Is Nothing Then
                    SelectedSpedinauteVM.MonInstrument3 = MyManager.GetInstrument(SelectedSpedinauteVM.Instrument3)
                End If

                SelectedSpedinauteVM.TitreDeLaPageEnCours = "Fiche d'identité - Modification"

                MyManager.MaFicheEditee = SelectedSpedinauteVM

                Return View(SelectedSpedinauteVM)

            Else

                Return View(New SpedinauteVM)

            End If

        End Function

        <HttpPost>
        <ValidateAntiForgeryToken>
        Public Function Edit(ficheeEditee As SpedinauteVM) As ActionResult

            ' Soit prévient le Spedinaute que la session est finie
            If Session Is Nothing Then
                Return RedirectToAction("Index", "SessionExpiree")
            End If

            '   Récupere Tpujours le Manager courant
            MyManager = System.Web.HttpContext.Current.Session("User")
            If MyManager Is Nothing Then Return RedirectToAction("Index", "Connexion")

            '  Civilité
            If String.IsNullOrEmpty(ficheeEditee.Civilite.NulleOuNon) Then
                MyManager.MaFicheEditee = ficheeEditee
                MyManager.ValeursErreurs = "[ Veuillez saisir la civilité. Reprenez svp ]"
                Return RedirectToAction("Edit", "Spedinaute")
            End If

            '  Prénom
            If String.IsNullOrEmpty(ficheeEditee.Prenom.NulleOuNon) Then
                MyManager.MaFicheEditee = ficheeEditee
                MyManager.ValeursErreurs = "[ Veuillez saisir le prénom. Reprenez svp ]"
                Return RedirectToAction("Edit", "Spedinaute")
            End If

            '  Nom de Famille
            If String.IsNullOrEmpty(ficheeEditee.Nom.NulleOuNon) Then
                MyManager.MaFicheEditee = ficheeEditee
                MyManager.ValeursErreurs = "[ Veuillez saisir le Nom de Famille. Reprenez svp ]"
                Return RedirectToAction("Edit", "Spedinaute")
            End If

            '  Date de Naissance
            If String.IsNullOrEmpty(ficheeEditee.DateDeNaissance.NulleOuNon) Then
                MyManager.MaFicheEditee = ficheeEditee
                MyManager.ValeursErreurs = "[ Veuillez saisir la date de naissance. Reprenez svp ]"
                Return RedirectToAction("Edit", "Spedinaute")
            End If

            '  Lieu de Naissance
            If String.IsNullOrEmpty(ficheeEditee.LieuDeNaissance.NulleOuNon) Then
                MyManager.MaFicheEditee = ficheeEditee
                MyManager.ValeursErreurs = "[ Veuillez saisir le lieu de naissance. Reprenez svp ]"
                Return RedirectToAction("Edit", "Spedinaute")
            End If

            '  Nationalité
            If String.IsNullOrEmpty(ficheeEditee.Nationalite.NulleOuNon) Then
                MyManager.MaFicheEditee = ficheeEditee
                MyManager.ValeursErreurs = "[ Veuillez saisir le lieu de naissance. Reprenez svp ]"
                Return RedirectToAction("Edit", "Spedinaute")
            End If
            ficheeEditee.MaNationalite = MyManager.GetNationalite(ficheeEditee.Nationalite)


            '  Adresse Email
            If String.IsNullOrEmpty(ficheeEditee.AdresseEmail.NulleOuNon) Then
                MyManager.MaFicheEditee = ficheeEditee
                MyManager.ValeursErreurs = "[ L'adresse Email doit être renseignée. Reprenez svp ]"
                Return RedirectToAction("Edit", "Spedinaute")
            End If

            If Not MyManager.IsValideEmail(ficheeEditee.AdresseEmail.NulleOuNon) Then
                MyManager.MaFicheEditee = ficheeEditee
                MyManager.ValeursErreurs = "[ L'adresse Email doit être valide. Reprenez svp ]"
                Return RedirectToAction("Edit", "Spedinaute")
            End If

            '   Mot de passe
            If String.IsNullOrEmpty(ficheeEditee.PasseMot.NulleOuNon) Then
                MyManager.MaFicheEditee = ficheeEditee
                MyManager.ValeursErreurs = "[ Veuillez saisir le mot de passe. Reprenez svp ]"
                Return RedirectToAction("Edit", "Spedinaute")
            End If

            If ficheeEditee.PasseMot.Length > 10 Then
                MyManager.MaFicheEditee = ficheeEditee
                MyManager.ValeursErreurs = "[ Le mot de passe ne peut pas dépasser dix (10) caractères. Reprenez svp ]"
                Return RedirectToAction("Edit", "Spedinaute")
            End If

            If Not String.IsNullOrEmpty(MyManager.AncienMotDePasse.NulleOuNon) Then
                If Not MyManager.AncienMotDePasse.Equals(ficheeEditee.PasseMot) Then
                    '   Le mot de passe a changé
                    If Not IsStrongPWD(ficheeEditee.PasseMot, True) Then
                        MyManager.MaFicheEditee = ficheeEditee
                        MyManager.ValeursErreurs = "[ Le mot de passe doit contenir au moins 8 caractères dont au moins une Masjuscule, un chiffre et un Caractère spécial. Reprenez svp ]"
                        Return RedirectToAction("Edit", "Spedinaute")
                    End If
                End If
            Else
                '   On a saisi un nouveau mot de passe
                If Not IsStrongPWD(ficheeEditee.PasseMot, True) Then
                    MyManager.MaFicheEditee = ficheeEditee
                    MyManager.ValeursErreurs = "[ Le mot de passe doit contenir au moins 8 caractères dont au moins une Masjuscule, un chiffre et un Caractère spécial. Reprenez svp ]"
                    Return RedirectToAction("Edit", "Spedinaute")
                End If
            End If

            '  Instrument1
            If String.IsNullOrEmpty(ficheeEditee.Instrument1.NulleOuNon) Then
                MyManager.MaFicheEditee = ficheeEditee
                MyManager.ValeursErreurs = "[ Veuillez saisir l'instrument principal. Reprenez svp ]"
                Return RedirectToAction("Edit", "Spedinaute")
            End If
            ficheeEditee.MonInstrument1 = MyManager.GetInstrument(ficheeEditee.Instrument1)

            If Not ficheeEditee.Instrument2 Is Nothing Then
                ficheeEditee.MonInstrument2 = MyManager.GetInstrument(ficheeEditee.Instrument2)
            End If
            If Not ficheeEditee.Instrument3 Is Nothing Then
                ficheeEditee.MonInstrument3 = MyManager.GetInstrument(ficheeEditee.Instrument3)
            End If

            '   ComparaisonPourHistorisation
            MyManager.bAMettreAJour = False
            SelectedSpedinaute = MyManager.MaFiche

            Dim OnaFaitQuoi As [String] = [String].Empty
            OnaFaitQuoi += "<ul>"

            ' Civilité
            If Not String.IsNullOrEmpty(SelectedSpedinaute.Civilite.NulleOuNon) Then
                If Not SelectedSpedinaute.Civilite.Trim().Equals(ficheeEditee.Civilite.Trim()) Then
                    OnaFaitQuoi += "<li>" + "La Civilité a changé : " + SelectedSpedinaute.Civilite.Trim() + "] est devenue [ " + ficheeEditee.Civilite.Trim() + "</li>"
                    SelectedSpedinaute.Civilite = ficheeEditee.Civilite
                    bCivil = True
                    MyManager.bAMettreAJour = True
                End If
            Else
                OnaFaitQuoi += "<li>" + "La Civilité est devenue :" + ficheeEditee.Civilite.Trim() + "</li>"
                SelectedSpedinaute.Civilite = ficheeEditee.Civilite
                bCivil = True
                MyManager.bAMettreAJour = True
            End If


            '  Prénom(s)
            If Not String.IsNullOrEmpty(SelectedSpedinaute.Prenom.NulleOuNon) Then
                If Not SelectedSpedinaute.Prenom.Trim.ToUpper.Equals(ficheeEditee.Prenom.Trim.ToUpper) Then
                    OnaFaitQuoi += "<li>" + "Prénom a changé : " + SelectedSpedinaute.Prenom.Trim.ToUpper + " est devenu :" + ficheeEditee.Prenom.Trim.ToUpper + "</li>"
                    SelectedSpedinaute.Prenom = ficheeEditee.Prenom.Trim.ToUpper
                    bPrenom = True
                    MyManager.bAMettreAJour = True
                End If
            Else
                OnaFaitQuoi += "<li>" + "Prénom est devenu :" + ficheeEditee.Prenom.Trim() + "</li>"
                SelectedSpedinaute.Prenom = ficheeEditee.Prenom
                bPrenom = True
                MyManager.bAMettreAJour = True
            End If


            ' Autre Prénom(s)
            If Not String.IsNullOrEmpty(ficheeEditee.AutrePrenom.NulleOuNon) Then
                If Not String.IsNullOrEmpty(SelectedSpedinaute.AutrePrenom.NulleOuNon) Then
                    If Not SelectedSpedinaute.AutrePrenom.Trim.ToUpper.Equals(ficheeEditee.AutrePrenom.Trim.ToUpper) Then
                        OnaFaitQuoi += "<li>" + "L' Autre Prenom a changé : " + SelectedSpedinaute.AutrePrenom.Trim.ToUpper + " est devenu :" + ficheeEditee.AutrePrenom.Trim.ToUpper + "</li>"
                        SelectedSpedinaute.AutrePrenom = ficheeEditee.AutrePrenom
                        bPrenom2 = True
                        MyManager.bAMettreAJour = True
                    End If
                Else
                    OnaFaitQuoi += "<li>" + "L' Autre Prenom est devenu :" + ficheeEditee.AutrePrenom.Trim() + "</li>"
                    SelectedSpedinaute.AutrePrenom = ficheeEditee.AutrePrenom
                    bPrenom2 = True
                    MyManager.bAMettreAJour = True
                End If
            Else
                '   Cas ou on efface le champ
                If Not String.IsNullOrEmpty(SelectedSpedinaute.AutrePrenom.NulleOuNon) Then
                    OnaFaitQuoi += "<li>" + "L' Autre Prenom a été effacé.</li>"
                    SelectedSpedinaute.AutrePrenom = Nothing
                    bPrenom2 = True
                    MyManager.bAMettreAJour = True
                End If
            End If

            '  Nom de Famille
            If Not String.IsNullOrEmpty(ficheeEditee.Nom.NulleOuNon) Then
                If Not String.IsNullOrEmpty(SelectedSpedinaute.Nom.NulleOuNon) Then
                    ficheeEditee.Nom = ficheeEditee.Nom.Trim.ToUpper
                    If Not SelectedSpedinaute.Nom.Trim().Equals(ficheeEditee.Nom.Trim()) Then
                        OnaFaitQuoi += "<li>" + "Le Nom a changé : " + SelectedSpedinaute.Nom.Trim() + " est devenu :" + ficheeEditee.Nom.Trim() + "</li>"
                        SelectedSpedinaute.Nom = ficheeEditee.Nom
                        bNom = True
                        MyManager.bAMettreAJour = True
                    End If
                Else
                    OnaFaitQuoi += "<li>" + "Le Nom est devenu :" + ficheeEditee.Nom.Trim() + "</li>"
                    SelectedSpedinaute.Nom = ficheeEditee.Nom
                    bNom = True
                    MyManager.bAMettreAJour = True
                End If
            End If

            If Not String.IsNullOrEmpty(ficheeEditee.NomDeJeuneFille.NulleOuNon) Then
                If Not String.IsNullOrEmpty(SelectedSpedinaute.NomDeJeuneFille.NulleOuNon) Then
                    If Not SelectedSpedinaute.NomDeJeuneFille.Trim.ToUpper.Equals(ficheeEditee.NomDeJeuneFille.Trim.ToUpper) Then
                        OnaFaitQuoi += "<li>" + "Le Nom De Jeune Fille a changé : " + SelectedSpedinaute.NomDeJeuneFille.Trim.ToUpper + " est devenu :" + ficheeEditee.NomDeJeuneFille.Trim.ToUpper + "</li>"
                        SelectedSpedinaute.NomDeJeuneFille = ficheeEditee.NomDeJeuneFille.Trim.ToUpper
                        bNomJF = True
                        MyManager.bAMettreAJour = True
                    End If
                Else
                    OnaFaitQuoi += "<li>" + "Le Nom De Jeune Fille est devenu :" + ficheeEditee.NomDeJeuneFille.Trim.ToUpper + "</li>"
                    SelectedSpedinaute.NomDeJeuneFille = ficheeEditee.NomDeJeuneFille.Trim.ToUpper
                    bNomJF = True
                    MyManager.bAMettreAJour = True
                End If
            Else
                '   Cas ou on efface le champ
                If Not String.IsNullOrEmpty(SelectedSpedinaute.NomDeJeuneFille.NulleOuNon) Then
                    OnaFaitQuoi += "<li>" + "Le Nom de Jeune fille a été effacé.</li>"
                    SelectedSpedinaute.NomDeJeuneFille = Nothing
                    bNomJF = True
                    MyManager.bAMettreAJour = True
                End If
            End If

            '  Date De Naissance
            If Not ficheeEditee.DateDeNaissance Is Nothing Then
                If Not String.IsNullOrEmpty(SelectedSpedinaute.DateDeNaissance.NulleOuNon) Then
                    If Not SelectedSpedinaute.DateDeNaissance.Equals(ficheeEditee.DateDeNaissance) Then
                        OnaFaitQuoi += "<li>" + "Date de naissance a changé : " + Date.Parse(SelectedSpedinaute.DateDeNaissance).ToShortDateString() + " est devenue " + DateTime.Parse(ficheeEditee.DateDeNaissance).ToShortDateString + "</li>"
                        SelectedSpedinaute.DateDeNaissance = ficheeEditee.DateDeNaissance
                        bDate = True
                        MyManager.bAMettreAJour = True
                    End If
                Else
                    OnaFaitQuoi += "<li>" + "Date de naissance a changé est devenue " + Date.Parse(ficheeEditee.DateDeNaissance.ToString).ToShortDateString() + "</li>"
                    SelectedSpedinaute.DateDeNaissance = ficheeEditee.DateDeNaissance
                    bDate = True
                    MyManager.bAMettreAJour = True
                End If
            End If

            '  Lieu De Naissance
            If Not ficheeEditee.LieuDeNaissance Is Nothing AndAlso Not ficheeEditee.LieuDeNaissance.Trim.Equals(String.Empty) Then
                If Not String.IsNullOrEmpty(SelectedSpedinaute.LieuDeNaissance.NulleOuNon) Then
                    If Not SelectedSpedinaute.LieuDeNaissance.Trim().Equals(ficheeEditee.LieuDeNaissance.Trim()) Then
                        OnaFaitQuoi += "<li>" + "Lieu de naissance a changé : " + SelectedSpedinaute.LieuDeNaissance.Trim() + " est devenu :" + ficheeEditee.LieuDeNaissance.Trim() + "</li>"
                        SelectedSpedinaute.LieuDeNaissance = ficheeEditee.LieuDeNaissance
                        bLieu = True
                        MyManager.bAMettreAJour = True
                    End If
                Else
                    OnaFaitQuoi += "<li>" + "Lieu de naissance a changé est devenu " + ficheeEditee.LieuDeNaissance.Trim() + "</li>"
                    SelectedSpedinaute.LieuDeNaissance = ficheeEditee.LieuDeNaissance
                    bLieu = True
                    MyManager.bAMettreAJour = True
                End If
            End If

            '  Nationalité
            If Not ficheeEditee.Nationalite Is Nothing AndAlso Not ficheeEditee.Nationalite.Trim.Equals(String.Empty) Then
                If Not String.IsNullOrEmpty(SelectedSpedinaute.Nationalite.NulleOuNon) Then
                    If Not SelectedSpedinaute.Nationalite.Trim().Equals(ficheeEditee.Nationalite.Trim()) Then
                        OnaFaitQuoi += "<li>" + "La nationalité a changé : " + MyManager.GetNationalite(SelectedSpedinaute.Nationalite.Trim()) + " est devenu :" + MyManager.GetNationalite(ficheeEditee.Nationalite.Trim()) + "</li>"
                        SelectedSpedinaute.Nationalite = ficheeEditee.Nationalite
                        bNation = True
                        MyManager.bAMettreAJour = True
                    End If
                Else
                    OnaFaitQuoi += "<li>" + "La nationalité a changé est devenu " + MyManager.GetNationalite(ficheeEditee.Nationalite.Trim()) + "</li>"
                    SelectedSpedinaute.Nationalite = ficheeEditee.Nationalite
                    bNation = True
                    MyManager.bAMettreAJour = True
                End If
            End If

            If Not String.IsNullOrEmpty(ficheeEditee.PasseMot.NulleOuNon) Then
                If Not String.IsNullOrEmpty(SelectedSpedinaute.PasseMot.NulleOuNon) Then
                    If Not SelectedSpedinaute.PasseMot.Trim().Equals(ficheeEditee.PasseMot.Trim()) Then
                        OnaFaitQuoi += "<li>" + "Le mot de passe a changé : " + SelectedSpedinaute.PasseMot.Trim() + " est devenu :" + ficheeEditee.PasseMot.Trim() + "</li>"
                        SelectedSpedinaute.PasseMot = ficheeEditee.PasseMot
                        MyManager.AncienMotDePasse = ficheeEditee.PasseMot
                        bMotDePasse = True
                        MyManager.bAMettreAJour = True
                    End If
                Else
                    OnaFaitQuoi += "<li>" + "Le mot de passe a changé est devenu " + ficheeEditee.PasseMot.Trim() + "</li>"
                    SelectedSpedinaute.PasseMot = ficheeEditee.PasseMot
                    MyManager.AncienMotDePasse = ficheeEditee.PasseMot
                    bNation = True
                    MyManager.bAMettreAJour = True
                End If
            End If

            '  Numero De Fixe
            If Not String.IsNullOrEmpty(ficheeEditee.NumeroDeFixe.NulleOuNon) Then
                If Not String.IsNullOrEmpty(SelectedSpedinaute.NumeroDeFixe.NulleOuNon) Then
                    If Not SelectedSpedinaute.NumeroDeFixe.Trim().Equals(ficheeEditee.NumeroDeFixe.Trim()) Then
                        OnaFaitQuoi += "<li>" + "Le Numero De Fixe a changé : " + SelectedSpedinaute.NumeroDeFixe.Trim() + " est devenu :" + ficheeEditee.NumeroDeFixe.Trim() + "</li>"
                        SelectedSpedinaute.NumeroDeFixe = ficheeEditee.NumeroDeFixe
                        bTelFixe = True
                        MyManager.bAMettreAJour = True
                    End If
                Else
                    OnaFaitQuoi += "<li>" + "Le Numero De Fixe est devenu :" + ficheeEditee.NumeroDeFixe.Trim() + "</li>"
                    SelectedSpedinaute.NumeroDeFixe = ficheeEditee.NumeroDeFixe
                    bTelFixe = True
                    MyManager.bAMettreAJour = True
                End If
            Else
                '   Cas ou on efface le champ
                If Not String.IsNullOrEmpty(SelectedSpedinaute.NumeroDeFixe.NulleOuNon) Then
                    OnaFaitQuoi += "<li>" + "Le Numéro de fixe a été effacé.</li>"
                    SelectedSpedinaute.NumeroDeFixe = Nothing
                    bTelFixe = True
                    MyManager.bAMettreAJour = True
                End If
            End If

            '  Numero De Mobile
            If Not String.IsNullOrEmpty(ficheeEditee.NumeroDeMobile.NulleOuNon) Then
                If Not String.IsNullOrEmpty(SelectedSpedinaute.NumeroDeMobile.NulleOuNon) Then
                    If Not SelectedSpedinaute.NumeroDeMobile.Trim().Equals(ficheeEditee.NumeroDeMobile.Trim()) Then
                        OnaFaitQuoi += "<li>" + "Le Numero De Mobile a changé : " + SelectedSpedinaute.NumeroDeMobile.Trim() + " est devenu :" + ficheeEditee.NumeroDeMobile.Trim() + "</li>"
                        SelectedSpedinaute.NumeroDeMobile = ficheeEditee.NumeroDeMobile
                        bTelMobile = True
                        MyManager.bAMettreAJour = True
                    End If
                Else
                    OnaFaitQuoi += "<li>" + "Le Numero De Mobile est devenu :" + ficheeEditee.NumeroDeMobile.Trim() + "</li>"
                    SelectedSpedinaute.NumeroDeMobile = ficheeEditee.NumeroDeMobile
                    bTelMobile = True
                    MyManager.bAMettreAJour = True
                End If
            Else
                '   Cas ou on efface le champ
                If Not String.IsNullOrEmpty(SelectedSpedinaute.NumeroDeMobile.NulleOuNon) Then
                    OnaFaitQuoi += "<li>" + "Le Numéro de mobile a été effacé.</li>"
                    SelectedSpedinaute.NumeroDeMobile = Nothing
                    bTelMobile = True
                    MyManager.bAMettreAJour = True
                End If
            End If

            '  Adresse Email
            If Not String.IsNullOrEmpty(SelectedSpedinaute.AdresseEmail.NulleOuNon) Then
                If Not SelectedSpedinaute.AdresseEmail.Trim().Equals(ficheeEditee.AdresseEmail.Trim()) Then
                    OnaFaitQuoi += "<li>" + "Le Adresse Email a changé : " + SelectedSpedinaute.AdresseEmail.Trim() + " est devenue :" + ficheeEditee.AdresseEmail.Trim() + "</li>"
                    SelectedSpedinaute.AdresseEmail = ficheeEditee.AdresseEmail
                    bEmail = True
                    MyManager.bAMettreAJour = True
                End If
            Else
                OnaFaitQuoi += "<li>" + "L'Adresse Email est devenue :" + ficheeEditee.AdresseEmail.Trim() + "</li>"
                SelectedSpedinaute.AdresseEmail = ficheeEditee.AdresseEmail
                bEmail = True
                MyManager.bAMettreAJour = True
            End If


            '  Instrument Principal
            If Not ficheeEditee.Instrument1 Is Nothing Then
                If Not SelectedSpedinaute.Instrument1 Is Nothing Then
                    If Not SelectedSpedinaute.Instrument1.Equals(ficheeEditee.Instrument1) Then
                        OnaFaitQuoi += "<li>" + "L'Instrument principal a changé : " + MyManager.GetInstrument(SelectedSpedinaute.Instrument1) + " est devenu :" + MyManager.GetInstrument(ficheeEditee.Instrument1) + "</li>"
                        SelectedSpedinaute.Instrument1 = ficheeEditee.Instrument1
                        bMusic1 = True
                        MyManager.bAMettreAJour = True
                    End If
                Else
                    OnaFaitQuoi += "<li>" + "L'Instrument principal est devenu :" + MyManager.GetInstrument(ficheeEditee.Instrument1) + "</li>"
                    SelectedSpedinaute.Instrument1 = ficheeEditee.Instrument1
                    bMusic1 = True
                    MyManager.bAMettreAJour = True
                End If
            End If

            '  Instrument Secondaire
            If Not ficheeEditee.Instrument2 Is Nothing AndAlso Not ficheeEditee.Instrument2.Equals(114) Then
                If Not SelectedSpedinaute.Instrument2 Is Nothing Then
                    If Not SelectedSpedinaute.Instrument2.Equals(ficheeEditee.Instrument2) Then
                        OnaFaitQuoi += "<li>" + "L'Instrument Secondaire a changé : " + MyManager.GetInstrument(SelectedSpedinaute.Instrument2) + " est devenu :" + MyManager.GetInstrument(ficheeEditee.Instrument2) + "</li>"
                        SelectedSpedinaute.Instrument2 = ficheeEditee.Instrument2
                        bMusic2 = True
                        MyManager.bAMettreAJour = True
                    End If
                Else
                    OnaFaitQuoi += "<li>" + "L'Instrument Secondaire a changé  est devenu  " + MyManager.GetInstrument(ficheeEditee.Instrument2) + "</li>"
                    SelectedSpedinaute.Instrument2 = ficheeEditee.Instrument2
                    bMusic2 = True
                    MyManager.bAMettreAJour = True
                End If
            Else
                '   Cas ou on efface le champ
                If Not String.IsNullOrEmpty(SelectedSpedinaute.Instrument2.NulleOuNon) AndAlso Not SelectedSpedinaute.Instrument2.Equals(114) Then
                    OnaFaitQuoi += "<li>" + "Le second instrument a été effacé.</li>"
                    SelectedSpedinaute.Instrument2 = Nothing
                    bMusic2 = True
                    MyManager.bAMettreAJour = True
                End If
            End If

            '  Instrument Tertiaire
            If Not ficheeEditee.Instrument3 Is Nothing AndAlso Not ficheeEditee.Instrument3.Equals(114) Then
                If Not SelectedSpedinaute.Instrument3 Is Nothing Then
                    If Not SelectedSpedinaute.Instrument3.Equals(ficheeEditee.Instrument3) Then
                        OnaFaitQuoi += "<li>" + "Le troisième Instrument a changé : " + MyManager.GetInstrument(SelectedSpedinaute.Instrument3) + " est devenu :" + MyManager.GetInstrument(ficheeEditee.Instrument3) + "</li>"
                        SelectedSpedinaute.Instrument3 = ficheeEditee.Instrument3
                        bMusic3 = True
                        MyManager.bAMettreAJour = True
                    End If
                Else
                    OnaFaitQuoi += "<li>" + "Le troisième Instrument est devenu :" + MyManager.GetInstrument(ficheeEditee.Instrument3) + "</li>"
                    SelectedSpedinaute.Instrument3 = ficheeEditee.Instrument3
                    bMusic3 = True
                    MyManager.bAMettreAJour = True
                End If
            Else
                '   Cas ou on efface le champ
                If Not String.IsNullOrEmpty(SelectedSpedinaute.Instrument3.NulleOuNon) AndAlso Not SelectedSpedinaute.Instrument3.Equals(114) Then
                    OnaFaitQuoi += "<li>" + "Le troisième Instrument  a été effacé.</li>"
                    SelectedSpedinaute.Instrument3 = Nothing
                    bMusic3 = True
                    MyManager.bAMettreAJour = True
                End If

            End If

            '  Pseudonyme Principal
            If Not ficheeEditee.Pseudonyme1 Is Nothing AndAlso Not ficheeEditee.Pseudonyme1.Trim.Equals(String.Empty) Then
                If Not String.IsNullOrEmpty(SelectedSpedinaute.Pseudonyme1.NulleOuNon) Then
                    If Not SelectedSpedinaute.Pseudonyme1.Trim().Equals(ficheeEditee.Pseudonyme1.Trim()) Then
                        OnaFaitQuoi += "<li>" + "Le Pseudonyme principal a changé : " + SelectedSpedinaute.Pseudonyme1.Trim() + " est devenu :" + ficheeEditee.Pseudonyme1.Trim() + "</li>"
                        SelectedSpedinaute.Pseudonyme1 = ficheeEditee.Pseudonyme1
                        bPseudo1 = True
                        MyManager.bAMettreAJour = True
                    End If
                Else
                    OnaFaitQuoi += "<li>" + "Le Pseudonyme principal est devenu :" + ficheeEditee.Pseudonyme1.Trim() + "</li>"
                    SelectedSpedinaute.Pseudonyme1 = ficheeEditee.Pseudonyme1
                    bPseudo1 = True
                    MyManager.bAMettreAJour = True
                End If
            Else
                '   Cas ou on efface le champ
                If Not String.IsNullOrEmpty(SelectedSpedinaute.Pseudonyme1.NulleOuNon) Then
                    OnaFaitQuoi += "<li>" + "Le Pseudonyme Principal a été effacé.</li>"
                    SelectedSpedinaute.Pseudonyme1 = Nothing
                    bPseudo1 = True
                    MyManager.bAMettreAJour = True
                End If
            End If

            '  Pseudonyme Secondaire
            If Not ficheeEditee.Pseudonyme2 Is Nothing AndAlso Not ficheeEditee.Pseudonyme2.Trim.Equals(String.Empty) Then
                If Not String.IsNullOrEmpty(SelectedSpedinaute.Pseudonyme2.NulleOuNon) Then
                    If Not SelectedSpedinaute.Pseudonyme2.Trim().Equals(ficheeEditee.Pseudonyme2.Trim()) Then
                        OnaFaitQuoi += "<li>" + "Le Pseudonyme Secondaire a changé : " + SelectedSpedinaute.Pseudonyme2.Trim() + " est devenu :" + ficheeEditee.Pseudonyme2.Trim() + "</li>"
                        SelectedSpedinaute.Pseudonyme2 = ficheeEditee.Pseudonyme2
                        BPseudo2 = True
                        MyManager.bAMettreAJour = True
                    End If
                Else
                    OnaFaitQuoi += "<li>" + "Le Pseudonyme Secondaire est devenu :" + ficheeEditee.Pseudonyme2.Trim() + "</li>"
                    SelectedSpedinaute.Pseudonyme2 = ficheeEditee.Pseudonyme2
                    BPseudo2 = True
                    MyManager.bAMettreAJour = True
                End If
            Else
                '   Cas ou on efface le champ
                If Not String.IsNullOrEmpty(SelectedSpedinaute.Pseudonyme2.NulleOuNon) Then
                    OnaFaitQuoi += "<li>" + "Le Second Pseudonyme a été effacé.</li>"
                    SelectedSpedinaute.Pseudonyme2 = Nothing
                    BPseudo2 = True
                    MyManager.bAMettreAJour = True
                End If
            End If

            '  Pseudonyme Tertiaire
            If Not ficheeEditee.Pseudonyme3 Is Nothing AndAlso Not ficheeEditee.Pseudonyme3.Trim.Equals(String.Empty) Then
                If Not String.IsNullOrEmpty(SelectedSpedinaute.Pseudonyme3.NulleOuNon) Then
                    If Not SelectedSpedinaute.Pseudonyme3.Trim().Equals(ficheeEditee.Pseudonyme3.Trim()) Then
                        OnaFaitQuoi += "<li>" + "Le Troisieme Pseudonyme  a changé : " + SelectedSpedinaute.Pseudonyme3.Trim() + " est devenu :" + ficheeEditee.Pseudonyme3.Trim() + "</li>"
                        SelectedSpedinaute.Pseudonyme3 = ficheeEditee.Pseudonyme3
                        bPseudo3 = True
                        MyManager.bAMettreAJour = True
                    End If
                Else
                    OnaFaitQuoi += "<li>" + "Le Troisieme Pseudonyme  est devenu :" + ficheeEditee.Pseudonyme3.Trim() + "</li>"
                    SelectedSpedinaute.Pseudonyme3 = ficheeEditee.Pseudonyme3
                    bPseudo3 = True
                    MyManager.bAMettreAJour = True
                End If
            Else
                '   Cas ou on efface le champ
                If Not String.IsNullOrEmpty(SelectedSpedinaute.Pseudonyme3.NulleOuNon) Then
                    OnaFaitQuoi += "<li>" + "Le Troisieme Pseudonyme a été effacé.</li>"
                    SelectedSpedinaute.Pseudonyme3 = Nothing
                    bPseudo3 = True
                    MyManager.bAMettreAJour = True
                End If
            End If

            Dim szTitulaireMaj = String.Empty
            If Not ficheeEditee.Prenom Is Nothing Then
                szTitulaireMaj = szTitulaireMaj + ficheeEditee.Prenom.Trim
            End If
            If Not ficheeEditee.AutrePrenom Is Nothing Then
                szTitulaireMaj = szTitulaireMaj + " " + ficheeEditee.AutrePrenom.Trim
            End If
            If Not ficheeEditee.Nom Is Nothing Then
                szTitulaireMaj = szTitulaireMaj + " " + ficheeEditee.Nom.ToUpper.Trim
            End If

            '   Met a jour le Shared MyManager
            MyManager.SpedinauteConnecte = szTitulaireMaj

            OnaFaitQuoi += "</ul><br></br>"

            If MyManager.bAMettreAJour = True Then

                Dim laKleTcheMwen = SelectedSpedinaute.IdSpedinaute

                MyManager.DbContext.Entry(SelectedSpedinaute).State = EntityState.Modified
                MyManager.DbContext.SaveChanges()

                '  Recharge la fiche a jour Du Compte Artiste
                SelectedSpedinaute = MyManager.DbContext.Spedinautes.FirstOrDefault(Function(s) s.IdSpedinaute = laKleTcheMwen)
                SelectedSpedinauteVM = AutoMapper.Mapper.Map(Of SpedinauteVM)(SelectedSpedinaute)

                If Not SelectedSpedinauteVM.Nationalite Is Nothing Then
                    SelectedSpedinauteVM.MaNationalite = SelectedSpedinauteVM.Nationalite.Trim()
                End If
                If Not SelectedSpedinauteVM.Instrument1 Is Nothing Then
                    SelectedSpedinauteVM.MonInstrument1 = MyManager.GetInstrument(SelectedSpedinauteVM.Instrument1)
                End If
                If Not SelectedSpedinauteVM.Instrument2 Is Nothing Then
                    SelectedSpedinauteVM.MonInstrument2 = MyManager.GetInstrument(SelectedSpedinauteVM.Instrument2)
                End If
                If Not SelectedSpedinauteVM.Instrument3 Is Nothing Then
                    SelectedSpedinauteVM.MonInstrument3 = MyManager.GetInstrument(SelectedSpedinauteVM.Instrument3)
                End If

                SelectedSpedinaute = MyManager.MaFiche
                SelectedSpedinauteVM = AutoMapper.Mapper.Map(Of SpedinauteVM)(SelectedSpedinaute)
                MyManager.MaFicheEditee = SelectedSpedinauteVM

                Historisation(OnaFaitQuoi, SelectedSpedinauteVM)
                NotificationdEmail(OnaFaitQuoi, SelectedSpedinauteVM)

                Session("user") = MyManager

            End If

            '  Charge la fiche Artiste
            Return RedirectToAction("Index", "Spedinaute")

        End Function

#End Region

#Region "METHODES"
        Private Sub Historisation(watwedo As String, obj As SpedinauteVM)

            Dim szWatwedo = watwedo.Replace("<li>", String.Empty).Replace("</li>", ".").Replace("<ul>", String.Empty).Replace("</ul>", Chr(13))

            '  SAUVEGARDE DE LA TACHE EFFECTUEE
            Dim UneTache = New TacheEffectuee()

            UneTache.TypeDeTache = "M"
            UneTache.DateDuJour = DateTime.Today.ToShortDateString()
            UneTache.HeureCourante = DateTime.Today.ToShortTimeString()

            UneTache.IdAdresseConcernee = "N"
            UneTache.IdModePasseConcerne = "N"
            UneTache.IdRibIbanConcerne = "N"
            UneTache.FicheConcernee = "O"

            UneTache.NomDuDevice = ""
            UneTache.AdresseMac = ""
            UneTache.AdresseIP = ""

            UneTache.Commentaire = szWatwedo
            UneTache.HistoriserON = True

            UneTache.IdSpedinaute = SelectedSpedinaute.IdSpedinaute

            MyManager.DbContext.TacheEffectuees.Add(UneTache)
            MyManager.DbContext.SaveChanges()

            '   MISE A JOUR DE LA FICHE DANS GESPERE
            If MyManager.DbCtx IsNot Nothing Then
                '  HISTORISATION DE LA TACHE DANS LA BASE GESPERE
                Dim UneTacheB = New TacheHistorisee()

                UneTacheB.TypeDeTache = "M"
                UneTacheB.DateDuJour = DateTime.Today.ToShortDateString()
                UneTacheB.HeureCourante = DateTime.Now.TimeOfDay

                UneTacheB.IdAdresseConcernee = "N"
                UneTacheB.IdModePasseConcerne = "N"
                UneTacheB.IdRibIbanConcerne = "N"
                UneTacheB.FicheConcernee = "O"


                UneTacheB.Commentaire = szWatwedo
                UneTacheB.HistoriserON = True

                UneTacheB.IdSpedinaute = SelectedSpedinaute.IdSpedinaute
                UneTacheB.Id_ayant = SelectedSpedinaute.NumeroIdAyant

                MyManager.DbCtx.TacheHistorisees.Add(UneTacheB)
                MyManager.DbCtx.SaveChanges()

                MyManager.bDone = True

                If MyManager.DbCtx.AyantDro.Count() > 0 Then

                    If Not MyManager.SelectedAyantDroit Is Nothing Then

                        MyManager.SelectedAyantDroit.dt_maj = DateTime.Today

                        If bCivil Then MyManager.SelectedAyantDroit.id_civil = obj.Civilite
                        If bPrenom Then MyManager.SelectedAyantDroit.prenom = obj.Prenom.Trim.ToUpper

                        If bPrenom2 Then
                            If Not String.IsNullOrEmpty(obj.AutrePrenom.NulleOuNon) Then
                                MyManager.SelectedAyantDroit.prenom2 = obj.AutrePrenom.Trim.ToUpper
                            Else
                                MyManager.SelectedAyantDroit.prenom2 = Nothing
                            End If
                        End If

                        If bNom Then MyManager.SelectedAyantDroit.nom = obj.Nom.Trim.ToUpper

                        If bNomJF Then
                            If Not String.IsNullOrEmpty(obj.NomDeJeuneFille.NulleOuNon) Then
                                MyManager.SelectedAyantDroit.nomJF = obj.NomDeJeuneFille.Trim.ToUpper
                            Else
                                MyManager.SelectedAyantDroit.nomJF = Nothing
                            End If
                        End If

                        If bDate Then MyManager.SelectedAyantDroit.dt_naiss = obj.DateDeNaissance

                        If bLieu Then MyManager.SelectedAyantDroit.lieuNaiss = obj.LieuDeNaissance.Trim.ToUpper
                        If bNation Then MyManager.SelectedAyantDroit.id_pays = obj.Nationalite.Trim.ToUpper

                        If bMotDePasse Then MyManager.SelectedAyantDroit.password = obj.PasseMot

                        If bMusic1 Then MyManager.SelectedAyantDroit.id1Instr = (StrDup((5 - obj.Instrument1.ToString.Length), "0")) + obj.Instrument1.ToString()

                        If bMusic2 Then
                            If Not obj.Instrument2 Is Nothing AndAlso Not obj.Instrument2.Equals(114) Then
                                MyManager.SelectedAyantDroit.id2Instr = (StrDup((5 - obj.Instrument2.ToString.Length), "0")) + obj.Instrument2.ToString()
                            Else
                                MyManager.SelectedAyantDroit.id2Instr = Nothing
                            End If
                        End If

                        If bMusic3 Then
                            If Not obj.Instrument3 Is Nothing AndAlso Not obj.Instrument2.Equals(114) Then
                                MyManager.SelectedAyantDroit.id3Instr = (StrDup((5 - obj.Instrument3.ToString.Length), "0")) + obj.Instrument3.ToString()
                            Else
                                MyManager.SelectedAyantDroit.id3Instr = Nothing
                            End If
                        End If

                        If bPseudo1 Then
                            If Not String.IsNullOrEmpty(obj.Pseudonyme1) Then
                                MyManager.SelectedAyantDroit.pseudo1 = obj.Pseudonyme1.ToString
                            Else
                                MyManager.SelectedAyantDroit.pseudo1 = Nothing
                            End If
                        End If

                        If BPseudo2 Then
                            If Not String.IsNullOrEmpty(obj.Pseudonyme2) Then
                                MyManager.SelectedAyantDroit.pseudo2 = obj.Pseudonyme2.ToString
                            Else
                                MyManager.SelectedAyantDroit.pseudo2 = Nothing
                            End If
                        End If

                        If bPseudo3 Then
                            If Not String.IsNullOrEmpty(obj.Pseudonyme3) Then
                                MyManager.SelectedAyantDroit.pseudo3 = obj.Pseudonyme3.ToString
                            Else
                                MyManager.SelectedAyantDroit.pseudo3 = Nothing
                            End If
                        End If

                        If bNation Then MyManager.SelectedAyantDroit.id_pays = obj.Nationalite.Trim.ToUpper

                        MyManager.SelectedAyantDroit.notes = MyManager.SelectedAyantDroit.notes + Chr(13) + DateTime.Today.ToShortDateString().Replace("/", "-") + " : CHANGEMENTS EFFECTUES : " + szWatwedo
                        MyManager.DbCtx.Entry(MyManager.SelectedAyantDroit).State = EntityState.Modified
                        MyManager.DbCtx.SaveChanges()

                        If Not MyManager.SelectedBeneficiaire Is Nothing Then

                            If bCivil Then MyManager.SelectedBeneficiaire.id_civil = obj.Civilite

                            If bPrenom Then MyManager.SelectedBeneficiaire.prenom = obj.Prenom.Trim.ToUpper

                            If bPrenom2 Then
                                If Not String.IsNullOrEmpty(obj.AutrePrenom.NulleOuNon) Then
                                    MyManager.SelectedBeneficiaire.prenom2 = obj.AutrePrenom.Trim.ToUpper
                                Else
                                    MyManager.SelectedBeneficiaire.prenom2 = Nothing
                                End If
                            End If

                            If bNom Then MyManager.SelectedBeneficiaire.nom = obj.Nom.Trim.ToUpper

                            If bNomJF Then
                                If Not String.IsNullOrEmpty(obj.NomDeJeuneFille.NulleOuNon) Then
                                    MyManager.SelectedBeneficiaire.nomjf = obj.NomDeJeuneFille.Trim.ToUpper
                                Else
                                    MyManager.SelectedBeneficiaire.nomjf = Nothing
                                End If
                            End If

                            If bTelFixe Then
                                If Not String.IsNullOrEmpty(obj.NumeroDeFixe.NulleOuNon) Then
                                    MyManager.SelectedBeneficiaire.no_telfi = obj.NumeroDeFixe
                                Else
                                    MyManager.SelectedBeneficiaire.no_telfi = Nothing
                                End If
                            End If

                            If bTelMobile Then
                                If Not String.IsNullOrEmpty(obj.NumeroDeMobile.NulleOuNon) Then
                                    MyManager.SelectedBeneficiaire.no_telpo = obj.NumeroDeMobile
                                Else
                                    MyManager.SelectedBeneficiaire.no_telpo = Nothing
                                End If
                            End If

                            If bEmail Then MyManager.SelectedBeneficiaire.mele = obj.AdresseEmail

                            '   rajout après Oubli signalé par xlh
                            If bDate Then MyManager.SelectedBeneficiaire.dt_naiss = obj.DateDeNaissance
                            If bLieu Then MyManager.SelectedBeneficiaire.lieunaiss = obj.LieuDeNaissance.Trim.ToUpper
                            If bNation Then MyManager.SelectedBeneficiaire.id_pays = obj.Nationalite.Trim.ToUpper

                        End If

                        MyManager.DbCtx.Entry(MyManager.SelectedBeneficiaire).State = EntityState.Modified
                        MyManager.DbCtx.SaveChanges()

                    End If

                End If

            End If

        End Sub

        Private Sub NotificationdEmail(watwedo As String, obj As SpedinauteVM)

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
            mail.CC.Add("assistance@spedidam.fr")
            mail.Bcc.Add("moustapha.thiam@spedidam.fr,xavier.lehir@spedidam.fr")

            mail.Subject = "Notification du " + DateTime.Today.ToShortDateString() + "."

            mail.Body = "<P>Bonjour "
            mail.Body += obj.Prenom.Trim() + "  "
            mail.Body += obj.Nom.Trim().ToUpper()
            mail.Body += "<br /> <br /> Suite à votre session sur MySpedidam.fr, vous avez effectué les modifications suivantes : <br /><br/>"
            mail.Body += watwedo + "<br></br>" + "<br></br>" + "Si vous n'êtes pas à l'origine de cette modification, avertissez nous en cliquant sur le "
            mail.Body += (lienAlerte & Convert.ToString(".")) + "<br></br>"
            mail.Body += "Cordialement .</P>"

            smtpServer.Send(mail)

        End Sub

        Private Function IsStrongPWD(szPWD As String, mode As Boolean) As Boolean

            Dim bStrong As Boolean = False

            Dim isSpecialCar As Byte = 0
            Dim isLetterCar As Byte = 0
            Dim isNumberCar As Byte = 0

            Dim nbUCar As Integer = 0
            Dim nbLCar As Integer = 0

            If String.IsNullOrEmpty(szPWD) Then
                Return bStrong
            End If

            If szPWD.Length > 10 Then
                Return False
            End If

            If szPWD.Length >= 8 Then
                bStrong = True
            End If

            Dim c As Char = ControlChars.NullChar

            For Each c_loopVariable As Char In szPWD
                c = c_loopVariable

                If Not Char.IsLetterOrDigit(c.ToString(), 0) Then

                    isSpecialCar += 1
                ElseIf Char.IsLetter(c.ToString(), 0) Then
                    isLetterCar += 1

                    If Char.IsLower(c.ToString(), 0) Then
                        nbLCar += 1
                    End If

                    If Char.IsUpper(c.ToString(), 0) Then
                        nbUCar += 1

                    End If
                ElseIf Char.IsDigit(c.ToString(), 0) Then
                    isNumberCar += 1

                End If
            Next

            '   Limite le control a deux conditions
            'If isLetterCar >= 1 And isNumberCar >= 1 Then
            '    bStrong = True
            'Else
            '    bStrong = False
            'End If

            If mode Then
                If isSpecialCar >= 1 And nbLCar >= 1 And nbUCar >= 1 And isNumberCar >= 1 Then
                    bStrong = True
                Else
                    bStrong = False
                End If
            End If

            Return bStrong

        End Function

#End Region

    End Class

End Namespace