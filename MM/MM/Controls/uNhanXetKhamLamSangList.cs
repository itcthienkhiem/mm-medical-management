using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using MM.Bussiness;
using MM.Common;
using MM.Databasae;
using MM.Dialogs;
using MM.Exports;
using SpreadsheetGear;

namespace MM.Controls
{
    public partial class uNhanXetKhamLamSangList : uBase
    {
        #region Members
        private Dictionary<string, DataRow> _dictNhanXet = new Dictionary<string, DataRow>();
        private string _name = string.Empty;
        private DataTable _dtTemp = null;
        #endregion

        #region Constructor
        public uNhanXetKhamLamSangList()
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

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                _name = txtNhanXet.Text;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayNhanXetKhamLamSangListProc));
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
            DataTable dt = dgNhanXet.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
            }

            dgNhanXet.DataSource = null;
        }

        public override void SearchAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                _name = txtNhanXet.Text;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplayNhanXetKhamLamSangList()
        {
            lock (ThisLock)
            {
                Result result = NhanXetKhamLamSangBus.GetNhanXetKhamLamSangist(_name);
                if (result.IsOK)
                {
                    dgNhanXet.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();
                        DataTable dt = result.QueryResult as DataTable;
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        UpdateChecked(dt);
                        dgNhanXet.DataSource = dt;
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("NhanXetKhamLamSangBus.GetNhanXetKhamLamSangist"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("NhanXetKhamLamSangBus.GetNhanXetKhamLamSangist"));
                }
            }
        }

        private void UpdateChecked(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string key = row["NhanXetKhamLamSangGUID"].ToString();
                if (_dictNhanXet.ContainsKey(key))
                    row["Checked"] = true;
            }
        }

        private void OnAdd()
        {
            dlgAddNhanXetKhamLamSang dlg = new dlgAddNhanXetKhamLamSang();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                SearchAsThread();
            }
        }

        private void OnEdit()
        {
            if (dgNhanXet.SelectedRows == null || dgNhanXet.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 nhận xét khám lâm sàng.", IconType.Information);
                return;
            }

            DataRow drData = (dgNhanXet.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (drData == null) return;
            dlgAddNhanXetKhamLamSang dlg = new dlgAddNhanXetKhamLamSang(drData);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SearchAsThread();
            }
        }

        private void OnDelete()
        {
            if (_dictNhanXet == null) return;

            List<string> deletedList = new List<string>();
            List<DataRow> deletedRows = _dictNhanXet.Values.ToList<DataRow>();

            foreach (DataRow row in deletedRows)
            {
                deletedList.Add(row["NhanXetKhamLamSangGUID"].ToString());
            }

            if (deletedList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những nhận xét khám lâm sàng mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = NhanXetKhamLamSangBus.DeleteNhanXetKhamLamSang(deletedList);
                    if (result.IsOK)
                    {
                        DataTable dt = dgNhanXet.DataSource as DataTable;
                        if (dt == null || dt.Rows.Count <= 0) return;

                        foreach (string key in deletedList)
                        {
                            DataRow[] rows = dt.Select(string.Format("NhanXetKhamLamSangGUID='{0}'", key));
                            if (rows == null || rows.Length <= 0) continue;
                            dt.Rows.Remove(rows[0]);        
                        }

                        _dictNhanXet.Clear();
                        _dtTemp.Rows.Clear();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("NhanXetKhamLamSangBus.DeleteNhanXetKhamLamSang"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("NhanXetKhamLamSangBus.DeleteNhanXetKhamLamSang"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những nhận xét khám lâm sàng cần xóa.", IconType.Information);
        }
        #endregion
       
        #region Window Event Handlers
        private void dgSymptom_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;
            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgNhanXet.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgNhanXet.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string nhanXetKhamLamSangGUID = row["NhanXetKhamLamSangGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            if (isChecked)
            {
                if (!_dictNhanXet.ContainsKey(nhanXetKhamLamSangGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictNhanXet.Add(nhanXetKhamLamSangGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictNhanXet.ContainsKey(nhanXetKhamLamSangGUID))
                {
                    _dictNhanXet.Remove(nhanXetKhamLamSangGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("NhanXetKhamLamSangGUID='{0}'", nhanXetKhamLamSangGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
                }
            }
        }

        private void dgSymptom_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
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

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgNhanXet.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
                string nhanXetKhamLamSangGUID = row["NhanXetKhamLamSangGUID"].ToString();

                if (chkChecked.Checked)
                {
                    if (!_dictNhanXet.ContainsKey(nhanXetKhamLamSangGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictNhanXet.Add(nhanXetKhamLamSangGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
                else
                {
                    if (_dictNhanXet.ContainsKey(nhanXetKhamLamSangGUID))
                    {
                        _dictNhanXet.Remove(nhanXetKhamLamSangGUID);

                        DataRow[] rows = _dtTemp.Select(string.Format("NhanXetKhamLamSangGUID='{0}'", nhanXetKhamLamSangGUID));
                        if (rows != null && rows.Length > 0)
                            _dtTemp.Rows.Remove(rows[0]);
                    }
                }
            }
        }

        private void txtTrieuChung_TextChanged(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void txtTrieuChung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgNhanXet.Focus();

                if (dgNhanXet.SelectedRows != null && dgNhanXet.SelectedRows.Count > 0)
                {
                    int index = dgNhanXet.SelectedRows[0].Index;
                    if (index < dgNhanXet.RowCount - 1)
                    {
                        index++;
                        dgNhanXet.CurrentCell = dgNhanXet[1, index];
                        dgNhanXet.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgNhanXet.Focus();

                if (dgNhanXet.SelectedRows != null && dgNhanXet.SelectedRows.Count > 0)
                {
                    int index = dgNhanXet.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgNhanXet.CurrentCell = dgNhanXet[1, index];
                        dgNhanXet.Rows[index].Selected = true;
                    }
                }
            }
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
        private void OnDisplayNhanXetKhamLamSangListProc(object state)
        {
            try
            {
                OnDisplayNhanXetKhamLamSangList();
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
                OnDisplayNhanXetKhamLamSangList();
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
