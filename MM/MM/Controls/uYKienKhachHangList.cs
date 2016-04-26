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
    public partial class uYKienKhachHangList : uBase
    {
        #region Members
        private string _tenBenhNhan = string.Empty;
        private string _tenNguoiTao = string.Empty;
        private string _bacSiPhuTrach = string.Empty;
        private DateTime _fromDate = Global.MinDateTime;
        private DateTime _toDate = Global.MaxDateTime;
        private int _inOut = 0; //0: All; 1: IN; 2: OUT
        private string _docStaffGUID = string.Empty;
        #endregion

        #region Constructor
        public uYKienKhachHangList()
        {
            InitializeComponent();
            dtpkDenNgay.Value = DateTime.Now;
            dtpkTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        public void ClearData()
        {
            DataTable dt = dgYKienKhachHang.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgYKienKhachHang.DataSource = null;
            }
        }

        private void ClearBSPhuTrach()
        {
            DataTable dt = cboDocStaff.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                //cboDocStaff.DataSource = null;
            }
        }

        public void DisplayBacSiPhuTrach()
        {
            ClearBSPhuTrach();
            //DocStaff
            List<byte> staffTypes = new List<byte>();
            staffTypes.Add((byte)StaffType.BacSi);
            staffTypes.Add((byte)StaffType.BacSiSieuAm);
            staffTypes.Add((byte)StaffType.BacSiNgoaiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiNoiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiPhuKhoa);
            Result result = DocStaffBus.GetDocStaffList(staffTypes);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
                return;
            }
            else
            {
                DataTable dt = result.QueryResult as DataTable;
                DataRow newRow = dt.NewRow();
                newRow["DocStaffGUID"] = Guid.Empty.ToString();
                newRow["FullName"] = string.Empty;
                dt.Rows.InsertAt(newRow, 0);
                cboDocStaff.DataSource = dt;
            }
        }

        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
            btnPrint.Enabled = AllowPrint;
            btnPrintPreview.Enabled = AllowPrint;
            btnExportExcel.Enabled = AllowExport;

            addToolStripMenuItem.Enabled = AllowAdd;
            editToolStripMenuItem.Enabled = AllowEdit;
            deleteToolStripMenuItem.Enabled = AllowDelete;
            printPreviewToolStripMenuItem.Enabled = AllowPrint;
            printToolStripMenuItem.Enabled = AllowPrint;
            exportExcelToolStripMenuItem.Enabled = AllowExport;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();

                lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
                chkChecked.Checked = false;

                _fromDate = Global.MinDateTime;
                _toDate = Global.MaxDateTime;

                if (chkTuNgay.Checked)
                    _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);

                if (chkDenNgay.Checked && chkTuNgay.Checked)
                    _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);

                _tenBenhNhan = txtTenBenhNhan.Text.Trim();
                _tenNguoiTao = txtTenNguoiTao.Text.Trim();
                _bacSiPhuTrach = cboDocStaff.Text.Trim();

                if (cboINOUT.Text == string.Empty)
                    _inOut = 0;
                else if (cboINOUT.Text == "IN")
                    _inOut = 1;
                else
                    _inOut = 2;

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayYKienKhachHangListProc));
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

        private void OnDisplayYKienKhachHangList()
        {
            Result result = YKienKhachHangBus.GetYKienKhachHangList(_fromDate, _toDate, _tenBenhNhan, _tenNguoiTao, _bacSiPhuTrach, _inOut);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    dgYKienKhachHang.DataSource = result.QueryResult;
                    lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", (result.QueryResult as DataTable).Rows.Count);
                    //RefreshNo();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("YKienKhachHangBus.GetYKienKhachHangList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("YKienKhachHangBus.GetYKienKhachHangList"));
            }
        }

        private void RefreshNo()
        {
            int index = 1;
            foreach (DataGridViewRow row in dgYKienKhachHang.Rows)
            {
                row.Cells["STT"].Value = index++;
            }
        }

        private void OnAdd()
        {
            dlgAddYKienKhachHang dlg = new dlgAddYKienKhachHang();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnEdit()
        {
            if (dgYKienKhachHang.SelectedRows == null || dgYKienKhachHang.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 ý kiến khách hàng.", IconType.Information);
                return;
            }

            DataRow drYKienKhachHang = (dgYKienKhachHang.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddYKienKhachHang dlg = new dlgAddYKienKhachHang(drYKienKhachHang);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnDelete()
        {
            List<string> deletedSpecList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgYKienKhachHang.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    string userGUID = row["ContactBy"].ToString();
                    if (userGUID != Global.UserGUID)
                    {
                        MsgBox.Show(Application.ProductName, "Bạn không thể xóa ý kiến khách hàng của người khác. Vui lòng kiểm tra lại.", IconType.Information);
                        return;
                    }

                    deletedSpecList.Add(row["YKienKhachHangGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedSpecList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những ý kiến khách hàng mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = YKienKhachHangBus.DeleteYKienKhachHang(deletedSpecList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("YKienKhachHangBus.DeleteYKienKhachHang"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("YKienKhachHangBus.DeleteYKienKhachHang"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những ý kiến khách hàng cần xóa.", IconType.Information);
        }

        private List<DataRow> GetCheckedRows()
        {
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgYKienKhachHang.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    checkedRows.Add(row);
                }
            }

            return checkedRows;
        }

        private void OnPrint(bool isPreview)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = GetCheckedRows();

            if (checkedRows.Count > 0)
            {
                string exportFileName = string.Format("{0}\\Temp\\YKienKhachHang.xls", Application.StartupPath);
                if (isPreview)
                {
                    if (!ExportExcel.ExportYKienKhachHangToExcel(exportFileName, checkedRows))
                        return;
                    else
                    {
                        try
                        {
                            ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.YKienKhachHangTemplate));
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                            return;
                        }
                    }
                }
                else
                {
                    if (_printDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (!ExportExcel.ExportYKienKhachHangToExcel(exportFileName, checkedRows))
                            return;
                        else
                        {
                            try
                            {
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.YKienKhachHangTemplate));
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                return;
                            }
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những ý kiến khách hàng cần in.", IconType.Information);
        }

        private void OnExportToExcel()
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = GetCheckedRows();

            if (checkedRows.Count > 0)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (!ExportExcel.ExportYKienKhachHangToExcel(dlg.FileName, checkedRows))
                        return;
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những ý kiến khách hàng cần xuất excel.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void dgYKienKhachHang_DoubleClick(object sender, EventArgs e)
        {
            if (dgYKienKhachHang.SelectedRows == null || dgYKienKhachHang.SelectedRows.Count <= 0) return;
            if (dgYKienKhachHang.CurrentCell != null && dgYKienKhachHang.CurrentCell.ColumnIndex == 0) return;

            if (dgYKienKhachHang.CurrentCell.ColumnIndex == 6)
            {
                DataRow row = (dgYKienKhachHang.SelectedRows[0].DataBoundItem as DataRowView).Row;
                if (row == null) return;
                string yKienKhachHangGUID = row["YKienKhachHangGUID"].ToString();
                string ketLuan = string.Empty;
                if (row["KetLuan"] != null && row["KetLuan"] != DBNull.Value)
                    ketLuan = row["KetLuan"].ToString();

                dlgKetLuan dlg = new dlgKetLuan(ketLuan);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    ketLuan = dlg.KetLuan;

                    Result result = YKienKhachHangBus.UpdateKetLuan(yKienKhachHangGUID, ketLuan);
                    if (result.IsOK)
                    {
                        row["NguoiKetLuan"] = Global.UserGUID;
                        row["TenNguoiKetLuan"] = Global.Fullname;
                        row["KetLuan"] = ketLuan;
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("YKienKhachHangBus.UpdateKetLuan"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("YKienKhachHangBus.UpdateKetLuan"));
                    }
                }
            }
            else OnEdit();
        }

        private void dgYKienKhachHang_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //RefreshNo();
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
            DataTable dt = dgYKienKhachHang.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void chkTuNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpkTuNgay.Enabled = chkTuNgay.Checked;

            //if (chkTuNgay.Checked)
            //{
            //    dtpkTuNgay.Enabled = true;
            //    chkDenNgay.Enabled = true;
            //    dtpkDenNgay.Enabled = chkDenNgay.Checked;
            //}
            //else
            //{
            //    dtpkTuNgay.Enabled = false;
            //    dtpkDenNgay.Enabled = false;
            //    chkDenNgay.Enabled = false;
            //}
        }

        private void chkDenNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpkDenNgay.Enabled = chkDenNgay.Checked;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (chkTuNgay.Checked && chkDenNgay.Checked && dtpkTuNgay.Value > dtpkDenNgay.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkTuNgay.Focus();
                return;
            }

            DisplayAsThread();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportToExcel();
        }

        private void dgYKienKhachHang_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.RowIndex < 0 || e.ColumnIndex != 6) return;
            //DataRow row = (dgYKienKhachHang.Rows[e.RowIndex].DataBoundItem as DataRowView).Row;
            //if (row == null) return;
            //string yKienKhachHangGUID = row["YKienKhachHangGUID"].ToString();
            //string ketLuan = string.Empty;
            //if (row["KetLuan"] != null && row["KetLuan"] != DBNull.Value)
            //    ketLuan = row["KetLuan"].ToString();

            //dlgKetLuan dlg = new dlgKetLuan(ketLuan);
            //if (dlg.ShowDialog(this) == DialogResult.OK)
            //{
            //    ketLuan = dlg.KetLuan;

            //    Result result = YKienKhachHangBus.UpdateKetLuan(yKienKhachHangGUID, ketLuan);
            //    if (result.IsOK)
            //    {
            //        row["NguoiKetLuan"] = Global.UserGUID;
            //        row["TenNguoiKetLuan"] = Global.Fullname;
            //        row["KetLuan"] = ketLuan;
            //    }
            //    else
            //    {
            //        MsgBox.Show(Application.ProductName, result.GetErrorAsString("YKienKhachHangBus.UpdateKetLuan"), IconType.Error);
            //        Utility.WriteToTraceLog(result.GetErrorAsString("YKienKhachHangBus.UpdateKetLuan"));
            //    }
            //}
        }

        private void uYKienKhachHangList_Load(object sender, EventArgs e)
        {
            //DisplayBacSiPhuTrach();
        }

        private void cboDocStaff_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DisplayAsThread();
        }

        private void txtTenBenhNhan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DisplayAsThread();
        }

        private void txtTenNguoiTao_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DisplayAsThread();
        }

        private void dgYKienKhachHang_SelectionChanged(object sender, EventArgs e)
        {
            int count = 0;
            if (dgYKienKhachHang.SelectedRows != null)
                count = dgYKienKhachHang.SelectedRows.Count;

            lbSoDongChon.Text = string.Format("Số dòng chọn: {0}", count);
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

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void exportExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExportToExcel();
        }
        #endregion

        #region Working Thread
        private void OnDisplayYKienKhachHangListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayYKienKhachHangList();
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
