@ModelType DB.SpedinauteVM
@Code
    ViewData("Title") = "Index"
End Code


@Using (Html.BeginForm())
    @Html.AntiForgeryToken()

    @<div Class="contentWrapper contentWrapper--grey">
        <section Class="press-section content content--white content--bounded content--verticallySpaced content--marginTop">
            <h2 Class="section-title"><strong>@Model.TitreDeLaPageEnCours</strong></h2>
            <hr Class="title-underline title-underline--brand">

            <div class="form-horizontal col-lg-12" style="background-color:whitesmoke;border-bottom-left-radius:6px;border-top-left-radius:6px">
                <br>
                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                <div class="row">
                    <div class="form-group col-sm-6">
                        <label style="text-align:left" class="control-label col-lg-4">Civilité</label>
                        <div class="col-lg-4">
                            <input type="text" class="form-control" value="@Model.MaCivilite" readonly="readonly" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">Prénom</label>
                        <div class="col-lg-8">
                            <input type="text" class="form-control" value="@Model.Prenom" readonly="readonly" />
                        </div>
                    </div>
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">Autre prénom</label>
                        <div class="col-lg-8">
                            <input type="text" class="form-control" value="@Model.AutrePrenom" readonly="readonly" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">Nom</label>
                        <div class="col-lg-8">
                            <input type="text" class="form-control" value="@Model.Nom" readonly="readonly" />
                        </div>
                    </div>
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">Nom de Jeune fille</label>
                        <div class="col-lg-8">
                            <input type="text" class="form-control" value="@Model.NomDeJeuneFille" readonly="readonly" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">Date De Naissance</label>
                        <div class="col-lg-4">
                            <input type="text" class="form-control" value="@DateTime.Parse(Model.DateDeNaissance).ToShortDateString()" readonly="readonly" />
                        </div>
                    </div>
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">Lieu De Naissance</label>
                        <div class="col-lg-8">
                            <input type="text" class="form-control" value="@Model.LieuDeNaissance" readonly="readonly" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">Nationalité</label>
                        <div class="col-lg-8">
                            <input type="text" class="form-control" value="@Model.MaNationalite" readonly="readonly" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">Numéro de Fixe</label>
                        <div class="col-lg-4">
                            <input type="text" class="form-control" value="@Model.NumeroDeFixe" readonly="readonly" />
                        </div>
                    </div>
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">Numéro de Mobile</label>
                        <div class="col-lg-4">
                            <input type="text" class="form-control" value="@Model.NumeroDeMobile" readonly="readonly" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">Adresse Email</label>
                        <div class="col-lg-8">
                            <input type="text" class="form-control" value="@Model.AdresseEmail" readonly="readonly" />
                        </div>
                    </div>
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">Mot de passe</label>
                        <div class="col-lg-4">
                            <input type="text" class="form-control" value="@Model.PasseMot" readonly="readonly" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">N° IPN</label>
                        <div class="col-lg-4">
                            <input type="text" class="form-control" value="@Model.IPN" readonly="readonly" />
                        </div>
                    </div>
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">N° d'Associé</label>
                        <div class="col-lg-4">
                            <input type="text" class="form-control" value="@Model.NumeroAD" readonly="readonly" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">N° de Non Associé</label>
                        <div class="col-lg-4">
                            <input type="text" class="form-control" value="@Model.NumeroNA" readonly="readonly" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">Instrument 1</label>
                        <div class="col-lg-8">
                            <input type="text" class="form-control" value="@Model.MonInstrument1" readonly="readonly" />
                        </div>
                    </div>

                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">Instrument 2</label>
                        <div class="col-lg-8">
                            <input type="text" class="form-control" value="@Model.MonInstrument2" readonly="readonly" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">Instrument 3</label>
                        <div class="col-lg-8">
                            <input type="text" class="form-control" value="@Model.MonInstrument3" readonly="readonly" />
                        </div>
                    </div>
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">Pseudonyme 1</label>
                        <div class="col-lg-8">
                            <input type="text" class="form-control" value="@Model.Pseudonyme1" readonly="readonly" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">Pseudonyme 2</label>
                        <div class="col-lg-8">
                            <input type="text" class="form-control" value="@Model.Pseudonyme2" readonly="readonly" />
                        </div>
                    </div>
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">Pseudonyme 3</label>
                        <div class="col-lg-8">
                            <input type="text" class="form-control" value="@Model.Pseudonyme3" readonly="readonly" />
                        </div>
                    </div>
                </div>

                <div class="text-center col-lg-offset-1 col-lg-10">
                    <a href="/Spedinaute/Edit/@Model.IdSpedinaute" type="submit" class="btn btn-primary btn-block"><span class="glyphicon glyphicon-pencil"></span> MODIFIER FICHE </a>
                </div>

            </div>

        </section>
    </div>

End Using
