@ModelType DB.MesAdressesVM
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

                <div class="section">
                    <div class="container">
                        <div class="row">

                            <!-- DIV POSTALE -->
                            <div class="col-md-6">
                                <h1 class="text-danger">Postale</h1>
                                <div class="row">
                                    <div class="form-group col-sm-12">
                                        <label style="text-align:left" class="control-label col-sm-4">Adresse</label>
                                        <div class="col-lg-8">
                                            <input type="text" class="form-control" value="@Model.Postale.LibelleVoie" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-sm-12">
                                        <label style="text-align:left" class="control-label col-sm-4">Complément</label>
                                        <div class="col-lg-8">
                                            <input type="text" class="form-control" value="@Model.Postale.Adresse2" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-sm-12">
                                        <label style="text-align:left" class="control-label col-sm-4">Code Postal</label>
                                        <div class="col-lg-8">
                                            <input type="text" class="form-control" value="@Model.Postale.CodePostal" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-sm-12">
                                        <label style="text-align:left" class="control-label col-sm-4">Ville</label>
                                        <div class="col-lg-8">
                                            <input type="text" class="form-control" value="@Model.Postale.Ville" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-sm-12">
                                        <label style="text-align:left" class="control-label col-sm-4">Pays</label>
                                        <div class="col-lg-8">
                                            <input type="text" class="form-control" value="@Model.Postale.MonPays" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <p></p>
                                <div class="text-center col-lg-offset-1 col-lg-10">
                                    <a href="/SpedAdresses/ChangerDAdresse/" type="submit" class="btn btn-primary btn-block"><span class="glyphicon glyphicon-pencil"></span> CHANGER ADRESSE POSTALE </a>
                                </div>
                                <p></p>
                                <p></p>
                            </div>

                            <!-- DIV FISCALE -->
                            <div class="col-md-6">
                                <h1 class="text-danger">Fiscale</h1>
                                <div class="row">
                                    <div class="form-group col-sm-12">
                                        <label style="text-align:left" class="control-label col-sm-4">Adresse</label>
                                        <div class="col-lg-8">
                                            <input type="text" class="form-control" value="@Model.Fiscale.LibelleVoie" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-sm-12">
                                        <label style="text-align:left" class="control-label col-sm-4">Complément</label>
                                        <div class="col-lg-8">
                                            <input type="text" class="form-control" value="@Model.Fiscale.Adresse2" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-sm-12">
                                        <label style="text-align:left" class="control-label col-sm-4">Code Postal</label>
                                        <div class="col-lg-8">
                                            <input type="text" class="form-control" value="@Model.Fiscale.CodePostal" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-sm-12">
                                        <label style="text-align:left" class="control-label col-sm-4">Ville</label>
                                        <div class="col-lg-8">
                                            <input type="text" class="form-control" value="@Model.Fiscale.Ville" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-sm-12">
                                        <label style="text-align:left" class="control-label col-sm-4">Pays</label>
                                        <div class="col-lg-8">
                                            <input type="text" class="form-control" value="@Model.Fiscale.MonPays" disabled="disabled" />
                                        </div>
                                    </div>
                                </div>
                                <p></p>
                                <div class="text-center col-lg-offset-1 col-lg-10">
                                    <a href="/SpedAdresses/ChangerDAdresseFiscale/" type="submit" class="btn btn-primary btn-block"><span class="glyphicon glyphicon-pencil"></span> CHANGER ADRESSE FISCALE </a>
                                </div>
                                <p></p>
                                <p></p>
                            </div>

                        </div>
                    </div>
                </div>

            </div>

        </section>
    </div>

End Using

