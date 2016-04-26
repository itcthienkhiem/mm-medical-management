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
using MM.Bussiness;
using MM.Dialogs;
using MM.Exports;

namespace MM.Controls
{
    public partial class uPhieuChiList : uBase
    {
        #region Members
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        #endregion

        #region Constructor
        public uPhieuChiList()
        {
            InitializeComponent();

            dtpkDenNgay.Value = DateTime.Now;
            dtpkTuNgay.Value = DateTime.Now.AddDays(-7);
        }
        #endregion

        #region UI Command
        public void ClearData()
        {
            DataTable dt = dgPhieuChi.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgPhieuChi.DataSource = null;
            }
        }

        public void DisplayAsThread()
        {
            try
            {
                lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
                _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayPhieuChiListProc));
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

        private void OnDisplayPhieuChiList()
        {
            Result result = PhieuChiBus.GetPhieuChiList(_fromDate, _toDate);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    DataTable dt = result.QueryResult as DataTable;
                    dgPhieuChi.DataSource = result.QueryResult;
                    lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dt.Rows.Count);
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuChiBus.GetPhieuChiList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PhieuChiBus.GetPhieuChiList"));
            }
        }

        private void ThemPhieuChi()
        {
            dlgAddPhieuChi dlg = new dlgAddPhieuChi();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void SuaPhieuChi()
        {
            if (dgPhieuChi.SelectedRows == null || dgPhieuChi.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 phiếu chi.", IconType.Information);
                return;
            }

            DataRow drPhieuChi = (dgPhieuChi.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddPhieuChi dlg = new dlgAddPhieuChi(drPhieuChi);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void XoaPhieuChi()
        {
            List<string> deletedPhieuChiList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgPhieuChi.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedPhieuChiList.Add(row["PhieuChiGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedPhieuChiList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những phiếu chi mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = PhieuChiBus.DeletePhieuChi(deletedPhieuChiList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuChiBus.DeletePhieuChi"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PhieuChiBus.DeletePhieuChi"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những phiếu chi cần xóa.", IconType.Information);
        }

        private void XuatExcel()
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgPhieuChi.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                    checkedRows.Add(row);
            }

            if (checkedRows.Count > 0)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ExportExcel.ExportDanhSachPhieuChiToExcel(dlg.FileName, checkedRows);
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những phiếu chi cần xuất Excel.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ThemPhieuChi();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SuaPhieuChi();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            XoaPhieuChi();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            XuatExcel();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgPhieuChi.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (dtpkTuNgay.Value > dtpkDenNgay.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkTuNgay.Focus();
                return;
            }
            
            DisplayAsThread();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemPhieuChi();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SuaPhieuChi();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XoaPhieuChi();
        }

        private void exportExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XuatExcel();
        }

        private void dgPhieuChi_DoubleClick(object sender, EventArgs e)
        {
            SuaPhieuChi();
        }
        #endregion

        #region Working Thread
        private void OnDisplayPhieuChiListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayPhieuChiList();
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
