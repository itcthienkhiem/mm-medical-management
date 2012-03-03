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
            get
            {
                if (_dataSource == null || _dataSource.Rows.Count <= 0) return null;
                UpdateChecked();
                List<DataRow> checkedRows = new List<DataRow>();
                foreach (DataRow row in _dataSource.Rows)
                {
                    if (Convert.ToBoolean(row["Checked"]))
                        checkedRows.Add(row);
                }

                return checkedRows;
            }

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
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServicesBus.GetServicesList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.GetServicesList"));
            }
        }

        private void OnSearchService()
        {
            UpdateChecked();

            chkChecked.Checked = false;
            if (txtSearchService.Text.Trim() == string.Empty)
            {
                dgService.DataSource = _dataSource;
                UpdateDichVuChiDinh();
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
                UpdateDichVuChiDinh();
                return;
            }


            //Name
            results = (from p in _dataSource.AsEnumerable()
                       where p.Field<string>("Name") != null &&
                           p.Field<string>("Name").Trim() != string.Empty &&
                           //(p.Field<string>("Name").ToLower().IndexOf(str) >= 0 ||
                       //str.IndexOf(p.Field<string>("Name").ToLower()) >= 0)
                       p.Field<string>("Name").ToLower().IndexOf(str) >= 0
                       select p).ToList<DataRow>();

            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgService.DataSource = newDataSource;
                UpdateDichVuChiDinh();
                return;
            }

            dgService.DataSource = newDataSource;

            UpdateDichVuChiDinh();
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

        private void DisplayInfo()
        {
            try
            {
                _chiDinh.ChiDinhGUID = Guid.Parse(_drChiDinh["ChiDinhGUID"].ToString());
                txtMaChiDinh.Text = _drChiDinh["MaChiDinh"].ToString();
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
