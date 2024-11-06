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
    public partial class QuantityForm : Form
    {
		private int status;
		private System.Windows.Forms.TextBox currentBox;
        private DataItem dataItem;
        private DataColor dataColor;
        private DataSize dataSize;
        private DataItemVariantDim dataItemVariantDim;
        private DataSize2 dataSize2;


		public QuantityForm(DataItem dataItem, DataColor dataColor, DataSize dataSize, DataSize2 dataSize2)
		{
			this.dataItem = dataItem;
			this.dataColor = dataColor;
			this.dataSize = dataSize;
			this.dataSize2 = dataSize2;

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			itemNoBox.Text = dataItem.no;
			descriptionBox.Text = dataItem.description;
			/*
			if (dataSize2 != null)
			{
				size2Box.Visible = true;
				label5.Visible = true;
				size2Box.Text = dataSize2.code;
			}
			else
			{
				size2Box.Visible = false;
				label5.Visible = false;
			}

             */
		}

		public QuantityForm(DataItem dataItem, DataColor dataColor)
		{
			this.dataItem = dataItem;
			this.dataColor = dataColor;

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			itemNoBox.Text = dataItem.no;
			descriptionBox.Text = dataItem.description;
			//colorBox.Text = dataColor.code;

			//sizeBox.Visible = false;
			//size2Box.Visible = false;
            label3.Visible = false;
            label4.Visible = false;

            quantityBox.Top = 67;
            quantityBox.Left = 4;
            quantityBox.Width = 232;
            label5.Top = 51;
            label5.Left = 4;
            label5.Width = 200;


		}

		public QuantityForm(DataItem dataItem, DataItemVariantDim dataItemVariantDim)
		{
			this.dataItem = dataItem;
			this.dataItemVariantDim = dataItemVariantDim;

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			itemNoBox.Text = dataItem.no;
			descriptionBox.Text = dataItem.description;
			//colorBox.Text = dataItemVariantDim.code;

			label3.Text = "Variant";

			//sizeBox.Visible = false;
			//size2Box.Visible = false;
            label3.Visible = false;
            label4.Visible = false;

            quantityBox.Top = 67;
            quantityBox.Left = 4;
            quantityBox.Width = 232;
            label5.Top = 51;
            label5.Left = 4;
            label5.Width = 200;

            currentBox = quantityBox;	

		}

		public QuantityForm(DataItem dataItem)
		{
			this.dataItem = dataItem;

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			itemNoBox.Text = dataItem.no;
			descriptionBox.Text = dataItem.description;
	
			//colorBox.Visible = false;
			//sizeBox.Visible = false;
			//size2Box.Visible = false;
			//label5.Visible = false;
            label3.Visible = false;
            label4.Visible = false;

            quantityBox.Top = 67;
            quantityBox.Left = 4;
            quantityBox.Width = 232;
            label5.Top = 51;
            label5.Left = 4;
            label5.Width = 200;

			currentBox = quantityBox;	

		}

        private void button4_Click(object sender, EventArgs e)
        {
            appendButton("-");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            appendButton("0");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            appendButton(".");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            appendButton("1");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            appendButton("2");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            appendButton("3");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            appendButton("4");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            appendButton("5");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            appendButton("6");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            appendButton("7");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            appendButton("8");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            appendButton("9");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            appendButton("CL");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            status = 0;
            this.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            status = 1;
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
                    {
                        if (buttonStr == ".")
                        {
                            currentBox.Text = currentBox.Text + buttonStr;
                        }
                        else
                        {
                            currentBox.Text = buttonStr;
                        }
                    }
                    else
                    {
                        if (buttonStr == ".")
                        {
                            if (currentBox.Text == "")
                            {
                                currentBox.Text = "0.";
                            }
                            else
                            {
                                currentBox.Text = currentBox.Text + buttonStr;
                            }
                        }
                        else
                            currentBox.Text = currentBox.Text + buttonStr;
                    }
                }
            }

            if ((quantityBox.Text.Length > 0) && (boxQuantity.Text.Length > 0))
            {
                if ((quantityBox.Text != "-") && (boxQuantity.Text != "-") && (boxQuantity.Text != "0.") && (quantityBox.Text != "0.") && (quantityBox.Text != ".") && (boxQuantity.Text != "0."))
                {
                    totalQuantity.Text = "" + float.Parse(quantityBox.Text) * float.Parse(boxQuantity.Text);
                }
            }
            else
            {
                totalQuantity.Text = "0";
            }
        }

        private void QuantityForm_Load(object sender, EventArgs e)
        {
            if (quantityBox.Text == "") quantityBox.Text = "1";

            if ((quantityBox.Text.Length > 0) && (boxQuantity.Text.Length > 0))
            {
                totalQuantity.Text = "" + float.Parse(quantityBox.Text) * float.Parse(boxQuantity.Text);
            }

            currentBox.Focus();
            currentBox.SelectAll();
        }

        public void enableBoxQuantity()
        {
            
            quantityBox.Top = 67;
            quantityBox.Left = 79;
            quantityBox.Width = 74;
            label3.Top = 51;
            label3.Left = 4;
            label3.Width = 64;

            boxQuantity.Top = 67;
            boxQuantity.Left = 4;
            boxQuantity.Width = 69;

            label5.Top = 51;
            label5.Left = 78;
            label5.Width = 68;

            boxQuantity.Visible = true;
            label3.Visible = true;

            totalQuantity.Visible = true;
            label4.Visible = true;
            
            currentBox = boxQuantity;
            boxQuantity.Text = "1";

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

        public string getTotalValue(string format)
        {
            if (format != "")
            {
                try
                {
                    return String.Format(format, float.Parse(totalQuantity.Text));
                }
                catch (Exception e)
                {
                    return "0";
                }
            }
            return totalQuantity.Text;
        }

        public string getBoxValue(string format)
        {
            if (format != "")
            {
                try
                {
                    return String.Format(format, float.Parse(boxQuantity.Text));
                }
                catch (Exception e)
                {
                    return "0";
                }
            }
            return boxQuantity.Text;
        }

        public void setValue(string strValue)
        {
            quantityBox.Text = strValue;
        }

        public void setBoxValue(string strValue)
        {
            boxQuantity.Text = strValue;
            if (float.Parse(strValue) == 0) boxQuantity.Text = "1";
        }

        public void setTotalValue(string strValue)
        {
            totalQuantity.Text = strValue;
            if (float.Parse(strValue) == 0)
            {
                quantityBox.Text = "0";
            }
            else
            {
                quantityBox.Text = (float.Parse(totalQuantity.Text) / float.Parse(boxQuantity.Text)).ToString();
            }
        }

        public int getStatus()
        {
            return status;
        }

        public void setCaption(string caption)
        {
            label5.Text = caption;
        }

        private void boxQuantity_GotFocus(object sender, EventArgs e)
        {
            currentBox = boxQuantity;
            //currentBox.SelectAll();

            Timer timer = new Timer();
            timer.Interval = 10;
            timer.Tick += new EventHandler(timer_SelectAll);
            timer.Enabled = true;
        }

        private void quantityBox_GotFocus(object sender, EventArgs e)
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
    }
}