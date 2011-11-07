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
    public partial class uDocStaffList : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uDocStaffList()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        public void ClearData()
        {
            dgDocStaff.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayDocStaffListProc));
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

        private void OnDisplayDocStaffList()
        {
            Result result = DocStaffBus.GetDocStaffList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgDocStaff.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
            }
        }

        private void OnAddDocStaff()
        {
            dlgAddDocStaff dlg = new dlgAddDocStaff();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = dgDocStaff.DataSource as DataTable;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["DocStaffGUID"] = dlg.DocStaff.DocStaffGUID.ToString();
                newRow["ContactGUID"] = dlg.Contact.ContactGUID.ToString();
                newRow["SurName"] = dlg.Contact.SurName;
                newRow["MiddleName"] = dlg.Contact.MiddleName;
                newRow["FirstName"] = dlg.Contact.FirstName;
                newRow["KnownAs"] = dlg.Contact.KnownAs;
                newRow["PreferredName"] = dlg.Contact.PreferredName;
                newRow["Gender"] = dlg.Contact.Gender;
                newRow["GenderAsStr"] = dlg.Contact.Gender == 0 ? "Nam" : "Nữ";
                newRow["Dob"] = dlg.Contact.Dob;
                newRow["IdentityCard"] = dlg.Contact.IdentityCard;
                newRow["HomePhone"] = dlg.Contact.HomePhone;
                newRow["WorkPhone"] = dlg.Contact.WorkPhone;
                newRow["Mobile"] = dlg.Contact.Mobile;
                newRow["Email"] = dlg.Contact.Email;
                newRow["FAX"] = dlg.Contact.FAX;
                newRow["Address"] = dlg.Contact.Address;
                newRow["Ward"] = dlg.Contact.Ward;
                newRow["District"] = dlg.Contact.District;
                newRow["City"] = dlg.Contact.City;
                newRow["Fullname"] = string.Format("{0} {1} {2}", dlg.Contact.SurName, dlg.Contact.MiddleName, dlg.Contact.FirstName);
                newRow["FullAddress"] = string.Format("{0} {1} {2} {3}", dlg.Contact.Address, dlg.Contact.Ward, dlg.Contact.District, dlg.Contact.City);

                newRow["Qualifications"] = dlg.DocStaff.Qualifications;
                newRow["SpecialityGUID"] = dlg.DocStaff.SpecialityGUID.ToString();
                newRow["WorkType"] = dlg.DocStaff.WorkType;
                newRow["StaffType"] = dlg.DocStaff.StaffType;
                dt.Rows.Add(newRow);
            }
        }

        private void OnEditDocStaff()
        {
            if (dgDocStaff.SelectedRows == null || dgDocStaff.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 bác sĩ.");
                return;
            }

            DataRow drDocStaff = (dgDocStaff.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddDocStaff dlg = new dlgAddDocStaff(drDocStaff);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drDocStaff["SurName"] = dlg.Contact.SurName;
                drDocStaff["MiddleName"] = dlg.Contact.MiddleName;
                drDocStaff["FirstName"] = dlg.Contact.FirstName;
                drDocStaff["KnownAs"] = dlg.Contact.KnownAs;
                drDocStaff["PreferredName"] = dlg.Contact.PreferredName;
                drDocStaff["Gender"] = dlg.Contact.Gender;
                drDocStaff["GenderAsStr"] = dlg.Contact.Gender == 0 ? "Nam" : "Nữ";
                drDocStaff["Dob"] = dlg.Contact.Dob;
                drDocStaff["IdentityCard"] = dlg.Contact.IdentityCard;
                drDocStaff["HomePhone"] = dlg.Contact.HomePhone;
                drDocStaff["WorkPhone"] = dlg.Contact.WorkPhone;
                drDocStaff["Mobile"] = dlg.Contact.Mobile;
                drDocStaff["Email"] = dlg.Contact.Email;
                drDocStaff["FAX"] = dlg.Contact.FAX;
                drDocStaff["Address"] = dlg.Contact.Address;
                drDocStaff["Ward"] = dlg.Contact.Ward;
                drDocStaff["District"] = dlg.Contact.District;
                drDocStaff["City"] = dlg.Contact.City;
                drDocStaff["Fullname"] = string.Format("{0} {1} {2}", dlg.Contact.SurName, dlg.Contact.MiddleName, dlg.Contact.FirstName);
                drDocStaff["FullAddress"] = string.Format("{0} {1} {2} {3}", dlg.Contact.Address, dlg.Contact.Ward, dlg.Contact.District, dlg.Contact.City);

                drDocStaff["Qualifications"] = dlg.DocStaff.Qualifications;
                drDocStaff["SpecialityGUID"] = dlg.DocStaff.SpecialityGUID.ToString();
                drDocStaff["WorkType"] = dlg.DocStaff.WorkType;
                drDocStaff["StaffType"] = dlg.DocStaff.StaffType;
            }
        }

        private void OnDeleteDocStaff()
        {
            List<string> deletedDocStaffList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgDocStaff.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedDocStaffList.Add(row["DocStaffGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedDocStaffList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những bác sĩ mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = DocStaffBus.DeleteDocStaff(deletedDocStaffList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("DocStaffBus.DeleteDocStaff"));
                        Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.DeleteDocStaff"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bác sĩ cần xóa.");
        }
        #endregion

        #region Window Event Handlers
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddDocStaff();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditDocStaff();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteDocStaff();
        }

        private void dgDocStaff_DoubleClick(object sender, EventArgs e)
        {
            OnEditDocStaff();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgDocStaff.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayDocStaffListProc(object state)
        {
            try
            {
                //Thread.Sleep(1000);
                OnDisplayDocStaffList();
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
