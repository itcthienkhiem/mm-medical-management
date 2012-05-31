using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uTraCuuThongTinKhachHang : uBase
    {
        #region Constructor
        public uTraCuuThongTinKhachHang()
        {
            InitializeComponent();
            dtpkTuNgay.Value = DateTime.Now;
            dtpkDenNgay.Value = DateTime.Now;
        }
        #endregion

        #region UI Command
        public void DisplayAsThread()
        {
            Cursor.Current = Cursors.WaitCursor;
            lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
            if (dtpkTuNgay.Value > dtpkDenNgay.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkTuNgay.Focus();
                return;
            }

            string tenBenhNhan = txtTenBenhNhan.Text;
            DateTime tuNgay = dtpkTuNgay.Value;
            DateTime denNgay = dtpkDenNgay.Value;

            Result result = UserBus.GetAccountList(tuNgay, denNgay, tenBenhNhan, chkMaBenhNhan.Checked);
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                dgBenhNhan.DataSource = result.QueryResult as DataTable;
                lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dt.Rows.Count);
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("UserBus.GetAccountList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("UserBus.GetAccountList"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }
        #endregion
    }
}
