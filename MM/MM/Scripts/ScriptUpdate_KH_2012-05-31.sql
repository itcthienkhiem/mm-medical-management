USE MM
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET TenXetNghiem = N'Neu', Fullname = N'Neu'
WHERE TenXetNghiem = 'NEU'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET TenXetNghiem = N'Lym', Fullname = N'Lym'
WHERE TenXetNghiem = 'LYM'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET TenXetNghiem = N'Mono', Fullname = N'Mono'
WHERE TenXetNghiem = 'MONO'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET TenXetNghiem = N'Eos', Fullname = N'Eos'
WHERE TenXetNghiem = 'EOS'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET TenXetNghiem = N'Baso', Fullname = N'Baso'
WHERE TenXetNghiem = 'BASO'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET TenXetNghiem = N'Hb', Fullname = N'Hb'
WHERE TenXetNghiem = 'HGB'
GO
UPDATE dbo.XetNghiem_CellDyn3200
SET TenXetNghiem = N'Hct', Fullname = N'Hct'
WHERE TenXetNghiem = 'HCT'
GO
UPDATE dbo.ChiTietKetQuaXetNghiem_CellDyn3200
SET TenXetNghiem = N'Hb'
WHERE TenXetNghiem = N'HGB'
GO
/****** Object:  Table [dbo].[User]    Script Date: 05/31/2012 23:28:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[CustomerId] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Password] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[AccountView]    Script Date: 05/31/2012 23:28:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AccountView]
AS
SELECT     dbo.[User].*, dbo.PatientView.FullName, dbo.PatientView.DobStr, dbo.PatientView.PatientGUID, dbo.PatientView.GenderAsStr, dbo.PatientView.Address, 
                      dbo.PatientView.Archived
FROM         dbo.[User] INNER JOIN
                      dbo.PatientView ON dbo.[User].CustomerId = dbo.PatientView.FileNum

GO
INSERT [dbo].[Function] ([FunctionGUID], [FunctionCode], [FunctionName]) VALUES (N'1f557797-0cb2-4acc-b76a-0129e8904368', N'TraCuuThongTinKhachHang', N'Tra cứu thông tin khách hàng')

