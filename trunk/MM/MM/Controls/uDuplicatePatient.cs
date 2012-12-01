using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;
using System.Threading;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uDuplicatePatient : uBase
    {
        #region Members
        private bool _isAscending = true;
        private string _name = string.Empty;
        private int _type = 0;
        #endregion

        #region Constructor
        public uDuplicatePatient()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnMerge.Enabled = AllowEdit;
        }

        public void ClearData()
        {
            DataTable dt = dgDuplicatePatient.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgDuplicatePatient.DataSource = null;
            }
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                _name = txtSearchPatient.Text;
                if (_name.Trim() == string.Empty) _name = "*";
                else if (_name.Trim() == "*") _name = string.Empty;

                if (chkMaBenhNhan.Checked) _type = 1;
                else if (chkTheoSoDienThoai.Checked) _type = 2;
                else _type = 0;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayDuplicatePatientListProc));
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

        public override void SearchAsThread()
        {
            try
            {
                _name = txtSearchPatient.Text;
                if (_name.Trim() == string.Empty) _name = "*";
                else if (_name.Trim() == "*") _name = string.Empty;

                if (chkMaBenhNhan.Checked) _type = 1;
                else if (chkTheoSoDienThoai.Checked) _type = 2;
                else _type = 0;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplayDuplicatePatientList()
        {
            lock (ThisLock)
            {
                Result result = PatientBus.GetDuplicatePatientList(_name, _type);
                if (result.IsOK)
                {
                    dgDuplicatePatient.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();

                        DataTable dt = result.QueryResult as DataTable;
                        dgDuplicatePatient.DataSource = dt;

                        lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dt.Rows.Count);
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.GetPatientList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientList"));
                }
            }
        }
        #endregion
        
        #region Working Thread
        private void OnDisplayDuplicatePatientListProc(object state)
        {
            try
            {
                OnDisplayDuplicatePatientList();
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

        private void OnSearchProc(object state)
        {
            try
            {
                OnDisplayDuplicatePatientList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }
        #endregion

        #region Window Event Handers
        private void btnMerge_Click(object sender, EventArgs e)
        {
            if (dgDuplicatePatient.SelectedRows == null || dgDuplicatePatient.SelectedRows.Count <= 1)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn ít nhất 2 bệnh nhân để merge.", IconType.Information);
                return;
            }
            else
            {
                DataTable dt = (dgDuplicatePatient.DataSource as DataTable).Clone();
                for (int i = 0; i < dgDuplicatePatient.SelectedRows.Count; i++)
                {
                    DataRow dr = (dgDuplicatePatient.SelectedRows[i].DataBoundItem as DataRowView).Row;
                    dt.ImportRow(dr);
                }
                dlgMergePatient dlg = new dlgMergePatient();
                dlg.SetDataSource(dt);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //re-bind data soource
                    OnDisplayDuplicatePatientList();
                }
            }
        }

        private void dgDuplicatePatient_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                _isAscending = !_isAscending;
                DataTable dt = dgDuplicatePatient.DataSource as DataTable;

                DataTable newDataSource = null;
                
                if (_isAscending)
                {
                    newDataSource = (from p in dt.AsEnumerable()
                                     orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                                     select p).CopyToDataTable();
                }
                else
                {
                    newDataSource = (from p in dt.AsEnumerable()
                                     orderby p.Field<string>("FirstName") descending, p.Field<string>("FullName") descending
                                     select p).CopyToDataTable();
                }

                dgDuplicatePatient.DataSource = newDataSource;

                if (dt != null)
                {
                    dt.Rows.Clear();
                    dt.Clear();
                    dt = null;
                }
            }
            else
                _isAscending = false;
        }

        private void chkMaBenhNhan_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void chkTheoSoDienThoai_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void txtSearchPatient_TextChanged(object sender, EventArgs e)
        {
            StartTimer();
        }
        #endregion
    }
}
