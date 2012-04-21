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
    public partial class dlgUpdateChiSoKetQuaXetNghiem : dlgBase
    {
        #region Members
        private DataRow _drCTKQXN = null;
        private ChiTietKetQuaXetNghiem_Hitachi917 _chiTietKQXN = new ChiTietKetQuaXetNghiem_Hitachi917();
        #endregion

        #region Constructor
        public dlgUpdateChiSoKetQuaXetNghiem(DataRow drCTKQXN)
        {
            InitializeComponent();
            _drCTKQXN = drCTKQXN;
        }
        #endregion

        #region Properties
        public ChiTietKetQuaXetNghiem_Hitachi917 ChiTietKQXN
        {
            get { return _chiTietKQXN; }
        }
        #endregion

        #region UI Command
        private void DisplayInfo()
        {
            _chiTietKQXN.ChiTietKQXN_Hitachi917GUID = Guid.Parse(_drCTKQXN["ChiTietKQXN_Hitachi917GUID"].ToString());

            if (_drCTKQXN["TenXetNghiem"] != null && _drCTKQXN["TenXetNghiem"] != DBNull.Value)
                txTenXetNghiem.Text = _drCTKQXN["TenXetNghiem"].ToString();

            txtKetQua.Text = _drCTKQXN["TestResult"].ToString().Trim();
            txtBinhThuong.Text = _drCTKQXN["BinhThuong"].ToString();

            _chiTietKQXN.TestNum = Convert.ToInt32(_drCTKQXN["TestNum"]);
        }

        private bool CheckInfo()
        {
            if (txtKetQua.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập kết quả xét nghiệm.", IconType.Information);
                txtKetQua.Focus();
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
                    _chiTietKQXN.TestResult = txtKetQua.Text;
                    Result result = XetNghiem_Hitachi917Bus.UpdateChiSoKetQuaXetNghiem(_chiTietKQXN);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("XetNghiem_Hitachi917Bus.UpdateChiSoKetQuaXetNghiem"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_Hitachi917Bus.UpdateChiSoKetQuaXetNghiem"));
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
        private void dlgUpdateChiSoKetQuaXetNghiem_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        }

        private void dlgUpdateChiSoKetQuaXetNghiem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) 
                    e.Cancel = true;
                else
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
