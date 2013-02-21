namespace MM.Controls
{
    partial class uKetQuaCanLamSangList
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgKetQuaCanLamSang = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ngayKhamDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.normalDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.abnormalDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.negativeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.positiveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.noteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ketQuaCanLamSangViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pFilter = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpkToDate = new System.Windows.Forms.DateTimePicker();
            this.lbToDate = new System.Windows.Forms.Label();
            this.dtpkFromDate = new System.Windows.Forms.DateTimePicker();
            this.raFromDateToDate = new System.Windows.Forms.RadioButton();
            this.raAll = new System.Windows.Forms.RadioButton();
            this._printDialog = new System.Windows.Forms.PrintDialog();
            this.ctmAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgKetQuaCanLamSang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ketQuaCanLamSangViewBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.pFilter.SuspendLayout();
            this.ctmAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.chkChecked);
            this.panel3.Controls.Add(this.dgKetQuaCanLamSang);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 60);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1020, 483);
            this.panel3.TabIndex = 2;
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(44, 5);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 2;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgKetQuaCanLamSang
            // 
            this.dgKetQuaCanLamSang.AllowUserToAddRows = false;
            this.dgKetQuaCanLamSang.AllowUserToDeleteRows = false;
            this.dgKetQuaCanLamSang.AllowUserToOrderColumns = true;
            this.dgKetQuaCanLamSang.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgKetQuaCanLamSang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgKetQuaCanLamSang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgKetQuaCanLamSang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.ngayKhamDataGridViewTextBoxColumn,
            this.codeDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.normalDataGridViewCheckBoxColumn,
            this.abnormalDataGridViewCheckBoxColumn,
            this.negativeDataGridViewCheckBoxColumn,
            this.positiveDataGridViewCheckBoxColumn,
            this.noteDataGridViewTextBoxColumn});
            this.dgKetQuaCanLamSang.ContextMenuStrip = this.ctmAction;
            this.dgKetQuaCanLamSang.DataSource = this.ketQuaCanLamSangViewBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgKetQuaCanLamSang.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgKetQuaCanLamSang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgKetQuaCanLamSang.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgKetQuaCanLamSang.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgKetQuaCanLamSang.HighlightSelectedColumnHeaders = false;
            this.dgKetQuaCanLamSang.Location = new System.Drawing.Point(0, 0);
            this.dgKetQuaCanLamSang.MultiSelect = false;
            this.dgKetQuaCanLamSang.Name = "dgKetQuaCanLamSang";
            this.dgKetQuaCanLamSang.RowHeadersWidth = 30;
            this.dgKetQuaCanLamSang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgKetQuaCanLamSang.Size = new System.Drawing.Size(1016, 479);
            this.dgKetQuaCanLamSang.TabIndex = 1;
            this.dgKetQuaCanLamSang.DoubleClick += new System.EventHandler(this.dgServiceHistory_DoubleClick);
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
            // ngayKhamDataGridViewTextBoxColumn
            // 
            this.ngayKhamDataGridViewTextBoxColumn.DataPropertyName = "NgayKham";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            dataGridViewCellStyle2.NullValue = null;
            this.ngayKhamDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.ngayKhamDataGridViewTextBoxColumn.HeaderText = "Ngày khám";
            this.ngayKhamDataGridViewTextBoxColumn.Name = "ngayKhamDataGridViewTextBoxColumn";
            this.ngayKhamDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // codeDataGridViewTextBoxColumn
            // 
            this.codeDataGridViewTextBoxColumn.DataPropertyName = "Code";
            this.codeDataGridViewTextBoxColumn.HeaderText = "Mã DV";
            this.codeDataGridViewTextBoxColumn.Name = "codeDataGridViewTextBoxColumn";
            this.codeDataGridViewTextBoxColumn.ReadOnly = true;
            this.codeDataGridViewTextBoxColumn.Width = 120;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Tên DV";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 250;
            // 
            // normalDataGridViewCheckBoxColumn
            // 
            this.normalDataGridViewCheckBoxColumn.DataPropertyName = "Normal";
            this.normalDataGridViewCheckBoxColumn.HeaderText = "Bình thường";
            this.normalDataGridViewCheckBoxColumn.Name = "normalDataGridViewCheckBoxColumn";
            this.normalDataGridViewCheckBoxColumn.ReadOnly = true;
            this.normalDataGridViewCheckBoxColumn.Width = 80;
            // 
            // abnormalDataGridViewCheckBoxColumn
            // 
            this.abnormalDataGridViewCheckBoxColumn.DataPropertyName = "Abnormal";
            this.abnormalDataGridViewCheckBoxColumn.HeaderText = "Bất thường";
            this.abnormalDataGridViewCheckBoxColumn.Name = "abnormalDataGridViewCheckBoxColumn";
            this.abnormalDataGridViewCheckBoxColumn.ReadOnly = true;
            this.abnormalDataGridViewCheckBoxColumn.Width = 80;
            // 
            // negativeDataGridViewCheckBoxColumn
            // 
            this.negativeDataGridViewCheckBoxColumn.DataPropertyName = "Negative";
            this.negativeDataGridViewCheckBoxColumn.HeaderText = "Âm tính";
            this.negativeDataGridViewCheckBoxColumn.Name = "negativeDataGridViewCheckBoxColumn";
            this.negativeDataGridViewCheckBoxColumn.ReadOnly = true;
            this.negativeDataGridViewCheckBoxColumn.Width = 80;
            // 
            // positiveDataGridViewCheckBoxColumn
            // 
            this.positiveDataGridViewCheckBoxColumn.DataPropertyName = "Positive";
            this.positiveDataGridViewCheckBoxColumn.HeaderText = "Dương tính";
            this.positiveDataGridViewCheckBoxColumn.Name = "positiveDataGridViewCheckBoxColumn";
            this.positiveDataGridViewCheckBoxColumn.ReadOnly = true;
            this.positiveDataGridViewCheckBoxColumn.Width = 80;
            // 
            // noteDataGridViewTextBoxColumn
            // 
            this.noteDataGridViewTextBoxColumn.DataPropertyName = "Note";
            this.noteDataGridViewTextBoxColumn.HeaderText = "Nhận xét";
            this.noteDataGridViewTextBoxColumn.Name = "noteDataGridViewTextBoxColumn";
            this.noteDataGridViewTextBoxColumn.ReadOnly = true;
            this.noteDataGridViewTextBoxColumn.Width = 250;
            // 
            // ketQuaCanLamSangViewBindingSource
            // 
            this.ketQuaCanLamSangViewBindingSource.DataSource = typeof(MM.Databasae.KetQuaCanLamSangView);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 543);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1020, 37);
            this.panel2.TabIndex = 10;
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
            // pFilter
            // 
            this.pFilter.Controls.Add(this.btnSearch);
            this.pFilter.Controls.Add(this.dtpkToDate);
            this.pFilter.Controls.Add(this.lbToDate);
            this.pFilter.Controls.Add(this.dtpkFromDate);
            this.pFilter.Controls.Add(this.raFromDateToDate);
            this.pFilter.Controls.Add(this.raAll);
            this.pFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pFilter.Location = new System.Drawing.Point(0, 0);
            this.pFilter.Name = "pFilter";
            this.pFilter.Size = new System.Drawing.Size(1020, 60);
            this.pFilter.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::MM.Properties.Resources.viewalldie;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(339, 30);
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
            this.dtpkToDate.Enabled = false;
            this.dtpkToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkToDate.Location = new System.Drawing.Point(237, 30);
            this.dtpkToDate.Name = "dtpkToDate";
            this.dtpkToDate.Size = new System.Drawing.Size(96, 20);
            this.dtpkToDate.TabIndex = 4;
            // 
            // lbToDate
            // 
            this.lbToDate.AutoSize = true;
            this.lbToDate.Location = new System.Drawing.Point(179, 34);
            this.lbToDate.Name = "lbToDate";
            this.lbToDate.Size = new System.Drawing.Size(52, 13);
            this.lbToDate.TabIndex = 3;
            this.lbToDate.Text = "đến ngày";
            // 
            // dtpkFromDate
            // 
            this.dtpkFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpkFromDate.Enabled = false;
            this.dtpkFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkFromDate.Location = new System.Drawing.Point(79, 30);
            this.dtpkFromDate.Name = "dtpkFromDate";
            this.dtpkFromDate.Size = new System.Drawing.Size(96, 20);
            this.dtpkFromDate.TabIndex = 2;
            // 
            // raFromDateToDate
            // 
            this.raFromDateToDate.AutoSize = true;
            this.raFromDateToDate.Location = new System.Drawing.Point(13, 31);
            this.raFromDateToDate.Name = "raFromDateToDate";
            this.raFromDateToDate.Size = new System.Drawing.Size(64, 17);
            this.raFromDateToDate.TabIndex = 1;
            this.raFromDateToDate.Text = "Từ ngày";
            this.raFromDateToDate.UseVisualStyleBackColor = true;
            // 
            // raAll
            // 
            this.raAll.AutoSize = true;
            this.raAll.Checked = true;
            this.raAll.Location = new System.Drawing.Point(13, 9);
            this.raAll.Name = "raAll";
            this.raAll.Size = new System.Drawing.Size(56, 17);
            this.raAll.TabIndex = 0;
            this.raAll.TabStop = true;
            this.raAll.Text = "Tất cả";
            this.raAll.UseVisualStyleBackColor = true;
            this.raAll.CheckedChanged += new System.EventHandler(this.raAll_CheckedChanged);
            // 
            // _printDialog
            // 
            this._printDialog.UseEXDialog = true;
            // 
            // ctmAction
            // 
            this.ctmAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.toolStripSeparator1,
            this.editToolStripMenuItem,
            this.toolStripSeparator2,
            this.deleteToolStripMenuItem});
            this.ctmAction.Name = "cmtAction";
            this.ctmAction.Size = new System.Drawing.Size(153, 104);
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
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::MM.Properties.Resources.edit;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editToolStripMenuItem.Text = "Sửa";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::MM.Properties.Resources.del;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "Xóa";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // uKetQuaCanLamSangList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pFilter);
            this.Name = "uKetQuaCanLamSangList";
            this.Size = new System.Drawing.Size(1020, 580);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgKetQuaCanLamSang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ketQuaCanLamSangViewBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.pFilter.ResumeLayout(false);
            this.pFilter.PerformLayout();
            this.ctmAction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pFilter;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DateTimePicker dtpkToDate;
        private System.Windows.Forms.Label lbToDate;
        private System.Windows.Forms.DateTimePicker dtpkFromDate;
        private System.Windows.Forms.RadioButton raFromDateToDate;
        private System.Windows.Forms.RadioButton raAll;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgKetQuaCanLamSang;
        private System.Windows.Forms.CheckBox chkChecked;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdbyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.PrintDialog _printDialog;
        private System.Windows.Forms.BindingSource ketQuaCanLamSangViewBindingSource;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayKhamDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn normalDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn abnormalDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn negativeDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn positiveDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noteDataGridViewTextBoxColumn;
        protected System.Windows.Forms.ContextMenuStrip ctmAction;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}
