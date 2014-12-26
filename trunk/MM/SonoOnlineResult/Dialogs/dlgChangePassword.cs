using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using System.Threading;

namespace SonoOnlineResult.Dialogs
{
    public partial class dlgChangePassword : dlgBase
    {
        #region Members
        private string _newPassword = string.Empty;
        #endregion

        #region Constructor
        public dlgChangePassword()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private bool CheckInfo()
        {
            if (txtCurrentPassword.Text != Global.Password)
            {
                MessageBox.Show("The current password is incorrect. Please re-enter.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCurrentPassword.Focus();
                return false;
            }

            if (txtNewPassword.Text == txtCurrentPassword.Text)
            {
                MessageBox.Show("The new password must not be identical to the old password. Please re-enter.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNewPassword.Focus();
                return false;
            }

            if (!Utility.IsValidPassword(txtNewPassword.Text))
            {
                MessageBox.Show("The new password is invalid (4-12 characters). Please re-enter.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNewPassword.Focus();
                return false;
            }

            if (txtNewPassword.Text != txtConfirmNewPassword.Text)
            {
                MessageBox.Show("The confirm password does not match the new password entered. Please re-enter.",
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtConfirmNewPassword.Focus();
                return false;
            }

            return true;
        }

        private void ChangePasswordAsThread()
        {
            try
            {
                _newPassword = txtNewPassword.Text;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnChangePasswordProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnChangePassword()
        {
            Result result = MySQL.ChangePassword(Global.Username, _newPassword);
            if (result.IsOK)
            {
                Global.Password = _newPassword;
                MessageBox.Show("The password was changed successfully.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(result.GetErrorAsString("MySQL.ChangePassword"),
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgChangePassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    ChangePasswordAsThread();
                else
                    e.Cancel = true;
            }
        }
        #endregion

        #region Working Thread
        private void OnChangePasswordProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnChangePassword();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion
    }
}
