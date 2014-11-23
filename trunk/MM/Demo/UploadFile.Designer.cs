namespace Demo
{
    partial class UploadFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadFile));
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonFTPConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonMailConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonFTPConfig,
            this.toolStripButtonMailConfig});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(592, 31);
            this.toolStripMain.TabIndex = 0;
            this.toolStripMain.Text = "toolStrip1";
            this.toolStripMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripMain_ItemClicked);
            // 
            // toolStripButtonFTPConfig
            // 
            this.toolStripButtonFTPConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFTPConfig.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonFTPConfig.Image")));
            this.toolStripButtonFTPConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFTPConfig.Name = "toolStripButtonFTPConfig";
            this.toolStripButtonFTPConfig.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonFTPConfig.Text = "toolStripButton1";
            this.toolStripButtonFTPConfig.ToolTipText = "FTP Configuration";
            // 
            // toolStripButtonMailConfig
            // 
            this.toolStripButtonMailConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMailConfig.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMailConfig.Image")));
            this.toolStripButtonMailConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMailConfig.Name = "toolStripButtonMailConfig";
            this.toolStripButtonMailConfig.Size = new System.Drawing.Size(28, 28);
            this.toolStripButtonMailConfig.Text = "toolStripButton2";
            this.toolStripButtonMailConfig.ToolTipText = "Mail Configuration";
            // 
            // UploadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 383);
            this.Controls.Add(this.toolStripMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UploadFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upload File";
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonFTPConfig;
        private System.Windows.Forms.ToolStripButton toolStripButtonMailConfig;
    }
}