using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using MM.Dialogs;
using MM.Bussiness;
using MM.Databasae;
using MM.Common;

namespace MM.Controls
{
    public partial class uChuyenBenhAn : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        private DataRow _patientRow2 = null;
        #endregion

        #region Constructor
        public uChuyenBenhAn()
        {
            InitializeComponent();
            _uToaThuocList.EnableTextboxBenhNhan = false;
            InitData();
        }
        #endregion

        #region Properties
        public DataRow PatientRow
        {
            get { return _patientRow; }
            set 
            { 
                _patientRow = value;
                _uServiceHistory.PatientRow = value;
                _uToaThuocList.PatientRow = value;
                _uChiDinhList.PatientRow = value;
                _uCanDoList.PatientRow = value;
                _uLoiKhuyenList.PatientRow = value;
                _uKetQuaLamSangList.PatientRow = value;
                _uKetQuaCanLamSangList.PatientRow = value;
                _uKetLuanList.PatientRow = value;
                _uKetQuaNoiSoiList.PatientRow = value;
                _uKetQuaSoiCTCList.PatientRow = value;
                _uKetQuaSieuAmList.PatientRow = value;
            }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            _uServiceHistory.IsChuyenBenhAn = true;
            _uToaThuocList.IsChuyenBenhAn = true;
            _uChiDinhList.IsChuyenBenhAn = true;
            _uCanDoList.IsChuyenBenhAn = true;
            _uLoiKhuyenList.IsChuyenBenhAn = true;
            _uKetQuaLamSangList.IsChuyenBenhAn = true;
            _uKetQuaCanLamSangList.IsChuyenBenhAn = true;
            _uKetLuanList.IsChuyenBenhAn = true;
            _uKetQuaNoiSoiList.IsChuyenBenhAn = true;
            _uKetQuaSoiCTCList.IsChuyenBenhAn = true;
            _uKetQuaSieuAmList.IsChuyenBenhAn = true;
        }

        private void UpdateGUI()
        {
            _uServiceHistory.AllowChuyenKetQuaKham = AllowEdit;
            _uToaThuocList.AllowChuyenKetQuaKham = AllowEdit;
            _uChiDinhList.AllowChuyenKetQuaKham = AllowEdit;
            _uCanDoList.AllowChuyenKetQuaKham = AllowEdit;
            _uLoiKhuyenList.AllowChuyenKetQuaKham = AllowEdit;
            _uKetQuaLamSangList.AllowChuyenKetQuaKham = AllowEdit;
            _uKetQuaCanLamSangList.AllowChuyenKetQuaKham = AllowEdit;
            _uKetLuanList.AllowChuyenKetQuaKham = AllowEdit;
            _uKetQuaNoiSoiList.AllowChuyenKetQuaKham = AllowEdit;
            _uKetQuaSoiCTCList.AllowChuyenKetQuaKham = AllowEdit;
            _uKetQuaSieuAmList.AllowChuyenKetQuaKham = AllowEdit;
        }

        public void DisplayAsThread()
        {
            UpdateGUI();
            OnRefreshData();
        }

        public void ClearData()
        {
            _uServiceHistory.ClearData();
            _uToaThuocList.ClearData();
            _uChiDinhList.ClearChiDinhData();
            _uChiDinhList.ClearChiTietChiDinh();
            _uCanDoList.ClearData();
            _uKetQuaLamSangList.ClearData();
            _uKetQuaCanLamSangList.ClearData();
            _uLoiKhuyenList.ClearData();
            _uKetLuanList.ClearData();
            _uKetQuaNoiSoiList.ClearData();
            _uKetQuaSoiCTCList.ClearData();
            _uKetQuaSieuAmList.ClearData();
        }

        private void OnRefreshData()
        {
            if (_patientRow == null) return;

            DataRow row = _patientRow;
            if (tabServiceHistory.SelectedTabIndex == 0)
                _uServiceHistory.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 1)
                _uToaThuocList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 2)
                _uChiDinhList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 3)
                _uCanDoList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 4)
                _uKetQuaLamSangList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 5)
                _uKetQuaCanLamSangList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 6)
                _uLoiKhuyenList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 7)
                _uKetLuanList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 8)
                _uKetQuaNoiSoiList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 9)
                _uKetQuaSoiCTCList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 10)
                _uKetQuaSieuAmList.DisplayAsThread();
        }

        private void OnSetPatientChuyen()
        {
            _uServiceHistory.PatientRow = _patientRow;
            _uToaThuocList.PatientRow = _patientRow;
            _uChiDinhList.PatientRow = _patientRow;
            _uCanDoList.PatientRow = _patientRow;
            _uKetQuaLamSangList.PatientRow = _patientRow;
            _uKetQuaCanLamSangList.PatientRow = _patientRow;
            _uLoiKhuyenList.PatientRow = _patientRow;
            _uKetLuanList.PatientRow = _patientRow;
            _uKetQuaNoiSoiList.PatientRow = _patientRow;
            _uKetQuaSoiCTCList.PatientRow = _patientRow;
            _uKetQuaSieuAmList.PatientRow = _patientRow;
        }

        private void OnSetPatientNhan()
        {
            _uServiceHistory.PatientRow2 = _patientRow2;
            _uToaThuocList.PatientRow2 = _patientRow2;
            _uChiDinhList.PatientRow2 = _patientRow2;
            _uCanDoList.PatientRow2 = _patientRow2;
            _uKetQuaLamSangList.PatientRow2 = _patientRow2;
            _uKetQuaCanLamSangList.PatientRow2 = _patientRow2;
            _uLoiKhuyenList.PatientRow2 = _patientRow2;
            _uKetLuanList.PatientRow2 = _patientRow2;
            _uKetQuaNoiSoiList.PatientRow2 = _patientRow2;
            _uKetQuaSoiCTCList.PatientRow2 = _patientRow2;
            _uKetQuaSieuAmList.PatientRow2 = _patientRow2;
        }
        #endregion

        #region Window Event Handlers
        private void tabServiceHistory_SelectedTabChanged(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
        {
            OnRefreshData();
        }

        private void btnChonBenhNhanChuyen_Click(object sender, EventArgs e)
        {
            dlgSelectPatient dlg = new dlgSelectPatient(PatientSearchType.BenhNhan);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                if (_patientRow2 != null)
                {
                    string fileNum2 = _patientRow2["FileNum"].ToString().ToLower();
                    string fileNum = dlg.PatientRow["FileNum"].ToString().ToLower();

                    if (fileNum == fileNum2)
                    {
                        MsgBox.Show(Application.ProductName, "Bệnh nhân chuyển và bệnh nhân nhận phải khác nhau.", IconType.Information);
                        btnChonBenhNhanChuyen.Focus();
                        return;
                    }
                }

                _patientRow = dlg.PatientRow;

                if (_patientRow != null)
                {
                    txtFileNumChuyen.Text = _patientRow["FileNum"].ToString();
                    txtTenBenhNhanChuyen.Text = _patientRow["FullName"].ToString();
                    txtNgaySinhChuyen.Text = _patientRow["DobStr"].ToString();
                    txtGioiTinhChuyen.Text = _patientRow["GenderAsStr"].ToString();
                    OnSetPatientChuyen();
                    OnRefreshData();
                }
            }
        }

        private void btnChonBenhNhanNhan_Click(object sender, EventArgs e)
        {
            dlgSelectPatient dlg = new dlgSelectPatient(PatientSearchType.BenhNhan);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                if (_patientRow != null)
                {
                    string fileNum = _patientRow["FileNum"].ToString().ToLower();
                    string fileNum2 = dlg.PatientRow["FileNum"].ToString().ToLower();

                    if (fileNum == fileNum2)
                    {
                        MsgBox.Show(Application.ProductName, "Bệnh nhân chuyển và bệnh nhân nhận phải khác nhau.", IconType.Information);
                        btnChonBenhNhanNhan.Focus();
                        return;
                    }
                }

                _patientRow2 = dlg.PatientRow;
                if (_patientRow2 != null)
                {
                    txtFileNumNhan.Text = _patientRow2["FileNum"].ToString();
                    txtTenBenhNhanNhan.Text = _patientRow2["FullName"].ToString();
                    txtNgaySinhNhan.Text = _patientRow2["DobStr"].ToString();
                    txtGioiTinhNhan.Text = _patientRow2["GenderAsStr"].ToString();

                    OnSetPatientNhan();
                }
            }
        }
        #endregion

        #region Working Thread

        #endregion
    }
}
