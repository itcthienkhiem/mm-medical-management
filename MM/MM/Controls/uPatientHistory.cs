using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using MM.Bussiness;
using MM.Common;
using MM.Databasae;

namespace MM.Controls
{
    public partial class uPatientHistory : uBase
    {
        #region Members
        private object _patientRow = null;
        #endregion

        #region Constructor
        public uPatientHistory()
        {
            InitializeComponent();
            //DockContainerItem item = new DockContainerItem("Name", "Name");
            //PanelDockContainer p = new PanelDockContainer();
            //TextBox text = new TextBox();
            //p.Controls.Add(text);
            //item.Control = p;
            

            //docBarManager.Items.Add(item);
        }
        #endregion

        #region Properties
        public object PatientRow
        {
            get { return _patientRow; }
            set { _patientRow = value; }
        }
        #endregion

        #region UI Command

        #endregion

        #region Window Event Handlers

        #endregion
    }
}
