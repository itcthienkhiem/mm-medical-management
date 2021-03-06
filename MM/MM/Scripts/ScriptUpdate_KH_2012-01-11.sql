USE MM
GO
ALTER TABLE KetQuaLamSang
ADD [PARA] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SoiTuoiHuyetTrang] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NgayKinhChot] [datetime] NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[KetQuaLamSangView]
AS
SELECT     dbo.KetQuaLamSang.KetQuaLamSangGUID, dbo.KetQuaLamSang.NgayKham, dbo.KetQuaLamSang.PatientGUID, dbo.KetQuaLamSang.DocStaffGUID, 
                      dbo.KetQuaLamSang.CoQuan, dbo.KetQuaLamSang.Normal, dbo.KetQuaLamSang.Abnormal, dbo.KetQuaLamSang.Note, 
                      dbo.KetQuaLamSang.CreatedDate, dbo.KetQuaLamSang.CreatedBy, dbo.KetQuaLamSang.UpdatedDate, dbo.KetQuaLamSang.UpdatedBy, 
                      dbo.KetQuaLamSang.DeletedDate, dbo.KetQuaLamSang.DeletedBy, dbo.KetQuaLamSang.Status, dbo.DocStaffView.FullName, 
                      dbo.DocStaffView.FirstName, dbo.DocStaffView.SurName, dbo.DocStaffView.Archived, 
                      CASE CoQuan WHEN 0 THEN N'Eyes (Mắt)' WHEN 1 THEN N'Ear, Nose, Throat (Tai, mũi, họng)' WHEN 2 THEN N'Odontology (Răng, hàm, mặt)' WHEN 3 THEN
                       N'Respiratory system (Hô hấp)' WHEN 4 THEN N'Cardiovascular system (Tim mạch)' WHEN 5 THEN N'Gastro - intestinal system (Tiêu hóa)' WHEN 6 THEN
                       N'Genitourinary system (Tiết niệu, sinh dục)' WHEN 7 THEN N'Musculoskeletal system (Cơ, xương, khớp)' WHEN 8 THEN N'Dermatology (Da liễu)' WHEN 9 THEN
                       N'Neurological system (Thần kinh)' WHEN 10 THEN N'Endocrine system (Nội tiết)' WHEN 11 THEN N'Orthers (Các cơ quan khác)' WHEN 12 THEN N'Gynecology (Khám phụ khoa)' END AS CoQuanStr, 
                      dbo.KetQuaLamSang.PARA, dbo.KetQuaLamSang.SoiTuoiHuyetTrang, dbo.KetQuaLamSang.NgayKinhChot
FROM         dbo.KetQuaLamSang INNER JOIN
                      dbo.DocStaffView ON dbo.KetQuaLamSang.DocStaffGUID = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ReceiptView]
AS
SELECT     dbo.PatientView.FullName, dbo.PatientView.FileNum, dbo.PatientView.Address, dbo.Receipt.ReceiptGUID, dbo.Receipt.PatientGUID, dbo.Receipt.ReceiptDate, 
                      dbo.Receipt.CreatedDate, dbo.Receipt.CreatedBy, dbo.Receipt.UpdatedDate, dbo.Receipt.UpdatedBy, dbo.Receipt.DeletedDate, dbo.Receipt.DeletedBy, 
                      dbo.Receipt.Status, dbo.Receipt.ReceiptCode, dbo.Receipt.IsExportedInVoice, dbo.PatientView.CompanyName
FROM         dbo.Receipt INNER JOIN
                      dbo.PatientView ON dbo.Receipt.PatientGUID = dbo.PatientView.PatientGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO