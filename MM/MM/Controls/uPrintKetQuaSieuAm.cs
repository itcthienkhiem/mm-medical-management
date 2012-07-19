using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Controls
{
    public partial class uPrintKetQuaSieuAm : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        private DataRow _drKetQuaSieuAm = null;
        private string _reportTemplate = string.Format("{0}\\Templates\\SieuAmTemplate.rtf", Application.StartupPath);
        #endregion

        #region Constructor
        public uPrintKetQuaSieuAm()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public DataRow PatientRow
        {
            get { return _patientRow; }
            set { _patientRow = value; }
        }

        public DataRow DrKetQuaSieuAm
        {
            get { return _drKetQuaSieuAm; }
            set { _drKetQuaSieuAm = value; }
        }
        #endregion

        #region UI Command
        public void DisplayInfo()
        {
            Cursor.Current = Cursors.WaitCursor;
            _textControl.Load(_reportTemplate, TXTextControl.StreamType.RichTextFormat);
            _textControl.Tables.GridLines = false;


        }
        #endregion

        #region Window Event Handlers
        private void tbPrint_Click(object sender, EventArgs e)
        {
            _textControl.Print("Kết quả siêu âm");
        }
        #endregion
    }
}
