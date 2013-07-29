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
using MM.Dialogs;
using MM.Exports;
using System.IO;

namespace MM.Controls
{
    public partial class uInMauHoSo : uBase
    {
        #region Members
        private string _name = string.Empty;
        private int _type = 0;
        private int _doiTuong = 0;
        private string _hopDongGUID = string.Empty;
        private string _patientGUID = string.Empty;
        private string _tenNhanVien = string.Empty;
        private string _contractMemberGUID = string.Empty;
        private Dictionary<string, DataRow> _dictPatient = new Dictionary<string, DataRow>();
        private DataTable _dtTemp = null;
        private bool _isAscending = true;
        private bool _isCheckAll = false;
        private bool _flag = false;
        #endregion

        #region Constructor
        public uInMauHoSo()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        public void ClearData()
        {
            DataTable dtOld = dgPatient.DataSource as DataTable;
            if (dtOld != null)
            {
                dtOld.Rows.Clear();
                dtOld.Clear();
                dtOld = null;
                dgPatient.DataSource = null;
            }
        }

        private void UpdateGUI()
        {
            btnPrint.Enabled = AllowPrint;
            inHoSoToolStripMenuItem.Enabled = AllowPrint;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayHopDongListProc));
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

        private void OnDisplayHopDongList()
        {
            Result result = CompanyContractBus.GetContractList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    cboMaHopDong.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyContractBus.GetNoCompletedContractList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetNoCompletedContractList"));
            }
        }

        private string GetTenHopDong(string hopDongGUID)
        {
            string tenHopDong = string.Empty;

            DataTable dt = cboMaHopDong.DataSource as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] rows = dt.Select(string.Format("CompanyContractGUID='{0}'", hopDongGUID));
                if (rows == null || rows.Length <= 0) return string.Empty;

                tenHopDong = rows[0]["ContractName"].ToString();
            }

            return tenHopDong;
        }

        private void UpdateChecked(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string key = row["PatientGUID"].ToString();
                if (_dictPatient.ContainsKey(key))
                    row["Checked"] = true;
            }
        }

        private void OnDisplayDanhSachNhanVien()
        {
            lock (ThisLock)
            {
                Result result = CompanyContractBus.GetContractMemberList(_hopDongGUID, _name, _type, _doiTuong);

                if (result.IsOK)
                {
                    dgPatient.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();

                        DataTable dt = result.QueryResult as DataTable;
                        if (_dtTemp == null) _dtTemp = dt.Clone();
                        
                        dgPatient.DataSource = dt;

                        if (_isCheckAll)
                        {
                            _flag = true;
                            chkChecked.Checked = true;
                            _flag = false;
                            OnCheckAll(true);
                            _isCheckAll = false;
                        }
                        else
                            UpdateChecked(dt);
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyContractBus.GetContractMemberList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetContractMemberList"));
                }
            }
        }

        public override void SearchAsThread()
        {
            try
            {
                _name = txtSearchPatient.Text;
                if (chkMaBenhNhan.Checked) _type = 1;
                else _type = 0;

                if (raAll.Checked) _doiTuong = 0;
                else if (raNam.Checked) _doiTuong = 1;
                else if (raNu.Checked) _doiTuong = 2;
                else if (raNuCoGiaDinh.Checked) _doiTuong = 3;
                else if (raNamTren40.Checked) _doiTuong = 4;
                else _doiTuong = 5;

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayDanhSachNhanVientProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnPrint()
        {
            List<DataRow> checkedRows = _dictPatient.Values.ToList();
            if (checkedRows.Count > 0)
            {
                if (_printDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (DataRow row in checkedRows)
                    {
                        string contractMemberGUID = row["ContractMemberGUID"].ToString();
                        Result result = MauHoSoBus.GetMauChayHoSo(contractMemberGUID, _hopDongGUID);
                        if (!result.IsOK)
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("MauHoSoBus.GetMauChayHoSo"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("MauHoSoBus.GetMauChayHoSo"));
                            return;
                        }

                        List<MauHoSo> mauHoSoList = result.QueryResult as List<MauHoSo>;
                        if (mauHoSoList == null || mauHoSoList.Count <= 0) continue;

                        result = MauHoSoBus.GetDichVuChayMauHoSo(contractMemberGUID, _hopDongGUID);
                        if (!result.IsOK)
                        {
                            MsgBox.Show(Application.ProductName, result.GetErrorAsString("MauHoSoBus.GetDichVuChayMauHoSo"), IconType.Error);
                            Utility.WriteToTraceLog(result.GetErrorAsString("MauHoSoBus.GetDichVuChayMauHoSo"));
                            return;
                        }

                        DataTable dtAllService = result.QueryResult as DataTable;
                        DataRow[] serviceRows3 = dtAllService.Select(string.Format("Loai=3"));
                        if (serviceRows3 != null && serviceRows3.Length > 0)
                        {
                            foreach (DataRow r in serviceRows3)
                            {
                                string serviceName = r["Name"].ToString();
                                bool isNormal_Abnormal = Convert.ToBoolean(r["Normal_Abnormal"]);
                                bool isNegative_Positive = Convert.ToBoolean(r["Negative_Positive"]);
                                if (!isNormal_Abnormal && !isNegative_Positive)
                                {
                                    MsgBox.Show(Application.ProductName, string.Format("Dịch vụ: '{0}' chưa được cấu hình. Vui lòng cấu hình cho dịch vụ này.", serviceName), 
                                        IconType.Information);
                                    return;
                                }
                            }
                        }

                        foreach (MauHoSo mauHoSo in mauHoSoList)
                        {
                            try
                            {
                                object fileName = GetMauHoSoTemplate(mauHoSo);
                                object reportFileName = string.Format("{0}\\Temp\\{1}", Application.StartupPath, Path.GetFileName(fileName.ToString()));
                                File.Copy(fileName.ToString(), reportFileName.ToString(), true);

                                if (mauHoSo.Loai == 1)
                                {
                                    DataRow[] serviceRows = dtAllService.Select(string.Format("Loai=1"));
                                    ExportWord.PrintXetNghiem(reportFileName, row, serviceRows,
                                        ExcelPrintPreview.ConvertToExcelPrinterFriendlyName(_printDialog.PrinterSettings.PrinterName));
                                }
                                else if (mauHoSo.Loai == 2)
                                {
                                    DataRow[] serviceRows = dtAllService.Select(string.Format("Loai=2"));
                                    ExportWord.PrintChecklist(reportFileName, row, serviceRows,
                                        ExcelPrintPreview.ConvertToExcelPrinterFriendlyName(_printDialog.PrinterSettings.PrinterName));
                                }
                                else if (mauHoSo.Loai == 3)
                                {
                                    DataRow[] serviceRows = dtAllService.Select(string.Format("Loai=3"));
                                    ExportWord.PrintKetQuaCanLamSang(reportFileName, row, serviceRows,
                                        ExcelPrintPreview.ConvertToExcelPrinterFriendlyName(_printDialog.PrinterSettings.PrinterName));
                                }
                                else
                                {
                                    ExportWord.PrintMauHoSoChung(reportFileName, row,
                                        ExcelPrintPreview.ConvertToExcelPrinterFriendlyName(_printDialog.PrinterSettings.PrinterName));
                                }
                            }
                            catch (Exception ex)
                            {
                                MsgBox.Show(Application.ProductName, ex.Message, IconType.Error);
                                Utility.WriteToTraceLog(ex.Message);
                            }
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân cần in.", IconType.Information);
        }

        private string GetMauHoSoTemplate(MauHoSo mauHoSo)
        {
            switch (mauHoSo.Loai)
            {
                case 1:
                    return string.Format("{0}\\Templates\\1 ABORATORY REQUEST FORM.doc", Application.StartupPath);
                case 2:
                    return string.Format("{0}\\Templates\\2 CHECK LIST.doc", Application.StartupPath);
                case 3:
                    return string.Format("{0}\\Templates\\3 GENERAL EXAMINATION REPORT NEW.doc", Application.StartupPath);
                case 4:
                    return string.Format("{0}\\Templates\\4 ECG FORM NEW.doc", Application.StartupPath);
                case 5:
                    return string.Format("{0}\\Templates\\5 X RAY.doc", Application.StartupPath);
                case 6:
                    return string.Format("{0}\\Templates\\6 AUDIOMETRY.doc", Application.StartupPath);
                case 7:
                    return string.Format("{0}\\Templates\\7 SO DO RANG.doc", Application.StartupPath);
                case 8:
                    return string.Format("{0}\\Templates\\8 TAT KUC XA.doc", Application.StartupPath);
            }

            return string.Empty;
        }

        private void OnCheckAll(bool check)
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = check;
                string patientGUID = row["PatientGUID"].ToString();
                if (check)
                {
                    if (!_dictPatient.ContainsKey(patientGUID))
                    {
                        _dtTemp.ImportRow(row);
                        _dictPatient.Add(patientGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                    }
                }
                else
                {
                    if (_dictPatient.ContainsKey(patientGUID))
                    {
                        _dictPatient.Remove(patientGUID);

                        DataRow[] rows = _dtTemp.Select(string.Format("PatientGUID='{0}'", patientGUID));
                        if (rows != null && rows.Length > 0)
                            _dtTemp.Rows.Remove(rows[0]);
                    }
                }
            }
        }
        #endregion

        #region Window Event Handlers
        private void cboMaHopDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaHopDong.SelectedValue == null || cboMaHopDong.SelectedValue == DBNull.Value) return;

            _hopDongGUID = cboMaHopDong.SelectedValue.ToString();
            txtTenHopDong.Text = GetTenHopDong(_hopDongGUID);

            _dictPatient.Clear();
            if (_dtTemp != null) _dtTemp.Rows.Clear();
            _isCheckAll = true;
            SearchAsThread();
        }

        private void chkMaBenhNhan_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void raAll_CheckedChanged(object sender, EventArgs e)
        {
            if (raAll.Checked) SearchAsThread();
        }

        private void raNam_CheckedChanged(object sender, EventArgs e)
        {
            if (raNam.Checked) SearchAsThread();
        }

        private void raNu_CheckedChanged(object sender, EventArgs e)
        {
            if (raNu.Checked) SearchAsThread();
        }

        private void raNuCoGiaDinh_CheckedChanged(object sender, EventArgs e)
        {
            if (raNuCoGiaDinh.Checked) SearchAsThread();
        }

        private void raNamTren40_CheckedChanged(object sender, EventArgs e)
        {
            if (raNamTren40.Checked) SearchAsThread();
        }

        private void raNuTren40_CheckedChanged(object sender, EventArgs e)
        {
            if (raNuTren40.Checked) SearchAsThread();
        }

        private void txtSearchPatient_TextChanged(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void txtSearchPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgPatient.Focus();

                if (dgPatient.SelectedRows != null && dgPatient.SelectedRows.Count > 0)
                {
                    int index = dgPatient.SelectedRows[0].Index;
                    if (index < dgPatient.RowCount - 1)
                    {
                        index++;
                        dgPatient.CurrentCell = dgPatient[1, index];
                        dgPatient.Rows[index].Selected = true;
                    }
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                dgPatient.Focus();

                if (dgPatient.SelectedRows != null && dgPatient.SelectedRows.Count > 0)
                {
                    int index = dgPatient.SelectedRows[0].Index;
                    if (index > 0)
                    {
                        index--;
                        dgPatient.CurrentCell = dgPatient[1, index];
                        dgPatient.Rows[index].Selected = true;
                    }
                }
            }
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            if (_flag) return;
            OnCheckAll(chkChecked.Checked);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint();
        }

        private void inHoSoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint();
        }

        private void dgPatient_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                _isAscending = !_isAscending;

                DataTable dt = dgPatient.DataSource as DataTable;
                DataTable newDataSource = null;

                if (_isAscending)
                {
                    newDataSource = (from p in dt.AsEnumerable()
                                     orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                                     select p).CopyToDataTable();
                }
                else
                {
                    newDataSource = (from p in dt.AsEnumerable()
                                     orderby p.Field<string>("FirstName") descending, p.Field<string>("FullName") descending
                                     select p).CopyToDataTable();
                }

                dgPatient.DataSource = newDataSource;

                if (dt != null)
                {
                    dt.Rows.Clear();
                    dt.Clear();
                    dt = null;
                }
            }
            else
                _isAscending = false;
        }

        private void dgPatient_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0) return;

            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgPatient.Rows[e.RowIndex].Cells[0];
            DataRow row = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row;
            string patientGUID = row["PatientGUID"].ToString();
            bool isChecked = Convert.ToBoolean(cell.EditingCellFormattedValue);

            if (isChecked)
            {
                if (!_dictPatient.ContainsKey(patientGUID))
                {
                    _dtTemp.ImportRow(row);
                    _dictPatient.Add(patientGUID, _dtTemp.Rows[_dtTemp.Rows.Count - 1]);
                }
            }
            else
            {
                if (_dictPatient.ContainsKey(patientGUID))
                {
                    _dictPatient.Remove(patientGUID);

                    DataRow[] rows = _dtTemp.Select(string.Format("PatientGUID='{0}'", patientGUID));
                    if (rows != null && rows.Length > 0)
                        _dtTemp.Rows.Remove(rows[0]);
                }
            }
        }
        #endregion

        #region Working Thread
        private void OnDisplayHopDongListProc(object state)
        {
            try
            {
                OnDisplayHopDongList();
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

        private void OnDisplayDanhSachNhanVientProc(object state)
        {
            try
            {
                OnDisplayDanhSachNhanVien();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }
        #endregion

        

       

        

        
       

        

        

        
    }
}
