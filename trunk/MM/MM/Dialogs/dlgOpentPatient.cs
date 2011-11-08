using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Bussiness;
using MM.Common;
using MM.Controls;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgOpentPatient : Form
    {
        #region Members
        private object _patientRow = null;
        #endregion

        #region Constructor
        public dlgOpentPatient()
        {
            InitializeComponent();
            _uSearchPatient.OnOpenPatient += new OpenPatientHandler(_uSearchPatient_OnOpenPatient);
        }
        #endregion

        #region Properties
        public object DataSource
        {
            get { return _uSearchPatient.DataSource; }
            set { _uSearchPatient.DataSource = value; }
        }

        public object PatientRow
        {
            get { return _patientRow; }
            set { _patientRow = value; }
        }
        #endregion

        #region UI Command
        
        #endregion

        #region Window Event Handlers
        private void btnOpenPatient_Click(object sender, EventArgs e)
        {
            _patientRow = _uSearchPatient.PatientRow;
            if (_patientRow != null)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void _uSearchPatient_OnOpenPatient(object patientRow)
        {
            _patientRow = patientRow;
            if (_patientRow != null)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void dlgOpentPatient_Load(object sender, EventArgs e)
        {
        }
        #endregion
    }
}
