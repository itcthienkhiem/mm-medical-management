namespace MM.Controls
{
    partial class uInvoiceList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkChecked = new System.Windows.Forms.CheckBox();
            this.dgInvoice = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.invoiceViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._printDialog = new System.Windows.Forms.PrintDialog();
            this.colChecked = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.invoiceCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoiceDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fullNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenDonViDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.soTaiKhoanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hinhThucThanhToanStrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vATDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 433);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(864, 38);
            this.panel1.TabIndex = 2;
            // 
            // btnPrint
            // 
            this.btnPrint.Image = global::MM.Properties.Resources.Printer_icon__1_;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(87, 6);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(97, 25);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "      &In hóa đơn";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::MM.Properties.Resources.del;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(8, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "    &Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkChecked);
            this.panel2.Controls.Add(this.dgInvoice);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(864, 433);
            this.panel2.TabIndex = 3;
            // 
            // chkChecked
            // 
            this.chkChecked.AutoSize = true;
            this.chkChecked.Location = new System.Drawing.Point(45, 5);
            this.chkChecked.Name = "chkChecked";
            this.chkChecked.Size = new System.Drawing.Size(15, 14);
            this.chkChecked.TabIndex = 5;
            this.chkChecked.UseVisualStyleBackColor = true;
            this.chkChecked.CheckedChanged += new System.EventHandler(this.chkChecked_CheckedChanged);
            // 
            // dgInvoice
            // 
            this.dgInvoice.AllowUserToAddRows = false;
            this.dgInvoice.AllowUserToDeleteRows = false;
            this.dgInvoice.AllowUserToOrderColumns = true;
            this.dgInvoice.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgInvoice.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgInvoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgInvoice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChecked,
            this.invoiceCodeDataGridViewTextBoxColumn,
            this.invoiceDateDataGridViewTextBoxColumn,
            this.fullNameDataGridViewTextBoxColumn,
            this.tenDonViDataGridViewTextBoxColumn,
            this.addressDataGridViewTextBoxColumn,
            this.soTaiKhoanDataGridViewTextBoxColumn,
            this.hinhThucThanhToanStrDataGridViewTextBoxColumn,
            this.vATDataGridViewTextBoxColumn});
            this.dgInvoice.DataSource = this.invoiceViewBindingSource;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgInvoice.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgInvoice.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgInvoice.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgInvoice.HighlightSelectedColumnHeaders = false;
            this.dgInvoice.Location = new System.Drawing.Point(0, 0);
            this.dgInvoice.MultiSelect = false;
            this.dgInvoice.Name = "dgInvoice";
            this.dgInvoice.ReadOnly = true;
            this.dgInvoice.RowHeadersWidth = 30;
            this.dgInvoice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgInvoice.Size = new System.Drawing.Size(864, 433);
            this.dgInvoice.TabIndex = 4;
            this.dgInvoice.DoubleClick += new System.EventHandler(this.dgInvoice_DoubleClick);
            // 
            // invoiceViewBindingSource
            // 
            this.invoiceViewBindingSource.DataSource = typeof(MM.Databasae.InvoiceView);
            // 
            // _printDialog
            // 
            this._printDialog.UseEXDialog = true;
            // 
            // colChecked
            // 
            this.colChecked.Checked = true;
            this.colChecked.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.colChecked.CheckValue = "N";
            this.colChecked.DataPropertyName = "Checked";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colChecked.DefaultCellStyle = dataGridViewCellStyle2;
            this.colChecked.Frozen = true;
            this.colChecked.HeaderText = "";
            this.colChecked.Name = "colChecked";
            this.colChecked.ReadOnly = true;
            this.colChecked.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colChecked.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colChecked.Width = 40;
            // 
            // invoiceCodeDataGridViewTextBoxColumn
            // 
            this.invoiceCodeDataGridViewTextBoxColumn.DataPropertyName = "InvoiceCode";
            this.invoiceCodeDataGridViewTextBoxColumn.HeaderText = "Mã hóa đơn";
            this.invoiceCodeDataGridViewTextBoxColumn.Name = "invoiceCodeDataGridViewTextBoxColumn";
            this.invoiceCodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // invoiceDateDataGridViewTextBoxColumn
            // 
            this.invoiceDateDataGridViewTextBoxColumn.DataPropertyName = "InvoiceDate";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "dd/MM/yyyy";
            dataGridViewCellStyle3.NullValue = null;
            this.invoiceDateDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.invoiceDateDataGridViewTextBoxColumn.HeaderText = "Ngày xuất";
            this.invoiceDateDataGridViewTextBoxColumn.Name = "invoiceDateDataGridViewTextBoxColumn";
            this.invoiceDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fullNameDataGridViewTextBoxColumn
            // 
            this.fullNameDataGridViewTextBoxColumn.DataPropertyName = "FullName";
            this.fullNameDataGridViewTextBoxColumn.HeaderText = "Người mua hàng";
            this.fullNameDataGridViewTextBoxColumn.Name = "fullNameDataGridViewTextBoxColumn";
            this.fullNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fullNameDataGridViewTextBoxColumn.Width = 200;
            // 
            // tenDonViDataGridViewTextBoxColumn
            // 
            this.tenDonViDataGridViewTextBoxColumn.DataPropertyName = "TenDonVi";
            this.tenDonViDataGridViewTextBoxColumn.HeaderText = "Tên đơn vị";
            this.tenDonViDataGridViewTextBoxColumn.Name = "tenDonViDataGridViewTextBoxColumn";
            this.tenDonViDataGridViewTextBoxColumn.ReadOnly = true;
            this.tenDonViDataGridViewTextBoxColumn.Width = 200;
            // 
            // addressDataGridViewTextBoxColumn
            // 
            this.addressDataGridViewTextBoxColumn.DataPropertyName = "Address";
            this.addressDataGridViewTextBoxColumn.HeaderText = "Địa chỉ";
            this.addressDataGridViewTextBoxColumn.Name = "addressDataGridViewTextBoxColumn";
            this.addressDataGridViewTextBoxColumn.ReadOnly = true;
            this.addressDataGridViewTextBoxColumn.Width = 200;
            // 
            // soTaiKhoanDataGridViewTextBoxColumn
            // 
            this.soTaiKhoanDataGridViewTextBoxColumn.DataPropertyName = "SoTaiKhoan";
            this.soTaiKhoanDataGridViewTextBoxColumn.HeaderText = "Số tài khoản";
            this.soTaiKhoanDataGridViewTextBoxColumn.Name = "soTaiKhoanDataGridViewTextBoxColumn";
            this.soTaiKhoanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // hinhThucThanhToanStrDataGridViewTextBoxColumn
            // 
            this.hinhThucThanhToanStrDataGridViewTextBoxColumn.DataPropertyName = "HinhThucThanhToanStr";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.hinhThucThanhToanStrDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.hinhThucThanhToanStrDataGridViewTextBoxColumn.HeaderText = "Hình thức thanh toán";
            this.hinhThucThanhToanStrDataGridViewTextBoxColumn.Name = "hinhThucThanhToanStrDataGridViewTextBoxColumn";
            this.hinhThucThanhToanStrDataGridViewTextBoxColumn.ReadOnly = true;
            this.hinhThucThanhToanStrDataGridViewTextBoxColumn.Width = 150;
            // 
            // vATDataGridViewTextBoxColumn
            // 
            this.vATDataGridViewTextBoxColumn.DataPropertyName = "VAT";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.vATDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.vATDataGridViewTextBoxColumn.HeaderText = "GTGT (%)";
            this.vATDataGridViewTextBoxColumn.Name = "vATDataGridViewTextBoxColumn";
            this.vATDataGridViewTextBoxColumn.ReadOnly = true;
            this.vATDataGridViewTextBoxColumn.Width = 80;
            // 
            // uInvoiceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "uInvoiceList";
            this.Size = new System.Drawing.Size(864, 471);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.invoiceViewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkChecked;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgInvoice;
        private System.Windows.Forms.BindingSource invoiceViewBindingSource;
        private System.Windows.Forms.PrintDialog _printDialog;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn colChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoiceCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoiceDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fullNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tenDonViDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soTaiKhoanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hinhThucThanhToanStrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vATDataGridViewTextBoxColumn;
    }
}
