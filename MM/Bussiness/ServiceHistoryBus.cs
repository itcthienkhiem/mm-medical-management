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
    public class ServiceHistoryBus : BusBase
    {
        public static Result GetServiceHistory(string patientGUID, bool isAll, DateTime fromDate, DateTime toDate)
        {
            Result result = new Result();

            try
            {
                string query = string.Empty;
                if (isAll)
                {
                    if (Global.StaffType != StaffType.BacSi && Global.StaffType != StaffType.BacSiSieuAm &&
                        Global.StaffType != StaffType.BacSiNgoaiTongQuat && Global.StaffType != StaffType.BacSiNoiTongQuat &&
                        Global.StaffType != StaffType.BacSiPhuKhoa && Global.StaffType != StaffType.XetNghiem)
                        query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, *, CAST((FixedPrice - (FixedPrice * Discount)/100) AS float) AS Amount FROM ServiceHistoryView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND Status = {1} AND ServiceStatus = {1} ORDER BY Name", 
                            patientGUID, (byte)Status.Actived);
                    else
                        query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, *, CAST((FixedPrice - (FixedPrice * Discount)/100) AS float) AS Amount FROM ServiceHistoryView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND Status = {1} AND ServiceStatus = {1} AND (DocStaffGUID = '{2}' OR DocStaffGUID IS NULL) ORDER BY Name", 
                            patientGUID, (byte)Status.Actived, Global.UserGUID);
                }
                else
                {
                    if (Global.StaffType != StaffType.BacSi && Global.StaffType != StaffType.BacSiSieuAm &&
                        Global.StaffType != StaffType.BacSiNgoaiTongQuat && Global.StaffType != StaffType.BacSiNoiTongQuat &&
                        Global.StaffType != StaffType.BacSiPhuKhoa && Global.StaffType != StaffType.XetNghiem)
                        query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, *, CAST((FixedPrice - (FixedPrice * Discount)/100) AS float) AS Amount FROM ServiceHistoryView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND ActivedDate BETWEEN '{1}' AND '{2}' AND Status = {3} AND ServiceStatus = {3} ORDER BY Name",
                            patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived);
                    else
                        query = string.Format("SELECT  CAST(0 AS Bit) AS Checked, *, CAST((FixedPrice - (FixedPrice * Discount)/100) AS float) AS Amount FROM ServiceHistoryView WITH(NOLOCK) WHERE PatientGUID = '{0}' AND ActivedDate BETWEEN '{1}' AND '{2}' AND Status = {3} AND ServiceStatus = {3} AND (DocStaffGUID = '{4}' OR DocStaffGUID IS NULL) ORDER BY Name",
                            patientGUID, fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived, Global.UserGUID);
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

        public static Result GetServiceHistory(string patientGUID, DateTime fromDate, DateTime toDate, ServiceType type)
        {
            Result result = new Result();
            MMOverride db = null;
            try
            {
                db = new MMOverride();
                List<ServiceHistoryView> serviceHistoryList = (from s in db.ServiceHistoryViews
                                                               where s.PatientGUID.ToString() == patientGUID &&
                                                               s.ActivedDate.Value >= fromDate && s.ActivedDate.Value <= toDate &&
                                                               s.Status == (byte)Status.Actived && s.Type == (byte)type &&
                                                               s.ServiceStatus == (byte)Status.Actived
                                                               orderby s.Name ascending
                                                               select s).ToList<ServiceHistoryView>();
                result.QueryResult = serviceHistoryList;
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

        public static Result GetDSDichVuChuaXuatPhieu(DateTime fromDate, DateTime toDate)
        {
            Result result = new Result();
            MMOverride db = null;
            try
            {
                string query = string.Format("SELECT S.*, P.FullName AS TenBenhNhan FROM ServiceHistoryView S WITH(NOLOCK), PatientView P WITH(NOLOCK) WHERE S.PatientGUID = P.PatientGUID AND S.ActivedDate BETWEEN '{0}' AND '{1}' AND S.IsExported = 'False' AND S.Status = {2} AND ServiceStatus = {2} AND KhamTuTuc = 'True' ORDER BY S.ActivedDate",
                    fromDate.ToString("yyyy-MM-dd HH:mm:ss"), toDate.ToString("yyyy-MM-dd HH:mm:ss"), (byte)Status.Actived);

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

        public static Result DeleteServiceHistory(List<String> serviceHistoryKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    string desc = string.Empty;
                    foreach (string key in serviceHistoryKeys)
                    {
                        ServiceHistory srvHistory = db.ServiceHistories.SingleOrDefault<ServiceHistory>(s => s.ServiceHistoryGUID.ToString() == key);
                        if (srvHistory != null)
                        {
                            srvHistory.DeletedDate = DateTime.Now;
                            srvHistory.DeletedBy = Guid.Parse(Global.UserGUID);
                            srvHistory.Status = (byte)Status.Deactived;

                            string bacSiThucHien = string.Empty;
                            if (srvHistory.DocStaff != null)
                                bacSiThucHien = srvHistory.DocStaff.Contact.FullName;

                            string nguoiChuyenNhuong = string.Empty;
                            if (srvHistory.RootPatientGUID != null && srvHistory.RootPatientGUID.HasValue)
                            {
                                PatientView patient = db.PatientViews.FirstOrDefault(p => p.PatientGUID == srvHistory.RootPatientGUID.Value);
                                if (patient != null) nguoiChuyenNhuong = patient.FullName;
                            }

                            desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Dịch vụ: '{3}', Giá: '{4}', Giảm: '{5}', Giá vốn: '{6}', Nguuời chuyển nhượng: '{7}', Khám tự túc: '{8}'\n",
                                srvHistory.ServiceHistoryGUID.ToString(), srvHistory.Patient.Contact.FullName, bacSiThucHien,
                                srvHistory.Service.Name, srvHistory.Price.Value, srvHistory.Discount, srvHistory.GiaVon, nguoiChuyenNhuong, srvHistory.KhamTuTuc);
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin dịch vụ sử dụng";
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

        public static Result InsertServiceHistory(ServiceHistory serviceHistory)
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
                    if (serviceHistory.ServiceHistoryGUID == null || serviceHistory.ServiceHistoryGUID == Guid.Empty)
                    {
                        serviceHistory.ServiceHistoryGUID = Guid.NewGuid();
                        db.ServiceHistories.InsertOnSubmit(serviceHistory);
                        db.SubmitChanges();

                        string bacSiThucHien = string.Empty;
                        if (serviceHistory.DocStaff != null)
                            bacSiThucHien = serviceHistory.DocStaff.Contact.FullName;

                        string nguoiChuyenNhuong = string.Empty;
                        if (serviceHistory.RootPatientGUID != null && serviceHistory.RootPatientGUID.HasValue)
                        {
                            PatientView patient = db.PatientViews.FirstOrDefault(p => p.PatientGUID == serviceHistory.RootPatientGUID.Value);
                            if (patient != null) nguoiChuyenNhuong = patient.FullName;
                        }

                        //Tracking
                        desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Dịch vụ: '{3}', Giá: '{4}', Giảm: '{5}', Giá vốn: '{6}', Người chuyển nhượng: '{7}', Khám tự túc: '{8}'",
                                serviceHistory.ServiceHistoryGUID.ToString(), serviceHistory.Patient.Contact.FullName, bacSiThucHien,
                                serviceHistory.Service.Name, serviceHistory.Price.Value, serviceHistory.Discount, serviceHistory.GiaVon, 
                                nguoiChuyenNhuong, serviceHistory.KhamTuTuc);

                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin dịch vụ sử dụng";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.Price;
                        db.Trackings.InsertOnSubmit(tk);

                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        ServiceHistory srvHistory = db.ServiceHistories.SingleOrDefault<ServiceHistory>(s => s.ServiceHistoryGUID.ToString() == serviceHistory.ServiceHistoryGUID.ToString());
                        if (srvHistory != null)
                        {
                            double giaCu = srvHistory.Price.Value;
                            double giamCu = srvHistory.Discount;
                            double giaVonCu = srvHistory.GiaVon;

                            srvHistory.ActivedDate = serviceHistory.ActivedDate;
                            srvHistory.CreatedBy = serviceHistory.CreatedBy;
                            srvHistory.CreatedDate = serviceHistory.CreatedDate;
                            srvHistory.DeletedBy = serviceHistory.DeletedBy;
                            srvHistory.DeletedDate = serviceHistory.DeletedDate;
                            srvHistory.DocStaffGUID = serviceHistory.DocStaffGUID;
                            srvHistory.Note = serviceHistory.Note;
                            srvHistory.Price = serviceHistory.Price;
                            srvHistory.Discount = serviceHistory.Discount;
                            srvHistory.GiaVon = serviceHistory.GiaVon;
                            srvHistory.ServiceGUID = serviceHistory.ServiceGUID;
                            srvHistory.UpdatedBy = serviceHistory.UpdatedBy;
                            srvHistory.UpdatedDate = serviceHistory.UpdatedDate;
                            srvHistory.Status = serviceHistory.Status;
                            srvHistory.IsNormalOrNegative = serviceHistory.IsNormalOrNegative;
                            srvHistory.Normal = serviceHistory.Normal;
                            srvHistory.Abnormal = serviceHistory.Abnormal;
                            srvHistory.Negative = serviceHistory.Negative;
                            srvHistory.Positive = serviceHistory.Positive;
                            srvHistory.RootPatientGUID = serviceHistory.RootPatientGUID;
                            srvHistory.KhamTuTuc = serviceHistory.KhamTuTuc;

                            string bacSiThucHien = string.Empty;
                            if (srvHistory.DocStaff != null)
                                bacSiThucHien = srvHistory.DocStaff.Contact.FullName;

                            string nguoiChuyenNhuong = string.Empty;
                            if (srvHistory.RootPatientGUID != null && srvHistory.RootPatientGUID.HasValue)
                            {
                                PatientView patient = db.PatientViews.FirstOrDefault(p => p.PatientGUID == srvHistory.RootPatientGUID.Value);
                                if (patient != null) nguoiChuyenNhuong = patient.FullName;
                            }

                            //Tracking
                            desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Dịch vụ: '{3}', Giá: cũ: '{4}' - mới: '{5}', Giảm: cũ: '{6}' - mới: '{7}', Giá vốn cũ: '{8}' - mới: '{9}', Người chuyển nhượng: '{10}', Khám tự túc: '{11}'",
                                    srvHistory.ServiceHistoryGUID.ToString(), srvHistory.Patient.Contact.FullName, bacSiThucHien,
                                    srvHistory.Service.Name, giaCu, srvHistory.Price.Value, giamCu, srvHistory.Discount, giaVonCu, srvHistory.GiaVon, nguoiChuyenNhuong, srvHistory.KhamTuTuc);

                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin dịch vụ sử dụng";
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

        public static Result GetPhieuThuByServiceHistoryGUID(string serviceHistoryGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                ServiceHistory srvHistory = db.ServiceHistories.SingleOrDefault<ServiceHistory>(s => s.ServiceHistoryGUID.ToString() == serviceHistoryGUID);
                if (srvHistory != null)
                {
                    if (srvHistory.ReceiptDetails != null && srvHistory.ReceiptDetails.Count > 0)
                        result.QueryResult = srvHistory.ReceiptDetails[0].Receipt;
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

        public static Result UpdateChecklist(string patientGUID, string hopDongGUID, DateTime beginDate, DateTime endDate, List<DataRow> checklistRows)
        {
            Result result = new Result();

            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (DataRow row in checklistRows)
                    {
                        string desc = string.Empty;
                        bool isUsing = Convert.ToBoolean(row["Using"]);
                        string serviceGUID = row["ServiceGUID"].ToString();

                        List<ServiceHistory> serviceHistoryList = (from s in db.ServiceHistories
                                                                    where s.ServiceGUID.Value.ToString() == serviceGUID &&
                                                                    s.Status == (byte)Status.Actived &&
                                                                    !s.KhamTuTuc && s.PatientGUID.Value.ToString() == patientGUID &&
                                                                    s.ActivedDate.Value >= beginDate && s.ActivedDate.Value <= endDate
                                                                    select s).ToList();

                        if (!isUsing)
                        {
                            if (serviceHistoryList != null && serviceHistoryList.Count > 0)
                            {
                                foreach (var srvHistory in serviceHistoryList)
                                {
                                    srvHistory.Status = (byte)Status.Deactived;

                                    string bacSiThucHien = string.Empty;
                                    if (srvHistory.DocStaff != null)
                                        bacSiThucHien = srvHistory.DocStaff.Contact.FullName;

                                    string nguoiChuyenNhuong = string.Empty;
                                    if (srvHistory.RootPatientGUID != null && srvHistory.RootPatientGUID.HasValue)
                                    {
                                        PatientView patient = db.PatientViews.FirstOrDefault(p => p.PatientGUID == srvHistory.RootPatientGUID.Value);
                                        if (patient != null) nguoiChuyenNhuong = patient.FullName;
                                    }

                                    desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Dịch vụ: '{3}', Giá: '{4}', Giảm: '{5}', Giá vốn: '{6}', Nguuời chuyển nhượng: '{7}', Khám tự túc: '{8}'\n",
                                        srvHistory.ServiceHistoryGUID.ToString(), srvHistory.Patient.Contact.FullName, bacSiThucHien,
                                        srvHistory.Service.Name, srvHistory.Price.Value, srvHistory.Discount, srvHistory.GiaVon, nguoiChuyenNhuong, srvHistory.KhamTuTuc);
                                }


                                //Tracking
                                desc = desc.Substring(0, desc.Length - 1);
                                Tracking tk = new Tracking();
                                tk.TrackingGUID = Guid.NewGuid();
                                tk.TrackingDate = DateTime.Now;
                                tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                                tk.ActionType = (byte)ActionType.Delete;
                                tk.Action = "Xóa thông tin dịch vụ sử dụng";
                                tk.Description = desc;
                                tk.TrackingType = (byte)TrackingType.Price;
                                db.Trackings.InsertOnSubmit(tk);
                            }
                        }
                        else
                        {
                            if (serviceHistoryList == null || serviceHistoryList.Count <= 0)
                            {
                                List<ServiceHistory> svh = (from s in db.ServiceHistories
                                                            where s.ServiceGUID.Value.ToString() == serviceGUID &&
                                                            s.Status == (byte)Status.Actived &&
                                                            s.KhamTuTuc && s.RootPatientGUID.HasValue &&
                                                            s.RootPatientGUID.Value.ToString() == patientGUID &&
                                                            s.ActivedDate.Value >= beginDate && s.ActivedDate.Value <= endDate
                                                            select s).ToList();

                                if (svh == null || svh.Count <= 0)
                                {
                                    ServiceHistory srv = new ServiceHistory();
                                    srv.ServiceHistoryGUID = Guid.NewGuid();
                                    srv.ServiceGUID = Guid.Parse(serviceGUID);
                                    srv.PatientGUID = Guid.Parse(patientGUID);
                                    srv.Status = (byte)Status.Actived;
                                    srv.CreatedDate = DateTime.Now;
                                    srv.CreatedBy = Guid.Parse(Global.UserGUID);
                                    srv.ActivedDate = DateTime.Now;
                                    srv.IsExported = false;
                                    srv.IsNormalOrNegative = true;
                                    srv.Normal = true;
                                    srv.Abnormal = false;
                                    srv.Positive = false;
                                    srv.Negative = false;
                                    srv.KhamTuTuc = false;
                                    srv.Discount = 0;
                                    srv.Price = 0;
                                    srv.GiaVon = 0;

                                    GiaDichVuHopDong giaDichVuHopDong = db.GiaDichVuHopDongs.SingleOrDefault(g => g.HopDongGUID.ToString() == hopDongGUID &&
                                                                        g.ServiceGUID.ToString() == serviceGUID && g.Status == 0);

                                    if (giaDichVuHopDong != null) srv.Price = giaDichVuHopDong.Gia;

                                    GiaVonDichVu giaVonDichVu = (from g in db.GiaVonDichVus
                                                                 where g.ServiceGUID.ToString() == serviceGUID && g.Status == (byte)Status.Actived &&
                                                                 g.NgayApDung <= DateTime.Now
                                                                 orderby g.NgayApDung descending
                                                                 select g).FirstOrDefault();

                                    if (giaVonDichVu != null) srv.GiaVon = giaVonDichVu.GiaVon;

                                    db.ServiceHistories.InsertOnSubmit(srv);
                                    db.SubmitChanges();

                                    string bacSiThucHien = string.Empty;
                                    if (srv.DocStaff != null)
                                        bacSiThucHien = srv.DocStaff.Contact.FullName;

                                    string nguoiChuyenNhuong = string.Empty;
                                    if (srv.RootPatientGUID != null && srv.RootPatientGUID.HasValue)
                                    {
                                        PatientView patient = db.PatientViews.FirstOrDefault(p => p.PatientGUID == srv.RootPatientGUID.Value);
                                        if (patient != null) nguoiChuyenNhuong = patient.FullName;
                                    }

                                    //Tracking
                                    desc += string.Format("- GUID: '{0}', Bệnh nhân: '{1}', Bác sĩ: '{2}', Dịch vụ: '{3}', Giá: '{4}', Giảm: '{5}', Giá vốn: '{6}', Người chuyển nhượng: '{7}', Khám tự túc: '{8}'",
                                            srv.ServiceHistoryGUID.ToString(), srv.Patient.Contact.FullName, bacSiThucHien, srv.Service.Name, srv.Price.Value, 
                                            srv.Discount, srv.GiaVon, nguoiChuyenNhuong, srv.KhamTuTuc);

                                    Tracking tk = new Tracking();
                                    tk.TrackingGUID = Guid.NewGuid();
                                    tk.TrackingDate = DateTime.Now;
                                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                                    tk.ActionType = (byte)ActionType.Add;
                                    tk.Action = "Thêm thông tin dịch vụ sử dụng";
                                    tk.Description = desc;
                                    tk.TrackingType = (byte)TrackingType.Price;
                                    db.Trackings.InsertOnSubmit(tk);
                                }
                            }
                        }
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
