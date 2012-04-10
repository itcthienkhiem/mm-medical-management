using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class uBookingList : uBase
    {
        #region Members
        private DateTime _fromDate = DateTime.Now;
        private DateTime _toDate = DateTime.Now;
        #endregion

        #region Constructor
        public uBookingList()
        {
            InitializeComponent();
            dtpkTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0).AddYears(-1);
            dtpkDenNgay.Value = DateTime.Now;
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        public void DisplayAsThread()
        {
            _fromDate = new DateTime(dtpkTuNgay.Value.Year, dtpkTuNgay.Value.Month, dtpkTuNgay.Value.Day, 0, 0, 0);
            _toDate = new DateTime(dtpkDenNgay.Value.Year, dtpkDenNgay.Value.Month, dtpkDenNgay.Value.Day, 23, 59, 59);
            
            ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayBookingListProc));
            base.ShowWaiting();
        }

        private void OnDisplayBookingList()
        {
            Result result = BookingBus.GetBookingList(_fromDate, _toDate);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    DataTable dt = result.QueryResult as DataTable;

                    Hashtable htBooking = new Hashtable();
                    Hashtable htBloodTaking = new Hashtable();
                    int saturdayCount = 0;
                    int count1 = 0;
                    int count2 = 0;
                    string dateStr = string.Empty;

                    foreach (DataRow row in dt.Rows)
                    {
                        BookingType bookingType = (BookingType)Convert.ToInt32(row["BookingType"]);
                        DateTime bookingDate = Convert.ToDateTime(row["BookingDate"]);
                        string bookingDateStr = Convert.ToDateTime(row["BookingDate"]).ToString("yyyy/MM/dd");

                        if (dateStr != bookingDateStr)
                        {
                            dateStr = bookingDateStr;
                            if (bookingDate.DayOfWeek == DayOfWeek.Saturday) saturdayCount++;
                        }

                        //string company = row["Company"].ToString();
                        //int morningCount = Convert.ToInt32(row["MorningCount"]);
                        //int afternoonCount = Convert.ToInt32(row["AfternoonCount"]);
                        //int eveningCount = Convert.ToInt32(row["EveningCount"]);
                        //int pax = Convert.ToInt32(row["Pax"]);
                        //string time = bookingDate.ToString("hh:mm tt");
                        //string sales = row["Sales"].ToString();

                        if (bookingType == BookingType.Monitor)
                        {
                            if (!htBooking.ContainsKey(bookingDateStr))
                            {
                                List<DataRow> rows = new List<DataRow>();
                                rows.Add(row);
                                htBooking.Add(bookingDateStr, rows);
                            }
                            else
                            {
                                List<DataRow> rows = (List<DataRow>)htBooking[bookingDateStr];
                                rows.Add(row);
                            }

                            count1++;
                        }
                        else
                        {
                            if (!htBloodTaking.ContainsKey(bookingDateStr))
                            {
                                List<DataRow> rows = new List<DataRow>();
                                rows.Add(row);
                                htBloodTaking.Add(bookingDateStr, rows);
                            }
                            else
                            {
                                List<DataRow> rows = (List<DataRow>)htBloodTaking[bookingDateStr];
                                rows.Add(row);
                            }

                            count2++;
                        }
                    }

                    int colCount = 14;
                    int rowCount = count1 > count2 ? count1 : count2;
                    rowCount += 3;

                    if (saturdayCount > 0)
                    {
                        DateTime dateTime = Convert.ToDateTime(dt.Rows[dt.Rows.Count - 1]["BookingDate"]);
                        if (dateTime.DayOfWeek == DayOfWeek.Saturday)
                            rowCount += (saturdayCount - 1);
                        else
                            rowCount += saturdayCount;
                    }

                    dgBooking.Redim(rowCount, colCount);

                    InitHeader();
                    if (rowCount > 3)
                    {

                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("BookingBus.GetBookingList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("BookingBus.GetBookingList"));
            }
        }

        private void InitHeader()
        {
            if (dgBooking[0, 0] != null) return;
            Font font = new System.Drawing.Font("Tohama", 10, FontStyle.Bold);
            SourceGrid2.RectangleBorder border = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Black), 
                                                                                 new SourceGrid2.Border(Color.Black));

            SourceGrid2.RectangleBorder border2 = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.LightGray),
                                                                                 new SourceGrid2.Border(Color.Black),
                                                                                 new SourceGrid2.Border(Color.LightGray),
                                                                                 new SourceGrid2.Border(Color.LightGray));

            SourceGrid2.Cells.Real.Cell cell = NewCell("BOOKING MONITOR", Color.White, Color.Red, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = border2;
            dgBooking[0, 0] = cell;
            dgBooking[0, 0].ColumnSpan = 9;
            
            cell = NewCell(string.Empty, Color.White, Color.Red, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[0, 9] = cell;

            cell = NewCell("BLOOD TAKING", Color.White, Color.Red, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[0, 10] = cell;
            dgBooking[0, 10].ColumnSpan = 4;

            cell = NewCell("Day", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = border;
            dgBooking[1, 0] = cell;
            dgBooking[1, 0].RowSpan = 2;

            cell = NewCell("Date", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = border;
            dgBooking[1, 1] = cell;
            dgBooking[1, 1].RowSpan = 2;
            dgBooking.AutoSizeColumn(1, 70);

            cell = NewCell("Company", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = border;
            dgBooking[1, 2] = cell;
            dgBooking[1, 2].RowSpan = 2;
            dgBooking.AutoSizeColumn(2, 170);

            cell = NewCell("Morning", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = border;
            dgBooking[1, 3] = cell;
            dgBooking[1, 3].RowSpan = 2;
            dgBooking.AutoSizeColumn(3, 60);

            cell = NewCell("Afternoon", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[1, 4] = cell;
            dgBooking[1, 4].RowSpan = 2;
            dgBooking.AutoSizeColumn(4, 60);

            cell = NewCell("Evening", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[1, 5] = cell;
            dgBooking[1, 5].RowSpan = 2;
            dgBooking.AutoSizeColumn(5, 60);

            cell = NewCell("Total", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[1, 6] = cell;
            dgBooking[1, 6].ColumnSpan = 3;

            cell = NewCell("Morning", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[2, 6] = cell;
            dgBooking.AutoSizeColumn(6, 60);

            cell = NewCell("Afternoon", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[2, 7] = cell;
            dgBooking.AutoSizeColumn(7, 60);

            cell = NewCell("Evening", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[2, 8] = cell;
            dgBooking.AutoSizeColumn(8, 60);

            cell = NewCell(string.Empty, Color.White, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[1, 9] = cell;
            dgBooking.AutoSizeColumn(9, 30);

            cell = NewCell(string.Empty, Color.White, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[1, 10] = cell;

            cell = NewCell(string.Empty, Color.White, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[1, 11] = cell;

            cell = NewCell(string.Empty, Color.White, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[1, 12] = cell;

            cell = NewCell(string.Empty, Color.White, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[1, 13] = cell;

            cell = NewCell(string.Empty, Color.White, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[2, 9] = cell;

            cell = NewCell("Company", Color.Yellow, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[2, 10] = cell;
            dgBooking.AutoSizeColumn(10, 170);

            cell = NewCell("Pax", Color.Yellow, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[2, 11] = cell;
            dgBooking.AutoSizeColumn(11, 50);

            cell = NewCell("Time", Color.Yellow, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[2, 12] = cell;
            dgBooking.AutoSizeColumn(12, 60);

            cell = NewCell("Sales", Color.Yellow, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[2, 13] = cell;
            dgBooking.AutoSizeColumn(13, 170);

            Font f = new Font("Tohama", 8, FontStyle.Bold);
            cell = NewCell("Thứ 3", Color.White, Color.Black, ContentAlignment.MiddleCenter, f, false, string.Empty);
            dgBooking[3, 0] = cell;

            cell = NewCell("15/03/2012", Color.White, Color.Black, ContentAlignment.MiddleCenter, f, false, string.Empty);
            dgBooking[3, 1] = cell;
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
        #endregion

        #region Window Event Handlers
        private void btnView_Click(object sender, EventArgs e)
        {
            DisplayAsThread();
        }
        #endregion

        #region Working Thread
        private void OnDisplayBookingListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayBookingList();
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
