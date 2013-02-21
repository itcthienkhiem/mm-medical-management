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
    public partial class uKhoCapCuu : uBase
    {
        #region Members
        private bool _isReport = false;
        private DataTable _dtTemp = null;
        private Dictionary<string, DataRow> _dictKhoCapCuu = new Dictionary<string,DataRow>();
        private string _name = string.Empty;
        #endregion

        #region Constructor
        public uKhoCapCuu()
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
            get
            {
                List<DataRow> checkedRows = _dictKhoCapCuu.Values.ToList();
                return checkedRows;
            }
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
            btnExportExcel.Enabled = AllowExport;

            addToolStripMenuItem.Enabled = AllowAdd;
            editToolStripMenuItem.Enabled = AllowEdit;
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
                dgThuoc.DataSource = null;
            }
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                _name = txtTenCapCuu.Text;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayKhoCapCuuProc));
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
                _name = txtTenCapCuu.Text;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplayKhoCapCuu()
        {
            lock (ThisLock)
            {
                Result result = KhoCapCuuBus.GetDanhSachCapCuu(_name);
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
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("KhoCapCuuBus.GetDanhSachCapCuu"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("KhoCapCuuBus.GetDanhSachCapCuu"));
                }
            }
        }

        private void UpdateChecked(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string key = row["KhoCapCuuGUID"].ToString();
                if (_dictKhoCapCuu.ContainsKey(key))
                    row["Checked"] = true;
            }
        }

        private void OnAdd()
        {
            dlgAddCapCuu dlg = new dlgAddCapCuu();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                SearchAsThread();
            }
        }

        private void OnEdit()
        {
            if (_dictKhoCapCuu == null) return;
            if (dgThuoc.SelectedRows == null || dgThuoc.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 thông tin cấp cứu.", IconType.Information);
                return;
            }

            DataRow drCapCuu = (dgThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (drCapCuu == null) return;
            dlgAddCapCuu dlg = new dlgAddCapCuu(drCapCuu);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SearchAsThread();
            }
        }

        private void OnDelete()
        {
            if (_dictKhoCapCuu == null) return;
            List<string> deletedThuocList = new List<string>();
            List<DataRow> deletedRows = _dictKhoCapCuu.Values.ToList();

            foreach (DataRow row in deletedRows)
            {
                deletedThuocList.Add(row["KhoCapCuuGUID"].ToString());
            }

            if (deletedThuocList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những thông tin cấp cứu mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = KhoCapCuuBus.DeleteThongTinCapCuu(deletedThuocList);
                    if (result.IsOK)
                    {
                        DataTable dt = dgThuoc.DataSource as DataTable;
                        if (dt == null || dt.Rows.Count <= 0) return;
                        foreach (string key in deletedThuocList)
                        {
                            DataRow[] rows = dt.Select(string.Format("KhoCapCuuGUID='{0}'", key));
                            if (rows == null || rows.Length <= 0) continue;
                            dt.Rows.Remove(rows[0]);
                        }

                        _dictKhoCapCuu.Clear();
                        _dtTemp.Rows.Clear();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("KhoCapCuuBus.DeleteThongTinCapCuu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KhoCapCuuBus.DeleteThongTinCapCuu"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những thông tin cấp cứu cần xóa.", IconType.Information);
        }

        private void OnExportExcel()
        {
            if (_dictKhoCapCuu == null) return;
            List<DataRow> checkedRows = _dictKhoCapCuu.Values.ToList();
            
            if (checkedRows.Count <= 0)
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những thông tin cấp cứu cần xuất excel.", IconType.Information);
            else
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    ExportExcel.ExportDanhSachKhoCapCuuToExcel(dlg.FileName, checkedRows);
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
            string khoCapCuuGUID = row["KhoCapCuuGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            if (isChecked)
            {
                if (!_dictKhoCapCuu.ContainsKey(khoCapCuuGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictKhoCapCuu.Add(khoCapCuuGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictKhoCapCuu.ContainsKey(khoCapCuuGUID))
                {
                    _dictKhoCapCuu.Remove(khoCapCuuGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("KhoCapCuuGUID='{0}'", khoCapCuuGUID));
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
                string khoCapCuuGUID = row["KhoCapCuuGUID"].ToString();
                if (chkChecked.Checked)
                {
                    if (!_dictKhoCapCuu.ContainsKey(khoCapCuuGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictKhoCapCuu.Add(khoCapCuuGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
                else
                {
                    if (_dictKhoCapCuu.ContainsKey(khoCapCuuGUID))
                    {
                        _dictKhoCapCuu.Remove(khoCapCuuGUID);

                        DataRow[] rows = _dtTemp.Select(string.Format("KhoCapCuuGUID='{0}'", khoCapCuuGUID));
                        if (rows != null && rows.Length > 0)
                            _dtTemp.Rows.Remove(rows[0]);
                    }
                }
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

        private void dgThuoc_DoubleClick(object sender, EventArgs e)
        {
            if (_isReport) return;
            if (!AllowEdit) return;
            OnEdit();
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

        private void exportExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }
        #endregion

        #region Working Thread
        private void OnDisplayKhoCapCuuProc(object state)
        {
            try
            {
                OnDisplayKhoCapCuu();
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
                OnDisplayKhoCapCuu();
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
