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
    public partial class uBaoCaoDoanhThuThuocTheoPhieuThu : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uBaoCaoDoanhThuThuocTheoPhieuThu()
        {
            InitializeComponent();
            dtpkTuNgay.Value = DateTime.Now;
            dtpkDenNgay.Value = DateTime.Now;
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        public void InitData()
        {
            btnPrintPreview.Enabled = AllowPrint;
            btnPrint.Enabled = AllowPrint;
            btnExportExcel.Enabled = AllowExport;
        }

        private void OnPrint(bool isPreview)
        {
            Cursor.Current = Cursors.WaitCursor;

            int type = 0;
            if (raDaThuTien.Checked) type = 1;
            else if (raChuaThuTien.Checked) type = 2;

            string exportFileName = string.Format("{0}\\Temp\\BaoCaoDoanhThuThuocTheoPhieuThu.xls", Application.StartupPath);
            if (isPreview)
            {
                if (!chkGiaNhap.Checked)
                {
                    if (!ExportExcel.ExportDoanhThuThuocTheoPhieuThuKhongGiaNhapToExcel(exportFileName, dtpkTuNgay.Value, dtpkDenNgay.Value, type))
                        return;
                }
                else
                {
                    if (!ExportExcel.ExportDoanhThuThuocTheoPhieuThuCoGiaNhapToExcel(exportFileName, dtpkTuNgay.Value, dtpkDenNgay.Value, type))
                        return;
                }


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
            else
            {
                if (_printDialog.ShowDialog() == DialogResult.OK)
                {
                    if (!chkGiaNhap.Checked)
                    {
                        if (!ExportExcel.ExportDoanhThuThuocTheoPhieuThuKhongGiaNhapToExcel(exportFileName, dtpkTuNgay.Value, dtpkDenNgay.Value, type))
                            return;
                    }
                    else
                    {
                        if (!ExportExcel.ExportDoanhThuThuocTheoPhieuThuCoGiaNhapToExcel(exportFileName, dtpkTuNgay.Value, dtpkDenNgay.Value, type))
                            return;
                    }

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
            }

        }

        private void OnExportExcel()
        {
            Cursor.Current = Cursors.WaitCursor;
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export Excel";
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                int type = 0;
                if (raDaThuTien.Checked) type = 1;
                else if (raChuaThuTien.Checked) type = 2;

                if (!chkGiaNhap.Checked)
                    ExportExcel.ExportDoanhThuThuocTheoPhieuThuKhongGiaNhapToExcel(dlg.FileName, dtpkTuNgay.Value, dtpkDenNgay.Value, type);
                else
                    ExportExcel.ExportDoanhThuThuocTheoPhieuThuCoGiaNhapToExcel(dlg.FileName, dtpkTuNgay.Value, dtpkDenNgay.Value, type);
            }
        }
        #endregion

        #region Window Event Handlers
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
        #endregion
    }
}
