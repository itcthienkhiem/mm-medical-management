﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MM.Common;
using MM.Databasae;
using MM.Bussiness;

namespace MM.Dialogs
{
    public partial class dlgSelectTinNhanMau : dlgBase
    {
        #region Members

        #endregion

        #region Constructor
        public dlgSelectTinNhanMau()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public string TinNhanMau
        {
            get
            {
                if (dgTinNhanMau.SelectedRows == null || dgTinNhanMau.SelectedRows.Count <= 0) return string.Empty;
                DataRow row = (dgTinNhanMau.SelectedRows[0].DataBoundItem as DataRowView).Row;
                return row["NoiDung"].ToString();
            }
        }
        #endregion

        #region UI Command
        public void DisplayAsThread()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(OnDisplayTinNhanMauListProc));
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

        private void OnDisplayTinNhanMauList()
        {
            Result result = TinNhanMauBus.GetTinNhanMauDaDuyetList();
            if (result.IsOK)
            {
                MethodInvoker method = delegate
                {
                    dgTinNhanMau.DataSource = result.QueryResult;
                };

                if (InvokeRequired) BeginInvoke(method);
                else method.Invoke();
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("TinNhanMauBus.GetTinNhanMauList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("TinNhanMauBus.GetTinNhanMauList"));
            }
        }
        #endregion

        #region Window Event Handlers
        private void dlgSelectTinNhanMau_Load(object sender, EventArgs e)
        {
            DisplayAsThread();
        }

        private void dlgSelectTinNhanMau_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (dgTinNhanMau.SelectedRows == null || dgTinNhanMau.SelectedRows.Count <= 0)
                {
                    MsgBox.Show(this.Text, "Vui lòng chọn 1 tin nhắn mẫu", IconType.Information);
                    e.Cancel = true;
                }
            }
        }

        private void dgTinNhanMau_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Working Thread
        private void OnDisplayTinNhanMauListProc(object state)
        {
            try
            {
                //Thread.Sleep(500);
                OnDisplayTinNhanMauList();
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
}
