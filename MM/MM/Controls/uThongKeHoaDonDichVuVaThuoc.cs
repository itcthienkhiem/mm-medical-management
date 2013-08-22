﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Exports;

namespace MM.Controls
{
    public partial class uThongKeHoaDonDichVuVaThuoc : uBase
    {
        #region Members

        #endregion

        #region Constructor
        public uThongKeHoaDonDichVuVaThuoc()
        {
            InitializeComponent();
            dtpkTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            dtpkDenNgay.Value = DateTime.Now;
        }
        #endregion

        #region UI Command
        public void InitData()
        {
            btnExportExcel.Enabled = AllowExport;
        }

        private void OnExportExcel()
        {
            Cursor.Current = Cursors.WaitCursor;
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export Excel";
            dlg.Filter = "Excel Files(*.xls,*.xlsx)|*.xls;*.xlsx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ExportExcel.ExportHoaDonDichVuVaThuocToExcel(dlg.FileName, dtpkTuNgay.Value, dtpkDenNgay.Value);
            }
        }
        #endregion

        #region Window Event Handlers
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            OnExportExcel();
        }
        #endregion
    }
}
