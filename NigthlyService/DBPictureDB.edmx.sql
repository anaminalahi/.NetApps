
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/09/2025 19:43:35
-- Generated from EDMX file: C:\.NetApps\NigthlyService\DBPictureDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LNN_BLOB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FILTEREDPICTURES]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FILTEREDPICTURES];
GO
IF OBJECT_ID(N'[dbo].[HALFBAD]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HALFBAD];
GO
IF OBJECT_ID(N'[dbo].[PERSONNEL]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PERSONNEL];
GO
IF OBJECT_ID(N'[dbo].[MMOBJS]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MMOBJS];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'FILTEREDPICTURES'
CREATE TABLE [dbo].[FILTEREDPICTURES] (
    [EMPID] int  NOT NULL,
    [OBJECT] int  NOT NULL,
    [TYPE] int  NOT NULL,
    [LNL_BLOB] varbinary(max)  NULL,
    [LASTCHANGED] datetime  NULL,
    [ACCEPTANCETHRESHOLD] int  NULL,
    [BIO_BODYPART] smallint  NULL,
    [LNL_BLOB_TXT] varbinary(max)  NULL,
    [FORMAT_IMAGE] nvarchar(5)  NULL
);
GO

-- Creating table 'HALFBAD'
CREATE TABLE [dbo].[HALFBAD] (
    [EMPID] int  NOT NULL,
    [OBJECT] int  NOT NULL,
    [TYPE] int  NOT NULL,
    [LNL_BLOB] varbinary(max)  NULL,
    [LASTCHANGED] datetime  NULL,
    [ACCEPTANCETHRESHOLD] int  NULL,
    [BIO_BODYPART] smallint  NULL,
    [LNL_BLOB_TXT] varbinary(max)  NULL,
    [FORMAT_IMAGE] nvarchar(5)  NULL
);
GO

-- Creating table 'PERSONNEL'
CREATE TABLE [dbo].[PERSONNEL] (
    [EMPID] int  NOT NULL,
    [FULLNAME] nvarchar(100)  NOT NULL,
    [LNL_BLOB] nvarchar(max)  NULL,
    [CARD_NUMBER] nvarchar(100)  NULL,
    [STATUS] nvarchar(25)  NULL,
    [ACTIVATION] datetime  NULL,
    [DEACTIVATION] datetime  NULL
);
GO

-- Creating table 'MMOBJS'
CREATE TABLE [dbo].[MMOBJS] (
    [EMPID] int  NOT NULL,
    [OBJECT] int  NOT NULL,
    [TYPE] int  NOT NULL,
    [LNL_BLOB] varbinary(max)  NULL,
    [LASTCHANGED] datetime  NULL,
    [ACCEPTANCETHRESHOLD] int  NULL,
    [BIO_BODYPART] smallint  NULL,
    [LNL_BLOB_TXT] varbinary(max)  NULL,
    [FORMAT_IMAGE] nvarchar(5)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [EMPID], [OBJECT], [TYPE] in table 'FILTEREDPICTURES'
ALTER TABLE [dbo].[FILTEREDPICTURES]
ADD CONSTRAINT [PK_FILTEREDPICTURES]
    PRIMARY KEY CLUSTERED ([EMPID], [OBJECT], [TYPE] ASC);
GO

-- Creating primary key on [EMPID], [OBJECT], [TYPE] in table 'HALFBAD'
ALTER TABLE [dbo].[HALFBAD]
ADD CONSTRAINT [PK_HALFBAD]
    PRIMARY KEY CLUSTERED ([EMPID], [OBJECT], [TYPE] ASC);
GO

-- Creating primary key on [EMPID] in table 'PERSONNEL'
ALTER TABLE [dbo].[PERSONNEL]
ADD CONSTRAINT [PK_PERSONNEL]
    PRIMARY KEY CLUSTERED ([EMPID] ASC);
GO

-- Creating primary key on [EMPID], [OBJECT], [TYPE] in table 'MMOBJS'
ALTER TABLE [dbo].[MMOBJS]
ADD CONSTRAINT [PK_MMOBJS]
    PRIMARY KEY CLUSTERED ([EMPID], [OBJECT], [TYPE] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------