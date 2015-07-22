namespace MM.Dialogs
{
    partial class dlgGhiNhanTraNo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgGhiNhanTraNo));
            this._uGhiNhanTraNoList = new MM.Controls.uGhiNhanTraNoList();
            this.SuspendLayout();
            // 
            // _uGhiNhanTraNoList
            // 
            this._uGhiNhanTraNoList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._uGhiNhanTraNoList.Location = new System.Drawing.Point(0, 0);
            this._uGhiNhanTraNoList.Name = "_uGhiNhanTraNoList";
            this._uGhiNhanTraNoList.Size = new System.Drawing.Size(819, 386);
            this._uGhiNhanTraNoList.TabIndex = 0;
            // 
            // dlgGhiNhanTraNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 386);
            this.Controls.Add(this._uGhiNhanTraNoList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgGhiNhanTraNo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ghi nhan tra no";
            this.Load += new System.EventHandler(this.dlgGhiNhanTraNo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.uGhiNhanTraNoList _uGhiNhanTraNoList;
    }
}