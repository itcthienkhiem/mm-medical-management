﻿namespace MM.Controls
{
    partial class uToaCapCuuList
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpkToDate = new System.Windows.Forms.DateTimePicker();
            this.lbToDate = new System.Windows.Forms.Label();
            this.dtpkFromDate = new System.Windows.Forms.DateTimePicker();
            this.raFromDateToDate = new System.Windows.Forms.RadioButton();
            this.raAll = new System.Windows.Forms.RadioButton();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgToaCapCuu = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toaCapCuuViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colChecked = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.maToaCapCuuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayKeToaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bacSiKeToaGUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maBenhNhanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenBenhNhanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diaChiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenCongTyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdByDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updatedDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updatedByDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deletedDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deletedByDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgToaCapCuu)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toaCapCuuViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 431);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(839, 38);
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
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.dtpkToDate);
            this.panel2.Controls.Add(this.lbToDate);
            this.panel2.Controls.Add(this.dtpkFromDate);
            this.panel2.Controls.Add(this.raFromDateToDate);
            this.panel2.Controls.Add(this.raAll);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(839, 60);
            this.panel2.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::MM.Properties.Resources.viewalldie;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(340, 28);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(64, 21);
            this.btnSearch.TabIndex = 12;
            this.btnSearch.Text = "    &Tìm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpkToDate
            // 
            this.dtpkToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpkToDate.Enabled = false;
            this.dtpkToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkToDate.Location = new System.Drawing.Point(238, 28);
            this.dtpkToDate.Name = "dtpkToDate";
            this.dtpkToDate.Size = new System.Drawing.Size(96, 20);
            this.dtpkToDate.TabIndex = 11;
            // 
            // lbToDate
            // 
            this.lbToDate.AutoSize = true;
            this.lbToDate.Location = new System.Drawing.Point(180, 32);
            this.lbToDate.Name = "lbToDate";
            this.lbToDate.Size = new System.Drawing.Size(52, 13);
            this.lbToDate.TabIndex = 10;
            this.lbToDate.Text = "đến ngày";
            // 
            // dtpkFromDate
            // 
            this.dtpkFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpkFromDate.Enabled = false;
            this.dtpkFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkFromDate.Location = new System.Drawing.Point(80, 28);
            this.dtpkFromDate.Name = "dtpkFromDate";
            this.dtpkFromDate.Size = new System.Drawing.Size(96, 20);
            this.dtpkFromDate.TabIndex = 9;
            // 
            // raFromDateToDate
            // 
            this.raFromDateToDate.AutoSize = true;
            this.raFromDateToDate.Location = new System.Drawing.Point(13, 30);
            this.raFromDateToDate.Name = "raFromDateToDate";
            this.raFromDateToDate.Size = new System.Drawing.Size(64, 17);
            this.raFromDateToDate.TabIndex = 8;
            this.raFromDateToDate.Text = "Từ ngày";
            this.raFromDateToDate.UseVisualStyleBackColor = true;
            // 
            // raAll
            // 
            this.raAll.AutoSize = true;
            this.raAll.Checked = true;
            this.raAll.Location = new System.Drawing.Point(13, 8);
            this.raAll.Name = "raAll";
            this.raAll.Size = new System.Drawing.Size(56, 17);
            this.raAll.TabIndex = 7;
            this.raAll.TabStop = true;
            this.raAll.Text = "Tất cả";
            this.raAll.UseVisualStyleBackColor = true;
            this.raAll.CheckedChanged += new System.EventHandler(this.raAll_CheckedChanged);
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
            // dgToaCapCuu
            // 
            this.dgToaCapCuu.AllowUserToAddRows = false;
            this.dgToaCapCuu.AllowUserToDeleteRows = false;
            this.dgToaCapCuu.AllowUserToOrderColumns = true;
            this.dgToaCapCuu.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgToaCapCuu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgToaCapCuu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgToaCapCuu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.maToaCapCuuDataGridViewTextBoxColumn,
            this.ngayKeToaDataGridViewTextBoxColumn,
            this.fullNameDataGridViewTextBoxColumn,
            this.bacSiKeToaGUIDDataGridViewTextBoxColumn,
            this.maBenhNhanDataGridViewTextBoxColumn,
            this.tenBenhNhanDataGridViewTextBoxColumn,
            this.diaChiDataGridViewTextBoxColumn,
            this.tenCongTyDataGridViewTextBoxColumn,
            this.noteDataGridViewTextBoxColumn,
            this.createdDateDataGridViewTextBoxColumn,
            this.createdByDataGridViewTextBoxColumn,
            this.updatedDateDataGridViewTextBoxColumn,
            this.updatedByDataGridViewTextBoxColumn,
            this.deletedDateDataGridViewTextBoxColumn,
            this.deletedByDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn});
            this.dgToaCapCuu.DataSource = this.toaCapCuuViewBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgToaCapCuu.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgToaCapCuu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgToaCapCuu.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgToaCapCuu.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgToaCapCuu.HighlightSelectedColumnHeaders = false;
            this.dgToaCapCuu.Location = new System.Drawing.Point(0, 0);
            this.dgToaCapCuu.MultiSelect = false;
            this.dgToaCapCuu.Name = "dgToaCapCuu";
            this.dgToaCapCuu.ReadOnly = true;
            this.dgToaCapCuu.RowHeadersWidth = 30;
            this.dgToaCapCuu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgToaCapCuu.Size = new System.Drawing.Size(839, 371);
            this.dgToaCapCuu.TabIndex = 4;
            this.dgToaCapCuu.DoubleClick += new System.EventHandler(this.dgThuoc_DoubleClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkChecked);
            this.panel3.Controls.Add(this.dgToaCapCuu);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 60);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(839, 371);
            this.panel3.TabIndex = 4;
            // 
            // toaCapCuuViewBindingSource
            // 
            this.toaCapCuuViewBindingSource.DataSource = typeof(MM.Databasae.ToaCapCuuView);
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
            // maToaCapCuuDataGridViewTextBoxColumn
            // 
            this.maToaCapCuuDataGridViewTextBoxColumn.DataPropertyName = "MaToaCapCuu";
            this.maToaCapCuuDataGridViewTextBoxColumn.HeaderText = "MaToaCapCuu";
            this.maToaCapCuuDataGridViewTextBoxColumn.Name = "maToaCapCuuDataGridViewTextBoxColumn";
            this.maToaCapCuuDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ngayKeToaDataGridViewTextBoxColumn
            // 
            this.ngayKeToaDataGridViewTextBoxColumn.DataPropertyName = "NgayKeToa";
            this.ngayKeToaDataGridViewTextBoxColumn.HeaderText = "NgayKeToa";
            this.ngayKeToaDataGridViewTextBoxColumn.Name = "ngayKeToaDataGridViewTextBoxColumn";
            this.ngayKeToaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fullNameDataGridViewTextBoxColumn
            // 
            this.fullNameDataGridViewTextBoxColumn.DataPropertyName = "FullName";
            this.fullNameDataGridViewTextBoxColumn.HeaderText = "FullName";
            this.fullNameDataGridViewTextBoxColumn.Name = "fullNameDataGridViewTextBoxColumn";
            this.fullNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bacSiKeToaGUIDDataGridViewTextBoxColumn
            // 
            this.bacSiKeToaGUIDDataGridViewTextBoxColumn.DataPropertyName = "BacSiKeToaGUID";
            this.bacSiKeToaGUIDDataGridViewTextBoxColumn.HeaderText = "BacSiKeToaGUID";
            this.bacSiKeToaGUIDDataGridViewTextBoxColumn.Name = "bacSiKeToaGUIDDataGridViewTextBoxColumn";
            this.bacSiKeToaGUIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // maBenhNhanDataGridViewTextBoxColumn
            // 
            this.maBenhNhanDataGridViewTextBoxColumn.DataPropertyName = "MaBenhNhan";
            this.maBenhNhanDataGridViewTextBoxColumn.HeaderText = "MaBenhNhan";
            this.maBenhNhanDataGridViewTextBoxColumn.Name = "maBenhNhanDataGridViewTextBoxColumn";
            this.maBenhNhanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tenBenhNhanDataGridViewTextBoxColumn
            // 
            this.tenBenhNhanDataGridViewTextBoxColumn.DataPropertyName = "TenBenhNhan";
            this.tenBenhNhanDataGridViewTextBoxColumn.HeaderText = "TenBenhNhan";
            this.tenBenhNhanDataGridViewTextBoxColumn.Name = "tenBenhNhanDataGridViewTextBoxColumn";
            this.tenBenhNhanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // diaChiDataGridViewTextBoxColumn
            // 
            this.diaChiDataGridViewTextBoxColumn.DataPropertyName = "DiaChi";
            this.diaChiDataGridViewTextBoxColumn.HeaderText = "DiaChi";
            this.diaChiDataGridViewTextBoxColumn.Name = "diaChiDataGridViewTextBoxColumn";
            this.diaChiDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tenCongTyDataGridViewTextBoxColumn
            // 
            this.tenCongTyDataGridViewTextBoxColumn.DataPropertyName = "TenCongTy";
            this.tenCongTyDataGridViewTextBoxColumn.HeaderText = "TenCongTy";
            this.tenCongTyDataGridViewTextBoxColumn.Name = "tenCongTyDataGridViewTextBoxColumn";
            this.tenCongTyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // noteDataGridViewTextBoxColumn
            // 
            this.noteDataGridViewTextBoxColumn.DataPropertyName = "Note";
            this.noteDataGridViewTextBoxColumn.HeaderText = "Note";
            this.noteDataGridViewTextBoxColumn.Name = "noteDataGridViewTextBoxColumn";
            this.noteDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // createdDateDataGridViewTextBoxColumn
            // 
            this.createdDateDataGridViewTextBoxColumn.DataPropertyName = "CreatedDate";
            this.createdDateDataGridViewTextBoxColumn.HeaderText = "CreatedDate";
            this.createdDateDataGridViewTextBoxColumn.Name = "createdDateDataGridViewTextBoxColumn";
            this.createdDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // createdByDataGridViewTextBoxColumn
            // 
            this.createdByDataGridViewTextBoxColumn.DataPropertyName = "CreatedBy";
            this.createdByDataGridViewTextBoxColumn.HeaderText = "CreatedBy";
            this.createdByDataGridViewTextBoxColumn.Name = "createdByDataGridViewTextBoxColumn";
            this.createdByDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // updatedDateDataGridViewTextBoxColumn
            // 
            this.updatedDateDataGridViewTextBoxColumn.DataPropertyName = "UpdatedDate";
            this.updatedDateDataGridViewTextBoxColumn.HeaderText = "UpdatedDate";
            this.updatedDateDataGridViewTextBoxColumn.Name = "updatedDateDataGridViewTextBoxColumn";
            this.updatedDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // updatedByDataGridViewTextBoxColumn
            // 
            this.updatedByDataGridViewTextBoxColumn.DataPropertyName = "UpdatedBy";
            this.updatedByDataGridViewTextBoxColumn.HeaderText = "UpdatedBy";
            this.updatedByDataGridViewTextBoxColumn.Name = "updatedByDataGridViewTextBoxColumn";
            this.updatedByDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // deletedDateDataGridViewTextBoxColumn
            // 
            this.deletedDateDataGridViewTextBoxColumn.DataPropertyName = "DeletedDate";
            this.deletedDateDataGridViewTextBoxColumn.HeaderText = "DeletedDate";
            this.deletedDateDataGridViewTextBoxColumn.Name = "deletedDateDataGridViewTextBoxColumn";
            this.deletedDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // deletedByDataGridViewTextBoxColumn
            // 
            this.deletedByDataGridViewTextBoxColumn.DataPropertyName = "DeletedBy";
            this.deletedByDataGridViewTextBoxColumn.HeaderText = "DeletedBy";
            this.deletedByDataGridViewTextBoxColumn.Name = "deletedByDataGridViewTextBoxColumn";
            this.deletedByDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "Status";
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            this.statusDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // uToaCapCuuList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uToaCapCuuList";
            this.Size = new System.Drawing.Size(839, 469);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgToaCapCuu)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toaCapCuuViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgToaCapCuu;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtpkToDate;
        private System.Windows.Forms.Label lbToDate;
        private System.Windows.Forms.DateTimePicker dtpkFromDate;
        private System.Windows.Forms.RadioButton raFromDateToDate;
        private System.Windows.Forms.RadioButton raAll;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn maToaCapCuuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayKeToaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bacSiKeToaGUIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maBenhNhanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenBenhNhanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn diaChiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenCongTyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdByDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn updatedDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn updatedByDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn deletedDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn deletedByDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource toaCapCuuViewBindingSource;
    }
}
