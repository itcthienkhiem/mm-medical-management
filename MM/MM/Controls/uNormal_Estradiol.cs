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
    public partial class uNormal_Estradiol : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uNormal_Estradiol()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool FollicularPhaseChecked
        {
            get { return chkFollicularPhase.Checked; }
            set { chkFollicularPhase.Checked = value; }
        }

        public bool MidcycleChecked
        {
            get { return chkMidcycle.Checked; }
            set { chkMidcycle.Checked = value; }
        }

        public bool LutelPhaseChecked
        {
            get { return chkLutelPhase.Checked; }
            set { chkLutelPhase.Checked = value; }
        }

        public uNormal_Chung Normal_FollicularPhase
        {
            get { return uNormal_FollicularPhase; }
        }

        public uNormal_Chung Normal_Midcycle
        {
            get { return uNormal_Midcycle; }
        }

        public uNormal_Chung Normal_LutelPhase
        {
            get { return uNormal_LutelPhase; }
        }

        public DataTable DonViList
        {
            set
            {
                this.Normal_FollicularPhase.DonViList = value;
                this.Normal_Midcycle.DonViList = value;
                this.Normal_LutelPhase.DonViList = value;
            }
        }
        #endregion

        #region UI Command
        public List<ChiTietXetNghiem_Manual> GetChiTietXetNghiem_ManualList()
        {
            List<ChiTietXetNghiem_Manual> ctxns = new List<ChiTietXetNghiem_Manual>();
            if (this.FollicularPhaseChecked)
            {
                ChiTietXetNghiem_Manual ct = this.Normal_FollicularPhase.GetChiTietXetNghiem_Manual();
                ct.DoiTuong = (byte)DoiTuong.FollicularPhase;
                ctxns.Add(ct);
            }

            if (this.MidcycleChecked)
            {
                ChiTietXetNghiem_Manual ct = this.Normal_Midcycle.GetChiTietXetNghiem_Manual();
                ct.DoiTuong = (byte)DoiTuong.Midcycle;
                ctxns.Add(ct);
            }

            if (this.LutelPhaseChecked)
            {
                ChiTietXetNghiem_Manual ct = this.Normal_LutelPhase.GetChiTietXetNghiem_Manual();
                ct.DoiTuong = (byte)DoiTuong.LutelPhase;
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
                    case DoiTuong.FollicularPhase:
                        this.FollicularPhaseChecked = true;
                        this.Normal_FollicularPhase.SetChiTietXetNghiem_Manual(ct);
                        break;
                    case DoiTuong.Midcycle:
                        this.MidcycleChecked = true;
                        this.Normal_Midcycle.SetChiTietXetNghiem_Manual(ct);
                        break;
                    case DoiTuong.LutelPhase:
                        this.LutelPhaseChecked = true;
                        this.Normal_LutelPhase.SetChiTietXetNghiem_Manual(ct);
                        break;
                }
            }
        }

        public bool CheckInfo()
        {
            if (!chkFollicularPhase.Checked && !chkMidcycle.Checked && !chkLutelPhase.Checked)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập chỉ số ít nhất cho Follicular phase hoặc Midcycle hoặc Lutel phase.", Common.IconType.Information);
                chkFollicularPhase.Focus();
                return false;
            }

            if (chkFollicularPhase.Checked && !Normal_FollicularPhase.CheckInfo())
            {
                uNormal_FollicularPhase.Focus();
                return false;
            }

            if (chkMidcycle.Checked && !Normal_Midcycle.CheckInfo())
            {
                uNormal_Midcycle.Focus();
                return false;
            }

            if (chkLutelPhase.Checked && !Normal_LutelPhase.CheckInfo())
            {
                uNormal_LutelPhase.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Window Event Handlers
        private void chkFollicularPhase_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_FollicularPhase.Enabled = chkFollicularPhase.Checked;
        }

        private void chkMidcycle_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_Midcycle.Enabled = chkMidcycle.Checked;
        }

        private void chkLutelPhase_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_LutelPhase.Enabled = chkLutelPhase.Checked;
        }        
        #endregion
    }
}
