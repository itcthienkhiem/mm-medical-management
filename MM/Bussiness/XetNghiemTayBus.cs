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
    public class XetNghiemTayBus : BusBase
    {
        public static Result GetXetNghiemList()
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE [Type] WHEN 'MienDich' THEN N'Miễn dịch' WHEN 'Urine' THEN N'Nước tiểu' WHEN 'Khac' THEN N'Khác' WHEN 'Haematology' THEN N'Huyết học' WHEN 'Biochemistry' THEN N'Sinh hóa' END LoaiXN FROM XetNghiem_Manual WITH(NOLOCK) WHERE Status={0} ORDER BY GroupID, [Order]", (byte)Status.Actived);
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

        public static Result GetChiTietXetNghiemList(string xetNghiem_ManualGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ChiTietXetNghiem_Manual WITH(NOLOCK) WHERE Status={0} AND XetNghiem_ManualGUID='{1}'",
                    (byte)Status.Actived, xetNghiem_ManualGUID);
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

        public static Result GetChiTietXetNghiemList2(string xetNghiem_ManualGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                List<ChiTietXetNghiem_Manual> ctxns = (from x in db.ChiTietXetNghiem_Manuals
                                                       where x.XetNghiem_ManualGUID.ToString() == xetNghiem_ManualGUID &&
                                                       x.Status == (byte)Status.Actived
                                                       select x).ToList();

                result.QueryResult = ctxns;
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

        public static Result GetDonViList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT DISTINCT DonVi FROM ChiTietXetNghiem_Manual WITH(NOLOCK)");
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

        public static Result GetNhomXetNghiemList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT DISTINCT GroupName, GroupID FROM XetNghiem_Manual WITH(NOLOCK) ORDER BY GroupID");
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

        public static Result CheckTenXetNghiemExist(string xetNghiem_ManualGUID, string tenXetNghiem, string groupName)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                XetNghiem_Manual xn = null;
                if (xetNghiem_ManualGUID == null || xetNghiem_ManualGUID == string.Empty)
                    xn = db.XetNghiem_Manuals.SingleOrDefault<XetNghiem_Manual>(x => x.Fullname.ToLower() == tenXetNghiem.ToLower() &&
                        x.GroupName.ToLower() == groupName.ToLower());
                else
                    xn = db.XetNghiem_Manuals.SingleOrDefault<XetNghiem_Manual>(x => x.Fullname.ToLower() == tenXetNghiem.ToLower() &&
                                                                x.XetNghiem_ManualGUID.ToString() != xetNghiem_ManualGUID &&
                                                                x.GroupName.ToLower() == groupName.ToLower());

                if (xn == null)
                    result.Error.Code = ErrorCode.NOT_EXIST;
                else
                    result.Error.Code = ErrorCode.EXIST;
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

        public static Result DeleteXetNghiem(List<string> keys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in keys)
                    {
                        XetNghiem_Manual xn = db.XetNghiem_Manuals.SingleOrDefault<XetNghiem_Manual>(x => x.XetNghiem_ManualGUID.ToString() == key);
                        if (xn != null)
                        {
                            xn.DeletedDate = DateTime.Now;
                            xn.DeletedBy = Guid.Parse(Global.UserGUID);
                            xn.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Tên xét nghiệm: '{1}', Loại xét nghiệm: '{2}'\n", 
                                xn.XetNghiem_ManualGUID.ToString(), xn.Fullname, xn.Type);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin xét nghiệm tay";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.None;
                    tk.ComputerName = Utility.GetDNSHostName();
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

        public static Result InsertXetNghiem(XetNghiem_Manual xetNghiem, List<ChiTietXetNghiem_Manual> ctxns)   
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
                    if (xetNghiem.XetNghiem_ManualGUID == null || xetNghiem.XetNghiem_ManualGUID == Guid.Empty)
                    {
                        xetNghiem.XetNghiem_ManualGUID = Guid.NewGuid();
                        db.XetNghiem_Manuals.InsertOnSubmit(xetNghiem);
                        db.SubmitChanges();

                        //Chi tiết xét nghiệm
                        foreach (ChiTietXetNghiem_Manual ctxn in ctxns)
                        {
                            ctxn.ChiTietXetNghiem_ManualGUID = Guid.NewGuid();
                            ctxn.XetNghiem_ManualGUID = xetNghiem.XetNghiem_ManualGUID;
                            ctxn.CreatedBy = Guid.Parse(Global.UserGUID);
                            ctxn.CreatedDate = DateTime.Now;
                            db.ChiTietXetNghiem_Manuals.InsertOnSubmit(ctxn);
                        }

                        //Tracking
                        desc += string.Format("GUID: '{0}', Tên xét nghiệm: '{1}', Loại xét nghiệm: '{2}'", 
                            xetNghiem.XetNghiem_ManualGUID.ToString(), xetNghiem.Fullname, xetNghiem.Type);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin xét nghiệm tay";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        XetNghiem_Manual xn = db.XetNghiem_Manuals.SingleOrDefault<XetNghiem_Manual>(x => x.XetNghiem_ManualGUID == xetNghiem.XetNghiem_ManualGUID);
                        if (xn != null)
                        {
                            xn.TenXetNghiem = xetNghiem.TenXetNghiem;
                            xn.Fullname = xetNghiem.Fullname;
                            xn.Type = xetNghiem.Type;
                            xn.GroupID = xetNghiem.GroupID;
                            xn.Order = xetNghiem.Order;
                            xn.GroupName = xetNghiem.GroupName;
                            xn.UpdatedDate = DateTime.Now;
                            xn.UpdatedBy = Guid.Parse(Global.UserGUID);
                            xn.Status = xetNghiem.Status;

                            //Update chi tiết xét nghiệm
                            var chiTietXetNghiemList = xn.ChiTietXetNghiem_Manuals;
                            foreach (ChiTietXetNghiem_Manual ctxn in chiTietXetNghiemList)
                            {
                                ctxn.UpdatedDate = DateTime.Now;
                                ctxn.UpdatedBy = Guid.Parse(Global.UserGUID);
                                ctxn.Status = (byte)Status.Deactived;
                            }

                            db.SubmitChanges();

                            foreach (ChiTietXetNghiem_Manual ctxn in ctxns)
                            {
                                ChiTietXetNghiem_Manual ct = db.ChiTietXetNghiem_Manuals.SingleOrDefault<ChiTietXetNghiem_Manual>(c => c.DoiTuong == ctxn.DoiTuong &&
                                    c.XetNghiem_ManualGUID == xn.XetNghiem_ManualGUID);
                                if (ct == null)
                                {
                                    ctxn.ChiTietXetNghiem_ManualGUID = Guid.NewGuid();
                                    ctxn.XetNghiem_ManualGUID = xn.XetNghiem_ManualGUID;
                                    ctxn.CreatedBy = Guid.Parse(Global.UserGUID);
                                    ctxn.CreatedDate = DateTime.Now;
                                    db.ChiTietXetNghiem_Manuals.InsertOnSubmit(ctxn);
                                }
                                else
                                {
                                    ct.FromValue = ctxn.FromValue;
                                    ct.ToValue = ctxn.ToValue;
                                    ct.DonVi = ctxn.DonVi;
                                    ct.DoiTuong = ctxn.DoiTuong;
                                    ct.FromAge = ctxn.FromAge;
                                    ct.ToAge = ctxn.ToAge;
                                    ct.FromTime = ctxn.FromTime;
                                    ct.ToTime = ctxn.ToTime;
                                    ct.FromOperator = ctxn.FromOperator;
                                    ct.ToOperator = ctxn.ToOperator;
                                    ct.FromTimeOperator = ctxn.FromTimeOperator;
                                    ct.ToTimeOperator = ctxn.ToTimeOperator;
                                    ct.XValue = ctxn.XValue;
                                    ct.UpdatedBy = Guid.Parse(Global.UserGUID);
                                    ct.UpdatedDate = DateTime.Now;
                                    ct.Status = (byte)Status.Actived;
                                }
                            }

                            //Tracking
                            desc += string.Format("GUID: '{0}', Tên xét nghiệm: '{1}', Loại xét nghiệm: '{2}'",
                                xn.XetNghiem_ManualGUID.ToString(), xn.Fullname, xn.Type);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin xét nghiệm tay";
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

        public static Result GetDanhSachXetNghiemTheoNhom(string groupName)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                List<XetNghiem_Manual> xetNghiemList = (from x in db.XetNghiem_Manuals
                                                        where x.GroupName.ToLower() == groupName.ToLower() &&
                                                        x.Status == (byte)Status.Actived
                                                        orderby x.Order ascending
                                                        select x).ToList();
                result.QueryResult = xetNghiemList;
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
