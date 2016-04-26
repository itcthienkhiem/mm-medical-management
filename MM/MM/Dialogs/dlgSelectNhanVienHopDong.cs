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
    public partial class dlgSelectNhanVienHopDong : dlgBase
    {
        #region Members
        private DateTime _activedDate = DateTime.Now;
        private string _hopDongGUID = string.Empty;
        private string _serviceGUID = string.Empty;
        private string _patientGUID = string.Empty;
        #endregion

        #region Constructor
        public dlgSelectNhanVienHopDong(DateTime activedDate, string serviceGUID, string patientGUID)
        {
            InitializeComponent();
            _activedDate = activedDate;
            _serviceGUID = serviceGUID;
            _patientGUID = patientGUID;
            _uSearchPatient.ServiceGUID = _serviceGUID;
            _uSearchPatient.PatientGUID = _patientGUID;
            _uSearchPatient.PatientSearchType = PatientSearchType.NhanVienHopDong;
            _uSearchPatient.OnOpenPatientEvent += new MM.Controls.OpenPatientHandler(_uSearchPatient_OnOpenPatient);
        }
        #endregion

        #region Properties
        public DataRow PatientRow
        {
            get { return _uSearchPatient.PatientRow; }
        }

        public string HopDongGUID
        {
            get { return cboMaHopDong.SelectedValue.ToString(); }
        }
        #endregion

        #region UI Command
        private void DisplayHopDongByThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(DisplayHopDongProc));
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

        private void OnDisplayHopDong()
        {
            Result result = CompanyContractBus.GetHopDongByDate(_activedDate, _serviceGUID);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    cboMaHopDong.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyContractBus.GetHopDongByDate"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetHopDongByDate"));
            }
        }

        private string GetTenHopDong()
        {
            List<CompanyContractView> hopDongList = cboMaHopDong.DataSource as List<CompanyContractView>;
            if (hopDongList != null && hopDongList.Count > 0)
            {
                foreach (var hd in hopDongList)
                {
                    if (hd.CompanyContractGUID.ToString() == _hopDongGUID)
                        return hd.ContractName;
                }
            }

            return string.Empty;
        }
        #endregion

        #region Window Event Handles
        private void dlgSelectNhanVienHopDong_Load(object sender, EventArgs e)
        {
            DisplayHopDongByThread();
        }

        private void _uSearchPatient_OnOpenPatient(DataRow patientRow)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void cboMaHopDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaHopDong.SelectedValue == null || cboMaHopDong.SelectedValue == DBNull.Value) return;
            _hopDongGUID = cboMaHopDong.SelectedValue.ToString();
            _uSearchPatient.HopDongGUID = _hopDongGUID;

            txtTenHopDong.Text = GetTenHopDong();

            _uSearchPatient.SearchAsThread();
        }
        #endregion

        #region Working Thread
        private void DisplayHopDongProc(object state)
        {
            try
            {
                OnDisplayHopDong();
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
        #endregion

        

        
    }
}
