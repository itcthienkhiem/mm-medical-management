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
using MM.Dialogs;
using MM.Exports;
using System.IO;
using SpreadsheetGear;

namespace MM.Controls
{
    public partial class uNhatKyLienHeCongTy : uBase
    {
        #region Members
        //private bool _isFromDateToDate = true;
        private string _tenBenhNhan = string.Empty;
        private string _tenNguoiTao = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private int _type = 0; //0: From date to date; 1: Tên bệnh nhân; 2: Tên người tạo
        #endregion

        #region Constructor
        public uNhatKyLienHeCongTy()
        {
            InitializeComponent();
            dtpkDenNgay.Value = DateTime.Now;
            dtpkTuNgay.Value = DateTime.Now.AddDays(-30);
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        public void ClearData()
        {
            dgNhatKyLienHeCongTy.DataSource = null;
        }

        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
            btnPrint.Enabled = AllowPrint;
            btnPrintPreview.Enabled = AllowPrint;

            if (AllowExportAll)
                btnExportExcel.Enabled = true;
            else
                btnExportExcel.Enabled = AllowExport;

            btnImportExcel.Enabled = AllowImport;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();

                //if (raTenBenhNhan.Checked && txtTenBenhNhan.Text.Trim() == string.Empty)
                //{
                //    MsgBox.Show(Application.ProductName, "Vui lòng nhập tên công ty cần tìm.", IconType.Information);
                //    txtTenBenhNhan.Focus();
                //    return;
                //}

                //if (raTenNguoiTao.Checked && txtTenNguoiTao.Text.Trim() == string.Empty)
                //{
                //    MsgBox.Show(Application.ProductName, "Vui lòng nhập tên người tạo cần tìm.", IconType.Information);
                //    txtTenNguoiTao.Focus();
                //    return;
                //}

                chkChecked.Checked = false;
                if (raTuNgayToiNgay.Checked) _type = 0;
                else if (raTenBenhNhan.Checked) _type = 1;
                else _type = 2;
                _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                _tenBenhNhan = txtTenBenhNhan.Text;
                _tenNguoiTao = txtTenNguoiTao.Text;

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayNhatKyLienHeCongTyListProc));
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

        private void OnDisplayNhatKyLienHeCongTyList()
        {
            Result result = NhatKyLienHeCongTyBus.GetNhatKyLienHeCongTyList(_type, _fromDate, _toDate, _tenBenhNhan, _tenNguoiTao);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgNhatKyLienHeCongTy.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("NhatKyLienHeCongTyBus.GetNhatKyLienHeCongTyList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("NhatKyLienHeCongTyBus.GetNhatKyLienHeCongTyList"));
            }
        }

        private void OnAdd()
        {
            dlgAddNhatKyKienHeCongTy dlg = new dlgAddNhatKyKienHeCongTy();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnEdit()
        {
            if (dgNhatKyLienHeCongTy.SelectedRows == null || dgNhatKyLienHeCongTy.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 nhật ký liên hệ.", IconType.Information);
                return;
            }

            DataRow drNhatKyLienHeCongTy = (dgNhatKyLienHeCongTy.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddNhatKyKienHeCongTy dlg = new dlgAddNhatKyKienHeCongTy(drNhatKyLienHeCongTy);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnDelete()
        {
            List<string> deletedSpecList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgNhatKyLienHeCongTy.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    string userGUID = row["CreatedBy"].ToString();
                    if (userGUID != Global.UserGUID)
                    {
                        MsgBox.Show(Application.ProductName, "Bạn không thể xóa nhật ký liên hệ của người khác. Vui lòng kiểm tra lại.", IconType.Information);
                        return;
                    }

                    deletedSpecList.Add(row["NhatKyLienHeCongTyGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedSpecList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những nhật ký liên hệ mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = NhatKyLienHeCongTyBus.DeleteNhatKyLienHeCongTy(deletedSpecList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("NhatKyLienHeCongTyBus.DeleteNhatKyLienHeCongTy"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("NhatKyLienHeCongTyBus.DeleteNhatKyLienHeCongTy"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những nhật ký liên hệ cần xóa.", IconType.Information);
        }

        private List<DataRow> GetCheckedRows()
        {
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgNhatKyLienHeCongTy.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    checkedRows.Add(row);
                }
            }

            return checkedRows;
        }

        private void OnPrint(bool isPreview)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = GetCheckedRows();

            if (!AllowExportAll)
            {
                foreach (DataRow row in checkedRows)
                {
                    string userGUID = row["CreatedBy"].ToString();
                    if (userGUID != Global.UserGUID)
                    {
                        MsgBox.Show(Application.ProductName, "Bạn chỉ có thể in được liên hệ công ty do mình tạo. Vui lòng chọn lại.", IconType.Information);
                        return;
                    }
                }
            }

            if (checkedRows.Count > 0)
            {
                string exportFileName = string.Format("{0}\\Temp\\NhatKyLienHeCongTy.xls", Application.StartupPath);
                if (isPreview)
                {
                    if (!ExportExcel.ExportNhatKyLienHeCongTyToExcel(exportFileName, checkedRows))
                        return;
                    else
                    {
                        try
                        {
                            ExcelPrintPreview.PrintPreview(exportFileName);
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                            return;
                        }
                    }
                }
                else
                {
                    if (_printDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (!ExportExcel.ExportNhatKyLienHeCongTyToExcel(exportFileName, checkedRows))
                            return;
                        else
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
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những nhật ký liên hệ cần in.", IconType.Information);
        }

        private void OnExportToExcel()
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = GetCheckedRows();

            if (!AllowExportAll)
            {
                foreach (DataRow row in checkedRows)
                {
                    string userGUID = row["CreatedBy"].ToString();
                    if (userGUID != Global.UserGUID)
                    {
                        MsgBox.Show(Application.ProductName, "Bạn chỉ có thể xuất được liên hệ công ty do mình tạo. Vui lòng chọn lại.", IconType.Information);
                        return;
                    }
                }
            }

            if (checkedRows.Count > 0)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (!ExportExcel.ExportNhatKyLienHeCongTyToExcel(dlg.FileName, checkedRows))
                        return;
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những nhật ký liên hệ cần xuất excel.", IconType.Information);
        }
        private void OnImportExcel()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Import Excel";
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            string filename;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                filename = dlg.FileName;
                ImportNhatKyFromExcel(filename);
            }

        }
        private bool CheckNhatKyTemplate(IWorksheet ws, ref string message)
        {
            string s = string.Format("Sheet {0} không đúng định dạng nên không được nhập", ws.Name) + System.Environment.NewLine;
            try
            {
                if (ws.Cells[0, 0].Value != null && ws.Cells[0, 0].Value.ToString().ToLower() != "company name" ||
                        ws.Cells[0, 1].Value != null && ws.Cells[0, 1].Value.ToString().ToLower() != "district" ||
                        ws.Cells[0, 2].Value != null && ws.Cells[0, 2].Value.ToString().ToLower() != "person contact" ||
                        ws.Cells[0, 3].Value != null && ws.Cells[0, 3].Value.ToString().ToLower() != "tel" ||
                        ws.Cells[0, 4].Value != null && ws.Cells[0, 4].Value.ToString().ToLower() != "quantity" ||
                        ws.Cells[0, 5].Value != null && ws.Cells[0, 5].Value.ToString().ToLower() != "check-up month" ||
                        ws.Cells[0, 6].Value != null && ws.Cells[0, 6].Value.ToString().ToLower() != "feedback" ||
                        ws.Cells[0, 7].Value != null && ws.Cells[0, 7].Value.ToString().ToLower() != "email" ||
                        ws.Cells[0, 8].Value != null && ws.Cells[0, 8].Value.ToString().ToLower() != "contact date")
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
        private string ReFormatDate(string type, string value)
        {
            string sRet = value;
            if (type.ToLower().StartsWith("m"))
            {
                char[] split = new char[] { '/' };
                string[] sTemp = value.Split(split, StringSplitOptions.None);
                if (sTemp.Count() == 3)
                {
                    sRet = sTemp[1] + "/" + sTemp[0] + "/" + sTemp[2];
                }
                else
                {
                    sRet = value;
                }
            }
            return sRet;
        }
        private void ImportNhatKyFromExcel(string filename)
        {
            string message = "Nhập dữ liệu từ Excel hoàn tất." + System.Environment.NewLine;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (File.Exists(filename))
                {
                    IWorkbook book = SpreadsheetGear.Factory.GetWorkbook(filename);

                    foreach (IWorksheet sheet in book.Worksheets)
                    {
                        if (CheckNhatKyTemplate(sheet, ref message))
                        {
                            int RowCount = sheet.UsedRange.RowCount + 1;
                            int ColumnCount = sheet.UsedRange.ColumnCount + 1;
                            for (int i = 1; i < RowCount; i++)
                            {
                                NhatKyLienHeCongTy diary = new NhatKyLienHeCongTy();
                                for (int j = 0; j < ColumnCount; j++)
                                {
                                    string curCellValue = string.Empty;
                                    if (sheet.Cells[i, j] != null && sheet.Cells[i, j].Value != null && sheet.Cells[i, j].Text != null)
                                    {
                                        curCellValue = sheet.Cells[i, j].Text.Trim();
                                    }
                                    //process NULL text in excel 
                                    if (curCellValue.ToUpper() == "NULL")
                                        curCellValue = "";

                                    string sType = sheet.Cells[i, j].NumberFormat.Trim();
                                    if (sType.Contains("yy"))
                                    {
                                        curCellValue = ReFormatDate(sType, curCellValue);
                                    }
                                    //process "'" character
                                    curCellValue = curCellValue.Replace("'", "''");
                                    if (sheet.Cells[i, j].Font.Name.ToLower().IndexOf("vni") == 0)
                                        curCellValue = Utility.ConvertVNI2Unicode(curCellValue);
                                    if (sheet.Cells[0, j].Value != null && sheet.Cells[0, j].Value.ToString().Trim() != null)
                                    {
                                        switch (sheet.Cells[0, j].Value.ToString().Trim().ToLower())
                                        {
                                            case "district":
                                                diary.DiaChi = curCellValue;
                                                break;

                                            case "company name":
                                                diary.CongTyLienHe = curCellValue;
                                                break;

                                            case "tel":
                                                diary.SoDienThoaiLienHe = curCellValue;
                                                break;

                                            case "quantity":
                                                diary.SoNguoiKham = curCellValue;
                                                break;

                                            case "check-up month":
                                                diary.ThangKham = curCellValue;
                                                break;
                                            //
                                            case "feedback":
                                                diary.NoiDungLienHe = curCellValue;
                                                break;
                                            case "email":
                                                diary.Email = curCellValue;
                                                break;
                                            case "contact date":
                                                DateTime dt = new DateTime();
                                                if (DateTime.TryParse(curCellValue, out dt))
                                                {
                                                    diary.CreatedDate = dt;
                                                    diary.NgayGioLienHe = dt;
                                                }
                                                else
                                                {
                                                    diary.CreatedDate = DateTime.Now;
                                                    diary.NgayGioLienHe = DateTime.Now;
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }

                                }

                                //add to db
                                if (Global.StaffType != StaffType.Admin)
                                    diary.DocStaffGUID = Guid.Parse(Global.UserGUID);
                                else
                                    diary.DocStaffGUID = null;

                                diary.CreatedBy = Guid.Parse(Global.UserGUID);
                                diary.Note = "Import from Excel on " + DateTime.Now.ToString("dd/MM/yyyy");
                                Result result = NhatKyLienHeCongTyBus.InsertNhatKyLienHeCongTy(diary);
                                if (!result.IsOK)
                                {
                                    MsgBox.Show(this.Text, result.GetErrorAsString("NhatKyLienHeCongTyBus.InsertNhatKyLienHeCongTy"), IconType.Error);
                                    Utility.WriteToTraceLog(result.GetErrorAsString("NhatKyLienHeCongTyBus.InsertNhatKyLienHeCongTy"));
                                }
                            }
                        }
                    }
                }

                MsgBox.Show(Application.ProductName, message, IconType.Information);
                DisplayAsThread();
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
        }
        #endregion

        #region Window Event Handlers
        private void raTuNgayToiNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpkTuNgay.Enabled = raTuNgayToiNgay.Checked;
            dtpkDenNgay.Enabled = raTuNgayToiNgay.Checked;
            //txtTenBenhNhan.ReadOnly = raTuNgayToiNgay.Checked;
        }

        private void raTenBenhNhan_CheckedChanged(object sender, EventArgs e)
        {
            txtTenBenhNhan.ReadOnly = !raTenBenhNhan.Checked;
        }

        private void raTenNguoiTao_CheckedChanged(object sender, EventArgs e)
        {
            txtTenNguoiTao.ReadOnly = !raTenNguoiTao.Checked;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgNhatKyLienHeCongTy.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void dgYKienKhachHang_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDelete();
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
            OnExportToExcel();
        }
        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            OnImportExcel();
        }
        #endregion

        #region Working Thread
        private void OnDisplayNhatKyLienHeCongTyListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayNhatKyLienHeCongTyList();
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
