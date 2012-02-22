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
    public partial class dlgChonHinh : dlgBase
    {
        #region Constructor
        public dlgChonHinh()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public int ImageIndex
        {
            get
            {
                if (raHinh1.Checked) return 1;
                if (raHinh2.Checked) return 2;
                if (raHinh3.Checked) return 3;
                return 4;
            }
        }
        #endregion
    }
}
