USE [MM]
GO
ALTER TABLE CompanyContract
ADD EndDate datetime NULL
GO
ALTER TABLE Contact
ADD CompanyName nvarchar(255) NULL
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) 
VALUES (N'28700972-7aca-46dd-b765-243a571a6269', N'DichVuTuTuc', N'Báo cáo dịch vụ tự túc')
GO
/****** Object:  StoredProcedure [dbo].[spDichVuTuTuc]    Script Date: 12/27/2011 14:18:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spDichVuTuTuc]
	@TuNgay dateTime,
	@DenNgay datetime
AS
BEGIN
	SELECT @TuNgay AS TuNgay, @DenNgay AS DenNgay, P.FullName, P.FirstName, 
	Min(S.ActivedDate) AS NgayKham, ISNULL(P.CompanyName, N'Tự túc') AS CompanyName, P.Mobile  
	FROM ServiceHistory S, PatientView P
	WHERE S.PatientGUID = P.PatientGUID AND
	S.ActivedDate BETWEEN @TuNgay AND @DenNgay AND S.Status = 0 AND
	S.Status = 0 AND P.Archived = 'False' AND NOT EXISTS 
	(SELECT TOP 1 L.ServiceGUID
	FROM CompanyContract C, CompanyCheckList L, CompanyMember M,
	ContractMember CM
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	M.PatientGUID = P.PatientGUID AND L.ServiceGUID = S.ServiceGUID AND
	M.Status = 0 AND L.Status = 0 AND CM.Status = 0 AND C.Status = 0 AND
	(C.Completed = 'False' AND C.BeginDate <= S.ActivedDate OR 
	C.Completed = 'True' AND S.ActivedDate BETWEEN C.BeginDate AND C.EndDate))
	GROUP BY P.FullName, P.FirstName, ISNULL(P.CompanyName, N'Tự túc'), P.Mobile
	ORDER BY P.FirstName, P.FullName
END
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
ALTER PROCEDURE [dbo].[spGetCheckList]
	@PatientGUID nvarchar(50)
AS
BEGIN
	SELECT DISTINCT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, EndDate,
	CAST(0 AS bit) AS Checked, C.Completed
	INTO TMP
	FROM CompanyContract C, CompanyCheckList L, CompanyMember M,
	ContractMember CM
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	M.PatientGUID = @PatientGUID AND CM.Status = 0 AND
	M.Status = 0 AND L.Status = 0 AND C.Status = 0 AND
	(C.Completed = 'False' AND C.BeginDate <= GetDate() OR 
	 C.Completed = 'True' AND GetDate() BETWEEN C.BeginDate AND C.EndDate)

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.PatientGUID = @PatientGUID AND S.Status = 0 AND
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	SELECT CompanyGUID, CompanyContractGUID, S.ServiceGUID, Code, [Name], BeginDate, Checked 
	FROM TMP, Services S
	WHERE TMP.ServiceGUID = S.ServiceGUID
	ORDER BY TMP.Checked, S.[Name]

	DROP TABLE TMP
END
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
ALTER PROCEDURE [dbo].[spCheckMember]
	@PatientGUID nvarchar(50),
	@Result int output
AS
BEGIN
	SELECT DISTINCT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, EndDate, 
	CAST(0 AS bit) AS Checked, C.Completed 
	INTO TMP
	FROM CompanyContract C, CompanyCheckList L, CompanyMember M,
	ContractMember CM
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	M.PatientGUID = @PatientGUID AND 
	M.Status = 0 AND L.Status = 0 AND C.Status = 0 AND CM.Status = 0 AND
	(C.Completed = 'False' AND C.BeginDate <= GetDate() OR 
	 C.Completed = 'True' AND GetDate() BETWEEN C.BeginDate AND C.EndDate)

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.PatientGUID = @PatientGUID AND S.Status = 0  AND 
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	DECLARE @ServiceCount int
	DECLARE @UsingCount int
	SET @ServiceCount = 0
	SET @UsingCount = 0

	SET @ServiceCount = (SELECT Count(*) FROM TMP)
	SET @UsingCount = (SELECT Count(*) FROM TMP WHERE Checked = 'True')

	IF (@UsingCount = 0)
		SET @Result = 0
	ELSE IF (@UsingCount < @ServiceCount)
		SET @Result = 1
	ELSE
		SET @Result = 2

	DROP TABLE TMP
END
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
ALTER PROCEDURE [dbo].[spGetCheckListByContract]
	@ContractGUID nvarchar(50),
	@PatientGUID nvarchar(50)
AS
BEGIN
	SELECT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, EndDate,
	CAST(0 AS bit) AS Using, C.Completed
	INTO TMP
	FROM CompanyContract C, CompanyCheckList L, CompanyMember M,
	ContractMember CM
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	M.PatientGUID = @PatientGUID AND CM.Status = 0 AND
	M.Status = 0 AND L.Status = 0 AND C.CompanyContractGUID = @ContractGUID

	UPDATE TMP
	SET Using = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.PatientGUID = @PatientGUID AND S.Status = 0 AND
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	SELECT CompanyGUID, CompanyContractGUID, S.ServiceGUID, Code, [Name],
	BeginDate, Using, CAST(0 AS Bit) AS Checked
	FROM TMP, Services S
	WHERE TMP.ServiceGUID = S.ServiceGUID
	ORDER BY TMP.Using, S.[Name]

	DROP TABLE TMP
END
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
ALTER PROCEDURE [dbo].[spDichVuHopDong]
	@ContractGUID nvarchar(50),
	@TuNgay dateTime,
	@DenNgay datetime,
	@Type int --0: All, 1: Chua kham; 2: Da kham
AS
BEGIN
	DECLARE @BeginDate datetime
	DECLARE @EndDate  datetime
	DECLARE @FromDate  datetime
	DECLARE @ToDate  datetime

	SET @FromDate = @TuNgay
	SET @ToDate = @DenNgay

	SELECT @BeginDate = BeginDate, @EndDate = EndDate FROM CompanyContract
	WHERE CompanyContractGUID = @ContractGUID

	IF (@TuNgay < @BeginDate) 
	BEGIN
		SET @TuNgay = @BeginDate
	END

	IF (@EndDate IS NOT NULL AND @DenNgay > @EndDate)
	BEGIN
		SET @DenNgay = @EndDate
	END
	
	IF (@Type = 0)
	BEGIN
		SELECT @FromDate AS TuNgay, @ToDate AS DenNgay, ContractName, FullName, 
		NULL AS NgayKham, FirstName, Mobile, N'Chưa khám' AS TinhTrang
		FROM ContractMemberView M, CompanyContract C
		WHERE C.CompanyContractGUID = M.CompanyContractGUID	AND 
		C.CompanyContractGUID = @ContractGUID AND 
		NOT EXISTS (SELECT TOP 1 * FROM ServiceHistory WHERE PatientGUID = M.PatientGUID AND
		ActivedDate BETWEEN @TuNgay AND @DenNgay AND ServiceHistory.Status = 0) AND
		M.Status = 0 AND M.CompanyMemberStatus = 0 AND M.Archived = 'False'
		UNION
		SELECT @FromDate AS TuNgay, @ToDate AS DenNgay, ContractName, FullName, 
		Min(ActivedDate) AS NgayKham, FirstName, Mobile, N'Đã khám' AS TinhTrang
		FROM ContractMemberView M, CompanyContract C, ServiceHistory S
		WHERE C.CompanyContractGUID = M.CompanyContractGUID	AND 
		M.PatientGUID = S.PatientGUID AND
		C.CompanyContractGUID = @ContractGUID AND 
		S.ActivedDate BETWEEN @TuNgay AND @DenNgay AND S.Status = 0 AND
		M.Status = 0 AND M.CompanyMemberStatus = 0 AND M.Archived = 'False'
		GROUP BY ContractName, FullName, FirstName, Mobile
		ORDER BY FirstName, FullName
	END
	ELSE IF (@Type = 1)
	BEGIN
		SELECT @FromDate AS TuNgay, @ToDate AS DenNgay, ContractName, FullName, 
		NULL AS NgayKham, FirstName, Mobile, N'Chưa khám' AS TinhTrang
		FROM ContractMemberView M, CompanyContract C
		WHERE C.CompanyContractGUID = M.CompanyContractGUID	AND 
		C.CompanyContractGUID = @ContractGUID AND 
		NOT EXISTS (SELECT TOP 1 * FROM ServiceHistory WHERE PatientGUID = M.PatientGUID AND
		ActivedDate BETWEEN @TuNgay AND @DenNgay AND ServiceHistory.Status = 0) AND
		M.Status = 0 AND M.CompanyMemberStatus = 0 AND M.Archived = 'False'
		ORDER BY FirstName, FullName
	END
	ELSE
	BEGIN
		SELECT @FromDate AS TuNgay, @ToDate AS DenNgay, ContractName, FullName, 
		Min(ActivedDate) AS NgayKham, FirstName, Mobile, N'Đã khám' AS TinhTrang
		FROM ContractMemberView M, CompanyContract C, ServiceHistory S
		WHERE C.CompanyContractGUID = M.CompanyContractGUID	AND 
		M.PatientGUID = S.PatientGUID AND
		C.CompanyContractGUID = @ContractGUID AND 
		S.ActivedDate BETWEEN @TuNgay AND @DenNgay AND S.Status = 0 AND
		M.Status = 0 AND M.CompanyMemberStatus = 0 AND M.Archived = 'False'
		GROUP BY ContractName, FullName, FirstName, Mobile
		ORDER BY FirstName, FullName
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[CompanyContractView]
AS
SELECT     dbo.Company.CompanyGUID, dbo.Company.MaCty, dbo.Company.TenCty, dbo.Company.DiaChi, dbo.Company.Dienthoai, dbo.Company.Fax, 
                      dbo.Company.Website, dbo.CompanyContract.CompanyContractGUID, dbo.CompanyContract.ContractName, dbo.CompanyContract.Completed, 
                      dbo.CompanyContract.CreatedDate, dbo.CompanyContract.CreatedBy, dbo.CompanyContract.UpdatedDate, dbo.CompanyContract.UpdatedBy, 
                      dbo.CompanyContract.DeletedDate, dbo.CompanyContract.DeletedBy, dbo.CompanyContract.Status AS ContractStatus, 
                      dbo.Company.Status AS CompanyStatus, dbo.CompanyContract.BeginDate, dbo.CompanyContract.ContractCode, dbo.CompanyContract.EndDate
FROM         dbo.Company INNER JOIN
                      dbo.CompanyContract ON dbo.Company.CompanyGUID = dbo.CompanyContract.CompanyGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[PatientView]
AS
SELECT     dbo.Contact.ContactGUID, dbo.Contact.Title, dbo.Contact.FirstName, dbo.Contact.SurName, dbo.Contact.KnownAs, dbo.Contact.MiddleName, 
                      dbo.Contact.AliasFirstName, dbo.Contact.AliasSurName, dbo.Contact.Dob, dbo.Contact.PreferredName, dbo.Contact.Occupation, 
                      dbo.Contact.IdentityCard, dbo.Contact.Archived, dbo.Contact.DateArchived, dbo.Contact.Note, dbo.Contact.HomePhone, dbo.Contact.WorkPhone, 
                      dbo.Contact.Mobile, dbo.Contact.Email, dbo.Contact.FAX, dbo.Contact.CreatedDate, dbo.Contact.CreatedBy, dbo.Contact.UpdatedDate, 
                      dbo.Contact.UpdatedBy, dbo.Contact.DeletedDate, dbo.Contact.DeletedBy, dbo.Contact.Gender, dbo.Contact.Address, dbo.Contact.Ward, 
                      dbo.Contact.District, dbo.Contact.City, dbo.Patient.FileNum, dbo.Patient.BarCode, dbo.Patient.Picture, dbo.Patient.HearFrom, dbo.Patient.Salutation, 
                      dbo.Patient.LastSeenDate, dbo.Patient.LastSeenDocGUID, dbo.Patient.DateDeceased, dbo.Patient.LastVisitGUID, 
                      CASE Gender WHEN 0 THEN N'Nam' WHEN 1 THEN N'Nữ' END AS GenderAsStr, dbo.Patient.PatientGUID, dbo.Contact.DobStr, dbo.Contact.FullName, 
                      dbo.PatientHistory.Di_Ung_Thuoc, dbo.PatientHistory.Thuoc_Di_Ung, dbo.PatientHistory.Dot_Quy, dbo.PatientHistory.Benh_Tim_Mach, 
                      dbo.PatientHistory.Benh_Lao, dbo.PatientHistory.Dai_Thao_Duong, dbo.PatientHistory.Dai_Duong_Dang_Dieu_Tri, dbo.PatientHistory.Viem_Gan_B, 
                      dbo.PatientHistory.Viem_Gan_C, dbo.PatientHistory.Viem_Gan_Dang_Dieu_Tri, dbo.PatientHistory.Ung_Thu, dbo.PatientHistory.Co_Quan_Ung_Thu, 
                      dbo.PatientHistory.Dong_Kinh, dbo.PatientHistory.Hen_Suyen, dbo.PatientHistory.Benh_Khac, dbo.PatientHistory.Benh_Gi, 
                      dbo.PatientHistory.Thuoc_Dang_Dung, dbo.PatientHistory.Hut_Thuoc, dbo.PatientHistory.Uong_Ruou, dbo.PatientHistory.Tinh_Trang_Gia_Dinh, 
                      dbo.PatientHistory.Chich_Ngua_Viem_Gan_B, dbo.PatientHistory.Chich_Ngua_Uon_Van, dbo.PatientHistory.Chich_Ngua_Cum, 
                      dbo.PatientHistory.Dang_Co_Thai, dbo.PatientHistory.PatientHistoryGUID, dbo.Contact.Source, dbo.Contact.CompanyName
FROM         dbo.Contact INNER JOIN
                      dbo.Patient ON dbo.Contact.ContactGUID = dbo.Patient.ContactGUID INNER JOIN
                      dbo.PatientHistory ON dbo.Patient.PatientGUID = dbo.PatientHistory.PatientGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

