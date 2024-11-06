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
	public class SizeList2 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.DataGridTextBoxColumn codeCol;
		private System.Windows.Forms.DataGridTextBoxColumn[] size2Col;
		private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
		
		private DataSizes dataSizes;
		private DataSizes2 dataSizes2;
		private SmartDatabase smartDatabase;
		private DataItem dataItem;
		private DataColor dataColor;
		private DataSalesHeader dataSalesHeader;

		private System.Windows.Forms.TextBox descriptionBox;
		private System.Windows.Forms.TextBox itemNoBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGrid sizeGrid;
		private System.Windows.Forms.DataGridTableStyle sizeTable;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox colorBox;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.MainMenu mainMenu2;

		private int status;
		private System.Windows.Forms.TextBox discount;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox year;
		private System.Windows.Forms.ComboBox month;
		private System.Windows.Forms.ComboBox day;
		private bool readOnly;
	
		public SizeList2(SmartDatabase smartDatabase, DataSalesHeader dataSalesHeader, DataItem dataItem, DataColor dataColor)
		{
			this.smartDatabase = smartDatabase;
			this.dataSalesHeader = dataSalesHeader;
			this.dataSizes = new DataSizes(smartDatabase, dataItem);
			this.dataSizes2 = new DataSizes2(smartDatabase, dataItem);
			this.dataItem = dataItem;
			this.dataColor = dataColor;
	
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			DataSet size2DataSet = dataSizes2.getDataSet();
			size2Col = new DataGridTextBoxColumn[100];

			int i = 0;
			while (i < size2DataSet.Tables[0].Rows.Count)
			{
				size2Col[i] = new DataGridTextBoxColumn();
				size2Col[i].HeaderText = (string)size2DataSet.Tables[0].Rows[i].ItemArray.GetValue(0);
				size2Col[i].MappingName = "quantity"+i;
				size2Col[i].NullText = "";
				size2Col[i].Width = 25;
				sizeTable.GridColumnStyles.Add(size2Col[i]);
				i++;
			}

			year.Items.Add(""+System.DateTime.Now.Year);
			year.Items.Add(""+(System.DateTime.Now.Year+1));

			int monthCounter = 1;
			while (monthCounter <= 12)
			{
				string monthString = ""+monthCounter;
				if (monthString.Length == 1) monthString = "0"+monthString;
				month.Items.Add(monthString);
				if (System.DateTime.Now.Month == monthCounter) month.Text = monthString;
				monthCounter++;
			}

			int dayCounter = 1;
			while (dayCounter <= 31)
			{
				string dayString = ""+dayCounter;
				if (dayString.Length == 1) dayString = "0"+dayString;
				day.Items.Add(dayString);
				if (System.DateTime.Now.Day == dayCounter) day.Text = dayString;
				dayCounter++;
			}

			year.Text = ""+System.DateTime.Now.Year;
			month.Text = ""+System.DateTime.Now.Month;
			day.Text = ""+System.DateTime.Now.Day;

			discount.Text = "0";
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
			this.sizeGrid = new System.Windows.Forms.DataGrid();
			this.sizeTable = new System.Windows.Forms.DataGridTableStyle();
			this.codeCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.descriptionBox = new System.Windows.Forms.TextBox();
			this.itemNoBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.colorBox = new System.Windows.Forms.TextBox();
			this.mainMenu2 = new System.Windows.Forms.MainMenu();
			this.discount = new System.Windows.Forms.TextBox();
			this.year = new System.Windows.Forms.ComboBox();
			this.month = new System.Windows.Forms.ComboBox();
			this.day = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			// 
			// sizeGrid
			// 
			this.sizeGrid.Location = new System.Drawing.Point(0, 56);
			this.sizeGrid.Size = new System.Drawing.Size(240, 128);
			this.sizeGrid.TableStyles.Add(this.sizeTable);
			this.sizeGrid.Text = "dataGrid1";
			this.sizeGrid.Click += new System.EventHandler(this.size2Grid_Click);
			// 
			// sizeTable
			// 
			this.sizeTable.GridColumnStyles.Add(this.codeCol);
			this.sizeTable.MappingName = "size";
			// 
			// codeCol
			// 
			this.codeCol.HeaderText = "Storlek";
			this.codeCol.MappingName = "code";
			this.codeCol.NullText = "";
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
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(192, 8);
			this.label3.Size = new System.Drawing.Size(40, 16);
			this.label3.Text = "Färg:";
			// 
			// colorBox
			// 
			this.colorBox.Location = new System.Drawing.Point(192, 24);
			this.colorBox.ReadOnly = true;
			this.colorBox.Size = new System.Drawing.Size(40, 20);
			this.colorBox.Text = "";
			// 
			// discount
			// 
			this.discount.Location = new System.Drawing.Point(8, 208);
			this.discount.Size = new System.Drawing.Size(48, 20);
			this.discount.Text = "";
			this.discount.GotFocus += new System.EventHandler(this.discount_GotFocus);
			// 
			// year
			// 
			this.year.Location = new System.Drawing.Point(96, 208);
			this.year.Size = new System.Drawing.Size(48, 21);
			this.year.SelectedIndexChanged += new System.EventHandler(this.year_SelectedIndexChanged);
			// 
			// month
			// 
			this.month.Location = new System.Drawing.Point(148, 208);
			this.month.Size = new System.Drawing.Size(40, 21);
			this.month.SelectedIndexChanged += new System.EventHandler(this.month_SelectedIndexChanged);
			// 
			// day
			// 
			this.day.Location = new System.Drawing.Point(192, 208);
			this.day.Size = new System.Drawing.Size(40, 21);
			this.day.SelectedIndexChanged += new System.EventHandler(this.day_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 192);
			this.label4.Size = new System.Drawing.Size(72, 16);
			this.label4.Text = "Rabatt:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(96, 192);
			this.label5.Size = new System.Drawing.Size(100, 16);
			this.label5.Text = "Leveransdatum:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(64, 211);
			this.label6.Size = new System.Drawing.Size(24, 20);
			this.label6.Text = "%";
			// 
			// SizeList2
			// 
			this.ClientSize = new System.Drawing.Size(242, 270);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.day);
			this.Controls.Add(this.month);
			this.Controls.Add(this.year);
			this.Controls.Add(this.discount);
			this.Controls.Add(this.colorBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.descriptionBox);
			this.Controls.Add(this.itemNoBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.sizeGrid);
			this.Menu = this.mainMenu2;
			this.Text = "SmartOrder - Storlekar";
			this.Load += new System.EventHandler(this.Colors_Load);

		}
		#endregion

		private void updateGrid()
		{
			
			DataSet sizeDataSet = dataSizes.getDataSet();
			DataSet size2DataSet = dataSizes2.getDataSet();

			int i = 0;
			while (i < size2DataSet.Tables[0].Rows.Count)
			{
				sizeDataSet.Tables[0].Columns.Add("quantity"+i);
				i++;
			}

			sizeGrid.DataSource = sizeDataSet.Tables[0];

			itemNoBox.Text = dataItem.no;
			descriptionBox.Text = dataItem.description;
			colorBox.Text = dataColor.code;

			if (sizeGrid.BindingContext[sizeGrid.DataSource, ""] != null)
			{
				i = 0;
				while (i < sizeGrid.BindingContext[sizeGrid.DataSource, ""].Count)
				{
					DataSize dataSize = new DataSize(sizeGrid[i, 0].ToString());

					int j = 0;
					while (j < size2DataSet.Tables[0].Rows.Count)
					{
						DataSize2 dataSize2 = new DataSize2(sizeTable.GridColumnStyles[j+1].HeaderText);
						
						DataSalesLine dataSalesLine = new DataSalesLine(dataSalesHeader, dataItem, dataColor, dataSize, dataSize2, 0, smartDatabase);
						dataSalesLine.getFromDb();
						sizeGrid[i, j+1] = dataSalesLine.quantity;
						j++;
					}

					i++;
				}
			}

		}

		public int getStatus()
		{
			return status;
		}

		private void Colors_Load(object sender, System.EventArgs e)
		{
			updateGrid();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			status = 0;
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			DataSalesLines dataSalesLines = new DataSalesLines(smartDatabase);
			dataSalesLines.setAdditionalInfo(dataSalesHeader, dataItem, dataColor, "discount = '"+discount.Text+"', deliveryDate = '"+year.Text+"-"+month.Text+"-"+day.Text+"'");
			status = 1;
			this.Close();
		}

		private void size2Grid_Click(object sender, System.EventArgs e)
		{
			if ((sizeGrid.CurrentCell.ColumnNumber > 0) && (!readOnly))
			{
				DataSize dataSize = new DataSize(sizeGrid[sizeGrid.CurrentRowIndex, 0].ToString());	
				DataSize2 dataSize2 = new DataSize2(sizeTable.GridColumnStyles[sizeGrid.CurrentCell.ColumnNumber].HeaderText);

				QuantityForm quantityForm = new QuantityForm(dataItem, dataColor, dataSize, dataSize2);
				quantityForm.setValue(sizeGrid[sizeGrid.CurrentRowIndex, sizeGrid.CurrentCell.ColumnNumber].ToString());
				quantityForm.ShowDialog();
				if (quantityForm.getStatus() == 1)
				{
					DataSalesLine dataSalesLine = new DataSalesLine(dataSalesHeader, dataItem, dataColor, dataSize, dataSize2, float.Parse(quantityForm.getValue()), smartDatabase);
					dataSalesLine.save();
				}
				updateGrid();
			}
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			status = 2;
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

		private void day_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ((year.Text != "") && (month.Text != "") && (day.Text != ""))
			{
				try
				{
					System.DateTime dateTime = new System.DateTime(int.Parse(year.Text), int.Parse(month.Text), int.Parse(day.Text));
				}
				catch (Exception f)
				{
					System.Windows.Forms.MessageBox.Show("Datumet är ogiltigt.");
					day.Text = "";
					f = null;
				}
			}
		}

		private void month_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ((year.Text != "") && (month.Text != "") && (day.Text != ""))
			{
				try
				{
					System.DateTime dateTime = new System.DateTime(int.Parse(year.Text), int.Parse(month.Text), int.Parse(day.Text));
				}
				catch (Exception f)
				{
					System.Windows.Forms.MessageBox.Show("Datumet är ogiltigt.");
					f = null;
				}
			}
		}

		private void year_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ((year.Text != "") && (month.Text != "") && (day.Text != ""))
			{
				try
				{
					System.DateTime dateTime = new System.DateTime(int.Parse(year.Text), int.Parse(month.Text), int.Parse(day.Text));
				}
				catch (Exception f)
				{
					System.Windows.Forms.MessageBox.Show("Datumet är ogiltigt.");
					f = null;
				}
			}
		}

		private void discount_GotFocus(object sender, System.EventArgs e)
		{
			QuantityForm discountForm = new QuantityForm(dataItem, dataColor);
			discountForm.setCaption("Rabatt (%):");
			discountForm.setValue(discount.Text);
			discountForm.ShowDialog();
			if (discountForm.getStatus() == 1)
			{
				discount.Text = discountForm.getValue();
				if (discount.Text == "") discount.Text = "0";
				this.Focus();
			}
		}


	}
}
