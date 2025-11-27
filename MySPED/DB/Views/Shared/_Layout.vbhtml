<!DOCTYPE html>
<html>   
    <head>
        <title>@ViewBag.Title - My Telerik MVC Application</title>
        <link href="@Url.Content("~/Content/bootstrap.css")" rel="stylesheet" type="text/css" />

        @* Content-box fixes as per http://docs.telerik.com/kendo-ui/third-party/using-kendo-with-twitter-bootstrap article  *@
        <link href="@Url.Content("~/Content/box-sizing-fixes.css")" rel="stylesheet" type="text/css" />

        <link href="@Url.Content("~/Content/Site.css")" rel="stylesheet" type="text/css" />

        @Scripts.Render("~/bundles/modernizr")
        @Scripts.Render("~/bundles/jquery")

    </head>
    <body>
        <div class="navbar navbar-inverse navbar-fixed-top">
            <div class="container">
                <div class="navbar-header">
                    <button id="configure" class="k-rpanel-toggle k-button btn-toggle"><span class="k-icon k-i-hbars"></span></button>
                    @Html.ActionLink("Application name", "Index", "Home", New With {Key .area = ""}, New With {Key .class = "navbar-brand"})
                </div>

                <div id="responsive-panel" class="navbar-left">
                    @Code
                        Html.Kendo().Menu() _
                    .Name("Menu") _
                    .Items(Sub(items)
                                items.Add().Text("Home").Action("Index", "Home", New With {Key .area = ""})
                                items.Add().Text("About").Action("About", "Home", New With {Key .area = ""})
                                items.Add().Text("Contact").Action("Contact", "Home", New With {Key .area = ""})
                            End Sub).Render()
                    End Code
                </div>
            </div>
        </div>
        <div class="body-content">
            <div class="container">
                @RenderBody()
                <hr />
                <footer>
                    <p>&copy; @DateTime.Now.Year - My ASP.NET Application</p>
                </footer>
            </div>
        </div>

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

        @Scripts.Render("~/bundles/bootstrap")
        @RenderSection("scripts", required:=False)
    </body>
</html>
