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
    public partial class dlgDanhSachNhanVien : dlgBase
    {
        #region Members
        private string _contractGUID = string.Empty;
        private int _type = 0; //0: Chua kham; 1: Kham chua du; 2: Kham day du
        #endregion

        #region Constructor
        public dlgDanhSachNhanVien(string contractGUID, int type)
        {
            InitializeComponent();
            _contractGUID = contractGUID;
            _type = type;
        }
        #endregion

        #region UI Command
        private void DisplayInfo()
        {
            Cursor.Current = Cursors.WaitCursor;

        }
        #endregion

        #region Window Event Handlers
        private void dlgDanhSachNhanVien_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        }
        #endregion
    }
}
