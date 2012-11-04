using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Databasae;
using MM.Bussiness;
using MM.Common;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uDanhSachXetNghiem_CellDyn3200List : uBase
    {
        #region Members
        private DataTable _dataSource = null;
        private string _tenXetNghiem = string.Empty;
        private DataRow _row = null;
        #endregion

        #region Constructor
        public uDanhSachXetNghiem_CellDyn3200List()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void HideChiSoXetNghiem()
        {
            groupBox1.Visible = false;
            btnOK.Visible = false;
        }

        private void ShowChiSoXetNghiem()
        {
            groupBox1.Visible = true;
            btnOK.Visible = true;
        }

        public void DisplayAsThread()
        {
            try
            {
                HideChiSoXetNghiem();
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayXetNghiemListProc));
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
            if (_dataSource != null)
            {
                _dataSource.Rows.Clear();
                _dataSource.Clear();
                _dataSource = null;
            }

            DataTable dt = dgXetNghiem.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgXetNghiem.DataSource = null;
            }
        }

        private void ClearDataSource()
        {
            DataTable dt = dgXetNghiem.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgXetNghiem.DataSource = null;
            }
        }

        private void OnDisplayXetNghiemList()
        {
            Result result = XetNghiem_CellDyn3200Bus.GetDanhSachXetNghiem();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    _dataSource = result.QueryResult as DataTable;
                    OnSearchXetNghiem();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_CellDyn3200Bus.GetDanhSachXetNghiem"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_CellDyn3200Bus.GetDanhSachXetNghiem"));
            }
        }

        private void OnSearchXetNghiem()
        {
            
            HideChiSoXetNghiem();
            List<DataRow> results = null;
            DataTable newDataSource = null;

            ClearDataSource();

            if (txtXetNghiem.Text.Trim() == string.Empty)
            {
                results = (from p in _dataSource.AsEnumerable()
                           orderby p.Field<int>("GroupID"), p.Field<int>("Order")
                           select p).ToList<DataRow>();

                newDataSource = _dataSource.Clone();
                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgXetNghiem.DataSource = newDataSource;
                if (dgXetNghiem.RowCount > 0) dgXetNghiem.Rows[0].Selected = true;
                return;
            }

            string str = txtXetNghiem.Text.ToLower();
            newDataSource = _dataSource.Clone();

            //Name
            results = (from p in _dataSource.AsEnumerable()
                       where p.Field<string>("Fullname") != null &&
                           p.Field<string>("Fullname").Trim() != string.Empty &&
                           p.Field<string>("Fullname").ToLower().IndexOf(str) >= 0
                       orderby p.Field<int>("GroupID"), p.Field<int>("Order")
                       select p).ToList<DataRow>();

            foreach (DataRow row in results)
                newDataSource.ImportRow(row);

            if (newDataSource.Rows.Count > 0)
            {
                dgXetNghiem.DataSource = newDataSource;
                return;
            }

            dgXetNghiem.DataSource = newDataSource;
        }

        private DataRow GetDataRow(string xetNghiemGUID)
        {
            if (_dataSource == null || _dataSource.Rows.Count <= 0) return null;
            DataRow[] rows = _dataSource.Select(string.Format("XetNghiemGUID = '{0}'", xetNghiemGUID));
            if (rows == null || rows.Length <= 0) return null;

            return rows[0];
        }

        private void OnEdit()
        {
            if (_dataSource == null) return;

            if (dgXetNghiem.SelectedRows == null || dgXetNghiem.SelectedRows.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn 1 xét nghiệm.", IconType.Information);
                return;
            }

            string xetNghiemGUID = (dgXetNghiem.SelectedRows[0].DataBoundItem as DataRowView).Row["XetNghiemGUID"].ToString();
            DataRow drXetNghiem = GetDataRow(xetNghiemGUID);
            if (drXetNghiem == null) return;

            dlgAddXetNghiem_CellDyn3200 dlg = new dlgAddXetNghiem_CellDyn3200(drXetNghiem);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                DisplayAsThread();
            }
        }

        private void OnDisplayChiSoXetNghiem()
        {
            try
            {
                if (dgXetNghiem.RowCount <= 0) return;
                if (dgXetNghiem.SelectedRows == null || dgXetNghiem.SelectedRows.Count <= 0) return;

                ShowChiSoXetNghiem();

                groupBox1.Enabled = AllowEdit;
                btnOK.Enabled = AllowEdit;

                string xetNghiemGUID = (dgXetNghiem.SelectedRows[0].DataBoundItem as DataRowView).Row["XetNghiemGUID"].ToString();
                _row = GetDataRow(xetNghiemGUID);
                if (_row["FromValue"] != null && _row["FromValue"] != DBNull.Value)
                {
                    chkFromValue_Normal.Checked = true;
                    numFromValue_Normal.Value = (Decimal)Convert.ToDouble(_row["FromValue"]);
                }
                else
                {
                    chkFromValue_Normal.Checked = false;
                    numFromValue_Normal.Value = 0;
                }

                if (_row["ToValue"] != null && _row["ToValue"] != DBNull.Value)
                {
                    chkToValue_Normal.Checked = true;
                    numToValue_Normal.Value = (Decimal)Convert.ToDouble(_row["ToValue"]);
                }
                else
                {
                    chkToValue_Normal.Checked = false;
                    numToValue_Normal.Value = 0;
                }

                _tenXetNghiem = _row["Fullname"].ToString();

                //_tenXetNghiem = _row["Fullname"].ToString().ToUpper();
                //if (_tenXetNghiem != "LYM" && _tenXetNghiem != "BASO" && _tenXetNghiem != "MONO" &&
                //    _tenXetNghiem != "EOS" && _tenXetNghiem != "NEU")
                //{
                //    chkFromValue_NormalPercent.Enabled = false;
                //    chkFromValue_NormalPercent.Checked = false;
                //    numFromValue_NormalPercent.Enabled = false;
                //    chkToValue_NormalPercent.Enabled = false;
                //    chkToValue_NormalPercent.Checked = false;
                //    numToValue_NormalPercent.Enabled = false;
                //    numFromValue_NormalPercent.Value = 0;
                //    numToValue_NormalPercent.Value = 0;
                //}
                //else
                //{
                //    chkFromValue_NormalPercent.Enabled = true;
                //    numFromValue_NormalPercent.Enabled = true;
                //    chkToValue_NormalPercent.Enabled = true;
                //    numToValue_NormalPercent.Enabled = true;

                //    if (_row["FromPercent"] != null && _row["FromPercent"] != DBNull.Value)
                //    {
                //        chkFromValue_NormalPercent.Checked = true;
                //        numFromValue_NormalPercent.Value = (Decimal)Convert.ToDouble(_row["FromPercent"]);
                //    }
                //    else
                //    {
                //        chkFromValue_NormalPercent.Checked = false;
                //        numFromValue_NormalPercent.Value = 0;
                //    }

                //    if (_row["ToPercent"] != null && _row["ToPercent"] != DBNull.Value)
                //    {
                //        chkToValue_NormalPercent.Checked = true;
                //        numToValue_NormalPercent.Value = (Decimal)Convert.ToDouble(_row["ToPercent"]);
                //    }
                //    else
                //    {
                //        chkToValue_NormalPercent.Checked = false;
                //        numToValue_NormalPercent.Value = 0;
                //    }
                //}
            }
            catch (Exception e)
            {
                MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (!chkFromValue_Normal.Checked && !chkToValue_Normal.Checked)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng nhập chỉ số xét nghiệm.", IconType.Information);
                chkFromValue_Normal.Focus();
                return false;
            }

            //if ((_tenXetNghiem == "LYM" || _tenXetNghiem == "BASO" || _tenXetNghiem == "MONO" ||
            //    _tenXetNghiem == "EOS" || _tenXetNghiem == "NEU") && !chkFromValue_NormalPercent.Checked && !chkToValue_NormalPercent.Checked)
            //{
            //    MsgBox.Show(Application.ProductName, "Vui lòng nhập chỉ số % xét nghiệm.", IconType.Information);
            //    chkFromValue_NormalPercent.Focus();
            //    return false;
            //}

            return true;
        }

        private void SaveAsThread()
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
                XetNghiem_CellDyn3200 xetNghiem = new XetNghiem_CellDyn3200();
                xetNghiem.XetNghiemGUID = Guid.Parse(_row["XetNghiemGUID"].ToString());
                xetNghiem.UpdatedDate = DateTime.Now;
                xetNghiem.UpdatedBy = Guid.Parse(Global.UserGUID);

                MethodInvoker method = delegate
                {
                    xetNghiem.TenXetNghiem = _tenXetNghiem;
                    xetNghiem.FullName = _tenXetNghiem;

                    if (chkFromValue_Normal.Checked)
                        xetNghiem.FromValue = (double)numFromValue_Normal.Value;

                    if (chkToValue_Normal.Checked)
                        xetNghiem.ToValue = (double)numToValue_Normal.Value;

                    //if (chkFromValue_NormalPercent.Enabled && chkFromValue_NormalPercent.Checked)
                    //    xetNghiem.FromPercent = (double)numFromValue_NormalPercent.Value;

                    //if (chkToValue_NormalPercent.Enabled && chkToValue_NormalPercent.Checked)
                    //    xetNghiem.ToPercent = (double)numToValue_NormalPercent.Value;

                    Result result = XetNghiem_CellDyn3200Bus.UpdateXetNghiem(xetNghiem);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_CellDyn3200Bus.UpdateXetNghiem"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_CellDyn3200Bus.UpdateXetNghiem"));
                    }
                    else
                    {
                        DataRow row = GetDataRow(xetNghiem.XetNghiemGUID.ToString());
                        if (row != null)
                        {
                            if (chkFromValue_Normal.Checked)
                                row["FromValue"] = xetNghiem.FromValue.Value;
                            else
                                row["FromValue"] = DBNull.Value;

                            if (chkToValue_Normal.Checked)
                                row["ToValue"] = xetNghiem.ToValue.Value;
                            else
                                row["ToValue"] = DBNull.Value;

                            //if (chkFromValue_NormalPercent.Enabled && chkFromValue_NormalPercent.Checked)
                            //    row["FromPercent"] = xetNghiem.FromPercent.Value;
                            //else
                            //    row["FromPercent"] = DBNull.Value;

                            //if (chkToValue_NormalPercent.Enabled && chkToValue_NormalPercent.Checked)
                            //    row["ToPercent"] = xetNghiem.ToPercent.Value;
                            //else
                            //    row["ToPercent"] = DBNull.Value;

                            MsgBox.Show(Application.ProductName, "Lưu chỉ số xét nghiệm thành công.", IconType.Information);
                        }
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
        private void txtXetNghiem_TextChanged(object sender, EventArgs e)
        {
            OnSearchXetNghiem();
        }
        
        private void dgXetNghiem_SelectionChanged(object sender, EventArgs e)
        {
            OnDisplayChiSoXetNghiem();
        }

        private void chkFromValue_Normal_CheckedChanged(object sender, EventArgs e)
        {
            numFromValue_Normal.Enabled = chkFromValue_Normal.Checked;
        }

        private void chkToValue_Normal_CheckedChanged(object sender, EventArgs e)
        {
            numToValue_Normal.Enabled = chkToValue_Normal.Checked;
        }

        private void chkFromValue_NormalPercent_CheckedChanged(object sender, EventArgs e)
        {
            numFromValue_NormalPercent.Enabled = chkFromValue_NormalPercent.Checked;
        }

        private void chkToValue_NormalPercent_CheckedChanged(object sender, EventArgs e)
        {
            numToValue_NormalPercent.Enabled = chkToValue_NormalPercent.Checked;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CheckInfo()) SaveAsThread();
        }
        #endregion

        #region Working Thread
        private void OnDisplayXetNghiemListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayXetNghiemList();
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
