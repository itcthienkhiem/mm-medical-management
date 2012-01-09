using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Bussiness;
using MM.Common;
using MM.Databasae;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uServiceGroupList : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uServiceGroupList()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;

        }

        public void ClearData()
        {
            dgServiceGroup.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayServiceGroupListProc));
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

        private void OnDisplayServiceGroupList()
        {
            Result result = ServiceGroupBus.GetServicesGroupList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgServiceGroup.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServiceGroupBus.GetServicesGroupList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ServiceGroupBus.GetServicesGroupList"));
            }
        }

        private void OnAddServiceGroup()
        {
            dlgAddServiceGroup dlg = new dlgAddServiceGroup();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = dgServiceGroup.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["ServiceGroupGUID"] = dlg.ServiceGroup.ServiceGroupGUID.ToString();
                newRow["Code"] = dlg.ServiceGroup.Code;
                newRow["Name"] = dlg.ServiceGroup.Name;
                newRow["Note"] = dlg.ServiceGroup.Note;
                newRow["EnglishName"] = dlg.ServiceGroup.EnglishName;

                if (dlg.ServiceGroup.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.ServiceGroup.CreatedDate;

                if (dlg.ServiceGroup.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.ServiceGroup.CreatedBy.ToString();

                if (dlg.ServiceGroup.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.ServiceGroup.UpdatedDate;

                if (dlg.ServiceGroup.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.ServiceGroup.UpdatedBy.ToString();

                if (dlg.ServiceGroup.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.ServiceGroup.DeletedDate;

                if (dlg.ServiceGroup.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.ServiceGroup.DeletedBy.ToString();

                newRow["Status"] = dlg.ServiceGroup.Status;
                dt.Rows.Add(newRow);
            }
        }

        private void OnEditServiceGroup()
        {
            if (dgServiceGroup.SelectedRows == null || dgServiceGroup.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 nhóm dịch vụ.", IconType.Information);
                return;
            }

            DataRow drServiceGroup = (dgServiceGroup.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddServiceGroup dlg = new dlgAddServiceGroup(drServiceGroup);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                drServiceGroup["Code"] = dlg.ServiceGroup.Code;
                drServiceGroup["Name"] = dlg.ServiceGroup.Name;
                drServiceGroup["Note"] = dlg.ServiceGroup.Note;
                drServiceGroup["EnglishName"] = dlg.ServiceGroup.EnglishName;

                if (dlg.ServiceGroup.CreatedDate.HasValue)
                    drServiceGroup["CreatedDate"] = dlg.ServiceGroup.CreatedDate;

                if (dlg.ServiceGroup.CreatedBy.HasValue)
                    drServiceGroup["CreatedBy"] = dlg.ServiceGroup.CreatedBy.ToString();

                if (dlg.ServiceGroup.UpdatedDate.HasValue)
                    drServiceGroup["UpdatedDate"] = dlg.ServiceGroup.UpdatedDate;

                if (dlg.ServiceGroup.UpdatedBy.HasValue)
                    drServiceGroup["UpdatedBy"] = dlg.ServiceGroup.UpdatedBy.ToString();

                if (dlg.ServiceGroup.DeletedDate.HasValue)
                    drServiceGroup["DeletedDate"] = dlg.ServiceGroup.DeletedDate;

                if (dlg.ServiceGroup.DeletedBy.HasValue)
                    drServiceGroup["DeletedBy"] = dlg.ServiceGroup.DeletedBy.ToString();

                drServiceGroup["Status"] = dlg.ServiceGroup.Status;
            }
        }

        private void OnDeleteServiceGroup()
        {
            List<string> deletedServiceGroupList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgServiceGroup.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedServiceGroupList.Add(row["ServiceGroupGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedServiceGroupList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những nhóm dịch vụ mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = ServiceGroupBus.DeleteServiceGroup(deletedServiceGroupList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServiceGroupBus.DeleteServiceGroup"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ServiceGroupBus.DeleteServiceGroup"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những nhóm dịch vụ cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgServiceGroup.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddServiceGroup();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditServiceGroup();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteServiceGroup();
        }

        private void dgServiceGroup_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEditServiceGroup();
        }
        #endregion

        #region Working Thread
        private void OnDisplayServiceGroupListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayServiceGroupList();
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
