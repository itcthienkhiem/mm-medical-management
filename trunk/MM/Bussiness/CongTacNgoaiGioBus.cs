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
    public class CongTacNgoaiGioBus : BusBase
    {
        public static Result GetCongTacNgoaiGioList(DateTime fromDate, DateTime toDate, string tenNhanVien)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                if (tenNhanVien.Trim() != string.Empty)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM CongTacNgoaiGioView WHERE Ngay BETWEEN '{0}' AND '{1}' AND TenNguoiLam LIKE N'{2}%' AND Status={3} ORDER BY Ngay DESC",
                               fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"), tenNhanVien, (byte)Status.Actived);
                }
                else
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM CongTacNgoaiGioView WHERE Ngay BETWEEN '{0}' AND '{1}' AND Status={2} ORDER BY Ngay DESC",
                               fromDate.ToString("yyyy-MM-dd HH:ss:mm"), toDate.ToString("yyyy-MM-dd HH:ss:mm"), (byte)Status.Actived);
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

        public static Result DeleteCongTacNgoaiGio(List<string> congtacNgoaiKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in congtacNgoaiKeys)
                    {
                        CongTacNgoaiGio ctng = db.CongTacNgoaiGios.SingleOrDefault<CongTacNgoaiGio>(c => c.CongTacNgoaiGioGUID.ToString() == key);
                        if (ctng != null)
                        {
                            ctng.DeletedDate = DateTime.Now;
                            ctng.DeletedBy = Guid.Parse(Global.UserGUID);
                            ctng.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Ngày: '{1}', Họ tên: '{2}', Mục đích: '{3}', Giờ vào: '{4}', Giờ ra: '{5}', Kết quả đánh giá: '{6}', Người đề xuất: '{7}', Ghi chú: '{8}'\n",
                                ctng.CongTacNgoaiGioGUID.ToString(), ctng.Ngay.ToString("dd/MM/yyyy"), ctng.DocStaff.Contact.FullName, ctng.MucDich, ctng.GioVao.Value.ToString("HH:mm"), ctng.GioRa.Value.ToString("HH:mm"),
                                ctng.KetQuaDanhGia, ctng.DocStaff.Contact.FullName, ctng.GhiChu);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin công tác ngoài giờ";
                    tk.Description = desc;
                    tk.TrackingType = (byte)TrackingType.None;
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

        public static Result InsertCongTacNgoaiGio(CongTacNgoaiGio ctng)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope tnx = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (ctng.CongTacNgoaiGioGUID == null || ctng.CongTacNgoaiGioGUID == Guid.Empty)
                    {
                        ctng.CongTacNgoaiGioGUID = Guid.NewGuid();
                        db.CongTacNgoaiGios.InsertOnSubmit(ctng);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Ngày: '{1}', Họ tên: '{2}', Mục đích: '{3}', Giờ vào: '{4}', Giờ ra: '{5}', Kết quả đánh giá: '{6}', Người đề xuất: '{7}', Ghi chú: '{8}'",
                                ctng.CongTacNgoaiGioGUID.ToString(), ctng.Ngay.ToString("dd/MM/yyyy"), ctng.DocStaff.Contact.FullName, ctng.MucDich, ctng.GioVao.Value.ToString("HH:mm"), ctng.GioRa.Value.ToString("HH:mm"),
                                ctng.KetQuaDanhGia, ctng.DocStaff.Contact.FullName, ctng.GhiChu);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin công tác ngoài giờ";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        CongTacNgoaiGio congTacNgoaiGio = db.CongTacNgoaiGios.SingleOrDefault<CongTacNgoaiGio>(c => c.CongTacNgoaiGioGUID.ToString() == ctng.CongTacNgoaiGioGUID.ToString());
                        if (congTacNgoaiGio != null)
                        {
                            congTacNgoaiGio.Ngay = ctng.Ngay;
                            congTacNgoaiGio.TenNguoiLam = ctng.TenNguoiLam;
                            congTacNgoaiGio.MucDich = ctng.MucDich;
                            congTacNgoaiGio.GioVao = ctng.GioVao;
                            congTacNgoaiGio.GioRa = ctng.GioRa;
                            congTacNgoaiGio.KetQuaDanhGia = ctng.KetQuaDanhGia;
                            congTacNgoaiGio.NguoiDeXuatGUID = ctng.NguoiDeXuatGUID;
                            congTacNgoaiGio.GhiChu = ctng.GhiChu;
                            congTacNgoaiGio.CreatedDate = ctng.CreatedDate;
                            congTacNgoaiGio.CreatedBy = ctng.CreatedBy;
                            congTacNgoaiGio.UpdatedDate = ctng.UpdatedDate;
                            congTacNgoaiGio.UpdatedBy = ctng.UpdatedBy;
                            congTacNgoaiGio.DeletedDate = ctng.DeletedDate;
                            congTacNgoaiGio.DeletedBy = ctng.DeletedBy;
                            congTacNgoaiGio.Status = ctng.Status;

                            db.SubmitChanges();

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Ngày: '{1}', Họ tên: '{2}', Mục đích: '{3}', Giờ vào: '{4}', Giờ ra: '{5}', Kết quả đánh giá: '{6}', Người đề xuất: '{7}', Ghi chú: '{8}'",
                               congTacNgoaiGio.CongTacNgoaiGioGUID.ToString(), congTacNgoaiGio.Ngay.ToString("dd/MM/yyyy"), congTacNgoaiGio.DocStaff.Contact.FullName, congTacNgoaiGio.MucDich,
                               congTacNgoaiGio.GioVao.Value.ToString("HH:mm"), congTacNgoaiGio.GioRa.Value.ToString("HH:mm"), congTacNgoaiGio.KetQuaDanhGia, congTacNgoaiGio.DocStaff.Contact.FullName, congTacNgoaiGio.GhiChu);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin công tác ngoài giờ";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
                            db.Trackings.InsertOnSubmit(tk);

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
