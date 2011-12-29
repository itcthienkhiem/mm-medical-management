using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uChiDinhList : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        private Hashtable _htDichVuChiDinh = null;
        #endregion

        #region Constructor
        public uChiDinhList()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public DataRow PatientRow
        {
            get { return _patientRow; }
            set { _patientRow = value; }
        }
        #endregion

        #region UI Command
        private void UpdateGUI()
        {
            btnAdd.Enabled = Global.AllowAddChiDinh;
            btnEdit.Enabled = Global.AllowEditChiDinh;
            btnDelete.Enabled = Global.AllowDeleteChiDinh;
            btnConfirm.Enabled = Global.AllowConfirmChiDinh;
        }

        public void DisplayAsThread()
        {
            try
            {
                UpdateGUI();
                chkChecked.Checked = false;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayChiDinhListProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnDisplayChiDinhList()
        {
            if (_patientRow == null) return;
            string patientGUID = _patientRow["PatientGUID"].ToString();
            Result result = ChiDinhBus.GetChiDinhList(patientGUID);
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgChiDinh.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ChiDinhBus.GetChiDinhList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.GetChiDinhList"));
            }
        }

        private void OnGetDichVuChiDinh()
        {
            if (_htDichVuChiDinh == null) _htDichVuChiDinh = new Hashtable();
            else _htDichVuChiDinh.Clear();

            foreach (DataGridViewRow row in dgChiDinh.Rows)
            {
                DataRow r = (row.DataBoundItem as DataRowView).Row;
                string chiDinhGUID = r["ChiDinhGUID"].ToString();
                Result result = ChiDinhBus.GetDichVuChiDinhList(chiDinhGUID);
                if (result.IsOK)
                {
                    List<DichVuChiDinhView> dichVuChiDinhList = (List<DichVuChiDinhView>)result.QueryResult;
                    _htDichVuChiDinh.Add(chiDinhGUID, dichVuChiDinhList);

                    if (dichVuChiDinhList != null && dichVuChiDinhList.Count > 0)
                    {
                        (row.Cells["Checked"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                        row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                    }
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("ChiDinhBus.GetDichVuChiDinhList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.GetDichVuChiDinhList"));
                    return;
                }
            }
        }

        private void OnDisplayChiTietChiDinh()
        {
            string chiDinhGUID = string.Empty;
            if (dgChiDinh.SelectedRows == null || dgChiDinh.SelectedRows.Count <= 0)
                chiDinhGUID = Guid.Empty.ToString();
            else
            {
                DataRow row = (dgChiDinh.SelectedRows[0].DataBoundItem as DataRowView).Row;
                chiDinhGUID = row["ChiDinhGUID"].ToString();
            }

            Result result = ChiDinhBus.GetChiTietChiDinhList(chiDinhGUID);
            if (result.IsOK)
            {
                dgChiTiet.DataSource = result.QueryResult;

                if (_htDichVuChiDinh.ContainsKey(chiDinhGUID))
                {
                    List<DichVuChiDinhView> dichVuChiDinhList = (List<DichVuChiDinhView>)_htDichVuChiDinh[chiDinhGUID];
                    foreach (DataGridViewRow row in dgChiTiet.Rows)
                    {
                        DataRow r = (row.DataBoundItem as DataRowView).Row;
                        string serviceGUID = r["ServiceGUID"].ToString();
                        foreach (var dvcd in dichVuChiDinhList)
                        {
                            if (serviceGUID == dvcd.ServiceGUID.ToString())
                            {
                                (row.Cells["ChiTietChiDinhChecked"] as DataGridViewDisableCheckBoxCell).Enabled = false;
                                row.DefaultCellStyle.BackColor = Color.LightSeaGreen;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ChiDinhBus.GetChiTietChiDinhList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ChiDinhBus.GetChiTietChiDinhList"));
            }
        }

        private void OnAddChiDinh()
        {

        }

        private void OnEditChiDinh()
        {

        }

        private void OnDeleteChiDinh()
        {

        }

        private void OnConfirmDichVuChiDinh()
        {

        }
        #endregion

        #region Window Event Handlers
        private void dgChiDinh_SelectionChanged(object sender, EventArgs e)
        {
            OnDisplayChiTietChiDinh();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            if (dgChiDinh.RowCount <= 0) return;
            foreach (DataGridViewRow row in dgChiDinh.Rows)
            {
                DataGridViewDisableCheckBoxCell cell = row.Cells["Checked"] as DataGridViewDisableCheckBoxCell;
                if (cell.Enabled)
                {
                    DataRow r = (row.DataBoundItem as DataRowView).Row;
                    r["Checked"] = chkChecked.Checked;
                }
            }
        }

        private void chkChiTietChecked_CheckedChanged(object sender, EventArgs e)
        {
            if (dgChiTiet.RowCount <= 0) return;
            foreach (DataGridViewRow row in dgChiTiet.Rows)
            {
                DataGridViewDisableCheckBoxCell cell = row.Cells["ChiTietChiDinhChecked"] as DataGridViewDisableCheckBoxCell;
                if (cell.Enabled)
                {
                    DataRow r = (row.DataBoundItem as DataRowView).Row;
                    r["Checked"] = chkChecked.Checked;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OnAddChiDinh();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEditChiDinh();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnDeleteChiDinh();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            OnConfirmDichVuChiDinh();
        }
        #endregion

        #region Working Thread
        private void OnDisplayChiDinhListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayChiDinhList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion

        
    }
}
