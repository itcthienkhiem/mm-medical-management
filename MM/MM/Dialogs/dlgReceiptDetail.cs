using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MM.Common;
using MM.Bussiness;
using MM.Databasae;

namespace MM.Dialogs
{
    public partial class dlgReceiptDetail : Form
    {
        #region Members

        #endregion

        #region Constructor
        public dlgReceiptDetail(DataRow drReceipt)
        {
            InitializeComponent();
            DisplayInfo(drReceipt);
        }
        #endregion

        #region Properties

        #endregion

        #region UI Command
        private void DisplayInfo(DataRow drReceipt)
        {
            Cursor.Current = Cursors.WaitCursor;
            txtPatientName.Text = drReceipt["FullName"].ToString();
            txtFileNum.Text = drReceipt["FileNum"].ToString();

            if (drReceipt["Address"] != null && drReceipt["Address"] != DBNull.Value)
                txtAddress.Text = drReceipt["Address"].ToString();

            txtReceiptDate.Text = Convert.ToDateTime(drReceipt["ReceiptDate"]).ToString("dd/MM/yyyy HH:mm:ss");
            lbTotalPrice.Text = "Tổng tiền: 0 (VNĐ)";
            
            Result result = ReceiptBus.GetReceiptDetailList(drReceipt["receiptGUID"].ToString());
            if (result.IsOK)
            {
                DataTable dt = result.QueryResult as DataTable;
                dgReceiptDetail.DataSource = dt;

                double totalPrice = 0;
                foreach (DataRow row in dt.Rows)
                {
                    double amount = Convert.ToDouble(row["Amount"]);
                    totalPrice += amount;
                }

                if (totalPrice > 0)
                    lbTotalPrice.Text = string.Format("Tổng tiền: {0:#,###} (VNĐ)", totalPrice);
            }
            else
            {
                MsgBox.Show(Application.ProductName, result.GetErrorAsString("ReceiptBus.GetReceiptDetailList"), IconType.Error);
                Utility.WriteToTraceLog(result.GetErrorAsString("ReceiptBus.GetReceiptDetailList"));
            }
        }
        #endregion

        #region Window Event Handlers

        #endregion
    }
}
