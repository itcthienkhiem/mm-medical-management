using System;
using System.Collections.Generic;
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
            servicesToolStripMenuItem.Enabled = isLogin;
            tbServiceList.Enabled = isLogin;
            doctorToolStripMenuItem.Enabled = isLogin;
            tbDoctorList.Enabled = isLogin;
            patientToolStripMenuItem.Enabled = isLogin;
            tbPatientList.Enabled = isLogin;
            tbOpenPatient.Enabled = isLogin;
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

                case "Doctor List":
                    OnDoctorList();
                    break;

                case "Help":
                    OnHelp();
                    break;

                case "About":
                    OnAbout();
                    break;
            }
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
        }

        private void OnDoctorList()
        {
            this.Text = string.Format("{0} - Danh muc bac si", Application.ProductName);
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
                MsgBox.Show(Application.ProductName, e.Message);
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
                Thread.Sleep(1000);
                OnInitConfig();
            }
            catch (Exception e)
            {
                MsgBox.Show(Application.ProductName, e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion
    }
}
