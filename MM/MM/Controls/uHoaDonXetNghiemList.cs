﻿using System;
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
    public partial class uHoaDonXetNghiemList : uBase
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
        public uHoaDonXetNghiemList()
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

        private void OnDisplayInvoiceList()
        {
            Result result = HoaDonXetNghiemBus.GetHoaDonXetNghiemList(_fromDate, _toDate, _tenBenhNhan, _tenDonVi, _maSoThue, _type);
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
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("HoaDonXetNghiemBus.GetHoaDonXetNghiemList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("HoaDonXetNghiemBus.GetHoaDonXetNghiemList"));
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
                        string soHoaDon = row["SoHoaDon"].ToString();
                        string hoaDonXetNghiemGUID = row["HoaDonXetNghiemGUID"].ToString();

                        dlgLyDoXoa dlg = new dlgLyDoXoa(soHoaDon, 1);
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            noteList.Add(dlg.Notes);
                            deletedInvoiceList.Add(hoaDonXetNghiemGUID);
                        }
                    }

                    if (deletedInvoiceList.Count > 0)
                    {
                        Result result = HoaDonXetNghiemBus.DeleteHoaDonXetNghiem(deletedInvoiceList, noteList);
                        if (result.IsOK)
                        {
                            foreach (DataRow row in deletedRows)
                            {
                                string hoaDonXetNghiemGUID = row["HoaDonXetNghiemGUID"].ToString();
                                if (deletedInvoiceList.Contains(hoaDonXetNghiemGUID))
                                    dt.Rows.Remove(row);
                            }
                        }
                        else
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("HoaDonXetNghiemBus.DeleteHoaDonXetNghiem"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("HoaDonXetNghiemBus.DeleteHoaDonXetNghiem"));
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
                    checkedInvoicetList.Add(row["HoaDonXetNghiemGUID"].ToString());
                }
            }

            if (checkedInvoicetList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn in những hóa đơn mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    dlgPrintType dlg = new dlgPrintType();
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string exportFileName = string.Format("{0}\\Temp\\HoaDonXetNghiemYKhoaTemplate.xls", Application.StartupPath);
                        if (isPreview)
                        {
                            foreach (string hoaDonXetNghiemGUID in checkedInvoicetList)
                            {
                                if (dlg.Lien1)
                                {
                                    if (ExportExcel.ExportHoaDonXetNghiemToExcel(exportFileName, hoaDonXetNghiemGUID, "                                                                              Liên 1: Lưu"))
                                    {
                                        try
                                        {
                                            ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.HoaDonXetNghiemYKhoaTemplate));
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
                                    if (ExportExcel.ExportHoaDonXetNghiemToExcel(exportFileName, hoaDonXetNghiemGUID, "                                                                Liên 2: Giao cho người mua"))
                                    {
                                        try
                                        {
                                            ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.HoaDonXetNghiemYKhoaTemplate));
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
                                    if (ExportExcel.ExportHoaDonXetNghiemToExcel(exportFileName, hoaDonXetNghiemGUID, "                                                                        Liên 3: Nội bộ"))
                                    {
                                        try
                                        {
                                            ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.HoaDonXetNghiemYKhoaTemplate));
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
                                foreach (string hoaDonXetNghiemGUID in checkedInvoicetList)
                                {
                                    if (dlg.Lien1)
                                    {
                                        if (ExportExcel.ExportHoaDonXetNghiemToExcel(exportFileName, hoaDonXetNghiemGUID, "                                                                              Liên 1: Lưu"))
                                        {
                                            try
                                            {
                                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.HoaDonXetNghiemYKhoaTemplate));
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
                                        if (ExportExcel.ExportHoaDonXetNghiemToExcel(exportFileName, hoaDonXetNghiemGUID, "                                                                Liên 2: Giao cho người mua"))
                                        {
                                            try
                                            {
                                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.HoaDonXetNghiemYKhoaTemplate));
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
                                        if (ExportExcel.ExportHoaDonXetNghiemToExcel(exportFileName, hoaDonXetNghiemGUID, "                                                                        Liên 3: Nội bộ"))
                                        {
                                            try
                                            {
                                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.HoaDonXetNghiemYKhoaTemplate));
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
            dlgHoaDonXetNghiem dlg = new dlgHoaDonXetNghiem(drInvoice, true);
            dlg.ShowDialog();
        }

        private void OnXuatHoaDon()
        {
            dlgHoaDonXetNghiem dlg = new dlgHoaDonXetNghiem();
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