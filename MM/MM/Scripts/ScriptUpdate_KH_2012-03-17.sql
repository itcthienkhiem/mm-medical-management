USE MM
GO
ALTER TABLE Permission
ADD [IsLock] [bit] NOT NULL DEFAULT ((0))
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[PermissionView]
AS
SELECT     dbo.Permission.PermissionGUID, dbo.Permission.LogonGUID, dbo.[Function].FunctionGUID, dbo.[Function].FunctionCode, dbo.Permission.IsView, 
                      dbo.Permission.IsAdd, dbo.Permission.IsEdit, dbo.Permission.IsDelete, dbo.Permission.IsPrint, dbo.Permission.IsExport, dbo.Permission.CreatedDate, 
                      dbo.Permission.CreatedBy, dbo.Permission.UpdatedDate, dbo.Permission.UpdatedBy, dbo.Permission.DeletedDate, dbo.Permission.DeletedBy, 
                      dbo.[Function].FunctionName, dbo.Permission.IsImport, dbo.Permission.IsConfirm, dbo.Permission.IsLock
FROM         dbo.Permission INNER JOIN
                      dbo.[Function] ON dbo.Permission.FunctionGUID = dbo.[Function].FunctionGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
GO
/****** Object:  Table [dbo].[Lock]    Script Date: 03/17/2012 10:47:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lock](
	[LockGUID] [uniqueidentifier] NOT NULL,
	[KeyGUID] [uniqueidentifier] NOT NULL,
	[Loai] [tinyint] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_Lock_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_Lock] PRIMARY KEY CLUSTERED 
(
	[LockGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GiaDichVuHopDong]    Script Date: 03/17/2012 10:47:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiaDichVuHopDong](
	[GiaDichVuHopDongGUID] [uniqueidentifier] NOT NULL,
	[ServiceGUID] [uniqueidentifier] NOT NULL,
	[HopDongGUID] [uniqueidentifier] NOT NULL,
	[Gia] [float] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_GiaDichVuHopDong] PRIMARY KEY CLUSTERED 
(
	[GiaDichVuHopDongGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[GiaDichVuHopDong]  WITH CHECK ADD  CONSTRAINT [FK_GiaDichVuHopDong_CompanyContract] FOREIGN KEY([HopDongGUID])
REFERENCES [dbo].[CompanyContract] ([CompanyContractGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GiaDichVuHopDong]  WITH CHECK ADD  CONSTRAINT [FK_GiaDichVuHopDong_Services] FOREIGN KEY([ServiceGUID])
REFERENCES [dbo].[Services] ([ServiceGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  View [dbo].[GiaDichVuHopDongView]    Script Date: 03/17/2012 10:54:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GiaDichVuHopDongView]
AS
SELECT     dbo.GiaDichVuHopDong.Gia, dbo.GiaDichVuHopDong.HopDongGUID, dbo.GiaDichVuHopDong.CreatedDate, 
                      dbo.GiaDichVuHopDong.GiaDichVuHopDongGUID, dbo.GiaDichVuHopDong.ServiceGUID, dbo.GiaDichVuHopDong.CreatedBy, dbo.GiaDichVuHopDong.UpdatedDate, 
                      dbo.GiaDichVuHopDong.UpdatedBy, dbo.GiaDichVuHopDong.DeletedDate, dbo.GiaDichVuHopDong.DeletedBy, dbo.GiaDichVuHopDong.Status AS GiaDVHDStatus, 
                      dbo.Services.Code, dbo.Services.Name, dbo.Services.EnglishName, dbo.Services.Price, dbo.Services.Type, dbo.Services.Status AS ServiceStatus
FROM         dbo.GiaDichVuHopDong INNER JOIN
                      dbo.Services ON dbo.GiaDichVuHopDong.ServiceGUID = dbo.Services.ServiceGUID

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
CREATE PROCEDURE [dbo].[spCheckContractByService]
	@ContractGUID nvarchar(50),
	@ServiceGUID nvarchar(50),
	@Result int output
AS
BEGIN
	SELECT DISTINCT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, EndDate, 
	CAST(0 AS bit) AS Checked, C.Completed, M.PatientGUID 
	INTO TMP
	FROM CompanyContract C, CompanyCheckList L, CompanyMember M,
	ContractMember CM, Services S
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	S.ServiceGUID = L.ServiceGUID AND --S.Status = 0 AND
	--M.PatientGUID = @PatientGUID AND 
	L.Status = 0 AND C.Status = 0 AND CM.Status = 0 AND
	(C.Completed = 'False' AND C.BeginDate <= GetDate() OR 
	 C.Completed = 'True' AND GetDate() BETWEEN C.BeginDate AND C.EndDate) AND
	L.ServiceGUID = @ServiceGUID AND C.CompanyContractGUID = @ContractGUID

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'False' AND
	S.PatientGUID = TMP.PatientGUID AND S.Status = 0  AND 
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate) AND
	S.ServiceGUID = @ServiceGUID

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'True' AND
	S.RootPatientGUID = TMP.PatientGUID AND S.Status = 0 AND
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate) AND
	S.ServiceGUID = @ServiceGUID

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
ALTER PROCEDURE [dbo].[spCheckMember]
	@PatientGUID nvarchar(50),
	@Result int output
AS
BEGIN
	SELECT DISTINCT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, EndDate, 
	CAST(0 AS bit) AS Checked, C.Completed 
	INTO TMP
	FROM CompanyContract C, CompanyCheckList L, CompanyMember M,
	ContractMember CM, Services S
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	S.ServiceGUID = L.ServiceGUID AND --S.Status = 0 AND
	M.PatientGUID = @PatientGUID AND 
	L.Status = 0 AND C.Status = 0 AND CM.Status = 0 AND
	(C.Completed = 'False' AND C.BeginDate <= GetDate() OR 
	 C.Completed = 'True' AND GetDate() BETWEEN C.BeginDate AND C.EndDate)

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'False' AND
	S.PatientGUID = @PatientGUID AND S.Status = 0  AND 
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'True' AND
	S.RootPatientGUID = @PatientGUID AND S.Status = 0 AND
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
ALTER PROCEDURE [dbo].[spCheckMemberByService]
	@PatientGUID nvarchar(50),
	@ServiceGUID nvarchar(50),
	@Result int output
AS
BEGIN
	SELECT DISTINCT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, EndDate, 
	CAST(0 AS bit) AS Checked, C.Completed 
	INTO TMP
	FROM CompanyContract C, CompanyCheckList L, CompanyMember M,
	ContractMember CM, Services S
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	S.ServiceGUID = L.ServiceGUID AND --S.Status = 0 AND
	M.PatientGUID = @PatientGUID AND 
	L.Status = 0 AND C.Status = 0 AND CM.Status = 0 AND
	(C.Completed = 'False' AND C.BeginDate <= GetDate() OR 
	 C.Completed = 'True' AND GetDate() BETWEEN C.BeginDate AND C.EndDate) AND
	L.ServiceGUID = @ServiceGUID

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'False' AND
	S.PatientGUID = @PatientGUID AND S.Status = 0  AND 
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate) AND
	S.ServiceGUID = @ServiceGUID

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'True' AND
	S.RootPatientGUID = @PatientGUID AND S.Status = 0 AND
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate) AND
	S.ServiceGUID = @ServiceGUID

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
ALTER PROCEDURE [dbo].[spGetCheckList]
	@PatientGUID nvarchar(50)
AS
BEGIN
	SELECT DISTINCT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, EndDate,
	CAST(0 AS bit) AS Checked, C.Completed, CAST('' AS nvarchar(255)) AS NguoiChuyenNhuong
	INTO TMP
	FROM CompanyContract C, CompanyCheckList L, CompanyMember M,
	ContractMember CM, Services S
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	S.ServiceGUID = L.ServiceGUID AND --S.Status = 0 AND
	M.PatientGUID = @PatientGUID AND CM.Status = 0 AND
	L.Status = 0 AND C.Status = 0 AND
	(C.Completed = 'False' AND C.BeginDate <= GetDate() OR 
	 C.Completed = 'True' AND GetDate() BETWEEN C.BeginDate AND C.EndDate)

	UPDATE TMP
	SET Checked = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'False' AND
	S.PatientGUID = @PatientGUID AND S.Status = 0 AND
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	UPDATE TMP
	SET Checked = 'True', NguoiChuyenNhuong = P.FullName 
	FROM ServiceHistory S, PatientView P
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.PatientGUID = P.PatientGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'True' AND
	S.RootPatientGUID = @PatientGUID AND S.Status = 0 AND
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	SELECT CompanyGUID, CompanyContractGUID, S.ServiceGUID, 
	Code, [Name], BeginDate, EndDate, Checked, NguoiChuyenNhuong
	FROM TMP, Services S
	WHERE TMP.ServiceGUID = S.ServiceGUID
	ORDER BY TMP.Checked, S.[Name]

	DROP TABLE TMP
END
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
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
	CAST(0 AS bit) AS Using, C.Completed, CAST('' AS nvarchar(255)) AS NguoiChuyenNhuong
	INTO TMP
	FROM CompanyContract C, CompanyCheckList L, CompanyMember M,
	ContractMember CM, Services S
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.CompanyGUID = M.CompanyGUID AND 
	S.ServiceGUID = L.ServiceGUID AND --S.Status = 0 AND
	M.PatientGUID = @PatientGUID AND CM.Status = 0 AND
	L.Status = 0 AND C.CompanyContractGUID = @ContractGUID

	UPDATE TMP
	SET Using = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'False' AND
	S.PatientGUID = @PatientGUID AND S.Status = 0 AND
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	UPDATE TMP
	SET Using = 'True', NguoiChuyenNhuong = P.FullName 
	FROM ServiceHistory S, PatientView P
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.PatientGUID = P.PatientGUID AND
	S.IsExported = 'False' AND S.KhamTuTuc = 'True' AND
	S.RootPatientGUID = @PatientGUID AND S.Status = 0 AND
	(TMP.Completed = 'False' AND S.ActivedDate > TMP.BeginDate OR
	TMP.Completed = 'True' AND S.ActivedDate BETWEEN TMP.BeginDate AND TMP.EndDate)

	SELECT CompanyGUID, CompanyContractGUID, S.ServiceGUID, Code, [Name], EnglishName,
	BeginDate, EndDate, Using, CAST(0 AS Bit) AS Checked, NguoiChuyenNhuong
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

ALTER VIEW [dbo].[CompanyContractView]
AS
SELECT     dbo.Company.CompanyGUID, dbo.Company.MaCty, dbo.Company.TenCty, dbo.Company.DiaChi, dbo.Company.Dienthoai, dbo.Company.Fax, dbo.Company.Website, 
                      dbo.CompanyContract.CompanyContractGUID, dbo.CompanyContract.ContractName, dbo.CompanyContract.Completed, dbo.CompanyContract.CreatedDate, 
                      dbo.CompanyContract.CreatedBy, dbo.CompanyContract.UpdatedDate, dbo.CompanyContract.UpdatedBy, dbo.CompanyContract.DeletedDate, 
                      dbo.CompanyContract.DeletedBy, dbo.CompanyContract.Status AS ContractStatus, dbo.Company.Status AS CompanyStatus, dbo.CompanyContract.BeginDate, 
                      dbo.CompanyContract.ContractCode, dbo.CompanyContract.EndDate, ISNULL(dbo.Lock.Status, 0) AS Lock
FROM         dbo.Company INNER JOIN
                      dbo.CompanyContract ON dbo.Company.CompanyGUID = dbo.CompanyContract.CompanyGUID LEFT OUTER JOIN
                      dbo.Lock ON dbo.CompanyContract.CompanyContractGUID = dbo.Lock.KeyGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



















