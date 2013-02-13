namespace MM.Controls
{
    partial class uThongBaoList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uThongBaoList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.raDaXoa = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTenThongBao = new System.Windows.Forms.TextBox();
            this.raDangChoDuyet = new System.Windows.Forms.RadioButton();
            this.raDaDuyet = new System.Windows.Forms.RadioButton();
            this.raTatCa = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbKetQuaTimDuoc = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.txtTenNguoiTao = new System.Windows.Forms.TextBox();
            this.dtpkDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpkTuNgay = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPhucHoi = new System.Windows.Forms.Button();
            this.btnXemSuaDoi = new System.Windows.Forms.Button();
            this.btnXemQuaTrinhDuyet = new System.Windows.Forms.Button();
            this.btnXemThongBao = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgThongBao = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tenThongBaoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayDuyet1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayDuyet2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayDuyet3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thongBaoViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgThongBao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thongBaoViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.raDaXoa);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtTenThongBao);
            this.panel2.Controls.Add(this.raDangChoDuyet);
            this.panel2.Controls.Add(this.raDaDuyet);
            this.panel2.Controls.Add(this.raTatCa);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lbKetQuaTimDuoc);
            this.panel2.Controls.Add(this.btnView);
            this.panel2.Controls.Add(this.txtTenNguoiTao);
            this.panel2.Controls.Add(this.dtpkDenNgay);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.dtpkTuNgay);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(974, 111);
            this.panel2.TabIndex = 9;
            // 
            // raDaXoa
            // 
            this.raDaXoa.AutoSize = true;
            this.raDaXoa.Location = new System.Drawing.Point(353, 85);
            this.raDaXoa.Name = "raDaXoa";
            this.raDaXoa.Size = new System.Drawing.Size(59, 17);
            this.raDaXoa.TabIndex = 10;
            this.raDaXoa.Text = "Đã xóa";
            this.raDaXoa.UseVisualStyleBackColor = true;
            this.raDaXoa.CheckedChanged += new System.EventHandler(this.raDaXoa_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Tên thông báo:";
            // 
            // txtTenThongBao
            // 
            this.txtTenThongBao.Location = new System.Drawing.Point(98, 57);
            this.txtTenThongBao.Name = "txtTenThongBao";
            this.txtTenThongBao.Size = new System.Drawing.Size(291, 20);
            this.txtTenThongBao.TabIndex = 6;
            // 
            // raDangChoDuyet
            // 
            this.raDangChoDuyet.AutoSize = true;
            this.raDangChoDuyet.Location = new System.Drawing.Point(257, 85);
            this.raDangChoDuyet.Name = "raDangChoDuyet";
            this.raDangChoDuyet.Size = new System.Drawing.Size(79, 17);
            this.raDangChoDuyet.TabIndex = 9;
            this.raDangChoDuyet.Text = "Chưa duyệt";
            this.raDangChoDuyet.UseVisualStyleBackColor = true;
            this.raDangChoDuyet.CheckedChanged += new System.EventHandler(this.raDangChoDuyet_CheckedChanged);
            // 
            // raDaDuyet
            // 
            this.raDaDuyet.AutoSize = true;
            this.raDaDuyet.Location = new System.Drawing.Point(172, 85);
            this.raDaDuyet.Name = "raDaDuyet";
            this.raDaDuyet.Size = new System.Drawing.Size(68, 17);
            this.raDaDuyet.TabIndex = 8;
            this.raDaDuyet.Text = "Đã duyệt";
            this.raDaDuyet.UseVisualStyleBackColor = true;
            this.raDaDuyet.CheckedChanged += new System.EventHandler(this.raDaDuyet_CheckedChanged);
            // 
            // raTatCa
            // 
            this.raTatCa.AutoSize = true;
            this.raTatCa.Checked = true;
            this.raTatCa.Location = new System.Drawing.Point(98, 85);
            this.raTatCa.Name = "raTatCa";
            this.raTatCa.Size = new System.Drawing.Size(56, 17);
            this.raTatCa.TabIndex = 7;
            this.raTatCa.TabStop = true;
            this.raTatCa.Text = "Tất cả";
            this.raTatCa.UseVisualStyleBackColor = true;
            this.raTatCa.CheckedChanged += new System.EventHandler(this.raTatCa_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Tên người tạo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Tử ngày";
            // 
            // lbKetQuaTimDuoc
            // 
            this.lbKetQuaTimDuoc.AutoSize = true;
            this.lbKetQuaTimDuoc.ForeColor = System.Drawing.Color.Blue;
            this.lbKetQuaTimDuoc.Location = new System.Drawing.Point(504, 85);
            this.lbKetQuaTimDuoc.Name = "lbKetQuaTimDuoc";
            this.lbKetQuaTimDuoc.Size = new System.Drawing.Size(100, 13);
            this.lbKetQuaTimDuoc.TabIndex = 16;
            this.lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
            // 
            // btnView
            // 
            this.btnView.Image = global::MM.Properties.Resources.views_icon;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(423, 80);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 11;
            this.btnView.Text = "   &Tìm";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // txtTenNguoiTao
            // 
            this.txtTenNguoiTao.Location = new System.Drawing.Point(98, 34);
            this.txtTenNguoiTao.Name = "txtTenNguoiTao";
            this.txtTenNguoiTao.Size = new System.Drawing.Size(291, 20);
            this.txtTenNguoiTao.TabIndex = 5;
            // 
            // dtpkDenNgay
            // 
            this.dtpkDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkDenNgay.Location = new System.Drawing.Point(276, 10);
            this.dtpkDenNgay.Name = "dtpkDenNgay";
            this.dtpkDenNgay.Size = new System.Drawing.Size(113, 20);
            this.dtpkDenNgay.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(219, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "đến ngày";
            // 
            // dtpkTuNgay
            // 
            this.dtpkTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkTuNgay.Location = new System.Drawing.Point(98, 10);
            this.dtpkTuNgay.Name = "dtpkTuNgay";
            this.dtpkTuNgay.Size = new System.Drawing.Size(113, 20);
            this.dtpkTuNgay.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPhucHoi);
            this.panel1.Controls.Add(this.btnXemSuaDoi);
            this.panel1.Controls.Add(this.btnXemQuaTrinhDuyet);
            this.panel1.Controls.Add(this.btnXemThongBao);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 391);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(974, 38);
            this.panel1.TabIndex = 10;
            // 
            // btnPhucHoi
            // 
            this.btnPhucHoi.Image = global::MM.Properties.Resources.backup_restore_icon__1_;
            this.btnPhucHoi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPhucHoi.Location = new System.Drawing.Point(244, 6);
            this.btnPhucHoi.Name = "btnPhucHoi";
            this.btnPhucHoi.Size = new System.Drawing.Size(87, 25);
            this.btnPhucHoi.TabIndex = 7;
            this.btnPhucHoi.Text = "     &Phục hồi";
            this.btnPhucHoi.UseVisualStyleBackColor = true;
            this.btnPhucHoi.Click += new System.EventHandler(this.btnPhucHoi_Click);
            // 
            // btnXemSuaDoi
            // 
            this.btnXemSuaDoi.ForeColor = System.Drawing.Color.Red;
            this.btnXemSuaDoi.Image = ((System.Drawing.Image)(resources.GetObject("btnXemSuaDoi.Image")));
            this.btnXemSuaDoi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXemSuaDoi.Location = new System.Drawing.Point(610, 6);
            this.btnXemSuaDoi.Name = "btnXemSuaDoi";
            this.btnXemSuaDoi.Size = new System.Drawing.Size(153, 25);
            this.btnXemSuaDoi.TabIndex = 5;
            this.btnXemSuaDoi.Text = "    &Xem sửa đổi cần duyệt";
            this.btnXemSuaDoi.UseVisualStyleBackColor = true;
            this.btnXemSuaDoi.Click += new System.EventHandler(this.btnXemSuaDoi_Click);
            // 
            // btnXemQuaTrinhDuyet
            // 
            this.btnXemQuaTrinhDuyet.Image = ((System.Drawing.Image)(resources.GetObject("btnXemQuaTrinhDuyet.Image")));
            this.btnXemQuaTrinhDuyet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXemQuaTrinhDuyet.Location = new System.Drawing.Point(460, 6);
            this.btnXemQuaTrinhDuyet.Name = "btnXemQuaTrinhDuyet";
            this.btnXemQuaTrinhDuyet.Size = new System.Drawing.Size(146, 25);
            this.btnXemQuaTrinhDuyet.TabIndex = 4;
            this.btnXemQuaTrinhDuyet.Text = "    &Xem quá trình duyệt";
            this.btnXemQuaTrinhDuyet.UseVisualStyleBackColor = true;
            this.btnXemQuaTrinhDuyet.Click += new System.EventHandler(this.btnXemQuaTrinhDuyet_Click);
            // 
            // btnXemThongBao
            // 
            this.btnXemThongBao.Image = ((System.Drawing.Image)(resources.GetObject("btnXemThongBao.Image")));
            this.btnXemThongBao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXemThongBao.Location = new System.Drawing.Point(336, 6);
            this.btnXemThongBao.Name = "btnXemThongBao";
            this.btnXemThongBao.Size = new System.Drawing.Size(119, 25);
            this.btnXemThongBao.TabIndex = 3;
            this.btnXemThongBao.Text = "    &Xem thông báo";
            this.btnXemThongBao.UseVisualStyleBackColor = true;
            this.btnXemThongBao.Click += new System.EventHandler(this.btnXemThongBao_Click);
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
            // panel3
            // 
            this.panel3.Controls.Add(this.chkChecked);
            this.panel3.Controls.Add(this.dgThongBao);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 111);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(974, 280);
            this.panel3.TabIndex = 11;
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
            // dgThongBao
            // 
            this.dgThongBao.AllowUserToAddRows = false;
            this.dgThongBao.AllowUserToDeleteRows = false;
            this.dgThongBao.AllowUserToOrderColumns = true;
            this.dgThongBao.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgThongBao.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgThongBao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgThongBao.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.tenThongBaoDataGridViewTextBoxColumn,
            this.ngayDuyet1DataGridViewTextBoxColumn,
            this.ngayDuyet2DataGridViewTextBoxColumn,
            this.ngayDuyet3DataGridViewTextBoxColumn,
            this.createdDateDataGridViewTextBoxColumn,
            this.fullNameDataGridViewTextBoxColumn});
            this.dgThongBao.DataSource = this.thongBaoViewBindingSource;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgThongBao.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgThongBao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgThongBao.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgThongBao.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgThongBao.HighlightSelectedColumnHeaders = false;
            this.dgThongBao.Location = new System.Drawing.Point(0, 0);
            this.dgThongBao.MultiSelect = false;
            this.dgThongBao.Name = "dgThongBao";
            this.dgThongBao.RowHeadersWidth = 30;
            this.dgThongBao.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgThongBao.Size = new System.Drawing.Size(974, 280);
            this.dgThongBao.TabIndex = 2;
            this.dgThongBao.DoubleClick += new System.EventHandler(this.dgThongBao_DoubleClick);
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
            // tenThongBaoDataGridViewTextBoxColumn
            // 
            this.tenThongBaoDataGridViewTextBoxColumn.DataPropertyName = "TenThongBao";
            this.tenThongBaoDataGridViewTextBoxColumn.HeaderText = "Tên thông báo";
            this.tenThongBaoDataGridViewTextBoxColumn.Name = "tenThongBaoDataGridViewTextBoxColumn";
            this.tenThongBaoDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenThongBaoDataGridViewTextBoxColumn.Width = 200;
            // 
            // ngayDuyet1DataGridViewTextBoxColumn
            // 
            this.ngayDuyet1DataGridViewTextBoxColumn.DataPropertyName = "NgayDuyet1";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle2.NullValue = null;
            this.ngayDuyet1DataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.ngayDuyet1DataGridViewTextBoxColumn.HeaderText = "Duyệt lần 1";
            this.ngayDuyet1DataGridViewTextBoxColumn.Name = "ngayDuyet1DataGridViewTextBoxColumn";
            this.ngayDuyet1DataGridViewTextBoxColumn.ReadOnly = true;
            this.ngayDuyet1DataGridViewTextBoxColumn.Width = 120;
            // 
            // ngayDuyet2DataGridViewTextBoxColumn
            // 
            this.ngayDuyet2DataGridViewTextBoxColumn.DataPropertyName = "NgayDuyet2";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle3.NullValue = null;
            this.ngayDuyet2DataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.ngayDuyet2DataGridViewTextBoxColumn.HeaderText = "Duyệt lần 2";
            this.ngayDuyet2DataGridViewTextBoxColumn.Name = "ngayDuyet2DataGridViewTextBoxColumn";
            this.ngayDuyet2DataGridViewTextBoxColumn.ReadOnly = true;
            this.ngayDuyet2DataGridViewTextBoxColumn.Width = 120;
            // 
            // ngayDuyet3DataGridViewTextBoxColumn
            // 
            this.ngayDuyet3DataGridViewTextBoxColumn.DataPropertyName = "NgayDuyet3";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle4.NullValue = null;
            this.ngayDuyet3DataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.ngayDuyet3DataGridViewTextBoxColumn.HeaderText = "Duyệt lần 3";
            this.ngayDuyet3DataGridViewTextBoxColumn.Name = "ngayDuyet3DataGridViewTextBoxColumn";
            this.ngayDuyet3DataGridViewTextBoxColumn.ReadOnly = true;
            this.ngayDuyet3DataGridViewTextBoxColumn.Width = 120;
            // 
            // createdDateDataGridViewTextBoxColumn
            // 
            this.createdDateDataGridViewTextBoxColumn.DataPropertyName = "CreatedDate";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle5.NullValue = null;
            this.createdDateDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.createdDateDataGridViewTextBoxColumn.HeaderText = "Ngày tạo";
            this.createdDateDataGridViewTextBoxColumn.Name = "createdDateDataGridViewTextBoxColumn";
            this.createdDateDataGridViewTextBoxColumn.ReadOnly = true;
            this.createdDateDataGridViewTextBoxColumn.Width = 120;
            // 
            // fullNameDataGridViewTextBoxColumn
            // 
            this.fullNameDataGridViewTextBoxColumn.DataPropertyName = "FullName";
            this.fullNameDataGridViewTextBoxColumn.HeaderText = "Người tạo";
            this.fullNameDataGridViewTextBoxColumn.Name = "fullNameDataGridViewTextBoxColumn";
            this.fullNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fullNameDataGridViewTextBoxColumn.Width = 200;
            // 
            // thongBaoViewBindingSource
            // 
            this.thongBaoViewBindingSource.DataSource = typeof(MM.Databasae.ThongBaoView);
            // 
            // uThongBaoList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "uThongBaoList";
            this.Size = new System.Drawing.Size(974, 429);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgThongBao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thongBaoViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbKetQuaTimDuoc;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.TextBox txtTenNguoiTao;
        private System.Windows.Forms.DateTimePicker dtpkDenNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpkTuNgay;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnXemThongBao;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgThongBao;
        private System.Windows.Forms.BindingSource thongBaoViewBindingSource;
        private System.Windows.Forms.Button btnXemQuaTrinhDuyet;
        private System.Windows.Forms.RadioButton raDangChoDuyet;
        private System.Windows.Forms.RadioButton raDaDuyet;
        private System.Windows.Forms.RadioButton raTatCa;
        private System.Windows.Forms.Button btnXemSuaDoi;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenThongBaoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayDuyet1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayDuyet2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayDuyet3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTenThongBao;
        private System.Windows.Forms.RadioButton raDaXoa;
        private System.Windows.Forms.Button btnPhucHoi;
    }
}
