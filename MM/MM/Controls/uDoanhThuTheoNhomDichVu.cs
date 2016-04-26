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
using MM.Exports;
using MM.Common;

namespace MM.Controls
{
    public partial class uDoanhThuTheoNhomDichVu : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uDoanhThuTheoNhomDichVu()
        {
            InitializeComponent();
            dtpkTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            dtpkDenNgay.Value = DateTime.Now;
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        public void InitData()
        {
            btnExportExcel.Enabled = AllowExport;
            btnPrint.Enabled = AllowPrint;
            btnPrintPreview.Enabled = AllowPrint;
        }

        private bool CheckInfo()
        {
            if (dtpkTuNgay.Value > dtpkDenNgay.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkTuNgay.Focus();
                return false;
            }

            return true;
        }

        private void OnExportExcel()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export Excel";
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DateTime tuNgay = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                DateTime denNgay = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                int type = 0;
                if (raAll.Checked) type = 0;
                else if (raDaThuTien.Checked) type = 1;
                ExportExcel.ExportDoanhThuTheoNhomDichVuToExcel(dlg.FileName, tuNgay, denNgay, type);
            }
        }

        private void OnPrint(bool isPreview)
        {
            string exportFileName = string.Format("{0}\\Temp\\DoanhThuTheoNhomDichVuTemplate.xls", Application.StartupPath);
            if (isPreview)
            {
                DateTime tuNgay = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                DateTime denNgay = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                int type = 0;
                if (raAll.Checked) type = 0;
                else if (raDaThuTien.Checked) type = 1;
                else type = 2;
                if (ExportExcel.ExportDoanhThuTheoNhomDichVuToExcel(exportFileName, tuNgay, denNgay, type))
                {
                    try
                    {
                        ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.DoanhThuTheoNgayTemplate));
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
                    DateTime tuNgay = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                    DateTime denNgay = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                    int type = 0;
                    if (raAll.Checked) type = 0;
                    else if (raDaThuTien.Checked) type = 1;
                    if (ExportExcel.ExportDoanhThuTheoNhomDichVuToExcel(exportFileName, tuNgay, denNgay, type))
                    {
                        try
                        {
                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.DoanhThuTheoNgayTemplate));
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
        #endregion

        #region Window Events Handlers
        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            if (CheckInfo())
                OnPrint(true);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (CheckInfo())
                OnPrint(false);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (CheckInfo())
                OnExportExcel();
        }
        #endregion
    }
}
