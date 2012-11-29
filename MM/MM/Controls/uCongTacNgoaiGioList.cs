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
using MM.Dialogs;
using MM.Exports;

namespace MM.Controls
{
    public partial class uCongTacNgoaiGioList : uBase
    {
        #region Members
        private DateTime _tuNgay = DateTime.Now;
        private DateTime _denNgay = DateTime.Now;
        private string _tenNhanVien = string.Empty;
        #endregion

        #region Constructor
        public uCongTacNgoaiGioList()
        {
            InitializeComponent();
            dtpkTuNgay.Value = DateTime.Now;
            dtpkDenNgay.Value = DateTime.Now;
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
            btnExportExcel.Enabled = AllowExport;
            btnPrint.Enabled = AllowPrint;
            btnPrintPreview.Enabled = AllowPrint;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
                chkChecked.Checked = false;
                _tuNgay = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _denNgay = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                _tenNhanVien = txtTenBenhNhan.Text;
                
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayCongTacNgoaiGioListProc));
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
            DataTable dt = dgCongTacNgoaiGio.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgCongTacNgoaiGio.DataSource = null;
            }
        }

        private void OnDisplayCongTacNgoaiGioList()
        {
            Result result = CongTacNgoaiGioBus.GetCongTacNgoaiGioList(_tuNgay, _denNgay, _tenNhanVien);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    DataTable dt = result.QueryResult as DataTable;
                    dgCongTacNgoaiGio.DataSource = result.QueryResult;
                    lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dt.Rows.Count);
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("CongTacNgoaiGioBus.GetCongTacNgoaiGioList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CongTacNgoaiGioBus.GetCongTacNgoaiGioList"));
            }
        }

        private void OnAdd()
        {
            dlgAddCongTacNgoaiGio dlg = new dlgAddCongTacNgoaiGio();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnEdit()
        {
            if (dgCongTacNgoaiGio.SelectedRows == null || dgCongTacNgoaiGio.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 công tác ngoài giờ.", IconType.Information);
                return;
            }

            DataRow drCongTacNgoaiGio = (dgCongTacNgoaiGio.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (drCongTacNgoaiGio == null) return;

            dlgAddCongTacNgoaiGio dlg = new dlgAddCongTacNgoaiGio(drCongTacNgoaiGio);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnDelete()
        {
            List<string> deletedKeyList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgCongTacNgoaiGio.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedKeyList.Add(row["CongTacNgoaiGioGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedKeyList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những công tác ngoài giờ mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = CongTacNgoaiGioBus.DeleteCongTacNgoaiGio(deletedKeyList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("CongTacNgoaiGioBus.DeleteCongTacNgoaiGio"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("CongTacNgoaiGioBus.DeleteCongTacNgoaiGio"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những công tác ngoài giờ cần xóa.", IconType.Information);
        }

        private void OnPrint(bool isPreview)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = GetCheckedRows();
            if (checkedRows.Count > 0)
            {
                string exportFileName = string.Format("{0}\\Temp\\CongTacNgoaiGio.xls", Application.StartupPath);
                if (isPreview)
                {
                    if (ExportExcel.ExportCongTacNgoaiGioToExcel(exportFileName, checkedRows))
                    {
                        try
                        {
                            ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.CongTacNgoaiGioTemplate));
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                            return;
                        }
                    }
                }
                else
                {
                    if (_printDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (ExportExcel.ExportCongTacNgoaiGioToExcel(exportFileName, checkedRows))
                        {
                            try
                            {
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.CongTacNgoaiGioTemplate));
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
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những công tác ngoài giờ cần in.", IconType.Information);
        }

        private List<DataRow> GetCheckedRows()
        {
            List<DataRow> checkedRows = new List<DataRow>();

            DataTable dt = dgCongTacNgoaiGio.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return checkedRows;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                    checkedRows.Add(row);
            }

            return checkedRows;
        }

        private void OnExportExcel()
        {
            List<DataRow> checkedRows = GetCheckedRows();
            if (checkedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những công tác ngoài giờ cần xuất Excel.", IconType.Information);
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export Excel";
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (!ExportExcel.ExportCongTacNgoaiGioToExcel(dlg.FileName, checkedRows))
                    return;
            }
        }
        #endregion

        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            if (dtpkTuNgay.Value > dtpkDenNgay.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkTuNgay.Focus();
                return;
            }

            DisplayAsThread();
        }

        private void dgCongTacNgoaiGio_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgCongTacNgoaiGio.DataSource as DataTable;
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

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }
        #endregion

        #region Working Thread
        private void OnDisplayCongTacNgoaiGioListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayCongTacNgoaiGioList();
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
