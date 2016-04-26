/* Copyright (c) 2016, Cocosoft Inc.
 All rights reserved.
 http://www.Cocosofttech.com

 This file is part of the LIS open source project.

 The LIS  open source project is free software: you can
 redistribute it and/or modify it under the terms of the GNU General Public
 License as published by the Free Software Foundation, either version 3 of the
 License, or (at your option) any later version.

 The ClearCanvas LIS open source project is distributed in the hope that it
 will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
 Public License for more details.

 You should have received a copy of the GNU General Public License along with
 the LIS open source project.  If not, see
 <http://www.gnu.org/licenses/>.
*/
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
        private DataRow _patientRow = null;
        private bool _isFirst = true;
        #endregion

        #region Constructor
        public uPatientHistory()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        public void Display(DataRow patientRow)
        {
            if (patientRow == null) return;
            string patientGUID = patientRow["PatientGUID"].ToString();

            DataRow[] rows = null;
            bool isAddNew = true;
            //Update patient row to all doctab
            foreach (DockContainerItem item in docBar.Items)
            {
                if (item.Control.Tag == null) continue;
                string id = item.Control.Tag.ToString();
                rows = Global.dtOpenPatient.Select(string.Format("PatientGUID='{0}'", id));

                if (id != patientGUID)
                {
                    if (rows != null && rows.Length > 0)
                    {
                        PanelDockContainer p = item.Control as PanelDockContainer;
                        uPatient ctrl = p.Controls[0] as uPatient;
                        ctrl.PatientRow = rows[0];
                    }
                    else
                        item.Visible = false;
                }
                else
                {
                    item.Text = string.Format("{0} - {1}", rows[0]["FileNum"], rows[0]["FullName"]);
                    PanelDockContainer p = item.Control as PanelDockContainer;
                    uPatient ctrl = p.Controls[0] as uPatient;
                    p.Tag = patientGUID;
                    ctrl.PatientRow = rows[0];
                    ctrl.DisplayInfo();
                    item.Visible = true;
                    item.Selected = true;
                    isAddNew = false;
                }
            }

            if (isAddNew)
            {
                _patientRow = patientRow;
                string fileNum = _patientRow["FileNum"] as string;
                string fullName = _patientRow["Fullname"] as string;
                AddDockContainerItem(fileNum, fullName, patientGUID);
            }

            docBar.Visible = true;
        }

        private void AddDockContainerItem(string fileNum, string fullName, string patientGUID)
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
                    panelDockContainer1.Tag = patientGUID;
                    panelDockContainer1.Controls.Add(ctrl);
                    ctrl.Dock = DockStyle.Fill;
                    ctrl.DisplayInfo();
                }
                else
                {
                    panelDockContainer1.Tag = patientGUID;
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
                p.Tag = patientGUID;

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

        private DockContainerItem GetDockContainerItem(string patientGUID)
        {
            foreach (DockContainerItem item in docBar.Items)
            {
                if (item.Control.Tag != null && item.Control.Tag.ToString() == patientGUID)
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
