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
            dtpkTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
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
                    List<string> keys = new List<string>();

                    foreach (DataRow row in dt.Rows)
                    {
                        BookingType bookingType = (BookingType)Convert.ToInt32(row["BookingType"]);
                        DateTime bookingDate = Convert.ToDateTime(row["BookingDate"]);
                        string bookingDateStr = Convert.ToDateTime(row["BookingDate"]).ToString("yyyy/MM/dd");

                        if (!keys.Contains(bookingDateStr)) keys.Add(bookingDateStr);

                        if (dateStr != bookingDateStr)
                        {
                            dateStr = bookingDateStr;
                            if (bookingDate.DayOfWeek == DayOfWeek.Saturday) saturdayCount++;
                        }

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

                    ClearData();       

                    dgBooking.Redim(rowCount, colCount);

                    InitHeader();
                    if (keys.Count > 0) FillData(htBooking, htBloodTaking, keys);
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

        private void ClearData()
        {
            for (int i = 3; i < dgBooking.RowsCount; i++)
            {
                for (int j = 0; j < dgBooking.ColumnsCount; j++)
                {
                    dgBooking[i, j] = null;
                }
            }
        }

        private void FillData(Hashtable htBooking, Hashtable htBloodTaking, List<string> keys)
        {
            SourceGrid2.RectangleBorder borderRB = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Black), new SourceGrid2.Border(Color.Black));
            SourceGrid2.RectangleBorder borderRB2 = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Black), 
                new SourceGrid2.Border(Color.Black, 2));

            SourceGrid2.RectangleBorder borderR = new SourceGrid2.RectangleBorder();
            borderR.Right = new SourceGrid2.Border(Color.Black);
            borderR.Bottom = new SourceGrid2.Border(Color.LightGray);

            SourceGrid2.RectangleBorder borderRT = new SourceGrid2.RectangleBorder();
            borderRT.Top = new SourceGrid2.Border(Color.Black);
            borderRT.Right = new SourceGrid2.Border(Color.Black);
            borderRT.Bottom = new SourceGrid2.Border(Color.LightGray);

            SourceGrid2.RectangleBorder borderTB = new SourceGrid2.RectangleBorder();
            borderTB.Top = new SourceGrid2.Border(Color.Black);
            borderTB.Right = new SourceGrid2.Border(Color.LightGray);
            borderTB.Bottom = new SourceGrid2.Border(Color.Black);

            SourceGrid2.RectangleBorder borderRBT = new SourceGrid2.RectangleBorder();
            borderRBT.Top = new SourceGrid2.Border(Color.Black);
            borderRBT.Right = new SourceGrid2.Border(Color.Black);
            borderRBT.Bottom = new SourceGrid2.Border(Color.Black);

            SourceGrid2.RectangleBorder borderRBT2 = new SourceGrid2.RectangleBorder();
            borderRBT2.Top = new SourceGrid2.Border(Color.Black);
            borderRBT2.Right = new SourceGrid2.Border(Color.Black);
            borderRBT2.Bottom = new SourceGrid2.Border(Color.Black, 2);

            SourceGrid2.RectangleBorder borderLRBT = new SourceGrid2.RectangleBorder();
            borderLRBT.Top = new SourceGrid2.Border(Color.Black);
            borderLRBT.Right = new SourceGrid2.Border(Color.Black);
            borderLRBT.Bottom = new SourceGrid2.Border(Color.Black);
            borderLRBT.Left = new SourceGrid2.Border(Color.Black);

            SourceGrid2.RectangleBorder borderLRBT2 = new SourceGrid2.RectangleBorder();
            borderLRBT2.Top = new SourceGrid2.Border(Color.Black);
            borderLRBT2.Right = new SourceGrid2.Border(Color.Black);
            borderLRBT2.Bottom = new SourceGrid2.Border(Color.Black, 2);
            borderLRBT2.Left = new SourceGrid2.Border(Color.Black);

            SourceGrid2.RectangleBorder borderLR = new SourceGrid2.RectangleBorder();
            borderLR.Left = new SourceGrid2.Border(Color.Black);
            borderLR.Right = new SourceGrid2.Border(Color.Black);
            borderLR.Bottom = new SourceGrid2.Border(Color.LightGray);

            SourceGrid2.RectangleBorder borderLRB = new SourceGrid2.RectangleBorder();
            borderLRB.Right = new SourceGrid2.Border(Color.Black);
            borderLRB.Bottom = new SourceGrid2.Border(Color.Black);
            borderLRB.Left = new SourceGrid2.Border(Color.Black);

            SourceGrid2.RectangleBorder borderLRB2 = new SourceGrid2.RectangleBorder();
            borderLRB2.Right = new SourceGrid2.Border(Color.Black);
            borderLRB2.Bottom = new SourceGrid2.Border(Color.Black, 2);
            borderLRB2.Left = new SourceGrid2.Border(Color.Black);

            SourceGrid2.RectangleBorder borderRBT3 = new SourceGrid2.RectangleBorder();
            borderRBT3.Top = new SourceGrid2.Border(Color.Black);
            borderRBT3.Right = new SourceGrid2.Border(Color.Black, 2);
            borderRBT3.Bottom = new SourceGrid2.Border(Color.Black);

            SourceGrid2.RectangleBorder borderRBT4 = new SourceGrid2.RectangleBorder();
            borderRBT4.Top = new SourceGrid2.Border(Color.Black);
            borderRBT4.Right = new SourceGrid2.Border(Color.Black, 2);
            borderRBT4.Bottom = new SourceGrid2.Border(Color.Black, 2);

            SourceGrid2.RectangleBorder borderRB3 = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Black, 2), 
                new SourceGrid2.Border(Color.Black));
            SourceGrid2.RectangleBorder borderRB4 = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Black, 2),
                new SourceGrid2.Border(Color.Black, 2));
            
            Font fontBold = new Font("Tahoma", 8, FontStyle.Bold);
            Font fontNormal = new Font("Tahoma", 8);

            int rowIndex = 3;
            SourceGrid2.Cells.Real.Cell cell = null;

            for (int j = 0; j < keys.Count; j++)
            {
                string key = keys[j];
                int totalMorning = 0;
                int totalAfternoon = 0;
                int totalEvening = 0;

                List<DataRow> bookingList = htBooking[key] as List<DataRow>;
                List<DataRow> bloodTakingList = htBloodTaking[key] as List<DataRow>;
                int bookingCount = bookingList != null ? bookingList.Count : 0;
                int bloodTakingCount = bloodTakingList != null ? bloodTakingList.Count : 0;
                int count = bookingCount > bloodTakingCount ? bookingCount : bloodTakingCount;
                DateTime bookingDate;
                if (bookingList != null) bookingDate = Convert.ToDateTime(bookingList[0]["BookingDate"]);
                else bookingDate = Convert.ToDateTime(bloodTakingList[0]["BookingDate"]);

                Color foreColor = bookingDate.DayOfWeek == DayOfWeek.Saturday ? Color.Red : Color.Black;
                
                for (int i = 0; i < count; i++)
                {
                    if (i == 0)
                    {
                        //Day
                        cell = NewCell(GetDayOfWeek(bookingDate), Color.White, foreColor,
                            ContentAlignment.MiddleCenter, fontBold, false, string.Empty);

                        if (j < keys.Count - 1)
                            cell.Border = borderRT;
                        else
                            cell.Border = i < count - 1 ? borderRT : borderRBT2;

                        dgBooking[rowIndex, 0] = cell;

                        //Date
                        cell = NewCell(bookingDate.ToString("dd/MM/yyyy"), Color.White, foreColor,
                            ContentAlignment.MiddleCenter, fontBold, false, string.Empty);

                        if (j < keys.Count - 1)
                            cell.Border = borderRT;
                        else
                            cell.Border = i < count - 1 ? borderRT : borderRBT2;

                        dgBooking[rowIndex, 1] = cell;
                    }
                    else
                    {
                        cell = NewCell(string.Empty, Color.White, foreColor,
                            ContentAlignment.MiddleCenter, fontBold, false, string.Empty);
                        if (j < keys.Count - 1)
                            cell.Border = i < count - 1 ? borderR : borderRB;
                        else
                            cell.Border = i < count - 1 ? borderR : borderRB2;

                        dgBooking[rowIndex, 0] = cell;

                        cell = NewCell(string.Empty, Color.White, foreColor,
                            ContentAlignment.MiddleCenter, fontBold, false, string.Empty);
                        if (j < keys.Count - 1)
                            cell.Border = i < count - 1 ? borderR : borderRB;
                        else
                            cell.Border = i < count - 1 ? borderR : borderRB2;

                        dgBooking[rowIndex, 1] = cell;
                    }

                    string company = string.Empty;
                    int morningCount = 0;
                    int afternoonCount = 0;
                    int eveningCount = 0;

                    //Booking Monitor
                    if (i < bookingCount)
                    {
                        company = bookingList[i]["Company"].ToString();
                        morningCount = Convert.ToInt32(bookingList[i]["MorningCount"]);
                        afternoonCount = Convert.ToInt32(bookingList[i]["AfternoonCount"]);
                        eveningCount = Convert.ToInt32(bookingList[i]["EveningCount"]);

                        totalMorning += morningCount;
                        totalAfternoon += afternoonCount;
                        totalEvening += eveningCount;
                    }

                    //Company
                    cell = NewCell(company, Color.White, foreColor, ContentAlignment.MiddleLeft, fontNormal, false, string.Empty);
                    if (j < keys.Count - 1) cell.Border = i == 0 ? borderRBT : borderRB;
                    else if (i == 0) cell.Border = i == count - 1 ? borderRBT2 : borderRBT;
                    else cell.Border = i == count - 1 ? borderRB2 : borderRB;

                    dgBooking[rowIndex, 2] = cell;

                    //Morning
                    cell = NewCell(morningCount == 0 ? string.Empty : morningCount.ToString(), Color.White, foreColor, 
                        ContentAlignment.MiddleRight, fontNormal, false, string.Empty);
                    if (j < keys.Count - 1) cell.Border = i == 0 ? borderRBT : borderRB;
                    else if (i == 0) cell.Border = i == count - 1 ? borderRBT2 : borderRBT;
                    else cell.Border = i == count - 1 ? borderRB2 : borderRB;
                    dgBooking[rowIndex, 3] = cell;

                    //Afternoon
                    cell = NewCell(afternoonCount == 0 ? string.Empty : afternoonCount.ToString(), Color.White, foreColor,
                        ContentAlignment.MiddleRight, fontNormal, false, string.Empty);
                    if (j < keys.Count - 1) cell.Border = i == 0 ? borderRBT : borderRB;
                    else if (i == 0) cell.Border = i == count - 1 ? borderRBT2 : borderRBT;
                    else cell.Border = i == count - 1 ? borderRB2 : borderRB;
                    dgBooking[rowIndex, 4] = cell;

                    //Evening
                    cell = NewCell(eveningCount == 0 ? string.Empty : eveningCount.ToString(), Color.White, foreColor,
                        ContentAlignment.MiddleRight, fontNormal, false, string.Empty);
                    if (j < keys.Count - 1) cell.Border = i == 0 ? borderRBT : borderRB;
                    else if (i == 0) cell.Border = i == count - 1 ? borderRBT2 : borderRBT;
                    else cell.Border = i == count - 1 ? borderRB2 : borderRB;
                    dgBooking[rowIndex, 5] = cell;

                    //Empty Cell
                    cell = NewCell(string.Empty, Color.White, foreColor,
                        ContentAlignment.MiddleRight, fontNormal, false, string.Empty);
                    cell.Border = borderR;
                    dgBooking[rowIndex, 9] = cell;

                    //Blood Taking
                    company = string.Empty;
                    int pax = 0;
                    string time = string.Empty;
                    string sales = string.Empty;
                    if (i < bloodTakingCount)
                    {
                        company = bloodTakingList[i]["Company"].ToString();
                        pax = Convert.ToInt32(bloodTakingList[i]["Pax"]);
                        time = Convert.ToDateTime(bloodTakingList[i]["BookingDate"]).ToString("hh:mm tt");
                        sales = bloodTakingList[i]["Sales"].ToString();
                    }

                    //Company
                    cell = NewCell(company, Color.White, foreColor, ContentAlignment.MiddleLeft, fontNormal, false, string.Empty);
                    if (j < keys.Count - 1) cell.Border = i == 0 ? borderLRBT : borderLRB;
                    else if (i == 0) cell.Border = i == count - 1 ? borderLRBT2 : borderLRBT;
                    else cell.Border = i == count - 1 ? borderLRB2 : borderLRB;

                    dgBooking[rowIndex, 10] = cell;

                    //Pax
                    cell = NewCell(pax == 0 ? string.Empty : pax.ToString(), Color.White, foreColor,
                        ContentAlignment.MiddleRight, fontNormal, false, string.Empty);
                    if (j < keys.Count - 1) cell.Border = i == 0 ? borderRBT : borderRB;
                    else if (i == 0) cell.Border = i == count - 1 ? borderRBT2 : borderRBT;
                    else cell.Border = i == count - 1 ? borderRB2 : borderRB;
                    dgBooking[rowIndex, 11] = cell;

                    //Time
                    cell = NewCell(time, Color.White, foreColor,
                        ContentAlignment.MiddleCenter, fontNormal, false, string.Empty);
                    if (j < keys.Count - 1) cell.Border = i == 0 ? borderRBT : borderRB;
                    else if (i == 0) cell.Border = i == count - 1 ? borderRBT2 : borderRBT;
                    else cell.Border = i == count - 1 ? borderRB2 : borderRB;
                    dgBooking[rowIndex, 12] = cell;

                    //Sales
                    cell = NewCell(sales, Color.White, foreColor,
                        ContentAlignment.MiddleLeft, fontNormal, false, string.Empty);
                    if (j < keys.Count - 1) cell.Border = i == 0 ? borderRBT3 : borderRB3;
                    else if (i == 0) cell.Border = i == count - 1 ? borderRBT4 : borderRBT3;
                    else cell.Border = i == count - 1 ? borderRB4 : borderRB3;
                    dgBooking[rowIndex, 13] = cell;

                    rowIndex++;
                }

                //Total Morning
                cell = NewCell(totalMorning == 0 ? string.Empty : totalMorning.ToString(), Color.White, foreColor,
                    ContentAlignment.MiddleRight, fontNormal, false, string.Empty);
                cell.Border = j < keys.Count - 1 ? borderRBT : borderRBT2;
                dgBooking[rowIndex - count, 6] = cell;
                dgBooking[rowIndex - count, 6].RowSpan = count;

                //Total Afternoon
                cell = NewCell(totalAfternoon == 0 ? string.Empty : totalAfternoon.ToString(), Color.White, foreColor,
                    ContentAlignment.MiddleRight, fontNormal, false, string.Empty);
                cell.Border = j < keys.Count - 1 ? borderRBT : borderRBT2;
                dgBooking[rowIndex - count, 7] = cell;
                dgBooking[rowIndex - count, 7].RowSpan = count;

                //Total Evening
                cell = NewCell(totalEvening == 0 ? string.Empty : totalEvening.ToString(), Color.White, foreColor,
                    ContentAlignment.MiddleRight, fontNormal, false, string.Empty);
                cell.Border = j < keys.Count - 1 ? borderRBT3 : borderRBT4;
                dgBooking[rowIndex - count, 8] = cell;
                dgBooking[rowIndex - count, 8].RowSpan = count;

                //Saturday
                if (bookingDate.DayOfWeek == DayOfWeek.Saturday && j < keys.Count - 1)
                {
                    cell = NewCell(string.Empty, Color.White, foreColor,
                            ContentAlignment.MiddleCenter, fontBold, false, string.Empty);
                    cell.Border = borderTB;
                    dgBooking[rowIndex, 0] = cell;
                    dgBooking[rowIndex, 0].ColumnSpan = 9;

                    cell = NewCell(string.Empty, Color.White, foreColor,
                            ContentAlignment.MiddleCenter, fontBold, false, string.Empty);
                    dgBooking[rowIndex, 9] = cell;

                    cell = NewCell(string.Empty, Color.White, foreColor,
                            ContentAlignment.MiddleCenter, fontBold, false, string.Empty);
                    cell.Border = borderTB;
                    dgBooking[rowIndex, 10] = cell;
                    dgBooking[rowIndex, 10].ColumnSpan = 4;

                    rowIndex++;
                }
            }
        }

        private string GetDayOfWeek(DateTime dt)
        {
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    return "Thứ 6";
                case DayOfWeek.Monday:
                    return "Thứ 2";
                case DayOfWeek.Saturday:
                    return "Thứ 7";
                case DayOfWeek.Sunday:
                    return "CN";
                case DayOfWeek.Thursday:
                    return "Thứ 5";
                case DayOfWeek.Tuesday:
                    return "Thứ 3";
                case DayOfWeek.Wednesday:
                    return "Thứ 4";
            }

            return string.Empty;
        }

        private void InitHeader()
        {
            if (dgBooking[0, 0] != null) return;
            Font font = new System.Drawing.Font("Tohama", 9, FontStyle.Bold);
            SourceGrid2.RectangleBorder borderRB = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Black), new SourceGrid2.Border(Color.Black));
            SourceGrid2.RectangleBorder borderRB2 = new SourceGrid2.RectangleBorder(new SourceGrid2.Border(Color.Black, 2), new SourceGrid2.Border(Color.Black));

            SourceGrid2.RectangleBorder borderTRB = new SourceGrid2.RectangleBorder();
            borderTRB.Top = new SourceGrid2.Border(Color.Black);
            borderTRB.Right = new SourceGrid2.Border(Color.Black);
            borderTRB.Bottom = new SourceGrid2.Border(Color.Black);

            SourceGrid2.RectangleBorder borderTRB2 = new SourceGrid2.RectangleBorder();
            borderTRB2.Top = new SourceGrid2.Border(Color.Black);
            borderTRB2.Right = new SourceGrid2.Border(Color.Black, 2);
            borderTRB2.Bottom = new SourceGrid2.Border(Color.Black);

            SourceGrid2.RectangleBorder borderLTRB = new SourceGrid2.RectangleBorder();
            borderLTRB.Bottom = new SourceGrid2.Border(Color.Black);
            borderLTRB.Left = new SourceGrid2.Border(Color.Black);
            borderLTRB.Top = new SourceGrid2.Border(Color.Black);
            borderLTRB.Right = new SourceGrid2.Border(Color.Black);

            SourceGrid2.RectangleBorder borderR = new SourceGrid2.RectangleBorder();
            borderR.Left = new SourceGrid2.Border(Color.LightGray);
            borderR.Right = new SourceGrid2.Border(Color.Black);
            borderR.Bottom = new SourceGrid2.Border(Color.LightGray);
                        
            SourceGrid2.Cells.Real.Cell cell = NewCell("BOOKING MONITOR", Color.White, Color.Red, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[0, 0] = cell;
            dgBooking[0, 0].ColumnSpan = 9;
            
            cell = NewCell(string.Empty, Color.White, Color.Red, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[0, 9] = cell;

            cell = NewCell("BLOOD TAKING", Color.White, Color.Red, ContentAlignment.MiddleCenter, font, false, string.Empty);
            dgBooking[0, 10] = cell;
            dgBooking[0, 10].ColumnSpan = 4;

            cell = NewCell("Day", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB;
            dgBooking[1, 0] = cell;
            dgBooking[1, 0].RowSpan = 2;

            cell = NewCell("Date", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB;
            dgBooking[1, 1] = cell;
            dgBooking[1, 1].RowSpan = 2;
            dgBooking.AutoSizeColumn(1, 75);

            cell = NewCell("Company", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB;
            dgBooking[1, 2] = cell;
            dgBooking[1, 2].RowSpan = 2;
            dgBooking.AutoSizeColumn(2, 170);

            cell = NewCell("Morning", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB;
            dgBooking[1, 3] = cell;
            dgBooking[1, 3].RowSpan = 2;
            dgBooking.AutoSizeColumn(3, 60);

            cell = NewCell("Afternoon", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB;
            dgBooking[1, 4] = cell;
            dgBooking[1, 4].RowSpan = 2;
            dgBooking.AutoSizeColumn(4, 60);

            cell = NewCell("Evening", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB;
            dgBooking[1, 5] = cell;
            dgBooking[1, 5].RowSpan = 2;
            dgBooking.AutoSizeColumn(5, 60);

            cell = NewCell("Total", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB2;
            dgBooking[1, 6] = cell;
            dgBooking[1, 6].ColumnSpan = 3;

            cell = NewCell("Morning", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderRB;
            dgBooking[2, 6] = cell;
            dgBooking.AutoSizeColumn(6, 60);

            cell = NewCell("Afternoon", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderRB;
            dgBooking[2, 7] = cell;
            dgBooking.AutoSizeColumn(7, 60);

            cell = NewCell("Evening", Color.Gray, Color.White, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderRB2;
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
            cell.Border = borderR;
            dgBooking[2, 9] = cell;

            cell = NewCell("Company", Color.Yellow, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderLTRB;
            dgBooking[2, 10] = cell;
            dgBooking.AutoSizeColumn(10, 170);

            cell = NewCell("Pax", Color.Yellow, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB;
            dgBooking[2, 11] = cell;
            dgBooking.AutoSizeColumn(11, 50);

            cell = NewCell("Time", Color.Yellow, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB;
            dgBooking[2, 12] = cell;
            dgBooking.AutoSizeColumn(12, 60);

            cell = NewCell("Sales", Color.Yellow, Color.Black, ContentAlignment.MiddleCenter, font, false, string.Empty);
            cell.Border = borderTRB;
            dgBooking[2, 13] = cell;
            dgBooking.AutoSizeColumn(13, 170);
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
