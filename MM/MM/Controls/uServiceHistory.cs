using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using SpreadsheetGear;
using System.IO;
using MM.Common;
using MM.Dialogs;
using MM.Bussiness;
using MM.Databasae;

namespace MM.Controls
{
    public partial class uServiceHistory : uBase
    {
        #region Members
        private object _patientRow = null;
        private string _patientGUID = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private bool _isAll = true;
        private bool _isDailyService = false;
        #endregion

        #region Constructor
        public uServiceHistory()
        {
            InitializeComponent();
            dtpkFromDate.Value = DateTime.Now.AddDays(-1);
            dtpkToDate.Value = DateTime.Now;
        }
        #endregion

        #region Properties
        public object PatientRow
        {
            get { return _patientRow; }
            set { _patientRow = value; }
        }

        public bool IsDailyService
        {
            get { return _isDailyService; }
            set 
            { 
                _isDailyService = value;
                pFilter.Visible = !_isDailyService;
            }
        }

        public List<DataRow> CheckedServiceRows
        {
            get
            {
                if (dgServiceHistory.RowCount <= 0) return null;
                List<DataRow> checkedRows = new List<DataRow>();
                DataTable dt = dgServiceHistory.DataSource as DataTable;
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
        private void UpdateGUI()
        {
            fixedPriceDataGridViewTextBoxColumn.Visible = Global.AllowShowServiePrice;
            pTotal.Visible = Global.AllowShowServiePrice;
            btnExportReceipt.Visible = Global.AllowExportReceipt;
        }

        private void OnAdd()
        {
            dlgAddServiceHistory dlg = new dlgAddServiceHistory(_patientGUID);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                base.RaiseServiceHistoryChanged();
            }
        }

        private void OnEdit()
        {
            if (dgServiceHistory.SelectedRows == null || dgServiceHistory.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 dịch vụ.", IconType.Information);
                return;
            }

            DataRow drServiceHistory = (dgServiceHistory.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddServiceHistory dlg = new dlgAddServiceHistory(_patientGUID, drServiceHistory);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                base.RaiseServiceHistoryChanged();
            }
        }

        private void OnDelete()
        {
            List<string> deletedServiceHistoryList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgServiceHistory.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedServiceHistoryList.Add(row["ServiceHistoryGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedServiceHistoryList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những dịch vụ mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = ServiceHistoryBus.DeleteServiceHistory(deletedServiceHistoryList);
                    if (result.IsOK)
                        base.RaiseServiceHistoryChanged();
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServiceHistoryBus.DeleteServiceHistory"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ServiceHistoryBus.DeleteServiceHistory"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những dịch vụ cần xóa.", IconType.Information);
        }

        public void DisplayAsThread()
        {
            UpdateGUI();
            if (_patientRow == null) return;

            try
            {
                DataRow row = _patientRow as DataRow;
                _patientGUID = row["PatientGUID"].ToString();
                if (!_isDailyService)
                {
                    _isAll = raAll.Checked;
                    _fromDate = new DateTime(dtpkFromDate.Value.Year, dtpkFromDate.Value.Month, dtpkFromDate.Value.Day, 0, 0, 0);
                    _toDate = new DateTime(dtpkToDate.Value.Year, dtpkToDate.Value.Month, dtpkToDate.Value.Day, 23, 59, 59);
                }
                else
                {
                    _isAll = false;
                    _fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                    _toDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                }
                
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayServiceHistoryProc));
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

        public void HighlightPaidServices()
        {
            foreach (DataGridViewRow row in dgServiceHistory.Rows)
            {
                DataRow dr = (row.DataBoundItem as DataRowView).Row;
                bool isExported = Convert.ToBoolean(dr["IsExported"]);
                if (isExported)
                    row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
            }
        }

        private void OnDisplayServicesHistory()
        {
            Result result = ServiceHistoryBus.GetServiceHistory(_patientGUID, _isAll, _fromDate, _toDate);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgServiceHistory.DataSource = result.QueryResult;
                    CalculateTotalPrice();
                    HighlightPaidServices();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServiceHistoryBus.GetServiceHistory"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ServiceHistoryBus.GetServiceHistory"));
            }
        }

        private void CalculateTotalPrice()
        {
            if (!Global.AllowShowServiePrice) return;

            double totalPrice = 0;
            double totalPriceReceipt = 0;
            DataTable dt = dgServiceHistory.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0)
            {
                lbTotalPrice.Text = "Tổng tiền tất cả: 0 (VNĐ)";
                lbTotalReceipt.Text = "Tổng tiền thu: 0 (VNĐ)";
                lbPay.Text = "Còn lại: 0 (VNĐ)";
            }
            else
            {
                foreach (DataRow row in dt.Rows)
                {
                    double price = Convert.ToDouble(row["FixedPrice"]);
                    bool isChecked = Convert.ToBoolean(row["Checked"]);
                    totalPrice += price;

                    if (isChecked) totalPriceReceipt += price;
                }

                lbTotalPrice.Text = string.Format("Tổng tiền tất cả: {0:#,###} (VNĐ)", totalPrice);

                if (totalPriceReceipt > 0)
                    lbTotalReceipt.Text = string.Format("Tổng tiền thu: {0:#,###} (VNĐ)", totalPriceReceipt);
                else
                    lbTotalReceipt.Text = "Tổng tiền thu: 0 (VNĐ)";
                
                double promotionPrice = 0;
                if (raPercentage.Checked)
                    promotionPrice = (totalPriceReceipt * (double)numPercentage.Value) / 100;
                else
                    promotionPrice = (double)numAmount.Value;

                if (totalPriceReceipt - promotionPrice == 0)
                    lbPay.Text = "Còn lại: 0 (VNĐ)";
                else
                    lbPay.Text = string.Format("Còn lại: {0:#,###} (VNĐ)", totalPriceReceipt - promotionPrice);
            }
        }

        private void OnPrint(string receiptGUID)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dgServiceHistory.RowCount <= 0) return;

            string exportFileName = string.Format("{0}\\Temp\\Receipt.xls", Application.StartupPath);
            if (ExportToExcel(exportFileName, receiptGUID))
                ExcelPrintPreview.Print(exportFileName);
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
                int rowIndex = 6;

                workSheet.Cells["B2"].Value = receipt.FullName;
                workSheet.Cells["B3"].Value = receipt.Collector;
                workSheet.Cells["B4"].Value = receipt.ReceiptDate.ToString("dd/MM/yyyy HH:mm:ss");

                DataTable dtSource = result.QueryResult as DataTable;
                foreach (DataRow row in dtSource.Rows)
                {
                    string serviceCode = row["Code"].ToString();
                    string serviceName = row["Name"].ToString();
                    double price = Convert.ToDouble(row["Price"]);

                    workSheet.Cells[rowIndex, 0].Value = serviceCode;
                    workSheet.Cells[rowIndex, 1].Value = serviceName;
                    workSheet.Cells[rowIndex, 2].Value = price.ToString("#,###");

                    rowIndex++;
                }

                IRange range = workSheet.Cells[string.Format("A7:C{0}", dtSource.Rows.Count + 6)];
                range.WrapText = true;
                range.HorizontalAlignment = HAlign.General;
                range.VerticalAlignment = VAlign.Top;
                range.Borders.Color = Color.Black;

                range = workSheet.Cells[string.Format("B{0}", dtSource.Rows.Count + 7)];
                range.Value = "Tổng tiền:";
                range.HorizontalAlignment = HAlign.Right;

                range = workSheet.Cells[string.Format("C{0}", dtSource.Rows.Count + 7)];
                if (receipt.TotalPrice > 0)
                    range.Value = receipt.TotalPrice.ToString("#,###");
                else
                    range.Value = "0";

                range = workSheet.Cells[string.Format("B{0}", dtSource.Rows.Count + 8)];
                range.Value = "Giảm giá:";
                range.HorizontalAlignment = HAlign.Right;

                range = workSheet.Cells[string.Format("C{0}", dtSource.Rows.Count + 8)];
                if (receipt.Promotion > 0)
                    range.Value = receipt.Promotion.ToString("#,###");
                else
                    range.Value = "0";

                range.HorizontalAlignment = HAlign.Right;

                range = workSheet.Cells[string.Format("B{0}", dtSource.Rows.Count + 9)];
                range.Value = "Còn lại:";
                range.HorizontalAlignment = HAlign.Right;

                range = workSheet.Cells[string.Format("C{0}", dtSource.Rows.Count + 9)];
                if (receipt.Payment > 0)
                    range.Value = receipt.Payment.ToString("#,###");
                else
                    range.Value = "0";

                range = workSheet.Cells[string.Format("A{0}", dtSource.Rows.Count + 11)];
                range.Value = "Bệnh nhân";
                range.HorizontalAlignment = HAlign.Right;

                range = workSheet.Cells[string.Format("C{0}", dtSource.Rows.Count + 11)];
                range.Value = "Người thu";

                string path = string.Format("{0}\\Temp", Application.StartupPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                workBook.SaveAs(exportFileName, SpreadsheetGear.FileFormat.XLS97);
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

        private void OnExportReceipt() 
        {
            List<DataRow> paidServiceList = new List<DataRow>();
            List<DataRow> noPaidServiceList = new List<DataRow>();
            List<DataRow> checkedRows = CheckedServiceRows;
            List<string> serviceHistoryKeys = new List<string>();
            
            foreach (DataRow row in checkedRows)
            {
                
                bool isExported = Convert.ToBoolean(row["IsExported"]);
                if (!isExported)
                {
                    noPaidServiceList.Add(row);
                    serviceHistoryKeys.Add(row["ServiceHistoryGUID"].ToString());
                }
                else
                    paidServiceList.Add(row);
            }

            if (paidServiceList.Count <= 0)
            {
                List<ReceiptDetail> receiptDetails = new List<ReceiptDetail>();
                double totalPrice = 0;
                foreach (DataRow row in noPaidServiceList)
                {
                    double price = Convert.ToDouble(row["FixedPrice"]);
                    totalPrice += price;
                    ReceiptDetail detail = new ReceiptDetail();
                    detail.ServiceGUID = Guid.Parse(row["ServiceGUID"].ToString());
                    detail.Price = Convert.ToDouble(row["FixedPrice"]);
                    detail.Note = row["Note"].ToString();
                    detail.CreatedDate = DateTime.Now;
                    detail.CreatedBy = Guid.Parse(Global.UserGUID);
                    detail.Status = (byte)Status.Actived;
                    receiptDetails.Add(detail);
                }

                double promotionPrice = 0;
                if (raPercentage.Checked)
                    promotionPrice = (totalPrice * (double)numPercentage.Value) / 100;
                else
                    promotionPrice = (double)numAmount.Value;

                double payment = totalPrice - promotionPrice;

                Receipt receipt = new Receipt();
                receipt.PatientGUID = Guid.Parse(_patientGUID);
                receipt.Collector = Global.Fullname;
                receipt.ReceiptDate = DateTime.Now;
                receipt.TotalPrice = totalPrice;
                receipt.Promotion = promotionPrice;
                receipt.Payment = payment;
                receipt.Status = (byte)Status.Actived;
                receipt.CreatedDate = DateTime.Now;
                receipt.CreatedBy = Guid.Parse(Global.UserGUID);

                Result result = ReceiptBus.InsertReceipt(receipt, receiptDetails, serviceHistoryKeys);
                if (result.IsOK)
                {
                    DisplayAsThread();
                    if (MsgBox.Question(Application.ProductName, "Bạn có muốn in phiếu thu ?") == DialogResult.Yes)
                        OnPrint(receipt.ReceiptGUID.ToString());
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.InsertReceipt"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.InsertReceipt"));
                }
            }
            else
            {
                if (noPaidServiceList.Count <= 0)
                {
                    if (MsgBox.Question(Application.ProductName, "Những dịch vụ này đã xuất phiếu thu rồi. Bạn có muốn xuất lại phiếu thu ?") == DialogResult.Yes)
                    {
                        List<ReceiptDetail> receiptDetails = new List<ReceiptDetail>();
                        double totalPrice = 0;
                        foreach (DataRow row in paidServiceList)
                        {
                            double price = Convert.ToDouble(row["FixedPrice"]);
                            totalPrice += price;
                            ReceiptDetail detail = new ReceiptDetail();
                            detail.ServiceGUID = Guid.Parse(row["ServiceGUID"].ToString());
                            detail.Price = Convert.ToDouble(row["FixedPrice"]);
                            detail.Note = row["Note"].ToString();
                            detail.CreatedDate = DateTime.Now;
                            detail.CreatedBy = Guid.Parse(Global.UserGUID);
                            detail.Status = (byte)Status.Actived;
                            receiptDetails.Add(detail);
                        }

                        double promotionPrice = 0;
                        if (raPercentage.Checked)
                            promotionPrice = (totalPrice * (double)numPercentage.Value) / 100;
                        else
                            promotionPrice = (double)numAmount.Value;

                        double payment = totalPrice - promotionPrice;

                        Receipt receipt = new Receipt();
                        receipt.PatientGUID = Guid.Parse(_patientGUID);
                        receipt.Collector = Global.Fullname;
                        receipt.ReceiptDate = DateTime.Now;
                        receipt.TotalPrice = totalPrice;
                        receipt.Promotion = promotionPrice;
                        receipt.Payment = payment;
                        receipt.Status = (byte)Status.Actived;
                        receipt.CreatedDate = DateTime.Now;
                        receipt.CreatedBy = Guid.Parse(Global.UserGUID);

                        Result result = ReceiptBus.InsertReceipt(receipt, receiptDetails, serviceHistoryKeys);
                        if (result.IsOK)
                        {
                            DisplayAsThread();
                            if (MsgBox.Question(Application.ProductName, "Bạn có muốn in phiếu thu ?") == DialogResult.Yes)
                                OnPrint(receipt.ReceiptGUID.ToString());
                        }
                        else
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.InsertReceipt"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.InsertReceipt"));
                        }
                    }
                }
                else
                {
                    List<ReceiptDetail> receiptDetails = new List<ReceiptDetail>();
                    double totalPrice = 0;
                    foreach (DataRow row in noPaidServiceList)
                    {
                        double price = Convert.ToDouble(row["FixedPrice"]);
                        totalPrice += price;
                        ReceiptDetail detail = new ReceiptDetail();
                        detail.ServiceGUID = Guid.Parse(row["ServiceGUID"].ToString());
                        detail.Price = Convert.ToDouble(row["FixedPrice"]);
                        detail.Note = row["Note"].ToString();
                        detail.CreatedDate = DateTime.Now;
                        detail.CreatedBy = Guid.Parse(Global.UserGUID);
                        detail.Status = (byte)Status.Actived;
                        receiptDetails.Add(detail);
                    }

                    if (MsgBox.Question(Application.ProductName, "Có 1 số dịch vụ đã xuất phiếu thu rồi. Bạn có muốn xuất phiếu thu lần nữa ?") == DialogResult.Yes)
                    {
                        foreach (DataRow row in paidServiceList)
                        {
                            double price = Convert.ToDouble(row["FixedPrice"]);
                            totalPrice += price;
                            ReceiptDetail detail = new ReceiptDetail();
                            detail.ServiceGUID = Guid.Parse(row["ServiceGUID"].ToString());
                            detail.Price = Convert.ToDouble(row["FixedPrice"]);
                            detail.Note = row["Note"].ToString();
                            detail.CreatedDate = DateTime.Now;
                            detail.CreatedBy = Guid.Parse(Global.UserGUID);
                            detail.Status = (byte)Status.Actived;
                            receiptDetails.Add(detail);
                        }
                    }

                    double promotionPrice = 0;
                    if (raPercentage.Checked)
                        promotionPrice = (totalPrice * (double)numPercentage.Value) / 100;
                    else
                        promotionPrice = (double)numAmount.Value;

                    double payment = totalPrice - promotionPrice;

                    Receipt receipt = new Receipt();
                    receipt.PatientGUID = Guid.Parse(_patientGUID);
                    receipt.Collector = Global.Fullname;
                    receipt.ReceiptDate = DateTime.Now;
                    receipt.TotalPrice = totalPrice;
                    receipt.Promotion = promotionPrice;
                    receipt.Payment = payment;
                    receipt.Status = (byte)Status.Actived;
                    receipt.CreatedDate = DateTime.Now;
                    receipt.CreatedBy = Guid.Parse(Global.UserGUID);

                    Result result = ReceiptBus.InsertReceipt(receipt, receiptDetails, serviceHistoryKeys);
                    if (result.IsOK)
                    {
                        DisplayAsThread();
                        if (MsgBox.Question(Application.ProductName, "Bạn có muốn in phiếu thu ?") == DialogResult.Yes)
                            OnPrint(receipt.ReceiptGUID.ToString());
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.InsertReceipt"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.InsertReceipt"));
                    }
                }
            }
        }
        #endregion

        #region Window Event Handlers
        private void btnExportReceipt_Click(object sender, EventArgs e)
        {
            if (dgServiceHistory.RowCount <= 0) return;
            if (MsgBox.Question(Application.ProductName, "Bạn có muốn xuất phiếu thu ?") == DialogResult.Yes)
            {
                OnExportReceipt();
            }
        }

        private void raAll_CheckedChanged(object sender, EventArgs e)
        {
            dtpkFromDate.Enabled = !raAll.Checked;
            dtpkToDate.Enabled = !raAll.Checked;
            btnSearch.Enabled = !raAll.Checked;

            DisplayAsThread();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgServiceHistory.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }

            CalculateTotalPrice();
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

        private void dtpk_ValueChanged(object sender, EventArgs e)
        {
            //if (_fromDate.ToString("dd/MM/yyyy") == dtpkFromDate.Value.ToString("dd/MM/yyyy") &&
            //    _toDate.ToString("dd/MM/yyyy") == dtpkToDate.Value.ToString("dd/MM/yyyy")) return;

            //DisplayAsThread();
        }

        private void dgServiceHistory_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void dtpk_Leave(object sender, EventArgs e)
        {
            //if (_fromDate.ToString("dd/MM/yyyy") == dtpkFromDate.Value.ToString("dd/MM/yyyy") &&
            //    _toDate.ToString("dd/MM/yyyy") == dtpkToDate.Value.ToString("dd/MM/yyyy")) return;

            //DisplayAsThread();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void uServiceHistory_Load(object sender, EventArgs e)
        {
            //DisplayAsThread();
        }

        private void raPercentage_CheckedChanged(object sender, EventArgs e)
        {
            numPercentage.Enabled = raPercentage.Checked;
            numAmount.Enabled = !raPercentage.Checked;
            CalculateTotalPrice();
        }

        private void numPercentage_ValueChanged(object sender, EventArgs e)
        {
            CalculateTotalPrice();
        }

        private void numAmount_ValueChanged(object sender, EventArgs e)
        {
            CalculateTotalPrice();
        }

        private void numPercentage_KeyUp(object sender, KeyEventArgs e)
        {
            if (numPercentage.Text == string.Empty)
                numPercentage.Value = 0;
            CalculateTotalPrice();
        }

        private void numAmount_KeyUp(object sender, KeyEventArgs e)
        {
            if (numAmount.Text == string.Empty)
                numAmount.Value = 0;

            CalculateTotalPrice();
        }

        private void dgServiceHistory_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void dgServiceHistory_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            if (dgServiceHistory.Columns[e.ColumnIndex].Name == "colChecked")
            {
                CalculateTotalPrice();
            }
        }

        private void dgServiceHistory_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            HighlightPaidServices();
        }
        #endregion

        #region Working Thread
        private void OnDisplayServiceHistoryProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayServicesHistory();
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
