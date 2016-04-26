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
namespace MM.Controls
{
    partial class uDanhSachXetNghiemHitachi917List
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtXetNghiem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgXetNghiem = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.fullnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xetNghiemHitachi917BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this._uChiSoXetNghiem_Glucose_Hitachi917 = new MM.Controls.uChiSoXetNghiem_Glucose_Hitachi917();
            this._uChiSoXetNghiem_Hitachi917 = new MM.Controls.uChiSoXetNghiem_Hitachi917();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgXetNghiem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xetNghiemHitachi917BindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtXetNghiem);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(301, 37);
            this.panel2.TabIndex = 3;
            // 
            // txtXetNghiem
            // 
            this.txtXetNghiem.Location = new System.Drawing.Point(89, 9);
            this.txtXetNghiem.Name = "txtXetNghiem";
            this.txtXetNghiem.Size = new System.Drawing.Size(203, 20);
            this.txtXetNghiem.TabIndex = 3;
            this.txtXetNghiem.TextChanged += new System.EventHandler(this.txtXetNghiem_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tìm xét nghiệm:";
            // 
            // dgXetNghiem
            // 
            this.dgXetNghiem.AllowUserToAddRows = false;
            this.dgXetNghiem.AllowUserToDeleteRows = false;
            this.dgXetNghiem.AllowUserToOrderColumns = true;
            this.dgXetNghiem.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgXetNghiem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgXetNghiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgXetNghiem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fullnameDataGridViewTextBoxColumn});
            this.dgXetNghiem.DataSource = this.xetNghiemHitachi917BindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgXetNghiem.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgXetNghiem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgXetNghiem.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgXetNghiem.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgXetNghiem.HighlightSelectedColumnHeaders = false;
            this.dgXetNghiem.Location = new System.Drawing.Point(0, 0);
            this.dgXetNghiem.MultiSelect = false;
            this.dgXetNghiem.Name = "dgXetNghiem";
            this.dgXetNghiem.ReadOnly = true;
            this.dgXetNghiem.RowHeadersWidth = 30;
            this.dgXetNghiem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgXetNghiem.Size = new System.Drawing.Size(301, 430);
            this.dgXetNghiem.TabIndex = 5;
            this.dgXetNghiem.SelectionChanged += new System.EventHandler(this.dgXetNghiem_SelectionChanged);
            // 
            // fullnameDataGridViewTextBoxColumn
            // 
            this.fullnameDataGridViewTextBoxColumn.DataPropertyName = "Fullname";
            this.fullnameDataGridViewTextBoxColumn.HeaderText = "Tên xét nghiệm";
            this.fullnameDataGridViewTextBoxColumn.Name = "fullnameDataGridViewTextBoxColumn";
            this.fullnameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fullnameDataGridViewTextBoxColumn.Width = 250;
            // 
            // xetNghiemHitachi917BindingSource
            // 
            this.xetNghiemHitachi917BindingSource.DataSource = typeof(MM.Databasae.XetNghiem_Hitachi917);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 467);
            this.panel1.TabIndex = 7;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgXetNghiem);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 37);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(301, 430);
            this.panel3.TabIndex = 4;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(301, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(700, 20);
            this.panel6.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DodgerBlue;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(700, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Thông số chỉ số xét nghiệm";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _uChiSoXetNghiem_Glucose_Hitachi917
            // 
            this._uChiSoXetNghiem_Glucose_Hitachi917.Location = new System.Drawing.Point(307, 26);
            this._uChiSoXetNghiem_Glucose_Hitachi917.Name = "_uChiSoXetNghiem_Glucose_Hitachi917";
            this._uChiSoXetNghiem_Glucose_Hitachi917.Size = new System.Drawing.Size(633, 438);
            this._uChiSoXetNghiem_Glucose_Hitachi917.TabIndex = 9;
            this._uChiSoXetNghiem_Glucose_Hitachi917.Visible = false;
            // 
            // _uChiSoXetNghiem_Hitachi917
            // 
            this._uChiSoXetNghiem_Hitachi917.Location = new System.Drawing.Point(307, 26);
            this._uChiSoXetNghiem_Hitachi917.Name = "_uChiSoXetNghiem_Hitachi917";
            this._uChiSoXetNghiem_Hitachi917.Size = new System.Drawing.Size(582, 297);
            this._uChiSoXetNghiem_Hitachi917.TabIndex = 10;
            this._uChiSoXetNghiem_Hitachi917.Visible = false;
            // 
            // uDanhSachXetNghiemHitachi917List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this._uChiSoXetNghiem_Hitachi917);
            this.Controls.Add(this._uChiSoXetNghiem_Glucose_Hitachi917);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel1);
            this.Name = "uDanhSachXetNghiemHitachi917List";
            this.Size = new System.Drawing.Size(1001, 467);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgXetNghiem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xetNghiemHitachi917BindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtXetNghiem;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgXetNghiem;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource xetNghiemHitachi917BindingSource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private uChiSoXetNghiem_Glucose_Hitachi917 _uChiSoXetNghiem_Glucose_Hitachi917;
        private uChiSoXetNghiem_Hitachi917 _uChiSoXetNghiem_Hitachi917;
    }
}
