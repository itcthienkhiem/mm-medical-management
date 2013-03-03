namespace MM.Dialogs
{
    partial class dlgAddTinNhanMau
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAddTinNhanMau));
            this.txtNoiDung = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lvBookMarks = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTieuDe = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.EnableAutoDragDrop = true;
            this.txtNoiDung.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoiDung.Location = new System.Drawing.Point(7, 52);
            this.txtNoiDung.MaxLength = 160;
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(368, 183);
            this.txtNoiDung.TabIndex = 2;
            this.txtNoiDung.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nội dung tin nhắn:";
            // 
            // lvBookMarks
            // 
            this.lvBookMarks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName});
            this.lvBookMarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lvBookMarks.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lvBookMarks.FullRowSelect = true;
            this.lvBookMarks.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvBookMarks.HideSelection = false;
            this.lvBookMarks.Location = new System.Drawing.Point(381, 52);
            this.lvBookMarks.MultiSelect = false;
            this.lvBookMarks.Name = "lvBookMarks";
            this.lvBookMarks.Size = new System.Drawing.Size(161, 183);
            this.lvBookMarks.TabIndex = 3;
            this.lvBookMarks.UseCompatibleStateImageBehavior = false;
            this.lvBookMarks.View = System.Windows.Forms.View.Details;
            this.lvBookMarks.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvBookMarks_ItemDrag);
            this.lvBookMarks.DoubleClick += new System.EventHandler(this.lvBookMarks_DoubleClick);
            // 
            // colName
            // 
            this.colName.Text = "Bookmarks";
            this.colName.Width = 157;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::MM.Properties.Resources.Log_Out_icon__1_;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(276, 241);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::MM.Properties.Resources.save;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(197, 241);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "   &Lưu";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Tiêu đề:";
            // 
            // txtTieuDe
            // 
            this.txtTieuDe.Location = new System.Drawing.Point(67, 8);
            this.txtTieuDe.Name = "txtTieuDe";
            this.txtTieuDe.Size = new System.Drawing.Size(475, 20);
            this.txtTieuDe.TabIndex = 1;
            // 
            // dlgAddTinNhanMau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 272);
            this.Controls.Add(this.txtTieuDe);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lvBookMarks);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNoiDung);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAddTinNhanMau";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Them tin nhan mau";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddTinNhanMau_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddTinNhanMau_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtNoiDung;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvBookMarks;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTieuDe;
    }
}