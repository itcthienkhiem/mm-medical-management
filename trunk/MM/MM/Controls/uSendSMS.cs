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

            guiSMSToolStripMenuItem.Enabled = AllowSendSMS;
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
                                if (int.TryParse(responseFromServer, out result))
                                {
                                    switch (result)
                                    {
                                        case -1:
                                            //MM.MsgBox.Show(Application.ProductName, "Send SMS Error: Chưa nhập đầy đủ các tham số yêu cầu.", IconType.Error);
                                            Utility.WriteToTraceLog(string.Format("Send SMS Error: Chưa nhập đầy đủ các tham số yêu cầu (Mã bệnh nhân: {0}, Tên bệnh nhân: {1}).", maBenhNhan, tenBenhNhan));
                                            break;
                                        case -2:
                                            //MM.MsgBox.Show(Application.ProductName, "Send SMS Error: Không thể kết nối đến máy chủ VIETGUYS, máy chủ đang bận.", IconType.Error);
                                            Utility.WriteToTraceLog(string.Format("Send SMS Error: Không thể kết nối đến máy chủ VIETGUYS, máy chủ đang bận (Mã bệnh nhân: {0}, Tên bệnh nhân: {1}).", maBenhNhan, tenBenhNhan));
                                            break;
                                        case -3:
                                        case -5:
                                            //MM.MsgBox.Show(Application.ProductName, "Send SMS Error: Thông tin xác nhận tài khoản chưa chính xác.", IconType.Error);
                                            Utility.WriteToTraceLog(string.Format("Send SMS Error: Thông tin xác nhận tài khoản chưa chính xác (Mã bệnh nhân: {0}, Tên bệnh nhân: {1}).", maBenhNhan, tenBenhNhan));
                                            break;
                                        case -4:
                                            //MM.MsgBox.Show(Application.ProductName, "Send SMS Error: Tài khoản bị khóa.", IconType.Error);
                                            Utility.WriteToTraceLog(string.Format("Send SMS Error: Tài khoản bị khóa (Mã bệnh nhân: {0}, Tên bệnh nhân: {1}).", maBenhNhan, tenBenhNhan));
                                            break;
                                        case -6:
                                            //MM.MsgBox.Show(Application.ProductName, "Send SMS Error: Chức năng API chưa kích hoạt.", IconType.Error);
                                            Utility.WriteToTraceLog(string.Format("Send SMS Error: Chức năng API chưa kích hoạt (Mã bệnh nhân: {0}, Tên bệnh nhân: {1}).", maBenhNhan, tenBenhNhan));
                                            break;
                                        case -7:
                                            //MM.MsgBox.Show(Application.ProductName, "Send SMS Error: IP bị giới hạn truy cập, không được phép gửi từ IP này.", IconType.Error);
                                            Utility.WriteToTraceLog(string.Format("Send SMS Error: IP bị giới hạn truy cập, không được phép gửi từ IP này (Mã bệnh nhân: {0}, Tên bệnh nhân: {1}).", maBenhNhan, tenBenhNhan));
                                            break;
                                        case -8:
                                            //MM.MsgBox.Show(Application.ProductName, "Send SMS Error: Giá trị Gửi-từ-đâu chưa được phép sử dụng, vui lòng liên hệ với VIETGUYS để khai báo trước khi sử dụng.", IconType.Error);
                                            Utility.WriteToTraceLog(string.Format("Send SMS Error: Giá trị Gửi-từ-đâu chưa được phép sử dụng, vui lòng liên hệ với VIETGUYS để khai báo trước khi sử dụng (Mã bệnh nhân: {0}, Tên bệnh nhân: {1}).", maBenhNhan, tenBenhNhan));
                                            break;
                                        case -9:
                                            //MM.MsgBox.Show(Application.ProductName, "Send SMS Error: Tài khoản hết credits gửi tin.", IconType.Error);
                                            Utility.WriteToTraceLog(string.Format("Send SMS Error: Tài khoản hết credits gửi tin (Mã bệnh nhân: {0}, Tên bệnh nhân: {1}).", maBenhNhan, tenBenhNhan));
                                            break;
                                        case -10:
                                            //MM.MsgBox.Show(Application.ProductName, "Send SMS Error: Số điện thoại người nhận chưa chính xác.", IconType.Error);
                                            Utility.WriteToTraceLog(string.Format("Send SMS Error: Số điện thoại người nhận chưa chính xác (Mã bệnh nhân: {0}, Tên bệnh nhân: {1}).", maBenhNhan, tenBenhNhan));
                                            break;
                                        case -11:
                                            //MM.MsgBox.Show(Application.ProductName, "Send SMS Error: Số điện thoại nằm trong danh sách Blacklist, là danh sách không muốn nhận tin nhắn.", IconType.Error);
                                            Utility.WriteToTraceLog(string.Format("Send SMS Error: Số điện thoại nằm trong danh sách Blacklist, là danh sách không muốn nhận tin nhắn (Mã bệnh nhân: {0}, Tên bệnh nhân: {1}).", maBenhNhan, tenBenhNhan));
                                            break;
                                        case -12:
                                            //MM.MsgBox.Show(Application.ProductName, "Send SMS Error: Tài khoản không đủ credits để thực hiện gửi tin nhắn.", IconType.Error);
                                            Utility.WriteToTraceLog(string.Format("Send SMS Error: Tài khoản không đủ credits để thực hiện gửi tin nhắn (Mã bệnh nhân: {0}, Tên bệnh nhân: {1}).", maBenhNhan, tenBenhNhan));
                                            break;
                                    }
                                }
                                else
                                {
                                    Utility.WriteToTraceLog(string.Format("Send SMS OK: {0} (Mã bệnh nhân: {1}, Tên bệnh nhân: {2}).", responseFromServer, maBenhNhan, tenBenhNhan));
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
