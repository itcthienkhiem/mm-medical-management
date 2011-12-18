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
using MM.Databasae;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgUpdateCheckList : Form
    {
        #region Members
        private Member _member; 
        #endregion

        #region Contructor
        public dlgUpdateCheckList(Member member)
        {
            InitializeComponent();
            _member = member;
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void OnAddService()
        {
            dlgServices dlg = new dlgServices(_member.ConstractGUID, _member.CompanyMemberGUID, _member.AddedServices, _member.DeletedServiceRows);
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                List<DataRow> checkedRows = dlg.Services;
                DataTable dataSource = dgService.DataSource as DataTable;
                if (dataSource == null)
                {
                    dataSource = checkedRows[0].Table.Clone();
                    dgService.DataSource = dataSource;
                }

                foreach (DataRow row in checkedRows)
                {
                    string serviceGUID = row["ServiceGUID"].ToString();
                    DataRow[] rows = dataSource.Select(string.Format("ServiceGUID='{0}'", serviceGUID));
                    if (rows == null || rows.Length <= 0)
                    {
                        DataRow newRow = dataSource.NewRow();
                        newRow["Checked"] = false;
                        newRow["ServiceGUID"] = serviceGUID;
                        newRow["Code"] = row["Code"];
                        newRow["Name"] = row["Name"];
                        newRow["Price"] = row["Price"];
                        dataSource.Rows.Add(newRow);

                        if (!_member.AddedServices.Contains(serviceGUID))
                            _member.AddedServices.Add(serviceGUID);

                        _member.DeletedServices.Remove(serviceGUID);
                        foreach (DataRow r in _member.DeletedServiceRows)
                        {
                            if (r["ServiceGUID"].ToString() == serviceGUID)
                            {
                                _member.DeletedServiceRows.Remove(r);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void OnDeleteService()
        {
            List<string> deletedSrvList = new List<string>();
            List<DataRow> deletedRows = new List<DataRow>();
            DataTable dt = dgService.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                if (Boolean.Parse(row["Checked"].ToString()))
                {
                    deletedSrvList.Add(row["ServiceGUID"].ToString());
                    deletedRows.Add(row);
                }
            }

            if (deletedSrvList.Count > 0)
            {
                if (MsgBox.Question(Application.ProductName, "Bạn có muốn xóa những dịch vụ mà bạn đã đánh dấu ?") == DialogResult.Yes)
                {
                    foreach (DataRow row in deletedRows)
                    {
                        string serviceGUID = row["ServiceGUID"].ToString();
                        if (!_member.DeletedServices.Contains(serviceGUID))
                        {
                            _member.DeletedServices.Add(serviceGUID);
                            DataRow r = dt.NewRow();
                            r.ItemArray = row.ItemArray;
                            _member.DeletedServiceRows.Add(r);
                        }

                        _member.AddedServices.Remove(serviceGUID);

                        dt.Rows.Remove(row);
                    }
                }
            }
            else
                MsgBox.Show(this.Text, "Vui lòng đánh dấu những dịch vụ cần xóa.", IconType.Information);
        }
        #endregion

        #region Window Event Handlers
        private void dlgUpdateCheckList_Load(object sender, EventArgs e)
        {
            dgService.DataSource = _member.DataSource;
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgService.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            OnAddService();
        }

        private void btnDeleteService_Click(object sender, EventArgs e)
        {
            OnDeleteService();
        }
        #endregion
    }
}
