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
    public partial class dlgAddKetQuaSieuAm : dlgBase, ISampleGrabberCB
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

        private bool _isNew = true;
        private string _patientGUID = string.Empty;
        private KetQuaSieuAm _ketQuaSieuAm = new KetQuaSieuAm();
        private DataRow _drKetQuaSieuAm = null;
        private bool _isContinue = false;
        private WebCam _webCam = null;
        private bool _isPrint = false;
        private bool _allowEdit = true;
        private string _gioiTinh = string.Empty;
        private int _hinh = 1;
        private Hashtable _htMauBaoCao = new Hashtable();
        private string _loaiSieuAmGUID = string.Empty;
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
            Cursor.Current = Cursors.WaitCursor;
            dtpkNgaySieuAm.Value = DateTime.Now;
            DisplayDSBacSiChiDinh();
            DisplayDSBasSiSieuAm();
            DisplayLoaiSieuAm();

            if (_allowEdit) StartTVCapture();
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

            if (cboBSSieuAm.Text.Trim() == string.Empty)
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

                    if (cboBSCD.Text.Trim() != string.Empty)
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
        #endregion

        #region Window Event Handlers
        private void dlgAddKetQuaSieuAm_Load(object sender, EventArgs e)
        {
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
                    CloseInterfaces();
                }
                else
                    e.Cancel = true;
            }
        }

        private void btnHinh1_Click(object sender, EventArgs e)
        {
            _hinh = 1;
            if (sampGrabber == null) return;

            if (savedArray == null)
            {
                int size = videoInfoHeader.BmiHeader.ImageSize;
                if ((size < 1000) || (size > 16000000))
                    return;
                savedArray = new byte[size + 64000];
            }

            btnHinh1.Enabled = false;
            captured = false;
            int hr = sampGrabber.SetCallback(this, 1);
        }

        private void btnHinh2_Click(object sender, EventArgs e)
        {
            _hinh = 2;
            if (sampGrabber == null) return;

            if (savedArray == null)
            {
                int size = videoInfoHeader.BmiHeader.ImageSize;
                if ((size < 1000) || (size > 16000000))
                    return;
                savedArray = new byte[size + 64000];
            }

            btnHinh2.Enabled = false;
            captured = false;
            int hr = sampGrabber.SetCallback(this, 1);
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

        private void btnTVTune_Click(object sender, EventArgs e)
        {
            if (sampGrabber == null) return;

            if (capGraph != null) DsUtils.ShowTunerPinDialog(capGraph, capFilter, this.Handle);
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
                if (_hinh == 1) btnHinh1.Enabled = true;
                else btnHinh2.Enabled = true;

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

                if (_hinh == 1) picHinh1.Image = b;
                else picHinh2.Image = b;
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

        internal enum PlayState
        {
            Init, Stopped, Paused, Running
        }
    }
}
