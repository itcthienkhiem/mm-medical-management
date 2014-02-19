namespace MM.Controls
{
    partial class uHoaDonHopDongList
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
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.raDaXoa = new System.Windows.Forms.RadioButton();
            this.raChuaXoa = new System.Windows.Forms.RadioButton();
            this.raTatCa = new System.Windows.Forms.RadioButton();
            this.btnView = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnExportInvoice = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgInvoice = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.soHoaDonDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayXuatHoaDonDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenNguoiMuaHangDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenDonViDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DaThuTien = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.maSoThueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diaChiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soTaiKhoanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hinhThucThanhToanStrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vATDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NguoiTao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctmAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.xuatHoaDonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.xemBanInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hoaDonHopDongViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._printDialog = new System.Windows.Forms.PrintDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaSoThue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTenDonVi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkTuNgay = new System.Windows.Forms.CheckBox();
            this.txtTenKhachHang = new System.Windows.Forms.TextBox();
            this.dtpkDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpkTuNgay = new System.Windows.Forms.DateTimePicker();
            this.chkDenNgay = new System.Windows.Forms.CheckBox();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInvoice)).BeginInit();
            this.ctmAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hoaDonHopDongViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtMaSoThue);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.txtTenDonVi);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.chkTuNgay);
            this.panel3.Controls.Add(this.txtTenKhachHang);
            this.panel3.Controls.Add(this.dtpkDenNgay);
            this.panel3.Controls.Add(this.dtpkTuNgay);
            this.panel3.Controls.Add(this.chkDenNgay);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.btnView);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(783, 135);
            this.panel3.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.raDaXoa);
            this.groupBox1.Controls.Add(this.raChuaXoa);
            this.groupBox1.Controls.Add(this.raTatCa);
            this.groupBox1.Location = new System.Drawing.Point(10, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 35);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            // 
            // raDaXoa
            // 
            this.raDaXoa.AutoSize = true;
            this.raDaXoa.Location = new System.Drawing.Point(271, 11);
            this.raDaXoa.Name = "raDaXoa";
            this.raDaXoa.Size = new System.Drawing.Size(59, 17);
            this.raDaXoa.TabIndex = 3;
            this.raDaXoa.Text = "Đã xóa";
            this.raDaXoa.UseVisualStyleBackColor = true;
            // 
            // raChuaXoa
            // 
            this.raChuaXoa.AutoSize = true;
            this.raChuaXoa.Checked = true;
            this.raChuaXoa.Location = new System.Drawing.Point(137, 11);
            this.raChuaXoa.Name = "raChuaXoa";
            this.raChuaXoa.Size = new System.Drawing.Size(70, 17);
            this.raChuaXoa.TabIndex = 2;
            this.raChuaXoa.TabStop = true;
            this.raChuaXoa.Text = "Chưa xóa";
            this.raChuaXoa.UseVisualStyleBackColor = true;
            // 
            // raTatCa
            // 
            this.raTatCa.AutoSize = true;
            this.raTatCa.Location = new System.Drawing.Point(20, 11);
            this.raTatCa.Name = "raTatCa";
            this.raTatCa.Size = new System.Drawing.Size(56, 17);
            this.raTatCa.TabIndex = 1;
            this.raTatCa.Text = "Tất cả";
            this.raTatCa.UseVisualStyleBackColor = true;
            // 
            // btnView
            // 
            this.btnView.Image = global::MM.Properties.Resources.views_icon;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(391, 101);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 24;
            this.btnView.Text = "   &Xem";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPrintPreview);
            this.panel1.Controls.Add(this.btnExportInvoice);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 458);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(783, 38);
            this.panel1.TabIndex = 6;
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Image = global::MM.Properties.Resources.Actions_print_preview_icon;
            this.btnPrintPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintPreview.Location = new System.Drawing.Point(197, 6);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(93, 25);
            this.btnPrintPreview.TabIndex = 5;
            this.btnPrintPreview.Text = "      &Xem bản in";
            this.btnPrintPreview.UseVisualStyleBackColor = true;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // btnExportInvoice
            // 
            this.btnExportInvoice.Image = global::MM.Properties.Resources.invoice_icon;
            this.btnExportInvoice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportInvoice.Location = new System.Drawing.Point(8, 6);
            this.btnExportInvoice.Name = "btnExportInvoice";
            this.btnExportInvoice.Size = new System.Drawing.Size(106, 25);
            this.btnExportInvoice.TabIndex = 2;
            this.btnExportInvoice.Text = "      &Xuất hóa đơn";
            this.btnExportInvoice.UseVisualStyleBackColor = true;
            this.btnExportInvoice.Click += new System.EventHandler(this.btnExportInvoice_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = global::MM.Properties.Resources.Printer_icon__1_;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(294, 6);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(97, 25);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "      &In hóa đơn";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::MM.Properties.Resources.del;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(118, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "    &Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(44, 140);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 25;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgInvoice
            // 
            this.dgInvoice.AllowUserToAddRows = false;
            this.dgInvoice.AllowUserToDeleteRows = false;
            this.dgInvoice.AllowUserToOrderColumns = true;
            this.dgInvoice.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgInvoice.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgInvoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgInvoice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.soHoaDonDataGridViewTextBoxColumn,
            this.ngayXuatHoaDonDataGridViewTextBoxColumn,
            this.tenNguoiMuaHangDataGridViewTextBoxColumn,
            this.tenDonViDataGridViewTextBoxColumn,
            this.DaThuTien,
            this.maSoThueDataGridViewTextBoxColumn,
            this.diaChiDataGridViewTextBoxColumn,
            this.soTaiKhoanDataGridViewTextBoxColumn,
            this.hinhThucThanhToanStrDataGridViewTextBoxColumn,
            this.vATDataGridViewTextBoxColumn,
            this.notesDataGridViewTextBoxColumn,
            this.NguoiTao});
            this.dgInvoice.ContextMenuStrip = this.ctmAction;
            this.dgInvoice.DataSource = this.hoaDonHopDongViewBindingSource;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgInvoice.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgInvoice.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgInvoice.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgInvoice.HighlightSelectedColumnHeaders = false;
            this.dgInvoice.Location = new System.Drawing.Point(0, 135);
            this.dgInvoice.MultiSelect = false;
            this.dgInvoice.Name = "dgInvoice";
            this.dgInvoice.RowHeadersWidth = 30;
            this.dgInvoice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgInvoice.Size = new System.Drawing.Size(783, 323);
            this.dgInvoice.TabIndex = 7;
            this.dgInvoice.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgInvoice_CellContentClick);
            this.dgInvoice.DoubleClick += new System.EventHandler(this.dgInvoice_DoubleClick);
            // 
            // colChecked
            // 
            this.colChecked.DataPropertyName = "Checked";
            this.colChecked.Frozen = true;
            this.colChecked.HeaderText = "";
            this.colChecked.Name = "colChecked";
            this.colChecked.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colChecked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colChecked.Width = 40;
            // 
            // soHoaDonDataGridViewTextBoxColumn
            // 
            this.soHoaDonDataGridViewTextBoxColumn.DataPropertyName = "SoHoaDon";
            this.soHoaDonDataGridViewTextBoxColumn.HeaderText = "Mã hóa đơn";
            this.soHoaDonDataGridViewTextBoxColumn.Name = "soHoaDonDataGridViewTextBoxColumn";
            this.soHoaDonDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ngayXuatHoaDonDataGridViewTextBoxColumn
            // 
            this.ngayXuatHoaDonDataGridViewTextBoxColumn.DataPropertyName = "NgayXuatHoaDon";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            dataGridViewCellStyle2.NullValue = null;
            this.ngayXuatHoaDonDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.ngayXuatHoaDonDataGridViewTextBoxColumn.HeaderText = "Ngày xuất";
            this.ngayXuatHoaDonDataGridViewTextBoxColumn.Name = "ngayXuatHoaDonDataGridViewTextBoxColumn";
            this.ngayXuatHoaDonDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tenNguoiMuaHangDataGridViewTextBoxColumn
            // 
            this.tenNguoiMuaHangDataGridViewTextBoxColumn.DataPropertyName = "TenNguoiMuaHang";
            this.tenNguoiMuaHangDataGridViewTextBoxColumn.HeaderText = "Người mua hàng";
            this.tenNguoiMuaHangDataGridViewTextBoxColumn.Name = "tenNguoiMuaHangDataGridViewTextBoxColumn";
            this.tenNguoiMuaHangDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenNguoiMuaHangDataGridViewTextBoxColumn.Width = 200;
            // 
            // tenDonViDataGridViewTextBoxColumn
            // 
            this.tenDonViDataGridViewTextBoxColumn.DataPropertyName = "TenDonVi";
            this.tenDonViDataGridViewTextBoxColumn.HeaderText = "Tên đơn vị";
            this.tenDonViDataGridViewTextBoxColumn.Name = "tenDonViDataGridViewTextBoxColumn";
            this.tenDonViDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenDonViDataGridViewTextBoxColumn.Width = 200;
            // 
            // DaThuTien
            // 
            this.DaThuTien.DataPropertyName = "DaThuTien";
            this.DaThuTien.HeaderText = "Đã thu tiền";
            this.DaThuTien.Name = "DaThuTien";
            this.DaThuTien.ReadOnly = true;
            this.DaThuTien.Width = 80;
            // 
            // maSoThueDataGridViewTextBoxColumn
            // 
            this.maSoThueDataGridViewTextBoxColumn.DataPropertyName = "MaSoThue";
            this.maSoThueDataGridViewTextBoxColumn.HeaderText = "Mã số thuế";
            this.maSoThueDataGridViewTextBoxColumn.Name = "maSoThueDataGridViewTextBoxColumn";
            this.maSoThueDataGridViewTextBoxColumn.ReadOnly = true;
            this.maSoThueDataGridViewTextBoxColumn.Width = 120;
            // 
            // diaChiDataGridViewTextBoxColumn
            // 
            this.diaChiDataGridViewTextBoxColumn.DataPropertyName = "DiaChi";
            this.diaChiDataGridViewTextBoxColumn.HeaderText = "Địa chỉ";
            this.diaChiDataGridViewTextBoxColumn.Name = "diaChiDataGridViewTextBoxColumn";
            this.diaChiDataGridViewTextBoxColumn.ReadOnly = true;
            this.diaChiDataGridViewTextBoxColumn.Width = 200;
            // 
            // soTaiKhoanDataGridViewTextBoxColumn
            // 
            this.soTaiKhoanDataGridViewTextBoxColumn.DataPropertyName = "SoTaiKhoan";
            this.soTaiKhoanDataGridViewTextBoxColumn.HeaderText = "Số tài khoản";
            this.soTaiKhoanDataGridViewTextBoxColumn.Name = "soTaiKhoanDataGridViewTextBoxColumn";
            this.soTaiKhoanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // hinhThucThanhToanStrDataGridViewTextBoxColumn
            // 
            this.hinhThucThanhToanStrDataGridViewTextBoxColumn.DataPropertyName = "HinhThucThanhToanStr";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.hinhThucThanhToanStrDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.hinhThucThanhToanStrDataGridViewTextBoxColumn.HeaderText = "Hình thức thanh toán";
            this.hinhThucThanhToanStrDataGridViewTextBoxColumn.Name = "hinhThucThanhToanStrDataGridViewTextBoxColumn";
            this.hinhThucThanhToanStrDataGridViewTextBoxColumn.ReadOnly = true;
            this.hinhThucThanhToanStrDataGridViewTextBoxColumn.Width = 150;
            // 
            // vATDataGridViewTextBoxColumn
            // 
            this.vATDataGridViewTextBoxColumn.DataPropertyName = "VAT";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.NullValue = null;
            this.vATDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.vATDataGridViewTextBoxColumn.HeaderText = "VAT";
            this.vATDataGridViewTextBoxColumn.Name = "vATDataGridViewTextBoxColumn";
            this.vATDataGridViewTextBoxColumn.ReadOnly = true;
            this.vATDataGridViewTextBoxColumn.Width = 80;
            // 
            // notesDataGridViewTextBoxColumn
            // 
            this.notesDataGridViewTextBoxColumn.DataPropertyName = "Notes";
            this.notesDataGridViewTextBoxColumn.HeaderText = "Ghi chú";
            this.notesDataGridViewTextBoxColumn.Name = "notesDataGridViewTextBoxColumn";
            this.notesDataGridViewTextBoxColumn.ReadOnly = true;
            this.notesDataGridViewTextBoxColumn.Width = 250;
            // 
            // NguoiTao
            // 
            this.NguoiTao.DataPropertyName = "NguoiTao";
            this.NguoiTao.HeaderText = "Người xuất";
            this.NguoiTao.Name = "NguoiTao";
            this.NguoiTao.ReadOnly = true;
            this.NguoiTao.Width = 200;
            // 
            // ctmAction
            // 
            this.ctmAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xuatHoaDonToolStripMenuItem,
            this.toolStripSeparator5,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator4,
            this.xemBanInToolStripMenuItem,
            this.toolStripSeparator1,
            this.printToolStripMenuItem});
            this.ctmAction.Name = "cmtAction";
            this.ctmAction.Size = new System.Drawing.Size(146, 110);
            // 
            // xuatHoaDonToolStripMenuItem
            // 
            this.xuatHoaDonToolStripMenuItem.Image = global::MM.Properties.Resources.invoice_icon;
            this.xuatHoaDonToolStripMenuItem.Name = "xuatHoaDonToolStripMenuItem";
            this.xuatHoaDonToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.xuatHoaDonToolStripMenuItem.Text = "Xuất hóa đơn";
            this.xuatHoaDonToolStripMenuItem.Click += new System.EventHandler(this.xuatHoaDonToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(142, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::MM.Properties.Resources.del;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.deleteToolStripMenuItem.Text = "Xóa";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(142, 6);
            // 
            // xemBanInToolStripMenuItem
            // 
            this.xemBanInToolStripMenuItem.Image = global::MM.Properties.Resources.Actions_print_preview_icon;
            this.xemBanInToolStripMenuItem.Name = "xemBanInToolStripMenuItem";
            this.xemBanInToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.xemBanInToolStripMenuItem.Text = "Xem bản in";
            this.xemBanInToolStripMenuItem.Click += new System.EventHandler(this.xemBanInToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(142, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = global::MM.Properties.Resources.Printer_icon__1_;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.printToolStripMenuItem.Text = "In hóa đơn";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // hoaDonHopDongViewBindingSource
            // 
            this.hoaDonHopDongViewBindingSource.DataSource = typeof(MM.Databasae.HoaDonHopDongView);
            // 
            // _printDialog
            // 
            this._printDialog.UseEXDialog = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 40;
            this.label3.Text = "Mã số thuế:";
            // 
            // txtMaSoThue
            // 
            this.txtMaSoThue.Location = new System.Drawing.Point(119, 74);
            this.txtMaSoThue.Name = "txtMaSoThue";
            this.txtMaSoThue.Size = new System.Drawing.Size(266, 20);
            this.txtMaSoThue.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "Tên đơn vị:";
            // 
            // txtTenDonVi
            // 
            this.txtTenDonVi.Location = new System.Drawing.Point(119, 52);
            this.txtTenDonVi.Name = "txtTenDonVi";
            this.txtTenDonVi.Size = new System.Drawing.Size(266, 20);
            this.txtTenDonVi.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Tên người mua hàng:";
            // 
            // chkTuNgay
            // 
            this.chkTuNgay.AutoSize = true;
            this.chkTuNgay.Checked = true;
            this.chkTuNgay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTuNgay.Location = new System.Drawing.Point(10, 9);
            this.chkTuNgay.Name = "chkTuNgay";
            this.chkTuNgay.Size = new System.Drawing.Size(65, 17);
            this.chkTuNgay.TabIndex = 0;
            this.chkTuNgay.Text = "Từ ngày";
            this.chkTuNgay.UseVisualStyleBackColor = true;
            this.chkTuNgay.CheckedChanged += new System.EventHandler(this.chkTuNgay_CheckedChanged);
            // 
            // txtTenKhachHang
            // 
            this.txtTenKhachHang.Location = new System.Drawing.Point(119, 30);
            this.txtTenKhachHang.Name = "txtTenKhachHang";
            this.txtTenKhachHang.Size = new System.Drawing.Size(266, 20);
            this.txtTenKhachHang.TabIndex = 4;
            // 
            // dtpkDenNgay
            // 
            this.dtpkDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkDenNgay.Location = new System.Drawing.Point(272, 7);
            this.dtpkDenNgay.Name = "dtpkDenNgay";
            this.dtpkDenNgay.Size = new System.Drawing.Size(113, 20);
            this.dtpkDenNgay.TabIndex = 3;
            // 
            // dtpkTuNgay
            // 
            this.dtpkTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkTuNgay.Location = new System.Drawing.Point(78, 7);
            this.dtpkTuNgay.Name = "dtpkTuNgay";
            this.dtpkTuNgay.Size = new System.Drawing.Size(113, 20);
            this.dtpkTuNgay.TabIndex = 1;
            // 
            // chkDenNgay
            // 
            this.chkDenNgay.AutoSize = true;
            this.chkDenNgay.Checked = true;
            this.chkDenNgay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDenNgay.Location = new System.Drawing.Point(202, 9);
            this.chkDenNgay.Name = "chkDenNgay";
            this.chkDenNgay.Size = new System.Drawing.Size(71, 17);
            this.chkDenNgay.TabIndex = 2;
            this.chkDenNgay.Text = "đến ngày";
            this.chkDenNgay.UseVisualStyleBackColor = true;
            this.chkDenNgay.CheckedChanged += new System.EventHandler(this.chkDenNgay_CheckedChanged);
            // 
            // uHoaDonHopDongList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkChecked);
            this.Controls.Add(this.dgInvoice);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Name = "uHoaDonHopDongList";
            this.Size = new System.Drawing.Size(783, 496);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgInvoice)).EndInit();
            this.ctmAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hoaDonHopDongViewBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton raDaXoa;
        private System.Windows.Forms.RadioButton raChuaXoa;
        private System.Windows.Forms.RadioButton raTatCa;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExportInvoice;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgInvoice;
        private System.Windows.Forms.PrintDialog _printDialog;
        private System.Windows.Forms.BindingSource hoaDonHopDongViewBindingSource;
        protected System.Windows.Forms.ContextMenuStrip ctmAction;
        private System.Windows.Forms.ToolStripMenuItem xuatHoaDonToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn soHoaDonDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayXuatHoaDonDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenNguoiMuaHangDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenDonViDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DaThuTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn maSoThueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diaChiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soTaiKhoanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hinhThucThanhToanStrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vATDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn notesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NguoiTao;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.ToolStripMenuItem xemBanInToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMaSoThue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTenDonVi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkTuNgay;
        private System.Windows.Forms.TextBox txtTenKhachHang;
        private System.Windows.Forms.DateTimePicker dtpkDenNgay;
        private System.Windows.Forms.DateTimePicker dtpkTuNgay;
        private System.Windows.Forms.CheckBox chkDenNgay;
    }
}
