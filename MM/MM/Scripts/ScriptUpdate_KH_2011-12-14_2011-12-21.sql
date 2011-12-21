USE [MM]
GO
/****** Object:  Table [dbo].[Thuoc]    Script Date: 12/21/2011 22:34:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Thuoc](
	[ThuocGUID] [uniqueidentifier] NOT NULL,
	[MaThuoc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TenThuoc] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BietDuoc] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HamLuong] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HoatChat] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DonViTinh] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Note] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_Thuoc_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_Thuoc] PRIMARY KEY CLUSTERED 
(
	[ThuocGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhomThuoc]    Script Date: 12/21/2011 22:36:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhomThuoc](
	[NhomThuocGUID] [uniqueidentifier] NOT NULL,
	[MaNhomThuoc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TenNhomThuoc] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Note] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_NhomThuoc_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_NhomThuoc] PRIMARY KEY CLUSTERED 
(
	[NhomThuocGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhomThuoc_Thuoc]    Script Date: 12/21/2011 22:36:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhomThuoc_Thuoc](
	[NhomThuoc_ThuocGUID] [uniqueidentifier] NOT NULL,
	[NhomThuocGUID] [uniqueidentifier] NOT NULL,
	[ThuocGUID] [uniqueidentifier] NOT NULL,
	[Note] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_NhomThuoc_Thuoc_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_NhomThuoc_Thuoc] PRIMARY KEY CLUSTERED 
(
	[NhomThuoc_ThuocGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[NhomThuoc_Thuoc]  WITH CHECK ADD  CONSTRAINT [FK_NhomThuoc_Thuoc_NhomThuoc] FOREIGN KEY([NhomThuocGUID])
REFERENCES [dbo].[NhomThuoc] ([NhomThuocGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NhomThuoc_Thuoc]  WITH CHECK ADD  CONSTRAINT [FK_NhomThuoc_Thuoc_Thuoc] FOREIGN KEY([ThuocGUID])
REFERENCES [dbo].[Thuoc] ([ThuocGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  Table [dbo].[LoThuoc]    Script Date: 12/21/2011 22:37:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoThuoc](
	[LoThuocGUID] [uniqueidentifier] NOT NULL,
	[ThuocGUID] [uniqueidentifier] NOT NULL,
	[MaLoThuoc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TenLoThuoc] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SoDangKy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NhaPhanPhoi] [nvarchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HangSanXuat] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NgaySanXuat] [datetime] NOT NULL,
	[NgayHetHan] [datetime] NOT NULL,
	[SoLuongNhap] [int] NOT NULL,
	[GiaNhap] [float] NOT NULL,
	[DonViTinhNhap] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SoLuongQuiDoi] [int] NOT NULL CONSTRAINT [DF_LoThuoc_SoLuongQuiDoi]  DEFAULT ((0)),
	[DonViTinhQuiDoi] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SoLuongXuat] [int] NOT NULL CONSTRAINT [DF_LoThuoc_SoLuongXuat]  DEFAULT ((0)),
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_LoThuoc_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_LoThuoc] PRIMARY KEY CLUSTERED 
(
	[LoThuocGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [MM]
GO
ALTER TABLE [dbo].[LoThuoc]  WITH CHECK ADD  CONSTRAINT [FK_LoThuoc_Thuoc] FOREIGN KEY([ThuocGUID])
REFERENCES [dbo].[Thuoc] ([ThuocGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  Table [dbo].[GiaThuoc]    Script Date: 12/21/2011 22:37:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiaThuoc](
	[GiaThuocGUID] [uniqueidentifier] NOT NULL,
	[ThuocGUID] [uniqueidentifier] NOT NULL,
	[GiaBan] [float] NOT NULL,
	[NgayApDung] [datetime] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_GiaThuoc] PRIMARY KEY CLUSTERED 
(
	[GiaThuocGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [MM]
GO
ALTER TABLE [dbo].[GiaThuoc]  WITH CHECK ADD  CONSTRAINT [FK_GiaThuoc_Thuoc] FOREIGN KEY([ThuocGUID])
REFERENCES [dbo].[Thuoc] ([ThuocGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  Table [dbo].[ToaThuoc]    Script Date: 12/21/2011 22:38:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToaThuoc](
	[ToaThuocGUID] [uniqueidentifier] NOT NULL,
	[NgayKeToa] [datetime] NOT NULL,
	[BacSiKeToa] [uniqueidentifier] NOT NULL,
	[BenhNhan] [uniqueidentifier] NOT NULL,
	[Note] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL CONSTRAINT [DF_ToaThuoc_Status]  DEFAULT ((0)),
 CONSTRAINT [PK_ToaThuoc] PRIMARY KEY CLUSTERED 
(
	[ToaThuocGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [MM]
GO
ALTER TABLE [dbo].[ToaThuoc]  WITH CHECK ADD  CONSTRAINT [FK_ToaThuoc_DocStaff] FOREIGN KEY([BacSiKeToa])
REFERENCES [dbo].[DocStaff] ([DocStaffGUID])
GO
ALTER TABLE [dbo].[ToaThuoc]  WITH CHECK ADD  CONSTRAINT [FK_ToaThuoc_Patient] FOREIGN KEY([BenhNhan])
REFERENCES [dbo].[Patient] ([PatientGUID])
GO
/****** Object:  Table [dbo].[ChiTietToaThuoc]    Script Date: 12/21/2011 22:38:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietToaThuoc](
	[ChiTietToaThuocGUID] [uniqueidentifier] NOT NULL,
	[ToaThuocGUID] [uniqueidentifier] NOT NULL,
	[ThuocGUID] [uniqueidentifier] NOT NULL,
	[SoNgayUong] [int] NOT NULL,
	[SoLanTrongNgay] [int] NOT NULL,
	[SoLuongTrongLan] [int] NOT NULL,
	[Note] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
	[DeletedBy] [uniqueidentifier] NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_ChiTietToaThuoc] PRIMARY KEY CLUSTERED 
(
	[ChiTietToaThuocGUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [MM]
GO
ALTER TABLE [dbo].[ChiTietToaThuoc]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietToaThuoc_Thuoc] FOREIGN KEY([ThuocGUID])
REFERENCES [dbo].[Thuoc] ([ThuocGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietToaThuoc]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietToaThuoc_ToaThuoc] FOREIGN KEY([ToaThuocGUID])
REFERENCES [dbo].[ToaThuoc] ([ToaThuocGUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  View [dbo].[NhomThuoc_ThuocView]    Script Date: 12/21/2011 22:39:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[NhomThuoc_ThuocView]
AS
SELECT     dbo.NhomThuoc_Thuoc.NhomThuoc_ThuocGUID, dbo.NhomThuoc_Thuoc.NhomThuocGUID, dbo.NhomThuoc_Thuoc.ThuocGUID, 
                      dbo.NhomThuoc_Thuoc.Status AS NhomThuoc_ThuocStatus, dbo.Thuoc.MaThuoc, dbo.Thuoc.TenThuoc, dbo.Thuoc.BietDuoc, dbo.Thuoc.Note, 
                      dbo.Thuoc.Status AS ThuocStatus
FROM         dbo.NhomThuoc_Thuoc INNER JOIN
                      dbo.Thuoc ON dbo.NhomThuoc_Thuoc.ThuocGUID = dbo.Thuoc.ThuocGUID
GO
/****** Object:  View [dbo].[LoThuocView]    Script Date: 12/21/2011 22:40:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[LoThuocView]
AS
SELECT     dbo.LoThuoc.LoThuocGUID, dbo.LoThuoc.ThuocGUID, dbo.LoThuoc.MaLoThuoc, dbo.LoThuoc.TenLoThuoc, dbo.LoThuoc.SoDangKy, 
                      dbo.LoThuoc.NgaySanXuat, dbo.LoThuoc.HangSanXuat, dbo.LoThuoc.NgayHetHan, dbo.LoThuoc.NhaPhanPhoi, dbo.LoThuoc.SoLuongNhap, 
                      dbo.LoThuoc.DonViTinhNhap, dbo.LoThuoc.GiaNhap, dbo.LoThuoc.SoLuongQuiDoi, dbo.LoThuoc.DonViTinhQuiDoi, dbo.LoThuoc.SoLuongXuat, 
                      dbo.LoThuoc.UpdatedDate, dbo.LoThuoc.UpdatedBy, dbo.LoThuoc.DeletedBy, dbo.LoThuoc.Status AS LoThuocStatus, dbo.Thuoc.MaThuoc, 
                      dbo.Thuoc.TenThuoc, dbo.Thuoc.Status AS ThuocStatus, dbo.LoThuoc.DeletedDate, dbo.LoThuoc.CreatedBy, dbo.LoThuoc.CreatedDate
FROM         dbo.LoThuoc INNER JOIN
                      dbo.Thuoc ON dbo.LoThuoc.ThuocGUID = dbo.Thuoc.ThuocGUID
GO
/****** Object:  View [dbo].[GiaThuocView]    Script Date: 12/21/2011 22:40:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GiaThuocView]
AS
SELECT     dbo.GiaThuoc.GiaThuocGUID, dbo.GiaThuoc.ThuocGUID, dbo.GiaThuoc.GiaBan, dbo.GiaThuoc.NgayApDung, dbo.GiaThuoc.CreatedDate, 
                      dbo.GiaThuoc.CreatedBy, dbo.GiaThuoc.UpdatedDate, dbo.GiaThuoc.UpdatedBy, dbo.GiaThuoc.DeletedDate, dbo.GiaThuoc.DeletedBy, 
                      dbo.GiaThuoc.Status AS GiaThuocStatus, dbo.Thuoc.MaThuoc, dbo.Thuoc.TenThuoc, dbo.Thuoc.DonViTinh, dbo.Thuoc.Status AS ThuocStatus
FROM         dbo.GiaThuoc INNER JOIN
                      dbo.Thuoc ON dbo.GiaThuoc.ThuocGUID = dbo.Thuoc.ThuocGUID
GO
/****** Object:  View [dbo].[ChiTietToaThuocView]    Script Date: 12/21/2011 22:40:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ChiTietToaThuocView]
AS
SELECT     dbo.ChiTietToaThuoc.ChiTietToaThuocGUID, dbo.ChiTietToaThuoc.ToaThuocGUID, dbo.ChiTietToaThuoc.ThuocGUID, 
                      dbo.ChiTietToaThuoc.SoNgayUong, dbo.ChiTietToaThuoc.SoLanTrongNgay, dbo.ChiTietToaThuoc.SoLuongTrongLan, dbo.ChiTietToaThuoc.Note, 
                      dbo.ChiTietToaThuoc.CreatedDate, dbo.ChiTietToaThuoc.CreatedBy, dbo.ChiTietToaThuoc.UpdatedDate, dbo.ChiTietToaThuoc.UpdatedBy, 
                      dbo.ChiTietToaThuoc.DeletedDate, dbo.ChiTietToaThuoc.DeletedBy, dbo.ChiTietToaThuoc.Status AS ChiTietToaThuocStatus, dbo.Thuoc.MaThuoc, 
                      dbo.Thuoc.TenThuoc, dbo.Thuoc.DonViTinh, dbo.Thuoc.Status AS ThuocStatus
FROM         dbo.ChiTietToaThuoc INNER JOIN
                      dbo.Thuoc ON dbo.ChiTietToaThuoc.ThuocGUID = dbo.Thuoc.ThuocGUID
GO
/****** Object:  StoredProcedure [dbo].[spGetCheckListByContract]    Script Date: 12/21/2011 22:41:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spGetCheckListByContract]
	@ContractGUID nvarchar(50),
	@PatientGUID nvarchar(50)
AS
BEGIN
	SELECT C.CompanyGUID, C.CompanyContractGUID, L.ServiceGUID, BeginDate, CAST(0 AS bit) AS Using 
	INTO TMP
	FROM CompanyContract C, CompanyCheckList L, CompanyMember M,
	ContractMember CM
	WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
	CM.ContractMemberGUID = L.ContractMemberGUID AND 
	CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
	C.Completed = 'False' AND C.CompanyGUID = M.CompanyGUID AND 
	M.PatientGUID = @PatientGUID AND CM.Status = 0 AND
	M.Status = 0 AND L.Status = 0 AND C.CompanyContractGUID = @ContractGUID

	UPDATE TMP
	SET Using = 'True'
	FROM ServiceHistory S
	WHERE S.ServiceGUID = TMP.ServiceGUID AND
	S.PatientGUID = @PatientGUID AND
	S.ActivedDate > TMP.BeginDate AND S.Status = 0

	SELECT CompanyGUID, CompanyContractGUID, S.ServiceGUID, Code, [Name],
	BeginDate, Using, CAST(0 AS Bit) AS Checked
	FROM TMP, Services S
	WHERE TMP.ServiceGUID = S.ServiceGUID
	ORDER BY TMP.Using, S.[Name]

	DROP TABLE TMP
END
GO
/****** Object:  StoredProcedure [dbo].[spMerge2Patients]    Script Date: 12/21/2011 22:42:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spMerge2Patients] 
(
	@KeepGUID nvarchar(50),
	@MergedGUID nvarchar(50),
	@DoneByGUID nvarchar(50)
)
AS

	BEGIN TRANSACTION MergePatient
	--update CompanyMember
	UPDATE dbo.CompanyMember set PatientGUID=@KeepGUID  where PatientGUID=@MergedGUID
	IF @@ERROR <> 0
	BEGIN
		-- Rollback the transaction
		ROLLBACK
		RETURN
	END
	
	Update dbo.ContractMember Set CompanyMemberGUID=@KeepGUID  where CompanyMemberGUID=@MergedGUID
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
	
	Update dbo.ServiceHistory set PatientGUID=@KeepGUID  where PatientGUID = @MergedGUID
	IF @@ERROR <> 0
		BEGIN
		-- Rollback the transaction
		ROLLBACK
		RETURN
	END
	
	--set deleted for 2nd patietn
	Update dbo.Contact Set Archived=1, DeletedDate = GETDATE(),DeletedBy=@DoneByGUID, Note= Note + ' Merged with patient ' + @KeepGUID   where ContactGUID= (select ContactGUID from dbo.Patient where PatientGUID= @MergedGUID)
	IF @@ERROR <> 0
		BEGIN
		-- Rollback the transaction
		ROLLBACK
		RETURN
	END
	
	COMMIT TRANSACTION MergePatient;
GO
/****** Object:  StoredProcedure [dbo].[spDoanhThuNhanVienTongHop]    Script Date: 12/21/2011 22:44:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spDoanhThuNhanVienTongHop]
	@FromDate datetime,
	@ToDate datetime,
	@DocStaffGUID nvarchar(50),
	@Type tinyint --0: ServiceHistory; 1: Receipt
AS
BEGIN
	IF (@Type = 0)
	BEGIN
		SELECT @FromDate AS FromDate, @ToDate AS ToDate, D.FullName, SUM(S.Price) AS Revenue 
		FROM ServiceHistory S, DocStaffView D
		WHERE S.DocStaffGUID = D.DocStaffGUID AND 
		S.ActivedDate BETWEEN @FromDate AND @ToDate AND
		(@DocStaffGUID = '00000000-0000-0000-0000-000000000000' OR S.DocStaffGUID = @DocStaffGUID) AND
		S.Status = 0
		GROUP BY D.DocStaffGUID, D.FullName
		ORDER BY D.FullName
	END
	ELSE IF (@Type = 1)
	BEGIN
		SELECT @FromDate AS FromDate, @ToDate AS ToDate, D.FullName, SUM(S.Price) AS Revenue 
		FROM ServiceHistory S, DocStaffView D, Receipt R, ReceiptDetail T
		WHERE S.DocStaffGUID = D.DocStaffGUID AND R.ReceiptGUID = T.ReceiptGUID AND
		T.ServiceHistoryGUID = S.ServiceHistoryGUID AND R.Status = 0 AND
		R.ReceiptDate BETWEEN @FromDate AND @ToDate AND
		(@DocStaffGUID = '00000000-0000-0000-0000-000000000000' OR S.DocStaffGUID = @DocStaffGUID) 
		GROUP BY D.DocStaffGUID, D.FullName
		ORDER BY D.FullName
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spDoanhThuNhanVienChiTiet]    Script Date: 12/21/2011 22:44:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spDoanhThuNhanVienChiTiet]
	@FromDate datetime,
	@ToDate datetime,
	@DocStaffGUID nvarchar(50),
	@Type tinyint --0: ServiceHistory; 1: Receipt
AS
BEGIN
	IF (@Type = 0)
	BEGIN
		SELECT @FromDate AS FromDate, @ToDate AS ToDate, D.FullName, 
		CAST(CAST(DATEPART(year ,ActivedDate) AS nvarchar) + '/' +
		CAST(DATEPART(month ,ActivedDate) AS nvarchar) + '/' +
		CAST(DATEPART(day ,ActivedDate) AS nvarchar) AS datetime) AS  ActivedDate, SUM(S.Price) AS Revenue 
		FROM ServiceHistory S, DocStaffView D
		WHERE S.DocStaffGUID = D.DocStaffGUID AND 
		S.ActivedDate BETWEEN @FromDate AND @ToDate AND
		(@DocStaffGUID = '00000000-0000-0000-0000-000000000000' OR S.DocStaffGUID = @DocStaffGUID) AND
		S.Status = 0
		GROUP BY D.DocStaffGUID, D.FullName, CAST(CAST(DATEPART(year ,ActivedDate) AS nvarchar) + '/' +
		CAST(DATEPART(month ,ActivedDate) AS nvarchar) + '/' +
		CAST(DATEPART(day ,ActivedDate) AS nvarchar) AS datetime)
		ORDER BY D.FullName, ActivedDate
	END
	ELSE IF (@Type = 1)
	BEGIN
		SELECT @FromDate AS FromDate, @ToDate AS ToDate, D.FullName, 
		CAST(CAST(DATEPART(year ,ReceiptDate) AS nvarchar) + '/' +
		CAST(DATEPART(month ,ReceiptDate) AS nvarchar) + '/' +
		CAST(DATEPART(day ,ReceiptDate) AS nvarchar) AS datetime) AS  ActivedDate, SUM(S.Price) AS Revenue 
		FROM ServiceHistory S, DocStaffView D, Receipt R, ReceiptDetail T
		WHERE S.DocStaffGUID = D.DocStaffGUID AND R.ReceiptGUID = T.ReceiptGUID AND
		T.ServiceHistoryGUID = S.ServiceHistoryGUID AND R.Status = 0 AND
		R.ReceiptDate BETWEEN @FromDate AND @ToDate AND
		(@DocStaffGUID = '00000000-0000-0000-0000-000000000000' OR S.DocStaffGUID = @DocStaffGUID) 
		GROUP BY D.DocStaffGUID, D.FullName, CAST(CAST(DATEPART(year ,ReceiptDate) AS nvarchar) + '/' +
		CAST(DATEPART(month ,ReceiptDate) AS nvarchar) + '/' +
		CAST(DATEPART(day ,ReceiptDate) AS nvarchar) AS datetime)
		ORDER BY D.FullName, ActivedDate
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spDichVuHopDong]    Script Date: 12/21/2011 22:44:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spDichVuHopDong]
	@ContractGUID nvarchar(50)
AS
BEGIN
	DECLARE @PatientGUID nvarchar(50)	
	DECLARE @FullName nvarchar(255)

	CREATE TABLE #TEMP
	(
		ContractName nvarchar(500),
		FullName nvarchar(255),
		ServiceName nvarchar(200),
		Using nvarchar(50)
	)	

	DECLARE db_cursor CURSOR FOR  
	SELECT PatientGUID, FullName
	FROM dbo.ContractMemberView
	WHERE CompanyContractGUID = @ContractGUID AND Status = 0 AND 
	CompanyMemberStatus = 0 AND Archived = 'False'
	
	OPEN db_cursor   
	FETCH NEXT FROM db_cursor INTO @PatientGUID, @FullName 
	WHILE @@FETCH_STATUS = 0   
	BEGIN   
		SELECT C.ContractName, L.ServiceGUID, BeginDate, CAST(N'Chưa Khám' AS nvarchar) AS Using 
		INTO TMP
		FROM CompanyContract C, CompanyCheckList L, CompanyMember M,
		ContractMember CM
		WHERE C.CompanyContractGUID = CM.CompanyContractGUID AND
		CM.ContractMemberGUID = L.ContractMemberGUID AND 
		CM.CompanyMemberGUID = M.CompanyMemberGUID AND 
		C.Completed = 'False' AND C.CompanyGUID = M.CompanyGUID AND 
		M.PatientGUID = @PatientGUID AND CM.Status = 0 AND
		M.Status = 0 AND L.Status = 0 AND C.CompanyContractGUID = @ContractGUID

		UPDATE TMP
		SET Using = N'Đã Khám'
		FROM ServiceHistory S
		WHERE S.ServiceGUID = TMP.ServiceGUID AND
		S.PatientGUID = @PatientGUID AND
		S.ActivedDate > TMP.BeginDate AND S.Status = 0

		INSERT INTO #TEMP(ContractName, FullName, ServiceName, Using)
		SELECT ContractName, @FullName, [Name] AS ServiceName, Using
		FROM TMP, Services S
		WHERE TMP.ServiceGUID = S.ServiceGUID

		DROP TABLE 	TMP	

		FETCH NEXT FROM db_cursor INTO @PatientGUID, @FullName    
	END   

	CLOSE db_cursor   
	DEALLOCATE db_cursor

	SELECT * FROM #TEMP ORDER BY FullName, ServiceName
	
	DROP TABLE #TEMP

	--SELECT 'ABC' AS ContractName, 'ABC' AS FullName, 'ABC' AS ServiceName, 'Đã khám' AS Using
END
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'ec57fb37-bb1e-40eb-a6f9-5ed7c4af3d21', N'DuplicatePatient', N'Bệnh nhân trùng lặp')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'5e902caa-8c27-417b-9a27-cddb9de608ce', N'DoanhThuNhanVien', N'Báo cáo doanh thu nhân viên')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'9dd2f4f0-365a-4570-bd53-c8411bb0658c', N'DichVuHopDong', N'Báo cáo dịch vụ hợp đồng')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'c72a34e9-b01a-4c1b-82cd-7c45578fcc97', N'Thuoc', N'Thuốc')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'a0146055-ded1-42b9-93ee-ab43195d9455', N'NhomThuoc', N'Nhóm thuốc')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'f413879e-46ff-448b-a9ae-2c5c647d5374', N'LoThuoc', N'Lô thuốc')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'6ad340c1-476c-41d7-b4e2-a4f8639473b3', N'GiaThuoc', N'Giá thuốc')
GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'2d6f7c4d-6472-4ed2-86ca-6193a7eff020', N'KeToa', N'Kê toa')