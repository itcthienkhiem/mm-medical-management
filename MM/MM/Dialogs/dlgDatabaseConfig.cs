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
    public partial class dlgDatabaseConfig : dlgBase
    {
        #region Memnbers

        #endregion

        #region Constructor
        public dlgDatabaseConfig()
        {
            InitializeComponent();
            cboAuthentication.SelectedIndex = 1;
        }
        #endregion

        #region Properties
        public bool IsChangeConnectionInfo
        {
            get
            {
                if (Global.ConnectionInfo.ServerName.ToLower() != cboServerName.Text.ToLower()) return true;
                if (Global.ConnectionInfo.DatabaseName.ToLower() != txtDatabaseName.Text.ToLower()) return true;
                if (Global.ConnectionInfo.Authentication.ToLower() != cboAuthentication.Text.ToLower()) return true;
                if (Global.ConnectionInfo.UserName.ToLower() != txtUserName.Text.ToLower()) return true;
                if (Global.ConnectionInfo.Password != txtPassword.Text) return true;
                return false;
            }
        }
        #endregion

        #region UI Command
        private void DisplaySQLInstancesAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(DisplaySQLInstancesProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void DisplaySQLInstances()
        {
            List<string> sqlInstances = Utility.GetSQLServerInstances();

            MethodInvoker method = delegate
            {
                foreach (string instance in sqlInstances)
                {
                    cboServerName.Items.Add(instance);
                }
            };

            if (InvokeRequired) BeginInvoke(method);
            else method.Invoke();
        }

        private bool CheckInfo()
        {
            bool result = true;

            if (cboServerName.Text == string.Empty)
            {
                result = false;
                MM.MsgBox.Show(this.Text, "Vui lòng nhập máy chủ.");
                cboServerName.Focus();
            }
            else if (txtDatabaseName.Text == string.Empty)
            {
                result = false;
                MM.MsgBox.Show(this.Text, "Vui lòng nhập CSDL.");
                txtDatabaseName.Focus();
            }
            else if (cboAuthentication.SelectedIndex == 1)
            {
                if (txtUserName.Text == string.Empty)
                {
                    result = false;
                    MM.MsgBox.Show(this.Text, "Vui lòng nhập tên đăng nhập.");
                    txtUserName.Focus();
                }
                else if (txtPassword.Text == string.Empty)
                {
                    result = false;
                    MM.MsgBox.Show(this.Text, "Vui lòng nhập mật khẩu.");
                    txtPassword.Focus();
                }
            }

            return result;
        }

        private bool TestConnection()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                ConnectionInfo connectionInfo = new ConnectionInfo();
                connectionInfo.ServerName = cboServerName.Text;
                connectionInfo.DatabaseName = txtDatabaseName.Text;
                connectionInfo.Authentication = cboAuthentication.Text;
                connectionInfo.UserName = txtUserName.Text;
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
            Global.ConnectionInfo.ServerName = cboServerName.Text;
            Global.ConnectionInfo.DatabaseName = txtDatabaseName.Text;
            Global.ConnectionInfo.Authentication = cboAuthentication.Text;
            Global.ConnectionInfo.UserName = txtUserName.Text;
            Global.ConnectionInfo.Password = txtPassword.Text;
        }
        #endregion

        #region Window Event Handlers
        private void cboAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAuthentication.SelectedIndex == 0)
            {
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                //txtServerName.Text = ("(local)");
            }
            else
            {
                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
            }		
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            if (!CheckInfo()) return;

            if (TestConnection())
                MM.MsgBox.Show(this.Text, "Thông tin kết nối thành công.");
            else
                MM.MsgBox.Show(this.Text, "Thông tin kết nối thất bại.");
        }

        private void dlgDatabaseConfig_Load(object sender, EventArgs e)
        {
            DisplaySQLInstancesAsThread();

            //Connnection Info
            cboServerName.Text = Global.ConnectionInfo.ServerName;
            txtDatabaseName.Text = Global.ConnectionInfo.DatabaseName;
            txtUserName.Text = Global.ConnectionInfo.UserName;
            txtPassword.Text = Global.ConnectionInfo.Password;
            cboAuthentication.SelectedIndex = 1;
            if (Global.ConnectionInfo.Authentication == "Windows Authentication")
                cboAuthentication.SelectedIndex = 0;
        }

        private void dlgDatabaseConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (CheckInfo())
                {
                    if (!TestConnection())
                    {
                        MM.MsgBox.Show(this.Text, "Thông tin kết nối thất bại.");
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

        #region Working Thread
        private void DisplaySQLInstancesProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                DisplaySQLInstances();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion
    }
}
