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
        private CompanyInfo _companyInfo = null;
        private List<string> _deletedDichVuCons = new List<string>();
        #endregion

        #region Constructor
        public dlgAddGiaDichVuHopDong(CompanyInfo companyInfo)
        {
            InitializeComponent();
            _companyInfo = companyInfo;
            _dataSource = companyInfo.GiaDichVuDataSource;
        }

        public dlgAddGiaDichVuHopDong(CompanyInfo companyInfo, DataRow drGiaDichVu)
        {
            InitializeComponent();
            _companyInfo = companyInfo;
            _dataSource = companyInfo.GiaDichVuDataSource;
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
                cboService.DataSource = result.QueryResult;

            //Dich Vụ Con
            result = CompanyContractBus.GetDichVuCon(Guid.Empty.ToString());
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetDichVuCon"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetDichVuCon"));
                return;
            }
            else
                dgDichVuCon.DataSource = result.QueryResult;
        }

        private void DisplayInfo(DataRow drGiaDichVu)
        {
            try
            {
                string serviceGUID = drGiaDichVu["ServiceGUID"].ToString();
                cboService.SelectedValue = serviceGUID;
                numPrice.Value = (Decimal)Convert.ToDouble(drGiaDichVu["Gia"]);

                if (_companyInfo.DictDichVuCon == null) return;

                if (_companyInfo.DictDichVuCon.ContainsKey(serviceGUID))
                {
                    DataTable dt = _companyInfo.DictDichVuCon[serviceGUID].Copy();
                    foreach (DataRow row in dt.Rows)
                    {
                        row["Checked"] = false;
                    }

                    dgDichVuCon.DataSource = dt;
                }
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

            string serviceGUID = cboService.SelectedValue.ToString();
            DataTable dt = dgDichVuCon.DataSource as DataTable;
            DataRow[] rs = dt.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
            if (rs != null && rs.Length > 0)
            {
                MsgBox.Show(this.Text, string.Format("Dịch vụ: '{0}' không được trùng với dịch vụ con.", cboService.Text), IconType.Information);
                cboService.Focus();
                return false;
            }

            return true;
        }

        private void OnAdd()
        {
            if (cboService.SelectedValue == null || cboService.Text.Trim() == string.Empty) return;
            string serviceGUID = cboService.SelectedValue.ToString();
            
            DataTable dt = dgDichVuCon.DataSource as DataTable;
            dlgServices dlg = new dlgServices(serviceGUID, dt);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                List<DataRow> serviceList = dlg.Services;

                foreach (DataRow row in serviceList)
                {
                    DataRow newRow = dt.NewRow();
                    newRow["Checked"] = false;
                    newRow["ServiceGUID"] = row["ServiceGUID"];
                    newRow["Code"] = row["Code"];
                    newRow["Name"] = row["Name"];
                    newRow["EnglishName"] = row["EnglishName"];

                    dt.Rows.Add(newRow);
                }
            }
        }

        private void OnDelete()
        {
            DataTable dt = dgDichVuCon.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            List<DataRow> deletedRows = new List<DataRow>();

            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                    deletedRows.Add(row);
            }

            if (deletedRows.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa dịch vụ mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    foreach (DataRow row in deletedRows)
                    {
                        string serviceGUID = row["ServiceGUID"].ToString();
                        if (!_deletedDichVuCons.Contains(serviceGUID))
                            _deletedDichVuCons.Add(serviceGUID);

                        dt.Rows.Remove(row);
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những dịch vụ cần xóa.", IconType.Information);
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
                else
                {
                    string serviceGUID = cboService.SelectedValue.ToString();
                    if (_companyInfo.DictDichVuCon == null) _companyInfo.DictDichVuCon = new Dictionary<string, DataTable>();

                    if (_companyInfo.DictDichVuCon.ContainsKey(serviceGUID))
                        _companyInfo.DictDichVuCon[serviceGUID] = dgDichVuCon.DataSource as DataTable;
                    else
                        _companyInfo.DictDichVuCon.Add(serviceGUID, dgDichVuCon.DataSource as DataTable);

                    if (_companyInfo.DictDeletedDichVuCons == null) _companyInfo.DictDeletedDichVuCons = new Dictionary<string, List<string>>();

                    if (_companyInfo.DictDeletedDichVuCons.ContainsKey(serviceGUID))
                        _companyInfo.DictDeletedDichVuCons[serviceGUID] = _deletedDichVuCons;
                    else
                        _companyInfo.DictDeletedDichVuCons.Add(serviceGUID, _deletedDichVuCons);
                }
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDelete();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgDichVuCon.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnDelete();
        }
        #endregion

        
    }
}
