using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartOrder
{
    public partial class OrderItem : Form
    {
        private SmartDatabase smartDatabase;
        private DataItem dataItem;
        private DataItemVariantDim dataItemVariantDim;
        private DataSalesHeader dataSalesHeader;
        private DataSalesLine dataSalesLine;
        private int status;
        private bool readOnly;
        private float boxQuantity;
        private bool showPad;

        public OrderItem(SmartDatabase smartDatabase, DataSalesHeader dataSalesHeader, DataItem dataItem)
        {
            this.dataItem = dataItem;
            this.smartDatabase = smartDatabase;
            this.dataSalesHeader = dataSalesHeader;

            InitializeComponent();

            init();
        }

		public OrderItem(SmartDatabase smartDatabase, DataSalesHeader dataSalesHeader, DataItem dataItem, DataItemVariantDim dataItemVariantDim)
		{
			this.smartDatabase = smartDatabase;
			this.dataItem = dataItem;
			this.dataItemVariantDim = dataItemVariantDim;
			this.dataSalesHeader = dataSalesHeader;

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			init();
		}

		public OrderItem(SmartDatabase smartDatabase, DataSalesHeader dataSalesHeader, DataSalesLine dataSalesLine, DataItem dataItem)
		{
			this.smartDatabase = smartDatabase;
			this.dataSalesLine = dataSalesLine;
			this.dataSalesHeader = dataSalesHeader;
			
			this.dataItem = dataItem;
			if (dataSalesLine.colorCode != "")
			{
				dataItemVariantDim = new DataItemVariantDim(dataItem, dataSalesLine.colorCode, smartDatabase);
				dataItemVariantDim.getFromDb();
			}

			
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			init();
		}

        private void init()
        {
            DataSetup dataSetup = smartDatabase.getSetup();

            year.Items.Add("" + System.DateTime.Now.Year);
            year.Items.Add("" + (System.DateTime.Now.Year + 1));

            int monthCounter = 1;
            while (monthCounter <= 12)
            {
                string monthString = "" + monthCounter;
                if (monthString.Length == 1) monthString = "0" + monthString;
                month.Items.Add(monthString);
                monthCounter++;
            }

            int dayCounter = 1;
            while (dayCounter <= 31)
            {
                string dayString = "" + dayCounter;
                if (dayString.Length == 1) dayString = "0" + dayString;
                day.Items.Add(dayString);
                dayCounter++;
            }


            if ((dataSalesHeader.deliveryDate != "") && (dataSalesHeader.deliveryDate != null))
            {
                year.Text = dataSalesHeader.deliveryDate.Substring(0, 4);
                month.Text = dataSalesHeader.deliveryDate.Substring(5, 2);
                day.Text = dataSalesHeader.deliveryDate.Substring(8, 2);
            }
            else
            {
                if (dataSetup.deliveryDateToday)
                {
                    string dayString = System.DateTime.Now.Day.ToString();
                    string monthString = System.DateTime.Now.Month.ToString();
                    if (monthString.Length == 1) monthString = "0" + monthString;
                    if (dayString.Length == 1) dayString = "0" + dayString;

                    year.Text = System.DateTime.Now.Year.ToString();
                    month.Text = monthString;
                    day.Text = dayString;
                }
            }

            discount.Text = "0";
            amountBox.Text = "0";
            unitPriceBox.Text = "0";

            if (dataSalesLine.deliveryDate != null)
            {
                year.Text = dataSalesLine.deliveryDate.Substring(0, 4);
                month.Text = dataSalesLine.deliveryDate.Substring(5, 2);
                day.Text = dataSalesLine.deliveryDate.Substring(8, 2);
            }

            if (dataSalesLine.newLine)
            {
                DataCustomer dataCustomer = new DataCustomer(dataSalesHeader.customerNo);
                DataLineDiscount lineDiscount = new DataLineDiscount(dataItem, dataCustomer, dataItemVariantDim, 0, smartDatabase);

                dataSalesLine.discount = lineDiscount.discount;



                if (dataSetup.useDynamicPrices)
                {
                    DataItemPrice dataItemPrice = new DataItemPrice(dataItem, dataItemVariantDim, dataCustomer, smartDatabase);
                    dataSalesLine.unitPrice = dataItemPrice.amount;

                    if (dataSalesLine.unitPrice == 0)
                    {
                        dataSalesLine.unitPrice = dataItem.price;
                    }
                }
                else
                {
                    dataSalesLine.unitPrice = dataItem.price;
                    if (dataItemVariantDim != null)
                    {
                        if (dataItemVariantDim.unitPrice > 0) dataSalesLine.unitPrice = dataItemVariantDim.unitPrice;
                    }

                }
            }

            discount.Text = dataSalesLine.discount.ToString();
            quantity.Text = dataSalesLine.quantity.ToString();

            unitPriceBox.Text = String.Format("{0:f}", dataSalesLine.unitPrice);
            amountBox.Text = String.Format("{0:f}", dataSalesLine.amount);
            boxQuantity = dataSalesLine.boxQuantity;
            if (boxQuantity == 0) boxQuantity = 1;

            if (float.Parse(quantity.Text) == 0)
            {
                quantity.Text = dataItem.defaultQuantity.ToString();
                updateAmount();
            }


            updateGrid();

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void updateGrid()
        {

            itemNoBox.Text = dataItem.no;
            descriptionBox.Text = dataItem.description;
            if (dataItemVariantDim != null)
            {
                //variantBox.Text = dataItemVariantDim.code;
                //variantDescBox.Text = dataItemVariantDim.description;
            }
        }


        public void hideButtons()
        {
            this.button2.Visible = false;
        }

        public void setReadOnly()
        {
            this.readOnly = true;
            discount.ReadOnly = true;
            quantity.ReadOnly = true;
            unitPriceBox.ReadOnly = true;
        }

        public int getStatus()
        {
            return status;
        }

        private void updateAmount()
        {
            amountBox.Text = String.Format("{0:f}", ((float.Parse(quantity.Text) * float.Parse(unitPriceBox.Text)) - (float.Parse(quantity.Text) * float.Parse(unitPriceBox.Text) * (float.Parse(discount.Text) / 100))));
            //dataItem.price = float.Parse(unitPriceBox.Text);
            //dataItem.commit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if ((year.Text == "") || (month.Text == "") || (day.Text == ""))
            {
                System.Windows.Forms.MessageBox.Show("Du måste fylla i leveransdatum.");
            }
            else
            {
                dataSalesLine.deliveryDate = year.Text + "-" + month.Text + "-" + day.Text;
                dataSalesLine.discount = float.Parse(discount.Text);
                dataSalesLine.quantity = float.Parse(quantity.Text);
                dataSalesLine.unitPrice = float.Parse(unitPriceBox.Text);
                dataSalesLine.amount = float.Parse(amountBox.Text);
                dataSalesLine.boxQuantity = boxQuantity;

                dataSalesLine.save();

                status = 0;
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((year.Text == "") || (month.Text == "") || (day.Text == ""))
            {
                System.Windows.Forms.MessageBox.Show("Du måste fylla i leveransdatum.");
            }
            else
            {
                dataSalesLine.deliveryDate = year.Text + "-" + month.Text + "-" + day.Text;
                dataSalesLine.discount = float.Parse(discount.Text);
                dataSalesLine.quantity = float.Parse(quantity.Text);
                dataSalesLine.unitPrice = float.Parse(unitPriceBox.Text);
                dataSalesLine.amount = float.Parse(amountBox.Text);
                dataSalesLine.boxQuantity = boxQuantity;

                dataSalesLine.save();
                status = 1;
                this.Close();
            }
        }

 
        private void discount_GotFocus(object sender, EventArgs e)
        {
            if (!readOnly)
            {
                if (showPad)
                {
                    showPad = false;
                    QuantityForm discountForm = new QuantityForm(dataItem);
                    discountForm.setCaption("Rabatt (%):");
                    discountForm.setValue(discount.Text);
                    discountForm.ShowDialog();
                    if (discountForm.getStatus() == 1)
                    {
                        discount.Text = discountForm.getValue("{0:f}");
                        if (discount.Text == "") discount.Text = "0";
                        updateAmount();
                    }
                    discountForm.Dispose();
                }
                itemNoBox.Focus();
            }
        }

        private void quantity_GotFocus(object sender, EventArgs e)
        {
            if (!readOnly)
            {
                if (showPad)
                {
                    showPad = false;
                    QuantityForm quantityForm = new QuantityForm(dataItem);
                    quantityForm.setCaption("Antal:");
                    quantityForm.enableBoxQuantity();
                    quantityForm.setBoxValue(boxQuantity.ToString());
                    quantityForm.setTotalValue(float.Parse(quantity.Text).ToString());
                    quantityForm.ShowDialog();
                    if (quantityForm.getStatus() == 1)
                    {
                        quantity.Text = quantityForm.getTotalValue("{0:0}");
                        boxQuantity = float.Parse(quantityForm.getBoxValue("{0:f}"));
                        if (quantity.Text == "") quantity.Text = "0";
                        updateAmount();

                    }
                    quantityForm.Dispose();
                }
                itemNoBox.Focus();
            }
        }

        private void year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((year.Text != "") && (month.Text != "") && (day.Text != ""))
            {
                try
                {
                    System.DateTime dateTime = new System.DateTime(int.Parse(year.Text), int.Parse(month.Text), int.Parse(day.Text));
                }
                catch (Exception f)
                {
                    System.Windows.Forms.MessageBox.Show("Datumet är ogiltigt.");
                }
            }
		
        }

        private void month_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((year.Text != "") && (month.Text != "") && (day.Text != ""))
            {
                try
                {
                    System.DateTime dateTime = new System.DateTime(int.Parse(year.Text), int.Parse(month.Text), int.Parse(day.Text));
                }
                catch (Exception f)
                {
                    System.Windows.Forms.MessageBox.Show("Datumet är ogiltigt.");
                }
            }
        }

        private void day_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((year.Text != "") && (month.Text != "") && (day.Text != ""))
            {
                try
                {
                    System.DateTime dateTime = new System.DateTime(int.Parse(year.Text), int.Parse(month.Text), int.Parse(day.Text));
                }
                catch (Exception f)
                {
                    System.Windows.Forms.MessageBox.Show("Datumet är ogiltigt.");
                    day.Text = "";
                }
            }
        }

        private void unitPriceBox_GotFocus(object sender, EventArgs e)
        {
            if (!readOnly)
            {
                if (showPad)
                {
                    showPad = false;

                    QuantityForm quantityForm = new QuantityForm(dataItem);
                    quantityForm.setCaption("A-pris:");
                    quantityForm.setValue(unitPriceBox.Text);
                    quantityForm.ShowDialog();
                    if (quantityForm.getStatus() == 1)
                    {
                        unitPriceBox.Text = quantityForm.getValue("{0:f}");

                        if (unitPriceBox.Text == "") unitPriceBox.Text = "0";
                        updateAmount();
                    }
                    quantityForm.Dispose();
                }
                itemNoBox.Focus();
            }
        }

        private void amountBox_GotFocus(object sender, EventArgs e)
        {
            if (!readOnly)
            {
                if (showPad)
                {
                    showPad = false;

                    QuantityForm quantityForm = new QuantityForm(dataItem);
                    quantityForm.setCaption("Belopp:");
                    quantityForm.setValue(amountBox.Text);
                    quantityForm.ShowDialog();
                    if (quantityForm.getStatus() == 1)
                    {
                        amountBox.Text = quantityForm.getValue("{0:f}");
                        if ((amountBox.Text != "") && (amountBox.Text != "0"))
                        {
                            unitPriceBox.Text = String.Format("{0:f}", float.Parse(amountBox.Text) / float.Parse(this.quantity.Text));
                            this.discount.Text = "0";
                        }
                        else
                        {
                            amountBox.Text = "0";
                            unitPriceBox.Text = "0";
                        }
                        updateAmount();
                    }
                    quantityForm.Dispose();
                }
                itemNoBox.Focus();
            }		
        }

        private void itemNoBox_GotFocus(object sender, EventArgs e)
        {
            showPad = true;
        }
    }
}