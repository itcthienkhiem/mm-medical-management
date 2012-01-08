USE MM
GO
ALTER TABLE Services
ADD EnglishName nvarchar(200) NULL
GO
ALTER TABLE CanDo
ADD [MuMau] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MatTrai] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MatPhai] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HieuChinh] [bit] NOT NULL CONSTRAINT [DF_CanDo_HieuChinh]  DEFAULT ((0))
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ServiceHistoryView]
AS
SELECT     dbo.ServiceHistory.PatientGUID, dbo.DocStaff.DocStaffGUID, dbo.ServiceHistory.Price AS FixedPrice, dbo.ServiceHistory.Note, dbo.Services.ServiceGUID, 
                      dbo.Services.Code, dbo.Services.Name, dbo.Services.Price, dbo.ServiceHistory.CreatedDate, dbo.ServiceHistory.CreatedBy, dbo.ServiceHistory.UpdatedDate, 
                      dbo.ServiceHistory.UpdatedBy, dbo.ServiceHistory.DeletedDate, dbo.ServiceHistory.DeletedBy, dbo.DocStaff.AvailableToWork, 
                      dbo.ServiceHistory.ServiceHistoryGUID, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS CreatedName, dbo.ServiceHistory.Status, dbo.ServiceHistory.ActivedDate, 
                      dbo.Contact.FullName, dbo.ServiceHistory.IsExported, dbo.ServiceHistory.Discount, dbo.Services.EnglishName
FROM         dbo.Contact INNER JOIN
                      dbo.DocStaff ON dbo.Contact.ContactGUID = dbo.DocStaff.ContactGUID INNER JOIN
                      dbo.ServiceHistory ON dbo.DocStaff.DocStaffGUID = dbo.ServiceHistory.DocStaffGUID INNER JOIN
                      dbo.Services ON dbo.ServiceHistory.ServiceGUID = dbo.Services.ServiceGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.ServiceHistory.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ChiTietChiDinhView]
AS
SELECT     dbo.ChiTietChiDinh.ChiTietChiDinhGUID, dbo.ChiTietChiDinh.ChiDinhGUID, dbo.ChiTietChiDinh.ServiceGUID, dbo.ChiTietChiDinh.CreatedDate, 
                      dbo.ChiTietChiDinh.CreatedBy, dbo.ChiTietChiDinh.UpdatedDate, dbo.ChiTietChiDinh.UpdatedBy, dbo.ChiTietChiDinh.DeletedDate, dbo.ChiTietChiDinh.DeletedBy, 
                      dbo.ChiTietChiDinh.Status AS CTCDStatus, dbo.Services.Status AS ServiceStatus, dbo.Services.Code, dbo.Services.Name, dbo.Services.Price, 
                      dbo.Services.EnglishName
FROM         dbo.ChiTietChiDinh INNER JOIN
                      dbo.Services ON dbo.ChiTietChiDinh.ServiceGUID = dbo.Services.ServiceGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[CompanyCheckListView]
AS
SELECT     dbo.CompanyCheckList.CompanyCheckListGUID, dbo.CompanyCheckList.ContractMemberGUID, dbo.CompanyCheckList.ServiceGUID, 
                      dbo.CompanyCheckList.CreatedDate, dbo.CompanyCheckList.CreatedBy, dbo.CompanyCheckList.UpdatedDate, dbo.CompanyCheckList.UpdatedBy, 
                      dbo.CompanyCheckList.DeletedDate, dbo.CompanyCheckList.DeletedBy, dbo.CompanyCheckList.Status AS CheckListStatus, dbo.Services.Code, dbo.Services.Name, 
                      dbo.Services.Price, dbo.Services.Status AS ServiceStatus, dbo.Services.EnglishName
FROM         dbo.CompanyCheckList INNER JOIN
                      dbo.Services ON dbo.CompanyCheckList.ServiceGUID = dbo.Services.ServiceGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ChiTietChiDinhView]
AS
SELECT     dbo.ChiTietChiDinh.ChiTietChiDinhGUID, dbo.ChiTietChiDinh.ChiDinhGUID, dbo.ChiTietChiDinh.ServiceGUID, dbo.ChiTietChiDinh.CreatedDate, 
                      dbo.ChiTietChiDinh.CreatedBy, dbo.ChiTietChiDinh.UpdatedDate, dbo.ChiTietChiDinh.UpdatedBy, dbo.ChiTietChiDinh.DeletedDate, dbo.ChiTietChiDinh.DeletedBy, 
                      dbo.ChiTietChiDinh.Status AS CTCDStatus, dbo.Services.Status AS ServiceStatus, dbo.Services.Code, dbo.Services.Name, dbo.Services.Price, 
                      dbo.Services.EnglishName
FROM         dbo.ChiTietChiDinh INNER JOIN
                      dbo.Services ON dbo.ChiTietChiDinh.ServiceGUID = dbo.Services.ServiceGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO