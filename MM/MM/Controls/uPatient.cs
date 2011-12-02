﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Dialogs;
using MM.Bussiness;
using MM.Databasae;
using MM.Common;

namespace MM.Controls
{
    public partial class uPatient : uBase
    {
        #region Members
        private object _patientRow = null;
        #endregion

        #region Constructor
        public uPatient()
        {
            InitializeComponent();
            _uServiceHistory.OnServiceHistoryChanged += new ServiceHistoryChangedHandler(_uServiceHistory_OnServiceHistoryChanged);
            _uDailyServiceHistory.OnServiceHistoryChanged += new ServiceHistoryChangedHandler(_uServiceHistory_OnServiceHistoryChanged);
        }
        #endregion

        #region Properties
        public object PatientRow
        {
            get { return _patientRow; }
            set 
            { 
                _patientRow = value;
                _uServiceHistory.PatientRow = value;
                _uDailyServiceHistory.PatientRow = value;
            }
        }
        #endregion

        #region UI Command
        public void DisplayInfo()
        {
            if (_patientRow == null) return;

            DataRow row = _patientRow as DataRow;

            txtFullName.Text = row["FullName"].ToString();
            txtGender.Text = row["GenderAsStr"].ToString();
            txtDOB.Text ="NS: " + row["DobStr"].ToString();
            txtAge.Text = Utility.GetAge(row["DobStr"].ToString()).ToString() + " tuổi";
            txtIdentityCard.Text ="CMND: " +  row["IdentityCard"].ToString();
            //txtHomePhone.Text = row["HomePhone"].ToString();
            txtWorkPhone.Text = "ĐT: "  + row["WorkPhone"].ToString();
            txtMobile.Text = "DĐ: " + row["Mobile"].ToString();
            txtEmail.Text ="Email: " + row["Email"].ToString();
            txtFullAddress.Text ="Địa chỉ: " + row["Address"].ToString();
            txtThuocDiUng.Text = row["Thuoc_Di_Ung"].ToString();

            _uServiceHistory.DisplayAsThread();
            _uDailyServiceHistory.DisplayAsThread();

            DisplayCheckListAsThread();
        }

        public void DisplayCheckListAsThread()
        {
            try
            {
                lvService.Items.Clear();
                string patientGUID = (_patientRow as DataRow)["PatientGUID"].ToString();
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayCheckListProc), patientGUID);
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

        private void OnDisplayCheckList(string patientGUID)
        {
            Result result = CompanyContractBus.GetCheckListByPatient(patientGUID);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    DataTable dt = result.QueryResult as DataTable;
                    lvService.Visible = dt.Rows.Count > 0 ? true : false;

                    foreach (DataRow row in dt.Rows)
                    {
                        string code = row["Code"].ToString();
                        string name = row["Name"].ToString();
                        bool isChecked = Convert.ToBoolean(row["Checked"]);
                        int imgIndex = isChecked ? 0 : 1;

                        ListViewItem item = new ListViewItem(string.Empty, imgIndex);
                        item.SubItems.Add(code);
                        item.SubItems.Add(name);
                        lvService.Items.Add(item);
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                lvService.Visible = false;
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyBus.GetCompanyMemberList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.GetCompanyMemberList"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void uPatient_Load(object sender, EventArgs e)
        {

        }

        private void _uServiceHistory_OnServiceHistoryChanged()
        {
            _uServiceHistory.DisplayAsThread();
            _uDailyServiceHistory.DisplayAsThread();
            DisplayCheckListAsThread();
        }

        private void txtThuocDiUng_DoubleClick(object sender, EventArgs e)
        {
            dlgPatientHistory dlg = new dlgPatientHistory((DataRow)_patientRow);
            dlg.ShowDialog(this);
        }
        #endregion

        #region Working Thread
        private void OnDisplayCheckListProc(object state)
        {
            try
            {
                OnDisplayCheckList(state.ToString());
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
