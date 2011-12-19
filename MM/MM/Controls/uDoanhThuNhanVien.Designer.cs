namespace MM.Controls
{
    partial class uDoanhThuNhanVien
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.raReceipt = new System.Windows.Forms.RadioButton();
            this.raServiceHistory = new System.Windows.Forms.RadioButton();
            this.raChiTiet = new System.Windows.Forms.RadioButton();
            this.raTongHop = new System.Windows.Forms.RadioButton();
            this.btnView = new System.Windows.Forms.Button();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.docStaffViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.dtpkToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpkFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this._ucReportViewer = new MM.Controls.ucReportViewer();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 435);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(849, 38);
            this.panel1.TabIndex = 3;
            this.panel1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.raChiTiet);
            this.panel2.Controls.Add(this.raTongHop);
            this.panel2.Controls.Add(this.btnView);
            this.panel2.Controls.Add(this.cboNhanVien);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.dtpkToDate);
            this.panel2.Controls.Add(this.dtpkFromDate);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(849, 138);
            this.panel2.TabIndex = 4;
            // 
            // raReceipt
            // 
            this.raReceipt.AutoSize = true;
            this.raReceipt.Location = new System.Drawing.Point(143, 3);
            this.raReceipt.Name = "raReceipt";
            this.raReceipt.Size = new System.Drawing.Size(85, 17);
            this.raReceipt.TabIndex = 10;
            this.raReceipt.Text = "Từ phiếu thu";
            this.raReceipt.UseVisualStyleBackColor = true;
            // 
            // raServiceHistory
            // 
            this.raServiceHistory.AutoSize = true;
            this.raServiceHistory.Checked = true;
            this.raServiceHistory.Location = new System.Drawing.Point(5, 3);
            this.raServiceHistory.Name = "raServiceHistory";
            this.raServiceHistory.Size = new System.Drawing.Size(121, 17);
            this.raServiceHistory.TabIndex = 9;
            this.raServiceHistory.TabStop = true;
            this.raServiceHistory.Text = "Từ dịch vụ đã khám";
            this.raServiceHistory.UseVisualStyleBackColor = true;
            // 
            // raChiTiet
            // 
            this.raChiTiet.AutoSize = true;
            this.raChiTiet.Location = new System.Drawing.Point(215, 61);
            this.raChiTiet.Name = "raChiTiet";
            this.raChiTiet.Size = new System.Drawing.Size(57, 17);
            this.raChiTiet.TabIndex = 8;
            this.raChiTiet.Text = "Chi tiết";
            this.raChiTiet.UseVisualStyleBackColor = true;
            // 
            // raTongHop
            // 
            this.raTongHop.AutoSize = true;
            this.raTongHop.Checked = true;
            this.raTongHop.Location = new System.Drawing.Point(77, 61);
            this.raTongHop.Name = "raTongHop";
            this.raTongHop.Size = new System.Drawing.Size(71, 17);
            this.raTongHop.TabIndex = 7;
            this.raTongHop.TabStop = true;
            this.raTongHop.Text = "Tổng hợp";
            this.raTongHop.UseVisualStyleBackColor = true;
            // 
            // btnView
            // 
            this.btnView.Image = global::MM.Properties.Resources.views_icon;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(77, 108);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 6;
            this.btnView.Text = "   &Xem";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboNhanVien.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboNhanVien.DataSource = this.docStaffViewBindingSource;
            this.cboNhanVien.DisplayMember = "FullName";
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVien.FormattingEnabled = true;
            this.cboNhanVien.Location = new System.Drawing.Point(77, 34);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(301, 21);
            this.cboNhanVien.TabIndex = 5;
            this.cboNhanVien.ValueMember = "DocStaffGUID";
            // 
            // docStaffViewBindingSource
            // 
            this.docStaffViewBindingSource.DataSource = typeof(MM.Databasae.DocStaffView);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nhân viên:";
            // 
            // dtpkToDate
            // 
            this.dtpkToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpkToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkToDate.Location = new System.Drawing.Point(264, 10);
            this.dtpkToDate.Name = "dtpkToDate";
            this.dtpkToDate.Size = new System.Drawing.Size(114, 20);
            this.dtpkToDate.TabIndex = 3;
            // 
            // dtpkFromDate
            // 
            this.dtpkFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpkFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkFromDate.Location = new System.Drawing.Point(77, 10);
            this.dtpkFromDate.Name = "dtpkFromDate";
            this.dtpkFromDate.Size = new System.Drawing.Size(114, 20);
            this.dtpkFromDate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Đến ngày:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ ngày:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this._ucReportViewer);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 138);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(849, 297);
            this.panel3.TabIndex = 5;
            // 
            // _ucReportViewer
            // 
            this._ucReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ucReportViewer.Location = new System.Drawing.Point(0, 0);
            this._ucReportViewer.Name = "_ucReportViewer";
            this._ucReportViewer.Size = new System.Drawing.Size(849, 297);
            this._ucReportViewer.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.raReceipt);
            this.panel4.Controls.Add(this.raServiceHistory);
            this.panel4.Location = new System.Drawing.Point(72, 82);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(242, 22);
            this.panel4.TabIndex = 11;
            // 
            // uDoanhThuNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uDoanhThuNhanVien";
            this.Size = new System.Drawing.Size(849, 473);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpkToDate;
        private System.Windows.Forms.DateTimePicker dtpkFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource docStaffViewBindingSource;
        private System.Windows.Forms.Button btnView;
        private ucReportViewer _ucReportViewer;
        private System.Windows.Forms.RadioButton raChiTiet;
        private System.Windows.Forms.RadioButton raTongHop;
        private System.Windows.Forms.RadioButton raReceipt;
        private System.Windows.Forms.RadioButton raServiceHistory;
        private System.Windows.Forms.Panel panel4;
    }
}
