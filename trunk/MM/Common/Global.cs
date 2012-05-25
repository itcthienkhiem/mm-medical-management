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
        public static string FTPFolder = "MMTest";
        public static ConnectionInfo ConnectionInfo = new ConnectionInfo();
        public static FTPConnectionInfo FTPConnectionInfo = new FTPConnectionInfo();
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
        
        //Kê toa
        public static bool AllowViewKeToa = true;

        public static DateTime MinDateTime = new DateTime(1753, 1, 1);
        public static DateTime MaxDateTime = new DateTime(9997, 12, 31);

        public static bool IsStart = false;

        public static PortConfigCollection PortConfigCollection = new PortConfigCollection();
        public static string PortConfigPath = string.Format("{0}\\PortConfig.xml", AppDomain.CurrentDomain.BaseDirectory);
        public static DateTime NgayThayDoiSoHoaDonSauCung = MinDateTime;
    }
}
