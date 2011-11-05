using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using MM.Common;
using MM.Controls;
using MM.Dialogs;

namespace MM
{
    public partial class MainForm : Form
    {
        #region Members
        private bool _flag = true;
        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();
            InitConfig();
        }   
        #endregion

        #region UI Command
        private void InitConfig()
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
            }
            else
            {
                dlgDatabaseConfig dlg = new dlgDatabaseConfig();
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    dlg.SetAppConfig();
                    SaveAppConfig();
                }
                else
                {
                    _flag = false;
                    this.Close();
                }
            }
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

        private void ExcuteCmd(string cmd)
        {
            Cursor.Current = Cursors.WaitCursor;
            switch (cmd)
            {
                case "Database Configuration":
                    OnDatabaseConfig();
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

        private void OnDoctorList()
        {
            
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
        }

        private void OnPatientList()
        {

        }

        private void OnOpenPatient()
        {

        }

        private void OnHelp()
        {

        }

        private void OnAbout()
        {

        }
        #endregion

        #region Window Event Handlers
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
    }
}
