using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for NewOrder.
	/// </summary>
	public class NewOrder : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label orderNo3Label;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button8;
	
		private int status;

		public NewOrder()
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
			this.button8 = new System.Windows.Forms.Button();
			this.orderNo3Label = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(208, 48);
			this.button1.Size = new System.Drawing.Size(104, 32);
			this.button1.Text = "Körorder";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button8
			// 
			this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button8.Location = new System.Drawing.Point(208, 120);
			this.button8.Size = new System.Drawing.Size(104, 32);
			this.button8.Text = "Följesedel";
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// orderNo3Label
			// 
			this.orderNo3Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.orderNo3Label.Location = new System.Drawing.Point(5, 3);
			this.orderNo3Label.Size = new System.Drawing.Size(251, 20);
			this.orderNo3Label.Text = "Ny order";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 32);
			this.label1.Size = new System.Drawing.Size(192, 56);
			this.label1.Text = "En körorder är ett underlag för det som skall hämtas. Det går också att skicka vi" +
				"dare en körorder till en annan bil.";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 104);
			this.label2.Size = new System.Drawing.Size(192, 56);
			this.label2.Text = "En följesedel speglar något som blivit hämtat. Följesedeln skickas direkt vidare " +
				"till kontoret efter klarmarkering.";
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(208, 176);
			this.button2.Size = new System.Drawing.Size(104, 32);
			this.button2.Text = "Avbryt";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// NewOrder
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.orderNo3Label);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.button8);
			this.Text = "Ny order";

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			status = 0;
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			status = 1;
			this.Close();
		}

		private void button8_Click(object sender, System.EventArgs e)
		{
			status = 2;
			this.Close();
		}

		public int getStatus()
		{
			return status;
		}

	}
}
