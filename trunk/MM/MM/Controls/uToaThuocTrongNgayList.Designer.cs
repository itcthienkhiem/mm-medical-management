namespace MM.Controls
{
    partial class uToaThuocTrongNgayList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTatCanhBao = new System.Windows.Forms.Button();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgToaThuoc = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.maToaThuocDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayKham = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayTaiKham = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChanDoan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenBenhNhanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DobStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GenderAsStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenBacSiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctmAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tatCanhBaoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toaThuocViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgToaThuoc)).BeginInit();
            this.ctmAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toaThuocViewBindingSource)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnTatCanhBao);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 431);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(839, 38);
            this.panel1.TabIndex = 2;
            // 
            // btnTatCanhBao
            // 
            this.btnTatCanhBao.Image = global::MM.Properties.Resources.del;
            this.btnTatCanhBao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTatCanhBao.Location = new System.Drawing.Point(6, 6);
            this.btnTatCanhBao.Name = "btnTatCanhBao";
            this.btnTatCanhBao.Size = new System.Drawing.Size(108, 25);
            this.btnTatCanhBao.TabIndex = 0;
            this.btnTatCanhBao.Text = "      &Tắt cảnh báo";
            this.btnTatCanhBao.UseVisualStyleBackColor = true;
            this.btnTatCanhBao.Click += new System.EventHandler(this.btnTatCanhBao_Click);
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
            // dgToaThuoc
            // 
            this.dgToaThuoc.AllowUserToAddRows = false;
            this.dgToaThuoc.AllowUserToDeleteRows = false;
            this.dgToaThuoc.AllowUserToOrderColumns = true;
            this.dgToaThuoc.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgToaThuoc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgToaThuoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgToaThuoc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.maToaThuocDataGridViewTextBoxColumn,
            this.NgayKham,
            this.NgayTaiKham,
            this.ChanDoan,
            this.noteDataGridViewTextBoxColumn,
            this.tenBenhNhanDataGridViewTextBoxColumn,
            this.DobStr,
            this.GenderAsStr,
            this.Mobile,
            this.Address,
            this.tenBacSiDataGridViewTextBoxColumn});
            this.dgToaThuoc.ContextMenuStrip = this.ctmAction;
            this.dgToaThuoc.DataSource = this.toaThuocViewBindingSource;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgToaThuoc.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgToaThuoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgToaThuoc.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgToaThuoc.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgToaThuoc.HighlightSelectedColumnHeaders = false;
            this.dgToaThuoc.Location = new System.Drawing.Point(0, 0);
            this.dgToaThuoc.MultiSelect = false;
            this.dgToaThuoc.Name = "dgToaThuoc";
            this.dgToaThuoc.RowHeadersWidth = 30;
            this.dgToaThuoc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgToaThuoc.Size = new System.Drawing.Size(839, 431);
            this.dgToaThuoc.TabIndex = 4;
            this.dgToaThuoc.DoubleClick += new System.EventHandler(this.dgThuoc_DoubleClick);
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
            // maToaThuocDataGridViewTextBoxColumn
            // 
            this.maToaThuocDataGridViewTextBoxColumn.DataPropertyName = "MaToaThuoc";
            this.maToaThuocDataGridViewTextBoxColumn.HeaderText = "Mã toa thuốc";
            this.maToaThuocDataGridViewTextBoxColumn.Name = "maToaThuocDataGridViewTextBoxColumn";
            this.maToaThuocDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // NgayKham
            // 
            this.NgayKham.DataPropertyName = "NgayKham";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            dataGridViewCellStyle2.NullValue = null;
            this.NgayKham.DefaultCellStyle = dataGridViewCellStyle2;
            this.NgayKham.HeaderText = "Ngày khám";
            this.NgayKham.Name = "NgayKham";
            this.NgayKham.ReadOnly = true;
            this.NgayKham.Width = 90;
            // 
            // NgayTaiKham
            // 
            this.NgayTaiKham.DataPropertyName = "NgayTaiKham";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "dd/MM/yyyy";
            dataGridViewCellStyle3.NullValue = null;
            this.NgayTaiKham.DefaultCellStyle = dataGridViewCellStyle3;
            this.NgayTaiKham.HeaderText = "Ngày tái khám";
            this.NgayTaiKham.Name = "NgayTaiKham";
            this.NgayTaiKham.ReadOnly = true;
            // 
            // ChanDoan
            // 
            this.ChanDoan.DataPropertyName = "ChanDoan";
            this.ChanDoan.HeaderText = "Chẩn đoán";
            this.ChanDoan.Name = "ChanDoan";
            this.ChanDoan.ReadOnly = true;
            this.ChanDoan.Width = 200;
            // 
            // noteDataGridViewTextBoxColumn
            // 
            this.noteDataGridViewTextBoxColumn.DataPropertyName = "Note";
            this.noteDataGridViewTextBoxColumn.HeaderText = "Lời dặn";
            this.noteDataGridViewTextBoxColumn.Name = "noteDataGridViewTextBoxColumn";
            this.noteDataGridViewTextBoxColumn.ReadOnly = true;
            this.noteDataGridViewTextBoxColumn.Width = 250;
            // 
            // tenBenhNhanDataGridViewTextBoxColumn
            // 
            this.tenBenhNhanDataGridViewTextBoxColumn.DataPropertyName = "TenBenhNhan";
            this.tenBenhNhanDataGridViewTextBoxColumn.HeaderText = "Tên bệnh nhân";
            this.tenBenhNhanDataGridViewTextBoxColumn.Name = "tenBenhNhanDataGridViewTextBoxColumn";
            this.tenBenhNhanDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenBenhNhanDataGridViewTextBoxColumn.Width = 200;
            // 
            // DobStr
            // 
            this.DobStr.DataPropertyName = "DobStr";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DobStr.DefaultCellStyle = dataGridViewCellStyle4;
            this.DobStr.HeaderText = "Ngày sinh";
            this.DobStr.Name = "DobStr";
            this.DobStr.ReadOnly = true;
            this.DobStr.Width = 90;
            // 
            // GenderAsStr
            // 
            this.GenderAsStr.DataPropertyName = "GenderAsStr";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.GenderAsStr.DefaultCellStyle = dataGridViewCellStyle5;
            this.GenderAsStr.HeaderText = "Giới tính";
            this.GenderAsStr.Name = "GenderAsStr";
            this.GenderAsStr.ReadOnly = true;
            this.GenderAsStr.Width = 70;
            // 
            // Mobile
            // 
            this.Mobile.DataPropertyName = "Mobile";
            this.Mobile.HeaderText = "Điện thoại";
            this.Mobile.Name = "Mobile";
            this.Mobile.ReadOnly = true;
            // 
            // Address
            // 
            this.Address.DataPropertyName = "Address";
            this.Address.HeaderText = "Địa chỉ";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            this.Address.Width = 150;
            // 
            // tenBacSiDataGridViewTextBoxColumn
            // 
            this.tenBacSiDataGridViewTextBoxColumn.DataPropertyName = "TenBacSi";
            this.tenBacSiDataGridViewTextBoxColumn.HeaderText = "Bác sĩ kê toa";
            this.tenBacSiDataGridViewTextBoxColumn.Name = "tenBacSiDataGridViewTextBoxColumn";
            this.tenBacSiDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenBacSiDataGridViewTextBoxColumn.Width = 150;
            // 
            // ctmAction
            // 
            this.ctmAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.tatCanhBaoToolStripMenuItem});
            this.ctmAction.Name = "cmtAction";
            this.ctmAction.Size = new System.Drawing.Size(144, 32);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(140, 6);
            // 
            // tatCanhBaoToolStripMenuItem
            // 
            this.tatCanhBaoToolStripMenuItem.Image = global::MM.Properties.Resources.del;
            this.tatCanhBaoToolStripMenuItem.Name = "tatCanhBaoToolStripMenuItem";
            this.tatCanhBaoToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.tatCanhBaoToolStripMenuItem.Text = "Tắt cảnh báo";
            this.tatCanhBaoToolStripMenuItem.Click += new System.EventHandler(this.tatCanhBaoToolStripMenuItem_Click);
            // 
            // toaThuocViewBindingSource
            // 
            this.toaThuocViewBindingSource.DataSource = typeof(MM.Databasae.ToaThuocView);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkChecked);
            this.panel3.Controls.Add(this.dgToaThuoc);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(839, 431);
            this.panel3.TabIndex = 4;
            // 
            // uToaThuocTrongNgayList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "uToaThuocTrongNgayList";
            this.Size = new System.Drawing.Size(839, 469);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgToaThuoc)).EndInit();
            this.ctmAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.toaThuocViewBindingSource)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTatCanhBao;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgToaThuoc;
        private System.Windows.Forms.BindingSource toaThuocViewBindingSource;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn maToaThuocDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayKham;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayTaiKham;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChanDoan;
        private System.Windows.Forms.DataGridViewTextBoxColumn noteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenBenhNhanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DobStr;
        private System.Windows.Forms.DataGridViewTextBoxColumn GenderAsStr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenBacSiDataGridViewTextBoxColumn;
        protected System.Windows.Forms.ContextMenuStrip ctmAction;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tatCanhBaoToolStripMenuItem;
    }
}
