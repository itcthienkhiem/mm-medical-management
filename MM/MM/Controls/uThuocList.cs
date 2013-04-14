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
using MM.Exports;

namespace MM.Controls
{
    public partial class uThuocList : uBase
    {
        #region Members
        private bool _isReport = false;
        private Dictionary<string, DataRow> _dictThuoc = new Dictionary<string,DataRow>();
        private DataTable _dtTemp = null;
        private string _name = string.Empty;
        #endregion

        #region Constructor
        public uThuocList()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool IsReport
        {
            get { return _isReport; }
            set
            {
                _isReport = value;
                panel1.Visible = !_isReport;
            }
        }

        public List<DataRow> CheckedRows
        {
            get { return _dictThuoc.Values.ToList(); }
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnDelete.Enabled = AllowDelete;
            btnExportExcel.Enabled = AllowExport;

            addToolStripMenuItem.Enabled = AllowAdd;
            deleteToolStripMenuItem.Enabled = AllowDelete;
            exportExcelToolStripMenuItem.Enabled = AllowExport;
        }

        public void ClearData()
        {
            DataTable dt = dgThuoc.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
            }

            dgThuoc.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                _name = txtTenThuoc.Text;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayThuocListProc));
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

        public override void SearchAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                _name = txtTenThuoc.Text;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplayThuocList()
        {
            lock (ThisLock)
            {
                Result result = ThuocBus.GetThuocList(_name);
                if (result.IsOK)
                {
                    dgThuoc.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();

                        DataTable dt = result.QueryResult as DataTable;
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        UpdateChecked(dt);
                        dgThuoc.DataSource = dt;
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ThuocBus.GetThuocList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ThuocBus.GetThuocList"));
                }
            }
        }

        private void UpdateChecked(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string key = row["ThuocGUID"].ToString();
                if (_dictThuoc.ContainsKey(key))
                    row["Checked"] = true;
            }
        }

        private void OnAddThuoc()
        {
            dlgAddThuoc dlg = new dlgAddThuoc();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                SearchAsThread();
            }
        }

        private void OnEditThuoc()
        {
            if (_dictThuoc == null) return;
            if (dgThuoc.SelectedRows == null || dgThuoc.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 thuốc.", IconType.Information);
                return;
            }

            DataRow drThuoc = (dgThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (drThuoc == null) return;
            dlgAddThuoc dlg = new dlgAddThuoc(drThuoc, AllowEdit);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SearchAsThread();
            }
        }

        private void OnDeleteThuoc()
        {
            if (_dictThuoc == null) return;
            
            List<string> deletedThuocList = new List<string>();
            List<DataRow> deletedRows = _dictThuoc.Values.ToList();
            
            foreach (DataRow row in deletedRows)
            {
                deletedThuocList.Add(row["ThuocGUID"].ToString());
            }

            if (deletedThuocList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những thuốc mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = ThuocBus.DeleteThuoc(deletedThuocList);
                    if (result.IsOK)
                    {
                        DataTable dt = dgThuoc.DataSource as DataTable;
                        if (dt == null || dt.Rows.Count <= 0) return;

                        foreach (string key in deletedThuocList)
                        {
                            DataRow[] rows = dt.Select(string.Format("ThuocGUID='{0}'", key));
                            if (rows == null || rows.Length <= 0) continue;
                            dt.Rows.Remove(rows[0]);
                        }

                        _dictThuoc.Clear();
                        _dtTemp.Rows.Clear();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ThuocBus.DeleteThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ThuocBus.DeleteThuoc"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những thuốc cần xóa.", IconType.Information);
        }

        private void OnExportExcel()
        {
            if (_dictThuoc == null) return;
            List<DataRow> checkedRows = _dictThuoc.Values.ToList();
            
            if (checkedRows.Count <= 0)
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những thuốc cần xuất excel.", IconType.Information);
            else
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    ExportExcel.ExportDanhSachThuocToExcel(dlg.FileName, checkedRows);
                }
            }
        }
        #endregion

        #region Window Event Handlers
        private void dgThuoc_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgThuoc.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string thuocGUID = row["ThuocGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            if (isChecked)
            {
                if (!_dictThuoc.ContainsKey(thuocGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictThuoc.Add(thuocGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictThuoc.ContainsKey(thuocGUID))
                {
                    _dictThuoc.Remove(thuocGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("ThuocGUID='{0}'", thuocGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
                }
            }
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgThuoc.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
                string thuocGUID = row["ThuocGUID"].ToString();

                if (chkChecked.Checked)
                {
                    if (!_dictThuoc.ContainsKey(thuocGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictThuoc.Add(thuocGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
                else
                {
                    if (_dictThuoc.ContainsKey(thuocGUID))
                    {
                        _dictThuoc.Remove(thuocGUID);

                        DataRow[] rows = _dtTemp.Select(string.Format("ThuocGUID='{0}'", thuocGUID));
                        if (rows != null && rows.Length > 0)
                            _dtTemp.Rows.Remove(rows[0]);
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddThuoc();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditThuoc();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteThuoc();
        }

        private void dgThuoc_DoubleClick(object sender, EventArgs e)
        {
            if (_isReport) return;
            OnEditThuoc();
        }

        private void txtTenThuoc_TextChanged(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void txtTenThuoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgThuoc.Focus();

                if (dgThuoc.SelectedRows != null && dgThuoc.SelectedRows.Count > 0)
                {
                    int index = dgThuoc.SelectedRows[0].Index;
                    if (index < dgThuoc.RowCount - 1)
                    {
                        index++;
                        dgThuoc.CurrentCell = dgThuoc[1, index];
                        dgThuoc.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgThuoc.Focus();

                if (dgThuoc.SelectedRows != null && dgThuoc.SelectedRows.Count > 0)
                {
                    int index = dgThuoc.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgThuoc.CurrentCell = dgThuoc[1, index];
                        dgThuoc.Rows[index].Selected = true;
                    }
                }
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAddThuoc();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnEditThuoc();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDeleteThuoc();
        }

        private void exportExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }
        #endregion

        #region Working Thread
        private void OnDisplayThuocListProc(object state)
        {
            try
            {
                OnDisplayThuocList();
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
                OnDisplayThuocList();
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
