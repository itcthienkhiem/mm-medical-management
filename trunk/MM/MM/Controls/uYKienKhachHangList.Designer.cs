﻿namespace MM.Controls
{
    partial class uYKienKhachHangList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkTuNgay = new System.Windows.Forms.CheckBox();
            this.cboDocStaff = new System.Windows.Forms.ComboBox();
            this.docStaffViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.raBacSiPhuTrach = new System.Windows.Forms.RadioButton();
            this.txtTenNguoiTao = new System.Windows.Forms.TextBox();
            this.raTenNguoiTao = new System.Windows.Forms.RadioButton();
            this.btnView = new System.Windows.Forms.Button();
            this.txtTenBenhNhan = new System.Windows.Forms.TextBox();
            this.raTenBenhNhan = new System.Windows.Forms.RadioButton();
            this.dtpkDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpkTuNgay = new System.Windows.Forms.DateTimePicker();
            this.chkDenNgay = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgYKienKhachHang = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.contactDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenKhachHangDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soDienThoaiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diaChiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yeuCauDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KetLuan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NguoiTao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BacSiPhuTrach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DaXong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nguonDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NguoiKetLuan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NguoiCapNhat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yKienKhachHangBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._printDialog = new System.Windows.Forms.PrintDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgYKienKhachHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yKienKhachHangBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkTuNgay);
            this.panel1.Controls.Add(this.cboDocStaff);
            this.panel1.Controls.Add(this.raBacSiPhuTrach);
            this.panel1.Controls.Add(this.txtTenNguoiTao);
            this.panel1.Controls.Add(this.raTenNguoiTao);
            this.panel1.Controls.Add(this.btnView);
            this.panel1.Controls.Add(this.txtTenBenhNhan);
            this.panel1.Controls.Add(this.raTenBenhNhan);
            this.panel1.Controls.Add(this.dtpkDenNgay);
            this.panel1.Controls.Add(this.dtpkTuNgay);
            this.panel1.Controls.Add(this.chkDenNgay);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1188, 106);
            this.panel1.TabIndex = 0;
            // 
            // chkTuNgay
            // 
            this.chkTuNgay.AutoSize = true;
            this.chkTuNgay.Location = new System.Drawing.Point(12, 10);
            this.chkTuNgay.Name = "chkTuNgay";
            this.chkTuNgay.Size = new System.Drawing.Size(65, 17);
            this.chkTuNgay.TabIndex = 1;
            this.chkTuNgay.Text = "Từ ngày";
            this.chkTuNgay.UseVisualStyleBackColor = true;
            this.chkTuNgay.CheckedChanged += new System.EventHandler(this.chkTuNgay_CheckedChanged);
            // 
            // cboDocStaff
            // 
            this.cboDocStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDocStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDocStaff.DataSource = this.docStaffViewBindingSource;
            this.cboDocStaff.DisplayMember = "Fullname";
            this.cboDocStaff.Enabled = false;
            this.cboDocStaff.FormattingEnabled = true;
            this.cboDocStaff.Location = new System.Drawing.Point(119, 78);
            this.cboDocStaff.Name = "cboDocStaff";
            this.cboDocStaff.Size = new System.Drawing.Size(277, 21);
            this.cboDocStaff.TabIndex = 13;
            this.cboDocStaff.ValueMember = "DocStaffGUID";
            this.cboDocStaff.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboDocStaff_KeyUp);
            // 
            // docStaffViewBindingSource
            // 
            this.docStaffViewBindingSource.DataSource = typeof(MM.Databasae.DocStaffView);
            // 
            // raBacSiPhuTrach
            // 
            this.raBacSiPhuTrach.AutoSize = true;
            this.raBacSiPhuTrach.Location = new System.Drawing.Point(11, 79);
            this.raBacSiPhuTrach.Name = "raBacSiPhuTrach";
            this.raBacSiPhuTrach.Size = new System.Drawing.Size(105, 17);
            this.raBacSiPhuTrach.TabIndex = 9;
            this.raBacSiPhuTrach.Text = "Bác sĩ phụ trách";
            this.raBacSiPhuTrach.UseVisualStyleBackColor = true;
            this.raBacSiPhuTrach.CheckedChanged += new System.EventHandler(this.raBacSiPhuTrach_CheckedChanged);
            // 
            // txtTenNguoiTao
            // 
            this.txtTenNguoiTao.Location = new System.Drawing.Point(119, 55);
            this.txtTenNguoiTao.Name = "txtTenNguoiTao";
            this.txtTenNguoiTao.ReadOnly = true;
            this.txtTenNguoiTao.Size = new System.Drawing.Size(277, 20);
            this.txtTenNguoiTao.TabIndex = 8;
            this.txtTenNguoiTao.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTenNguoiTao_KeyUp);
            // 
            // raTenNguoiTao
            // 
            this.raTenNguoiTao.AutoSize = true;
            this.raTenNguoiTao.Location = new System.Drawing.Point(11, 56);
            this.raTenNguoiTao.Name = "raTenNguoiTao";
            this.raTenNguoiTao.Size = new System.Drawing.Size(91, 17);
            this.raTenNguoiTao.TabIndex = 7;
            this.raTenNguoiTao.Text = "Tên người tạo";
            this.raTenNguoiTao.UseVisualStyleBackColor = true;
            this.raTenNguoiTao.CheckedChanged += new System.EventHandler(this.raTenNguoiTao_CheckedChanged);
            // 
            // btnView
            // 
            this.btnView.Image = global::MM.Properties.Resources.views_icon;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(401, 77);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 11;
            this.btnView.Text = "   &Xem";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // txtTenBenhNhan
            // 
            this.txtTenBenhNhan.Location = new System.Drawing.Point(119, 32);
            this.txtTenBenhNhan.Name = "txtTenBenhNhan";
            this.txtTenBenhNhan.Size = new System.Drawing.Size(277, 20);
            this.txtTenBenhNhan.TabIndex = 6;
            this.txtTenBenhNhan.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTenBenhNhan_KeyUp);
            // 
            // raTenBenhNhan
            // 
            this.raTenBenhNhan.AutoSize = true;
            this.raTenBenhNhan.Checked = true;
            this.raTenBenhNhan.Location = new System.Drawing.Point(11, 33);
            this.raTenBenhNhan.Name = "raTenBenhNhan";
            this.raTenBenhNhan.Size = new System.Drawing.Size(104, 17);
            this.raTenBenhNhan.TabIndex = 5;
            this.raTenBenhNhan.TabStop = true;
            this.raTenBenhNhan.Text = "Tên khách hàng";
            this.raTenBenhNhan.UseVisualStyleBackColor = true;
            this.raTenBenhNhan.CheckedChanged += new System.EventHandler(this.raTenBenhNhan_CheckedChanged);
            // 
            // dtpkDenNgay
            // 
            this.dtpkDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkDenNgay.Enabled = false;
            this.dtpkDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkDenNgay.Location = new System.Drawing.Point(283, 8);
            this.dtpkDenNgay.Name = "dtpkDenNgay";
            this.dtpkDenNgay.Size = new System.Drawing.Size(113, 20);
            this.dtpkDenNgay.TabIndex = 4;
            // 
            // dtpkTuNgay
            // 
            this.dtpkTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkTuNgay.Enabled = false;
            this.dtpkTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkTuNgay.Location = new System.Drawing.Point(80, 8);
            this.dtpkTuNgay.Name = "dtpkTuNgay";
            this.dtpkTuNgay.Size = new System.Drawing.Size(117, 20);
            this.dtpkTuNgay.TabIndex = 2;
            // 
            // chkDenNgay
            // 
            this.chkDenNgay.AutoSize = true;
            this.chkDenNgay.Enabled = false;
            this.chkDenNgay.Location = new System.Drawing.Point(215, 11);
            this.chkDenNgay.Name = "chkDenNgay";
            this.chkDenNgay.Size = new System.Drawing.Size(71, 17);
            this.chkDenNgay.TabIndex = 3;
            this.chkDenNgay.Text = "đến ngày";
            this.chkDenNgay.UseVisualStyleBackColor = true;
            this.chkDenNgay.CheckedChanged += new System.EventHandler(this.chkDenNgay_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExportExcel);
            this.panel2.Controls.Add(this.btnPrintPreview);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 575);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1188, 38);
            this.panel2.TabIndex = 1;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Image = global::MM.Properties.Resources.page_excel_icon;
            this.btnExportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExcel.Location = new System.Drawing.Point(408, 6);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(93, 25);
            this.btnExportExcel.TabIndex = 79;
            this.btnExportExcel.Text = "      &Xuất Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Image = global::MM.Properties.Resources.Actions_print_preview_icon;
            this.btnPrintPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintPreview.Location = new System.Drawing.Point(243, 6);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(93, 25);
            this.btnPrintPreview.TabIndex = 77;
            this.btnPrintPreview.Text = "      &Xem bản in";
            this.btnPrintPreview.UseVisualStyleBackColor = true;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = global::MM.Properties.Resources.Printer_icon__1_;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(340, 6);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(64, 25);
            this.btnPrint.TabIndex = 78;
            this.btnPrint.Text = "   &In";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::MM.Properties.Resources.del;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(164, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "    &Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::MM.Properties.Resources.edit;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(85, 6);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 25);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "    &Sửa";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::MM.Properties.Resources.add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(6, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 25);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "    &Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkChecked);
            this.panel3.Controls.Add(this.dgYKienKhachHang);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 106);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1188, 469);
            this.panel3.TabIndex = 2;
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(44, 5);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 3;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgYKienKhachHang
            // 
            this.dgYKienKhachHang.AllowUserToAddRows = false;
            this.dgYKienKhachHang.AllowUserToDeleteRows = false;
            this.dgYKienKhachHang.AllowUserToOrderColumns = true;
            this.dgYKienKhachHang.AutoGenerateColumns = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgYKienKhachHang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgYKienKhachHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgYKienKhachHang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.contactDateDataGridViewTextBoxColumn,
            this.tenKhachHangDataGridViewTextBoxColumn,
            this.soDienThoaiDataGridViewTextBoxColumn,
            this.diaChiDataGridViewTextBoxColumn,
            this.yeuCauDataGridViewTextBoxColumn,
            this.KetLuan,
            this.NguoiTao,
            this.BacSiPhuTrach,
            this.DaXong,
            this.nguonDataGridViewTextBoxColumn,
            this.NguoiKetLuan,
            this.NguoiCapNhat});
            this.dgYKienKhachHang.DataSource = this.yKienKhachHangBindingSource;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgYKienKhachHang.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgYKienKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgYKienKhachHang.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgYKienKhachHang.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgYKienKhachHang.HighlightSelectedColumnHeaders = false;
            this.dgYKienKhachHang.Location = new System.Drawing.Point(0, 0);
            this.dgYKienKhachHang.MultiSelect = false;
            this.dgYKienKhachHang.Name = "dgYKienKhachHang";
            this.dgYKienKhachHang.RowHeadersWidth = 30;
            this.dgYKienKhachHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgYKienKhachHang.Size = new System.Drawing.Size(1188, 469);
            this.dgYKienKhachHang.TabIndex = 2;
            this.dgYKienKhachHang.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgYKienKhachHang_CellMouseUp);
            this.dgYKienKhachHang.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgYKienKhachHang_ColumnHeaderMouseClick);
            this.dgYKienKhachHang.DoubleClick += new System.EventHandler(this.dgYKienKhachHang_DoubleClick);
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
            // contactDateDataGridViewTextBoxColumn
            // 
            this.contactDateDataGridViewTextBoxColumn.DataPropertyName = "ContactDate";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle6.NullValue = null;
            this.contactDateDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.contactDateDataGridViewTextBoxColumn.HeaderText = "Ngày liên hệ";
            this.contactDateDataGridViewTextBoxColumn.Name = "contactDateDataGridViewTextBoxColumn";
            this.contactDateDataGridViewTextBoxColumn.ReadOnly = true;
            this.contactDateDataGridViewTextBoxColumn.Width = 120;
            // 
            // tenKhachHangDataGridViewTextBoxColumn
            // 
            this.tenKhachHangDataGridViewTextBoxColumn.DataPropertyName = "TenKhachHang";
            this.tenKhachHangDataGridViewTextBoxColumn.HeaderText = "Tên khách hàng";
            this.tenKhachHangDataGridViewTextBoxColumn.Name = "tenKhachHangDataGridViewTextBoxColumn";
            this.tenKhachHangDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenKhachHangDataGridViewTextBoxColumn.Width = 200;
            // 
            // soDienThoaiDataGridViewTextBoxColumn
            // 
            this.soDienThoaiDataGridViewTextBoxColumn.DataPropertyName = "SoDienThoai";
            this.soDienThoaiDataGridViewTextBoxColumn.HeaderText = "Số điện thoại";
            this.soDienThoaiDataGridViewTextBoxColumn.Name = "soDienThoaiDataGridViewTextBoxColumn";
            this.soDienThoaiDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // diaChiDataGridViewTextBoxColumn
            // 
            this.diaChiDataGridViewTextBoxColumn.DataPropertyName = "DiaChi";
            this.diaChiDataGridViewTextBoxColumn.HeaderText = "Địa chỉ";
            this.diaChiDataGridViewTextBoxColumn.Name = "diaChiDataGridViewTextBoxColumn";
            this.diaChiDataGridViewTextBoxColumn.ReadOnly = true;
            this.diaChiDataGridViewTextBoxColumn.Width = 300;
            // 
            // yeuCauDataGridViewTextBoxColumn
            // 
            this.yeuCauDataGridViewTextBoxColumn.DataPropertyName = "YeuCau";
            this.yeuCauDataGridViewTextBoxColumn.HeaderText = "Yêu cầu từ khách hàng";
            this.yeuCauDataGridViewTextBoxColumn.Name = "yeuCauDataGridViewTextBoxColumn";
            this.yeuCauDataGridViewTextBoxColumn.ReadOnly = true;
            this.yeuCauDataGridViewTextBoxColumn.Width = 250;
            // 
            // KetLuan
            // 
            this.KetLuan.DataPropertyName = "KetLuan";
            this.KetLuan.HeaderText = "Hướng giải quyết";
            this.KetLuan.Name = "KetLuan";
            this.KetLuan.ReadOnly = true;
            this.KetLuan.Width = 250;
            // 
            // NguoiTao
            // 
            this.NguoiTao.DataPropertyName = "NguoiTao";
            this.NguoiTao.HeaderText = "Điều dưỡng phụ trách";
            this.NguoiTao.Name = "NguoiTao";
            this.NguoiTao.ReadOnly = true;
            this.NguoiTao.Width = 200;
            // 
            // BacSiPhuTrach
            // 
            this.BacSiPhuTrach.DataPropertyName = "BacSiPhuTrach";
            this.BacSiPhuTrach.HeaderText = "Bác sĩ phụ trách";
            this.BacSiPhuTrach.Name = "BacSiPhuTrach";
            this.BacSiPhuTrach.ReadOnly = true;
            this.BacSiPhuTrach.Width = 200;
            // 
            // DaXong
            // 
            this.DaXong.DataPropertyName = "DaXongStr";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DaXong.DefaultCellStyle = dataGridViewCellStyle7;
            this.DaXong.HeaderText = "Trạng thái";
            this.DaXong.Name = "DaXong";
            this.DaXong.ReadOnly = true;
            this.DaXong.Width = 80;
            // 
            // nguonDataGridViewTextBoxColumn
            // 
            this.nguonDataGridViewTextBoxColumn.DataPropertyName = "Nguon";
            this.nguonDataGridViewTextBoxColumn.HeaderText = "Nguồn";
            this.nguonDataGridViewTextBoxColumn.Name = "nguonDataGridViewTextBoxColumn";
            this.nguonDataGridViewTextBoxColumn.ReadOnly = true;
            this.nguonDataGridViewTextBoxColumn.Width = 200;
            // 
            // NguoiKetLuan
            // 
            this.NguoiKetLuan.DataPropertyName = "TenNguoiKetLuan";
            this.NguoiKetLuan.HeaderText = "Người kết luận";
            this.NguoiKetLuan.Name = "NguoiKetLuan";
            this.NguoiKetLuan.ReadOnly = true;
            this.NguoiKetLuan.Visible = false;
            this.NguoiKetLuan.Width = 200;
            // 
            // NguoiCapNhat
            // 
            this.NguoiCapNhat.DataPropertyName = "NguoiCapNhat";
            this.NguoiCapNhat.HeaderText = "Người cập nhật";
            this.NguoiCapNhat.Name = "NguoiCapNhat";
            this.NguoiCapNhat.ReadOnly = true;
            this.NguoiCapNhat.Visible = false;
            this.NguoiCapNhat.Width = 200;
            // 
            // yKienKhachHangBindingSource
            // 
            this.yKienKhachHangBindingSource.DataSource = typeof(MM.Databasae.YKienKhachHang);
            // 
            // _printDialog
            // 
            this._printDialog.AllowCurrentPage = true;
            this._printDialog.AllowSelection = true;
            this._printDialog.AllowSomePages = true;
            this._printDialog.ShowHelp = true;
            this._printDialog.UseEXDialog = true;
            // 
            // uYKienKhachHangList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uYKienKhachHangList";
            this.Size = new System.Drawing.Size(1188, 613);
            this.Load += new System.EventHandler(this.uYKienKhachHangList_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgYKienKhachHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yKienKhachHangBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtTenBenhNhan;
        private System.Windows.Forms.RadioButton raTenBenhNhan;
        private System.Windows.Forms.DateTimePicker dtpkDenNgay;
        private System.Windows.Forms.DateTimePicker dtpkTuNgay;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgYKienKhachHang;
        private System.Windows.Forms.BindingSource yKienKhachHangBindingSource;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.PrintDialog _printDialog;
        private System.Windows.Forms.TextBox txtTenNguoiTao;
        private System.Windows.Forms.RadioButton raTenNguoiTao;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn contactDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenKhachHangDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soDienThoaiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diaChiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn yeuCauDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn KetLuan;
        private System.Windows.Forms.DataGridViewTextBoxColumn NguoiTao;
        private System.Windows.Forms.DataGridViewTextBoxColumn BacSiPhuTrach;
        private System.Windows.Forms.DataGridViewTextBoxColumn DaXong;
        private System.Windows.Forms.DataGridViewTextBoxColumn nguonDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NguoiKetLuan;
        private System.Windows.Forms.DataGridViewTextBoxColumn NguoiCapNhat;
        private System.Windows.Forms.RadioButton raBacSiPhuTrach;
        private System.Windows.Forms.BindingSource docStaffViewBindingSource;
        private System.Windows.Forms.ComboBox cboDocStaff;
        private System.Windows.Forms.CheckBox chkTuNgay;
        private System.Windows.Forms.CheckBox chkDenNgay;
    }
}
