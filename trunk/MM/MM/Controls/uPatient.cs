using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Dialogs;
using MM.Common;

namespace MM.Controls
{
    public partial class uPatient : UserControl
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
            txtDOB.Text = row["DobStr"].ToString();
            txtAge.Text = Utility.GetAge(txtDOB.Text).ToString();
            txtIdentityCard.Text = row["IdentityCard"].ToString();
            txtHomePhone.Text = row["HomePhone"].ToString();
            txtWorkPhone.Text = row["WorkPhone"].ToString();
            txtMobile.Text = row["Mobile"].ToString();
            txtEmail.Text = row["Email"].ToString();
            txtFullAddress.Text = row["Address"].ToString();
            txtThuocDiUng.Text = row["Thuoc_Di_Ung"].ToString();

            _uServiceHistory.DisplayAsThread();
            _uDailyServiceHistory.DisplayAsThread();
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
        }

        private void txtThuocDiUng_DoubleClick(object sender, EventArgs e)
        {
            dlgPatientHistory dlg = new dlgPatientHistory((DataRow)_patientRow);
            dlg.ShowDialog(this);
        }
        #endregion
    }
}
