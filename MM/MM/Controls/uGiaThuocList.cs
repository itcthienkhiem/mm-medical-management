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
    public partial class uGiaThuocList : uBase
    {
        #region Members
        private DataTable _dtTemp = null;
        private Dictionary<string, DataRow> _dictGiaThuoc = new Dictionary<string,DataRow>();
        private string _name = string.Empty;
        private Object _thisLock = new Object();
        #endregion

        #region Constructor
        public uGiaThuocList()
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
            DataTable dt = dgGiaThuoc.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgGiaThuoc.DataSource = null;
            }
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                _name = txtTenThuoc.Text;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayGiaThuocListProc));
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
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplayGiaThuocList()
        {
            lock (_thisLock)
            {
                Result result = GiaThuocBus.GetGiaThuocList(_name);
                if (result.IsOK)
                {
                    dgGiaThuoc.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();

                        DataTable dt = result.QueryResult as DataTable;
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        UpdateChecked(dt);
                        dgGiaThuoc.DataSource = dt;
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("GiaThuocBus.GetGiaThuocList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("GiaThuocBus.GetGiaThuocList"));
                }
            }
        }

        private void UpdateChecked(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string key = row["GiaThuocGUID"].ToString();
                if (_dictGiaThuoc.ContainsKey(key))
                    row["Checked"] = true;
            }
        }

        private void OnAddGiaThuoc()
        {
            dlgAddGiaThuoc dlg = new dlgAddGiaThuoc();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SearchAsThread();
            }
        }

        private void OnEditGiaThuoc()
        {
            if (_dictGiaThuoc == null) return;

            if (dgGiaThuoc.SelectedRows == null || dgGiaThuoc.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 giá thuốc.", IconType.Information);
                return;
            }

            DataRow drGiaThuoc = (dgGiaThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (drGiaThuoc == null) return;
            dlgAddGiaThuoc dlg = new dlgAddGiaThuoc(drGiaThuoc);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SearchAsThread();
            }
        }

        private void OnDeleteGiaThuoc()
        {
            if (_dictGiaThuoc == null) return;
            List<string> deletedGiaThuocList = new List<string>();
            List<DataRow> deletedRows = _dictGiaThuoc.Values.ToList();
            foreach (DataRow row in deletedRows)
            {
                string giaThuocGUID = row["GiaThuocGUID"].ToString();
                deletedGiaThuocList.Add(giaThuocGUID);
            }

            if (deletedGiaThuocList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những giá thuốc mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = GiaThuocBus.DeleteGiaThuoc(deletedGiaThuocList);
                    if (result.IsOK)
                    {
                        DataTable dt = dgGiaThuoc.DataSource as DataTable;
                        if (dt == null || dt.Rows.Count <= 0) return;
                        foreach (string key in deletedGiaThuocList)
                        {
                            DataRow[] rows = dt.Select(string.Format("GiaThuocGUID='{0}'", key));
                            if (rows == null || rows.Length <= 0) continue;
                            dt.Rows.Remove(rows[0]);
                        }

                        _dictGiaThuoc.Clear();
                        _dtTemp.Rows.Clear();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("GiaThuocBus.DeleteGiaThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("GiaThuocBus.DeleteGiaThuoc"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những giá thuốc cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void dgGiaThuoc_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgGiaThuoc.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgGiaThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string giaThuocGUID = row["GiaThuocGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            if (isChecked)
            {
                if (!_dictGiaThuoc.ContainsKey(giaThuocGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictGiaThuoc.Add(giaThuocGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictGiaThuoc.ContainsKey(giaThuocGUID))
                {
                    _dictGiaThuoc.Remove(giaThuocGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("GiaThuocGUID='{0}'", giaThuocGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddGiaThuoc();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditGiaThuoc();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteGiaThuoc();
        }

        private void dgGiaThuoc_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEditGiaThuoc();
        }


        private void txtTenThuoc_TextChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgGiaThuoc.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
                string giaThuocGUID = row["GiaThuocGUID"].ToString();
                if (chkChecked.Checked)
                {
                    if (!_dictGiaThuoc.ContainsKey(giaThuocGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictGiaThuoc.Add(giaThuocGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
                else
                {
                    if (_dictGiaThuoc.ContainsKey(giaThuocGUID))
                    {
                        _dictGiaThuoc.Remove(giaThuocGUID);

                        DataRow[] rows = _dtTemp.Select(string.Format("GiaThuocGUID='{0}'", giaThuocGUID));
                        if (rows != null && rows.Length > 0)
                            _dtTemp.Rows.Remove(rows[0]);
                    }
                }
            }
        }

        private void txtTenThuoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgGiaThuoc.Focus();

                if (dgGiaThuoc.SelectedRows != null && dgGiaThuoc.SelectedRows.Count > 0)
                {
                    int index = dgGiaThuoc.SelectedRows[0].Index;
                    if (index < dgGiaThuoc.RowCount - 1)
                    {
                        index++;
                        dgGiaThuoc.CurrentCell = dgGiaThuoc[1, index];
                        dgGiaThuoc.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgGiaThuoc.Focus();

                if (dgGiaThuoc.SelectedRows != null && dgGiaThuoc.SelectedRows.Count > 0)
                {
                    int index = dgGiaThuoc.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgGiaThuoc.CurrentCell = dgGiaThuoc[1, index];
                        dgGiaThuoc.Rows[index].Selected = true;
                    }
                }
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayGiaThuocListProc(object state)
        {
            try
            {
                OnDisplayGiaThuocList();
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
                OnDisplayGiaThuocList();
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
