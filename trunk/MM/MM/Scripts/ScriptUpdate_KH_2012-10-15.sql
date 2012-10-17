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
/****** Object:  Table [dbo].[NhapKhoCapCuu]    Script Date: 10/17/2012 21:57:48 ******/
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
	[SoLuongXuat] [int] NOT NULL CONSTRAINT [DF_NhapKhoCapCuu_SoLuongXuat]  DEFAULT ((0)),
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
/****** Object:  Table [dbo].[XuatKhoCapCuu]    Script Date: 10/17/2012 21:23:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[XuatKhoCapCuu](
	[XuatKhoCapCuuGUID] [uniqueidentifier] NOT NULL,
	[KhoCapCuuGUID] [uniqueidentifier] NOT NULL,
	[NgayXuat] [datetime] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[GiaXuat] [float] NULL,
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
ALTER TABLE [dbo].[XuatKhoCapCuu]  WITH CHECK ADD  CONSTRAINT [FK_XuatKhoCapCuu_KhoCapCuu] FOREIGN KEY([KhoCapCuuGUID])
REFERENCES [dbo].[KhoCapCuu] ([KhoCapCuuGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  View [dbo].[NhapKhoCapCuuView]    Script Date: 10/17/2012 21:58:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[NhapKhoCapCuuView]
AS
SELECT     dbo.NhapKhoCapCuu.NhapKhoCapCuuGUID, dbo.NhapKhoCapCuu.KhoCapCuuGUID, dbo.NhapKhoCapCuu.SoDangKy, dbo.NhapKhoCapCuu.NhaPhanPhoi, 
                      dbo.NhapKhoCapCuu.NgaySanXuat, dbo.NhapKhoCapCuu.HangSanXuat, dbo.NhapKhoCapCuu.NgayHetHan, dbo.NhapKhoCapCuu.NgayNhap, 
                      dbo.NhapKhoCapCuu.SoLuongNhap, dbo.NhapKhoCapCuu.GiaNhap, dbo.NhapKhoCapCuu.DonViTinhNhap, dbo.NhapKhoCapCuu.SoLuongQuiDoi, 
                      dbo.NhapKhoCapCuu.DonViTinhQuiDoi, dbo.NhapKhoCapCuu.GiaNhapQuiDoi, dbo.NhapKhoCapCuu.Note, dbo.NhapKhoCapCuu.CreatedDate, 
                      dbo.NhapKhoCapCuu.CreatedBy, dbo.NhapKhoCapCuu.UpdatedDate, dbo.NhapKhoCapCuu.UpdatedBy, dbo.NhapKhoCapCuu.DeletedBy, 
                      dbo.NhapKhoCapCuu.DeletedDate, dbo.NhapKhoCapCuu.Status AS NhapKhoCapCuuStatus, dbo.KhoCapCuu.TenCapCuu, dbo.KhoCapCuu.Status AS KhoCapCuuStatus, 
                      dbo.NhapKhoCapCuu.SoLuongXuat
FROM         dbo.KhoCapCuu INNER JOIN
                      dbo.NhapKhoCapCuu ON dbo.KhoCapCuu.KhoCapCuuGUID = dbo.NhapKhoCapCuu.KhoCapCuuGUID
GO
/****** Object:  View [dbo].[XuatKhoCapCuuView]    Script Date: 10/17/2012 21:35:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[XuatKhoCapCuuView]
AS
SELECT     dbo.XuatKhoCapCuu.XuatKhoCapCuuGUID, dbo.XuatKhoCapCuu.KhoCapCuuGUID, dbo.XuatKhoCapCuu.NgayXuat, dbo.XuatKhoCapCuu.SoLuong, 
                      dbo.XuatKhoCapCuu.GiaXuat, dbo.XuatKhoCapCuu.Note, dbo.XuatKhoCapCuu.CreatedDate, dbo.XuatKhoCapCuu.CreatedBy, dbo.XuatKhoCapCuu.UpdatedDate, 
                      dbo.XuatKhoCapCuu.UpdatedBy, dbo.XuatKhoCapCuu.DeletedDate, dbo.XuatKhoCapCuu.DeletedBy, dbo.XuatKhoCapCuu.Status AS XuatKhoCapCuuStatus, 
                      dbo.KhoCapCuu.TenCapCuu, dbo.KhoCapCuu.DonViTinh, dbo.KhoCapCuu.Status AS KhoCapCuuStatus
FROM         dbo.KhoCapCuu INNER JOIN
                      dbo.XuatKhoCapCuu ON dbo.KhoCapCuu.KhoCapCuuGUID = dbo.XuatKhoCapCuu.KhoCapCuuGUID

GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'48fd9e0f-6a76-4252-a125-049502f8c890', N'KhoCapCuu', N'Kho cấp cứu')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'27a0dca7-fdd5-41e1-af59-641820f24870', N'NhapKhoCapCuu', N'Nhập kho cấp cứu')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'9a28c1e8-35ce-4333-bb85-40c2fc655190', N'XuatKhoCapCuu', N'Xuất kho cấp cứu')


