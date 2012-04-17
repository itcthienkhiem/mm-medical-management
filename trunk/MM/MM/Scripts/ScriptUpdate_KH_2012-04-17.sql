USE MM
GO
/****** Object:  Table [dbo].[XetNghiem_Hitachi917]    Script Date: 04/17/2012 11:30:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[XetNghiem_Hitachi917](
	[XetNghiemGUID] [uniqueidentifier] NOT NULL,
	[TestNum] [int] NOT NULL,
	[TenXetNghiem] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Fullname] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Type] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_XetNghiem_Hitachi917] PRIMARY KEY CLUSTERED 
(
	[XetNghiemGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietXetNghiem_Hitachi917]    Script Date: 04/17/2012 11:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietXetNghiem_Hitachi917](
	[ChiTietXetNghiemGUID] [uniqueidentifier] NOT NULL,
	[XetNghiemGUID] [uniqueidentifier] NOT NULL,
	[FromValue] [float] NULL,
	[ToValue] [float] NULL,
	[DoiTuong] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietXetNghiem_Hitachi917_DoiTuong]  DEFAULT ((0)),
	[DonVi] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_ChiTietXetNghiem_Hitachi917] PRIMARY KEY CLUSTERED 
(
	[ChiTietXetNghiemGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChiTietXetNghiem_Hitachi917]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietXetNghiem_Hitachi917_XetNghiem_Hitachi917] FOREIGN KEY([XetNghiemGUID])
REFERENCES [dbo].[XetNghiem_Hitachi917] ([XetNghiemGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietXetNghiem_Hitachi917] CHECK CONSTRAINT [FK_ChiTietXetNghiem_Hitachi917_XetNghiem_Hitachi917]
GO
/****** Object:  Table [dbo].[KetQuaXetNghiem_Hitachi917]    Script Date: 04/17/2012 13:52:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KetQuaXetNghiem_Hitachi917](
	[KQXN_Hitachi917GUID] [uniqueidentifier] NOT NULL,
	[IDNum] [nvarchar](13) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PatientGUID] [uniqueidentifier] NULL,
	[NgayXN] [datetime] NOT NULL,
	[OperationID] [nvarchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_KetQuaXetNghiem_Hitachi917_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_KetQuaXetNghiem_Hitachi917] PRIMARY KEY CLUSTERED 
(
	[KQXN_Hitachi917GUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietKetQuaXetNghiem_Hitachi917]    Script Date: 04/17/2012 13:52:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietKetQuaXetNghiem_Hitachi917](
	[ChiTietKQXN_Hitachi917GUID] [uniqueidentifier] NOT NULL,
	[KQXN_Hitachi917GUID] [uniqueidentifier] NOT NULL,
	[TestNum] [int] NOT NULL,
	[TestResult] [nvarchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AlarmCode] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TinhTrang] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietKetQuaXetNghiem_Hitachi917_TinhTrang]  DEFAULT ((0)),
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietKetQuaXetNghiem_Hitachi917_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ChiTietKetQuaXetNghiem_Hitachi917] PRIMARY KEY CLUSTERED 
(
	[ChiTietKQXN_Hitachi917GUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChiTietKetQuaXetNghiem_Hitachi917]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietKetQuaXetNghiem_Hitachi917_KetQuaXetNghiem_Hitachi917] FOREIGN KEY([KQXN_Hitachi917GUID])
REFERENCES [dbo].[KetQuaXetNghiem_Hitachi917] ([KQXN_Hitachi917GUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietKetQuaXetNghiem_Hitachi917] CHECK CONSTRAINT [FK_ChiTietKetQuaXetNghiem_Hitachi917_KetQuaXetNghiem_Hitachi917]
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'd528283a-8924-4f7a-a812-a5e8030446d0', N'XetNghiem', N'Xét nghiệm')
GO