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

namespace MM.Controls
{
    public partial class uYKienKhachHangList : uBase
    {
        #region Members
        private bool _isFromDateToDate = true;
        private string _tenBenhNhan = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
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
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                _isFromDateToDate = raTuNgayToiNgay.Checked;
                _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                _tenBenhNhan = txtTenBenhNhan.Text;

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
            Result result = YKienKhachHangBus.GetYKienKhachHangList(_isFromDateToDate, _fromDate, _toDate, _tenBenhNhan);
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
            txtTenBenhNhan.ReadOnly = raTuNgayToiNgay.Checked;
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
