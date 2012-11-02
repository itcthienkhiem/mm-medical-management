namespace MM.Dialogs
{
    partial class dlgAddBenhNhanNgoaiGoiKham
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddBenhNhanNgoaiGoiKham));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgBenhNhanNgoaiGoiKham = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayKhamDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatientGUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patientGUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serviceGUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.lanDauStrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.benhNhanNgoaiGoiKhamViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBenhNhanNgoaiGoiKham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.benhNhanNgoaiGoiKhamViewBindingSource)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 445);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(820, 38);
            this.panel1.TabIndex = 20;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(398, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(319, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(820, 25);
            this.panel2.TabIndex = 13;
            // 
            // dgBenhNhanNgoaiGoiKham
            // 
            this.dgBenhNhanNgoaiGoiKham.AllowUserToOrderColumns = true;
            this.dgBenhNhanNgoaiGoiKham.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgBenhNhanNgoaiGoiKham.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgBenhNhanNgoaiGoiKham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBenhNhanNgoaiGoiKham.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNo,
            this.ngayKhamDataGridViewTextBoxColumn,
            this.PatientGUID,
            this.patientGUIDDataGridViewTextBoxColumn,
            this.serviceGUIDDataGridViewTextBoxColumn,
            this.lanDauStrDataGridViewTextBoxColumn});
            this.dgBenhNhanNgoaiGoiKham.DataSource = this.benhNhanNgoaiGoiKhamViewBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgBenhNhanNgoaiGoiKham.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgBenhNhanNgoaiGoiKham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgBenhNhanNgoaiGoiKham.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgBenhNhanNgoaiGoiKham.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgBenhNhanNgoaiGoiKham.HighlightSelectedColumnHeaders = false;
            this.dgBenhNhanNgoaiGoiKham.Location = new System.Drawing.Point(0, 0);
            this.dgBenhNhanNgoaiGoiKham.Name = "dgBenhNhanNgoaiGoiKham";
            this.dgBenhNhanNgoaiGoiKham.RowHeadersWidth = 30;
            this.dgBenhNhanNgoaiGoiKham.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBenhNhanNgoaiGoiKham.Size = new System.Drawing.Size(820, 420);
            this.dgBenhNhanNgoaiGoiKham.TabIndex = 5;
            this.dgBenhNhanNgoaiGoiKham.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBenhNhanNgoaiGoiKham_CellContentDoubleClick);
            this.dgBenhNhanNgoaiGoiKham.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBenhNhanNgoaiGoiKham_CellDoubleClick);
            this.dgBenhNhanNgoaiGoiKham.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.dgBenhNhanNgoaiGoiKham_CellParsing);
            this.dgBenhNhanNgoaiGoiKham.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgBenhNhanNgoaiGoiKham_CellValidating);
            this.dgBenhNhanNgoaiGoiKham.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgBenhNhanNgoaiGoiKham_EditingControlShowing);
            this.dgBenhNhanNgoaiGoiKham.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgBenhNhanNgoaiGoiKham_UserAddedRow);
            this.dgBenhNhanNgoaiGoiKham.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgBenhNhanNgoaiGoiKham_UserDeletedRow);
            // 
            // colNo
            // 
            this.colNo.HeaderText = "STT";
            this.colNo.Name = "colNo";
            this.colNo.ReadOnly = true;
            this.colNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colNo.Width = 40;
            // 
            // ngayKhamDataGridViewTextBoxColumn
            // 
            this.ngayKhamDataGridViewTextBoxColumn.DataPropertyName = "NgayKham";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            dataGridViewCellStyle2.NullValue = null;
            this.ngayKhamDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.ngayKhamDataGridViewTextBoxColumn.HeaderText = "Ngày khám";
            this.ngayKhamDataGridViewTextBoxColumn.Name = "ngayKhamDataGridViewTextBoxColumn";
            this.ngayKhamDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ngayKhamDataGridViewTextBoxColumn.Width = 110;
            // 
            // PatientGUID
            // 
            this.PatientGUID.DataPropertyName = "PatientGUID";
            this.PatientGUID.HeaderText = "PatientGUID";
            this.PatientGUID.Name = "PatientGUID";
            this.PatientGUID.ReadOnly = true;
            this.PatientGUID.Visible = false;
            // 
            // patientGUIDDataGridViewTextBoxColumn
            // 
            this.patientGUIDDataGridViewTextBoxColumn.DataPropertyName = "FullName";
            this.patientGUIDDataGridViewTextBoxColumn.HeaderText = "Tên bệnh nhân";
            this.patientGUIDDataGridViewTextBoxColumn.Name = "patientGUIDDataGridViewTextBoxColumn";
            this.patientGUIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.patientGUIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.patientGUIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.patientGUIDDataGridViewTextBoxColumn.Width = 250;
            // 
            // serviceGUIDDataGridViewTextBoxColumn
            // 
            this.serviceGUIDDataGridViewTextBoxColumn.DataPropertyName = "ServiceGUID";
            this.serviceGUIDDataGridViewTextBoxColumn.DisplayStyleForCurrentCellOnly = true;
            this.serviceGUIDDataGridViewTextBoxColumn.HeaderText = "Dịch vụ";
            this.serviceGUIDDataGridViewTextBoxColumn.Name = "serviceGUIDDataGridViewTextBoxColumn";
            this.serviceGUIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.serviceGUIDDataGridViewTextBoxColumn.Width = 250;
            // 
            // lanDauStrDataGridViewTextBoxColumn
            // 
            this.lanDauStrDataGridViewTextBoxColumn.DataPropertyName = "LanDauStr";
            this.lanDauStrDataGridViewTextBoxColumn.DisplayStyleForCurrentCellOnly = true;
            this.lanDauStrDataGridViewTextBoxColumn.HeaderText = "Lần đầu/Tái khám";
            this.lanDauStrDataGridViewTextBoxColumn.Items.AddRange(new object[] {
            "Lần đầu",
            "Tái khám"});
            this.lanDauStrDataGridViewTextBoxColumn.Name = "lanDauStrDataGridViewTextBoxColumn";
            this.lanDauStrDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.lanDauStrDataGridViewTextBoxColumn.Width = 120;
            // 
            // benhNhanNgoaiGoiKhamViewBindingSource
            // 
            this.benhNhanNgoaiGoiKhamViewBindingSource.DataSource = typeof(MM.Databasae.BenhNhanNgoaiGoiKhamView);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "NgayKham";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "dd/MM/yyyy";
            dataGridViewCellStyle4.NullValue = null;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn1.HeaderText = "NgayKham";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 110;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "LanDauStr";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Format = "dd/MM/yyyy";
            dataGridViewCellStyle5.NullValue = null;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn2.HeaderText = "LanDauStr";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 110;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgBenhNhanNgoaiGoiKham);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 25);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(820, 420);
            this.panel3.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(5, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(414, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lưu ý: nhấn đúp chuột vào ô tên bệnh nhân để nhập bệnh nhân.";
            // 
            // dlgAddBenhNhanNgoaiGoiKham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(820, 483);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddBenhNhanNgoaiGoiKham";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them benh nhan ngoai goi kham";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddBenhNhanNgoaiGoiKham_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddBenhNhanNgoaiGoiKham_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBenhNhanNgoaiGoiKham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.benhNhanNgoaiGoiKhamViewBindingSource)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgBenhNhanNgoaiGoiKham;
        private System.Windows.Forms.BindingSource benhNhanNgoaiGoiKhamViewBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayKhamDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatientGUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn patientGUIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn serviceGUIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn lanDauStrDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
    }
}