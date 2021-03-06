USE MM
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'cb5ce20e-e1c3-4560-94ef-70bce76d054f', N'PhongCho', N'Phòng chờ')
GO
/****** Object:  Table [dbo].[PhongCho]    Script Date: 02/19/2012 10:04:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhongCho](
	[PhongChoGUID] [uniqueidentifier] NOT NULL,
	[PatientGUID] [uniqueidentifier] NOT NULL,
	[Ngay] [datetime] NOT NULL,
 CONSTRAINT [PK_PhongCho] PRIMARY KEY CLUSTERED 
(
	[PhongChoGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PhongCho]  WITH CHECK ADD  CONSTRAINT [FK_PhongCho_Patient] FOREIGN KEY([PatientGUID])
REFERENCES [dbo].[Patient] ([PatientGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  Table [dbo].[KetQuaNoiSoi]    Script Date: 02/19/2012 10:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KetQuaNoiSoi](
	[KetQuaNoiSoiGUID] [uniqueidentifier] NOT NULL,
	[SoPhieu] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NgayKham] [datetime] NOT NULL,
	[PatientGUID] [uniqueidentifier] NOT NULL,
	[LyDoKham] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BacSiChiDinh] [uniqueidentifier] NULL,
	[BacSiSoi] [uniqueidentifier] NOT NULL,
	[KetLuan] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DeNghi] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LoaiNoiSoi] [tinyint] NOT NULL CONSTRAINT [DF_KetQuaNoiSoi_LoaiNoiSoi]  DEFAULT ((0)),
	[OngTaiTrai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OngTaiPhai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MangNhiTrai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MangNhiPhai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CanBuaTrai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CanBuaPhai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HomNhiTrai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HomNhiPhai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ValsavaTrai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ValsavaPhai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NiemMacTrai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NiemMacPhai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VachNganTrai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VachNganPhai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[KheTrenTrai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[KheTrenPhai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[KheGiuaTrai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[KheGiuaPhai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CuonGiuaTrai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CuonGiuaPhai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CuonDuoiTrai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CuonDuoiPhai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MomMocTrai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MomMocPhai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BongSangTrai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BongSangPhai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VomTrai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VomPhai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Amydale] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[XoangLe] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MiengThucQuan] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SunPheu] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DayThanh] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BangThanhThat] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OngTaiNgoai] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MangNhi] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NiemMac] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VachNgan] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[KheTren] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[KheGiua] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MomMoc_BongSang] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Vom] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ThanhQuan] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Hinh1] [image] NULL,
	[Hinh2] [image] NULL,
	[Hinh3] [image] NULL,
	[Hinh4] [image] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_KetQuaNoiSoi_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_KetQuaNoiSoi] PRIMARY KEY CLUSTERED 
(
	[KetQuaNoiSoiGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
USE [MM]
GO
ALTER TABLE [dbo].[KetQuaNoiSoi]  WITH CHECK ADD  CONSTRAINT [FK_KetQuaNoiSoi_DocStaff] FOREIGN KEY([BacSiSoi])
REFERENCES [dbo].[DocStaff] ([DocStaffGUID])
GO
ALTER TABLE [dbo].[KetQuaNoiSoi]  WITH CHECK ADD  CONSTRAINT [FK_KetQuaNoiSoi_Patient] FOREIGN KEY([PatientGUID])
REFERENCES [dbo].[Patient] ([PatientGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  View [dbo].[BacSiChiDinhView]    Script Date: 02/19/2012 10:05:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[BacSiChiDinhView]
AS
SELECT     dbo.DocStaffView.*
FROM         dbo.DocStaffView

GO
/****** Object:  View [dbo].[BacSiNoiSoiView]    Script Date: 02/19/2012 10:05:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[BacSiNoiSoiView]
AS
SELECT     dbo.DocStaffView.*
FROM         dbo.DocStaffView

GO
/****** Object:  View [dbo].[KetQuaNoiSoiView]    Script Date: 02/19/2012 10:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[KetQuaNoiSoiView]
AS
SELECT     dbo.KetQuaNoiSoi.KetQuaNoiSoiGUID, dbo.KetQuaNoiSoi.SoPhieu, dbo.KetQuaNoiSoi.NgayKham, dbo.KetQuaNoiSoi.PatientGUID, dbo.KetQuaNoiSoi.LyDoKham, 
                      dbo.KetQuaNoiSoi.BacSiChiDinh, dbo.KetQuaNoiSoi.BacSiSoi, dbo.KetQuaNoiSoi.KetLuan, dbo.KetQuaNoiSoi.DeNghi, dbo.KetQuaNoiSoi.LoaiNoiSoi, 
                      dbo.KetQuaNoiSoi.OngTaiTrai, dbo.KetQuaNoiSoi.MangNhiTrai, dbo.KetQuaNoiSoi.MangNhiPhai, dbo.KetQuaNoiSoi.CanBuaTrai, dbo.KetQuaNoiSoi.CanBuaPhai, 
                      dbo.KetQuaNoiSoi.HomNhiTrai, dbo.KetQuaNoiSoi.HomNhiPhai, dbo.KetQuaNoiSoi.ValsavaTrai, dbo.KetQuaNoiSoi.ValsavaPhai, dbo.KetQuaNoiSoi.NiemMacTrai, 
                      dbo.KetQuaNoiSoi.NiemMacPhai, dbo.KetQuaNoiSoi.VachNganTrai, dbo.KetQuaNoiSoi.VachNganPhai, dbo.KetQuaNoiSoi.KheTrenTrai, 
                      dbo.KetQuaNoiSoi.KheTrenPhai, dbo.KetQuaNoiSoi.KheGiuaTrai, dbo.KetQuaNoiSoi.KheGiuaPhai, dbo.KetQuaNoiSoi.CuonGiuaTrai, dbo.KetQuaNoiSoi.CuonGiuaPhai, 
                      dbo.KetQuaNoiSoi.CuonDuoiTrai, dbo.KetQuaNoiSoi.CuonDuoiPhai, dbo.KetQuaNoiSoi.BongSangTrai, dbo.KetQuaNoiSoi.BongSangPhai, dbo.KetQuaNoiSoi.VomTrai, 
                      dbo.KetQuaNoiSoi.VomPhai, dbo.KetQuaNoiSoi.Amydale, dbo.KetQuaNoiSoi.XoangLe, dbo.KetQuaNoiSoi.MiengThucQuan, dbo.KetQuaNoiSoi.SunPheu, 
                      dbo.KetQuaNoiSoi.DayThanh, dbo.KetQuaNoiSoi.BangThanhThat, dbo.KetQuaNoiSoi.OngTaiNgoai, dbo.KetQuaNoiSoi.MangNhi, dbo.KetQuaNoiSoi.NiemMac, 
                      dbo.KetQuaNoiSoi.VachNgan, dbo.KetQuaNoiSoi.KheTren, dbo.KetQuaNoiSoi.KheGiua, dbo.KetQuaNoiSoi.MomMoc_BongSang, dbo.KetQuaNoiSoi.Vom, 
                      dbo.KetQuaNoiSoi.ThanhQuan, dbo.KetQuaNoiSoi.Hinh1, dbo.KetQuaNoiSoi.Hinh2, dbo.KetQuaNoiSoi.Hinh3, dbo.KetQuaNoiSoi.Hinh4, 
                      dbo.KetQuaNoiSoi.CreatedDate, dbo.KetQuaNoiSoi.CreatedBy, dbo.KetQuaNoiSoi.UpdatedDate, dbo.KetQuaNoiSoi.UpdatedBy, dbo.KetQuaNoiSoi.DeletedDate, 
                      dbo.KetQuaNoiSoi.DeletedBy, dbo.KetQuaNoiSoi.Status, dbo.BacSiChiDinhView.FullName AS TenBacSiChiDinh, dbo.BacSiNoiSoiView.FullName AS TenBacSiNoiSoi, 
                      dbo.BacSiChiDinhView.Archived AS BSCDArchived, dbo.BacSiNoiSoiView.Archived AS BSNSArchived, 
                      CASE LoaiNoiSoi WHEN 0 THEN N'Tai' WHEN 1 THEN N'Mũi' WHEN 2 THEN N'Họng - Thanh quản' WHEN 3 THEN N'Tai mũi họng' WHEN 4 THEN N'Tổng quát' END AS LoaiNoiSoiStr,
                       dbo.KetQuaNoiSoi.OngTaiPhai, dbo.KetQuaNoiSoi.MomMocTrai, dbo.KetQuaNoiSoi.MomMocPhai
FROM         dbo.KetQuaNoiSoi INNER JOIN
                      dbo.BacSiNoiSoiView ON dbo.KetQuaNoiSoi.BacSiSoi = dbo.BacSiNoiSoiView.DocStaffGUID LEFT OUTER JOIN
                      dbo.BacSiChiDinhView ON dbo.KetQuaNoiSoi.BacSiChiDinh = dbo.BacSiChiDinhView.DocStaffGUID

GO