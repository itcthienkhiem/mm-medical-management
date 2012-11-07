using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MM.Common
{
    public class Global
    {
        public static string AppConfig = string.Format("{0}\\App.cfg", AppDomain.CurrentDomain.BaseDirectory);
        public static string FTPUploadPath = string.Format("{0}\\FTPUpload", AppDomain.CurrentDomain.BaseDirectory);
        public static string UsersPath = string.Format("{0}\\Users", AppDomain.CurrentDomain.BaseDirectory);
        public static string UsersFilePath = string.Format("{0}\\Users\\Users.txt", AppDomain.CurrentDomain.BaseDirectory);
        public static string FTPFolder = "domains/healthcare.com.vn/public_html/report/images/";
        public static ConnectionInfo ConnectionInfo = new ConnectionInfo();
        public static FTPConnectionInfo FTPConnectionInfo = new FTPConnectionInfo();
        public static int AlertDays = 3;
        public static int AlertSoNgayHetHanCapCuu = 3;
        public static int AlertSoLuongHetTonKhoCapCuu = 10;
        public static string UserGUID = string.Empty;
        public static string LogonGUID = string.Empty;
        public static string Fullname = string.Empty;
        public static string Password = string.Empty;
        public static StaffType StaffType = Common.StaffType.Admin;
        public static bool AllowShowServiePrice = true;
        public static bool AllowPrintReceipt = true;
        public static bool AllowExportReceipt = true;
        public static bool AllowExportInvoice = true;
        public static bool AllowPrintInvoice = true;
        public static string PrintLabelConfigPath = string.Format("{0}\\PrintLabelConfig.xml", AppDomain.CurrentDomain.BaseDirectory);
        public static PrintLabelConfig PrintLabelConfig = new PrintLabelConfig();
        public static bool AllowViewChiDinh = true;
        public static bool AllowAddChiDinh = true;
        public static bool AllowEditChiDinh = true;
        public static bool AllowDeleteChiDinh = true;
        public static bool AllowConfirmChiDinh = true;
        public static bool AllowAddPhongCho = true;

        //Dịch vụ đã sử dụng
        public static bool AllowViewDichVuDaSuDung = true;
        public static bool AllowAddDichVuDaSuDung = true;
        public static bool AllowEditDichVuDaSuDung = true;
        public static bool AllowDeleteDichVuDaSuDung = true;
        public static bool AllowExportDichVuDaSuDung = true;

        //Cân đo
        public static bool AllowViewCanDo = true;
        public static bool AllowAddCanDo = true;
        public static bool AllowEditCanDo = true;
        public static bool AllowDeleteCanDo = true;

        //Khám lâm sàng
        public static bool AllowViewKhamLamSang = true;
        public static bool AllowAddKhamLamSang = true;
        public static bool AllowEditKhamLamSang = true;
        public static bool AllowDeleteKhamLamSang = true;

        //Lời khuyên
        public static bool AllowViewLoiKhuyen = true;
        public static bool AllowAddLoiKhuyen = true;
        public static bool AllowEditLoiKhuyen = true;
        public static bool AllowDeleteLoiKhuyen = true;

        //Kết luận
        public static bool AllowViewKetLuan = true;
        public static bool AllowAddKetLuan = true;
        public static bool AllowEditKetLuan = true;
        public static bool AllowDeleteKetLuan = true;

        //Khám nội soi
        public static bool AllowViewKhamNoiSoi = true;
        public static bool AllowAddKhamNoiSoi = true;
        public static bool AllowEditKhamNoiSoi = true;
        public static bool AllowDeleteKhamNoiSoi = true;
        public static bool AllowExportKhamNoiSoi = true;
        public static bool AllowPrintKhamNoiSoi = true;

        //Khám CTC
        public static bool AllowViewKhamCTC = true;
        public static bool AllowAddKhamCTC = true;
        public static bool AllowEditKhamCTC = true;
        public static bool AllowDeleteKhamCTC = true;
        public static bool AllowExportKhamCTC = true;
        public static bool AllowPrintKhamCTC = true;

        //Siêu Âm
        public static bool AllowViewSieuAm = true;
        public static bool AllowAddSieuAm = true;
        public static bool AllowEditSieuAm = true;
        public static bool AllowDeleteSieuAm = true;
        public static bool AllowExportSieuAm = true;
        public static bool AllowPrintSieuAm = true;
        
        //Kê toa
        public static bool AllowViewKeToa = true;

        //Danh Sách địa chỉ công ty
        public static bool AllowViewDSDiaChiCongTy = true;
        public static bool AllowAddDSDiaChiCongTy = true;
        public static bool AllowEditDSDiaChiCongTy = true;
        public static bool AllowDeleteDSDiaChiCongTy = true;

        //Tra cứu danh sách khách hàng
        public static bool AllowViewTraCuuDanhSachKhachHang = true;

        public static DateTime MinDateTime = new DateTime(1753, 1, 1);
        public static DateTime MaxDateTime = new DateTime(9997, 12, 31);

        public static bool IsStart = false;

        public static PortConfigCollection PortConfigCollection = new PortConfigCollection();
        public static string PortConfigPath = string.Format("{0}\\PortConfig.xml", AppDomain.CurrentDomain.BaseDirectory);
        public static DateTime NgayThayDoiSoHoaDonSauCung = MinDateTime;
        public static string MauSoSauCung = "01GTKT3/002";
        public static string KiHieuSauCung = "AA/12T";
        public static PageSetupConfig PageSetupConfig = new PageSetupConfig();
        public static string PageSetupConfigPath = string.Format("{0}\\PageSetupConfig.xml", AppDomain.CurrentDomain.BaseDirectory);
        public static List<string> ExcelTemplates = new List<string>();

        public static void InitExcelTempates()
        {
            ExcelTemplates.Add(Const.CheckListTemplate);
            ExcelTemplates.Add(Const.ChiTietPhieuThuDichVuTemplate);
            ExcelTemplates.Add(Const.ChiTietPhieuThuThuocTemplate);
            ExcelTemplates.Add(Const.DanhSachBenhNhanDenKhamTemplate);
            ExcelTemplates.Add(Const.DanhSachBenhNhanTemplate);
            ExcelTemplates.Add(Const.DichVuHopDongTemplate);
            ExcelTemplates.Add(Const.DichVuTuTucTemplate);
            ExcelTemplates.Add(Const.DoanhThuTheoNgayTemplate);
            ExcelTemplates.Add(Const.GiaVonDichVuTemplate);
            ExcelTemplates.Add(Const.HDGTGTTemplate);
            ExcelTemplates.Add(Const.KetQuaNoiSoiHongThanhQuanTemplate);
            ExcelTemplates.Add(Const.KetQuaNoiSoiMuiTemplate);
            ExcelTemplates.Add(Const.KetQuaNoiSoiTaiMuiHongTemplate);
            ExcelTemplates.Add(Const.KetQuaNoiSoiTaiTemplate);
            ExcelTemplates.Add(Const.KetQuaNoiSoiTongQuatTemplate);
            ExcelTemplates.Add(Const.KetQuaSoiCTCTemplate);
            ExcelTemplates.Add(Const.KhamSucKhoeTongQuatTemplate);
            ExcelTemplates.Add(Const.NhatKyLienHeCongTyTemplate);
            ExcelTemplates.Add(Const.PhieuThuThuocTemplate);
            ExcelTemplates.Add(Const.PhieuThuDichVuTemplate);
            ExcelTemplates.Add(Const.TrieuChungTemplate);
            ExcelTemplates.Add(Const.ThuocTonKhoTheoKhoangThoiGianTemplate);
            ExcelTemplates.Add(Const.ToaThuocTemplate);
            ExcelTemplates.Add(Const.ToaThuocChungTemplate);
            ExcelTemplates.Add(Const.ToaThuocSanKhoaTemplate);
            ExcelTemplates.Add(Const.YKienKhachHangTemplate);
            ExcelTemplates.Add(Const.KetQuaXetNghiemCellDyn3200Template);
            ExcelTemplates.Add(Const.KetQuaXetNghiemSinhHoaTemplate);
            ExcelTemplates.Add(Const.DanhSachDichVuXuatPhieuThuTemplate);
            ExcelTemplates.Add(Const.DanhSachDichVuTemplate);
            ExcelTemplates.Add(Const.DanhSachThuocTemplate);
            ExcelTemplates.Add(Const.DanhSachNhanVienTemplate);
            ExcelTemplates.Add(Const.CongTacNgoaiGioTemplate);
            ExcelTemplates.Add(Const.LichKhamTemplate);
            ExcelTemplates.Add(Const.BenhNhanNgoaiGoiKhamTemplate);
            ExcelTemplates.Add(Const.PhieuThuCapCuuTemplate);
            ExcelTemplates.Add(Const.ChiTietPhieuThuCapCuuTemplate);

            ExcelTemplates.Sort();
        }
    }
}
