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
    public partial class dlgUpdateChiSoKetQuaXetNghiem_CellDyn3200 : dlgBase
    {
        #region Members
        private DataRow _drCTKQXN = null;
        private ChiTietKetQuaXetNghiem_CellDyn3200 _chiTietKQXN = new ChiTietKetQuaXetNghiem_CellDyn3200();
        #endregion

        #region Constructor
        public dlgUpdateChiSoKetQuaXetNghiem_CellDyn3200(DataRow drCTKQXN)
        {
            InitializeComponent();
            _drCTKQXN = drCTKQXN;
        }
        #endregion

        #region Properties
        public ChiTietKetQuaXetNghiem_CellDyn3200 ChiTietKQXN
        {
            get { return _chiTietKQXN; }
        }
        #endregion

        #region UI Command
        private void DisplayInfo()
        {
            _chiTietKQXN.ChiTietKQXN_CellDyn3200GUID = Guid.Parse(_drCTKQXN["ChiTietKQXN_CellDyn3200GUID"].ToString());

            if (_drCTKQXN["TenXetNghiem"] != null && _drCTKQXN["TenXetNghiem"] != DBNull.Value)
                txTenXetNghiem.Text = _drCTKQXN["TenXetNghiem"].ToString();

            numKetQua.Value = (Decimal)Convert.ToDouble(_drCTKQXN["TestResult"].ToString().Trim());

            if (_drCTKQXN["TestPercent"] == null || _drCTKQXN["TestPercent"] == DBNull.Value)
                numTestPercent.Enabled = false;
            else
                numTestPercent.Value = (decimal)Convert.ToDouble(_drCTKQXN["TestPercent"]);

            txtBinhThuong.Text = _drCTKQXN["BinhThuong"].ToString();

            _chiTietKQXN.TenXetNghiem = _drCTKQXN["TenXetNghiem"].ToString();
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
                    _chiTietKQXN.TestResult = (double)numKetQua.Value;
                    if (numTestPercent.Enabled)
                        _chiTietKQXN.TestPercent = (double)numTestPercent.Value;

                    Result result = XetNghiem_CellDyn3200Bus.UpdateChiSoKetQuaXetNghiem(_chiTietKQXN);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("XetNghiem_CellDyn3200Bus.UpdateChiSoKetQuaXetNghiem"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_CellDyn3200Bus.UpdateChiSoKetQuaXetNghiem"));
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
