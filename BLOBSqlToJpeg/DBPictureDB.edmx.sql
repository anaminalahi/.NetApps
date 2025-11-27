
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/24/2024 11:55:23
-- Generated from EDMX file: C:\MasenLabo\BLOBSqlToJpeg\BLOBSqlToJpeg\DBPictureDB.edmx
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

IF OBJECT_ID(N'[DBPictureDBModelStoreContainer].[FILTEREDPICTURES]', 'U') IS NOT NULL
    DROP TABLE [DBPictureDBModelStoreContainer].[FILTEREDPICTURES];
GO
IF OBJECT_ID(N'[DBPictureDBModelStoreContainer].[HALFBAD]', 'U') IS NOT NULL
    DROP TABLE [DBPictureDBModelStoreContainer].[HALFBAD];
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

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------