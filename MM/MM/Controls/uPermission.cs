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
    public partial class uPermission : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uPermission()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        public void ClearData()
        {
            dgLogon.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayUserLogonListProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplayUserLogonList()
        {
            Result result = LogonBus.GetUserListWithoutAdmin();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgLogon.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("LogonBus.GetUserListWithoutAdmin"));
                Utility.WriteToTraceLog(result.GetErrorAsString("LogonBus.GetUserListWithoutAdmin"));
            }
        }

        private void OnAddUserLogon()
        {
            dlgAddUserLogon dlg = new dlgAddUserLogon();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = dgLogon.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["LogonGUID"] = dlg.Logon.LogonGUID.ToString();
                newRow["DocStaffGUID"] = dlg.Logon.DocStaffGUID.ToString();
                newRow["Password"] = dlg.Logon.Password;
                newRow["StaffTypeStr"] = dlg.StaffTypeStr;
                newRow["FullName"] = dlg.FullName;

                if (dlg.Logon.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.Logon.CreatedDate;

                if (dlg.Logon.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.Logon.CreatedBy.ToString();

                if (dlg.Logon.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.Logon.UpdatedDate;

                if (dlg.Logon.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.Logon.UpdatedBy.ToString();

                if (dlg.Logon.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.Logon.DeletedDate;

                if (dlg.Logon.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.Logon.DeletedBy.ToString();

                newRow["Status"] = dlg.Logon.Status;
                dt.Rows.Add(newRow);
                SelectLastedRow();
            }
        }

        private void SelectLastedRow()
        {
            dgLogon.CurrentCell = dgLogon[1, dgLogon.RowCount - 1];
            dgLogon.Rows[dgLogon.RowCount - 1].Selected = true;
        }

        private void OnEditUserLogon()
        {
            if (dgLogon.SelectedRows == null || dgLogon.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 người sử dụng.");
                return;
            }

            DataRow drLogon = (dgLogon.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddUserLogon dlg = new dlgAddUserLogon(drLogon);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drLogon["DocStaffGUID"] = dlg.Logon.DocStaffGUID.ToString();
                drLogon["Password"] = dlg.Logon.Password;
                drLogon["StaffTypeStr"] = dlg.StaffTypeStr;
                drLogon["FullName"] = dlg.FullName;

                if (dlg.Logon.CreatedDate.HasValue)
                    drLogon["CreatedDate"] = dlg.Logon.CreatedDate;

                if (dlg.Logon.CreatedBy.HasValue)
                    drLogon["CreatedBy"] = dlg.Logon.CreatedBy.ToString();

                if (dlg.Logon.UpdatedDate.HasValue)
                    drLogon["UpdatedDate"] = dlg.Logon.UpdatedDate;

                if (dlg.Logon.UpdatedBy.HasValue)
                    drLogon["UpdatedBy"] = dlg.Logon.UpdatedBy.ToString();

                if (dlg.Logon.DeletedDate.HasValue)
                    drLogon["DeletedDate"] = dlg.Logon.DeletedDate;

                if (dlg.Logon.DeletedBy.HasValue)
                    drLogon["DeletedBy"] = dlg.Logon.DeletedBy.ToString();

                drLogon["Status"] = dlg.Logon.Status;
            }
        }

        private void OnDeleteUserLogon()
        {
            List<string> deletedLogonList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgLogon.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedLogonList.Add(row["LogonGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedLogonList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những người sử dụng mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = LogonBus.DeleteUserLogon(deletedLogonList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("LogonBus.DeleteUserLogon"));
                        Utility.WriteToTraceLog(result.GetErrorAsString("LogonBus.DeleteUserLogon"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những người sử dụng cần xóa.");
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgLogon.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddUserLogon();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditUserLogon();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteUserLogon();
        }

        private void dgLogon_DoubleClick(object sender, EventArgs e)
        {
            OnEditUserLogon();
        }
        #endregion

        #region Working Thread
        private void OnDisplayUserLogonListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayUserLogonList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message);
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
