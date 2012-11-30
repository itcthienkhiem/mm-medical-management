using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgSelectPatient : dlgBase
    {
        #region Constructor
        public dlgSelectPatient(PatientSearchType patientSearchType)
        {
            InitializeComponent();
            _uSearchPatient.PatientSearchType = patientSearchType;
            if (patientSearchType == PatientSearchType.BenhNhanKhongThanThuoc) 
                this.IsMulti = true;
            else
                _uSearchPatient.OnOpenPatientEvent += new MM.Controls.OpenPatientHandler(_uSearchPatient_OnOpenPatient);
        }
        #endregion

        #region Properties
        public DataRow PatientRow
        {
            get { return _uSearchPatient.PatientRow; }
        }

        public bool IsMulti
        {
            get { return _uSearchPatient.IsMulti; }
            set { _uSearchPatient.IsMulti = value; }
        }

        public List<DataRow> CheckedPatientRows
        {
            get { return _uSearchPatient.CheckedPatientRows; }
        }
        #endregion

        #region Window Event Handlers
        private void _uSearchPatient_OnOpenPatient(object patientRow)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        
    }
}
