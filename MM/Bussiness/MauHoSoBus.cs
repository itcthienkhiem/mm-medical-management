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
    public class MauHoSoBus : BusBase
    {
        public static Result GetMauHoSoList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT * FROM MauHoSo WITH(NOLOCK) ORDER BY Loai");
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

        public static Result GetChiTietMauHoSoList(string mauHoSoGUID, string hopDongGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ChiTietMauHoSoView WITH(NOLOCK) WHERE MauHoSoGUID = '{0}' AND HopDongGUID = '{1}' ORDER BY [Name]",
                    mauHoSoGUID, hopDongGUID);
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

        public static Result DeleteServices(string mauHoSoGUID, string hopDongGUID, List<string> serviceKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in serviceKeys)
                    {
                        ChiTietMauHoSo s = db.ChiTietMauHoSos.FirstOrDefault<ChiTietMauHoSo>(ss => ss.MauHoSoGUID.ToString() == mauHoSoGUID && 
                            ss.ServiceGUID.ToString() == key && ss.HopDongGUID.ToString() == hopDongGUID);
                        if (s != null)
                        {
                            db.ChiTietMauHoSos.DeleteOnSubmit(s);
                            desc += string.Format("- GUID: '{0}', ServiceGUID: '{1}', Mã mẫu hồ sơ: '{2}', HopDongGUID: '{3}'\n", 
                                s.ChiTietMauHoSoGUID.ToString(), s.ServiceGUID.ToString(), mauHoSoGUID, hopDongGUID);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa chi tiết mẫu hồ sơ";
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

        public static Result AddServices(string mauHoSoGUID, string hopDongGUID, List<DataRow> addedServices)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (DataRow row in addedServices)
                    {
                        ChiTietMauHoSo ct = new ChiTietMauHoSo();
                        ct.ChiTietMauHoSoGUID = Guid.NewGuid();
                        ct.MauHoSoGUID = Guid.Parse(mauHoSoGUID);
                        ct.HopDongGUID = Guid.Parse(hopDongGUID);
                        ct.ServiceGUID = Guid.Parse(row["ServiceGUID"].ToString());
                        db.ChiTietMauHoSos.InsertOnSubmit(ct);
                        db.SubmitChanges();
                        desc += string.Format("- GUID: '{0}', ServiceGUID: '{1}', Mã mẫu hồ sơ: '{2}', HopDongGUID: '{3}'\n", ct.ChiTietMauHoSoGUID.ToString(), 
                            ct.ServiceGUID.ToString(), mauHoSoGUID, hopDongGUID);
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Thêm chi tiết mẫu hồ sơ";
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

        public static Result GetMauChayHoSo(string contractMemberGUID, string hopDongGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                List<MauHoSo> mauHoSoList = (from m in db.MauHoSos
                                             join c in db.ChiTietMauHoSos on m.MauHoSoGUID equals c.MauHoSoGUID
                                             join l in db.CompanyCheckLists on c.ServiceGUID equals l.ServiceGUID
                                             where l.Status == 0 && c.HopDongGUID.ToString() == hopDongGUID &&
                                             l.ContractMemberGUID.ToString() == contractMemberGUID
                                             select m).Distinct().OrderBy(x => x.Loai).ToList();

                result.QueryResult = mauHoSoList;
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

        public static Result GetDichVuChayMauHoSo(string contractMemberGUID, string hopDongGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT M.Loai, S.ServiceGUID, S.[Name], S.EnglishName, ISNULL(Normal_Abnormal, CAST(0 AS bit)) AS Normal_Abnormal, ISNULL(Negative_Positive, CAST(0 AS bit)) AS Negative_Positive FROM  dbo.Services S WITH(NOLOCK) INNER JOIN dbo.CompanyCheckList L WITH(NOLOCK) ON S.ServiceGUID = L.ServiceGUID INNER JOIN dbo.MauHoSo M WITH(NOLOCK) INNER JOIN dbo.ChiTietMauHoSo C WITH(NOLOCK) ON M.MauHoSoGUID = C.MauHoSoGUID ON  L.ServiceGUID = C.ServiceGUID LEFT OUTER JOIN dbo.CauHinhDichVuXetNghiem X WITH(NOLOCK) ON S.ServiceGUID = X.ServiceGUID WHERE L.ContractMemberGUID = '{0}' AND C.HopDongGUID = '{1}' AND L.Status = 0 AND M.Loai IN (1, 2, 3) ORDER BY M.Loai, S.[Name]",
                    contractMemberGUID, hopDongGUID);
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

        
    }
}
