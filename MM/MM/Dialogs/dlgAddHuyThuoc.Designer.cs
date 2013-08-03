namespace MM.Dialogs
{
    partial class dlgAddHuyThuoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddHuyThuoc));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numSoLuongTon = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpkNgayHuy = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChonThuoc = new System.Windows.Forms.Button();
            this.cboThuoc = new System.Windows.Forms.ComboBox();
            this.thuocBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongTon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thuocBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtGhiChu);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numSoLuongTon);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpkNgayHuy);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numSoLuong);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnChonThuoc);
            this.groupBox1.Controls.Add(this.cboThuoc);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(467, 210);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // numSoLuongTon
            // 
            this.numSoLuongTon.Enabled = false;
            this.numSoLuongTon.Location = new System.Drawing.Point(272, 71);
            this.numSoLuongTon.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numSoLuongTon.Name = "numSoLuongTon";
            this.numSoLuongTon.Size = new System.Drawing.Size(86, 20);
            this.numSoLuongTon.TabIndex = 11;
            this.numSoLuongTon.ThousandsSeparator = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(199, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Số lượng tồn:";
            // 
            // dtpkNgayHuy
            // 
            this.dtpkNgayHuy.CustomFormat = "dd/MM/yyyy";
            this.dtpkNgayHuy.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkNgayHuy.Location = new System.Drawing.Point(77, 45);
            this.dtpkNgayHuy.Name = "dtpkNgayHuy";
            this.dtpkNgayHuy.Size = new System.Drawing.Size(105, 20);
            this.dtpkNgayHuy.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Ngày hủy:";
            // 
            // numSoLuong
            // 
            this.numSoLuong.Location = new System.Drawing.Point(77, 71);
            this.numSoLuong.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(105, 20);
            this.numSoLuong.TabIndex = 3;
            this.numSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Số lượng:";
            // 
            // btnChonThuoc
            // 
            this.btnChonThuoc.Location = new System.Drawing.Point(378, 17);
            this.btnChonThuoc.Name = "btnChonThuoc";
            this.btnChonThuoc.Size = new System.Drawing.Size(75, 23);
            this.btnChonThuoc.TabIndex = 1;
            this.btnChonThuoc.Text = "&Chọn thuốc";
            this.btnChonThuoc.UseVisualStyleBackColor = true;
            this.btnChonThuoc.Click += new System.EventHandler(this.btnChonThuoc_Click);
            // 
            // cboThuoc
            // 
            this.cboThuoc.DataSource = this.thuocBindingSource;
            this.cboThuoc.DisplayMember = "TenThuoc";
            this.cboThuoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboThuoc.Enabled = false;
            this.cboThuoc.FormattingEnabled = true;
            this.cboThuoc.Location = new System.Drawing.Point(77, 18);
            this.cboThuoc.Name = "cboThuoc";
            this.cboThuoc.Size = new System.Drawing.Size(295, 21);
            this.cboThuoc.TabIndex = 0;
            this.cboThuoc.ValueMember = "ThuocGUID";
            this.cboThuoc.SelectedIndexChanged += new System.EventHandler(this.cboThuoc_SelectedIndexChanged);
            // 
            // thuocBindingSource
            // 
            this.thuocBindingSource.DataSource = typeof(MM.Databasae.Thuoc);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tên thuốc:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(243, 220);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.check;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(164, 220);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 19;
            this.btnOK.Text = "    &Đồng ý";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Ghi chú:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(77, 97);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(376, 100);
            this.txtGhiChu.TabIndex = 13;
            // 
            // dlgAddHuyThuoc
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(483, 251);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddHuyThuoc";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them huy thuoc";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddHuyThuoc_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddHuyThuoc_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuongTon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thuocBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.BindingSource thuocBindingSource;
        private System.Windows.Forms.Button btnChonThuoc;
        private System.Windows.Forms.ComboBox cboThuoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DateTimePicker dtpkNgayHuy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numSoLuongTon;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label label4;
    }
}