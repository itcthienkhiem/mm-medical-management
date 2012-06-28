using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using MM.Dialogs;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;
using MM.Exports;
using SpreadsheetGear;

namespace MM.Controls
{
    public partial class uReceiptList : uBase
    {
        #region Members
        private bool _isFromDateToDate = true;
        private string _tenBenhNhan = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private int _type = 1; //0: TatCa; 1: ChuaXoa; 2: DaXoa
        #endregion

        #region Constructor
        public uReceiptList()
        {
            InitializeComponent();
            dtpkDenNgay.Value = DateTime.Now;
            dtpkTuNgay.Value = DateTime.Now.AddDays(-7);
        }
        #endregion

        #region Properties
        public List<DataRow> CheckedReceiptRows
        {
            get
            {
                if (dgReceipt.RowCount <= 0) return null;
                List<DataRow> checkedRows = new List<DataRow>();
                DataTable dt = dgReceipt.DataSource as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    if (Boolean.Parse(row["Checked"].ToString()))
                        checkedRows.Add(row);
                }

                return checkedRows;
            }
        }
        #endregion

        #region UI Command
        public void ClearData()
        {
            dgReceipt.DataSource = null;
        }

        private void UpdateGUI()
        {
            btnDelete.Enabled = AllowDelete;
            btnPrint.Enabled = AllowPrint;
            btnExportInvoice.Enabled = Global.AllowExportInvoice;
            btnExportExcel.Enabled = Global.AllowExportInvoice;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
                chkChecked.Checked = false;
                _isFromDateToDate = raTuNgayToiNgay.Checked;
                _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                _tenBenhNhan = txtTenBenhNhan.Text;
                if (raTatCa.Checked) _type = 0;
                else if (raChuaXoa.Checked) _type = 1;
                else _type = 2;
                               
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayReceiptListProc));
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

        public void HighlightExportedInvoice()
        {
            foreach (DataGridViewRow row in dgReceipt.Rows)
            {
                DataRow dr = (row.DataBoundItem as DataRowView).Row;
                bool isExported = Convert.ToBoolean(dr["IsExportedInVoice"]);
                if (isExported)
                    row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
            }
        }

        private void OnDisplayReceiptList()
        {
            Result result = ReceiptBus.GetReceiptList(_isFromDateToDate, _fromDate, _toDate, _tenBenhNhan, _type);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    DataTable dt = result.QueryResult as DataTable;
                    dgReceipt.DataSource = result.QueryResult;
                    HighlightExportedInvoice();
                    lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dt.Rows.Count);
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.GetReceiptList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.GetReceiptList"));
            }
        }

        private void OnDeleteReceipt()
        {
            List<string> deletedReceiptList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgReceipt.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    //deletedReceiptList.Add(row["ReceiptGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedRows.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những phiếu thu mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    List<string> noteList = new List<string>();

                    foreach (DataRow row in deletedRows)
                    {
                        string receiptCode = row["ReceiptCode"].ToString();
                        string receiptGUID = row["ReceiptGUID"].ToString();

                        dlgLyDoXoa dlg = new dlgLyDoXoa(receiptCode, 0);
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            noteList.Add(dlg.Notes);
                            deletedReceiptList.Add(receiptGUID);
                        }
                    }

                    if (deletedReceiptList.Count > 0)
                    {
                        Result result = ReceiptBus.DeleteReceipts(deletedReceiptList, noteList);
                        if (result.IsOK)
                        {
                            foreach (DataRow row in deletedRows)
                            {
                                string receiptGUID = row["ReceiptGUID"].ToString();
                                if (deletedReceiptList.Contains(receiptGUID))
                                    dt.Rows.Remove(row);
                            }
                        }
                        else
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.DeleteReceipts"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.DeleteReceipts"));
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những phiếu thu cần xóa.", IconType.Information);
        }

        private void OnPrint()
        {
            Cursor.Current = Cursors.WaitCursor;
            List<string> checkedReceiptList = new List<string>();
            DataTable dt = dgReceipt.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    checkedReceiptList.Add(row["ReceiptGUID"].ToString());
                }
            }

            if (checkedReceiptList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn in những phiếu thu mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    string exportFileName = string.Format("{0}\\Temp\\Receipt.xls", Application.StartupPath);
                    foreach (string receiptGUID in checkedReceiptList)
                    {
                        try
                        {
                            if (ExportExcel.ExportReceiptToExcel(exportFileName, receiptGUID))
                            {
                                if (_printDialog.ShowDialog() == DialogResult.OK)
                                    ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.PhieuThuDichVuTemplate));
                                //ExcelPrintPreview.PrintPreview(exportFileName);
                            }
                            else
                                return;
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những phiếu thu cần in.", IconType.Information);
        }

        private void OnExportExcel()
        {
            Cursor.Current = Cursors.WaitCursor;
            List<string> checkedReceiptList = new List<string>();
            DataTable dt = dgReceipt.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    checkedReceiptList.Add(row["ReceiptGUID"].ToString());
                }
            }

            if (checkedReceiptList.Count > 0)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ExportExcel.ExportChiTietPhieuThuToExcel(dlg.FileName, checkedReceiptList);
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những phiếu thu cần in.", IconType.Information);
        }

        private void DisplayReceiptDetail()
        {
            if (dgReceipt.SelectedRows == null || dgReceipt.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 phiếu thu.", IconType.Information);
                return;
            }

            DataRow drReceipt = (dgReceipt.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgReceiptDetail dlg = new dlgReceiptDetail(drReceipt);
            if (dlg.ShowDialog() == DialogResult.Cancel)
            {
                if (dlg.IsExportedInvoice)
                    HighlightExportedInvoice();
            }
            else
                HighlightExportedInvoice();
        }

        private void OnExportInvoice()
        {
            List<DataRow> exportedInvoiceList = new List<DataRow>();
            List<DataRow> noExportedInvoiceList = new List<DataRow>();
            List<DataRow> checkedRows = CheckedReceiptRows;

            foreach (DataRow row in checkedRows)
            {

                bool isExported = Convert.ToBoolean(row["IsExportedInvoice"]);
                if (!isExported)
                    noExportedInvoiceList.Add(row);
                else
                    exportedInvoiceList.Add(row);
            }

            if (exportedInvoiceList.Count > 0)
            {
                MsgBox.Show(Application.ProductName, "(Một số) phiếu thu đã xuất hóa đơn rồi. Vui lòng kiểm tra lại.", IconType.Information);
                return;
            }

            if (MsgBox.Question(Application.ProductName, "Bạn có muốn xuất hóa đơn ?") == DialogResult.No) return;

            dlgInvoiceInfo dlg = new dlgInvoiceInfo(noExportedInvoiceList);
            dlg.ShowDialog();

            HighlightExportedInvoice();
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgReceipt.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteReceipt();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint();
        }

        private void dgReceipt_DoubleClick(object sender, EventArgs e)
        {
            DisplayReceiptDetail();
        }

        private void btnExportInvoice_Click(object sender, EventArgs e)
        {
            if (dgReceipt.RowCount <= 0 ||
                CheckedReceiptRows == null || CheckedReceiptRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu ít nhất 1 phiếu thu cần xuất hóa đơn.", IconType.Information);
                return;
            }

            OnExportInvoice();
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
                MsgBox.Show(Application.ProductName, "Vui lòng nhập tên bệnh nhân.", IconType.Information);
                txtTenBenhNhan.Focus();
                return;
            }

            DisplayAsThread();
        }

        private void raTuNgayToiNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpkTuNgay.Enabled = raTuNgayToiNgay.Checked;
            dtpkDenNgay.Enabled = raTuNgayToiNgay.Checked;
            txtTenBenhNhan.ReadOnly = raTuNgayToiNgay.Checked;
        }

        private void txtTenBenhNhan_KeyDown(object sender, KeyEventArgs e)
        {
            //if (raTenBenhNhan.Checked && e.KeyCode == Keys.Enter)
            //{
            //    if (raTuNgayToiNgay.Checked && dtpkTuNgay.Value > dtpkDenNgay.Value)
            //    {
            //        MsgBox.Show(Application.ProductName, "Vui lòng nhập từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
            //        dtpkTuNgay.Focus();
            //        return;
            //    }

            //    if (raTenBenhNhan.Checked && txtTenBenhNhan.Text.Trim() == string.Empty)
            //    {
            //        MsgBox.Show(Application.ProductName, "Vui lòng nhập tên bệnh nhân.", IconType.Information);
            //        txtTenBenhNhan.Focus();
            //        return;
            //    }

            //    DisplayAsThread();
            //}
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }

        private void dgReceipt_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            HighlightExportedInvoice();
        }
        #endregion

        #region Working Thread
        private void OnDisplayReceiptListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayReceiptList();
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
