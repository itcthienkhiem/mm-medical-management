using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;

namespace MM.Dialogs
{
    public partial class dlgFTPConfig : dlgBase
    {
        #region Memnbers

        #endregion

        #region Constructor
        public dlgFTPConfig()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool IsChangeConnectionInfo
        {
            get
            {
                if (Global.FTPConnectionInfo.ServerName.ToLower() != txtServerName.Text.ToLower()) return true;
                if (Global.FTPConnectionInfo.Username.ToLower() != txtUserName.Text.ToLower()) return true;
                if (Global.FTPConnectionInfo.Password != txtPassword.Text) return true;
                return false;
            }
        }
        #endregion

        #region UI Command
        private bool CheckInfo()
        {
            bool result = true;

            if (txtServerName.Text == string.Empty)
            {
                result = false;
                MM.MsgBox.Show(this.Text, "Vui lòng nhập máy chủ.", IconType.Information);
                txtServerName.Focus();
            }
            else if (txtUserName.Text == string.Empty)
            {
                result = false;
                MM.MsgBox.Show(this.Text, "Vui lòng nhập tên đăng nhập.", IconType.Information);
                txtUserName.Focus();
            }
            else if (txtPassword.Text == string.Empty)
            {
                result = false;
                MM.MsgBox.Show(this.Text, "Vui lòng nhập mật khẩu.", IconType.Information);
                txtPassword.Focus();
            }

            return result;
        }

        private bool TestConnection()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                FTPConnectionInfo connectionInfo = new FTPConnectionInfo();
                connectionInfo.ServerName = txtServerName.Text;
                connectionInfo.Username = txtUserName.Text;
                connectionInfo.Password = txtPassword.Text;

                return connectionInfo.TestConnection();
            }
            catch
            {
                return false;
            }
        }

        public void SetAppConfig()
        {
            //Connection Info
            Global.FTPConnectionInfo.ServerName = txtServerName.Text;
            Global.FTPConnectionInfo.Username = txtUserName.Text;
            Global.FTPConnectionInfo.Password = txtPassword.Text;
        }
        #endregion

        #region Window Event Handlers
        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            if (!CheckInfo()) return;

            if (TestConnection())
                MM.MsgBox.Show(this.Text, "Thông tin kết nối thành công.", IconType.Information);
            else
                MM.MsgBox.Show(this.Text, "Thông tin kết nối thất bại.", IconType.Information);
        }

        private void dlgDatabaseConfig_Load(object sender, EventArgs e)
        {
            //Connnection Info
            txtServerName.Text = Global.FTPConnectionInfo.ServerName;
            txtUserName.Text = Global.FTPConnectionInfo.Username;
            txtPassword.Text = Global.FTPConnectionInfo.Password;
        }

        private void dlgDatabaseConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (CheckInfo())
                {
                    if (!TestConnection())
                    {
                        MM.MsgBox.Show(this.Text, "Thông tin kết nối thất bại.", IconType.Information);
                        e.Cancel = true;
                    }
                }
                else
                    e.Cancel = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
