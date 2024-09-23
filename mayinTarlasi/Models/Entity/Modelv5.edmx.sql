
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/01/2024 10:20:30
-- Generated from EDMX file: C:\Users\quick\Documents\Kişisel Belgelerim\İş\mayinTarlasi\mayinTarlasi\Models\Entity\Modelv5.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MVCTest];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PersonelMessage_TBLuserData]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonelMessage] DROP CONSTRAINT [FK_PersonelMessage_TBLuserData];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonelMessage_TBLuserData1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonelMessage] DROP CONSTRAINT [FK_PersonelMessage_TBLuserData1];
GO
IF OBJECT_ID(N'[dbo].[FK_PublicMessenger_TBLgameData]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PublicMessenger] DROP CONSTRAINT [FK_PublicMessenger_TBLgameData];
GO
IF OBJECT_ID(N'[dbo].[FK_PublicMessenger_TBLuserData]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PublicMessenger] DROP CONSTRAINT [FK_PublicMessenger_TBLuserData];
GO
IF OBJECT_ID(N'[dbo].[FK_TBLgameData_TBLuserData]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TBLgameData] DROP CONSTRAINT [FK_TBLgameData_TBLuserData];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PersonelMessage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonelMessage];
GO
IF OBJECT_ID(N'[dbo].[PublicMessenger]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PublicMessenger];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[TBLgameData]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TBLgameData];
GO
IF OBJECT_ID(N'[dbo].[TBLuserData]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TBLuserData];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PublicMessengers'
CREATE TABLE [dbo].[PublicMessengers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SenderID] int  NOT NULL,
    [SenderName] nvarchar(50)  NULL,
    [Message] nvarchar(max)  NULL,
    [Senddate] datetime  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'TBLgameDatas'
CREATE TABLE [dbo].[TBLgameDatas] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NOT NULL,
    [Score] int  NULL,
    [flagsUsed] int  NULL,
    [Time] int  NULL,
    [Try] int  NOT NULL
);
GO

-- Creating table 'TBLuserDatas'
CREATE TABLE [dbo].[TBLuserDatas] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(50)  NULL,
    [Password] nvarchar(50)  NULL,
    [Email] nvarchar(50)  NULL,
    [NameSurname] nvarchar(50)  NULL,
    [ProfilePic] int  NULL
);
GO

-- Creating table 'PersonelMessages'
CREATE TABLE [dbo].[PersonelMessages] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SenderID] int  NOT NULL,
    [ReceiverID] int  NOT NULL,
    [Message] nvarchar(max)  NULL,
    [Date] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'PublicMessengers'
ALTER TABLE [dbo].[PublicMessengers]
ADD CONSTRAINT [PK_PublicMessengers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [ID] in table 'TBLgameDatas'
ALTER TABLE [dbo].[TBLgameDatas]
ADD CONSTRAINT [PK_TBLgameDatas]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'TBLuserDatas'
ALTER TABLE [dbo].[TBLuserDatas]
ADD CONSTRAINT [PK_TBLuserDatas]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'PersonelMessages'
ALTER TABLE [dbo].[PersonelMessages]
ADD CONSTRAINT [PK_PersonelMessages]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [SenderID] in table 'PublicMessengers'
ALTER TABLE [dbo].[PublicMessengers]
ADD CONSTRAINT [FK_PublicMessenger_TBLgameData]
    FOREIGN KEY ([SenderID])
    REFERENCES [dbo].[TBLgameDatas]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PublicMessenger_TBLgameData'
CREATE INDEX [IX_FK_PublicMessenger_TBLgameData]
ON [dbo].[PublicMessengers]
    ([SenderID]);
GO

-- Creating foreign key on [SenderID] in table 'PublicMessengers'
ALTER TABLE [dbo].[PublicMessengers]
ADD CONSTRAINT [FK_PublicMessenger_TBLuserData]
    FOREIGN KEY ([SenderID])
    REFERENCES [dbo].[TBLuserDatas]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PublicMessenger_TBLuserData'
CREATE INDEX [IX_FK_PublicMessenger_TBLuserData]
ON [dbo].[PublicMessengers]
    ([SenderID]);
GO

-- Creating foreign key on [UserID] in table 'TBLgameDatas'
ALTER TABLE [dbo].[TBLgameDatas]
ADD CONSTRAINT [FK_TBLgameData_TBLuserData]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[TBLuserDatas]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TBLgameData_TBLuserData'
CREATE INDEX [IX_FK_TBLgameData_TBLuserData]
ON [dbo].[TBLgameDatas]
    ([UserID]);
GO

-- Creating foreign key on [SenderID] in table 'PersonelMessages'
ALTER TABLE [dbo].[PersonelMessages]
ADD CONSTRAINT [FK_PersonelMessage_TBLuserData]
    FOREIGN KEY ([SenderID])
    REFERENCES [dbo].[TBLuserDatas]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonelMessage_TBLuserData'
CREATE INDEX [IX_FK_PersonelMessage_TBLuserData]
ON [dbo].[PersonelMessages]
    ([SenderID]);
GO

-- Creating foreign key on [ReceiverID] in table 'PersonelMessages'
ALTER TABLE [dbo].[PersonelMessages]
ADD CONSTRAINT [FK_PersonelMessage_TBLuserData1]
    FOREIGN KEY ([ReceiverID])
    REFERENCES [dbo].[TBLuserDatas]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonelMessage_TBLuserData1'
CREATE INDEX [IX_FK_PersonelMessage_TBLuserData1]
ON [dbo].[PersonelMessages]
    ([ReceiverID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------