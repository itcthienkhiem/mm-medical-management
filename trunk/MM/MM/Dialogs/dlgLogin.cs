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
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgLogin : dlgBase
    {
        #region Members

        #endregion

        #region Constructor
        public dlgLogin()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void DisplayUserListAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayUserListProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplayUserList()
        {
            Result result = LogonBus.GetUserList();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                MethodInvoker method = delegate
                {
                    cboUserName.DataSource = dt;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("LogonBus.GetUserList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("LogonBus.GetUserList"));
            }
        }


        #endregion

        #region Window Event Handlers
        private void dlgLogin_Load(object sender, EventArgs e)
        {
            DisplayUserListAsThread();
        }

        private void dlgLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                DataTable dt = cboUserName.DataSource as DataTable;
                DataRow[] rows = dt.Select(string.Format("DocStaffGUID='{0}'", cboUserName.SelectedValue.ToString()));
                if (rows != null && rows.Length > 0)
                {
                    RijndaelCrypto crypt = new RijndaelCrypto();
                    string password = crypt.Decrypt(rows[0]["Password"].ToString());

                    if (password == txtPassword.Text)
                    {
                        Global.UserGUID = cboUserName.SelectedValue.ToString();
                        Global.Password = password;
                        Global.Fullname = cboUserName.Text;
                        Global.StaffType = (StaffType)Convert.ToInt32(rows[0]["StaffType"]);
                        Global.LogonGUID = rows[0]["LogonGUID"].ToString();
                    }
                    else
                    {
                        MsgBox.Show(this.Text, "Mật khẩu không chính xác. Vui lòng nhập lại.", IconType.Information);
                        txtPassword.Focus();
                        e.Cancel = true;
                    }
                }
                else
                    e.Cancel = true;
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayUserListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayUserList();
            }
            catch (Exception e)
            {
                MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion
    }
}
