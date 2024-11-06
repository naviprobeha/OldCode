using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartOrder
{
	/// <summary>
	/// Summary description for CustomerList.
	/// </summary>
	public class CustomerList : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.DataGrid customerGrid;
		private System.Windows.Forms.DataGridTableStyle customerTable;
		private System.Windows.Forms.DataGridTextBoxColumn noCol;
		private System.Windows.Forms.DataGridTextBoxColumn nameCol;
		private System.Windows.Forms.DataGridTextBoxColumn addressCol;
		private System.Windows.Forms.DataGridTextBoxColumn selectCol;
		private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
		private System.Windows.Forms.DataGridTextBoxColumn address2Col;
		private System.Windows.Forms.DataGridTextBoxColumn zipCodeCol;
		private System.Windows.Forms.DataGridTextBoxColumn cityCol;
		private System.Windows.Forms.MainMenu mainMenu1;

		private SmartDatabase smartDatabase;
		private DataCustomers dataCustomers;
		private System.Windows.Forms.MainMenu mainMenu2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox searchCustomer;
		private DataVisitList dataVisitList;
	
		public CustomerList(SmartDatabase smartDatabase)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.smartDatabase = smartDatabase;
			dataCustomers = new DataCustomers(smartDatabase);
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
			this.customerGrid = new System.Windows.Forms.DataGrid();
			this.customerTable = new System.Windows.Forms.DataGridTableStyle();
			this.selectCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.noCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.nameCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.addressCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.address2Col = new System.Windows.Forms.DataGridTextBoxColumn();
			this.zipCodeCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.cityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.mainMenu2 = new System.Windows.Forms.MainMenu();
			this.label1 = new System.Windows.Forms.Label();
			this.searchCustomer = new System.Windows.Forms.TextBox();
			// 
			// customerGrid
			// 
			this.customerGrid.Location = new System.Drawing.Point(0, 40);
			this.customerGrid.Size = new System.Drawing.Size(240, 200);
			this.customerGrid.TableStyles.Add(this.customerTable);
			this.customerGrid.Text = "dataGrid1";
			this.customerGrid.Click += new System.EventHandler(this.customerGrid_Click);
			this.customerGrid.GotFocus += new System.EventHandler(this.customerGrid_GotFocus);
			// 
			// customerTable
			// 
			this.customerTable.GridColumnStyles.Add(this.selectCol);
			this.customerTable.GridColumnStyles.Add(this.noCol);
			this.customerTable.GridColumnStyles.Add(this.nameCol);
			this.customerTable.GridColumnStyles.Add(this.addressCol);
			this.customerTable.GridColumnStyles.Add(this.address2Col);
			this.customerTable.GridColumnStyles.Add(this.zipCodeCol);
			this.customerTable.GridColumnStyles.Add(this.cityCol);
			this.customerTable.MappingName = "customer";
			// 
			// selectCol
			// 
			this.selectCol.HeaderText = "Besöka";
			this.selectCol.MappingName = "checked";
			this.selectCol.NullText = "";
			this.selectCol.Width = 20;
			// 
			// noCol
			// 
			this.noCol.HeaderText = "Kundnr";
			this.noCol.MappingName = "no";
			this.noCol.NullText = "";
			// 
			// nameCol
			// 
			this.nameCol.HeaderText = "Namn";
			this.nameCol.MappingName = "name";
			this.nameCol.NullText = "";
			this.nameCol.Width = 100;
			// 
			// addressCol
			// 
			this.addressCol.HeaderText = "Adress";
			this.addressCol.MappingName = "address";
			this.addressCol.NullText = "";
			this.addressCol.Width = 100;
			// 
			// address2Col
			// 
			this.address2Col.HeaderText = "Adress 2";
			this.address2Col.MappingName = "address2";
			this.address2Col.NullText = "";
			this.address2Col.Width = 100;
			// 
			// zipCodeCol
			// 
			this.zipCodeCol.HeaderText = "Postnr";
			this.zipCodeCol.MappingName = "zipCode";
			this.zipCodeCol.NullText = "";
			// 
			// cityCol
			// 
			this.cityCol.HeaderText = "Postadress";
			this.cityCol.MappingName = "city";
			this.cityCol.NullText = "";
			this.cityCol.Width = 100;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 11);
			this.label1.Size = new System.Drawing.Size(72, 16);
			this.label1.Text = "Sök kund:";
			// 
			// searchCustomer
			// 
			this.searchCustomer.Location = new System.Drawing.Point(88, 8);
			this.searchCustomer.Size = new System.Drawing.Size(104, 20);
			this.searchCustomer.Text = "";
			this.searchCustomer.GotFocus += new System.EventHandler(this.searchCustomer_GotFocus);
			this.searchCustomer.TextChanged += new System.EventHandler(this.searchCustomer_TextChanged);
			// 
			// CustomerList
			// 
			this.Controls.Add(this.searchCustomer);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.customerGrid);
			this.Menu = this.mainMenu2;
			this.Text = "Kundlista";
			this.Load += new System.EventHandler(this.CustomerList_Load);

		}
		#endregion

		private void CustomerList_Load(object sender, System.EventArgs e)
		{
			System.Data.DataSet customerDataSet = dataCustomers.getDataSet();
			customerGrid.DataSource = customerDataSet.Tables[0];
			
			
			if (customerGrid.BindingContext[customerGrid.DataSource, ""] != null)
			{
				if (customerGrid.BindingContext[customerGrid.DataSource, ""].Count > 0)
				{
					int i = 0;
					while (i < customerGrid.BindingContext[customerGrid.DataSource, ""].Count)
					{
						if (dataVisitList.check(new DataCustomer(customerGrid[i, 1].ToString())))
						{
							customerGrid[i, 0] = "Ja";
						}
						i++;
					}
					customerGrid.CurrentRowIndex = 0;
				}
			}
			
		}

		private void customerGrid_Click(object sender, EventArgs e)
		{
			if (customerGrid[customerGrid.CurrentRowIndex, 0].ToString().Equals("Ja"))
			{
				customerGrid[customerGrid.CurrentRowIndex, 0] = "";
				dataVisitList.remove(new DataCustomer(customerGrid[customerGrid.CurrentRowIndex, 1].ToString()));
			}
			else
			{
				customerGrid[customerGrid.CurrentRowIndex, 0] = "Ja";
				dataVisitList.add(new DataCustomer(customerGrid[customerGrid.CurrentRowIndex, 1].ToString()));
			}

		}

		private void searchCustomer_TextChanged(object sender, System.EventArgs e)
		{
			bool found = false;

			if (customerGrid.BindingContext[customerGrid.DataSource, ""] != null)
			{
				int i = 0;
				while ((i < customerGrid.BindingContext[customerGrid.DataSource, ""].Count) && (!found))
				{
					if (customerGrid[i, 2].ToString().Length >= searchCustomer.Text.Length)
					{
						if (customerGrid[i, 2].ToString().Substring(0, searchCustomer.Text.Length).ToUpper() == searchCustomer.Text.ToUpper())
						{
							customerGrid.CurrentRowIndex = i;
							found = true;
						}
					}
					i++;
				}
				if (!found)
				{
					customerGrid.CurrentRowIndex = 0;
				}
			}
		
		}

		private void searchCustomer_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = true;
		}

		private void customerGrid_GotFocus(object sender, System.EventArgs e)
		{
			inputPanel1.Enabled = false;
		}


	}
}
