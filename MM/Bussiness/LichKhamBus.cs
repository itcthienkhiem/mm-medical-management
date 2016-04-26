/* Copyright (c) 2016, Cocosoft Inc.
 All rights reserved.
 http://www.Cocosofttech.com

 This file is part of the LIS open source project.

 The LIS  open source project is free software: you can
 redistribute it and/or modify it under the terms of the GNU General Public
 License as published by the Free Software Foundation, either version 3 of the
 License, or (at your option) any later version.

 The ClearCanvas LIS open source project is distributed in the hope that it
 will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
 Public License for more details.

 You should have received a copy of the GNU General Public License along with
 the LIS open source project.  If not, see
 <http://www.gnu.org/licenses/>.
*/
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
    public class LichKhamBus : BusBase
    {
        public static Result GetLichKhamTheoThang(int thang, int nam)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                DateTime fromDate = new DateTime(nam, thang, 1, 0, 0, 0, 0);
                int daysInMonth = DateTime.DaysInMonth(nam, thang);
                DateTime toDate = new DateTime(nam, thang, daysInMonth, 23, 59, 59);

                List<LichKham> lichKhams = (from l in db.LichKhams
                                            where l.Ngay >= fromDate && l.Ngay <= toDate
                                            select l).ToList();

                result.QueryResult = lichKhams;
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

        public static Result InsertLichKham(LichKham lichKham)
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
                    if (lichKham.LichKhamGUID == null || lichKham.LichKhamGUID == Guid.Empty)
                    {
                        lichKham.LichKhamGUID = Guid.NewGuid();
                        db.LichKhams.InsertOnSubmit(lichKham);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Ngay: '{1}', Type: '{2}', Value: '{3}'",
                                lichKham.LichKhamGUID.ToString(), lichKham.Ngay.ToString("dd/MM/yyyy"), lichKham.Type, lichKham.Value);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin lịch khám";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        LichKham lk = db.LichKhams.SingleOrDefault<LichKham>(l => l.LichKhamGUID == lichKham.LichKhamGUID);
                        if (lk != null)
                        {
                            lk.Value = lichKham.Value;
                            lk.CreatedBy = lichKham.CreatedBy;
                            lk.CreatedDate = lichKham.CreatedDate;
                            lk.DeletedBy = lichKham.DeletedBy;
                            lk.DeletedDate = lichKham.DeletedDate;
                            lk.UpdatedBy = lichKham.UpdatedBy;
                            lk.UpdatedDate = lichKham.UpdatedDate;
                            db.SubmitChanges();

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Ngay: '{1}', Type: '{2}', Value: '{3}'",
                               lk.LichKhamGUID.ToString(), lk.Ngay.ToString("dd/MM/yyyy"), lk.Type, lk.Value);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin lịch khám";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);

                            db.SubmitChanges();
                        }
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
    }
}
