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
using MM.Bussiness;
using MM.Common;
using MM.Databasae;
using MM.Exports;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uInKetQuaKhamSucKhoeTongQuat : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        #endregion

        #region Constructor
        public uInKetQuaKhamSucKhoeTongQuat()
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
            UpdateGUI();
        }

        private void UpdateGUI()
        {
            btnPrintPreview.Enabled = this.AllowPrint;
            btnPrint.Enabled = this.AllowPrint;
            btnExportExcel.Enabled = this.AllowExport;
        }

        private bool CheckInfo()
        {
            if (txtTenBenhNhan.Text.Trim() == string.Empty)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 bệnh nhân.", IconType.Information);
                btnChonBenhNhan.Focus();
                return false;
            }

            if (dtpkTuNgay.Value > dtpkDenNgay.Value)
            {
                MsgBox.Show(Application.ProductName, "Từ ngày phải nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkTuNgay.Focus();
                return false;
            }

            return true;
        }

        private void OnExportExcel()
        {
            if (!CheckInfo()) return;

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export Excel";
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DateTime tuNgay = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                DateTime denNgay = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                ExportExcel.ExportKhamSucKhoeTongQuatToExcel(dlg.FileName, _patientRow, tuNgay, denNgay);
            }
        }

        private void OnPrint(bool isPreview)
        {
            if (!CheckInfo()) return;

            string exportFileName = string.Format("{0}\\Temp\\KhamSucKhoeTongQuat.xls", Application.StartupPath);
            if (isPreview)
            {
                DateTime tuNgay = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                DateTime denNgay = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                if (ExportExcel.ExportKhamSucKhoeTongQuatToExcel(exportFileName, _patientRow, tuNgay, denNgay))
                {
                    try
                    {
                        ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.KhamSucKhoeTongQuatTemplate));
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
                    if (ExportExcel.ExportKhamSucKhoeTongQuatToExcel(exportFileName, _patientRow, tuNgay, denNgay))
                    {
                        try
                        {
                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KhamSucKhoeTongQuatTemplate));
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

        #region Window Event Handlers
        private void btnChonBenhNhan_Click(object sender, EventArgs e)
        {
            dlgSelectPatient dlg = new dlgSelectPatient(PatientSearchType.BenhNhan);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                _patientRow = dlg.PatientRow;
                if (_patientRow != null)
                {
                    txtTenBenhNhan.Tag = _patientRow["PatientGUID"].ToString();
                    txtTenBenhNhan.Text = _patientRow["FullName"].ToString();
                    txtNgaySinh.Text = _patientRow["DobStr"].ToString();
                    txtGioiTinh.Text = _patientRow["GenderAsStr"].ToString();
                    txtDienThoai.Text = _patientRow["Mobile"].ToString();
                    txtDiaChi.Text = _patientRow["Address"].ToString();
                }
            }
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
        #endregion
    }
}
