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
    public partial class dlgUpdateChiSoKetQuaXetNghiem_CellDyn3200 : dlgBase
    {
        #region Members
        private DataRow _drCTKQXN = null;
        private ChiTietKetQuaXetNghiem_CellDyn3200 _chiTietKQXN = new ChiTietKetQuaXetNghiem_CellDyn3200();
        private string _binhThuong = string.Empty;
        private bool _isTongHop = false;
        #endregion

        #region Constructor
        public dlgUpdateChiSoKetQuaXetNghiem_CellDyn3200(DataRow drCTKQXN)
        {
            InitializeComponent();
            _drCTKQXN = drCTKQXN;
        }
        #endregion

        #region Properties
        public bool IsTongHop
        {
            get { return _isTongHop; }
            set { _isTongHop = value; }
        }

        public ChiTietKetQuaXetNghiem_CellDyn3200 ChiTietKQXN
        {
            get { return _chiTietKQXN; }
        }

        public string BinhThuong
        {
            get { return _binhThuong; }
        }
        #endregion

        #region UI Command
        private void DisplayInfo()
        {
            if (!_isTongHop)
                _chiTietKQXN.ChiTietKQXN_CellDyn3200GUID = Guid.Parse(_drCTKQXN["ChiTietKQXN_CellDyn3200GUID"].ToString());
            else
            {
                _chiTietKQXN.ChiTietKQXN_CellDyn3200GUID = Guid.Parse(_drCTKQXN["ChiTietKQXNGUID"].ToString());

                //if (_drCTKQXN["Fullname"] != null && _drCTKQXN["Fullname"] != DBNull.Value)
                //    txTenXetNghiem.Text = _drCTKQXN["Fullname"].ToString();

                //numKetQua.Value = (Decimal)Convert.ToDouble(_drCTKQXN["TestResult"].ToString().Trim());

                //if ((_drCTKQXN["FromValue2"] == null || _drCTKQXN["FromValue2"] == DBNull.Value) &&
                //    (_drCTKQXN["ToValue2"] == null || _drCTKQXN["ToValue2"] == DBNull.Value))
                //{
                //    chkFromValue_Normal.Enabled = false;
                //    chkToValue_Normal.Enabled = false;
                //}
                //else if (_drCTKQXN["ToValue2"] == null || _drCTKQXN["ToValue2"] == DBNull.Value)
                //{
                //    chkFromValue_Normal.Checked = true;
                //    numFromValue_Normal.Value = (Decimal)Convert.ToDouble(_drCTKQXN["FromValue2"]);
                //}
                //else if (_drCTKQXN["FromValue2"] == null || _drCTKQXN["FromValue2"] == DBNull.Value)
                //{
                //    chkToValue_Normal.Checked = true;
                //    numToValue_Normal.Value = (Decimal)Convert.ToDouble(_drCTKQXN["ToValue2"]);
                //}
                //else
                //{
                //    chkFromValue_Normal.Checked = true;
                //    numFromValue_Normal.Value = (Decimal)Convert.ToDouble(_drCTKQXN["FromValue2"]);
                //    chkToValue_Normal.Checked = true;
                //    numToValue_Normal.Value = (Decimal)Convert.ToDouble(_drCTKQXN["ToValue2"]);
                //}
                
                //if (_drCTKQXN["DonVi2"] != null && _drCTKQXN["DonVi2"] != DBNull.Value)
                //    txtDonVi.Text = _drCTKQXN["DonVi2"].ToString();

                //_chiTietKQXN.TenXetNghiem = _drCTKQXN["Fullname"].ToString();

                //chkLamThem.Checked = Convert.ToBoolean(_drCTKQXN["LamThem"]);
            }

            if (_drCTKQXN["Fullname"] != null && _drCTKQXN["Fullname"] != DBNull.Value)
                txTenXetNghiem.Text = _drCTKQXN["Fullname"].ToString();

            numKetQua.Value = (Decimal)Convert.ToDouble(_drCTKQXN["TestResult"].ToString().Trim());

            if ((_drCTKQXN["FromValue"] == null || _drCTKQXN["FromValue"] == DBNull.Value) &&
                (_drCTKQXN["ToValue"] == null || _drCTKQXN["ToValue"] == DBNull.Value))
            {
                chkFromValue_Normal.Enabled = false;
                chkToValue_Normal.Enabled = false;
            }
            else if (_drCTKQXN["ToValue"] == null || _drCTKQXN["ToValue"] == DBNull.Value)
            {
                chkFromValue_Normal.Checked = true;
                numFromValue_Normal.Value = (Decimal)Convert.ToDouble(_drCTKQXN["FromValue"]);
            }
            else if (_drCTKQXN["FromValue"] == null || _drCTKQXN["FromValue"] == DBNull.Value)
            {
                chkToValue_Normal.Checked = true;
                numToValue_Normal.Value = (Decimal)Convert.ToDouble(_drCTKQXN["ToValue"]);
            }
            else
            {
                chkFromValue_Normal.Checked = true;
                numFromValue_Normal.Value = (Decimal)Convert.ToDouble(_drCTKQXN["FromValue"]);
                chkToValue_Normal.Checked = true;
                numToValue_Normal.Value = (Decimal)Convert.ToDouble(_drCTKQXN["ToValue"]);
            }

            if (_drCTKQXN["DonVi"] != null && _drCTKQXN["DonVi"] != DBNull.Value)
                txtDonVi.Text = _drCTKQXN["DonVi"].ToString();

            _chiTietKQXN.TenXetNghiem = _drCTKQXN["Fullname"].ToString();

            chkLamThem.Checked = Convert.ToBoolean(_drCTKQXN["LamThem"]);
        }

        private bool CheckInfo()
        {
            if (chkFromValue_Normal.Enabled == true && chkToValue_Normal.Enabled == true)
            {
                if (!chkFromValue_Normal.Checked && !chkToValue_Normal.Checked)
                {
                    MsgBox.Show(this.Text, "Vui lòng nhập ngưỡng chỉ số.", IconType.Information);
                    return false;
                }
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
                MethodInvoker method = delegate
                {
                    _chiTietKQXN.TestResult = (double)numKetQua.Value;
                    
                    if (chkFromValue_Normal.Checked)
                        _chiTietKQXN.FromValue = (double)numFromValue_Normal.Value;

                    if (chkToValue_Normal.Checked)
                        _chiTietKQXN.ToValue = (double)numToValue_Normal.Value;

                    
                    _chiTietKQXN.DonVi = txtDonVi.Text;

                    _chiTietKQXN.LamThem = chkLamThem.Checked;

                    Result result = XetNghiem_CellDyn3200Bus.UpdateChiSoKetQuaXetNghiem(_chiTietKQXN, ref _binhThuong);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("XetNghiem_CellDyn3200Bus.UpdateChiSoKetQuaXetNghiem"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_CellDyn3200Bus.UpdateChiSoKetQuaXetNghiem"));
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
        private void dlgUpdateChiSoKetQuaXetNghiem_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        }

        private void dlgUpdateChiSoKetQuaXetNghiem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
                else SaveInfoAsThread();
            }
        }

        private void chkFromValue_Normal_CheckedChanged(object sender, EventArgs e)
        {
            numFromValue_Normal.Enabled = chkFromValue_Normal.Checked;
        }

        private void chkToValue_Normal_CheckedChanged(object sender, EventArgs e)
        {
            numToValue_Normal.Enabled = chkToValue_Normal.Checked;
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
