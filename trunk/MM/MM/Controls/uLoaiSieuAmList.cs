using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Controls
{
    public partial class uLoaiSieuAmList : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uLoaiSieuAmList()
        {
            InitializeComponent();
           
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayLoaiSieuListProc));
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
            }
        }

        private void OnDisplayLoaiSieuList()
        {
            Result result = SieuAmBus.GetLoaiSieuAmList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgLoaiSieuAm.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("SieuAmBus.GetLoaiSieuAmList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("SieuAmBus.GetLoaiSieuAmList"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dgLoaiSieuAm_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void raChung_CheckedChanged(object sender, EventArgs e)
        {
            btnBrowse_Chung.Enabled = raChung.Checked;
            if (raChung.Checked)
                pageChung.Text = "Mẫu báo cáo (Chung)";
        }

        private void raNamNu_CheckedChanged(object sender, EventArgs e)
        {
            btnBrowse_Nam.Enabled = raNamNu.Checked;
            btnBrowse_Nu.Enabled = raNamNu.Checked;

            if (raNamNu.Checked)
            {
                pageChung.Text = "Mẫu báo cáo (Nam)";
                TabPage pageNu = new TabPage("Mẫu báo cáo (Nữ)");
                pageNu.Padding = new System.Windows.Forms.Padding(3);
                pageNu.UseVisualStyleBackColor = true;

                tabMauBaoCao.TabPages.Add(pageNu);
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayLoaiSieuListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayLoaiSieuList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
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
