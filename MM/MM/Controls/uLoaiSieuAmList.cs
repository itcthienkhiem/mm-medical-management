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
    public partial class uLoaiSieuAmList : uBase
    {
        #region Members
        private TXTextControl.TextControl _textControl2 = null;
        private TabPage _page2 = null;
        private bool _isNew = true;
        private DataRow _drLoaiSieuAm = null;
        private bool _flag = false;
        #endregion

        #region Constructor
        public uLoaiSieuAmList()
        {
            InitializeComponent();
            InitData();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void InitData()
        {
            _page2 = new TabPage();
            _page2.Text = "test";
            _page2.UseVisualStyleBackColor = true;
            _textControl2 = new TXTextControl.TextControl();
            _textControl2.EditMode = TXTextControl.EditMode.ReadAndSelect;
            _textControl2.ViewMode = TXTextControl.ViewMode.Normal;
            _textControl2.Text = string.Empty;
            _page2.Controls.Add(_textControl2);
            _textControl2.Dock = DockStyle.Fill;
        }

        private void UpdateGUI()
        {
            btnAdd.Enabled = AllowAdd;
            btnEdit.Enabled = AllowEdit;
            btnDelete.Enabled = AllowDelete;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayLoaiSieuListProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void ClearData()
        {
            DataTable dt = dgLoaiSieuAm.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgLoaiSieuAm.DataSource = null;
            }
        }

        private void OnDisplayLoaiSieuList()
        {
            Result result = SieuAmBus.GetLoaiSieuAmList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    dgLoaiSieuAm.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("SieuAmBus.GetLoaiSieuAmList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("SieuAmBus.GetLoaiSieuAmList"));
            }
        }

        private bool CheckInfo()
        {
            if (txtTenSieuAm.Text.Trim() == string.Empty)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập tên siêu âm.", IconType.Information);
                txtTenSieuAm.Focus();
                return false;
            }

            string loaiSieuAmGUID = _isNew ? null : _drLoaiSieuAm["LoaiSieuAmGUID"].ToString();
            Result result = SieuAmBus.CheckTenSieuAmExist(loaiSieuAmGUID, txtTenSieuAm.Text);
            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Tên siêu âm này đã tồn tại rồi. Vui lòng nhập tên khác.", IconType.Information);
                    txtTenSieuAm.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("SieuAmBus.CheckTenSieuAmExist"), IconType.Error);
                return false;
            }

            return true;
        }

        private void OnAdd()
        {
            _isNew = true;
            if (!CheckInfo()) return;

            LoaiSieuAm loaiSieuAm = new LoaiSieuAm();
            loaiSieuAm.CreatedBy = Guid.Parse(Global.UserGUID);
            loaiSieuAm.CreatedDate = DateTime.Now;
            loaiSieuAm.TenSieuAm = txtTenSieuAm.Text.Trim();
            loaiSieuAm.ThuTu = (int)numThuTu.Value;
            loaiSieuAm.InTrang2 = chkInTrang2.Checked;
            loaiSieuAm.Status = (byte)Status.Actived;

            List<MauBaoCao> mauBaoCaoList = new List<MauBaoCao>();

            byte[] buff = null;
            _textControl1.Save(out buff, TXTextControl.BinaryStreamType.MSWord);
            MauBaoCao mauBaoCao = new MauBaoCao();
            mauBaoCao.Template = new System.Data.Linq.Binary(buff);
            mauBaoCao.DoiTuong = (int)DoiTuong.Chung;
            

            if (raNamNu.Checked)
            {
                if (chkNam.Checked)
                {
                    mauBaoCao.DoiTuong = (int)DoiTuong.Nam;
                    mauBaoCaoList.Add(mauBaoCao);
                }

                if (chkNu.Checked)
                {
                    buff = null;
                    _textControl2.Save(out buff, TXTextControl.BinaryStreamType.MSWord);
                    mauBaoCao = new MauBaoCao();
                    mauBaoCao.Template = new System.Data.Linq.Binary(buff);
                    mauBaoCao.DoiTuong = (int)DoiTuong.Nu;

                    mauBaoCaoList.Add(mauBaoCao);    
                }
            }
            else
                mauBaoCaoList.Add(mauBaoCao);

            Result result = SieuAmBus.InsertLoaiSieuAm(loaiSieuAm, mauBaoCaoList);
            if (result.IsOK)
            {
                _flag = true;
                OnDisplayLoaiSieuList();
                _flag = false;

                SetCurrentLoaiSieuAm(loaiSieuAm.LoaiSieuAmGUID.ToString());
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("SieuAmBus.InsertLoaiSieuAm"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("SieuAmBus.InsertLoaiSieuAm"));
            }
        }

        private void SetCurrentLoaiSieuAm(string loaiSieuAmGUID)
        {
            foreach (DataGridViewRow row in dgLoaiSieuAm.Rows)
            {
                DataRow dr = (row.DataBoundItem as DataRowView).Row;
                if (dr["LoaiSieuAmGUID"] == null || dr["LoaiSieuAmGUID"] == DBNull.Value) continue;

                if (dr["LoaiSieuAmGUID"].ToString().Trim().ToUpper() == loaiSieuAmGUID.Trim().ToUpper())
                {
                    if (row.Index > 0)
                    {
                        dgLoaiSieuAm.CurrentCell = dgLoaiSieuAm[0, row.Index];
                        dgLoaiSieuAm.Rows[row.Index].Selected = true;
                    }
                    break;
                }
            }
        }

        private void OnEdit()
        {
            _isNew = false;
            if (_drLoaiSieuAm == null)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn loại siêu âm cần cập nhật.", IconType.Information);
                return;
            }

            if (!CheckInfo()) return;

            LoaiSieuAm loaiSieuAm = new LoaiSieuAm();
            loaiSieuAm.LoaiSieuAmGUID = Guid.Parse(_drLoaiSieuAm["LoaiSieuAmGUID"].ToString());
            loaiSieuAm.UpdatedBy = Guid.Parse(Global.UserGUID);
            loaiSieuAm.UpdatedDate = DateTime.Now;
            loaiSieuAm.TenSieuAm = txtTenSieuAm.Text.Trim();
            loaiSieuAm.ThuTu = (int)numThuTu.Value;
            loaiSieuAm.InTrang2 = chkInTrang2.Checked;
            loaiSieuAm.Status = (byte)Status.Actived;

            List<MauBaoCao> mauBaoCaoList = new List<MauBaoCao>();

            byte[] buff = null;
            _textControl1.Save(out buff, TXTextControl.BinaryStreamType.MSWord);
            MauBaoCao mauBaoCao = new MauBaoCao();
            mauBaoCao.Template = new System.Data.Linq.Binary(buff);
            mauBaoCao.DoiTuong = (int)DoiTuong.Chung;
            

            if (raNamNu.Checked)
            {
                if (chkNam.Checked)
                {
                    mauBaoCao.DoiTuong = (int)DoiTuong.Nam;
                    mauBaoCaoList.Add(mauBaoCao);
                }

                if (chkNu.Checked)
                {
                    buff = null;
                    _textControl2.Save(out buff, TXTextControl.BinaryStreamType.MSWord);
                    mauBaoCao = new MauBaoCao();
                    mauBaoCao.Template = new System.Data.Linq.Binary(buff);
                    mauBaoCao.DoiTuong = (int)DoiTuong.Nu;

                    mauBaoCaoList.Add(mauBaoCao);
                }
            }
            else
                mauBaoCaoList.Add(mauBaoCao);

            Result result = SieuAmBus.InsertLoaiSieuAm(loaiSieuAm, mauBaoCaoList);
            if (result.IsOK)
            {
                _flag = true;
                OnDisplayLoaiSieuList();
                _flag = false;

                SetCurrentLoaiSieuAm(loaiSieuAm.LoaiSieuAmGUID.ToString());
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("SieuAmBus.InsertLoaiSieuAm"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("SieuAmBus.InsertLoaiSieuAm"));
            }
        }

        private void OnDelete()
        {
            List<string> deletedServiceList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgLoaiSieuAm.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedServiceList.Add(row["LoaiSieuAmGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedRows.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những loại siêu âm mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    Result result = SieuAmBus.DeleteLoaiSieuAm(deletedServiceList);
                    if (result.IsOK)
                    {
                        foreach (DataRow row in deletedRows)
                        {
                            dt.Rows.Remove(row);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("SieuAmBus.DeleteLoaiSieuAm"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("SieuAmBus.DeleteLoaiSieuAm"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những loại siêu âm cần xóa.", IconType.Information);
        }

        private void OnDisplayMauBaoCao()
        {
            if (dgLoaiSieuAm.SelectedRows == null || dgLoaiSieuAm.SelectedRows.Count <= 0) 
            {
                _drLoaiSieuAm = null;
                //txtTenSieuAm.Text = string.Empty;
                //txtMauBaoCao_Chung.Text = string.Empty;
                //txtMauBaoCao_Nam.Text = string.Empty;
                //txtMauBaoCao_Nu.Text = string.Empty;
                //_textControl1.ResetContents();
                //_textControl2.ResetContents();
                return;
            }

            _drLoaiSieuAm = (dgLoaiSieuAm.SelectedRows[0].DataBoundItem as DataRowView).Row;

            if (_flag) return;

            string loaiSieuAmGUID = _drLoaiSieuAm["LoaiSieuAmGUID"].ToString();

            Result result = SieuAmBus.GetMauBaoCaoList(loaiSieuAmGUID);
            if (result.IsOK)
            {
                List<MauBaoCao> mauBaoCaoList = (List<MauBaoCao>)result.QueryResult;
                txtTenSieuAm.Text = _drLoaiSieuAm["TenSieuAm"].ToString();
                numThuTu.Value = Convert.ToInt32(_drLoaiSieuAm["ThuTu"]);
                chkInTrang2.Checked = Convert.ToBoolean(_drLoaiSieuAm["InTrang2"]);

                chkNam.Checked = false;
                chkNu.Checked = false;
                foreach (var mauBaoCao in mauBaoCaoList)
                {
                    DoiTuong doiTuong = (DoiTuong)mauBaoCao.DoiTuong;
                    TXTextControl.TextControl textControl = null;
                    switch (doiTuong)
                    {
                        case DoiTuong.Chung:
                            raChung.Checked = true;
                            textControl = _textControl1;            
                            break;
                        case DoiTuong.Nam:
                            chkNam.Checked = true;
                            raNamNu.Checked = true;

                            foreach (TabPage tabPage in tabMauBaoCao.TabPages)
                            {
                                if (tabPage.Text == "Mẫu báo cáo (Nam)")
                                {
                                    textControl = (tabPage.Controls[0] as TXTextControl.TextControl);
                                    break;
                                }
                            }
                            break;
                        case DoiTuong.Nu:
                            chkNu.Checked = true;
                            
                            raNamNu.Checked = true;

                            foreach (TabPage tabPage in tabMauBaoCao.TabPages)
                            {
                                if (tabPage.Text == "Mẫu báo cáo (Nữ)")
                                {
                                    textControl = (tabPage.Controls[0] as TXTextControl.TextControl);
                                    break;
                                }
                            }
                            break;
                    }

                    textControl.Load(mauBaoCao.Template.ToArray(), TXTextControl.BinaryStreamType.MSWord);
                    textControl.Tables.GridLines = false;
                }
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("SieuAmBus.GetMauBaoCaoList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("SieuAmBus.GetMauBaoCaoList"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgLoaiSieuAm.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void dgLoaiSieuAm_SelectionChanged(object sender, EventArgs e)
        {
            OnDisplayMauBaoCao();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAdd();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDelete();
        }

        private void raChung_CheckedChanged(object sender, EventArgs e)
        {
            btnBrowse_Chung.Enabled = raChung.Checked;
            if (raChung.Checked)
            {
                txtMauBaoCao_Nam.Text = string.Empty;
                txtMauBaoCao_Nu.Text = string.Empty;
                _textControl1.ResetContents();
                _textControl2.ResetContents();

                _page1.Text = "Mẫu báo cáo (Chung)";
                tabMauBaoCao.TabPages.Clear();
                tabMauBaoCao.TabPages.Add(_page1);
            }
        }

        private void raNamNu_CheckedChanged(object sender, EventArgs e)
        {
            chkNam.Enabled = raNamNu.Checked;
            chkNu.Enabled = raNamNu.Checked;
            btnBrowse_Nam.Enabled = raNamNu.Checked && chkNam.Checked;
            btnBrowse_Nu.Enabled = raNamNu.Checked && chkNu.Checked;

            if (raNamNu.Checked)
            {
                txtMauBaoCao_Chung.Text = string.Empty;
                _textControl1.ResetContents();
                _textControl2.ResetContents();

                tabMauBaoCao.TabPages.Clear();
                if (chkNam.Checked && chkNu.Checked)
                {
                    _page1.Text = "Mẫu báo cáo (Nam)";
                    _page2.Text = "Mẫu báo cáo (Nữ)";
                    tabMauBaoCao.TabPages.Add(_page1);
                    tabMauBaoCao.TabPages.Add(_page2);
                }
                else if (chkNam.Checked)
                {
                    _page1.Text = "Mẫu báo cáo (Nam)";
                    tabMauBaoCao.TabPages.Add(_page1);
                }
                else if (chkNu.Checked)
                {
                    _page2.Text = "Mẫu báo cáo (Nữ)";
                    tabMauBaoCao.TabPages.Add(_page2);
                }
            }
        }

        private void chkNam_CheckedChanged(object sender, EventArgs e)
        {
            if (!raNamNu.Checked) return;
            btnBrowse_Nam.Enabled = raNamNu.Checked && chkNam.Checked;
            tabMauBaoCao.TabPages.Clear();
            if (chkNam.Checked && chkNu.Checked)
            {
                _page1.Text = "Mẫu báo cáo (Nam)";
                _page2.Text = "Mẫu báo cáo (Nữ)";
                tabMauBaoCao.TabPages.Add(_page1);
                tabMauBaoCao.TabPages.Add(_page2);
            }
            else if (chkNam.Checked)
            {
                _page1.Text = "Mẫu báo cáo (Nam)";
                tabMauBaoCao.TabPages.Add(_page1);
            }
            else if (chkNu.Checked)
            {
                _page2.Text = "Mẫu báo cáo (Nữ)";
                tabMauBaoCao.TabPages.Add(_page2);
            }
        }

        private void chkNu_CheckedChanged(object sender, EventArgs e)
        {
            if (!raNamNu.Checked) return;
            btnBrowse_Nu.Enabled = raNamNu.Checked && chkNu.Checked;

            tabMauBaoCao.TabPages.Clear();
            if (chkNam.Checked && chkNu.Checked)
            {
                _page1.Text = "Mẫu báo cáo (Nam)";
                _page2.Text = "Mẫu báo cáo (Nữ)";
                tabMauBaoCao.TabPages.Add(_page1);
                tabMauBaoCao.TabPages.Add(_page2);
            }
            else if (chkNam.Checked)
            {
                _page1.Text = "Mẫu báo cáo (Nam)";
                tabMauBaoCao.TabPages.Add(_page1);
            }
            else if (chkNu.Checked)
            {
                _page2.Text = "Mẫu báo cáo (Nữ)";
                tabMauBaoCao.TabPages.Add(_page2);
            }
        }

        private void btnBrowse_Chung_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    txtMauBaoCao_Chung.Text = dlg.FileName;
                    TXTextControl.TextControl textControl = (_page1.Controls[0] as TXTextControl.TextControl);
                    textControl.Load(dlg.FileName, TXTextControl.StreamType.RichTextFormat);
                    textControl.Tables.GridLines = false;
                }
                catch (Exception ex)
                {
                    MsgBox.Show(Application.ProductName, ex.Message, IconType.Information);
                }
                
            }
        }

        private void btnBrowse_Nam_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                txtMauBaoCao_Nam.Text = dlg.FileName;

                foreach (TabPage tabPage in tabMauBaoCao.TabPages)
                {
                    if (tabPage.Text == "Mẫu báo cáo (Nam)")
                    {
                        try
                        {
                            TXTextControl.TextControl textControl = (tabPage.Controls[0] as TXTextControl.TextControl);
                            textControl.Load(dlg.FileName, TXTextControl.StreamType.RichTextFormat);
                            textControl.Tables.GridLines = false;
                            break;
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(Application.ProductName, ex.Message, IconType.Information);
                        }
                    }
                }
            }
        }

        
        private void btnBrowse_Nu_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                txtMauBaoCao_Nu.Text = dlg.FileName;

                foreach (TabPage tabPage in tabMauBaoCao.TabPages)
                {
                    if (tabPage.Text == "Mẫu báo cáo (Nữ)")
                    {
                        try
                        {
                            TXTextControl.TextControl textControl = (tabPage.Controls[0] as TXTextControl.TextControl);
                            textControl.Load(dlg.FileName, TXTextControl.StreamType.RichTextFormat);
                            textControl.Tables.GridLines = false;
                            break;
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(Application.ProductName, ex.Message, IconType.Information);
                        }
                    }
                }
            }
        }

        private void btnExportWord_Click(object sender, EventArgs e)
        {
            try
            {
                if (raChung.Checked)
                {
                    if (_textControl1.Text.Trim() == string.Empty) return;

                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.Filter = "Rich Text Format (*.rtf)|*.rtf";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        _textControl1.Save(dlg.FileName, TXTextControl.StreamType.RichTextFormat);
                    }
                }
                else
                {
                    if (chkNam.Checked && chkNu.Checked)
                    {
                        if (tabMauBaoCao.SelectedIndex == 0)
                        {
                            if (_textControl1.Text.Trim() == string.Empty) return;
                            SaveFileDialog dlg = new SaveFileDialog();
                            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf";
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                _textControl1.Save(dlg.FileName, TXTextControl.StreamType.RichTextFormat);
                            }
                        }
                        else
                        {
                            if (_textControl2.Text.Trim() == string.Empty) return;
                            SaveFileDialog dlg = new SaveFileDialog();
                            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf";
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                _textControl2.Save(dlg.FileName, TXTextControl.StreamType.RichTextFormat);
                            }
                        }
                    }
                    else
                    {
                        if (chkNam.Checked)
                        {
                            if (_textControl1.Text.Trim() == string.Empty) return;
                            SaveFileDialog dlg = new SaveFileDialog();
                            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf";
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                _textControl1.Save(dlg.FileName, TXTextControl.StreamType.RichTextFormat);
                            }
                        }
                        else if (chkNu.Checked)
                        {
                            if (_textControl2.Text.Trim() == string.Empty) return;
                            SaveFileDialog dlg = new SaveFileDialog();
                            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf";
                            if (dlg.ShowDialog() == DialogResult.OK)
                            {
                                _textControl2.Save(dlg.FileName, TXTextControl.StreamType.RichTextFormat);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayLoaiSieuListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayLoaiSieuList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion
    }
}
