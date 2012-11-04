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
    public partial class uTrackingList : uBase
    {
        #region Members
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        private string _docStaffGUID = string.Empty;
        private bool _isAdd = false;
        private bool _isEdit = false;
        private bool _isDelete = false;
        #endregion

        #region Constructor
        public uTrackingList()
        {
            InitializeComponent();

            dtpkTuNgay.Value = DateTime.Now.AddDays(-1);
            dtpkDenNgay.Value = DateTime.Now;
        }
        #endregion

        #region Properties
        
        #endregion

        #region UI Command
        public void InitData()
        {
            //DocStaff
            List<byte> staffTypes = new List<byte>();
            staffTypes.Add((byte)StaffType.BacSi);
            staffTypes.Add((byte)StaffType.DieuDuong);
            staffTypes.Add((byte)StaffType.KeToan);
            staffTypes.Add((byte)StaffType.LeTan);
            staffTypes.Add((byte)StaffType.Sale);
            staffTypes.Add((byte)StaffType.ThuKyYKhoa);
            staffTypes.Add((byte)StaffType.XetNghiem);

            Result result = DocStaffBus.GetDocStaffList(staffTypes);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
                return;
            }
            else
            {
                DataTable dt = result.QueryResult as DataTable;
                DataRow newRow = dt.NewRow();
                newRow["DocStaffGUID"] = Guid.Empty.ToString();
                newRow["FullName"] = "--------Tất cả-------";
                dt.Rows.InsertAt(newRow, 0);

                cboNhanVien.DataSource = dt;
            }
        }

        private bool CheckInfo()
        {
            if (!chkThem.Checked && !chkSua.Checked && !chkXoa.Checked)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu ít nhất 1 trong 3 loại (Thêm, Sửa, Xóa).", IconType.Information);
                chkThem.Focus();
                return false;
            }

            return true;
        }

        private void ViewAsThread()
        {
            try
            {
                if (!CheckInfo()) return;
                _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
                _docStaffGUID = cboNhanVien.SelectedValue.ToString();
                _isAdd = chkThem.Checked;
                _isEdit = chkSua.Checked;
                _isDelete = chkXoa.Checked;

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayTrackingListProc));
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

        public void ClearData()
        {
            DataTable dt = dgTracking.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgTracking.DataSource = null;
            }
        }

        private void OnDisplayTrackingList()
        {
            Result result = TrackingBus.GetTrackingList(_fromDate, _toDate, _docStaffGUID, _isAdd, _isEdit, _isDelete);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    dgTracking.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("TrackingBus.GetTrackingList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("TrackingBus.GetTrackingList"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            ViewAsThread();
        }

        private void dgTracking_DoubleClick(object sender, EventArgs e)
        {
            if (dgTracking.SelectedRows == null || dgTracking.SelectedRows.Count <= 0) return;
            DataRow row = (dgTracking.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string desc = row["Description"].ToString();
            dlgActionDescription dlg = new dlgActionDescription(desc);
            dlg.ShowDialog(this);
        }
        #endregion

        #region Working Thread
        private void OnDisplayTrackingListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayTrackingList();
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
