using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgDanhSachNhanVien : dlgBase
    {
        #region Members
        private string _contractGUID = string.Empty;
        private int _type = 0; //0: Chua kham; 1: Kham chua du; 2: Kham day du
        #endregion

        #region Constructor
        public dlgDanhSachNhanVien(string contractGUID, int type)
        {
            InitializeComponent();
            _contractGUID = contractGUID;
            _type = type;
        }
        #endregion

        #region UI Command
        private void OnDisplayInfo()
        {
            Result result = CompanyContractBus.GetDanhSachNhanVien(_contractGUID, _type);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgDSNV.DataSource = result.QueryResult;
                    RefreshNo();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyContractBus.GetDanhSachNhanVien"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetDanhSachNhanVien"));
            }
        }

        private void RefreshNo()
        {
            int index = 1;
            foreach (DataGridViewRow row in dgDSNV.Rows)
            {
                row.Cells["STT"].Value = index++;
            }
        }

        private void DisplayInfoAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayInfoProc));
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
        #endregion

        #region Window Event Handlers
        private void dlgDanhSachNhanVien_Load(object sender, EventArgs e)
        {
            DisplayInfoAsThread();
        }
        #endregion

        #region Working Thread
        private void OnDisplayInfoProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayInfo();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion
    }
}
