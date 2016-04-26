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
using System.IO;
using System.Threading;
using System.Windows.Forms;
using MM.Bussiness;
using MM.Databasae;
using MM.Common;
using MM.Dialogs;
using MM.Exports;
using SpreadsheetGear;

namespace MM.Controls
{
    public partial class uInvoiceList : uBase
    {
        #region Members
        private string _tenBenhNhan = string.Empty;
        private string _tenDonVi = string.Empty;
        private string _maSoThue = string.Empty;
        private DateTime _fromDate = Global.MinDateTime;
        private DateTime _toDate = Global.MaxDateTime;
        private int _type = 1; //0: TatCa; 1: ChuaXoa; 2: DaXoa
        #endregion

        #region Constructor
        public uInvoiceList()
        {
            InitializeComponent();

            dtpkDenNgay.Value = DateTime.Now;
            dtpkTuNgay.Value = DateTime.Now.AddDays(-7);
        }
        #endregion

        #region Properties

        #endregion

        #region UI Commnad
        private void UpdateGUI()
        {
            btnDelete.Enabled = AllowDelete;
            btnPrint.Enabled = AllowPrint;
            btnPrintPreview.Enabled = AllowPrint;
            btnExportInvoice.Enabled = AllowExport;

            deleteToolStripMenuItem.Enabled = AllowDelete;
            printToolStripMenuItem.Enabled = AllowPrint;
            xemBanInToolStripMenuItem.Enabled = AllowPrint;
            xuatHoaDonToolStripMenuItem.Enabled = AllowExport;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();

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

                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayInvoiceListProc));
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

        private void OnDisplayInvoiceList()
        {
            Result result = InvoiceBus.GetInvoiceList(_fromDate, _toDate, _tenBenhNhan, _tenDonVi, _maSoThue, _type);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    dgInvoice.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("InvoiceBus.GetInvoiceList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("InvoiceBus.GetInvoiceList"));
            }
        }

        public void ClearData()
        {
            DataTable dt = dgInvoice.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgInvoice.DataSource = null;
            }
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
                        string invoiceCode = row["InvoiceCode"].ToString();
                        string invoiceGUID = row["InvoiceGUID"].ToString();

                        dlgLyDoXoa dlg = new dlgLyDoXoa(invoiceCode, 1);
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            noteList.Add(dlg.Notes);
                            deletedInvoiceList.Add(invoiceGUID);
                        }
                    }

                    if (deletedInvoiceList.Count > 0)
                    {
                        Result result = InvoiceBus.DeleteInvoices(deletedInvoiceList, noteList);
                        if (result.IsOK)
                        {
                            foreach (DataRow row in deletedRows)
                            {
                                string invoiceGUID = row["InvoiceGUID"].ToString();
                                if (deletedInvoiceList.Contains(invoiceGUID))
                                    dt.Rows.Remove(row);
                            }
                        }
                        else
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("InvoiceBus.DeleteInvoices"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("InvoiceBus.DeleteInvoices"));
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
                    checkedInvoicetList.Add(row["invoiceGUID"].ToString());
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
                            foreach (string invoiceGUID in checkedInvoicetList)
                            {
                                if (dlg.Lien1)
                                {
                                    if (ExportExcel.ExportInvoiceToExcel(exportFileName, invoiceGUID, "                                   Liên 1: Lưu"))
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
                                    if (ExportExcel.ExportInvoiceToExcel(exportFileName, invoiceGUID, "                               Liên 2: Giao cho người mua"))
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
                                    if (ExportExcel.ExportInvoiceToExcel(exportFileName, invoiceGUID, "                                   Liên 3: Nội bộ"))
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
                                foreach (string invoiceGUID in checkedInvoicetList)
                                {
                                    if (dlg.Lien1)
                                    {
                                        if (ExportExcel.ExportInvoiceToExcel(exportFileName, invoiceGUID, "                                   Liên 1: Lưu"))
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
                                        if (ExportExcel.ExportInvoiceToExcel(exportFileName, invoiceGUID, "                               Liên 2: Giao cho người mua"))
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
                                        if (ExportExcel.ExportInvoiceToExcel(exportFileName, invoiceGUID, "                                   Liên 3: Nội bộ"))
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
            dlgInvoiceInfo2 dlg = new dlgInvoiceInfo2(drInvoice, true);
            dlg.ShowDialog();
        }

        private void OnXuatHoaDon()
        {
            dlgInvoiceInfo dlg = new dlgInvoiceInfo(null);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }
        #endregion

        #region Window Event Handlers
        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteInvoice();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgInvoice.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void dgInvoice_DoubleClick(object sender, EventArgs e)
        {
            OnDisplayInvoiceInfo();
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

        private void btnExportInvoice_Click(object sender, EventArgs e)
        {
            OnXuatHoaDon();
        }

        private void xuatHoaDonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnXuatHoaDon();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDeleteInvoice();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void xemBanInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }
        #endregion

        #region Working Thread
        private void OnDisplayInvoiceListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayInvoiceList();
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
