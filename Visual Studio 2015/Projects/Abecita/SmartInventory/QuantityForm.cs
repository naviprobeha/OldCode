using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SmartInventory
{
    public partial class QuantityForm : Form
    {
        private int status;
        private bool notAllowDecimal;

        public QuantityForm(string itemNo, string description, SmartDatabase smartDatabase)
        {
            InitializeComponent();

            itemNoBox.Text = itemNo;
            descriptionBox.Text = description;

            DataSetup dataSetup = new DataSetup(smartDatabase);
            if (dataSetup.allowDecimal == false)
            {
                button14.Enabled = false;
                notAllowDecimal = true;
            }
        }

        private void QuantityForm_Load(object sender, EventArgs e)
        {
            if (quantityBox.Text == "") quantityBox.Text = "0";
            quantityBox.Focus();
            quantityBox.SelectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            appendButton("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            appendButton("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            appendButton("3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            appendButton("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            appendButton("5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            appendButton("6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            appendButton("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            appendButton("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            appendButton("9");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            appendButton("0");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            appendButton(".");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            appendButton("CL");
        }

        public string getValue()
        {
            return quantityBox.Text;
        }

        public void setValue(string strValue)
        {
            quantityBox.Text = strValue;
        }

        public int getStatus()
        {
            return status;
        }

        public void setCaption(string caption, string caption2)
        {
            label6.Text = caption;
            label1.Text = caption2;
        }

        private void appendButton(string buttonStr)
        {
            if (quantityBox.SelectionLength > 0)
            {
                if (buttonStr.Equals("CL"))
                {
                    quantityBox.SelectedText = "";
                }
                else
                {
                    quantityBox.SelectedText = buttonStr;
                }
            }
            else
            {
                if (buttonStr.Equals("CL"))
                {
                    quantityBox.Text = quantityBox.Text.Substring(0, quantityBox.Text.Length - 1);
                }
                else
                {
                    quantityBox.Text = quantityBox.Text + buttonStr;
                }
            }
        }

        private void quantityBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) button10_Click(sender, null);
            if ((e.KeyChar == '.') && (notAllowDecimal))
            {
                e.Handled = false;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            status = 0;
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            status = 1;
            this.Close();
        }

    }
}