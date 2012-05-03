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
            get { return cboXetNghiem.SelectedValue.ToString(); }
        }

        public string TenXetNghiem
        {
            get { return cboXetNghiem.Text; }
        }

        public string TestResult
        {
            get { return txtResult.Text; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            Result result = XetNghiemTayBus.GetXetNghiemList();
            if (result.IsOK)
                cboXetNghiem.DataSource = result.QueryResult as DataTable;
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiemTayBus.GetXetNghiemList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiemTayBus.GetXetNghiemList"));
            }
        }

        private void DisplayInfo()
        {
            try
            {
                cboXetNghiem.SelectedValue = _drChiTietKQXN["XetNghiem_ManualGUID"].ToString();
                txtResult.Text = _drChiTietKQXN["TestResult"].ToString();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (cboXetNghiem.Text == string.Empty)
            {
                MsgBox.Show(this.TestResult, "Vui lòng chọn 1 xét nghiệm.", IconType.Information);
                cboXetNghiem.Focus();
                return false;
            }

            if (txtResult.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.TestResult, "Vui lòng nhập kết quả.", IconType.Information);
                txtResult.Focus();
                return false;
            }

            if (_dtChiTietKQXN != null)
            {
                foreach (DataRow row in _dtChiTietKQXN.Rows)
                {
                    if (row.RowState == DataRowState.Deleted || row.RowState == DataRowState.Detached) continue;

                    if (_isNew)
                    {
                        if (row["XetNghiem_ManualGUID"].ToString() == cboXetNghiem.SelectedValue.ToString())
                        {
                            MsgBox.Show(this.Text, string.Format("Xét nghiệm: '{0}' đã nhập rồi. Vui lòng chọn xét nghiệm khác.", row["Fullname"].ToString()), IconType.Information);
                            cboXetNghiem.Focus();
                            return false;
                        }
                    }
                    else if (row["ChiTietKetQuaXetNghiem_ManualGUID"].ToString() != _drChiTietKQXN["ChiTietKetQuaXetNghiem_ManualGUID"].ToString())
                    {
                        if (row["XetNghiem_ManualGUID"].ToString() == cboXetNghiem.SelectedValue.ToString())
                        {
                            MsgBox.Show(this.Text, string.Format("Xét nghiệm: '{0}' đã nhập rồi. Vui lòng chọn xét nghiệm khác.", row["Fullname"].ToString()), IconType.Information);
                            cboXetNghiem.Focus();
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void btnChonXetNghiem_Click(object sender, EventArgs e)
        {
            DataTable dataSource = cboXetNghiem.DataSource as DataTable;
            dlgSelectXetNghiemTay dlg = new dlgSelectXetNghiemTay(dataSource);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                cboXetNghiem.SelectedValue = dlg.XetNghiem_ManualGUID;
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
