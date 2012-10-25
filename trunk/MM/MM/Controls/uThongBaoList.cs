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
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uThongBaoList : uBase
    {
        #region Members
        private DateTime _tuNgay = DateTime.Now;
        private DateTime _denNgay = DateTime.Now;
        private string _tenNguoiTao = string.Empty;
        #endregion

        #region Constructor
        public uThongBaoList()
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
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                _tuNgay = dtpkTuNgay.Value;
                _denNgay = dtpkDenNgay.Value;
                _tenNguoiTao = txtTenNguoiTao.Text.Trim();
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayThongBaoListProc));
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

        private void OnDisplayThongBaoList()
        {
            bool isAll = (AllowAdd || AllowEdit || AllowDelete) ? true : false;

            Result result = ThongBaoBus.GetThongBaoList(_tuNgay, _denNgay, _tenNguoiTao, isAll);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgThongBao.DataSource = result.QueryResult as DataTable;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ThongBaoBus.GetThongBaoList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThongBaoBus.GetThongBaoList"));
            }
        }
        #endregion
        
        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            if (dtpkTuNgay.Value > dtpkDenNgay.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập từ ngày nhỏ hơn hoặc bằng đến ngày.", IconType.Information);
                dtpkTuNgay.Focus();
                return;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnXemThongBao_Click(object sender, EventArgs e)
        {

        }

        private void dgThongBao_DoubleClick(object sender, EventArgs e)
        {

        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgThongBao.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayThongBaoListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayThongBaoList();
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
