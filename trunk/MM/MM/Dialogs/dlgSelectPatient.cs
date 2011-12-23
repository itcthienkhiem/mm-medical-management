using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MM.Dialogs
{
    public partial class dlgSelectPatient : dlgBase
    {
        #region Members
        private DataTable _dataSource = null;
        #endregion

        #region Constructor
        public dlgSelectPatient(DataTable dataSource)
        {
            InitializeComponent();
            _dataSource = dataSource;
            _uSearchPatient.DataSource = dataSource;
            _uSearchPatient.OnOpenPatient += new MM.Controls.OpenPatientHandler(_uSearchPatient_OnOpenPatient);
        }
        #endregion

        #region Properties
        public DataTable DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        public DataRow PatientRow
        {
            get { return (DataRow)_uSearchPatient.PatientRow; }
        }
        #endregion

        #region UI Command

        #endregion

        #region Window Event Handlers
        private void _uSearchPatient_OnOpenPatient(object patientRow)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void dlgSelectPatient_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        #endregion
    }
}
