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
    public partial class uKetQuaXetNghiem_CellDyn3200 : uBase
    {
        #region Members
        private bool _isFromDateToDate = true;
        private string _tenBenhNhan = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        #endregion

        #region Constructor
        public uKetQuaXetNghiem_CellDyn3200()
        {
            InitializeComponent();
            dtpkDenNgay.Value = DateTime.Now;
            dtpkTuNgay.Value = DateTime.Now;
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnDelete.Enabled = AllowDelete;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();

                _isFromDateToDate = raTuNgayToiNgay.Checked;
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
            Result result = XetNghiem_Hitachi917Bus.GetKetQuaXetNghiemList(_isFromDateToDate, _fromDate, _toDate, _tenBenhNhan);
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
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_CellDyn3200Bus.GetKetQuaXetNghiemList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_CellDyn3200Bus.GetKetQuaXetNghiemList"));
            }
        }

        private void RefreshHighlight()
        {
            //foreach (DataGridViewRow row in dgChiTietKQXN.Rows)
            //{
            //    DataRow dr = (row.DataBoundItem as DataRowView).Row;
            //    TinhTrang tinhTrang = (TinhTrang)Convert.ToByte(dr["TinhTrang"]);
            //    if (tinhTrang == TinhTrang.BatThuong)
            //        row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
            //    else
            //        row.DefaultCellStyle.BackColor = SystemColors.Window;
            //}
        }
        #endregion

        #region Window Event Handlers
        private void raTuNgayToiNgay_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {

        }

        private void dgXetNghiem_DoubleClick(object sender, EventArgs e)
        {

        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void chkCTKQXNChecked_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dgChiTietKQXN_DoubleClick(object sender, EventArgs e)
        {

        }

        private void btnCapNhatCTKQXN_Click(object sender, EventArgs e)
        {

        }

        private void btnXoaCTKQXN_Click(object sender, EventArgs e)
        {

        }

        private void raTuNgayToiNgay_KeyDown(object sender, KeyEventArgs e)
        {

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
