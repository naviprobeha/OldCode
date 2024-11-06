using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Container.
	/// </summary>
	public class ScaleEntry
	{

		public string factoryCode;
		public int entryNo;
		public int type;
		public string reference;
		public string containerNo;
		public string containerTypeCode;
		public DateTime entryDateTime;		
		public string shippingCustomerNo;
		public string categoryCode;
		public float weight;
		public string agentCode;
		public int noOfContainers;
		public string containerNo2;
		public int lineOrderEntryNo;
		public int navisionStatus;
		public int status;
		public string comment;
		
		private string updateMethod;

		public ScaleEntry()
		{

		}

		public ScaleEntry(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.factoryCode = dataReader.GetValue(0).ToString();
			this.entryNo = dataReader.GetInt32(1);
			this.type = int.Parse(dataReader.GetValue(2).ToString());
			this.reference = dataReader.GetValue(3).ToString();
			this.containerNo = dataReader.GetValue(4).ToString();
			this.containerTypeCode = dataReader.GetValue(5).ToString();
			
			DateTime entryDate = dataReader.GetDateTime(6);
			DateTime entryTime = dataReader.GetDateTime(7);
			this.entryDateTime = new DateTime(entryDate.Year, entryDate.Month, entryDate.Day, entryTime.Hour, entryTime.Minute, entryTime.Second);

			this.shippingCustomerNo = dataReader.GetValue(8).ToString();
			this.categoryCode = dataReader.GetValue(9).ToString();
			this.weight = float.Parse(dataReader.GetValue(10).ToString());
			this.agentCode = dataReader.GetValue(11).ToString();

			this.lineOrderEntryNo = dataReader.GetInt32(12);

			this.navisionStatus = int.Parse(dataReader.GetValue(13).ToString());

			this.status = int.Parse(dataReader.GetValue(14).ToString());

			this.noOfContainers = dataReader.GetInt32(15);
			this.containerNo2 = dataReader.GetValue(16).ToString();
			this.comment = dataReader.GetValue(17).ToString();

			updateMethod = "";

		}

		public ScaleEntry(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.factoryCode = dataRow.ItemArray.GetValue(0).ToString();
			this.entryNo = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
			this.type = int.Parse(dataRow.ItemArray.GetValue(2).ToString());
			this.reference = dataRow.ItemArray.GetValue(3).ToString();
			this.containerNo = dataRow.ItemArray.GetValue(4).ToString();
			this.containerTypeCode = dataRow.ItemArray.GetValue(5).ToString();
			
			DateTime entryDate = DateTime.Parse(dataRow.ItemArray.GetValue(6).ToString());
			DateTime entryTime = DateTime.Parse(dataRow.ItemArray.GetValue(7).ToString());
			this.entryDateTime = new DateTime(entryDate.Year, entryDate.Month, entryDate.Day, entryTime.Hour, entryTime.Minute, entryTime.Second);

			this.shippingCustomerNo = dataRow.ItemArray.GetValue(8).ToString();
			this.categoryCode = dataRow.ItemArray.GetValue(9).ToString();
			this.weight = float.Parse(dataRow.ItemArray.GetValue(10).ToString());
			this.agentCode = dataRow.ItemArray.GetValue(11).ToString();

			this.lineOrderEntryNo = int.Parse(dataRow.ItemArray.GetValue(12).ToString());

			this.navisionStatus = int.Parse(dataRow.ItemArray.GetValue(13).ToString());

			this.status = int.Parse(dataRow.ItemArray.GetValue(14).ToString());

			this.noOfContainers = int.Parse(dataRow.ItemArray.GetValue(15).ToString());
			this.containerNo2 = dataRow.ItemArray.GetValue(16).ToString();
			this.comment = dataRow.ItemArray.GetValue(17).ToString();

			updateMethod = "";

		}

		public void save(Database database)
		{
			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Scale Entry] WHERE [Factory Code] = '"+factoryCode+"' AND [Entry No] = '"+entryNo+"'");
				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Scale Entry] WHERE [Factory Code] = '"+factoryCode+"' AND [Entry No] = '"+entryNo+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Scale Entry] SET [Type] = '"+type+"', [Reference] = '"+reference+"', [Container No] = '"+containerNo+"', [Container Type Code] = '"+containerTypeCode+"', [Entry Date] = '"+entryDateTime.ToString("yyyy-MM-dd 00:00:00")+"', [Entry Time] = '"+entryDateTime.ToString("1754-01-01 HH:mm:ss")+"', [Shipping Customer No] = '"+shippingCustomerNo+"', [Category Code] = '"+categoryCode+"', [Weight] = '"+weight.ToString().Replace(",", ".")+"', [Agent Code] = '"+agentCode+"', [Line Order Entry No] = '"+lineOrderEntryNo+"', [Navision Status] = '"+this.navisionStatus+"', [Status] = '"+status+"', [No Of Containers] = '"+this.noOfContainers+"', [Container No 2] = '"+this.containerNo2+"', [Comment] = '"+this.comment+"' WHERE [Factory Code] = '"+factoryCode+"' AND [Entry No] = '"+entryNo+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Scale Entry] ([Factory Code], [Entry No], [Type], [Reference], [Container No], [Container Type Code], [Entry Date], [Entry Time], [Shipping Customer No], [Category Code], [Weight], [Agent Code], [Line Order Entry No], [Navision Status], [Status], [No Of Containers], [Container No 2], [Comment]) VALUES ('"+this.factoryCode+"', '"+this.entryNo+"', '"+this.type+"','"+this.reference+"','"+this.containerNo+"','"+this.containerTypeCode+"','"+entryDateTime.ToString("yyyy-MM-dd 00:00:00")+"','"+entryDateTime.ToString("1754-01-01 HH:mm:ss")+"','"+shippingCustomerNo+"','"+categoryCode+"','"+weight.ToString().Replace(",", ".")+"','"+agentCode+"', '"+lineOrderEntryNo+"', '"+this.navisionStatus+"', '"+status+"', '"+noOfContainers+"', '"+containerNo2+"', '"+comment+"')");
					}
				}

			}
			catch(Exception e)
			{					
				throw new Exception("Error on scale entry update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}


		public string getType()
		{
			if (this.type == 0) return "In";
			if (this.type == 1) return "Ut";

			return "";
		}

		public string getStatus()
		{
			if (status == 1) return "1:a vägning";
			if (status == 2) return "Klar";
			if (status == 3) return "";
			if (status == 4) return "";
			if (status == 5) return "";
			if (status == 6) return "";
			if (status == 7) return "";
			if (status == 8) return "Makulering";
			if (status == 9) return "Makulerad";

			return "";
		}

		public string getNavisionStatus()
		{
			if (this.navisionStatus == 0) return "";
			if (this.navisionStatus == 1) return "Köad";
			if (this.navisionStatus == 2) return "Skickad";
			if (this.navisionStatus == 3) return "Bekräftad";


			return "";
		}


		public string getShippingCustomerName(Database database)
		{
			ShippingCustomers shippingCustomers = new ShippingCustomers();
			ShippingCustomer shippingCustomer = shippingCustomers.getEntry(database, this.shippingCustomerNo);
			if (shippingCustomer != null) return shippingCustomer.name+", "+shippingCustomer.city;

			return "";
		}

		public string getCategory(Database database)
		{
			Categories categories = new Categories();
			Category category = categories.getEntry(database, this.categoryCode);
			if (category != null) return category.description;

			return "";
		}

		public bool shipmentMonthSameAsScaleMonth(Database database)
		{
			DateTime scaleMonth = new DateTime(this.entryDateTime.Year, this.entryDateTime.Month, 1);

			LineOrders lineOrders = new LineOrders();
			LineOrder lineOrder = lineOrders.getEntry(database, this.lineOrderEntryNo.ToString());
			if (lineOrder != null)
			{
				DateTime creationMonth = new DateTime(lineOrder.creationDate.Year, lineOrder.creationDate.Month, 1);

				if (creationMonth < scaleMonth) return false; 
			}

			return true;
		}

		public bool hasShipments(Database database)
		{
			LineOrders lineOrders = new LineOrders();
			LineOrder lineOrder = lineOrders.getEntry(database, this.lineOrderEntryNo.ToString());
			if (lineOrder != null)
			{
				LineOrderShipments lineOrderShipments = new LineOrderShipments();
				DataSet dataSet = lineOrderShipments.getDataSet(database, this.lineOrderEntryNo);
				if (dataSet.Tables[0].Rows.Count > 0) return true;
			}

			return false;
		}

		public float getCreationDateWeight(Database database)
		{
			float weight = 0;

			LineOrders lineOrders = new LineOrders();
			LineOrder lineOrder = lineOrders.getEntry(database, this.lineOrderEntryNo.ToString());
			if (lineOrder != null)
			{
				LineOrderShipments lineOrderShipments = new LineOrderShipments();
				DataSet dataSet = lineOrderShipments.getDataSet(database, this.lineOrderEntryNo);
				int i = 0;
				while (i < dataSet.Tables[0].Rows.Count)
				{
					LineOrderShipment lineOrderShipment = new LineOrderShipment(database, dataSet.Tables[0].Rows[i]);

					ShipmentHeaders shipmentHeaders = new ShipmentHeaders();
					ShipmentHeader shipmentHeader = shipmentHeaders.getEntry(database, lineOrderShipment.shipmentNo);
					if (shipmentHeader != null)
					{
						if (shipmentHeader.shipDate.Month == lineOrder.creationDate.Month)
						{
							weight = weight + shipmentHeader.getWeight(database);
						}
					}
					

					i++;
				}
			}

			return weight;
		}


		public XmlElement toDOM(XmlDocument xmlDoc)
		{
			XmlElement scaleElement = xmlDoc.CreateElement("SCALE_ENTRY");

			XmlElement factoryCodeElement = xmlDoc.CreateElement("FACTORY_CODE");
			factoryCodeElement.AppendChild(xmlDoc.CreateTextNode(this.factoryCode));
			scaleElement.AppendChild(factoryCodeElement);

			XmlElement entryNoElement = xmlDoc.CreateElement("ENTRY_NO");
			entryNoElement.AppendChild(xmlDoc.CreateTextNode(this.entryNo.ToString()));
			scaleElement.AppendChild(entryNoElement);

			XmlElement typeElement = xmlDoc.CreateElement("TYPE");
			typeElement.AppendChild(xmlDoc.CreateTextNode(type.ToString()));
			scaleElement.AppendChild(typeElement);

			XmlElement referenceElement = xmlDoc.CreateElement("REFERENCE");
			referenceElement.AppendChild(xmlDoc.CreateTextNode(this.reference));
			scaleElement.AppendChild(referenceElement);

			XmlElement containerNoElement = xmlDoc.CreateElement("CONTAINER_NO");
			containerNoElement.AppendChild(xmlDoc.CreateTextNode(this.containerNo));
			scaleElement.AppendChild(containerNoElement);

			XmlElement containerTypeElement = xmlDoc.CreateElement("CONTAINER_TYPE_CODE");
			containerTypeElement.AppendChild(xmlDoc.CreateTextNode(this.containerTypeCode));
			scaleElement.AppendChild(containerTypeElement);

			XmlElement entryDateElement = xmlDoc.CreateElement("ENTRY_DATE");
			entryDateElement.AppendChild(xmlDoc.CreateTextNode(this.entryDateTime.ToString("yyyy-MM-dd")));
			scaleElement.AppendChild(entryDateElement);

			XmlElement entryTimeElement = xmlDoc.CreateElement("ENTRY_TIME");
			entryTimeElement.AppendChild(xmlDoc.CreateTextNode(this.entryDateTime.ToString("HH:mm:ss")));
			scaleElement.AppendChild(entryTimeElement);

			XmlElement shippingCustomerNoElement = xmlDoc.CreateElement("CUSTOMER_NO");
			shippingCustomerNoElement.AppendChild(xmlDoc.CreateTextNode(this.shippingCustomerNo));
			scaleElement.AppendChild(shippingCustomerNoElement);

			XmlElement categoryCodeElement = xmlDoc.CreateElement("CATEGORY_CODE");
			categoryCodeElement.AppendChild(xmlDoc.CreateTextNode(this.categoryCode));
			scaleElement.AppendChild(categoryCodeElement);

			XmlElement noOfContainersElement = xmlDoc.CreateElement("NO_OF_CONTAINERS");
			noOfContainersElement.AppendChild(xmlDoc.CreateTextNode(this.noOfContainers.ToString()));
			scaleElement.AppendChild(noOfContainersElement);

			XmlElement containerNo2Element = xmlDoc.CreateElement("CONTAINER_NO_2");
			containerNo2Element.AppendChild(xmlDoc.CreateTextNode(this.containerNo2));
			scaleElement.AppendChild(containerNo2Element);

			XmlElement weightElement = xmlDoc.CreateElement("WEIGHT");
			weightElement.AppendChild(xmlDoc.CreateTextNode(this.weight.ToString()));
			scaleElement.AppendChild(weightElement);

			XmlElement agentCodeElement = xmlDoc.CreateElement("TRUCK");
			agentCodeElement.AppendChild(xmlDoc.CreateTextNode(this.agentCode));
			scaleElement.AppendChild(agentCodeElement);

			XmlElement commentlement = xmlDoc.CreateElement("COMMENT");
			commentlement.AppendChild(xmlDoc.CreateTextNode(this.comment));
			scaleElement.AppendChild(commentlement);

			XmlElement lineOrderEntryNoElement = xmlDoc.CreateElement("LINE_ORDER_ENTRY_NO");
			lineOrderEntryNoElement.AppendChild(xmlDoc.CreateTextNode(this.lineOrderEntryNo.ToString()));
			scaleElement.AppendChild(lineOrderEntryNoElement);

			XmlElement statusElement = xmlDoc.CreateElement("STATUS");
			statusElement.AppendChild(xmlDoc.CreateTextNode(this.status.ToString()));
			scaleElement.AppendChild(statusElement);

			return scaleElement;

		}

	}
}
