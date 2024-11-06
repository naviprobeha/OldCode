using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for OrganizationLocationList.
	/// </summary>
	public class OrganizationLocationList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label9;
	
		private SmartDatabase smartDatabase;
		private string selectedShippingCustomerNo;
		private DataSet locationDataSet;
		private System.Windows.Forms.DataGridTableStyle locationTable;
		private System.Windows.Forms.DataGridTextBoxColumn shippingCustomerNoCol;
		private System.Windows.Forms.DataGridTextBoxColumn nameCol;
		private System.Windows.Forms.DataGrid locationGrid;

		public OrganizationLocationList(SmartDatabase smartDatabase)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.smartDatabase = smartDatabase;
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			updateGrid();

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		private void updateGrid()
		{
			DataOrganizationLocations dataOrganizationLocations = new DataOrganizationLocations(this.smartDatabase);
			locationDataSet = dataOrganizationLocations.getDataSet();
			locationGrid.DataSource = locationDataSet.Tables[0];
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label9 = new System.Windows.Forms.Label();
			this.locationGrid = new System.Windows.Forms.DataGrid();
			this.button1 = new System.Windows.Forms.Button();
			this.locationTable = new System.Windows.Forms.DataGridTableStyle();
			this.shippingCustomerNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.nameCol = new System.Windows.Forms.DataGridTextBoxColumn();
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label9.Location = new System.Drawing.Point(5, 3);
			this.label9.Size = new System.Drawing.Size(219, 20);
			this.label9.Text = "Omlastningsplats";
			// 
			// locationGrid
			// 
			this.locationGrid.Location = new System.Drawing.Point(8, 32);
			this.locationGrid.Size = new System.Drawing.Size(304, 128);
			this.locationGrid.TableStyles.Add(this.locationTable);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(240, 168);
			this.button1.Size = new System.Drawing.Size(72, 32);
			this.button1.Text = "OK";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// locationTable
			// 
			this.locationTable.GridColumnStyles.Add(this.shippingCustomerNoCol);
			this.locationTable.GridColumnStyles.Add(this.nameCol);
			this.locationTable.MappingName = "organizationLocation";
			// 
			// shippingCustomerNoCol
			// 
			this.shippingCustomerNoCol.HeaderText = "Nr";
			this.shippingCustomerNoCol.MappingName = "shippingCustomerNo";
			this.shippingCustomerNoCol.NullText = "";
			// 
			// nameCol
			// 
			this.nameCol.HeaderText = "Namn";
			this.nameCol.MappingName = "name";
			this.nameCol.NullText = "";
			this.nameCol.Width = 250;
			// 
			// OrganizationLocationList
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.locationGrid);
			this.Controls.Add(this.label9);
			this.Text = "Omlastningsplats";

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (locationGrid.CurrentRowIndex > -1)
			{
				this.selectedShippingCustomerNo = locationGrid[locationGrid.CurrentRowIndex, 0].ToString();
				this.Close();
			}
		}

		public string getOrganizationLocation()
		{
			return this.selectedShippingCustomerNo;
		}
	}
}
