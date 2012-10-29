﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;

namespace MM.Dialogs
{
    public partial class dlgQuaTrinhDuyetThongBao : dlgBase
    {
        #region Members
        private DataRow _drThongBao = null;
        #endregion

        #region Constructor
        public dlgQuaTrinhDuyetThongBao(DataRow drThongBao)
        {
            InitializeComponent();
            _drThongBao = drThongBao;
        }
        #endregion

        #region UI Command
        private void DisplayInfo()
        {
            txtTenThongBao.Text = _drThongBao["TenThongBao"] as string;

            if (_drThongBao["ThongBaoBuff1"] != null && _drThongBao["ThongBaoBuff1"] != DBNull.Value)
            {
                chkDuyetLan1.Checked = true;
                btnXemThongBao1.Enabled = true;
                txtNgayDuyet1.Text = Convert.ToDateTime(_drThongBao["NgayDuyet1"]).ToString("dd/MM/yyyy HH:mm:ss");
                txtNguoiDuyet1.Text = _drThongBao["NguoiDuyet1"] as string;
            }

            if (_drThongBao["ThongBaoBuff2"] != null && _drThongBao["ThongBaoBuff2"] != DBNull.Value)
            {
                chkDuyetLan2.Checked = true;
                btnXemThongBao2.Enabled = true;
                txtNgayDuyet2.Text = Convert.ToDateTime(_drThongBao["NgayDuyet2"]).ToString("dd/MM/yyyy HH:mm:ss");
                txtNguoiDuyet2.Text = _drThongBao["NguoiDuyet2"] as string;
            }

            if (_drThongBao["ThongBaoBuff3"] != null && _drThongBao["ThongBaoBuff3"] != DBNull.Value)
            {
                chkDuyetLan3.Checked = true;
                btnXemThongBao3.Enabled = true;
                txtNgayDuyet3.Text = Convert.ToDateTime(_drThongBao["NgayDuyet3"]).ToString("dd/MM/yyyy HH:mm:ss");
                txtNguoiDuyet3.Text = _drThongBao["NguoiDuyet3"] as string;
            }
        }

        private void ExecuteThongBao(byte[] buff)
        {
            string fileName = string.Format("{0}\\Temp\\ThongBao_{1}.xls", Application.StartupPath, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"));
            Utility.SaveFileFromBytes(fileName, buff);
            Utility.ExecuteFile(fileName);
        }
        #endregion

        #region Window Event Handlers
        private void dlgQuaTrinhDuyetThongBao_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        }

        private void btnXemThongBao1_Click(object sender, EventArgs e)
        {
            byte[] buff = (byte[])_drThongBao["ThongBaoBuff1"];
            ExecuteThongBao(buff);
        }

        private void btnXemThongBao2_Click(object sender, EventArgs e)
        {
            byte[] buff = (byte[])_drThongBao["ThongBaoBuff2"];
            ExecuteThongBao(buff);
        }

        private void btnXemThongBao3_Click(object sender, EventArgs e)
        {
            byte[] buff = (byte[])_drThongBao["ThongBaoBuff2"];
            ExecuteThongBao(buff);
        }
        #endregion

        
    }
}
