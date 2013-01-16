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
    public class BenhNhanNgoaiGoiKhamBus : BusBase
    {
        public static Result GetBenhNhanNgoaiGoiKhamList(DateTime tuNgay, DateTime denNgay, string tenBenhNhan, int type)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (type == 0) //Tên bệnh nhân
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM BenhNhanNgoaiGoiKhamView WITH(NOLOCK) WHERE NgayKham BETWEEN '{0}' AND '{1}' AND FullName LIKE N'%{2}%' AND Status = {3} AND ServiceStatus = {3} AND Archived = 'False' ORDER BY NgayKham DESC",
                        tuNgay.ToString("yyyy-MM-dd 00:00:00"), denNgay.ToString("yyyy-MM-dd 23:59:59"), tenBenhNhan, (byte)Status.Actived);
                }
                else if (type == 1) //Mã bệnh nhân
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM BenhNhanNgoaiGoiKhamView WITH(NOLOCK) WHERE NgayKham BETWEEN '{0}' AND '{1}' AND FileNum LIKE N'%{2}%' AND Status = {3} AND ServiceStatus = {3} AND Archived = 'False' ORDER BY NgayKham DESC",
                        tuNgay.ToString("yyyy-MM-dd 00:00:00"), denNgay.ToString("yyyy-MM-dd 23:59:59"), tenBenhNhan, (byte)Status.Actived);
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

        public static Result DeleteBenhNhanNgoaiGoiKham(List<string> keys)
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
                        BenhNhanNgoaiGoiKham bnngk = db.BenhNhanNgoaiGoiKhams.SingleOrDefault<BenhNhanNgoaiGoiKham>(b => b.BenhNhanNgoaiGoiKhamGUID.ToString() == key);
                        if (bnngk != null)
                        {
                            bnngk.DeletedDate = DateTime.Now;
                            bnngk.DeletedBy = Guid.Parse(Global.UserGUID);
                            bnngk.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Ngày khám: '{1}', Bệnh nhân: '{2}', Dịch vụ: '{3}', Lần đầu: '{4}'\n",
                                bnngk.BenhNhanNgoaiGoiKhamGUID.ToString(), bnngk.NgayKham.ToString("dd/MM/yyyy HH:mm:ss"),
                                bnngk.Patient.Contact.FullName, bnngk.Service.Name, bnngk.LanDau == 0 ? "Lần đầu" : "Tái khám");
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa bệnh nhân ngoài gói khám";
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

        public static Result UpdateBenhNhanNgoaiGoiKham(BenhNhanNgoaiGoiKham benhNhanNgoaiGoiKham)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {

                    BenhNhanNgoaiGoiKham bnngk = db.BenhNhanNgoaiGoiKhams.SingleOrDefault<BenhNhanNgoaiGoiKham>(b => b.BenhNhanNgoaiGoiKhamGUID == benhNhanNgoaiGoiKham.BenhNhanNgoaiGoiKhamGUID);
                    if (bnngk != null)
                    {
                        bnngk.NgayKham = benhNhanNgoaiGoiKham.NgayKham;
                        bnngk.PatientGUID = benhNhanNgoaiGoiKham.PatientGUID;
                        bnngk.ServiceGUID = benhNhanNgoaiGoiKham.ServiceGUID;
                        bnngk.LanDau = benhNhanNgoaiGoiKham.LanDau;
                        bnngk.CreatedDate = benhNhanNgoaiGoiKham.CreatedDate;
                        bnngk.CreatedBy = benhNhanNgoaiGoiKham.CreatedBy;
                        bnngk.UpdatedDate = benhNhanNgoaiGoiKham.UpdatedDate;
                        bnngk.UpdatedBy = benhNhanNgoaiGoiKham.UpdatedBy;
                        bnngk.DeletedDate = benhNhanNgoaiGoiKham.DeletedDate;
                        bnngk.DeletedBy = benhNhanNgoaiGoiKham.DeletedBy;
                        bnngk.Status = benhNhanNgoaiGoiKham.Status;

                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Ngày khám: '{1}', Bệnh nhân: '{2}', Dịch vụ: '{3}', Lần đầu: '{4}'\n",
                                bnngk.BenhNhanNgoaiGoiKhamGUID.ToString(), bnngk.NgayKham.ToString("dd/MM/yyyy HH:mm:ss"),
                                bnngk.Patient.Contact.FullName, bnngk.Service.Name, bnngk.LanDau == 0 ? "Lần đầu" : "Tái khám");

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Edit;
                        tk.Action = "Sửa bệnh nhân ngoài gói khám";
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

        public static Result InsertBenhNhanNgoaiGoiKham(List<BenhNhanNgoaiGoiKham> benhNhanNgoaiGoiKhamList)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (BenhNhanNgoaiGoiKham bnngk in benhNhanNgoaiGoiKhamList)
                    {
                        bnngk.BenhNhanNgoaiGoiKhamGUID = Guid.NewGuid();
                        db.BenhNhanNgoaiGoiKhams.InsertOnSubmit(bnngk);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Ngày khám: '{1}', Bệnh nhân: '{2}', Dịch vụ: '{3}', Lần đầu: '{4}'\n",
                                bnngk.BenhNhanNgoaiGoiKhamGUID.ToString(), bnngk.NgayKham.ToString("dd/MM/yyyy HH:mm:ss"),
                                bnngk.Patient.Contact.FullName, bnngk.Service.Name, bnngk.LanDau == 0 ? "Lần đầu" : "Tái khám");

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm bệnh nhân ngoài gói khám";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);
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
