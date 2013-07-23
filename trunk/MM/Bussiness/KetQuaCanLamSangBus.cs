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
    public class KetQuaCanLamSangBus : BusBase
    {
        public static Result GetKetQuaCanLamSangList(string patientGUID, bool isAll, DateTime fromDate, DateTime toDate)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                if (isAll)
                {
                    //if (Global.StaffType != StaffType.BacSi && Global.StaffType != StaffType.BacSiSieuAm &&
                    //    Global.StaffType != StaffType.BacSiNgoaiTongQuat && Global.StaffType != StaffType.BacSiNoiTongQuat &&
                    //    Global.StaffType != StaffType.BacSiPhuKhoa && Global.StaffType != StaffType.XetNghiem)
                    //    query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM KetQuaCanLamSangView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND KetQuaCanLamSangStatus = {1} AND ServiceStatus = {1} ORDER BY Name",
                    //        patientGUID, (byte)Status.Actived);
                    //else
                    //    query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM KetQuaCanLamSangView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND KetQuaCanLamSangStatus = {1} AND ServiceStatus = {1} AND (BacSiThucHienGUID = '{2}' OR BacSiThucHienGUID IS NULL) ORDER BY Name",
                    //        patientGUID, (byte)Status.Actived, Global.UserGUID);

                    query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM KetQuaCanLamSangView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND KetQuaCanLamSangStatus = {1} AND ServiceStatus = {1} ORDER BY Name",
                            patientGUID, (byte)Status.Actived);
                }
                else
                {
                    query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM KetQuaCanLamSangView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND NgayKham BETWEEN '{1}' AND '{2}' AND KetQuaCanLamSangStatus = {3} AND ServiceStatus = {3} ORDER BY Name",
                            patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived);

                    //if (Global.StaffType != StaffType.BacSi && Global.StaffType != StaffType.BacSiSieuAm &&
                    //    Global.StaffType != StaffType.BacSiNgoaiTongQuat && Global.StaffType != StaffType.BacSiNoiTongQuat &&
                    //    Global.StaffType != StaffType.BacSiPhuKhoa && Global.StaffType != StaffType.XetNghiem)
                    //    query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM KetQuaCanLamSangView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND NgayKham BETWEEN '{1}' AND '{2}' AND KetQuaCanLamSangStatus = {3} AND ServiceStatus = {3} ORDER BY Name",
                    //        patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived);
                    //else
                    //    query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM KetQuaCanLamSangView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND NgayKham BETWEEN '{1}' AND '{2}' AND KetQuaCanLamSangStatus = {3} AND ServiceStatus = {3} AND (BacSiThucHienGUID = '{4}' OR BacSiThucHienGUID IS NULL) ORDER BY Name",
                    //        patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived, Global.UserGUID);
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

        public static Result GetKetQuaCanLamSangList(string patientGUID, DateTime fromDate, DateTime toDate)
        {
            Result result = new Result();
            MMOverride db = null;
            try
            {
                db = new MMOverride();
                List<KetQuaCanLamSangView> ketQuaCanLamSangList = (from s in db.KetQuaCanLamSangViews
                                                                    where s.PatientGUID.ToString() == patientGUID &&
                                                                    s.NgayKham >= fromDate && s.NgayKham <= toDate &&
                                                                    s.KetQuaCanLamSangStatus == (byte)Status.Actived && 
                                                                    s.ServiceStatus == (byte)Status.Actived
                                                                    orderby s.Name ascending
                                                                   select s).ToList<KetQuaCanLamSangView>();
                result.QueryResult = ketQuaCanLamSangList;
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

        public static Result DeleteKetQuaCanLamSang(List<String> keys)
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
                        KetQuaCanLamSang canLamSang = db.KetQuaCanLamSangs.SingleOrDefault<KetQuaCanLamSang>(s => s.KetQuaCanLamSangGUID.ToString() == key);
                        if (canLamSang != null)
                        {
                            canLamSang.DeletedDate = DateTime.Now;
                            canLamSang.DeletedBy = Guid.Parse(Global.UserGUID);
                            canLamSang.Status = (byte)Status.Deactived;

                            string bacSiThucHien = string.Empty;
                            if (canLamSang.DocStaff != null)
                                bacSiThucHien = canLamSang.DocStaff.Contact.FullName;

                            desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ thực hiện: '{2}', Dịch vụ: '{3}'\n",
                                canLamSang.KetQuaCanLamSangGUID.ToString(), canLamSang.Patient.Contact.FullName, bacSiThucHien, canLamSang.Service.Name);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin kết quả cận lâm sàng";
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

        public static Result InsertKetQuaCanLamSang(KetQuaCanLamSang canLamSang)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (canLamSang.KetQuaCanLamSangGUID == null || canLamSang.KetQuaCanLamSangGUID == Guid.Empty)
                    {
                        canLamSang.KetQuaCanLamSangGUID = Guid.NewGuid();
                        db.KetQuaCanLamSangs.InsertOnSubmit(canLamSang);
                        db.SubmitChanges();

                        string bacSiThucHien = string.Empty;
                        if (canLamSang.DocStaff != null)
                            bacSiThucHien = canLamSang.DocStaff.Contact.FullName;

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Dịch vụ: '{3}'",
                                canLamSang.KetQuaCanLamSangGUID.ToString(), canLamSang.Patient.Contact.FullName, bacSiThucHien, canLamSang.Service.Name);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin kết quả cận lâm sàng";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        KetQuaCanLamSang kqcls = db.KetQuaCanLamSangs.SingleOrDefault<KetQuaCanLamSang>(s => s.KetQuaCanLamSangGUID == canLamSang.KetQuaCanLamSangGUID);
                        if (kqcls != null)
                        {
                            kqcls.NgayKham = canLamSang.NgayKham;
                            kqcls.CreatedBy = canLamSang.CreatedBy;
                            kqcls.CreatedDate = canLamSang.CreatedDate;
                            kqcls.DeletedBy = canLamSang.DeletedBy;
                            kqcls.DeletedDate = canLamSang.DeletedDate;
                            kqcls.BacSiThucHienGUID = canLamSang.BacSiThucHienGUID;
                            kqcls.Note = canLamSang.Note;
                            kqcls.ServiceGUID = canLamSang.ServiceGUID;
                            kqcls.UpdatedBy = canLamSang.UpdatedBy;
                            kqcls.UpdatedDate = canLamSang.UpdatedDate;
                            kqcls.Status = canLamSang.Status;
                            kqcls.IsNormalOrNegative = canLamSang.IsNormalOrNegative;
                            kqcls.Normal = canLamSang.Normal;
                            kqcls.Abnormal = canLamSang.Abnormal;
                            kqcls.Negative = canLamSang.Negative;
                            kqcls.Positive = canLamSang.Positive;

                            db.SubmitChanges();

                            string bacSiThucHien = string.Empty;
                            if (kqcls.DocStaff != null)
                                bacSiThucHien = kqcls.DocStaff.Contact.FullName;

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Dịch vụ: '{3}'",
                                    kqcls.KetQuaCanLamSangGUID.ToString(), kqcls.Patient.Contact.FullName, bacSiThucHien, kqcls.Service.Name);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin kết quả cận lâm sàng";
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

        public static Result InsertMultiKetQuaCanLamSang(List<KetQuaCanLamSang> canLamSangList)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (KetQuaCanLamSang canLamSang in canLamSangList)
                    {
                        canLamSang.KetQuaCanLamSangGUID = Guid.NewGuid();
                        db.KetQuaCanLamSangs.InsertOnSubmit(canLamSang);
                        db.SubmitChanges();

                        string bacSiThucHien = string.Empty;
                        if (canLamSang.DocStaff != null)
                            bacSiThucHien = canLamSang.DocStaff.Contact.FullName;

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Dịch vụ: '{3}'",
                                canLamSang.KetQuaCanLamSangGUID.ToString(), canLamSang.Patient.Contact.FullName, bacSiThucHien, canLamSang.Service.Name);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin kết quả cận lâm sàng";
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
                        string ketQuaCanLamSangGUID = row["KetQuaCanLamSangGUID"].ToString();
                        KetQuaCanLamSang ketQuaCanLamSang = (from s in db.KetQuaCanLamSangs
                                                             where s.KetQuaCanLamSangGUID.ToString() == ketQuaCanLamSangGUID
                                                             select s).FirstOrDefault();

                        if (ketQuaCanLamSang == null) continue;

                        //Tracking
                        string desc = string.Format("- KetQuaCanLamSangGUID: '{0}': PatientGUID: '{1}' ==> '{2}' (KetQuaCanLamSang)",
                            ketQuaCanLamSangGUID, ketQuaCanLamSang.PatientGUID.ToString(), patientGUID);

                        ketQuaCanLamSang.PatientGUID = Guid.Parse(patientGUID);

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
