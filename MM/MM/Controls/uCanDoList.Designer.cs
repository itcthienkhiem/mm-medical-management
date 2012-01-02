namespace MM.Controls
{
    partial class uCanDoList
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
            this.dgCanDo = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.ngayCanDoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timMachDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.huyetApDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hoHapDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chieuCaoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.canNangDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bMIDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.canDoKhacDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.canDoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pFilter.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCanDo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.canDoBindingSource)).BeginInit();
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
            this.pFilter.Size = new System.Drawing.Size(734, 42);
            this.pFilter.TabIndex = 1;
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
            this.panel2.Location = new System.Drawing.Point(0, 367);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(734, 37);
            this.panel2.TabIndex = 11;
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
            this.panel1.Controls.Add(this.dgCanDo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(734, 325);
            this.panel1.TabIndex = 12;
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(45, 5);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 4;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgCanDo
            // 
            this.dgCanDo.AllowUserToAddRows = false;
            this.dgCanDo.AllowUserToDeleteRows = false;
            this.dgCanDo.AllowUserToOrderColumns = true;
            this.dgCanDo.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCanDo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgCanDo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCanDo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.ngayCanDoDataGridViewTextBoxColumn,
            this.timMachDataGridViewTextBoxColumn,
            this.huyetApDataGridViewTextBoxColumn,
            this.hoHapDataGridViewTextBoxColumn,
            this.chieuCaoDataGridViewTextBoxColumn,
            this.canNangDataGridViewTextBoxColumn,
            this.bMIDataGridViewTextBoxColumn,
            this.canDoKhacDataGridViewTextBoxColumn});
            this.dgCanDo.DataSource = this.canDoBindingSource;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgCanDo.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgCanDo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgCanDo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgCanDo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgCanDo.HighlightSelectedColumnHeaders = false;
            this.dgCanDo.Location = new System.Drawing.Point(0, 0);
            this.dgCanDo.MultiSelect = false;
            this.dgCanDo.Name = "dgCanDo";
            this.dgCanDo.ReadOnly = true;
            this.dgCanDo.RowHeadersWidth = 30;
            this.dgCanDo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCanDo.Size = new System.Drawing.Size(734, 325);
            this.dgCanDo.TabIndex = 3;
            this.dgCanDo.DoubleClick += new System.EventHandler(this.dgCanDo_DoubleClick);
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
            // ngayCanDoDataGridViewTextBoxColumn
            // 
            this.ngayCanDoDataGridViewTextBoxColumn.DataPropertyName = "NgayCanDo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "dd/MM/yyyy HH:mm:ss";
            this.ngayCanDoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.ngayCanDoDataGridViewTextBoxColumn.HeaderText = "Ngày cân đo";
            this.ngayCanDoDataGridViewTextBoxColumn.Name = "ngayCanDoDataGridViewTextBoxColumn";
            this.ngayCanDoDataGridViewTextBoxColumn.ReadOnly = true;
            this.ngayCanDoDataGridViewTextBoxColumn.Width = 120;
            // 
            // timMachDataGridViewTextBoxColumn
            // 
            this.timMachDataGridViewTextBoxColumn.DataPropertyName = "TimMach";
            this.timMachDataGridViewTextBoxColumn.HeaderText = "Tim mạch";
            this.timMachDataGridViewTextBoxColumn.Name = "timMachDataGridViewTextBoxColumn";
            this.timMachDataGridViewTextBoxColumn.ReadOnly = true;
            this.timMachDataGridViewTextBoxColumn.Width = 120;
            // 
            // huyetApDataGridViewTextBoxColumn
            // 
            this.huyetApDataGridViewTextBoxColumn.DataPropertyName = "HuyetAp";
            this.huyetApDataGridViewTextBoxColumn.HeaderText = "Huyết áp";
            this.huyetApDataGridViewTextBoxColumn.Name = "huyetApDataGridViewTextBoxColumn";
            this.huyetApDataGridViewTextBoxColumn.ReadOnly = true;
            this.huyetApDataGridViewTextBoxColumn.Width = 120;
            // 
            // hoHapDataGridViewTextBoxColumn
            // 
            this.hoHapDataGridViewTextBoxColumn.DataPropertyName = "HoHap";
            this.hoHapDataGridViewTextBoxColumn.HeaderText = "Hô hấp";
            this.hoHapDataGridViewTextBoxColumn.Name = "hoHapDataGridViewTextBoxColumn";
            this.hoHapDataGridViewTextBoxColumn.ReadOnly = true;
            this.hoHapDataGridViewTextBoxColumn.Width = 120;
            // 
            // chieuCaoDataGridViewTextBoxColumn
            // 
            this.chieuCaoDataGridViewTextBoxColumn.DataPropertyName = "ChieuCao";
            this.chieuCaoDataGridViewTextBoxColumn.HeaderText = "Chiều cao";
            this.chieuCaoDataGridViewTextBoxColumn.Name = "chieuCaoDataGridViewTextBoxColumn";
            this.chieuCaoDataGridViewTextBoxColumn.ReadOnly = true;
            this.chieuCaoDataGridViewTextBoxColumn.Width = 120;
            // 
            // canNangDataGridViewTextBoxColumn
            // 
            this.canNangDataGridViewTextBoxColumn.DataPropertyName = "CanNang";
            this.canNangDataGridViewTextBoxColumn.HeaderText = "Cân nặng";
            this.canNangDataGridViewTextBoxColumn.Name = "canNangDataGridViewTextBoxColumn";
            this.canNangDataGridViewTextBoxColumn.ReadOnly = true;
            this.canNangDataGridViewTextBoxColumn.Width = 120;
            // 
            // bMIDataGridViewTextBoxColumn
            // 
            this.bMIDataGridViewTextBoxColumn.DataPropertyName = "BMI";
            this.bMIDataGridViewTextBoxColumn.HeaderText = "BMI";
            this.bMIDataGridViewTextBoxColumn.Name = "bMIDataGridViewTextBoxColumn";
            this.bMIDataGridViewTextBoxColumn.ReadOnly = true;
            this.bMIDataGridViewTextBoxColumn.Width = 120;
            // 
            // canDoKhacDataGridViewTextBoxColumn
            // 
            this.canDoKhacDataGridViewTextBoxColumn.DataPropertyName = "CanDoKhac";
            this.canDoKhacDataGridViewTextBoxColumn.HeaderText = "Cân đo khác";
            this.canDoKhacDataGridViewTextBoxColumn.Name = "canDoKhacDataGridViewTextBoxColumn";
            this.canDoKhacDataGridViewTextBoxColumn.ReadOnly = true;
            this.canDoKhacDataGridViewTextBoxColumn.Width = 120;
            // 
            // canDoBindingSource
            // 
            this.canDoBindingSource.DataSource = typeof(MM.Databasae.CanDo);
            // 
            // uCanDoList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pFilter);
            this.Name = "uCanDoList";
            this.Size = new System.Drawing.Size(734, 404);
            this.pFilter.ResumeLayout(false);
            this.pFilter.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCanDo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.canDoBindingSource)).EndInit();
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
        private DevComponents.DotNetBar.Controls.DataGridViewX dgCanDo;
        private System.Windows.Forms.BindingSource canDoBindingSource;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayCanDoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timMachDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn huyetApDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hoHapDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn chieuCaoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn canNangDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bMIDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn canDoKhacDataGridViewTextBoxColumn;
    }
}
