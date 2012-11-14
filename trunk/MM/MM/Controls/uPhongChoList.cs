using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Controls
{
    public partial class uPhongChoList : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uPhongChoList()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool IsEnableBtnRaPhongCho
        {
            set 
            { 
                btnDelete.Enabled = value;
                chkChecked.Enabled = value;

                if (!value)
                {
                    dgPatient.DataSource = null;
                    StopTimer();
                }
                else
                    StartTimer();
            }
        }
        #endregion

        #region UI Command
        public void StartTimer()
        {
            _timer.Enabled = true;
            _timer.Start();
        }

        public void StopTimer()
        {
            _timer.Stop();
            _timer.Enabled = false;
        }

        private void RefreshNo()
        {
            int stt = 1;
            foreach (DataGridViewRow row in dgPatient.Rows)
            {
                row.Cells["STT"].Value = stt++;
            }
        }

        public void ClearData()
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgPatient.DataSource = null;
            }
        }

        private void OnDisplayPhongChoList()
        {
            Result result = PhongChoBus.GetPhongChoList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    
                    DataTable dt = dgPatient.DataSource as DataTable;
                    if (dt == null)
                        dgPatient.DataSource = result.QueryResult;
                    else
                    {
                        int selectedIndex = 0;
                        if (dgPatient.SelectedRows != null && dgPatient.SelectedRows.Count > 0)
                            selectedIndex = dgPatient.SelectedRows[0].Index;

                        DataTable dt2 = result.QueryResult as DataTable;
                        foreach (DataRow row in dt2.Rows)
                        {
                            string patientGUID = row["PatientGUID"].ToString();
                            DataRow[] rows = dt.Select(string.Format("PatientGUID='{0}'", patientGUID));
                            if (rows != null && rows.Length > 0)
                            {
                                row["Checked"] = rows[0]["Checked"];
                            }
                        }

                        ClearData();
                        dgPatient.DataSource = dt2;

                        if (selectedIndex <= dgPatient.RowCount - 1)
                        {
                            dgPatient.CurrentCell = dgPatient[1, selectedIndex];
                            dgPatient.Rows[selectedIndex].Selected = true;
                        }
                    }

                    RefreshNo();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhongChoBus.GetPhongChoList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PhongChoBus.GetPhongChoList"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void _timer_Tick(object sender, EventArgs e)
        {
            OnDisplayPhongChoList();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgPatient.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<string> deletedPhongChoList = new List<string>();
            DataTable dt = dgPatient.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedPhongChoList.Add(row["PhongChoGUID"].ToString());
                }
            }

            if (deletedPhongChoList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn cho những bệnh nhân đã đánh dấu ra khỏi phòng chờ ?") == DialogResult.Yes)
                {
                    Result result = PhongChoBus.DeletePhongCho(deletedPhongChoList);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("PhongChoBus.DeletePhongCho"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("PhongChoBus.DeletePhongCho"));
                    }
                }
            }
            else
                MsgBox.Show(Application.ProductName, "Vui lòng đánh dấu những bệnh nhân ra khỏi phòng chờ.", IconType.Information);
        }

        private void dgPatient_DoubleClick(object sender, EventArgs e)
        {
            if (dgPatient.SelectedRows == null || dgPatient.SelectedRows.Count <= 0) return;

            DataRow drPatient = (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row;
            base.RaiseOpentPatient(drPatient);
        }
        #endregion
    }
}
