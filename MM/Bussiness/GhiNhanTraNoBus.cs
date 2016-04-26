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
using System.Transactions;
using System.Data;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class GhiNhanTraNoBus : BusBase
    {
        public static Result GetGhiNhanTraNoList(string phieuThuGUID, LoaiPT loaiPT)
        {
            Result result = new Result();

            try
            {
                string query = string.Format(@"SELECT CAST(0 AS Bit) AS Checked, * FROM GhiNhanTraNoView WITH(NOLOCK) 
                                                WHERE MaPhieuThuGUID = '{0}' AND LoaiPT = {1} AND [Status] = 0 
                                                ORDER BY NgayTra DESC", phieuThuGUID, (int)loaiPT);
                

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

        public static Result GetTongTienTraNo(string ghiNhanTraNoGUID, string phieuThuGUID, LoaiPT loaiPT)
        {
            Result result = new Result();

            try
            {
                string subQuery = ghiNhanTraNoGUID == string.Empty ? 
                    string.Empty : string.Format("AND GhiNhanTraNoGUID <> '{0}'", ghiNhanTraNoGUID);

                string query = string.Format(@"SELECT SUM(SoTien) AS TongTien FROM GhiNhanTraNo WITH(NOLOCK) 
                                                WHERE MaPhieuThuGUID = '{0}' AND LoaiPT = {1} AND [Status] = 0 {2}", 
                                                phieuThuGUID, (int)loaiPT, subQuery);

                result = ExcuteQuery(query);

                double tongTien = 0;
                if (result.IsOK)
                {
                    DataTable dt = result.QueryResult as DataTable;
                    if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                        tongTien = Convert.ToDouble(dt.Rows[0][0]);
                }

                result.QueryResult = tongTien;
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

        public static Result GetTongTienPhieuThu(string phieuThuGUID, LoaiPT loaiPT)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;

                switch (loaiPT)
                {
                    case LoaiPT.DichVu:
                        query = string.Format(@"SELECT SUM(CAST(((Price - (Price * Discount)/100) * SoLuong) AS float)) AS TongTien
                                                FROM ReceiptDetailView WITH(NOLOCK)
                                                WHERE ReceiptGUID = '{0}' AND ReceiptDetailStatus = 0", phieuThuGUID);
                        break;
                    case LoaiPT.Thuoc:
                        query = string.Format(@"SELECT SUM(ThanhTien) AS TongTien FROM ChiTietPhieuThuThuoc WITH(NOLOCK) 
                                                WHERE PhieuThuThuocGUID = '{0}' AND [Status] = 0", phieuThuGUID);
                        break;
                    case LoaiPT.HopDong:
                        query = string.Format(@"SELECT SUM(ThanhTien) AS TongTien FROM ChiTietPhieuThuHopDong WITH(NOLOCK) 
                                                WHERE PhieuThuHopDongGUID = '{0}' AND [Status] = 0", phieuThuGUID);
                        break;
                    case LoaiPT.CapCuu:
                        query = string.Format(@"SELECT SUM(ThanhTien) AS TongTien FROM ChiTietPhieuThuCapCuu WITH(NOLOCK) 
                                                WHERE PhieuThuCapCuuGUID = '{0}' AND [Status] = 0", phieuThuGUID);
                        break;
                }

                result = ExcuteQuery(query);

                double tongTien = 0;
                if (result.IsOK)
                {
                    DataTable dt = result.QueryResult as DataTable;
                    if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                        tongTien = Convert.ToDouble(dt.Rows[0][0]);
                }

                result.QueryResult = tongTien;
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

        public static Result DeleteGhiNhanTraNo(List<string> keys, string phieuThuGUID, LoaiPT loaiPT)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    bool isDelete = false;
                    string desc = string.Empty;
                    foreach (string key in keys)
                    {
                        GhiNhanTraNo tn = db.GhiNhanTraNos.SingleOrDefault<GhiNhanTraNo>(ss => ss.GhiNhanTraNoGUID.ToString() == key);
                        if (tn != null)
                        {
                            tn.DeletedDate = DateTime.Now;
                            tn.DeletedBy = Guid.Parse(Global.UserGUID);

                            if (tn.Status == (byte)Status.Actived)
                                isDelete = true;

                            tn.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Phiếu thu GUID: '{1}', Ngày trả: '{2}', Số tiền: '{3}', Ghi chú: '{4}'\n",
                                tn.GhiNhanTraNoGUID.ToString(), tn.MaPhieuThuGUID.ToString(), tn.NgayTra.ToString("dd/MM/yyyy HH:mm:ss"), tn.SoTien, tn.GhiChu);
                        }
                    }

                    //Update trạng thái phiếu thu
                    if (isDelete)
                    {
                        switch (loaiPT)
                        {
                            case LoaiPT.DichVu:
                                Receipt ptdv = db.Receipts.Where(p => p.ReceiptGUID.ToString() == phieuThuGUID).FirstOrDefault();
                                ptdv.ChuaThuTien = true;
                                break;
                            case LoaiPT.Thuoc:
                                PhieuThuThuoc ptt = db.PhieuThuThuocs.Where(p => p.PhieuThuThuocGUID.ToString() == phieuThuGUID).FirstOrDefault();
                                ptt.ChuaThuTien = true;
                                break;
                            case LoaiPT.HopDong:
                                PhieuThuHopDong pthd = db.PhieuThuHopDongs.Where(p => p.PhieuThuHopDongGUID.ToString() == phieuThuGUID).FirstOrDefault();
                                pthd.ChuaThuTien = true;
                                break;
                            case LoaiPT.CapCuu:
                                PhieuThuCapCuu ptcc = db.PhieuThuCapCuus.Where(p => p.PhieuThuCapCuuGUID.ToString() == phieuThuGUID).FirstOrDefault();
                                ptcc.ChuaThuTien = true;
                                break;
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin ghi nhận trả nợ";
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

        public static Result InsertGhiNhanTraNo(GhiNhanTraNo ghiNhanTraNo, bool isDataTraDu, string phieuThuGUID, LoaiPT loaiPT)
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
                    if (ghiNhanTraNo.GhiNhanTraNoGUID == null || ghiNhanTraNo.GhiNhanTraNoGUID == Guid.Empty)
                    {
                        ghiNhanTraNo.GhiNhanTraNoGUID = Guid.NewGuid();
                        db.GhiNhanTraNos.InsertOnSubmit(ghiNhanTraNo);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Phiếu thu GUID: '{1}', Ngày trả: '{2}', Số tiền: '{3}', Ghi chú: '{4}'",
                               ghiNhanTraNo.GhiNhanTraNoGUID.ToString(), ghiNhanTraNo.MaPhieuThuGUID.ToString(), 
                               ghiNhanTraNo.NgayTra.ToString("dd/MM/yyyy HH:mm:ss"), ghiNhanTraNo.SoTien, ghiNhanTraNo.GhiChu);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin ghi nhận trả nợ";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        GhiNhanTraNo tn = db.GhiNhanTraNos.SingleOrDefault<GhiNhanTraNo>(s => s.GhiNhanTraNoGUID.ToString() == ghiNhanTraNo.GhiNhanTraNoGUID.ToString());
                        if (tn != null)
                        {
                            tn.MaPhieuThuGUID = ghiNhanTraNo.MaPhieuThuGUID;
                            tn.NgayTra = ghiNhanTraNo.NgayTra;
                            tn.SoTien = ghiNhanTraNo.SoTien;
                            tn.GhiChu = ghiNhanTraNo.GhiChu;
                            tn.LoaiPT = ghiNhanTraNo.LoaiPT;
                            tn.CreatedDate = ghiNhanTraNo.CreatedDate;
                            tn.CreatedBy = ghiNhanTraNo.CreatedBy;
                            tn.UpdatedDate = ghiNhanTraNo.UpdatedDate;
                            tn.UpdatedBy = ghiNhanTraNo.UpdatedBy;
                            tn.DeletedDate = ghiNhanTraNo.DeletedDate;
                            tn.DeletedBy = ghiNhanTraNo.DeletedBy;
                            tn.Status = ghiNhanTraNo.Status;

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Phiếu thu GUID: '{1}', Ngày trả: '{2}', Số tiền: '{3}', Ghi chú: '{4}'",
                                tn.GhiNhanTraNoGUID.ToString(), tn.MaPhieuThuGUID.ToString(), 
                                tn.NgayTra.ToString("dd/MM/yyyy HH:mm:ss"), tn.SoTien, tn.GhiChu);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin ghi nhận trả nợ";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);

                            db.SubmitChanges();
                        }
                    }

                    switch (loaiPT)
                    {
                        case LoaiPT.DichVu:
                            Receipt ptdv = db.Receipts.Where(p => p.ReceiptGUID.ToString() == phieuThuGUID).FirstOrDefault();
                            ptdv.ChuaThuTien = !isDataTraDu;
                            break;
                        case LoaiPT.Thuoc:
                            PhieuThuThuoc ptt = db.PhieuThuThuocs.Where(p => p.PhieuThuThuocGUID.ToString() == phieuThuGUID).FirstOrDefault();
                            ptt.ChuaThuTien = !isDataTraDu;
                            break;
                        case LoaiPT.HopDong:
                            PhieuThuHopDong pthd = db.PhieuThuHopDongs.Where(p => p.PhieuThuHopDongGUID.ToString() == phieuThuGUID).FirstOrDefault();
                            pthd.ChuaThuTien = !isDataTraDu;
                            break;
                        case LoaiPT.CapCuu:
                            PhieuThuCapCuu ptcc = db.PhieuThuCapCuus.Where(p => p.PhieuThuCapCuuGUID.ToString() == phieuThuGUID).FirstOrDefault();
                            ptcc.ChuaThuTien = !isDataTraDu;
                            break;
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
