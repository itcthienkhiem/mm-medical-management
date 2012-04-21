USE MM
GO
ALTER TABLE KetQuaXetNghiem_Hitachi917 
ADD [Sex] [int] NULL,
	[Age] [int] NULL,
	[AgeUnit] [int] NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[KetQuaXetNghiem_Hitachi917View]
AS
SELECT     dbo.KetQuaXetNghiem_Hitachi917.KQXN_Hitachi917GUID, dbo.KetQuaXetNghiem_Hitachi917.IDNum, dbo.KetQuaXetNghiem_Hitachi917.PatientGUID, 
                      dbo.KetQuaXetNghiem_Hitachi917.NgayXN, dbo.KetQuaXetNghiem_Hitachi917.OperationID, dbo.KetQuaXetNghiem_Hitachi917.CreatedDate, 
                      dbo.KetQuaXetNghiem_Hitachi917.CreatedBy, dbo.KetQuaXetNghiem_Hitachi917.UpdatedDate, dbo.KetQuaXetNghiem_Hitachi917.UpdatedBy, 
                      dbo.KetQuaXetNghiem_Hitachi917.DeletedDate, dbo.KetQuaXetNghiem_Hitachi917.DeletedBy, dbo.KetQuaXetNghiem_Hitachi917.Status, dbo.PatientView.FullName, 
                      dbo.PatientView.DobStr, dbo.PatientView.FileNum, dbo.PatientView.GenderAsStr, dbo.PatientView.Archived, dbo.KetQuaXetNghiem_Hitachi917.Sex, 
                      dbo.KetQuaXetNghiem_Hitachi917.Age, dbo.KetQuaXetNghiem_Hitachi917.AgeUnit
FROM         dbo.KetQuaXetNghiem_Hitachi917 LEFT OUTER JOIN
                      dbo.PatientView ON dbo.KetQuaXetNghiem_Hitachi917.PatientGUID = dbo.PatientView.PatientGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO