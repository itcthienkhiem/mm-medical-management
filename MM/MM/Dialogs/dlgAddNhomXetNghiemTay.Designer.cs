namespace MM.Dialogs
{
    partial class dlgAddNhomXetNghiemTay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddNhomXetNghiemTay));
            this.xetNghiemManualBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvNhomXN = new System.Windows.Forms.ListView();
            this.colNhomXN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dtpkNgayXetNghiem = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.xetNghiemManualBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xetNghiemManualBindingSource
            // 
            this.xetNghiemManualBindingSource.DataSource = typeof(MM.Databasae.XetNghiem_Manual);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.check;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(124, 415);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 22;
            this.btnOK.Text = "    &Đồng ý";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(203, 415);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvNhomXN);
            this.groupBox1.Controls.Add(this.dtpkNgayXetNghiem);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(5, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(393, 407);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lvNhomXN
            // 
            this.lvNhomXN.CheckBoxes = true;
            this.lvNhomXN.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNhomXN});
            this.lvNhomXN.FullRowSelect = true;
            this.lvNhomXN.GridLines = true;
            this.lvNhomXN.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvNhomXN.Location = new System.Drawing.Point(10, 44);
            this.lvNhomXN.MultiSelect = false;
            this.lvNhomXN.Name = "lvNhomXN";
            this.lvNhomXN.Size = new System.Drawing.Size(374, 352);
            this.lvNhomXN.TabIndex = 9;
            this.lvNhomXN.UseCompatibleStateImageBehavior = false;
            this.lvNhomXN.View = System.Windows.Forms.View.Details;
            // 
            // colNhomXN
            // 
            this.colNhomXN.Text = "Nhóm xét nghiệm";
            this.colNhomXN.Width = 370;
            // 
            // dtpkNgayXetNghiem
            // 
            this.dtpkNgayXetNghiem.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dtpkNgayXetNghiem.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkNgayXetNghiem.Location = new System.Drawing.Point(102, 18);
            this.dtpkNgayXetNghiem.Name = "dtpkNgayXetNghiem";
            this.dtpkNgayXetNghiem.Size = new System.Drawing.Size(143, 20);
            this.dtpkNgayXetNghiem.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Ngày xét nghiệm:";
            // 
            // dlgAddNhomXetNghiemTay
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(403, 445);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddNhomXetNghiemTay";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them nhom xet nghiem tay";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddChiTietKetQuaXetNghiemTay_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddChiTietKetQuaXetNghiemTay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xetNghiemManualBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.BindingSource xetNghiemManualBindingSource;
        private System.Windows.Forms.DateTimePicker dtpkNgayXetNghiem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvNhomXN;
        private System.Windows.Forms.ColumnHeader colNhomXN;
        private System.Windows.Forms.Button btnOK;
    }
}