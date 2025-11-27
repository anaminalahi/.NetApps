@Code
    ViewData("Title") = "Session Terminée"
End Code
<div class="login formWrapper">
    <div class="panel panel-primary" style="border: 1px solid grey">
        <div class="panel-heading">
            <h4 class="panel-title text-center">SESSION TERMINEE</h4>
        </div>
        <div class="panel-body">
            <br />
            @Using (Html.BeginForm())
                @Html.AntiForgeryToken()
                @<div Class="form-horizontal">
                    <p Class="text-justify">
                        Votre session est terminée. Veuillez vous connecter à nouveau s'il vous plaît. Merci.
                    </p>
                    <br />
                    <div Class="form-group">
                        <div Class="text-center">
                            <a href="https://www.myspedidam.fr" Class="btn btn-lg btn-danger" type="submit">Retour à la page de connexion</a>
                        </div>
                    </div>
                </div>
            End Using
        </div>
        <br />
    </div>
</div>




