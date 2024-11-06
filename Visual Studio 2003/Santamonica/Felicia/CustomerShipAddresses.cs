using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for CustomerShipAddresses.
	/// </summary>
	public class CustomerShipAddresses : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label headerLabel;

		private SmartDatabase smartDatabase;
		private DataCustomer dataCustomer;
		private System.Windows.Forms.DataGrid customerShipAddressGrid;
		private System.Windows.Forms.DataGridTableStyle customerShipAddressTable;
		private System.Windows.Forms.DataGridTextBoxColumn nameCol;
		private System.Windows.Forms.DataGridTextBoxColumn addressCol;
		private System.Windows.Forms.DataGridTextBoxColumn cityCol;
		private int status;
	
		public CustomerShipAddresses(SmartDatabase smartDatabase, DataCustomer dataCustomer)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			this.smartDatabase = smartDatabase;
			this.dataCustomer = dataCustomer;
			this.status = 0;

			updateGrid();
		}


		private void updateGrid()
		{
			Cursor.Current = Cursors.WaitCursor;
			Cursor.Show();

			DataCustomerShipAddresses dataCustomerShipAddresses = new DataCustomerShipAddresses(smartDatabase);
			DataSet customerShipAddressDataSet = dataCustomerShipAddresses.getDataSet(dataCustomer.no);
			customerShipAddressGrid.DataSource = customerShipAddressDataSet.Tables[0];

			Cursor.Current = Cursors.Default;
			Cursor.Hide();
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
			this.button5 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.customerShipAddressGrid = new System.Windows.Forms.DataGrid();
			this.button3 = new System.Windows.Forms.Button();
			this.headerLabel = new System.Windows.Forms.Label();
			this.customerShipAddressTable = new System.Windows.Forms.DataGridTableStyle();
			this.nameCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.addressCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.cityCol = new System.Windows.Forms.DataGridTextBoxColumn();
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
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(168, 176);
			this.button2.Size = new System.Drawing.Size(64, 32);
			this.button2.Text = "Avbryt";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(240, 176);
			this.button1.Size = new System.Drawing.Size(72, 32);
			this.button1.Text = "Ok";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// customerShipAddressGrid
			// 
			this.customerShipAddressGrid.Location = new System.Drawing.Point(8, 32);
			this.customerShipAddressGrid.Size = new System.Drawing.Size(304, 136);
			this.customerShipAddressGrid.TableStyles.Add(this.customerShipAddressTable);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(96, 176);
			this.button3.Size = new System.Drawing.Size(64, 32);
			this.button3.Text = "Ingen";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// headerLabel
			// 
			this.headerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.headerLabel.Location = new System.Drawing.Point(5, 3);
			this.headerLabel.Size = new System.Drawing.Size(219, 20);
			this.headerLabel.Text = "Gårdar";
			// 
			// customerShipAddressTable
			// 
			this.customerShipAddressTable.GridColumnStyles.Add(this.nameCol);
			this.customerShipAddressTable.GridColumnStyles.Add(this.addressCol);
			this.customerShipAddressTable.GridColumnStyles.Add(this.cityCol);
			this.customerShipAddressTable.MappingName = "customerShipAddress";
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
			// cityCol
			// 
			this.cityCol.HeaderText = "Ort";
			this.cityCol.MappingName = "city";
			this.cityCol.NullText = "";
			this.cityCol.Width = 100;
			// 
			// CustomerShipAddresses
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.headerLabel);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.customerShipAddressGrid);
			this.Text = "Hämtadresser";

		}
		#endregion

		private void button4_Click(object sender, System.EventArgs e)
		{
			if ((this.customerShipAddressGrid.DataSource != null) && (((DataTable)this.customerShipAddressGrid.DataSource).Rows.Count > 0))
			{
				if (this.customerShipAddressGrid.CurrentRowIndex > 0)
				{
					this.customerShipAddressGrid.CurrentRowIndex = this.customerShipAddressGrid.CurrentRowIndex - 1;
					this.customerShipAddressGrid.Select(this.customerShipAddressGrid.CurrentRowIndex);
				}
			}

		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			if ((this.customerShipAddressGrid.DataSource != null) && (((DataTable)this.customerShipAddressGrid.DataSource).Rows.Count > 0))
			{
				if (this.customerShipAddressGrid.CurrentRowIndex < (((DataTable)this.customerShipAddressGrid.DataSource).Rows.Count -1))
				{
					this.customerShipAddressGrid.CurrentRowIndex = this.customerShipAddressGrid.CurrentRowIndex + 1;
					this.customerShipAddressGrid.Select(this.customerShipAddressGrid.CurrentRowIndex);
				}
			}

		}

		public int getStatus()
		{
			return status;
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			status = 0;
			this.Close();
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			this.status = 2;
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			DataTable customerShipAddressTable = (DataTable)customerShipAddressGrid.DataSource;
			if (customerShipAddressTable.Rows.Count > 0)
			{
				this.status = 1;
				this.Close();
			}
			else
			{
				MessageBox.Show("Du måste välja en gård, eller klicka på 'Ingen'.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			}
		}


		public string getCustomerShipAddressNo()
		{
			if (customerShipAddressGrid.DataSource != null)
			{
				DataTable customerShipAddressTable = (DataTable)customerShipAddressGrid.DataSource;
				if (customerShipAddressTable.Rows.Count > 0)
				{
					return customerShipAddressTable.Rows[customerShipAddressGrid.CurrentRowIndex].ItemArray.GetValue(0).ToString();
				}
			}
			return null;
		}

	}
}
