using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgAddServiceGroup : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private ServiceGroup _serviceGroup = new ServiceGroup();
        private List<string> _addedServices = new List<string>();
        private List<string> _deletedServices = new List<string>();
        private List<DataRow> _deletedServiceRows = new List<DataRow>();
        private DataRow _drServiceGroup = null;
        private bool _flag = true;
        #endregion

        #region Constructor
        public dlgAddServiceGroup()
        {
            InitializeComponent();
            GenerateCode();
        }

        public dlgAddServiceGroup(DataRow drServiceGroup)
        {
            InitializeComponent();
            _isNew = false;
            this.Text = "Sua nhom dich vu";
            _drServiceGroup = drServiceGroup;
        }
        #endregion

        #region Properties
        public ServiceGroup ServiceGroup
        {
            get { return _serviceGroup; }
            set { _serviceGroup = value; }
        }
        #endregion

        #region UI Command
        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = ServiceGroupBus.GetServiceGroupCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                txtCode.Text = Utility.GetCode("NDV", count + 1, 4);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ServiceGroupBus.GetServiceGroupCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ServiceGroupBus.GetServiceGroupCount"));
            }
        }

        private void DisplayServiceListAsThread(string serviceGroupGUID)
        {
            try
            {
                chkCheckedService.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayServiceListProc), serviceGroupGUID);
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplayServiceList(string serviceGroupGUID)
        {
            Result result = ServiceGroupBus.GetServiceListByGroup(serviceGroupGUID);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgService.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ServiceGroupBus.GetServiceListByGroup"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ServiceGroupBus.GetServiceListByGroup"));
            }
        }

        private void DisplayInfo(DataRow drServiceGroup)
        {
            try
            {
                txtCode.Text = drServiceGroup["Code"] as string;
                txtName.Text = drServiceGroup["Name"] as string;
                txtNote.Text = drServiceGroup["Note"] as string;

                _serviceGroup.ServiceGroupGUID = Guid.Parse(drServiceGroup["ServiceGroupGUID"].ToString());

                if (drServiceGroup["CreatedDate"] != null && drServiceGroup["CreatedDate"] != DBNull.Value)
                    _serviceGroup.CreatedDate = Convert.ToDateTime(drServiceGroup["CreatedDate"]);

                if (drServiceGroup["CreatedBy"] != null && drServiceGroup["CreatedBy"] != DBNull.Value)
                    _serviceGroup.CreatedBy = Guid.Parse(drServiceGroup["CreatedBy"].ToString());

                if (drServiceGroup["UpdatedDate"] != null && drServiceGroup["UpdatedDate"] != DBNull.Value)
                    _serviceGroup.UpdatedDate = Convert.ToDateTime(drServiceGroup["UpdatedDate"]);

                if (drServiceGroup["UpdatedBy"] != null && drServiceGroup["UpdatedBy"] != DBNull.Value)
                    _serviceGroup.UpdatedBy = Guid.Parse(drServiceGroup["UpdatedBy"].ToString());

                if (drServiceGroup["DeletedDate"] != null && drServiceGroup["DeletedDate"] != DBNull.Value)
                    _serviceGroup.DeletedDate = Convert.ToDateTime(drServiceGroup["DeletedDate"]);

                if (drServiceGroup["DeletedBy"] != null && drServiceGroup["DeletedBy"] != DBNull.Value)
                    _serviceGroup.DeletedBy = Guid.Parse(drServiceGroup["DeletedBy"].ToString());

                _serviceGroup.Status = Convert.ToByte(drServiceGroup["Status"]);

                DisplayServiceListAsThread(_serviceGroup.ServiceGroupGUID.ToString());
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (txtCode.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã nhóm dịch vụ.", IconType.Information);
                txtCode.Focus();
                return false;
            }

            if (txtName.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên nhóm dịch vụ.", IconType.Information);
                txtName.Focus();
                return false;
            }

            string serviceGroupGUID = _isNew ? string.Empty : _serviceGroup.ServiceGroupGUID.ToString();
            Result result = ServiceGroupBus.CheckServiceGroupExistCode(serviceGroupGUID, txtCode.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Mã nhóm dịch vụ này đã tồn tại rồi. Vui lòng nhập mã khác.", IconType.Information);
                    txtCode.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ServiceGroupBus.CheckServiceGroupExistCode"), IconType.Error);
                return false;
            }

            if (dgService.RowCount <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn ít nhất 1 dịch vụ.", IconType.Information);
                return false;
            }

            foreach (string serviceGUID in _addedServices)
            {
                result = ServiceGroupBus.CheckServiceExist(serviceGUID);

                if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
                {
                    if (result.Error.Code == ErrorCode.EXIST)
                    {
                        string serviceName = GetServiceName(serviceGUID);
                        MsgBox.Show(this.Text, string.Format("Dịch vụ: '{0}' đã thuộc 1 nhóm khác.", serviceName), IconType.Information);

                        DataTable dt = dgService.DataSource as DataTable;
                        if (dt != null)
                        {
                            DataRow[] rows = dt.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
                            if (rows != null && rows.Length > 0)
                            {
                                _addedServices.Remove(rows[0]["ServiceGUID"].ToString());
                                dt.Rows.Remove(rows[0]);
                            }
                        }

                        return false;
                    }
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("ServiceGroupBus.CheckServiceExist"), IconType.Error);
                    return false;
                }
            }

            return true;
        }

        private string GetServiceName(string serviceGUID)
        {
            string serviceName = string.Empty;
            DataTable dt = dgService.DataSource as DataTable;
            if (dt != null)
            {
                DataRow[] rows = dt.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
                if (rows != null && rows.Length > 0)
                    serviceName = rows[0]["Name"].ToString();
            }

            return serviceName;
        }

        private void OnAddService()
        {
            dlgServices dlg = new dlgServices(_addedServices, _deletedServiceRows);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                List<DataRow> checkedRows = dlg.Services;
                DataTable dataSource = dgService.DataSource as DataTable;
                foreach (DataRow row in checkedRows)
                {
                    string serviceGUID = row["ServiceGUID"].ToString();
                    DataRow[] rows = dataSource.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
                    if (rows == null || rows.Length <= 0)
                    {
                        DataRow newRow = dataSource.NewRow();
                        newRow["Checked"] = false;
                        newRow["ServiceGUID"] = serviceGUID;
                        newRow["Code"] = row["Code"];
                        newRow["Name"] = row["Name"];
                        dataSource.Rows.Add(newRow);

                        if (!_addedServices.Contains(serviceGUID))
                            _addedServices.Add(serviceGUID);

                        _deletedServices.Remove(serviceGUID);
                        foreach (DataRow r in _deletedServiceRows)
                        {
                            if (r["ServiceGUID"].ToString() == serviceGUID)
                            {
                                _deletedServiceRows.Remove(r);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void OnDeleteService()
        {
            List<string> deletedServiceList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgService.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedServiceList.Add(row["ServiceGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedServiceList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những dịch vụ mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    foreach (DataRow row in deletedRows)
                    {
                        string serviceGUID = row["ServiceGUID"].ToString();
                        if (!_deletedServices.Contains(serviceGUID))
                        {
                            _deletedServices.Add(serviceGUID);

                            DataRow r = dt.NewRow();
                            r.ItemArray = row.ItemArray;
                            _deletedServiceRows.Add(r);
                        }

                        _addedServices.Remove(serviceGUID);

                        dt.Rows.Remove(row);
                    }
                }
            }
            else
                MsgBox.Show(this.Text, "Vui lòng đánh dấu những dịch vụ cần xóa.", IconType.Information);
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
                _serviceGroup.Code = txtCode.Text;
                _serviceGroup.Name = txtName.Text;
                _serviceGroup.EnglishName = string.Empty;
                _serviceGroup.Note = txtNote.Text;
                _serviceGroup.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _serviceGroup.CreatedDate = DateTime.Now;
                    _serviceGroup.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _serviceGroup.UpdatedDate = DateTime.Now;
                    _serviceGroup.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                List<Service_ServiceGroup> addedList = new List<Service_ServiceGroup>();
                foreach (string serviceGUID in _addedServices)
                {
                    Service_ServiceGroup ssg = new Service_ServiceGroup();
                    ssg.ServiceGUID = Guid.Parse(serviceGUID);
                    ssg.CreatedDate = DateTime.Now;
                    ssg.DeletedBy = Guid.Parse(Global.UserGUID);
                    ssg.Status = (byte)Status.Actived;
                    addedList.Add(ssg);
                }

                Result result = ServiceGroupBus.InsertServiceGroup(_serviceGroup, addedList, _deletedServices);
                if (!result.IsOK)
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("ServiceGroupBus.InsertServiceGroup"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ServiceGroupBus.InsertServiceGroup"));
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }
        #endregion

        #region Window Event Handlers
        private void chkCheckedService_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgService.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkCheckedService.Checked;
            }
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            OnAddService();
        }

        private void btnDeleteService_Click(object sender, EventArgs e)
        {
            OnDeleteService();
        }

        private void dlgAddServiceGroup_Load(object sender, EventArgs e)
        {
            if (_isNew)
                DisplayServiceListAsThread(Guid.Empty.ToString());
            else
                DisplayInfo(_drServiceGroup);
        }

        private void dlgAddServiceGroup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
            else
            {
                if (!_flag)
                {
                    _flag = true;
                    return;
                }

                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin nhóm dịch vụ ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (CheckInfo())
                        SaveInfoAsThread();
                    else
                        e.Cancel = true;
                }
            }
        }
        #endregion

        #region Working Thread
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

        private void OnDisplayServiceListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayServiceList(state.ToString());
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(this.Text, e.Message, IconType.Error);
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
