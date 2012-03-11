using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgAddSoHoaDonXuatTruoc : dlgBase
    {
        #region Members

        #endregion

        #region Constructor
        public dlgAddSoHoaDonXuatTruoc()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public DataTable DataSource
        {
            get
            {
                return dgSoHoaDon.DataSource as DataTable;
            }
        }
        #endregion

        #region UI Command
        private void PhatSinhSoHoaDon()
        {
            Cursor.Current = Cursors.WaitCursor;
            int count = (int)numNo.Value;
            dgSoHoaDon.DataSource = null;

            Result result = QuanLySoHoaDonBus.GetSoHoaDonChuaXuat(count);
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;

                if (dt.Rows.Count < count)
                {
                    result = QuanLySoHoaDonBus.GetMaxSoHoaDon();
                    if (result.IsOK)
                    {
                        int maxSoHoaDon = Convert.ToInt32(result.QueryResult);
                        int delta = count - dt.Rows.Count;
                        for (int i = 0; i < delta; i++)
                        {
                            maxSoHoaDon++;
                            DataRow newRow = dt.NewRow();
                            newRow["SoHoaDon"] = maxSoHoaDon;
                            dt.Rows.Add(newRow);
                        }
                    }
                    else
                    {
                        MsgBox.Show(Application.ProductName, result.GetErrorAsString("QuanLySoHoaDonBus.GetMaxSoHoaDon"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("QuanLySoHoaDonBus.GetMaxSoHoaDon"));
                    }
                }

                dgSoHoaDon.DataSource = dt;
                RefreshNo();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("QuanLySoHoaDonBus.GetSoHoaDonChuaXuat"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("QuanLySoHoaDonBus.GetSoHoaDonChuaXuat"));
            }
        }

        private void RefreshNo()
        {
            int index = 1;
            foreach (DataGridViewRow row in dgSoHoaDon.Rows)
            {
                row.Cells["STT"].Value = index++;
            }
        }

        private bool CheckInfo()
        {
            DataTable dt = dgSoHoaDon.DataSource as DataTable;
            if (dt == null || dt.Rows.Count <= 0)
            {
                MsgBox.Show(this.Text, "Vui lòng phát sinh số hóa đơn.", IconType.Information);
                return false;
            }

            foreach (DataRow row in dt.Rows)
            {
                int soHoaDon = Convert.ToInt32(row["SoHoaDon"]);
                string code = Utility.GetCode(string.Empty, soHoaDon, 7);
                Result result = HoaDonThuocBus.CheckHoaDonThuocExistCode(Convert.ToInt32(code));
                if (result.Error.Code == ErrorCode.EXIST || result.Error.Code == ErrorCode.NOT_EXIST)
                {
                    if (result.Error.Code == ErrorCode.EXIST)
                    {
                        MsgBox.Show(this.Text, "Số hóa đơn này đã được xuất rồi. Vui lòng phát sinh lại.", IconType.Information);
                        return false;
                    }
                }
                else
                {
                    MsgBox.Show(this.Text, result.GetErrorAsString("HoaDonThuocBus.CheckHoaDonThuocExistCode"), IconType.Error);
                    return false;
                }
            }

            return true;
        }

        private void SaveInfoAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnSaveInfoProc));
                base.ShowWaiting();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }

        private void OnSaveInfo()
        {
            try
            {
                MethodInvoker method = delegate
                {
                    List<QuanLySoHoaDon> qlshdList = new List<QuanLySoHoaDon>();
                    DataTable dt = dgSoHoaDon.DataSource as DataTable;
                    foreach (DataRow row in dt.Rows)
                    {
                        QuanLySoHoaDon qlshd = new QuanLySoHoaDon();
                        qlshd.SoHoaDon = Convert.ToInt32(row["SoHoaDon"]);
                        qlshd.DaXuat = false;
                        qlshd.XuatTruoc = true;
                        qlshdList.Add(qlshd);
                    }

                    Result result = HoaDonXuatTruocBus.InsertQuanLySoHoaDon(qlshdList);
                    if (!result.IsOK)
                    {
                        MsgBox.Show(this.Text, result.GetErrorAsString("HoaDonXuatTruocBus.InsertInsertQuanLySoHoaDonService"), IconType.Error);
                        Utility.WriteToTraceLog(result.GetErrorAsString("HoaDonXuatTruocBus.InsertQuanLySoHoaDon"));
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    }
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
                Utility.WriteToTraceLog(e.Message);
            }
        }
        #endregion

        #region Window Event Handlers
        private void btnPhatSinh_Click(object sender, EventArgs e)
        {
            PhatSinhSoHoaDon();
        }

        private void dgSoHoaDon_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            RefreshNo();
        }

        private void dgSoHoaDon_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgSoHoaDon.CurrentCell.ColumnIndex == 1)
            {
                TextBox textBox = e.Control as TextBox;

                if (textBox != null)
                {
                    textBox.KeyPress -= new KeyPressEventHandler(textBox_KeyPress);
                    textBox.TextChanged -= new EventHandler(textBox_TextChanged);
                    textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
                    textBox.TextChanged += new EventHandler(textBox_TextChanged);
                }
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            int colIndex = dgSoHoaDon.CurrentCell.ColumnIndex;
            if (colIndex != 1) return;

            if (textBox.Text == null || textBox.Text.Trim() == string.Empty)
            {
                textBox.Text = "1";
            }

            string strValue = textBox.Text.Replace(",", "").Replace(".", "");

            try
            {
                int value = int.Parse(strValue);
                if (value == 0) textBox.Text = "1";
            }
            catch
            {
                textBox.Text = int.MaxValue.ToString();
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            int colIndex = dgSoHoaDon.CurrentCell.ColumnIndex;
            if (colIndex != 1) return;

            DataGridViewTextBoxEditingControl textBox = (DataGridViewTextBoxEditingControl)sender;
            if (!(char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != '\b') //allow the backspace key
                {
                    e.Handled = true;
                }
            }
        }

        private void dlgAddSoHoaDonXuatTruoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (!CheckInfo()) e.Cancel = true;
                else SaveInfoAsThread();
            }
        }
        #endregion

        #region Working Thread
        private void OnSaveInfoProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnSaveInfo();
            }
            catch (Exception e)
            {
                MsgBox.Show(this.Text, e.Message, IconType.Error);
            }
            finally
            {
                base.HideWaiting();
            }
        }
        #endregion
    }
}
