@ModelType DB.DeclarationVM
@Code
    ViewData("Title") = "Index"
End Code


@Using (Html.BeginForm())
    @Html.AntiForgeryToken()

    @<div Class="contentWrapper contentWrapper--grey">

        <section Class="press-section content content--white content--bounded content--verticallySpaced content--marginTop">
            <h2 Class="section-title"><strong>@Model.TitreDeLaPageEnCours</strong></h2>
            <hr Class="title-underline title-underline--brand">


            <div class="content_box text-center">
                <h2></h2>
                <p>Liste des années disponibles</p>
                <p>
                    <br />
                    <br />
                </p>

                <div class="form-group">
                    <Label style="text-align:left" Class="control-label col-lg-4">Choix de l'année</Label>
                    <div Class="col-lg-8">
                        @Html.DropDownListFor(Function(model) model.SelectedAnnee, New SelectList(MvcApplication.ListeDesAnneesFiscales, "Annee", "Libelle"), New With {Key .htmlAttributes = New With {Key .[class] = "form-control selectpicker"}})
                    </div>
                </div>
                <p>
                    <br/>
                </p>

                <div class="form-group">
                    <button id="btnRechercher" class="btn btn-lg btn-danger btn-block" type="submit"><span class="glyphicon glyphicon-globe"></span> GENERER LE JUSTIFICATIF </button>
                </div>
                <hr />

                <div id="Resultats">

                    @If Model.IsFound Then
                    @<div class="alert alert-success alert-dismissible">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <strong>Trouvé !</strong>
                    </div>
                    @<div id="Afficher">
                        <ul class="L1">
                            <li id="doc_liste_item"><a href="@Model.FichierPDF" target="_blank">Relevé fiscal<img src="images/pdficon_large.png" title="Ouvrir le pdf" style="vertical-align:middle" width="28"></a></li>
                        </ul>
                    </div>
                    ElseIf Model.FichierPDF = "Pas trouvé" Then

                        @<div class="alert alert-danger alert-dismissible">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <strong>Aucun relevé disponible pour cette année.</strong>.
                    </div>

                    End If

                </div>

                <p class="dernier">&nbsp;</p><!--?<div class="cleaner h30"-->

            </div>

        </section>
    </div>

End Using

@Section Scripts
    <script src="~/Scripts/fiscalitejs.js"></script>
End Section