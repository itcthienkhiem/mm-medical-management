namespace SonoOnlineResult.Controls
{
    partial class uBase
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
            this.timerSearch = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timerSearch
            // 
            this.timerSearch.Interval = 200;
            this.timerSearch.Tick += new System.EventHandler(this.timerSearch_Tick);
            // 
            // uBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "uBase";
            this.Size = new System.Drawing.Size(796, 469);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerSearch;

    }
}
