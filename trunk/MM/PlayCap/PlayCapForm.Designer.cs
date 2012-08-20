namespace PlayCap
{
    partial class PlayCapForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayCapForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonChupHinh = new System.Windows.Forms.ToolStripButton();
            this.videoPanel = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonChupHinh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(300, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonChupHinh
            // 
            this.toolStripButtonChupHinh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonChupHinh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonChupHinh.Image")));
            this.toolStripButtonChupHinh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonChupHinh.Name = "toolStripButtonChupHinh";
            this.toolStripButtonChupHinh.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonChupHinh.Text = "toolStripButton1";
            this.toolStripButtonChupHinh.ToolTipText = "Chụp hình";
            this.toolStripButtonChupHinh.Click += new System.EventHandler(this.toolStripButtonChupHinh_Click);
            // 
            // videoPanel
            // 
            this.videoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoPanel.Location = new System.Drawing.Point(0, 25);
            this.videoPanel.Name = "videoPanel";
            this.videoPanel.Size = new System.Drawing.Size(300, 200);
            this.videoPanel.TabIndex = 1;
            this.videoPanel.SizeChanged += new System.EventHandler(this.videoPanel_SizeChanged);
            // 
            // PlayCapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 225);
            this.Controls.Add(this.videoPanel);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PlayCapForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Play Cap";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlayCapForm_FormClosing);
            this.Load += new System.EventHandler(this.PlayCapForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonChupHinh;
        private System.Windows.Forms.Panel videoPanel;
    }
}

