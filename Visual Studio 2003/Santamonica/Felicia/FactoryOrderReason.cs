using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for FactoryOrderReason.
	/// </summary>
	public class FactoryOrderReason : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label9;

		private int reasonValue;
		private System.Windows.Forms.Button reason1;
		private System.Windows.Forms.Button reason2;
		private System.Windows.Forms.Button reason0;
		private System.Windows.Forms.Button reason3;
		private System.Windows.Forms.Button reason4;
		private System.Windows.Forms.Button reason5;
		private System.Windows.Forms.Button reason6;
		private System.Windows.Forms.Button reason7;
		private string reasonText;
	
		public FactoryOrderReason()
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
			this.label9 = new System.Windows.Forms.Label();
			this.reason1 = new System.Windows.Forms.Button();
			this.reason2 = new System.Windows.Forms.Button();
			this.reason0 = new System.Windows.Forms.Button();
			this.reason3 = new System.Windows.Forms.Button();
			this.reason4 = new System.Windows.Forms.Button();
			this.reason5 = new System.Windows.Forms.Button();
			this.reason6 = new System.Windows.Forms.Button();
			this.reason7 = new System.Windows.Forms.Button();
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label9.Location = new System.Drawing.Point(5, 3);
			this.label9.Size = new System.Drawing.Size(219, 20);
			this.label9.Text = "Orsak väntetid";
			// 
			// reason1
			// 
			this.reason1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.reason1.Location = new System.Drawing.Point(8, 32);
			this.reason1.Size = new System.Drawing.Size(144, 32);
			this.reason1.Text = "Segt material";
			this.reason1.Click += new System.EventHandler(this.button5_Click);
			// 
			// reason2
			// 
			this.reason2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.reason2.Location = new System.Drawing.Point(8, 72);
			this.reason2.Size = new System.Drawing.Size(144, 32);
			this.reason2.Text = "Rundpumpning";
			this.reason2.Click += new System.EventHandler(this.button1_Click);
			// 
			// reason0
			// 
			this.reason0.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.reason0.Location = new System.Drawing.Point(168, 152);
			this.reason0.Size = new System.Drawing.Size(144, 32);
			this.reason0.Text = "Annan orsak";
			this.reason0.Click += new System.EventHandler(this.button2_Click);
			// 
			// reason3
			// 
			this.reason3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.reason3.Location = new System.Drawing.Point(168, 32);
			this.reason3.Size = new System.Drawing.Size(144, 32);
			this.reason3.Text = "Bil före";
			this.reason3.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// reason4
			// 
			this.reason4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.reason4.Location = new System.Drawing.Point(168, 72);
			this.reason4.Size = new System.Drawing.Size(144, 32);
			this.reason4.Text = "Hög nivå i silo";
			this.reason4.Click += new System.EventHandler(this.reason4_Click);
			// 
			// reason5
			// 
			this.reason5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.reason5.Location = new System.Drawing.Point(8, 112);
			this.reason5.Size = new System.Drawing.Size(144, 32);
			this.reason5.Text = "Blött material";
			this.reason5.Click += new System.EventHandler(this.reason5_Click);
			// 
			// reason6
			// 
			this.reason6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.reason6.Location = new System.Drawing.Point(168, 112);
			this.reason6.Size = new System.Drawing.Size(144, 32);
			this.reason6.Text = "Bomkörning";
			this.reason6.Click += new System.EventHandler(this.reason6_Click);
			// 
			// reason7
			// 
			this.reason7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.reason7.Location = new System.Drawing.Point(8, 152);
			this.reason7.Size = new System.Drawing.Size(144, 32);
			this.reason7.Text = "Rengöring tank";
			this.reason7.Click += new System.EventHandler(this.reason7_Click);
			// 
			// FactoryOrderReason
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.reason7);
			this.Controls.Add(this.reason6);
			this.Controls.Add(this.reason5);
			this.Controls.Add(this.reason4);
			this.Controls.Add(this.reason3);
			this.Controls.Add(this.reason0);
			this.Controls.Add(this.reason2);
			this.Controls.Add(this.reason1);
			this.Controls.Add(this.label9);
			this.Text = "FactoryOrderReason";

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			Keyboard keyboard = new Keyboard(100);
			keyboard.setHeaderText("Ange orsak");
			keyboard.ShowDialog();
			
			this.reasonValue = 0;
			this.reasonText = keyboard.getInputString();
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.reasonValue = 2;
			this.reasonText = reason2.Text;
			this.Close();
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			this.reasonValue = 1;
			this.reasonText = reason1.Text;
			this.Close();

		}

		public int getReasonValue()
		{
			return reasonValue;
		}

		public string getReasonText()
		{
			return reasonText;
		}

		private void button1_Click_1(object sender, System.EventArgs e)
		{
			this.reasonValue = 3;
			this.reasonText = reason3.Text;
			this.Close();
	
		}

		private void reason4_Click(object sender, System.EventArgs e)
		{
			this.reasonValue = 4;
			this.reasonText = reason4.Text;
			this.Close();

		}

		private void reason5_Click(object sender, System.EventArgs e)
		{
			this.reasonValue = 5;
			this.reasonText = reason5.Text;
			this.Close();

		}

		private void reason6_Click(object sender, System.EventArgs e)
		{
			this.reasonValue = 6;
			this.reasonText = reason6.Text;
			this.Close();

		}

		private void reason7_Click(object sender, System.EventArgs e)
		{
			this.reasonValue = 7;
			this.reasonText = reason7.Text;
			this.Close();

		}
	}
}
