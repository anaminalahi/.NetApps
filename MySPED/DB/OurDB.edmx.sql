
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/25/2017 14:59:15
-- Generated from EDMX file: C:\01GitSources\MySPED\DB\OurDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Gespere_prod];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AyantDroTacheHistorise]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TacheHistorisees] DROP CONSTRAINT [FK_AyantDroTacheHistorise];
GO
IF OBJECT_ID(N'[dbo].[FK_Benefici_AyantDroit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Benefici] DROP CONSTRAINT [FK_Benefici_AyantDroit];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Adresses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Adresses];
GO
IF OBJECT_ID(N'[dbo].[AyantDro]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AyantDro];
GO
IF OBJECT_ID(N'[dbo].[Benefici]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Benefici];
GO
IF OBJECT_ID(N'[dbo].[RibIbanArchives]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RibIbanArchives];
GO
IF OBJECT_ID(N'[dbo].[TacheHistorisees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TacheHistorisees];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Adresses'
CREATE TABLE [dbo].[Adresses] (
    [id] int IDENTITY(1,1) NOT NULL,
    [id_adres] char(16)  NULL,
    [id_entit] char(5)  NULL,
    [id_type] char(3)  NULL,
    [no_voie] varchar(5)  NULL,
    [id_typvo] char(3)  NULL,
    [lb_voie] varchar(32)  NULL,
    [adr2] varchar(100)  NULL,
    [codposta] varchar(10)  NULL,
    [ville] varchar(32)  NULL,
    [bureau] varchar(32)  NULL,
    [id_pays] char(3)  NULL,
    [etat] char(3)  NULL,
    [dt_fin] datetime  NULL,
    [id_ayant] char(6)  NULL,
    [no_benef] char(2)  NULL,
    [IntegreDansBachON] bit  NULL
);
GO

-- Creating table 'AyantDro'
CREATE TABLE [dbo].[AyantDro] (
    [id_ayant] char(6)  NOT NULL,
    [no_adher] char(8)  NULL,
    [no_nonAd] char(8)  NULL,
    [id_civil] char(3)  NULL,
    [nom] varchar(32)  NOT NULL,
    [prenom] varchar(32)  NULL,
    [prenom2] varchar(64)  NULL,
    [nomJF] varchar(32)  NULL,
    [pseudo1] varchar(64)  NULL,
    [pseudo2] varchar(32)  NULL,
    [pseudo3] varchar(32)  NULL,
    [dt_naiss] datetime  NULL,
    [lieuNaiss] varchar(32)  NULL,
    [id_pays] char(3)  NULL,
    [id_secu] char(10)  NULL,
    [no_conge] char(10)  NULL,
    [lb_profe] varchar(32)  NULL,
    [id1Instr] char(5)  NULL,
    [id2Instr] char(5)  NULL,
    [id3Instr] char(5)  NULL,
    [groupe] varchar(64)  NULL,
    [lstOrgan] varchar(34)  NULL,
    [dt_adhes] datetime  NULL,
    [mt_ParSo] decimal(5,2)  NULL,
    [dt_sorti] datetime  NULL,
    [lb_sorti] varchar(32)  NULL,
    [typSorti] char(3)  NULL,
    [co_deces] char(1)  NULL,
    [dt_deces] datetime  NULL,
    [dtDAppDG] datetime  NULL,
    [dtFAppDG] datetime  NULL,
    [no_dossi] char(6)  NULL,
    [id_doubl] char(6)  NULL,
    [no_ipd] char(8)  NULL,
    [dt_creat] datetime  NULL,
    [dt_maj] datetime  NULL,
    [id_etat] char(3)  NULL,
    [notes] varchar(max)  NULL,
    [idCUtili] char(5)  NULL,
    [idMUtili] char(5)  NULL,
    [no_sai] char(8)  NULL,
    [no_adami] char(6)  NULL,
    [noCAdami] char(6)  NULL,
    [noCSpedi] char(6)  NULL,
    [NaAd] char(2)  NOT NULL,
    [password] char(10)  NULL,
    [dt_pwEnvoi] datetime  NULL,
    [ipd_PC] char(6)  NULL,
    [ipd_BR] char(6)  NULL,
    [ipd_CP] char(6)  NULL,
    [ipd_MA] char(6)  NULL,
    [ipd_LE] char(6)  NULL,
    [ipd_PP] char(6)  NULL,
    [ipd_RE] char(6)  NULL,
    [ipd_RP] char(6)  NULL,
    [ipd_RR] char(6)  NULL,
    [ipd_dup] bit  NULL,
    [ipd_verif] bit  NULL,
    [ipd_dtVer] datetime  NULL,
    [IntegreDansBachON] bit  NULL
);
GO

-- Creating table 'Benefici'
CREATE TABLE [dbo].[Benefici] (
    [id_benef] char(8)  NOT NULL,
    [id_ayant] char(6)  NOT NULL,
    [no_benef] char(2)  NOT NULL,
    [QuaBenef] char(2)  NULL,
    [id_civil] char(3)  NULL,
    [nom] varchar(32)  NULL,
    [prenom] varchar(32)  NULL,
    [prenom2] varchar(64)  NULL,
    [nomjf] varchar(32)  NULL,
    [no_telfi] varchar(17)  NULL,
    [no_telpo] varchar(17)  NULL,
    [no_telec] varchar(17)  NULL,
    [mele] varchar(64)  NULL,
    [dt_naiss] datetime  NULL,
    [lieunaiss] varchar(32)  NULL,
    [id_pays] char(3)  NULL,
    [dt_sorti] datetime  NULL,
    [lb_sorti] varchar(32)  NULL,
    [typsorti] char(3)  NULL,
    [co_deces] char(1)  NULL,
    [dt_deces] datetime  NULL,
    [tx_quoti] decimal(6,2)  NULL,
    [txrdroit] decimal(6,2)  NULL,
    [txrvmp] decimal(6,2)  NULL,
    [txrdat] decimal(6,2)  NULL,
    [id_modpa] char(3)  NULL,
    [nombanqu] varchar(32)  NULL,
    [co_banqu] char(5)  NULL,
    [co_guich] char(5)  NULL,
    [co_compt] char(30)  NULL,
    [clerib] char(2)  NULL,
    [bic] char(11)  NULL,
    [aba] char(9)  NULL,
    [iban] varchar(34)  NULL,
    [lb_domic] varchar(32)  NULL,
    [ad1banqu] varchar(32)  NULL,
    [ad2banqu] varchar(32)  NULL,
    [cp_banqu] varchar(8)  NULL,
    [vilbanq] varchar(32)  NULL,
    [paybanqu] char(3)  NULL,
    [id2benef] char(8)  NULL,
    [notes] varchar(max)  NULL,
    [id_etat] char(3)  NULL,
    [ribvalab] bit  NULL,
    [instit_can] char(6)  NULL,
    [branch_can] char(7)  NULL,
    [IntegreDansBachON] bit  NULL
);
GO

-- Creating table 'RibIbanArchives'
CREATE TABLE [dbo].[RibIbanArchives] (
    [IdRibIban] int IDENTITY(1,1) NOT NULL,
    [Titulaire] nvarchar(100)  NULL,
    [BicSwift] nvarchar(11)  NULL,
    [NumeroIBAN] nvarchar(34)  NULL,
    [Domiciliation] nvarchar(100)  NULL,
    [CodeBanque] nvarchar(5)  NULL,
    [Guichet] nvarchar(5)  NULL,
    [NumeroCompte] nvarchar(50)  NULL,
    [CleRIB] nvarchar(3)  NULL,
    [Pays] nvarchar(3)  NULL,
    [DateDeSaisie] datetime  NULL,
    [Actuel] bit  NOT NULL,
    [IntegreDansBachON] bit  NOT NULL,
    [IdSpedinaute] int  NOT NULL,
    [id_ayant] char(6)  NOT NULL
);
GO

-- Creating table 'TacheHistorisees'
CREATE TABLE [dbo].[TacheHistorisees] (
    [IdTacheHistorisee] int IDENTITY(1,1) NOT NULL,
    [TypeDeTache] nvarchar(1)  NOT NULL,
    [DateDuJour] datetime  NOT NULL,
    [HeureCourante] time  NOT NULL,
    [IdAdresseConcernee] nvarchar(1)  NULL,
    [IdModePasseConcerne] nvarchar(1)  NULL,
    [IdRibIbanConcerne] nvarchar(1)  NOT NULL,
    [FicheConcernee] nvarchar(1)  NOT NULL,
    [Commentaire] nvarchar(max)  NOT NULL,
    [HistoriserON] bit  NOT NULL,
    [IdSpedinaute] int  NOT NULL,
    [Id_ayant] char(6)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Adresses'
ALTER TABLE [dbo].[Adresses]
ADD CONSTRAINT [PK_Adresses]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id_ayant] in table 'AyantDro'
ALTER TABLE [dbo].[AyantDro]
ADD CONSTRAINT [PK_AyantDro]
    PRIMARY KEY CLUSTERED ([id_ayant] ASC);
GO

-- Creating primary key on [id_benef] in table 'Benefici'
ALTER TABLE [dbo].[Benefici]
ADD CONSTRAINT [PK_Benefici]
    PRIMARY KEY CLUSTERED ([id_benef] ASC);
GO

-- Creating primary key on [IdRibIban] in table 'RibIbanArchives'
ALTER TABLE [dbo].[RibIbanArchives]
ADD CONSTRAINT [PK_RibIbanArchives]
    PRIMARY KEY CLUSTERED ([IdRibIban] ASC);
GO

-- Creating primary key on [IdTacheHistorisee] in table 'TacheHistorisees'
ALTER TABLE [dbo].[TacheHistorisees]
ADD CONSTRAINT [PK_TacheHistorisees]
    PRIMARY KEY CLUSTERED ([IdTacheHistorisee] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [id_ayant] in table 'Benefici'
ALTER TABLE [dbo].[Benefici]
ADD CONSTRAINT [FK_Benefici_AyantDroit]
    FOREIGN KEY ([id_ayant])
    REFERENCES [dbo].[AyantDro]
        ([id_ayant])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Benefici_AyantDroit'
CREATE INDEX [IX_FK_Benefici_AyantDroit]
ON [dbo].[Benefici]
    ([id_ayant]);
GO

-- Creating foreign key on [Id_ayant] in table 'TacheHistorisees'
ALTER TABLE [dbo].[TacheHistorisees]
ADD CONSTRAINT [FK_AyantDroTacheHistorise]
    FOREIGN KEY ([Id_ayant])
    REFERENCES [dbo].[AyantDro]
        ([id_ayant])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AyantDroTacheHistorise'
CREATE INDEX [IX_FK_AyantDroTacheHistorise]
ON [dbo].[TacheHistorisees]
    ([Id_ayant]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------