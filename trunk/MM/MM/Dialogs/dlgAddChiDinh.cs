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
using MM.Bussiness;
using MM.Databasae;
using MM.Controls;

namespace MM.Dialogs
{
    public partial class dlgAddChiDinh : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private DataTable _dtTemp = null;
        private List<DichVuChiDinhView> _dichVuChiDinhList = null;
        private DataRow _drChiDinh = null;
        private DataTable _dtChiTietChiDinh = null;
        private ChiDinh _chiDinh = new ChiDinh();
        private DataRow _patientRow = null;
        private string _name = string.Empty;
        private Dictionary<string, DataRow> _dictService = new Dictionary<string, DataRow>();
        #endregion

        #region Constructor
        public dlgAddChiDinh(DataRow patientRow)
        {
            InitializeComponent();
            _patientRow = patientRow;
            GenerateCode();
        }

        public dlgAddChiDinh(DataRow patientRow, DataRow drChiDinh, DataTable dtChiTietChiDinh, List<DichVuChiDinhView> dichVuChiDinhList)
        {
            InitializeComponent();
            _patientRow = patientRow;
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
            get { return _dictService.Values.ToList(); }
        }

        public ChiDinh ChiDinh
        {
            get { return _chiDinh; }
        }

        public string TenBacSiChiDinh
        {
            get { return cboDocStaff.Text; }
        }
        #endregion

        #region UI Command
        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = ChiDinhBus.GetChiDinhCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                txtMaChiDinh.Text = Utility.GetCode("CD", count + 1, 5);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ChiDinhBus.GetChiDinhCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.GetChiDinhCount"));
            }
        }

        private void InitData()
        {
            //DocStaff
            List<byte> staffTypes = new List<byte>();
            staffTypes.Add((byte)StaffType.BacSi);
            staffTypes.Add((byte)StaffType.BacSiSieuAm);
            staffTypes.Add((byte)StaffType.BacSiNgoaiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiNoiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiPhuKhoa);
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

            if (Global.StaffType == StaffType.BacSi || Global.StaffType == StaffType.BacSiSieuAm ||
                Global.StaffType == StaffType.BacSiNgoaiTongQuat || Global.StaffType == StaffType.BacSiNoiTongQuat ||
                Global.StaffType == StaffType.BacSiPhuKhoa)
            {
                cboDocStaff.SelectedValue = Global.UserGUID;
                cboDocStaff.Enabled = false;
            }

            DisplayAsThread();
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

        private void OnDisplayServicesList()
        {
            lock (ThisLock)
            {
                Result result = ServicesBus.GetServicesList(_name);

                if (result.IsOK)
                {
                    dgService.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();

                        DataTable dt = result.QueryResult as DataTable;
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        UpdateChecked(dt);
                        dgService.DataSource = dt;
                        UpdateDichVuChiDinh();
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServicesBus.GetServicesList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.GetServicesList"));
                }
            }
        }

        private void UpdateChecked(DataTable dt)
        {
            if (_dtChiTietChiDinh != null && _dtChiTietChiDinh.Rows.Count > 0)
            {
                foreach (DataRow row in _dtChiTietChiDinh.Rows)
                {
                    string serviceGUID = row["ServiceGUID"].ToString();
                    if (!_dictService.ContainsKey(serviceGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictService.Add(serviceGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
            }

            foreach (DataRow row in dt.Rows)
            {
                string key = row["ServiceGUID"].ToString();
                if (_dictService.ContainsKey(key))
                    row["Checked"] = true;
            }
        }

        private void UpdateDichVuChiDinh()
        {
            if (_dichVuChiDinhList == null || _dichVuChiDinhList.Count <= 0) return;
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

        private void DisplayInfo()
        {
            try
            {
                _chiDinh.ChiDinhGUID = Guid.Parse(_drChiDinh["ChiDinhGUID"].ToString());
                txtMaChiDinh.Text = _drChiDinh["MaChiDinh"].ToString();
                cboDocStaff.SelectedValue = _drChiDinh["BacSiChiDinhGUID"].ToString();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (cboDocStaff.SelectedValue == null || cboDocStaff.Text == string.Empty)
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

            string chiDinhGUID = _isNew ? string.Empty : _chiDinh.ChiDinhGUID.ToString();
            Result result = ChiDinhBus.CheckChiDinhExistCode(chiDinhGUID, txtMaChiDinh.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Mã chỉ định này đã tồn tại rồi. Vui lòng nhập mã khác.", IconType.Information);
                    txtMaChiDinh.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ChiDinhBus.CheckChiDinhExistCode"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.CheckChiDinhExistCode"));
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
                    _chiDinh = new Databasae.ChiDinh();
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
                    _chiDinh.MaChiDinh = txtMaChiDinh.Text;
                    _chiDinh.BenhNhanGUID = Guid.Parse(_patientRow["PatientGUID"].ToString());
                    _chiDinh.BacSiChiDinhGUID = Guid.Parse(cboDocStaff.SelectedValue.ToString());
                    _chiDinh.NgayChiDinh = DateTime.Now;
                    _chiDinh.Status = (byte)Status.Actived;

                    List<DataRow> checkedRows = this.CheckedRows;
                    List<ChiTietChiDinh> addedList = new List<ChiTietChiDinh>();
                    List<string> deletedKeys = new List<string>();

                    foreach (DataRow row in checkedRows)
                    {
                        ChiTietChiDinh ctcd = new ChiTietChiDinh();
                        ctcd.CreatedDate = DateTime.Now;
                        ctcd.CreatedBy = Guid.Parse(Global.UserGUID);
                        ctcd.ServiceGUID = Guid.Parse(row["ServiceGUID"].ToString());
                        addedList.Add(ctcd);
                    }

                    if (!_isNew)
                    {
                        foreach (DataRow row in _dtChiTietChiDinh.Rows)
                        {
                            bool isExist = false;
                            foreach (DataRow row2 in checkedRows)
                            {
                                if (row["ServiceGUID"].ToString() == row2["ServiceGUID"].ToString())
                                {
                                    isExist = true;
                                    break;
                                }
                            }

                            if (!isExist)
                                deletedKeys.Add(row["ChiTietChiDinhGUID"].ToString());
                        }
                    }

                    Result result = ChiDinhBus.InsertChiDinh(_chiDinh, addedList, deletedKeys);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ChiDinhBus.InsertChiDinh"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.InsertChiDinh"));
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    }
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

        private void ClearAllCheck()
        {
            if (dgService.RowCount <= 0) return;
            foreach (DataGridViewRow row in dgService.Rows)
            {
                DataRow r = (row.DataBoundItem as DataRowView).Row;
                r["Checked"] = chkChecked.Checked;
                string serviceGUID = r["ServiceGUID"].ToString();

                if (_dictService.ContainsKey(serviceGUID))
                {
                    _dictService.Remove(serviceGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
                }
            }
        }
        #endregion

        #region Window Event Handlers
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

        private void dlgAddChiDinh_Load(object sender, EventArgs e)
        {
            InitData();
            if (!_isNew) DisplayInfo();
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
                if (!_dictService.ContainsKey(serviceGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictService.Add(serviceGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictService.ContainsKey(serviceGUID))
                {
                    _dictService.Remove(serviceGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
                }
            }
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
                    string serviceGUID = r["ServiceGUID"].ToString();

                    if (chkChecked.Checked)
                    {
                        if (!_dictService.ContainsKey(serviceGUID))
                        {
                            _dtTemp.ImportRow(r);
                            _dictService.Add(serviceGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                        }
                    }
                    else
                    {
                        if (_dictService.ContainsKey(serviceGUID))
                        {
                            _dictService.Remove(serviceGUID);

                            DataRow[] rows = _dtTemp.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
                            if (rows != null && rows.Length > 0)
                                _dtTemp.Rows.Remove(rows[0]);
                        }
                    }
                }
            }
        }

        private void dlgAddChiDinh_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                {
                    SaveInfoAsThread();

                    if (_isNew)
                    {
                        MsgBox.Show(this.Text, "Đã thêm chỉ định thành công.", IconType.Information);
                        base.RaiseAddChiDinh(_chiDinh, TenBacSiChiDinh);
                        GenerateCode();
                        ClearAllCheck();
                        e.Cancel = true;
                    }
                }
                else
                    e.Cancel = true;
            }
            else
            {
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin chỉ định ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (CheckInfo())
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        SaveInfoAsThread();
                    }
                    else
                        e.Cancel = true;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgService_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            UpdateDichVuChiDinh();
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

        private void OnSaveInfoProc(object state)
        {
            try
            {
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
