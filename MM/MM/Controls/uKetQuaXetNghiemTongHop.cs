using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;
using MM.Dialogs;
using MM.Exports;

namespace MM.Controls
{
    public partial class uKetQuaXetNghiemTongHop : uBase
    {
        #region Members
        private Font _normalFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        private Font _boldFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        private Hashtable _htXN = new Hashtable();
        private string _patientGUID = string.Empty;
        private bool _flag = false;
        #endregion

        #region Constructor
        public uKetQuaXetNghiemTongHop()
        {
            InitializeComponent();
            dtpkTuNgay.Value = DateTime.Now;
            dtpkDenNgay.Value = DateTime.Now;
        }
        #endregion

        #region UI Command
        public void UpdateGUI()
        {
            btnAddChiTiet.Enabled = AllowAdd;
            btnEditChiTiet.Enabled = AllowEdit;
            btnDeleteChiTiet.Enabled = AllowDelete;

            btnPrintCellDyn3200.Enabled = AllowPrint;
            btnExportExcelCellDyn3200.Enabled = AllowExport;
            btnExportExcelSinhHoa.Enabled = AllowExport;
            btnUploadFTP.Enabled = AllowExport;

            btnPrintCellDyn.Enabled = AllowPrint;
            btnPrintSinhHoa.Enabled = AllowPrint;
        }

        public void DisplayDanhSachBenhNhan()
        {
            if (dtpkTuNgay.Value > dtpkDenNgay.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkTuNgay.Focus();
                return;
            }

            string tenBenhNhan = txtTenBenhNhan.Text;
            DateTime tuNgay = dtpkTuNgay.Value;
            DateTime denNgay = dtpkDenNgay.Value;

            Result result = KetQuaXetNghiemTongHopBus.GetDanhSachBenhNhanXetNghiemList(tuNgay, denNgay, tenBenhNhan, chkMaBenhNhan.Checked);
            if (result.IsOK)
            {
                _patientGUID = string.Empty;
                _htXN.Clear();

                dgBenhNhan.DataSource = result.QueryResult as DataTable;
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaXetNghiemTongHopBus.GetDanhSachBenhNhanXetNghiemList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTongHopBus.GetDanhSachBenhNhanXetNghiemList"));
            }
        }

        private void DisplayDanhSachXetNghiem(DataRow patientRow)
        {
            if (_patientGUID != string.Empty)
            {
                List<string> uncheckedList = null;
                if (!_htXN.ContainsKey(_patientGUID))
                {
                    uncheckedList = new List<string>();

                    DataTable dt = dgXetNghiem.DataSource as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (!Convert.ToBoolean(row["Checked"]))
                                uncheckedList.Add(row["ChiTietKQXNGUID"].ToString());
                        }
                    }

                    _htXN.Add(_patientGUID, uncheckedList);
                }
                else
                {
                    uncheckedList = (List<string>)_htXN[_patientGUID];
                    uncheckedList.Clear();

                    DataTable dt = dgXetNghiem.DataSource as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (!Convert.ToBoolean(row["Checked"]))
                                uncheckedList.Add(row["ChiTietKQXNGUID"].ToString());
                        }
                    }
                }
            }

            _patientGUID = patientRow["PatientGUID"].ToString();
            string ngaySinh = patientRow["DobStr"].ToString();
            string gioiTinh = patientRow["GenderAsStr"].ToString();
            DateTime tuNgay = dtpkTuNgay.Value;
            DateTime denNgay = dtpkDenNgay.Value;

            Result result = KetQuaXetNghiemTongHopBus.GetKetQuaXetNghiemTongHopList(tuNgay, denNgay, _patientGUID, ngaySinh, gioiTinh);
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                //dgXetNghiem.DataSource = result.QueryResult as DataTable;
                RefreshNgayXetNghiem(dt);
                
                
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaXetNghiemTongHopBus.GetKetQuaXetNghiemTongHopList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTongHopBus.GetKetQuaXetNghiemTongHopList"));
            }
        }

        private void RefreshHighlight(List<string> uncheckedList)
        {
            if (uncheckedList == null || uncheckedList.Count <= 0)
            {
                _flag = true;
                chkCheckedXN.Checked = true;
                _flag = false;
            }
            else
            {
                _flag = true;
                chkCheckedXN.Checked = false;
                _flag = false;
            }

            foreach (DataGridViewRow row in dgXetNghiem.Rows)
            {
                row.Cells["Checked"].Style.BackColor = Color.LightBlue;
                row.Cells["DaIn"].Style.BackColor = Color.LightBlue;
                row.Cells["DaUpload"].Style.BackColor = Color.LightBlue;
                row.Cells["LamThem"].Style.BackColor = Color.LightBlue;

                DataRow dr = (row.DataBoundItem as DataRowView).Row;
                string chiTietKQXNGUID = dr["ChiTietKQXNGUID"].ToString();

                if (uncheckedList != null && uncheckedList.Contains(chiTietKQXNGUID))
                    dr["Checked"] = false;
                else
                    dr["Checked"] = true;

                TinhTrang tinhTrang = (TinhTrang)Convert.ToByte(dr["TinhTrang"]);
                if (tinhTrang == MM.Common.TinhTrang.BatThuong)
                {
                    //row.DefaultCellStyle.Font = _boldFont;
                    //row.DefaultCellStyle.ForeColor = Color.Red;
                    row.Cells["TestResult"].Style.Font = _boldFont;
                    row.Cells["TestResult"].Style.ForeColor = Color.Red;
                }
                else
                {
                    row.Cells["TestResult"].Style.Font = _normalFont;
                    row.Cells["TestResult"].Style.ForeColor = Color.Black;
                    //row.DefaultCellStyle.Font = _normalFont;
                    //row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void RefreshNgayXetNghiem(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                dgXetNghiem.DataSource = dt;
                return;
            }

            if (!chkHienThiGioXetNghiem.Checked)
            {
                List<DataRow> results = (from p in dt.AsEnumerable()
                                         orderby p.Field<DateTime>("NgayXN"), p.Field<int>("GroupID"), p.Field<int>("Order")
                                         select p).ToList<DataRow>();

                DataTable newDataSource = dt.Clone();

                string ngay = string.Empty;
                foreach (DataRow row in results)
                {
                    string ngayXN = Convert.ToDateTime(row["NgayXN"]).ToString("dd/MM/yyyy");
                    if (ngay == ngayXN)
                        row["NgayXN2"] = DBNull.Value;
                    else
                        row["NgayXN2"] = ngayXN;

                    ngay = ngayXN;

                    newDataSource.ImportRow(row);
                }

                dt.Rows.Clear();
                dt = null;
                dgXetNghiem.DataSource =  newDataSource;
            }
            else
            {
                List<DataRow> results = (from p in dt.AsEnumerable()
                                         orderby p.Field<DateTime>("NgayXN"), p.Field<int>("GroupID"), p.Field<int>("Order")
                                         select p).ToList<DataRow>();

                DataTable newDataSource = dt.Clone();

                string ngay = string.Empty;
                foreach (DataRow row in results)
                {
                    string ngayXN = Convert.ToDateTime(row["NgayXN"]).ToString("dd/MM/yyyy");
                    if (ngay == ngayXN)
                        row["NgayXN2"] = Convert.ToDateTime(row["NgayXN"]).ToString("HH:mm:ss");
                    else
                        row["NgayXN2"] = Convert.ToDateTime(row["NgayXN"]).ToString("dd/MM/yyyy HH:mm:ss");

                    ngay = ngayXN;

                    newDataSource.ImportRow(row);
                }

                dt.Rows.Clear();
                dt = null;
                dgXetNghiem.DataSource = newDataSource;
            }

            List<string> uncheckedList = null;
            if (_htXN.ContainsKey(_patientGUID))
                uncheckedList = (List<string>)_htXN[_patientGUID];

            RefreshHighlight(uncheckedList);
        }

        private void UpdateUncheckedXetNghiem()
        {
            if (dgBenhNhan.SelectedRows == null || dgBenhNhan.SelectedRows.Count <= 0) return;
            DataRow patientRow = (dgBenhNhan.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (patientRow == null) return;

            string patientGUID = patientRow["PatientGUID"].ToString();
            List<string> uncheckedList = null;
            if (!_htXN.ContainsKey(patientGUID))
            {
                uncheckedList = new List<string>();

                DataTable dt = dgXetNghiem.DataSource as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (!Convert.ToBoolean(row["Checked"]))
                            uncheckedList.Add(row["ChiTietKQXNGUID"].ToString());
                    }
                }

                _htXN.Add(patientGUID, uncheckedList);
            }
            else
            {
                uncheckedList = (List<string>)_htXN[patientGUID];
                uncheckedList.Clear();

                DataTable dt = dgXetNghiem.DataSource as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (!Convert.ToBoolean(row["Checked"]))
                            uncheckedList.Add(row["ChiTietKQXNGUID"].ToString());
                    }
                }
            }
        }

        private List<DataRow> GetCheckedPatientRows()
        {
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgBenhNhan.DataSource as DataTable;
            if (dt == null) return checkedRows;

            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToBoolean(row["Checked"]))
                    checkedRows.Add(row);
            }

            return checkedRows;
        }

        private void ExportCellDyn3200ToExcel()
        {
            List<DataRow> checkedPatientRows = GetCheckedPatientRows();
            if (checkedPatientRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu ít nhất 1 bệnh nhân cần xuất excel.", IconType.Information);
                return;
            }

            UpdateUncheckedXetNghiem();

            DateTime tuNgay = dtpkTuNgay.Value;
            DateTime denNgay = dtpkDenNgay.Value;


            foreach (DataRow row in checkedPatientRows)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    List<string> uncheckedList = null;
                    string patientGUID = row["PatientGUID"].ToString();
                    if (_htXN.ContainsKey(patientGUID))
                        uncheckedList = (List<string>)_htXN[patientGUID];

                    bool isData = false;
                    DateTime maxNgayXN = DateTime.Now;
                    List<string> keys = null;
                    if (!ExportExcel.ExportKetQuaXetNghiemCellDyn3200ToExcel(dlg.FileName, row, tuNgay, denNgay, uncheckedList, false, true, ref isData, ref maxNgayXN, ref keys))
                        return;
                }
            }
        }

        private void PrintAll()
        {
            List<DataRow> checkedPatientRows = GetCheckedPatientRows();
            if (checkedPatientRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu ít nhất 1 bệnh nhân cần in.", IconType.Information);
                return;
            }

            UpdateUncheckedXetNghiem();

            DateTime tuNgay = dtpkTuNgay.Value;
            DateTime denNgay = dtpkDenNgay.Value;
            string exportFileName = string.Format("{0}\\Temp\\KetQuaXetNghiemCellDyn3200.xls", Application.StartupPath);
            if (_printDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (DataRow row in checkedPatientRows)
                {
                    List<string> uncheckedList = null;
                    string patientGUID = row["PatientGUID"].ToString();
                    if (_htXN.ContainsKey(patientGUID))
                        uncheckedList = (List<string>)_htXN[patientGUID];

                    bool isData = false;
                    DateTime maxNgayXN = DateTime.Now;
                    List<string> keys = null;
                    List<string> hitachi917Keys = null;
                    List<string> manualKeys = null;
                    if (!ExportExcel.ExportKetQuaXetNghiemCellDyn3200ToExcel(exportFileName, row, tuNgay, denNgay, uncheckedList, true, chkCoLine.Checked, ref isData, ref maxNgayXN, ref keys))
                        return;
                    else
                    {
                        try
                        {
                            if (isData)
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaXetNghiemCellDyn3200Template));
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                            return;
                        }
                    }

                    exportFileName = string.Format("{0}\\Temp\\KetQuaXetNghiemSinhHoa.xls", Application.StartupPath);
                    if (!ExportExcel.ExportKetQuaXetNghiemSinhToExcel(exportFileName, row, tuNgay, denNgay, uncheckedList, true, chkCoLine.Checked, ref isData, ref maxNgayXN, ref hitachi917Keys, ref manualKeys))
                        return;
                    else
                    {
                        try
                        {
                            if (isData)
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaXetNghiemCellDyn3200Template));
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                            return;
                        }
                    }
                }

                DataTable dt = dgXetNghiem.DataSource as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        bool isChecked = Convert.ToBoolean(row["Checked"]);
                        if (isChecked)
                            row["DaIn"] = true;
                    }
                }
            }
        }

        private void ExportSinhHoaToExcel()
        {
            List<DataRow> checkedPatientRows = GetCheckedPatientRows();
            if (checkedPatientRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu ít nhất 1 bệnh nhân cần xuất excel.", IconType.Information);
                return;
            }

            UpdateUncheckedXetNghiem();

            DateTime tuNgay = dtpkTuNgay.Value;
            DateTime denNgay = dtpkDenNgay.Value;

            foreach (DataRow row in checkedPatientRows)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    List<string> uncheckedList = null;
                    string patientGUID = row["PatientGUID"].ToString();
                    if (_htXN.ContainsKey(patientGUID))
                        uncheckedList = (List<string>)_htXN[patientGUID];

                    bool isData = false;
                    DateTime maxNgayXN = DateTime.Now;
                    List<string> hitachi917Keys = null;
                    List<string> manualKeys = null;
                    if (!ExportExcel.ExportKetQuaXetNghiemSinhToExcel(dlg.FileName, row, tuNgay, denNgay, uncheckedList, false, true, ref isData, ref maxNgayXN, ref hitachi917Keys, ref manualKeys))
                        return;
                }
            }
        }

        private void PrintSinhHoa()
        {
            List<DataRow> checkedPatientRows = GetCheckedPatientRows();
            if (checkedPatientRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu ít nhất 1 bệnh nhân cần in.", IconType.Information);
                return;
            }

            UpdateUncheckedXetNghiem();

            DateTime tuNgay = dtpkTuNgay.Value;
            DateTime denNgay = dtpkDenNgay.Value;
            string exportFileName = string.Format("{0}\\Temp\\KetQuaXetNghiemSinhHoa.xls", Application.StartupPath);
            if (_printDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (DataRow row in checkedPatientRows)
                {
                    List<string> uncheckedList = null;
                    string patientGUID = row["PatientGUID"].ToString();
                    if (_htXN.ContainsKey(patientGUID))
                        uncheckedList = (List<string>)_htXN[patientGUID];

                    bool isData = false;
                    DateTime maxNgayXN = DateTime.Now;
                    List<string> hitachi917Keys = null;
                    List<string> manualKeys = null;
                    if (!ExportExcel.ExportKetQuaXetNghiemSinhToExcel(exportFileName, row, tuNgay, denNgay, uncheckedList, true, chkCoLine.Checked, ref isData, ref maxNgayXN, ref hitachi917Keys, ref manualKeys))
                        return;
                    else
                    {
                        try
                        {
                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaXetNghiemSinhHoaTemplate));
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                            return;
                        }

                        break;
                    }
                }
            }
        }

        private void OnAddChiTiet()
        {
            DataRow patientRow = null;
            if (dgBenhNhan.SelectedRows != null && dgBenhNhan.SelectedRows.Count > 0)
            {
                patientRow = (dgBenhNhan.SelectedRows[0].DataBoundItem as DataRowView).Row;
            }

            dlgAddKetQuaXetNghiemTay dlg = new dlgAddKetQuaXetNghiemTay(patientRow);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayDanhSachBenhNhan();
            }
        }

        private void OnEditChiTiet()
        {
            if (dgXetNghiem.SelectedRows == null || dgXetNghiem.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 kết quả xét nghiệm để cập nhật.", IconType.Information);
                return;
            }

            DataRow drXetNghiem = (dgXetNghiem.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (drXetNghiem == null) return;

            string loaiXN = drXetNghiem["LoaiXN"].ToString();
            if (loaiXN == "Hitachi917")
            {
                dlgUpdateChiSoKetQuaXetNghiem dlg = new dlgUpdateChiSoKetQuaXetNghiem(drXetNghiem);
                dlg.IsTongHop = true;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    DataRow row = (dgBenhNhan.SelectedRows[0].DataBoundItem as DataRowView).Row;
                    DisplayDanhSachXetNghiem(row);
                }
            }
            else if (loaiXN == "CellDyn3200")
            {
                dlgUpdateChiSoKetQuaXetNghiem_CellDyn3200 dlg = new dlgUpdateChiSoKetQuaXetNghiem_CellDyn3200(drXetNghiem);
                dlg.IsTongHop = true;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    DataRow row = (dgBenhNhan.SelectedRows[0].DataBoundItem as DataRowView).Row;
                    DisplayDanhSachXetNghiem(row);
                }
            }
            else
            {
                dlgEditChiTietKetQuaXetNghiemTay dlg = new dlgEditChiTietKetQuaXetNghiemTay(drXetNghiem);
                dlg.IsTongHop = true;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    DataRow row = (dgBenhNhan.SelectedRows[0].DataBoundItem as DataRowView).Row;
                    DisplayDanhSachXetNghiem(row);
                }

            }
        }

        private void OnDeleteChiTiet()
        {
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgXetNghiem.DataSource as DataTable;
            if (dt == null) return;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    //deletedCTKQXNList.Add(row["ChiTietKQXNGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedRows.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những kết quả xét nghiệm bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    List<string> hitachi917Keys = new List<string>();
                    List<string> celldyn3200Keys = new List<string>();
                    List<string> manualKeys = new List<string>();
                    foreach (DataRow row in deletedRows)
                    {
                        string loaiXN = row["LoaiXN"].ToString();
                        if (loaiXN == "Hitachi917")
                            hitachi917Keys.Add(row["ChiTietKQXNGUID"].ToString());
                        else if (loaiXN == "CellDyn3200")
                            celldyn3200Keys.Add(row["ChiTietKQXNGUID"].ToString());
                        else
                            manualKeys.Add(row["ChiTietKQXNGUID"].ToString());
                    }

                    if (hitachi917Keys.Count > 0)
                    {
                        Result result = XetNghiem_Hitachi917Bus.DeleteChiTietKetQuaXetNghiem(hitachi917Keys);
                        if (!result.IsOK)
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_Hitachi917Bus.DeleteChiTietKetQuaXetNghiem"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_Hitachi917Bus.DeleteChiTietKetQuaXetNghiem"));
                        }
                    }

                    if (celldyn3200Keys.Count > 0)
                    {
                        Result result = XetNghiem_CellDyn3200Bus.DeleteChiTietKetQuaXetNghiem(celldyn3200Keys);
                        if (!result.IsOK)
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_CellDyn3200Bus.DeleteChiTietKetQuaXetNghiem"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_CellDyn3200Bus.DeleteChiTietKetQuaXetNghiem"));
                        }
                    }

                    if (manualKeys.Count > 0)
                    {
                        Result result = KetQuaXetNghiemTayBus.DeleteChiTietXetNghiem(manualKeys);
                        if (!result.IsOK)
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaXetNghiemTayBus.DeleteChiTietXetNghiem"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTayBus.DeleteChiTietXetNghiem"));
                        }
                    }

                    foreach (DataRow row in deletedRows)
                    {
                        dt.Rows.Remove(row);
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những kết quả xét nghiệm cần xóa.", IconType.Information);
        }

        private bool AddUserToTextFile(string customerId, string password, string name)
        {
            StreamWriter sw = null;

            string fileName = string.Format("{0}\\Users_{1}.txt", Global.UsersPath, DateTime.Now.ToString("dd_MM_yyyy"));

            try
            {
                sw = new StreamWriter(fileName, true);
                sw.WriteLine(string.Format("customer_id: {0}, password: {1}, name: {2}", customerId, password, name));
                return true;
            }
            catch (Exception e)
            {
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    sw = null;
                }
            }

            return false;
        }

        private void UploadKQXN()
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedPatientRows = GetCheckedPatientRows();
            if (checkedPatientRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu ít nhất 1 bệnh nhân cần upload.", IconType.Information);
                return;
            }

            if (MsgBox.Question(Application.ProductName, "Bạn có muốn upload kết quả xét nghiệm ?") == DialogResult.No)
                return;

            UpdateUncheckedXetNghiem();

            DateTime tuNgay = dtpkTuNgay.Value;
            DateTime denNgay = dtpkDenNgay.Value;
            string exportFileName = string.Format("{0}\\Temp\\KetQuaXetNghiemCellDyn3200.xls", Application.StartupPath);
            foreach (DataRow row in checkedPatientRows)
            {
                List<string> uncheckedList = null;
                string patientGUID = row["PatientGUID"].ToString();
                    

                if (_htXN.ContainsKey(patientGUID))
                    uncheckedList = (List<string>)_htXN[patientGUID];

                bool isData = false;
                DateTime maxNgayXN = DateTime.Now;
                List<string> cellDyn3200Keys = null;
                List<string> hitachi917Keys = null;
                List<string> manualKeys = null;

                if (!ExportExcel.ExportKetQuaXetNghiemCellDyn3200ToExcel(exportFileName, row, tuNgay, denNgay, uncheckedList, true, true, ref isData, ref maxNgayXN, ref cellDyn3200Keys))
                    return;
                else
                {
                    try
                    {
                        if (isData)
                        {
                            string maBenhNhan = row["FileNum"].ToString();
                            string tenBenhNhan = row["FullName"].ToString();
                            string password = Utility.GeneratePassword();

                            string remoteFileName = string.Format("{0}/{1}/{2}_{3}.xls", Global.FTPFolder, maBenhNhan,
                                maxNgayXN.ToString("ddMMyyyyHHmmss"), "CellDyn3200");

                            Result result = MySQLHelper.CheckUserExist(maBenhNhan);
                            if (result.Error.Code != ErrorCode.EXIST && result.Error.Code != ErrorCode.NOT_EXIST)
                            {
                                MsgBox.Show(Application.ProductName, result.GetErrorAsString("MySQLHelper.CheckUserExist"), IconType.Information);
                                Utility.WriteToTraceLog(result.GetErrorAsString("MySQLHelper.CheckUserExist"));
                                return;
                            }

                            if (result.Error.Code == ErrorCode.NOT_EXIST)
                            {
                                result = MySQLHelper.InsertUser(maBenhNhan, password, tenBenhNhan);
                                if (!result.IsOK)
                                {
                                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("MySQLHelper.InsertUser"), IconType.Information);
                                    Utility.WriteToTraceLog(result.GetErrorAsString("MySQLHelper.InsertUser"));
                                    return;
                                }
                            }
                            else
                                password = result.QueryResult.ToString();

                            result = UserBus.AddUser(maBenhNhan, password);
                            if (!result.IsOK)
                            {
                                MsgBox.Show(Application.ProductName, result.GetErrorAsString("UserBus.AddUser"), IconType.Information);
                                Utility.WriteToTraceLog(result.GetErrorAsString("UserBus.AddUser"));
                                return;
                            }

                            result = FTP.UploadFile(Global.FTPConnectionInfo, exportFileName, remoteFileName);
                            if (!result.IsOK)
                            {
                                MsgBox.Show(Application.ProductName, result.GetErrorAsString("FTP.UploadFile"), IconType.Information);
                                Utility.WriteToTraceLog(result.GetErrorAsString("FTP.UploadFile"));
                            }
                            else
                            {
                                result = XetNghiem_CellDyn3200Bus.UpdateDaUpload(cellDyn3200Keys);
                                if (!result.IsOK)
                                {
                                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_CellDyn3200Bus.UpdateDaUpload"), IconType.Information);
                                    Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_CellDyn3200Bus.UpdateDaUpload"));
                                } 
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                        return;
                    }
                }

                exportFileName = string.Format("{0}\\Temp\\KetQuaXetNghiemSinhHoa.xls", Application.StartupPath);
                if (!ExportExcel.ExportKetQuaXetNghiemSinhToExcel(exportFileName, row, tuNgay, denNgay, uncheckedList, true, true, ref isData, ref maxNgayXN, ref hitachi917Keys, ref manualKeys))
                    return;
                else
                {
                    try
                    {
                        if (isData)
                        {
                            string maBenhNhan = row["FileNum"].ToString();
                            string tenBenhNhan = row["FullName"].ToString();
                            string password = Utility.GeneratePassword();

                            string remoteFileName = string.Format("{0}/{1}/{2}_{3}.xls", Global.FTPFolder, maBenhNhan,
                                maxNgayXN.ToString("ddMMyyyyHHmmss"), "SinhHoa");

                            Result result = MySQLHelper.CheckUserExist(maBenhNhan);
                            if (result.Error.Code != ErrorCode.EXIST && result.Error.Code != ErrorCode.NOT_EXIST)
                            {
                                MsgBox.Show(Application.ProductName, result.GetErrorAsString("MySQLHelper.CheckUserExist"), IconType.Information);
                                Utility.WriteToTraceLog(result.GetErrorAsString("MySQLHelper.CheckUserExist"));
                                return;
                            }

                            if (result.Error.Code == ErrorCode.NOT_EXIST)
                            {
                                result = MySQLHelper.InsertUser(maBenhNhan, password, tenBenhNhan);
                                if (!result.IsOK)
                                {
                                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("MySQLHelper.InsertUser"), IconType.Information);
                                    Utility.WriteToTraceLog(result.GetErrorAsString("MySQLHelper.InsertUser"));
                                    return;
                                }
                            }
                            else
                                password = result.QueryResult.ToString();

                            result = UserBus.AddUser(maBenhNhan, password);
                            if (!result.IsOK)
                            {
                                MsgBox.Show(Application.ProductName, result.GetErrorAsString("UserBus.AddUser"), IconType.Information);
                                Utility.WriteToTraceLog(result.GetErrorAsString("UserBus.AddUser"));
                                return;
                            }

                            result = FTP.UploadFile(Global.FTPConnectionInfo, exportFileName, remoteFileName);
                            if (!result.IsOK)
                            {
                                MsgBox.Show(Application.ProductName, result.GetErrorAsString("FTP.UploadFile"), IconType.Information);
                                Utility.WriteToTraceLog(result.GetErrorAsString("FTP.UploadFile"));
                            }
                            else
                            {
                                result = XetNghiem_Hitachi917Bus.UpdateDaUpload(hitachi917Keys);
                                if (!result.IsOK)
                                {
                                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_Hitachi917Bus.UpdateDaUpload"), IconType.Information);
                                    Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_Hitachi917Bus.UpdateDaUpload"));
                                }

                                result = KetQuaXetNghiemTayBus.UpdateDaUpload(hitachi917Keys);
                                if (!result.IsOK)
                                {
                                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaXetNghiemTayBus.UpdateDaUpload"), IconType.Information);
                                    Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTayBus.UpdateDaUpload"));
                                } 
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                        return;
                    }
                }
            }

            DataTable dt = dgXetNghiem.DataSource as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    bool isChecked = Convert.ToBoolean(row["Checked"]);
                    if (isChecked)
                        row["DaUpload"] = true;
                }
            }
        }
        #endregion

        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            DisplayDanhSachBenhNhan();
        }

        private void dtpkTuNgay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DisplayDanhSachBenhNhan();
        }

        private void chkCheckedBN_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgBenhNhan.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkCheckedBN.Checked;
            }
        }

        private void chkCheckedXN_CheckedChanged(object sender, EventArgs e)
        {
            if (_flag) return;
            DataTable dt = dgXetNghiem.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkCheckedXN.Checked;
            }
        }

        private void dgBenhNhan_SelectionChanged(object sender, EventArgs e)
        {
            if (dgBenhNhan.SelectedRows == null || dgBenhNhan.SelectedRows.Count <= 0)
            {
                if (dgXetNghiem.DataSource != null)
                {
                    DataTable dt = dgXetNghiem.DataSource as DataTable;
                    dt.Rows.Clear();
                }

                chkCheckedXN.Checked = false;
                return;
            }

            DataRow row = (dgBenhNhan.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (row == null)
            {
                if (dgXetNghiem.DataSource != null)
                {
                    DataTable dt = dgXetNghiem.DataSource as DataTable;
                    dt.Rows.Clear();
                }

                chkCheckedXN.Checked = false;
                return;
            }

            DisplayDanhSachXetNghiem(row);
        }

        private void btnPrintCellDyn3200_Click(object sender, EventArgs e)
        {
            PrintAll();
        }

        private void btnExportExcelCellDyn3200_Click(object sender, EventArgs e)
        {
            ExportCellDyn3200ToExcel();
        }

        private void btnExportExcelSinhHoa_Click(object sender, EventArgs e)
        {
            ExportSinhHoaToExcel();
        }

        private void btnAddChiTiet_Click(object sender, EventArgs e)
        {
            OnAddChiTiet();
        }

        private void btnEditChiTiet_Click(object sender, EventArgs e)
        {
            OnEditChiTiet();
        }

        private void btnDeleteChiTiet_Click(object sender, EventArgs e)
        {
            OnDeleteChiTiet();
        }

        private void dgXetNghiem_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEditChiTiet();
        }

        private void btnUploadFTP_Click(object sender, EventArgs e)
        {
            UploadKQXN();
        }

        private void uKetQuaXetNghiemTongHop_Load(object sender, EventArgs e)
        {
            int height1 = panel4.Height;
            int height2 = panel1.Height;
            int height = height1 + height2;
            height = (int)(height * 0.7);
            panel1.Height = height;
        }

        private void btnPrintCellDyn_Click(object sender, EventArgs e)
        {
            List<DataRow> checkedPatientRows = GetCheckedPatientRows();
            if (checkedPatientRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu ít nhất 1 bệnh nhân cần in.", IconType.Information);
                return;
            }

            UpdateUncheckedXetNghiem();

            DateTime tuNgay = dtpkTuNgay.Value;
            DateTime denNgay = dtpkDenNgay.Value;
            string exportFileName = string.Format("{0}\\Temp\\KetQuaXetNghiemCellDyn3200.xls", Application.StartupPath);
            if (_printDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> allKeys = new List<string>();
                foreach (DataRow row in checkedPatientRows)
                {
                    List<string> uncheckedList = null;
                    string patientGUID = row["PatientGUID"].ToString();
                    if (_htXN.ContainsKey(patientGUID))
                        uncheckedList = (List<string>)_htXN[patientGUID];

                    bool isData = false;
                    DateTime maxNgayXN = DateTime.Now;
                    List<string> keys = null;
                    if (!ExportExcel.ExportKetQuaXetNghiemCellDyn3200ToExcel(exportFileName, row, tuNgay, denNgay, uncheckedList, true, chkCoLine.Checked, ref isData, ref maxNgayXN, ref keys))
                        return;
                    else
                    {
                        try
                        {
                            if (isData)
                            {
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaXetNghiemCellDyn3200Template));
                                allKeys.AddRange(keys);
                            }
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                            return;
                        }
                    }
                }

                DataTable dt = dgXetNghiem.DataSource as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        bool isChecked = Convert.ToBoolean(row["Checked"]);
                        string key = row["ChiTietKQXNGUID"].ToString();
                        if (isChecked && allKeys.Contains(key))
                            row["DaIn"] = true;
                    }
                }
            }
        }

        private void btnPrintSinhHoa_Click(object sender, EventArgs e)
        {
            List<DataRow> checkedPatientRows = GetCheckedPatientRows();
            if (checkedPatientRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu ít nhất 1 bệnh nhân cần in.", IconType.Information);
                return;
            }

            UpdateUncheckedXetNghiem();

            DateTime tuNgay = dtpkTuNgay.Value;
            DateTime denNgay = dtpkDenNgay.Value;
            string exportFileName = string.Format("{0}\\Temp\\KetQuaXetNghiemSinhHoa.xls", Application.StartupPath);
            if (_printDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> allKeys = new List<string>();
                foreach (DataRow row in checkedPatientRows)
                {
                    List<string> uncheckedList = null;
                    string patientGUID = row["PatientGUID"].ToString();
                    if (_htXN.ContainsKey(patientGUID))
                        uncheckedList = (List<string>)_htXN[patientGUID];

                    bool isData = false;
                    DateTime maxNgayXN = DateTime.Now;
                    List<string> hitachi917Keys = null;
                    List<string> manualKeys = null;
                    if (!ExportExcel.ExportKetQuaXetNghiemSinhToExcel(exportFileName, row, tuNgay, denNgay, uncheckedList, true, chkCoLine.Checked, ref isData, ref maxNgayXN, ref hitachi917Keys, ref manualKeys))
                        return;
                    else
                    {
                        try
                        {
                            if (isData)
                            {
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaXetNghiemSinhHoaTemplate));
                                if (hitachi917Keys != null && hitachi917Keys.Count > 0)
                                    allKeys.AddRange(hitachi917Keys);

                                if (manualKeys != null && manualKeys.Count > 0)
                                    allKeys.AddRange(manualKeys);

                            }
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                            return;
                        }
                    }
                }

                DataTable dt = dgXetNghiem.DataSource as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        bool isChecked = Convert.ToBoolean(row["Checked"]);
                        string key = row["ChiTietKQXNGUID"].ToString();
                        if (isChecked && allKeys.Contains(key))
                            row["DaIn"] = true;
                    }
                }
            }
        }

        private void chkHienThiGioXetNghiem_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgXetNghiem.DataSource as DataTable;
            RefreshNgayXetNghiem(dt);
        }
        #endregion
    }
}
