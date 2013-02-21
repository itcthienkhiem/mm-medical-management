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
    public partial class uBenhNhanNgoaiGoiKhamList : uBase
    {
        #region Members
        private DateTime _tuNgay = DateTime.Now;
        private DateTime _denNgay = DateTime.Now;
        private string _tenBenhNhan = string.Empty;
        private int _type = 0; //0: Tên bệnh nhân; 1: Mã bệnh nhân
        #endregion

        #region Constructor
        public uBenhNhanNgoaiGoiKhamList()
        {
            InitializeComponent();

            dtpkDenNgay.Value = DateTime.Now;
            dtpkTuNgay.Value = DateTime.Now.AddDays(-7);
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
            btnPrint.Enabled = AllowPrint;
            btnPrintPreview.Enabled = AllowPrint;
            btnExportExcel.Enabled = AllowExport;

            addToolStripMenuItem.Enabled = AllowAdd;
            editToolStripMenuItem.Enabled = AllowEdit;
            deleteToolStripMenuItem.Enabled = AllowDelete;
            printPreviewToolStripMenuItem.Enabled = AllowPrint;
            printToolStripMenuItem.Enabled = AllowPrint;
            exportExcelToolStripMenuItem.Enabled = AllowExport;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
                chkChecked.Checked = false;
                _tuNgay = dtpkTuNgay.Value;
                _denNgay = dtpkDenNgay.Value;
                _tenBenhNhan = txtBenhNhan.Text.Trim();
                if (chkMaBenhNhan.Checked) _type = 1;
                else _type = 0;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayBenhNhanNgoaiGoiKhamListProc));
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
            DataTable dt = dgBenhNhanNgoaiGoiKham.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgBenhNhanNgoaiGoiKham.DataSource = null;
            }
        }

        private void OnDisplayBenhNhanNgoaiGoiKhamList()
        {
            Result result = BenhNhanNgoaiGoiKhamBus.GetBenhNhanNgoaiGoiKhamList(_tuNgay, _denNgay, _tenBenhNhan, _type);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    DataTable dt = result.QueryResult as DataTable;
                    dgBenhNhanNgoaiGoiKham.DataSource = result.QueryResult as DataTable;
                    lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dt.Rows.Count);
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("BenhNhanNgoaiGoiKhamBus.GetBenhNhanNgoaiGoiKhamList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("BenhNhanNgoaiGoiKhamBus.GetBenhNhanNgoaiGoiKhamList"));
            }
        }

        private void OnAdd()
        {
            DataTable dtSource = (dgBenhNhanNgoaiGoiKham.DataSource as DataTable).Clone();
            dlgAddBenhNhanNgoaiGoiKham dlg = new dlgAddBenhNhanNgoaiGoiKham(dtSource);
            if (dlg.ShowDialog(this) == DialogResult.OK)
                DisplayAsThread();
        }

        private void OnEdit()
        {
            if (dgBenhNhanNgoaiGoiKham.SelectedRows == null || dgBenhNhanNgoaiGoiKham.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 bệnh nhân ngoài gói khám.", IconType.Information);
                return;
            }

            DataRow drBenhNhanNgoaiGoiKham = (dgBenhNhanNgoaiGoiKham.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string nguoiTaoGUID = drBenhNhanNgoaiGoiKham["CreatedBy"].ToString();
            if (nguoiTaoGUID != Global.UserGUID && !AllowConfirm)
            {
                MsgBox.Show(Application.ProductName, "Bạn không thể sửa bệnh nhân ngoài gói khám do người khác tạo. Vui lòng kiểm tra lại.", IconType.Information);
                return;
            }

            dlgEditBenhNhanNgoaiGoiKham dlg = new dlgEditBenhNhanNgoaiGoiKham(drBenhNhanNgoaiGoiKham);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnDelete()
        {
            List<string> deletedKeysList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgBenhNhanNgoaiGoiKham.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedKeysList.Add(row["BenhNhanNgoaiGoiKhamGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedKeysList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những bệnh nhân ngoài gói khám mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    foreach (DataRow row in deletedRows)
                    {
                        string nguoiTaoGUID = row["CreatedBy"].ToString();
                        if (nguoiTaoGUID != Global.UserGUID)
                        {
                            MsgBox.Show(Application.ProductName, "Bạn không thể xóa bệnh nhân ngoài gói khám do người khác tạo. Vui lòng kiểm tra lại.", IconType.Information);
                            return;
                        }
                    }

                    Result result = BenhNhanNgoaiGoiKhamBus.DeleteBenhNhanNgoaiGoiKham(deletedKeysList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("BenhNhanNgoaiGoiKhamBus.DeleteBenhNhanNgoaiGoiKham"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("BenhNhanNgoaiGoiKhamBus.DeleteBenhNhanNgoaiGoiKham"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân ngoài gói khám cần xóa.", IconType.Information);
        }

        private List<DataRow> GetCheckedRows()
        {
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgBenhNhanNgoaiGoiKham.DataSource as DataTable;
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
                string exportFileName = string.Format("{0}\\Temp\\BenhNhanNgoaiGoiKham.xls", Application.StartupPath);
                if (isPreview)
                {
                    if (!ExportExcel.ExportDanhSachBenhNhanNgoaiGoiKhamToExcel(exportFileName, checkedRows))
                        return;
                    else
                    {
                        try
                        {
                            ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.BenhNhanNgoaiGoiKhamTemplate));
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
                        if (!ExportExcel.ExportDanhSachBenhNhanNgoaiGoiKhamToExcel(exportFileName, checkedRows))
                            return;
                        else
                        {
                            try
                            {
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.BenhNhanNgoaiGoiKhamTemplate));
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
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân ngoài gói khám cần in.", IconType.Information);
        }

        private void OnExportExcell()
        {
            Cursor.Current = Cursors.WaitCursor;
            List<DataRow> checkedRows = GetCheckedRows();

            if (checkedRows.Count > 0)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (!ExportExcel.ExportDanhSachBenhNhanNgoaiGoiKhamToExcel(dlg.FileName, checkedRows))
                        return;
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân ngoài gói khám cần xuất excel.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
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
            OnExportExcell();
        }

        private void dgBenhNhanNgoaiGoiKham_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEdit();
        }

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

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgBenhNhanNgoaiGoiKham.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
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

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void exportExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExportExcell();
        }
        #endregion

        #region Working Thread
        private void OnDisplayBenhNhanNgoaiGoiKhamListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayBenhNhanNgoaiGoiKhamList();
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
