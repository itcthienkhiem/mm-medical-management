namespace MM.Dialogs
{
    partial class dlgPageSetupConfig
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgPageSetupConfig));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgPageSetup = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.ReportName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftMargin = new DevComponents.DotNetBar.Controls.DataGridViewDoubleInputColumn();
            this.RightMargin = new DevComponents.DotNetBar.Controls.DataGridViewDoubleInputColumn();
            this.TopMargin = new DevComponents.DotNetBar.Controls.DataGridViewDoubleInputColumn();
            this.BottomMargin = new DevComponents.DotNetBar.Controls.DataGridViewDoubleInputColumn();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPageSetup)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 621);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(752, 36);
            this.panel2.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(378, 6);
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
            this.btnOK.Location = new System.Drawing.Point(299, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "   &Đồng ý";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgPageSetup);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(752, 621);
            this.panel1.TabIndex = 3;
            // 
            // dgPageSetup
            // 
            this.dgPageSetup.AllowUserToAddRows = false;
            this.dgPageSetup.AllowUserToDeleteRows = false;
            this.dgPageSetup.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPageSetup.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPageSetup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPageSetup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ReportName,
            this.LeftMargin,
            this.RightMargin,
            this.TopMargin,
            this.BottomMargin});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPageSetup.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgPageSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPageSetup.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgPageSetup.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgPageSetup.HighlightSelectedColumnHeaders = false;
            this.dgPageSetup.Location = new System.Drawing.Point(0, 0);
            this.dgPageSetup.MultiSelect = false;
            this.dgPageSetup.Name = "dgPageSetup";
            this.dgPageSetup.RowHeadersWidth = 30;
            this.dgPageSetup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPageSetup.Size = new System.Drawing.Size(752, 621);
            this.dgPageSetup.TabIndex = 2;
            // 
            // ReportName
            // 
            this.ReportName.HeaderText = "Tên báo cáo";
            this.ReportName.Name = "ReportName";
            this.ReportName.ReadOnly = true;
            this.ReportName.Width = 300;
            // 
            // LeftMargin
            // 
            // 
            // 
            // 
            this.LeftMargin.BackgroundStyle.Class = "DataGridViewNumericBorder";
            this.LeftMargin.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.LeftMargin.DefaultCellStyle = dataGridViewCellStyle2;
            this.LeftMargin.HeaderText = "Lề trái";
            this.LeftMargin.Increment = 1D;
            this.LeftMargin.MaxValue = 100D;
            this.LeftMargin.MinValue = 0D;
            this.LeftMargin.Name = "LeftMargin";
            this.LeftMargin.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // RightMargin
            // 
            // 
            // 
            // 
            this.RightMargin.BackgroundStyle.Class = "DataGridViewNumericBorder";
            this.RightMargin.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.RightMargin.DefaultCellStyle = dataGridViewCellStyle3;
            this.RightMargin.HeaderText = "Lề phải";
            this.RightMargin.Increment = 1D;
            this.RightMargin.Name = "RightMargin";
            this.RightMargin.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // TopMargin
            // 
            // 
            // 
            // 
            this.TopMargin.BackgroundStyle.Class = "DataGridViewNumericBorder";
            this.TopMargin.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TopMargin.DefaultCellStyle = dataGridViewCellStyle4;
            this.TopMargin.HeaderText = "Lề trên";
            this.TopMargin.Increment = 1D;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // BottomMargin
            // 
            // 
            // 
            // 
            this.BottomMargin.BackgroundStyle.Class = "DataGridViewNumericBorder";
            this.BottomMargin.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BottomMargin.DefaultCellStyle = dataGridViewCellStyle5;
            this.BottomMargin.HeaderText = "Lề dưới";
            this.BottomMargin.Increment = 1D;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dlgPageSetupConfig
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(752, 657);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgPageSetupConfig";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cau hinh trang in";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgPageSetupConfig_FormClosing);
            this.Load += new System.EventHandler(this.dlgPageSetupConfig_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPageSetup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgPageSetup;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReportName;
        private DevComponents.DotNetBar.Controls.DataGridViewDoubleInputColumn LeftMargin;
        private DevComponents.DotNetBar.Controls.DataGridViewDoubleInputColumn RightMargin;
        private DevComponents.DotNetBar.Controls.DataGridViewDoubleInputColumn TopMargin;
        private DevComponents.DotNetBar.Controls.DataGridViewDoubleInputColumn BottomMargin;
    }
}