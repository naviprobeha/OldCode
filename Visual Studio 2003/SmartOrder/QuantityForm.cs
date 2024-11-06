using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartOrder
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
		private System.Windows.Forms.Button button12;
		private System.Windows.Forms.Button button14;

		private int status;

		private DataItem dataItem;
		private DataColor dataColor;
		private DataSize dataSize;
		private System.Windows.Forms.TextBox colorBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox descriptionBox;
		private System.Windows.Forms.TextBox itemNoBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox size2Box;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox sizeBox;
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
			colorBox.Text = dataColor.code;
			sizeBox.Text = dataSize.code;
			
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
			colorBox.Text = dataColor.code;

			sizeBox.Visible = false;
			size2Box.Visible = false;
			label5.Visible = false;
			label4.Visible = false;

			quantityBox.Top = 54;
			quantityBox.Left = 8;
			quantityBox.Width = 224;
			label6.Top = 38;
			label6.Left = 8;
			label6.Width = 200;

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
	
			colorBox.Visible = false;
			sizeBox.Visible = false;
			size2Box.Visible = false;
			label5.Visible = false;
			label3.Visible = false;
			label4.Visible = false;

			descriptionBox.Width = 152;
			quantityBox.Top = 54;
			quantityBox.Left = 8;
			quantityBox.Width = 224;
			label6.Top = 38;
			label6.Left = 8;

	
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
			this.button12 = new System.Windows.Forms.Button();
			this.colorBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.descriptionBox = new System.Windows.Forms.TextBox();
			this.itemNoBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.size2Box = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.sizeBox = new System.Windows.Forms.TextBox();
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
			this.button11.Location = new System.Drawing.Point(152, 176);
			this.button11.Size = new System.Drawing.Size(80, 40);
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
			this.quantityBox.Location = new System.Drawing.Point(152, 54);
			this.quantityBox.Size = new System.Drawing.Size(80, 20);
			this.quantityBox.Text = "";
			// 
			// button12
			// 
			this.button12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
			this.button12.Location = new System.Drawing.Point(8, 224);
			this.button12.Size = new System.Drawing.Size(40, 40);
			this.button12.Text = "CL";
			this.button12.Click += new System.EventHandler(this.button12_Click);
			// 
			// colorBox
			// 
			this.colorBox.Location = new System.Drawing.Point(192, 16);
			this.colorBox.ReadOnly = true;
			this.colorBox.Size = new System.Drawing.Size(40, 20);
			this.colorBox.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(192, 0);
			this.label3.Size = new System.Drawing.Size(40, 16);
			this.label3.Text = "Färg:";
			// 
			// descriptionBox
			// 
			this.descriptionBox.Location = new System.Drawing.Point(80, 16);
			this.descriptionBox.ReadOnly = true;
			this.descriptionBox.Size = new System.Drawing.Size(104, 20);
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
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 38);
			this.label4.Size = new System.Drawing.Size(56, 16);
			this.label4.Text = "Storlek:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(80, 38);
			this.label5.Size = new System.Drawing.Size(56, 16);
			this.label5.Text = "Kupa:";
			// 
			// size2Box
			// 
			this.size2Box.Location = new System.Drawing.Point(80, 54);
			this.size2Box.ReadOnly = true;
			this.size2Box.Size = new System.Drawing.Size(64, 20);
			this.size2Box.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(152, 38);
			this.label6.Size = new System.Drawing.Size(32, 16);
			this.label6.Text = "Antal:";
			// 
			// sizeBox
			// 
			this.sizeBox.Location = new System.Drawing.Point(8, 54);
			this.sizeBox.ReadOnly = true;
			this.sizeBox.Size = new System.Drawing.Size(64, 20);
			this.sizeBox.Text = "";
			// 
			// QuantityForm
			// 
			this.Controls.Add(this.sizeBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.size2Box);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.colorBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.descriptionBox);
			this.Controls.Add(this.itemNoBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button12);
			this.Controls.Add(this.quantityBox);
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
			if (quantityBox.Text == "") quantityBox.Text = "0";
			quantityBox.SelectAll();
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
			appendButton(",");
		}

		private void button12_Click(object sender, System.EventArgs e)
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
					quantityBox.Text = quantityBox.Text.Substring(0,quantityBox.Text.Length-1);		
				}
				else
				{
					quantityBox.Text = quantityBox.Text + buttonStr;
				}
			}
		}
	}
}
