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
    public partial class dlgAddUser : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private DataRow _dataRow = null;
        private int _logonKey = 0;
        private int _branchKey = 0;
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _notes = string.Empty;
        private DataTable _dtBranch = null;
        #endregion

        #region Constructor
        public dlgAddUser(DataTable dtBranch)
        {
            InitializeComponent();
            _dtBranch = dtBranch;
        }

        public dlgAddUser(DataRow dataRow, DataTable dtBranch) : this(dtBranch)
        {
            _isNew = false;
            this.Text = "Edit User";
            _dataRow = dataRow;
        }
        #endregion

        #region Properties
        public int LogonKey
        {
            get { return _logonKey; }
        }

        public int BranchKey
        {
            get { return _branchKey; }
        }

        public string Username
        {
            get { return _username; }
        }

        public string Password
        {
            get { return _password; }
        }

        public string Notes
        {
            get { return _notes; }
        }
        #endregion

        #region UI Command
        //public void LoadBranchListAsThread()
        //{
        //    try
        //    {
        //        ThreadPool.QueueUserWorkItem(new WaitCallback(OnLoadBranchListProc));
        //        base.ShowWaiting();
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        base.HideWaiting();
        //    }
        //}

        //private void OnLoadBranchList()
        //{
        //    Result result = MySQL.GetBranchList();
        //    if (result.IsOK)
        //    {
        //        MethodInvoker method = delegate
        //        {
        //            DataTable dt = result.QueryResult as DataTable;
        //            DataRow row = dt.NewRow();
        //            row["BranchKey"] = 0;
        //            row["BranchName"] = "[None]";
        //            dt.Rows.InsertAt(row, 0);
        //            cboBranch.DataSource = dt;
        //        };

        //        if (InvokeRequired) BeginInvoke(method);
        //        else method.Invoke();
        //    }
        //    else
        //        MessageBox.Show(result.GetErrorAsString("MySQL.GetBranchList"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //}

        private void DisplayInfo()
        {
            txtUsername.Text = _dataRow["Username"].ToString();
            string password = _dataRow["Password"].ToString();
            RijndaelCrypto cryto = new RijndaelCrypto();
            password = cryto.Decrypt(password);
            txtPassword.Text = password;
            txtNote.Text = _dataRow["Note"].ToString();
            cboBranch.SelectedValue = _dataRow["BranchKey"];
            _logonKey = Convert.ToInt32(_dataRow["LogonKey"]);
        }

        private bool CheckInfo()
        {
            if (txtUsername.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please input Username.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsername.Focus();
                return false;
            }

            if (txtPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please input Password.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Focus();
                return false;
            }

            Result result = MySQL.CheckUserLogonExist(txtUsername.Text, _logonKey);
            if (result.Error.Code != ErrorCode.EXIST && result.Error.Code != ErrorCode.NOT_EXIST)
            {
                MessageBox.Show(result.GetErrorAsString("MySQL.CheckUserLogonExist"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (result.Error.Code == ErrorCode.EXIST)
            {
                MessageBox.Show(string.Format("The Username: '{0}' is exists.", txtUsername.Text),
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (cboBranch.SelectedValue == null || Convert.ToInt32(cboBranch.SelectedValue) <= 0)
            {
                MessageBox.Show("Please select Branch.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboBranch.Focus();
                return false;
            }

            return true;
        }

        private void SaveInfoAsThread()
        {
            try
            {
                _username = txtUsername.Text;
                _password = txtPassword.Text;
                RijndaelCrypto cryto = new RijndaelCrypto();
                _password = cryto.Encrypt(_password);
                _branchKey = Convert.ToInt32(cboBranch.SelectedValue);
                _notes = txtNote.Text;

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSaveInfoProc));
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

        private void OnSaveInfo()
        {
            try
            {
                Result result = MySQL.InsertUserLogon(_logonKey, _username, _password, _branchKey, _notes);
                if (result.IsOK)
                {
                    if (_isNew)
                        _logonKey = Convert.ToInt32(result.QueryResult);
                }
                else
                {
                    MessageBox.Show(result.GetErrorAsString("MySQL.InsertUserLogon"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnAddBranch()
        {
            dlgAddBranch dlg = new dlgAddBranch();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                DataTable dt = cboBranch.DataSource as DataTable;
                DataRow row = dt.NewRow();
                row["BranchKey"] = dlg.BranchKey;
                row["BranchName"] = dlg.BranchName;
                row["Address"] = dlg.Address;
                row["Telephone"] = dlg.Telephone;
                row["Fax"] = dlg.Fax;
                row["Website"] = dlg.Website;
                row["Note"] = dlg.Notes;
                dt.Rows.Add(row);

                cboBranch.SelectedValue = dlg.BranchKey;
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddUser_Load(object sender, EventArgs e)
        {
            cboBranch.DataSource = _dtBranch;
            if (!_isNew) DisplayInfo();
        }

        private void dlgAddUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
                else SaveInfoAsThread();
            }
        }

        private void btnAddBranch_Click(object sender, EventArgs e)
        {
            OnAddBranch();
        }
        #endregion

        #region Working Thread
        //private void OnLoadBranchListProc(object state)
        //{
        //    try
        //    {
        //        OnLoadBranchList();
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        base.HideWaiting();
        //    }
        //}

        private void OnSaveInfoProc(object state)
        {
            try
            {
                OnSaveInfo();
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
