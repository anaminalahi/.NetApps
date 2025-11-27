@ModelType DB.AssembleeVM
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
                <h2>Assemblées relatives à l'exercice @Model.Exercice</h2>
                <p>Les Assemblées Générales Extraordinaire et Ordinaire de la SPEDIDAM relatives à l'exercice @Model.Exercice auront lieu le</p>
                <div class="big_center">
                    <p>
                        @Model.LeJourEnTexte<br>
                        @Model.Lieu<br>
                        @Model.Adresse
                    </p>
                </div>
                <p>&nbsp;</p>
                <h3>Ordres du jour des Assemblées</h3>
                <ul class="L1">
                    <li id="doc_liste_item"><a href="documents/@Model.OrdreDuJourFichier" target="_blank">Ordres du jour<img src="images/pdficon_large.png" title="Ouvrir le pdf" style="vertical-align:middle" width="28"></a></li>
                </ul>
                <p>&nbsp;</p>

                <h3>Liste des documents disponibles</h3>

                <ul class="L1">
                    @For Each oDoc As DB.PdfDoc In Model.ListeDesDocuments
                        @<li id="doc_liste_item"><a href="documents/@oDoc.EmplacementDuFichier" target="_blank">@oDoc.TitreDuSujet <img src="images/pdficon_large.png" title="Ouvrir le pdf" style="vertical-align:middle" width="28"></a></li>
                    Next oDoc
                </ul>

                <p class="dernier">&nbsp;</p><!--?<div class="cleaner h30"-->
            </div>

        </section>
    </div>

End Using
