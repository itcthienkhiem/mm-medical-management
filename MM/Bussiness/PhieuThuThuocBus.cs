using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data;
using System.Data.Linq;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class PhieuThuThuocBus : BusBase
    {
        public static Result GetPhieuThuThuocList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM PhieuThuThuoc WHERE Status={0} ORDER BY NgayThu ASC, MaPhieuThuThuoc ASC", (byte)Status.Actived);
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

        public static Result GetChiTietPhieuThuThuoc(string phieuThuThuocGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT * FROM ChiTietPhieuThuThuocView WHERE CTPTTStatus={0} AND ThuocStatus={0} AND PhieuThuThuocGUID='{1}' ORDER BY TenThuoc", 
                    (byte)Status.Actived, phieuThuThuocGUID);
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

        public static Result GetPhieuThuThuocCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM PhieuThuThuoc";
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

        public static Result GetPhieuThuThuoc(string phieuThuThuocGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                PhieuThuThuoc ptthuoc = db.PhieuThuThuocs.SingleOrDefault<PhieuThuThuoc>(p => p.PhieuThuThuocGUID.ToString() == phieuThuThuocGUID);
                result.QueryResult = ptthuoc;
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

        public static Result DeletePhieuThuThuoc(List<string> phieuThuThuocKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (string key in phieuThuThuocKeys)
                    {
                        PhieuThuThuoc ptthuoc = db.PhieuThuThuocs.SingleOrDefault<PhieuThuThuoc>(p => p.PhieuThuThuocGUID.ToString() == key);
                        if (ptthuoc != null)
                        {
                            ptthuoc.DeletedDate = DateTime.Now;
                            ptthuoc.DeletedBy = Guid.Parse(Global.UserGUID);
                            ptthuoc.Status = (byte)Status.Deactived;
                        }
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

        public static Result CheckPhieuThuThuocExistCode(string phieuThuThuocGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                PhieuThuThuoc ptthuoc = null;
                if (phieuThuThuocGUID == null || phieuThuThuocGUID == string.Empty)
                    ptthuoc = db.PhieuThuThuocs.SingleOrDefault<PhieuThuThuoc>(p => p.MaPhieuThuThuoc.ToLower() == code.ToLower());
                else
                    ptthuoc = db.PhieuThuThuocs.SingleOrDefault<PhieuThuThuoc>(p => p.MaPhieuThuThuoc.ToLower() == code.ToLower() &&
                                                                p.PhieuThuThuocGUID.ToString() != phieuThuThuocGUID);

                if (ptthuoc == null)
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

        public static Result InsertPhieuThuThuoc(PhieuThuThuoc ptthuoc, List<ChiTietPhieuThuThuoc> addedList)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope tnx = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (ptthuoc.PhieuThuThuocGUID == null || ptthuoc.PhieuThuThuocGUID == Guid.Empty)
                    {
                        ptthuoc.PhieuThuThuocGUID = Guid.NewGuid();
                        db.PhieuThuThuocs.InsertOnSubmit(ptthuoc);
                        db.SubmitChanges();

                        //Chi tiet phieu thu
                        foreach (ChiTietPhieuThuThuoc ctptt in addedList)
                        {
                            ctptt.PhieuThuThuocGUID = ptthuoc.PhieuThuThuocGUID;
                            ctptt.ChiTietPhieuThuThuocGUID = Guid.NewGuid();
                            db.ChiTietPhieuThuThuocs.InsertOnSubmit(ctptt);
                        }

                        db.SubmitChanges();
                    }
                    /*else //Update
                    {
                        Thuoc th = db.Thuocs.SingleOrDefault<Thuoc>(t => t.ThuocGUID.ToString() == thuoc.ThuocGUID.ToString());
                        if (th != null)
                        {
                            th.MaThuoc = thuoc.MaThuoc;
                            th.TenThuoc = thuoc.TenThuoc;
                            th.BietDuoc = thuoc.BietDuoc;
                            th.HamLuong = thuoc.HamLuong;
                            th.HoatChat = thuoc.HoatChat;
                            th.DonViTinh = thuoc.DonViTinh;
                            th.Note = thuoc.Note;
                            th.CreatedDate = thuoc.CreatedDate;
                            th.CreatedBy = thuoc.CreatedBy;
                            th.UpdatedDate = thuoc.UpdatedDate;
                            th.UpdatedBy = thuoc.UpdatedBy;
                            th.DeletedDate = thuoc.DeletedDate;
                            th.DeletedBy = thuoc.DeletedBy;
                            th.Status = thuoc.Status;

                            //Update DVT to LoThuoc
                            var loThuocs = th.LoThuocs;
                            foreach (var lo in loThuocs)
                            {
                                if (th.DonViTinh == lo.DonViTinhQuiDoi) continue;
                                lo.DonViTinhQuiDoi = th.DonViTinh;
                                if (lo.DonViTinhNhap != "Hộp" && lo.DonViTinhNhap != "Vỉ")
                                    lo.DonViTinhNhap = th.DonViTinh;
                                else if (lo.DonViTinhNhap == "Vỉ" && th.DonViTinh != "Viên")
                                    lo.DonViTinhNhap = "Hộp";
                            }

                            db.SubmitChanges();
                        }
                    }*/

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
