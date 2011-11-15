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
    public class PatientBus : BusBase
    {
        public static Result GetPatientList()
        {
            Result result = null;

            try
            {
                string query = "SELECT  CAST(0 AS Bit) AS Checked, * FROM PatientView WHERE Archived = 'True' ORDER BY FullName";
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

        public static Result DeletePatient(List<String> patientKeys)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    foreach (string key in patientKeys)
                    {
                        Patient patient = db.Patients.SingleOrDefault<Patient>(p => p.PatientGUID.ToString() == key);
                        if (patient != null)
                        {
                            Contact contact = patient.Contact;
                            if (contact != null)
                            {
                                contact.Archived = false;
                                contact.DateArchived = DateTime.Now;
                                contact.DeletedBy = Guid.Parse(Global.UserGUID);
                                contact.DeletedDate = DateTime.Now;
                            }
                        }

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

        public static Result InsertPatient(Contact contact, Patient patient, PatientHistory patientHistory)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();

                using (TransactionScope t = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    //Insert
                    if (contact.ContactGUID == null || contact.ContactGUID == Guid.Empty)
                    {
                        contact.ContactGUID = Guid.NewGuid();
                        db.Contacts.InsertOnSubmit(contact);
                        db.SubmitChanges();
                        patient.PatientGUID = Guid.NewGuid();
                        patient.ContactGUID = contact.ContactGUID;
                        db.Patients.InsertOnSubmit(patient);
                        db.SubmitChanges();
                    }
                    else //Update
                    {
                        Contact ct = db.Contacts.SingleOrDefault<Contact>(cc => cc.ContactGUID.ToString() == contact.ContactGUID.ToString());
                        if (ct != null)
                        {
                            ct.Address = contact.Address;
                            ct.AliasFirstName = contact.AliasFirstName;
                            ct.AliasSurName = contact.AliasSurName;
                            ct.Archived = contact.Archived;
                            ct.City = contact.City;
                            ct.CreatedBy = contact.CreatedBy;
                            ct.CreatedDate = contact.CreatedDate;
                            ct.DateArchived = contact.DateArchived;
                            ct.DeletedBy = contact.DeletedBy;
                            ct.DeletedDate = contact.DeletedDate;
                            ct.District = contact.District;
                            ct.Dob = contact.Dob;
                            ct.DobStr = contact.DobStr;
                            ct.Email = contact.Email;
                            ct.FAX = contact.FAX;
                            ct.FullName = contact.FullName;
                            ct.FirstName = contact.FirstName;
                            ct.Gender = contact.Gender;
                            ct.HomePhone = contact.HomePhone;
                            ct.IdentityCard = contact.IdentityCard;
                            ct.KnownAs = contact.KnownAs;
                            ct.MiddleName = contact.MiddleName;
                            ct.Mobile = contact.Mobile;
                            ct.Note = contact.Note;
                            ct.Occupation = contact.Occupation;
                            ct.PreferredName = contact.PreferredName;
                            ct.SurName = contact.SurName;
                            ct.Title = contact.Title;
                            ct.UpdatedBy = contact.UpdatedBy;
                            ct.UpdatedDate = contact.UpdatedDate;
                            ct.Ward = contact.Ward;
                            ct.WorkPhone = contact.WorkPhone;

                            Patient p = db.Patients.SingleOrDefault<Patient>(pp => pp.PatientGUID.ToString() == patient.PatientGUID.ToString());
                            if (p != null)
                            {
                                p.FileNum = patient.FileNum;
                                p.BarCode = patient.BarCode;
                                p.Picture = patient.Picture;
                                p.HearFrom = patient.HearFrom;
                                p.Salutation = patient.Salutation;
                                p.LastSeenDate = patient.LastSeenDate;
                                p.LastSeenDocGUID = patient.LastSeenDocGUID;
                                p.DateDeceased = patient.DateDeceased;
                                p.LastVisitGUID = patient.LastVisitGUID;
                            }

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

        public static Result CheckPatientExistFileNum(string patientGUID, string fileNum)
        {
            Result result = new Result();
            MMOverride db = null;

            try
            {
                db = new MMOverride();
                Patient patient = null;
                if (patientGUID == null || patientGUID == string.Empty)
                    patient = db.Patients.SingleOrDefault<Patient>(p => p.FileNum.Trim().ToLower() == fileNum.Trim().ToLower());
                else
                    patient = db.Patients.SingleOrDefault<Patient>(p => p.FileNum.Trim().ToLower() == fileNum.Trim().ToLower() &&
                                                                p.PatientGUID.ToString() != patientGUID);

                if (patient == null)
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
    }
}
