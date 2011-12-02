using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Dialogs;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Controls
{
    public partial class uReceiptList : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uReceiptList()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        public void ClearData()
        {
            //dgService.DataSource = null;
        }

        public void DisplayAsThread()
        {
            /*try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayServicesListProc));
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
            }*/
        }
        #endregion

        #region Window Event Handlers

        #endregion
    }
}
