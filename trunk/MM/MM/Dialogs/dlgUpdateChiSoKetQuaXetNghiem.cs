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
        private string _binhThuong = string.Empty;
        private bool _isTongHop = false;
        #endregion

        #region Constructor
        public dlgUpdateChiSoKetQuaXetNghiem(DataRow drCTKQXN)
        {
            InitializeComponent();
            _drCTKQXN = drCTKQXN;
        }
        #endregion

        #region Properties
        public bool IsTongHop
        {
            get { return _isTongHop; }
            set { _isTongHop = value; }
        }

        public ChiTietKetQuaXetNghiem_Hitachi917 ChiTietKQXN
        {
            get { return _chiTietKQXN; }
        }

        public string BinhThuong
        {
            get { return _binhThuong; }
        }
        #endregion

        #region UI Command
        private void DisplayInfo()
        {
            if (!_isTongHop)
            {
                _chiTietKQXN.ChiTietKQXN_Hitachi917GUID = Guid.Parse(_drCTKQXN["ChiTietKQXN_Hitachi917GUID"].ToString());

                if (_drCTKQXN["Fullname"] != null && _drCTKQXN["Fullname"] != DBNull.Value)
                    txTenXetNghiem.Text = _drCTKQXN["Fullname"].ToString();

                numKetQua.Value = (Decimal)Convert.ToDouble(_drCTKQXN["TestResult"].ToString().Trim());
                _chiTietKQXN.TestNum = Convert.ToInt32(_drCTKQXN["TestNum"]);

                if ((_drCTKQXN["FromValue"] == null || _drCTKQXN["FromValue"] == DBNull.Value) &&
                    (_drCTKQXN["ToValue"] == null || _drCTKQXN["ToValue"] == DBNull.Value))
                {
                    chkFromValue.Enabled = false;
                    chkToValue.Enabled = false;
                }
                else if (_drCTKQXN["ToValue"] == null || _drCTKQXN["ToValue"] == DBNull.Value)
                {
                    chkFromValue.Checked = true;
                    numFromValue.Value = (Decimal)Convert.ToDouble(_drCTKQXN["FromValue"]);
                }
                else if (_drCTKQXN["FromValue"] == null || _drCTKQXN["FromValue"] == DBNull.Value)
                {
                    chkToValue.Checked = true;
                    numToValue.Value = (Decimal)Convert.ToDouble(_drCTKQXN["ToValue"]);
                }
                else
                {
                    chkFromValue.Checked = true;
                    numFromValue.Value = (Decimal)Convert.ToDouble(_drCTKQXN["FromValue"]);
                    chkToValue.Checked = true;
                    numToValue.Value = (Decimal)Convert.ToDouble(_drCTKQXN["ToValue"]);
                }

                if (_drCTKQXN["DonVi"] != null && _drCTKQXN["DonVi"] != DBNull.Value)
                    txtDonVi.Text = _drCTKQXN["DonVi"].ToString();

                if (_drCTKQXN["DoiTuong"] != null && _drCTKQXN["DoiTuong"] != DBNull.Value)
                    _chiTietKQXN.DoiTuong = Convert.ToByte(_drCTKQXN["DoiTuong"]);

                chkLamThem.Checked = Convert.ToBoolean(_drCTKQXN["LamThem"]);
            }
            else
            {
                _chiTietKQXN.ChiTietKQXN_Hitachi917GUID = Guid.Parse(_drCTKQXN["ChiTietKQXNGUID"].ToString());

                if (_drCTKQXN["Fullname"] != null && _drCTKQXN["Fullname"] != DBNull.Value)
                    txTenXetNghiem.Text = _drCTKQXN["Fullname"].ToString();

                numKetQua.Value = (Decimal)Convert.ToDouble(_drCTKQXN["TestResult"].ToString().Trim());
                _chiTietKQXN.TestNum = Convert.ToInt32(_drCTKQXN["TestNum"]);

                if ((_drCTKQXN["FromValue2"] == null || _drCTKQXN["FromValue2"] == DBNull.Value) &&
                    (_drCTKQXN["ToValue2"] == null || _drCTKQXN["ToValue2"] == DBNull.Value))
                {
                    chkFromValue.Enabled = false;
                    chkToValue.Enabled = false;
                }
                else if (_drCTKQXN["ToValue2"] == null || _drCTKQXN["ToValue2"] == DBNull.Value)
                {
                    chkFromValue.Checked = true;
                    numFromValue.Value = (Decimal)Convert.ToDouble(_drCTKQXN["FromValue2"]);
                }
                else if (_drCTKQXN["FromValue2"] == null || _drCTKQXN["FromValue2"] == DBNull.Value)
                {
                    chkToValue.Checked = true;
                    numToValue.Value = (Decimal)Convert.ToDouble(_drCTKQXN["ToValue2"]);
                }
                else
                {
                    chkFromValue.Checked = true;
                    numFromValue.Value = (Decimal)Convert.ToDouble(_drCTKQXN["FromValue2"]);
                    chkToValue.Checked = true;
                    numToValue.Value = (Decimal)Convert.ToDouble(_drCTKQXN["ToValue2"]);
                }

                if (_drCTKQXN["DonVi2"] != null && _drCTKQXN["DonVi2"] != DBNull.Value)
                    txtDonVi.Text = _drCTKQXN["DonVi2"].ToString();

                if (_drCTKQXN["DoiTuong2"] != null && _drCTKQXN["DoiTuong2"] != DBNull.Value)
                    _chiTietKQXN.DoiTuong = Convert.ToByte(_drCTKQXN["DoiTuong2"]);

                chkLamThem.Checked = Convert.ToBoolean(_drCTKQXN["LamThem"]);
            }
        }

        private bool CheckInfo()
        {
            if (chkFromValue.Enabled == true && chkToValue.Enabled == true)
            {
                if (!chkFromValue.Checked && !chkToValue.Checked)
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
                    _chiTietKQXN.TestResult = numKetQua.Value.ToString();
                    if (chkFromValue.Checked)
                        _chiTietKQXN.FromValue = (double)numFromValue.Value;

                    if (chkToValue.Checked)
                        _chiTietKQXN.ToValue = (double)numToValue.Value;

                    _chiTietKQXN.DonVi = txtDonVi.Text;

                    _chiTietKQXN.LamThem = chkLamThem.Checked;

                    Result result = XetNghiem_Hitachi917Bus.UpdateChiSoKetQuaXetNghiem(_chiTietKQXN);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("XetNghiem_Hitachi917Bus.UpdateChiSoKetQuaXetNghiem"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_Hitachi917Bus.UpdateChiSoKetQuaXetNghiem"));
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    }
                    else
                        _binhThuong = result.QueryResult.ToString();
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

        private void chkFromValue_CheckedChanged(object sender, EventArgs e)
        {
            numFromValue.Enabled = chkFromValue.Checked;
        }

        private void chkToValue_CheckedChanged(object sender, EventArgs e)
        {
            numToValue.Enabled = chkToValue.Checked;
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
