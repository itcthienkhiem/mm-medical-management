USE MM
GO
ALTER TABLE CompanyContract
ADD [SoTien] [float] NOT NULL DEFAULT ((0))
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[CompanyContractView]
AS
SELECT     dbo.Company.CompanyGUID, dbo.Company.MaCty, dbo.Company.TenCty, dbo.Company.DiaChi, dbo.Company.Dienthoai, dbo.Company.Fax, 
                      dbo.Company.Website, dbo.CompanyContract.CompanyContractGUID, dbo.CompanyContract.ContractName, dbo.CompanyContract.Completed, 
                      dbo.CompanyContract.CreatedDate, dbo.CompanyContract.CreatedBy, dbo.CompanyContract.UpdatedDate, dbo.CompanyContract.UpdatedBy, 
                      dbo.CompanyContract.DeletedDate, dbo.CompanyContract.DeletedBy, dbo.CompanyContract.Status AS ContractStatus, 
                      dbo.Company.Status AS CompanyStatus, dbo.CompanyContract.BeginDate, dbo.CompanyContract.ContractCode, dbo.CompanyContract.EndDate, 
                      ISNULL(dbo.Lock.Status, 0) AS Lock, dbo.CompanyContract.SoTien
FROM         dbo.Company INNER JOIN
                      dbo.CompanyContract ON dbo.Company.CompanyGUID = dbo.CompanyContract.CompanyGUID LEFT OUTER JOIN
                      dbo.Lock ON dbo.CompanyContract.CompanyContractGUID = dbo.Lock.KeyGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER TABLE BenhNhanNgoaiGoiKham
ADD [HopDongGUID] [uniqueidentifier] NULL
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[BenhNhanNgoaiGoiKhamView]
AS
SELECT     dbo.BenhNhanNgoaiGoiKham.BenhNhanNgoaiGoiKhamGUID, dbo.BenhNhanNgoaiGoiKham.NgayKham, dbo.BenhNhanNgoaiGoiKham.PatientGUID, 
                      dbo.BenhNhanNgoaiGoiKham.ServiceGUID, dbo.BenhNhanNgoaiGoiKham.LanDau, dbo.BenhNhanNgoaiGoiKham.CreatedDate, 
                      dbo.BenhNhanNgoaiGoiKham.CreatedBy, dbo.BenhNhanNgoaiGoiKham.UpdatedDate, dbo.BenhNhanNgoaiGoiKham.UpdatedBy, 
                      dbo.BenhNhanNgoaiGoiKham.DeletedDate, dbo.BenhNhanNgoaiGoiKham.DeletedBy, dbo.BenhNhanNgoaiGoiKham.Status, dbo.Services.Code, 
                      dbo.Services.Name, dbo.Services.EnglishName, dbo.PatientView.FullName, dbo.PatientView.DobStr, dbo.PatientView.GenderAsStr, 
                      dbo.PatientView.Address, dbo.PatientView.FileNum, dbo.PatientView.Mobile, dbo.PatientView.Email, dbo.Services.Status AS ServiceStatus, 
                      dbo.PatientView.Archived, CASE dbo.BenhNhanNgoaiGoiKham.LanDau WHEN 0 THEN N'Lần đầu' ELSE N'Tái khám' END AS LanDauStr, 
                      CASE dbo.BenhNhanNgoaiGoiKham.CreatedBy WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE DocStaffView.FullName END AS NguoiTao,
                       dbo.BenhNhanNgoaiGoiKham.HopDongGUID, dbo.CompanyContract.ContractName, dbo.CompanyContract.ContractCode, 
                      dbo.CompanyContract.BeginDate, dbo.CompanyContract.EndDate, dbo.CompanyContract.Completed, dbo.CompanyContract.SoTien
FROM         dbo.BenhNhanNgoaiGoiKham INNER JOIN
                      dbo.Services ON dbo.BenhNhanNgoaiGoiKham.ServiceGUID = dbo.Services.ServiceGUID INNER JOIN
                      dbo.PatientView ON dbo.BenhNhanNgoaiGoiKham.PatientGUID = dbo.PatientView.PatientGUID LEFT OUTER JOIN
                      dbo.CompanyContract ON dbo.BenhNhanNgoaiGoiKham.HopDongGUID = dbo.CompanyContract.CompanyContractGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.BenhNhanNgoaiGoiKham.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



