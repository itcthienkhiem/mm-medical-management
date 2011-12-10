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
        private DataTable _dataSource = null;
        //private int _width = 202;
        //private int _height = 168;
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
        //private int _widthPxl = 0;
        //private int _heightPxl = 0;
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
        #endregion

        #region Constructor
        public uPrintLabel()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public List<DataRow> Members
        {
            get
            {
                if (_dataSource == null) return null;
                List<DataRow> checkedRows = new List<DataRow>();
                foreach (DataRow row in _dataSource.Rows)
                {
                    if (Boolean.Parse(row["Checked"].ToString()))
                    {
                        checkedRows.Add(row);
                    }
                }

                return checkedRows;
            }

        }
        #endregion

        #region UI Command
        public void ClearData()
        {
            dgMembers.DataSource = null;
        }

        public void DisplayAsThread()
        {
            try
            {
                chkChecked.Checked = false;
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

        private void OnDisplayPatientList()
        {
            Result result = PatientBus.GetPatientList();

            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    _dataSource = result.QueryResult as DataTable;
                    dgMembers.DataSource = _dataSource;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("PatientBus.GetPatientList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("PatientBus.GetPatientList"));
            }
        }

        private void OnSearchPatient()
        {
            UpdateChecked();

            chkChecked.Checked = false;
            if (txtSearchPatient.Text.Trim() == string.Empty)
            {
                dgMembers.DataSource = _dataSource;
                return;
            }

            string str = txtSearchPatient.Text.ToLower();

            //FullName
            List<DataRow> results = (from p in _dataSource.AsEnumerable()
                          where p.Field<string>("FullName") != null &&
                          p.Field<string>("FullName").Trim() != string.Empty &&
                          (p.Field<string>("FullName").ToLower().IndexOf(str) >= 0 ||
                          str.IndexOf(p.Field<string>("FullName").ToLower()) >= 0) 
                          select p).ToList<DataRow>();

            DataTable newDataSource = _dataSource.Clone();
            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgMembers.DataSource = newDataSource;
                return;
            }


            //FileNum
            results = (from p in _dataSource.AsEnumerable()
                      where p.Field<string>("FileNum") != null &&
                          p.Field<string>("FileNum").Trim() != string.Empty &&
                          (p.Field<string>("FileNum").ToLower().IndexOf(str) >= 0 ||
                      str.IndexOf(p.Field<string>("FileNum").ToLower()) >= 0)
                      select p).ToList<DataRow>();

            foreach (DataRow row in results)
                newDataSource.Rows.Add(row.ItemArray);

            if (newDataSource.Rows.Count > 0)
            {
                dgMembers.DataSource = newDataSource;
                return;
            }

            dgMembers.DataSource = newDataSource;
        }

        private void UpdateChecked()
        {
            DataTable dt = dgMembers.DataSource as DataTable;
            if (dt == null) return;

            foreach (DataRow row1 in dt.Rows)
            {
                string patientGUID1 = row1["PatientGUID"].ToString();
                bool isChecked1 = Convert.ToBoolean(row1["Checked"]);
                foreach (DataRow row2 in _dataSource.Rows)
                {
                    string patientGUID2 = row2["PatientGUID"].ToString();
                    bool isChecked2 = Convert.ToBoolean(row2["Checked"]);

                    if (patientGUID1 == patientGUID2)
                    {
                        row2["Checked"] = row1["Checked"];
                        break;
                    }
                }
            }
        }

        private void InitData()
        {
            _flag = false;
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
                _maxLenght = 100;
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
            UpdateChecked();
            List<DataRow> members = this.Members;
            if (members == null || members.Count <= 0)
            {
                MsgBox.Show(Application.ProductName, "Vui lòng dánh dấu ít nhất 1 bệnh nhân.", IconType.Information);
                return;
            }

            InitData();

            int count = members.Count * (int)numCount.Value;
            _pageCount = count / _pageSize;
            if (count % _pageSize != 0) _pageCount++;

            if (_labels == null) _labels = new List<LabelInfo>();
            else _labels.Clear();

            count = (int)numCount.Value;
            foreach (DataRow row in members)
            {
                LabelInfo lbInfo = new LabelInfo();
                lbInfo.FullName = row["FullName"].ToString();
                lbInfo.GenderStr = row["GenderAsStr"].ToString() == "Nam" ? "M" : "F";
                lbInfo.DobStr = row["DobStr"].ToString();
                lbInfo.FileNum = row["FileNum"].ToString();
                for (int i = 0; i < count; i++)
                {
                    _labels.Add(lbInfo);
                }
            }

            _labelIndex = 0;
            if (isPreview)
            {
                _printPreviewDialog.ShowDialog();
            }
            else
            {
                if (_printDialog.ShowDialog() == DialogResult.OK)
                    _printDocument.Print();
            }
        }

        private void OnDrawLabel_1x2(Graphics g, float left, float top, LabelInfo labelInfo)
        {
            //g.DrawRectangle(_pen, left, top, _labelWidthPxl, _labelHeightPxl);
            int deltaLeft = 30;

            if (labelInfo.FullName.Length <= _maxLenght)
            {
                g.DrawString(labelInfo.FullName, _font, Brushes.Black, left + deltaLeft, top + 70);
                g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + deltaLeft, top + 130);
                g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + deltaLeft, top + 190);
            }
            else
            {
                int index = labelInfo.FullName.LastIndexOf(" ");
                string fullName1 = string.Empty;
                string fullName2 = string.Empty;
                if (index >= 0)
                {
                    fullName1 = labelInfo.FullName.Substring(0, index);
                    fullName2 = labelInfo.FullName.Substring(index + 1, labelInfo.FullName.Length - index - 1);
                }
                else
                {
                    fullName1 = labelInfo.FullName.Substring(0, _maxLenght);
                    fullName2 = labelInfo.FullName.Substring(_maxLenght, labelInfo.FullName.Length - _maxLenght);
                }

                g.DrawString(fullName1, _font, Brushes.Black, left + deltaLeft, top + 60);
                g.DrawString(fullName2, _font, Brushes.Black, left + deltaLeft, top + 110);
                g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + deltaLeft, top + 160);
                g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + deltaLeft, top + 210);
            }
        }

        private void OnDrawLabel_2x4(Graphics g, float left, float top, LabelInfo labelInfo)
        {
            //g.DrawRectangle(_pen, left, top, _labelWidthPxl, _labelHeightPxl);

            int deltaLeft = 20;

            if (labelInfo.FullName.Length <= _maxLenght)
            {
                g.DrawString(labelInfo.FullName, _font, Brushes.Black, left + deltaLeft, top + 30);
                g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + deltaLeft, top + 60);
                g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + deltaLeft, top + 90);
            }
            else
            {
                int index = labelInfo.FullName.LastIndexOf(" ");
                string fullName1 = string.Empty;
                string fullName2 = string.Empty;
                if (index >= 0)
                {
                    fullName1 = labelInfo.FullName.Substring(0, index);
                    fullName2 = labelInfo.FullName.Substring(index + 1, labelInfo.FullName.Length - index - 1);
                }
                else
                {
                    fullName1 = labelInfo.FullName.Substring(0, _maxLenght);
                    fullName2 = labelInfo.FullName.Substring(_maxLenght, labelInfo.FullName.Length - _maxLenght);
                }

                g.DrawString(fullName1, _font, Brushes.Black, left + deltaLeft, top + 15);
                g.DrawString(fullName2, _font, Brushes.Black, left + deltaLeft, top + 45);
                g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + deltaLeft, top + 75);
                g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + deltaLeft, top + 105);
            }
        }

        private void OnDrawLabel_5x6(Graphics g, float left, float top, LabelInfo labelInfo)
        {
            //g.DrawRectangle(_pen, left, top, _labelWidthPxl, _labelHeightPxl);

            if (labelInfo.FullName.Length <= _maxLenght)
            {
                g.DrawString(labelInfo.FullName, _font, Brushes.Black, left + 8, top + 26);
                g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + 8, top + 42);
                g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + 8, top + 58);
            }
            else
            {
                int index = labelInfo.FullName.LastIndexOf(" ");
                string fullName1 = string.Empty;
                string fullName2 = string.Empty;
                if (index >= 0)
                {
                    fullName1 = labelInfo.FullName.Substring(0, index);
                    fullName2 = labelInfo.FullName.Substring(index + 1, labelInfo.FullName.Length - index - 1);
                }
                else
                {
                    fullName1 = labelInfo.FullName.Substring(0, _maxLenght);
                    fullName2 = labelInfo.FullName.Substring(_maxLenght, labelInfo.FullName.Length - _maxLenght);
                }

                g.DrawString(fullName1, _font, Brushes.Black, left + 8, top + 20);
                g.DrawString(fullName2, _font, Brushes.Black, left + 8, top + 34);
                g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + 8, top + 50);
                g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + 8, top + 66);
            }
        }

        private void OnDrawLabel_5x8(Graphics g, float left, float top, LabelInfo labelInfo)
        {
            //g.DrawRectangle(_pen, left, top, _labelWidthPxl, _labelHeightPxl);

            if (labelInfo.FullName.Length <= _maxLenght)
            {
                g.DrawString(labelInfo.FullName, _font, Brushes.Black, left + 8, top + 16);
                g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + 8, top + 32);
                g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + 8, top + 48);
            }
            else
            {
                int index = labelInfo.FullName.LastIndexOf(" ");
                string fullName1 = string.Empty;
                string fullName2 = string.Empty;
                if (index >= 0)
                {
                    fullName1 = labelInfo.FullName.Substring(0, index);
                    fullName2 = labelInfo.FullName.Substring(index + 1, labelInfo.FullName.Length - index - 1);
                }
                else
                {
                    fullName1 = labelInfo.FullName.Substring(0, _maxLenght);
                    fullName2 = labelInfo.FullName.Substring(_maxLenght, labelInfo.FullName.Length - _maxLenght);
                }

                g.DrawString(fullName1, _font, Brushes.Black, left + 8, top + 10);
                g.DrawString(fullName2, _font, Brushes.Black, left + 8, top + 24);
                g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + 8, top + 40);
                g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + 8, top + 56);
            }
        }

        private void OnDrawLabel_5x11(Graphics g, float left, float top, LabelInfo labelInfo)
        {
            //g.DrawRectangle(_pen, left, top, _labelWidthPxl, _labelHeightPxl);
            g.DrawString(labelInfo.FullName, _font, Brushes.Black, left + 8, top + 10);
            g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + 8, top + 20);
            g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + 8, top + 30);
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
            OnSearchPatient();
        }

        private void chkChecked_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dt = dgMembers.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                row["Checked"] = chkChecked.Checked;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            OnPrint(false);
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            OnPrint(true);
        }
        #endregion

        #region Working Thread
        private void OnDisplayPatientListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
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
