namespace MM.Controls
{
    partial class uKetLuanList
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
            this.pFilter = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpkToDate = new System.Windows.Forms.DateTimePicker();
            this.lbToDate = new System.Windows.Forms.Label();
            this.dtpkFromDate = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgKetLuan = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.ketLuanViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colChecked = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.ngayKetLuanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hasLamThemXetNghiemDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cacXetNghiemLamThemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hasLamDuCanLamSangDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lyDoCanLamSangDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hasDuSucKhoeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lyDoSucKhoeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pFilter.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgKetLuan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ketLuanViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pFilter
            // 
            this.pFilter.Controls.Add(this.label1);
            this.pFilter.Controls.Add(this.btnSearch);
            this.pFilter.Controls.Add(this.dtpkToDate);
            this.pFilter.Controls.Add(this.lbToDate);
            this.pFilter.Controls.Add(this.dtpkFromDate);
            this.pFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pFilter.Location = new System.Drawing.Point(0, 0);
            this.pFilter.Name = "pFilter";
            this.pFilter.Size = new System.Drawing.Size(1060, 42);
            this.pFilter.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Từ ngày";
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::MM.Properties.Resources.viewalldie;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(325, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(64, 21);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "    &Tìm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpkToDate
            // 
            this.dtpkToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpkToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkToDate.Location = new System.Drawing.Point(223, 10);
            this.dtpkToDate.Name = "dtpkToDate";
            this.dtpkToDate.Size = new System.Drawing.Size(96, 20);
            this.dtpkToDate.TabIndex = 4;
            // 
            // lbToDate
            // 
            this.lbToDate.AutoSize = true;
            this.lbToDate.Location = new System.Drawing.Point(165, 14);
            this.lbToDate.Name = "lbToDate";
            this.lbToDate.Size = new System.Drawing.Size(52, 13);
            this.lbToDate.TabIndex = 3;
            this.lbToDate.Text = "đến ngày";
            // 
            // dtpkFromDate
            // 
            this.dtpkFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpkFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkFromDate.Location = new System.Drawing.Point(65, 10);
            this.dtpkFromDate.Name = "dtpkFromDate";
            this.dtpkFromDate.Size = new System.Drawing.Size(96, 20);
            this.dtpkFromDate.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 431);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1060, 37);
            this.panel2.TabIndex = 12;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.chkChecked);
            this.panel1.Controls.Add(this.dgKetLuan);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1060, 389);
            this.panel1.TabIndex = 13;
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(45, 5);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 6;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgKetLuan
            // 
            this.dgKetLuan.AllowUserToAddRows = false;
            this.dgKetLuan.AllowUserToDeleteRows = false;
            this.dgKetLuan.AllowUserToOrderColumns = true;
            this.dgKetLuan.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgKetLuan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgKetLuan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgKetLuan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.ngayKetLuanDataGridViewTextBoxColumn,
            this.fullNameDataGridViewTextBoxColumn,
            this.hasLamThemXetNghiemDataGridViewCheckBoxColumn,
            this.cacXetNghiemLamThemDataGridViewTextBoxColumn,
            this.hasLamDuCanLamSangDataGridViewCheckBoxColumn,
            this.lyDoCanLamSangDataGridViewTextBoxColumn,
            this.hasDuSucKhoeDataGridViewCheckBoxColumn,
            this.lyDoSucKhoeDataGridViewTextBoxColumn});
            this.dgKetLuan.DataSource = this.ketLuanViewBindingSource;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgKetLuan.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgKetLuan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgKetLuan.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgKetLuan.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgKetLuan.HighlightSelectedColumnHeaders = false;
            this.dgKetLuan.Location = new System.Drawing.Point(0, 0);
            this.dgKetLuan.MultiSelect = false;
            this.dgKetLuan.Name = "dgKetLuan";
            this.dgKetLuan.ReadOnly = true;
            this.dgKetLuan.RowHeadersWidth = 30;
            this.dgKetLuan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgKetLuan.Size = new System.Drawing.Size(1060, 389);
            this.dgKetLuan.TabIndex = 5;
            this.dgKetLuan.DoubleClick += new System.EventHandler(this.dgKetLuan_DoubleClick);
            // 
            // ketLuanViewBindingSource
            // 
            this.ketLuanViewBindingSource.DataSource = typeof(MM.Databasae.KetLuanView);
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
            // ngayKetLuanDataGridViewTextBoxColumn
            // 
            this.ngayKetLuanDataGridViewTextBoxColumn.DataPropertyName = "NgayKetLuan";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle3.NullValue = null;
            this.ngayKetLuanDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.ngayKetLuanDataGridViewTextBoxColumn.HeaderText = "Ngày kết luận";
            this.ngayKetLuanDataGridViewTextBoxColumn.Name = "ngayKetLuanDataGridViewTextBoxColumn";
            this.ngayKetLuanDataGridViewTextBoxColumn.ReadOnly = true;
            this.ngayKetLuanDataGridViewTextBoxColumn.Width = 120;
            // 
            // fullNameDataGridViewTextBoxColumn
            // 
            this.fullNameDataGridViewTextBoxColumn.DataPropertyName = "FullName";
            this.fullNameDataGridViewTextBoxColumn.HeaderText = "Bác sĩ";
            this.fullNameDataGridViewTextBoxColumn.Name = "fullNameDataGridViewTextBoxColumn";
            this.fullNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fullNameDataGridViewTextBoxColumn.Width = 200;
            // 
            // hasLamThemXetNghiemDataGridViewCheckBoxColumn
            // 
            this.hasLamThemXetNghiemDataGridViewCheckBoxColumn.DataPropertyName = "HasLamThemXetNghiem";
            this.hasLamThemXetNghiemDataGridViewCheckBoxColumn.HeaderText = "Làm thêm xét nghiệm";
            this.hasLamThemXetNghiemDataGridViewCheckBoxColumn.Name = "hasLamThemXetNghiemDataGridViewCheckBoxColumn";
            this.hasLamThemXetNghiemDataGridViewCheckBoxColumn.ReadOnly = true;
            this.hasLamThemXetNghiemDataGridViewCheckBoxColumn.Width = 130;
            // 
            // cacXetNghiemLamThemDataGridViewTextBoxColumn
            // 
            this.cacXetNghiemLamThemDataGridViewTextBoxColumn.DataPropertyName = "CacXetNghiemLamThem";
            this.cacXetNghiemLamThemDataGridViewTextBoxColumn.HeaderText = "Các xét nghiệm làm thêm";
            this.cacXetNghiemLamThemDataGridViewTextBoxColumn.Name = "cacXetNghiemLamThemDataGridViewTextBoxColumn";
            this.cacXetNghiemLamThemDataGridViewTextBoxColumn.ReadOnly = true;
            this.cacXetNghiemLamThemDataGridViewTextBoxColumn.Width = 200;
            // 
            // hasLamDuCanLamSangDataGridViewCheckBoxColumn
            // 
            this.hasLamDuCanLamSangDataGridViewCheckBoxColumn.DataPropertyName = "HasLamDuCanLamSang";
            this.hasLamDuCanLamSangDataGridViewCheckBoxColumn.HeaderText = "Đã làm đủ cận lâm sàng";
            this.hasLamDuCanLamSangDataGridViewCheckBoxColumn.Name = "hasLamDuCanLamSangDataGridViewCheckBoxColumn";
            this.hasLamDuCanLamSangDataGridViewCheckBoxColumn.ReadOnly = true;
            this.hasLamDuCanLamSangDataGridViewCheckBoxColumn.Width = 140;
            // 
            // lyDoCanLamSangDataGridViewTextBoxColumn
            // 
            this.lyDoCanLamSangDataGridViewTextBoxColumn.DataPropertyName = "LyDo_CanLamSang";
            this.lyDoCanLamSangDataGridViewTextBoxColumn.HeaderText = "Lý do";
            this.lyDoCanLamSangDataGridViewTextBoxColumn.Name = "lyDoCanLamSangDataGridViewTextBoxColumn";
            this.lyDoCanLamSangDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // hasDuSucKhoeDataGridViewCheckBoxColumn
            // 
            this.hasDuSucKhoeDataGridViewCheckBoxColumn.DataPropertyName = "HasDuSucKhoe";
            this.hasDuSucKhoeDataGridViewCheckBoxColumn.HeaderText = "Đủ sức khỏe làm việc";
            this.hasDuSucKhoeDataGridViewCheckBoxColumn.Name = "hasDuSucKhoeDataGridViewCheckBoxColumn";
            this.hasDuSucKhoeDataGridViewCheckBoxColumn.ReadOnly = true;
            this.hasDuSucKhoeDataGridViewCheckBoxColumn.Width = 140;
            // 
            // lyDoSucKhoeDataGridViewTextBoxColumn
            // 
            this.lyDoSucKhoeDataGridViewTextBoxColumn.DataPropertyName = "LyDo_SucKhoe";
            this.lyDoSucKhoeDataGridViewTextBoxColumn.HeaderText = "Loại";
            this.lyDoSucKhoeDataGridViewTextBoxColumn.Name = "lyDoSucKhoeDataGridViewTextBoxColumn";
            this.lyDoSucKhoeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // uKetLuanList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pFilter);
            this.Name = "uKetLuanList";
            this.Size = new System.Drawing.Size(1060, 468);
            this.pFilter.ResumeLayout(false);
            this.pFilter.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgKetLuan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ketLuanViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtpkToDate;
        private System.Windows.Forms.Label lbToDate;
        private System.Windows.Forms.DateTimePicker dtpkFromDate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgKetLuan;
        private System.Windows.Forms.BindingSource ketLuanViewBindingSource;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayKetLuanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn hasLamThemXetNghiemDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cacXetNghiemLamThemDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn hasLamDuCanLamSangDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lyDoCanLamSangDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn hasDuSucKhoeDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lyDoSucKhoeDataGridViewTextBoxColumn;
    }
}
