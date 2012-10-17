namespace MM.Controls
{
    partial class uLoThuocList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgLoThuoc = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.maLoThuocDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenLoThuocDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenThuocDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngaySanXuatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayHetHanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soLuongNhapDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.giaNhapDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.donViTinhNhapDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TongTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.donViTinhQuiDoiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soLuongQuiDoiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GiaNhapQuiDoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soLuongXuatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soDangKyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hangSanXuatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nhaPhanPhoiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loThuocViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtpkDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpkTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTenThuoc = new System.Windows.Forms.TextBox();
            this.raTuNgayDenNgay = new System.Windows.Forms.RadioButton();
            this.raTenThuoc = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLoThuoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loThuocViewBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 400);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(973, 38);
            this.panel1.TabIndex = 2;
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::MM.Properties.Resources.del;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(164, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 2;
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
            this.btnEdit.TabIndex = 1;
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
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "    &Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(45, 5);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 5;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgLoThuoc
            // 
            this.dgLoThuoc.AllowUserToAddRows = false;
            this.dgLoThuoc.AllowUserToDeleteRows = false;
            this.dgLoThuoc.AllowUserToOrderColumns = true;
            this.dgLoThuoc.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgLoThuoc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgLoThuoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLoThuoc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.maLoThuocDataGridViewTextBoxColumn,
            this.tenLoThuocDataGridViewTextBoxColumn,
            this.tenThuocDataGridViewTextBoxColumn,
            this.ngaySanXuatDataGridViewTextBoxColumn,
            this.ngayHetHanDataGridViewTextBoxColumn,
            this.soLuongNhapDataGridViewTextBoxColumn,
            this.giaNhapDataGridViewTextBoxColumn,
            this.donViTinhNhapDataGridViewTextBoxColumn,
            this.TongTien,
            this.donViTinhQuiDoiDataGridViewTextBoxColumn,
            this.soLuongQuiDoiDataGridViewTextBoxColumn,
            this.GiaNhapQuiDoi,
            this.soLuongXuatDataGridViewTextBoxColumn,
            this.soDangKyDataGridViewTextBoxColumn,
            this.hangSanXuatDataGridViewTextBoxColumn,
            this.nhaPhanPhoiDataGridViewTextBoxColumn,
            this.CreatedDate});
            this.dgLoThuoc.DataSource = this.loThuocViewBindingSource;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgLoThuoc.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgLoThuoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgLoThuoc.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgLoThuoc.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgLoThuoc.HighlightSelectedColumnHeaders = false;
            this.dgLoThuoc.Location = new System.Drawing.Point(0, 0);
            this.dgLoThuoc.MultiSelect = false;
            this.dgLoThuoc.Name = "dgLoThuoc";
            this.dgLoThuoc.ReadOnly = true;
            this.dgLoThuoc.RowHeadersWidth = 30;
            this.dgLoThuoc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgLoThuoc.Size = new System.Drawing.Size(973, 336);
            this.dgLoThuoc.TabIndex = 4;
            this.dgLoThuoc.DoubleClick += new System.EventHandler(this.dgLoThuoc_DoubleClick);
            // 
            // colChecked
            // 
            this.colChecked.Checked = true;
            this.colChecked.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.colChecked.CheckValue = "N";
            this.colChecked.DataPropertyName = "Checked";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colChecked.DefaultCellStyle = dataGridViewCellStyle2;
            this.colChecked.Frozen = true;
            this.colChecked.HeaderText = "";
            this.colChecked.Name = "colChecked";
            this.colChecked.ReadOnly = true;
            this.colChecked.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colChecked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colChecked.Width = 40;
            // 
            // maLoThuocDataGridViewTextBoxColumn
            // 
            this.maLoThuocDataGridViewTextBoxColumn.DataPropertyName = "MaLoThuoc";
            this.maLoThuocDataGridViewTextBoxColumn.HeaderText = "Mã lô thuốc";
            this.maLoThuocDataGridViewTextBoxColumn.Name = "maLoThuocDataGridViewTextBoxColumn";
            this.maLoThuocDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tenLoThuocDataGridViewTextBoxColumn
            // 
            this.tenLoThuocDataGridViewTextBoxColumn.DataPropertyName = "TenLoThuoc";
            this.tenLoThuocDataGridViewTextBoxColumn.HeaderText = "Tên lô thuốc";
            this.tenLoThuocDataGridViewTextBoxColumn.Name = "tenLoThuocDataGridViewTextBoxColumn";
            this.tenLoThuocDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenLoThuocDataGridViewTextBoxColumn.Width = 150;
            // 
            // tenThuocDataGridViewTextBoxColumn
            // 
            this.tenThuocDataGridViewTextBoxColumn.DataPropertyName = "TenThuoc";
            this.tenThuocDataGridViewTextBoxColumn.HeaderText = "Tên thuốc";
            this.tenThuocDataGridViewTextBoxColumn.Name = "tenThuocDataGridViewTextBoxColumn";
            this.tenThuocDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenThuocDataGridViewTextBoxColumn.Width = 150;
            // 
            // ngaySanXuatDataGridViewTextBoxColumn
            // 
            this.ngaySanXuatDataGridViewTextBoxColumn.DataPropertyName = "NgaySanXuat";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "dd/MM/yyyy";
            dataGridViewCellStyle3.NullValue = null;
            this.ngaySanXuatDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.ngaySanXuatDataGridViewTextBoxColumn.HeaderText = "Ngày sản xuất";
            this.ngaySanXuatDataGridViewTextBoxColumn.Name = "ngaySanXuatDataGridViewTextBoxColumn";
            this.ngaySanXuatDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ngayHetHanDataGridViewTextBoxColumn
            // 
            this.ngayHetHanDataGridViewTextBoxColumn.DataPropertyName = "NgayHetHan";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "dd/MM/yyyy";
            dataGridViewCellStyle4.NullValue = null;
            this.ngayHetHanDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.ngayHetHanDataGridViewTextBoxColumn.HeaderText = "Ngày hết hạn";
            this.ngayHetHanDataGridViewTextBoxColumn.Name = "ngayHetHanDataGridViewTextBoxColumn";
            this.ngayHetHanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // soLuongNhapDataGridViewTextBoxColumn
            // 
            this.soLuongNhapDataGridViewTextBoxColumn.DataPropertyName = "SoLuongNhap";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = null;
            this.soLuongNhapDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.soLuongNhapDataGridViewTextBoxColumn.HeaderText = "Số lượng nhập";
            this.soLuongNhapDataGridViewTextBoxColumn.Name = "soLuongNhapDataGridViewTextBoxColumn";
            this.soLuongNhapDataGridViewTextBoxColumn.ReadOnly = true;
            this.soLuongNhapDataGridViewTextBoxColumn.Width = 110;
            // 
            // giaNhapDataGridViewTextBoxColumn
            // 
            this.giaNhapDataGridViewTextBoxColumn.DataPropertyName = "GiaNhap";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewCellStyle6.NullValue = null;
            this.giaNhapDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.giaNhapDataGridViewTextBoxColumn.HeaderText = "Giá nhập (VNĐ)";
            this.giaNhapDataGridViewTextBoxColumn.Name = "giaNhapDataGridViewTextBoxColumn";
            this.giaNhapDataGridViewTextBoxColumn.ReadOnly = true;
            this.giaNhapDataGridViewTextBoxColumn.Width = 110;
            // 
            // donViTinhNhapDataGridViewTextBoxColumn
            // 
            this.donViTinhNhapDataGridViewTextBoxColumn.DataPropertyName = "DonViTinhNhap";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.donViTinhNhapDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.donViTinhNhapDataGridViewTextBoxColumn.HeaderText = "ĐVT nhập";
            this.donViTinhNhapDataGridViewTextBoxColumn.Name = "donViTinhNhapDataGridViewTextBoxColumn";
            this.donViTinhNhapDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // TongTien
            // 
            this.TongTien.DataPropertyName = "TongTien";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N0";
            dataGridViewCellStyle8.NullValue = null;
            this.TongTien.DefaultCellStyle = dataGridViewCellStyle8;
            this.TongTien.HeaderText = "Tổng tiền";
            this.TongTien.Name = "TongTien";
            this.TongTien.ReadOnly = true;
            // 
            // donViTinhQuiDoiDataGridViewTextBoxColumn
            // 
            this.donViTinhQuiDoiDataGridViewTextBoxColumn.DataPropertyName = "DonViTinhQuiDoi";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.donViTinhQuiDoiDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.donViTinhQuiDoiDataGridViewTextBoxColumn.HeaderText = "ĐVT qui đổi";
            this.donViTinhQuiDoiDataGridViewTextBoxColumn.Name = "donViTinhQuiDoiDataGridViewTextBoxColumn";
            this.donViTinhQuiDoiDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // soLuongQuiDoiDataGridViewTextBoxColumn
            // 
            this.soLuongQuiDoiDataGridViewTextBoxColumn.DataPropertyName = "SoLuongQuiDoi";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "N0";
            dataGridViewCellStyle10.NullValue = null;
            this.soLuongQuiDoiDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.soLuongQuiDoiDataGridViewTextBoxColumn.HeaderText = "Số lượng qui đổi";
            this.soLuongQuiDoiDataGridViewTextBoxColumn.Name = "soLuongQuiDoiDataGridViewTextBoxColumn";
            this.soLuongQuiDoiDataGridViewTextBoxColumn.ReadOnly = true;
            this.soLuongQuiDoiDataGridViewTextBoxColumn.Width = 110;
            // 
            // GiaNhapQuiDoi
            // 
            this.GiaNhapQuiDoi.DataPropertyName = "GiaNhapQuiDoi";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "N0";
            dataGridViewCellStyle11.NullValue = null;
            this.GiaNhapQuiDoi.DefaultCellStyle = dataGridViewCellStyle11;
            this.GiaNhapQuiDoi.HeaderText = "Giá nhập qui đổi (VNĐ)";
            this.GiaNhapQuiDoi.Name = "GiaNhapQuiDoi";
            this.GiaNhapQuiDoi.ReadOnly = true;
            this.GiaNhapQuiDoi.Width = 150;
            // 
            // soLuongXuatDataGridViewTextBoxColumn
            // 
            this.soLuongXuatDataGridViewTextBoxColumn.DataPropertyName = "SoLuongXuat";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N0";
            dataGridViewCellStyle12.NullValue = null;
            this.soLuongXuatDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle12;
            this.soLuongXuatDataGridViewTextBoxColumn.HeaderText = "Số lượng xuất";
            this.soLuongXuatDataGridViewTextBoxColumn.Name = "soLuongXuatDataGridViewTextBoxColumn";
            this.soLuongXuatDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // soDangKyDataGridViewTextBoxColumn
            // 
            this.soDangKyDataGridViewTextBoxColumn.DataPropertyName = "SoDangKy";
            this.soDangKyDataGridViewTextBoxColumn.HeaderText = "Số đăng ký";
            this.soDangKyDataGridViewTextBoxColumn.Name = "soDangKyDataGridViewTextBoxColumn";
            this.soDangKyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // hangSanXuatDataGridViewTextBoxColumn
            // 
            this.hangSanXuatDataGridViewTextBoxColumn.DataPropertyName = "HangSanXuat";
            this.hangSanXuatDataGridViewTextBoxColumn.HeaderText = "Hãng sản xuất";
            this.hangSanXuatDataGridViewTextBoxColumn.Name = "hangSanXuatDataGridViewTextBoxColumn";
            this.hangSanXuatDataGridViewTextBoxColumn.ReadOnly = true;
            this.hangSanXuatDataGridViewTextBoxColumn.Width = 150;
            // 
            // nhaPhanPhoiDataGridViewTextBoxColumn
            // 
            this.nhaPhanPhoiDataGridViewTextBoxColumn.DataPropertyName = "NhaPhanPhoi";
            this.nhaPhanPhoiDataGridViewTextBoxColumn.HeaderText = "Nhà phân phối";
            this.nhaPhanPhoiDataGridViewTextBoxColumn.Name = "nhaPhanPhoiDataGridViewTextBoxColumn";
            this.nhaPhanPhoiDataGridViewTextBoxColumn.ReadOnly = true;
            this.nhaPhanPhoiDataGridViewTextBoxColumn.Width = 150;
            // 
            // CreatedDate
            // 
            this.CreatedDate.DataPropertyName = "CreatedDate";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle13.NullValue = null;
            this.CreatedDate.DefaultCellStyle = dataGridViewCellStyle13;
            this.CreatedDate.HeaderText = "Ngày tạo";
            this.CreatedDate.Name = "CreatedDate";
            this.CreatedDate.ReadOnly = true;
            this.CreatedDate.Width = 120;
            // 
            // loThuocViewBindingSource
            // 
            this.loThuocViewBindingSource.DataSource = typeof(MM.Databasae.LoThuocView);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtpkDenNgay);
            this.panel2.Controls.Add(this.dtpkTuNgay);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtTenThuoc);
            this.panel2.Controls.Add(this.raTuNgayDenNgay);
            this.panel2.Controls.Add(this.raTenThuoc);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(973, 64);
            this.panel2.TabIndex = 6;
            // 
            // dtpkDenNgay
            // 
            this.dtpkDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkDenNgay.Enabled = false;
            this.dtpkDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkDenNgay.Location = new System.Drawing.Point(269, 34);
            this.dtpkDenNgay.Name = "dtpkDenNgay";
            this.dtpkDenNgay.Size = new System.Drawing.Size(111, 20);
            this.dtpkDenNgay.TabIndex = 6;
            this.dtpkDenNgay.ValueChanged += new System.EventHandler(this.dtpkDenNgay_ValueChanged);
            // 
            // dtpkTuNgay
            // 
            this.dtpkTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkTuNgay.Enabled = false;
            this.dtpkTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkTuNgay.Location = new System.Drawing.Point(93, 34);
            this.dtpkTuNgay.Name = "dtpkTuNgay";
            this.dtpkTuNgay.Size = new System.Drawing.Size(111, 20);
            this.dtpkTuNgay.TabIndex = 5;
            this.dtpkTuNgay.ValueChanged += new System.EventHandler(this.dtpkTuNgay_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Đến ngày:";
            // 
            // txtTenThuoc
            // 
            this.txtTenThuoc.Location = new System.Drawing.Point(93, 10);
            this.txtTenThuoc.Name = "txtTenThuoc";
            this.txtTenThuoc.Size = new System.Drawing.Size(287, 20);
            this.txtTenThuoc.TabIndex = 2;
            this.txtTenThuoc.TextChanged += new System.EventHandler(this.txtTenThuoc_TextChanged);
            // 
            // raTuNgayDenNgay
            // 
            this.raTuNgayDenNgay.AutoSize = true;
            this.raTuNgayDenNgay.Location = new System.Drawing.Point(16, 35);
            this.raTuNgayDenNgay.Name = "raTuNgayDenNgay";
            this.raTuNgayDenNgay.Size = new System.Drawing.Size(64, 17);
            this.raTuNgayDenNgay.TabIndex = 1;
            this.raTuNgayDenNgay.Text = "Từ ngày";
            this.raTuNgayDenNgay.UseVisualStyleBackColor = true;
            this.raTuNgayDenNgay.CheckedChanged += new System.EventHandler(this.raTuNgayDenNgay_CheckedChanged);
            // 
            // raTenThuoc
            // 
            this.raTenThuoc.AutoSize = true;
            this.raTenThuoc.Checked = true;
            this.raTenThuoc.Location = new System.Drawing.Point(16, 11);
            this.raTenThuoc.Name = "raTenThuoc";
            this.raTenThuoc.Size = new System.Drawing.Size(74, 17);
            this.raTenThuoc.TabIndex = 0;
            this.raTenThuoc.TabStop = true;
            this.raTenThuoc.Text = "Tên thuốc";
            this.raTenThuoc.UseVisualStyleBackColor = true;
            this.raTenThuoc.CheckedChanged += new System.EventHandler(this.raTenThuoc_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkChecked);
            this.panel3.Controls.Add(this.dgLoThuoc);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 64);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(973, 336);
            this.panel3.TabIndex = 7;
            // 
            // uLoThuocList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uLoThuocList";
            this.Size = new System.Drawing.Size(973, 438);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgLoThuoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loThuocViewBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgLoThuoc;
        private System.Windows.Forms.BindingSource loThuocViewBindingSource;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dtpkDenNgay;
        private System.Windows.Forms.DateTimePicker dtpkTuNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTenThuoc;
        private System.Windows.Forms.RadioButton raTuNgayDenNgay;
        private System.Windows.Forms.RadioButton raTenThuoc;
        private System.Windows.Forms.Panel panel3;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn maLoThuocDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenLoThuocDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenThuocDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngaySanXuatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayHetHanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soLuongNhapDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn giaNhapDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn donViTinhNhapDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TongTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn donViTinhQuiDoiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soLuongQuiDoiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn GiaNhapQuiDoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn soLuongXuatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soDangKyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hangSanXuatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nhaPhanPhoiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedDate;
    }
}
