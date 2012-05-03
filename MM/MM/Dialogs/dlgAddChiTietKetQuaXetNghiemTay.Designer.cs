namespace MM.Dialogs
{
    partial class dlgAddChiTietKetQuaXetNghiemTay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddChiTietKetQuaXetNghiemTay));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnChonXetNghiem = new System.Windows.Forms.Button();
            this.cboXetNghiem = new System.Windows.Forms.ComboBox();
            this.xetNghiemManualBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xetNghiemManualBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtResult);
            this.groupBox1.Controls.Add(this.btnChonXetNghiem);
            this.groupBox1.Controls.Add(this.cboXetNghiem);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(440, 79);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnChonXetNghiem
            // 
            this.btnChonXetNghiem.Location = new System.Drawing.Point(323, 18);
            this.btnChonXetNghiem.Name = "btnChonXetNghiem";
            this.btnChonXetNghiem.Size = new System.Drawing.Size(106, 23);
            this.btnChonXetNghiem.TabIndex = 4;
            this.btnChonXetNghiem.Text = "Chọn xét nghiệm...";
            this.btnChonXetNghiem.UseVisualStyleBackColor = true;
            this.btnChonXetNghiem.Click += new System.EventHandler(this.btnChonXetNghiem_Click);
            // 
            // cboXetNghiem
            // 
            this.cboXetNghiem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboXetNghiem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboXetNghiem.DataSource = this.xetNghiemManualBindingSource;
            this.cboXetNghiem.DisplayMember = "Fullname";
            this.cboXetNghiem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboXetNghiem.FormattingEnabled = true;
            this.cboXetNghiem.Location = new System.Drawing.Point(79, 19);
            this.cboXetNghiem.Name = "cboXetNghiem";
            this.cboXetNghiem.Size = new System.Drawing.Size(239, 21);
            this.cboXetNghiem.TabIndex = 2;
            this.cboXetNghiem.ValueMember = "XetNghiem_ManualGUID";
            // 
            // xetNghiemManualBindingSource
            // 
            this.xetNghiemManualBindingSource.DataSource = typeof(MM.Databasae.XetNghiem_Manual);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Kết quả:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Xét nghiệm:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(227, 87);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(148, 87);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 17;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(79, 44);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(122, 20);
            this.txtResult.TabIndex = 5;
            // 
            // dlgAddChiTietKetQuaXetNghiemTay
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(450, 118);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddChiTietKetQuaXetNghiemTay";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them chi tiet ket qua xet nghiem tay";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddChiTietKetQuaXetNghiemTay_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddChiTietKetQuaXetNghiemTay_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xetNghiemManualBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cboXetNghiem;
        private System.Windows.Forms.BindingSource xetNghiemManualBindingSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChonXetNghiem;
        private System.Windows.Forms.TextBox txtResult;
    }
}