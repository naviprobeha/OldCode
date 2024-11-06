using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for NumPad.
	/// </summary>
	public class NumPad : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button43;
		private System.Windows.Forms.Button button42;
		private System.Windows.Forms.Button button41;
		private System.Windows.Forms.Button button40;
		private System.Windows.Forms.Button button39;
		private System.Windows.Forms.Button button38;
		private System.Windows.Forms.Button button37;
		private System.Windows.Forms.Button button36;
		private System.Windows.Forms.Button button35;
		private System.Windows.Forms.Button button34;
		private System.Windows.Forms.Button button33;
		private System.Windows.Forms.Button button32;
		private System.Windows.Forms.TextBox inputBox;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
	
		private string inputString;

		public NumPad()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public NumPad(bool decimalValue)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			if (decimalValue)
			{
				button41.Height = 40;
				button2.Location = new Point(184, 160);
				button2.Visible = true;
			}
			else
			{
				button2.Visible = false;
			}

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
			this.button43 = new System.Windows.Forms.Button();
			this.button42 = new System.Windows.Forms.Button();
			this.button41 = new System.Windows.Forms.Button();
			this.button40 = new System.Windows.Forms.Button();
			this.button39 = new System.Windows.Forms.Button();
			this.button38 = new System.Windows.Forms.Button();
			this.button37 = new System.Windows.Forms.Button();
			this.button36 = new System.Windows.Forms.Button();
			this.button35 = new System.Windows.Forms.Button();
			this.button34 = new System.Windows.Forms.Button();
			this.button33 = new System.Windows.Forms.Button();
			this.button32 = new System.Windows.Forms.Button();
			this.inputBox = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			// 
			// button43
			// 
			this.button43.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button43.Location = new System.Drawing.Point(240, 160);
			this.button43.Size = new System.Drawing.Size(72, 40);
			this.button43.Text = "OK";
			this.button43.Click += new System.EventHandler(this.button43_Click);
			// 
			// button42
			// 
			this.button42.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button42.Location = new System.Drawing.Point(184, 64);
			this.button42.Size = new System.Drawing.Size(48, 40);
			this.button42.Text = "BS";
			this.button42.Click += new System.EventHandler(this.button42_Click);
			// 
			// button41
			// 
			this.button41.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button41.Location = new System.Drawing.Point(184, 112);
			this.button41.Size = new System.Drawing.Size(48, 88);
			this.button41.Text = "0";
			this.button41.Click += new System.EventHandler(this.button41_Click);
			// 
			// button40
			// 
			this.button40.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button40.Location = new System.Drawing.Point(128, 160);
			this.button40.Size = new System.Drawing.Size(48, 40);
			this.button40.Text = "3";
			this.button40.Click += new System.EventHandler(this.button40_Click);
			// 
			// button39
			// 
			this.button39.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button39.Location = new System.Drawing.Point(72, 160);
			this.button39.Size = new System.Drawing.Size(48, 40);
			this.button39.Text = "2";
			this.button39.Click += new System.EventHandler(this.button39_Click);
			// 
			// button38
			// 
			this.button38.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button38.Location = new System.Drawing.Point(128, 112);
			this.button38.Size = new System.Drawing.Size(48, 40);
			this.button38.Text = "6";
			this.button38.Click += new System.EventHandler(this.button38_Click);
			// 
			// button37
			// 
			this.button37.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button37.Location = new System.Drawing.Point(72, 112);
			this.button37.Size = new System.Drawing.Size(48, 40);
			this.button37.Text = "5";
			this.button37.Click += new System.EventHandler(this.button37_Click);
			// 
			// button36
			// 
			this.button36.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button36.Location = new System.Drawing.Point(128, 64);
			this.button36.Size = new System.Drawing.Size(48, 40);
			this.button36.Text = "9";
			this.button36.Click += new System.EventHandler(this.button36_Click);
			// 
			// button35
			// 
			this.button35.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button35.Location = new System.Drawing.Point(72, 64);
			this.button35.Size = new System.Drawing.Size(48, 40);
			this.button35.Text = "8";
			this.button35.Click += new System.EventHandler(this.button35_Click);
			// 
			// button34
			// 
			this.button34.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button34.Location = new System.Drawing.Point(16, 160);
			this.button34.Size = new System.Drawing.Size(48, 40);
			this.button34.Text = "1";
			this.button34.Click += new System.EventHandler(this.button34_Click);
			// 
			// button33
			// 
			this.button33.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button33.Location = new System.Drawing.Point(16, 112);
			this.button33.Size = new System.Drawing.Size(48, 40);
			this.button33.Text = "4";
			this.button33.Click += new System.EventHandler(this.button33_Click);
			// 
			// button32
			// 
			this.button32.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button32.Location = new System.Drawing.Point(16, 64);
			this.button32.Size = new System.Drawing.Size(48, 40);
			this.button32.Text = "7";
			this.button32.Click += new System.EventHandler(this.button32_Click);
			// 
			// inputBox
			// 
			this.inputBox.Location = new System.Drawing.Point(16, 16);
			this.inputBox.Size = new System.Drawing.Size(296, 20);
			this.inputBox.Text = "";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(240, 112);
			this.button1.Size = new System.Drawing.Size(72, 40);
			this.button1.Text = "Avbryt";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(240, 64);
			this.button2.Size = new System.Drawing.Size(48, 40);
			this.button2.Text = ",";
			this.button2.Visible = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// NumPad
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.inputBox);
			this.Controls.Add(this.button43);
			this.Controls.Add(this.button42);
			this.Controls.Add(this.button41);
			this.Controls.Add(this.button40);
			this.Controls.Add(this.button39);
			this.Controls.Add(this.button38);
			this.Controls.Add(this.button37);
			this.Controls.Add(this.button36);
			this.Controls.Add(this.button35);
			this.Controls.Add(this.button34);
			this.Controls.Add(this.button33);
			this.Controls.Add(this.button32);
			this.Text = "Tangentbord";

		}
		#endregion

		private void onButtonPress(string ch)
		{
			if (ch == "BS") 
			{
				if (inputString.Length > 0)
				{
					inputString = inputString.Substring(0, inputString.Length-1);
				}
			}
			else
			{
				if (inputString == "0")
				{
					inputString = ch;
				}
				else
				{
					inputString = inputString + ch;
				}
			}

			inputBox.Text = inputString;
		}

		public string getInputString()
		{
			//return inputString;
			return inputBox.Text;
		}


		private void button34_Click(object sender, System.EventArgs e)
		{
			onButtonPress("1");
		}

		private void button39_Click(object sender, System.EventArgs e)
		{
			onButtonPress("2");
		}

		private void button40_Click(object sender, System.EventArgs e)
		{
			onButtonPress("3");
		}

		private void button33_Click(object sender, System.EventArgs e)
		{
			onButtonPress("4");
		}

		private void button37_Click(object sender, System.EventArgs e)
		{
			onButtonPress("5");
		}

		private void button38_Click(object sender, System.EventArgs e)
		{
			onButtonPress("6");
		}

		private void button32_Click(object sender, System.EventArgs e)
		{
			onButtonPress("7");
		}

		private void button35_Click(object sender, System.EventArgs e)
		{
			onButtonPress("8");
		}

		private void button36_Click(object sender, System.EventArgs e)
		{
			onButtonPress("9");
		}

		private void button41_Click(object sender, System.EventArgs e)
		{
			onButtonPress("0");
		}

		private void button42_Click(object sender, System.EventArgs e)
		{
			onButtonPress("BS");
		}

		private void button43_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.inputString = "";
			this.Close();
		}

		public void setInputString(string inpString)
		{
			inputBox.Text = inpString;
			inputString = "";
			//inputString = inputBox.Text;
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			onButtonPress(".");
		}

	}
}
