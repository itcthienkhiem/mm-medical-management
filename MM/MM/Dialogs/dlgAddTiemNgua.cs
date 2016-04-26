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
using MM.Databasae;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgAddTiemNgua : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private TiemNgua _tiemNgua = new TiemNgua();
        private DataRow _drTiemNgua = null;
        #endregion

        #region Constructor
        public dlgAddTiemNgua()
        {
            InitializeComponent();

        }

        public dlgAddTiemNgua(DataRow drTiemNgua)
        {
            InitializeComponent();
            _isNew = false;
            _drTiemNgua = drTiemNgua;
            this.Text = "Sua tiem ngua";
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void DisplayInfo()
        {
            try
            {

                _tiemNgua.TiemNguaGUID = Guid.Parse(_drTiemNgua["TiemNguaGUID"].ToString());
                _tiemNgua.PatientGUID = Guid.Parse(_drTiemNgua["PatientGUID"].ToString());
                
                txtBenhNhan.Text = _drTiemNgua["FullName"].ToString();

                if (_drTiemNgua["Lan1"] != null && _drTiemNgua["Lan1"] != DBNull.Value)
                {
                    chkLan1.Checked = true;
                    dtpkLan1.Value = Convert.ToDateTime(_drTiemNgua["Lan1"]);

                    if (_drTiemNgua["DaChich1"] != null && _drTiemNgua["DaChich1"] != DBNull.Value)
                        chkDaChich1.Checked = Convert.ToBoolean(_drTiemNgua["DaChich1"]);
                }

                if (_drTiemNgua["Lan2"] != null && _drTiemNgua["Lan2"] != DBNull.Value)
                {
                    chkLan2.Checked = true;
                    dtpkLan2.Value = Convert.ToDateTime(_drTiemNgua["Lan2"]);

                    if (_drTiemNgua["DaChich2"] != null && _drTiemNgua["DaChich2"] != DBNull.Value)
                        chkDaChich2.Checked = Convert.ToBoolean(_drTiemNgua["DaChich2"]);
                }

                if (_drTiemNgua["Lan3"] != null && _drTiemNgua["Lan3"] != DBNull.Value)
                {
                    chkLan3.Checked = true;
                    dtpkLan3.Value = Convert.ToDateTime(_drTiemNgua["Lan3"]);

                    if (_drTiemNgua["DaChich3"] != null && _drTiemNgua["DaChich3"] != DBNull.Value)
                        chkDaChich3.Checked = Convert.ToBoolean(_drTiemNgua["DaChich3"]);
                }

                if (_drTiemNgua["CreatedDate"] != null && _drTiemNgua["CreatedDate"] != DBNull.Value)
                    _tiemNgua.CreatedDate = Convert.ToDateTime(_drTiemNgua["CreatedDate"]);

                if (_drTiemNgua["CreatedBy"] != null && _drTiemNgua["CreatedBy"] != DBNull.Value)
                    _tiemNgua.CreatedBy = Guid.Parse(_drTiemNgua["CreatedBy"].ToString());

                if (_drTiemNgua["UpdatedDate"] != null && _drTiemNgua["UpdatedDate"] != DBNull.Value)
                    _tiemNgua.UpdatedDate = Convert.ToDateTime(_drTiemNgua["UpdatedDate"]);

                if (_drTiemNgua["UpdatedBy"] != null && _drTiemNgua["UpdatedBy"] != DBNull.Value)
                    _tiemNgua.UpdatedBy = Guid.Parse(_drTiemNgua["UpdatedBy"].ToString());

                if (_drTiemNgua["DeletedDate"] != null && _drTiemNgua["DeletedDate"] != DBNull.Value)
                    _tiemNgua.DeletedDate = Convert.ToDateTime(_drTiemNgua["DeletedDate"]);

                if (_drTiemNgua["DeletedBy"] != null && _drTiemNgua["DeletedBy"] != DBNull.Value)
                    _tiemNgua.DeletedBy = Guid.Parse(_drTiemNgua["DeletedBy"].ToString());

                _tiemNgua.Status = Convert.ToByte(_drTiemNgua["Status"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (txtBenhNhan.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bệnh nhân.", IconType.Information);
                btnChonBenhNhan.Focus();
                return false;
            }

            if (!chkLan1.Checked && !chkLan2.Checked && !chkLan3.Checked)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập ít nhất 1 lần tiêm ngừa.", IconType.Information);
                chkLan1.Focus();
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
                    _tiemNgua.CreatedDate = DateTime.Now;
                    _tiemNgua.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _tiemNgua.UpdatedDate = DateTime.Now;
                    _tiemNgua.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                MethodInvoker method = delegate
                {
                    if (chkLan1.Checked)
                    {
                        _tiemNgua.Lan1 = dtpkLan1.Value;
                        _tiemNgua.DaChich1 = chkDaChich1.Checked;
                    }

                    if (chkLan2.Checked)
                    {
                        _tiemNgua.Lan2 = dtpkLan2.Value;
                        _tiemNgua.DaChich2 = chkDaChich2.Checked;
                    }

                    if (chkLan3.Checked)
                    {
                        _tiemNgua.Lan3 = dtpkLan3.Value;
                        _tiemNgua.DaChich3 = chkDaChich3.Checked;
                    }

                    Result result = TiemNguaBus.InsertTiemNgua(_tiemNgua);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("TiemNguaBus.InsertTiemNgua"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("TiemNguaBus.InsertTiemNgua"));
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
        private void dlgAddTiemNgua_Load(object sender, EventArgs e)
        {
            dtpkLan1.Value = DateTime.Now;
            dtpkLan2.Value = DateTime.Now;
            dtpkLan3.Value = DateTime.Now;

            if (!_isNew) DisplayInfo();
        }

        private void dlgAddTiemNgua_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
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
                    _tiemNgua.PatientGUID = Guid.Parse(patientRow["PatientGUID"].ToString());
                    txtBenhNhan.Text = patientRow["FullName"].ToString();
                }
            }
        }

        private void chkLan1_CheckedChanged(object sender, EventArgs e)
        {
            dtpkLan1.Enabled = chkLan1.Checked;
            chkDaChich1.Enabled = chkLan1.Checked;

            if (chkLan1.Checked) chkLan2.Enabled = true;
            else
            {
                chkLan2.Checked = false;
                chkLan2.Enabled = false;
            }
        }

        private void chkLan2_CheckedChanged(object sender, EventArgs e)
        {
            dtpkLan2.Enabled = chkLan2.Checked;
            chkDaChich2.Enabled = chkLan2.Checked;

            if (chkLan2.Checked) chkLan3.Enabled = true;
            else
            {
                chkLan3.Checked = false;
                chkLan3.Enabled = false;
            }
        }

        private void chkLan3_CheckedChanged(object sender, EventArgs e)
        {
            dtpkLan3.Enabled = chkLan3.Checked;
            chkDaChich3.Enabled = chkLan3.Checked;
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
