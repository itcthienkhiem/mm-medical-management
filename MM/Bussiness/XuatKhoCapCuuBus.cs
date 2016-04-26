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
    public class XuatKhoCapCuuBus : BusBase
    {
        public static Result GetXuatKhoCapCuuList()
        {
            Result result = null;

            try
            {

                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM XuatKhoCapCuuView WITH(NOLOCK) WHERE XuatKhoCapCuuStatus={0} AND KhoCapCuuStatus={0} ORDER BY NgayXuat DESC",
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

        public static Result DeleteXuatKhoCappCuu(List<string> keys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    DateTime dt = DateTime.Now;
                    string desc = string.Empty;
                    foreach (string key in keys)
                    {
                        XuatKhoCapCuu xuatKhoCapCuu = db.XuatKhoCapCuus.SingleOrDefault<XuatKhoCapCuu>(x => x.XuatKhoCapCuuGUID.ToString() == key);
                        if (xuatKhoCapCuu != null)
                        {
                            Status status = (Status)xuatKhoCapCuu.Status;
                            xuatKhoCapCuu.DeletedDate = DateTime.Now;
                            xuatKhoCapCuu.DeletedBy = Guid.Parse(Global.UserGUID);
                            xuatKhoCapCuu.Status = (byte)Status.Deactived;

                            if (status == (byte)Status.Actived)
                            {
                                //Update số lượng xuất 
                                int soLuong = xuatKhoCapCuu.SoLuong;
                                var nhapKhoCapCuuList = from l in db.NhapKhoCapCuus
                                                        where l.Status == (byte)Status.Actived &&
                                                        l.KhoCapCuuGUID == xuatKhoCapCuu.KhoCapCuuGUID &&
                                                        new DateTime(l.NgayHetHan.Value.Year, l.NgayHetHan.Value.Month, l.NgayHetHan.Value.Day) > dt &&
                                                        l.SoLuongXuat > 0
                                                        orderby new DateTime(l.NgayHetHan.Value.Year, l.NgayHetHan.Value.Month, l.NgayHetHan.Value.Day) ascending,
                                                        l.NgayNhap ascending
                                                        select l;

                                if (nhapKhoCapCuuList != null)
                                {
                                    foreach (var nk in nhapKhoCapCuuList)
                                    {
                                        if (soLuong > 0)
                                        {
                                            if (nk.SoLuongXuat >= soLuong)
                                            {
                                                nk.SoLuongXuat -= soLuong;
                                                soLuong = 0;
                                                db.SubmitChanges();
                                                break;
                                            }
                                            else
                                            {
                                                soLuong -= nk.SoLuongXuat;
                                                nk.SoLuongXuat = 0;
                                                db.SubmitChanges();
                                            }
                                        }
                                    }
                                }
                            }

                            desc += string.Format("- GUID: '{0}', Ngày xuất: '{1}', Tên cấp cứu: '{2}', SL xuất: '{3}'\n",
                                xuatKhoCapCuu.XuatKhoCapCuuGUID.ToString(), xuatKhoCapCuu.NgayXuat.ToString("dd/MM/yyyy HH:mm:ss"),
                                xuatKhoCapCuu.KhoCapCuu.TenCapCuu, xuatKhoCapCuu.SoLuong);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin xuất kho cấp cứu";
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

        public static Result InsertXuatKhoCapCuu(XuatKhoCapCuu xuatKhoCapCuu)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                DateTime dt = DateTime.Now;

                using (TransactionScope tnx = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (xuatKhoCapCuu.XuatKhoCapCuuGUID == null || xuatKhoCapCuu.XuatKhoCapCuuGUID == Guid.Empty)
                    {
                        xuatKhoCapCuu.XuatKhoCapCuuGUID = Guid.NewGuid();
                        db.XuatKhoCapCuus.InsertOnSubmit(xuatKhoCapCuu);
                        db.SubmitChanges();

                        //Update số lượng xuất
                        int soLuong = xuatKhoCapCuu.SoLuong;
                        var nhapKhoCapCuuList = from l in db.NhapKhoCapCuus
                                                where l.Status == (byte)Status.Actived &&
                                                l.KhoCapCuuGUID == xuatKhoCapCuu.KhoCapCuuGUID &&
                                                new DateTime(l.NgayHetHan.Value.Year, l.NgayHetHan.Value.Month, l.NgayHetHan.Value.Day) > dt &&
                                                l.SoLuongNhap * l.SoLuongQuiDoi - l.SoLuongXuat > 0
                                                orderby new DateTime(l.NgayHetHan.Value.Year, l.NgayHetHan.Value.Month, l.NgayHetHan.Value.Day) ascending, 
                                                l.NgayNhap ascending
                                                select l;

                        if (nhapKhoCapCuuList != null)
                        {
                            foreach (var lt in nhapKhoCapCuuList)
                            {
                                if (soLuong > 0)
                                {
                                    int soLuongTon = lt.SoLuongNhap * lt.SoLuongQuiDoi - lt.SoLuongXuat;
                                    if (soLuongTon >= soLuong)
                                    {
                                        lt.SoLuongXuat += soLuong;
                                        soLuong = 0;
                                        db.SubmitChanges();
                                        break;
                                    }
                                    else
                                    {
                                        lt.SoLuongXuat += soLuongTon;
                                        soLuong -= soLuongTon;
                                        db.SubmitChanges();
                                    }
                                }
                            }
                        }

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Ngày xuất: '{1}', Tên cấp cứu: '{2}', SL xuất: '{3}'",
                                xuatKhoCapCuu.XuatKhoCapCuuGUID.ToString(), xuatKhoCapCuu.NgayXuat.ToString("dd/MM/yyyy HH:mm:ss"), 
                                xuatKhoCapCuu.KhoCapCuu.TenCapCuu, xuatKhoCapCuu.SoLuong);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin xuất kho cấp cứu";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    //else //Update
                    //{
                    //    XuatKhoCapCuu xk = db.XuatKhoCapCuus.SingleOrDefault<XuatKhoCapCuu>(x => x.XuatKhoCapCuuGUID == xuatKhoCapCuu.XuatKhoCapCuuGUID);
                    //    if (xk != null)
                    //    {
                    //        int soLuongXuatCu = xk.SoLuong;
                    //        xk.KhoCapCuuGUID = xuatKhoCapCuu.KhoCapCuuGUID;
                    //        xk.NgayXuat = xuatKhoCapCuu.NgayXuat;
                    //        xk.SoLuong = xuatKhoCapCuu.SoLuong;
                    //        xk.GiaXuat = xuatKhoCapCuu.GiaXuat;
                    //        xk.Note = xuatKhoCapCuu.Note;
                    //        xk.CreatedDate = xuatKhoCapCuu.CreatedDate;
                    //        xk.CreatedBy = xuatKhoCapCuu.CreatedBy;
                    //        xk.UpdatedDate = xuatKhoCapCuu.UpdatedDate;
                    //        xk.UpdatedBy = xuatKhoCapCuu.UpdatedBy;
                    //        xk.DeletedDate = xuatKhoCapCuu.DeletedDate;
                    //        xk.DeletedBy = xuatKhoCapCuu.DeletedBy;
                    //        xk.Status = xuatKhoCapCuu.Status;
                    //        db.SubmitChanges();



                    //        Tracking
                    //        desc += string.Format("- GUID: '{0}', Ngày xuất: '{1}', Tên cấp cứu: '{2}', SL xuất: cũ: '{3}' - mới: '{4}'",
                    //                xk.XuatKhoCapCuuGUID.ToString(), xk.NgayXuat.ToString("dd/MM/yyyy HH:mm:ss"), xk.KhoCapCuu.TenCapCuu, 
                    //                soLuongXuatCu, xk.SoLuong);

                    //        Tracking tk = new Tracking();
                    //        tk.TrackingGUID = Guid.NewGuid();
                    //        tk.TrackingDate = DateTime.Now;
                    //        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    //        tk.ActionType = (byte)ActionType.Edit;
                    //        tk.Action = "Sửa thông tin xuất kho cấp cứu";
                    //        tk.Description = desc;
                    //        tk.TrackingType = (byte)TrackingType.None;
                    //        db.Trackings.InsertOnSubmit(tk);

                    //        db.SubmitChanges();
                    //    }
                    //}

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
    }
}
