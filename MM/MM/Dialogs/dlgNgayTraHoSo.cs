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
    public partial class dlgNgayTraHoSo : Form
    {
        #region Members

        #endregion

        #region Constructor
        public dlgNgayTraHoSo()
        {
            InitializeComponent();
            dtpkNgayTra.Value = DateTime.Now;
        }
        #endregion

        #region Properties
        public DateTime NgayTra
        {
            get { return dtpkNgayTra.Value; }
            set { dtpkNgayTra.Value = value; }
        }
        #endregion

        #region UI Command

        #endregion

        #region Window Event Handlers
        private void dlgNgayTraHoSo_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        #endregion
        
    }
}
