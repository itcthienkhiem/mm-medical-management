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
    public class HuyThuocBus : BusBase
    {
        public static Result GetHuyThuocList(string tenThuoc, DateTime tuNgay, DateTime denNgay)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM HuyThuocView WITH(NOLOCK) WHERE Status={0} AND NgayHuy BETWEEN '{1}' AND '{2}' ORDER BY NgayHuy DESC",
                        (byte)Status.Actived, tuNgay.ToString("yyyy-MM-dd 00:00:00"), denNgay.ToString("yyyy-MM-dd 23:59:59"));

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

        public static Result GetThuocTonKho(string thuocGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                DateTime dt = DateTime.Now;
                List<LoThuoc> loThuocList = (from l in db.LoThuocs
                                             where l.Status == (byte)Status.Actived &&
                                             l.ThuocGUID.ToString() == thuocGUID &&
                                             l.SoLuongNhap * l.SoLuongQuiDoi - l.SoLuongXuat > 0
                                             select l).ToList<LoThuoc>();

                if (loThuocList != null && loThuocList.Count > 0)
                {
                    int tongSLNhap = 0;
                    int tongSLXuat = 0;

                    foreach (LoThuoc lt in loThuocList)
                    {
                        tongSLNhap += lt.SoLuongNhap * lt.SoLuongQuiDoi;
                        tongSLXuat += lt.SoLuongXuat;
                    }

                    result.QueryResult = tongSLNhap - tongSLXuat;
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

        public static Result DeleteHuyThuoc(List<string> keys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                OverrideTransactionScopeMaximumTimeout(new TimeSpan(1, 0, 0));

                using (TransactionScope t = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 45, 0)))
                {
                    string desc = string.Empty;
                    foreach (string key in keys)
                    {
                        HuyThuoc huyThuoc = db.HuyThuocs.SingleOrDefault<HuyThuoc>(p => p.HuyThuocGUID.ToString() == key);
                        if (huyThuoc != null)
                        {
                            Status status = (Status)huyThuoc.Status;
                            huyThuoc.DeletedDate = DateTime.Now;
                            huyThuoc.DeletedBy = Guid.Parse(Global.UserGUID);
                            huyThuoc.Status = (byte)Status.Deactived;

                            if (status == (byte)Status.Actived)
                            {
                                //Update So luong Lo thuoc
                                var cthts = huyThuoc.ChiTietHuyThuocs;
                                foreach (var ctht in cthts)
                                {
                                    LoThuoc lt = (from l in db.LoThuocs
                                                  where l.LoThuocGUID == ctht.LoThuocGUID
                                                  select l).FirstOrDefault();

                                    if (lt != null)
                                    {
                                        lt.SoLuongXuat -= ctht.SoLuong;
                                        db.SubmitChanges();
                                    }
                                    else
                                        Utility.WriteToTraceLog(string.Format("Không tồn tại lô thuốc: '{0}', ChiTietHuyThuocGUID: '{1}'",
                                        lt.LoThuocGUID.ToString(), ctht.ChiTietHuyThuocGUID.ToString()));
                                }
                            }

                            desc += string.Format("- GUID: '{0}', Mã thuốc: '{1}', Tên thuốc: '{2}', ĐVT: '{3}', Biệt dược: '{4}', Hàm lượng: '{5}', Hoạt chất: '{6}', Ngày hủy: '{7}', Số lượng: '{8}', Ghi chú: '{9}'\n",
                            huyThuoc.HuyThuocGUID.ToString(), huyThuoc.Thuoc.MaThuoc, huyThuoc.Thuoc.TenThuoc, huyThuoc.Thuoc.DonViTinh, huyThuoc.Thuoc.BietDuoc, huyThuoc.Thuoc.HamLuong,
                            huyThuoc.Thuoc.HoatChat, huyThuoc.NgayHuy.ToString("dd/MM/yyyy HH:mm:ss"), huyThuoc.SoLuong, huyThuoc.Note);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin hủy thuốc";
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

        public static Result InsertHuyThuoc(HuyThuoc huyThuoc)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;

                OverrideTransactionScopeMaximumTimeout(new TimeSpan(1, 0, 0));

                using (TransactionScope t = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 45, 0)))
                {
                    //Insert
                    huyThuoc.HuyThuocGUID = Guid.NewGuid();
                    db.HuyThuocs.InsertOnSubmit(huyThuoc);
                    db.SubmitChanges();

                    //Tracking
                    desc += string.Format("- GUID: '{0}', Mã thuốc: '{1}', Tên thuốc: '{2}', ĐVT: '{3}', Biệt dược: '{4}', Hàm lượng: '{5}', Hoạt chất: '{6}', Ngày hủy: '{7}', Số lượng: '{8}', Ghi chú: '{9}'\n",
                            huyThuoc.HuyThuocGUID.ToString(), huyThuoc.Thuoc.MaThuoc, huyThuoc.Thuoc.TenThuoc, huyThuoc.Thuoc.DonViTinh, huyThuoc.Thuoc.BietDuoc, huyThuoc.Thuoc.HamLuong, 
                            huyThuoc.Thuoc.HoatChat, huyThuoc.NgayHuy.ToString("dd/MM/yyyy HH:mm:ss"), huyThuoc.SoLuong, huyThuoc.Note);

                    

                    var loThuocList = from l in db.LoThuocs
                                      where l.Status == (byte)Status.Actived &&
                                      l.ThuocGUID == huyThuoc.ThuocGUID &&
                                      l.SoLuongNhap * l.SoLuongQuiDoi - l.SoLuongXuat > 0
                                      orderby new DateTime(l.NgayHetHan.Year, l.NgayHetHan.Month, l.NgayHetHan.Day) ascending, l.CreatedDate ascending
                                      select l;

                    if (loThuocList != null)
                    {
                        desc += "- Chi tiết hủy thuốc:\n";
                        int soLuong = huyThuoc.SoLuong;

                        foreach (var lt in loThuocList)
                        {
                            int soLuongTon = lt.SoLuongNhap * lt.SoLuongQuiDoi - lt.SoLuongXuat;
                            if (soLuongTon >= soLuong)
                            {
                                lt.SoLuongXuat += soLuong;
                                

                                ChiTietHuyThuoc ctht = new ChiTietHuyThuoc();
                                ctht.ChiTietHuyThuocGUID = Guid.NewGuid();
                                ctht.CreatedDate = DateTime.Now;
                                ctht.CreatedBy = Guid.Parse(Global.UserGUID);
                                ctht.Status = (byte)Status.Actived;
                                ctht.LoThuocGUID = lt.LoThuocGUID;
                                ctht.SoLuong = soLuong;
                                ctht.HuyThuocGUID = huyThuoc.HuyThuocGUID;
                                db.ChiTietHuyThuocs.InsertOnSubmit(ctht);

                                db.SubmitChanges();

                                desc += string.Format("  + ChiTietHuyThuocGUID: '{0}' LoThuocGUID: '{1}', Mã lô thuốc: '{2}',Tên lô thuốc: '{3}', Số lượng hủy: '{4}'\n",
                                    ctht.ChiTietHuyThuocGUID.ToString(), lt.LoThuocGUID.ToString(), lt.MaLoThuoc, lt.TenLoThuoc, soLuong);

                                soLuong = 0;

                                break;
                            }
                            else
                            {
                                lt.SoLuongXuat += soLuongTon;
                                soLuong -= soLuongTon;

                                ChiTietHuyThuoc ctht = new ChiTietHuyThuoc();
                                ctht.ChiTietHuyThuocGUID = Guid.NewGuid();
                                ctht.CreatedDate = DateTime.Now;
                                ctht.CreatedBy = Guid.Parse(Global.UserGUID);
                                ctht.Status = (byte)Status.Actived;
                                ctht.LoThuocGUID = lt.LoThuocGUID;
                                ctht.SoLuong = soLuongTon;
                                ctht.HuyThuocGUID = huyThuoc.HuyThuocGUID;
                                db.ChiTietHuyThuocs.InsertOnSubmit(ctht);

                                db.SubmitChanges();

                                desc += string.Format("  + ChiTietHuyThuocGUID: '{0}' LoThuocGUID: '{1}', Mã lô thuốc: '{2}',Tên lô thuốc: '{3}', Số lượng hủy: '{4}'\n",
                                    ctht.ChiTietHuyThuocGUID.ToString(), lt.LoThuocGUID.ToString(), lt.MaLoThuoc, lt.TenLoThuoc, soLuongTon);
                            }
                        }
                    }
                    else
                        desc += "- Không tồn tại lô thuốc nào.";
                       
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Add;
                    tk.Action = "Thêm thông tin hủy thuốc";
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
    }
}
