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
using MM.Dialogs;

namespace MM.Controls
{
    public partial class uDanhSachXetNghiemHitachi917List : uBase
    {
        #region Members
        private DataTable _dataSource = null;
        private DataRow _row = null;
        #endregion

        #region Constructor
        public uDanhSachXetNghiemHitachi917List()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void HideChiSoXetNghiem()
        {
            _uChiSoXetNghiem_Glucose_Hitachi917.Visible = false;
            _uChiSoXetNghiem_Hitachi917.Visible = false;
        }

        public void DisplayAsThread()
        {
            try
            {
                HideChiSoXetNghiem();
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayXetNghiemListProc));
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

        private void OnDisplayXetNghiemList()
        {
            Result result = XetNghiem_Hitachi917Bus.GetDanhSachXetNghiem();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    _dataSource = result.QueryResult as DataTable;
                    OnSearchXetNghiem();
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("XetNghiem_Hitachi917Bus.GetXetNghiemList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("XetNghiem_Hitachi917Bus.GetXetNghiemList"));
            }
        }

        private DataRow GetDataRow(string xetNghiemGUID)
        {
            if (_dataSource == null || _dataSource.Rows.Count <= 0) return null;
            DataRow[] rows = _dataSource.Select(string.Format("XetNghiemGUID = '{0}'", xetNghiemGUID));
            if (rows == null || rows.Length <= 0) return null;

            return rows[0];
        }

        private void OnDisplayChiSoXetNghiem()
        {
            try
            {
                HideChiSoXetNghiem();
                if (dgXetNghiem.RowCount <= 0) return;
                if (dgXetNghiem.SelectedRows == null || dgXetNghiem.SelectedRows.Count <= 0) return;

                string xetNghiemGUID = (dgXetNghiem.SelectedRows[0].DataBoundItem as DataRowView).Row["XetNghiemGUID"].ToString();
                _row = GetDataRow(xetNghiemGUID);

                int testNum = Convert.ToInt32(_row["TestNum"]);
                if (testNum == 17) //Glucose
                {
                    _uChiSoXetNghiem_Glucose_Hitachi917.Visible = true;
                    _uChiSoXetNghiem_Glucose_Hitachi917.Enabled = AllowEdit;
                    _uChiSoXetNghiem_Glucose_Hitachi917.DisplayInfo(_row);
                }
                else
                {
                    _uChiSoXetNghiem_Hitachi917.Visible = true;
                    _uChiSoXetNghiem_Hitachi917.Enabled = AllowEdit;
                    _uChiSoXetNghiem_Hitachi917.DisplayInfo(_row);
                }
            }
            catch (Exception e)
            {
                MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnSearchXetNghiem()
        {
            HideChiSoXetNghiem();
            List<DataRow> results = null;
            DataTable newDataSource = null;

            if (txtXetNghiem.Text.Trim() == string.Empty)
            {
                results = (from p in _dataSource.AsEnumerable()
                           orderby p.Field<string>("Fullname")
                           select p).ToList<DataRow>();

                newDataSource = _dataSource.Clone();
                foreach (DataRow row in results)
                    newDataSource.ImportRow(row);

                dgXetNghiem.DataSource = newDataSource;
                if (dgXetNghiem.RowCount > 0) dgXetNghiem.Rows[0].Selected = true;
                return;
            }

            string str = txtXetNghiem.Text.ToLower();
            newDataSource = _dataSource.Clone();

            //Name
            results = (from p in _dataSource.AsEnumerable()
                       where p.Field<string>("Fullname") != null &&
                           p.Field<string>("Fullname").Trim() != string.Empty &&
                           p.Field<string>("Fullname").ToLower().IndexOf(str) >= 0
                       orderby p.Field<string>("Fullname")
                       select p).ToList<DataRow>();

            foreach (DataRow row in results)
                newDataSource.ImportRow(row);

            if (newDataSource.Rows.Count > 0)
            {
                dgXetNghiem.DataSource = newDataSource;
                return;
            }

            dgXetNghiem.DataSource = newDataSource;
        }
        #endregion

        #region Window Event Handlers
        private void txtXetNghiem_TextChanged(object sender, EventArgs e)
        {
            OnSearchXetNghiem();
        }

        private void dgXetNghiem_SelectionChanged(object sender, EventArgs e)
        {
            OnDisplayChiSoXetNghiem();
        }

        #endregion

        #region Working Thread
        private void OnDisplayXetNghiemListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayXetNghiemList();
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
