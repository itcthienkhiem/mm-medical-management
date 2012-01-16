using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgAddSymptom : dlgBase
    {
        #region Members
        private bool _isNew = true;
        private Symptom _symptom = new Symptom();
        #endregion

        #region Constructor
        public dlgAddSymptom()
        {
            InitializeComponent();
            GenerateCode();
        }

        public dlgAddSymptom(DataRow drSymp)
        {
            InitializeComponent();
            _isNew = false;
            this.Text = "Sua trieu chung";
            DisplayInfo(drSymp);
        }
        #endregion

        #region Properties
        public Symptom Symptom
        {
            get { return _symptom; }
            set { _symptom = value; }
        }
        #endregion

        #region UI Command
        private void GenerateCode()
        {
            Cursor.Current = Cursors.WaitCursor;
            Result result = SymptomBus.GetSymptomCount();
            if (result.IsOK)
            {
                int count = Convert.ToInt32(result.QueryResult);
                txtCode.Text = Utility.GetCode("TC", count + 1, 4);
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("SymptomBus.GetSymptomCount"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("SymptomBus.GetSymptomCount"));
            }
        }

        private void DisplayInfo(DataRow drSymp)
        {
            try
            {
                txtCode.Text = drSymp["Code"] as string;
                txtSymptom.Text = drSymp["SymptomName"] as string;
                txtAdvice.Text = drSymp["Advice"] as string;

                _symptom.SymptomGUID = Guid.Parse(drSymp["SymptomGUID"].ToString());

                if (drSymp["CreatedDate"] != null && drSymp["CreatedDate"] != DBNull.Value)
                    _symptom.CreatedDate = Convert.ToDateTime(drSymp["CreatedDate"]);

                if (drSymp["CreatedBy"] != null && drSymp["CreatedBy"] != DBNull.Value)
                    _symptom.CreatedBy = Guid.Parse(drSymp["CreatedBy"].ToString());

                if (drSymp["UpdatedDate"] != null && drSymp["UpdatedDate"] != DBNull.Value)
                    _symptom.UpdatedDate = Convert.ToDateTime(drSymp["UpdatedDate"]);

                if (drSymp["UpdatedBy"] != null && drSymp["UpdatedBy"] != DBNull.Value)
                    _symptom.UpdatedBy = Guid.Parse(drSymp["UpdatedBy"].ToString());

                if (drSymp["DeletedDate"] != null && drSymp["DeletedDate"] != DBNull.Value)
                    _symptom.DeletedDate = Convert.ToDateTime(drSymp["DeletedDate"]);

                if (drSymp["DeletedBy"] != null && drSymp["DeletedBy"] != DBNull.Value)
                    _symptom.DeletedBy = Guid.Parse(drSymp["DeletedBy"].ToString());

                _symptom.Status = Convert.ToByte(drSymp["Status"]);
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private bool CheckInfo()
        {
            if (txtCode.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập mã triệu chứng.", IconType.Information);
                txtCode.Focus();
                return false;
            }

            if (txtSymptom.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập triệu chứng.", IconType.Information);
                txtSymptom.Focus();
                return false;
            }

            if (txtAdvice.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập lời khuyên.", IconType.Information);
                txtAdvice.Focus();
                return false;
            }

            string sympGUID = _isNew ? string.Empty : _symptom.SymptomGUID.ToString();
            Result result = SymptomBus.CheckSymptomExistCode(sympGUID, txtCode.Text);

            if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
            {
                if (result.Error.Code == ErrorCode.EXIST)
                {
                    MsgBox.Show(this.Text, "Mã triệu chứng này đã tồn tại rồi. Vui lòng nhập mã khác.", IconType.Information);
                    txtCode.Focus();
                    return false;
                }
            }
            else
            {
                MsgBox.Show(this.Text, result.GetErrorAsString("SymptomBus.CheckSymptomExistCode"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("SymptomBus.CheckSymptomExistCode"));
                return false;
            }

            return true;
        }

        private void SaveInfoAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSaveInfoProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnSaveInfo()
        {
            try
            {
                _symptom.Code = txtCode.Text;
                _symptom.SymptomName = txtSymptom.Text;
                _symptom.Advice = txtAdvice.Text;
                _symptom.Status = (byte)Status.Actived;

                if (_isNew)
                {
                    _symptom.CreatedDate = DateTime.Now;
                    _symptom.CreatedBy = Guid.Parse(Global.UserGUID);
                }
                else
                {
                    _symptom.UpdatedDate = DateTime.Now;
                    _symptom.UpdatedBy = Guid.Parse(Global.UserGUID);
                }

                Result result = SymptomBus.InsertSymptom(_symptom);
                if (!result.IsOK)
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("SymptomBus.InsertSymptom"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("SymptomBus.InsertSymptom"));
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
           
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddSymptom_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (CheckInfo())
                    SaveInfoAsThread();
                else
                    e.Cancel = true;
            }
            else
            {
                if (MsgBox.Question(this.Text, "Bạn có muốn lưu thông tin triệu chứng ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (CheckInfo())
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        SaveInfoAsThread();
                    }
                    else
                        e.Cancel = true;
                }
            }
        }
        #endregion

        #region Working Thread
        private void OnSaveInfoProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnSaveInfo();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion
    }
}
