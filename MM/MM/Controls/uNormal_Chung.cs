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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Databasae;

namespace MM.Controls
{
    public partial class uNormal_Chung : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uNormal_Chung()
        {
            InitializeComponent();
            InitData();
        }
        #endregion

        #region Properties
        public bool FromValueChecked
        {
            get { return chkFromValue.Checked; }
            set { chkFromValue.Checked = value; }
        }

        public bool ToValueChecked
        {
            get { return chkToValue.Checked; }
            set { chkToValue.Checked = value; }
        }

        public double FromValue
        {
            get { return (double)numFromValue.Value; }
            set { numFromValue.Value = (Decimal)value; }
        }

        public double ToValue
        {
            get { return (double)numToValue.Value; }
            set { numToValue.Value = (Decimal)value; }
        }

        public string FromOperator
        {
            get { return cboFromOperator.Text; }
            set { cboFromOperator.Text = value; }
        }

        public string ToOperator
        {
            get { return cboToOperator.Text; }
            set { cboToOperator.Text = value; }
        }

        public string DonVi
        {
            get { return cboDonVi.Text; }
            set { cboDonVi.Text = value; }
        }

        public DataTable DonViList
        {
            set
            {
                cboDonVi.Items.Clear();
                if (value == null || value.Rows.Count <= 0) return;
                foreach (DataRow row in value.Rows)
                {
                    if (row[0] == null || row[0] == DBNull.Value || row[0].ToString().Trim() == string.Empty)
                        continue;

                    cboDonVi.Items.Add(row[0].ToString());
                }
            }
        }
        #endregion

        #region UI Command
        public ChiTietXetNghiem_Manual GetChiTietXetNghiem_Manual()
        {
            ChiTietXetNghiem_Manual ct = new ChiTietXetNghiem_Manual();
            if (this.FromValueChecked)
            {
                ct.FromValue = this.FromValue;
                ct.FromOperator = this.FromOperator;
            }

            if (this.ToValueChecked)
            {
                ct.ToValue = this.ToValue;
                ct.ToOperator = this.ToOperator;
            }

            ct.DonVi = this.DonVi;
            ct.DoiTuong = (byte)DoiTuong.Chung;

            return ct;
        }

        public void SetChiTietXetNghiem_Manual(ChiTietXetNghiem_Manual ct)
        {
            if (ct.FromValue != null && ct.FromValue.HasValue)
            {
                this.FromValueChecked = true;
                this.FromValue = ct.FromValue.Value;
                this.FromOperator = ct.FromOperator.Trim();
            }
            else
                this.FromValueChecked = false;

            if (ct.ToValue != null && ct.ToValue.HasValue)
            {
                this.ToValueChecked = true;
                this.ToValue = ct.ToValue.Value;
                this.ToOperator = ct.ToOperator.Trim();
            }
            else
                this.ToValueChecked = false;

            this.DonVi = ct.DonVi;
        }

        private void InitData()
        {
            cboFromOperator.SelectedIndex = 1;
            cboToOperator.SelectedIndex = 1;
        }

        public bool CheckInfo()
        {
            if (chkFromValue.Checked && chkToValue.Checked && numFromValue.Value >= numToValue.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập chỉ số từ phải nhỏ hơn chỉ số đến.", IconType.Information);
                numFromValue.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void chkFromValue_CheckedChanged(object sender, EventArgs e)
        {
            numFromValue.Enabled = chkFromValue.Checked;

            if (!chkFromValue.Checked)
            {
                cboFromOperator.Enabled = false;
                cboToOperator.Enabled = chkToValue.Checked;
            }
            else
            {
                if (chkFromValue.Checked && chkToValue.Checked)
                {
                    cboFromOperator.Enabled = false;
                    cboToOperator.Enabled = false;
                    cboFromOperator.SelectedIndex = 1;
                    cboToOperator.SelectedIndex = 1;
                }
                else
                    cboFromOperator.Enabled = true;
            }

            //if (chkFromValue.Checked || chkToValue.Checked)
            //    cboDonVi.Enabled = true;
            //else
            //    cboDonVi.Enabled = false;
        }

        private void chkToValue_CheckedChanged(object sender, EventArgs e)
        {
            numToValue.Enabled = chkToValue.Checked;

            if (!chkToValue.Checked)
            {
                cboToOperator.Enabled = false;
                cboFromOperator.Enabled = chkFromValue.Checked;
            }
            else
            {
                if (chkFromValue.Checked && chkToValue.Checked)
                {
                    cboFromOperator.Enabled = false;
                    cboToOperator.Enabled = false;
                    cboFromOperator.SelectedIndex = 1;
                    cboToOperator.SelectedIndex = 1;
                }
                else
                    cboToOperator.Enabled = true;
            }

            //cboToOperator.Enabled = chkToValue.Checked;

            //if (chkToValue.Checked || chkToValue.Checked)
            //    cboDonVi.Enabled = true;
            //else
            //    cboDonVi.Enabled = false;
        }
        #endregion
    }
}
