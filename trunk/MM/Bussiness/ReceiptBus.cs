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
    public class ReceiptBus : BusBase
    {
        public static Result GetReceiptList(bool isFromDateToDate, DateTime fromDate, DateTime toDate, string tenBenhNhan, int type)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                if (isFromDateToDate)
                {
                    if (type == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM ReceiptView WITH(NOLOCK) WHERE ReceiptDate BETWEEN '{0}' AND '{1}' ORDER BY ReceiptDate DESC",
                        fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM ReceiptView WITH(NOLOCK) WHERE Status={0} AND ReceiptDate BETWEEN '{1}' AND '{2}' ORDER BY ReceiptDate DESC",
                        (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM ReceiptView WITH(NOLOCK) WHERE Status={0} AND ReceiptDate BETWEEN '{1}' AND '{2}' ORDER BY ReceiptDate DESC",
                        (byte)Status.Deactived, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    
                }
                else
                {
                    if (type == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM ReceiptView WITH(NOLOCK) WHERE FullName LIKE N'%{0}%' ORDER BY ReceiptDate DESC", tenBenhNhan);
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM ReceiptView WITH(NOLOCK) WHERE Status={0} AND FullName LIKE N'%{1}%' ORDER BY ReceiptDate DESC",
                        (byte)Status.Actived, tenBenhNhan);
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM ReceiptView WITH(NOLOCK) WHERE Status={0} AND FullName LIKE N'%{1}%' ORDER BY ReceiptDate DESC",
                        (byte)Status.Deactived, tenBenhNhan);
                    }
                    
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

        public static Result GetChiTietPhieuThuDichVuList(bool isFromDateToDate, DateTime fromDate, DateTime toDate, string tenBenhNhan)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                if (isFromDateToDate)
                {
                    query = string.Format("SELECT * FROM dbo.ChiTietPhieuThuDichVuView WITH(NOLOCK) WHERE Status = 0 AND ReceiptDetailStatus = 0 AND ServiceStatus = 0 AND Archived = 'False' AND ReceiptDate BETWEEN '{0}' AND '{1}' ORDER BY ReceiptDate DESC, FullName",
                    fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    query = string.Format("SELECT * FROM dbo.ChiTietPhieuThuDichVuView WITH(NOLOCK) WHERE Status = 0 AND ReceiptDetailStatus = 0 AND ServiceStatus = 0 AND Archived = 'False' AND FullName LIKE N'%{0}%' ORDER BY ReceiptDate DESC, FullName",
                    tenBenhNhan);
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
                ReceiptView receipt = db.ReceiptViews.SingleOrDefault<ReceiptView>(r => r.ReceiptGUID.ToString() == receiptGUID);
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
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CAST((Price - (Price * Discount)/100) AS float) AS Amount FROM ReceiptDetailView WITH(NOLOCK) WHERE ReceiptGUID='{0}' AND ReceiptDetailStatus={1} ORDER BY Code", 
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

        public static Result DeleteReceipts(List<string> receiptKeys, List<string> noteList)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    int index = 0;
                    foreach (string key in receiptKeys)
                    {
                        Receipt r = db.Receipts.SingleOrDefault<Receipt>(rr => rr.ReceiptGUID.ToString() == key);
                        if (r != null)
                        {
                            r.Notes = noteList[index];
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

                            desc += string.Format("- GUID: '{0}', Mã phiếu thu: '{1}', Ngày thu: '{2}', Mã bệnh nhân: '{3}', Tên bệnh nhân: '{4}', Địa chỉ: '{5}', Ghi chú: '{6}', Đã thu tiền: '{7}', Lý do giảm: '{8}'\n",
                                r.ReceiptGUID.ToString(), r.ReceiptCode, r.ReceiptDate.ToString("dd/MM/yyyy HH:mm:ss"), r.Patient.FileNum, 
                                r.Patient.Contact.FullName, r.Patient.Contact.Address, noteList[index], !r.ChuaThuTien, r.LyDoGiam);
                        }

                        index++;
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin phiếu thu";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.Price;
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

        public static Result InsertReceipt(Receipt receipt, List<ReceiptDetail> receiptDetails)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    receipt.ReceiptGUID = Guid.NewGuid();
                    db.Receipts.InsertOnSubmit(receipt);
                    db.SubmitChanges();

                    desc += string.Format("- Phiếu thu: GUID: '{0}', Mã phiếu thu: '{1}', Ngày thu: '{2}', Mã bệnh nhân: '{3}', Tên bệnh nhân: '{4}', Địa chỉ: '{5}', Ghi chú: '{6}', Đã thu tiền: '{7}', Lý do giảm: '{8}'\n",
                               receipt.ReceiptGUID.ToString(), receipt.ReceiptCode, receipt.ReceiptDate.ToString("dd/MM/yyyy HH:mm:ss"), receipt.Patient.FileNum,
                               receipt.Patient.Contact.FullName, receipt.Patient.Contact.Address, receipt.Notes, !receipt.ChuaThuTien, receipt.LyDoGiam);

                    desc += "- Chi tiết phiếu thu được thêm:\n";

                    //Detail
                    foreach (var receiptDetail in receiptDetails)
                    {
                        receiptDetail.ReceiptDetailGUID = Guid.NewGuid();
                        receiptDetail.ReceiptGUID = receipt.ReceiptGUID;
                        db.ReceiptDetails.InsertOnSubmit(receiptDetail);
                        db.SubmitChanges();

                        desc += string.Format("  + GUID: '{0}', Dịch vụ: '{1}', Đơn giá: '{2}', Số lượng: '1', Giảm: '{3}', Thành tiền: '{4}'\n",
                            receiptDetail.ReceiptDetailGUID.ToString(), receiptDetail.ServiceHistory.Service.Name, receiptDetail.ServiceHistory.Price.Value,
                            receiptDetail.ServiceHistory.Discount, 
                            Math.Round(receiptDetail.ServiceHistory.Price.Value - (receiptDetail.ServiceHistory.Price.Value * receiptDetail.ServiceHistory.Discount / 100), 0));

                        //Update Exported Service History
                        ServiceHistory serviceHistory = db.ServiceHistories.SingleOrDefault<ServiceHistory>(s => s.ServiceHistoryGUID == receiptDetail.ServiceHistoryGUID);
                        if (serviceHistory != null)
                            serviceHistory.IsExported = true;
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Add;
                    tk.Action = "Thêm thông tin phiếu thu";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.Price;
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

        public static Result GetBacSiChiDinh(string serviceHistoryGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                DichVuChiDinh dvcd = db.DichVuChiDinhs.SingleOrDefault(d => d.ServiceHistoryGUID.ToString() == serviceHistoryGUID && d.Status == (byte)Status.Actived);
                if (dvcd != null && dvcd.ChiTietChiDinh.Status == (byte)Status.Actived && 
                    dvcd.ChiTietChiDinh.ChiDinh.Status == (byte)Status.Actived &&
                    dvcd.ChiTietChiDinh.ChiDinh.DocStaff.Contact.Archived == false)
                {
                    result.QueryResult = dvcd.ChiTietChiDinh.ChiDinh.DocStaff.Contact.FullName;
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

        public static Result CapNhatTrangThaiPhieuThu(string receiptGUID, bool daXuatHD, bool daThuTien)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    Receipt receipt = db.Receipts.SingleOrDefault<Receipt>(r => r.ReceiptGUID.ToString() == receiptGUID);
                    if (receipt != null)
                    {
                        receipt.UpdatedDate = DateTime.Now;
                        receipt.UpdatedBy = Guid.Parse(Global.UserGUID);
                        receipt.IsExportedInVoice = daXuatHD;
                        receipt.ChuaThuTien = !daThuTien;

                        string desc = string.Format("Phiếu thu: GUID: '{0}', Mã phiếu thu: '{1}', Ngày thu: '{2}', Mã bệnh nhân: '{3}', Tên bệnh nhân: '{4}', Địa chỉ: '{5}', Ghi chú: '{6}', Đã thu tiền: '{7}', Đã xuất HĐ: '{8}'",
                               receipt.ReceiptGUID.ToString(), receipt.ReceiptCode, receipt.ReceiptDate.ToString("dd/MM/yyyy HH:mm:ss"), receipt.Patient.FileNum,
                               receipt.Patient.Contact.FullName, receipt.Patient.Contact.Address, receipt.Notes, !receipt.ChuaThuTien, receipt.IsExportedInVoice);

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Edit;
                        tk.Action = "Sửa trạng thái phiếu thu";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

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
    }
}
