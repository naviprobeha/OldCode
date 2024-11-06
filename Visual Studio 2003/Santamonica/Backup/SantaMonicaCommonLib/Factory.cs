using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for PickupLocation
	/// </summary>
	public class Factory
	{
		public string no;
		public string name;
		public string factoryTypeCode;
		public string contactName;
		public string address;
		public string address2;
		public string postCode;
		public string city;
		public string countryCode;
		public string phoneNo;
		public string confirmationIdNo;
		public int containerLimit;
		public int inventoryCapacity;
		public int positionX;
		public int positionY;
		public bool enabled;
		public bool dropPoint;
		public string shippingCustomerNo;

		private string updateMethod;

		public Factory(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.no = dataReader.GetValue(0).ToString();
			this.name = dataReader.GetValue(1).ToString();
			this.factoryTypeCode = dataReader.GetValue(2).ToString();
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

			this.confirmationIdNo = dataReader.GetValue(13).ToString();
			this.containerLimit = dataReader.GetInt32(14);

			this.inventoryCapacity = dataReader.GetInt32(15);

			this.dropPoint = false;
			if (dataReader.GetValue(16).ToString() == "1") this.dropPoint = true;

			this.shippingCustomerNo = dataReader.GetValue(17).ToString();

			updateMethod = "";


		}

		public Factory(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.no = dataRow.ItemArray.GetValue(0).ToString();
			this.name = dataRow.ItemArray.GetValue(1).ToString();
			this.factoryTypeCode = dataRow.ItemArray.GetValue(2).ToString();
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

			this.confirmationIdNo = dataRow.ItemArray.GetValue(13).ToString();
			this.containerLimit = int.Parse(dataRow.ItemArray.GetValue(14).ToString());
			this.inventoryCapacity = int.Parse(dataRow.ItemArray.GetValue(15).ToString());

			this.dropPoint = false;
			if (dataRow.ItemArray.GetValue(16).ToString() == "1") this.dropPoint = true;

			this.shippingCustomerNo = dataRow.ItemArray.GetValue(17).ToString();

			updateMethod = "";


		}


		public Factory()
		{
		}

		public bool checkConditions(Database database)
		{
			if (this.enabled)
			{
				if (this.containerLimit == 0) return true;

				LineOrderContainers lineOrderContainers = new LineOrderContainers();
				DataSet lineOrderContainerDataSet = lineOrderContainers.getUnScaledDataSet(database, no);
				if (lineOrderContainerDataSet.Tables[0].Rows.Count < this.containerLimit) return true;
			}

			return false;

		}

		public void save(Database database)
		{
			int enabledVal = 0;
			if (enabled) enabledVal = 1;

			int dropPointVal = 0;
			if (dropPoint) dropPointVal = 1;

			try
			{
				if (updateMethod == "D")
				{
					database.nonQuery("DELETE FROM [Factory] WHERE [No] = '"+no+"'");
				}
				else
				{
					SqlDataReader dataReader = database.query("SELECT [No] FROM [Factory] WHERE [No] = '"+no+"'");

					if (dataReader.Read())
					{
						dataReader.Close();
						database.nonQuery("UPDATE [Factory] SET [Name] = '"+name+"', [Factory Type Code] = '"+factoryTypeCode+"', [Contact Name] = '"+contactName+"', [Address] = '"+address+"', [Address 2] = '"+address2+"', [Post Code] = '"+postCode+"', [City] = '"+city+"', [Country Code] = '"+countryCode+"', [Phone No] = '"+phoneNo+"', [Position X] = '"+this.positionX+"', [Position Y] = '"+this.positionY+"', [Enabled] = '"+enabledVal+"', [Confirmation ID No] = '"+this.confirmationIdNo+"', [Container Limit] = '"+this.containerLimit+"', [Inventory Capacity] = '"+this.inventoryCapacity+"', [Drop Point] = '"+dropPointVal+"', [Shipping Customer No] = '"+shippingCustomerNo+"' WHERE [No] = '"+no+"'");
					}
					else
					{
						dataReader.Close();
						database.nonQuery("INSERT INTO [Factory] ([No], [Name], [Factory Type Code], [Contact Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Position X], [Position Y], [Enabled], [Confirmation ID No], [Container Limit], [Inventory Capacity], [Drop Point], [Shipping Customer No]) VALUES ('"+no+"','"+name+"','"+this.factoryTypeCode+"','"+contactName+"','"+address+"','"+address2+"','"+postCode+"','"+city+"','"+countryCode+"','"+phoneNo+"','"+positionX+"','"+positionY+"', '"+enabledVal+"', '"+confirmationIdNo+"', '"+containerLimit+"', '"+inventoryCapacity+"', '"+dropPointVal+"', '"+shippingCustomerNo+"')");
					}

				}
			}
			catch(Exception e)
			{
					
				throw new Exception("Error on factory update: "+e.Message+" ("+database.getLastSQLCommand()+")");
			}

		}

	}
}
