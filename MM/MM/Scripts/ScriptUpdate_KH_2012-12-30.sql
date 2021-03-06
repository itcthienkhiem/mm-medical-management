USE MM
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'e0020d22-d0e9-4422-9694-06e615b9dad9', N'GuiSMS', N'Gửi SMS')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'2da07dbe-9be6-490c-941b-9018c32030c8', N'TinNhanMau', N'Tin nhắn mẫu')
GO
ALTER TABLE UserGroup_Permission
ADD [IsSendSMS] [bit] NOT NULL DEFAULT ((0))
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[UserGroup_PermissionView]
AS
SELECT     dbo.[Function].FunctionCode, dbo.[Function].FunctionName, dbo.UserGroup_Permission.UserGroup_PermissionGUID, dbo.UserGroup_Permission.UserGroupGUID, 
                      dbo.UserGroup_Permission.FunctionGUID, dbo.UserGroup_Permission.IsView, dbo.UserGroup_Permission.IsAdd, dbo.UserGroup_Permission.IsEdit, 
                      dbo.UserGroup_Permission.IsDelete, dbo.UserGroup_Permission.IsPrint, dbo.UserGroup_Permission.IsImport, dbo.UserGroup_Permission.IsExport, 
                      dbo.UserGroup_Permission.IsConfirm, dbo.UserGroup_Permission.IsLock, dbo.UserGroup_Permission.IsExportAll, dbo.UserGroup_Permission.IsCreateReport, 
                      dbo.UserGroup_Permission.IsUpload, dbo.UserGroup_Permission.CreatedDate, dbo.UserGroup_Permission.CreatedBy, dbo.UserGroup_Permission.UpdatedDate, 
                      dbo.UserGroup_Permission.UpdatedBy, dbo.UserGroup_Permission.DeletedDate, dbo.UserGroup_Permission.DeletedBy, 
                      dbo.UserGroup_Permission.IsSendSMS
FROM         dbo.UserGroup_Permission INNER JOIN
                      dbo.[Function] ON dbo.UserGroup_Permission.FunctionGUID = dbo.[Function].FunctionGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/****** Object:  Table [dbo].[TinNhanMau]    Script Date: 01/03/2013 10:49:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TinNhanMau](
	[TinNhanMauGUID] [uniqueidentifier] NOT NULL,
	[TieuDe] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NoiDung] [nvarchar](160) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_TinNhanMau_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_TinNhanMau] PRIMARY KEY CLUSTERED 
(
	[TinNhanMauGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
