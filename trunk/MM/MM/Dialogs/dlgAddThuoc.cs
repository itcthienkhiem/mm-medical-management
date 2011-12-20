using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgAddThuoc : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private Thuoc _thuoc = new Thuoc();
        #endregion

        #region Constructor
        public dlgAddThuoc()
        {
            InitializeComponent();
            GenerateCode();
        }

        public dlgAddThuoc(DataRow drThuoc)
        {
            InitializeComponent();
            _isNew = false;
            this.Text = "Sua thuoc";
            DisplayInfo(drThuoc);
        }
        #endregion

        #region Properties
        public Thuoc Thuoc
        {
            get { return _thuoc; }
        }
        #endregion

        #region UI Command
        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = ThuocBus.GetThuocCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                txtMaThuoc.Text = Utility.GetCode("TH", count + 1, 5);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ThuocBus.GetThuocCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThuocBus.GetThuocCount"));
            }
        }

        private void DisplayInfo(DataRow drThuoc)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                txtMaThuoc.Text = drThuoc["MaThuoc"] as string;
                txtTenThuoc.Text = drThuoc["TenThuoc"] as string;
                txtBietDuoc.Text = drThuoc["BietDuoc"] as string;

                if (drThuoc["HamLuong"] != null && drThuoc["HamLuong"] != DBNull.Value)
                    txtHamLuong.Text = drThuoc["HamLuong"] as string;

                if (drThuoc["HoatChat"] != null && drThuoc["HoatChat"] != DBNull.Value)
                    txtHoatChat.Text = drThuoc["HoatChat"] as string;

                txtNote.Text = drThuoc["Note"] as string;

                _thuoc.ThuocGUID = Guid.Parse(drThuoc["ThuocGUID"].ToString());

                if (drThuoc["CreatedDate"] != null && drThuoc["CreatedDate"] != DBNull.Value)
                    _thuoc.CreatedDate = Convert.ToDateTime(drThuoc["CreatedDate"]);

                if (drThuoc["CreatedBy"] != null && drThuoc["CreatedBy"] != DBNull.Value)
                    _thuoc.CreatedBy = Guid.Parse(drThuoc["CreatedBy"].ToString());

                if (drThuoc["UpdatedDate"] != null && drThuoc["UpdatedDate"] != DBNull.Value)
                    _thuoc.UpdatedDate = Convert.ToDateTime(drThuoc["UpdatedDate"]);

                if (drThuoc["UpdatedBy"] != null && drThuoc["UpdatedBy"] != DBNull.Value)
                    _thuoc.UpdatedBy = Guid.Parse(drThuoc["UpdatedBy"].ToString());

                if (drThuoc["DeletedDate"] != null && drThuoc["DeletedDate"] != DBNull.Value)
                    _thuoc.DeletedDate = Convert.ToDateTime(drThuoc["DeletedDate"]);

                if (drThuoc["DeletedBy"] != null && drThuoc["DeletedBy"] != DBNull.Value)
                    _thuoc.DeletedBy = Guid.Parse(drThuoc["DeletedBy"].ToString());

                _thuoc.Status = Convert.ToByte(drThuoc["Status"]);

                DisplayDonViTinh(_thuoc.ThuocGUID.ToString());
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void DisplayDonViTinh(string thuocGUID)
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = ThuocBus.GetDonViTinhList(thuocGUID);
            if (result.IsOK)
            {
                if (result.QueryResult != null)
                {
                    List<DonViTinh_Thuoc> donViTinhList = (List<DonViTinh_Thuoc>)result.QueryResult;
                    if (donViTinhList.Count == 2)
                    {
                        raNhom1.Checked = true;
                        foreach (DonViTinh_Thuoc dvt in donViTinhList)
                        {
                            if (dvt.DonViTinhLon == "Hộp")
                                numVi_Nhom1.Value = (Decimal)dvt.SoLuong;
                            else
                                numVien_Nhom1.Value = (Decimal)dvt.SoLuong;
                        }
                    }
                    else if (donViTinhList.Count == 1)
                    {
                        DonViTinh_Thuoc dvt = donViTinhList[0];
                        switch (dvt.DonViTinhNho)
                        {
                            case "Viên":
                                raNhom2.Checked = true;
                                numVien_Nhom2.Value = (Decimal)dvt.SoLuong;
                                break;

                            case "Chai":
                                raNhom3.Checked = true;
                                numChai_Nhom3.Value = (Decimal)dvt.SoLuong;
                                break;

                            case "Ống":
                                raNhom4.Checked = true;
                                numOng_Nhom4.Value = (Decimal)dvt.SoLuong;
                                break;

                            case "Miếng":
                                raNhom5.Checked = true;
                                numMieng_Nhom5.Value = (Decimal)dvt.SoLuong;
                                break;

                            case "Gói":
                                raNhom6.Checked = true;
                                numGoi_Nhom6.Value = (Decimal)dvt.SoLuong;
                                break;
                        }
                    }
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ThuocBus.GetDonViTinhList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ThuocBus.GetDonViTinhList"));
            }
        }

        private bool CheckInfo()
        {
            if (txtMaThuoc.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã thuốc.", IconType.Information);
                txtMaThuoc.Focus();
                return false;
            }

            if (txtTenThuoc.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên thuốc.", IconType.Information);
                txtTenThuoc.Focus();
                return false;
            }

            string thuocGUID = _isNew ? string.Empty : _thuoc.ThuocGUID.ToString();
            Result result = ThuocBus.CheckThuocExistCode(thuocGUID, txtMaThuoc.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Mã thuốc này đã tồn tại rồi. Vui lòng nhập mã khác.", IconType.Information);
                    txtMaThuoc.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("ThuocBus.CheckThuocExistCode"), IconType.Error);
                return false;
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
                MsgBox.Show(this.Text, e.Message, IconType.Error);
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
                _thuoc.MaThuoc = txtMaThuoc.Text;
                _thuoc.TenThuoc = txtTenThuoc.Text;
                _thuoc.BietDuoc = txtBietDuoc.Text;
                _thuoc.HamLuong = txtHamLuong.Text;
                _thuoc.HoatChat = txtHoatChat.Text;
                _thuoc.Note = txtNote.Text;
                _thuoc.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _thuoc.CreatedDate = DateTime.Now;
                    _thuoc.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _thuoc.UpdatedDate = DateTime.Now;
                    _thuoc.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                List<DonViTinh_Thuoc> donViTinhList = new List<DonViTinh_Thuoc>();
                MethodInvoker method = delegate
                {
                    if (raNhom1.Checked)
                    {
                        DonViTinh_Thuoc dvt = new DonViTinh_Thuoc();
                        dvt.DonViTinhLon = "Hộp";
                        dvt.DonViTinhNho = "Vĩ";
                        dvt.SoLuong = (int)numVi_Nhom1.Value;
                        dvt.CreatedDate = DateTime.Now;
                        dvt.CreatedBy = Guid.Parse(Global.UserGUID);
                        dvt.Status = (byte)Status.Actived;
                        donViTinhList.Add(dvt);

                        dvt = new DonViTinh_Thuoc();
                        dvt.DonViTinhLon = "Vĩ";
                        dvt.DonViTinhNho = "Viên";
                        dvt.SoLuong = (int)numVien_Nhom1.Value;
                        dvt.CreatedDate = DateTime.Now;
                        dvt.CreatedBy = Guid.Parse(Global.UserGUID);
                        dvt.Status = (byte)Status.Actived;
                        donViTinhList.Add(dvt);
                    }
                    else if (raNhom2.Checked)
                    {
                        DonViTinh_Thuoc dvt = new DonViTinh_Thuoc();
                        dvt.DonViTinhLon = "Hộp";
                        dvt.DonViTinhNho = "Viên";
                        dvt.SoLuong = (int)numVien_Nhom2.Value;
                        dvt.CreatedDate = DateTime.Now;
                        dvt.CreatedBy = Guid.Parse(Global.UserGUID);
                        dvt.Status = (byte)Status.Actived;
                        donViTinhList.Add(dvt);
                    }
                    else if (raNhom3.Checked)
                    {
                        DonViTinh_Thuoc dvt = new DonViTinh_Thuoc();
                        dvt.DonViTinhLon = "Hộp";
                        dvt.DonViTinhNho = "Chai";
                        dvt.SoLuong = (int)numChai_Nhom3.Value;
                        dvt.CreatedDate = DateTime.Now;
                        dvt.CreatedBy = Guid.Parse(Global.UserGUID);
                        dvt.Status = (byte)Status.Actived;
                        donViTinhList.Add(dvt);
                    }
                    else if (raNhom4.Checked)
                    {
                        DonViTinh_Thuoc dvt = new DonViTinh_Thuoc();
                        dvt.DonViTinhLon = "Hộp";
                        dvt.DonViTinhNho = "Ống";
                        dvt.SoLuong = (int)numOng_Nhom4.Value;
                        dvt.CreatedDate = DateTime.Now;
                        dvt.CreatedBy = Guid.Parse(Global.UserGUID);
                        dvt.Status = (byte)Status.Actived;
                        donViTinhList.Add(dvt);
                    }
                    else if (raNhom5.Checked)
                    {
                        DonViTinh_Thuoc dvt = new DonViTinh_Thuoc();
                        dvt.DonViTinhLon = "Hộp";
                        dvt.DonViTinhNho = "Miếng";
                        dvt.SoLuong = (int)numMieng_Nhom5.Value;
                        dvt.CreatedDate = DateTime.Now;
                        dvt.CreatedBy = Guid.Parse(Global.UserGUID);
                        dvt.Status = (byte)Status.Actived;
                        donViTinhList.Add(dvt);
                    }
                    else if (raNhom6.Checked)
                    {
                        DonViTinh_Thuoc dvt = new DonViTinh_Thuoc();
                        dvt.DonViTinhLon = "Hộp";
                        dvt.DonViTinhNho = "Gói";
                        dvt.SoLuong = (int)numGoi_Nhom6.Value;
                        dvt.CreatedDate = DateTime.Now;
                        dvt.CreatedBy = Guid.Parse(Global.UserGUID);
                        dvt.Status = (byte)Status.Actived;
                        donViTinhList.Add(dvt);
                    }

                    Result result = ThuocBus.InsertThuoc(_thuoc, donViTinhList);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("ThuocBus.InsertThuoc"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("ThuocBus.InsertThuoc"));
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddThuoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void raNhom1_CheckedChanged(object sender, EventArgs e)
        {
            numVi_Nhom1.Enabled = raNhom1.Checked;
            numVien_Nhom1.Enabled = raNhom1.Checked;
        }

        private void raNhom2_CheckedChanged(object sender, EventArgs e)
        {
            numVien_Nhom2.Enabled = raNhom2.Checked;
        }

        private void raNhom3_CheckedChanged(object sender, EventArgs e)
        {
            numChai_Nhom3.Enabled = raNhom3.Checked;
        }

        private void raNhom4_CheckedChanged(object sender, EventArgs e)
        {
            numOng_Nhom4.Enabled = raNhom4.Checked;
        }

        private void raNhom5_CheckedChanged(object sender, EventArgs e)
        {
            numMieng_Nhom5.Enabled = raNhom5.Checked;
        }

        private void raNhom6_CheckedChanged(object sender, EventArgs e)
        {
            numGoi_Nhom6.Enabled = raNhom6.Checked;
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
                MsgBox.Show(this.Text, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion
    }
}
