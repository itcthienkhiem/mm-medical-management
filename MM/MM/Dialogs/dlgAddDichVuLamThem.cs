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
    public partial class dlgAddDichVuLamThem : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private DichVuLamThem _dichVuLamThem = new DichVuLamThem();
        private DataRow _drDichVuLamThem = null;
        private string _contractMemberGUID = string.Empty;
        #endregion

        #region Constructor
        public dlgAddDichVuLamThem(string contractMemberGUID)
        {
            InitializeComponent();
            _contractMemberGUID = contractMemberGUID;
        }

        public dlgAddDichVuLamThem(string contractMemberGUID, DataRow drDichVuLamThem)
        {
            InitializeComponent();
            _contractMemberGUID = contractMemberGUID;
            _isNew = false;
            this.Text = "Sua dich vu lam them";
            _drDichVuLamThem = drDichVuLamThem;
        }
        #endregion

        #region Properties
        public DichVuLamThem DichVuLamThem
        {
            get { return _dichVuLamThem; }
            set { _dichVuLamThem = value; }
        }

        public string ServiceName
        {
            get { return cboService.Text; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
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
        }

        private void DisplayInfo()
        {
            try
            {
                cboService.SelectedValue = _drDichVuLamThem["ServiceGUID"].ToString();
                numPrice.Value = (decimal)Double.Parse(_drDichVuLamThem["FixedPrice"].ToString());
                numDiscount.Value = (decimal)Double.Parse(_drDichVuLamThem["Discount"].ToString());
                _dichVuLamThem.DichVuLamThemGUID = Guid.Parse(_drDichVuLamThem["DichVuLamThemGUID"].ToString());

                if (_drDichVuLamThem["ActiveDate"] != null && _drDichVuLamThem["ActiveDate"] != DBNull.Value)
                {
                    _dichVuLamThem.ActiveDate = Convert.ToDateTime(_drDichVuLamThem["ActiveDate"]);
                    dtpkActiveDate.Value = _dichVuLamThem.ActiveDate;
                }

                chkDaThuTien.Checked = Convert.ToBoolean(_drDichVuLamThem["DaThuTien"]);

                if (_drDichVuLamThem["CreatedDate"] != null && _drDichVuLamThem["CreatedDate"] != DBNull.Value)
                    _dichVuLamThem.CreatedDate = Convert.ToDateTime(_drDichVuLamThem["CreatedDate"]);

                if (_drDichVuLamThem["CreatedBy"] != null && _drDichVuLamThem["CreatedBy"] != DBNull.Value)
                    _dichVuLamThem.CreatedBy = Guid.Parse(_drDichVuLamThem["CreatedBy"].ToString());

                if (_drDichVuLamThem["UpdatedDate"] != null && _drDichVuLamThem["UpdatedDate"] != DBNull.Value)
                    _dichVuLamThem.UpdatedDate = Convert.ToDateTime(_drDichVuLamThem["UpdatedDate"]);

                if (_drDichVuLamThem["UpdatedBy"] != null && _drDichVuLamThem["UpdatedBy"] != DBNull.Value)
                    _dichVuLamThem.UpdatedBy = Guid.Parse(_drDichVuLamThem["UpdatedBy"].ToString());

                if (_drDichVuLamThem["DeletedDate"] != null && _drDichVuLamThem["DeletedDate"] != DBNull.Value)
                    _dichVuLamThem.DeletedDate = Convert.ToDateTime(_drDichVuLamThem["DeletedDate"]);

                if (_drDichVuLamThem["DeletedBy"] != null && _drDichVuLamThem["DeletedBy"] != DBNull.Value)
                    _dichVuLamThem.DeletedBy = Guid.Parse(_drDichVuLamThem["DeletedBy"].ToString());

                _dichVuLamThem.Status = Convert.ToByte(_drDichVuLamThem["Status"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (cboService.SelectedValue == null || cboService.Text == null || cboService.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn dịch vụ.", IconType.Information);
                cboService.Focus();
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
                    _dichVuLamThem.CreatedDate = DateTime.Now;
                    _dichVuLamThem.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _dichVuLamThem.UpdatedDate = DateTime.Now;
                    _dichVuLamThem.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                _dichVuLamThem.ContractMemberGUID = Guid.Parse(_contractMemberGUID);

                MethodInvoker method = delegate
                {
                    _dichVuLamThem.ActiveDate = dtpkActiveDate.Value;
                    _dichVuLamThem.ServiceGUID = Guid.Parse(cboService.SelectedValue.ToString());
                    _dichVuLamThem.Price = (double)numPrice.Value;
                    _dichVuLamThem.Discount = (double)numDiscount.Value;
                    _dichVuLamThem.DaThuTien = chkDaThuTien.Checked;

                    Result result = DichVuLamThemBus.InsertDichVuLamThem(_dichVuLamThem);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("DichVuLamThemBus.InsertDichVuLamThem"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("DichVuLamThemBus.InsertDichVuLamThem"));
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
        private void dlgAddDichVuLamThem_Load(object sender, EventArgs e)
        {
            InitData();

            if (!_isNew) DisplayInfo();

            lbPrice.Visible = Global.AllowShowServiePrice;
            lbUnit.Visible = Global.AllowShowServiePrice;
            numPrice.Visible = Global.AllowShowServiePrice;
        }

        private void cboService_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboService.SelectedValue == null || cboService.SelectedValue.ToString() == string.Empty) return;
            DataTable dt = cboService.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;

            string serviceGUID = cboService.SelectedValue.ToString();
            DataRow[] rows = dt.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
            if (rows != null && rows.Length > 0)
            {
                numPrice.Value = (decimal)Double.Parse(rows[0]["Price"].ToString());
            }
        }

        private void dlgAddDichVuLamThem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
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
        #endregion
    }
}
