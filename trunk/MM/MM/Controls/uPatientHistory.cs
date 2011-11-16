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
        private bool _isFirst = true;
        #endregion

        #region Constructor
        public uPatientHistory()
        {
            InitializeComponent();
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
        public void Display()
        {
            DataRow row = _patientRow as DataRow;
            string fileNum = row["FileNum"] as string;
            string fullName = row["Fullname"] as string;

            DockContainerItem item = GetDockContainerItem(fileNum);
            if (item == null)
                AddDockContainerItem(fileNum, fullName);
            else
            {
                PanelDockContainer p = item.Control as PanelDockContainer;
                uPatient ctrl = p.Controls[0] as uPatient;
                ctrl.PatientRow = _patientRow;
                ctrl.DisplayInfo();
                item.Visible = true;
                item.Selected = true;
            }

            docBar.Visible = true;
        }

        private void AddDockContainerItem(string fileNum, string fullName)
        {
            if (_isFirst)
            {
                dockContainerItem1.Name = fileNum;
                dockContainerItem1.Text = string.Format("{0} - {1}", fileNum, fullName);
                dockContainerItem1.Visible = true;

                if (panelDockContainer1.Controls.Count <= 0)
                {
                    uPatient ctrl = new uPatient();
                    ctrl.PatientRow = _patientRow;
                    panelDockContainer1.Controls.Add(ctrl);
                    ctrl.Dock = DockStyle.Fill;
                    ctrl.DisplayInfo();
                }
                else
                {
                    uPatient ctrl = panelDockContainer1.Controls[0] as uPatient;
                    ctrl.PatientRow = _patientRow;
                    ctrl.DisplayInfo();
                }
                
                _isFirst = false;
            }
            else
            {
                DockContainerItem item = new DockContainerItem(fileNum, string.Format("{0} - {1}", fileNum, fullName));
                PanelDockContainer p = new PanelDockContainer();
                p.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Windows7;
                p.Style.Alignment = System.Drawing.StringAlignment.Center;
                p.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
                p.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
                p.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
                p.Style.GradientAngle = 90;

                uPatient ctrl = new uPatient();
                ctrl.PatientRow = _patientRow;
                p.Controls.Add(ctrl);
                ctrl.Dock = DockStyle.Fill;
                ctrl.DisplayInfo();

                docBar.Controls.Add(p);
                item.Control = p;
                docBar.Items.Add(item);
                item.Selected = true;
            }
        }

        private DockContainerItem GetDockContainerItem(string fileNum)
        {
            foreach (DockContainerItem item in docBar.Items)
            {
                if (item.Name.Trim().ToLower() == fileNum.Trim().ToLower())
                    return item;
            }

            return null;
        }

        public void ClearData()
        {
            _isFirst = true;
            List<DockContainerItem> deletedDocks = new List<DockContainerItem>();

            foreach (DockContainerItem item in docBar.Items)
            {
                if (item.Tag != null && item.Tag.ToString() == "NotDelete")
                {
                    item.Name = "Name";
                    continue;
                }

                deletedDocks.Add(item);
            }

            foreach (DockContainerItem item in deletedDocks)
            {
                docBar.Items.Remove(item);
            }
        }
        #endregion

        #region Window Event Handlers

        #endregion
    }
}
