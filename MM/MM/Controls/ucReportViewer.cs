using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace MM.Controls
{
    public partial class ucReportViewer : UserControl
    {
        #region Members

        #endregion

        #region Constructor
        public ucReportViewer()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        public void ViewReport(string reportEmbeddedResource, ReportDataSource reportDataSource)
        {
            _reportViewer.LocalReport.DataSources.Clear();
            _reportViewer.LocalReport.DataSources.Add(reportDataSource);
            _reportViewer.LocalReport.ReportEmbeddedResource = reportEmbeddedResource;
            _reportViewer.RefreshReport();
        }
        #endregion
    }
}
