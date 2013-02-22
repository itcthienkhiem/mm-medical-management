namespace MM.Dialogs
{
    partial class dlgAddGiaDichVuHopDong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddGiaDichVuHopDong));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgDichVuCon = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.codeDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dichVuConViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.lbUnit = new System.Windows.Forms.Label();
            this.numPrice = new System.Windows.Forms.NumericUpDown();
            this.lbPrice = new System.Windows.Forms.Label();
            this.btnChonDichVu = new System.Windows.Forms.Button();
            this.cboService = new System.Windows.Forms.ComboBox();
            this.serviceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.ctmAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDichVuCon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dichVuConViewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceBindingSource)).BeginInit();
            this.ctmAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.chkChecked);
            this.groupBox1.Controls.Add(this.dgDichVuCon);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lbUnit);
            this.groupBox1.Controls.Add(this.numPrice);
            this.groupBox1.Controls.Add(this.lbPrice);
            this.groupBox1.Controls.Add(this.btnChonDichVu);
            this.groupBox1.Controls.Add(this.cboService);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(622, 370);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin dịch vụ hợp đồng";
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::MM.Properties.Resources.del;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(539, 128);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 19;
            this.btnDelete.Text = "    &Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::MM.Properties.Resources.add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(539, 99);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 25);
            this.btnAdd.TabIndex = 18;
            this.btnAdd.Text = "    &Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(57, 104);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 17;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgDichVuCon
            // 
            this.dgDichVuCon.AllowUserToAddRows = false;
            this.dgDichVuCon.AllowUserToDeleteRows = false;
            this.dgDichVuCon.AllowUserToOrderColumns = true;
            this.dgDichVuCon.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDichVuCon.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDichVuCon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDichVuCon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.codeDataGridViewTextBoxColumn2,
            this.nameDataGridViewTextBoxColumn2});
            this.dgDichVuCon.ContextMenuStrip = this.ctmAction;
            this.dgDichVuCon.DataSource = this.dichVuConViewBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDichVuCon.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgDichVuCon.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgDichVuCon.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgDichVuCon.HighlightSelectedColumnHeaders = false;
            this.dgDichVuCon.Location = new System.Drawing.Point(13, 99);
            this.dgDichVuCon.MultiSelect = false;
            this.dgDichVuCon.Name = "dgDichVuCon";
            this.dgDichVuCon.RowHeadersWidth = 30;
            this.dgDichVuCon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDichVuCon.Size = new System.Drawing.Size(520, 259);
            this.dgDichVuCon.TabIndex = 16;
            // 
            // colChecked
            // 
            this.colChecked.DataPropertyName = "Checked";
            this.colChecked.HeaderText = "";
            this.colChecked.Name = "colChecked";
            this.colChecked.Width = 40;
            // 
            // codeDataGridViewTextBoxColumn2
            // 
            this.codeDataGridViewTextBoxColumn2.DataPropertyName = "Code";
            this.codeDataGridViewTextBoxColumn2.HeaderText = "Mã dịch vụ";
            this.codeDataGridViewTextBoxColumn2.Name = "codeDataGridViewTextBoxColumn2";
            this.codeDataGridViewTextBoxColumn2.ReadOnly = true;
            this.codeDataGridViewTextBoxColumn2.Width = 150;
            // 
            // nameDataGridViewTextBoxColumn2
            // 
            this.nameDataGridViewTextBoxColumn2.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn2.HeaderText = "Tên dịch vụ";
            this.nameDataGridViewTextBoxColumn2.Name = "nameDataGridViewTextBoxColumn2";
            this.nameDataGridViewTextBoxColumn2.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn2.Width = 280;
            // 
            // dichVuConViewBindingSource
            // 
            this.dichVuConViewBindingSource.DataSource = typeof(MM.Databasae.DichVuConView);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(10, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "Dịch vụ con:";
            // 
            // lbUnit
            // 
            this.lbUnit.AutoSize = true;
            this.lbUnit.Location = new System.Drawing.Point(185, 50);
            this.lbUnit.Name = "lbUnit";
            this.lbUnit.Size = new System.Drawing.Size(36, 13);
            this.lbUnit.TabIndex = 13;
            this.lbUnit.Text = "(VNĐ)";
            // 
            // numPrice
            // 
            this.numPrice.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numPrice.Location = new System.Drawing.Point(60, 47);
            this.numPrice.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numPrice.Name = "numPrice";
            this.numPrice.Size = new System.Drawing.Size(121, 20);
            this.numPrice.TabIndex = 14;
            this.numPrice.ThousandsSeparator = true;
            // 
            // lbPrice
            // 
            this.lbPrice.AutoSize = true;
            this.lbPrice.Location = new System.Drawing.Point(10, 50);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Size = new System.Drawing.Size(26, 13);
            this.lbPrice.TabIndex = 12;
            this.lbPrice.Text = "Giá:";
            // 
            // btnChonDichVu
            // 
            this.btnChonDichVu.Location = new System.Drawing.Point(423, 21);
            this.btnChonDichVu.Name = "btnChonDichVu";
            this.btnChonDichVu.Size = new System.Drawing.Size(110, 23);
            this.btnChonDichVu.TabIndex = 3;
            this.btnChonDichVu.Text = "Chọn dịch vụ...";
            this.btnChonDichVu.UseVisualStyleBackColor = true;
            this.btnChonDichVu.Click += new System.EventHandler(this.btnChonDichVu_Click);
            // 
            // cboService
            // 
            this.cboService.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboService.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboService.DataSource = this.serviceBindingSource;
            this.cboService.DisplayMember = "Name";
            this.cboService.FormattingEnabled = true;
            this.cboService.Location = new System.Drawing.Point(60, 22);
            this.cboService.Name = "cboService";
            this.cboService.Size = new System.Drawing.Size(357, 21);
            this.cboService.TabIndex = 2;
            this.cboService.ValueMember = "ServiceGUID";
            this.cboService.SelectedValueChanged += new System.EventHandler(this.cboService_SelectedValueChanged);
            // 
            // serviceBindingSource
            // 
            this.serviceBindingSource.DataSource = typeof(MM.Databasae.Service);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dịch vụ:";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(318, 380);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.check;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(239, 380);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "    &Đồng ý";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // ctmAction
            // 
            this.ctmAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.toolStripSeparator1,
            this.deleteToolStripMenuItem});
            this.ctmAction.Name = "cmtAction";
            this.ctmAction.Size = new System.Drawing.Size(153, 76);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Image = global::MM.Properties.Resources.add;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addToolStripMenuItem.Text = "Thêm";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::MM.Properties.Resources.del;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "Xóa";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // dlgAddGiaDichVuHopDong
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(633, 411);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddGiaDichVuHopDong";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them dich vu hop dong";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddGiaDichVuHopDong_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddGiaDichVuHopDong_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDichVuCon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dichVuConViewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.serviceBindingSource)).EndInit();
            this.ctmAction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChonDichVu;
        private System.Windows.Forms.ComboBox cboService;
        private System.Windows.Forms.BindingSource serviceBindingSource;
        private System.Windows.Forms.Label lbUnit;
        private System.Windows.Forms.NumericUpDown numPrice;
        private System.Windows.Forms.Label lbPrice;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource dichVuConViewBindingSource;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgDichVuCon;
        private System.Windows.Forms.CheckBox chkChecked;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn codeDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn2;
        protected System.Windows.Forms.ContextMenuStrip ctmAction;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}