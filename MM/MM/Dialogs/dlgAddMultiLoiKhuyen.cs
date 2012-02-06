using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgAddMultiLoiKhuyen : dlgBase
    {
        #region Members
        private string _patientGUID = string.Empty;
        private DataTable _dataSource = null;
        #endregion

        #region Constructor
        public dlgAddMultiLoiKhuyen(string patientGUID)
        {
            InitializeComponent();
            _patientGUID = patientGUID;
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            dtpkNgay.Value = DateTime.Now;

            //DocStaff
            List<byte> staffTypes = new List<byte>();
            staffTypes.Add((byte)StaffType.BacSi);
            staffTypes.Add((byte)StaffType.BacSiSieuAm);
            staffTypes.Add((byte)StaffType.BacSiNgoaiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiNoiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiPhuKhoa);
            Result result = DocStaffBus.GetDocStaffList(staffTypes);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
                return;
            }
            else
            {
                cboDocStaff.DataSource = result.QueryResult;
            }

            if (Global.StaffType == StaffType.BacSi || Global.StaffType == StaffType.BacSiSieuAm ||
                Global.StaffType == StaffType.BacSiNgoaiTongQuat || Global.StaffType == StaffType.BacSiNoiTongQuat ||
                Global.StaffType == StaffType.BacSiPhuKhoa)
            {
                cboDocStaff.SelectedValue = Global.UserGUID;
                cboDocStaff.Enabled = false;
            }

            //Symptom
            result = SymptomBus.GetSymptomList();
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("SymptomBus.GetSymptomList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("SymptomBus.GetSymptomList"));
                return;
            }
            else
            {
                dgSymptom.DataSource = result.QueryResult;
                _dataSource = result.QueryResult as DataTable;
            }
        }

        private List<string> GetCheckedSymptomList()
        {
            UpdateChecked();
            List<string> checkedSympList = new List<string>();
            //DataTable dt = dgSymptom.DataSource as DataTable;
            foreach (DataRow row in _dataSource.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    checkedSympList.Add(row["SymptomGUID"].ToString());
                }
            }

            return checkedSympList;
        }

        private bool CheckInfo()
        {
            if (cboDocStaff.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ.", IconType.Information);
                cboDocStaff.Focus();
                return false;
            }

            List<string> checkedSympList = GetCheckedSymptomList();
            if (checkedSympList.Count <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn triệu chứng.", IconType.Information);
                dgSymptom.Focus();
                return false;
            }

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
                MsgBox.Show(this.Text, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnSaveInfo()
        {
            try
            {
                MethodInvoker method = delegate
                {
                    List<string> checkedSympList = GetCheckedSymptomList();
                    foreach (string symptomGUID in checkedSympList)
                    {
                        LoiKhuyen loiKhuyen = new LoiKhuyen();
                        loiKhuyen.CreatedDate = DateTime.Now;
                        loiKhuyen.CreatedBy = Guid.Parse(Global.UserGUID);
                        loiKhuyen.PatientGUID = Guid.Parse(_patientGUID);

                        loiKhuyen.Ngay = dtpkNgay.Value;
                        loiKhuyen.DocStaffGUID = Guid.Parse(cboDocStaff.SelectedValue.ToString());
                        loiKhuyen.SymptomGUID = Guid.Parse(symptomGUID);

                        Result result = LoiKhuyenBus.InsertLoiKhuyen(loiKhuyen);
                        if (!result.IsOK)
                        {
                            MsgBox.Show(this.Text, result.GetErrorAsString("LoiKhuyenBus.InsertLoiKhuyen"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("LoiKhuyenBus.InsertLoiKhuyen"));
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            break;
                        }
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }

        }

        private void OnSearchTrieuChung()
        {
            UpdateChecked();
            chkChecked.Checked = false;
            List<DataRow> results = null;
            DataTable newDataSource = null;

            if (txtTimTrieuChung.Text.Trim() == string.Empty)
            {
                results = (from p in _dataSource.AsEnumerable()
                           orderby p.Field<string>("SymptomName")
                           select p).ToList<DataRow>();

                newDataSource = _dataSource.Clone();
                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgSymptom.DataSource = newDataSource;
                if (dgSymptom.RowCount > 0) dgSymptom.Rows[0].Selected = true;
                return;
            }

            string str = txtTimTrieuChung.Text.ToLower();
            newDataSource = _dataSource.Clone();

            if (chkTheoMaTrieuChung.Checked)
            {
                //Code
                results = (from p in _dataSource.AsEnumerable()
                           where p.Field<string>("Code") != null &&
                             p.Field<string>("Code").Trim() != string.Empty &&
                             (p.Field<string>("Code").ToLower().IndexOf(str) >= 0 ||
                           str.IndexOf(p.Field<string>("Code").ToLower()) >= 0)
                           orderby p.Field<string>("SymptomName")
                           select p).ToList<DataRow>();

                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                if (newDataSource.Rows.Count > 0)
                {
                    dgSymptom.DataSource = newDataSource;
                    return;
                }
            }
            else
            {
                //FullName
                results = (from p in _dataSource.AsEnumerable()
                           where (p.Field<string>("SymptomName").ToLower().IndexOf(str) >= 0 ||
                           str.IndexOf(p.Field<string>("SymptomName").ToLower()) >= 0) &&
                           p.Field<string>("SymptomName") != null &&
                           p.Field<string>("SymptomName").Trim() != string.Empty
                           orderby p.Field<string>("SymptomName")
                           select p).ToList<DataRow>();


                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                if (newDataSource.Rows.Count > 0)
                {
                    dgSymptom.DataSource = newDataSource;
                    return;
                }
            }

            dgSymptom.DataSource = newDataSource;
        }

        private void UpdateChecked()
        {
            DataTable dt = dgSymptom.DataSource as DataTable;
            if (dt == null) return;

            foreach (DataRow row1 in dt.Rows)
            {
                string patientGUID1 = row1["SymptomGUID"].ToString();
                bool isChecked1 = Convert.ToBoolean(row1["Checked"]);
                foreach (DataRow row2 in _dataSource.Rows)
                {
                    string patientGUID2 = row2["SymptomGUID"].ToString();
                    bool isChecked2 = Convert.ToBoolean(row2["Checked"]);

                    if (patientGUID1 == patientGUID2)
                    {
                        row2["Checked"] = row1["Checked"];
                        break;
                    }
                }
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddLoiKhuyen_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void dlgAddLoiKhuyen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
            else
            {
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin lời khuyên ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (CheckInfo())
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        SaveInfoAsThread();
                    }
                    else
                        e.Cancel = true;
                }
            }
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgSymptom.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void txtTimTrieuChung_TextChanged(object sender, EventArgs e)
        {
            OnSearchTrieuChung();
        }

        private void chkTheoMaTrieuChung_CheckedChanged(object sender, EventArgs e)
        {
            OnSearchTrieuChung();
        }
        #endregion

        #region Working Thread
        private void OnSaveInfoProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnSaveInfo();
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
    }
}
