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
    public partial class ReceiptWorksheet : Form
    {
        private Configuration configuration;
        private SmartDatabase smartDatabase;
        private DataSet receiptLineDataSet;

        private string _documentNo = "";

        public ReceiptWorksheet(Configuration configuration, SmartDatabase smartDatabase, string documentNo)
        {
            InitializeComponent();

            this.configuration = configuration;
            this.smartDatabase = smartDatabase;
            this._documentNo = documentNo;

            updateView();

        }

        private void updateView()
        {
            this.receiptNoBox.Text = _documentNo;
            int noOfLines = DataReceiptLine.countLines(smartDatabase, _documentNo);
            this.noOfLinesBox.Text = noOfLines.ToString();

            receiptLineDataSet = DataReceiptLine.getDataSet(smartDatabase, _documentNo);
            receiptLinesGrid.DataSource = receiptLineDataSet.Tables[0];
            

            scanBox.Focus();

        }

        private void scanBox_GotFocus(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void scanBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                DataItemCrossReference itemCross = DataItemCrossReference.getItem(smartDatabase, _documentNo, scanBox.Text);
                if (itemCross != null)
                {
                    DataReceiptLine dataReceiptLine = DataReceiptLine.getFirstItemLine(smartDatabase, _documentNo, itemCross.itemNo, "");
                    if (dataReceiptLine == null)
                    {
                        dataReceiptLine = new DataReceiptLine(smartDatabase);

                    }

                    ReceiptItem receiptItem = new ReceiptItem(configuration, smartDatabase, dataReceiptLine);
                    receiptItem.ShowDialog();

                    receiptItem.Dispose();

                }
                else
                {
                    //StoreItem storeItem = new StoreItem(configuration, smartDatabase, dataStoreLine, scanBox.Text);
                    //storeItem.ShowDialog();

                    //storeItem.Dispose();

                    //updateView();
                }


                e.Handled = true;
                scanBox.Text = "";
                scanBox.Focus();
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (receiptLinesGrid.BindingContext[receiptLinesGrid.DataSource, ""].Count > 0)
            {
                string itemNo = receiptLineDataSet.Tables[0].Rows[receiptLinesGrid.CurrentRowIndex].ItemArray.GetValue(4).ToString();

                DataReceiptLine dataReceiptLine = DataReceiptLine.getFirstItemLine(smartDatabase, _documentNo, itemNo, "");
                if (dataReceiptLine == null)
                {
                    dataReceiptLine = new DataReceiptLine(smartDatabase);

                }


                ReceiptItem receiptItem = new ReceiptItem(configuration, smartDatabase, dataReceiptLine);
                receiptItem.ShowDialog();

                receiptItem.Dispose();

                updateView();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(Translation.translate(configuration.languageCode, "Det finns inga rader i listan."), Translation.translate(configuration.languageCode, "Fel"), System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);

            }

        }
    }
}