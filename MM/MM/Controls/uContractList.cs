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
        private DataTable _dataSource = null;

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
            //btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
            btnKhoa.Enabled = AllowLock;
            btnMoKhoa.Enabled = AllowLock;
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
                    _dataSource = result.QueryResult as DataTable;
                    OnSearchHopDong();
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
            if (_dataSource == null) return;
            dlgAddContract dlg = new dlgAddContract();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = _dataSource;
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

                newRow["Lock"] = false;
                dt.Rows.Add(newRow);

                //SelectLastedRow();
                OnSearchHopDong();
            }
        }

        private void SelectLastedRow()
        {
            dgContract.CurrentCell = dgContract[1, dgContract.RowCount - 1];
            dgContract.Rows[dgContract.RowCount - 1].Selected = true;
        }

        private DataRow GetDataRow(string hopDongGUID)
        {
            if (_dataSource == null || _dataSource.Rows.Count <= 0) return null;
            DataRow[] rows = _dataSource.Select(string.Format("CompanyContractGUID = '{0}'", hopDongGUID));
            if (rows == null || rows.Length <= 0) return null;

            return rows[0];
        }

        private void OnEditContract()
        {
            if (_dataSource == null) return;

            if (dgContract.SelectedRows == null || dgContract.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 hợp đồng.", IconType.Information);
                return;
            }

            string hopDongGUID = (dgContract.SelectedRows[0].DataBoundItem as DataRowView).Row["CompanyContractGUID"].ToString();
            DataRow drCon = GetDataRow(hopDongGUID);
            if (drCon == null) return;

            dlgAddContract dlg = new dlgAddContract(drCon, AllowEdit);
            dlg.OnOpenPatient += new OpenPatientHandler(dlg_OnOpenPatient);
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

                OnSearchHopDong();
            }
        }

        private void UpdateChecked()
        {
            DataTable dt = dgContract.DataSource as DataTable;
            if (dt == null) return;

            DataRow[] rows1 = dt.Select("Checked='True'");
            if (rows1 == null || rows1.Length <= 0) return;

            foreach (DataRow row1 in rows1)
            {
                string patientGUID1 = row1["CompanyContractGUID"].ToString();
                DataRow[] rows2 = _dataSource.Select(string.Format("CompanyContractGUID='{0}'", patientGUID1));
                if (rows2 == null || rows2.Length <= 0) continue;

                rows2[0]["Checked"] = row1["Checked"];
            }
        }

        private void OnDeleteContract()
        {
            if (_dataSource == null) return;
            UpdateChecked();
            List<string> deletedConList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = _dataSource;
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
                            _dataSource.Rows.Remove(row);
                        }

                        OnSearchHopDong();
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

        private void OnSearchHopDong()
        {
            UpdateChecked();
            chkChecked.Checked = false;
            List<DataRow> results = null;
            DataTable newDataSource = null;

            if (txtHopDong.Text.Trim() == string.Empty)
            {
                results = (from p in _dataSource.AsEnumerable()
                           orderby p.Field<DateTime>("BeginDate") descending
                           select p).ToList<DataRow>();

                newDataSource = _dataSource.Clone();
                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgContract.DataSource = newDataSource;
                if (dgContract.RowCount > 0) dgContract.Rows[0].Selected = true;
                return;
            }

            string str = txtHopDong.Text.ToLower();

            newDataSource = _dataSource.Clone();

            if (chkMaHopDong.Checked)
            {
                //Ma hop dong
                results = (from p in _dataSource.AsEnumerable()
                           where p.Field<string>("ContractCode") != null &&
                             p.Field<string>("ContractCode").Trim() != string.Empty &&
                             p.Field<string>("ContractCode").ToLower().IndexOf(str) >= 0
                           orderby p.Field<DateTime>("BeginDate") descending
                           select p).ToList<DataRow>();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                if (newDataSource.Rows.Count > 0)
                {
                    dgContract.DataSource = newDataSource;
                    return;
                }
            }
            else
            {
                //Ten hop dong
                results = (from p in _dataSource.AsEnumerable()
                           where p.Field<string>("ContractName").ToLower().IndexOf(str) >= 0 &&
                           p.Field<string>("ContractName") != null &&
                           p.Field<string>("ContractName").Trim() != string.Empty
                           orderby p.Field<DateTime>("BeginDate") descending
                           select p).ToList<DataRow>();


                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                if (newDataSource.Rows.Count > 0)
                {
                    dgContract.DataSource = newDataSource;
                    return;
                }
            }

            dgContract.DataSource = newDataSource;
        }
        #endregion

        #region Window Event Handlers
        private void dlg_OnOpenPatient(object patientRow)
        {
            base.RaiseOpentPatient(patientRow);
        }

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
            //if (!AllowEdit) return;
            OnEditContract();
        }

        private void txtHopDong_TextChanged(object sender, EventArgs e)
        {
            OnSearchHopDong();
        }

        private void chkMaHopDong_CheckedChanged(object sender, EventArgs e)
        {
            OnSearchHopDong();
        }

        private void txtHopDong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgContract.Focus();

                if (dgContract.SelectedRows != null && dgContract.SelectedRows.Count > 0)
                {
                    int index = dgContract.SelectedRows[0].Index;
                    if (index < dgContract.RowCount - 1)
                    {
                        index++;
                        dgContract.CurrentCell = dgContract[1, index];
                        dgContract.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgContract.Focus();

                if (dgContract.SelectedRows != null && dgContract.SelectedRows.Count > 0)
                {
                    int index = dgContract.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgContract.CurrentCell = dgContract[1, index];
                        dgContract.Rows[index].Selected = true;
                    }
                }
            }
        }

        private void btnKhoa_Click(object sender, EventArgs e)
        {
            if (_dataSource == null) return;
            UpdateChecked();
            List<string> deletedConList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = _dataSource;
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
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn khóa những hợp đồng mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = CompanyContractBus.LockHopDong(deletedConList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            row["Lock"] = 1;
                        }

                        OnSearchHopDong();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyContractBus.LockHopDong"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.LockHopDong"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những hợp đồng cần khóa.", IconType.Information);
        }

        private void btnMoKhoa_Click(object sender, EventArgs e)
        {
            if (_dataSource == null) return;
            UpdateChecked();
            List<string> deletedConList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = _dataSource;
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
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn mở khóa những hợp đồng mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = CompanyContractBus.UnlockHopDong(deletedConList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            row["Lock"] = 0;
                        }

                        OnSearchHopDong();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyContractBus.UnlockHopDong"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.UnlockHopDong"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những hợp đồng cần mở khóa.", IconType.Information);
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
