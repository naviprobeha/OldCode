using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;

namespace SmartShipment
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Startform : System.Windows.Forms.Form
	{
		private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MainMenu mainMenu1;

		private SmartDatabase smartDatabase;
		private const string dbFileName = "SmartOrder.sdf";
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;

		public Startform()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			smartDatabase = new SmartDatabase(dbFileName);

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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem4);
			this.mainMenu1.MenuItems.Add(this.menuItem6);
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			// 
			// menuItem4
			// 
			this.menuItem4.MenuItems.Add(this.menuItem5);
			this.menuItem4.Text = "Order";
			// 
			// menuItem5
			// 
			this.menuItem5.Text = "Lista";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.MenuItems.Add(this.menuItem7);
			this.menuItem6.Text = "Verktyg";
			// 
			// menuItem7
			// 
			this.menuItem7.Text = "Synkronisera";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.menuItem2);
			this.menuItem1.Text = "Inställningar";
			// 
			// menuItem2
			// 
			this.menuItem2.Text = "Synkronisering";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(8, 0);
			this.label1.Size = new System.Drawing.Size(120, 24);
			this.label1.Text = "Huvudmeny";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular);
			this.button1.Location = new System.Drawing.Point(16, 32);
			this.button1.Size = new System.Drawing.Size(208, 48);
			this.button1.Text = "Skapa order";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular);
			this.button2.Location = new System.Drawing.Point(16, 88);
			this.button2.Size = new System.Drawing.Size(208, 48);
			this.button2.Text = "Hämta order";
			// 
			// Startform
			// 
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.Menu = this.mainMenu1;
			this.Text = "SmartShipment";
			this.Load += new System.EventHandler(this.Startform_Load);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		static void Main() 
		{
			Application.Run(new Startform());
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			OrderHeader orderHeader = new OrderHeader(smartDatabase);
			orderHeader.ShowDialog();
			orderHeader.Dispose();
		}

		private void Startform_Load(object sender, System.EventArgs e)
		{
			if (!smartDatabase.init())
			{
				smartDatabase.createDatabase();
			}

			

		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			SynchSettings synchSettings = new SynchSettings(smartDatabase);
			synchSettings.ShowDialog();
			synchSettings.Dispose();
			smartDatabase.getSetup().refresh();
		}

		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			Synchronize synchronize = new Synchronize(smartDatabase);
			synchronize.ShowDialog();
			synchronize.Dispose();
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			Orders orders = new Orders(smartDatabase);
			orders.ShowDialog();
			orders.Dispose();
		}
	}
}
