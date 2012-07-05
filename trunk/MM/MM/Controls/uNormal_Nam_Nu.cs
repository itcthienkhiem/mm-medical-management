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
    public partial class uNormal_Nam_Nu : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uNormal_Nam_Nu()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool NamChecked
        {
            get { return chkNam.Checked; }
            set { chkNam.Checked = value; }
        }

        public bool NuChecked
        {
            get { return chkNu.Checked; }
            set { chkNu.Checked = value; }
        }

        public bool FromAge_NamChecked
        {
            get { return chkFromAge_Nam.Checked; }
            set { chkFromAge_Nam.Checked = value; }
        }

        public bool FromAge_NuChecked
        {
            get { return chkFromAge_Nu.Checked; }
            set { chkFromAge_Nu.Checked = value; }
        }

        public bool ToAge_NamChecked
        {
            get { return chkToAge_Nam.Checked; }
            set { chkToAge_Nam.Checked = value; }
        }

        public bool ToAge_NuChecked
        {
            get { return chkToAge_Nu.Checked; }
            set { chkToAge_Nu.Checked = value; }
        }

        public int FromAge_Nam
        {
            get { return (int)numFromAge_Nam.Value; }
            set { numFromAge_Nam.Value = value; }
        }

        public int FromAge_Nu
        {
            get { return (int)numFromAge_Nu.Value; }
            set { numFromAge_Nu.Value = value; }
        }

        public int ToAge_Nam
        {
            get { return (int)numToAge_Nam.Value; }
            set { numToAge_Nam.Value = value; }
        }

        public int ToAge_Nu
        {
            get { return (int)numToAge_Nu.Value; }
            set { numToAge_Nu.Value = value; }
        }

        public uNormal_Chung Normal_Nam
        {
            get { return uNormal_Nam; }
        }

        public uNormal_Chung Normal_Nu
        {
            get { return uNormal_Nu; }
        }

        public DataTable DonViList
        {
            set
            {
                this.Normal_Nam.DonViList = value;
                this.Normal_Nu.DonViList = value;
            }
        }
        #endregion

        #region UI Command
        public List<ChiTietXetNghiem_Manual> GetChiTietXetNghiem_ManualList()
        {
            List<ChiTietXetNghiem_Manual> ctxns = new List<ChiTietXetNghiem_Manual>();

            if (this.NamChecked)
            {
                ChiTietXetNghiem_Manual ct = Normal_Nam.GetChiTietXetNghiem_Manual();
                ct.DoiTuong = (byte)DoiTuong.Nam;
                if (this.FromAge_NamChecked)
                    ct.FromAge = this.FromAge_Nam;

                if (this.ToAge_NamChecked)
                    ct.ToAge = this.ToAge_Nam;

                ctxns.Add(ct);
            }

            if (this.NuChecked)
            {
                ChiTietXetNghiem_Manual ct = Normal_Nu.GetChiTietXetNghiem_Manual();
                ct.DoiTuong = (byte)DoiTuong.Nu;
                if (this.FromAge_NuChecked)
                    ct.FromAge = this.FromAge_Nu;

                if (this.ToAge_NuChecked)
                    ct.ToAge = this.ToAge_Nu;

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
                    case DoiTuong.Nam:
                    case DoiTuong.Sang_Nam:
                    case DoiTuong.Chieu_Nam:
                        this.NamChecked = true;
                        if (ct.FromAge != null && ct.FromAge.HasValue)
                        {
                            this.FromAge_NamChecked = true;
                            this.FromAge_Nam = ct.FromAge.Value;
                        }
                        else
                            this.FromAge_NamChecked = false;

                        if (ct.ToAge != null && ct.ToAge.HasValue)
                        {
                            this.ToAge_NamChecked = true;
                            this.ToAge_Nam = ct.ToAge.Value;
                        }
                        else
                            this.ToAge_NamChecked = false;

                        this.Normal_Nam.SetChiTietXetNghiem_Manual(ct);
                        break;
                    case DoiTuong.Nu:
                    case DoiTuong.Sang_Nu:
                    case DoiTuong.Chieu_Nu:
                        this.NuChecked = true;
                        if (ct.FromAge != null && ct.FromAge.HasValue)
                        {
                            this.FromAge_NuChecked = true;
                            this.FromAge_Nu = ct.FromAge.Value;
                        }
                        else
                            this.FromAge_NuChecked = false;

                        if (ct.ToAge != null && ct.ToAge.HasValue)
                        {
                            this.ToAge_NuChecked = true;
                            this.ToAge_Nu = ct.ToAge.Value;
                        }
                        else
                            this.ToAge_NuChecked = false;

                        this.Normal_Nu.SetChiTietXetNghiem_Manual(ct);
                        break;
                }
            }
        }

        public bool CheckInfo()
        {
            if (!chkNam.Checked && !chkNu.Checked)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập chỉ số ít nhất cho nam hoặc nữ.", Common.IconType.Information);
                chkNam.Focus();
                return false;
            }

            if (chkFromAge_Nam.Checked && chkToAge_Nam.Checked && numFromAge_Nam.Value > numToAge_Nam.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập tuổi từ nhỏ hơn hoặc bằng tuổi đến.", Common.IconType.Information);
                numFromAge_Nam.Focus();
                return false;
            }

            if (chkNam.Checked && !Normal_Nam.CheckInfo())
            {
                uNormal_Nam.Focus();
                return false;
            }

            if (chkFromAge_Nu.Checked && chkToAge_Nu.Checked && numFromAge_Nu.Value > numToAge_Nu.Value)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập tuổi từ nhỏ hơn hoặc bằng tuổi đến.", Common.IconType.Information);
                numFromAge_Nu.Focus();
                return false;
            }

            if (chkNu.Checked && !Normal_Nu.CheckInfo())
            {
                uNormal_Nu.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void chkNam_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_Nam.Enabled = chkNam.Checked;

            chkFromAge_Nam.Enabled = chkNam.Checked;
            chkToAge_Nam.Enabled = chkNam.Checked;

            numFromAge_Nam.Enabled = chkNam.Checked && chkFromAge_Nam.Checked;
            numToAge_Nam.Enabled = chkNam.Checked && chkToAge_Nam.Checked;
        }

        private void chkNu_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_Nu.Enabled = chkNu.Checked;

            chkFromAge_Nu.Enabled = chkNu.Checked;
            chkToAge_Nu.Enabled = chkNu.Checked;

            numFromAge_Nu.Enabled = chkNu.Checked && chkFromAge_Nu.Checked;
            numToAge_Nu.Enabled = chkNu.Checked && chkToAge_Nu.Checked;
        }

        private void chkFromAge_Nam_CheckedChanged(object sender, EventArgs e)
        {
            numFromAge_Nam.Enabled = chkNam.Checked && chkFromAge_Nam.Checked;
        }

        private void chkToAge_Nam_CheckedChanged(object sender, EventArgs e)
        {
            numToAge_Nam.Enabled = chkNam.Checked && chkToAge_Nam.Checked;
        }

        private void chkFromAge_Nu_CheckedChanged(object sender, EventArgs e)
        {
            numFromAge_Nu.Enabled = chkNu.Checked && chkFromAge_Nu.Checked;
        }

        private void chkToAge_Nu_CheckedChanged(object sender, EventArgs e)
        {
            numToAge_Nu.Enabled = chkNu.Checked && chkToAge_Nu.Checked;
        }
        #endregion
    }
}
