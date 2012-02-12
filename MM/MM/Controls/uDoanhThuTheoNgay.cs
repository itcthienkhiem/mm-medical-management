using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Exports;
using MM.Dialogs;
using MM.Common;

namespace MM.Controls
{
    public partial class uDoanhThuTheoNgay : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uDoanhThuTheoNgay()
        {
            InitializeComponent();
            dtpkNgay.Value = DateTime.Now;
        }
        #endregion

        #region UI Command
        private void OnPrint(bool isPreview)
        {
            string exportFileName = string.Format("{0}\\Temp\\DoanhThuTheoNgay.xls", Application.StartupPath);
            if (isPreview)
            {
                DateTime tuNgay = new DateTime(dtpkNgay.Value.Year, dtpkNgay.Value.Month, dtpkNgay.Value.Day, 0, 0, 0);
                DateTime denNgay = new DateTime(dtpkNgay.Value.Year, dtpkNgay.Value.Month, dtpkNgay.Value.Day, 23, 59, 59);
                if (ExportExcel.ExportDoanhThuTheoNgayToExcel(exportFileName, tuNgay, denNgay))
                {
                    try
                    {
                        ExcelPrintPreview.PrintPreview(exportFileName);
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
                    DateTime tuNgay = new DateTime(dtpkNgay.Value.Year, dtpkNgay.Value.Month, dtpkNgay.Value.Day, 0, 0, 0);
                    DateTime denNgay = new DateTime(dtpkNgay.Value.Year, dtpkNgay.Value.Month, dtpkNgay.Value.Day, 23, 59, 59);
                    if (ExportExcel.ExportDoanhThuTheoNgayToExcel(exportFileName, tuNgay, denNgay))
                    {
                        try
                        {
                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName);
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
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export Excel";
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DateTime tuNgay = new DateTime(dtpkNgay.Value.Year, dtpkNgay.Value.Month, dtpkNgay.Value.Day, 0, 0, 0);
                DateTime denNgay = new DateTime(dtpkNgay.Value.Year, dtpkNgay.Value.Month, dtpkNgay.Value.Day, 23, 59, 59);
                ExportExcel.ExportDoanhThuTheoNgayToExcel(dlg.FileName, tuNgay, denNgay);
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
