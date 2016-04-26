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
using MM.Common;
using MM.Bussiness;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgAddTuVanKhachHang : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private TuVanKhachHang _tuVanKhachHang = new TuVanKhachHang();
        private DataRow _drTuVanKhachHang = null;
        private bool _isView = false;
        #endregion

        #region Constructor
        public dlgAddTuVanKhachHang()
        {
            InitializeComponent();
        }

        public dlgAddTuVanKhachHang(object drPatient)
        {
            InitializeComponent();

            DataRow patientRow = (DataRow)drPatient;

            txtTenKhachHang.Tag = patientRow["PatientGUID"].ToString();
            txtTenKhachHang.Text = patientRow["FullName"].ToString();
            txtSoDienThoai.Text = patientRow["Mobile"].ToString();
            txtDiaChi.Text = patientRow["Address"].ToString();
        }

        public dlgAddTuVanKhachHang(DataRow drTuVanKhachHang)
        {
            InitializeComponent();
            _drTuVanKhachHang = drTuVanKhachHang;
            _isNew = false;
            this.Text = "Sua tu van khach hang";
        }
        #endregion

        #region Properties
        public TuVanKhachHang TuVanKhachHang
        {
            get { return _tuVanKhachHang; }
            set { _tuVanKhachHang = value; }
        }
        #endregion

        #region UI Command
        private void OnDisplayNguonList()
        {
            Result result = TuVanKhachHangBus.GetNguonList();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    cboNguon.Items.Add(row["Nguon"].ToString());
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("TuVanKhachHangBus.GetNguonList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("TuVanKhachHangBus.GetNguonList"));
            }
        }

        private void DisplayInfo(DataRow drTuVanKhachHang)
        {
            try
            {
                txtTenKhachHang.Text = drTuVanKhachHang["TenKhachHang"] as string;
                txtSoDienThoai.Text = drTuVanKhachHang["SoDienThoai"] as string;
                txtDiaChi.Text = drTuVanKhachHang["DiaChi"] as string;
                txtYeuCau.Text = drTuVanKhachHang["YeuCau"] as string;
                cboNguon.Text = drTuVanKhachHang["Nguon"] as string;
                txtTenCongTy.Text = drTuVanKhachHang["TenCongTy"] as string;
                txtMaKhachHang.Text = drTuVanKhachHang["MaKhachHang"] as string;
                txtMucDich.Text = drTuVanKhachHang["MucDich"] as string;

                bool isIN = Convert.ToBoolean(drTuVanKhachHang["IsIN"]);
                raIN.Checked = isIN;
                raOUT.Checked = !isIN;

                txtSoTongDai.Text = drTuVanKhachHang["SoTongDai"] as string;

                if (drTuVanKhachHang["KetLuan"] != null && drTuVanKhachHang["KetLuan"] != DBNull.Value)
                    txtHuongGiaiQuyet.Text = drTuVanKhachHang["KetLuan"].ToString();

                if (drTuVanKhachHang["BacSiPhuTrachGUID"] != null && drTuVanKhachHang["BacSiPhuTrachGUID"] != DBNull.Value)
                    cboDocStaff.SelectedValue = drTuVanKhachHang["BacSiPhuTrachGUID"].ToString();

                chkDaXong.Checked = Convert.ToBoolean(drTuVanKhachHang["DaXong"]);
                chkBanThe.Checked = Convert.ToBoolean(drTuVanKhachHang["BanThe"]);

                _tuVanKhachHang.TuVanKhachHangGUID = Guid.Parse(drTuVanKhachHang["TuVanKhachHangGUID"].ToString());

                if (drTuVanKhachHang["ContactDate"] != null && drTuVanKhachHang["ContactDate"] != DBNull.Value)
                    _tuVanKhachHang.ContactDate = Convert.ToDateTime(drTuVanKhachHang["ContactDate"]);

                if (drTuVanKhachHang["ContactBy"] != null && drTuVanKhachHang["ContactBy"] != DBNull.Value)
                    _tuVanKhachHang.ContactBy = Guid.Parse(drTuVanKhachHang["ContactBy"].ToString());

                if (drTuVanKhachHang["UpdatedDate"] != null && drTuVanKhachHang["UpdatedDate"] != DBNull.Value)
                    _tuVanKhachHang.UpdatedDate = Convert.ToDateTime(drTuVanKhachHang["UpdatedDate"]);

                if (drTuVanKhachHang["UpdatedBy"] != null && drTuVanKhachHang["UpdatedBy"] != DBNull.Value)
                    _tuVanKhachHang.UpdatedBy = Guid.Parse(drTuVanKhachHang["UpdatedBy"].ToString());

                if (drTuVanKhachHang["DeletedDate"] != null && drTuVanKhachHang["DeletedDate"] != DBNull.Value)
                    _tuVanKhachHang.DeletedDate = Convert.ToDateTime(drTuVanKhachHang["DeletedDate"]);

                if (drTuVanKhachHang["DeletedBy"] != null && drTuVanKhachHang["DeletedBy"] != DBNull.Value)
                    _tuVanKhachHang.DeletedBy = Guid.Parse(drTuVanKhachHang["DeletedBy"].ToString());

                _tuVanKhachHang.Status = Convert.ToByte(drTuVanKhachHang["Status"]);

                string userGUID = drTuVanKhachHang["ContactBy"].ToString();
                if (userGUID != Global.UserGUID)
                {
                    groupBox1.Enabled = false;
                    btnOK.Enabled = false;
                    _isView = true;
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
            if (txtTenKhachHang.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên khách hàng.", IconType.Information);
                txtTenKhachHang.Focus();
                return false;
            }

            //string tuVanKhachHangGUID = string.Empty;
            //if (!_isNew) tuVanKhachHangGUID = _tuVanKhachHang.TuVanKhachHangGUID.ToString();

            //Result result = TuVanKhachHangBus.CheckKhachHangExist(txtTenKhachHang.Text, tuVanKhachHangGUID);
            //if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            //{
            //    if (result.Error.Code == ErrorCode.EXIST)
            //    {
            //        MsgBox.Show(this.Text, string.Format("Khách hàng: '{0}' đã được tư vấn trước rồi. Vui lòng cập nhật thông tin cho khách hàng này.", txtTenKhachHang.Text),
            //            IconType.Information);

            //        _isView = true;
            //        return false;
            //    }
            //}
            //else
            //{
            //    MsgBox.Show(this.Text, result.GetErrorAsString("TuVanKhachHangBus.CheckKhachHangExist"), IconType.Error);
            //    return false;
            //}

            //if (cboDocStaff.SelectedValue == null || cboDocStaff.Text.Trim() == string.Empty)
            //{
            //    MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ phụ trách.", IconType.Information);
            //    cboDocStaff.Focus();
            //    return false;
            //}

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
                _tuVanKhachHang.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _tuVanKhachHang.ContactDate = DateTime.Now;
                    _tuVanKhachHang.ContactBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _tuVanKhachHang.UpdatedDate = DateTime.Now;
                    _tuVanKhachHang.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                MethodInvoker method = delegate
                {
                    if (txtTenKhachHang.Tag != null)
                        _tuVanKhachHang.PatientGUID = Guid.Parse(txtTenKhachHang.Tag.ToString());
                    else
                        _tuVanKhachHang.PatientGUID = null;

                    _tuVanKhachHang.TenCongTy = txtTenCongTy.Text;
                    _tuVanKhachHang.MaKhachHang = txtMaKhachHang.Text;
                    _tuVanKhachHang.MucDich = txtMucDich.Text;
                    _tuVanKhachHang.TenKhachHang = txtTenKhachHang.Text;
                    _tuVanKhachHang.SoDienThoai = txtSoDienThoai.Text;
                    _tuVanKhachHang.DiaChi = txtDiaChi.Text;
                    _tuVanKhachHang.YeuCau = txtYeuCau.Text;
                    _tuVanKhachHang.Nguon = cboNguon.Text;
                    _tuVanKhachHang.Note = string.Empty;
                    _tuVanKhachHang.IsIN = raIN.Checked;
                    _tuVanKhachHang.SoTongDai = txtSoTongDai.Text;

                    if (cboDocStaff.SelectedValue != null && cboDocStaff.Text.Trim() != string.Empty)
                        _tuVanKhachHang.BacSiPhuTrachGUID = Guid.Parse(cboDocStaff.SelectedValue.ToString());
                    else
                        _tuVanKhachHang.BacSiPhuTrachGUID = null;

                    _tuVanKhachHang.DaXong = chkDaXong.Checked;
                    _tuVanKhachHang.BanThe = chkBanThe.Checked;
                    _tuVanKhachHang.KetLuan = txtHuongGiaiQuyet.Text;

                    Result result = TuVanKhachHangBus.InsertTuVanKhachHang(_tuVanKhachHang);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("TuVanKhachHangBus.InsertTuVanKhachHang"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("TuVanKhachHangBus.InsertTuVanKhachHang"));
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

        private void DisplayBacSiPhuTrach()
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
                newRow["DocStaffGUID"] = Guid.Empty.ToString();
                newRow["FullName"] = string.Empty;
                dt.Rows.InsertAt(newRow, 0);

                cboDocStaff.DataSource = dt;
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddYKienKhachHang_Load(object sender, EventArgs e)
        {
            OnDisplayNguonList();
            DisplayBacSiPhuTrach();
            if (!_isNew) DisplayInfo(_drTuVanKhachHang);
        }

        private void dlgAddYKienKhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else if (!_isView)
                    e.Cancel = true;
            }
            else if (!_isView)
            {
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu tư vấn khách hàng ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void btnChonBenhNhan_Click(object sender, EventArgs e)
        {
            dlgSelectPatient dlg = new dlgSelectPatient(PatientSearchType.BenhNhan);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                DataRow patientRow = dlg.PatientRow;
                if (patientRow != null)
                {
                    txtTenKhachHang.Tag = patientRow["PatientGUID"].ToString();
                    txtTenKhachHang.Text = patientRow["FullName"].ToString();
                    txtSoDienThoai.Text = patientRow["Mobile"].ToString();
                    txtDiaChi.Text = patientRow["Address"].ToString();
                    txtMaKhachHang.Text = patientRow["FileNum"].ToString();
                }
            }
        }

        private void raIN_CheckedChanged(object sender, EventArgs e)
        {
            if (raIN.Checked)
                txtSoTongDai.Text = "19001856";
            else
                txtSoTongDai.Text = string.Empty;
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
