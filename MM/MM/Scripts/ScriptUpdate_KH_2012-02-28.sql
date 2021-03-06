USE MM
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'2ae953e4-e23a-4a43-8a81-312d80117244', N'DichVuChuaXuatPhieuThu', N'Báo cáo dịch vụ chưa xuất phiếu thu')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'18d1087a-76d8-40e8-b7e5-b0af2130cb1c', N'HoaDonThuoc', N'Hóa đơn thuốc')
GO
ALTER TABLE [PhieuThuThuoc]
ADD [IsExported] [bit] NOT NULL DEFAULT ((0))
GO
/****** Object:  Table [dbo].[InvoiceDetail]    Script Date: 02/29/2012 08:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDetail](
	[InvoiceDetailGUID] [uniqueidentifier] NOT NULL,
	[InvoiceGUID] [uniqueidentifier] NOT NULL,
	[TenDichVu] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DonViTinh] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SoLuong] [int] NOT NULL CONSTRAINT [DF_InvoiceDetail_SoLuong]  DEFAULT ((0)),
	[DonGia] [float] NOT NULL CONSTRAINT [DF_InvoiceDetail_DonGia]  DEFAULT ((0)),
	[ThanhTien] [float] NOT NULL CONSTRAINT [DF_InvoiceDetail_ThanhTien]  DEFAULT ((0)),
	[CreatedDate] [datetime] NULL,
	[CreateBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_InvoiceDetail_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_InvoiceDetail] PRIMARY KEY CLUSTERED 
(
	[InvoiceDetailGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Invoice] FOREIGN KEY([InvoiceGUID])
REFERENCES [dbo].[Invoice] ([InvoiceGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_Invoice]
GO
/****** Object:  Table [dbo].[HoaDonThuoc]    Script Date: 02/29/2012 08:36:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDonThuoc](
	[HoaDonThuocGUID] [uniqueidentifier] NOT NULL,
	[PhieuThuThuocGUID] [uniqueidentifier] NOT NULL,
	[SoHoaDon] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NgayXuatHoaDon] [datetime] NULL,
	[TenNguoiMuaHang] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DiaChi] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TenDonVi] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaSoThue] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SoTaiKhoan] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HinhThucThanhToan] [tinyint] NULL,
	[VAT] [float] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_HoaDonThuoc_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_HoaDonThuoc] PRIMARY KEY CLUSTERED 
(
	[HoaDonThuocGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[HoaDonThuoc]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonThuoc_PhieuThuThuoc] FOREIGN KEY([PhieuThuThuocGUID])
REFERENCES [dbo].[PhieuThuThuoc] ([PhieuThuThuocGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HoaDonThuoc] CHECK CONSTRAINT [FK_HoaDonThuoc_PhieuThuThuoc]
GO
/****** Object:  Table [dbo].[ChiTietHoaDonThuoc]    Script Date: 02/29/2012 08:36:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDonThuoc](
	[ChiTietHoaDonThuocGUID] [uniqueidentifier] NOT NULL,
	[HoaDonThuocGUID] [uniqueidentifier] NOT NULL,
	[TenThuoc] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DonViTinh] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SoLuong] [int] NOT NULL CONSTRAINT [DF_ChiTietHoaDonThuoc_SoLuong]  DEFAULT ((0)),
	[DonGia] [float] NOT NULL CONSTRAINT [DF_ChiTietHoaDonThuoc_DonGia]  DEFAULT ((0)),
	[ThanhTien] [float] NOT NULL CONSTRAINT [DF_ChiTietHoaDonThuoc_ThanhTien]  DEFAULT ((0)),
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietHoaDonThuoc_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ChiTietHoaDonThuoc] PRIMARY KEY CLUSTERED 
(
	[ChiTietHoaDonThuocGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChiTietHoaDonThuoc]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHoaDonThuoc_HoaDonThuoc] FOREIGN KEY([HoaDonThuocGUID])
REFERENCES [dbo].[HoaDonThuoc] ([HoaDonThuocGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietHoaDonThuoc] CHECK CONSTRAINT [FK_ChiTietHoaDonThuoc_HoaDonThuoc]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 02/29/2012 12:24:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[SettingGUID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Settings_SettingGUID]  DEFAULT (newid()),
	[SoHoaDonBatDau] [bigint] NOT NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[SettingGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
UPDATE [Function]
SET FunctionName = N'Phiếu thu dịch vụ'
WHERE FunctionCode='Receipt'
GO
UPDATE [Function]
SET FunctionName = N'Hóa đơn dịch vụ'
WHERE FunctionCode='Invoice'
GO
/****** Object:  View [dbo].[HoaDonThuocView]    Script Date: 02/29/2012 13:23:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[HoaDonThuocView]
AS
SELECT     HoaDonThuocGUID, PhieuThuThuocGUID, SoHoaDon, NgayXuatHoaDon, TenNguoiMuaHang, DiaChi, TenDonVi, MaSoThue, SoTaiKhoan, 
                      HinhThucThanhToan, VAT, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, Status, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'Tiền mặt' WHEN 1 THEN N'Chuyển khoản' END AS HinhThucThanhToanStr
FROM         dbo.HoaDonThuoc

GO

