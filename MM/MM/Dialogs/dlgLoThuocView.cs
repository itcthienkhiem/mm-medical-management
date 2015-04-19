using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Databasae;
using MM.Bussiness;
using MM.Common;

namespace MM.Dialogs
{
    public partial class dlgLoThuocView : Form
    {
        #region Members
        private string _thuocGUID = string.Empty;
        #endregion

        #region Constructor
        public dlgLoThuocView(string thuocGUID)
        {
            InitializeComponent();
            _thuocGUID = thuocGUID;
        }
        #endregion

        #region UI Command
        private void DisplayInfo()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = LoThuocBus.GetLoThuocByThuoc(_thuocGUID);
            if (result.IsOK)
            {
                dgThuocTonKho.DataSource = result.QueryResult;
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("LoThuocBus.GetLoThuocByThuoc"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("LoThuocBus.GetLoThuocByThuoc"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgLoThuocView_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        }
        #endregion
    }
}
