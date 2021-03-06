USE [MM]
GO
/****** Object:  Table [dbo].[PhieuChi]    Script Date: 05/23/2015 22:53:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuChi](
	[PhieuChiGUID] [uniqueidentifier] NOT NULL,
	[SoPhieuChi] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NgayChi] [datetime] NOT NULL,
	[SoTien] [float] NOT NULL,
	[DienGiai] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_PhieuChi_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_PhieuChi] PRIMARY KEY CLUSTERED 
(
	[PhieuChiGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]











