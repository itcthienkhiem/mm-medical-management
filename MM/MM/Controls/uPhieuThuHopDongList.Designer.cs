namespace MM.Controls
{
    partial class uPhieuThuHopDongList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtHopDong = new System.Windows.Forms.TextBox();
            this.raHopDong = new System.Windows.Forms.RadioButton();
            this.lbKetQuaTimDuoc = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.raDaXoa = new System.Windows.Forms.RadioButton();
            this.raChuaXoa = new System.Windows.Forms.RadioButton();
            this.raTatCa = new System.Windows.Forms.RadioButton();
            this.btnView = new System.Windows.Forms.Button();
            this.txtTenBenhNhan = new System.Windows.Forms.TextBox();
            this.raTenBenhNhan = new System.Windows.Forms.RadioButton();
            this.dtpkDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpkTuNgay = new System.Windows.Forms.DateTimePicker();
            this.raTuNgayToiNgay = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExportInvoice = new System.Windows.Forms.Button();
            this.btnPrintPreview = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgPhieuThu = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.phieuThuHopDongBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._printDialog = new System.Windows.Forms.PrintDialog();
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.maPhieuThuHopDongDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayThuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenHopDong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenNguoiNopDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenCongTyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diaChiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isExportedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DaThuTien = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.NguoiTao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPhieuThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phieuThuHopDongBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtHopDong);
            this.panel2.Controls.Add(this.raHopDong);
            this.panel2.Controls.Add(this.lbKetQuaTimDuoc);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.btnView);
            this.panel2.Controls.Add(this.txtTenBenhNhan);
            this.panel2.Controls.Add(this.raTenBenhNhan);
            this.panel2.Controls.Add(this.dtpkDenNgay);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.dtpkTuNgay);
            this.panel2.Controls.Add(this.raTuNgayToiNgay);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(813, 131);
            this.panel2.TabIndex = 9;
            // 
            // txtHopDong
            // 
            this.txtHopDong.Location = new System.Drawing.Point(120, 57);
            this.txtHopDong.Name = "txtHopDong";
            this.txtHopDong.ReadOnly = true;
            this.txtHopDong.Size = new System.Drawing.Size(255, 20);
            this.txtHopDong.TabIndex = 7;
            // 
            // raHopDong
            // 
            this.raHopDong.AutoSize = true;
            this.raHopDong.Location = new System.Drawing.Point(16, 57);
            this.raHopDong.Name = "raHopDong";
            this.raHopDong.Size = new System.Drawing.Size(76, 17);
            this.raHopDong.TabIndex = 6;
            this.raHopDong.Text = "Hợp đồng:";
            this.raHopDong.UseVisualStyleBackColor = true;
            this.raHopDong.CheckedChanged += new System.EventHandler(this.raHopDong_CheckedChanged);
            // 
            // lbKetQuaTimDuoc
            // 
            this.lbKetQuaTimDuoc.AutoSize = true;
            this.lbKetQuaTimDuoc.ForeColor = System.Drawing.Color.Blue;
            this.lbKetQuaTimDuoc.Location = new System.Drawing.Point(471, 104);
            this.lbKetQuaTimDuoc.Name = "lbKetQuaTimDuoc";
            this.lbKetQuaTimDuoc.Size = new System.Drawing.Size(100, 13);
            this.lbKetQuaTimDuoc.TabIndex = 16;
            this.lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.raDaXoa);
            this.groupBox1.Controls.Add(this.raChuaXoa);
            this.groupBox1.Controls.Add(this.raTatCa);
            this.groupBox1.Location = new System.Drawing.Point(16, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 43);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // raDaXoa
            // 
            this.raDaXoa.AutoSize = true;
            this.raDaXoa.Location = new System.Drawing.Point(271, 15);
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
            this.raChuaXoa.Location = new System.Drawing.Point(137, 15);
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
            this.raTatCa.Location = new System.Drawing.Point(20, 15);
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
            this.btnView.Location = new System.Drawing.Point(381, 99);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 13;
            this.btnView.Text = "   &Xem";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // txtTenBenhNhan
            // 
            this.txtTenBenhNhan.Location = new System.Drawing.Point(120, 34);
            this.txtTenBenhNhan.Name = "txtTenBenhNhan";
            this.txtTenBenhNhan.ReadOnly = true;
            this.txtTenBenhNhan.Size = new System.Drawing.Size(255, 20);
            this.txtTenBenhNhan.TabIndex = 5;
            // 
            // raTenBenhNhan
            // 
            this.raTenBenhNhan.AutoSize = true;
            this.raTenBenhNhan.Location = new System.Drawing.Point(16, 35);
            this.raTenBenhNhan.Name = "raTenBenhNhan";
            this.raTenBenhNhan.Size = new System.Drawing.Size(107, 17);
            this.raTenBenhNhan.TabIndex = 4;
            this.raTenBenhNhan.Text = "Tên khách hàng:";
            this.raTenBenhNhan.UseVisualStyleBackColor = true;
            this.raTenBenhNhan.CheckedChanged += new System.EventHandler(this.raTenBenhNhan_CheckedChanged);
            // 
            // dtpkDenNgay
            // 
            this.dtpkDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkDenNgay.Location = new System.Drawing.Point(262, 10);
            this.dtpkDenNgay.Name = "dtpkDenNgay";
            this.dtpkDenNgay.Size = new System.Drawing.Size(113, 20);
            this.dtpkDenNgay.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(205, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "đến ngày";
            // 
            // dtpkTuNgay
            // 
            this.dtpkTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkTuNgay.Location = new System.Drawing.Point(84, 10);
            this.dtpkTuNgay.Name = "dtpkTuNgay";
            this.dtpkTuNgay.Size = new System.Drawing.Size(113, 20);
            this.dtpkTuNgay.TabIndex = 1;
            // 
            // raTuNgayToiNgay
            // 
            this.raTuNgayToiNgay.AutoSize = true;
            this.raTuNgayToiNgay.Checked = true;
            this.raTuNgayToiNgay.Location = new System.Drawing.Point(16, 11);
            this.raTuNgayToiNgay.Name = "raTuNgayToiNgay";
            this.raTuNgayToiNgay.Size = new System.Drawing.Size(64, 17);
            this.raTuNgayToiNgay.TabIndex = 0;
            this.raTuNgayToiNgay.TabStop = true;
            this.raTuNgayToiNgay.Text = "Từ ngày";
            this.raTuNgayToiNgay.UseVisualStyleBackColor = true;
            this.raTuNgayToiNgay.CheckedChanged += new System.EventHandler(this.raTuNgayToiNgay_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExportInvoice);
            this.panel1.Controls.Add(this.btnPrintPreview);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 416);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(813, 38);
            this.panel1.TabIndex = 10;
            // 
            // btnExportInvoice
            // 
            this.btnExportInvoice.Image = global::MM.Properties.Resources.invoice_icon;
            this.btnExportInvoice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportInvoice.Location = new System.Drawing.Point(329, 6);
            this.btnExportInvoice.Name = "btnExportInvoice";
            this.btnExportInvoice.Size = new System.Drawing.Size(106, 25);
            this.btnExportInvoice.TabIndex = 17;
            this.btnExportInvoice.Text = "      &Xuất hóa đơn";
            this.btnExportInvoice.UseVisualStyleBackColor = true;
            this.btnExportInvoice.Click += new System.EventHandler(this.btnExportInvoice_Click);
            // 
            // btnPrintPreview
            // 
            this.btnPrintPreview.Image = global::MM.Properties.Resources.Actions_print_preview_icon;
            this.btnPrintPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintPreview.Location = new System.Drawing.Point(164, 6);
            this.btnPrintPreview.Name = "btnPrintPreview";
            this.btnPrintPreview.Size = new System.Drawing.Size(93, 25);
            this.btnPrintPreview.TabIndex = 5;
            this.btnPrintPreview.Text = "      &Xem bản in";
            this.btnPrintPreview.UseVisualStyleBackColor = true;
            this.btnPrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = global::MM.Properties.Resources.Printer_icon__1_;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(261, 6);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(64, 25);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "   &In";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::MM.Properties.Resources.del;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(85, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "    &Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::MM.Properties.Resources.add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(6, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 25);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "    &Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkChecked);
            this.panel3.Controls.Add(this.dgPhieuThu);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 131);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(813, 285);
            this.panel3.TabIndex = 11;
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(44, 5);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 9;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgPhieuThu
            // 
            this.dgPhieuThu.AllowUserToAddRows = false;
            this.dgPhieuThu.AllowUserToDeleteRows = false;
            this.dgPhieuThu.AllowUserToOrderColumns = true;
            this.dgPhieuThu.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPhieuThu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPhieuThu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPhieuThu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.maPhieuThuHopDongDataGridViewTextBoxColumn,
            this.ngayThuDataGridViewTextBoxColumn,
            this.TenHopDong,
            this.tenNguoiNopDataGridViewTextBoxColumn,
            this.tenCongTyDataGridViewTextBoxColumn,
            this.diaChiDataGridViewTextBoxColumn,
            this.notesDataGridViewTextBoxColumn,
            this.isExportedDataGridViewCheckBoxColumn,
            this.DaThuTien,
            this.NguoiTao});
            this.dgPhieuThu.DataSource = this.phieuThuHopDongBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPhieuThu.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgPhieuThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPhieuThu.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgPhieuThu.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgPhieuThu.HighlightSelectedColumnHeaders = false;
            this.dgPhieuThu.Location = new System.Drawing.Point(0, 0);
            this.dgPhieuThu.MultiSelect = false;
            this.dgPhieuThu.Name = "dgPhieuThu";
            this.dgPhieuThu.RowHeadersWidth = 30;
            this.dgPhieuThu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPhieuThu.Size = new System.Drawing.Size(813, 285);
            this.dgPhieuThu.TabIndex = 8;
            this.dgPhieuThu.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgPhieuThu_ColumnHeaderMouseClick);
            this.dgPhieuThu.DoubleClick += new System.EventHandler(this.dgPhieuThu_DoubleClick);
            // 
            // phieuThuHopDongBindingSource
            // 
            this.phieuThuHopDongBindingSource.DataSource = typeof(MM.Databasae.PhieuThuHopDong);
            // 
            // _printDialog
            // 
            this._printDialog.UseEXDialog = true;
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
            // maPhieuThuHopDongDataGridViewTextBoxColumn
            // 
            this.maPhieuThuHopDongDataGridViewTextBoxColumn.DataPropertyName = "MaPhieuThuHopDong";
            this.maPhieuThuHopDongDataGridViewTextBoxColumn.HeaderText = "Mã phiếu thu";
            this.maPhieuThuHopDongDataGridViewTextBoxColumn.Name = "maPhieuThuHopDongDataGridViewTextBoxColumn";
            this.maPhieuThuHopDongDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ngayThuDataGridViewTextBoxColumn
            // 
            this.ngayThuDataGridViewTextBoxColumn.DataPropertyName = "NgayThu";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            dataGridViewCellStyle2.NullValue = null;
            this.ngayThuDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.ngayThuDataGridViewTextBoxColumn.HeaderText = "Ngày thu";
            this.ngayThuDataGridViewTextBoxColumn.Name = "ngayThuDataGridViewTextBoxColumn";
            this.ngayThuDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // TenHopDong
            // 
            this.TenHopDong.DataPropertyName = "ContractName";
            this.TenHopDong.HeaderText = "Tên hợp đồng";
            this.TenHopDong.Name = "TenHopDong";
            this.TenHopDong.ReadOnly = true;
            this.TenHopDong.Width = 200;
            // 
            // tenNguoiNopDataGridViewTextBoxColumn
            // 
            this.tenNguoiNopDataGridViewTextBoxColumn.DataPropertyName = "TenNguoiNop";
            this.tenNguoiNopDataGridViewTextBoxColumn.HeaderText = "Tên khách hàng";
            this.tenNguoiNopDataGridViewTextBoxColumn.Name = "tenNguoiNopDataGridViewTextBoxColumn";
            this.tenNguoiNopDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenNguoiNopDataGridViewTextBoxColumn.Width = 200;
            // 
            // tenCongTyDataGridViewTextBoxColumn
            // 
            this.tenCongTyDataGridViewTextBoxColumn.DataPropertyName = "TenCongTy";
            this.tenCongTyDataGridViewTextBoxColumn.HeaderText = "Công ty";
            this.tenCongTyDataGridViewTextBoxColumn.Name = "tenCongTyDataGridViewTextBoxColumn";
            this.tenCongTyDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenCongTyDataGridViewTextBoxColumn.Width = 200;
            // 
            // diaChiDataGridViewTextBoxColumn
            // 
            this.diaChiDataGridViewTextBoxColumn.DataPropertyName = "DiaChi";
            this.diaChiDataGridViewTextBoxColumn.HeaderText = "Địa chỉ";
            this.diaChiDataGridViewTextBoxColumn.Name = "diaChiDataGridViewTextBoxColumn";
            this.diaChiDataGridViewTextBoxColumn.ReadOnly = true;
            this.diaChiDataGridViewTextBoxColumn.Width = 300;
            // 
            // notesDataGridViewTextBoxColumn
            // 
            this.notesDataGridViewTextBoxColumn.DataPropertyName = "Notes";
            this.notesDataGridViewTextBoxColumn.HeaderText = "Ghi chú";
            this.notesDataGridViewTextBoxColumn.Name = "notesDataGridViewTextBoxColumn";
            this.notesDataGridViewTextBoxColumn.ReadOnly = true;
            this.notesDataGridViewTextBoxColumn.Width = 250;
            // 
            // isExportedDataGridViewCheckBoxColumn
            // 
            this.isExportedDataGridViewCheckBoxColumn.DataPropertyName = "IsExported";
            this.isExportedDataGridViewCheckBoxColumn.HeaderText = "Đã xuất HĐ";
            this.isExportedDataGridViewCheckBoxColumn.Name = "isExportedDataGridViewCheckBoxColumn";
            this.isExportedDataGridViewCheckBoxColumn.ReadOnly = true;
            this.isExportedDataGridViewCheckBoxColumn.Width = 80;
            // 
            // DaThuTien
            // 
            this.DaThuTien.DataPropertyName = "DaThuTien";
            this.DaThuTien.HeaderText = "Đã thu tiền";
            this.DaThuTien.Name = "DaThuTien";
            this.DaThuTien.ReadOnly = true;
            this.DaThuTien.Width = 80;
            // 
            // NguoiTao
            // 
            this.NguoiTao.DataPropertyName = "NguoiTao";
            this.NguoiTao.HeaderText = "Người tạo";
            this.NguoiTao.Name = "NguoiTao";
            this.NguoiTao.ReadOnly = true;
            this.NguoiTao.Width = 200;
            // 
            // uPhieuThuHopDongList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "uPhieuThuHopDongList";
            this.Size = new System.Drawing.Size(813, 454);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPhieuThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phieuThuHopDongBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton raDaXoa;
        private System.Windows.Forms.RadioButton raChuaXoa;
        private System.Windows.Forms.RadioButton raTatCa;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.TextBox txtTenBenhNhan;
        private System.Windows.Forms.RadioButton raTenBenhNhan;
        private System.Windows.Forms.DateTimePicker dtpkDenNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpkTuNgay;
        private System.Windows.Forms.RadioButton raTuNgayToiNgay;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExportInvoice;
        private System.Windows.Forms.Button btnPrintPreview;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgPhieuThu;
        private System.Windows.Forms.BindingSource phieuThuHopDongBindingSource;
        private System.Windows.Forms.PrintDialog _printDialog;
        private System.Windows.Forms.Label lbKetQuaTimDuoc;
        private System.Windows.Forms.TextBox txtHopDong;
        private System.Windows.Forms.RadioButton raHopDong;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn maPhieuThuHopDongDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayThuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenHopDong;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenNguoiNopDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenCongTyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diaChiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn notesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isExportedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DaThuTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn NguoiTao;
    }
}
