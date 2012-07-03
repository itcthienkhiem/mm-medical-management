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
    public partial class dlgAddXetNghiemTay : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private XetNghiem_Manual _xetNghiem = new XetNghiem_Manual();
        private DataRow _drXetNghiem = null;
        private bool _allowEdit = true;
        #endregion

        #region Constructor
        public dlgAddXetNghiemTay()
        {
            InitializeComponent();
        }

        public dlgAddXetNghiemTay(DataRow drXetNghiem, bool allowEdit)
        {
            InitializeComponent();
            _isNew = false;
            _allowEdit = allowEdit;
            this.Text = "Sua xet nghiem tay";
            _drXetNghiem = drXetNghiem;
        }
        #endregion

        #region Properties
        public XetNghiem_Manual XetNghiem
        {
            get { return _xetNghiem; }
            set { _xetNghiem = value; }
        }
        #endregion

        #region UI Command
        private void DisplayInfo()
        {
            try
            {
                txtTenXetNghiem.Text = _drXetNghiem["Fullname"] as string;
                cboLoaiXetNghiem.Text = Utility.GetLoaiXetNghiem(_drXetNghiem["Type"].ToString());
                numThuTuNhom.Value = Convert.ToInt32(_drXetNghiem["GroupID"]);
                numThuTu.Value = Convert.ToInt32(_drXetNghiem["Order"]);
                if (_drXetNghiem["GroupName"] != null && _drXetNghiem["GroupName"] != DBNull.Value)
                    cboNhomXetNghiem.Text = _drXetNghiem["GroupName"].ToString();

                _xetNghiem.XetNghiem_ManualGUID = Guid.Parse(_drXetNghiem["XetNghiem_ManualGUID"].ToString());

                if (_drXetNghiem["CreatedDate"] != null && _drXetNghiem["CreatedDate"] != DBNull.Value)
                    _xetNghiem.CreatedDate = Convert.ToDateTime(_drXetNghiem["CreatedDate"]);

                if (_drXetNghiem["CreatedBy"] != null && _drXetNghiem["CreatedBy"] != DBNull.Value)
                    _xetNghiem.CreatedBy = Guid.Parse(_drXetNghiem["CreatedBy"].ToString());

                if (_drXetNghiem["UpdatedDate"] != null && _drXetNghiem["UpdatedDate"] != DBNull.Value)
                    _xetNghiem.UpdatedDate = Convert.ToDateTime(_drXetNghiem["UpdatedDate"]);

                if (_drXetNghiem["UpdatedBy"] != null && _drXetNghiem["UpdatedBy"] != DBNull.Value)
                    _xetNghiem.UpdatedBy = Guid.Parse(_drXetNghiem["UpdatedBy"].ToString());

                if (_drXetNghiem["DeletedDate"] != null && _drXetNghiem["DeletedDate"] != DBNull.Value)
                    _xetNghiem.DeletedDate = Convert.ToDateTime(_drXetNghiem["DeletedDate"]);

                if (_drXetNghiem["DeletedBy"] != null && _drXetNghiem["DeletedBy"] != DBNull.Value)
                    _xetNghiem.DeletedBy = Guid.Parse(_drXetNghiem["DeletedBy"].ToString());

                _xetNghiem.Status = Convert.ToByte(_drXetNghiem["Status"]);

                Result result = XetNghiemTayBus.GetChiTietXetNghiemList2(_xetNghiem.XetNghiem_ManualGUID.ToString());
                if (result.IsOK)
                {
                    List<ChiTietXetNghiem_Manual> ctxns = result.QueryResult as List<ChiTietXetNghiem_Manual>;
                    _uNormal.SetChiTietXetNghiem_ManualList(ctxns);

                    if (!_allowEdit)
                    {
                        gbXetNghiem.Enabled = _allowEdit;
                        btnOK.Enabled = _allowEdit;
                    }
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiemTayBus.GetChiTietXetNghiemList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiemTayBus.GetChiTietXetNghiemList"));
                }
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        public bool CheckInfo()
        {
            if (txtTenXetNghiem.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên xét nghiệm.", IconType.Information);
                txtTenXetNghiem.Focus();
                return false;
            }

            if (cboLoaiXetNghiem.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn loại xét nghiệm.", IconType.Information);
                cboLoaiXetNghiem.Focus();
                return false;
            }

            string xetNghiem_ManualGUID = _isNew ? string.Empty : _xetNghiem.XetNghiem_ManualGUID.ToString();
            Result result = XetNghiemTayBus.CheckTenXetNghiemExist(xetNghiem_ManualGUID, txtTenXetNghiem.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Tên xét nghiệm này đã tồn tại rồi. Vui lòng nhập tên khác.", IconType.Information);
                    txtTenXetNghiem.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("XetNghiemTayBus.CheckTenXetNghiemExist"), IconType.Error);
                return false;
            }

            if (!_uNormal.CheckInfo())
            {
                _uNormal.Focus();
                return false;
            }

            return true;
        }

        private void SaveInfoAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSaveInfoProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnSaveInfo()
        {
            try
            {
                _xetNghiem.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _xetNghiem.CreatedDate = DateTime.Now;
                    _xetNghiem.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _xetNghiem.UpdatedDate = DateTime.Now;
                    _xetNghiem.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                MethodInvoker method = delegate
                {
                    _xetNghiem.Fullname = txtTenXetNghiem.Text;
                    _xetNghiem.TenXetNghiem = txtTenXetNghiem.Text;
                    _xetNghiem.Type = GetLoaiXetNghiem();
                    _xetNghiem.GroupID = (int)numThuTuNhom.Value;
                    _xetNghiem.GroupName = cboNhomXetNghiem.Text;
                    _xetNghiem.Order = (int)numThuTu.Value;

                    List<ChiTietXetNghiem_Manual> ctxns = _uNormal.GetChiTietXetNghiem_ManualList();
                    if (ctxns == null) ctxns = new List<ChiTietXetNghiem_Manual>();

                    Result result = XetNghiemTayBus.InsertXetNghiem(_xetNghiem, ctxns);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("XetNghiemTayBus.InsertXetNghiem"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiemTayBus.InsertXetNghiem"));
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private string GetLoaiXetNghiem()
        {
            switch (cboLoaiXetNghiem.Text)
            {
                case "Nước tiểu":
                    return LoaiXetNghiem.Urine.ToString();
                case "Miễn dịch":
                    return LoaiXetNghiem.MienDich.ToString();
                case "Khác":
                    return LoaiXetNghiem.Khac.ToString();
                case "Huyết học":
                    return LoaiXetNghiem.Haematology.ToString();
                case "Sinh hóa":
                    return LoaiXetNghiem.Biochemistry.ToString();
            }

            return string.Empty;
        }

        private void InitData()
        {
            Result result = XetNghiemTayBus.GetDonViList();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                _uNormal.DonViList = dt;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("XetNghiemTayBus.GetDonViList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiemTayBus.GetDonViList"));
            }

            result = XetNghiemTayBus.GetNhomXetNghiemList();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                cboNhomXetNghiem.Items.Add(string.Empty);
                foreach (DataRow row in dt.Rows)
                {
                    string nhomXetNghiem = row[0].ToString().Trim();
                    if (nhomXetNghiem == string.Empty) continue;
                    cboNhomXetNghiem.Items.Add(nhomXetNghiem);
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("XetNghiemTayBus.GetNhomXetNghiemList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiemTayBus.GetNhomXetNghiemList"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddXetNghiemTay_Load(object sender, EventArgs e)
        {
            InitData();
            if (!_isNew) DisplayInfo();
        }

        private void dlgAddXetNghiemTay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
        }
        #endregion

        #region Working Thread
        private void OnSaveInfoProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnSaveInfo();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion

        
    }
}
