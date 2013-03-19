using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Bussiness;
using MM.Common;
using MM.Exports;

namespace MM.Controls
{
    public partial class uBaoCaoCongNoHopDong : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uBaoCaoCongNoHopDong()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        public void ClearData()
        {
            DataTable dt = cboHopDong.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                //cboHopDong.DataSource = null;
            }
        }

        public void DisplayAsThread()
        {
            Result result = CompanyContractBus.GetContractList();
            if (result.IsOK)
            {
                DataTable dtContract = result.QueryResult as DataTable;
                cboHopDong.DataSource = dtContract;
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("CompanyContractBus.GetContractList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("CompanyContractBus.GetContractList"));
            }
        }

        private void OnPrint(bool isPreview)
        {
            Cursor.Current = Cursors.WaitCursor;
            
            if (cboHopDong.SelectedValue != null && cboHopDong.Text.Trim() != string.Empty)
            {
                string hopDongGUID = cboHopDong.SelectedValue.ToString();
                DataRow row = GetHopDongRow(hopDongGUID);
                string template = raTongHop.Checked ? Const.BaoCaoCongNoHopDongTongHopTemplate : Const.BaoCaoCongNoHopDongChiTietTemplate;

                string exportFileName = string.Format("{0}\\Temp\\BaoCaoCongNoHopDong.xls", Application.StartupPath);
                if (isPreview)
                {
                    //if (raTongHop.Checked)
                    //{
                    //    if (!ExportExcel.ExportCongNoHopDongTongHopToExcel(exportFileName, hopDongGUID, row["ContractCode"].ToString(), cboHopDong.Text))
                    //        return;
                    //}
                    //else
                    //{
                    //    if (!ExportExcel.ExportCongNoHopDongChiTietToExcel(exportFileName, hopDongGUID, row["ContractCode"].ToString(), cboHopDong.Text))
                    //        return;
                    //}

                    if (!ExportExcel.ExportCongNoHopDongToExcel(exportFileName, hopDongGUID, row["ContractCode"].ToString(), cboHopDong.Text))
                        return;

                    try
                    {
                        ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(template));
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                        return;
                    }
                }
                else
                {
                    if (_printDialog.ShowDialog() == DialogResult.OK)
                    {
                        //if (raTongHop.Checked)
                        //{
                        //    if (!ExportExcel.ExportCongNoHopDongTongHopToExcel(exportFileName, hopDongGUID, row["ContractCode"].ToString(), cboHopDong.Text))
                        //    return;
                        //}
                        //else
                        //{
                        //    if (!ExportExcel.ExportCongNoHopDongChiTietToExcel(exportFileName, hopDongGUID, row["ContractCode"].ToString(), cboHopDong.Text))
                        //    return;
                        //}

                        if (!ExportExcel.ExportCongNoHopDongToExcel(exportFileName, hopDongGUID, row["ContractCode"].ToString(), cboHopDong.Text))
                            return;
                        
                        try
                        {
                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(template));
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                            return;
                        }
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng chọn hợp đồng cần xem.", IconType.Information);
        }

        private DataRow GetHopDongRow(string hopDongGUID)
        {
            DataTable dt = cboHopDong.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return null;

            DataRow[] rows = dt.Select(string.Format("CompanyContractGUID='{0}'", hopDongGUID));
            if (rows == null || rows.Length <= 0) return null;

            return rows[0];
        }

        private void OnExportExcell()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (cboHopDong.SelectedValue != null && cboHopDong.Text.Trim() != string.Empty)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "Export Excel";
                dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string hopDongGUID = cboHopDong.SelectedValue.ToString();
                    DataRow row = GetHopDongRow(hopDongGUID);

                    //if (raTongHop.Checked)
                    //{
                    //    if (!ExportExcel.ExportCongNoHopDongTongHopToExcel(dlg.FileName, hopDongGUID, row["ContractCode"].ToString(), cboHopDong.Text))
                    //        return;
                    //}
                    //else
                    //{
                    //    if (!ExportExcel.ExportCongNoHopDongChiTietToExcel(dlg.FileName, hopDongGUID, row["ContractCode"].ToString(), cboHopDong.Text))
                    //        return;
                    //}

                    ExportExcel.ExportCongNoHopDongToExcel(dlg.FileName, hopDongGUID, row["ContractCode"].ToString(), cboHopDong.Text);
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng chọn hợp đồng cần xuất excel.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportExcell();
        }
        #endregion
    }
}
