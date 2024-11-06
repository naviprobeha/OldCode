using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for PriorityView.
	/// </summary>
	public class PriorityView : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button30;
		private System.Windows.Forms.Button button29;
		private System.Windows.Forms.Button button28;
		private System.Windows.Forms.Button button18;
		private System.Windows.Forms.Button button17;
		private System.Windows.Forms.Button button16;
		private System.Windows.Forms.Button button15;
		private System.Windows.Forms.Button button14;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button13;
	
		private int currentPriorityView;

		public PriorityView(SmartDatabase smartDatabase)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			DataShipOrders dataShipOrders = new DataShipOrders(smartDatabase);
			this.button13.Text = "1 ("+dataShipOrders.countPriority(1).ToString()+")";
			this.button14.Text= "2 ("+dataShipOrders.countPriority(2).ToString()+")";
			this.button15.Text = "3 ("+dataShipOrders.countPriority(3).ToString()+")";
			this.button16.Text = "4 ("+dataShipOrders.countPriority(4).ToString()+")";
			this.button17.Text = "5 ("+dataShipOrders.countPriority(5).ToString()+")";
			this.button18.Text = "6 ("+dataShipOrders.countPriority(6).ToString()+")";
			this.button28.Text = "7 ("+dataShipOrders.countPriority(7).ToString()+")";
			this.button29.Text = "8 ("+dataShipOrders.countPriority(8).ToString()+")";
			this.button30.Text = "9 ("+dataShipOrders.countPriority(9).ToString()+")";
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
			this.button30 = new System.Windows.Forms.Button();
			this.button29 = new System.Windows.Forms.Button();
			this.button28 = new System.Windows.Forms.Button();
			this.button18 = new System.Windows.Forms.Button();
			this.button17 = new System.Windows.Forms.Button();
			this.button16 = new System.Windows.Forms.Button();
			this.button15 = new System.Windows.Forms.Button();
			this.button14 = new System.Windows.Forms.Button();
			this.button13 = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			// 
			// button30
			// 
			this.button30.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button30.Location = new System.Drawing.Point(224, 136);
			this.button30.Size = new System.Drawing.Size(88, 32);
			this.button30.Text = "9";
			this.button30.Click += new System.EventHandler(this.button30_Click);
			// 
			// button29
			// 
			this.button29.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button29.Location = new System.Drawing.Point(224, 96);
			this.button29.Size = new System.Drawing.Size(88, 32);
			this.button29.Text = "8";
			this.button29.Click += new System.EventHandler(this.button29_Click);
			// 
			// button28
			// 
			this.button28.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button28.Location = new System.Drawing.Point(224, 56);
			this.button28.Size = new System.Drawing.Size(88, 32);
			this.button28.Text = "7";
			this.button28.Click += new System.EventHandler(this.button28_Click);
			// 
			// button18
			// 
			this.button18.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button18.Location = new System.Drawing.Point(120, 136);
			this.button18.Size = new System.Drawing.Size(88, 32);
			this.button18.Text = "6";
			this.button18.Click += new System.EventHandler(this.button18_Click);
			// 
			// button17
			// 
			this.button17.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button17.Location = new System.Drawing.Point(120, 96);
			this.button17.Size = new System.Drawing.Size(88, 32);
			this.button17.Text = "5";
			this.button17.Click += new System.EventHandler(this.button17_Click);
			// 
			// button16
			// 
			this.button16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button16.Location = new System.Drawing.Point(120, 56);
			this.button16.Size = new System.Drawing.Size(88, 32);
			this.button16.Text = "4";
			this.button16.Click += new System.EventHandler(this.button16_Click);
			// 
			// button15
			// 
			this.button15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button15.Location = new System.Drawing.Point(16, 136);
			this.button15.Size = new System.Drawing.Size(88, 32);
			this.button15.Text = "3";
			this.button15.Click += new System.EventHandler(this.button15_Click);
			// 
			// button14
			// 
			this.button14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button14.Location = new System.Drawing.Point(16, 96);
			this.button14.Size = new System.Drawing.Size(88, 32);
			this.button14.Text = "2";
			this.button14.Click += new System.EventHandler(this.button14_Click);
			// 
			// button13
			// 
			this.button13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button13.Location = new System.Drawing.Point(16, 56);
			this.button13.Size = new System.Drawing.Size(88, 32);
			this.button13.Text = "1";
			this.button13.Click += new System.EventHandler(this.button13_Click);
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label9.Location = new System.Drawing.Point(5, 3);
			this.label9.Size = new System.Drawing.Size(219, 20);
			this.label9.Text = "Välj prioritetsvy";
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(16, 176);
			this.button3.Size = new System.Drawing.Size(296, 32);
			this.button3.Text = "Alla körorder";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// PriorityView
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.button30);
			this.Controls.Add(this.button29);
			this.Controls.Add(this.button28);
			this.Controls.Add(this.button18);
			this.Controls.Add(this.button17);
			this.Controls.Add(this.button16);
			this.Controls.Add(this.button15);
			this.Controls.Add(this.button14);
			this.Controls.Add(this.button13);
			this.Text = "Välj prioritet";

		}
		#endregion

		public int getPriorityView()
		{
			return currentPriorityView;
		}

		private void button13_Click(object sender, System.EventArgs e)
		{
			currentPriorityView = 1;
			this.Close();
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			currentPriorityView = 0;
			this.Close();

		}

		private void button16_Click(object sender, System.EventArgs e)
		{
			currentPriorityView = 4;
			this.Close();

		}

		private void button28_Click(object sender, System.EventArgs e)
		{
			currentPriorityView = 7;
			this.Close();

		}

		private void button14_Click(object sender, System.EventArgs e)
		{
			currentPriorityView = 2;
			this.Close();

		}

		private void button17_Click(object sender, System.EventArgs e)
		{
			currentPriorityView = 5;
			this.Close();

		}

		private void button29_Click(object sender, System.EventArgs e)
		{
			currentPriorityView = 8;
			this.Close();

		}

		private void button15_Click(object sender, System.EventArgs e)
		{
			currentPriorityView = 3;
			this.Close();

		}

		private void button18_Click(object sender, System.EventArgs e)
		{
			currentPriorityView = 6;
			this.Close();

		}

		private void button30_Click(object sender, System.EventArgs e)
		{
			currentPriorityView = 9;
			this.Close();

		}
	}
}
