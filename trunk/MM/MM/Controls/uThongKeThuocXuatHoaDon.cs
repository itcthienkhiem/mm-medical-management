using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Exports;

namespace MM.Controls
{
    public partial class uThongKeThuocXuatHoaDon : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uThongKeThuocXuatHoaDon()
        {
            InitializeComponent();
            dtpkTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            dtpkDenNgay.Value = DateTime.Now;
        }
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
            string template = Const.ThongKeThuocXuatHoaDonTemplate;

            string exportFileName = string.Format("{0}\\Temp\\ThongKeThuocXuatHoaDon.xls", Application.StartupPath);
            if (isPreview)
            {
                if (!ExportExcel.ExportThuocXuatHoaDonToExcel(exportFileName, dtpkTuNgay.Value, dtpkDenNgay.Value))
                    return;

                try
                {
                    ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(template));
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
                    if (!ExportExcel.ExportThuocXuatHoaDonToExcel(exportFileName, dtpkTuNgay.Value, dtpkDenNgay.Value))
                        return;

                    try
                    {
                        ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(template));
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
                ExportExcel.ExportThuocXuatHoaDonToExcel(dlg.FileName, dtpkTuNgay.Value, dtpkDenNgay.Value);
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
