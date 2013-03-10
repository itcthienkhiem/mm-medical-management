namespace MM.Controls
{
    partial class uSMSLog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbKetQuaTimDuoc = new System.Windows.Forms.Label();
            this.raKhongThanhCong = new System.Windows.Forms.RadioButton();
            this.raThanhCong = new System.Windows.Forms.RadioButton();
            this.raTatCa = new System.Windows.Forms.RadioButton();
            this.btnView = new System.Windows.Forms.Button();
            this.dtpkDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpkTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgTracking = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.ngayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mobileDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.noiDungDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nguoiGuiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sMSLogViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTracking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sMSLogViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbKetQuaTimDuoc);
            this.panel1.Controls.Add(this.raKhongThanhCong);
            this.panel1.Controls.Add(this.raThanhCong);
            this.panel1.Controls.Add(this.raTatCa);
            this.panel1.Controls.Add(this.btnView);
            this.panel1.Controls.Add(this.dtpkDenNgay);
            this.panel1.Controls.Add(this.dtpkTuNgay);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1107, 95);
            this.panel1.TabIndex = 1;
            // 
            // lbKetQuaTimDuoc
            // 
            this.lbKetQuaTimDuoc.AutoSize = true;
            this.lbKetQuaTimDuoc.ForeColor = System.Drawing.Color.Blue;
            this.lbKetQuaTimDuoc.Location = new System.Drawing.Point(128, 68);
            this.lbKetQuaTimDuoc.Name = "lbKetQuaTimDuoc";
            this.lbKetQuaTimDuoc.Size = new System.Drawing.Size(100, 13);
            this.lbKetQuaTimDuoc.TabIndex = 17;
            this.lbKetQuaTimDuoc.Text = "Kết quả tìm được: 0";
            // 
            // raKhongThanhCong
            // 
            this.raKhongThanhCong.AutoSize = true;
            this.raKhongThanhCong.Location = new System.Drawing.Point(226, 37);
            this.raKhongThanhCong.Name = "raKhongThanhCong";
            this.raKhongThanhCong.Size = new System.Drawing.Size(113, 17);
            this.raKhongThanhCong.TabIndex = 16;
            this.raKhongThanhCong.Text = "Không thành công";
            this.raKhongThanhCong.UseVisualStyleBackColor = true;
            this.raKhongThanhCong.CheckedChanged += new System.EventHandler(this.raKhongThanhCong_CheckedChanged);
            // 
            // raThanhCong
            // 
            this.raThanhCong.AutoSize = true;
            this.raThanhCong.Checked = true;
            this.raThanhCong.Location = new System.Drawing.Point(114, 37);
            this.raThanhCong.Name = "raThanhCong";
            this.raThanhCong.Size = new System.Drawing.Size(83, 17);
            this.raThanhCong.TabIndex = 15;
            this.raThanhCong.TabStop = true;
            this.raThanhCong.Text = "Thành công";
            this.raThanhCong.UseVisualStyleBackColor = true;
            this.raThanhCong.CheckedChanged += new System.EventHandler(this.raThanhCong_CheckedChanged);
            // 
            // raTatCa
            // 
            this.raTatCa.AutoSize = true;
            this.raTatCa.Location = new System.Drawing.Point(24, 37);
            this.raTatCa.Name = "raTatCa";
            this.raTatCa.Size = new System.Drawing.Size(56, 17);
            this.raTatCa.TabIndex = 14;
            this.raTatCa.Text = "Tất cả";
            this.raTatCa.UseVisualStyleBackColor = true;
            this.raTatCa.CheckedChanged += new System.EventHandler(this.raTatCa_CheckedChanged);
            // 
            // btnView
            // 
            this.btnView.Image = global::MM.Properties.Resources.views_icon;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(24, 63);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 13;
            this.btnView.Text = "   &Xem";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // dtpkDenNgay
            // 
            this.dtpkDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkDenNgay.Location = new System.Drawing.Point(269, 11);
            this.dtpkDenNgay.Name = "dtpkDenNgay";
            this.dtpkDenNgay.Size = new System.Drawing.Size(121, 20);
            this.dtpkDenNgay.TabIndex = 3;
            // 
            // dtpkTuNgay
            // 
            this.dtpkTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkTuNgay.Location = new System.Drawing.Point(76, 11);
            this.dtpkTuNgay.Name = "dtpkTuNgay";
            this.dtpkTuNgay.Size = new System.Drawing.Size(121, 20);
            this.dtpkTuNgay.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(207, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Đến ngày:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ ngày:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgTracking);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 95);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1107, 314);
            this.panel2.TabIndex = 2;
            // 
            // dgTracking
            // 
            this.dgTracking.AllowUserToAddRows = false;
            this.dgTracking.AllowUserToDeleteRows = false;
            this.dgTracking.AllowUserToOrderColumns = true;
            this.dgTracking.AutoGenerateColumns = false;
            this.dgTracking.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgTracking.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgTracking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTracking.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ngayDataGridViewTextBoxColumn,
            this.fullNameDataGridViewTextBoxColumn,
            this.mobileDataGridViewTextBoxColumn,
            this.noiDungDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn,
            this.notesDataGridViewTextBoxColumn,
            this.nguoiGuiDataGridViewTextBoxColumn});
            this.dgTracking.DataSource = this.sMSLogViewBindingSource;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgTracking.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgTracking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTracking.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgTracking.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgTracking.HighlightSelectedColumnHeaders = false;
            this.dgTracking.Location = new System.Drawing.Point(0, 0);
            this.dgTracking.MultiSelect = false;
            this.dgTracking.Name = "dgTracking";
            this.dgTracking.RowHeadersWidth = 30;
            this.dgTracking.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgTracking.Size = new System.Drawing.Size(1107, 314);
            this.dgTracking.TabIndex = 1;
            // 
            // ngayDataGridViewTextBoxColumn
            // 
            this.ngayDataGridViewTextBoxColumn.DataPropertyName = "Ngay";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "dd/MM/yyyy HH:mm:ss";
            this.ngayDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.ngayDataGridViewTextBoxColumn.Frozen = true;
            this.ngayDataGridViewTextBoxColumn.HeaderText = "Ngày";
            this.ngayDataGridViewTextBoxColumn.Name = "ngayDataGridViewTextBoxColumn";
            this.ngayDataGridViewTextBoxColumn.ReadOnly = true;
            this.ngayDataGridViewTextBoxColumn.Width = 120;
            // 
            // fullNameDataGridViewTextBoxColumn
            // 
            this.fullNameDataGridViewTextBoxColumn.DataPropertyName = "FullName";
            this.fullNameDataGridViewTextBoxColumn.HeaderText = "Tên bệnh nhân";
            this.fullNameDataGridViewTextBoxColumn.Name = "fullNameDataGridViewTextBoxColumn";
            this.fullNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fullNameDataGridViewTextBoxColumn.Width = 250;
            // 
            // mobileDataGridViewTextBoxColumn
            // 
            this.mobileDataGridViewTextBoxColumn.DataPropertyName = "Mobile";
            this.mobileDataGridViewTextBoxColumn.HeaderText = "Mobile";
            this.mobileDataGridViewTextBoxColumn.Name = "mobileDataGridViewTextBoxColumn";
            this.mobileDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // noiDungDataGridViewTextBoxColumn
            // 
            this.noiDungDataGridViewTextBoxColumn.DataPropertyName = "NoiDung";
            this.noiDungDataGridViewTextBoxColumn.HeaderText = "Nội dung";
            this.noiDungDataGridViewTextBoxColumn.Name = "noiDungDataGridViewTextBoxColumn";
            this.noiDungDataGridViewTextBoxColumn.ReadOnly = true;
            this.noiDungDataGridViewTextBoxColumn.Width = 250;
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.statusDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.statusDataGridViewTextBoxColumn.HeaderText = "Trạng thái";
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            this.statusDataGridViewTextBoxColumn.ReadOnly = true;
            this.statusDataGridViewTextBoxColumn.Width = 80;
            // 
            // notesDataGridViewTextBoxColumn
            // 
            this.notesDataGridViewTextBoxColumn.DataPropertyName = "Notes";
            this.notesDataGridViewTextBoxColumn.HeaderText = "Ghi chú";
            this.notesDataGridViewTextBoxColumn.Name = "notesDataGridViewTextBoxColumn";
            this.notesDataGridViewTextBoxColumn.ReadOnly = true;
            this.notesDataGridViewTextBoxColumn.Width = 250;
            // 
            // nguoiGuiDataGridViewTextBoxColumn
            // 
            this.nguoiGuiDataGridViewTextBoxColumn.DataPropertyName = "NguoiGui";
            this.nguoiGuiDataGridViewTextBoxColumn.HeaderText = "Người gửi";
            this.nguoiGuiDataGridViewTextBoxColumn.Name = "nguoiGuiDataGridViewTextBoxColumn";
            this.nguoiGuiDataGridViewTextBoxColumn.ReadOnly = true;
            this.nguoiGuiDataGridViewTextBoxColumn.Width = 250;
            // 
            // sMSLogViewBindingSource
            // 
            this.sMSLogViewBindingSource.DataSource = typeof(MM.Databasae.SMSLogView);
            // 
            // uSMSLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uSMSLog";
            this.Size = new System.Drawing.Size(1107, 409);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTracking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sMSLogViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.DateTimePicker dtpkDenNgay;
        private System.Windows.Forms.DateTimePicker dtpkTuNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton raKhongThanhCong;
        private System.Windows.Forms.RadioButton raThanhCong;
        private System.Windows.Forms.RadioButton raTatCa;
        private System.Windows.Forms.Label lbKetQuaTimDuoc;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.BindingSource sMSLogViewBindingSource;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgTracking;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mobileDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noiDungDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn notesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nguoiGuiDataGridViewTextBoxColumn;
    }
}
