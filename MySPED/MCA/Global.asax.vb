Imports System.Net.Mail
Imports System.Web.Optimization
Imports DB

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Public Shared MyManager As DB.Manager
    Public Shared SessionTermineFR As String

#Region "LISTE DE REFERENCES"

    Public Shared Property ListeDesIntruments As List(Of Instrument)
    Public Shared Property ListeDesNationalites As List(Of Pays)
    Public Shared Property ListeDesTypesAdresses As List(Of TypeAdresses)
    Public Shared Property ListeDesTypeDeVoies As List(Of TypeDeVoies)
    Public Shared Property ListeDesAnneesFiscales As List(Of AnneeFiscale)

#End Region

    Sub Application_Start()

        Try

            AreaRegistration.RegisterAllAreas()
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
            RouteConfig.RegisterRoutes(RouteTable.Routes)
            BundleConfig.RegisterBundles(BundleTable.Bundles)

            AutoMapper.Mapper.CreateMap(Of DB.Spedinaute, DB.SpedinauteVM)()
            AutoMapper.Mapper.CreateMap(Of DB.SpedAdresse, DB.SpedAdresseVM)()
            AutoMapper.Mapper.CreateMap(Of DB.RibIban, DB.RibIbanVM)()
            AutoMapper.Mapper.CreateMap(Of DB.Assemblee, DB.AssembleeVM)()

        Catch e As Exception

            RenvoiParMailDesErreurs("fn Application_Error " + e.Message())
            Server.ClearError()
            Response.Redirect("/SessionExpiree/Index")

        End Try

    End Sub

    Sub Application_Error(sender As Object, e As EventArgs)

        Dim objErr As Exception = Server.GetLastError().GetBaseException
        Dim err As String = "Error in: " & Request.Url.ToString() & ". Error Message:" & objErr.Message.ToString()

        RenvoiParMailDesErreurs("fn Application_Error " + err)
        Server.ClearError()
        Response.Redirect("/SessionExpiree/Index")

    End Sub

    Sub Application_Session_End(sender As Object, e As EventArgs)

        'SessionTermineFR = "Votre session a expiré. Merci de bien vouloir vous reconnecter."
        Response.Redirect("/SessionExpiree/Index")

    End Sub

    Public Shared Sub RenvoiParMailDesErreurs(Logs As String)

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

        smtpServer.Dispose()
        smtpServer = Nothing
        mail = Nothing

    End Sub

End Class
