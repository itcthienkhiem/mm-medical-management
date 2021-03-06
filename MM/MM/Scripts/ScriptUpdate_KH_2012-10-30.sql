USE MM
GO
ALTER TABLE ThongBao
ADD [NguoiDuyet1GUID] [uniqueidentifier] NULL,
	[NguoiDuyet2GUID] [uniqueidentifier] NULL,
	[NguoiDuyet3GUID] [uniqueidentifier] NULL
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ThongBaoView]
AS
SELECT     dbo.ThongBao.ThongBaoGUID, dbo.ThongBao.TenThongBao, dbo.ThongBao.ThongBaoBuff, dbo.ThongBao.NgayDuyet1, dbo.ThongBao.ThongBaoBuff1, 
                      dbo.ThongBao.NgayDuyet2, dbo.ThongBao.ThongBaoBuff2, dbo.ThongBao.NgayDuyet3, dbo.ThongBao.ThongBaoBuff3, dbo.ThongBao.CreatedDate, 
                      dbo.ThongBao.CreatedBy, dbo.ThongBao.UpdatedDate, dbo.ThongBao.UpdatedBy, dbo.ThongBao.DeletedDate, dbo.ThongBao.DeletedBy, dbo.ThongBao.Status, 
                      dbo.DocStaffView.DobStr, dbo.DocStaffView.GenderAsStr, 
                      CASE dbo.ThongBao.CreatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView.FullName END AS FullName, dbo.ThongBao.Path,
                       dbo.ThongBao.GhiChu, 
                      CASE dbo.ThongBao.NguoiDuyet1GUID WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView_1.FullName END AS NguoiDuyet1, 
                      CASE dbo.ThongBao.NguoiDuyet2GUID WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView_2.FullName END AS NguoiDuyet2, 
                      CASE dbo.ThongBao.NguoiDuyet3GUID WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView_3.FullName END AS NguoiDuyet3, 
                      dbo.ThongBao.NguoiDuyet1GUID, dbo.ThongBao.NguoiDuyet2GUID, dbo.ThongBao.NguoiDuyet3GUID
FROM         dbo.ThongBao LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_3 ON dbo.ThongBao.NguoiDuyet3GUID = DocStaffView_3.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_2 ON dbo.ThongBao.NguoiDuyet2GUID = DocStaffView_2.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_1 ON dbo.ThongBao.NguoiDuyet1GUID = DocStaffView_1.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.ThongBao.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
UPDATE ThongBao
SET NguoiDuyet1GUID = CreatedBy
WHERE NgayDuyet1 IS NOT NULL
GO
UPDATE ThongBao
SET NguoiDuyet2GUID = CreatedBy
WHERE NgayDuyet2 IS NOT NULL
GO
UPDATE ThongBao
SET NguoiDuyet3GUID = CreatedBy
WHERE NgayDuyet3 IS NOT NULL



















