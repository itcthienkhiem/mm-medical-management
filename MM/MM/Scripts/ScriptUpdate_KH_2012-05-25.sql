USE MM
GO
/****** Object:  Table [dbo].[MaxNgayXetNghiem]    Script Date: 05/25/2012 14:51:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaxNgayXetNghiem](
	[MaxNgayXetNghiemGUID] [uniqueidentifier] NOT NULL,
	[PatientGUID] [uniqueidentifier] NOT NULL,
	[MaxNgayXetNghiem] [datetime] NOT NULL,
	[LoaiXN] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_MaxNgayXetNghiem] PRIMARY KEY CLUSTERED 
(
	[MaxNgayXetNghiemGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[spDanhSachNhanVienDenKham]    Script Date: 05/25/2012 15:45:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spDanhSachNhanVienDenKham]
	@FromDate datetime,
	@ToDate datetime,
	@MaBenhNhan nvarchar(50)
AS
BEGIN
	SELECT P.PatientGUID, Max(S.ActivedDate) AS NgayKham, P.FileNum, P.FullName, 
	P.DobStr, P.GenderAsStr, P.Address 
	FROM PatientView P, ServiceHistory S 
	WHERE P.PatientGUID = S.PatientGUID AND 
	S.ActivedDate BETWEEN @FromDate AND @ToDate AND 
	P.FileNum LIKE @MaBenhNhan AND Archived = 'False' AND S.Status = 0 
	GROUP BY P.PatientGUID, P.FullName, P.FileNum, P.GenderAsStr, P.Address, P.DobStr 
	ORDER BY NgayKham
END
