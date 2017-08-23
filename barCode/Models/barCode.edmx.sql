
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/07/2017 18:03:37
-- Generated from EDMX file: C:\Users\alexh\Documents\Visual Studio 2017\Projects\barCode\barCode\Models\barCode.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [barCode];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Administradores]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Administradores];
GO
IF OBJECT_ID(N'[dbo].[Boleta]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Boleta];
GO
IF OBJECT_ID(N'[dbo].[Categoria]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categoria];
GO
IF OBJECT_ID(N'[dbo].[Cliente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cliente];
GO
IF OBJECT_ID(N'[dbo].[Comuna]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comuna];
GO
IF OBJECT_ID(N'[dbo].[Direccion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Direccion];
GO
IF OBJECT_ID(N'[dbo].[Distribuidor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Distribuidor];
GO
IF OBJECT_ID(N'[dbo].[HistorialCompras]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HistorialCompras];
GO
IF OBJECT_ID(N'[dbo].[MediosdePago]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MediosdePago];
GO
IF OBJECT_ID(N'[dbo].[Producto]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Producto];
GO
IF OBJECT_ID(N'[dbo].[Region]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Region];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Boleta'
CREATE TABLE [dbo].[Boleta] (
    [IdBoleta] int IDENTITY(1,1) NOT NULL,
    [IdRegistro] int  NOT NULL,
    [IdCliente] int  NOT NULL,
    [IdDistribuidor] int  NULL,
    [NroBoleta] int  NULL,
    [Total] int  NULL,
    [Fecha] varchar(20)  NULL,
    [Despachado] bit  NULL,
    [Detalle] varchar(200)  NULL
);
GO

-- Creating table 'Comuna'
CREATE TABLE [dbo].[Comuna] (
    [IdComuna] int IDENTITY(1,1) NOT NULL,
    [IdRegion] int  NOT NULL,
    [Comuna1] varchar(50)  NOT NULL
);
GO

-- Creating table 'Direccion'
CREATE TABLE [dbo].[Direccion] (
    [IdDireccion] int IDENTITY(1,1) NOT NULL,
    [IdDistribuidor] int  NULL,
    [IdComuna] int  NOT NULL,
    [IdCliente] int  NOT NULL,
    [Direccion1] varchar(100)  NULL
);
GO

-- Creating table 'Distribuidor'
CREATE TABLE [dbo].[Distribuidor] (
    [IdDistribuidor] int IDENTITY(1,1) NOT NULL,
    [Distribuidor1] varchar(50)  NULL,
    [Comentarios] varchar(200)  NULL
);
GO

-- Creating table 'HistorialCompras'
CREATE TABLE [dbo].[HistorialCompras] (
    [IdRegistro] int IDENTITY(1,1) NOT NULL,
    [IdCliente] int  NULL,
    [FechaCompra] datetime  NOT NULL
);
GO

-- Creating table 'MediosdePago'
CREATE TABLE [dbo].[MediosdePago] (
    [IdPago] int IDENTITY(1,1) NOT NULL,
    [IdBoleta] int  NOT NULL,
    [MedioPago] varchar(50)  NOT NULL
);
GO

-- Creating table 'Region'
CREATE TABLE [dbo].[Region] (
    [IdRegion] int IDENTITY(1,1) NOT NULL,
    [Region1] varchar(50)  NOT NULL
);
GO

-- Creating table 'Producto'
CREATE TABLE [dbo].[Producto] (
    [IdProducto] int IDENTITY(1,1) NOT NULL,
    [IdBoleta] int  NULL,
    [IdCategoria] varchar(50)  NOT NULL,
    [Foto] varchar(max)  NULL,
    [NombreProd] varchar(50)  NULL,
    [Marca] varchar(50)  NULL,
    [Stock] int  NULL,
    [Precio] int  NOT NULL,
    [Descripcion] varchar(2000)  NULL,
    [Eliminado] bit  NULL,
    [Grado] varchar(5)  NULL
);
GO

-- Creating table 'Categoria'
CREATE TABLE [dbo].[Categoria] (
    [IdCategoria] varchar(50)  NOT NULL
);
GO

-- Creating table 'Administradores'
CREATE TABLE [dbo].[Administradores] (
    [IdAdmin] int IDENTITY(1,1) NOT NULL,
    [IdDistribuidor] int  NULL,
    [RutAdm] char(10)  NOT NULL,
    [NombreAdm] varchar(50)  NULL,
    [ApPatAdm] varchar(50)  NULL,
    [ApMatAdm] varchar(50)  NULL,
    [Fono] varchar(50)  NULL,
    [Usuario] varchar(20)  NULL,
    [Contrasena] varchar(50)  NULL
);
GO

-- Creating table 'Cliente'
CREATE TABLE [dbo].[Cliente] (
    [IdCliente] int IDENTITY(1,1) NOT NULL,
    [Rut] char(10)  NOT NULL,
    [Nombres] varchar(50)  NULL,
    [ApPaterno] varchar(50)  NULL,
    [ApMaterno] varchar(50)  NULL,
    [Telefono] varchar(50)  NULL,
    [User] varchar(50)  NULL,
    [Pass] varchar(20)  NULL,
    [fechaNacimiento] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdBoleta] in table 'Boleta'
ALTER TABLE [dbo].[Boleta]
ADD CONSTRAINT [PK_Boleta]
    PRIMARY KEY CLUSTERED ([IdBoleta] ASC);
GO

-- Creating primary key on [IdComuna] in table 'Comuna'
ALTER TABLE [dbo].[Comuna]
ADD CONSTRAINT [PK_Comuna]
    PRIMARY KEY CLUSTERED ([IdComuna] ASC);
GO

-- Creating primary key on [IdDireccion] in table 'Direccion'
ALTER TABLE [dbo].[Direccion]
ADD CONSTRAINT [PK_Direccion]
    PRIMARY KEY CLUSTERED ([IdDireccion] ASC);
GO

-- Creating primary key on [IdDistribuidor] in table 'Distribuidor'
ALTER TABLE [dbo].[Distribuidor]
ADD CONSTRAINT [PK_Distribuidor]
    PRIMARY KEY CLUSTERED ([IdDistribuidor] ASC);
GO

-- Creating primary key on [IdRegistro] in table 'HistorialCompras'
ALTER TABLE [dbo].[HistorialCompras]
ADD CONSTRAINT [PK_HistorialCompras]
    PRIMARY KEY CLUSTERED ([IdRegistro] ASC);
GO

-- Creating primary key on [IdPago] in table 'MediosdePago'
ALTER TABLE [dbo].[MediosdePago]
ADD CONSTRAINT [PK_MediosdePago]
    PRIMARY KEY CLUSTERED ([IdPago] ASC);
GO

-- Creating primary key on [IdRegion] in table 'Region'
ALTER TABLE [dbo].[Region]
ADD CONSTRAINT [PK_Region]
    PRIMARY KEY CLUSTERED ([IdRegion] ASC);
GO

-- Creating primary key on [IdProducto] in table 'Producto'
ALTER TABLE [dbo].[Producto]
ADD CONSTRAINT [PK_Producto]
    PRIMARY KEY CLUSTERED ([IdProducto] ASC);
GO

-- Creating primary key on [IdCategoria] in table 'Categoria'
ALTER TABLE [dbo].[Categoria]
ADD CONSTRAINT [PK_Categoria]
    PRIMARY KEY CLUSTERED ([IdCategoria] ASC);
GO

-- Creating primary key on [IdAdmin] in table 'Administradores'
ALTER TABLE [dbo].[Administradores]
ADD CONSTRAINT [PK_Administradores]
    PRIMARY KEY CLUSTERED ([IdAdmin] ASC);
GO

-- Creating primary key on [IdCliente] in table 'Cliente'
ALTER TABLE [dbo].[Cliente]
ADD CONSTRAINT [PK_Cliente]
    PRIMARY KEY CLUSTERED ([IdCliente] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------