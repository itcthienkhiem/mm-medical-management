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
using MM.Exports;

namespace MM.Dialogs
{
    public partial class dlgAddKetQuaSoiCTC : dlgBase
    {
        #region Members
        private int _imgCount = 0;
        private bool _isNew = true;
        private string _patientGUID = string.Empty;
        private KetQuaSoiCTC _ketQuaSoiCTC = new KetQuaSoiCTC();
        private DataRow _drKetQuaSoiCTC = null;
        private bool _isPrint = false;
        private bool _allowEdit = true;
        private WatchingFolder _watchingFolder = null;
        #endregion

        #region Constructor
        public dlgAddKetQuaSoiCTC(string patientGUID)
        {
            InitializeComponent();
            _patientGUID = patientGUID;
        }

        public dlgAddKetQuaSoiCTC(string patientGUID, DataRow drKetQuaSoiCTC, bool allowEdit)
        {
            InitializeComponent();
            _allowEdit = allowEdit;
            _patientGUID = patientGUID;
            _drKetQuaSoiCTC = drKetQuaSoiCTC;
            _isNew = false;
            this.Text = "Sua kham soi CTC";
        }
        #endregion

        #region Properties
        public bool IsPrint
        {
            get { return _isPrint; }
        }

        public KetQuaSoiCTC KetQuaSoiCTC
        {
            get { return _ketQuaSoiCTC; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            Cursor.Current = Cursors.WaitCursor;
            dtpkNgayKham.Value = DateTime.Now;
            DisplayDSBasSiSoi();

            if (_allowEdit)
            {
                if (!Global.TVHomeConfig.SuDungSoiCTC)
                {
                    PlayCapFactory.RunPlayCapProcess(true);
                    PlayCapFactory.OnCaptureCompletedEvent += new CaptureCompletedHandler(PlayCapFactory_OnCaptureCompletedEvent);
                }
                else
                {
                    _watchingFolder = new WatchingFolder();
                    _watchingFolder.OnCreatedFileEvent += new CreatedFileEventHandler(_watchingFolder_OnCreatedFileEvent);
                    _watchingFolder.StartMoritoring(Global.HinhChupPath);

                    if (!Utility.CheckRunningProcess(Const.TVHomeProcessName))
                        Utility.ExecuteFile(Global.TVHomeConfig.Path);
                }
            }
        }
                
        private void DisplayDSBasSiSoi()
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
                cboBSSoi.DataSource = result.QueryResult;

            if (Global.StaffType == StaffType.BacSi || Global.StaffType == StaffType.BacSiSieuAm ||
                Global.StaffType == StaffType.BacSiNgoaiTongQuat || Global.StaffType == StaffType.BacSiNoiTongQuat ||
                Global.StaffType == StaffType.BacSiPhuKhoa)
            {
                cboBSSoi.SelectedValue = Global.UserGUID;
                cboBSSoi.Enabled = false;
            }
        }

        private bool CheckInfo()
        {
            if (cboBSSoi.SelectedValue == null || cboBSSoi.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập bác sĩ soi.", IconType.Information);
                cboBSSoi.Focus();
                return false;
            }

            if (picHinh1.Image == null)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn hình 1.", IconType.Information);
                return false;
            }

            if (picHinh2.Image == null)
            {
                MsgBox.Show(this.Text, "Vui lòng chọn hình 2.", IconType.Information);
                return false;
            }

            return true;
        }

        private void DisplayInfo(DataRow drKetQuaSoiCTC)
        {
            try
            {
                dtpkNgayKham.Value = Convert.ToDateTime(drKetQuaSoiCTC["NgayKham"]);
                cboBSSoi.SelectedValue = drKetQuaSoiCTC["BacSiSoi"].ToString();
                cboKetLuan.Text = drKetQuaSoiCTC["KetLuan"].ToString();
                cboDeNghi.Text = drKetQuaSoiCTC["DeNghi"].ToString();

                if (drKetQuaSoiCTC["Hinh1"] != null && drKetQuaSoiCTC["Hinh1"] != DBNull.Value)
                    picHinh1.Image = Utility.ParseImage((byte[])drKetQuaSoiCTC["Hinh1"]);

                if (drKetQuaSoiCTC["Hinh2"] != null && drKetQuaSoiCTC["Hinh2"] != DBNull.Value)
                    picHinh2.Image = Utility.ParseImage((byte[])drKetQuaSoiCTC["Hinh2"]);

                _uKetQuaSoiCTC.AmHo = drKetQuaSoiCTC["AmHo"].ToString();
                _uKetQuaSoiCTC.AmDao = drKetQuaSoiCTC["AmDao"].ToString();
                _uKetQuaSoiCTC.CTC = drKetQuaSoiCTC["CTC"].ToString();
                _uKetQuaSoiCTC.BieuMoLat = drKetQuaSoiCTC["BieuMoLat"].ToString();
                _uKetQuaSoiCTC.MoDem = drKetQuaSoiCTC["MoDem"].ToString();
                _uKetQuaSoiCTC.RanhGioiLatTru = drKetQuaSoiCTC["RanhGioiLatTru"].ToString();
                _uKetQuaSoiCTC.SauAcidAcetic = drKetQuaSoiCTC["SauAcidAcetic"].ToString();
                _uKetQuaSoiCTC.SauLugol = drKetQuaSoiCTC["SauLugol"].ToString();

                _ketQuaSoiCTC.KetQuaSoiCTCGUID = Guid.Parse(drKetQuaSoiCTC["KetQuaSoiCTCGUID"].ToString());

                if (drKetQuaSoiCTC["CreatedDate"] != null && drKetQuaSoiCTC["CreatedDate"] != DBNull.Value)
                    _ketQuaSoiCTC.CreatedDate = Convert.ToDateTime(drKetQuaSoiCTC["CreatedDate"]);

                if (drKetQuaSoiCTC["CreatedBy"] != null && drKetQuaSoiCTC["CreatedBy"] != DBNull.Value)
                    _ketQuaSoiCTC.CreatedBy = Guid.Parse(drKetQuaSoiCTC["CreatedBy"].ToString());

                if (drKetQuaSoiCTC["UpdatedDate"] != null && drKetQuaSoiCTC["UpdatedDate"] != DBNull.Value)
                    _ketQuaSoiCTC.UpdatedDate = Convert.ToDateTime(drKetQuaSoiCTC["UpdatedDate"]);

                if (drKetQuaSoiCTC["UpdatedBy"] != null && drKetQuaSoiCTC["UpdatedBy"] != DBNull.Value)
                    _ketQuaSoiCTC.UpdatedBy = Guid.Parse(drKetQuaSoiCTC["UpdatedBy"].ToString());

                if (drKetQuaSoiCTC["DeletedDate"] != null && drKetQuaSoiCTC["DeletedDate"] != DBNull.Value)
                    _ketQuaSoiCTC.DeletedDate = Convert.ToDateTime(drKetQuaSoiCTC["DeletedDate"]);

                if (drKetQuaSoiCTC["DeletedBy"] != null && drKetQuaSoiCTC["DeletedBy"] != DBNull.Value)
                    _ketQuaSoiCTC.DeletedBy = Guid.Parse(drKetQuaSoiCTC["DeletedBy"].ToString());

                _ketQuaSoiCTC.Status = Convert.ToByte(drKetQuaSoiCTC["Status"]);

                if (!_allowEdit)
                {
                    btnOK.Enabled = _allowEdit;
                    btnSaveAndPrint.Enabled = _allowEdit;
                    panel2.Enabled = _allowEdit;
                    panel4.Enabled = _allowEdit;
                    panel5.Enabled = _allowEdit;
                    panel6.Enabled = _allowEdit;
                }
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void ChonHinh()
        {
            if (lvCapture.SelectedItems == null || lvCapture.SelectedItems.Count <= 0) return;

            dlgChonHinh dlg = new dlgChonHinh();
            dlg.EnableHinh3 = false;
            dlg.EnableHinh4 = false;
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Image img = (Image)lvCapture.SelectedItems[0].Tag;

                switch (dlg.ImageIndex)
                {
                    case 1:
                        picHinh1.Image = img;
                        break;
                    case 2:
                        picHinh2.Image = img;
                        break;
                }
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

        private List<Bookmark> GetBoommark()
        {
            List<Bookmark> bookmarkList = new List<Bookmark>();
            Bookmark bookmark = null;

            if (_uKetQuaSoiCTC.AmHo.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaSoiAmHo;
                bookmark.Value = _uKetQuaSoiCTC.AmHo;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaSoiCTC.AmDao.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaSoiAmDao;
                bookmark.Value = _uKetQuaSoiCTC.AmDao;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaSoiCTC.CTC.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaSoiCTC;
                bookmark.Value = _uKetQuaSoiCTC.CTC;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaSoiCTC.BieuMoLat.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaSoiBieuMoLat;
                bookmark.Value = _uKetQuaSoiCTC.BieuMoLat;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaSoiCTC.MoDem.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaSoiMoDem;
                bookmark.Value = _uKetQuaSoiCTC.MoDem;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaSoiCTC.RanhGioiLatTru.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaSoiRanhGioiLatTru;
                bookmark.Value = _uKetQuaSoiCTC.RanhGioiLatTru;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaSoiCTC.SauAcidAcetic.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaSoiSauAcidAcetic;
                bookmark.Value = _uKetQuaSoiCTC.SauAcidAcetic;
                bookmarkList.Add(bookmark);
            }

            if (_uKetQuaSoiCTC.SauLugol.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetQuaSoiSauLugol;
                bookmark.Value = _uKetQuaSoiCTC.SauLugol;
                bookmarkList.Add(bookmark);
            }

            if (cboKetLuan.Text.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetLuanSoiCTCT;
                bookmark.Value = cboKetLuan.Text;
                bookmarkList.Add(bookmark);
            }

            if (cboDeNghi.Text.Trim() != string.Empty)
            {
                bookmark = new Bookmark();
                bookmark.Type = (int)BookMarkType.KetLuanSoiCTCT;
                bookmark.Value = cboDeNghi.Text;
                bookmarkList.Add(bookmark);
            }

            return bookmarkList;
        }

        private void OnSaveInfo()
        {
            try
            {
                _ketQuaSoiCTC.PatientGUID = Guid.Parse(_patientGUID);
                _ketQuaSoiCTC.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _ketQuaSoiCTC.CreatedDate = DateTime.Now;
                    _ketQuaSoiCTC.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _ketQuaSoiCTC.UpdatedDate = DateTime.Now;
                    _ketQuaSoiCTC.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                MethodInvoker method = delegate
                {
                    _ketQuaSoiCTC.NgayKham = dtpkNgayKham.Value;
                    _ketQuaSoiCTC.BacSiSoi = Guid.Parse(cboBSSoi.SelectedValue.ToString());
                    _ketQuaSoiCTC.KetLuan = cboKetLuan.Text;
                    _ketQuaSoiCTC.DeNghi = cboDeNghi.Text;

                    _ketQuaSoiCTC.Hinh1 = null;
                    _ketQuaSoiCTC.Hinh2 = null;

                    if (picHinh1.Image != null)
                        _ketQuaSoiCTC.Hinh1 = new System.Data.Linq.Binary(Utility.GetBinaryFromImage(picHinh1.Image));

                    if (picHinh2.Image != null)
                        _ketQuaSoiCTC.Hinh2 = new System.Data.Linq.Binary(Utility.GetBinaryFromImage(picHinh2.Image));

                    _ketQuaSoiCTC.AmHo = _uKetQuaSoiCTC.AmHo;
                    _ketQuaSoiCTC.AmDao = _uKetQuaSoiCTC.AmDao;
                    _ketQuaSoiCTC.CTC = _uKetQuaSoiCTC.CTC;
                    _ketQuaSoiCTC.BieuMoLat = _uKetQuaSoiCTC.BieuMoLat;
                    _ketQuaSoiCTC.MoDem = _uKetQuaSoiCTC.MoDem;
                    _ketQuaSoiCTC.RanhGioiLatTru = _uKetQuaSoiCTC.RanhGioiLatTru;
                    _ketQuaSoiCTC.SauAcidAcetic = _uKetQuaSoiCTC.SauAcidAcetic;
                    _ketQuaSoiCTC.SauLugol = _uKetQuaSoiCTC.SauLugol;
                   
                    Result result = KetQuaSoiCTCBus.InsertKetQuaSoiCTC(_ketQuaSoiCTC);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("KetQuaSoiCTCBus.InsertKetQuaSoiCTC"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("KetQuaSoiCTCBus.InsertKetQuaSoiCTC"));
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    }
                    else
                    {
                        List<Bookmark> bookmarkList = GetBoommark();
                        if (bookmarkList != null && bookmarkList.Count > 0)
                        {
                            result = BookmarkBus.InsertBookmark(bookmarkList);
                            if (!result.IsOK)
                            {
                                MsgBox.Show(this.Text, result.GetErrorAsString("BookmarkBus.InsertBookmark"), IconType.Error);
                                Utility.WriteToTraceLog(result.GetErrorAsString("BookmarkBus.InsertBookmark"));
                                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            }
                        }
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
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(dlg.FileName);
                picBox.Image = bmp;
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddKetQuaSoiCTC_Load(object sender, EventArgs e)
        {
            InitData();

            if (!_isNew)
                DisplayInfo(_drKetQuaSoiCTC);
            else
                _uKetQuaSoiCTC.SetDefault();
        }

        private void dlgAddKetQuaSoiCTC_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                {
                    SaveInfoAsThread();

                    if (!Global.TVHomeConfig.SuDungSoiCTC)
                        PlayCapFactory.KillPlayCapProcess();
                    else
                        _watchingFolder.StopMoritoring();
                }
                else
                    e.Cancel = true;
            }
            else
            {
                if (_allowEdit && MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin khám CTC ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (CheckInfo())
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        SaveInfoAsThread();

                        if (!Global.TVHomeConfig.SuDungSoiCTC)
                            PlayCapFactory.KillPlayCapProcess();
                        else
                            _watchingFolder.StopMoritoring();
                    }
                    else
                        e.Cancel = true;
                }
                else
                {
                    if (!Global.TVHomeConfig.SuDungSoiCTC)
                        PlayCapFactory.KillPlayCapProcess();
                    else
                        _watchingFolder.StopMoritoring();
                }
            }
        }

        private void PlayCapFactory_OnCaptureCompletedEvent(Image b)
        {
            imgListCapture.Images.Add(b);

            _imgCount++;
            ListViewItem item = new ListViewItem(string.Format("Hình {0}", _imgCount), imgListCapture.Images.Count - 1);
            item.Tag = b;
            lvCapture.Items.Add(item);

            if (lvCapture.Items.Count <= 2)
            {
                switch (lvCapture.Items.Count)
                {
                    case 1:
                        picHinh1.Image = (Image)lvCapture.Items[0].Tag;
                        break;
                    case 2:
                        picHinh2.Image = (Image)lvCapture.Items[1].Tag;
                        break;
                }
            }
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

        private void lvCapture_DoubleClick(object sender, EventArgs e)
        {
            ChonHinh();
        }

        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvCapture.SelectedItems == null || lvCapture.SelectedItems.Count <= 0) return;

            foreach (ListViewItem item in lvCapture.SelectedItems)
            {
                int imgIndex = item.ImageIndex;
                lvCapture.Items.Remove(item);
                imgListCapture.Images.RemoveAt(imgIndex);
            }
        }

        private void xóaTấtCảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvCapture.Items.Clear();
            imgListCapture.Images.Clear();
            _imgCount = 0;
        }

        private void chọnHìnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChonHinh();
        }

        private void btnSaveAndPrint_Click(object sender, EventArgs e)
        {
            _isPrint = true;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void _watchingFolder_OnCreatedFileEvent(FileSystemEventArgs e)
        {
            Thread.Sleep(1000);
            try
            {
                lvCapture.Invoke(new MethodInvoker(delegate()
                {
                    int count = 0;
                    Bitmap bmp = null;
                    while (bmp == null && count <= 10)
                    {
                        try
                        {
                            bmp = new Bitmap(e.FullPath);    
                        }
                        catch (Exception ex)
                        {
                            bmp = null;   
                        }
                        
                        count++;
                    }

                    if (bmp == null) return;

                    imgListCapture.Images.Add(bmp);

                    _imgCount++;
                    ListViewItem item = new ListViewItem(string.Format("Hình {0}", _imgCount), imgListCapture.Images.Count - 1);
                    item.Tag = bmp;
                    lvCapture.Items.Add(item);

                    if (lvCapture.Items.Count <= 2)
                    {
                        switch (lvCapture.Items.Count)
                        {
                            case 1:
                                picHinh1.Image = (Image)lvCapture.Items[0].Tag;
                                break;
                            case 2:
                                picHinh2.Image = (Image)lvCapture.Items[1].Tag;
                                break;
                        }
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
