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
using MM.Bussiness;
using MM.Databasae;
using MM.Exports;

namespace MM.Controls
{
    public partial class uLichKham : uBase
    {
        #region Members
        private int _thang = 0;
        private int _nam = 0;
        private string _thangStr = string.Empty;
        private string _namStr = string.Empty;
        private string _currentValue = string.Empty;
        #endregion

        #region Constructor
        public uLichKham()
        {
            InitializeComponent();
            InitData();
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            cboThang.SelectedIndex = DateTime.Now.Month - 1;

            for (int i = 2000; i < 2100; i++)
            {
                cboNam.Items.Add(i.ToString());
            }

            cboNam.Text = DateTime.Now.Year.ToString();

            dgLichKham.KeyUp += new KeyEventHandler(dgLichKham_KeyUp);
        }

        private void UpdateGUI()
        {
            btnExportExcel.Enabled = AllowExport;
            btnPrint.Enabled = AllowPrint;
            btnPrintPreview.Enabled = AllowPrint;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                _thang = Convert.ToInt32(cboThang.Text);
                _nam = Convert.ToInt32(cboNam.Text);
                _thangStr = cboThang.Text;
                _namStr = cboNam.Text;

                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayLichKhamProc));
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

        private int GetRowCount()
        {
            int daysInMonth = DateTime.DaysInMonth(_nam, _thang);
            int rowCount = 2;
            int spaceCount = 0;

            DateTime dt = new DateTime(_nam, _thang, 1);
            for (int i = 0; i < daysInMonth; i++)
            {
                if (dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (i != 0 && i != daysInMonth - 1)
                        spaceCount++;
                }
                
                rowCount++;

                dt = dt.AddDays(1);
            }

            rowCount += spaceCount;

            return rowCount;
        }

        private void InitHeader()
        {
            if (dgLichKham[0, 0] == null)
            {
                Font font = new System.Drawing.Font("Tohama", 9, FontStyle.Bold);
                SourceGrid2.Cells.Real.Cell cell = NewCell("Ngày", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
                dgLichKham[0, 0] = cell;
                dgLichKham[0, 0].RowSpan = 2;

                cell = NewCell("Công ty", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
                dgLichKham[0, 1] = cell;
                dgLichKham[0, 1].ColumnSpan = 2;

                cell = NewCell("Sáng", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
                dgLichKham[1, 1] = cell;

                cell = NewCell("Chiều", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
                dgLichKham[1, 2] = cell;

                cell = NewCell("Bs Nội tổng quát", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
                dgLichKham[0, 3] = cell;
                dgLichKham[0, 3].ColumnSpan = 2;

                cell = NewCell("Sáng", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
                dgLichKham[1, 3] = cell;

                cell = NewCell("Chiều", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
                dgLichKham[1, 4] = cell;

                cell = NewCell("Bs Ngoại tổng quát", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
                dgLichKham[0, 5] = cell;
                dgLichKham[0, 5].ColumnSpan = 2;

                cell = NewCell("Sáng", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
                dgLichKham[1, 5] = cell;

                cell = NewCell("Chiều", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
                dgLichKham[1, 6] = cell;

                cell = NewCell("Bs Siêu âm", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
                dgLichKham[0, 7] = cell;
                dgLichKham[0, 7].ColumnSpan = 2;

                cell = NewCell("Sáng", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
                dgLichKham[1, 7] = cell;

                cell = NewCell("Chiều", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
                dgLichKham[1, 8] = cell;

                cell = NewCell("Bs Sản Phụ Khoa", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
                dgLichKham[0, 9] = cell;
                dgLichKham[0, 9].ColumnSpan = 2;

                cell = NewCell("Sáng", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
                dgLichKham[1, 9] = cell;

                cell = NewCell("Chiều", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
                dgLichKham[1, 10] = cell;
            }
        }

        private SourceGrid2.Cells.Real.Cell NewNumericCell(object value, Color backColor, Color foreColor, ContentAlignment textAlignment, Font font, bool isEnableEdit, string toolTip)
        {
            SourceGrid2.VisualModels.Common visualModel = new SourceGrid2.VisualModels.Common();
            visualModel.BackColor = backColor;
            visualModel.TextAlignment = textAlignment;
            visualModel.ForeColor = foreColor;
            visualModel.Font = font;

            SourceGrid2.DataModels.EditorTextBoxNumeric editorModel = new SourceGrid2.DataModels.EditorTextBoxNumeric(typeof(int));
            editorModel.EnableEdit = isEnableEdit;
            editorModel.MinimumValue = 0;
            editorModel.MaximumValue = 99999;
            editorModel.AllowNull = true;
            editorModel.DefaultValue = null;

            SourceGrid2.Cells.Real.Cell cell = new SourceGrid2.Cells.Real.Cell(value, editorModel, visualModel);
            cell.ToolTipText = toolTip;

            return cell;
        }

        private SourceGrid2.Cells.Real.Cell NewCell(object value, Color backColor, Color foreColor, ContentAlignment textAlignment, Font font, bool isEnableEdit, string toolTip)
        {
            SourceGrid2.VisualModels.Common visualModel = new SourceGrid2.VisualModels.Common();
            visualModel.BackColor = backColor;
            visualModel.TextAlignment = textAlignment;
            visualModel.ForeColor = foreColor;
            visualModel.Font = font;
            visualModel.WordWrap = true;

            SourceGrid2.DataModels.EditorTextBox editorModel = new SourceGrid2.DataModels.EditorTextBox(typeof(string));
            editorModel.EnableEdit = isEnableEdit;

            SourceGrid2.Cells.Real.Cell cell = new SourceGrid2.Cells.Real.Cell(value, editorModel, visualModel);
            cell.ToolTipText = toolTip;

            return cell;
        }

        private void ClearData()
        {
            for (int i = 0; i < dgLichKham.RowsCount; i++)
            {
                for (int j = 0; j < dgLichKham.ColumnsCount; j++)
                {
                    dgLichKham[i, j] = null;
                }
            }
        }

        private LichKham GetLichKham(List<LichKham> lichKhams, DateTime ngay, LoaiLichKham loaiLichKham)
        {
            if (lichKhams == null) return null;
            return lichKhams.Where(l => l.Ngay.ToString("dd/MM/yyyy") == ngay.ToString("dd/MM/yyyy") &&
                l.Type == (int)loaiLichKham).FirstOrDefault();
        }

        private int GetPersonCount(List<Booking> bookingList, int state)
        {
            if (bookingList == null || bookingList.Count <= 0) return 0;

            int count = 0;
            if (state == 0)
            {
                foreach (var booking in bookingList)
                {
                    count += booking.MorningCount;
                }
            }
            else
            {
                foreach (var booking in bookingList)
                {
                    count += booking.AfternoonCount;
                }
            }

            return count;
        }

        private void FillData(List<LichKham> lichKhams)
        {
            DateTime dt = new DateTime(_nam, _thang, 1);
            int daysInMonth = DateTime.DaysInMonth(_nam, _thang);
            int rowIndex = 2;
            Color foreColor = Color.Black;
            Font fontBold = new Font("Tahoma", 9, FontStyle.Bold);
            Font fontNormal = new Font("Tahoma", 9);
            SourceGrid2.RectangleBorder borderRB = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Black), new SourceGrid2.Border(Color.Black));
            SourceGrid2.RectangleBorder borderRTB = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Black), new SourceGrid2.Border(Color.Black));
            borderRTB.Top = new SourceGrid2.Border(Color.Black);

            bool isBorderTop = true;
            for (int i = 0; i < daysInMonth; i++)
            {
                string dateStr = string.Format("{0} {1}", dt.ToString("dd/MM"), Utility.GetDayOfWeek(dt));
                SourceGrid2.Cells.Real.Cell cell = NewCell(dateStr, Color.White, foreColor, ContentAlignment.MiddleCenter, fontBold, false, string.Empty);
                cell.Tag = dt;
                cell.Border = isBorderTop ? borderRTB : borderRB;
                dgLichKham[rowIndex, 0] = cell;

                Result result = BookingBus.GetBooking(dt);
                if (!result.IsOK)
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("BookingBus.GetBooking"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("BookingBus.GetBooking"));
                }

                for (int col = 0; col < 10; col++)
                {
                    if (col < 2)
                    {
                        object value = null;
                        if (result.QueryResult != null)
                        {
                            List<Booking> bookingList = result.QueryResult as List<Booking>;
                            int count = GetPersonCount(bookingList, col);
                            if (count > 0) value = count;
                        }

                        cell = NewNumericCell(value, Color.White, foreColor, ContentAlignment.MiddleCenter, fontNormal, false, string.Empty);
                    }
                    else
                    {
                        LichKham lichKham = GetLichKham(lichKhams, dt, (LoaiLichKham)col);
                        bool isEnable = AllowEdit;
                        object value = lichKham != null ? lichKham.Value : null;
                        cell = NewCell(value, Color.White, foreColor, ContentAlignment.MiddleCenter, fontNormal, isEnable, string.Empty);
                        cell.Tag = lichKham;
                    }

                    cell.Border = isBorderTop ? borderRTB : borderRB; ;
                    dgLichKham[rowIndex, col + 1] = cell;
                }

                isBorderTop = false;
                rowIndex++;

                if (dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    isBorderTop = true;
                    if (i != 0 && i != daysInMonth - 1)
                        rowIndex++;
                }
                
                dt = dt.AddDays(1);
            }
        }

        private void OnDisplayLichKham()
        {
            Result result = LichKhamBus.GetLichKhamTheoThang(_thang, _nam);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    ClearData();
                    int colCount = 11;
                    int rowCount = GetRowCount();
                    List<LichKham> lichKhams = result.QueryResult as List<LichKham>;

                    dgLichKham.Redim(rowCount, colCount);
                    dgLichKham.FixedRows = 2;
                    dgLichKham.FixedColumns = 1;
                    InitHeader();
                    FillData(lichKhams);
                    dgLichKham.AutoSizeView(false);
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("LichKhamBus.GetLichKhamTheoThang"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("LichKhamBus.GetLichKhamTheoThang"));
            }
        }

        private void OnExportExcel()
        {
            if (dgLichKham.RowsCount <= 2) return;

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export Excel";
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (!ExportExcel.ExportLichKhamToExcel(dlg.FileName, dgLichKham, _thangStr, _namStr))
                    return;
            }
        }

        private void OnPrint(bool isPreview)
        {
            if (dgLichKham.RowsCount <= 2) return;
            Cursor.Current = Cursors.WaitCursor;
            string exportFileName = string.Format("{0}\\Temp\\LichKham.xls", Application.StartupPath);
            if (isPreview)
            {
                if (ExportExcel.ExportLichKhamToExcel(exportFileName, dgLichKham, _thangStr, _namStr))
                {
                    try
                    {
                        ExcelPrintPreview.PrintPreview(exportFileName, Global.PageSetupConfig.GetPageSetup(Const.LichKhamTemplate));
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                        return;
                    }
                }
            }
            else
            {
                if (_printDialog.ShowDialog() == DialogResult.OK)
                {
                    if (ExportExcel.ExportLichKhamToExcel(exportFileName, dgLichKham, _thangStr, _namStr))
                    {
                        try
                        {
                            ExcelPrintPreview.Print(exportFileName, _printDialog.PrinterSettings.PrinterName, Global.PageSetupConfig.GetPageSetup(Const.LichKhamTemplate));
                        }
                        catch (Exception ex)
                        {
                            MsgBox.Show(Application.ProductName, "Vui lòng kiểm tra lại máy in.", IconType.Error);
                            return;
                        }
                    }
                }
            }
        }
        #endregion

        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void dgLichKham_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && AllowEdit && dgLichKham.Selection != null)
            {
                List<SourceGrid2.Cells.Real.Cell> deletedCells = new List<SourceGrid2.Cells.Real.Cell>();
                foreach (SourceGrid2.Cells.Real.Cell cell in dgLichKham.Selection.GetCells())
                {
                    if (cell.Column < 3 || cell.Row < 2 || cell.Value == null || 
                        cell.Value.ToString().Trim() == string.Empty) continue;

                    deletedCells.Add(cell);
                }

                foreach (SourceGrid2.Cells.Real.Cell cell in deletedCells)
                {
                    LichKham lichKham = cell.Tag as LichKham;
                    if (lichKham == null) continue;
                    lichKham.UpdatedDate = DateTime.Now;
                    lichKham.UpdatedBy = Guid.Parse(Global.UserGUID);
                    lichKham.Value = string.Empty;

                    Result result = LichKhamBus.InsertLichKham(lichKham);
                    if (result.IsOK)
                        cell.Value = null;
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("LichKhamBus.InsertLichKham"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("LichKhamBus.InsertLichKham"));
                        return;
                    }
                }
            }

            dgLichKham.AutoSizeView(false);
        }

        private void dgLichKham_CellGotFocus(object sender, SourceGrid2.PositionCancelEventArgs e)
        {
            if (e.Position.Column < 3 || e.Position.Row < 2) return;
            object value = (e.Cell as SourceGrid2.Cells.Real.Cell).Value;
            _currentValue = value == null ? string.Empty : value.ToString().Trim();
        }

        private void dgLichKham_CellLostFocus(object sender, SourceGrid2.PositionCancelEventArgs e)
        {
            if (e.Position.Column < 3 || e.Position.Row < 2) return;
            SourceGrid2.Cells.Real.Cell cell = e.Cell as SourceGrid2.Cells.Real.Cell;
            object value = cell.Value;
            string strValue = value == null ? string.Empty : value.ToString().Trim();
            if (_currentValue == strValue) return;

            LichKham lichKham = cell.Tag as LichKham;
            if (lichKham == null)
            {
                lichKham = new LichKham();
                lichKham.Ngay = Convert.ToDateTime(dgLichKham[e.Position.Row, 0].Tag);
                lichKham.Type = e.Position.Column - 1;
                lichKham.Value = strValue;
                lichKham.CreatedDate = DateTime.Now;
                lichKham.CreatedBy = Guid.Parse(Global.UserGUID);
            }
            else
            {
                lichKham.Value = strValue;
                lichKham.UpdatedDate = DateTime.Now;
                lichKham.UpdatedBy = Guid.Parse(Global.UserGUID);
            }

            Result result = LichKhamBus.InsertLichKham(lichKham);
            if (result.IsOK)
                cell.Tag = lichKham;
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("LichKhamBus.InsertLichKham"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("LichKhamBus.InsertLichKham"));
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }
        #endregion

        #region Working Thread
        private void OnDisplayLichKhamProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayLichKham();
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
