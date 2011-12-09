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
using SpreadsheetGear;

namespace MM.Controls
{
    public partial class uReceiptList : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uReceiptList()
        {
            InitializeComponent();
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
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
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
            Result result = ReceiptBus.GetReceiptList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgReceipt.DataSource = result.QueryResult;
                    HighlightExportedInvoice();
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
                    deletedReceiptList.Add(row["ReceiptGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedReceiptList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những phiếu thu mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = ReceiptBus.DeleteReceipts(deletedReceiptList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
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
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những phiếu thu cần xóa.", IconType.Information);
        }

        private bool ExportToExcel(string exportFileName, string receiptGUID)
        {
            Cursor.Current = Cursors.WaitCursor;
            IWorkbook workBook = null;

            try
            {
                Result result = ReceiptBus.GetReceipt(receiptGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.GetReceipt"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.GetReceipt"));
                    return false;
                }

                ReceiptView receipt = result.QueryResult as ReceiptView;
                if (receipt == null) return false;

                result = ReceiptBus.GetReceiptDetailList(receiptGUID);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.GetReceiptDetailList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.GetReceiptDetailList"));
                    return false;
                }

                string excelTemplateName = string.Format("{0}\\Templates\\ReceiptTemplate.xls", Application.StartupPath);

                workBook = SpreadsheetGear.Factory.GetWorkbook(excelTemplateName);
                ExcelPrintPreview.SetCulturalWithEN_US();
                IWorksheet workSheet = workBook.Worksheets[0];
                workSheet.Cells["A2"].Value = string.Format("Số: {0}", receipt.ReceiptCode);
                workSheet.Cells["B6"].Value = string.Format("Tên: {0}", receipt.FullName);
                workSheet.Cells["B7"].Value = string.Format("Mã bệnh nhân: {0}", receipt.FileNum);
                workSheet.Cells["B8"].Value = string.Format("Ngày: {0}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                if (receipt.Address != null) workSheet.Cells["B9"].Value = string.Format("Địa chỉ: {0}", receipt.Address);
                else workSheet.Cells["B9"].Value = "Địa chỉ:";

                int rowIndex = 11;
                int no = 1;
                double totalPrice = 0;
                IRange range;
                DataTable dtSource = result.QueryResult as DataTable;
                foreach (DataRow row in dtSource.Rows)
                {
                    string serviceName = row["Name"].ToString();
                    double price = Convert.ToDouble(row["Price"]);
                    double disCount = Convert.ToDouble(row["Discount"]);
                    double amount = Convert.ToDouble(row["Amount"]);
                    totalPrice += amount;
                    workSheet.Cells[rowIndex, 1].Value = no++;
                    workSheet.Cells[rowIndex, 1].HorizontalAlignment = HAlign.Center;

                    workSheet.Cells[rowIndex, 2].Value = serviceName;

                    if (price > 0)
                        workSheet.Cells[rowIndex, 3].Value = price.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 3].Value = price.ToString();

                    workSheet.Cells[rowIndex, 3].HorizontalAlignment = HAlign.Right;

                    if (disCount > 0)
                        workSheet.Cells[rowIndex, 4].Value = disCount.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 4].Value = disCount.ToString();

                    workSheet.Cells[rowIndex, 4].HorizontalAlignment = HAlign.Right;

                    if (amount > 0)
                        workSheet.Cells[rowIndex, 5].Value = amount.ToString("#,###");
                    else
                        workSheet.Cells[rowIndex, 5].Value = amount.ToString();

                    workSheet.Cells[rowIndex, 5].HorizontalAlignment = HAlign.Right;

                    range = workSheet.Cells[string.Format("B{0}:F{0}", rowIndex + 1)];
                    range.Borders[BordersIndex.EdgeBottom].LineStyle = LineStyle.Dash;
                    range.Borders[BordersIndex.EdgeBottom].Color = Color.Black;

                    rowIndex++;
                }

                range = workSheet.Cells[string.Format("E{0}", rowIndex + 1)];
                range.Value = "Tổng cộng:";
                range.Font.Bold = true;

                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                if (totalPrice > 0)
                    range.Value = string.Format("{0} VNĐ", totalPrice.ToString("#,###"));
                else
                    range.Value = string.Format("{0} VNĐ", totalPrice.ToString());

                range.Font.Bold = true;
                range.HorizontalAlignment = HAlign.Right;

                rowIndex += 2;
                range = workSheet.Cells[string.Format("B{0}", rowIndex + 1)];
                range.Value = string.Format("Bằng chữ: {0}", Utility.ReadNumberAsString((long)totalPrice).ToUpper());
                range.Font.Bold = true;

                range = workSheet.Cells[string.Format("B{0}:F{0}", rowIndex + 1)];
                range.Borders[BordersIndex.EdgeBottom].LineStyle = LineStyle.Dash;
                range.Borders[BordersIndex.EdgeBottom].Color = Color.Black;

                rowIndex += 2;
                range = workSheet.Cells[string.Format("C{0}", rowIndex + 1)];
                range.Value = "Người lập phiếu";
                range.HorizontalAlignment = HAlign.Center;

                range = workSheet.Cells[string.Format("D{0}", rowIndex + 1)];
                range.Value = "Người nộp tiền";
                range.HorizontalAlignment = HAlign.Left;

                range = workSheet.Cells[string.Format("F{0}", rowIndex + 1)];
                range.Value = "Thu ngân";
                range.HorizontalAlignment = HAlign.Left;

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.Excel8);
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                return false;
            }
            finally
            {
                ExcelPrintPreview.SetCulturalWithCurrent();
                if (workBook != null)
                {
                    workBook.Close();
                    workBook = null;
                }
            }

            return true;
        }

        private void OnPrint()
        {
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
                            if (ExportToExcel(exportFileName, receiptGUID))
                            {
                                if (_printDialog.ShowDialog() == DialogResult.OK)
                                    ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName);
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

        private void DisplayReceiptDetail()
        {
            if (dgReceipt.SelectedRows == null || dgReceipt.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 phiếu thu.", IconType.Information);
                return;
            }

            DataRow drReceipt = (dgReceipt.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgReceiptDetail dlg = new dlgReceiptDetail(drReceipt);
            dlg.ShowDialog();
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
                MsgBox.Show(Application.ProductName, "Đã có 1 số phiếu thu xuất hóa đơn rồi. Vui lòng kiểm tra lại.", IconType.Information);
                return;
            }

            if (MsgBox.Question(Application.ProductName, "Bạn có muốn xuất hóa đơn ?") == DialogResult.No) return;

            foreach (DataRow row in noExportedInvoiceList)
            {
                
            }
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
