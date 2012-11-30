namespace MM.Dialogs
{
    partial class dlgAddXetNghiemTay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddXetNghiemTay));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbXetNghiem = new System.Windows.Forms.GroupBox();
            this._uNormal = new MM.Controls.uNormal();
            this.numThuTuNhom = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.cboNhomXetNghiem = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.numThuTu = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.cboLoaiXetNghiem = new System.Windows.Forms.ComboBox();
            this.txtTenXetNghiem = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbXetNghiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThuTuNhom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThuTu)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(238, 599);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(317, 599);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // gbXetNghiem
            // 
            this.gbXetNghiem.Controls.Add(this._uNormal);
            this.gbXetNghiem.Controls.Add(this.numThuTuNhom);
            this.gbXetNghiem.Controls.Add(this.label11);
            this.gbXetNghiem.Controls.Add(this.cboNhomXetNghiem);
            this.gbXetNghiem.Controls.Add(this.label10);
            this.gbXetNghiem.Controls.Add(this.numThuTu);
            this.gbXetNghiem.Controls.Add(this.label9);
            this.gbXetNghiem.Controls.Add(this.cboLoaiXetNghiem);
            this.gbXetNghiem.Controls.Add(this.txtTenXetNghiem);
            this.gbXetNghiem.Controls.Add(this.label2);
            this.gbXetNghiem.Controls.Add(this.label1);
            this.gbXetNghiem.Location = new System.Drawing.Point(6, 2);
            this.gbXetNghiem.Name = "gbXetNghiem";
            this.gbXetNghiem.Size = new System.Drawing.Size(619, 591);
            this.gbXetNghiem.TabIndex = 0;
            this.gbXetNghiem.TabStop = false;
            // 
            // _uNormal
            // 
            this._uNormal.Location = new System.Drawing.Point(11, 145);
            this._uNormal.Name = "_uNormal";
            this._uNormal.Size = new System.Drawing.Size(597, 438);
            this._uNormal.TabIndex = 15;
            // 
            // numThuTuNhom
            // 
            this.numThuTuNhom.Location = new System.Drawing.Point(105, 93);
            this.numThuTuNhom.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numThuTuNhom.Name = "numThuTuNhom";
            this.numThuTuNhom.Size = new System.Drawing.Size(74, 20);
            this.numThuTuNhom.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(32, 95);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Thứ tự nhóm:";
            // 
            // cboNhomXetNghiem
            // 
            this.cboNhomXetNghiem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboNhomXetNghiem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboNhomXetNghiem.FormattingEnabled = true;
            this.cboNhomXetNghiem.Location = new System.Drawing.Point(105, 68);
            this.cboNhomXetNghiem.MaxDropDownItems = 15;
            this.cboNhomXetNghiem.Name = "cboNhomXetNghiem";
            this.cboNhomXetNghiem.Size = new System.Drawing.Size(503, 21);
            this.cboNhomXetNghiem.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Nhóm xét nghiệm:";
            // 
            // numThuTu
            // 
            this.numThuTu.Location = new System.Drawing.Point(105, 117);
            this.numThuTu.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numThuTu.Name = "numThuTu";
            this.numThuTu.Size = new System.Drawing.Size(74, 20);
            this.numThuTu.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 119);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Thứ tự xét nghiệm:";
            // 
            // cboLoaiXetNghiem
            // 
            this.cboLoaiXetNghiem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboLoaiXetNghiem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboLoaiXetNghiem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiXetNghiem.FormattingEnabled = true;
            this.cboLoaiXetNghiem.Items.AddRange(new object[] {
            "Huyết học",
            "Sinh hóa",
            "Miễn dịch",
            "Nước tiểu",
            "Khác"});
            this.cboLoaiXetNghiem.Location = new System.Drawing.Point(105, 43);
            this.cboLoaiXetNghiem.Name = "cboLoaiXetNghiem";
            this.cboLoaiXetNghiem.Size = new System.Drawing.Size(160, 21);
            this.cboLoaiXetNghiem.TabIndex = 3;
            // 
            // txtTenXetNghiem
            // 
            this.txtTenXetNghiem.Location = new System.Drawing.Point(105, 19);
            this.txtTenXetNghiem.Name = "txtTenXetNghiem";
            this.txtTenXetNghiem.Size = new System.Drawing.Size(281, 20);
            this.txtTenXetNghiem.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Loại xét nghiệm:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên xét nghiệm:";
            // 
            // dlgAddXetNghiemTay
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(631, 629);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbXetNghiem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddXetNghiemTay";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them xet nghiem tay";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddXetNghiemTay_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddXetNghiemTay_Load);
            this.gbXetNghiem.ResumeLayout(false);
            this.gbXetNghiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThuTuNhom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThuTu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbXetNghiem;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboLoaiXetNghiem;
        private System.Windows.Forms.TextBox txtTenXetNghiem;
        private System.Windows.Forms.NumericUpDown numThuTu;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboNhomXetNghiem;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numThuTuNhom;
        private System.Windows.Forms.Label label11;
        private Controls.uNormal _uNormal;
    }
}