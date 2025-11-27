@ModelType DB.SpedinauteVM
@Code
    ViewData("Title") = "Edit"
End Code

<head>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.2/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="~/Content/bootstrap-select.min.css" />
    <script src="~/Scripts/bootstrap-select.min.js"></script>

</head>

@Using (Html.BeginForm("Edit", "Spedinaute", FormMethod.Post))
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

                <div Class="row">
                    <br />
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">Civilité</Label>
                        <div Class="col-lg-4">
                            @Html.DropDownListFor(Function(model) model.Civilite,
                          New SelectList(New List(Of [Object])() From {
                          New With {
                          Key .value = "MME",
                          Key .text = "MADAME"
                          },
                          New With {
                          Key .value = "MLE",
                          Key .text = "MADEMOISELLE"
                          },
                          New With {
                          Key .value = "M.",
                          Key .text = "MONSIEUR"
                          }
                          }, "value", "text", 3),
                          New With {Key .htmlAttributes = New With {Key .class = "selectpicker"}})
                        </div>
                    </div>
                </div>
                <div Class="row">
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">Prénom</Label>
                        <div Class="col-lg-8">
                            @Html.EditorFor(Function(model) model.Prenom, New With {.htmlAttributes = New With {.class = "form-control", .Type = "Text", .PlaceHolder = "JEAN PIERRE"}})
                        </div>
                    </div>
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">Autre prénom</Label>
                        <div Class="col-lg-8">
                            @Html.EditorFor(Function(model) model.AutrePrenom, New With {.htmlAttributes = New With {.class = "form-control", .Type = "Text", .PlaceHolder = "COMO"}})
                        </div>
                    </div>
                </div>
                <div Class="row">
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">Nom</Label>
                        <div Class="col-lg-8">
                            @Html.EditorFor(Function(model) model.Nom, New With {.htmlAttributes = New With {.class = "form-control", .Type = "Text", .PlaceHolder = "COMO"}})
                        </div>
                    </div>
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">Nom de Jeune fille</Label>
                        <div Class="col-lg-8">
                            @Html.EditorFor(Function(model) model.NomDeJeuneFille, New With {.htmlAttributes = New With {.class = "form-control", .Type = "Text", .PlaceHolder = ""}})
                        </div>
                    </div>
                </div>
                <div Class="row">
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">Date De Naissance</Label>
                        <div Class="col-lg-4">
                            @Html.EditorFor(Function(model) model.DateDeNaissance, "{0:dd/MM/yyyy}", New With {.htmlAttributes = New With {.class = "date-picker", .Type = "Text", .PlaceHolder = "04/04/1960"}})
                        </div>
                    </div>
                    <div Class="form-group col-lg-6">
                        <Label style="text-align: Left" Class="control-label col-lg-4">Lieu De Naissance</Label>
                        <div Class="col-lg-8">
                            @Html.EditorFor(Function(model) model.LieuDeNaissance, New With {.htmlAttributes = New With {.class = "form-control"}})
                        </div>
                    </div>
                </div>
                <div Class="row">
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">Nationalité</Label>
                        <div Class="col-lg-8">
                            @Html.DropDownListFor(Function(model) model.Nationalite,
         New SelectList(MvcApplication.ListeDesNationalites, "id_pays", "lb_pays"),
         New With {Key .htmlAttributes = New With {Key .[class] = "form-control selectpicker"}})
                        </div>
                    </div>
                </div>
                <div Class="row">
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">Numéro de Fixe</Label>
                        <div Class="col-lg-4">
                            @Html.EditorFor(Function(model) model.NumeroDeFixe, New With {.htmlAttributes = New With {.class = "form-control"}})
                        </div>
                    </div>
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">Numéro de Mobile</Label>
                        <div Class="col-lg-4">
                            @Html.EditorFor(Function(model) model.NumeroDeMobile, New With {.htmlAttributes = New With {.class = "form-control"}})
                        </div>
                    </div>
                </div>
                <div Class="row">
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">Adresse Email</Label>
                        <div Class="col-lg-8">
                            @Html.EditorFor(Function(model) model.AdresseEmail, New With {.htmlAttributes = New With {.class = "form-control", .Type = "Text", Key .placeHolder = "morndadje@sossal.fr"}})
                        </div>
                    </div>
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">Mot de passe</Label>
                        <div Class="col-lg-4">
                            @Html.TextBoxFor(Function(model) model.PasseMot, New With {.htmlAttributes = New With {.class = "form-control", .Type = "Text", Key .placeHolder = "**********"}})
                        </div>
                    </div>
                </div>
                <div Class="row">
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">N° IPN</Label>
                        <div Class="col-lg-4">
                            @Html.TextBoxFor(Function(model) model.IPN, New With {.htmlAttributes = New With {.class = "form-control"}, Key .readonly = "readonly"})
                        </div>
                    </div>
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">N° d'Associé</Label>
                        <div Class="col-lg-4">
                            @Html.TextBoxFor(Function(model) model.NumeroAD, New With {.htmlAttributes = New With {.class = "form-control"}, Key .readonly = "readonly"})
                        </div>
                    </div>
                </div>
                <div Class="row">
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">N° de Non Associé</Label>
                        <div Class="col-lg-4">
                            @Html.TextBoxFor(Function(model) model.NumeroNA, New With {.htmlAttributes = New With {.class = "form-control"}, Key .readonly = "readonly"})
                        </div>
                    </div>
                </div>
                <div Class="row">
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">Instrument 1</Label>
                        <div Class="col-lg-8">
                            @Html.DropDownListFor(Function(model) model.Instrument1,
   New SelectList(MvcApplication.ListeDesIntruments, "id_instr", "lb_instr"),
   New With {Key .htmlAttributes = New With {Key .[class] = "form-control"}})
                        </div>
                    </div>
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">Instrument 2</Label>
                        <div Class="col-lg-8">
                            @Html.DropDownListFor(Function(model) model.Instrument2,
      New SelectList(MvcApplication.ListeDesIntruments, "id_instr", "lb_instr"),
      New With {Key .htmlAttributes = New With {Key .[class] = "form-control"}})
                        </div>
                    </div>
                </div>
                <div Class="row">
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">Instrument 3</Label>
                        <div Class="col-lg-8">
                            @Html.DropDownListFor(Function(model) model.Instrument3,
        New SelectList(MvcApplication.ListeDesIntruments, "id_instr", "lb_instr"),
        New With {Key .htmlAttributes = New With {Key .[class] = "form-control"}})
                        </div>
                    </div>
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">Pseudonyme 1</Label>
                        <div Class="col-lg-8">
                            @Html.EditorFor(Function(model) model.Pseudonyme1, New With {.htmlAttributes = New With {.class = "form-control"}})
                        </div>
                    </div>
                </div>
                <div Class="row">
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">Pseudonyme 2</Label>
                        <div Class="col-lg-8">
                            @Html.EditorFor(Function(model) model.Pseudonyme2, New With {.htmlAttributes = New With {.class = "form-control"}})
                        </div>
                    </div>
                    <div Class="form-group col-lg-6">
                        <Label style="text-align:left" Class="control-label col-lg-4">Pseudonyme 3</Label>
                        <div Class="col-lg-8">
                            @Html.EditorFor(Function(model) model.Pseudonyme3, New With {.htmlAttributes = New With {.class = "form-control"}})
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
                            <a href="/Spedinaute/AnnulerSaisie" Class="btn btn-danger btn-block"><span Class="glyphicon glyphicon-remove"></span> ANNULER </a>
                        </div>
                    </div>
                    <br>
                </div>
            </div>

        </section>
    </div>

End Using
