namespace MM.Dialogs
{
    partial class dlgUpdateChiSoKetQuaXetNghiem_CellDyn3200
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgUpdateChiSoKetQuaXetNghiem_CellDyn3200));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDonVi = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numToValue_Normal = new System.Windows.Forms.NumericUpDown();
            this.chkToValue_Normal = new System.Windows.Forms.CheckBox();
            this.numFromValue_Normal = new System.Windows.Forms.NumericUpDown();
            this.chkFromValue_Normal = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numKetQua = new System.Windows.Forms.NumericUpDown();
            this.txTenXetNghiem = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkLamThem = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numToValue_Normal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromValue_Normal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKetQua)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(113, 159);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(192, 159);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkLamThem);
            this.groupBox1.Controls.Add(this.txtDonVi);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numToValue_Normal);
            this.groupBox1.Controls.Add(this.chkToValue_Normal);
            this.groupBox1.Controls.Add(this.numFromValue_Normal);
            this.groupBox1.Controls.Add(this.chkFromValue_Normal);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numKetQua);
            this.groupBox1.Controls.Add(this.txTenXetNghiem);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(369, 151);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtDonVi
            // 
            this.txtDonVi.Location = new System.Drawing.Point(93, 91);
            this.txtDonVi.Name = "txtDonVi";
            this.txtDonVi.ReadOnly = true;
            this.txtDonVi.Size = new System.Drawing.Size(94, 20);
            this.txtDonVi.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Đơn vị:";
            // 
            // numToValue_Normal
            // 
            this.numToValue_Normal.DecimalPlaces = 2;
            this.numToValue_Normal.Enabled = false;
            this.numToValue_Normal.Location = new System.Drawing.Point(284, 67);
            this.numToValue_Normal.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numToValue_Normal.Minimum = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.numToValue_Normal.Name = "numToValue_Normal";
            this.numToValue_Normal.Size = new System.Drawing.Size(74, 20);
            this.numToValue_Normal.TabIndex = 17;
            // 
            // chkToValue_Normal
            // 
            this.chkToValue_Normal.AutoSize = true;
            this.chkToValue_Normal.Location = new System.Drawing.Point(241, 69);
            this.chkToValue_Normal.Name = "chkToValue_Normal";
            this.chkToValue_Normal.Size = new System.Drawing.Size(45, 17);
            this.chkToValue_Normal.TabIndex = 16;
            this.chkToValue_Normal.Text = "đến";
            this.chkToValue_Normal.UseVisualStyleBackColor = true;
            this.chkToValue_Normal.CheckedChanged += new System.EventHandler(this.chkToValue_Normal_CheckedChanged);
            // 
            // numFromValue_Normal
            // 
            this.numFromValue_Normal.DecimalPlaces = 2;
            this.numFromValue_Normal.Enabled = false;
            this.numFromValue_Normal.Location = new System.Drawing.Point(161, 67);
            this.numFromValue_Normal.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numFromValue_Normal.Minimum = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.numFromValue_Normal.Name = "numFromValue_Normal";
            this.numFromValue_Normal.Size = new System.Drawing.Size(74, 20);
            this.numFromValue_Normal.TabIndex = 15;
            // 
            // chkFromValue_Normal
            // 
            this.chkFromValue_Normal.AutoSize = true;
            this.chkFromValue_Normal.Location = new System.Drawing.Point(94, 69);
            this.chkFromValue_Normal.Name = "chkFromValue_Normal";
            this.chkFromValue_Normal.Size = new System.Drawing.Size(70, 17);
            this.chkFromValue_Normal.TabIndex = 14;
            this.chkFromValue_Normal.Text = "Chỉ số từ:";
            this.chkFromValue_Normal.UseVisualStyleBackColor = true;
            this.chkFromValue_Normal.CheckedChanged += new System.EventHandler(this.chkFromValue_Normal_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Bình thường:";
            // 
            // numKetQua
            // 
            this.numKetQua.DecimalPlaces = 3;
            this.numKetQua.Location = new System.Drawing.Point(93, 43);
            this.numKetQua.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numKetQua.Name = "numKetQua";
            this.numKetQua.Size = new System.Drawing.Size(87, 20);
            this.numKetQua.TabIndex = 4;
            // 
            // txTenXetNghiem
            // 
            this.txTenXetNghiem.Location = new System.Drawing.Point(93, 19);
            this.txTenXetNghiem.Name = "txTenXetNghiem";
            this.txTenXetNghiem.ReadOnly = true;
            this.txTenXetNghiem.Size = new System.Drawing.Size(195, 20);
            this.txTenXetNghiem.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Kết quả:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên xét nghiệm:";
            // 
            // chkLamThem
            // 
            this.chkLamThem.AutoSize = true;
            this.chkLamThem.Location = new System.Drawing.Point(25, 123);
            this.chkLamThem.Name = "chkLamThem";
            this.chkLamThem.Size = new System.Drawing.Size(72, 17);
            this.chkLamThem.TabIndex = 24;
            this.chkLamThem.Text = "Làm thêm";
            this.chkLamThem.UseVisualStyleBackColor = true;
            // 
            // dlgUpdateChiSoKetQuaXetNghiem_CellDyn3200
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(380, 190);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgUpdateChiSoKetQuaXetNghiem_CellDyn3200";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sua chi tiet ket qua xet nghiem";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgUpdateChiSoKetQuaXetNghiem_FormClosing);
            this.Load += new System.EventHandler(this.dlgUpdateChiSoKetQuaXetNghiem_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numToValue_Normal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromValue_Normal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKetQua)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txTenXetNghiem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.NumericUpDown numKetQua;
        private System.Windows.Forms.NumericUpDown numToValue_Normal;
        private System.Windows.Forms.CheckBox chkToValue_Normal;
        private System.Windows.Forms.NumericUpDown numFromValue_Normal;
        private System.Windows.Forms.CheckBox chkFromValue_Normal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDonVi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkLamThem;
    }
}