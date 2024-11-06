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
	public class Form1 : System.Windows.Forms.Form
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
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button12;
		private System.Windows.Forms.Button button14;
	
		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.label1 = new System.Windows.Forms.Label();
			this.button12 = new System.Windows.Forms.Button();
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
			this.button10.Location = new System.Drawing.Point(152, 240);
			this.button10.Size = new System.Drawing.Size(80, 24);
			this.button10.Text = "Nästa";
			this.button10.Click += new System.EventHandler(this.button10_Click);
			// 
			// button11
			// 
			this.button11.Location = new System.Drawing.Point(152, 208);
			this.button11.Size = new System.Drawing.Size(80, 24);
			this.button11.Text = "Föregående";
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
			this.quantityBox.Location = new System.Drawing.Point(8, 32);
			this.quantityBox.Size = new System.Drawing.Size(136, 20);
			this.quantityBox.Text = "";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Size = new System.Drawing.Size(152, 16);
			this.label1.Text = "Fyll i antalet nedan.";
			// 
			// button12
			// 
			this.button12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
			this.button12.Location = new System.Drawing.Point(8, 224);
			this.button12.Size = new System.Drawing.Size(40, 40);
			this.button12.Text = "CL";
			this.button12.Click += new System.EventHandler(this.button12_Click);
			// 
			// Form1
			// 
			this.Controls.Add(this.button12);
			this.Controls.Add(this.label1);
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
			if (quantityBox.Text == "0") quantityBox.Text = "";
			quantityBox.Text = quantityBox.Text + '3';		
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			quantityBox.Text = "0";
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (quantityBox.Text == "0") quantityBox.Text = "";
			quantityBox.Text = quantityBox.Text + '1';
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			if (quantityBox.Text == "0") quantityBox.Text = "";
			quantityBox.Text = quantityBox.Text + '2';		
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			if (quantityBox.Text == "0") quantityBox.Text = "";
			quantityBox.Text = quantityBox.Text + '4';	
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			if (quantityBox.Text == "0") quantityBox.Text = "";
			quantityBox.Text = quantityBox.Text + '5';		
		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			if (quantityBox.Text == "0") quantityBox.Text = "";
			quantityBox.Text = quantityBox.Text + '6';		
		}

		private void button7_Click(object sender, System.EventArgs e)
		{
			if (quantityBox.Text == "0") quantityBox.Text = "";
			quantityBox.Text = quantityBox.Text + '7';		
		}

		private void button8_Click(object sender, System.EventArgs e)
		{
			if (quantityBox.Text == "0") quantityBox.Text = "";
			quantityBox.Text = quantityBox.Text + '8';		
		}

		private void button9_Click(object sender, System.EventArgs e)
		{
			if (quantityBox.Text == "0") quantityBox.Text = "";
			quantityBox.Text = quantityBox.Text + '9';		
		}

		private void button13_Click(object sender, System.EventArgs e)
		{
			if (quantityBox.Text == "0") quantityBox.Text = "";
			quantityBox.Text = quantityBox.Text + '0';		
		}

		private void button14_Click(object sender, System.EventArgs e)
		{
			if (quantityBox.Text == "0") quantityBox.Text = "";
			quantityBox.Text = quantityBox.Text + '.';
		}

		private void button12_Click(object sender, System.EventArgs e)
		{
			quantityBox.Text = quantityBox.Text.Substring(0,quantityBox.Text.Length-1);		
			if (quantityBox.Text == "") quantityBox.Text = "0";
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

		private void button10_Click(object sender, System.EventArgs e)
		{
			if ((quantityBox.Text == "") || (quantityBox.Text == "0")) 
			{
				System.Windows.Forms.MessageBox.Show("Antal kan inte vara 0.");
			}
			else
			{
				status = 1;
				this.Close();
			}				
		}

		private void button11_Click(object sender, System.EventArgs e)
		{
			status = 0;
			this.Close();
		}

	}
}
