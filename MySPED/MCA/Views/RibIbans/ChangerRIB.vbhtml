@ModelType DB.RibIbanVM
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
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">Titulaire du Compte</label>
                        <div class="col-lg-8">
                            @Html.EditorFor(Function(model) model.Titulaire, New With {.htmlAttributes = New With {.class = "form-control", .Type = "Text", .disabled = "disabled"}})
                        </div>
                    </div>
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">Pays</Label>
                        <div Class="col-lg-8">
                            @Html.DropDownListFor(Function(model) model.Id_pays,
                            New SelectList(MvcApplication.ListeDesNationalites, "id_pays", "lb_pays"),
                            New With {Key .htmlAttributes = New With {Key .[class] = "form-control selectpicker"}})
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">BIC SWIFT</label>
                        <div class="col-lg-8">
                            @Html.EditorFor(Function(model) model.BicSwift, New With {.htmlAttributes = New With {.class = "form-control", .Type = "Text", .autofocus = "autofocus"}})
                        </div>
                    </div>
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">IBAN</label>
                        <div class="col-lg-8">
                            @Html.EditorFor(Function(model) model.NumeroIBAN, New With {.htmlAttributes = New With {.class = "form-control", .Type = "Text"}})
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-lg-6">
                        <label style="text-align:left" class="control-label col-lg-4">Domiciliation</label>
                        <div class="col-lg-8">
                            @Html.EditorFor(Function(model) model.Domiciliation, New With {.htmlAttributes = New With {.class = "form-control", .Type = "Text"}})
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div Class="form-group col-lg-6">
                        <div Class="col-lg-4">
                            <Button Class="btn btn-primary btn-block" type="submit"><span class="glyphicon glyphicon-floppy-save"></span> ENREGISTRER </Button>
                        </div>
                        <div class="col-lg-2"></div>
                    </div>
                    <div Class="form-group col-lg-6">
                        <div class="col-lg-2"></div>
                        <div Class="col-lg-4">
                            <a href="/RibIbans/AnnulerSaisie" Class="btn btn-danger btn-block"><span Class="glyphicon glyphicon-remove"></span> ANNULER </a>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>

End Using
