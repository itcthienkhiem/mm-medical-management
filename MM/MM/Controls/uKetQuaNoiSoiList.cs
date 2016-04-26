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
using MM.Common;
using MM.Bussiness;
using MM.Databasae;
using MM.Dialogs;
using MM.Exports;

namespace MM.Controls
{
    public partial class uKetQuaNoiSoiList : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        private string _patientGUID = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private bool _isPrint = false;
        private KetQuaNoiSoi _ketQuaNoiSoi = null;
        private DataRow _patientRow2 = null;
        private bool _isChuyenBenhAn = false;
        #endregion

        #region Constructor
        public uKetQuaNoiSoiList()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool IsChuyenBenhAn
        {
            get { return _isChuyenBenhAn; }
            set 
            { 
                _isChuyenBenhAn = value;
                btnChuyen.Visible = _isChuyenBenhAn;
                btnAdd.Visible = !_isChuyenBenhAn;
                btnEdit.Visible = !_isChuyenBenhAn;
                btnDelete.Visible = !_isChuyenBenhAn;
                btnExportExcel.Visible = !_isChuyenBenhAn;
                btnPrint.Visible = !_isChuyenBenhAn;
                btnPrintPreview.Visible = !_isChuyenBenhAn;

                if (_isChuyenBenhAn)
                    dgKhamNoiSoi.ContextMenuStrip = ctmAction2;
            }
        }

        public DataRow PatientRow
        {
            get { return _patientRow; }
            set { _patientRow = value; }
        }

        public DataRow PatientRow2
        {
            get { return _patientRow2; }
            set { _patientRow2 = value; }
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = Global.AllowAddKhamNoiSoi;
            btnDelete.Enabled = Global.AllowDeleteKhamNoiSoi;
            btnPrint.Enabled = Global.AllowPrintKhamNoiSoi;
            btnPrintPreview.Enabled = Global.AllowPrintKhamNoiSoi;
            btnExportExcel.Enabled = Global.AllowExportKhamNoiSoi;

            addToolStripMenuItem.Enabled = Global.AllowAddKhamNoiSoi;
            deleteToolStripMenuItem.Enabled = Global.AllowDeleteKhamNoiSoi;
            printPreviewToolStripMenuItem.Enabled = Global.AllowPrintKhamNoiSoi;
            printToolStripMenuItem.Enabled = Global.AllowPrintKhamNoiSoi;
            exportExcelToolStripMenuItem.Enabled = Global.AllowExportKhamNoiSoi;

            btnChuyen.Enabled = AllowChuyenKetQuaKham;
            chuyenToolStripMenuItem.Enabled = AllowChuyenKetQuaKham;
        }

        public void DisplayAsThread()
        {
            UpdateGUI();
            if (_patientRow == null) return;

            try
            {
                DataRow row = _patientRow as DataRow;
                _patientGUID = row["PatientGUID"].ToString();
                if (raAll.Checked)
                {
                    _fromDate = Global.MinDateTime;
                    _toDate = Global.MaxDateTime;
                }
                else
                {
                    _fromDate = new DateTime(dtpkFromDate.Value.Year, dtpkFromDate.Value.Month, dtpkFromDate.Value.Day, 0, 0, 0);
                    _toDate = new DateTime(dtpkToDate.Value.Year, dtpkToDate.Value.Month, dtpkToDate.Value.Day, 23, 59, 59);
                }
                

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayKetQuaNoiSoiListProc));
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

        public void ClearData()
        {
            DataTable dt = dgKhamNoiSoi.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgKhamNoiSoi.DataSource = null;
            }
        }

        private void OnDisplayKetQuaNoiSoiList()
        {
            Result result = KetQuaNoiSoiBus.GetKetQuaNoiSoiList(_patientGUID, _fromDate, _toDate);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    DataTable dt = result.QueryResult as DataTable;
                    dgKhamNoiSoi.DataSource = dt;

                    if (_isPrint)
                    {
                        DataRow[] rows = dt.Select(string.Format("KetQuaNoiSoiGUID='{0}'", _ketQuaNoiSoi.KetQuaNoiSoiGUID.ToString()));
                        if (rows != null && rows.Length > 0)
                        {
                            OnPrint(rows[0]);
                        }
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaNoiSoiBus.GetKetQuaNoiSoiList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaNoiSoiBus.GetKetQuaNoiSoiList"));
            }
        }

        private void OnAdd()
        {
            _isPrint = false;
            _ketQuaNoiSoi = null;
            string maBenhNhan = _patientRow["FileNum"].ToString();
            string tenBenhNhan = _patientRow["FullName"].ToString();
            dlgAddKetQuaNoiSoi dlg = new dlgAddKetQuaNoiSoi(_patientGUID);
            dlg.MaBenhNhan = maBenhNhan;
            dlg.TenBenhNhan = tenBenhNhan;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _isPrint = dlg.IsPrint;
                _ketQuaNoiSoi = dlg.KetQuaNoiSoi;
                DisplayAsThread();
            }
        }

        private void OnEdit()
        {
            _isPrint = false;
            _ketQuaNoiSoi = null;
            if (dgKhamNoiSoi.SelectedRows == null || dgKhamNoiSoi.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 kết quả nội soi.", IconType.Information);
                return;
            }

            DataRow drKetQuaNoiSoi = (dgKhamNoiSoi.SelectedRows[0].DataBoundItem as DataRowView).Row;
            bool allowEdit = _isChuyenBenhAn ? false : Global.AllowEditKhamNoiSoi;
            string maBenhNhan = _patientRow["FileNum"].ToString();
            string tenBenhNhan = _patientRow["FullName"].ToString();
            dlgAddKetQuaNoiSoi dlg = new dlgAddKetQuaNoiSoi(_patientGUID, drKetQuaNoiSoi, allowEdit);
            dlg.MaBenhNhan = maBenhNhan;
            dlg.TenBenhNhan = tenBenhNhan;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _isPrint = dlg.IsPrint;
                _ketQuaNoiSoi = dlg.KetQuaNoiSoi;
                DisplayAsThread();
            }
        }

        private void OnDelete()
        {
            List<string> deletedKQNSList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgKhamNoiSoi.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedKQNSList.Add(row["KetQuaNoiSoiGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedKQNSList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những kết quả nội soi mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = KetQuaNoiSoiBus.DeleteKetQuaNoiSoi(deletedKQNSList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaNoiSoiBus.DeleteKetQuaNoiSoi"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaNoiSoiBus.DeleteKetQuaNoiSoi"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những kết quả nội soi.", IconType.Information);
        }

        private List<DataRow> GetCheckedRows()
        {
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgKhamNoiSoi.DataSource as DataTable;
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
            if (checkedRows.Count > 0)
            {
                string exportFileName = string.Format("{0}\\Temp\\KetQuaNoiSoi.xls", Application.StartupPath);
                if (isPreview)
                {
                    foreach (DataRow row in checkedRows)
                    {
                        LoaiNoiSoi type = (LoaiNoiSoi)Convert.ToByte(row["LoaiNoiSoi"]);
                        switch (type)
                        {
                            case LoaiNoiSoi.Tai:
                                if (!ExportExcel.ExportKetQuaNoiSoiTaiToExcel(exportFileName, _patientRow, row))
                                    return;
                                else
                                {
                                    try
                                    {
                                        ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiTaiTemplate));
                                    }
                                    catch (Exception ex)
                                    {
                                        MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                        return;
                                    }

                                    break;
                                }
                            case LoaiNoiSoi.Mui:
                                if (!ExportExcel.ExportKetQuaNoiSoiMuiToExcel(exportFileName, _patientRow, row))
                                    return;
                                else
                                {
                                    try
                                    {
                                        ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiMuiTemplate));
                                    }
                                    catch (Exception ex)
                                    {
                                        MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                        return;
                                    }

                                    break;
                                }
                            case LoaiNoiSoi.Hong_ThanhQuan:
                                if (!ExportExcel.ExportKetQuaNoiSoiHongThanhQuanToExcel(exportFileName, _patientRow, row))
                                    return;
                                else
                                {
                                    try
                                    {
                                        ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiHongThanhQuanTemplate));
                                    }
                                    catch (Exception ex)
                                    {
                                        MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                        return;
                                    }

                                    break;
                                }
                            case LoaiNoiSoi.TaiMuiHong:
                                if (!ExportExcel.ExportKetQuaNoiSoiTaiMuiHongToExcel(exportFileName, _patientRow, row))
                                    return;
                                else
                                {
                                    try
                                    {
                                        ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiTaiMuiHongTemplate));
                                    }
                                    catch (Exception ex)
                                    {
                                        MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                        return;
                                    }

                                    break;
                                }
                            case LoaiNoiSoi.TongQuat:
                                if (!ExportExcel.ExportKetQuaNoiSoiTongQuatToExcel(exportFileName, _patientRow, row))
                                    return;
                                else
                                {
                                    try
                                    {
                                        ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiTongQuatTemplate));
                                    }
                                    catch (Exception ex)
                                    {
                                        MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                        return;
                                    }

                                    break;
                                }
                            case LoaiNoiSoi.DaDay:
                                if (!ExportExcel.ExportKetQuaNoiSoiDaDayToExcel(exportFileName, _patientRow, row))
                                    return;
                                else
                                {
                                    try
                                    {
                                        ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiDaDayTemplate));
                                    }
                                    catch (Exception ex)
                                    {
                                        MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                        return;
                                    }

                                    break;
                                }
                            case LoaiNoiSoi.TrucTrang:
                                if (!ExportExcel.ExportKetQuaNoiSoiTrucTrangToExcel(exportFileName, _patientRow, row))
                                    return;
                                else
                                {
                                    try
                                    {
                                        ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiTrucTrangTemplate));
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
                else
                {
                    if (_printDialog.ShowDialog() == DialogResult.OK)
                    {
                        foreach (DataRow row in checkedRows)
                        {
                            LoaiNoiSoi type = (LoaiNoiSoi)Convert.ToByte(row["LoaiNoiSoi"]);
                            switch (type)
                            {
                                case LoaiNoiSoi.Tai:
                                    if (!ExportExcel.ExportKetQuaNoiSoiTaiToExcel(exportFileName, _patientRow, row))
                                        return;
                                    else
                                    {
                                        try
                                        {
                                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiTaiTemplate));
                                        }
                                        catch (Exception ex)
                                        {
                                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                            return;
                                        }

                                        break;
                                    }
                                case LoaiNoiSoi.Mui:
                                    if (!ExportExcel.ExportKetQuaNoiSoiMuiToExcel(exportFileName, _patientRow, row))
                                        return;
                                    else
                                    {
                                        try
                                        {
                                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiMuiTemplate));
                                        }
                                        catch (Exception ex)
                                        {
                                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                            return;
                                        }

                                        break;
                                    }
                                case LoaiNoiSoi.Hong_ThanhQuan:
                                    if (!ExportExcel.ExportKetQuaNoiSoiHongThanhQuanToExcel(exportFileName, _patientRow, row))
                                        return;
                                    else
                                    {
                                        try
                                        {
                                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiHongThanhQuanTemplate));
                                        }
                                        catch (Exception ex)
                                        {
                                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                            return;
                                        }

                                        break;
                                    }
                                case LoaiNoiSoi.TaiMuiHong:
                                    if (!ExportExcel.ExportKetQuaNoiSoiTaiMuiHongToExcel(exportFileName, _patientRow, row))
                                        return;
                                    else
                                    {
                                        try
                                        {
                                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiTaiMuiHongTemplate));
                                        }
                                        catch (Exception ex)
                                        {
                                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                            return;
                                        }

                                        break;
                                    }
                                case LoaiNoiSoi.TongQuat:
                                    if (!ExportExcel.ExportKetQuaNoiSoiTongQuatToExcel(exportFileName, _patientRow, row))
                                        return;
                                    else
                                    {
                                        try
                                        {
                                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiTongQuatTemplate));
                                        }
                                        catch (Exception ex)
                                        {
                                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                            return;
                                        }

                                        break;
                                    }
                                case LoaiNoiSoi.DaDay:
                                    if (!ExportExcel.ExportKetQuaNoiSoiDaDayToExcel(exportFileName, _patientRow, row))
                                        return;
                                    else
                                    {
                                        try
                                        {
                                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiDaDayTemplate));
                                        }
                                        catch (Exception ex)
                                        {
                                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                            return;
                                        }

                                        break;
                                    }
                                case LoaiNoiSoi.TrucTrang:
                                    if (!ExportExcel.ExportKetQuaNoiSoiTrucTrangToExcel(exportFileName, _patientRow, row))
                                        return;
                                    else
                                    {
                                        try
                                        {
                                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiTrucTrangTemplate));
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
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những kết quả nội soi cần in.", IconType.Information);
        }

        private void OnPrint(DataRow drKetQuaNoiSoi)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (_printDialog.ShowDialog() == DialogResult.OK)
            {
                string exportFileName = string.Format("{0}\\Temp\\KetQuaNoiSoi.xls", Application.StartupPath);
                LoaiNoiSoi type = (LoaiNoiSoi)Convert.ToByte(drKetQuaNoiSoi["LoaiNoiSoi"]);
                switch (type)
                {
                    case LoaiNoiSoi.Tai:
                        if (!ExportExcel.ExportKetQuaNoiSoiTaiToExcel(exportFileName, _patientRow, drKetQuaNoiSoi))
                            return;
                        else
                        {
                            try
                            {
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiTaiTemplate));
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                return;
                            }

                            break;
                        }
                    case LoaiNoiSoi.Mui:
                        if (!ExportExcel.ExportKetQuaNoiSoiMuiToExcel(exportFileName, _patientRow, drKetQuaNoiSoi))
                            return;
                        else
                        {
                            try
                            {
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiMuiTemplate));
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                return;
                            }

                            break;
                        }
                    case LoaiNoiSoi.Hong_ThanhQuan:
                        if (!ExportExcel.ExportKetQuaNoiSoiHongThanhQuanToExcel(exportFileName, _patientRow, drKetQuaNoiSoi))
                            return;
                        else
                        {
                            try
                            {
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiHongThanhQuanTemplate));
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                return;
                            }

                            break;
                        }
                    case LoaiNoiSoi.TaiMuiHong:
                        if (!ExportExcel.ExportKetQuaNoiSoiTaiMuiHongToExcel(exportFileName, _patientRow, drKetQuaNoiSoi))
                            return;
                        else
                        {
                            try
                            {
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiTaiMuiHongTemplate));
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                return;
                            }

                            break;
                        }
                    case LoaiNoiSoi.TongQuat:
                        if (!ExportExcel.ExportKetQuaNoiSoiTongQuatToExcel(exportFileName, _patientRow, drKetQuaNoiSoi))
                            return;
                        else
                        {
                            try
                            {
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiTongQuatTemplate));
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                return;
                            }

                            break;
                        }
                    case LoaiNoiSoi.DaDay:
                        if (!ExportExcel.ExportKetQuaNoiSoiDaDayToExcel(exportFileName, _patientRow, drKetQuaNoiSoi))
                            return;
                        else
                        {
                            try
                            {
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiDaDayTemplate));
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                return;
                            }

                            break;
                        }
                    case LoaiNoiSoi.TrucTrang:
                        if (!ExportExcel.ExportKetQuaNoiSoiTrucTrangToExcel(exportFileName, _patientRow, drKetQuaNoiSoi))
                            return;
                        else
                        {
                            try
                            {
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaNoiSoiTrucTrangTemplate));
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

        private void OnExportExcel()
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = GetCheckedRows();
            if (checkedRows.Count > 0)
            {
                foreach (DataRow row in checkedRows)
                {
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.Title = "Export Excel";
                    dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        LoaiNoiSoi type = (LoaiNoiSoi)Convert.ToByte(row["LoaiNoiSoi"]);
                        switch (type)
                        {
                            case LoaiNoiSoi.Tai:
                                if (!ExportExcel.ExportKetQuaNoiSoiTaiToExcel(dlg.FileName, _patientRow, row))
                                    return;
                                else
                                    break;
                            case LoaiNoiSoi.Mui:
                                if (!ExportExcel.ExportKetQuaNoiSoiMuiToExcel(dlg.FileName, _patientRow, row))
                                    return;
                                else
                                    break;
                            case LoaiNoiSoi.Hong_ThanhQuan:
                                if (!ExportExcel.ExportKetQuaNoiSoiHongThanhQuanToExcel(dlg.FileName, _patientRow, row))
                                    return;
                                else
                                    break;
                            case LoaiNoiSoi.TaiMuiHong:
                                if (!ExportExcel.ExportKetQuaNoiSoiTaiMuiHongToExcel(dlg.FileName, _patientRow, row))
                                    return;
                                else
                                    break;
                            case LoaiNoiSoi.TongQuat:
                                if (!ExportExcel.ExportKetQuaNoiSoiTongQuatToExcel(dlg.FileName, _patientRow, row))
                                    return;
                                else
                                    break;
                            case LoaiNoiSoi.DaDay:
                                if (!ExportExcel.ExportKetQuaNoiSoiDaDayToExcel(dlg.FileName, _patientRow, row))
                                    return;
                                else
                                    break;
                            case LoaiNoiSoi.TrucTrang:
                                if (!ExportExcel.ExportKetQuaNoiSoiTrucTrangToExcel(dlg.FileName, _patientRow, row))
                                    return;
                                else
                                    break;
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những kết quả nội soi cần xuất excel.", IconType.Information);
        }

        private void OnChuyenKetQuaKham()
        {
            if (!_isChuyenBenhAn) return;

            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgKhamNoiSoi.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                    deletedRows.Add(row);
            }

            if (dgKhamNoiSoi.RowCount <= 0 || deletedRows == null || deletedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu ít nhất 1 kết quả nội soi cần chuyển.", IconType.Information);
                return;
            }

            if (_patientRow2 == null)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn bệnh nhân nhận kết quả nội soi chuyển đến.", IconType.Information);
                return;
            }

            string fileNum = _patientRow2["FileNum"].ToString();
            if (MsgBox.Question(Application.ProductName, string.Format("Bạn có muốn chuyển những kết quả nội soi đã chọn đến bệnh nhân: '{0}'?", fileNum)) == DialogResult.No) return;

            Result result = KetQuaNoiSoiBus.ChuyenBenhAn(_patientRow2["PatientGUID"].ToString(), deletedRows);
            if (result.IsOK)
                DisplayAsThread();
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaNoiSoiBus.ChuyenBenhAn"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaNoiSoiBus.ChuyenBenhAn"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
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

        private void dgKhamNoiSoi_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgKhamNoiSoi.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
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

        private void raAll_CheckedChanged(object sender, EventArgs e)
        {
            dtpkFromDate.Enabled = !raAll.Checked;
            dtpkToDate.Enabled = !raAll.Checked;
            btnSearch.Enabled = !raAll.Checked;

            DisplayAsThread();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDelete();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void exportExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }

        private void btnChuyen_Click(object sender, EventArgs e)
        {
            OnChuyenKetQuaKham();
        }

        private void chuyenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnChuyenKetQuaKham();
        }
        #endregion

        #region Working Thread
        private void OnDisplayKetQuaNoiSoiListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayKetQuaNoiSoiList();
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
