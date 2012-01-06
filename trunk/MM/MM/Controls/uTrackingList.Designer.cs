namespace MM.Controls
{
    partial class uTrackingList
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
            this.btnView = new System.Windows.Forms.Button();
            this.chkXoa = new System.Windows.Forms.CheckBox();
            this.chkSua = new System.Windows.Forms.CheckBox();
            this.chkThem = new System.Windows.Forms.CheckBox();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.docStaffViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.dtpkDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpkTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgTracking = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.trackingViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.trackingDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActionTypeStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTracking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackingViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnView);
            this.panel1.Controls.Add(this.chkXoa);
            this.panel1.Controls.Add(this.chkSua);
            this.panel1.Controls.Add(this.chkThem);
            this.panel1.Controls.Add(this.cboNhanVien);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dtpkDenNgay);
            this.panel1.Controls.Add(this.dtpkTuNgay);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(907, 117);
            this.panel1.TabIndex = 0;
            // 
            // btnView
            // 
            this.btnView.Image = global::MM.Properties.Resources.views_icon;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(76, 83);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 13;
            this.btnView.Text = "   &Xem";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // chkXoa
            // 
            this.chkXoa.AutoSize = true;
            this.chkXoa.Checked = true;
            this.chkXoa.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkXoa.Location = new System.Drawing.Point(252, 60);
            this.chkXoa.Name = "chkXoa";
            this.chkXoa.Size = new System.Drawing.Size(45, 17);
            this.chkXoa.TabIndex = 8;
            this.chkXoa.Text = "Xóa";
            this.chkXoa.UseVisualStyleBackColor = true;
            // 
            // chkSua
            // 
            this.chkSua.AutoSize = true;
            this.chkSua.Checked = true;
            this.chkSua.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSua.Location = new System.Drawing.Point(163, 60);
            this.chkSua.Name = "chkSua";
            this.chkSua.Size = new System.Drawing.Size(45, 17);
            this.chkSua.TabIndex = 7;
            this.chkSua.Text = "Sửa";
            this.chkSua.UseVisualStyleBackColor = true;
            // 
            // chkThem
            // 
            this.chkThem.AutoSize = true;
            this.chkThem.Checked = true;
            this.chkThem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkThem.Location = new System.Drawing.Point(76, 60);
            this.chkThem.Name = "chkThem";
            this.chkThem.Size = new System.Drawing.Size(53, 17);
            this.chkThem.TabIndex = 6;
            this.chkThem.Text = "Thêm";
            this.chkThem.UseVisualStyleBackColor = true;
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.DataSource = this.docStaffViewBindingSource;
            this.cboNhanVien.DisplayMember = "FullName";
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVien.FormattingEnabled = true;
            this.cboNhanVien.Location = new System.Drawing.Point(76, 34);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(314, 21);
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
            this.label3.Location = new System.Drawing.Point(11, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nhân viên:";
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
            this.panel2.Location = new System.Drawing.Point(0, 117);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(907, 442);
            this.panel2.TabIndex = 1;
            // 
            // dgTracking
            // 
            this.dgTracking.AllowUserToAddRows = false;
            this.dgTracking.AllowUserToDeleteRows = false;
            this.dgTracking.AllowUserToOrderColumns = true;
            this.dgTracking.AutoGenerateColumns = false;
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
            this.trackingDateDataGridViewTextBoxColumn,
            this.fullNameDataGridViewTextBoxColumn,
            this.actionDataGridViewTextBoxColumn,
            this.ActionTypeStr});
            this.dgTracking.DataSource = this.trackingViewBindingSource;
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
            this.dgTracking.ReadOnly = true;
            this.dgTracking.RowHeadersWidth = 30;
            this.dgTracking.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgTracking.Size = new System.Drawing.Size(907, 442);
            this.dgTracking.TabIndex = 3;
            this.dgTracking.DoubleClick += new System.EventHandler(this.dgTracking_DoubleClick);
            // 
            // trackingViewBindingSource
            // 
            this.trackingViewBindingSource.DataSource = typeof(MM.Databasae.TrackingView);
            // 
            // trackingDateDataGridViewTextBoxColumn
            // 
            this.trackingDateDataGridViewTextBoxColumn.DataPropertyName = "TrackingDate";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle2.NullValue = null;
            this.trackingDateDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.trackingDateDataGridViewTextBoxColumn.HeaderText = "Ngày truy vết";
            this.trackingDateDataGridViewTextBoxColumn.Name = "trackingDateDataGridViewTextBoxColumn";
            this.trackingDateDataGridViewTextBoxColumn.ReadOnly = true;
            this.trackingDateDataGridViewTextBoxColumn.Width = 120;
            // 
            // fullNameDataGridViewTextBoxColumn
            // 
            this.fullNameDataGridViewTextBoxColumn.DataPropertyName = "FullName";
            this.fullNameDataGridViewTextBoxColumn.HeaderText = "Tên nhân viên";
            this.fullNameDataGridViewTextBoxColumn.Name = "fullNameDataGridViewTextBoxColumn";
            this.fullNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fullNameDataGridViewTextBoxColumn.Width = 250;
            // 
            // actionDataGridViewTextBoxColumn
            // 
            this.actionDataGridViewTextBoxColumn.DataPropertyName = "Action";
            this.actionDataGridViewTextBoxColumn.HeaderText = "Hành động";
            this.actionDataGridViewTextBoxColumn.Name = "actionDataGridViewTextBoxColumn";
            this.actionDataGridViewTextBoxColumn.ReadOnly = true;
            this.actionDataGridViewTextBoxColumn.Width = 350;
            // 
            // ActionTypeStr
            // 
            this.ActionTypeStr.DataPropertyName = "ActionTypeStr";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ActionTypeStr.DefaultCellStyle = dataGridViewCellStyle3;
            this.ActionTypeStr.HeaderText = "Loại";
            this.ActionTypeStr.Name = "ActionTypeStr";
            this.ActionTypeStr.ReadOnly = true;
            this.ActionTypeStr.Width = 80;
            // 
            // uTrackingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uTrackingList";
            this.Size = new System.Drawing.Size(907, 559);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTracking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackingViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.BindingSource docStaffViewBindingSource;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpkDenNgay;
        private System.Windows.Forms.DateTimePicker dtpkTuNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkXoa;
        private System.Windows.Forms.CheckBox chkSua;
        private System.Windows.Forms.CheckBox chkThem;
        private System.Windows.Forms.Button btnView;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgTracking;
        private System.Windows.Forms.BindingSource trackingViewBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn trackingDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn actionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActionTypeStr;
    }
}
