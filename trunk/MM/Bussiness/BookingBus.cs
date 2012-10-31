using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Linq;
using System.Text;
using System.Transactions;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class BookingBus : BusBase
    {
        public static Result GetBookingList(DateTime fromDate, DateTime toDate)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT * FROM BookingView WITH(NOLOCK) WHERE Status={0} AND BookingDate BETWEEN '{1}' AND '{2}' ORDER BY BookingDate",
                        (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"));
                
                return ExcuteQuery(query);
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result GetCompanyList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT DISTINCT Company FROM Booking WITH(NOLOCK) WHERE Status={0}", (byte)Status.Actived);
                return ExcuteQuery(query);
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }

            return result;
        }

        public static Result DeleteBooking(List<string> keys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    int index = 0;
                    foreach (string key in keys)
                    {
                        Booking bk = db.Bookings.SingleOrDefault<Booking>(b => b.BookingGUID.ToString() == key);
                        if (bk != null)
                        {
                            bk.DeletedDate = DateTime.Now;
                            bk.DeletedBy = Guid.Parse(Global.UserGUID);
                            bk.Status = (byte)Status.Deactived;

                            //Tracking
                            string docStaffGUID = string.Empty;
                            string nguoiTao = "Admin";
                            DocStaffView docStaff = db.DocStaffViews.SingleOrDefault<DocStaffView>(d => d.DocStaffGUID == bk.CreatedBy.Value);
                            if (docStaff != null)
                            {
                                docStaffGUID = docStaff.DocStaffGUID.ToString();
                                nguoiTao = docStaff.FullName;
                            }

                            if (bk.BookingType == (byte)BookingType.Monitor)
                            {
                                desc += string.Format("- GUID: '{0}', BookingDate: '{1}', Company: '{2}', Morning: '{3}', Afternoon: '{4}', Evening: '{5}', OwnerGUID: '{6}', Owner: '{7}', BookingType: '{8}', In/Out: '{9}'\n",
                                        bk.BookingGUID.ToString(), bk.BookingDate.ToString("dd/MM/yyyy"), bk.Company,
                                        bk.MorningCount, bk.AfternoonCount, bk.EveningCount, docStaffGUID, nguoiTao, "Booking Monitor", bk.InOut);
                            }
                            else
                            {
                                desc += string.Format("- GUID: '{0}', BookingDate: '{1}', Company: '{2}', Pax: '{3}', SaleGUID: '{4}', Sales: '{5}', BookingType: '{6}', In/Out: '{7}'\n",
                                        bk.BookingGUID.ToString(), bk.BookingDate.ToString("dd/MM/yyyy hh:mm tt"), bk.Company,
                                        bk.Pax, docStaffGUID, nguoiTao, "Blood Taking", bk.InOut);
                            }
                        }

                        index++;
                    }

                    //Tracking
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Add;
                    tk.Action = "Xóa lịch hẹn";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.None;
                    db.Trackings.InsertOnSubmit(tk);

                    db.SubmitChanges();
                    t.Complete();
                }
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result InsertBooking(List<Booking> bookingList)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    foreach (Booking booking in bookingList)
                    {
                        booking.BookingGUID = Guid.NewGuid();
                        db.Bookings.InsertOnSubmit(booking);
                        db.SubmitChanges();

                        //Tracking
                        string docStaffGUID = string.Empty;
                        string nguoiTao = "Admin";
                        DocStaffView docStaff = db.DocStaffViews.SingleOrDefault<DocStaffView>(d => d.DocStaffGUID == booking.CreatedBy.Value);
                        if (docStaff != null)
                        {
                            docStaffGUID = docStaff.DocStaffGUID.ToString();
                            nguoiTao = docStaff.FullName;
                        }

                        if (booking.BookingType == (byte)BookingType.Monitor)
                        {
                            desc += string.Format("- GUID: '{0}', BookingDate: '{1}', Company: '{2}', Morning: '{3}', Afternoon: '{4}', Evening: '{5}', OwnerGUID: '{6}', Owner: '{7}', BookingType: '{8}', In/Out: '{9}'\n",
                                    booking.BookingGUID.ToString(), booking.BookingDate.ToString("dd/MM/yyyy"), booking.Company,
                                    booking.MorningCount, booking.AfternoonCount, booking.EveningCount, docStaffGUID, nguoiTao, "Booking Monitor", booking.InOut);
                        }
                        else
                        {
                            desc += string.Format("- GUID: '{0}', BookingDate: '{1}', Company: '{2}', Pax: '{3}', SaleGUID: '{4}', Sales: '{5}', BookingType: '{6}', In/Out: '{7}'",
                                    booking.BookingGUID.ToString(), booking.BookingDate.ToString("dd/MM/yyyy hh:mm tt"), booking.Company,
                                    booking.Pax, docStaffGUID, nguoiTao, "Blood Taking", booking.InOut);
                        }
                        

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm lịch hẹn";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);    
                    }

                    db.SubmitChanges();
                    
                    t.Complete();
                }

            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result UpdateBooking(Booking booking)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    Booking bk = db.Bookings.SingleOrDefault<Booking>(b => b.BookingGUID == booking.BookingGUID);
                    if (bk != null)
                    {
                        bk.BookingDate = booking.BookingDate;
                        bk.Company = booking.Company;
                        bk.MorningCount = booking.MorningCount;
                        bk.AfternoonCount = booking.AfternoonCount;
                        bk.EveningCount = booking.EveningCount;
                        bk.Pax = booking.Pax;
                        bk.CreatedDate = booking.CreatedDate;
                        bk.CreatedBy = booking.CreatedBy;
                        bk.UpdatedDate = booking.UpdatedDate;
                        bk.UpdatedBy = booking.UpdatedBy;
                        bk.DeletedDate = booking.DeletedDate;
                        bk.DeletedBy = booking.DeletedBy;
                        bk.Status = booking.Status;
                        bk.BookingType = booking.BookingType;
                        bk.InOut = booking.InOut;

                        //Tracking
                        string docStaffGUID = string.Empty;
                        string nguoiTao = "Admin";
                        DocStaffView docStaff = db.DocStaffViews.SingleOrDefault<DocStaffView>(d => d.DocStaffGUID == bk.CreatedBy.Value);
                        if (docStaff != null)
                        {
                            docStaffGUID = docStaff.DocStaffGUID.ToString();
                            nguoiTao = docStaff.FullName;
                        }

                        if (bk.BookingType == (byte)BookingType.Monitor)
                        {
                            desc += string.Format("- GUID: '{0}', BookingDate: '{1}', Company: '{2}', Morning: '{3}', Afternoon: '{4}', Evening: '{5}', OwnerGUID: '{6}', Owner: '{7}', BookingType: '{8}', In/Out: '{9}'",
                                    bk.BookingGUID.ToString(), bk.BookingDate.ToString("dd/MM/yyyy"), bk.Company,
                                    bk.MorningCount, bk.AfternoonCount, bk.EveningCount, docStaffGUID, nguoiTao, "Booking Monitor", bk.InOut);
                        }
                        else
                        {
                            desc += string.Format("- GUID: '{0}', BookingDate: '{1}', Company: '{2}', Pax: '{3}', SaleGUID: '{4}', Sales: '{5}', BookingType: '{6}', In/Out: '{7}'",
                                    bk.BookingGUID.ToString(), bk.BookingDate.ToString("dd/MM/yyyy hh:mm tt"), bk.Company,
                                    bk.Pax, docStaffGUID, nguoiTao, "Blood Taking", bk.InOut);
                        }


                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Sửa lịch hẹn";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }

                    t.Complete();
                }

            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }

        public static Result GetBooking(DateTime ngay)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                List<Booking> bookingList = (from b in db.Bookings
                                             where b.BookingDate.Year == ngay.Year && b.BookingDate.Month == ngay.Month &&
                                                 b.BookingDate.Day == ngay.Day && b.BookingType == (byte)BookingType.Monitor && 
                                                 b.Status == (byte)Status.Actived && b.InOut == "IN"
                                             select b).ToList();

                result.QueryResult = bookingList;
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                result.Error.Code = (se.Message.IndexOf("Timeout expired") >= 0) ? ErrorCode.SQL_QUERY_TIMEOUT : ErrorCode.INVALID_SQL_STATEMENT;
                result.Error.Description = se.ToString();
            }
            catch (Exception e)
            {
                result.Error.Code = ErrorCode.UNKNOWN_ERROR;
                result.Error.Description = e.ToString();
            }
            finally
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }

            return result;
        }
    }
}
