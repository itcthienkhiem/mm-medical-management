namespace MM.Controls
{
    partial class uNhapKhoCapCuuList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgNhapKhoCapCuu = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.nhapKhoCapCuuViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtpkDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpkTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTenCapCuu = new System.Windows.Forms.TextBox();
            this.raTuNgayDenNgay = new System.Windows.Forms.RadioButton();
            this.raTenCapCuu = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.colChecked = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.tenCapCuuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngaySanXuatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayHetHanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soLuongNhapDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.donViTinhNhapDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.donViTinhQuiDoiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soLuongQuiDoiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soLuongXuatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soDangKyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hangSanXuatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nhaPhanPhoiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayNhapDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgNhapKhoCapCuu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nhapKhoCapCuuViewBindingSource)).BeginInit();
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
            // dgNhapKhoCapCuu
            // 
            this.dgNhapKhoCapCuu.AllowUserToAddRows = false;
            this.dgNhapKhoCapCuu.AllowUserToDeleteRows = false;
            this.dgNhapKhoCapCuu.AllowUserToOrderColumns = true;
            this.dgNhapKhoCapCuu.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgNhapKhoCapCuu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgNhapKhoCapCuu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgNhapKhoCapCuu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.tenCapCuuDataGridViewTextBoxColumn,
            this.ngaySanXuatDataGridViewTextBoxColumn,
            this.ngayHetHanDataGridViewTextBoxColumn,
            this.soLuongNhapDataGridViewTextBoxColumn,
            this.donViTinhNhapDataGridViewTextBoxColumn,
            this.donViTinhQuiDoiDataGridViewTextBoxColumn,
            this.soLuongQuiDoiDataGridViewTextBoxColumn,
            this.soLuongXuatDataGridViewTextBoxColumn,
            this.soDangKyDataGridViewTextBoxColumn,
            this.hangSanXuatDataGridViewTextBoxColumn,
            this.nhaPhanPhoiDataGridViewTextBoxColumn,
            this.ngayNhapDataGridViewTextBoxColumn});
            this.dgNhapKhoCapCuu.DataSource = this.nhapKhoCapCuuViewBindingSource;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgNhapKhoCapCuu.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgNhapKhoCapCuu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgNhapKhoCapCuu.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgNhapKhoCapCuu.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgNhapKhoCapCuu.HighlightSelectedColumnHeaders = false;
            this.dgNhapKhoCapCuu.Location = new System.Drawing.Point(0, 0);
            this.dgNhapKhoCapCuu.MultiSelect = false;
            this.dgNhapKhoCapCuu.Name = "dgNhapKhoCapCuu";
            this.dgNhapKhoCapCuu.ReadOnly = true;
            this.dgNhapKhoCapCuu.RowHeadersWidth = 30;
            this.dgNhapKhoCapCuu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgNhapKhoCapCuu.Size = new System.Drawing.Size(973, 336);
            this.dgNhapKhoCapCuu.TabIndex = 4;
            this.dgNhapKhoCapCuu.DoubleClick += new System.EventHandler(this.dgLoThuoc_DoubleClick);
            // 
            // nhapKhoCapCuuViewBindingSource
            // 
            this.nhapKhoCapCuuViewBindingSource.DataSource = typeof(MM.Databasae.NhapKhoCapCuuView);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtpkDenNgay);
            this.panel2.Controls.Add(this.dtpkTuNgay);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtTenCapCuu);
            this.panel2.Controls.Add(this.raTuNgayDenNgay);
            this.panel2.Controls.Add(this.raTenCapCuu);
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
            this.dtpkDenNgay.Location = new System.Drawing.Point(279, 34);
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
            this.dtpkTuNgay.Location = new System.Drawing.Point(103, 34);
            this.dtpkTuNgay.Name = "dtpkTuNgay";
            this.dtpkTuNgay.Size = new System.Drawing.Size(111, 20);
            this.dtpkTuNgay.TabIndex = 5;
            this.dtpkTuNgay.ValueChanged += new System.EventHandler(this.dtpkTuNgay_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Đến ngày:";
            // 
            // txtTenCapCuu
            // 
            this.txtTenCapCuu.Location = new System.Drawing.Point(103, 10);
            this.txtTenCapCuu.Name = "txtTenCapCuu";
            this.txtTenCapCuu.Size = new System.Drawing.Size(287, 20);
            this.txtTenCapCuu.TabIndex = 2;
            this.txtTenCapCuu.TextChanged += new System.EventHandler(this.txtTenThuoc_TextChanged);
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
            // raTenCapCuu
            // 
            this.raTenCapCuu.AutoSize = true;
            this.raTenCapCuu.Checked = true;
            this.raTenCapCuu.Location = new System.Drawing.Point(16, 11);
            this.raTenCapCuu.Name = "raTenCapCuu";
            this.raTenCapCuu.Size = new System.Drawing.Size(86, 17);
            this.raTenCapCuu.TabIndex = 0;
            this.raTenCapCuu.TabStop = true;
            this.raTenCapCuu.Text = "Tên cấp cứu";
            this.raTenCapCuu.UseVisualStyleBackColor = true;
            this.raTenCapCuu.CheckedChanged += new System.EventHandler(this.raTenThuoc_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkChecked);
            this.panel3.Controls.Add(this.dgNhapKhoCapCuu);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 64);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(973, 336);
            this.panel3.TabIndex = 7;
            // 
            // colChecked
            // 
            this.colChecked.Checked = true;
            this.colChecked.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.colChecked.CheckValue = "N";
            this.colChecked.DataPropertyName = "Checked";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colChecked.DefaultCellStyle = dataGridViewCellStyle2;
            this.colChecked.HeaderText = "";
            this.colChecked.Name = "colChecked";
            this.colChecked.ReadOnly = true;
            this.colChecked.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colChecked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colChecked.Width = 40;
            // 
            // tenCapCuuDataGridViewTextBoxColumn
            // 
            this.tenCapCuuDataGridViewTextBoxColumn.DataPropertyName = "TenCapCuu";
            this.tenCapCuuDataGridViewTextBoxColumn.HeaderText = "Tên cấp cứu";
            this.tenCapCuuDataGridViewTextBoxColumn.Name = "tenCapCuuDataGridViewTextBoxColumn";
            this.tenCapCuuDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenCapCuuDataGridViewTextBoxColumn.Width = 200;
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
            // donViTinhNhapDataGridViewTextBoxColumn
            // 
            this.donViTinhNhapDataGridViewTextBoxColumn.DataPropertyName = "DonViTinhNhap";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.donViTinhNhapDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.donViTinhNhapDataGridViewTextBoxColumn.HeaderText = "ĐVT Nhập";
            this.donViTinhNhapDataGridViewTextBoxColumn.Name = "donViTinhNhapDataGridViewTextBoxColumn";
            this.donViTinhNhapDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // donViTinhQuiDoiDataGridViewTextBoxColumn
            // 
            this.donViTinhQuiDoiDataGridViewTextBoxColumn.DataPropertyName = "DonViTinhQuiDoi";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.donViTinhQuiDoiDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.donViTinhQuiDoiDataGridViewTextBoxColumn.HeaderText = "ĐVT qui đổi";
            this.donViTinhQuiDoiDataGridViewTextBoxColumn.Name = "donViTinhQuiDoiDataGridViewTextBoxColumn";
            this.donViTinhQuiDoiDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // soLuongQuiDoiDataGridViewTextBoxColumn
            // 
            this.soLuongQuiDoiDataGridViewTextBoxColumn.DataPropertyName = "SoLuongQuiDoi";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N0";
            dataGridViewCellStyle8.NullValue = null;
            this.soLuongQuiDoiDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.soLuongQuiDoiDataGridViewTextBoxColumn.HeaderText = "Số lượng qui đổi";
            this.soLuongQuiDoiDataGridViewTextBoxColumn.Name = "soLuongQuiDoiDataGridViewTextBoxColumn";
            this.soLuongQuiDoiDataGridViewTextBoxColumn.ReadOnly = true;
            this.soLuongQuiDoiDataGridViewTextBoxColumn.Width = 110;
            // 
            // soLuongXuatDataGridViewTextBoxColumn
            // 
            this.soLuongXuatDataGridViewTextBoxColumn.DataPropertyName = "SoLuongXuat";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N0";
            dataGridViewCellStyle9.NullValue = null;
            this.soLuongXuatDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.soLuongXuatDataGridViewTextBoxColumn.HeaderText = "Số lượng xuất";
            this.soLuongXuatDataGridViewTextBoxColumn.Name = "soLuongXuatDataGridViewTextBoxColumn";
            this.soLuongXuatDataGridViewTextBoxColumn.ReadOnly = true;
            this.soLuongXuatDataGridViewTextBoxColumn.Visible = false;
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
            // ngayNhapDataGridViewTextBoxColumn
            // 
            this.ngayNhapDataGridViewTextBoxColumn.DataPropertyName = "NgayNhap";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle10.NullValue = null;
            this.ngayNhapDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.ngayNhapDataGridViewTextBoxColumn.HeaderText = "Ngày nhập";
            this.ngayNhapDataGridViewTextBoxColumn.Name = "ngayNhapDataGridViewTextBoxColumn";
            this.ngayNhapDataGridViewTextBoxColumn.ReadOnly = true;
            this.ngayNhapDataGridViewTextBoxColumn.Width = 120;
            // 
            // uNhapKhoCapCuuList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uNhapKhoCapCuuList";
            this.Size = new System.Drawing.Size(973, 438);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgNhapKhoCapCuu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nhapKhoCapCuuViewBindingSource)).EndInit();
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
        private DevComponents.DotNetBar.Controls.DataGridViewX dgNhapKhoCapCuu;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dtpkDenNgay;
        private System.Windows.Forms.DateTimePicker dtpkTuNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTenCapCuu;
        private System.Windows.Forms.RadioButton raTuNgayDenNgay;
        private System.Windows.Forms.RadioButton raTenCapCuu;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.BindingSource nhapKhoCapCuuViewBindingSource;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenCapCuuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngaySanXuatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayHetHanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soLuongNhapDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn donViTinhNhapDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn donViTinhQuiDoiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soLuongQuiDoiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soLuongXuatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soDangKyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hangSanXuatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nhaPhanPhoiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayNhapDataGridViewTextBoxColumn;
    }
}
