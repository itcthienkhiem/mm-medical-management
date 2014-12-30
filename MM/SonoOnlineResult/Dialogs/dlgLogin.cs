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

namespace SonoOnlineResult.Dialogs
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

        #region Properties
        public string Username
        {
            get { return cboUsername.Text; }
        }

        public string Password
        {
            get { return txtPassword.Text; }
        }

        public string BranchName
        {
            get
            {
                string branchName = string.Empty;
                int logonKey = Convert.ToInt32(cboUsername.SelectedValue);
                DataTable dt = cboUsername.DataSource as DataTable;
                DataRow[] rows = dt.Select(string.Format("LogonKey={0}", logonKey));
                if (rows != null && rows.Length > 0)
                {
                    if (rows[0]["BranchKey"] != null && rows[0]["BranchKey"] != DBNull.Value)
                    {
                        int branchKey = Convert.ToInt32(rows[0]["BranchKey"]);
                        Result result = MySQL.GetBranch(branchKey);
                        if (result.IsOK)
                        {
                            DataTable dtBranch = result.QueryResult as DataTable;
                            if (dtBranch != null && dtBranch.Rows.Count >= 0)
                                branchName = dtBranch.Rows[0]["BranchName"].ToString();
                        }
                        else
                            MessageBox.Show(result.GetErrorAsString("MySQL.GetBranch"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                return branchName;
            }
        }
        #endregion

        #region UI Commnad
        public void LoadUserListAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnLoadUserListProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnLoadUserList()
        {
            Result result = MySQL.GetUserLogonWithBranchList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    cboUsername.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
                MessageBox.Show(result.GetErrorAsString("MySQL.GetAllUserLogonList"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool CheckInfo()
        {
            if (cboUsername.SelectedValue == null && cboUsername.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please input Username.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboUsername.Focus();
                return false;
            }

            if (txtPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please input Password.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Focus();
                return false;
            }

            Result result = MySQL.GetUserLogon(cboUsername.Text, txtPassword.Text);

            if (!result.IsOK)
            {
                if (result.Error.Code == ErrorCode.INVALID_USERNAME)
                {
                    MessageBox.Show("The Username incorrect.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboUsername.Focus();
                    return false;
                }

                if (result.Error.Code == ErrorCode.INVALID_PASSWORD)
                {
                    MessageBox.Show("The Password incorrect.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPassword.Focus();
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void dlgLogin_Load(object sender, EventArgs e)
        {
            LoadUserListAsThread();
        }

        private void dlgLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
            }
        }
        #endregion

        #region Working Thread
        private void OnLoadUserListProc(object state)
        {
            try
            {
                OnLoadUserList();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion

        
    }
}
