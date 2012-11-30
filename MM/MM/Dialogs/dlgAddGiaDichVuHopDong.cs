using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Databasae;
using MM.Common;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgAddGiaDichVuHopDong : dlgBase
    {
        #region Members
        private DataTable _dataSource = null;
        private bool _isNew = true;
        private DataRow _drGiaDichVu = null;
        #endregion

        #region Constructor
        public dlgAddGiaDichVuHopDong(DataTable dataSource)
        {
            InitializeComponent();
            _dataSource = dataSource;
        }

        public dlgAddGiaDichVuHopDong(DataTable dataSource, DataRow drGiaDichVu)
        {
            InitializeComponent();
            _dataSource = dataSource;
            _drGiaDichVu = drGiaDichVu;
            _isNew = false;
            this.Text = "Sua dich vu hop dong";
        }
        #endregion

        #region Properties
        public DataTable DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        public string ServiceGUID
        {
            get { return cboService.SelectedValue.ToString(); }
        }

        public double Gia
        {
            get { return (double)numPrice.Value; }
        }

        public string MaDichVu
        {
            get
            {
                DataTable dt = cboService.DataSource as DataTable;
                if (dt == null || dt.Rows.Count <= 0) return string.Empty;
                string serviceGUID = cboService.SelectedValue.ToString();
                DataRow[] rows = dt.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
                if (rows != null && rows.Length > 0)
                    return rows[0]["Code"].ToString();

                return string.Empty;
            }
        }

        public string TenDichVu
        {
            get { return cboService.Text; }
        }
        #endregion

        #region UI Commnad
        private void InitData()
        {
            //Service
            Result result = ServicesBus.GetServicesList();
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ServicesBus.GetServicesList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.GetServicesList"));
                return;
            }
            else
            {
                cboService.DataSource = result.QueryResult;
            }
        }

        private void DisplayInfo(DataRow drGiaDichVu)
        {
            try
            {
                cboService.SelectedValue = drGiaDichVu["ServiceGUID"].ToString();
                numPrice.Value = (Decimal)Convert.ToDouble(drGiaDichVu["Gia"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (cboService.SelectedValue == null || cboService.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn dịch vụ.", IconType.Information);
                cboService.Focus();
                return false;
            }

            if (numPrice.Value == 0)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập giá cho dịch vụ.", IconType.Information);
                return false;
            }

            if (_dataSource != null)
            {
                if (_isNew)
                {
                    DataRow[] rows = _dataSource.Select(string.Format("ServiceGUID='{0}'", cboService.SelectedValue.ToString()));
                    if (rows != null && rows.Length > 0)
                    {
                        MsgBox.Show(this.Text, string.Format("Dịch vụ: '{0}' đã được thêm rồi. Vui lòng chọn dịch vụ khác.", cboService.Text), 
                            IconType.Information);
                        cboService.Focus();
                        return false;
                    }
                }
                else
                {
                    string giaDichVuHopDongGUID = _drGiaDichVu["GiaDichVuHopDongGUID"].ToString();
                    DataRow[] rows = _dataSource.Select(string.Format("ServiceGUID='{0}'", cboService.SelectedValue.ToString()));

                    if (rows != null && rows.Length > 0)
                    {
                        if (rows[0] != _drGiaDichVu)
                        {
                            MsgBox.Show(this.Text, string.Format("Dịch vụ: '{0}' đã được thêm rồi. Vui lòng chọn dịch vụ khác.", cboService.Text),
                            IconType.Information);
                            cboService.Focus();
                            return false;
                        }
                        
                    }
                }
            }

            return true;
        }
        #endregion

        #region Window Event Handler
        private void dlgAddGiaDichVuHopDong_Load(object sender, EventArgs e)
        {
            InitData();
            if (!_isNew) DisplayInfo(_drGiaDichVu);
        }

        private void dlgAddGiaDichVuHopDong_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
            }
        }

        private void btnChonDichVu_Click(object sender, EventArgs e)
        {
            DataTable dtService = cboService.DataSource as DataTable;
            dlgSelectSingleDichVu dlg = new dlgSelectSingleDichVu(dtService);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                cboService.SelectedValue = dlg.ServiceGUID;
            }
        }

        private void cboService_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (cboService.SelectedValue == null || cboService.SelectedValue.ToString() == string.Empty) return;
            //DataTable dt = cboService.DataSource as DataTable;
            //if (dt == null || dt.Rows.Count <= 0) return;

            //string serviceGUID = cboService.SelectedValue.ToString();
            //DataRow[] rows = dt.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
            //if (rows != null && rows.Length > 0)
            //    numPrice.Value = (decimal)Double.Parse(rows[0]["Price"].ToString());
        }
        #endregion

       
    }
}
