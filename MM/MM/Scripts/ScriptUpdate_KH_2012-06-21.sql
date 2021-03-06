USE MM
GO
GO
/****** Object:  UserDefinedFunction [dbo].[Split]    Script Date: 06/21/2012 21:26:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[Split]
(
	@RowData nvarchar(4000),
	@SplitOn nvarchar(5)
)  
RETURNS @RtnValue table 
(
	Id int identity(1,1),
	Data nvarchar(100)
) 
AS  
BEGIN 
	Declare @Cnt int
	Set @Cnt = 1

	While (Charindex(@SplitOn,@RowData)>0)
	Begin
		Insert Into @RtnValue (data)
		Select 
			Data = ltrim(rtrim(Substring(@RowData,1,Charindex(@SplitOn,@RowData)-1)))

		Set @RowData = Substring(@RowData,Charindex(@SplitOn,@RowData)+1,len(@RowData))
		Set @Cnt = @Cnt + 1
	End
	
	Insert Into @RtnValue (data)
	Select Data = ltrim(rtrim(@RowData))

	Return
END
GO
GO
/****** Object:  StoredProcedure [dbo].[spThuocTonKho]    Script Date: 06/21/2012 21:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spThuocTonKho]
	@TuNgay datetime,
	@DenNgay datetime,
	@MaThuocs nvarchar(4000)
AS
BEGIN
	DECLARE @MaThuoc nvarchar(100)
	DECLARE @TenThuoc nvarchar(255)
	DECLARE @DVT nvarchar(50)
	DECLARE @ThuocGUID nvarchar(50)
	DECLARE @SoDu INT
	DECLARE @SLNhap INT
	DECLARE @SLXuat INT
	DECLARE @SLTon INT

	DECLARE @ThuocTonKho table 
	(
		ThuocGUID nvarchar(50),
		MaThuoc nvarchar(100),
		TenThuoc nvarchar(255),	
		DonViTinh nvarchar(50),
		SoDu int,
		SLNhap int,
		SLXuat int,
		SLTon int
	) 

	DECLARE Thuoc_Cursor CURSOR FOR 
	SELECT Data FROM  dbo.Split(@MaThuocs, ',') --ORDER BY Data
	OPEN Thuoc_Cursor
	FETCH NEXT FROM Thuoc_Cursor 
	INTO @MaThuoc

	WHILE @@FETCH_STATUS = 0
	BEGIN
		SELECT @ThuocGUID = ThuocGUID, @TenThuoc = TenThuoc, @DVT = DonViTinh
		FROM Thuoc WHERE MaThuoc = @MaThuoc AND Status = 0

		IF (@ThuocGUID IS NOT NULL)
		BEGIN
			--SL Nhap Truoc Tu Ngay
			SET @SLNhap = (SELECT SUM(SoLuongNhap * SoLuongQuiDoi) FROM LoThuoc 
			WHERE ThuocGUID = @ThuocGUID AND Status = 0 AND CreatedDate < @TuNgay)
			IF (@SLNhap IS NULL) SET @SLNhap = 0
			--PRINT @SLNhap

			--SL Xuat Truoc Tu Ngay
			SET @SLXuat = (SELECT SUM(CT.SoLuong) FROM PhieuThuThuoc PT, ChiTietPhieuThuThuoc CT
			WHERE PT.PhieuThuThuocGUID = CT.PhieuThuThuocGUID AND
			PT.Status = 0 AND CT.Status = 0 AND CT.ThuocGUID = @ThuocGUID AND
			PT.NgayThu < @TuNgay)

			IF (@SLXuat IS NULL) SET @SLXuat = 0
			--PRINT @SLXuat

			-- So Du Dau
			SET @SoDu = @SLNhap - @SLXuat
			--Print @SoDu	

			--SL Nhap Trong Khoang Tu Ngay - Den Ngay
			SET @SLNhap = (SELECT SUM(SoLuongNhap * SoLuongQuiDoi) FROM LoThuoc 
			WHERE ThuocGUID = @ThuocGUID AND Status = 0 AND CreatedDate BETWEEN @TuNgay AND @DenNgay)
			IF (@SLNhap IS NULL) SET @SLNhap = 0
			
			--SL Xuat Trong Khoang Tu Ngay - Den Ngay
			SET @SLXuat = (SELECT SUM(CT.SoLuong) FROM PhieuThuThuoc PT, ChiTietPhieuThuThuoc CT
			WHERE PT.PhieuThuThuocGUID = CT.PhieuThuThuocGUID AND
			PT.Status = 0 AND CT.Status = 0 AND CT.ThuocGUID = @ThuocGUID AND
			PT.NgayThu BETWEEN @TuNgay AND @DenNgay)
			IF (@SLXuat IS NULL) SET @SLXuat = 0

			--SL Ton
			SET @SLTon = @SoDu + @SLNhap - @SLXuat

			INSERT INTO @ThuocTonKho
			SELECT @ThuocGUID, @MaThuoc, @TenThuoc, @DVT, @SoDu, @SLNhap, @SLXuat, @SLTon
		END
	    
		FETCH NEXT FROM Thuoc_Cursor 
		INTO @MaThuoc
	END 
	CLOSE Thuoc_Cursor
	DEALLOCATE Thuoc_Cursor

	SELECT * FROM @ThuocTonKho ORDER BY TenThuoc
END
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'c8b187be-962e-4c2b-8488-0afb999af048', N'ThuocTonKhoTheoKhoangThoiGian', N'Báo cáo thuốc tồn kho theo khoảng thời gian')