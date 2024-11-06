using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartShipment
{
    public partial class OrderLines : Form, Logger
    {

        private DataSalesHeader dataSalesHeader;
        private DataSalesLines dataSalesLines;
        private SmartDatabase smartDatabase;
        private DataSet salesLineDataSet;
        private DataSetup dataSetup;
        private int status;

        public OrderLines(DataSalesHeader dataSalesHeader, SmartDatabase smartDatabase, DataSetup dataSetup)
        {
            InitializeComponent();

            this.dataSalesHeader = dataSalesHeader;
            this.smartDatabase = smartDatabase;
            this.dataSetup = dataSetup;

            this.Text = "Order " + dataSetup.getAgent().agentId + dataSalesHeader.no.ToString();
            dataSalesLines = new DataSalesLines(smartDatabase);
        }

 
        private void updateGrid()
        {
            salesLineDataSet = dataSalesLines.getDataSet(dataSalesHeader);
            salesLineGrid.DataSource = salesLineDataSet.Tables[0];

            DataColumn lineUnitPriceCol = salesLineDataSet.Tables[0].Columns.Add("lineUnitPrice");
            DataColumn lineAmountCol = salesLineDataSet.Tables[0].Columns.Add("lineAmount");

            int i = 0;

            if (salesLineDataSet.Tables[0].Rows.Count > 0)
            {
                while (i < salesLineDataSet.Tables[0].Rows.Count)
                {

                    salesLineGrid[i, salesLineTable.GridColumnStyles.IndexOf(this.unitPriceCol)] = String.Format("{0:f}", salesLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(8));
                    salesLineGrid[i, salesLineTable.GridColumnStyles.IndexOf(this.amountCol)] = String.Format("{0:f}", salesLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(9));

                    i++;
                }

                salesLineGrid.CurrentRowIndex = 0;
            }

        }

        private void OrderLines_Load(object sender, EventArgs e)
        {
            updateGrid();

            this.scanBox.Focus();
        }

        private void fetchUnitPrice(ref DataItem dataItem)
        {
            serviceLog.Items.Clear();
            serviceLog.Items.Add("Hämtar prislista...");
            serviceLog.Visible = true;
            serviceLog.Width = 480;
            serviceLog.Height = 340;

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            bool error = false;

            disableButtons();

            Service synchService = new Service("priceRequest", smartDatabase, dataSetup);
            synchService.setLogger(this);

            ItemPrice itemPrice = new ItemPrice(smartDatabase, new DataCustomer(dataSalesHeader.customerNo), dataItem);
            ItemPrice itemPriceResponse = new ItemPrice(smartDatabase, new DataCustomer(dataSalesHeader.customerNo), dataItem);

            synchService.serviceRequest.setServiceArgument(itemPrice);

            ServiceResponse serviceResponse = synchService.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.error.status + ": " + serviceResponse.error.description, "Fel", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                    write("Förfrågan misslyckades.");
                    error = true;
                }
                else
                {
                    write("Förfrågan klar.");
                    itemPriceResponse = serviceResponse.itemPrice;
                }
            }
            else
            {
                write("Förfrågan misslyckades.");
                error = true;
            }

            enableButtons();

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            serviceLog.Visible = false;

            if (error == true) return;

            dataItem.price = itemPriceResponse.unitPrice;
            dataItem.discount = itemPriceResponse.discount;
        }

        private void scanBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void scanBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) || (e.KeyChar == '>'))
            {
                e.Handled = true;
                DataReference reference = new DataReference(scanBox.Text);
                reference.customerNo = dataSalesHeader.customerNo;
                DataReference responseReference = fetchSerialReference(reference);

                if (responseReference != null)
                {
                    if (responseReference.no != "")
                    {
                        Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);

                        DataSalesLine dataSalesLine = new DataSalesLine(dataSalesHeader, smartDatabase);
                        dataSalesLine.itemNo = responseReference.itemNo;
                        dataSalesLine.description = responseReference.description;
                        dataSalesLine.unitPrice = responseReference.unitPrice * (1 - (responseReference.discount / 100));
                        dataSalesLine.discount = responseReference.discount;
                        dataSalesLine.baseUnit = responseReference.baseUnit;
                        dataSalesLine.hanging = responseReference.hanging;
                        dataSalesLine.referenceNo = responseReference.no;

                        string month = System.DateTime.Today.Month.ToString();
                        string day = System.DateTime.Today.Day.ToString();
                        if (month.Length == 1) month = "0" + month;
                        if (day.Length == 1) day = "0" + day;

                        dataSalesLine.deliveryDate = System.DateTime.Today.Year.ToString() + "-" + month + "-" + day;

                        dataSalesLine.quantity = 1;
                        dataSalesLine.amount = dataSalesLine.quantity * dataSalesLine.unitPrice;
                        dataSalesLine.save();

                        updateGrid();
                    }
                    else
                    {
                        Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                    }
                }
                else
                {
                    Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
                }

                e.Handled = true;
                scanBox.Text = "";
                scanBox.Focus();
            }

        }

        private DataReference fetchSerialReference(DataReference dataReference)
        {
            DataReference responseReference = null;

            serviceLog.Items.Clear();
            serviceLog.Items.Add("Hämtar lagerartikel...");
            serviceLog.Visible = true;
            serviceLog.Width = 480;
            serviceLog.Height = 340;

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            System.Windows.Forms.Cursor.Show();

            disableButtons();

            Service synchService = new Service("referenceRequest", smartDatabase, dataSetup);
            synchService.setLogger(this);

            synchService.serviceRequest.setServiceArgument(dataReference);

            ServiceResponse serviceResponse = synchService.performService();

            if (serviceResponse != null)
            {
                if (serviceResponse.hasErrors)
                {
                    System.Windows.Forms.MessageBox.Show(serviceResponse.error.status + ": " + serviceResponse.error.description, "Fel", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                    write("Förfrågan misslyckades.");
                }
                else
                {
                    write("Förfrågan klar.");
                    responseReference = serviceResponse.reference;
                }
            }
            else
            {
                write("Förfrågan misslyckades.");
            }

            enableButtons();

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            System.Windows.Forms.Cursor.Hide();

            serviceLog.Visible = false;

            return responseReference;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ItemList itemList = new ItemList(smartDatabase);
            itemList.ShowDialog();
            if (itemList.getItem() != null)
            {
                DataItem dataItem = itemList.getItem();

                DataSalesLine dataSalesLine = new DataSalesLine(dataSalesHeader, smartDatabase);
                dataSalesLine.itemNo = dataItem.no;
                dataSalesLine.description = dataItem.description;
                dataSalesLine.unitPrice = dataItem.price;

                string month = System.DateTime.Today.Month.ToString();
                string day = System.DateTime.Today.Day.ToString();
                if (month.Length == 1) month = "0" + month;
                if (day.Length == 1) day = "0" + day;

                dataSalesLine.deliveryDate = System.DateTime.Today.Year.ToString() + "-" + month + "-" + day;

                fetchUnitPrice(ref dataItem);

                QuantityForm quantityForm = new QuantityForm(dataItem);

                quantityForm.ShowDialog();
                if (quantityForm.getStatus() == 1)
                {
                    dataSalesLine.quantity = float.Parse(quantityForm.getValue("{0:f}"));
                    dataSalesLine.unitPrice = quantityForm.getUnitPrice();
                    dataSalesLine.discount = quantityForm.getDiscount();
                    dataSalesLine.amount = dataSalesLine.quantity * dataSalesLine.unitPrice;

                    /*
                    dataSalesLine.unitPrice = dataItem.price * (1 - (dataItem.discount / 100));
                    dataSalesLine.discount = dataItem.discount;
                    dataSalesLine.amount = dataSalesLine.quantity * dataSalesLine.unitPrice;
                    */
                    dataSalesLine.save();
                }

                updateGrid();
            }

            itemList.Dispose();
            scanBox.Focus();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (salesLineGrid.BindingContext[salesLineGrid.DataSource, ""].Count > 0)
            {
                int lineNo = (int)salesLineDataSet.Tables[0].Rows[salesLineGrid.CurrentRowIndex].ItemArray.GetValue(0);
                DataSalesLine dataSalesLine = new DataSalesLine(lineNo, smartDatabase);

                dataSalesLine.delete();

                updateGrid();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("En rad måste markeras.", "Fel", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }

            scanBox.Focus();
        }

        private void disableButtons()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
        }

        private void enableButtons()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (salesLineGrid.BindingContext[salesLineGrid.DataSource, ""].Count > 0)
            {
                //if (salesLineDataSet.Tables[0].Rows[salesLineGrid.CurrentRowIndex].ItemArray.GetValue(11).ToString() == "")
                //{
                int lineNo = (int)salesLineDataSet.Tables[0].Rows[salesLineGrid.CurrentRowIndex].ItemArray.GetValue(0);
                DataSalesLine dataSalesLine = new DataSalesLine(lineNo, smartDatabase);

                DataItem dataItem = new DataItem(salesLineGrid[salesLineGrid.CurrentRowIndex, 0].ToString(), smartDatabase);
                dataItem.description = salesLineGrid[salesLineGrid.CurrentRowIndex, 1].ToString();

                QuantityForm quantityForm = new QuantityForm(dataItem);
                quantityForm.setQuantity(dataSalesLine.quantity);
                quantityForm.setUnitPrice(dataSalesLine.unitPrice);
                quantityForm.setDiscount(dataSalesLine.discount);

                quantityForm.ShowDialog();
                if (quantityForm.getStatus() == 1)
                {
                    dataSalesLine.quantity = float.Parse(quantityForm.getValue("{0:f}"));
                    dataSalesLine.unitPrice = quantityForm.getUnitPrice();
                    dataSalesLine.discount = quantityForm.getDiscount();
                    dataSalesLine.amount = dataSalesLine.quantity * dataSalesLine.unitPrice;
                    dataSalesLine.save();

                    updateGrid();
                }

                quantityForm.Dispose();
                //}
                //else
                //{
                //	System.Windows.Forms.MessageBox.Show("Det går inte att ändra antal på en lagerdörr.", "Fel", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);

                //}

            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Det finns inga orderrader.", "Fel", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);

            }
            scanBox.Focus();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            status = 1;
            this.Close();
        }

        public int getStatus()
        {
            return status;
        }

        #region Logger Members

        public void write(string message)
        {
            serviceLog.Items.Add(message);
            Application.DoEvents();
        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}