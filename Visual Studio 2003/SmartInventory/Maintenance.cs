using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for Maintenance.
	/// </summary>
	public class Maintenance : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button menu2;
		private System.Windows.Forms.Button menu1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
	
		private SmartDatabase smartDatabase;

		public Maintenance(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
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
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.menu2 = new System.Windows.Forms.Button();
			this.menu1 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Size = new System.Drawing.Size(128, 24);
			this.label2.Text = "Lagervård";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
			this.label1.Location = new System.Drawing.Point(8, 32);
			this.label1.Size = new System.Drawing.Size(184, 20);
			this.label1.Text = "Välj funktion i menyn nedan.";
			// 
			// menu2
			// 
			this.menu2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular);
			this.menu2.Location = new System.Drawing.Point(24, 104);
			this.menu2.Size = new System.Drawing.Size(184, 40);
			this.menu2.Text = "Omflyttning";
			this.menu2.Click += new System.EventHandler(this.menu2_Click);
			// 
			// menu1
			// 
			this.menu1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular);
			this.menu1.Location = new System.Drawing.Point(24, 56);
			this.menu1.Size = new System.Drawing.Size(184, 40);
			this.menu1.Text = "Lagerplats info.";
			this.menu1.Click += new System.EventHandler(this.menu1_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular);
			this.button1.Location = new System.Drawing.Point(24, 248);
			this.button1.Size = new System.Drawing.Size(184, 40);
			this.button1.Text = "Tillbaka";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular);
			this.button2.Location = new System.Drawing.Point(24, 152);
			this.button2.Size = new System.Drawing.Size(184, 40);
			this.button2.Text = "Inventering";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular);
			this.button3.Location = new System.Drawing.Point(24, 200);
			this.button3.Size = new System.Drawing.Size(184, 40);
			this.button3.Text = "Direktinlagring";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// Maintenance
			// 
			this.ClientSize = new System.Drawing.Size(240, 296);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.menu2);
			this.Controls.Add(this.menu1);
			this.Text = "Lagervård";

		}
		#endregion

		private void menu1_Click(object sender, System.EventArgs e)
		{
			MaintInfo maintInfo = new MaintInfo(smartDatabase);
			maintInfo.ShowDialog();
			maintInfo.Dispose();
		}

		private void menu2_Click(object sender, System.EventArgs e)
		{
			MaintMove maintMove = new MaintMove(smartDatabase);
			maintMove.ShowDialog();
			maintMove.Dispose();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void menu3_Click(object sender, System.EventArgs e)
		{
			MaintStatus maintStatus = new MaintStatus(smartDatabase);
			maintStatus.ShowDialog();
			maintStatus.Dispose();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			Inventory inventory = new Inventory(smartDatabase);
			inventory.ShowDialog();
			inventory.Dispose();
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			StoreFormInstant storeFormInstant = new StoreFormInstant(smartDatabase);
			storeFormInstant.ShowDialog();
			storeFormInstant.Dispose();
		}
	}
}
