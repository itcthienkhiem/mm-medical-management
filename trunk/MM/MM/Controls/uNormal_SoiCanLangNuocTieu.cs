﻿using System;
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
    public partial class uNormal_SoiCanLangNuocTieu : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uNormal_SoiCanLangNuocTieu()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool FromToChecked
        {
            get { return chkFromTo.Checked; }
            set { chkFromTo.Checked = value; }
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

        public double XValue
        {
            get { return (double)numXValue.Value; }
            set { numXValue.Value = (Decimal)value; }
        }
        #endregion

        #region UI Command
        public ChiTietXetNghiem_Manual GetChiTietXetNghiem_Manual()
        {
            ChiTietXetNghiem_Manual ct = new ChiTietXetNghiem_Manual();
            ct.DoiTuong = (byte)DoiTuong.Khac;
            if (this.FromToChecked)
            {
                ct.FromValue = this.FromValue;
                ct.ToValue = this.ToValue;
            }

            ct.XValue = this.XValue;

            return ct;
        }

        public void SetChiTietXetNghiem_Manual(ChiTietXetNghiem_Manual ct)
        {
            if (ct.FromValue != null && ct.FromValue.HasValue)
            {
                this.FromToChecked = true;
                this.FromValue = ct.FromValue.Value;
                this.ToValue = ct.ToValue.Value;
            }
            else
                this.FromToChecked = false;

            this.XValue = ct.XValue.Value;
        }

        public bool CheckInfo()
        {
            if (chkFromTo.Checked && numFromValue.Value > numToValue.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập chỉ số từ nhỏ hơn hoặc bằng chỉ số đến.", Common.IconType.Information);
                numFromValue.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void chkFromTo_CheckedChanged(object sender, EventArgs e)
        {
            numFromValue.Enabled = chkFromTo.Checked;
            numToValue.Enabled = chkFromTo.Checked;
        }
        #endregion
    }
}
