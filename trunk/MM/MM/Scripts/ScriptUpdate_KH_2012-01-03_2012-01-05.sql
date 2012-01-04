USE MM
GO
/****** Object:  Table [dbo].[Tracking]    Script Date: 01/04/2012 11:26:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tracking](
	[TrackingGUID] [uniqueidentifier] NOT NULL,
	[TrackingDate] [datetime] NOT NULL,
	[DocStaffGUID] [uniqueidentifier] NOT NULL,
	[ActionType] [tinyint] NOT NULL,
	[Action] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TrackingType] [tinyint] NULL,
 CONSTRAINT [PK_Tracking] PRIMARY KEY CLUSTERED 
(
	[TrackingGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[TrackingView]    Script Date: 01/04/2012 11:26:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TrackingView]
AS
SELECT     dbo.Tracking.TrackingGUID, dbo.Tracking.TrackingDate, dbo.Tracking.DocStaffGUID, dbo.Tracking.ActionType, dbo.Tracking.Description, 
                      dbo.DocStaffView.FirstName, dbo.DocStaffView.SurName, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS FullName, dbo.DocStaffView.GenderAsStr, 
                      dbo.DocStaffView.Address, dbo.DocStaffView.DobStr, dbo.DocStaffView.Status, dbo.DocStaffView.AvailableToWork, dbo.DocStaffView.Archived, 
                      dbo.Tracking.Action, dbo.Tracking.TrackingType
FROM         dbo.Tracking LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.Tracking.DocStaffGUID = dbo.DocStaffView.DocStaffGUID

GO