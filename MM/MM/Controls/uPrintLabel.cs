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
using System.Collections;
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

namespace MM.Controls
{
    public partial class uPrintLabel : uBase
    {
        #region Members
        private float _labelWidth = 36;
        private float _labelHeight = 19;
        private float _deltaWidth = 3;
        private float _deltaHeight = 1;
        private float _top = 0;
        private float _left = 8;
        private float _right = 5;
        private float _bottom = 5;
        private int _pageSize = 40;
        private int _pageCount = 0;
        private int _labelIndex = 0;
        private float _solution = 100;
        private List<LabelInfo> _labels = null;
        private float _labelWidthPxl = 0;
        private float _labelHeightPxl = 0;
        private float _deltaWidthPxl = 0;
        private float _deltaHeightPxl = 0;
        private float _topPxl = 0;
        private float _leftPxl = 0;
        private float _rightPxl = 0;
        private float bottomPxl = 0;
        private int _maxRow = 8;
        private int _maxCol = 5;
        private Font _font = new Font("Microsoft Sans Serif", 8);
        private Pen _pen = new Pen(Color.Black);
        private int _maxLenght = 20;
        private bool _flag = false;
        private bool _isAscending1 = true;
        private bool _isAscending2 = true;
        private string _name = string.Empty;
        private int _type = 0;
        #endregion

        #region Constructor
        public uPrintLabel()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Command
        public void ClearData()
        {
            DataTable dt = dgMembers.DataSource as DataTable;
            if (dt != null)
            {
                dt.Rows.Clear();
                dt.Clear();
                dt = null;
                dgMembers.DataSource = null;
            }
        }

        public void DisplayAsThread()
        {
            try
            {
                _name = txtSearchPatient.Text;
                if (chkMaBenhNhan.Checked) _type = 1;
                else if (chkTheoSoDienThoai.Checked) _type = 2;
                else _type = 0;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayPatientListProc));
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

        public override void SearchAsThread()
        {
            try
            {
                _name = txtSearchPatient.Text;
                if (chkMaBenhNhan.Checked) _type = 1;
                else if (chkTheoSoDienThoai.Checked) _type = 2;
                else _type = 0;
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSearchProc));
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }

        private void OnDisplayPatientList()
        {
            lock (ThisLock)
            {
                Result result = PatientBus.GetPatientList(_name, _type);

                if (result.IsOK)
                {
                    dgMembers.Invoke(new MethodInvoker(delegate()
                    {
                        ClearData();

                        DataTable dt = result.QueryResult as DataTable;
                        dgMembers.DataSource = GetDataSource(dt);
                    }));
                }
                else
                {
                    MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.GetPatientList"), IconType.Error);
                    Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientList"));
                }
            }
        }

        private DataTable GetDataSource(DataTable dt)
        {
            if (dgSelectedMember.RowCount <= 0) return dt;
            DataTable dt2 = dgSelectedMember.DataSource as DataTable;
            
            foreach (DataRow row in dt2.Rows)
            {
                DataRow[] rows = dt.Select(string.Format("PatientGUID='{0}'", row["PatientGUID"].ToString()));
                if (rows == null || rows.Length <= 0) continue;
                dt.Rows.Remove(rows[0]);
            }

            return dt;
        }

        private void InitData()
        {
            _flag = false;
            _printDocument = new System.Drawing.Printing.PrintDocument();
            _printDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(_printDocument_BeginPrint);
            _printDocument.EndPrint += new System.Drawing.Printing.PrintEventHandler(_printDocument_EndPrint);
            _printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(_printDocument_PrintPage);
            _printDialog.Document = _printDocument;
            _printPreviewDialog.Document = _printDocument;
            Global.PrintLabelConfig.Deserialize(Global.PrintLabelConfigPath);

            if (ra1x2.Checked)
            {
                _labelWidth = 120;
                _labelHeight = 80;
                _deltaWidth = 0;
                _deltaHeight = 10;
                _top = Global.PrintLabelConfig.Top_1x2;
                _left = Global.PrintLabelConfig.Left_1x2;
                _right = 45;
                _bottom = 12;
                _pageSize = 2;
                _maxRow = 2;
                _maxCol = 1;
                _font = new Font("Microsoft Sans Serif", 26);
                _maxLenght = 20;
            }
            else if (ra2x4.Checked)
            {
                _labelWidth = 95;
                _labelHeight = 37;
                _deltaWidth = 3;
                _deltaHeight = 3;
                _top = Global.PrintLabelConfig.Top_2x4;
                _left = Global.PrintLabelConfig.Left_2x4;
                _right = 8;
                _bottom = 0;
                _pageSize = 8;
                _maxRow = 4;
                _maxCol = 2;
                _font = new Font("Microsoft Sans Serif", 20);
                _maxLenght = 20;
            }
            else if (ra5x6.Checked)
            {
                _labelWidth = 37;
                _labelHeight = 25;
                _deltaWidth = 3;
                _deltaHeight = 1.5f;
                _top = Global.PrintLabelConfig.Top_5x6;
                _left = Global.PrintLabelConfig.Left_5x6;
                _right = 5;
                _bottom = 5;
                _pageSize = 30;
                _maxRow = 6;
                _maxCol = 5;
                _font = new Font("Microsoft Sans Serif", 8);
                _maxLenght = 20;
            }
            else if (ra5x8.Checked)
            {
                _labelWidth = 36;
                _labelHeight = 19;
                _deltaWidth = 3;
                _deltaHeight = 1;
                _top = Global.PrintLabelConfig.Top_5x8;
                _left = Global.PrintLabelConfig.Left_5x8;
                _right = 5;
                _bottom = 5;
                _pageSize = 40;
                _maxRow = 8;
                _maxCol = 5;
                _font = new Font("Microsoft Sans Serif", 8);
                _maxLenght = 20;
            }
            else if (ra5x11.Checked)
            {
                _labelWidth = 37;
                _labelHeight = 12.5f;
                _deltaWidth = 2;
                _deltaHeight = 2;
                _top = Global.PrintLabelConfig.Top_5x11;
                _left = Global.PrintLabelConfig.Left_5x11;
                _right = 5;
                _bottom = 5;
                _pageSize = 55;
                _maxRow = 11;
                _maxCol = 5;
                _font = new Font("Microsoft Sans Serif", 6);
                _maxLenght = 20;
            }

            _labelWidthPxl = (int)Math.Round(((_labelWidth * _solution) / 25.4));
            _labelHeightPxl = (int)Math.Round(((_labelHeight * _solution) / 25.4));
            _deltaWidthPxl = (int)Math.Round(((_deltaWidth * _solution) / 25.4));
            _deltaHeightPxl = (int)Math.Round(((_deltaHeight * _solution) / 25.4));
            _topPxl = (int)Math.Round(((_top * _solution) / 25.4));
            _leftPxl = (int)Math.Round(((_left * _solution) / 25.4));
            _rightPxl = (int)Math.Round(((_right * _solution) / 25.4));
            bottomPxl = (int)Math.Round(((_bottom * _solution) / 25.4));
        }

        private void OnPrint(bool isPreview)
        {
            if (dgSelectedMember.RowCount <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng chọn bệnh nhân để in nhãn.", IconType.Information);
                return;
            }

            InitData();

            int count = dgSelectedMember.RowCount * (int)numCount.Value;
            _pageCount = count / _pageSize;
            if (count % _pageSize != 0) _pageCount++;

            if (_labels == null) _labels = new List<LabelInfo>();
            else _labels.Clear();

            count = (int)numCount.Value;
            DataTable dt = dgSelectedMember.DataSource as DataTable;
            foreach (DataRow row in dt.Rows)
            {
                LabelInfo lbInfo = new LabelInfo();
                lbInfo.FullName = row["FullName"].ToString();
                if (row["GenderAsStr"].ToString() == "Nam")
                    lbInfo.GenderStr = "M";
                else if (row["GenderAsStr"].ToString() == "Nữ")
                    lbInfo.GenderStr = "F";
                else
                    lbInfo.GenderStr = "N";
                
                lbInfo.DobStr = row["DobStr"].ToString();
                lbInfo.FileNum = row["FileNum"].ToString();
                for (int i = 0; i < count; i++)
                {
                    _labels.Add(lbInfo);
                }
            }

            _labelIndex = 0;
            if (isPreview)
                _printPreviewDialog.ShowDialog();
            else
            {
                if (_printDialog.ShowDialog() == DialogResult.OK)
                    _printDocument.Print();
            }
        }

        private void ProcessFullName(string fullName, ref string fullName1, ref string fullName2)
        {
            int index = fullName.LastIndexOf(" ");
            fullName1 = string.Empty;
            fullName2 = string.Empty;
            if (index >= 0)
            {
                fullName1 = fullName.Substring(0, index);
                fullName2 = fullName.Substring(index + 1, fullName.Length - index - 1);
                if (index > _maxLenght - 1)
                {
                    string fullName3 = string.Empty;
                    string fullName4 = string.Empty;
                    index = fullName1.LastIndexOf(" ");
                    if (index >= 0)
                    {
                        fullName3 = fullName1.Substring(0, index);
                        fullName4 = fullName1.Substring(index + 1, fullName1.Length - index - 1);
                    }
                    else
                    {
                        fullName3 = fullName1.Substring(0, _maxLenght);
                        fullName4 = fullName1.Substring(_maxLenght, fullName1.Length - _maxLenght);
                    }

                    fullName1 = fullName3;
                    fullName2 = string.Format("{0} {1}", fullName4, fullName2);
                }
            }
            else
            {
                fullName1 = fullName.Substring(0, _maxLenght);
                fullName2 = fullName.Substring(_maxLenght, fullName.Length - _maxLenght);
            }
        }

        private void OnDrawLabel_1x2(Graphics g, float left, float top, LabelInfo labelInfo)
        {
            _maxLenght = 18;
            //g.DrawRectangle(_pen, left, top, _labelWidthPxl, _labelHeightPxl);
            int deltaLeft = 30;
            int deltaTop = 70;

            if (labelInfo.FullName.Length <= _maxLenght)
            {
                g.DrawString(labelInfo.FullName, _font, Brushes.Black, left + deltaLeft, top + deltaTop);
                g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + deltaLeft, top + deltaTop + 60);
                g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + deltaLeft, top + deltaTop + 120);
            }
            else
            {
                deltaTop -= 10;

                string fullName1 = string.Empty;
                string fullName2 = string.Empty;
                ProcessFullName(labelInfo.FullName, ref fullName1, ref fullName2);

                g.DrawString(fullName1, _font, Brushes.Black, left + deltaLeft, top + deltaTop);
                g.DrawString(fullName2, _font, Brushes.Black, left + deltaLeft, top + deltaTop + 50);
                g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + deltaLeft, top + deltaTop + 100);
                g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + deltaLeft, top + +deltaTop + 150);
            }
        }

        private void OnDrawLabel_2x4(Graphics g, float left, float top, LabelInfo labelInfo)
        {
            _maxLenght = 19;
            //g.DrawRectangle(_pen, left, top, _labelWidthPxl, _labelHeightPxl);

            int deltaLeft = 20;
            int deltaTop = 30;

            if (labelInfo.FullName.Length <= _maxLenght)
            {
                g.DrawString(labelInfo.FullName, _font, Brushes.Black, left + deltaLeft, top + deltaTop);
                g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + deltaLeft, top + deltaTop + 30);
                g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + deltaLeft, top + deltaTop + 60);
            }
            else
            {
                deltaTop -= 15;

                string fullName1 = string.Empty;
                string fullName2 = string.Empty;
                ProcessFullName(labelInfo.FullName, ref fullName1, ref fullName2);

                g.DrawString(fullName1, _font, Brushes.Black, left + deltaLeft, top + deltaTop);
                g.DrawString(fullName2, _font, Brushes.Black, left + deltaLeft, top + deltaTop + 30);
                g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + deltaLeft, top + deltaTop + 60);
                g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + deltaLeft, top + deltaTop + 90);
            }
        }

        private void OnDrawLabel_5x6(Graphics g, float left, float top, LabelInfo labelInfo)
        {
            _maxLenght = 17;
            //g.DrawRectangle(_pen, left, top, _labelWidthPxl, _labelHeightPxl);
            int deltaLeft = 8;
            int deltaTop = 26;

            if (labelInfo.FullName.Length <= _maxLenght)
            {
                g.DrawString(labelInfo.FullName, _font, Brushes.Black, left + deltaLeft, top + deltaTop);
                g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + deltaLeft, top + deltaTop + 16);
                g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + deltaLeft, top + deltaTop + 32);
            }
            else
            {
                deltaTop -= 6;

                string fullName1 = string.Empty;
                string fullName2 = string.Empty;
                ProcessFullName(labelInfo.FullName, ref fullName1, ref fullName2);

                g.DrawString(fullName1, _font, Brushes.Black, left + deltaLeft, top + deltaTop);
                g.DrawString(fullName2, _font, Brushes.Black, left + deltaLeft, top + deltaTop + 16);
                g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + deltaLeft, top + deltaTop + 32);
                g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + deltaLeft, top + deltaTop + 48);
            }
        }

        private void OnDrawLabel_5x8(Graphics g, float left, float top, LabelInfo labelInfo)
        {
            _maxLenght = 16;
            //g.DrawRectangle(_pen, left, top, _labelWidthPxl, _labelHeightPxl);
            int deltaLeft = 8;
            int deltaTop = 16;

            if (labelInfo.FullName.Length <= _maxLenght)
            {
                g.DrawString(labelInfo.FullName, _font, Brushes.Black, left + deltaLeft, top + deltaTop);
                g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + deltaLeft, top + deltaTop + 16);
                g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + deltaLeft, top + deltaTop + 32);
            }
            else
            {
                deltaTop -= 6;

                string fullName1 = string.Empty;
                string fullName2 = string.Empty;
                ProcessFullName(labelInfo.FullName, ref fullName1, ref fullName2);

                g.DrawString(fullName1, _font, Brushes.Black, left + deltaLeft, top + deltaTop);
                g.DrawString(fullName2, _font, Brushes.Black, left + deltaLeft, top + deltaTop + 16);
                g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + deltaLeft, top + deltaTop + 32);
                g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + deltaLeft, top + deltaTop + 48);
            }
        }

        private void OnDrawLabel_5x11(Graphics g, float left, float top, LabelInfo labelInfo)
        {
            //g.DrawRectangle(_pen, left, top, _labelWidthPxl, _labelHeightPxl);
            int deltaLeft = 8;
            int deltaTop = 7;

            if (labelInfo.FullName.Length <= _maxLenght)
            {
                g.DrawString(labelInfo.FullName, _font, Brushes.Black, left + deltaLeft, top + deltaTop);
                g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + deltaLeft, top + deltaTop + 10);
                g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + deltaLeft, top + deltaTop + 20);
            }
            else
            {
                deltaTop -= 3;
                string fullName1 = string.Empty;
                string fullName2 = string.Empty;
                ProcessFullName(labelInfo.FullName, ref fullName1, ref fullName2);

                g.DrawString(fullName1, _font, Brushes.Black, left + deltaLeft, top + deltaTop);
                g.DrawString(fullName2, _font, Brushes.Black, left + deltaLeft, top + deltaTop + 10);
                g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + deltaLeft, top + deltaTop + 20);
                g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + deltaLeft, top + deltaTop + 30);
            }
        }

        private void OnMoveRight()
        {
            if (dgMembers.RowCount <= 0) return;
            if (dgMembers.SelectedRows == null || dgMembers.SelectedRows.Count <= 0) return;

            DataTable dt = dgMembers.DataSource as DataTable;
            if (dt == null) return;
            DataTable dt2 = dgSelectedMember.DataSource as DataTable;
            
            if (dt2 == null)
            {
                dt2 = dt.Clone();
                dgSelectedMember.DataSource = dt2;
            }
            
            foreach  (DataGridViewRow row in dgMembers.SelectedRows)
            {
                DataRow drMember = (row.DataBoundItem as DataRowView).Row;
                dt2.ImportRow(drMember);

                DataRow[] rows = dt.Select(string.Format("PatientGUID='{0}'", drMember["PatientGUID"]));
                if (rows != null && rows.Length > 0)
                    dt.Rows.Remove(rows[0]);
            }
        }

        private void OnMoveAllRight()
        {
            if (dgMembers.RowCount <= 0) return;
            DataTable dt = dgMembers.DataSource as DataTable;
            if (dt == null) return;
            dgSelectedMember.DataSource = dt.Copy();
            
            dt.Rows.Clear();
        }

        private void OnMoveLeft()
        {
            if (dgSelectedMember.RowCount <= 0) return;
            if (dgSelectedMember.SelectedRows == null || dgSelectedMember.SelectedRows.Count <= 0) return;

            DataTable dt2 = dgSelectedMember.DataSource as DataTable;

            foreach (DataGridViewRow row in dgSelectedMember.SelectedRows)
            {
                DataRow drMember = (row.DataBoundItem as DataRowView).Row;
                dt2.Rows.Remove(drMember);
            }

            SearchAsThread();
        }

        private void OnMoveAllLeft()
        {
            if (dgSelectedMember.RowCount <= 0) return;
            DataTable dt2 = dgSelectedMember.DataSource as DataTable;
            dt2.Rows.Clear();
            SearchAsThread();
        }
        #endregion

        #region Window Event Handlers
        private void _printDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            _printDocument.DefaultPageSettings.Margins.Left = 0;
            _printDocument.DefaultPageSettings.Margins.Top = 0;
            _printDocument.DefaultPageSettings.Margins.Right = 0;
            _printDocument.DefaultPageSettings.Margins.Bottom = 0;

            _printDocument.PrinterSettings.DefaultPageSettings.Margins.Left = 0;
            _printDocument.PrinterSettings.DefaultPageSettings.Margins.Top = 0;
            _printDocument.PrinterSettings.DefaultPageSettings.Margins.Right = 0;
            _printDocument.PrinterSettings.DefaultPageSettings.Margins.Bottom = 0;

            if (_flag)
            {
                if (_printDialog.ShowDialog() != DialogResult.OK)
                    e.Cancel = true;
            }
            else _flag = true;
        }

        private void _printDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void _printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            float left = _leftPxl;
            float top = _topPxl;

            for (int i = 0; i < _maxCol; i++)
            {
                top = _topPxl;
                for (int j = 0; j < _maxRow; j++)
                {
                    if (_labelIndex >= _labels.Count)
                    {
                        e.HasMorePages = false;
                        _labelIndex = 0;
                        return;
                    }

                    LabelInfo labelInfo = _labels[_labelIndex];

                    if (ra1x2.Checked)
                        OnDrawLabel_1x2(e.Graphics, left, top, labelInfo);
                    else if (ra2x4.Checked)
                        OnDrawLabel_2x4(e.Graphics, left, top, labelInfo);
                    else if (ra5x6.Checked)
                        OnDrawLabel_5x6(e.Graphics, left, top, labelInfo);
                    else if (ra5x8.Checked)
                        OnDrawLabel_5x8(e.Graphics, left, top, labelInfo);
                    else if (ra5x11.Checked)
                        OnDrawLabel_5x11(e.Graphics, left, top, labelInfo);

                    top += _deltaHeightPxl + _labelHeightPxl;
                    _labelIndex++;
                }

                left += _deltaWidthPxl + _labelWidthPxl;
            }

            if (_labelIndex >= _labels.Count)
            {
                e.HasMorePages = false;
                _labelIndex = 0;
                return;
            }

            e.HasMorePages = true;
        }

        private void txtSearchPatient_TextChanged(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            OnMoveRight();
        }

        private void btnAllRight_Click(object sender, EventArgs e)
        {
            OnMoveAllRight();
        }

        private void btnAllLeft_Click(object sender, EventArgs e)
        {
            OnMoveAllLeft();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            OnMoveLeft();
        }

        private void dgMembers_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                _isAscending1 = !_isAscending1;

                DataTable dt = dgMembers.DataSource as DataTable;
                DataTable newDataSource = null;

                if (_isAscending1)
                {
                    newDataSource = (from p in dt.AsEnumerable()
                                     orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                                     select p).CopyToDataTable();
                }
                else
                {
                    newDataSource = (from p in dt.AsEnumerable()
                                     orderby p.Field<string>("FirstName") descending, p.Field<string>("FullName") descending
                                     select p).CopyToDataTable();
                }

                dgMembers.DataSource = newDataSource;

                if (dt != null)
                {
                    dt.Rows.Clear();
                    dt.Clear();
                    dt = null;
                }
            }
            else
                _isAscending1 = false;
        }

        private void dgSelectedMember_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                _isAscending2 = !_isAscending2;

                DataTable dt = dgSelectedMember.DataSource as DataTable;
                DataTable newDataSource = null;

                if (_isAscending2)
                {
                    newDataSource = (from p in dt.AsEnumerable()
                                     orderby p.Field<string>("FirstName"), p.Field<string>("FullName")
                                     select p).CopyToDataTable();
                }
                else
                {
                    newDataSource = (from p in dt.AsEnumerable()
                                     orderby p.Field<string>("FirstName") descending, p.Field<string>("FullName") descending
                                     select p).CopyToDataTable();
                }

                dgSelectedMember.DataSource = newDataSource;

                if (dt != null)
                {
                    dt.Rows.Clear();
                    dt.Clear();
                    dt = null;
                }
            }
            else
                _isAscending2 = false;
        }

        private void chkMaBenhNhan_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void chkTheoSoDienThoai_CheckedChanged(object sender, EventArgs e)
        {
            SearchAsThread();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }
        #endregion

        #region Working Thread
        private void OnDisplayPatientListProc(object state)
        {
            try
            {
                OnDisplayPatientList();
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

        private void OnSearchProc(object state)
        {
            try
            {
                OnDisplayPatientList();
            }
            catch (Exception e)
            {
                MM.MsgBox.Show(Application.ProductName, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }
        #endregion

       
    }

    public class LabelInfo
    {
        public string FileNum = string.Empty;
        public string FullName = string.Empty;
        public string DobStr = string.Empty;
        public string GenderStr = string.Empty;

        public LabelInfo()
        {

        }
    }
}
