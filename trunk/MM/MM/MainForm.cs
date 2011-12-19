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

                _uDocStaffList.AllowAdd = true;
                _uDocStaffList.AllowEdit = true;
                _uDocStaffList.AllowDelete = true;
                _uDocStaffList.AllowPrint = true;
                _uDocStaffList.AllowExport = true;
                _uDocStaffList.AllowImport = true;

                _uPatientList.AllowAdd = true;
                _uPatientList.AllowEdit = true;
                _uPatientList.AllowDelete = true;
                _uPatientList.AllowPrint = true;
                _uPatientList.AllowExport = true;
                _uPatientList.AllowImport = true;

                _uSpecialityList.AllowAdd = true;
                _uSpecialityList.AllowEdit = true;
                _uSpecialityList.AllowDelete = true;
                _uSpecialityList.AllowPrint = true;
                _uSpecialityList.AllowExport = true;
                _uSpecialityList.AllowImport = true;

                _uCompanyList.AllowAdd = true;
                _uCompanyList.AllowEdit = true;
                _uCompanyList.AllowDelete = true;
                _uCompanyList.AllowPrint = true;
                _uCompanyList.AllowExport = true;
                _uCompanyList.AllowImport = true;

                _uServicesList.AllowAdd = true;
                _uServicesList.AllowEdit = true;
                _uServicesList.AllowDelete = true;
                _uServicesList.AllowPrint = true;
                _uServicesList.AllowExport = true;
                _uServicesList.AllowImport = true;
                _uServicesList.AllowShowServicePrice = true;

                _uContractList.AllowAdd = true;
                _uContractList.AllowEdit = true;
                _uContractList.AllowDelete = true;
                _uContractList.AllowPrint = true;
                _uContractList.AllowExport = true;
                _uContractList.AllowImport = true;

                _uPatientList.AllowOpenPatient = true;

                _uPermission.AllowAdd = true;
                _uPermission.AllowEdit = true;
                _uPermission.AllowDelete = true;
                _uPermission.AllowPrint = true;
                _uPermission.AllowExport = true;
                _uPermission.AllowImport = true;

                _uSymptomList.AllowAdd = true;
                _uSymptomList.AllowEdit = true;
                _uSymptomList.AllowDelete = true;
                _uSymptomList.AllowPrint = true;
                _uSymptomList.AllowExport = true;
                _uSymptomList.AllowImport = true;

                _uInvoiceList.AllowAdd = true;
                _uInvoiceList.AllowEdit = true;
                _uInvoiceList.AllowDelete = true;
                _uInvoiceList.AllowPrint = true;
                _uInvoiceList.AllowExport = true;
                _uInvoiceList.AllowImport = true;

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
            }
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

        private void MainForm_Load(object sender, EventArgs e)
        {            
            InitConfigAsThread();
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

        #region Working Thread
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
