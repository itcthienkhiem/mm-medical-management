USE MM
GO
/****** Object:  Table [dbo].[PhieuThuHopDong]    Script Date: 03/12/2012 15:36:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuThuHopDong](
	[PhieuThuHopDongGUID] [uniqueidentifier] NOT NULL,
	[HopDongGUID] [uniqueidentifier] NOT NULL,
	[MaPhieuThuHopDong] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TenNguoiNop] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TenCongTy] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DiaChi] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NgayThu] [datetime] NOT NULL,
	[Notes] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsExported] [bit] NOT NULL CONSTRAINT [DF_PhieuThuHopDong_IsExported]  DEFAULT ((0)),
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_PhieuThuHopDong_Status]  DEFAULT ((0)),
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_PhieuThuHopDong] PRIMARY KEY CLUSTERED 
(
	[PhieuThuHopDongGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PhieuThuHopDong]  WITH CHECK ADD  CONSTRAINT [FK_PhieuThuHopDong_CompanyContract] FOREIGN KEY([HopDongGUID])
REFERENCES [dbo].[CompanyContract] ([CompanyContractGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PhieuThuHopDong] CHECK CONSTRAINT [FK_PhieuThuHopDong_CompanyContract]
GO
/****** Object:  Table [dbo].[ChiTietPhieuThuHopDong]    Script Date: 03/12/2012 15:36:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietPhieuThuHopDong](
	[ChiTietPhieuThuHopDongGUID] [uniqueidentifier] NOT NULL,
	[PhieuThuHopDongGUID] [uniqueidentifier] NOT NULL,
	[DichVu] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DonGia] [float] NOT NULL,
	[SoLuong] [float] NOT NULL,
	[Giam] [float] NOT NULL,
	[ThanhTien] [float] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietPhieuThuHopDong_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ChiTietPhieuThuHopDong] PRIMARY KEY CLUSTERED 
(
	[ChiTietPhieuThuHopDongGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChiTietPhieuThuHopDong]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuThuHopDong_PhieuThuHopDong] FOREIGN KEY([PhieuThuHopDongGUID])
REFERENCES [dbo].[PhieuThuHopDong] ([PhieuThuHopDongGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietPhieuThuHopDong] CHECK CONSTRAINT [FK_ChiTietPhieuThuHopDong_PhieuThuHopDong]
GO
/****** Object:  Table [dbo].[HoaDonHopDong]    Script Date: 03/12/2012 15:36:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDonHopDong](
	[HoaDonHopDongGUID] [uniqueidentifier] NOT NULL,
	[PhieuThuHopDongGUIDList] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SoHoaDon] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NgayXuatHoaDon] [datetime] NULL,
	[TenNguoiMuaHang] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DiaChi] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TenDonVi] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaSoThue] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SoTaiKhoan] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HinhThucThanhToan] [tinyint] NULL,
	[VAT] [float] NULL,
	[Notes] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_HoaDonHopDong_Status]  DEFAULT ((0)),
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_HoaDonHopDong] PRIMARY KEY CLUSTERED 
(
	[HoaDonHopDongGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietHoaDonHopDong]    Script Date: 03/12/2012 15:37:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDonHopDong](
	[ChiTietHoaDonHopDongGUID] [uniqueidentifier] NOT NULL,
	[HoaDonHopDongGUID] [uniqueidentifier] NOT NULL,
	[TenMatHang] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DonViTinh] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SoLuong] [int] NOT NULL,
	[DonGia] [float] NOT NULL,
	[ThanhTien] [float] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ChiTietHoaDonHopDong_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ChiTietHoaDonHopDong] PRIMARY KEY CLUSTERED 
(
	[ChiTietHoaDonHopDongGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ChiTietHoaDonHopDong]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHoaDonHopDong_HoaDonHopDong] FOREIGN KEY([HoaDonHopDongGUID])
REFERENCES [dbo].[HoaDonHopDong] ([HoaDonHopDongGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietHoaDonHopDong] CHECK CONSTRAINT [FK_ChiTietHoaDonHopDong_HoaDonHopDong]
GO
/****** Object:  View [dbo].[HoaDonHopDongView]    Script Date: 03/12/2012 15:37:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[HoaDonHopDongView]
AS
SELECT     HoaDonHopDongGUID, PhieuThuHopDongGUIDList, SoHoaDon, NgayXuatHoaDon, TenNguoiMuaHang, DiaChi, TenDonVi, MaSoThue, SoTaiKhoan, 
                      HinhThucThanhToan, VAT, Notes, Status, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' WHEN 2 THEN N'TM/CK' END AS HinhThucThanhToanStr
FROM         dbo.HoaDonHopDong

GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'a7b7da37-278b-44ac-8974-284cc68d8485', N'PhieuThuHopDong', N'Phiếu thu hợp đồng')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'e1f9f5d1-4239-4b4a-82d3-fce1cb025152', N'HoaDonHopDong', N'Hóa đơn hợp đồng')
GO
