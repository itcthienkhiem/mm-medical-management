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

namespace MM.Controls
{
    public partial class uLichKham : uBase
    {
        #region Members
        private int _thang = 0;
        private int _nam = 0;
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
                else
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

        private SourceGrid2.Cells.Real.Cell NewCell(object value, Color backColor, Color foreColor, ContentAlignment textAlignment, Font font, bool isEnableEdit, string toolTip)
        {
            SourceGrid2.VisualModels.Common visualModel = new SourceGrid2.VisualModels.Common();
            visualModel.BackColor = backColor;
            visualModel.TextAlignment = textAlignment;
            visualModel.ForeColor = foreColor;
            visualModel.Font = font;

            SourceGrid2.DataModels.EditorTextBox editorModel = new SourceGrid2.DataModels.EditorTextBoxNumeric(typeof(string));
            editorModel.EnableEdit = isEnableEdit;

            SourceGrid2.Cells.Real.Cell cell = new SourceGrid2.Cells.Real.Cell(value, editorModel, visualModel);
            cell.ToolTipText = toolTip;

            return cell;
        }

        private void ClearData()
        {
            for (int i = 2; i < dgLichKham.RowsCount; i++)
            {
                for (int j = 0; j < dgLichKham.ColumnsCount; j++)
                {
                    dgLichKham[i, j] = null;
                }
            }
        }

        private void FillData(List<LichKham> lichKhams)
        {
            DateTime dt = new DateTime(_nam, _thang, 1);
            int daysInMonth = DateTime.DaysInMonth(_nam, _thang);
            int rowIndex = 2;
            Color foreColor = Color.Black;
            Font fontBold = new Font("Tahoma", 9, FontStyle.Bold);
            Font fontNormal = new Font("Tahoma", 9);

            for (int i = 0; i < daysInMonth; i++)
            {
                if (dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (i != 0 && i != daysInMonth - 1)
                        rowIndex++;
                }
                else
                {
                    string dateStr = string.Format("{0} {1}", dt.ToString("dd/MM"), Utility.GetDayOfWeek(dt));
                    SourceGrid2.Cells.Real.Cell cell = NewCell(dateStr, Color.White, foreColor, ContentAlignment.MiddleCenter, fontBold, false, string.Empty);
                    dgLichKham[rowIndex, 0] = cell;
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
                    InitHeader();
                    FillData(lichKhams);
                    dgLichKham.AutoSizeAll(25, 105);
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
        #endregion

        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
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
