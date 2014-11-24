using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using MM.Common;


namespace SonoOnlineResult
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
                MessageBox.Show("Connect to MySQL successfully.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
