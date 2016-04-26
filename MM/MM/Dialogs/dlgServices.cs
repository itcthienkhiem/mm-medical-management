/* Copyright (c) 2016, Cocosoft Inc.
 All rights reserved.
 http://www.Cocosofttech.com

 This file is part of the LIS open source project.

 The LIS  open source project is free software: you can
 redistribute it and/or modify it under the terms of the GNU General Public
 License as published by the Free Software Foundation, either version 3 of the
 License, or (at your option) any later version.

 The ClearCanvas LIS open source project is distributed in the hope that it
 will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
 Public License for more details.

 You should have received a copy of the GNU General Public License along with
 the LIS open source project.  If not, see
 <http://www.gnu.org/licenses/>.
*/
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
        private bool _isDichVuCon = false;
        private string _serviceGUID = string.Empty;
        private DataTable _dtDichVuCon = null;
        private bool _isDichVuHopDong = false;
        private bool _isViewAllDichVuHopDong = false;
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
            _isDichVuCon = false;
        }

        public dlgServices(List<string> addedServices, List<DataRow> deletedServiceRows)
        {
            InitializeComponent();
            _addedServices = addedServices;
            _deletedServiceRows = deletedServiceRows;
            _isServiceGroup = true;
            _isDichVuCon = false;
        }

        public dlgServices(string serviceGUID, DataTable dtDichVuCon)
        {
            InitializeComponent();
            _serviceGUID = serviceGUID;
            _dtDichVuCon = dtDichVuCon;
            _isDichVuCon = true;
        }

        public dlgServices(List<string> addedServices, string hopDongGUID)
        {
            InitializeComponent();
            _addedServices = addedServices;
            _contractGUID = hopDongGUID;
            _isDichVuCon = true;
            _isDichVuHopDong = true;
        }

        public dlgServices(DataTable giaDichVuDataSource)
        {
            InitializeComponent();
            _giaDichVuDataSource = giaDichVuDataSource;
            _isViewAllDichVuHopDong = true;
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
            if (_addedServices != null)
            {
                foreach (string key in _addedServices)
                {
                    DataRow[] rows = dt.Select(string.Format("{0}='{1}'", fieldName, key));
                    if (rows == null || rows.Length <= 0) continue;

                    deletedRows.AddRange(rows);
                }
            }

            if (_serviceGUID != string.Empty)
            {
                DataRow[] rows = dt.Select(string.Format("{0}='{1}'", fieldName, _serviceGUID));
                if (rows != null || rows.Length > 0)
                    deletedRows.AddRange(rows);
            }

            foreach (DataRow row in deletedRows)
            {
                dt.Rows.Remove(row);
            }

            //Add
            if (_deletedServiceRows != null)
            {
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

            if (_dtDichVuCon != null)
            {
                foreach (DataRow row in _dtDichVuCon.Rows)
                {
                    DataRow[] rows = dt.Select(string.Format("ServiceGUID='{0}'", row["ServiceGUID"].ToString()));
                    if (rows != null && rows.Length > 0)
                        dt.Rows.Remove(rows[0]);
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
                if (!_isViewAllDichVuHopDong)
                {
                    Result result = null;

                    if (!_isDichVuCon)
                    {
                        if (!_isServiceGroup)
                            result = ServicesBus.GetServicesListNotInCheckList(_contractGUID, _companyMemberGUID, _name);
                        else
                            result = ServiceGroupBus.GetServiceListNotInGroup(_name);
                    }
                    else if (_isDichVuHopDong)
                    {
                        result = ServicesBus.GetDichVuHopDongList(_name, _contractGUID);
                    }
                    else
                        result = ServicesBus.GetServicesList(_name);

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
                else
                {
                    dgService.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();

                        DataTable dt = _giaDichVuDataSource.Copy();
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        UpdateChecked(dt);
                        dgService.DataSource = dt;
                    }));
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
