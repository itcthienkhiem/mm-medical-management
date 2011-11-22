namespace MM.Dialogs
{
    partial class dlgAddUserLogon
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddUserLogon));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboDocStaff = new System.Windows.Forms.ComboBox();
            this.docStaffViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgPermission = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.permissionViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.functionNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isViewDataGridViewCheckBoxColumn = new MM.Controls.DataGridViewDisableCheckBoxColumn();
            this.isAddDataGridViewCheckBoxColumn = new MM.Controls.DataGridViewDisableCheckBoxColumn();
            this.isEditDataGridViewCheckBoxColumn = new MM.Controls.DataGridViewDisableCheckBoxColumn();
            this.isDeleteDataGridViewCheckBoxColumn = new MM.Controls.DataGridViewDisableCheckBoxColumn();
            this.isPrintDataGridViewCheckBoxColumn = new MM.Controls.DataGridViewDisableCheckBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPermission)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.permissionViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboDocStaff);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(530, 85);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin người sử dụng";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(78, 49);
            this.txtPassword.MaxLength = 12;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(167, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mật khẩu:";
            // 
            // cboDocStaff
            // 
            this.cboDocStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboDocStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDocStaff.DataSource = this.docStaffViewBindingSource;
            this.cboDocStaff.DisplayMember = "FullName";
            this.cboDocStaff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDocStaff.FormattingEnabled = true;
            this.cboDocStaff.Location = new System.Drawing.Point(78, 23);
            this.cboDocStaff.Name = "cboDocStaff";
            this.cboDocStaff.Size = new System.Drawing.Size(271, 21);
            this.cboDocStaff.TabIndex = 1;
            this.cboDocStaff.ValueMember = "DocStaffGUID";
            // 
            // docStaffViewBindingSource
            // 
            this.docStaffViewBindingSource.DataSource = typeof(MM.Databasae.DocStaffView);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bác sĩ:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgPermission);
            this.groupBox2.Location = new System.Drawing.Point(8, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(530, 439);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Phân quyền";
            // 
            // dgPermission
            // 
            this.dgPermission.AllowUserToAddRows = false;
            this.dgPermission.AllowUserToDeleteRows = false;
            this.dgPermission.AllowUserToResizeColumns = false;
            this.dgPermission.AllowUserToResizeRows = false;
            this.dgPermission.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPermission.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPermission.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPermission.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.functionNameDataGridViewTextBoxColumn,
            this.isViewDataGridViewCheckBoxColumn,
            this.isAddDataGridViewCheckBoxColumn,
            this.isEditDataGridViewCheckBoxColumn,
            this.isDeleteDataGridViewCheckBoxColumn,
            this.isPrintDataGridViewCheckBoxColumn});
            this.dgPermission.DataSource = this.permissionViewBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPermission.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgPermission.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgPermission.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgPermission.HighlightSelectedColumnHeaders = false;
            this.dgPermission.Location = new System.Drawing.Point(12, 18);
            this.dgPermission.MultiSelect = false;
            this.dgPermission.Name = "dgPermission";
            this.dgPermission.RowHeadersVisible = false;
            this.dgPermission.RowHeadersWidth = 30;
            this.dgPermission.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPermission.Size = new System.Drawing.Size(506, 409);
            this.dgPermission.TabIndex = 4;
            // 
            // permissionViewBindingSource
            // 
            this.permissionViewBindingSource.DataSource = typeof(MM.Databasae.PermissionView);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(275, 542);
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
            this.btnOK.Location = new System.Drawing.Point(196, 542);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // functionNameDataGridViewTextBoxColumn
            // 
            this.functionNameDataGridViewTextBoxColumn.DataPropertyName = "FunctionName";
            this.functionNameDataGridViewTextBoxColumn.HeaderText = "Tên chức năng";
            this.functionNameDataGridViewTextBoxColumn.Name = "functionNameDataGridViewTextBoxColumn";
            this.functionNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.functionNameDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.functionNameDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.functionNameDataGridViewTextBoxColumn.Width = 230;
            // 
            // isViewDataGridViewCheckBoxColumn
            // 
            this.isViewDataGridViewCheckBoxColumn.DataPropertyName = "IsView";
            this.isViewDataGridViewCheckBoxColumn.HeaderText = "Xem";
            this.isViewDataGridViewCheckBoxColumn.Name = "isViewDataGridViewCheckBoxColumn";
            this.isViewDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.isViewDataGridViewCheckBoxColumn.Width = 50;
            // 
            // isAddDataGridViewCheckBoxColumn
            // 
            this.isAddDataGridViewCheckBoxColumn.DataPropertyName = "IsAdd";
            this.isAddDataGridViewCheckBoxColumn.HeaderText = "Thêm";
            this.isAddDataGridViewCheckBoxColumn.Name = "isAddDataGridViewCheckBoxColumn";
            this.isAddDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.isAddDataGridViewCheckBoxColumn.Width = 50;
            // 
            // isEditDataGridViewCheckBoxColumn
            // 
            this.isEditDataGridViewCheckBoxColumn.DataPropertyName = "IsEdit";
            this.isEditDataGridViewCheckBoxColumn.HeaderText = "Sửa";
            this.isEditDataGridViewCheckBoxColumn.Name = "isEditDataGridViewCheckBoxColumn";
            this.isEditDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.isEditDataGridViewCheckBoxColumn.Width = 50;
            // 
            // isDeleteDataGridViewCheckBoxColumn
            // 
            this.isDeleteDataGridViewCheckBoxColumn.DataPropertyName = "IsDelete";
            this.isDeleteDataGridViewCheckBoxColumn.HeaderText = "Xóa";
            this.isDeleteDataGridViewCheckBoxColumn.Name = "isDeleteDataGridViewCheckBoxColumn";
            this.isDeleteDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.isDeleteDataGridViewCheckBoxColumn.Width = 50;
            // 
            // isPrintDataGridViewCheckBoxColumn
            // 
            this.isPrintDataGridViewCheckBoxColumn.DataPropertyName = "IsPrint";
            this.isPrintDataGridViewCheckBoxColumn.HeaderText = "In";
            this.isPrintDataGridViewCheckBoxColumn.Name = "isPrintDataGridViewCheckBoxColumn";
            this.isPrintDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.isPrintDataGridViewCheckBoxColumn.Width = 50;
            // 
            // dlgAddUserLogon
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(547, 573);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddUserLogon";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them nguoi su dung";
            this.Load += new System.EventHandler(this.dlgAddUserLogon_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPermission)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.permissionViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboDocStaff;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.BindingSource docStaffViewBindingSource;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgPermission;
        private System.Windows.Forms.BindingSource permissionViewBindingSource;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn functionNameDataGridViewTextBoxColumn;
        private Controls.DataGridViewDisableCheckBoxColumn isViewDataGridViewCheckBoxColumn;
        private Controls.DataGridViewDisableCheckBoxColumn isAddDataGridViewCheckBoxColumn;
        private Controls.DataGridViewDisableCheckBoxColumn isEditDataGridViewCheckBoxColumn;
        private Controls.DataGridViewDisableCheckBoxColumn isDeleteDataGridViewCheckBoxColumn;
        private Controls.DataGridViewDisableCheckBoxColumn isPrintDataGridViewCheckBoxColumn;
    }
}