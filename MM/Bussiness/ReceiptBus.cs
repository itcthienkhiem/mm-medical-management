﻿using System;
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
    public class ReceiptBus : BusBase
    {
        public static Result GetReceiptList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ReceiptView WHERE Status={0} ORDER BY ReceiptDate", (byte)Status.Actived);
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

        public static Result GetReceiptCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM Receipt";
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

        public static Result GetReceipt(string receiptGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                ReceiptView receipt = db.ReceiptViews.SingleOrDefault<ReceiptView>(r => r.ReceiptGUID.ToString() == receiptGUID && r.Status == (byte)Status.Actived);
                result.QueryResult = receipt;
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

        public static Result GetReceiptDetailList(string receiptGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CAST((Price - (Price * Discount)/100) AS float) AS Amount FROM ReceiptDetailView WHERE ReceiptGUID='{0}' AND ReceiptDetailStatus={1} ORDER BY Code", 
                    receiptGUID, (byte)Status.Actived);
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

        public static Result DeleteReceipts(List<string> receiptKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (string key in receiptKeys)
                    {
                        Receipt r = db.Receipts.SingleOrDefault<Receipt>(rr => rr.ReceiptGUID.ToString() == key);
                        if (r != null)
                        {
                            r.DeletedDate = DateTime.Now;
                            r.DeletedBy = Guid.Parse(Global.UserGUID);
                            r.Status = (byte)Status.Deactived;

                            List<ReceiptDetail> receiptDetails = r.ReceiptDetails.ToList<ReceiptDetail>();
                            if (receiptDetails != null && receiptDetails.Count > 0)
                            {
                                foreach (var receiptDetail in receiptDetails)
                                {
                                    ServiceHistory serviceHistory = db.ServiceHistories.SingleOrDefault<ServiceHistory>(s => s.ServiceHistoryGUID == receiptDetail.ServiceHistoryGUID);
                                    if (serviceHistory != null)
                                        serviceHistory.IsExported = false;
                                }
                            }
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

        public static Result InsertReceipt(Receipt receipt, List<ReceiptDetail> receiptDetails)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();

                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    receipt.ReceiptGUID = Guid.NewGuid();
                    db.Receipts.InsertOnSubmit(receipt);
                    db.SubmitChanges();

                    //Detail
                    foreach (var receiptDetail in receiptDetails)
                    {
                        receiptDetail.ReceiptDetailGUID = Guid.NewGuid();
                        receiptDetail.ReceiptGUID = receipt.ReceiptGUID;

                        //Update Exported Service History
                        ServiceHistory serviceHistory = db.ServiceHistories.SingleOrDefault<ServiceHistory>(s => s.ServiceHistoryGUID == receiptDetail.ServiceHistoryGUID);
                        if (serviceHistory != null)
                            serviceHistory.IsExported = true;
                    }

                    db.ReceiptDetails.InsertAllOnSubmit(receiptDetails);

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