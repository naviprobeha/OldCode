using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SmartOrder
{
	/// <summary>
	/// Summary description for ItemList.
	/// </summary>
	public class SizeGrid : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.DataGridTableStyle itemTable;
	
		private SmartDatabase smartDatabase;
		private DataSalesLines dataSalesLines;
		private DataSalesLine selectedSalesLine;
		private DataItem dataItem;

		private DataSalesHeader dataSalesHeader;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.DataGridTextBoxColumn colorCol;
		private System.Windows.Forms.DataGridTextBoxColumn sizeCol;
		private System.Windows.Forms.DataGridTextBoxColumn size2Col;
		private System.Windows.Forms.DataGridTextBoxColumn quantityCol;
		private System.Windows.Forms.DataGrid salesLineGrid;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox itemNoBox;
		private System.Windows.Forms.TextBox descriptionBox;
		private int status;

		public SizeGrid(SmartDatabase smartDatabase, DataSalesHeader dataSalesHeader, DataItem dataItem)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.smartDatabase = smartDatabase;
			this.dataSalesLines = new DataSalesLines(smartDatabase);
			this.dataSalesHeader = dataSalesHeader;
			this.dataItem = dataItem;

			status = 0;
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
			this.salesLineGrid = new System.Windows.Forms.DataGrid();
			this.itemTable = new System.Windows.Forms.DataGridTableStyle();
			this.colorCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.sizeCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.size2Col = new System.Windows.Forms.DataGridTextBoxColumn();
			this.quantityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.itemNoBox = new System.Windows.Forms.TextBox();
			this.descriptionBox = new System.Windows.Forms.TextBox();
			// 
			// salesLineGrid
			// 
			this.salesLineGrid.Location = new System.Drawing.Point(0, 56);
			this.salesLineGrid.Size = new System.Drawing.Size(240, 176);
			this.salesLineGrid.TableStyles.Add(this.itemTable);
			this.salesLineGrid.Text = "dataGrid1";
			this.salesLineGrid.Click += new System.EventHandler(this.itemGrid_Click);
			// 
			// itemTable
			// 
			this.itemTable.GridColumnStyles.Add(this.colorCol);
			this.itemTable.GridColumnStyles.Add(this.sizeCol);
			this.itemTable.GridColumnStyles.Add(this.size2Col);
			this.itemTable.GridColumnStyles.Add(this.quantityCol);
			this.itemTable.MappingName = "salesLine";
			// 
			// colorCol
			// 
			this.colorCol.HeaderText = "Färg";
			this.colorCol.MappingName = "colorCode";
			this.colorCol.NullText = "";
			// 
			// sizeCol
			// 
			this.sizeCol.HeaderText = "Storlek";
			this.sizeCol.MappingName = "sizeCode";
			this.sizeCol.NullText = "";
			// 
			// size2Col
			// 
			this.size2Col.HeaderText = "Kupa";
			this.size2Col.MappingName = "size2Code";
			this.size2Col.NullText = "";
			// 
			// quantityCol
			// 
			this.quantityCol.HeaderText = "Antal";
			this.quantityCol.MappingName = "quantity";
			this.quantityCol.NullText = "";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(160, 240);
			this.button1.Text = "Klar";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(80, 240);
			this.button2.Text = "Lägg till";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.Text = "Artikelnr:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(80, 8);
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.Text = "Beskrivning:";
			// 
			// itemNoBox
			// 
			this.itemNoBox.Location = new System.Drawing.Point(8, 24);
			this.itemNoBox.ReadOnly = true;
			this.itemNoBox.Size = new System.Drawing.Size(64, 20);
			this.itemNoBox.Text = "";
			// 
			// descriptionBox
			// 
			this.descriptionBox.Location = new System.Drawing.Point(80, 24);
			this.descriptionBox.ReadOnly = true;
			this.descriptionBox.Size = new System.Drawing.Size(152, 20);
			this.descriptionBox.Text = "";
			// 
			// SizeGrid
			// 
			this.Controls.Add(this.descriptionBox);
			this.Controls.Add(this.itemNoBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.salesLineGrid);
			this.Menu = this.mainMenu1;
			this.Text = "SmartOrder - Matris";
			this.Load += new System.EventHandler(this.ItemList_Load);

		}
		#endregion

		private void ItemList_Load(object sender, System.EventArgs e)
		{
			updateGrid();
		}

		private void updateGrid()
		{
			System.Data.DataSet salesLineDataSet = dataSalesLines.getDataSet(dataSalesHeader, dataItem);
			salesLineGrid.DataSource = salesLineDataSet.Tables[0];
			
			itemNoBox.Text = dataItem.no;
			descriptionBox.Text = dataItem.description;
		}

		private void itemGrid_Click(object sender, System.EventArgs e)
		{
			DataTable salesLineDataTable = (DataTable)salesLineGrid.DataSource;
			selectedSalesLine = new DataSalesLine((int)salesLineDataTable.Rows[salesLineGrid.CurrentRowIndex].ItemArray.GetValue(0), smartDatabase);
		}

		public int getStatus()
		{
			return status;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			status = 1;
			this.Close();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			status = 2;
			this.Close();
		}
	}
}
