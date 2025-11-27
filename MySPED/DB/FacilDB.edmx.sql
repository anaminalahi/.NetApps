
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/03/2017 06:23:42
-- Generated from EDMX file: C:\01GitSources\MySPED\DB\FacilDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ustaspe133652fr29914_facilDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AssembleeDocumentDisponible]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DocumentDisponibles] DROP CONSTRAINT [FK_AssembleeDocumentDisponible];
GO
IF OBJECT_ID(N'[dbo].[FK_DetailsDeRepartionFeuille]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Feuilles] DROP CONSTRAINT [FK_DetailsDeRepartionFeuille];
GO
IF OBJECT_ID(N'[dbo].[FK_FeuilleInformation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Informations] DROP CONSTRAINT [FK_FeuilleInformation];
GO
IF OBJECT_ID(N'[dbo].[FK_InstrumentsJoues_Instrument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InstrumentsJoues] DROP CONSTRAINT [FK_InstrumentsJoues_Instrument];
GO
IF OBJECT_ID(N'[dbo].[FK_InstrumentsJoues_Spedinaute]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InstrumentsJoues] DROP CONSTRAINT [FK_InstrumentsJoues_Spedinaute];
GO
IF OBJECT_ID(N'[dbo].[FK_MandataireAvoirMandat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Mandats] DROP CONSTRAINT [FK_MandataireAvoirMandat];
GO
IF OBJECT_ID(N'[dbo].[FK_PaysRibIban]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RibIbans] DROP CONSTRAINT [FK_PaysRibIban];
GO
IF OBJECT_ID(N'[dbo].[FK_RepartitionConcerneDetailsDeRepartion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DetailsDeRepartions] DROP CONSTRAINT [FK_RepartitionConcerneDetailsDeRepartion];
GO
IF OBJECT_ID(N'[dbo].[FK_SpedinauteDetailsDeRepartion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DetailsDeRepartions] DROP CONSTRAINT [FK_SpedinauteDetailsDeRepartion];
GO
IF OBJECT_ID(N'[dbo].[FK_SpedinauteMandat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Mandats] DROP CONSTRAINT [FK_SpedinauteMandat];
GO
IF OBJECT_ID(N'[dbo].[FK_SpedinauteMotDePasse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MotDePasses] DROP CONSTRAINT [FK_SpedinauteMotDePasse];
GO
IF OBJECT_ID(N'[dbo].[FK_SpedinauteRibIban]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RibIbans] DROP CONSTRAINT [FK_SpedinauteRibIban];
GO
IF OBJECT_ID(N'[dbo].[FK_SpedinauteSpedAdresse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpedAdresses] DROP CONSTRAINT [FK_SpedinauteSpedAdresse];
GO
IF OBJECT_ID(N'[dbo].[FK_TypeAdressesSpedAdresse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SpedAdresses] DROP CONSTRAINT [FK_TypeAdressesSpedAdresse];
GO
IF OBJECT_ID(N'[dbo].[FK_TypeDeMandatEtMandat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Mandats] DROP CONSTRAINT [FK_TypeDeMandatEtMandat];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Assemblees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Assemblees];
GO
IF OBJECT_ID(N'[dbo].[DeclarationFiscales]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeclarationFiscales];
GO
IF OBJECT_ID(N'[dbo].[DetailsDeRepartions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DetailsDeRepartions];
GO
IF OBJECT_ID(N'[dbo].[DocumentDisponibles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DocumentDisponibles];
GO
IF OBJECT_ID(N'[dbo].[Feuilles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Feuilles];
GO
IF OBJECT_ID(N'[dbo].[Informations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Informations];
GO
IF OBJECT_ID(N'[dbo].[Instruments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Instruments];
GO
IF OBJECT_ID(N'[dbo].[InstrumentsJoues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InstrumentsJoues];
GO
IF OBJECT_ID(N'[dbo].[Mandataires]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Mandataires];
GO
IF OBJECT_ID(N'[dbo].[Mandats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Mandats];
GO
IF OBJECT_ID(N'[dbo].[MotDePasses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MotDePasses];
GO
IF OBJECT_ID(N'[dbo].[Pays]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pays];
GO
IF OBJECT_ID(N'[dbo].[RepartitionGenerales]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RepartitionGenerales];
GO
IF OBJECT_ID(N'[dbo].[RibIbans]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RibIbans];
GO
IF OBJECT_ID(N'[dbo].[SpedAdresses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SpedAdresses];
GO
IF OBJECT_ID(N'[dbo].[Spedinautes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Spedinautes];
GO
IF OBJECT_ID(N'[dbo].[TacheEffectuees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TacheEffectuees];
GO
IF OBJECT_ID(N'[dbo].[TypeAdresses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeAdresses];
GO
IF OBJECT_ID(N'[dbo].[TypeDeMandats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeDeMandats];
GO
IF OBJECT_ID(N'[dbo].[TypeDeVoies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeDeVoies];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Instruments'
CREATE TABLE [dbo].[Instruments] (
    [id_instr] int  NOT NULL,
    [raninstr] int  NULL,
    [lb_instr] nvarchar(255)  NOT NULL,
    [no_instr] nvarchar(255)  NULL,
    [scapr] nvarchar(255)  NULL,
    [lb_scapr] nvarchar(255)  NULL,
    [typscapr] nvarchar(255)  NULL
);
GO

-- Creating table 'MotDePasses'
CREATE TABLE [dbo].[MotDePasses] (
    [IdMotDePasse] int IDENTITY(1,1) NOT NULL,
    [DateDeCreation] datetime  NOT NULL,
    [DateDeRenvoi] datetime  NOT NULL,
    [Actuel] bit  NOT NULL,
    [PasseMot] nvarchar(15)  NOT NULL,
    [IntegreDansBachON] bit  NOT NULL,
    [IdSpedinaute] int  NOT NULL
);
GO

-- Creating table 'Pays'
CREATE TABLE [dbo].[Pays] (
    [id_pays] varchar(3)  NOT NULL,
    [id_Web] int  NULL,
    [lb_pays] nvarchar(255)  NULL,
    [co_ipd] varchar(3)  NULL,
    [on_ue] bit  NOT NULL,
    [dt_ue] datetime  NULL,
    [co_insee] nvarchar(255)  NULL,
    [cfpcps] float  NULL,
    [cfpcpa] float  NULL,
    [cfpre] float  NULL,
    [dt_rome] datetime  NULL,
    [on_rome] bit  NOT NULL,
    [est_sepa] bit  NOT NULL,
    [long_iban] int  NULL,
    [pays_iban] nvarchar(255)  NULL
);
GO

-- Creating table 'RepartitionGenerales'
CREATE TABLE [dbo].[RepartitionGenerales] (
    [IdRepartition] int IDENTITY(1,1) NOT NULL,
    [Annee] nvarchar(4)  NOT NULL,
    [MoisRepartition] nvarchar(13)  NULL,
    [TableDecodification] nvarchar(max)  NOT NULL,
    [ReglesDeRepartition] nvarchar(max)  NOT NULL,
    [LettreDAccompagnement] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'RibIbans'
CREATE TABLE [dbo].[RibIbans] (
    [IdRibIban] int IDENTITY(1,1) NOT NULL,
    [Titulaire] nvarchar(100)  NULL,
    [BicSwift] nvarchar(11)  NULL,
    [NumeroIBAN] nvarchar(34)  NULL,
    [Domiciliation] nvarchar(100)  NULL,
    [CodeBanque] nvarchar(5)  NULL,
    [Guichet] nvarchar(5)  NULL,
    [NumeroCompte] nvarchar(50)  NULL,
    [CleRIB] nvarchar(3)  NULL,
    [DateDeSaisie] datetime  NULL,
    [Actuel] bit  NOT NULL,
    [IntegreDansBachON] bit  NOT NULL,
    [IdSpedinaute] int  NOT NULL,
    [Id_pays] varchar(3)  NULL,
    [NomDuFichierJoint] nvarchar(50)  NULL,
    [ControledByJJ] bit  NULL
);
GO

-- Creating table 'SpedAdresses'
CREATE TABLE [dbo].[SpedAdresses] (
    [IdAdresse] int IDENTITY(1,1) NOT NULL,
    [TypeAdresse] nvarchar(3)  NOT NULL,
    [Numero] varchar(5)  NULL,
    [TypeDeVoie] nvarchar(5)  NULL,
    [LibelleVoie] nvarchar(100)  NULL,
    [Adresse2] nvarchar(100)  NULL,
    [CodePostal] nvarchar(10)  NULL,
    [Ville] nvarchar(50)  NULL,
    [Bureau] nvarchar(32)  NULL,
    [DateSaisie] datetime  NULL,
    [Actuelle] bit  NULL,
    [IntegreDansBachON] bit  NULL,
    [IdSpedinaute] int  NOT NULL,
    [Id_pays] varchar(3)  NULL
);
GO

-- Creating table 'Spedinautes'
CREATE TABLE [dbo].[Spedinautes] (
    [IdSpedinaute] int IDENTITY(1,1) NOT NULL,
    [TypeDeProfil] nvarchar(3)  NOT NULL,
    [Civilite] nvarchar(15)  NULL,
    [Prenom] nvarchar(100)  NULL,
    [AutrePrenom] nvarchar(100)  NULL,
    [Nom] nvarchar(50)  NULL,
    [NomDeJeuneFille] nvarchar(50)  NULL,
    [DateDeNaissance] datetime  NULL,
    [LieuDeNaissance] nvarchar(50)  NULL,
    [Nationalite] nvarchar(3)  NULL,
    [NumeroDeMobile] nvarchar(17)  NULL,
    [NumeroDeFixe] nvarchar(17)  NULL,
    [AdresseEmail] nvarchar(100)  NULL,
    [PasseMot] nvarchar(10)  NULL,
    [NumeroIdAyant] char(6)  NOT NULL,
    [IPN] nvarchar(10)  NULL,
    [NumeroAD] nvarchar(10)  NULL,
    [NumeroNA] nvarchar(10)  NULL,
    [ArtistePrincipalSpedidam] bit  NULL,
    [AccepterEmailInfos] nvarchar(1)  NULL,
    [Enligne] nvarchar(1)  NULL,
    [DernierDateDeConnexion] datetime  NULL,
    [MotDePasseOublie] bit  NULL,
    [CompteUsurpe] bit  NULL,
    [DateDUsurpation] datetime  NULL,
    [Pseudonyme1] nvarchar(255)  NULL,
    [Pseudonyme2] nvarchar(255)  NULL,
    [Pseudonyme3] nvarchar(255)  NULL,
    [Instrument1] int  NULL,
    [Instrument2] int  NULL,
    [Instrument3] int  NULL,
    [Photo] nvarchar(255)  NULL,
    [IntegreDansBachON] bit  NULL
);
GO

-- Creating table 'TacheEffectuees'
CREATE TABLE [dbo].[TacheEffectuees] (
    [IdTacheEffectuee] int IDENTITY(1,1) NOT NULL,
    [TypeDeTache] nvarchar(1)  NOT NULL,
    [DateDuJour] nvarchar(max)  NOT NULL,
    [HeureCourante] nvarchar(max)  NULL,
    [IdAdresseConcernee] nvarchar(1)  NULL,
    [IdModePasseConcerne] nvarchar(1)  NULL,
    [IdRibIbanConcerne] nvarchar(1)  NOT NULL,
    [FicheConcernee] nvarchar(1)  NOT NULL,
    [NomDuDevice] nvarchar(max)  NULL,
    [AdresseMac] nvarchar(max)  NULL,
    [AdresseIP] nvarchar(max)  NULL,
    [Commentaire] nvarchar(max)  NULL,
    [HistoriserON] bit  NOT NULL,
    [IdSpedinaute] int  NOT NULL,
    [IntegreDansBachON] bit  NOT NULL
);
GO

-- Creating table 'TypeAdresses'
CREATE TABLE [dbo].[TypeAdresses] (
    [TypeAdresse] nvarchar(3)  NOT NULL,
    [LibelleAdresse] nvarchar(10)  NULL
);
GO

-- Creating table 'TypeDeVoies'
CREATE TABLE [dbo].[TypeDeVoies] (
    [TypeDeVoie] nvarchar(5)  NOT NULL,
    [LbelleVoie] nvarchar(50)  NULL
);
GO

-- Creating table 'DeclarationFiscales'
CREATE TABLE [dbo].[DeclarationFiscales] (
    [IdDeclarationFiscale] int IDENTITY(1,1) NOT NULL,
    [Annee] nvarchar(4)  NULL,
    [NomDuFichier] nvarchar(50)  NULL,
    [NumeroIdAyant] char(6)  NULL
);
GO

-- Creating table 'DetailsDeRepartions'
CREATE TABLE [dbo].[DetailsDeRepartions] (
    [IdDetailsDeRepartion] int IDENTITY(1,1) NOT NULL,
    [FichierDetail] nvarchar(max)  NOT NULL,
    [IdRepartition] int  NOT NULL,
    [IdSpedinaute] int  NOT NULL
);
GO

-- Creating table 'Assemblees'
CREATE TABLE [dbo].[Assemblees] (
    [IdAssemblee] int IDENTITY(1,1) NOT NULL,
    [Exercice] nvarchar(max)  NULL,
    [DateDuJour] nvarchar(max)  NULL,
    [Horaire] nvarchar(max)  NULL,
    [Lieu] nvarchar(max)  NULL,
    [Adresse] nvarchar(max)  NULL,
    [OrdreDuJourFichier] nvarchar(max)  NULL,
    [LeJourEnTexte] nvarchar(50)  NULL,
    [Actuelle] bit  NULL
);
GO

-- Creating table 'DocumentDisponibles'
CREATE TABLE [dbo].[DocumentDisponibles] (
    [IdDocumentDisponible] int IDENTITY(1,1) NOT NULL,
    [IdAssemblee] int  NOT NULL,
    [NumeroDOrdre] nvarchar(max)  NOT NULL,
    [TitreDuSujet] nvarchar(max)  NOT NULL,
    [EmplacementDuFichier] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Feuilles'
CREATE TABLE [dbo].[Feuilles] (
    [IdFeuille] int IDENTITY(1,1) NOT NULL,
    [NumeroDeFeuille] nvarchar(max)  NOT NULL,
    [IntituleGeneral] nvarchar(max)  NOT NULL,
    [IdDetailsDeRepartion] int  NOT NULL
);
GO

-- Creating table 'Informations'
CREATE TABLE [dbo].[Informations] (
    [IdInformation] int IDENTITY(1,1) NOT NULL,
    [DateEnregistrement] nvarchar(max)  NOT NULL,
    [TitreOeuvre] nvarchar(max)  NOT NULL,
    [ArtistePrincipalOuGroupe] nvarchar(max)  NOT NULL,
    [Compositeur] nvarchar(max)  NOT NULL,
    [Destinations] nvarchar(max)  NOT NULL,
    [IdFeuille] int  NOT NULL,
    [NumeroDeFeuille] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Mandataires'
CREATE TABLE [dbo].[Mandataires] (
    [IdMandataire] int IDENTITY(1,1) NOT NULL,
    [Prenom] nvarchar(max)  NOT NULL,
    [Nom] nvarchar(max)  NOT NULL,
    [AdresseEmail] nvarchar(100)  NULL,
    [PasseMot] nvarchar(10)  NULL,
    [Enligne] nvarchar(1)  NULL,
    [DernierDateDeConnexion] datetime  NULL,
    [MotDePasseOublie] bit  NULL,
    [IntegreDansBachON] bit  NOT NULL
);
GO

-- Creating table 'Mandats'
CREATE TABLE [dbo].[Mandats] (
    [IdMandat] int IDENTITY(1,1) NOT NULL,
    [IdTypeDeMandat] int  NOT NULL,
    [DateDeDebut] nvarchar(max)  NOT NULL,
    [DateDeFin] nvarchar(max)  NULL,
    [IdMandataire] int  NOT NULL,
    [IdSpedinaute] int  NULL
);
GO

-- Creating table 'TypeDeMandats'
CREATE TABLE [dbo].[TypeDeMandats] (
    [IdTypeDeMandat] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'DeclarationFiscaleFinales'
CREATE TABLE [dbo].[DeclarationFiscaleFinales] (
    [IdDeclarationFiscale] int IDENTITY(1,1) NOT NULL,
    [Annee] nvarchar(13)  NULL,
    [NomDuFichier] nvarchar(50)  NULL,
    [MontantNet] real  NOT NULL,
    [DroitsBruts] nvarchar(max)  NOT NULL,
    [PrelevementSocial] nvarchar(max)  NOT NULL,
    [CSGDeductible] nvarchar(max)  NOT NULL,
    [NetImposable] nvarchar(max)  NOT NULL,
    [NumeroIdAyant] char(6)  NULL,
    [IdSpedinaute] int  NOT NULL
);
GO

-- Creating table 'InstrumentsJoues'
CREATE TABLE [dbo].[InstrumentsJoues] (
    [Spedinaute_IdSpedinaute] int  NOT NULL,
    [Instrument_id_instr] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id_instr] in table 'Instruments'
ALTER TABLE [dbo].[Instruments]
ADD CONSTRAINT [PK_Instruments]
    PRIMARY KEY CLUSTERED ([id_instr] ASC);
GO

-- Creating primary key on [IdMotDePasse] in table 'MotDePasses'
ALTER TABLE [dbo].[MotDePasses]
ADD CONSTRAINT [PK_MotDePasses]
    PRIMARY KEY CLUSTERED ([IdMotDePasse] ASC);
GO

-- Creating primary key on [id_pays] in table 'Pays'
ALTER TABLE [dbo].[Pays]
ADD CONSTRAINT [PK_Pays]
    PRIMARY KEY CLUSTERED ([id_pays] ASC);
GO

-- Creating primary key on [IdRepartition] in table 'RepartitionGenerales'
ALTER TABLE [dbo].[RepartitionGenerales]
ADD CONSTRAINT [PK_RepartitionGenerales]
    PRIMARY KEY CLUSTERED ([IdRepartition] ASC);
GO

-- Creating primary key on [IdRibIban] in table 'RibIbans'
ALTER TABLE [dbo].[RibIbans]
ADD CONSTRAINT [PK_RibIbans]
    PRIMARY KEY CLUSTERED ([IdRibIban] ASC);
GO

-- Creating primary key on [IdAdresse] in table 'SpedAdresses'
ALTER TABLE [dbo].[SpedAdresses]
ADD CONSTRAINT [PK_SpedAdresses]
    PRIMARY KEY CLUSTERED ([IdAdresse] ASC);
GO

-- Creating primary key on [IdSpedinaute] in table 'Spedinautes'
ALTER TABLE [dbo].[Spedinautes]
ADD CONSTRAINT [PK_Spedinautes]
    PRIMARY KEY CLUSTERED ([IdSpedinaute] ASC);
GO

-- Creating primary key on [IdTacheEffectuee] in table 'TacheEffectuees'
ALTER TABLE [dbo].[TacheEffectuees]
ADD CONSTRAINT [PK_TacheEffectuees]
    PRIMARY KEY CLUSTERED ([IdTacheEffectuee] ASC);
GO

-- Creating primary key on [TypeAdresse] in table 'TypeAdresses'
ALTER TABLE [dbo].[TypeAdresses]
ADD CONSTRAINT [PK_TypeAdresses]
    PRIMARY KEY CLUSTERED ([TypeAdresse] ASC);
GO

-- Creating primary key on [TypeDeVoie] in table 'TypeDeVoies'
ALTER TABLE [dbo].[TypeDeVoies]
ADD CONSTRAINT [PK_TypeDeVoies]
    PRIMARY KEY CLUSTERED ([TypeDeVoie] ASC);
GO

-- Creating primary key on [IdDeclarationFiscale] in table 'DeclarationFiscales'
ALTER TABLE [dbo].[DeclarationFiscales]
ADD CONSTRAINT [PK_DeclarationFiscales]
    PRIMARY KEY CLUSTERED ([IdDeclarationFiscale] ASC);
GO

-- Creating primary key on [IdDetailsDeRepartion] in table 'DetailsDeRepartions'
ALTER TABLE [dbo].[DetailsDeRepartions]
ADD CONSTRAINT [PK_DetailsDeRepartions]
    PRIMARY KEY CLUSTERED ([IdDetailsDeRepartion] ASC);
GO

-- Creating primary key on [IdAssemblee] in table 'Assemblees'
ALTER TABLE [dbo].[Assemblees]
ADD CONSTRAINT [PK_Assemblees]
    PRIMARY KEY CLUSTERED ([IdAssemblee] ASC);
GO

-- Creating primary key on [IdDocumentDisponible] in table 'DocumentDisponibles'
ALTER TABLE [dbo].[DocumentDisponibles]
ADD CONSTRAINT [PK_DocumentDisponibles]
    PRIMARY KEY CLUSTERED ([IdDocumentDisponible] ASC);
GO

-- Creating primary key on [IdFeuille] in table 'Feuilles'
ALTER TABLE [dbo].[Feuilles]
ADD CONSTRAINT [PK_Feuilles]
    PRIMARY KEY CLUSTERED ([IdFeuille] ASC);
GO

-- Creating primary key on [IdInformation] in table 'Informations'
ALTER TABLE [dbo].[Informations]
ADD CONSTRAINT [PK_Informations]
    PRIMARY KEY CLUSTERED ([IdInformation] ASC);
GO

-- Creating primary key on [IdMandataire] in table 'Mandataires'
ALTER TABLE [dbo].[Mandataires]
ADD CONSTRAINT [PK_Mandataires]
    PRIMARY KEY CLUSTERED ([IdMandataire] ASC);
GO

-- Creating primary key on [IdMandat] in table 'Mandats'
ALTER TABLE [dbo].[Mandats]
ADD CONSTRAINT [PK_Mandats]
    PRIMARY KEY CLUSTERED ([IdMandat] ASC);
GO

-- Creating primary key on [IdTypeDeMandat] in table 'TypeDeMandats'
ALTER TABLE [dbo].[TypeDeMandats]
ADD CONSTRAINT [PK_TypeDeMandats]
    PRIMARY KEY CLUSTERED ([IdTypeDeMandat] ASC);
GO

-- Creating primary key on [IdDeclarationFiscale] in table 'DeclarationFiscaleFinales'
ALTER TABLE [dbo].[DeclarationFiscaleFinales]
ADD CONSTRAINT [PK_DeclarationFiscaleFinales]
    PRIMARY KEY CLUSTERED ([IdDeclarationFiscale] ASC);
GO

-- Creating primary key on [Spedinaute_IdSpedinaute], [Instrument_id_instr] in table 'InstrumentsJoues'
ALTER TABLE [dbo].[InstrumentsJoues]
ADD CONSTRAINT [PK_InstrumentsJoues]
    PRIMARY KEY CLUSTERED ([Spedinaute_IdSpedinaute], [Instrument_id_instr] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdSpedinaute] in table 'MotDePasses'
ALTER TABLE [dbo].[MotDePasses]
ADD CONSTRAINT [FK_SpedinauteMotDePasse]
    FOREIGN KEY ([IdSpedinaute])
    REFERENCES [dbo].[Spedinautes]
        ([IdSpedinaute])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SpedinauteMotDePasse'
CREATE INDEX [IX_FK_SpedinauteMotDePasse]
ON [dbo].[MotDePasses]
    ([IdSpedinaute]);
GO

-- Creating foreign key on [IdSpedinaute] in table 'TacheEffectuees'
ALTER TABLE [dbo].[TacheEffectuees]
ADD CONSTRAINT [FK_SpedinauteTacheEffectuee]
    FOREIGN KEY ([IdSpedinaute])
    REFERENCES [dbo].[Spedinautes]
        ([IdSpedinaute])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SpedinauteTacheEffectuee'
CREATE INDEX [IX_FK_SpedinauteTacheEffectuee]
ON [dbo].[TacheEffectuees]
    ([IdSpedinaute]);
GO

-- Creating foreign key on [IdSpedinaute] in table 'RibIbans'
ALTER TABLE [dbo].[RibIbans]
ADD CONSTRAINT [FK_SpedinauteRibIban]
    FOREIGN KEY ([IdSpedinaute])
    REFERENCES [dbo].[Spedinautes]
        ([IdSpedinaute])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SpedinauteRibIban'
CREATE INDEX [IX_FK_SpedinauteRibIban]
ON [dbo].[RibIbans]
    ([IdSpedinaute]);
GO

-- Creating foreign key on [IdSpedinaute] in table 'SpedAdresses'
ALTER TABLE [dbo].[SpedAdresses]
ADD CONSTRAINT [FK_SpedinauteSpedAdresse]
    FOREIGN KEY ([IdSpedinaute])
    REFERENCES [dbo].[Spedinautes]
        ([IdSpedinaute])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SpedinauteSpedAdresse'
CREATE INDEX [IX_FK_SpedinauteSpedAdresse]
ON [dbo].[SpedAdresses]
    ([IdSpedinaute]);
GO

-- Creating foreign key on [TypeAdresse] in table 'SpedAdresses'
ALTER TABLE [dbo].[SpedAdresses]
ADD CONSTRAINT [FK_TypeAdressesSpedAdresse]
    FOREIGN KEY ([TypeAdresse])
    REFERENCES [dbo].[TypeAdresses]
        ([TypeAdresse])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TypeAdressesSpedAdresse'
CREATE INDEX [IX_FK_TypeAdressesSpedAdresse]
ON [dbo].[SpedAdresses]
    ([TypeAdresse]);
GO

-- Creating foreign key on [IdAssemblee] in table 'DocumentDisponibles'
ALTER TABLE [dbo].[DocumentDisponibles]
ADD CONSTRAINT [FK_AssembleeDocumentDisponible]
    FOREIGN KEY ([IdAssemblee])
    REFERENCES [dbo].[Assemblees]
        ([IdAssemblee])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AssembleeDocumentDisponible'
CREATE INDEX [IX_FK_AssembleeDocumentDisponible]
ON [dbo].[DocumentDisponibles]
    ([IdAssemblee]);
GO

-- Creating foreign key on [IdRepartition] in table 'DetailsDeRepartions'
ALTER TABLE [dbo].[DetailsDeRepartions]
ADD CONSTRAINT [FK_RepartitionConcerneDetailsDeRepartion]
    FOREIGN KEY ([IdRepartition])
    REFERENCES [dbo].[RepartitionGenerales]
        ([IdRepartition])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RepartitionConcerneDetailsDeRepartion'
CREATE INDEX [IX_FK_RepartitionConcerneDetailsDeRepartion]
ON [dbo].[DetailsDeRepartions]
    ([IdRepartition]);
GO

-- Creating foreign key on [IdSpedinaute] in table 'DetailsDeRepartions'
ALTER TABLE [dbo].[DetailsDeRepartions]
ADD CONSTRAINT [FK_SpedinauteDetailsDeRepartion]
    FOREIGN KEY ([IdSpedinaute])
    REFERENCES [dbo].[Spedinautes]
        ([IdSpedinaute])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SpedinauteDetailsDeRepartion'
CREATE INDEX [IX_FK_SpedinauteDetailsDeRepartion]
ON [dbo].[DetailsDeRepartions]
    ([IdSpedinaute]);
GO

-- Creating foreign key on [IdDetailsDeRepartion] in table 'Feuilles'
ALTER TABLE [dbo].[Feuilles]
ADD CONSTRAINT [FK_DetailsDeRepartionFeuille]
    FOREIGN KEY ([IdDetailsDeRepartion])
    REFERENCES [dbo].[DetailsDeRepartions]
        ([IdDetailsDeRepartion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DetailsDeRepartionFeuille'
CREATE INDEX [IX_FK_DetailsDeRepartionFeuille]
ON [dbo].[Feuilles]
    ([IdDetailsDeRepartion]);
GO

-- Creating foreign key on [IdFeuille] in table 'Informations'
ALTER TABLE [dbo].[Informations]
ADD CONSTRAINT [FK_FeuilleInformation]
    FOREIGN KEY ([IdFeuille])
    REFERENCES [dbo].[Feuilles]
        ([IdFeuille])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FeuilleInformation'
CREATE INDEX [IX_FK_FeuilleInformation]
ON [dbo].[Informations]
    ([IdFeuille]);
GO

-- Creating foreign key on [IdMandataire] in table 'Mandats'
ALTER TABLE [dbo].[Mandats]
ADD CONSTRAINT [FK_MandataireAvoirMandat]
    FOREIGN KEY ([IdMandataire])
    REFERENCES [dbo].[Mandataires]
        ([IdMandataire])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MandataireAvoirMandat'
CREATE INDEX [IX_FK_MandataireAvoirMandat]
ON [dbo].[Mandats]
    ([IdMandataire]);
GO

-- Creating foreign key on [IdSpedinaute] in table 'Mandats'
ALTER TABLE [dbo].[Mandats]
ADD CONSTRAINT [FK_SpedinauteMandat]
    FOREIGN KEY ([IdSpedinaute])
    REFERENCES [dbo].[Spedinautes]
        ([IdSpedinaute])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SpedinauteMandat'
CREATE INDEX [IX_FK_SpedinauteMandat]
ON [dbo].[Mandats]
    ([IdSpedinaute]);
GO

-- Creating foreign key on [IdTypeDeMandat] in table 'Mandats'
ALTER TABLE [dbo].[Mandats]
ADD CONSTRAINT [FK_TypeDeMandatEtMandat]
    FOREIGN KEY ([IdTypeDeMandat])
    REFERENCES [dbo].[TypeDeMandats]
        ([IdTypeDeMandat])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TypeDeMandatEtMandat'
CREATE INDEX [IX_FK_TypeDeMandatEtMandat]
ON [dbo].[Mandats]
    ([IdTypeDeMandat]);
GO

-- Creating foreign key on [Spedinaute_IdSpedinaute] in table 'InstrumentsJoues'
ALTER TABLE [dbo].[InstrumentsJoues]
ADD CONSTRAINT [FK_InstrumentsJoues_Spedinaute]
    FOREIGN KEY ([Spedinaute_IdSpedinaute])
    REFERENCES [dbo].[Spedinautes]
        ([IdSpedinaute])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Instrument_id_instr] in table 'InstrumentsJoues'
ALTER TABLE [dbo].[InstrumentsJoues]
ADD CONSTRAINT [FK_InstrumentsJoues_Instrument]
    FOREIGN KEY ([Instrument_id_instr])
    REFERENCES [dbo].[Instruments]
        ([id_instr])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstrumentsJoues_Instrument'
CREATE INDEX [IX_FK_InstrumentsJoues_Instrument]
ON [dbo].[InstrumentsJoues]
    ([Instrument_id_instr]);
GO

-- Creating foreign key on [Id_pays] in table 'RibIbans'
ALTER TABLE [dbo].[RibIbans]
ADD CONSTRAINT [FK_PaysRibIban]
    FOREIGN KEY ([Id_pays])
    REFERENCES [dbo].[Pays]
        ([id_pays])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PaysRibIban'
CREATE INDEX [IX_FK_PaysRibIban]
ON [dbo].[RibIbans]
    ([Id_pays]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------