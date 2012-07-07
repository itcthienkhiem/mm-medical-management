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
        #region Members
        private DataTable _dataSource = null;
        private bool _isBenhNhanThanThuoc = false;
        #endregion

        #region Constructor
        public dlgSelectPatient(DataTable dataSource)
        {
            InitializeComponent();
            _dataSource = dataSource;
            _uSearchPatient.DataSource = dataSource;
            _uSearchPatient.OnOpenPatient += new MM.Controls.OpenPatientHandler(_uSearchPatient_OnOpenPatient);
        }

        public dlgSelectPatient()
        {
            InitializeComponent();
            OnDisplayBenhNhan();
            _uSearchPatient.OnOpenPatient += new MM.Controls.OpenPatientHandler(_uSearchPatient_OnOpenPatient);
        }

        public dlgSelectPatient(bool isBenhNhanThanThuoc)
        {
            InitializeComponent();
            _isBenhNhanThanThuoc = isBenhNhanThanThuoc;
            this.IsMulti = true;
            OnDisplayBenhNhan();
        }
        #endregion

        #region Properties
        public DataTable DataSource
        {
            get { return _dataSource; }
            set 
            {
                _dataSource = value;
                if (_dataSource == null)
                    OnDisplayBenhNhan();
            }
        }

        public DataRow PatientRow
        {
            get { return (DataRow)_uSearchPatient.PatientRow; }
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

        #region UI Command
        private void OnDisplayBenhNhan()
        {
            Cursor.Current = Cursors.WaitCursor;
            
            if (!_isBenhNhanThanThuoc)
            {
                Result result = PatientBus.GetPatientList();

                if (result.IsOK)
                {
                    _dataSource = result.QueryResult as DataTable;
                    _uSearchPatient.DataSource = _dataSource;
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("PatientBus.GetPatientList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientList"));
                }
            }
            else
            {
                Result result = PatientBus.GetBenhNhanKhongThanThuocList();

                if (result.IsOK)
                {
                    _dataSource = result.QueryResult as DataTable;
                    _uSearchPatient.DataSource = _dataSource;
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("PatientBus.GetBenhNhanKhongThanThuocList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetBenhNhanKhongThanThuocList"));
                }
            }
        }
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        
    }
}
