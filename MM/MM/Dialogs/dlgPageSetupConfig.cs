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
using MM.Common;

namespace MM.Dialogs
{
    public partial class dlgPageSetupConfig : dlgBase
    {
        #region Members

        #endregion

        #region Constructor
        public dlgPageSetupConfig()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void DisplayInfo()
        {
            foreach (string template in Global.ExcelTemplates)
            {
                PageSetup p = Global.PageSetupConfig.GetPageSetup(template);
                if (p == null)
                {
                    p = Utility.GetPageSetup(template);

                    if (p == null)
                        dgPageSetup.Rows.Add(template, 0, 0, 0, 0);
                    else
                        dgPageSetup.Rows.Add(template, p.LeftMargin, p.RightMargin, p.TopMargin, p.BottomMargin);
                }
                else
                    dgPageSetup.Rows.Add(template, p.LeftMargin, p.RightMargin, p.TopMargin, p.BottomMargin);
            }
        }

        private void SavePageSetup()
        {
            Global.PageSetupConfig.ClearAll();
            foreach (DataGridViewRow row in dgPageSetup.Rows)
            {
                string template = row.Cells[0].Value.ToString();
                double left = row.Cells[1].Value != null ? Convert.ToDouble(row.Cells[1].Value) : 0;
                double right = row.Cells[2].Value != null ? Convert.ToDouble(row.Cells[2].Value) : 0;
                double top = row.Cells[3].Value != null ? Convert.ToDouble(row.Cells[3].Value) : 0;
                double bottom = row.Cells[4].Value != null ? Convert.ToDouble(row.Cells[4].Value) : 0;

                PageSetup p = new PageSetup();
                p.Template = template;
                p.LeftMargin = left;
                p.RightMargin = right;
                p.TopMargin = top;
                p.BottomMargin = bottom;
                Global.PageSetupConfig.AddPageSetup(p);
            }

            Global.PageSetupConfig.Serialize(Global.PageSetupConfigPath);
        }
        #endregion

        #region Window Event Handlers
        private void dlgPageSetupConfig_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        }

        private void dlgPageSetupConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                SavePageSetup();
            }
        }
        #endregion
    }
}
