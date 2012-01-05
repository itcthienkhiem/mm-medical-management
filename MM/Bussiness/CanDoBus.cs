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
    public class CanDoBus : BusBase
    {
        public static Result GetCanDo(string patientGUID, DateTime fromDate, DateTime toDate)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM CanDo WHERE Status = {0} AND NgayCanDo BETWEEN '{1}' AND '{2}' AND PatientGUID = '{3}' ORDER BY NgayCanDo DESC",
                            (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), patientGUID);

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

        public static Result DeleteCanDo(List<String> canDoKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();

                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in canDoKeys)
                    {
                        CanDo cd = db.CanDos.SingleOrDefault<CanDo>(c => c.CanDoGuid.ToString() == key);
                        if (cd != null)
                        {
                            cd.DeletedDate = DateTime.Now;
                            cd.DeletedBy = Guid.Parse(Global.UserGUID);
                            cd.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Ngày cân đo: '{1}', Bệnh nhân: '{2}', Tim mạch: '{3}', Huyết áp: '{4}', Hô hấp: '{5}', Chiều cao: '{6}', Cân nặng: '{7}', BMI: '{8}', Cân đo khác: '{9}'\n",
                                cd.CanDoGuid.ToString(), cd.NgayCanDo.ToString("dd/MM/yyyy HH:mm:ss"), cd.Patient.Contact.FullName, cd.TimMach, cd.HuyetAp, 
                                cd.HoHap, cd.ChieuCao, cd.CanNang, cd.BMI, cd.CanDoKhac);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin cân đo";
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

        public static Result InsertCanDo(CanDo canDo)
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
                    if (canDo.CanDoGuid == null || canDo.CanDoGuid == Guid.Empty)
                    {
                        canDo.CanDoGuid = Guid.NewGuid();
                        db.CanDos.InsertOnSubmit(canDo);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Ngày cân đo: '{1}', Bệnh nhân: '{2}', Tim mạch: '{3}', Huyết áp: '{4}', Hô hấp: '{5}', Chiều cao: '{6}', Cân nặng: '{7}', BMI: '{8}', Cân đo khác: '{9}'",
                                canDo.CanDoGuid.ToString(), canDo.NgayCanDo.ToString("dd/MM/yyyy HH:mm:ss"), canDo.Patient.Contact.FullName, canDo.TimMach,
                                canDo.HuyetAp, canDo.HoHap, canDo.ChieuCao, canDo.CanNang, canDo.BMI, canDo.CanDoKhac);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin cân đo";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        CanDo cd = db.CanDos.SingleOrDefault<CanDo>(c => c.CanDoGuid.ToString() == canDo.CanDoGuid.ToString());
                        if (cd != null)
                        {
                            cd.PatientGUID = canDo.PatientGUID;
                            cd.NgayCanDo = canDo.NgayCanDo;
                            cd.TimMach = canDo.TimMach;
                            cd.HuyetAp = canDo.HuyetAp;
                            cd.HoHap = canDo.HoHap;
                            cd.ChieuCao = canDo.ChieuCao;
                            cd.CanNang = canDo.CanNang;
                            cd.BMI = canDo.BMI;
                            cd.CanDoKhac = canDo.CanDoKhac;
                            cd.CreatedBy = canDo.CreatedBy;
                            cd.CreatedDate = canDo.CreatedDate;
                            cd.DeletedBy = canDo.DeletedBy;
                            cd.DeletedDate = canDo.DeletedDate;
                            cd.UpdatedBy = canDo.UpdatedBy;
                            cd.UpdatedDate = canDo.UpdatedDate;
                            cd.Status = canDo.Status;

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Ngày cân đo: '{1}', Bệnh nhân: '{2}', Tim mạch: '{3}', Huyết áp: '{4}', Hô hấp: '{5}', Chiều cao: '{6}', Cân nặng: '{7}', BMI: '{8}', Cân đo khác: '{9}'",
                                cd.CanDoGuid.ToString(), cd.NgayCanDo.ToString("dd/MM/yyyy HH:mm:ss"), cd.Patient.Contact.FullName, cd.TimMach, cd.HuyetAp,
                                cd.HoHap, cd.ChieuCao, cd.CanNang, cd.BMI, cd.CanDoKhac);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin cân đo";
                            tk.Description = desc;
                            tk.TrackingType = (byte)TrackingType.None;
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
