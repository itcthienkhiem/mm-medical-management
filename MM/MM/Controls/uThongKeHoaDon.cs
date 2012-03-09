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
    public partial class uThongKeHoaDon : uBase
    {
        #region Members
        private bool _isFromDateToDate = true;
        private string _tenBenhNhan = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private int _type = 1; //0: TatCa; 1: ChuaXoa; 2: DaXoa
        #endregion

        #region Constructor
        public uThongKeHoaDon()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnPrint.Enabled = AllowPrint;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();

                _isFromDateToDate = raTuNgayToiNgay.Checked;
                _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                _tenBenhNhan = txtTenBenhNhan.Text;

                if (raTatCa.Checked) _type = 0;
                else if (raChuaXoa.Checked) _type = 1;
                else _type = 2;

                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayInvoiceListProc));
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

        private void OnDisplayInvoiceList()
        {
            Result result = ReportBus.GetTatCaHoaDon(_isFromDateToDate, _fromDate, _toDate, _tenBenhNhan, _type);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgInvoice.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetTatCaHoaDon"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetTatCaHoaDon"));
            }
        }

        public void ClearData()
        {
            dgInvoice.DataSource = null;
        }

        private void OnPrint()
        {
            List<string> checkedInvoicetList = new List<string>();
            List<string> loaiHoaDonList = new List<string>();
            DataTable dt = dgInvoice.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    checkedInvoicetList.Add(row["HoaDonThuocGUID"].ToString());
                    loaiHoaDonList.Add(row["LoaiHoaDon"].ToString());
                }
            }

            if (checkedInvoicetList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn in những hóa đơn mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    dlgPrintType dlg = new dlgPrintType();
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (_printDialog.ShowDialog() == DialogResult.OK)
                        {
                            string exportFileName = string.Format("{0}\\Temp\\HDGTGT.xls", Application.StartupPath);
                            int index = 0;
                            foreach (string hoaDonThuocGUID in checkedInvoicetList)
                            {
                                string loaiHoaDon = loaiHoaDonList[index];
                                if (dlg.Lien1)
                                {
                                    if (loaiHoaDon == "Hóa đơn thuốc")
                                    {
                                        if (ExportExcel.ExportHoaDonThuocToExcel(exportFileName, hoaDonThuocGUID, "                                   Liên 1: Lưu"))
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
                                        else return;
                                    }
                                    else if (loaiHoaDon == "Hóa đơn dịch vụ")
                                    {
                                        if (ExportExcel.ExportInvoiceToExcel(exportFileName, hoaDonThuocGUID, "                                   Liên 1: Lưu"))
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
                                        else return;
                                    }
                                    else
                                    {
                                        if (ExportExcel.ExportHoaDonXuatTruocToExcel(exportFileName, hoaDonThuocGUID, "                                   Liên 1: Lưu"))
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
                                        else return;
                                    }

                                }

                                if (dlg.Lien2)
                                {
                                    if (loaiHoaDon == "Hóa đơn thuốc")
                                    {
                                        if (ExportExcel.ExportHoaDonThuocToExcel(exportFileName, hoaDonThuocGUID, "                                   Liên 2: Giao người mua"))
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
                                        else return;
                                    }
                                    else if (loaiHoaDon == "Hóa đơn dịch vụ")
                                    {
                                        if (ExportExcel.ExportInvoiceToExcel(exportFileName, hoaDonThuocGUID, "                                   Liên 2: Giao người mua"))
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
                                        else return;
                                    }
                                    else
                                    {
                                        if (ExportExcel.ExportHoaDonXuatTruocToExcel(exportFileName, hoaDonThuocGUID, "                                   Liên 2: Giao người mua"))
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
                                        else return;
                                    }


                                }

                                if (dlg.Lien3)
                                {
                                    if (loaiHoaDon == "Hóa đơn thuốc")
                                    {
                                        if (ExportExcel.ExportHoaDonThuocToExcel(exportFileName, hoaDonThuocGUID, "                                   Liên 3: Nội bộ"))
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
                                        else return;
                                    }
                                    else if (loaiHoaDon == "Hóa đơn dịch vụ")
                                    {
                                        if (ExportExcel.ExportInvoiceToExcel(exportFileName, hoaDonThuocGUID, "                                   Liên 3: Nội bộ"))
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
                                        else return;
                                    }
                                    else
                                    {
                                        if (ExportExcel.ExportHoaDonXuatTruocToExcel(exportFileName, hoaDonThuocGUID, "                                   Liên 3: Nội bộ"))
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
                                        else return;
                                    }
                                }

                                index++;
                            }
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những hóa đơn cần in.", IconType.Information);
        }

        private void OnDisplayInvoiceInfo()
        {
            if (dgInvoice.SelectedRows == null || dgInvoice.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 hóa đơn.", IconType.Information);
                return;
            }

            DataRow drInvoice = (dgInvoice.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string loaiHoaDonStr = drInvoice["LoaiHoaDon"].ToString();
            Common.LoaiHoaDon lhd = Common.LoaiHoaDon.HoaDonThuoc;
            if (loaiHoaDonStr == "Hóa đơn thuốc")
                lhd = Common.LoaiHoaDon.HoaDonThuoc;
            else if (loaiHoaDonStr == "Hóa đơn dịch vụ")
                lhd = Common.LoaiHoaDon.HoaDonDichVu;
            else
                lhd = Common.LoaiHoaDon.HoaDonXuatTruoc;

            dlgHoaDonThuoc dlg = new dlgHoaDonThuoc(drInvoice, true);
            dlg.LoaiHoaDon = lhd;
            dlg.ShowDialog();
        }
        #endregion

        #region Window Event Handlers
        private void raTuNgayToiNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpkTuNgay.Enabled = raTuNgayToiNgay.Checked;
            dtpkDenNgay.Enabled = raTuNgayToiNgay.Checked;
            txtTenBenhNhan.ReadOnly = raTuNgayToiNgay.Checked;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgInvoice.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void dgInvoice_DoubleClick(object sender, EventArgs e)
        {
            OnDisplayInvoiceInfo();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (raTuNgayToiNgay.Checked && dtpkTuNgay.Value > dtpkDenNgay.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkTuNgay.Focus();
                return;
            }

            if (raTenBenhNhan.Checked && txtTenBenhNhan.Text.Trim() == string.Empty)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập tên tên khách hàng.", IconType.Information);
                txtTenBenhNhan.Focus();
                return;
            }

            DisplayAsThread();
        }
        #endregion

        #region Working Thread
        private void OnDisplayInvoiceListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayInvoiceList();
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
