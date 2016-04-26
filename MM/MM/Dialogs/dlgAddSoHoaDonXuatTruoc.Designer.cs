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
    partial class dlgAddSoHoaDonXuatTruoc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddSoHoaDonXuatTruoc));
            this.label1 = new System.Windows.Forms.Label();
            this.numNo = new System.Windows.Forms.NumericUpDown();
            this.btnPhatSinh = new System.Windows.Forms.Button();
            this.dgSoHoaDon = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.quanLySoHoaDonBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoHoaDon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.numNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSoHoaDon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.quanLySoHoaDonBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Số lượng hóa đơn:";
            // 
            // numNo
            // 
            this.numNo.Location = new System.Drawing.Point(106, 8);
            this.numNo.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numNo.Name = "numNo";
            this.numNo.Size = new System.Drawing.Size(78, 20);
            this.numNo.TabIndex = 1;
            this.numNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnPhatSinh
            // 
            this.btnPhatSinh.Location = new System.Drawing.Point(188, 7);
            this.btnPhatSinh.Name = "btnPhatSinh";
            this.btnPhatSinh.Size = new System.Drawing.Size(121, 23);
            this.btnPhatSinh.TabIndex = 2;
            this.btnPhatSinh.Text = "Phát sinh số hóa đơn";
            this.btnPhatSinh.UseVisualStyleBackColor = true;
            this.btnPhatSinh.Click += new System.EventHandler(this.btnPhatSinh_Click);
            // 
            // dgSoHoaDon
            // 
            this.dgSoHoaDon.AllowUserToAddRows = false;
            this.dgSoHoaDon.AllowUserToDeleteRows = false;
            this.dgSoHoaDon.AllowUserToOrderColumns = true;
            this.dgSoHoaDon.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSoHoaDon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgSoHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSoHoaDon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.SoHoaDon});
            this.dgSoHoaDon.DataSource = this.quanLySoHoaDonBindingSource;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgSoHoaDon.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgSoHoaDon.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgSoHoaDon.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgSoHoaDon.HighlightSelectedColumnHeaders = false;
            this.dgSoHoaDon.Location = new System.Drawing.Point(10, 34);
            this.dgSoHoaDon.MultiSelect = false;
            this.dgSoHoaDon.Name = "dgSoHoaDon";
            this.dgSoHoaDon.RowHeadersVisible = false;
            this.dgSoHoaDon.RowHeadersWidth = 30;
            this.dgSoHoaDon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSoHoaDon.Size = new System.Drawing.Size(297, 359);
            this.dgSoHoaDon.TabIndex = 7;
            this.dgSoHoaDon.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgSoHoaDon_ColumnHeaderMouseClick);
            this.dgSoHoaDon.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgSoHoaDon_EditingControlShowing);
            // 
            // quanLySoHoaDonBindingSource
            // 
            this.quanLySoHoaDonBindingSource.DataSource = typeof(MM.Databasae.QuanLySoHoaDon);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(160, 399);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(81, 399);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // STT
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.STT.DefaultCellStyle = dataGridViewCellStyle2;
            this.STT.Frozen = true;
            this.STT.HeaderText = "STT";
            this.STT.Name = "STT";
            this.STT.ReadOnly = true;
            this.STT.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.STT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.STT.Width = 50;
            // 
            // SoHoaDon
            // 
            this.SoHoaDon.DataPropertyName = "SoHoaDon";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SoHoaDon.DefaultCellStyle = dataGridViewCellStyle3;
            this.SoHoaDon.HeaderText = "Số hóa đơn";
            this.SoHoaDon.Name = "SoHoaDon";
            this.SoHoaDon.ReadOnly = true;
            this.SoHoaDon.Width = 220;
            // 
            // dlgAddSoHoaDonXuatTruoc
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(316, 430);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgSoHoaDon);
            this.Controls.Add(this.btnPhatSinh);
            this.Controls.Add(this.numNo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddSoHoaDonXuatTruoc";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them so hoa don xuat truoc";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddSoHoaDonXuatTruoc_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSoHoaDon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.quanLySoHoaDonBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numNo;
        private System.Windows.Forms.Button btnPhatSinh;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgSoHoaDon;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.BindingSource quanLySoHoaDonBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoHoaDon;

    }
}
