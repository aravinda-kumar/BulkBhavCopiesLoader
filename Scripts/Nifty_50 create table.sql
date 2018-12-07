USE [BhavCopiesDb]
GO

/****** Object:  Table [dbo].[ind_Nifty_50]    Script Date: 05-12-2018 11.53.44 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Nifty_50](
	[Id] BIGINT IDENTITY(1,1) PRIMARY KEY,
	[Company Name] [varchar](100) NULL,
	[Industry] [varchar](100) NULL,
	[Symbol] [varchar](50) NULL,
	[Series] [varchar](50) NULL,
	[ISIN Code] [varchar](50) NULL
) ON [PRIMARY]
GO


