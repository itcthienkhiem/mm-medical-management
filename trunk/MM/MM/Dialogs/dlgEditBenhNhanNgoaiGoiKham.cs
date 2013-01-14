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
    public partial class dlgEditBenhNhanNgoaiGoiKham : dlgBase
    {
        #region Members
        private DataRow _drBenhNhanNgoaiGoiKham = null;
        private BenhNhanNgoaiGoiKham _benhNhanNgoaiGoiKham = new BenhNhanNgoaiGoiKham();
        #endregion

        #region Constructor
        public dlgEditBenhNhanNgoaiGoiKham(DataRow drBenhNhanNgoaiGoiKham)
        {
            InitializeComponent();
            _drBenhNhanNgoaiGoiKham = drBenhNhanNgoaiGoiKham;
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void InitData()
        {
            Cursor.Current = Cursors.WaitCursor;
            cboLanDauTaiKham.SelectedIndex = 0;

            Result result = PatientBus.GetPatientList();
            if (result.IsOK)
            {
                DataTable dtPatient = result.QueryResult as DataTable;
                DataRow row = dtPatient.NewRow();
                row["PatientGUID"] = Guid.Empty.ToString();
                row["FullName"] = string.Empty;
                dtPatient.Rows.InsertAt(row, 0);

                cboBenhNhan.DataSource = dtPatient;
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.GetPatientList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientList"));
            }

            result = ServicesBus.GetServicesList();
            if (result.IsOK)
            {
                DataTable dtService = result.QueryResult as DataTable;
                DataRow row = dtService.NewRow();
                row["ServiceGUID"] = Guid.Empty.ToString();
                row["Name"] = " ";
                dtService.Rows.InsertAt(row, 0);

                cboService.DataSource = dtService;
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServicesBus.GetServicesList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.GetServicesList"));
            }

            result = CompanyContractBus.GetContractList();
            if (result.IsOK)
            {
                DataTable dtContract = result.QueryResult as DataTable;
                DataRow row = dtContract.NewRow();
                row["CompanyContractGUID"] = Guid.Empty.ToString();
                row["ContractName"] = string.Empty;
                dtContract.Rows.InsertAt(row, 0);

                cboHopDong.DataSource = dtContract;
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyContractBus.GetContractList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetContractList"));
            }
        }

        private void DisplayInfo()
        {
            try
            {
                _benhNhanNgoaiGoiKham.BenhNhanNgoaiGoiKhamGUID = Guid.Parse(_drBenhNhanNgoaiGoiKham["BenhNhanNgoaiGoiKhamGUID"].ToString());
                dtpkNgayKham.Value = Convert.ToDateTime(_drBenhNhanNgoaiGoiKham["NgayKham"]);
                cboBenhNhan.SelectedValue = _drBenhNhanNgoaiGoiKham["PatientGUID"].ToString();
                cboService.SelectedValue = _drBenhNhanNgoaiGoiKham["ServiceGUID"].ToString();

                if (_drBenhNhanNgoaiGoiKham["HopDongGUID"] != null && 
                    _drBenhNhanNgoaiGoiKham["HopDongGUID"] != DBNull.Value && 
                    _drBenhNhanNgoaiGoiKham["HopDongGUID"].ToString() != Guid.Empty.ToString())
                    cboHopDong.SelectedValue = Guid.Parse(_drBenhNhanNgoaiGoiKham["HopDongGUID"].ToString());

                cboLanDauTaiKham.SelectedIndex = Convert.ToInt32(_drBenhNhanNgoaiGoiKham["LanDau"]);

                if (_drBenhNhanNgoaiGoiKham["CreatedDate"] != null && _drBenhNhanNgoaiGoiKham["CreatedDate"] != DBNull.Value)
                    _benhNhanNgoaiGoiKham.CreatedDate = Convert.ToDateTime(_drBenhNhanNgoaiGoiKham["CreatedDate"]);

                if (_drBenhNhanNgoaiGoiKham["CreatedBy"] != null && _drBenhNhanNgoaiGoiKham["CreatedBy"] != DBNull.Value)
                    _benhNhanNgoaiGoiKham.CreatedBy = Guid.Parse(_drBenhNhanNgoaiGoiKham["CreatedBy"].ToString());

                if (_drBenhNhanNgoaiGoiKham["UpdatedDate"] != null && _drBenhNhanNgoaiGoiKham["UpdatedDate"] != DBNull.Value)
                    _benhNhanNgoaiGoiKham.UpdatedDate = Convert.ToDateTime(_drBenhNhanNgoaiGoiKham["UpdatedDate"]);

                if (_drBenhNhanNgoaiGoiKham["UpdatedBy"] != null && _drBenhNhanNgoaiGoiKham["UpdatedBy"] != DBNull.Value)
                    _benhNhanNgoaiGoiKham.UpdatedBy = Guid.Parse(_drBenhNhanNgoaiGoiKham["UpdatedBy"].ToString());

                if (_drBenhNhanNgoaiGoiKham["DeletedDate"] != null && _drBenhNhanNgoaiGoiKham["DeletedDate"] != DBNull.Value)
                    _benhNhanNgoaiGoiKham.DeletedDate = Convert.ToDateTime(_drBenhNhanNgoaiGoiKham["DeletedDate"]);

                if (_drBenhNhanNgoaiGoiKham["DeletedBy"] != null && _drBenhNhanNgoaiGoiKham["DeletedBy"] != DBNull.Value)
                    _benhNhanNgoaiGoiKham.DeletedBy = Guid.Parse(_drBenhNhanNgoaiGoiKham["DeletedBy"].ToString());

                _benhNhanNgoaiGoiKham.Status = Convert.ToByte(_drBenhNhanNgoaiGoiKham["Status"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (cboBenhNhan.SelectedValue == null || cboBenhNhan.Text == string.Empty)
            {
                string serverName = cboService.Text;
                if (serverName.ToLower().IndexOf("siêu âm") >= 0 || serverName.ToLower().IndexOf("sieu am") >= 0)
                {
                    MsgBox.Show(this.Text, "Vui lòng chọn bệnh nhân.", IconType.Information);
                    cboBenhNhan.Focus();
                    return false;
                }
            }

            if (cboService.SelectedValue == null || cboService.Text == null || cboService.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn dịch vụ.", IconType.Information);
                cboService.Focus();
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
                MethodInvoker method = delegate
                {
                    _benhNhanNgoaiGoiKham.UpdatedDate = DateTime.Now;
                    _benhNhanNgoaiGoiKham.DeletedBy = Guid.Parse(Global.UserGUID);

                    _benhNhanNgoaiGoiKham.NgayKham = dtpkNgayKham.Value;
                    _benhNhanNgoaiGoiKham.PatientGUID = Guid.Parse(cboBenhNhan.SelectedValue.ToString());
                    _benhNhanNgoaiGoiKham.ServiceGUID = Guid.Parse(cboService.SelectedValue.ToString());
                    _benhNhanNgoaiGoiKham.LanDau = (byte)cboLanDauTaiKham.SelectedIndex;

                    if (cboHopDong.SelectedValue != null && cboHopDong.Text.Trim() != string.Empty)
                        _benhNhanNgoaiGoiKham.HopDongGUID = Guid.Parse(cboHopDong.SelectedValue.ToString());
                    else
                        _benhNhanNgoaiGoiKham.HopDongGUID = null;

                    Result result = BenhNhanNgoaiGoiKhamBus.UpdateBenhNhanNgoaiGoiKham(_benhNhanNgoaiGoiKham);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("BenhNhanNgoaiGoiKhamBus.UpdateBenhNhanNgoaiGoiKham"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("BenhNhanNgoaiGoiKhamBus.UpdateBenhNhanNgoaiGoiKham"));
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
        #endregion

        #region Window Event Handlers
        private void dlgEditBenhNhanNgoaiGoiKham_Load(object sender, EventArgs e)
        {
            InitData();
            DisplayInfo();
        }

        private void dlgEditBenhNhanNgoaiGoiKham_FormClosing(object sender, FormClosingEventArgs e)
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
