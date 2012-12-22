using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Controls
{
    public partial class uPrintKetQuaSieuAm : uBase
    {
        #region Members
        private DataRow _patientRow = null;
        private string _reportTemplate = string.Format("{0}\\Templates\\SieuAmTemplate.rtf", Application.StartupPath);
        #endregion

        #region Constructor
        public uPrintKetQuaSieuAm()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public DataRow PatientRow
        {
            get { return _patientRow; }
            set { _patientRow = value; }
        }
        #endregion

        #region UI Command
        public void PrintPreview(DataRow drKetQuaSieuAm)
        {
            bool is2Page = Convert.ToBoolean(drKetQuaSieuAm["InTrang2"]);
            if (is2Page) _reportTemplate = string.Format("{0}\\Templates\\SieuAmTemplate2.rtf", Application.StartupPath);
            else _reportTemplate = string.Format("{0}\\Templates\\SieuAmTemplate.rtf", Application.StartupPath);

            Cursor.Current = Cursors.WaitCursor;
            _textControl.Load(_reportTemplate, TXTextControl.StreamType.RichTextFormat);
            _textControl.Tables.GridLines = false;

            string label = "Mã bệnh nhân: ";
            int index = _textControl.Find(label, 0, TXTextControl.FindOptions.NoMessageBox);
            if (index > -1)
            {
                _textControl.Selection.Start = index;
                _textControl.Selection.Text = label + _patientRow["FileNum"].ToString();
            }

            label = "Tuổi: ";
            index = _textControl.Find(label, 0, TXTextControl.FindOptions.NoMessageBox);
            if (index > -1)
            {
                _textControl.Selection.Start = index;
                _textControl.Selection.Text = label + _patientRow["DobStr"].ToString();
            }

            label = "Họ - Tên: ";
            index = _textControl.Find(label, 0, TXTextControl.FindOptions.NoMessageBox);
            if (index > -1)
            {
                _textControl.Selection.Start = index;
                _textControl.Selection.Text = label + _patientRow["FullName"].ToString();
            }

            label = "Giới tính: ";
            index = _textControl.Find(label, 0, TXTextControl.FindOptions.NoMessageBox);
            if (index > -1)
            {
                _textControl.Selection.Start = index;
                _textControl.Selection.Text = label + _patientRow["GenderAsStr"].ToString();
            }

            label = "Địa chỉ: ";
            index = _textControl.Find(label, 0, TXTextControl.FindOptions.NoMessageBox);
            if (index > -1)
            {
                _textControl.Selection.Start = index;
                _textControl.Selection.Text = label + _patientRow["Address"].ToString();
            }

            label = "Lâm sàng: ";
            index = _textControl.Find(label, 0, TXTextControl.FindOptions.NoMessageBox);
            if (index > -1)
            {
                _textControl.Selection.Start = index;
                _textControl.Selection.Text = label + drKetQuaSieuAm["LamSang"].ToString();
            }

            if (drKetQuaSieuAm["BacSiChiDinhGUID"] != null && drKetQuaSieuAm["BacSiChiDinhGUID"] != DBNull.Value)
            {
                label = "BS. Chỉ định: ";
                index = _textControl.Find(label, 0, TXTextControl.FindOptions.NoMessageBox);
                if (index > -1)
                {
                    _textControl.Selection.Start = index;
                    _textControl.Selection.Text = label + drKetQuaSieuAm["BacSiChiDinh"].ToString();
                }
            }

            if (!is2Page)
            {
                label = "Result";
                index = _textControl.Find(label, 0, TXTextControl.FindOptions.NoMessageBox);
                if (index > -1)
                {
                    _textControl.Selection.Start = index;
                    byte[] buff = (byte[])drKetQuaSieuAm["KetQuaSieuAm"];
                    _textControl.Selection.Load(buff, TXTextControl.BinaryStreamType.MSWord);
                }
            }
            else
            {
                _textControl.Selection.Start = 9999;
                byte[] buff = (byte[])drKetQuaSieuAm["KetQuaSieuAm"];
                _textControl.Selection.Load(buff, TXTextControl.BinaryStreamType.MSWord);
            }
            
            label = "Hình 1";
            index = _textControl.Find(label, 0, TXTextControl.FindOptions.NoMessageBox);
            TXTextControl.Image img = null;
            if (index > -1)
            {
                _textControl.Selection.Start = index;
                _textControl.Selection.Text = string.Empty;

                Image bmp = null;
                if (drKetQuaSieuAm["Hinh1"] != null && drKetQuaSieuAm["Hinh1"] != DBNull.Value)
                    bmp = Utility.ParseImage((byte[])drKetQuaSieuAm["Hinh1"], 300, 200);
                else
                    bmp = Properties.Resources.WhiteImage;

                img = new TXTextControl.Image(bmp);
                _textControl.Images.Add(img, TXTextControl.HorizontalAlignment.Center, index,
                    TXTextControl.ImageInsertionMode.MoveWithText);
            }
            
            label = "Hình 2";
            index = _textControl.Find(label, 0, TXTextControl.FindOptions.NoMessageBox);
            if (index > -1)
            {
                _textControl.Selection.Start = index;
                _textControl.Selection.Text = string.Empty;

                Image bmp = null;
                if (drKetQuaSieuAm["Hinh2"] != null && drKetQuaSieuAm["Hinh2"] != DBNull.Value)
                    bmp = Utility.ParseImage((byte[])drKetQuaSieuAm["Hinh2"], 300, 200);
                else
                    bmp = Properties.Resources.WhiteImage;

                img = new TXTextControl.Image(bmp);
                _textControl.Images.Add(img, TXTextControl.HorizontalAlignment.Center, index,
                    TXTextControl.ImageInsertionMode.MoveWithText);
            }

            label = "Ngày";
            index = _textControl.Find(label, 0, TXTextControl.FindOptions.NoMessageBox);
            if (index > -1)
            {
                DateTime ngaySieuAm = Convert.ToDateTime(drKetQuaSieuAm["NgaySieuAm"]);
                _textControl.Selection.Start = index;
                _textControl.Selection.Text = string.Format("{0} {1} Tháng {2} Năm {3}", 
                    label, ngaySieuAm.Day, ngaySieuAm.Month, ngaySieuAm.Year);
            }

            label = "Ths.BS";
            index = _textControl.Find(label, 0, TXTextControl.FindOptions.NoMessageBox);
            if (index > -1)
            {
                _textControl.Selection.Start = index;
                _textControl.Selection.Text = drKetQuaSieuAm["BacSiSieuAm"].ToString();
            }
        }

        public void Print(List<DataRow> ketQuaSieuAmList)
        {
            _textControl.ResetContents();
            List<string> dataList = new List<string>();
            foreach (DataRow row in ketQuaSieuAmList)
            {
                PrintPreview(row);
                string data = string.Empty;
                _textControl.Save(out data, TXTextControl.StringStreamType.RichTextFormat);
                dataList.Add(data);
            }

            int i = 0;
            foreach (string data in dataList)
            {
                if (i == 0)
                    _textControl.Load(data, TXTextControl.StringStreamType.RichTextFormat);
                else
                    _textControl.Append(data, TXTextControl.StringStreamType.RichTextFormat, TXTextControl.AppendSettings.StartWithNewSection);

                i++;
            }

            _textControl.Print("KetQuaSieuAm");
        }

        public void Export(DataRow drKetQuaSieuAm, string fileName)
        {
            _textControl.ResetContents();
            PrintPreview(drKetQuaSieuAm);
            _textControl.Save(fileName, TXTextControl.StreamType.RichTextFormat);
        }

        public void ExportToPDF(DataRow drKetQuaSieuAm, string fileName)
        {
            _textControl.ResetContents();
            PrintPreview(drKetQuaSieuAm);
            _textControl.Save(fileName, TXTextControl.StreamType.AdobePDF);
        }
        #endregion

        #region Window Event Handlers
        private void tbPrint_Click(object sender, EventArgs e)
        {
            _textControl.Print("KetQuaSieuAm");
        }
        #endregion
    }
}
