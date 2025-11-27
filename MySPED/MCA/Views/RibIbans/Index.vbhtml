@ModelType DB.RibIbanVM
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
                     <span></span>
                 </div>
                 <div class="row">
                     <div class="form-group col-lg-6">
                         <label style="text-align:left" class="control-label col-lg-4">Titulaire du Compte</label>
                         <div class="col-lg-8">
                             <input type="text" class="form-control" value="@Model.Titulaire" disabled="disabled" />
                         </div>
                     </div>
                     <div class="form-group col-lg-6">
                         <label style="text-align:left" class="control-label col-lg-4">Pays</label>
                         <div class="col-lg-8">
                             <input type="text" class="form-control" value="@Model.MonPays" readonly="readonly" />
                         </div>
                     </div>
                 </div>
                 <div class="row">
                     <div class="form-group col-lg-6">
                         <label style="text-align:left" class="control-label col-lg-4">BIC SWIFT</label>
                         <div class="col-lg-8">
                             <input type="text" class="form-control" value="@Model.BicSwift" disabled="disabled" />
                         </div>
                     </div>
                     <div class="form-group col-lg-6">
                         <label style="text-align:left" class="control-label col-lg-4">IBAN</label>
                         <div class="col-lg-8">
                             <input type="text" class="form-control" value="@Model.NumeroIBAN" disabled="disabled" />
                         </div>
                     </div>
                 </div>
                 <div class="row">
                     <div class="form-group col-lg-6">
                         <label style="text-align:left" class="control-label col-lg-4">Domiciliation</label>
                         <div class="col-lg-8">
                             <input type="text" class="form-control" value="@Model.Domiciliation" disabled="disabled" />
                         </div>
                     </div>
                 </div>
                 <div class="row">
                     <div class="col-lg-3"></div>
                     <div class="text-center col-lg-6">
                         <a href="/RibIbans/ChangerRIB/" type="submit" class="btn btn-primary btn-block"><span class="glyphicon glyphicon-pencil"></span> CHANGER MON RIB </a>
                     </div>
                     <div class="col-lg-3"></div>
                 </div>
                 <div class="row">
                     <span></span>
                 </div>

             </div>

         </section>
    </div>

End Using


