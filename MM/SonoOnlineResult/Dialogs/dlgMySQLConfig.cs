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
using MySql.Data.MySqlClient;
using MM.Common;


namespace SonoOnlineResult.Dialogs
{
    public partial class dlgMySQLConfig : Form
    {
        #region Constructor
        public dlgMySQLConfig()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private bool CheckInfo()
        {
            if (txtServerName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter server.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtServerName.Focus();
                return false;
            }

            if (txtDatabase.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter database.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDatabase.Focus();
                return false;
            }

            if (txtUserName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter user.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserName.Focus();
                return false;
            }

            if (txtPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter password.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Focus();
                return false;
            }

            return true;
        }

        private void SaveMySQLConfig()
        {
            Global.MySQLConnectionInfo.Server = txtServerName.Text;
            Global.MySQLConnectionInfo.Database = txtDatabase.Text;
            Global.MySQLConnectionInfo.User = txtUserName.Text;
            Global.MySQLConnectionInfo.Password = txtPassword.Text;
            Global.MySQLConnectionInfo.Serialize(Global.MySQLConnectionInfoPath);
        }
        #endregion

        #region Window Event Handlers
        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (!CheckInfo()) return;

            MySQLConnectionInfo connectionInfo = new MySQLConnectionInfo();
            connectionInfo.Server = txtServerName.Text;
            connectionInfo.Database = txtDatabase.Text;
            connectionInfo.User = txtUserName.Text;
            connectionInfo.Password = txtPassword.Text;

            try
            {
                MySQLHelper._connectionString = connectionInfo.ConnectionString;
                MySqlConnection cnn = MySQLHelper.CreateConnection();
                cnn.Close();
                cnn.Dispose();
                cnn = null;
                MessageBox.Show("Connect to MySQL successfully.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dlgMySQLConfig_Load(object sender, EventArgs e)
        {
            txtServerName.Text = Global.MySQLConnectionInfo.Server;
            txtDatabase.Text = Global.MySQLConnectionInfo.Database;
            txtUserName.Text = Global.MySQLConnectionInfo.User;
            txtPassword.Text = Global.MySQLConnectionInfo.Password;
        }

        private void dlgMySQLConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo())
                    e.Cancel = true;
                else
                    SaveMySQLConfig();
            }
        }
        #endregion
    }
}
