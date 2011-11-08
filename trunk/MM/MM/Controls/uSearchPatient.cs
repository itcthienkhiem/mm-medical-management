﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Controls
{
    public partial class uSearchPatient : uBase
    {
        #region Members
        private object _dataSource = null;
        #endregion

        #region Constructor
        public uSearchPatient()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public object DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        public object PatientRow
        {
            get
            {
                if (dgPatient.SelectedRows != null && dgPatient.SelectedRows.Count > 0)
                    return (dgPatient.SelectedRows[0].DataBoundItem as DataRowView).Row;

                return null;
            }
        }
        #endregion

        #region UI Command
        private void OnSearch()
        {
            if (txtSearchPatient.Text.Trim() == string.Empty)
            {
                dgPatient.DataSource = null;
                return;
            }

            string str = txtSearchPatient.Text.ToLower();
            DataTable dt = _dataSource as DataTable;

            //Fullname
            var results = from p in dt.AsEnumerable()
                          where (p.Field<string>("Fullname").ToLower().IndexOf(str) >= 0 ||
                          str.IndexOf(p.Field<string>("Fullname").ToLower()) >= 0) &&
                          p.Field<string>("Fullname") != null &&
                          p.Field<string>("Fullname").Trim() != string.Empty
                          select p;

            DataTable newDataSource = dt.Clone();
            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource; 
                return;
            }
            

            //FileNum
            results = from p in dt.AsEnumerable()
                      where (p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0 ||
                      str.IndexOf(p.Field<string>("FileNum").ToLower()) >= 0) &&
                          p.Field<string>("FileNum") != null &&
                          p.Field<string>("FileNum").Trim() != string.Empty
                      select p;

            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource;
                return;
            }

            //HomePhone
            results = from p in dt.AsEnumerable()
                      where (p.Field<string>("HomePhone").ToLower().IndexOf(str) >= 0 ||
                      str.IndexOf(p.Field<string>("HomePhone").ToLower()) >= 0) &&
                      p.Field<string>("HomePhone") != null && 
                      p.Field<string>("HomePhone").Trim() != string.Empty
                      select p;

            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource;
                return;
            }

            //WorkPhone
            results = from p in dt.AsEnumerable()
                      where (p.Field<string>("WorkPhone").ToLower().IndexOf(str) >= 0 ||
                      str.IndexOf(p.Field<string>("WorkPhone").ToLower()) >= 0) &&
                          p.Field<string>("WorkPhone") != null &&
                          p.Field<string>("WorkPhone").Trim() != string.Empty
                      select p;

            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgPatient.DataSource = newDataSource;
                return;
            }

            //Mobile
            results = from p in dt.AsEnumerable()
                      where (p.Field<string>("Mobile").ToLower().IndexOf(str) >= 0 ||
                      str.IndexOf(p.Field<string>("Mobile").ToLower()) >= 0) &&
                          p.Field<string>("Mobile") != null &&
                          p.Field<string>("Mobile").Trim() != string.Empty
                      select p;

            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            dgPatient.DataSource = newDataSource;
        }

        private void RaiseOpentPatient()
        {
            object patientRow = this.PatientRow;
            if (patientRow != null)
                base.RaiseOpentPatient(patientRow);
        }
        #endregion

        #region Window Event Handlers
        private void txtSearchPatient_TextChanged(object sender, EventArgs e)
        {
            OnSearch();
        }

        private void dgPatient_DoubleClick(object sender, EventArgs e)
        {
            RaiseOpentPatient();
        }

        private void dgPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                RaiseOpentPatient();
        }

        private void txtSearchPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgPatient.Focus();

                if (dgPatient.SelectedRows != null && dgPatient.SelectedRows.Count > 0)
                {
                    int index = dgPatient.SelectedRows[0].Index;
                    if (index < dgPatient.RowCount - 1)
                    {
                        index++;
                        dgPatient.CurrentCell = dgPatient.Rows[index].Cells[1];
                        dgPatient.Rows[index].Selected = true;
                    }
                }
            }
        }
        #endregion
    }
}
