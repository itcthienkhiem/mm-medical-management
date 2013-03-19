using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Transactions;
using MM.Common;
using MM.Databasae;

namespace MM.Bussiness
{
    public class CompanyContractBus : BusBase
    {
        public static Result GetContractList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM CompanyContractView WITH(NOLOCK) WHERE ContractStatus={0} AND CompanyStatus={0} ORDER BY BeginDate DESC", (byte)Status.Actived);
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

        public static Result GetNoCompletedContractList()
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM CompanyContractView WITH(NOLOCK) WHERE ContractStatus={0} AND CompanyStatus={0} AND ((Completed='False' AND BeginDate <= GetDate()) OR (Completed = 'True' AND GetDate() BETWEEN BeginDate AND EndDate)) ORDER BY BeginDate DESC", (byte)Status.Actived);
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

        public static Result GetContractList(string name, bool isMaHopDong)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (name.Trim() == string.Empty)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM CompanyContractView WITH(NOLOCK) WHERE ContractStatus={0} AND CompanyStatus={0} ORDER BY BeginDate DESC",
                        (byte)Status.Actived);
                }
                else if (isMaHopDong)
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM CompanyContractView WITH(NOLOCK) WHERE ContractCode LIKE N'%{0}%' AND ContractStatus={1} AND CompanyStatus={1} ORDER BY BeginDate DESC",
                        name, (byte)Status.Actived);
                }
                else
                {
                    query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM CompanyContractView WITH(NOLOCK) WHERE ContractName LIKE N'%{0}%' AND ContractStatus={1} AND CompanyStatus={1} ORDER BY BeginDate DESC",
                        name, (byte)Status.Actived);
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

        public static Result GetContractCount()
        {
            Result result = null;
            try
            {
                string query = "SELECT Count(*) FROM CompanyContract WITH(NOLOCK)";
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

        public static Result GetContractMemberList(string contractGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ContractMemberView WITH(NOLOCK) WHERE CompanyContractGUID='{0}' AND Status={1} AND Archived = 'False' ORDER BY FirstName, FullName",
                    contractGUID, (byte)Status.Actived);
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

        public static Result GetContractMemberList(string contractGUID, string tenBenhNhan, int type, int doiTuong)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (tenBenhNhan.Trim() == string.Empty)
                {
                    if (doiTuong == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ContractMemberView WITH(NOLOCK) WHERE CompanyContractGUID='{0}' AND Status={1} AND Archived = 'False' ORDER BY FirstName, FullName",
                                contractGUID, (byte)Status.Actived);
                    }
                    else if (doiTuong == 1)//Nam
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ContractMemberView WITH(NOLOCK) WHERE CompanyContractGUID='{0}' AND Status={1} AND GenderAsStr = N'Nam' AND Archived = 'False' ORDER BY FirstName, FullName",
                                contractGUID, (byte)Status.Actived);
                    }
                    else if (doiTuong == 2)//Nữ độc thân
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ContractMemberView WITH(NOLOCK) WHERE CompanyContractGUID='{0}' AND Status={1} AND GenderAsStr = N'Nữ' AND (Tinh_Trang_Gia_Dinh IS NULL OR Tinh_Trang_Gia_Dinh <> N'Có gia đình') AND Archived = 'False' ORDER BY FirstName, FullName",
                                contractGUID, (byte)Status.Actived);
                    }
                    else if (doiTuong == 3)//Nữ có gia đình
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ContractMemberView WITH(NOLOCK) WHERE CompanyContractGUID='{0}' AND Status={1} AND GenderAsStr = N'Nữ' AND Tinh_Trang_Gia_Dinh IS NOT NULL AND Tinh_Trang_Gia_Dinh = N'Có gia đình' AND Archived = 'False' ORDER BY FirstName, FullName",
                                contractGUID, (byte)Status.Actived);
                    }
                }
                else
                {
                    string fieldName = string.Empty;
                    if (type == 0) fieldName = "FullName";
                    else if (type == 1) fieldName = "FileNum";
                    else fieldName = "Mobile";

                    if (doiTuong == 0) //Tất cả
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ContractMemberView WITH(NOLOCK) WHERE CompanyContractGUID='{0}' AND Status={1} AND {2} LIKE N'%{3}%' AND Archived = 'False' ORDER BY FirstName, FullName",
                                contractGUID, (byte)Status.Actived, fieldName, tenBenhNhan);
                    }
                    else if (doiTuong == 1)//Nam
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ContractMemberView WITH(NOLOCK) WHERE CompanyContractGUID='{0}' AND Status={1} AND {2} LIKE N'%{3}%' AND GenderAsStr = N'Nam' AND Archived = 'False' ORDER BY FirstName, FullName",
                                contractGUID, (byte)Status.Actived, fieldName, tenBenhNhan);
                    }
                    else if (doiTuong == 2)//Nữ độc thân
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ContractMemberView WITH(NOLOCK) WHERE CompanyContractGUID='{0}' AND Status={1} AND {2} LIKE N'%{3}%' AND GenderAsStr = N'Nữ' AND (Tinh_Trang_Gia_Dinh IS NULL OR Tinh_Trang_Gia_Dinh <> N'Có gia đình') AND Archived = 'False' ORDER BY FirstName, FullName",
                                contractGUID, (byte)Status.Actived, fieldName, tenBenhNhan);
                    }
                    else if (doiTuong == 3)//Nữ có gia đình
                    {
                        query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM ContractMemberView WITH(NOLOCK) WHERE CompanyContractGUID='{0}' AND Status={1} AND {2} LIKE N'%{3}%' AND GenderAsStr = N'Nữ' AND Tinh_Trang_Gia_Dinh IS NOT NULL AND Tinh_Trang_Gia_Dinh = N'Có gia đình' AND Archived = 'False' ORDER BY FirstName, FullName",
                                contractGUID, (byte)Status.Actived, fieldName, tenBenhNhan);
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

        public static Result GetDanhSachGiaDichVuList(string contractGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM GiaDichVuHopDongView WITH(NOLOCK) WHERE HopDongGUID='{0}' AND GiaDVHDStatus={1} ORDER BY Name",
                    contractGUID, (byte)Status.Actived);
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

        public static Result GetDichVuCon(string giaDichVuHopDongGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM DichVuConView WITH(NOLOCK) WHERE GiaDichVuHopDongGUID='{0}' AND Status={1} AND ServiceStatus = {1} ORDER BY Name",
                    giaDichVuHopDongGUID, (byte)Status.Actived);
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

        public static Result GetDichVuCon(string hopDongGUID, string serviceGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT D.* FROM DichVuConView D WITH(NOLOCK), GiaDichVuHopDong G WITH(NOLOCK) WHERE D.GiaDichVuHopDongGUID=G.GiaDichVuHopDongGUID AND G.HopDongGUID='{0}' AND G.ServiceGUID='{1}' AND D.Status={2} AND D.ServiceStatus = {2} AND G.Status = {2} ORDER BY D.Name",
                    hopDongGUID, serviceGUID, (byte)Status.Actived);
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

        public static Result GetContractMemberList(string contractGUID, string serviceGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT DISTINCT C.* FROM ContractMemberView C WITH(NOLOCK), CompanyCheckList L WITH(NOLOCK) WHERE C.CompanyContractGUID='{0}' AND  C.ContractMemberGUID = L.ContractMemberGUID AND L.Status = {2} AND C.Status={2} AND C.CompanyMemberStatus={2} AND C.Archived='False' AND L.ServiceGUID = '{1}' ORDER BY C.FirstName, C.FullName",
                    contractGUID, serviceGUID, (byte)Status.Actived);
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

        public static Result GetContractMemberList(string contractGUID, string serviceGUID, string patientGUID, string tenBenhNhan, int type)
        {
            Result result = null;

            try
            {
                string query = string.Empty;

                if (tenBenhNhan.Trim() == string.Empty)
                {
                    query = "SELECT TOP 0 * FROM ContractMemberView WITH(NOLOCK)";
                }
                else if (tenBenhNhan.Trim() == "*")
                {
                    query = string.Format("SELECT DISTINCT C.* FROM ContractMemberView C WITH(NOLOCK), CompanyCheckList L WITH(NOLOCK) WHERE C.CompanyContractGUID='{0}' AND  C.ContractMemberGUID = L.ContractMemberGUID AND L.Status = {2} AND C.Status={2} AND C.CompanyMemberStatus={2} AND C.Archived='False' AND L.ServiceGUID = '{1}' AND C.PatientGUID <> '{3}' ORDER BY C.FirstName, C.FullName",
                        contractGUID, serviceGUID, (byte)Status.Actived, patientGUID);
                }
                else if (type == 0)
                {
                    query = string.Format("SELECT DISTINCT C.* FROM ContractMemberView C WITH(NOLOCK), CompanyCheckList L WITH(NOLOCK) WHERE C.CompanyContractGUID='{0}' AND  C.ContractMemberGUID = L.ContractMemberGUID AND L.Status = {2} AND C.Status={2} AND C.CompanyMemberStatus={2} AND C.Archived='False' AND L.ServiceGUID = '{1}' AND FullName LIKE N'%{3}%' AND C.PatientGUID <> '{4}' ORDER BY C.FirstName, C.FullName",
                        contractGUID, serviceGUID, (byte)Status.Actived, tenBenhNhan, patientGUID);
                }
                else if (type == 1)
                {
                    query = string.Format("SELECT DISTINCT C.* FROM ContractMemberView C WITH(NOLOCK), CompanyCheckList L WITH(NOLOCK) WHERE C.CompanyContractGUID='{0}' AND  C.ContractMemberGUID = L.ContractMemberGUID AND L.Status = {2} AND C.Status={2} AND C.CompanyMemberStatus={2} AND C.Archived='False' AND L.ServiceGUID = '{1}' AND FileNum LIKE N'%{3}%' AND C.PatientGUID <> '{4}' ORDER BY C.FirstName, C.FullName",
                        contractGUID, serviceGUID, (byte)Status.Actived, tenBenhNhan, patientGUID);
                }
                else if (type == 2)
                {
                    query = string.Format("SELECT DISTINCT C.* FROM ContractMemberView C WITH(NOLOCK), CompanyCheckList L WITH(NOLOCK) WHERE C.CompanyContractGUID='{0}' AND  C.ContractMemberGUID = L.ContractMemberGUID AND L.Status = {2} AND C.Status={2} AND C.CompanyMemberStatus={2} AND C.Archived='False' AND L.ServiceGUID = '{1}' AND Mobile LIKE N'%{3}%' AND C.PatientGUID <> '{4}' ORDER BY C.FirstName, C.FullName",
                        contractGUID, serviceGUID, (byte)Status.Actived, tenBenhNhan, patientGUID);
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

        public static Result GetCheckList(string contractMemberGUID)
        {
            Result result = null;

            try
            {
                string query = string.Format("SELECT CAST(0 AS Bit) AS Checked, * FROM CompanyCheckListView WITH(NOLOCK) WHERE ContractMemberGUID='{0}' AND ServiceStatus={1} AND CheckListStatus={1} ORDER BY Name",
                    contractMemberGUID, (byte)Status.Actived);
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

        public static Result GetCheckListByPatient(string patientGUID)
        {
            Result result = null;

            try
            {
                string spName = "spGetCheckList";
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                SqlParameter param = new SqlParameter("@PatientGUID", patientGUID);
                sqlParams.Add(param);

                return ExcuteQuery(spName, sqlParams);
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

        public static Result GetCheckList(string contractGUID, string patientGUID)
        {
            Result result = null;

            try
            {
                string spName = "spGetCheckListByContract";
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                SqlParameter param = new SqlParameter("@ContractGUID", contractGUID);
                sqlParams.Add(param);
                param = new SqlParameter("@PatientGUID", patientGUID);
                sqlParams.Add(param);

                return ExcuteQuery(spName, sqlParams);
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

        public static Result GetDanhSachNhanVien(string contractGUID, int type, string tenBenhNhan, int filterType)
        {
            Result result = null;

            try
            {
                string spName = "spGetDanhSachNhanVien";
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                SqlParameter param = new SqlParameter("@ContractGUID", contractGUID);
                sqlParams.Add(param);
                param = new SqlParameter("@Type", type);
                sqlParams.Add(param);
                param = new SqlParameter("@TenBenhNhan", tenBenhNhan);
                sqlParams.Add(param);
                param = new SqlParameter("@FilterType", filterType);
                sqlParams.Add(param);

                return ExcuteQuery(spName, sqlParams);
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

        public static Result DeleteContract(List<string> keys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                string desc = string.Empty;

                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (string key in keys)
                    {
                        CompanyContract c = db.CompanyContracts.SingleOrDefault<CompanyContract>(cc => cc.CompanyContractGUID.ToString() == key);
                        if (c != null)
                        {
                            c.DeletedDate = DateTime.Now;
                            c.DeletedBy = Guid.Parse(Global.UserGUID);
                            c.Status = (byte)Status.Deactived;
                            string strNgayDatCoc = string.Empty;
                            if (c.NgayDatCoc != null && c.NgayDatCoc.HasValue)
                                strNgayDatCoc = c.NgayDatCoc.Value.ToString("dd/MM/yyyy HH:mm:ss");

                            if (c.EndDate.HasValue)
                            {
                                desc += string.Format("- GUID: '{0}', Mã hợp đồng: '{1}', Tên hợp đồng: '{2}', Cty: '{3}', Ngày bắt đầu: '{4}', Ngày kết thúc: '{5}', Số tiền: '{6}', Đặt cọc: '{7}', Nhân sự phụ trách: '{8}', Số điện thoại: '{9}', Ngày đặt cọc: '{10}'\n",
                                    c.CompanyContractGUID.ToString(), c.ContractCode, c.ContractName, c.Company.TenCty, c.BeginDate.ToString("dd/MM/yyyy HH:mm:ss"),
                                    c.EndDate.Value.ToString("dd/MM/yyyy HH:mm:ss"), c.SoTien, c.DatCoc, c.NhanSuPhuTrach, c.SoDienThoai, strNgayDatCoc);
                            }
                            else
                            {
                                desc += string.Format("- GUID: '{0}', Mã hợp đồng: '{1}', Tên hợp đồng: '{2}', Cty: '{3}', Ngày bắt đầu: '{4}', Số tiền: '{5}', Đặt cọc: '{6}', Nhân sự phụ trách: '{7}', Số điện thoại: '{8}', Ngày đặt cọc: '{9}'\n",
                                    c.CompanyContractGUID.ToString(), c.ContractCode, c.ContractName, c.Company.TenCty, c.BeginDate.ToString("dd/MM/yyyy HH:mm:ss"),
                                    c.SoTien, c.DatCoc, c.NhanSuPhuTrach, c.SoDienThoai, strNgayDatCoc);
                            }
                        }
                    }

                    //Tracking
                    desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Xóa thông tin hợp đồng";
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

        public static Result CheckContractExistCode(string contractGUID, string code)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                CompanyContract contract = null;
                if (contractGUID == null || contractGUID == string.Empty)
                    contract = db.CompanyContracts.SingleOrDefault<CompanyContract>(c => c.ContractCode.ToLower() == code.ToLower());
                else
                    contract = db.CompanyContracts.SingleOrDefault<CompanyContract>(c => c.ContractCode.ToLower() == code.ToLower() &&
                                                                c.CompanyContractGUID.ToString() != contractGUID);

                if (contract == null)
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

        public static Result InsertContract(CompanyContract contract, CompanyInfo companyInfo)
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
                    if (contract.CompanyContractGUID == null || contract.CompanyContractGUID == Guid.Empty)
                    {
                        contract.CompanyContractGUID = Guid.NewGuid();
                        db.CompanyContracts.InsertOnSubmit(contract);
                        db.SubmitChanges();

                        string strNgayDatCoc = string.Empty;
                        if (contract.NgayDatCoc != null && contract.NgayDatCoc.HasValue)
                            strNgayDatCoc = contract.NgayDatCoc.Value.ToString("dd/MM/yyyy HH:mm:ss");
                        if (contract.EndDate.HasValue)
                        {
                            desc += string.Format("- Hợp đồng: GUID: '{0}', Mã hợp đồng: '{1}', Tên hợp đồng: '{2}', Cty: '{3}', Ngày bắt đầu: '{4}', Ngày kết thúc: '{5}', Số tiền: '{6}', Đặt cọc: '{7}', Nhân sự phụ trách: '{8}', Số điện thoại: '{9}', Ngày đặt cọc: '{10}'\n",
                                contract.CompanyContractGUID.ToString(), contract.ContractCode, contract.ContractName, contract.Company.TenCty,
                                contract.BeginDate.ToString("dd/MM/yyyy HH:mm:ss"), contract.EndDate.Value.ToString("dd/MM/yyyy HH:mm:ss"), contract.SoTien, contract.DatCoc, contract.NhanSuPhuTrach, contract.SoDienThoai, strNgayDatCoc);
                        }
                        else
                        {
                            desc += string.Format("- Hợp đồng: GUID: '{0}', Mã hợp đồng: '{1}', Tên hợp đồng: '{2}', Cty: '{3}', Ngày bắt đầu: '{4}', Số tiền: '{5}', Đặt cọc: '{6}', Nhân sự phụ trách: '{7}', Số điện thoại: '{8}', Ngày đặt cọc: '{9}'\n",
                                contract.CompanyContractGUID.ToString(), contract.ContractCode, contract.ContractName, contract.Company.TenCty,
                                contract.BeginDate.ToString("dd/MM/yyyy HH:mm:ss"), contract.SoTien, contract.DatCoc, contract.NhanSuPhuTrach, contract.SoDienThoai, strNgayDatCoc);
                        }

                        //Giá dịch vụ hợp đồng
                        if (companyInfo.GiaDichVuDataSource != null && companyInfo.GiaDichVuDataSource.Rows.Count > 0)
                        {
                            foreach (DataRow row in companyInfo.GiaDichVuDataSource.Rows)
                            {
                                GiaDichVuHopDong giaDichVuHopDong = new GiaDichVuHopDong();
                                giaDichVuHopDong.GiaDichVuHopDongGUID = Guid.NewGuid();
                                giaDichVuHopDong.HopDongGUID = contract.CompanyContractGUID;
                                giaDichVuHopDong.ServiceGUID = Guid.Parse(row["ServiceGUID"].ToString());
                                giaDichVuHopDong.Gia = Convert.ToDouble(row["Gia"]);
                                giaDichVuHopDong.CreatedBy = Guid.Parse(Global.UserGUID);
                                giaDichVuHopDong.CreatedDate = DateTime.Now;
                                db.GiaDichVuHopDongs.InsertOnSubmit(giaDichVuHopDong);
                                db.SubmitChanges();

                                //Dịch vụ con
                                if (!companyInfo.DictDichVuCon.ContainsKey(giaDichVuHopDong.ServiceGUID.ToString())) continue;
                                DataTable dtDichVuCon = companyInfo.DictDichVuCon[giaDichVuHopDong.ServiceGUID.ToString()];
                                if (dtDichVuCon == null || dtDichVuCon.Rows.Count <= 0) continue;
                                foreach (DataRow drDichVuCon in dtDichVuCon.Rows)
	                            {
                                    DichVuCon dvc = new DichVuCon();
                                    dvc.DichVuConGUID = Guid.NewGuid();
                                    dvc.GiaDichVuHopDongGUID = giaDichVuHopDong.GiaDichVuHopDongGUID;
                                    dvc.ServiceGUID = Guid.Parse(drDichVuCon["ServiceGUID"].ToString());
                                    dvc.CreatedDate = DateTime.Now;
                                    dvc.CreatedBy = Guid.Parse(Global.UserGUID);
                                    dvc.Status = (byte)Status.Actived;
                                    db.DichVuCons.InsertOnSubmit(dvc);
	                            }
                            }
                        }

                        //Members
                        if (companyInfo.AddedMembers != null && companyInfo.AddedMembers.Count > 0)
                        {
                            desc += "- Danh sách nhân viên được thêm:\n";
                            foreach (Member member in companyInfo.AddedMembers.Values)
                            {
                                ContractMember m = db.ContractMembers.SingleOrDefault<ContractMember>(mm => mm.CompanyMemberGUID.ToString() == member.CompanyMemberGUID &&
                                                                                                    mm.CompanyContractGUID.ToString() == contract.CompanyContractGUID.ToString());
                                if (m == null)
                                {
                                    m = new ContractMember();
                                    m.ContractMemberGUID = Guid.NewGuid();
                                    m.CompanyContractGUID = contract.CompanyContractGUID;
                                    m.CompanyMemberGUID = Guid.Parse(member.CompanyMemberGUID);
                                    m.CreatedDate = DateTime.Now;
                                    m.CreatedBy = Guid.Parse(Global.UserGUID);
                                    m.Status = (byte)Status.Actived;
                                    db.ContractMembers.InsertOnSubmit(m);
                                    
                                }
                                else
                                {
                                    m.Status = (byte)Status.Actived;
                                    m.UpdatedDate = DateTime.Now;
                                    m.UpdatedBy = Guid.Parse(Global.UserGUID);
                                }

                                db.SubmitChanges();

                                var companyMember = db.CompanyMembers.SingleOrDefault<CompanyMember>(cm => cm.CompanyMemberGUID == m.CompanyMemberGUID);
                                string tenNhanVien = string.Empty;
                                if (companyMember != null) tenNhanVien = companyMember.Patient.Contact.FullName;
                                desc += string.Format("  + GUID: '{0}', Nhân viên: '{1}'\n", m.ContractMemberGUID.ToString(), tenNhanVien);

                                //Check List
                                if (member.AddedServices != null && member.AddedServices.Count > 0)
                                {
                                    desc += string.Format("     Danh sách dịch vụ được thêm:\n");
                                    foreach (string key in member.AddedServices)
                                    {
                                        CompanyCheckList c = db.CompanyCheckLists.SingleOrDefault<CompanyCheckList>(cc => cc.ServiceGUID.ToString() == key &&
                                                                                                            cc.ContractMemberGUID.ToString() == m.ContractMemberGUID.ToString());
                                        if (c == null)
                                        {
                                            c = new CompanyCheckList();
                                            c.CompanyCheckListGUID = Guid.NewGuid();
                                            c.ContractMemberGUID = m.ContractMemberGUID;
                                            c.ServiceGUID = Guid.Parse(key);
                                            c.CreatedDate = DateTime.Now;
                                            c.CreatedBy = Guid.Parse(Global.UserGUID);
                                            c.Status = (byte)Status.Actived;
                                            db.CompanyCheckLists.InsertOnSubmit(c);
                                            db.SubmitChanges();
                                        }
                                        else
                                        {
                                            c.Status = (byte)Status.Actived;
                                            c.UpdatedDate = DateTime.Now;
                                            c.UpdatedBy = Guid.Parse(Global.UserGUID);
                                            db.SubmitChanges();
                                        }

                                        desc += string.Format("     * GUID: '{0}', Dịch vụ: {1}\n", c.CompanyCheckListGUID.ToString(), c.Service.Name);
                                    }
                                }
                            }
                        }

                        //Tracking
                        desc = desc.Substring(0, desc.Length - 1);
                        Tracking tk = new Tracking();
                        tk.TrackingGUID = Guid.NewGuid();
                        tk.TrackingDate = DateTime.Now;
                        tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                        tk.ActionType = (byte)ActionType.Add;
                        tk.Action = "Thêm thông tin hợp đồng";
                        tk.Description = desc;
                        tk.TrackingType = (byte)TrackingType.None;
                        db.Trackings.InsertOnSubmit(tk);
                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        CompanyContract con = db.CompanyContracts.SingleOrDefault<CompanyContract>(c => c.CompanyContractGUID.ToString() == contract.CompanyContractGUID.ToString());
                        if (con != null)
                        {
                            con.CompanyGUID = contract.CompanyGUID;
                            con.ContractCode = contract.ContractCode;
                            con.ContractName = contract.ContractName;
                            con.BeginDate = contract.BeginDate;
                            con.EndDate = contract.EndDate;
                            con.Completed = contract.Completed;
                            con.CreatedDate = contract.CreatedDate;
                            con.CreatedBy = contract.CreatedBy;
                            con.UpdatedDate = contract.UpdatedDate;
                            con.UpdatedBy = contract.UpdatedBy;
                            con.DeletedDate = contract.DeletedDate;
                            con.DeletedBy = contract.DeletedBy;
                            con.Status = contract.Status;
                            con.SoTien = contract.SoTien;
                            con.DatCoc = contract.DatCoc;
                            con.NhanSuPhuTrach = contract.NhanSuPhuTrach;
                            con.SoDienThoai = contract.SoDienThoai;
                            con.NgayDatCoc = contract.NgayDatCoc;
                            db.SubmitChanges();

                            string strNgayDatCoc = string.Empty;
                            if (con.NgayDatCoc != null && con.NgayDatCoc.HasValue)
                                strNgayDatCoc = con.NgayDatCoc.Value.ToString("dd/MM/yyyy");

                            if (con.EndDate.HasValue)
                            {
                                desc += string.Format("- Hợp đồng: GUID: '{0}', Mã hợp đồng: '{1}', Tên hợp đồng: '{2}', Cty: '{3}', Ngày bắt đầu: '{4}', Ngày kết thúc: '{5}', Số tiền: '{6}', Đặt cọc: '{7}', Nhân sự phụ trách: '{8}', Số điện thoại: '{9}', Ngày đặt cọc: '{10}'\n",
                                    con.CompanyContractGUID.ToString(), con.ContractCode, con.ContractName, con.Company.TenCty,
                                    con.BeginDate.ToString("dd/MM/yyyy HH:mm:ss"), con.EndDate.Value.ToString("dd/MM/yyyy HH:mm:ss"), con.SoTien, con.DatCoc, con.NhanSuPhuTrach, con.SoDienThoai, strNgayDatCoc);
                            }
                            else
                            {
                                desc += string.Format("- Hợp đồng: GUID: '{0}', Mã hợp đồng: '{1}', Tên hợp đồng: '{2}', Cty: '{3}', Ngày bắt đầu: '{4}', Số tiền: '{5}', Đặt cọc: '{6}', Nhân sự phụ trách: '{7}', Số điện thoại: '{8}', Ngày đặt cọc: '{9}'\n",
                                    con.CompanyContractGUID.ToString(), con.ContractCode, con.ContractName, con.Company.TenCty,
                                    con.BeginDate.ToString("dd/MM/yyyy HH:mm:ss"), con.SoTien, con.DatCoc, con.NhanSuPhuTrach, con.SoDienThoai, strNgayDatCoc);
                            }

                            //Members
                            if (companyInfo.DeletedMembers != null && companyInfo.DeletedMembers.Count > 0)
                            {
                                desc += "- Danh sách nhân viên được xóa:\n";
                                foreach (string key in companyInfo.DeletedMembers)
                                {
                                    ContractMember m = db.ContractMembers.SingleOrDefault<ContractMember>(mm => mm.CompanyMemberGUID.ToString() == key &&
                                                                            mm.CompanyContractGUID.ToString() == contract.CompanyContractGUID.ToString());
                                    if (m != null)
                                    {
                                        m.Status = (byte)Status.Deactived;
                                        m.DeletedDate = DateTime.Now;
                                        m.DeletedBy = Guid.Parse(Global.UserGUID);

                                        var companyMember = db.CompanyMembers.SingleOrDefault<CompanyMember>(cm => cm.CompanyMemberGUID == m.CompanyMemberGUID);
                                        string tenNhanVien = string.Empty;
                                        if (companyMember != null) tenNhanVien = companyMember.Patient.Contact.FullName;
                                        desc += string.Format("  + GUID: '{0}', Nhân viên: '{1}'\n", m.ContractMemberGUID.ToString(), tenNhanVien);
                                    }
                                }

                                db.SubmitChanges();
                            }

                            if (companyInfo.AddedMembers != null && companyInfo.AddedMembers.Count > 0)
                            {
                                desc += "- Danh sách nhân viên được thêm:\n";
                                foreach (Member member in companyInfo.AddedMembers.Values)
                                {
                                    ContractMember m = db.ContractMembers.SingleOrDefault<ContractMember>(mm => mm.CompanyMemberGUID.ToString() == member.CompanyMemberGUID &&
                                                                                                        mm.CompanyContractGUID.ToString() == contract.CompanyContractGUID.ToString());
                                    if (m == null)
                                    {
                                        m = new ContractMember();
                                        m.ContractMemberGUID = Guid.NewGuid();
                                        m.CompanyContractGUID = contract.CompanyContractGUID;
                                        m.CompanyMemberGUID = Guid.Parse(member.CompanyMemberGUID);
                                        m.CreatedDate = DateTime.Now;
                                        m.CreatedBy = Guid.Parse(Global.UserGUID);
                                        m.Status = (byte)Status.Actived;
                                        db.ContractMembers.InsertOnSubmit(m);
                                    }
                                    else
                                    {
                                        m.Status = (byte)Status.Actived;
                                        m.UpdatedDate = DateTime.Now;
                                        m.UpdatedBy = Guid.Parse(Global.UserGUID);
                                    }

                                    db.SubmitChanges();

                                    var companyMember = db.CompanyMembers.SingleOrDefault<CompanyMember>(cm => cm.CompanyMemberGUID == m.CompanyMemberGUID);
                                    string tenNhanVien = string.Empty;
                                    if (companyMember != null) tenNhanVien = companyMember.Patient.Contact.FullName;
                                    desc += string.Format("  + GUID: '{0}', Nhân viên: '{1}'\n", m.ContractMemberGUID.ToString(), tenNhanVien);


                                    //Delete Service
                                    if (member.DeletedServices != null && member.DeletedServices.Count > 0)
                                    {
                                        desc += string.Format("     Danh sách dịch vụ được xóa:\n");

                                        foreach (string key in member.DeletedServices)
                                        {
                                            CompanyCheckList c = db.CompanyCheckLists.SingleOrDefault<CompanyCheckList>(cc => cc.ServiceGUID.ToString() == key &&
                                                                                                                cc.ContractMemberGUID.ToString() == m.ContractMemberGUID.ToString());
                                            if (c != null)
                                            {
                                                c.Status = (byte)Status.Deactived;
                                                c.DeletedDate = DateTime.Now;
                                                c.DeletedBy = Guid.Parse(Global.UserGUID);

                                                desc += string.Format("     * GUID: '{0}', Dịch vụ: {1}\n", c.CompanyCheckListGUID.ToString(), c.Service.Name);
                                            }
                                        }

                                        db.SubmitChanges();
                                    }

                                    //Add Service
                                    if (member.AddedServices != null && member.AddedServices.Count > 0)
                                    {
                                        desc += string.Format("     Danh sách dịch vụ được thêm:\n");
                                        foreach (string key in member.AddedServices)
                                        {
                                            CompanyCheckList c = db.CompanyCheckLists.SingleOrDefault<CompanyCheckList>(cc => cc.ServiceGUID.ToString() == key &&
                                                                                                                cc.ContractMemberGUID.ToString() == m.ContractMemberGUID.ToString());
                                            if (c == null)
                                            {
                                                c = new CompanyCheckList();
                                                c.CompanyCheckListGUID = Guid.NewGuid();
                                                c.ContractMemberGUID = m.ContractMemberGUID;
                                                c.ServiceGUID = Guid.Parse(key);
                                                c.CreatedDate = DateTime.Now;
                                                c.CreatedBy = Guid.Parse(Global.UserGUID);
                                                c.Status = (byte)Status.Actived;
                                                db.CompanyCheckLists.InsertOnSubmit(c);
                                                db.SubmitChanges();
                                            }
                                            else
                                            {
                                                c.Status = (byte)Status.Actived;
                                                c.UpdatedDate = DateTime.Now;
                                                c.UpdatedBy = Guid.Parse(Global.UserGUID);
                                                db.SubmitChanges();
                                            }

                                            desc += string.Format("     * GUID: '{0}', Dịch vụ: {1}\n", c.CompanyCheckListGUID.ToString(), c.Service.Name);
                                        }
                                    }
                                }
                            }

                            //Delete Gia dịch vụ hợp đồng
                            if (companyInfo.DeletedGiaDichVus != null && companyInfo.DeletedGiaDichVus.Count > 0)
                            {
                                foreach (string key in companyInfo.DeletedGiaDichVus)
                                {
                                    GiaDichVuHopDong gdvhd = db.GiaDichVuHopDongs.SingleOrDefault(g => g.GiaDichVuHopDongGUID.ToString() == key);
                                    if (gdvhd != null)
                                    {
                                        gdvhd.Status = (byte)Status.Deactived;
                                        gdvhd.DeletedBy = Guid.Parse(Global.UserGUID);
                                        gdvhd.DeletedDate = DateTime.Now;

                                        var checkList = from c in db.CompanyContracts
                                                        join m in db.ContractMembers on c.CompanyContractGUID equals m.CompanyContractGUID
                                                        join l in db.CompanyCheckLists on m.ContractMemberGUID equals l.ContractMemberGUID
                                                        where c.Status == 0 && m.Status == 0 && l.Status == 0 && c.CompanyContractGUID == gdvhd.HopDongGUID &&
                                                        l.ServiceGUID == gdvhd.ServiceGUID
                                                        select l;

                                        foreach (var cl in checkList)
                                        {
                                            cl.Status = 1;
                                        }
                                    }
                                }

                                db.SubmitChanges();
                            }

                            //Add giá dịch vụ hợp đồng
                            if (companyInfo.GiaDichVuDataSource != null && companyInfo.GiaDichVuDataSource.Rows.Count > 0)
                            {
                                foreach (DataRow row in companyInfo.GiaDichVuDataSource.Rows)
                                {
                                    GiaDichVuHopDong gdvhd = null;
                                    if (row["GiaDichVuHopDongGUID"] == null || row["GiaDichVuHopDongGUID"] == DBNull.Value)
                                    {
                                        gdvhd = new GiaDichVuHopDong();
                                        gdvhd.GiaDichVuHopDongGUID = Guid.NewGuid();
                                        gdvhd.HopDongGUID = contract.CompanyContractGUID;
                                        gdvhd.ServiceGUID = Guid.Parse(row["ServiceGUID"].ToString());
                                        gdvhd.Gia = Convert.ToDouble(row["Gia"]);
                                        gdvhd.CreatedBy = Guid.Parse(Global.UserGUID);
                                        gdvhd.CreatedDate = DateTime.Now;
                                        db.GiaDichVuHopDongs.InsertOnSubmit(gdvhd);
                                        db.SubmitChanges();
                                    }
                                    else
                                    {
                                        string giaDichVuHopDongGUID = row["GiaDichVuHopDongGUID"].ToString();
                                        gdvhd = db.GiaDichVuHopDongs.SingleOrDefault(g => g.GiaDichVuHopDongGUID.ToString() == giaDichVuHopDongGUID);
                                        if (gdvhd != null)
                                        {
                                            gdvhd.Gia = Convert.ToDouble(row["Gia"]);
                                            gdvhd.ServiceGUID = Guid.Parse(row["ServiceGUID"].ToString());
                                            gdvhd.UpdatedBy = Guid.Parse(Global.UserGUID);
                                            gdvhd.UpdatedDate = DateTime.Now;
                                            gdvhd.Status = (byte)Status.Actived;
                                        }
                                    }

                                    //Delete Dịch vụ con
                                    if (companyInfo.DictDeletedDichVuCons != null &&
                                        companyInfo.DictDeletedDichVuCons.ContainsKey(gdvhd.ServiceGUID.ToString()))
                                    {
                                        List<string> deletedDichVuConList = companyInfo.DictDeletedDichVuCons[gdvhd.ServiceGUID.ToString()];
                                        foreach (string servicecGUID in deletedDichVuConList)
                                        {
                                            DichVuCon dvc = (from d in db.DichVuCons
                                                            where d.GiaDichVuHopDongGUID == gdvhd.GiaDichVuHopDongGUID &&
                                                            d.ServiceGUID.ToString() == servicecGUID
                                                            select d).FirstOrDefault();

                                            if (dvc != null)
                                            {
                                                dvc.DeletedDate = DateTime.Now;
                                                dvc.DeletedBy = Guid.Parse(Global.UserGUID);
                                                dvc.Status = (byte)Status.Deactived;
                                            }
                                        }
                                    }

                                    //Add dịch vụ con
                                    if (companyInfo.DictDichVuCon.ContainsKey(gdvhd.ServiceGUID.ToString()))
                                    {
                                        DataTable dtDichVuCon = companyInfo.DictDichVuCon[gdvhd.ServiceGUID.ToString()];
                                        if (dtDichVuCon != null && dtDichVuCon.Rows.Count > 0)
                                        {
                                            foreach (DataRow drDichVuCon in dtDichVuCon.Rows)
                                            {
                                                string serviceGUID = drDichVuCon["ServiceGUID"].ToString();
                                                DichVuCon dvc = (from d in db.DichVuCons
                                                                 where d.GiaDichVuHopDongGUID == gdvhd.GiaDichVuHopDongGUID &&
                                                                 d.ServiceGUID.ToString() == serviceGUID
                                                                 select d).FirstOrDefault();
                                                if (dvc == null)
                                                {
                                                    dvc = new DichVuCon();
                                                    dvc.DichVuConGUID = Guid.NewGuid();
                                                    dvc.GiaDichVuHopDongGUID = gdvhd.GiaDichVuHopDongGUID;
                                                    dvc.ServiceGUID = Guid.Parse(serviceGUID);
                                                    dvc.CreatedDate = DateTime.Now;
                                                    dvc.CreatedBy = Guid.Parse(Global.UserGUID);
                                                    dvc.Status = (byte)Status.Actived;
                                                    db.DichVuCons.InsertOnSubmit(dvc);
                                                }
                                                else
                                                {
                                                    dvc.UpdatedDate = DateTime.Now;
                                                    dvc.UpdatedBy = Guid.Parse(Global.UserGUID);
                                                    dvc.Status = (byte)Status.Actived;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            //Tracking
                            desc = desc.Substring(0, desc.Length - 1);
                            Tracking tk = new Tracking();
                            tk.TrackingGUID = Guid.NewGuid();
                            tk.TrackingDate = DateTime.Now;
                            tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                            tk.ActionType = (byte)ActionType.Edit;
                            tk.Action = "Sửa thông tin hợp đồng";
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

        public static Result GetHopDongByDate(DateTime activedDate, string serviceGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                List<CompanyContractView> hopDongList = (from h in db.CompanyContractViews
                                                        where h.CompanyStatus == (byte)Status.Actived &&
                                                        h.ContractStatus == (byte)Status.Actived &&
                                                        ((h.Completed.Value && activedDate > h.BeginDate && activedDate <= h.EndDate.Value) ||
                                                        (!h.Completed.Value && activedDate > h.BeginDate))
                                                        select h).ToList<CompanyContractView>();

                List<CompanyContractView> newHopDongList = null;
                if (hopDongList != null && hopDongList.Count > 0)
                {
                    newHopDongList = new List<CompanyContractView>();
                    foreach (CompanyContractView hopDong in hopDongList)
                    {
                        var aa = (from m in db.ContractMemberViews
                                  join l in db.CompanyCheckLists on m.ContractMemberGUID equals l.ContractMemberGUID
                                  where l.ServiceGUID.ToString() == serviceGUID && m.Status == (byte)Status.Actived &&
                                  l.Status == (byte)Status.Actived && m.Archived == false &&
                                  m.CompanyContractGUID == hopDong.CompanyContractGUID
                                  select l).FirstOrDefault<CompanyCheckList>();

                        if (aa != null)
                            newHopDongList.Add(hopDong);
                    }
                }

                result.QueryResult = newHopDongList;
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

        public static Result CheckNhanVienHopDongDaSuDungDichVu(string patientGUID)
        {
            Result result = null;

            try
            {
                string spName = "spCheckMember";
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                SqlParameter param = new SqlParameter("@PatientGUID", patientGUID);
                param.Direction = ParameterDirection.Input;
                sqlParams.Add(param);

                param = new SqlParameter("@Result", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                sqlParams.Add(param);

                result = ExcuteNonQuery(spName, sqlParams);

                int count = Convert.ToInt32(param.Value);
                if (count <= 0) result.Error.Code = ErrorCode.NOT_EXIST;
                else result.Error.Code = ErrorCode.EXIST;
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

        public static Result CheckDichVuHopDongDaSuDung(string patientGUID, string serviceGUID)
        {
            Result result = null;

            try
            {
                string spName = "spCheckMemberByService";
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                SqlParameter param = new SqlParameter("@PatientGUID", patientGUID);
                param.Direction = ParameterDirection.Input;
                sqlParams.Add(param);

                param = new SqlParameter("@ServiceGUID", serviceGUID);
                param.Direction = ParameterDirection.Input;
                sqlParams.Add(param);

                param = new SqlParameter("@Result", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                sqlParams.Add(param);

                result = ExcuteNonQuery(spName, sqlParams);

                int count = Convert.ToInt32(param.Value);
                if (count <= 0) result.Error.Code = ErrorCode.NOT_EXIST;
                else result.Error.Code = ErrorCode.EXIST;
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

        public static Result CheckDichVuHopDongDaSuDungByGiaDichVu(string hopDongGUID, string serviceGUID)
        {
            Result result = null;

            try
            {
                string spName = "spCheckContractByService";
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                SqlParameter param = new SqlParameter("@ContractGUID", hopDongGUID);
                param.Direction = ParameterDirection.Input;
                sqlParams.Add(param);

                param = new SqlParameter("@ServiceGUID", serviceGUID);
                param.Direction = ParameterDirection.Input;
                sqlParams.Add(param);

                param = new SqlParameter("@Result", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                sqlParams.Add(param);

                result = ExcuteNonQuery(spName, sqlParams);

                int count = Convert.ToInt32(param.Value);
                if (count <= 0)
                {
                    string query = string.Format("SELECT TOP 1 L.* FROM CompanyContract C WITH(NOLOCK), ContractMember M WITH(NOLOCK), CompanyCheckList L WITH(NOLOCK) WHERE C.CompanyContractGUID = M.CompanyContractGUID AND M.ContractMemberGUID = L.ContractMemberGUID AND L.Status = 0 AND C.CompanyContractGUID = '{0}' AND L.ServiceGUID = '{1}'",
                        hopDongGUID, serviceGUID);
                    result = ExcuteQuery(query);

                    DataTable dt = result.QueryResult as DataTable;
                    if (dt == null || dt.Rows.Count <= 0) result.Error.Code = ErrorCode.NOT_EXIST;
                    else result.Error.Code = ErrorCode.EXIST;
                }
                else result.Error.Code = ErrorCode.EXIST;
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

        public static Result LockHopDong(List<string> keys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                string desc = string.Empty;
                db = new MMOverride();

                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (string key in keys)
                    {
                        Lock l = db.Locks.SingleOrDefault(ll => ll.KeyGUID.ToString() == key && ll.Loai == (int)LockType.HopDong);

                        if (l == null)
                        {
                            l = new Lock();
                            l.LockGUID = Guid.NewGuid();
                            l.KeyGUID = Guid.Parse(key);
                            l.Loai = (int)LockType.HopDong;
                            l.CreatedBy = Guid.Parse(Global.UserGUID);
                            l.CreatedDate = DateTime.Now;
                            l.Status = (byte)Status.Deactived;
                            db.Locks.InsertOnSubmit(l);
                        }
                        else
                        {
                            l.Status = (byte)Status.Deactived;
                        }

                        CompanyContract contract = db.CompanyContracts.SingleOrDefault(c => c.CompanyContractGUID == l.KeyGUID);

                        if (contract != null)
                        {
                            string strEndDate = string.Empty;
                            if (contract.EndDate.HasValue)
                                strEndDate = contract.EndDate.Value.ToString("dd/MM/yyyy HH:mm:ss");

                            desc += string.Format("- GUID: '{0}', Mã hợp đồng: '{1}', Tên hợp đồng: '{2}', Cty: '{3}', Ngày bắt đầu: '{4}', Ngày kết thúc: '{5}'\n",
                                       contract.CompanyContractGUID.ToString(), contract.ContractCode, contract.ContractName, contract.Company.TenCty,
                                       contract.BeginDate.ToString("dd/MM/yyyy HH:mm:ss"), strEndDate);
                        }


                    }

                    //Tracking
                    if (desc != string.Empty)
                        desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Khóa hợp đồng";
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

        public static Result UnlockHopDong(List<string> keys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                string desc = string.Empty;
                db = new MMOverride();

                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (string key in keys)
                    {
                        Lock l = db.Locks.SingleOrDefault(ll => ll.KeyGUID.ToString() == key && ll.Loai == (int)LockType.HopDong);

                        if (l == null)
                        {
                            l = new Lock();
                            l.LockGUID = Guid.NewGuid();
                            l.KeyGUID = Guid.Parse(key);
                            l.Loai = (int)LockType.HopDong;
                            l.CreatedBy = Guid.Parse(Global.UserGUID);
                            l.CreatedDate = DateTime.Now;
                            l.Status = (byte)Status.Actived;
                            db.Locks.InsertOnSubmit(l);
                        }
                        else
                        {
                            l.Status = (byte)Status.Actived;
                        }

                        CompanyContract contract = db.CompanyContracts.SingleOrDefault(c => c.CompanyContractGUID == l.KeyGUID);

                        if (contract != null)
                        {
                            string strEndDate = string.Empty;
                            if (contract.EndDate.HasValue)
                                strEndDate = contract.EndDate.Value.ToString("dd/MM/yyyy HH:mm:ss");

                            desc += string.Format("- GUID: '{0}', Mã hợp đồng: '{1}', Tên hợp đồng: '{2}', Cty: '{3}', Ngày bắt đầu: '{4}', Ngày kết thúc: '{5}'\n",
                                       contract.CompanyContractGUID.ToString(), contract.ContractCode, contract.ContractName, contract.Company.TenCty,
                                       contract.BeginDate.ToString("dd/MM/yyyy HH:mm:ss"), strEndDate);
                        }

                        
                    }

                    //Tracking
                    if (desc != string.Empty)
                        desc = desc.Substring(0, desc.Length - 1);
                    Tracking tk = new Tracking();
                    tk.TrackingGUID = Guid.NewGuid();
                    tk.TrackingDate = DateTime.Now;
                    tk.DocStaffGUID = Guid.Parse(Global.UserGUID);
                    tk.ActionType = (byte)ActionType.Delete;
                    tk.Action = "Mở khóa hợp đồng";
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

        public static Result GetGiaDichVuHopDong(string hopDongGUID, string serviceGUID)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                GiaDichVuHopDong giaDichVuHopDong = db.GiaDichVuHopDongs.SingleOrDefault(g => g.HopDongGUID.ToString() == hopDongGUID &&
                    g.ServiceGUID.ToString() == serviceGUID && g.Status == 0);
                if (giaDichVuHopDong != null)
                    result.QueryResult = giaDichVuHopDong.Gia;
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

        public static Result GetCompanyMember(string companyGUID, string hoTen, string namSinh, string gioiTinh)
        {
            Result result = null;

            try
            {
                string query = string.Empty;
                if (gioiTinh.Trim() == string.Empty)
                {
                    query = string.Format("SELECT TOP 1 * FROM CompanyMemberView WITH(NOLOCK) WHERE CompanyGUID='{0}' AND Status={1} AND Archived='False' AND FullName = N'{2}'",
                    companyGUID, (byte)Status.Actived, hoTen);
                }
                else
                {
                    query = string.Format("SELECT TOP 1 * FROM CompanyMemberView WITH(NOLOCK) WHERE CompanyGUID='{0}' AND Status={1} AND Archived='False' AND FullName = N'{2}' AND GenderAsStr = N'{3}'",
                    companyGUID, (byte)Status.Actived, hoTen, gioiTinh);
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
    }
}
