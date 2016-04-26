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
    public partial class dlgEditBooking : dlgBase
    {
        #region Members
        private DataRow _drBooking = null;
        private Booking _booking = new Booking();
        private BookingType _bookingType = BookingType.Monitor;
        #endregion

        #region Constructor
        public dlgEditBooking(DataRow drBooking)
        {
            InitializeComponent();
            _drBooking = drBooking;
            cboBookingMonitorInOut.SelectedIndex = 0;
            cboBloodTakingInOut.SelectedIndex = 0;
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
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

        private void DisplayInfo(DataRow drBooking)
        {
            try
            {
                _bookingType = (BookingType)Convert.ToInt32(drBooking["BookingType"]);
                if (_bookingType == BookingType.Monitor)
                {
                    dtpkBookingMonitorDate.Value = Convert.ToDateTime(drBooking["BookingDate"]);
                    cboBookingMonitorCompany.Text = drBooking["Company"].ToString();
                    numMorning.Value = Convert.ToInt32(drBooking["MorningCount"]);
                    numAfternoon.Value = Convert.ToInt32(drBooking["AfternoonCount"]);
                    numEvening.Value = Convert.ToInt32(drBooking["EveningCount"]);

                    if (drBooking["InOut"].ToString() == "IN" || drBooking["InOut"].ToString() == "OUT")
                        cboBookingMonitorInOut.Text = drBooking["InOut"].ToString();
                    else
                        cboBookingMonitorInOut.SelectedIndex = 1;

                    gbBloodTaking.Visible = false;
                }
                else
                {
                    gbBookingMonitor.Visible = false;
                    Size size = new Size(345, 194);
                    this.Size = size;

                    btnOK.Location = new Point(92, 136);
                    btnCancel.Location = new Point(171, 136);

                    dtpkBloodTakingDate.Value = Convert.ToDateTime(drBooking["BookingDate"]);
                    cboBloodTakingCompany.Text = drBooking["Company"].ToString();
                    numPax.Value = Convert.ToInt32(drBooking["Pax"]);

                    if (drBooking["InOut"].ToString() == "IN" || drBooking["InOut"].ToString() == "OUT")
                        cboBloodTakingInOut.Text = drBooking["InOut"].ToString();
                    else
                        cboBloodTakingInOut.SelectedIndex = 1;
                }

                _booking.BookingGUID = Guid.Parse(drBooking["BookingGUID"].ToString());
                _booking.BookingDate = Convert.ToDateTime(drBooking["BookingDate"]);
                _booking.Company = drBooking["Company"].ToString();
                _booking.MorningCount = Convert.ToInt32(drBooking["MorningCount"]);
                _booking.AfternoonCount = Convert.ToInt32(drBooking["AfternoonCount"]);
                _booking.EveningCount = Convert.ToInt32(drBooking["EveningCount"]);
                _booking.Pax = Convert.ToInt32(drBooking["Pax"]);

                if (drBooking["CreatedDate"] != null && drBooking["CreatedDate"] != DBNull.Value)
                    _booking.CreatedDate = Convert.ToDateTime(drBooking["CreatedDate"]);

                if (drBooking["CreatedBy"] != null && drBooking["CreatedBy"] != DBNull.Value)
                    _booking.CreatedBy = Guid.Parse(drBooking["CreatedBy"].ToString());

                if (drBooking["UpdatedDate"] != null && drBooking["UpdatedDate"] != DBNull.Value)
                    _booking.UpdatedDate = Convert.ToDateTime(drBooking["UpdatedDate"]);

                if (drBooking["UpdatedBy"] != null && drBooking["UpdatedBy"] != DBNull.Value)
                    _booking.UpdatedBy = Guid.Parse(drBooking["UpdatedBy"].ToString());

                if (drBooking["DeletedDate"] != null && drBooking["DeletedDate"] != DBNull.Value)
                    _booking.DeletedDate = Convert.ToDateTime(drBooking["DeletedDate"]);

                if (drBooking["DeletedBy"] != null && drBooking["DeletedBy"] != DBNull.Value)
                    _booking.DeletedBy = Guid.Parse(drBooking["DeletedBy"].ToString());

                _booking.Status = Convert.ToByte(drBooking["Status"]);
            }
            catch (Exception ex)
            {
                MsgBox.Show(this.Text, ex.Message, IconType.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
        }

        private bool CheckInfo()
        {
            if (_bookingType == BookingType.Monitor)
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
            else
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
                    if (_bookingType == BookingType.Monitor)
                    {
                        _booking.UpdatedDate = DateTime.Now;
                        _booking.UpdatedBy = Guid.Parse(Global.UserGUID);
                        _booking.BookingDate = dtpkBookingMonitorDate.Value;
                        _booking.Company = cboBookingMonitorCompany.Text;
                        _booking.MorningCount = (int)numMorning.Value;
                        _booking.AfternoonCount = (int)numAfternoon.Value;
                        _booking.EveningCount = (int)numEvening.Value;
                        _booking.InOut = cboBookingMonitorInOut.Text;
                        _booking.BookingType = (byte)BookingType.Monitor;
                        _booking.Status = (byte)Status.Actived;
                    }
                    else
                    {
                        _booking.UpdatedDate = DateTime.Now;
                        _booking.UpdatedBy = Guid.Parse(Global.UserGUID);
                        _booking.BookingDate = dtpkBloodTakingDate.Value;
                        _booking.Company = cboBloodTakingCompany.Text;
                        _booking.Pax = (int)numPax.Value;
                        _booking.InOut = cboBloodTakingInOut.Text;
                        _booking.BookingType = (byte)BookingType.BloodTaking;
                        _booking.Status = (byte)Status.Actived;
                    }

                    Result result = BookingBus.UpdateBooking(_booking);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("BookingBus.UpdateBooking"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("BookingBus.UpdateBooking"));
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
        private void dlgEditBooking_Load(object sender, EventArgs e)
        {
            InitData();
            DisplayInfo(_drBooking);
        }

        private void dlgEditBooking_FormClosing(object sender, FormClosingEventArgs e)
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
