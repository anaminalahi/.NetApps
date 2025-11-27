@Code
    ViewData("Title") = "PasTrouve"
End Code

<div class="login formWrapper">

    <div class="panel panel-primary" style="border: 1px solid grey">

        <div class="panel-heading">
            <h4 class="panel-title text-center">CONNEXION IMPOSSIBLE</h4>
        </div>

        <div class="panel-body">

            <br />
            <p class="text-center"><strong>Désolé, mais nous n'avons pu vous identifier.</strong></p>
            <br />
            @Using (Html.BeginForm())
                @Html.AntiForgeryToken()

                @<div Class="form-horizontal">
                    <p Class="text-center">Merci de contacter la Spedidam :</p>
                    <ul>
                        <li> par email : <a href="mailto:assistance@spedidam.fr" target="_blank">assistance@spedidam.fr</a></li>
                        <li> par téléphone : 01 44 18 58 58 du lundi au jeudi de 9h00 à 13h00 et de 14h00 à 17h30, le Vendredi de 9h00 à 12h00</li>
                    </ul>
                    <br />
                    <div Class="form-group">
                        <div Class="text-center">
                            <a href="https://www.myspedidam.fr" Class="btn btn-lg btn-danger" type="submit">Retour a la page de connexion</a>
                        </div>
                    </div>
                </div>

            End Using
            <br />
            <br />

        </div>
    </div>

</div>




