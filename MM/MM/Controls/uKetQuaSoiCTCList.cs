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
using MM.Exports;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uKetQuaSoiCTCList : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        private string _patientGUID = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private bool _isPrint = false;
        private KetQuaSoiCTC _ketQuaSoiCTC = new KetQuaSoiCTC();
        #endregion

        #region Constructor
        public uKetQuaSoiCTCList()
        {
            InitializeComponent();
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
            btnAdd.Enabled = Global.AllowAddKhamNoiSoi;
            btnDelete.Enabled = Global.AllowDeleteKhamNoiSoi;
            btnPrint.Enabled = Global.AllowPrintKhamNoiSoi;
            btnPrintPreview.Enabled = Global.AllowPrintKhamNoiSoi;
            btnExportExcel.Enabled = Global.AllowExportKhamNoiSoi;
        }

        public void DisplayAsThread()
        {
            UpdateGUI();
            if (_patientRow == null) return;

            try
            {
                _patientGUID = _patientRow["PatientGUID"].ToString();
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


                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayKetQuaSoiCTCListProc));
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

        private void ClearData()
        {
            DataTable dt = dgSoCTC.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgSoCTC.DataSource = null;
            }
        }

        private void OnDisplayKetQuaSoiCTCList()
        {
            Result result = KetQuaSoiCTCBus.GetKetQuaSoiCTCList(_patientGUID, _fromDate, _toDate);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    DataTable dt = result.QueryResult as DataTable;
                    dgSoCTC.DataSource = dt;

                    if (_isPrint)
                    {
                        DataRow[] rows = dt.Select(string.Format("KetQuaSoiCTCGUID='{0}'", _ketQuaSoiCTC.KetQuaSoiCTCGUID.ToString()));
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
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaSoiCTCBus.GetKetQuaSoiCTCList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaSoiCTCBus.GetKetQuaSoiCTCList"));
            }
        }

        private void OnAdd()
        {
            _isPrint = false;
            _ketQuaSoiCTC = null;
            dlgAddKetQuaSoiCTC dlg = new dlgAddKetQuaSoiCTC(_patientGUID);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _isPrint = dlg.IsPrint;
                _ketQuaSoiCTC = dlg.KetQuaSoiCTC;
                DisplayAsThread();
            }
        }

        private void OnEdit()
        {
            _isPrint = false;
            _ketQuaSoiCTC = null;
            if (dgSoCTC.SelectedRows == null || dgSoCTC.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 kết quả soi.", IconType.Information);
                return;
            }

            DataRow drKetQuaSoiCTC = (dgSoCTC.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddKetQuaSoiCTC dlg = new dlgAddKetQuaSoiCTC(_patientGUID, drKetQuaSoiCTC, Global.AllowEditKhamCTC);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                _isPrint = dlg.IsPrint;
                _ketQuaSoiCTC = dlg.KetQuaSoiCTC;
                DisplayAsThread();
            }
        }

        private void OnDelete()
        {
            List<string> deletedKQNSList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgSoCTC.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedKQNSList.Add(row["KetQuaSoiCTCGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedKQNSList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những kết quả soi mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = KetQuaSoiCTCBus.DeleteKetQuaSoiCTC(deletedKQNSList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaSoiCTCBus.DeleteKetQuaSoiCTC"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaSoiCTCBus.DeleteKetQuaSoiCTC"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những kết quả soi.", IconType.Information);
        }

        private List<DataRow> GetCheckedRows()
        {
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgSoCTC.DataSource as DataTable;
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
                string exportFileName = string.Format("{0}\\Temp\\KetQuaSoiCTC.xls", Application.StartupPath);
                if (isPreview)
                {
                    foreach (DataRow row in checkedRows)
                    {
                        if (!ExportExcel.ExportKetQuaSoiCTCToExcel(exportFileName, _patientRow, row))
                            return;
                        else
                        {
                            try
                            {
                                ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaSoiCTCTemplate));
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                                return;
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
                            if (!ExportExcel.ExportKetQuaSoiCTCToExcel(exportFileName, _patientRow, row))
                                return;
                            else
                            {
                                try
                                {
                                    ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaSoiCTCTemplate));
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
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những kết quả soi cần in.", IconType.Information);
        }

        private void OnPrint(DataRow drKetQuaSoiCTC)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (_printDialog.ShowDialog() == DialogResult.OK)
            {
                string exportFileName = string.Format("{0}\\Temp\\KetQuaSoiCTC.xls", Application.StartupPath);

                if (!ExportExcel.ExportKetQuaSoiCTCToExcel(exportFileName, _patientRow, drKetQuaSoiCTC))
                    return;
                else
                {
                    try
                    {
                        ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.KetQuaSoiCTCTemplate));
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                        return;
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
                        if (!ExportExcel.ExportKetQuaSoiCTCToExcel(dlg.FileName, _patientRow, row))
                            return;
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những kết quả soi cần xuất excel.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
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

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgSoCTC.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
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
            OnExportExcel();
        }

        private void dgSoCTC_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
        }
        #endregion

        #region Working Thread
        private void OnDisplayKetQuaSoiCTCListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayKetQuaSoiCTCList();
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
