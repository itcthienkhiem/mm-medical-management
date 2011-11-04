using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using MM.Controls;
using MM.Dialogs;

namespace MM
{
    public partial class MainForm : Form
    {
        #region Members
        
        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();
        }   
        #endregion

        #region UI Command
        private void ExcuteCmd(string cmd)
        {
            Cursor.Current = Cursors.WaitCursor;
            switch (cmd)
            {
                case "Database Configuration":
                    OnDatabaseConfig();
                    break;

                case "Exit":
                    OnExit();
                    break;

                case "Services List":
                    OnServicesList();
                    break;

                case "Patient List":
                    OnPatientList();
                    break;

                case "Open Patient":
                    OnOpenPatient();
                    break;

                case "Doctor List":
                    OnDoctorList();
                    break;

                case "Help":
                    OnHelp();
                    break;

                case "About":
                    OnAbout();
                    break;
            }
        }

        private void OnDoctorList()
        {
            
        }

        private void OnDatabaseConfig()
        {

        }

        private void OnExit()
        {
            this.Close();
        }

        private void OnServicesList()
        {

        }

        private void OnPatientList()
        {

        }

        private void OnOpenPatient()
        {

        }

        private void OnHelp()
        {

        }

        private void OnAbout()
        {

        }
        #endregion

        #region Window Event Handlers
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void _mainToolbar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string cmd = e.ClickedItem.Tag as string;
            if (cmd == null || cmd == string.Empty) return;
            ExcuteCmd(cmd);
        }

        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            string cmd = (sender as ToolStripMenuItem).Tag as string;
            if (cmd == null || cmd == string.Empty) return;
            ExcuteCmd(cmd);
        }
        #endregion
    }
}
