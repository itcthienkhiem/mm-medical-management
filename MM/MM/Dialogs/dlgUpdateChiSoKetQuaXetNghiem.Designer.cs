namespace MM.Dialogs
{
    partial class dlgUpdateChiSoKetQuaXetNghiem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgUpdateChiSoKetQuaXetNghiem));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDonVi = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numToValue = new System.Windows.Forms.NumericUpDown();
            this.chkToValue = new System.Windows.Forms.CheckBox();
            this.numFromValue = new System.Windows.Forms.NumericUpDown();
            this.chkFromValue = new System.Windows.Forms.CheckBox();
            this.numKetQua = new System.Windows.Forms.NumericUpDown();
            this.txTenXetNghiem = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.chkLamThem = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numToValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKetQua)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkLamThem);
            this.groupBox1.Controls.Add(this.txtDonVi);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numToValue);
            this.groupBox1.Controls.Add(this.chkToValue);
            this.groupBox1.Controls.Add(this.numFromValue);
            this.groupBox1.Controls.Add(this.chkFromValue);
            this.groupBox1.Controls.Add(this.numKetQua);
            this.groupBox1.Controls.Add(this.txTenXetNghiem);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 151);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtDonVi
            // 
            this.txtDonVi.Location = new System.Drawing.Point(93, 90);
            this.txtDonVi.Name = "txtDonVi";
            this.txtDonVi.ReadOnly = true;
            this.txtDonVi.Size = new System.Drawing.Size(94, 20);
            this.txtDonVi.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Đơn vị:";
            // 
            // numToValue
            // 
            this.numToValue.DecimalPlaces = 2;
            this.numToValue.Enabled = false;
            this.numToValue.Location = new System.Drawing.Point(216, 66);
            this.numToValue.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numToValue.Minimum = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.numToValue.Name = "numToValue";
            this.numToValue.Size = new System.Drawing.Size(74, 20);
            this.numToValue.TabIndex = 9;
            // 
            // chkToValue
            // 
            this.chkToValue.AutoSize = true;
            this.chkToValue.Location = new System.Drawing.Point(173, 68);
            this.chkToValue.Name = "chkToValue";
            this.chkToValue.Size = new System.Drawing.Size(45, 17);
            this.chkToValue.TabIndex = 8;
            this.chkToValue.Text = "đến";
            this.chkToValue.UseVisualStyleBackColor = true;
            this.chkToValue.CheckedChanged += new System.EventHandler(this.chkToValue_CheckedChanged);
            // 
            // numFromValue
            // 
            this.numFromValue.DecimalPlaces = 2;
            this.numFromValue.Enabled = false;
            this.numFromValue.Location = new System.Drawing.Point(93, 66);
            this.numFromValue.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numFromValue.Minimum = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.numFromValue.Name = "numFromValue";
            this.numFromValue.Size = new System.Drawing.Size(74, 20);
            this.numFromValue.TabIndex = 7;
            // 
            // chkFromValue
            // 
            this.chkFromValue.AutoSize = true;
            this.chkFromValue.Location = new System.Drawing.Point(22, 68);
            this.chkFromValue.Name = "chkFromValue";
            this.chkFromValue.Size = new System.Drawing.Size(70, 17);
            this.chkFromValue.TabIndex = 6;
            this.chkFromValue.Text = "Chỉ số từ:";
            this.chkFromValue.UseVisualStyleBackColor = true;
            this.chkFromValue.CheckedChanged += new System.EventHandler(this.chkFromValue_CheckedChanged);
            // 
            // numKetQua
            // 
            this.numKetQua.DecimalPlaces = 1;
            this.numKetQua.Location = new System.Drawing.Point(93, 42);
            this.numKetQua.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numKetQua.Name = "numKetQua";
            this.numKetQua.Size = new System.Drawing.Size(132, 20);
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
            this.label2.Location = new System.Drawing.Point(42, 46);
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
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(158, 159);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(79, 159);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // chkLamThem
            // 
            this.chkLamThem.AutoSize = true;
            this.chkLamThem.Location = new System.Drawing.Point(22, 125);
            this.chkLamThem.Name = "chkLamThem";
            this.chkLamThem.Size = new System.Drawing.Size(72, 17);
            this.chkLamThem.TabIndex = 12;
            this.chkLamThem.Text = "Làm thêm";
            this.chkLamThem.UseVisualStyleBackColor = true;
            // 
            // dlgUpdateChiSoKetQuaXetNghiem
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(312, 189);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgUpdateChiSoKetQuaXetNghiem";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sua chi tiet ket qua xet nghiem";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgUpdateChiSoKetQuaXetNghiem_FormClosing);
            this.Load += new System.EventHandler(this.dlgUpdateChiSoKetQuaXetNghiem_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numToValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFromValue)).EndInit();
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
        private System.Windows.Forms.TextBox txtDonVi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numToValue;
        private System.Windows.Forms.CheckBox chkToValue;
        private System.Windows.Forms.NumericUpDown numFromValue;
        private System.Windows.Forms.CheckBox chkFromValue;
        private System.Windows.Forms.CheckBox chkLamThem;
    }
}