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


