USE [master]
GO
/****** Object:  Database [MovieShop]    Script Date: 2/10/2020 11:00:50 am ******/
CREATE DATABASE [MovieShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MovieShop_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MovieShop.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MovieShop_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MovieShop.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MovieShop] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MovieShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MovieShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MovieShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MovieShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MovieShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MovieShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [MovieShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MovieShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MovieShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MovieShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MovieShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MovieShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MovieShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MovieShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MovieShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MovieShop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MovieShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MovieShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MovieShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MovieShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MovieShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MovieShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MovieShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MovieShop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MovieShop] SET  MULTI_USER 
GO
ALTER DATABASE [MovieShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MovieShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MovieShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MovieShop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MovieShop] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MovieShop', N'ON'
GO
ALTER DATABASE [MovieShop] SET QUERY_STORE = OFF
GO
USE [MovieShop]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 2/10/2020 11:00:51 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[Phone] [nvarchar](255) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movies]    Script Date: 2/10/2020 11:00:51 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movies](
	[MovieID] [int] IDENTITY(1,1) NOT NULL,
	[Rating] [nvarchar](50) NULL,
	[Title] [nvarchar](255) NULL,
	[Year] [nvarchar](255) NULL,
	[Rental_Cost] [money] NULL,
	[Copies] [nvarchar](50) NULL,
	[Plot] [ntext] NULL,
	[Genre] [nvarchar](50) NULL,
 CONSTRAINT [PK_Movies] PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RentedMovies]    Script Date: 2/10/2020 11:00:51 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RentedMovies](
	[RMID] [int] IDENTITY(1,1) NOT NULL,
	[MovieIDFK] [int] NULL,
	[CustIDFK] [int] NULL,
	[DateRented] [datetime] NULL,
	[DateReturned] [datetime] NULL,
	[Rented] [int] NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([CustID], [FirstName], [LastName], [Address], [Phone]) VALUES (1, N'harpreet', N'kaur', N'61, bondary road cludelands', N'0223997471')
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
ALTER TABLE [dbo].[RentedMovies]  WITH CHECK ADD  CONSTRAINT [FK1] FOREIGN KEY([MovieIDFK])
REFERENCES [dbo].[Movies] ([MovieID])
GO
ALTER TABLE [dbo].[RentedMovies] CHECK CONSTRAINT [FK1]
GO
ALTER TABLE [dbo].[RentedMovies]  WITH CHECK ADD  CONSTRAINT [FK2] FOREIGN KEY([CustIDFK])
REFERENCES [dbo].[Customer] ([CustID])
GO
ALTER TABLE [dbo].[RentedMovies] CHECK CONSTRAINT [FK2]
GO
USE [master]
GO
ALTER DATABASE [MovieShop] SET  READ_WRITE 
GO
