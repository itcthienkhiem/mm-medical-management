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
        private DataTable _dtTemp = null;
        private Dictionary<string, DataRow> _dictLoiKhuyen = new Dictionary<string, DataRow>();
        private string _name = string.Empty;
        private int _type = 0;
        private Object _thisLock = new Object();
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
            DisplayAsThread();
        }

        public void ClearData()
        {
            DataTable dt = dgSymptom.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
            }

            dgSymptom.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                _name = txtTimTrieuChung.Text;
                if (chkTheoMaTrieuChung.Checked) _type = 1;
                else _type = 0;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayTrieuChungListProc));
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

        private void SearchAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                _name = txtTimTrieuChung.Text;
                if (chkTheoMaTrieuChung.Checked) _type = 1;
                else _type = 0;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplayTrieuChungList()
        {
            lock (_thisLock)
            {
                Result result = SymptomBus.GetSymptomList(_name, _type);
                if (result.IsOK)
                {
                    dgSymptom.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();

                        DataTable dt = result.QueryResult as DataTable;
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        UpdateChecked(dt);
                        dgSymptom.DataSource = dt;
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ThuocBus.GetThuocList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ThuocBus.GetThuocList"));
                }
            }
        }

        private void UpdateChecked(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string key = row["SymptomGUID"].ToString();
                if (_dictLoiKhuyen.ContainsKey(key))
                    row["Checked"] = true;
            }
        }

        private List<string> GetCheckedSymptomList()
        {
            List<string> checkedSympList = new List<string>();
            foreach (DataRow row in _dictLoiKhuyen.Values.ToList())
            {
                checkedSympList.Add(row["SymptomGUID"].ToString());
            }

            return checkedSympList;
        }

        private bool CheckInfo()
        {
            if (cboDocStaff.SelectedValue == null || cboDocStaff.Text.Trim() == string.Empty)
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

        private void dgSymptom_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgSymptom.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgSymptom.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string symptomGUID = row["SymptomGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            if (isChecked)
            {
                if (!_dictLoiKhuyen.ContainsKey(symptomGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictLoiKhuyen.Add(symptomGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictLoiKhuyen.ContainsKey(symptomGUID))
                {
                    _dictLoiKhuyen.Remove(symptomGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("SymptomGUID='{0}'", symptomGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
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
                string symptomGUID = row["SymptomGUID"].ToString();
                if (chkChecked.Checked)
                {
                    if (!_dictLoiKhuyen.ContainsKey(symptomGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictLoiKhuyen.Add(symptomGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
                else
                {
                    if (_dictLoiKhuyen.ContainsKey(symptomGUID))
                    {
                        _dictLoiKhuyen.Remove(symptomGUID);

                        DataRow[] rows = _dtTemp.Select(string.Format("SymptomGUID='{0}'", symptomGUID));
                        if (rows != null && rows.Length > 0)
                            _dtTemp.Rows.Remove(rows[0]);
                    }
                }
            }
        }

        private void txtTimTrieuChung_TextChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void chkTheoMaTrieuChung_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
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
                MsgBox.Show(this.Text, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplayTrieuChungListProc(object state)
        {
            try
            {
                OnDisplayTrieuChungList();
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

        private void OnSearchProc(object state)
        {
            try
            {
                OnDisplayTrieuChungList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }
        #endregion

        
    }
}
