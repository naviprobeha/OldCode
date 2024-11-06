using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartShipment
{
	/// <summary>
	/// Summary description for CustomerList.
	/// </summary>
	public class CustomerList : System.Windows.Forms.Form, Logger
	{

		private SmartDatabase smartDatabase;
		private DataCustomers dataCustomers;
		private DataCustomer selectedCustomer;
		private DataCustomer returnCustomer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox searchCustomer;
		private System.Windows.Forms.DataGrid customerGrid;
		private System.Windows.Forms.DataGridTableStyle customerTable;
		private System.Windows.Forms.DataGridTextBoxColumn nameCol;
		private System.Windows.Forms.DataGridTextBoxColumn addressCol;
		private System.Windows.Forms.DataGridTextBoxColumn customerNoCol;
		private System.Windows.Forms.MainMenu mainMenu1;
		private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ListBox serviceLog;
		private DataSetup dataSetup;
		private CreditCheck creditData;
	
		public CustomerList(SmartDatabase smartDatabase)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.smartDatabase = smartDatabase;
			dataCustomers = new DataCustomers(smartDatabase);
			dataSetup = new DataSetup(smartDatabase);


			serviceLog.Width = 240;
			serviceLog.Height = 184;
			serviceLog.Visible = false;
			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			this.searchCustomer.Focus();
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
			this.label1 = new System.Windows.Forms.Label();
			this.searchCustomer = new System.Windows.Forms.TextBox();
			this.customerGrid = new System.Windows.Forms.DataGrid();
			this.customerTable = new System.Windows.Forms.DataGridTableStyle();
			this.customerNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.nameCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.addressCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.serviceLog = new System.Windows.Forms.ListBox();
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
			// customerGrid
			// 
			this.customerGrid.Location = new System.Drawing.Point(0, 40);
			this.customerGrid.Size = new System.Drawing.Size(240, 176);
			this.customerGrid.TableStyles.Add(this.customerTable);
			this.customerGrid.Text = "customerGrid";
			this.customerGrid.Click += new System.EventHandler(this.customerGrid_Click_1);
			// 
			// customerTable
			// 
			this.customerTable.GridColumnStyles.Add(this.customerNoCol);
			this.customerTable.GridColumnStyles.Add(this.nameCol);
			this.customerTable.GridColumnStyles.Add(this.addressCol);
			this.customerTable.MappingName = "customer";
			// 
			// customerNoCol
			// 
			this.customerNoCol.HeaderText = "Kundnr";
			this.customerNoCol.MappingName = "no";
			this.customerNoCol.NullText = "";
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
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
			this.button1.Location = new System.Drawing.Point(128, 224);
			this.button1.Size = new System.Drawing.Size(104, 40);
			this.button1.Text = "Ok";
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
			this.button2.Location = new System.Drawing.Point(8, 224);
			this.button2.Size = new System.Drawing.Size(104, 40);
			this.button2.Text = "Avbryt";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// serviceLog
			// 
			this.serviceLog.Location = new System.Drawing.Point(0, 40);
			this.serviceLog.Size = new System.Drawing.Size(100, 93);
			this.serviceLog.Visible = false;
			// 
			// CustomerList
			// 
			this.Controls.Add(this.serviceLog);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.customerGrid);
			this.Controls.Add(this.searchCustomer);
			this.Controls.Add(this.label1);
			this.Menu = this.mainMenu1;
			this.Text = "Kundlista";
			this.Load += new System.EventHandler(this.CustomerList_Load);

		}
		#endregion

		private void CustomerList_Load(object sender, System.EventArgs e)
		{
			
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			System.Windows.Forms.Cursor.Show();
			
			System.Data.DataSet customerDataSet = dataCustomers.getDataSet();
			customerGrid.DataSource = customerDataSet.Tables[0];

			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			System.Windows.Forms.Cursor.Hide();

		}

		private void customerGrid_Click(object sender, EventArgs e)
		{

		}

		private void searchCustomer_TextChanged(object sender, System.EventArgs e)
		{
			bool found = false;

			if (customerGrid.BindingContext[customerGrid.DataSource, ""] != null)
			{
				int i = 0;
				while ((i < customerGrid.BindingContext[customerGrid.DataSource, ""].Count) && (!found))
				{
					if (customerGrid[i, 1].ToString().Length >= searchCustomer.Text.Length)
					{
						if (customerGrid[i, 1].ToString().Substring(0, searchCustomer.Text.Length).ToUpper() == searchCustomer.Text.ToUpper())
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

		private void button1_Click(object sender, System.EventArgs e)
		{
		
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void customerGrid_Click_1(object sender, System.EventArgs e)
		{
			if (customerGrid.BindingContext[customerGrid.DataSource, ""].Count > 0)
			{
				selectedCustomer = new DataCustomer(customerGrid[customerGrid.CurrentRowIndex, 0].ToString(), smartDatabase);
			}

		}

		private void button1_Click_1(object sender, System.EventArgs e)
		{
			if (selectedCustomer == null)
			{
				MessageBox.Show("Du måste välja en kund först.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			}
			else
			{
				if (checkCreditLimit())
				{
					returnCustomer = selectedCustomer;
					this.Close();
				}
				else
				{
					System.Windows.Forms.MessageBox.Show("Kreditprövningen misslyckades.", "Kreditprövning");
				}
			}

		}

		private bool checkCreditLimit()
		{
			serviceLog.Items.Clear();
			serviceLog.Items.Add("Kreditprövning...");
			serviceLog.Visible = true;
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			System.Windows.Forms.Cursor.Show();

			bool error = false;

			button1.Enabled = false;
			button2.Enabled = false;

			Service synchService = new Service("creditRequest", smartDatabase, dataSetup);
			synchService.setLogger(this);

			synchService.serviceRequest.setServiceArgument(selectedCustomer);

			ServiceResponse serviceResponse = synchService.performService();

			if (serviceResponse != null)
			{
				if (serviceResponse.hasErrors)
				{
					System.Windows.Forms.MessageBox.Show(serviceResponse.error.status+": "+serviceResponse.error.description, "Fel", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
					write("Förfrågan misslyckades.");
					error = true;	
				}
				else
				{
					write("Förfrågan klar.");
					this.creditData = serviceResponse.creditCheck;
				}
			}
			else
			{
				write("Förfrågan misslyckades.");
				error = true;
			}
			
			button1.Enabled = true;
			button2.Enabled = true;

			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			System.Windows.Forms.Cursor.Hide();
		
			serviceLog.Visible = false;
			
			if (error == true) return false;

			if (creditData.status) 
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public DataCustomer getCustomer()
		{
			return returnCustomer;
		}
		#region Logger Members

		public void write(string message)
		{
			// TODO:  Add CustomerList.write implementation
			serviceLog.Items.Add(message);
			Application.DoEvents();
		}

		#endregion
	}
}
