using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;

namespace MM.Dialogs
{
    public partial class dlgGhiNhanTraNo : Form
    {
        #region Members
        private LoaiPT _loaiPT = LoaiPT.DichVu;
        private string _phieuThuGUID = string.Empty;
        private bool _daThuTien = true;
        #endregion

        #region Constructor
        public dlgGhiNhanTraNo(LoaiPT loaiPT, string phieuThuGUID, bool daThuTien)
        {
            InitializeComponent();
            _loaiPT = loaiPT;
            _phieuThuGUID = phieuThuGUID;
            _daThuTien = daThuTien;
            _uGhiNhanTraNoList.LoaiPT = _loaiPT;
            _uGhiNhanTraNoList.PhieuThuGUID = _phieuThuGUID;
            _uGhiNhanTraNoList.DaThuTien = _daThuTien;
            _uGhiNhanTraNoList.OnCloseEvent += new MM.Controls.CloseClickEventHandler(_uGhiNhanTraNoList_OnCloseEvent);
        }
        #endregion

        #region Properties
        public bool IsDataChange
        {
            get { return _uGhiNhanTraNoList.IsDataChange; }
            set { _uGhiNhanTraNoList.IsDataChange = value; }
        }
        #endregion

        #region UI Command

        #endregion

        #region Window Event Handlers
        private void dlgGhiNhanTraNo_Load(object sender, EventArgs e)
        {
            _uGhiNhanTraNoList.DisplayAsThread();
        }

        private void _uGhiNhanTraNoList_OnCloseEvent()
        {
            this.Close();
        }
        #endregion

        
    }
}
