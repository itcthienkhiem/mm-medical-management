using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uUserGroupList : uBase
    {
        #region Constructor
        public uUserGroupList()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;

            addToolStripMenuItem.Enabled = AllowAdd;
            editToolStripMenuItem.Enabled = AllowEdit;
            deleteToolStripMenuItem.Enabled = AllowDelete;
        }

        public void ClearData()
        {
            DataTable dt = dgUserGroup.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgUserGroup.DataSource = null;
            }
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayUserGroupListProc));
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

        private void OnDisplayUserGroupList()
        {
            Result result = UserGroupBus.GetUserGroupList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    dgUserGroup.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("UserGroupBus.GetUserGroupList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("UserGroupBus.GetUserGroupList"));
            }
        }

        private void OnAdd()
        {
            dlgAddUserGroup dlg = new dlgAddUserGroup();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = dgUserGroup.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["UserGroupGUID"] = dlg.UserGroup.UserGroupGUID.ToString();
                newRow["GroupName"] = dlg.UserGroup.GroupName;

                if (dlg.UserGroup.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.UserGroup.CreatedDate;

                if (dlg.UserGroup.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.UserGroup.CreatedBy.ToString();

                if (dlg.UserGroup.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.UserGroup.UpdatedDate;

                if (dlg.UserGroup.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.UserGroup.UpdatedBy.ToString();

                if (dlg.UserGroup.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.UserGroup.DeletedDate;

                if (dlg.UserGroup.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.UserGroup.DeletedBy.ToString();

                newRow["Status"] = dlg.UserGroup.Status;
                dt.Rows.Add(newRow);
            }
        }

        private void OnEdit()
        {
            if (dgUserGroup.SelectedRows == null || dgUserGroup.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 nhóm người sử dụng.", IconType.Information);
                return;
            }

            DataRow drUserGroup = (dgUserGroup.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddUserGroup dlg = new dlgAddUserGroup(drUserGroup);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drUserGroup["UserGroupGUID"] = dlg.UserGroup.UserGroupGUID.ToString();
                drUserGroup["GroupName"] = dlg.UserGroup.GroupName;

                if (dlg.UserGroup.CreatedDate.HasValue)
                    drUserGroup["CreatedDate"] = dlg.UserGroup.CreatedDate;

                if (dlg.UserGroup.CreatedBy.HasValue)
                    drUserGroup["CreatedBy"] = dlg.UserGroup.CreatedBy.ToString();

                if (dlg.UserGroup.UpdatedDate.HasValue)
                    drUserGroup["UpdatedDate"] = dlg.UserGroup.UpdatedDate;

                if (dlg.UserGroup.UpdatedBy.HasValue)
                    drUserGroup["UpdatedBy"] = dlg.UserGroup.UpdatedBy.ToString();

                if (dlg.UserGroup.DeletedDate.HasValue)
                    drUserGroup["DeletedDate"] = dlg.UserGroup.DeletedDate;

                if (dlg.UserGroup.DeletedBy.HasValue)
                    drUserGroup["DeletedBy"] = dlg.UserGroup.DeletedBy.ToString();

                drUserGroup["Status"] = dlg.UserGroup.Status;
            }
        }

        private void OnDelete()
        {
            List<string> deletedLogonList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgUserGroup.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedLogonList.Add(row["UserGroupGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedLogonList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những nhóm người sử dụng mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = UserGroupBus.DeleteUserGroup(deletedLogonList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("UserGroupBus.DeleteUserGroup"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("UserGroupBus.DeleteUserGroup"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những nhóm người sử dụng cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgUserGroup.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
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

        private void dgLogon_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEdit();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnEdit(); 
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDelete();
        }
        #endregion

        #region Working Thread
        private void OnDisplayUserGroupListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayUserGroupList();
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
        #endregion

        
    }
}
