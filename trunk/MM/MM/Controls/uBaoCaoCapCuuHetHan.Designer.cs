namespace MM.Controls
{
    partial class uBaoCaoCapCuuHetHan
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
            this.tabReport = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.ctmAction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.xemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this._uKhoCapCuu = new MM.Controls.uKhoCapCuu();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnView = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numSoNgayHetHan = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.pageFilter = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this._ucReportViewer = new MM.Controls.ucReportViewer();
            this.pageBaoCao = new DevComponents.DotNetBar.TabItem(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tabReport)).BeginInit();
            this.tabReport.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.ctmAction.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoNgayHetHan)).BeginInit();
            this.tabControlPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabReport
            // 
            this.tabReport.CanReorderTabs = true;
            this.tabReport.Controls.Add(this.tabControlPanel1);
            this.tabReport.Controls.Add(this.tabControlPanel2);
            this.tabReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabReport.Location = new System.Drawing.Point(0, 0);
            this.tabReport.Name = "tabReport";
            this.tabReport.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.tabReport.SelectedTabIndex = 0;
            this.tabReport.Size = new System.Drawing.Size(820, 479);
            this.tabReport.Style = DevComponents.DotNetBar.eTabStripStyle.VS2005;
            this.tabReport.TabIndex = 0;
            this.tabReport.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabReport.Tabs.Add(this.pageFilter);
            this.tabReport.Tabs.Add(this.pageBaoCao);
            this.tabReport.Text = "tabControl1";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.ContextMenuStrip = this.ctmAction;
            this.tabControlPanel1.Controls.Add(this.panel3);
            this.tabControlPanel1.Controls.Add(this.panel2);
            this.tabControlPanel1.Controls.Add(this.panel1);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 25);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(820, 454);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.White;
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(168)))), ((int)(((byte)(153)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.pageFilter;
            // 
            // ctmAction
            // 
            this.ctmAction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xemToolStripMenuItem});
            this.ctmAction.Name = "ctmAction";
            this.ctmAction.Size = new System.Drawing.Size(153, 48);
            // 
            // xemToolStripMenuItem
            // 
            this.xemToolStripMenuItem.Image = global::MM.Properties.Resources.views_icon;
            this.xemToolStripMenuItem.Name = "xemToolStripMenuItem";
            this.xemToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.xemToolStripMenuItem.Text = "Xem";
            this.xemToolStripMenuItem.Click += new System.EventHandler(this.xemToolStripMenuItem_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this._uKhoCapCuu);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1, 38);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(818, 379);
            this.panel3.TabIndex = 2;
            // 
            // _uKhoCapCuu
            // 
            this._uKhoCapCuu.Dock = System.Windows.Forms.DockStyle.Fill;
            this._uKhoCapCuu.IsReport = true;
            this._uKhoCapCuu.Location = new System.Drawing.Point(0, 0);
            this._uKhoCapCuu.Name = "_uKhoCapCuu";
            this._uKhoCapCuu.Size = new System.Drawing.Size(818, 379);
            this._uKhoCapCuu.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(1, 417);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(818, 36);
            this.panel2.TabIndex = 1;
            // 
            // btnView
            // 
            this.btnView.Image = global::MM.Properties.Resources.views_icon;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(6, 6);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 8;
            this.btnView.Text = "   &Xem";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.numSoNgayHetHan);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(818, 37);
            this.panel1.TabIndex = 0;
            // 
            // numSoNgayHetHan
            // 
            this.numSoNgayHetHan.Location = new System.Drawing.Point(108, 9);
            this.numSoNgayHetHan.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numSoNgayHetHan.Name = "numSoNgayHetHan";
            this.numSoNgayHetHan.Size = new System.Drawing.Size(69, 20);
            this.numSoNgayHetHan.TabIndex = 1;
            this.numSoNgayHetHan.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Số ngày hết hạn:";
            // 
            // pageFilter
            // 
            this.pageFilter.AttachedControl = this.tabControlPanel1;
            this.pageFilter.Image = global::MM.Properties.Resources.filter_icon;
            this.pageFilter.Name = "pageFilter";
            this.pageFilter.Text = "Điều kiện xem báo cáo";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this._ucReportViewer);
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 25);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(820, 454);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.White;
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(168)))), ((int)(((byte)(153)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 2;
            this.tabControlPanel2.TabItem = this.pageBaoCao;
            // 
            // _ucReportViewer
            // 
            this._ucReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ucReportViewer.Location = new System.Drawing.Point(1, 1);
            this._ucReportViewer.Name = "_ucReportViewer";
            this._ucReportViewer.Size = new System.Drawing.Size(818, 452);
            this._ucReportViewer.TabIndex = 0;
            // 
            // pageBaoCao
            // 
            this.pageBaoCao.AttachedControl = this.tabControlPanel2;
            this.pageBaoCao.Image = global::MM.Properties.Resources.product_sales_report_icon;
            this.pageBaoCao.Name = "pageBaoCao";
            this.pageBaoCao.Text = "Báo cáo";
            // 
            // uBaoCaoCapCuuHetHan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabReport);
            this.Name = "uBaoCaoCapCuuHetHan";
            this.Size = new System.Drawing.Size(820, 479);
            ((System.ComponentModel.ISupportInitialize)(this.tabReport)).EndInit();
            this.tabReport.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            this.ctmAction.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoNgayHetHan)).EndInit();
            this.tabControlPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.TabControl tabReport;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem pageFilter;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem pageBaoCao;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown numSoNgayHetHan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnView;
        private ucReportViewer _ucReportViewer;
        private uKhoCapCuu _uKhoCapCuu;
        private System.Windows.Forms.ContextMenuStrip ctmAction;
        private System.Windows.Forms.ToolStripMenuItem xemToolStripMenuItem;
    }
}
