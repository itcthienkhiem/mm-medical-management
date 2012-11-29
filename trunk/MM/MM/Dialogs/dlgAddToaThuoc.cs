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
    public partial class dlgAddToaThuoc : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private DataRow _drToaThuoc = null;
        private ToaThuoc _toaThuoc = new ToaThuoc();
        private List<string> _deletedKeys = new List<string>();
        private DataRow _patientRow = null;
        private bool _allowEdit = true;
        #endregion

        #region Constructor
        public dlgAddToaThuoc()
        {
            InitializeComponent();
            GenerateCode();
        }

        public dlgAddToaThuoc(DataRow drToaThuoc, bool allowEdit)
        {
            InitializeComponent();
            _isNew = false;
            _allowEdit = allowEdit;
            this.Text = "Sua toa thuoc";
            _drToaThuoc = drToaThuoc;
        }
        #endregion

        #region Properties
        public ToaThuoc ToaThuoc
        {
            get { return _toaThuoc; }
        }

        public string TenBacSi
        {
            get { return cboBacSi.Text; }
        }

        public string TenBenhNhan
        {
            get { return txtTenBenhNhan.Text; }
        }

        public string NgaySinh
        {
            get { return txtNgaySinh.Text; }
        }

        public string GioiTinh
        {
            get { return txtGioiTinh.Text; }
        }

        public string DiaChi
        {
            get { return txtDiaChi.Text; }
        }

        public string DienThoai
        {
            get { return txtDienThoai.Text; }
        }

        public DataRow PatientRow
        {
            get { return _patientRow; }
            set 
            { 
                _patientRow = value;
                if (_patientRow != null)
                {
                    txtTenBenhNhan.Tag = _patientRow["PatientGUID"].ToString();
                    txtTenBenhNhan.Text = _patientRow["FullName"].ToString();
                    txtNgaySinh.Text = _patientRow["DobStr"].ToString();
                    txtGioiTinh.Text = _patientRow["GenderAsStr"].ToString();
                    txtDienThoai.Text = _patientRow["Mobile"].ToString();
                    txtDiaChi.Text = _patientRow["Address"].ToString();
                    btnChonBenhNhan.Visible = false;

                    if (Global.StaffType == StaffType.BacSi || Global.StaffType == StaffType.BacSiSieuAm ||
                        Global.StaffType == StaffType.BacSiNgoaiTongQuat || Global.StaffType == StaffType.BacSiNoiTongQuat ||
                        Global.StaffType == StaffType.BacSiPhuKhoa)
                    {
                        cboBacSi.Enabled = false;
                    }
                }
            }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            Cursor.Current = Cursors.WaitCursor;
            dtpkNgayKham.Value = DateTime.Now;
            dtpkNgayTaiKham.Value = DateTime.Now.AddDays(7);
            OnDisplayBacSi();
        }

        private void OnDisplayBacSi()
        {
            List<byte> staffTypes = new List<byte>();
            staffTypes.Add((byte)StaffType.BacSi);
            staffTypes.Add((byte)StaffType.BacSiSieuAm);
            staffTypes.Add((byte)StaffType.BacSiNgoaiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiNoiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiPhuKhoa);
            Result result = DocStaffBus.GetDocStaffList(staffTypes);
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                cboBacSi.DataSource = dt;

                DataRow[] rows = dt.Select(string.Format("DocStaffGUID='{0}'", Global.UserGUID));
                if (rows != null && rows.Length > 0)
                    cboBacSi.SelectedValue = Global.UserGUID;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
            }
        }

        private void OnGetChiTietToaThuoc(string toaThuocGUID)
        {
            Result result = KeToaBus.GetChiTietToaThuocList(toaThuocGUID);
            if (result.IsOK)
            {
                dgChiTiet.DataSource = result.QueryResult;
                //RefreshNo();
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KeToaBus.GetChiTietToaThuocList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KeToaBus.GetChiTietToaThuocList"));
            }
        }

        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = KeToaBus.GetToaThuocCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                txtMaToaThuoc.Text = Utility.GetCode("KT", count + 1, 7);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KeToaBus.GetToaThuocCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("KeToaBus.GetToaThuocCount"));
            }
        }

        private void DisplayInfo(DataRow drToaThuoc)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                txtMaToaThuoc.Text = drToaThuoc["MaToaThuoc"] as string;

                if (drToaThuoc["NgayKham"] != null && drToaThuoc["NgayKham"] != DBNull.Value)
                    dtpkNgayKham.Value = Convert.ToDateTime(drToaThuoc["NgayKham"]);

                if (drToaThuoc["NgayTaiKham"] != null && drToaThuoc["NgayTaiKham"] != DBNull.Value)
                {
                    dtpkNgayTaiKham.Value = Convert.ToDateTime(drToaThuoc["NgayTaiKham"]);
                    chkNgayTaiKham.Checked = true;
                }
                else
                    chkNgayTaiKham.Checked = false;

                LoaiToaThuoc loai = (LoaiToaThuoc)Convert.ToByte(drToaThuoc["Loai"]);
                if (loai == LoaiToaThuoc.Chung) raToaChung.Checked = true;
                else raToaSanKhoa.Checked = true;

                cboBacSi.SelectedValue = drToaThuoc["BacSiKeToa"].ToString();
                txtTenBenhNhan.Text = drToaThuoc["TenBenhNhan"].ToString();
                txtTenBenhNhan.Tag = drToaThuoc["BenhNhan"].ToString();
                txtNgaySinh.Text = drToaThuoc["DobStr"].ToString();
                txtGioiTinh.Text = drToaThuoc["GenderAsStr"].ToString();
                if (drToaThuoc["Address"] != null && drToaThuoc["Address"] != DBNull.Value)
                    txtDiaChi.Text = drToaThuoc["Address"].ToString();

                if (drToaThuoc["Mobile"] != null && drToaThuoc["Mobile"] != DBNull.Value)
                    txtDienThoai.Text = drToaThuoc["Mobile"].ToString();

                if (drToaThuoc["ChanDoan"] != null && drToaThuoc["ChanDoan"] != DBNull.Value)
                    txtChanDoan.Text = drToaThuoc["ChanDoan"].ToString();

                txtGhiChu.Text = drToaThuoc["Note"] as string;

                _toaThuoc.ToaThuocGUID = Guid.Parse(drToaThuoc["ToaThuocGUID"].ToString());

                if (drToaThuoc["CreatedDate"] != null && drToaThuoc["CreatedDate"] != DBNull.Value)
                    _toaThuoc.CreatedDate = Convert.ToDateTime(drToaThuoc["CreatedDate"]);

                if (drToaThuoc["CreatedBy"] != null && drToaThuoc["CreatedBy"] != DBNull.Value)
                    _toaThuoc.CreatedBy = Guid.Parse(drToaThuoc["CreatedBy"].ToString());

                if (drToaThuoc["UpdatedDate"] != null && drToaThuoc["UpdatedDate"] != DBNull.Value)
                    _toaThuoc.UpdatedDate = Convert.ToDateTime(drToaThuoc["UpdatedDate"]);

                if (drToaThuoc["UpdatedBy"] != null && drToaThuoc["UpdatedBy"] != DBNull.Value)
                    _toaThuoc.UpdatedBy = Guid.Parse(drToaThuoc["UpdatedBy"].ToString());

                if (drToaThuoc["DeletedDate"] != null && drToaThuoc["DeletedDate"] != DBNull.Value)
                    _toaThuoc.DeletedDate = Convert.ToDateTime(drToaThuoc["DeletedDate"]);

                if (drToaThuoc["DeletedBy"] != null && drToaThuoc["DeletedBy"] != DBNull.Value)
                    _toaThuoc.DeletedBy = Guid.Parse(drToaThuoc["DeletedBy"].ToString());

                _toaThuoc.Status = Convert.ToByte(drToaThuoc["Status"]);

                OnGetChiTietToaThuoc(_toaThuoc.ToaThuocGUID.ToString());

                if (!_allowEdit)
                {
                    btnOK.Enabled = _allowEdit;
                    groupBox1.Enabled = _allowEdit;
                    btnAddMember.Enabled = _allowEdit;
                    btnEdit.Enabled = _allowEdit;
                    btnDeleteMember.Enabled = _allowEdit;
                }
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (txtMaToaThuoc.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã toa thuốc.", IconType.Information);
                txtMaToaThuoc.Focus();
                return false;
            }

            if (cboBacSi.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ kê toa.", IconType.Information);
                cboBacSi.Focus();
                return false;
            }

            if (txtTenBenhNhan.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bệnh nhân.", IconType.Information);
                btnChonBenhNhan.Focus();
                return false;
            }


            string toaThuocGUID = _isNew ? string.Empty : _toaThuoc.ToaThuocGUID.ToString();
            Result result = KeToaBus.CheckToaThuocExistCode(toaThuocGUID, txtMaToaThuoc.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Mã toa thuốc này đã tồn tại rồi. Vui lòng nhập mã khác.", IconType.Information);
                    txtMaToaThuoc.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("KeToaBus.CheckToaThuocExistCode"), IconType.Error);
                return false;
            }

            if (dgChiTiet.RowCount <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng kê toa ít nhất 1 thuốc.", IconType.Information);
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
                    _toaThuoc.MaToaThuoc = txtMaToaThuoc.Text;
                    _toaThuoc.NgayKeToa = DateTime.Now;
                    _toaThuoc.NgayKham = dtpkNgayKham.Value;

                    if (chkNgayTaiKham.Checked)
                        _toaThuoc.NgayTaiKham = dtpkNgayTaiKham.Value;
                    else
                        _toaThuoc.NgayTaiKham = null;

                    _toaThuoc.BacSiKeToa = Guid.Parse(cboBacSi.SelectedValue.ToString());
                    _toaThuoc.BenhNhan = Guid.Parse(txtTenBenhNhan.Tag.ToString());
                    _toaThuoc.ChanDoan = txtChanDoan.Text;
                    _toaThuoc.Note = txtGhiChu.Text;
                    _toaThuoc.Status = (byte)Status.Actived;
                    _toaThuoc.Loai = raToaChung.Checked ? (byte)LoaiToaThuoc.Chung : (byte)LoaiToaThuoc.SanKhoa;

                    if (_isNew)
                    {
                        _toaThuoc.CreatedDate = DateTime.Now;
                        _toaThuoc.CreatedBy = Guid.Parse(Global.UserGUID);
                    }
                    else
                    {
                        _toaThuoc.UpdatedDate = DateTime.Now;
                        _toaThuoc.UpdatedBy = Guid.Parse(Global.UserGUID);
                    }

                    DataTable dt = dgChiTiet.DataSource as DataTable;
                    List<ChiTietToaThuoc> addedList = new List<ChiTietToaThuoc>();
                    foreach (DataRow row in dt.Rows)
                    {
                        ChiTietToaThuoc cttt = new ChiTietToaThuoc();
                        if (row["ChiTietToaThuocGUID"] != null && row["ChiTietToaThuocGUID"] != DBNull.Value)
                        {
                            cttt.ChiTietToaThuocGUID = Guid.Parse(row["ChiTietToaThuocGUID"].ToString());
                            cttt.UpdatedDate = DateTime.Now;
                            cttt.UpdatedBy = Guid.Parse(Global.UserGUID);
                        }
                        else
                        {
                            cttt.CreatedDate = DateTime.Now;
                            cttt.CreatedBy = Guid.Parse(Global.UserGUID);
                        }

                        cttt.ThuocGUID = Guid.Parse(row["ThuocGUID"].ToString());
                        cttt.SoLuong = Convert.ToInt32(row["SoLuong"]);
                        cttt.LieuDung = row["LieuDung"].ToString();
                        cttt.Note = row["Note"].ToString();
                        cttt.Sang = Convert.ToBoolean(row["Sang"]);
                        cttt.SangNote = row["SangNote"].ToString();
                        cttt.Trua = Convert.ToBoolean(row["Trua"]);
                        cttt.TruaNote = row["TruaNote"].ToString();
                        cttt.Chieu = Convert.ToBoolean(row["Chieu"]);
                        cttt.ChieuNote = row["ChieuNote"].ToString();
                        cttt.Toi = Convert.ToBoolean(row["Toi"]);
                        cttt.ToiNote = row["ToiNote"].ToString();
                        cttt.TruocAn = Convert.ToBoolean(row["TruocAn"]);
                        cttt.TruocAnNote = row["TruocAnNote"].ToString();
                        cttt.SauAn = Convert.ToBoolean(row["SauAn"]);
                        cttt.SauAnNote = row["SauAnNote"].ToString();
                        cttt.Khac_TruocSauAn = Convert.ToBoolean(row["Khac_TruocSauAn"]);
                        cttt.Khac_TruocSauAnNote = row["Khac_TruocSauAnNote"].ToString();
                        cttt.Uong = Convert.ToBoolean(row["Uong"]);
                        cttt.UongNote = row["UongNote"].ToString();
                        cttt.Boi = Convert.ToBoolean(row["Boi"]);
                        cttt.BoiNote = row["BoiNote"].ToString();
                        cttt.Dat = Convert.ToBoolean(row["Dat"]);
                        cttt.DatNote = row["DatNote"].ToString();
                        cttt.Khac_CachDung = Convert.ToBoolean(row["Khac_CachDung"]);
                        cttt.Khac_CachDungNote = row["Khac_CachDungNote"].ToString();

                        cttt.Status = (byte)Status.Actived;
                        addedList.Add(cttt);
                    }

                    Result result = KeToaBus.InsertToaThuoc(_toaThuoc, addedList, _deletedKeys);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("KeToaBus.InsertToaThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KeToaBus.InsertToaThuoc"));
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

        private void SelectLastedRow()
        {
            dgChiTiet.CurrentCell = dgChiTiet[1, dgChiTiet.RowCount - 1];
            dgChiTiet.Rows[dgChiTiet.RowCount - 1].Selected = true;
        }

        private void OnAddThuoc()
        {
            LoaiToaThuoc type = raToaChung.Checked ? LoaiToaThuoc.Chung : LoaiToaThuoc.SanKhoa;
            dlgAddThuocKeToa dlg = new dlgAddThuocKeToa(type);
            DataTable dt = (DataTable)dgChiTiet.DataSource;
            dlg.DataSource = dt; 
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                DataRow newRow = dt.NewRow();
                newRow["Checked"] = false;
                newRow["ChiTietToaThuocGUID"] = dlg.ChiTietToaThuoc.ChiTietToaThuocGUID;
                newRow["ThuocGUID"] = dlg.ChiTietToaThuoc.ThuocGUID;
                newRow["TenThuoc"] = dlg.TenThuoc;
                newRow["SoLuong"] = dlg.ChiTietToaThuoc.SoLuong;
                newRow["Sang"] = dlg.ChiTietToaThuoc.Sang;
                newRow["SangNote"] = dlg.ChiTietToaThuoc.SangNote;
                newRow["Trua"] = dlg.ChiTietToaThuoc.Trua;
                newRow["TruaNote"] = dlg.ChiTietToaThuoc.TruaNote;
                newRow["Chieu"] = dlg.ChiTietToaThuoc.Chieu;
                newRow["ChieuNote"] = dlg.ChiTietToaThuoc.ChieuNote;
                newRow["Toi"] = dlg.ChiTietToaThuoc.Toi;
                newRow["ToiNote"] = dlg.ChiTietToaThuoc.ToiNote;
                newRow["TruocAn"] = dlg.ChiTietToaThuoc.TruocAn;
                newRow["TruocAnNote"] = dlg.ChiTietToaThuoc.TruocAnNote;
                newRow["SauAn"] = dlg.ChiTietToaThuoc.SauAn;
                newRow["SauAnNote"] = dlg.ChiTietToaThuoc.SauAnNote;
                newRow["Khac_TruocSauAn"] = dlg.ChiTietToaThuoc.Khac_TruocSauAn;
                newRow["Khac_TruocSauAnNote"] = dlg.ChiTietToaThuoc.Khac_TruocSauAnNote;
                newRow["Uong"] = dlg.ChiTietToaThuoc.Uong;
                newRow["UongNote"] = dlg.ChiTietToaThuoc.UongNote;
                newRow["Boi"] = dlg.ChiTietToaThuoc.Boi;
                newRow["BoiNote"] = dlg.ChiTietToaThuoc.BoiNote;
                newRow["Dat"] = dlg.ChiTietToaThuoc.Dat;
                newRow["DatNote"] = dlg.ChiTietToaThuoc.DatNote;
                newRow["Khac_CachDung"] = dlg.ChiTietToaThuoc.Khac_CachDung;
                newRow["Khac_CachDungNote"] = dlg.ChiTietToaThuoc.Khac_CachDungNote;
                newRow["LieuDung"] = dlg.ChiTietToaThuoc.LieuDung;
                newRow["Note"] = dlg.ChiTietToaThuoc.Note;

                if (dlg.ChiTietToaThuoc.CreatedDate.HasValue)
                    newRow["CreatedDate"] = dlg.ChiTietToaThuoc.CreatedDate;

                if (dlg.ChiTietToaThuoc.CreatedBy.HasValue)
                    newRow["CreatedBy"] = dlg.ChiTietToaThuoc.CreatedBy.ToString();

                if (dlg.ChiTietToaThuoc.UpdatedDate.HasValue)
                    newRow["UpdatedDate"] = dlg.ChiTietToaThuoc.UpdatedDate;

                if (dlg.ChiTietToaThuoc.UpdatedBy.HasValue)
                    newRow["UpdatedBy"] = dlg.ChiTietToaThuoc.UpdatedBy.ToString();

                if (dlg.ChiTietToaThuoc.DeletedDate.HasValue)
                    newRow["DeletedDate"] = dlg.ChiTietToaThuoc.DeletedDate;

                if (dlg.ChiTietToaThuoc.DeletedBy.HasValue)
                    newRow["DeletedBy"] = dlg.ChiTietToaThuoc.DeletedBy.ToString();

                newRow["ChiTietToaThuocStatus"] = dlg.ChiTietToaThuoc.Status;

                dt.Rows.Add(newRow);
                //SelectLastedRow();
            }
        }

        private void OnEditThuoc()
        {
            if (dgChiTiet.SelectedRows == null || dgChiTiet.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 thuốc.", IconType.Information);
                return;
            }

            LoaiToaThuoc type = raToaChung.Checked ? LoaiToaThuoc.Chung : LoaiToaThuoc.SanKhoa;
            DataRow drThuoc = (dgChiTiet.SelectedRows[0].DataBoundItem as DataRowView).Row;
            dlgAddThuocKeToa dlg = new dlgAddThuocKeToa(drThuoc, type);
            DataTable dt = (DataTable)dgChiTiet.DataSource;
            dlg.DataSource = dt;
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                drThuoc["ThuocGUID"] = dlg.ChiTietToaThuoc.ThuocGUID;
                drThuoc["TenThuoc"] = dlg.TenThuoc;
                drThuoc["SoLuong"] = dlg.ChiTietToaThuoc.SoLuong;
                drThuoc["Sang"] = dlg.ChiTietToaThuoc.Sang;
                drThuoc["SangNote"] = dlg.ChiTietToaThuoc.SangNote;
                drThuoc["Trua"] = dlg.ChiTietToaThuoc.Trua;
                drThuoc["TruaNote"] = dlg.ChiTietToaThuoc.TruaNote;
                drThuoc["Chieu"] = dlg.ChiTietToaThuoc.Chieu;
                drThuoc["ChieuNote"] = dlg.ChiTietToaThuoc.ChieuNote;
                drThuoc["Toi"] = dlg.ChiTietToaThuoc.Toi;
                drThuoc["ToiNote"] = dlg.ChiTietToaThuoc.ToiNote;
                drThuoc["TruocAn"] = dlg.ChiTietToaThuoc.TruocAn;
                drThuoc["TruocAnNote"] = dlg.ChiTietToaThuoc.TruocAnNote;
                drThuoc["SauAn"] = dlg.ChiTietToaThuoc.SauAn;
                drThuoc["SauAnNote"] = dlg.ChiTietToaThuoc.SauAnNote;
                drThuoc["Khac_TruocSauAn"] = dlg.ChiTietToaThuoc.Khac_TruocSauAn;
                drThuoc["Khac_TruocSauAnNote"] = dlg.ChiTietToaThuoc.Khac_TruocSauAnNote;
                drThuoc["Uong"] = dlg.ChiTietToaThuoc.Uong;
                drThuoc["UongNote"] = dlg.ChiTietToaThuoc.UongNote;
                drThuoc["Boi"] = dlg.ChiTietToaThuoc.Boi;
                drThuoc["BoiNote"] = dlg.ChiTietToaThuoc.BoiNote;
                drThuoc["Dat"] = dlg.ChiTietToaThuoc.Dat;
                drThuoc["DatNote"] = dlg.ChiTietToaThuoc.DatNote;
                drThuoc["Khac_CachDung"] = dlg.ChiTietToaThuoc.Khac_CachDung;
                drThuoc["Khac_CachDungNote"] = dlg.ChiTietToaThuoc.Khac_CachDungNote;
                drThuoc["LieuDung"] = dlg.ChiTietToaThuoc.LieuDung;
                drThuoc["Note"] = dlg.ChiTietToaThuoc.Note;

                if (dlg.ChiTietToaThuoc.CreatedDate.HasValue)
                    drThuoc["CreatedDate"] = dlg.ChiTietToaThuoc.CreatedDate;

                if (dlg.ChiTietToaThuoc.CreatedBy.HasValue)
                    drThuoc["CreatedBy"] = dlg.ChiTietToaThuoc.CreatedBy.ToString();

                if (dlg.ChiTietToaThuoc.UpdatedDate.HasValue)
                    drThuoc["UpdatedDate"] = dlg.ChiTietToaThuoc.UpdatedDate;

                if (dlg.ChiTietToaThuoc.UpdatedBy.HasValue)
                    drThuoc["UpdatedBy"] = dlg.ChiTietToaThuoc.UpdatedBy.ToString();

                if (dlg.ChiTietToaThuoc.DeletedDate.HasValue)
                    drThuoc["DeletedDate"] = dlg.ChiTietToaThuoc.DeletedDate;

                if (dlg.ChiTietToaThuoc.DeletedBy.HasValue)
                    drThuoc["DeletedBy"] = dlg.ChiTietToaThuoc.DeletedBy.ToString();

                drThuoc["ChiTietToaThuocStatus"] = dlg.ChiTietToaThuoc.Status;
            }
        }

        private void OnDeleteThuoc()
        {
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgChiTiet.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedRows.Add(row);
                }
            }

            if (deletedRows.Count > 0)
            {
                if (MsgBox.Question(this.Text, "Bạn có muốn xóa những thuốc mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    
                    foreach (DataRow row in deletedRows)
                    {
                        if (row["ChiTietToaThuocGUID"] == null || row["ChiTietToaThuocGUID"] == DBNull.Value) continue;
                        string chiTietToaThuocGUID = row["ChiTietToaThuocGUID"].ToString();
                        if (!_deletedKeys.Contains(chiTietToaThuocGUID))
                            _deletedKeys.Add(chiTietToaThuocGUID);

                        dt.Rows.Remove(row);
                    }
                }
            }
            else
                MsgBox.Show(this.Text, "Vui lòng đánh dấu những thuốc cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddToaThuoc_Load(object sender, EventArgs e)
        {
            InitData();
            if (_isNew)
                OnGetChiTietToaThuoc(Guid.Empty.ToString());
            else
                DisplayInfo(_drToaThuoc);    
        }

        private void dlgAddToaThuoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
            else if (_allowEdit)
            {
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin toa thuốc ?") == System.Windows.Forms.DialogResult.Yes)
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

        private void btnChonBenhNhan_Click(object sender, EventArgs e)
        {
            dlgSelectPatient dlg = new dlgSelectPatient(PatientSearchType.BenhNhan);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                _patientRow = dlg.PatientRow;
                if (_patientRow != null)
                {
                    txtTenBenhNhan.Tag = _patientRow["PatientGUID"].ToString();
                    txtTenBenhNhan.Text = _patientRow["FullName"].ToString();
                    txtNgaySinh.Text = _patientRow["DobStr"].ToString();
                    txtGioiTinh.Text = _patientRow["GenderAsStr"].ToString();
                    txtDienThoai.Text = _patientRow["Mobile"].ToString();
                    txtDiaChi.Text = _patientRow["Address"].ToString();
                }
            }
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            OnAddThuoc();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditThuoc();
        }

        private void btnDeleteMember_Click(object sender, EventArgs e)
        {
            OnDeleteThuoc();
        }

        private void dgChiTiet_DoubleClick(object sender, EventArgs e)
        {
            OnEditThuoc();
        }

        private void chkNgayTaiKham_CheckedChanged(object sender, EventArgs e)
        {
            dtpkNgayTaiKham.Enabled = chkNgayTaiKham.Checked;
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
