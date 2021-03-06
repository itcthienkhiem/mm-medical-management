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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MM.Common;
using MM.Bussiness;
using MM.Exports;
using MM.Databasae;
using SpreadsheetGear;

namespace MM.Dialogs
{
    public partial class dlgReceiptDetail : Form
    {
        #region Members
        private DataRow _drReceipt = null;
        private bool _isExportedInvoice = false;
        #endregion

        #region Constructor
        public dlgReceiptDetail(DataRow drReceipt)
        {
            InitializeComponent();
            cboHinhThucThanhToan.SelectedIndex = 0;
            _drReceipt = drReceipt;
            UpdateGUI();
            DisplayInfo(drReceipt);
        }
        #endregion

        #region Properties
        public bool IsExportedInvoice
        {
            get { return _isExportedInvoice; }
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnPrint.Enabled = Global.AllowPrintPhieuThuDichVu;
            bool isExportedInvoice = Convert.ToBoolean(_drReceipt["IsExportedInVoice"]);
            btnExportInvoice.Enabled = Global.AllowExportHoaDonDichVu && !isExportedInvoice;
        }

        private void DisplayInfo(DataRow drReceipt)
        {
            Cursor.Current = Cursors.WaitCursor;
            txtPatientName.Text = drReceipt["FullName"].ToString();
            txtFileNum.Text = drReceipt["FileNum"].ToString();

            if (drReceipt["Address"] != null && drReceipt["Address"] != DBNull.Value)
                txtAddress.Text = drReceipt["Address"].ToString();

            txtReceiptDate.Text = Convert.ToDateTime(drReceipt["ReceiptDate"]).ToString("dd/MM/yyyy HH:mm:ss");

            chkDaXuatHD.Checked = Convert.ToBoolean(drReceipt["IsExportedInVoice"]);
            chkDaThuTien.Checked = Convert.ToBoolean(drReceipt["DaThuTien"]);
            chkTrongGoiKham.Checked = Convert.ToBoolean(drReceipt["TrongGoiKham"]);

            if (drReceipt["LyDoGiam"] != null && drReceipt["LyDoGiam"] != DBNull.Value)
                txtLyDoGiam.Text = drReceipt["LyDoGiam"].ToString();

            cboHinhThucThanhToan.SelectedIndex = Convert.ToInt32(drReceipt["HinhThucThanhToan"]);

            lbTotalPrice.Text = "Tổng tiền: 0 (VNĐ)";
            
            Result result = ReceiptBus.GetReceiptDetailList(drReceipt["receiptGUID"].ToString());
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                dgReceiptDetail.DataSource = dt;

                double totalPrice = 0;
                foreach (DataRow row in dt.Rows)
                {
                    double amount = Convert.ToDouble(row["Amount"]);
                    totalPrice += amount;
                }

                if (totalPrice > 0)
                    lbTotalPrice.Text = string.Format("Tổng tiền: {0:#,###} (VNĐ)", totalPrice);
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.GetReceiptDetailList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.GetReceiptDetailList"));
            }
        }

        private void OnPrint()
        {
            if (MsgBox.Question(Application.ProductName, "Bạn có muốn in phiếu thu ?") == DialogResult.Yes)
            {
                string exportFileName = string.Format("{0}\\Temp\\Receipt.xls", Application.StartupPath);

                try
                {
                    string receiptGUID = _drReceipt["ReceiptGUID"].ToString();
                    if (ExportExcel.ExportReceiptToExcel(exportFileName, receiptGUID))
                    {
                        if (_printDialog.ShowDialog() == DialogResult.OK)
                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.PhieuThuDichVuTemplate));
                    }
                    else
                        return;
                }
                catch (Exception ex)
                {
                    MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                }
            }
        }

        private void OnExportInvoice()
        {
            List<DataRow> receiptList = new List<DataRow>();
            receiptList.Add(_drReceipt);
            dlgInvoiceInfo2 dlg = new dlgInvoiceInfo2(receiptList);
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _isExportedInvoice = true;
                btnExportInvoice.Enabled = false;
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgReceiptDetail_Load(object sender, EventArgs e)
        {
            if (Global.StaffType == StaffType.Admin)
            {
                chkDaXuatHD.Enabled = true;
                chkDaThuTien.Enabled = true;
                cboHinhThucThanhToan.Enabled = true;
                chkTrongGoiKham.Enabled = true;
                btnOK.Enabled = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint();
        }

        private void btnExportInvoice_Click(object sender, EventArgs e)
        {
            OnExportInvoice();
        }

        private void dlgReceiptDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Global.StaffType == StaffType.Admin)
            {
                if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    Result result = ReceiptBus.CapNhatTrangThaiPhieuThu(_drReceipt["ReceiptGUID"].ToString(), chkDaXuatHD.Checked, 
                        chkDaThuTien.Checked, (byte)cboHinhThucThanhToan.SelectedIndex, chkTrongGoiKham.Checked);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.CapNhatTrangThaiPhieuThu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.CapNhatTrangThaiPhieuThu"));
                        e.Cancel = true;
                    }
                    else
                    {
                        _drReceipt["IsExportedInVoice"] = chkDaXuatHD.Checked;
                        _drReceipt["DaThuTien"] = chkDaThuTien.Checked;
                        _drReceipt["HinhThucThanhToan"] = (byte)cboHinhThucThanhToan.SelectedIndex;
                        _drReceipt["HinhThucThanhToanStr"] = cboHinhThucThanhToan.Text;
                        _drReceipt["TrongGoiKham"] = chkTrongGoiKham.Checked;
                    }
                }
            }
        }
        #endregion
    }
}
