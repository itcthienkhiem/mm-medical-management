USE MM
GO
ALTER TABLE ChiTietMauHoSo
ADD [HopDongGUID] [uniqueidentifier] NOT NULL
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[ChiTietMauHoSoView]
AS
SELECT     dbo.ChiTietMauHoSo.ChiTietMauHoSoGUID, dbo.ChiTietMauHoSo.MauHoSoGUID, dbo.ChiTietMauHoSo.HopDongGUID, dbo.ChiTietMauHoSo.ServiceGUID, 
                      dbo.Services.Code, dbo.Services.Name
FROM         dbo.ChiTietMauHoSo INNER JOIN
                      dbo.Services ON dbo.ChiTietMauHoSo.ServiceGUID = dbo.Services.ServiceGUID
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


