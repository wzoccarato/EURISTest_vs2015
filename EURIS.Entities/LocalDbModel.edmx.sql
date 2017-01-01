
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/01/2017 19:09:14
-- Generated from EDMX file: D:\Progetti\Walter\EURISTest_vs2015\EURISTest_vs2015\EURIS.Entities\LocalDbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EurisTestDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ListinoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ListinoSet];
GO
IF OBJECT_ID(N'[dbo].[ProdottoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProdottoSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ListinoSet'
CREATE TABLE [dbo].[ListinoSet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [codice] nvarchar(50)  NOT NULL,
    [descrizione] nvarchar(max)  NULL,
    [id_prodotto] int  NOT NULL
);
GO

-- Creating table 'ProdottoSet'
CREATE TABLE [dbo].[ProdottoSet] (
    [id] int IDENTITY(1,1) NOT NULL,
    [codice] nvarchar(50)  NOT NULL,
    [descrizione] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id], [id_prodotto] in table 'ListinoSet'
ALTER TABLE [dbo].[ListinoSet]
ADD CONSTRAINT [PK_ListinoSet]
    PRIMARY KEY CLUSTERED ([id], [id_prodotto] ASC);
GO

-- Creating primary key on [id] in table 'ProdottoSet'
ALTER TABLE [dbo].[ProdottoSet]
ADD CONSTRAINT [PK_ProdottoSet]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------