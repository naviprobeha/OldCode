using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Navipro.SantaMonica.Felicia
{
	/// <summary>
	/// Summary description for ItemInfo.
	/// </summary>
	public class ShipmentItem : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox itemNoBox;
		private System.Windows.Forms.TextBox descriptionBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox quantityBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox unitPriceBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button3;

		private DataShipmentLine dataShipmentLine;
		private SmartDatabase smartDatabase;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox connectionQuantityBox;
		private System.Windows.Forms.TextBox amountBox;
		private System.Windows.Forms.TextBox connectionAmountBox;
		private int status;

		private DataItem dataItem;
		private DataItem dataConnectionItem;
		private System.Windows.Forms.TextBox connectionUnitPriceBox;
		private System.Windows.Forms.Label unitOfMeasureBox;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button8;
		private DataShipmentHeader dataShipmentHeader;
	
		public ShipmentItem(SmartDatabase smartDatabase, DataShipmentHeader dataShipmentHeader, DataShipmentLine dataShipmentLine)
		{
			this.smartDatabase = smartDatabase;
			this.dataShipmentLine = dataShipmentLine;
			this.dataShipmentHeader = dataShipmentHeader;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			updateForm();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		private void updateForm()
		{
			this.itemNoBox.Text = dataShipmentLine.itemNo;
			this.descriptionBox.Text = dataShipmentLine.description;
			this.quantityBox.Text = dataShipmentLine.quantity.ToString();
			this.connectionQuantityBox.Text = dataShipmentLine.connectionQuantity.ToString();
			//this.testQuantityBox.Text = dataShipmentLine.testQuantity.ToString();
			this.unitPriceBox.Text = String.Format("{0:f}", dataShipmentLine.unitPrice);
			this.amountBox.Text = String.Format("{0:f}", dataShipmentLine.totalAmount);
			
			this.dataItem = new DataItem(smartDatabase, dataShipmentLine.itemNo);
			dataShipmentLine.connectionItemNo = dataItem.connectionItemNo;
			if (dataItem.connectionItemNo != "")
			{
				dataConnectionItem = new DataItem(smartDatabase, dataItem.connectionItemNo);
			}
			else
			{
				connectionQuantityBox.Visible = false;
				connectionUnitPriceBox.Visible = false;
				connectionAmountBox.Visible = false;
				label4.Visible = false;
				label8.Visible = false;
				label10.Visible = false;
				button1.Visible = false;
			}

			this.unitOfMeasureBox.Text = dataItem.unitOfMeasure;

			if (this.dataShipmentLine.extraPayment) 
			{
				this.button6.Text = "Enkel taxa";
			}
			else
			{
				this.button6.Text = "Dubbel taxa";
			}

			updateAmount();


			if (dataItem.requireCashPayment)
			{
				if (dataShipmentHeader.payment == 0)
				{
					if (MessageBox.Show("Ändra till kontant betalning?", "Betalsätt", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
					{
						dataShipmentHeader.payment = 1;
						dataShipmentHeader.commit();
					}
				}
			}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.itemNoBox = new System.Windows.Forms.TextBox();
			this.descriptionBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.quantityBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.connectionQuantityBox = new System.Windows.Forms.TextBox();
			this.unitPriceBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.connectionUnitPriceBox = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.amountBox = new System.Windows.Forms.TextBox();
			this.connectionAmountBox = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.unitOfMeasureBox = new System.Windows.Forms.Label();
			this.button6 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			// 
			// itemNoBox
			// 
			this.itemNoBox.Location = new System.Drawing.Point(8, 24);
			this.itemNoBox.ReadOnly = true;
			this.itemNoBox.Size = new System.Drawing.Size(88, 20);
			this.itemNoBox.Text = "";
			// 
			// descriptionBox
			// 
			this.descriptionBox.Location = new System.Drawing.Point(104, 24);
			this.descriptionBox.ReadOnly = true;
			this.descriptionBox.Size = new System.Drawing.Size(136, 20);
			this.descriptionBox.Text = "";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Text = "Artikelnr";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(104, 8);
			this.label2.Text = "Beskrivning";
			// 
			// quantityBox
			// 
			this.quantityBox.Location = new System.Drawing.Point(8, 64);
			this.quantityBox.ReadOnly = true;
			this.quantityBox.Size = new System.Drawing.Size(48, 20);
			this.quantityBox.Text = "";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label3.Location = new System.Drawing.Point(8, 48);
			this.label3.Text = "Antal";
			// 
			// connectionQuantityBox
			// 
			this.connectionQuantityBox.Location = new System.Drawing.Point(8, 104);
			this.connectionQuantityBox.ReadOnly = true;
			this.connectionQuantityBox.Size = new System.Drawing.Size(88, 20);
			this.connectionQuantityBox.Text = "";
			// 
			// unitPriceBox
			// 
			this.unitPriceBox.Location = new System.Drawing.Point(104, 64);
			this.unitPriceBox.ReadOnly = true;
			this.unitPriceBox.Size = new System.Drawing.Size(56, 20);
			this.unitPriceBox.Text = "";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label4.Location = new System.Drawing.Point(8, 88);
			this.label4.Size = new System.Drawing.Size(96, 20);
			this.label4.Text = "Antal avlivningar";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(104, 48);
			this.label5.Size = new System.Drawing.Size(64, 20);
			this.label5.Text = "A-pris";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(248, 92);
			this.button1.Size = new System.Drawing.Size(64, 32);
			this.button1.Text = "Ändra";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(248, 52);
			this.button2.Size = new System.Drawing.Size(64, 32);
			this.button2.Text = "Ändra";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(96, 176);
			this.button3.Size = new System.Drawing.Size(56, 32);
			this.button3.Text = "ID-nr";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(160, 176);
			this.button4.Size = new System.Drawing.Size(72, 32);
			this.button4.Text = "Avbryt";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button5.Location = new System.Drawing.Point(240, 176);
			this.button5.Size = new System.Drawing.Size(72, 32);
			this.button5.Text = "Klar";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// connectionUnitPriceBox
			// 
			this.connectionUnitPriceBox.Location = new System.Drawing.Point(104, 104);
			this.connectionUnitPriceBox.ReadOnly = true;
			this.connectionUnitPriceBox.Size = new System.Drawing.Size(56, 20);
			this.connectionUnitPriceBox.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(104, 88);
			this.label8.Size = new System.Drawing.Size(56, 20);
			this.label8.Text = "A-pris";
			// 
			// amountBox
			// 
			this.amountBox.Location = new System.Drawing.Point(168, 64);
			this.amountBox.ReadOnly = true;
			this.amountBox.Size = new System.Drawing.Size(72, 20);
			this.amountBox.Text = "";
			// 
			// connectionAmountBox
			// 
			this.connectionAmountBox.Location = new System.Drawing.Point(168, 104);
			this.connectionAmountBox.ReadOnly = true;
			this.connectionAmountBox.Size = new System.Drawing.Size(72, 20);
			this.connectionAmountBox.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(168, 48);
			this.label9.Size = new System.Drawing.Size(64, 20);
			this.label9.Text = "Belopp";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(168, 88);
			this.label10.Size = new System.Drawing.Size(64, 20);
			this.label10.Text = "Belopp";
			// 
			// unitOfMeasureBox
			// 
			this.unitOfMeasureBox.Location = new System.Drawing.Point(56, 67);
			this.unitOfMeasureBox.Size = new System.Drawing.Size(32, 20);
			// 
			// button6
			// 
			this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
			this.button6.Location = new System.Drawing.Point(8, 176);
			this.button6.Size = new System.Drawing.Size(80, 32);
			this.button6.Text = "Dubbel taxa";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button8
			// 
			this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button8.Location = new System.Drawing.Point(248, 12);
			this.button8.Size = new System.Drawing.Size(64, 32);
			this.button8.Text = "Ändra";
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// ShipmentItem
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.button8);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.unitOfMeasureBox);
			this.Controls.Add(this.connectionAmountBox);
			this.Controls.Add(this.amountBox);
			this.Controls.Add(this.connectionUnitPriceBox);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.unitPriceBox);
			this.Controls.Add(this.connectionQuantityBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.quantityBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.descriptionBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.itemNoBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label10);
			this.Text = "Artikelinfo";

		}
		#endregion

		private void button3_Click(object sender, System.EventArgs e)
		{
			dataShipmentLine.commit();
	
			ShipmentIds shipmentIds = new ShipmentIds(smartDatabase, dataShipmentHeader, dataShipmentLine);
			shipmentIds.ShowDialog();
			shipmentIds.Dispose();
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			/*if (dataItem.requireId) 
			{
				DataShipmentLineIds dataShipmentLineIds = new DataShipmentLineIds(smartDatabase);
				if (!dataShipmentLineIds.checkIfIdsEntered(dataShipmentLine))
				{
					dataShipmentLine.delete();
				}
			}*/

			this.status = 0;
			this.Close();
		}

		private void button5_Click(object sender, System.EventArgs e)
		{

			dataShipmentLine.commit();
			dataShipmentLine.updateTestings();

			if (dataItem.requireId)
			{
				DataShipmentLineIds dataShipmentLineIds = new DataShipmentLineIds(smartDatabase);
				if (!dataShipmentLineIds.checkIfIdsEntered(dataShipmentLine))
				{
					System.Windows.Forms.MessageBox.Show("Du måste ange identitet.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
					return;
				}

			}

			if (dataShipmentLine.amount == 0)
			{
				if (System.Windows.Forms.MessageBox.Show("Du har registrerat "+quantityBox.Text+" "+unitOfMeasureBox.Text+" "+descriptionBox.Text+" för 0 kr. Är det riktigt?", "Fråga", MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No) return;
			}

			if (dataItem.addStopItem)
			{
				if (MessageBox.Show("Lägga på stoppavgift?", "Stoppavgift", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					applyStopItem();
				}
			}
			
			this.status = 1;

			this.Close();
		}

		public int getStatus()
		{
			return status;
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			NumPad numPad = new NumPad();
			numPad.setInputString(quantityBox.Text);
			numPad.ShowDialog();
			if (numPad.getInputString() != "")
			{
				quantityBox.Text = numPad.getInputString();
				dataShipmentLine.quantity = int.Parse(quantityBox.Text);
			}
			numPad.Dispose();
			updateAmount();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			NumPad numPad = new NumPad();
			numPad.setInputString(connectionQuantityBox.Text);
			numPad.ShowDialog();
			if (numPad.getInputString() != "")
			{
				connectionQuantityBox.Text = numPad.getInputString();
				dataShipmentLine.connectionQuantity = int.Parse(connectionQuantityBox.Text);
			}
			numPad.Dispose();
			updateAmount();
		}

		private void updateAmount()
		{
			DataItemPrice dataItemPrice = new DataItemPrice(smartDatabase, dataItem, dataShipmentLine.quantity, dataShipmentHeader.dataCustomer);
			if (dataItemPrice.unitPrice > 0)
			{
				dataShipmentLine.unitPrice = dataItemPrice.unitPrice;
				dataShipmentLine.amount = dataItemPrice.unitPrice * dataShipmentLine.quantity;

			}
			else
			{

				DataItemPriceExtended dataItemPriceExtended = new DataItemPriceExtended(smartDatabase, dataItem, dataShipmentLine.quantity, dataShipmentHeader.dataCustomer);
				if (dataItemPriceExtended.lineAmount > 0)
				{
					dataShipmentLine.unitPrice = (float)decimal.Round((decimal)(dataItemPriceExtended.lineAmount / dataShipmentLine.quantity), 2);
					dataShipmentLine.amount = dataItemPriceExtended.lineAmount;

				}
				else
				{
					dataShipmentLine.unitPrice = dataItem.unitPrice;
					dataShipmentLine.amount = dataShipmentLine.quantity * dataItem.unitPrice;
				}
			}

			if (dataShipmentLine.extraPayment) 
			{
				dataShipmentLine.unitPrice = dataShipmentLine.unitPrice * 2;
				dataShipmentLine.amount = dataShipmentLine.amount * 2;
			}

			if (dataConnectionItem != null)
			{
				dataItemPrice = new DataItemPrice(smartDatabase, dataConnectionItem, dataShipmentLine.connectionQuantity, dataShipmentHeader.dataCustomer);
				if (dataItemPrice.unitPrice > 0)
				{
					dataShipmentLine.connectionUnitPrice = dataItemPrice.unitPrice;
					dataShipmentLine.connectionAmount = dataItemPrice.unitPrice * dataShipmentLine.connectionQuantity;
				}
				else
				{

					DataItemPriceExtended dataItemPriceExtended = new DataItemPriceExtended(smartDatabase, dataConnectionItem, dataShipmentLine.connectionQuantity, dataShipmentHeader.dataCustomer);
					if (dataItemPriceExtended.lineAmount > 0)
					{
						dataShipmentLine.connectionUnitPrice = (float)decimal.Round((decimal)(dataItemPriceExtended.lineAmount / dataShipmentLine.quantity), 2);
						dataShipmentLine.connectionAmount = dataItemPriceExtended.lineAmount;
					}
					else
					{
						dataShipmentLine.connectionUnitPrice = dataConnectionItem.unitPrice;
						dataShipmentLine.connectionAmount = dataShipmentLine.connectionQuantity * dataConnectionItem.unitPrice;
					}
				}

			}

			dataShipmentLine.totalAmount = dataShipmentLine.amount + dataShipmentLine.connectionAmount;

			unitPriceBox.Text = String.Format("{0:f}", dataShipmentLine.unitPrice);
			amountBox.Text = String.Format("{0:f}", dataShipmentLine.amount);
			connectionUnitPriceBox.Text = String.Format("{0:f}", dataShipmentLine.connectionUnitPrice);
			connectionAmountBox.Text = String.Format("{0:f}", dataShipmentLine.connectionAmount);

			//totalAmountBox.Text = String.Format("{0:f}", dataShipmentLine.totalAmount);
		}

		private void applyStopItem()
		{
			DataShipmentLines dataShipmentLines = new DataShipmentLines(smartDatabase);
			DataSet shipmentLineDataSet = dataShipmentLines.getShipmentDataSet(dataShipmentHeader);

			bool found = false;

			int i = 0;
			while (i < shipmentLineDataSet.Tables[0].Rows.Count)
			{
				if (shipmentLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() == dataItem.stopItemNo) found = true;

				i++;
			}

			if (!found)
			{
				DataOrganization dataOrganization = new DataOrganization(smartDatabase, dataShipmentHeader.dataCustomer.organizationNo);

				DataItem stopItem = new DataItem(smartDatabase, dataItem.stopItemNo);
				DataShipmentLine dataStopShipmentLine = new DataShipmentLine(smartDatabase, dataShipmentHeader, stopItem);
				dataStopShipmentLine.quantity = 1;
				dataStopShipmentLine.unitPrice = dataOrganization.stopFee;
				dataStopShipmentLine.amount = dataOrganization.stopFee;
				dataStopShipmentLine.totalAmount = dataOrganization.stopFee;
				dataStopShipmentLine.commit();
			}

			
		}

		private void button6_Click(object sender, System.EventArgs e)
		{
			if (this.dataShipmentLine.extraPayment) 
			{
				this.button6.Text = "Dubbel taxa";
				this.dataShipmentLine.extraPayment = false;

				this.updateAmount();
			}
			else
			{
				this.button6.Text = "Enkel taxa";
				this.dataShipmentLine.extraPayment = true;

				this.updateAmount();

			}

		}


		private void button8_Click(object sender, System.EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;
			Cursor.Show();

			ItemList itemList = new ItemList(smartDatabase);

			Cursor.Current = Cursors.Default;
			Cursor.Hide();

			itemList.ShowDialog();
			DataItem dataItem = itemList.getItem();
			itemList.Dispose();

			if (dataItem != null)
			{
				this.dataShipmentLine.setItem(dataItem);
				this.updateForm();
				this.updateAmount();

			}
		}
	}
}
