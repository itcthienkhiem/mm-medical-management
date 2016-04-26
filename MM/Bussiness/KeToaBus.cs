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
    public class KeToaBus : BusBase
    {
        public static Result GetToaThuocList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ToaThuocView WITH(NOLOCK) WHERE Status={0} ORDER BY NgayKham DESC",
                        (byte)Status.Actived);
                

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

        public static Result GetToaThuocTrongNgayList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ToaThuocView WITH(NOLOCK) WHERE Status={0} AND IsWarning=1 AND NgayKham BETWEEN '{1}' AND '{2}' ORDER BY NgayKham DESC",
                        (byte)Status.Actived, DateTime.Now.ToString("yyyy-MM-dd 00:00:00"), DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));

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

        public static Result GetToaThuocList(bool isAll, DateTime fromDate, DateTime toDate, string tenBenhNhan)
        {
            Result result = null;

            try
            {
                string query = string.Empty;

                if (isAll)
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ToaThuocView WITH(NOLOCK) WHERE Status={0} AND TenBenhNhan LIKE N'%{1}%' ORDER BY NgayKham DESC",
                        (byte)Status.Actived, tenBenhNhan);
                else
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ToaThuocView WITH(NOLOCK) WHERE Status={0} AND NgayKham BETWEEN '{1}' AND '{2}' AND TenBenhNhan LIKE N'%{3}%' ORDER BY NgayKham DESC",
                        (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), tenBenhNhan);

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

        public static Result GetToaThuocList(string patientGUID, string docStaffGUID, bool isAll, DateTime fromDate, DateTime toDate)
        {
            Result result = null;

            try
            {
                string query = string.Empty;

                if (isAll)
                {
                    //if (docStaffGUID == Guid.Empty.ToString())
                    //    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ToaThuocView WITH(NOLOCK) WHERE Status={0} AND BenhNhan='{1}' ORDER BY NgayKham DESC",
                    //    (byte)Status.Actived, patientGUID);
                    //else
                    //    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ToaThuocView WITH(NOLOCK) WHERE Status={0} AND BenhNhan='{1}' AND BacSiKeToa='{2}' ORDER BY NgayKham DESC",
                    //    (byte)Status.Actived, patientGUID, docStaffGUID);

                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ToaThuocView WITH(NOLOCK) WHERE Status={0} AND BenhNhan='{1}' ORDER BY NgayKham DESC",
                        (byte)Status.Actived, patientGUID);
                }
                else
                {
                    //if (docStaffGUID == Guid.Empty.ToString())
                    //    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ToaThuocView WITH(NOLOCK) WHERE Status={0} AND BenhNhan='{1}' AND NgayKham BETWEEN '{2}' AND '{3}' ORDER BY NgayKham DESC",
                    //    (byte)Status.Actived, patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    //else
                    //    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ToaThuocView WITH(NOLOCK) WHERE Status={0} AND BenhNhan='{1}' AND BacSiKeToa='{2}' AND NgayKham BETWEEN '{3}' AND '{4}' ORDER BY NgayKham DESC",
                    //    (byte)Status.Actived, patientGUID, docStaffGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"));

                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ToaThuocView WITH(NOLOCK) WHERE Status={0} AND BenhNhan='{1}' AND NgayKham BETWEEN '{2}' AND '{3}' ORDER BY NgayKham DESC",
                        (byte)Status.Actived, patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"));
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

        public static Result GetChiTietToaThuocList(string toaThuocGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ChiTietToaThuocView WITH(NOLOCK) WHERE ThuocStatus={0} AND ChiTietToaThuocStatus={0} AND ToaThuocGUID='{1}' ORDER BY TenThuoc",
                    (byte)Status.Actived, toaThuocGUID);
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

        public static Result GetChiTietToaThuocListWithoutThuocNgoai(string toaThuocGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ChiTietToaThuocView WITH(NOLOCK) WHERE ThuocStatus={0} AND ChiTietToaThuocStatus={0} AND ToaThuocGUID='{1}' AND ThuocGUID IS NOT NULL ORDER BY TenThuoc",
                    (byte)Status.Actived, toaThuocGUID);
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

        public static Result GetToaThuoc(string toaThuocGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                ToaThuocView toaThuoc = db.ToaThuocViews.SingleOrDefault<ToaThuocView>(t => t.ToaThuocGUID.ToString() == toaThuocGUID);
                result.QueryResult = toaThuoc;
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

        public static Result GetToaThuocCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM ToaThuoc WITH(NOLOCK)";
                result = ExcuteQuery(query);
                if (result.IsOK)
                {
                    DataTable dt = result.QueryResult as DataTable;
                    if (dt != null && dt.Rows.Count > 0)
                        result.QueryResult = Convert.ToInt32(dt.Rows[0][0]);
                    else result.QueryResult = 0;
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

            return result;
        }

        public static Result DeleteToaThuoc(List<string> toaThuocKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in toaThuocKeys)
                    {
                        ToaThuoc toaThuoc = db.ToaThuocs.SingleOrDefault<ToaThuoc>(tt => tt.ToaThuocGUID.ToString() == key);
                        if (toaThuoc != null)
                        {
                            toaThuoc.DeletedDate = DateTime.Now;
                            toaThuoc.DeletedBy = Guid.Parse(Global.UserGUID);
                            toaThuoc.Status = (byte)Status.Deactived;

                            string ngayTaiKhamStr = string.Empty;
                            if (toaThuoc.NgayTaiKham != null && toaThuoc.NgayTaiKham.HasValue)
                                ngayTaiKhamStr = toaThuoc.NgayTaiKham.Value.ToString("dd/MM/yyyy HH:mm:ss");

                            desc += string.Format("- GUID: '{0}', Mã toa thuốc: '{1}', Ngày khám: '{2}', Ngày tái khám: '{3}', Bác sĩ kê toa: '{4}', Bệnh nhân: '{5}', Chẩn đoán: '{6}', Lời dặn: '{7}'\n",
                                toaThuoc.ToaThuocGUID.ToString(), toaThuoc.MaToaThuoc, toaThuoc.NgayKham.Value.ToString("dd/MM/yyyy HH:mm:ss"), ngayTaiKhamStr, 
                                toaThuoc.DocStaff.Contact.FullName, toaThuoc.Patient.Contact.FullName, toaThuoc.ChanDoan, toaThuoc.Note);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin toa thuốc";
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

        public static Result TatCanhBaoToaThuoc(List<string> toaThuocKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in toaThuocKeys)
                    {
                        ToaThuoc toaThuoc = db.ToaThuocs.SingleOrDefault<ToaThuoc>(tt => tt.ToaThuocGUID.ToString() == key);
                        if (toaThuoc != null)
                        {
                            toaThuoc.UpdatedDate = DateTime.Now;
                            toaThuoc.UpdatedBy = Guid.Parse(Global.UserGUID);
                            toaThuoc.IsWarning = false;

                            string ngayTaiKhamStr = string.Empty;
                            if (toaThuoc.NgayTaiKham != null && toaThuoc.NgayTaiKham.HasValue)
                                ngayTaiKhamStr = toaThuoc.NgayTaiKham.Value.ToString("dd/MM/yyyy HH:mm:ss");

                            desc += string.Format("- GUID: '{0}', Mã toa thuốc: '{1}', Ngày khám: '{2}', Ngày tái khám: '{3}', Bác sĩ kê toa: '{4}', Bệnh nhân: '{5}', Chẩn đoán: '{6}', Lời dặn: '{7}'\n",
                                toaThuoc.ToaThuocGUID.ToString(), toaThuoc.MaToaThuoc, toaThuoc.NgayKham.Value.ToString("dd/MM/yyyy HH:mm:ss"), ngayTaiKhamStr,
                                toaThuoc.DocStaff.Contact.FullName, toaThuoc.Patient.Contact.FullName, toaThuoc.ChanDoan, toaThuoc.Note);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Edit;
                    tk.Action = "Tắt cảnh báo toa thuốc";
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

        public static Result CheckToaThuocExistCode(string toaThuocGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                ToaThuoc tt = null;
                if (toaThuocGUID == null || toaThuocGUID == string.Empty)
                    tt = db.ToaThuocs.SingleOrDefault<ToaThuoc>(o => o.MaToaThuoc.ToLower() == code.ToLower());
                else
                    tt = db.ToaThuocs.SingleOrDefault<ToaThuoc>(o => o.MaToaThuoc.ToLower() == code.ToLower() &&
                                                                o.ToaThuocGUID.ToString() != toaThuocGUID);

                if (tt == null)
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

        public static Result InsertToaThuoc(ToaThuoc toaThuoc, List<ChiTietToaThuoc> addedList, List<string> deletedKeys)
        {
            Result result = new Result();   
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope tnx = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (toaThuoc.ToaThuocGUID == null || toaThuoc.ToaThuocGUID == Guid.Empty)
                    {
                        toaThuoc.ToaThuocGUID = Guid.NewGuid();
                        toaThuoc.IsWarning = true;
                        db.ToaThuocs.InsertOnSubmit(toaThuoc);
                        db.SubmitChanges();

                        string ngayTaiKhamStr = string.Empty;
                        if (toaThuoc.NgayTaiKham != null && toaThuoc.NgayTaiKham.HasValue)
                            ngayTaiKhamStr = toaThuoc.NgayTaiKham.Value.ToString("dd/MM/yyyy HH:mm:ss");

                        desc += string.Format("- Toa thuốc: GUID: '{0}', Mã toa thuốc: '{1}', Ngày khám: '{2}', Ngày tái khám: '{3}', Bác sĩ kê toa: '{4}', Bệnh nhân: '{5}', Chẩn đoán: '{6}', Lời dặn: '{7}'\n",
                                toaThuoc.ToaThuocGUID.ToString(), toaThuoc.MaToaThuoc, toaThuoc.NgayKham.Value.ToString("dd/MM/yyyy HH:mm:ss"), ngayTaiKhamStr, 
                                toaThuoc.DocStaff.Contact.FullName, toaThuoc.Patient.Contact.FullName, toaThuoc.ChanDoan, toaThuoc.Note);

                        if (addedList != null && addedList.Count > 0)
                        {
                            desc += "- Chi tiết toa thuốc được thêm:\n";

                            //Chi tiet toa thuoc
                            foreach (ChiTietToaThuoc cttt in addedList)
                            {
                                cttt.ChiTietToaThuocGUID = Guid.NewGuid();
                                cttt.ToaThuocGUID = toaThuoc.ToaThuocGUID;
                                db.ChiTietToaThuocs.InsertOnSubmit(cttt);
                                db.SubmitChanges();

                                string tenThuoc = string.Empty;
                                if (cttt.Thuoc != null) tenThuoc = cttt.Thuoc.TenThuoc;
                                else tenThuoc = cttt.TenThuocNgoai;

                                desc += string.Format("  + GUID: '{0}', Thuốc: '{1}', Số lượng: '{2}'\n", cttt.ChiTietToaThuocGUID.ToString(),
                                    tenThuoc, cttt.SoLuong);
                            }
                        }

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin toa thuốc";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        ToaThuoc tt = db.ToaThuocs.SingleOrDefault<ToaThuoc>(o => o.ToaThuocGUID.ToString() == toaThuoc.ToaThuocGUID.ToString());
                        if (tt != null)
                        {
                            tt.MaToaThuoc = toaThuoc.MaToaThuoc;
                            tt.NgayKeToa = toaThuoc.NgayKeToa;
                            tt.NgayKham = toaThuoc.NgayKham;
                            tt.NgayTaiKham = toaThuoc.NgayTaiKham;
                            tt.BacSiKeToa = toaThuoc.BacSiKeToa;
                            tt.BenhNhan = toaThuoc.BenhNhan;
                            tt.ChanDoan = toaThuoc.ChanDoan;
                            tt.Note = toaThuoc.Note;
                            tt.Loai = toaThuoc.Loai;
                            tt.CreatedDate = toaThuoc.CreatedDate;
                            tt.CreatedBy = toaThuoc.CreatedBy;
                            tt.UpdatedDate = toaThuoc.UpdatedDate;
                            tt.UpdatedBy = toaThuoc.UpdatedBy;
                            tt.DeletedDate = toaThuoc.DeletedDate;
                            tt.DeletedBy = toaThuoc.DeletedBy;
                            tt.Status = toaThuoc.Status;
                            db.SubmitChanges();

                            string ngayTaiKhamStr = string.Empty;
                            if (tt.NgayTaiKham != null && tt.NgayTaiKham.HasValue)
                                ngayTaiKhamStr = tt.NgayTaiKham.Value.ToString("dd/MM/yyyy HH:mm:ss");

                            desc += string.Format("- Toa thuốc: GUID: '{0}', Mã toa thuốc: '{1}', Ngày khám: '{2}', Ngày tái khám: '{3}', Bác sĩ kê toa: '{4}', Bệnh nhân: '{5}', Chẩn đoán: '{6}', Lời dặn: '{7}'\n",
                                tt.ToaThuocGUID.ToString(), tt.MaToaThuoc, tt.NgayKham.Value.ToString("dd/MM/yyyy HH:mm:ss"), ngayTaiKhamStr, 
                                tt.DocStaff.Contact.FullName, tt.Patient.Contact.FullName, tt.ChanDoan, toaThuoc.Note);

                            //Delete chi tiet toa thuoc
                            if (deletedKeys != null && deletedKeys.Count > 0)
                            {
                                desc += "- Chi tiết toa thuốc được xóa:\n";
                                foreach (string key in deletedKeys)
                                {
                                    ChiTietToaThuoc cttt = db.ChiTietToaThuocs.SingleOrDefault<ChiTietToaThuoc>(c => c.ChiTietToaThuocGUID.ToString() == key);
                                    if (cttt != null)
                                    {
                                        cttt.DeletedDate = DateTime.Now;
                                        cttt.DeletedBy = Guid.Parse(Global.UserGUID);
                                        cttt.Status = (byte)Status.Deactived;

                                        string tenThuoc = string.Empty;
                                        if (cttt.Thuoc != null) tenThuoc = cttt.Thuoc.TenThuoc;
                                        else tenThuoc = cttt.TenThuocNgoai;

                                        desc += string.Format("  + GUID: '{0}', Thuốc: '{1}', Số lượng: '{2}'\n", cttt.ChiTietToaThuocGUID.ToString(),
                                        tenThuoc, cttt.SoLuong);
                                    }
                                }

                                db.SubmitChanges();
                            }
                            

                            //Add chi tiet toa thuoc
                            if (addedList != null && addedList.Count > 0)
                            {
                                desc += "- Chi tiết toa thuốc được thêm:\n";
                                foreach (ChiTietToaThuoc cttt in addedList)
                                {
                                    cttt.ToaThuocGUID = tt.ToaThuocGUID;
                                    if (cttt.ChiTietToaThuocGUID == Guid.Empty)
                                    {
                                        cttt.ChiTietToaThuocGUID = Guid.NewGuid();
                                        db.ChiTietToaThuocs.InsertOnSubmit(cttt);
                                        db.SubmitChanges();

                                        string tenThuoc = string.Empty;
                                        if (cttt.Thuoc != null) tenThuoc = cttt.Thuoc.TenThuoc;
                                        else tenThuoc = cttt.TenThuocNgoai;

                                        desc += string.Format("  + GUID: '{0}', Thuốc: '{1}', Số lượng: '{2}'\n", cttt.ChiTietToaThuocGUID.ToString(),
                                        tenThuoc, cttt.SoLuong);
                                    }
                                    else
                                    {
                                        ChiTietToaThuoc chiTietToaThuoc = db.ChiTietToaThuocs.SingleOrDefault<ChiTietToaThuoc>(c => c.ChiTietToaThuocGUID == cttt.ChiTietToaThuocGUID);
                                        if (chiTietToaThuoc != null)
                                        {
                                            chiTietToaThuoc.ThuocGUID = cttt.ThuocGUID;
                                            chiTietToaThuoc.TenThuocNgoai = cttt.TenThuocNgoai;
                                            chiTietToaThuoc.SoLuong = cttt.SoLuong;
                                            chiTietToaThuoc.LieuDung = cttt.LieuDung;
                                            chiTietToaThuoc.Note = cttt.Note;
                                            chiTietToaThuoc.Sang = cttt.Sang;
                                            chiTietToaThuoc.Trua = cttt.Trua;
                                            chiTietToaThuoc.Chieu = cttt.Chieu;
                                            chiTietToaThuoc.Toi = cttt.Toi;
                                            chiTietToaThuoc.TruocAn = cttt.TruocAn;
                                            chiTietToaThuoc.SauAn = cttt.SauAn;
                                            chiTietToaThuoc.Khac_TruocSauAn = cttt.Khac_TruocSauAn;
                                            chiTietToaThuoc.Uong = cttt.Uong;
                                            chiTietToaThuoc.Boi = cttt.Boi;
                                            chiTietToaThuoc.Dat = cttt.Dat;
                                            chiTietToaThuoc.Khac_CachDung = cttt.Khac_CachDung;
                                            chiTietToaThuoc.SangNote = cttt.SangNote;
                                            chiTietToaThuoc.TruaNote = cttt.TruaNote;
                                            chiTietToaThuoc.ChieuNote = cttt.ChieuNote;
                                            chiTietToaThuoc.ToiNote = cttt.ToiNote;
                                            chiTietToaThuoc.TruocAnNote = cttt.TruocAnNote;
                                            chiTietToaThuoc.SauAnNote = cttt.SauAnNote;
                                            chiTietToaThuoc.Khac_TruocSauAnNote = cttt.Khac_TruocSauAnNote;
                                            chiTietToaThuoc.UongNote = cttt.UongNote;
                                            chiTietToaThuoc.BoiNote = cttt.BoiNote;
                                            chiTietToaThuoc.DatNote = cttt.DatNote;
                                            chiTietToaThuoc.Khac_CachDungNote = cttt.Khac_CachDungNote;
                                            chiTietToaThuoc.Status = (byte)Status.Actived;
                                            chiTietToaThuoc.UpdatedDate = cttt.UpdatedDate;
                                            chiTietToaThuoc.UpdatedBy = cttt.UpdatedBy;
                                            db.SubmitChanges();

                                            string tenThuoc = string.Empty;
                                            if (cttt.Thuoc != null) tenThuoc = cttt.Thuoc.TenThuoc;
                                            else tenThuoc = cttt.TenThuocNgoai;

                                            desc += string.Format("  + GUID: '{0}', Thuốc: '{1}', Số lượng: '{2}'\n", 
                                                chiTietToaThuoc.ChiTietToaThuocGUID.ToString(), tenThuoc, chiTietToaThuoc.SoLuong);
                                        }
                                    }
                                }
                            }

                            //Tracking
                            desc = desc.Substring(0, desc.Length - 1);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin toa thuốc";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);

                            db.SubmitChanges();
                        }
                    }

                    tnx.Complete();
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

        public static Result ChuyenBenhAn(string patientGUID, List<DataRow> rows)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (DataRow row in rows)
                    {
                        string toaThuocGUID = row["ToaThuocGUID"].ToString();
                        ToaThuoc toaThuoc = (from s in db.ToaThuocs
                                             where s.ToaThuocGUID.ToString() == toaThuocGUID
                                             select s).FirstOrDefault();

                        if (toaThuoc == null) continue;

                        //Tracking
                        string desc = string.Format("- ToaThuocGUID: '{0}': PatientGUID: '{1}' ==> '{2}' (ToaThuoc)",
                            toaThuocGUID, toaThuoc.BenhNhan.ToString(), patientGUID);

                        toaThuoc.BenhNhan = Guid.Parse(patientGUID);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Edit;
                        tk.Action = "Chuyển bệnh án";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
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
    }
}
