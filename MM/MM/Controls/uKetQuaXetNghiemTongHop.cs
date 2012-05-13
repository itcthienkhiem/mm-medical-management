﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
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
                dgXetNghiem.DataSource = result.QueryResult as DataTable;

                List<string> uncheckedList = null;
                if (_htXN.ContainsKey(_patientGUID))
                    uncheckedList = (List<string>)_htXN[_patientGUID];

                RefreshHighlight(uncheckedList);
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
                DataRow dr = (row.DataBoundItem as DataRowView).Row;
                string chiTietKQXNGUID = dr["ChiTietKQXNGUID"].ToString();

                if (uncheckedList != null && uncheckedList.Contains(chiTietKQXNGUID))
                    dr["Checked"] = false;
                else
                    dr["Checked"] = true;

                TinhTrang tinhTrang = (TinhTrang)Convert.ToByte(dr["TinhTrang"]);
                if (tinhTrang == MM.Common.TinhTrang.BatThuong)
                {
                    row.DefaultCellStyle.Font = _boldFont;
                    row.DefaultCellStyle.ForeColor = Color.Red;
                }
                else
                {
                    row.DefaultCellStyle.Font = _normalFont;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
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
                    if (!ExportExcel.ExportKetQuaXetNghiemCellDyn3200ToExcel(dlg.FileName, row, tuNgay, denNgay, uncheckedList, false, ref isData))
                        return;
                }
            }
        }

        private void PrintCellDyn3200()
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
                    if (!ExportExcel.ExportKetQuaXetNghiemCellDyn3200ToExcel(exportFileName, row, tuNgay, denNgay, uncheckedList, true, ref isData))
                        return;
                    else
                    {
                        try
                        {
                            if (isData) 
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName);
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                            return;
                        }
                    }

                    exportFileName = string.Format("{0}\\Temp\\KetQuaXetNghiemSinhHoa.xls", Application.StartupPath);
                    if (!ExportExcel.ExportKetQuaXetNghiemSinhToExcel(exportFileName, row, tuNgay, denNgay, uncheckedList, true, ref isData))
                        return;
                    else
                    {
                        try
                        {
                            if (isData)
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName);
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
                    if (!ExportExcel.ExportKetQuaXetNghiemSinhToExcel(dlg.FileName, row, tuNgay, denNgay, uncheckedList, false, ref isData))
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
                    if (!ExportExcel.ExportKetQuaXetNghiemSinhToExcel(exportFileName, row, tuNgay, denNgay, uncheckedList, true, ref isData))
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

                        break;
                    }
                }
            }
        }

        private void OnAddChiTiet()
        {
            dlgAddKetQuaXetNghiemTay dlg = new dlgAddKetQuaXetNghiemTay();
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
            PrintCellDyn3200();
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
        #endregion

       

        
    }
}
