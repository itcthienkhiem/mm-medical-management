USE MM
GO
ALTER TABLE Booking 
ALTER COLUMN [InOut] [nvarchar](255) NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[BookingView]
AS
SELECT     dbo.Booking.BookingGUID, dbo.Booking.BookingDate, dbo.Booking.Company, dbo.Booking.MorningCount, dbo.Booking.AfternoonCount, dbo.Booking.EveningCount, 
                      dbo.Booking.Pax, dbo.Booking.BookingType, dbo.Booking.CreatedDate, dbo.Booking.CreatedBy, dbo.Booking.UpdatedDate, dbo.Booking.UpdatedBy, 
                      dbo.Booking.DeletedDate, dbo.Booking.DeletedBy, dbo.Booking.Status, ISNULL(dbo.DocStaffView.FullName, 'Admin') AS Sales, dbo.DocStaffView.Archived, 
                      dbo.Booking.InOut
FROM         dbo.Booking LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.Booking.CreatedBy = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
