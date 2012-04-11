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
            dtpkBloodTakingDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
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
                if (txtBookingMonitorCompany.Text.Trim() == string.Empty)
                {
                    MsgBox.Show(this.Text, "Vui lòng nhập tên công ty.", IconType.Information);
                    txtBookingMonitorCompany.Focus();
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
                if (txtBloodTakingCompany.Text.Trim() == string.Empty)
                {
                    MsgBox.Show(this.Text, "Vui lòng nhập tên công ty.", IconType.Information);
                    txtBloodTakingCompany.Focus();
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
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
            
        }
        #endregion
    }
}
