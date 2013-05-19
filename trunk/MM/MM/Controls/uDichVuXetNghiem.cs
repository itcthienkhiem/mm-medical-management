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
using MM.Exports;

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
        private double _tongTien = 0;
        private double _currentSoTien = 0;
        private DataTable _dtSource = null;
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
            string tenCongTy = cboTenCongTy.Text;
            string tenKhachHang = cboTenKhachHang.Text;
            string tenDichVu = cboTenDichVu.Text;

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
                cboTenCongTy.Text = tenCongTy;
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
                cboTenKhachHang.Text = tenKhachHang;
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
                cboTenDichVu.Text = tenDichVu;
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
            soTienDataGridViewTextBoxColumn.ReadOnly = !AllowEdit;
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

                    result = ThucHienXetNghiemBus.GetTongTien(_tuNgay, _denNgay, _tenCongTy, _tenKhachHang, _tenDichVu);
                    if (result.IsOK)
                    {
                        _tongTien = Convert.ToDouble(result.QueryResult);
                        UpdateTongTien(_tongTien);
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ThucHienXetNghiemBus.GetTongTien"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ThucHienXetNghiemBus.GetTongTien"));
                    }
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
            List<string> keyList = new List<string>();
            DataTable dt = dgXetNghiem.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                    keyList.Add(row["ThucHienXetNghiemGUID"].ToString());
            }

            if (keyList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những dịch vụ xét nghiệm mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = ThucHienXetNghiemBus.DeleteDichVuXetNghiem(keyList);
                    if (result.IsOK)
                    {
                        InitData();
                        DisplayAsThread();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ThucHienXetNghiemBus.DeleteDichVuXetNghiem"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ThucHienXetNghiemBus.DeleteDichVuXetNghiem"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những dịch vụ xét nghiệm cần xóa.", IconType.Information);
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

        private void OnImportExcel(string fileName)
        {
            int row = 7;
            try
            {
                IWorkbook book = SpreadsheetGear.Factory.GetWorkbook(fileName);
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
                    thxn.SourcePath = fileName;
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
                InitData();
                DisplayAsThread();
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
        }

        private void OnImportExcelAsThread()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Import Excel";
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.Refresh();
                    ThreadPool.QueueUserWorkItem(new WaitCallback(OnImportExcelProc), dlg.FileName);
                    base.ShowWaiting();
                }
                catch (Exception ex)
                {
                    MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                    Utility.WriteToTraceLog(ex.Message);
                }
                finally
                {
                    base.HideWaiting();
                }
            }
        }

        private void UpdateTongTien(double tongTien)
        {
            if (tongTien != 0)
                lbTongTien.Text = string.Format("Tổng tiền: {0} VNĐ", tongTien.ToString("#,###"));
            else
                lbTongTien.Text = "Tổng tiền: 0 VNĐ";
        }

        private void OnExportExcelAsThread()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export Excel";
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.Refresh();
                    _dtSource = dgXetNghiem.DataSource as DataTable;
                    ThreadPool.QueueUserWorkItem(new WaitCallback(OnExportExcelProc), dlg.FileName);
                    base.ShowWaiting();
                }
                catch (Exception ex)
                {
                    MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                    Utility.WriteToTraceLog(ex.Message);
                }
                finally
                {
                    base.HideWaiting();
                }
            }
        }

        private void OnExportExcel(string fileName)
        {
            try
            {
                if (_dtSource == null || _dtSource.Rows.Count <= 0) return;
                ExportExcel.ExportDichVuXetNghiemToExcel(fileName, _tuNgay, _denNgay, _dtSource);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
        }

        private void OnPrintAsThread(bool isPreview)
        {
            try
            {
                this.Refresh();
                _dtSource = dgXetNghiem.DataSource as DataTable;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnPrintProc), isPreview);
                base.ShowWaiting();
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnPrint(bool isPreview)
        {
            try
            {
                string exportFileName = string.Format("{0}\\Temp\\DichVuXetNghiem.xls", Application.StartupPath);
                if (isPreview)
                {
                    if (ExportExcel.ExportDichVuXetNghiemToExcel(exportFileName, _tuNgay, _denNgay, _dtSource))
                    {
                        try
                        {
                            MethodInvoker method = delegate
                            {
                                ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.PhieuThuThuocTemplate));
                            };
                            if (InvokeRequired) BeginInvoke(method);
                            else method.Invoke();
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                        }
                    }
                }
                else
                {
                    if (ExportExcel.ExportDichVuXetNghiemToExcel(exportFileName, _tuNgay, _denNgay, _dtSource)) 
                    {
                        MethodInvoker method = delegate
                        {
                            if (_printDialog.ShowDialog() == DialogResult.OK)
                            {
                                try
                                {
                                    ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.PhieuThuThuocTemplate));
                                }
                                catch (Exception ex)
                                {
                                    MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                }
                            }
                        };
                        if (InvokeRequired) BeginInvoke(method);
                        else method.Invoke();
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
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
            OnImportExcelAsThread();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportExcelAsThread();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            OnPrintAsThread(true);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrintAsThread(false);
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
            OnImportExcelAsThread();
        }

        private void xuatExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExportExcelAsThread();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrintAsThread(true);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrintAsThread(false);
        }

        private void dgXetNghiem_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex != 5) return;
            if (e.RowIndex < 0) return;

            try
            {
                double soTien = Convert.ToDouble(e.FormattedValue);
                if (soTien != _currentSoTien)
                {
                    _tongTien = _tongTien - _currentSoTien + soTien;
                    UpdateTongTien(_tongTien);
                    DataRow row = (dgXetNghiem.Rows[e.RowIndex].DataBoundItem as DataRowView).Row;
                    string key = row["ThucHienXetNghiemGUID"].ToString();
                    Result result = ThucHienXetNghiemBus.UpdateGiaDichVuXetNghiem(key, soTien);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ThucHienXetNghiemBus.UpdateGiaDichVuXetNghiem"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ThucHienXetNghiemBus.UpdateGiaDichVuXetNghiem"));
                    }
                }
            }
            catch
            {
                e.Cancel = true;
            }
        }

        private void dgXetNghiem_SelectionChanged(object sender, EventArgs e)
        {
            if (dgXetNghiem.SelectedRows == null || dgXetNghiem.SelectedRows.Count <= 0) return;
            if (dgXetNghiem.SelectedRows[0].Cells[5].Value != null && dgXetNghiem.SelectedRows[0].Cells[5].Value != DBNull.Value)
                _currentSoTien = Convert.ToDouble(dgXetNghiem.SelectedRows[0].Cells[5].Value);
            else
                _currentSoTien = 0;
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

        private void OnImportExcelProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnImportExcel(state.ToString());
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

        private void OnExportExcelProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnExportExcel(state.ToString());
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

        private void OnPrintProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnPrint(Convert.ToBoolean(state));
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
