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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MM.Common;

namespace MM.Dialogs
{
    public partial class dlgTVHomeConfig : dlgBase
    {
        #region Members
        
        #endregion

        #region Constructor
        public dlgTVHomeConfig()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        private bool CheckInfo()
        {
            if (txtPath.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn đường dẫn TVHome.", IconType.Information);
                btnBrowser.Focus();
                return false;
            }

            if (!File.Exists(txtPath.Text))
            {
                MsgBox.Show(this.Text, "Đường dẫn TVHome không tồn tại. Vui lòng kiểm tra lại.", IconType.Information);
                btnBrowser.Focus();
                return false;
            }

            return true;
        }

        public void SetAppConfig()
        {
            Global.TVHomeConfig.Path = txtPath.Text;
            Global.TVHomeConfig.SuDungSoiCTC = raSoiCTC_Co.Checked;
            Global.TVHomeConfig.SuDungSieuAm = raSieuAm_Co.Checked;
            Global.TVHomeConfig.Format = raJPG.Checked ? TVHomeImageFormat.JPG : TVHomeImageFormat.BMP;
        }
        #endregion

        #region Window Event Handlers
        private void dlgTVHomeConfig_Load(object sender, EventArgs e)
        {
            txtPath.Text = Global.TVHomeConfig.Path;
            raSoiCTC_Co.Checked = Global.TVHomeConfig.SuDungSoiCTC;
            raSoiCTC_Khong.Checked = !Global.TVHomeConfig.SuDungSoiCTC;
            raSieuAm_Co.Checked = Global.TVHomeConfig.SuDungSieuAm;
            raSieuAm_Khong.Checked = !Global.TVHomeConfig.SuDungSieuAm;
            raJPG.Checked = Global.TVHomeConfig.Format == TVHomeImageFormat.JPG;
            raBMP.Checked = Global.TVHomeConfig.Format == TVHomeImageFormat.BMP;
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "(*.exe)|*.exe";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = dlg.FileName;
            }
        }

        private void dlgTVHomeConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
            }
        }
        #endregion
    }
}
