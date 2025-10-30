Imports System.IO
Imports LMKit.TextGeneration
Public Class FormMain

#Region "Propriétés"

    Public Property ListeDeProject As List(Of ProjetVBP)
    Public Property ListeDesApplications As List(Of String)
    Public Property ListeDesFichiersVBP As List(Of String)
    Public Property ListeDesFichiersNonTrouves As List(Of String) = New List(Of String)()
    Public Property ListeDesFichierSource As List(Of FicSource)
    Public Property ListeDesRefOCx As List(Of RefComp)
    Public Property SelectedProjetVBP As ProjetVBP
    Public Property SelectedFormulaire As FicSource
    Public Property SelectedModule As FicSource
    Public Property SelectedClsClasse As FicSource
    Public Property SelectedRefComp As RefComp

#End Region

#Region "Chargement Formulaire"

    Private Sub FormMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ListeDesFichiersVBP = New List(Of String)() From {
                "AGIRA|C:\Analyses_VB\VB6Apps\AGIRA\Suivi\Suivi_Agira.vbp",
                "AGIRA|C:\Analyses_VB\VB6Apps\AGIRA\Import\ImportAGIRA.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\tools\ExtractionActuariat\Actuariat1\Actuariat1.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\tools\ExtractionActuariat\Actuariat2\Actuariat2.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\tools\MaintenanceET\maintenance\maintenance.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\tools\MaintenanceET\creerBilanAnnuelManquant\CreerBAManquant.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\tools\NonRegression\controleBilanAnnuel\ControleBilanAnnuel.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\tools\NonRegression\controleTMG\ControleTMG.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\tools\NonRegression\detecteur\Detecteur.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\tools\NonRegression\ExtractionRente\ExtractionRente.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\tools\RepriseNavi\repriseNaviET\RepriseNaviET.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\tools\Tracfin\Tracfin.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\Apogee\Apogee.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\Chargements\Chrgt.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\CtrAss\CtrAss.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\Editiq\Editiq.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\ET\ET.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\Evenem\Evenem.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\GED\GED.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\Invest\Invest.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\Prestations\Presta.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\Produit_Garantie\ProGar.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\RefIp\RefIp.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\Rente\Rente.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\Server\Server.vbp",
                "APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\Versements\Versmt.vbp",
                "BAC|C:\Analyses_VB\VB6Apps\BAC\ArreteCie\AgentciePropre.vbp",
                "BAC|C:\Analyses_VB\VB6Apps\BAC\Journal\JournalPropre.vbp",
                "DCL|C:\Analyses_VB\VB6Apps\DCL\Dcl2022.vbp",
                "FLOTTES|C:\Analyses_VB\VB6Apps\FLOTTES\Bin\Flottes.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\AffectationApporteur\VB\AffectAppContrats.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\AlerteFourmi\VB\AlerteFourmi.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ApprocheClient\Vb\AppClients.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Arret_CICS\vb\Arret_CICS.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Bloque\VB\Bloque.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Client\Vb\Client.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\CompagNew\vb\CompagNew.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Compose\VB\Compose.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\COMPTE\Vb\compte.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\CONTRAT\Vb\Contrat.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\CopieAgence\Vb\CopieAgence.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Cpgcomm\Vb\CPGCOMM.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\CpgUtil\Vb\CpgUtil.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\CPTEAPP\Vb\cpteapp.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DecaissNew\vb\DecaissNew.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Dll_Previsu\Vb\SrvPrevisu.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllAct12Sit36\Vb\srvAct12Sit36.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllAutoImpr\Vb\SrvAutImp.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllBdd\Vb\srvBdd.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllBlocNotes\Vb\SrvBlNot.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllCalcSoldes\Vb\SrvCalcSoldes.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllCarteVerte\Vb\SrvImpreCV.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllClient\Vb\srvClient.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllConnectGEF2\vb\srvConnectGEF2.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllConnection\Vb\ExeConnectADO.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllContratVie\Vb\srvContratVie.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllDedoublonnage\Vb\srvDedoublonnage.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllDocPdf\Vb\SrvDocPdf.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllEcheancier\Vb\SrvEcheancier.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllEmail\Vb\SrvEmail.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllEvenement\Vb\srvEvenement.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllExcelPrint\Vb\SrvExcelPrint.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllFileNet\srvFileNet.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllFusionContratDoublonsVIE\FusionContratVIE.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllGaranties\Vb\dllGaranties.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllGestClient\Vb\SrvGestClient.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllGestTache\Vb\SrvGestTache.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllMessClient\Vb\srvMessClient.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllNote\Vb\srvPostIt.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllOMSynchro\Vb\srvOMSynchro.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllOptParRlv\Vb\OptPaRlv.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllParamImpr\Vb\SrvPaImp.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllPDTI\Vb\Dll_PDTI.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllRce\VB\SrvRce.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllReaffectation\Vb\srvNewReaffectation.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllReleve\Vb\SRVRELEV.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllSuiviCpg\vb\suivicpg.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DLLToolTips\Vb\SrvToolTips.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllTransit\Vb\srvTransition.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllTransit\Vb\srvTransition.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllTruncate\Vb\SrvTruncInfocentre.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Dosscli\Vb\Dosscli.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\EchGest\Vb\EchGest.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\EchParam\Vb\ECHPARAM.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\EclatementGB85\Vb\Eclat_GB85.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\EJUNew\Vb\EjuNew.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\EncaissNew\VB\EncaissNew.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Epuratio\Vb\Epuration.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ExeBOBI\Vb\ExeBOBI.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ExeConnect\Vb\SrvExeOracle.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ExeCopy\Vb\ExeCopy.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ExeLEA\Vb\LEA.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ExeLEA_ACD\Vb\PrgLea_Acd.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ExeListePrg\vb\SrvLstProgrammes.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ExeMusique\Vb\ExeMusique.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ExeRACF\Vb\RACF.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ExeTosca\Vb\Tosca.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ExportItool\Vb\ExportItool.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Flottes\vb\Flottes.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\fulight\Vb\Fulight.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Garages\vb\Garages.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\GestLibLibre\GestLibLibres.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\IMPDIFF\VB\Impdiff.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\InfocentreNew\Vb\Infocentre.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\JOURNALNew\Vb\JournalNew.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\LETCHEQ\VB\Letcheq.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\MajLot\vb\MajLot.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\MigBlocAdresse\Vb\MigBlcAdr.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\MigrationAvtNgo\vb\MigrationAvtNgo.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\MigrationCRM\vb\MigrTaches_CRM.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\objetApporteur\VB\objetApporteurt.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\objetTauxComm\VB\objTauxComm.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ODClientNew\Vb\ODClientNew.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ParamImp\VB\ParamImp.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Pollic\Vb\pollic.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Pont\Vb\Pont.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Pont\Vb_Ulis\Pont.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\PontCrm\Vb\PontCrm.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Pont-Echéancier\Vb\PontEcheancier.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\PontFourmiPdti\vb\PontFourmiPdti.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\PontPDTI\VB\PontPDTI.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Ponts\Vb\Pont.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\PontUlis\Vb\PontUlis.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\PRELEV\Vb\Prelev.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\PrfNew\Vb\PRFNEW.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Ptv_util\Vb\PtvUtil.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\QUITTCE\Vb\QUITTCE.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\RefPostal\vb\RefPostal.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\RELEVE\Vb\Releve.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\RequeteurCie\vb\RequeteurCie.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\RunElisaFourmi\vb\RunElisaFourmi.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\SIGNALEC\Vb\Signalec.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\SINISTRE\VB\Sinistre.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\SpiDesc\vb\spi.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\SpiDesc\ULSPI00\Ulspi00.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\SplitAvtNgo\vb\Split_AVT_NGO.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\SrvBureauXP\Vb\Bureau.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\STATGEN\VB\STATGEN.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Support\Composants\MaxBox\Vb\_MaxBox.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Support\Composants\Resizer\Vb\_Resiser.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Support\DetailGarantie\Atoll Dépendance\ATOLLDEP.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Support\DetailGarantie\Domicile\KJA-DOMICILE.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Support\DetailGarantie\NOA\KJA-DOMICILE.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Support\epur\epurmod.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\SynchroRCE\SynchroRCE.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\TABLE\Vb\TABLE.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Themes\Vb\Themes.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ThemesChamps\Vb\Champs.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\TIERDIV\Vb\TIERDIV.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Ulis\vb\Ulis.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\UlletchkNew\Vb\ulletchkNew.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\UtiNew\Vb\UtiNew.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\WordVisible\Vb\Projet1.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\ALIMOLE\VB\ALIMOLE.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\ALIMWSH24\VB\ALIMWSH24.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\ALOODCIE\VB\ALOODCIE.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\ANALYSEFG\Vb\AnalyseFG.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\ARCHIVE\VB\ARCHIVE.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Bascule\Vb\Bascule.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\BaseClient\Vb\BaseClient.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\BatEpur\Vb\BatEpur.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\BatMarq\Vb\BatMarque.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\BATSERV\VB\BATSERV.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\BatSifter\vb\BatSifter.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\BatSuppHG\vb\BatSuppHG.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\BDOLE\VB\BDOLE.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\CalcArr\Vb\CalcArr.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\ContratArchivable\VB\ContratArchivable.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\CPGSIEGE\VB\CPGSIEGE.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\DelProspect\VB\DelProspect.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\DllFusionContrat\VB\DllFusionContrat.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\DllFusionContrat\Lanceur\LanceurFusionContrat.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Eclatement\Vb\Eclatmt.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\ECLATEUR\VB\ECLATEUR.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\EXTRACT\VB\EXTRACT.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Findgest\Vb\FINDGEST.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Flux1MIFv1\VB\FLUX1MIFV1.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FLUX1V1\VB\FLUX1V1.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FLUX2V1\Vb\FLUX2V1.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FLUX3V1\VB\FLUX3V1.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FLUX4V1\VB\FLUX4V1.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FLUX6V1\Vb\FLUX6V1.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FLUX7V1\Vb\FLUX7V1.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Flux7v2\VB\Flux7v2.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Flux7v3\vb\Flux7v3.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FLUX8V1\Vb\FLUX8V1.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Flux17v1\Vb\Flux17v1.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Flux18v1\vb\Flux18v1.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Flux20v1\vb\Flux20v1.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Flux21v1\vb\Flux21v1.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Flux22v1\vb\Flux22v1.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Flux23v1\vb\flux23v1.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FLUX30V1\Vb\Flux30v1.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FLUX31V1\Vb\Flux31v1.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FLUX34V1\Vb\Flux34v1.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FluxBCv1\Vb\FluxBCv1.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FluxMIFInhibeV1\FuRestor\Vb\Restore.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FluxMIFInhibeV1\vb\FluxMIFInhibev1.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FourmiMaitre\FourmiMaitre.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FRMOLEEC\VB\FRMOLEEC.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FuSave\Vb\Save.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FusionAuto\Vb\FusionAuto.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FusionCie\Vb\FusionCie.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FusionFG\Vb\Fusionfg.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\GedBuro\Vb\CollecteurMiseEnGED.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\GestionCrmCla\Vb\PrjCrmCLa.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\GestionMultiQuittance\Vb\GestionMultiQuittance.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\IniPtVie\Vb\IniPtVie.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\InitBC\VB\InitBC.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FuSave\Vb\Save.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FusionAuto\Vb\FusionAuto.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FusionCie\Vb\FusionCie.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FusionFG\Vb\Fusionfg.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\GedBuro\Vb\CollecteurMiseEnGED.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\GestionCrmCla\Vb\PrjCrmCLa.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\GestionMultiQuittance\Vb\GestionMultiQuittance.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\IniPtVie\Vb\IniPtVie.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\InitBC\VB\InitBC.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\InitInterH24\VB\initialisationInter.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\LanceFusion\Vb\LanceFus.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\MajActSit\Vb\MajActSit.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\MajFileNetMdi\VB\MajFileNetMdi.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\MigBarometre\VB\MigrationBaro.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\MigrationCTR\vb\ANONYCTR.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\MiseAJourPDTI\Vb\majBaseHG.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\REPCLI\Vb\REPCLI.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Reuse_GDS\VB\Reuse_GDS.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Reuse_PdTI\VB\Reuse_PdTI.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\SCHDFOUR\Vb\SCHDFOUR.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\SuiviFlux\Vb\SUIVIFLUX.VBP",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\support\CodeInter\vb\CodeInter.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\TPSuiviExploit\VB\TPSuiviExploit.vbp",
                "FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\UtiNumAuto\VB\UtiNumClient.vbp",
                "FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\Blosoft_Run.vbp",
                "FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\ControleCotisation\ControleCotisation.vbp",
                "FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\ControleH24\ControleH24.vbp",
                "FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\CorrectionMigration\CorrectionArchivageMigration.vbp",
                "FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\Gestion Cie\ManCie.vbp",
                "FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\GestionInfocentre A confirmer\InfoCentreRobot.vbp",
                "FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\ItoolV2\ItoolV2.vbp",
                "FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\Liste des flux\ListeFlux.vbp",
                "FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\LogMdi\LogMdi.vbp",
                "FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\Parseur\Parseur.vbp",
                "FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\suiviMdi\SuiviMDI.vbp",
                "FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\TrtCurratifAnnulAcompte\TrtCurratifAnnulAcompte.vbp",
                "FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\Outils en cours\CieIdtCiEa0\Projet1.vbp",
                "FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\Outils en cours\cliidtclia0\Projet1.vbp",
                "FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\Outils en cours\CLSIDCLEANER\CLSIDFOURMICLEANER.vbp",
                "FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\Outils en cours\generateur\generateur.vbp",
                "FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\Outils en cours\RattrapageDédouble\decaissement\ratdedoudec.vbp",
                "FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\Outils en cours\RattrapageDédouble\encaissement\ratdedouenc.vbp",
                "GARAGES|C:\Analyses_VB\VB6Apps\GARAGES\Bin\Garages.vbp",
                "IT|C:\Analyses_VB\VB6Apps\IT\Interfaces.vbp",
                "JAUGE|C:\Analyses_VB\VB6Apps\FLOTTES\Bin\RealJauge\Jauge.vbp",
                "KOFAX|C:\Analyses_VB\VB6Apps\KOFAX\WFAgent\WFAgent\WFAgent.vbp",
                "KOFAX|C:\Analyses_VB\VB6Apps\GBPROD\GBPROD_WFAgent\GBPROD_WFAgent.vbp",
                "KOFAX|C:\Analyses_VB\VB6Apps\KOFAX\CreationLot\DisplayBatchID.vbp",
                "KOFAX|C:\Analyses_VB\VB6Apps\KOFAX\XMLRelease\XML Release Doc\XMLReleaseDocument.vbp",
                "KOFAX|C:\Analyses_VB\VB6Apps\GBPROD\GBPROD_XMLRelease\GBPROD_XMLReleaseDocument.vbp",
                "KOFAX|C:\Analyses_VB\VB6Apps\KOFAX\NewRel\ReleaseXMLAID\RelXMLRoutage.vbp",
                "KOFAX|C:\Analyses_VB\VB6Apps\KOFAX\Ocx Validation\Validation.vbp",
                "KOFAX|C:\Analyses_VB\VB6Apps\KOFAX\RelQRCODEDecoup\ReleaseXMLAID\RelQRCODEDecoup.vbp",
                "KOFAX|C:\Analyses_VB\VB6Apps\KOFAX\RelTIFKtmKta - Version TIF et XML\ReleaseXMLAID\RelTIFKtmKta.vbp",
                "MAESTRO|C:\Analyses_VB\VB6Apps\MAESTRO\Courrier.vbp",
                "MESSAUTO|C:\Analyses_VB\VB6Apps\MESSAUTO\copiesMail\CopiesMail.vbp",
                "MESSAUTO|C:\Analyses_VB\VB6Apps\MESSAUTO\Decompte2\Decompte2.vbp",
                "MESSAUTO|C:\Analyses_VB\VB6Apps\MESSAUTO\LctTrt\LctTrt.vbp",
                "MESSAUTO|C:\Analyses_VB\VB6Apps\MESSAUTO\MajFolios\MajFolios.vbp",
                "MESSAUTO|C:\Analyses_VB\VB6Apps\MESSAUTO\Traitement2\TraitementOutlook2.vbp",
                "MESSAUTO|C:\Analyses_VB\VB6Apps\MESSAUTO\transco\Transco.vbp",
                "MESSAUTO|C:\Analyses_VB\VB6Apps\MESSAUTO\Transco2\Transco2.vbp",
                "OPTIFLUX|C:\Analyses_VB\VB6Apps\OPTIFLUX\ITO\ITO.vbp",
                "OPTIFLUX|C:\Analyses_VB\VB6Apps\OPTIFLUX\OCO\OCOX.vbp",
                "SPORT|C:\Analyses_VB\VB6Apps\SPORT\SPORT_dev\SURVEILLANCE.vbp"
            }

        ListeDesApplications = New List(Of String)() From {
            "AGIRA",
            "APOGEE",
            "BAC",
            "DCL",
            "FLOTTES",
            "FOURMI",
            "FOURMI-TOOL",
            "GARAGES",
            "IT",
            "JAUGE",
            "KOFAX",
            "MAESTRO",
            "MESSAUTO",
            "OPTIFLUX",
            "SPORT"
        }

        ListeDeProject = Nothing

    End Sub

#End Region


#Region "Analyses"

    Private Sub Analyse()


        Try

            Dim szChemin As String
            Dim szApplication As String
            Dim szArray0 As String()

            ListeDeProject = New List(Of ProjetVBP)
            ListeDesFichierSource = New List(Of FicSource)
            ListeDesRefOCx = New List(Of RefComp)

            For Each objProj In ListeDesFichiersVBP

                szArray0 = objProj.Split("|")
                szApplication = szArray0(0)
                szChemin = szArray0(1)
                CMF.FileName = szChemin

                ' Using monStreamReader As StreamReader = New StreamReader(szChemin, Encoding.Default)
                Using monStreamReader = New System.IO.StreamReader(New System.IO.FileStream(szChemin, System.IO.FileMode.Open), System.Text.Encoding.Latin1)

                    Dim ligne As String
                    ligne = monStreamReader.ReadLine()

                    SelectedProjetVBP = New ProjetVBP With {
                        .NomApplication = szApplication,
                        .NomDuFichierVBP = CMF.SafeFileName,
                        .Emplacement = System.IO.Path.GetFullPath(szArray0(1).Replace(CMF.SafeFileName, String.Empty))
                    }

                    Do While (ligne IsNot Nothing)

                        ' Test Type de Projet
                        If (ligne.StartsWith("Type")) Then
                            If (ligne.Contains("Type=Exe")) Then
                                SelectedProjetVBP.TypeDeProjet = "Exe"
                            ElseIf (ligne.Contains("Type=OleExe")) Then
                                SelectedProjetVBP.TypeDeProjet = "OleExe"
                            ElseIf (ligne.Contains("OleDll")) Then
                                SelectedProjetVBP.TypeDeProjet = "OleDll"
                            ElseIf (ligne.Contains("Control")) Then
                                SelectedProjetVBP.TypeDeProjet = "Control"
                            End If
                        End If

                        ' Test NomDuProjet
                        If (ligne.StartsWith("Name")) Then
                            SelectedProjetVBP.NomDuProjet = ExtraireValeurEgal(ligne)
                        End If

                        ' Test NomDeCompilation
                        If (ligne.StartsWith("ExeName32")) Then
                            SelectedProjetVBP.NomDeCompilation = ExtraireValeurEgal(ligne)
                        End If

                        '  Test Startup
                        If (ligne.StartsWith("Startup")) Then
                            SelectedProjetVBP.Startup = ExtraireValeurEgal(ligne)
                        End If

                        '  Test Reference
                        If (ligne.StartsWith("Reference")) Then
                            SelectedRefComp = New RefComp
                            SelectedRefComp.NomApplication = SelectedProjetVBP.NomApplication
                            SelectedRefComp.NomDuProjet = SelectedProjetVBP.NomDuFichierVBP
                            SelectedRefComp.ExtraitDeLigneVBP = ligne
                            SelectedRefComp.MotClef = 1

                            SelectedProjetVBP.ListeDeReference.Add(ExtraireReference(ligne))
                            SelectedProjetVBP.NombreReferenceDLL += 1

                            ListeDesRefOCx.Add(SelectedRefComp)
                        End If

                        '  Test Object
                        If (ligne.StartsWith("Object")) Then
                            SelectedRefComp = New RefComp
                            SelectedRefComp.NomApplication = SelectedProjetVBP.NomApplication
                            SelectedRefComp.NomDuProjet = SelectedProjetVBP.NomDuFichierVBP
                            SelectedRefComp.ExtraitDeLigneVBP = ligne
                            SelectedRefComp.MotClef = 0

                            SelectedProjetVBP.ListeDActiveX.Add(ExtraireObject(ligne))
                            SelectedProjetVBP.NombreDActiveX += 1

                            ListeDesRefOCx.Add(SelectedRefComp)
                        End If

                        '  Test UserControl
                        If (ligne.StartsWith("UserControl")) Then
                            SelectedProjetVBP.ListeUserControles.Add(ExtraireUserCtrl(ligne))
                            SelectedProjetVBP.NombreDeUserControles += 1
                        End If

                        '  Test Formulaire
                        If (ligne.StartsWith("Form")) Then
                            Dim obj = ExtraireFichier(ligne, TypeSource.TypeFichier.Frm)
                            ListeDesFichierSource.Add(obj)
                            SelectedProjetVBP.ListeFormulaire.Add(obj)
                            SelectedProjetVBP.NombreDeFormulaire += 1
                            obj = Nothing
                        End If

                        '  Test Module
                        If (ligne.StartsWith("Module")) Then
                            Dim obj = ExtraireFichier(ligne, TypeSource.TypeFichier.ModuleBas)
                            ListeDesFichierSource.Add(obj)
                            SelectedProjetVBP.ListeDeModule.Add(obj)
                            SelectedProjetVBP.NombreDeModuleBas += 1
                            obj = Nothing
                        End If

                        '  Test Classe
                        If (ligne.StartsWith("Class")) Then
                            Dim obj = ExtraireFichier(ligne, TypeSource.TypeFichier.Classe)
                            ListeDesFichierSource.Add(obj)
                            SelectedProjetVBP.ListeClasse.Add(obj)
                            SelectedProjetVBP.NombreDeClasse += 1
                            obj = Nothing
                        End If

                        ligne = monStreamReader.ReadLine()

                    Loop

                    ListeDeProject.Add(SelectedProjetVBP)

                End Using

            Next

        Catch ex As Exception
            MsgBox(SelectedProjetVBP.NomDuFichierVBP + ex.Message)
        End Try

    End Sub

    Private Sub AnalyserFichier(ByRef objFic As FicSource)

        Try

            Dim ligne, valtest As String
            Dim nbLignes = 0

            Using monStreamReader As StreamReader = New StreamReader(objFic.Emplacement)

                ligne = monStreamReader.ReadLine()

                Do While (ligne IsNot Nothing)

                    If (ligne.Trim().StartsWith("'") = False) Then

                        nbLignes += 1
                        Dim objRegeX = New RegexFound()

                        If (ligne.Contains("Declare Function")) Then

                            objRegeX.Position = nbLignes
                            objRegeX.MotClef = RegexFound.LeMotClef.Api32

                            If (ligne.Length >= 255) Then
                                objRegeX.ExtraitDeLigne = Trim(ligne.Substring(0, 255))
                            Else
                                objRegeX.ExtraitDeLigne = Trim(ligne)
                            End If

                            objFic.ListofRegeX.Add(objRegeX)
                            objFic.NombreDApi32 += 1

                            SelectedProjetVBP.NombreDApi32 += 1

                        End If

                        If (ligne.Contains("CreateObject(")) Then

                            If (ligne.Contains("Excel.Application")) Then

                                objRegeX.Position = nbLignes
                                objRegeX.MotClef = RegexFound.LeMotClef.ExcelApp

                                If (ligne.Length >= 255) Then
                                    objRegeX.ExtraitDeLigne = Trim(ligne.Substring(0, 255))
                                Else
                                    objRegeX.ExtraitDeLigne = Trim(ligne)
                                End If

                                objFic.ListofRegeX.Add(objRegeX)
                                objFic.NombreDeExcelApp += 1

                                SelectedProjetVBP.NombreDeExcelApp += 1

                            ElseIf (ligne.Contains("Word.Application")) Then

                                objRegeX.Position = nbLignes
                                objRegeX.MotClef = RegexFound.LeMotClef.WordApp

                                If (ligne.Length >= 255) Then
                                    objRegeX.ExtraitDeLigne = Trim(ligne.Substring(0, 255))
                                Else
                                    objRegeX.ExtraitDeLigne = Trim(ligne)
                                End If

                                objFic.ListofRegeX.Add(objRegeX)
                                objFic.NombreDeWordApp += 1

                                SelectedProjetVBP.NombreDeWordApp += 1

                            Else

                                objRegeX.Position = nbLignes
                                objRegeX.MotClef = RegexFound.LeMotClef.CreateObject

                                If (ligne.Length >= 255) Then
                                    objRegeX.ExtraitDeLigne = Trim(ligne.Substring(0, 255))
                                Else
                                    objRegeX.ExtraitDeLigne = Trim(ligne)
                                End If

                                objFic.ListofRegeX.Add(objRegeX)
                                objFic.NombreDeCreateObject += 1

                                SelectedProjetVBP.NombreDeCreateObject += 1

                            End If

                        End If

                        If (ligne.Contains("UserControl")) Then

                            objRegeX.Position = nbLignes
                            objRegeX.MotClef = RegexFound.LeMotClef.UserCtrl

                            If (ligne.Length >= 255) Then
                                objRegeX.ExtraitDeLigne = Trim(ligne.Substring(0, 255))
                            Else
                                objRegeX.ExtraitDeLigne = Trim(ligne)
                            End If

                            objFic.ListofRegeX.Add(objRegeX)
                            objFic.NombreDeUserControles += 1

                            SelectedProjetVBP.NombreDeUserControles += 1

                        End If

                    Else
                        'tu peux créer ici un fichier des lignes ignorées 
                        valtest = ligne
                    End If

                    ligne = monStreamReader.ReadLine()

                Loop

            End Using

            objFic.NombreDeLignesActives = nbLignes
            SelectedProjetVBP.NombreDeLignesActives = SelectedProjetVBP.NombreDeLignesActives + nbLignes

            If (objFic.NombreDeCreateObject > 1) Then
                If (objFic.NombreDeExcelApp = 0) And (objFic.NombreDeWordApp = 0) Then
                    objFic.NombreDeOfficeouAutre = objFic.NombreDeCreateObject
                    SelectedProjetVBP.NombreDeOfficeouAutre = SelectedProjetVBP.NombreDeOfficeouAutre + objFic.NombreDeCreateObject
                End If
            End If

        Catch ex As Exception
            MsgBox(SelectedProjetVBP.NomDuFichierVBP + " " + ex.Message)
            ListeDesFichiersNonTrouves.Add(objFic.Emplacement + objFic.NomExtFicSource)
        End Try

    End Sub

#End Region


#Region "Extraction_Data_Creation_CSV"

    Private Sub CreerFichierCSV(ByVal oListeDeProject As List(Of ProjetVBP))

        Try
            ' Détail Par Applications
            FileOpen(1, "C:\_ScannerCSV\" & "APPLICATIONS_VB.CSV", OpenMode.Output)
            Dim enteteDuFichier = "APPLICATION;" & "PROJET;" & "NB_REFERENCES;" & "NB_FORMULAIRES;" & "NB_OCX_CONTROLES;" & "NB_USERCONTROLES;" & "NB_MODULES;" & "NB_CLASSES;"

            PrintLine(1, enteteDuFichier)
            For Each oneProjet As ProjetVBP In oListeDeProject

                Dim UneLigneProjet As String
                Try
                    UneLigneProjet = oneProjet.NomApplication & ";"
                    UneLigneProjet = UneLigneProjet & oneProjet.NomDuProjet & ";" & oneProjet.NombreReferenceDLL.ToString & ";" & oneProjet.NombreDeFormulaire.ToString & ";"
                    UneLigneProjet = UneLigneProjet & oneProjet.NombreDActiveX.ToString & ";" & oneProjet.NombreDeUserControles.ToString & ";" & oneProjet.NombreDeModuleBas.ToString & ";"
                    UneLigneProjet = UneLigneProjet & oneProjet.NombreDeClasse.ToString

                    PrintLine(1, UneLigneProjet)

                Catch ex As Exception
                End Try

            Next
            FileClose(1)

            'Application | Projet | Type (OCX ou REF) | Fichier (OCX/DLL/TLB) | Libellé (uniquement pour les refs) | Ligne du VBP
            FileOpen(1, "C:\_ScannerCSV\" & "OCX_REF_DLL_VB.CSV", OpenMode.Output)
            enteteDuFichier = "APPLICATION;" & "PROJET;" & "Type (OCX ou REF);" & "Fichier (OCX/DLL/TLB);" & "Libellé (uniquement pour les refs);" & "Ligne du VBP;"
            PrintLine(1, enteteDuFichier)
            For Each oneRefComp As RefComp In ListeDesRefOCx

                Dim UneLigneProjet As String
                Try
                    UneLigneProjet = oneRefComp.NomApplication & ";"
                    UneLigneProjet = UneLigneProjet & oneRefComp.NomDuProjet & ";"

                    Select Case oneRefComp.MotClef
                        Case 0
                            UneLigneProjet = UneLigneProjet & "OCX"
                            Exit Select
                        Case 1
                            UneLigneProjet = UneLigneProjet & "REF"
                            Exit Select
                    End Select

                    UneLigneProjet = UneLigneProjet & ";" & oneRefComp.NomDuFichierRefOCxTlb & ";" & oneRefComp.libelleRefOCxTlb & ";" & oneRefComp.ExtraitDeLigneVBP


                    PrintLine(1, UneLigneProjet)

                Catch ex As Exception
                End Try

            Next

            FileClose(1)

            ' Detail Par Projet
            Dim rq01 = From obj In ListeDeProject Order By obj.NomApplication, obj.NomDuProjet Select obj
            FileOpen(1, "C:\_ScannerCSV\" & "LISTE_DES_PROJETS.CSV", OpenMode.Output)

            enteteDuFichier = "APPLICATION;" & "PROJET;" & "NB_REFERENCES;" & "NB_FORMULAIRES;" & "NB_OCX_CONTROLES;" & "NB_USERCONTROLES;" & "NB_MODULES;" & "NB_CLASSES;" & "NB_CREATE_OBJECT;" & "NB_API32;" & "NB_WORD;" & "NB_EXCEL;" & "NB OFFICE;" & "NB_LIGNES_ACTIVES"

            PrintLine(1, enteteDuFichier)
            For Each oneProjet As ProjetVBP In rq01

                Dim UneLigneProjet As String
                Try

                    UneLigneProjet = oneProjet.NomApplication & ";" + oneProjet.NomDuProjet & ";" & oneProjet.NombreReferenceDLL.ToString & ";" & oneProjet.NombreDeFormulaire.ToString() & ";"
                    UneLigneProjet = UneLigneProjet & oneProjet.NombreDActiveX & ";" & oneProjet.NombreDeUserControles & ";" & oneProjet.NombreDeModuleBas.ToString() & ";"
                    UneLigneProjet = UneLigneProjet & oneProjet.NombreDeClasse & ";" & oneProjet.NombreDeCreateObject & ";" & oneProjet.NombreDApi32.ToString() & ";"
                    UneLigneProjet = UneLigneProjet & oneProjet.NombreDeWordApp & ";" & oneProjet.NombreDeExcelApp & ";" & oneProjet.NombreDeOfficeouAutre.ToString() & ";"
                    UneLigneProjet = UneLigneProjet & oneProjet.NombreDeLignesActives.ToString()

                    PrintLine(1, UneLigneProjet)

                Catch ex As Exception
                End Try

            Next
            FileClose(1)


            ' Details par Fichier Source FRM,MOD,CLS
            Dim rq02 = From obj In ListeDesFichierSource Order By obj.NomApplication, obj.NomDuFichierVBP Select obj

            FileOpen(1, "C:\_ScannerCSV\" & "FICHIERS_SOURCES.CSV", OpenMode.Output)
            FileOpen(2, "C:\_ScannerCSV\" & "CR_API32_WORD_EXCEL.CSV", OpenMode.Output)

            Dim enteteDuFichier01 = "APPLICATION;" & "PROJET;" & "FICHIER;" & "NB_CREATE_OBJECT;" & "NB_API32;" & "NB_WORD;" & "NB_EXCEL;" & "NB OFFICE;" & "NB_LIGNES_ACTIVES"
            Dim enteteDuFichier02 = "APPLICATION;" & "PROJET;" & "FICHIER;" & "NB_TYPE;" & "POSITION;" & "EXTRAIT"

            PrintLine(1, enteteDuFichier01)
            PrintLine(2, enteteDuFichier02)
            For Each oneFichier As FicSource In rq02

                Dim UneLigne As String
                Dim UnDetail As String

                Try

                    UneLigne = oneFichier.NomApplication & ";" + oneFichier.NomDuFichierVBP & ";" & oneFichier.NomExtFicSource & ";"
                    UneLigne = UneLigne & oneFichier.NombreDeCreateObject.ToString() & ";" & oneFichier.NombreDApi32.ToString() & ";"
                    UneLigne = UneLigne & oneFichier.NombreDeWordApp.ToString() & ";" & oneFichier.NombreDeExcelApp.ToString() & ";"
                    UneLigne = UneLigne & oneFichier.NombreDeOfficeouAutre.ToString() & ";" & oneFichier.NombreDeLignesActives.ToString()

                    PrintLine(1, UneLigne)

                    If oneFichier.ListofRegeX.Count > 0 Then

                        For Each oneRegex As RegexFound In oneFichier.ListofRegeX

                            UnDetail = oneFichier.NomApplication & ";" + oneFichier.NomDuFichierVBP & ";" & oneFichier.NomExtFicSource & ";"

                            Select Case oneRegex.MotClef
                                Case 0
                                    UnDetail = UnDetail & "Api32"
                                    Exit Select
                                Case 1
                                    UnDetail = UnDetail & "CreateObject"
                                    Exit Select
                                Case 2
                                    UnDetail = UnDetail & "ExcelApp"
                                    Exit Select
                                Case 3
                                    UnDetail = UnDetail & "WordApp"
                                    Exit Select
                                Case Else
                                    UnDetail = UnDetail & "UserCtrl"
                                    Exit Select
                            End Select

                            UnDetail = UnDetail & ";" & oneRegex.Position.ToString() & ";" + oneRegex.ExtraitDeLigne
                            PrintLine(2, UnDetail)

                        Next

                    End If

                Catch ex As Exception

                End Try

            Next

            FileClose(1)
            FileClose(2)

            If ListeDesFichiersNonTrouves.Any() Then
                FileOpen(1, "C:\_ScannerCSV\" & "LISTE_DES_MANQUANTS.CSV", OpenMode.Output)
                For Each oneFile In ListeDesFichiersNonTrouves
                    PrintLine(1, oneFile)
                Next
                FileClose(1)
            End If

        Catch ex As Exception
            FileClose(1)
        End Try

    End Sub


    Private Function ExtraireValeurEgal(ByVal objString As String) As String
        Dim szArray = objString.Split("=")
        Return szArray(1).Replace("""", String.Empty)
    End Function

    Private Function ExtraireReference(objString As String) As ReferenceDLL

        Dim szArray1 = objString.Split("=")
        Dim szArray2 As String()
        Dim szArray3 As String()

        Dim objRef = New ReferenceDLL()

        If szArray1(1).Where(Function(x) Equals("\")).Count() >= 1 Then
            If szArray1(1).Where(Function(x) Equals("#")).Count() = 0 Then
                ' Reference =*\ A..\Server\Server.vbp
                szArray2 = szArray1(1).Split("\")
                objRef.NomDeLaReferenceDLL = szArray2(szArray2.Length - 1).Trim()
                objRef.NomDuFichiereDLL = szArray1(1).TrimStart()
            Else
                ' Reference =*\ G{91147A58-DFE4-47C0-8E76-987FC1A6001B}#3.0#0#C:\Program Files\Fichiers communs\MSSoap\Binaries\MSSOAP30.dll#Microsoft Soap Type Library v3.0
                szArray2 = szArray1(1).Split("#")
                objRef.NomDeLaReferenceDLL = szArray2(szArray2.Length - 1).Trim()
                objRef.NomDuFichiereDLL = szArray2(szArray2.Length - 2).Trim()
                szArray3 = szArray2(szArray2.Length - 2).Trim().Split("\")
                SelectedRefComp.NomDuFichierRefOCxTlb = szArray3(szArray3.Length - 1).Trim()
            End If
        End If

        SelectedRefComp.libelleRefOCxTlb = objRef.NomDeLaReferenceDLL

        szArray1 = Nothing
        szArray2 = Nothing
        szArray3 = Nothing

        Return objRef

    End Function

    Private Function ExtraireObject(ByVal objString As String) As ObjectActiveX

        Dim objActX = New ObjectActiveX()
        Dim szArray1 = objString.Split("=")
        Dim szArray2 As String()

        If szArray1(1).Where(Function(x) Equals("\")).Count() > 1 AndAlso Not szArray1(1).Contains(";") Then
            ' Object =*\ A..\Communs\Controles\prjControles.vbp
            szArray2 = szArray1(1).Split("\")
            objActX.NomObjectActiveX = szArray2(szArray2.Length - 1).Trim()
        ElseIf szArray1(1).Where(Function(x) Equals("\")).Count() = 0 AndAlso szArray1(1).Contains(";") Then
            ' Object = {831.0FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.1#0; mscomctl.ocx
            szArray2 = objString.Split(";")
            objActX.NomObjectActiveX = szArray2(1).Trim()
        End If


        SelectedRefComp.NomDuFichierRefOCxTlb = objActX.NomObjectActiveX

        szArray1 = Nothing
        szArray2 = Nothing

        Return objActX
    End Function

    Private Function ExtraireUserCtrl(ByVal objString As String) As UserControle
        Dim szArray1 = objString.Split("=")
        Dim objUserCtrl = New UserControle()
        objUserCtrl.NomUserControle = szArray1(1)
        szArray1 = Nothing
        Return objUserCtrl
    End Function

    Private Function ExtraireFichier(objLigne As String, oTypeFichier As Byte) As FicSource
        Dim szArray1, szArray2, szArray3 As String()

        szArray1 = objLigne.Split("=")

        Dim objFicSource = New FicSource() With
        {
        .NomApplication = SelectedProjetVBP.NomApplication,
        .NomDuFichierVBP = SelectedProjetVBP.NomDuFichierVBP,
        .TypeFicSource = CType(oTypeFichier, TypeSource.TypeFichier)
        }

        Try

            Select Case oTypeFichier

                Case 0

                    ' Form=Feuilles\frmAccueil.frm
                    szArray2 = szArray1(1).Split("\")

                    objFicSource.NomIntFicSource = szArray2(1)
                    objFicSource.NomExtFicSource = szArray2(1)

                    objFicSource.Emplacement = SelectedProjetVBP.Emplacement.ToString() & szArray1(1)
                    Exit Select

                Case 1, 2

                    ' Module=Demarrage;Modules\Demarrage.bas
                    ' Class=clIntermediaire;Classes\clIntermediaire.cls
                    szArray2 = szArray1(1).Split(";")
                    szArray3 = szArray2(1).Split("\")

                    objFicSource.NomIntFicSource = szArray2(0).Trim()
                    objFicSource.NomExtFicSource = szArray3(1).Trim()

                    objFicSource.Emplacement = SelectedProjetVBP.Emplacement.ToString() & Trim(szArray2(1))
                    Exit Select

            End Select

            AnalyserFichier(objFicSource)

            szArray1 = Nothing
            szArray2 = Nothing
            szArray3 = Nothing

        Catch ex As Exception

            MsgBox(SelectedProjetVBP.NomDuFichierVBP.ToString() & ex.Message)

        End Try

        Return objFicSource
    End Function

#End Region

#Region "Interaction_IHM"
    Private Sub BtnAnalyse_Click(sender As Object, e As EventArgs) Handles BtnAnalyse.Click
        Me.Cursor = Cursors.WaitCursor
        Analyse()
        Me.Cursor = Cursors.Default
        MsgBox("Analyse terminée",, "Scanner de Projets Visual Basic 6")
    End Sub

    Private Sub BtnExporter_Click(sender As Object, e As EventArgs) Handles BtnExporter.Click
        Cursor = Cursors.WaitCursor
        CreerFichierCSV(ListeDeProject)
        Cursor = Cursors.Default
        MsgBox("Export CSV terminé",, "Scanner de Projets Visual Basic 6")
    End Sub

    Private Sub BtnQuitter_Click(sender As Object, e As EventArgs) Handles BtnQuitter.Click
        End
    End Sub

    Private Sub BtnRecherche_Click(sender As Object, e As EventArgs) Handles BtnRecherche.Click
        Me.Cursor = Cursors.WaitCursor

        Me.Cursor = Cursors.Default
        MsgBox("Recherche terminée",, "Scanner de Projets Visual Basic 6")
    End Sub

#End Region

End Class