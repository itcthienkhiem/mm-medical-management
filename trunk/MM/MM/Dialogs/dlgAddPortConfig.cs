using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using MM.Common;

namespace MM.Dialogs
{
    public partial class dlgAddPortConfig : Form
    {
        #region Members
        private bool _isNew = true;
        private PortConfig _portConfig = null;
        #endregion

        #region Constructor
        public dlgAddPortConfig()
        {
            InitializeComponent();
            InitData();
        }

        public dlgAddPortConfig(PortConfig portConfig)
        {
            InitializeComponent();
            _isNew = false;
            _portConfig = portConfig;
            InitData();
            this.Text = "Sua cau hinh ket noi";
            txtTenMayXetNghiem.Text = _portConfig.TenMayXetNghiem;
            cboLoaiMay.SelectedIndex = (int)_portConfig.LoaiMay;
            cboCOM.Text = portConfig.PortName;
        }
        #endregion

        #region Properties
        public PortConfig PortConfig
        {
            get { return _portConfig; }
        }
        #endregion

        #region UI Command
        private void InitData()
        {
            string id = _isNew ? string.Empty : _portConfig.Id;
            foreach (string portName in SerialPort.GetPortNames())
            {
                if (!Global.PortConfigCollection.CheckPortNameTonTai(portName, id))
                    cboCOM.Items.Add(portName);
            }

            string[] names = Enum.GetNames(typeof(LoaiMayXN));
            foreach (string name in names)
            {
                cboLoaiMay.Items.Add(name);
            }
        }

        private bool CheckInfo()
        {
            if (txtTenMayXetNghiem.Text.Trim() == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập tên máy xét nghiệm.", IconType.Information);
                txtTenMayXetNghiem.Focus();
                return false;
            }

            if (cboLoaiMay.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập loại máy xét nghiệm.", IconType.Information);
                cboCOM.Focus();
                return false;
            }

            if (cboCOM.Text == string.Empty)
            {
                MsgBox.Show(this.Text, "Vui lòng nhập cổng kết nối.", IconType.Information);
                cboCOM.Focus();
                return false;
            }

            string id = _isNew ? string.Empty : _portConfig.Id;
            if (Global.PortConfigCollection.CheckTenMayXetNghiemTonTai(txtTenMayXetNghiem.Text, id))
            {
                MsgBox.Show(this.Text, string.Format("Tên máy xét nghiệm: '{0}' đã tồn tại. Vui lòng nhập tên khác.", txtTenMayXetNghiem.Text), 
                    IconType.Information);
                txtTenMayXetNghiem.Focus();
                return false;
            }

            return true;
        }

        private void SetData()
        {
            if (_isNew) 
            {
                _portConfig = new PortConfig();
                _portConfig.Id = Guid.NewGuid().ToString();
            }

            _portConfig.TenMayXetNghiem = txtTenMayXetNghiem.Text;
            _portConfig.LoaiMay = (LoaiMayXN)cboLoaiMay.SelectedIndex;
            _portConfig.PortName = cboCOM.Text;
        }
        #endregion

        #region Window Event Handlers
        private void dlgAddPortConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
                else
                    SetData();
            }
        }
        #endregion
    }
}
