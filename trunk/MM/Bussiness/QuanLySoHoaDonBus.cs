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
    public class QuanLySoHoaDonBus : BusBase
    {
        public static Result GetSoHoaDon()
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                while (true)
                {
                    string query = string.Format("SELECT Min(SoHoaDon) as SoHoaDon FROM QuanLySoHoaDon WITH(NOLOCK) WHERE XuatTruoc = 'False' AND DaXuat = 'False' AND NgayBatDau >= '{0}'",
                        Global.NgayThayDoiSoHoaDonSauCung.ToString("yyyy-MM-dd HH:mm:ss"));
                    result = ExcuteQuery(query);
                    if (!result.IsOK) return result;
                    DataTable dt = result.QueryResult as DataTable;
                    if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != null && dt.Rows[0][0] != DBNull.Value)
                        result.QueryResult = dt.Rows[0][0];
                    else
                    {
                        query = string.Format("SELECT MAX(SoHoaDon) as SoHoaDon FROM QuanLySoHoaDon WITH(NOLOCK) WHERE NgayBatDau >= '{0}'", 
                            Global.NgayThayDoiSoHoaDonSauCung.ToString("yyyy-MM-dd HH:mm:ss"));
                        result = ExcuteQuery(query);
                        if (!result.IsOK) return result;

                        dt = result.QueryResult as DataTable;
                        if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != null && dt.Rows[0][0] != DBNull.Value)
                            result.QueryResult = Convert.ToInt32(dt.Rows[0][0]) + 1;
                        else
                            result.QueryResult = 1;
                    }

                    db = new MMOverride();
                    int soHoaDon = Convert.ToInt32(result.QueryResult);
                    bool isExist = false;

                    //Hoa don dich vu
                    Invoice hdhd = db.Invoices.FirstOrDefault<Invoice>(h => Convert.ToInt32(h.InvoiceCode) == soHoaDon && 
                        h.Status == (byte)Status.Actived && h.InvoiceDate >= Global.NgayThayDoiSoHoaDonSauCung);
                    if (hdhd != null)
                    {
                        QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.FirstOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon && 
                            q.NgayBatDau.Value >= Global.NgayThayDoiSoHoaDonSauCung);
                        if (qlshd == null)
                        {
                            qlshd = new QuanLySoHoaDon();
                            qlshd.QuanLySoHoaDonGUID = Guid.NewGuid();
                            qlshd.SoHoaDon = soHoaDon;
                            qlshd.DaXuat = true;
                            qlshd.XuatTruoc = false;
                            qlshd.NgayBatDau = Global.NgayThayDoiSoHoaDonSauCung;
                            db.QuanLySoHoaDons.InsertOnSubmit(qlshd);
                        }
                        else
                            qlshd.DaXuat = true;

                        db.SubmitChanges();
                        isExist = true;
                    }

                    //Hoa don thuoc
                    if (!isExist)
                    {
                        HoaDonThuoc hdt = db.HoaDonThuocs.FirstOrDefault<HoaDonThuoc>(h => Convert.ToInt32(h.SoHoaDon) == soHoaDon && 
                            h.Status == (byte)Status.Actived && h.NgayXuatHoaDon >= Global.NgayThayDoiSoHoaDonSauCung);
                        if (hdt != null)
                        {
                            QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.FirstOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon &&
                                q.NgayBatDau.Value >= Global.NgayThayDoiSoHoaDonSauCung);
                            if (qlshd == null)
                            {
                                qlshd = new QuanLySoHoaDon();
                                qlshd.QuanLySoHoaDonGUID = Guid.NewGuid();
                                qlshd.SoHoaDon = soHoaDon;
                                qlshd.DaXuat = true;
                                qlshd.XuatTruoc = false;
                                qlshd.NgayBatDau = Global.NgayThayDoiSoHoaDonSauCung;
                                db.QuanLySoHoaDons.InsertOnSubmit(qlshd);
                            }
                            else
                                qlshd.DaXuat = true;

                            db.SubmitChanges();
                            isExist = true;
                        }
                    }
                    

                    //Hoa don xuat truoc
                    if (!isExist)
                    {
                        HoaDonXuatTruoc hdxt = db.HoaDonXuatTruocs.FirstOrDefault<HoaDonXuatTruoc>(h => Convert.ToInt32(h.SoHoaDon) == soHoaDon && 
                            h.Status == (byte)Status.Actived && h.NgayXuatHoaDon >= Global.NgayThayDoiSoHoaDonSauCung);
                        if (hdxt != null)
                        {
                            QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.FirstOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon &&
                                q.NgayBatDau.Value >= Global.NgayThayDoiSoHoaDonSauCung);
                            if (qlshd == null)
                            {
                                qlshd = new QuanLySoHoaDon();
                                qlshd.QuanLySoHoaDonGUID = Guid.NewGuid();
                                qlshd.SoHoaDon = soHoaDon;
                                qlshd.DaXuat = true;
                                qlshd.XuatTruoc = true;
                                qlshd.NgayBatDau = Global.NgayThayDoiSoHoaDonSauCung;
                                db.QuanLySoHoaDons.InsertOnSubmit(qlshd);
                            }
                            else
                                qlshd.DaXuat = true;

                            db.SubmitChanges();
                            isExist = true;
                        }
                    }

                    //Hoa don hop dong
                    if (!isExist)
                    {
                        HoaDonHopDong hd = db.HoaDonHopDongs.FirstOrDefault<HoaDonHopDong>(h => Convert.ToInt32(h.SoHoaDon) == soHoaDon && 
                            h.Status == (byte)Status.Actived && h.NgayXuatHoaDon >= Global.NgayThayDoiSoHoaDonSauCung);
                        if (hd != null)
                        {
                            QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.FirstOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon &&
                                q.NgayBatDau.Value >= Global.NgayThayDoiSoHoaDonSauCung);
                            if (qlshd == null)
                            {
                                qlshd = new QuanLySoHoaDon();
                                qlshd.QuanLySoHoaDonGUID = Guid.NewGuid();
                                qlshd.SoHoaDon = soHoaDon;
                                qlshd.DaXuat = true;
                                qlshd.XuatTruoc = false;
                                qlshd.NgayBatDau = Global.NgayThayDoiSoHoaDonSauCung;
                                db.QuanLySoHoaDons.InsertOnSubmit(qlshd);
                            }
                            else
                                qlshd.DaXuat = true;

                            db.SubmitChanges();
                            isExist = true;
                        }
                    }

                    if (!isExist) break;
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

        public static Result GetSoHoaDonChuaXuat(int count)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                string query = string.Format("SELECT TOP {0} CAST(0 AS Bit) AS Checked, * FROM QuanLySoHoaDon WITH(NOLOCK) WHERE XuatTruoc = 'False' AND DaXuat = 'False' AND NgayBatDau >= '{1}' ORDER BY SoHoaDon", 
                    count, Global.NgayThayDoiSoHoaDonSauCung.ToString("yyyy-MM-dd HH:mm:ss"));
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

        public static Result GetMaxSoHoaDon()
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                string query = string.Format("SELECT MAX(SoHoaDon) as SoHoaDon FROM QuanLySoHoaDon WITH(NOLOCK) WHERE NgayBatDau >= '{0}'",
                    Global.NgayThayDoiSoHoaDonSauCung.ToString("yyyy-MM-dd HH:mm:ss"));
                result = ExcuteQuery(query);
                if (!result.IsOK) return result;

                DataTable dt = result.QueryResult as DataTable;
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != null && dt.Rows[0][0] != DBNull.Value)
                    result.QueryResult = Convert.ToInt32(dt.Rows[0][0]);
                else
                    result.QueryResult = 0;
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

        public static Result GetMinMaxNgayXuatHoaDon(int soHoaDon, ref DateTime minDate, ref DateTime maxDate)
        {
            Result result = new Result();
            minDate = Global.MinDateTime;
            maxDate = Global.MaxDateTime;

            try
            {
                string query = string.Format("SELECT MAX(InvoiceDate) AS MinDate FROM Invoice WITH(NOLOCK) WHERE Status = 0 AND CAST(InvoiceCode as int) < {0} AND InvoiceDate >= '{1}' SELECT MAX(NgayXuatHoaDon) AS MinDate FROM HoaDonThuoc WHERE Status = 0 AND CAST(SoHoaDon as int) < {0} AND NgayXuatHoaDon >= '{1}' SELECT MAX(NgayXuatHoaDon) AS MinDate FROM HoaDonXuatTruoc WHERE Status = 0 AND CAST(SoHoaDon as int) < {0} AND NgayXuatHoaDon >= '{1}' SELECT MIN(InvoiceDate) AS MaxDate FROM Invoice WHERE Status = 0 AND CAST(InvoiceCode as int) > {0} AND InvoiceDate >= '{1}' SELECT MIN(NgayXuatHoaDon) AS MaxDate FROM HoaDonThuoc WHERE Status = 0 AND CAST(SoHoaDon as int) > {0} AND NgayXuatHoaDon >= '{1}' SELECT MIN(NgayXuatHoaDon) AS MaxDate FROM HoaDonXuatTruoc WHERE Status = 0 AND CAST(SoHoaDon as int) > {0} AND NgayXuatHoaDon >= '{1}'", 
                    soHoaDon, Global.NgayThayDoiSoHoaDonSauCung.ToString("yyyy-MM-dd HH:mm:ss"));
                result = ExcuteQueryDataSet(query);
                if (!result.IsOK) return result;

                DataSet ds = result.QueryResult as DataSet;
                object obj = ds.Tables[0].Rows[0]["MinDate"];
                if (obj != null && obj != DBNull.Value && Convert.ToDateTime(obj) > minDate)
                    minDate = Convert.ToDateTime(obj);

                obj = ds.Tables[1].Rows[0]["MinDate"];
                if (obj != null && obj != DBNull.Value && Convert.ToDateTime(obj) > minDate)
                    minDate = Convert.ToDateTime(obj);

                obj = ds.Tables[2].Rows[0]["MinDate"];
                if (obj != null && obj != DBNull.Value && Convert.ToDateTime(obj) > minDate)
                    minDate = Convert.ToDateTime(obj);

                obj = ds.Tables[3].Rows[0]["MaxDate"];
                if (obj != null && obj != DBNull.Value && Convert.ToDateTime(obj) < maxDate)
                    maxDate = Convert.ToDateTime(obj);

                obj = ds.Tables[4].Rows[0]["MaxDate"];
                if (obj != null && obj != DBNull.Value && Convert.ToDateTime(obj) < maxDate)
                    maxDate = Convert.ToDateTime(obj);

                obj = ds.Tables[5].Rows[0]["MaxDate"];
                if (obj != null && obj != DBNull.Value && Convert.ToDateTime(obj) < maxDate)
                    maxDate = Convert.ToDateTime(obj);

                minDate = new DateTime(minDate.Year, minDate.Month, minDate.Day, 0, 0, 0);
                maxDate = new DateTime(maxDate.Year, maxDate.Month, maxDate.Day, 23, 59, 59);
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

        public static Result SetThayDoiSoHoaSon(DateTime ngayThayDoi, string mauSo, string kiHieu)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                NgayBatDauLamMoiSoHoaDon nbd = new NgayBatDauLamMoiSoHoaDon();
                nbd.MaNgayBatDauGUID = Guid.NewGuid();
                nbd.NgayBatDau = ngayThayDoi;
                nbd.MauSo = mauSo;
                nbd.KiHieu = kiHieu;
                db.NgayBatDauLamMoiSoHoaDons.InsertOnSubmit(nbd);
                db.SubmitChanges();
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

        public static Result GetThayDoiSoHoaSonSauCung()
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                var ngayThayDoi = (from n in db.NgayBatDauLamMoiSoHoaDons
                                       orderby n.NgayBatDau descending
                                       select n).FirstOrDefault();

                if (ngayThayDoi != null)
                    result.QueryResult = ngayThayDoi;
                else
                    result.QueryResult = null;
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
