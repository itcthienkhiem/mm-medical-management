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
            txtCollector.Text = drReceipt["Collector"].ToString();
            txtReceiptDate.Text = Convert.ToDateTime(drReceipt["ReceiptDate"]).ToString("dd/MM/yyyy HH:mm:ss");

            double totalPrice = Convert.ToDouble(drReceipt["TotalPrice"]);
            if (totalPrice == 0)
                lbTotalPrice.Text = "Tổng tiền: 0 (VNĐ)";
            else
                lbTotalPrice.Text = string.Format("Tổng tiền: {0:#,###} (VNĐ)", totalPrice);

            double promotion = Convert.ToDouble(drReceipt["Promotion"]);
            if (promotion == 0)
                lbPromotion.Text = "Giảm giá: 0 (VNĐ)";
            else
                lbPromotion.Text = string.Format("Giảm giá: {0:#,###} (VNĐ)", promotion);

            double payment = Convert.ToDouble(drReceipt["Payment"]);
            if (payment == 0)
                lbPayment.Text = "Còn lại: 0 (VNĐ)";
            else
                lbPayment.Text = string.Format("Còn lại: {0:#,###} (VNĐ)", payment);

            Result result = ReceiptBus.GetReceiptDetailList(drReceipt["receiptGUID"].ToString());
            if (result.IsOK)
                dgReceiptDetail.DataSource = result.QueryResult as DataTable;
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
