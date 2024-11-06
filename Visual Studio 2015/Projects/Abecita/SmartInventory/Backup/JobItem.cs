using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for JobItem.
	/// </summary>
	public class JobItem : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox scanLocationBox;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox stausBox;
		private System.Windows.Forms.TextBox sumQuantityBox;
		private System.Windows.Forms.TextBox itemNoBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label1;
	
		private SmartDatabase smartDatabase;
		private int zone;

		public JobItem(SmartDatabase smartDatabase, int zone)
		{
			this.smartDatabase = smartDatabase;
			this.zone = zone;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			switch (zone)
			{
				case 1: 
				{
					label4.Text = "Frekvensklass: Högfrekvent";
					break;
				}
				case 2: 
				{
					label4.Text = "Frekvensklass: Mellanfrekvent";
					break;
				}
				case 3: 
				{
					label4.Text = "Frekvensklass: Lågfrekvent";
					break;
				}
				case 4: 
				{
					label4.Text = "Frekvensklass: Mkt lågfrekvent";
					break;
				}
				case 5: 
				{
					label4.Text = "Frekvensklass: Storvolymigt";
					break;
				}
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
			this.label6 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.scanLocationBox = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.stausBox = new System.Windows.Forms.TextBox();
			this.sumQuantityBox = new System.Windows.Forms.TextBox();
			this.itemNoBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label6.Location = new System.Drawing.Point(8, 8);
			this.label6.Size = new System.Drawing.Size(200, 20);
			this.label6.Text = "Uttag - Batch:";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.label7);
			this.panel3.Controls.Add(this.label5);
			this.panel3.Controls.Add(this.scanLocationBox);
			this.panel3.Location = new System.Drawing.Point(0, 48);
			this.panel3.Size = new System.Drawing.Size(240, 88);
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label7.Location = new System.Drawing.Point(16, 32);
			this.label7.Size = new System.Drawing.Size(112, 20);
			this.label7.Text = "Scanna lagerplats:";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
			this.label5.Location = new System.Drawing.Point(16, 8);
			this.label5.Size = new System.Drawing.Size(208, 20);
			this.label5.Text = "Hämtas från";
			// 
			// scanLocationBox
			// 
			this.scanLocationBox.Location = new System.Drawing.Point(16, 56);
			this.scanLocationBox.Size = new System.Drawing.Size(208, 20);
			this.scanLocationBox.Text = "";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.stausBox);
			this.panel1.Controls.Add(this.sumQuantityBox);
			this.panel1.Controls.Add(this.itemNoBox);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(0, 136);
			this.panel1.Size = new System.Drawing.Size(240, 88);
			// 
			// stausBox
			// 
			this.stausBox.Location = new System.Drawing.Point(88, 56);
			this.stausBox.ReadOnly = true;
			this.stausBox.Size = new System.Drawing.Size(136, 20);
			this.stausBox.Text = "";
			// 
			// sumQuantityBox
			// 
			this.sumQuantityBox.Location = new System.Drawing.Point(88, 32);
			this.sumQuantityBox.ReadOnly = true;
			this.sumQuantityBox.Size = new System.Drawing.Size(136, 20);
			this.sumQuantityBox.Text = "";
			// 
			// itemNoBox
			// 
			this.itemNoBox.Location = new System.Drawing.Point(88, 8);
			this.itemNoBox.ReadOnly = true;
			this.itemNoBox.Size = new System.Drawing.Size(136, 20);
			this.itemNoBox.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 59);
			this.label3.Size = new System.Drawing.Size(72, 20);
			this.label3.Text = "Antal:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 35);
			this.label2.Size = new System.Drawing.Size(72, 20);
			this.label2.Text = "Artikel:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 11);
			this.label1.Size = new System.Drawing.Size(72, 20);
			this.label1.Text = "HE ID:";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
			this.label4.Location = new System.Drawing.Point(8, 24);
			this.label4.Size = new System.Drawing.Size(208, 20);
			this.label4.Text = "Frekvensklass:";
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button2.Location = new System.Drawing.Point(8, 232);
			this.button2.Size = new System.Drawing.Size(224, 32);
			this.button2.Text = "Tillbaka";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// JobItem
			// 
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel1);
			this.Text = "Påfyllning - Uttag";

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
