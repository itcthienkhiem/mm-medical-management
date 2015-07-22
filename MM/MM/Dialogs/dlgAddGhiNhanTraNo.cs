using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Databasae;
using System.Threading;
using MM.Common;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgAddGhiNhanTraNo : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private GhiNhanTraNo _ghiNhanTraNo = new GhiNhanTraNo();
        private DataRow _drGhiNhanTraNo = null;
        private string _phieuThuGUID = string.Empty;
        private LoaiPT _loaiPT = LoaiPT.DichVu;
        private bool _isDaTraDu = false;
        #endregion

        #region Constructor
        public dlgAddGhiNhanTraNo(string phieuThuGUID, LoaiPT loaiPT)
        {
            InitializeComponent();
            _phieuThuGUID = phieuThuGUID;
            _loaiPT = loaiPT;
            dtpkNgayTra.Value = DateTime.Now;
        }

        public dlgAddGhiNhanTraNo(DataRow drGhiNhanTraNo, string phieuThuGUID, LoaiPT loaiPT) : 
            this(phieuThuGUID, loaiPT)
        {
            _drGhiNhanTraNo = drGhiNhanTraNo;
            _isNew = false;
            this.Text = "Sua ghi nhan tra no";
        }
        #endregion

        #region Properties
        public bool IsDaTraDu
        {
            get { return _isDaTraDu; }
        }
        #endregion

        #region UI Command
        private void DisplayInfo()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                dtpkNgayTra.Value = Convert.ToDateTime(_drGhiNhanTraNo["NgayTra"]);
                numSoTien.Value = (Decimal)Convert.ToDouble(_drGhiNhanTraNo["SoTien"]);
                txtGhiChu.Text = _drGhiNhanTraNo["GhiChu"] as string;
                _ghiNhanTraNo.GhiNhanTraNoGUID = Guid.Parse(_drGhiNhanTraNo["GhiNhanTraNoGUID"].ToString());

                if (_drGhiNhanTraNo["CreatedDate"] != null && _drGhiNhanTraNo["CreatedDate"] != DBNull.Value)
                    _ghiNhanTraNo.CreatedDate = Convert.ToDateTime(_drGhiNhanTraNo["CreatedDate"]);

                if (_drGhiNhanTraNo["CreatedBy"] != null && _drGhiNhanTraNo["CreatedBy"] != DBNull.Value)
                    _ghiNhanTraNo.CreatedBy = Guid.Parse(_drGhiNhanTraNo["CreatedBy"].ToString());

                if (_drGhiNhanTraNo["UpdatedDate"] != null && _drGhiNhanTraNo["UpdatedDate"] != DBNull.Value)
                    _ghiNhanTraNo.UpdatedDate = Convert.ToDateTime(_drGhiNhanTraNo["UpdatedDate"]);

                if (_drGhiNhanTraNo["UpdatedBy"] != null && _drGhiNhanTraNo["UpdatedBy"] != DBNull.Value)
                    _ghiNhanTraNo.UpdatedBy = Guid.Parse(_drGhiNhanTraNo["UpdatedBy"].ToString());

                if (_drGhiNhanTraNo["DeletedDate"] != null && _drGhiNhanTraNo["DeletedDate"] != DBNull.Value)
                    _ghiNhanTraNo.DeletedDate = Convert.ToDateTime(_drGhiNhanTraNo["DeletedDate"]);

                if (_drGhiNhanTraNo["DeletedBy"] != null && _drGhiNhanTraNo["DeletedBy"] != DBNull.Value)
                    _ghiNhanTraNo.DeletedBy = Guid.Parse(_drGhiNhanTraNo["DeletedBy"].ToString());

                _ghiNhanTraNo.Status = Convert.ToByte(_drGhiNhanTraNo["Status"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (numSoTien.Value <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập số tiền.", IconType.Information);
                numSoTien.Focus();
                return false;
            }

            double tienNo = GetSoTienConNo();
            double soTien = (double)numSoTien.Value;
            if (tienNo < soTien)
            {
                MsgBox.Show(this.Text, "Số tiền trả nhiều hơn số tiền còn nợ. Vui lòng kiểm tra lại.", IconType.Information);
                return false;
            }

            _isDaTraDu = (tienNo == soTien);


            return true;
        }

        private double GetSoTienConNo()
        {
            //Get tổng tiền đã trả
            double tongTienTra = 0;
            Result result = GhiNhanTraNoBus.GetTongTienTraNo(_ghiNhanTraNo.GhiNhanTraNoGUID.ToString(), _phieuThuGUID, _loaiPT);
            if (result.IsOK)
                tongTienTra = Convert.ToDouble(result.QueryResult);
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("GhiNhanTraNoBus.GetTongTienTraNo"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("GhiNhanTraNoBus.GetTongTienTraNo"));
            }

            //Get tổng tiền còn nợ
            double tongTienNo = 0;
            result = GhiNhanTraNoBus.GetTongTienPhieuThu(_phieuThuGUID, _loaiPT);
            if (result.IsOK)
                tongTienNo = Convert.ToDouble(result.QueryResult);
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("GhiNhanTraNoBus.GetTongTienPhieuThu"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("GhiNhanTraNoBus.GetTongTienPhieuThu"));
            }

            tongTienNo = tongTienNo - tongTienTra;

            return tongTienNo;
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
                MethodInvoker method = delegate
                {
                    _ghiNhanTraNo.MaPhieuThuGUID = Guid.Parse(_phieuThuGUID);
                    _ghiNhanTraNo.LoaiPT = (int)_loaiPT;
                    _ghiNhanTraNo.NgayTra = dtpkNgayTra.Value;
                    _ghiNhanTraNo.SoTien = (double)numSoTien.Value;
                    _ghiNhanTraNo.GhiChu = txtGhiChu.Text;
                    _ghiNhanTraNo.Status = (byte)Status.Actived;

                    if (_isNew)
                    {
                        _ghiNhanTraNo.CreatedDate = DateTime.Now;
                        _ghiNhanTraNo.CreatedBy = Guid.Parse(Global.UserGUID);
                    }
                    else
                    {
                        _ghiNhanTraNo.UpdatedDate = DateTime.Now;
                        _ghiNhanTraNo.UpdatedBy = Guid.Parse(Global.UserGUID);
                    }

                    Result result = GhiNhanTraNoBus.InsertGhiNhanTraNo(_ghiNhanTraNo, _isDaTraDu, _phieuThuGUID, _loaiPT);

                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("GhiNhanTraNoBus.InsertGhiNhanTraNo"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("GhiNhanTraNoBus.InsertGhiNhanTraNo"));
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
        private void dlgAddGhiNhanTraNo_Load(object sender, EventArgs e)
        {
            if (!_isNew) DisplayInfo();
        }

        private void dlgAddGhiNhanTraNo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
                else SaveInfoAsThread();
            }
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
