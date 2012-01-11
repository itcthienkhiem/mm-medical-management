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

namespace MM
{
    public partial class MainForm : dlgBase
    {
        #region Members
        private bool _flag = true;
        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();
            _uPatientList.OnOpenPatient += new OpenPatientHandler(_uPatientList_OnOpenPatient);
            _uCompanyList.OnOpenPatient += new OpenPatientHandler(_uPatientList_OnOpenPatient);
            _uContractList.OnOpenPatient += new OpenPatientHandler(_uPatientList_OnOpenPatient);
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

                        if (functionCode == Const.DocStaff)
                        {
                            doctorListToolStripMenuItem.Enabled = isView && isLogin;
                            doctorToolStripMenuItem.Enabled = isLogin;
                            tbDoctorList.Enabled = isView && isLogin;

                            _uDocStaffList.AllowAdd = isAdd;
                            _uDocStaffList.AllowEdit = isEdit;
                            _uDocStaffList.AllowDelete = isDelete;
                            _uDocStaffList.AllowPrint = isPrint;
                            _uDocStaffList.AllowExport = isExport;
                            _uDocStaffList.AllowImport = isImport;
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
                        }
                        else if (functionCode == Const.Speciality)
                        {
                            specialityListToolStripMenuItem.Enabled = isView && isLogin;
                            specialityToolStripMenuItem.Enabled = isLogin;
                            tbSpecialityList.Enabled = isView && isLogin;
                            _uSpecialityList.AllowAdd = isAdd;
                            _uSpecialityList.AllowEdit = isEdit;
                            _uSpecialityList.AllowDelete = isDelete;
                            _uSpecialityList.AllowPrint = isPrint;
                            _uSpecialityList.AllowExport = isExport;
                            _uSpecialityList.AllowImport = isImport;
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
                        }
                        else if (functionCode == Const.Symptom)
                        {
                            symptomListToolStripMenuItem.Enabled = isView && isLogin;
                            symptomToolStripMenuItem.Enabled = isLogin;
                            tbSympton.Enabled = isView && isLogin;
                            _uSymptomList.AllowAdd = isAdd;
                            _uSymptomList.AllowEdit = isEdit;
                            _uSymptomList.AllowDelete = isDelete;
                            _uSymptomList.AllowPrint = isPrint;
                            _uSymptomList.AllowExport = isExport;
                            _uSymptomList.AllowImport = isImport;
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
                        }
                        else if (functionCode == Const.KeToa)
                        {
                            keToaToolStripMenuItem.Enabled = isView && isLogin;
                            tbKeToa.Enabled = isView && isLogin;
                            _uToaThuocList.AllowAdd = isAdd;
                            _uToaThuocList.AllowEdit = isEdit;
                            _uToaThuocList.AllowDelete = isDelete;
                            _uToaThuocList.AllowPrint = isPrint;
                            _uToaThuocList.AllowExport = isExport;
                            _uToaThuocList.AllowImport = isImport;
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
                            _uServiceGroupList.AllowAdd = isAdd;
                            _uServiceGroupList.AllowEdit = isEdit;
                            _uServiceGroupList.AllowDelete = isDelete;
                            _uServiceGroupList.AllowPrint = isPrint;
                            _uServiceGroupList.AllowExport = isExport;
                            _uServiceGroupList.AllowImport = isImport;
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

                foreach (Control ctrl in this._mainPanel.Controls)
                {   
                    (ctrl as uBase).AllowAdd = true;
                    (ctrl as uBase).AllowEdit = true;
                    (ctrl as uBase).AllowDelete = true;
                    (ctrl as uBase).AllowPrint = true;
                    (ctrl as uBase).AllowExport = true;
                    (ctrl as uBase).AllowImport = true;
                }

                servicesToolStripMenuItem.Enabled = isLogin;
                serviceListToolStripMenuItem.Enabled = isLogin;
                tbServiceList.Enabled = isLogin;

                doctorToolStripMenuItem.Enabled = isLogin;
                doctorListToolStripMenuItem.Enabled = isLogin;
                tbDoctorList.Enabled = isLogin;

                patientToolStripMenuItem.Enabled = isLogin;
                patientListToolStripMenuItem.Enabled = isLogin;
                openPatientToolStripMenuItem.Enabled = isLogin;
                tbPatientList.Enabled = isLogin;
                tbOpenPatient.Enabled = isLogin;

                specialityToolStripMenuItem.Enabled = isLogin;
                specialityListToolStripMenuItem.Enabled = isLogin;
                tbSpecialityList.Enabled = isLogin;

                symptomToolStripMenuItem.Enabled = isLogin;
                symptomListToolStripMenuItem.Enabled = isLogin;
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
            }
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
            this.Text = string.Format("{0} - Danh muc hoa don", Application.ProductName);
            ViewControl(_uInvoiceList);
            _uInvoiceList.DisplayAsThread();
        }

        private void OnReceiptList()
        {
            this.Text = string.Format("{0} - Danh muc phieu thu", Application.ProductName);
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
            this.Text = string.Format("{0} - Thong tin benh nhan", Application.ProductName);
            ViewControl(_uPatientHistory);
            _uPatientHistory.PatientRow = patientRow;
            _uPatientHistory.Display();

            if (_flag) AddPatientToList((DataRow)patientRow);
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
            //AutoDetectUpdateAsThread();
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
            string fileOnServer = string.Format("\\\\{0}\\MMupdate\\updatedDate.txt", strServerName);
            if (!File.Exists(fileOnServer))
                return;
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
                        RunUpdateFile(Path.Combine(string.Format("\\\\{0}\\MMupdate\\", strServerName), "MMSetup.exe"));
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
    }
}
