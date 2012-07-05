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
    public partial class dlgAddKetQuaXetNghiemTay : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private KetQuaXetNghiem_Manual _ketQuaXetNghiem = new KetQuaXetNghiem_Manual();
        private DataRow _drKetQuaXetNghiem = null;
        private DataTable _dtChiTietKQXN = null;
        private Font _normalFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        private Font _boldFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        private DataRow _patientRow = null;
        #endregion

        #region Constructor
        public dlgAddKetQuaXetNghiemTay()
        {
            InitializeComponent();
        }

        public dlgAddKetQuaXetNghiemTay(DataRow patientRow)
        {
            InitializeComponent();
            _patientRow = patientRow;
        }

        public dlgAddKetQuaXetNghiemTay(DataRow drKetQuaXetNghiem, DataTable dtChiTietKQXN)
        {
            InitializeComponent();
            _drKetQuaXetNghiem = drKetQuaXetNghiem;
            _dtChiTietKQXN = dtChiTietKQXN;
            _isNew = false;
            this.Text = "Sua ket qua xet nghiem";
        }
        #endregion

        #region Properties
        public KetQuaXetNghiem_Manual KetQuaXetNghiem
        {
            get { return _ketQuaXetNghiem; }
            set { _ketQuaXetNghiem = value; }
        }
        #endregion

        #region UI Command
        private void DisplayInfo()
        {
            try
            {
                txtBenhNhan.Text = _drKetQuaXetNghiem["FullName"].ToString();
                txtBenhNhan.Tag = _drKetQuaXetNghiem["PatientGUID"].ToString();

                _ketQuaXetNghiem.KetQuaXetNghiemManualGUID = Guid.Parse(_drKetQuaXetNghiem["KetQuaXetNghiemManualGUID"].ToString());

                dgChiTiet.DataSource = _dtChiTietKQXN;
                //RefreshHighlight();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void RefreshHighlight()
        {
            foreach (DataGridViewRow row in dgChiTiet.Rows)
            {
                DataRow dr = (row.DataBoundItem as DataRowView).Row;
                TinhTrang tinhTrang = (TinhTrang)Convert.ToByte(dr["TinhTrang"]);
                if (tinhTrang == TinhTrang.BatThuong)
                {
                    row.DefaultCellStyle.Font = _boldFont;
                    row.DefaultCellStyle.ForeColor = Color.Red;
                }
                else
                {
                    row.DefaultCellStyle.Font = _normalFont;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private bool CheckInfo()
        {
            if (txtBenhNhan.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bệnh nhân.", IconType.Information);
                btnChonBenhNhan.Focus();
                return false;
            }

            if (dgChiTiet.RowCount <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập ít nhất 1 kết quả xét nghiệm.", IconType.Information);
                btnAdd.Focus();
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
                _ketQuaXetNghiem.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _ketQuaXetNghiem.CreatedDate = DateTime.Now;
                    _ketQuaXetNghiem.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _ketQuaXetNghiem.UpdatedDate = DateTime.Now;
                    _ketQuaXetNghiem.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                MethodInvoker method = delegate
                {
                    _ketQuaXetNghiem.NgayXN = DateTime.Now;
                    _ketQuaXetNghiem.PatientGUID = Guid.Parse(txtBenhNhan.Tag.ToString());

                    List<ChiTietKetQuaXetNghiem_Manual> ctkqxns = new List<ChiTietKetQuaXetNghiem_Manual>();
                    foreach (DataGridViewRow row in dgChiTiet.Rows)
                    {
                        DataRow dr = (row.DataBoundItem as DataRowView).Row;
                        ChiTietKetQuaXetNghiem_Manual ct = new ChiTietKetQuaXetNghiem_Manual();
                        ct.XetNghiem_ManualGUID = Guid.Parse(dr["XetNghiem_ManualGUID"].ToString());
                        ct.TestResult = dr["TestResult"].ToString();
                        ct.LamThem = Convert.ToBoolean(dr["LamThem"]);
                        ct.HasHutThuoc = Convert.ToBoolean(dr["HasHutThuoc"]);
                        ct.NgayXetNghiem = Convert.ToDateTime(dr["NgayXetNghiem"]);
                        ctkqxns.Add(ct);
                    }

                    Result result = KetQuaXetNghiemTayBus.InsertKQXN(_ketQuaXetNghiem, ctkqxns);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("XetNghiemTayBus.InsertKQXN"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiemTayBus.InsertKQXN"));
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

        private void OnAdd()
        {
            DataTable dtChiTietKQXN = dgChiTiet.DataSource as DataTable;
            if (dtChiTietKQXN == null) return;
            dlgAddChiTietKetQuaXetNghiemTay dlg = new dlgAddChiTietKetQuaXetNghiemTay(dtChiTietKQXN);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                DataRow newRow = dtChiTietKQXN.NewRow();
                newRow["Checked"] = false;
                newRow["ChiTietKetQuaXetNghiem_ManualGUID"] = Guid.NewGuid();
                newRow["XetNghiem_ManualGUID"] = dlg.XetNghiem_ManualGUID;
                newRow["TenXetNghiem"] = dlg.TenXetNghiem;
                newRow["Fullname"] = dlg.TenXetNghiem;
                newRow["GroupName"] = dlg.NhomXetNghiem;
                newRow["TestResult"] = dlg.TestResult;
                newRow["TinhTrang"] = (byte)TinhTrang.BinhThuong;
                newRow["LamThem"] = dlg.LamThem;
                newRow["NgayXetNghiem"] = dlg.NgayXetNghiem;
                newRow["HasHutThuoc"] = dlg.HasHutThuoc;
                dtChiTietKQXN.Rows.Add(newRow);
            }
        }

        private void OnEdit()
        {
            if (dgChiTiet.SelectedRows == null || dgChiTiet.SelectedRows.Count <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn 1 chi tiết kết quả xét nghiệm.", IconType.Information);
                return;
            }

            DataTable dtChiTietKQXN = dgChiTiet.DataSource as DataTable;
            if (dtChiTietKQXN == null) return;

            DataRow drChiTietKQXN = (dgChiTiet.SelectedRows[0].DataBoundItem as DataRowView).Row;
            if (drChiTietKQXN == null) return;
            dlgAddChiTietKetQuaXetNghiemTay dlg = new dlgAddChiTietKetQuaXetNghiemTay(dtChiTietKQXN, drChiTietKQXN);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                drChiTietKQXN["XetNghiem_ManualGUID"] = dlg.XetNghiem_ManualGUID;
                drChiTietKQXN["XetNghiem_ManualGUID"] = dlg.XetNghiem_ManualGUID;
                drChiTietKQXN["TenXetNghiem"] = dlg.TenXetNghiem;
                drChiTietKQXN["Fullname"] = dlg.TenXetNghiem;
                drChiTietKQXN["GroupName"] = dlg.NhomXetNghiem;
                drChiTietKQXN["TestResult"] = dlg.TestResult;
                drChiTietKQXN["TinhTrang"] = (byte)TinhTrang.BinhThuong;
                drChiTietKQXN["LamThem"] = dlg.LamThem;
                drChiTietKQXN["NgayXetNghiem"] = dlg.NgayXetNghiem;
                drChiTietKQXN["HasHutThuoc"] = dlg.HasHutThuoc;
            }
        }

        private void OnDelete()
        {
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgChiTiet.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                    deletedRows.Add(row);
            }

            if (deletedRows.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những chi tiết kết quả xét nghiệm mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    foreach (DataRow row in deletedRows)
                    {
                        dt.Rows.Remove(row);
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những chi tiết kết quả xét nghiệm cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddKetQuaXetNghiemTay_Load(object sender, EventArgs e)
        {
            if (!_isNew) DisplayInfo();
            else
            {
                if (_patientRow != null)
                {
                    txtBenhNhan.Tag = _patientRow["PatientGUID"].ToString();
                    txtBenhNhan.Text = _patientRow["FullName"].ToString();
                }

                Result result = KetQuaXetNghiemTayBus.GetChiTietKetQuaXetNghiem(Guid.Empty.ToString());
                if (result.IsOK) dgChiTiet.DataSource = result.QueryResult as DataTable;
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("KetQuaXetNghiemTayBus.GetChiTietKetQuaXetNghiem"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaXetNghiemTayBus.GetChiTietKetQuaXetNghiem"));
                }
            }
        }

        private void dlgAddKetQuaXetNghiemTay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
        }

        private void btnChonBenhNhan_Click(object sender, EventArgs e)
        {
            dlgSelectPatient dlg = new dlgSelectPatient();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                DataRow patientRow = dlg.PatientRow;
                if (patientRow != null)
                {
                    txtBenhNhan.Tag = patientRow["PatientGUID"].ToString();
                    txtBenhNhan.Text = patientRow["FullName"].ToString();
                }
            }
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgChiTiet.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void btnDeleteMember_Click(object sender, EventArgs e)
        {
            OnDelete();
        }

        private void dgChiTiet_DoubleClick(object sender, EventArgs e)
        {
            OnEdit();
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
