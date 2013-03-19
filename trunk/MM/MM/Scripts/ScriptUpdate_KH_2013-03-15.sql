USE MM
GO
ALTER TABLE dbo.TinNhanMau
ADD [IsDuyet] [bit] NOT NULL DEFAULT ((0))
GO
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[spDanhSachNhanVienDenKham]
	@FromDate datetime,
	@ToDate datetime,
	@MaBenhNhan nvarchar(50),
	@Type int --0: Ten benh nhan, 1: Ma benh nhan
AS
BEGIN
	SELECT P.PatientGUID, Max(S.ActivedDate) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile
	INTO #TEMP
	FROM PatientView P WITH(NOLOCK), ServiceHistory S WITH(NOLOCK) 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.ActivedDate BETWEEN @FromDate AND @ToDate AND 
	((@Type=1 AND P.FileNum LIKE N'%' + @MaBenhNhan + '%') OR
	(@Type=0 AND P.FullName LIKE N'%' + @MaBenhNhan + '%')) AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayCanDo) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P WITH(NOLOCK), CanDo S WITH(NOLOCK) 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayCanDo BETWEEN @FromDate AND @ToDate AND 
	((@Type=1 AND P.FileNum LIKE N'%' + @MaBenhNhan + '%') OR
	(@Type=0 AND P.FullName LIKE N'%' + @MaBenhNhan + '%')) AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile	
	UNION
	SELECT P.PatientGUID, Max(S.NgayKetLuan) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P WITH(NOLOCK), KetLuan S WITH(NOLOCK) 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayKetLuan BETWEEN @FromDate AND @ToDate AND 
	((@Type=1 AND P.FileNum LIKE N'%' + @MaBenhNhan + '%') OR
	(@Type=0 AND P.FullName LIKE N'%' + @MaBenhNhan + '%')) AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayKham) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P WITH(NOLOCK), KetQuaLamSang S WITH(NOLOCK)
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayKham BETWEEN @FromDate AND @ToDate AND 
	((@Type=1 AND P.FileNum LIKE N'%' + @MaBenhNhan + '%') OR
	(@Type=0 AND P.FullName LIKE N'%' + @MaBenhNhan + '%')) AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayKham) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P WITH(NOLOCK), KetQuaNoiSoi S WITH(NOLOCK) 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayKham BETWEEN @FromDate AND @ToDate AND 
	((@Type=1 AND P.FileNum LIKE N'%' + @MaBenhNhan + '%') OR
	(@Type=0 AND P.FullName LIKE N'%' + @MaBenhNhan + '%')) AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayKham) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P WITH(NOLOCK), KetQuaSoiCTC S WITH(NOLOCK) 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayKham BETWEEN @FromDate AND @ToDate AND 
	((@Type=1 AND P.FileNum LIKE N'%' + @MaBenhNhan + '%') OR
	(@Type=0 AND P.FullName LIKE N'%' + @MaBenhNhan + '%')) AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.Ngay) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P WITH(NOLOCK), LoiKhuyen S WITH(NOLOCK) 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.Ngay BETWEEN @FromDate AND @ToDate AND 
	((@Type=1 AND P.FileNum LIKE N'%' + @MaBenhNhan + '%') OR
	(@Type=0 AND P.FullName LIKE N'%' + @MaBenhNhan + '%')) AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgayKeToa) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P WITH(NOLOCK), ToaThuoc S WITH(NOLOCK) 
	WHERE P.PatientGUID = S.BenhNhan AND 
	S.NgayKeToa BETWEEN @FromDate AND @ToDate AND 
	((@Type=1 AND P.FileNum LIKE N'%' + @MaBenhNhan + '%') OR
	(@Type=0 AND P.FullName LIKE N'%' + @MaBenhNhan + '%')) AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT PatientGUID, NgayKham, FileNum, FullName, DobStr, GenderAsStr, Address, Mobile
	FROM PatientView WITH(NOLOCK)
	WHERE NgayKham IS NOT NULL AND NgayKham BETWEEN @FromDate AND @ToDate AND
	((@Type=1 AND FileNum LIKE N'%' + @MaBenhNhan + '%') OR
	(@Type=0 AND FullName LIKE N'%' + @MaBenhNhan + '%')) AND Archived = 'False'  
	UNION
	SELECT P.PatientGUID, Max(S.NgayKham) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P WITH(NOLOCK), KetQuaCanLamSang S WITH(NOLOCK)
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgayKham BETWEEN @FromDate AND @ToDate AND 
	((@Type=1 AND P.FileNum LIKE N'%' + @MaBenhNhan + '%') OR
	(@Type=0 AND P.FullName LIKE N'%' + @MaBenhNhan + '%')) AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile
	UNION
	SELECT P.PatientGUID, Max(S.NgaySieuAm) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address, P.Mobile 
	FROM PatientView P WITH(NOLOCK), KetQuaSieuAm S WITH(NOLOCK) 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.NgaySieuAm BETWEEN @FromDate AND @ToDate AND 
	((@Type=1 AND P.FileNum LIKE N'%' + @MaBenhNhan + '%') OR
	(@Type=0 AND P.FullName LIKE N'%' + @MaBenhNhan + '%')) AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr, P.Mobile

	SELECT PatientGUID, Max(NgayKham) AS NgayKham, FileNum, FullName, 
	DobStr, GenderAsStr, Address, Mobile 
	FROM #TEMP
	GROUP BY PatientGUID, FullName, FileNum, GenderAsStr, Address, DobStr, Mobile
	ORDER BY NgayKham
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
ALTER PROCEDURE [dbo].[spDanhSachNhanVienChuaDenKham]
	@FromDate datetime,
	@ToDate datetime,
	@MaBenhNhan nvarchar(50),
	@Type int --0: Ten benh nhan, 1: Ma benh nhan
AS
BEGIN
	
	SELECT P.PatientGUID, NULL AS NgayKham, P.FileNum, P.FullName, P.DobStr, P.GenderAsStr, P.Address, P.Mobile
	FROM PatientView P WITH(NOLOCK)
	WHERE ((@Type=1 AND P.FileNum LIKE N'%' + @MaBenhNhan + '%') OR
	(@Type=0 AND P.FullName LIKE N'%' + @MaBenhNhan + '%')) AND P.Archived = 'False' AND
	(P.NgayKham IS NULL OR P.NgayKham < @FromDate OR P.NgayKham > @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.PatientGUID FROM ServiceHistory S 
	WHERE P.PatientGUID = S.PatientGUID AND S.Status = 0 AND S.ActivedDate BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.PatientGUID FROM CanDo S 
	WHERE P.PatientGUID = S.PatientGUID AND S.Status = 0 AND S.NgayCanDo BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.PatientGUID FROM KetLuan S 
	WHERE P.PatientGUID = S.PatientGUID AND S.Status = 0 AND S.NgayKetLuan BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.PatientGUID FROM KetQuaLamSang S 
	WHERE P.PatientGUID = S.PatientGUID AND S.Status = 0 AND S.NgayKham BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.PatientGUID FROM KetQuaNoiSoi S 
	WHERE P.PatientGUID = S.PatientGUID AND S.Status = 0 AND S.NgayKham BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.PatientGUID FROM KetQuaSoiCTC S 
	WHERE P.PatientGUID = S.PatientGUID AND S.Status = 0 AND S.NgayKham BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.PatientGUID FROM LoiKhuyen S 
	WHERE P.PatientGUID = S.PatientGUID AND S.Status = 0 AND S.Ngay BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.BenhNhan FROM ToaThuoc S 
	WHERE P.PatientGUID = S.BenhNhan AND S.Status = 0 AND S.NgayKeToa BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.BenhNhan FROM ToaThuoc S 
	WHERE P.PatientGUID = S.BenhNhan AND S.Status = 0 AND S.NgayKeToa BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.PatientGUID FROM KetQuaCanLamSang S 
	WHERE P.PatientGUID = S.PatientGUID AND S.Status = 0 AND S.NgayKham BETWEEN @FromDate AND @ToDate) AND
	NOT EXISTS (SELECT TOP 1 S.PatientGUID FROM KetQuaSieuAm S 
	WHERE P.PatientGUID = S.PatientGUID AND S.Status = 0 AND S.NgaySieuAm BETWEEN @FromDate AND @ToDate)
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER TABLE dbo.SMSLog
ALTER COLUMN PatientGUID uniqueidentifier NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[SMSLogView]
AS
SELECT     dbo.SMSLog.SMSLogGUID, dbo.SMSLog.Ngay, dbo.SMSLog.NoiDung, dbo.SMSLog.Mobile, dbo.SMSLog.PatientGUID, dbo.SMSLog.DocStaffGUID, 
                      dbo.SMSLog.Status, dbo.SMSLog.Notes, dbo.PatientView.FullName, dbo.PatientView.DobStr, dbo.PatientView.GenderAsStr, dbo.PatientView.FileNum, 
                      dbo.PatientView.Address, 
                      CASE dbo.SMSLog.DocStaffGUID WHEN '00000000-0000-0000-0000-000000000000' THEN 'Admin' ELSE dbo.DocStaffView.FullName END AS NguoiGui
FROM         dbo.SMSLog LEFT OUTER JOIN
                      dbo.PatientView ON dbo.SMSLog.PatientGUID = dbo.PatientView.PatientGUID LEFT OUTER JOIN
                      dbo.DocStaffView ON dbo.SMSLog.DocStaffGUID = dbo.DocStaffView.DocStaffGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER TABLE dbo.CompanyContract
ADD [NhanSuPhuTrach] [nvarchar](255) NULL,
	[SoDienThoai] [nvarchar](50) NULL,
	[NgayDatCoc] [datetime] NULL
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
                      dbo.CompanyContract.ContractCode, dbo.CompanyContract.EndDate, ISNULL(dbo.Lock.Status, 0) AS Lock, dbo.CompanyContract.SoTien, 
                      dbo.CompanyContract.DatCoc, dbo.Company.MaSoThue, dbo.CompanyContract.NhanSuPhuTrach, dbo.CompanyContract.SoDienThoai, 
                      dbo.CompanyContract.NgayDatCoc
FROM         dbo.Company INNER JOIN
                      dbo.CompanyContract ON dbo.Company.CompanyGUID = dbo.CompanyContract.CompanyGUID LEFT OUTER JOIN
                      dbo.Lock ON dbo.CompanyContract.CompanyContractGUID = dbo.Lock.KeyGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spCongNoHopDong]
	@HopDongGUID nvarchar(50)
AS
BEGIN
	SELECT PV.PatientGUID, PV.FirstName, PV.FullName, PV.DobStr, PV.GenderAsStr, S.ServiceGUID, S.[Name], 
	SH.Price AS Gia, CAST(0 as float) AS Giam, SH.Price AS ThanhTien, '' AS NguoiNhanChuyenNhuong, 
	CAST(0 AS bit) AS DaThuTien, CAST(0 as INT) AS Loai 
	INTO #TMP
	FROM CompanyContract HD WITH(NOLOCK), ContractMember CM WITH(NOLOCK), 
	CompanyMember NV WITH(NOLOCK), PatientView PV WITH(NOLOCK), 
	CompanyCheckList CL WITH(NOLOCK), ServiceHistory SH WITH(NOLOCK), Services S WITH(NOLOCK) 
	WHERE HD.CompanyContractGUID = CM.CompanyContractGUID AND 
	CM.CompanyMemberGUID = NV.CompanyMemberGUID AND 
	CL.ContractMemberGUID = CM.ContractMemberGUID AND 
	CL.ServiceGUID = SH.ServiceGUID AND NV.PatientGUID = SH.PatientGUID AND 
	SH.ServiceGUID = S.ServiceGUID AND PV.PatientGUID = NV.PatientGUID AND 
	PV.Archived = 'False' AND HD.Status = 0 AND CM.Status = 0 AND CL.Status = 0 AND 
	S.Status = 0 AND SH.Status = 0 AND 
	HD.CompanyContractGUID = @HopDongGUID AND 
	SH.IsExported = 'False' AND SH.KhamTuTuc = 'False' AND 
	((HD.Completed = 'False' AND SH.ActivedDate > HD.BeginDate) OR 
	(HD.Completed = 'True' AND SH.ActivedDate BETWEEN HD.BeginDate AND HD.EndDate)) 
	UNION
	SELECT CM.PatientGUID, CM.FirstName, CM.FullName, CM.DobStr, CM.GenderAsStr, V.ServiceGUID, V.[Name], 
	V.FixedPrice AS Gia, V.Discount AS Giam, 
	CAST((V.FixedPrice - (V.FixedPrice * V.Discount)/100) AS float) AS ThanhTien, 
	'' AS NguoiNhanChuyenNhuong, V.DaThuTien, CAST(2 as INT) AS Loai
	FROM dbo.CompanyContract C WITH(NOLOCK), dbo.ContractMember M WITH(NOLOCK), 
	dbo.DichVuLamThemView V WITH(NOLOCK),ContractMemberView CM WITH(NOLOCK) 
	WHERE C.CompanyContractGUID = M.CompanyContractGUID AND 
	V.ContractMemberGUID = M.ContractMemberGUID AND M.Status = 0 AND 
	V.Status = 0 AND V.ServiceStatus = 0 AND 
	C.CompanyContractGUID = @HopDongGUID AND 
	V.ContractMemberGUID = CM.ContractMemberGUID AND CM.Archived = 'False'
	UNION
	SELECT PV.PatientGUID, PV.FirstName, PV.FullName, PV.DobStr, PV.GenderAsStr, S.ServiceGUID,S.[Name], 
	SH.Price AS Gia, SH.Discount AS Giam, CAST((SH.Price - (SH.Price * SH.Discount)/100) AS float) AS ThanhTien,
	PV2.FullName AS NguoiNhanChuyenNhuong, CAST(0 AS bit) AS DaThuTien, CAST(1 as INT) AS Loai
	FROM CompanyContract HD WITH(NOLOCK), ContractMember CM WITH(NOLOCK), 
	CompanyMember NV WITH(NOLOCK), PatientView PV WITH(NOLOCK), 
	CompanyCheckList CL WITH(NOLOCK), ServiceHistory SH WITH(NOLOCK), 
	Services S WITH(NOLOCK), PatientView PV2 WITH(NOLOCK) 
	WHERE HD.CompanyContractGUID = CM.CompanyContractGUID AND 
	CM.CompanyMemberGUID = NV.CompanyMemberGUID AND CL.ContractMemberGUID = CM.ContractMemberGUID AND
	 CL.ServiceGUID = SH.ServiceGUID AND  NV.PatientGUID = SH.RootPatientGUID AND 
	SH.ServiceGUID = S.ServiceGUID AND PV.PatientGUID = NV.PatientGUID AND 
	PV.Archived = 'False' AND HD.Status = 0 AND CM.Status = 0 AND CL.Status = 0 AND 
	S.Status = 0 AND SH.Status = 0 AND HD.CompanyContractGUID = @HopDongGUID AND 
	SH.IsExported = 'False' AND SH.KhamTuTuc = 'True' AND 
	((HD.Completed = 'False' AND SH.ActivedDate > HD.BeginDate) OR 
	(HD.Completed = 'True' AND SH.ActivedDate BETWEEN HD.BeginDate AND HD.EndDate)) AND 
	SH.PatientGUID = PV2.PatientGUID

	SELECT * FROM #TMP
	ORDER BY FirstName, FullName, PatientGUID, [Name], Loai

	
	SELECT Loai, Max(SoLuong) AS MaxSoLuong FROM 
		(SELECT PatientGUID, Loai, Count(ServiceGUID) AS SoLuong 
		 FROM #TMP
		 GROUP BY PatientGUID, Loai) T
	GROUP BY Loai

	DROP TABLE #TMP
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
ALTER PROCEDURE [dbo].[spGetDanhSachNhanVien]
	@ContractGUID nvarchar(50),
	@Type int
AS
BEGIN
	DECLARE @PatientGUID nvarchar(50)
	DECLARE @FileNum nvarchar(50)
	DECLARE @FullName nvarchar(255)
	DECLARE @Dob nvarchar(50)
	DECLARE @Gender nvarchar(3)
	DECLARE @Result int

	CREATE TABLE #TEMP
	(
		PatientGUID nvarchar(50),
		FileNum nvarchar(50),
		FullName nvarchar(255),
		Dob nvarchar(50),
		Gender nvarchar(3)
	)	

	DECLARE db_cursor CURSOR FOR  
	SELECT PatientGUID, FileNum, FullName, DobStr, GenderAsStr
	FROM dbo.ContractMemberView WITH(NOLOCK)
	WHERE CompanyContractGUID = @ContractGUID AND Status = 0 AND 
	CompanyMemberStatus = 0 --AND Archived = 'False'
	
	OPEN db_cursor   
	FETCH NEXT FROM db_cursor INTO @PatientGUID, @FileNum, @FullName, @Dob, @Gender 
	WHILE @@FETCH_STATUS = 0   
	BEGIN   
		EXEC dbo.spCheckMember @PatientGUID, @Result output
		
		IF(@Result = @Type)
		BEGIN
			INSERT INTO #TEMP
			VALUES (@PatientGUID, @FileNum, @FullName, @Dob, @Gender) 				
		END
		
		FETCH NEXT FROM db_cursor INTO @PatientGUID, @FileNum, @FullName, @Dob, @Gender    
	END   

	CLOSE db_cursor   
	DEALLOCATE db_cursor

	--SELECT * FROM #TEMP ORDER BY FullName
	SELECT V.* 
	FROM #TEMP T, PatientView V 
	WHERE T.PatientGUID = V.PatientGUID
	ORDER BY FirstName, FullName
	
	DROP TABLE #TEMP
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO





