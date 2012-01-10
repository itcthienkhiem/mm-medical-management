namespace MM.Dialogs
{
    partial class dlgAddLoiKhuyen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddLoiKhuyen));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpkNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.docStaffViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cboDocStaff = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboTrieuChung = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLoiKhuyen = new System.Windows.Forms.TextBox();
            this.symptomBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.symptomBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtLoiKhuyen);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboTrieuChung);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboDocStaff);
            this.groupBox1.Controls.Add(this.dtpkNgay);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(488, 230);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin lời khuyên";
            // 
            // dtpkNgay
            // 
            this.dtpkNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkNgay.Location = new System.Drawing.Point(81, 20);
            this.dtpkNgay.Name = "dtpkNgay";
            this.dtpkNgay.Size = new System.Drawing.Size(106, 20);
            this.dtpkNgay.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ngày:";
            // 
            // docStaffViewBindingSource
            // 
            this.docStaffViewBindingSource.DataSource = typeof(MM.Databasae.DocStaffView);
            // 
            // cboDocStaff
            // 
            this.cboDocStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboDocStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDocStaff.DataSource = this.docStaffViewBindingSource;
            this.cboDocStaff.DisplayMember = "FullName";
            this.cboDocStaff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDocStaff.FormattingEnabled = true;
            this.cboDocStaff.Location = new System.Drawing.Point(81, 44);
            this.cboDocStaff.Name = "cboDocStaff";
            this.cboDocStaff.Size = new System.Drawing.Size(242, 21);
            this.cboDocStaff.TabIndex = 5;
            this.cboDocStaff.ValueMember = "DocStaffGUID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Bác sĩ:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Triệu chứng:";
            // 
            // cboTrieuChung
            // 
            this.cboTrieuChung.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboTrieuChung.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboTrieuChung.DataSource = this.symptomBindingSource;
            this.cboTrieuChung.DisplayMember = "SymptomName";
            this.cboTrieuChung.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrieuChung.FormattingEnabled = true;
            this.cboTrieuChung.Location = new System.Drawing.Point(81, 69);
            this.cboTrieuChung.Name = "cboTrieuChung";
            this.cboTrieuChung.Size = new System.Drawing.Size(393, 21);
            this.cboTrieuChung.TabIndex = 8;
            this.cboTrieuChung.ValueMember = "SymptomGUID";
            this.cboTrieuChung.SelectedIndexChanged += new System.EventHandler(this.cboTrieuChung_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Lời khuyên:";
            // 
            // txtLoiKhuyen
            // 
            this.txtLoiKhuyen.Location = new System.Drawing.Point(81, 94);
            this.txtLoiKhuyen.MaxLength = 4000;
            this.txtLoiKhuyen.Multiline = true;
            this.txtLoiKhuyen.Name = "txtLoiKhuyen";
            this.txtLoiKhuyen.ReadOnly = true;
            this.txtLoiKhuyen.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLoiKhuyen.Size = new System.Drawing.Size(393, 122);
            this.txtLoiKhuyen.TabIndex = 10;
            // 
            // symptomBindingSource
            // 
            this.symptomBindingSource.DataSource = typeof(MM.Databasae.Symptom);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(253, 242);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(174, 242);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // dlgAddLoiKhuyen
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(503, 273);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddLoiKhuyen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them loi khuyen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddLoiKhuyen_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddLoiKhuyen_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.symptomBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpkNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboDocStaff;
        private System.Windows.Forms.BindingSource docStaffViewBindingSource;
        private System.Windows.Forms.TextBox txtLoiKhuyen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboTrieuChung;
        private System.Windows.Forms.BindingSource symptomBindingSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}