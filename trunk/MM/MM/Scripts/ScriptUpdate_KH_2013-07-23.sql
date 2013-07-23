USE MM
GO
ALTER TABLE [Tracking]
ADD [ComputerName] [nvarchar](255) NULL
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[TrackingView]
AS
SELECT     dbo.Tracking.TrackingGUID, dbo.Tracking.TrackingDate, dbo.Tracking.DocStaffGUID, dbo.Tracking.ActionType, dbo.Tracking.Description, dbo.DocStaffView.FirstName, 
                      dbo.DocStaffView.SurName, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS FullName, dbo.DocStaffView.GenderAsStr, dbo.DocStaffView.Address, 
                      dbo.DocStaffView.DobStr, dbo.DocStaffView.Status, dbo.DocStaffView.AvailableToWork, dbo.DocStaffView.Archived, dbo.Tracking.Action, dbo.Tracking.TrackingType, 
                      CASE ActionType WHEN 0 THEN N'Thêm' WHEN 1 THEN N'Sửa' WHEN 2 THEN N'Xóa' END AS ActionTypeStr, dbo.Tracking.ComputerName
FROM         dbo.Tracking LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.Tracking.DocStaffGUID = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO







