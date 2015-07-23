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
using MM.Bussiness;

namespace MM.Controls
{
    public partial class uThongKeChiDinhCuaBacSi : uBase
    {
        #region Constructor
        public uThongKeChiDinhCuaBacSi()
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

            //DocStaff
            List<byte> staffTypes = new List<byte>();
            staffTypes.Add((byte)StaffType.BacSi);
            staffTypes.Add((byte)StaffType.BacSiSieuAm);
            staffTypes.Add((byte)StaffType.BacSiNgoaiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiNoiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiPhuKhoa);

            Result result = null;

            if (Global.UserGUID == Guid.Empty.ToString())
                result = DocStaffBus.GetDocStaffList(staffTypes);
            else
                result = DocStaffBus.GetDocStaffList(staffTypes, Global.UserGUID);

            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
                return;
            }
            else
            {
                cboDocStaff.DataSource = result.QueryResult;
            }
        }

        private void OnPrint(bool isPreview)
        {
            Cursor.Current = Cursors.WaitCursor;
            string template = string.Empty;

            DateTime tuNgay = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
            DateTime denNgay = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
            string docStaffGUID = cboDocStaff.SelectedValue.ToString();

            string exportFileName = string.Format("{0}\\Temp\\ThongKeChiDinhCuaBacSi.xls", Application.StartupPath);
            if (isPreview)
            {
                if (!ExportExcel.ExportChiDinhCuaBacSi(exportFileName, tuNgay, denNgay, docStaffGUID))
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
                    if (!ExportExcel.ExportChiDinhCuaBacSi(exportFileName, tuNgay, denNgay, docStaffGUID))
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
                DateTime tuNgay = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                DateTime denNgay = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                string docStaffGUID = cboDocStaff.SelectedValue.ToString();
                ExportExcel.ExportChiDinhCuaBacSi(dlg.FileName, tuNgay, denNgay, docStaffGUID);
            }
        }

        private bool CheckInfo()
        {
            if (cboDocStaff.SelectedValue == null || cboDocStaff.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ chỉ định", IconType.Information);
                cboDocStaff.Focus();
                return false;
            }

            return true;
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
