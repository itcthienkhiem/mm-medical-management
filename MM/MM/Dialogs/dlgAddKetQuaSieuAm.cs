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
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Threading;
using DShowNET;
using DShowNET.Device;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;
    
namespace MM.Dialogs
{
    public partial class dlgAddKetQuaSieuAm : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private string _patientGUID = string.Empty;
        private KetQuaSieuAm _ketQuaSieuAm = new KetQuaSieuAm();
        private DataRow _drKetQuaSieuAm = null;
        private bool _isPrint = false;
        private bool _allowEdit = true;
        private string _gioiTinh = string.Empty;
        private int _hinh = 1;
        private Hashtable _htMauBaoCao = new Hashtable();
        private string _loaiSieuAmGUID = string.Empty;
        private WatchingFolder _watchingFolder = null;
        private string _maBenhNhan = string.Empty;
        #endregion

        #region Constructor
        public dlgAddKetQuaSieuAm(string patientGUID, string gioiTinh)
        {
            InitializeComponent();
            _patientGUID = patientGUID;
            _gioiTinh = gioiTinh;
        }

        public dlgAddKetQuaSieuAm(string patientGUID, string gioiTinh, DataRow drKetQuaSieuAm, bool allowEdit)
        {
            InitializeComponent();
            _allowEdit = allowEdit;
            _patientGUID = patientGUID;
            _gioiTinh = gioiTinh;
            _drKetQuaSieuAm = drKetQuaSieuAm;
            _isNew = false;
            this.Text = "Sua ket qua sieu am";
        }
        #endregion

        #region Properties
        public bool IsPrint
        {
            get { return _isPrint; }
        }

        public KetQuaSieuAm KetQuaSieuAm
        {
            get { return _ketQuaSieuAm; }
        }
        #endregion

        #region UI Commnad
        private void InitData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                dtpkNgaySieuAm.Value = DateTime.Now;
                DisplayDSBacSiChiDinh();
                DisplayDSBasSiSieuAm();
                DisplayLoaiSieuAm();

                btnHinh1.Enabled = _allowEdit;
                btnHinh2.Enabled = _allowEdit;

                if (_allowEdit)
                {
                    //if (!Global.TVHomeConfig.SuDungSieuAm)
                    //{
                    //    PlayCapFactory.RunPlayCapProcess(false);
                    //    PlayCapFactory.OnCaptureCompletedEvent += new CaptureCompletedHandler(PlayCapFactory_OnCaptureCompletedEvent);
                    //}
                    //else
                    {
                        btnHinh1.Visible = false;
                        btnHinh2.Visible = false;

                        _watchingFolder = new WatchingFolder();
                        _watchingFolder.OnCreatedFileEvent += new CreatedFileEventHandler(_watchingFolder_OnCreatedFileEvent);
                        _watchingFolder.StartMoritoring(Global.HinhChupPath);

                        if (!Utility.CheckRunningProcess(Const.TVHomeProcessName))
                            Utility.ExecuteFile(Global.TVHomeConfig.Path);
                    }
                }

                Result result = PatientBus.GetPatient2(_patientGUID);
                if (result.IsOK)
                {
                    PatientView patient = result.QueryResult as PatientView;
                    _maBenhNhan = patient.FileNum;
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("PatientBus.GetPatient"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatient"));
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(this.Text, ex.Message, IconType.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
        }

        private void StopWatchingFolder()
        {
            try
            {
                if (_watchingFolder == null) return;
                _watchingFolder.OnCreatedFileEvent -= new CreatedFileEventHandler(_watchingFolder_OnCreatedFileEvent);
                _watchingFolder.StopMoritoring();
                _watchingFolder = null;
            }
            catch (Exception ex)
            {
                MsgBox.Show(this.Text, ex.Message, IconType.Error);
                Utility.WriteToTraceLog(ex.Message);
            }
        }

        private void DisplayLoaiSieuAm()
        {
            bool isNam = _gioiTinh.ToLower() == "nam" ? true : false;
            Result result = SieuAmBus.GetLoaiSieuAmList(isNam);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("SieuAmBus.GetLoaiSieuAmList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("SieuAmBus.GetLoaiSieuAmList"));
                return;
            }
            else
                cboLoaiSieuAm.DataSource = result.QueryResult;
        }

        private void DisplayDSBacSiChiDinh()
        {
            //DocStaff
            List<byte> staffTypes = new List<byte>();
            staffTypes.Add((byte)StaffType.BacSi);
            staffTypes.Add((byte)StaffType.BacSiSieuAm);
            staffTypes.Add((byte)StaffType.BacSiNgoaiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiNoiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiPhuKhoa);
            Result result = DocStaffBus.GetDocStaffList(staffTypes);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
                return;
            }
            else
            {
                DataTable dt = result.QueryResult as DataTable;
                DataRow newRow = dt.NewRow();
                newRow["Fullname"] = string.Empty;
                newRow["DocStaffGUID"] = Guid.Empty.ToString();
                dt.Rows.InsertAt(newRow, 0);

                cboBSCD.DataSource = dt;
            }
        }

        private void DisplayDSBasSiSieuAm()
        {
            //DocStaff
            List<byte> staffTypes = new List<byte>();
            staffTypes.Add((byte)StaffType.BacSi);
            staffTypes.Add((byte)StaffType.BacSiSieuAm);
            staffTypes.Add((byte)StaffType.BacSiNgoaiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiNoiTongQuat);
            staffTypes.Add((byte)StaffType.BacSiPhuKhoa);
            Result result = DocStaffBus.GetDocStaffList(staffTypes);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("DocStaffBus.GetDocStaffList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("DocStaffBus.GetDocStaffList"));
                return;
            }
            else
                cboBSSieuAm.DataSource = result.QueryResult;

            if (Global.StaffType == StaffType.BacSi || Global.StaffType == StaffType.BacSiSieuAm ||
                Global.StaffType == StaffType.BacSiNgoaiTongQuat || Global.StaffType == StaffType.BacSiNoiTongQuat ||
                Global.StaffType == StaffType.BacSiPhuKhoa)
            {
                cboBSSieuAm.SelectedValue = Global.UserGUID;
                cboBSSieuAm.Enabled = false;
            }
        }

        private bool CheckInfo()
        {
            if (cboLoaiSieuAm.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn loại siêu âm.", IconType.Information);
                cboLoaiSieuAm.Focus();
                return false;
            }

            if (cboBSSieuAm.SelectedValue == null || cboBSSieuAm.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn bác sĩ siêu âm.", IconType.Information);
                cboBSSieuAm.Focus();
                return false;
            }

            if (picHinh1.Image == null && picHinh2.Image == null)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn ít nhất 1 hình siêu âm.", IconType.Information);
                picHinh1.Focus();
                return false;
            }

            return true;
        }

        private void DisplayMauBaoCao()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (cboLoaiSieuAm.Text.Trim() == string.Empty)
            {
                _textControl.ResetContents();
                return;
            }

            if (_loaiSieuAmGUID != string.Empty && _htMauBaoCao.ContainsKey(_loaiSieuAmGUID))
            {
                byte[] buff = null;
                _textControl.Save(out buff, TXTextControl.BinaryStreamType.MSWord);
                _htMauBaoCao[_loaiSieuAmGUID] = buff;
            }

            
            bool isNam = _gioiTinh.ToLower() == "nam" ? true : false;
            _loaiSieuAmGUID = cboLoaiSieuAm.SelectedValue.ToString();
            Result result = SieuAmBus.GetMauBaoCao(_loaiSieuAmGUID, isNam);
            if (!result.IsOK)
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("SieuAmBus.GetMauBaoCao"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("SieuAmBus.GetMauBaoCao"));
            }
            else
            {
                MauBaoCao mauBaoCao = result.QueryResult as MauBaoCao;
                if (mauBaoCao == null) _textControl.ResetContents();
                else
                {
                    byte[] buff = null;
                    if (!_htMauBaoCao.ContainsKey(_loaiSieuAmGUID))
                    {
                        buff = mauBaoCao.Template.ToArray();
                        _htMauBaoCao.Add(_loaiSieuAmGUID, buff);
                    }
                    else
                        buff = (byte[])_htMauBaoCao[_loaiSieuAmGUID];

                    _textControl.Load(buff, TXTextControl.BinaryStreamType.MSWord);
                }
            }
        }

        private void DisplayInfo()
        {
            try
            {
                _ketQuaSieuAm.KetQuaSieuAmGUID = Guid.Parse(_drKetQuaSieuAm["KetQuaSieuAmGUID"].ToString());
                dtpkNgaySieuAm.Value = Convert.ToDateTime(_drKetQuaSieuAm["NgaySieuAm"]);
                
                cboBSSieuAm.SelectedValue = _drKetQuaSieuAm["BacSiSieuAmGUID"].ToString();
                if (_drKetQuaSieuAm["BacSiChiDinhGUID"] != null && _drKetQuaSieuAm["BacSiChiDinhGUID"] != DBNull.Value)
                    cboBSCD.SelectedValue = _drKetQuaSieuAm["BacSiChiDinhGUID"].ToString();

                cboLoaiSieuAm.SelectedValue = Guid.Parse(_drKetQuaSieuAm["LoaiSieuAmGUID"].ToString());
                
                txtLamSang.Text = _drKetQuaSieuAm["LamSang"].ToString();

                byte[] buff = (byte[])_drKetQuaSieuAm["KetQuaSieuAm"];
                _textControl.Load(buff, TXTextControl.BinaryStreamType.MSWord);

                if (_drKetQuaSieuAm["Hinh1"] != null && _drKetQuaSieuAm["Hinh1"] != DBNull.Value)
                    picHinh1.Image = Utility.ParseImage((byte[])_drKetQuaSieuAm["Hinh1"]);

                if (_drKetQuaSieuAm["Hinh2"] != null && _drKetQuaSieuAm["Hinh2"] != DBNull.Value)
                    picHinh2.Image = Utility.ParseImage((byte[])_drKetQuaSieuAm["Hinh2"]);

                if (_drKetQuaSieuAm["CreatedDate"] != null && _drKetQuaSieuAm["CreatedDate"] != DBNull.Value)
                    _ketQuaSieuAm.CreatedDate = Convert.ToDateTime(_drKetQuaSieuAm["CreatedDate"]);

                if (_drKetQuaSieuAm["CreatedBy"] != null && _drKetQuaSieuAm["CreatedBy"] != DBNull.Value)
                    _ketQuaSieuAm.CreatedBy = Guid.Parse(_drKetQuaSieuAm["CreatedBy"].ToString());

                if (_drKetQuaSieuAm["UpdatedDate"] != null && _drKetQuaSieuAm["UpdatedDate"] != DBNull.Value)
                    _ketQuaSieuAm.UpdatedDate = Convert.ToDateTime(_drKetQuaSieuAm["UpdatedDate"]);

                if (_drKetQuaSieuAm["UpdatedBy"] != null && _drKetQuaSieuAm["UpdatedBy"] != DBNull.Value)
                    _ketQuaSieuAm.UpdatedBy = Guid.Parse(_drKetQuaSieuAm["UpdatedBy"].ToString());

                if (_drKetQuaSieuAm["DeletedDate"] != null && _drKetQuaSieuAm["DeletedDate"] != DBNull.Value)
                    _ketQuaSieuAm.DeletedDate = Convert.ToDateTime(_drKetQuaSieuAm["DeletedDate"]);

                if (_drKetQuaSieuAm["DeletedBy"] != null && _drKetQuaSieuAm["DeletedBy"] != DBNull.Value)
                    _ketQuaSieuAm.DeletedBy = Guid.Parse(_drKetQuaSieuAm["DeletedBy"].ToString());

                _ketQuaSieuAm.Status = Convert.ToByte(_drKetQuaSieuAm["Status"]);

                if (!_allowEdit)
                {
                    btnOK.Enabled = _allowEdit;
                    btnSaveAndPrint.Enabled = _allowEdit;
                    panel3.Enabled = _allowEdit;
                    panel4.Enabled = _allowEdit;
                    _textControl.EditMode = TXTextControl.EditMode.ReadAndSelect;
                }
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
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
                _ketQuaSieuAm.PatientGUID = Guid.Parse(_patientGUID);
                _ketQuaSieuAm.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _ketQuaSieuAm.CreatedDate = DateTime.Now;
                    _ketQuaSieuAm.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _ketQuaSieuAm.UpdatedDate = DateTime.Now;
                    _ketQuaSieuAm.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                MethodInvoker method = delegate
                {
                    _ketQuaSieuAm.NgaySieuAm = dtpkNgaySieuAm.Value;
                    _ketQuaSieuAm.BacSiSieuAmGUID = Guid.Parse(cboBSSieuAm.SelectedValue.ToString());
                    _ketQuaSieuAm.LoaiSieuAmGUID = Guid.Parse(cboLoaiSieuAm.SelectedValue.ToString());

                    if (cboBSCD.SelectedValue != null && cboBSCD.Text.Trim() != string.Empty)
                        _ketQuaSieuAm.BacSiChiDinhGUID = Guid.Parse(cboBSCD.SelectedValue.ToString());

                    _ketQuaSieuAm.LamSang = txtLamSang.Text;

                    byte[] buff = null;
                    _textControl.Save(out buff, TXTextControl.BinaryStreamType.MSWord);
                    _ketQuaSieuAm.KetQuaSieuAm1 = new System.Data.Linq.Binary(buff);

                    _ketQuaSieuAm.Hinh1 = null;
                    _ketQuaSieuAm.Hinh2 = null;

                    if (picHinh1.Image != null)
                        _ketQuaSieuAm.Hinh1 = new System.Data.Linq.Binary(Utility.GetBinaryFromImage(picHinh1.Image));

                    if (picHinh2.Image != null)
                        _ketQuaSieuAm.Hinh2 = new System.Data.Linq.Binary(Utility.GetBinaryFromImage(picHinh2.Image));

                    Result result = SieuAmBus.InsertKetQuaSieuAm(_ketQuaSieuAm);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("SieuAmBus.InsertKetQuaSieuAm"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("SieuAmBus.InsertKetQuaSieuAm"));
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

        private void ChonHinhTuBenNgoai(PictureBox picBox)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.bmp; *gif) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.bmp; *gif";
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(dlg.FileName);
                picBox.Image = bmp;
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddKetQuaSieuAm_Load(object sender, EventArgs e)
        {
            if (!this.IsHandleCreated)
                this.CreateHandle();

            InitData();
            if (!_isNew) DisplayInfo();
        }

        private void dlgAddKetQuaSieuAm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                {
                    SaveInfoAsThread();

                    if (!Global.TVHomeConfig.SuDungSieuAm)
                        PlayCapFactory.KillPlayCapProcess();
                    else
                        _watchingFolder.StopMoritoring();
                }
                else
                    e.Cancel = true;
            }
            else
            {
                if (!Global.TVHomeConfig.SuDungSieuAm)
                    PlayCapFactory.KillPlayCapProcess();
                else
                    _watchingFolder.StopMoritoring();
            }
        }

        private void dlgAddKetQuaSieuAm_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopWatchingFolder();
        }

        private void btnHinh1_Click(object sender, EventArgs e)
        {
            _hinh = 1;
            PlayCapFactory.Capture();
            btnHinh1.Enabled = false;
        }

        private void btnHinh2_Click(object sender, EventArgs e)
        {
            _hinh = 2;
            PlayCapFactory.Capture();
            btnHinh2.Enabled = false;
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            Image img = picHinh1.Image;
            picHinh1.Image = picHinh2.Image;
            picHinh2.Image = img;
        }

        private void xóaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (picHinh1.Image == null) return;
            picHinh1.Image = null;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (picHinh2.Image == null) return;
            picHinh2.Image = null;
        }

        private void cboLoaiSieuAm_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayMauBaoCao();
        }

        private void btnSaveAndPrint_Click(object sender, EventArgs e)
        {
            _isPrint = true;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void PlayCapFactory_OnCaptureCompletedEvent(Image img)
        {
            try
            {
                if (_hinh == 1) btnHinh1.Enabled = true;
                else btnHinh2.Enabled = true;

                if (_hinh == 1) picHinh1.Image = img;
                else picHinh2.Image = img;
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "Could not grab picture\r\n" + ee.Message, "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void _watchingFolder_OnCreatedFileEvent(FileSystemEventArgs e)
        {
            try
            {
                string ext = Path.GetExtension(e.FullPath);
                if (!Utility.CheckTVHomeFormat(ext, Global.TVHomeConfig.Format))
                    return;

                int count = 0;
                Image bmp = null;
                bool isRenameOK = false;
                string fileName = string.Format("{0}\\SieuAm-{1}-{2}{3}", Global.HinhChupPath, _maBenhNhan, 
                    DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-ms"), ext);
                while (File.Exists(fileName))
                {
                    fileName = string.Format("{0}\\SieuAm-{1}-{2}{3}", Global.HinhChupPath, _maBenhNhan, 
                        DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-ms"), ext);
                    Thread.Sleep(5);
                }

                while ((bmp == null || !isRenameOK) && count <= Const.DeplayCount)
                {
                    try
                    {
                        if (bmp == null)
                            bmp = Utility.LoadImageFromFile(e.FullPath);
                    }
                    catch (Exception ex)
                    {
                        bmp = null;
                    }

                    if (bmp != null)
                    {
                        try
                        {
                            isRenameOK = Utility.RenameFileName(e.FullPath, fileName);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    
                    count++;
                    Thread.Sleep(10);
                }

                if (bmp == null) return;

                this.Invoke(new MethodInvoker(delegate()
                {
                    if (_hinh == 1)
                    {
                        picHinh1.Image = bmp;
                        _hinh = 2;
                    }
                    else if (_hinh == 2)
                    {
                        picHinh2.Image = bmp;
                        _hinh = 1;
                    }
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chọnHìnhTừBênNgoàiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChonHinhTuBenNgoai(picHinh1);
        }

        private void chọnHìnhTừBênNgoàiToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ChonHinhTuBenNgoai(picHinh2);
        }

        private void picHinh1_DoubleClick(object sender, EventArgs e)
        {
            ChonHinhTuBenNgoai(picHinh1);
        }

        private void picHinh2_DoubleClick(object sender, EventArgs e)
        {
            ChonHinhTuBenNgoai(picHinh2);
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
