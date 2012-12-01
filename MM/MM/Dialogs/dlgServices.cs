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
        private string _companyMemberGUID = string.Empty;
        private string _contractGUID = string.Empty;
        private bool _isServiceGroup = false;
        private DataTable _giaDichVuDataSource = null;
        private DataTable _dtTemp = null;
        private string _name = string.Empty;
        private Dictionary<string, DataRow> _dictServices = new Dictionary<string, DataRow>();
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
            get { return _dictServices.Values.ToList(); }
        }
        #endregion

        #region UI Command
        public void DisplayAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                _name = txtSearchService.Text;
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

        public override void SearchAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                _name = txtSearchService.Text;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
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

        public void ClearData()
        {
            DataTable dt = dgService.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
            }

            dgService.DataSource = null;
        }

        private void OnDisplayServicesList()
        {
            lock (ThisLock)
            {
                Result result = null;

                if (!_isServiceGroup)
                    result = ServicesBus.GetServicesListNotInCheckList(_contractGUID, _companyMemberGUID, _name);
                else
                    result = ServiceGroupBus.GetServiceListNotInGroup(_name);

                if (result.IsOK)
                {
                    dgService.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();

                        DataTable dt = result.QueryResult as DataTable;
                        dt = GetDataSource(dt);
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        UpdateChecked(dt);
                        dgService.DataSource = dt;
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServicesBus.GetServicesListNotInCheckList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.GetServicesListNotInCheckList"));
                }
            }
        }

        private void UpdateChecked(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string key = row["ServiceGUID"].ToString();
                if (_dictServices.ContainsKey(key))
                    row["Checked"] = true;
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
                    MsgBox.Show(this.Text, "Vui lòng đánh dấu ít nhất 1 dịch vụ.", IconType.Information);
                    e.Cancel = true;
                }
            }
        }

        private void dgService_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgService.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgService.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string serviceGUID = row["ServiceGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            if (isChecked)
            {
                if (!_dictServices.ContainsKey(serviceGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictServices.Add(serviceGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictServices.ContainsKey(serviceGUID))
                {
                    _dictServices.Remove(serviceGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
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
                string serviceGUID = row["ServiceGUID"].ToString();

                if (chkChecked.Checked)
                {
                    if (!_dictServices.ContainsKey(serviceGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictServices.Add(serviceGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
                else
                {
                    if (_dictServices.ContainsKey(serviceGUID))
                    {
                        _dictServices.Remove(serviceGUID);

                        DataRow[] rows = _dtTemp.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
                        if (rows != null && rows.Length > 0)
                            _dtTemp.Rows.Remove(rows[0]);
                    }
                }
            }
        }

        private void txtSearchService_TextChanged(object sender, EventArgs e)
        {
            StartTimer();
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

        private void OnSearchProc(object state)
        {
            try
            {
                OnDisplayServicesList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }
        #endregion

        
    }
}
