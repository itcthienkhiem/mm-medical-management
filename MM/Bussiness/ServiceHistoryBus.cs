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
    public class ServiceHistoryBus : BusBase
    {
        public static Result GetServiceHistory(string patientGUID, bool isAll, DateTime fromDate, DateTime toDate)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (isAll)
                {
                    if (Global.StaffType == StaffType.Admin || Global.StaffType == StaffType.Reception)
                        query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, *, CAST((FixedPrice - (FixedPrice * Discount)/100) AS float) AS Amount FROM ServiceHistoryView WHERE PatientGUID = '{0}' AND Status = {1} ORDER BY Name", 
                            patientGUID, (byte)Status.Actived);
                    else
                        query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, *, CAST((FixedPrice - (FixedPrice * Discount)/100) AS float) AS Amount FROM ServiceHistoryView WHERE PatientGUID = '{0}' AND Status = {1} AND DocStaffGUID = '{2}' ORDER BY Name", 
                            patientGUID, (byte)Status.Actived, Global.UserGUID);
                }
                else
                {
                    if (Global.StaffType == StaffType.Admin || Global.StaffType == StaffType.Reception)
                        query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, *, CAST((FixedPrice - (FixedPrice * Discount)/100) AS float) AS Amount FROM ServiceHistoryView WHERE PatientGUID = '{0}' AND ActivedDate BETWEEN '{1}' AND '{2}' AND Status = {3} ORDER BY Name",
                            patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived);
                    else
                        query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, *, CAST((FixedPrice - (FixedPrice * Discount)/100) AS float) AS Amount FROM ServiceHistoryView WHERE PatientGUID = '{0}' AND ActivedDate BETWEEN '{1}' AND '{2}' AND Status = {3} AND DocStaffGUID = '{4}' ORDER BY Name",
                            patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived, Global.UserGUID);
                }

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

        public static Result DeleteServiceHistory(List<String> serviceHistoryKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (string key in serviceHistoryKeys)
                    {
                        ServiceHistory srvHistory = db.ServiceHistories.SingleOrDefault<ServiceHistory>(s => s.ServiceHistoryGUID.ToString() == key);
                        if (srvHistory != null)
                        {
                            srvHistory.DeletedDate = DateTime.Now;
                            srvHistory.DeletedBy = Guid.Parse(Global.UserGUID);
                            srvHistory.Status = (byte)Status.Deactived;
                        }

                        db.SubmitChanges();
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

        public static Result InsertServiceHistory(ServiceHistory serviceHistory)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();

                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (serviceHistory.ServiceHistoryGUID == null || serviceHistory.ServiceHistoryGUID == Guid.Empty)
                    {
                        serviceHistory.ServiceHistoryGUID = Guid.NewGuid();
                        db.ServiceHistories.InsertOnSubmit(serviceHistory);
                    }
                    else //Update
                    {
                        ServiceHistory srvHistory = db.ServiceHistories.SingleOrDefault<ServiceHistory>(s => s.ServiceHistoryGUID.ToString() == serviceHistory.ServiceHistoryGUID.ToString());
                        if (srvHistory != null)
                        {
                            srvHistory.ActivedDate = serviceHistory.ActivedDate;
                            srvHistory.CreatedBy = serviceHistory.CreatedBy;
                            srvHistory.CreatedDate = serviceHistory.CreatedDate;
                            srvHistory.DeletedBy = serviceHistory.DeletedBy;
                            srvHistory.DeletedDate = serviceHistory.DeletedDate;
                            srvHistory.DocStaffGUID = serviceHistory.DocStaffGUID;
                            srvHistory.Note = serviceHistory.Note;
                            srvHistory.Price = serviceHistory.Price;
                            srvHistory.Discount = serviceHistory.Discount;
                            srvHistory.ServiceGUID = serviceHistory.ServiceGUID;
                            srvHistory.UpdatedBy = serviceHistory.UpdatedBy;
                            srvHistory.UpdatedDate = serviceHistory.UpdatedDate;
                            srvHistory.Status = serviceHistory.Status;
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
    }
}
