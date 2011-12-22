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
    public class LoThuocBus : BusBase
    {
        public static Result GetLoThuocList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CAST(SoLuongNhap * GiaNhap AS float) AS TongTien FROM LoThuocView WHERE LoThuocStatus={0} AND ThuocStatus={0} ORDER BY TenLoThuoc", 
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

        public static Result GetNhaPhanPhoiList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT DISTINCT NhaPhanPhoi FROM LoThuoc ORDER BY NhaPhanPhoi",
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

        public static Result GetLoThuocCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM LoThuoc";
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

        public static Result DeleteLoThuoc(List<string> loThuocKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (string key in loThuocKeys)
                    {
                        LoThuoc loThuoc = db.LoThuocs.SingleOrDefault<LoThuoc>(l => l.LoThuocGUID.ToString() == key);
                        if (loThuoc != null)
                        {
                            loThuoc.DeletedDate = DateTime.Now;
                            loThuoc.DeletedBy = Guid.Parse(Global.UserGUID);
                            loThuoc.Status = (byte)Status.Deactived;
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

        public static Result CheckLoThuocExistCode(string loThuocGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                LoThuoc lt = null;
                if (loThuocGUID == null || loThuocGUID == string.Empty)
                    lt = db.LoThuocs.SingleOrDefault<LoThuoc>(l => l.MaLoThuoc.ToLower() == code.ToLower());
                else
                    lt = db.LoThuocs.SingleOrDefault<LoThuoc>(l => l.MaLoThuoc.ToLower() == code.ToLower() &&
                                                                l.LoThuocGUID.ToString() != loThuocGUID);

                if (lt == null)
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

        public static Result InsertLoThuoc(LoThuoc loThuoc)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope tnx = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (loThuoc.LoThuocGUID == null || loThuoc.LoThuocGUID == Guid.Empty)
                    {
                        loThuoc.LoThuocGUID = Guid.NewGuid();
                        db.LoThuocs.InsertOnSubmit(loThuoc);
                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        LoThuoc lt = db.LoThuocs.SingleOrDefault<LoThuoc>(l => l.LoThuocGUID.ToString() == loThuoc.LoThuocGUID.ToString());
                        if (lt != null)
                        {
                            lt.ThuocGUID = loThuoc.ThuocGUID;
                            lt.MaLoThuoc = loThuoc.MaLoThuoc;
                            lt.TenLoThuoc = loThuoc.TenLoThuoc;
                            lt.SoDangKy = loThuoc.SoDangKy;
                            lt.HangSanXuat = loThuoc.HangSanXuat;
                            lt.NgaySanXuat = loThuoc.NgaySanXuat;
                            lt.NgayHetHan = loThuoc.NgayHetHan;
                            lt.NhaPhanPhoi = loThuoc.NhaPhanPhoi;
                            lt.SoLuongNhap = loThuoc.SoLuongNhap;
                            lt.DonViTinhNhap = loThuoc.DonViTinhNhap;
                            lt.GiaNhap = loThuoc.GiaNhap;
                            lt.SoLuongQuiDoi = loThuoc.SoLuongQuiDoi;
                            lt.DonViTinhQuiDoi = loThuoc.DonViTinhQuiDoi;
                            lt.GiaNhapQuiDoi = loThuoc.GiaNhapQuiDoi;
                            lt.Note = loThuoc.Note;
                            lt.CreatedDate = loThuoc.CreatedDate;
                            lt.CreatedBy = loThuoc.CreatedBy;
                            lt.UpdatedDate = loThuoc.UpdatedDate;
                            lt.UpdatedBy = loThuoc.UpdatedBy;
                            lt.DeletedDate = loThuoc.DeletedDate;
                            lt.DeletedBy = loThuoc.DeletedBy;
                            lt.Status = loThuoc.Status;
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
    }
}
