using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for StartForm.
	/// </summary>
	public class StartForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button menu1;
		private System.Windows.Forms.Button menu2;
		private System.Windows.Forms.Button menu3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;

		private SmartDatabase smartDatabase;
		private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
		private System.Windows.Forms.Button button1;

		private const string dbFileName = "\\Flash File Store\\SmartInventory.sdf";

		public StartForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			smartDatabase = new SmartDatabase(dbFileName);	
			if (!smartDatabase.init())
			{
				smartDatabase.createDatabase();
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
			this.menu1 = new System.Windows.Forms.Button();
			this.menu2 = new System.Windows.Forms.Button();
			this.menu3 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
			this.button1 = new System.Windows.Forms.Button();
			// 
			// menu1
			// 
			this.menu1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular);
			this.menu1.Location = new System.Drawing.Point(24, 64);
			this.menu1.Size = new System.Drawing.Size(184, 40);
			this.menu1.Text = "Inlagring";
			this.menu1.Click += new System.EventHandler(this.menu1_Click);
			// 
			// menu2
			// 
			this.menu2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular);
			this.menu2.Location = new System.Drawing.Point(24, 160);
			this.menu2.Size = new System.Drawing.Size(184, 40);
			this.menu2.Text = "Påfyllning";
			this.menu2.Click += new System.EventHandler(this.menu2_Click);
			// 
			// menu3
			// 
			this.menu3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular);
			this.menu3.Location = new System.Drawing.Point(24, 208);
			this.menu3.Size = new System.Drawing.Size(184, 40);
			this.menu3.Text = "Lagervård";
			this.menu3.Click += new System.EventHandler(this.menu3_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
			this.label1.Location = new System.Drawing.Point(8, 32);
			this.label1.Size = new System.Drawing.Size(184, 20);
			this.label1.Text = "Välj funktion i menyn nedan.";
			this.label1.ParentChanged += new System.EventHandler(this.label1_ParentChanged);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Size = new System.Drawing.Size(128, 24);
			this.label2.Text = "Huvudmeny";
			this.label2.ParentChanged += new System.EventHandler(this.label2_ParentChanged);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.menuItem2);
			this.menuItem1.MenuItems.Add(this.menuItem3);
			this.menuItem1.Text = "Verktyg";
			// 
			// menuItem2
			// 
			this.menuItem2.Text = "Synkronisera";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Text = "Inställningar";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular);
			this.button1.Location = new System.Drawing.Point(24, 112);
			this.button1.Size = new System.Drawing.Size(184, 40);
			this.button1.Text = "Antalsreg.";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// StartForm
			// 
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.menu3);
			this.Controls.Add(this.menu2);
			this.Controls.Add(this.menu1);
			this.Menu = this.mainMenu1;
			this.Text = "SmartInventory";

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		static void Main() 
		{
			Application.Run(new StartForm());
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			Synchronize synchronize = new Synchronize(smartDatabase);
			synchronize.ShowDialog();
			synchronize.Dispose();
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			SynchSettings synchSettings = new SynchSettings(smartDatabase);
			synchSettings.ShowDialog();
			synchSettings.Dispose();
		}

		private void menu1_Click(object sender, System.EventArgs e)
		{
			StoreSynch storeSynch = new StoreSynch(smartDatabase);
			storeSynch.ShowDialog();
			storeSynch.Dispose();
		}

		private void menu2_Click(object sender, System.EventArgs e)
		{
			JobSynch jobSynch = new JobSynch(smartDatabase);
			jobSynch.ShowDialog();
			jobSynch.Dispose();
		}

		private void label1_ParentChanged(object sender, System.EventArgs e)
		{
		
		}

		private void menu3_Click(object sender, System.EventArgs e)
		{
			Maintenance maint = new Maintenance(smartDatabase);
			maint.ShowDialog();
			maint.Dispose();
		}

		private void label2_ParentChanged(object sender, System.EventArgs e)
		{
		
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			StoreItemQuantity storeItemQuantity = new StoreItemQuantity(smartDatabase);
			storeItemQuantity.ShowDialog();
			storeItemQuantity.Dispose();
		}
	}
}
