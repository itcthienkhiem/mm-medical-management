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
    public partial class dlgAddUserLogon : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private Logon _logon = new Logon();
        private DataRow _drLogon = null;
        private int _selecedColumnIndex = 0;
        #endregion

        #region Constructor
        public dlgAddUserLogon()
        {
            InitializeComponent();
            InitData();
        }

        public dlgAddUserLogon(DataRow drLogon)
        {
            InitializeComponent();
            _isNew = false;
            this.Text = "Sua nguoi su dung";
            _drLogon = drLogon;
            cboDocStaff.Enabled = false;
            InitData();
        }
        #endregion

        #region Properties
        public Logon Logon
        {
            get { return _logon; }
            set { _logon = value; }
        }

        public string StaffTypeStr
        {
            get
            {
                DataTable dt = cboDocStaff.DataSource as DataTable;
                DataRow[] rows = dt.Select(string.Format("DocStaffGUID='{0}'", cboDocStaff.SelectedValue.ToString()));
                if (rows != null && rows.Length > 0)
                {
                    StaffType type = (StaffType)Convert.ToInt32(rows[0]["StaffType"]);
                    switch (type)
                    {
                        case StaffType.BacSi:
                            return "Bác sĩ";
                        case StaffType.DieuDuong:
                            return "Điều dưỡng";
                        case StaffType.LeTan:
                            return "Lễ tân";
                        case StaffType.BenhNhan:
                            return "Bệnh nhân";
                        case StaffType.Admin:
                            return "Admin";
                        case StaffType.KeToan:
                            return "Kế toán";
                        case StaffType.ThuKyYKhoa:
                            return "Thư ký y khoa";
                        case StaffType.XetNghiem:
                            return "Xét nghiệm";
                        case StaffType.Sale:
                            return "Sale";
                        case StaffType.BacSiSieuAm:
                            return "Bác sĩ siêu âm";
                        case StaffType.BacSiNgoaiTongQuat:
                            return "Bác sĩ ngoại tổng quát";
                        case StaffType.BacSiNoiTongQuat:
                            return "Bác sĩ nội tổng quát";
                        case StaffType.BacSiPhuKhoa:
                            return "Bác sĩ phụ khoa";
                        default:
                            return string.Empty;
                    }
                }
                else
                    return string.Empty;
            }
        }

        public string FullName
        {
            get
            {
                DataTable dt = cboDocStaff.DataSource as DataTable;
                DataRow[] rows = dt.Select(string.Format("DocStaffGUID='{0}'", cboDocStaff.SelectedValue.ToString()));
                if (rows != null && rows.Length > 0)
                    return rows[0]["FullName"].ToString();
                else
                    return string.Empty;
            }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            string docStaffGUID = _isNew ? Guid.Empty.ToString() : _drLogon["DocStaffGUID"].ToString();
            Result result = DocStaffBus.GetDocStaffListWithoutLogon(docStaffGUID);
            if (result.IsOK)
            {
                cboDocStaff.DataSource = result.QueryResult as DataTable;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
            }
        }

        private void DisplayInfo(DataRow drLogon)
        {
            try
            {
                RijndaelCrypto crypt = new RijndaelCrypto();
                cboDocStaff.SelectedValue = drLogon["DocStaffGUID"].ToString();
                txtPassword.Text = crypt.Decrypt(drLogon["Password"].ToString());

                _logon.LogonGUID = Guid.Parse(drLogon["LogonGUID"].ToString());

                if (drLogon["CreatedDate"] != null && drLogon["CreatedDate"] != DBNull.Value)
                    _logon.CreatedDate = Convert.ToDateTime(drLogon["CreatedDate"]);

                if (drLogon["CreatedBy"] != null && drLogon["CreatedBy"] != DBNull.Value)
                    _logon.CreatedBy = Guid.Parse(drLogon["CreatedBy"].ToString());

                if (drLogon["UpdatedDate"] != null && drLogon["UpdatedDate"] != DBNull.Value)
                    _logon.UpdatedDate = Convert.ToDateTime(drLogon["UpdatedDate"]);

                if (drLogon["UpdatedBy"] != null && drLogon["UpdatedBy"] != DBNull.Value)
                    _logon.UpdatedBy = Guid.Parse(drLogon["UpdatedBy"].ToString());

                if (drLogon["DeletedDate"] != null && drLogon["DeletedDate"] != DBNull.Value)
                    _logon.DeletedDate = Convert.ToDateTime(drLogon["DeletedDate"]);

                if (drLogon["DeletedBy"] != null && drLogon["DeletedBy"] != DBNull.Value)
                    _logon.DeletedBy = Guid.Parse(drLogon["DeletedBy"].ToString());

                _logon.Status = Convert.ToByte(drLogon["Status"]);

                DisplayPermissionAsThread(_logon.LogonGUID.ToString());
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void DisplayPermissionAsThread(string logonGUID)
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayPermissionProc), logonGUID);
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

        private void UpdateGUI()
        {
            foreach (DataGridViewRow row in dgPermission.Rows)
            {
                string functionCode = row.Cells["FunctionCode"].Value.ToString();
                if (functionCode == Const.DocStaff)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Patient)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Speciality)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Company)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Services)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.ServicePrice)
                {
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Contract)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                }
                else if (functionCode == Const.OpenPatient)
                {
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Permission)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Symptom)
                {
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.PrintLabel)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Receipt)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Invoice)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.DuplicatePatient)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.DoanhThuNhanVien)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.DichVuHopDong)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Thuoc)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.NhomThuoc)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.LoThuoc)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.GiaThuoc)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.KeToa)
                {
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.ThuocHetHan)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.ThuocTonKho)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.PhieuThuThuoc)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.DichVuTuTuc)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.ChiDinh)
                {
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Tracking)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.ServiceGroup)
                {
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.InKetQuaKhamSucKhoeTongQuat)
                {
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.GiaVonDichVu)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.DoanhThuTheoNgay)
                {
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.PhongCho)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.DichVuChuaXuatPhieuThu)
                {
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.HoaDonThuoc)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.HoaDonXuatTruoc)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.DangKyHoaDonXuatTruoc)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.ThongKeHoaDon)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.PhucHoiBenhNhan)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.PhieuThuHopDong)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.HoaDonHopDong)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.YKienKhachHang)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                }
                else if (functionCode == Const.NhatKyLienHeCongTy)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                }
                else if (functionCode == Const.DichVuDaSuDung)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.CanDo)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.KhamLamSang)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.LoiKhuyen)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.KetLuan)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.KhamNoiSoi)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                }
                else if (functionCode == Const.KhamCTC)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                }
            }
        }

        private void OnDisplayPermission(string logonGUID)
        {
            Result result = LogonBus.GetPermission(logonGUID);
            if (result.IsOK)
            {
                Result funcResult = LogonBus.GetFunction();
                if (funcResult.IsOK)
                {
                    DataTable dtPermission = result.QueryResult as DataTable;
                    DataTable dtFunction = funcResult.QueryResult as DataTable;

                    MethodInvoker method = delegate
                    {
                        if (_isNew)
                        {
                            foreach (DataRow row in dtFunction.Rows)
                            {
                                DataRow newRow = dtPermission.NewRow();
                                newRow["FunctionGUID"] = row["FunctionGUID"];
                                newRow["FunctionCode"] = row["FunctionCode"];
                                newRow["FunctionName"] = row["FunctionName"];
                                newRow["IsView"] = false;
                                newRow["IsAdd"] = false;
                                newRow["IsEdit"] = false;
                                newRow["IsDelete"] = false;
                                newRow["IsPrint"] = false;
                                newRow["IsExport"] = false;
                                newRow["IsImport"] = false;
                                newRow["IsConfirm"] = false;
                                newRow["IsLock"] = false;
                                dtPermission.Rows.Add(newRow);
                            }

                            dgPermission.DataSource = dtPermission;
                            UpdateGUI();
                        }
                        else
                        {
                            foreach (DataRow row in dtFunction.Rows)
                            {
                                string functionGUID = row["FunctionGUID"].ToString();
                                DataRow[] rows = dtPermission.Select(string.Format("FunctionGUID='{0}'", functionGUID));
                                if (rows == null || rows.Length <= 0)
                                {
                                    DataRow newRow = dtPermission.NewRow();
                                    newRow["FunctionGUID"] = row["FunctionGUID"];
                                    newRow["FunctionCode"] = row["FunctionCode"];
                                    newRow["FunctionName"] = row["FunctionName"];
                                    newRow["IsView"] = false;
                                    newRow["IsAdd"] = false;
                                    newRow["IsEdit"] = false;
                                    newRow["IsDelete"] = false;
                                    newRow["IsPrint"] = false;
                                    newRow["IsExport"] = false;
                                    newRow["IsImport"] = false;
                                    newRow["IsConfirm"] = false;
                                    newRow["IsLock"] = false;
                                    dtPermission.Rows.Add(newRow);
                                }
                            }

                            dgPermission.DataSource = dtPermission;
                            UpdateGUI();
                        }
                    };

                    if (InvokeRequired) BeginInvoke(method);
                    else method.Invoke();
                }
                else
                {
                    MsgBox.Show(this.Text, funcResult.GetErrorAsString("LogonBus.GetFunction"), IconType.Error);
                    Utility.WriteToTraceLog(funcResult.GetErrorAsString("LogonBus.GetFunction"));
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("LogonBus.GetPermission"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("LogonBus.GetPermission"));
            }
        }

        private bool CheckInfo()
        {
            if (cboDocStaff.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ.", IconType.Information);
                cboDocStaff.Focus();
                return false;
            }

            if (!Utility.IsValidPassword(txtPassword.Text))
            {
                MsgBox.Show(this.Text, "Mật khẩu không hợp lệ (4-12 kí tự). Vui lòng nhập lại.", IconType.Information);
                txtPassword.Focus();
                return false;
            }

            string logonGUID = _isNew ? string.Empty : _logon.LogonGUID.ToString();
            Result result = LogonBus.CheckUserLogonExist(logonGUID, cboDocStaff.SelectedValue.ToString());

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Bác sĩ này đã được cấp tài khoản đăng nhập rồi. Vui lòng chọn bác sĩ khác.", IconType.Information);
                    cboDocStaff.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("LogonBus.CheckUserLogonExist"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("LogonBus.CheckUserLogonExist"));
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

                RijndaelCrypto crypt = new RijndaelCrypto();
                _logon.Status = (byte)Status.Actived;
                _logon.Password = crypt.Encrypt(txtPassword.Text);


                if (_isNew)
                {
                    _logon.CreatedDate = DateTime.Now;
                    _logon.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _logon.UpdatedDate = DateTime.Now;
                    _logon.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                MethodInvoker method = delegate
                {
                    _logon.DocStaffGUID = Guid.Parse(cboDocStaff.SelectedValue.ToString());

                    DataTable dtPermission = dgPermission.DataSource as DataTable;
                    Result result = LogonBus.InsertUserLogon(_logon, dtPermission);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("LogonBus.InsertUserLogon"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("LogonBus.InsertUserLogon"));
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
        private void dlgAddUserLogon_Load(object sender, EventArgs e)
        {
            if (_isNew)
                DisplayPermissionAsThread(Guid.Empty.ToString());
            else
                DisplayInfo(_drLogon);
        }

        private void dlgAddUserLogon_FormClosing(object sender, FormClosingEventArgs e)
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
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin người dùng ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgPermission.Rows)
            {
                DataGridViewDisableCheckBoxCell cell = (DataGridViewDisableCheckBoxCell)row.Cells[_selecedColumnIndex];
                if (cell.Enabled) cell.Value = true;
            }
        }

        private void unselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgPermission.Rows)
            {
                DataGridViewDisableCheckBoxCell cell = (DataGridViewDisableCheckBoxCell)row.Cells[_selecedColumnIndex];
                if (cell.Enabled) cell.Value = false;
            }
        }

        private void dgPermission_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                dgPermission.ContextMenuStrip = null;
                return;
            }

            if (dgPermission.Columns[e.ColumnIndex].Name != "IsView" && dgPermission.Columns[e.ColumnIndex].Name != "IsAdd" &&
                dgPermission.Columns[e.ColumnIndex].Name != "IsEdit" && dgPermission.Columns[e.ColumnIndex].Name != "IsDelete" &&
                dgPermission.Columns[e.ColumnIndex].Name != "IsPrint" && dgPermission.Columns[e.ColumnIndex].Name != "IsImport" &&
                dgPermission.Columns[e.ColumnIndex].Name != "IsExport" && dgPermission.Columns[e.ColumnIndex].Name != "IsConfirm" &&
                dgPermission.Columns[e.ColumnIndex].Name != "IsLock")
            {
                dgPermission.ContextMenuStrip = null;
                return;
            }

            dgPermission.ContextMenuStrip = ctmPermission;
            _selecedColumnIndex = e.ColumnIndex;
        }
        #endregion

        #region Working Thread
        private void OnDisplayPermissionProc(object state)
        {
            try
            {
                //Thread.Sleep(1000);
                OnDisplayPermission(state.ToString());
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
