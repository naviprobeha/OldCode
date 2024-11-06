using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartShipment
{
    public partial class QuantityForm : Form
    {

        private int status;
        private System.Windows.Forms.TextBox currentBox;
        private DataItem dataItem;
        private float grossPrice;

        public QuantityForm(DataItem dataItem)
        {
            InitializeComponent();

            this.dataItem = dataItem;

            itemNoBox.Text = dataItem.no;
            descriptionBox.Text = dataItem.description;

            setUnitPrice(dataItem.price);
            setDiscount(dataItem.discount);


            currentBox = quantityBox;	
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            appendButton("3");
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            if (quantityBox.Text == "")
            {
                quantityBox.Text = "1";
            }

            currentBox.Focus();
            currentBox.SelectAll();
        }


        private void button1_Click(object sender, System.EventArgs e)
        {
            appendButton("1");
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            appendButton("2");
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            appendButton("4");
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            appendButton("5");
        }

        private void button6_Click(object sender, System.EventArgs e)
        {
            appendButton("6");
        }

        private void button7_Click(object sender, System.EventArgs e)
        {
            appendButton("7");
        }

        private void button8_Click(object sender, System.EventArgs e)
        {
            appendButton("8");
        }

        private void button9_Click(object sender, System.EventArgs e)
        {
            appendButton("9");
        }

        private void button13_Click(object sender, System.EventArgs e)
        {
            appendButton("0");
        }

        private void button14_Click(object sender, System.EventArgs e)
        {
            if (currentBox.Text.IndexOf(".") == -1)
            {
                if ((currentBox.Text.Length > 0) && (currentBox.Text != "0"))
                {
                    appendButton(".");
                }
            }
        }

        private void button12_Click(object sender, System.EventArgs e)
        {
            appendButton("CL");
        }

        public string getValue(string format)
        {
            if (format != "")
            {
                try
                {
                    return String.Format(format, float.Parse(quantityBox.Text));
                }
                catch (Exception e)
                {
                    return "0";
                }
            }
            return quantityBox.Text;
        }


        public void setQuantity(float quantity)
        {
            quantityBox.Text = quantity.ToString();

            updateAmounts();
        }

        public void setUnitPrice(float unitPrice)
        {
            unitPriceBox.Text = unitPrice.ToString();

            grossPriceBox.Text = String.Format("{0:f}", unitPrice);

            updateAmounts();
        }

        public void setDiscount(float discount)
        {
            discountBox.Text = discount.ToString();
            grossPrice = float.Parse(unitPriceBox.Text) / (1 - (float.Parse(discountBox.Text) / 100));

            grossPriceBox.Text = String.Format("{0:f}", grossPrice);

            updateAmounts();
        }

        public int getStatus()
        {
            return status;
        }

        public float getUnitPrice()
        {
            return float.Parse(unitPriceBox.Text);
        }

        public float getDiscount()
        {
            return float.Parse(discountBox.Text);
        }


        public void setCaption(string caption)
        {
            label6.Text = caption;
        }

        private void button10_Click(object sender, System.EventArgs e)
        {
            status = 1;
            this.Close();
        }

        private void button11_Click(object sender, System.EventArgs e)
        {
            status = 0;
            this.Close();
        }

        private void appendButton(string buttonStr)
        {
            if (currentBox.SelectionLength > 0)
            {
                if (buttonStr.Equals("CL"))
                {
                    currentBox.SelectedText = "";
                }
                else
                {
                    currentBox.SelectedText = buttonStr;
                }
            }
            else
            {
                if (buttonStr.Equals("CL"))
                {
                    currentBox.Text = currentBox.Text.Substring(0, currentBox.Text.Length - 1);
                }
                else
                {
                    if (currentBox.Text == "0")
                        currentBox.Text = buttonStr;
                    else
                        currentBox.Text = currentBox.Text + buttonStr;
                }
            }

            updateAmounts();

        }

        private void button15_Click(object sender, System.EventArgs e)
        {
            appendButton("-");
        }


        private void quantityBox_TextChanged(object sender, System.EventArgs e)
        {
            updateAmounts();
        }

        private void quantityBox_GotFocus(object sender, System.EventArgs e)
        {
            currentBox = quantityBox;
            //currentBox.SelectAll();

            Timer timer = new Timer();
            timer.Interval = 10;
            timer.Tick += new EventHandler(timer_SelectAll);
            timer.Enabled = true;
        }

        public void timer_SelectAll(object sender, System.EventArgs e)
        {
            ((Timer)sender).Enabled = false;
            currentBox.SelectAll();

        }

        private void unitPriceBox_GotFocus(object sender, System.EventArgs e)
        {
            currentBox = unitPriceBox;

            Timer timer = new Timer();
            timer.Interval = 10;
            timer.Tick += new EventHandler(timer_SelectAll);
            timer.Enabled = true;

        }

        private void totalBox_GotFocus(object sender, System.EventArgs e)
        {
        }

        private void discountBox_GotFocus(object sender, System.EventArgs e)
        {
            currentBox = discountBox;

            Timer timer = new Timer();
            timer.Interval = 10;
            timer.Tick += new EventHandler(timer_SelectAll);
            timer.Enabled = true;

        }

        private void updateAmounts()
        {
            if (quantityBox.Text == "") quantityBox.Text = "0";
            if (unitPriceBox.Text == "") unitPriceBox.Text = "0";
            if (discountBox.Text == "") discountBox.Text = "0";

            if ((quantityBox.Text.Length > 0) && (unitPriceBox.Text.Length > 0))
            {
                if ((quantityBox.Text != "-") && (quantityBox.Text != "0,") && (quantityBox.Text != ",") && (unitPriceBox.Text != "0,"))
                {
                    if (currentBox == unitPriceBox)
                    {
                        if (grossPrice > 0)
                        {
                            discountBox.Text = (((grossPrice - float.Parse(unitPriceBox.Text)) / grossPrice) * 100).ToString();
                        }
                    }
                    if (currentBox == discountBox)
                    {
                        unitPriceBox.Text = (grossPrice * (1 - (float.Parse(discountBox.Text) / 100))).ToString();
                    }
                    totalBox.Text = String.Format("{0:f}", float.Parse(unitPriceBox.Text) * float.Parse(quantityBox.Text));
                }
            }
        }

        private void quantityBox_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}