using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Bussiness;
using MM.Common;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgAddMultiKetQuaCanLamSang : dlgBase
    {
        #region Members
        private string _patientGUID = string.Empty;
        #endregion

        #region Constructor
        public dlgAddMultiKetQuaCanLamSang(string patientGUID)
        {
            InitializeComponent();
            _patientGUID = patientGUID;
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            Cursor.Current = Cursors.WaitCursor;
            dtpkActiveDate.Value = DateTime.Now;

            //Service
            Result result = ServicesBus.GetServicesList(ServiceType.CanLamSang);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ServicesBus.GetServicesList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.GetServicesList"));
                return;
            }
            else
            {
                DataTable dtService = result.QueryResult as DataTable;
                DataRow row = dtService.NewRow();
                row["ServiceGUID"] = Guid.Empty.ToString();
                row["Name"] = " ";
                dtService.Rows.InsertAt(row, 0);

                serviceGUIDDataGridViewTextBoxColumn.DataSource = dtService;
                serviceGUIDDataGridViewTextBoxColumn.DisplayMember = "Name";
                serviceGUIDDataGridViewTextBoxColumn.ValueMember = "ServiceGUID";
            }

            DisplayDocStaffList();

            btnAdd.Enabled = Global.AllowAddLoiKhuyen;
        }

        private void DisplayDocStaffList()
        {
            //DocStaff
            List<byte> staffTypes = new List<byte>();
            staffTypes.Add((byte)StaffType.BacSi);
            staffTypes.Add((byte)StaffType.BacSiNgoaiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiNoiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiPhuKhoa);
            staffTypes.Add((byte)StaffType.BacSiSieuAm);

            Result result = DocStaffBus.GetDocStaffList(staffTypes);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
                return;
            }
            else
            {
                DataTable dt = result.QueryResult as DataTable;
                DataRow newRow = dt.NewRow();
                newRow["DocStaffGUID"] = Guid.Empty.ToString();
                newRow["FullName"] = " ";
                dt.Rows.InsertAt(newRow, 0);

                bacSiThucHienGUIDDataGridViewTextBoxColumn.DataSource = dt;
                bacSiThucHienGUIDDataGridViewTextBoxColumn.DisplayMember = "FullName";
                bacSiThucHienGUIDDataGridViewTextBoxColumn.ValueMember = "DocStaffGUID";
            }
        }

        private bool CheckInfo()
        {
            DataTable dt = dgCanLamSang.DataSource as DataTable;

            if (dgCanLamSang.RowCount <= 1)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập ít nhất 1 kết quả cận lâm sàng.", IconType.Information);
                return false;
            }

            int count = dgCanLamSang.RowCount - 1;
            for (int i = 0; i < count; i++)
            {
                DataGridViewRow row = dgCanLamSang.Rows[i];
                if (row.Cells[1].Value == null || row.Cells[1].Value == DBNull.Value ||
                    row.Cells[1].Value.ToString() == Guid.Empty.ToString())
                {
                    MsgBox.Show(this.Text, "Vui lòng nhập dịch vụ.", IconType.Information);
                    return false;
                }
            }

            return true;
        }

        private void OnSaveInfo()
        {
            try
            {
                MethodInvoker method = delegate
                {
                    int count = dgCanLamSang.RowCount - 1;
                    List<KetQuaCanLamSang> canLamSangList = new List<KetQuaCanLamSang>();
                    for (int i = 0; i < count; i++)
                    {
                        DataGridViewRow row = dgCanLamSang.Rows[i];
                        KetQuaCanLamSang canLamSang = new KetQuaCanLamSang();
                        canLamSang.CreatedDate = DateTime.Now;
                        canLamSang.CreatedBy = Guid.Parse(Global.UserGUID);
                        canLamSang.PatientGUID = Guid.Parse(_patientGUID);
                        canLamSang.NgayKham = dtpkActiveDate.Value;

                        canLamSang.ServiceGUID = Guid.Parse(row.Cells[1].Value.ToString());
                        if (row.Cells[2].Value != null && row.Cells[2].Value.ToString() != Guid.Empty.ToString())
                            canLamSang.BacSiThucHienGUID = Guid.Parse(row.Cells[2].Value.ToString());
                        else
                            canLamSang.BacSiThucHienGUID = null;

                        bool binhThuong = Convert.ToBoolean(row.Cells[3].Value);
                        bool batThuong = Convert.ToBoolean(row.Cells[4].Value);
                        bool amTinh = Convert.ToBoolean(row.Cells[5].Value);
                        bool duongTinh = Convert.ToBoolean(row.Cells[6].Value);

                        canLamSang.IsNormalOrNegative = (!amTinh && !duongTinh) ? true : false;
                        canLamSang.Normal = binhThuong;
                        canLamSang.Abnormal = batThuong;
                        canLamSang.Negative = amTinh;
                        canLamSang.Positive = duongTinh;
                        canLamSang.Note = row.Cells[7].Value as string;
                        canLamSangList.Add(canLamSang);
                    }

                    Result result = KetQuaCanLamSangBus.InsertMultiKetQuaCanLamSang(canLamSangList);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("KetQuaCanLamSangBus.InsertMultiKetQuaCanLamSang"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaCanLamSangBus.InsertMultiKetQuaCanLamSang"));
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
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

        private void RefreshNo()
        {
            int i = 1;
            foreach (DataGridViewRow row in dgCanLamSang.Rows)
            {
                row.Cells[0].Value = i++;
            }
        }

        private void OnAddLoiKhuyen()
        {
            dlgAddMultiLoiKhuyen dlg = new dlgAddMultiLoiKhuyen(_patientGUID);
            dlg.ShowDialog(this);
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddServiceHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
        }

        private void dlgAddServiceHistory_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void dgCanLamSang_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control.GetType() == typeof(DataGridViewComboBoxEditingControl))
            {
                DataGridViewComboBoxEditingControl cbo = e.Control as DataGridViewComboBoxEditingControl;
                cbo.DropDownStyle = ComboBoxStyle.DropDown;
                cbo.AutoCompleteMode = AutoCompleteMode.Suggest;
            }
        }

        private void dgCanLamSang_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            RefreshNo();
        }

        private void dgCanLamSang_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            RefreshNo();
        }

        private void dgCanLamSang_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 3 || e.ColumnIndex > 6) return;
            if (e.RowIndex < 0) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgCanLamSang.Rows[e.RowIndex].Cells[e.ColumnIndex];
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);
            cell.Value = isChecked;
            switch (e.ColumnIndex)
            {
                case 3:
                    if (isChecked)
                    {
                        cell = (DataGridViewCheckBoxCell)dgCanLamSang.Rows[e.RowIndex].Cells[4];
                        cell.EditingCellFormattedValue = false;
                        cell.Value = false;

                        cell = (DataGridViewCheckBoxCell)dgCanLamSang.Rows[e.RowIndex].Cells[5];
                        cell.EditingCellFormattedValue = false;
                        cell.Value = false;

                        cell = (DataGridViewCheckBoxCell)dgCanLamSang.Rows[e.RowIndex].Cells[6];
                        cell.EditingCellFormattedValue = false;
                        cell.Value = false;
                    }
                    break;

                case 4:
                    if (isChecked)
                    {
                        cell = (DataGridViewCheckBoxCell)dgCanLamSang.Rows[e.RowIndex].Cells[3];
                        cell.EditingCellFormattedValue = false;
                        cell.Value = false;

                        cell = (DataGridViewCheckBoxCell)dgCanLamSang.Rows[e.RowIndex].Cells[5];
                        cell.EditingCellFormattedValue = false;
                        cell.Value = false;

                        cell = (DataGridViewCheckBoxCell)dgCanLamSang.Rows[e.RowIndex].Cells[6];
                        cell.EditingCellFormattedValue = false;
                        cell.Value = false;
                    }
                    break;

                case 5:
                    if (isChecked)
                    {
                        cell = (DataGridViewCheckBoxCell)dgCanLamSang.Rows[e.RowIndex].Cells[3];
                        cell.EditingCellFormattedValue = false;
                        cell.Value = false;

                        cell = (DataGridViewCheckBoxCell)dgCanLamSang.Rows[e.RowIndex].Cells[4];
                        cell.EditingCellFormattedValue = false;
                        cell.Value = false;

                        cell = (DataGridViewCheckBoxCell)dgCanLamSang.Rows[e.RowIndex].Cells[6];
                        cell.EditingCellFormattedValue = false;
                        cell.Value = false;
                    }
                    break;

                case 6:
                    if (isChecked)
                    {
                        cell = (DataGridViewCheckBoxCell)dgCanLamSang.Rows[e.RowIndex].Cells[3];
                        cell.EditingCellFormattedValue = false;
                        cell.Value = false;

                        cell = (DataGridViewCheckBoxCell)dgCanLamSang.Rows[e.RowIndex].Cells[4];
                        cell.EditingCellFormattedValue = false;
                        cell.Value = false;

                        cell = (DataGridViewCheckBoxCell)dgCanLamSang.Rows[e.RowIndex].Cells[5];
                        cell.EditingCellFormattedValue = false;
                        cell.Value = false;
                    }
                    break;
            }

            dgCanLamSang.Refresh();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddLoiKhuyen();
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
