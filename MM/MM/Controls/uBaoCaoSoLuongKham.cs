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
using MM.Exports;

namespace MM.Controls
{
    public partial class uBaoCaoSoLuongKham : uBase
    {
        #region Members
        
        #endregion

        #region Constructor
        public uBaoCaoSoLuongKham()
        {
            InitializeComponent();
            dtpkFromDate.Value = DateTime.Now;
            dtpkToDate.Value = DateTime.Now;
        }
        #endregion

        #region UI Command
        public void UpdateGUI()
        {
            btnPrintPreview.Enabled = AllowPrint;
            btnPrint.Enabled = AllowPrint;
            btnExportExcel.Enabled = AllowExport;

            printPreviewToolStripMenuItem.Enabled = AllowPrint;
            printToolStripMenuItem.Enabled = AllowPrint;
            exportExcelToolStripMenuItem.Enabled = AllowExport;
        }

        private void ClearData()
        {
            DataTable dt = dgBenhNhan.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                //dt = null;
                //dgBenhNhan.DataSource = null;
            }
        }

        private void OnViewData()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dtpkFromDate.Value > dtpkToDate.Value)
            {
                MsgBox.Show(Application.ProductName, "Từ ngày phải nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkFromDate.Focus();
                return;
            }

            DateTime fromDate = new DateTime(dtpkFromDate.Value.Year, dtpkFromDate.Value.Month, dtpkFromDate.Value.Day, 0, 0, 0);
            DateTime toDate = new DateTime(dtpkToDate.Value.Year, dtpkToDate.Value.Month, dtpkToDate.Value.Day, 23, 59, 59);
            bool isDenKham = raDenKham.Checked;
            int type = chkMaBenhNhan.Checked ? 1 : 0;

            ClearData();

            Result result = ReportBus.GetDanhSachBenhNhanKhamBenh(fromDate, toDate, txtMaBenhNhan.Text.Trim(), isDenKham, type);
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                dgBenhNhan.DataSource = dt;
                lbKetQua.Text = string.Format("kết quả được tìm thấy: {0}", dt.Rows.Count);
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetDanhSachBenhNhanKhamBenh"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetDanhSachBenhNhanKhamBenh"));
            }
        }

        private void OnPrint(bool isPreview)
        {
            if (dgBenhNhan.RowCount <= 0) return;
            DataTable dt = dgBenhNhan.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;

            string exportFileName = string.Format("{0}\\Temp\\DanhSachBenhNhanDenKham.xls", Application.StartupPath);
            if (isPreview)
            {
                if (ExportExcel.ExportDanhSachBenhNhanDenKhamToExcel(exportFileName, dt, raDenKham.Checked))
                {
                    try
                    {
                        ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.DanhSachBenhNhanDenKhamTemplate));
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                    }
                }
            }
            else
            {
                if (_printDialog.ShowDialog() == DialogResult.OK)
                {
                    if (ExportExcel.ExportDanhSachBenhNhanDenKhamToExcel(exportFileName, dt, raDenKham.Checked))
                    {
                        try
                        {
                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.DanhSachBenhNhanDenKhamTemplate));
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

        private void OnExportExcel()
        {
            if (dgBenhNhan.RowCount <= 0) return;
            DataTable dt = dgBenhNhan.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export Excel";
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                ExportExcel.ExportDanhSachBenhNhanDenKhamToExcel(dlg.FileName, dt, raDenKham.Checked);
            }
        }
        #endregion

        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            OnViewData();
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
            OnExportExcel();
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
            OnExportExcel();
        }

        private void raDenKham_CheckedChanged(object sender, EventArgs e)
        {
            if (raDenKham.Checked) OnViewData();
        }

        private void raChuaDenKham_CheckedChanged(object sender, EventArgs e)
        {
            if (raChuaDenKham.Checked) OnViewData();
        }

        private void chkMaBenhNhan_CheckedChanged(object sender, EventArgs e)
        {
            OnViewData();
        }
        #endregion
    }
}
