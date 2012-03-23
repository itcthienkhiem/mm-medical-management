namespace MM.Controls
{
    partial class uNhatKyLienHeCongTy
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgNhatKyLienHeCongTy = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.nhatKyLienHeCongTyViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colChecked = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.ngayGioLienHeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.congTyLienHeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noiDungLienHeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnView = new System.Windows.Forms.Button();
            this.txtTenBenhNhan = new System.Windows.Forms.TextBox();
            this.raTenBenhNhan = new System.Windows.Forms.RadioButton();
            this.dtpkDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpkTuNgay = new System.Windows.Forms.DateTimePicker();
            this.raTuNgayToiNgay = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgNhatKyLienHeCongTy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nhatKyLienHeCongTyViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnView);
            this.panel1.Controls.Add(this.txtTenBenhNhan);
            this.panel1.Controls.Add(this.raTenBenhNhan);
            this.panel1.Controls.Add(this.dtpkDenNgay);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpkTuNgay);
            this.panel1.Controls.Add(this.raTuNgayToiNgay);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 62);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 406);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 38);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkChecked);
            this.panel3.Controls.Add(this.dgNhatKyLienHeCongTy);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 62);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 344);
            this.panel3.TabIndex = 2;
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::MM.Properties.Resources.del;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(164, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 8;
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
            this.btnEdit.TabIndex = 7;
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
            this.btnAdd.TabIndex = 6;
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
            // dgNhatKyLienHeCongTy
            // 
            this.dgNhatKyLienHeCongTy.AllowUserToAddRows = false;
            this.dgNhatKyLienHeCongTy.AllowUserToDeleteRows = false;
            this.dgNhatKyLienHeCongTy.AllowUserToOrderColumns = true;
            this.dgNhatKyLienHeCongTy.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgNhatKyLienHeCongTy.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgNhatKyLienHeCongTy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgNhatKyLienHeCongTy.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.ngayGioLienHeDataGridViewTextBoxColumn,
            this.congTyLienHeDataGridViewTextBoxColumn,
            this.noiDungLienHeDataGridViewTextBoxColumn,
            this.fullNameDataGridViewTextBoxColumn});
            this.dgNhatKyLienHeCongTy.DataSource = this.nhatKyLienHeCongTyViewBindingSource;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgNhatKyLienHeCongTy.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgNhatKyLienHeCongTy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgNhatKyLienHeCongTy.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgNhatKyLienHeCongTy.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgNhatKyLienHeCongTy.HighlightSelectedColumnHeaders = false;
            this.dgNhatKyLienHeCongTy.Location = new System.Drawing.Point(0, 0);
            this.dgNhatKyLienHeCongTy.MultiSelect = false;
            this.dgNhatKyLienHeCongTy.Name = "dgNhatKyLienHeCongTy";
            this.dgNhatKyLienHeCongTy.ReadOnly = true;
            this.dgNhatKyLienHeCongTy.RowHeadersWidth = 30;
            this.dgNhatKyLienHeCongTy.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgNhatKyLienHeCongTy.Size = new System.Drawing.Size(800, 344);
            this.dgNhatKyLienHeCongTy.TabIndex = 4;
            this.dgNhatKyLienHeCongTy.DoubleClick += new System.EventHandler(this.dgYKienKhachHang_DoubleClick);
            // 
            // nhatKyLienHeCongTyViewBindingSource
            // 
            this.nhatKyLienHeCongTyViewBindingSource.DataSource = typeof(MM.Databasae.NhatKyLienHeCongTyView);
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
            // ngayGioLienHeDataGridViewTextBoxColumn
            // 
            this.ngayGioLienHeDataGridViewTextBoxColumn.DataPropertyName = "NgayGioLienHe";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle3.NullValue = null;
            this.ngayGioLienHeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.ngayGioLienHeDataGridViewTextBoxColumn.HeaderText = "Ngày giờ liên hệ";
            this.ngayGioLienHeDataGridViewTextBoxColumn.Name = "ngayGioLienHeDataGridViewTextBoxColumn";
            this.ngayGioLienHeDataGridViewTextBoxColumn.ReadOnly = true;
            this.ngayGioLienHeDataGridViewTextBoxColumn.Width = 120;
            // 
            // congTyLienHeDataGridViewTextBoxColumn
            // 
            this.congTyLienHeDataGridViewTextBoxColumn.DataPropertyName = "CongTyLienHe";
            this.congTyLienHeDataGridViewTextBoxColumn.HeaderText = "Công ty liên hệ";
            this.congTyLienHeDataGridViewTextBoxColumn.Name = "congTyLienHeDataGridViewTextBoxColumn";
            this.congTyLienHeDataGridViewTextBoxColumn.ReadOnly = true;
            this.congTyLienHeDataGridViewTextBoxColumn.Width = 250;
            // 
            // noiDungLienHeDataGridViewTextBoxColumn
            // 
            this.noiDungLienHeDataGridViewTextBoxColumn.DataPropertyName = "NoiDungLienHe";
            this.noiDungLienHeDataGridViewTextBoxColumn.HeaderText = "Nội dung liên hệ";
            this.noiDungLienHeDataGridViewTextBoxColumn.Name = "noiDungLienHeDataGridViewTextBoxColumn";
            this.noiDungLienHeDataGridViewTextBoxColumn.ReadOnly = true;
            this.noiDungLienHeDataGridViewTextBoxColumn.Width = 250;
            // 
            // fullNameDataGridViewTextBoxColumn
            // 
            this.fullNameDataGridViewTextBoxColumn.DataPropertyName = "FullName";
            this.fullNameDataGridViewTextBoxColumn.HeaderText = "Nhân viên liên hệ";
            this.fullNameDataGridViewTextBoxColumn.Name = "fullNameDataGridViewTextBoxColumn";
            this.fullNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fullNameDataGridViewTextBoxColumn.Width = 200;
            // 
            // btnView
            // 
            this.btnView.Image = global::MM.Properties.Resources.views_icon;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(377, 31);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 21;
            this.btnView.Text = "   &Xem";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // txtTenBenhNhan
            // 
            this.txtTenBenhNhan.Location = new System.Drawing.Point(95, 33);
            this.txtTenBenhNhan.Name = "txtTenBenhNhan";
            this.txtTenBenhNhan.ReadOnly = true;
            this.txtTenBenhNhan.Size = new System.Drawing.Size(277, 20);
            this.txtTenBenhNhan.TabIndex = 20;
            // 
            // raTenBenhNhan
            // 
            this.raTenBenhNhan.AutoSize = true;
            this.raTenBenhNhan.Location = new System.Drawing.Point(13, 34);
            this.raTenBenhNhan.Name = "raTenBenhNhan";
            this.raTenBenhNhan.Size = new System.Drawing.Size(82, 17);
            this.raTenBenhNhan.TabIndex = 19;
            this.raTenBenhNhan.Text = "Tên công ty";
            this.raTenBenhNhan.UseVisualStyleBackColor = true;
            // 
            // dtpkDenNgay
            // 
            this.dtpkDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkDenNgay.Location = new System.Drawing.Point(259, 9);
            this.dtpkDenNgay.Name = "dtpkDenNgay";
            this.dtpkDenNgay.Size = new System.Drawing.Size(113, 20);
            this.dtpkDenNgay.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(202, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "đến ngày";
            // 
            // dtpkTuNgay
            // 
            this.dtpkTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkTuNgay.Location = new System.Drawing.Point(81, 9);
            this.dtpkTuNgay.Name = "dtpkTuNgay";
            this.dtpkTuNgay.Size = new System.Drawing.Size(113, 20);
            this.dtpkTuNgay.TabIndex = 16;
            // 
            // raTuNgayToiNgay
            // 
            this.raTuNgayToiNgay.AutoSize = true;
            this.raTuNgayToiNgay.Checked = true;
            this.raTuNgayToiNgay.Location = new System.Drawing.Point(13, 10);
            this.raTuNgayToiNgay.Name = "raTuNgayToiNgay";
            this.raTuNgayToiNgay.Size = new System.Drawing.Size(64, 17);
            this.raTuNgayToiNgay.TabIndex = 15;
            this.raTuNgayToiNgay.TabStop = true;
            this.raTuNgayToiNgay.Text = "Từ ngày";
            this.raTuNgayToiNgay.UseVisualStyleBackColor = true;
            this.raTuNgayToiNgay.CheckedChanged += new System.EventHandler(this.raTuNgayToiNgay_CheckedChanged);
            // 
            // uNhatKyLienHeCongTy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uNhatKyLienHeCongTy";
            this.Size = new System.Drawing.Size(800, 444);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgNhatKyLienHeCongTy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nhatKyLienHeCongTyViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgNhatKyLienHeCongTy;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayGioLienHeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn congTyLienHeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noiDungLienHeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource nhatKyLienHeCongTyViewBindingSource;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.TextBox txtTenBenhNhan;
        private System.Windows.Forms.RadioButton raTenBenhNhan;
        private System.Windows.Forms.DateTimePicker dtpkDenNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpkTuNgay;
        private System.Windows.Forms.RadioButton raTuNgayToiNgay;
    }
}
