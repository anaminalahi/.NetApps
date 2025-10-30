
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/22/2016 13:45:29
-- Generated from EDMX file: C:\Anaminalahi\GESTELAC\GESTELAC\DBGESTELAC.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [dbgestelac];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AccessoireAvoir_Proprietes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Accessoires_Proprietes] DROP CONSTRAINT [FK_AccessoireAvoir_Proprietes];
GO
IF OBJECT_ID(N'[dbo].[FK_AccessoireCompatiblite]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Compatibilites] DROP CONSTRAINT [FK_AccessoireCompatiblite];
GO
IF OBJECT_ID(N'[dbo].[FK_AccessoireEtVisuels]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Accessoires_Visuels] DROP CONSTRAINT [FK_AccessoireEtVisuels];
GO
IF OBJECT_ID(N'[dbo].[FK_CategorieAccessoire]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Accessoires] DROP CONSTRAINT [FK_CategorieAccessoire];
GO
IF OBJECT_ID(N'[dbo].[FK_MarqueProduitModele]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Modeles] DROP CONSTRAINT [FK_MarqueProduitModele];
GO
IF OBJECT_ID(N'[dbo].[FK_ModeleCompatiblite]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Compatibilites] DROP CONSTRAINT [FK_ModeleCompatiblite];
GO
IF OBJECT_ID(N'[dbo].[FK_ModeleModele_Propriete]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Modeles_Proprietes] DROP CONSTRAINT [FK_ModeleModele_Propriete];
GO
IF OBJECT_ID(N'[dbo].[FK_StatutAccessoire]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Accessoires] DROP CONSTRAINT [FK_StatutAccessoire];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Accessoires]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accessoires];
GO
IF OBJECT_ID(N'[dbo].[Accessoires_Proprietes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accessoires_Proprietes];
GO
IF OBJECT_ID(N'[dbo].[Accessoires_Visuels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accessoires_Visuels];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[Compatibilites]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Compatibilites];
GO
IF OBJECT_ID(N'[dbo].[Marques]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Marques];
GO
IF OBJECT_ID(N'[dbo].[Modeles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Modeles];
GO
IF OBJECT_ID(N'[dbo].[Modeles_Proprietes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Modeles_Proprietes];
GO
IF OBJECT_ID(N'[dbo].[Statuts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Statuts];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Accessoires'
CREATE TABLE [dbo].[Accessoires] (
    [IdAccessoire] int IDENTITY(1,1) NOT NULL,
    [IdCategorie] nvarchar(50)  NULL,
    [Sku] nvarchar(128)  NOT NULL,
    [Stock] int  NULL,
    [PriceDeBase] float  NULL,
    [PrixDeVente] float  NULL,
    [PrixPublic] float  NULL,
    [Libelle_Francais] nvarchar(255)  NULL,
    [Libelle_Anglais] nvarchar(255)  NULL,
    [Description] nvarchar(max)  NULL,
    [Description_Html] nvarchar(max)  NULL,
    [Support] nvarchar(512)  NULL,
    [SupportTechnique] nvarchar(1024)  NULL,
    [IdStatut] nvarchar(25)  NOT NULL,
    [Ean] nvarchar(50)  NULL,
    [UrlImage] nvarchar(512)  NULL,
    [DateAjout] datetime  NULL,
    [DureeDeGarantie] int  NULL,
    [Toremain] varchar(50)  NULL,
    [NombreDeCompatible] int  NULL,
    [ImageAccessoire] varbinary(max)  NULL
);
GO

-- Creating table 'Accessoires_Proprietes'
CREATE TABLE [dbo].[Accessoires_Proprietes] (
    [IdAccessoire_Propriete] int IDENTITY(1,1) NOT NULL,
    [Sku] nvarchar(128)  NOT NULL,
    [Cle] nvarchar(50)  NOT NULL,
    [Valeur] nvarchar(50)  NOT NULL,
    [Parent] int  NULL,
    [IdAccessoire] int  NOT NULL
);
GO

-- Creating table 'Accessoires_Visuels'
CREATE TABLE [dbo].[Accessoires_Visuels] (
    [IdAccessoire_Visuel] int IDENTITY(1,1) NOT NULL,
    [MimeType] nvarchar(max)  NULL,
    [ImageAccessoire] varbinary(max)  NULL,
    [Sku] nvarchar(128)  NULL,
    [DateAjout] datetime  NULL,
    [Taille] int  NULL,
    [IdAccessoire] int  NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [IdCategorie] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Compatibilites'
CREATE TABLE [dbo].[Compatibilites] (
    [IdCompatiblite] int IDENTITY(1,1) NOT NULL,
    [IdModele] int  NOT NULL,
    [IdAccessoire] int  NOT NULL,
    [DateDeMiseAjour] datetime  NOT NULL
);
GO

-- Creating table 'Marques'
CREATE TABLE [dbo].[Marques] (
    [IdMarque] int IDENTITY(1,1) NOT NULL,
    [NomDeLaMarque] nvarchar(50)  NOT NULL,
    [IdGsmArena] int  NOT NULL
);
GO

-- Creating table 'Modeles'
CREATE TABLE [dbo].[Modeles] (
    [IdModele] int IDENTITY(1,1) NOT NULL,
    [NomDuModele] nvarchar(100)  NOT NULL,
    [ReferenceTechnique] nvarchar(100)  NOT NULL,
    [UrlImage] nvarchar(255)  NOT NULL,
    [GsmArenaUrl] nvarchar(255)  NOT NULL,
    [IdMarque] int  NOT NULL,
    [NomDeLaMarque] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Modeles_Proprietes'
CREATE TABLE [dbo].[Modeles_Proprietes] (
    [IdModele_Propriete] int IDENTITY(1,1) NOT NULL,
    [Cle] nvarchar(255)  NULL,
    [Valeur] nvarchar(255)  NULL,
    [IdModele] int  NOT NULL,
    [NomDuModele] nvarchar(100)  NULL,
    [NomDeLaMarque] nvarchar(50)  NULL
);
GO

-- Creating table 'Statuts'
CREATE TABLE [dbo].[Statuts] (
    [IdStatut] nvarchar(25)  NOT NULL,
    [Valeur] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdAccessoire] in table 'Accessoires'
ALTER TABLE [dbo].[Accessoires]
ADD CONSTRAINT [PK_Accessoires]
    PRIMARY KEY CLUSTERED ([IdAccessoire] ASC);
GO

-- Creating primary key on [IdAccessoire_Propriete] in table 'Accessoires_Proprietes'
ALTER TABLE [dbo].[Accessoires_Proprietes]
ADD CONSTRAINT [PK_Accessoires_Proprietes]
    PRIMARY KEY CLUSTERED ([IdAccessoire_Propriete] ASC);
GO

-- Creating primary key on [IdAccessoire_Visuel] in table 'Accessoires_Visuels'
ALTER TABLE [dbo].[Accessoires_Visuels]
ADD CONSTRAINT [PK_Accessoires_Visuels]
    PRIMARY KEY CLUSTERED ([IdAccessoire_Visuel] ASC);
GO

-- Creating primary key on [IdCategorie] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([IdCategorie] ASC);
GO

-- Creating primary key on [IdCompatiblite] in table 'Compatibilites'
ALTER TABLE [dbo].[Compatibilites]
ADD CONSTRAINT [PK_Compatibilites]
    PRIMARY KEY CLUSTERED ([IdCompatiblite] ASC);
GO

-- Creating primary key on [IdMarque] in table 'Marques'
ALTER TABLE [dbo].[Marques]
ADD CONSTRAINT [PK_Marques]
    PRIMARY KEY CLUSTERED ([IdMarque] ASC);
GO

-- Creating primary key on [IdModele] in table 'Modeles'
ALTER TABLE [dbo].[Modeles]
ADD CONSTRAINT [PK_Modeles]
    PRIMARY KEY CLUSTERED ([IdModele] ASC);
GO

-- Creating primary key on [IdModele_Propriete] in table 'Modeles_Proprietes'
ALTER TABLE [dbo].[Modeles_Proprietes]
ADD CONSTRAINT [PK_Modeles_Proprietes]
    PRIMARY KEY CLUSTERED ([IdModele_Propriete] ASC);
GO

-- Creating primary key on [IdStatut] in table 'Statuts'
ALTER TABLE [dbo].[Statuts]
ADD CONSTRAINT [PK_Statuts]
    PRIMARY KEY CLUSTERED ([IdStatut] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdAccessoire] in table 'Accessoires_Proprietes'
ALTER TABLE [dbo].[Accessoires_Proprietes]
ADD CONSTRAINT [FK_AccessoireAvoir_Proprietes]
    FOREIGN KEY ([IdAccessoire])
    REFERENCES [dbo].[Accessoires]
        ([IdAccessoire])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccessoireAvoir_Proprietes'
CREATE INDEX [IX_FK_AccessoireAvoir_Proprietes]
ON [dbo].[Accessoires_Proprietes]
    ([IdAccessoire]);
GO

-- Creating foreign key on [IdAccessoire] in table 'Compatibilites'
ALTER TABLE [dbo].[Compatibilites]
ADD CONSTRAINT [FK_AccessoireCompatiblite]
    FOREIGN KEY ([IdAccessoire])
    REFERENCES [dbo].[Accessoires]
        ([IdAccessoire])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccessoireCompatiblite'
CREATE INDEX [IX_FK_AccessoireCompatiblite]
ON [dbo].[Compatibilites]
    ([IdAccessoire]);
GO

-- Creating foreign key on [IdAccessoire] in table 'Accessoires_Visuels'
ALTER TABLE [dbo].[Accessoires_Visuels]
ADD CONSTRAINT [FK_AccessoireEtVisuels]
    FOREIGN KEY ([IdAccessoire])
    REFERENCES [dbo].[Accessoires]
        ([IdAccessoire])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccessoireEtVisuels'
CREATE INDEX [IX_FK_AccessoireEtVisuels]
ON [dbo].[Accessoires_Visuels]
    ([IdAccessoire]);
GO

-- Creating foreign key on [IdCategorie] in table 'Accessoires'
ALTER TABLE [dbo].[Accessoires]
ADD CONSTRAINT [FK_CategorieAccessoire]
    FOREIGN KEY ([IdCategorie])
    REFERENCES [dbo].[Categories]
        ([IdCategorie])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategorieAccessoire'
CREATE INDEX [IX_FK_CategorieAccessoire]
ON [dbo].[Accessoires]
    ([IdCategorie]);
GO

-- Creating foreign key on [IdStatut] in table 'Accessoires'
ALTER TABLE [dbo].[Accessoires]
ADD CONSTRAINT [FK_StatutAccessoire]
    FOREIGN KEY ([IdStatut])
    REFERENCES [dbo].[Statuts]
        ([IdStatut])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StatutAccessoire'
CREATE INDEX [IX_FK_StatutAccessoire]
ON [dbo].[Accessoires]
    ([IdStatut]);
GO

-- Creating foreign key on [IdModele] in table 'Compatibilites'
ALTER TABLE [dbo].[Compatibilites]
ADD CONSTRAINT [FK_ModeleCompatiblite]
    FOREIGN KEY ([IdModele])
    REFERENCES [dbo].[Modeles]
        ([IdModele])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModeleCompatiblite'
CREATE INDEX [IX_FK_ModeleCompatiblite]
ON [dbo].[Compatibilites]
    ([IdModele]);
GO

-- Creating foreign key on [IdMarque] in table 'Modeles'
ALTER TABLE [dbo].[Modeles]
ADD CONSTRAINT [FK_MarqueProduitModele]
    FOREIGN KEY ([IdMarque])
    REFERENCES [dbo].[Marques]
        ([IdMarque])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MarqueProduitModele'
CREATE INDEX [IX_FK_MarqueProduitModele]
ON [dbo].[Modeles]
    ([IdMarque]);
GO

-- Creating foreign key on [IdModele] in table 'Modeles_Proprietes'
ALTER TABLE [dbo].[Modeles_Proprietes]
ADD CONSTRAINT [FK_ModeleModele_Propriete]
    FOREIGN KEY ([IdModele])
    REFERENCES [dbo].[Modeles]
        ([IdModele])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ModeleModele_Propriete'
CREATE INDEX [IX_FK_ModeleModele_Propriete]
ON [dbo].[Modeles_Proprietes]
    ([IdModele]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------