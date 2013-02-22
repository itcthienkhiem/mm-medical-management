namespace MM.Controls
{
    partial class uXuatKhoCapCuuList
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
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgNhapKhoCapCuu = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ngayXuatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenCapCuuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soLuongDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xuatKhoCapCuuViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtpkDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpkTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTenCapCuu = new System.Windows.Forms.TextBox();
            this.raTuNgayDenNgay = new System.Windows.Forms.RadioButton();
            this.raTenCapCuu = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ctmAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgNhapKhoCapCuu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xuatKhoCapCuuViewBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.ctmAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDelete);
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
            this.ngayXuatDataGridViewTextBoxColumn,
            this.tenCapCuuDataGridViewTextBoxColumn,
            this.soLuongDataGridViewTextBoxColumn,
            this.noteDataGridViewTextBoxColumn});
            this.dgNhapKhoCapCuu.ContextMenuStrip = this.ctmAction;
            this.dgNhapKhoCapCuu.DataSource = this.xuatKhoCapCuuViewBindingSource;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgNhapKhoCapCuu.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgNhapKhoCapCuu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgNhapKhoCapCuu.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgNhapKhoCapCuu.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgNhapKhoCapCuu.HighlightSelectedColumnHeaders = false;
            this.dgNhapKhoCapCuu.Location = new System.Drawing.Point(0, 0);
            this.dgNhapKhoCapCuu.MultiSelect = false;
            this.dgNhapKhoCapCuu.Name = "dgNhapKhoCapCuu";
            this.dgNhapKhoCapCuu.RowHeadersWidth = 30;
            this.dgNhapKhoCapCuu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgNhapKhoCapCuu.Size = new System.Drawing.Size(973, 336);
            this.dgNhapKhoCapCuu.TabIndex = 4;
            // 
            // colChecked
            // 
            this.colChecked.DataPropertyName = "Checked";
            this.colChecked.HeaderText = "";
            this.colChecked.Name = "colChecked";
            this.colChecked.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colChecked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colChecked.Width = 40;
            // 
            // ngayXuatDataGridViewTextBoxColumn
            // 
            this.ngayXuatDataGridViewTextBoxColumn.DataPropertyName = "NgayXuat";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle2.NullValue = null;
            this.ngayXuatDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.ngayXuatDataGridViewTextBoxColumn.HeaderText = "Ngày xuất";
            this.ngayXuatDataGridViewTextBoxColumn.Name = "ngayXuatDataGridViewTextBoxColumn";
            this.ngayXuatDataGridViewTextBoxColumn.ReadOnly = true;
            this.ngayXuatDataGridViewTextBoxColumn.Width = 120;
            // 
            // tenCapCuuDataGridViewTextBoxColumn
            // 
            this.tenCapCuuDataGridViewTextBoxColumn.DataPropertyName = "TenCapCuu";
            this.tenCapCuuDataGridViewTextBoxColumn.HeaderText = "Tên cấp cứu";
            this.tenCapCuuDataGridViewTextBoxColumn.Name = "tenCapCuuDataGridViewTextBoxColumn";
            this.tenCapCuuDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenCapCuuDataGridViewTextBoxColumn.Width = 200;
            // 
            // soLuongDataGridViewTextBoxColumn
            // 
            this.soLuongDataGridViewTextBoxColumn.DataPropertyName = "SoLuong";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.soLuongDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.soLuongDataGridViewTextBoxColumn.HeaderText = "Số lượng xuất";
            this.soLuongDataGridViewTextBoxColumn.Name = "soLuongDataGridViewTextBoxColumn";
            this.soLuongDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // noteDataGridViewTextBoxColumn
            // 
            this.noteDataGridViewTextBoxColumn.DataPropertyName = "Note";
            this.noteDataGridViewTextBoxColumn.HeaderText = "Ghi chú";
            this.noteDataGridViewTextBoxColumn.Name = "noteDataGridViewTextBoxColumn";
            this.noteDataGridViewTextBoxColumn.ReadOnly = true;
            this.noteDataGridViewTextBoxColumn.Width = 250;
            // 
            // xuatKhoCapCuuViewBindingSource
            // 
            this.xuatKhoCapCuuViewBindingSource.DataSource = typeof(MM.Databasae.XuatKhoCapCuuView);
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
            // ctmAction
            // 
            this.ctmAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.toolStripSeparator1,
            this.deleteToolStripMenuItem});
            this.ctmAction.Name = "cmtAction";
            this.ctmAction.Size = new System.Drawing.Size(153, 76);
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
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::MM.Properties.Resources.del;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "Xóa";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // uXuatKhoCapCuuList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uXuatKhoCapCuuList";
            this.Size = new System.Drawing.Size(973, 438);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgNhapKhoCapCuu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xuatKhoCapCuuViewBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ctmAction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDelete;
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
        private System.Windows.Forms.BindingSource xuatKhoCapCuuViewBindingSource;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayXuatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenCapCuuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soLuongDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noteDataGridViewTextBoxColumn;
        protected System.Windows.Forms.ContextMenuStrip ctmAction;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}
