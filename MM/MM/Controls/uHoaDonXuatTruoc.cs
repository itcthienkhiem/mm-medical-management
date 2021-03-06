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
using MM.Databasae;
using MM.Dialogs;
using MM.Exports;

namespace MM.Controls
{
    public partial class uHoaDonXuatTruoc : uBase
    {
        #region Members
        private string _tenBenhNhan = string.Empty;
        private string _tenDonVi = string.Empty;
        private string _maSoThue = string.Empty;
        private DateTime _fromDate = Global.MinDateTime;
        private DateTime _toDate = Global.MaxDateTime;
        private int _type = 1; //0: TatCa; 1: ChuaXoa; 2: DaXoa
        private bool _flag = false;
        private string _mauSo = string.Empty;
        private string _kiHieu = string.Empty;
        private DateTime _fromNgayThayDoiHD = DateTime.Now;
        private DateTime _toNgayThayDoiHD = DateTime.Now;
        #endregion

        #region Constructor
        public uHoaDonXuatTruoc()
        {
            InitializeComponent();
            dtpkDenNgay.Value = DateTime.Now;
            dtpkTuNgay.Value = DateTime.Now.AddDays(-7);
        }
        #endregion

        #region Properties
        public List<DataRow> SoHoaDonCheckedRows
        {
            get
            {
                DataTable dt = dgSoHoaDon.DataSource as DataTable;
                if (dt == null || dt.Rows.Count <= 0) return null;

                DataRow[] rows = dt.Select("Checked='True'");
                if (rows == null || rows.Length <= 0) return null;

                List<DataRow> checkedRows = new List<DataRow>();
                checkedRows.AddRange(rows);
                return checkedRows;
            }
        }

        public List<string> SoHoaDonCheckedKeys
        {
            get
            {
                DataTable dt = dgSoHoaDon.DataSource as DataTable;
                if (dt == null || dt.Rows.Count <= 0) return null;

                DataRow[] rows = dt.Select("Checked='True'");
                if (rows == null || rows.Length <= 0) return null;

                List<string> checkedKeys = new List<string>();
                foreach (DataRow row in rows)
                {
                    checkedKeys.Add(row["QuanLySoHoaDonGUID"].ToString());    
                }
                
                return checkedKeys;
            }
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnDeleteHoaDon.Enabled = AllowDelete;
            btnPrint.Enabled = AllowPrint;
            btnPrintPreview.Enabled = AllowPrint;
            btnExportInvoice.Enabled = AllowExport;

            btnAdd.Enabled = AllowAddDangKy;
            btnDelete.Enabled = AllowDeleteDangKy;

            deleteToolStripMenuItem.Enabled = AllowDelete;
            printToolStripMenuItem.Enabled = AllowPrint;
            xemBanInToolStripMenuItem.Enabled = AllowPrint;
            xuatHoaDonToolStripMenuItem.Enabled = AllowExport;

            addToolStripMenuItem.Enabled = AllowAddDangKy;
            deleteDangKySoHDToolStripMenuItem.Enabled = AllowDeleteDangKy;
        }

        public void ClearData()
        {
            DataTable dt = dgSoHoaDon.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgSoHoaDon.DataSource = null;
            }

            dt = dgInvoice.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgInvoice.DataSource = null;
            }
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                ClearData();
                DisplayComboMauHoaDon();

                _fromDate = Global.MinDateTime;
                _toDate = Global.MaxDateTime;

                if (chkTuNgay.Checked)
                    _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);

                if (chkDenNgay.Checked && chkTuNgay.Checked)
                    _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);

                _tenBenhNhan = txtTenBenhNhan.Text.Trim();
                _tenDonVi = txtTenDonVi.Text.Trim();
                _maSoThue = txtMaSoThue.Text.Trim();

                if (raTatCa.Checked) _type = 0;
                else if (raChuaXoa.Checked) _type = 1;
                else _type = 2;

                chkChecked_HoaDon.Checked = false;
                chkChecked_SoHoaDon.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayInfoProc));
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

        private void DisplayComboMauHoaDon()
        {
            Result result = QuanLySoHoaDonBus.GetMauHoaDonList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    _flag = true;
                    object key = cboMauHoaDon.SelectedValue;
                    cboMauHoaDon.DataSource = result.QueryResult as DataTable;
                    cboMauHoaDon.DisplayMember = "MauHoaDon";
                    cboMauHoaDon.ValueMember = "MaNgayBatDauGUID";
                    if (key != null) cboMauHoaDon.SelectedValue = key;
                    SetMauHoaDon();
                    _flag = false;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("QuanLySoHoaDonBus.GetMauHoaDonList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("QuanLySoHoaDonBus.GetMauHoaDonList"));
            }
        }

        private void SetMauHoaDon()
        {
            if (cboMauHoaDon.SelectedValue == null) return;
            string mauHoaDon = cboMauHoaDon.Text;
            int index = mauHoaDon.IndexOf("-");
            if (index >= 0)
            {
                _mauSo = mauHoaDon.Substring(0, index);
                _kiHieu = mauHoaDon.Substring(index + 1, mauHoaDon.Length - index - 1);
                Result result = QuanLySoHoaDonBus.GetThayDoiSoHoaDon(_mauSo, _kiHieu, ref _toNgayThayDoiHD);
                if (result.IsOK)
                {
                    NgayBatDauLamMoiSoHoaDon nbdlm = result.QueryResult as NgayBatDauLamMoiSoHoaDon;
                    _fromNgayThayDoiHD = nbdlm.NgayBatDau;
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("QuanLySoHoaDonBus.GetThayDoiSoHoaDon"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("QuanLySoHoaDonBus.GetThayDoiSoHoaDon"));
                }
            }
        }

        private void DisplayDSHoaDonXuatTruoc()
        {
            Result result = HoaDonXuatTruocBus.GetHoaDonXuatTruocList(_fromDate, _toDate, _tenBenhNhan, _tenDonVi, _maSoThue, _type);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgInvoice.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("HoaDonXuatTruocBus.GetHoaDonXuatTruocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("HoaDonXuatTruocBus.GetHoaDonXuatTruocList"));
            }
        }

        private void DisplayDSSoHoaDonXuatTruoc()
        {
            Result result = HoaDonXuatTruocBus.GetSoHoaDonXuatTruocList(_fromNgayThayDoiHD, _toNgayThayDoiHD);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgSoHoaDon.DataSource = result.QueryResult;
                    HighlightSoHoaDonDaXuat();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("HoaDonXuatTruocBus.GetSoHoaDonXuatTruocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("HoaDonXuatTruocBus.GetSoHoaDonXuatTruocList"));
            }
        }

        public void HighlightSoHoaDonDaXuat()
        {
            foreach (DataGridViewRow row in dgSoHoaDon.Rows)
            {
                DataRow dr = (row.DataBoundItem as DataRowView).Row;
                bool isExported = Convert.ToBoolean(dr["DaXuat"]);
                if (isExported)
                {
                    row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                    dr["Checked"] = false;
                    (row.Cells["colChecked"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
            }
        }

        private void OnAddDangKy()
        {
            dlgAddSoHoaDonXuatTruoc dlg = new dlgAddSoHoaDonXuatTruoc(_fromNgayThayDoiHD, _toNgayThayDoiHD);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DisplayDSSoHoaDonXuatTruoc();
            }
        }

        private void OnDeleteDangKy()
        {
            List<string> checkedKeys = SoHoaDonCheckedKeys;
            if (checkedKeys == null || checkedKeys.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những số hóa đơn cần xóa.", IconType.Information);
                return;
            }

            if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những số hóa đơn mà bạn đã đánh dấu ?") == DialogResult.Yes)
            {
                Result result = HoaDonXuatTruocBus.DeleteQuanLySoHoaDon(checkedKeys);
                if (result.IsOK)
                {
                    DisplayDSSoHoaDonXuatTruoc();
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("HoaDonXuatTruocBus.DeleteQuanLySoHoaDon"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("HoaDonXuatTruocBus.DeleteQuanLySoHoaDon"));
                }
            }
        }

        private void OnExportInvoice()
        {
            List<DataRow> checkedRows = SoHoaDonCheckedRows;
            if (checkedRows == null || checkedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những số hóa đơn cần xuất.", IconType.Information);
                return;
            }

            if (MsgBox.Question(Application.ProductName, "Bạn có muốn xuất hóa đơn ?") == DialogResult.No) return;

            foreach (DataRow row in checkedRows)
            {
                int soHoaDon = Convert.ToInt32(row["SoHoaDon"]);
                dlgHoaDonXuatTruoc dlg = new dlgHoaDonXuatTruoc(soHoaDon, _fromNgayThayDoiHD, _toNgayThayDoiHD, _mauSo, _kiHieu);
                dlg.ShowDialog();
            }

            DisplayAsThread();
        }

        private void OnDeleteInvoice()
        {
            List<string> deletedInvoiceList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgInvoice.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    //deletedInvoiceList.Add(row["HoaDonXuatTruocGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedRows.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những hóa đơn mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    List<string> noteList = new List<string>();

                    foreach (DataRow row in deletedRows)
                    {
                        string soHoaDon = row["SoHoaDon"].ToString();
                        string hoaDonXuatTruocGUID = row["HoaDonXuatTruocGUID"].ToString();

                        dlgLyDoXoa dlg = new dlgLyDoXoa(soHoaDon, 1);
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            noteList.Add(dlg.Notes);
                            deletedInvoiceList.Add(hoaDonXuatTruocGUID);
                        }
                    }

                    if (deletedInvoiceList.Count > 0)
                    {
                        Result result = HoaDonXuatTruocBus.DeleteHoaDonXuatTruoc(deletedInvoiceList, noteList);
                        if (result.IsOK)
                        {
                            foreach (DataRow row in deletedRows)
                            {
                                string hoaDonXuatTruocGUID = row["HoaDonXuatTruocGUID"].ToString();
                                if (deletedInvoiceList.Contains(hoaDonXuatTruocGUID))
                                    dt.Rows.Remove(row);
                            }

                            DisplayDSSoHoaDonXuatTruoc();
                        }
                        else
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("HoaDonXuatTruocBus.DeleteHoaDonXuatTruoc"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("HoaDonXuatTruocBus.DeleteHoaDonXuatTruoc"));
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những hóa đơn cần xóa.", IconType.Information);
        }

        private void OnPrint(bool isPreview)
        {
            List<string> checkedInvoicetList = new List<string>();
            DataTable dt = dgInvoice.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    checkedInvoicetList.Add(row["HoaDonXuatTruocGUID"].ToString());
                }
            }

            if (checkedInvoicetList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn in những hóa đơn mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    dlgPrintType dlg = new dlgPrintType();
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string exportFileName = string.Format("{0}\\Temp\\HDGTGT.xls", Application.StartupPath);
                        if (isPreview)
                        {
                            foreach (string hoaDonXuatTruocGUID in checkedInvoicetList)
                            {
                                if (dlg.Lien1)
                                {
                                    if (ExportExcel.ExportHoaDonXuatTruocToExcel(exportFileName, hoaDonXuatTruocGUID, "                                   Liên 1: Lưu"))
                                    {
                                        try
                                        {
                                            ExcelPrintPreview.PrintPreview(exportFileName, null);
                                        }
                                        catch (Exception ex)
                                        {
                                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                            return;
                                        }
                                    }
                                    else return;
                                }

                                if (dlg.Lien2)
                                {
                                    if (ExportExcel.ExportHoaDonXuatTruocToExcel(exportFileName, hoaDonXuatTruocGUID, "                               Liên 2: Giao cho người mua"))
                                    {
                                        try
                                        {
                                            ExcelPrintPreview.PrintPreview(exportFileName, null);
                                        }
                                        catch (Exception ex)
                                        {
                                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                            return;
                                        }
                                    }
                                    else return;
                                }

                                if (dlg.Lien3)
                                {
                                    if (ExportExcel.ExportHoaDonXuatTruocToExcel(exportFileName, hoaDonXuatTruocGUID, "                                   Liên 3: Nội bộ"))
                                    {
                                        try
                                        {
                                            ExcelPrintPreview.PrintPreview(exportFileName, null);
                                        }
                                        catch (Exception ex)
                                        {
                                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                            return;
                                        }
                                    }
                                    else return;
                                }
                            }
                        }
                        else
                        {
                            if (_printDialog.ShowDialog() == DialogResult.OK)
                            {
                                foreach (string hoaDonXuatTruocGUID in checkedInvoicetList)
                                {
                                    if (dlg.Lien1)
                                    {
                                        if (ExportExcel.ExportHoaDonXuatTruocToExcel(exportFileName, hoaDonXuatTruocGUID, "                                   Liên 1: Lưu"))
                                        {
                                            try
                                            {
                                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, null);
                                            }
                                            catch (Exception ex)
                                            {
                                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                                return;
                                            }
                                        }
                                        else return;
                                    }

                                    if (dlg.Lien2)
                                    {
                                        if (ExportExcel.ExportHoaDonXuatTruocToExcel(exportFileName, hoaDonXuatTruocGUID, "                               Liên 2: Giao cho người mua"))
                                        {
                                            try
                                            {
                                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, null);
                                            }
                                            catch (Exception ex)
                                            {
                                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                                return;
                                            }
                                        }
                                        else return;
                                    }

                                    if (dlg.Lien3)
                                    {
                                        if (ExportExcel.ExportHoaDonXuatTruocToExcel(exportFileName, hoaDonXuatTruocGUID, "                                   Liên 3: Nội bộ"))
                                        {
                                            try
                                            {
                                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, null);
                                            }
                                            catch (Exception ex)
                                            {
                                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                                return;
                                            }
                                        }
                                        else return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những hóa đơn cần in.", IconType.Information);
        }

        private void OnDisplayInvoiceInfo()
        {
            if (dgInvoice.SelectedRows == null || dgInvoice.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 hóa đơn.", IconType.Information);
                return;
            }

            DataRow drInvoice = (dgInvoice.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgHoaDonXuatTruoc dlg = new dlgHoaDonXuatTruoc(drInvoice, true);
            dlg.ShowDialog();
        }

        private void dgSoHoaDon_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            HighlightSoHoaDonDaXuat();
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_SoHoaDon_CheckedChanged(object sender, EventArgs e)
        {
            if (dgSoHoaDon.RowCount <= 0) return;
            foreach (DataGridViewRow row in dgSoHoaDon.Rows)
            {
                DataGridViewDisableCheckBoxCell cell = row.Cells["colChecked"] as DataGridViewDisableCheckBoxCell;
                if (cell.Enabled)
                    cell.Value = chkChecked_SoHoaDon.Checked;
                else
                    cell.Value = false;
            }

            //DataTable dt = dgSoHoaDon.DataSource as DataTable;
            //if (dt == null || dt.Rows.Count <= 0) return;
            //foreach (DataRow row in dt.Rows)
            //{
            //    row["Checked"] = chkChecked_SoHoaDon.Checked;
            //}
        }

        private void chkChecked_HoaDon_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgInvoice.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked_HoaDon.Checked;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddDangKy();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteDangKy();
        }

        private void chkTuNgay_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkDenNgay_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (chkTuNgay.Checked && chkDenNgay.Checked && dtpkTuNgay.Value > dtpkDenNgay.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkTuNgay.Focus();
                return;
            }

            _fromDate = Global.MinDateTime;
            _toDate = Global.MaxDateTime;

            if (chkTuNgay.Checked)
                _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);

            if (chkDenNgay.Checked && chkTuNgay.Checked)
                _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);

            _tenBenhNhan = txtTenBenhNhan.Text.Trim();
            _tenDonVi = txtTenDonVi.Text.Trim();
            _maSoThue = txtMaSoThue.Text.Trim();

            if (raTatCa.Checked) _type = 0;
            else if (raChuaXoa.Checked) _type = 1;
            else _type = 2;

            chkChecked_HoaDon.Checked = false;

            DisplayDSHoaDonXuatTruoc();
        }

        private void btnExportInvoice_Click(object sender, EventArgs e)
        {
            OnExportInvoice();
        }

        private void btnDeleteHoaDon_Click(object sender, EventArgs e)
        {
            OnDeleteInvoice();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void dgInvoice_DoubleClick(object sender, EventArgs e)
        {
            OnDisplayInvoiceInfo();
        }

        private void xuatHoaDonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExportInvoice();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDeleteInvoice();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAddDangKy();
        }

        private void deleteDangKySoHDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDeleteDangKy();
        }

        private void xemBanInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void cboMauHoaDon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_flag) return;
            SetMauHoaDon();
            DisplayDSSoHoaDonXuatTruoc();
        }
        #endregion

        #region Working Thread
        private void OnDisplayInfoProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                DisplayDSSoHoaDonXuatTruoc();
                DisplayDSHoaDonXuatTruoc();
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
