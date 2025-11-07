
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/17/2024 03:40:28
-- Generated from EDMX file: C:\MasenLabo\BLOBSqlToJpeg\BLOBSqlToJpeg\BlobSQLDB.edmx
-- --------------------------------------------------
                   
SET QUOTED_IDENTIFIER OFF;
GO   
USE [BLOB2PngAndJpeg];
GO     
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO                   
         
-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------
                                 
-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
    
IF OBJECT_ID(N'[dbo].[PERSONEL]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PERSONEL];
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
    [FORMAT_IMAGE] nvarchar(5)  NULL,
    [DEACTIVATIONDATE] datetime  NULL,
    [CSN] nvarchar(100)  NULL
);

GO
  
-- Creating table 'PERSONEL'
CREATE TABLE [dbo].[PERSONEL] (
    [EMPID] smallint  NOT NULL,
    [FIRSTNAME] nvarchar(50)  NULL,
    [LASTNAME] nvarchar(50)  NULL,
    [LNL_BLOB] nvarchar(max)  NULL,
    [OBJECT] tinyint  NULL,
    [TYPE] tinyint  NULL
);       
GO    
    
-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------
            
-- Creating primary key on [EMPID] in table 'PERSONEL'
ALTER TABLE [dbo].[PERSONEL]
ADD CONSTRAINT [PK_PERSONEL]
    PRIMARY KEY CLUSTERED ([EMPID] ASC);
GO  
                               
-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------
                 
-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------