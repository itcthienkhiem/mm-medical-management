namespace MM.Dialogs
{
    partial class dlgAddToaThuoc
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddToaThuoc));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtGioiTinh = new System.Windows.Forms.TextBox();
            this.txtNgaySinh = new System.Windows.Forms.TextBox();
            this.txtTenBenhNhan = new System.Windows.Forms.TextBox();
            this.btnChonBenhNhan = new System.Windows.Forms.Button();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.cboBacSi = new System.Windows.Forms.ComboBox();
            this.docStaffViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dtpkNgayKeToa = new System.Windows.Forms.DateTimePicker();
            this.txtMaToaThuoc = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.patientViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.dgChiTiet = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thuocGUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.thuocBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ThuocThayTheDataGridButtonColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soNgayUongDataGridViewTextBoxColumn = new MM.Controls.TNumEditDataGridViewColumn();
            this.soLanTrongNgayDataGridViewTextBoxColumn = new MM.Controls.TNumEditDataGridViewColumn();
            this.soLuongTrongLanDataGridViewTextBoxColumn = new MM.Controls.TNumEditDataGridViewColumn();
            this.noteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctmToaThuoc = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.thuocThayTheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chiTietToaThuocBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgChiTiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thuocBindingSource)).BeginInit();
            this.ctmToaThuoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chiTietToaThuocBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtGioiTinh);
            this.groupBox1.Controls.Add(this.txtNgaySinh);
            this.groupBox1.Controls.Add(this.txtTenBenhNhan);
            this.groupBox1.Controls.Add(this.btnChonBenhNhan);
            this.groupBox1.Controls.Add(this.txtGhiChu);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.cboBacSi);
            this.groupBox1.Controls.Add(this.dtpkNgayKeToa);
            this.groupBox1.Controls.Add(this.txtMaToaThuoc);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(789, 202);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin toa thuốc";
            // 
            // txtGioiTinh
            // 
            this.txtGioiTinh.Location = new System.Drawing.Point(432, 94);
            this.txtGioiTinh.Name = "txtGioiTinh";
            this.txtGioiTinh.ReadOnly = true;
            this.txtGioiTinh.Size = new System.Drawing.Size(75, 20);
            this.txtGioiTinh.TabIndex = 53;
            this.txtGioiTinh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtNgaySinh
            // 
            this.txtNgaySinh.Location = new System.Drawing.Point(340, 94);
            this.txtNgaySinh.Name = "txtNgaySinh";
            this.txtNgaySinh.ReadOnly = true;
            this.txtNgaySinh.Size = new System.Drawing.Size(88, 20);
            this.txtNgaySinh.TabIndex = 52;
            this.txtNgaySinh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTenBenhNhan
            // 
            this.txtTenBenhNhan.Location = new System.Drawing.Point(93, 94);
            this.txtTenBenhNhan.Name = "txtTenBenhNhan";
            this.txtTenBenhNhan.ReadOnly = true;
            this.txtTenBenhNhan.Size = new System.Drawing.Size(243, 20);
            this.txtTenBenhNhan.TabIndex = 51;
            // 
            // btnChonBenhNhan
            // 
            this.btnChonBenhNhan.Location = new System.Drawing.Point(531, 93);
            this.btnChonBenhNhan.Name = "btnChonBenhNhan";
            this.btnChonBenhNhan.Size = new System.Drawing.Size(105, 22);
            this.btnChonBenhNhan.TabIndex = 50;
            this.btnChonBenhNhan.Text = "Chọn bệnh nhân...";
            this.btnChonBenhNhan.UseVisualStyleBackColor = true;
            this.btnChonBenhNhan.Click += new System.EventHandler(this.btnChonBenhNhan_Click);
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(93, 118);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(543, 70);
            this.txtGhiChu.TabIndex = 49;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(511, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 13);
            this.label7.TabIndex = 48;
            this.label7.Text = "[*]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(385, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 46;
            this.label6.Text = "[*]";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(243, 24);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(17, 13);
            this.label22.TabIndex = 45;
            this.label22.Text = "[*]";
            // 
            // cboBacSi
            // 
            this.cboBacSi.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboBacSi.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboBacSi.DataSource = this.docStaffViewBindingSource;
            this.cboBacSi.DisplayMember = "FullName";
            this.cboBacSi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBacSi.FormattingEnabled = true;
            this.cboBacSi.Location = new System.Drawing.Point(93, 69);
            this.cboBacSi.Name = "cboBacSi";
            this.cboBacSi.Size = new System.Drawing.Size(288, 21);
            this.cboBacSi.TabIndex = 7;
            this.cboBacSi.ValueMember = "DocStaffGUID";
            // 
            // docStaffViewBindingSource
            // 
            this.docStaffViewBindingSource.DataSource = typeof(MM.Databasae.DocStaffView);
            // 
            // dtpkNgayKeToa
            // 
            this.dtpkNgayKeToa.CustomFormat = "dd/MM/yyyy";
            this.dtpkNgayKeToa.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkNgayKeToa.Location = new System.Drawing.Point(93, 45);
            this.dtpkNgayKeToa.Name = "dtpkNgayKeToa";
            this.dtpkNgayKeToa.Size = new System.Drawing.Size(115, 20);
            this.dtpkNgayKeToa.TabIndex = 6;
            // 
            // txtMaToaThuoc
            // 
            this.txtMaToaThuoc.Location = new System.Drawing.Point(93, 21);
            this.txtMaToaThuoc.Name = "txtMaToaThuoc";
            this.txtMaToaThuoc.Size = new System.Drawing.Size(146, 20);
            this.txtMaToaThuoc.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Ghi chú:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Bệnh nhân:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Bác sĩ kê toa:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ngày kê toa:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã toa thuốc:";
            // 
            // patientViewBindingSource
            // 
            this.patientViewBindingSource.DataSource = typeof(MM.Databasae.PatientView);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(403, 538);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(324, 538);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // dgChiTiet
            // 
            this.dgChiTiet.AllowUserToOrderColumns = true;
            this.dgChiTiet.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgChiTiet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.thuocGUIDDataGridViewTextBoxColumn,
            this.ThuocThayTheDataGridButtonColumn,
            this.soNgayUongDataGridViewTextBoxColumn,
            this.soLanTrongNgayDataGridViewTextBoxColumn,
            this.soLuongTrongLanDataGridViewTextBoxColumn,
            this.noteDataGridViewTextBoxColumn});
            this.dgChiTiet.ContextMenuStrip = this.ctmToaThuoc;
            this.dgChiTiet.DataSource = this.chiTietToaThuocBindingSource;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgChiTiet.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgChiTiet.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgChiTiet.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgChiTiet.HighlightSelectedColumnHeaders = false;
            this.dgChiTiet.Location = new System.Drawing.Point(6, 212);
            this.dgChiTiet.Name = "dgChiTiet";
            this.dgChiTiet.RowHeadersWidth = 30;
            this.dgChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgChiTiet.Size = new System.Drawing.Size(790, 320);
            this.dgChiTiet.TabIndex = 11;
            this.dgChiTiet.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgChiTiet_CellMouseDown);
            this.dgChiTiet.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgChiTiet_CellValueChanged);
            this.dgChiTiet.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgChiTiet_ColumnHeaderMouseClick);
            this.dgChiTiet.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgChiTiet_EditingControlShowing);
            this.dgChiTiet.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgChiTiet_UserAddedRow);
            this.dgChiTiet.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgChiTiet_UserDeletedRow);
            this.dgChiTiet.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgChiTiet_UserDeletingRow);
            // 
            // STT
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.STT.DefaultCellStyle = dataGridViewCellStyle2;
            this.STT.Frozen = true;
            this.STT.HeaderText = "STT";
            this.STT.Name = "STT";
            this.STT.ReadOnly = true;
            this.STT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.STT.Width = 30;
            // 
            // thuocGUIDDataGridViewTextBoxColumn
            // 
            this.thuocGUIDDataGridViewTextBoxColumn.DataPropertyName = "ThuocGUID";
            this.thuocGUIDDataGridViewTextBoxColumn.DataSource = this.thuocBindingSource;
            this.thuocGUIDDataGridViewTextBoxColumn.DisplayMember = "TenThuoc";
            this.thuocGUIDDataGridViewTextBoxColumn.DisplayStyleForCurrentCellOnly = true;
            this.thuocGUIDDataGridViewTextBoxColumn.HeaderText = "Tên thuốc";
            this.thuocGUIDDataGridViewTextBoxColumn.Name = "thuocGUIDDataGridViewTextBoxColumn";
            this.thuocGUIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.thuocGUIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.thuocGUIDDataGridViewTextBoxColumn.ValueMember = "ThuocGUID";
            this.thuocGUIDDataGridViewTextBoxColumn.Width = 180;
            // 
            // thuocBindingSource
            // 
            this.thuocBindingSource.DataSource = typeof(MM.Databasae.Thuoc);
            // 
            // ThuocThayTheDataGridButtonColumn
            // 
            this.ThuocThayTheDataGridButtonColumn.DataPropertyName = "DonViTinh";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ThuocThayTheDataGridButtonColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.ThuocThayTheDataGridButtonColumn.HeaderText = "Đơn vị tính";
            this.ThuocThayTheDataGridButtonColumn.Name = "ThuocThayTheDataGridButtonColumn";
            this.ThuocThayTheDataGridButtonColumn.ReadOnly = true;
            this.ThuocThayTheDataGridButtonColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ThuocThayTheDataGridButtonColumn.Width = 85;
            // 
            // soNgayUongDataGridViewTextBoxColumn
            // 
            this.soNgayUongDataGridViewTextBoxColumn.AllowNegative = false;
            this.soNgayUongDataGridViewTextBoxColumn.DataPropertyName = "SoNgayUong";
            this.soNgayUongDataGridViewTextBoxColumn.DecimalLength = 0;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            this.soNgayUongDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.soNgayUongDataGridViewTextBoxColumn.HeaderText = "Số ngày uống";
            this.soNgayUongDataGridViewTextBoxColumn.Name = "soNgayUongDataGridViewTextBoxColumn";
            this.soNgayUongDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // soLanTrongNgayDataGridViewTextBoxColumn
            // 
            this.soLanTrongNgayDataGridViewTextBoxColumn.AllowNegative = false;
            this.soLanTrongNgayDataGridViewTextBoxColumn.DataPropertyName = "SoLanTrongNgay";
            this.soLanTrongNgayDataGridViewTextBoxColumn.DecimalLength = 0;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = null;
            this.soLanTrongNgayDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.soLanTrongNgayDataGridViewTextBoxColumn.HeaderText = "Số lần/ngày";
            this.soLanTrongNgayDataGridViewTextBoxColumn.Name = "soLanTrongNgayDataGridViewTextBoxColumn";
            this.soLanTrongNgayDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.soLanTrongNgayDataGridViewTextBoxColumn.Width = 90;
            // 
            // soLuongTrongLanDataGridViewTextBoxColumn
            // 
            this.soLuongTrongLanDataGridViewTextBoxColumn.AllowNegative = false;
            this.soLuongTrongLanDataGridViewTextBoxColumn.DataPropertyName = "SoLuongTrongLan";
            this.soLuongTrongLanDataGridViewTextBoxColumn.DecimalLength = 0;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewCellStyle6.NullValue = null;
            this.soLuongTrongLanDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.soLuongTrongLanDataGridViewTextBoxColumn.HeaderText = "Số lượng/lần";
            this.soLuongTrongLanDataGridViewTextBoxColumn.Name = "soLuongTrongLanDataGridViewTextBoxColumn";
            this.soLuongTrongLanDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // noteDataGridViewTextBoxColumn
            // 
            this.noteDataGridViewTextBoxColumn.DataPropertyName = "Note";
            this.noteDataGridViewTextBoxColumn.HeaderText = "Ghi chú";
            this.noteDataGridViewTextBoxColumn.Name = "noteDataGridViewTextBoxColumn";
            this.noteDataGridViewTextBoxColumn.Width = 240;
            // 
            // ctmToaThuoc
            // 
            this.ctmToaThuoc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thuocThayTheToolStripMenuItem});
            this.ctmToaThuoc.Name = "ctmToaThuoc";
            this.ctmToaThuoc.Size = new System.Drawing.Size(159, 26);
            // 
            // thuocThayTheToolStripMenuItem
            // 
            this.thuocThayTheToolStripMenuItem.Name = "thuocThayTheToolStripMenuItem";
            this.thuocThayTheToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.thuocThayTheToolStripMenuItem.Text = "Thuốc thay thế";
            this.thuocThayTheToolStripMenuItem.Click += new System.EventHandler(this.thuocThayTheToolStripMenuItem_Click);
            // 
            // chiTietToaThuocBindingSource
            // 
            this.chiTietToaThuocBindingSource.DataSource = typeof(MM.Databasae.ChiTietToaThuoc);
            // 
            // dlgAddToaThuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(803, 568);
            this.Controls.Add(this.dgChiTiet);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddToaThuoc";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them toa thuoc";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddToaThuoc_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddToaThuoc_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgChiTiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thuocBindingSource)).EndInit();
            this.ctmToaThuoc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chiTietToaThuocBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboBacSi;
        private System.Windows.Forms.BindingSource docStaffViewBindingSource;
        private System.Windows.Forms.DateTimePicker dtpkNgayKeToa;
        private System.Windows.Forms.TextBox txtMaToaThuoc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.BindingSource patientViewBindingSource;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgChiTiet;
        private System.Windows.Forms.BindingSource thuocBindingSource;
        private System.Windows.Forms.BindingSource chiTietToaThuocBindingSource;
        private System.Windows.Forms.ContextMenuStrip ctmToaThuoc;
        private System.Windows.Forms.ToolStripMenuItem thuocThayTheToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewComboBoxColumn thuocGUIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThuocThayTheDataGridButtonColumn;
        private Controls.TNumEditDataGridViewColumn soNgayUongDataGridViewTextBoxColumn;
        private Controls.TNumEditDataGridViewColumn soLanTrongNgayDataGridViewTextBoxColumn;
        private Controls.TNumEditDataGridViewColumn soLuongTrongLanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noteDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnChonBenhNhan;
        private System.Windows.Forms.TextBox txtGioiTinh;
        private System.Windows.Forms.TextBox txtNgaySinh;
        private System.Windows.Forms.TextBox txtTenBenhNhan;
    }
}