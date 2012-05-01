namespace MM.Controls
{
    partial class uKetQuaXetNghiemTongHop
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnChonBenhNhan = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.txtTenBenhNhan = new System.Windows.Forms.TextBox();
            this.dtpkDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpkTuNgay = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgXetNghiem = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XetNghiemGUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayXN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fullname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BinhThuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TinhTrang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgXetNghiem)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnChonBenhNhan);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.btnView);
            this.panel3.Controls.Add(this.txtTenBenhNhan);
            this.panel3.Controls.Add(this.dtpkDenNgay);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.dtpkTuNgay);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(854, 90);
            this.panel3.TabIndex = 8;
            // 
            // btnChonBenhNhan
            // 
            this.btnChonBenhNhan.Location = new System.Drawing.Point(375, 30);
            this.btnChonBenhNhan.Name = "btnChonBenhNhan";
            this.btnChonBenhNhan.Size = new System.Drawing.Size(106, 23);
            this.btnChonBenhNhan.TabIndex = 25;
            this.btnChonBenhNhan.Text = "Chọn bệnh nhân...";
            this.btnChonBenhNhan.UseVisualStyleBackColor = true;
            this.btnChonBenhNhan.Click += new System.EventHandler(this.btnChonBenhNhan_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Bệnh nhân:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Từ ngày:";
            // 
            // btnView
            // 
            this.btnView.Image = global::MM.Properties.Resources.views_icon;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(80, 58);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 22;
            this.btnView.Text = "   &Xem";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            this.btnView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpkTuNgay_KeyDown);
            // 
            // txtTenBenhNhan
            // 
            this.txtTenBenhNhan.Location = new System.Drawing.Point(80, 32);
            this.txtTenBenhNhan.Name = "txtTenBenhNhan";
            this.txtTenBenhNhan.ReadOnly = true;
            this.txtTenBenhNhan.Size = new System.Drawing.Size(291, 20);
            this.txtTenBenhNhan.TabIndex = 21;
            this.txtTenBenhNhan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpkTuNgay_KeyDown);
            // 
            // dtpkDenNgay
            // 
            this.dtpkDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkDenNgay.Location = new System.Drawing.Point(258, 8);
            this.dtpkDenNgay.Name = "dtpkDenNgay";
            this.dtpkDenNgay.Size = new System.Drawing.Size(113, 20);
            this.dtpkDenNgay.TabIndex = 19;
            this.dtpkDenNgay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpkTuNgay_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(201, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "đến ngày:";
            // 
            // dtpkTuNgay
            // 
            this.dtpkTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkTuNgay.Location = new System.Drawing.Point(80, 8);
            this.dtpkTuNgay.Name = "dtpkTuNgay";
            this.dtpkTuNgay.Size = new System.Drawing.Size(113, 20);
            this.dtpkTuNgay.TabIndex = 17;
            this.dtpkTuNgay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpkTuNgay_KeyDown);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 434);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(854, 38);
            this.panel2.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgXetNghiem);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 90);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(854, 344);
            this.panel1.TabIndex = 10;
            // 
            // dgXetNghiem
            // 
            this.dgXetNghiem.AllowUserToAddRows = false;
            this.dgXetNghiem.AllowUserToDeleteRows = false;
            this.dgXetNghiem.AllowUserToOrderColumns = true;
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
            this.XetNghiemGUID,
            this.NgayXN,
            this.Fullname,
            this.TestResult,
            this.TestPercent,
            this.BinhThuong,
            this.TinhTrang});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgXetNghiem.DefaultCellStyle = dataGridViewCellStyle5;
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
            this.dgXetNghiem.Size = new System.Drawing.Size(854, 344);
            this.dgXetNghiem.TabIndex = 9;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "NgayXN";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle6.NullValue = null;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn1.HeaderText = "Ngày xét nghiệm";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 120;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Fullname";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle7.NullValue = null;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn2.HeaderText = "Tên xét nghiệm";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "TestResult";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn3.HeaderText = "Kết quả";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 250;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "TestPercent";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn4.HeaderText = "% kết quả";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "BinhThuong";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn5.HeaderText = "Bình thường";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 180;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "BinhThuong";
            this.dataGridViewTextBoxColumn6.HeaderText = "TinhTrang";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Visible = false;
            this.dataGridViewTextBoxColumn6.Width = 180;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "TinhTrang";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Visible = false;
            // 
            // XetNghiemGUID
            // 
            this.XetNghiemGUID.DataPropertyName = "XetNghiemGUID";
            this.XetNghiemGUID.HeaderText = "XetNghiemGUID";
            this.XetNghiemGUID.Name = "XetNghiemGUID";
            this.XetNghiemGUID.ReadOnly = true;
            this.XetNghiemGUID.Visible = false;
            // 
            // NgayXN
            // 
            this.NgayXN.DataPropertyName = "NgayXN";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle2.NullValue = null;
            this.NgayXN.DefaultCellStyle = dataGridViewCellStyle2;
            this.NgayXN.HeaderText = "Ngày xét nghiệm";
            this.NgayXN.Name = "NgayXN";
            this.NgayXN.ReadOnly = true;
            this.NgayXN.Width = 130;
            // 
            // Fullname
            // 
            this.Fullname.DataPropertyName = "Fullname";
            this.Fullname.HeaderText = "Tên xét nghiệm";
            this.Fullname.Name = "Fullname";
            this.Fullname.ReadOnly = true;
            this.Fullname.Width = 250;
            // 
            // TestResult
            // 
            this.TestResult.DataPropertyName = "TestResult";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TestResult.DefaultCellStyle = dataGridViewCellStyle3;
            this.TestResult.HeaderText = "Kết quả";
            this.TestResult.Name = "TestResult";
            this.TestResult.ReadOnly = true;
            // 
            // TestPercent
            // 
            this.TestPercent.DataPropertyName = "TestPercent";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TestPercent.DefaultCellStyle = dataGridViewCellStyle4;
            this.TestPercent.HeaderText = "% kết quả";
            this.TestPercent.Name = "TestPercent";
            this.TestPercent.ReadOnly = true;
            // 
            // BinhThuong
            // 
            this.BinhThuong.DataPropertyName = "BinhThuong";
            this.BinhThuong.HeaderText = "Bình thường";
            this.BinhThuong.Name = "BinhThuong";
            this.BinhThuong.ReadOnly = true;
            this.BinhThuong.Width = 180;
            // 
            // TinhTrang
            // 
            this.TinhTrang.DataPropertyName = "TinhTrang";
            this.TinhTrang.HeaderText = "TinhTrang";
            this.TinhTrang.Name = "TinhTrang";
            this.TinhTrang.ReadOnly = true;
            this.TinhTrang.Visible = false;
            // 
            // uKetQuaXetNghiemTongHop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Name = "uKetQuaXetNghiemTongHop";
            this.Size = new System.Drawing.Size(854, 472);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgXetNghiem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.TextBox txtTenBenhNhan;
        private System.Windows.Forms.DateTimePicker dtpkDenNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpkTuNgay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnChonBenhNhan;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgXetNghiem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn XetNghiemGUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayXN;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fullname;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn BinhThuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn TinhTrang;
    }
}
