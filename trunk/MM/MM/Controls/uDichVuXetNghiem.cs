using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;
using SpreadsheetGear;

namespace MM.Controls
{
    public partial class uDichVuXetNghiem : uBase
    {
        #region Members
        private DateTime _tuNgay = DateTime.Now;
        private DateTime _denNgay = DateTime.Now;
        private string _tenCongTy = string.Empty;
        private string _tenKhachHang = string.Empty;
        private string _tenDichVu = string.Empty;
        #endregion

        #region Contructor
        public uDichVuXetNghiem()
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
            DataTable dt = cboTenCongTy.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
            }

            dt = cboTenKhachHang.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
            }

            dt = cboTenDichVu.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
            }

            Result result = ThucHienXetNghiemBus.GetDanhSachCongTy();
            if (result.IsOK)
            {
                dt = result.QueryResult  as DataTable;
                DataRow newRow = dt.NewRow();
                newRow[0] = string.Empty;
                dt.Rows.InsertAt(newRow, 0);
                cboTenCongTy.DataSource = dt;
                cboTenCongTy.DisplayMember = "TenCongTy";
                cboTenCongTy.ValueMember = "TenCongTy";
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ThucHienXetNghiemBus.GetDanhSachCongTy"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThucHienXetNghiemBus.GetDanhSachCongTy"));
                return;
            }

            result = ThucHienXetNghiemBus.GetDanhSachKhachHang();
            if (result.IsOK)
            {
                dt = result.QueryResult as DataTable;
                DataRow newRow = dt.NewRow();
                newRow[0] = string.Empty;
                dt.Rows.InsertAt(newRow, 0);
                cboTenKhachHang.DataSource = dt;
                cboTenKhachHang.DisplayMember = "TenKhachHang";
                cboTenKhachHang.ValueMember = "TenKhachHang";
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ThucHienXetNghiemBus.GetDanhSachKhachHang"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThucHienXetNghiemBus.GetDanhSachKhachHang"));
                return;
            }

            result = ThucHienXetNghiemBus.GetDanhSachDichVu();
            if (result.IsOK)
            {
                dt = result.QueryResult as DataTable;
                DataRow newRow = dt.NewRow();
                newRow[0] = string.Empty;
                dt.Rows.InsertAt(newRow, 0);
                cboTenDichVu.DataSource = dt;
                cboTenDichVu.DisplayMember = "TenDichVu";
                cboTenDichVu.ValueMember = "TenDichVu";
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ThucHienXetNghiemBus.GetDanhSachDichVu"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThucHienXetNghiemBus.GetDanhSachDichVu"));
                return;
            }
        }

        private void UpdateGUI()
        {
            btnOK.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
            btnPrint.Enabled = AllowPrint;
            btnPrintPreview.Enabled = AllowPrint;
            btnExportExcel.Enabled = AllowExport;
            btnImportExcel.Enabled = AllowImport;

            luuToolStripMenuItem.Enabled = AllowEdit;
            xoaToolStripMenuItem.Enabled = AllowDelete;
            printPreviewToolStripMenuItem.Enabled = AllowPrint;
            printToolStripMenuItem.Enabled = AllowPrint;
            xuatExcelToolStripMenuItem.Enabled = AllowExport;
            nhapExcelToolStripMenuItem.Enabled = AllowImport;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
                chkChecked.Checked = false;
                _tuNgay = dtpkTuNgay.Value;
                _denNgay = dtpkDenNgay.Value;
                _tenCongTy = cboTenCongTy.Text;
                _tenKhachHang = cboTenKhachHang.Text;
                _tenDichVu = cboTenDichVu.Text;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayDichVuXetNghiemListProc));
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

        private void OnDisplayDichVuXetNghiemList()
        {
            Result result = ThucHienXetNghiemBus.GetDichVuXetNghiemList(_tuNgay, _denNgay, _tenCongTy, _tenKhachHang, _tenDichVu);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    DataTable dt = result.QueryResult as DataTable;
                    dgXetNghiem.DataSource = result.QueryResult;
                    lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dt.Rows.Count);
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ThucHienXetNghiemBus.GetDichVuXetNghiemList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThucHienXetNghiemBus.GetDichVuXetNghiemList"));
            }
        }

        public void ClearData()
        {
            DataTable dt = dgXetNghiem.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgXetNghiem.DataSource = null;
            }
        }

        private void OnUpdateGiaXetNghiem()
        {

        }

        private void OnDelete()
        {

        }

        private bool CheckTemplate(IWorksheet ws, ref string message)
        {
            string s = string.Format("Sheet {0} không đúng định dạng nên không được nhập", ws.Name) + System.Environment.NewLine;
            try
            {
                if (ws.Cells["B6"].Text.ToLower().Trim() != "stt" ||
                        ws.Cells["D6"].Text.ToLower().Trim() != "ngày th" ||
                        ws.Cells["E6"].Text.ToLower().Trim() != "tên công ty" ||
                        ws.Cells["F6"].Text.ToLower().Trim() != "tên khách hàng" ||
                        ws.Cells["I6"].Text.ToLower().Trim() != "tên dịch vụ" ||
                        ws.Cells["J6"].Text.ToLower().Trim() != "số tiền")
                {
                    message += s;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                message += s;
                Utility.WriteToTraceLog(ex.Message);
                return false;
            }
        }

        private void OnImportExcel()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Import Excel";
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                int row = 7;
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this.Refresh();
                    IWorkbook book = SpreadsheetGear.Factory.GetWorkbook(dlg.FileName);
                    if (book.Worksheets.Count <= 0) return;

                    IWorksheet workSheet = book.Worksheets[0];
                    string msg = "Nhập dữ liệu từ Excel hoàn tất." + System.Environment.NewLine;
                    if (!CheckTemplate(workSheet, ref msg))
                    {
                        MsgBox.Show(Application.ProductName, msg, IconType.Information);
                        return;
                    }
                   
                    while (true)
                    {
                        IRange range = workSheet.Cells[string.Format("B{0}", row)];
                        if (range.Value == null || range.Value.ToString().Trim() == string.Empty ||
                            range.Value.ToString().Trim().ToLower() == "tổng") 
                            break;

                        DateTime ngayThucHien = Convert.ToDateTime(workSheet.Cells[string.Format("D{0}", row)].Text);
                        string tenCongTy = workSheet.Cells[string.Format("E{0}", row)].Value.ToString().Trim();
                        string tenKhachHang = workSheet.Cells[string.Format("F{0}", row)].Value.ToString().Trim();
                        string tenDichVu = workSheet.Cells[string.Format("I{0}", row)].Value.ToString().Trim();
                        double soTien = Convert.ToDouble(workSheet.Cells[string.Format("J{0}", row)].Value);
                        ThucHienXetNghiem thxn = new ThucHienXetNghiem();
                        thxn.NgayThucHien = ngayThucHien;
                        thxn.TenCongTy = tenCongTy;
                        thxn.TenKhachHang = tenKhachHang;
                        thxn.TenDichVu = tenDichVu;
                        thxn.SoTien = soTien;
                        thxn.SourcePath = dlg.FileName;
                        Result result = ThucHienXetNghiemBus.InsertDichVuXetNghiem(thxn);
                        if (!result.IsOK)
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("ThucHienXetNghiemBus.InsertDichVuXetNghiem"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("ThucHienXetNghiemBus.InsertDichVuXetNghiem"));
                            return;
                        }

                        row++;
                    }

                    MsgBox.Show(Application.ProductName, msg, IconType.Information);
                }
                catch (Exception ex)
                {
                    MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                    Utility.WriteToTraceLog(ex.Message);
                }
            }
        }

        private void OnExportExcel()
        {

        }

        private void OnPrint(bool isPreview)
        {

        }
        #endregion

        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgXetNghiem.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OnUpdateGiaXetNghiem();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDelete();
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            OnImportExcel();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void luuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnUpdateGiaXetNghiem();
        }

        private void xoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDelete();
        }

        private void nhapExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnImportExcel();
        }

        private void xuatExcelToolStripMenuItem_Click(object sender, EventArgs e)
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
        #endregion

        #region Working Thread
        private void OnDisplayDichVuXetNghiemListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayDichVuXetNghiemList();
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
