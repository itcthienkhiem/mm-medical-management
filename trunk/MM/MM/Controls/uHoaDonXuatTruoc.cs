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

namespace MM.Controls
{
    public partial class uHoaDonXuatTruoc : uBase
    {
        #region Members
        private bool _isFromDateToDate = true;
        private string _tenBenhNhan = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private int _type = 1; //0: TatCa; 1: ChuaXoa; 2: DaXoa
        #endregion

        #region Constructor
        public uHoaDonXuatTruoc()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnDeleteHoaDon.Enabled = AllowDelete;
            btnPrint.Enabled = AllowPrint;
            btnExportInvoice.Enabled = AllowExport;

            btnAdd.Enabled = AllowAddDangKy;
            btnEdit.Enabled = AllowEditDangKy;
            btnDelete.Enabled = AllowDeleteDangKy;
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

                if (raTatCa.Checked) _type = 0;
                else if (raChuaXoa.Checked) _type = 1;
                else _type = 2;

                chkChecked_HoaDon.Checked = false;
                chkChecked_SoHoaDon.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayInfoProc));
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

        private void DisplayDSHoaDonXuatTruoc()
        {
            Result result = HoaDonXuatTruocBus.GetHoaDonXuatTruocList(_isFromDateToDate, _fromDate, _toDate, _tenBenhNhan, _type);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgInvoice.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("HoaDonXuatTruocBus.GetHoaDonXuatTruocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("HoaDonXuatTruocBus.GetHoaDonXuatTruocList"));
            }
        }

        private void DisplayDSSoHoaDonXuatTruoc()
        {
            Result result = HoaDonXuatTruocBus.GetSoHoaDonXuatTruocList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgSoHoaDon.DataSource = result.QueryResult;
                    HighlightSoHoaDonDaXuat();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("HoaDonXuatTruocBus.GetSoHoaDonXuatTruocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("HoaDonXuatTruocBus.GetSoHoaDonXuatTruocList"));
            }
        }

        public void HighlightSoHoaDonDaXuat()
        {
            foreach (DataGridViewRow row in dgSoHoaDon.Rows)
            {
                DataRow dr = (row.DataBoundItem as DataRowView).Row;
                bool isExported = Convert.ToBoolean(dr["DaXuat"]);
                if (isExported)
                {
                    row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                    dr["Checked"] = false;
                    (row.Cells["colChecked"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
            }
        }

        private void OnAddDangKy()
        {
            dlgAddSoHoaDonXuatTruoc dlg = new dlgAddSoHoaDonXuatTruoc();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DisplayDSSoHoaDonXuatTruoc();
            }
        }

        private void OnEditDangKy()
        {

        }

        private void OnDeleteDangKy()
        {

        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_SoHoaDon_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgSoHoaDon.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked_SoHoaDon.Checked;
            }
        }

        private void chkChecked_HoaDon_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgInvoice.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked_HoaDon.Checked;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddDangKy();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditDangKy();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteDangKy();
        }

        private void dgSoHoaDon_DoubleClick(object sender, EventArgs e)
        {
            OnEditDangKy();
        }

        private void raTuNgayToiNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpkTuNgay.Enabled = raTuNgayToiNgay.Checked;
            dtpkDenNgay.Enabled = raTuNgayToiNgay.Checked;
            txtTenBenhNhan.ReadOnly = raTuNgayToiNgay.Checked;
        }

        private void btnView_Click(object sender, EventArgs e)
        {

        }

        private void btnExportInvoice_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteHoaDon_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void dgInvoice_DoubleClick(object sender, EventArgs e)
        {

        }
        #endregion

        #region Working Thread
        private void OnDisplayInfoProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                DisplayDSSoHoaDonXuatTruoc();
                DisplayDSHoaDonXuatTruoc();
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
