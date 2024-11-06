using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ShipmentLine.
	/// </summary>
	public class ShipmentLine
	{

		public int entryNo;
		public int originalEntryNo;
		public string shipmentNo;
		public string itemNo;
		public string description;
		public int quantity;
		public int connectionQuantity;
		public float unitPrice;
		public float amount;
		public float connectionUnitPrice;
		public float connectionAmount;
		public float totalAmount;
		public string connectionItemNo;
		public int testQuantity;
		public bool extraPayment;

		public string agentCode;
		private string updateMethod;

		public ShipmentLine(Database database, string agentCode, DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			
			this.agentCode = agentCode;

			fromDataRow(dataRow);

			Items items = new Items();
			Item item = items.getEntry(database, itemNo);
			if (item.invoiceToJbv) 
			{
				totalAmount = connectionAmount;
				unitPrice = 0;
				amount = 0;
			}

			
			updateMethod = "";
			save(database);


		}

		public ShipmentLine(DataRow dataRow)
		{

			fromDataRow(dataRow);
		}

		private void fromDataRow(DataRow dataRow)
		{
			originalEntryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
			shipmentNo = agentCode +"-"+dataRow.ItemArray.GetValue(1).ToString();
			itemNo = dataRow.ItemArray.GetValue(2).ToString();
			description = dataRow.ItemArray.GetValue(3).ToString();
			quantity = int.Parse(dataRow.ItemArray.GetValue(4).ToString());
			connectionQuantity = int.Parse(dataRow.ItemArray.GetValue(5).ToString());
			unitPrice = float.Parse(dataRow.ItemArray.GetValue(6).ToString());
			amount = float.Parse(dataRow.ItemArray.GetValue(7).ToString());
			connectionUnitPrice = float.Parse(dataRow.ItemArray.GetValue(8).ToString());
			connectionAmount = float.Parse(dataRow.ItemArray.GetValue(9).ToString());		
			totalAmount = float.Parse(dataRow.ItemArray.GetValue(10).ToString());		
			connectionItemNo = dataRow.ItemArray.GetValue(11).ToString();

			if (dataRow.ItemArray.Length > 12)
			{
				extraPayment = false;
				if (int.Parse(dataRow.ItemArray.GetValue(12).ToString()) == 1) extraPayment = true;

				testQuantity = int.Parse(dataRow.ItemArray.GetValue(13).ToString());

			}

		}

		public XmlElement toDOM(XmlDocument xmlDoc)
		{
			
			XmlElement shipmentLineElement = xmlDoc.CreateElement("SHIPMENT_LINE");

			XmlElement entryNoElement = xmlDoc.CreateElement("ENTRY_NO");
			entryNoElement.AppendChild(xmlDoc.CreateTextNode(this.originalEntryNo.ToString()));
			shipmentLineElement.AppendChild(entryNoElement);

			XmlElement shipmentNoElement = xmlDoc.CreateElement("SHIPMENT_NO");
			shipmentNoElement.AppendChild(xmlDoc.CreateTextNode(this.shipmentNo));
			shipmentLineElement.AppendChild(shipmentNoElement);

			XmlElement itemNoElement = xmlDoc.CreateElement("ITEM_NO");
			itemNoElement.AppendChild(xmlDoc.CreateTextNode(this.itemNo));
			shipmentLineElement.AppendChild(itemNoElement);

			XmlElement descriptionElement = xmlDoc.CreateElement("DESCRIPTION");
			descriptionElement.AppendChild(xmlDoc.CreateTextNode(this.description));
			shipmentLineElement.AppendChild(descriptionElement);

			XmlElement quantityElement = xmlDoc.CreateElement("QUANTITY");
			quantityElement.AppendChild(xmlDoc.CreateTextNode(this.quantity.ToString()));
			shipmentLineElement.AppendChild(quantityElement);

			XmlElement conQuantityElement = xmlDoc.CreateElement("CONNECTION_QUANTITY");
			conQuantityElement.AppendChild(xmlDoc.CreateTextNode(this.connectionQuantity.ToString()));
			shipmentLineElement.AppendChild(conQuantityElement);

			XmlElement unitPriceElement = xmlDoc.CreateElement("UNIT_PRICE");
			unitPriceElement.AppendChild(xmlDoc.CreateTextNode(this.unitPrice.ToString()));
			shipmentLineElement.AppendChild(unitPriceElement);

			XmlElement amountElement = xmlDoc.CreateElement("AMOUNT");
			amountElement.AppendChild(xmlDoc.CreateTextNode(this.amount.ToString()));
			shipmentLineElement.AppendChild(amountElement);

			XmlElement conUnitPriceElement = xmlDoc.CreateElement("CONNECTION_UNIT_PRICE");
			conUnitPriceElement.AppendChild(xmlDoc.CreateTextNode(this.connectionUnitPrice.ToString()));
			shipmentLineElement.AppendChild(conUnitPriceElement);

			XmlElement conAmountElement = xmlDoc.CreateElement("CONNECTION_AMOUNT");
			conAmountElement.AppendChild(xmlDoc.CreateTextNode(this.connectionAmount.ToString()));
			shipmentLineElement.AppendChild(conAmountElement);

			XmlElement totalAmountElement = xmlDoc.CreateElement("TOTAL_AMOUNT");
			totalAmountElement.AppendChild(xmlDoc.CreateTextNode(this.totalAmount.ToString()));
			shipmentLineElement.AppendChild(totalAmountElement);

			XmlElement conItemNoElement = xmlDoc.CreateElement("CONNECTION_ITEM_NO");
			conItemNoElement.AppendChild(xmlDoc.CreateTextNode(this.connectionItemNo));
			shipmentLineElement.AppendChild(conItemNoElement);

			XmlElement testQuantityElement = xmlDoc.CreateElement("TEST_QUANTITY");
			testQuantityElement.AppendChild(xmlDoc.CreateTextNode(this.testQuantity.ToString()));
			shipmentLineElement.AppendChild(testQuantityElement);

			XmlElement extraPaymentElement = xmlDoc.CreateElement("EXTRA_PAYMENT");
			if (extraPayment)
			{
				extraPaymentElement.AppendChild(xmlDoc.CreateTextNode("TRUE"));
			}
			else
			{
				extraPaymentElement.AppendChild(xmlDoc.CreateTextNode("FALSE"));
			}
			shipmentLineElement.AppendChild(extraPaymentElement);
			
			return shipmentLineElement;
		}

		public void save(Database database)
		{
			int extraPaymentValue = 0;
			if (extraPayment) extraPaymentValue = 1;

			SqlDataReader dataReader = database.query("SELECT * FROM [Shipment Line] WHERE [Entry No] = '"+entryNo+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					database.nonQuery("DELETE FROM [Shipment Line] WHERE [Entry No] = '"+entryNo+"'");
				}

				else
				{

					database.nonQuery("UPDATE [Shipment Line] SET [Shipment No] = '"+this.shipmentNo+"', [Item No] = '"+this.itemNo+"', [Description] = '"+this.description+"', [Quantity] = '"+this.quantity+"', [Connection Quantity] = '"+this.connectionQuantity+"', [Unit Price] = '"+this.unitPrice.ToString().Replace(",", ".")+"', [Amount] = '"+this.amount.ToString().Replace(",", ".")+"', [Connection Unit Price] = '"+this.connectionUnitPrice.ToString().Replace(",", ".")+"', [Connection Amount] = '"+this.connectionAmount.ToString().Replace(",", ".")+"', [Total Amount] = '"+this.totalAmount.ToString().Replace(",", ".")+"', [Connection Item No] = '"+this.connectionItemNo+"', [Test Quantity] = '"+this.testQuantity.ToString()+"' [Extra Payment] = '"+extraPaymentValue.ToString()+"', [Original Entry No] = '"+originalEntryNo+"' WHERE [Entry No] = '"+this.entryNo+"'");
				}
			}
			else
			{
				dataReader.Close();
				database.nonQuery("INSERT INTO [Shipment Line] ([Shipment No], [Item No], [Description], [Quantity], [Connection Quantity], [Unit Price], [Amount], [Connection Unit Price], [Connection Amount], [Total Amount], [Connection Item No], [Test Quantity], [Extra Payment], [Original Entry No]) VALUES ('"+this.shipmentNo+"','"+this.itemNo+"','"+this.description+"','"+this.quantity+"','"+this.connectionQuantity+"','"+this.unitPrice.ToString().Replace(",", ".")+"','"+this.amount.ToString().Replace(",", ".")+"','"+this.connectionUnitPrice.ToString().Replace(",", ".")+"','"+this.connectionAmount.ToString().Replace(",", ".")+"','"+this.totalAmount.ToString().Replace(",", ".")+"','"+this.connectionItemNo+"', '"+this.testQuantity.ToString()+"', '"+extraPaymentValue+"', '"+originalEntryNo+"')");
				entryNo = (int)database.getInsertedSeqNo();
			}

		}
	}
}
