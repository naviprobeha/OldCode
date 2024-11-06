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
	public class OrderItem : System.Windows.Forms.Form
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

		private DataOrderLine dataOrderLine;
		private SmartDatabase smartDatabase;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox connectionQuantityBox;
		private System.Windows.Forms.TextBox amountBox;
		private System.Windows.Forms.TextBox connectionAmountBox;
		private int status;

		private DataItem dataItem;
		private DataItem dataConnectionItem;
		private System.Windows.Forms.TextBox totalAmountBox;
		private System.Windows.Forms.TextBox connectionUnitPriceBox;
		private System.Windows.Forms.Label unitOfMeasureBox;
		private DataOrderHeader dataOrderHeader;
	
		public OrderItem(SmartDatabase smartDatabase, DataOrderHeader dataOrderHeader, DataOrderLine dataOrderLine)
		{
			this.smartDatabase = smartDatabase;
			this.dataOrderLine = dataOrderLine;
			this.dataOrderHeader = dataOrderHeader;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			this.itemNoBox.Text = dataOrderLine.itemNo;
			this.descriptionBox.Text = dataOrderLine.description;
			this.quantityBox.Text = dataOrderLine.quantity.ToString();
			this.connectionQuantityBox.Text = dataOrderLine.connectionQuantity.ToString();
			this.unitPriceBox.Text = String.Format("{0:f}", dataOrderLine.unitPrice);
			this.amountBox.Text = String.Format("{0:f}", dataOrderLine.totalAmount);
			
			this.dataItem = new DataItem(smartDatabase, dataOrderLine.itemNo);
			dataOrderLine.connectionItemNo = dataItem.connectionItemNo;
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

			updateAmount();

			if (dataItem.requireCashPayment)
			{
				dataOrderHeader.payment = 1;
				dataOrderHeader.commit();
				MessageBox.Show("Betalsätt ändrat till kontant.", "Betalsätt");
			}
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
			this.totalAmountBox = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.unitOfMeasureBox = new System.Windows.Forms.Label();
			// 
			// itemNoBox
			// 
			this.itemNoBox.Location = new System.Drawing.Point(8, 24);
			this.itemNoBox.ReadOnly = true;
			this.itemNoBox.Size = new System.Drawing.Size(80, 20);
			this.itemNoBox.Text = "";
			// 
			// descriptionBox
			// 
			this.descriptionBox.Location = new System.Drawing.Point(96, 24);
			this.descriptionBox.ReadOnly = true;
			this.descriptionBox.Size = new System.Drawing.Size(216, 20);
			this.descriptionBox.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Text = "Artikelnr";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(96, 8);
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
			this.label3.Location = new System.Drawing.Point(8, 48);
			this.label3.Text = "Antal";
			// 
			// connectionQuantityBox
			// 
			this.connectionQuantityBox.Location = new System.Drawing.Point(8, 104);
			this.connectionQuantityBox.ReadOnly = true;
			this.connectionQuantityBox.Size = new System.Drawing.Size(80, 20);
			this.connectionQuantityBox.Text = "";
			// 
			// unitPriceBox
			// 
			this.unitPriceBox.Location = new System.Drawing.Point(96, 64);
			this.unitPriceBox.ReadOnly = true;
			this.unitPriceBox.Size = new System.Drawing.Size(64, 20);
			this.unitPriceBox.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 88);
			this.label4.Size = new System.Drawing.Size(88, 20);
			this.label4.Text = "Antal avlivningar";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(96, 48);
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
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(8, 176);
			this.button3.Size = new System.Drawing.Size(144, 32);
			this.button3.Text = "Identiteter";
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
			this.connectionUnitPriceBox.Location = new System.Drawing.Point(96, 104);
			this.connectionUnitPriceBox.ReadOnly = true;
			this.connectionUnitPriceBox.Size = new System.Drawing.Size(63, 20);
			this.connectionUnitPriceBox.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(96, 88);
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
			// totalAmountBox
			// 
			this.totalAmountBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.totalAmountBox.Location = new System.Drawing.Point(168, 144);
			this.totalAmountBox.ReadOnly = true;
			this.totalAmountBox.Size = new System.Drawing.Size(72, 20);
			this.totalAmountBox.Text = "";
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
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(168, 128);
			this.label11.Size = new System.Drawing.Size(64, 20);
			this.label11.Text = "Totalbelopp";
			// 
			// unitOfMeasureBox
			// 
			this.unitOfMeasureBox.Location = new System.Drawing.Point(56, 67);
			this.unitOfMeasureBox.Size = new System.Drawing.Size(32, 20);
			// 
			// OrderItem
			// 
			this.ClientSize = new System.Drawing.Size(322, 216);
			this.Controls.Add(this.unitOfMeasureBox);
			this.Controls.Add(this.totalAmountBox);
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
			this.Controls.Add(this.label11);
			this.Text = "Artikelinfo";

		}
		#endregion

		private void button3_Click(object sender, System.EventArgs e)
		{
			dataOrderLine.commit();
	
			OrderIds orderIds = new OrderIds(smartDatabase, dataOrderHeader, dataOrderLine);
			orderIds.ShowDialog();
			orderIds.Dispose();
		}

		private void button4_Click(object sender, System.EventArgs e)
		{
			if (dataItem.requireId) 
			{
				DataOrderLineIds dataOrderLineIds = new DataOrderLineIds(smartDatabase);
				if (!dataOrderLineIds.checkIfIdsEntered(dataOrderLine))
				{
					dataOrderLine.delete();
				}
			}

			this.status = 0;
			this.Close();
		}

		private void button5_Click(object sender, System.EventArgs e)
		{

			dataOrderLine.commit();

			if (dataItem.requireId)
			{
				DataOrderLineIds dataOrderLineIds = new DataOrderLineIds(smartDatabase);
				if (!dataOrderLineIds.checkIfIdsEntered(dataOrderLine))
				{
					System.Windows.Forms.MessageBox.Show("Du måste ange identitet.", "Fel", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
					return;
				}

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
				dataOrderLine.quantity = int.Parse(quantityBox.Text);
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
				dataOrderLine.connectionQuantity = int.Parse(connectionQuantityBox.Text);
			}
			numPad.Dispose();
			updateAmount();
		}

		private void updateAmount()
		{
			DataItemPrice dataItemPrice = new DataItemPrice(smartDatabase, dataItem, dataOrderLine.quantity, dataOrderHeader.dataCustomer);
			if (dataItemPrice.unitPrice > 0)
			{
				dataOrderLine.unitPrice = dataItemPrice.unitPrice;
				dataOrderLine.amount = dataItemPrice.unitPrice * dataOrderLine.quantity;
			}
			else
			{

				DataItemPriceExtended dataItemPriceExtended = new DataItemPriceExtended(smartDatabase, dataItem, dataOrderLine.quantity, dataOrderHeader.dataCustomer);
				if (dataItemPriceExtended.lineAmount > 0)
				{
					dataOrderLine.unitPrice = (float)decimal.Round((decimal)(dataItemPriceExtended.lineAmount / dataOrderLine.quantity), 2);
					dataOrderLine.amount = dataItemPriceExtended.lineAmount;
				}
				else
				{
					dataOrderLine.unitPrice = dataItem.unitPrice;
					dataOrderLine.amount = dataOrderLine.quantity * dataItem.unitPrice;
				}
			}

			if (dataConnectionItem != null)
			{
				dataItemPrice = new DataItemPrice(smartDatabase, dataConnectionItem, dataOrderLine.connectionQuantity, dataOrderHeader.dataCustomer);
				if (dataItemPrice.unitPrice > 0)
				{
					dataOrderLine.connectionUnitPrice = dataItemPrice.unitPrice;
					dataOrderLine.connectionAmount = dataItemPrice.unitPrice * dataOrderLine.connectionQuantity;
				}
				else
				{

					DataItemPriceExtended dataItemPriceExtended = new DataItemPriceExtended(smartDatabase, dataConnectionItem, dataOrderLine.connectionQuantity, dataOrderHeader.dataCustomer);
					if (dataItemPriceExtended.lineAmount > 0)
					{
						dataOrderLine.connectionUnitPrice = (float)decimal.Round((decimal)(dataItemPriceExtended.lineAmount / dataOrderLine.quantity), 2);
						dataOrderLine.connectionAmount = dataItemPriceExtended.lineAmount;
					}
					else
					{
						dataOrderLine.connectionUnitPrice = dataConnectionItem.unitPrice;
						dataOrderLine.connectionAmount = dataOrderLine.connectionQuantity * dataConnectionItem.unitPrice;
					}
				}

			}

			dataOrderLine.totalAmount = dataOrderLine.amount + dataOrderLine.connectionAmount;

			unitPriceBox.Text = String.Format("{0:f}", dataOrderLine.unitPrice);
			amountBox.Text = String.Format("{0:f}", dataOrderLine.amount);
			connectionUnitPriceBox.Text = String.Format("{0:f}", dataOrderLine.connectionUnitPrice);
			connectionAmountBox.Text = String.Format("{0:f}", dataOrderLine.connectionAmount);

			totalAmountBox.Text = String.Format("{0:f}", dataOrderLine.totalAmount);
		}

		private void applyStopItem()
		{
			DataOrderLines dataOrderLines = new DataOrderLines(smartDatabase);
			DataSet orderLineDataSet = dataOrderLines.getOrderDataSet(dataOrderHeader);

			bool found = false;

			int i = 0;
			while (i < orderLineDataSet.Tables[0].Rows.Count)
			{
				if (orderLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString() == dataItem.stopItemNo) found = true;

				i++;
			}

			if (!found)
			{
				DataOrganization dataOrganization = new DataOrganization(smartDatabase, dataOrderHeader.dataCustomer.organizationNo);

				DataItem stopItem = new DataItem(smartDatabase, dataItem.stopItemNo);
				DataOrderLine dataStopOrderLine = new DataOrderLine(smartDatabase, dataOrderHeader, stopItem);
				dataStopOrderLine.quantity = 1;
				dataStopOrderLine.unitPrice = dataOrganization.stopFee;
				dataStopOrderLine.amount = dataOrganization.stopFee;
				dataStopOrderLine.totalAmount = dataOrganization.stopFee;
				dataStopOrderLine.commit();
			}

			
		}
	}
}
