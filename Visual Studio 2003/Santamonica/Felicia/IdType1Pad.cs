using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for IdType1Pad.
	/// </summary>
	public class IdType1Pad : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button44;
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
		private System.Windows.Forms.TextBox prefixBox;
		private System.Windows.Forms.TextBox prodNoBox;
		private System.Windows.Forms.TextBox idNoBox;
		private System.Windows.Forms.TextBox controlNoBox;
		private System.Windows.Forms.Button button32;
	
		private string inputString;
		private System.Windows.Forms.TextBox currentBox;
		private int maxLength;

		public IdType1Pad()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.inputString = "";
			currentBox = idNoBox;
			maxLength = 4;

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
			this.button44 = new System.Windows.Forms.Button();
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
			this.prefixBox = new System.Windows.Forms.TextBox();
			this.prodNoBox = new System.Windows.Forms.TextBox();
			this.idNoBox = new System.Windows.Forms.TextBox();
			this.controlNoBox = new System.Windows.Forms.TextBox();
			// 
			// button44
			// 
			this.button44.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button44.Location = new System.Drawing.Point(184, 96);
			this.button44.Size = new System.Drawing.Size(48, 40);
			this.button44.Text = "-";
			// 
			// button43
			// 
			this.button43.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button43.Location = new System.Drawing.Point(240, 144);
			this.button43.Size = new System.Drawing.Size(72, 40);
			this.button43.Text = "OK";
			this.button43.Click += new System.EventHandler(this.button43_Click);
			// 
			// button42
			// 
			this.button42.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button42.Location = new System.Drawing.Point(184, 48);
			this.button42.Size = new System.Drawing.Size(48, 40);
			this.button42.Text = "BS";
			this.button42.Click += new System.EventHandler(this.button42_Click);
			// 
			// button41
			// 
			this.button41.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button41.Location = new System.Drawing.Point(184, 144);
			this.button41.Size = new System.Drawing.Size(48, 40);
			this.button41.Text = "0";
			this.button41.Click += new System.EventHandler(this.button41_Click);
			// 
			// button40
			// 
			this.button40.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button40.Location = new System.Drawing.Point(128, 144);
			this.button40.Size = new System.Drawing.Size(48, 40);
			this.button40.Text = "3";
			this.button40.Click += new System.EventHandler(this.button40_Click);
			// 
			// button39
			// 
			this.button39.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button39.Location = new System.Drawing.Point(72, 144);
			this.button39.Size = new System.Drawing.Size(48, 40);
			this.button39.Text = "2";
			this.button39.Click += new System.EventHandler(this.button39_Click);
			// 
			// button38
			// 
			this.button38.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button38.Location = new System.Drawing.Point(128, 96);
			this.button38.Size = new System.Drawing.Size(48, 40);
			this.button38.Text = "6";
			this.button38.Click += new System.EventHandler(this.button38_Click);
			// 
			// button37
			// 
			this.button37.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button37.Location = new System.Drawing.Point(72, 96);
			this.button37.Size = new System.Drawing.Size(48, 40);
			this.button37.Text = "5";
			this.button37.Click += new System.EventHandler(this.button37_Click);
			// 
			// button36
			// 
			this.button36.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button36.Location = new System.Drawing.Point(128, 48);
			this.button36.Size = new System.Drawing.Size(48, 40);
			this.button36.Text = "9";
			this.button36.Click += new System.EventHandler(this.button36_Click);
			// 
			// button35
			// 
			this.button35.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button35.Location = new System.Drawing.Point(72, 48);
			this.button35.Size = new System.Drawing.Size(48, 40);
			this.button35.Text = "8";
			this.button35.Click += new System.EventHandler(this.button35_Click);
			// 
			// button34
			// 
			this.button34.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button34.Location = new System.Drawing.Point(16, 144);
			this.button34.Size = new System.Drawing.Size(48, 40);
			this.button34.Text = "1";
			this.button34.Click += new System.EventHandler(this.button34_Click);
			// 
			// button33
			// 
			this.button33.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button33.Location = new System.Drawing.Point(16, 96);
			this.button33.Size = new System.Drawing.Size(48, 40);
			this.button33.Text = "4";
			this.button33.Click += new System.EventHandler(this.button33_Click);
			// 
			// button32
			// 
			this.button32.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button32.Location = new System.Drawing.Point(16, 48);
			this.button32.Size = new System.Drawing.Size(48, 40);
			this.button32.Text = "7";
			this.button32.Click += new System.EventHandler(this.button32_Click);
			// 
			// prefixBox
			// 
			this.prefixBox.Location = new System.Drawing.Point(16, 16);
			this.prefixBox.ReadOnly = true;
			this.prefixBox.Size = new System.Drawing.Size(24, 20);
			this.prefixBox.Text = "SE";
			this.prefixBox.GotFocus += new System.EventHandler(this.prefixBox_GotFocus);
			// 
			// prodNoBox
			// 
			this.prodNoBox.Location = new System.Drawing.Point(48, 16);
			this.prodNoBox.Size = new System.Drawing.Size(64, 20);
			this.prodNoBox.Text = "";
			this.prodNoBox.GotFocus += new System.EventHandler(this.prodNoBox_GotFocus);
			// 
			// idNoBox
			// 
			this.idNoBox.Location = new System.Drawing.Point(120, 16);
			this.idNoBox.Size = new System.Drawing.Size(48, 20);
			this.idNoBox.Text = "";
			this.idNoBox.GotFocus += new System.EventHandler(this.idNoBox_GotFocus);
			// 
			// controlNoBox
			// 
			this.controlNoBox.Location = new System.Drawing.Point(176, 16);
			this.controlNoBox.Size = new System.Drawing.Size(24, 20);
			this.controlNoBox.Text = "";
			this.controlNoBox.GotFocus += new System.EventHandler(this.controlNoBox_GotFocus);
			// 
			// IdType1Pad
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.controlNoBox);
			this.Controls.Add(this.idNoBox);
			this.Controls.Add(this.prodNoBox);
			this.Controls.Add(this.prefixBox);
			this.Controls.Add(this.button44);
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
			this.Text = "ID-nr Nöt/Får";

		}
		#endregion

		private void onButtonPress(string ch)
		{
			if (ch == "BS") 
			{
				if (currentBox.Text.Length > 0)
				{
					currentBox.Text = currentBox.Text.Substring(0, currentBox.Text.Length-1);
				}
			}
			else
			{
				if (currentBox.Text == "") 
				{
					currentBox.Text = ch;
				}
				else
				{
					if (currentBox.Text.Length < maxLength)
					{
						currentBox.Text = currentBox.Text + ch;
					}
				}
			}

			inputString = prefixBox.Text+prodNoBox.Text+"-"+idNoBox.Text+"-"+controlNoBox.Text;

		}

		public string getInputString()
		{
			return inputString;
		}

		public void setInputString(string inputString)
		{
			this.inputString = inputString;
			
			string prodNo = "";
			string idNo = "";
			string controlNo = "";

			splitType1Id(inputString, out prodNo, out idNo, out controlNo);

			prodNoBox.Text = prodNo;
			idNoBox.Text = idNo;
			controlNoBox.Text = controlNo;

		}

		private void prefixBox_GotFocus(object sender, System.EventArgs e)
		{

		}

		private void prodNoBox_GotFocus(object sender, System.EventArgs e)
		{
			currentBox = prodNoBox;
			maxLength = 6;
		}

		private void idNoBox_GotFocus(object sender, System.EventArgs e)
		{
			currentBox = idNoBox;
			maxLength = 4;
		}

		private void controlNoBox_GotFocus(object sender, System.EventArgs e)
		{
			currentBox = controlNoBox;
			maxLength = 2;
		}

		public void splitType1Id(string inputId, out string prodNo, out string idNo, out string controlNo)
		{
			prodNo = "";
			idNo = "";
			controlNo = "";

			if (inputId.Substring(0, 2).ToUpper() == "SE") inputId = inputId.Substring(2);
			if (inputId.Substring(0, 1) == " ") inputId = inputId.Substring(1);
			if (inputId.IndexOf("-") == -1)
			{
				prodNo = inputId;
				return;
			}
			if (inputId.IndexOf("-") > -1)
			{
				prodNo = inputId.Substring(0, inputId.IndexOf("-"));
				inputId = inputId.Substring(inputId.IndexOf("-") + 1);
			}
			if (inputId.IndexOf("-") == -1)
			{
				idNo = inputId;
				return;
			}
			if (inputId.IndexOf("-") > -1)
			{
				idNo = inputId.Substring(0, inputId.IndexOf("-"));
				inputId = inputId.Substring(inputId.IndexOf("-") + 1);
			}
			controlNo = inputId;
		}

		private void button43_Click(object sender, System.EventArgs e)
		{
			this.Close();
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

		private void button42_Click(object sender, System.EventArgs e)
		{
			onButtonPress("BS");	
		}

		private void button41_Click(object sender, System.EventArgs e)
		{
			onButtonPress("0");	
		}


	}
}
