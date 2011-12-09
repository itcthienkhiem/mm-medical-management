using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using MM.Bussiness;
using MM.Databasae;
using MM.Common;
using MM.Dialogs;
using MM.Exports;
using SpreadsheetGear;

namespace MM.Controls
{
    public partial class uInvoiceList : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uInvoiceList()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Commnad
        private void UpdateGUI()
        {
            btnDelete.Enabled = AllowDelete;
            btnPrint.Enabled = AllowPrint;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
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
            Result result = InvoiceBus.GetInvoiceList();
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
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("InvoiceBus.GetInvoiceList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("InvoiceBus.GetInvoiceList"));
            }
        }

        public void ClearData()
        {
            dgInvoice.DataSource = null;
        }

        private void OnDeleteInvoice()
        {
            List<string> deletedInvoiceList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgInvoice.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedInvoiceList.Add(row["invoiceGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedInvoiceList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những hóa đơn mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = InvoiceBus.DeleteInvoices(deletedInvoiceList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("InvoiceBus.DeleteInvoices"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("InvoiceBus.DeleteInvoices"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những hóa đơn cần xóa.", IconType.Information);
        }

        private void OnPrint()
        {
            List<string> checkedInvoicetList = new List<string>();
            DataTable dt = dgInvoice.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    checkedInvoicetList.Add(row["invoiceGUID"].ToString());
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
                            foreach (string invoiceGUID in checkedInvoicetList)
                            {
                                if (dlg.Lien1)
                                {
                                    if (ExportExcel.ExportInvoiceToExcel(exportFileName, invoiceGUID, "                                   Liên 1: Lưu"))
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

                                if (dlg.Lien2)
                                {
                                    if (ExportExcel.ExportInvoiceToExcel(exportFileName, invoiceGUID, "                                   Liên 2: Giao người mua"))
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

                                if (dlg.Lien3)
                                {
                                    if (ExportExcel.ExportInvoiceToExcel(exportFileName, invoiceGUID, "                                   Liên 3: Nội bộ"))
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
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những hóa đơn cần in.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteInvoice();
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
