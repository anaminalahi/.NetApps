@Imports RazorExtensions
<!DOCTYPE html>
<html class="fonts-loaded" lang="fr">

<head>

    <meta name="viewport" content="width=device-width" />
    <title>@ViewData("Title")</title>

    @Scripts.Render("~/bundles/modernizr")
    @Scripts.Render("~/bundles/jquery")

    <link href="@Url.Content("~/Content/bootstrap.css")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/Content/box-sizing-fixes.css")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/Content/Site.css")" rel="stylesheet" type="text/css" />

    <link href="https://kendo.cdn.telerik.com/2017.2.504/styles/kendo.common-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="https://kendo.cdn.telerik.com/2017.2.504/styles/kendo.mobile.all.min.css" rel="stylesheet" type="text/css" />
    <link href="https://kendo.cdn.telerik.com/2017.2.504/styles/kendo.dataviz.min.css" rel="stylesheet" type="text/css" />
    <link href="https://kendo.cdn.telerik.com/2017.2.504/styles/kendo.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="https://kendo.cdn.telerik.com/2017.2.504/styles/kendo.dataviz.bootstrap.min.css" rel="stylesheet" type="text/css" />

    <script src="https://kendo.cdn.telerik.com/2017.2.504/js/jquery.min.js"></script>
    <script src="https://kendo.cdn.telerik.com/2017.2.504/js/jszip.min.js"></script>
    <script src="https://kendo.cdn.telerik.com/2017.2.504/js/kendo.all.min.js"></script>
    <script src="https://kendo.cdn.telerik.com/2017.2.504/js/kendo.aspnetmvc.min.js"></script>

    <script src="@Url.Content("~/Scripts/kendo.modernizr.custom.js")"></script>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <link href="~/Content/mysped.css" rel="stylesheet" type="text/css"/>

    <link rel="icon" type="image/icon" href="~/Images/spedifavico.ico" sizes="64x64 32x32 24x24 16x16">
    <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Open+Sans:200,300,400,400italic,600,700,800">

    <script async="" src="https://www.google-analytics.com/analytics.js"></script>

    <script>
        (function () {
            var ls = window.localStorage;
            if (ls && ls.getItem('fonts-loaded')) {
                var l = document.createElement('link');
                l.rel = 'stylesheet';
                l.type = 'text/css';
                l.href = 'https://fonts.googleapis.com/css?family=Open+Sans:300,400,400italic,600,700,800';
                document.getElementsByTagName('head')[0].appendChild(l);
                document.documentElement.className += ' fonts-loaded';
            }
        })();
    </script>

</head>

<body style="background-color:#202020;">

    <div class="pageContainer">
        <div class="headerWrapper">
            <header class="header">
                <div class="header-content content--bounded">
                    <h2 class="logo">
                        <a href="/">
                            <img src="~/Images/logo_navbar.png" alt="MySpedidam - Mon Compte Artiste" class="logo-img">
                        </a>
                    </h2>
                    <input id="menu-toggle" role="button" class="menu-toggle-input" type="checkbox">
                    <label for="menu-toggle" data-menu-open="Close" data-menu-closed="Menu" class="menu-toggle-button button button--outline button--small"></label>
                    <nav class="navigation">
                        <ul class="navigation-links">
                            <li>@Html.ActionLink(" Fiche ", "Index", "Spedinaute", FormMethod.Get, New With {Key .[class] = "glyphicon glyphicon-user"})</li>
                            <li>@Html.ActionLink(" Adresse ", "Index", "SpedAdresses", FormMethod.Get, New With {Key .[class] = "glyphicon glyphicon-road"})</li>
                            <li>@Html.ActionLink(" Rib ", "Index", "RibIbans", FormMethod.Get, New With {Key .[class] = "glyphicon glyphicon-credit-card"})</li>
                            <li>@Html.ActionLink(" Assemblées ", "Index", "Assemblees", FormMethod.Get, New With {Key .[class] = "glyphicon glyphicon-music"})</li>
                            <li>@Html.ActionLink(" Fiscalité ", "Index", "Fiscalite", FormMethod.Get, New With {Key .[class] = "glyphicon glyphicon-music"})</li>
                        </ul>
                        <ul class="navigation-buttons">
                            <li>@Html.ActionLink(" QUITTER", "CloreApplication", "Connexion", FormMethod.Get, New With {Key .[class] = "button button--danger glyphicon glyphicon-log-out"})</li>
                        </ul>
                    </nav>
                </div>
            </header>
        </div>

        <main class="main">
            @RenderBody()
        </main>

        <footer class="footer">
            <div class="content--bounded">
                <ul class="footer-linkList">
                    <li class="footer-linkItem">
                        <a href="mailto:assistance@spedidam.fr" class="footer-link">
                            <i class="footer-icon-mail"></i>
                            <span class="footer-link-text">assistance@spedidam.fr</span>
                        </a>
                    </li>
                    <li class="footer-linkItem">
                        <a href="https://facebook.com/Spedidam" class="footer-link">
                            <i class="footer-icon-facebook"></i>
                            <span class="footer-link-text">Facebook</span>
                        </a>
                    </li>
                    <li class="footer-linkItem">
                        <a href="https://twitter.com/Spedidam" class="footer-link">
                            <i class="footer-icon-twitter"></i>
                            <span class="footer-link-text">Twitter</span>
                        </a>
                    </li>
                    <li class="footer-linkItem">
                        <a href="https://plus.google.com/+Spedidam" class="footer-link">
                            <i class="footer-icon-googlePlus"></i>
                            <span class="footer-link-text">Google+</span>
                        </a>
                    </li>
                </ul>
                <div class="content--bounded">
                    <ul class="footer-legalList">
                        <li class="footer-legalItem">
                            <a href="#" class="footer-legal-link">Mentions légales</a>
                        </li>
                        <li class="footer-legalItem footer-copyrightItem">
                            <span class="footer-copyrightText">@DateTime.Now.Year - SPEDIDAM Tous droits réservés.</span>
                        </li>
                        <li class="footer-legalItem">
                            <a href="#" class="footer-legal-link">Contact</a>
                        </li>
                    </ul>
                </div>
            </div>
        </footer>

        <script>
            $(document).ready(function () {
                $("#responsive-panel").kendoResponsivePanel({
                    breakpoint: 768,
                    autoClose: false,
                    orientation: "top"
                });
            });
            function onclick(e) {
                $("#responsive-panel").getKendoResponsivePanel().toggle();
            }
        </script>

        <script>window.googleAnalyticsAccount = 'UA-5869575-3';</script>
        <script src="/js/public-pages-782dae9dcf.js"></script>
        <script>require('browserify.publicPages').PublicCommonController().init();</script>
        <script>require('browserify.publicPages').LoginController().init();</script>

        @Scripts.Render("~/bundles/bootstrap")
        @RenderSection("scripts", required:=False)

    </div>

</body>

</html>
