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
using SpreadsheetGear;
using SpreadsheetGear.Advanced.Cells;

namespace MM.Controls
{
    public partial class uSendSMS : uBase
    {
        #region Members
        private string _fileName = string.Empty;
        private bool _isAscending = true;
        private Dictionary<string, DataRow> _dictPatient = new Dictionary<string,DataRow>();
        private string _name = string.Empty;
        private int _type = 0;
        private DataTable _dtTemp = null;
        #endregion

        #region Constructor
        public uSendSMS()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnSendSMS.Enabled = AllowSendSMS;
        }

        public void ClearData()
        {
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
                _name = txtSearchPatient.Text;
                if (_name.Trim() == string.Empty) _name = "*";
                else if (_name.Trim() == "*") _name = string.Empty;
                if (chkMaBenhNhan.Checked) _type = 1;
                else if (chkTheoSoDienThoai.Checked) _type = 2;
                else _type = 0;

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

        public override void SearchAsThread()
        {
            try
            {
                chkChecked.Checked = false;
                _name = txtSearchPatient.Text;
                if (_name.Trim() == string.Empty) _name = "*";
                else if (_name.Trim() == "*") _name = string.Empty;
                if (chkMaBenhNhan.Checked) _type = 1;
                else if (chkTheoSoDienThoai.Checked) _type = 2;
                else _type = 0;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplayPatientList()
        {
            lock (ThisLock)
            {
                Result result = PatientBus.GetPatientWithMobiList(_name, _type);
                if (result.IsOK)
                {
                    dgPatient.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();

                        DataTable dt = result.QueryResult as DataTable;
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        UpdateChecked(dt);
                        dgPatient.DataSource = dt;

                        lbKetQuaTimDuoc.Text = string.Format("Kết quả tìm được: {0}", dt.Rows.Count);
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.GetPatientWithMobiList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientWithMobiList"));
                }
            }
        }

        private void UpdateChecked(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string key = row["PatientGUID"].ToString();
                if (_dictPatient.ContainsKey(key))
                    row["Checked"] = true;
            }
        }

        private void OnSendSMS()
        {
            List<DataRow> checkedRows = _dictPatient.Values.ToList();
            if (checkedRows.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn gửi SMS những bệnh nhân mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    dlgSelectTinNhanMau dlg = new dlgSelectTinNhanMau();
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        string tinNhanMau = dlg.TinNhanMau;
                        foreach (DataRow row in checkedRows)
                        {
                            string maBenhNhan = row["FileNum"].ToString();
                            string tenBenhNhan = row["FullName"].ToString();
                            string ngaySinh = row["DobStr"].ToString();
                            string gioiTinh = row["GenderAsStr"].ToString();
                            string diaChi = row["Address"].ToString();
                            string cmnd = row["IdentityCard"].ToString();
                            string mobile = row["Mobile"].ToString();
                            string email = row["Email"].ToString();

                            string noiDung = tinNhanMau.Replace("#MaBenhNhan#", maBenhNhan);
                            noiDung = noiDung.Replace("#TenBenhNhan#", tenBenhNhan);
                            noiDung = noiDung.Replace("#NgaySinh#", ngaySinh);
                            noiDung = noiDung.Replace("#GioiTinh#", gioiTinh);
                            noiDung = noiDung.Replace("#DiaChi#", diaChi);
                            noiDung = noiDung.Replace("#CMND#", cmnd);
                            noiDung = noiDung.Replace("#Mobile#", mobile);
                            noiDung = noiDung.Replace("#Email#", email);


                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần gửi SMS.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
                string patientGUID = row["PatientGUID"].ToString();
                if (chkChecked.Checked)
                {
                    if (!_dictPatient.ContainsKey(patientGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictPatient.Add(patientGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
                else
                {
                    if (_dictPatient.ContainsKey(patientGUID))
                    {
                        _dictPatient.Remove(patientGUID);

                        DataRow[] rows = _dtTemp.Select(string.Format("PatientGUID='{0}'", patientGUID));
                        if (rows != null && rows.Length > 0)
                            _dtTemp.Rows.Remove(rows[0]);
                    }
                }
            }
        }

        private void txtSearchPatient_TextChanged(object sender, EventArgs e)
        {
            StartTimer();
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
                DataTable newDataSource = null;

                if (_isAscending)
                {
                    newDataSource = (from p in dt.AsEnumerable()
                                     orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                                     select p).CopyToDataTable();
                }
                else
                {
                    newDataSource = (from p in dt.AsEnumerable()
                                     orderby p.Field<string>("FirstName") descending, p.Field<string>("FullName") descending
                                     select p).CopyToDataTable();
                }
                
                dgPatient.DataSource = newDataSource;

                if (dt != null)
                {
                    dt.Rows.Clear();
                    dt.Clear();
                    dt = null;
                }
            }
            else
                _isAscending = false;
        }

        private void chkMaBenhNhan_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void dgPatient_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgPatient.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string patientGUID = row["PatientGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            if (isChecked)
            {
                if (!_dictPatient.ContainsKey(patientGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictPatient.Add(patientGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictPatient.ContainsKey(patientGUID))
                {
                    _dictPatient.Remove(patientGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("PatientGUID='{0}'", patientGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
                }
            }
        }

        private void chkTheoSoDienThoai_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            OnSendSMS();
        }
        #endregion

        #region Working Thread
        private void OnDisplayPatientListProc(object state)
        {
            try
            {
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

        private void OnSearchProc(object state)
        {
            try
            {
                OnDisplayPatientList();
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
