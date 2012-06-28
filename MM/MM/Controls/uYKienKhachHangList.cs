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
    public partial class uYKienKhachHangList : uBase
    {
        #region Members
        private string _tenBenhNhan = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private int _type = 0; //0: From date to date; 1: Tên bệnh nhân; 2: Tên người tạo
        private string _tenNguoiTao = string.Empty;
        #endregion

        #region Constructor
        public uYKienKhachHangList()
        {
            InitializeComponent();
            dtpkDenNgay.Value = DateTime.Now;
            dtpkTuNgay.Value = DateTime.Now.AddDays(-30);
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        public void ClearData()
        {
            dgYKienKhachHang.DataSource = null;
        }

        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
            btnPrint.Enabled = AllowPrint;
            btnPrintPreview.Enabled = AllowPrint;
            btnExportExcel.Enabled = AllowExport;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;

                //if (raTenBenhNhan.Checked && txtTenBenhNhan.Text.Trim() == string.Empty)
                //{
                //    MsgBox.Show(Application.ProductName, "Vui lòng nhập tên khách hàng cần tìm.", IconType.Information);
                //    txtTenBenhNhan.Focus();
                //    return;
                //}

                //if (raTenNguoiTao.Checked && txtTenNguoiTao.Text.Trim() == string.Empty)
                //{
                //    MsgBox.Show(Application.ProductName, "Vui lòng nhập tên người tạo cần tìm.", IconType.Information);
                //    txtTenNguoiTao.Focus();
                //    return;
                //}

                if (raTuNgayToiNgay.Checked) _type = 0;
                else if (raTenBenhNhan.Checked) _type = 1;
                else _type = 2;

                _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                _tenBenhNhan = txtTenBenhNhan.Text;
                _tenNguoiTao = txtTenNguoiTao.Text;

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayYKienKhachHangListProc));
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

        private void OnDisplayYKienKhachHangList()
        {
            Result result = YKienKhachHangBus.GetYKienKhachHangList(_type, _fromDate, _toDate, _tenBenhNhan, _tenNguoiTao);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgYKienKhachHang.DataSource = result.QueryResult;
                    RefreshNo();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("YKienKhachHangBus.GetYKienKhachHangList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("YKienKhachHangBus.GetYKienKhachHangList"));
            }
        }

        private void RefreshNo()
        {
            int index = 1;
            foreach (DataGridViewRow row in dgYKienKhachHang.Rows)
            {
                row.Cells["STT"].Value = index++;
            }
        }

        private void OnAdd()
        {
            dlgAddYKienKhachHang dlg = new dlgAddYKienKhachHang();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnEdit()
        {
            if (dgYKienKhachHang.SelectedRows == null || dgYKienKhachHang.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 ý kiến khách hàng.", IconType.Information);
                return;
            }

            DataRow drYKienKhachHang = (dgYKienKhachHang.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddYKienKhachHang dlg = new dlgAddYKienKhachHang(drYKienKhachHang);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnDelete()
        {
            List<string> deletedSpecList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgYKienKhachHang.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    string userGUID = row["ContactBy"].ToString();
                    if (userGUID != Global.UserGUID)
                    {
                        MsgBox.Show(Application.ProductName, "Bạn không thể xóa ý kiến khách hàng của người khác. Vui lòng kiểm tra lại.", IconType.Information);
                        return;
                    }

                    deletedSpecList.Add(row["YKienKhachHangGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedSpecList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những ý kiến khách hàng mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = YKienKhachHangBus.DeleteYKienKhachHang(deletedSpecList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("YKienKhachHangBus.DeleteYKienKhachHang"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("YKienKhachHangBus.DeleteYKienKhachHang"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những ý kiến khách hàng cần xóa.", IconType.Information);
        }

        private List<DataRow> GetCheckedRows()
        {
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgYKienKhachHang.DataSource as DataTable;
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
                string exportFileName = string.Format("{0}\\Temp\\YKienKhachHang.xls", Application.StartupPath);
                if (isPreview)
                {
                    if (!ExportExcel.ExportYKienKhachHangToExcel(exportFileName, checkedRows))
                        return;
                    else
                    {
                        try
                        {
                            ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.YKienKhachHangTemplate));
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
                        if (!ExportExcel.ExportYKienKhachHangToExcel(exportFileName, checkedRows))
                            return;
                        else
                        {
                            try
                            {
                                ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.YKienKhachHangTemplate));
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
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những ý kiến khách hàng cần in.", IconType.Information);
        }

        private void OnExportToExcel()
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
                    if (!ExportExcel.ExportYKienKhachHangToExcel(dlg.FileName, checkedRows))
                        return;
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những ý kiến khách hàng cần xuất excel.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void dgYKienKhachHang_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void dgYKienKhachHang_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            RefreshNo();
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

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgYKienKhachHang.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void raTuNgayToiNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpkTuNgay.Enabled = raTuNgayToiNgay.Checked;
            dtpkDenNgay.Enabled = raTuNgayToiNgay.Checked;
        }

        private void raTenBenhNhan_CheckedChanged(object sender, EventArgs e)
        {
            txtTenBenhNhan.ReadOnly = !raTenBenhNhan.Checked;
        }

        private void raTenNguoiTao_CheckedChanged(object sender, EventArgs e)
        {
            txtTenNguoiTao.ReadOnly = !raTenNguoiTao.Checked;
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
                MsgBox.Show(Application.ProductName, "Vui lòng nhập tên khách hàng.", IconType.Information);
                txtTenBenhNhan.Focus();
                return;
            }

            DisplayAsThread();
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
            OnExportToExcel();
        }
        #endregion

        #region Working Thread
        private void OnDisplayYKienKhachHangListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayYKienKhachHangList();
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
