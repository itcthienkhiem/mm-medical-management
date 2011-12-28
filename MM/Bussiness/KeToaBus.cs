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
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ToaThuocView WHERE Status={0} ORDER BY MaToaThuoc ASC",
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

        public static Result GetToaThuocList(string patientGUID, string docStaffGUID)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (docStaffGUID == Guid.Empty.ToString())
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ToaThuocView WHERE Status={0} AND BenhNhan='{1}' ORDER BY NgayKeToa ASC, MaToaThuoc ASC",
                    (byte)Status.Actived, patientGUID);
                else
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ToaThuocView WHERE Status={0} AND BenhNhan='{1}' AND BacSiKeToa='{2}' ORDER BY NgayKeToa ASC, MaToaThuoc ASC",
                    (byte)Status.Actived, patientGUID, docStaffGUID);
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
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ChiTietToaThuocView WHERE ThuocStatus={0} AND ChiTietToaThuocStatus={0} AND ToaThuocGUID='{1}' ORDER BY TenThuoc",
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
                string query = "SELECT Count(*) FROM ToaThuoc";
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
                    foreach (string key in toaThuocKeys)
                    {
                        ToaThuoc toaThuoc = db.ToaThuocs.SingleOrDefault<ToaThuoc>(tt => tt.ToaThuocGUID.ToString() == key);
                        if (toaThuoc != null)
                        {
                            toaThuoc.DeletedDate = DateTime.Now;
                            toaThuoc.DeletedBy = Guid.Parse(Global.UserGUID);
                            toaThuoc.Status = (byte)Status.Deactived;
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
                using (TransactionScope tnx = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (toaThuoc.ToaThuocGUID == null || toaThuoc.ToaThuocGUID == Guid.Empty)
                    {
                        toaThuoc.ToaThuocGUID = Guid.NewGuid();
                        db.ToaThuocs.InsertOnSubmit(toaThuoc);
                        db.SubmitChanges();

                        //Chi tiet toa thuoc
                        foreach (ChiTietToaThuoc cttt in addedList)
                        {
                            cttt.ChiTietToaThuocGUID = Guid.NewGuid();
                            cttt.ToaThuocGUID = toaThuoc.ToaThuocGUID;
                            db.ChiTietToaThuocs.InsertOnSubmit(cttt);
                        }
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

                            //Delete chi tiet toa thuoc
                            foreach (string key in deletedKeys)
                            {
                                ChiTietToaThuoc cttt = db.ChiTietToaThuocs.SingleOrDefault<ChiTietToaThuoc>(c => c.ChiTietToaThuocGUID.ToString() == key);
                                if (cttt != null)
                                {
                                    cttt.DeletedDate = DateTime.Now;
                                    cttt.DeletedBy = Guid.Parse(Global.UserGUID);
                                    cttt.Status = (byte)Status.Deactived;
                                }
                            }

                            db.SubmitChanges();

                            //Add chi tiet toa thuoc
                            foreach (ChiTietToaThuoc cttt in addedList)
                            {
                                cttt.ToaThuocGUID = tt.ToaThuocGUID;
                                if (cttt.ChiTietToaThuocGUID == Guid.Empty)
                                {
                                    cttt.ChiTietToaThuocGUID = Guid.NewGuid();
                                    db.ChiTietToaThuocs.InsertOnSubmit(cttt);
                                }
                                else
                                {
                                    ChiTietToaThuoc chiTietToaThuoc = db.ChiTietToaThuocs.SingleOrDefault<ChiTietToaThuoc>(c => c.ChiTietToaThuocGUID == cttt.ChiTietToaThuocGUID);
                                    if (chiTietToaThuoc != null)
                                    {
                                        chiTietToaThuoc.ThuocGUID = cttt.ThuocGUID;
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
                                    }
                                }
                            }

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
