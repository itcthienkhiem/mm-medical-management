USE MM
GO
ALTER TABLE Services
ADD EnglishName nvarchar(200) NULL,
	[Type] [tinyint] NOT NULL CONSTRAINT [DF_Services_Type]  DEFAULT ((0))
GO
ALTER TABLE CanDo
ADD [MuMau] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MatTrai] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MatPhai] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HieuChinh] [bit] NOT NULL CONSTRAINT [DF_CanDo_HieuChinh]  DEFAULT ((0)),
	[DocStaffGUID] [uniqueidentifier] NOT NULL
GO
ALTER TABLE [dbo].[CanDo]  WITH CHECK ADD  CONSTRAINT [FK_CanDo_DocStaff] FOREIGN KEY([DocStaffGUID])
REFERENCES [dbo].[DocStaff] ([DocStaffGUID])
GO
ALTER TABLE [dbo].[CanDo] CHECK CONSTRAINT [FK_CanDo_DocStaff]
GO
ALTER TABLE ServiceHistory
ADD [IsNormalOrNegative] [bit] NOT NULL CONSTRAINT [DF_ServiceHistory_IsNormalOrNegative]  DEFAULT ((1)),
	[Normal] [bit] NOT NULL CONSTRAINT [DF_ServiceHistory_Normal]  DEFAULT ((0)),
	[Abnormal] [bit] NOT NULL CONSTRAINT [DF_ServiceHistory_Abnormal]  DEFAULT ((0)),
	[Negative] [bit] NOT NULL CONSTRAINT [DF_ServiceHistory_Negative]  DEFAULT ((0)),
	[Positive] [bit] NOT NULL CONSTRAINT [DF_ServiceHistory_Positive]  DEFAULT ((0))
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ServiceHistoryView]
AS
SELECT     dbo.ServiceHistory.PatientGUID, dbo.DocStaff.DocStaffGUID, dbo.ServiceHistory.Price AS FixedPrice, dbo.ServiceHistory.Note, 
                      dbo.Services.ServiceGUID, dbo.Services.Code, dbo.Services.Name, dbo.Services.Price, dbo.ServiceHistory.CreatedDate, 
                      dbo.ServiceHistory.CreatedBy, dbo.ServiceHistory.UpdatedDate, dbo.ServiceHistory.UpdatedBy, dbo.ServiceHistory.DeletedDate, 
                      dbo.ServiceHistory.DeletedBy, dbo.DocStaff.AvailableToWork, dbo.ServiceHistory.ServiceHistoryGUID, ISNULL(dbo.DocStaffView.FullName, 'Admin') 
                      AS CreatedName, dbo.ServiceHistory.Status, dbo.ServiceHistory.ActivedDate, dbo.Contact.FullName, dbo.ServiceHistory.IsExported, 
                      dbo.ServiceHistory.Discount, dbo.ServiceHistory.IsNormalOrNegative, dbo.ServiceHistory.Normal, dbo.ServiceHistory.Abnormal, 
                      dbo.ServiceHistory.Negative, dbo.ServiceHistory.Positive
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
                      dbo.ChiTietChiDinh.CreatedBy, dbo.ChiTietChiDinh.UpdatedDate, dbo.ChiTietChiDinh.UpdatedBy, dbo.ChiTietChiDinh.DeletedDate, 
                      dbo.ChiTietChiDinh.DeletedBy, dbo.ChiTietChiDinh.Status AS CTCDStatus, dbo.Services.Status AS ServiceStatus, dbo.Services.Code, 
                      dbo.Services.Name, dbo.Services.Price, dbo.Services.EnglishName, dbo.Services.Type
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

ALTER VIEW [dbo].[DichVuChiDinhView]
AS
SELECT     dbo.DichVuChiDinh.DichVuChiDinhGUID, dbo.DichVuChiDinh.ChiTietChiDinhGUID, dbo.DichVuChiDinh.ServiceHistoryGUID, 
                      dbo.ServiceHistoryView.Name, dbo.ServiceHistoryView.Price, dbo.ServiceHistoryView.Code, dbo.ServiceHistoryView.ServiceGUID, 
                      dbo.ServiceHistoryView.Status, dbo.ServiceHistoryView.Type, dbo.ServiceHistoryView.EnglishName
FROM         dbo.DichVuChiDinh INNER JOIN
                      dbo.ServiceHistoryView ON dbo.DichVuChiDinh.ServiceHistoryGUID = dbo.ServiceHistoryView.ServiceHistoryGUID
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
                      dbo.CompanyCheckList.DeletedDate, dbo.CompanyCheckList.DeletedBy, dbo.CompanyCheckList.Status AS CheckListStatus, dbo.Services.Code, 
                      dbo.Services.Name, dbo.Services.Price, dbo.Services.Status AS ServiceStatus, dbo.Services.EnglishName, dbo.Services.Type
FROM         dbo.CompanyCheckList INNER JOIN
                      dbo.Services ON dbo.CompanyCheckList.ServiceGUID = dbo.Services.ServiceGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

/****** Object:  View [dbo].[CanDoView]    Script Date: 01/09/2012 11:02:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CanDoView]
AS
SELECT     dbo.CanDo.CanDoGuid, dbo.CanDo.PatientGUID, dbo.CanDo.DocStaffGUID, dbo.CanDo.NgayCanDo, dbo.CanDo.TimMach, dbo.CanDo.HuyetAp, 
                      dbo.CanDo.HoHap, dbo.CanDo.ChieuCao, dbo.CanDo.CanNang, dbo.CanDo.BMI, dbo.CanDo.CanDoKhac, dbo.CanDo.CreatedDate, 
                      dbo.CanDo.CreatedBy, dbo.CanDo.UpdatedDate, dbo.CanDo.UpdatedBy, dbo.CanDo.DeletedDate, dbo.CanDo.DeletedBy, dbo.CanDo.Status, 
                      dbo.CanDo.MuMau, dbo.CanDo.MatTrai, dbo.CanDo.MatPhai, dbo.CanDo.HieuChinh, dbo.DocStaffView.FullName, dbo.DocStaffView.DobStr, 
                      dbo.DocStaffView.GenderAsStr, dbo.DocStaffView.Archived, dbo.DocStaffView.FirstName, dbo.DocStaffView.SurName, dbo.DocStaffView.Mobile, 
                      dbo.DocStaffView.WorkPhone, dbo.DocStaffView.HomePhone, dbo.DocStaffView.Email, dbo.DocStaffView.Address, dbo.DocStaffView.StaffType
FROM         dbo.CanDo INNER JOIN
                      dbo.DocStaffView ON dbo.CanDo.DocStaffGUID = dbo.DocStaffView.DocStaffGUID

GO
/****** Object:  Table [dbo].[ServiceGroup]    Script Date: 01/09/2012 20:35:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceGroup](
	[ServiceGroupGUID] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Name] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EnglishName] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ServiceGroup_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ServiceGroup] PRIMARY KEY CLUSTERED 
(
	[ServiceGroupGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service_ServiceGroup]    Script Date: 01/09/2012 17:00:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service_ServiceGroup](
	[Service_GroupServiceGUID] [uniqueidentifier] NOT NULL,
	[ServiceGroupGUID] [uniqueidentifier] NOT NULL,
	[ServiceGUID] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_Service_ServiceGroup_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_Service_ServiceGroup] PRIMARY KEY CLUSTERED 
(
	[Service_GroupServiceGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Service_ServiceGroup]  WITH CHECK ADD  CONSTRAINT [FK_Service_ServiceGroup_ServiceGroup] FOREIGN KEY([ServiceGroupGUID])
REFERENCES [dbo].[ServiceGroup] ([ServiceGroupGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Service_ServiceGroup] CHECK CONSTRAINT [FK_Service_ServiceGroup_ServiceGroup]
GO
ALTER TABLE [dbo].[Service_ServiceGroup]  WITH CHECK ADD  CONSTRAINT [FK_Service_ServiceGroup_Services] FOREIGN KEY([ServiceGUID])
REFERENCES [dbo].[Services] ([ServiceGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Service_ServiceGroup] CHECK CONSTRAINT [FK_Service_ServiceGroup_Services]
GO
/****** Object:  View [dbo].[Service_ServiceGroupView]    Script Date: 01/09/2012 17:04:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Service_ServiceGroupView]
AS
SELECT     dbo.Service_ServiceGroup.Service_GroupServiceGUID, dbo.Service_ServiceGroup.ServiceGroupGUID, dbo.Service_ServiceGroup.ServiceGUID, 
                      dbo.Service_ServiceGroup.CreatedDate, dbo.Service_ServiceGroup.UpdatedDate, dbo.Service_ServiceGroup.CreatedBy, 
                      dbo.Service_ServiceGroup.UpdatedBy, dbo.Service_ServiceGroup.DeletedDate, dbo.Service_ServiceGroup.DeletedBy, 
                      dbo.Service_ServiceGroup.Status AS Service_ServiceGroupStatus, dbo.Services.Code, dbo.Services.Name, dbo.Services.EnglishName, 
                      dbo.Services.Price, dbo.Services.Type, dbo.Services.Description, dbo.Services.Status AS ServiceStatus, 
                      dbo.ServiceGroup.Status AS ServiceGroupStatus
FROM         dbo.Service_ServiceGroup INNER JOIN
                      dbo.ServiceGroup ON dbo.Service_ServiceGroup.ServiceGroupGUID = dbo.ServiceGroup.ServiceGroupGUID INNER JOIN
                      dbo.Services ON dbo.Service_ServiceGroup.ServiceGUID = dbo.Services.ServiceGUID

GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'508e6cd8-d1ba-4601-9930-6b4c206df43d', N'ServiceGroup', N'Nhóm dịch vụ')
GO