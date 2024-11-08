USE [master]
GO
/****** Object:  Database [ImportadoraMoyaUlate]    Script Date: 3/21/2024 12:32:14 AM ******/
CREATE DATABASE [ImportadoraMoyaUlate]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ImportadoraMoyaUltae', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ImportadoraMoyaUltae.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ImportadoraMoyaUltae_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ImportadoraMoyaUltae_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ImportadoraMoyaUlate].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET ARITHABORT OFF 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET  MULTI_USER 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET QUERY_STORE = ON
GO
ALTER DATABASE [ImportadoraMoyaUlate] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ImportadoraMoyaUlate]
GO
/****** Object:  Table [dbo].[Canton]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Canton](
	[ID_Provincia] [int] NOT NULL,
	[ID_Canton] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Tbl_Canton] PRIMARY KEY CLUSTERED 
(
	[ID_Canton] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carrito]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carrito](
	[ID_Carrito] [bigint] IDENTITY(1,1) NOT NULL,
	[ID_Usuario] [bigint] NOT NULL,
	[ID_Producto] [bigint] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[FechaCarrito] [datetime] NOT NULL,
 CONSTRAINT [PK_TCarrito] PRIMARY KEY CLUSTERED 
(
	[ID_Carrito] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[ID_Categoria] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Categoria] [varchar](50) NOT NULL,
	[Estado_Categoria] [int] NOT NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[ID_Categoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[compras]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[compras](
	[id_compras] [int] IDENTITY(1,1) NOT NULL,
	[Empresa] [bigint] NOT NULL,
	[fecha] [datetime] NOT NULL,
	[concepto] [nvarchar](255) NOT NULL,
	[cantidad] [int] NOT NULL,
	[total] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_compras] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Direcciones]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Direcciones](
	[ID_Direccion] [int] IDENTITY(1,1) NOT NULL,
	[ID_Provincia] [int] NOT NULL,
	[ID_Canton] [int] NOT NULL,
	[ID_Distrito] [int] NOT NULL,
	[Direccion_Exacta] [varchar](500) NOT NULL,
 CONSTRAINT [PK_Tbl_Direcciones] PRIMARY KEY CLUSTERED 
(
	[ID_Direccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Distrito]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Distrito](
	[ID_Provincia] [int] NOT NULL,
	[ID_Canton] [int] NOT NULL,
	[ID_Distrito] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Tbl_Distrito] PRIMARY KEY CLUSTERED 
(
	[ID_Distrito] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empresa]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empresa](
	[ID_Empresa] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre_empresa] [varchar](255) NOT NULL,
	[Descripcion] [varchar](255) NOT NULL,
	[Ubicacion] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Empresa] PRIMARY KEY CLUSTERED 
(
	[ID_Empresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estado]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estado](
	[ID_Estado] [int] NOT NULL,
	[Tipo_Estado] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Estado] PRIMARY KEY CLUSTERED 
(
	[ID_Estado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Factura_Detalle]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factura_Detalle](
	[ID_Detalle] [bigint] IDENTITY(1,1) NOT NULL,
	[ID_Factura] [bigint] NOT NULL,
	[ID_Producto] [bigint] NOT NULL,
	[PrecioPagado] [decimal](18, 2) NOT NULL,
	[CantidadPagado] [int] NOT NULL,
	[ImpuestoPagado] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_FacturaDetalle] PRIMARY KEY CLUSTERED 
(
	[ID_Detalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Factura_Encabezado]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factura_Encabezado](
	[ID_Factura] [bigint] IDENTITY(1,1) NOT NULL,
	[ID_Usuario] [bigint] NOT NULL,
	[FechaCompra] [datetime] NOT NULL,
	[TotalCompra] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_FacturaEncabezado] PRIMARY KEY CLUSTERED 
(
	[ID_Factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Identificacion]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Identificacion](
	[ID_Identificacion] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Tbl_Identificacion] PRIMARY KEY CLUSTERED 
(
	[ID_Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedidos]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedidos](
	[ID_Pedido] [bigint] IDENTITY(1,1) NOT NULL,
	[ID_Cliente] [bigint] NOT NULL,
	[ID_Transaccion] [varchar](255) NOT NULL,
	[ID_Factura] [bigint] NOT NULL,
	[Estado] [int] NOT NULL,
 CONSTRAINT [PK_TPedidos] PRIMARY KEY CLUSTERED 
(
	[ID_Pedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[ID_Producto] [bigint] IDENTITY(1,1) NOT NULL,
	[ID_Categoria] [int] NOT NULL,
	[Nombre] [varchar](250) NOT NULL,
	[Descripcion] [varchar](500) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Precio] [decimal](18, 2) NOT NULL,
	[SKU] [varchar](10) NOT NULL,
	[Imagen] [varchar](250) NOT NULL,
	[Estado] [int] NOT NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[ID_Producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedores]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedores](
	[ID_Proveedor] [bigint] IDENTITY(1,1) NOT NULL,
	[ID_Identificacion] [int] NOT NULL,
	[Nombre_Proveedor] [varchar](255) NOT NULL,
	[Apellido_Proveedor] [varchar](255) NULL,
	[Cedula_Proveedor] [varchar](15) NOT NULL,
	[Direccion_Exacta] [varchar](255) NOT NULL,
	[Estado_Proveedor] [int] NOT NULL,
	[Empresa] [bigint] NOT NULL,
	[Telefono] [varchar](15) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Proveedores] PRIMARY KEY CLUSTERED 
(
	[ID_Proveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Provincia]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Provincia](
	[ID_Provincia] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](15) NOT NULL,
 CONSTRAINT [PK_Tbl_Provincia] PRIMARY KEY CLUSTERED 
(
	[ID_Provincia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ID_Rol] [int] NOT NULL,
	[Nombre_Rol] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Tbl_Rol] PRIMARY KEY CLUSTERED 
(
	[ID_Rol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[ID_Usuario] [bigint] IDENTITY(1,1) NOT NULL,
	[ID_Identificacion] [int] NOT NULL,
	[Identificacion_Usuario] [varchar](25) NOT NULL,
	[Nombre_Usuario] [varchar](60) NOT NULL,
	[Apellido_Usuario] [varchar](70) NULL,
	[Correo_Usuario] [varchar](100) NOT NULL,
	[Contrasenna_Usuario] [varchar](50) NOT NULL,
	[ID_Direccion] [int] NULL,
	[Telefono_Usuario] [varchar](20) NULL,
	[ID_Estado] [int] NOT NULL,
	[ID_Rol] [int] NOT NULL,
	[C_esTemporal] [int] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[ID_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (1, 101, N'San José')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (1, 102, N'Escazú')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (1, 103, N'Desamparados')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (1, 104, N'Puriscal')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (1, 105, N'Tarrazú')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (1, 106, N'Aserrí')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (1, 107, N'Mora')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (1, 108, N'Goicoechea')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (1, 109, N'Santa Ana')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (1, 110, N'Alajuelita')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (1, 111, N'Vázquez de Coronado')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (1, 112, N'Acosta')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (1, 113, N'Tibás')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (1, 114, N'Moravia')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (1, 115, N'Montes de Oca')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (1, 116, N'Turrubares')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (1, 117, N'Dota')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (1, 118, N'Curridabat')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (1, 119, N'Pérez Zeledón')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (1, 120, N'León Cortés Castro')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (2, 201, N'Alajuela')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (2, 202, N'San Ramón')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (2, 203, N'Grecia')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (2, 204, N'San Mateo')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (2, 205, N'Atenas')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (2, 206, N'Naranjo')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (2, 207, N'Palmares')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (2, 208, N'Poás')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (2, 209, N'Orotina')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (2, 210, N'San Carlos')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (2, 211, N'Zarcero')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (2, 212, N'Sarchí')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (2, 213, N'Upala')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (2, 214, N'Los Chiles')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (2, 215, N'Guatuso')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (2, 216, N'Río Cuarto')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (3, 301, N'Cartago')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (3, 302, N'Paraíso')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (3, 303, N'La Unión')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (3, 304, N'Jiménez')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (3, 305, N'Turrialba')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (3, 306, N'Alvarado')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (3, 307, N'Oreamuno')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (3, 308, N'El Guarco')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (4, 401, N'Heredia')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (4, 402, N'Barva')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (4, 403, N'Santo Domingo')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (4, 404, N'Santa Bárbara')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (4, 405, N'San Rafael')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (4, 406, N'San Isidro')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (4, 407, N'Belén')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (4, 408, N'Flores')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (4, 409, N'San Pablo')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (4, 410, N'Sarapiquí')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (5, 501, N'Liberia')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (5, 502, N'Nicoya')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (5, 503, N'Santa Cruz')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (5, 504, N'Bagaces')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (5, 505, N'Carrillo')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (5, 506, N'Cañas')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (5, 507, N'Abangares')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (5, 508, N'Tilarán')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (5, 509, N'Nandayure')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (5, 510, N'La Cruz')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (5, 511, N'Hojancha')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (6, 601, N'Puntarenas')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (6, 602, N'Esparza')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (6, 603, N'Buenos Aires')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (6, 604, N'Montes de Oro')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (6, 605, N'Osa')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (6, 606, N'Quepos')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (6, 607, N'Golfito')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (6, 608, N'Coto Brus')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (6, 609, N'Parrita')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (6, 610, N'Corredores')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (6, 611, N'Garabito')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (6, 612, N'Monteverde')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (7, 701, N'Limón')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (7, 702, N'Pococí')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (7, 703, N'Siquirres')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (7, 704, N'Talamanca')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (7, 705, N'Matina')
INSERT [dbo].[Canton] ([ID_Provincia], [ID_Canton], [Nombre]) VALUES (7, 706, N'Guácimo')
GO
SET IDENTITY_INSERT [dbo].[Categorias] ON 

INSERT [dbo].[Categorias] ([ID_Categoria], [Nombre_Categoria], [Estado_Categoria]) VALUES (1, N'Hombre', 1)
INSERT [dbo].[Categorias] ([ID_Categoria], [Nombre_Categoria], [Estado_Categoria]) VALUES (2, N'Mujer', 1)
INSERT [dbo].[Categorias] ([ID_Categoria], [Nombre_Categoria], [Estado_Categoria]) VALUES (3, N'Niños', 1)
SET IDENTITY_INSERT [dbo].[Categorias] OFF
GO
SET IDENTITY_INSERT [dbo].[compras] ON 

INSERT [dbo].[compras] ([id_compras], [Empresa], [fecha], [concepto], [cantidad], [total]) VALUES (1, 1, CAST(N'2024-03-20T12:00:00.000' AS DateTime), N'pruebaaaa', 5, CAST(4.00 AS Decimal(10, 2)))
INSERT [dbo].[compras] ([id_compras], [Empresa], [fecha], [concepto], [cantidad], [total]) VALUES (2, 1, CAST(N'2024-03-20T12:00:00.000' AS DateTime), N'pruebita', 1, CAST(1500.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[compras] OFF
GO
SET IDENTITY_INSERT [dbo].[Direcciones] ON 

INSERT [dbo].[Direcciones] ([ID_Direccion], [ID_Provincia], [ID_Canton], [ID_Distrito], [Direccion_Exacta]) VALUES (1, 1, 101, 10101, N'500 metros oeste ')
INSERT [dbo].[Direcciones] ([ID_Direccion], [ID_Provincia], [ID_Canton], [ID_Distrito], [Direccion_Exacta]) VALUES (3, 4, 401, 40102, N'Nueva Dirección')
INSERT [dbo].[Direcciones] ([ID_Direccion], [ID_Provincia], [ID_Canton], [ID_Distrito], [Direccion_Exacta]) VALUES (4, 4, 408, 40802, N'500 metros oeste')
INSERT [dbo].[Direcciones] ([ID_Direccion], [ID_Provincia], [ID_Canton], [ID_Distrito], [Direccion_Exacta]) VALUES (5, 2, 203, 20302, N'1234 Elm Street, Apt 5678, Unit #910, Pleasantville, Stateville, 12345, Country gletogpfgtekmnbvhjgtlt')
INSERT [dbo].[Direcciones] ([ID_Direccion], [ID_Provincia], [ID_Canton], [ID_Distrito], [Direccion_Exacta]) VALUES (6, 6, 606, 60602, N'Centro de Quepos, 300m Este de la plaza de deportes')
INSERT [dbo].[Direcciones] ([ID_Direccion], [ID_Provincia], [ID_Canton], [ID_Distrito], [Direccion_Exacta]) VALUES (10, 4, 404, 40401, N'300 metros norte de la plaza')
SET IDENTITY_INSERT [dbo].[Direcciones] OFF
GO
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 101, 10101, N'Carmen')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 101, 10102, N'Merced')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 101, 10103, N'Hospital')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 101, 10104, N'Catedral')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 101, 10105, N'Zapote')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 101, 10106, N'San Francisco de Dos Ríos')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 101, 10107, N'Uruca')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 101, 10108, N'Mata Redonda')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 101, 10109, N'Pavas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 101, 10110, N'Hatillo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 101, 10111, N'San Sebastián')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 102, 10201, N'Escazú')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 102, 10202, N'San Antonio')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 102, 10203, N'San Rafael')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 103, 10301, N'Desamparados')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 103, 10302, N'San Miguel')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 103, 10303, N'San Juan de Dios')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 103, 10304, N'San Rafael Arriba')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 103, 10305, N'San Antonio')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 103, 10306, N'Frailes')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 103, 10307, N'Patarra')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 103, 10308, N'San Cristobal')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 103, 10309, N'Rosario')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 103, 10310, N'Damas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 103, 10311, N'San Rafael Abajo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 103, 10312, N'Gravilias')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 103, 10313, N'Los Guido')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 104, 10401, N'Santiago')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 104, 10402, N'Mercedes Sur')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 104, 10403, N'Barbacoas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 104, 10404, N'Grifo Alto')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 104, 10405, N'San Rafael')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 104, 10406, N'Candelarita')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 104, 10407, N'Desamparaditos')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 104, 10408, N'San Antonio')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 104, 10409, N'Chires')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 105, 10501, N'San Marcos')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 105, 10502, N'San Lorenzo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 105, 10503, N'San Carlos')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 106, 10601, N'Aserrí')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 106, 10602, N'Tarbaca')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 106, 10603, N'Vuelta de Jorco')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 106, 10604, N'San Gabriel')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 106, 10605, N'Legua')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 106, 10606, N'Monterrey')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 106, 10607, N'Salitrillos')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 107, 10701, N'Colón')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 107, 10702, N'Guayabo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 107, 10703, N'Tabarcia')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 107, 10704, N'Piedras Negras')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 107, 10705, N'Picagres')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 107, 10706, N'Jaris')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 107, 10707, N'Quitirrisí')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 108, 10801, N'Guadalupe')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 108, 10802, N'San Francisco')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 108, 10803, N'Calle Blancos')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 108, 10804, N'Mata de Plátano')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 108, 10805, N'Ipis')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 108, 10806, N'Rancho Redondo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 108, 10807, N'Purral')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 109, 10901, N'Santa Ana')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 109, 10902, N'Salitral')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 109, 10903, N'Pozos')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 109, 10904, N'Uruca')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 109, 10905, N'Piedades')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 109, 10906, N'Brasil')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 110, 11001, N'Alajuelita')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 110, 11002, N'San Josecito')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 110, 11003, N'San Antonio')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 110, 11004, N'Concepción')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 110, 11005, N'San Felipe')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 111, 11101, N'San Isidro')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 111, 11102, N'San Rafael')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 111, 11103, N'Dulce Nombre de Jesús')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 111, 11104, N'Patalillo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 111, 11105, N'Cascajal')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 112, 11201, N'San Ignacio')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 112, 11202, N'Guaitil')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 112, 11203, N'Palmichal')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 112, 11204, N'Cangrejal')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 112, 11205, N'Sabanillas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 113, 11301, N'San Juan')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 113, 11302, N'Cinco Esquinas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 113, 11303, N'Anselmo Llorente')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 113, 11304, N'León XIII')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 113, 11305, N'Colima')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 114, 11401, N'San Vicente')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 114, 11402, N'San Jerónimo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 114, 11403, N'La Trinidad')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 115, 11501, N'San Pedro')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 115, 11502, N'Sabanilla')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 115, 11503, N'Mercedes')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 115, 11504, N'San Rafael')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 116, 11601, N'San Pablo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 116, 11602, N'San Pedro')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 116, 11603, N'San Juan de Mata')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 116, 11604, N'San Luis')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 116, 11605, N'Carara')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 117, 11701, N'Santa María')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 117, 11702, N'Jardín')
GO
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 117, 11703, N'Copey')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 118, 11801, N'Curridabat')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 118, 11802, N'Granadilla')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 118, 11803, N'Sánchez')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 118, 11804, N'Tirrases')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 119, 11901, N'San Isidro de El General')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 119, 11902, N'El General')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 119, 11903, N'Daniel Flores')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 119, 11904, N'Rivas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 119, 11905, N'San Pedro')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 119, 11906, N'Platanares')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 119, 11907, N'Pejibaye')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 119, 11908, N'Cajón')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 119, 11909, N'Barú')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 119, 11910, N'Río Nuevo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 119, 11911, N'Paramo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 119, 11912, N'La Amistad')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 120, 12001, N'San Pablo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 120, 12002, N'San Andrés')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 120, 12003, N'Llano Bonito')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 120, 12004, N'San Isidro')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 120, 12005, N'Santa Cruz')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (1, 120, 12006, N'San Antonio')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 201, 20101, N'Alajuela')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 201, 20102, N'San José')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 201, 20103, N'Carrizal')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 201, 20104, N'San Antonio')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 201, 20105, N'Guácima')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 201, 20106, N'San Isidro')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 201, 20107, N'Sabanilla')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 201, 20108, N'San Rafael')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 201, 20109, N'Río Segundo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 201, 20110, N'Desamparados')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 201, 20111, N'Turrucares')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 201, 20112, N'Tambor')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 201, 20113, N'Garita')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 201, 20114, N'Sarapiquí')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 202, 20201, N'San Ramón')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 202, 20202, N'Santiago')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 202, 20203, N'San Juan')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 202, 20204, N'Piedades Norte')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 202, 20205, N'Piedades Sur')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 202, 20206, N'San Rafael')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 202, 20207, N'San Isidro')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 202, 20208, N'Ángeles')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 202, 20209, N'Alfaro')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 202, 20210, N'Volio')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 202, 20211, N'Concepción')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 202, 20212, N'Zapotal')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 202, 20213, N'Peñas Blancas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 202, 20214, N'San Lorenzo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 203, 20301, N'Grecia')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 203, 20302, N'San Isidro')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 203, 20303, N'San José')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 203, 20304, N'San Roque')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 203, 20305, N'Tacares')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 203, 20307, N'Puente de Piedra')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 203, 20308, N'Bolivar')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 204, 20401, N'San Mateo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 204, 20402, N'Desmonte')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 204, 20403, N'Jesús María')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 204, 20404, N'Labrador')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 205, 20501, N'Atenas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 205, 20502, N'Jesús')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 205, 20503, N'Mercedes')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 205, 20504, N'San Isidro')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 205, 20505, N'Concepción')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 205, 20506, N'San José')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 205, 20507, N'Santa Eulalia')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 205, 20508, N'Escobal')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 206, 20601, N'Naranjo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 206, 20602, N'San Miguel')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 206, 20603, N'San José')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 206, 20604, N'Cirrí Sur')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 206, 20605, N'San Jerónimo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 206, 20606, N'San Juan')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 206, 20607, N'El Rosario')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 206, 20608, N'Palmitos')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 207, 20701, N'Palmares')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 207, 20702, N'Zaragoza')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 207, 20703, N'Buenos Aires')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 207, 20704, N'Santiago')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 207, 20705, N'Candelaria')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 207, 20706, N'Esquipulas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 207, 20707, N'La Granja')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 208, 20801, N'San Pedro')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 208, 20802, N'San Juan')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 208, 20803, N'San Rafael')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 208, 20804, N'Carrillos')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 208, 20805, N'Sabana Redonda')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 209, 20901, N'Orotina')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 209, 20902, N'El Mastate')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 209, 20903, N'Hacienda Vieja')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 209, 20904, N'Coyolar')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 209, 20905, N'La Ceiba')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 210, 21001, N'Quesada')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 210, 21002, N'Florencia')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 210, 21003, N'Buenavista')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 210, 21004, N'Aguas Zarcas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 210, 21005, N'Venecia')
GO
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 210, 21006, N'Pital')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 210, 21007, N'La Fortuna')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 210, 21008, N'La Tigra')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 210, 21009, N'La Palmera')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 210, 21010, N'Venado')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 210, 21011, N'Cutris')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 210, 21012, N'Monterrey')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 210, 21013, N'Pocosol')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 211, 21101, N'Zarcero')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 211, 21102, N'Laguna')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 211, 21103, N'Tapesco')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 211, 21104, N'Guadalupe')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 211, 21105, N'Palmira')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 211, 21106, N'Zapote')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 211, 21107, N'Brisas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 212, 21201, N'Sarchí Norte')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 212, 21202, N'Sarchí Sur')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 212, 21203, N'Toro Amarillo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 212, 21204, N'San Pedro')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 212, 21205, N'Rodríguez')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 213, 21301, N'Upala')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 213, 21302, N'Aguas Claras')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 213, 21303, N'San José O Pizote')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 213, 21304, N'Bijagua')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 213, 21305, N'Delicias')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 213, 21306, N'Dos Ríos')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 213, 21307, N'Yolillal')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 213, 21308, N'Canalete')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 214, 21401, N'Los Chiles')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 214, 21402, N'Caño Negro')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 214, 21403, N'El Amparo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 214, 21404, N'San Jorge')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 215, 21501, N'San Rafael')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 215, 21502, N'Buenavista')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 215, 21503, N'Cote')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 215, 21504, N'Katira')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 216, 21601, N'Río Cuarto')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 216, 21602, N'Santa Rita')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (2, 216, 21603, N'Santa Isabel')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 301, 30101, N'Oriental')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 301, 30102, N'Occidental')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 301, 30103, N'Carmen')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 301, 30104, N'San Nicolás')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 301, 30105, N'Aguacaliente o San Francisco')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 301, 30106, N'Guadalupe o Arenilla')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 301, 30107, N'Corralillo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 301, 30108, N'Tierra Blanca')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 301, 30109, N'Dulce Nombre')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 301, 30110, N'Llano Grande')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 301, 30111, N'Quebradilla')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 302, 30201, N'Paraíso')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 302, 30202, N'Santiago')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 302, 30203, N'Orosi')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 302, 30204, N'Cachí')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 302, 30205, N'Llanos de Santa Lucía')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 302, 30206, N'Birrisito')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 303, 30301, N'Tres Ríos')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 303, 30302, N'San Diego')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 303, 30303, N'San Juan')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 303, 30304, N'San Rafael')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 303, 30305, N'Concepción')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 303, 30306, N'Dulce Nombre')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 303, 30307, N'San Ramón')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 303, 30308, N'Río Azul')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 304, 30401, N'Juan Viñas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 304, 30402, N'Tucurrique')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 304, 30403, N'Pejibaye')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 305, 30501, N'Turrialba')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 305, 30502, N'La Suiza')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 305, 30503, N'Peralta')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 305, 30504, N'Santa Cruz')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 305, 30505, N'Santa Teresita')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 305, 30506, N'Pavones')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 305, 30507, N'Tuis')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 305, 30508, N'Tayutic')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 305, 30509, N'Santa Rosa')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 305, 30510, N'Tres Equis')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 305, 30511, N'La Isabel')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 305, 30512, N'Chirripó')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 306, 30601, N'Pacayas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 306, 30602, N'Cervantes')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 306, 30603, N'Capellades')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 307, 30701, N'San Rafael')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 307, 30702, N'Cot')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 307, 30703, N'Potrero Cerrado')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 307, 30704, N'Cipreses')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 307, 30705, N'Santa Rosa')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 308, 30801, N'El Tejar')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 308, 30802, N'San Isidro')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 308, 30803, N'Tobosi')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (3, 308, 30804, N'Patio de Agua')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 401, 40101, N'Heredia')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 401, 40102, N'Mercedes')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 401, 40103, N'San Francisco')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 401, 40104, N'Ulloa')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 401, 40105, N'Varablanca')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 402, 40201, N'Barva')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 402, 40202, N'San Pedro')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 402, 40203, N'San Pablo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 402, 40204, N'San Roque')
GO
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 402, 40205, N'Santa Lucía')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 402, 40206, N'San José de la Montaña')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 403, 40301, N'Santo Domingo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 403, 40302, N'San Vicente')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 403, 40303, N'San Miguel')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 403, 40304, N'Paracito')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 403, 40305, N'Santo Tomás')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 403, 40306, N'Santa Rosa')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 403, 40307, N'Tures')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 403, 40308, N'Pará')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 404, 40401, N'Santa Bárbara')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 404, 40402, N'San Pedro')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 404, 40403, N'San Juan')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 404, 40404, N'Jesús')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 404, 40405, N'Santo Domingo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 404, 40406, N'Purabá')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 405, 40501, N'San Rafael')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 405, 40502, N'San Josecito')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 405, 40503, N'Santiago')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 405, 40504, N'Ángeles')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 405, 40505, N'Concepción')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 406, 40601, N'San Isidro')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 406, 40602, N'San José')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 406, 40603, N'Concepción')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 406, 40604, N'San Francisco')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 407, 40701, N'San Antonio')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 407, 40702, N'La Ribera')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 407, 40703, N'La Asunción')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 408, 40801, N'San Joaquín')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 408, 40802, N'Barrantes')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 408, 40803, N'Llorente')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 409, 40901, N'San Pablo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 409, 40902, N'Rincón de Sabanilla')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 410, 41001, N'Puerto Viejo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 410, 41002, N'La Virgen')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 410, 41003, N'Las Horquetas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 410, 41004, N'Llanuras del Gaspar')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (4, 410, 41005, N'Cureña')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 501, 50101, N'Liberia')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 501, 50102, N'Cañas Dulces')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 501, 50103, N'Mayorga')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 501, 50104, N'Nacascolo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 501, 50105, N'Curubandé')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 502, 50201, N'Nicoya')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 502, 50202, N'Mansión')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 502, 50203, N'San Antonio')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 502, 50204, N'Quebrada Honda')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 502, 50205, N'Sámara')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 502, 50206, N'Nosara')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 502, 50207, N'Belén de Nosarita')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 503, 50301, N'Santa Cruz')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 503, 50302, N'Bolsón')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 503, 50303, N'Veintisiete de Abril')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 503, 50304, N'Tempate')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 503, 50305, N'Cartagena')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 503, 50306, N'Cuajiniquil')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 503, 50307, N'Diriá')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 503, 50308, N'Cabo Velas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 503, 50309, N'Tamarindo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 504, 50401, N'Bagaces')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 504, 50402, N'La Fortuna')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 504, 50403, N'Mogote')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 504, 50404, N'Río Naranjo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 505, 50501, N'Filadelfia')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 505, 50502, N'Palmira')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 505, 50503, N'Sardinal')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 505, 50504, N'Belén')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 506, 50601, N'Cañas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 506, 50602, N'Palmira')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 506, 50603, N'San Miguel')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 506, 50604, N'Bebedero')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 506, 50605, N'Porozal')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 507, 50701, N'Las Juntas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 507, 50702, N'Sierra')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 507, 50703, N'San Juan')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 507, 50704, N'Colorado')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 508, 50801, N'Tilarán')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 508, 50802, N'Quebrada Grande')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 508, 50803, N'Tronadora')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 508, 50804, N'Santa Rosa')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 508, 50805, N'Líbano')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 508, 50806, N'Tierras Morenas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 508, 50807, N'Arenal')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 508, 50808, N'Cabeceras')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 509, 50901, N'Carmona')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 509, 50902, N'Santa Rita')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 509, 50903, N'Zapotal')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 509, 50904, N'San Pablo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 509, 50905, N'Porvenir')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 509, 50906, N'Bejuco')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 510, 51001, N'La Cruz')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 510, 51002, N'Santa Cecilia')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 510, 51003, N'La Garita')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 510, 51004, N'Santa Elena')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 511, 51101, N'Hojancha')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 511, 51102, N'Monte Romo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 511, 51103, N'Puerto Carrillo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 511, 51104, N'Huacas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (5, 511, 51105, N'Matambú')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 601, 60101, N'Puntarenas')
GO
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 601, 60102, N'Pitahaya')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 601, 60103, N'Chomes')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 601, 60104, N'Lepanto')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 601, 60105, N'Paquera')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 601, 60106, N'Manzanillo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 601, 60107, N'Guacimal')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 601, 60108, N'Barranca')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 601, 60110, N'Isla del Coco')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 601, 60111, N'Cóbano')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 601, 60112, N'Chacarita')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 601, 60113, N'Chira')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 601, 60114, N'Acapulco')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 601, 60115, N'El Roble')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 601, 60116, N'Arancibia')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 602, 60201, N'Espíritu Santo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 602, 60202, N'San Juan Grande')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 602, 60203, N'Macacona')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 602, 60204, N'San Rafael')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 602, 60205, N'San Jerónimo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 602, 60206, N'Caldera')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 603, 60301, N'Buenos Aires')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 603, 60302, N'Volcán')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 603, 60303, N'Potrero Grande')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 603, 60304, N'Boruca')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 603, 60305, N'Pilas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 603, 60306, N'Colinas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 603, 60307, N'Chánguena')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 603, 60308, N'Biolley')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 603, 60309, N'Brunka')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 604, 60401, N'Miramar')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 604, 60402, N'La Unión')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 604, 60403, N'San Isidro')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 605, 60501, N'Puerto Cortés')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 605, 60502, N'Palmar')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 605, 60503, N'Sierpe')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 605, 60504, N'Bahía Ballena')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 605, 60505, N'Piedras Blancas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 605, 60506, N'Bahía Drake')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 606, 60601, N'Quepos')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 606, 60602, N'Savegre')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 606, 60603, N'Naranjito')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 607, 60701, N'Golfito')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 607, 60702, N'Puerto Jiménez')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 607, 60703, N'Guaycará')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 607, 60704, N'Pavón')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 608, 60801, N'San Vito')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 608, 60802, N'Sabalito')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 608, 60803, N'Aguabuena')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 608, 60804, N'Limoncito')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 608, 60805, N'Pittier')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 608, 60806, N'Gutiérrez Braun')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 609, 60901, N'Parrita')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 610, 61001, N'Corredor')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 610, 61002, N'La Cuesta')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 610, 61003, N'Canoas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 610, 61004, N'Laurel')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 611, 61101, N'Jacó')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 611, 61102, N'Tárcoles')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 611, 61103, N'Lagunillas')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (6, 612, 61201, N'Monteverde')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 701, 70101, N'Limón')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 701, 70102, N'Valle La Estrella')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 701, 70103, N'Río Blanco')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 701, 70104, N'Matama')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 702, 70201, N'Guápiles')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 702, 70202, N'Jiménez')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 702, 70203, N'Rita')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 702, 70204, N'Roxana')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 702, 70205, N'Cariari')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 702, 70206, N'Colorado')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 702, 70207, N'La Colonia')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 703, 70301, N'Siquirres')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 703, 70302, N'Pacuarito')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 703, 70303, N'Florida')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 703, 70304, N'Germania')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 703, 70305, N'El Cairo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 703, 70306, N'Alegría')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 703, 70307, N'Reventazón')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 704, 70401, N'Bratsi')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 704, 70402, N'Sixaola')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 704, 70403, N'Cahuita')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 704, 70404, N'Telire')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 705, 70501, N'Matina')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 705, 70502, N'Batán')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 705, 70503, N'Carrandí')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 706, 70601, N'Guácimo')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 706, 70602, N'Mercedes')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 706, 70603, N'Pocora')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 706, 70604, N'Río Jiménez')
INSERT [dbo].[Distrito] ([ID_Provincia], [ID_Canton], [ID_Distrito], [Nombre]) VALUES (7, 706, 70605, N'Duacarí')
GO
SET IDENTITY_INSERT [dbo].[Empresa] ON 

INSERT [dbo].[Empresa] ([ID_Empresa], [Nombre_empresa], [Descripcion], [Ubicacion]) VALUES (1, N'prueba', N'pruebaaa', N'heredia')
SET IDENTITY_INSERT [dbo].[Empresa] OFF
GO
INSERT [dbo].[Estado] ([ID_Estado], [Tipo_Estado]) VALUES (0, N'Inactivo')
INSERT [dbo].[Estado] ([ID_Estado], [Tipo_Estado]) VALUES (1, N'Activo')
GO
SET IDENTITY_INSERT [dbo].[Factura_Detalle] ON 

INSERT [dbo].[Factura_Detalle] ([ID_Detalle], [ID_Factura], [ID_Producto], [PrecioPagado], [CantidadPagado], [ImpuestoPagado]) VALUES (1, 1, 7, CAST(10000.00 AS Decimal(18, 2)), 1, CAST(1300.00 AS Decimal(18, 2)))
INSERT [dbo].[Factura_Detalle] ([ID_Detalle], [ID_Factura], [ID_Producto], [PrecioPagado], [CantidadPagado], [ImpuestoPagado]) VALUES (2, 2, 5, CAST(30000.00 AS Decimal(18, 2)), 1, CAST(3900.00 AS Decimal(18, 2)))
INSERT [dbo].[Factura_Detalle] ([ID_Detalle], [ID_Factura], [ID_Producto], [PrecioPagado], [CantidadPagado], [ImpuestoPagado]) VALUES (3, 3, 7, CAST(10000.00 AS Decimal(18, 2)), 2, CAST(1300.00 AS Decimal(18, 2)))
INSERT [dbo].[Factura_Detalle] ([ID_Detalle], [ID_Factura], [ID_Producto], [PrecioPagado], [CantidadPagado], [ImpuestoPagado]) VALUES (4, 3, 5, CAST(30000.00 AS Decimal(18, 2)), 1, CAST(3900.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Factura_Detalle] OFF
GO
SET IDENTITY_INSERT [dbo].[Factura_Encabezado] ON 

INSERT [dbo].[Factura_Encabezado] ([ID_Factura], [ID_Usuario], [FechaCompra], [TotalCompra]) VALUES (1, 13, CAST(N'2024-03-20T21:40:20.950' AS DateTime), CAST(11300.00 AS Decimal(18, 2)))
INSERT [dbo].[Factura_Encabezado] ([ID_Factura], [ID_Usuario], [FechaCompra], [TotalCompra]) VALUES (2, 13, CAST(N'2024-03-20T23:29:57.430' AS DateTime), CAST(33900.00 AS Decimal(18, 2)))
INSERT [dbo].[Factura_Encabezado] ([ID_Factura], [ID_Usuario], [FechaCompra], [TotalCompra]) VALUES (3, 13, CAST(N'2024-03-21T00:29:38.340' AS DateTime), CAST(56500.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Factura_Encabezado] OFF
GO
SET IDENTITY_INSERT [dbo].[Identificacion] ON 

INSERT [dbo].[Identificacion] ([ID_Identificacion], [Nombre]) VALUES (1, N'Cédula Física')
INSERT [dbo].[Identificacion] ([ID_Identificacion], [Nombre]) VALUES (2, N'Cédula Jurídica')
INSERT [dbo].[Identificacion] ([ID_Identificacion], [Nombre]) VALUES (3, N'Pasaporte')
SET IDENTITY_INSERT [dbo].[Identificacion] OFF
GO
SET IDENTITY_INSERT [dbo].[Pedidos] ON 

INSERT [dbo].[Pedidos] ([ID_Pedido], [ID_Cliente], [ID_Transaccion], [ID_Factura], [Estado]) VALUES (1, 13, N'04J861002M313894B', 1, 0)
INSERT [dbo].[Pedidos] ([ID_Pedido], [ID_Cliente], [ID_Transaccion], [ID_Factura], [Estado]) VALUES (2, 13, N'3P273575BC644260H', 2, 0)
INSERT [dbo].[Pedidos] ([ID_Pedido], [ID_Cliente], [ID_Transaccion], [ID_Factura], [Estado]) VALUES (3, 13, N'2AS31170D5433622T', 3, 0)
SET IDENTITY_INSERT [dbo].[Pedidos] OFF
GO
SET IDENTITY_INSERT [dbo].[Producto] ON 

INSERT [dbo].[Producto] ([ID_Producto], [ID_Categoria], [Nombre], [Descripcion], [Cantidad], [Precio], [SKU], [Imagen], [Estado]) VALUES (3, 1, N'Lentes Estilo 5', N'Lentes cuadrados negros para hombre', 16, CAST(50000.00 AS Decimal(18, 2)), N'nulo', N'/Images/3.png', 1)
INSERT [dbo].[Producto] ([ID_Producto], [ID_Categoria], [Nombre], [Descripcion], [Cantidad], [Precio], [SKU], [Imagen], [Estado]) VALUES (5, 2, N'Lentes MAP', N'Lentes redondos mujer', 4, CAST(30000.00 AS Decimal(18, 2)), N'nulo', N'/Images/5.png', 1)
INSERT [dbo].[Producto] ([ID_Producto], [ID_Categoria], [Nombre], [Descripcion], [Cantidad], [Precio], [SKU], [Imagen], [Estado]) VALUES (6, 2, N'Lentes Gucci', N'Lentes cuadrados y grandes', 2, CAST(52000.00 AS Decimal(18, 2)), N'nulo', N'/Images/6.png', 1)
INSERT [dbo].[Producto] ([ID_Producto], [ID_Categoria], [Nombre], [Descripcion], [Cantidad], [Precio], [SKU], [Imagen], [Estado]) VALUES (7, 3, N'Lentes Coloridos', N'Lentes especiales para ninos', 19, CAST(10000.00 AS Decimal(18, 2)), N'nulo', N'/Images/7.png', 1)
INSERT [dbo].[Producto] ([ID_Producto], [ID_Categoria], [Nombre], [Descripcion], [Cantidad], [Precio], [SKU], [Imagen], [Estado]) VALUES (8, 2, N'Ray Ban', N'Estilo elegante y simple', 48, CAST(35000.00 AS Decimal(18, 2)), N'nulo', N'/Images/8.png', 1)
SET IDENTITY_INSERT [dbo].[Producto] OFF
GO
SET IDENTITY_INSERT [dbo].[Proveedores] ON 

INSERT [dbo].[Proveedores] ([ID_Proveedor], [ID_Identificacion], [Nombre_Proveedor], [Apellido_Proveedor], [Cedula_Proveedor], [Direccion_Exacta], [Estado_Proveedor], [Empresa], [Telefono], [Correo]) VALUES (1, 1, N'Carlos', N'Viquez', N'102580075', N'sddddsl,sdl,ddddddddddddddddddd dddskmksmdkdmslls,md', 1, 1, N'88775544', N'prueba@gmail.com')
SET IDENTITY_INSERT [dbo].[Proveedores] OFF
GO
SET IDENTITY_INSERT [dbo].[Provincia] ON 

INSERT [dbo].[Provincia] ([ID_Provincia], [Nombre]) VALUES (1, N'San José')
INSERT [dbo].[Provincia] ([ID_Provincia], [Nombre]) VALUES (2, N'Alajuela')
INSERT [dbo].[Provincia] ([ID_Provincia], [Nombre]) VALUES (3, N'Cartago')
INSERT [dbo].[Provincia] ([ID_Provincia], [Nombre]) VALUES (4, N'Heredia')
INSERT [dbo].[Provincia] ([ID_Provincia], [Nombre]) VALUES (5, N'Guanacaste')
INSERT [dbo].[Provincia] ([ID_Provincia], [Nombre]) VALUES (6, N'Puntarenas')
INSERT [dbo].[Provincia] ([ID_Provincia], [Nombre]) VALUES (7, N'Limón')
SET IDENTITY_INSERT [dbo].[Provincia] OFF
GO
INSERT [dbo].[Roles] ([ID_Rol], [Nombre_Rol]) VALUES (1, N'Administrador')
INSERT [dbo].[Roles] ([ID_Rol], [Nombre_Rol]) VALUES (2, N'Usuario')
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([ID_Usuario], [ID_Identificacion], [Identificacion_Usuario], [Nombre_Usuario], [Apellido_Usuario], [Correo_Usuario], [Contrasenna_Usuario], [ID_Direccion], [Telefono_Usuario], [ID_Estado], [ID_Rol], [C_esTemporal]) VALUES (12, 1, N'402590056', N'Angélica', N'Valerín', N'angelicavalerin13@gmail.com', N'a!123456789', NULL, N'', 1, 1, 0)
INSERT [dbo].[Usuario] ([ID_Usuario], [ID_Identificacion], [Identificacion_Usuario], [Nombre_Usuario], [Apellido_Usuario], [Correo_Usuario], [Contrasenna_Usuario], [ID_Direccion], [Telefono_Usuario], [ID_Estado], [ID_Rol], [C_esTemporal]) VALUES (13, 1, N'101110111', N'Angélica', N'Viquez', N'angelica_valerin@hotmail.com', N'a!123456', 10, N'85300459', 1, 2, 0)
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UK_Identificacion_Usuario]    Script Date: 3/21/2024 12:32:14 AM ******/
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [UK_Identificacion_Usuario] UNIQUE NONCLUSTERED 
(
	[Identificacion_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Canton]  WITH CHECK ADD  CONSTRAINT [FK_Canton_Provincia] FOREIGN KEY([ID_Provincia])
REFERENCES [dbo].[Provincia] ([ID_Provincia])
GO
ALTER TABLE [dbo].[Canton] CHECK CONSTRAINT [FK_Canton_Provincia]
GO
ALTER TABLE [dbo].[Carrito]  WITH CHECK ADD  CONSTRAINT [FK_Carrito_Producto] FOREIGN KEY([ID_Producto])
REFERENCES [dbo].[Producto] ([ID_Producto])
GO
ALTER TABLE [dbo].[Carrito] CHECK CONSTRAINT [FK_Carrito_Producto]
GO
ALTER TABLE [dbo].[Carrito]  WITH CHECK ADD  CONSTRAINT [FK_Carrito_Usuario] FOREIGN KEY([ID_Usuario])
REFERENCES [dbo].[Usuario] ([ID_Usuario])
GO
ALTER TABLE [dbo].[Carrito] CHECK CONSTRAINT [FK_Carrito_Usuario]
GO
ALTER TABLE [dbo].[Categorias]  WITH CHECK ADD  CONSTRAINT [FK_Estado_Categoria] FOREIGN KEY([Estado_Categoria])
REFERENCES [dbo].[Estado] ([ID_Estado])
GO
ALTER TABLE [dbo].[Categorias] CHECK CONSTRAINT [FK_Estado_Categoria]
GO
ALTER TABLE [dbo].[compras]  WITH CHECK ADD  CONSTRAINT [FK_compras_Empresa] FOREIGN KEY([Empresa])
REFERENCES [dbo].[Empresa] ([ID_Empresa])
GO
ALTER TABLE [dbo].[compras] CHECK CONSTRAINT [FK_compras_Empresa]
GO
ALTER TABLE [dbo].[Direcciones]  WITH CHECK ADD  CONSTRAINT [FK_Direccion_Canton] FOREIGN KEY([ID_Canton])
REFERENCES [dbo].[Canton] ([ID_Canton])
GO
ALTER TABLE [dbo].[Direcciones] CHECK CONSTRAINT [FK_Direccion_Canton]
GO
ALTER TABLE [dbo].[Direcciones]  WITH CHECK ADD  CONSTRAINT [FK_Direccion_Distrito] FOREIGN KEY([ID_Distrito])
REFERENCES [dbo].[Distrito] ([ID_Distrito])
GO
ALTER TABLE [dbo].[Direcciones] CHECK CONSTRAINT [FK_Direccion_Distrito]
GO
ALTER TABLE [dbo].[Direcciones]  WITH CHECK ADD  CONSTRAINT [FK_Direccion_Provincia] FOREIGN KEY([ID_Provincia])
REFERENCES [dbo].[Provincia] ([ID_Provincia])
GO
ALTER TABLE [dbo].[Direcciones] CHECK CONSTRAINT [FK_Direccion_Provincia]
GO
ALTER TABLE [dbo].[Distrito]  WITH CHECK ADD  CONSTRAINT [FK_Distrito_Canton] FOREIGN KEY([ID_Canton])
REFERENCES [dbo].[Canton] ([ID_Canton])
GO
ALTER TABLE [dbo].[Distrito] CHECK CONSTRAINT [FK_Distrito_Canton]
GO
ALTER TABLE [dbo].[Distrito]  WITH CHECK ADD  CONSTRAINT [FK_Distrito_Provincia] FOREIGN KEY([ID_Provincia])
REFERENCES [dbo].[Provincia] ([ID_Provincia])
GO
ALTER TABLE [dbo].[Distrito] CHECK CONSTRAINT [FK_Distrito_Provincia]
GO
ALTER TABLE [dbo].[Factura_Detalle]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Detalle_Factura_Encabezado] FOREIGN KEY([ID_Factura])
REFERENCES [dbo].[Factura_Encabezado] ([ID_Factura])
GO
ALTER TABLE [dbo].[Factura_Detalle] CHECK CONSTRAINT [FK_Factura_Detalle_Factura_Encabezado]
GO
ALTER TABLE [dbo].[Factura_Detalle]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Detalle_Producto] FOREIGN KEY([ID_Producto])
REFERENCES [dbo].[Producto] ([ID_Producto])
GO
ALTER TABLE [dbo].[Factura_Detalle] CHECK CONSTRAINT [FK_Factura_Detalle_Producto]
GO
ALTER TABLE [dbo].[Factura_Encabezado]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Encabezado_Usuario] FOREIGN KEY([ID_Usuario])
REFERENCES [dbo].[Usuario] ([ID_Usuario])
GO
ALTER TABLE [dbo].[Factura_Encabezado] CHECK CONSTRAINT [FK_Factura_Encabezado_Usuario]
GO
ALTER TABLE [dbo].[Pedidos]  WITH CHECK ADD  CONSTRAINT [FK_Pedidos_Factura] FOREIGN KEY([ID_Factura])
REFERENCES [dbo].[Factura_Encabezado] ([ID_Factura])
GO
ALTER TABLE [dbo].[Pedidos] CHECK CONSTRAINT [FK_Pedidos_Factura]
GO
ALTER TABLE [dbo].[Pedidos]  WITH CHECK ADD  CONSTRAINT [FK_Pedidos_Usuario] FOREIGN KEY([ID_Cliente])
REFERENCES [dbo].[Usuario] ([ID_Usuario])
GO
ALTER TABLE [dbo].[Pedidos] CHECK CONSTRAINT [FK_Pedidos_Usuario]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Categoria] FOREIGN KEY([ID_Categoria])
REFERENCES [dbo].[Categorias] ([ID_Categoria])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Categoria]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Estado] FOREIGN KEY([Estado])
REFERENCES [dbo].[Estado] ([ID_Estado])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Estado]
GO
ALTER TABLE [dbo].[Proveedores]  WITH CHECK ADD  CONSTRAINT [FK_Proveedores_Empresa] FOREIGN KEY([Empresa])
REFERENCES [dbo].[Empresa] ([ID_Empresa])
GO
ALTER TABLE [dbo].[Proveedores] CHECK CONSTRAINT [FK_Proveedores_Empresa]
GO
ALTER TABLE [dbo].[Proveedores]  WITH CHECK ADD  CONSTRAINT [FK_Proveedores_Estado] FOREIGN KEY([Estado_Proveedor])
REFERENCES [dbo].[Estado] ([ID_Estado])
GO
ALTER TABLE [dbo].[Proveedores] CHECK CONSTRAINT [FK_Proveedores_Estado]
GO
ALTER TABLE [dbo].[Proveedores]  WITH CHECK ADD  CONSTRAINT [FK_Proveedores_Identificacion] FOREIGN KEY([ID_Identificacion])
REFERENCES [dbo].[Identificacion] ([ID_Identificacion])
GO
ALTER TABLE [dbo].[Proveedores] CHECK CONSTRAINT [FK_Proveedores_Identificacion]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Direccion] FOREIGN KEY([ID_Direccion])
REFERENCES [dbo].[Direcciones] ([ID_Direccion])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Direccion]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Estado] FOREIGN KEY([ID_Estado])
REFERENCES [dbo].[Estado] ([ID_Estado])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Estado]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Identificacion] FOREIGN KEY([ID_Identificacion])
REFERENCES [dbo].[Identificacion] ([ID_Identificacion])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Identificacion]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY([ID_Rol])
REFERENCES [dbo].[Roles] ([ID_Rol])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Rol]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarCategoriaSP]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ActualizarCategoriaSP]
    @ID_Categoria int,
    @Nombre_Categoria varchar(255)
AS
BEGIN
    UPDATE dbo.Categorias
    SET
        Nombre_Categoria = @Nombre_Categoria
    WHERE ID_Categoria = @ID_Categoria;
END;
GO
/****** Object:  StoredProcedure [dbo].[ActualizarCompra]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ActualizarCompra]
    @IdCompra INT,
    @Empresa bigint,
    @Fecha DATETIME,
    @concepto NVARCHAR(255),
    @Cantidad INT,
    @Total DECIMAL(10, 2)
AS
BEGIN
    UPDATE compras
    SET 
        Empresa = @Empresa,
        fecha = @Fecha,
        concepto = @concepto,
        cantidad = @Cantidad,
        total = @Total
    WHERE id_compras = @IdCompra;
END;
GO
/****** Object:  StoredProcedure [dbo].[ActualizarCuentaUsuarioSP]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarCuentaUsuarioSP]
	@ID BIGINT,
    @Nombre VARCHAR(60),
	@Apellido VARCHAR(70),
    @Correo VARCHAR(100),
	@NuevaContrasenna VARCHAR(50),
	@Telefono VARCHAR(20),
	@Prov INT,
	@Canton INT,
	@Distrito INT,
	@Direccion VARCHAR(500)
AS
BEGIN
    DECLARE @NuevaDireccionID INT

    -- Verificar si ID_Direccion es nulo
	IF (SELECT ID_Direccion FROM dbo.Usuario WHERE ID_Usuario = @ID) IS NULL
	BEGIN
		-- Insertar un nuevo registro en la tabla Direcciones
		INSERT INTO dbo.Direcciones(ID_Provincia, ID_Canton, ID_Distrito, Direccion_Exacta)
		VALUES (@Prov, @Canton, @Distrito, @Direccion);

		-- Obtener el ID de la dirección recién insertada
		SET @NuevaDireccionID = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		-- Obtener el ID de la dirección existente
		SET @NuevaDireccionID = (SELECT ID_Direccion FROM dbo.Usuario WHERE ID_Usuario = @ID);

		-- Actualizar la tabla Direcciones con los nuevos datos
		UPDATE dbo.Direcciones
		SET ID_Provincia = @Prov,
			ID_Canton = @Canton,
			ID_Distrito = @Distrito,
			Direccion_Exacta = @Direccion
		WHERE ID_Direccion = @NuevaDireccionID;
	END

    -- Actualizar la tabla Usuario con los nuevos valores
	UPDATE dbo.Usuario
	SET Nombre_Usuario = @Nombre,
		Apellido_Usuario = @Apellido,
		Correo_Usuario = @Correo,
		Contrasenna_Usuario = @NuevaContrasenna,
		ID_Direccion = @NuevaDireccionID,
		Telefono_Usuario = @Telefono
	WHERE ID_Usuario = @ID;
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarEmpresaSP]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ActualizarEmpresaSP]
    @ID_Empresa bigint,
    @Nombre_empresa varchar(255),
    @Descripcion varchar(255),
    @Ubicacion varchar(255)
AS
BEGIN
    UPDATE dbo.Empresa
    SET
        Nombre_empresa = @Nombre_empresa,
        Descripcion = @Descripcion,
        Ubicacion = @Ubicacion
    WHERE ID_Empresa = @ID_Empresa;
END;
GO
/****** Object:  StoredProcedure [dbo].[ActualizarEstadoCategoriaSP]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarEstadoCategoriaSP]
    @ID_Categoria BIGINT
AS
BEGIN
    UPDATE Categorias
    SET Estado_Categoria = (CASE WHEN Estado_Categoria = 1 THEN 0 ELSE 1 END)
    WHERE ID_Categoria = @ID_Categoria;
END;
GO
/****** Object:  StoredProcedure [dbo].[ActualizarEstadoProveedorSP]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ActualizarEstadoProveedorSP]
    @ID_Proveedor BIGINT
AS
BEGIN
    UPDATE Proveedores
    SET Estado_Proveedor = (CASE WHEN Estado_Proveedor = 1 THEN 0 ELSE 1 END)
    WHERE ID_Proveedor = @ID_Proveedor;
END;
GO
/****** Object:  StoredProcedure [dbo].[ActualizarEstadoUsuarioSP]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarEstadoUsuarioSP]
    @ID_Usuario BIGINT
AS
BEGIN
    UPDATE Usuario
    SET ID_Estado = (CASE WHEN ID_Estado = 1 THEN 0 ELSE 1 END)
    WHERE ID_Usuario = @ID_Usuario;
END;
GO
/****** Object:  StoredProcedure [dbo].[ActualizarProveedorSP]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarProveedorSP]
    @ID_Proveedor BIGINT,
    @Nombre_Proveedor VARCHAR(255),
    @Apellido_Proveedor VARCHAR(255),
    @Direccion_Exacta VARCHAR(255),
    @Empresa BIGINT,
    @Telefono VARCHAR(15),
    @Correo VARCHAR(100)
AS
BEGIN
    UPDATE dbo.Proveedores
    SET
        Nombre_Proveedor = @Nombre_Proveedor,
        Apellido_Proveedor = @Apellido_Proveedor,
        Direccion_Exacta = @Direccion_Exacta,
        Empresa = @Empresa,
        Telefono = @Telefono,
        Correo = @Correo
    WHERE ID_Proveedor = @ID_Proveedor;
END;
GO
/****** Object:  StoredProcedure [dbo].[ActualizarRolUsuarioSP]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarRolUsuarioSP]
    @ID_Usuario BIGINT
AS
BEGIN
    UPDATE Usuario
    SET ID_Rol = (CASE WHEN ID_Rol = 2 THEN 1 ELSE 2 END)
    WHERE ID_Usuario = @ID_Usuario;
END;
GO
/****** Object:  StoredProcedure [dbo].[EliminarCategoriaSP]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EliminarCategoriaSP]
    @ID_Categoria bigint
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.Categorias WHERE ID_Categoria = @ID_Categoria;
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarEmpresaSP]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EliminarEmpresaSP]
    @ID_Empresa bigint
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.Empresa WHERE ID_Empresa = @ID_Empresa;
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarProveedorSP]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EliminarProveedorSP]
    @ID_Proveedor bigint
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM dbo.Proveedores WHERE ID_Proveedor = @ID_Proveedor;
END
GO
/****** Object:  StoredProcedure [dbo].[InactivarUsuarioSP]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InactivarUsuarioSP]
	@CodigoUsuario	BIGINT
AS
BEGIN
	
	UPDATE	dbo.Usuario
	SET		ID_Estado=0
	WHERE	ID_Usuario = @CodigoUsuario

END;
GO
/****** Object:  StoredProcedure [dbo].[IniciarSesionSP]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IniciarSesionSP]
	@Correo         varchar(100),
    @Contrasenna    varchar(50)
AS
BEGIN
	
	SELECT ID_Usuario, ID_Identificacion, Identificacion_Usuario, Nombre_Usuario, Apellido_Usuario, Correo_Usuario, Contrasenna_Usuario,ID_Direccion=0,Telefono_Usuario,ID_Estado,ID_Rol, C_esTemporal
	From Usuario
	  WHERE Correo_Usuario = @Correo
	  AND   Contrasenna_Usuario = @Contrasenna
	  AND	ID_Estado = 1
END
GO
/****** Object:  StoredProcedure [dbo].[PagarCarritoSP]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PagarCarritoSP]
	@ID_Usuario BIGINT
AS
BEGIN
	
	IF (SELECT	TOP 1 P.Cantidad - C.Cantidad 
		FROM	Producto	P
		INNER	JOIN	Carrito  C	ON P.ID_Producto = C.ID_Producto
		WHERE	ID_Usuario = @ID_Usuario) < 0
	BEGIN

		SELECT 'FALSE'
		
	END
	ELSE
	BEGIN

		DECLARE @TotalCompra DECIMAL(18,2)
		DECLARE @CodigoFactura BIGINT
	
		SELECT	@TotalCompra = SUM(P.Precio * C.Cantidad) + (SUM(P.Precio * C.Cantidad) * 0.13)
		FROM	Carrito C
		INNER	JOIN Producto P ON C.ID_Producto = P.ID_Producto
		WHERE	ID_Usuario = @ID_Usuario

		INSERT INTO dbo.Factura_Encabezado(ID_Usuario,FechaCompra,TotalCompra)
		VALUES (@ID_Usuario, GETDATE(), @TotalCompra)

		SET @CodigoFactura = @@IDENTITY

		INSERT INTO dbo.Factura_Detalle(ID_Factura,ID_Producto,PrecioPagado,CantidadPagado,ImpuestoPagado)
		SELECT	@CodigoFactura, C.ID_Producto, P.Precio, C.Cantidad, P.Precio * 0.13
		FROM	Carrito C
		INNER	JOIN Producto P ON C.ID_Producto = P.ID_Producto
		WHERE	ID_Usuario = @ID_Usuario

		UPDATE	P
		SET		P.Cantidad = P.Cantidad - C.Cantidad
		FROM	Producto P
		INNER	JOIN Carrito C ON C.ID_Producto = P.ID_Producto
		WHERE	ID_Usuario = @ID_Usuario

		DELETE FROM Carrito
		WHERE ID_Usuario = @ID_Usuario

		SELECT 'TRUE'

	END

END
GO
/****** Object:  StoredProcedure [dbo].[RecuperarCuentaUsuarioSP]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RecuperarCuentaUsuarioSP]
    @Correo varchar(100)
AS
BEGIN
    DECLARE @UsuarioExistente INT

    -- Verificar si el correo electrónico existe en la tabla Usuario
    SELECT @UsuarioExistente = COUNT(*)
    FROM dbo.Usuario
    WHERE Correo_Usuario = @Correo

    IF @UsuarioExistente > 0
    BEGIN
        -- Generar contraseña aleatoria que contenga entre 8 y 15 caracteres, al menos 1 número, 1 letra y 1 signo de exclamación
		DECLARE @NuevaContrasenna varchar(50)
        SET @NuevaContrasenna = (
            SELECT SUBSTRING(CONVERT(varchar(255), NEWID()), 1, 8)
            + CHAR(65 + (ABS(CHECKSUM(NEWID())) % 26)) -- al menos una letra mayúscula
            + CHAR(97 + (ABS(CHECKSUM(NEWID())) % 26)) -- al menos una letra minúscula
            + CHAR(33) -- signo de exclamación
            + CONVERT(varchar(2), ABS(CHECKSUM(NEWID())) % 10) -- al menos un número
        )

		 -- Truncar la contraseña si supera los 15 caracteres
        SET @NuevaContrasenna = LEFT(@NuevaContrasenna, 15)

        -- Actualizar la contraseña del usuario
        UPDATE dbo.Usuario
        SET Contrasenna_Usuario = @NuevaContrasenna,
		 C_esTemporal = 1
        WHERE Correo_Usuario = @Correo

        -- Seleccionar los datos del usuario actualizados
        SELECT Nombre_Usuario,
               Apellido_Usuario,
               Correo_Usuario,
               Contrasenna_Usuario
        FROM dbo.Usuario
        WHERE Correo_Usuario = @Correo
            AND ID_Estado = 1
    END
END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarCategoriaSP]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegistrarCategoriaSP]
    @Categoria varchar(20),
	@Estado_Categoria int
AS
BEGIN
    INSERT INTO [dbo].[Categorias] (
        [Nombre_Categoria],
		[Estado_Categoria]
    )
    VALUES (
        @Categoria,
		1
    );
END;
GO
/****** Object:  StoredProcedure [dbo].[RegistrarCompra]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[RegistrarCompra]
    @Empresa bigint,
    @Fecha DATETIME,
    @concepto NVARCHAR(255),
    @Cantidad INT,
    @Total DECIMAL(10, 2)
AS
BEGIN
    INSERT INTO compras (Empresa, Fecha, Concepto, Cantidad, Total)
    VALUES (@Empresa, @Fecha, @concepto, @Cantidad, @Total);
END;
GO
/****** Object:  StoredProcedure [dbo].[RegistrarEmpresaSP]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegistrarEmpresaSP]
    @Nombre_empresa varchar(255),
    @Descripcion varchar(255),
    @Ubicacion varchar(255)
AS
BEGIN
    INSERT INTO [dbo].[Empresa] (
        [Nombre_empresa],
        [Descripcion],
        [Ubicacion]
    )
    VALUES (
        @Nombre_empresa,
        @Descripcion,
        @Ubicacion
    );
END;
GO
/****** Object:  StoredProcedure [dbo].[RegistrarProveedorSP]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegistrarProveedorSP]
    @Nombre_Proveedor varchar(60),
    @Apellido_Proveedor varchar(70),
	@tipo int,
    @Cedula_Proveedor varchar(25),
    @Direccion_Exacta varchar(255),
    @Estado_Proveedor int,
    @Empresa bigint,
    @Telefono varchar(15),
    @Correo varchar(100)
AS
BEGIN
    INSERT INTO [dbo].[Proveedores] (

        [Nombre_Proveedor],
        [Apellido_Proveedor],
		[ID_Identificacion],
        [Cedula_Proveedor],
        [Direccion_Exacta],
        [Estado_Proveedor],
        [Empresa],
        [Telefono],
        [Correo]
    )
    VALUES (
		@Nombre_Proveedor,
        @Apellido_Proveedor,
		@tipo,
        @Cedula_Proveedor,
        @Direccion_Exacta,
        1,
        @Empresa,
        @Telefono,
        @Correo
    );
END;
GO
/****** Object:  StoredProcedure [dbo].[RegistrarUsuarioSP]    Script Date: 3/21/2024 12:32:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegistrarUsuarioSP]
	@tipo int,
	@Identificacion varchar(25),
    @Nombre         varchar(60),
	@Apellido         varchar(70),
    @Correo         varchar(100),
    @Contrasenna    varchar(50),
	@Telefono    varchar(20)
AS
BEGIN

	INSERT INTO dbo.Usuario (ID_Identificacion,Identificacion_Usuario,Nombre_Usuario,Apellido_Usuario,Correo_Usuario,Contrasenna_Usuario,ID_Direccion,Telefono_Usuario,ID_Estado,ID_Rol,C_esTemporal)
    VALUES (@tipo,@Identificacion,@Nombre,@Apellido,@Correo,@Contrasenna,null,@Telefono,1,2,0)

END
GO


USE [ImportadoraMoyaUlate]
GO

/****** Object:  StoredProcedure [dbo].[ObtenerTemporal]    Script Date: 25/03/2024 12:32:47 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ObtenerTemporal]
    @Correo varchar(100)
AS
BEGIN
    SELECT C_esTemporal
    FROM Usuario
    WHERE Correo_Usuario = @Correo;
END
GO


