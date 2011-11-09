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
                item.Visible = true;
                item.Selected = true;
                //((((item.Control as PanelDockContainer).Controls[0] as DevComponents.DotNetBar.TabControl).Controls[0] as TabControlPanel).Controls[0] as uServiceHistory).PatientRow = _patientRow;
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
                    DevComponents.DotNetBar.TabControl tab = NewTabControl();
                    panelDockContainer1.Controls.Add(tab);
                }
                else
                {
                    DevComponents.DotNetBar.TabControl tab = panelDockContainer1.Controls[0] as DevComponents.DotNetBar.TabControl;
                    uServiceHistory uServiceHistory = (tab.Controls[0] as TabControlPanel).Controls[0] as uServiceHistory;
                    uServiceHistory.PatientRow = _patientRow;
                    uServiceHistory.DisplayAsThread();
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
                DevComponents.DotNetBar.TabControl tab = NewTabControl();
                p.Controls.Add(tab);

                docBar.Controls.Add(p);
                item.Control = p;
                docBar.Items.Add(item);
                item.Selected = true;
            }
        }

        private DevComponents.DotNetBar.TabControl NewTabControl()
        {
            DevComponents.DotNetBar.TabControl tab = new DevComponents.DotNetBar.TabControl();
            TabControlPanel p = new TabControlPanel();
            tab.Controls.Add(p);
            tab.Style = eTabStripStyle.VS2005;
            tab.Dock = DockStyle.Fill;

            //Service
            TabItem item = tab.CreateTab("Dịch vụ đã sử dụng");
            item.AttachedControl = p;
            p.Dock = System.Windows.Forms.DockStyle.Fill;

            uServiceHistory uServiceHistory = new uServiceHistory();
            uServiceHistory.PatientRow = _patientRow;
            p.Controls.Add(uServiceHistory);
            uServiceHistory.Dock = DockStyle.Fill;
            uServiceHistory.DisplayAsThread();
            return tab;
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
