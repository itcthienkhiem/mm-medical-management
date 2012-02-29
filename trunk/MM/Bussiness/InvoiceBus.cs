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
    public class InvoiceBus : BusBase
    {
        public static Result GetInvoiceList()
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM InvoiceView WHERE Status={0} ORDER BY InvoiceDate DESC", (byte)Status.Actived);
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

        public static Result GetInvoiceList(bool isFromDateToDate, DateTime fromDate, DateTime toDate, string tenBenhNhan, int type)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                if (isFromDateToDate)
                {
                    if (type == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM InvoiceView WHERE InvoiceDate BETWEEN '{0}' AND '{1}' ORDER BY InvoiceDate DESC",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM InvoiceView WHERE Status={0} AND InvoiceDate BETWEEN '{1}' AND '{2}' ORDER BY InvoiceDate DESC",
                        (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM InvoiceView WHERE Status={0} AND InvoiceDate BETWEEN '{1}' AND '{2}' ORDER BY InvoiceDate DESC",
                        (byte)Status.Deactived, fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }

                }
                else
                {
                    if (type == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM InvoiceView WHERE TenNguoiMuaHang LIKE N'%{0}%' ORDER BY InvoiceDate DESC", tenBenhNhan);
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM InvoiceView WHERE Status={0} AND TenNguoiMuaHang LIKE N'%{1}%' ORDER BY InvoiceDate DESC",
                        (byte)Status.Actived, tenBenhNhan);
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM InvoiceView WHERE Status={0} AND TenNguoiMuaHang LIKE N'%{1}%' ORDER BY InvoiceDate DESC",
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

        public static Result GetInvoiceDetail(string invoiceGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT TenDichVu, SoLuong, DonViTinh, DonGia, ThanhTien FROM InvoiceDetail WHERE InvoiceGUID='{0}' AND Status={1} ORDER BY TenDichVu", 
                    invoiceGUID, (byte)Status.Actived);
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

        public static Result GetInvoiceCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM Invoice";
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

        public static Result CheckInvoiceExistCode(string invoiceGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                Invoice invoice = null;
                if (invoiceGUID == null || invoiceGUID == string.Empty)
                    invoice = db.Invoices.SingleOrDefault<Invoice>(i => i.InvoiceCode.ToLower() == code.ToLower());
                else
                    invoice = db.Invoices.SingleOrDefault<Invoice>(i => i.InvoiceCode.ToLower() == code.ToLower() &&
                                                                i.InvoiceGUID.ToString() != invoiceGUID);

                if (invoice == null)
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

        public static Result GetChiTietPhieuThuDichVuList(string receiptGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT Name AS TenDichVu, CAST(N'Lần' AS nvarchar(5)) AS DonViTinh, CAST(1 AS int) AS SoLuong, CAST((Price - (Price * Discount)/100) AS float) AS DonGia, CAST((Price - (Price * Discount)/100) AS float) AS ThanhTien FROM ReceiptDetailView WHERE ReceiptGUID='{0}' AND ReceiptDetailStatus={1} ORDER BY Code",
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

        public static Result GetInvoice(string invoiceGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                InvoiceView invoice = db.InvoiceViews.SingleOrDefault<InvoiceView>(i => i.InvoiceGUID.ToString() == invoiceGUID && i.Status == (byte)Status.Actived);
                result.QueryResult = invoice;
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

        public static Result DeleteInvoices(List<string> invoiceKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in invoiceKeys)
                    {
                        Invoice invoice = db.Invoices.SingleOrDefault<Invoice>(i => i.InvoiceGUID.ToString() == key);
                        if (invoice != null)
                        {
                            invoice.DeletedDate = DateTime.Now;
                            invoice.DeletedBy = Guid.Parse(Global.UserGUID);
                            invoice.Status = (byte)Status.Deactived;

                            //Update Exported Invoice
                            invoice.Receipt.IsExportedInVoice = false;

                            desc += string.Format("- GUID: '{0}', Mã hóa đơn: '{1}', Ngày xuất HĐ: '{2}', Người mua hàng: '{3}', Tên đơn vị: '{4}', Địa chỉ: '{5}', STK: '{6}', Hình thức thanh toán: '{7}'\n",
                                invoice.InvoiceGUID.ToString(), invoice.InvoiceCode, invoice.InvoiceDate.ToString("dd/MM/yyyy HH:mm:ss"), 
                                invoice.TenNguoiMuaHang, invoice.TenDonVi, invoice.DiaChi, invoice.SoTaiKhoan, 
                                invoice.HinhThucThanhToan == 0 ? "Tiền mặt" : "Chuyển khoản");
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin hóa đơn dịch vụ";
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

        public static Result InsertInvoice(Invoice invoice, List<InvoiceDetail> addedDetails)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    invoice.InvoiceGUID = Guid.NewGuid();
                    db.Invoices.InsertOnSubmit(invoice);
                    db.SubmitChanges();

                    desc += string.Format("- Hóa đơn dịch vụ: GUID: '{0}', Mã hóa đơn: '{1}', Ngày xuất HĐ: '{2}', Người mua hàng: '{3}', Tên đơn vị: '{4}', Địa chỉ: '{5}', STK: '{6}', Hình thức thanh toán: '{7}'\n",
                                invoice.InvoiceGUID.ToString(), invoice.InvoiceCode, invoice.InvoiceDate.ToString("dd/MM/yyyy HH:mm:ss"),
                                invoice.TenNguoiMuaHang, invoice.TenDonVi, invoice.DiaChi, invoice.SoTaiKhoan,
                                invoice.HinhThucThanhToan == 0 ? "Tiền mặt" : "Chuyển khoản");

                    if (addedDetails != null && addedDetails.Count > 0)
                    {
                        desc += "- Chi tiết hóa đơn:\n";

                        foreach (InvoiceDetail detail in addedDetails)
                        {
                            detail.InvoiceDetailGUID = Guid.NewGuid();
                            detail.InvoiceGUID = invoice.InvoiceGUID;
                            db.InvoiceDetails.InsertOnSubmit(detail);

                            desc += string.Format("  + GUID: '{0}', Dịch vụ: '{1}', ĐVT: '{2}', Số lượng: '{3}', Đơn giá: '{4}', Thành tiền: '{5}'\n",
                                detail.InvoiceDetailGUID.ToString(), detail.TenDichVu, detail.DonViTinh, detail.SoLuong, detail.DonGia, detail.ThanhTien);
                        }

                        db.SubmitChanges();
                    }

                    //Update Exported Invoice
                    Receipt receipt = db.Receipts.SingleOrDefault<Receipt>(r => r.ReceiptGUID == invoice.ReceiptGUID);
                    if (receipt != null) receipt.IsExportedInVoice = true;

                    var settings = from s in db.Settings
                                      select s;
                    if (settings != null)
                    {
                        foreach (var setting in settings)
                        {
                            setting.SoHoaDonBatDau = Convert.ToInt32(invoice.InvoiceCode);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Add;
                    tk.Action = "Thêm thông tin hóa đơn dịch vụ";
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
    }
}
