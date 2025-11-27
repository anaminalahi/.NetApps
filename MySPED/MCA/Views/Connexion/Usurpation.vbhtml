@Code
    ViewData("Title") = "Usurpation"
End Code

<div class="login formWrapper">

    <div class="panel panel-primary" style="border: 1px solid grey">

        <div class="panel-heading">
            <h4 class="panel-title text-center">USURPATION DE COMPTE</h4>
        </div>

        <div class="panel-body">

            <br />

            @Using (Html.BeginForm())
                @Html.AntiForgeryToken()

                @<div Class="form-horizontal">

                    <p Class="text-justify">
                        Vous avez signalé l'usurpation de votre compte artiste.
                        Nos services vous contacterons sous peu. Cordialement
                    </p>
                    <br />

                    <div Class="form-group">
                        <div Class="text-center">
                            <a href="https://www.myspedidam.fr" Class="btn btn-lg btn-danger" type="submit">Retour a la page de connexion</a>
                        </div>
                    </div>

                </div>

            End Using

        </div>

        <br />

    </div>
</div>