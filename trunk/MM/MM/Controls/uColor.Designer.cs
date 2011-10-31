namespace MM.Controls
{
    partial class uColor
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
            this.picColor = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picColor)).BeginInit();
            this.SuspendLayout();
            // 
            // picColor
            // 
            this.picColor.BackColor = System.Drawing.Color.Red;
            this.picColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picColor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picColor.Location = new System.Drawing.Point(0, 0);
            this.picColor.Name = "picColor";
            this.picColor.Size = new System.Drawing.Size(16, 16);
            this.picColor.TabIndex = 0;
            this.picColor.TabStop = false;
            this.picColor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picColor_MouseDown);
            this.picColor.MouseLeave += new System.EventHandler(this.picColor_MouseLeave);
            this.picColor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picColor_MouseMove);
            // 
            // uColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picColor);
            this.Name = "uColor";
            this.Size = new System.Drawing.Size(16, 16);
            this.Load += new System.EventHandler(this.uColor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picColor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picColor;
    }
}
