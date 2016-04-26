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
    public class TiemNguaBus : BusBase
    {
        public static Result GetDanhSachTiemNguaList(bool isFromTuNgayDenNgay, DateTime tuNgay, DateTime denNgay, string tenBenhNhan, bool isMaBenhNhan)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                if (isFromTuNgayDenNgay)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM TiemNguaView WITH(NOLOCK) WHERE Archived = 'False' AND Status = {0} AND ((Lan1 IS NOT NULL AND Lan1 BETWEEN '{1}' AND '{2}') OR (Lan2 IS NOT NULL AND Lan2 BETWEEN '{1}' AND '{2}') OR (Lan3 IS NOT NULL AND Lan3 BETWEEN '{1}' AND '{2}')) ORDER BY FullName",
                        (byte)Status.Actived, tuNgay.ToString("yyyy-MM-dd 00:00:00"), denNgay.ToString("yyyy-MM-dd 23:59:59"));
                }
                else
                {
                    if (tenBenhNhan.Trim() == string.Empty)
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM TiemNguaView WITH(NOLOCK) WHERE Archived = 'False' AND Status = {0} ORDER BY FullName", (byte)Status.Actived);
                    }
                    else //Bệnh nhân
                    {
                        if (!isMaBenhNhan)
                        {
                            query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM TiemNguaView WITH(NOLOCK) WHERE Archived = 'False' AND Status = {0} AND FullName LIKE N'%{1}%' ORDER BY FullName",
                                (byte)Status.Actived, tenBenhNhan);
                        }
                        else
                        {
                            query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM TiemNguaView WITH(NOLOCK) WHERE Archived = 'False' AND Status = {0} AND FileNum LIKE N'%{1}%' ORDER BY FullName",
                                (byte)Status.Actived, tenBenhNhan);
                        }
                    }
                }

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

        public static Result CheckAlert()
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT TOP 1 * FROM TiemNgua WITH(NOLOCK) WHERE Status = {0} AND ((Lan1 IS NOT NULL AND DATEDIFF(day, GetDate(), Lan1) >= 0 AND DATEDIFF(day, GetDate(), Lan1) <= {1} AND (DaChich1 IS NULL OR DaChich1 = 0)) OR (Lan2 IS NOT NULL AND DATEDIFF(day, GetDate(), Lan2) >= 0 AND DATEDIFF(day, GetDate(), Lan2) <= {1} AND (DaChich2 IS NULL OR DaChich2 = 0)) OR (Lan3 IS NOT NULL AND DATEDIFF(day, GetDate(), Lan3) >= 0 AND DATEDIFF(day, GetDate(), Lan3) <= {1}) AND (DaChich3 IS NULL OR DaChich3 = 0))",
                    (byte)Status.Actived, Global.AlertDays);

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

        public static Result DeleteTiemNgua(List<string> keys)
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
                        TiemNgua s = db.TiemNguas.SingleOrDefault<TiemNgua>(ss => ss.TiemNguaGUID.ToString() == key);
                        if (s != null)
                        {
                            s.DeletedDate = DateTime.Now;
                            s.DeletedBy = Guid.Parse(Global.UserGUID);
                            s.Status = (byte)Status.Deactived;

                            string lan1 = string.Empty;
                            if (s.Lan1.HasValue) lan1 = s.Lan1.Value.ToString("dd/MM/yyyy HH:mm:ss");

                            string lan2 = string.Empty;
                            if (s.Lan2.HasValue) lan2 = s.Lan2.Value.ToString("dd/MM/yyyy HH:mm:ss");

                            string lan3 = string.Empty;
                            if (s.Lan3.HasValue) lan3 = s.Lan3.Value.ToString("dd/MM/yyyy HH:mm:ss");

                            desc += string.Format("- GUID: '{0}', Tên bệnh nhân: '{1}', Ngày sinh: '{2}', Số điện thoại: '{3}', Lần 1: '{4}', Lần 2: '{5}', Lần 3: '{6}'\n",
                                s.TiemNguaGUID, s.Patient.Contact.FullName, s.Patient.Contact.DobStr, s.Patient.Contact.Mobile, lan1, lan2, lan3);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin tiêm ngừa";
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

        public static Result InsertTiemNgua(TiemNgua tiemNgua)
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
                    if (tiemNgua.TiemNguaGUID == null || tiemNgua.TiemNguaGUID == Guid.Empty)
                    {
                        tiemNgua.TiemNguaGUID = Guid.NewGuid();
                        db.TiemNguas.InsertOnSubmit(tiemNgua);
                        db.SubmitChanges();

                        //Tracking
                        string lan1 = string.Empty;
                        if (tiemNgua.Lan1.HasValue) lan1 = tiemNgua.Lan1.Value.ToString("dd/MM/yyyy HH:mm:ss");

                        string lan2 = string.Empty;
                        if (tiemNgua.Lan2.HasValue) lan2 = tiemNgua.Lan2.Value.ToString("dd/MM/yyyy HH:mm:ss");

                        string lan3 = string.Empty;
                        if (tiemNgua.Lan3.HasValue) lan3 = tiemNgua.Lan3.Value.ToString("dd/MM/yyyy HH:mm:ss");

                        desc += string.Format("- GUID: '{0}', Tên bệnh nhân: '{1}', Ngày sinh: '{2}', Số điện thoại: '{3}', Lần 1: '{4}', Lần 2: '{5}', Lần 3: '{6}'",
                            tiemNgua.TiemNguaGUID, tiemNgua.Patient.Contact.FullName, tiemNgua.Patient.Contact.DobStr, tiemNgua.Patient.Contact.Mobile, lan1, lan2, lan3);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin tiêm ngừa";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        TiemNgua srv = db.TiemNguas.SingleOrDefault<TiemNgua>(s => s.TiemNguaGUID.ToString() == tiemNgua.TiemNguaGUID.ToString());
                        if (srv != null)
                        {
                            srv.PatientGUID = tiemNgua.PatientGUID;
                            srv.Lan1 = tiemNgua.Lan1;
                            srv.Lan2 = tiemNgua.Lan2;
                            srv.Lan3 = tiemNgua.Lan3;
                            srv.DaChich1 = tiemNgua.DaChich1;
                            srv.DaChich2 = tiemNgua.DaChich2;
                            srv.DaChich3 = tiemNgua.DaChich3;
                            srv.CreatedDate = tiemNgua.CreatedDate;
                            srv.CreatedBy = tiemNgua.CreatedBy;
                            srv.UpdatedDate = tiemNgua.UpdatedDate;
                            srv.UpdatedBy = tiemNgua.UpdatedBy;
                            srv.DeletedDate = tiemNgua.DeletedDate;
                            srv.DeletedBy = tiemNgua.DeletedBy;
                            srv.Status = tiemNgua.Status;

                            //Tracking
                            string lan1 = string.Empty;
                            if (srv.Lan1.HasValue) lan1 = srv.Lan1.Value.ToString("dd/MM/yyyy HH:mm:ss");

                            string lan2 = string.Empty;
                            if (srv.Lan2.HasValue) lan2 = srv.Lan2.Value.ToString("dd/MM/yyyy HH:mm:ss");

                            string lan3 = string.Empty;
                            if (srv.Lan3.HasValue) lan3 = srv.Lan3.Value.ToString("dd/MM/yyyy HH:mm:ss");

                            PatientView patient = db.PatientViews.FirstOrDefault(p => p.PatientGUID == srv.PatientGUID);
                            
                            desc += string.Format("- GUID: '{0}', Tên bệnh nhân: '{1}', Ngày sinh: '{2}', Số điện thoại: '{3}', Lần 1: '{4}', Lần 2: '{5}', Lần 3: '{6}'",
                                srv.TiemNguaGUID, patient.FullName, patient.DobStr, patient.Mobile, lan1, lan2, lan3);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin tiêm ngừa";
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
