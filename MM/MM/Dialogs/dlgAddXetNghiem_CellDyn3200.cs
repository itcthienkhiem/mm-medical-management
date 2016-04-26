/* Copyright (c) 2016, Cocosoft Inc.
 All rights reserved.
 http://www.Cocosofttech.com

 This file is part of the LIS open source project.

 The LIS  open source project is free software: you can
 redistribute it and/or modify it under the terms of the GNU General Public
 License as published by the Free Software Foundation, either version 3 of the
 License, or (at your option) any later version.

 The ClearCanvas LIS open source project is distributed in the hope that it
 will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
 Public License for more details.

 You should have received a copy of the GNU General Public License along with
 the LIS open source project.  If not, see
 <http://www.gnu.org/licenses/>.
*/
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
    public partial class dlgAddXetNghiem_CellDyn3200 : dlgBase
    {
        #region Members
        private DataRow _drXetNghiem = null;
        private XetNghiem_CellDyn3200 _xetNghiem = new XetNghiem_CellDyn3200();
        #endregion

        #region Constructor
        public dlgAddXetNghiem_CellDyn3200(DataRow drXetNghiem)
        {
            InitializeComponent();
            _drXetNghiem = drXetNghiem;
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void DisplayInfo()
        {
            try
            {
                _xetNghiem.XetNghiemGUID = Guid.Parse(_drXetNghiem["XetNghiemGUID"].ToString());
                txtTenXetNghiem.Text = _drXetNghiem["Fullname"].ToString();

                if (_drXetNghiem["FromValue"] != null && _drXetNghiem["FromValue"] != DBNull.Value)
                {
                    chkFromValue_Normal.Checked = true;
                    numFromValue_Normal.Value = (Decimal)Convert.ToDouble(_drXetNghiem["FromValue"]);
                }

                if (_drXetNghiem["ToValue"] != null && _drXetNghiem["ToValue"] != DBNull.Value)
                {
                    chkToValue_Normal.Checked = true;
                    numToValue_Normal.Value = (Decimal)Convert.ToDouble(_drXetNghiem["ToValue"]);
                }

                if (txtTenXetNghiem.Text != "LYM" && txtTenXetNghiem.Text != "BASO" &&
                    txtTenXetNghiem.Text != "MONO" && txtTenXetNghiem.Text != "EOS" &&
                    txtTenXetNghiem.Text != "NEU")
                {
                    chkFromValue_NormalPercent.Enabled = false;
                    numFromValue_NormalPercent.Enabled = false;
                    chkToValue_NormalPercent.Enabled = false;
                    numToValue_NormalPercent.Enabled = false;
                }
                else
                {
                    if (_drXetNghiem["FromPercent"] != null && _drXetNghiem["FromPercent"] != DBNull.Value)
                    {
                        chkFromValue_NormalPercent.Checked = true;
                        numFromValue_NormalPercent.Value = (Decimal)Convert.ToDouble(_drXetNghiem["FromPercent"]);
                    }

                    if (_drXetNghiem["ToPercent"] != null && _drXetNghiem["ToPercent"] != DBNull.Value)
                    {
                        chkToValue_NormalPercent.Checked = true;
                        numToValue_NormalPercent.Value = (Decimal)Convert.ToDouble(_drXetNghiem["ToPercent"]);
                    }
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
            if (!chkFromValue_Normal.Checked && !chkToValue_Normal.Checked)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập chỉ số xét nghiệm.", IconType.Information);
                chkFromValue_Normal.Focus();
                return false;
            }

            if ((txtTenXetNghiem.Text == "LYM" || txtTenXetNghiem.Text == "BASO" || txtTenXetNghiem.Text == "MONO" ||
                txtTenXetNghiem.Text == "EOS" || txtTenXetNghiem.Text == "NEU") && !chkFromValue_NormalPercent.Checked && !chkToValue_NormalPercent.Checked)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập chỉ số % xét nghiệm.", IconType.Information);
                chkFromValue_NormalPercent.Focus();
                return false;
            }

            return true;
        }

        private void SaveAsThread()
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
                _xetNghiem.UpdatedDate = DateTime.Now;
                _xetNghiem.UpdatedBy = Guid.Parse(Global.UserGUID);

                MethodInvoker method = delegate
                {
                    _xetNghiem.TenXetNghiem = txtTenXetNghiem.Text;
                    _xetNghiem.FullName = txtTenXetNghiem.Text;

                    if (chkFromValue_Normal.Checked)
                        _xetNghiem.FromValue = (double)numFromValue_Normal.Value;

                    if (chkToValue_Normal.Checked)
                        _xetNghiem.ToValue = (double)numToValue_Normal.Value;

                    if (chkFromValue_NormalPercent.Enabled && chkFromValue_NormalPercent.Checked)
                        _xetNghiem.FromPercent = (double)numFromValue_NormalPercent.Value;

                    if (chkToValue_NormalPercent.Enabled && chkToValue_NormalPercent.Checked)
                        _xetNghiem.ToPercent = (double)numToValue_NormalPercent.Value;

                    Result result = XetNghiem_CellDyn3200Bus.UpdateXetNghiem(_xetNghiem);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("XetNghiem_CellDyn3200Bus.UpdateXetNghiem"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_CellDyn3200Bus.UpdateXetNghiem"));
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

        private void dlgAddXetNghiem_CellDyn3200_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        }

        private void dlgAddXetNghiem_CellDyn3200_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
                else SaveAsThread();
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
