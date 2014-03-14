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
    public partial class dlgAddUserGroup : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private UserGroup _userGroup = new UserGroup();
        private DataRow _drUserGroup = null;
        private int _selecedColumnIndex = 0;
        #endregion

        #region Constructor
        public dlgAddUserGroup()
        {
            InitializeComponent();
        }

        public dlgAddUserGroup(DataRow drUserGroup)
        {
            InitializeComponent();
            _isNew = false;
            this.Text = "Sua nhom nguoi su dung";
            _drUserGroup = drUserGroup;
        }
        #endregion

        #region Properties
        public UserGroup UserGroup
        {
            get { return _userGroup; }
            set { _userGroup = value; }
        }
        #endregion

        #region UI Command
        private void DisplayInfo(DataRow drUserGroup)
        {
            try
            {
                _userGroup.UserGroupGUID = Guid.Parse(_drUserGroup["UserGroupGUID"].ToString());
                txtGroupName.Text = _drUserGroup["GroupName"] as string;

                if (_drUserGroup["CreatedDate"] != null && _drUserGroup["CreatedDate"] != DBNull.Value)
                    _userGroup.CreatedDate = Convert.ToDateTime(_drUserGroup["CreatedDate"]);

                if (_drUserGroup["CreatedBy"] != null && _drUserGroup["CreatedBy"] != DBNull.Value)
                    _userGroup.CreatedBy = Guid.Parse(_drUserGroup["CreatedBy"].ToString());

                if (_drUserGroup["UpdatedDate"] != null && _drUserGroup["UpdatedDate"] != DBNull.Value)
                    _userGroup.UpdatedDate = Convert.ToDateTime(_drUserGroup["UpdatedDate"]);

                if (_drUserGroup["UpdatedBy"] != null && _drUserGroup["UpdatedBy"] != DBNull.Value)
                    _userGroup.UpdatedBy = Guid.Parse(_drUserGroup["UpdatedBy"].ToString());

                if (_drUserGroup["DeletedDate"] != null && _drUserGroup["DeletedDate"] != DBNull.Value)
                    _userGroup.DeletedDate = Convert.ToDateTime(_drUserGroup["DeletedDate"]);

                if (_drUserGroup["DeletedBy"] != null && _drUserGroup["DeletedBy"] != DBNull.Value)
                    _userGroup.DeletedBy = Guid.Parse(_drUserGroup["DeletedBy"].ToString());

                _userGroup.Status = Convert.ToByte(_drUserGroup["Status"]);

                DisplayPermissionAsThread(_userGroup.UserGroupGUID.ToString());
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void DisplayPermissionAsThread(string userGroupGUID)
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayPermissionProc), userGroupGUID);
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
                (row.Cells["IsCreateReport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                (row.Cells["IsUpload"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                (row.Cells["IsSendSMS"] as DataGridViewDisableCheckBoxCell).Enabled = false;

                if (functionCode == Const.DocStaff)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Patient)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Speciality)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Company)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Services)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Contract)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Permission)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Symptom)
                {
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Thuoc)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.NhomThuoc)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.LoThuoc)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.GiaThuoc)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.KeToa)
                {
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.ChiDinh)
                {
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.ServiceGroup)
                {
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.InKetQuaKhamSucKhoeTongQuat)
                {
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.GiaVonDichVu)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.DoanhThuTheoNgay)
                {
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.ThongKeHoaDon)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.HoaDonHopDong)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.NhatKyLienHeCongTy)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = true;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.Booking)
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.XetNghiem)
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.BaoCaoKhachHangMuaThuoc)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.BaoCaoSoLuongKham)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.TraCuuThongTinKhachHang)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.DiaChiCongTy)
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.ChiTietPhieuThuDichVu)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.ThuocTonKho)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.BenhNhanThanThuoc)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.KetQuaSieuAm)
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.LoaiSieuAm)
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.CongTacNgoaiGio)
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.LichKham)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.KhoCapCuu)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.NhapKhoCapCuu)
                {
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.XuatKhoCapCuu)
                {
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.BaoCaoCapCuuHetHan)
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.BaoCaoTonKhoCapCuu)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.TiemNgua)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.ThongBao)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.BenhNhanNgoaiGoiKham)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.PhieuThuCapCuu)
                {
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.NhomNguoiSuDung)
                {
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.NguoiSuDung)
                {
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.KetQuaCanLamSang)
                {
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.TaoHoSo)
                {
                    (row.Cells["IsCreateReport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.UploadHoSo)
                {
                    (row.Cells["IsUpload"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.KeToaCapCuu)
                {
                    (row.Cells["IsUpload"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.TaoMatKhauHoSo)
                {
                    (row.Cells["IsUpload"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.KhamHopDong)
                {
                    (row.Cells["IsUpload"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.TinNhanMau)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.GuiSMS)
                {
                    (row.Cells["IsSendSMS"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsUpload"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.BaoCaoCongNoHopDong)
                {
                    (row.Cells["IsSendSMS"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsUpload"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.SMSLog)
                {
                    (row.Cells["IsSendSMS"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsUpload"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.CapNhatNhanhChecklist)
                {
                    (row.Cells["IsSendSMS"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsUpload"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.ToaThuocTrongNgay)
                {
                    (row.Cells["IsSendSMS"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsUpload"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.NhanVienTrungLap)
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.ChuyenBenhAn)
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.DichVuXetNghiem)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.ThongKeThuocXuatHoaDon)
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.MapMauHoSoVoiDichVu)
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.InMauHoSo)
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.CauHinhDichVuXetNghiem)
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.TraHoSo)
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.DoanhThuTheoNhomDichVu)
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.HuyThuoc)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.ThongKeHoaDonDichVuVaThuoc)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.ThongKePhieuThuDichVuVaThuoc)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.HoaDonXetNghiem)
                {
                    (row.Cells["IsView"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsAdd"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsEdit"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsDelete"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsImport"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExport"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsPrint"] as DataGridViewDisableCheckBoxCell).Enabled = true;
                    (row.Cells["IsConfirm"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsLock"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.ThayDoiSoHoaDon)
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.ThayDoiSoHoaDonXetNghiem)
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
                else if (functionCode == Const.BaoCaoDoanhThuThuocTheoPhieuThu)
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
                    (row.Cells["IsExportAll"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                }
            }
        }

        private void OnDisplayPermission(string userGroupGUID)
        {
            Result result = UserGroupBus.GetPermission(userGroupGUID);
            if (result.IsOK)
            {
                Result funcResult = UserGroupBus.GetFunction();
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
                                newRow["IsExportAll"] = false;
                                newRow["IsCreateReport"] = false;
                                newRow["IsUpload"] = false;
                                newRow["IsSendSMS"] = false;
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
                                    newRow["IsExportAll"] = false;
                                    newRow["IsCreateReport"] = false;
                                    newRow["IsUpload"] = false;
                                    newRow["IsSendSMS"] = false;
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
                    MsgBox.Show(this.Text, funcResult.GetErrorAsString("UserGroupBus.GetFunction"), IconType.Error);
                    Utility.WriteToTraceLog(funcResult.GetErrorAsString("UserGroupBus.GetFunction"));
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("UserGroupBus.GetPermission"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("UserGroupBus.GetPermission"));
            }
        }

        private bool CheckInfo()
        {
            if (txtGroupName.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên nhóm người sử dụng.", IconType.Information);
                txtGroupName.Focus();
                return false;
            }

            string userGroupGUID = _isNew ? string.Empty : _userGroup.UserGroupGUID.ToString();
            Result result = UserGroupBus.CheckUserGroupExist(userGroupGUID, txtGroupName.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Tên nhóm người sử dụng này đã tồn tại rồi. Vui lòng nhập tên khác.", IconType.Information);
                    txtGroupName.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("UserGroupBus.CheckUserGroupExist"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("UserGroupBus.CheckUserGroupExist"));
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
                _userGroup.Status = (byte)Status.Actived;
                
                if (_isNew)
                {
                    _userGroup.CreatedDate = DateTime.Now;
                    _userGroup.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _userGroup.UpdatedDate = DateTime.Now;
                    _userGroup.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                MethodInvoker method = delegate
                {
                    _userGroup.GroupName = txtGroupName.Text;

                    DataTable dtPermission = dgPermission.DataSource as DataTable;
                    Result result = UserGroupBus.InsertUserGroup(_userGroup, dtPermission);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("UserGroupBus.InsertUserGroup"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("UserGroupBus.InsertUserGroup"));
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
                DisplayInfo(_drUserGroup);
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
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin nhóm người sử dụng ?") == System.Windows.Forms.DialogResult.Yes)
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
                if (cell.Enabled)
                {
                    cell.Value = true;
                    cell.EditingCellFormattedValue = true;
                }
            }

            dgPermission.Refresh();
        }

        private void unselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgPermission.Rows)
            {
                DataGridViewDisableCheckBoxCell cell = (DataGridViewDisableCheckBoxCell)row.Cells[_selecedColumnIndex];
                if (cell.Enabled)
                {
                    cell.Value = false;
                    cell.EditingCellFormattedValue = false;
                }
            }

            dgPermission.Refresh();
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
                dgPermission.Columns[e.ColumnIndex].Name != "IsLock" && dgPermission.Columns[e.ColumnIndex].Name != "IsExportAll" &&
                dgPermission.Columns[e.ColumnIndex].Name != "IsCreateReport" && dgPermission.Columns[e.ColumnIndex].Name != "IsUpload" &&
                dgPermission.Columns[e.ColumnIndex].Name != "IsSendSMS")
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
