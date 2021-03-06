USE MM
GO
/****** Object:  Table [dbo].[CanDo]    Script Date: 01/02/2012 21:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CanDo](
	[CanDoGuid] [uniqueidentifier] NOT NULL CONSTRAINT [DF_CanDo_CanDoGuid]  DEFAULT (newid()),
	[PatientGUID] [uniqueidentifier] NOT NULL,
	[NgayCanDo] [datetime] NOT NULL,
	[TimMach] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HuyetAp] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HoHap] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ChieuCao] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CanNang] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BMI] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CanDoKhac] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_CanDo_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_CanDo] PRIMARY KEY CLUSTERED 
(
	[CanDoGuid] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [MM]
GO
ALTER TABLE [dbo].[CanDo]  WITH CHECK ADD  CONSTRAINT [FK_CanDo_Patient] FOREIGN KEY([PatientGUID])
REFERENCES [dbo].[Patient] ([PatientGUID])
ON UPDATE CASCADE
ON DELETE CASCADE