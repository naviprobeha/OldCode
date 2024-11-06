using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Navipro.SmartInventory
{
    public partial class AddItemToReceiptWorksheet : Form, Logger
    {
        private Configuration configuration;
        private SmartDatabase smartDatabase;
        private DataSet purchaseOrderDataSet;
        private string whseDocNo;


        public AddItemToReceiptWorksheet(Configuration configuration, SmartDatabase smartDatabase)
        {
            InitializeComponent();

            this.configuration = configuration;
            this.smartDatabase = smartDatabase;

            scanBox.Focus();

        }

        public AddItemToReceiptWorksheet(Configuration configuration, SmartDatabase smartDatabase, string whseDocNo)
        {
            InitializeComponent();

            this.whseDocNo = whseDocNo;
            this.configuration = configuration;
            this.smartDatabase = smartDatabase;

            scanBox.Focus();

        }
  

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void scanBinBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void scanBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                logViewList.Items.Clear();
                logViewList.Visible = true;
                logViewList.Width = 240;
                logViewList.Height = 240;
                logViewList.Top = 24;
                logViewList.Left = 0;
                
                DataPurchaseOrder purchaseOrder = NAVComm.getPurchaseOrders(configuration, smartDatabase, this, scanBox.Text);

                if (purchaseOrder != null)
                {
                    descriptionBox.Text = purchaseOrder.description;

                    purchaseOrderDataSet = DataPurchaseOrder.getDataSet(smartDatabase, purchaseOrder.itemNo, purchaseOrder.variantCode);
                    purchaseOrderGrid.DataSource = purchaseOrderDataSet.Tables[0];

                }

                logViewList.Visible = false;
                e.Handled = true;
                scanBox.Text = "";
                scanBox.Focus();
            }

        }

        #region Logger Members

        void Logger.write(string message)
        {
            logViewList.Items.Add(message);
            Application.DoEvents();   
        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            if (purchaseOrderGrid.BindingContext[purchaseOrderGrid.DataSource, ""].Count > 0)
            {
                DataPurchaseOrder dataPurchaseOrder = new DataPurchaseOrder(smartDatabase, purchaseOrderDataSet.Tables[0].Rows[purchaseOrderGrid.CurrentRowIndex]);
                dataPurchaseOrder.setWhseDocNo(whseDocNo);

                logViewList.Items.Clear();
                logViewList.Visible = true;
                logViewList.Width = 240;
                logViewList.Height = 240;
                logViewList.Top = 24;
                logViewList.Left = 0;

                whseDocNo = NAVComm.addPurchaseOrderToReceiptWorksheet(configuration, smartDatabase, this, dataPurchaseOrder);

                logViewList.Visible = false;

                if (whseDocNo != "")
                {
                    this.Close();

                }

            }
            else
            {
                System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Det finns inga inköpsorder i listan."), Translation.translate(configuration.languageCode, "Fel"), System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        public string getWhseDocNo()
        {
            return whseDocNo;
        }
    }
}