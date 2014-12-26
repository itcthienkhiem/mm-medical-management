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
    public partial class dlgUserList : dlgBase
    {
        #region Members
        private DataTable _dtBranch = null;
        #endregion

        #region Constructor
        public dlgUserList()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void OnAdd()
        {
            dlgAddUser dlg = new dlgAddUser(_dtBranch);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                DataTable dt = dgUser.DataSource as DataTable;
                DataRow row = dt.NewRow();
                row["LogonKey"] = dlg.LogonKey;
                row["Username"] = dlg.Username;
                row["Password"] = dlg.Password;
                row["BranchKey"] = dlg.BranchKey;
                row["Note"] = dlg.Notes;
                dt.Rows.Add(row);
                dgUser.Rows[dgUser.RowCount - 1].Selected = true;
            }
        }

        private void OnEdit()
        {
            if (dgUser.SelectedRows == null || dgUser.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Please select one user.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataRow dataRow = (dgUser.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddUser dlg = new dlgAddUser(dataRow, _dtBranch);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                dataRow["Username"] = dlg.Username;
                dataRow["Password"] = dlg.Password;
                dataRow["BranchKey"] = dlg.BranchKey;
                dataRow["Note"] = dlg.Notes;
            }
        }

        private void OnDelete()
        {
            List<DataRow> checkedRows = GetCheckedRows();
            if (checkedRows.Count > 0)
            {
                if (MessageBox.Show("Do you want to delete selected users ?",
                    this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    DataTable dt = dgUser.DataSource as DataTable;
                    foreach (DataRow row in checkedRows)
                    {
                        int logonKey = Convert.ToInt32(row["LogonKey"]);
                        Result result = MySQL.DeleteUserLogon(logonKey);
                        if (result.IsOK)
                            dt.Rows.Remove(row);
                        else
                        {
                            MessageBox.Show(result.GetErrorAsString("MySQL.DeleteUserLogon"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
            else
                MessageBox.Show("Please check at least one user.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void DisplayAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayProc));
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

        private void OnDisplay()
        {
            Result result = MySQL.GetUserLogonList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgUser.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
                MessageBox.Show(result.GetErrorAsString("MySQL.GetUserLogonList"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void LoadBranchListAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnLoadBranchListProc));
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

        private void OnLoadBranchList()
        {
            Result result = MySQL.GetBranchList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    _dtBranch = result.QueryResult as DataTable;
                    DataRow row = _dtBranch.NewRow();
                    row["BranchKey"] = 0;
                    row["BranchName"] = "[None]";
                    _dtBranch.Rows.InsertAt(row, 0);

                    colBranchKey.DataSource = result.QueryResult;
                    colBranchKey.DisplayMember = "BranchName";
                    colBranchKey.ValueMember = "BranchKey";
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
                MessageBox.Show(result.GetErrorAsString("MySQL.GetBranchList"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private List<DataRow> GetCheckedRows()
        {
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgUser.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Check"].ToString()))
                {
                    checkedRows.Add(row);
                }
            }

            return checkedRows;
        }
        #endregion

        #region Window Event Handlers
        private void dlgUserList_Load(object sender, EventArgs e)
        {
            LoadBranchListAsThread();
            DisplayAsThread();
        }

        private void dgUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OnEdit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDelete();
        }

        private void chkCheck_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgUser.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Check"] = chkCheck.Checked;
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayProc(object state)
        {
            try
            {
                OnDisplay();
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

        private void OnLoadBranchListProc(object state)
        {
            try
            {
                OnLoadBranchList();
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

        private void dgUser_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
