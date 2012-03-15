using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Bussiness;
using MM.Common;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgAddServiceHistory : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private ServiceHistory _serviceHistory = new ServiceHistory();
        private string _patientGUID = string.Empty;
        private DataRow _drServiceHistory = null;
        private StaffType _staffType = StaffType.None;
        private string _serviceGUID = string.Empty;
        private string _bacSiChiDinhGUID = string.Empty;
        private ChiDinh _chiDinh = null;
        private bool _isExported = false;
        private DataTable _dtCheckList = null;
        #endregion

        #region Constructor
        public dlgAddServiceHistory(string patientGUID)
        {
            InitializeComponent();
            _patientGUID = patientGUID;
        }

        public dlgAddServiceHistory(string patientGUID, DataRow drServiceHistory)
        {
            InitializeComponent();
            _isNew = false;
            _patientGUID = patientGUID;
            this.Text = "Sua su dung dich vu";
            _drServiceHistory = drServiceHistory;
        }
        #endregion

        #region Properties
        public string ServiceGUID
        {
            set { _serviceGUID = value; }
        }

        public ServiceHistory ServiceHistory
        {
            get { return _serviceHistory; }
        }

        public string FullName
        {
            get { return cboDocStaff.Text; }
        }

        public string ServiceName
        {
            get { return cboService.Text; }
        }

        public string ServiceCode
        {
            get
            {
                DataTable dt = cboService.DataSource as DataTable;
                if (dt == null || dt.Rows.Count <= 0) return string.Empty;

                string serviceGUID = cboService.SelectedValue.ToString();
                DataRow[] rows = dt.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
                if (rows != null && rows.Length > 0)
                    return rows[0]["Code"].ToString();

                return string.Empty;
            }
        }

        public string BacSiChiDinhGUID
        {
            get { return _bacSiChiDinhGUID; }
            set { _bacSiChiDinhGUID = value; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            Cursor.Current = Cursors.WaitCursor;
            dtpkActiveDate.Value = DateTime.Now;

            //Service
            Result result = ServicesBus.GetServicesList();
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ServicesBus.GetServicesList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.GetServicesList"));
                return;
            }
            else
            {
                cboService.DataSource = result.QueryResult;
            }

            DisplayBacSiChiDinhList();
        }

        private void GetCheckListByPatient(string patientGUID)
        {
            Result result = CompanyContractBus.GetCheckListByPatient(patientGUID);
            if (result.IsOK)
            {
                if (_dtCheckList != null)
                {
                    _dtCheckList.Rows.Clear();
                    _dtCheckList = null;
                }

                _dtCheckList = result.QueryResult as DataTable;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyBus.GetCompanyMemberList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.GetCompanyMemberList"));
            }
        }

        private void DisplayBacSiChiDinhList()
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
                DataTable dt = result.QueryResult as DataTable;
                DataRow newRow = dt.NewRow();
                newRow["Fullname"] = string.Empty;
                newRow["DocStaffGUID"] = Guid.Empty.ToString();
                dt.Rows.InsertAt(newRow, 0);

                cboBacSiChiDinh.DataSource = dt;
            }
        }

        private void DisplayDocStaffList()
        {
            //DocStaff
            List<byte> staffTypes = new List<byte>();
            if (_staffType != StaffType.None)
                staffTypes.Add((byte)_staffType);
            else
            {
                staffTypes.Add((byte)StaffType.BacSi);
                staffTypes.Add((byte)StaffType.BacSiNgoaiTongQuat);
                staffTypes.Add((byte)StaffType.BacSiNoiTongQuat);
                staffTypes.Add((byte)StaffType.BacSiPhuKhoa);
                staffTypes.Add((byte)StaffType.BacSiSieuAm);
                staffTypes.Add((byte)StaffType.DieuDuong);
                staffTypes.Add((byte)StaffType.XetNghiem);
            }

            Result result = DocStaffBus.GetDocStaffList(staffTypes);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
                return;
            }
            else
            {
                DataTable dt = result.QueryResult as DataTable;
                DataRow newRow = dt.NewRow();
                newRow["DocStaffGUID"] = Guid.Empty.ToString();
                newRow["FullName"] = string.Empty;
                dt.Rows.InsertAt(newRow, 0);
                cboDocStaff.DataSource = dt;
            }

            if (Global.StaffType == _staffType)
            {
                cboDocStaff.SelectedValue = Global.UserGUID;
                cboDocStaff.Enabled = false;
            }
            else
                cboDocStaff.Enabled = true;
        }

        private void DisplayInfo(DataRow drServiceHistory)
        {
            try
            {
                cboService.SelectedValue = drServiceHistory["ServiceGUID"].ToString();
                cboDocStaff.SelectedValue = drServiceHistory["DocStaffGUID"].ToString();

                if (drServiceHistory["RootPatientGUID"] != null && drServiceHistory["RootPatientGUID"] != DBNull.Value)
                {
                    txtChuyenNhuong.Tag = drServiceHistory["RootPatientGUID"].ToString();
                    chkChuyenNhuong.Checked = true;
                }
                
                if (drServiceHistory["TenBenhNhanChuyenNhuong"] != null && drServiceHistory["TenBenhNhanChuyenNhuong"] != DBNull.Value)
                    txtChuyenNhuong.Text = drServiceHistory["TenBenhNhanChuyenNhuong"].ToString();

                numPrice.Value = (decimal)Double.Parse(drServiceHistory["FixedPrice"].ToString());
                numDiscount.Value = (decimal)Double.Parse(drServiceHistory["Discount"].ToString());
                txtDescription.Text = drServiceHistory["Note"] as string;
                _serviceHistory.ServiceHistoryGUID = Guid.Parse(drServiceHistory["ServiceHistoryGUID"].ToString());

                bool isNormalOrNegative = Convert.ToBoolean(drServiceHistory["IsNormalOrNegative"]);
                bool normal = Convert.ToBoolean(drServiceHistory["Normal"]);
                bool abnormal = Convert.ToBoolean(drServiceHistory["Abnormal"]);
                bool negative = Convert.ToBoolean(drServiceHistory["Negative"]);
                bool positive = Convert.ToBoolean(drServiceHistory["Positive"]);

                raNormal.Checked = isNormalOrNegative;
                raNegative.Checked = !isNormalOrNegative;
                chkNormal.Checked = normal;
                chkAbnormal.Checked = abnormal;
                chkNegative.Checked = negative;
                chkPositive.Checked = positive;
                raKhamTuTuc.Checked = Convert.ToBoolean(drServiceHistory["KhamTuTuc"]);
                raKhamTheoHopDong.Checked = !raKhamTuTuc.Checked;

                if (drServiceHistory["ActivedDate"] != null && drServiceHistory["ActivedDate"] != DBNull.Value)
                {
                    _serviceHistory.ActivedDate = Convert.ToDateTime(drServiceHistory["ActivedDate"]);
                    dtpkActiveDate.Value = _serviceHistory.ActivedDate.Value;
                }

                if (drServiceHistory["CreatedDate"] != null && drServiceHistory["CreatedDate"] != DBNull.Value)
                    _serviceHistory.CreatedDate = Convert.ToDateTime(drServiceHistory["CreatedDate"]);

                if (drServiceHistory["CreatedBy"] != null && drServiceHistory["CreatedBy"] != DBNull.Value)
                    _serviceHistory.CreatedBy = Guid.Parse(drServiceHistory["CreatedBy"].ToString());

                if (drServiceHistory["UpdatedDate"] != null && drServiceHistory["UpdatedDate"] != DBNull.Value)
                    _serviceHistory.UpdatedDate = Convert.ToDateTime(drServiceHistory["UpdatedDate"]);

                if (drServiceHistory["UpdatedBy"] != null && drServiceHistory["UpdatedBy"] != DBNull.Value)
                    _serviceHistory.UpdatedBy = Guid.Parse(drServiceHistory["UpdatedBy"].ToString());

                if (drServiceHistory["DeletedDate"] != null && drServiceHistory["DeletedDate"] != DBNull.Value)
                    _serviceHistory.DeletedDate = Convert.ToDateTime(drServiceHistory["DeletedDate"]);

                if (drServiceHistory["DeletedBy"] != null && drServiceHistory["DeletedBy"] != DBNull.Value)
                    _serviceHistory.DeletedBy = Guid.Parse(drServiceHistory["DeletedBy"].ToString());

                _serviceHistory.Status = Convert.ToByte(drServiceHistory["Status"]);

                Result result = ChiDinhBus.GetChiDinh(_serviceHistory.ServiceHistoryGUID.ToString());
                if (!result.IsOK)
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("ChiDinhBus.GetBacSiChiDinh"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.GetBacSiChiDinh"));
                    return;
                }
                else if (result.QueryResult != null)
                {
                    _chiDinh = (ChiDinh)result.QueryResult;
                    cboBacSiChiDinh.SelectedValue = _chiDinh.BacSiChiDinhGUID.ToString();
                    chkBSCD.Checked = true;
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
            if (cboService.Text == null || cboService.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn dịch vụ.", IconType.Information);
                cboService.Focus();
                return false;
            }

            if (chkBSCD.Checked && cboBacSiChiDinh.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ chỉ định", IconType.Information);
                cboBacSiChiDinh.Focus();
                return false;
            }

            if (raKhamTheoHopDong.Checked && !CheckDichVuTheoHopDongValid(_patientGUID)) return false;

            if (chkChuyenNhuong.Checked && txtChuyenNhuong.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn người chuyển nhượng.", IconType.Information);
                btnChonBenhNhan.Focus();
                return false;
            }

            if (chkChuyenNhuong.Checked && !CheckDichVuChuyenNhuongValid(txtChuyenNhuong.Tag.ToString())) return false;

            return true;
        }

        private string GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = ChiDinhBus.GetChiDinhCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                return Utility.GetCode("CD", count + 1, 5);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ChiDinhBus.GetChiDinhCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.GetChiDinhCount"));
            }

            return string.Empty;
        }

        private void OnSaveInfo()
        {
            try
            {
                if (_isNew)
                {
                    _serviceHistory.CreatedDate = DateTime.Now;
                    _serviceHistory.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _serviceHistory.UpdatedDate = DateTime.Now;
                    _serviceHistory.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                _serviceHistory.PatientGUID = Guid.Parse(_patientGUID);
                _serviceHistory.Note = txtDescription.Text;

                MethodInvoker method = delegate
                {
                    _serviceHistory.ActivedDate = dtpkActiveDate.Value;
                    _serviceHistory.KhamTuTuc = raKhamTuTuc.Checked;
                    if (cboDocStaff.Text != string.Empty)
                        _serviceHistory.DocStaffGUID = Guid.Parse(cboDocStaff.SelectedValue.ToString());
                    else
                        _serviceHistory.DocStaffGUID = null;

                    if (chkChuyenNhuong.Checked)
                        _serviceHistory.RootPatientGUID = Guid.Parse(txtChuyenNhuong.Tag.ToString());
                    else
                        _serviceHistory.RootPatientGUID = null;
                    
                    _serviceHistory.ServiceGUID = Guid.Parse(cboService.SelectedValue.ToString());
                    _serviceHistory.Price = (double)numPrice.Value;
                    _serviceHistory.Discount = (double)numDiscount.Value;

                    _serviceHistory.IsNormalOrNegative = raNormal.Checked;
                    if (raNormal.Checked)
                    {
                        _serviceHistory.Normal = chkNormal.Checked;
                        _serviceHistory.Abnormal = chkAbnormal.Checked;
                        _serviceHistory.Negative = false;
                        _serviceHistory.Positive = false;
                    }
                    else
                    {
                        _serviceHistory.Normal = false;
                        _serviceHistory.Abnormal = false;
                        _serviceHistory.Negative = chkNegative.Checked;
                        _serviceHistory.Positive = chkPositive.Checked;
                    }

                    Result result = GiaVonDichVuBus.GetGiaVonDichVuMoiNhat(_serviceHistory.ServiceGUID.ToString(), _serviceHistory.ActivedDate.Value);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("GiaVonDichVuBus.GetGiaVonDichVuMoiNhat"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("GiaVonDichVuBus.GetGiaVonDichVuMoiNhat"));
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                        return;
                    }

                    DataTable dt = result.QueryResult as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                        _serviceHistory.GiaVon = Convert.ToDouble(dt.Rows[0]["GiaVon"]);

                    result = ServiceHistoryBus.InsertServiceHistory(_serviceHistory);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("ServiceHistoryBus.InsertServiceHistory"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ServiceHistoryBus.InsertServiceHistory"));
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    }
                    else if (_serviceGUID == string.Empty)
                    {
                        if (chkBSCD.Checked)
                        {
                            if (_chiDinh == null)
                            {
                                _chiDinh = new ChiDinh();
                                string maChiDinh = GenerateCode();
                                if (maChiDinh == string.Empty) return;

                                //Insert chỉ định
                                _chiDinh.CreatedDate = DateTime.Now;
                                _chiDinh.CreatedBy = Guid.Parse(Global.UserGUID);
                                _chiDinh.MaChiDinh = maChiDinh;
                                _chiDinh.BenhNhanGUID = Guid.Parse(_patientGUID);
                                _chiDinh.BacSiChiDinhGUID = Guid.Parse(cboBacSiChiDinh.SelectedValue.ToString());
                                _chiDinh.NgayChiDinh = DateTime.Now;
                                _chiDinh.Status = (byte)Status.Actived;

                                List<ChiTietChiDinh> addedList = new List<ChiTietChiDinh>();
                                List<string> deletedKeys = new List<string>();
                                ChiTietChiDinh ctcd = new ChiTietChiDinh();
                                ctcd.CreatedDate = DateTime.Now;
                                ctcd.CreatedBy = Guid.Parse(Global.UserGUID);
                                ctcd.ServiceGUID = _serviceHistory.ServiceGUID.Value;
                                addedList.Add(ctcd);

                                result = ChiDinhBus.InsertChiDinh(_chiDinh, addedList, deletedKeys);
                                if (!result.IsOK)
                                {
                                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ChiDinhBus.InsertChiDinh"), IconType.Error);
                                    Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.InsertChiDinh"));
                                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                                }
                                else
                                {
                                    DichVuChiDinh dichVuChiDinh = new DichVuChiDinh();
                                    dichVuChiDinh.ServiceHistoryGUID = _serviceHistory.ServiceHistoryGUID;
                                    dichVuChiDinh.ChiTietChiDinhGUID = ctcd.ChiTietChiDinhGUID;
                                    dichVuChiDinh.CreatedDate = DateTime.Now;
                                    dichVuChiDinh.CraetedBy = Guid.Parse(Global.UserGUID);
                                    dichVuChiDinh.Status = (byte)Status.Actived;

                                    result = ChiDinhBus.InsertDichVuChiDinh(dichVuChiDinh);
                                    if (!result.IsOK)
                                    {
                                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ChiDinhBus.InsertDichVuChiDinh"), IconType.Error);
                                        Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.InsertDichVuChiDinh"));
                                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                                    }
                                }
                            }
                            else
                            {
                                _chiDinh.UpdatedDate = DateTime.Now;
                                _chiDinh.UpdatedBy = Guid.Parse(Global.UserGUID);
                                _chiDinh.Status = (byte)Status.Actived;

                                _chiDinh.BacSiChiDinhGUID = Guid.Parse(cboBacSiChiDinh.SelectedValue.ToString());
                                result = ChiDinhBus.UpdateChiDinh(_chiDinh, cboService.SelectedValue.ToString());
                                if (!result.IsOK)
                                {
                                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ChiDinhBus.UpdateChiDinh"), IconType.Error);
                                    Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.UpdateChiDinh"));
                                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                                }
                            }
                        }
                        else if (_chiDinh != null)
                        {
                            List<string> keys = new List<string>();
                            keys.Add(_chiDinh.ChiDinhGUID.ToString());
                            result = ChiDinhBus.DeleteChiDinhs(keys);
                            if (!result.IsOK)
                            {
                                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ChiDinhBus.DeleteChiDinhs"), IconType.Error);
                                Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.DeleteChiDinhs"));
                                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            }
                        }
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

        private bool CheckDichVuTheoHopDongValid(string patientGUID)
        {
            Cursor.Current = Cursors.WaitCursor;
            GetCheckListByPatient(patientGUID);

            if (_dtCheckList == null || _dtCheckList.Rows.Count <= 0)
            {
                MsgBox.Show(this.Text, string.Format("Dịch vụ: '{0}' không thể khám theo hợp đồng vì bệnh nhân này không có trong hợp đồng.", 
                    cboService.SelectedValue.ToString()), IconType.Information);
                return false;
            }
            else
            {
                string serviceGUID = cboService.SelectedValue.ToString();
                DataRow[] rows = _dtCheckList.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
                if (rows != null && rows.Length > 0)
                {
                    bool isOK = false;
                    foreach (DataRow row in rows)
                    {
                        DateTime beginDate = Convert.ToDateTime(row["BeginDate"]);
                        DateTime endDate = Global.MaxDateTime;
                        if (row["EndDate"] != null && row["EndDate"] != DBNull.Value)
                            endDate = Convert.ToDateTime(row["EndDate"]);

                        bool isUsed = Convert.ToBoolean(row["Checked"]);

                        if (!isUsed && dtpkActiveDate.Value >= beginDate && dtpkActiveDate.Value <= endDate)
                        {
                            isOK = true;
                            break;
                        }
                    }

                    if (!isOK)
                    {
                        if (_isNew)
                        {
                            MsgBox.Show(this.Text, string.Format("Dịch vụ: '{0}' không thể khám theo hợp đồng vì đã được sử dụng rồi.", 
                                cboService.SelectedValue.ToString()), IconType.Information);
                            return false;
                        }
                        else
                        {
                            bool isKhamTuTuc = Convert.ToBoolean(_drServiceHistory["KhamTuTuc"]);
                            if (isKhamTuTuc || _drServiceHistory["ServiceGUID"].ToString() != cboService.SelectedValue.ToString())
                            {
                                MsgBox.Show(this.Text, string.Format("Dịch vụ: '{0}' không thể khám theo hợp đồng vì đã được sử dụng rồi.",
                                cboService.SelectedValue.ToString()), IconType.Information);
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    MsgBox.Show(this.Text, string.Format("Dịch vụ: '{0}' không thể khám theo hợp đồng vì bệnh nhân này không có trong hợp đồng.",
                    cboService.SelectedValue.ToString()), IconType.Information);
                    return false;
                }
            }

            return true;
        }

        private bool CheckDichVuChuyenNhuongValid(string patientGUID)
        {
            Cursor.Current = Cursors.WaitCursor;
            GetCheckListByPatient(patientGUID);

            if (_dtCheckList == null || _dtCheckList.Rows.Count <= 0)
            {
                MsgBox.Show(this.Text, string.Format("Bệnh nhân: '{0}' không thể chuyển nhượng dịch vụ: '{1}' vì không có trong hợp đồng",
                    txtChuyenNhuong.Text, cboService.SelectedValue.ToString()), IconType.Information);
                return false;
            }
            else
            {
                string serviceGUID = cboService.SelectedValue.ToString();
                DataRow[] rows = _dtCheckList.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
                if (rows != null && rows.Length > 0)
                {
                    bool isOK = false;
                    foreach (DataRow row in rows)
                    {
                        DateTime beginDate = Convert.ToDateTime(row["BeginDate"]);
                        DateTime endDate = Global.MaxDateTime;
                        if (row["EndDate"] != null && row["EndDate"] != DBNull.Value)
                            endDate = Convert.ToDateTime(row["EndDate"]);

                        bool isUsed = Convert.ToBoolean(row["Checked"]);

                        if (!isUsed && dtpkActiveDate.Value >= beginDate && dtpkActiveDate.Value <= endDate)
                        {
                            isOK = true;
                            break;
                        }
                    }

                    if (!isOK)
                    {
                        if (_isNew)
                        {
                            MsgBox.Show(this.Text, string.Format("Bệnh nhân: '{0}' không thể chuyển nhượng dịch vụ: '{1}' vì dịch vụ này đã được sử dụng rồi.",
                                txtChuyenNhuong.Text, cboService.SelectedValue.ToString()), IconType.Information);
                            return false;
                        }
                        else
                        {
                            bool isChuyenNhuong = false;
                            if (_drServiceHistory["RootPatientGUID"] != null && _drServiceHistory["RootPatientGUID"] != DBNull.Value)
                                isChuyenNhuong = true;

                            if (!isChuyenNhuong || _drServiceHistory["RootPatientGUID"].ToString() != txtChuyenNhuong.Tag.ToString())
                            {
                                MsgBox.Show(this.Text, string.Format("Bệnh nhân: '{0}' không thể chuyển nhượng dịch vụ: '{1}' vì dịch vụ này đã được sử dụng rồi.",
                                txtChuyenNhuong.Text, cboService.SelectedValue.ToString()), IconType.Information);
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    MsgBox.Show(this.Text, string.Format("Dịch vụ: '{0}' không thể khám theo hợp đồng vì bệnh nhân này không có trong hợp đồng.",
                    cboService.SelectedValue.ToString()), IconType.Information);
                    return false;
                }
            }

            return true;
        }

        private void RefreshGUI()
        {
            Cursor.Current = Cursors.WaitCursor;
            GetCheckListByPatient(_patientGUID);
            if (_dtCheckList == null || _dtCheckList.Rows.Count <= 0)
            {
                raKhamTuTuc.Checked = true;
                raKhamTheoHopDong.Enabled = false;
            }
            else
            {
                string serviceGUID = cboService.SelectedValue.ToString();
                DataRow[] rows = _dtCheckList.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
                if (rows != null && rows.Length > 0)
                {
                    bool isOK = false;
                    foreach (DataRow row in rows)
                    {
                        DateTime beginDate = Convert.ToDateTime(row["BeginDate"]);
                        DateTime endDate = Global.MaxDateTime;
                        if (row["EndDate"] != null && row["EndDate"] != DBNull.Value)
                            endDate = Convert.ToDateTime(row["EndDate"]);

                        bool isUsed = Convert.ToBoolean(row["Checked"]);

                        if (!isUsed && dtpkActiveDate.Value >= beginDate && dtpkActiveDate.Value <= endDate)
                        {
                            isOK = true;
                            break;
                        }
                    }

                    if (isOK)
                    {
                        raKhamTheoHopDong.Enabled = true;
                    }
                    else
                    {
                        if (_isNew)
                        {
                            raKhamTuTuc.Checked = true;
                            raKhamTheoHopDong.Enabled = false;
                        }
                        else
                        {
                            bool isKhamTuTuc = Convert.ToBoolean(_drServiceHistory["KhamTuTuc"]);
                            if (isKhamTuTuc || _drServiceHistory["ServiceGUID"].ToString() != cboService.SelectedValue.ToString())
                            {
                                raKhamTuTuc.Checked = true;
                                raKhamTheoHopDong.Enabled = false;
                            }
                            else 
                                raKhamTheoHopDong.Enabled = true;
                        }
                    }
                }
                else
                {
                    raKhamTuTuc.Checked = true;
                    raKhamTheoHopDong.Enabled = false;
                }
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddServiceHistory_FormClosing(object sender, FormClosingEventArgs e)
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
                if (!_isExported && MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin dịch vụ sử dụng ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void cboService_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboService.SelectedValue == null || cboService.SelectedValue.ToString() == string.Empty) return;
            DataTable dt = cboService.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;

            string serviceGUID = cboService.SelectedValue.ToString();
            DataRow[] rows = dt.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
            if (rows != null && rows.Length > 0)
            {
                numPrice.Value = (decimal)Double.Parse(rows[0]["Price"].ToString());

                if (rows[0]["StaffType"] != null && rows[0]["StaffType"] != DBNull.Value)
                    _staffType = (StaffType)Convert.ToByte(rows[0]["StaffType"]);
                else
                    _staffType = StaffType.None;

                DisplayDocStaffList();
                RefreshGUI();
            }
        }

        private void dlgAddServiceHistory_Load(object sender, EventArgs e)
        {
            InitData();

            if (_serviceGUID != string.Empty)
            {
                cboService.SelectedValue = _serviceGUID;
                cboService.Enabled = false;
                btnChonDichVu.Enabled = false;
                chkBSCD.Checked = true;
                cboBacSiChiDinh.SelectedValue = _bacSiChiDinhGUID;
            }

            if (!_isNew)
            {
                DisplayInfo(_drServiceHistory);
                _isExported = Convert.ToBoolean(_drServiceHistory["IsExported"]);
                if (_isExported)
                {
                    cboService.Enabled = false;
                    btnChonDichVu.Enabled = false;
                    raKhamTuTuc.Enabled = false;
                    raKhamTheoHopDong.Enabled = false;
                    cboDocStaff.Enabled = false;
                    chkBSCD.Enabled = false;
                    cboBacSiChiDinh.Enabled = false;
                    chkChuyenNhuong.Enabled = false;
                    txtChuyenNhuong.ReadOnly= true;
                    btnChonBenhNhan.Enabled = false;
                    numPrice.Enabled = false;
                    numDiscount.Enabled = false;
                    dtpkActiveDate.Enabled = false;
                    raNegative.Enabled = false;
                    raNormal.Enabled = false;
                    chkAbnormal.Enabled = false;
                    chkNormal.Enabled = false;
                    chkPositive.Enabled = false;
                    chkNegative.Enabled = false;
                    txtDescription.Enabled = false;
                    btnOK.Enabled = false;
                }
            }

            lbPrice.Visible = Global.AllowShowServiePrice;
            lbUnit.Visible = Global.AllowShowServiePrice;
            numPrice.Visible = Global.AllowShowServiePrice;
        }

        private void raNormal_CheckedChanged(object sender, EventArgs e)
        {
            gbNormal.Enabled = raNormal.Checked;
        }

        private void raNegative_CheckedChanged(object sender, EventArgs e)
        {
            gbNegative.Enabled = raNegative.Checked;
        }

        private void chkNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkNormal.Checked && !chkAbnormal.Checked)
                chkNormal.Checked = true;

            if (chkNormal.Checked) chkAbnormal.Checked = false;
        }

        private void chkAbnormal_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAbnormal.Checked && !chkNormal.Checked) chkAbnormal.Checked = true;

            if (chkAbnormal.Checked) chkNormal.Checked = false;
        }

        private void chkNegative_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkNegative.Checked && !chkPositive.Checked)
                chkNegative.Checked = true;

            if (chkNegative.Checked) chkPositive.Checked = false;
        }

        private void chkPositive_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkNegative.Checked && !chkPositive.Checked)
                chkPositive.Checked = true;

            if (chkPositive.Checked) chkNegative.Checked = false;
        }

        private void chkBSCD_CheckedChanged(object sender, EventArgs e)
        {
            cboBacSiChiDinh.Enabled = chkBSCD.Checked;
        }

        private void btnChonDichVu_Click(object sender, EventArgs e)
        {
            DataTable dtService = cboService.DataSource as DataTable;
            dlgSelectSingleDichVu dlg = new dlgSelectSingleDichVu(dtService);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                cboService.SelectedValue = dlg.ServiceGUID;
            }
        }

        private void btnChonBenhNhan_Click(object sender, EventArgs e)
        {
            if (cboService.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn dịch vụ.", IconType.Information);
                cboService.Focus();
                return;
            }

            DateTime activedDate = dtpkActiveDate.Value;
            dlgSelectNhanVienHopDong dlg = new dlgSelectNhanVienHopDong(activedDate, cboService.SelectedValue.ToString(), _patientGUID);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtChuyenNhuong.Tag = dlg.PatientRow["PatientGUID"].ToString();
                txtChuyenNhuong.Text = dlg.PatientRow["FullName"].ToString();
            }
        }

        private void chkChuyenNhuong_CheckedChanged(object sender, EventArgs e)
        {
            btnChonBenhNhan.Enabled = chkChuyenNhuong.Checked;
            if (chkChuyenNhuong.Checked)
            {
                txtChuyenNhuong.Enabled = true;
                txtChuyenNhuong.ReadOnly = true;
            }
            else
                txtChuyenNhuong.Enabled = false;
        }

        private void dtpkActiveDate_ValueChanged(object sender, EventArgs e)
        {
            if (cboService.Text == string.Empty) return;
            RefreshGUI();
        }

        private void raKhamTuTuc_CheckedChanged(object sender, EventArgs e)
        {
            chkChuyenNhuong.Enabled = raKhamTuTuc.Checked;
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
        #endregion
    }
}
