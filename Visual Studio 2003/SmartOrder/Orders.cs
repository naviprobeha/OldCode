using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SmartOrder
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

		private DataSalesHeaders dataSalesHeaders;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.DataGrid orderGrid;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.DataGrid readyOrderGrid;
		private System.Windows.Forms.DataGridTableStyle readyOrderTable;
		private System.Windows.Forms.DataGridTextBoxColumn orderNoCol2;
		private System.Windows.Forms.DataGridTextBoxColumn customerNoCol2;
		private System.Windows.Forms.DataGridTextBoxColumn customerNameCol2;
		private SmartDatabase smartDatabase;

		public Orders(SmartDatabase smartDatabase)
		{
			this.smartDatabase = smartDatabase;
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
			this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.orderTable = new System.Windows.Forms.DataGridTableStyle();
			this.orderNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.customerNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.customerNameCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.button1 = new System.Windows.Forms.Button();
			this.orderGrid = new System.Windows.Forms.DataGrid();
			this.label1 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.readyOrderGrid = new System.Windows.Forms.DataGrid();
			this.readyOrderTable = new System.Windows.Forms.DataGridTableStyle();
			this.orderNoCol2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.customerNoCol2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.customerNameCol2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.button2 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
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
			this.orderNoCol.MappingName = "no";
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
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(250, 272);
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.button1);
			this.tabPage1.Controls.Add(this.orderGrid);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Location = new System.Drawing.Point(4, 4);
			this.tabPage1.Size = new System.Drawing.Size(242, 246);
			this.tabPage1.Text = "Under bearbetning";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(160, 216);
			this.button1.Text = "Visa";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// orderGrid
			// 
			this.orderGrid.Location = new System.Drawing.Point(0, 32);
			this.orderGrid.Size = new System.Drawing.Size(240, 176);
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
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.readyOrderGrid);
			this.tabPage2.Controls.Add(this.button2);
			this.tabPage2.Controls.Add(this.label2);
			this.tabPage2.Location = new System.Drawing.Point(4, 4);
			this.tabPage2.Size = new System.Drawing.Size(242, 246);
			this.tabPage2.Text = "Klara";
			// 
			// readyOrderGrid
			// 
			this.readyOrderGrid.Location = new System.Drawing.Point(0, 32);
			this.readyOrderGrid.Size = new System.Drawing.Size(240, 176);
			this.readyOrderGrid.TableStyles.Add(this.readyOrderTable);
			this.readyOrderGrid.Text = "dataGrid1";
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
			this.orderNoCol2.MappingName = "no";
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
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(160, 216);
			this.button2.Text = "Visa";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Size = new System.Drawing.Size(144, 20);
			this.label2.Text = "Klarmarkerade order:";
			// 
			// Orders
			// 
			this.ClientSize = new System.Drawing.Size(250, 270);
			this.Controls.Add(this.tabControl1);
			this.Menu = this.mainMenu1;
			this.Text = "Lagda order";
			this.Load += new System.EventHandler(this.Orders_Load);

		}
		#endregion

		private void Orders_Load(object sender, System.EventArgs e)
		{
			updateGrid();
		}

		private void updateGrid()
		{
			readyOrderDataSet = dataSalesHeaders.getDataSet(true);
			readyOrderGrid.DataSource = readyOrderDataSet.Tables[0];
			orderDataSet = dataSalesHeaders.getDataSet(false);
			orderGrid.DataSource = orderDataSet.Tables[0];
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			DataSalesHeader dataSalesHeader = new DataSalesHeader((int)orderGrid[orderGrid.CurrentRowIndex, 0], smartDatabase);
			Order order = new Order(dataSalesHeader, smartDatabase);
			order.ShowDialog();
			updateGrid();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			DataSalesHeader dataSalesHeader = new DataSalesHeader((int)readyOrderGrid[readyOrderGrid.CurrentRowIndex, 0], smartDatabase);
			Order order = new Order(dataSalesHeader, smartDatabase);
			order.ShowDialog();
			updateGrid();		
		}

	}
}
