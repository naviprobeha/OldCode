using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartShipment
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class QuantityForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.Button button10;
		private System.Windows.Forms.Button button11;
		private System.Windows.Forms.Button button13;
		private System.Windows.Forms.TextBox quantityBox;
		private System.Windows.Forms.Button button14;

		private int status;
		private System.Windows.Forms.TextBox currentBox;
		private DataItem dataItem;
		private float grossPrice;

		private System.Windows.Forms.TextBox descriptionBox;
		private System.Windows.Forms.TextBox itemNoBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox unitPriceBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox totalBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox discountBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox grossPriceBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button button12;


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

			setUnitPrice(dataItem.price);
			setDiscount(dataItem.discount);
	

			currentBox = quantityBox;	

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.button9 = new System.Windows.Forms.Button();
			this.button10 = new System.Windows.Forms.Button();
			this.button11 = new System.Windows.Forms.Button();
			this.button13 = new System.Windows.Forms.Button();
			this.button14 = new System.Windows.Forms.Button();
			this.quantityBox = new System.Windows.Forms.TextBox();
			this.descriptionBox = new System.Windows.Forms.TextBox();
			this.itemNoBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.button12 = new System.Windows.Forms.Button();
			this.unitPriceBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.totalBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.discountBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.grossPriceBox = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
			this.button1.Location = new System.Drawing.Point(8, 176);
			this.button1.Size = new System.Drawing.Size(40, 40);
			this.button1.Text = "1";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
			this.button2.Location = new System.Drawing.Point(56, 176);
			this.button2.Size = new System.Drawing.Size(40, 40);
			this.button2.Text = "2";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
			this.button3.Location = new System.Drawing.Point(104, 176);
			this.button3.Size = new System.Drawing.Size(40, 40);
			this.button3.Text = "3";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
			this.button4.Location = new System.Drawing.Point(8, 128);
			this.button4.Size = new System.Drawing.Size(40, 40);
			this.button4.Text = "4";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
			this.button5.Location = new System.Drawing.Point(56, 128);
			this.button5.Size = new System.Drawing.Size(40, 40);
			this.button5.Text = "5";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button6
			// 
			this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
			this.button6.Location = new System.Drawing.Point(104, 128);
			this.button6.Size = new System.Drawing.Size(40, 40);
			this.button6.Text = "6";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button7
			// 
			this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
			this.button7.Location = new System.Drawing.Point(8, 80);
			this.button7.Size = new System.Drawing.Size(40, 40);
			this.button7.Text = "7";
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// button8
			// 
			this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
			this.button8.Location = new System.Drawing.Point(56, 80);
			this.button8.Size = new System.Drawing.Size(40, 40);
			this.button8.Text = "8";
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// button9
			// 
			this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
			this.button9.Location = new System.Drawing.Point(104, 80);
			this.button9.Size = new System.Drawing.Size(40, 40);
			this.button9.Text = "9";
			this.button9.Click += new System.EventHandler(this.button9_Click);
			// 
			// button10
			// 
			this.button10.Location = new System.Drawing.Point(152, 224);
			this.button10.Size = new System.Drawing.Size(80, 40);
			this.button10.Text = "OK";
			this.button10.Click += new System.EventHandler(this.button10_Click);
			// 
			// button11
			// 
			this.button11.Location = new System.Drawing.Point(152, 192);
			this.button11.Size = new System.Drawing.Size(80, 24);
			this.button11.Text = "Avbryt";
			this.button11.Click += new System.EventHandler(this.button11_Click);
			// 
			// button13
			// 
			this.button13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
			this.button13.Location = new System.Drawing.Point(56, 224);
			this.button13.Size = new System.Drawing.Size(40, 40);
			this.button13.Text = "0";
			this.button13.Click += new System.EventHandler(this.button13_Click);
			// 
			// button14
			// 
			this.button14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
			this.button14.Location = new System.Drawing.Point(104, 224);
			this.button14.Size = new System.Drawing.Size(40, 40);
			this.button14.Text = ",";
			this.button14.Click += new System.EventHandler(this.button14_Click);
			// 
			// quantityBox
			// 
			this.quantityBox.Location = new System.Drawing.Point(8, 54);
			this.quantityBox.ReadOnly = true;
			this.quantityBox.Size = new System.Drawing.Size(64, 20);
			this.quantityBox.Text = "";
			this.quantityBox.LostFocus += new System.EventHandler(this.quantityBox_LostFocus);
			this.quantityBox.GotFocus += new System.EventHandler(this.quantityBox_GotFocus);
			this.quantityBox.TextChanged += new System.EventHandler(this.quantityBox_TextChanged);
			// 
			// descriptionBox
			// 
			this.descriptionBox.Location = new System.Drawing.Point(80, 16);
			this.descriptionBox.ReadOnly = true;
			this.descriptionBox.Size = new System.Drawing.Size(152, 20);
			this.descriptionBox.Text = "";
			// 
			// itemNoBox
			// 
			this.itemNoBox.Location = new System.Drawing.Point(8, 16);
			this.itemNoBox.ReadOnly = true;
			this.itemNoBox.Size = new System.Drawing.Size(64, 20);
			this.itemNoBox.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(80, 0);
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.Text = "Beskrivning:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 0);
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.Text = "Artikelnr:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 40);
			this.label6.Size = new System.Drawing.Size(32, 16);
			this.label6.Text = "Antal:";
			// 
			// button12
			// 
			this.button12.Location = new System.Drawing.Point(152, 160);
			this.button12.Size = new System.Drawing.Size(80, 24);
			this.button12.Text = "CL";
			this.button12.Click += new System.EventHandler(this.button12_Click);
			// 
			// unitPriceBox
			// 
			this.unitPriceBox.Location = new System.Drawing.Point(152, 54);
			this.unitPriceBox.ReadOnly = true;
			this.unitPriceBox.Size = new System.Drawing.Size(80, 20);
			this.unitPriceBox.Text = "";
			this.unitPriceBox.GotFocus += new System.EventHandler(this.unitPriceBox_GotFocus);
			this.unitPriceBox.TextChanged += new System.EventHandler(this.unitPriceBox_TextChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(152, 40);
			this.label3.Size = new System.Drawing.Size(72, 16);
			this.label3.Text = "A-pris Netto:";
			// 
			// totalBox
			// 
			this.totalBox.Location = new System.Drawing.Point(152, 136);
			this.totalBox.ReadOnly = true;
			this.totalBox.Size = new System.Drawing.Size(80, 20);
			this.totalBox.Text = "";
			this.totalBox.GotFocus += new System.EventHandler(this.totalBox_GotFocus);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(152, 120);
			this.label4.Size = new System.Drawing.Size(40, 16);
			this.label4.Text = "Totalt:";
			// 
			// discountBox
			// 
			this.discountBox.Location = new System.Drawing.Point(152, 96);
			this.discountBox.ReadOnly = true;
			this.discountBox.Size = new System.Drawing.Size(80, 20);
			this.discountBox.Text = "";
			this.discountBox.GotFocus += new System.EventHandler(this.discountBox_GotFocus);
			this.discountBox.TextChanged += new System.EventHandler(this.discountBox_TextChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(152, 80);
			this.label5.Size = new System.Drawing.Size(64, 16);
			this.label5.Text = "Rabatt (%):";
			// 
			// grossPriceBox
			// 
			this.grossPriceBox.Location = new System.Drawing.Point(80, 54);
			this.grossPriceBox.ReadOnly = true;
			this.grossPriceBox.Size = new System.Drawing.Size(64, 20);
			this.grossPriceBox.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(80, 40);
			this.label7.Size = new System.Drawing.Size(40, 16);
			this.label7.Text = "A-pris:";
			// 
			// QuantityForm
			// 
			this.Controls.Add(this.grossPriceBox);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.discountBox);
			this.Controls.Add(this.totalBox);
			this.Controls.Add(this.unitPriceBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.quantityBox);
			this.Controls.Add(this.button12);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.descriptionBox);
			this.Controls.Add(this.itemNoBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button14);
			this.Controls.Add(this.button13);
			this.Controls.Add(this.button11);
			this.Controls.Add(this.button10);
			this.Controls.Add(this.button9);
			this.Controls.Add(this.button8);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label5);
			this.Menu = this.mainMenu1;
			this.Text = "SmartOrder - Antal";
			this.Load += new System.EventHandler(this.Form1_Load);

		}
		#endregion

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
					currentBox.Text = currentBox.Text.Substring(0,currentBox.Text.Length-1);		
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

		private void quantityBox_LostFocus(object sender, System.EventArgs e)
		{

		}

		private void unitPriceBox_TextChanged(object sender, System.EventArgs e)
		{
		}

		private void discountBox_TextChanged(object sender, System.EventArgs e)
		{		
		}
	}
}
