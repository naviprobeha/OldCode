using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;

namespace SmartOrder
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class StartForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MainMenu mainMenu1;

		private DataVisitList dataVisitList;
		private DataCustomer selectedCustomer;

		private SmartDatabase smartDatabase;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGrid visitGrid;
		private System.Windows.Forms.DataGridTableStyle activeTable;
		private System.Windows.Forms.DataGridTextBoxColumn customerNoCol;
		private System.Windows.Forms.DataGridTextBoxColumn nameCol;
		private System.Windows.Forms.Button button1;

		private const string dbFileName = "SmartOrder.sdf";

		public StartForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			smartDatabase = new SmartDatabase(dbFileName);	
			dataVisitList = new DataVisitList(smartDatabase);

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
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.button1 = new System.Windows.Forms.Button();
			this.visitGrid = new System.Windows.Forms.DataGrid();
			this.activeTable = new System.Windows.Forms.DataGridTableStyle();
			this.customerNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.nameCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.label1 = new System.Windows.Forms.Label();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			this.mainMenu1.MenuItems.Add(this.menuItem2);
			this.mainMenu1.MenuItems.Add(this.menuItem7);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.menuItem3);
			this.menuItem1.MenuItems.Add(this.menuItem4);
			this.menuItem1.Text = "Order";
			// 
			// menuItem3
			// 
			this.menuItem3.Text = "Lagda order";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Text = "Ny order";
			// 
			// menuItem2
			// 
			this.menuItem2.MenuItems.Add(this.menuItem5);
			this.menuItem2.MenuItems.Add(this.menuItem6);
			this.menuItem2.Text = "Kund";
			// 
			// menuItem5
			// 
			this.menuItem5.Text = "Lista";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Text = "Ny kund";
			// 
			// menuItem7
			// 
			this.menuItem7.MenuItems.Add(this.menuItem9);
			this.menuItem7.MenuItems.Add(this.menuItem8);
			this.menuItem7.Text = "Verktyg";
			// 
			// menuItem9
			// 
			this.menuItem9.Text = "Inställningar";
			this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Text = "Synkronisera";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(242, 272);
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.button1);
			this.tabPage1.Controls.Add(this.visitGrid);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Location = new System.Drawing.Point(4, 4);
			this.tabPage1.Size = new System.Drawing.Size(234, 246);
			this.tabPage1.Text = "Besökslista";
			this.tabPage1.EnabledChanged += new System.EventHandler(this.tabPage1_EnabledChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(160, 216);
			this.button1.Text = "Ny order";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// visitGrid
			// 
			this.visitGrid.Location = new System.Drawing.Point(0, 32);
			this.visitGrid.Size = new System.Drawing.Size(240, 176);
			this.visitGrid.TableStyles.Add(this.activeTable);
			this.visitGrid.Text = "dataGrid1";
			this.visitGrid.Click += new System.EventHandler(this.visitGrid_Click);
			// 
			// activeTable
			// 
			this.activeTable.GridColumnStyles.Add(this.customerNoCol);
			this.activeTable.GridColumnStyles.Add(this.nameCol);
			this.activeTable.MappingName = "activeCustomer";
			// 
			// customerNoCol
			// 
			this.customerNoCol.HeaderText = "Kundnr";
			this.customerNoCol.MappingName = "customerNo";
			this.customerNoCol.NullText = "";
			// 
			// nameCol
			// 
			this.nameCol.HeaderText = "Namn";
			this.nameCol.MappingName = "name";
			this.nameCol.NullText = "";
			this.nameCol.Width = 150;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Size = new System.Drawing.Size(112, 16);
			this.label1.Text = "Kunder att besöka:";
			// 
			// StartForm
			// 
			this.ClientSize = new System.Drawing.Size(242, 279);
			this.Controls.Add(this.tabControl1);
			this.Menu = this.mainMenu1;
			this.Text = "SmartOrder";
			this.Load += new System.EventHandler(this.StartForm_Load);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		static void Main() 
		{
			Application.Run(new StartForm());
		}

		private void StartForm_Load(object sender, System.EventArgs e)
		{
			if (!smartDatabase.init())
			{
				smartDatabase.createDatabase();
			}

			updateGrid();
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			CustomerList customerList = new CustomerList(smartDatabase);
			customerList.ShowDialog();
			updateGrid();
		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			SynchSettings synchSettings = new SynchSettings(smartDatabase);
			synchSettings.ShowDialog();
		}

		private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		private void tabPage1_EnabledChanged(object sender, System.EventArgs e)
		{
		
		}

		private void updateGrid()
		{
			System.Data.DataSet customerDataSet = dataVisitList.getDataSet();
			visitGrid.DataSource = customerDataSet.Tables[0];
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			Synchronize synchronize = new Synchronize(smartDatabase);
			synchronize.ShowDialog();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (selectedCustomer == null)
			{
				MessageBox.Show("Du måste välja en kund först.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			}
			else
			{
				DataSalesHeader dataSalesHeader = new DataSalesHeader(selectedCustomer, smartDatabase);
				Order order = new Order(dataSalesHeader, smartDatabase);
				order.ShowDialog();
			}
		}

		private void visitGrid_Click(object sender, System.EventArgs e)
		{
			selectedCustomer = new DataCustomer(visitGrid[visitGrid.CurrentRowIndex, 0].ToString(), smartDatabase);
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			Orders orders = new Orders(smartDatabase);
			orders.ShowDialog();
		}
	}
}
