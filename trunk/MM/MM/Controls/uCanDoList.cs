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
using MM.Dialogs;
using MM.Bussiness;
using MM.Databasae;

namespace MM.Controls
{
    public partial class uCanDoList : uBase
    {
        #region Members
        private object _patientRow = null;
        private string _patientGUID = string.Empty;
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        #endregion

        #region Constructor
        public uCanDoList()
        {
            InitializeComponent();
            dtpkFromDate.Value = DateTime.Now.AddDays(-1);
            dtpkToDate.Value = DateTime.Now;
        }
        #endregion

        #region Properties
        public object PatientRow
        {
            get { return _patientRow; }
            set { _patientRow = value; }
        }
        #endregion

        #region UI Command
        public void DisplayAsThread()
        {
            if (_patientRow == null) return;

            try
            {
                DataRow row = _patientRow as DataRow;
                _patientGUID = row["PatientGUID"].ToString();
                _fromDate = new DateTime(dtpkFromDate.Value.Year, dtpkFromDate.Value.Month, dtpkFromDate.Value.Day, 0, 0, 0);
                _toDate = new DateTime(dtpkToDate.Value.Year, dtpkToDate.Value.Month, dtpkToDate.Value.Day, 23, 59, 59);

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayCanDoProc));
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

        private void OnDisplayCanDo()
        {
            Result result = CanDoBus.GetCanDo(_patientGUID, _fromDate, _toDate);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgCanDo.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("CanDoBus.GetCanDo"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CanDoBus.GetCanDo"));
            }
        }

        private void OnAdd()
        {
            dlgAddCanDo dlg = new dlgAddCanDo(_patientGUID);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }



        private void OnEdit()
        {
            if (dgCanDo.SelectedRows == null || dgCanDo.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 cân đo.", IconType.Information);
                return;
            }

            DataRow drCanDo = (dgCanDo.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddCanDo dlg = new dlgAddCanDo(_patientGUID, drCanDo);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnDelete()
        {
            List<string> deletedCanDoList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgCanDo.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedCanDoList.Add(row["CanDoGuid"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedCanDoList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những cân đo mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = CanDoBus.DeleteCanDo(deletedCanDoList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("CanDoBus.DeleteCanDo"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("CanDoBus.DeleteCanDo"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những cân đo cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
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

        private void dgCanDo_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
        }
        #endregion

        #region Working Thread
        private void OnDisplayCanDoProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayCanDo();
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
