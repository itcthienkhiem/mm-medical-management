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
    public partial class dlgAddChiTietKetQuaXetNghiemTay : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private DataRow _drChiTietKQXN = null;
        private DataTable _dtChiTietKQXN = null;
        private string _tenXetNghiem = string.Empty;
        private string _nhomXetNghiem = string.Empty;
        private DataTable _dtXetNghiem = null;
        #endregion

        #region Constructor
        public dlgAddChiTietKetQuaXetNghiemTay(DataTable dtChiTietKQXN)
        {
            InitializeComponent();
            _dtChiTietKQXN = dtChiTietKQXN;
        }

        public dlgAddChiTietKetQuaXetNghiemTay(DataTable dtChiTietKQXN, DataRow drChiTietKQXN)
        {
            InitializeComponent();
            _dtChiTietKQXN = dtChiTietKQXN;
            _isNew = false;
            this.Text = "Sua chi tiet ket qua xet nghiem tay";
            _drChiTietKQXN = drChiTietKQXN;
        }
        #endregion

        #region Properties
        public string XetNghiem_ManualGUID
        {
            get 
            { 
                if (txtXetNghiem.Tag != null)
                    return txtXetNghiem.Tag.ToString();

                return string.Empty;
            }
        }

        public string TenXetNghiem
        {
            get { return _tenXetNghiem; }
        }

        public string NhomXetNghiem
        {
            get { return _nhomXetNghiem; }
        }

        public string TestResult
        {
            get { return txtResult.Text; }
        }

        public bool LamThem
        {
            get { return chkLamThem.Checked; }
        }

        public bool HasHutThuoc
        {
            get { return chkHutThuoc.Checked; }
        }

        public DateTime NgayXetNghiem
        {
            get { return dtpkNgayXetNghiem.Value; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            Result result = XetNghiemTayBus.GetXetNghiemList();
            if (result.IsOK)
                _dtXetNghiem = result.QueryResult as DataTable;
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiemTayBus.GetXetNghiemList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiemTayBus.GetXetNghiemList"));
            }

            dtpkNgayXetNghiem.Value = DateTime.Now;
        }

        private void DisplayInfo()
        {
            try
            {
                txtXetNghiem.Tag = _drChiTietKQXN["XetNghiem_ManualGUID"].ToString();
                _tenXetNghiem = _drChiTietKQXN["FullName"].ToString();
                _nhomXetNghiem = _drChiTietKQXN["GroupName"].ToString();
                txtXetNghiem.Text = string.Format("{0} ({1})", _tenXetNghiem, _nhomXetNghiem);
                dtpkNgayXetNghiem.Value = Convert.ToDateTime(_drChiTietKQXN["NgayXetNghiem"]);

                txtResult.Text = _drChiTietKQXN["TestResult"].ToString();
                chkLamThem.Checked = Convert.ToBoolean(_drChiTietKQXN["LamThem"]);

                if (_drChiTietKQXN["HasHutThuoc"] != null && _drChiTietKQXN["HasHutThuoc"] != DBNull.Value)
                    chkHutThuoc.Checked = Convert.ToBoolean(_drChiTietKQXN["HasHutThuoc"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (txtXetNghiem.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn 1 xét nghiệm.", IconType.Information);
                btnChonXetNghiem.Focus();
                return false;
            }

            if (txtResult.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập kết quả.", IconType.Information);
                txtResult.Focus();
                return false;
            }

            //if (_dtChiTietKQXN != null)
            //{
            //    foreach (DataRow row in _dtChiTietKQXN.Rows)
            //    {
            //        if (row.RowState == DataRowState.Deleted || row.RowState == DataRowState.Detached) continue;

            //        if (_isNew)
            //        {
            //            if (row["XetNghiem_ManualGUID"].ToString() == cboXetNghiem.SelectedValue.ToString())
            //            {
            //                MsgBox.Show(this.Text, string.Format("Xét nghiệm: '{0}' đã nhập rồi. Vui lòng chọn xét nghiệm khác.", row["Fullname"].ToString()), IconType.Information);
            //                cboXetNghiem.Focus();
            //                return false;
            //            }
            //        }
            //        else if (row["ChiTietKetQuaXetNghiem_ManualGUID"].ToString() != _drChiTietKQXN["ChiTietKetQuaXetNghiem_ManualGUID"].ToString())
            //        {
            //            if (row["XetNghiem_ManualGUID"].ToString() == cboXetNghiem.SelectedValue.ToString())
            //            {
            //                MsgBox.Show(this.Text, string.Format("Xét nghiệm: '{0}' đã nhập rồi. Vui lòng chọn xét nghiệm khác.", row["Fullname"].ToString()), IconType.Information);
            //                cboXetNghiem.Focus();
            //                return false;
            //            }
            //        }
            //    }
            //}

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void btnChonXetNghiem_Click(object sender, EventArgs e)
        {
            dlgSelectXetNghiemTay dlg = new dlgSelectXetNghiemTay(_dtXetNghiem);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataRow row = dlg.XetNghiemRow;
                txtXetNghiem.Tag = row["XetNghiem_ManualGUID"].ToString();
                _tenXetNghiem = row["FullName"].ToString();
                _nhomXetNghiem = row["GroupName"].ToString();
                txtXetNghiem.Text = string.Format("{0} ({1})", _tenXetNghiem, _nhomXetNghiem);                
            }
        }

        private void dlgAddChiTietKetQuaXetNghiemTay_Load(object sender, EventArgs e)
        {
            InitData();
            if (!_isNew) DisplayInfo();
        }

        private void dlgAddChiTietKetQuaXetNghiemTay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
            }
        }
        #endregion
    }
}
