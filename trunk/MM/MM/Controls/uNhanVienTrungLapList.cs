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
using MM.Exports;

namespace MM.Controls
{
    public partial class uNhanVienTrungLapList : uBase
    {
        #region Members
        private bool _isAscending = true;
        #endregion

        #region Constructor
        public uNhanVienTrungLapList()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnMerge.Enabled = AllowEdit;
            mergeToolStripMenuItem.Enabled = AllowEdit;
        }

        public void ClearData()
        {
            DataTable dt = dgDocStaff.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgDocStaff.DataSource = null;
            }
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayDocStaffListProc));
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

        private void OnDisplayDocStaffList()
        {
            Result result = DocStaffBus.GetNhanVienTrungLapList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    dgDocStaff.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("DocStaffBus.GetNhanVienTrungLapList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetNhanVienTrungLapList"));
            }
        }

        private void OnMerge()
        {
            if (dgDocStaff.SelectedRows == null || dgDocStaff.SelectedRows.Count <= 1)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn ít nhất 2 nhân viên để merge.", IconType.Information);
                return;
            }
            else
            {
                DataTable dt = (dgDocStaff.DataSource as DataTable).Clone();
                for (int i = 0; i < dgDocStaff.SelectedRows.Count; i++)
                {
                    DataRow dr = (dgDocStaff.SelectedRows[i].DataBoundItem as DataRowView).Row;
                    dt.ImportRow(dr);
                }

                dlgMergeNhanVien dlg = new dlgMergeNhanVien();
                dlg.SetDataSource(dt);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //re-bind data soource
                    OnDisplayDocStaffList();
                }
            }
        }
        #endregion

        #region Window Event Handlers
        private void dgDocStaff_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                _isAscending = !_isAscending;
                DataTable dt = dgDocStaff.DataSource as DataTable;

                List<DataRow> results = null;

                if (_isAscending)
                {
                    results = (from p in dt.AsEnumerable()
                               orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                               select p).ToList<DataRow>();
                }
                else
                {
                    results = (from p in dt.AsEnumerable()
                               orderby p.Field<string>("FirstName") descending, p.Field<string>("FullName") descending
                               select p).ToList<DataRow>();
                }


                DataTable newDataSource = dt.Clone();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgDocStaff.DataSource = newDataSource;
            }
            else
                _isAscending = false;
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            OnMerge();
        }

        private void mergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnMerge();
        }
        #endregion

        #region Working Thread
        private void OnDisplayDocStaffListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayDocStaffList();
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
