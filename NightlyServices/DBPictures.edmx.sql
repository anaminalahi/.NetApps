
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/17/2025 11:21:02
-- Generated from EDMX file: C:\.NetApps\NightlyServices\DBPictures.edmx
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
IF OBJECT_ID(N'[dbo].[PERSONNEL]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PERSONNEL];
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

-- Creating table 'PERSONNEL'
CREATE TABLE [dbo].[PERSONNEL] (
    [EMPID] int  NOT NULL,
    [FULLNAME] nvarchar(100)  NULL,
    [LNL_BLOB] varbinary(max)  NULL,
    [CARD_NUMBER] nvarchar(100)  NULL,
    [STATUS] nvarchar(25)  NULL,
    [ACTIVATION] datetime  NULL,
    [DEACTIVATION] datetime  NULL
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

-- Creating primary key on [EMPID] in table 'PERSONNEL'
ALTER TABLE [dbo].[PERSONNEL]
ADD CONSTRAINT [PK_PERSONNEL]
    PRIMARY KEY CLUSTERED ([EMPID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------