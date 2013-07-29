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
                string query = string.Empty;

                //if (Global.StaffType != StaffType.DieuDuong)
                {
                    query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, *, 'R(P): ' + MatPhai + '; L(T): ' + MatTrai + '; ' + CASE HieuChinh WHEN 'True' THEN N'Hiệu chỉnh' ELSE N'Không hiệu chỉnh' END AS ThiLuc FROM CanDoView WITH(NOLOCK) WHERE Status = {0} AND NgayCanDo BETWEEN '{1}' AND '{2}' AND PatientGUID = '{3}' ORDER BY NgayCanDo DESC",
                            (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), patientGUID);
                }
                //else
                //{
                //    query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, *, 'R(P): ' + MatPhai + '; L(T): ' + MatTrai + '; ' + CASE HieuChinh WHEN 'True' THEN N'Hiệu chỉnh' ELSE N'Không hiệu chỉnh' END AS ThiLuc FROM CanDoView WHERE Status = {0} AND NgayCanDo BETWEEN '{1}' AND '{2}' AND PatientGUID = '{3}' AND Archived = 'False' AND DocStaffGUID = '{4}' ORDER BY NgayCanDo DESC",
                //            (byte)Status.Actived, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), patientGUID,
                //            Global.UserGUID);
                //}

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

                            desc += string.Format("- GUID: '{0}', Ngày cân đo: '{1}', Bệnh nhân: '{2}', Người khám: '{3}', Tim mạch: '{4}', Huyết áp: '{5}', Hô hấp: '{6}', Chiều cao: '{7}', Cân nặng: '{8}', BMI: '{9}', Mù màu: '{10}', Thị lực: 'R(P): {11}; L(T): {12}; {13}'\n",
                                cd.CanDoGuid.ToString(), cd.NgayCanDo.ToString("dd/MM/yyyy HH:mm:ss"), cd.Patient.Contact.FullName, cd.DocStaff.Contact.FullName, cd.TimMach,
                                cd.HuyetAp, cd.HoHap, cd.ChieuCao, cd.CanNang, cd.BMI, cd.MuMau, cd.MatPhai, cd.MatTrai, cd.HieuChinh ? "Hiệu chỉnh" : "Không hiệu chỉnh");
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
                        desc += string.Format("- GUID: '{0}', Ngày cân đo: '{1}', Bệnh nhân: '{2}', Người khám: '{3}', Tim mạch: '{4}', Huyết áp: '{5}', Hô hấp: '{6}', Chiều cao: '{7}', Cân nặng: '{8}', BMI: '{9}', Mù màu: '{10}', Thị lực: 'R(P): {11}; L(T): {12}; {13}'",
                                canDo.CanDoGuid.ToString(), canDo.NgayCanDo.ToString("dd/MM/yyyy HH:mm:ss"), canDo.Patient.Contact.FullName,
                                canDo.DocStaff.Contact.FullName, canDo.TimMach, canDo.HuyetAp, canDo.HoHap, canDo.ChieuCao, canDo.CanNang, canDo.BMI,
                                canDo.MuMau, canDo.MatPhai, canDo.MatTrai, canDo.HieuChinh ? "Hiệu chỉnh" : "Không hiệu chỉnh");

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin cân đo";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        CanDo cd = db.CanDos.SingleOrDefault<CanDo>(c => c.CanDoGuid.ToString() == canDo.CanDoGuid.ToString());
                        if (cd != null)
                        {
                            cd.PatientGUID = canDo.PatientGUID;
                            cd.DocStaffGUID = canDo.DocStaffGUID;
                            cd.NgayCanDo = canDo.NgayCanDo;
                            cd.TimMach = canDo.TimMach;
                            cd.HuyetAp = canDo.HuyetAp;
                            cd.HoHap = canDo.HoHap;
                            cd.ChieuCao = canDo.ChieuCao;
                            cd.CanNang = canDo.CanNang;
                            cd.BMI = canDo.BMI;
                            cd.MuMau = canDo.MuMau;
                            cd.MatPhai = canDo.MatPhai;
                            cd.MatTrai = canDo.MatTrai;
                            cd.HieuChinh = canDo.HieuChinh;
                            cd.CanDoKhac = canDo.CanDoKhac;
                            cd.CreatedBy = canDo.CreatedBy;
                            cd.CreatedDate = canDo.CreatedDate;
                            cd.DeletedBy = canDo.DeletedBy;
                            cd.DeletedDate = canDo.DeletedDate;
                            cd.UpdatedBy = canDo.UpdatedBy;
                            cd.UpdatedDate = canDo.UpdatedDate;
                            cd.Status = canDo.Status;

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Ngày cân đo: '{1}', Bệnh nhân: '{2}', Người khám: '{3}', Tim mạch: '{4}', Huyết áp: '{5}', Hô hấp: '{6}', Chiều cao: '{7}', Cân nặng: '{8}', BMI: '{9}', Mù màu: '{10}', Thị lực: 'R(P): {11}; L(T): {12}; {13}'",
                                 cd.CanDoGuid.ToString(), cd.NgayCanDo.ToString("dd/MM/yyyy HH:mm:ss"), cd.Patient.Contact.FullName, cd.DocStaff.Contact.FullName, cd.TimMach,
                                 cd.HuyetAp, cd.HoHap, cd.ChieuCao, cd.CanNang, cd.BMI, cd.MuMau, cd.MatPhai, cd.MatTrai, cd.HieuChinh ? "Hiệu chỉnh" : "Không hiệu chỉnh");

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin cân đo";
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

        public static Result GetLastCanDo(string patientGUID, DateTime fromDate, DateTime toDate)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                CanDo canDo = (from cd in db.CanDos
                            where cd.PatientGUID.ToString() == patientGUID &&
                            cd.NgayCanDo >= fromDate && cd.NgayCanDo <= toDate &&
                            cd.Status == (byte)Status.Actived
                            orderby cd.NgayCanDo descending
                            select cd).FirstOrDefault<CanDo>();

                result.QueryResult = canDo;
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

        public static Result GetNgayCanDoCuoiCung(string patientGUID)
        {
            Result result = new Result();

            try
            {
                string query = string.Format("SELECT Max(NgayCanDo) FROM CanDo WITH(NOLOCK) WHERE PatientGUID = '{0}' AND Status = {1}",
                    patientGUID, (byte)Status.Actived);

                result = ExcuteQuery(query);

                if (!result.IsOK) return result;

                DataTable dt = result.QueryResult as DataTable;
                if (dt == null || dt.Rows.Count <= 0)
                    result.QueryResult = Global.MinDateTime;
                else
                {
                    if (dt.Rows[0][0] != null && dt.Rows[0][0] != DBNull.Value)
                        result.QueryResult = Convert.ToDateTime(dt.Rows[0][0]);
                    else result.QueryResult = Global.MinDateTime;
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

        public static Result ChuyenBenhAn(string patientGUID, List<DataRow> rows)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (DataRow row in rows)
                    {
                        string canDoGUID = row["CanDoGuid"].ToString();
                        CanDo canDo = (from s in db.CanDos
                                       where s.CanDoGuid.ToString() == canDoGUID
                                       select s).FirstOrDefault();

                        if (canDo == null) continue;

                        //Tracking
                        string desc = string.Format("- CanDoGUID: '{0}': PatientGUID: '{1}' ==> '{2}' (CanDo)",
                            canDoGUID, canDo.PatientGUID.ToString(), patientGUID);

                        canDo.PatientGUID = Guid.Parse(patientGUID);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Edit;
                        tk.Action = "Chuyển bệnh án";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);
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
