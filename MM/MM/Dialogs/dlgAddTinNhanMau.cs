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
    public partial class dlgAddTinNhanMau : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private TinNhanMau _tinNhanMau = new TinNhanMau();
        private DataRow _drTinNhanMau = null;
        private bool _allowDuyet = false;
        #endregion

        #region Constructor
        public dlgAddTinNhanMau(bool allowDuyet)
        {
            InitializeComponent();
            _allowDuyet = allowDuyet;
            chkDuyet.Enabled = _allowDuyet;
        }

        public dlgAddTinNhanMau(DataRow drTinNhanMau, bool allowDuyet)
        {
            InitializeComponent();
            _isNew = false;
            _allowDuyet = allowDuyet;
            chkDuyet.Enabled = _allowDuyet;
            _drTinNhanMau = drTinNhanMau;
            this.Text = "Sua tin nhan mau";
        }
        #endregion

        #region Properties
        public TinNhanMau TinNhanMau
        {
            get { return _tinNhanMau; }
            set { _tinNhanMau = value; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            List<string> bookMarks = new List<string>();
            bookMarks.Add("#MaBenhNhan#");
            bookMarks.Add("#TenBenhNhan#");
            bookMarks.Add("#NgaySinh#");
            bookMarks.Add("#GioiTinh#");
            bookMarks.Add("#DiaChi#");
            bookMarks.Add("#CMND#");
            bookMarks.Add("#Mobile#");
            bookMarks.Add("#Email#");

            foreach (string bookMark in bookMarks)
            {
                ListViewItem item = new ListViewItem(bookMark);
                lvBookMarks.Items.Add(item);
            }

            txtNoiDung.DragDrop += new DragEventHandler(txtNoiDung_DragDrop);
            txtNoiDung.DragEnter += new DragEventHandler(txtNoiDung_DragEnter);
        }

        private void DisplayInfo()
        {
            try
            {
                txtTieuDe.Text = _drTinNhanMau["TieuDe"] as string;

                txtNoiDung.Text = _drTinNhanMau["NoiDung"] as string;

                chkDuyet.Checked = Convert.ToBoolean(_drTinNhanMau["IsDuyet"]);

                _tinNhanMau.TinNhanMauGUID = Guid.Parse(_drTinNhanMau["TinNhanMauGUID"].ToString());

                if (_drTinNhanMau["CreatedDate"] != null && _drTinNhanMau["CreatedDate"] != DBNull.Value)
                    _tinNhanMau.CreatedDate = Convert.ToDateTime(_drTinNhanMau["CreatedDate"]);

                if (_drTinNhanMau["CreatedBy"] != null && _drTinNhanMau["CreatedBy"] != DBNull.Value)
                    _tinNhanMau.CreatedBy = Guid.Parse(_drTinNhanMau["CreatedBy"].ToString());

                if (_drTinNhanMau["UpdatedDate"] != null && _drTinNhanMau["UpdatedDate"] != DBNull.Value)
                    _tinNhanMau.UpdatedDate = Convert.ToDateTime(_drTinNhanMau["UpdatedDate"]);

                if (_drTinNhanMau["UpdatedBy"] != null && _drTinNhanMau["UpdatedBy"] != DBNull.Value)
                    _tinNhanMau.UpdatedBy = Guid.Parse(_drTinNhanMau["UpdatedBy"].ToString());

                if (_drTinNhanMau["DeletedDate"] != null && _drTinNhanMau["DeletedDate"] != DBNull.Value)
                    _tinNhanMau.DeletedDate = Convert.ToDateTime(_drTinNhanMau["DeletedDate"]);

                if (_drTinNhanMau["DeletedBy"] != null && _drTinNhanMau["DeletedBy"] != DBNull.Value)
                    _tinNhanMau.DeletedBy = Guid.Parse(_drTinNhanMau["DeletedBy"].ToString());

                _tinNhanMau.Status = Convert.ToByte(_drTinNhanMau["Status"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (txtTieuDe.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tiêu đề tin nhắn.", IconType.Information);
                txtTieuDe.Focus();
                return false;
            }

            if (txtNoiDung.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập nội dung tin nhắn.", IconType.Information);
                txtNoiDung.Focus();
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
                this.Invoke(new MethodInvoker(delegate()
                {
                    _tinNhanMau.TieuDe = txtTieuDe.Text;
                    _tinNhanMau.NoiDung = txtNoiDung.Text;
                    _tinNhanMau.IsDuyet = chkDuyet.Checked;
                }));

                _tinNhanMau.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _tinNhanMau.CreatedDate = DateTime.Now;
                    _tinNhanMau.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _tinNhanMau.UpdatedDate = DateTime.Now;
                    _tinNhanMau.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                Result result = TinNhanMauBus.InsertTinNhanMau(_tinNhanMau);
                if (!result.IsOK)
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("TinNhanMauBus.InsertTinNhanMau"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("TinNhanMauBus.InsertTinNhanMau"));
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddTinNhanMau_Load(object sender, EventArgs e)
        {
            InitData();
            if (!_isNew) DisplayInfo();
        }

        private void lvBookMarks_DoubleClick(object sender, EventArgs e)
        {
            if (lvBookMarks.SelectedItems == null || lvBookMarks.SelectedItems.Count <= 0) return;
            ListViewItem item = lvBookMarks.SelectedItems[0];
            string bookMark = item.Text;
            int index = txtNoiDung.SelectionStart;
            string noiDung = txtNoiDung.Text;
            noiDung = noiDung.Insert(index, bookMark);
            txtNoiDung.Text = noiDung;
            txtNoiDung.SelectionStart = index + bookMark.Length;
            txtNoiDung.Focus();
        }

        private void dlgAddTinNhanMau_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
        }

        private void lvBookMarks_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item == null) return;

            ListViewItem item = e.Item as ListViewItem;
            string bookMark = item.Text;

            lvBookMarks.DoDragDrop(bookMark, DragDropEffects.Move);
        }

        private void txtNoiDung_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void txtNoiDung_DragDrop(object sender, DragEventArgs e)
        {

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
