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
    public class LoiKhuyenBus : BusBase
    {
        public static Result GetLoiKhuyenList(string patientGUID, DateTime fromDate, DateTime toDate)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                //if (Global.StaffType != StaffType.BacSi && Global.StaffType != StaffType.BacSiSieuAm &&
                //    Global.StaffType != StaffType.BacSiNgoaiTongQuat && Global.StaffType != StaffType.BacSiNoiTongQuat && 
                //    Global.StaffType != StaffType.BacSiPhuKhoa)
                //    query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM LoiKhuyenView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND Ngay BETWEEN '{1}' AND '{2}' AND LoiKhuyenStatus = {3} AND SymptomStatus = {3} AND Archived = 'False' ORDER BY Ngay DESC",
                //        patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived);
                //else
                //    query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM LoiKhuyenView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND Ngay BETWEEN '{1}' AND '{2}' AND LoiKhuyenStatus = {3} AND SymptomStatus = {3} AND Archived = 'False' AND DocStaffGUID = '{4}' ORDER BY Ngay DESC",
                //        patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived, Global.UserGUID);

                query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, * FROM LoiKhuyenView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND Ngay BETWEEN '{1}' AND '{2}' AND LoiKhuyenStatus = {3} AND SymptomStatus = {3} ORDER BY Ngay DESC",
                        patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived);

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

        public static Result GetLoiKhuyenList2(string patientGUID, DateTime fromDate, DateTime toDate)
        {
            Result result = new Result();
            MMOverride db = null;
            try
            {
                db = new MMOverride();
                List<LoiKhuyenView> loiKhuyenList = (from lk in db.LoiKhuyenViews
                                                     where lk.PatientGUID.ToString() == patientGUID &&
                                                     lk.Ngay >= fromDate && lk.Ngay <= toDate &&
                                                     lk.LoiKhuyenStatus == (byte)Status.Actived &&
                                                     lk.SymptomStatus == (byte)Status.Actived
                                                     orderby lk.SymptomName descending
                                                     select lk).ToList<LoiKhuyenView>();

                result.QueryResult = loiKhuyenList;
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

        public static Result DeleteLoiKhuyen(List<String> loiKhuyenKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in loiKhuyenKeys)
                    {
                        LoiKhuyen loiKhuyen = db.LoiKhuyens.SingleOrDefault<LoiKhuyen>(l => l.LoiKhuyenGUID.ToString() == key);
                        if (loiKhuyen != null)
                        {
                            loiKhuyen.DeletedDate = DateTime.Now;
                            loiKhuyen.DeletedBy = Guid.Parse(Global.UserGUID);
                            loiKhuyen.Status = (byte)Status.Deactived;

                            desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Triệu chứng: '{3}', Lời khuyên: '{4}'\n",
                                loiKhuyen.LoiKhuyenGUID.ToString(), loiKhuyen.Patient.Contact.FullName, loiKhuyen.DocStaff.Contact.FullName,
                                loiKhuyen.Symptom.SymptomName, loiKhuyen.Symptom.Advice);
                        }


                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin lời khuyên";
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

        public static Result InsertLoiKhuyen(LoiKhuyen loiKhuyen)
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
                    if (loiKhuyen.LoiKhuyenGUID == null || loiKhuyen.LoiKhuyenGUID == Guid.Empty)
                    {
                        loiKhuyen.LoiKhuyenGUID = Guid.NewGuid();
                        db.LoiKhuyens.InsertOnSubmit(loiKhuyen);
                        db.SubmitChanges();

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Triệu chứng: '{3}', Lời khuyên: '{4}'",
                                 loiKhuyen.LoiKhuyenGUID.ToString(), loiKhuyen.Patient.Contact.FullName, loiKhuyen.DocStaff.Contact.FullName,
                                 loiKhuyen.Symptom.SymptomName, loiKhuyen.Symptom.Advice);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin lời khuyên";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        tk.ComputerName = Utility.GetDNSHostName();
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        LoiKhuyen lk = db.LoiKhuyens.SingleOrDefault<LoiKhuyen>(l => l.LoiKhuyenGUID.ToString() == loiKhuyen.LoiKhuyenGUID.ToString());
                        if (lk != null)
                        {
                            lk.Ngay = loiKhuyen.Ngay;
                            lk.PatientGUID = loiKhuyen.PatientGUID;
                            lk.DocStaffGUID = loiKhuyen.DocStaffGUID;
                            lk.SymptomGUID = loiKhuyen.SymptomGUID;
                            lk.Note = loiKhuyen.Note;
                            lk.CreatedBy = loiKhuyen.CreatedBy;
                            lk.CreatedDate = loiKhuyen.CreatedDate;
                            lk.DeletedBy = loiKhuyen.DeletedBy;
                            lk.DeletedDate = loiKhuyen.DeletedDate;
                            lk.UpdatedBy = loiKhuyen.UpdatedBy;
                            lk.UpdatedDate = loiKhuyen.UpdatedDate;
                            lk.Status = loiKhuyen.Status;
                            db.SubmitChanges();
                            
                            //Tracking
                            desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Triệu chứng: '{3}', Lời khuyên: '{4}'",
                                 lk.LoiKhuyenGUID.ToString(), lk.Patient.Contact.FullName, lk.DocStaff.Contact.FullName,
                                 lk.Symptom.SymptomName, lk.Symptom.Advice);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin lời khuyên";
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
                        string loiKhuyenGUID = row["LoiKhuyenGUID"].ToString();
                        LoiKhuyen loiKhuyen = (from s in db.LoiKhuyens
                                               where s.LoiKhuyenGUID.ToString() == loiKhuyenGUID
                                               select s).FirstOrDefault();

                        if (loiKhuyen == null) continue;

                        //Tracking
                        string desc = string.Format("- LoiKhuyenGUID: '{0}': PatientGUID: '{1}' ==> '{2}' (LoiKhuyen)",
                            loiKhuyenGUID, loiKhuyen.PatientGUID.ToString(), patientGUID);

                        loiKhuyen.PatientGUID = Guid.Parse(patientGUID);

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
