using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MM.Common
{
    public enum ServiceType : byte
    {
        CanLamSang = 0,
        LamSang
    };

    public enum ActionType : byte
    {
        Add = 0,
        Edit,
        Delete
    };

    public enum TrackingType : byte
    {
        None = 0,
        Price,
        Count
    };

    public enum DrawType
    {
        None,
        Line,
        Pencil
    };

    public enum ErrorCode : int
    {
        OK = 0,
        NO_DATA,
        INVALID_SQL_STATEMENT,
        NO_AUTHORIZED,
        NO_DATA_TO_ANALYZE,
        SQL_QUERY_TIMEOUT,
        UNKNOWN_ERROR,
        INVALID_CONNECTION_INFO,
        INVALID_SERVERNAME,
        INVALID_DATABASENAME,
        INVALID_USERNAME,
        INVALID_PASSWORD,
        DATA_INCONSISTENT,
        LOCK,
        DELETED,
        NO_UPDATED,
        CANCEL_UPDATED,
        EXIST,
        NOT_EXIST
    };

    public enum Gender : int
    {
        Male = 0,
        Female,
        None
    };

    public enum IconType : int
    {
        Information,
        Question,
        Error
    };

    public enum LoaiToaThuoc : byte
    {
        Chung = 0,
        SanKhoa
    };

    public enum MsgBoxType
    {
        OK,
        YesNo
    };

    public enum StaffType : int
    {
        BacSi = 0,
        DieuDuong,
        LeTan,
        BenhNhan,
        Admin,
        XetNghiem,
        ThuKyYKhoa,
        Sale,
        KeToan,
        None,
        BacSiSieuAm,
        BacSiNgoaiTongQuat,
        BacSiNoiTongQuat,
        BacSiPhuKhoa
    };

    public enum Status : byte
    {
        Actived = 0,
        Deactived
    };

    public enum WorkType : int
    {
        FullTime = 0,
        PartTime
    };

    public enum PaymentType : int
    {
        TienMat = 0,
        ChuyenKhoan,
        TienMat_ChuyenKhoan
    };

    public enum CoQuan : int
    {
        Mat = 0,
        TaiMuiHong,
        RangHamMat,
        HoHap,
        TimMach,
        TieuHoa,
        TietNieuSinhDuc,
        CoXuongKhop,
        DaLieu,
        ThanKinh,
        NoiTiet,
        Khac, 
        KhamPhuKhoa
    };

    public enum LoaiNoiSoi : byte
    {
        Tai = 0,
        Mui,
        Hong_ThanhQuan,
        TaiMuiHong,
        TongQuat
    };

    public enum BookMarkType : int
    {
        KetLuanNoiSoiTai = 0,
        KetLuanNoiSoiMui,
        KetLuanNoiSoiHongThanhQuan,
        KetLuanNoiSoiTaiMuiHong,
        KetLuanNoiSoiTongQuat,

        DeNghiNoiSoiTai,
        DeNghiNoiSoiMui,
        DeNghiNoiSoiHongThanhQuan,
        DeNghiNoiSoiTaiMuiHong,
        DeNghiNoiSoiTongQuat,

        KetQuaNoiSoiOngTai,
        KetQuaNoiSoiMangNhi,
        KetQuaNoiSoiCanBua,
        KetQuaNoiSoiHomNhi,
        KetQuaNoiSoiValsava,

        KetQuaNoiSoiNiemMac,
        KetQuaNoiSoiVachNgan,
        KetQuaNoiSoiKheTren,
        KetQuaNoiSoiKheGiua,
        KetQuaNoiSoiCuonGiua,
        KetQuaNoiSoiCuonDuoi,
        KetQuaNoiSoiMomMoc,
        KetQuaNoiSoiBongSang,
        KetQuaNoiSoiVom,

        KetQuaNoiSoiAmydale,
        KetQuaNoiSoiXoangLe,
        KetQuaNoiSoiMiengThucQuan,
        KetQuaNoiSoiSunPheu,
        KetQuaNoiSoiDayThanh,
        KetQuaNoiSoiBangThanhThat,

        KetQuaNoiSoiMomMoc_BongSang,
        KetQuaNoiSoiThanhQuan,

        KetQuaSoiAmHo,
        KetQuaSoiAmDao,
        KetQuaSoiCTC,
        KetQuaSoiBieuMoLat,
        KetQuaSoiMoDem,
        KetQuaSoiRanhGioiLatTru,
        KetQuaSoiSauAcidAcetic,
        KetQuaSoiSauLugol,
        KetLuanSoiCTCT
    };

    public enum LoaiHoaDon : int
    {
        HoaDonThuoc = 0,
        HoaDonDichVu,
        HoaDonXuatTruoc,
        HoaDonHopDong
    };

    public enum LockType : int
    {
        HopDong = 0
    };

    public enum BookingType : int
    {
        Monitor = 0,
        BloodTaking
    };

    public enum DoiTuong : byte
    {
        Chung = 0,
        Chung_Sau2h,
        Nam,
        Nu,
        TreEm,
        NguoiLon,
        NguoiCaoTuoi
    };

    public enum LoaiMayXN : byte
    {
        Hitachi917,
        CellDyn3200
    };

    public enum TinhTrang : byte
    {
        BinhThuong = 0,
        BatThuong
    };

    public enum AgeUnit : byte
    {
        Unknown = 0,
        Days,
        Months,
        Years
    };

    public enum LoaiXetNghiem : byte
    {
        Biochemistry = 0,
        Urine,
        Electrolytes,
        Haematology
    }
}
