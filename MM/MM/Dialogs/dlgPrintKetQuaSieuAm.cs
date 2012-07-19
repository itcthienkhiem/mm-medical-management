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
    public partial class dlgPrintKetQuaSieuAm : dlgBase
    {
        #region Members
        private DataRow _drKetQuaSieuAm = null;
        #endregion

        #region Constructor
        public dlgPrintKetQuaSieuAm(DataRow patientRow, DataRow drKetQuaSieuAm)
        {
            InitializeComponent();

            _uPrintKetQuaSieuAm.PatientRow = patientRow;
            _drKetQuaSieuAm = drKetQuaSieuAm;
        }
        #endregion

        #region UI Command

        #endregion

        #region Window Event Handlers
        private void dlgPrintKetQuaSieuAm_Load(object sender, EventArgs e)
        {
            _uPrintKetQuaSieuAm.PrintPreview(_drKetQuaSieuAm);
        }
        #endregion
    }
}
