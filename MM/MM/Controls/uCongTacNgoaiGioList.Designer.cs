namespace MM.Controls
{
    partial class uCongTacNgoaiGioList
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbKetQuaTimDuoc = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.txtTenBenhNhan = new System.Windows.Forms.TextBox();
            this.dtpkDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpkTuNgay = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgCongTacNgoaiGio = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ngayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenNguoiLam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mucDichDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gioVaoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gioRaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ketQuaDanhGiaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenNguoiDeXuatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ghiChuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.congTacNgoaiGioViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._printDialog = new System.Windows.Forms.PrintDialog();
            this.ctmAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exportExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCongTacNgoaiGio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.congTacNgoaiGioViewBindingSource)).BeginInit();
            this.ctmAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lbKetQuaTimDuoc);
            this.panel2.Controls.Add(this.btnView);
            this.panel2.Controls.Add(this.txtTenBenhNhan);
            this.panel2.Controls.Add(this.dtpkDenNgay);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.dtpkTuNgay);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1058, 64);
            this.panel2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Tên người làm:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Từ ngày";
            // 
            // lbKetQuaTimDuoc
            // 
            this.lbKetQuaTimDuoc.AutoSize = true;
            this.lbKetQuaTimDuoc.ForeColor = System.Drawing.Color.Blue;
            this.lbKetQuaTimDuoc.Location = new System.Drawing.Point(455, 37);
            this.lbKetQuaTimDuoc.Name = "lbKetQuaTimDuoc";
            this.lbKetQuaTimDuoc.Size = new System.Drawing.Size(100, 13);
            this.lbKetQuaTimDuoc.TabIndex = 15;
            this.lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
            // 
            // btnView
            // 
            this.btnView.Image = global::MM.Properties.Resources.views_icon;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(363, 32);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 13;
            this.btnView.Text = "   &Xem";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // txtTenBenhNhan
            // 
            this.txtTenBenhNhan.Location = new System.Drawing.Point(99, 34);
            this.txtTenBenhNhan.Name = "txtTenBenhNhan";
            this.txtTenBenhNhan.Size = new System.Drawing.Size(258, 20);
            this.txtTenBenhNhan.TabIndex = 5;
            // 
            // dtpkDenNgay
            // 
            this.dtpkDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkDenNgay.Location = new System.Drawing.Point(244, 10);
            this.dtpkDenNgay.Name = "dtpkDenNgay";
            this.dtpkDenNgay.Size = new System.Drawing.Size(113, 20);
            this.dtpkDenNgay.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(187, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "đến ngày";
            // 
            // dtpkTuNgay
            // 
            this.dtpkTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkTuNgay.Location = new System.Drawing.Point(66, 10);
            this.dtpkTuNgay.Name = "dtpkTuNgay";
            this.dtpkTuNgay.Size = new System.Drawing.Size(113, 20);
            this.dtpkTuNgay.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPrintPreview);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnExportExcel);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 457);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1058, 38);
            this.panel1.TabIndex = 4;
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Image = global::MM.Properties.Resources.Actions_print_preview_icon;
            this.btnPrintPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintPreview.Location = new System.Drawing.Point(341, 6);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(93, 25);
            this.btnPrintPreview.TabIndex = 88;
            this.btnPrintPreview.Text = "      &Xem bản in";
            this.btnPrintPreview.UseVisualStyleBackColor = true;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = global::MM.Properties.Resources.Printer_icon__1_;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(438, 6);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(64, 25);
            this.btnPrint.TabIndex = 89;
            this.btnPrint.Text = "   &In";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Image = global::MM.Properties.Resources.page_excel_icon;
            this.btnExportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExcel.Location = new System.Drawing.Point(244, 6);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(93, 25);
            this.btnExportExcel.TabIndex = 87;
            this.btnExportExcel.Text = "      &Xuất Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::MM.Properties.Resources.del;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(165, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 86;
            this.btnDelete.Text = "    &Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::MM.Properties.Resources.edit;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(86, 6);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 25);
            this.btnEdit.TabIndex = 85;
            this.btnEdit.Text = "    &Sửa";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::MM.Properties.Resources.add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(7, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 25);
            this.btnAdd.TabIndex = 84;
            this.btnAdd.Text = "    &Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkChecked);
            this.panel3.Controls.Add(this.dgCongTacNgoaiGio);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 64);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1058, 393);
            this.panel3.TabIndex = 5;
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(44, 5);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 5;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgCongTacNgoaiGio
            // 
            this.dgCongTacNgoaiGio.AllowUserToAddRows = false;
            this.dgCongTacNgoaiGio.AllowUserToDeleteRows = false;
            this.dgCongTacNgoaiGio.AllowUserToOrderColumns = true;
            this.dgCongTacNgoaiGio.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCongTacNgoaiGio.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgCongTacNgoaiGio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCongTacNgoaiGio.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.ngayDataGridViewTextBoxColumn,
            this.TenNguoiLam,
            this.mucDichDataGridViewTextBoxColumn,
            this.gioVaoDataGridViewTextBoxColumn,
            this.gioRaDataGridViewTextBoxColumn,
            this.ketQuaDanhGiaDataGridViewTextBoxColumn,
            this.tenNguoiDeXuatDataGridViewTextBoxColumn,
            this.ghiChuDataGridViewTextBoxColumn});
            this.dgCongTacNgoaiGio.ContextMenuStrip = this.ctmAction;
            this.dgCongTacNgoaiGio.DataSource = this.congTacNgoaiGioViewBindingSource;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgCongTacNgoaiGio.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgCongTacNgoaiGio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgCongTacNgoaiGio.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgCongTacNgoaiGio.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgCongTacNgoaiGio.HighlightSelectedColumnHeaders = false;
            this.dgCongTacNgoaiGio.Location = new System.Drawing.Point(0, 0);
            this.dgCongTacNgoaiGio.MultiSelect = false;
            this.dgCongTacNgoaiGio.Name = "dgCongTacNgoaiGio";
            this.dgCongTacNgoaiGio.RowHeadersWidth = 30;
            this.dgCongTacNgoaiGio.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCongTacNgoaiGio.Size = new System.Drawing.Size(1058, 393);
            this.dgCongTacNgoaiGio.TabIndex = 4;
            this.dgCongTacNgoaiGio.DoubleClick += new System.EventHandler(this.dgCongTacNgoaiGio_DoubleClick);
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
            // ngayDataGridViewTextBoxColumn
            // 
            this.ngayDataGridViewTextBoxColumn.DataPropertyName = "Ngay";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            dataGridViewCellStyle2.NullValue = null;
            this.ngayDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.ngayDataGridViewTextBoxColumn.HeaderText = "Ngày";
            this.ngayDataGridViewTextBoxColumn.Name = "ngayDataGridViewTextBoxColumn";
            this.ngayDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // TenNguoiLam
            // 
            this.TenNguoiLam.DataPropertyName = "TenNguoiLam";
            this.TenNguoiLam.HeaderText = "Họ tên người làm";
            this.TenNguoiLam.Name = "TenNguoiLam";
            this.TenNguoiLam.ReadOnly = true;
            this.TenNguoiLam.Width = 200;
            // 
            // mucDichDataGridViewTextBoxColumn
            // 
            this.mucDichDataGridViewTextBoxColumn.DataPropertyName = "MucDich";
            this.mucDichDataGridViewTextBoxColumn.HeaderText = "Mục đích";
            this.mucDichDataGridViewTextBoxColumn.Name = "mucDichDataGridViewTextBoxColumn";
            this.mucDichDataGridViewTextBoxColumn.ReadOnly = true;
            this.mucDichDataGridViewTextBoxColumn.Width = 150;
            // 
            // gioVaoDataGridViewTextBoxColumn
            // 
            this.gioVaoDataGridViewTextBoxColumn.DataPropertyName = "GioVao";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "HH:mm";
            dataGridViewCellStyle3.NullValue = null;
            this.gioVaoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.gioVaoDataGridViewTextBoxColumn.HeaderText = "Giờ vào";
            this.gioVaoDataGridViewTextBoxColumn.Name = "gioVaoDataGridViewTextBoxColumn";
            this.gioVaoDataGridViewTextBoxColumn.ReadOnly = true;
            this.gioVaoDataGridViewTextBoxColumn.Width = 70;
            // 
            // gioRaDataGridViewTextBoxColumn
            // 
            this.gioRaDataGridViewTextBoxColumn.DataPropertyName = "GioRa";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "HH:mm";
            dataGridViewCellStyle4.NullValue = null;
            this.gioRaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.gioRaDataGridViewTextBoxColumn.HeaderText = "Giờ ra";
            this.gioRaDataGridViewTextBoxColumn.Name = "gioRaDataGridViewTextBoxColumn";
            this.gioRaDataGridViewTextBoxColumn.ReadOnly = true;
            this.gioRaDataGridViewTextBoxColumn.Width = 70;
            // 
            // ketQuaDanhGiaDataGridViewTextBoxColumn
            // 
            this.ketQuaDanhGiaDataGridViewTextBoxColumn.DataPropertyName = "KetQuaDanhGia";
            this.ketQuaDanhGiaDataGridViewTextBoxColumn.HeaderText = "Kết quả/Đánh giá";
            this.ketQuaDanhGiaDataGridViewTextBoxColumn.Name = "ketQuaDanhGiaDataGridViewTextBoxColumn";
            this.ketQuaDanhGiaDataGridViewTextBoxColumn.ReadOnly = true;
            this.ketQuaDanhGiaDataGridViewTextBoxColumn.Width = 150;
            // 
            // tenNguoiDeXuatDataGridViewTextBoxColumn
            // 
            this.tenNguoiDeXuatDataGridViewTextBoxColumn.DataPropertyName = "TenNguoiDeXuat";
            this.tenNguoiDeXuatDataGridViewTextBoxColumn.HeaderText = "Người đề xuất";
            this.tenNguoiDeXuatDataGridViewTextBoxColumn.Name = "tenNguoiDeXuatDataGridViewTextBoxColumn";
            this.tenNguoiDeXuatDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenNguoiDeXuatDataGridViewTextBoxColumn.Width = 200;
            // 
            // ghiChuDataGridViewTextBoxColumn
            // 
            this.ghiChuDataGridViewTextBoxColumn.DataPropertyName = "GhiChu";
            this.ghiChuDataGridViewTextBoxColumn.HeaderText = "Ghi chú";
            this.ghiChuDataGridViewTextBoxColumn.Name = "ghiChuDataGridViewTextBoxColumn";
            this.ghiChuDataGridViewTextBoxColumn.ReadOnly = true;
            this.ghiChuDataGridViewTextBoxColumn.Width = 150;
            // 
            // congTacNgoaiGioViewBindingSource
            // 
            this.congTacNgoaiGioViewBindingSource.DataSource = typeof(MM.Databasae.CongTacNgoaiGioView);
            // 
            // _printDialog
            // 
            this._printDialog.UseEXDialog = true;
            // 
            // ctmAction
            // 
            this.ctmAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.toolStripSeparator1,
            this.editToolStripMenuItem,
            this.toolStripSeparator2,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator3,
            this.exportExcelToolStripMenuItem,
            this.toolStripSeparator5,
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator6,
            this.printToolStripMenuItem});
            this.ctmAction.Name = "cmtAction";
            this.ctmAction.Size = new System.Drawing.Size(153, 188);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Image = global::MM.Properties.Resources.add;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addToolStripMenuItem.Text = "Thêm";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::MM.Properties.Resources.edit;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editToolStripMenuItem.Text = "Sửa";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::MM.Properties.Resources.del;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "Xóa";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // exportExcelToolStripMenuItem
            // 
            this.exportExcelToolStripMenuItem.Image = global::MM.Properties.Resources.page_excel_icon;
            this.exportExcelToolStripMenuItem.Name = "exportExcelToolStripMenuItem";
            this.exportExcelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportExcelToolStripMenuItem.Text = "Xuất Excel";
            this.exportExcelToolStripMenuItem.Click += new System.EventHandler(this.exportExcelToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(149, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = global::MM.Properties.Resources.Printer_icon__1_;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.printToolStripMenuItem.Text = "In";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(149, 6);
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Image = global::MM.Properties.Resources.Actions_print_preview_icon;
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.printPreviewToolStripMenuItem.Text = "Xem bản in";
            this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.printPreviewToolStripMenuItem_Click);
            // 
            // uCongTacNgoaiGioList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "uCongTacNgoaiGioList";
            this.Size = new System.Drawing.Size(1058, 495);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCongTacNgoaiGio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.congTacNgoaiGioViewBindingSource)).EndInit();
            this.ctmAction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbKetQuaTimDuoc;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.TextBox txtTenBenhNhan;
        private System.Windows.Forms.DateTimePicker dtpkDenNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpkTuNgay;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgCongTacNgoaiGio;
        private System.Windows.Forms.BindingSource congTacNgoaiGioViewBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.PrintDialog _printDialog;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenNguoiLam;
        private System.Windows.Forms.DataGridViewTextBoxColumn mucDichDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gioVaoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gioRaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ketQuaDanhGiaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenNguoiDeXuatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ghiChuDataGridViewTextBoxColumn;
        protected System.Windows.Forms.ContextMenuStrip ctmAction;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exportExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
    }
}
