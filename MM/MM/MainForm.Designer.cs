namespace MM
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                CloseAllCOMPort();

                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this._mainToolbar = new System.Windows.Forms.ToolStrip();
            this.tbLogin = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tbServiceList = new System.Windows.Forms.ToolStripButton();
            this.tbNhomDichVu = new System.Windows.Forms.ToolStripButton();
            this.tbGiaVonDichVu = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tbSpecialityList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tbDoctorList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbOpenPatient = new System.Windows.Forms.ToolStripButton();
            this.tbPatientList = new System.Windows.Forms.ToolStripButton();
            this.tbDuplicatePatient = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tbCompanyList = new System.Windows.Forms.ToolStripButton();
            this.tbContractList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tbReceiptList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.tbInvoiceList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.tbSympton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.tbDanhMucThuoc = new System.Windows.Forms.ToolStripButton();
            this.tbNhomThuoc = new System.Windows.Forms.ToolStripButton();
            this.tbLoThuoc = new System.Windows.Forms.ToolStripButton();
            this.tbGiaThuoc = new System.Windows.Forms.ToolStripButton();
            this.tbKeToa = new System.Windows.Forms.ToolStripButton();
            this.tbPhieuThuThuoc = new System.Windows.Forms.ToolStripButton();
            this._mainStatus = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._mainMenu = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.permissionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.danhmụcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chuyenKhoaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator40 = new System.Windows.Forms.ToolStripSeparator();
            this.nhanVienToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator41 = new System.Windows.Forms.ToolStripSeparator();
            this.trieuChungToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.servicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviceListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
            this.serviceGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator30 = new System.Windows.Forms.ToolStripSeparator();
            this.giaVonDichVuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPatientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.patientListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.DuplicatePatientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator36 = new System.Windows.Forms.ToolStripSeparator();
            this.phucHoiBenhNhanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.companyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.companyListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.contractListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receiptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receiptListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            this.phieuThuThuocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator37 = new System.Windows.Forms.ToolStripSeparator();
            this.phieuThuHopDongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invoiceListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator33 = new System.Windows.Forms.ToolStripSeparator();
            this.hoaDonThuocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator34 = new System.Windows.Forms.ToolStripSeparator();
            this.hoaDonXuatTruocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator38 = new System.Windows.Forms.ToolStripSeparator();
            this.hoaDonHopDongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thuocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.danhMucThuocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.nhomThuocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.loThuocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.giaThuocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.keToaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doanhThuNhanVienToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.doanhThuTheoNgayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator31 = new System.Windows.Forms.ToolStripSeparator();
            this.dichVuHopDongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.dichVuTuTucToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator26 = new System.Windows.Forms.ToolStripSeparator();
            this.thuocHetHanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.thuocTonKhoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
            this.inKetQuaKhamSucKhoeTongQuatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator32 = new System.Windows.Forms.ToolStripSeparator();
            this.dichVuChuaXuatPhieuThuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator35 = new System.Windows.Forms.ToolStripSeparator();
            this.thongKeHoaDonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator45 = new System.Windows.Forms.ToolStripSeparator();
            this.baoCaoKhachHangMuaThuocToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator48 = new System.Windows.Forms.ToolStripSeparator();
            this.baoCaoSoLuongKhamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chamSocKhachHangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yKienKhachHangToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator39 = new System.Windows.Forms.ToolStripSeparator();
            this.nhatKyLienHeCongTyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xetNghiemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.danhSachXetNghiemHitachi917ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xetNghiemHiTachi917ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator44 = new System.Windows.Forms.ToolStripSeparator();
            this.danhSachXetNghiemCellDyn3200ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xetNghiemCellDyn3200ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator43 = new System.Windows.Forms.ToolStripSeparator();
            this.xetNghiemTayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ketQuaXetNghiemTayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator46 = new System.Windows.Forms.ToolStripSeparator();
            this.ketQuaXetNghiemTongQuatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator47 = new System.Windows.Forms.ToolStripSeparator();
            this.cauHinhKetNoiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dICOMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
            this.trackingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator42 = new System.Windows.Forms.ToolStripSeparator();
            this.bookingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medicalManagementHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutMedicalManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
            this.templateExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.biểuMẫuPhòngSaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._dotNetBarManager = new DevComponents.DotNetBar.DotNetBarManager(this.components);
            this.dockSite4 = new DevComponents.DotNetBar.DockSite();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.panelDockContainer1 = new DevComponents.DotNetBar.PanelDockContainer();
            this.dgPatient = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.fileNumDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fullname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GenderAsStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dobDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.identityCardDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.homePhoneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workPhoneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mobileDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patientViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dockContainerItem1 = new DevComponents.DotNetBar.DockContainerItem();
            this.bar2 = new DevComponents.DotNetBar.Bar();
            this.panelDockContainer2 = new DevComponents.DotNetBar.PanelDockContainer();
            this._uPhongChoList = new MM.Controls.uPhongChoList();
            this.dockContainerItem2 = new DevComponents.DotNetBar.DockContainerItem();
            this.dockSite1 = new DevComponents.DotNetBar.DockSite();
            this.dockSite2 = new DevComponents.DotNetBar.DockSite();
            this.dockSite8 = new DevComponents.DotNetBar.DockSite();
            this.dockSite5 = new DevComponents.DotNetBar.DockSite();
            this.dockSite6 = new DevComponents.DotNetBar.DockSite();
            this.dockSite7 = new DevComponents.DotNetBar.DockSite();
            this.dockSite3 = new DevComponents.DotNetBar.DockSite();
            this._uBaoCaoSoLuongKham = new MM.Controls.uBaoCaoSoLuongKham();
            this._mainPanel = new System.Windows.Forms.Panel();
            this._uDanhSachXetNghiem_CellDyn3200List = new MM.Controls.uDanhSachXetNghiem_CellDyn3200List();
            this._uDanhSachXetNghiemHitachi917List = new MM.Controls.uDanhSachXetNghiemHitachi917List();
            this._uKetQuaXetNghiemTongHop = new MM.Controls.uKetQuaXetNghiemTongHop();
            this._uKetQuaXetNghiemTay = new MM.Controls.uKetQuaXetNghiemTay();
            this._uXetNghiemTay = new MM.Controls.uXetNghiemTay();
            this._uBaoCaoKhachHangMuaThuoc = new MM.Controls.uBaoCaoKhachHangMuaThuoc();
            this._uKetQuaXetNghiem_CellDyn3200 = new MM.Controls.uKetQuaXetNghiem_CellDyn3200();
            this._uKetQuaXetNghiem_Hitachi917 = new MM.Controls.uKetQuaXetNghiem_Hitachi917();
            this._uBookingList = new MM.Controls.uBookingList();
            this._uNhatKyLienHeCongTy = new MM.Controls.uNhatKyLienHeCongTy();
            this._uYKienKhachHangList = new MM.Controls.uYKienKhachHangList();
            this._uHoaDonHopDongList = new MM.Controls.uHoaDonHopDongList();
            this._uPhieuThuHopDongList = new MM.Controls.uPhieuThuHopDongList();
            this._uPhucHoiBenhNhan = new MM.Controls.uPhucHoiBenhNhan();
            this._uThongKeHoaDon = new MM.Controls.uThongKeHoaDon();
            this._uHoaDonXuatTruoc = new MM.Controls.uHoaDonXuatTruoc();
            this._uHoaDonThuocList = new MM.Controls.uHoaDonThuocList();
            this._uBaoCaoDichVuChuaXuatPhieuThu = new MM.Controls.uBaoCaoDichVuChuaXuatPhieuThu();
            this._uDoanhThuTheoNgay = new MM.Controls.uDoanhThuTheoNgay();
            this._uGiaVonDichVuList = new MM.Controls.uGiaVonDichVuList();
            this._uInKetQuaKhamSucKhoeTongQuat = new MM.Controls.uInKetQuaKhamSucKhoeTongQuat();
            this._uServiceGroupList = new MM.Controls.uServiceGroupList();
            this._uTrackingList = new MM.Controls.uTrackingList();
            this._uDichVuTuTuc = new MM.Controls.uDichVuTuTuc();
            this._uPhieuThuThuocList = new MM.Controls.uPhieuThuThuocList();
            this._uBaoCaoThuocTonKho = new MM.Controls.uBaoCaoThuocTonKho();
            this._uBaoCaoThuocHetHan = new MM.Controls.uBaoCaoThuocHetHan();
            this._uToaThuocList = new MM.Controls.uToaThuocList();
            this._uGiaThuocList = new MM.Controls.uGiaThuocList();
            this._uLoThuocList = new MM.Controls.uLoThuocList();
            this._uNhomThuocList = new MM.Controls.uNhomThuocList();
            this._uThuocList = new MM.Controls.uThuocList();
            this._uDichVuHopDong = new MM.Controls.uDichVuHopDong();
            this._uDoanhThuNhanVien = new MM.Controls.uDoanhThuNhanVien();
            this._uInvoiceList = new MM.Controls.uInvoiceList();
            this._uReceiptList = new MM.Controls.uReceiptList();
            this._uPrintLabel = new MM.Controls.uPrintLabel();
            this._uPermission = new MM.Controls.uPermission();
            this._uContractList = new MM.Controls.uContractList();
            this._uCompanyList = new MM.Controls.uCompanyList();
            this._uSymptomList = new MM.Controls.uSymptomList();
            this._uSpecialityList = new MM.Controls.uSpecialityList();
            this._uPatientHistory = new MM.Controls.uPatientHistory();
            this._uPatientList = new MM.Controls.uPatientList();
            this._uDuplicatePatient = new MM.Controls.uDuplicatePatient();
            this._uDocStaffList = new MM.Controls.uDocStaffList();
            this._uServicesList = new MM.Controls.uServicesList();
            this._mainToolbar.SuspendLayout();
            this._mainStatus.SuspendLayout();
            this._mainMenu.SuspendLayout();
            this.dockSite4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.bar1.SuspendLayout();
            this.panelDockContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPatient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).BeginInit();
            this.bar2.SuspendLayout();
            this.panelDockContainer2.SuspendLayout();
            this._mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainToolbar
            // 
            this._mainToolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._mainToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbLogin,
            this.toolStripSeparator8,
            this.tbServiceList,
            this.tbNhomDichVu,
            this.tbGiaVonDichVu,
            this.toolStripSeparator6,
            this.tbSpecialityList,
            this.toolStripSeparator9,
            this.tbDoctorList,
            this.toolStripSeparator1,
            this.tbOpenPatient,
            this.tbPatientList,
            this.tbDuplicatePatient,
            this.toolStripSeparator5,
            this.tbCompanyList,
            this.tbContractList,
            this.toolStripSeparator11,
            this.tbReceiptList,
            this.toolStripSeparator13,
            this.tbInvoiceList,
            this.toolStripSeparator14,
            this.tbSympton,
            this.toolStripSeparator18,
            this.tbDanhMucThuoc,
            this.tbNhomThuoc,
            this.tbLoThuoc,
            this.tbGiaThuoc,
            this.tbKeToa,
            this.tbPhieuThuThuoc});
            resources.ApplyResources(this._mainToolbar, "_mainToolbar");
            this._mainToolbar.Name = "_mainToolbar";
            this._mainToolbar.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this._mainToolbar_ItemClicked);
            // 
            // tbLogin
            // 
            this.tbLogin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbLogin.Image = global::MM.Properties.Resources.Login_icon;
            resources.ApplyResources(this.tbLogin, "tbLogin");
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Tag = "Login";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            // 
            // tbServiceList
            // 
            this.tbServiceList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbServiceList, "tbServiceList");
            this.tbServiceList.Image = global::MM.Properties.Resources.Service_icon__1_;
            this.tbServiceList.Name = "tbServiceList";
            this.tbServiceList.Tag = "Services List";
            // 
            // tbNhomDichVu
            // 
            this.tbNhomDichVu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbNhomDichVu, "tbNhomDichVu");
            this.tbNhomDichVu.Image = global::MM.Properties.Resources.Freelance_icon;
            this.tbNhomDichVu.Name = "tbNhomDichVu";
            this.tbNhomDichVu.Tag = "ServiceGroup";
            // 
            // tbGiaVonDichVu
            // 
            this.tbGiaVonDichVu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbGiaVonDichVu, "tbGiaVonDichVu");
            this.tbGiaVonDichVu.Image = global::MM.Properties.Resources.US_dollar_icon;
            this.tbGiaVonDichVu.Name = "tbGiaVonDichVu";
            this.tbGiaVonDichVu.Tag = "GiaVonDichVu";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // tbSpecialityList
            // 
            this.tbSpecialityList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbSpecialityList, "tbSpecialityList");
            this.tbSpecialityList.Image = global::MM.Properties.Resources.stethoscope_icon;
            this.tbSpecialityList.Name = "tbSpecialityList";
            this.tbSpecialityList.Tag = "Speciality List";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            resources.ApplyResources(this.toolStripSeparator9, "toolStripSeparator9");
            // 
            // tbDoctorList
            // 
            this.tbDoctorList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbDoctorList, "tbDoctorList");
            this.tbDoctorList.Image = global::MM.Properties.Resources.Doctor_32;
            this.tbDoctorList.Name = "tbDoctorList";
            this.tbDoctorList.Tag = "Doctor List";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tbOpenPatient
            // 
            this.tbOpenPatient.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbOpenPatient, "tbOpenPatient");
            this.tbOpenPatient.Image = global::MM.Properties.Resources.folder_customer_icon;
            this.tbOpenPatient.Name = "tbOpenPatient";
            this.tbOpenPatient.Tag = "Open Patient";
            // 
            // tbPatientList
            // 
            this.tbPatientList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbPatientList, "tbPatientList");
            this.tbPatientList.Image = global::MM.Properties.Resources._1320161545_people;
            this.tbPatientList.Name = "tbPatientList";
            this.tbPatientList.Tag = "Patient List";
            // 
            // tbDuplicatePatient
            // 
            this.tbDuplicatePatient.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbDuplicatePatient, "tbDuplicatePatient");
            this.tbDuplicatePatient.Image = global::MM.Properties.Resources.Actions_window_duplicate_icon;
            this.tbDuplicatePatient.Name = "tbDuplicatePatient";
            this.tbDuplicatePatient.Tag = "DuplicatePatient";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // tbCompanyList
            // 
            this.tbCompanyList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbCompanyList, "tbCompanyList");
            this.tbCompanyList.Image = global::MM.Properties.Resources.industry;
            this.tbCompanyList.Name = "tbCompanyList";
            this.tbCompanyList.Tag = "Company List";
            // 
            // tbContractList
            // 
            this.tbContractList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbContractList, "tbContractList");
            this.tbContractList.Image = global::MM.Properties.Resources.Contract_icon;
            this.tbContractList.Name = "tbContractList";
            this.tbContractList.Tag = "Contract List";
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            resources.ApplyResources(this.toolStripSeparator11, "toolStripSeparator11");
            // 
            // tbReceiptList
            // 
            this.tbReceiptList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbReceiptList, "tbReceiptList");
            this.tbReceiptList.Image = global::MM.Properties.Resources.check_icon;
            this.tbReceiptList.Name = "tbReceiptList";
            this.tbReceiptList.Tag = "Receipt List";
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            resources.ApplyResources(this.toolStripSeparator13, "toolStripSeparator13");
            // 
            // tbInvoiceList
            // 
            this.tbInvoiceList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbInvoiceList, "tbInvoiceList");
            this.tbInvoiceList.Image = global::MM.Properties.Resources.invoice_icon__1_;
            this.tbInvoiceList.Name = "tbInvoiceList";
            this.tbInvoiceList.Tag = "Invoice List";
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            resources.ApplyResources(this.toolStripSeparator14, "toolStripSeparator14");
            // 
            // tbSympton
            // 
            this.tbSympton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbSympton, "tbSympton");
            this.tbSympton.Image = global::MM.Properties.Resources.research;
            this.tbSympton.Name = "tbSympton";
            this.tbSympton.Tag = "Symptom List";
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            resources.ApplyResources(this.toolStripSeparator18, "toolStripSeparator18");
            // 
            // tbDanhMucThuoc
            // 
            this.tbDanhMucThuoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbDanhMucThuoc, "tbDanhMucThuoc");
            this.tbDanhMucThuoc.Image = global::MM.Properties.Resources.pills_3_icon;
            this.tbDanhMucThuoc.Name = "tbDanhMucThuoc";
            this.tbDanhMucThuoc.Tag = "DanhMucThuoc";
            // 
            // tbNhomThuoc
            // 
            this.tbNhomThuoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbNhomThuoc, "tbNhomThuoc");
            this.tbNhomThuoc.Image = global::MM.Properties.Resources.Drug_basket_icon;
            this.tbNhomThuoc.Name = "tbNhomThuoc";
            this.tbNhomThuoc.Tag = "NhomThuoc";
            // 
            // tbLoThuoc
            // 
            this.tbLoThuoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbLoThuoc, "tbLoThuoc");
            this.tbLoThuoc.Image = global::MM.Properties.Resources.inventory_maintenance_icon;
            this.tbLoThuoc.Name = "tbLoThuoc";
            this.tbLoThuoc.Tag = "LoThuoc";
            // 
            // tbGiaThuoc
            // 
            this.tbGiaThuoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbGiaThuoc, "tbGiaThuoc");
            this.tbGiaThuoc.Image = global::MM.Properties.Resources.currency_dollar_yellow;
            this.tbGiaThuoc.Name = "tbGiaThuoc";
            this.tbGiaThuoc.Tag = "GiaThuoc";
            // 
            // tbKeToa
            // 
            this.tbKeToa.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbKeToa, "tbKeToa");
            this.tbKeToa.Image = global::MM.Properties.Resources.prescription_icon;
            this.tbKeToa.Name = "tbKeToa";
            this.tbKeToa.Tag = "KeToa";
            // 
            // tbPhieuThuThuoc
            // 
            this.tbPhieuThuThuoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tbPhieuThuThuoc, "tbPhieuThuThuoc");
            this.tbPhieuThuThuoc.Image = global::MM.Properties.Resources.folder_invoices_icon;
            this.tbPhieuThuThuoc.Name = "tbPhieuThuThuoc";
            this.tbPhieuThuThuoc.Tag = "PhieuThuThuoc";
            // 
            // _mainStatus
            // 
            this._mainStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            resources.ApplyResources(this._mainStatus, "_mainStatus");
            this._mainStatus.Name = "_mainStatus";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            resources.ApplyResources(this.statusLabel, "statusLabel");
            this.statusLabel.Spring = true;
            // 
            // _mainMenu
            // 
            this._mainMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem,
            this.danhmụcToolStripMenuItem,
            this.servicesToolStripMenuItem,
            this.patientToolStripMenuItem,
            this.companyToolStripMenuItem,
            this.receiptToolStripMenuItem,
            this.invoiceToolStripMenuItem,
            this.thuocToolStripMenuItem,
            this.reportToolStripMenuItem,
            this.chamSocKhachHangToolStripMenuItem,
            this.xetNghiemToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this._mainMenu, "_mainMenu");
            this._mainMenu.Name = "_mainMenu";
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.databaseConfigurationToolStripMenuItem,
            this.toolStripSeparator7,
            this.permissionToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.toolStripSeparator4,
            this.loginToolStripMenuItem,
            this.toolStripSeparator12,
            this.exitToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            resources.ApplyResources(this.systemToolStripMenuItem, "systemToolStripMenuItem");
            // 
            // databaseConfigurationToolStripMenuItem
            // 
            this.databaseConfigurationToolStripMenuItem.Image = global::MM.Properties.Resources.connect_info;
            this.databaseConfigurationToolStripMenuItem.Name = "databaseConfigurationToolStripMenuItem";
            resources.ApplyResources(this.databaseConfigurationToolStripMenuItem, "databaseConfigurationToolStripMenuItem");
            this.databaseConfigurationToolStripMenuItem.Tag = "Database Configuration";
            this.databaseConfigurationToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            // 
            // permissionToolStripMenuItem
            // 
            resources.ApplyResources(this.permissionToolStripMenuItem, "permissionToolStripMenuItem");
            this.permissionToolStripMenuItem.Image = global::MM.Properties.Resources.users_2;
            this.permissionToolStripMenuItem.Name = "permissionToolStripMenuItem";
            this.permissionToolStripMenuItem.Tag = "Permission";
            this.permissionToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            resources.ApplyResources(this.changePasswordToolStripMenuItem, "changePasswordToolStripMenuItem");
            this.changePasswordToolStripMenuItem.Image = global::MM.Properties.Resources._1321933963_change_password;
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Tag = "Change Password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Image = global::MM.Properties.Resources.Login_icon;
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            resources.ApplyResources(this.loginToolStripMenuItem, "loginToolStripMenuItem");
            this.loginToolStripMenuItem.Tag = "Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            resources.ApplyResources(this.toolStripSeparator12, "toolStripSeparator12");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::MM.Properties.Resources.Log_Out_icon;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Tag = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // danhmụcToolStripMenuItem
            // 
            this.danhmụcToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chuyenKhoaToolStripMenuItem,
            this.toolStripSeparator40,
            this.nhanVienToolStripMenuItem,
            this.toolStripSeparator41,
            this.trieuChungToolStripMenuItem});
            resources.ApplyResources(this.danhmụcToolStripMenuItem, "danhmụcToolStripMenuItem");
            this.danhmụcToolStripMenuItem.Name = "danhmụcToolStripMenuItem";
            // 
            // chuyenKhoaToolStripMenuItem
            // 
            this.chuyenKhoaToolStripMenuItem.Image = global::MM.Properties.Resources.stethoscope_icon;
            this.chuyenKhoaToolStripMenuItem.Name = "chuyenKhoaToolStripMenuItem";
            resources.ApplyResources(this.chuyenKhoaToolStripMenuItem, "chuyenKhoaToolStripMenuItem");
            this.chuyenKhoaToolStripMenuItem.Tag = "Speciality List";
            this.chuyenKhoaToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator40
            // 
            this.toolStripSeparator40.Name = "toolStripSeparator40";
            resources.ApplyResources(this.toolStripSeparator40, "toolStripSeparator40");
            // 
            // nhanVienToolStripMenuItem
            // 
            this.nhanVienToolStripMenuItem.Image = global::MM.Properties.Resources.Doctor_32;
            this.nhanVienToolStripMenuItem.Name = "nhanVienToolStripMenuItem";
            resources.ApplyResources(this.nhanVienToolStripMenuItem, "nhanVienToolStripMenuItem");
            this.nhanVienToolStripMenuItem.Tag = "Doctor List";
            this.nhanVienToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator41
            // 
            this.toolStripSeparator41.Name = "toolStripSeparator41";
            resources.ApplyResources(this.toolStripSeparator41, "toolStripSeparator41");
            // 
            // trieuChungToolStripMenuItem
            // 
            this.trieuChungToolStripMenuItem.Image = global::MM.Properties.Resources.research;
            this.trieuChungToolStripMenuItem.Name = "trieuChungToolStripMenuItem";
            resources.ApplyResources(this.trieuChungToolStripMenuItem, "trieuChungToolStripMenuItem");
            this.trieuChungToolStripMenuItem.Tag = "Symptom List";
            this.trieuChungToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // servicesToolStripMenuItem
            // 
            this.servicesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serviceListToolStripMenuItem,
            this.toolStripSeparator28,
            this.serviceGroupToolStripMenuItem,
            this.toolStripSeparator30,
            this.giaVonDichVuToolStripMenuItem});
            resources.ApplyResources(this.servicesToolStripMenuItem, "servicesToolStripMenuItem");
            this.servicesToolStripMenuItem.Name = "servicesToolStripMenuItem";
            // 
            // serviceListToolStripMenuItem
            // 
            this.serviceListToolStripMenuItem.Image = global::MM.Properties.Resources.Service_icon__1_;
            this.serviceListToolStripMenuItem.Name = "serviceListToolStripMenuItem";
            resources.ApplyResources(this.serviceListToolStripMenuItem, "serviceListToolStripMenuItem");
            this.serviceListToolStripMenuItem.Tag = "Services List";
            this.serviceListToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator28
            // 
            this.toolStripSeparator28.Name = "toolStripSeparator28";
            resources.ApplyResources(this.toolStripSeparator28, "toolStripSeparator28");
            // 
            // serviceGroupToolStripMenuItem
            // 
            this.serviceGroupToolStripMenuItem.Image = global::MM.Properties.Resources.Freelance_icon;
            this.serviceGroupToolStripMenuItem.Name = "serviceGroupToolStripMenuItem";
            resources.ApplyResources(this.serviceGroupToolStripMenuItem, "serviceGroupToolStripMenuItem");
            this.serviceGroupToolStripMenuItem.Tag = "ServiceGroup";
            this.serviceGroupToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator30
            // 
            this.toolStripSeparator30.Name = "toolStripSeparator30";
            resources.ApplyResources(this.toolStripSeparator30, "toolStripSeparator30");
            // 
            // giaVonDichVuToolStripMenuItem
            // 
            this.giaVonDichVuToolStripMenuItem.Image = global::MM.Properties.Resources.US_dollar_icon;
            this.giaVonDichVuToolStripMenuItem.Name = "giaVonDichVuToolStripMenuItem";
            resources.ApplyResources(this.giaVonDichVuToolStripMenuItem, "giaVonDichVuToolStripMenuItem");
            this.giaVonDichVuToolStripMenuItem.Tag = "GiaVonDichVu";
            this.giaVonDichVuToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // patientToolStripMenuItem
            // 
            this.patientToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPatientToolStripMenuItem,
            this.toolStripSeparator3,
            this.patientListToolStripMenuItem,
            this.toolStripSeparator15,
            this.DuplicatePatientToolStripMenuItem,
            this.toolStripSeparator36,
            this.phucHoiBenhNhanToolStripMenuItem});
            resources.ApplyResources(this.patientToolStripMenuItem, "patientToolStripMenuItem");
            this.patientToolStripMenuItem.Name = "patientToolStripMenuItem";
            // 
            // openPatientToolStripMenuItem
            // 
            this.openPatientToolStripMenuItem.Image = global::MM.Properties.Resources.folder_customer_icon;
            this.openPatientToolStripMenuItem.Name = "openPatientToolStripMenuItem";
            resources.ApplyResources(this.openPatientToolStripMenuItem, "openPatientToolStripMenuItem");
            this.openPatientToolStripMenuItem.Tag = "Open Patient";
            this.openPatientToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // patientListToolStripMenuItem
            // 
            this.patientListToolStripMenuItem.Image = global::MM.Properties.Resources._1320161545_people;
            this.patientListToolStripMenuItem.Name = "patientListToolStripMenuItem";
            resources.ApplyResources(this.patientListToolStripMenuItem, "patientListToolStripMenuItem");
            this.patientListToolStripMenuItem.Tag = "Patient List";
            this.patientListToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            resources.ApplyResources(this.toolStripSeparator15, "toolStripSeparator15");
            // 
            // DuplicatePatientToolStripMenuItem
            // 
            this.DuplicatePatientToolStripMenuItem.Image = global::MM.Properties.Resources.Actions_window_duplicate_icon;
            this.DuplicatePatientToolStripMenuItem.Name = "DuplicatePatientToolStripMenuItem";
            resources.ApplyResources(this.DuplicatePatientToolStripMenuItem, "DuplicatePatientToolStripMenuItem");
            this.DuplicatePatientToolStripMenuItem.Tag = "DuplicatePatient";
            this.DuplicatePatientToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator36
            // 
            this.toolStripSeparator36.Name = "toolStripSeparator36";
            resources.ApplyResources(this.toolStripSeparator36, "toolStripSeparator36");
            // 
            // phucHoiBenhNhanToolStripMenuItem
            // 
            this.phucHoiBenhNhanToolStripMenuItem.Image = global::MM.Properties.Resources.backup_restore_icon;
            this.phucHoiBenhNhanToolStripMenuItem.Name = "phucHoiBenhNhanToolStripMenuItem";
            resources.ApplyResources(this.phucHoiBenhNhanToolStripMenuItem, "phucHoiBenhNhanToolStripMenuItem");
            this.phucHoiBenhNhanToolStripMenuItem.Tag = "PhucHoiBenhNhan";
            this.phucHoiBenhNhanToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // companyToolStripMenuItem
            // 
            this.companyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.companyListToolStripMenuItem,
            this.toolStripSeparator10,
            this.contractListToolStripMenuItem});
            resources.ApplyResources(this.companyToolStripMenuItem, "companyToolStripMenuItem");
            this.companyToolStripMenuItem.Name = "companyToolStripMenuItem";
            // 
            // companyListToolStripMenuItem
            // 
            this.companyListToolStripMenuItem.Image = global::MM.Properties.Resources.industry;
            this.companyListToolStripMenuItem.Name = "companyListToolStripMenuItem";
            resources.ApplyResources(this.companyListToolStripMenuItem, "companyListToolStripMenuItem");
            this.companyListToolStripMenuItem.Tag = "Company List";
            this.companyListToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            resources.ApplyResources(this.toolStripSeparator10, "toolStripSeparator10");
            // 
            // contractListToolStripMenuItem
            // 
            this.contractListToolStripMenuItem.Image = global::MM.Properties.Resources.Contract_icon;
            this.contractListToolStripMenuItem.Name = "contractListToolStripMenuItem";
            resources.ApplyResources(this.contractListToolStripMenuItem, "contractListToolStripMenuItem");
            this.contractListToolStripMenuItem.Tag = "Contract List";
            this.contractListToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // receiptToolStripMenuItem
            // 
            this.receiptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.receiptListToolStripMenuItem,
            this.toolStripSeparator24,
            this.phieuThuThuocToolStripMenuItem,
            this.toolStripSeparator37,
            this.phieuThuHopDongToolStripMenuItem});
            resources.ApplyResources(this.receiptToolStripMenuItem, "receiptToolStripMenuItem");
            this.receiptToolStripMenuItem.Name = "receiptToolStripMenuItem";
            // 
            // receiptListToolStripMenuItem
            // 
            this.receiptListToolStripMenuItem.Image = global::MM.Properties.Resources.check_icon;
            this.receiptListToolStripMenuItem.Name = "receiptListToolStripMenuItem";
            resources.ApplyResources(this.receiptListToolStripMenuItem, "receiptListToolStripMenuItem");
            this.receiptListToolStripMenuItem.Tag = "Receipt List";
            this.receiptListToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator24
            // 
            this.toolStripSeparator24.Name = "toolStripSeparator24";
            resources.ApplyResources(this.toolStripSeparator24, "toolStripSeparator24");
            // 
            // phieuThuThuocToolStripMenuItem
            // 
            this.phieuThuThuocToolStripMenuItem.Image = global::MM.Properties.Resources.check_icon;
            this.phieuThuThuocToolStripMenuItem.Name = "phieuThuThuocToolStripMenuItem";
            resources.ApplyResources(this.phieuThuThuocToolStripMenuItem, "phieuThuThuocToolStripMenuItem");
            this.phieuThuThuocToolStripMenuItem.Tag = "PhieuThuThuoc";
            this.phieuThuThuocToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator37
            // 
            this.toolStripSeparator37.Name = "toolStripSeparator37";
            resources.ApplyResources(this.toolStripSeparator37, "toolStripSeparator37");
            // 
            // phieuThuHopDongToolStripMenuItem
            // 
            this.phieuThuHopDongToolStripMenuItem.Image = global::MM.Properties.Resources.check_icon;
            this.phieuThuHopDongToolStripMenuItem.Name = "phieuThuHopDongToolStripMenuItem";
            resources.ApplyResources(this.phieuThuHopDongToolStripMenuItem, "phieuThuHopDongToolStripMenuItem");
            this.phieuThuHopDongToolStripMenuItem.Tag = "PhieuThuHopDong";
            this.phieuThuHopDongToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // invoiceToolStripMenuItem
            // 
            this.invoiceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.invoiceListToolStripMenuItem,
            this.toolStripSeparator33,
            this.hoaDonThuocToolStripMenuItem,
            this.toolStripSeparator34,
            this.hoaDonXuatTruocToolStripMenuItem,
            this.toolStripSeparator38,
            this.hoaDonHopDongToolStripMenuItem});
            resources.ApplyResources(this.invoiceToolStripMenuItem, "invoiceToolStripMenuItem");
            this.invoiceToolStripMenuItem.Name = "invoiceToolStripMenuItem";
            // 
            // invoiceListToolStripMenuItem
            // 
            this.invoiceListToolStripMenuItem.Image = global::MM.Properties.Resources.invoice_icon__1_;
            this.invoiceListToolStripMenuItem.Name = "invoiceListToolStripMenuItem";
            resources.ApplyResources(this.invoiceListToolStripMenuItem, "invoiceListToolStripMenuItem");
            this.invoiceListToolStripMenuItem.Tag = "Invoice List";
            this.invoiceListToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator33
            // 
            this.toolStripSeparator33.Name = "toolStripSeparator33";
            resources.ApplyResources(this.toolStripSeparator33, "toolStripSeparator33");
            // 
            // hoaDonThuocToolStripMenuItem
            // 
            this.hoaDonThuocToolStripMenuItem.Image = global::MM.Properties.Resources.invoice_icon__1_;
            this.hoaDonThuocToolStripMenuItem.Name = "hoaDonThuocToolStripMenuItem";
            resources.ApplyResources(this.hoaDonThuocToolStripMenuItem, "hoaDonThuocToolStripMenuItem");
            this.hoaDonThuocToolStripMenuItem.Tag = "HoaDonThuoc";
            this.hoaDonThuocToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator34
            // 
            this.toolStripSeparator34.Name = "toolStripSeparator34";
            resources.ApplyResources(this.toolStripSeparator34, "toolStripSeparator34");
            // 
            // hoaDonXuatTruocToolStripMenuItem
            // 
            this.hoaDonXuatTruocToolStripMenuItem.Image = global::MM.Properties.Resources.invoice_icon__1_;
            this.hoaDonXuatTruocToolStripMenuItem.Name = "hoaDonXuatTruocToolStripMenuItem";
            resources.ApplyResources(this.hoaDonXuatTruocToolStripMenuItem, "hoaDonXuatTruocToolStripMenuItem");
            this.hoaDonXuatTruocToolStripMenuItem.Tag = "HoaDonXuatTruoc";
            this.hoaDonXuatTruocToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator38
            // 
            this.toolStripSeparator38.Name = "toolStripSeparator38";
            resources.ApplyResources(this.toolStripSeparator38, "toolStripSeparator38");
            // 
            // hoaDonHopDongToolStripMenuItem
            // 
            this.hoaDonHopDongToolStripMenuItem.Image = global::MM.Properties.Resources.invoice_icon__1_;
            this.hoaDonHopDongToolStripMenuItem.Name = "hoaDonHopDongToolStripMenuItem";
            resources.ApplyResources(this.hoaDonHopDongToolStripMenuItem, "hoaDonHopDongToolStripMenuItem");
            this.hoaDonHopDongToolStripMenuItem.Tag = "HoaDonHopDong";
            this.hoaDonHopDongToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // thuocToolStripMenuItem
            // 
            this.thuocToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.danhMucThuocToolStripMenuItem,
            this.toolStripSeparator17,
            this.nhomThuocToolStripMenuItem,
            this.toolStripSeparator19,
            this.loThuocToolStripMenuItem,
            this.toolStripSeparator20,
            this.giaThuocToolStripMenuItem,
            this.toolStripSeparator21,
            this.keToaToolStripMenuItem});
            resources.ApplyResources(this.thuocToolStripMenuItem, "thuocToolStripMenuItem");
            this.thuocToolStripMenuItem.Name = "thuocToolStripMenuItem";
            // 
            // danhMucThuocToolStripMenuItem
            // 
            this.danhMucThuocToolStripMenuItem.Image = global::MM.Properties.Resources.pills_3_icon;
            this.danhMucThuocToolStripMenuItem.Name = "danhMucThuocToolStripMenuItem";
            resources.ApplyResources(this.danhMucThuocToolStripMenuItem, "danhMucThuocToolStripMenuItem");
            this.danhMucThuocToolStripMenuItem.Tag = "DanhMucThuoc";
            this.danhMucThuocToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            resources.ApplyResources(this.toolStripSeparator17, "toolStripSeparator17");
            // 
            // nhomThuocToolStripMenuItem
            // 
            this.nhomThuocToolStripMenuItem.Image = global::MM.Properties.Resources.Drug_basket_icon;
            this.nhomThuocToolStripMenuItem.Name = "nhomThuocToolStripMenuItem";
            resources.ApplyResources(this.nhomThuocToolStripMenuItem, "nhomThuocToolStripMenuItem");
            this.nhomThuocToolStripMenuItem.Tag = "NhomThuoc";
            this.nhomThuocToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            resources.ApplyResources(this.toolStripSeparator19, "toolStripSeparator19");
            // 
            // loThuocToolStripMenuItem
            // 
            this.loThuocToolStripMenuItem.Image = global::MM.Properties.Resources.inventory_maintenance_icon;
            this.loThuocToolStripMenuItem.Name = "loThuocToolStripMenuItem";
            resources.ApplyResources(this.loThuocToolStripMenuItem, "loThuocToolStripMenuItem");
            this.loThuocToolStripMenuItem.Tag = "LoThuoc";
            this.loThuocToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            resources.ApplyResources(this.toolStripSeparator20, "toolStripSeparator20");
            // 
            // giaThuocToolStripMenuItem
            // 
            this.giaThuocToolStripMenuItem.Image = global::MM.Properties.Resources.currency_dollar_yellow;
            this.giaThuocToolStripMenuItem.Name = "giaThuocToolStripMenuItem";
            resources.ApplyResources(this.giaThuocToolStripMenuItem, "giaThuocToolStripMenuItem");
            this.giaThuocToolStripMenuItem.Tag = "GiaThuoc";
            this.giaThuocToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            resources.ApplyResources(this.toolStripSeparator21, "toolStripSeparator21");
            // 
            // keToaToolStripMenuItem
            // 
            this.keToaToolStripMenuItem.Image = global::MM.Properties.Resources.prescription_icon;
            this.keToaToolStripMenuItem.Name = "keToaToolStripMenuItem";
            resources.ApplyResources(this.keToaToolStripMenuItem, "keToaToolStripMenuItem");
            this.keToaToolStripMenuItem.Tag = "KeToa";
            this.keToaToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.doanhThuNhanVienToolStripMenuItem,
            this.toolStripSeparator16,
            this.doanhThuTheoNgayToolStripMenuItem,
            this.toolStripSeparator31,
            this.dichVuHopDongToolStripMenuItem,
            this.toolStripSeparator22,
            this.dichVuTuTucToolStripMenuItem,
            this.toolStripSeparator26,
            this.thuocHetHanToolStripMenuItem,
            this.toolStripSeparator23,
            this.thuocTonKhoToolStripMenuItem,
            this.toolStripSeparator29,
            this.inKetQuaKhamSucKhoeTongQuatToolStripMenuItem,
            this.toolStripSeparator32,
            this.dichVuChuaXuatPhieuThuToolStripMenuItem,
            this.toolStripSeparator35,
            this.thongKeHoaDonToolStripMenuItem,
            this.toolStripSeparator45,
            this.baoCaoKhachHangMuaThuocToolStripMenuItem,
            this.toolStripSeparator48,
            this.baoCaoSoLuongKhamToolStripMenuItem});
            resources.ApplyResources(this.reportToolStripMenuItem, "reportToolStripMenuItem");
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            // 
            // doanhThuNhanVienToolStripMenuItem
            // 
            this.doanhThuNhanVienToolStripMenuItem.Image = global::MM.Properties.Resources.personal_loan_icon;
            this.doanhThuNhanVienToolStripMenuItem.Name = "doanhThuNhanVienToolStripMenuItem";
            resources.ApplyResources(this.doanhThuNhanVienToolStripMenuItem, "doanhThuNhanVienToolStripMenuItem");
            this.doanhThuNhanVienToolStripMenuItem.Tag = "DoanhThuNhanVien";
            this.doanhThuNhanVienToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            resources.ApplyResources(this.toolStripSeparator16, "toolStripSeparator16");
            // 
            // doanhThuTheoNgayToolStripMenuItem
            // 
            this.doanhThuTheoNgayToolStripMenuItem.Image = global::MM.Properties.Resources.event_search_icon;
            this.doanhThuTheoNgayToolStripMenuItem.Name = "doanhThuTheoNgayToolStripMenuItem";
            resources.ApplyResources(this.doanhThuTheoNgayToolStripMenuItem, "doanhThuTheoNgayToolStripMenuItem");
            this.doanhThuTheoNgayToolStripMenuItem.Tag = "DoanhThuTheoNgay";
            this.doanhThuTheoNgayToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator31
            // 
            this.toolStripSeparator31.Name = "toolStripSeparator31";
            resources.ApplyResources(this.toolStripSeparator31, "toolStripSeparator31");
            // 
            // dichVuHopDongToolStripMenuItem
            // 
            this.dichVuHopDongToolStripMenuItem.Image = global::MM.Properties.Resources.folder_contract_icon;
            this.dichVuHopDongToolStripMenuItem.Name = "dichVuHopDongToolStripMenuItem";
            resources.ApplyResources(this.dichVuHopDongToolStripMenuItem, "dichVuHopDongToolStripMenuItem");
            this.dichVuHopDongToolStripMenuItem.Tag = "DichVuHopDong";
            this.dichVuHopDongToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            resources.ApplyResources(this.toolStripSeparator22, "toolStripSeparator22");
            // 
            // dichVuTuTucToolStripMenuItem
            // 
            this.dichVuTuTucToolStripMenuItem.Image = global::MM.Properties.Resources.accept_icon;
            this.dichVuTuTucToolStripMenuItem.Name = "dichVuTuTucToolStripMenuItem";
            resources.ApplyResources(this.dichVuTuTucToolStripMenuItem, "dichVuTuTucToolStripMenuItem");
            this.dichVuTuTucToolStripMenuItem.Tag = "DichVuTuTuc";
            this.dichVuTuTucToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator26
            // 
            this.toolStripSeparator26.Name = "toolStripSeparator26";
            resources.ApplyResources(this.toolStripSeparator26, "toolStripSeparator26");
            // 
            // thuocHetHanToolStripMenuItem
            // 
            this.thuocHetHanToolStripMenuItem.Image = global::MM.Properties.Resources.Actions_view_calendar_upcoming_events_icon;
            this.thuocHetHanToolStripMenuItem.Name = "thuocHetHanToolStripMenuItem";
            resources.ApplyResources(this.thuocHetHanToolStripMenuItem, "thuocHetHanToolStripMenuItem");
            this.thuocHetHanToolStripMenuItem.Tag = "ThuocHetHan";
            this.thuocHetHanToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            resources.ApplyResources(this.toolStripSeparator23, "toolStripSeparator23");
            // 
            // thuocTonKhoToolStripMenuItem
            // 
            this.thuocTonKhoToolStripMenuItem.Image = global::MM.Properties.Resources.palet_03_icon;
            this.thuocTonKhoToolStripMenuItem.Name = "thuocTonKhoToolStripMenuItem";
            resources.ApplyResources(this.thuocTonKhoToolStripMenuItem, "thuocTonKhoToolStripMenuItem");
            this.thuocTonKhoToolStripMenuItem.Tag = "ThuocTonKho";
            this.thuocTonKhoToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator29
            // 
            this.toolStripSeparator29.Name = "toolStripSeparator29";
            resources.ApplyResources(this.toolStripSeparator29, "toolStripSeparator29");
            // 
            // inKetQuaKhamSucKhoeTongQuatToolStripMenuItem
            // 
            this.inKetQuaKhamSucKhoeTongQuatToolStripMenuItem.Image = global::MM.Properties.Resources.Stethoscope_icon3;
            this.inKetQuaKhamSucKhoeTongQuatToolStripMenuItem.Name = "inKetQuaKhamSucKhoeTongQuatToolStripMenuItem";
            resources.ApplyResources(this.inKetQuaKhamSucKhoeTongQuatToolStripMenuItem, "inKetQuaKhamSucKhoeTongQuatToolStripMenuItem");
            this.inKetQuaKhamSucKhoeTongQuatToolStripMenuItem.Tag = "InKetQuaKhamSucKhoeTongQuat";
            this.inKetQuaKhamSucKhoeTongQuatToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator32
            // 
            this.toolStripSeparator32.Name = "toolStripSeparator32";
            resources.ApplyResources(this.toolStripSeparator32, "toolStripSeparator32");
            // 
            // dichVuChuaXuatPhieuThuToolStripMenuItem
            // 
            this.dichVuChuaXuatPhieuThuToolStripMenuItem.Image = global::MM.Properties.Resources.Process_Warning_icon;
            this.dichVuChuaXuatPhieuThuToolStripMenuItem.Name = "dichVuChuaXuatPhieuThuToolStripMenuItem";
            resources.ApplyResources(this.dichVuChuaXuatPhieuThuToolStripMenuItem, "dichVuChuaXuatPhieuThuToolStripMenuItem");
            this.dichVuChuaXuatPhieuThuToolStripMenuItem.Tag = "DichVuChuaXuatPhieuThu";
            this.dichVuChuaXuatPhieuThuToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator35
            // 
            this.toolStripSeparator35.Name = "toolStripSeparator35";
            resources.ApplyResources(this.toolStripSeparator35, "toolStripSeparator35");
            // 
            // thongKeHoaDonToolStripMenuItem
            // 
            this.thongKeHoaDonToolStripMenuItem.Image = global::MM.Properties.Resources.invoice_icon__1_;
            this.thongKeHoaDonToolStripMenuItem.Name = "thongKeHoaDonToolStripMenuItem";
            resources.ApplyResources(this.thongKeHoaDonToolStripMenuItem, "thongKeHoaDonToolStripMenuItem");
            this.thongKeHoaDonToolStripMenuItem.Tag = "ThongKeHoaDon";
            this.thongKeHoaDonToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator45
            // 
            this.toolStripSeparator45.Name = "toolStripSeparator45";
            resources.ApplyResources(this.toolStripSeparator45, "toolStripSeparator45");
            // 
            // baoCaoKhachHangMuaThuocToolStripMenuItem
            // 
            resources.ApplyResources(this.baoCaoKhachHangMuaThuocToolStripMenuItem, "baoCaoKhachHangMuaThuocToolStripMenuItem");
            this.baoCaoKhachHangMuaThuocToolStripMenuItem.Name = "baoCaoKhachHangMuaThuocToolStripMenuItem";
            this.baoCaoKhachHangMuaThuocToolStripMenuItem.Tag = "BaoCaoKhachHangMuaThuoc";
            this.baoCaoKhachHangMuaThuocToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator48
            // 
            this.toolStripSeparator48.Name = "toolStripSeparator48";
            resources.ApplyResources(this.toolStripSeparator48, "toolStripSeparator48");
            // 
            // baoCaoSoLuongKhamToolStripMenuItem
            // 
            resources.ApplyResources(this.baoCaoSoLuongKhamToolStripMenuItem, "baoCaoSoLuongKhamToolStripMenuItem");
            this.baoCaoSoLuongKhamToolStripMenuItem.Name = "baoCaoSoLuongKhamToolStripMenuItem";
            this.baoCaoSoLuongKhamToolStripMenuItem.Tag = "BaoCaoSoLuongKham";
            this.baoCaoSoLuongKhamToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // chamSocKhachHangToolStripMenuItem
            // 
            this.chamSocKhachHangToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yKienKhachHangToolStripMenuItem,
            this.toolStripSeparator39,
            this.nhatKyLienHeCongTyToolStripMenuItem});
            resources.ApplyResources(this.chamSocKhachHangToolStripMenuItem, "chamSocKhachHangToolStripMenuItem");
            this.chamSocKhachHangToolStripMenuItem.Name = "chamSocKhachHangToolStripMenuItem";
            // 
            // yKienKhachHangToolStripMenuItem
            // 
            this.yKienKhachHangToolStripMenuItem.Name = "yKienKhachHangToolStripMenuItem";
            resources.ApplyResources(this.yKienKhachHangToolStripMenuItem, "yKienKhachHangToolStripMenuItem");
            this.yKienKhachHangToolStripMenuItem.Tag = "YKienKhachHang";
            this.yKienKhachHangToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator39
            // 
            this.toolStripSeparator39.Name = "toolStripSeparator39";
            resources.ApplyResources(this.toolStripSeparator39, "toolStripSeparator39");
            // 
            // nhatKyLienHeCongTyToolStripMenuItem
            // 
            this.nhatKyLienHeCongTyToolStripMenuItem.Name = "nhatKyLienHeCongTyToolStripMenuItem";
            resources.ApplyResources(this.nhatKyLienHeCongTyToolStripMenuItem, "nhatKyLienHeCongTyToolStripMenuItem");
            this.nhatKyLienHeCongTyToolStripMenuItem.Tag = "NhatKyLienHeCongTy";
            this.nhatKyLienHeCongTyToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // xetNghiemToolStripMenuItem
            // 
            this.xetNghiemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.danhSachXetNghiemHitachi917ToolStripMenuItem,
            this.xetNghiemHiTachi917ToolStripMenuItem,
            this.toolStripSeparator44,
            this.danhSachXetNghiemCellDyn3200ToolStripMenuItem,
            this.xetNghiemCellDyn3200ToolStripMenuItem,
            this.toolStripSeparator43,
            this.xetNghiemTayToolStripMenuItem,
            this.ketQuaXetNghiemTayToolStripMenuItem,
            this.toolStripSeparator46,
            this.ketQuaXetNghiemTongQuatToolStripMenuItem,
            this.toolStripSeparator47,
            this.cauHinhKetNoiToolStripMenuItem});
            resources.ApplyResources(this.xetNghiemToolStripMenuItem, "xetNghiemToolStripMenuItem");
            this.xetNghiemToolStripMenuItem.Name = "xetNghiemToolStripMenuItem";
            // 
            // danhSachXetNghiemHitachi917ToolStripMenuItem
            // 
            this.danhSachXetNghiemHitachi917ToolStripMenuItem.Name = "danhSachXetNghiemHitachi917ToolStripMenuItem";
            resources.ApplyResources(this.danhSachXetNghiemHitachi917ToolStripMenuItem, "danhSachXetNghiemHitachi917ToolStripMenuItem");
            this.danhSachXetNghiemHitachi917ToolStripMenuItem.Tag = "DanhSachXetNghiemHitachi917";
            this.danhSachXetNghiemHitachi917ToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // xetNghiemHiTachi917ToolStripMenuItem
            // 
            this.xetNghiemHiTachi917ToolStripMenuItem.Name = "xetNghiemHiTachi917ToolStripMenuItem";
            resources.ApplyResources(this.xetNghiemHiTachi917ToolStripMenuItem, "xetNghiemHiTachi917ToolStripMenuItem");
            this.xetNghiemHiTachi917ToolStripMenuItem.Tag = "XetNghiem_Hitachi917";
            this.xetNghiemHiTachi917ToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator44
            // 
            this.toolStripSeparator44.Name = "toolStripSeparator44";
            resources.ApplyResources(this.toolStripSeparator44, "toolStripSeparator44");
            // 
            // danhSachXetNghiemCellDyn3200ToolStripMenuItem
            // 
            this.danhSachXetNghiemCellDyn3200ToolStripMenuItem.Name = "danhSachXetNghiemCellDyn3200ToolStripMenuItem";
            resources.ApplyResources(this.danhSachXetNghiemCellDyn3200ToolStripMenuItem, "danhSachXetNghiemCellDyn3200ToolStripMenuItem");
            this.danhSachXetNghiemCellDyn3200ToolStripMenuItem.Tag = "DanhSachXetNghiemCellDyn3200";
            this.danhSachXetNghiemCellDyn3200ToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // xetNghiemCellDyn3200ToolStripMenuItem
            // 
            this.xetNghiemCellDyn3200ToolStripMenuItem.Name = "xetNghiemCellDyn3200ToolStripMenuItem";
            resources.ApplyResources(this.xetNghiemCellDyn3200ToolStripMenuItem, "xetNghiemCellDyn3200ToolStripMenuItem");
            this.xetNghiemCellDyn3200ToolStripMenuItem.Tag = "XetNghiem_CellDyn3200";
            this.xetNghiemCellDyn3200ToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator43
            // 
            this.toolStripSeparator43.Name = "toolStripSeparator43";
            resources.ApplyResources(this.toolStripSeparator43, "toolStripSeparator43");
            // 
            // xetNghiemTayToolStripMenuItem
            // 
            this.xetNghiemTayToolStripMenuItem.Name = "xetNghiemTayToolStripMenuItem";
            resources.ApplyResources(this.xetNghiemTayToolStripMenuItem, "xetNghiemTayToolStripMenuItem");
            this.xetNghiemTayToolStripMenuItem.Tag = "XetNghiemTay";
            this.xetNghiemTayToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // ketQuaXetNghiemTayToolStripMenuItem
            // 
            this.ketQuaXetNghiemTayToolStripMenuItem.Name = "ketQuaXetNghiemTayToolStripMenuItem";
            resources.ApplyResources(this.ketQuaXetNghiemTayToolStripMenuItem, "ketQuaXetNghiemTayToolStripMenuItem");
            this.ketQuaXetNghiemTayToolStripMenuItem.Tag = "KetQuaXetNghiemTay";
            this.ketQuaXetNghiemTayToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator46
            // 
            this.toolStripSeparator46.Name = "toolStripSeparator46";
            resources.ApplyResources(this.toolStripSeparator46, "toolStripSeparator46");
            // 
            // ketQuaXetNghiemTongQuatToolStripMenuItem
            // 
            this.ketQuaXetNghiemTongQuatToolStripMenuItem.Name = "ketQuaXetNghiemTongQuatToolStripMenuItem";
            resources.ApplyResources(this.ketQuaXetNghiemTongQuatToolStripMenuItem, "ketQuaXetNghiemTongQuatToolStripMenuItem");
            this.ketQuaXetNghiemTongQuatToolStripMenuItem.Tag = "KetQuaXetNghiemTongHop";
            this.ketQuaXetNghiemTongQuatToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator47
            // 
            this.toolStripSeparator47.Name = "toolStripSeparator47";
            resources.ApplyResources(this.toolStripSeparator47, "toolStripSeparator47");
            // 
            // cauHinhKetNoiToolStripMenuItem
            // 
            this.cauHinhKetNoiToolStripMenuItem.Name = "cauHinhKetNoiToolStripMenuItem";
            resources.ApplyResources(this.cauHinhKetNoiToolStripMenuItem, "cauHinhKetNoiToolStripMenuItem");
            this.cauHinhKetNoiToolStripMenuItem.Tag = "CauHinhKetNoi";
            this.cauHinhKetNoiToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dICOMToolStripMenuItem,
            this.printLabelToolStripMenuItem,
            this.toolStripSeparator27,
            this.trackingToolStripMenuItem,
            this.toolStripSeparator42,
            this.bookingToolStripMenuItem});
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            // 
            // dICOMToolStripMenuItem
            // 
            this.dICOMToolStripMenuItem.Name = "dICOMToolStripMenuItem";
            resources.ApplyResources(this.dICOMToolStripMenuItem, "dICOMToolStripMenuItem");
            this.dICOMToolStripMenuItem.Tag = "DICOM";
            this.dICOMToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // printLabelToolStripMenuItem
            // 
            this.printLabelToolStripMenuItem.Image = global::MM.Properties.Resources.Printer_icon;
            this.printLabelToolStripMenuItem.Name = "printLabelToolStripMenuItem";
            resources.ApplyResources(this.printLabelToolStripMenuItem, "printLabelToolStripMenuItem");
            this.printLabelToolStripMenuItem.Tag = "Print Label";
            this.printLabelToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator27
            // 
            this.toolStripSeparator27.Name = "toolStripSeparator27";
            resources.ApplyResources(this.toolStripSeparator27, "toolStripSeparator27");
            // 
            // trackingToolStripMenuItem
            // 
            this.trackingToolStripMenuItem.Image = global::MM.Properties.Resources.log_icon;
            this.trackingToolStripMenuItem.Name = "trackingToolStripMenuItem";
            resources.ApplyResources(this.trackingToolStripMenuItem, "trackingToolStripMenuItem");
            this.trackingToolStripMenuItem.Tag = "Tracking";
            this.trackingToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator42
            // 
            this.toolStripSeparator42.Name = "toolStripSeparator42";
            resources.ApplyResources(this.toolStripSeparator42, "toolStripSeparator42");
            // 
            // bookingToolStripMenuItem
            // 
            resources.ApplyResources(this.bookingToolStripMenuItem, "bookingToolStripMenuItem");
            this.bookingToolStripMenuItem.Name = "bookingToolStripMenuItem";
            this.bookingToolStripMenuItem.Tag = "Booking";
            this.bookingToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.medicalManagementHelpToolStripMenuItem,
            this.toolStripSeparator2,
            this.aboutMedicalManagementToolStripMenuItem,
            this.toolStripSeparator25,
            this.templateExcelToolStripMenuItem,
            this.biểuMẫuPhòngSaleToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // medicalManagementHelpToolStripMenuItem
            // 
            this.medicalManagementHelpToolStripMenuItem.Image = global::MM.Properties.Resources.help;
            this.medicalManagementHelpToolStripMenuItem.Name = "medicalManagementHelpToolStripMenuItem";
            resources.ApplyResources(this.medicalManagementHelpToolStripMenuItem, "medicalManagementHelpToolStripMenuItem");
            this.medicalManagementHelpToolStripMenuItem.Tag = "Help";
            this.medicalManagementHelpToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // aboutMedicalManagementToolStripMenuItem
            // 
            this.aboutMedicalManagementToolStripMenuItem.Image = global::MM.Properties.Resources.about;
            this.aboutMedicalManagementToolStripMenuItem.Name = "aboutMedicalManagementToolStripMenuItem";
            resources.ApplyResources(this.aboutMedicalManagementToolStripMenuItem, "aboutMedicalManagementToolStripMenuItem");
            this.aboutMedicalManagementToolStripMenuItem.Tag = "About";
            this.aboutMedicalManagementToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripSeparator25
            // 
            this.toolStripSeparator25.Name = "toolStripSeparator25";
            resources.ApplyResources(this.toolStripSeparator25, "toolStripSeparator25");
            // 
            // templateExcelToolStripMenuItem
            // 
            this.templateExcelToolStripMenuItem.Image = global::MM.Properties.Resources.Excel2_icon;
            this.templateExcelToolStripMenuItem.Name = "templateExcelToolStripMenuItem";
            resources.ApplyResources(this.templateExcelToolStripMenuItem, "templateExcelToolStripMenuItem");
            this.templateExcelToolStripMenuItem.Tag = "ExcelTemplate";
            this.templateExcelToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // biểuMẫuPhòngSaleToolStripMenuItem
            // 
            this.biểuMẫuPhòngSaleToolStripMenuItem.Name = "biểuMẫuPhòngSaleToolStripMenuItem";
            resources.ApplyResources(this.biểuMẫuPhòngSaleToolStripMenuItem, "biểuMẫuPhòngSaleToolStripMenuItem");
            this.biểuMẫuPhòngSaleToolStripMenuItem.Tag = "TemplateForSale";
            this.biểuMẫuPhòngSaleToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // _dotNetBarManager
            // 
            this._dotNetBarManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.F1);
            this._dotNetBarManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlC);
            this._dotNetBarManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlA);
            this._dotNetBarManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlV);
            this._dotNetBarManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlX);
            this._dotNetBarManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlZ);
            this._dotNetBarManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlY);
            this._dotNetBarManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Del);
            this._dotNetBarManager.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Ins);
            this._dotNetBarManager.BottomDockSite = this.dockSite4;
            this._dotNetBarManager.EnableFullSizeDock = false;
            this._dotNetBarManager.LeftDockSite = this.dockSite1;
            this._dotNetBarManager.ParentForm = this;
            this._dotNetBarManager.RightDockSite = this.dockSite2;
            this._dotNetBarManager.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this._dotNetBarManager.ToolbarBottomDockSite = this.dockSite8;
            this._dotNetBarManager.ToolbarLeftDockSite = this.dockSite5;
            this._dotNetBarManager.ToolbarRightDockSite = this.dockSite6;
            this._dotNetBarManager.ToolbarTopDockSite = this.dockSite7;
            this._dotNetBarManager.TopDockSite = this.dockSite3;
            // 
            // dockSite4
            // 
            this.dockSite4.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.dockSite4.Controls.Add(this.bar1);
            this.dockSite4.Controls.Add(this.bar2);
            resources.ApplyResources(this.dockSite4, "dockSite4");
            this.dockSite4.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer(new DevComponents.DotNetBar.DocumentBaseContainer[] {
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.bar1, 1195, 193))),
            ((DevComponents.DotNetBar.DocumentBaseContainer)(new DevComponents.DotNetBar.DocumentBarContainer(this.bar2, 1195, 222)))}, DevComponents.DotNetBar.eOrientation.Vertical);
            this.dockSite4.Name = "dockSite4";
            this.dockSite4.TabStop = false;
            // 
            // bar1
            // 
            resources.ApplyResources(this.bar1, "bar1");
            this.bar1.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.bar1.AutoHide = true;
            this.bar1.AutoSyncBarCaption = true;
            this.bar1.CloseSingleTab = true;
            this.bar1.Controls.Add(this.panelDockContainer1);
            this.bar1.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dockContainerItem1});
            this.bar1.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.bar1.Name = "bar1";
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.bar1.TabStop = false;
            // 
            // panelDockContainer1
            // 
            this.panelDockContainer1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.panelDockContainer1.Controls.Add(this.dgPatient);
            resources.ApplyResources(this.panelDockContainer1, "panelDockContainer1");
            this.panelDockContainer1.Name = "panelDockContainer1";
            this.panelDockContainer1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDockContainer1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelDockContainer1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelDockContainer1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelDockContainer1.Style.GradientAngle = 90;
            // 
            // dgPatient
            // 
            this.dgPatient.AllowUserToAddRows = false;
            this.dgPatient.AllowUserToDeleteRows = false;
            this.dgPatient.AllowUserToOrderColumns = true;
            this.dgPatient.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPatient.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPatient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPatient.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fileNumDataGridViewTextBoxColumn,
            this.Fullname,
            this.FullAddress,
            this.GenderAsStr,
            this.dobDataGridViewTextBoxColumn,
            this.identityCardDataGridViewTextBoxColumn,
            this.homePhoneDataGridViewTextBoxColumn,
            this.workPhoneDataGridViewTextBoxColumn,
            this.mobileDataGridViewTextBoxColumn,
            this.emailDataGridViewTextBoxColumn});
            this.dgPatient.DataSource = this.patientViewBindingSource;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPatient.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(this.dgPatient, "dgPatient");
            this.dgPatient.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgPatient.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgPatient.HighlightSelectedColumnHeaders = false;
            this.dgPatient.MultiSelect = false;
            this.dgPatient.Name = "dgPatient";
            this.dgPatient.ReadOnly = true;
            this.dgPatient.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPatient.DoubleClick += new System.EventHandler(this.dgPatient_DoubleClick);
            // 
            // fileNumDataGridViewTextBoxColumn
            // 
            this.fileNumDataGridViewTextBoxColumn.DataPropertyName = "FileNum";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.fileNumDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.fileNumDataGridViewTextBoxColumn, "fileNumDataGridViewTextBoxColumn");
            this.fileNumDataGridViewTextBoxColumn.Name = "fileNumDataGridViewTextBoxColumn";
            this.fileNumDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Fullname
            // 
            this.Fullname.DataPropertyName = "FullName";
            resources.ApplyResources(this.Fullname, "Fullname");
            this.Fullname.Name = "Fullname";
            this.Fullname.ReadOnly = true;
            // 
            // FullAddress
            // 
            this.FullAddress.DataPropertyName = "Address";
            resources.ApplyResources(this.FullAddress, "FullAddress");
            this.FullAddress.Name = "FullAddress";
            this.FullAddress.ReadOnly = true;
            // 
            // GenderAsStr
            // 
            this.GenderAsStr.DataPropertyName = "GenderAsStr";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.GenderAsStr.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.GenderAsStr, "GenderAsStr");
            this.GenderAsStr.Name = "GenderAsStr";
            this.GenderAsStr.ReadOnly = true;
            // 
            // dobDataGridViewTextBoxColumn
            // 
            this.dobDataGridViewTextBoxColumn.DataPropertyName = "DobStr";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.NullValue = null;
            this.dobDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.dobDataGridViewTextBoxColumn, "dobDataGridViewTextBoxColumn");
            this.dobDataGridViewTextBoxColumn.Name = "dobDataGridViewTextBoxColumn";
            this.dobDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // identityCardDataGridViewTextBoxColumn
            // 
            this.identityCardDataGridViewTextBoxColumn.DataPropertyName = "IdentityCard";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.identityCardDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.identityCardDataGridViewTextBoxColumn, "identityCardDataGridViewTextBoxColumn");
            this.identityCardDataGridViewTextBoxColumn.Name = "identityCardDataGridViewTextBoxColumn";
            this.identityCardDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // homePhoneDataGridViewTextBoxColumn
            // 
            this.homePhoneDataGridViewTextBoxColumn.DataPropertyName = "HomePhone";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.homePhoneDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.homePhoneDataGridViewTextBoxColumn, "homePhoneDataGridViewTextBoxColumn");
            this.homePhoneDataGridViewTextBoxColumn.Name = "homePhoneDataGridViewTextBoxColumn";
            this.homePhoneDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // workPhoneDataGridViewTextBoxColumn
            // 
            this.workPhoneDataGridViewTextBoxColumn.DataPropertyName = "WorkPhone";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.workPhoneDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.workPhoneDataGridViewTextBoxColumn, "workPhoneDataGridViewTextBoxColumn");
            this.workPhoneDataGridViewTextBoxColumn.Name = "workPhoneDataGridViewTextBoxColumn";
            this.workPhoneDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mobileDataGridViewTextBoxColumn
            // 
            this.mobileDataGridViewTextBoxColumn.DataPropertyName = "Mobile";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.mobileDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.mobileDataGridViewTextBoxColumn, "mobileDataGridViewTextBoxColumn");
            this.mobileDataGridViewTextBoxColumn.Name = "mobileDataGridViewTextBoxColumn";
            this.mobileDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // emailDataGridViewTextBoxColumn
            // 
            this.emailDataGridViewTextBoxColumn.DataPropertyName = "Email";
            resources.ApplyResources(this.emailDataGridViewTextBoxColumn, "emailDataGridViewTextBoxColumn");
            this.emailDataGridViewTextBoxColumn.Name = "emailDataGridViewTextBoxColumn";
            this.emailDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // patientViewBindingSource
            // 
            this.patientViewBindingSource.DataSource = typeof(MM.Databasae.PatientView);
            // 
            // dockContainerItem1
            // 
            this.dockContainerItem1.Control = this.panelDockContainer1;
            this.dockContainerItem1.Name = "dockContainerItem1";
            resources.ApplyResources(this.dockContainerItem1, "dockContainerItem1");
            // 
            // bar2
            // 
            resources.ApplyResources(this.bar2, "bar2");
            this.bar2.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.bar2.AutoHide = true;
            this.bar2.AutoSyncBarCaption = true;
            this.bar2.CloseSingleTab = true;
            this.bar2.Controls.Add(this.panelDockContainer2);
            this.bar2.GrabHandleStyle = DevComponents.DotNetBar.eGrabHandleStyle.Caption;
            this.bar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.dockContainerItem2});
            this.bar2.LayoutType = DevComponents.DotNetBar.eLayoutType.DockContainer;
            this.bar2.Name = "bar2";
            this.bar2.Stretch = true;
            this.bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.bar2.TabStop = false;
            // 
            // panelDockContainer2
            // 
            this.panelDockContainer2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.panelDockContainer2.Controls.Add(this._uPhongChoList);
            resources.ApplyResources(this.panelDockContainer2, "panelDockContainer2");
            this.panelDockContainer2.Name = "panelDockContainer2";
            this.panelDockContainer2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelDockContainer2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.panelDockContainer2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.panelDockContainer2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.panelDockContainer2.Style.GradientAngle = 90;
            // 
            // _uPhongChoList
            // 
            resources.ApplyResources(this._uPhongChoList, "_uPhongChoList");
            this._uPhongChoList.Name = "_uPhongChoList";
            // 
            // dockContainerItem2
            // 
            this.dockContainerItem2.Control = this.panelDockContainer2;
            this.dockContainerItem2.Name = "dockContainerItem2";
            resources.ApplyResources(this.dockContainerItem2, "dockContainerItem2");
            // 
            // dockSite1
            // 
            this.dockSite1.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            resources.ApplyResources(this.dockSite1, "dockSite1");
            this.dockSite1.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite1.Name = "dockSite1";
            this.dockSite1.TabStop = false;
            // 
            // dockSite2
            // 
            this.dockSite2.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            resources.ApplyResources(this.dockSite2, "dockSite2");
            this.dockSite2.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite2.Name = "dockSite2";
            this.dockSite2.TabStop = false;
            // 
            // dockSite8
            // 
            this.dockSite8.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            resources.ApplyResources(this.dockSite8, "dockSite8");
            this.dockSite8.Name = "dockSite8";
            this.dockSite8.TabStop = false;
            // 
            // dockSite5
            // 
            this.dockSite5.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            resources.ApplyResources(this.dockSite5, "dockSite5");
            this.dockSite5.Name = "dockSite5";
            this.dockSite5.TabStop = false;
            // 
            // dockSite6
            // 
            this.dockSite6.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            resources.ApplyResources(this.dockSite6, "dockSite6");
            this.dockSite6.Name = "dockSite6";
            this.dockSite6.TabStop = false;
            // 
            // dockSite7
            // 
            this.dockSite7.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            resources.ApplyResources(this.dockSite7, "dockSite7");
            this.dockSite7.Name = "dockSite7";
            this.dockSite7.TabStop = false;
            // 
            // dockSite3
            // 
            this.dockSite3.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            resources.ApplyResources(this.dockSite3, "dockSite3");
            this.dockSite3.DocumentDockContainer = new DevComponents.DotNetBar.DocumentDockContainer();
            this.dockSite3.Name = "dockSite3";
            this.dockSite3.TabStop = false;
            // 
            // _uBaoCaoSoLuongKham
            // 
            resources.ApplyResources(this._uBaoCaoSoLuongKham, "_uBaoCaoSoLuongKham");
            this._uBaoCaoSoLuongKham.Name = "_uBaoCaoSoLuongKham";
            // 
            // _mainPanel
            // 
            this._mainPanel.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this._mainPanel, "_mainPanel");
            this._mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._mainPanel.Controls.Add(this._uBaoCaoSoLuongKham);
            this._mainPanel.Controls.Add(this._uDanhSachXetNghiem_CellDyn3200List);
            this._mainPanel.Controls.Add(this._uDanhSachXetNghiemHitachi917List);
            this._mainPanel.Controls.Add(this._uKetQuaXetNghiemTongHop);
            this._mainPanel.Controls.Add(this._uKetQuaXetNghiemTay);
            this._mainPanel.Controls.Add(this._uXetNghiemTay);
            this._mainPanel.Controls.Add(this._uBaoCaoKhachHangMuaThuoc);
            this._mainPanel.Controls.Add(this._uKetQuaXetNghiem_CellDyn3200);
            this._mainPanel.Controls.Add(this._uKetQuaXetNghiem_Hitachi917);
            this._mainPanel.Controls.Add(this._uBookingList);
            this._mainPanel.Controls.Add(this._uNhatKyLienHeCongTy);
            this._mainPanel.Controls.Add(this._uYKienKhachHangList);
            this._mainPanel.Controls.Add(this._uHoaDonHopDongList);
            this._mainPanel.Controls.Add(this._uPhieuThuHopDongList);
            this._mainPanel.Controls.Add(this._uPhucHoiBenhNhan);
            this._mainPanel.Controls.Add(this._uThongKeHoaDon);
            this._mainPanel.Controls.Add(this._uHoaDonXuatTruoc);
            this._mainPanel.Controls.Add(this._uHoaDonThuocList);
            this._mainPanel.Controls.Add(this._uBaoCaoDichVuChuaXuatPhieuThu);
            this._mainPanel.Controls.Add(this._uDoanhThuTheoNgay);
            this._mainPanel.Controls.Add(this._uGiaVonDichVuList);
            this._mainPanel.Controls.Add(this._uInKetQuaKhamSucKhoeTongQuat);
            this._mainPanel.Controls.Add(this._uServiceGroupList);
            this._mainPanel.Controls.Add(this._uTrackingList);
            this._mainPanel.Controls.Add(this._uDichVuTuTuc);
            this._mainPanel.Controls.Add(this._uPhieuThuThuocList);
            this._mainPanel.Controls.Add(this._uBaoCaoThuocTonKho);
            this._mainPanel.Controls.Add(this._uBaoCaoThuocHetHan);
            this._mainPanel.Controls.Add(this._uToaThuocList);
            this._mainPanel.Controls.Add(this._uGiaThuocList);
            this._mainPanel.Controls.Add(this._uLoThuocList);
            this._mainPanel.Controls.Add(this._uNhomThuocList);
            this._mainPanel.Controls.Add(this._uThuocList);
            this._mainPanel.Controls.Add(this._uDichVuHopDong);
            this._mainPanel.Controls.Add(this._uDoanhThuNhanVien);
            this._mainPanel.Controls.Add(this._uInvoiceList);
            this._mainPanel.Controls.Add(this._uReceiptList);
            this._mainPanel.Controls.Add(this._uPrintLabel);
            this._mainPanel.Controls.Add(this._uPermission);
            this._mainPanel.Controls.Add(this._uContractList);
            this._mainPanel.Controls.Add(this._uCompanyList);
            this._mainPanel.Controls.Add(this._uSymptomList);
            this._mainPanel.Controls.Add(this._uSpecialityList);
            this._mainPanel.Controls.Add(this._uPatientHistory);
            this._mainPanel.Controls.Add(this._uPatientList);
            this._mainPanel.Controls.Add(this._uDuplicatePatient);
            this._mainPanel.Controls.Add(this._uDocStaffList);
            this._mainPanel.Controls.Add(this._uServicesList);
            this._mainPanel.Name = "_mainPanel";
            // 
            // _uDanhSachXetNghiem_CellDyn3200List
            // 
            resources.ApplyResources(this._uDanhSachXetNghiem_CellDyn3200List, "_uDanhSachXetNghiem_CellDyn3200List");
            this._uDanhSachXetNghiem_CellDyn3200List.Name = "_uDanhSachXetNghiem_CellDyn3200List";
            // 
            // _uDanhSachXetNghiemHitachi917List
            // 
            resources.ApplyResources(this._uDanhSachXetNghiemHitachi917List, "_uDanhSachXetNghiemHitachi917List");
            this._uDanhSachXetNghiemHitachi917List.Name = "_uDanhSachXetNghiemHitachi917List";
            // 
            // _uKetQuaXetNghiemTongHop
            // 
            resources.ApplyResources(this._uKetQuaXetNghiemTongHop, "_uKetQuaXetNghiemTongHop");
            this._uKetQuaXetNghiemTongHop.Name = "_uKetQuaXetNghiemTongHop";
            // 
            // _uKetQuaXetNghiemTay
            // 
            resources.ApplyResources(this._uKetQuaXetNghiemTay, "_uKetQuaXetNghiemTay");
            this._uKetQuaXetNghiemTay.Name = "_uKetQuaXetNghiemTay";
            // 
            // _uXetNghiemTay
            // 
            resources.ApplyResources(this._uXetNghiemTay, "_uXetNghiemTay");
            this._uXetNghiemTay.Name = "_uXetNghiemTay";
            // 
            // _uBaoCaoKhachHangMuaThuoc
            // 
            resources.ApplyResources(this._uBaoCaoKhachHangMuaThuoc, "_uBaoCaoKhachHangMuaThuoc");
            this._uBaoCaoKhachHangMuaThuoc.Name = "_uBaoCaoKhachHangMuaThuoc";
            // 
            // _uKetQuaXetNghiem_CellDyn3200
            // 
            resources.ApplyResources(this._uKetQuaXetNghiem_CellDyn3200, "_uKetQuaXetNghiem_CellDyn3200");
            this._uKetQuaXetNghiem_CellDyn3200.Name = "_uKetQuaXetNghiem_CellDyn3200";
            // 
            // _uKetQuaXetNghiem_Hitachi917
            // 
            resources.ApplyResources(this._uKetQuaXetNghiem_Hitachi917, "_uKetQuaXetNghiem_Hitachi917");
            this._uKetQuaXetNghiem_Hitachi917.Name = "_uKetQuaXetNghiem_Hitachi917";
            // 
            // _uBookingList
            // 
            resources.ApplyResources(this._uBookingList, "_uBookingList");
            this._uBookingList.Name = "_uBookingList";
            // 
            // _uNhatKyLienHeCongTy
            // 
            resources.ApplyResources(this._uNhatKyLienHeCongTy, "_uNhatKyLienHeCongTy");
            this._uNhatKyLienHeCongTy.Name = "_uNhatKyLienHeCongTy";
            // 
            // _uYKienKhachHangList
            // 
            resources.ApplyResources(this._uYKienKhachHangList, "_uYKienKhachHangList");
            this._uYKienKhachHangList.Name = "_uYKienKhachHangList";
            // 
            // _uHoaDonHopDongList
            // 
            resources.ApplyResources(this._uHoaDonHopDongList, "_uHoaDonHopDongList");
            this._uHoaDonHopDongList.Name = "_uHoaDonHopDongList";
            // 
            // _uPhieuThuHopDongList
            // 
            resources.ApplyResources(this._uPhieuThuHopDongList, "_uPhieuThuHopDongList");
            this._uPhieuThuHopDongList.Name = "_uPhieuThuHopDongList";
            // 
            // _uPhucHoiBenhNhan
            // 
            resources.ApplyResources(this._uPhucHoiBenhNhan, "_uPhucHoiBenhNhan");
            this._uPhucHoiBenhNhan.Name = "_uPhucHoiBenhNhan";
            // 
            // _uThongKeHoaDon
            // 
            resources.ApplyResources(this._uThongKeHoaDon, "_uThongKeHoaDon");
            this._uThongKeHoaDon.Name = "_uThongKeHoaDon";
            // 
            // _uHoaDonXuatTruoc
            // 
            resources.ApplyResources(this._uHoaDonXuatTruoc, "_uHoaDonXuatTruoc");
            this._uHoaDonXuatTruoc.Name = "_uHoaDonXuatTruoc";
            // 
            // _uHoaDonThuocList
            // 
            resources.ApplyResources(this._uHoaDonThuocList, "_uHoaDonThuocList");
            this._uHoaDonThuocList.Name = "_uHoaDonThuocList";
            // 
            // _uBaoCaoDichVuChuaXuatPhieuThu
            // 
            resources.ApplyResources(this._uBaoCaoDichVuChuaXuatPhieuThu, "_uBaoCaoDichVuChuaXuatPhieuThu");
            this._uBaoCaoDichVuChuaXuatPhieuThu.Name = "_uBaoCaoDichVuChuaXuatPhieuThu";
            // 
            // _uDoanhThuTheoNgay
            // 
            resources.ApplyResources(this._uDoanhThuTheoNgay, "_uDoanhThuTheoNgay");
            this._uDoanhThuTheoNgay.Name = "_uDoanhThuTheoNgay";
            // 
            // _uGiaVonDichVuList
            // 
            resources.ApplyResources(this._uGiaVonDichVuList, "_uGiaVonDichVuList");
            this._uGiaVonDichVuList.Name = "_uGiaVonDichVuList";
            // 
            // _uInKetQuaKhamSucKhoeTongQuat
            // 
            resources.ApplyResources(this._uInKetQuaKhamSucKhoeTongQuat, "_uInKetQuaKhamSucKhoeTongQuat");
            this._uInKetQuaKhamSucKhoeTongQuat.Name = "_uInKetQuaKhamSucKhoeTongQuat";
            // 
            // _uServiceGroupList
            // 
            resources.ApplyResources(this._uServiceGroupList, "_uServiceGroupList");
            this._uServiceGroupList.Name = "_uServiceGroupList";
            // 
            // _uTrackingList
            // 
            resources.ApplyResources(this._uTrackingList, "_uTrackingList");
            this._uTrackingList.Name = "_uTrackingList";
            // 
            // _uDichVuTuTuc
            // 
            resources.ApplyResources(this._uDichVuTuTuc, "_uDichVuTuTuc");
            this._uDichVuTuTuc.Name = "_uDichVuTuTuc";
            // 
            // _uPhieuThuThuocList
            // 
            resources.ApplyResources(this._uPhieuThuThuocList, "_uPhieuThuThuocList");
            this._uPhieuThuThuocList.Name = "_uPhieuThuThuocList";
            // 
            // _uBaoCaoThuocTonKho
            // 
            resources.ApplyResources(this._uBaoCaoThuocTonKho, "_uBaoCaoThuocTonKho");
            this._uBaoCaoThuocTonKho.Name = "_uBaoCaoThuocTonKho";
            // 
            // _uBaoCaoThuocHetHan
            // 
            resources.ApplyResources(this._uBaoCaoThuocHetHan, "_uBaoCaoThuocHetHan");
            this._uBaoCaoThuocHetHan.Name = "_uBaoCaoThuocHetHan";
            // 
            // _uToaThuocList
            // 
            resources.ApplyResources(this._uToaThuocList, "_uToaThuocList");
            this._uToaThuocList.Name = "_uToaThuocList";
            this._uToaThuocList.PatientRow = null;
            // 
            // _uGiaThuocList
            // 
            resources.ApplyResources(this._uGiaThuocList, "_uGiaThuocList");
            this._uGiaThuocList.Name = "_uGiaThuocList";
            // 
            // _uLoThuocList
            // 
            resources.ApplyResources(this._uLoThuocList, "_uLoThuocList");
            this._uLoThuocList.Name = "_uLoThuocList";
            // 
            // _uNhomThuocList
            // 
            resources.ApplyResources(this._uNhomThuocList, "_uNhomThuocList");
            this._uNhomThuocList.Name = "_uNhomThuocList";
            // 
            // _uThuocList
            // 
            resources.ApplyResources(this._uThuocList, "_uThuocList");
            this._uThuocList.IsReport = false;
            this._uThuocList.Name = "_uThuocList";
            // 
            // _uDichVuHopDong
            // 
            resources.ApplyResources(this._uDichVuHopDong, "_uDichVuHopDong");
            this._uDichVuHopDong.Name = "_uDichVuHopDong";
            // 
            // _uDoanhThuNhanVien
            // 
            resources.ApplyResources(this._uDoanhThuNhanVien, "_uDoanhThuNhanVien");
            this._uDoanhThuNhanVien.Name = "_uDoanhThuNhanVien";
            // 
            // _uInvoiceList
            // 
            resources.ApplyResources(this._uInvoiceList, "_uInvoiceList");
            this._uInvoiceList.Name = "_uInvoiceList";
            // 
            // _uReceiptList
            // 
            resources.ApplyResources(this._uReceiptList, "_uReceiptList");
            this._uReceiptList.Name = "_uReceiptList";
            // 
            // _uPrintLabel
            // 
            resources.ApplyResources(this._uPrintLabel, "_uPrintLabel");
            this._uPrintLabel.Name = "_uPrintLabel";
            // 
            // _uPermission
            // 
            resources.ApplyResources(this._uPermission, "_uPermission");
            this._uPermission.Name = "_uPermission";
            // 
            // _uContractList
            // 
            resources.ApplyResources(this._uContractList, "_uContractList");
            this._uContractList.Name = "_uContractList";
            // 
            // _uCompanyList
            // 
            resources.ApplyResources(this._uCompanyList, "_uCompanyList");
            this._uCompanyList.Name = "_uCompanyList";
            // 
            // _uSymptomList
            // 
            resources.ApplyResources(this._uSymptomList, "_uSymptomList");
            this._uSymptomList.Name = "_uSymptomList";
            // 
            // _uSpecialityList
            // 
            resources.ApplyResources(this._uSpecialityList, "_uSpecialityList");
            this._uSpecialityList.Name = "_uSpecialityList";
            // 
            // _uPatientHistory
            // 
            resources.ApplyResources(this._uPatientHistory, "_uPatientHistory");
            this._uPatientHistory.Name = "_uPatientHistory";
            // 
            // _uPatientList
            // 
            resources.ApplyResources(this._uPatientList, "_uPatientList");
            this._uPatientList.Name = "_uPatientList";
            // 
            // _uDuplicatePatient
            // 
            resources.ApplyResources(this._uDuplicatePatient, "_uDuplicatePatient");
            this._uDuplicatePatient.Name = "_uDuplicatePatient";
            // 
            // _uDocStaffList
            // 
            resources.ApplyResources(this._uDocStaffList, "_uDocStaffList");
            this._uDocStaffList.Name = "_uDocStaffList";
            // 
            // _uServicesList
            // 
            resources.ApplyResources(this._uServicesList, "_uServicesList");
            this._uServicesList.Name = "_uServicesList";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dockSite2);
            this.Controls.Add(this.dockSite1);
            this.Controls.Add(this._mainPanel);
            this.Controls.Add(this._mainToolbar);
            this.Controls.Add(this._mainMenu);
            this.Controls.Add(this.dockSite3);
            this.Controls.Add(this.dockSite4);
            this.Controls.Add(this.dockSite5);
            this.Controls.Add(this.dockSite6);
            this.Controls.Add(this.dockSite7);
            this.Controls.Add(this.dockSite8);
            this.Controls.Add(this._mainStatus);
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this._mainToolbar.ResumeLayout(false);
            this._mainToolbar.PerformLayout();
            this._mainStatus.ResumeLayout(false);
            this._mainStatus.PerformLayout();
            this._mainMenu.ResumeLayout(false);
            this._mainMenu.PerformLayout();
            this.dockSite4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.bar1.ResumeLayout(false);
            this.panelDockContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPatient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).EndInit();
            this.bar2.ResumeLayout(false);
            this.panelDockContainer2.ResumeLayout(false);
            this._mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tbServiceList;
        private System.Windows.Forms.ToolStrip _mainToolbar;
        private System.Windows.Forms.ToolStripButton tbDoctorList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tbOpenPatient;
        private System.Windows.Forms.ToolStripButton tbPatientList;
        private System.Windows.Forms.StatusStrip _mainStatus;
        private System.Windows.Forms.MenuStrip _mainMenu;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem servicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviceListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openPatientToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem patientListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medicalManagementHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem aboutMedicalManagementToolStripMenuItem;
        private System.Windows.Forms.Panel _mainPanel;
        private Controls.uServicesList _uServicesList;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tbLogin;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private Controls.uDocStaffList _uDocStaffList;
        private Controls.uPatientList _uPatientList;
        private Controls.uDuplicatePatient _uDuplicatePatient;
        private Controls.uPatientHistory _uPatientHistory;
        private System.Windows.Forms.ToolStripButton tbSpecialityList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private Controls.uSpecialityList _uSpecialityList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tbSympton;
        private Controls.uSymptomList _uSymptomList;
        private System.Windows.Forms.ToolStripMenuItem companyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem companyListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton tbCompanyList;
        private System.Windows.Forms.ToolStripButton tbContractList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem contractListToolStripMenuItem;
        private Controls.uCompanyList _uCompanyList;
        private Controls.uContractList _uContractList;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dICOMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem permissionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private Controls.uPermission _uPermission;
        private System.Windows.Forms.ToolStripMenuItem printLabelToolStripMenuItem;
        private Controls.uPrintLabel _uPrintLabel;
        private System.Windows.Forms.ToolStripMenuItem receiptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem receiptListToolStripMenuItem;
        private Controls.uReceiptList _uReceiptList;
        private System.Windows.Forms.ToolStripButton tbReceiptList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem invoiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invoiceListToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tbInvoiceList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private Controls.uInvoiceList _uInvoiceList;
        private System.Windows.Forms.ToolStripMenuItem DuplicatePatientToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripButton tbDuplicatePatient;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doanhThuNhanVienToolStripMenuItem;
        private Controls.uDoanhThuNhanVien _uDoanhThuNhanVien;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripMenuItem dichVuHopDongToolStripMenuItem;
        private Controls.uDichVuHopDong _uDichVuHopDong;
        private System.Windows.Forms.ToolStripMenuItem thuocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem danhMucThuocToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripMenuItem nhomThuocToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
        private System.Windows.Forms.ToolStripButton tbDanhMucThuoc;
        private System.Windows.Forms.ToolStripButton tbNhomThuoc;
        private Controls.uThuocList _uThuocList;
        private Controls.uNhomThuocList _uNhomThuocList;
        private System.Windows.Forms.ToolStripButton tbLoThuoc;
        private System.Windows.Forms.ToolStripButton tbGiaThuoc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
        private System.Windows.Forms.ToolStripMenuItem loThuocToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
        private System.Windows.Forms.ToolStripMenuItem giaThuocToolStripMenuItem;
        private Controls.uLoThuocList _uLoThuocList;
        private Controls.uGiaThuocList _uGiaThuocList;
        private System.Windows.Forms.ToolStripButton tbKeToa;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
        private System.Windows.Forms.ToolStripMenuItem keToaToolStripMenuItem;
        private Controls.uToaThuocList _uToaThuocList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator22;
        private System.Windows.Forms.ToolStripMenuItem thuocHetHanToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;
        private System.Windows.Forms.ToolStripMenuItem thuocTonKhoToolStripMenuItem;
        private Controls.uBaoCaoThuocHetHan _uBaoCaoThuocHetHan;
        private Controls.uBaoCaoThuocTonKho _uBaoCaoThuocTonKho;
        private System.Windows.Forms.ToolStripButton tbPhieuThuThuoc;
        private Controls.uPhieuThuThuocList _uPhieuThuThuocList;
        private System.Windows.Forms.ToolStripMenuItem templateExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator25;
        private System.Windows.Forms.ToolStripMenuItem dichVuTuTucToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator26;
        private Controls.uDichVuTuTuc _uDichVuTuTuc;
        private DevComponents.DotNetBar.DotNetBarManager _dotNetBarManager;
        private DevComponents.DotNetBar.DockSite dockSite4;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.PanelDockContainer panelDockContainer1;
        private DevComponents.DotNetBar.DockContainerItem dockContainerItem1;
        private DevComponents.DotNetBar.DockSite dockSite1;
        private DevComponents.DotNetBar.DockSite dockSite2;
        private DevComponents.DotNetBar.DockSite dockSite3;
        private DevComponents.DotNetBar.DockSite dockSite5;
        private DevComponents.DotNetBar.DockSite dockSite6;
        private DevComponents.DotNetBar.DockSite dockSite7;
        private DevComponents.DotNetBar.DockSite dockSite8;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgPatient;
        private System.Windows.Forms.BindingSource patientViewBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileNumDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fullname;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn GenderAsStr;
        private System.Windows.Forms.DataGridViewTextBoxColumn dobDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn identityCardDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn homePhoneDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn workPhoneDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mobileDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailDataGridViewTextBoxColumn;
        private Controls.uTrackingList _uTrackingList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator27;
        private System.Windows.Forms.ToolStripMenuItem trackingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator28;
        private System.Windows.Forms.ToolStripMenuItem serviceGroupToolStripMenuItem;
        private Controls.uServiceGroupList _uServiceGroupList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator29;
        private System.Windows.Forms.ToolStripMenuItem inKetQuaKhamSucKhoeTongQuatToolStripMenuItem;
        private Controls.uInKetQuaKhamSucKhoeTongQuat _uInKetQuaKhamSucKhoeTongQuat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator30;
        private System.Windows.Forms.ToolStripMenuItem giaVonDichVuToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tbNhomDichVu;
        private System.Windows.Forms.ToolStripButton tbGiaVonDichVu;
        private Controls.uGiaVonDichVuList _uGiaVonDichVuList;
        private System.Windows.Forms.ToolStripMenuItem doanhThuTheoNgayToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator31;
        private Controls.uDoanhThuTheoNgay _uDoanhThuTheoNgay;
        private DevComponents.DotNetBar.Bar bar2;
        private DevComponents.DotNetBar.PanelDockContainer panelDockContainer2;
        private DevComponents.DotNetBar.DockContainerItem dockContainerItem2;
        private Controls.uPhongChoList _uPhongChoList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator32;
        private System.Windows.Forms.ToolStripMenuItem dichVuChuaXuatPhieuThuToolStripMenuItem;
        private Controls.uBaoCaoDichVuChuaXuatPhieuThu _uBaoCaoDichVuChuaXuatPhieuThu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;
        private System.Windows.Forms.ToolStripMenuItem phieuThuThuocToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator33;
        private System.Windows.Forms.ToolStripMenuItem hoaDonThuocToolStripMenuItem;
        private Controls.uHoaDonThuocList _uHoaDonThuocList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator34;
        private System.Windows.Forms.ToolStripMenuItem hoaDonXuatTruocToolStripMenuItem;
        private Controls.uHoaDonXuatTruoc _uHoaDonXuatTruoc;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator35;
        private System.Windows.Forms.ToolStripMenuItem thongKeHoaDonToolStripMenuItem;
        private Controls.uThongKeHoaDon _uThongKeHoaDon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator36;
        private System.Windows.Forms.ToolStripMenuItem phucHoiBenhNhanToolStripMenuItem;
        private Controls.uPhucHoiBenhNhan _uPhucHoiBenhNhan;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator37;
        private System.Windows.Forms.ToolStripMenuItem phieuThuHopDongToolStripMenuItem;
        private Controls.uPhieuThuHopDongList _uPhieuThuHopDongList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator38;
        private System.Windows.Forms.ToolStripMenuItem hoaDonHopDongToolStripMenuItem;
        private Controls.uHoaDonHopDongList _uHoaDonHopDongList;
        private System.Windows.Forms.ToolStripMenuItem chamSocKhachHangToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yKienKhachHangToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator39;
        private System.Windows.Forms.ToolStripMenuItem nhatKyLienHeCongTyToolStripMenuItem;
        private Controls.uYKienKhachHangList _uYKienKhachHangList;
        private Controls.uNhatKyLienHeCongTy _uNhatKyLienHeCongTy;
        private System.Windows.Forms.ToolStripMenuItem danhmụcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chuyenKhoaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator40;
        private System.Windows.Forms.ToolStripMenuItem nhanVienToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator41;
        private System.Windows.Forms.ToolStripMenuItem trieuChungToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem biểuMẫuPhòngSaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator42;
        private System.Windows.Forms.ToolStripMenuItem bookingToolStripMenuItem;
        private Controls.uBookingList _uBookingList;
        private System.Windows.Forms.ToolStripMenuItem xetNghiemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xetNghiemHiTachi917ToolStripMenuItem;
        private Controls.uKetQuaXetNghiem_Hitachi917 _uKetQuaXetNghiem_Hitachi917;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator43;
        private System.Windows.Forms.ToolStripMenuItem cauHinhKetNoiToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator44;
        private System.Windows.Forms.ToolStripMenuItem xetNghiemCellDyn3200ToolStripMenuItem;
        private Controls.uKetQuaXetNghiem_CellDyn3200 _uKetQuaXetNghiem_CellDyn3200;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator45;
        private System.Windows.Forms.ToolStripMenuItem baoCaoKhachHangMuaThuocToolStripMenuItem;
        private Controls.uBaoCaoKhachHangMuaThuoc _uBaoCaoKhachHangMuaThuoc;
        private System.Windows.Forms.ToolStripMenuItem xetNghiemTayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ketQuaXetNghiemTayToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator46;
        private System.Windows.Forms.ToolStripMenuItem ketQuaXetNghiemTongQuatToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator47;
        private Controls.uXetNghiemTay _uXetNghiemTay;
        private Controls.uKetQuaXetNghiemTay _uKetQuaXetNghiemTay;
        private Controls.uKetQuaXetNghiemTongHop _uKetQuaXetNghiemTongHop;
        private System.Windows.Forms.ToolStripMenuItem danhSachXetNghiemHitachi917ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem danhSachXetNghiemCellDyn3200ToolStripMenuItem;
        private Controls.uDanhSachXetNghiem_CellDyn3200List _uDanhSachXetNghiem_CellDyn3200List;
        private Controls.uDanhSachXetNghiemHitachi917List _uDanhSachXetNghiemHitachi917List;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator48;
        private System.Windows.Forms.ToolStripMenuItem baoCaoSoLuongKhamToolStripMenuItem;
        private Controls.uBaoCaoSoLuongKham _uBaoCaoSoLuongKham;

    }
}

