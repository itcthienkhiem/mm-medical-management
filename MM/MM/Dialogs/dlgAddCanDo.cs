using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Bussiness;
using MM.Common;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgAddCanDo : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private string _patientGUID = string.Empty;
        private CanDo _canDo = new CanDo();
        private DataRow _drCanDo = null;
        #endregion

        #region Constructor
        public dlgAddCanDo(string patientGUID)
        {
            InitializeComponent();
            //InitData();
            _patientGUID = patientGUID;
        }

        public dlgAddCanDo(string patientGUID, DataRow drCanDo)
        {
            InitializeComponent();
            //InitData();
            _isNew = false;
            this.Text = "Sua can do";
            _patientGUID = patientGUID;
            _drCanDo = drCanDo;
            //DisplayInfo(drCanDo);
        }
        #endregion

        #region Properties
        public CanDo CanDo
        {
            get { return _canDo; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            dtpkNgayCanDo.Value = DateTime.Now;

            //DocStaff
            List<byte> staffTypes = new List<byte>();
            staffTypes.Add((byte)StaffType.DieuDuong);
            Result result = DocStaffBus.GetDocStaffList(staffTypes);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
                return;
            }
            else
            {
                cboDocStaff.DataSource = result.QueryResult;
            }

            if (Global.StaffType == StaffType.DieuDuong)
            {
                cboDocStaff.SelectedValue = Global.UserGUID;
                cboDocStaff.Enabled = false;
            }
        }

        private void DisplayInfo(DataRow drCanDo)
        {
            try
            {
                _canDo.CanDoGuid = Guid.Parse(drCanDo["CanDoGuid"].ToString());
                dtpkNgayCanDo.Value = Convert.ToDateTime(drCanDo["NgayCanDo"]);
                cboDocStaff.SelectedValue = drCanDo["DocStaffGUID"].ToString();

                if (drCanDo["TimMach"] != null && drCanDo["TimMach"] != DBNull.Value)
                    txtTimMach.Text = drCanDo["TimMach"].ToString();

                if (drCanDo["HuyetAp"] != null && drCanDo["HuyetAp"] != DBNull.Value)
                    txtHuyetAp.Text = drCanDo["HuyetAp"].ToString();

                if (drCanDo["ChieuCao"] != null && drCanDo["ChieuCao"] != DBNull.Value)
                    txtChieuCao.Text = drCanDo["ChieuCao"].ToString();

                if (drCanDo["HoHap"] != null && drCanDo["HoHap"] != DBNull.Value)
                    txtHoHap.Text = drCanDo["HoHap"].ToString();

                if (drCanDo["ChieuCao"] != null && drCanDo["ChieuCao"] != DBNull.Value)
                    txtChieuCao.Text = drCanDo["ChieuCao"].ToString();

                if (drCanDo["CanNang"] != null && drCanDo["CanNang"] != DBNull.Value)
                    txtCanNang.Text = drCanDo["CanNang"].ToString();

                if (drCanDo["BMI"] != null && drCanDo["BMI"] != DBNull.Value)
                    txtBMI.Text = drCanDo["BMI"].ToString();

                if (drCanDo["MuMau"] != null && drCanDo["MuMau"] != DBNull.Value)
                    txtMuMau.Text = drCanDo["MuMau"].ToString();

                if (drCanDo["MatPhai"] != null && drCanDo["MatPhai"] != DBNull.Value)
                    txtMatPhai.Text = drCanDo["MatPhai"].ToString();

                if (drCanDo["MatTrai"] != null && drCanDo["MatTrai"] != DBNull.Value)
                    txtMatTrai.Text = drCanDo["MatTrai"].ToString();

                if (drCanDo["HieuChinh"] != null && drCanDo["HieuChinh"] != DBNull.Value)
                {
                    bool isHieuChinh = Convert.ToBoolean(drCanDo["HieuChinh"]);
                    raHieuChinh.Checked = isHieuChinh;
                }

                if (drCanDo["CreatedDate"] != null && drCanDo["CreatedDate"] != DBNull.Value)
                    _canDo.CreatedDate = Convert.ToDateTime(drCanDo["CreatedDate"]);

                if (drCanDo["CreatedBy"] != null && drCanDo["CreatedBy"] != DBNull.Value)
                    _canDo.CreatedBy = Guid.Parse(drCanDo["CreatedBy"].ToString());

                if (drCanDo["UpdatedDate"] != null && drCanDo["UpdatedDate"] != DBNull.Value)
                    _canDo.UpdatedDate = Convert.ToDateTime(drCanDo["UpdatedDate"]);

                if (drCanDo["UpdatedBy"] != null && drCanDo["UpdatedBy"] != DBNull.Value)
                    _canDo.UpdatedBy = Guid.Parse(drCanDo["UpdatedBy"].ToString());

                if (drCanDo["DeletedDate"] != null && drCanDo["DeletedDate"] != DBNull.Value)
                    _canDo.DeletedDate = Convert.ToDateTime(drCanDo["DeletedDate"]);

                if (drCanDo["DeletedBy"] != null && drCanDo["DeletedBy"] != DBNull.Value)
                    _canDo.DeletedBy = Guid.Parse(drCanDo["DeletedBy"].ToString());

                _canDo.Status = Convert.ToByte(drCanDo["Status"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (cboDocStaff.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn người khám.", IconType.Information);
                cboDocStaff.Focus();
                return false;
            }

            return true;
        }

        private void OnSaveInfo()
        {
            try
            {
                if (_isNew)
                {
                    _canDo.CreatedDate = DateTime.Now;
                    _canDo.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _canDo.UpdatedDate = DateTime.Now;
                    _canDo.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                _canDo.PatientGUID = Guid.Parse(_patientGUID);
                _canDo.TimMach = txtTimMach.Text;
                _canDo.HuyetAp = txtHuyetAp.Text;
                _canDo.HoHap = txtHoHap.Text;
                _canDo.ChieuCao = txtChieuCao.Text;
                _canDo.CanNang = txtCanNang.Text;
                _canDo.BMI = txtBMI.Text;
                _canDo.MuMau = txtMuMau.Text;
                _canDo.MatPhai = txtMatPhai.Text;
                _canDo.MatTrai = txtMatTrai.Text;
                _canDo.HieuChinh = raHieuChinh.Checked;
                _canDo.CanDoKhac = string.Empty;

                MethodInvoker method = delegate
                {
                    _canDo.NgayCanDo = dtpkNgayCanDo.Value;
                    _canDo.DocStaffGUID = Guid.Parse(cboDocStaff.SelectedValue.ToString());

                    Result result = CanDoBus.InsertCanDo(_canDo);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("CanDoBus.InsertCanDo"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("CanDoBus.InsertCanDo"));
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
        #endregion

        #region Window Event Handlers
        private void dlgAddCanDo_Load(object sender, EventArgs e)
        {
            InitData();
            if (!_isNew) DisplayInfo(_drCanDo);
        }


        private void dlgAddCanDo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
            else
            {
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin cân đo ?") == System.Windows.Forms.DialogResult.Yes)
                    SaveInfoAsThread();
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
