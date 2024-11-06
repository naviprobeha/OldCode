using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for StoreMenu.
	/// </summary>
	public class StoreFreq : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;

		private SmartDatabase smartDatabase;
		private int qtyFreq1;
		private int qtyFreq2;
		private int qtyFreq3;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private int qtyFreq4;
	
		public StoreFreq(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			updateButtons();
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
			this.button4 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.button6 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button4.Location = new System.Drawing.Point(120, 192);
			this.button4.Size = new System.Drawing.Size(112, 64);
			this.button4.Text = "Scanna";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Size = new System.Drawing.Size(208, 20);
			this.label1.Text = "Inlagring";
			// 
			// button6
			// 
			this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.button6.Location = new System.Drawing.Point(8, 192);
			this.button6.Size = new System.Drawing.Size(104, 64);
			this.button6.Text = "Tillbaka";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Size = new System.Drawing.Size(104, 72);
			this.label2.Text = "Här visas antalet uppdragsrader i resp. frekvensklass. För att gå vidare klicka S" +
				"canna.";
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.menuItem2);
			this.menuItem1.MenuItems.Add(this.menuItem3);
			this.menuItem1.Text = "Visa";
			// 
			// menuItem2
			// 
			this.menuItem2.Text = "Uppdrag";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Text = "Lagerplatser";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.label3.Location = new System.Drawing.Point(120, 40);
			this.label3.Size = new System.Drawing.Size(112, 24);
			this.label3.Text = "label3";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.label4.Location = new System.Drawing.Point(120, 80);
			this.label4.Size = new System.Drawing.Size(112, 24);
			this.label4.Text = "label4";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.label5.Location = new System.Drawing.Point(120, 120);
			this.label5.Size = new System.Drawing.Size(112, 24);
			this.label5.Text = "label5";
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular);
			this.label6.Location = new System.Drawing.Point(120, 160);
			this.label6.Size = new System.Drawing.Size(112, 24);
			this.label6.Text = "label6";
			// 
			// StoreFreq
			// 
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button4);
			this.Menu = this.mainMenu1;
			this.Text = "Inlagring";

		}
		#endregion



		private void button4_Click(object sender, System.EventArgs e)
		{
			showStoreForm();
		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void updateButtons()
		{
			DataWhseActivityLines dataWhseActivityLines = new DataWhseActivityLines(smartDatabase);

			qtyFreq1 = dataWhseActivityLines.countLines(DataWhseActivityHeaders.WHSE_TYPE_ARRIVAL, 1, DataWhseActivityLines.WHSE_ACTION_PLACE, DataWhseActivityLines.WHSE_STATUS_NONE, true);
			qtyFreq2 = dataWhseActivityLines.countLines(DataWhseActivityHeaders.WHSE_TYPE_ARRIVAL, 2, DataWhseActivityLines.WHSE_ACTION_PLACE, DataWhseActivityLines.WHSE_STATUS_NONE, true);
			qtyFreq3 = dataWhseActivityLines.countLines(DataWhseActivityHeaders.WHSE_TYPE_ARRIVAL, 3, DataWhseActivityLines.WHSE_ACTION_PLACE, DataWhseActivityLines.WHSE_STATUS_NONE, true);
			qtyFreq4 = dataWhseActivityLines.countLines(DataWhseActivityHeaders.WHSE_TYPE_ARRIVAL, 4, DataWhseActivityLines.WHSE_ACTION_PLACE, DataWhseActivityLines.WHSE_STATUS_NONE, true);

			label3.Text = "Hög ("+qtyFreq1+")";
			label4.Text = "Mellan ("+qtyFreq2+")";
			label5.Text = "Låg ("+qtyFreq3+")";
			label6.Text = "Mkt låg ("+qtyFreq4+")";
		}


		private void showStoreForm()
		{
			StoreForm storeForm = new StoreForm(smartDatabase);
			storeForm.ShowDialog();
			if (storeForm.getStatus() == 2)
			{
				this.Close();
			}
			else
			{
				updateButtons();
			}
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			StoreJobs storeJobs = new StoreJobs(smartDatabase);
			storeJobs.ShowDialog();
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			Bins bins = new Bins(smartDatabase);
			bins.ShowDialog();
		}
	}
}
