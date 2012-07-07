using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using MM.Dialogs;
using MM.Bussiness;
using MM.Common;
using MM.Databasae;

namespace MM.Controls
{
    public partial class uBenhNhanThanThuocList : uBase
    {
        #region Members
        private DataTable _dataSource = null;
        private string _fileName = string.Empty;
        private bool _isAscending = true;
        #endregion

        #region Constructor
        public uBenhNhanThanThuocList()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnDelete.Enabled = AllowDelete;
            btnOpenPatient.Enabled = AllowOpenPatient;
            btnVaoPhongCho.Enabled = Global.AllowAddPhongCho;
        }

        public void ClearData()
        {
            if (_dataSource != null)
            {
                _dataSource.Rows.Clear();
                _dataSource.Clear();
                _dataSource = null;
            }

            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
            }

            dgPatient.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayPatientListProc));
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

        private void OnDisplayPatientList()
        {
            Result result = PatientBus.GetBenhNhanThanThuocList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    _dataSource = result.QueryResult as DataTable;
                    OnSearchPatient();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.GetBenhNhanThanThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetBenhNhanThanThuocList"));
            }
        }

        private DataRow GetDataRow(string patientGUID)
        {
            if (_dataSource == null || _dataSource.Rows.Count <= 0) return null;
            DataRow[] rows = _dataSource.Select(string.Format("PatientGUID = '{0}'", patientGUID));
            if (rows == null || rows.Length <= 0) return null;

            return rows[0];
        }

        private void OnEditPatient()
        {
            if (_dataSource == null) return;

            if (dgPatient.SelectedRows == null || dgPatient.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 bệnh nhân.", IconType.Information);
                return;
            }

            string patientGUID = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row["PatientGUID"].ToString();
            DataRow drPatient = GetDataRow(patientGUID);
            if (drPatient == null) return;

            dlgAddPatient dlg = new dlgAddPatient(drPatient, AllowEdit);
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

                if (dlg.Contact.Gender == 0) drPatient["GenderAsStr"] = "Nam";//dlg.Contact.Gender == 0 ? "Nam" : "Nữ";
                else if (dlg.Contact.Gender == 1) drPatient["GenderAsStr"] = "Nữ";
                else drPatient["GenderAsStr"] = "Không xác định";

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
                drPatient["CompanyName"] = dlg.Contact.CompanyName;

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

                OnSearchPatient();
            }
        }

        private void OnOpentPatient()
        {
            if (dgPatient.SelectedRows == null || dgPatient.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 bệnh nhân.", IconType.Information);
                return;
            }

            string patientGUID = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row["PatientGUID"].ToString();
            DataRow drPatient = GetDataRow(patientGUID);
            base.RaiseOpentPatient(drPatient);
        }

        private void UpdateChecked()
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt == null) return;

            DataRow[] rows1 = dt.Select("Checked='True'");
            if (rows1 == null || rows1.Length <= 0) return;

            foreach (DataRow row1 in rows1)
            {
                string patientGUID1 = row1["PatientGUID"].ToString();
                DataRow[] rows2 = _dataSource.Select(string.Format("PatientGUID='{0}'", patientGUID1));
                if (rows2 == null || rows2.Length <= 0) continue;

                rows2[0]["Checked"] = row1["Checked"];
            }
        }

        private void OnSearchPatient()
        {
            UpdateChecked();
            chkChecked.Checked = false;
            List<DataRow> results = null;
            DataTable newDataSource = null;

            if (txtSearchPatient.Text.Trim() == string.Empty)
            {
                results = (from p in _dataSource.AsEnumerable()
                           orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                           select p).ToList<DataRow>();

                newDataSource = _dataSource.Clone();
                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgPatient.DataSource = newDataSource;
                if (dgPatient.RowCount > 0) dgPatient.Rows[0].Selected = true;
                _isAscending = true;
                lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dgPatient.RowCount);
                return;
            }

            string str = txtSearchPatient.Text.ToLower();

            newDataSource = _dataSource.Clone();

            if (chkMaBenhNhan.Checked)
            {
                //FileNum
                results = (from p in _dataSource.AsEnumerable()
                           where p.Field<string>("FileNum") != null &&
                             p.Field<string>("FileNum").Trim() != string.Empty &&
                             p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0
                           //(p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0 ||
                           //str.IndexOf(p.Field<string>("FileNum").ToLower()) >= 0)
                           orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                           select p).ToList<DataRow>();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                if (newDataSource.Rows.Count > 0)
                {
                    dgPatient.DataSource = newDataSource;
                    lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dgPatient.RowCount);
                    return;
                }
            }

            if (chkTheoSoDienThoai.Checked)
            {
                //FileNum
                results = (from p in _dataSource.AsEnumerable()
                           where p.Field<string>("Mobile") != null &&
                             p.Field<string>("Mobile").Trim() != string.Empty &&
                             p.Field<string>("Mobile").ToLower().IndexOf(str) >= 0
                           //(p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0 ||
                           //str.IndexOf(p.Field<string>("FileNum").ToLower()) >= 0)
                           orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                           select p).ToList<DataRow>();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                if (newDataSource.Rows.Count > 0)
                {
                    dgPatient.DataSource = newDataSource;
                    lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dgPatient.RowCount);
                    return;
                }
            }

            //FullName
            results = (from p in _dataSource.AsEnumerable()
                       where //(p.Field<string>("FullName").ToLower().IndexOf(str) >= 0 ||
                           //str.IndexOf(p.Field<string>("FullName").ToLower()) >= 0) &&
                       p.Field<string>("FullName").ToLower().IndexOf(str) >= 0 &&
                       p.Field<string>("FullName") != null &&
                       p.Field<string>("FullName").Trim() != string.Empty
                       orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                       select p).ToList<DataRow>();


            foreach (DataRow row in results)
                newDataSource.ImportRow(row);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource;
                lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dgPatient.RowCount);
                return;
            }

            dgPatient.DataSource = newDataSource;
            lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dgPatient.RowCount);
        }

        private void OnAddBenhNhanThanThuoc()
        {
            if (_dataSource == null) return;
            dlgSelectPatient dlg = new dlgSelectPatient(true);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                List<DataRow> patientRows = dlg.CheckedPatientRows;
                if (patientRows == null || patientRows.Count <= 0) return;

                Result result = PatientBus.InsertBenhNhanThanThuoc(patientRows);
                if (result.IsOK)
                {
                    foreach (DataRow row in patientRows)
                    {
                        row["Checked"] = false;
                        _dataSource.ImportRow(row);
                    }

                    OnSearchPatient();
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.DeletePatient"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.DeletePatient"));
                }
            }
        }

        private void OnDeleteBenhNhanThanThuoc()
        {
            if (_dataSource == null) return;
            UpdateChecked();
            List<string> deletedPatientList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataRow[] rows = _dataSource.Select("Checked='True'");
            if (rows != null && rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    string patientGUID = row["PatientGUID"].ToString();
                    deletedPatientList.Add(patientGUID);
                    deletedRows.Add(row);
                }
            }

            if (deletedPatientList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những bệnh nhân thân thuộc mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = PatientBus.DeleteBenhNhanThanThuoc(deletedPatientList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            _dataSource.Rows.Remove(row);
                        }

                        OnSearchPatient();
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.DeletePatient"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.DeletePatient"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân thân thuộc cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
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

        private void dgPatient_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                _isAscending = !_isAscending;

                DataTable dt = dgPatient.DataSource as DataTable;
                List<DataRow> results = null;

                if (_isAscending)
                {
                    results = (from p in dt.AsEnumerable()
                               orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                               select p).ToList<DataRow>();
                }
                else
                {
                    results = (from p in dt.AsEnumerable()
                               orderby p.Field<string>("FirstName") descending, p.Field<string>("FullName") descending
                               select p).ToList<DataRow>();
                }


                DataTable newDataSource = dt.Clone();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgPatient.DataSource = newDataSource;
            }
            else
                _isAscending = false;
        }

        private void chkMaBenhNhan_CheckedChanged(object sender, EventArgs e)
        {
            OnSearchPatient();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddBenhNhanThanThuoc();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteBenhNhanThanThuoc();
        }

        private void btnOpenPatient_Click(object sender, EventArgs e)
        {
            OnOpentPatient();
        }

        private void btnVaoPhongCho_Click(object sender, EventArgs e)
        {
            if (_dataSource == null) return;
            UpdateChecked();
            List<string> addedPatientList = new List<string>();
            foreach (DataRow row in _dataSource.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    string patientGUID = row["PatientGUID"].ToString();
                    addedPatientList.Add(patientGUID);
                }
            }

            if (addedPatientList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn thêm những bệnh nhân đã đánh dấu vào phòng chờ ?") == DialogResult.Yes)
                {
                    Result result = PhongChoBus.AddPhongCho(addedPatientList);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhongChoBus.AddPhongCho"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PhongChoBus.AddPhongCho"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần đưa vào phòng chờ.", IconType.Information);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditPatient();
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

        private void dgPatient_DoubleClick(object sender, EventArgs e)
        {
            OnEditPatient();
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
