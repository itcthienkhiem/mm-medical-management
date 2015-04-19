namespace MM.Dialogs
{
    partial class dlgLoThuocView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgLoThuocView));
            this.dgThuocTonKho = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colMaLoThuoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenLoThuoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNguoiTao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgThuocTonKho)).BeginInit();
            this.SuspendLayout();
            // 
            // dgThuocTonKho
            // 
            this.dgThuocTonKho.AllowUserToAddRows = false;
            this.dgThuocTonKho.AllowUserToDeleteRows = false;
            this.dgThuocTonKho.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgThuocTonKho.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgThuocTonKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgThuocTonKho.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaLoThuoc,
            this.colTenLoThuoc,
            this.colGiaNhap,
            this.colNgayNhap,
            this.colNguoiTao});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgThuocTonKho.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgThuocTonKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgThuocTonKho.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgThuocTonKho.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgThuocTonKho.HighlightSelectedColumnHeaders = false;
            this.dgThuocTonKho.Location = new System.Drawing.Point(0, 0);
            this.dgThuocTonKho.MultiSelect = false;
            this.dgThuocTonKho.Name = "dgThuocTonKho";
            this.dgThuocTonKho.ReadOnly = true;
            this.dgThuocTonKho.RowHeadersWidth = 30;
            this.dgThuocTonKho.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgThuocTonKho.Size = new System.Drawing.Size(737, 430);
            this.dgThuocTonKho.TabIndex = 5;
            // 
            // colMaLoThuoc
            // 
            this.colMaLoThuoc.DataPropertyName = "MaLoThuoc";
            this.colMaLoThuoc.HeaderText = "Mã lô thuốc";
            this.colMaLoThuoc.Name = "colMaLoThuoc";
            this.colMaLoThuoc.ReadOnly = true;
            this.colMaLoThuoc.Width = 120;
            // 
            // colTenLoThuoc
            // 
            this.colTenLoThuoc.DataPropertyName = "TenLoThuoc";
            this.colTenLoThuoc.HeaderText = "Tên lô thuốc";
            this.colTenLoThuoc.Name = "colTenLoThuoc";
            this.colTenLoThuoc.ReadOnly = true;
            this.colTenLoThuoc.Width = 200;
            // 
            // colGiaNhap
            // 
            this.colGiaNhap.DataPropertyName = "GiaNhap";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.colGiaNhap.DefaultCellStyle = dataGridViewCellStyle2;
            this.colGiaNhap.HeaderText = "Giá nhập";
            this.colGiaNhap.Name = "colGiaNhap";
            this.colGiaNhap.ReadOnly = true;
            // 
            // colNgayNhap
            // 
            this.colNgayNhap.DataPropertyName = "CreatedDate";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colNgayNhap.DefaultCellStyle = dataGridViewCellStyle3;
            this.colNgayNhap.HeaderText = "Ngày nhập";
            this.colNgayNhap.Name = "colNgayNhap";
            this.colNgayNhap.ReadOnly = true;
            this.colNgayNhap.Width = 120;
            // 
            // colNguoiTao
            // 
            this.colNguoiTao.DataPropertyName = "NguoiTao";
            this.colNguoiTao.HeaderText = "Người tạo";
            this.colNguoiTao.Name = "colNguoiTao";
            this.colNguoiTao.ReadOnly = true;
            this.colNguoiTao.Width = 150;
            // 
            // dlgLoThuocView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 430);
            this.Controls.Add(this.dgThuocTonKho);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgLoThuocView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lo Thuoc";
            this.Load += new System.EventHandler(this.dlgLoThuocView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgThuocTonKho)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX dgThuocTonKho;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaLoThuoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenLoThuoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNguoiTao;
    }
}