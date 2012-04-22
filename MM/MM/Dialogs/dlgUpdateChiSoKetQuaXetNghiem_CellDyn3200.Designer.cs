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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numTestPercent = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numKetQua = new System.Windows.Forms.NumericUpDown();
            this.txtBinhThuong = new System.Windows.Forms.TextBox();
            this.txTenXetNghiem = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTestPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKetQua)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numTestPercent);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numKetQua);
            this.groupBox1.Controls.Add(this.txtBinhThuong);
            this.groupBox1.Controls.Add(this.txTenXetNghiem);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 119);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // numTestPercent
            // 
            this.numTestPercent.DecimalPlaces = 3;
            this.numTestPercent.Location = new System.Drawing.Point(93, 65);
            this.numTestPercent.Name = "numTestPercent";
            this.numTestPercent.Size = new System.Drawing.Size(87, 20);
            this.numTestPercent.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "% kết quả:";
            // 
            // numKetQua
            // 
            this.numKetQua.DecimalPlaces = 3;
            this.numKetQua.Location = new System.Drawing.Point(93, 42);
            this.numKetQua.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numKetQua.Name = "numKetQua";
            this.numKetQua.Size = new System.Drawing.Size(87, 20);
            this.numKetQua.TabIndex = 4;
            // 
            // txtBinhThuong
            // 
            this.txtBinhThuong.Location = new System.Drawing.Point(93, 88);
            this.txtBinhThuong.Name = "txtBinhThuong";
            this.txtBinhThuong.ReadOnly = true;
            this.txtBinhThuong.Size = new System.Drawing.Size(195, 20);
            this.txtBinhThuong.TabIndex = 6;
            // 
            // txTenXetNghiem
            // 
            this.txTenXetNghiem.Location = new System.Drawing.Point(93, 19);
            this.txTenXetNghiem.Name = "txTenXetNghiem";
            this.txTenXetNghiem.ReadOnly = true;
            this.txTenXetNghiem.Size = new System.Drawing.Size(195, 20);
            this.txTenXetNghiem.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Bình thường:";
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
            this.btnCancel.Location = new System.Drawing.Point(158, 127);
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
            this.btnOK.Location = new System.Drawing.Point(79, 127);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // dlgUpdateChiSoKetQuaXetNghiem_CellDyn3200
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(313, 157);
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
            this.Text = "Sua chi so ket qua xet nghiem";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgUpdateChiSoKetQuaXetNghiem_FormClosing);
            this.Load += new System.EventHandler(this.dlgUpdateChiSoKetQuaXetNghiem_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTestPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKetQua)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBinhThuong;
        private System.Windows.Forms.TextBox txTenXetNghiem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.NumericUpDown numKetQua;
        private System.Windows.Forms.NumericUpDown numTestPercent;
        private System.Windows.Forms.Label label4;
    }
}