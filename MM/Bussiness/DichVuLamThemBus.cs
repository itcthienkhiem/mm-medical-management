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
    public class DichVuLamThemBus : BusBase
    {
        public static Result GetDichVuLamThemList(string contractMemberGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, *, CAST((FixedPrice - (FixedPrice * Discount)/100) AS float) AS Amount FROM DichVuLamThemView WITH(NOLOCK) WHERE ContractMemberGUID = '{0}' AND Status = 0 AND ServiceStatus = 0", contractMemberGUID);
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

        public static Result DeleteDichVuLamThem(List<string> keys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in keys)
                    {
                        DichVuLamThem s = db.DichVuLamThems.SingleOrDefault<DichVuLamThem>(ss => ss.DichVuLamThemGUID.ToString() == key);
                        if (s != null)
                        {
                            s.DeletedDate = DateTime.Now;
                            s.DeletedBy = Guid.Parse(Global.UserGUID);
                            s.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Mã dịch vụ: '{1}', Tên dịch vụ: '{2}', Tên tiếng anh: '{3}', Giá: '{4}', Giảm: '{5}', Ngày sử dụng: '{6}', Đã thu tiền: '{7}'\n",
                                s.DichVuLamThemGUID.ToString(), s.Service.Code, s.Service.Name, s.Service.EnglishName, s.Price, s.Discount, s.ActiveDate.ToString("dd/MM/yyyy HH:mm:ss"), s.DaThuTien);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa dịch vụ làm thêm";
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

        public static Result InsertDichVuLamThem(DichVuLamThem dichVuLamThem)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    //Insert
                    if (dichVuLamThem.DichVuLamThemGUID == null || dichVuLamThem.DichVuLamThemGUID == Guid.Empty)
                    {
                        dichVuLamThem.DichVuLamThemGUID = Guid.NewGuid();
                        db.DichVuLamThems.InsertOnSubmit(dichVuLamThem);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Mã dịch vụ: '{1}', Tên dịch vụ: '{2}', Tên tiếng anh: '{3}', Giá: '{4}', Giảm: '{5}', Ngày sử dụng: '{6}', Đã thu tiền: '{7}'",
                                dichVuLamThem.DichVuLamThemGUID.ToString(), dichVuLamThem.Service.Code, dichVuLamThem.Service.Name,
                                dichVuLamThem.Service.EnglishName, dichVuLamThem.Price, dichVuLamThem.Discount,
                                dichVuLamThem.ActiveDate.ToString("dd/MM/yyyy HH:mm:ss"), dichVuLamThem.DaThuTien);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm dịch vụ làm thêm";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.Price;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        DichVuLamThem dvlt = db.DichVuLamThems.SingleOrDefault<DichVuLamThem>(s => s.DichVuLamThemGUID == dichVuLamThem.DichVuLamThemGUID);
                        if (dvlt != null)
                        {
                            dvlt.ServiceGUID = dichVuLamThem.ServiceGUID;
                            dvlt.ActiveDate = dichVuLamThem.ActiveDate;
                            dvlt.Price = dichVuLamThem.Price;
                            dvlt.Discount = dichVuLamThem.Discount;
                            dvlt.DaThuTien = dichVuLamThem.DaThuTien;
                            dvlt.Note = dichVuLamThem.Note;
                            dvlt.CreatedDate = dichVuLamThem.CreatedDate;
                            dvlt.CreatedBy = dichVuLamThem.CreatedBy;
                            dvlt.UpdatedDate = dichVuLamThem.UpdatedDate;
                            dvlt.UpdatedBy = dichVuLamThem.UpdatedBy;
                            dvlt.DeletedDate = dichVuLamThem.DeletedDate;
                            dvlt.DeletedBy = dichVuLamThem.DeletedBy;
                            dvlt.Status = dichVuLamThem.Status;

                            db.SubmitChanges();

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Mã dịch vụ: '{1}', Tên dịch vụ: '{2}', Tên tiếng anh: '{3}', Giá: '{4}', Giảm: '{5}', Ngày sử dụng: '{6}', ContractMemberGUID: '{7}', Đã thu tiền: '{8}'",
                                dvlt.DichVuLamThemGUID.ToString(), dvlt.Service.Code, dvlt.Service.Name, dvlt.Service.EnglishName, dvlt.Price,
                                dvlt.Discount, dvlt.ActiveDate.ToString("dd/MM/yyyy HH:mm:ss"), dvlt.ContractMemberGUID.ToString(), dvlt.DaThuTien);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa dịch vụ làm thêm";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.Price;
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
