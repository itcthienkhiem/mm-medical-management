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

        //public int FromTime_Sang
        //{
        //    get { return (int)numFromTime_Sang.Value; }
        //    set { numFromTime_Sang.Value = value; }
        //}

        //public int FromTime_Chieu
        //{
        //    get { return (int)numFromTime_Chieu.Value; }
        //    set { numFromTime_Chieu.Value = value; }
        //}

        //public int ToTime_Sang
        //{
        //    get { return (int)numToTime_Sang.Value; }
        //    set { numToTime_Sang.Value = value; }
        //}

        //public int ToTime_Chieu
        //{
        //    get { return (int)numToTime_Chieu.Value; }
        //    set { numToTime_Chieu.Value = value; }
        //}

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
                uNormal_Nam_Nu_Sang.DonViList = value;
                uNormal_Nam_Nu_Chieu.DonViList = value;
            }
        }
        #endregion

        #region UI Command
        public List<ChiTietXetNghiem_Manual> GetChiTietXetNghiem_ManualList()
        {
            List<ChiTietXetNghiem_Manual> ctxns = new List<ChiTietXetNghiem_Manual>();
            if (this.SangChecked)
            {
                if (raChung_Sang.Checked)
                {
                    ChiTietXetNghiem_Manual ct = this.Normal_Sang.GetChiTietXetNghiem_Manual();
                    ct.DoiTuong = (byte)DoiTuong.Sang_Chung;
                    if (_uTimeRange_Sang.FromValueChecked)
                    {
                        ct.FromTime = _uTimeRange_Sang.FromValue;
                        ct.FromTimeOperator = _uTimeRange_Sang.FromOperator;
                    }

                    if (_uTimeRange_Sang.ToValueChecked)
                    {
                        ct.ToTime = _uTimeRange_Sang.ToValue;
                        ct.ToTimeOperator = _uTimeRange_Sang.ToOperator;
                    }

                    ctxns.Add(ct);
                }
                else
                {
                    List<ChiTietXetNghiem_Manual> namNuSangList = uNormal_Nam_Nu_Sang.GetChiTietXetNghiem_ManualList();

                    foreach (var ct in namNuSangList)
                    {
                        if (ct.DoiTuong == (byte)DoiTuong.Nam)
                            ct.DoiTuong = (byte)DoiTuong.Sang_Nam;
                        else if (ct.DoiTuong == (byte)DoiTuong.Nu)
                            ct.DoiTuong = (byte)DoiTuong.Sang_Nu;

                        if (_uTimeRange_Sang.FromValueChecked)
                        {
                            ct.FromTime = _uTimeRange_Sang.FromValue;
                            ct.FromTimeOperator = _uTimeRange_Sang.FromOperator;
                        }

                        if (_uTimeRange_Sang.ToValueChecked)
                        {
                            ct.ToTime = _uTimeRange_Sang.ToValue;
                            ct.ToTimeOperator = _uTimeRange_Sang.ToOperator;
                        }

                        ctxns.Add(ct);
                    }
                }
            }

            if (this.ChieuChecked)
            {
                if (raChung_Chieu.Checked)
                {
                    ChiTietXetNghiem_Manual ct = this.Normal_Chieu.GetChiTietXetNghiem_Manual();
                    ct.DoiTuong = (byte)DoiTuong.Chieu_Chung;

                    if (_uTimeRange_Chieu.FromValueChecked)
                    {
                        ct.FromTime = _uTimeRange_Chieu.FromValue;
                        ct.FromTimeOperator = _uTimeRange_Chieu.FromOperator;
                    }

                    if (_uTimeRange_Chieu.ToValueChecked)
                    {
                        ct.ToTime = _uTimeRange_Chieu.ToValue;
                        ct.ToTimeOperator = _uTimeRange_Chieu.ToOperator;
                    }

                    ctxns.Add(ct);
                }
                else
                {
                    List<ChiTietXetNghiem_Manual> namNuChieuList = uNormal_Nam_Nu_Chieu.GetChiTietXetNghiem_ManualList();

                    foreach (var ct in namNuChieuList)
                    {
                        if (ct.DoiTuong == (byte)DoiTuong.Nam)
                            ct.DoiTuong = (byte)DoiTuong.Chieu_Nam;
                        else if (ct.DoiTuong == (byte)DoiTuong.Nu)
                            ct.DoiTuong = (byte)DoiTuong.Chieu_Nu;

                        if (_uTimeRange_Chieu.FromValueChecked)
                        {
                            ct.FromTime = _uTimeRange_Chieu.FromValue;
                            ct.FromTimeOperator = _uTimeRange_Chieu.FromOperator;
                        }

                        if (_uTimeRange_Chieu.ToValueChecked)
                        {
                            ct.ToTime = _uTimeRange_Chieu.ToValue;
                            ct.ToTimeOperator = _uTimeRange_Chieu.ToOperator;
                        }

                        ctxns.Add(ct);
                    }
                }
            }

            return ctxns;
        }

        public void SetChiTietXetNghiem_ManualList(List<ChiTietXetNghiem_Manual> ctxns)
        {
            if (ctxns == null || ctxns.Count <= 0) return;

            List<ChiTietXetNghiem_Manual> namNuSangList = new List<ChiTietXetNghiem_Manual>();
            List<ChiTietXetNghiem_Manual> namNuChieuList = new List<ChiTietXetNghiem_Manual>();

            foreach (var ct in ctxns)
            {
                switch ((DoiTuong)ct.DoiTuong)
                {
                    case DoiTuong.Sang_Nam:
                    case DoiTuong.Sang_Nu:
                        namNuSangList.Add(ct);
                        break;

                    case DoiTuong.Chieu_Nam:
                    case DoiTuong.Chieu_Nu:
                        namNuChieuList.Add(ct);
                        break;
                }
            }

            foreach (var ct in ctxns)
            {
                switch ((DoiTuong)ct.DoiTuong)
                {
                    case DoiTuong.Sang_Chung:
                        this.SangChecked = true;
                        
                        if (ct.FromTime != null && ct.FromTime.HasValue)
                        {
                            _uTimeRange_Sang.FromValueChecked = true;
                            _uTimeRange_Sang.FromValue = ct.FromTime.Value;
                            _uTimeRange_Sang.FromOperator = ct.FromTimeOperator;
                        }
                        else
                            _uTimeRange_Sang.FromValueChecked = false;


                        if (ct.ToTime != null && ct.ToTime.HasValue)
                        {
                            _uTimeRange_Sang.ToValueChecked = true;
                            _uTimeRange_Sang.ToValue = ct.ToTime.Value;
                            _uTimeRange_Sang.ToOperator = ct.ToTimeOperator;
                        }
                        else
                            _uTimeRange_Sang.ToValueChecked = false;

                        raChung_Sang.Checked = true;
                        this.Normal_Sang.SetChiTietXetNghiem_Manual(ct);
                        break;

                    case DoiTuong.Chieu_Chung:
                        this.ChieuChecked = true;

                        if (ct.FromTime != null && ct.FromTime.HasValue)
                        {
                            _uTimeRange_Chieu.FromValueChecked = true;
                            _uTimeRange_Chieu.FromValue = ct.FromTime.Value;
                            _uTimeRange_Chieu.FromOperator = ct.FromTimeOperator;
                        }
                        else
                            _uTimeRange_Chieu.FromValueChecked = false;


                        if (ct.ToTime != null && ct.ToTime.HasValue)
                        {
                            _uTimeRange_Chieu.ToValueChecked = true;
                            _uTimeRange_Chieu.ToValue = ct.ToTime.Value;
                            _uTimeRange_Chieu.ToOperator = ct.ToTimeOperator;
                        }
                        else
                            _uTimeRange_Chieu.ToValueChecked = false;

                        raChung_Chieu.Checked = true;
                        this.Normal_Chieu.SetChiTietXetNghiem_Manual(ct);
                        break;

                    case DoiTuong.Sang_Nam:
                    case DoiTuong.Sang_Nu:
                        this.SangChecked = true;
                        if (ct.FromTime != null && ct.FromTime.HasValue)
                        {
                            _uTimeRange_Sang.FromValueChecked = true;
                            _uTimeRange_Sang.FromValue = ct.FromTime.Value;
                            _uTimeRange_Sang.FromOperator = ct.FromTimeOperator;
                        }
                        else
                            _uTimeRange_Sang.FromValueChecked = false;


                        if (ct.ToTime != null && ct.ToTime.HasValue)
                        {
                            _uTimeRange_Sang.ToValueChecked = true;
                            _uTimeRange_Sang.ToValue = ct.ToTime.Value;
                            _uTimeRange_Sang.ToOperator = ct.ToTimeOperator;
                        }
                        else
                            _uTimeRange_Sang.ToValueChecked = false;

                        raNamNu_Sang.Checked = true;
                        uNormal_Nam_Nu_Sang.SetChiTietXetNghiem_ManualList(namNuSangList);
                        break;

                    case DoiTuong.Chieu_Nam:
                    case DoiTuong.Chieu_Nu:
                        this.ChieuChecked = true;

                        if (ct.FromTime != null && ct.FromTime.HasValue)
                        {
                            _uTimeRange_Chieu.FromValueChecked = true;
                            _uTimeRange_Chieu.FromValue = ct.FromTime.Value;
                            _uTimeRange_Chieu.FromOperator = ct.FromTimeOperator;
                        }
                        else
                            _uTimeRange_Chieu.FromValueChecked = false;


                        if (ct.ToTime != null && ct.ToTime.HasValue)
                        {
                            _uTimeRange_Chieu.ToValueChecked = true;
                            _uTimeRange_Chieu.ToValue = ct.ToTime.Value;
                            _uTimeRange_Chieu.ToOperator = ct.ToTimeOperator;
                        }
                        else
                            _uTimeRange_Chieu.ToValueChecked = false;

                        raNamNu_Chieu.Checked = true;
                        uNormal_Nam_Nu_Chieu.SetChiTietXetNghiem_ManualList(namNuChieuList);
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

            if (chkSang.Checked && !_uTimeRange_Sang.CheckInfo())
            {
                _uTimeRange_Sang.Focus();
                return false;
            }

            if (chkSang.Checked && !Normal_Sang.CheckInfo())
            {
                uNormal_Sang.Focus();
                return false;
            }

            if (chkChieu.Checked && !_uTimeRange_Chieu.CheckInfo())
            {
                _uTimeRange_Chieu.Focus();
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
            _uTimeRange_Sang.Enabled = chkSang.Checked;
            raChung_Sang.Enabled = chkSang.Checked;
            raNamNu_Sang.Enabled = chkSang.Checked;

            uNormal_Sang.Enabled = chkSang.Checked && raChung_Sang.Checked;
            uNormal_Nam_Nu_Sang.Enabled = chkSang.Checked && raNamNu_Sang.Checked;
        }

        private void chkChieu_CheckedChanged(object sender, EventArgs e)
        {
            _uTimeRange_Chieu.Enabled = chkChieu.Checked;
            raChung_Chieu.Enabled = chkChieu.Checked;
            raNamNu_Chieu.Enabled = chkChieu.Checked;

            uNormal_Chieu.Enabled = chkChieu.Checked && raChung_Chieu.Checked;
            uNormal_Nam_Nu_Chieu.Enabled = chkChieu.Checked && raNamNu_Chieu.Checked;
        }

        private void raChung_Sang_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_Sang.Enabled = raChung_Sang.Checked && chkSang.Checked;
        }

        private void raNamNu_Sang_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_Nam_Nu_Sang.Enabled = raNamNu_Sang.Checked && chkSang.Checked;
        }

        private void raChung_Chieu_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_Chieu.Enabled = raChung_Chieu.Checked && chkChieu.Checked;
        }

        private void raNamNu_Chieu_CheckedChanged(object sender, EventArgs e)
        {
            uNormal_Nam_Nu_Chieu.Enabled = raNamNu_Chieu.Checked && chkChieu.Checked;
        }
        #endregion
    }
}
