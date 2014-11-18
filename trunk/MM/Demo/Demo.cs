﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using MM.Common;

namespace Demo
{
    public partial class Demo : System.Windows.Forms.Form
    {
        #region Members
        private WebCam _webCam = null;
        private bool _isContinue = false;
        private int _imgCount = 0;
        #endregion

        #region Constructor
        public Demo()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            Cursor.Current = Cursors.WaitCursor;
            _webCam = new WebCam();
            _webCam.InitializeWebCam(ref picWebCam);
        }

        private void OnPlayWebCam()
        {
            try
            {
                btnStop.Enabled = true;
                btnCapture.Enabled = true;
                btnPlay.Enabled = false;

                if (!_isContinue)
                {
                    _webCam.Start();
                    _isContinue = true;
                }
                else
                    _webCam.Continue();

            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Text, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
        }

        private void OnStopWebCam()
        {
            try
            {
                btnStop.Enabled = false;
                btnCapture.Enabled = false;
                btnPlay.Enabled = true;

                _webCam.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Text, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddKetQuaNoiSoi_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void dlgAddKetQuaNoiSoi_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnStopWebCam();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            OnPlayWebCam();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            OnStopWebCam();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (picWebCam.Image == null) return;

            Image img = picWebCam.Image;
            imgListCapture.Images.Add(img);

            _imgCount++;
            ListViewItem item = new ListViewItem(string.Format("Hình {0}", _imgCount), imgListCapture.Images.Count - 1);
            item.Tag = picWebCam.Image;
            lvCapture.Items.Add(item);
        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvCapture.SelectedItems == null || lvCapture.SelectedItems.Count <= 0) return;

            foreach (ListViewItem item in lvCapture.SelectedItems)
            {
                int imgIndex = item.ImageIndex;
                lvCapture.Items.Remove(item);
                imgListCapture.Images.RemoveAt(imgIndex);
            }
        }

        private void xóaTấtCảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvCapture.Items.Clear();
            imgListCapture.Images.Clear();
            _imgCount = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        
    }
}
