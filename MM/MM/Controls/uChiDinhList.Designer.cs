namespace MM.Controls
{
    partial class uChiDinhList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.chkChiTietChecked = new System.Windows.Forms.CheckBox();
            this.dgChiTiet = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.ChiTietChiDinhChecked = new MM.Controls.DataGridViewDisableCheckBoxColumn();
            this.codeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chiTietChiDinhViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgChiDinh = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Checked = new MM.Controls.DataGridViewDisableCheckBoxColumn();
            this.maChiDinhDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayChiDinhDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chiDinhViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgChiTiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chiTietChiDinhViewBindingSource)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgChiDinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chiDinhViewBindingSource)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnConfirm);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 467);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(804, 38);
            this.panel1.TabIndex = 2;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = global::MM.Properties.Resources.check;
            this.btnConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirm.Location = new System.Drawing.Point(343, 6);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(150, 25);
            this.btnConfirm.TabIndex = 3;
            this.btnConfirm.Text = "      &Xác nhận DV chỉ định";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::MM.Properties.Resources.del;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(229, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(110, 25);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "    &Xóa chỉ định";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::MM.Properties.Resources.edit;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(119, 6);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(106, 25);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "    &Sửa chỉ định";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::MM.Properties.Resources.add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(6, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(109, 25);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "    &Thêm chỉ định";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 287);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(804, 180);
            this.panel2.TabIndex = 3;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.chkChiTietChecked);
            this.panel7.Controls.Add(this.dgChiTiet);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 20);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(804, 160);
            this.panel7.TabIndex = 2;
            // 
            // chkChiTietChecked
            // 
            this.chkChiTietChecked.AutoSize = true;
            this.chkChiTietChecked.Location = new System.Drawing.Point(44, 5);
            this.chkChiTietChecked.Name = "chkChiTietChecked";
            this.chkChiTietChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChiTietChecked.TabIndex = 7;
            this.chkChiTietChecked.UseVisualStyleBackColor = true;
            this.chkChiTietChecked.CheckedChanged += new System.EventHandler(this.chkChiTietChecked_CheckedChanged);
            // 
            // dgChiTiet
            // 
            this.dgChiTiet.AllowUserToAddRows = false;
            this.dgChiTiet.AllowUserToDeleteRows = false;
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
            this.ChiTietChiDinhChecked,
            this.codeDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn});
            this.dgChiTiet.DataSource = this.chiTietChiDinhViewBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgChiTiet.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgChiTiet.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgChiTiet.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgChiTiet.HighlightSelectedColumnHeaders = false;
            this.dgChiTiet.Location = new System.Drawing.Point(0, 0);
            this.dgChiTiet.MultiSelect = false;
            this.dgChiTiet.Name = "dgChiTiet";
            this.dgChiTiet.RowHeadersWidth = 30;
            this.dgChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgChiTiet.Size = new System.Drawing.Size(804, 160);
            this.dgChiTiet.TabIndex = 6;
            this.dgChiTiet.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgChiTiet_ColumnHeaderMouseClick);
            // 
            // ChiTietChiDinhChecked
            // 
            this.ChiTietChiDinhChecked.DataPropertyName = "Checked";
            this.ChiTietChiDinhChecked.Frozen = true;
            this.ChiTietChiDinhChecked.HeaderText = "";
            this.ChiTietChiDinhChecked.Name = "ChiTietChiDinhChecked";
            this.ChiTietChiDinhChecked.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ChiTietChiDinhChecked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ChiTietChiDinhChecked.Width = 40;
            // 
            // codeDataGridViewTextBoxColumn
            // 
            this.codeDataGridViewTextBoxColumn.DataPropertyName = "Code";
            this.codeDataGridViewTextBoxColumn.HeaderText = "Mã dịch vụ";
            this.codeDataGridViewTextBoxColumn.Name = "codeDataGridViewTextBoxColumn";
            this.codeDataGridViewTextBoxColumn.ReadOnly = true;
            this.codeDataGridViewTextBoxColumn.Width = 120;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Tên dịch vụ";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 250;
            // 
            // chiTietChiDinhViewBindingSource
            // 
            this.chiTietChiDinhViewBindingSource.DataSource = typeof(MM.Databasae.ChiTietChiDinhView);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(804, 20);
            this.panel6.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(804, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Danh sách dịch vụ chỉ định";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(804, 287);
            this.panel3.TabIndex = 4;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.chkChecked);
            this.panel5.Controls.Add(this.dgChiDinh);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 20);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(804, 267);
            this.panel5.TabIndex = 1;
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
            // dgChiDinh
            // 
            this.dgChiDinh.AllowUserToAddRows = false;
            this.dgChiDinh.AllowUserToDeleteRows = false;
            this.dgChiDinh.AllowUserToOrderColumns = true;
            this.dgChiDinh.AutoGenerateColumns = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgChiDinh.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgChiDinh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgChiDinh.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Checked,
            this.maChiDinhDataGridViewTextBoxColumn,
            this.ngayChiDinhDataGridViewTextBoxColumn,
            this.fullNameDataGridViewTextBoxColumn});
            this.dgChiDinh.DataSource = this.chiDinhViewBindingSource;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgChiDinh.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgChiDinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgChiDinh.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgChiDinh.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgChiDinh.HighlightSelectedColumnHeaders = false;
            this.dgChiDinh.Location = new System.Drawing.Point(0, 0);
            this.dgChiDinh.MultiSelect = false;
            this.dgChiDinh.Name = "dgChiDinh";
            this.dgChiDinh.RowHeadersWidth = 30;
            this.dgChiDinh.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgChiDinh.Size = new System.Drawing.Size(804, 267);
            this.dgChiDinh.TabIndex = 4;
            this.dgChiDinh.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgChiDinh_ColumnHeaderMouseClick);
            this.dgChiDinh.SelectionChanged += new System.EventHandler(this.dgChiDinh_SelectionChanged);
            this.dgChiDinh.DoubleClick += new System.EventHandler(this.dgChiDinh_DoubleClick);
            // 
            // Checked
            // 
            this.Checked.DataPropertyName = "Checked";
            this.Checked.HeaderText = "";
            this.Checked.Name = "Checked";
            this.Checked.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Checked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Checked.Width = 40;
            // 
            // maChiDinhDataGridViewTextBoxColumn
            // 
            this.maChiDinhDataGridViewTextBoxColumn.DataPropertyName = "MaChiDinh";
            this.maChiDinhDataGridViewTextBoxColumn.HeaderText = "Mã chỉ định";
            this.maChiDinhDataGridViewTextBoxColumn.Name = "maChiDinhDataGridViewTextBoxColumn";
            this.maChiDinhDataGridViewTextBoxColumn.ReadOnly = true;
            this.maChiDinhDataGridViewTextBoxColumn.Width = 120;
            // 
            // ngayChiDinhDataGridViewTextBoxColumn
            // 
            this.ngayChiDinhDataGridViewTextBoxColumn.DataPropertyName = "NgayChiDinh";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle4.NullValue = null;
            this.ngayChiDinhDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.ngayChiDinhDataGridViewTextBoxColumn.HeaderText = "Ngày chỉ định";
            this.ngayChiDinhDataGridViewTextBoxColumn.Name = "ngayChiDinhDataGridViewTextBoxColumn";
            this.ngayChiDinhDataGridViewTextBoxColumn.ReadOnly = true;
            this.ngayChiDinhDataGridViewTextBoxColumn.Width = 140;
            // 
            // fullNameDataGridViewTextBoxColumn
            // 
            this.fullNameDataGridViewTextBoxColumn.DataPropertyName = "FullName";
            this.fullNameDataGridViewTextBoxColumn.HeaderText = "Bác sĩ chỉ định";
            this.fullNameDataGridViewTextBoxColumn.Name = "fullNameDataGridViewTextBoxColumn";
            this.fullNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fullNameDataGridViewTextBoxColumn.Width = 200;
            // 
            // chiDinhViewBindingSource
            // 
            this.chiDinhViewBindingSource.DataSource = typeof(MM.Databasae.ChiDinhView);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(804, 20);
            this.panel4.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(804, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Danh sách chỉ định";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uChiDinhList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uChiDinhList";
            this.Size = new System.Drawing.Size(804, 505);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgChiTiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chiTietChiDinhViewBindingSource)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgChiDinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chiDinhViewBindingSource)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgChiDinh;
        private System.Windows.Forms.CheckBox chkChiTietChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgChiTiet;
        private System.Windows.Forms.BindingSource chiTietChiDinhViewBindingSource;
        private System.Windows.Forms.BindingSource chiDinhViewBindingSource;
        private DataGridViewDisableCheckBoxColumn ChiTietChiDinhChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewDisableCheckBoxColumn Checked;
        private System.Windows.Forms.DataGridViewTextBoxColumn maChiDinhDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayChiDinhDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
    }
}
