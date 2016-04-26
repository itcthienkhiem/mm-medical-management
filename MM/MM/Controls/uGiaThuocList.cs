/* Copyright (c) 2016, Cocosoft Inc.
 All rights reserved.
 http://www.Cocosofttech.com

 This file is part of the LIS open source project.

 The LIS  open source project is free software: you can
 redistribute it and/or modify it under the terms of the GNU General Public
 License as published by the Free Software Foundation, either version 3 of the
 License, or (at your option) any later version.

 The ClearCanvas LIS open source project is distributed in the hope that it
 will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
 Public License for more details.

 You should have received a copy of the GNU General Public License along with
 the LIS open source project.  If not, see
 <http://www.gnu.org/licenses/>.
*/
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
    public partial class uGiaThuocList : uBase
    {
        #region Members
        private DataTable _dtTemp = null;
        private Dictionary<string, DataRow> _dictGiaThuoc = new Dictionary<string,DataRow>();
        private string _name = string.Empty;
        private int _type = 0; //0: Tên thuốc; 1: Biệt dược
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
            btnExportExcel.Enabled = AllowExport;

            addToolStripMenuItem.Enabled = AllowAdd;
            editToolStripMenuItem.Enabled = AllowEdit;
            deleteToolStripMenuItem.Enabled = AllowDelete;
            exportExcelToolStripMenuItem.Enabled = AllowExport;
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
                _type = chkBietDuoc.Checked ? 1 : 0;
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

        public override void SearchAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                _name = txtTenThuoc.Text;
                _type = chkBietDuoc.Checked ? 1 : 0;
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
            lock (ThisLock)
            {
                Result result = GiaThuocBus.GetGiaThuocList(_name, _type);
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

        private void OnExportExcel()
        {
            List<DataRow> checkedRows = _dictGiaThuoc.Values.ToList();
            if (checkedRows.Count > 0)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ExportExcel.ExportGiaThuocToExcel(dlg.FileName, checkedRows);
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những giá thuốc cần xuất Excel.", IconType.Information);
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
            StartTimer();
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

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAddGiaThuoc();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnEditGiaThuoc();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDeleteGiaThuoc();
        }

        private void chkBietDuoc_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void exportExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportExcel();
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
