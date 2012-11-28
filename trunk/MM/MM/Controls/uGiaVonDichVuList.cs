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
    public partial class uGiaVonDichVuList : uBase
    {
        #region Members
        private bool _isFromDateToDate = true;
        private string _tenDichVu = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        #endregion

        #region Constructor
        public uGiaVonDichVuList()
        {
            InitializeComponent();
            dtpkTuNgay.Value = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);
            dtpkDenNgay.Value = DateTime.Now;
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
            btnExportExcel.Enabled = AllowExport;
        }

        public void ClearData()
        {
            DataTable dt = dgGiaVonDichVu.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgGiaVonDichVu.DataSource = null;
            }
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                _isFromDateToDate = raTuNgayToiNgay.Checked;
                _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                _tenDichVu = txtDichVu.Text;

                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayGiaVonDichVuListProc));
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

        private void OnDisplayGiaVonDichVuList()
        {
            Result result = GiaVonDichVuBus.GetGiaVonDichVuList(_isFromDateToDate, _fromDate, _toDate, _tenDichVu);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    dgGiaVonDichVu.DataSource = result.QueryResult as DataTable;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("GiaVonDichVuBus.GetGiaThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("GiaVonDichVuBus.GetGiaThuocList"));
            }
        }

        private void OnAddGiaVonDichVu()
        {
            dlgAddGiaVonDichVu dlg = new dlgAddGiaVonDichVu();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnEditGiaVonDichVu()
        {
            if (dgGiaVonDichVu.SelectedRows == null || dgGiaVonDichVu.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 giá vốn dịch vụ.", IconType.Information);
                return;
            }

            DataRow drGiaVonDichVu = (dgGiaVonDichVu.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (drGiaVonDichVu == null) return;
            dlgAddGiaVonDichVu dlg = new dlgAddGiaVonDichVu(drGiaVonDichVu);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnDeleteGiaVonDichVu()
        {
            List<string> deletedGiaVonList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgGiaVonDichVu.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    string giaThuocGUID = row["GiaVonDichVuGUID"].ToString();
                    deletedGiaVonList.Add(giaThuocGUID);
                    deletedRows.Add(row);
                }
            }

            if (deletedGiaVonList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những giá vốn dịch vụ mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = GiaVonDichVuBus.DeleteGiaVonDichVu(deletedGiaVonList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("GiaVonDichVuBus.DeleteGiaVonDichVu"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("GiaVonDichVuBus.DeleteGiaVonDichVu"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những giá vốn dịch vụ cần xóa.", IconType.Information);
        }

        private void OnExportExcel()
        {
            List<DataRow> checkedRows = new List<DataRow>();
            DataTable dt = dgGiaVonDichVu.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    checkedRows.Add(row);
                }
            }

            if (checkedRows.Count > 0)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (!ExportExcel.ExportGiaVonDichVuToExcel(dlg.FileName, checkedRows))
                        return;
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những giá vốn dịch vụ cần xuất Excel.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddGiaVonDichVu();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditGiaVonDichVu();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteGiaVonDichVu();
        }

        private void dgGiaThuoc_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEditGiaVonDichVu();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgGiaVonDichVu.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }

        private void raTuNgayToiNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpkTuNgay.Enabled = raTuNgayToiNgay.Checked;
            dtpkDenNgay.Enabled = raTuNgayToiNgay.Checked;
            txtDichVu.Enabled = !raTuNgayToiNgay.Checked;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }
        #endregion

        #region Working Thread
        private void OnDisplayGiaVonDichVuListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayGiaVonDichVuList();
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
