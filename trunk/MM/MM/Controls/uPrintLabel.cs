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
        private int _width = 202;
        private int _height = 168;
        private int _labelWidth = 36;
        private int _labelHeight = 19;
        private int _deltaWidth = 3;
        private int _deltaHeight = 1;
        private int _top = 2;
        private int _left = 2;
        private int _right = 5;
        private int _bottom = 5;
        private int _pageSize = 40;
        private int _pageCount = 0;
        private int _labelIndex = 0;
        private int _solution = 100;
        private List<LabelInfo> _labels = null;
        private int _widthPxl = 0;
        private int _heightPxl = 0;
        private int _labelWidthPxl = 0;
        private int _labelHeightPxl = 0;
        private int _deltaWidthPxl = 0;
        private int _deltaHeightPxl = 0;
        private int _topPxl = 0;
        private int _leftPxl = 0;
        private int _rightPxl = 0;
        private int bottomPxl = 0;
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

            _printDocument.DefaultPageSettings.Margins.Left = 0;
            _printDocument.DefaultPageSettings.Margins.Top = 0;
            _printDocument.DefaultPageSettings.Margins.Right = 0;
            _printDocument.DefaultPageSettings.Margins.Bottom = 0;

            _printDocument.PrinterSettings.DefaultPageSettings.Margins.Left = 0;
            _printDocument.PrinterSettings.DefaultPageSettings.Margins.Top = 0;
            _printDocument.PrinterSettings.DefaultPageSettings.Margins.Right = 0;
            _printDocument.PrinterSettings.DefaultPageSettings.Margins.Bottom = 0;

            if (ra5x8.Checked)
            {
                _width = 202;
                _height = 168;
                _labelWidth = 36;
                _labelHeight = 19;
                _deltaWidth = 3;
                _deltaHeight = 1;
                _top = 2;
                _left = 2;
                _right = 5;
                _bottom = 5;
                _pageSize = 40;
                _maxRow = 8;
                _maxCol = 5;
                _font = new Font("Microsoft Sans Serif", 8);
                _maxLenght = 20;
            }

            _widthPxl = (int)Math.Round(((_width * _solution) / 25.4));
            _heightPxl = (int)Math.Round(((_height * _solution) / 25.4));
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

        private void OnDrawLabel_5x8(Graphics g, int left, int top, LabelInfo labelInfo)
        {
            g.DrawRectangle(_pen, left, top, _labelWidthPxl, _labelHeightPxl);

            if (labelInfo.FullName.Length <= _maxLenght)
            {
                g.DrawString(labelInfo.FullName, _font, Brushes.Black, left + 8, top + 12);
                g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + 8, top + 28);
                g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + 8, top + 44);
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

                g.DrawString(fullName1, _font, Brushes.Black, left + 8, top + 8);
                g.DrawString(fullName2, _font, Brushes.Black, left + 8, top + 22);
                g.DrawString(string.Format("{0} {1}", labelInfo.GenderStr, labelInfo.DobStr), _font, Brushes.Black, left + 8, top + 38);
                g.DrawString(labelInfo.FileNum, _font, Brushes.Black, left + 8, top + 54);
            }
        }
        #endregion

        #region Window Event Handlers
        private void _printDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
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
            //e.Graphics.DrawRectangle(_pen, 0, 0, _widthPxl, _heightPxl);
            int left = _leftPxl;
            int top = _topPxl;

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
                    if (ra5x8.Checked)
                        OnDrawLabel_5x8(e.Graphics, left, top, labelInfo);

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
