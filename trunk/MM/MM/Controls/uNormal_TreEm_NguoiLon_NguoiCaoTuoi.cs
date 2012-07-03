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
    public partial class uNormal_TreEm_NguoiLon_NguoiCaoTuoi : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uNormal_TreEm_NguoiLon_NguoiCaoTuoi()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool TreEmChecked
        {
            get { return chkTreEm.Checked; }
            set { chkTreEm.Checked = value; }
        }

        public bool NguoiLonChecked
        {
            get { return chkNguoiLon.Checked; }
            set { chkNguoiLon.Checked = value; }
        }

        public bool NguoiCaoTuoiChecked
        {
            get { return chkNguoiCaoTuoi.Checked; }
            set { chkNguoiCaoTuoi.Checked = value; }
        }

        public uNormal_Chung Normal_TreEm
        {
            get { return uNormal_TreEm; }
        }

        public uNormal_Chung Normal_NguoiLon
        {
            get { return uNormal_NguoiLon; }
        }

        public uNormal_Chung Normal_NguoiCaoTuoi
        {
            get { return uNormal_NguoiCaoTuoi; }
        }

        public DataTable DonViList
        {
            set
            {
                this.Normal_TreEm.DonViList = value;
                this.Normal_NguoiLon.DonViList = value;
                this.Normal_NguoiCaoTuoi.DonViList = value;
            }
        }
        #endregion

        #region UI Command
        public List<ChiTietXetNghiem_Manual> GetChiTietXetNghiem_ManualList()
        {
            List<ChiTietXetNghiem_Manual> ctxns = new List<ChiTietXetNghiem_Manual>();

            if (this.TreEmChecked)
            {
                ChiTietXetNghiem_Manual ct = this.Normal_TreEm.GetChiTietXetNghiem_Manual();
                ct.DoiTuong = (byte)DoiTuong.TreEm;
                ctxns.Add(ct);
            }

            if (this.NguoiLonChecked)
            {
                ChiTietXetNghiem_Manual ct = this.Normal_NguoiLon.GetChiTietXetNghiem_Manual();
                ct.DoiTuong = (byte)DoiTuong.NguoiLon;
                ctxns.Add(ct);
            }

            if (this.NguoiCaoTuoiChecked)
            {
                ChiTietXetNghiem_Manual ct = this.Normal_NguoiCaoTuoi.GetChiTietXetNghiem_Manual();
                ct.DoiTuong = (byte)DoiTuong.NguoiCaoTuoi;
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
                    case DoiTuong.TreEm:
                        this.TreEmChecked = true;
                        this.Normal_TreEm.SetChiTietXetNghiem_Manual(ct);
                        break;

                    case DoiTuong.NguoiLon:
                        this.NguoiLonChecked = true;
                        this.Normal_NguoiLon.SetChiTietXetNghiem_Manual(ct);
                        break;

                    case DoiTuong.NguoiCaoTuoi:
                        this.NguoiCaoTuoiChecked = true;
                        this.Normal_NguoiCaoTuoi.SetChiTietXetNghiem_Manual(ct);
                        break;
                }
            }
        }

        public bool CheckInfo()
        {
            if (!chkTreEm.Checked && !chkNguoiLon.Checked && !chkNguoiCaoTuoi.Checked) 
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập chỉ số ít nhất cho trẻ em hoặc người lớn hoặc người cao tuổi.", Common.IconType.Information);
                chkTreEm.Focus();
                return false;
            }

            if (chkTreEm.Checked && !Normal_TreEm.CheckInfo())
            {
                uNormal_TreEm.Focus();
                return false;
            }

            if (chkNguoiLon.Checked && !Normal_NguoiLon.CheckInfo())
            {
                uNormal_NguoiLon.Focus();
                return false;
            }

            if (chkNguoiCaoTuoi.Checked && !Normal_NguoiCaoTuoi.CheckInfo())
            {
                uNormal_NguoiCaoTuoi.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void chkTreEm_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_TreEm.Enabled = chkTreEm.Checked;
        }

        private void chkNguoiLon_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_NguoiLon.Enabled = chkNguoiLon.Checked; 
        }

        private void chkNguoiCaoTuoi_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_NguoiCaoTuoi.Enabled = chkNguoiCaoTuoi.Checked;
        }
        #endregion
    }
}
