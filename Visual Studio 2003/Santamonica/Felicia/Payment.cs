using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for Payment.
	/// </summary>
	public class Payment : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label orderNo3Label;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button3;

		private int paymentMethod;
	
		public Payment()
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
			this.button2 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.orderNo3Label = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(208, 176);
			this.button2.Size = new System.Drawing.Size(104, 32);
			this.button2.Text = "Avbryt";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(11, 107);
			this.label2.Size = new System.Drawing.Size(192, 56);
			this.label2.Text = "Om kunden är tvingad att betala kontant, visas endast kontant- och kort-valen.";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(11, 35);
			this.label1.Size = new System.Drawing.Size(192, 56);
			this.label1.Text = "Välj betalsätt genom att klicka på knapparna till höger.";
			// 
			// orderNo3Label
			// 
			this.orderNo3Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.orderNo3Label.Location = new System.Drawing.Point(8, 6);
			this.orderNo3Label.Size = new System.Drawing.Size(251, 20);
			this.orderNo3Label.Text = "Välj betalsätt";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(208, 120);
			this.button1.Size = new System.Drawing.Size(104, 32);
			this.button1.Text = "Faktura";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button8
			// 
			this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button8.Location = new System.Drawing.Point(208, 40);
			this.button8.Size = new System.Drawing.Size(104, 32);
			this.button8.Text = "Kontant";
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(208, 80);
			this.button3.Size = new System.Drawing.Size(104, 32);
			this.button3.Text = "Kort";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// Payment
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.orderNo3Label);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.button8);
			this.Text = "Betalsätt";

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		public int getPaymentMethod()
		{

			return paymentMethod;
		}

		public void setPaymentMethod(int paymentMethod)
		{
			this.paymentMethod = paymentMethod;
		}

		public void setCashOnly()
		{
			button1.Visible = false;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.paymentMethod = 0;
			this.Close();
		}

		private void button8_Click(object sender, System.EventArgs e)
		{
			this.paymentMethod = 1;
			this.Close();
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			this.paymentMethod = 2;
			this.Close();
		}
	}
}
