@ModelType DB.Spedinaute

@Code
    ViewData("Title") = "RenvoiOuCreationDeMotDePasse"
End Code

<div class="login formWrapper">

    <div class="panel panel-primary" style="border: 1px solid grey">

        <div class="panel-heading">
            <h4 class="panel-title text-center">OUBLI / CREATION DE MOT DE PASSE</h4>
        </div>
        <div class="panel-body">
            <p class="text-center">Veuillez saisir les informations</p>
            <p class="text-center">qui permettront de vous identifier</p>
            <br />
            <p class="text-center" style="color:red"> @ViewBag.ErrMessage </p>
            <br />

            @Using (Html.BeginForm())

                @Html.AntiForgeryToken()

                @<div class="form">

                    @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                    <div>
                        @Html.Label("Prénom(s)", New With {.class = "control-label"})
                    </div>
                    <div class="form-group">
                        @Html.EditorFor(Function(model) model.Prenom, New With {.class = "form-control"})
                    </div>
                    <p></p>
                    <div>
                        @Html.Label("Nom", New With {.class = "control-label"})
                    </div>
                    <div class="form-group">
                        @Html.EditorFor(Function(model) model.Nom, New With {.htmlAttributes = New With {.class = "form-control", .Type = "Text", .PlaceHolder = "ZAWINUL"}})
                    </div>
                    <p></p>
                    <div>
                        @Html.Label("Email", New With {.class = "control-label"})
                    </div>
                    <div class="form-group">
                        @Html.EditorFor(Function(model) model.AdresseEmail, New With {.htmlAttributes = New With {.class = "form-control", .Type = "Text", .PlaceHolder = "morndadje@sossal.fr"}})
                    </div>

                    <hr/>

                    <div class="form-group">
                        @Html.CheckBox("RenvoiOuCreation")
                        @Html.Label("Cochez pour renvoyer, sinon créez votre de passe", New With {.htmlAttibutes = New With {.class = "text-center"}})
                    </div>

                    <hr/>

                    <div id="passwordChangeForm">
                        <div>
                            @Html.Label("Mot de passe", New With {.class = "control-label"})
                        </div>
                        <div class="form-group">
                            @Html.EditorFor(Function(model) model.PasseMot, New With {.class = "form-control"})
                        </div>
                        <p></p>
                        <div>
                            @Html.Label("Confirmer Mot de passe", New With {.class = "control-label"})
                        </div>
                        <div class="form-group">
                            @Html.Editor("PasseMotConfirme", New With {.class = "form-control"})
                        </div>
                    </div>
                    <hr />
                    <div Class="form-group">
                        <Button Class="btn btn-lg btn-danger btn-block" type="submit"><span class="glyphicon glyphicon-send"></span> VALIDER </Button>
                    </div>
                    <br />
                    <div class="text-center">
                        @Html.ActionLink("Retour au Menu Précédent", "Index", New With {.class = "text-center"})
                    </div>
                </div>

            End Using

            @Section Scripts
                <script src="~/Scripts/RenvoiOuCreationDeMotDePasse.js"></script>
            End Section

        </div>
    </div>

    <br />
</div>





