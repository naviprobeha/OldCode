using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for PickupLocation
	/// </summary>
	public class Consumer
	{
		public string no;
		public string name;
		public string consumerTypeCode;
		public string contactName;
		public string address;
		public string address2;
		public string postCode;
		public string city;
		public string countryCode;
		public string phoneNo;
		public int positionX;
		public int positionY;
		public bool enabled;
		public int inventoryCapacity;
		public int inventoryShipmentPoint;
		public string shippingCustomerNo;

		public int presentationUnit;

		private string updateMethod;

		public Consumer(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.no = dataReader.GetValue(0).ToString();
			this.name = dataReader.GetValue(1).ToString();
			this.consumerTypeCode = dataReader.GetValue(2).ToString();
			this.contactName = dataReader.GetValue(3).ToString();
			this.address = dataReader.GetValue(4).ToString();
			this.address2 = dataReader.GetValue(5).ToString();
			this.postCode = dataReader.GetValue(6).ToString();
			this.city = dataReader.GetValue(7).ToString();
			this.countryCode = dataReader.GetValue(8).ToString();
			this.phoneNo = dataReader.GetValue(9).ToString();

			this.positionX = dataReader.GetInt32(10);
			this.positionY = dataReader.GetInt32(11);

			this.enabled = false;
			if (dataReader.GetValue(12).ToString() == "1") this.enabled = true;

			this.inventoryCapacity = dataReader.GetInt32(13);
			this.inventoryShipmentPoint = dataReader.GetInt32(14);

			this.presentationUnit = dataReader.GetInt32(15);

			this.shippingCustomerNo = dataReader.GetValue(16).ToString();

			updateMethod = "";


		}

		public Consumer(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.no = dataRow.ItemArray.GetValue(0).ToString();
			this.name = dataRow.ItemArray.GetValue(1).ToString();
			this.consumerTypeCode = dataRow.ItemArray.GetValue(2).ToString();
			this.contactName = dataRow.ItemArray.GetValue(3).ToString();
			this.address = dataRow.ItemArray.GetValue(4).ToString();
			this.address2 = dataRow.ItemArray.GetValue(5).ToString();
			this.postCode = dataRow.ItemArray.GetValue(6).ToString();
			this.city = dataRow.ItemArray.GetValue(7).ToString();
			this.countryCode = dataRow.ItemArray.GetValue(8).ToString();
			this.phoneNo = dataRow.ItemArray.GetValue(9).ToString();

			this.positionX = int.Parse(dataRow.ItemArray.GetValue(10).ToString());
			this.positionY = int.Parse(dataRow.ItemArray.GetValue(11).ToString());

			this.enabled = false;
			if (dataRow.ItemArray.GetValue(12).ToString() == "1") this.enabled = true;

			this.inventoryCapacity = int.Parse(dataRow.ItemArray.GetValue(13).ToString());
			this.inventoryShipmentPoint = int.Parse(dataRow.ItemArray.GetValue(14).ToString());

			this.presentationUnit = int.Parse(dataRow.ItemArray.GetValue(15).ToString());

			this.shippingCustomerNo = dataRow.ItemArray.GetValue(16).ToString();

			updateMethod = "";


		}


		public Consumer()
		{
			this.name = "";
			this.address = "";
			this.address2 = "";
			this.postCode = "";
			this.city = "";
			this.phoneNo = "";			
								
		}


		public void save(Database database)
		{
			int enabledVal = 0;
			if (enabled) enabledVal = 1;

			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Consumer] WHERE [No] = '"+no+"'");
				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [No] FROM [Consumer] WHERE [No] = '"+no+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Consumer] SET [Name] = '"+name+"', [Consumer Type Code] = '"+consumerTypeCode+"', [Contact Name] = '"+contactName+"', [Address] = '"+address+"', [Address 2] = '"+address2+"', [Post Code] = '"+postCode+"', [City] = '"+city+"', [Country Code] = '"+countryCode+"', [Phone No] = '"+phoneNo+"', [Position X] = '"+this.positionX+"', [Position Y] = '"+this.positionY+"', [Enabled] = '"+enabledVal+"', [Inventory Capacity] = '"+inventoryCapacity+"', [Inventory Shipment Point] = '"+inventoryShipmentPoint+"', [Presentation Unit] = '"+presentationUnit+"', [Shipping Customer No] = '"+shippingCustomerNo+"' WHERE [No] = '"+no+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Consumer] ([No], [Name], [Consumer Type Code], [Contact Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Position X], [Position Y], [Enabled], [Inventory Capacity], [Inventory Shipment Point], [Presentation Unit], [Shipping Customer No]) VALUES ('"+no+"','"+name+"','"+this.consumerTypeCode+"','"+contactName+"','"+address+"','"+address2+"','"+postCode+"','"+city+"','"+countryCode+"','"+phoneNo+"','"+positionX+"','"+positionY+"', '"+enabledVal+"', '"+inventoryCapacity+"', '"+inventoryShipmentPoint+"', '"+presentationUnit+"', '"+shippingCustomerNo+"')");
					}

				}
			}
			catch(Exception e)
			{
					
				throw new Exception("Error on consumer update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}

	}
}
