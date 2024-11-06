using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for CustomerList.
	/// </summary>
	public class CustomerList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.DataGrid customerGrid;
		private System.Windows.Forms.DataGridTableStyle customerTable;
		private System.Windows.Forms.DataGridTextBoxColumn nameCol;
		private System.Windows.Forms.DataGridTextBoxColumn addressCol;
		private System.Windows.Forms.DataGridTextBoxColumn cityBox;
		private System.Windows.Forms.DataGridTextBoxColumn phoneNoCol;
		private System.Windows.Forms.TextBox searchBox;
		private System.Windows.Forms.ComboBox searchWhatBox;
	
		private SmartDatabase smartDatabase;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;

		private int status;
		private System.Windows.Forms.Button button6;
		private int selectedControl = 0;

		public CustomerList(SmartDatabase smartDatabase)
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
			this.customerGrid = new System.Windows.Forms.DataGrid();
			this.customerTable = new System.Windows.Forms.DataGridTableStyle();
			this.nameCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.addressCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.cityBox = new System.Windows.Forms.DataGridTextBoxColumn();
			this.phoneNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.searchBox = new System.Windows.Forms.TextBox();
			this.searchWhatBox = new System.Windows.Forms.ComboBox();
			this.button3 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			// 
			// customerGrid
			// 
			this.customerGrid.Location = new System.Drawing.Point(8, 40);
			this.customerGrid.Size = new System.Drawing.Size(304, 128);
			this.customerGrid.TableStyles.Add(this.customerTable);
			this.customerGrid.Click += new System.EventHandler(this.customerGrid_Click);
			this.customerGrid.GotFocus += new System.EventHandler(this.customerGrid_GotFocus);
			// 
			// customerTable
			// 
			this.customerTable.GridColumnStyles.Add(this.nameCol);
			this.customerTable.GridColumnStyles.Add(this.addressCol);
			this.customerTable.GridColumnStyles.Add(this.cityBox);
			this.customerTable.GridColumnStyles.Add(this.phoneNoCol);
			this.customerTable.MappingName = "customer";
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
			// cityBox
			// 
			this.cityBox.HeaderText = "Ort";
			this.cityBox.MappingName = "city";
			this.cityBox.NullText = "";
			this.cityBox.Width = 100;
			// 
			// phoneNoCol
			// 
			this.phoneNoCol.HeaderText = "Telefonnr";
			this.phoneNoCol.MappingName = "phoneNo";
			this.phoneNoCol.NullText = "";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(240, 176);
			this.button1.Size = new System.Drawing.Size(72, 32);
			this.button1.Text = "Ok";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(168, 176);
			this.button2.Size = new System.Drawing.Size(64, 32);
			this.button2.Text = "Avbryt";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// searchBox
			// 
			this.searchBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular);
			this.searchBox.Location = new System.Drawing.Point(152, 8);
			this.searchBox.ReadOnly = true;
			this.searchBox.Size = new System.Drawing.Size(104, 24);
			this.searchBox.Text = "";
			this.searchBox.GotFocus += new System.EventHandler(this.searchBox_GotFocus);
			this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
			// 
			// searchWhatBox
			// 
			this.searchWhatBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular);
			this.searchWhatBox.Items.Add("Namn");
			this.searchWhatBox.Items.Add("Ort");
			this.searchWhatBox.Items.Add("Organisationsnr");
			this.searchWhatBox.Items.Add("Personnr");
			this.searchWhatBox.Items.Add("Produktionsplatsnr");
			this.searchWhatBox.Items.Add("Telefonnr");
			this.searchWhatBox.Items.Add("Kundnr");
			this.searchWhatBox.Items.Add("Adress");
			this.searchWhatBox.Location = new System.Drawing.Point(8, 9);
			this.searchWhatBox.Size = new System.Drawing.Size(136, 26);
			this.searchWhatBox.GotFocus += new System.EventHandler(this.searchWhatBox_GotFocus);
			this.searchWhatBox.SelectedIndexChanged += new System.EventHandler(this.searchWhatBox_SelectedIndexChanged);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(264, 8);
			this.button3.Size = new System.Drawing.Size(48, 24);
			this.button3.Text = "Sök";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(48, 176);
			this.button5.Size = new System.Drawing.Size(32, 32);
			this.button5.Text = ">";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(8, 176);
			this.button4.Size = new System.Drawing.Size(32, 32);
			this.button4.Text = "<";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button6
			// 
			this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button6.Location = new System.Drawing.Point(88, 176);
			this.button6.Size = new System.Drawing.Size(72, 32);
			this.button6.Text = "Info";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// CustomerList
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.searchWhatBox);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.searchBox);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.customerGrid);
			this.Text = "Kundlista";
			this.Load += new System.EventHandler(this.CustomerList_Load);

		}
		#endregion


		private void updateGrid()
		{
			if (searchBox.Text != "")
			{
				Cursor.Current = Cursors.WaitCursor;
				Cursor.Show();

				DataCustomers dataCustomers = new DataCustomers(smartDatabase);
				DataSet customerDataSet = dataCustomers.getDataSet(searchWhatBox.SelectedIndex, searchBox.Text);
				customerGrid.DataSource = customerDataSet.Tables[0];

				Cursor.Current = Cursors.Default;
				Cursor.Hide();
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.status = 0;
			this.Close();
		}

		private void searchBox_GotFocus(object sender, System.EventArgs e)
		{
		}

		private void searchWhatBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			updateGrid();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (customerGrid.DataSource != null)
			{
				DataTable customerTable = (DataTable)customerGrid.DataSource;
				if (customerTable.Rows.Count > 0)
				{
					this.status = 1;
					this.Close();
				}
				else
				{
					MessageBox.Show("Du måste välja en kund.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
				}
			}
		}

		public int getStatus()
		{
			return status;
		}

		public string getCustomerNo()
		{
			if (customerGrid.DataSource != null)
			{
				DataTable customerTable = (DataTable)customerGrid.DataSource;
				if (customerTable.Rows.Count > 0)
				{
					return customerTable.Rows[customerGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString();
				}
			}
			return null;
		}

		private void CustomerList_Load(object sender, System.EventArgs e)
		{
			/*
			Cursor.Current = Cursors.WaitCursor;
			Cursor.Show();

			CustomerSearch customerSearch = new CustomerSearch();

			Cursor.Current = Cursors.Default;
			Cursor.Hide();

			customerSearch.ShowDialog();

			Cursor.Current = Cursors.WaitCursor;
			Cursor.Show();

			int searchWhat = customerSearch.getSearchWhat();
			string searchString = customerSearch.getSearchString();
			customerSearch.Dispose();

			if (searchString == "")
			{

				this.Close();

				Cursor.Current = Cursors.Default;
				Cursor.Hide();

			}
			else
			{
				searchWhatBox.SelectedIndex = searchWhat;
				searchBox.Text = searchString;
				updateGrid();

				Cursor.Current = Cursors.Default;
				Cursor.Hide();

			}

			*/
			
			searchWhatBox.SelectedIndex = smartDatabase.getSetup().defaultCustomerSearchType;
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			Keyboard keyboard = new Keyboard(30);
			keyboard.setInputString(searchBox.Text);

			if (searchWhatBox.SelectedIndex == 0) keyboard.setStartTab(0);
			if (searchWhatBox.SelectedIndex == 1) keyboard.setStartTab(0);
			if (searchWhatBox.SelectedIndex == 2) keyboard.setStartTab(1);
			if (searchWhatBox.SelectedIndex == 3) keyboard.setStartTab(1);
			if (searchWhatBox.SelectedIndex == 4) keyboard.setStartTab(1);
			if (searchWhatBox.SelectedIndex == 5) keyboard.setStartTab(1);
			if (searchWhatBox.SelectedIndex == 6) keyboard.setStartTab(1);

			keyboard.ShowDialog();
			searchBox.Text = keyboard.getInputString();
			keyboard.Dispose();

			updateGrid();
			
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			if (selectedControl == 1)
			{
				if (this.searchWhatBox.SelectedIndex < (this.searchWhatBox.Items.Count-1))
				{
					this.searchWhatBox.SelectedIndex = this.searchWhatBox.SelectedIndex + 1;
				}
			}
			else
			{
				if ((this.customerGrid.DataSource != null) && (((DataTable)this.customerGrid.DataSource).Rows.Count > 0))
				{
					if (this.customerGrid.CurrentRowIndex < (((DataTable)this.customerGrid.DataSource).Rows.Count -1))
					{
						this.customerGrid.CurrentRowIndex = this.customerGrid.CurrentRowIndex + 1;
						this.customerGrid.Select(this.customerGrid.CurrentRowIndex);
					}
				}
			}

		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			if (selectedControl == 1)
			{
				if (this.searchWhatBox.SelectedIndex > 0) 
				{
					this.searchWhatBox.SelectedIndex = this.searchWhatBox.SelectedIndex - 1;
				}
			}
			else
			{
				if ((this.customerGrid.DataSource != null) && (((DataTable)this.customerGrid.DataSource).Rows.Count > 0))
				{
					if (this.customerGrid.CurrentRowIndex > 0)
					{
						this.customerGrid.CurrentRowIndex = this.customerGrid.CurrentRowIndex - 1;
						this.customerGrid.Select(this.customerGrid.CurrentRowIndex);
					}
				}
			}
		}

		private void searchBox_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void searchWhatBox_GotFocus(object sender, System.EventArgs e)
		{
			selectedControl = 1;
		}

		private void customerGrid_Click(object sender, System.EventArgs e)
		{
		}

		private void customerGrid_GotFocus(object sender, System.EventArgs e)
		{
			selectedControl = 0;
		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			if (customerGrid.DataSource != null)
			{
				DataTable customerTable = (DataTable)customerGrid.DataSource;
				if (customerTable.Rows.Count > 0)
				{
					DataCustomer dataCustomer = new DataCustomer(smartDatabase, customerTable.Rows[customerGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString());
					CustomerInfo customerInfo = new CustomerInfo(smartDatabase, dataCustomer);
					customerInfo.ShowDialog();
					customerInfo.Dispose();
				}
			}

		}
	}
}
