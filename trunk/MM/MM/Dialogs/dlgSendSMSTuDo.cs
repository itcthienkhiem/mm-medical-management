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
using System.IO;
using System.Net;

namespace MM.Dialogs
{
    public partial class dlgSendSMSTuDo : dlgBase
    {
        #region Members

        #endregion

        #region Constructor
        public dlgSendSMSTuDo()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public string TinNhanMau
        {
            get
            {
                if (dgTinNhanMau.SelectedRows == null || dgTinNhanMau.SelectedRows.Count <= 0) return string.Empty;
                DataRow row = (dgTinNhanMau.SelectedRows[0].DataBoundItem as DataRowView).Row;
                return row["NoiDung"].ToString();
            }
        }
        #endregion

        #region UI Command
        public void DisplayAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayTinNhanMauListProc));
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

        private void OnDisplayTinNhanMauList()
        {
            Result result = TinNhanMauBus.GetTinNhanMauDaDuyetList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgTinNhanMau.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("TinNhanMauBus.GetTinNhanMauList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("TinNhanMauBus.GetTinNhanMauList"));
            }
        }

        private bool CheckInfo()
        {
            if (txtSoDienThoai.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập số điện thoại người nhận tin nhắn.", IconType.Information);
                txtSoDienThoai.Focus();
                return false;
            }

            if (dgTinNhanMau.SelectedRows == null || dgTinNhanMau.SelectedRows.Count <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn 1 tin nhắn mẫu", IconType.Information);
                return false;
            }

            return true;
        }

        private void OnSendSMS()
        {
            string strMobile = txtSoDienThoai.Text.Trim().Replace(";", ",");
            string[] mobileList = strMobile.Split(",".ToCharArray());

            string tinNhanMau = this.TinNhanMau;
            tinNhanMau = Utility.ConvertToUnSign3(tinNhanMau);
            tinNhanMau = tinNhanMau.Replace("#", "");
            tinNhanMau = tinNhanMau.Replace("\n", "\r\n");
            foreach (string mobile in mobileList)
            {
                if (mobile.Trim() == string.Empty) continue;
                Stream resStream = null;
                StreamReader sr = null;
                try
                {
                    string url = string.Format("http://sms.vietguys.biz/api/?u=vigor&pwd=ahhsw&from=VigorHEALTH&phone={0}&sms={1}",
                    mobile, tinNhanMau);

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    resStream = response.GetResponseStream();
                    sr = new StreamReader(resStream);
                    string responseFromServer = sr.ReadToEnd();
                    responseFromServer = responseFromServer.Replace("\t", "");

                    int result = 0;
                    SMSLog smsLog = new SMSLog();
                    smsLog.Ngay = DateTime.Now;
                    smsLog.NoiDung = tinNhanMau;
                    smsLog.Mobile = mobile;
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
        #endregion

        #region Window Event Handlers
        private void dlgSendSMSTuDo_Load(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void dlgSendSMSTuDo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
                else
                {
                    if (MsgBox.Question(this.Text, "Bạn thật sự muốn gửi tin nhắn này ?") == System.Windows.Forms.DialogResult.Yes)
                        OnSendSMS();
                    else
                        e.Cancel = true;
                }
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayTinNhanMauListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayTinNhanMauList();
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
        #endregion
    }
}
