USE MM
GO
ALTER TABLE [Services]
ADD [StaffType] [tinyint] NULL
GO
/****** Object:  View [dbo].[ServiceView]    Script Date: 01/16/2012 21:31:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ServiceView]
AS
SELECT     ServiceGUID, Code, Name, EnglishName, Price, Description, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, Status, Type, StaffType, 
                      CASE ISNULL(StaffType, 9) 
                      WHEN 0 THEN N'Bác sĩ' WHEN 1 THEN N'Điều dưỡng' WHEN 2 THEN N'Lễ tân' WHEN 4 THEN N'Admin' WHEN 5 THEN N'Xét nghiệm' WHEN 6 THEN N'Thư ký y khoa' WHEN
                       7 THEN N'Sale' WHEN 8 THEN N'Kế toán' WHEN 9 THEN N'' END AS StaffTypeStr
FROM         dbo.Services

GO