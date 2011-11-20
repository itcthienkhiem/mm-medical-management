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
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uCompanyList : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uCompanyList()
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
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayCompanyListProc));
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

        public void ClearData()
        {
            dgCompany.DataSource = null;
        }

        private void OnDisplayCompanyList()
        {
            Result result = CompanyBus.GetCompanyList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgCompany.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyBus.GetCompanyList"));
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.GetCompanyList"));
            }
        }

        private void OnAddCompany()
        {
            dlgAddCompany dlg = new dlgAddCompany();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DataTable dt = dgCompany.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["CompanyGUID"] = dlg.Company.CompanyGUID.ToString();
                newRow["MaCty"] = dlg.Company.MaCty;
                newRow["TenCty"] = dlg.Company.TenCty;
                newRow["DiaChi"] = dlg.Company.DiaChi;
                newRow["Dienthoai"] = dlg.Company.Dienthoai;
                newRow["Fax"] = dlg.Company.Fax;
                newRow["Website"] = dlg.Company.Website;

                if (dlg.Company.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.Company.CreatedDate;

                if (dlg.Company.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.Company.CreatedBy.ToString();

                if (dlg.Company.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.Company.UpdatedDate;

                if (dlg.Company.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.Company.UpdatedBy.ToString();

                if (dlg.Company.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.Company.DeletedDate;

                if (dlg.Company.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.Company.DeletedBy.ToString();

                newRow["Status"] = dlg.Company.Status;
                dt.Rows.Add(newRow);
            }
        }

        private void OnEditCompany()
        {
            if (dgCompany.SelectedRows == null || dgCompany.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 công ty.");
                return;
            }

            DataRow drCom = (dgCompany.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddCompany dlg = new dlgAddCompany(drCom);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drCom["MaCty"] = dlg.Company.MaCty;
                drCom["TenCty"] = dlg.Company.TenCty;
                drCom["DiaChi"] = dlg.Company.DiaChi;
                drCom["Dienthoai"] = dlg.Company.Dienthoai;
                drCom["Fax"] = dlg.Company.Fax;
                drCom["Website"] = dlg.Company.Website;

                if (dlg.Company.CreatedDate.HasValue)
                    drCom["CreatedDate"] = dlg.Company.CreatedDate;

                if (dlg.Company.CreatedBy.HasValue)
                    drCom["CreatedBy"] = dlg.Company.CreatedBy.ToString();

                if (dlg.Company.UpdatedDate.HasValue)
                    drCom["UpdatedDate"] = dlg.Company.UpdatedDate;

                if (dlg.Company.UpdatedBy.HasValue)
                    drCom["UpdatedBy"] = dlg.Company.UpdatedBy.ToString();

                if (dlg.Company.DeletedDate.HasValue)
                    drCom["DeletedDate"] = dlg.Company.DeletedDate;

                if (dlg.Company.DeletedBy.HasValue)
                    drCom["DeletedBy"] = dlg.Company.DeletedBy.ToString();

                drCom["Status"] = dlg.Company.Status;
            }
        }

        private void OnDeleteCompany()
        {
            List<string> deletedComList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgCompany.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedComList.Add(row["CompanyGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedComList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những công ty mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = CompanyBus.DeleteCompany(deletedComList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyBus.DeleteCompany"));
                        Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.DeleteCompany"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những công ty cần xóa.");
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgCompany.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddCompany();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditCompany();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteCompany();
        }

        private void dgCompany_DoubleClick(object sender, EventArgs e)
        {
            OnEditCompany();
        }
        #endregion

        #region Working Thread
        private void OnDisplayCompanyListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayCompanyList();
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
