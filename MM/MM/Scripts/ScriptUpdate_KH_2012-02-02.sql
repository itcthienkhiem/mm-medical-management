USE MM
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[UserView]
AS
SELECT     ISNULL(dbo.DocStaff.DocStaffGUID, '00000000-0000-0000-0000-000000000000') AS DocStaffGUID, ISNULL(dbo.Contact.FullName, 'Admin') 
                      AS FullName, ISNULL(dbo.DocStaff.AvailableToWork, 'True') AS AvailableToWork, ISNULL(dbo.DocStaff.StaffType, 4) AS StaffType, 
                      ISNULL(dbo.DocStaff.WorkType, 0) AS WorkType, dbo.Logon.LogonGUID, dbo.Logon.Status, dbo.Logon.Password, 
                      CASE ISNULL(dbo.DocStaff.StaffType, 4) 
                      WHEN 0 THEN N'Bác sĩ' WHEN 1 THEN N'Điều dưỡng' WHEN 2 THEN N'Lễ tân' WHEN 4 THEN N'Admin' WHEN 5 THEN N'Xét nghiệm' WHEN 6 THEN N'Thư ký y khoa'
                       WHEN 7 THEN N'Sale' WHEN 8 THEN N'Kế toán' WHEN 10 THEN N'Bác sĩ siêu âm' WHEN 11 THEN N'Bác sĩ ngoại tổng quát' WHEN 12 THEN N'Bác sĩ nội tổng quát'
                       WHEN 13 THEN N'Bác sĩ phụ khoa' END AS StaffTypeStr, dbo.Logon.CreatedDate, dbo.Logon.CreatedBy, dbo.Logon.UpdatedDate, dbo.Logon.UpdatedBy, dbo.Logon.DeletedDate, 
                      dbo.Logon.DeletedBy, dbo.Contact.FirstName, dbo.Contact.SurName
FROM         dbo.DocStaff INNER JOIN
                      dbo.Contact ON dbo.DocStaff.ContactGUID = dbo.Contact.ContactGUID RIGHT OUTER JOIN
                      dbo.Logon ON dbo.DocStaff.DocStaffGUID = dbo.Logon.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ServiceView]
AS
SELECT     ServiceGUID, Code, Name, EnglishName, Price, Description, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, Status, 
                      Type, StaffType, CASE ISNULL(StaffType, 9) 
                      WHEN 0 THEN N'Bác sĩ' WHEN 1 THEN N'Điều dưỡng' WHEN 2 THEN N'Lễ tân' WHEN 4 THEN N'Admin' WHEN 5 THEN N'Xét nghiệm' WHEN 6 THEN N'Thư ký y khoa'
                       WHEN 7 THEN N'Sale' WHEN 8 THEN N'Kế toán' WHEN 9 THEN N'' WHEN 10 THEN N'Bác sĩ siêu âm' WHEN 11 THEN N'Bác sĩ ngoại tổng quát' WHEN
                       12 THEN N'Bác sĩ nội tổng quát' WHEN 13 THEN N'Bác sĩ phụ khoa' END AS StaffTypeStr
FROM         dbo.Services
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

