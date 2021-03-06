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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using MM.Dialogs;
using MM.Bussiness;
using MM.Common;
using MM.Databasae;
using SpreadsheetGear;
using SpreadsheetGear.Advanced.Cells;
using System.Net;

namespace MM.Controls
{
    public partial class uSendSMS : uBase
    {
        #region Members
        private string _fileName = string.Empty;
        private bool _isAscending = true;
        private Dictionary<string, DataRow> _dictPatient = new Dictionary<string,DataRow>();
        private string _name = string.Empty;
        private int _type = 0;
        private DataTable _dtTemp = null;
        #endregion

        #region Constructor
        public uSendSMS()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnSendSMS.Enabled = AllowSendSMS;
            btnGuiSMSTuDo.Enabled = AllowSendSMS;

            guiSMSToolStripMenuItem.Enabled = AllowSendSMS;
            guiSMSTuDoToolStripMenuItem.Enabled = AllowSendSMS;
        }

        public void ClearData()
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
            }

            dgPatient.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                _name = txtSearchPatient.Text;
                if (_name.Trim() == string.Empty) _name = "*";
                else if (_name.Trim() == "*") _name = string.Empty;
                if (chkMaBenhNhan.Checked) _type = 1;
                else if (chkTheoSoDienThoai.Checked) _type = 2;
                else _type = 0;

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayPatientListProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        public override void SearchAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                _name = txtSearchPatient.Text;
                if (_name.Trim() == string.Empty) _name = "*";
                else if (_name.Trim() == "*") _name = string.Empty;
                if (chkMaBenhNhan.Checked) _type = 1;
                else if (chkTheoSoDienThoai.Checked) _type = 2;
                else _type = 0;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplayPatientList()
        {
            lock (ThisLock)
            {
                Result result = PatientBus.GetPatientWithMobiList(_name, _type);
                if (result.IsOK)
                {
                    dgPatient.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();

                        DataTable dt = result.QueryResult as DataTable;
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        UpdateChecked(dt);
                        dgPatient.DataSource = dt;

                        lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dt.Rows.Count);
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.GetPatientWithMobiList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientWithMobiList"));
                }
            }
        }

        private void UpdateChecked(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string key = row["PatientGUID"].ToString();
                if (_dictPatient.ContainsKey(key))
                    row["Checked"] = true;
            }
        }

        private void OnSendSMS()
        {
            List<DataRow> checkedRows = _dictPatient.Values.ToList();
            if (checkedRows.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn gửi SMS những bệnh nhân mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    dlgSelectTinNhanMau dlg = new dlgSelectTinNhanMau();
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        string tinNhanMau = dlg.TinNhanMau;
                        foreach (DataRow row in checkedRows)
                        {
                            string maBenhNhan = row["FileNum"].ToString();
                            string tenBenhNhan = row["FullName"].ToString();
                            string ngaySinh = row["DobStr"].ToString();
                            string gioiTinh = row["GenderAsStr"].ToString();
                            string diaChi = row["Address"].ToString();
                            string cmnd = row["IdentityCard"].ToString();
                            string mobile = row["Mobile"].ToString();
                            string email = row["Email"].ToString();

                            string noiDung = tinNhanMau.Replace("#MaBenhNhan#", maBenhNhan);
                            noiDung = noiDung.Replace("#TenBenhNhan#", tenBenhNhan);
                            noiDung = noiDung.Replace("#NgaySinh#", ngaySinh);
                            noiDung = noiDung.Replace("#GioiTinh#", gioiTinh);
                            noiDung = noiDung.Replace("#DiaChi#", diaChi);
                            noiDung = noiDung.Replace("#CMND#", cmnd);
                            noiDung = noiDung.Replace("#Mobile#", mobile);
                            noiDung = noiDung.Replace("#Email#", email);
                            noiDung = Utility.ConvertToUnSign3(noiDung);
                            noiDung = noiDung.Replace("#", "");
                            noiDung = noiDung.Replace("\n", "\r\n");

                            Stream resStream = null;
                            StreamReader sr = null;
                            try
                            {
                                string url = string.Format("http://sms.vietguys.biz/api/?u=vigor&pwd=ahhsw&from=VigorHEALTH&phone={0}&sms={1}",
                                mobile, noiDung);

                                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                                resStream = response.GetResponseStream();
                                sr = new StreamReader(resStream);
                                string responseFromServer = sr.ReadToEnd();
                                responseFromServer = responseFromServer.Replace("\t", "");

                                int result = 0;
                                SMSLog smsLog = new SMSLog();
                                smsLog.Ngay = DateTime.Now;
                                smsLog.NoiDung = noiDung;
                                smsLog.Mobile = mobile;
                                smsLog.PatientGUID = Guid.Parse(row["PatientGUID"].ToString());
                                smsLog.DocStaffGUID = Guid.Parse(Global.UserGUID);

                                if (int.TryParse(responseFromServer, out result))
                                {
                                    switch (result)
                                    {
                                        case -1:
                                            smsLog.Status = -1;
                                            smsLog.Notes = "Chưa nhập đầy đủ các tham số yêu cầu.";
                                            break;
                                        case -2:
                                            smsLog.Status = -2;
                                            smsLog.Notes = "Không thể kết nối đến máy chủ VIETGUYS, máy chủ đang bận.";
                                            break;
                                        case -3:
                                            smsLog.Status = -3;
                                            smsLog.Notes = "Thông tin xác nhận tài khoản chưa chính xác.";
                                            break;
                                        case -5:
                                            smsLog.Status = -5;
                                            smsLog.Notes = "Thông tin xác nhận tài khoản chưa chính xác.";
                                            break;
                                        case -4:
                                            smsLog.Status = -4;
                                            smsLog.Notes = "Tài khoản bị khóa.";
                                            break;
                                        case -6:
                                            smsLog.Status = -6;
                                            smsLog.Notes = "Tài khoản bị khóa.";
                                            break;
                                        case -7:
                                            smsLog.Status = -7;
                                            smsLog.Notes = "IP bị giới hạn truy cập, không được phép gửi từ IP này.";
                                            break;
                                        case -8:
                                            smsLog.Status = -8;
                                            smsLog.Notes = "Giá trị Gửi-từ-đâu chưa được phép sử dụng, vui lòng liên hệ với VIETGUYS để khai báo trước khi sử dụng.";
                                            break;
                                        case -9:
                                            smsLog.Status = -9;
                                            smsLog.Notes = "Tài khoản hết credits gửi tin.";
                                            break;
                                        case -10:
                                            smsLog.Status = -10;
                                            smsLog.Notes = "Số điện thoại người nhận chưa chính xác.";
                                            break;
                                        case -11:
                                            smsLog.Status = -11;
                                            smsLog.Notes = "Số điện thoại nằm trong danh sách Blacklist, là danh sách không muốn nhận tin nhắn.";
                                            break;
                                        case -12:
                                            smsLog.Status = -12;
                                            smsLog.Notes = "Tài khoản không đủ credits để thực hiện gửi tin nhắn.";
                                            break;
                                    }
                                }
                                else
                                {
                                    smsLog.Status = 0;
                                    smsLog.Notes = responseFromServer;
                                }

                                Result rs = SMSLogBus.InsertSMSLog(smsLog);
                                if (!rs.IsOK)
                                {
                                    MsgBox.Show(this.Text, rs.GetErrorAsString("SMSLogBus.InsertSMSLog"), IconType.Error);
                                    Utility.WriteToTraceLog(rs.GetErrorAsString("SMSLogBus.InsertSMSLog"));
                                    return;
                                }
                            }
                            catch (Exception e)
                            {
                                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                                Utility.WriteToTraceLog(e.Message);
                            }
                            finally
                            {
                                if (sr != null)
                                {
                                    sr.Close();
                                    sr = null;
                                }

                                if (resStream != null)
                                {
                                    resStream.Close();
                                    resStream = null;
                                }
                            }
                            
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần gửi SMS.", IconType.Information);
        }

        private void OnSendSMSTuDo()
        {
            dlgSendSMSTuDo dlg = new dlgSendSMSTuDo();
            dlg.ShowDialog(this);
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
                string patientGUID = row["PatientGUID"].ToString();
                if (chkChecked.Checked)
                {
                    if (!_dictPatient.ContainsKey(patientGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictPatient.Add(patientGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
                else
                {
                    if (_dictPatient.ContainsKey(patientGUID))
                    {
                        _dictPatient.Remove(patientGUID);

                        DataRow[] rows = _dtTemp.Select(string.Format("PatientGUID='{0}'", patientGUID));
                        if (rows != null && rows.Length > 0)
                            _dtTemp.Rows.Remove(rows[0]);
                    }
                }
            }
        }

        private void txtSearchPatient_TextChanged(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void txtSearchPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgPatient.Focus();

                if (dgPatient.SelectedRows != null && dgPatient.SelectedRows.Count > 0)
                {
                    int index = dgPatient.SelectedRows[0].Index;
                    if (index < dgPatient.RowCount - 1)
                    {
                        index++;
                        dgPatient.CurrentCell = dgPatient[1, index];
                        dgPatient.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgPatient.Focus();

                if (dgPatient.SelectedRows != null && dgPatient.SelectedRows.Count > 0)
                {
                    int index = dgPatient.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgPatient.CurrentCell = dgPatient[1, index];
                        dgPatient.Rows[index].Selected = true;
                    }
                }
            }
        }

        private void dgPatient_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                _isAscending = !_isAscending;

                DataTable dt = dgPatient.DataSource as DataTable;
                DataTable newDataSource = null;

                if (_isAscending)
                {
                    newDataSource = (from p in dt.AsEnumerable()
                                     orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                                     select p).CopyToDataTable();
                }
                else
                {
                    newDataSource = (from p in dt.AsEnumerable()
                                     orderby p.Field<string>("FirstName") descending, p.Field<string>("FullName") descending
                                     select p).CopyToDataTable();
                }
                
                dgPatient.DataSource = newDataSource;

                if (dt != null)
                {
                    dt.Rows.Clear();
                    dt.Clear();
                    dt = null;
                }
            }
            else
                _isAscending = false;
        }

        private void chkMaBenhNhan_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void dgPatient_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgPatient.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string patientGUID = row["PatientGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            if (isChecked)
            {
                if (!_dictPatient.ContainsKey(patientGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictPatient.Add(patientGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictPatient.ContainsKey(patientGUID))
                {
                    _dictPatient.Remove(patientGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("PatientGUID='{0}'", patientGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
                }
            }
        }

        private void chkTheoSoDienThoai_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            OnSendSMS();
        }

        private void guiSMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnSendSMS();
        }

        private void btnGuiSMSTuDo_Click(object sender, EventArgs e)
        {
            OnSendSMSTuDo();
        }

        private void guiSMSTuDoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnSendSMSTuDo();
        }
        #endregion

        #region Working Thread
        private void OnDisplayPatientListProc(object state)
        {
            try
            {
                OnDisplayPatientList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnSearchProc(object state)
        {
            try
            {
                OnDisplayPatientList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }
        #endregion
    }
}
