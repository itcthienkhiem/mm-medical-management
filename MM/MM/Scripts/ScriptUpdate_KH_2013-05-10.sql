USE MM
GO
GO
/****** Object:  StoredProcedure [dbo].[spUpdateGiaHopDong2ServiceHistory]    Script Date: 05/10/2013 09:44:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateGiaHopDong2ServiceHistory]
	@ContractGUID nvarchar(50),
	@ServiceGUID nvarchar(50),
	@Gia float
AS
BEGIN
	SELECT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, EndDate,
	C.Completed, M.PatientGUID
	INTO TMP
	FROM CompanyContract C WITH(NOLOCK), CompanyCheckList L WITH(NOLOCK), CompanyMemberView M WITH(NOLOCK),
	ContractMember CM WITH(NOLOCK)
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	CM.Status = 0 AND
	L.Status = 0 AND C.CompanyContractGUID = @ContractGUID AND
	L.ServiceGUID = @ServiceGUID AND
	M.Status = 0 AND CM.Status = 0 AND M.Archived = 'False'

	UPDATE ServiceHistory
	SET Price = @Gia
	FROM TMP
	WHERE ServiceHistory.ServiceGUID = TMP.ServiceGUID AND
	ServiceHistory.IsExported = 'False' AND ServiceHistory.KhamTuTuc = 'False' AND
	ServiceHistory.PatientGUID = TMP.PatientGUID AND ServiceHistory.Status = 0 AND
	(TMP.Completed = 'False' AND ServiceHistory.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND ServiceHistory.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	UPDATE ServiceHistory
	SET Price = @Gia
	FROM TMP, PatientView P
	WHERE ServiceHistory.ServiceGUID = TMP.ServiceGUID AND
	ServiceHistory.PatientGUID = P.PatientGUID AND
	ServiceHistory.IsExported = 'False' AND ServiceHistory.KhamTuTuc = 'True' AND
	ServiceHistory.RootPatientGUID = TMP.PatientGUID AND ServiceHistory.Status = 0 AND
	(TMP.Completed = 'False' AND ServiceHistory.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND ServiceHistory.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	DROP TABLE TMP
END



