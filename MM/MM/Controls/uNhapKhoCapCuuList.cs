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

namespace MM.Controls
{
    public partial class uNhapKhoCapCuuList : uBase
    {
        #region Members
        private DataTable _dtTemp = null;
        private int _currentRowIndex = 0;
        private Dictionary<string, DataRow> _dictNhapKhoCapCuu = new Dictionary<string,DataRow>();
        private string _name = string.Empty;
        private bool _isTenCapCuu = true;
        private DateTime _tuNgay = DateTime.Now;
        private DateTime _denNgay = DateTime.Now;
        private bool _flag = true;
        #endregion

        #region Constructor
        public uNhapKhoCapCuuList()
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
            DataTable dt = dgNhapKhoCapCuu.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgNhapKhoCapCuu.DataSource = null;
            }
        }

        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnDelete.Enabled = AllowDelete;

            addToolStripMenuItem.Enabled = AllowAdd;
            deleteToolStripMenuItem.Enabled = AllowDelete;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                _name = txtTenCapCuu.Text;
                _isTenCapCuu = raTenCapCuu.Checked;
                _tuNgay = dtpkTuNgay.Value;
                _denNgay = dtpkDenNgay.Value;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayNhapKhoCapCuuListProc));
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
                _isTenCapCuu = raTenCapCuu.Checked;
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

        private void OnDisplayNhapKhoCapCuuList()
        {
            lock (ThisLock)
            {
                Result result = NhapKhoCapCuuBus.GetNhapKhoCapCuuList(_name, _tuNgay, _denNgay, _isTenCapCuu);
                if (result.IsOK)
                {
                    dgNhapKhoCapCuu.Invoke(new MethodInvoker(delegate()
                    {
                        if (dgNhapKhoCapCuu.CurrentRow != null)
                            _currentRowIndex = dgNhapKhoCapCuu.CurrentRow.Index;

                        ClearData();

                        DataTable dt = result.QueryResult as DataTable;
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        UpdateChecked(dt);
                        dgNhapKhoCapCuu.DataSource = dt;

                        if (_currentRowIndex < dt.Rows.Count)
                        {
                            dgNhapKhoCapCuu.CurrentCell = dgNhapKhoCapCuu[0, _currentRowIndex];
                            dgNhapKhoCapCuu.Rows[_currentRowIndex].Selected = true;
                        }
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("NhapKhoCapCuuBus.GetNhapKhoCapCuuList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("NhapKhoCapCuuBus.GetNhapKhoCapCuuList"));
                }
            }
        }

        private void UpdateChecked(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string key = row["NhapKhoCapCuuGUID"].ToString();
                if (_dictNhapKhoCapCuu.ContainsKey(key))
                    row["Checked"] = true;
            }
        }

        private void OnAdd()
        {
            dlgAddNhapKhoCapCuu dlg = new dlgAddNhapKhoCapCuu();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                SearchAsThread();
            }
        }

        private void OnEdit()
        {
            if (dgNhapKhoCapCuu.SelectedRows == null || dgNhapKhoCapCuu.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 thông tin cấp cứu.", IconType.Information);
                return;
            }

            DataRow drNhapKhoCapCuu = (dgNhapKhoCapCuu.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (drNhapKhoCapCuu == null) return;
            dlgAddNhapKhoCapCuu dlg = new dlgAddNhapKhoCapCuu(drNhapKhoCapCuu, AllowEdit);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                SearchAsThread();
            }
        }

        private void OnDelete()
        {
            if (_dictNhapKhoCapCuu == null) return;
            List<string> deletedList = new List<string>();
            List<DataRow> deletedRows = _dictNhapKhoCapCuu.Values.ToList();

            foreach (DataRow row in deletedRows)
            {
                deletedList.Add(row["NhapKhoCapCuuGUID"].ToString());
            }

            if (deletedList.Count > 0)
            {
                foreach (string key in deletedList)
                {
                    Result rs = NhapKhoCapCuuBus.GetNhapKhoCapCuu(key);
                    if (!rs.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, rs.GetErrorAsString("NhapKhoCapCuuBus.GetNhapKhoCapCuu"), IconType.Error);
                        Utility.WriteToTraceLog(rs.GetErrorAsString("NhapKhoCapCuuBus.GetNhapKhoCapCuu"));
                        return;
                    }

                    NhapKhoCapCuuView nkcc = rs.QueryResult as NhapKhoCapCuuView;
                    if (nkcc.SoLuongXuat > 0)
                    {
                        MsgBox.Show(Application.ProductName, string.Format("Cấp cứu: '{0}' này đã xuất rồi không thể xóa.", nkcc.TenCapCuu),
                            IconType.Information);
                        return;
                    }
                }

                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những thông tin nhập kho cấp cứu mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = NhapKhoCapCuuBus.DeleteNhapKhoCappCuu(deletedList);
                    if (result.IsOK)
                    {
                        DataTable dt = dgNhapKhoCapCuu.DataSource as DataTable;
                        if (dt == null || dt.Rows.Count <= 0) return;

                        foreach (string key in deletedList)
                        {
                            DataRow[] rows = dt.Select(string.Format("NhapKhoCapCuuGUID='{0}'", key));
                            if (rows == null || rows.Length <= 0) continue;
                            dt.Rows.Remove(rows[0]);
                        }

                        _dictNhapKhoCapCuu.Clear();
                        _dtTemp.Rows.Clear();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("NhapKhoCapCuuBus.DeleteNhapKhoCappCuu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("NhapKhoCapCuuBus.DeleteNhapKhoCappCuu"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những thông tin nhập kho cấp cứu cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void dgNhapKhoCapCuu_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgNhapKhoCapCuu.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgNhapKhoCapCuu.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string nhapKhoCapCuuGUID = row["NhapKhoCapCuuGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            if (isChecked)
            {
                if (!_dictNhapKhoCapCuu.ContainsKey(nhapKhoCapCuuGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictNhapKhoCapCuu.Add(nhapKhoCapCuuGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictNhapKhoCapCuu.ContainsKey(nhapKhoCapCuuGUID))
                {
                    _dictNhapKhoCapCuu.Remove(nhapKhoCapCuuGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("NhapKhoCapCuuGUID='{0}'", nhapKhoCapCuuGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
                }
            }
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgNhapKhoCapCuu.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
                string nhapKhoCapCuuGUID = row["NhapKhoCapCuuGUID"].ToString();
                if (chkChecked.Checked)
                {
                    if (!_dictNhapKhoCapCuu.ContainsKey(nhapKhoCapCuuGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictNhapKhoCapCuu.Add(nhapKhoCapCuuGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
                else
                {
                    if (_dictNhapKhoCapCuu.ContainsKey(nhapKhoCapCuuGUID))
                    {
                        _dictNhapKhoCapCuu.Remove(nhapKhoCapCuuGUID);

                        DataRow[] rows = _dtTemp.Select(string.Format("NhapKhoCapCuuGUID='{0}'", nhapKhoCapCuuGUID));
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

        private void dgLoThuoc_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void raTenThuoc_CheckedChanged(object sender, EventArgs e)
        {
            txtTenCapCuu.ReadOnly = !raTenCapCuu.Checked;
            dtpkTuNgay.Enabled = !raTenCapCuu.Checked;
            dtpkDenNgay.Enabled = !raTenCapCuu.Checked;

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
            StartTimer();
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
        private void OnDisplayNhapKhoCapCuuListProc(object state)
        {
            try
            {
                OnDisplayNhapKhoCapCuuList();
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
                OnDisplayNhapKhoCapCuuList();
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
