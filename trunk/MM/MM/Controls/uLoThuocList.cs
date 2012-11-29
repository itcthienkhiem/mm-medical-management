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
    public partial class uLoThuocList : uBase
    {
        #region Members
        private DataTable _dtTemp = null;
        private int _currentRowIndex = 0;
        private Dictionary<string, DataRow> _dictLoThuoc = new Dictionary<string,DataRow>();
        private string _name = string.Empty;
        private bool _isTenThuoc = true;
        private DateTime _tuNgay = DateTime.Now;
        private DateTime _denNgay = DateTime.Now;
        private bool _flag = true;
        private Object _thisLock = new Object();
        #endregion

        #region Constructor
        public uLoThuocList()
        {
            InitializeComponent();

            _flag = false;
            dtpkTuNgay.Value = DateTime.Now;
            dtpkDenNgay.Value = DateTime.Now;
            _flag = true;
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        public void ClearData()
        {
            DataTable dt = dgLoThuoc.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgLoThuoc.DataSource = null;
            }
        }

        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnDelete.Enabled = AllowDelete;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                _name = txtTenThuoc.Text;
                _isTenThuoc = raTenThuoc.Checked;
                _tuNgay = dtpkTuNgay.Value;
                _denNgay = dtpkDenNgay.Value;

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayLoThuocListProc));
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

        private void SearchAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                _name = txtTenThuoc.Text;
                _isTenThuoc = raTenThuoc.Checked;
                _tuNgay = dtpkTuNgay.Value;
                _denNgay = dtpkDenNgay.Value;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplayLoThuocList()
        {
            lock (_thisLock)
            {
                Result result = LoThuocBus.GetLoThuocList(_name, _tuNgay, _denNgay, _isTenThuoc);
                if (result.IsOK)
                {
                    dgLoThuoc.Invoke(new MethodInvoker(delegate()
                    {
                        if (dgLoThuoc.CurrentRow != null)
                            _currentRowIndex = dgLoThuoc.CurrentRow.Index;

                        ClearData();

                        DataTable dt = result.QueryResult as DataTable;
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        UpdateChecked(dt);
                        dgLoThuoc.DataSource = dt;

                        if (_currentRowIndex < dt.Rows.Count)
                        {
                            dgLoThuoc.CurrentCell = dgLoThuoc[0, _currentRowIndex];
                            dgLoThuoc.Rows[_currentRowIndex].Selected = true;
                        }
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("LoThuocBus.GetLoThuocList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("LoThuocBus.GetLoThuocList"));
                }
            }
        }

        private void UpdateChecked(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string key = row["LoThuocGUID"].ToString();
                if (_dictLoThuoc.ContainsKey(key))
                    row["Checked"] = true;
            }
        }

        private void OnAddLoThuoc()
        {
            dlgAddLoThuoc dlg = new dlgAddLoThuoc();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                SearchAsThread();
            }
        }

        private void OnEditLoThuoc()
        {
            if (dgLoThuoc.SelectedRows == null || dgLoThuoc.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 lô thuốc.", IconType.Information);
                return;
            }

            DataRow drLoThuoc = (dgLoThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (drLoThuoc == null) return;
            dlgAddLoThuoc dlg = new dlgAddLoThuoc(drLoThuoc, AllowEdit);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                SearchAsThread();
            }
        }

        private void OnDeleteLoThuoc()
        {
            if (_dictLoThuoc == null) return;
            List<string> deletedLoThuocList = new List<string>();
            List<DataRow> deletedRows = _dictLoThuoc.Values.ToList();

            foreach (DataRow row in deletedRows)
            {
                deletedLoThuocList.Add(row["LoThuocGUID"].ToString());
            }

            if (deletedLoThuocList.Count > 0)
            {
                foreach (string key in deletedLoThuocList)
                {
                    Result rs = LoThuocBus.GetLoThuoc(key);
                    if (!rs.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, rs.GetErrorAsString("LoThuocBus.GetLoThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(rs.GetErrorAsString("LoThuocBus.GetLoThuoc"));
                        return;
                    }

                    LoThuoc lt = rs.QueryResult as LoThuoc;
                    if (lt.SoLuongXuat > 0)
                    {
                        MsgBox.Show(Application.ProductName, string.Format("Lô thuốc: '{0}' này đã xuất rồi không thể xóa.", lt.TenLoThuoc), 
                            IconType.Information);
                        return;
                    }
                }

                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những lô thuốc mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = LoThuocBus.DeleteLoThuoc(deletedLoThuocList);
                    if (result.IsOK)
                    {
                        DataTable dt = dgLoThuoc.DataSource as DataTable;
                        if (dt == null || dt.Rows.Count <= 0) return;

                        foreach (string key in deletedLoThuocList)
                        {
                            DataRow[] rows = dt.Select(string.Format("LoThuocGUID='{0}'", key));
                            if (rows == null || rows.Length <= 0) continue;
                            dt.Rows.Remove(rows[0]);
                        }

                        _dictLoThuoc.Clear();
                        _dtTemp.Rows.Clear();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("LoThuocBus.DeleteLoThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("LoThuocBus.DeleteLoThuoc"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những lô thuốc cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void dgLoThuoc_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgLoThuoc.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgLoThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string loThuocGUID = row["LoThuocGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            if (isChecked)
            {
                if (!_dictLoThuoc.ContainsKey(loThuocGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictLoThuoc.Add(loThuocGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictLoThuoc.ContainsKey(loThuocGUID))
                {
                    _dictLoThuoc.Remove(loThuocGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("LoThuocGUID='{0}'", loThuocGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
                }
            }
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgLoThuoc.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
                string loThuocGUID = row["LoThuocGUID"].ToString();
                if (chkChecked.Checked)
                {
                    if (!_dictLoThuoc.ContainsKey(loThuocGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictLoThuoc.Add(loThuocGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
                else
                {
                    if (_dictLoThuoc.ContainsKey(loThuocGUID))
                    {
                        _dictLoThuoc.Remove(loThuocGUID);

                        DataRow[] rows = _dtTemp.Select(string.Format("LoThuocGUID='{0}'", loThuocGUID));
                        if (rows != null && rows.Length > 0)
                            _dtTemp.Rows.Remove(rows[0]);
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddLoThuoc();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditLoThuoc();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteLoThuoc();
        }

        private void dgLoThuoc_DoubleClick(object sender, EventArgs e)
        {
            OnEditLoThuoc();
        }

        private void raTenThuoc_CheckedChanged(object sender, EventArgs e)
        {
            txtTenThuoc.ReadOnly = !raTenThuoc.Checked;
            dtpkTuNgay.Enabled = !raTenThuoc.Checked;
            dtpkDenNgay.Enabled = !raTenThuoc.Checked;

            SearchAsThread();
        }

        private void raTuNgayDenNgay_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void dtpkTuNgay_ValueChanged(object sender, EventArgs e)
        {
            if (!_flag) return;
            SearchAsThread();
        }

        private void dtpkDenNgay_ValueChanged(object sender, EventArgs e)
        {
            if (!_flag) return;
            SearchAsThread();
        }

        private void txtTenThuoc_TextChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }
        #endregion

        #region Working Thread
        private void OnDisplayLoThuocListProc(object state)
        {
            try
            {
                OnDisplayLoThuocList();
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
                OnDisplayLoThuocList();
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
