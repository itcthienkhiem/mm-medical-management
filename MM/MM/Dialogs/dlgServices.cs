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
        private DataTable _dataSource = null;
        private List<string> _addedServices = null;
        private List<DataRow> _deletedServiceRows = null;
        private string _companyMemberGUID = string.Empty;
        private string _contractGUID = string.Empty;
        private bool _isServiceGroup = false;
        private DataTable _giaDichVuDataSource = null;
        #endregion

        #region Constructor
        public dlgServices(string contractGUID, string companyMemberGUID, List<string> addedServices, 
            List<DataRow> deletedServiceRows, DataTable giaDichVuDataSource)
        {
            InitializeComponent();
            _contractGUID = contractGUID;
            _companyMemberGUID = companyMemberGUID;
            _addedServices = addedServices;
            _deletedServiceRows = deletedServiceRows;
            _giaDichVuDataSource = giaDichVuDataSource;
        }

        public dlgServices(List<string> addedServices, List<DataRow> deletedServiceRows)
        {
            InitializeComponent();
            _addedServices = addedServices;
            _deletedServiceRows = deletedServiceRows;
            _isServiceGroup = true;
        }
        #endregion

        #region Properties
        public List<DataRow> Services
        {
            get
            {
                if (dgService.RowCount <= 0) return null;
                UpdateChecked();
                List<DataRow> checkedRows = new List<DataRow>();
                foreach (DataRow row in _dataSource.Rows)
                {
                    if (Boolean.Parse(row["Checked"].ToString()))
                        checkedRows.Add(row);
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
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
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
                //newRow["Price"] = row["Price"];
                //newRow["Description"] = row["Description"];
                dt.Rows.Add(newRow);
            }

            if (_giaDichVuDataSource != null)
            {
                deletedRows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    DataRow[] rows = _giaDichVuDataSource.Select(string.Format("ServiceGUID='{0}'", row["ServiceGUID"].ToString()));
                    if (rows == null || rows.Length <= 0)
                        deletedRows.Add(row);
                }

                foreach (DataRow row in deletedRows)
                {
                    dt.Rows.Remove(row);
                }
            }

            return dt;
        }

        private void OnDisplayServicesList()
        {
            Result result = null;

            if (!_isServiceGroup)
                result = ServicesBus.GetServicesListNotInCheckList(_contractGUID, _companyMemberGUID);
            else
                result = ServiceGroupBus.GetServiceListNotInGroup();

            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    _dataSource = GetDataSource((DataTable)result.QueryResult);
                    dgService.DataSource = _dataSource;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServicesBus.GetServicesListNotInCheckList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.GetServicesListNotInCheckList"));
            }
        }

        private void OnSearchService()
        {
            UpdateChecked();

            chkChecked.Checked = false;
            if (txtSearchService.Text.Trim() == string.Empty)
            {
                dgService.DataSource = _dataSource;
                return;
            }

            string str = txtSearchService.Text.ToLower();

            //Code
            List<DataRow> results = (from p in _dataSource.AsEnumerable()
                                     where p.Field<string>("Code") != null &&
                                     p.Field<string>("Code").Trim() != string.Empty &&
                                     //(p.Field<string>("Code").ToLower().IndexOf(str) >= 0 ||
                                     //str.IndexOf(p.Field<string>("Code").ToLower()) >= 0)
                                     p.Field<string>("Code").ToLower().IndexOf(str) >= 0
                                     select p).ToList<DataRow>();

            DataTable newDataSource = _dataSource.Clone();
            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgService.DataSource = newDataSource;
                return;
            }


            //Name
            results = (from p in _dataSource.AsEnumerable()
                       where p.Field<string>("Name") != null &&
                           p.Field<string>("Name").Trim() != string.Empty &&
                           (p.Field<string>("Name").ToLower().IndexOf(str) >= 0 ||
                       str.IndexOf(p.Field<string>("Name").ToLower()) >= 0)
                       select p).ToList<DataRow>();

            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgService.DataSource = newDataSource;
                return;
            }

            dgService.DataSource = newDataSource;
        }

        private void UpdateChecked()
        {
            DataTable dt = dgService.DataSource as DataTable;
            if (dt == null) return;

            DataRow[] rows1 = dt.Select("Checked='True'");
            if (rows1 == null || rows1.Length <= 0) return;

            foreach (DataRow row1 in rows1)
            {
                string patientGUID1 = row1["ServiceGUID"].ToString();
                DataRow[] rows2 = _dataSource.Select(string.Format("ServiceGUID='{0}'", patientGUID1));
                if (rows2 == null || rows2.Length <= 0) continue;

                rows2[0]["Checked"] = row1["Checked"];
            }

            //DataTable dt = dgService.DataSource as DataTable;
            //if (dt == null) return;

            //foreach (DataRow row1 in dt.Rows)
            //{
            //    string patientGUID1 = row1["ServiceGUID"].ToString();
            //    bool isChecked1 = Convert.ToBoolean(row1["Checked"]);
            //    foreach (DataRow row2 in _dataSource.Rows)
            //    {
            //        string patientGUID2 = row2["ServiceGUID"].ToString();
            //        bool isChecked2 = Convert.ToBoolean(row2["Checked"]);

            //        if (patientGUID1 == patientGUID2)
            //        {
            //            row2["Checked"] = row1["Checked"];
            //            break;
            //        }
            //    }
            //}
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
                    MsgBox.Show(this.Text, "Vui lòng đánh dấu ít nhất 1 dịch vụ.", IconType.Information);
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

        private void txtSearchService_TextChanged(object sender, EventArgs e)
        {
            OnSearchService();
        }

        private void txtSearchService_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgService.Focus();

                if (dgService.SelectedRows != null && dgService.SelectedRows.Count > 0)
                {
                    int index = dgService.SelectedRows[0].Index;
                    if (index < dgService.RowCount - 1)
                    {
                        index++;
                        dgService.CurrentCell = dgService[1, index];
                        dgService.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgService.Focus();

                if (dgService.SelectedRows != null && dgService.SelectedRows.Count > 0)
                {
                    int index = dgService.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgService.CurrentCell = dgService[1, index];
                        dgService.Rows[index].Selected = true;
                    }
                }
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
