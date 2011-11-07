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
    public partial class uServicesList : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uServicesList()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        public void DisplayAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayServicesListProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplayServicesList()
        {
            Result result = ServicesBus.GetServicesList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgService.DataSource = result.QueryResult; 
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServicesBus.GetServicesList"));
                Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.GetServicesList"));
            }
        }

        private void OnAddService()
        {
            dlgAddServices dlg = new dlgAddServices();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = dgService.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["ServiceGUID"] = dlg.Service.ServiceGUID.ToString();
                newRow["Code"] = dlg.Service.Code;
                newRow["Name"] = dlg.Service.Name;
                newRow["Price"] = dlg.Service.Price;
                newRow["Description"] = dlg.Service.Description;
                dt.Rows.Add(newRow);
            }
        }

        private void OnEditService()
        {
            if (dgService.SelectedRows == null || dgService.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 dịch vụ.");
                return;
            }

            DataRow drService = (dgService.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddServices dlg = new dlgAddServices(drService);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drService["Code"] = dlg.Service.Code;
                drService["Name"] = dlg.Service.Name;
                drService["Price"] = dlg.Service.Price;
                drService["Description"] = dlg.Service.Description;
            }
        }

        private void OnDeleteService()
        {
            List<Service> deletedServiceList = new List<Service>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgService.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    Service srv = new Service();
                    srv.ServiceGUID = Guid.Parse(row["ServiceGUID"].ToString());
                    deletedServiceList.Add(srv);
                    deletedRows.Add(row);
                }
            }

            if (deletedServiceList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những dịch vụ mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = ServicesBus.DeleteServices(deletedServiceList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServicesBus.DeleteServices"));
                        Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.DeleteServices"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những dịch vụ cần xóa.");
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgService.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;                
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddService();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditService();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteService();
        }

        private void dgService_DoubleClick(object sender, EventArgs e)
        {
            OnEditService();
        }
        #endregion

        #region Working Thread
        private void OnDisplayServicesListProc(object state)
        {
            try
            {
                //Thread.Sleep(1000);
                OnDisplayServicesList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message);
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
