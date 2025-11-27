@ModelType DB.SpedAdresseVM
@Code
    ViewData("Title") = "Edit"
End Code

@Using (Html.BeginForm())
    @Html.AntiForgeryToken()

    @<div Class="contentWrapper contentWrapper--grey">
        <section Class="press-section content content--white content--bounded content--verticallySpaced content--marginTop">
            <h2 Class="section-title"><strong>@Model.TitreDeLaPageEnCours</strong></h2>
            <hr Class="title-underline title-underline--brand">

            <br />
            <p class="text-center" style="color:red"> @ViewBag.ErrMessage </p>
            <br />

            <div class="form-horizontal col-lg-12" style="background-color:whitesmoke;border-bottom-left-radius:6px;border-top-left-radius:6px">

                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})

                <div class="row">
                    <div class="form-group col-lg-12">
                        <label style="text-align:left" class="control-label col-lg-4">Adresse</label>
                        <div class="col-lg-8">
                            @Html.EditorFor(Function(model) model.LibelleVoie, New With {.htmlAttributes = New With {.class = "form-control", .Type = "Text"}})
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-lg-12">
                        <label style="text-align:left" class="control-label col-lg-4">Complément</label>
                        <div class="col-lg-8">
                            @Html.EditorFor(Function(model) model.Adresse2, New With {.htmlAttributes = New With {.class = "form-control", .Type = "Text"}})
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-lg-12">
                        <label style="text-align:left" class="control-label col-lg-4">Code Postal</label>
                        <div class="col-lg-3">
                            @Html.EditorFor(Function(model) model.CodePostal, New With {.htmlAttributes = New With {.class = "form-control", .Type = "Text"}})
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-lg-12">
                        <label style="text-align:left" class="control-label col-lg-4">Ville</label>
                        <div class="col-lg-8">
                            @Html.EditorFor(Function(model) model.Ville, New With {.htmlAttributes = New With {.class = "form-control", .Type = "Text"}})
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="form-group col-lg-12">
                        <label style="text-align:left" class="control-label col-lg-4">Pays</label>
                        <div class="col-lg-8">
                            @Html.DropDownListFor(Function(model) model.Id_pays,
                                                     New SelectList(MvcApplication.ListeDesNationalites, "id_pays", "lb_pays"),
                                                     New With {Key .htmlAttributes = New With {Key .[class] = "form-control"}})
                        </div>
                    </div>
                </div>

                <div Class="row">
                    <div Class="form-group col-lg-6">
                        <Label Class="col-lg-4"></Label>
                        <div Class="col-lg-6">
                            <Button Class="btn btn-primary btn-block" type="submit"><span class="glyphicon glyphicon-floppy-save"></span> ENREGISTRER </Button>
                        </div>
                    </div>
                    <div Class="form-group col-lg-6">
                        <Label Class="col-lg-4"></Label>
                        <div Class="col-lg-6">
                            <a href="/SpedAdresses/Index" Class="btn btn-danger btn-block"><span Class="glyphicon glyphicon-remove"></span> ANNULER </a>
                        </div>
                    </div>
                    <br>

                </div>

                <br>

            </div>

        </section>
    </div>

End Using

