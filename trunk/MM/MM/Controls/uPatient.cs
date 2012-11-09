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
using MM.Databasae;
using MM.Common;

namespace MM.Controls
{
    public partial class uPatient : uBase
    {
        #region Members
        private object _patientRow = null;
        private bool _isCallDisplayInfo = false;
        #endregion

        #region Constructor
        public uPatient()
        {
            InitializeComponent();
            _uServiceHistory.OnServiceHistoryChanged += new ServiceHistoryChangedHandler(_uServiceHistory_OnServiceHistoryChanged);
            _uDailyServiceHistory.OnServiceHistoryChanged += new ServiceHistoryChangedHandler(_uServiceHistory_OnServiceHistoryChanged);
            _uServiceHistory.OnExportReceiptChanged += new ExportReceiptChangedHandler(_uServiceHistory_OnExportReceiptChanged);
            _uDailyServiceHistory.OnExportReceiptChanged += new ExportReceiptChangedHandler(_uServiceHistory_OnExportReceiptChanged);
            _uServiceHistory.OnRefreshCheckList += new RefreshCheckListHandler(_uServiceHistory_OnRefreshCheckList);
            _uDailyServiceHistory.OnRefreshCheckList += new RefreshCheckListHandler(_uServiceHistory_OnRefreshCheckList);
            this.HandleCreated += new EventHandler(uPatient_HandleCreated);
        }

        private void _uServiceHistory_OnRefreshCheckList()
        {
            DisplayCheckListAsThread();
        }

        private void _uServiceHistory_OnExportReceiptChanged()
        {
            DisplayCheckListAsThread();
        }

        private void uPatient_HandleCreated(object sender, EventArgs e)
        {
            DisplayInfo();
        }
        #endregion

        #region Properties
        public object PatientRow
        {
            get { return _patientRow; }
            set 
            { 
                _patientRow = value;
                _uServiceHistory.PatientRow = value;
                _uDailyServiceHistory.PatientRow = value;
                _uToaThuocList.PatientRow = (DataRow)value;
                _uChiDinhList.PatientRow = (DataRow)value;
                _uCanDoList.PatientRow = (DataRow)value;
                _uLoiKhuyenList.PatientRow = (DataRow)value;
                _uKetQuaLamSangList.PatientRow = (DataRow)value;
                _uKetQuaCanLamSangList.PatientRow = (DataRow)value;
                _uKetLuanList.PatientRow = (DataRow)value;
                _uKetQuaNoiSoiList.PatientRow = (DataRow)value;
                _uKetQuaSoiCTCList.PatientRow = (DataRow)value;
                _uKetQuaSieuAmList.PatientRow = (DataRow)value;
            }
        }
        #endregion

        #region UI Command
        public void DisplayInfo()
        {
            if (!this.IsHandleCreated)
            {
                _isCallDisplayInfo = true;
                return;
            }
            else
                _isCallDisplayInfo = false;

            if (_patientRow == null) return;

            DataRow row = _patientRow as DataRow;

            txtFullName.Text = row["FullName"].ToString();
            txtGender.Text = row["GenderAsStr"].ToString();
            txtDOB.Text ="NS: " + row["DobStr"].ToString();
            txtAge.Text = Utility.GetAge(row["DobStr"].ToString()).ToString() + " tuổi";
            txtIdentityCard.Text ="CMND: " +  row["IdentityCard"].ToString();
            txtWorkPhone.Text = "ĐT: "  + row["WorkPhone"].ToString();
            txtMobile.Text = "DĐ: " + row["Mobile"].ToString();
            txtEmail.Text ="Email: " + row["Email"].ToString();
            txtFullAddress.Text ="Địa chỉ: " + row["Address"].ToString();
            txtThuocDiUng.Text = row["Thuoc_Di_Ung"].ToString();

            btnThemYKienKhachHang.Enabled = Global.AllowAddYKienKhachHang;
            
            pageDailyService.Visible = Global.AllowViewDichVuDaSuDung;
            pageServiceHistory.Visible = Global.AllowViewDichVuDaSuDung;
            pageKeToa.Visible = Global.AllowViewKeToa;
            pageChiDinh.Visible = Global.AllowViewChiDinh;
            pageCanDo.Visible = Global.AllowViewCanDo;
            pageKhamLamSang.Visible = Global.AllowViewKhamLamSang;
            pageCanLamSang.Visible = Global.AllowViewCanLamSang;
            pageLoiKhuyen.Visible = Global.AllowViewLoiKhuyen;
            pageKetLuan.Visible = Global.AllowViewKetLuan;
            pageKhamNoiSoi.Visible = Global.AllowViewKhamNoiSoi;
            pageKhamCTC.Visible = Global.AllowViewKhamCTC;
            pageKetQuaSieuAm.Visible = Global.AllowViewSieuAm;

            _uToaThuocList.AllowAdd = Global.AllowAddKeToa;
            _uToaThuocList.AllowEdit = Global.AllowEditKeToa;
            _uToaThuocList.AllowDelete = Global.AllowDeleteKeToa;
            _uToaThuocList.AllowPrint = Global.AllowPrintKeToa;

            OnRefreshData();
            
        }

        private void GetNgayLienHeBenhNhanGanNhat(string patientGUID)
        {
            Result result = YKienKhachHangBus.GetNgayLienHeBenhNhanGanNhat(patientGUID);
            if (result.IsOK)
            {
                if (result.QueryResult != null)
                {
                    DateTime dt = Convert.ToDateTime(result.QueryResult);
                    txtNgayLienHeGanNhat.Text = dt.ToString("dd/MM/yyyy HH:mm:ss");
                }
                else
                    txtNgayLienHeGanNhat.Text = string.Empty;
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("YKienKhachHangBus.GetNgayLienHeBenhNhanGanNhat"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("YKienKhachHangBus.GetCompanyMemberList"));
            }
        }

        private void OnRefreshData()
        {
            DisplayCheckListAsThread();

            DataRow row = _patientRow as DataRow;
            GetNgayLienHeBenhNhanGanNhat(row["PatientGUID"].ToString());

            if (tabServiceHistory.SelectedTabIndex == 0)
                _uDailyServiceHistory.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 1)
                _uServiceHistory.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 2)
                _uToaThuocList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 3)
                _uChiDinhList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 4)
                _uCanDoList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 5)
                _uKetQuaLamSangList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 6)
                _uKetQuaCanLamSangList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 7)
                _uLoiKhuyenList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 8)
                _uKetLuanList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 9)
                _uKetQuaNoiSoiList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 10)
                _uKetQuaSoiCTCList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 11)
                _uKetQuaSieuAmList.DisplayAsThread();
        }

        public void DisplayCheckListAsThread()
        {
            try
            {
                lvService.Items.Clear();
                string patientGUID = (_patientRow as DataRow)["PatientGUID"].ToString();
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayCheckListProc), patientGUID);
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

        private void OnDisplayCheckList(string patientGUID)
        {
            Result result = CompanyContractBus.GetCheckListByPatient(patientGUID);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    DataTable dt = result.QueryResult as DataTable;
                    lvService.Visible = dt.Rows.Count > 0 ? true : false;

                    foreach (DataRow row in dt.Rows)
                    {
                        string code = row["Code"].ToString();
                        string name = row["Name"].ToString();
                        string nguoiNhanCN = row["NguoiChuyenNhuong"].ToString();
                        bool isChecked = Convert.ToBoolean(row["Checked"]);
                        int imgIndex = isChecked ? 0 : 1;

                        ListViewItem item = new ListViewItem(string.Empty, imgIndex);
                        item.SubItems.Add(code);
                        item.SubItems.Add(name);
                        item.SubItems.Add(nguoiNhanCN);
                        lvService.Items.Add(item);
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                lvService.Visible = false;
                MsgBox.Show(this.Text, result.GetErrorAsString("CompanyBus.GetCompanyMemberList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyBus.GetCompanyMemberList"));
            }
        }

        private void btnThemYKienKhachHang_Click(object sender, EventArgs e)
        {
            dlgAddYKienKhachHang dlg = new dlgAddYKienKhachHang(_patientRow);
            dlg.ShowDialog(this);
        }
        #endregion

        #region Window Event Handlers
        private void uPatient_Load(object sender, EventArgs e)
        {
        }

        private void _uServiceHistory_OnServiceHistoryChanged()
        {
            _uServiceHistory.DisplayAsThread();
            _uDailyServiceHistory.DisplayAsThread();
            DisplayCheckListAsThread();
        }

        private void txtThuocDiUng_DoubleClick(object sender, EventArgs e)
        {
            dlgPatientHistory dlg = new dlgPatientHistory((DataRow)_patientRow);
            dlg.ShowDialog(this);
        }

        private void tabServiceHistory_SelectedTabChanged(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
        {
            if (tabServiceHistory.SelectedTabIndex == 0)
                _uDailyServiceHistory.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 1)
                _uServiceHistory.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 2)
                _uToaThuocList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 3)
                _uChiDinhList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 4)
                _uCanDoList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 5)
                _uKetQuaLamSangList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 6)
                _uKetQuaCanLamSangList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 7)
                _uLoiKhuyenList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 8)
                _uKetLuanList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 9)
                _uKetQuaNoiSoiList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 10)
                _uKetQuaSoiCTCList.DisplayAsThread();
            else if (tabServiceHistory.SelectedTabIndex == 11)
                _uKetQuaSieuAmList.DisplayAsThread();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            OnRefreshData();
        }
        #endregion

        #region Working Thread
        private void OnDisplayCheckListProc(object state)
        {
            try
            {
                OnDisplayCheckList(state.ToString());
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
