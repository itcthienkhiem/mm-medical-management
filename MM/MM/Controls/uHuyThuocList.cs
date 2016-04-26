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
using MM.Common;
using MM.Bussiness;
using MM.Databasae;
using MM.Dialogs;
using System.Threading;

namespace MM.Controls
{
    public partial class uHuyThuocList : uBase
    {
        #region Members
        private DataTable _dtTemp = null;
        private Dictionary<string, DataRow> _dictHuyThuoc = new Dictionary<string, DataRow>();
        private string _name = string.Empty;
        private DateTime _tuNgay = DateTime.Now;
        private DateTime _denNgay = DateTime.Now;
        private bool _flag = true;
        #endregion

        #region Constructor
        public uHuyThuocList()
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
            DataTable dt = dgHuyThuoc.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgHuyThuoc.DataSource = null;
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
                _name = txtTenThuoc.Text;

                if (chkTuNgay.Checked)
                {
                    _tuNgay = dtpkTuNgay.Value;
                    _denNgay = dtpkDenNgay.Value;
                }
                else
                {
                    _tuNgay = Global.MinDateTime;
                    _denNgay = Global.MaxDateTime;
                }

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayHuyThuocListProc));
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

                if (chkTuNgay.Checked)
                {
                    _tuNgay = dtpkTuNgay.Value;
                    _denNgay = dtpkDenNgay.Value;
                }
                else
                {
                    _tuNgay = Global.MinDateTime;
                    _denNgay = Global.MaxDateTime;
                }

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayHuyThuocListProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplayHuyThuocList()
        {
            lock (ThisLock)
            {
                Result result = HuyThuocBus.GetHuyThuocList(_name, _tuNgay, _denNgay);
                if (result.IsOK)
                {
                    dgHuyThuoc.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();

                        DataTable dt = result.QueryResult as DataTable;
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        UpdateChecked(dt);
                        dgHuyThuoc.DataSource = dt;
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("HuyThuocBus.GetHuyThuocList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("HuyThuocBus.GetHuyThuocList"));
                }
            }
        }

        private void UpdateChecked(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string key = row["HuyThuocGUID"].ToString();
                if (_dictHuyThuoc.ContainsKey(key))
                    row["Checked"] = true;
            }
        }

        private void OnAdd()
        {
            dlgAddHuyThuoc dlg = new dlgAddHuyThuoc();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                SearchAsThread();
            }
        }

        private void OnDelete()
        {
            if (_dictHuyThuoc == null) return;
            List<string> deletedHuyThuocList = new List<string>();
            List<DataRow> deletedRows = _dictHuyThuoc.Values.ToList();

            if (deletedRows.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những hủy thuốc mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    foreach (DataRow row in deletedRows)
                    {
                        string huyThuocGUID = row["HuyThuocGUID"].ToString();
                        deletedHuyThuocList.Add(huyThuocGUID);
                    }

                    Result result = HuyThuocBus.DeleteHuyThuoc(deletedHuyThuocList);
                    if (result.IsOK)
                    {
                        DataTable dt = dgHuyThuoc.DataSource as DataTable;
                        if (dt == null || dt.Rows.Count <= 0) return;

                        foreach (string key in deletedHuyThuocList)
                        {
                            DataRow[] rows = dt.Select(string.Format("HuyThuocGUID='{0}'", key));
                            if (rows == null || rows.Length <= 0) continue;
                            dt.Rows.Remove(rows[0]);

                            _dictHuyThuoc.Remove(key);
                            rows = _dtTemp.Select(string.Format("HuyThuocGUID='{0}'", key));
                            if (rows != null && rows.Length > 0)
                                _dtTemp.Rows.Remove(rows[0]);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("HuyThuocBus.DeleteHuyThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("HuyThuocBus.DeleteHuyThuoc"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những hủy thuốc cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void chkTuNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpkTuNgay.Enabled = chkTuNgay.Checked;
            dtpkDenNgay.Enabled = chkTuNgay.Checked;

            SearchAsThread();
        }

        private void dtpkTuNgay_ValueChanged(object sender, EventArgs e)
        {
            if (!_flag) return;
            SearchAsThread();
        }

        private void txtTenThuoc_TextChanged(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgHuyThuoc.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
                string huyThuocGUID = row["HuyThuocGUID"].ToString();
                if (chkChecked.Checked)
                {
                    if (!_dictHuyThuoc.ContainsKey(huyThuocGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictHuyThuoc.Add(huyThuocGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
                else
                {
                    if (_dictHuyThuoc.ContainsKey(huyThuocGUID))
                    {
                        _dictHuyThuoc.Remove(huyThuocGUID);

                        DataRow[] rows = _dtTemp.Select(string.Format("HuyThuocGUID='{0}'", huyThuocGUID));
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDelete();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDelete();
        }

        private void dgHuyThuoc_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgHuyThuoc.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgHuyThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string huyThuocGUID = row["HuyThuocGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            if (isChecked)
            {
                if (!_dictHuyThuoc.ContainsKey(huyThuocGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictHuyThuoc.Add(huyThuocGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictHuyThuoc.ContainsKey(huyThuocGUID))
                {
                    _dictHuyThuoc.Remove(huyThuocGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("HuyThuocGUID='{0}'", huyThuocGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
                }
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayHuyThuocListProc(object state)
        {
            try
            {
                OnDisplayHuyThuocList();
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
                OnDisplayHuyThuocList();
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
