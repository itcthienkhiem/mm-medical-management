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
    public class InvoiceBus : BusBase
    {
        public static Result GetInvoiceList()
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM InvoiceView WITH(NOLOCK) WHERE Status={0} ORDER BY InvoiceDate DESC", (byte)Status.Actived);
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
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM InvoiceView WITH(NOLOCK) WHERE InvoiceDate BETWEEN '{0}' AND '{1}' ORDER BY InvoiceDate DESC",
                           fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM InvoiceView WITH(NOLOCK) WHERE Status={0} AND InvoiceDate BETWEEN '{1}' AND '{2}' ORDER BY InvoiceDate DESC",
                        (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM InvoiceView WITH(NOLOCK) WHERE Status={0} AND InvoiceDate BETWEEN '{1}' AND '{2}' ORDER BY InvoiceDate DESC",
                        (byte)Status.Deactived, fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"));
                    }

                }
                else
                {
                    if (type == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM InvoiceView WITH(NOLOCK) WHERE TenNguoiMuaHang LIKE N'%{0}%' ORDER BY InvoiceDate DESC", tenBenhNhan);
                    }
                    else if (type == 1) //Chưa xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM InvoiceView WITH(NOLOCK) WHERE Status={0} AND TenNguoiMuaHang LIKE N'%{1}%' ORDER BY InvoiceDate DESC",
                        (byte)Status.Actived, tenBenhNhan);
                    }
                    else //Đã xóa
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CASE ChuaThuTien WHEN 'True' THEN 'False' ELSE 'True' END AS DaThuTien FROM InvoiceView WITH(NOLOCK) WHERE Status={0} AND TenNguoiMuaHang LIKE N'%{1}%' ORDER BY InvoiceDate DESC",
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
                string query = string.Format("SELECT TenDichVu, SoLuong, DonViTinh, DonGia, ThanhTien FROM InvoiceDetail WITH(NOLOCK) WHERE InvoiceGUID='{0}' AND Status={1} ORDER BY TenDichVu", 
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

        public static Result GetNgayXuatHoaDon(string soHoaDon)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                Invoice invoice = (from i in db.Invoices
                                   where i.InvoiceCode == soHoaDon &&
                                   i.Status == (byte)Status.Deactived &&
                                   i.InvoiceDate >= Global.NgayThayDoiSoHoaDonSauCung
                                   orderby i.InvoiceDate descending
                                   select i).FirstOrDefault();

                if (invoice != null)
                    result.QueryResult = invoice.InvoiceDate;
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

        public static Result CheckInvoiceExistCode(int soHoaDon)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.SingleOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon && 
                    (q.DaXuat == true || q.XuatTruoc == true) && q.NgayBatDau.Value >= Global.NgayThayDoiSoHoaDonSauCung);

                if (qlshd == null)
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
                string query = string.Format("SELECT Name AS TenDichVu, CAST(N'Lần' AS nvarchar(5)) AS DonViTinh, CAST(1 AS int) AS SoLuong, CAST((Price - (Price * Discount)/100) AS float) AS DonGia, CAST((Price - (Price * Discount)/100) AS float) AS ThanhTien FROM ReceiptDetailView WITH(NOLOCK) WHERE ReceiptGUID='{0}' AND ReceiptDetailStatus={1} ORDER BY Code",
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

        public static Result GetChiTietPhieuThuDichVuList(List<DataRow> receiptList)
        {
            Result result = new Result();

            try
            {
                if (receiptList == null || receiptList.Count <= 0) return result;

                DataTable dtAll = null;

                foreach (DataRow row in receiptList)
                {
                    string receiptGUID = row["ReceiptGUID"].ToString();
                    string query = string.Format("SELECT Name AS TenDichVu, CAST(N'Lần' AS nvarchar(5)) AS DonViTinh, SoLuong, CAST((Price - (Price * Discount)/100) AS float) AS DonGia, CAST(((Price - (Price * Discount)/100) * SoLuong) AS float) AS ThanhTien FROM ReceiptDetailView WITH(NOLOCK) WHERE ReceiptGUID='{0}' AND ReceiptDetailStatus={1} ORDER BY Code",
                        receiptGUID, (byte)Status.Actived);
                    result = ExcuteQuery(query);

                    if (!result.IsOK) return result;

                    DataTable dt = result.QueryResult as DataTable;
                    if (dtAll == null)
                    {
                        dtAll = new DataTable();
                        dtAll = dt;
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            dtAll.ImportRow(dr);
                        }
                    }
                }

                result.QueryResult = dtAll;
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

        public static Result DeleteInvoices(List<string> invoiceKeys, List<string> nodeList)
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
                    foreach (string key in invoiceKeys)
                    {
                        Invoice invoice = db.Invoices.SingleOrDefault<Invoice>(i => i.InvoiceGUID.ToString() == key);
                        if (invoice != null)
                        {
                            invoice.DeletedDate = DateTime.Now;
                            invoice.DeletedBy = Guid.Parse(Global.UserGUID);
                            invoice.Status = (byte)Status.Deactived;

                            if (invoice.Notes == null || invoice.Notes.Trim() == string.Empty)
                                invoice.Notes = nodeList[index];
                            else
                                invoice.Notes += string.Format(" - {0}", nodeList[index]);

                            //Update Exported Invoice
                            if (invoice.ReceiptGUIDList != null && invoice.ReceiptGUIDList.Trim() != string.Empty)
                            {
                                string[] keys = invoice.ReceiptGUIDList.Split(',');
                                foreach (string receiptGUID in keys)
                                {
                                    Receipt receipt = db.Receipts.SingleOrDefault<Receipt>(r => r.ReceiptGUID.ToString() == receiptGUID);
                                    if (receipt != null) receipt.IsExportedInVoice = false;
                                }

                            }

                            int soHoaDon = Convert.ToInt32(invoice.InvoiceCode);
                            QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.SingleOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon &&
                                q.NgayBatDau.Value >= Global.NgayThayDoiSoHoaDonSauCung);
                            if (qlshd != null) qlshd.DaXuat = false;

                            string htttStr = Utility.ParseHinhThucThanhToanToStr((PaymentType)invoice.HinhThucThanhToan);

                            desc += string.Format("- GUID: '{0}', Mã hóa đơn: '{1}', Ngày xuất HĐ: '{2}', Người mua hàng: '{3}', Tên đơn vị: '{4}', Địa chỉ: '{5}', STK: '{6}', Hình thức thanh toán: '{7}', Ghi chú: '{8}', Đã thu tiền: '{9}'\n",
                                invoice.InvoiceGUID.ToString(), invoice.InvoiceCode, invoice.InvoiceDate.ToString("dd/MM/yyyy HH:mm:ss"), 
                                invoice.TenNguoiMuaHang, invoice.TenDonVi, invoice.DiaChi, invoice.SoTaiKhoan, htttStr, nodeList[index], !invoice.ChuaThuTien);
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

                    string htttStr = Utility.ParseHinhThucThanhToanToStr((PaymentType)invoice.HinhThucThanhToan);
                    
                    desc += string.Format("- Hóa đơn dịch vụ: GUID: '{0}', Mã hóa đơn: '{1}', Ngày xuất HĐ: '{2}', Người mua hàng: '{3}', Tên đơn vị: '{4}', Địa chỉ: '{5}', STK: '{6}', Hình thức thanh toán: '{7}', Ghi chú: '{8}', Đã thu tiền: '{9}'\n",
                                invoice.InvoiceGUID.ToString(), invoice.InvoiceCode, invoice.InvoiceDate.ToString("dd/MM/yyyy HH:mm:ss"),
                                invoice.TenNguoiMuaHang, invoice.TenDonVi, invoice.DiaChi, invoice.SoTaiKhoan, htttStr, invoice.Notes, !invoice.ChuaThuTien);

                    if (addedDetails != null && addedDetails.Count > 0)
                    {
                        desc += "- Chi tiết hóa đơn:\n";

                        foreach (InvoiceDetail detail in addedDetails)
                        {
                            detail.InvoiceDetailGUID = Guid.NewGuid();
                            detail.InvoiceGUID = invoice.InvoiceGUID;
                            db.InvoiceDetails.InsertOnSubmit(detail);

                            desc += string.Format("  + GUID: '{0}', Dịch vụ: '{1}', ĐVT: '{2}', Số lượng: '{3}', Đơn giá: '{4}', Thành tiền: '{5}', Loại: '{6}'\n",
                                detail.InvoiceDetailGUID.ToString(), detail.TenDichVu, detail.DonViTinh, detail.SoLuong, detail.DonGia, detail.ThanhTien, detail.Loai);
                        }

                        db.SubmitChanges();
                    }

                    //Update Exported Invoice
                    if (invoice.ReceiptGUIDList != null && invoice.ReceiptGUIDList.Trim() != string.Empty)
                    {
                        string[] keys = invoice.ReceiptGUIDList.Split(',');
                        foreach (string key in keys)
                        {
                            Receipt receipt = db.Receipts.SingleOrDefault<Receipt>(r => r.ReceiptGUID.ToString() == key);
                            if (receipt != null) receipt.IsExportedInVoice = true;    
                        }
                        
                    }

                    int soHoaDon = Convert.ToInt32(invoice.InvoiceCode);
                    QuanLySoHoaDon qlshd = db.QuanLySoHoaDons.SingleOrDefault<QuanLySoHoaDon>(q => q.SoHoaDon == soHoaDon &&
                        q.NgayBatDau.Value >= Global.NgayThayDoiSoHoaDonSauCung);
                    if (qlshd != null) qlshd.DaXuat = true;
                    else
                    {
                        qlshd = new QuanLySoHoaDon();
                        qlshd.QuanLySoHoaDonGUID = Guid.NewGuid();
                        qlshd.SoHoaDon = soHoaDon;
                        qlshd.DaXuat = true;
                        qlshd.XuatTruoc = false;
                        qlshd.NgayBatDau = Global.NgayThayDoiSoHoaDonSauCung;
                        db.QuanLySoHoaDons.InsertOnSubmit(qlshd);
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

        public static Result UpdateDaThuTienInvoice(string invoiceGUID, bool daThuTien)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    Invoice invoice = db.Invoices.SingleOrDefault<Invoice>(i => i.InvoiceGUID.ToString() == invoiceGUID);
                    if (invoice != null)
                    {
                        invoice.UpdatedDate = DateTime.Now;
                        invoice.UpdatedBy = Guid.Parse(Global.UserGUID);
                        invoice.ChuaThuTien = !daThuTien;

                        string htttStr = Utility.ParseHinhThucThanhToanToStr((PaymentType)invoice.HinhThucThanhToan);

                        desc += string.Format("- Hóa đơn dịch vụ: GUID: '{0}', Mã hóa đơn: '{1}', Ngày xuất HĐ: '{2}', Người mua hàng: '{3}', Tên đơn vị: '{4}', Địa chỉ: '{5}', STK: '{6}', Hình thức thanh toán: '{7}', Ghi chú: '{8}', Đã thu tiền: '{9}'\n",
                                    invoice.InvoiceGUID.ToString(), invoice.InvoiceCode, invoice.InvoiceDate.ToString("dd/MM/yyyy HH:mm:ss"),
                                    invoice.TenNguoiMuaHang, invoice.TenDonVi, invoice.DiaChi, invoice.SoTaiKhoan, htttStr, invoice.Notes, !invoice.ChuaThuTien);

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Cập nhật thông tin hóa đơn dịch vụ";
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
