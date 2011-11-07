using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgAddDocStaff : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private Contact _contact = new Contact();
        private DocStaff _docStaff = new DocStaff();
        #endregion

        #region Constructor
        public dlgAddDocStaff()
        {
            InitializeComponent();
        }

        public dlgAddDocStaff(DataRow drDocStaff)
        {
            InitializeComponent();
            _isNew = false;
            this.Text = "Sửa bác sĩ";
            DisplayInfo(drDocStaff);
        }

        private void DisplayInfo(DataRow drDocStaff)
        {
            
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private bool CheckInfo()
        {

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
                MsgBox.Show(this.Text, e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnSaveInfo()
        {

        }
        #endregion

        #region Window Event Handlers
        private void dlgAddDocStaff_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
        }
        #endregion

        #region Working Thread
        private void OnSaveInfoProc(object state)
        {
            try
            {
                OnSaveInfo();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion
    }
}
