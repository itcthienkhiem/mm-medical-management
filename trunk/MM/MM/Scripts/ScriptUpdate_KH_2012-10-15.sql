USE MM
GO
/****** Object:  Table [dbo].[KhoCapCuu]    Script Date: 10/15/2012 22:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhoCapCuu](
	[KhoCapCuuGUID] [uniqueidentifier] NOT NULL,
	[TenCapCuu] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DonViTinh] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_KhoCapCuu_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_KhoCapCuu] PRIMARY KEY CLUSTERED 
(
	[KhoCapCuuGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhapKhoCapCuu]    Script Date: 10/15/2012 22:05:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhapKhoCapCuu](
	[NhapKhoCapCuuGUID] [uniqueidentifier] NOT NULL,
	[KhoCapCuuGUID] [uniqueidentifier] NOT NULL,
	[SoDangKy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NhaPhanPhoi] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HangSanXuat] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NgaySanXuat] [datetime] NULL,
	[NgayHetHan] [datetime] NULL,
	[NgayNhap] [datetime] NOT NULL,
	[SoLuongNhap] [int] NOT NULL,
	[GiaNhap] [float] NULL,
	[DonViTinhNhap] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SoLuongQuiDoi] [int] NOT NULL,
	[DonViTinhQuiDoi] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GiaNhapQuiDoi] [float] NULL,
	[Note] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_NhapKhoCapCuu_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_NhapKhoCapCuu] PRIMARY KEY CLUSTERED 
(
	[NhapKhoCapCuuGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[NhapKhoCapCuu]  WITH CHECK ADD  CONSTRAINT [FK_NhapKhoCapCuu_KhoCapCuu] FOREIGN KEY([KhoCapCuuGUID])
REFERENCES [dbo].[KhoCapCuu] ([KhoCapCuuGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  Table [dbo].[XuatKhoCapCuu]    Script Date: 10/15/2012 22:06:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[XuatKhoCapCuu](
	[XuatKhoCapCuuGUID] [uniqueidentifier] NOT NULL,
	[NgayXuat] [datetime] NOT NULL,
	[Note] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_XuatKhoCapCuu_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_XuatKhoCapCuu] PRIMARY KEY CLUSTERED 
(
	[XuatKhoCapCuuGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietXuatKhoCapCuu]    Script Date: 10/15/2012 22:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietXuatKhoCapCuu](
	[ChiTietXuatKhoCapCuuGUID] [uniqueidentifier] NOT NULL,
	[XuatKhoCapCuuGUID] [uniqueidentifier] NOT NULL,
	[KhoCapCuuGUID] [uniqueidentifier] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[GiaXuat] [float] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietXuatKhoCapCuu_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ChiTietXuatKhoCapCuu] PRIMARY KEY CLUSTERED 
(
	[ChiTietXuatKhoCapCuuGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChiTietXuatKhoCapCuu]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietXuatKhoCapCuu_KhoCapCuu] FOREIGN KEY([KhoCapCuuGUID])
REFERENCES [dbo].[KhoCapCuu] ([KhoCapCuuGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietXuatKhoCapCuu]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietXuatKhoCapCuu_XuatKhoCapCuu] FOREIGN KEY([XuatKhoCapCuuGUID])
REFERENCES [dbo].[XuatKhoCapCuu] ([XuatKhoCapCuuGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  View [dbo].[ChiTietXuatKhoCapCuuView]    Script Date: 10/15/2012 22:14:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ChiTietXuatKhoCapCuuView]
AS
SELECT     dbo.ChiTietXuatKhoCapCuu.ChiTietXuatKhoCapCuuGUID, dbo.ChiTietXuatKhoCapCuu.XuatKhoCapCuuGUID, dbo.ChiTietXuatKhoCapCuu.KhoCapCuuGUID, 
                      dbo.ChiTietXuatKhoCapCuu.SoLuong, dbo.ChiTietXuatKhoCapCuu.GiaXuat, dbo.ChiTietXuatKhoCapCuu.CreatedDate, dbo.ChiTietXuatKhoCapCuu.CreatedBy, 
                      dbo.ChiTietXuatKhoCapCuu.UpdatedDate, dbo.ChiTietXuatKhoCapCuu.UpdatedBy, dbo.ChiTietXuatKhoCapCuu.DeletedDate, dbo.ChiTietXuatKhoCapCuu.DeletedBy, 
                      dbo.ChiTietXuatKhoCapCuu.Status AS ChiTietXuatKhoCapCuuStatus, dbo.KhoCapCuu.TenCapCuu, dbo.KhoCapCuu.DonViTinh, 
                      dbo.KhoCapCuu.Status AS KhoCapCuuStatus
FROM         dbo.KhoCapCuu INNER JOIN
                      dbo.ChiTietXuatKhoCapCuu ON dbo.KhoCapCuu.KhoCapCuuGUID = dbo.ChiTietXuatKhoCapCuu.KhoCapCuuGUID

GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'48fd9e0f-6a76-4252-a125-049502f8c890', N'KhoCapCuu', N'Kho cấp cứu')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'27a0dca7-fdd5-41e1-af59-641820f24870', N'NhapKhoCapCuu', N'Nhập kho cấp cứu')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'9a28c1e8-35ce-4333-bb85-40c2fc655190', N'XuatKhoCapCuu', N'Xuất kho cấp cứu')


