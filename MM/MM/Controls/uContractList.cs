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
        private DataTable _dtTemp = null;
        private Dictionary<string, DataRow> _dictContract = new Dictionary<string,DataRow>();
        private string _name = string.Empty;
        private bool _isMaHopDong = false;
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
                _name = txtHopDong.Text;
                _isMaHopDong = chkMaHopDong.Checked;
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
            DataTable dt = dgContract.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgContract.DataSource = null;
            }
        }

        public override void SearchAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                _name = txtHopDong.Text;
                _isMaHopDong = chkMaHopDong.Checked;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplayContractList()
        {
            lock (ThisLock)
            {
                Result result = CompanyContractBus.GetContractList(_name, _isMaHopDong);
                if (result.IsOK)
                {
                    dgContract.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();

                        DataTable dt = result.QueryResult as DataTable;
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        UpdateChecked(dt);
                        dgContract.DataSource = dt;
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyContractBus.GetContractList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetContractList"));
                }
            }
        }

        private void UpdateChecked(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string key = row["CompanyContractGUID"].ToString();
                if (_dictContract.ContainsKey(key))
                    row["Checked"] = true;
            }
        }

        private void OnAddContract()
        {
            dlgAddContract dlg = new dlgAddContract();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                SearchAsThread();
            }
        }

        private void OnEditContract()
        {
            if (_dictContract == null) return;

            if (dgContract.SelectedRows == null || dgContract.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 hợp đồng.", IconType.Information);
                return;
            }

            DataRow drCon = (dgContract.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (drCon == null) return;

            dlgAddContract dlg = new dlgAddContract(drCon, AllowEdit);
            dlg.OnOpenPatientEvent += new OpenPatientHandler(dlg_OnOpenPatient);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SearchAsThread();
            }
        }

        private void OnDeleteContract()
        {
            if (_dictContract == null) return;

            List<string> deletedConList = new List<string>();
            List<DataRow> deletedRows = _dictContract.Values.ToList();

            foreach (DataRow row in deletedRows)
            {
                deletedConList.Add(row["CompanyContractGUID"].ToString());
            }

            if (deletedConList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những hợp đồng mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = CompanyContractBus.DeleteContract(deletedConList);
                    if (result.IsOK)
                    {
                        DataTable dt = dgContract.DataSource as DataTable;
                        if (dt == null || dt.Rows.Count <= 0) return;

                        foreach (string key in deletedConList)
                        {
                            DataRow[] rows = dt.Select(string.Format("CompanyContractGUID='{0}'", key));
                            if (rows == null || rows.Length <= 0) continue;
                            dt.Rows.Remove(rows[0]);
                        }

                        _dictContract.Clear();
                        _dtTemp.Rows.Clear();
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
        private void dgContract_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgContract.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgContract.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string contractGUID = row["CompanyContractGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            if (isChecked)
            {
                if (!_dictContract.ContainsKey(contractGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictContract.Add(contractGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictContract.ContainsKey(contractGUID))
                {
                    _dictContract.Remove(contractGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("CompanyContractGUID='{0}'", contractGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
                }
            }
        }

        private void dlg_OnOpenPatient(DataRow patientRow)
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
                string contractGUID = row["CompanyContractGUID"].ToString();

                if (chkChecked.Checked)
                {
                    if (!_dictContract.ContainsKey(contractGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictContract.Add(contractGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
                else
                {
                    if (_dictContract.ContainsKey(contractGUID))
                    {
                        _dictContract.Remove(contractGUID);

                        DataRow[] rows = _dtTemp.Select(string.Format("CompanyContractGUID='{0}'", contractGUID));
                        if (rows != null && rows.Length > 0)
                            _dtTemp.Rows.Remove(rows[0]);
                    }
                }
            }
        }

        private void dgContract_DoubleClick(object sender, EventArgs e)
        {
            OnEditContract();
        }

        private void txtHopDong_TextChanged(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void chkMaHopDong_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
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
            if (_dictContract == null) return;
            List<string> deletedConList = new List<string>();
            List<DataRow> deletedRows = _dictContract.Values.ToList();

            foreach (DataRow row in deletedRows)
            {
                deletedConList.Add(row["CompanyContractGUID"].ToString());
            }

            if (deletedConList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn khóa những hợp đồng mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = CompanyContractBus.LockHopDong(deletedConList);
                    if (result.IsOK)
                    {
                        DataTable dt = dgContract.DataSource as DataTable;
                        if (dt == null || dt.Rows.Count <= 0) return;

                        foreach (string key in deletedConList)
                        {
                            DataRow[] rows = dt.Select(string.Format("CompanyContractGUID='{0}'", key));
                            if (rows == null || rows.Length <= 0) continue;
                            rows[0]["Lock"] = 1;
                        }
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
            if (_dictContract == null) return;
            List<string> deletedConList = new List<string>();
            List<DataRow> deletedRows = _dictContract.Values.ToList();

            foreach (DataRow row in deletedRows)
            {
                deletedConList.Add(row["CompanyContractGUID"].ToString());
            }

            if (deletedConList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn mở khóa những hợp đồng mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = CompanyContractBus.UnlockHopDong(deletedConList);
                    if (result.IsOK)
                    {
                        DataTable dt = dgContract.DataSource as DataTable;
                        if (dt == null || dt.Rows.Count <= 0) return;

                        foreach (string key in deletedConList)
                        {
                            DataRow[] rows = dt.Select(string.Format("CompanyContractGUID='{0}'", key));
                            if (rows == null || rows.Length <= 0) continue;
                            rows[0]["Lock"] = 0;
                        }
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

        private void OnSearchProc(object state)
        {
            try
            {
                OnDisplayContractList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }
        #endregion

        
    }
}
