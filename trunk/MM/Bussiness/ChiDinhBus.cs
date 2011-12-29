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
    public class ChiDinhBus : BusBase
    {
        public static Result GetChiDinhList(string patientGUID)
        {
            Result result = null;

            try
            {
                DateTime fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                DateTime toDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                string query = string.Empty;

                if (Global.StaffType == StaffType.Admin || Global.StaffType == StaffType.Reception)
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ChiDinhView WHERE Archived='False' AND Status={0} AND BenhNhanGUID='{1}' AND NgayChiDinh BETWEEN '{2}' AND '{3}' ORDER BY NgayChiDinh DESC",
                        (byte)Status.Actived, patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"));
                else
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ChiDinhView WHERE Archived='False' AND Status={0} AND BenhNhanGUID='{1}' AND NgayChiDinh BETWEEN '{2}' AND '{3}' AND BacSiChiDinhGUID='{4}' ORDER BY NgayChiDinh DESC",
                        (byte)Status.Actived, patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), Global.UserGUID);

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

        public static Result GetChiTietChiDinhList(string chiDinhGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ChiTietChiDinhView WHERE CTCDStatus={0} AND ServiceStatus={0} AND ChiDinhGUID='{1}' ORDER BY Name",
                        (byte)Status.Actived, chiDinhGUID);

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

        public static Result GetChiDinhCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM ChiDinh";
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

        public static Result CheckChiDinhExistCode(string chiDinhGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                ChiDinh chiDinh = null;
                if (chiDinhGUID == null || chiDinhGUID == string.Empty)
                    chiDinh = db.ChiDinhs.SingleOrDefault<ChiDinh>(c => c.MaChiDinh.ToLower() == code.ToLower());
                else
                    chiDinh = db.ChiDinhs.SingleOrDefault<ChiDinh>(c => c.MaChiDinh.ToLower() == code.ToLower() &&
                                                                c.ChiDinhGUID.ToString() != chiDinhGUID);

                if (chiDinh == null)
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

        public static Result GetDichVuChiDinhList(string chiDinhGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                List<DichVuChiDinhView> dichVuChiDinhList = (from cd in db.ChiTietChiDinhs
                                                             join ct in db.ChiTietChiDinhs on cd.ChiDinhGUID equals ct.ChiDinhGUID
                                                             join dv in db.DichVuChiDinhViews on ct.ChiTietChiDinhGUID equals dv.ChiTietChiDinhGUID
                                                             where cd.ChiDinhGUID.ToString() == chiDinhGUID &&
                                                             cd.Status == (byte)Status.Actived &&
                                                             ct.Status == (byte)Status.Actived &&
                                                             dv.Status == (byte)Status.Actived
                                                             select dv).Distinct().ToList();

                result.QueryResult = dichVuChiDinhList;
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

        

        public static Result DeleteChiDinhs(List<string> chiDinhKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (string key in chiDinhKeys)
                    {
                        ChiDinh chiDinh = db.ChiDinhs.SingleOrDefault<ChiDinh>(c => c.ChiDinhGUID.ToString() == key);
                        if (chiDinh != null)
                        {
                            chiDinh.DeletedDate = DateTime.Now;
                            chiDinh.DeletedBy = Guid.Parse(Global.UserGUID);
                            chiDinh.Status = (byte)Status.Deactived;
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

        public static Result InsertChiDinh(ChiDinh chiDinh, List<ChiTietChiDinh> addedList, List<string> deletedList)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();

                //Insert
                if (chiDinh.ChiDinhGUID == null || chiDinh.ChiDinhGUID == Guid.Empty)
                {
                    chiDinh.ChiDinhGUID = Guid.NewGuid();
                    db.ChiDinhs.InsertOnSubmit(chiDinh);
                    db.SubmitChanges();

                    //Add chi tiet
                    if (addedList != null && addedList.Count > 0)
                    {
                        foreach (ChiTietChiDinh ctcd in addedList)
                        {
                            ctcd.ChiTietChiDinhGUID = Guid.NewGuid();
                            ctcd.ChiDinhGUID = chiDinh.ChiDinhGUID;
                            db.ChiTietChiDinhs.InsertOnSubmit(ctcd);
                        }

                        db.SubmitChanges();
                    }
                }
                else //Update
                {
                    ChiDinh cd = db.ChiDinhs.SingleOrDefault<ChiDinh>(c => c.ChiDinhGUID.ToString() == chiDinh.ChiDinhGUID.ToString());
                    if (cd != null)
                    {
                        cd.MaChiDinh = chiDinh.MaChiDinh;
                        cd.NgayChiDinh = chiDinh.NgayChiDinh;
                        cd.BacSiChiDinhGUID = chiDinh.BacSiChiDinhGUID;
                        cd.BenhNhanGUID = chiDinh.BenhNhanGUID;
                        cd.CreatedDate = chiDinh.CreatedDate;
                        cd.CreatedBy = chiDinh.CreatedBy;
                        cd.UpdatedDate = chiDinh.UpdatedDate;
                        cd.UpdatedBy = chiDinh.UpdatedBy;
                        cd.DeletedDate = chiDinh.DeletedDate;
                        cd.DeletedBy = chiDinh.DeletedBy;
                        cd.Status = chiDinh.Status;

                        //Delete chi tiet
                        if (deletedList != null && deletedList.Count > 0)
                        {
                            foreach (string key in deletedList)
                            {
                                ChiTietChiDinh ctcd = db.ChiTietChiDinhs.SingleOrDefault<ChiTietChiDinh>(c => c.ChiTietChiDinhGUID.ToString() == key);
                                if (ctcd != null)
                                {
                                    ctcd.DeletedDate = DateTime.Now;
                                    ctcd.DeletedBy = Guid.Parse(Global.UserGUID);
                                    ctcd.Status = (byte)Status.Deactived;
                                }    
                            }

                            db.SubmitChanges();
                        }

                        //Add chi tiet
                        if (addedList != null && addedList.Count > 0)
                        {
                            foreach (ChiTietChiDinh ctcd in addedList)
                            {
                                ChiTietChiDinh ct = db.ChiTietChiDinhs.SingleOrDefault<ChiTietChiDinh>(c => c.ServiceGUID == ctcd.ServiceGUID &&
                                                                                                        c.ChiDinhGUID == cd.ChiDinhGUID);
                                if (ct == null)
                                {
                                    ctcd.ChiTietChiDinhGUID = Guid.NewGuid();
                                    ctcd.ChiDinhGUID = cd.ChiDinhGUID;
                                    ctcd.CreatedDate = DateTime.Now;
                                    ctcd.CreatedBy = Guid.Parse(Global.UserGUID);
                                    db.ChiTietChiDinhs.InsertOnSubmit(ctcd);
                                }
                                else
                                {
                                    ct.UpdatedDate = DateTime.Now;
                                    ct.UpdatedBy = Guid.Parse(Global.UserGUID);
                                    ct.Status = (byte)Status.Actived;
                                }
                            }

                            db.SubmitChanges();
                        }
                    }
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

        public static Result InsertDichVuChiDinh(DichVuChiDinh dichVuChiDinh)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope tnx = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    dichVuChiDinh.DichVuChiDinhGUID = Guid.NewGuid();
                    db.DichVuChiDinhs.InsertOnSubmit(dichVuChiDinh);
                    db.SubmitChanges();
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
