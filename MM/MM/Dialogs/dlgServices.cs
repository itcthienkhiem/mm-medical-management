using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Databasae;
using MM.Bussiness;
using MM.Common;

namespace MM.Dialogs
{
    public partial class dlgServices : dlgBase
    {
        #region Members
        private List<string> _addedServices = null;
        private List<DataRow> _deletedServiceRows = null;
        private string _contractGUID = string.Empty;
        #endregion

        #region Constructor
        public dlgServices(string contractGUID, List<string> addedServices, List<DataRow> deletedServiceRows)
        {
            InitializeComponent();
            _contractGUID = contractGUID;
            _addedServices = addedServices;
            _deletedServiceRows = deletedServiceRows;
        }
        #endregion

        #region Properties
        public List<DataRow> Services
        {
            get
            {
                if (dgService.RowCount <= 0) return null;
                DataTable dt = dgService.DataSource as DataTable;
                List<DataRow> checkedRows = new List<DataRow>();
                foreach (DataRow row in dt.Rows)
                {
                    if (Boolean.Parse(row["Checked"].ToString()))
                    {
                        checkedRows.Add(row);
                    }
                }

                return checkedRows;
            }

        }
        #endregion

        #region UI Command
        public void DisplayAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayServicesListProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private DataTable GetDataSource(DataTable dt)
        {
            string fieldName = "ServiceGUID";

            //Delete
            List<DataRow> deletedRows = new List<DataRow>();
            foreach (string key in _addedServices)
            {
                DataRow[] rows = dt.Select(string.Format("{0}='{1}'", fieldName, key));
                if (rows == null || rows.Length <= 0) continue;

                deletedRows.AddRange(rows);
            }

            foreach (DataRow row in deletedRows)
            {
                dt.Rows.Remove(row);
            }

            //Add
            foreach (DataRow row in _deletedServiceRows)
            {
                string key = row[fieldName].ToString();
                DataRow[] rows = dt.Select(string.Format("{0}='{1}'", fieldName, key));
                if (rows != null && rows.Length > 0) continue;

                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["ServiceGUID"] = key;
                newRow["Code"] = row["Code"];
                newRow["Name"] = row["Name"];
                newRow["Price"] = row["Price"];
                newRow["Description"] = row["Description"];
                dt.Rows.Add(newRow);
            }

            return dt;
        }

        private void OnDisplayServicesList()
        {
            Result result = ServicesBus.GetServicesListNotInCheckList(_contractGUID);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgService.DataSource = GetDataSource((DataTable)result.QueryResult);
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServicesBus.GetServicesListNotInCheckList"));
                Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.GetServicesListNotInCheckList"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgServices_Load(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void dlgServices_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                List<DataRow> checkedRows = this.Services;
                if (checkedRows == null || checkedRows.Count <= 0)
                {
                    MsgBox.Show(this.Text, "Vui lòng đánh dấu ít nhất 1 dịch vụ.");
                    e.Cancel = true;
                }
            }
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgService.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayServicesListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayServicesList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message);
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
