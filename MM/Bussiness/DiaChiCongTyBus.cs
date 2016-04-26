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
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Transactions;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class DiaChiCongTyBus : BusBase
    {
        public static Result GetDanhSachDiaChiCongTy()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM DiaChiCongTy WITH(NOLOCK) WHERE Status={0} ORDER BY MaCongTy", (byte)Status.Actived);
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

        public static Result DeleteDiaChiCongTy(List<string> keys)
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
                        DiaChiCongTy s = db.DiaChiCongTies.SingleOrDefault<DiaChiCongTy>(ss => ss.DiaChiCongTyGUID.ToString() == key);
                        if (s != null)
                        {
                            s.DeletedDate = DateTime.Now;
                            s.DeletedBy = Guid.Parse(Global.UserGUID);
                            s.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Mã công ty: '{1}', Địa chỉ: '{2}'\n",
                                s.DiaChiCongTyGUID.ToString(), s.MaCongTy, s.DiaChi);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa địa chỉ công ty";
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

        public static Result CheckMaCongTyExistCode(string diaChiCongTyGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                DiaChiCongTy symp = null;
                if (diaChiCongTyGUID == null || diaChiCongTyGUID == string.Empty)
                    symp = db.DiaChiCongTies.SingleOrDefault<DiaChiCongTy>(s => s.MaCongTy.ToLower() == code.ToLower());
                else
                    symp = db.DiaChiCongTies.SingleOrDefault<DiaChiCongTy>(s => s.MaCongTy.ToLower() == code.ToLower() &&
                                                                s.DiaChiCongTyGUID.ToString() != diaChiCongTyGUID);

                if (symp == null)
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

        public static Result GetDiaChiCongTy(string maCongTy)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                DiaChiCongTy diaChiCongTy = db.DiaChiCongTies.FirstOrDefault<DiaChiCongTy>(s => s.MaCongTy.ToLower() == maCongTy.ToLower());

                if (diaChiCongTy == null)
                    result.QueryResult = string.Empty;
                else
                    result.QueryResult = diaChiCongTy.DiaChi;
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

        public static Result InsertDiaChiCongTy(DiaChiCongTy symp)
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
                    if (symp.DiaChiCongTyGUID == null || symp.DiaChiCongTyGUID == Guid.Empty)
                    {
                        symp.DiaChiCongTyGUID = Guid.NewGuid();
                        db.DiaChiCongTies.InsertOnSubmit(symp);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Mã công ty: '{1}', Địa chỉ: '{2}''",
                               symp.DiaChiCongTyGUID.ToString(), symp.MaCongTy, symp.DiaChi);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm địa chỉ công ty";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        DiaChiCongTy symptom = db.DiaChiCongTies.SingleOrDefault<DiaChiCongTy>(s => s.DiaChiCongTyGUID.ToString() == symp.DiaChiCongTyGUID.ToString());
                        if (symptom != null)
                        {
                            symptom.MaCongTy = symp.MaCongTy;
                            symptom.DiaChi = symp.DiaChi;
                            symptom.CreatedDate = symp.CreatedDate;
                            symptom.CreatedBy = symp.CreatedBy;
                            symptom.UpdatedDate = symp.UpdatedDate;
                            symptom.UpdatedBy = symp.UpdatedBy;
                            symptom.DeletedDate = symp.DeletedDate;
                            symptom.DeletedBy = symp.DeletedBy;
                            symptom.Status = symp.Status;

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Mã công ty: '{1}', Địa chỉ: '{2}'",
                                   symptom.DiaChiCongTyGUID.ToString(), symptom.MaCongTy, symptom.DiaChi);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa địa chỉ công ty";
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
