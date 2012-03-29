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
    public partial class dlgAddKetQuaSoiCTC : dlgBase, ISampleGrabberCB
    {
        #region Members
        /// <summary> flag to detect first Form appearance </summary>
        private bool firstActive;

        /// <summary> base filter of the actually used video devices. </summary>
        private IBaseFilter capFilter;

        /// <summary> graph builder interface. </summary>
        private IGraphBuilder graphBuilder;

        /// <summary> capture graph builder interface. </summary>
        private ICaptureGraphBuilder2 capGraph;
        private ISampleGrabber sampGrabber;

        /// <summary> control interface. </summary>
        private IMediaControl mediaCtrl;

        /// <summary> event interface. </summary>
        private IMediaEventEx mediaEvt;

        /// <summary> video window interface. </summary>
        private IVideoWindow videoWin;

        /// <summary> grabber filter interface. </summary>
        private IBaseFilter baseGrabFlt;

        /// <summary> structure describing the bitmap to grab. </summary>
        private VideoInfoHeader videoInfoHeader;
        private bool captured = true;
        private int bufferedSize;

        /// <summary> buffer for bitmap data. </summary>
        private byte[] savedArray;

        /// <summary> list of installed video devices. </summary>
        private ArrayList capDevices;

        private const int WM_GRAPHNOTIFY = 0x00008001;	// message from graph

        private const int WS_CHILD = 0x40000000;	// attributes for video window
        private const int WS_CLIPCHILDREN = 0x02000000;
        private const int WS_CLIPSIBLINGS = 0x04000000;

        /// <summary> event when callback has finished (ISampleGrabberCB.BufferCB). </summary>
        private delegate void CaptureDone();

#if DEBUG
        private int rotCookie = 0;
#endif

        private int _imgCount = 0;
        private bool _isNew = true;
        private string _patientGUID = string.Empty;
        private KetQuaSoiCTC _ketQuaSoiCTC = new KetQuaSoiCTC();
        private DataRow _drKetQuaSoiCTC = null;
        private bool _isPrint = false;
        private bool _allowEdit = true;
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
            StartTVCapture();
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
            if (cboBSSoi.Text == string.Empty)
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

        private Image ParseImage(byte[] buffer)
        {
            Bitmap bmp = null;
            MemoryStream ms = null;

            try
            {
                ms = new MemoryStream(buffer);
                bmp = new Bitmap(ms);
                return bmp;
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                    ms = null;
                }
            }

            return bmp;
        }

        private byte[] GetBinaryFromImage(Image img)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.GetBuffer();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                if (ms != null)
                {
                    ms.Close();
                    ms = null;
                }
            }

            return null;
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
                    picHinh1.Image = ParseImage((byte[])drKetQuaSoiCTC["Hinh1"]);

                if (drKetQuaSoiCTC["Hinh2"] != null && drKetQuaSoiCTC["Hinh2"] != DBNull.Value)
                    picHinh2.Image = ParseImage((byte[])drKetQuaSoiCTC["Hinh2"]);

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
                        _ketQuaSoiCTC.Hinh1 = new System.Data.Linq.Binary(GetBinaryFromImage(picHinh1.Image));

                    if (picHinh2.Image != null)
                        _ketQuaSoiCTC.Hinh2 = new System.Data.Linq.Binary(GetBinaryFromImage(picHinh2.Image));

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
                    CloseInterfaces();
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
                        CloseInterfaces();
                    }
                    else
                        e.Cancel = true;
                }
                else
                    CloseInterfaces();
            }
        }

        private void btnTVTune_Click(object sender, EventArgs e)
        {
            if (sampGrabber == null) return;

            if (capGraph != null) DsUtils.ShowTunerPinDialog(capGraph, capFilter, this.Handle);
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (sampGrabber == null) return;

            if (savedArray == null)
            {
                int size = videoInfoHeader.BmiHeader.ImageSize;
                if ((size < 1000) || (size > 16000000))
                    return;
                savedArray = new byte[size + 64000];
            }

            btnCapture.Enabled = false;
            captured = false;
            int hr = sampGrabber.SetCallback(this, 1);
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
        #endregion

        #region TV Capture
        private void StartTVCapture()
        {
            if (!DsUtils.IsCorrectDirectXVersion())
            {
                MessageBox.Show(this, "DirectX 8.1 NOT installed!", "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close(); return;
            }

            if (!DsDev.GetDevicesOfCat(FilterCategory.VideoInputDevice, out capDevices))
            {
                MessageBox.Show(this, "No video capture devices found!", "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close(); return;
            }

            DsDevice dev = null;
            if (capDevices.Count == 1)
                dev = capDevices[0] as DsDevice;
            else
            {
                DeviceSelector selector = new DeviceSelector(capDevices);
                selector.ShowDialog(this);
                dev = selector.SelectedDevice;
            }

            if (dev == null)
            {
                this.Close(); return;
            }

            if (!StartupVideo(dev.Mon))
                this.Close();
        }

        /// <summary> capture event, triggered by buffer callback. </summary>
        private void OnCaptureDone()
        {
            try
            {
                btnCapture.Enabled = true;
                int hr;
                if (sampGrabber == null)
                    return;
                hr = sampGrabber.SetCallback(null, 0);

                int w = videoInfoHeader.BmiHeader.Width;
                int h = videoInfoHeader.BmiHeader.Height;
                if (((w & 0x03) != 0) || (w < 32) || (w > 4096) || (h < 32) || (h > 4096))
                    return;
                int stride = w * 3;

                GCHandle handle = GCHandle.Alloc(savedArray, GCHandleType.Pinned);
                int scan0 = (int)handle.AddrOfPinnedObject();
                scan0 += (h - 1) * stride;
                Bitmap b = new Bitmap(w, h, -stride, PixelFormat.Format24bppRgb, (IntPtr)scan0);
                handle.Free();
                savedArray = null;

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
            catch (Exception ee)
            {
                MessageBox.Show(this, "Could not grab picture\r\n" + ee.Message, "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        /// <summary> start all the interfaces, graphs and preview window. </summary>
        private bool StartupVideo(UCOMIMoniker mon)
        {
            int hr;
            try
            {
                if (!CreateCaptureDevice(mon))
                    return false;

                if (!GetInterfaces())
                    return false;

                if (!SetupGraph())
                    return false;

                if (!SetupVideoWindow())
                    return false;

#if DEBUG
                DsROT.AddGraphToRot(graphBuilder, out rotCookie);		// graphBuilder capGraph
#endif

                hr = mediaCtrl.Run();
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                bool hasTuner = DsUtils.ShowTunerPinDialog(capGraph, capFilter, this.Handle);
                btnTVTune.Enabled = hasTuner;

                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "Could not start video stream\r\n" + ee.Message, "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
        }

        /// <summary> make the video preview window to show in videoPanel. </summary>
        private bool SetupVideoWindow()
        {
            int hr;
            try
            {
                // Set the video window to be a child of the main window
                hr = videoWin.put_Owner(videoPanel.Handle);
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                // Set video window style
                hr = videoWin.put_WindowStyle(WS_CHILD | WS_CLIPCHILDREN);
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                // Use helper function to position video window in client rect of owner window
                ResizeVideoWindow();

                // Make the video window visible, now that it is properly positioned
                hr = videoWin.put_Visible(DsHlp.OATRUE);
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                hr = mediaEvt.SetNotifyWindow(this.Handle, WM_GRAPHNOTIFY, IntPtr.Zero);
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);
                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "Could not setup video window\r\n" + ee.Message, "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
        }


        /// <summary> build the capture graph for grabber. </summary>
        private bool SetupGraph()
        {
            int hr;
            try
            {
                hr = capGraph.SetFiltergraph(graphBuilder);
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                hr = graphBuilder.AddFilter(capFilter, "Ds.NET Video Capture Device");
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                //DsUtils.ShowCapPinDialog( capGraph, capFilter, this.Handle );

                AMMediaType media = new AMMediaType();
                media.majorType = MediaType.Video;
                media.subType = MediaSubType.RGB24;
                media.formatType = FormatType.VideoInfo;		// ???
                hr = sampGrabber.SetMediaType(media);
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                hr = graphBuilder.AddFilter(baseGrabFlt, "Ds.NET Grabber");
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                Guid cat = PinCategory.Preview;
                Guid med = MediaType.Video;
                hr = capGraph.RenderStream(ref cat, ref med, capFilter, null, null); // baseGrabFlt 
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                cat = PinCategory.Capture;
                med = MediaType.Video;
                hr = capGraph.RenderStream(ref cat, ref med, capFilter, null, baseGrabFlt); // baseGrabFlt 
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                media = new AMMediaType();
                hr = sampGrabber.GetConnectedMediaType(media);
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);
                if ((media.formatType != FormatType.VideoInfo) || (media.formatPtr == IntPtr.Zero))
                    throw new NotSupportedException("Unknown Grabber Media Format");

                videoInfoHeader = (VideoInfoHeader)Marshal.PtrToStructure(media.formatPtr, typeof(VideoInfoHeader));
                Marshal.FreeCoTaskMem(media.formatPtr); media.formatPtr = IntPtr.Zero;

                hr = sampGrabber.SetBufferSamples(false);
                if (hr == 0)
                    hr = sampGrabber.SetOneShot(false);
                if (hr == 0)
                    hr = sampGrabber.SetCallback(null, 0);
                if (hr < 0)
                    Marshal.ThrowExceptionForHR(hr);

                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "Could not setup graph\r\n" + ee.Message, "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                _allowEdit = false;
                return false;
            }
        }


        /// <summary> create the used COM components and get the interfaces. </summary>
        private bool GetInterfaces()
        {
            Type comType = null;
            object comObj = null;
            try
            {
                comType = Type.GetTypeFromCLSID(Clsid.FilterGraph);
                if (comType == null)
                    throw new NotImplementedException(@"DirectShow FilterGraph not installed/registered!");
                comObj = Activator.CreateInstance(comType);
                graphBuilder = (IGraphBuilder)comObj; comObj = null;

                Guid clsid = Clsid.CaptureGraphBuilder2;
                Guid riid = typeof(ICaptureGraphBuilder2).GUID;
                comObj = DsBugWO.CreateDsInstance(ref clsid, ref riid);
                capGraph = (ICaptureGraphBuilder2)comObj; comObj = null;

                comType = Type.GetTypeFromCLSID(Clsid.SampleGrabber);
                if (comType == null)
                    throw new NotImplementedException(@"DirectShow SampleGrabber not installed/registered!");
                comObj = Activator.CreateInstance(comType);
                sampGrabber = (ISampleGrabber)comObj; comObj = null;

                mediaCtrl = (IMediaControl)graphBuilder;
                videoWin = (IVideoWindow)graphBuilder;
                mediaEvt = (IMediaEventEx)graphBuilder;
                baseGrabFlt = (IBaseFilter)sampGrabber;
                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "Could not get interfaces\r\n" + ee.Message, "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            finally
            {
                if (comObj != null)
                    Marshal.ReleaseComObject(comObj); comObj = null;
            }
        }

        /// <summary> create the user selected capture device. </summary>
        private bool CreateCaptureDevice(UCOMIMoniker mon)
        {
            object capObj = null;
            try
            {
                Guid gbf = typeof(IBaseFilter).GUID;
                mon.BindToObject(null, null, ref gbf, out capObj);
                capFilter = (IBaseFilter)capObj; capObj = null;
                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(this, "Could not create capture device\r\n" + ee.Message, "DirectShow.NET", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            finally
            {
                if (capObj != null)
                    Marshal.ReleaseComObject(capObj); capObj = null;
            }

        }

        /// <summary> do cleanup and release DirectShow. </summary>
        private void CloseInterfaces()
        {
            int hr;
            try
            {
#if DEBUG
                if (rotCookie != 0)
                    DsROT.RemoveGraphFromRot(ref rotCookie);
#endif

                if (mediaCtrl != null)
                {
                    hr = mediaCtrl.Stop();
                    Marshal.ReleaseComObject(mediaCtrl);
                    mediaCtrl = null;
                }

                if (mediaEvt != null)
                {
                   //hr = mediaEvt.SetNotifyWindow(IntPtr.Zero, WM_GRAPHNOTIFY, IntPtr.Zero);
                    Marshal.ReleaseComObject(mediaEvt);
                    mediaEvt = null;
                }

                if (videoWin != null)
                {
                    //hr = videoWin.put_Visible(DsHlp.OAFALSE);
                   //hr = videoWin.put_Owner(IntPtr.Zero);
                    Marshal.ReleaseComObject(videoWin);
                    videoWin = null;
                }

                if (baseGrabFlt != null)
                {
                    baseGrabFlt.Stop();
                    Marshal.ReleaseComObject(baseGrabFlt);
                    baseGrabFlt = null;
                }

                if (sampGrabber != null)
                    Marshal.ReleaseComObject(sampGrabber); sampGrabber = null;

                if (capGraph != null)
                    Marshal.ReleaseComObject(capGraph); capGraph = null;

                if (graphBuilder != null)
                    Marshal.ReleaseComObject(graphBuilder); graphBuilder = null;

                if (capFilter != null)
                    Marshal.ReleaseComObject(capFilter); capFilter = null;

                if (capDevices != null)
                {
                    foreach (DsDevice d in capDevices)
                    {
                        d.Name = string.Empty;
                        Marshal.ReleaseComObject(d.Mon);
                        d.Mon = null;
                        d.Dispose();
                    }

                    capDevices = null;
                }
            }
            catch (Exception)
            { }
        }

        /// <summary> resize preview video window to fill client area. </summary>
        private void ResizeVideoWindow()
        {
            if (videoWin != null)
            {
                Rectangle rc = videoPanel.ClientRectangle;
                videoWin.SetWindowPosition(0, 0, rc.Right, rc.Bottom);
            }
        }

        /// <summary> override window fn to handle graph events. </summary>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_GRAPHNOTIFY)
            {
                if (mediaEvt != null)
                    OnGraphNotify();
                return;
            }
            base.WndProc(ref m);
        }

        /// <summary> graph event (WM_GRAPHNOTIFY) handler. </summary>
        private void OnGraphNotify()
        {
            DsEvCode code;
            int p1, p2, hr = 0;
            do
            {
                hr = mediaEvt.GetEvent(out code, out p1, out p2, 0);
                if (hr < 0)
                    break;
                hr = mediaEvt.FreeEventParams(code, p1, p2);
            }
            while (hr == 0);
        }

        /// <summary> sample callback, NOT USED. </summary>
        int ISampleGrabberCB.SampleCB(double SampleTime, IMediaSample pSample)
        {
            Trace.WriteLine("!!CB: ISampleGrabberCB.SampleCB");
            return 0;
        }

        /// <summary> buffer callback, COULD BE FROM FOREIGN THREAD. </summary>
        int ISampleGrabberCB.BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen)
        {
            if (captured || (savedArray == null))
            {
                Trace.WriteLine("!!CB: ISampleGrabberCB.BufferCB");
                return 0;
            }

            captured = true;
            bufferedSize = BufferLen;
            Trace.WriteLine("!!CB: ISampleGrabberCB.BufferCB  !GRAB! size = " + BufferLen.ToString());
            if ((pBuffer != IntPtr.Zero) && (BufferLen > 1000) && (BufferLen <= savedArray.Length))
                Marshal.Copy(pBuffer, savedArray, 0, BufferLen);
            else
                Trace.WriteLine("    !!!GRAB! failed ");
            this.BeginInvoke(new CaptureDone(this.OnCaptureDone));
            return 0;
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

    internal enum PlayState
    {
        Init, Stopped, Paused, Running
    }
}
