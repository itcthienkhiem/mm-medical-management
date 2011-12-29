﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;
using MM.Controls;

namespace MM.Dialogs
{
    public partial class dlgAddChiDinh : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private DataTable _dataSource = null;
        private List<DichVuChiDinhView> _dichVuChiDinhList = null;
        private DataRow _drChiDinh = null;
        private DataTable _dtChiTietChiDinh = null;
        private ChiDinh _chiDinh = new ChiDinh();
        private DataRow _patientRow = null;
        #endregion

        #region Constructor
        public dlgAddChiDinh(DataRow patientRow)
        {
            InitializeComponent();
            _patientRow = patientRow;
        }

        public dlgAddChiDinh(DataRow drChiDinh, DataTable dtChiTietChiDinh, List<DichVuChiDinhView> dichVuChiDinhList)
        {
            InitializeComponent();
            _drChiDinh = drChiDinh;
            _dichVuChiDinhList = dichVuChiDinhList;
            _dtChiTietChiDinh = dtChiTietChiDinh;
            _isNew = false;
            this.Text = "Sua chi dinh";
        }
        #endregion

        #region Properties
        public List<DataRow> CheckedRows
        {
            get
            {
                if (_dataSource == null || _dataSource.Rows.Count <= 0) return null;
                List<DataRow> checkedRows = new List<DataRow>();
                foreach (DataRow row in _dataSource.Rows)
                {
                    if (Convert.ToBoolean(row["Checked"]))
                        checkedRows.Add(row);
                }

                return checkedRows;
            }

        }
        #endregion

        #region UI Command
        private void InitData()
        {
            //DocStaff
            List<byte> staffTypes = new List<byte>();
            staffTypes.Add((byte)StaffType.Doctor);
            staffTypes.Add((byte)StaffType.Nurse);
            Result result = DocStaffBus.GetDocStaffList(staffTypes);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
                return;
            }
            else
            {
                cboDocStaff.DataSource = result.QueryResult;
            }

            if (Global.StaffType != StaffType.Admin && Global.StaffType != StaffType.Reception)
            {
                cboDocStaff.SelectedValue = Global.UserGUID;
                cboDocStaff.Enabled = false;
            }

            DisplayAsThread();
        }

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

        private void OnDisplayServicesList()
        {
            Result result = ServicesBus.GetServicesList();

            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    _dataSource = (DataTable)result.QueryResult;
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
                                     (p.Field<string>("Code").ToLower().IndexOf(str) >= 0 ||
                                     str.IndexOf(p.Field<string>("Code").ToLower()) >= 0)
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

            UpdateDichVuChiDinh();
        }

        private void UpdateDichVuChiDinh()
        {
            if (_dichVuChiDinhList == null || _dichVuChiDinhList.Count > 0) return;
            foreach (DataGridViewRow row in dgService.Rows)
            {
                DataRow r = (row.DataBoundItem as DataRowView).Row;
                string serviceGUID = r["ServiceGUID"].ToString();

                foreach (var dvcd in _dichVuChiDinhList)
                {
                    if (serviceGUID == dvcd.ServiceGUID.ToString())
                    {
                        (row.Cells["Checked"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                        row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                        break;
                    }
                }
            }
        }

        private void UpdateChecked()
        {
            DataTable dt = dgService.DataSource as DataTable;
            if (dt == null) return;

            foreach (DataRow row1 in dt.Rows)
            {
                string patientGUID1 = row1["ServiceGUID"].ToString();
                bool isChecked1 = Convert.ToBoolean(row1["Checked"]);
                foreach (DataRow row2 in _dataSource.Rows)
                {
                    string patientGUID2 = row2["ServiceGUID"].ToString();
                    bool isChecked2 = Convert.ToBoolean(row2["Checked"]);

                    if (patientGUID1 == patientGUID2)
                    {
                        row2["Checked"] = row1["Checked"];
                        break;
                    }
                }
            }
        }

        private void DisplayInfo()
        {
            try
            {
                _chiDinh.ChiDinhGUID = Guid.Parse(_drChiDinh["ChiDinhGUID"].ToString());
                cboDocStaff.SelectedValue = _drChiDinh["BacSiChiDinhGUID"].ToString();

                if (_dtChiTietChiDinh != null && _dtChiTietChiDinh.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgService.Rows)
                    {
                        DataRow r = (row.DataBoundItem as DataRowView).Row;
                        string serviceGUID = r["ServiceGUID"].ToString();

                        foreach (DataRow drChiTiet in _dtChiTietChiDinh.Rows)
                        {
                            if (serviceGUID == drChiTiet["ServiceGUID"].ToString())
                            {
                                r["Checked"] = true;
                                break;
                            }
                        }

                        foreach (var dvcd in _dichVuChiDinhList)
                        {
                            if (serviceGUID == dvcd.ServiceGUID.ToString())
                            {
                                (row.Cells["Checked"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                                row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (cboDocStaff.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ chỉ định", IconType.Information);
                cboDocStaff.Focus();
                return false;
            }

            if (this.CheckedRows == null || this.CheckedRows.Count <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn ít nhất 1 dịch vụ chỉ định.", IconType.Information);
                return false;
            }

            return true;
        }

        private void SaveInfoAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSaveInfoProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnSaveInfo()
        {
            try
            {
                if (_isNew)
                {
                    _chiDinh.CreatedDate = DateTime.Now;
                    _chiDinh.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _chiDinh.UpdatedDate = DateTime.Now;
                    _chiDinh.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                MethodInvoker method = delegate
                {
                    _chiDinh.BenhNhanGUID = Guid.Parse(_patientRow["PatientGUID"].ToString());
                    _chiDinh.BacSiChiDinhGUID = Guid.Parse(cboDocStaff.SelectedValue.ToString());
                    _chiDinh.NgayChiDinh = DateTime.Now;
                    _chiDinh.Status = (byte)Status.Actived;


                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }

        }
        #endregion

        #region Window Event Handlers
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

        private void dlgAddChiDinh_Load(object sender, EventArgs e)
        {
            InitData();
            if (!_isNew) DisplayInfo();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            if (dgService.RowCount <= 0) return;
            foreach (DataGridViewRow row in dgService.Rows)
            {
                DataGridViewDisableCheckBoxCell cell = row.Cells["Checked"] as DataGridViewDisableCheckBoxCell;
                if (cell.Enabled)
                {
                    DataRow r = (row.DataBoundItem as DataRowView).Row;
                    r["Checked"] = chkChecked.Checked;
                }
            }
        }

        private void dlgAddChiDinh_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void OnSaveInfoProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnSaveInfo();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion

        
    }
}