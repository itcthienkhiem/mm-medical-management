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
    public class PhieuChiBus : BusBase
    {
        public static Result GetPhieuChiList(DateTime fromDate, DateTime toDate)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM PhieuChi WITH(NOLOCK) WHERE NgayChi BETWEEN '{0}' AND '{1}' AND Status = 0 ORDER BY NgayChi DESC, SoPhieuChi ASC",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));

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

        public static Result GetPhieuChiCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM PhieuChi WITH(NOLOCK)";
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

        public static Result DeletePhieuChi(List<string> phieuChiKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in phieuChiKeys)
                    {
                        PhieuChi pc = db.PhieuChis.SingleOrDefault<PhieuChi>(ss => ss.PhieuChiGUID.ToString() == key);
                        if (pc != null)
                        {
                            pc.DeletedDate = DateTime.Now;
                            pc.DeletedBy = Guid.Parse(Global.UserGUID);
                            pc.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Sồ phiếu chi: '{1}', Ngày chi: '{2}', Số tiền: '{3}', Ghi chú: '{4}'\n",
                                pc.PhieuChiGUID.ToString(), pc.SoPhieuChi, pc.NgayChi.ToString("dd/MM/yyyy HH:mm:ss"), pc.SoTien, pc.DienGiai);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin phiếu chi";
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

        public static Result InsertPhieuChi(PhieuChi phieuChi)
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
                    if (phieuChi.PhieuChiGUID == null || phieuChi.PhieuChiGUID == Guid.Empty)
                    {
                        phieuChi.PhieuChiGUID = Guid.NewGuid();
                        db.PhieuChis.InsertOnSubmit(phieuChi);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Sồ phiếu chi: '{1}', Ngày chi: '{2}', Số tiền: '{3}', Ghi chú: '{4}'",
                               phieuChi.PhieuChiGUID.ToString(), phieuChi.SoPhieuChi, phieuChi.NgayChi.ToString("dd/MM/yyyy HH:mm:ss"), phieuChi.SoTien, phieuChi.DienGiai);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin phiếu chi";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        PhieuChi pc = db.PhieuChis.SingleOrDefault<PhieuChi>(s => s.PhieuChiGUID.ToString() == phieuChi.PhieuChiGUID.ToString());
                        if (pc != null)
                        {
                            pc.SoPhieuChi = phieuChi.SoPhieuChi;
                            pc.NgayChi = phieuChi.NgayChi;
                            pc.SoTien = phieuChi.SoTien;
                            pc.DienGiai = phieuChi.DienGiai;
                            pc.CreatedDate = phieuChi.CreatedDate;
                            pc.CreatedBy = phieuChi.CreatedBy;
                            pc.UpdatedDate = phieuChi.UpdatedDate;
                            pc.UpdatedBy = phieuChi.UpdatedBy;
                            pc.DeletedDate = phieuChi.DeletedDate;
                            pc.DeletedBy = phieuChi.DeletedBy;
                            pc.Status = phieuChi.Status;

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Sồ phiếu chi: '{1}', Ngày chi: '{2}', Số tiền: '{3}', Ghi chú: '{4}'",
                                pc.PhieuChiGUID.ToString(), pc.SoPhieuChi, pc.NgayChi.ToString("dd/MM/yyyy HH:mm:ss"), pc.SoTien, pc.DienGiai);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin phiếu chi";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            tk.ComputerName = Utility.GetDNSHostName();
                            db.Trackings.InsertOnSubmit(tk);

                            db.SubmitChanges();
                        }
                    }

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
