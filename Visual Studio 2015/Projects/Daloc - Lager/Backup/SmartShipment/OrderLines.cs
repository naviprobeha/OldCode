using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace SmartShipment
{
	/// <summary>
	/// Summary description for OrderLines.
	/// </summary>
	public class OrderLines : System.Windows.Forms.Form, Logger
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox scanBox;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.DataGridTextBoxColumn itemNoCol;
		private System.Windows.Forms.DataGridTextBoxColumn descriptionCol;
		private System.Windows.Forms.DataGridTextBoxColumn hangingCol;
		private System.Windows.Forms.DataGridTextBoxColumn unitPriceCol;
		private System.Windows.Forms.DataGridTextBoxColumn amountCol;
		private System.Windows.Forms.DataGridTextBoxColumn quantityCol;
		private System.Windows.Forms.DataGrid salesLineGrid;
	
		private DataSalesHeader dataSalesHeader;
		private System.Windows.Forms.DataGridTableStyle salesLineTable;
		private DataSalesLines dataSalesLines;
		private SmartDatabase smartDatabase;
		private System.Windows.Forms.ListBox serviceLog;
		private DataSet salesLineDataSet;
		private System.Windows.Forms.MainMenu mainMenu1;
		private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button5;
		private DataSetup dataSetup;
		private System.Windows.Forms.DataGridTextBoxColumn discountCol;
		private int status;

		public OrderLines(DataSalesHeader dataSalesHeader, SmartDatabase smartDatabase, DataSetup dataSetup)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.dataSalesHeader = dataSalesHeader;
			this.smartDatabase = smartDatabase;
			this.dataSetup = dataSetup;

			serviceLog.Height = 168;
			serviceLog.Width = 240;

			this.Text = "Order "+dataSetup.getAgent().agentId+dataSalesHeader.no.ToString();
			dataSalesLines = new DataSalesLines(smartDatabase);
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
			this.salesLineTable = new System.Windows.Forms.DataGridTableStyle();
			this.itemNoCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.descriptionCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.hangingCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.quantityCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.unitPriceCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.amountCol = new System.Windows.Forms.DataGridTextBoxColumn();
			this.label1 = new System.Windows.Forms.Label();
			this.scanBox = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.serviceLog = new System.Windows.Forms.ListBox();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.discountCol = new System.Windows.Forms.DataGridTextBoxColumn();
			// 
			// salesLineGrid
			// 
			this.salesLineGrid.Location = new System.Drawing.Point(0, 40);
			this.salesLineGrid.Size = new System.Drawing.Size(240, 152);
			this.salesLineGrid.TableStyles.Add(this.salesLineTable);
			this.salesLineGrid.Text = "salesLineGrid";
			// 
			// salesLineTable
			// 
			this.salesLineTable.GridColumnStyles.Add(this.itemNoCol);
			this.salesLineTable.GridColumnStyles.Add(this.descriptionCol);
			this.salesLineTable.GridColumnStyles.Add(this.hangingCol);
			this.salesLineTable.GridColumnStyles.Add(this.quantityCol);
			this.salesLineTable.GridColumnStyles.Add(this.unitPriceCol);
			this.salesLineTable.GridColumnStyles.Add(this.discountCol);
			this.salesLineTable.GridColumnStyles.Add(this.amountCol);
			this.salesLineTable.MappingName = "salesLine";
			// 
			// itemNoCol
			// 
			this.itemNoCol.HeaderText = "Artikelnr";
			this.itemNoCol.MappingName = "itemNo";
			this.itemNoCol.NullText = "";
			this.itemNoCol.Width = 80;
			// 
			// descriptionCol
			// 
			this.descriptionCol.HeaderText = "Beskrivning";
			this.descriptionCol.MappingName = "description";
			this.descriptionCol.NullText = "";
			this.descriptionCol.Width = 100;
			// 
			// hangingCol
			// 
			this.hangingCol.HeaderText = "Hängning";
			this.hangingCol.MappingName = "hanging";
			this.hangingCol.NullText = "";
			// 
			// quantityCol
			// 
			this.quantityCol.HeaderText = "Antal";
			this.quantityCol.MappingName = "quantity";
			this.quantityCol.NullText = "";
			// 
			// unitPriceCol
			// 
			this.unitPriceCol.HeaderText = "A-pris";
			this.unitPriceCol.MappingName = "lineUnitPrice";
			this.unitPriceCol.NullText = "";
			// 
			// amountCol
			// 
			this.amountCol.HeaderText = "Belopp";
			this.amountCol.MappingName = "lineAmount";
			this.amountCol.NullText = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 11);
			this.label1.Size = new System.Drawing.Size(56, 20);
			this.label1.Text = "Scanna:";
			// 
			// scanBox
			// 
			this.scanBox.Location = new System.Drawing.Point(72, 8);
			this.scanBox.Size = new System.Drawing.Size(160, 20);
			this.scanBox.Text = "";
			this.scanBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scanBox_KeyPress);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
			this.button1.Location = new System.Drawing.Point(128, 224);
			this.button1.Size = new System.Drawing.Size(104, 40);
			this.button1.Text = "Slutför";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular);
			this.button2.Location = new System.Drawing.Point(8, 224);
			this.button2.Size = new System.Drawing.Size(104, 40);
			this.button2.Text = "Föreg.";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// serviceLog
			// 
			this.serviceLog.Location = new System.Drawing.Point(0, 40);
			this.serviceLog.Size = new System.Drawing.Size(200, 119);
			this.serviceLog.Visible = false;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(88, 200);
			this.button4.Text = "Ta bort";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(8, 200);
			this.button3.Text = "Lägg till";
			this.button3.Click += new System.EventHandler(this.button3_Click_1);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(168, 200);
			this.button5.Size = new System.Drawing.Size(64, 20);
			this.button5.Text = "Ändra";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// discountCol
			// 
			this.discountCol.HeaderText = "Rabatt %";
			this.discountCol.MappingName = "discount";
			this.discountCol.NullText = "";
			// 
			// OrderLines
			// 
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.serviceLog);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.scanBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.salesLineGrid);
			this.Menu = this.mainMenu1;
			this.Text = "Orderrader";
			this.Load += new System.EventHandler(this.OrderLines_Load);

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			status = 0;
			this.Close();
		}

		private void updateGrid()
		{
			salesLineDataSet = dataSalesLines.getDataSet(dataSalesHeader);
			salesLineGrid.DataSource = salesLineDataSet.Tables[0];

			DataColumn lineUnitPriceCol = salesLineDataSet.Tables[0].Columns.Add("lineUnitPrice");
			DataColumn lineAmountCol = salesLineDataSet.Tables[0].Columns.Add("lineAmount");

			int i = 0;

			if (salesLineDataSet.Tables[0].Rows.Count > 0)
			{
				while (i < salesLineDataSet.Tables[0].Rows.Count)
				{
			
					salesLineGrid[i, salesLineTable.GridColumnStyles.IndexOf(this.unitPriceCol)] = String.Format("{0:f}", salesLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(8));
					salesLineGrid[i, salesLineTable.GridColumnStyles.IndexOf(this.amountCol)] = String.Format("{0:f}", salesLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(9));

					i++;
				}
	
				salesLineGrid.CurrentRowIndex = 0;
			}

		}

		private void OrderLines_Load(object sender, System.EventArgs e)
		{
			updateGrid();

			this.scanBox.Focus();
		}

		private void fetchUnitPrice(ref DataItem dataItem)
		{
			serviceLog.Items.Clear();
			serviceLog.Items.Add("Hämtar prislista...");
			serviceLog.Visible = true;
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			System.Windows.Forms.Cursor.Show();

			bool error = false;

			disableButtons();

			Service synchService = new Service("priceRequest", smartDatabase, dataSetup);
			synchService.setLogger(this);

			ItemPrice itemPrice = new ItemPrice(smartDatabase, new DataCustomer(dataSalesHeader.customerNo), dataItem);
			ItemPrice itemPriceResponse = new ItemPrice(smartDatabase, new DataCustomer(dataSalesHeader.customerNo), dataItem);

			synchService.serviceRequest.setServiceArgument(itemPrice);

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
					itemPriceResponse = serviceResponse.itemPrice;
				}
			}
			else
			{
				write("Förfrågan misslyckades.");
				error = true;
			}
			
			enableButtons();

			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			System.Windows.Forms.Cursor.Hide();
		
			serviceLog.Visible = false;
			
			if (error == true) return;

			dataItem.price = itemPriceResponse.unitPrice;
			dataItem.discount = itemPriceResponse.discount;
		}
		#region Logger Members

		public void write(string message)
		{
			// TODO:  Add OrderLines.write implementation
			serviceLog.Items.Add(message);
			Application.DoEvents();
		}

		#endregion

		private void scanBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ((e.KeyChar == 13) || (e.KeyChar == '>'))
			{
				e.Handled = true;
				DataReference reference = new DataReference(scanBox.Text);
				reference.customerNo = dataSalesHeader.customerNo;
				DataReference responseReference = fetchSerialReference(reference);

				if (responseReference != null)
				{
					if (responseReference.no != "")
					{
						Sound sound = new Sound(Sound.SOUND_TYPE_SUCCESS);

						DataSalesLine dataSalesLine = new DataSalesLine(dataSalesHeader, smartDatabase);
						dataSalesLine.itemNo = responseReference.itemNo;
						dataSalesLine.description = responseReference.description;
						dataSalesLine.unitPrice = responseReference.unitPrice * (1 - (responseReference.discount / 100));
						dataSalesLine.discount = responseReference.discount;
						dataSalesLine.baseUnit = responseReference.baseUnit;
						dataSalesLine.hanging = responseReference.hanging;
						dataSalesLine.referenceNo = responseReference.no;

						string month = System.DateTime.Today.Month.ToString();
						string day = System.DateTime.Today.Day.ToString();
						if (month.Length == 1) month = "0" + month;
						if (day.Length == 1) day = "0" + day;

						dataSalesLine.deliveryDate = System.DateTime.Today.Year.ToString()+"-"+month+"-"+day;

						dataSalesLine.quantity = 1;
						dataSalesLine.amount = dataSalesLine.quantity * dataSalesLine.unitPrice;
						dataSalesLine.save();

						updateGrid();
					}
					else
					{
						Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
					}
				}
				else
				{
					Sound sound = new Sound(Sound.SOUND_TYPE_FAIL);
				}

				e.Handled = true;
				scanBox.Text = "";
				scanBox.Focus();
			}

		}


		private DataReference fetchSerialReference(DataReference dataReference)
		{
			DataReference responseReference = null;

			serviceLog.Items.Clear();
			serviceLog.Items.Add("Hämtar lagerartikel...");
			serviceLog.Visible = true;
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			System.Windows.Forms.Cursor.Show();

			disableButtons();

			Service synchService = new Service("referenceRequest", smartDatabase, dataSetup);
			synchService.setLogger(this);

			synchService.serviceRequest.setServiceArgument(dataReference);

			ServiceResponse serviceResponse = synchService.performService();

			if (serviceResponse != null)
			{
				if (serviceResponse.hasErrors)
				{
					System.Windows.Forms.MessageBox.Show(serviceResponse.error.status+": "+serviceResponse.error.description, "Fel", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
					write("Förfrågan misslyckades.");
				}
				else
				{
					write("Förfrågan klar.");
					responseReference = serviceResponse.reference;
				}
			}
			else
			{
				write("Förfrågan misslyckades.");
			}
			
			enableButtons();

			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
			System.Windows.Forms.Cursor.Hide();
		
			serviceLog.Visible = false;
			
			return responseReference;
		}

		private void button3_Click_1(object sender, System.EventArgs e)
		{
			ItemList itemList = new ItemList(smartDatabase);
			itemList.ShowDialog();
			if (itemList.getItem() != null)
			{
				DataItem dataItem = itemList.getItem();

				DataSalesLine dataSalesLine = new DataSalesLine(dataSalesHeader, smartDatabase);
				dataSalesLine.itemNo = dataItem.no;
				dataSalesLine.description = dataItem.description;
				dataSalesLine.unitPrice = dataItem.price;

				string month = System.DateTime.Today.Month.ToString();
				string day = System.DateTime.Today.Day.ToString();
				if (month.Length == 1) month = "0" + month;
				if (day.Length == 1) day = "0" + day;

				dataSalesLine.deliveryDate = System.DateTime.Today.Year.ToString()+"-"+month+"-"+day;

				fetchUnitPrice(ref dataItem);

				QuantityForm quantityForm = new QuantityForm(dataItem);

				quantityForm.ShowDialog();
				if (quantityForm.getStatus() == 1)
				{
					dataSalesLine.quantity = float.Parse(quantityForm.getValue("{0:f}"));
					dataSalesLine.unitPrice = quantityForm.getUnitPrice();
					dataSalesLine.discount = quantityForm.getDiscount();
					dataSalesLine.amount = dataSalesLine.quantity * dataSalesLine.unitPrice;
					
					/*
					dataSalesLine.unitPrice = dataItem.price * (1 - (dataItem.discount / 100));
					dataSalesLine.discount = dataItem.discount;
					dataSalesLine.amount = dataSalesLine.quantity * dataSalesLine.unitPrice;
					*/
					dataSalesLine.save();
				}

				updateGrid();
			}

			itemList.Dispose();
			scanBox.Focus();
		
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			if (salesLineGrid.BindingContext[salesLineGrid.DataSource, ""].Count > 0)
			{
				int lineNo = (int)salesLineDataSet.Tables[0].Rows[salesLineGrid.CurrentRowIndex].ItemArray.GetValue(0);
				DataSalesLine dataSalesLine = new DataSalesLine(lineNo, smartDatabase);

				dataSalesLine.delete();

				updateGrid();
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("En rad måste markeras.", "Fel", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
			}

			scanBox.Focus();
		}

		private void disableButtons()
		{
			button1.Enabled = false;
			button2.Enabled = false;
			button3.Enabled = false;
			button4.Enabled = false;
			button5.Enabled = false;
		}

		private void enableButtons()
		{
			button1.Enabled = true;
			button2.Enabled = true;
			button3.Enabled = true;
			button4.Enabled = true;
			button5.Enabled = true;
		}

		private void button5_Click(object sender, System.EventArgs e)
		{
			if (salesLineGrid.BindingContext[salesLineGrid.DataSource, ""].Count > 0)
			{
				//if (salesLineDataSet.Tables[0].Rows[salesLineGrid.CurrentRowIndex].ItemArray.GetValue(11).ToString() == "")
				//{
					int lineNo = (int)salesLineDataSet.Tables[0].Rows[salesLineGrid.CurrentRowIndex].ItemArray.GetValue(0);
					DataSalesLine dataSalesLine = new DataSalesLine(lineNo, smartDatabase);				

					DataItem dataItem = new DataItem(salesLineGrid[salesLineGrid.CurrentRowIndex, 0].ToString(), smartDatabase);
					dataItem.description = salesLineGrid[salesLineGrid.CurrentRowIndex, 1].ToString();

					QuantityForm quantityForm = new QuantityForm(dataItem);
					quantityForm.setQuantity(dataSalesLine.quantity);
					quantityForm.setUnitPrice(dataSalesLine.unitPrice);
					quantityForm.setDiscount(dataSalesLine.discount);

					quantityForm.ShowDialog();
					if (quantityForm.getStatus() == 1)
					{
						dataSalesLine.quantity = float.Parse(quantityForm.getValue("{0:f}"));
						dataSalesLine.unitPrice = quantityForm.getUnitPrice();
						dataSalesLine.discount = quantityForm.getDiscount();
						dataSalesLine.amount = dataSalesLine.quantity * dataSalesLine.unitPrice;
						dataSalesLine.save();

						updateGrid();
					}

					quantityForm.Dispose();
				//}
				//else
				//{
				//	System.Windows.Forms.MessageBox.Show("Det går inte att ändra antal på en lagerdörr.", "Fel", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
				
				//}

			}
			else
			{
				System.Windows.Forms.MessageBox.Show("Det finns inga orderrader.", "Fel", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
				
			}
			scanBox.Focus();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			status = 1;
			this.Close();
		}

		public int getStatus()
		{
			return status;
		}
	}
}
