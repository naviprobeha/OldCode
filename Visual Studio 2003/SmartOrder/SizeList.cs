using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SmartOrder
{
	/// <summary>
	/// Summary description for Colors.
	/// </summary>
	public class SizeList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.DataGridTextBoxColumn codeCol;
		private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
		
		private DataSizes dataSizes;
		private SmartDatabase smartDatabase;
		private DataSet sizeDataSet;
		private DataItem dataItem;
		private DataSize selectedSize;
		private DataColor dataColor;
		private DataSalesHeader dataSalesHeader;

		private System.Windows.Forms.DataGrid sizeGrid;
		private System.Windows.Forms.DataGridTableStyle sizeTable;
		private System.Windows.Forms.TextBox descriptionBox;
		private System.Windows.Forms.TextBox itemNoBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox colorBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DataGridTextBoxColumn quantityCol;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;

		private int status;
		private bool readOnly;
	
		public SizeList(SmartDatabase smartDatabase, DataSalesHeader dataSalesHeader, DataItem dataItem, DataColor dataColor)
		{
			this.smartDatabase = smartDatabase;
			this.dataSizes = new DataSizes(smartDatabase, dataItem);
			this.dataItem = dataItem;
			this.dataColor = dataColor;
			this.dataSalesHeader = dataSalesHeader;
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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
			this.sizeGrid = new System.Windows.Forms.DataGrid();
			this.sizeTable = new System.Windows.Forms.DataGridTableStyle();
			this.codeCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.quantityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.descriptionBox = new System.Windows.Forms.TextBox();
			this.itemNoBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.colorBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			// 
			// sizeGrid
			// 
			this.sizeGrid.Location = new System.Drawing.Point(0, 56);
			this.sizeGrid.Size = new System.Drawing.Size(240, 176);
			this.sizeGrid.TableStyles.Add(this.sizeTable);
			this.sizeGrid.Text = "dataGrid1";
			this.sizeGrid.Click += new System.EventHandler(this.sizeGrid_Click);
			// 
			// sizeTable
			// 
			this.sizeTable.GridColumnStyles.Add(this.codeCol);
			this.sizeTable.GridColumnStyles.Add(this.quantityCol);
			this.sizeTable.MappingName = "size";
			// 
			// codeCol
			// 
			this.codeCol.HeaderText = "Kod";
			this.codeCol.MappingName = "code";
			this.codeCol.NullText = "";
			// 
			// quantityCol
			// 
			this.quantityCol.HeaderText = "Antal";
			this.quantityCol.MappingName = "quantity";
			this.quantityCol.NullText = "";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(176, 240);
			this.button1.Size = new System.Drawing.Size(56, 20);
			this.button1.Text = "Klar";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(88, 240);
			this.button2.Size = new System.Drawing.Size(80, 20);
			this.button2.Text = "Ny färg";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// descriptionBox
			// 
			this.descriptionBox.Location = new System.Drawing.Point(80, 24);
			this.descriptionBox.ReadOnly = true;
			this.descriptionBox.Size = new System.Drawing.Size(104, 20);
			this.descriptionBox.Text = "";
			// 
			// itemNoBox
			// 
			this.itemNoBox.Location = new System.Drawing.Point(8, 24);
			this.itemNoBox.ReadOnly = true;
			this.itemNoBox.Size = new System.Drawing.Size(64, 20);
			this.itemNoBox.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(80, 8);
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.Text = "Beskrivning:";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Size = new System.Drawing.Size(56, 16);
			this.label1.Text = "Artikelnr:";
			// 
			// colorBox
			// 
			this.colorBox.Location = new System.Drawing.Point(192, 24);
			this.colorBox.ReadOnly = true;
			this.colorBox.Size = new System.Drawing.Size(40, 20);
			this.colorBox.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(192, 8);
			this.label3.Size = new System.Drawing.Size(40, 16);
			this.label3.Text = "Färg:";
			// 
			// SizeList
			// 
			this.Controls.Add(this.colorBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.descriptionBox);
			this.Controls.Add(this.itemNoBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.sizeGrid);
			this.Menu = this.mainMenu1;
			this.Text = "SmartOrder - Storlekar";
			this.Load += new System.EventHandler(this.Colors_Load);

		}
		#endregion

		private void updateGrid()
		{
			sizeDataSet = dataSizes.getDataSet();

			sizeDataSet.Tables[0].Columns.Add("quantity");

			sizeGrid.DataSource = sizeDataSet.Tables[0];

			itemNoBox.Text = dataItem.no;
			descriptionBox.Text = dataItem.description;
			colorBox.Text = dataColor.code;

			if (sizeGrid.BindingContext[sizeGrid.DataSource, ""] != null)
			{
				int i = 0;
				while (i < sizeGrid.BindingContext[sizeGrid.DataSource, ""].Count)
				{
					DataSize dataSize = new DataSize(sizeGrid[i, 0].ToString());

					DataSalesLine dataSalesLine = new DataSalesLine(dataSalesHeader, dataItem, dataColor, dataSize, null, 0, smartDatabase);
					dataSalesLine.getFromDb();
					sizeGrid[i, 1] = dataSalesLine.quantity;

					i++;
				}
			}
		}

		public DataSize getSelected()
		{
			return selectedSize;
		}

		public void setSelected(DataSize dataSize)
		{
			selectedSize = dataSize;
		}

		public int getStatus()
		{
			return status;
		}


		private void Colors_Load(object sender, System.EventArgs e)
		{
			updateGrid();
		}

		private void sizeGrid_Click(object sender, System.EventArgs e)
		{
			if ((sizeGrid.CurrentCell.ColumnNumber > 0) && (!readOnly))
			{
				DataSize dataSize = new DataSize(sizeGrid[sizeGrid.CurrentRowIndex, 0].ToString());	

				QuantityForm quantityForm = new QuantityForm(dataItem, dataColor, dataSize, null);
				quantityForm.setValue(sizeGrid[sizeGrid.CurrentRowIndex, 1].ToString());
				quantityForm.ShowDialog();
				if (quantityForm.getStatus() == 1)
				{
					DataSalesLine dataSalesLine = new DataSalesLine(dataSalesHeader, dataItem, dataColor, dataSize, null, float.Parse(quantityForm.getValue()), smartDatabase);
					dataSalesLine.save();
				}
				updateGrid();
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			status = 0;
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			status = 1;
			this.Close();
		}

		public void hideButtons()
		{
			this.button2.Visible = false;
		}

		public void setReadOnly()
		{
			this.readOnly = true;
		}
	}
}
