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
using MM.Databasae;
using MM.Bussiness;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uContractList : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uContractList()
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

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayContractListProc));
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

        public void ClearData()
        {
            dgContract.DataSource = null;
        }

        private void OnDisplayContractList()
        {
            Result result = CompanyContractBus.GetContractList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgContract.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyContractBus.GetContractList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetContractList"));
            }
        }

        private void OnAddContract()
        {
            dlgAddContract dlg = new dlgAddContract();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = dgContract.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["CompanyContractGUID"] = dlg.Contract.CompanyContractGUID.ToString();
                newRow["CompanyGUID"] = dlg.Contract.CompanyGUID.ToString();
                newRow["TenCty"] = dlg.ComName;
                newRow["ContractCode"] = dlg.Contract.ContractCode;
                newRow["ContractName"] = dlg.Contract.ContractName;
                newRow["BeginDate"] = dlg.Contract.BeginDate;
                if (dlg.Contract.EndDate != null)
                    newRow["EndDate"] = dlg.Contract.EndDate;
                else
                    newRow["EndDate"] = DBNull.Value;

                newRow["Completed"] = dlg.Contract.Completed;

                if (dlg.Contract.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.Contract.CreatedDate;

                if (dlg.Contract.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.Contract.CreatedBy.ToString();

                if (dlg.Contract.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.Contract.UpdatedDate;

                if (dlg.Contract.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.Contract.UpdatedBy.ToString();

                if (dlg.Contract.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.Contract.DeletedDate;

                if (dlg.Contract.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.Contract.DeletedBy.ToString();

                newRow["ContractStatus"] = dlg.Contract.Status;
                dt.Rows.Add(newRow);

                SelectLastedRow();
            }
        }

        private void SelectLastedRow()
        {
            dgContract.CurrentCell = dgContract[1, dgContract.RowCount - 1];
            dgContract.Rows[dgContract.RowCount - 1].Selected = true;
        }

        private void OnEditContract()
        {
            if (dgContract.SelectedRows == null || dgContract.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 hợp đồng.", IconType.Information);
                return;
            }

            DataRow drCon = (dgContract.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddContract dlg = new dlgAddContract(drCon);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drCon["CompanyGUID"] = dlg.Contract.CompanyGUID.ToString();
                drCon["TenCty"] = dlg.ComName;
                drCon["ContractCode"] = dlg.Contract.ContractCode;
                drCon["ContractName"] = dlg.Contract.ContractName;
                drCon["BeginDate"] = dlg.Contract.BeginDate;

                if (dlg.Contract.EndDate != null)
                    drCon["EndDate"] = dlg.Contract.EndDate;
                else
                    drCon["EndDate"] = DBNull.Value;

                drCon["Completed"] = dlg.Contract.Completed;

                if (dlg.Contract.CreatedDate.HasValue)
                    drCon["CreatedDate"] = dlg.Contract.CreatedDate;

                if (dlg.Contract.CreatedBy.HasValue)
                    drCon["CreatedBy"] = dlg.Contract.CreatedBy.ToString();

                if (dlg.Contract.UpdatedDate.HasValue)
                    drCon["UpdatedDate"] = dlg.Contract.UpdatedDate;

                if (dlg.Contract.UpdatedBy.HasValue)
                    drCon["UpdatedBy"] = dlg.Contract.UpdatedBy.ToString();

                if (dlg.Contract.DeletedDate.HasValue)
                    drCon["DeletedDate"] = dlg.Contract.DeletedDate;

                if (dlg.Contract.DeletedBy.HasValue)
                    drCon["DeletedBy"] = dlg.Contract.DeletedBy.ToString();

                drCon["ContractStatus"] = dlg.Contract.Status;
            }
        }

        private void OnDeleteContract()
        {
            List<string> deletedConList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgContract.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedConList.Add(row["CompanyContractGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedConList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những hợp đồng mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = CompanyContractBus.DeleteContract(deletedConList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyContractBus.DeleteContract"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.DeleteContract"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những hợp đồng cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddContract();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditContract();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteContract();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgContract.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void dgContract_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEditContract();
        }
        #endregion

        #region Working Thread
        private void OnDisplayContractListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayContractList();
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
