namespace MM.Dialogs
{
    partial class dlgAddMultiLoiKhuyen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddMultiLoiKhuyen));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkTheoMaTrieuChung = new System.Windows.Forms.CheckBox();
            this.txtTimTrieuChung = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgSymptom = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.codeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.symptomNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adviceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.cboDocStaff = new System.Windows.Forms.ComboBox();
            this.docStaffViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dtpkNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.symptomBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSymptom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.docStaffViewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.symptomBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkTheoMaTrieuChung);
            this.groupBox1.Controls.Add(this.txtTimTrieuChung);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.chkChecked);
            this.groupBox1.Controls.Add(this.dgSymptom);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboDocStaff);
            this.groupBox1.Controls.Add(this.dtpkNgay);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(537, 477);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin lời khuyên";
            // 
            // chkTheoMaTrieuChung
            // 
            this.chkTheoMaTrieuChung.AutoSize = true;
            this.chkTheoMaTrieuChung.Location = new System.Drawing.Point(393, 79);
            this.chkTheoMaTrieuChung.Name = "chkTheoMaTrieuChung";
            this.chkTheoMaTrieuChung.Size = new System.Drawing.Size(124, 17);
            this.chkTheoMaTrieuChung.TabIndex = 7;
            this.chkTheoMaTrieuChung.Text = "Theo mã triệu chứng";
            this.chkTheoMaTrieuChung.UseVisualStyleBackColor = true;
            this.chkTheoMaTrieuChung.CheckedChanged += new System.EventHandler(this.chkTheoMaTrieuChung_CheckedChanged);
            // 
            // txtTimTrieuChung
            // 
            this.txtTimTrieuChung.Location = new System.Drawing.Point(95, 76);
            this.txtTimTrieuChung.Name = "txtTimTrieuChung";
            this.txtTimTrieuChung.Size = new System.Drawing.Size(291, 20);
            this.txtTimTrieuChung.TabIndex = 6;
            this.txtTimTrieuChung.TextChanged += new System.EventHandler(this.txtTimTrieuChung_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Tìm triệu chứng:";
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(56, 106);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 8;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgSymptom
            // 
            this.dgSymptom.AllowUserToAddRows = false;
            this.dgSymptom.AllowUserToDeleteRows = false;
            this.dgSymptom.AllowUserToOrderColumns = true;
            this.dgSymptom.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSymptom.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgSymptom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSymptom.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.codeDataGridViewTextBoxColumn,
            this.symptomNameDataGridViewTextBoxColumn,
            this.adviceDataGridViewTextBoxColumn});
            this.dgSymptom.DataSource = this.bindingSource1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgSymptom.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgSymptom.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgSymptom.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgSymptom.HighlightSelectedColumnHeaders = false;
            this.dgSymptom.Location = new System.Drawing.Point(12, 101);
            this.dgSymptom.MultiSelect = false;
            this.dgSymptom.Name = "dgSymptom";
            this.dgSymptom.RowHeadersWidth = 30;
            this.dgSymptom.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSymptom.Size = new System.Drawing.Size(512, 365);
            this.dgSymptom.TabIndex = 8;
            this.dgSymptom.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgSymptom_CellMouseUp);
            // 
            // colChecked
            // 
            this.colChecked.DataPropertyName = "Checked";
            this.colChecked.Frozen = true;
            this.colChecked.HeaderText = "";
            this.colChecked.Name = "colChecked";
            this.colChecked.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colChecked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colChecked.Width = 40;
            // 
            // codeDataGridViewTextBoxColumn
            // 
            this.codeDataGridViewTextBoxColumn.DataPropertyName = "Code";
            this.codeDataGridViewTextBoxColumn.HeaderText = "Mã triệu chứng";
            this.codeDataGridViewTextBoxColumn.Name = "codeDataGridViewTextBoxColumn";
            this.codeDataGridViewTextBoxColumn.ReadOnly = true;
            this.codeDataGridViewTextBoxColumn.Width = 120;
            // 
            // symptomNameDataGridViewTextBoxColumn
            // 
            this.symptomNameDataGridViewTextBoxColumn.DataPropertyName = "SymptomName";
            this.symptomNameDataGridViewTextBoxColumn.HeaderText = "Triệu chứng";
            this.symptomNameDataGridViewTextBoxColumn.Name = "symptomNameDataGridViewTextBoxColumn";
            this.symptomNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.symptomNameDataGridViewTextBoxColumn.Width = 300;
            // 
            // adviceDataGridViewTextBoxColumn
            // 
            this.adviceDataGridViewTextBoxColumn.DataPropertyName = "Advice";
            this.adviceDataGridViewTextBoxColumn.HeaderText = "Lời khuyên";
            this.adviceDataGridViewTextBoxColumn.Name = "adviceDataGridViewTextBoxColumn";
            this.adviceDataGridViewTextBoxColumn.ReadOnly = true;
            this.adviceDataGridViewTextBoxColumn.Visible = false;
            this.adviceDataGridViewTextBoxColumn.Width = 350;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(MM.Databasae.Symptom);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Bác sĩ:";
            // 
            // cboDocStaff
            // 
            this.cboDocStaff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDocStaff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDocStaff.DataSource = this.docStaffViewBindingSource;
            this.cboDocStaff.DisplayMember = "FullName";
            this.cboDocStaff.FormattingEnabled = true;
            this.cboDocStaff.Location = new System.Drawing.Point(95, 45);
            this.cboDocStaff.Name = "cboDocStaff";
            this.cboDocStaff.Size = new System.Drawing.Size(291, 21);
            this.cboDocStaff.TabIndex = 5;
            this.cboDocStaff.ValueMember = "DocStaffGUID";
            // 
            // docStaffViewBindingSource
            // 
            this.docStaffViewBindingSource.DataSource = typeof(MM.Databasae.DocStaffView);
            // 
            // dtpkNgay
            // 
            this.dtpkNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpkNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpkNgay.Location = new System.Drawing.Point(95, 21);
            this.dtpkNgay.Name = "dtpkNgay";
            this.dtpkNgay.Size = new System.Drawing.Size(106, 20);
            this.dtpkNgay.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ngày:";
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
            this.btnCancel.Location = new System.Drawing.Point(278, 489);
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
            this.btnOK.Location = new System.Drawing.Point(199, 489);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // dlgAddMultiLoiKhuyen
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(552, 520);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddMultiLoiKhuyen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them loi khuyen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddLoiKhuyen_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddLoiKhuyen_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSymptom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
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
        private System.Windows.Forms.BindingSource symptomBindingSource;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgSymptom;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.CheckBox chkChecked;
        private System.Windows.Forms.CheckBox chkTheoMaTrieuChung;
        private System.Windows.Forms.TextBox txtTimTrieuChung;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn symptomNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn adviceDataGridViewTextBoxColumn;
    }
}