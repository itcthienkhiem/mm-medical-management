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
        private DataRow _drKetQuaSieuAm = null;
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

        public DataRow DrKetQuaSieuAm
        {
            get { return _drKetQuaSieuAm; }
            set { _drKetQuaSieuAm = value; }
        }
        #endregion

        #region UI Command
        public void DisplayInfo()
        {
            Cursor.Current = Cursors.WaitCursor;
            _textControl.Load(_reportTemplate, TXTextControl.StreamType.RichTextFormat);
            _textControl.Tables.GridLines = false;

            string label = "Số lưu trữ: ";
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
                _textControl.Selection.Text = label + _drKetQuaSieuAm["LamSang"].ToString();
            }

            if (_drKetQuaSieuAm["BacSiChiDinhGUID"] != null && _drKetQuaSieuAm["BacSiChiDinhGUID"] != DBNull.Value)
            {
                label = "BS. Chỉ định: ";
                index = _textControl.Find(label, 0, TXTextControl.FindOptions.NoMessageBox);
                if (index > -1)
                {
                    _textControl.Selection.Start = index;
                    _textControl.Selection.Text = label + _drKetQuaSieuAm["BacSiChiDinh"].ToString();
                }
            }

            label = "Result";
            index = _textControl.Find(label, 0, TXTextControl.FindOptions.NoMessageBox);
            if (index > -1)
            {
                _textControl.Selection.Start = index + 1;
                byte[] buff = (byte[])_drKetQuaSieuAm["KetQuaSieuAm"];
                _textControl.Selection.Load(buff, TXTextControl.BinaryStreamType.MSWord);
            }

            
            label = "Hình 1";
            index = _textControl.Find(label, 0, TXTextControl.FindOptions.NoMessageBox);
            TXTextControl.Image img = null;
            if (index > -1)
            {
                _textControl.Selection.Start = index;
                _textControl.Selection.Text = string.Empty;

                if (_drKetQuaSieuAm["Hinh1"] != null && _drKetQuaSieuAm["Hinh1"] != DBNull.Value)
                {
                    Image bmp = Utility.ParseImage((byte[])_drKetQuaSieuAm["Hinh1"]);
                    img = new TXTextControl.Image(bmp);
                    _textControl.Images.Add(img, TXTextControl.HorizontalAlignment.Center, index,
                        TXTextControl.ImageInsertionMode.DisplaceText);
                }
            }
            
            label = "Hình 2";
            index = _textControl.Find(label, 0, TXTextControl.FindOptions.NoMessageBox);
            if (index > -1)
            {
                _textControl.Selection.Start = index;
                _textControl.Selection.Text = string.Empty;

                if (_drKetQuaSieuAm["Hinh2"] != null && _drKetQuaSieuAm["Hinh2"] != DBNull.Value)
                {
                    Image bmp = Utility.ParseImage((byte[])_drKetQuaSieuAm["Hinh2"]);
                    img = new TXTextControl.Image(bmp);
                    _textControl.Images.Add(img, TXTextControl.HorizontalAlignment.Center, index,
                        TXTextControl.ImageInsertionMode.DisplaceText);
                }
            }

            label = "Ngày";
            index = _textControl.Find(label, 0, TXTextControl.FindOptions.NoMessageBox);
            if (index > -1)
            {
                DateTime ngaySieuAm = Convert.ToDateTime(_drKetQuaSieuAm["NgaySieuAm"]);
                _textControl.Selection.Start = index;
                _textControl.Selection.Text = string.Format("{0} {1} Tháng {2} Năm {3}", 
                    label, ngaySieuAm.Day, ngaySieuAm.Month, ngaySieuAm.Year);
            }

            label = "Ths.BS";
            index = _textControl.Find(label, 0, TXTextControl.FindOptions.NoMessageBox);
            if (index > -1)
            {
                _textControl.Selection.Start = index;
                _textControl.Selection.Text = _drKetQuaSieuAm["BacSiSieuAm"].ToString();
            }
        }
        #endregion

        #region Window Event Handlers
        private void tbPrint_Click(object sender, EventArgs e)
        {
            _textControl.Print("Kết quả siêu âm");
        }
        #endregion
    }
}
