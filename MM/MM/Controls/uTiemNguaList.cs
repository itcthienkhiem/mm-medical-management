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
    public partial class uTiemNguaList : uBase
    {
        #region Members
        private bool _isFromDateToDate = true;
        private string _tenBenhNhan = string.Empty;
        private bool _isMaBenhNhan = true;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        #endregion

        #region Constructor
        public uTiemNguaList()
        {
            InitializeComponent();
            dtpkTuNgay.Value = DateTime.Now;
            dtpkDenNgay.Value = DateTime.Now;
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnDelete.Enabled = AllowDelete;
            btnEdit.Enabled = AllowEdit;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();

                lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
                _isFromDateToDate = raTuNgayToiNgay.Checked;
                _fromDate = dtpkTuNgay.Value;
                _toDate = dtpkDenNgay.Value;
                _tenBenhNhan = txtTenBenhNhan.Text;
                _isMaBenhNhan = chkMaBenhNhan.Checked;

                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayTiemNguaListProc));
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

        private void OnDisplayTiemNguaList()
        {
            Result result = TiemNguaBus.GetDanhSachTiemNguaList(_isFromDateToDate, _fromDate, _toDate, _tenBenhNhan, _isMaBenhNhan);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgTiemNgua.DataSource = result.QueryResult;
                    HighLightAlert();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("TiemNguaBus.GetDanhSachTiemNguaList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("TiemNguaBus.GetDanhSachTiemNguaList"));
            }
        }

        private void HighLightAlert()
        {
            if (dgTiemNgua.RowCount <= 0) return;
            DateTime currentDate = DateTime.Now;

            foreach (DataGridViewRow row in dgTiemNgua.Rows)
            {
                DataRow dr = (row.DataBoundItem as DataRowView).Row;
                if (dr["Lan1"] != null && dr["Lan1"] != DBNull.Value)
                {
                    DateTime dt = Convert.ToDateTime(dr["Lan1"]);
                    int days = dt.Subtract(currentDate).Days;

                    if (days >= 0 && days <= Global.AlertDays)
                    {
                        row.DefaultCellStyle.BackColor = Color.DodgerBlue;
                        continue;
                    }
                }

                if (dr["Lan2"] != null && dr["Lan2"] != DBNull.Value)
                {
                    DateTime dt = Convert.ToDateTime(dr["Lan2"]);
                    int days = dt.Subtract(currentDate).Days;

                    if (days >= 0 && days <= Global.AlertDays)
                    {
                        row.DefaultCellStyle.BackColor = Color.DodgerBlue;
                        continue;
                    }
                }

                if (dr["Lan3"] != null && dr["Lan3"] != DBNull.Value)
                {
                    DateTime dt = Convert.ToDateTime(dr["Lan3"]);
                    int days = dt.Subtract(currentDate).Days;

                    if (days >= 0 && days <= Global.AlertDays)
                    {
                        row.DefaultCellStyle.BackColor = Color.DodgerBlue;
                        continue;
                    }
                }
            }
        }

        private void OnAdd()
        {
            dlgAddTiemNgua dlg = new dlgAddTiemNgua();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnEdit()
        {
            if (dgTiemNgua.SelectedRows == null || dgTiemNgua.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 tiêm ngừa.", IconType.Information);
                return;
            }

            DataRow drTiemNgua = (dgTiemNgua.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddTiemNgua dlg = new dlgAddTiemNgua(drTiemNgua);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnDelete()
        {
            List<string> deletedKeyList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgTiemNgua.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedKeyList.Add(row["TiemNguaGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedKeyList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những tiêm ngừa bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = TiemNguaBus.DeleteTiemNgua(deletedKeyList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("TiemNguaBus.DeleteTiemNgua"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("TiemNguaBus.DeleteTiemNgua"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những tiêm ngừa cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void raTuNgayToiNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpkTuNgay.Enabled = raTuNgayToiNgay.Checked;
            dtpkDenNgay.Enabled = raTuNgayToiNgay.Checked;
            txtTenBenhNhan.ReadOnly = raTuNgayToiNgay.Checked;
            chkMaBenhNhan.Enabled = !raTuNgayToiNgay.Checked;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (raTuNgayToiNgay.Checked && dtpkTuNgay.Value > dtpkDenNgay.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkTuNgay.Focus();
                return;
            }

            DisplayAsThread();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgTiemNgua.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void dgTiemNgua_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEdit();
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
        #endregion

        #region Working Thread
        private void OnDisplayTiemNguaListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayTiemNguaList();
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
