USE MM
GO
DELETE FROM Invoice
GO
DELETE FROM HoaDonThuoc
GO
ALTER TABLE Invoice
DROP CONSTRAINT FK_Invoice_Receipt
GO
ALTER TABLE HoaDonThuoc
DROP CONSTRAINT FK_HoaDonThuoc_PhieuThuThuoc
GO
ALTER TABLE Invoice
DROP COLUMN ReceiptGUID
GO
ALTER TABLE Invoice
ADD [ReceiptGUIDList] [nvarchar](max) NULL
GO
ALTER TABLE HoaDonThuoc
DROP COLUMN PhieuThuThuocGUID
GO
ALTER TABLE HoaDonThuoc
ADD [PhieuThuThuocGUIDList] [nvarchar](max) NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[InvoiceView]
AS
SELECT     InvoiceGUID, InvoiceCode, InvoiceDate, TenDonVi, SoTaiKhoan, HinhThucThanhToan, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, 
                      Status, VAT, CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' END AS HinhThucThanhToanStr, MaSoThue, ReceiptGUIDList, TenNguoiMuaHang, 
                      DiaChi
FROM         dbo.Invoice
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[HoaDonThuocView]
AS
SELECT     HoaDonThuocGUID, SoHoaDon, NgayXuatHoaDon, TenNguoiMuaHang, DiaChi, TenDonVi, MaSoThue, SoTaiKhoan, HinhThucThanhToan, VAT, CreatedDate, 
                      CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, Status, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'TM' WHEN 1 THEN N'CK' END AS HinhThucThanhToanStr, PhieuThuThuocGUIDList
FROM         dbo.HoaDonThuoc
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

DELETE FROM Invoice

DELETE FROM HoaDonThuoc