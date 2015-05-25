using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Databasae;
using MM.Bussiness;
using MM.Common;
using System.Threading;

namespace MM.Dialogs
{
    public partial class dlgAddPhieuChi : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private DataRow _drPhieuChi = null;
        private PhieuChi _phieuChi = new PhieuChi();
        #endregion

        #region Constructor
        public dlgAddPhieuChi()
        {
            InitializeComponent();
            GenerateCode();
        }

        public dlgAddPhieuChi(DataRow drPhieuChi) : this()
        {
            _drPhieuChi = drPhieuChi;
            _isNew = false;
            this.Text = "Sua phieu chi";
        }
        #endregion

        #region Properties
        public PhieuChi PhieuChi
        {
            get { return _phieuChi; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            dtpkNgayChi.Value = DateTime.Now;
        }

        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = PhieuChiBus.GetPhieuChiCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                txtSoPhieuChi.Text = Utility.GetCode("PC", count + 1, 7);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("PhieuChiBus.GetPhieuChiCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PhieuChiBus.GetPhieuChiCount"));
            }
        }

        private string GetGenerateCode()
        {
            Result result = PhieuChiBus.GetPhieuChiCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                return Utility.GetCode("PC", count + 1, 7);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("PhieuChiBus.GetPhieuChiCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PhieuChiBus.GetPhieuChiCount"));
                return string.Empty;
            }
        }

        private void DisplayInfo()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                txtSoPhieuChi.Text = _drPhieuChi["SoPhieuChi"] as string;
                dtpkNgayChi.Value = Convert.ToDateTime(_drPhieuChi["NgayChi"]);
                numSoTien.Value = (Decimal)Convert.ToDouble(_drPhieuChi["SoTien"]);
                txtGhiChu.Text = _drPhieuChi["DienGiai"] as string;
                _phieuChi.PhieuChiGUID = Guid.Parse(_drPhieuChi["PhieuChiGUID"].ToString());

                if (_drPhieuChi["CreatedDate"] != null && _drPhieuChi["CreatedDate"] != DBNull.Value)
                    _phieuChi.CreatedDate = Convert.ToDateTime(_drPhieuChi["CreatedDate"]);

                if (_drPhieuChi["CreatedBy"] != null && _drPhieuChi["CreatedBy"] != DBNull.Value)
                    _phieuChi.CreatedBy = Guid.Parse(_drPhieuChi["CreatedBy"].ToString());

                if (_drPhieuChi["UpdatedDate"] != null && _drPhieuChi["UpdatedDate"] != DBNull.Value)
                    _phieuChi.UpdatedDate = Convert.ToDateTime(_drPhieuChi["UpdatedDate"]);

                if (_drPhieuChi["UpdatedBy"] != null && _drPhieuChi["UpdatedBy"] != DBNull.Value)
                    _phieuChi.UpdatedBy = Guid.Parse(_drPhieuChi["UpdatedBy"].ToString());

                if (_drPhieuChi["DeletedDate"] != null && _drPhieuChi["DeletedDate"] != DBNull.Value)
                    _phieuChi.DeletedDate = Convert.ToDateTime(_drPhieuChi["DeletedDate"]);

                if (_drPhieuChi["DeletedBy"] != null && _drPhieuChi["DeletedBy"] != DBNull.Value)
                    _phieuChi.DeletedBy = Guid.Parse(_drPhieuChi["DeletedBy"].ToString());

                _phieuChi.Status = Convert.ToByte(_drPhieuChi["Status"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (numSoTien.Value <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập số tiền cần chi.", IconType.Information);
                numSoTien.Focus();
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
                    _phieuChi.NgayChi = dtpkNgayChi.Value;
                    _phieuChi.SoTien = (double)numSoTien.Value;
                    _phieuChi.DienGiai = txtGhiChu.Text;
                    _phieuChi.Status = (byte)Status.Actived;

                    if (_isNew)
                    {
                        _phieuChi.CreatedDate = DateTime.Now;
                        _phieuChi.CreatedBy = Guid.Parse(Global.UserGUID);

                        string soPhieuChi = GetGenerateCode();
                        if (soPhieuChi == string.Empty) return;
                        _phieuChi.SoPhieuChi = soPhieuChi;
                    }
                    else
                    {
                        _phieuChi.SoPhieuChi = txtSoPhieuChi.Text;
                        _phieuChi.UpdatedDate = DateTime.Now;
                        _phieuChi.UpdatedBy = Guid.Parse(Global.UserGUID);
                    }

                    Result result = PhieuChiBus.InsertPhieuChi(_phieuChi);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("PhieuChiBus.InsertPhieuChi"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PhieuChiBus.InsertPhieuChi"));
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
        private void dlgAddPhieuChi_Load(object sender, EventArgs e)
        {
            InitData();
            if (!_isNew) DisplayInfo();
        }

        private void dlgAddPhieuChi_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
                else SaveInfoAsThread();

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
