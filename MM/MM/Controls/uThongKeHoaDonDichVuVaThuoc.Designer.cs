namespace MM.Controls
{
    partial class uThongKeHoaDonDichVuVaThuoc
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpkDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpkTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this._printDialog = new System.Windows.Forms.PrintDialog();
            this.raChuaThuTien = new System.Windows.Forms.RadioButton();
            this.raDaThuTien = new System.Windows.Forms.RadioButton();
            this.raAll = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.raChuaThuTien);
            this.groupBox1.Controls.Add(this.raDaThuTien);
            this.groupBox1.Controls.Add(this.raAll);
            this.groupBox1.Controls.Add(this.dtpkDenNgay);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpkTuNgay);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(367, 81);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // dtpkDenNgay
            // 
            this.dtpkDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkDenNgay.Location = new System.Drawing.Point(239, 19);
            this.dtpkDenNgay.Name = "dtpkDenNgay";
            this.dtpkDenNgay.Size = new System.Drawing.Size(113, 20);
            this.dtpkDenNgay.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(182, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "đến ngày";
            // 
            // dtpkTuNgay
            // 
            this.dtpkTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkTuNgay.Location = new System.Drawing.Point(61, 19);
            this.dtpkTuNgay.Name = "dtpkTuNgay";
            this.dtpkTuNgay.Size = new System.Drawing.Size(113, 20);
            this.dtpkTuNgay.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Từ ngày:";
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Image = global::MM.Properties.Resources.page_excel_icon;
            this.btnExportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExcel.Location = new System.Drawing.Point(7, 90);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(93, 25);
            this.btnExportExcel.TabIndex = 84;
            this.btnExportExcel.Text = "      &Xuất Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // _printDialog
            // 
            this._printDialog.AllowCurrentPage = true;
            this._printDialog.AllowSelection = true;
            this._printDialog.AllowSomePages = true;
            this._printDialog.ShowHelp = true;
            this._printDialog.UseEXDialog = true;
            // 
            // raChuaThuTien
            // 
            this.raChuaThuTien.AutoSize = true;
            this.raChuaThuTien.Location = new System.Drawing.Point(260, 49);
            this.raChuaThuTien.Name = "raChuaThuTien";
            this.raChuaThuTien.Size = new System.Drawing.Size(88, 17);
            this.raChuaThuTien.TabIndex = 30;
            this.raChuaThuTien.Text = "Chưa thu tiền";
            this.raChuaThuTien.UseVisualStyleBackColor = true;
            // 
            // raDaThuTien
            // 
            this.raDaThuTien.AutoSize = true;
            this.raDaThuTien.Location = new System.Drawing.Point(128, 49);
            this.raDaThuTien.Name = "raDaThuTien";
            this.raDaThuTien.Size = new System.Drawing.Size(77, 17);
            this.raDaThuTien.TabIndex = 29;
            this.raDaThuTien.Text = "Đã thu tiền";
            this.raDaThuTien.UseVisualStyleBackColor = true;
            // 
            // raAll
            // 
            this.raAll.AutoSize = true;
            this.raAll.Checked = true;
            this.raAll.Location = new System.Drawing.Point(19, 49);
            this.raAll.Name = "raAll";
            this.raAll.Size = new System.Drawing.Size(56, 17);
            this.raAll.TabIndex = 28;
            this.raAll.TabStop = true;
            this.raAll.Text = "Tất cả";
            this.raAll.UseVisualStyleBackColor = true;
            // 
            // uThongKeHoaDonDichVuVaThuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.groupBox1);
            this.Name = "uThongKeHoaDonDichVuVaThuoc";
            this.Size = new System.Drawing.Size(384, 121);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpkDenNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpkTuNgay;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.PrintDialog _printDialog;
        private System.Windows.Forms.RadioButton raChuaThuTien;
        private System.Windows.Forms.RadioButton raDaThuTien;
        private System.Windows.Forms.RadioButton raAll;
    }
}
