using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Controls
{
    public partial class uChiSoXetNghiem_Hitachi917 : uBase
    {
        #region Members
        private XetNghiem_Hitachi917 _xetNghiem = new XetNghiem_Hitachi917();
        #endregion

        #region Constructor
        public uChiSoXetNghiem_Hitachi917()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        public void ClearData()
        {
            raChung.Checked = true;
            chkFromValue_Chung.Checked = false;
            numFromValue_Chung.Value = 0;
            chkToValue_Chung.Checked = false;
            numToValue_Chung.Value = 0;

            raNamNu.Checked = false;
            chkNam.Checked = false;
            chkFromValue_Nam.Checked = false;
            numFromValue_Nam.Value = 0;
            chkToValue_Nam.Checked = false;
            numToValue_Nam.Value = 0;
            chkNu.Checked = false;
            chkFromValue_Nu.Checked = false;
            numFromValue_Nu.Value = 0;
            chkToValue_Nu.Checked = false;
            numToValue_Nu.Value = 0;

            raTreEmNguoiLonNguoiCaoTuoi.Checked = false;
            chkTreEm.Checked = false;
            chkFromValue_TreEm.Checked = false;
            numFromValue_TreEm.Value = 0;
            chkToValue_TreEm.Checked = false;
            numToValue_TreEm.Value = 0;
            chkNguoiLon.Checked = false;
            chkFromValue_NguoiLon.Checked = false;
            numFromValue_NguoiLon.Value = 0;
            chkToValue_NguoiLon.Checked = false;
            numToValue_NguoiLon.Value = 0;
            chkNguoiCaoTuoi.Checked = false;
            chkFromValue_NguoiCaoTuoi.Checked = false;
            numFromValue_NguoiCaoTuoi.Value = 0;
            chkToValue_NguoiCaoTuoi.Checked = false;
            numToValue_NguoiCaoTuoi.Value = 0;
        }

        public void DisplayInfo(DataRow drXetNghiem)
        {
            try
            {
                ClearData();
                _xetNghiem.XetNghiemGUID = Guid.Parse(drXetNghiem["XetNghiemGUID"].ToString());
                Result result = XetNghiem_Hitachi917Bus.GetChiTietXetNghiemList(_xetNghiem.XetNghiemGUID.ToString());
                if (result.IsOK)
                {
                    DataTable dt = result.QueryResult as DataTable;
                    foreach (DataRow row in dt.Rows)
                    {
                        switch ((DoiTuong)Convert.ToByte(row["DoiTuong"]))
                        {
                            case DoiTuong.Chung:
                                raChung.Checked = true;
                                if (row["FromValue"] != null && row["FromValue"] != DBNull.Value)
                                {
                                    chkFromValue_Chung.Checked = true;
                                    numFromValue_Chung.Value = (Decimal)Convert.ToDouble(row["FromValue"]);
                                }

                                if (row["ToValue"] != null && row["ToValue"] != DBNull.Value)
                                {
                                    chkToValue_Chung.Checked = true;
                                    numToValue_Chung.Value = (Decimal)Convert.ToDouble(row["ToValue"]);
                                }

                                if (row["DonVi"] != null && row["DonVi"] != DBNull.Value)
                                    txtDonVi_Chung.Text = row["DonVi"].ToString();
                                break;
                            case DoiTuong.Nam:
                                raNamNu.Checked = true;
                                chkNam.Checked = true;
                                if (row["FromValue"] != null && row["FromValue"] != DBNull.Value)
                                {
                                    chkFromValue_Nam.Checked = true;
                                    numFromValue_Nam.Value = (Decimal)Convert.ToDouble(row["FromValue"]);
                                }

                                if (row["ToValue"] != null && row["ToValue"] != DBNull.Value)
                                {
                                    chkToValue_Nam.Checked = true;
                                    numToValue_Nam.Value = (Decimal)Convert.ToDouble(row["ToValue"]);
                                }

                                if (row["DonVi"] != null && row["DonVi"] != DBNull.Value)
                                    txtDonVi_Nam.Text = row["DonVi"].ToString();
                                break;
                            case DoiTuong.Nu:
                                raNamNu.Checked = true;
                                chkNu.Checked = true;
                                if (row["FromValue"] != null && row["FromValue"] != DBNull.Value)
                                {
                                    chkFromValue_Nu.Checked = true;
                                    numFromValue_Nu.Value = (Decimal)Convert.ToDouble(row["FromValue"]);
                                }

                                if (row["ToValue"] != null && row["ToValue"] != DBNull.Value)
                                {
                                    chkToValue_Nu.Checked = true;
                                    numToValue_Nu.Value = (Decimal)Convert.ToDouble(row["ToValue"]);
                                }

                                if (row["DonVi"] != null && row["DonVi"] != DBNull.Value)
                                    txtDonVi_Nu.Text = row["DonVi"].ToString();
                                break;
                            case DoiTuong.TreEm:
                                raTreEmNguoiLonNguoiCaoTuoi.Checked = true;
                                chkTreEm.Checked = true;
                                if (row["FromValue"] != null && row["FromValue"] != DBNull.Value)
                                {
                                    chkFromValue_TreEm.Checked = true;
                                    numFromValue_TreEm.Value = (Decimal)Convert.ToDouble(row["FromValue"]);
                                }

                                if (row["ToValue"] != null && row["ToValue"] != DBNull.Value)
                                {
                                    chkToValue_TreEm.Checked = true;
                                    numToValue_TreEm.Value = (Decimal)Convert.ToDouble(row["ToValue"]);
                                }

                                if (row["DonVi"] != null && row["DonVi"] != DBNull.Value)
                                    txtDonVi_TreEm.Text = row["DonVi"].ToString();
                                break;
                            case DoiTuong.NguoiLon:
                                raTreEmNguoiLonNguoiCaoTuoi.Checked = true;
                                chkNguoiLon.Checked = true;
                                if (row["FromValue"] != null && row["FromValue"] != DBNull.Value)
                                {
                                    chkFromValue_NguoiLon.Checked = true;
                                    numFromValue_NguoiLon.Value = (Decimal)Convert.ToDouble(row["FromValue"]);
                                }

                                if (row["ToValue"] != null && row["ToValue"] != DBNull.Value)
                                {
                                    chkToValue_NguoiLon.Checked = true;
                                    numToValue_NguoiLon.Value = (Decimal)Convert.ToDouble(row["ToValue"]);
                                }

                                if (row["DonVi"] != null && row["DonVi"] != DBNull.Value)
                                    txtDonVi_NguoiLon.Text = row["DonVi"].ToString();
                                break;
                            case DoiTuong.NguoiCaoTuoi:
                                raTreEmNguoiLonNguoiCaoTuoi.Checked = true;
                                chkNguoiCaoTuoi.Checked = true;
                                if (row["FromValue"] != null && row["FromValue"] != DBNull.Value)
                                {
                                    chkFromValue_NguoiCaoTuoi.Checked = true;
                                    numFromValue_NguoiCaoTuoi.Value = (Decimal)Convert.ToDouble(row["FromValue"]);
                                }

                                if (row["ToValue"] != null && row["ToValue"] != DBNull.Value)
                                {
                                    chkToValue_NguoiCaoTuoi.Checked = true;
                                    numToValue_NguoiCaoTuoi.Value = (Decimal)Convert.ToDouble(row["ToValue"]);
                                }

                                if (row["DonVi"] != null && row["DonVi"] != DBNull.Value)
                                    txtDonVi_NguoiCaoTuoi.Text = row["DonVi"].ToString();
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        public bool CheckInfo()
        {
            if (raChung.Checked)
            {
                if (!chkFromValue_Chung.Checked && !chkToValue_Chung.Checked)
                {
                    MsgBox.Show(Application.ProductName, "Vui lòng nhập ít nhất 1 trong 2 chỉ số.", IconType.Information);
                    chkFromValue_Chung.Focus();
                    return false;
                }

                if (chkFromValue_Chung.Checked && chkToValue_Chung.Checked)
                {
                    if (numFromValue_Chung.Value > numToValue_Chung.Value)
                    {
                        MsgBox.Show(Application.ProductName, "Chỉ số từ phải nhỏ hơn hoặc bằng chỉ số đến.", IconType.Information);
                        numFromValue_Chung.Focus();
                        return false;
                    }
                }
            }
            else if (raNamNu.Checked)
            {
                if (!chkNam.Checked && !chkNu.Checked)
                {
                    MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu ít nhất 1 trong 2 đối tưởng nam và nữ.", IconType.Information);
                    chkNam.Focus();
                    return false;
                }

                if (chkNam.Checked)
                {
                    if (!chkFromValue_Nam.Checked && !chkToValue_Nam.Checked)
                    {
                        MsgBox.Show(Application.ProductName, "Vui lòng nhập ít nhất 1 trong 2 chỉ số.", IconType.Information);
                        chkFromValue_Nam.Focus();
                        return false;
                    }

                    if (chkFromValue_Nam.Checked && chkToValue_Nam.Checked)
                    {
                        if (numFromValue_Nam.Value > numToValue_Nam.Value)
                        {
                            MsgBox.Show(Application.ProductName, "Chỉ số từ phải nhỏ hơn hoặc bằng chỉ số đến.", IconType.Information);
                            numFromValue_Nam.Focus();
                            return false;
                        }
                    }
                }

                if (chkNu.Checked)
                {
                    if (!chkFromValue_Nu.Checked && !chkToValue_Nu.Checked)
                    {
                        MsgBox.Show(Application.ProductName, "Vui lòng nhập ít nhất 1 trong 2 chỉ số.", IconType.Information);
                        chkFromValue_Nu.Focus();
                        return false;
                    }

                    if (chkFromValue_Nu.Checked && chkToValue_Nu.Checked)
                    {
                        if (numFromValue_Nu.Value > numToValue_Nu.Value)
                        {
                            MsgBox.Show(Application.ProductName, "Chỉ số từ phải nhỏ hơn hoặc bằng chỉ số đến.", IconType.Information);
                            numFromValue_Nu.Focus();
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (!chkTreEm.Checked && !chkNguoiLon.Checked && !chkNguoiCaoTuoi.Checked)
                {
                    MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu ít nhất 1 trong 3 đối tưởng trẻ em, người lớn và người cao tuổi.", IconType.Information);
                    chkTreEm.Focus();
                    return false;
                }

                if (chkTreEm.Checked)
                {
                    if (!chkFromValue_TreEm.Checked && !chkToValue_TreEm.Checked)
                    {
                        MsgBox.Show(Application.ProductName, "Vui lòng nhập ít nhất 1 trong 2 chỉ số.", IconType.Information);
                        chkFromValue_TreEm.Focus();
                        return false;
                    }

                    if (chkFromValue_TreEm.Checked && chkToValue_TreEm.Checked)
                    {
                        if (numFromValue_TreEm.Value > numToValue_TreEm.Value)
                        {
                            MsgBox.Show(Application.ProductName, "Chỉ số từ phải nhỏ hơn hoặc bằng chỉ số đến.", IconType.Information);
                            numFromValue_TreEm.Focus();
                            return false;
                        }
                    }
                }

                if (chkNguoiLon.Checked)
                {
                    if (!chkFromValue_NguoiLon.Checked && !chkToValue_NguoiLon.Checked)
                    {
                        MsgBox.Show(Application.ProductName, "Vui lòng nhập ít nhất 1 trong 2 chỉ số.", IconType.Information);
                        chkFromValue_NguoiLon.Focus();
                        return false;
                    }

                    if (chkFromValue_NguoiLon.Checked && chkToValue_NguoiLon.Checked)
                    {
                        if (numFromValue_NguoiLon.Value > numToValue_NguoiLon.Value)
                        {
                            MsgBox.Show(Application.ProductName, "Chỉ số từ phải nhỏ hơn hoặc bằng chỉ số đến.", IconType.Information);
                            numFromValue_NguoiLon.Focus();
                            return false;
                        }
                    }
                }

                if (chkNguoiCaoTuoi.Checked)
                {
                    if (!chkFromValue_NguoiCaoTuoi.Checked && !chkToValue_NguoiCaoTuoi.Checked)
                    {
                        MsgBox.Show(Application.ProductName, "Vui lòng nhập ít nhất 1 trong 2 chỉ số.", IconType.Information);
                        chkFromValue_NguoiCaoTuoi.Focus();
                        return false;
                    }

                    if (chkFromValue_NguoiCaoTuoi.Checked && chkToValue_NguoiCaoTuoi.Checked)
                    {
                        if (numFromValue_NguoiCaoTuoi.Value > numToValue_NguoiCaoTuoi.Value)
                        {
                            MsgBox.Show(Application.ProductName, "Chỉ số từ phải nhỏ hơn hoặc bằng chỉ số đến.", IconType.Information);
                            numFromValue_NguoiCaoTuoi.Focus();
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private void SaveInfoAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSaveInfoProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnSaveInfo()
        {
            try
            {
                MethodInvoker method = delegate
                {
                    List<ChiTietXetNghiem_Hitachi917> ctxns = new List<ChiTietXetNghiem_Hitachi917>();
                    if (raChung.Checked)
                    {
                        ChiTietXetNghiem_Hitachi917 ctxn = new ChiTietXetNghiem_Hitachi917();
                        ctxn.DoiTuong = (byte)DoiTuong.Chung;
                        if (chkFromValue_Chung.Checked) ctxn.FromValue = (double)numFromValue_Chung.Value;
                        if (chkToValue_Chung.Checked) ctxn.ToValue = (double)numToValue_Chung.Value;
                        ctxn.DonVi = txtDonVi_Chung.Text;
                        ctxns.Add(ctxn);
                    }
                    else if (raNamNu.Checked)
                    {
                        if (chkNam.Checked)
                        {
                            ChiTietXetNghiem_Hitachi917 ctxn = new ChiTietXetNghiem_Hitachi917();
                            ctxn.DoiTuong = (byte)DoiTuong.Nam;
                            if (chkFromValue_Nam.Checked) ctxn.FromValue = (double)numFromValue_Nam.Value;
                            if (chkToValue_Nam.Checked) ctxn.ToValue = (double)numToValue_Nam.Value;
                            ctxn.DonVi = txtDonVi_Nam.Text;
                            ctxns.Add(ctxn);
                        }

                        if (chkNu.Checked)
                        {
                            ChiTietXetNghiem_Hitachi917 ctxn = new ChiTietXetNghiem_Hitachi917();
                            ctxn.DoiTuong = (byte)DoiTuong.Nu;
                            if (chkFromValue_Nu.Checked) ctxn.FromValue = (double)numFromValue_Nu.Value;
                            if (chkToValue_Nu.Checked) ctxn.ToValue = (double)numToValue_Nu.Value;
                            ctxn.DonVi = txtDonVi_Nu.Text;
                            ctxns.Add(ctxn);
                        }
                    }
                    else
                    {
                        if (chkTreEm.Checked)
                        {
                            ChiTietXetNghiem_Hitachi917 ctxn = new ChiTietXetNghiem_Hitachi917();
                            ctxn.DoiTuong = (byte)DoiTuong.TreEm;
                            if (chkFromValue_TreEm.Checked) ctxn.FromValue = (double)numFromValue_TreEm.Value;
                            if (chkToValue_TreEm.Checked) ctxn.ToValue = (double)numToValue_TreEm.Value;
                            ctxn.DonVi = txtDonVi_TreEm.Text;
                            ctxns.Add(ctxn);
                        }

                        if (chkNguoiLon.Checked)
                        {
                            ChiTietXetNghiem_Hitachi917 ctxn = new ChiTietXetNghiem_Hitachi917();
                            ctxn.DoiTuong = (byte)DoiTuong.NguoiLon;
                            if (chkFromValue_NguoiLon.Checked) ctxn.FromValue = (double)numFromValue_NguoiLon.Value;
                            if (chkToValue_NguoiLon.Checked) ctxn.ToValue = (double)numToValue_NguoiLon.Value;
                            ctxn.DonVi = txtDonVi_NguoiLon.Text;
                            ctxns.Add(ctxn);
                        }

                        if (chkNguoiCaoTuoi.Checked)
                        {
                            ChiTietXetNghiem_Hitachi917 ctxn = new ChiTietXetNghiem_Hitachi917();
                            ctxn.DoiTuong = (byte)DoiTuong.NguoiCaoTuoi;
                            if (chkFromValue_NguoiCaoTuoi.Checked) ctxn.FromValue = (double)numFromValue_NguoiCaoTuoi.Value;
                            if (chkToValue_NguoiCaoTuoi.Checked) ctxn.ToValue = (double)numToValue_NguoiCaoTuoi.Value;
                            ctxn.DonVi = txtDonVi_NguoiCaoTuoi.Text;
                            ctxns.Add(ctxn);
                        }
                    }

                    Result result = XetNghiem_Hitachi917Bus.UpdateXetNghiem(_xetNghiem, ctxns);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_Hitachi917Bus.UpdateXetNghiem"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_Hitachi917Bus.UpdateXetNghiem"));
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, "Lưu chỉ số xét nghiệm thành công.", IconType.Information);
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            catch (Exception e)
            {
                MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }
        #endregion

        #region Window Event Handlers
        private void raChung_CheckedChanged(object sender, EventArgs e)
        {
            gbChung.Enabled = raChung.Checked;
            gbNamNu.Enabled = !raChung.Checked;
            gbTreEmNguoiLonNguoiCaoTuoi.Enabled = !raChung.Checked;
        }

        private void chkFromValue_Chung_CheckedChanged(object sender, EventArgs e)
        {
            numFromValue_Chung.Enabled = chkFromValue_Chung.Checked;
        }

        private void chkToValue_Chung_CheckedChanged(object sender, EventArgs e)
        {
            numToValue_Chung.Enabled = chkToValue_Chung.Checked;
        }

        private void raNamNu_CheckedChanged(object sender, EventArgs e)
        {
            gbNamNu.Enabled = raNamNu.Checked;
            gbChung.Enabled = !raNamNu.Checked;
            gbTreEmNguoiLonNguoiCaoTuoi.Enabled = !raNamNu.Checked;
        }

        private void raTreEmNguoiLonNguoiCaoTuoi_CheckedChanged(object sender, EventArgs e)
        {
            gbTreEmNguoiLonNguoiCaoTuoi.Enabled = raTreEmNguoiLonNguoiCaoTuoi.Checked;
            gbChung.Enabled = !raTreEmNguoiLonNguoiCaoTuoi.Checked;
            gbNamNu.Enabled = !raTreEmNguoiLonNguoiCaoTuoi.Checked;
        }

        private void chkNam_CheckedChanged(object sender, EventArgs e)
        {
            chkFromValue_Nam.Enabled = chkNam.Checked;
            chkToValue_Nam.Enabled = chkNam.Checked;
            //txtDonVi_Nam.Enabled = chkNam.Checked;

            numFromValue_Nam.Enabled = chkNam.Checked && chkFromValue_Nam.Checked;
            numToValue_Nam.Enabled = chkNam.Checked && chkToValue_Nam.Checked;
        }

        private void chkNu_CheckedChanged(object sender, EventArgs e)
        {
            chkFromValue_Nu.Enabled = chkNu.Checked;
            chkToValue_Nu.Enabled = chkNu.Checked;
            //txtDonVi_Nu.Enabled = chkNu.Checked;

            numFromValue_Nu.Enabled = chkNu.Checked && chkFromValue_Nu.Checked;
            numToValue_Nu.Enabled = chkNu.Checked && chkToValue_Nu.Checked;
        }

        private void chkFromValue_Nam_CheckedChanged(object sender, EventArgs e)
        {
            numFromValue_Nam.Enabled = chkFromValue_Nam.Checked;
        }

        private void chkToValue_Nam_CheckedChanged(object sender, EventArgs e)
        {
            numToValue_Nam.Enabled = chkToValue_Nam.Checked;
        }

        private void chkFromValue_Nu_CheckedChanged(object sender, EventArgs e)
        {
            numFromValue_Nu.Enabled = chkFromValue_Nu.Checked;
        }

        private void chkToValue_Nu_CheckedChanged(object sender, EventArgs e)
        {
            numToValue_Nu.Enabled = chkToValue_Nu.Checked;
        }

        private void chkTreEm_CheckedChanged(object sender, EventArgs e)
        {
            chkFromValue_TreEm.Enabled = chkTreEm.Checked;
            chkToValue_TreEm.Enabled = chkTreEm.Checked;
            //txtDonVi_TreEm.Enabled = chkTreEm.Checked;

            numFromValue_TreEm.Enabled = chkTreEm.Checked && chkFromValue_TreEm.Checked;
            numToValue_TreEm.Enabled = chkTreEm.Checked && chkToValue_TreEm.Checked;
        }

        private void chkNguoiLon_CheckedChanged(object sender, EventArgs e)
        {
            chkFromValue_NguoiLon.Enabled = chkNguoiLon.Checked;
            chkToValue_NguoiLon.Enabled = chkNguoiLon.Checked;
            //txtDonVi_NguoiLon.Enabled = chkNguoiLon.Checked;

            numFromValue_NguoiLon.Enabled = chkNguoiLon.Checked && chkFromValue_NguoiLon.Checked;
            numToValue_NguoiLon.Enabled = chkNguoiLon.Checked && chkToValue_NguoiLon.Checked;
        }

        private void chkNguoiCaoTuoi_CheckedChanged(object sender, EventArgs e)
        {
            chkFromValue_NguoiCaoTuoi.Enabled = chkNguoiCaoTuoi.Checked;
            chkToValue_NguoiCaoTuoi.Enabled = chkNguoiCaoTuoi.Checked;
            //txtDonVi_NguoiCaoTuoi.Enabled = chkNguoiCaoTuoi.Checked;

            numFromValue_NguoiCaoTuoi.Enabled = chkNguoiCaoTuoi.Checked && chkFromValue_NguoiCaoTuoi.Checked;
            numToValue_NguoiCaoTuoi.Enabled = chkNguoiCaoTuoi.Checked && chkToValue_NguoiCaoTuoi.Checked;
        }

        private void chkFromValue_TreEm_CheckedChanged(object sender, EventArgs e)
        {
            numFromValue_TreEm.Enabled = chkFromValue_TreEm.Checked;
        }

        private void chkToValue_TreEm_CheckedChanged(object sender, EventArgs e)
        {
            numToValue_TreEm.Enabled = chkToValue_TreEm.Checked;
        }

        private void chkFromValue_NguoiLon_CheckedChanged(object sender, EventArgs e)
        {
            numFromValue_NguoiLon.Enabled = chkFromValue_NguoiLon.Checked;
        }

        private void chkToValue_NguoiLon_CheckedChanged(object sender, EventArgs e)
        {
            numToValue_NguoiLon.Enabled = chkToValue_NguoiLon.Checked;
        }

        private void chkFromValue_NguoiCaoTuoi_CheckedChanged(object sender, EventArgs e)
        {
            numFromValue_NguoiCaoTuoi.Enabled = chkFromValue_NguoiCaoTuoi.Checked;
        }

        private void chkToValue_NguoiCaoTuoi_CheckedChanged(object sender, EventArgs e)
        {
            numToValue_NguoiCaoTuoi.Enabled = chkToValue_NguoiCaoTuoi.Checked;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CheckInfo())
                SaveInfoAsThread();
        }
        #endregion

        #region Working Thread
        private void OnSaveInfoProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnSaveInfo();
            }
            catch (Exception e)
            {
                MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion
    }
}
