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
    public partial class dlgAddPhieuThuHopDong : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private PhieuThuHopDong _phieuThuHopDong = new PhieuThuHopDong();
        private DataRow _drPhieuThu = null;
        private bool _isExportedInvoice = false;
        private string _hopDongGUID = string.Empty;
        #endregion

        #region Constructor
        public dlgAddPhieuThuHopDong()
        {
            InitializeComponent();
            GenerateCode();
        }
        #endregion

        #region Properties
        public PhieuThuHopDong PhieuThuHopDong
        {
            get { return _phieuThuHopDong; }
        }

        public bool IsExportedInvoice
        {
            get { return _isExportedInvoice; }
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            if (_isNew)
                btnExportInvoice.Enabled = false;
            else
            {
                bool isExportedInvoice = Convert.ToBoolean(_drPhieuThu["IsExported"]);
                btnExportInvoice.Enabled = Global.AllowExportInvoice && !isExportedInvoice;
            }
        }

        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = PhieuThuHopDongBus.GetPhieuThuHopDongCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                txtMaPhieuThu.Text = Utility.GetCode("PTHD", count + 1, 7);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("PhieuThuHopDongBus.GetPhieuThuHopDongCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuHopDongBus.GetPhieuThuHopDongCount"));
            }
        }

        private void InitData()
        {
            dtpkNgayThu.Value = DateTime.Now;
            OnDisplayHopDongList();
        }

        private void OnDisplayHopDongList()
        {
            Result result = CompanyContractBus.GetContractList();
            if (result.IsOK)
                cboMaHopDong.DataSource = result.QueryResult as DataTable;
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetContractList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetContractList"));
            }
        }

        private DataRow GetHopDong()
        {
            DataTable dt = cboMaHopDong.DataSource as DataTable;
            if (dt == null) return null;
            DataRow[] rows = dt.Select(string.Format("CompanyContractGUID='{0}'", _hopDongGUID));
            if (rows != null && rows.Length > 0)
                return rows[0];

            return null;
        }

        private void DisplayCongNoHopDong()
        {
            if (_hopDongGUID == string.Empty) return;

            Result result = PhieuThuHopDongBus.GetCongNoTheoHopDong(_hopDongGUID);
            if (result.IsOK)
            {
                double congNo = Convert.ToDouble(result.QueryResult);
                numCongNo.Value = (Decimal)congNo;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("PhieuThuHopDongBus.GetCongNoTheoHopDong"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PhieuThuHopDongBus.GetCongNoTheoHopDong"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddPhieuThuHopDong_Load(object sender, EventArgs e)
        {
            InitData();
            UpdateGUI();
        }

        private void cboMaHopDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaHopDong.SelectedValue == null || cboMaHopDong.SelectedValue == DBNull.Value) return;

            _hopDongGUID = cboMaHopDong.SelectedValue.ToString();

            DataRow row = GetHopDong();
            if (row != null)
                txtTenCongTy.Text = row["TenCty"].ToString();
            else
                txtTenCongTy.Text = string.Empty;

            DisplayCongNoHopDong();
        }

        private void btnChonHopDong_Click(object sender, EventArgs e)
        {

        }

        private void btnExportInvoice_Click(object sender, EventArgs e)
        {

        }

        private void dlgAddPhieuThuHopDong_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        #endregion

        
    }
}
