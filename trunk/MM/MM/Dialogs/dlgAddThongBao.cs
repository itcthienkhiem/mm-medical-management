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
using MM.Databasae;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgAddThongBao : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private ThongBao _thongBao = new ThongBao();
        private DataRow _drThongBao = null;
        #endregion

        #region Constructor
        public dlgAddThongBao()
        {
            InitializeComponent();
        }

        public dlgAddThongBao(DataRow drThongBao)
        {
            InitializeComponent();
            _drThongBao = drThongBao;
            _isNew = false;
            this.Text = "Sua thong bao";
        }
        #endregion

        #region Properties
        public ThongBao ThongBao
        {
            get { return _thongBao; }
        }
        #endregion

        #region UI Command
        private void DisplayInfo()
        {

        }

        private bool CheckInfo()
        {
            if (txtTenThongBao.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "", IconType.Information);
                txtTenThongBao.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddThongBao_Load(object sender, EventArgs e)
        {
            if (!_isNew) DisplayInfo();
        }

        private void dlgAddThongBao_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtTapTinThongBao.Text = dlg.FileName;
            }
        }
        #endregion
    }
}
