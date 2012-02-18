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
    public partial class dlgAddKetQuaNoiSoi : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private string _patientGUID = string.Empty;
        private KetQuaNoiSoi _ketQuaNoiSoi = new KetQuaNoiSoi();
        private DataRow _drKetQuaNoiSoi = null;
        #endregion

        #region Constructor
        public dlgAddKetQuaNoiSoi(string patientGUID)
        {
            InitializeComponent();
            _patientGUID = patientGUID;
        }

        public dlgAddKetQuaNoiSoi(string patientGUID, DataRow drKetQuaNoiSoi)
        {
            InitializeComponent();
            _patientGUID = patientGUID;
            _drKetQuaNoiSoi = drKetQuaNoiSoi;
            _isNew = false;
            this.Text = "Sua kham noi soi";
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command

        #endregion

        #region Window Event Handlers

        #endregion
    }
}
