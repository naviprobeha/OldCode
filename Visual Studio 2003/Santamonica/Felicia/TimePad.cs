using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for TimePad.
	/// </summary>
	public class TimePad : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox hourBox;
		private System.Windows.Forms.TextBox minuteBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label orderNo3Label;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button6;
	
		private int hour;
		private int minute;
		private System.Windows.Forms.TextBox dateBox;
		private DateTime currentDateTime;

		public TimePad()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			currentDateTime = DateTime.Now;
			dateBox.Text = currentDateTime.ToString("yyyy-MM-dd");

			this.hour = DateTime.Now.Hour;
			this.minute = DateTime.Now.Minute;

			updateForm();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		public void setDateTime(DateTime dateTime)
		{
			currentDateTime = dateTime;
			if (currentDateTime.Year < 2006) currentDateTime = DateTime.Now;

			dateBox.Text = currentDateTime.ToString("yyyy-MM-dd");
			this.hour = currentDateTime.Hour;
			this.minute = currentDateTime.Minute;

			updateForm();
		}

		private void updateForm()
		{
			hourBox.Text = hour.ToString();
			if (hour < 10) hourBox.Text = "0"+hour;

			minuteBox.Text = minute.ToString();
			if (minute < 10) minuteBox.Text = "0"+minute;

		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button5 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.hourBox = new System.Windows.Forms.TextBox();
			this.minuteBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.orderNo3Label = new System.Windows.Forms.Label();
			this.button7 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.dateBox = new System.Windows.Forms.TextBox();
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(48, 80);
			this.button5.Size = new System.Drawing.Size(32, 32);
			this.button5.Text = ">";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(8, 80);
			this.button4.Size = new System.Drawing.Size(32, 32);
			this.button4.Text = "<";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 32);
			this.label1.Size = new System.Drawing.Size(112, 20);
			this.label1.Text = "Datum:";
			// 
			// hourBox
			// 
			this.hourBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
			this.hourBox.Location = new System.Drawing.Point(152, 48);
			this.hourBox.Size = new System.Drawing.Size(72, 26);
			this.hourBox.Text = "";
			// 
			// minuteBox
			// 
			this.minuteBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
			this.minuteBox.Location = new System.Drawing.Point(232, 48);
			this.minuteBox.Size = new System.Drawing.Size(72, 26);
			this.minuteBox.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(152, 32);
			this.label2.Size = new System.Drawing.Size(80, 20);
			this.label2.Text = "Klockslag:";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
			this.label3.Location = new System.Drawing.Point(224, 50);
			this.label3.Size = new System.Drawing.Size(16, 20);
			this.label3.Text = ":";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(192, 80);
			this.button1.Size = new System.Drawing.Size(32, 32);
			this.button1.Text = ">";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(152, 80);
			this.button2.Size = new System.Drawing.Size(32, 32);
			this.button2.Text = "<";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(272, 80);
			this.button3.Size = new System.Drawing.Size(32, 32);
			this.button3.Text = ">";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button6
			// 
			this.button6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button6.Location = new System.Drawing.Point(232, 80);
			this.button6.Size = new System.Drawing.Size(32, 32);
			this.button6.Text = "<";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// orderNo3Label
			// 
			this.orderNo3Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.orderNo3Label.Location = new System.Drawing.Point(5, 3);
			this.orderNo3Label.Size = new System.Drawing.Size(251, 20);
			this.orderNo3Label.Text = "Datum och klockslag";
			// 
			// button7
			// 
			this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button7.Location = new System.Drawing.Point(96, 176);
			this.button7.Size = new System.Drawing.Size(104, 32);
			this.button7.Text = "Avbryt";
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// button8
			// 
			this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button8.Location = new System.Drawing.Point(208, 176);
			this.button8.Size = new System.Drawing.Size(104, 32);
			this.button8.Text = "OK";
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// dateBox
			// 
			this.dateBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular);
			this.dateBox.Location = new System.Drawing.Point(8, 48);
			this.dateBox.Size = new System.Drawing.Size(136, 26);
			this.dateBox.Text = "";
			// 
			// TimePad
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.dateBox);
			this.Controls.Add(this.button8);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.orderNo3Label);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.minuteBox);
			this.Controls.Add(this.hourBox);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Text = "Datum och klockslag";

		}
		#endregion

		private void button7_Click(object sender, System.EventArgs e)
		{
			currentDateTime = new DateTime(1753, 1, 1, 0, 0, 0);
			this.Close();
		}

		private void button8_Click(object sender, System.EventArgs e)
		{
			currentDateTime = DateTime.Parse(dateBox.Text+" "+hourBox.Text+":"+minuteBox.Text+":00");		
			this.Close();
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			currentDateTime = currentDateTime.AddDays(-1);
			dateBox.Text = currentDateTime.ToString("yyyy-MM-dd");

		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			currentDateTime = currentDateTime.AddDays(1);
			dateBox.Text = currentDateTime.ToString("yyyy-MM-dd");
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			hour = hour - 1;
			if (hour < 0) hour = 23;

			updateForm();
		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			minute = minute - 1;
			if (minute < 0) minute = 59;

			updateForm();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			hour = hour + 1;
			if (hour > 23) hour = 0;

			updateForm();
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			minute = minute + 1;
			if (minute > 59) minute = 0;

			updateForm();

		}

		public DateTime getDateTime()
		{
			return currentDateTime;			
		}
	}
}
