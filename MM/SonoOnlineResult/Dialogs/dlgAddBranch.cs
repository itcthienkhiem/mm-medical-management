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

namespace SonoOnlineResult.Dialogs
{
    public partial class dlgAddBranch : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private DataRow _dataRow = null;
        private int _branchKey = 0;
        private string _branchName = string.Empty;
        private string _address = string.Empty;
        private string _telephone = string.Empty;
        private string _fax = string.Empty;
        private string _website = string.Empty;
        private string _notes = string.Empty;
        #endregion

        #region Constructor
        public dlgAddBranch()
        {
            InitializeComponent();
        }

        public dlgAddBranch(DataRow dataRow) : this()
        {
            _isNew = false;
            this.Text = "Edit Branch";
            _dataRow = dataRow;
        }
        #endregion

        #region Properties
        public int BranchKey
        {
            get { return _branchKey; }
        }

        public string BranchName
        {
            get { return _branchName; }
        }

        public string Address
        {
            get { return _address; }
        }

        public string Telephone
        {
            get { return _telephone; }
        }

        public string Fax
        {
            get { return _fax; }
        }

        public string Website
        {
            get { return _website; }
        }

        public string Notes
        {
            get { return _notes; }
        }
        #endregion

        #region UI Commnad
        private void DisplayInfo()
        {
            txtBranchName.Text = _dataRow["BranchName"].ToString();
            txtAddress.Text = _dataRow["Address"].ToString();
            txtTelephone.Text = _dataRow["Telephone"].ToString();
            txtFax.Text = _dataRow["Fax"].ToString();
            txtWebsite.Text = _dataRow["Website"].ToString();
            txtNote.Text = _dataRow["Note"].ToString();
            _branchKey = Convert.ToInt32(_dataRow["BranchKey"]);
        }

        private bool CheckInfo()
        {
            if (txtBranchName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please input Branch Name.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBranchName.Focus();
                return false;
            }

            Result result = MySQL.CheckBranchExist(txtBranchName.Text, _branchKey);
            if (result.Error.Code != ErrorCode.EXIST && result.Error.Code != ErrorCode.NOT_EXIST)
            {
                MessageBox.Show(result.GetErrorAsString("MySQL.CheckBranchExist"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (result.Error.Code == ErrorCode.EXIST)
            {
                MessageBox.Show(string.Format("The Branch Name: '{0}' is exists.", txtBranchName.Text), 
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void SaveInfoAsThread()
        {
            try
            {
                _branchName = txtBranchName.Text;
                _address = txtAddress.Text;
                _telephone = txtTelephone.Text;
                _fax = txtFax.Text;
                _website = txtWebsite.Text;
                _notes = txtNote.Text;

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSaveInfoProc));
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

        private void OnSaveInfo()
        {
            try
            {
                Result result = MySQL.InsertBranch(_branchKey, _branchName, _address, _telephone, _fax, _website, _notes);
                if (result.IsOK)
                {
                    if (_isNew)
                        _branchKey = Convert.ToInt32(result.QueryResult);
                }
                else
                {
                    MessageBox.Show(result.GetErrorAsString("MySQL.InsertBranch"), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddBranch_Load(object sender, EventArgs e)
        {
            if (!_isNew) DisplayInfo();
        }

        private void dlgAddBranch_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
                else SaveInfoAsThread();
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
