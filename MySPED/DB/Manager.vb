Imports System.Data.Entity
Imports System.IO
Imports System.Net.Mail
Imports System.Runtime.Remoting.Messaging
Imports System.Text.RegularExpressions


''' <summary>
''' Cette Classe Gere toutes les Taches du Compte Artiste
''' http://docs.telerik.com/aspnet-mvc/tutorials/quickstart/team-efficiency#input-controls
''' </summary>

Public Class Manager

#Region "DB CONTEXTES"
    Public Property DbContext() As FacilDBEntities
    Public Property DbCtx() As OurDBEntities

    Public Property bDone As [Boolean]
    Public Property bDone2 As [Boolean]

    Public Property bDBOk As [Boolean]
    Public Property bDBOk2 As [Boolean]

    Public Property bAMettreAJour As Boolean



#End Region

#Region "MEMBRES PARTAGES"
    Public Property NumeroIdAyant As String
    Public Property IdSpedinaute As Integer

    Public Property SpedinauteConnecte As String
    Public Property TitreDeLaPageEnCours As String
    Public Property AdresseDeCourriel As String
    Public Property SousTitreModeEdition As String

    Public Property AncienMotDePasse As String

    Public Property ValeursErreurs As String
    Public Property ValeursNationales As String
    Public Property ValeursInstruments As String

    Public bAddPos, bAddFis, BAddRib, bMajEnCoursDuRib, bMajEnCoursFiche As Boolean

    Public Property SelectedAyantDroit As AyantDro
    Public Property SelectedBeneficiaire As Benefici
    Public Property SelectedAdresGespere As Adresse
    Public Property SelectedAdresGespereFis As Adresse


    Public Property MonRib As RibIban
    Public Property MonAdressePos As SpedAdresse
    Public Property MonAdresseFis As SpedAdresse
    Public Property AssembleeGenerale As Assemblee

    Public Property MaFiche As Spedinaute

    Public Property UneDeclaration As DB.DeclarationVM

    Public Property UneRepartition As DB.RepartitionVM

#End Region

#Region "LISTE DE REFERENCES"

    Public Property ListeDesIntruments As List(Of Instrument)
    Public Property ListeDesNationalites As List(Of Pays)

    Public Property ListeDesTypesAdresses As List(Of TypeAdresses)
    Public Property ListeDesTypeDeVoies As List(Of TypeDeVoies)

    Public Property ListeDesAnneesFiscales As List(Of AnneeFiscale)



#End Region

#Region "MODELES"

    'Public Property FicheDB As Spedinaute
    Public Property MesDroits As UserFunctionalityRights

    Public Property RibActuel As RibIban

    Public Property SpedAdresseActuel As SpedAdresse
    Public Property SpedAdresseActuelFis As SpedAdresse

#End Region

#Region "VIEW MODELES"
    Public Property MaFicheEditee() As SpedinauteVM
    Public Property RibEdite As RibIbanVM
    Public Property SpedAdresseEdite As SpedAdresseVM
    Public Property SpedAdresseEditeFis As SpedAdresseVM

#End Region

#Region "CONSTRUCTEUR"

    Sub New()

        AutoMapper.Mapper.CreateMap(Of DB.Spedinaute, DB.SpedinauteVM)()
        AutoMapper.Mapper.CreateMap(Of DB.SpedAdresse, DB.SpedAdresseVM)()
        AutoMapper.Mapper.CreateMap(Of DB.RibIban, DB.RibIbanVM)()
        AutoMapper.Mapper.CreateMap(Of DB.Assemblee, DB.AssembleeVM)()

        '   Connexion aux bases de données
        SeConnecterAFacilDB()

        If bDBOk Then

            SeConnecterAHarmonie()

            '   Création du ContratDeConnexion
            MesDroits = New UserFunctionalityRights
            MesDroits.IsLogged = False
            MesDroits.IsMenuGeneralAssembly = False

        End If

    End Sub

#End Region

#Region "CONTROLE DES ACCES DB"

    Public Function ControlerAccesDB() As Boolean

        Dim bEtatCnxDB = True

        If bDBOk = False And Not bDBOk2 Then
            'RenvoiParMailDesErreurs(ValeursNationales)
            Return False
        End If

        If bDBOk2 = False Then
            'RenvoiParMailDesErreurs(ValeursNationales)
            Return False
        End If

        Return bEtatCnxDB

    End Function

#End Region

#Region "AUTHENTIFICATION"

    Public Function SeConnecter(ByVal szEmail As String, ByVal szMotDePasse As String) As Boolean

        Dim bTrouve = False

        MaFiche = Nothing
        MonRib = Nothing
        MonAdressePos = Nothing
        MonAdresseFis = Nothing

        SelectedAyantDroit = Nothing
        SelectedBeneficiaire = Nothing
        SelectedAdresGespere = Nothing
        SelectedAdresGespereFis = Nothing


        '   Recherche Du Compte Artiste sur la Base Web Chez Ikoula
        MaFiche = DbContext.Spedinautes.Where(Function(s) s.AdresseEmail.Trim = szEmail.Trim AndAlso s.PasseMot.Trim = szMotDePasse.Trim).FirstOrDefault()

        '   Si la fiche Spedinaute est trouvée
        If Not MaFiche Is Nothing Then

            ' CAS des users sans mot d passe
            If MaFiche.PasseMot Is Nothing Then Return False
            If MaFiche.PasseMot.NulleOuNon = String.Empty Then Return False

            bTrouve = True
            AncienMotDePasse = MaFiche.PasseMot

            '   Récupération de
            MesDroits.IsLogged = True

            If Not MaFiche.TypeDeProfil Is Nothing Then
                If MaFiche.TypeDeProfil.Trim.Equals("AD") Then
                    MesDroits.IsMenuGeneralAssembly = True
                End If
            End If

            '   Stocke les Shared
            IdSpedinaute = MaFiche.IdSpedinaute
            NumeroIdAyant = MaFiche.NumeroIdAyant


            bAMettreAJour = False

            '   Recherche et récupere Rib 
            BAddRib = False
            MonRib = DbContext.RibIbans.Where(Function(s) s.IdSpedinaute = MaFiche.IdSpedinaute AndAlso s.Actuel = True).FirstOrDefault()
            If MonRib Is Nothing Then
                BAddRib = True
            End If

            '   Recherche et récupere les Adresses
            bAddPos = False
            MonAdressePos = GetTMonAdresseVraie(IdSpedinaute, True)

            '   Si Pas d'Adresse Propose une Adresse Vierge ou bien un message d'information
            '   Je déplace le traitement dans les fonctions de Comparaison
            If MonAdressePos Is Nothing Then
                bAddPos = True
            End If

            bAddFis = False
            MonAdresseFis = GetTMonAdresseVraie(IdSpedinaute, False)
            If MonAdresseFis Is Nothing Then
                bAddFis = True
            End If

            '   *********************************************************************************************************************************************
            '   Recherche Des Fiches Présentes Dans notre Base interne Gespere
            SelectedAyantDroit = DbCtx.AyantDro.Where(Function(s) (s.id_ayant.Trim() = MaFiche.NumeroIdAyant.Trim())).FirstOrDefault()

            '   Fiche Bénéficiaire
            Dim rqB = From objBenef As Benefici In DbCtx.Benefici
                      Where objBenef.id_ayant.Trim = MaFiche.NumeroIdAyant.Trim And objBenef.QuaBenef.Trim = "AY" And String.IsNullOrEmpty(objBenef.id_etat)
                      Select objBenef
            If Not rqB Is Nothing AndAlso rqB.Any Then
                SelectedBeneficiaire = rqB.FirstOrDefault
            End If

            Dim rqA = From objAdresse As Adresse In DbCtx.Adresses
                      Where objAdresse.id_ayant.Trim = MaFiche.NumeroIdAyant.Trim And String.IsNullOrEmpty(objAdresse.etat) And objAdresse.id_type.Trim.Equals("POS")
                      Select objAdresse

            If Not rqA Is Nothing AndAlso rqA.Any Then
                SelectedAdresGespere = rqA.FirstOrDefault
            Else
                '   désactivation des adresses Postales
                If Not MonAdressePos Is Nothing Then

                    MonAdressePos.Actuelle = False
                    DbContext.Entry(MonAdressePos).State = EntityState.Modified
                    DbContext.SaveChanges()

                    MonAdressePos = Nothing

                End If

            End If

            Dim rqF = From objAdresse As Adresse In DbCtx.Adresses
                      Where objAdresse.id_ayant.Trim = MaFiche.NumeroIdAyant.Trim And String.IsNullOrEmpty(objAdresse.etat) And objAdresse.id_type.Trim.Equals("FIS")
                      Select objAdresse
            If Not rqF Is Nothing AndAlso rqF.Any Then
                SelectedAdresGespereFis = rqF.FirstOrDefault
            Else
                '   désactivation des adresses Fiscales
                If Not MonAdresseFis Is Nothing Then
                    MonAdresseFis.Actuelle = False
                    DbContext.Entry(MonAdresseFis).State = EntityState.Modified
                    DbContext.SaveChanges()
                    MonAdresseFis = Nothing
                End If

            End If

            '   *********************************************************************************************************************************************
            '   Lancement des Recherches de Mise a Jour ascendantes
            Me.ComparerArtiste()
            MaFiche = DbContext.Spedinautes.Where(Function(s) s.AdresseEmail.Trim = szEmail.Trim AndAlso s.PasseMot.Trim = szMotDePasse.Trim).FirstOrDefault()
            MaFicheEditee = AutoMapper.Mapper.Map(Of SpedinauteVM)(MaFiche)

            If Not MaFiche.Prenom Is Nothing Then
                SpedinauteConnecte = SpedinauteConnecte + MaFiche.Prenom.Trim
            End If
            If Not MaFiche.AutrePrenom Is Nothing Then
                SpedinauteConnecte = SpedinauteConnecte + " " + MaFiche.AutrePrenom.Trim
            End If
            If Not MaFiche.Nom Is Nothing Then
                SpedinauteConnecte = SpedinauteConnecte + " " + MaFiche.Nom.ToUpper.Trim
            End If

            Me.ComparerRib()
            If Not BAddRib Then
                MonRib = DbContext.RibIbans.Where(Function(s) s.IdSpedinaute = MaFiche.IdSpedinaute AndAlso s.Actuel = True).FirstOrDefault()
            End If

            Me.ComparerAdressePos()
            Me.ComparerAdresseFiscale()

        Else
            '   Spedinaute pas trouvé
            Return bTrouve

        End If

        Return bTrouve

    End Function


#End Region

#Region "NOTIFICATIONS"

    Public Sub RenvoidEmail(obj As Spedinaute)

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

        mail.Subject = "Envoi du mot de passe du " + DateTime.Today.ToShortDateString() + "."

        mail.Body = "<P>Bonjour "

        If Not obj.Prenom Is Nothing Then
            mail.Body += obj.Prenom.Trim() + "  "
        End If

        mail.Body += obj.Nom.Trim().ToUpper()
        mail.Body += "<br /> <br /> Suite a votre demande sur MySpedidam.fr, nous avons le plaisir de vous communiquer votre ancien mot de passe. <br /><br /> Mot de passe : "
        mail.Body += obj.PasseMot.Trim() + "<br></br>" + "<br></br>" + "Si vous n'êtes pas à l'origine de cette demande, avertissez nous en cliquant sur le "
        mail.Body += (lienAlerte & Convert.ToString(".")) + "<br></br>" + "<br></br>" + "LE SAVIEZ-VOUS ?" + "<br></br>" + "Les règlements ne sont émis " + "<u>que si votre adresse est valide.</u>"
        mail.Body += "<br></br>" + " En effet, vous devons nous assurer d'appliquer les bons prèlévements (prélèvements sociaux pour les résidents en France ou retenue à la source pour les résidents à l'étranger)."
        mail.Body += "<br></br>" + "<br></br>" + "la SPEDIDAM <u>arrête les paiements par chèque bancaire</u>.<br></br>Afin d'être réglé par virement bancaire et de faciliter le versemment rapide et sécurisé de votre répartition, vérifiez et "
        mail.Body += (Convert.ToString((Convert.ToString("renseignez si nécessaire vos coordonnées bancaires depuis ") & lienCompte) + "." + "<br></br>" + "<br></br>" + "Depuis ") & lienCompte) + ", vous pouvez modifier quand vous le souhaitez : "
        mail.Body += "<br></br>" + "- Votre mot de passe" + "<br></br>" + "- Vos coordonnées personnelles" + "<br></br>" + "<br></br>" + "Au plaisir de vous retrouver dans Mon Compte Artiste." + "<br></br>" + "<br></br>" + "Cordialement .</P>"

        smtpServer.Send(mail)

        smtpServer.Dispose()
        smtpServer = Nothing
        mail = Nothing

    End Sub

    Public Sub EnvoidEmail(obj As Spedinaute)

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

        mail.Subject = "Envoi du mot de passe du " + DateTime.Today.ToShortDateString() + "."

        mail.Body = "<P>Bonjour "

        If Not obj.Prenom Is Nothing Then
            mail.Body += obj.Prenom.Trim() + "  "
        End If

        mail.Body += obj.Nom.Trim().ToUpper()
        mail.Body += "<br /> <br /> Suite à votre session sur MySpedidam.fr, nous avons le plaisir de vous communiquer votre nouveau mot de passe. <br /><br /> Mot de passe : "
        mail.Body += obj.PasseMot.Trim() + "<br></br>" + "<br></br>" + "Si vous n'êtes pas à l'origine de cette modification, avertissez nous en cliquant sur le "
        mail.Body += (lienAlerte & Convert.ToString(".")) + "<br></br>" + "<br></br>" + "LE SAVIEZ-VOUS ?" + "<br></br>" + "Les règlements ne sont émis " + "<u>que si votre adresse est valide.</u>"
        mail.Body += "<br></br>" + " En effet, vous devons nous assurer d'appliquer les bons prèlévements (prélèvements sociaux pour les résidents en France ou retenue à la source pour les résidents à l'étranger)."
        mail.Body += "<br></br>" + "<br></br>" + "la SPEDIDAM <u>arrête les paiements par chèque bancaire</u>.<br></br>Afin d'être réglé par virement bancaire et de faciliter le versemment rapide et sécurisé de votre répartition, vérifiez et "
        mail.Body += (Convert.ToString((Convert.ToString("renseignez si nécessaire vos coordonnées bancaires depuis ") & lienCompte) + "." + "<br></br>" + "<br></br>" + "Depuis ") & lienCompte) + ", vous pouvez modifier quand vous le souhaitez : "
        mail.Body += "<br></br>" + "- Votre mot de passe" + "<br></br>" + "- Vos coordonnées personnelles" + "<br></br>" + "<br></br>" + "Au plaisir de vous retrouver dans Mon Compte Artiste." + "<br></br>" + "<br></br>" + "Cordialement .</P>"

        smtpServer.Send(mail)

        smtpServer.Dispose()
        smtpServer = Nothing
        mail = Nothing

    End Sub

    Public Sub RenvoiParMailDesErreurs(Logs As String)

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

        mail.Body = "<P>Logs Manager Erreurs"
        mail.Body += "<br /> <br />"
        mail.Body += Logs + "</P>"

        smtpServer.Send(mail)

        smtpServer.Dispose()
        smtpServer = Nothing
        mail = Nothing

    End Sub

#End Region

#Region "PERSISTANCE"



#End Region

#Region "REPORTING"



#End Region

#Region "COMPARER"

    '   Recherche des Mise A Jour dans Gespere en Comparant les champs
    Private Sub ComparerArtiste()

        Dim bAMettreAJour = False

        Try
            '   Si la Fiche Gespere est existante
            If Not SelectedAyantDroit Is Nothing Then

                If Not String.IsNullOrEmpty(SelectedAyantDroit.id_civil.NulleOuNon) Then
                    If Not String.IsNullOrEmpty(MaFiche.Civilite.NulleOuNon) Then
                        If Not MaFiche.Civilite.Trim().ToUpper.Equals(SelectedAyantDroit.id_civil.Trim().ToUpper) Then
                            MaFiche.Civilite = SelectedAyantDroit.id_civil.Trim().ToUpper
                            bAMettreAJour = True
                        End If
                    Else
                        MaFiche.Civilite = SelectedAyantDroit.id_civil.Trim().ToUpper
                        bAMettreAJour = True
                    End If
                Else
                    MaFiche.Civilite = String.Empty
                    bAMettreAJour = True
                End If

                If Not String.IsNullOrEmpty(SelectedAyantDroit.prenom.NulleOuNon) Then
                    If Not String.IsNullOrEmpty(MaFiche.Prenom.NulleOuNon) Then
                        If Not MaFiche.Prenom.Trim().ToUpper.Equals(SelectedAyantDroit.prenom.Trim().ToUpper) Then
                            MaFiche.Prenom = SelectedAyantDroit.prenom.Trim()
                            bAMettreAJour = True
                        End If
                    Else
                        MaFiche.Prenom = SelectedAyantDroit.prenom.Trim().ToUpper
                        bAMettreAJour = True
                    End If
                Else
                    MaFiche.Prenom = String.Empty
                    bAMettreAJour = True
                End If

                If Not String.IsNullOrEmpty(SelectedAyantDroit.prenom2.NulleOuNon) Then
                    If Not String.IsNullOrEmpty(MaFiche.AutrePrenom.NulleOuNon) Then
                        If Not MaFiche.AutrePrenom.Trim().ToUpper.Equals(SelectedAyantDroit.prenom2.Trim().ToUpper) Then
                            MaFiche.AutrePrenom = SelectedAyantDroit.prenom2.Trim()
                            bAMettreAJour = True
                        End If
                    Else
                        MaFiche.AutrePrenom = SelectedAyantDroit.prenom2.Trim().ToUpper
                        bAMettreAJour = True
                    End If
                Else
                    MaFiche.AutrePrenom = String.Empty
                    bAMettreAJour = True
                End If

                If Not String.IsNullOrEmpty(SelectedAyantDroit.nom.NulleOuNon) Then
                    If Not String.IsNullOrEmpty(MaFiche.Nom.NulleOuNon) Then
                        If Not MaFiche.Nom.Trim().ToUpper.Equals(SelectedAyantDroit.nom.Trim().ToUpper) Then
                            MaFiche.Nom = SelectedAyantDroit.nom.Trim()
                            bAMettreAJour = True
                        End If
                    Else
                        MaFiche.Nom = SelectedAyantDroit.nom.Trim().ToUpper
                        bAMettreAJour = True
                    End If
                Else
                    MaFiche.Nom = String.Empty
                    bAMettreAJour = True
                End If

                If Not String.IsNullOrEmpty(SelectedAyantDroit.nomJF.NulleOuNon) Then
                    If Not String.IsNullOrEmpty(MaFiche.NomDeJeuneFille.NulleOuNon) Then
                        If Not MaFiche.NomDeJeuneFille.Trim().ToUpper.Equals(SelectedAyantDroit.nomJF.Trim().ToUpper) Then
                            MaFiche.NomDeJeuneFille = SelectedAyantDroit.nomJF.Trim()
                            bAMettreAJour = True
                        End If
                    Else
                        MaFiche.NomDeJeuneFille = SelectedAyantDroit.nomJF.Trim().ToUpper
                        bAMettreAJour = True
                    End If
                Else
                    MaFiche.NomDeJeuneFille = String.Empty
                    bAMettreAJour = True
                End If

                If Not String.IsNullOrEmpty(SelectedAyantDroit.dt_naiss.NulleOuNon) Then
                    If Not String.IsNullOrEmpty(MaFiche.DateDeNaissance.NulleOuNon) Then
                        If Not MaFiche.DateDeNaissance.Equals(SelectedAyantDroit.dt_naiss) Then
                            MaFiche.DateDeNaissance = SelectedAyantDroit.dt_naiss
                            bAMettreAJour = True
                        End If
                    Else
                        MaFiche.DateDeNaissance = SelectedAyantDroit.dt_naiss
                        bAMettreAJour = True
                    End If
                Else
                    MaFiche.DateDeNaissance = SelectedAyantDroit.dt_naiss
                    bAMettreAJour = True
                End If

                If Not String.IsNullOrEmpty(SelectedAyantDroit.lieuNaiss.NulleOuNon) Then
                    If Not String.IsNullOrEmpty(MaFiche.LieuDeNaissance.NulleOuNon) Then
                        If Not MaFiche.LieuDeNaissance.Trim().ToUpper.Equals(SelectedAyantDroit.lieuNaiss.Trim().ToUpper) Then
                            MaFiche.LieuDeNaissance = SelectedAyantDroit.lieuNaiss.Trim()
                            bAMettreAJour = True
                        End If
                    Else
                        MaFiche.LieuDeNaissance = SelectedAyantDroit.lieuNaiss.Trim().ToUpper
                        bAMettreAJour = True
                    End If
                Else
                    MaFiche.LieuDeNaissance = String.Empty
                    bAMettreAJour = True
                End If

                If Not String.IsNullOrEmpty(SelectedAyantDroit.id_pays.NulleOuNon) Then
                    If Not String.IsNullOrEmpty(MaFiche.Nationalite.NulleOuNon) Then
                        If Not MaFiche.Nationalite.Trim().ToUpper.Equals(SelectedAyantDroit.id_pays.Trim().ToUpper) Then
                            MaFiche.Nationalite = SelectedAyantDroit.id_pays.Trim()
                            bAMettreAJour = True
                        End If
                    Else
                        MaFiche.Nationalite = SelectedAyantDroit.id_pays.Trim().ToUpper
                        bAMettreAJour = True
                    End If
                Else
                    MaFiche.Nationalite = String.Empty
                    bAMettreAJour = True
                End If

                If Not String.IsNullOrEmpty(SelectedAyantDroit.no_ipd.NulleOuNon) Then
                    If Not String.IsNullOrEmpty(MaFiche.IPN.NulleOuNon) Then
                        If Not MaFiche.IPN.Trim().ToUpper.Equals(SelectedAyantDroit.no_ipd.Trim().ToUpper) Then
                            MaFiche.IPN = SelectedAyantDroit.no_ipd.Trim()
                            bAMettreAJour = True
                        End If
                    Else
                        MaFiche.IPN = SelectedAyantDroit.no_ipd.Trim().ToUpper
                        bAMettreAJour = True
                    End If
                Else
                    MaFiche.IPN = String.Empty
                    bAMettreAJour = True
                End If

                'If Not String.IsNullOrEmpty(MaFiche.Instrument1.NulleOuNon) Then
                '    If Not SelectedAyantDroit.id1Instr Is Nothing Then
                '        If Not String.IsNullOrEmpty(SelectedAyantDroit.id1Instr.NulleOuNon) Then
                '            If Not MaFiche.Instrument1.ToString.Equals(SelectedAyantDroit.id1Instr) Then
                '                MaFiche.Instrument1 = Integer.Parse(SelectedAyantDroit.id1Instr)
                '                bAMettreAJour = True
                '            End If
                '        End If
                '    End If
                'Else
                '    'If Not SelectedAyantDroit.id1Instr Is Nothing Then
                '    If Not String.IsNullOrEmpty(SelectedAyantDroit.id1Instr.NulleOuNon) Then
                '        MaFiche.Instrument1 = Integer.Parse(SelectedAyantDroit.id1Instr)
                '        bAMettreAJour = True
                '    End If
                '    'End If
                'End If

                If Not String.IsNullOrEmpty(SelectedAyantDroit.id1Instr.NulleOuNon) Then
                    If Not String.IsNullOrEmpty(MaFiche.Instrument1.NulleOuNon) Then
                        If Not MaFiche.Instrument1.ToString.Trim().Equals(SelectedAyantDroit.id1Instr.ToString.Trim()) Then
                            MaFiche.Instrument1 = Integer.Parse(SelectedAyantDroit.id1Instr)
                            bAMettreAJour = True
                        End If
                    Else
                        MaFiche.Instrument1 = Integer.Parse(SelectedAyantDroit.id1Instr)
                        bAMettreAJour = True
                    End If
                Else
                    MaFiche.Instrument1 = 114
                    bAMettreAJour = True
                End If

                If Not String.IsNullOrEmpty(SelectedAyantDroit.id2Instr.NulleOuNon) Then
                    If Not String.IsNullOrEmpty(MaFiche.Instrument2.NulleOuNon) Then
                        If Not MaFiche.Instrument2.ToString.Trim().Equals(SelectedAyantDroit.id2Instr.ToString.Trim()) Then
                            MaFiche.Instrument2 = Integer.Parse(SelectedAyantDroit.id2Instr)
                            bAMettreAJour = True
                        End If
                    Else
                        MaFiche.Instrument2 = Integer.Parse(SelectedAyantDroit.id2Instr)
                        bAMettreAJour = True
                    End If
                Else
                    MaFiche.Instrument2 = 114
                    bAMettreAJour = True
                End If

                If Not String.IsNullOrEmpty(SelectedAyantDroit.id3Instr.NulleOuNon) Then
                    If Not String.IsNullOrEmpty(MaFiche.Instrument3.NulleOuNon) Then
                        If Not MaFiche.Instrument3.ToString.Trim().Equals(SelectedAyantDroit.id3Instr.ToString.Trim()) Then
                            MaFiche.Instrument3 = Integer.Parse(SelectedAyantDroit.id3Instr)
                            bAMettreAJour = True
                        End If
                    Else
                        MaFiche.Instrument3 = Integer.Parse(SelectedAyantDroit.id3Instr)
                        bAMettreAJour = True
                    End If
                Else
                    MaFiche.Instrument3 = 114
                    bAMettreAJour = True
                End If

                '  Pseudonyme Principal  - Ancienne version
                'If Not String.IsNullOrEmpty(MaFiche.Pseudonyme1.NulleOuNon) Then
                '    If Not String.IsNullOrEmpty(SelectedAyantDroit.pseudo1.NulleOuNon) Then
                '        If Not MaFiche.Pseudonyme1.Equals(SelectedAyantDroit.pseudo1) Then
                '            MaFiche.Pseudonyme1 = SelectedAyantDroit.pseudo1
                '            bAMettreAJour = True
                '        End If
                '    End If
                'Else
                '    If Not String.IsNullOrEmpty(SelectedAyantDroit.pseudo1.NulleOuNon) Then
                '        MaFiche.Pseudonyme1 = SelectedAyantDroit.pseudo1.Trim()
                '        bAMettreAJour = True
                '    End If
                'End If

                '  Pseudonyme Principal  - Nouvelle version
                If Not String.IsNullOrEmpty(SelectedAyantDroit.pseudo1.NulleOuNon) Then
                    If Not String.IsNullOrEmpty(MaFiche.Pseudonyme1.NulleOuNon) Then
                        If Not MaFiche.Pseudonyme1.Trim().ToUpper.Equals(SelectedAyantDroit.pseudo1.Trim().ToUpper) Then
                            MaFiche.Pseudonyme1 = SelectedAyantDroit.pseudo1.Trim().ToUpper
                            bAMettreAJour = True
                        End If
                    Else
                        MaFiche.Pseudonyme1 = SelectedAyantDroit.pseudo1.Trim().ToUpper
                        bAMettreAJour = True
                    End If
                Else
                    MaFiche.Pseudonyme1 = String.Empty
                    bAMettreAJour = True
                End If

                If Not String.IsNullOrEmpty(SelectedAyantDroit.pseudo2.NulleOuNon) Then
                    If Not String.IsNullOrEmpty(MaFiche.Pseudonyme2.NulleOuNon) Then
                        If Not MaFiche.Pseudonyme2.Trim().ToUpper.Equals(SelectedAyantDroit.pseudo2.Trim().ToUpper) Then
                            MaFiche.Pseudonyme2 = SelectedAyantDroit.pseudo2.Trim().ToUpper
                            bAMettreAJour = True
                        End If
                    Else
                        MaFiche.Pseudonyme2 = SelectedAyantDroit.pseudo2.Trim().ToUpper
                        bAMettreAJour = True
                    End If
                Else
                    MaFiche.Pseudonyme2 = String.Empty
                    bAMettreAJour = True
                End If

                If Not String.IsNullOrEmpty(SelectedAyantDroit.pseudo3.NulleOuNon) Then
                    If Not String.IsNullOrEmpty(MaFiche.Pseudonyme3.NulleOuNon) Then
                        If Not MaFiche.Pseudonyme3.Trim().ToUpper.Equals(SelectedAyantDroit.pseudo3.Trim().ToUpper) Then
                            MaFiche.Pseudonyme3 = SelectedAyantDroit.pseudo3.Trim().ToUpper
                            bAMettreAJour = True
                        End If
                    Else
                        MaFiche.Pseudonyme3 = SelectedAyantDroit.pseudo3.Trim().ToUpper
                        bAMettreAJour = True
                    End If
                Else
                    MaFiche.Pseudonyme3 = String.Empty
                    bAMettreAJour = True
                End If

                '   Données provenant du bénéficiaire
                If SelectedBeneficiaire IsNot Nothing Then

                    If Not String.IsNullOrEmpty(SelectedBeneficiaire.no_telfi.NulleOuNon) Then
                        If Not String.IsNullOrEmpty(MaFiche.NumeroDeFixe.NulleOuNon) Then
                            If Not MaFiche.NumeroDeFixe.Trim().ToUpper.Equals(SelectedBeneficiaire.no_telfi.Trim().ToUpper) Then
                                MaFiche.NumeroDeFixe = SelectedBeneficiaire.no_telfi.Trim().ToUpper
                                bAMettreAJour = True
                            End If
                        Else
                            MaFiche.NumeroDeFixe = SelectedBeneficiaire.no_telfi.Trim().ToUpper
                            bAMettreAJour = True
                        End If
                    Else
                        MaFiche.NumeroDeFixe = String.Empty
                        bAMettreAJour = True
                    End If

                    If Not String.IsNullOrEmpty(SelectedBeneficiaire.no_telpo.NulleOuNon) Then
                        If Not String.IsNullOrEmpty(MaFiche.NumeroDeMobile.NulleOuNon) Then
                            If Not MaFiche.NumeroDeMobile.Trim().ToUpper.Equals(SelectedBeneficiaire.no_telpo.Trim().ToUpper) Then
                                MaFiche.NumeroDeMobile = SelectedBeneficiaire.no_telpo.Trim().ToUpper
                                bAMettreAJour = True
                            End If
                        Else
                            MaFiche.NumeroDeMobile = SelectedBeneficiaire.no_telpo.Trim().ToUpper
                            bAMettreAJour = True
                        End If
                    Else
                        MaFiche.NumeroDeMobile = String.Empty
                        bAMettreAJour = True
                    End If

                    If Not String.IsNullOrEmpty(SelectedBeneficiaire.mele.NulleOuNon) Then
                        If Not String.IsNullOrEmpty(MaFiche.AdresseEmail.NulleOuNon) Then
                            If Not MaFiche.AdresseEmail.Trim().ToUpper.Equals(SelectedBeneficiaire.mele.Trim().ToUpper) Then
                                MaFiche.AdresseEmail = SelectedBeneficiaire.mele.Trim().ToUpper
                                bAMettreAJour = True
                            End If
                        Else
                            MaFiche.AdresseEmail = SelectedBeneficiaire.mele.Trim().ToUpper
                            bAMettreAJour = True
                        End If
                    Else
                        MaFiche.AdresseEmail = String.Empty
                        bAMettreAJour = True
                    End If

                End If

                '   SAUVEGARDE DE LA FICHE MISE A JOUR
                If bAMettreAJour Then
                    DbContext.Entry(MaFiche).State = EntityState.Modified
                    DbContext.SaveChanges()
                End If

            End If

        Catch ex As Exception
            ValeursErreurs = ValeursErreurs + " fn ComparerArtiste " + ex.Message
            RenvoiParMailDesErreurs(ValeursErreurs)
        End Try

    End Sub

    '    Recherche des Mise a Jour dans Beneficiaire (tel fixe, telmobile, Email) 
    Private Sub ComparerRib()

        Dim bAMettreAJour = False

        Try

            If SelectedBeneficiaire IsNot Nothing Then

                '   Met à Jour le Rib Existant depuis Gespere_Prod
                If Not BAddRib Then

                    Dim testRib = String.Empty

                    testRib = MonRib.BicSwift.NulleOuNon + " "
                    testRib = testRib + MonRib.NumeroIBAN.NulleOuNon + " "
                    testRib = testRib + MonRib.Domiciliation.NulleOuNon + " "
                    testRib = testRib + MonRib.Id_pays.NulleOuNon + " "
                    testRib = testRib + MonRib.Guichet.NulleOuNon + " "
                    testRib = testRib + MonRib.NumeroCompte.NulleOuNon + " "
                    testRib = testRib + MonRib.CodeBanque.NulleOuNon.Trim

                    '	Toujours Vérifier les vaLeurs Nulle et Espace Blanc créées par Gespère
                    Dim szCle As String = String.Empty

                    If Not SelectedBeneficiaire.bic Is Nothing AndAlso SelectedBeneficiaire.bic.Length > 0 Then szCle = szCle + SelectedBeneficiaire.bic.Trim + " "
                    If Not SelectedBeneficiaire.iban Is Nothing AndAlso SelectedBeneficiaire.iban.Length > 0 Then szCle = szCle + SelectedBeneficiaire.iban.Trim + " "
                    If Not SelectedBeneficiaire.lb_domic Is Nothing AndAlso SelectedBeneficiaire.lb_domic.Length > 0 Then szCle = szCle + SelectedBeneficiaire.lb_domic.Trim + " "
                    If Not SelectedBeneficiaire.paybanqu Is Nothing AndAlso SelectedBeneficiaire.paybanqu.Length > 0 Then szCle = szCle + SelectedBeneficiaire.paybanqu.Trim + " "
                    If Not SelectedBeneficiaire.co_guich Is Nothing AndAlso SelectedBeneficiaire.co_guich.Length > 0 Then szCle = szCle + SelectedBeneficiaire.co_guich.Trim + " "
                    If Not SelectedBeneficiaire.co_compt Is Nothing AndAlso SelectedBeneficiaire.co_compt.Length > 0 Then szCle = szCle + SelectedBeneficiaire.co_compt.Trim + " "
                    If Not SelectedBeneficiaire.co_banqu Is Nothing AndAlso SelectedBeneficiaire.co_banqu.Length > 0 Then szCle = szCle + SelectedBeneficiaire.co_banqu.Trim

                    '   SI PAS DE CONCORDANCE - MET A JOUR L'ADRESSE EXISTANTE DANS LA BASE WEB DEPUIS L'ADRESSE GESPERE
                    If Not testRib.Trim.Equals(szCle.Trim) Then

                        MonRib.Actuel = vbTrue
                        MonRib.IdSpedinaute = IdSpedinaute
                        MonRib.DateDeSaisie = Today
                        MonRib.IntegreDansBachON = True
                        MonRib.BicSwift = SelectedBeneficiaire.bic.NulleOuNon
                        MonRib.NumeroIBAN = SelectedBeneficiaire.iban.NulleOuNon
                        MonRib.Id_pays = SelectedBeneficiaire.paybanqu.NulleOuNon
                        MonRib.Guichet = SelectedBeneficiaire.co_guich.NulleOuNon
                        MonRib.NumeroCompte = SelectedBeneficiaire.co_compt.NulleOuNon
                        MonRib.CodeBanque = SelectedBeneficiaire.co_banqu.NulleOuNon
                        MonRib.Domiciliation = SelectedBeneficiaire.lb_domic.NulleOuNon

                        DbContext.Entry(MonRib).State = EntityState.Modified
                        DbContext.SaveChanges()

                    End If

                Else

                    '   Remonte le Rib de Gespere et le cree dans la base Web

                    BAddRib = False

                    MonRib = New RibIban

                    MonRib.Actuel = vbTrue
                    MonRib.IdSpedinaute = IdSpedinaute
                    MonRib.DateDeSaisie = Today
                    MonRib.IntegreDansBachON = True
                    MonRib.BicSwift = SelectedBeneficiaire.bic.NulleOuNon
                    MonRib.NumeroIBAN = SelectedBeneficiaire.iban.NulleOuNon
                    MonRib.Id_pays = SelectedBeneficiaire.paybanqu.NulleOuNon
                    MonRib.Guichet = SelectedBeneficiaire.co_guich.NulleOuNon
                    MonRib.NumeroCompte = SelectedBeneficiaire.co_compt.NulleOuNon
                    MonRib.CodeBanque = SelectedBeneficiaire.co_banqu.NulleOuNon
                    MonRib.Domiciliation = SelectedBeneficiaire.lb_domic.NulleOuNon

                    DbContext.RibIbans.Add(MonRib)
                    DbContext.SaveChanges()

                End If

            End If

        Catch ex As Exception
            ValeursErreurs = ValeursErreurs + " fn ComparerRib " + ex.Message
            RenvoiParMailDesErreurs(ValeursErreurs)
        End Try

    End Sub

    ''' <summary>
    ''' Cette Methode permet de Comparer les adresses Postales locales et Web, mettre à Jour ou créer Si besoin
    ''' </summary>
    Private Sub ComparerAdressePos()

        Try
            '   Si l'adresse Fiscale est existante dans la Base Distante Gespere_Prod
            If SelectedAdresGespere IsNot Nothing Then

                Dim bAMettreAJour = False
                Dim prefixeLibelleAdresse = String.Empty
                Dim vPos = String.Empty

                vPos = SelectedAdresGespere.id_typvo.NulleOuNon
                Dim rqPos = DbContext.TypeDeVoies.Where(Function(s) s.TypeDeVoie = vPos).FirstOrDefault()
                If Not rqPos Is Nothing Then
                    vPos = rqPos.LbelleVoie
                End If

                '   Si l'adresse Postale est existante dans la Base WEB Spedinaute
                If Not bAddPos Then
                    '   REVERIFICATION DE CONCORDANCE ENTRE L'ADRESSE ACTIVE ET LA DERNIERE ADRESSE
                    Dim testAdresse = String.Empty

                    testAdresse = MonAdressePos.LibelleVoie.NulleOuNon + " "
                    testAdresse = testAdresse + MonAdressePos.Adresse2.NulleOuNon + " "
                    testAdresse = testAdresse + MonAdressePos.CodePostal.NulleOuNon + " "
                    testAdresse = testAdresse + MonAdressePos.Ville.NulleOuNon + " "
                    testAdresse = testAdresse + MonAdressePos.Id_pays.NulleOuNon.Trim

                    ' Toujours Vérifier les vaLeurs Nulle et Espace Blanc créées par Gespère
                    Dim szCle As String = String.Empty

                    If Not SelectedAdresGespere.no_voie Is Nothing AndAlso SelectedAdresGespere.no_voie.Length > 0 Then szCle = szCle + SelectedAdresGespere.no_voie.Trim + " "
                    If Not SelectedAdresGespere.id_typvo Is Nothing AndAlso SelectedAdresGespere.id_typvo.Length > 0 Then szCle = szCle + vPos + " "
                    If Not SelectedAdresGespere.lb_voie Is Nothing AndAlso SelectedAdresGespere.lb_voie.Length > 0 Then szCle = szCle + SelectedAdresGespere.lb_voie.Trim + " "
                    If Not SelectedAdresGespere.codposta Is Nothing AndAlso SelectedAdresGespere.codposta.Length > 0 Then szCle = szCle + SelectedAdresGespere.codposta.Trim + " "
                    If Not SelectedAdresGespere.ville Is Nothing AndAlso SelectedAdresGespere.ville.Length > 0 Then szCle = szCle + SelectedAdresGespere.ville.Trim
                    If Not SelectedAdresGespere.id_pays Is Nothing AndAlso SelectedAdresGespere.id_pays.Length > 0 Then szCle = szCle + SelectedAdresGespere.id_pays.Trim

                    '   SI PAS DE CONCORDANCE - MET A JOUR L'ADRESSE EXISTANTE DANS LA BASE WEB DEPUIS L'ADRESSE GESPERE
                    If Not testAdresse.Trim.Equals(szCle.Trim) Then

                        MonAdressePos.Actuelle = vbTrue
                        MonAdressePos.IdSpedinaute = IdSpedinaute
                        MonAdressePos.DateSaisie = Today
                        MonAdressePos.IntegreDansBachON = True
                        MonAdressePos.Numero = vbNull
                        MonAdressePos.TypeDeVoie = vbNull
                        MonAdressePos.TypeAdresse = "POS"
                        MonAdressePos.LibelleVoie = SelectedAdresGespere.no_voie.NulleOuNon + " " + vPos + " " + SelectedAdresGespere.lb_voie.NulleOuNon
                        MonAdressePos.Adresse2 = SelectedAdresGespere.adr2.NulleOuNon
                        MonAdressePos.CodePostal = SelectedAdresGespere.codposta.NulleOuNon
                        MonAdressePos.Ville = SelectedAdresGespere.ville.NulleOuNon
                        MonAdressePos.Id_pays = SelectedAdresGespere.id_pays.NulleOuNon

                        DbContext.Entry(MonAdressePos).State = EntityState.Modified
                        DbContext.SaveChanges()

                    End If

                Else

                    '   Récupération des Données depuis la base Gespere en Creant une nouvelle Adresse postale
                    MonAdressePos = New SpedAdresse

                    MonAdressePos.Actuelle = vbTrue
                    MonAdressePos.IdSpedinaute = IdSpedinaute
                    MonAdressePos.DateSaisie = Today
                    MonAdressePos.IntegreDansBachON = True
                    MonAdressePos.TypeAdresse = "POS"
                    MonAdressePos.LibelleVoie = SelectedAdresGespere.no_voie.NulleOuNon + " " + vPos + " " + SelectedAdresGespere.lb_voie.NulleOuNon
                    MonAdressePos.Adresse2 = SelectedAdresGespere.adr2.NulleOuNon
                    MonAdressePos.CodePostal = SelectedAdresGespere.codposta.NulleOuNon
                    MonAdressePos.Ville = SelectedAdresGespere.ville.NulleOuNon
                    MonAdressePos.Id_pays = SelectedAdresGespere.id_pays.NulleOuNon

                    DbContext.SpedAdresses.Add(MonAdressePos)
                    DbContext.SaveChanges()

                End If

                MonAdressePos = GetTMonAdresseVraie(IdSpedinaute, True)

            End If

        Catch ex As Exception
            ValeursErreurs = ValeursErreurs + " fn ComparerAdressePos " + ex.Message
            RenvoiParMailDesErreurs(ValeursErreurs)
        End Try

    End Sub

    ''' <summary>
    ''' Cette Methode permet de Comparer les adresses Fiscales locales et Web, mettre à Jour ou créer Si besoin
    ''' </summary>
    Private Sub ComparerAdresseFiscale()

        Try
            '   Si l'adresse Fiscale est existante dans la Base Distante Gespere_Prod
            If SelectedAdresGespereFis IsNot Nothing Then

                Dim bAMettreAJour = False
                Dim prefixeLibelleAdresse = String.Empty
                Dim vPos = String.Empty

                vPos = SelectedAdresGespereFis.id_typvo.NulleOuNon
                Dim rqPos = DbContext.TypeDeVoies.Where(Function(s) s.TypeDeVoie = vPos).FirstOrDefault()
                If Not rqPos Is Nothing Then
                    vPos = rqPos.LbelleVoie
                End If

                '   Si l'adresse Postale est existante dans la Base WEB Spedinaute
                If Not bAddFis Then

                    '   REVERIFICATION DE CONCORDANCE ENTRE L'ADRESSE ACTIVE ET LA DERNIERE ADRESSE
                    Dim testAdresse = String.Empty

                    testAdresse = MonAdresseFis.LibelleVoie.NulleOuNon + " "
                    testAdresse = testAdresse + MonAdresseFis.Adresse2.NulleOuNon + " "
                    testAdresse = testAdresse + MonAdresseFis.CodePostal.NulleOuNon + " "
                    testAdresse = testAdresse + MonAdresseFis.Ville.NulleOuNon + " "
                    testAdresse = testAdresse + MonAdresseFis.Id_pays.NulleOuNon.Trim

                    ' Toujours Vérifier les vaLeurs Nulle et Espace Blanc créées par Gespère
                    Dim szCle As String = String.Empty

                    If Not SelectedAdresGespereFis.no_voie Is Nothing AndAlso SelectedAdresGespereFis.no_voie.Length > 0 Then szCle = szCle + SelectedAdresGespereFis.no_voie.Trim + " "
                    If Not SelectedAdresGespereFis.id_typvo Is Nothing AndAlso SelectedAdresGespereFis.id_typvo.Length > 0 Then szCle = szCle + vPos + " "
                    If Not SelectedAdresGespereFis.lb_voie Is Nothing AndAlso SelectedAdresGespereFis.lb_voie.Length > 0 Then szCle = szCle + SelectedAdresGespereFis.lb_voie.Trim + " "
                    If Not SelectedAdresGespereFis.codposta Is Nothing AndAlso SelectedAdresGespereFis.codposta.Length > 0 Then szCle = szCle + SelectedAdresGespereFis.codposta.Trim + " "
                    If Not SelectedAdresGespereFis.ville Is Nothing AndAlso SelectedAdresGespereFis.ville.Length > 0 Then szCle = szCle + SelectedAdresGespereFis.ville.Trim
                    If Not SelectedAdresGespereFis.id_pays Is Nothing AndAlso SelectedAdresGespereFis.id_pays.Length > 0 Then szCle = szCle + SelectedAdresGespereFis.id_pays.Trim

                    '   SI PAS DE CONCORDANCE - MET A JOUR L'ADRESSE EXISTANTE DANS LA BASE WEB DEPUIS L'ADRESSE GESPERE
                    If Not testAdresse.Trim.Equals(szCle.Trim) Then

                        MonAdresseFis.Actuelle = vbTrue
                        MonAdresseFis.IdSpedinaute = IdSpedinaute
                        MonAdresseFis.DateSaisie = Today
                        MonAdresseFis.IntegreDansBachON = True
                        MonAdresseFis.Numero = vbNull
                        MonAdresseFis.TypeDeVoie = vbNull
                        MonAdresseFis.TypeAdresse = "FIS"
                        MonAdresseFis.LibelleVoie = SelectedAdresGespereFis.no_voie.NulleOuNon + " " + vPos + " " + SelectedAdresGespereFis.lb_voie.NulleOuNon
                        MonAdresseFis.Adresse2 = SelectedAdresGespereFis.adr2.NulleOuNon
                        MonAdresseFis.CodePostal = SelectedAdresGespereFis.codposta.NulleOuNon
                        MonAdresseFis.Ville = SelectedAdresGespereFis.ville.NulleOuNon
                        MonAdresseFis.Id_pays = SelectedAdresGespereFis.id_pays.NulleOuNon

                        DbContext.Entry(MonAdresseFis).State = EntityState.Modified
                        DbContext.SaveChanges()

                    End If

                Else

                    '   Récupération des Données depuis la base Gespere en Creant une nouvelle Adresse postale
                    MonAdresseFis = New SpedAdresse

                    MonAdresseFis.Actuelle = vbTrue
                    MonAdresseFis.IdSpedinaute = IdSpedinaute
                    MonAdresseFis.DateSaisie = Today
                    MonAdresseFis.IntegreDansBachON = True
                    MonAdresseFis.TypeAdresse = "FIS"
                    MonAdresseFis.LibelleVoie = SelectedAdresGespereFis.no_voie.NulleOuNon + " " + vPos + " " + SelectedAdresGespereFis.lb_voie.NulleOuNon
                    MonAdresseFis.Adresse2 = SelectedAdresGespereFis.adr2.NulleOuNon
                    MonAdresseFis.CodePostal = SelectedAdresGespereFis.codposta.NulleOuNon
                    MonAdresseFis.Ville = SelectedAdresGespereFis.ville.NulleOuNon
                    MonAdresseFis.Id_pays = SelectedAdresGespereFis.id_pays.NulleOuNon

                    DbContext.SpedAdresses.Add(MonAdresseFis)
                    DbContext.SaveChanges()

                End If

                MonAdresseFis = GetTMonAdresseVraie(IdSpedinaute, False)

            End If

        Catch ex As Exception
            ValeursErreurs = ValeursErreurs + " fn ComparerAdresseFis " + ex.Message
            RenvoiParMailDesErreurs(ValeursErreurs)
        End Try
    End Sub

#End Region

#Region "METHODES PARTAGEES"

    Sub SeConnecterAFacilDB()

        bDBOk = False

        Try

            DbContext = New FacilDBEntities()
            If Not DbContext Is Nothing Then

                bDBOk = True

                Dim rqI = From objInstrument As Instrument In DbContext.Instruments Select objInstrument
                ListeDesIntruments = rqI.ToList

                ListeDesIntruments = (From obj As Instrument In ListeDesIntruments Order By obj.lb_instr).ToList

                Dim rqN = From objPays As Pays In DbContext.Pays Order By objPays.id_pays Select objPays
                ListeDesNationalites = rqN.ToList

                Dim rqT = From objType As TypeAdresses In DbContext.TypeAdresses Select objType
                ListeDesTypesAdresses = rqT.ToList

                Dim rqV = From objType As TypeDeVoies In DbContext.TypeDeVoies Select objType
                ListeDesTypeDeVoies = rqV.ToList

                Dim rqA = From objAnnee As DeclarationFiscale In DbContext.DeclarationFiscales Select objAnnee.Annee Distinct

                ListeDesAnneesFiscales = New List(Of AnneeFiscale)
                ListeDesAnneesFiscales.Add(New AnneeFiscale)

                For Each oLigne As String In rqA

                    Dim uneAnnee = New AnneeFiscale
                    uneAnnee.Annee = oLigne.NulleOuNon
                    uneAnnee.Libelle = "Déclaration " + (Integer.Parse(oLigne) + 1).NulleOuNon + " Revenus " + oLigne.NulleOuNon

                    ListeDesAnneesFiscales.Add(uneAnnee)

                Next

            End If

        Catch e As Exception

            If Not bDBOk Then
                ValeursErreurs = "Erreur de Connexion à la base de données."
            End If

            ValeursErreurs = ValeursErreurs + e.Message + Chr(13)
            RenvoiParMailDesErreurs(ValeursErreurs)

        End Try

    End Sub

    Sub SeConnecterAHarmonie()

        bDBOk2 = False

        Try

            DbCtx = New OurDBEntities()

            If Not DbCtx Is Nothing Then
                bDBOk2 = True
            End If

        Catch e As Exception

            If Not bDBOk2 Then
                ValeursErreurs = ValeursErreurs + "Erreur de Connexion à la base de données."
            End If

            ValeursErreurs = ValeursErreurs + e.Message + Chr(13)
            RenvoiParMailDesErreurs(ValeursErreurs)

        End Try

    End Sub

    Public Function GetNationalite(ByVal seCodePays As String) As String

        Dim szData As String = seCodePays

        If Not seCodePays Is Nothing Then
            If Not ListeDesNationalites Is Nothing AndAlso ListeDesNationalites.Any Then
                Dim rqN = From obj In ListeDesNationalites Where obj.id_pays.Trim().Equals(seCodePays)
                If Not rqN Is Nothing AndAlso rqN.Any Then
                    szData = rqN.ToList().FirstOrDefault.lb_pays.Trim
                End If
            End If
        End If

        Return szData

    End Function

    Public Function GetInstrument(ByVal iCodeInstrument As Integer) As String

        Dim szData As String = String.Empty

        If iCodeInstrument <> 0 Then
            If Not ListeDesIntruments Is Nothing AndAlso ListeDesIntruments.Any Then

                Dim rqN = From obj In ListeDesIntruments Where obj.id_instr = iCodeInstrument
                If Not rqN Is Nothing AndAlso rqN.Any Then
                    szData = rqN.ToList().FirstOrDefault.lb_instr.Trim()
                End If

            End If
        End If
        Return szData

    End Function

    Public Function GetTypeAdresse(ByVal seCodePays As String) As String

        Dim szData As String = seCodePays
        If Not seCodePays Is Nothing Then

            If Not ListeDesTypesAdresses Is Nothing AndAlso ListeDesTypesAdresses.Any Then

                Dim rqN = From obj In ListeDesTypesAdresses Where obj.TypeAdresse.Trim() = seCodePays
                If Not rqN Is Nothing AndAlso rqN.Any Then
                    szData = rqN.ToList().FirstOrDefault.LibelleAdresse.Trim
                End If

            End If

        End If

        Return szData

    End Function

    Public Function GetTypeVoie(ByVal seCodePays As String) As String

        Dim szData As String = seCodePays
        If Not seCodePays Is Nothing Then

            If Not ListeDesTypeDeVoies Is Nothing AndAlso ListeDesTypeDeVoies.Any Then

                Dim rqN = From obj In ListeDesTypeDeVoies Where obj.TypeDeVoie.Trim() = seCodePays
                If Not rqN Is Nothing AndAlso rqN.Any Then
                    szData = rqN.ToList().FirstOrDefault.LbelleVoie.Trim
                End If

            End If

        End If

        Return szData

    End Function

    Public Function GetTMonAdresse(ByVal idSpedinaute As Integer, szType As Boolean) As SpedAdresseVM

        Dim szData As SpedAdresseVM = Nothing

        If idSpedinaute > 0 Then

            Dim rql As IEnumerable(Of SpedAdresse)
            If szType = True Then
                rql = From objSpedAdresse As SpedAdresse In DbContext.SpedAdresses
                      Where (objSpedAdresse.IdSpedinaute = idSpedinaute) And objSpedAdresse.Actuelle _
                          And objSpedAdresse.TypeAdresse.Trim.Equals("POS")
                      Select objSpedAdresse
            Else
                rql = From objSpedAdresse As SpedAdresse In DbContext.SpedAdresses
                      Where (objSpedAdresse.IdSpedinaute = idSpedinaute) And objSpedAdresse.Actuelle _
                          And objSpedAdresse.TypeAdresse.Trim.Equals("FIS")
                      Select objSpedAdresse
            End If

            If Not rql Is Nothing AndAlso rql.Any Then
                szData = AutoMapper.Mapper.Map(Of SpedAdresseVM)(rql.ToList.LastOrDefault)

                szData.MonTypeDAdresse = GetTypeAdresse(szData.TypeAdresse.Trim)
                szData.MonTypeDeVoie = GetTypeVoie(szData.TypeDeVoie.Trim)
                szData.MonPays = GetNationalite(szData.Pays.Trim)

                SpedAdresseActuel = rql.ToList.LastOrDefault
                SpedAdresseEdite = szData

            End If

        End If

        Return szData

    End Function

    Public Function GetTMonAdresseVraie(ByVal idSpedinaute As Integer, szType As Boolean) As SpedAdresse

        Dim szData As SpedAdresse = Nothing

        If idSpedinaute > 0 Then

            Dim rql As IEnumerable(Of SpedAdresse)
            If szType = True Then
                rql = From objSpedAdresse As SpedAdresse In DbContext.SpedAdresses
                      Where (objSpedAdresse.IdSpedinaute = idSpedinaute) And objSpedAdresse.Actuelle _
                          And objSpedAdresse.TypeAdresse.Trim.Equals("POS")
                      Select objSpedAdresse

                If Not rql Is Nothing AndAlso rql.Any Then
                    szData = rql.ToList.LastOrDefault
                    SpedAdresseActuel = rql.ToList.LastOrDefault
                End If

            Else
                rql = From objSpedAdresse As SpedAdresse In DbContext.SpedAdresses
                      Where (objSpedAdresse.IdSpedinaute = idSpedinaute) And objSpedAdresse.Actuelle _
                          And objSpedAdresse.TypeAdresse.Trim.Equals("FIS")
                      Select objSpedAdresse

                If Not rql Is Nothing AndAlso rql.Any Then
                    szData = rql.ToList.LastOrDefault
                    SpedAdresseActuelFis = rql.ToList.LastOrDefault
                End If

            End If

        End If

        Return szData

    End Function

    Public Function GetTRibVM(ByVal idSpedinaute As Integer, Optional szTitulaire As String = "") As RibIbanVM

        Dim szData As RibIbanVM = Nothing

        If idSpedinaute > 0 Then

            Dim rql As IEnumerable(Of RibIban)

            rql = From objRibIban As RibIban In DbContext.RibIbans
                  Where (objRibIban.IdSpedinaute = idSpedinaute) And objRibIban.Actuel
                  Select objRibIban

            If Not rql Is Nothing AndAlso rql.Any Then

                MonRib = rql.ToList.FirstOrDefault
                MonRib.Titulaire = szTitulaire
                szData = AutoMapper.Mapper.Map(Of RibIbanVM)(MonRib)

            End If

        End If

        Return szData

    End Function

    Public Function GetTVraiRib(ByVal idSpedinaute As Integer, Optional szTitulaire As String = "") As RibIban

        Dim szData As RibIban = Nothing

        If idSpedinaute > 0 Then

            Dim rql As IEnumerable(Of RibIban)

            rql = From objRibIban As RibIban In DbContext.RibIbans
                  Where (objRibIban.IdSpedinaute = idSpedinaute) And objRibIban.Actuel
                  Select objRibIban

            If Not rql Is Nothing AndAlso rql.Any Then

                MonRib = rql.ToList.FirstOrDefault
                MonRib.Titulaire = szTitulaire

                szData = MonRib

            End If

        End If

        Return szData

    End Function

    Public Function GetAssemblees() As Assemblee

        Dim szData As Assemblee = Nothing
        Dim rql As IEnumerable(Of Assemblee)

        rql = From objAssemblee As Assemblee In DbContext.Assemblees
              Where (objAssemblee.Actuelle = True)
              Select objAssemblee

        If Not rql Is Nothing AndAlso rql.Any Then
            szData = rql.ToList.FirstOrDefault
        End If

        Return szData

    End Function

#End Region

#Region "METHODES"

    Public Function IsValideEmail(szMail As String) As Boolean

        Dim bValide As Boolean = False

        Dim email As New Regex("([\w-+]+(?:\.[\w-+]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7})")

        If email.IsMatch(szMail) Then
            bValide = True
        End If

        Return bValide

    End Function

    Public Function IsValidDate(szDate As String) As Boolean

        Dim bValide As Boolean = False

        Try
            Dim UserDate As DateTime

            UserDate = Convert.ToDateTime(szDate)

            If UserDate <> Nothing Then
                bValide = True
            End If

            ' return bValid which is already false

        Catch ex As Exception
        End Try

        Return bValide

    End Function

    Public Function IsStrongPWD(szPWD As String, mode As Boolean) As Boolean

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
