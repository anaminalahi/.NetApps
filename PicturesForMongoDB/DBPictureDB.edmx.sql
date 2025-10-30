
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/26/2025 22:53:58
-- Generated from EDMX file: C:\.NetApps\PicturesForMongoDB\DBPictureDB.edmx
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

IF OBJECT_ID(N'[DBPictureDBModelStoreContainer].[MMOBJS]', 'U') IS NOT NULL
    DROP TABLE [DBPictureDBModelStoreContainer].[MMOBJS];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

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