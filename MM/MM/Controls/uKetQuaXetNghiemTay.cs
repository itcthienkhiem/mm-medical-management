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
    public partial class uKetQuaXetNghiemTay : uBase
    {
        #region Members
        private string _tenBenhNhan = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private Font _normalFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        private Font _boldFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        #endregion

        #region Constructor
        public uKetQuaXetNghiemTay()
        {
            InitializeComponent();
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

                _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                _tenBenhNhan = txtTenBenhNhan.Text;

                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayKetQuaXetNghiemListProc));
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

        private void OnDisplayKetQuaXetNghiemList()
        {
            Result result = KetQuaXetNghiemTayBus.GetKetQuaXetNghiemList(_fromDate, _toDate, _tenBenhNhan);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgXetNghiem.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaXetNghiemTayBus.GetKetQuaXetNghiemList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTayBus.GetKetQuaXetNghiemList"));
            }
        }

        private void OnDisplayChiTietKetQuaXetNghiem(string ketQuaXetNghiemGUID)
        {
            Result result = KetQuaXetNghiemTayBus.GetChiTietKetQuaXetNghiem(ketQuaXetNghiemGUID);
            if (result.IsOK)
            {
                dgChiTietKQXN.DataSource = result.QueryResult;
                RefreshHighlight();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaXetNghiemTayBus.GetChiTietKetQuaXetNghiem"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTayBus.GetChiTietKetQuaXetNghiem"));
            }
        }

        private void RefreshHighlight()
        {
            foreach (DataGridViewRow row in dgChiTietKQXN.Rows)
            {
                DataRow dr = (row.DataBoundItem as DataRowView).Row;
                TinhTrang tinhTrang = (TinhTrang)Convert.ToByte(dr["TinhTrang"]);
                if (tinhTrang == TinhTrang.BatThuong)
                {
                    row.DefaultCellStyle.Font = _boldFont;
                    row.DefaultCellStyle.ForeColor = Color.Red;
                }
                else
                {
                    row.DefaultCellStyle.Font = _normalFont;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void OnAdd()
        {
            dlgAddKetQuaXetNghiemTay dlg = new dlgAddKetQuaXetNghiemTay();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnEdit()
        {
            if (dgXetNghiem.SelectedRows == null || dgXetNghiem.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 kết quả xét nghiệm.", IconType.Information);
                return;
            }

            DataRow drKetQuaXN = (dgXetNghiem.SelectedRows[0].DataBoundItem as DataRowView).Row;
            DataTable dtChiTietKQXN = dgChiTietKQXN.DataSource as DataTable;
            dlgAddKetQuaXetNghiemTay dlg = new dlgAddKetQuaXetNghiemTay(drKetQuaXN, dtChiTietKQXN);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnDelete()
        {
            List<string> deletedKQXNList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgXetNghiem.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedKQXNList.Add(row["KetQuaXetNghiemManualGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedKQXNList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những xét nghiệm bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = KetQuaXetNghiemTayBus.DeleteXetNghiem(deletedKQXNList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaXetNghiemTayBus.DeleteXetNghiem"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTayBus.DeleteXetNghiem"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những xét nghiệm cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgXetNghiem.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void dgXetNghiem_DoubleClick(object sender, EventArgs e)
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

        private void raTuNgayToiNgay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                DisplayAsThread();
        }

        private void dgXetNghiem_SelectionChanged(object sender, EventArgs e)
        {
            if (dgXetNghiem.SelectedRows == null || dgXetNghiem.SelectedRows.Count <= 0)
            {
                dgChiTietKQXN.DataSource = null;
                return;
            }

            DataRow row = (dgXetNghiem.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (row == null)
            {
                dgChiTietKQXN.DataSource = null;
                return;
            }

            OnDisplayChiTietKetQuaXetNghiem(row["KetQuaXetNghiemManualGUID"].ToString());
        }
        #endregion

        #region Working Thread
        private void OnDisplayKetQuaXetNghiemListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayKetQuaXetNghiemList();
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
