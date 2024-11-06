using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Shipment.
	/// </summary>
	public class ShipmentHeader
	{
		public string organizationNo;
		public string no;
		public DateTime shipDate;
		public string customerNo;
		public string customerName;
		public string address;
		public string address2;
		public string postCode;
		public string city;
		public string countryCode;
		public string phoneNo;
		public string cellPhoneNo;
		public string productionSite;
		public int payment;
		public string paymentText;
		public int status;
		public string dairyCode;
		public string dairyNo;
		public string reference;
		public string containerNo;
		public string userName;
		public int shipOrderEntryNo;
		public int lineOrderEntryNo;

		public string agentCode;

		public int positionX;
		public int positionY;

		public string customerShipAddressNo;
		public string shipName;
		public string shipAddress;
		public string shipAddress2;
		public string shipPostCode;
		public string shipCity;

		public string invoiceNo;

		private string updateMethod;
		private Database database;

		public ShipmentHeader(Database database, string agentCode, DataSet dataSet)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			this.agentCode = agentCode;
			containerNo = "";
			fromDataSet(dataSet);

			//Detta fungerar inte då täckningen försvinner och följesedeln behöver skickas om.
			//checkIfExists(database);

			Agents agents = new Agents();
			Agent agent = agents.getAgent(database, agentCode);
			if (agent != null)
			{
				if (this.organizationNo == "") this.organizationNo = agent.organizationNo;
			}

			save(database);

			updateMethod = "";

		}


		public ShipmentHeader(Database database, string no)
		{	
			this.database = database;
			this.no = no;

			getFromDb();

		}

		public ShipmentHeader(Database database, SqlDataReader dataReader)
		{	
			this.database = database;

			readData(dataReader);
		}

		public ShipmentHeader(DataRow dataRow)
		{	
			fromDataRow(dataRow);
		}

		public void fromDataSet(DataSet dataset)
		{

			no = this.agentCode +"-"+ dataset.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
			organizationNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
			shipDate = DateTime.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
			customerNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
			customerName = dataset.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
			address = dataset.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
			address2 = dataset.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
			postCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
			city = dataset.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
			countryCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
			phoneNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
			cellPhoneNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
			productionSite = dataset.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
			positionX = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(14).ToString());
			positionY = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(15).ToString());
			payment = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(16).ToString());
			dairyCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
			dairyNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
			reference = dataset.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
			userName = dataset.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
			containerNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
			shipOrderEntryNo = int.Parse(dataset.Tables[0].Rows[0].ItemArray.GetValue(22).ToString());

			//Fix 2008-04-15
			shipDate = shipDate.AddHours(6);

			if (dataset.Tables[0].Rows[0].ItemArray.Length > 23)
			{
				this.customerShipAddressNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(23).ToString();			
				this.shipName = dataset.Tables[0].Rows[0].ItemArray.GetValue(24).ToString();			
				this.shipAddress = dataset.Tables[0].Rows[0].ItemArray.GetValue(25).ToString();			
				this.shipAddress2 = dataset.Tables[0].Rows[0].ItemArray.GetValue(26).ToString();			
				this.shipPostCode = dataset.Tables[0].Rows[0].ItemArray.GetValue(27).ToString();			
				this.shipCity = dataset.Tables[0].Rows[0].ItemArray.GetValue(28).ToString();			
			}

			if (dataset.Tables[0].Rows[0].ItemArray.Length > 29)
			{
				this.invoiceNo = dataset.Tables[0].Rows[0].ItemArray.GetValue(29).ToString();			
			}
		
			if (phoneNo.Length > 20) phoneNo = phoneNo.Substring(1, 20);
			if (cellPhoneNo.Length > 20) cellPhoneNo = cellPhoneNo.Substring(1, 20);
			if (customerName.Length > 50) customerName = customerName.Substring(1, 50);
			if (address.Length > 50) address = address.Substring(1, 50);
			if (address2.Length > 50) address2 = address2.Substring(1, 50);
			if (city.Length > 50) city = city.Substring(1, 50);


		}

		public void fromDataRow(DataRow dataRow)
		{

			no = dataRow.ItemArray.GetValue(0).ToString();
			organizationNo = dataRow.ItemArray.GetValue(1).ToString();
			try
			{
				shipDate = DateTime.Parse(dataRow.ItemArray.GetValue(2).ToString());
			}
			catch(Exception e)
			{
				throw new Exception(no+": "+dataRow.ItemArray.GetValue(2).ToString()+", "+e.Message);
			}
			customerNo = dataRow.ItemArray.GetValue(3).ToString();
			customerName = dataRow.ItemArray.GetValue(4).ToString();
			address = dataRow.ItemArray.GetValue(5).ToString();
			address2 = dataRow.ItemArray.GetValue(6).ToString();
			postCode = dataRow.ItemArray.GetValue(7).ToString();
			city = dataRow.ItemArray.GetValue(8).ToString();
			countryCode = dataRow.ItemArray.GetValue(9).ToString();
			phoneNo = dataRow.ItemArray.GetValue(10).ToString();
			cellPhoneNo = dataRow.ItemArray.GetValue(11).ToString();
			productionSite = dataRow.ItemArray.GetValue(12).ToString();
			payment = int.Parse(dataRow.ItemArray.GetValue(13).ToString());
			dairyCode = dataRow.ItemArray.GetValue(14).ToString();
			dairyNo = dataRow.ItemArray.GetValue(15).ToString();
			reference = dataRow.ItemArray.GetValue(16).ToString();
			agentCode = dataRow.ItemArray.GetValue(17).ToString();
			containerNo = dataRow.ItemArray.GetValue(18).ToString();
			userName = dataRow.ItemArray.GetValue(19).ToString();
			invoiceNo = dataRow.ItemArray.GetValue(20).ToString();
		}

		public XmlElement toDOM(XmlDocument xmlDoc)
		{
			XmlElement shipmentElement = xmlDoc.CreateElement("SHIPMENT_HEADER");

			XmlElement noElement = xmlDoc.CreateElement("NO");
			noElement.AppendChild(xmlDoc.CreateTextNode(this.no));
			shipmentElement.AppendChild(noElement);

			XmlElement organizationNoElement = xmlDoc.CreateElement("ORGANIZATION_NO");
			organizationNoElement.AppendChild(xmlDoc.CreateTextNode(this.organizationNo));
			shipmentElement.AppendChild(organizationNoElement);

			XmlElement shipmentDateElement = xmlDoc.CreateElement("SHIPMENT_DATE");
			shipmentDateElement.AppendChild(xmlDoc.CreateTextNode(this.shipDate.ToString("yyyy-MM-dd")));
			shipmentElement.AppendChild(shipmentDateElement);

			XmlElement customerNoElement = xmlDoc.CreateElement("CUSTOMER_NO");
			customerNoElement.AppendChild(xmlDoc.CreateTextNode(this.customerNo));
			shipmentElement.AppendChild(customerNoElement);

			XmlElement customerNameElement = xmlDoc.CreateElement("CUSTOMER_NAME");
			customerNameElement.AppendChild(xmlDoc.CreateTextNode(this.customerName));
			shipmentElement.AppendChild(customerNameElement);

			XmlElement addressElement = xmlDoc.CreateElement("ADDRESS");
			addressElement.AppendChild(xmlDoc.CreateTextNode(this.address));
			shipmentElement.AppendChild(addressElement);

			XmlElement address2Element = xmlDoc.CreateElement("ADDRESS_2");
			address2Element.AppendChild(xmlDoc.CreateTextNode(this.address2));
			shipmentElement.AppendChild(address2Element);

			XmlElement postCodeElement = xmlDoc.CreateElement("POST_CODE");
			postCodeElement.AppendChild(xmlDoc.CreateTextNode(this.postCode));
			shipmentElement.AppendChild(postCodeElement);

			XmlElement cityElement = xmlDoc.CreateElement("CITY");
			cityElement.AppendChild(xmlDoc.CreateTextNode(this.city));
			shipmentElement.AppendChild(cityElement);

			XmlElement countryCodeElement = xmlDoc.CreateElement("COUNTRY_CODE");
			countryCodeElement.AppendChild(xmlDoc.CreateTextNode(this.countryCode));
			shipmentElement.AppendChild(countryCodeElement);

			XmlElement phoneNoElement = xmlDoc.CreateElement("PHONE_NO");
			phoneNoElement.AppendChild(xmlDoc.CreateTextNode(this.phoneNo));
			shipmentElement.AppendChild(phoneNoElement);

			XmlElement cellPhoneNoElement = xmlDoc.CreateElement("CELL_PHONE_NO");
			cellPhoneNoElement.AppendChild(xmlDoc.CreateTextNode(this.cellPhoneNo));
			shipmentElement.AppendChild(cellPhoneNoElement);

			XmlElement productionSiteElement = xmlDoc.CreateElement("PRODUCTION_SITE");
			productionSiteElement.AppendChild(xmlDoc.CreateTextNode(this.productionSite));
			shipmentElement.AppendChild(productionSiteElement);

			XmlElement paymentTypeElement = xmlDoc.CreateElement("PAYMENT_TYPE");
			paymentTypeElement.AppendChild(xmlDoc.CreateTextNode(this.payment.ToString()));
			shipmentElement.AppendChild(paymentTypeElement);

			XmlElement dairyCodeElement = xmlDoc.CreateElement("DAIRY_CODE");
			dairyCodeElement.AppendChild(xmlDoc.CreateTextNode(this.dairyCode));
			shipmentElement.AppendChild(dairyCodeElement);

			XmlElement dairyNoElement = xmlDoc.CreateElement("DAIRY_NO");
			dairyNoElement.AppendChild(xmlDoc.CreateTextNode(this.dairyNo));
			shipmentElement.AppendChild(dairyNoElement);

			XmlElement referenceElement = xmlDoc.CreateElement("REFERENCE");
			referenceElement.AppendChild(xmlDoc.CreateTextNode(this.reference));
			shipmentElement.AppendChild(referenceElement);

			XmlElement truckCodeElement = xmlDoc.CreateElement("TRUCK_CODE");
			truckCodeElement.AppendChild(xmlDoc.CreateTextNode(this.agentCode));
			shipmentElement.AppendChild(truckCodeElement);

			XmlElement userNameElement = xmlDoc.CreateElement("USER_NAME");
			userNameElement.AppendChild(xmlDoc.CreateTextNode(this.userName));
			shipmentElement.AppendChild(userNameElement);

			XmlElement containerNoElement = xmlDoc.CreateElement("CONTAINER_NO");
			containerNoElement.AppendChild(xmlDoc.CreateTextNode(this.containerNo));
			shipmentElement.AppendChild(containerNoElement);

			XmlElement invoiceNoElement = xmlDoc.CreateElement("INVOICE_NO");
			invoiceNoElement.AppendChild(xmlDoc.CreateTextNode(this.invoiceNo));
			shipmentElement.AppendChild(invoiceNoElement);

			return shipmentElement;

		}

		private void checkIfExists(Database database)
		{
			SqlDataReader dataReader = database.query("SELECT [Status] FROM [Shipment Header] WHERE [No] = '"+no+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				throw new Exception("Shipment "+no+" already exists.");
			}
			dataReader.Close();

		}

		public void save(Database database)
		{

			SqlDataReader dataReader = database.query("SELECT [Status] FROM [Shipment Header] WHERE [No] = '"+no+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				if ((updateMethod != null) && (updateMethod.Equals("D")))
				{
					database.nonQuery("DELETE FROM [Shipment Header] WHERE [No] = '"+no+"'");
				}

				else
				{
					database.nonQuery("UPDATE [Shipment Header] SET [Customer No] = '"+customerNo+"', [Customer Name] = '"+customerName+"', [Address] = '"+address+"', [Address 2] = '"+address2+"', [Post Code] = '"+postCode+"', [City] = '"+city+"', [Country Code] = '"+countryCode+"', [Phone No] = '"+phoneNo+"', [Cell Phone No] = '"+cellPhoneNo+"', [Production Site] = '"+this.productionSite+"', [Shipment Date] = '"+this.shipDate.ToString("yyyy-MM-dd")+"', [Agent Code] = '"+this.agentCode+"', [Payment Type] = '"+this.payment+"', [Dairy Code] = '"+dairyCode+"', [Dairy No] = '"+dairyNo+"', [Reference] = '"+reference+"', [Container No] = '"+containerNo+"', [User Name] = '"+userName+"', [Ship Order Entry No] = '"+shipOrderEntryNo+"', [Position X] = '"+positionX+"', [Position Y] = '"+positionY+"', [Customer Ship Address No] = '"+this.customerShipAddressNo+"', [Ship Name] = '"+this.shipName+"', [Ship Address] = '"+this.shipAddress+"', [Ship Address 2] = '"+this.shipAddress2+"', [Ship Post Code] = '"+this.shipPostCode+"', [Ship City] = '"+this.shipCity+"', [Invoice No] = '"+invoiceNo+"', [Line Order Entry No] = '"+lineOrderEntryNo+"' WHERE [Organization No] = '"+organizationNo+"' AND [No] = '"+no+"'");
				}
			}
			else
			{
				dataReader.Close();
				database.nonQuery("INSERT INTO [Shipment Header] ([Organization No], [No], [Customer No], [Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Production Site], [Shipment Date], [Agent Code], [Payment Type], [Dairy Code], [Dairy No], [Reference], [Status], [Container No], [User Name], [Ship Order Entry No], [Position X], [Position Y], [Customer Ship Address No], [Ship Name], [Ship Address], [Ship Address 2], [Ship Post Code], [Ship City], [Invoice No], [Line Order Entry No]) VALUES ('"+this.organizationNo+"','"+this.no+"','"+this.customerNo+"','"+this.customerName+"','"+this.address+"','"+this.address2+"','"+this.postCode+"','"+this.city+"','"+this.countryCode+"','"+this.phoneNo+"','"+this.cellPhoneNo+"','"+this.productionSite+"','"+this.shipDate.ToString("yyyy-MM-dd")+"','"+this.agentCode+"','"+this.payment+"','"+this.dairyCode+"','"+this.dairyNo+"','"+this.reference+"', 0, '"+this.containerNo+"','"+userName+"','"+shipOrderEntryNo+"','"+positionX+"','"+positionY+"', '"+this.customerShipAddressNo+"', '"+this.shipName+"', '"+this.shipAddress+"', '"+this.shipAddress2+"', '"+this.shipPostCode+"', '"+this.shipCity+"', '"+invoiceNo+"', '"+lineOrderEntryNo+"')");
			}

		}

		public bool getFromDb()
		{
		
			SqlDataReader dataReader = database.query("SELECT [Organization No], [No], [Customer No], [Customer Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Production Site], [Shipment Date], [Agent Code], [Payment Type], [Dairy Code], [Dairy No], [Reference], [Container No], [User Name], [Ship Order Entry No], [Position X], [Position Y], [Customer Ship Address No], [Ship Name], [Ship Address], [Ship Address 2], [Ship Post Code], [Ship City], [Invoice No], [Line Order Entry No] FROM [Shipment Header] WHERE No = '"+no+"'");

			if (dataReader.Read())
			{
				readData(dataReader);

				dataReader.Close();
				return true;
			}
			dataReader.Close();
			return false;
			
		}

		private void readData(SqlDataReader dataReader)
		{
			this.organizationNo = dataReader.GetValue(0).ToString();
			this.no = dataReader.GetValue(1).ToString();
			this.customerNo = dataReader.GetValue(2).ToString();
			this.customerName = dataReader.GetValue(3).ToString();
			this.address = dataReader.GetValue(4).ToString();
			this.address2 = dataReader.GetValue(5).ToString();
			this.postCode = dataReader.GetValue(6).ToString();
			this.city = dataReader.GetValue(7).ToString();	
			this.countryCode = dataReader.GetValue(8).ToString();
			this.phoneNo = dataReader.GetValue(9).ToString();
			this.cellPhoneNo = dataReader.GetValue(10).ToString();
			this.productionSite = dataReader.GetValue(11).ToString();
			this.shipDate = dataReader.GetDateTime(12);
			this.agentCode = dataReader.GetValue(13).ToString();
			this.payment = dataReader.GetInt32(14);
			this.dairyCode = dataReader.GetValue(15).ToString();
			this.dairyNo = dataReader.GetValue(16).ToString();
			this.reference = dataReader.GetValue(17).ToString();
			this.containerNo = dataReader.GetValue(18).ToString();
			this.userName = dataReader.GetValue(19).ToString();
			this.shipOrderEntryNo = dataReader.GetInt32(20);
			this.positionX = dataReader.GetInt32(21);
			this.positionY = dataReader.GetInt32(22);
		
			if (this.payment == 2)
				paymentText = "Kort";
			if (this.payment == 1)
				paymentText = "Kontant";
			if (this.payment == 0)
				paymentText = "Faktura";

			this.customerShipAddressNo = dataReader.GetValue(23).ToString();
			this.shipName = dataReader.GetValue(24).ToString();
			this.shipAddress = dataReader.GetValue(25).ToString();
			this.shipAddress2 = dataReader.GetValue(26).ToString();
			this.shipPostCode = dataReader.GetValue(27).ToString();
			this.shipCity = dataReader.GetValue(28).ToString();
			this.invoiceNo = dataReader.GetValue(29).ToString();
			this.lineOrderEntryNo = dataReader.GetInt32(30);
		}


		public DataSet getShipmentLinesDataSet()
		{
			ShipmentLines shipmentLines = new ShipmentLines(database);
			return shipmentLines.getShipmentLinesDataSet(this.no);

		}

		public void setStatus(Database database, int status)
		{
			database.nonQuery("UPDATE [Shipment Header] SET [Status] = '"+status.ToString()+"' WHERE [Organization No] = '"+organizationNo+"' AND [No] = '"+no+"'");

		}

		public bool containsPostMortems(Database database)
		{	
			ShipmentLineIds shipmentLineIds = new ShipmentLineIds(database);
			return shipmentLineIds.shipmentContainsPostMortem(this.no);
		}

		public float getWeight(Database database)
		{
			ShipmentLines shipmentLines = new ShipmentLines(database);
			return shipmentLines.getWeight(this.no);
		}
	}
}
