using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MM.Common
{
    public class Global
    {
        public static string HDGTGTSettingsPath = string.Format("{0}\\HDGTGTSettings.xml", AppDomain.CurrentDomain.BaseDirectory);
        public static HDGTGTSettings HDGTGTSettings = new HDGTGTSettings();
        public static string AppConfig = string.Format("{0}\\App.cfg", AppDomain.CurrentDomain.BaseDirectory);
        public static string FTPUploadPath = string.Format("{0}\\FTPUpload", AppDomain.CurrentDomain.BaseDirectory);
        public static string UsersPath = string.Format("{0}\\Users", AppDomain.CurrentDomain.BaseDirectory);
        public static string UsersFilePath = string.Format("{0}\\Users\\Users.txt", AppDomain.CurrentDomain.BaseDirectory);
        public static string FTPFolder = "domains/healthcare.com.vn/public_html/report/images";
        public static string HoSoPath = string.Format("{0}\\HoSo", AppDomain.CurrentDomain.BaseDirectory);
        public static string HinhChupPath = string.Format("{0}\\HinhChup", AppDomain.CurrentDomain.BaseDirectory);
        public static string MailConfigPath = string.Format("{0}\\MailConfig.xml", AppDomain.CurrentDomain.BaseDirectory);
        public static ConnectionInfo ConnectionInfo = new ConnectionInfo();
        public static FTPConnectionInfo FTPConnectionInfo = new FTPConnectionInfo();
        public static MailConfig MailConfig = new MailConfig();
        public static TVHomeConfig TVHomeConfig = new TVHomeConfig();
        public static int AlertDays = 3;
        public static int AlertSoNgayHetHanCapCuu = 3;
        public static int AlertSoLuongHetTonKhoCapCuu = 10;
        public static string UserGUID = string.Empty;
        public static string LogonGUID = string.Empty;
        public static string Fullname = string.Empty;
        public static string Password = string.Empty;
        public static StaffType StaffType = Common.StaffType.Admin;
        public static bool AllowShowServiePrice = true;

        //Phiếu thu dịch vụ
        public static bool AllowExportPhieuThuDichVu = true;
        public static bool AllowPrintPhieuThuDichVu = true;

        //Phiếu thu thuốc
        public static bool AllowExportPhieuThuThuoc = true;
        public static bool AllowPrintPhieuThuThuoc = true;

        //Phiếu thu hợp đồng
        public static bool AllowExportPhieuThuHopDong = true;
        public static bool AllowPrintPhieuThuHopDong = true;

        //Phiếu thu cấp cứu
        public static bool AllowExportPhieuThuCapCuu = true;
        public static bool AllowPrintPhieuThuCapCuu = true;

        //Hóa đơn dịch vụ
        public static bool AllowExportHoaDonDichVu = true;
        public static bool AllowPrintHoaDonDichVu = true;

        //Hóa đơn thuốc
        public static bool AllowExportHoaDonThuoc = true;
        public static bool AllowPrintHoaDonThuoc = true;

        //Hóa đơn hợp đồng
        public static bool AllowExportHoaDonHopDong = true;
        public static bool AllowPrintHoaDonHopDong = true;

        //Hóa đơn xuất trước
        public static bool AllowExportHoaDonXuatTruoc = true;
        public static bool AllowPrintHoaDonXuatTruoc = true;

        //Hóa đơn xét nghiệm
        public static bool AllowExportHoaDonXetNghiem = true;
        public static bool AllowPrintHoaDonXetNghiem = true;

        public static string PrintLabelConfigPath = string.Format("{0}\\PrintLabelConfig.xml", AppDomain.CurrentDomain.BaseDirectory);
        public static PrintLabelConfig PrintLabelConfig = new PrintLabelConfig();
        public static bool AllowViewChiDinh = true;
        public static bool AllowAddChiDinh = true;
        public static bool AllowEditChiDinh = true;
        public static bool AllowDeleteChiDinh = true;
        public static bool AllowConfirmChiDinh = true;
        public static bool AllowPrintChiDinh = true;
        public static bool AllowAddPhongCho = true;

        //Dịch vụ đã sử dụng
        public static bool AllowViewDichVuDaSuDung = true;
        public static bool AllowAddDichVuDaSuDung = true;
        public static bool AllowEditDichVuDaSuDung = true;
        public static bool AllowDeleteDichVuDaSuDung = true;

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
        public static bool AllowAddKeToa = true;
        public static bool AllowEditKeToa = true;
        public static bool AllowDeleteKeToa = true;
        public static bool AllowPrintKeToa = true;

        //kết quả cận lâm sàng
        public static bool AllowViewCanLamSang = true;
        public static bool AllowAddCanLamSang = true;
        public static bool AllowEditCanLamSang = true;
        public static bool AllowDeleteCanLamSang = true;

        //Danh Sách địa chỉ công ty
        public static bool AllowViewDSDiaChiCongTy = true;
        public static bool AllowAddDSDiaChiCongTy = true;
        public static bool AllowEditDSDiaChiCongTy = true;
        public static bool AllowDeleteDSDiaChiCongTy = true;

        //Tra cứu danh sách khách hàng
        public static bool AllowViewTraCuuDanhSachKhachHang = true;

        //Y kiến khách hàng
        public static bool AllowAddYKienKhachHang = true;

        //Hồ sơ
        public static bool AllowTaoHoSo = true;
        public static bool AllowUploadHoSo = true;
        public static bool AllowAddMatKhauHoSo = true;

        //Dịch vụ
        public static bool AllowAddDichVu = true;

        //Thay đổi số hóa đơn
        public static bool AllowEditThayDoiSoHoaDon = true;
        public static bool AllowEditThayDoiSoHoaDonXetNghiem = true;

        public static DateTime MinDateTime = new DateTime(1753, 1, 1);
        public static DateTime MaxDateTime = new DateTime(9997, 12, 31);

        public static bool IsStart = false;

        public static PortConfigCollection PortConfigCollection = new PortConfigCollection();
        public static string PortConfigPath = string.Format("{0}\\PortConfig.xml", AppDomain.CurrentDomain.BaseDirectory);
        public static DateTime NgayThayDoiSoHoaDonSauCung = MinDateTime;
        public static string MauSoSauCung = "01GTKT3/002";
        public static string KiHieuSauCung = "AA/12T";
        public static int SoHoaDonBatDau = 1;
        public static PageSetupConfig PageSetupConfig = new PageSetupConfig();
        public static string PageSetupConfigPath = string.Format("{0}\\PageSetupConfig.xml", AppDomain.CurrentDomain.BaseDirectory);
        public static List<string> ExcelTemplates = new List<string>();

        public static DateTime NgayThayDoiSoHoaDonXetNghiemSauCung = MinDateTime;
        public static string MauSoXetNghiemSauCung = "01GTKT2/001";
        public static string KiHieuXetNghiemSauCung = "VG/13T";
        public static int SoHoaDonXetNghiemBatDau = 1;

        public static DataTable dtOpenPatient = null;

        public static List<string> IgnorePermissions = new List<string>();

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
            ExcelTemplates.Add(Const.BaoCaoCongNoHopDongTongHopTemplate);
            ExcelTemplates.Add(Const.BaoCaoCongNoHopDongChiTietTemplate);
            ExcelTemplates.Add(Const.ChiDinhTemplate);
            ExcelTemplates.Add(Const.DichVuXetNghiemTemplate);
            ExcelTemplates.Add(Const.ThongKeThuocXuatHoaDonTemplate);
            ExcelTemplates.Add(Const.DoanhThuTheoNhomDichVuTemplate);

            ExcelTemplates.Sort();
        }

        public static void InitIgnorePermissions()
        {
            IgnorePermissions.Add("7ED3B385-1985-4F0D-9A08-139FC27D01F5");
            IgnorePermissions.Add("F413879E-46FF-448B-A9AE-2C5C647D5374");
            IgnorePermissions.Add("DDEACD98-AF98-4461-B9C6-00532F2B837C");
            IgnorePermissions.Add("AD62CAF9-BF02-4285-9198-07A7C5A7C535");
            IgnorePermissions.Add("D9EB1168-A84B-4FE8-9FBF-07C4172DBBC5");
            IgnorePermissions.Add("011BD492-2879-487F-9959-1338364D1FF8");
            IgnorePermissions.Add("A7B7DA37-278B-44AC-8974-284CC68D8485");
            IgnorePermissions.Add("32CBF46D-DDA9-49A1-9ABE-2BD18F911516");
            IgnorePermissions.Add("2AE953E4-E23A-4A43-8A81-312D80117244");
            IgnorePermissions.Add("34295971-43C4-4A9D-BE85-364C24DAD998");
            IgnorePermissions.Add("DAB2005C-7EB0-4228-882C-4D9E7042E095");
            IgnorePermissions.Add("71C67741-4D3C-445D-8E2F-575BDF32BEE3");
            IgnorePermissions.Add("F4EEC3A1-7177-4531-A279-8140D12CE1E3");
            IgnorePermissions.Add("CCCB6998-6FC3-4EEC-A66A-888FABFF1811");
            IgnorePermissions.Add("8536D855-34C6-4D5E-8E3D-922D97E19602");
            IgnorePermissions.Add("30F15859-92F7-415F-A8BB-9A4AF1883980");
            IgnorePermissions.Add("6AD340C1-476C-41D7-B4E2-A4F8639473B3");
            IgnorePermissions.Add("18D1087A-76D8-40E8-B7E5-B0AF2130CB1C");
            IgnorePermissions.Add("1C0268F0-0A47-4193-B54F-CDC5B2E24A92");
            IgnorePermissions.Add("5E902CAA-8C27-417B-9A27-CDDB9DE608CE");
            IgnorePermissions.Add("2A43B652-1227-4B1C-8BC2-CE63BF90DE95");
            IgnorePermissions.Add("7977F594-FBC9-4CEE-A8A0-D18B768F24C4");
            IgnorePermissions.Add("D9D6F20D-40ED-41FA-9D4E-E2D4F41E29A2");
            IgnorePermissions.Add("0763C052-318A-4198-B7F7-E9929B42C2F9");
            IgnorePermissions.Add("E1F9F5D1-4239-4B4A-82D3-FCE1CB025152");
            IgnorePermissions.Add("2BDDF71D-8B33-4778-93A6-FF43BC13E2EA");
            IgnorePermissions.Add("2FB57B95-23D7-4B9F-B2B2-927F209D7BF1");
        }
    }
}
