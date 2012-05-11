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
        private string _binhThuong = string.Empty;
        private string _percent = string.Empty;
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

        public string BinhThuong
        {
            get { return _binhThuong; }
        }

        public string Percent
        {
            get { return _percent; }
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

            if ((_drCTKQXN["FromValue"] == null || _drCTKQXN["FromValue"] == DBNull.Value) &&
                (_drCTKQXN["ToValue"] == null || _drCTKQXN["ToValue"] == DBNull.Value))
            {
                chkFromValue_Normal.Enabled = false;
                chkToValue_Normal.Enabled = false;
            }
            else if (_drCTKQXN["ToValue"] == null || _drCTKQXN["ToValue"] == DBNull.Value)
            {
                chkFromValue_Normal.Checked = true;
                numFromValue_Normal.Value = (Decimal)Convert.ToDouble(_drCTKQXN["FromValue"]);
            }
            else if (_drCTKQXN["FromValue"] == null || _drCTKQXN["FromValue"] == DBNull.Value)
            {
                chkToValue_Normal.Checked = true;
                numToValue_Normal.Value = (Decimal)Convert.ToDouble(_drCTKQXN["ToValue"]);
            }
            else
            {
                chkFromValue_Normal.Checked = true;
                numFromValue_Normal.Value = (Decimal)Convert.ToDouble(_drCTKQXN["FromValue"]);
                chkToValue_Normal.Checked = true;
                numToValue_Normal.Value = (Decimal)Convert.ToDouble(_drCTKQXN["ToValue"]);
            }

            if ((_drCTKQXN["FromPercent"] == null || _drCTKQXN["FromPercent"] == DBNull.Value) &&
                (_drCTKQXN["ToPercent"] == null || _drCTKQXN["ToPercent"] == DBNull.Value))
            {
                chkFromValue_NormalPercent.Enabled = false;
                chkToValue_NormalPercent.Enabled = false;
            }
            else if (_drCTKQXN["ToPercent"] == null || _drCTKQXN["ToPercent"] == DBNull.Value)
            {
                chkFromValue_NormalPercent.Checked = true;
                numFromValue_NormalPercent.Value = (Decimal)Convert.ToDouble(_drCTKQXN["FromPercent"]);
            }
            else if (_drCTKQXN["FromPercent"] == null || _drCTKQXN["FromPercent"] == DBNull.Value)
            {
                chkToValue_NormalPercent.Checked = true;
                numToValue_NormalPercent.Value = (Decimal)Convert.ToDouble(_drCTKQXN["ToPercent"]);
            }
            else
            {
                chkFromValue_NormalPercent.Checked = true;
                numFromValue_NormalPercent.Value = (Decimal)Convert.ToDouble(_drCTKQXN["FromPercent"]);
                chkToValue_NormalPercent.Checked = true;
                numToValue_NormalPercent.Value = (Decimal)Convert.ToDouble(_drCTKQXN["ToPercent"]);
            }

            if (_drCTKQXN["DonVi"] != null && _drCTKQXN["DonVi"] != DBNull.Value)
                txtDonVi.Text = _drCTKQXN["DonVi"].ToString();

            _chiTietKQXN.TenXetNghiem = _drCTKQXN["TenXetNghiem"].ToString();
        }

        private bool CheckInfo()
        {
            if (chkFromValue_Normal.Enabled == true && chkToValue_Normal.Enabled == true)
            {
                if (!chkFromValue_Normal.Checked && !chkToValue_Normal.Checked)
                {
                    MsgBox.Show(this.Text, "Vui lòng nhập ngưỡng chỉ số.", IconType.Information);
                    return false;
                }
            }

            if (chkFromValue_NormalPercent.Enabled == true && chkToValue_NormalPercent.Enabled == true)
            {
                if (!chkFromValue_NormalPercent.Checked && !chkToValue_NormalPercent.Checked)
                {
                    MsgBox.Show(this.Text, "Vui lòng nhập ngưỡng chỉ số.", IconType.Information);
                    return false;
                }
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
                    _chiTietKQXN.TestResult = (double)numKetQua.Value;
                    if (numTestPercent.Enabled)
                        _chiTietKQXN.TestPercent = (double)numTestPercent.Value;

                    if (chkFromValue_Normal.Checked)
                        _chiTietKQXN.FromValue = (double)numFromValue_Normal.Value;

                    if (chkToValue_Normal.Checked)
                        _chiTietKQXN.ToValue = (double)numToValue_Normal.Value;

                    if (chkFromValue_NormalPercent.Checked)
                        _chiTietKQXN.FromPercent = (double)numFromValue_NormalPercent.Value;

                    if (chkToValue_NormalPercent.Checked)
                        _chiTietKQXN.ToPercent = (double)numToValue_NormalPercent.Value;

                    _chiTietKQXN.DonVi = txtDonVi.Text;

                    Result result = XetNghiem_CellDyn3200Bus.UpdateChiSoKetQuaXetNghiem(_chiTietKQXN, ref _binhThuong, ref _percent);

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
                if (!CheckInfo()) e.Cancel = true;
                else SaveInfoAsThread();
            }
        }

        private void chkFromValue_Normal_CheckedChanged(object sender, EventArgs e)
        {
            numFromValue_Normal.Enabled = chkFromValue_Normal.Checked;
        }

        private void chkToValue_Normal_CheckedChanged(object sender, EventArgs e)
        {
            numToValue_Normal.Enabled = chkToValue_Normal.Checked;
        }

        private void chkFromValue_NormalPercent_CheckedChanged(object sender, EventArgs e)
        {
            numFromValue_NormalPercent.Enabled = chkFromValue_NormalPercent.Checked;
        }

        private void chkToValue_NormalPercent_CheckedChanged(object sender, EventArgs e)
        {
            numToValue_NormalPercent.Enabled = chkToValue_NormalPercent.Checked;
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
