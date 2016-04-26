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
    partial class uKetQuaNoiSoiTai
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgKetQuaNoiSoi = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Column1 = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgKetQuaNoiSoi)).BeginInit();
            this.SuspendLayout();
            // 
            // dgKetQuaNoiSoi
            // 
            this.dgKetQuaNoiSoi.AllowUserToAddRows = false;
            this.dgKetQuaNoiSoi.AllowUserToDeleteRows = false;
            this.dgKetQuaNoiSoi.AllowUserToResizeColumns = false;
            this.dgKetQuaNoiSoi.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgKetQuaNoiSoi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgKetQuaNoiSoi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgKetQuaNoiSoi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgKetQuaNoiSoi.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgKetQuaNoiSoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgKetQuaNoiSoi.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgKetQuaNoiSoi.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgKetQuaNoiSoi.HighlightSelectedColumnHeaders = false;
            this.dgKetQuaNoiSoi.Location = new System.Drawing.Point(0, 0);
            this.dgKetQuaNoiSoi.MultiSelect = false;
            this.dgKetQuaNoiSoi.Name = "dgKetQuaNoiSoi";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgKetQuaNoiSoi.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgKetQuaNoiSoi.RowHeadersVisible = false;
            this.dgKetQuaNoiSoi.RowHeadersWidth = 30;
            this.dgKetQuaNoiSoi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgKetQuaNoiSoi.Size = new System.Drawing.Size(834, 205);
            this.dgKetQuaNoiSoi.TabIndex = 1;
            this.dgKetQuaNoiSoi.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgKetQuaNoiSoi_EditingControlShowing);
            // 
            // Column1
            // 
            this.Column1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Column1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Column1.DropDownHeight = 106;
            this.Column1.DropDownWidth = 121;
            this.Column1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Column1.HeaderText = "TAI PHẢI";
            this.Column1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Column1.IntegralHeight = false;
            this.Column1.ItemHeight = 13;
            this.Column1.MaxLength = 255;
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 320;
            // 
            // Column2
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column2.HeaderText = "BỆNH LÝ TAI";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 191;
            // 
            // Column3
            // 
            this.Column3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Column3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Column3.DropDownHeight = 106;
            this.Column3.DropDownWidth = 121;
            this.Column3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Column3.HeaderText = "TAI TRÁI";
            this.Column3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Column3.IntegralHeight = false;
            this.Column3.ItemHeight = 15;
            this.Column3.MaxLength = 255;
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 320;
            // 
            // uKetQuaNoiSoiTai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgKetQuaNoiSoi);
            this.Name = "uKetQuaNoiSoiTai";
            this.Size = new System.Drawing.Size(834, 205);
            ((System.ComponentModel.ISupportInitialize)(this.dgKetQuaNoiSoi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgKetQuaNoiSoi;
        private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn Column3;
    }
}
