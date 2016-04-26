/* Copyright (c) 2016, Cocosoft Inc.
 All rights reserved.
 http://www.Cocosofttech.com

 This file is part of the LIS open source project.

 The LIS  open source project is free software: you can
 redistribute it and/or modify it under the terms of the GNU General Public
 License as published by the Free Software Foundation, either version 3 of the
 License, or (at your option) any later version.

 The ClearCanvas LIS open source project is distributed in the hope that it
 will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
 Public License for more details.

 You should have received a copy of the GNU General Public License along with
 the LIS open source project.  If not, see
 <http://www.gnu.org/licenses/>.
*/
namespace MM.Dialogs
{
    partial class dlgAddMultiKetQuaCanLamSang
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddMultiKetQuaCanLamSang));
            this.dtpkActiveDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgCanLamSang = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serviceGUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.bacSiThucHienGUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.normalDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.abnormalDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.negativeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.positiveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.noteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ketQuaCanLamSangBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCanLamSang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ketQuaCanLamSangBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpkActiveDate
            // 
            this.dtpkActiveDate.CustomFormat = "dd/MM/yyyy";
            this.dtpkActiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkActiveDate.Location = new System.Drawing.Point(79, 8);
            this.dtpkActiveDate.Name = "dtpkActiveDate";
            this.dtpkActiveDate.Size = new System.Drawing.Size(122, 20);
            this.dtpkActiveDate.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Ngày khám:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(494, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(415, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.dtpkActiveDate);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(985, 37);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 525);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(985, 37);
            this.panel2.TabIndex = 13;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgCanLamSang);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 37);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(985, 488);
            this.panel3.TabIndex = 1;
            // 
            // dgCanLamSang
            // 
            this.dgCanLamSang.AllowUserToOrderColumns = true;
            this.dgCanLamSang.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCanLamSang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgCanLamSang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCanLamSang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNo,
            this.serviceGUIDDataGridViewTextBoxColumn,
            this.bacSiThucHienGUIDDataGridViewTextBoxColumn,
            this.normalDataGridViewCheckBoxColumn,
            this.abnormalDataGridViewCheckBoxColumn,
            this.negativeDataGridViewCheckBoxColumn,
            this.positiveDataGridViewCheckBoxColumn,
            this.noteDataGridViewTextBoxColumn});
            this.dgCanLamSang.DataSource = this.ketQuaCanLamSangBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgCanLamSang.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgCanLamSang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgCanLamSang.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgCanLamSang.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgCanLamSang.HighlightSelectedColumnHeaders = false;
            this.dgCanLamSang.Location = new System.Drawing.Point(0, 0);
            this.dgCanLamSang.Name = "dgCanLamSang";
            this.dgCanLamSang.RowHeadersWidth = 30;
            this.dgCanLamSang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCanLamSang.Size = new System.Drawing.Size(985, 488);
            this.dgCanLamSang.TabIndex = 6;
            this.dgCanLamSang.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgCanLamSang_CellMouseUp);
            this.dgCanLamSang.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgCanLamSang_EditingControlShowing);
            this.dgCanLamSang.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgCanLamSang_UserAddedRow);
            this.dgCanLamSang.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgCanLamSang_UserDeletedRow);
            // 
            // colNo
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colNo.DefaultCellStyle = dataGridViewCellStyle2;
            this.colNo.HeaderText = "STT";
            this.colNo.Name = "colNo";
            this.colNo.ReadOnly = true;
            this.colNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colNo.Width = 30;
            // 
            // serviceGUIDDataGridViewTextBoxColumn
            // 
            this.serviceGUIDDataGridViewTextBoxColumn.DataPropertyName = "ServiceGUID";
            this.serviceGUIDDataGridViewTextBoxColumn.DisplayStyleForCurrentCellOnly = true;
            this.serviceGUIDDataGridViewTextBoxColumn.HeaderText = "Dịch vụ";
            this.serviceGUIDDataGridViewTextBoxColumn.Name = "serviceGUIDDataGridViewTextBoxColumn";
            this.serviceGUIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.serviceGUIDDataGridViewTextBoxColumn.Width = 200;
            // 
            // bacSiThucHienGUIDDataGridViewTextBoxColumn
            // 
            this.bacSiThucHienGUIDDataGridViewTextBoxColumn.DataPropertyName = "BacSiThucHienGUID";
            this.bacSiThucHienGUIDDataGridViewTextBoxColumn.DisplayStyleForCurrentCellOnly = true;
            this.bacSiThucHienGUIDDataGridViewTextBoxColumn.HeaderText = "Bác sĩ thực hiện";
            this.bacSiThucHienGUIDDataGridViewTextBoxColumn.Name = "bacSiThucHienGUIDDataGridViewTextBoxColumn";
            this.bacSiThucHienGUIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.bacSiThucHienGUIDDataGridViewTextBoxColumn.Width = 200;
            // 
            // normalDataGridViewCheckBoxColumn
            // 
            this.normalDataGridViewCheckBoxColumn.DataPropertyName = "Normal";
            this.normalDataGridViewCheckBoxColumn.HeaderText = "Bình thường";
            this.normalDataGridViewCheckBoxColumn.Name = "normalDataGridViewCheckBoxColumn";
            this.normalDataGridViewCheckBoxColumn.Width = 70;
            // 
            // abnormalDataGridViewCheckBoxColumn
            // 
            this.abnormalDataGridViewCheckBoxColumn.DataPropertyName = "Abnormal";
            this.abnormalDataGridViewCheckBoxColumn.HeaderText = "Bất thường";
            this.abnormalDataGridViewCheckBoxColumn.Name = "abnormalDataGridViewCheckBoxColumn";
            this.abnormalDataGridViewCheckBoxColumn.Width = 65;
            // 
            // negativeDataGridViewCheckBoxColumn
            // 
            this.negativeDataGridViewCheckBoxColumn.DataPropertyName = "Negative";
            this.negativeDataGridViewCheckBoxColumn.HeaderText = "Âm tính";
            this.negativeDataGridViewCheckBoxColumn.Name = "negativeDataGridViewCheckBoxColumn";
            this.negativeDataGridViewCheckBoxColumn.Width = 50;
            // 
            // positiveDataGridViewCheckBoxColumn
            // 
            this.positiveDataGridViewCheckBoxColumn.DataPropertyName = "Positive";
            this.positiveDataGridViewCheckBoxColumn.HeaderText = "Dương tính";
            this.positiveDataGridViewCheckBoxColumn.Name = "positiveDataGridViewCheckBoxColumn";
            this.positiveDataGridViewCheckBoxColumn.Width = 70;
            // 
            // noteDataGridViewTextBoxColumn
            // 
            this.noteDataGridViewTextBoxColumn.DataPropertyName = "Note";
            this.noteDataGridViewTextBoxColumn.HeaderText = "Nhận xét";
            this.noteDataGridViewTextBoxColumn.Name = "noteDataGridViewTextBoxColumn";
            this.noteDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.noteDataGridViewTextBoxColumn.Width = 250;
            // 
            // ketQuaCanLamSangBindingSource
            // 
            this.ketQuaCanLamSangBindingSource.DataSource = typeof(MM.Databasae.KetQuaCanLamSang);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::MM.Properties.Resources.add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(208, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(127, 25);
            this.btnAdd.TabIndex = 84;
            this.btnAdd.Text = "    &Thêm lời khuyên";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dlgAddMultiKetQuaCanLamSang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(985, 562);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddMultiKetQuaCanLamSang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them ket qua can lam sang";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddServiceHistory_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddServiceHistory_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgCanLamSang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ketQuaCanLamSangBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DateTimePicker dtpkActiveDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgCanLamSang;
        private System.Windows.Forms.BindingSource ketQuaCanLamSangBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewComboBoxColumn serviceGUIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn bacSiThucHienGUIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn normalDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn abnormalDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn negativeDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn positiveDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noteDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnAdd;
    }
}
