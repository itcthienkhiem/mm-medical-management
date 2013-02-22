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
using MM.Databasae;
using MM.Bussiness;
using MM.Dialogs;
using MM.Exports;

namespace MM.Controls
{
    public partial class uToaThuocList : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private bool _isAll = true;
        #endregion

        #region Constructor
        public uToaThuocList()
        {
            InitializeComponent();
            dtpkFromDate.Value = DateTime.Now.AddDays(-1);
            dtpkToDate.Value = DateTime.Now;
        }
        #endregion

        #region Properties
        public DataRow PatientRow
        {
            get { return _patientRow; }
            set { _patientRow = value; }
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            //btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
            btnPrint.Enabled = AllowPrint;
            btnPrintPreview.Enabled = AllowPrint;

            addToolStripMenuItem.Enabled = AllowAdd;
            deleteToolStripMenuItem.Enabled = AllowDelete;
            printPreviewToolStripMenuItem.Enabled = AllowPrint;
            printToolStripMenuItem.Enabled = AllowPrint;
        }

        public void ClearData()
        {
            DataTable dt = dgToaThuoc.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgToaThuoc.DataSource = null;
            }
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;

                _isAll = raAll.Checked;
                _fromDate = new DateTime(dtpkFromDate.Value.Year, dtpkFromDate.Value.Month, dtpkFromDate.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkToDate.Value.Year, dtpkToDate.Value.Month, dtpkToDate.Value.Day, 23, 59, 59);

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayToaThuocListProc));
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

        private void OnDisplayToaThuocList()
        {
            Result result = null;
            if (_patientRow == null)
                result = KeToaBus.GetToaThuocList(_isAll, _fromDate, _toDate);
            else
            {
                string patientGUID = _patientRow["PatientGUID"].ToString();
                result = KeToaBus.GetToaThuocList(patientGUID, Global.UserGUID, _isAll, _fromDate, _toDate);
            }

            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    dgToaThuoc.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KeToaBus.GetToaThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KeToaBus.GetToaThuocList"));
            }
        }

        private void SelectLastedRow()
        {
            dgToaThuoc.CurrentCell = dgToaThuoc[1, dgToaThuoc.RowCount - 1];
            dgToaThuoc.Rows[dgToaThuoc.RowCount - 1].Selected = true;
        }

        private void OnAddToaThuoc()
        {
            dlgAddToaThuoc dlg = new dlgAddToaThuoc();
            if (_patientRow != null) dlg.PatientRow = _patientRow;
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnEditToaThuoc()
        {
            if (dgToaThuoc.SelectedRows == null || dgToaThuoc.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 toa thuốc.", IconType.Information);
                return;
            }

            DataRow drToaThuoc = (dgToaThuoc.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddToaThuoc dlg = new dlgAddToaThuoc(drToaThuoc, AllowEdit);
            if (_patientRow != null) dlg.PatientRow = _patientRow;
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnDeleteToaThuoc()
        {
            List<string> deletedToaThuocList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgToaThuoc.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedToaThuocList.Add(row["ToaThuocGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedToaThuocList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những toa thuốc mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = KeToaBus.DeleteToaThuoc(deletedToaThuocList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("KeToaBus.DeleteToaThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KeToaBus.DeleteToaThuoc"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những toa thuốc cần xóa.", IconType.Information);
        }

        private void OnPrint(bool isPreview)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgToaThuoc.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    checkedRows.Add(row);
                }
            }

            if (checkedRows.Count > 0)
            {
                string exportFileName = string.Format("{0}\\Temp\\ToaThuoc.xls", Application.StartupPath);
                if (isPreview)
                {
                    foreach (DataRow row in checkedRows)
                    {
                        string toaThuocGUID = row["ToaThuocGUID"].ToString();
                        if (ExportExcel.ExportToaThuocToExcel(exportFileName, toaThuocGUID))
                        {
                            try
                            {
                                ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.ToaThuocTemplate));
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                return;
                            }
                        }
                        else
                            return;
                    }
                }
                else
                {
                    if (_printDialog.ShowDialog() == DialogResult.OK)
                    {
                        foreach (DataRow row in checkedRows)
                        {
                            string toaThuocGUID = row["ToaThuocGUID"].ToString();
                            if (ExportExcel.ExportToaThuocToExcel(exportFileName, toaThuocGUID))
                            {
                                try
                                {
                                    ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.ToaThuocTemplate));
                                }
                                catch (Exception ex)
                                {
                                    MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                    return;
                                }
                            }
                            else
                                return;
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những toa thuốc cần in.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddToaThuoc();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditToaThuoc();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteToaThuoc();
        }

        private void dgThuoc_DoubleClick(object sender, EventArgs e)
        {
            //if (!AllowEdit) return;
            OnEditToaThuoc();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgToaThuoc.DataSource as DataTable;
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

        private void raAll_CheckedChanged(object sender, EventArgs e)
        {
            dtpkFromDate.Enabled = !raAll.Checked;
            dtpkToDate.Enabled = !raAll.Checked;
            btnSearch.Enabled = !raAll.Checked;

            DisplayAsThread();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAddToaThuoc();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnEditToaThuoc();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDeleteToaThuoc();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }
        #endregion

        #region Working Thread
        private void OnDisplayToaThuocListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayToaThuocList();
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
