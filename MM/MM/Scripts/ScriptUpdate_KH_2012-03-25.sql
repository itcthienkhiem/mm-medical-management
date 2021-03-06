USE MM
GO
ALTER TABLE NhatKyLienHeCongTy
ADD [TenNguoiLienHe] [nvarchar](255) NULL,
	[SoDienThoaiLienHe] [nvarchar](50)  NULL,
	[SoNguoiKham] [int] NOT NULL  DEFAULT ((0)),
	[ThangKham] [datetime] NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[NhatKyLienHeCongTyView]
AS
SELECT     dbo.NhatKyLienHeCongTy.NhatKyLienHeCongTyGUID, dbo.NhatKyLienHeCongTy.DocStaffGUID, dbo.NhatKyLienHeCongTy.NgayGioLienHe, 
                      dbo.NhatKyLienHeCongTy.CongTyLienHe, dbo.NhatKyLienHeCongTy.NoiDungLienHe, dbo.NhatKyLienHeCongTy.CreatedDate, dbo.NhatKyLienHeCongTy.CreatedBy, 
                      dbo.NhatKyLienHeCongTy.UpdatedDate, dbo.NhatKyLienHeCongTy.UpdatedBy, dbo.NhatKyLienHeCongTy.DeletedDate, dbo.NhatKyLienHeCongTy.DeletedBy, 
                      dbo.NhatKyLienHeCongTy.Status, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS FullName, dbo.DocStaffView.DobStr, dbo.DocStaffView.GenderAsStr, 
                      dbo.DocStaffView.Status AS DocStaffStatus, dbo.NhatKyLienHeCongTy.Note, 
                      CASE dbo.NhatKyLienHeCongTy.UpdatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView_1.FullName END AS NguoiCapNhat, 
                      dbo.NhatKyLienHeCongTy.TenNguoiLienHe, dbo.NhatKyLienHeCongTy.SoDienThoaiLienHe, dbo.NhatKyLienHeCongTy.SoNguoiKham, 
                      dbo.NhatKyLienHeCongTy.ThangKham
FROM         dbo.NhatKyLienHeCongTy LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_1 ON dbo.NhatKyLienHeCongTy.UpdatedBy = DocStaffView_1.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.NhatKyLienHeCongTy.DocStaffGUID = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
UPDATE NhatKyLienHeCongTy
SET CreatedBy = DeletedBy




















