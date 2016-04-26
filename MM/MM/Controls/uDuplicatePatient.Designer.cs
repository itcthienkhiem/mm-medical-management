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
    partial class uDuplicatePatient
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
            this.dgDuplicatePatient = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.FileNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fullname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DobStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GenderAsStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patientViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbKetQuaTimDuoc = new System.Windows.Forms.Label();
            this.chkTheoSoDienThoai = new System.Windows.Forms.CheckBox();
            this.chkMaBenhNhan = new System.Windows.Forms.CheckBox();
            this.txtSearchPatient = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnMerge = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ctmAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgDuplicatePatient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.ctmAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgDuplicatePatient
            // 
            this.dgDuplicatePatient.AllowUserToAddRows = false;
            this.dgDuplicatePatient.AllowUserToDeleteRows = false;
            this.dgDuplicatePatient.AllowUserToOrderColumns = true;
            this.dgDuplicatePatient.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDuplicatePatient.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDuplicatePatient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDuplicatePatient.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileNum,
            this.Fullname,
            this.DobStr,
            this.FullAddress,
            this.GenderAsStr,
            this.Mobile});
            this.dgDuplicatePatient.ContextMenuStrip = this.ctmAction;
            this.dgDuplicatePatient.DataSource = this.patientViewBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDuplicatePatient.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgDuplicatePatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDuplicatePatient.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgDuplicatePatient.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgDuplicatePatient.HighlightSelectedColumnHeaders = false;
            this.dgDuplicatePatient.Location = new System.Drawing.Point(0, 0);
            this.dgDuplicatePatient.Name = "dgDuplicatePatient";
            this.dgDuplicatePatient.ReadOnly = true;
            this.dgDuplicatePatient.RowHeadersWidth = 30;
            this.dgDuplicatePatient.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDuplicatePatient.Size = new System.Drawing.Size(867, 399);
            this.dgDuplicatePatient.TabIndex = 3;
            this.dgDuplicatePatient.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgDuplicatePatient_ColumnHeaderMouseClick);
            // 
            // FileNum
            // 
            this.FileNum.DataPropertyName = "FileNum";
            this.FileNum.HeaderText = "Mã Bệnh Nhân";
            this.FileNum.Name = "FileNum";
            this.FileNum.ReadOnly = true;
            this.FileNum.Width = 120;
            // 
            // Fullname
            // 
            this.Fullname.DataPropertyName = "FullName";
            this.Fullname.HeaderText = "Họ Tên";
            this.Fullname.Name = "Fullname";
            this.Fullname.ReadOnly = true;
            this.Fullname.Width = 150;
            // 
            // DobStr
            // 
            this.DobStr.DataPropertyName = "DobStr";
            this.DobStr.HeaderText = "Ngày sinh";
            this.DobStr.Name = "DobStr";
            this.DobStr.ReadOnly = true;
            // 
            // FullAddress
            // 
            this.FullAddress.DataPropertyName = "Address";
            this.FullAddress.HeaderText = "Địa chỉ";
            this.FullAddress.Name = "FullAddress";
            this.FullAddress.ReadOnly = true;
            this.FullAddress.Width = 250;
            // 
            // GenderAsStr
            // 
            this.GenderAsStr.DataPropertyName = "GenderAsStr";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.GenderAsStr.DefaultCellStyle = dataGridViewCellStyle2;
            this.GenderAsStr.HeaderText = "Giới tính";
            this.GenderAsStr.Name = "GenderAsStr";
            this.GenderAsStr.ReadOnly = true;
            // 
            // Mobile
            // 
            this.Mobile.DataPropertyName = "Mobile";
            this.Mobile.HeaderText = "Điện thoại";
            this.Mobile.Name = "Mobile";
            this.Mobile.ReadOnly = true;
            // 
            // patientViewBindingSource
            // 
            this.patientViewBindingSource.DataSource = typeof(MM.Databasae.PatientView);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbKetQuaTimDuoc);
            this.panel1.Controls.Add(this.chkTheoSoDienThoai);
            this.panel1.Controls.Add(this.chkMaBenhNhan);
            this.panel1.Controls.Add(this.txtSearchPatient);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(867, 37);
            this.panel1.TabIndex = 4;
            // 
            // lbKetQuaTimDuoc
            // 
            this.lbKetQuaTimDuoc.AutoSize = true;
            this.lbKetQuaTimDuoc.ForeColor = System.Drawing.Color.Blue;
            this.lbKetQuaTimDuoc.Location = new System.Drawing.Point(643, 13);
            this.lbKetQuaTimDuoc.Name = "lbKetQuaTimDuoc";
            this.lbKetQuaTimDuoc.Size = new System.Drawing.Size(100, 13);
            this.lbKetQuaTimDuoc.TabIndex = 18;
            this.lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
            // 
            // chkTheoSoDienThoai
            // 
            this.chkTheoSoDienThoai.AutoSize = true;
            this.chkTheoSoDienThoai.Location = new System.Drawing.Point(512, 12);
            this.chkTheoSoDienThoai.Name = "chkTheoSoDienThoai";
            this.chkTheoSoDienThoai.Size = new System.Drawing.Size(115, 17);
            this.chkTheoSoDienThoai.TabIndex = 7;
            this.chkTheoSoDienThoai.Text = "Theo số điện thoại";
            this.chkTheoSoDienThoai.UseVisualStyleBackColor = true;
            this.chkTheoSoDienThoai.CheckedChanged += new System.EventHandler(this.chkTheoSoDienThoai_CheckedChanged);
            // 
            // chkMaBenhNhan
            // 
            this.chkMaBenhNhan.AutoSize = true;
            this.chkMaBenhNhan.Checked = true;
            this.chkMaBenhNhan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMaBenhNhan.Location = new System.Drawing.Point(384, 12);
            this.chkMaBenhNhan.Name = "chkMaBenhNhan";
            this.chkMaBenhNhan.Size = new System.Drawing.Size(122, 17);
            this.chkMaBenhNhan.TabIndex = 4;
            this.chkMaBenhNhan.Text = "Theo mã bệnh nhân";
            this.chkMaBenhNhan.UseVisualStyleBackColor = true;
            this.chkMaBenhNhan.CheckedChanged += new System.EventHandler(this.chkMaBenhNhan_CheckedChanged);
            // 
            // txtSearchPatient
            // 
            this.txtSearchPatient.Location = new System.Drawing.Point(87, 10);
            this.txtSearchPatient.Name = "txtSearchPatient";
            this.txtSearchPatient.Size = new System.Drawing.Size(291, 20);
            this.txtSearchPatient.TabIndex = 3;
            this.txtSearchPatient.TextChanged += new System.EventHandler(this.txtSearchPatient_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tìm bệnh nhân:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnMerge);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 436);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(867, 38);
            this.panel3.TabIndex = 6;
            // 
            // btnMerge
            // 
            this.btnMerge.Image = global::MM.Properties.Resources.Architecture_info_icon;
            this.btnMerge.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMerge.Location = new System.Drawing.Point(8, 7);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(75, 23);
            this.btnMerge.TabIndex = 0;
            this.btnMerge.Text = "    Merge";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgDuplicatePatient);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(867, 399);
            this.panel2.TabIndex = 7;
            // 
            // ctmAction
            // 
            this.ctmAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mergeToolStripMenuItem});
            this.ctmAction.Name = "ctmAction";
            this.ctmAction.Size = new System.Drawing.Size(153, 48);
            // 
            // mergeToolStripMenuItem
            // 
            this.mergeToolStripMenuItem.Image = global::MM.Properties.Resources.Architecture_info_icon;
            this.mergeToolStripMenuItem.Name = "mergeToolStripMenuItem";
            this.mergeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mergeToolStripMenuItem.Text = "Merge";
            this.mergeToolStripMenuItem.Click += new System.EventHandler(this.mergeToolStripMenuItem_Click);
            // 
            // uDuplicatePatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "uDuplicatePatient";
            this.Size = new System.Drawing.Size(867, 474);
            ((System.ComponentModel.ISupportInitialize)(this.dgDuplicatePatient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.patientViewBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ctmAction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgDuplicatePatient;
        private System.Windows.Forms.BindingSource patientViewBindingSource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtSearchPatient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fullname;
        private System.Windows.Forms.DataGridViewTextBoxColumn DobStr;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn GenderAsStr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mobile;
        private System.Windows.Forms.CheckBox chkMaBenhNhan;
        private System.Windows.Forms.CheckBox chkTheoSoDienThoai;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbKetQuaTimDuoc;
        private System.Windows.Forms.ContextMenuStrip ctmAction;
        private System.Windows.Forms.ToolStripMenuItem mergeToolStripMenuItem;
    }
}
