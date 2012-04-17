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
        private string _lastResult = string.Empty;
        private List<SerialPort> _ports = new List<SerialPort>();
        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();
            _uPatientList.OnOpenPatient += new OpenPatientHandler(_uPatientList_OnOpenPatient);
            _uCompanyList.OnOpenPatient += new OpenPatientHandler(_uPatientList_OnOpenPatient);
            _uContractList.OnOpenPatient += new OpenPatientHandler(_uPatientList_OnOpenPatient);
            _uPhongChoList.OnOpenPatient += new OpenPatientHandler(_uPatientList_OnOpenPatient);

            OpenCOMPort();
        }
        #endregion

        #region UI Command
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

                    if (!Global.ConnectionInfo.TestConnection())
                    {
                        dlgDatabaseConfig dlg = new dlgDatabaseConfig();
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            dlg.SetAppConfig();
                            SaveAppConfig();
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
            };

            if (InvokeRequired) BeginInvoke(method);
            else method.Invoke();

            
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
                _uPhieuThuHopDongList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uHoaDonHopDongList))
                _uHoaDonHopDongList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uYKienKhachHangList))
                _uYKienKhachHangList.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uNhatKyLienHeCongTy))
                _uNhatKyLienHeCongTy.DisplayAsThread();
            else if (ctrl.GetType() == typeof(uBookingList))
                _uBookingList.DisplayAsThread();
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
            Configuration.SaveData(Global.AppConfig);
        }

        private void RefreshFunction(bool isLogin)
        {
            if (Global.StaffType != StaffType.Admin)
            {
                toolsToolStripMenuItem.Enabled = isLogin;
                changePasswordToolStripMenuItem.Enabled = isLogin;

                Global.AllowShowServiePrice = false;
                Global.AllowExportReceipt = false;
                Global.AllowPrintReceipt = false;
                Global.AllowExportInvoice = false;
                Global.AllowPrintInvoice = false;
                Global.AllowViewChiDinh = false;
                Global.AllowAddChiDinh = false;
                Global.AllowEditChiDinh = false;
                Global.AllowDeleteChiDinh = false;
                Global.AllowConfirmChiDinh = false;
                Global.AllowAddPhongCho = false;

                Result result = LogonBus.GetPermission(Global.LogonGUID);
                if (result.IsOK)
                {
                    DataTable dtPermission = result.QueryResult as DataTable;
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

                            Global.AllowPrintReceipt = isPrint;
                            Global.AllowExportReceipt = isExport;
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

                            Global.AllowPrintInvoice = isPrint;
                            Global.AllowExportInvoice = isExport;
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
                        else if (functionCode == Const.ThuocTonKho)
                        {
                            thuocTonKhoToolStripMenuItem.Enabled = isView && isLogin;
                            _uBaoCaoThuocTonKho.AllowAdd = isAdd;
                            _uBaoCaoThuocTonKho.AllowEdit = isEdit;
                            _uBaoCaoThuocTonKho.AllowDelete = isDelete;
                            _uBaoCaoThuocTonKho.AllowPrint = isPrint;
                            _uBaoCaoThuocTonKho.AllowExport = isExport;
                            _uBaoCaoThuocTonKho.AllowImport = isImport;
                            _uBaoCaoThuocTonKho.AllowLock = isLock;
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

                            Global.AllowPrintInvoice = isPrint;
                            Global.AllowExportInvoice = isExport;
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

                            Global.AllowPrintInvoice = isPrint;
                            Global.AllowExportInvoice = isExport;
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
                            Global.AllowPrintInvoice = isPrint;
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
                        }
                        else if (functionCode == Const.YKienKhachHang)
                        {
                            chamSocKhachHangToolStripMenuItem.Enabled = isLogin;
                            yKienKhachHangToolStripMenuItem.Enabled = isView && isLogin;
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
                            Global.AllowExportDichVuDaSuDung = isExport;
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
                Global.AllowExportReceipt = true;
                Global.AllowPrintReceipt = true;
                Global.AllowExportInvoice = true;
                Global.AllowPrintInvoice = true;
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
                Global.AllowExportDichVuDaSuDung = true;
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
                    OnServicesList();
                    break;

                case "Patient List":
                    OnPatientList();
                    break;

                case "Open Patient":
                    OnOpenPatient();
                    break;

                case "DuplicatePatient":
                    OnDuplicatePatient();
                    break;

                case "Doctor List":
                    OnDoctorList();
                    break;

                case "Speciality List":
                    OnSpecialityList();
                    break;

                case "Help":
                    OnHelp();
                    break;

                case "About":
                    OnAbout();
                    break;

                case "Symptom List":
                    OnSymptomList();
                    break;

                case "Company List":
                    OnCompanyList();
                    break;

                case "Contract List":
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
                    OnPermission();
                    break;

                case "Print Label":
                    OnPrintLabel();
                    break;

                case "Receipt List":
                    OnReceiptList();
                    break;

                case "Invoice List":
                    OnInvoiceList();
                    break;

                case "DoanhThuNhanVien":
                    OnDoanhThuNhanVien();
                    break;
                    
                case "DichVuHopDong":
                    OnDichVuHopDong();
                    break;

                case "DanhMucThuoc":
                    OnDanhMucThuoc();
                    break;

                case "NhomThuoc":
                    OnNhomThuoc();
                    break;

                case "LoThuoc":
                    OnLoThuoc();
                    break;

                case "GiaThuoc":
                    OnGiaThuoc();
                    break;

                case "KeToa":
                    OnKeToa();
                    break;

                case "ThuocHetHan":
                    OnThuocHetHan();
                    break;

                case "ThuocTonKho":
                    OnThuocTonKho();
                    break;

                case "PhieuThuThuoc":
                    OnPhieuThuThuoc();
                    break;

                case "DichVuTuTuc":
                    OnDichVuTuTuc();
                    break;

                case "Tracking":
                    OnTracking();
                    break;

                case "ServiceGroup":
                    OnServiceGroup();
                    break;

                case "InKetQuaKhamSucKhoeTongQuat":
                    OnInKetQuaKhamSucKhoeTongQuat();
                    break;

                case "GiaVonDichVu":
                    OnGiaVonDichVu();
                    break;

                case "DoanhThuTheoNgay":
                    OnDoanhThuTheoNgay();
                    break;

                case "DichVuChuaXuatPhieuThu":
                    OnBaoCaoDichVuChuaXuatPhieuThu();
                    break;

                case "HoaDonThuoc":
                    OnHoaDonThuoc();
                    break;

                case "HoaDonXuatTruoc":
                    OnHoaDonXuatTruoc();
                    break;

                case "ThongKeHoaDon":
                    OnThongKeHoaDon();
                    break;

                case "PhucHoiBenhNhan":
                    OnPhucHoiBenhNhan();
                    break;

                case "PhieuThuHopDong":
                    OnPhieuThuHopDong();
                    break;
                    
                case "HoaDonHopDong":
                    OnHoaDonHopDong();
                    break;

                case "YKienKhachHang":
                    OnYKienKhachHang();
                    break;

                case "NhatKyLienHeCongTy":
                    OnNhatKyLienHeCongTy();
                    break;

                case "Booking":
                    OnBooking();
                    break;

                case "XetNghiem_Hitachi917":
                    OnXetNghiem_Hitachi917();        
                    break;
            }
        }

        private void OnXetNghiem_Hitachi917()
        {
            this.Text = string.Format("{0} - Xet nghiem Hitachi917.", Application.ProductName);

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
                ClearData();
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
            dlg.DataSource = _uPatientList.DataSource;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                OnPatientHistory(dlg.PatientRow);
            }
        }

        private void OnPatientHistory(object patientRow)
        {
            if (_flag) AddPatientToList((DataRow)patientRow);

            this.Text = string.Format("{0} - Thong tin benh nhan", Application.ProductName);
            ViewControl(_uPatientHistory);
            string patientGUID = (patientRow as DataRow)["PatientGUID"].ToString();
            //_uPatientHistory.PatientRow = patientRow;
            DataTable dtPatient = _uPatientList.DataSource as DataTable;
            _uPatientHistory.Display(patientGUID, dtPatient);

        }

        private void AddPatientToList(DataRow patientRow)
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt == null) dt = patientRow.Table.Clone();

            DataRow[] rows = dt.Select(string.Format("PatientGUID='{0}'", patientRow["PatientGUID"].ToString()));
            if (rows == null || rows.Length <= 0)
                dt.ImportRow(patientRow);

            dgPatient.DataSource = dt;
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
        #endregion

        #region Window Event Handlers
        private void _uPatientList_OnOpenPatient(object patientRow)
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

            if (!System.Diagnostics.Debugger.IsAttached)
                AutoDetectUpdateAsThread();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_flag)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn thoát khỏi chương trình ?") == System.Windows.Forms.DialogResult.Yes)
                    SaveAppConfig();
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
        private void OpenCOMPort()
        {
            foreach (string portName in SerialPort.GetPortNames())
            {
                try
                {
                    SerialPort port = new SerialPort();
                    port.BaudRate = 9600;
                    port.DataBits = 8;
                    port.DiscardNull = false;
                    port.DtrEnable = true;
                    port.Handshake = Handshake.XOnXOff;
                    port.Parity = Parity.None;
                    port.ParityReplace = 63;
                    port.PortName = portName;
                    port.ReadBufferSize = 4096;
                    port.ReadTimeout = -1;
                    port.ReceivedBytesThreshold = 1;
                    port.RtsEnable = true;
                    port.StopBits = StopBits.One;
                    port.WriteBufferSize = 2048;
                    port.WriteTimeout = -1;
                    port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived);
                    port.ErrorReceived += new System.IO.Ports.SerialErrorReceivedEventHandler(port_ErrorReceived);
                    port.Open();
                    _ports.Add(port);
                }
                catch (Exception ex)
                {
                    Utility.WriteToTraceLog(ex.Message);   
                }
            }
        }

        private void port_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {
            Utility.WriteToTraceLog(string.Format("SerialPort: Error received data '{0}'", e.EventType.ToString()));
        }

        private void port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort port = (SerialPort)sender;
                string data = port.ReadExisting();

                List<TestResult_Hitachi917> testResults = ParseTestResult_Hitachi917(data);
                Result result = XetNghiem_Hitachi917Bus.InsertKQXN(testResults);
                if (!result.IsOK)
                    Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_Hitachi917Bus.InsertKQXN"));
            }
            catch (Exception ex)
            {
                Utility.WriteToTraceLog(string.Format("Serial Port: {0}", ex.Message));
            }
        }

        private List<TestResult_Hitachi917> ParseTestResult_Hitachi917(string result)
        {
            //result = "217:n1    1    1  11DINH ALCAM      3 0319121056XN      9  3  62.2  10   4.8  13  75.7  16  18.2  17   4.1  18  25.8  19  18.0  34   2.9  36   5.2 A1\r218:n1    2    1  21LOC ACE         3 0319121058XN      6 10   6.1  16  27.2  17   5.2  18  18.6  19  17.8  21   1.3 34\r";

            while (result[0] == '\x02')
            {
                result = result.Substring(1, result.Length - 1);
            }

            result = '\x02' + result;

            string[] strArr = result.Split('\r');

            List<TestResult_Hitachi917> testResults = new List<TestResult_Hitachi917>();
            if (strArr != null && strArr.Length > 0)
            {
                strArr[0] = string.Format("{0}{1}", _lastResult, strArr[0]);
                _lastResult = strArr[strArr.Length - 1];

                for (int i = 0; i < strArr.Length - 1; i++)
                {
                    result = strArr[i];
                    int istart = result.IndexOf('\x02');
                    int iEnd = result.IndexOf('\x03');
                    if (istart != 0) continue;
                    if (istart < 0 || iEnd < 0) continue;

                    TestResult_Hitachi917 testResult = new TestResult_Hitachi917();
                    testResult.STX = result[0];
                    testResult.Receiver = result[1];
                    testResult.Sender = result[2];
                    testResult.PakageNum = result[3];
                    testResult.Frame = result[4];
                    testResult.FunctionCode = result[5];
                    testResult.SampleClass = result[6];
                    testResult.SampleNum = result.Substring(7, 5);
                    testResult.DiskNum = result.Substring(12, 5);
                    testResult.PositionNum = result.Substring(17, 3);
                    testResult.SampleCup = result.Substring(20, 1);
                    testResult.IDNum = result.Substring(21, 13);
                    testResult.Age = result.Substring(34, 3);
                    testResult.AgeUnit = result.Substring(37, 1);
                    testResult.Sex = result.Substring(38, 1);
                    testResult.CollectionDate = result.Substring(39, 6);
                    testResult.CollectionTime = result.Substring(45, 4);
                    testResult.OperatorID = result.Substring(49, 6);
                    int resultCount = Convert.ToInt32(result.Substring(55, 3));

                    for (int j = 0; j < resultCount; j++)
                    {
                        Result_Hitachi917 r = new Result_Hitachi917();
                        r.TestNum = Convert.ToInt32(result.Substring((58 + j * 10), 3));
                        r.Result = result.Substring(61 + j * 10, 6);
                        r.AlarmCode = result.Substring(67 + j * 10, 1);
                        testResult.Results.Add(r);
                    }

                    testResults.Add(testResult);
                }
            }

            return testResults;
        }
        #endregion
    }
}
