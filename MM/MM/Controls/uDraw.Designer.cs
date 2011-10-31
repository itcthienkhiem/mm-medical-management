namespace MM.Controls
{
    partial class uDraw
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
            this.pDraw = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pDraw
            // 
            this.pDraw.BackColor = System.Drawing.Color.White;
            this.pDraw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pDraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pDraw.Location = new System.Drawing.Point(0, 0);
            this.pDraw.Name = "pDraw";
            this.pDraw.Size = new System.Drawing.Size(171, 229);
            this.pDraw.TabIndex = 0;
            this.pDraw.Paint += new System.Windows.Forms.PaintEventHandler(this.pDraw_Paint);
            this.pDraw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pDraw_MouseDown);
            this.pDraw.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pDraw_MouseMove);
            this.pDraw.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pDraw_MouseUp);
            // 
            // uDraw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pDraw);
            this.Name = "uDraw";
            this.Size = new System.Drawing.Size(171, 229);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pDraw;

    }
}
