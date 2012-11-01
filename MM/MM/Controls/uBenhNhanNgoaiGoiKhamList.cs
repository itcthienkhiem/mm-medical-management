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

namespace MM.Controls
{
    public partial class uBenhNhanNgoaiGoiKhamList : uBase
    {
        #region Members
        private DateTime _tuNgay = DateTime.Now;
        private DateTime _denNgay = DateTime.Now;
        private string _tenBenhNhan = string.Empty;
        private int _type = 0; //0: Tên bệnh nhân; 1: Mã bệnh nhân
        #endregion

        #region Constructor
        public uBenhNhanNgoaiGoiKhamList()
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
            btnPrint.Enabled = AllowPrint;
            btnPrintPreview.Enabled = AllowPrint;
            btnExportExcel.Enabled = AllowExport;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
                chkChecked.Checked = false;
                _tuNgay = dtpkTuNgay.Value;
                _denNgay = dtpkDenNgay.Value;
                _tenBenhNhan = txtBenhNhan.Text.Trim();
                if (chkMaBenhNhan.Checked) _type = 1;
                else _type = 0;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayBenhNhanNgoaiGoiKhamListProc));
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

        private void OnDisplayBenhNhanNgoaiGoiKhamList()
        {
            Result result = BenhNhanNgoaiGoiKhamBus.GetBenhNhanNgoaiGoiKhamList(_tuNgay, _denNgay, _tenBenhNhan, _type);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    DataTable dt = result.QueryResult as DataTable;
                    dgBenhNhanNgoaiGoiKham.DataSource = result.QueryResult as DataTable;
                    lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dt.Rows.Count);
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("BenhNhanNgoaiGoiKhamBus.GetBenhNhanNgoaiGoiKhamList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("BenhNhanNgoaiGoiKhamBus.GetBenhNhanNgoaiGoiKhamList"));
            }
        }

        private void OnAdd()
        {

        }

        private void OnEdit()
        {

        }

        private void OnDelete()
        {

        }

        private void OnPrint(bool isPreview)
        {

        }

        private void OnExportExcell()
        {

        }
        #endregion

        #region Window Event Handlers
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
            OnExportExcell();
        }

        private void dgBenhNhanNgoaiGoiKham_DoubleClick(object sender, EventArgs e)
        {
            if (!AllowEdit) return;
            OnEdit();
        }
        #endregion

        #region Working Thread
        private void OnDisplayBenhNhanNgoaiGoiKhamListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayBenhNhanNgoaiGoiKhamList();
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
