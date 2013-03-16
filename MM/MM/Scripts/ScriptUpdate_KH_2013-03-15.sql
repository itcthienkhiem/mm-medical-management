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


