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
    public partial class dlgAddBooking : dlgBase
    {
        #region Members

        #endregion

        #region Constructor
        public dlgAddBooking()
        {
            InitializeComponent();
            dtpkBookingMonitorDate.Value = DateTime.Now;
            dtpkBloodTakingDate.Value = DateTime.Now;//new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
            cboBookingMonitorInOut.SelectedIndex = 0;
            cboBloodTakingInOut.SelectedIndex = 0;
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private bool CheckInfo()
        {
            if (!chkBookingMonitor.Checked && !chkBloodTaking.Checked)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn ít nhất 1 trong 2 loại đặt lịch hẹn: Booking Monitor và Blood Taking.", IconType.Information);
                chkBookingMonitor.Focus();
                return false;
            }

            if (chkBookingMonitor.Checked)
            {
                if (cboBookingMonitorCompany.Text.Trim() == string.Empty)
                {
                    MsgBox.Show(this.Text, "Vui lòng nhập tên công ty.", IconType.Information);
                    cboBookingMonitorCompany.Focus();
                    return false;
                }

                if (numMorning.Value == 0 && numAfternoon.Value == 0 && numEvening.Value == 0)
                {
                    MsgBox.Show(this.Text, "Vui lòng nhập số lượng người khám.", IconType.Information);
                    numMorning.Focus();
                    return false;
                }
            }

            if (chkBloodTaking.Checked)
            {
                if (cboBloodTakingCompany.Text.Trim() == string.Empty)
                {
                    MsgBox.Show(this.Text, "Vui lòng nhập tên công ty.", IconType.Information);
                    cboBloodTakingCompany.Focus();
                    return false;
                }
            }

            return true;
        }

        private void InitData()
        {
            Result result = BookingBus.GetCompanyList();
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                foreach (DataRow row in dt.Rows)
                {
                    cboBookingMonitorCompany.Items.Add(row["Company"].ToString());
                    cboBloodTakingCompany.Items.Add(row["Company"].ToString());
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("BookingBus.GetCompanyList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("BookingBus.GetCompanyList"));
            }
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
                List<Booking> bookingList = new List<Booking>();

                MethodInvoker method = delegate
                {
                    if (chkBookingMonitor.Checked)
                    {
                        Booking booking = new Booking();
                        booking.CreatedDate = DateTime.Now;
                        booking.CreatedBy = Guid.Parse(Global.UserGUID);
                        booking.BookingDate = dtpkBookingMonitorDate.Value;
                        booking.Company = cboBookingMonitorCompany.Text;
                        booking.MorningCount = (int)numMorning.Value;
                        booking.AfternoonCount = (int)numAfternoon.Value;
                        booking.EveningCount = (int)numEvening.Value;
                        booking.InOut = cboBookingMonitorInOut.Text;
                        booking.BookingType = (byte)BookingType.Monitor;
                        booking.Status = (byte)Status.Actived;
                        bookingList.Add(booking);
                    }

                    if (chkBloodTaking.Checked)
                    {
                        Booking booking = new Booking();
                        booking.CreatedDate = DateTime.Now;
                        booking.CreatedBy = Guid.Parse(Global.UserGUID);
                        booking.BookingDate = dtpkBloodTakingDate.Value;
                        booking.Company = cboBloodTakingCompany.Text;
                        booking.Pax = (int)numPax.Value;
                        booking.InOut = cboBloodTakingInOut.Text;
                        booking.BookingType = (byte)BookingType.BloodTaking;
                        booking.Status = (byte)Status.Actived;
                        bookingList.Add(booking);
                    }

                    Result result = BookingBus.InsertBooking(bookingList);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("BookingBus.InsertBooking"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("BookingBus.InsertBooking"));
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
        private void dlgAddBooking_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void chkBookingMonitor_CheckedChanged(object sender, EventArgs e)
        {
            gbBookingMonitor.Enabled = chkBookingMonitor.Checked;
        }

        private void chkBloodTaking_CheckedChanged(object sender, EventArgs e)
        {
            gbBloodTaking.Enabled = chkBloodTaking.Checked;
        }

        private void dlgAddBooking_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
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
