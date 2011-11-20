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
using MM.Bussiness;
using MM.Common;
using MM.Databasae;

namespace MM.Controls
{
    public partial class uPatientList : uBase
    {
        #region Members
        //private Color _defaultBackColor;
        private Color _highLightBackColor;
        private DataTable _dataSource = null;
        #endregion

        #region Constructor
        public uPatientList()
        {
            InitializeComponent();
            _highLightBackColor = Color.YellowGreen;
        }
        #endregion

        #region Properties
        public object DataSource
        {
            get
            {
                if (dgPatient.RowCount <= 0)
                    DisplayAsThread();

                return dgPatient.DataSource;
            }
        }
        #endregion

        #region UI Command
        public void ClearData()
        {
            dgPatient.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayPatientListProc));
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

        private void OnDisplayPatientList()
        {
            Result result = PatientBus.GetPatientList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    _dataSource = result.QueryResult as DataTable;
                    OnSearchPatient();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.GetPatientList"));
                Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientList"));
            }
        }

        private void OnAddPatient()
        {
            if (_dataSource == null) return;
            dlgAddPatient dlg = new dlgAddPatient();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = _dataSource;
                if (dt == null) return;
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["PatientGUID"] = dlg.Patient.PatientGUID.ToString();
                newRow["ContactGUID"] = dlg.Contact.ContactGUID.ToString();
                newRow["FileNum"] = dlg.Patient.FileNum;
                newRow["FullName"] = dlg.Contact.FullName;
                newRow["SurName"] = dlg.Contact.SurName;
                newRow["MiddleName"] = dlg.Contact.MiddleName;
                newRow["FirstName"] = dlg.Contact.FirstName;
                newRow["KnownAs"] = dlg.Contact.KnownAs;
                newRow["PreferredName"] = dlg.Contact.PreferredName;
                newRow["Gender"] = dlg.Contact.Gender;
                newRow["GenderAsStr"] = dlg.Contact.Gender == 0 ? "Nam" : "Nữ";
                newRow["DobStr"] = dlg.Contact.DobStr;
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
                newRow["Occupation"] = dlg.Contact.Occupation;

                if (dlg.Contact.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.Contact.CreatedDate;

                if (dlg.Contact.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.Contact.CreatedBy.ToString();

                if (dlg.Contact.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.Contact.UpdatedDate;

                if (dlg.Contact.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.Contact.UpdatedBy.ToString();

                if (dlg.Contact.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.Contact.DeletedDate;

                if (dlg.Contact.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.Contact.DeletedBy.ToString();

                //Patient History
                newRow["PatientHistoryGUID"] = dlg.PatientHistory.PatientHistoryGUID.ToString();
                newRow["Di_Ung_Thuoc"] = dlg.PatientHistory.Di_Ung_Thuoc.Value;
                if (dlg.PatientHistory.Di_Ung_Thuoc.Value)
                    newRow["Thuoc_Di_Ung"] = dlg.PatientHistory.Thuoc_Di_Ung;
                else
                    newRow["Thuoc_Di_Ung"] = string.Empty;

                newRow["Dot_Quy"] = dlg.PatientHistory.Dot_Quy.Value;
                newRow["Benh_Tim_Mach"] = dlg.PatientHistory.Benh_Tim_Mach.Value;
                newRow["Benh_Lao"] = dlg.PatientHistory.Benh_Lao.Value;
                newRow["Dai_Thao_Duong"] = dlg.PatientHistory.Dai_Thao_Duong.Value;
                newRow["Dai_Duong_Dang_Dieu_Tri"] = dlg.PatientHistory.Dai_Duong_Dang_Dieu_Tri.Value;
                newRow["Viem_Gan_B"] = dlg.PatientHistory.Viem_Gan_B.Value;
                newRow["Viem_Gan_C"] = dlg.PatientHistory.Viem_Gan_C.Value;
                newRow["Viem_Gan_Dang_Dieu_Tri"] = dlg.PatientHistory.Viem_Gan_Dang_Dieu_Tri.Value;
                newRow["Ung_Thu"] = dlg.PatientHistory.Ung_Thu.Value;
                if (dlg.PatientHistory.Ung_Thu.Value)
                    newRow["Co_Quan_Ung_Thu"] = dlg.PatientHistory.Co_Quan_Ung_Thu;
                else
                    newRow["Co_Quan_Ung_Thu"] = string.Empty;

                newRow["Dong_Kinh"] = dlg.PatientHistory.Dong_Kinh.Value;
                newRow["Hen_Suyen"] = dlg.PatientHistory.Hen_Suyen.Value;
                newRow["Benh_Khac"] = dlg.PatientHistory.Benh_Khac.Value;
                if (dlg.PatientHistory.Benh_Khac.Value)
                {
                    newRow["Benh_Gi"] = dlg.PatientHistory.Benh_Gi;
                    newRow["Thuoc_Dang_Dung"] = dlg.PatientHistory.Thuoc_Dang_Dung;
                }
                else
                {
                    newRow["Benh_Gi"] = string.Empty;
                    newRow["Thuoc_Dang_Dung"] = string.Empty;
                }

                newRow["Hut_Thuoc"] = dlg.PatientHistory.Hut_Thuoc.Value;
                newRow["Uong_Ruou"] = dlg.PatientHistory.Uong_Ruou.Value;
                newRow["Tinh_Trang_Gia_Dinh"] = dlg.PatientHistory.Tinh_Trang_Gia_Dinh;
                newRow["Chich_Ngua_Viem_Gan_B"] = dlg.PatientHistory.Chich_Ngua_Viem_Gan_B.Value;
                newRow["Chich_Ngua_Uon_Van"] = dlg.PatientHistory.Chich_Ngua_Uon_Van;
                newRow["Chich_Ngua_Cum"] = dlg.PatientHistory.Chich_Ngua_Cum;
                newRow["Dang_Co_Thai"] = dlg.PatientHistory.Dang_Co_Thai.Value;

                dt.Rows.Add(newRow);
            }

            OnSearchPatient();
        }

        private DataRow GetDataRow(string patientGUID)
        {
            DataRow[] rows = _dataSource.Select(string.Format("PatientGUID = '{0}'", patientGUID));
            if (rows == null || rows.Length <= 0) return null;

            return rows[0];
        }

        private void OnEditPatient()
        {
            if (_dataSource == null) return;

            if (dgPatient.SelectedRows == null || dgPatient.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 bệnh nhân.");
                return;
            }

            string patientGUID = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row["PatientGUID"].ToString();
            DataRow drPatient = GetDataRow(patientGUID);
            if (drPatient == null) return;

            dlgAddPatient dlg = new dlgAddPatient(drPatient);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                drPatient["FileNum"] = dlg.Patient.FileNum;
                drPatient["FullName"] = dlg.Contact.FullName;
                drPatient["SurName"] = dlg.Contact.SurName;
                drPatient["MiddleName"] = dlg.Contact.MiddleName;
                drPatient["FirstName"] = dlg.Contact.FirstName;
                drPatient["KnownAs"] = dlg.Contact.KnownAs;
                drPatient["PreferredName"] = dlg.Contact.PreferredName;
                drPatient["Gender"] = dlg.Contact.Gender;
                drPatient["GenderAsStr"] = dlg.Contact.Gender == 0 ? "Nam" : "Nữ";
                drPatient["DobStr"] = dlg.Contact.DobStr;
                drPatient["IdentityCard"] = dlg.Contact.IdentityCard;
                drPatient["HomePhone"] = dlg.Contact.HomePhone;
                drPatient["WorkPhone"] = dlg.Contact.WorkPhone;
                drPatient["Mobile"] = dlg.Contact.Mobile;
                drPatient["Email"] = dlg.Contact.Email;
                drPatient["FAX"] = dlg.Contact.FAX;
                drPatient["Address"] = dlg.Contact.Address;
                drPatient["Ward"] = dlg.Contact.Ward;
                drPatient["District"] = dlg.Contact.District;
                drPatient["City"] = dlg.Contact.City;
                drPatient["Occupation"] = dlg.Contact.Occupation;

                if (dlg.Contact.CreatedDate.HasValue)
                    drPatient["CreatedDate"] = dlg.Contact.CreatedDate;

                if (dlg.Contact.CreatedBy.HasValue)
                    drPatient["CreatedBy"] = dlg.Contact.CreatedBy.ToString();

                if (dlg.Contact.UpdatedDate.HasValue)
                    drPatient["UpdatedDate"] = dlg.Contact.UpdatedDate;

                if (dlg.Contact.UpdatedBy.HasValue)
                    drPatient["UpdatedBy"] = dlg.Contact.UpdatedBy.ToString();

                if (dlg.Contact.DeletedDate.HasValue)
                    drPatient["DeletedDate"] = dlg.Contact.DeletedDate;

                if (dlg.Contact.DeletedBy.HasValue)
                    drPatient["DeletedBy"] = dlg.Contact.DeletedBy.ToString();

                //Patient History
                drPatient["Di_Ung_Thuoc"] = dlg.PatientHistory.Di_Ung_Thuoc.Value;
                if (dlg.PatientHistory.Di_Ung_Thuoc.Value)
                    drPatient["Thuoc_Di_Ung"] = dlg.PatientHistory.Thuoc_Di_Ung;
                else
                    drPatient["Thuoc_Di_Ung"] = string.Empty;

                drPatient["Dot_Quy"] = dlg.PatientHistory.Dot_Quy.Value;
                drPatient["Benh_Tim_Mach"] = dlg.PatientHistory.Benh_Tim_Mach.Value;
                drPatient["Benh_Lao"] = dlg.PatientHistory.Benh_Lao.Value;
                drPatient["Dai_Thao_Duong"] = dlg.PatientHistory.Dai_Thao_Duong.Value;
                drPatient["Dai_Duong_Dang_Dieu_Tri"] = dlg.PatientHistory.Dai_Duong_Dang_Dieu_Tri.Value;
                drPatient["Viem_Gan_B"] = dlg.PatientHistory.Viem_Gan_B.Value;
                drPatient["Viem_Gan_C"] = dlg.PatientHistory.Viem_Gan_C.Value;
                drPatient["Viem_Gan_Dang_Dieu_Tri"] = dlg.PatientHistory.Viem_Gan_Dang_Dieu_Tri.Value;
                drPatient["Ung_Thu"] = dlg.PatientHistory.Ung_Thu.Value;
                if (dlg.PatientHistory.Ung_Thu.Value)
                    drPatient["Co_Quan_Ung_Thu"] = dlg.PatientHistory.Co_Quan_Ung_Thu;
                else
                    drPatient["Co_Quan_Ung_Thu"] = string.Empty;

                drPatient["Dong_Kinh"] = dlg.PatientHistory.Dong_Kinh.Value;
                drPatient["Hen_Suyen"] = dlg.PatientHistory.Hen_Suyen.Value;
                drPatient["Benh_Khac"] = dlg.PatientHistory.Benh_Khac.Value;
                if (dlg.PatientHistory.Benh_Khac.Value)
                {
                    drPatient["Benh_Gi"] = dlg.PatientHistory.Benh_Gi;
                    drPatient["Thuoc_Dang_Dung"] = dlg.PatientHistory.Thuoc_Dang_Dung;
                }
                else
                {
                    drPatient["Benh_Gi"] = string.Empty;
                    drPatient["Thuoc_Dang_Dung"] = string.Empty;
                }

                drPatient["Hut_Thuoc"] = dlg.PatientHistory.Hut_Thuoc.Value;
                drPatient["Uong_Ruou"] = dlg.PatientHistory.Uong_Ruou.Value;
                drPatient["Tinh_Trang_Gia_Dinh"] = dlg.PatientHistory.Tinh_Trang_Gia_Dinh;
                drPatient["Chich_Ngua_Viem_Gan_B"] = dlg.PatientHistory.Chich_Ngua_Viem_Gan_B.Value;
                drPatient["Chich_Ngua_Uon_Van"] = dlg.PatientHistory.Chich_Ngua_Uon_Van;
                drPatient["Chich_Ngua_Cum"] = dlg.PatientHistory.Chich_Ngua_Cum;
                drPatient["Dang_Co_Thai"] = dlg.PatientHistory.Dang_Co_Thai.Value;
            }

            OnSearchPatient();
        }

        private void OnDeletePatient()
        {
            if (_dataSource == null) return;
            List<string> deletedPatientList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            List<DataRow> deletedRows2 = new List<DataRow>();
            DataTable dt = dgPatient.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    string patientGUID = row["PatientGUID"].ToString();
                    deletedPatientList.Add(patientGUID);
                    deletedRows.Add(row);
                    DataRow r = GetDataRow(patientGUID);
                    if (r != null) deletedRows2.Add(r);
                }
            }

            if (deletedPatientList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những bệnh nhân mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = PatientBus.DeletePatient(deletedPatientList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }

                        foreach (DataRow row in deletedRows2)
                        {
                            _dataSource.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.DeletePatient"));
                        Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.DeletePatient"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần xóa.");
        }

        private void OnOpentPatient()
        {
            if (dgPatient.SelectedRows == null || dgPatient.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 bệnh nhân.");
                return;
            }

            DataRow patientRow = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row;
            base.RaiseOpentPatient(patientRow);
        }

        private void OnSearchPatient()
        {
            chkChecked.Checked = false;
            if (txtSearchPatient.Text.Trim() == string.Empty)
            {
                dgPatient.DataSource = _dataSource;
                if (dgPatient.RowCount > 0) dgPatient.Rows[0].Selected = true;
                return;
            }

            string str = txtSearchPatient.Text.ToLower();

            //FullName
            var results = from p in _dataSource.AsEnumerable()
                          where (p.Field<string>("FullName").ToLower().IndexOf(str) >= 0 ||
                          str.IndexOf(p.Field<string>("FullName").ToLower()) >= 0) &&
                          p.Field<string>("FullName") != null &&
                          p.Field<string>("FullName").Trim() != string.Empty
                          select p;

            DataTable newDataSource = _dataSource.Clone();
            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource;
                return;
            }


            //FileNum
            results = from p in _dataSource.AsEnumerable()
                      where (p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0 ||
                      str.IndexOf(p.Field<string>("FileNum").ToLower()) >= 0) &&
                          p.Field<string>("FileNum") != null &&
                          p.Field<string>("FileNum").Trim() != string.Empty
                      select p;

            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource;
                return;
            }

            //HomePhone
            results = from p in _dataSource.AsEnumerable()
                      where (p.Field<string>("HomePhone").ToLower().IndexOf(str) >= 0 ||
                      str.IndexOf(p.Field<string>("HomePhone").ToLower()) >= 0) &&
                      p.Field<string>("HomePhone") != null &&
                      p.Field<string>("HomePhone").Trim() != string.Empty
                      select p;

            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource;
                return;
            }

            //WorkPhone
            results = from p in _dataSource.AsEnumerable()
                      where (p.Field<string>("WorkPhone").ToLower().IndexOf(str) >= 0 ||
                      str.IndexOf(p.Field<string>("WorkPhone").ToLower()) >= 0) &&
                          p.Field<string>("WorkPhone") != null &&
                          p.Field<string>("WorkPhone").Trim() != string.Empty
                      select p;

            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource;
                return;
            }

            //Mobile
            results = from p in _dataSource.AsEnumerable()
                      where (p.Field<string>("Mobile").ToLower().IndexOf(str) >= 0 ||
                      str.IndexOf(p.Field<string>("Mobile").ToLower()) >= 0) &&
                          p.Field<string>("Mobile") != null &&
                          p.Field<string>("Mobile").Trim() != string.Empty
                      select p;

            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            dgPatient.DataSource = newDataSource;
        }
        #endregion

        #region Window Event Handlers
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddPatient();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditPatient();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeletePatient();
        }

        private void btnOpenPatient_Click(object sender, EventArgs e)
        {
            OnOpentPatient();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void dgDocStaff_DoubleClick(object sender, EventArgs e)
        {
            OnEditPatient();
        }

        private void txtSearchPatient_TextChanged(object sender, EventArgs e)
        {
            OnSearchPatient();
        }

        private void txtSearchPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgPatient.Focus();

                if (dgPatient.SelectedRows != null && dgPatient.SelectedRows.Count > 0)
                {
                    int index = dgPatient.SelectedRows[0].Index;
                    if (index < dgPatient.RowCount - 1)
                    {
                        index++;
                        dgPatient.CurrentCell = dgPatient[1, index];
                        dgPatient.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgPatient.Focus();

                if (dgPatient.SelectedRows != null && dgPatient.SelectedRows.Count > 0)
                {
                    int index = dgPatient.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgPatient.CurrentCell = dgPatient[1, index];
                        dgPatient.Rows[index].Selected = true;
                    }
                }
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayPatientListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayPatientList();
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
