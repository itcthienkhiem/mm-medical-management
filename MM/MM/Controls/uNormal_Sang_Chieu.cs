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
    public partial class uNormal_Sang_Chieu : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uNormal_Sang_Chieu()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool SangChecked
        {
            get { return chkSang.Checked; }
            set { chkSang.Checked = value; }
        }

        public bool ChieuChecked
        {
            get { return chkChieu.Checked; }
            set { chkChieu.Checked = value; }
        }

        public int FromTime_Sang
        {
            get { return (int)numFromTime_Sang.Value; }
            set { numFromTime_Sang.Value = value; }
        }

        public int FromTime_Chieu
        {
            get { return (int)numFromTime_Chieu.Value; }
            set { numFromTime_Chieu.Value = value; }
        }

        public int ToTime_Sang
        {
            get { return (int)numToTime_Sang.Value; }
            set { numToTime_Sang.Value = value; }
        }

        public int ToTime_Chieu
        {
            get { return (int)numToTime_Chieu.Value; }
            set { numToTime_Chieu.Value = value; }
        }

        public uNormal_Chung Normal_Sang
        {
            get { return uNormal_Sang; }
        }

        public uNormal_Chung Normal_Chieu
        {
            get { return uNormal_Chieu; }
        }

        public DataTable DonViList
        {
            set
            {
                this.Normal_Sang.DonViList = value;
                this.Normal_Chieu.DonViList = value;
            }
        }
        #endregion

        #region UI Command
        public List<ChiTietXetNghiem_Manual> GetChiTietXetNghiem_ManualList()
        {
            List<ChiTietXetNghiem_Manual> ctxns = new List<ChiTietXetNghiem_Manual>();
            if (this.SangChecked)
            {
                ChiTietXetNghiem_Manual ct = this.Normal_Sang.GetChiTietXetNghiem_Manual();
                ct.DoiTuong = (byte)DoiTuong.Sang;
                ct.FromTime = this.FromTime_Sang;
                ct.ToTime = this.ToTime_Sang;
                ctxns.Add(ct);
            }

            if (this.ChieuChecked)
            {
                ChiTietXetNghiem_Manual ct = this.Normal_Chieu.GetChiTietXetNghiem_Manual();
                ct.DoiTuong = (byte)DoiTuong.Chieu;
                ct.FromTime = this.FromTime_Chieu;
                ct.ToTime = this.ToTime_Chieu;
                ctxns.Add(ct);
            }

            return ctxns;
        }

        public void SetChiTietXetNghiem_ManualList(List<ChiTietXetNghiem_Manual> ctxns)
        {
            if (ctxns == null || ctxns.Count <= 0) return;

            foreach (var ct in ctxns)
            {
                switch ((DoiTuong)ct.DoiTuong)
                {
                    case DoiTuong.Sang:
                        this.SangChecked = true;
                        this.FromTime_Sang = ct.FromTime.Value;
                        this.ToTime_Sang = ct.ToTime.Value;
                        this.Normal_Sang.SetChiTietXetNghiem_Manual(ct);
                        break;

                    case DoiTuong.Chieu:
                        this.ChieuChecked = true;
                        this.FromTime_Chieu = ct.FromTime.Value;
                        this.ToTime_Chieu = ct.ToTime.Value;
                        this.Normal_Chieu.SetChiTietXetNghiem_Manual(ct);
                        break;
                }
            }
        }

        public bool CheckInfo()
        {
            if (!chkSang.Checked && !chkChieu.Checked)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập chỉ số ít nhất cho sáng hoặc chiều.", Common.IconType.Information);
                chkSang.Focus();
                return false;
            }

            if (chkSang.Checked && numFromTime_Sang.Value > numToTime_Sang.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập thời gian từ nhở hơn hoặc bằng thời gian đến.", Common.IconType.Information);
                numFromTime_Sang.Focus();
                return false;
            }

            if (chkSang.Checked && !Normal_Sang.CheckInfo())
            {
                uNormal_Sang.Focus();
                return false;
            }

            if (chkChieu.Checked && numFromTime_Chieu.Value > numToTime_Chieu.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập thời gian từ nhở hơn hoặc bằng thời gian đến.", Common.IconType.Information);
                numFromTime_Chieu.Focus();
                return false;
            }

            if (chkChieu.Checked && !Normal_Chieu.CheckInfo())
            {
                uNormal_Chieu.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void chkSang_CheckedChanged(object sender, EventArgs e)
        {
            numFromTime_Sang.Enabled = chkSang.Checked;
            numToTime_Sang.Enabled = chkSang.Checked;
            uNormal_Sang.Enabled = chkSang.Checked;
        }

        private void chkChieu_CheckedChanged(object sender, EventArgs e)
        {
            numFromTime_Chieu.Enabled = chkChieu.Checked;
            numToTime_Chieu.Enabled = chkChieu.Checked;
            uNormal_Chieu.Enabled = chkChieu.Checked;
        }
        #endregion
    }
}
