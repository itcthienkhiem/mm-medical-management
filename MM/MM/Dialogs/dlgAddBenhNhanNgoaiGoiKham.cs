using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgAddBenhNhanNgoaiGoiKham : dlgBase
    {
        #region Members
        private DataTable _dtSource = null;
        private DataTable _dtBenhNhan = null;
        #endregion

        #region Constructor
        public dlgAddBenhNhanNgoaiGoiKham(DataTable dtSource)
        {
            InitializeComponent();
            _dtSource = dtSource;
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void InitData()
        {
            Cursor.Current = Cursors.WaitCursor;
            dgBenhNhanNgoaiGoiKham.DataSource = _dtSource;
            ngayKhamDataGridViewTextBoxColumn.DefaultCellStyle.NullValue = DateTime.Now.ToString("dd/MM/yyyy");

            Result result = PatientBus.GetPatientList();
            if (result.IsOK)
            {
                _dtBenhNhan = result.QueryResult as DataTable;
                //DataRow row = _dtBenhNhan.NewRow();
                //row["PatientGUID"] = Guid.Empty.ToString();
                //row["FullName"] = " ";
                //_dtBenhNhan.Rows.InsertAt(row, 0);

                //patientGUIDDataGridViewTextBoxColumn.DataSource = _dtBenhNhan;
                //patientGUIDDataGridViewTextBoxColumn.DisplayMember = "FullName";
                //patientGUIDDataGridViewTextBoxColumn.ValueMember = "PatientGUID";
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.GetPatientList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientList"));
                this.Close();
            }

            result = ServicesBus.GetServicesList();
            if (result.IsOK)
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
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ServicesBus.GetServicesList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ServicesBus.GetServicesList"));
                this.Close();
            }
        }

        private bool CheckInfo()
        {
            DataTable dt = dgBenhNhanNgoaiGoiKham.DataSource as DataTable;

            if (dt == null || dt.Rows.Count <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập ít nhất 1 bệnh nhân ngoài gói khám.", IconType.Information);
                return false;
            }

            foreach (DataRow row in dt.Rows)
            {
                if (row["PatientGUID"] == null || row["PatientGUID"] == DBNull.Value ||
                    row["PatientGUID"].ToString() == Guid.Empty.ToString())
                {
                    MsgBox.Show(this.Text, "Vui lòng nhập bệnh nhân.", IconType.Information);
                    return false;
                }

                if (row["ServiceGUID"] == null || row["ServiceGUID"] == DBNull.Value ||
                    row["ServiceGUID"].ToString() == Guid.Empty.ToString())
                {
                    MsgBox.Show(this.Text, "Vui lòng nhập dịch vụ.", IconType.Information);
                    return false;
                }

                if (row["LanDauStr"] == null || row["LanDauStr"] == DBNull.Value)
                {
                    MsgBox.Show(this.Text, "Vui lòng nhập lần đầu hoặc tái khám.", IconType.Information);
                    return false;
                }
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
                    List<BenhNhanNgoaiGoiKham> benhNhanNgoaiGoiKhamList = new List<BenhNhanNgoaiGoiKham>();
                    DataTable dt = dgBenhNhanNgoaiGoiKham.DataSource as DataTable;
                    foreach (DataRow row in dt.Rows)
                    {
                        BenhNhanNgoaiGoiKham bnngk = new BenhNhanNgoaiGoiKham();
                        DateTime ngayKham = DateTime.Now;
                        if (row["NgayKham"] != null && row["NgayKham"] != DBNull.Value)
                            ngayKham = Convert.ToDateTime(row["NgayKham"]);

                        bnngk.NgayKham = ngayKham;
                        bnngk.PatientGUID = Guid.Parse(row["PatientGUID"].ToString());
                        bnngk.ServiceGUID = Guid.Parse(row["ServiceGUID"].ToString());
                        string lanDauStr = row["LanDauStr"].ToString();
                        bnngk.LanDau = lanDauStr == "Lần đầu" ? (byte)0 : (byte)1;
                        bnngk.CreatedBy = Guid.Parse(Global.UserGUID);
                        bnngk.CreatedDate = DateTime.Now;

                        benhNhanNgoaiGoiKhamList.Add(bnngk);
                    }

                    Result result = BenhNhanNgoaiGoiKhamBus.InsertBenhNhanNgoaiGoiKham(benhNhanNgoaiGoiKhamList);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("BenhNhanNgoaiGoiKhamBus.InsertBenhNhanNgoaiGoiKham"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("BenhNhanNgoaiGoiKhamBus.InsertBenhNhanNgoaiGoiKham"));
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

        private void RefreshNo()
        {
            int i = 1;
            foreach (DataGridViewRow row in dgBenhNhanNgoaiGoiKham.Rows)
            {
                row.Cells[0].Value = i++;
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddBenhNhanNgoaiGoiKham_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void dgBenhNhanNgoaiGoiKham_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control.GetType() == typeof(DataGridViewComboBoxEditingControl))
            {
                DataGridViewComboBoxEditingControl cbo = e.Control as DataGridViewComboBoxEditingControl;
                cbo.DropDownStyle = ComboBoxStyle.DropDown;
                cbo.AutoCompleteMode = AutoCompleteMode.Suggest;
            }
        }

        private void dgBenhNhanNgoaiGoiKham_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("vi-VN");
            e.InheritedCellStyle.FormatProvider = cultureInfo;
        }

        private void dgBenhNhanNgoaiGoiKham_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 1:
                    try
                    {
                        CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("vi-VN");
                        DateTime.Parse(e.FormattedValue.ToString(), cultureInfo);
                    }
                    catch
                    {
                        MsgBox.Show(this.Text, "Định dạng ngày không hợp lệ. Vui lòng nhập lại.", IconType.Information);
                        e.Cancel = true;
                    }
                    break;
            }
        }

        private void dlgAddBenhNhanNgoaiGoiKham_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                    e.Cancel = true;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void dgBenhNhanNgoaiGoiKham_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            RefreshNo();
        }

        private void dgBenhNhanNgoaiGoiKham_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            RefreshNo();
        }

        private void dgBenhNhanNgoaiGoiKham_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex >= 0)
            {
                dlgSelectPatient dlg = new dlgSelectPatient(_dtBenhNhan);
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    string patientGUID = dlg.PatientRow["PatientGUID"].ToString();
                    string tenBenhNhan = dlg.PatientRow["FullName"].ToString();
                    dgBenhNhanNgoaiGoiKham.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = tenBenhNhan;
                    dgBenhNhanNgoaiGoiKham.Rows[e.RowIndex].Cells["PatientGUID"].Value = patientGUID;
                }
            }
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
