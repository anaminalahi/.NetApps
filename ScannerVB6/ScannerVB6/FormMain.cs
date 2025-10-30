using Microsoft.VisualBasic;
using ScannerVB6.Model;

namespace ScannerVB6
{
    public partial class FormMain : Form
    {

        #region Proprietes

            public List<ProjetVBP>? ListeDeProject { get; set; }
            public List<string>? ListeDesApplications { get; set; }
            public List<string>? ListeDesFichiersVBP { get; set; }
            public List<string>? ListeDesFichiersNonTrouves { get; set; } 
            public List<FicSource>? ListeDesFichierSource { get; set; }
            public List<RefComp>? ListeDesRefOCx { get; set; }
            public ProjetVBP? SelectedProjetVBP { get; set; }
            public FicSource? SelectedFormulaire { get; set; }
            public FicSource? SelectedModule { get; set; }
            public FicSource? SelectedClsClasse { get; set; }
            public RefComp? SelectedRefComp { get; set; }

        #endregion


        #region Constructeur

            public FormMain()
            {
                InitializeComponent();

                // Initialisation de La liste des Applications
                ListeDesApplications = new List<string>()
                {
                    "AGIRA",
                    "APOGEE",
                    "BAC",
                    "DCL",
                    "FLOTTES",
                    "FOURMI",
                    "FOURMI-TOOL",
                    "GARAGES",
                    "GBPROD",
                    "IT",
                    "JAUGE",
                    "KOFAX",
                    "MAESTRO",
                    "MESSAUTO",
                    "OPTIFLUX",
                    "SPORT"
                };

                // Initialisation de La liste des projets
                ListeDesFichiersVBP = new List<string>()
                {
                    @"AGIRA|C:\Analyses_VB\VB6Apps\AGIRA\Suivi\Suivi_Agira.vbp",
                    @"AGIRA|C:\Analyses_VB\VB6Apps\AGIRA\Import\ImportAGIRA.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\tools\ExtractionActuariat\Actuariat1\Actuariat1.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\tools\ExtractionActuariat\Actuariat2\Actuariat2.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\tools\MaintenanceET\maintenance\maintenance.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\tools\MaintenanceET\creerBilanAnnuelManquant\CreerBAManquant.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\tools\NonRegression\controleBilanAnnuel\ControleBilanAnnuel.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\tools\NonRegression\controleTMG\ControleTMG.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\tools\NonRegression\detecteur\Detecteur.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\tools\NonRegression\ExtractionRente\ExtractionRente.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\tools\RepriseNavi\repriseNaviET\RepriseNaviET.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\tools\Tracfin\Tracfin.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\Apogee\Apogee.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\Chargements\Chrgt.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\CtrAss\CtrAss.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\Editiq\Editiq.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\ET\ET.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\Evenem\Evenem.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\GED\GED.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\Invest\Invest.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\Prestations\Presta.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\Produit_Garantie\ProGar.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\RefIp\RefIp.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\Rente\Rente.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\Server\Server.vbp",
                    @"APOGEE|C:\Analyses_VB\VB6Apps\APOGEE\src\Versements\Versmt.vbp",
                    @"BAC|C:\Analyses_VB\VB6Apps\BAC\ArreteCie\AgentciePropre.vbp",
                    @"BAC|C:\Analyses_VB\VB6Apps\BAC\Journal\JournalPropre.vbp",
                    @"DCL|C:\Analyses_VB\VB6Apps\DCL\Dcl2022.vbp",
                    @"FLOTTES|C:\Analyses_VB\VB6Apps\FLOTTES\Bin\Flottes.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\AffectationApporteur\VB\AffectAppContrats.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\AlerteFourmi\VB\AlerteFourmi.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ApprocheClient\Vb\AppClients.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Arret_CICS\vb\Arret_CICS.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Bloque\VB\Bloque.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Client\Vb\Client.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\CompagNew\vb\CompagNew.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Compose\VB\Compose.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\COMPTE\Vb\compte.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\CONTRAT\Vb\Contrat.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\CopieAgence\Vb\CopieAgence.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Cpgcomm\Vb\CPGCOMM.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\CpgUtil\Vb\CpgUtil.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\CPTEAPP\Vb\cpteapp.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DecaissNew\vb\DecaissNew.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Dll_Previsu\Vb\SrvPrevisu.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllAct12Sit36\Vb\srvAct12Sit36.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllAutoImpr\Vb\SrvAutImp.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllBdd\Vb\srvBdd.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllBlocNotes\Vb\SrvBlNot.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllCalcSoldes\Vb\SrvCalcSoldes.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllCarteVerte\Vb\SrvImpreCV.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllClient\Vb\srvClient.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllConnectGEF2\vb\srvConnectGEF2.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllConnection\Vb\ExeConnectADO.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllContratVie\Vb\srvContratVie.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllDedoublonnage\Vb\srvDedoublonnage.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllDocPdf\Vb\SrvDocPdf.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllEcheancier\Vb\SrvEcheancier.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllEmail\Vb\SrvEmail.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllEvenement\Vb\srvEvenement.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllExcelPrint\Vb\SrvExcelPrint.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllFileNet\srvFileNet.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllFusionContratDoublonsVIE\FusionContratVIE.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllGaranties\Vb\dllGaranties.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllGestClient\Vb\SrvGestClient.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllGestTache\Vb\SrvGestTache.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllMessClient\Vb\srvMessClient.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllNote\Vb\srvPostIt.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllOMSynchro\Vb\srvOMSynchro.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllOptParRlv\Vb\OptPaRlv.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllParamImpr\Vb\SrvPaImp.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllPDTI\Vb\Dll_PDTI.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllRce\VB\SrvRce.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllReaffectation\Vb\srvNewReaffectation.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllReleve\Vb\SRVRELEV.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllSuiviCpg\vb\suivicpg.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DLLToolTips\Vb\SrvToolTips.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllTransit\Vb\srvTransition.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllTransit\Vb\srvTransition.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\DllTruncate\Vb\SrvTruncInfocentre.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Dosscli\Vb\Dosscli.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\EchGest\Vb\EchGest.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\EchParam\Vb\ECHPARAM.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\EclatementGB85\Vb\Eclat_GB85.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\EJUNew\Vb\EjuNew.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\EncaissNew\VB\EncaissNew.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Epuratio\Vb\Epuration.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ExeBOBI\Vb\ExeBOBI.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ExeConnect\Vb\SrvExeOracle.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ExeCopy\Vb\ExeCopy.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ExeLEA\Vb\LEA.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ExeLEA_ACD\Vb\PrgLea_Acd.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ExeListePrg\vb\SrvLstProgrammes.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ExeMusique\Vb\ExeMusique.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ExeRACF\Vb\RACF.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ExeTosca\Vb\Tosca.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ExportItool\Vb\ExportItool.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Flottes\vb\Flottes.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\fulight\Vb\Fulight.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Garages\vb\Garages.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\GestLibLibre\GestLibLibres.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\IMPDIFF\VB\Impdiff.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\InfocentreNew\Vb\Infocentre.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\JOURNALNew\Vb\JournalNew.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\LETCHEQ\VB\Letcheq.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\MajLot\vb\MajLot.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\MigBlocAdresse\Vb\MigBlcAdr.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\MigrationAvtNgo\vb\MigrationAvtNgo.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\MigrationCRM\vb\MigrTaches_CRM.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\objetApporteur\VB\objetApporteurt.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\objetTauxComm\VB\objTauxComm.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ODClientNew\Vb\ODClientNew.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ParamImp\VB\ParamImp.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Pollic\Vb\pollic.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Pont\Vb\Pont.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Pont\Vb_Ulis\Pont.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\PontCrm\Vb\PontCrm.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Pont-Echéancier\Vb\PontEcheancier.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\PontFourmiPdti\vb\PontFourmiPdti.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\PontPDTI\VB\PontPDTI.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Ponts\Vb\Pont.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\PontUlis\Vb\PontUlis.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\PRELEV\Vb\Prelev.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\PrfNew\Vb\PRFNEW.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Ptv_util\Vb\PtvUtil.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\QUITTCE\Vb\QUITTCE.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\RefPostal\vb\RefPostal.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\RELEVE\Vb\Releve.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\RequeteurCie\vb\RequeteurCie.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\RunElisaFourmi\vb\RunElisaFourmi.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\SIGNALEC\Vb\Signalec.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\SINISTRE\VB\Sinistre.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\SpiDesc\vb\spi.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\SpiDesc\ULSPI00\Ulspi00.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\SplitAvtNgo\vb\Split_AVT_NGO.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\SrvBureauXP\Vb\Bureau.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\STATGEN\VB\STATGEN.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Support\Composants\MaxBox\Vb\_MaxBox.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Support\Composants\Resizer\Vb\_Resiser.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Support\DetailGarantie\Atoll Dépendance\ATOLLDEP.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Support\DetailGarantie\Domicile\KJA-DOMICILE.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Support\DetailGarantie\NOA\KJA-DOMICILE.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Support\epur\epurmod.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\SynchroRCE\SynchroRCE.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\TABLE\Vb\TABLE.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Themes\Vb\Themes.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\ThemesChamps\Vb\Champs.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\TIERDIV\Vb\TIERDIV.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\Ulis\vb\Ulis.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\UlletchkNew\Vb\ulletchkNew.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\UtiNew\Vb\UtiNew.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi\WordVisible\Vb\Projet1.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\ALIMOLE\VB\ALIMOLE.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\ALIMWSH24\VB\ALIMWSH24.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\ALOODCIE\VB\ALOODCIE.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\ANALYSEFG\Vb\AnalyseFG.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\ARCHIVE\VB\ARCHIVE.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Bascule\Vb\Bascule.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\BaseClient\Vb\BaseClient.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\BatEpur\Vb\BatEpur.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\BatMarq\Vb\BatMarque.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\BATSERV\VB\BATSERV.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\BatSifter\vb\BatSifter.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\BatSuppHG\vb\BatSuppHG.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\BDOLE\VB\BDOLE.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\CalcArr\Vb\CalcArr.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\ContratArchivable\VB\ContratArchivable.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\CPGSIEGE\VB\CPGSIEGE.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\DelProspect\VB\DelProspect.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\DllFusionContrat\VB\DllFusionContrat.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\DllFusionContrat\Lanceur\LanceurFusionContrat.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Eclatement\Vb\Eclatmt.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\ECLATEUR\VB\ECLATEUR.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\EXTRACT\VB\EXTRACT.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Findgest\Vb\FINDGEST.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Flux1MIFv1\VB\FLUX1MIFV1.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FLUX1V1\VB\FLUX1V1.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FLUX2V1\Vb\FLUX2V1.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FLUX3V1\VB\FLUX3V1.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FLUX4V1\VB\FLUX4V1.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FLUX6V1\Vb\FLUX6V1.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FLUX7V1\Vb\FLUX7V1.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Flux7v2\VB\Flux7v2.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Flux7v3\vb\Flux7v3.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FLUX8V1\Vb\FLUX8V1.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Flux17v1\Vb\Flux17v1.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Flux18v1\vb\Flux18v1.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Flux20v1\vb\Flux20v1.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Flux21v1\vb\Flux21v1.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Flux22v1\vb\Flux22v1.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Flux23v1\vb\flux23v1.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FLUX30V1\Vb\Flux30v1.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FLUX31V1\Vb\Flux31v1.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FLUX34V1\Vb\Flux34v1.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FluxBCv1\Vb\FluxBCv1.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FluxMIFInhibeV1\FuRestor\Vb\Restore.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FluxMIFInhibeV1\vb\FluxMIFInhibev1.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FourmiMaitre\FourmiMaitre.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FRMOLEEC\VB\FRMOLEEC.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FuSave\Vb\Save.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FusionAuto\Vb\FusionAuto.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FusionCie\Vb\FusionCie.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FusionFG\Vb\Fusionfg.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\GedBuro\Vb\CollecteurMiseEnGED.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\GestionCrmCla\Vb\PrjCrmCLa.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\GestionMultiQuittance\Vb\GestionMultiQuittance.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\IniPtVie\Vb\IniPtVie.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\InitBC\VB\InitBC.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FuSave\Vb\Save.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FusionAuto\Vb\FusionAuto.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FusionCie\Vb\FusionCie.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\FusionFG\Vb\Fusionfg.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\GedBuro\Vb\CollecteurMiseEnGED.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\GestionCrmCla\Vb\PrjCrmCLa.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\GestionMultiQuittance\Vb\GestionMultiQuittance.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\IniPtVie\Vb\IniPtVie.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\InitBC\VB\InitBC.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\InitInterH24\VB\initialisationInter.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\LanceFusion\Vb\LanceFus.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\MajActSit\Vb\MajActSit.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\MajFileNetMdi\VB\MajFileNetMdi.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\MigBarometre\VB\MigrationBaro.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\MigrationCTR\vb\ANONYCTR.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\MiseAJourPDTI\Vb\majBaseHG.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\REPCLI\Vb\REPCLI.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Reuse_GDS\VB\Reuse_GDS.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\Reuse_PdTI\VB\Reuse_PdTI.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\SCHDFOUR\Vb\SCHDFOUR.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\SuiviFlux\Vb\SUIVIFLUX.VBP",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\support\CodeInter\vb\CodeInter.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\TPSuiviExploit\VB\TPSuiviExploit.vbp",
                    @"FOURMI|C:\Analyses_VB\VB6Apps\FOURMI\Fourmi32\UtiNumAuto\VB\UtiNumClient.vbp",
                    @"FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\Blosoft_Run.vbp",
                    @"FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\ControleCotisation\ControleCotisation.vbp",
                    @"FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\ControleH24\ControleH24.vbp",
                    @"FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\CorrectionMigration\CorrectionArchivageMigration.vbp",
                    @"FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\Gestion Cie\ManCie.vbp",
                    @"FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\GestionInfocentre A confirmer\InfoCentreRobot.vbp",
                    @"FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\ItoolV2\ItoolV2.vbp",
                    @"FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\Liste des flux\ListeFlux.vbp",
                    @"FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\LogMdi\LogMdi.vbp",
                    @"FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\Parseur\Parseur.vbp",
                    @"FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\suiviMdi\SuiviMDI.vbp",
                    @"FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\blosoft\TrtCurratifAnnulAcompte\TrtCurratifAnnulAcompte.vbp",
                    @"FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\Outils en cours\CieIdtCiEa0\Projet1.vbp",
                    @"FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\Outils en cours\cliidtclia0\Projet1.vbp",
                    @"FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\Outils en cours\CLSIDCLEANER\CLSIDFOURMICLEANER.vbp",
                    @"FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\Outils en cours\generateur\generateur.vbp",
                    @"FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\Outils en cours\RattrapageDédouble\decaissement\ratdedoudec.vbp",
                    @"FOURMI-TOOL|C:\Analyses_VB\VB6Apps\FOURMI\Outils en cours\RattrapageDédouble\encaissement\ratdedouenc.vbp",
                    @"GARAGES|C:\Analyses_VB\VB6Apps\GARAGES\Bin\Garages.vbp",
                    @"GBPROD|C:\Analyses_VB\VB6Apps\GBPROD\GBPROD_XMLRelease\TextRel.vbp",
                    @"GBPROD|C:\Analyses_VB\VB6Apps\GBPROD\GBPROD_XMLRelease\GBPROD_XMLReleaseDocument.vbp",
                    @"GBPROD|C:\Analyses_VB\VB6Apps\GBPROD\GBPROD_XMLRelease\XMLReleaseFantome.vbp",
                    @"GBPROD|C:\Analyses_VB\VB6Apps\GBPROD\GBPROD_WFAgent\WFAgent.vbp",
                    @"GBPROD|C:\Analyses_VB\VB6Apps\GBPROD\GBPROD_WFAgent\MaestroWorkflowOcx.vbp",
                    @"GBPROD|C:\Analyses_VB\VB6Apps\GBPROD\GBPROD_WFAgent\GBPROD_WFAgent.vbp",
                    @"IT|C:\Analyses_VB\VB6Apps\IT\Interfaces.vbp",
                    @"JAUGE|C:\Analyses_VB\VB6Apps\FLOTTES\Bin\RealJauge\Jauge.vbp",
                    @"KOFAX|C:\Analyses_VB\VB6Apps\KOFAX\CreationLot\DisplayBatchID.vbp",
                    @"KOFAX|C:\Analyses_VB\VB6Apps\KOFAX\NewRel\ReleaseXMLAID\RelXMLRoutage.vbp",
                    @"KOFAX|C:\Analyses_VB\VB6Apps\KOFAX\Ocx Validation\OcxModule.vbp",
                    @"KOFAX|C:\Analyses_VB\VB6Apps\KOFAX\Ocx Validation\PanelReg.vbp",
                    @"KOFAX|C:\Analyses_VB\VB6Apps\KOFAX\Ocx Validation\Validation.vbp",
                    @"KOFAX|C:\Analyses_VB\VB6Apps\KOFAX\RelQRCODEDecoup\ReleaseXMLAID\RelQRCODEDecoup.vbp",
                    @"KOFAX|C:\Analyses_VB\VB6Apps\KOFAX\RelTIFKtmKta - Version TIF et XML\ReleaseXMLAID\RelTIFKtmKta.vbp",
                    @"KOFAX|C:\Analyses_VB\VB6Apps\KOFAX\WFAgent\WFAgent\WFAgent.vbp",
                    @"KOFAX|C:\Analyses_VB\VB6Apps\KOFAX\WFAgent\MaestroWorkflowOcx.vbp",
                    @"MAESTRO|C:\Analyses_VB\VB6Apps\MAESTRO\Courrier.vbp",
                    @"MESSAUTO|C:\Analyses_VB\VB6Apps\MESSAUTO\copiesMail\CopiesMail.vbp",
                    @"MESSAUTO|C:\Analyses_VB\VB6Apps\MESSAUTO\Decompte2\Decompte2.vbp",
                    @"MESSAUTO|C:\Analyses_VB\VB6Apps\MESSAUTO\LctTrt\LctTrt.vbp",
                    @"MESSAUTO|C:\Analyses_VB\VB6Apps\MESSAUTO\MajFolios\MajFolios.vbp",
                    @"MESSAUTO|C:\Analyses_VB\VB6Apps\MESSAUTO\Traitement2\TraitementOutlook2.vbp",
                    @"MESSAUTO|C:\Analyses_VB\VB6Apps\MESSAUTO\transco\Transco.vbp",
                    @"MESSAUTO|C:\Analyses_VB\VB6Apps\MESSAUTO\Transco2\Transco2.vbp",
                    @"OPTIFLUX|C:\Analyses_VB\VB6Apps\OPTIFLUX\ITO\ITO.vbp",
                    @"OPTIFLUX|C:\Analyses_VB\VB6Apps\OPTIFLUX\OCO\OCOX.vbp",
                    @"SPORT|C:\Analyses_VB\VB6Apps\SPORT\SPORT_dev\SURVEILLANCE.vbp"
                };

                ListeDeProject = null;

                // Autosize des DataGrids
                DtgListeDesProjets.AutoResizeColumns();
                DtgListeDesFormulaires.AutoResizeColumns();
                DtgListeDesReferences.AutoResizeColumns();
                DtgListeDesActiveX.AutoResizeColumns();
                DtgListeDesModules.AutoResizeColumns();
                DtgListeDesClasses.AutoResizeColumns();
                DtgListeDesUserCtrl.AutoResizeColumns();
            }

        #endregion


        #region Analyses

        private void AnalyseGobale()
        {
            try
            {
                string szChemin;
                string szApplication;
                string[] szArray0;

                ListeDeProject = new List<ProjetVBP>();
                ListeDesFichierSource = new List<FicSource>();
                ListeDesRefOCx = new List<RefComp>();
                ListeDesFichiersNonTrouves = new List<string>();

                foreach (var objProj in ListeDesFichiersVBP)
                {
                    szArray0 = objProj.Split("|");
                    szApplication = szArray0[0];
                    szChemin = szArray0[1];
                    CMF.FileName = szChemin;

                    using (var monStreamReader = new System.IO.StreamReader(new System.IO.FileStream(szChemin, System.IO.FileMode.Open), System.Text.Encoding.Latin1))
                    {
                        string ligne;
                        ligne = monStreamReader.ReadLine();

                        SelectedProjetVBP = new ProjetVBP()
                        {
                            NomApplication = szApplication,
                            NomDuFichierVBP = CMF.SafeFileName,
                            Emplacement = System.IO.Path.GetFullPath(szArray0[1].Replace(CMF.SafeFileName, string.Empty))
                        };

                        while ((ligne != null))
                        {

                            // Test Type de Projet
                            if ((ligne.StartsWith("Type")))
                            {
                                if ((ligne.Contains("Type=Exe")))
                                    SelectedProjetVBP.TypeDeProjet = "Exe";
                                else if ((ligne.Contains("Type=OleExe")))
                                    SelectedProjetVBP.TypeDeProjet = "OleExe";
                                else if ((ligne.Contains("OleDll")))
                                    SelectedProjetVBP.TypeDeProjet = "OleDll";
                                else if ((ligne.Contains("Control")))
                                    SelectedProjetVBP.TypeDeProjet = "Control";
                            }

                            // Test NomDuProjet
                            if ((ligne.StartsWith("Name")))
                                SelectedProjetVBP.NomDuProjet = ExtraireValeurEgal(ligne);

                            // Test NomDeCompilation
                            if ((ligne.StartsWith("ExeName32")))
                                SelectedProjetVBP.NomDeCompilation = ExtraireValeurEgal(ligne);

                            // Test Startup
                            if ((ligne.StartsWith("Startup")))
                                SelectedProjetVBP.Startup = ExtraireValeurEgal(ligne);

                            // Test Reference
                            if ((ligne.StartsWith("Reference")))
                            {
                                SelectedRefComp = new RefComp();
                                SelectedRefComp.NomApplication = SelectedProjetVBP.NomApplication;
                                SelectedRefComp.NomDuProjet = SelectedProjetVBP.NomDuFichierVBP;
                                SelectedRefComp.ExtraitDeLigneVBP = ligne;
                                SelectedRefComp.MotClef = (RefComp.LeType)1;

                                SelectedProjetVBP.ListeDeReference.Add(ExtraireReference(ligne));
                                SelectedProjetVBP.NombreReferenceDLL += 1;

                                ListeDesRefOCx.Add(SelectedRefComp);
                            }

                            // Test Object
                            if ((ligne.StartsWith("Object")))
                            {
                                SelectedRefComp = new RefComp();
                                SelectedRefComp.NomApplication = SelectedProjetVBP.NomApplication;
                                SelectedRefComp.NomDuProjet = SelectedProjetVBP.NomDuFichierVBP;
                                SelectedRefComp.ExtraitDeLigneVBP = ligne;
                                SelectedRefComp.MotClef = 0;

                                SelectedProjetVBP.ListeDActiveX.Add(ExtraireObject(ligne));
                                SelectedProjetVBP.NombreDActiveX += 1;

                                ListeDesRefOCx.Add(SelectedRefComp);
                            }

                            // Test UserControl
                            if ((ligne.StartsWith("UserControl")))
                            {
                                SelectedProjetVBP.ListeUserControles.Add(ExtraireUserCtrl(ligne));
                                SelectedProjetVBP.NombreDeUserControles += 1;
                            }

                            // Test Formulaire
                            if ((ligne.StartsWith("Form")))
                            {
                                var obj = ExtraireFichier(ligne, (byte)TypeSource.TypeFichier.Frm);
                                ListeDesFichierSource.Add(obj);
                                SelectedProjetVBP.ListeFormulaire.Add(obj);
                                SelectedProjetVBP.NombreDeFormulaire += 1;
                                obj = null/* TODO Change to default(_) if this is not a reference type */;
                            }

                            // Test Module
                            if ((ligne.StartsWith("Module")))
                            {
                                var obj = ExtraireFichier(ligne, (byte)TypeSource.TypeFichier.ModuleBas);
                                ListeDesFichierSource.Add(obj);
                                SelectedProjetVBP.ListeDeModule.Add(obj);
                                SelectedProjetVBP.NombreDeModuleBas += 1;
                                obj = null/* TODO Change to default(_) if this is not a reference type */;
                            }

                            // Test Classe
                            if ((ligne.StartsWith("Class")))
                            {
                                var obj = ExtraireFichier(ligne, (byte)TypeSource.TypeFichier.Classe);
                                ListeDesFichierSource.Add(obj);
                                SelectedProjetVBP.ListeClasse.Add(obj);
                                SelectedProjetVBP.NombreDeClasse += 1;
                                obj = null/* TODO Change to default(_) if this is not a reference type */;
                            }

                            ligne = monStreamReader.ReadLine();
                        }

                        ListeDeProject.Add(SelectedProjetVBP);
                    }
                }

                DtgListeDesProjets.DataSource = ListeDeProject;
                SelectedProjetVBP = ListeDeProject.FirstOrDefault();

                ActualiserEcran();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void AnalyserFichier(ref FicSource objFic)
        {
            try
            {
                string ligne, valtest;
                var nbLignes = 0;

                using (StreamReader monStreamReader = new StreamReader(objFic.Emplacement))
                {
                    ligne = monStreamReader.ReadLine();

                    while ((ligne != null))
                    {
                        if ((ligne.Trim().StartsWith("'") == false))
                        {
                            nbLignes += 1;
                            var objRegeX = new RegexFound();

                            if ((ligne.Contains("Declare Function")))
                            {
                                objRegeX.Position = nbLignes;
                                objRegeX.MotClef = RegexFound.LeMotClef.Api32;

                                if ((ligne.Length >= 255))
                                    objRegeX.ExtraitDeLigne = Strings.Trim(ligne.Substring(0, 255));
                                else
                                    objRegeX.ExtraitDeLigne = Strings.Trim(ligne);

                                objFic.ListofRegeX.Add(objRegeX);
                                objFic.NombreDApi32 += 1;

                                SelectedProjetVBP.NombreDApi32 += 1;
                            }

                            if ((ligne.Contains("CreateObject(")))
                            {
                                if ((ligne.Contains("Excel.Application")))
                                {
                                    objRegeX.Position = nbLignes;
                                    objRegeX.MotClef = RegexFound.LeMotClef.ExcelApp;

                                    if ((ligne.Length >= 255))
                                        objRegeX.ExtraitDeLigne = Strings.Trim(ligne.Substring(0, 255));
                                    else
                                        objRegeX.ExtraitDeLigne = Strings.Trim(ligne);

                                    objFic.ListofRegeX.Add(objRegeX);
                                    objFic.NombreDeExcelApp += 1;

                                    SelectedProjetVBP.NombreDeExcelApp += 1;
                                }
                                else if ((ligne.Contains("Word.Application")))
                                {
                                    objRegeX.Position = nbLignes;
                                    objRegeX.MotClef = RegexFound.LeMotClef.WordApp;

                                    if ((ligne.Length >= 255))
                                        objRegeX.ExtraitDeLigne = Strings.Trim(ligne.Substring(0, 255));
                                    else
                                        objRegeX.ExtraitDeLigne = Strings.Trim(ligne);

                                    objFic.ListofRegeX.Add(objRegeX);
                                    objFic.NombreDeWordApp += 1;

                                    SelectedProjetVBP.NombreDeWordApp += 1;
                                }
                                else
                                {
                                    objRegeX.Position = nbLignes;
                                    objRegeX.MotClef = RegexFound.LeMotClef.CreateObject;

                                    if ((ligne.Length >= 255))
                                        objRegeX.ExtraitDeLigne = Strings.Trim(ligne.Substring(0, 255));
                                    else
                                        objRegeX.ExtraitDeLigne = Strings.Trim(ligne);

                                    objFic.ListofRegeX.Add(objRegeX);
                                    objFic.NombreDeCreateObject += 1;

                                    SelectedProjetVBP.NombreDeCreateObject += 1;
                                }
                            }

                            if ((ligne.Contains("UserControl")))
                            {
                                objRegeX.Position = nbLignes;
                                objRegeX.MotClef = RegexFound.LeMotClef.UserCtrl;

                                if ((ligne.Length >= 255))
                                    objRegeX.ExtraitDeLigne = Strings.Trim(ligne.Substring(0, 255));
                                else
                                    objRegeX.ExtraitDeLigne = Strings.Trim(ligne);

                                objFic.ListofRegeX.Add(objRegeX);
                                objFic.NombreDeUserControles += 1;

                                SelectedProjetVBP.NombreDeUserControles += 1;
                            }
                        }
                        else
                            // tu peux créer ici un fichier des lignes ignorées 
                            valtest = ligne;

                        ligne = monStreamReader.ReadLine();
                    }
                }

                objFic.NombreDeLignesActives = nbLignes;
                SelectedProjetVBP.NombreDeLignesActives = SelectedProjetVBP.NombreDeLignesActives + nbLignes;

                if ((objFic.NombreDeCreateObject > 1))
                {
                    if ((objFic.NombreDeExcelApp == 0) & (objFic.NombreDeWordApp == 0))
                    {
                        objFic.NombreDeOfficeouAutre = objFic.NombreDeCreateObject;
                        SelectedProjetVBP.NombreDeOfficeouAutre = SelectedProjetVBP.NombreDeOfficeouAutre + objFic.NombreDeCreateObject;
                    }
                }
            }
            catch (Exception ex)
            {
                ListeDesFichiersNonTrouves.Add(objFic.Emplacement + objFic.NomExtFicSource);
            }
        }





        #endregion


        #region Extractions

        private string ExtraireValeurEgal(string objString)
        {
            var szArray = objString.Split("=");
            return szArray[1].Replace("\"", string.Empty);
        }

        private ReferenceDLL ExtraireReference(string objString)
        {
            var szArray1 = objString.Split("=");
            string[] szArray2;
            string[] szArray3;

            var objRef = new ReferenceDLL();

            if (szArray1[1].Where(x => Equals( @"\")).Count() >= 1)
            {
                if (szArray1[1].Where(x => Equals("#")).Count() == 0)
                {
                    // Reference =*\ A..\Server\Server.vbp
                    szArray2 = szArray1[1].Split(@"\");
                    objRef.NomDeLaReferenceDLL = szArray2[szArray2.Length - 1].Trim();
                    objRef.NomDuFichiereDLL = szArray1[1].TrimStart();
                }
                else
                {
                    // Reference =*\ G{91147A58-DFE4-47C0-8E76-987FC1A6001B}#3.0#0#C:\Program Files\Fichiers communs\MSSoap\Binaries\MSSOAP30.dll#Microsoft Soap Type Library v3.0
                    szArray2 = szArray1[1].Split("#");
                    objRef.NomDeLaReferenceDLL = szArray2[szArray2.Length - 1].Trim();
                    objRef.NomDuFichiereDLL = szArray2[szArray2.Length - 2].Trim();
                    szArray3 = szArray2[szArray2.Length - 2].Trim().Split(@"\");
                    SelectedRefComp.NomDuFichierRefOCxTlb = szArray3[szArray3.Length - 1].Trim();
                }
            }

            SelectedRefComp.libelleRefOCxTlb = objRef.NomDeLaReferenceDLL;

            szArray1 = null;
            szArray2 = null;
            szArray3 = null;

            return objRef;
        }

        private ObjectActiveX ExtraireObject(string objString)
        {
            var objActX = new ObjectActiveX();
            var szArray1 = objString.Split("=");
            string[] szArray2;

            if (szArray1[1].Where(x => Equals(@"\")).Count() > 1 && !szArray1[1].Contains(";"))
            {

                // Object =*\ A..\Communs\Controles\prjControles.vbp
                szArray2 = szArray1[1].Split(@"\");
                objActX.NomObjectActiveX = szArray2[szArray2.Length - 1].Trim();
            }
            else if (szArray1[1].Where(x => Equals(@"\")).Count() == 0 && szArray1[1].Contains(";"))
            {
                // Object = {831.0FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.1#0; mscomctl.ocx
                szArray2 = objString.Split(";");
                objActX.NomObjectActiveX = szArray2[1].Trim();
            }


            SelectedRefComp.NomDuFichierRefOCxTlb = objActX.NomObjectActiveX;

            szArray1 = null;
            szArray2 = null;

            return objActX;
        }

        private UserControle ExtraireUserCtrl(string objString)
        {
            var szArray1 = objString.Split("=");
            var objUserCtrl = new UserControle();
            objUserCtrl.NomUserControle = szArray1[1];
            szArray1 = null;
            return objUserCtrl;
        }

        private FicSource ExtraireFichier(string objLigne, byte oTypeFichier)
        {
            string[] szArray1, szArray2, szArray3;

            szArray1 = objLigne.Split("=");

            var objFicSource = new FicSource()
            {
                NomApplication = SelectedProjetVBP.NomApplication,
                NomDuFichierVBP = SelectedProjetVBP.NomDuFichierVBP,
                TypeFicSource = (TypeSource.TypeFichier)oTypeFichier
            };

            try
            {
                switch (oTypeFichier)
                {
                    case 0:
                        {
                            // Form=Feuilles\frmAccueil.frm
                            szArray2 = szArray1[1].Split(@"\");

                            objFicSource.NomIntFicSource = szArray2[1];
                            objFicSource.NomExtFicSource = szArray2[1];

                            objFicSource.Emplacement = SelectedProjetVBP.Emplacement.ToString() + szArray1[1];
                            break;
                        }

                    case 1:
                    case 2:
                        {
                            // Module=Demarrage;Modules\Demarrage.bas
                            // Class=clIntermediaire;Classes\clIntermediaire.cls
                            szArray2 = szArray1[1].Split(";");
                            szArray3 = szArray2[1].Split(@"\");

                            objFicSource.NomIntFicSource = szArray2[0].Trim();
                            objFicSource.NomExtFicSource = szArray3[1].Trim();

                            objFicSource.Emplacement = SelectedProjetVBP.Emplacement.ToString() + Strings.Trim(szArray2[1]);
                            break;
                        }
                }

                AnalyserFichier(ref objFicSource);

                szArray1 = null;
                szArray2 = null;
                szArray3 = null;
            }
            catch (Exception ex)
            {
                throw;
            }

            return objFicSource;
        }


        #endregion


        #region ExportCSV

        private void CreerFichierCSV(List<ProjetVBP> oListeDeProject)
        {
            try
            {
                // Détail Par Applications
                FileSystem.FileOpen(1, @"C:\_DllFolder\" + "APPLICATIONS_VB.CSV", OpenMode.Output);
                var enteteDuFichier = "APPLICATION;" + "PROJET;" + "NB_REFERENCES;" + "NB_FORMULAIRES;" + "NB_OCX_CONTROLES;" + "NB_USERCONTROLES;" + "NB_MODULES;" + "NB_CLASSES;";

                FileSystem.PrintLine(1, enteteDuFichier);
                foreach (ProjetVBP oneProjet in oListeDeProject)
                {
                    string UneLigneProjet;
                    try
                    {
                        UneLigneProjet = oneProjet.NomApplication + ";";
                        UneLigneProjet = UneLigneProjet + oneProjet.NomDuProjet + ";" + oneProjet.NombreReferenceDLL.ToString() + ";" + oneProjet.NombreDeFormulaire.ToString() + ";";
                        UneLigneProjet = UneLigneProjet + oneProjet.NombreDActiveX.ToString() + ";" + oneProjet.NombreDeUserControles.ToString() + ";" + oneProjet.NombreDeModuleBas.ToString() + ";";
                        UneLigneProjet = UneLigneProjet +oneProjet.NombreDeClasse.ToString();

                        FileSystem.PrintLine(1, UneLigneProjet);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                FileSystem.FileClose(1);

                // Application | Projet | Type (OCX ou REF) | Fichier (OCX/DLL/TLB) | Libellé (uniquement pour les refs) | Ligne du VBP
                FileSystem.FileOpen(1, @"C:\_DllFolder\" + "OCX_REF_DLL_VB.CSV", OpenMode.Output);
                enteteDuFichier = "APPLICATION;" + "PROJET;" + "Type (OCX ou REF);" + "Fichier (OCX/DLL/TLB);" + "Libellé (uniquement pour les refs);" + "Ligne du VBP;";
                FileSystem.PrintLine(1, enteteDuFichier);
                foreach (RefComp oneRefComp in ListeDesRefOCx)
                {
                    string UneLigneProjet;
                    try
                    {
                        UneLigneProjet = oneRefComp.NomApplication + ";";
                        UneLigneProjet = UneLigneProjet + oneRefComp.NomDuProjet + ";";

                        switch (oneRefComp.MotClef)
                        {
                            case (RefComp.LeType)0:
                                {
                                    UneLigneProjet = UneLigneProjet + "OCX";
                                    break;
                                }

                            case (RefComp.LeType)1:
                                {
                                    UneLigneProjet = UneLigneProjet + "REF";
                                    break;
                                }
                        }

                        UneLigneProjet = UneLigneProjet + ";" + oneRefComp.NomDuFichierRefOCxTlb + ";" + oneRefComp.libelleRefOCxTlb + ";" + oneRefComp.ExtraitDeLigneVBP;


                        FileSystem.PrintLine(1, UneLigneProjet);
                    }
                    catch (Exception ex)
                    {
                    }
                }

                FileSystem.FileClose(1);

                // Detail Par Projet
                var rq01 = from obj in ListeDeProject
                           orderby obj.NomApplication, obj.NomDuProjet
                           select obj;
                FileSystem.FileOpen(1, @"C:\_DllFolder\" + "LISTE_DES_PROJETS.CSV", OpenMode.Output);

                enteteDuFichier = "APPLICATION;" + "PROJET;" + "NB_REFERENCES;" + "NB_FORMULAIRES;" + "NB_OCX_CONTROLES;" + "NB_USERCONTROLES;" + "NB_MODULES;" + "NB_CLASSES;" + "NB_CREATE_OBJECT;" + "NB_API32;" + "NB_WORD;" + "NB_EXCEL;" + "NB OFFICE;" + "NB_LIGNES_ACTIVES";

                FileSystem.PrintLine(1, enteteDuFichier);
                foreach (ProjetVBP oneProjet in rq01)
                {
                    string UneLigneProjet;
                    try
                    {
                        UneLigneProjet = oneProjet.NomApplication + ";" + oneProjet.NomDuProjet + ";" + oneProjet.NombreReferenceDLL + ";" + oneProjet.NombreDeFormulaire.ToString() + ";";
                        UneLigneProjet = UneLigneProjet + oneProjet.NombreDActiveX + ";" + oneProjet.NombreDeUserControles + ";" + oneProjet.NombreDeModuleBas.ToString() + ";";
                        UneLigneProjet = UneLigneProjet + oneProjet.NombreDeClasse + ";" + oneProjet.NombreDeCreateObject + ";" + oneProjet.NombreDApi32.ToString() + ";";
                        UneLigneProjet = UneLigneProjet + oneProjet.NombreDeWordApp + ";" + oneProjet.NombreDeExcelApp + ";" + oneProjet.NombreDeOfficeouAutre.ToString() + ";";
                        UneLigneProjet = UneLigneProjet + oneProjet.NombreDeLignesActives.ToString();

                        FileSystem.PrintLine(1, UneLigneProjet);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                FileSystem.FileClose(1);


                // Details par Fichier Source FRM,MOD,CLS
                var rq02 = from obj in ListeDesFichierSource
                           orderby obj.NomApplication, obj.NomDuFichierVBP
                           select obj;

                FileSystem.FileOpen(1, @"C:\_DllFolder\" + "FICHIERS_SOURCES.CSV", OpenMode.Output);
                FileSystem.FileOpen(2, @"C:\_DllFolder\" + "CR_API32_WORD_EXCEL.CSV", OpenMode.Output);

                var enteteDuFichier01 = "APPLICATION;" + "PROJET;" + "FICHIER;" + "NB_CREATE_OBJECT;" + "NB_API32;" + "NB_WORD;" + "NB_EXCEL;" + "NB OFFICE;" + "NB_LIGNES_ACTIVES";
                var enteteDuFichier02 = "APPLICATION;" + "PROJET;" + "FICHIER;" + "NB_TYPE;" + "POSITION;" + "EXTRAIT";

                FileSystem.PrintLine(1, enteteDuFichier01);
                FileSystem.PrintLine(2, enteteDuFichier02);
                foreach (FicSource oneFichier in rq02)
                {
                    string UneLigne;
                    string UnDetail;

                    try
                    {
                        UneLigne = oneFichier.NomApplication + ";" + oneFichier.NomDuFichierVBP + ";" + oneFichier.NomExtFicSource + ";";
                        UneLigne = UneLigne + oneFichier.NombreDeCreateObject.ToString() + ";" + oneFichier.NombreDApi32.ToString() + ";";
                        UneLigne = UneLigne + oneFichier.NombreDeWordApp.ToString() + ";" + oneFichier.NombreDeExcelApp.ToString() + ";";
                        UneLigne = UneLigne + oneFichier.NombreDeOfficeouAutre.ToString() + ";" + oneFichier.NombreDeLignesActives.ToString();

                        FileSystem.PrintLine(1, UneLigne);

                        if (oneFichier.ListofRegeX.Count > 0)
                        {
                            foreach (RegexFound oneRegex in oneFichier.ListofRegeX)
                            {
                                UnDetail = oneFichier.NomApplication + ";" + oneFichier.NomDuFichierVBP + ";" + oneFichier.NomExtFicSource + ";";

                                switch (oneRegex.MotClef)
                                {
                                    case (RegexFound.LeMotClef)0:
                                        {
                                            UnDetail = UnDetail + "Api32";
                                            break;
                                        }

                                    case (RegexFound.LeMotClef)1:
                                        {
                                            UnDetail = UnDetail + "CreateObject";
                                            break;
                                        }

                                    case (RegexFound.LeMotClef)2:
                                        {
                                            UnDetail = UnDetail + "ExcelApp";
                                            break;
                                        }

                                    case (RegexFound.LeMotClef)3:
                                        {
                                            UnDetail = UnDetail + "WordApp";
                                            break;
                                        }

                                    default:
                                        {
                                            UnDetail = UnDetail + "UserCtrl";
                                            break;
                                        }
                                }

                                UnDetail = UnDetail + ";" + oneRegex.Position.ToString() + ";" + oneRegex.ExtraitDeLigne;
                                FileSystem.PrintLine(2, UnDetail);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }

                FileSystem.FileClose(1);
                FileSystem.FileClose(2);

                if (ListeDesFichiersNonTrouves.Any())
                {
                    FileSystem.FileOpen(1, @"C:\_DllFolder\" + "LISTE_DES_MANQUANTS.CSV", OpenMode.Output);
                    foreach (var oneFile in ListeDesFichiersNonTrouves)
                        FileSystem.PrintLine(1, oneFile);
                    FileSystem.FileClose(1);
                }
            }
            catch (Exception ex)
            {
                FileSystem.FileClose(1);
            }
        }

        #endregion


        #region IhmActions

        private void BtnAnalyse_Click(object sender, EventArgs e)
            {
                AnalyseGobale();
            }

            private void BtnExporter_Click(object sender, EventArgs e)
            {
                CreerFichierCSV(ListeDeProject);
            }

            private void BtnQuitter_Click(object sender, EventArgs e)
            {
                Application.Exit();
            }

            private void ActualiserEcran()
            {
                DtgListeDesFormulaires.DataSource = SelectedProjetVBP.ListeFormulaire;
                DtgListeDesReferences.DataSource = SelectedProjetVBP.ListeDeReference;
                DtgListeDesActiveX.DataSource = SelectedProjetVBP.ListeDActiveX;
                DtgListeDesUserCtrl.DataSource = SelectedProjetVBP.ListeUserControles;
                DtgListeDesModules.DataSource = SelectedProjetVBP.ListeDeModule;
                DtgListeDesClasses.DataSource = SelectedProjetVBP.ListeClasse;

                if (SelectedProjetVBP.ListeFormulaire.Count > 0)
                    DtgListeDesRegeX.DataSource = SelectedProjetVBP.ListeFormulaire.FirstOrDefault().ListofRegeX;
                else
                    DtgListeDesRegeX.DataSource = null;
            }

        #region DatagridActions

            private void DtgListeDesProjets_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {
                if (this.DtgListeDesProjets.Rows.Count > 0)
                {
                    SelectedProjetVBP = DtgListeDesProjets.CurrentRow.DataBoundItem as ProjetVBP;
                    ActualiserEcran();
                }
            }

            private void DtgListeDesFormulaires_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {
                if (DtgListeDesFormulaires.Rows.Count > 0)
                {
                    SelectedFormulaire = this.DtgListeDesFormulaires.CurrentRow.DataBoundItem as FicSource;
                    DtgListeDesRegeX.DataSource = SelectedFormulaire.ListofRegeX;
                }
            }

            private void DtgListeDesModules_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {
                if (this.DtgListeDesModules.Rows.Count > 0)
                {
                    SelectedModule = this.DtgListeDesModules.CurrentRow.DataBoundItem as FicSource;
                    this.DtgListeDesRegeX.DataSource = SelectedModule.ListofRegeX;
                }
            }

            private void DtgListeDesClasses_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {
                if (this.DtgListeDesClasses.Rows.Count > 0)
                {
                    SelectedClsClasse = this.DtgListeDesClasses.CurrentRow.DataBoundItem as FicSource;
                    this.DtgListeDesRegeX.DataSource = SelectedClsClasse.ListofRegeX;
                }
            }

            #endregion

        #endregion

    }
}
