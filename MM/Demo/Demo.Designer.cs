/* Copyright (c) 2016, Cocosoft Inc.
 All rights reserved.
 http://www.Cocosofttech.com

 This file is part of the LIS open source project.

 The LIS  open source project is free software: you can
 redistribute it and/or modify it under the terms of the GNU General Public
 License as published by the Free Software Foundation, either version 3 of the
 License, or (at your option) any later version.

 The ClearCanvas LIS open source project is distributed in the hope that it
 will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
 Public License for more details.

 You should have received a copy of the GNU General Public License along with
 the LIS open source project.  If not, see
 <http://www.gnu.org/licenses/>.
*/
namespace Demo
{
    partial class Demo
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Demo));
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabKhamNoiSoi = new System.Windows.Forms.TabControl();
            this.pageChupHinh = new System.Windows.Forms.TabPage();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lvCapture = new System.Windows.Forms.ListView();
            this.ctmCapture = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.xóaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.xóaTấtCảToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imgListCapture = new System.Windows.Forms.ImageList(this.components);
            this.panel6 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.picWebCam = new System.Windows.Forms.PictureBox();
            this.btnCapture = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabKhamNoiSoi.SuspendLayout();
            this.pageChupHinh.SuspendLayout();
            this.panel7.SuspendLayout();
            this.ctmCapture.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWebCam)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(387, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "   &Đóng";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 499);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(848, 39);
            this.panel1.TabIndex = 5;
            this.panel1.TabStop = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabKhamNoiSoi);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(848, 499);
            this.panel3.TabIndex = 0;
            // 
            // tabKhamNoiSoi
            // 
            this.tabKhamNoiSoi.Controls.Add(this.pageChupHinh);
            this.tabKhamNoiSoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabKhamNoiSoi.ImageList = this.imgList;
            this.tabKhamNoiSoi.Location = new System.Drawing.Point(0, 0);
            this.tabKhamNoiSoi.Name = "tabKhamNoiSoi";
            this.tabKhamNoiSoi.SelectedIndex = 0;
            this.tabKhamNoiSoi.Size = new System.Drawing.Size(848, 499);
            this.tabKhamNoiSoi.TabIndex = 0;
            this.tabKhamNoiSoi.TabStop = false;
            // 
            // pageChupHinh
            // 
            this.pageChupHinh.BackColor = System.Drawing.SystemColors.Control;
            this.pageChupHinh.Controls.Add(this.panel7);
            this.pageChupHinh.Controls.Add(this.panel6);
            this.pageChupHinh.ImageIndex = 0;
            this.pageChupHinh.Location = new System.Drawing.Point(4, 23);
            this.pageChupHinh.Name = "pageChupHinh";
            this.pageChupHinh.Padding = new System.Windows.Forms.Padding(3);
            this.pageChupHinh.Size = new System.Drawing.Size(840, 472);
            this.pageChupHinh.TabIndex = 1;
            this.pageChupHinh.Text = "Chụp hình";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.lvCapture);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 257);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(834, 212);
            this.panel7.TabIndex = 1;
            // 
            // lvCapture
            // 
            this.lvCapture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvCapture.ContextMenuStrip = this.ctmCapture;
            this.lvCapture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCapture.LargeImageList = this.imgListCapture;
            this.lvCapture.Location = new System.Drawing.Point(0, 0);
            this.lvCapture.Name = "lvCapture";
            this.lvCapture.Size = new System.Drawing.Size(834, 212);
            this.lvCapture.SmallImageList = this.imgListCapture;
            this.lvCapture.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvCapture.TabIndex = 0;
            this.lvCapture.UseCompatibleStateImageBehavior = false;
            // 
            // ctmCapture
            // 
            this.ctmCapture.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xóaToolStripMenuItem,
            this.toolStripSeparator1,
            this.xóaTấtCảToolStripMenuItem});
            this.ctmCapture.Name = "ctmCapture";
            this.ctmCapture.Size = new System.Drawing.Size(127, 54);
            // 
            // xóaToolStripMenuItem
            // 
            this.xóaToolStripMenuItem.Name = "xóaToolStripMenuItem";
            this.xóaToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.xóaToolStripMenuItem.Text = "Xóa";
            this.xóaToolStripMenuItem.Click += new System.EventHandler(this.xóaToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(127, 6);
            // 
            // xóaTấtCảToolStripMenuItem
            // 
            this.xóaTấtCảToolStripMenuItem.Name = "xóaTấtCảToolStripMenuItem";
            this.xóaTấtCảToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.xóaTấtCảToolStripMenuItem.Text = "Xóa tất cả";
            this.xóaTấtCảToolStripMenuItem.Click += new System.EventHandler(this.xóaTấtCảToolStripMenuItem_Click);
            // 
            // imgListCapture
            // 
            this.imgListCapture.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgListCapture.ImageSize = new System.Drawing.Size(200, 200);
            this.imgListCapture.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label14);
            this.panel6.Controls.Add(this.picWebCam);
            this.panel6.Controls.Add(this.btnCapture);
            this.panel6.Controls.Add(this.btnStop);
            this.panel6.Controls.Add(this.btnPlay);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(834, 254);
            this.panel6.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Blue;
            this.label14.Location = new System.Drawing.Point(5, 234);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(177, 15);
            this.label14.TabIndex = 15;
            this.label14.Text = "Danh sách hình được chụp";
            // 
            // picWebCam
            // 
            this.picWebCam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picWebCam.Location = new System.Drawing.Point(317, 4);
            this.picWebCam.Name = "picWebCam";
            this.picWebCam.Size = new System.Drawing.Size(200, 200);
            this.picWebCam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picWebCam.TabIndex = 14;
            this.picWebCam.TabStop = false;
            // 
            // btnCapture
            // 
            this.btnCapture.Enabled = false;
            this.btnCapture.Image = ((System.Drawing.Image)(resources.GetObject("btnCapture.Image")));
            this.btnCapture.Location = new System.Drawing.Point(444, 207);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(42, 42);
            this.btnCapture.TabIndex = 2;
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.Location = new System.Drawing.Point(396, 207);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(42, 42);
            this.btnStop.TabIndex = 1;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.Location = new System.Drawing.Point(348, 207);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(42, 42);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "camera-icon (1).png");
            this.imgList.Images.SetKeyName(1, "clipboard-search-result-icon.png");
            // 
            // Demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(848, 538);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Demo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.dlgAddKetQuaNoiSoi_FormClosing);
            this.Load += new System.EventHandler(this.dlgAddKetQuaNoiSoi_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabKhamNoiSoi.ResumeLayout(false);
            this.pageChupHinh.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.ctmCapture.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWebCam)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabControl tabKhamNoiSoi;
        private System.Windows.Forms.TabPage pageChupHinh;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.PictureBox picWebCam;
        private System.Windows.Forms.ListView lvCapture;
        private System.Windows.Forms.ImageList imgListCapture;
        private System.Windows.Forms.ContextMenuStrip ctmCapture;
        private System.Windows.Forms.ToolStripMenuItem xóaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem xóaTấtCảToolStripMenuItem;
        private System.Windows.Forms.Label label14;

    }
}
