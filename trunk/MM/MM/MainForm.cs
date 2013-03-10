using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using MM.Common;
using MM.Databasae;
using MM.Controls;
using MM.Dialogs;
using MM.Bussiness;
using DicomImageViewer;
using System.Diagnostics;
using System.IO.Ports;

namespace MM
{
    public partial class MainForm : dlgBase
    {
        #region Members
        private bool _flag = true;
        //private List<SerialPort> _ports = new List<SerialPort>();
        //private Hashtable _htLastResult = new Hashtable();
        private bool _isStartTiemNgua = false;
        private bool _isStartCapCuuHetHSD = false;
        private bool _isStartCapCuuHetTonKho = false;

        private uUserGroupList _uUserGroupList = new uUserGroupList();
        private uNguoiSuDungList _uNguoiSuDungList = new uNguoiSuDungList();
        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();

            _uPatientList.OnOpenPatientEvent += new OpenPatientHandler(_uPatientList_OnOpenPatient);
            _uCompanyList.OnOpenPatientEvent += new OpenPatientHandler(_uPatientList_OnOpenPatient);
            _uContractList.OnOpenPatientEvent += new OpenPatientHandler(_uPatientList_OnOpenPatient);
            _uPhongChoList.OnOpenPatientEvent += new OpenPatientHandler(_uPatientList_OnOpenPatient);
            _uKhamHopDong.OnOpenPatientEvent += new OpenPatientHandler(_uPatientList_OnOpenPatient);
            _uBenhNhanThanThuocList.OnOpenPatientEvent += new OpenPatientHandler(_uPatientList_OnOpenPatient);

            Utility.CreateFolder(Global.UsersPath);

            InitControl();

            //string pass = "hoTn+aDg1LDgMf+9Hdt+ZA==";
            //RijndaelCrypto cryto = new RijndaelCrypto();
            //pass = cryto.Decrypt(pass);
        }
        #endregion

        #region UI Command
        private void InitControl()
        {
            xuatKhoCapCuuToolStripMenuItem.Visible = false;
            permissionToolStripMenuItem.Visible = false;

            _mainPanel.Controls.Add(_uUserGroupList);
            _uUserGroupList.Dock = DockStyle.Fill;
            _uUserGroupList.Visible = false;

            _mainPanel.Controls.Add(_uNguoiSuDungList);
            _uNguoiSuDungList.Dock = DockStyle.Fill;
            _uNguoiSuDungList.Visible = false;
        }

        private void StartTimerShowAlert()
        {
            _timerShowAlert.Enabled = true;
            _timerShowAlert.Start();
        }

        private void StopTimerShowAlert()
        {
            _timerShowAlert.Stop();
            _timerShowAlert.Enabled = false;
            statusAlert.Visible = false;
            statusCapCuuHetHan.Visible = false;
            statusCapCuuHetTonKho.Visible = false;
        }

        private void StartTimerCheckAlert()
        {
            _timerCheckAlert.Enabled = true;
            _timerCheckAlert.Start();
        }

        private void StopTimerCheckAlert()
        {
            _timerCheckAlert.Stop();
            _timerCheckAlert.Enabled = false;
        }

        private void OnInitConfig()
        {
            MethodInvoker method = delegate
            {
                if (File.Exists(Global.AppConfig))
                {
                    Configuration.LoadData(Global.AppConfig);

                    object obj = Configuration.GetValues(Const.ServerNameKey);
                    if (obj != null) Global.ConnectionInfo.ServerName = Convert.ToString(obj);

                    obj = Configuration.GetValues(Const.DatabaseNameKey);
                    if (obj != null) Global.ConnectionInfo.DatabaseName = Convert.ToString(obj);

                    obj = Configuration.GetValues(Const.AuthenticationKey);
                    if (obj != null) Global.ConnectionInfo.Authentication = Convert.ToString(obj);

                    obj = Configuration.GetValues(Const.UserNameKey);
                    if (obj != null) Global.ConnectionInfo.UserName = Convert.ToString(obj);

                    obj = Configuration.GetValues(Const.PasswordKey);
                    if (obj != null)
                    {
                        string password = Convert.ToString(obj);
                        RijndaelCrypto crypto = new RijndaelCrypto();
                        Global.ConnectionInfo.Password = crypto.Decrypt(password);
                    }

                    obj = Configuration.GetValues(Const.FTPServerNameKey);
                    if (obj != null) Global.FTPConnectionInfo.ServerName = Convert.ToString(obj);

                    obj = Configuration.GetValues(Const.FTPUserNameKey);
                    if (obj != null) Global.FTPConnectionInfo.Username = Convert.ToString(obj);

                    obj = Configuration.GetValues(Const.FTPPasswordKey);
                    if (obj != null)
                    {
                        string password = Convert.ToString(obj);
                        RijndaelCrypto crypto = new RijndaelCrypto();
                        Global.FTPConnectionInfo.Password = crypto.Decrypt(password);
                    }

                    obj = Configuration.GetValues(Const.AlertDayKey);
                    if (obj != null) Global.AlertDays = Convert.ToInt32(obj);

                    obj = Configuration.GetValues(Const.AlertSoNgayHetHanCapCuuKey);
                    if (obj != null) Global.AlertSoNgayHetHanCapCuu = Convert.ToInt32(obj);

                    obj = Configuration.GetValues(Const.AlertSoLuongHetTonKhoCapCuuKey);
                    if (obj != null) Global.AlertSoLuongHetTonKhoCapCuu = Convert.ToInt32(obj);

                    //TVHome
                    obj = Configuration.GetValues(Const.TVHomePathKey);
                    if (obj != null) Global.TVHomeConfig.Path = Convert.ToString(obj);

                    obj = Configuration.GetValues(Const.TVHomeSoiCTCKey);
                    if (obj != null) Global.TVHomeConfig.SuDungSoiCTC = Convert.ToBoolean(obj);

                    obj = Configuration.GetValues(Const.TVHomeSieuAmKey);
                    if (obj != null) Global.TVHomeConfig.SuDungSieuAm = Convert.ToBoolean(obj);

                    if (!Global.ConnectionInfo.TestConnection())
                    {
                        dlgDatabaseConfig dlg = new dlgDatabaseConfig();
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            dlg.SetAppConfig();
                            SaveAppConfig();
                            //Utility.ResetMMSerivice();
                            RefreshData();
                            OnLogin();
                        }
                        else
                        {
                            if (!Global.ConnectionInfo.TestConnection())
                            {
                                _flag = false;
                                this.Close();
                            }
                        }
                    }
                    else
                        OnLogin();
                }
                else
                {
                    dlgDatabaseConfig dlg = new dlgDatabaseConfig();
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        dlg.SetAppConfig();
                        SaveAppConfig();
                        //Utility.ResetMMSerivice();
                        RefreshData();
                        OnLogin();
                    }
                    else
                    {
                        _flag = false;
                        this.Close();
                    }
                }

                if (!File.Exists(Global.PrintLabelConfigPath))
                    Global.PrintLabelConfig.Serialize(Global.PrintLabelConfigPath);

                if (File.Exists(Global.PortConfigPath))
                    Global.PortConfigCollection.Deserialize(Global.PortConfigPath);

                InitPageSetup();

                Result result = QuanLySoHoaDonBus.GetThayDoiSoHoaSonSauCung();
                if (result.IsOK)
                {
                    if (result.QueryResult != null)
                    {
                        NgayBatDauLamMoiSoHoaDon thayDoiSauCung = result.QueryResult as NgayBatDauLamMoiSoHoaDon;
                        Global.NgayThayDoiSoHoaDonSauCung = thayDoiSauCung.NgayBatDau;
                        Global.MauSoSauCung = thayDoiSauCung.MauSo;
                        Global.KiHieuSauCung = thayDoiSauCung.KiHieu;
                    }
                    
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("QuanLySoHoaDonBus.GetThayDoiSoHoaSonSauCung"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("QuanLySoHoaDonBus.GetThayDoiSoHoaSonSauCung"));
                }

                if (!Directory.Exists(Global.HinhChupPath))
                    Utility.CreateFolder(Global.HinhChupPath);

                //Init DataTable Open Patient
                result = PatientBus.GetPatientList(string.Empty, 0);
                if (result.IsOK)
                {
                    Global.dtOpenPatient = result.QueryResult as DataTable;
                    dgPatient.DataSource = Global.dtOpenPatient;
                }
                else
                    Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientList"));
            };

            if (InvokeRequired) BeginInvoke(method);
            else method.Invoke();
        }

        private void InitPageSetup()
        {
            Global.InitExcelTempates();

            if (File.Exists(Global.PageSetupConfigPath))
                Global.PageSetupConfig.Deserialize(Global.PageSetupConfigPath);
        }

        private void RefreshData()
        {
            Control ctrl = GetControlActive();
            if (ctrl == null) return;
            ctrl.Enabled = true;
            if (ctrl.GetType() == typeof(uServicesList))
                _uServicesList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uDocStaffList))
                _uDocStaffList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uPatientList))
                _uPatientList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uSpecialityList))
                _uSpecialityList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uSymptomList))
                _uSymptomList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uCompanyList))
                _uCompanyList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uContractList))
                _uContractList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uPermission))
                _uPermission.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uPrintLabel))
                _uPrintLabel.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uReceiptList))
                _uReceiptList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uInvoiceList))
                _uInvoiceList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uDuplicatePatient))
                _uDuplicatePatient.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uDoanhThuNhanVien))
                _uDoanhThuNhanVien.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uDichVuHopDong))
                _uDichVuHopDong.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uThuocList))
                _uThuocList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uNhomThuocList))
                _uNhomThuocList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uLoThuocList))
                _uLoThuocList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uGiaThuocList))
                _uGiaThuocList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uToaThuocList))
                _uToaThuocList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uBaoCaoThuocHetHan))
                _uBaoCaoThuocHetHan.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uBaoCaoThuocTonKho))
                _uBaoCaoThuocTonKho.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uPhieuThuThuocList))
                _uPhieuThuThuocList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uDichVuTuTuc))
                _uDichVuTuTuc.InitData();
            else if (ctrl.GetType() == typeof(uTrackingList))
                _uTrackingList.InitData();
            else if (ctrl.GetType() == typeof(uServiceGroupList))
                _uServiceGroupList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uInKetQuaKhamSucKhoeTongQuat))
                _uInKetQuaKhamSucKhoeTongQuat.InitData();
            else if (ctrl.GetType() == typeof(uGiaVonDichVuList))
                _uGiaVonDichVuList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uHoaDonThuocList))
                _uHoaDonThuocList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uHoaDonXuatTruoc))
                _uHoaDonXuatTruoc.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uThongKeHoaDon))
                _uThongKeHoaDon.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uPhucHoiBenhNhan))
                _uPhucHoiBenhNhan.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uPhieuThuHopDongList))
            {
                _uPhieuThuHopDongList.DisplayComboHopDong();
                _uPhieuThuHopDongList.DisplayAsThread();
            }
            else if (ctrl.GetType() == typeof(uHoaDonHopDongList))
                _uHoaDonHopDongList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uYKienKhachHangList))
                _uYKienKhachHangList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uNhatKyLienHeCongTy))
                _uNhatKyLienHeCongTy.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uBookingList))
                _uBookingList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uKetQuaXetNghiem_Hitachi917))
                _uKetQuaXetNghiem_Hitachi917.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uKetQuaXetNghiem_CellDyn3200))
                _uKetQuaXetNghiem_CellDyn3200.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uBaoCaoKhachHangMuaThuoc))
                _uBaoCaoKhachHangMuaThuoc.InitData();
            else if (ctrl.GetType() == typeof(uXetNghiemTay))
                _uXetNghiemTay.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uKetQuaXetNghiemTay))
                _uKetQuaXetNghiemTay.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uKetQuaXetNghiemTongHop))
                _uKetQuaXetNghiemTongHop.UpdateGUI();
            else if (ctrl.GetType() == typeof(uTraCuuThongTinKhachHang))
                _uTraCuuThongTinKhachHang.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uDiaChiCongTyList))
                _uDiaChiCongTyList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uChiTietPhieuThuDichVu))
                _uChiTietPhieuThuDichVu.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uBaoCaoThuocTonKhoTheoKhoangThoiGian))
                _uBaoCaoThuocTonKhoTheoKhoangThoiGian.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uBenhNhanThanThuocList))
                _uBenhNhanThanThuocList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uXetNghiem))
                _uXetNghiem.InitData();
            else if (ctrl.GetType() == typeof(uLoaiSieuAmList))
                _uLoaiSieuAmList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uTiemNguaList))
                _uTiemNguaList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uCongTacNgoaiGioList))
                _uCongTacNgoaiGioList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uLichKham))
                _uLichKham.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uKhoCapCuu))
                _uKhoCapCuu.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uNhapKhoCapCuuList))
                _uNhapKhoCapCuuList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uXuatKhoCapCuuList))
                _uXuatKhoCapCuuList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uBaoCaoCapCuuHetHan))
                _uBaoCaoCapCuuHetHan.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uBaoCaoTonKhoCapCuu))
                _uBaoCaoTonKhoCapCuu.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uThongBaoList))
                _uThongBaoList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uBenhNhanNgoaiGoiKhamList))
                _uBenhNhanNgoaiGoiKhamList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uGiaCapCuuList))
                _uGiaCapCuuList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uPhieuThuCapCuuList))
                _uPhieuThuCapCuuList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uUserGroupList))
                _uUserGroupList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uNguoiSuDungList))
                _uNguoiSuDungList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uToaCapCuuList))
                _uToaCapCuuList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uKhamHopDong))
                _uKhamHopDong.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uTinNhanMauList))
                _uTinNhanMauList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uSendSMS))
                _uSendSMS.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uBaoCaoCongNoHopDong))
                _uBaoCaoCongNoHopDong.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uSMSLog))
                _uSMSLog.DisplayAsThread();
        }

        private void SaveAppConfig()
        {
            Configuration.SetValues(Const.ServerNameKey, Global.ConnectionInfo.ServerName);
            Configuration.SetValues(Const.DatabaseNameKey, Global.ConnectionInfo.DatabaseName);
            Configuration.SetValues(Const.AuthenticationKey, Global.ConnectionInfo.Authentication);
            Configuration.SetValues(Const.UserNameKey, Global.ConnectionInfo.UserName);
            RijndaelCrypto crypto = new RijndaelCrypto();
            string password = crypto.Encrypt(Global.ConnectionInfo.Password);
            Configuration.SetValues(Const.PasswordKey, password);

            Configuration.SetValues(Const.FTPServerNameKey, Global.FTPConnectionInfo.ServerName);
            Configuration.SetValues(Const.FTPUserNameKey, Global.FTPConnectionInfo.Username);
            password = crypto.Encrypt(Global.FTPConnectionInfo.Password);
            Configuration.SetValues(Const.FTPPasswordKey, password);

            Configuration.SetValues(Const.AlertDayKey, Global.AlertDays);
            Configuration.SetValues(Const.AlertSoLuongHetTonKhoCapCuuKey, Global.AlertSoLuongHetTonKhoCapCuu);
            Configuration.SetValues(Const.AlertSoNgayHetHanCapCuuKey, Global.AlertSoNgayHetHanCapCuu);

            Configuration.SetValues(Const.TVHomePathKey, Global.TVHomeConfig.Path);
            Configuration.SetValues(Const.TVHomeSoiCTCKey, Global.TVHomeConfig.SuDungSoiCTC);
            Configuration.SetValues(Const.TVHomeSieuAmKey, Global.TVHomeConfig.SuDungSieuAm);

            Configuration.SaveData(Global.AppConfig);
        }

        private void RefreshFunction(bool isLogin)
        {
            if (Global.UserGUID == Guid.Empty.ToString()) //Admin
            {
                thayDoiSoHoaDonToolStripMenuItem.Enabled = isLogin;
                cauHinhFTPToolStripMenuItem.Enabled = isLogin;
            }
            else
            {
                thayDoiSoHoaDonToolStripMenuItem.Enabled = false;
                cauHinhFTPToolStripMenuItem.Enabled = false;
            }

            if (Global.StaffType != StaffType.Admin)
            {
                toolsToolStripMenuItem.Enabled = isLogin;
                changePasswordToolStripMenuItem.Enabled = isLogin;
                cauHinhTVHomeToolStripMenuItem.Enabled = isLogin;
                cauHinhPageSetupToolStripMenuItem.Enabled = isLogin;

                Global.AllowAddYKienKhachHang = false;
                Global.AllowShowServiePrice = false;
                Global.AllowExportPhieuThuDichVu = false;
                Global.AllowPrintPhieuThuDichVu = false;
                Global.AllowExportHoaDonDichVu = false;
                Global.AllowExportHoaDonThuoc = false;
                Global.AllowExportHoaDonHopDong = false;
                Global.AllowExportHoaDonXuatTruoc = false;
                Global.AllowPrintHoaDonDichVu = false;
                Global.AllowPrintHoaDonThuoc = false;
                Global.AllowPrintHoaDonHopDong = false;
                Global.AllowPrintHoaDonXuatTruoc = false;
                Global.AllowViewChiDinh = false;
                Global.AllowAddChiDinh = false;
                Global.AllowEditChiDinh = false;
                Global.AllowDeleteChiDinh = false;
                Global.AllowConfirmChiDinh = false;
                Global.AllowAddPhongCho = false;
                Global.AllowViewDichVuDaSuDung = false;
                Global.AllowAddDichVuDaSuDung = false;
                Global.AllowEditDichVuDaSuDung = false;
                Global.AllowDeleteDichVuDaSuDung = false;
                Global.AllowViewCanDo = false;
                Global.AllowAddCanDo = false;
                Global.AllowEditCanDo = false;
                Global.AllowDeleteCanDo = false;
                Global.AllowViewKhamLamSang = false;
                Global.AllowAddKhamLamSang = false;
                Global.AllowEditKhamLamSang = false;
                Global.AllowDeleteKhamLamSang = false;
                Global.AllowViewLoiKhuyen = false;
                Global.AllowAddLoiKhuyen = false;
                Global.AllowEditLoiKhuyen = false;
                Global.AllowDeleteLoiKhuyen = false;
                Global.AllowViewKetLuan = false;
                Global.AllowAddKetLuan = false;
                Global.AllowEditKetLuan = false;
                Global.AllowDeleteKetLuan = false;
                Global.AllowViewKhamNoiSoi = false;
                Global.AllowAddKhamNoiSoi = false;
                Global.AllowEditKhamNoiSoi = false;
                Global.AllowDeleteKhamNoiSoi = false;
                Global.AllowExportKhamNoiSoi = false;
                Global.AllowPrintKhamNoiSoi = false;
                Global.AllowViewKeToa = false;
                Global.AllowViewKhamCTC = false;
                Global.AllowAddKhamCTC = false;
                Global.AllowEditKhamCTC = false;
                Global.AllowDeleteKhamCTC = false;
                Global.AllowExportKhamCTC = false;
                Global.AllowPrintKhamCTC = false;
                Global.AllowViewDSDiaChiCongTy = false;
                Global.AllowAddDSDiaChiCongTy = false;
                Global.AllowEditDSDiaChiCongTy = false;
                Global.AllowDeleteDSDiaChiCongTy = false;
                Global.AllowViewTraCuuDanhSachKhachHang = false;
                Global.AllowViewSieuAm = false;
                Global.AllowAddSieuAm = false;
                Global.AllowEditSieuAm = false;
                Global.AllowDeleteSieuAm = false;
                Global.AllowExportSieuAm = false;
                Global.AllowPrintSieuAm = false;
                Global.AllowAddKeToa = false;
                Global.AllowEditKeToa = false;
                Global.AllowDeleteKeToa = false;
                Global.AllowPrintKeToa = false;
                Global.AllowViewCanLamSang = false;
                Global.AllowAddCanLamSang = false;
                Global.AllowEditCanLamSang = false;
                Global.AllowDeleteCanLamSang = false;
                Global.AllowTaoHoSo = false;
                Global.AllowUploadHoSo = false;
                Global.AllowAddMatKhauHoSo = false;

                Result result = LogonBus.GetPermission2(Global.LogonGUID);
                if (result.IsOK)
                {
                    DataTable dtPermission = result.QueryResult as DataTable;
                    if (dtPermission == null) return;
                    foreach (DataRow row in dtPermission.Rows)
                    {
                        string functionCode = row["FunctionCode"].ToString();
                        bool isView = Convert.ToBoolean(row["IsView"]);
                        bool isAdd = Convert.ToBoolean(row["IsAdd"]);
                        bool isEdit = Convert.ToBoolean(row["IsEdit"]);
                        bool isDelete = Convert.ToBoolean(row["IsDelete"]);
                        bool isPrint = Convert.ToBoolean(row["IsPrint"]);
                        bool isImport = Convert.ToBoolean(row["IsImport"]);
                        bool isExport = Convert.ToBoolean(row["IsExport"]);
                        bool isConfirm = Convert.ToBoolean(row["IsConfirm"]);
                        bool isLock = Convert.ToBoolean(row["IsLock"]);
                        bool isExportAll = Convert.ToBoolean(row["IsExportAll"]);
                        bool isCreateReport = Convert.ToBoolean(row["IsCreateReport"]);
                        bool isUpload = Convert.ToBoolean(row["IsUpload"]);
                        bool isSendSMS = Convert.ToBoolean(row["IsSendSMS"]);

                        if (functionCode == Const.DocStaff)
                        {
                            danhmụcToolStripMenuItem.Enabled = isLogin;
                            nhanVienToolStripMenuItem.Enabled = isView && isLogin;
                            tbDoctorList.Enabled = isView && isLogin;

                            _uDocStaffList.AllowAdd = isAdd;
                            _uDocStaffList.AllowEdit = isEdit;
                            _uDocStaffList.AllowDelete = isDelete;
                            _uDocStaffList.AllowPrint = isPrint;
                            _uDocStaffList.AllowExport = isExport;
                            _uDocStaffList.AllowImport = isImport;
                            _uDocStaffList.AllowLock = isLock;
                        }
                        else if (functionCode == Const.Patient)
                        {
                            patientListToolStripMenuItem.Enabled = isView && isLogin;
                            patientToolStripMenuItem.Enabled = isLogin;
                            tbPatientList.Enabled = isView && isLogin;
                            _uPatientList.AllowAdd = isAdd;
                            _uPatientList.AllowEdit = isEdit;
                            _uPatientList.AllowDelete = isDelete;
                            _uPatientList.AllowPrint = isPrint;
                            _uPatientList.AllowExport = isExport;
                            _uPatientList.AllowImport = isImport;
                            _uPatientList.AllowLock = isLock;
                        }
                        else if (functionCode == Const.Speciality)
                        {
                            chuyenKhoaToolStripMenuItem.Enabled = isView && isLogin;
                            tbSpecialityList.Enabled = isView && isLogin;
                            _uSpecialityList.AllowAdd = isAdd;
                            _uSpecialityList.AllowEdit = isEdit;
                            _uSpecialityList.AllowDelete = isDelete;
                            _uSpecialityList.AllowPrint = isPrint;
                            _uSpecialityList.AllowExport = isExport;
                            _uSpecialityList.AllowImport = isImport;
                            _uSpecialityList.AllowLock = isLock;
                        }
                        else if (functionCode == Const.Company)
                        {
                            companyListToolStripMenuItem.Enabled = isView && isLogin;
                            companyToolStripMenuItem.Enabled = isLogin;
                            tbCompanyList.Enabled = isView && isLogin;
                            _uCompanyList.AllowAdd = isAdd;
                            _uCompanyList.AllowEdit = isEdit;
                            _uCompanyList.AllowDelete = isDelete;
                            _uCompanyList.AllowPrint = isPrint;
                            _uCompanyList.AllowExport = isExport;
                            _uCompanyList.AllowImport = isImport;
                            _uCompanyList.AllowLock = isLock;
                        }
                        else if (functionCode == Const.Services)
                        {
                            serviceListToolStripMenuItem.Enabled = isView && isLogin;
                            servicesToolStripMenuItem.Enabled = isLogin;
                            tbServiceList.Enabled = isView && isLogin;
                            _uServicesList.AllowAdd = isAdd;
                            _uServicesList.AllowEdit = isEdit;
                            _uServicesList.AllowDelete = isDelete;
                            _uServicesList.AllowPrint = isPrint;
                            _uServicesList.AllowExport = isExport;
                            _uServicesList.AllowImport = isImport;
                            _uServicesList.AllowLock = isLock;
                        }
                        else if (functionCode == Const.ServicePrice)
                        {
                            _uServicesList.AllowShowServicePrice = isView && isLogin;
                            Global.AllowShowServiePrice = isView && isLogin;
                        }
                        else if (functionCode == Const.Contract)
                        {
                            contractListToolStripMenuItem.Enabled = isView && isLogin;
                            tbContractList.Enabled = isView && isLogin;
                            _uContractList.AllowAdd = isAdd;
                            _uContractList.AllowEdit = isEdit;
                            _uContractList.AllowDelete = isDelete;
                            _uContractList.AllowPrint = isPrint;
                            _uContractList.AllowExport = isExport;
                            _uContractList.AllowImport = isImport;
                            _uContractList.AllowLock = isLock;
                        }
                        else if (functionCode == Const.OpenPatient)
                        {
                            openPatientToolStripMenuItem.Enabled = isView && isLogin;
                            tbOpenPatient.Enabled = isView && isLogin;
                            _uPatientList.AllowOpenPatient = isView;
                            _uKhamHopDong.AllowOpenPatient = isView;
                        }
                        else if (functionCode == Const.Permission)
                        {
                            permissionToolStripMenuItem.Enabled = isView && isLogin;
                            _uPermission.AllowAdd = isAdd;
                            _uPermission.AllowEdit = isEdit;
                            _uPermission.AllowDelete = isDelete;
                            _uPermission.AllowPrint = isPrint;
                            _uPermission.AllowExport = isExport;
                            _uPermission.AllowImport = isImport;
                            _uPermission.AllowLock = isLock;
                        }
                        else if (functionCode == Const.Symptom)
                        {
                            trieuChungToolStripMenuItem.Enabled = isView && isLogin;
                            tbSympton.Enabled = isView && isLogin;
                            _uSymptomList.AllowAdd = isAdd;
                            _uSymptomList.AllowEdit = isEdit;
                            _uSymptomList.AllowDelete = isDelete;
                            _uSymptomList.AllowPrint = isPrint;
                            _uSymptomList.AllowExport = isExport;
                            _uSymptomList.AllowImport = isImport;
                            _uSymptomList.AllowLock = isLock;
                        }
                        else if (functionCode == Const.PrintLabel)
                        {
                            printLabelToolStripMenuItem.Enabled = isPrint && isLogin;
                        }
                        else if (functionCode == Const.Receipt)
                        {
                            receiptListToolStripMenuItem.Enabled = isView && isLogin;
                            receiptToolStripMenuItem.Enabled = isLogin;
                            tbReceiptList.Enabled = isView && isLogin;
                            _uReceiptList.AllowAdd = isAdd;
                            _uReceiptList.AllowEdit = isEdit;
                            _uReceiptList.AllowDelete = isDelete;
                            _uReceiptList.AllowPrint = isPrint;
                            _uReceiptList.AllowExport = isExport;
                            _uReceiptList.AllowImport = isImport;
                            _uReceiptList.AllowLock = isLock;

                            Global.AllowPrintPhieuThuDichVu = isPrint;
                        }
                        else if (functionCode == Const.Invoice)
                        {
                            invoiceToolStripMenuItem.Enabled = isLogin;
                            invoiceListToolStripMenuItem.Enabled = isView && isLogin;
                            tbInvoiceList.Enabled = isView && isLogin;
                            _uInvoiceList.AllowAdd = isAdd;
                            _uInvoiceList.AllowEdit = isEdit;
                            _uInvoiceList.AllowDelete = isDelete;
                            _uInvoiceList.AllowPrint = isPrint;
                            _uInvoiceList.AllowExport = isExport;
                            _uInvoiceList.AllowImport = isImport;
                            _uInvoiceList.AllowLock = isLock;

                            Global.AllowPrintHoaDonDichVu = isPrint;
                            Global.AllowExportHoaDonDichVu = isExport;
                        }
                        else if (functionCode == Const.DuplicatePatient)
                        {
                            DuplicatePatientToolStripMenuItem.Enabled = isView && isLogin;
                            tbDuplicatePatient.Enabled = isView && isLogin;
                            _uDuplicatePatient.AllowAdd = isAdd;
                            _uDuplicatePatient.AllowEdit = isEdit;
                            _uDuplicatePatient.AllowDelete = isDelete;
                            _uDuplicatePatient.AllowPrint = isPrint;
                            _uDuplicatePatient.AllowExport = isExport;
                            _uDuplicatePatient.AllowImport = isImport;
                            _uDuplicatePatient.AllowLock = isLock;
                        }
                        else if (functionCode == Const.DoanhThuNhanVien)
                        {
                            reportToolStripMenuItem.Enabled = isLogin;
                            doanhThuNhanVienToolStripMenuItem.Enabled = isView && isLogin;
                            _uDoanhThuNhanVien.AllowAdd = isAdd;
                            _uDoanhThuNhanVien.AllowEdit = isEdit;
                            _uDoanhThuNhanVien.AllowDelete = isDelete;
                            _uDoanhThuNhanVien.AllowPrint = isPrint;
                            _uDoanhThuNhanVien.AllowExport = isExport;
                            _uDoanhThuNhanVien.AllowImport = isImport;
                            _uDoanhThuNhanVien.AllowLock = isLock;
                        }
                        else if (functionCode == Const.DichVuHopDong)
                        {
                            dichVuHopDongToolStripMenuItem.Enabled = isView && isLogin;
                            _uDichVuHopDong.AllowAdd = isAdd;
                            _uDichVuHopDong.AllowEdit = isEdit;
                            _uDichVuHopDong.AllowDelete = isDelete;
                            _uDichVuHopDong.AllowPrint = isPrint;
                            _uDichVuHopDong.AllowExport = isExport;
                            _uDichVuHopDong.AllowImport = isImport;
                            _uDichVuHopDong.AllowLock = isLock;
                        }
                        else if (functionCode == Const.Thuoc)
                        {
                            thuocToolStripMenuItem.Enabled = isLogin;
                            danhMucThuocToolStripMenuItem.Enabled = isView && isLogin;
                            tbDanhMucThuoc.Enabled = isView && isLogin;
                            _uThuocList.AllowAdd = isAdd;
                            _uThuocList.AllowEdit = isEdit;
                            _uThuocList.AllowDelete = isDelete;
                            _uThuocList.AllowPrint = isPrint;
                            _uThuocList.AllowExport = isExport;
                            _uThuocList.AllowImport = isImport;
                            _uThuocList.AllowLock = isLock;
                        }
                        else if (functionCode == Const.NhomThuoc)
                        {
                            nhomThuocToolStripMenuItem.Enabled = isView && isLogin;
                            tbNhomThuoc.Enabled = isView && isLogin;
                            _uNhomThuocList.AllowAdd = isAdd;
                            _uNhomThuocList.AllowEdit = isEdit;
                            _uNhomThuocList.AllowDelete = isDelete;
                            _uNhomThuocList.AllowPrint = isPrint;
                            _uNhomThuocList.AllowExport = isExport;
                            _uNhomThuocList.AllowImport = isImport;
                            _uNhomThuocList.AllowLock = isLock;
                        }
                        else if (functionCode == Const.LoThuoc)
                        {
                            loThuocToolStripMenuItem.Enabled = isView & isLogin;
                            tbLoThuoc.Enabled = isView & isLogin;
                            _uLoThuocList.AllowAdd = isAdd;
                            _uLoThuocList.AllowEdit = isEdit;
                            _uLoThuocList.AllowDelete = isDelete;
                            _uLoThuocList.AllowPrint = isPrint;
                            _uLoThuocList.AllowExport = isExport;
                            _uLoThuocList.AllowImport = isImport;
                            _uLoThuocList.AllowLock = isLock;
                        }
                        else if (functionCode == Const.GiaThuoc)
                        {
                            giaThuocToolStripMenuItem.Enabled = isView & isLogin;
                            tbGiaThuoc.Enabled = isView & isLogin;
                            _uGiaThuocList.AllowAdd = isAdd;
                            _uGiaThuocList.AllowEdit = isEdit;
                            _uGiaThuocList.AllowDelete = isDelete;
                            _uGiaThuocList.AllowPrint = isPrint;
                            _uGiaThuocList.AllowExport = isExport;
                            _uGiaThuocList.AllowImport = isImport;
                            _uGiaThuocList.AllowLock = isLock;
                        }
                        else if (functionCode == Const.KeToa)
                        {
                            keToaToolStripMenuItem.Enabled = isView && isLogin;
                            tbKeToa.Enabled = isView && isLogin;
                            Global.AllowViewKeToa = isView;
                            Global.AllowAddKeToa = isAdd;
                            Global.AllowEditKeToa = isEdit;
                            Global.AllowDeleteKeToa = isDelete;
                            Global.AllowPrintKeToa = isPrint;

                            _uToaThuocList.AllowAdd = isAdd;
                            _uToaThuocList.AllowEdit = isEdit;
                            _uToaThuocList.AllowDelete = isDelete;
                            _uToaThuocList.AllowPrint = isPrint;
                            _uToaThuocList.AllowExport = isExport;
                            _uToaThuocList.AllowImport = isImport;
                            _uToaThuocList.AllowLock = isLock;
                        }
                        else if (functionCode == Const.ThuocHetHan)
                        {
                            thuocHetHanToolStripMenuItem.Enabled = isView && isLogin;
                            _uBaoCaoThuocHetHan.AllowAdd = isAdd;
                            _uBaoCaoThuocHetHan.AllowEdit = isEdit;
                            _uBaoCaoThuocHetHan.AllowDelete = isDelete;
                            _uBaoCaoThuocHetHan.AllowPrint = isPrint;
                            _uBaoCaoThuocHetHan.AllowExport = isExport;
                            _uBaoCaoThuocHetHan.AllowImport = isImport;
                            _uBaoCaoThuocHetHan.AllowLock = isLock;
                        }
                        else if (functionCode == Const.PhieuThuThuoc)
                        {
                            phieuThuThuocToolStripMenuItem.Enabled = isView && isLogin;
                            tbPhieuThuThuoc.Enabled = isView && isLogin;
                            _uPhieuThuThuocList.AllowAdd = isAdd;
                            _uPhieuThuThuocList.AllowEdit = isEdit;
                            _uPhieuThuThuocList.AllowDelete = isDelete;
                            _uPhieuThuThuocList.AllowPrint = isPrint;
                            _uPhieuThuThuocList.AllowExport = isExport;
                            _uPhieuThuThuocList.AllowImport = isImport;
                            _uPhieuThuThuocList.AllowLock = isLock;
                        }
                        else if (functionCode == Const.DichVuTuTuc)
                        {
                            dichVuTuTucToolStripMenuItem.Enabled = isView && isLogin;
                            _uDichVuTuTuc.AllowAdd = isAdd;
                            _uDichVuTuTuc.AllowEdit = isEdit;
                            _uDichVuTuTuc.AllowDelete = isDelete;
                            _uDichVuTuTuc.AllowPrint = isPrint;
                            _uDichVuTuTuc.AllowExport = isExport;
                            _uDichVuTuTuc.AllowImport = isImport;
                            _uDichVuTuTuc.AllowLock = isLock;
                        }
                        else if (functionCode == Const.ChiDinh)
                        {
                            Global.AllowViewChiDinh = isView;
                            Global.AllowAddChiDinh = isAdd;
                            Global.AllowEditChiDinh = isEdit;
                            Global.AllowDeleteChiDinh = isDelete;
                            Global.AllowConfirmChiDinh = isConfirm;
                        }
                        else if (functionCode == Const.Tracking)
                        {
                            trackingToolStripMenuItem.Enabled = isView && isLogin;
                        }
                        else if (functionCode == Const.ServiceGroup)
                        {
                            serviceGroupToolStripMenuItem.Enabled = isView && isLogin;
                            tbNhomDichVu.Enabled = isView && isLogin;
                            _uServiceGroupList.AllowAdd = isAdd;
                            _uServiceGroupList.AllowEdit = isEdit;
                            _uServiceGroupList.AllowDelete = isDelete;
                            _uServiceGroupList.AllowPrint = isPrint;
                            _uServiceGroupList.AllowExport = isExport;
                            _uServiceGroupList.AllowImport = isImport;
                            _uServiceGroupList.AllowLock = isLock;
                        }
                        else if (functionCode == Const.InKetQuaKhamSucKhoeTongQuat)
                        {
                            inKetQuaKhamSucKhoeTongQuatToolStripMenuItem.Enabled = isView && isLogin;
                            _uInKetQuaKhamSucKhoeTongQuat.AllowAdd = isAdd;
                            _uInKetQuaKhamSucKhoeTongQuat.AllowEdit = isEdit;
                            _uInKetQuaKhamSucKhoeTongQuat.AllowDelete = isDelete;
                            _uInKetQuaKhamSucKhoeTongQuat.AllowPrint = isPrint;
                            _uInKetQuaKhamSucKhoeTongQuat.AllowExport = isExport;
                            _uInKetQuaKhamSucKhoeTongQuat.AllowImport = isImport;
                            _uInKetQuaKhamSucKhoeTongQuat.AllowLock = isLock;
                        }
                        else if (functionCode == Const.GiaVonDichVu)
                        {
                            giaVonDichVuToolStripMenuItem.Enabled = isView & isLogin;
                            tbGiaVonDichVu.Enabled = isView & isLogin;
                            _uGiaVonDichVuList.AllowAdd = isAdd;
                            _uGiaVonDichVuList.AllowEdit = isEdit;
                            _uGiaVonDichVuList.AllowDelete = isDelete;
                            _uGiaVonDichVuList.AllowPrint = isPrint;
                            _uGiaVonDichVuList.AllowExport = isExport;
                            _uGiaVonDichVuList.AllowImport = isImport;
                            _uGiaVonDichVuList.AllowLock = isLock;
                        }
                        else if (functionCode == Const.DoanhThuTheoNgay)
                        {
                            doanhThuTheoNgayToolStripMenuItem.Enabled = isView & isLogin;

                        }
                        else if (functionCode == Const.PhongCho)
                        {
                            _uPhongChoList.AllowAdd = isAdd;
                            _uPhongChoList.AllowEdit = isEdit;
                            _uPhongChoList.AllowDelete = isDelete;
                            _uPhongChoList.AllowPrint = isPrint;
                            _uPhongChoList.AllowExport = isExport;
                            _uPhongChoList.AllowImport = isImport;
                            _uPhongChoList.AllowLock = isLock;
                            Global.AllowAddPhongCho = isAdd;
                            _uPhongChoList.IsEnableBtnRaPhongCho = isDelete & isLogin;
                        }
                        else if (functionCode == Const.DichVuChuaXuatPhieuThu)
                        {
                            dichVuChuaXuatPhieuThuToolStripMenuItem.Enabled = isView && isLogin;
                            _uBaoCaoDichVuChuaXuatPhieuThu.AllowAdd = isAdd;
                            _uBaoCaoDichVuChuaXuatPhieuThu.AllowEdit = isEdit;
                            _uBaoCaoDichVuChuaXuatPhieuThu.AllowDelete = isDelete;
                            _uBaoCaoDichVuChuaXuatPhieuThu.AllowPrint = isPrint;
                            _uBaoCaoDichVuChuaXuatPhieuThu.AllowExport = isExport;
                            _uBaoCaoDichVuChuaXuatPhieuThu.AllowImport = isImport;
                            _uBaoCaoDichVuChuaXuatPhieuThu.AllowLock = isLock;
                        }
                        else if (functionCode == Const.HoaDonThuoc)
                        {
                            invoiceToolStripMenuItem.Enabled = isLogin;
                            hoaDonThuocToolStripMenuItem.Enabled = isView && isLogin;

                            _uHoaDonThuocList.AllowAdd = isAdd;
                            _uHoaDonThuocList.AllowEdit = isEdit;
                            _uHoaDonThuocList.AllowDelete = isDelete;
                            _uHoaDonThuocList.AllowPrint = isPrint;
                            _uHoaDonThuocList.AllowExport = isExport;
                            _uHoaDonThuocList.AllowImport = isImport;
                            _uHoaDonThuocList.AllowLock = isLock;

                            Global.AllowPrintHoaDonThuoc = isPrint;
                            Global.AllowExportHoaDonThuoc = isExport;
                        }
                        else if (functionCode == Const.HoaDonXuatTruoc)
                        {
                            invoiceToolStripMenuItem.Enabled = isLogin;
                            hoaDonXuatTruocToolStripMenuItem.Enabled = isView && isLogin;

                            _uHoaDonXuatTruoc.AllowAdd = isAdd;
                            _uHoaDonXuatTruoc.AllowEdit = isEdit;
                            _uHoaDonXuatTruoc.AllowDelete = isDelete;
                            _uHoaDonXuatTruoc.AllowPrint = isPrint;
                            _uHoaDonXuatTruoc.AllowExport = isExport;
                            _uHoaDonXuatTruoc.AllowImport = isImport;
                            _uHoaDonXuatTruoc.AllowLock = isLock;

                            Global.AllowPrintHoaDonXuatTruoc = isPrint;
                            Global.AllowExportHoaDonXuatTruoc = isExport;
                        }
                        else if (functionCode == Const.DangKyHoaDonXuatTruoc)
                        {
                            _uHoaDonXuatTruoc.AllowAddDangKy = isAdd;
                            _uHoaDonXuatTruoc.AllowEditDangKy = isEdit;
                            _uHoaDonXuatTruoc.AllowDeleteDangKy = isDelete;
                        }
                        else if (functionCode == Const.ThongKeHoaDon)
                        {
                            thongKeHoaDonToolStripMenuItem.Enabled = isView && isLogin;
                            _uThongKeHoaDon.AllowPrint = isPrint;
                            _uThongKeHoaDon.AllowLock = isLock;
                            //Global.AllowPrintInvoice = isPrint;
                        }
                        else if (functionCode == Const.PhucHoiBenhNhan)
                        {
                            phucHoiBenhNhanToolStripMenuItem.Enabled = isView && isLogin;
                            _uPhucHoiBenhNhan.AllowAdd = isAdd;
                            _uPhucHoiBenhNhan.AllowEdit = isEdit;
                            _uPhucHoiBenhNhan.AllowDelete = isDelete;
                            _uPhucHoiBenhNhan.AllowPrint = isPrint;
                            _uPhucHoiBenhNhan.AllowExport = isExport;
                            _uPhucHoiBenhNhan.AllowImport = isImport;
                            _uPhucHoiBenhNhan.AllowLock = isLock;
                        }
                        else if (functionCode == Const.PhieuThuHopDong)
                        {
                            phieuThuHopDongToolStripMenuItem.Enabled = isView && isLogin;
                            _uPhieuThuHopDongList.AllowAdd = isAdd;
                            _uPhieuThuHopDongList.AllowEdit = isEdit;
                            _uPhieuThuHopDongList.AllowDelete = isDelete;
                            _uPhieuThuHopDongList.AllowPrint = isPrint;
                            _uPhieuThuHopDongList.AllowExport = isExport;
                            _uPhieuThuHopDongList.AllowImport = isImport;
                            _uPhieuThuHopDongList.AllowLock = isLock;
                        }
                        else if (functionCode == Const.HoaDonHopDong)
                        {
                            chamSocKhachHangToolStripMenuItem.Enabled = isLogin;
                            hoaDonHopDongToolStripMenuItem.Enabled = isView && isLogin;
                            _uHoaDonHopDongList.AllowAdd = isAdd;
                            _uHoaDonHopDongList.AllowEdit = isEdit;
                            _uHoaDonHopDongList.AllowDelete = isDelete;
                            _uHoaDonHopDongList.AllowPrint = isPrint;
                            _uHoaDonHopDongList.AllowExport = isExport;
                            _uHoaDonHopDongList.AllowImport = isImport;
                            _uHoaDonHopDongList.AllowLock = isLock;

                            Global.AllowPrintHoaDonHopDong = isPrint;
                            Global.AllowExportHoaDonHopDong = isExport;
                        }
                        else if (functionCode == Const.YKienKhachHang)
                        {
                            chamSocKhachHangToolStripMenuItem.Enabled = isLogin;
                            yKienKhachHangToolStripMenuItem.Enabled = isView && isLogin;
                            Global.AllowAddYKienKhachHang = isAdd;
                            _uYKienKhachHangList.AllowAdd = isAdd;
                            _uYKienKhachHangList.AllowEdit = isEdit;
                            _uYKienKhachHangList.AllowDelete = isDelete;
                            _uYKienKhachHangList.AllowPrint = isPrint;
                            _uYKienKhachHangList.AllowExport = isExport;
                            _uYKienKhachHangList.AllowImport = isImport;
                            _uYKienKhachHangList.AllowLock = isLock;
                        }
                        else if (functionCode == Const.NhatKyLienHeCongTy)
                        {
                            chamSocKhachHangToolStripMenuItem.Enabled = isLogin;
                            nhatKyLienHeCongTyToolStripMenuItem.Enabled = isView && isLogin;
                            _uNhatKyLienHeCongTy.AllowAdd = isAdd;
                            _uNhatKyLienHeCongTy.AllowEdit = isEdit;
                            _uNhatKyLienHeCongTy.AllowDelete = isDelete;
                            _uNhatKyLienHeCongTy.AllowPrint = isPrint;
                            _uNhatKyLienHeCongTy.AllowExport = isExport;
                            _uNhatKyLienHeCongTy.AllowImport = isImport;
                            _uNhatKyLienHeCongTy.AllowLock = isLock;
                            _uNhatKyLienHeCongTy.AllowExportAll = isExportAll;
                        }
                        else if (functionCode == Const.DichVuDaSuDung)
                        {
                            Global.AllowViewDichVuDaSuDung = isView;
                            Global.AllowAddDichVuDaSuDung = isAdd;
                            Global.AllowEditDichVuDaSuDung = isEdit;
                            Global.AllowDeleteDichVuDaSuDung = isDelete;
                            Global.AllowExportPhieuThuDichVu = isExport;
                        }
                        else if (functionCode == Const.CanDo)
                        {
                            Global.AllowViewCanDo = isView;
                            Global.AllowAddCanDo = isAdd;
                            Global.AllowEditCanDo = isEdit;
                            Global.AllowDeleteCanDo = isDelete;
                        }
                        else if (functionCode == Const.KhamLamSang)
                        {
                            Global.AllowViewKhamLamSang = isView;
                            Global.AllowAddKhamLamSang = isAdd;
                            Global.AllowEditKhamLamSang = isEdit;
                            Global.AllowDeleteKhamLamSang = isDelete;
                        }
                        else if (functionCode == Const.LoiKhuyen)
                        {
                            Global.AllowViewLoiKhuyen = isView;
                            Global.AllowAddLoiKhuyen = isAdd;
                            Global.AllowEditLoiKhuyen = isEdit;
                            Global.AllowDeleteLoiKhuyen = isDelete;
                        }
                        else if (functionCode == Const.KetLuan)
                        {
                            Global.AllowViewKetLuan = isView;
                            Global.AllowAddKetLuan = isAdd;
                            Global.AllowEditKetLuan = isEdit;
                            Global.AllowDeleteKetLuan = isDelete;
                        }
                        else if (functionCode == Const.KhamNoiSoi)
                        {
                            Global.AllowViewKhamNoiSoi = isView;
                            Global.AllowAddKhamNoiSoi = isAdd;
                            Global.AllowEditKhamNoiSoi = isEdit;
                            Global.AllowDeleteKhamNoiSoi = isDelete;
                            Global.AllowExportKhamNoiSoi = isExport;
                            Global.AllowPrintKhamNoiSoi = isPrint;
                        }
                        else if (functionCode == Const.KhamCTC)
                        {
                            Global.AllowViewKhamCTC = isView;
                            Global.AllowAddKhamCTC = isAdd;
                            Global.AllowEditKhamCTC = isEdit;
                            Global.AllowDeleteKhamCTC = isDelete;
                            Global.AllowExportKhamCTC = isExport;
                            Global.AllowPrintKhamCTC = isPrint;
                        }
                        else if (functionCode == Const.Booking)
                        {
                            bookingToolStripMenuItem.Enabled = isView && isLogin;
                            _uBookingList.AllowAdd = isAdd;
                            _uBookingList.AllowEdit = isEdit;
                            _uBookingList.AllowDelete = isDelete;
                            _uBookingList.AllowPrint = isPrint;
                            _uBookingList.AllowExport = isExport;
                            _uBookingList.AllowImport = isImport;
                            _uBookingList.AllowLock = isLock;
                            _uBookingList.AllowExportAll = isExportAll;
                        }
                        else if (functionCode == Const.XetNghiem)
                        {
                            xetNghiemToolStripMenuItem.Enabled = isLogin;
                            xetNghiemHiTachi917ToolStripMenuItem.Enabled = isView && isLogin;
                            xetNghiemCellDyn3200ToolStripMenuItem.Enabled = isView && isLogin;
                            xetNghiemTayToolStripMenuItem.Enabled = isView && isLogin;
                            ketQuaXetNghiemTayToolStripMenuItem.Enabled = isView && isLogin;
                            ketQuaXetNghiemTongQuatToolStripMenuItem.Enabled = isView && isLogin;
                            danhSachXetNghiemHitachi917ToolStripMenuItem.Enabled = isView && isLogin;
                            danhSachXetNghiemCellDyn3200ToolStripMenuItem.Enabled = isView && isLogin;

                            _uXetNghiem.AllowView = isView;
                            _uXetNghiem.AllowAdd = isAdd;
                            _uXetNghiem.AllowEdit = isEdit;
                            _uXetNghiem.AllowDelete = isDelete;
                            _uXetNghiem.AllowPrint = isPrint;
                            _uXetNghiem.AllowExport = isExport;
                            _uXetNghiem.AllowImport = isImport;
                            _uXetNghiem.AllowLock = isLock;
                            _uXetNghiem.AllowExportAll = isExportAll;

                            _uKetQuaXetNghiem_Hitachi917.AllowAdd = isAdd;
                            _uKetQuaXetNghiem_Hitachi917.AllowEdit = isEdit;
                            _uKetQuaXetNghiem_Hitachi917.AllowDelete = isDelete;
                            _uKetQuaXetNghiem_Hitachi917.AllowPrint = isPrint;
                            _uKetQuaXetNghiem_Hitachi917.AllowExport = isExport;
                            _uKetQuaXetNghiem_Hitachi917.AllowImport = isImport;
                            _uKetQuaXetNghiem_Hitachi917.AllowLock = isLock;
                            _uKetQuaXetNghiem_Hitachi917.AllowExportAll = isExportAll;

                            _uKetQuaXetNghiem_CellDyn3200.AllowAdd = isAdd;
                            _uKetQuaXetNghiem_CellDyn3200.AllowEdit = isEdit;
                            _uKetQuaXetNghiem_CellDyn3200.AllowDelete = isDelete;
                            _uKetQuaXetNghiem_CellDyn3200.AllowPrint = isPrint;
                            _uKetQuaXetNghiem_CellDyn3200.AllowExport = isExport;
                            _uKetQuaXetNghiem_CellDyn3200.AllowImport = isImport;
                            _uKetQuaXetNghiem_CellDyn3200.AllowLock = isLock;
                            _uKetQuaXetNghiem_CellDyn3200.AllowExportAll = isExportAll;

                            _uXetNghiemTay.AllowAdd = isAdd;
                            _uXetNghiemTay.AllowEdit = isEdit;
                            _uXetNghiemTay.AllowDelete = isDelete;
                            _uXetNghiemTay.AllowPrint = isPrint;
                            _uXetNghiemTay.AllowExport = isExport;
                            _uXetNghiemTay.AllowImport = isImport;
                            _uXetNghiemTay.AllowLock = isLock;
                            _uXetNghiemTay.AllowExportAll = isExportAll;

                            _uDanhSachXetNghiemHitachi917List.AllowAdd = isAdd;
                            _uDanhSachXetNghiemHitachi917List.AllowEdit = isEdit;
                            _uDanhSachXetNghiemHitachi917List.AllowDelete = isDelete;
                            _uDanhSachXetNghiemHitachi917List.AllowPrint = isPrint;
                            _uDanhSachXetNghiemHitachi917List.AllowExport = isExport;
                            _uDanhSachXetNghiemHitachi917List.AllowImport = isImport;
                            _uDanhSachXetNghiemHitachi917List.AllowLock = isLock;
                            _uDanhSachXetNghiemHitachi917List.AllowExportAll = isExportAll;

                            _uDanhSachXetNghiem_CellDyn3200List.AllowAdd = isAdd;
                            _uDanhSachXetNghiem_CellDyn3200List.AllowEdit = isEdit;
                            _uDanhSachXetNghiem_CellDyn3200List.AllowDelete = isDelete;
                            _uDanhSachXetNghiem_CellDyn3200List.AllowPrint = isPrint;
                            _uDanhSachXetNghiem_CellDyn3200List.AllowExport = isExport;
                            _uDanhSachXetNghiem_CellDyn3200List.AllowImport = isImport;
                            _uDanhSachXetNghiem_CellDyn3200List.AllowLock = isLock;
                            _uDanhSachXetNghiem_CellDyn3200List.AllowExportAll = isExportAll;

                            _uKetQuaXetNghiemTay.AllowAdd = isAdd;
                            _uKetQuaXetNghiemTay.AllowEdit = isEdit;
                            _uKetQuaXetNghiemTay.AllowDelete = isDelete;
                            _uKetQuaXetNghiemTay.AllowPrint = isPrint;
                            _uKetQuaXetNghiemTay.AllowExport = isExport;
                            _uKetQuaXetNghiemTay.AllowImport = isImport;
                            _uKetQuaXetNghiemTay.AllowLock = isLock;
                            _uKetQuaXetNghiemTay.AllowExportAll = isExportAll;

                            _uKetQuaXetNghiemTongHop.AllowAdd = isAdd;
                            _uKetQuaXetNghiemTongHop.AllowEdit = isEdit;
                            _uKetQuaXetNghiemTongHop.AllowDelete = isDelete;
                            _uKetQuaXetNghiemTongHop.AllowPrint = isPrint;
                            _uKetQuaXetNghiemTongHop.AllowExport = isExport;
                            _uKetQuaXetNghiemTongHop.AllowImport = isImport;
                            _uKetQuaXetNghiemTongHop.AllowLock = isLock;
                            _uKetQuaXetNghiemTongHop.AllowExportAll = isExportAll;
                        }
                        else if (functionCode == Const.BaoCaoKhachHangMuaThuoc)
                        {
                            baoCaoKhachHangMuaThuocToolStripMenuItem.Enabled = isView && isLogin;
                            _uBaoCaoKhachHangMuaThuoc.AllowAdd = isAdd;
                            _uBaoCaoKhachHangMuaThuoc.AllowEdit = isEdit;
                            _uBaoCaoKhachHangMuaThuoc.AllowDelete = isDelete;
                            _uBaoCaoKhachHangMuaThuoc.AllowPrint = isPrint;
                            _uBaoCaoKhachHangMuaThuoc.AllowExport = isExport;
                            _uBaoCaoKhachHangMuaThuoc.AllowImport = isImport;
                            _uBaoCaoKhachHangMuaThuoc.AllowLock = isLock;
                            _uBaoCaoKhachHangMuaThuoc.AllowExportAll = isExportAll;
                        }
                        else if (functionCode == Const.BaoCaoSoLuongKham)
                        {
                            baoCaoSoLuongKhamToolStripMenuItem.Enabled = isView && isLogin;
                            _uBaoCaoSoLuongKham.AllowAdd = isAdd;
                            _uBaoCaoSoLuongKham.AllowEdit = isEdit;
                            _uBaoCaoSoLuongKham.AllowDelete = isDelete;
                            _uBaoCaoSoLuongKham.AllowPrint = isPrint;
                            _uBaoCaoSoLuongKham.AllowExport = isExport;
                            _uBaoCaoSoLuongKham.AllowImport = isImport;
                            _uBaoCaoSoLuongKham.AllowLock = isLock;
                            _uBaoCaoSoLuongKham.AllowExportAll = isExportAll;
                            _uBaoCaoSoLuongKham.UpdateGUI();
                        }
                        else if (functionCode == Const.TraCuuThongTinKhachHang)
                        {
                            traCuuThongTinKhachHangToolStripMenuItem.Enabled = isView && isLogin;
                            _uTraCuuThongTinKhachHang.AllowAdd = isAdd;
                            _uTraCuuThongTinKhachHang.AllowEdit = isEdit;
                            _uTraCuuThongTinKhachHang.AllowDelete = isDelete;
                            _uTraCuuThongTinKhachHang.AllowPrint = isPrint;
                            _uTraCuuThongTinKhachHang.AllowExport = isExport;
                            _uTraCuuThongTinKhachHang.AllowImport = isImport;
                            _uTraCuuThongTinKhachHang.AllowLock = isLock;
                            _uTraCuuThongTinKhachHang.AllowExportAll = isExportAll;

                            Global.AllowViewTraCuuDanhSachKhachHang = isView;
                        }
                        else if (functionCode == Const.DiaChiCongTy)
                        {
                            danhMucDiaChiCongTyToolStripMenuItem.Enabled = isView && isLogin;
                            _uDiaChiCongTyList.AllowAdd = isAdd;
                            _uDiaChiCongTyList.AllowEdit = isEdit;
                            _uDiaChiCongTyList.AllowDelete = isDelete;
                            _uDiaChiCongTyList.AllowPrint = isPrint;
                            _uDiaChiCongTyList.AllowExport = isExport;
                            _uDiaChiCongTyList.AllowImport = isImport;
                            _uDiaChiCongTyList.AllowLock = isLock;
                            _uDiaChiCongTyList.AllowExportAll = isExportAll;

                            Global.AllowViewDSDiaChiCongTy = isView;
                            Global.AllowAddDSDiaChiCongTy = isAdd;
                            Global.AllowEditDSDiaChiCongTy = isEdit;
                            Global.AllowDeleteDSDiaChiCongTy = isDelete;
                        }
                        else if (functionCode == Const.ChiTietPhieuThuDichVu)
                        {
                            chiTietPhieuThuDichVuToolStripMenuItem.Enabled = isView && isLogin;
                            _uChiTietPhieuThuDichVu.AllowAdd = isAdd;
                            _uChiTietPhieuThuDichVu.AllowEdit = isEdit;
                            _uChiTietPhieuThuDichVu.AllowDelete = isDelete;
                            _uChiTietPhieuThuDichVu.AllowPrint = isPrint;
                            _uChiTietPhieuThuDichVu.AllowExport = isExport;
                            _uChiTietPhieuThuDichVu.AllowImport = isImport;
                            _uChiTietPhieuThuDichVu.AllowLock = isLock;
                            _uChiTietPhieuThuDichVu.AllowExportAll = isExportAll;
                        }
                        else if (functionCode == Const.ThuocTonKho)
                        {
                            thuocTonKhoToolStripMenuItem.Enabled = isView && isLogin;
                            _uBaoCaoThuocTonKhoTheoKhoangThoiGian.AllowAdd = isAdd;
                            _uBaoCaoThuocTonKhoTheoKhoangThoiGian.AllowEdit = isEdit;
                            _uBaoCaoThuocTonKhoTheoKhoangThoiGian.AllowDelete = isDelete;
                            _uBaoCaoThuocTonKhoTheoKhoangThoiGian.AllowPrint = isPrint;
                            _uBaoCaoThuocTonKhoTheoKhoangThoiGian.AllowExport = isExport;
                            _uBaoCaoThuocTonKhoTheoKhoangThoiGian.AllowImport = isImport;
                            _uBaoCaoThuocTonKhoTheoKhoangThoiGian.AllowLock = isLock;
                            _uBaoCaoThuocTonKhoTheoKhoangThoiGian.AllowExportAll = isExportAll;
                        }
                        else if (functionCode == Const.BenhNhanThanThuoc)
                        {
                            benhNhanThanThuocToolStripMenuItem.Enabled = isView && isLogin;
                            _uBenhNhanThanThuocList.AllowAdd = isAdd;
                            _uBenhNhanThanThuocList.AllowEdit = isEdit;
                            _uBenhNhanThanThuocList.AllowDelete = isDelete;
                            _uBenhNhanThanThuocList.AllowPrint = isPrint;
                            _uBenhNhanThanThuocList.AllowExport = isExport;
                            _uBenhNhanThanThuocList.AllowImport = isImport;
                            _uBenhNhanThanThuocList.AllowLock = isLock;
                            _uBenhNhanThanThuocList.AllowExportAll = isExportAll;
                        }
                        else if (functionCode == Const.KetQuaSieuAm)
                        {
                            Global.AllowViewSieuAm = isView;
                            Global.AllowAddSieuAm = isAdd;
                            Global.AllowEditSieuAm = isEdit;
                            Global.AllowDeleteSieuAm = isDelete;
                            Global.AllowExportSieuAm = isExport;
                            Global.AllowPrintSieuAm = isPrint;
                        }
                        else if (functionCode == Const.LoaiSieuAm)
                        {
                            loaiSieuAmToolStripMenuItem.Enabled = isView && isLogin;
                            _uLoaiSieuAmList.AllowAdd = isAdd;
                            _uLoaiSieuAmList.AllowEdit = isEdit;
                            _uLoaiSieuAmList.AllowDelete = isDelete;
                            _uLoaiSieuAmList.AllowPrint = isPrint;
                            _uLoaiSieuAmList.AllowExport = isExport;
                            _uLoaiSieuAmList.AllowImport = isImport;
                            _uLoaiSieuAmList.AllowLock = isLock;
                            _uLoaiSieuAmList.AllowExportAll = isExportAll;
                        }
                        else if (functionCode == Const.TiemNgua)
                        {
                            tiemNguaToolStripMenuItem.Enabled = isView && isLogin;
                            _uTiemNguaList.AllowAdd = isAdd;
                            _uTiemNguaList.AllowEdit = isEdit;
                            _uTiemNguaList.AllowDelete = isDelete;
                            _uTiemNguaList.AllowPrint = isPrint;
                            _uTiemNguaList.AllowExport = isExport;
                            _uTiemNguaList.AllowImport = isImport;
                            _uTiemNguaList.AllowLock = isLock;
                            _uTiemNguaList.AllowExportAll = isExportAll;
                        }
                        else if (functionCode == Const.CongTacNgoaiGio)
                        {
                            congTacNgoaiGioToolStripMenuItem.Enabled = isView && isLogin;
                            _uCongTacNgoaiGioList.AllowAdd = isAdd;
                            _uCongTacNgoaiGioList.AllowEdit = isEdit;
                            _uCongTacNgoaiGioList.AllowDelete = isDelete;
                            _uCongTacNgoaiGioList.AllowPrint = isPrint;
                            _uCongTacNgoaiGioList.AllowExport = isExport;
                            _uCongTacNgoaiGioList.AllowImport = isImport;
                            _uCongTacNgoaiGioList.AllowLock = isLock;
                            _uCongTacNgoaiGioList.AllowExportAll = isExportAll;
                        }
                        else if (functionCode == Const.LichKham)
                        {
                            lichKhamToolStripMenuItem.Enabled = isView && isLogin;
                            _uLichKham.AllowAdd = isAdd;
                            _uLichKham.AllowEdit = isEdit;
                            _uLichKham.AllowDelete = isDelete;
                            _uLichKham.AllowPrint = isPrint;
                            _uLichKham.AllowExport = isExport;
                            _uLichKham.AllowImport = isImport;
                            _uLichKham.AllowLock = isLock;
                            _uLichKham.AllowExportAll = isExportAll;
                        }
                        else if (functionCode == Const.KhoCapCuu)
                        {
                            khoCapCuuToolStripMenuItem.Enabled = isLogin;
                            danhMucCapCuuToolStripMenuItem.Enabled = isView && isLogin;
                            _uKhoCapCuu.AllowAdd = isAdd;
                            _uKhoCapCuu.AllowEdit = isEdit;
                            _uKhoCapCuu.AllowDelete = isDelete;
                            _uKhoCapCuu.AllowPrint = isPrint;
                            _uKhoCapCuu.AllowExport = isExport;
                            _uKhoCapCuu.AllowImport = isImport;
                            _uKhoCapCuu.AllowLock = isLock;
                            _uKhoCapCuu.AllowExportAll = isExportAll;
                        }
                        else if (functionCode == Const.NhapKhoCapCuu)
                        {
                            khoCapCuuToolStripMenuItem.Enabled = isLogin;
                            nhapKhoCapCuuToolStripMenuItem.Enabled = isView && isLogin;
                            _uNhapKhoCapCuuList.AllowAdd = isAdd;
                            _uNhapKhoCapCuuList.AllowEdit = isEdit;
                            _uNhapKhoCapCuuList.AllowDelete = isDelete;
                            _uNhapKhoCapCuuList.AllowPrint = isPrint;
                            _uNhapKhoCapCuuList.AllowExport = isExport;
                            _uNhapKhoCapCuuList.AllowImport = isImport;
                            _uNhapKhoCapCuuList.AllowLock = isLock;
                        }
                        else if (functionCode == Const.XuatKhoCapCuu)
                        {
                            khoCapCuuToolStripMenuItem.Enabled = isLogin;
                            xuatKhoCapCuuToolStripMenuItem.Enabled = isView && isLogin;
                            _uXuatKhoCapCuuList.AllowAdd = isAdd;
                            _uXuatKhoCapCuuList.AllowEdit = isEdit;
                            _uXuatKhoCapCuuList.AllowDelete = isDelete;
                            _uXuatKhoCapCuuList.AllowPrint = isPrint;
                            _uXuatKhoCapCuuList.AllowExport = isExport;
                            _uXuatKhoCapCuuList.AllowImport = isImport;
                            _uXuatKhoCapCuuList.AllowLock = isLock;
                        }
                        else if (functionCode == Const.BaoCaoCapCuuHetHan)
                        {
                            khoCapCuuToolStripMenuItem.Enabled = isLogin;
                            baoCaoCapCuuHetHanToolStripMenuItem.Enabled = isView && isLogin;
                            _uBaoCaoCapCuuHetHan.AllowAdd = isAdd;
                            _uBaoCaoCapCuuHetHan.AllowEdit = isEdit;
                            _uBaoCaoCapCuuHetHan.AllowDelete = isDelete;
                            _uBaoCaoCapCuuHetHan.AllowPrint = isPrint;
                            _uBaoCaoCapCuuHetHan.AllowExport = isExport;
                            _uBaoCaoCapCuuHetHan.AllowImport = isImport;
                            _uBaoCaoCapCuuHetHan.AllowLock = isLock;
                        }
                        else if (functionCode == Const.BaoCaoTonKhoCapCuu)
                        {
                            khoCapCuuToolStripMenuItem.Enabled = isLogin;
                            baoCaoTonKhoCapCuuToolStripMenuItem.Enabled = isView && isLogin;
                            _uBaoCaoCapCuuHetHan.AllowAdd = isAdd;
                            _uBaoCaoCapCuuHetHan.AllowEdit = isEdit;
                            _uBaoCaoCapCuuHetHan.AllowDelete = isDelete;
                            _uBaoCaoCapCuuHetHan.AllowPrint = isPrint;
                            _uBaoCaoCapCuuHetHan.AllowExport = isExport;
                            _uBaoCaoCapCuuHetHan.AllowImport = isImport;
                            _uBaoCaoCapCuuHetHan.AllowLock = isLock;
                            _uBaoCaoCapCuuHetHan.AllowExportAll = isExportAll;
                        }
                        else if (functionCode == Const.ThongBao)
                        {
                            toolsToolStripMenuItem.Enabled = isLogin;
                            thongBaoToolStripMenuItem.Enabled = isView && isLogin;
                            _uThongBaoList.AllowAdd = isAdd;
                            _uThongBaoList.AllowEdit = isEdit;
                            _uThongBaoList.AllowDelete = isDelete;
                            _uThongBaoList.AllowPrint = isPrint;
                            _uThongBaoList.AllowExport = isExport;
                            _uThongBaoList.AllowImport = isImport;
                            _uThongBaoList.AllowLock = isLock;
                            _uThongBaoList.AllowExportAll = isExportAll;
                            _uThongBaoList.AllowConfirm = isConfirm;
                        }
                        else if (functionCode == Const.BenhNhanNgoaiGoiKham)
                        {
                            patientToolStripMenuItem.Enabled = isLogin;
                            benhNhanNgoaiGoiKhamToolStripMenuItem.Enabled = isView && isLogin;
                            _uBenhNhanNgoaiGoiKhamList.AllowAdd = isAdd;
                            _uBenhNhanNgoaiGoiKhamList.AllowEdit = isEdit;
                            _uBenhNhanNgoaiGoiKhamList.AllowDelete = isDelete;
                            _uBenhNhanNgoaiGoiKhamList.AllowPrint = isPrint;
                            _uBenhNhanNgoaiGoiKhamList.AllowExport = isExport;
                            _uBenhNhanNgoaiGoiKhamList.AllowImport = isImport;
                            _uBenhNhanNgoaiGoiKhamList.AllowLock = isLock;
                            _uBenhNhanNgoaiGoiKhamList.AllowExportAll = isExportAll;
                            _uBenhNhanNgoaiGoiKhamList.AllowConfirm = isConfirm;
                        }
                        else if (functionCode == Const.GiaCapCuu)
                        {
                            khoCapCuuToolStripMenuItem.Enabled = isLogin;
                            giaCapCuuToolStripMenuItem.Enabled = isView && isLogin;
                            _uGiaCapCuuList.AllowAdd = isAdd;
                            _uGiaCapCuuList.AllowEdit = isEdit;
                            _uGiaCapCuuList.AllowDelete = isDelete;
                            _uGiaCapCuuList.AllowPrint = isPrint;
                            _uGiaCapCuuList.AllowExport = isExport;
                            _uGiaCapCuuList.AllowImport = isImport;
                            _uGiaCapCuuList.AllowLock = isLock;
                            _uGiaCapCuuList.AllowExportAll = isExportAll;
                            _uGiaCapCuuList.AllowConfirm = isConfirm;
                        }
                        else if (functionCode == Const.PhieuThuCapCuu)
                        {
                            receiptToolStripMenuItem.Enabled = isLogin;
                            phieuThuCapCuuToolStripMenuItem.Enabled = isView && isLogin;
                            _uPhieuThuCapCuuList.AllowAdd = isAdd;
                            _uPhieuThuCapCuuList.AllowEdit = isEdit;
                            _uPhieuThuCapCuuList.AllowDelete = isDelete;
                            _uPhieuThuCapCuuList.AllowPrint = isPrint;
                            _uPhieuThuCapCuuList.AllowExport = isExport;
                            _uPhieuThuCapCuuList.AllowImport = isImport;
                            _uPhieuThuCapCuuList.AllowLock = isLock;
                            _uPhieuThuCapCuuList.AllowExportAll = isExportAll;
                            _uPhieuThuCapCuuList.AllowConfirm = isConfirm;
                        }
                        else if (functionCode == Const.NhomNguoiSuDung)
                        {
                            nhomNguoiSuDungToolStripMenuItem.Enabled = isView && isLogin;
                            _uUserGroupList.AllowAdd = isAdd;
                            _uUserGroupList.AllowEdit = isEdit;
                            _uUserGroupList.AllowDelete = isDelete;
                            _uUserGroupList.AllowPrint = isPrint;
                            _uUserGroupList.AllowExport = isExport;
                            _uUserGroupList.AllowImport = isImport;
                            _uUserGroupList.AllowLock = isLock;
                            _uUserGroupList.AllowExportAll = isExportAll;
                            _uUserGroupList.AllowConfirm = isConfirm;
                        }
                        else if (functionCode == Const.NguoiSuDung)
                        {
                            nguoiSuDungToolStripMenuItem.Enabled = isView && isLogin;
                            _uNguoiSuDungList.AllowAdd = isAdd;
                            _uNguoiSuDungList.AllowEdit = isEdit;
                            _uNguoiSuDungList.AllowDelete = isDelete;
                            _uNguoiSuDungList.AllowPrint = isPrint;
                            _uNguoiSuDungList.AllowExport = isExport;
                            _uNguoiSuDungList.AllowImport = isImport;
                            _uNguoiSuDungList.AllowLock = isLock;
                            _uNguoiSuDungList.AllowExportAll = isExportAll;
                            _uNguoiSuDungList.AllowConfirm = isConfirm;
                        }
                        else if (functionCode == Const.KetQuaCanLamSang)
                        {
                            Global.AllowViewCanLamSang = isView;
                            Global.AllowAddCanLamSang = isAdd;
                            Global.AllowEditCanLamSang = isEdit;
                            Global.AllowDeleteCanLamSang = isDelete;
                        }
                        else if (functionCode == Const.TaoHoSo)
                        {
                            Global.AllowTaoHoSo = isCreateReport;
                        }
                        else if (functionCode == Const.UploadHoSo)
                        {
                            Global.AllowUploadHoSo = isUpload;
                        }
                        else if (functionCode == Const.KeToaCapCuu)
                        {
                            khoCapCuuToolStripMenuItem.Enabled = isLogin;
                            keToaCapCuuToolStripMenuItem.Enabled = isView && isLogin;
                            _uToaCapCuuList.AllowAdd = isAdd;
                            _uToaCapCuuList.AllowEdit = isEdit;
                            _uToaCapCuuList.AllowDelete = isDelete;
                            _uToaCapCuuList.AllowPrint = isPrint;
                            _uToaCapCuuList.AllowExport = isExport;
                            _uToaCapCuuList.AllowImport = isImport;
                            _uToaCapCuuList.AllowLock = isLock;
                            _uToaCapCuuList.AllowExportAll = isExportAll;
                            _uToaCapCuuList.AllowConfirm = isConfirm;
                        }
                        else if (functionCode == Const.TaoMatKhauHoSo)
                        {
                            Global.AllowAddMatKhauHoSo = isAdd;
                        }
                        else if (functionCode == Const.KhamHopDong)
                        {
                            companyToolStripMenuItem.Enabled = isLogin;
                            khamHopDongToolStripMenuItem.Enabled = isView && isLogin;
                            _uKhamHopDong.AllowAdd = isAdd;
                            _uKhamHopDong.AllowEdit = isEdit;
                            _uKhamHopDong.AllowDelete = isDelete;
                            _uKhamHopDong.AllowPrint = isPrint;
                            _uKhamHopDong.AllowExport = isExport;
                            _uKhamHopDong.AllowImport = isImport;
                            _uKhamHopDong.AllowLock = isLock;
                            _uKhamHopDong.AllowExportAll = isExportAll;
                            _uKhamHopDong.AllowConfirm = isConfirm;
                        }
                        else if (functionCode == Const.GuiSMS)
                        {
                            sMSToolStripMenuItem.Enabled = isLogin;
                            guiSMSToolStripMenuItem.Enabled = isView && isLogin;
                            _uSendSMS.AllowAdd = isAdd;
                            _uSendSMS.AllowEdit = isEdit;
                            _uSendSMS.AllowDelete = isDelete;
                            _uSendSMS.AllowPrint = isPrint;
                            _uSendSMS.AllowExport = isExport;
                            _uSendSMS.AllowImport = isImport;
                            _uSendSMS.AllowLock = isLock;
                            _uSendSMS.AllowExportAll = isExportAll;
                            _uSendSMS.AllowConfirm = isConfirm;
                            _uSendSMS.AllowSendSMS = isSendSMS;
                        }
                        else if (functionCode == Const.TinNhanMau)
                        {
                            sMSToolStripMenuItem.Enabled = isLogin;
                            tinNhanMauToolStripMenuItem.Enabled = isView && isLogin;
                            _uTinNhanMauList.AllowAdd = isAdd;
                            _uTinNhanMauList.AllowEdit = isEdit;
                            _uTinNhanMauList.AllowDelete = isDelete;
                            _uTinNhanMauList.AllowPrint = isPrint;
                            _uTinNhanMauList.AllowExport = isExport;
                            _uTinNhanMauList.AllowImport = isImport;
                            _uTinNhanMauList.AllowLock = isLock;
                            _uTinNhanMauList.AllowExportAll = isExportAll;
                            _uTinNhanMauList.AllowConfirm = isConfirm;
                        }
                        else if (functionCode == Const.BaoCaoCongNoHopDong)
                        {
                            companyToolStripMenuItem.Enabled = isLogin;
                            baoCaoCongNoTheoHopDongToolStripMenuItem.Enabled = isView && isLogin;
                            _uBaoCaoCongNoHopDong.AllowAdd = isAdd;
                            _uBaoCaoCongNoHopDong.AllowEdit = isEdit;
                            _uBaoCaoCongNoHopDong.AllowDelete = isDelete;
                            _uBaoCaoCongNoHopDong.AllowPrint = isPrint;
                            _uBaoCaoCongNoHopDong.AllowExport = isExport;
                            _uBaoCaoCongNoHopDong.AllowImport = isImport;
                            _uBaoCaoCongNoHopDong.AllowLock = isLock;
                            _uBaoCaoCongNoHopDong.AllowExportAll = isExportAll;
                            _uBaoCaoCongNoHopDong.AllowConfirm = isConfirm;
                        }
                        else if (functionCode == Const.SMSLog)
                        {
                            sMSToolStripMenuItem.Enabled = isLogin;
                            sMSLogToolStripMenuItem.Enabled = isView && isLogin;
                            _uSMSLog.AllowAdd = isAdd;
                            _uSMSLog.AllowEdit = isEdit;
                            _uSMSLog.AllowDelete = isDelete;
                            _uSMSLog.AllowPrint = isPrint;
                            _uSMSLog.AllowExport = isExport;
                            _uSMSLog.AllowImport = isImport;
                            _uSMSLog.AllowLock = isLock;
                            _uSMSLog.AllowExportAll = isExportAll;
                            _uSMSLog.AllowConfirm = isConfirm;
                        }
                    }
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("LogonBus.GetPermission"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("LogonBus.GetPermission"));
                }
            }
            else
            {
                Global.AllowShowServiePrice = true;
                Global.AllowExportPhieuThuDichVu = true;
                Global.AllowPrintPhieuThuDichVu = true;
                Global.AllowExportHoaDonDichVu = true;
                Global.AllowExportHoaDonThuoc = true;
                Global.AllowExportHoaDonHopDong = true;
                Global.AllowExportHoaDonXuatTruoc = true;
                Global.AllowPrintHoaDonDichVu = true;
                Global.AllowPrintHoaDonThuoc = true;
                Global.AllowPrintHoaDonHopDong = true;
                Global.AllowPrintHoaDonXuatTruoc = true;
                Global.AllowViewChiDinh = true;
                Global.AllowAddChiDinh = true;
                Global.AllowEditChiDinh = true;
                Global.AllowDeleteChiDinh = true;
                Global.AllowConfirmChiDinh = true;
                Global.AllowAddPhongCho = true;
                Global.AllowViewDichVuDaSuDung = true;
                Global.AllowAddDichVuDaSuDung = true;
                Global.AllowEditDichVuDaSuDung = true;
                Global.AllowDeleteDichVuDaSuDung = true;
                Global.AllowViewCanDo = true;
                Global.AllowAddCanDo = true;
                Global.AllowEditCanDo = true;
                Global.AllowDeleteCanDo = true;
                Global.AllowViewKhamLamSang = true;
                Global.AllowAddKhamLamSang = true;
                Global.AllowEditKhamLamSang = true;
                Global.AllowDeleteKhamLamSang = true;
                Global.AllowViewLoiKhuyen = true;
                Global.AllowAddLoiKhuyen = true;
                Global.AllowEditLoiKhuyen = true;
                Global.AllowDeleteLoiKhuyen = true;
                Global.AllowViewKetLuan = true;
                Global.AllowAddKetLuan = true;
                Global.AllowEditKetLuan = true;
                Global.AllowDeleteKetLuan = true;
                Global.AllowViewKhamNoiSoi = true;
                Global.AllowAddKhamNoiSoi = true;
                Global.AllowEditKhamNoiSoi = true;
                Global.AllowDeleteKhamNoiSoi = true;
                Global.AllowExportKhamNoiSoi = true;
                Global.AllowPrintKhamNoiSoi = true;
                Global.AllowViewKeToa = true;
                Global.AllowViewKhamCTC = true;
                Global.AllowAddKhamCTC = true;
                Global.AllowEditKhamCTC = true;
                Global.AllowDeleteKhamCTC = true;
                Global.AllowExportKhamCTC = true;
                Global.AllowPrintKhamCTC = true;
                Global.AllowViewDSDiaChiCongTy = true;
                Global.AllowAddDSDiaChiCongTy = true;
                Global.AllowEditDSDiaChiCongTy = true;
                Global.AllowDeleteDSDiaChiCongTy = true;
                Global.AllowViewTraCuuDanhSachKhachHang = true;
                Global.AllowViewSieuAm = true;
                Global.AllowAddSieuAm = true;
                Global.AllowEditSieuAm = true;
                Global.AllowDeleteSieuAm = true;
                Global.AllowExportSieuAm = true;
                Global.AllowPrintSieuAm = true;
                Global.AllowAddYKienKhachHang = true;
                Global.AllowAddKeToa = true;
                Global.AllowEditKeToa = true;
                Global.AllowDeleteKeToa = true;
                Global.AllowPrintKeToa = true;
                Global.AllowViewCanLamSang = true;
                Global.AllowAddCanLamSang = true;
                Global.AllowEditCanLamSang = true;
                Global.AllowDeleteCanLamSang = true;
                Global.AllowTaoHoSo = true;
                Global.AllowUploadHoSo = true;
                Global.AllowAddMatKhauHoSo = true;

                foreach (Control ctrl in this._mainPanel.Controls)
                {   
                    (ctrl as uBase).AllowAdd = true;
                    (ctrl as uBase).AllowEdit = true;
                    (ctrl as uBase).AllowDelete = true;
                    (ctrl as uBase).AllowPrint = true;
                    (ctrl as uBase).AllowExport = true;
                    (ctrl as uBase).AllowImport = true;
                    (ctrl as uBase).AllowLock = true;
                    (ctrl as uBase).AllowExportAll = true;
                    (ctrl as uBase).AllowConfirm = true;
                }

                servicesToolStripMenuItem.Enabled = isLogin;
                serviceListToolStripMenuItem.Enabled = isLogin;
                tbServiceList.Enabled = isLogin;
                serviceGroupToolStripMenuItem.Enabled = isLogin;
                tbNhomDichVu.Enabled = isLogin;
                giaVonDichVuToolStripMenuItem.Enabled = isLogin;
                tbGiaVonDichVu.Enabled = isLogin;

                danhmụcToolStripMenuItem.Enabled = isLogin;
                nhanVienToolStripMenuItem.Enabled = isLogin;
                tbDoctorList.Enabled = isLogin;

                patientToolStripMenuItem.Enabled = isLogin;
                patientListToolStripMenuItem.Enabled = isLogin;
                openPatientToolStripMenuItem.Enabled = isLogin;
                tbPatientList.Enabled = isLogin;
                tbOpenPatient.Enabled = isLogin;

                chuyenKhoaToolStripMenuItem.Enabled = isLogin;
                tbSpecialityList.Enabled = isLogin;

                trieuChungToolStripMenuItem.Enabled = isLogin;
                tbSympton.Enabled = isLogin;

                companyToolStripMenuItem.Enabled = isLogin;
                contractListToolStripMenuItem.Enabled = isLogin;
                companyListToolStripMenuItem.Enabled = isLogin;
                tbCompanyList.Enabled = isLogin;
                tbContractList.Enabled = isLogin;

                toolsToolStripMenuItem.Enabled = isLogin;
                printLabelToolStripMenuItem.Enabled = isLogin;
            
                changePasswordToolStripMenuItem.Enabled = isLogin;
                permissionToolStripMenuItem.Enabled = isLogin;

                receiptListToolStripMenuItem.Enabled = isLogin;
                receiptToolStripMenuItem.Enabled = isLogin;
                tbReceiptList.Enabled = isLogin;

                invoiceToolStripMenuItem.Enabled = isLogin;
                tbInvoiceList.Enabled = isLogin;
                invoiceListToolStripMenuItem.Enabled = isLogin;

                DuplicatePatientToolStripMenuItem.Enabled = isLogin;
                tbDuplicatePatient.Enabled = isLogin;

                reportToolStripMenuItem.Enabled = isLogin;
                doanhThuNhanVienToolStripMenuItem.Enabled = isLogin;
                dichVuHopDongToolStripMenuItem.Enabled = isLogin;

                thuocToolStripMenuItem.Enabled = isLogin;
                danhMucThuocToolStripMenuItem.Enabled = isLogin;
                nhomThuocToolStripMenuItem.Enabled = isLogin;
                tbDanhMucThuoc.Enabled = isLogin;
                tbNhomThuoc.Enabled = isLogin;
                loThuocToolStripMenuItem.Enabled = isLogin;
                tbLoThuoc.Enabled = isLogin;
                giaThuocToolStripMenuItem.Enabled = isLogin;
                tbGiaThuoc.Enabled = isLogin;
                keToaToolStripMenuItem.Enabled = isLogin;
                tbKeToa.Enabled = isLogin;

                thuocHetHanToolStripMenuItem.Enabled = isLogin;
                thuocTonKhoToolStripMenuItem.Enabled = isLogin;
                phieuThuThuocToolStripMenuItem.Enabled = isLogin;
                tbPhieuThuThuoc.Enabled = isLogin;

                dichVuTuTucToolStripMenuItem.Enabled = isLogin;
                trackingToolStripMenuItem.Enabled = isLogin;

                serviceGroupToolStripMenuItem.Enabled = isLogin;
                inKetQuaKhamSucKhoeTongQuatToolStripMenuItem.Enabled = isLogin;
                doanhThuTheoNgayToolStripMenuItem.Enabled = isLogin;

                _uPhongChoList.IsEnableBtnRaPhongCho = isLogin;
                _uBaoCaoDichVuChuaXuatPhieuThu.Enabled = isLogin;
                hoaDonThuocToolStripMenuItem.Enabled = isLogin;
                hoaDonXuatTruocToolStripMenuItem.Enabled = isLogin;
                thongKeHoaDonToolStripMenuItem.Enabled = isLogin;
                phucHoiBenhNhanToolStripMenuItem.Enabled = isLogin;
                phieuThuHopDongToolStripMenuItem.Enabled = isLogin;
                hoaDonHopDongToolStripMenuItem.Enabled = isLogin;
                chamSocKhachHangToolStripMenuItem.Enabled = isLogin;
                yKienKhachHangToolStripMenuItem.Enabled = isLogin;
                nhatKyLienHeCongTyToolStripMenuItem.Enabled = isLogin;

                xetNghiemToolStripMenuItem.Enabled = isLogin;
                xetNghiemHiTachi917ToolStripMenuItem.Enabled = isLogin;
                xetNghiemCellDyn3200ToolStripMenuItem.Enabled = isLogin;
                xetNghiemTayToolStripMenuItem.Enabled = isLogin;
                ketQuaXetNghiemTayToolStripMenuItem.Enabled = isLogin;
                ketQuaXetNghiemTongQuatToolStripMenuItem.Enabled = isLogin;
                baoCaoKhachHangMuaThuocToolStripMenuItem.Enabled = isLogin;
                danhSachXetNghiemHitachi917ToolStripMenuItem.Enabled = isLogin;
                danhSachXetNghiemCellDyn3200ToolStripMenuItem.Enabled = isLogin;
                baoCaoSoLuongKhamToolStripMenuItem.Enabled = isLogin;
                traCuuThongTinKhachHangToolStripMenuItem.Enabled = isLogin;
                danhMucDiaChiCongTyToolStripMenuItem.Enabled = isLogin;
                chiTietPhieuThuDichVuToolStripMenuItem.Enabled = isLogin;
                thuocTonKhoTheoKhoangThoiGianToolStripMenuItem.Enabled = isLogin;
                benhNhanThanThuocToolStripMenuItem.Enabled = isLogin;
                loaiSieuAmToolStripMenuItem.Enabled = isLogin;
                tiemNguaToolStripMenuItem.Enabled = isLogin;
                congTacNgoaiGioToolStripMenuItem.Enabled = isLogin;
                lichKhamToolStripMenuItem.Enabled = isLogin;
                khoCapCuuToolStripMenuItem.Enabled = isLogin;
                danhMucCapCuuToolStripMenuItem.Enabled = isLogin;
                nhapKhoCapCuuToolStripMenuItem.Enabled = isLogin;
                xuatKhoCapCuuToolStripMenuItem.Enabled = isLogin;
                baoCaoCapCuuHetHanToolStripMenuItem.Enabled = isLogin;
                baoCaoTonKhoCapCuuToolStripMenuItem.Enabled = isLogin;
                thongBaoToolStripMenuItem.Enabled = isLogin;
                benhNhanNgoaiGoiKhamToolStripMenuItem.Enabled = isLogin;
                giaCapCuuToolStripMenuItem.Enabled = isLogin;
                phieuThuCapCuuToolStripMenuItem.Enabled = isLogin;
                nhomNguoiSuDungToolStripMenuItem.Enabled = isLogin;
                nguoiSuDungToolStripMenuItem.Enabled = isLogin;
                dichVuChuaXuatPhieuThuToolStripMenuItem.Enabled = isLogin;
                bookingToolStripMenuItem.Enabled = isLogin;
                keToaCapCuuToolStripMenuItem.Enabled = isLogin;
                cauHinhTVHomeToolStripMenuItem.Enabled = isLogin;
                cauHinhPageSetupToolStripMenuItem.Enabled = isLogin;
                khamHopDongToolStripMenuItem.Enabled = isLogin;
                sMSToolStripMenuItem.Enabled = isLogin;
                tinNhanMauToolStripMenuItem.Enabled = isLogin;
                guiSMSToolStripMenuItem.Enabled = isLogin;
                baoCaoCongNoTheoHopDongToolStripMenuItem.Enabled = isLogin;
                sMSLogToolStripMenuItem.Enabled = isLogin;
            }
        }

        private void ExcuteCmd(string cmd)
        {
            Cursor.Current = Cursors.WaitCursor;
            switch (cmd)
            {
                case "Database Configuration":
                    OnDatabaseConfig();
                    break;

                case "Login":
                    OnLogin();
                    break;

                case "Logout":
                    OnLogout();
                    break;

                case "Exit":
                    OnExit();
                    break;

                case "Services List":
                    ClearAllData();
                    OnServicesList();
                    break;

                case "Patient List":
                    ClearAllData();
                    OnPatientList();
                    break;

                case "Open Patient":
                    OnOpenPatient();
                    break;

                case "DuplicatePatient":
                    ClearAllData();
                    OnDuplicatePatient();
                    break;

                case "Doctor List":
                    ClearAllData();
                    OnDoctorList();
                    break;

                case "Speciality List":
                    ClearAllData();
                    OnSpecialityList();
                    break;

                case "Help":
                    OnHelp();
                    break;

                case "About":
                    OnAbout();
                    break;

                case "Symptom List":
                    ClearAllData();
                    OnSymptomList();
                    break;

                case "Company List":
                    ClearAllData();
                    OnCompanyList();
                    break;

                case "Contract List":
                    ClearAllData();
                    OnContractList();
                    break;

                case "DICOM":
                    OnDicom();
                    break;

                case "ExcelTemplate":
                    OnExcelTemplate();
                    break;

                case "TemplateForSale":
                    OnBieuMauPhongSale();
                    break;
                case "Change Password":
                    OnChangePassword();
                    break;

                case "Permission":
                    ClearAllData();
                    OnPermission();
                    break;

                case "Print Label":
                    ClearAllData();
                    OnPrintLabel();
                    break;

                case "Receipt List":
                    ClearAllData();
                    OnReceiptList();
                    break;

                case "Invoice List":
                    ClearAllData();
                    OnInvoiceList();
                    break;

                case "DoanhThuNhanVien":
                    ClearAllData();
                    OnDoanhThuNhanVien();
                    break;
                    
                case "DichVuHopDong":
                    ClearAllData();
                    OnDichVuHopDong();
                    break;

                case "DanhMucThuoc":
                    ClearAllData();
                    OnDanhMucThuoc();
                    break;

                case "NhomThuoc":
                    ClearAllData();
                    OnNhomThuoc();
                    break;

                case "LoThuoc":
                    ClearAllData();
                    OnLoThuoc();
                    break;

                case "GiaThuoc":
                    ClearAllData();
                    OnGiaThuoc();
                    break;

                case "KeToa":
                    ClearAllData();
                    OnKeToa();
                    break;

                case "ThuocHetHan":
                    ClearAllData();
                    OnThuocHetHan();
                    break;

                case "ThuocTonKho":
                    ClearAllData();
                    OnBaoCaoThuocTonKhoTheoKhoangThoiGian();
                    break;

                case "PhieuThuThuoc":
                    ClearAllData();
                    OnPhieuThuThuoc();
                    break;

                case "DichVuTuTuc":
                    ClearAllData();
                    OnDichVuTuTuc();
                    break;

                case "Tracking":
                    ClearAllData();
                    OnTracking();
                    break;

                case "ServiceGroup":
                    ClearAllData();
                    OnServiceGroup();
                    break;

                case "InKetQuaKhamSucKhoeTongQuat":
                    ClearAllData();
                    OnInKetQuaKhamSucKhoeTongQuat();
                    break;

                case "GiaVonDichVu":
                    ClearAllData();
                    OnGiaVonDichVu();
                    break;

                case "DoanhThuTheoNgay":
                    ClearAllData();
                    OnDoanhThuTheoNgay();
                    break;

                case "DichVuChuaXuatPhieuThu":
                    ClearAllData();
                    OnBaoCaoDichVuChuaXuatPhieuThu();
                    break;

                case "HoaDonThuoc":
                    ClearAllData();
                    OnHoaDonThuoc();
                    break;

                case "HoaDonXuatTruoc":
                    ClearAllData();
                    OnHoaDonXuatTruoc();
                    break;

                case "ThongKeHoaDon":
                    ClearAllData();
                    OnThongKeHoaDon();
                    break;

                case "PhucHoiBenhNhan":
                    ClearAllData();
                    OnPhucHoiBenhNhan();
                    break;

                case "PhieuThuHopDong":
                    ClearAllData();
                    OnPhieuThuHopDong();
                    break;
                    
                case "HoaDonHopDong":
                    ClearAllData();
                    OnHoaDonHopDong();
                    break;

                case "YKienKhachHang":
                    ClearAllData();
                    OnYKienKhachHang();
                    break;

                case "NhatKyLienHeCongTy":
                    ClearAllData();
                    OnNhatKyLienHeCongTy();
                    break;

                case "Booking":
                    ClearAllData();
                    OnBooking();
                    break;

                case "XetNghiem_Hitachi917":
                    ClearAllData();
                    OnXetNghiem_Hitachi917();        
                    break;

                case "XetNghiem_CellDyn3200":
                    ClearAllData();
                    OnXetNghiem_CellDyn3200();
                    break;

                case "CauHinhKetNoi":
                    OnCauHinhKetNoi();
                    break;

                case "BaoCaoKhachHangMuaThuoc":
                    ClearAllData();
                    OnBaoCaoKhachHangMuaThuoc();
                    break;

                case "XetNghiemTay":
                    ClearAllData();
                    OnXetNghiemTay();
                    break;

                case "KetQuaXetNghiemTay":
                    ClearAllData();
                    OnKetQuaXetNghiemTay();
                    break;

                case "KetQuaXetNghiemTongHop":
                    ClearAllData();
                    OnKetQuaXetNghiemTongHop();
                    break;

                case "DanhSachXetNghiemHitachi917":
                    ClearAllData();
                    OnDanhSachXetNghiemHitachi917();
                    break;

                case "DanhSachXetNghiemCellDyn3200":
                    ClearAllData();
                    OnDanhSachXetNghiemCellDyn3200();
                    break;

                case "BaoCaoSoLuongKham":
                    ClearAllData();
                    OnBaoCaoSoLuongKham();
                    break;

                case "ThayDoiSoHoaDon":
                    OnThayDoiSoHoaDon();
                    break;

                case "CauHinhFTP":
                    OnCauHinhFTP();
                    break;

                case "TraCuuThongTinKhachHang":
                    ClearAllData();
                    OnTraCuuThongTinKhachHang();
                    break;

                case "DiaChiCongTy":
                    ClearAllData();
                    OnDiaChiCongTy();
                    break;

                case "ChiTietPhieuThuDichVu":
                    ClearAllData();
                    OnChiTietPhieuThuDichVu();
                    break;

                case "CauHinhTrangIn":
                    OnCauHinhTrangIn();
                    break;

                case "BenhNhanThanThuoc":
                    ClearAllData();
                    OnBenhNhanThanThuoc();
                    break;

                case "XetNghiem":
                    ClearAllData();
                    OnXetNghiem();
                    break;

                case "LoaiSieuAm":
                    ClearAllData();
                    OnLoaiSieuAm();
                    break;

                case "TiemNgua":
                    ClearAllData();
                    OnTiemNgua();
                    break;

                case "CauHinhSoNgayBaoTiemNgua":
                    OnCauHinhSoNgayBaoTiemNgua();
                    break;

                case "CongTacNgoaiGio":
                    ClearAllData();
                    OnCongTacNgoaiGio();
                    break;

                case "LichKham":
                    ClearAllData();
                    OnLichKham();
                    break;

                case  "KhoCapCuu":
                    ClearAllData();
                    OnKhoCapCuu();
                    break;

                case "NhapKhoCapCuu":
                    ClearAllData();
                    OnNhapKhoCapCuu();
                    break;

                case "XuatKhoCapCuu":
                    ClearAllData();
                    OnXuatKhoCapCuu();
                    break;

                case "BaoCaoCapCuuHetHan":
                    ClearAllData();
                    OnBaoCaoCapCuuHetHan();
                    break;

                case "BaoCaoTonKhoCapCuu":
                    ClearAllData();
                    OnBaoCaoTonKhoCapCuu();
                    break;

                case "CauHinhKhoCapCuu":
                    OnCauHinhKhoCapCuu();
                    break;

                case "ThongBao":
                    ClearAllData();
                    OnThongBao();
                    break;

                case "BenhNhanNgoaiGoiKham":
                    ClearAllData();
                    OnBenhNhanNgoaiGoiKham();
                    break;

                case "GiaCapCuu":
                    ClearAllData();
                    OnGiaCapCuu();
                    break;

                case "PhieuThuCapCuu":
                    ClearAllData();
                    OnPhieuThuCapCuu();
                    break;

                case "NguoiSuDung":
                    ClearAllData();
                    OnNguoiSuDung();
                    break;

                case "NhomNguoiSuDung":
                    ClearAllData();
                    OnNhomNguoiSuDung();
                    break;

                case "KeToaCapCuu":
                    ClearAllData();
                    OnKeToaCapCuu();
                    break;

                case "TVHomeConfig":
                    OnTVHomeConfig();
                    break;

                case "KhamHopDong":
                    OnKhamHopDong();
                    break;

                case "TinNhanMau":
                    OnTinNhanMau();
                    break;

                case "GuiSMS":
                    OnSendSMS();
                    break;

                case "CauHinhNoiSoiSieuAm":
                    OnHelpCauHinhNoiSoiSieuAm();
                    break;

                case "BaoCaoCongNoHopDong":
                    OnBaoCaoCongNoHopDong();
                    break;

                case "SMSLog":
                    OnSMSLog();
                    break;
            }
        }

        private void OnSMSLog()
        {
            this.Text = string.Format("{0} - SMS Log", Application.ProductName);
            ViewControl(_uSMSLog);
            _uSMSLog.DisplayAsThread();
        }

        private void OnBaoCaoCongNoHopDong()
        {
            this.Text = string.Format("{0} - Bao cao cong no hop dong", Application.ProductName);
            ViewControl(_uBaoCaoCongNoHopDong);
            _uBaoCaoCongNoHopDong.DisplayAsThread();
        }

        private void OnSendSMS()
        {
            this.Text = string.Format("{0} - Gui SMS", Application.ProductName);
            ViewControl(_uSendSMS);
            _uSendSMS.DisplayAsThread();
        }

        private void OnHelpCauHinhNoiSoiSieuAm()
        {
            dlgHelpCauHinhNoiSoiSieuAm dlg = new dlgHelpCauHinhNoiSoiSieuAm();
            dlg.ShowDialog(this);
        }

        private void OnTinNhanMau()
        {
            this.Text = string.Format("{0} - Tin nhan mau", Application.ProductName);
            ViewControl(_uTinNhanMauList);
            _uTinNhanMauList.DisplayAsThread();
        }

        private void OnKhamHopDong()
        {
            this.Text = string.Format("{0} - Kham hop dong", Application.ProductName);
            ViewControl(_uKhamHopDong);
            _uKhamHopDong.DisplayAsThread();
        }

        private void OnTVHomeConfig()
        {
            dlgTVHomeConfig dlg = new dlgTVHomeConfig();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                dlg.SetAppConfig();
                SaveAppConfig();
            }
        }

        private void OnKeToaCapCuu()
        {
            this.Text = string.Format("{0} - Ke toa cap cuu", Application.ProductName);
            ViewControl(_uToaCapCuuList);
            _uToaCapCuuList.DisplayAsThread();
        }

        private void OnNguoiSuDung()
        {
            this.Text = string.Format("{0} - Nguoi su dung", Application.ProductName);
            ViewControl(_uNguoiSuDungList);
            _uNguoiSuDungList.DisplayAsThread();
        }

        private void OnNhomNguoiSuDung()
        {
            this.Text = string.Format("{0} - Nhom nguoi su dung", Application.ProductName);
            ViewControl(_uUserGroupList);
            _uUserGroupList.DisplayAsThread();
        }

        private void OnGiaCapCuu()
        {
            this.Text = string.Format("{0} - Gia cap cuu", Application.ProductName);
            ViewControl(_uGiaCapCuuList);
            _uGiaCapCuuList.DisplayAsThread();
        }

        private void OnPhieuThuCapCuu()
        {
            this.Text = string.Format("{0} - Phieu thu cap cuu", Application.ProductName);
            ViewControl(_uPhieuThuCapCuuList);
            _uPhieuThuCapCuuList.DisplayAsThread();
        }

        private void OnBenhNhanNgoaiGoiKham()
        {
            this.Text = string.Format("{0} - Benh nhan ngoai goi kham", Application.ProductName);
            ViewControl(_uBenhNhanNgoaiGoiKhamList);
            _uBenhNhanNgoaiGoiKhamList.DisplayAsThread();
        }

        private void OnThongBao()
        {
            this.Text = string.Format("{0} - Thong bao", Application.ProductName);
            ViewControl(_uThongBaoList);
            _uThongBaoList.DisplayAsThread();
        }

        private void OnCauHinhKhoCapCuu()
        {
            dlgCauHinhKhoCapCuu dlg = new dlgCauHinhKhoCapCuu();
            dlg.ShowDialog(this);
        }

        private void OnBaoCaoTonKhoCapCuu()
        {
            this.Text = string.Format("{0} - Bao cao ton kho cap cuu", Application.ProductName);
            ViewControl(_uBaoCaoTonKhoCapCuu);
            _uBaoCaoTonKhoCapCuu.DisplayAsThread();
        }

        private void OnBaoCaoCapCuuHetHan()
        {
            this.Text = string.Format("{0} - Bao cao cap cuu het han", Application.ProductName);
            ViewControl(_uBaoCaoCapCuuHetHan);
            _uBaoCaoCapCuuHetHan.DisplayAsThread();
        }

        private void OnXuatKhoCapCuu()
        {
            this.Text = string.Format("{0} - Xuat kho cap cuu", Application.ProductName);
            ViewControl(_uXuatKhoCapCuuList);
            _uXuatKhoCapCuuList.DisplayAsThread();
        }

        private void OnNhapKhoCapCuu()
        {
            this.Text = string.Format("{0} - Nhap kho cap cuu", Application.ProductName);
            ViewControl(_uNhapKhoCapCuuList);
            _uNhapKhoCapCuuList.DisplayAsThread();
        }

        private void OnKhoCapCuu()
        {
            this.Text = string.Format("{0} - Danh muc cap cuu", Application.ProductName);
            ViewControl(_uKhoCapCuu);
            _uKhoCapCuu.DisplayAsThread();
        }

        private void OnCongTacNgoaiGio()
        {
            this.Text = string.Format("{0} - Cong tac ngoai gio", Application.ProductName);
            ViewControl(_uCongTacNgoaiGioList);
            _uCongTacNgoaiGioList.DisplayAsThread();
        }

        private void OnLichKham()
        {
            this.Text = string.Format("{0} - Lich kham", Application.ProductName);
            ViewControl(_uLichKham);
            _uLichKham.DisplayAsThread();
        }

        private void OnCauHinhSoNgayBaoTiemNgua()
        {
            dlgCauHinhSoNgayBaoTiemNgua dlg = new dlgCauHinhSoNgayBaoTiemNgua();
            dlg.ShowDialog(this);
        }

        private void OnTiemNgua()
        {
            this.Text = string.Format("{0} - Tiem ngua", Application.ProductName);
            ViewControl(_uTiemNguaList);
            _uTiemNguaList.DisplayAsThread();
        }

        private void OnLoaiSieuAm()
        {
            this.Text = string.Format("{0} - Loai sieu am", Application.ProductName);
            ViewControl(_uLoaiSieuAmList);
            _uLoaiSieuAmList.DisplayAsThread();
        }

        private void OnXetNghiem()
        {
            this.Text = string.Format("{0} - Xet nghiem", Application.ProductName);
            ViewControl(_uXetNghiem);
            _uXetNghiem.InitData();
        }

        private void OnBenhNhanThanThuoc()
        {
            this.Text = string.Format("{0} - Benh nhan than thuoc", Application.ProductName);
            ViewControl(_uBenhNhanThanThuocList);
            _uBenhNhanThanThuocList.DisplayAsThread();
        }

        private void OnCauHinhTrangIn()
        {
            dlgPageSetupConfig dlg = new dlgPageSetupConfig();
            dlg.ShowDialog(this);
        }

        private void OnBaoCaoThuocTonKhoTheoKhoangThoiGian()
        {
            this.Text = string.Format("{0} - Bao cao thuoc ton kho", Application.ProductName);
            ViewControl(_uBaoCaoThuocTonKhoTheoKhoangThoiGian);
            _uBaoCaoThuocTonKhoTheoKhoangThoiGian.DisplayAsThread();
        }

        private void OnChiTietPhieuThuDichVu()
        {
            this.Text = string.Format("{0} - Danh sach dich vu xuat phieu thu", Application.ProductName);
            ViewControl(_uChiTietPhieuThuDichVu);
            _uChiTietPhieuThuDichVu.DisplayAsThread();
        }

        private void OnDiaChiCongTy()
        {
            this.Text = string.Format("{0} - Danh muc dia chi cong ty", Application.ProductName);
            ViewControl(_uDiaChiCongTyList);
            _uDiaChiCongTyList.DisplayAsThread();
        }

        private void OnTraCuuThongTinKhachHang()
        {
            this.Text = string.Format("{0} - Tra cuu thong tin khach hang", Application.ProductName);
            ViewControl(_uTraCuuThongTinKhachHang);
            _uTraCuuThongTinKhachHang.DisplayAsThread();
        }

        private void OnCauHinhFTP()
        {
            dlgFTPConfig dlg = new dlgFTPConfig();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (dlg.IsChangeConnectionInfo)
                {
                    dlg.SetAppConfig();
                    SaveAppConfig();
                }
            }
        }

        private void OnThayDoiSoHoaDon()
        {
            dlgLamMoiSoHoaDon dlg = new dlgLamMoiSoHoaDon();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                RefreshData();
            }
        }

        private void OnBaoCaoSoLuongKham()
        {
            this.Text = string.Format("{0} - Bao cao so luong kham", Application.ProductName);
            ViewControl(_uBaoCaoSoLuongKham);
        }

        private void OnDanhSachXetNghiemHitachi917()
        {
            this.Text = string.Format("{0} - Danh sach xet nghiem Hitachi917", Application.ProductName);
            ViewControl(_uDanhSachXetNghiemHitachi917List);
            _uDanhSachXetNghiemHitachi917List.DisplayAsThread();

        }

        private void OnDanhSachXetNghiemCellDyn3200()
        {
            this.Text = string.Format("{0} - Danh sach xet nghiem CellDyn3200", Application.ProductName);
            ViewControl(_uDanhSachXetNghiem_CellDyn3200List);
            _uDanhSachXetNghiem_CellDyn3200List.DisplayAsThread();
        }

        private void OnKetQuaXetNghiemTongHop()
        {
            this.Text = string.Format("{0} - Ket qua xet nghiem tong hop", Application.ProductName);
            ViewControl(_uKetQuaXetNghiemTongHop);
            _uKetQuaXetNghiemTongHop.UpdateGUI();
            _uKetQuaXetNghiemTongHop.ResizeGUI();
        }

        private void OnKetQuaXetNghiemTay()
        {
            this.Text = string.Format("{0} - Ket qua xet nghiem tay", Application.ProductName);
            ViewControl(_uKetQuaXetNghiemTay);
            _uKetQuaXetNghiemTay.DisplayAsThread();
            _uKetQuaXetNghiemTay.ResizeGUI();
        }

        private void OnXetNghiemTay()
        {
            this.Text = string.Format("{0} - Danh sach xet nghiem tay", Application.ProductName);
            ViewControl(_uXetNghiemTay);
            _uXetNghiemTay.DisplayAsThread();
        }

        private void OnBaoCaoKhachHangMuaThuoc()
        {
            this.Text = string.Format("{0} - Bao cao khách hang mua thuoc", Application.ProductName);
            ViewControl(_uBaoCaoKhachHangMuaThuoc);
            _uBaoCaoKhachHangMuaThuoc.InitData();
        }

        private void OnCauHinhKetNoi()
        {
            dlgPortConfig dlg = new dlgPortConfig();
            dlg.ShowDialog(this);
            //Utility.ResetMMSerivice();
            //OpenCOMPort();
        }

        private void OnXetNghiem_CellDyn3200()
        {
            this.Text = string.Format("{0} - Ket qua xet nghiem CellDyn3200", Application.ProductName);
            ViewControl(_uKetQuaXetNghiem_CellDyn3200);
            _uKetQuaXetNghiem_CellDyn3200.DisplayAsThread();
            _uKetQuaXetNghiem_CellDyn3200.ResizeGUI();
        }

        private void OnXetNghiem_Hitachi917()
        {
            this.Text = string.Format("{0} - Ket qua xet nghiem Hitachi917", Application.ProductName);
            ViewControl(_uKetQuaXetNghiem_Hitachi917);
            _uKetQuaXetNghiem_Hitachi917.DisplayAsThread();
            _uKetQuaXetNghiem_Hitachi917.ResizeGUI();
        }

        private void OnBooking()
        {
            this.Text = string.Format("{0} - Lich hen.", Application.ProductName);
            ViewControl(_uBookingList);
            _uBookingList.DisplayAsThread();
        }

        private void OnNhatKyLienHeCongTy()
        {
            this.Text = string.Format("{0} - Nhat ky lien he cong ty.", Application.ProductName);
            ViewControl(_uNhatKyLienHeCongTy);
            _uNhatKyLienHeCongTy.DisplayAsThread();
        }

        private void OnYKienKhachHang()
        {
            this.Text = string.Format("{0} - Y kien khach hang.", Application.ProductName);
            ViewControl(_uYKienKhachHangList);
            _uYKienKhachHangList.DisplayBacSiPhuTrach();
            _uYKienKhachHangList.DisplayAsThread();
        }

        private void OnHoaDonHopDong()
        {
            this.Text = string.Format("{0} - Hoa don hop dong.", Application.ProductName);
            ViewControl(_uHoaDonHopDongList);
            _uHoaDonHopDongList.DisplayAsThread();
        }

        private void OnPhieuThuHopDong()
        {
            this.Text = string.Format("{0} - Phieu thu hop dong.", Application.ProductName);
            ViewControl(_uPhieuThuHopDongList);
            _uPhieuThuHopDongList.DisplayComboHopDong();
            _uPhieuThuHopDongList.DisplayAsThread();
        }

        private void OnPhucHoiBenhNhan()
        {
            this.Text = string.Format("{0} - Phuc hoi benh nhan da xoa.", Application.ProductName);
            ViewControl(_uPhucHoiBenhNhan);
            _uPhucHoiBenhNhan.DisplayAsThread();
        }

        private void OnThongKeHoaDon()
        {
            this.Text = string.Format("{0} - Thong ke hoa don", Application.ProductName);
            ViewControl(_uThongKeHoaDon);
            _uThongKeHoaDon.DisplayAsThread();
        }

        private void OnHoaDonXuatTruoc()
        {
            this.Text = string.Format("{0} - Hoa don xuat truoc", Application.ProductName);
            ViewControl(_uHoaDonXuatTruoc);
            _uHoaDonXuatTruoc.DisplayAsThread();
        }

        private void OnHoaDonThuoc()
        {
            this.Text = string.Format("{0} - Hoa don thuoc", Application.ProductName);
            ViewControl(_uHoaDonThuocList);
            _uHoaDonThuocList.DisplayAsThread();
        }

        private void OnBaoCaoDichVuChuaXuatPhieuThu()
        {
            this.Text = string.Format("{0} - Dich vu chua xuat phieu thu.", Application.ProductName);
            ViewControl(_uBaoCaoDichVuChuaXuatPhieuThu);
        }

        private void OnDoanhThuTheoNgay()
        {
            this.Text = string.Format("{0} - Bao cao doanh thu theo ngay", Application.ProductName);
            ViewControl(_uDoanhThuTheoNgay);
        }

        private void OnGiaVonDichVu()
        {
            this.Text = string.Format("{0} - Gia von dich vu", Application.ProductName);
            ViewControl(_uGiaVonDichVuList);
            _uGiaVonDichVuList.DisplayAsThread();
        }

        private void OnInKetQuaKhamSucKhoeTongQuat()
        {
            this.Text = string.Format("{0} - In ket qua kham suc khoe tong quat", Application.ProductName);
            ViewControl(_uInKetQuaKhamSucKhoeTongQuat);
            _uInKetQuaKhamSucKhoeTongQuat.InitData();
        }

        private void OnServiceGroup()
        {
            this.Text = string.Format("{0} - Nhom dich vu", Application.ProductName);
            ViewControl(_uServiceGroupList);
            _uServiceGroupList.DisplayAsThread();
        }

        private void OnTracking()
        {
            this.Text = string.Format("{0} - Truy vet", Application.ProductName);
            ViewControl(_uTrackingList);
            _uTrackingList.InitData();
        }

        private void OnDichVuTuTuc()
        {
            this.Text = string.Format("{0} - Bao cao dich vu tu tuc", Application.ProductName);
            ViewControl(_uDichVuTuTuc);
            _uDichVuTuTuc.InitData();
        }

        private void OnPhieuThuThuoc()
        {
            this.Text = string.Format("{0} - Phieu thu thuoc", Application.ProductName);
            ViewControl(_uPhieuThuThuocList);
            _uPhieuThuThuocList.DisplayAsThread();
        }

        private void OnThuocHetHan()
        {
            this.Text = string.Format("{0} - Bao cao thuoc het han", Application.ProductName);
            ViewControl(_uBaoCaoThuocHetHan);
            _uBaoCaoThuocHetHan.DisplayAsThread();
        }

        private void OnThuocTonKho()
        {
            this.Text = string.Format("{0} - Bao cao thuoc ton kho", Application.ProductName);
            ViewControl(_uBaoCaoThuocTonKho);
            _uBaoCaoThuocTonKho.DisplayAsThread();
        }

        private void OnKeToa()
        {
            this.Text = string.Format("{0} - Ke toa", Application.ProductName);
            ViewControl(_uToaThuocList);
            _uToaThuocList.DisplayAsThread();
        }

        private void OnGiaThuoc()
        {
            this.Text = string.Format("{0} - Gia thuoc", Application.ProductName);
            ViewControl(_uGiaThuocList);
            _uGiaThuocList.DisplayAsThread();
        }

        private void OnLoThuoc()
        {
            this.Text = string.Format("{0} - Lo thuoc", Application.ProductName);
            ViewControl(_uLoThuocList);
            _uLoThuocList.DisplayAsThread();
        }

        private void OnDanhMucThuoc()
        {
            this.Text = string.Format("{0} - Danh muc thuoc", Application.ProductName);
            ViewControl(_uThuocList);
            _uThuocList.DisplayAsThread();
        }

        private void OnNhomThuoc()
        {
            this.Text = string.Format("{0} - Nhom thuoc", Application.ProductName);
            ViewControl(_uNhomThuocList);
            _uNhomThuocList.DisplayAsThread();
        }

        private void OnDichVuHopDong()
        {
            this.Text = string.Format("{0} - Bao cao dich vu hop dong", Application.ProductName);
            ViewControl(_uDichVuHopDong);
            _uDichVuHopDong.DisplayAsThread();
        }

        private void OnDoanhThuNhanVien()
        {
            this.Text = string.Format("{0} - Bao cao doanh thu nhan vien", Application.ProductName);
            ViewControl(_uDoanhThuNhanVien);
            _uDoanhThuNhanVien.DisplayAsThread();
        }

        private void OnInvoiceList()
        {
            this.Text = string.Format("{0} - Hoa don dich vu", Application.ProductName);
            ViewControl(_uInvoiceList);
            _uInvoiceList.DisplayAsThread();
        }

        private void OnReceiptList()
        {
            this.Text = string.Format("{0} - Phieu thu dich vu", Application.ProductName);
            ViewControl(_uReceiptList);
            _uReceiptList.DisplayAsThread();
        }

        private void OnPrintLabel()
        {
            this.Text = string.Format("{0} - In nhan", Application.ProductName);
            ViewControl(_uPrintLabel);
            _uPrintLabel.DisplayAsThread();
        }

        private void OnChangePassword()
        {
            dlgChangePassword dlg = new dlgChangePassword();
            dlg.ShowDialog();
        }

        private void OnPermission()
        {
            this.Text = string.Format("{0} - Phan quyen", Application.ProductName);
            ViewControl(_uPermission);
            _uPermission.DisplayAsThread();
        }

        private void OnBieuMauPhongSale()
        {
            dlgTemplateForSale dlg = new dlgTemplateForSale();
            dlg.ShowDialog();
        }

        private void OnExcelTemplate()
        {
            dlgExcelTemplate dlg = new dlgExcelTemplate();
            dlg.ShowDialog();
        }

        private void OnDicom()
        {
            ViewDicom dlg = new ViewDicom();
            dlg.ShowDialog();
        }

        private void OnCompanyList()
        {
            this.Text = string.Format("{0} - Danh muc cong ty", Application.ProductName);
            ViewControl(_uCompanyList);
            _uCompanyList.DisplayAsThread();
        }

        private void OnContractList()
        {
            this.Text = string.Format("{0} - Danh muc hop dong", Application.ProductName);
            ViewControl(_uContractList);
            _uContractList.DisplayAsThread();
        }

        private void ViewControl(Control view)
        {
            view.Visible = true;

            foreach (Control ctrl in this._mainPanel.Controls)
            {
                if (ctrl != view)
                    ctrl.Visible = false;
            }
        }

        private Control GetControlActive()
        {
            foreach (Control ctrl in this._mainPanel.Controls)
            {
                if (ctrl.Visible == true)
                    return ctrl;
            }

            return null;
        }

        private void OnSymptomList()
        {
            this.Text = string.Format("{0} - Danh muc trieu chung", Application.ProductName);
            ViewControl(_uSymptomList);
            _uSymptomList.DisplayAsThread();
        }

        private void OnLogin()
        {
            dlgLogin dlgLogin = new dlgLogin();
            if (dlgLogin.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                loginToolStripMenuItem.Tag = "Logout";
                loginToolStripMenuItem.Text = "Đăng xuất";
                loginToolStripMenuItem.Image = Properties.Resources.Apps_session_logout_icon;
                tbLogin.Tag = "Logout";
                tbLogin.ToolTipText = "Đăng xuất";
                tbLogin.Image = Properties.Resources.Apps_session_logout_icon;
                statusLabel.Text = string.Format("Người đăng nhập: {0}", Global.Fullname);
                RefreshFunction(true);
                RefreshData();
            }
        }

        private void OnLogout()
        {
            if (MsgBox.Question(Application.ProductName, 
                "Bạn có muốn đăng xuất ?") == System.Windows.Forms.DialogResult.Yes)
            {
                loginToolStripMenuItem.Tag = "Login";
                loginToolStripMenuItem.Text = "Đăng nhập";
                loginToolStripMenuItem.Image = Properties.Resources.Login_icon;

                tbLogin.Tag = "Login";
                tbLogin.ToolTipText = "Đăng nhập";
                tbLogin.Image = Properties.Resources.Login_icon;
                statusLabel.Text = string.Empty;
                RefreshFunction(false);
                HideAllControls();
                ClearAllData();
            }
        }

        private void HideAllControls()
        {
            foreach (Control ctrl in this._mainPanel.Controls)
            {
                ctrl.Visible = false;
            }
        }

        private void ClearData()
        {
            dgPatient.DataSource = null;
            _uPatientHistory.ClearData();
            Global.dtOpenPatient.Rows.Clear();

            Control ctrl = GetControlActive();
            if (ctrl == null) return;

            ctrl.Enabled = false;
            if (ctrl.GetType() == typeof(uServicesList))
                _uServicesList.ClearData();
            else if (ctrl.GetType() == typeof(uDocStaffList))
                _uDocStaffList.ClearData();
            else if (ctrl.GetType() == typeof(uPatientList))
                _uPatientList.ClearData();
            else if (ctrl.GetType() == typeof(uSpecialityList))
                _uSpecialityList.ClearData();
            else if (ctrl.GetType() == typeof(uSymptomList))
                _uSymptomList.ClearData();
            else if (ctrl.GetType() == typeof(uCompanyList))
                _uCompanyList.ClearData();
            else if (ctrl.GetType() == typeof(uContractList))
                _uContractList.ClearData();
            else if (ctrl.GetType() == typeof(uPermission))
                _uPermission.ClearData();
            else if (ctrl.GetType() == typeof(uPrintLabel))
                _uPrintLabel.ClearData();
            else if (ctrl.GetType() == typeof(uReceiptList))
                _uReceiptList.ClearData();
            else if (ctrl.GetType() == typeof(uInvoiceList))
                _uInvoiceList.ClearData();
            else if (ctrl.GetType() == typeof(uDuplicatePatient))
                _uDuplicatePatient.ClearData();
            else if (ctrl.GetType() == typeof(uThuocList))
                _uThuocList.ClearData();
            else if (ctrl.GetType() == typeof(uNhomThuocList))
                _uNhomThuocList.ClearData();
            else if (ctrl.GetType() == typeof(uLoThuocList))
                _uLoThuocList.ClearData();
            else if (ctrl.GetType() == typeof(uPhieuThuThuocList))
                _uPhieuThuThuocList.ClearData();
            else if (ctrl.GetType() == typeof(uServiceGroupList))
                _uServiceGroupList.ClearData();
            else if (ctrl.GetType() == typeof(uGiaThuocList))
                _uGiaThuocList.ClearData();
            else if (ctrl.GetType() == typeof(uGiaVonDichVuList))
                _uGiaVonDichVuList.ClearData();
            else if (ctrl.GetType() == typeof(uPhieuThuHopDongList))
                _uPhieuThuHopDongList.ClearData();
            else if (ctrl.GetType() == typeof(uHoaDonHopDongList))
                _uHoaDonHopDongList.ClearData();
            else if (ctrl.GetType() == typeof(uYKienKhachHangList))
                _uYKienKhachHangList.ClearData();
            else if (ctrl.GetType() == typeof(uNhatKyLienHeCongTy))
                _uNhatKyLienHeCongTy.ClearData();
            else if (ctrl.GetType() == typeof(uDiaChiCongTyList))
                _uDiaChiCongTyList.ClearData();
            else if (ctrl.GetType() == typeof(uBenhNhanThanThuocList))
                _uBenhNhanThanThuocList.ClearData();
            else if (ctrl.GetType() == typeof(uKhoCapCuu))
                _uKhoCapCuu.ClearData();
        }

        private void OnDoctorList()
        {
            this.Text = string.Format("{0} - Danh muc nhan vien", Application.ProductName);
            ViewControl(_uDocStaffList);
            _uDocStaffList.DisplayAsThread();
        }

        private void OnDatabaseConfig()
        {
            dlgDatabaseConfig dlg = new dlgDatabaseConfig();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (dlg.IsChangeConnectionInfo)
                {
                    dlg.SetAppConfig();
                    SaveAppConfig();
                    //Utility.ResetMMSerivice();
                    RefreshData();
                }
            }
        }

        private void OnSpecialityList()
        {
            this.Text = string.Format("{0} - Danh muc chuyen khoa", Application.ProductName);
            ViewControl(_uSpecialityList);
            _uSpecialityList.DisplayAsThread();
        }

        private void OnExit()
        {
            this.Close();
        }

        private void OnServicesList()
        {
            this.Text = string.Format("{0} - Danh muc dich vu", Application.ProductName);
            ViewControl(_uServicesList);
            _uServicesList.DisplayAsThread();
        }

        private void OnPatientList()
        {
            this.Text = string.Format("{0} - Danh muc benh nhan", Application.ProductName);
            ViewControl(_uPatientList);
            _uPatientList.DisplayAsThread();
        }

        private void OnDuplicatePatient()
        {
            this.Text = string.Format("{0} - Danh muc trung lap benh nhan", Application.ProductName);
            ViewControl(_uDuplicatePatient);
            _uDuplicatePatient.DisplayAsThread();
        }

        private void OnOpenPatient()
        {
            dlgOpentPatient dlg = new dlgOpentPatient();
            //dlg.DataSource = _uPatientList.DataSource;
            //dlg.DictPatient = _uPatientList.DictPatient;
            
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DataRow patientRow = dlg.PatientRow;
                if (patientRow != null)
                {
                    string patientGUID = patientRow["PatientGUID"].ToString();
                    DataRow[] rows = Global.dtOpenPatient.Select(string.Format("PatientGUID='{0}'", patientGUID));
                    if (rows == null || rows.Length <= 0)
                    {
                        DataRow newRow = Global.dtOpenPatient.NewRow();
                        newRow["PatientGUID"] = patientRow["PatientGUID"];
                        newRow["FileNum"] = patientRow["FileNum"];
                        newRow["FullName"] = patientRow["FullName"];
                        newRow["GenderAsStr"] = patientRow["GenderAsStr"];
                        newRow["DobStr"] = patientRow["DobStr"];
                        newRow["IdentityCard"] = patientRow["IdentityCard"];
                        newRow["WorkPhone"] = patientRow["WorkPhone"];
                        newRow["Mobile"] = patientRow["Mobile"];
                        newRow["Email"] = patientRow["Email"];
                        newRow["Address"] = patientRow["Address"];
                        newRow["Thuoc_Di_Ung"] = patientRow["Thuoc_Di_Ung"];
                        Global.dtOpenPatient.Rows.Add(newRow);
                        OnPatientHistory(newRow);
                    }
                    else
                    {
                        rows[0]["PatientGUID"] = patientRow["PatientGUID"];
                        rows[0]["FileNum"] = patientRow["FileNum"];
                        rows[0]["FullName"] = patientRow["FullName"];
                        rows[0]["GenderAsStr"] = patientRow["GenderAsStr"];
                        rows[0]["DobStr"] = patientRow["DobStr"];
                        rows[0]["IdentityCard"] = patientRow["IdentityCard"];
                        rows[0]["WorkPhone"] = patientRow["WorkPhone"];
                        rows[0]["Mobile"] = patientRow["Mobile"];
                        rows[0]["Email"] = patientRow["Email"];
                        rows[0]["Address"] = patientRow["Address"];
                        rows[0]["Thuoc_Di_Ung"] = patientRow["Thuoc_Di_Ung"];
                        OnPatientHistory(rows[0]);
                    }
                }
            }

            //_uPatientList.DataSource = dlg.DataSource;
            //_uPatientList.DictPatient = dlg.DictPatient;
        }

        private void OnPatientHistory(DataRow patientRow)
        {
            this.Text = string.Format("{0} - Thong tin benh nhan", Application.ProductName);
            ViewControl(_uPatientHistory);
            string patientGUID = patientRow["PatientGUID"].ToString();
            _uPatientHistory.Display(patientRow);

        }

        private void OnHelp()
        {

        }

        private void OnAbout()
        {

        }

        private void InitConfigAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnInitConfigProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }

        }

        private void OnCheckAlert()
        {
            _isStartTiemNgua = false;
            _isStartCapCuuHetHSD = false;
            _isStartCapCuuHetTonKho = false;

            StopTimerShowAlert();

            //Tiêm Ngừa
            Result result = TiemNguaBus.CheckAlert();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                    _isStartTiemNgua = true;
            }
            else
                Utility.WriteToTraceLog(result.GetErrorAsString("TiemNguaBus.CheckAlert"));

            //Cấp cứu hết hạn sử dụng
            result = NhapKhoCapCuuBus.CheckKhoCapCuuHetHan();
            if (result.IsOK)
                _isStartCapCuuHetHSD = Convert.ToBoolean(result.QueryResult);
            else
                Utility.WriteToTraceLog(result.GetErrorAsString("NhapKhoCapCuuBus.CheckKhoCapCuuHetHan"));

            //Cấp cứu hết tồn kho
            result = NhapKhoCapCuuBus.CheckKhoCapCuuTonKho();
            if (result.IsOK)
                _isStartCapCuuHetTonKho = Convert.ToBoolean(result.QueryResult);
            else
                Utility.WriteToTraceLog(result.GetErrorAsString("NhapKhoCapCuuBus.CheckKhoCapCuuTonKho"));

            if (_isStartTiemNgua || _isStartCapCuuHetHSD || _isStartCapCuuHetTonKho)
                StartTimerShowAlert();
        }

        private void ClearAllData()
        {
            _uServicesList.ClearData();
            _uDocStaffList.ClearData();
            _uPatientList.ClearData();
            _uSpecialityList.ClearData();
            _uSymptomList.ClearData();
            _uCompanyList.ClearData();
            _uContractList.ClearData();
            _uPermission.ClearData();
            _uPrintLabel.ClearData();
            _uReceiptList.ClearData();
            _uInvoiceList.ClearData();
            _uDuplicatePatient.ClearData();
            _uDoanhThuNhanVien.ClearData();
            _uDichVuHopDong.ClearData();
            _uThuocList.ClearData();
            _uNhomThuocList.ClearData();
            _uLoThuocList.ClearData();
            _uGiaThuocList.ClearData();
            _uToaThuocList.ClearData();
            _uBaoCaoThuocHetHan.ClearData();
            _uBaoCaoThuocTonKho.ClearData();
            _uPhieuThuThuocList.ClearData();
            _uTrackingList.ClearData();
            _uServiceGroupList.ClearData();
            _uGiaVonDichVuList.ClearData();
            _uHoaDonThuocList.ClearData();
            _uHoaDonXuatTruoc.ClearData();
            _uThongKeHoaDon.ClearData();
            _uPhucHoiBenhNhan.ClearData();
            _uPhieuThuHopDongList.ClearData();
            _uHoaDonHopDongList.ClearData();
            _uYKienKhachHangList.ClearData();
            _uNhatKyLienHeCongTy.ClearData();
            _uXetNghiemTay.ClearData();
            _uKetQuaXetNghiemTongHop.UpdateGUI();
            _uTraCuuThongTinKhachHang.ClearData();
            _uDiaChiCongTyList.ClearData();
            _uChiTietPhieuThuDichVu.ClearData();
            _uBaoCaoThuocTonKhoTheoKhoangThoiGian.ClearData();
            _uBenhNhanThanThuocList.ClearData();
            _uTiemNguaList.ClearData();
            _uCongTacNgoaiGioList.ClearData();
            _uLichKham.ClearData();
            _uKhoCapCuu.ClearData();
            _uNhapKhoCapCuuList.ClearData();
            _uXuatKhoCapCuuList.ClearData();
            _uBaoCaoCapCuuHetHan.ClearData();
            _uBaoCaoTonKhoCapCuu.ClearData();
            _uThongBaoList.ClearData();
            _uBenhNhanNgoaiGoiKhamList.ClearData();
            _uGiaCapCuuList.ClearData();
            _uPhieuThuCapCuuList.ClearData();
            _uUserGroupList.ClearData();
            _uNguoiSuDungList.ClearData();
            _uToaCapCuuList.ClearData();
            _uKhamHopDong.ClearData();
            _uTinNhanMauList.ClearData();
            _uSendSMS.ClearData();
            _uBaoCaoCongNoHopDong.ClearData();
            _uSMSLog.ClearData();
        }
        #endregion

        #region Window Event Handlers
        private void _uPatientList_OnOpenPatient(DataRow patientRow)
        {
            OnPatientHistory(patientRow);
        }

        private void dgPatient_DoubleClick(object sender, EventArgs e)
        {
            if (dgPatient.SelectedRows == null || dgPatient.SelectedRows.Count <= 0) return;
            DataRow patientRow = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row;
            _flag = false;
            OnPatientHistory(patientRow);
            _flag = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitConfigAsThread();
            OnCheckAlert();
            StartTimerCheckAlert();

            if (!System.Diagnostics.Debugger.IsAttached)
                AutoDetectUpdateAsThread();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utility.XoaThongBaoTemp();
            if (_flag)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn thoát khỏi chương trình ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveAppConfig();
                    StopTimerShowAlert();
                    StopTimerCheckAlert();
                }
                else
                    e.Cancel = true;
            }
        }

        private void _mainToolbar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string cmd = e.ClickedItem.Tag as string;
            if (cmd == null || cmd == string.Empty) return;
            ExcuteCmd(cmd);
        }

        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            string cmd = (sender as ToolStripMenuItem).Tag as string;
            if (cmd == null || cmd == string.Empty) return;
            ExcuteCmd(cmd);
        }

        private void _timerShowAlert_Tick(object sender, EventArgs e)
        {
            if (_isStartTiemNgua) statusAlert.Visible = !statusAlert.Visible;
            if(_isStartCapCuuHetHSD) statusCapCuuHetHan.Visible = !statusCapCuuHetHan.Visible;
            if (_isStartCapCuuHetTonKho) statusCapCuuHetTonKho.Visible = !statusCapCuuHetTonKho.Visible;
        }

        private void _timerCheckAlert_Tick(object sender, EventArgs e)
        {
            OnCheckAlert();
        }

        private void statusAlert_DoubleClick(object sender, EventArgs e)
        {
            if (_isStartTiemNgua) OnTiemNgua();
        }

        private void statusCapCuuHetTonKho_DoubleClick(object sender, EventArgs e)
        {
            if (_isStartCapCuuHetTonKho) OnNhapKhoCapCuu();
        }

        private void statusCapCuuHetHan_DoubleClick(object sender, EventArgs e)
        {
            if (_isStartCapCuuHetHSD) OnNhapKhoCapCuu();
        }
        #endregion

        #region AutoUpdate
        private bool IsServerMachine
        {
            get
            {
                string computerName = System.Environment.MachineName;
                string server = Global.ConnectionInfo.ServerName;
                if (computerName.ToUpper() == server.ToUpper() || server.ToUpper() == "(LOCAL)")
                    return true;
                else if (server.Contains("\\"))
                {
                    int ind = server.IndexOf("\\");
                    string temp = server.Substring(0, ind);
                    if (temp.ToUpper() == computerName.ToUpper())
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        private string ReadInforFromFile(string filepath)
        {
            StreamReader re = null;
            string input = null;
            try
            {
                re = File.OpenText(filepath);
                input = re.ReadLine();
                if (input != null)
                {
                    re.Close();
                }
                return input;
            }
            catch
            {
                if (re != null)
                    re.Close();
                return input;
            }
        }

        private void RunUpdateFile(string filename)
        {
            MsgBox.Show("Cap nhat chuong trinh", "Bạn đang dùng phiên bản cũ. Vui lòng chạy tập tin MMSeup.exe để cập nhật chương trình", IconType.Information);
            Process p = new Process();
            p.StartInfo.FileName = filename;
            p.Start();
            p.WaitForExit();
        }

        private void AutoDetectUpdateAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(AutoDetectUpdateProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void AutoDetectUpdate()
        {
            string strServerName;
            //read infor from server
            strServerName = Global.ConnectionInfo.ServerName;
            if (strServerName.Contains("\\"))
            {
                int ind = strServerName.IndexOf("\\");
                strServerName = strServerName.Substring(0, ind);
            }
            //hard code servername
            strServerName = "192.168.5.250";
            string fileOnServer = string.Format("\\\\{0}\\MMupdatedDate\\updatedDate.txt", strServerName);
            if (!File.Exists(fileOnServer))
            {
                //MsgBox.Show("Cap nhat chuong trinh", "Vui cập nhật chương trình trên server.(\\192.168.5.250)", IconType.Information);
                return;
            }
            string serverUpdatedDate = ReadInforFromFile(fileOnServer);
            if (serverUpdatedDate == null)
                return;
            //read file from client
            if (!IsServerMachine)
            {
                string storagePath = Path.Combine(Application.StartupPath, "updatedDate.txt");
                if (File.Exists(storagePath))
                {
                    //read infor from local file
                    string localUpdatedDate = ReadInforFromFile(storagePath);
                    if (localUpdatedDate != serverUpdatedDate)
                    {
                        //call update here
                        RunUpdateFile(string.Format("\\\\{0}\\MMupdatedDate\\MMSetup.exe", strServerName));
                        //copy the file from server tolocal
                        File.Copy(fileOnServer, storagePath, true);
                    }
                }
                else
                {
                    //Call update Mn & MIMS here
                    //MessageBox.Show("Call update MM here");
                }
            }
        }
        #endregion

        #region Working Thread
        private void AutoDetectUpdateProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                AutoDetectUpdate();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnInitConfigProc(object state)
        {
            try
            {
                //Thread.Sleep(1000);
                OnInitConfig();
            }
            catch (Exception e)
            {
                MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion

        #region COM
        //private void CloseAllCOMPort()
        //{
        //    foreach (SerialPort p in _ports)
        //    {
        //        p.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived);
        //        p.ErrorReceived -= new System.IO.Ports.SerialErrorReceivedEventHandler(port_ErrorReceived);
        //        p.Close();
        //    }
        //}

        //private SerialPort GetPort(string portName)
        //{
        //    foreach (SerialPort p in _ports)
        //    {
        //        if (p.PortName == portName)
        //            return p;
        //    }

        //    return null;
        //}

        //private void OpenCOMPort()
        //{
        //    if (File.Exists(Global.PortConfigPath))
        //    {
        //        Global.PortConfigCollection.Deserialize(Global.PortConfigPath);

        //        List<SerialPort> removePorts = new List<SerialPort>();
        //        foreach (SerialPort p in _ports)
        //        {
        //            if (!Global.PortConfigCollection.CheckPortNameTonTai(p.PortName, string.Empty))
        //            {
        //                try
        //                {
        //                    p.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived);
        //                    p.ErrorReceived -= new System.IO.Ports.SerialErrorReceivedEventHandler(port_ErrorReceived);
        //                    p.Close();
        //                    removePorts.Add(p);
        //                }
        //                catch (Exception ex)
        //                {
        //                    Utility.WriteToTraceLog(ex.Message);
        //                }
        //            }
        //        }

        //        foreach (SerialPort p in removePorts)
        //        {
        //            _ports.Remove(p);
        //        }

        //        foreach (PortConfig p in Global.PortConfigCollection.PortConfigList)
        //        {
        //            try
        //            {
        //                SerialPort port = GetPort(p.PortName);
        //                if (port == null)
        //                {
        //                    port = new SerialPort();
        //                    port.BaudRate = 9600;
        //                    port.DataBits = 8;
        //                    port.DiscardNull = false;
        //                    port.DtrEnable = true;
        //                    port.Handshake = Handshake.XOnXOff;
        //                    port.Parity = Parity.None;
        //                    port.ParityReplace = 63;
        //                    port.PortName = p.PortName;
        //                    port.ReadBufferSize = 4096;
        //                    port.ReadTimeout = -1;
        //                    port.ReceivedBytesThreshold = 1;
        //                    port.RtsEnable = true;
        //                    port.StopBits = StopBits.One;
        //                    port.WriteBufferSize = 2048;
        //                    port.WriteTimeout = -1;
        //                    port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived);
        //                    port.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(port_ErrorReceived);
        //                    port.Open();
        //                    _ports.Add(port);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Utility.WriteToTraceLog(ex.Message);
        //            }
        //        }
        //    }
        //}

        //private void port_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        //{
        //    Utility.WriteToTraceLog(string.Format("SerialPort: Error received data '{0}'", e.EventType.ToString()));
        //}

        //private void port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        //{
        //    try
        //    {
        //        SerialPort port = (SerialPort)sender;
        //        PortConfig portConfig = Global.PortConfigCollection.GetPortConfigByPortName(port.PortName);
        //        if (portConfig == null) return;

        //        string data = port.ReadExisting();

        //        if (portConfig.LoaiMay == LoaiMayXN.Hitachi917)
        //        {
        //            List<TestResult_Hitachi917> testResults = ParseTestResult_Hitachi917(data, port.PortName);
        //            Result result = XetNghiem_Hitachi917Bus.InsertKQXN(testResults);
        //            if (!result.IsOK)
        //                Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_Hitachi917Bus.InsertKQXN"));
        //        }
        //        else if (portConfig.LoaiMay == LoaiMayXN.CellDyn3200)
        //        {
        //            List<TestResult_CellDyn3200> testResults = ParseTestResult_CellDyn3200(data, port.PortName);
        //            Result result = XetNghiem_CellDyn3200Bus.InsertKQXN(testResults);
        //            if (!result.IsOK)
        //                Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_CellDyn3200Bus.InsertKQXN"));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.WriteToTraceLog(string.Format("Serial Port: {0}", ex.Message));
        //    }
        //}

        //private List<TestResult_Hitachi917> ParseTestResult_Hitachi917(string result, string portName)
        //{
        //    //result = "217:n1    1    1  11DINH ALCAM      3 0319121056XN      9  3  62.2  10   4.8  13  75.7  16  18.2  17   4.1  18  25.8  19  18.0  34   2.9  36   5.2 A1\r218:n1    2    1  21LOC ACE         3 0319121058XN      6 10   6.1  16  27.2  17   5.2  18  18.6  19  17.8  21   1.3 34\r";

        //    //while (result[0] == '\x02')
        //    //{
        //    //    result = result.Substring(1, result.Length - 1);
        //    //}

        //    //result = '\x02' + result;

        //    string[] strArr = result.Split('\r');

        //    List<TestResult_Hitachi917> testResults = new List<TestResult_Hitachi917>();
        //    if (strArr != null && strArr.Length > 0)
        //    {
        //        string lastResult = string.Empty;
        //        if (_htLastResult.ContainsKey(portName))
        //            lastResult = _htLastResult[portName].ToString();
        //        else
        //            _htLastResult.Add(portName, string.Empty);

        //        strArr[0] = string.Format("{0}{1}", lastResult, strArr[0]);
        //        _htLastResult[portName] = strArr[strArr.Length - 1];

        //        for (int i = 0; i < strArr.Length - 1; i++)
        //        {
        //            result = strArr[i];
        //            int istart = result.IndexOf('\x02');
        //            int iEnd = result.IndexOf('\x03');
        //            if (istart != 0) continue;
        //            if (istart < 0 || iEnd < 0) continue;

        //            TestResult_Hitachi917 testResult = new TestResult_Hitachi917();
        //            testResult.STX = result[0];
        //            testResult.Receiver = result[1];
        //            testResult.Sender = result[2];
        //            testResult.PakageNum = result[3];
        //            testResult.Frame = result[4];
        //            testResult.FunctionCode = result[5];
        //            testResult.SampleClass = result[6];
        //            testResult.SampleNum = result.Substring(7, 5);
        //            testResult.DiskNum = result.Substring(12, 5);
        //            testResult.PositionNum = result.Substring(17, 3);
        //            testResult.SampleCup = result.Substring(20, 1);
        //            testResult.IDNum = result.Substring(21, 13);
        //            testResult.Age = result.Substring(34, 3);
        //            testResult.AgeUnit = result.Substring(37, 1);
        //            testResult.Sex = result.Substring(38, 1);
        //            testResult.CollectionDate = result.Substring(39, 6);
        //            testResult.CollectionTime = result.Substring(45, 4);
        //            testResult.OperatorID = result.Substring(49, 6);
        //            int resultCount = Convert.ToInt32(result.Substring(55, 3));

        //            for (int j = 0; j < resultCount; j++)
        //            {
        //                Result_Hitachi917 r = new Result_Hitachi917();
        //                r.TestNum = Convert.ToInt32(result.Substring((58 + j * 10), 3));
        //                r.Result = result.Substring(61 + j * 10, 6);
        //                r.AlarmCode = result.Substring(67 + j * 10, 1);
        //                testResult.Results.Add(r);
        //            }

        //            testResults.Add(testResult);
        //        }
        //    }

        //    return testResults;
        //}

        //private List<TestResult_CellDyn3200> ParseTestResult_CellDyn3200(string result, string portName)
        //{
        //    List<TestResult_CellDyn3200> testResults = new List<TestResult_CellDyn3200>();
        //    //result = "\"   \",\"CD3200C\",\"------------\",3280,0,0,\"AVER124     \",\"BUI THI NGHIA TD            \",\"----------------\",\"F\",\"--/--/----\",\"----------------------\",\".  \",\"04/14/2012\",\"17:38\",\"--/--/----\",\"--:--\",\"----------------\",06.34,04.12,01.59,0.307,0.218,0.096,04.36,012.0,038.0,087.0,027.5,031.6,012.7,00254,06.53,0.166,016.7,065.1,025.1,04.85,03.45,01.51,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,00000,00000,00000,00000,00000,00000,00000,00000,-----,06.34,1,0,0,0,0,0,0,70\"   \",\"CD3200C\",\"------------\",3280,0,0,\"AVER124     \",\"BUI THI NGHIA TD            \",\"----------------\",\"F\",\"--/--/----\",\"----------------------\",\".  \",\"04/14/2012\",\"17:38\",\"--/--/----\",\"--:--\",\"----------------\",06.34,04.12,01.59,0.307,0.218,0.096,04.36,012.0,038.0,087.0,027.5,031.6,012.7,00254,06.53,0.166,016.7,065.1,025.1,04.85,03.45,01.51,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,00000,00000,00000,00000,00000,00000,00000,00000,-----,06.34,1,0,0,0,0,0,0,70";
        //    result = result.Replace("\"", "");

        //    string[] strArr = result.Split("".ToCharArray());

        //    if (strArr != null && strArr.Length > 0)
        //    {
        //        string lastResult = string.Empty;
        //        if (_htLastResult.ContainsKey(portName))
        //            lastResult = _htLastResult[portName].ToString();
        //        else
        //            _htLastResult.Add(portName, string.Empty);

        //        strArr[0] = string.Format("{0}{1}", lastResult, strArr[0]);
        //        _htLastResult[portName] = strArr[strArr.Length - 1];

        //        for (int i = 0; i < strArr.Length - 1; i++)
        //        {
        //            TestResult_CellDyn3200 testResult = new TestResult_CellDyn3200();
        //            result = strArr[i];
        //            string[] arrResult = result.Split(",".ToCharArray(), StringSplitOptions.None);
        //            if (arrResult == null || arrResult.Count() == 0) continue;

        //            testResult.KetQuaXetNghiem.MessageType = arrResult[0].Trim().Substring(1);
        //            testResult.KetQuaXetNghiem.InstrumentType = arrResult[1].Trim();
        //            testResult.KetQuaXetNghiem.SerialNum = arrResult[2].Trim();
        //            testResult.KetQuaXetNghiem.SequenceNum = Convert.ToInt32(arrResult[3].Trim());
        //            testResult.KetQuaXetNghiem.SpareField = arrResult[4].Trim();
        //            testResult.KetQuaXetNghiem.SpecimenType = Convert.ToInt32(arrResult[5].Trim());
        //            if (testResult.KetQuaXetNghiem.SpecimenType != 0) continue;

        //            testResult.KetQuaXetNghiem.SpecimenID = arrResult[6].Trim();
        //            testResult.KetQuaXetNghiem.SpecimenName = arrResult[7].Trim();
        //            testResult.KetQuaXetNghiem.PatientID = arrResult[8].Trim();
        //            testResult.KetQuaXetNghiem.SpecimenSex = arrResult[9].Trim();
        //            if (testResult.KetQuaXetNghiem.SpecimenSex == "-") testResult.KetQuaXetNghiem.SpecimenSex = string.Empty;

        //            testResult.KetQuaXetNghiem.SpecimenDOB = arrResult[10].Trim();
        //            if (testResult.KetQuaXetNghiem.SpecimenDOB == "--/--/----") testResult.KetQuaXetNghiem.SpecimenDOB = string.Empty;

        //            testResult.KetQuaXetNghiem.DrName = arrResult[11].Trim();
        //            testResult.KetQuaXetNghiem.OperatorID = arrResult[12].Trim();

        //            testResult.KetQuaXetNghiem.NgayXN = DateTime.ParseExact(string.Format("{0} {1}", arrResult[13].Trim(), arrResult[14].Trim()),
        //                    "MM/dd/yyyy HH:mm", null);

        //            testResult.KetQuaXetNghiem.CollectionDate = arrResult[15].Trim();
        //            testResult.KetQuaXetNghiem.CollectionTime = arrResult[16].Trim();
        //            testResult.KetQuaXetNghiem.Comment = arrResult[17].Trim();

        //            //WBC
        //            ChiTietKetQuaXetNghiem_CellDyn3200 ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
        //            ctkqxn.TenXetNghiem = "WBC";
        //            ctkqxn.TestResult = Convert.ToDouble(arrResult[18].Trim());
        //            testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

        //            //NEU
        //            ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
        //            ctkqxn.TenXetNghiem = "NEU";
        //            ctkqxn.TestResult = Convert.ToDouble(arrResult[19].Trim());
        //            ctkqxn.TestPercent = Convert.ToDouble(arrResult[35].Trim());
        //            testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

        //            //LYM
        //            ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
        //            ctkqxn.TenXetNghiem = "LYM";
        //            ctkqxn.TestResult = Convert.ToDouble(arrResult[20].Trim());
        //            ctkqxn.TestPercent = Convert.ToDouble(arrResult[36].Trim());
        //            testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

        //            //MONO
        //            ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
        //            ctkqxn.TenXetNghiem = "MONO";
        //            ctkqxn.TestResult = Convert.ToDouble(arrResult[21].Trim());
        //            ctkqxn.TestPercent = Convert.ToDouble(arrResult[37].Trim());
        //            testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

        //            //EOS
        //            ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
        //            ctkqxn.TenXetNghiem = "EOS";
        //            ctkqxn.TestResult = Convert.ToDouble(arrResult[22].Trim());
        //            ctkqxn.TestPercent = Convert.ToDouble(arrResult[38].Trim());
        //            testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

        //            //BASO
        //            ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
        //            ctkqxn.TenXetNghiem = "BASO";
        //            ctkqxn.TestResult = Convert.ToDouble(arrResult[23].Trim());
        //            ctkqxn.TestPercent = Convert.ToDouble(arrResult[39].Trim());
        //            testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

        //            //RBC
        //            ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
        //            ctkqxn.TenXetNghiem = "RBC";
        //            ctkqxn.TestResult = Convert.ToDouble(arrResult[24].Trim());
        //            testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

        //            //HGB
        //            ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
        //            ctkqxn.TenXetNghiem = "HGB";
        //            ctkqxn.TestResult = Convert.ToDouble(arrResult[25].Trim());
        //            testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

        //            //HCT
        //            ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
        //            ctkqxn.TenXetNghiem = "HCT";
        //            ctkqxn.TestResult = Convert.ToDouble(arrResult[26].Trim());
        //            testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

        //            //MCV
        //            ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
        //            ctkqxn.TenXetNghiem = "MCV";
        //            ctkqxn.TestResult = Convert.ToDouble(arrResult[27].Trim());
        //            testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

        //            //MCH
        //            ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
        //            ctkqxn.TenXetNghiem = "MCH";
        //            ctkqxn.TestResult = Convert.ToDouble(arrResult[28].Trim());
        //            testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

        //            //MCHC
        //            ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
        //            ctkqxn.TenXetNghiem = "MCHC";
        //            ctkqxn.TestResult = Convert.ToDouble(arrResult[29].Trim());
        //            testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

        //            //RDW
        //            ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
        //            ctkqxn.TenXetNghiem = "RDW";
        //            ctkqxn.TestResult = Convert.ToDouble(arrResult[30].Trim());
        //            testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

        //            //PLT
        //            ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
        //            ctkqxn.TenXetNghiem = "PLT";
        //            ctkqxn.TestResult = Convert.ToDouble(arrResult[31].Trim());
        //            testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

        //            //MPV
        //            ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
        //            ctkqxn.TenXetNghiem = "MPV";
        //            ctkqxn.TestResult = Convert.ToDouble(arrResult[32].Trim());
        //            testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

        //            //PCT
        //            ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
        //            ctkqxn.TenXetNghiem = "PCT";
        //            ctkqxn.TestResult = Convert.ToDouble(arrResult[33].Trim());
        //            testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

        //            //PDW
        //            ctkqxn = new ChiTietKetQuaXetNghiem_CellDyn3200();
        //            ctkqxn.TenXetNghiem = "PDW";
        //            ctkqxn.TestResult = Convert.ToDouble(arrResult[34].Trim());
        //            testResult.ChiTietKetQuaXetNghiem.Add(ctkqxn);

        //            testResults.Add(testResult);
        //        }
        //    }

        //    return testResults;
        //}
        #endregion
    }
}
