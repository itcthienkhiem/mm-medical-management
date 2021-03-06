USE MM
GO
ALTER TABLE Invoice
ADD MaSoThue nvarchar(50) NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[InvoiceView]
AS
SELECT     dbo.Invoice.InvoiceGUID, dbo.Invoice.ReceiptGUID, dbo.Invoice.InvoiceCode, dbo.Invoice.InvoiceDate, dbo.Invoice.TenDonVi, dbo.Invoice.SoTaiKhoan, 
                      dbo.Invoice.HinhThucThanhToan, dbo.Invoice.CreatedDate, dbo.Invoice.CreatedBy, dbo.Invoice.UpdatedDate, dbo.Invoice.UpdatedBy, dbo.Invoice.DeletedDate, 
                      dbo.Invoice.DeletedBy, dbo.Invoice.Status, dbo.ReceiptView.Address, dbo.ReceiptView.FileNum, dbo.ReceiptView.FullName, dbo.Invoice.VAT, 
                      CASE HinhThucThanhToan WHEN 0 THEN N'Tiền mặt' WHEN 1 THEN N'Chuyển khoản' END AS HinhThucThanhToanStr, dbo.Invoice.MaSoThue
FROM         dbo.Invoice INNER JOIN
                      dbo.ReceiptView ON dbo.Invoice.ReceiptGUID = dbo.ReceiptView.ReceiptGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


