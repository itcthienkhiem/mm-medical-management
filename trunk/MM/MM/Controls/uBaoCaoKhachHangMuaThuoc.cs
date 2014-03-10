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
    public partial class uBaoCaoKhachHangMuaThuoc : uBase
    {
        #region Members
        private DataTable _dtThuoc = null;
        #endregion

        #region Constructor
        public uBaoCaoKhachHangMuaThuoc()
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
            btnPrintPreview.Enabled = AllowPrint;
            btnPrint.Enabled = AllowPrint;
            btnExportExcel.Enabled = AllowExport;
        }

        public void InitData()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = ThuocBus.GetThuocList();
            if (result.IsOK)
                _dtThuoc = result.QueryResult as DataTable;
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ThuocBus.GetThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThuocBus.GetThuocList"));
            }
        }

        private void ClearData()
        {
            DataTable dt = dgDSKhachHang.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                //dt = null;
                //dgDSKhachHang.DataSource = null;
            }
        }

        private void OnView()
        {
            DateTime tuNgay = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
            DateTime denNgay = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);

            string thuocGUID = string.Empty;
            if (txtThuoc.Text.Trim() != string.Empty)
                thuocGUID = txtThuoc.Tag.ToString();

            ClearData();

            Result result = ReportBus.GetDanhSachKhachHangMuaThuoc(tuNgay, denNgay, thuocGUID);
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                dgDSKhachHang.DataSource = dt;

                int count = 0;
                foreach (DataRow row in dt.Rows)
                {
                    count += Convert.ToInt32(row["SoLuong"]);
                }

                lbCount.Text = string.Format("Tổng số thuốc bán: {0}", count);
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReportBus.GetDanhSachKhachHangMuaThuoc"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ReportBus.GetDanhSachKhachHangMuaThuoc"));
            }
        }

        private void ViewPhieuThuThuoc(string phieuThuThuocGUID)
        {
            Result result = PhieuThuThuocBus.GetPhieuThuThuocAsDataTable(phieuThuThuocGUID);
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    dlgAddPhieuThuThuoc2 dlg = new dlgAddPhieuThuThuoc2(row);
                    dlg.ShowDialog(this);
                }
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhieuThuThuocBus.GetPhieuThuThuocAsDataTable"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuThuocBus.GetPhieuThuThuocAsDataTable"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void btnChonThuoc_Click(object sender, EventArgs e)
        {
            dlgSelectSingleThuoc dlg = new dlgSelectSingleThuoc(_dtThuoc);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                DataRow drThuoc = dlg.ThuocRow;
                txtThuoc.Tag = drThuoc["ThuocGUID"].ToString();
                txtThuoc.Text = drThuoc["TenThuoc"].ToString();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (dtpkTuNgay.Value > dtpkDenNgay.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkTuNgay.Focus();
                return;
            }

            //if (txtThuoc.Tag == null)
            //{
            //    MsgBox.Show(Application.ProductName, "Vui lòng chọn thuốc.", IconType.Information);
            //    btnChonThuoc.Focus();
            //    return;
            //}

            OnView();
        }

        private void dgDSKhachHang_DoubleClick(object sender, EventArgs e)
        {
            if (dgDSKhachHang.SelectedRows == null || dgDSKhachHang.SelectedRows.Count <= 0) return;

            DataRow row = (dgDSKhachHang.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string phieuThuThuocGUID = row["PhieuThuThuocGUID"].ToString();
            ViewPhieuThuThuoc(phieuThuThuocGUID);
        }
        #endregion

        
    }
}
