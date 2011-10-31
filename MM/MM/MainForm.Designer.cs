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
            this.uPalette1 = new MM.Controls.uPalette();
            this.SuspendLayout();
            // 
            // uPalette1
            // 
            this.uPalette1.Color01 = System.Drawing.Color.Silver;
            this.uPalette1.Color02 = System.Drawing.Color.Red;
            this.uPalette1.Color03 = System.Drawing.Color.Tomato;
            this.uPalette1.Color04 = System.Drawing.Color.Yellow;
            this.uPalette1.Color05 = System.Drawing.Color.Lime;
            this.uPalette1.Color06 = System.Drawing.Color.Cyan;
            this.uPalette1.Color07 = System.Drawing.Color.Blue;
            this.uPalette1.Color08 = System.Drawing.Color.Fuchsia;
            this.uPalette1.Color09 = System.Drawing.Color.MediumOrchid;
            this.uPalette1.Color10 = System.Drawing.Color.DarkBlue;
            this.uPalette1.Color11 = System.Drawing.Color.MediumAquamarine;
            this.uPalette1.Color12 = System.Drawing.Color.LimeGreen;
            this.uPalette1.Color13 = System.Drawing.Color.DarkKhaki;
            this.uPalette1.Color14 = System.Drawing.Color.Chocolate;
            this.uPalette1.Color15 = System.Drawing.Color.DarkRed;
            this.uPalette1.Color16 = System.Drawing.Color.Gray;
            this.uPalette1.Location = new System.Drawing.Point(357, 119);
            this.uPalette1.Name = "uPalette1";
            this.uPalette1.Size = new System.Drawing.Size(131, 35);
            this.uPalette1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 449);
            this.Controls.Add(this.uPalette1);
            this.Name = "MainForm";
            this.Text = "Medical Management";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.uPalette uPalette1;


    }
}

