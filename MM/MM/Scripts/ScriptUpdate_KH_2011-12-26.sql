USE [MM]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[CompanyMemberView]
AS
SELECT     dbo.CompanyMember.CompanyMemberGUID, dbo.CompanyMember.CompanyGUID, dbo.CompanyMember.PatientGUID, 
                      dbo.CompanyMember.CreatedDate, dbo.CompanyMember.CreatedBy, dbo.CompanyMember.UpdatedDate, dbo.CompanyMember.UpdatedBy, 
                      dbo.CompanyMember.DeletedDate, dbo.CompanyMember.DeletedBy, dbo.PatientView.FullName, dbo.PatientView.DobStr, dbo.PatientView.Occupation, 
                      dbo.PatientView.IdentityCard, dbo.PatientView.HomePhone, dbo.PatientView.WorkPhone, dbo.PatientView.Mobile, dbo.PatientView.Email, 
                      dbo.PatientView.FAX, dbo.PatientView.GenderAsStr, dbo.PatientView.FileNum, dbo.PatientView.Address, dbo.CompanyMember.Status, 
                      dbo.PatientView.Archived, dbo.PatientView.Source, dbo.PatientView.FirstName, dbo.PatientView.SurName
FROM         dbo.CompanyMember INNER JOIN
                      dbo.PatientView ON dbo.CompanyMember.PatientGUID = dbo.PatientView.PatientGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ContractMemberView]
AS
SELECT     dbo.ContractMember.ContractMemberGUID, dbo.ContractMember.CompanyMemberGUID, dbo.ContractMember.CompanyContractGUID, 
                      dbo.ContractMember.CreatedDate, dbo.ContractMember.CreatedBy, dbo.ContractMember.UpdatedBy, dbo.ContractMember.UpdatedDate, 
                      dbo.ContractMember.DeletedDate, dbo.ContractMember.DeletedBy, dbo.ContractMember.Status, dbo.CompanyMemberView.PatientGUID, 
                      dbo.CompanyMemberView.CompanyGUID, dbo.CompanyMemberView.FullName, dbo.CompanyMemberView.FileNum, 
                      dbo.CompanyMemberView.Address, dbo.CompanyMemberView.GenderAsStr, dbo.CompanyMemberView.FAX, dbo.CompanyMemberView.Email, 
                      dbo.CompanyMemberView.Mobile, dbo.CompanyMemberView.WorkPhone, dbo.CompanyMemberView.HomePhone, 
                      dbo.CompanyMemberView.IdentityCard, dbo.CompanyMemberView.DobStr, dbo.CompanyMemberView.Occupation, 
                      dbo.CompanyMemberView.Status AS CompanyMemberStatus, dbo.CompanyMemberView.Source, dbo.CompanyMemberView.Archived, 
                      dbo.CompanyMemberView.FirstName, dbo.CompanyMemberView.SurName
FROM         dbo.ContractMember INNER JOIN
                      dbo.CompanyMemberView ON dbo.ContractMember.CompanyMemberGUID = dbo.CompanyMemberView.CompanyMemberGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
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
                      WHEN 0 THEN N'Bác sĩ' WHEN 1 THEN N'Y tá' WHEN 2 THEN N'Lễ tân' WHEN 4 THEN N'Admin' END AS StaffTypeStr, dbo.Logon.CreatedDate, 
                      dbo.Logon.CreatedBy, dbo.Logon.UpdatedDate, dbo.Logon.UpdatedBy, dbo.Logon.DeletedDate, dbo.Logon.DeletedBy, dbo.Contact.FirstName, 
                      dbo.Contact.SurName
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

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spMerge2Patients] 
(
	@KeepGUID nvarchar(50),
	@MergedGUID nvarchar(50),
	@DoneByGUID nvarchar(50)
)
AS
BEGIN TRANSACTION MergePatient
	--update CompanuMember
    UPDATE dbo.CompanyMember set PatientGUID = @KeepGUID  where PatientGUID=@MergedGUID
	IF @@ERROR <> 0
	BEGIN
		-- Rollback the transaction
		ROLLBACK
		RETURN
	END

	--set deleted for 2nd patietn
	Update dbo.Contact Set DeletedDate = GETDATE(),DeletedBy=@DoneByGUID, Note= Note + ' Merged with patient ' + @KeepGUID   where ContactGUID=@MergedGUID
	IF @@ERROR <> 0
		BEGIN
		-- Rollback the transaction
		ROLLBACK
		RETURN
	END
	
	Update dbo.ContractMember Set CompanyMemberGUID= @KeepGUID  where CompanyMemberGUID=@MergedGUID
	IF @@ERROR <> 0
		BEGIN
		-- Rollback the transaction
		ROLLBACK
		RETURN
	END
	
	update dbo.Patient set ContactGUID = @KeepGUID where ContactGUID=@MergedGUID
	IF @@ERROR <> 0
	BEGIN
		-- Rollback the transaction
		ROLLBACK
		RETURN
	END
	
	update dbo.PatientHistory set PatientGUID = @KeepGUID where PatientGUID = @MergedGUID
	IF @@ERROR <> 0
	BEGIN
		-- Rollback the transaction
		ROLLBACK
		RETURN
	END
	
	Update dbo.Receipt set PatientGUID = @KeepGUID where PatientGUID = @MergedGUID
	IF @@ERROR <> 0
	BEGIN
		-- Rollback the transaction
		ROLLBACK
		RETURN
	END
	
	Update dbo.ServiceHistory set PatientGUID = @KeepGUID where PatientGUID = @MergedGUID
	IF @@ERROR <> 0
		BEGIN
		-- Rollback the transaction
		ROLLBACK
		RETURN
	END
		
	COMMIT TRANSACTION MergePatient;
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

