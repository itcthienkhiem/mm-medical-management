USE [MM]
GO
ALTER TABLE KetQuaNoiSoi
ADD [ImageName1] [nvarchar](50)  NULL,
	[ImageName2] [nvarchar](50)  NULL,
	[ImageName3] [nvarchar](50)  NULL,
	[ImageName4] [nvarchar](50)  NULL
GO
ALTER TABLE KetQuaSieuAm
ADD [ImageName1] [nvarchar](50)  NULL,
	[ImageName2] [nvarchar](50)  NULL
GO
ALTER TABLE KetQuaSoiCTC
ADD [ImageName1] [nvarchar](50)  NULL,
	[ImageName2] [nvarchar](50)  NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[KetQuaNoiSoiView]
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
                       dbo.KetQuaNoiSoi.OngTaiPhai, dbo.KetQuaNoiSoi.MomMocTrai, dbo.KetQuaNoiSoi.MomMocPhai, dbo.KetQuaNoiSoi.ImageName1, dbo.KetQuaNoiSoi.ImageName2, 
                      dbo.KetQuaNoiSoi.ImageName3, dbo.KetQuaNoiSoi.ImageName4
FROM         dbo.KetQuaNoiSoi INNER JOIN
                      dbo.BacSiNoiSoiView ON dbo.KetQuaNoiSoi.BacSiSoi = dbo.BacSiNoiSoiView.DocStaffGUID LEFT OUTER JOIN
                      dbo.BacSiChiDinhView ON dbo.KetQuaNoiSoi.BacSiChiDinh = dbo.BacSiChiDinhView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[KetQuaSieuAmView]
AS
SELECT     dbo.KetQuaSieuAm.KetQuaSieuAmGUID, dbo.KetQuaSieuAm.PatientGUID, dbo.KetQuaSieuAm.BacSiSieuAmGUID, dbo.KetQuaSieuAm.BacSiChiDinhGUID, 
                      dbo.KetQuaSieuAm.LoaiSieuAmGUID, dbo.KetQuaSieuAm.NgaySieuAm, dbo.KetQuaSieuAm.LamSang, dbo.KetQuaSieuAm.KetQuaSieuAm, dbo.KetQuaSieuAm.Hinh1, 
                      dbo.KetQuaSieuAm.Hinh2, dbo.KetQuaSieuAm.CreatedDate, dbo.KetQuaSieuAm.CreatedBy, dbo.KetQuaSieuAm.UpdatedDate, dbo.KetQuaSieuAm.UpdatedBy, 
                      dbo.KetQuaSieuAm.DeletedDate, dbo.KetQuaSieuAm.DeletedBy, dbo.KetQuaSieuAm.Status, dbo.LoaiSieuAm.TenSieuAm, 
                      dbo.LoaiSieuAm.Status AS LoaiSieuAmStatus, dbo.PatientView.FullName, dbo.PatientView.DobStr, dbo.PatientView.FileNum, dbo.PatientView.Address, 
                      dbo.PatientView.GenderAsStr, dbo.PatientView.Archived AS PatientArchived, dbo.DocStaffView.FullName AS BacSiSieuAm, 
                      dbo.DocStaffView.Archived AS BacSiSieuAmArchived, DocStaffView_1.FullName AS BacSiChiDinh, DocStaffView_1.Archived AS BacSiChiDinhArchived, 
                      dbo.LoaiSieuAm.ThuTu, dbo.LoaiSieuAm.InTrang2, dbo.KetQuaSieuAm.ImageName2, dbo.KetQuaSieuAm.ImageName1
FROM         dbo.KetQuaSieuAm INNER JOIN
                      dbo.LoaiSieuAm ON dbo.KetQuaSieuAm.LoaiSieuAmGUID = dbo.LoaiSieuAm.LoaiSieuAmGUID INNER JOIN
                      dbo.PatientView ON dbo.KetQuaSieuAm.PatientGUID = dbo.PatientView.PatientGUID INNER JOIN
                      dbo.DocStaffView ON dbo.KetQuaSieuAm.BacSiSieuAmGUID = dbo.DocStaffView.DocStaffGUID LEFT OUTER JOIN
                      dbo.DocStaffView AS DocStaffView_1 ON dbo.KetQuaSieuAm.BacSiChiDinhGUID = DocStaffView_1.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[KetQuaSoiCTCView]
AS
SELECT     dbo.KetQuaSoiCTC.KetQuaSoiCTCGUID, dbo.KetQuaSoiCTC.PatientGUID, dbo.KetQuaSoiCTC.BacSiSoi, dbo.KetQuaSoiCTC.NgayKham, dbo.KetQuaSoiCTC.KetLuan, 
                      dbo.KetQuaSoiCTC.DeNghi, dbo.KetQuaSoiCTC.Hinh1, dbo.KetQuaSoiCTC.Hinh2, dbo.KetQuaSoiCTC.AmHo, dbo.KetQuaSoiCTC.AmDao, dbo.KetQuaSoiCTC.CTC, 
                      dbo.KetQuaSoiCTC.BieuMoLat, dbo.KetQuaSoiCTC.MoDem, dbo.KetQuaSoiCTC.RanhGioiLatTru, dbo.KetQuaSoiCTC.SauAcidAcetic, dbo.KetQuaSoiCTC.SauLugol, 
                      dbo.KetQuaSoiCTC.CreatedDate, dbo.KetQuaSoiCTC.CreatedBy, dbo.KetQuaSoiCTC.UpdatedDate, dbo.KetQuaSoiCTC.UpdatedBy, dbo.KetQuaSoiCTC.DeletedDate, 
                      dbo.KetQuaSoiCTC.DeletedBy, dbo.KetQuaSoiCTC.Status, dbo.DocStaffView.FullName, dbo.DocStaffView.DobStr, dbo.DocStaffView.GenderAsStr, 
                      dbo.DocStaffView.Archived, dbo.KetQuaSoiCTC.ImageName1, dbo.KetQuaSoiCTC.ImageName2
FROM         dbo.KetQuaSoiCTC INNER JOIN
                      dbo.DocStaffView ON dbo.KetQuaSoiCTC.BacSiSoi = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO











