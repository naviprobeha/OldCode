using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SmartShipment
{
	/// <summary>
	/// Summary description for Orders.
	/// </summary>
	public class Orders : System.Windows.Forms.Form
	{
		private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
		private System.Windows.Forms.DataGridTableStyle orderTable;
		private System.Windows.Forms.DataGridTextBoxColumn orderNoCol;
		private System.Windows.Forms.DataGridTextBoxColumn customerNoCol;
		private System.Windows.Forms.DataGridTextBoxColumn customerNameCol;
		private System.Windows.Forms.MainMenu mainMenu1;

		private DataSet orderDataSet;
		private DataSet readyOrderDataSet;
		private DataSetup dataSetup;

		private DataSalesHeaders dataSalesHeaders;
		private System.Windows.Forms.DataGridTableStyle readyOrderTable;
		private System.Windows.Forms.DataGridTextBoxColumn orderNoCol2;
		private System.Windows.Forms.DataGridTextBoxColumn customerNoCol2;
		private System.Windows.Forms.DataGridTextBoxColumn customerNameCol2;
		private System.Windows.Forms.DataGrid orderGrid;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private SmartDatabase smartDatabase;

		public Orders(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
			this.dataSetup = smartDatabase.getSetup();
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			dataSalesHeaders = new DataSalesHeaders(smartDatabase);
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
			this.orderTable = new System.Windows.Forms.DataGridTableStyle();
			this.orderNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.customerNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.customerNameCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.readyOrderTable = new System.Windows.Forms.DataGridTableStyle();
			this.orderNoCol2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.customerNoCol2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.customerNameCol2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.orderGrid = new System.Windows.Forms.DataGrid();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			// 
			// orderTable
			// 
			this.orderTable.GridColumnStyles.Add(this.orderNoCol);
			this.orderTable.GridColumnStyles.Add(this.customerNoCol);
			this.orderTable.GridColumnStyles.Add(this.customerNameCol);
			this.orderTable.MappingName = "salesHeader";
			// 
			// orderNoCol
			// 
			this.orderNoCol.HeaderText = "Ordernr";
			this.orderNoCol.MappingName = "orderNo";
			this.orderNoCol.NullText = "";
			// 
			// customerNoCol
			// 
			this.customerNoCol.HeaderText = "Kundnr";
			this.customerNoCol.MappingName = "customerNo";
			this.customerNoCol.NullText = "";
			// 
			// customerNameCol
			// 
			this.customerNameCol.HeaderText = "Kundnamn";
			this.customerNameCol.MappingName = "name";
			this.customerNameCol.NullText = "";
			this.customerNameCol.Width = 150;
			// 
			// readyOrderTable
			// 
			this.readyOrderTable.GridColumnStyles.Add(this.orderNoCol2);
			this.readyOrderTable.GridColumnStyles.Add(this.customerNoCol2);
			this.readyOrderTable.GridColumnStyles.Add(this.customerNameCol2);
			this.readyOrderTable.MappingName = "salesHeader";
			// 
			// orderNoCol2
			// 
			this.orderNoCol2.HeaderText = "Ordernr";
			this.orderNoCol2.MappingName = "orderNo";
			this.orderNoCol2.NullText = "";
			// 
			// customerNoCol2
			// 
			this.customerNoCol2.HeaderText = "Kundnr";
			this.customerNoCol2.MappingName = "customerNo";
			this.customerNoCol2.NullText = "";
			// 
			// customerNameCol2
			// 
			this.customerNameCol2.HeaderText = "Kundnamn";
			this.customerNameCol2.MappingName = "name";
			this.customerNameCol2.NullText = "";
			this.customerNameCol2.Width = 150;
			// 
			// orderGrid
			// 
			this.orderGrid.Location = new System.Drawing.Point(0, 32);
			this.orderGrid.Size = new System.Drawing.Size(240, 184);
			this.orderGrid.TableStyles.Add(this.orderTable);
			this.orderGrid.Text = "orderGrid";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Size = new System.Drawing.Size(192, 16);
			this.label1.Text = "Order under bearbetning:";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
			this.button1.Location = new System.Drawing.Point(128, 224);
			this.button1.Size = new System.Drawing.Size(104, 40);
			this.button1.Text = "Visa";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Orders
			// 
			this.ClientSize = new System.Drawing.Size(242, 270);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.orderGrid);
			this.Controls.Add(this.label1);
			this.Menu = this.mainMenu1;
			this.Text = "Lagda order";
			this.Load += new System.EventHandler(this.Orders_Load);

		}
		#endregion

		private void Orders_Load(object sender, System.EventArgs e)
		{
			updateGrid();

			orderGrid.Focus();
		}

		private void updateGrid()
		{
			orderDataSet = dataSalesHeaders.getDataSet(false);
			orderGrid.DataSource = orderDataSet.Tables[0];
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (orderGrid.BindingContext[orderGrid.DataSource, ""].Count > 0)
			{
				DataTable dataTable = (DataTable)orderGrid.DataSource;
						
				DataSalesHeader dataSalesHeader = new DataSalesHeader((int)dataTable.Rows[orderGrid.CurrentRowIndex].ItemArray.GetValue(0), smartDatabase);

				OrderHeader order = new OrderHeader(smartDatabase, dataSalesHeader);
				order.ShowDialog();

				order.Dispose();

				updateGrid();
			}


		}

		private void orderGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == 13) button1_Click(sender, null);
		}

	}
}
