namespace MM
{
    partial class MainForm
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
            this.uColor1 = new MM.Controls.uColor();
            this.SuspendLayout();
            // 
            // uColor1
            // 
            this.uColor1.Color = System.Drawing.Color.MediumBlue;
            this.uColor1.Location = new System.Drawing.Point(337, 66);
            this.uColor1.Name = "uColor1";
            this.uColor1.Size = new System.Drawing.Size(16, 16);
            this.uColor1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 449);
            this.Controls.Add(this.uColor1);
            this.Name = "MainForm";
            this.Text = "Medical Management";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.uColor uColor1;
    }
}

