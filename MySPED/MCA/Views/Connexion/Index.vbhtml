@ModelType DB.SpedinauteVM
@Code
    ViewData("Title") = "Index"
End Code

<div class="login formWrapper">

    <div class="panel panel-primary" style="border: 1px solid grey">

        <div class="panel-heading">
            <h4 class="panel-title text-center">CONNEXION</h4>
        </div>

        <div class="panel-body">
            <p class="text-center">Veuillez saisir votre <U>Adresse Email</U> </p>
            <p class="text-center">ainsi que votre <U> Mot de passe</U></p>
            <br />
            <p class="text-center" style="color:red"> @ViewBag.ErrMessage </p>
            <br />
            @Using (Html.BeginForm())
                @Html.AntiForgeryToken()
                @<div class="form">
                    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
                    <div id="formConnexion">
                        <div>
                            @Html.Label("Email", New With {.class = "control-label"})
                        </div>
                        <div class="form-group">
                            @Html.EditorFor(Function(model) model.AdresseEmail, New With {.class = "form-control", .placeHolder = "morndadje@sossal.fr"})
                        </div>
                        <p></p>
                        <div>
                            @Html.Label("Mot de passe", New With {.class = "control-label"})
                        </div>
                        <div class="form-group">
                            @Html.PasswordFor(Function(model) model.PasseMot, New With {.class = "form-control", .placeholder = "**********"})
                        </div>
                    </div>
                     <p></p>
                    <div class="form-group">
                        @Html.CheckBox("motDePasseoublie")
                        @Html.Label("Renvoi du mot de passe" + Chr(13) + "oublié ou création")
                    </div>
                    <div id="rememberMe">
                        <div class="form-group">
                            @Html.CheckBox("seSouvenirDeMoi")
                            @Html.Label("Se souvenir de moi")
                        </div>
                    </div>
                    <hr/>
                    <div class="form-group">
                        <button id="btnConnecter" class="btn btn-lg btn-danger btn-block" type="submit"><span class="glyphicon glyphicon-globe"></span> SE CONNECTER</button>
                    </div>
                </div>
            End Using
        </div>
    </div>
</div>

@Section Scripts
    <script src="~/Scripts/connexion.js"></script>
End Section