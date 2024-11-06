using System;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for ShipToAddress.
	/// </summary>
	public class ShipToAddress
	{

		private Database database;

		public string customerNo;
		public string code;
		public string name;
		public string name2;
		public string address;
		public string address2;
		public string postCode;
		public string city;
		public string countryCode;
		public string contactName;
		public string phoneNo;
		public string email;
		public string shipmentMethodCode;
		public string shippingAgentCode;
		public string shippingAgentServiceCode;

		public ShipToAddress(Database database, string customerNo, string code)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				 
			this.customerNo = customerNo;
			this.code = code;

			getFromDatabase();
		}


		private void getFromDatabase()
		{
			SqlDataReader dataReader = database.query("SELECT [Customer No_], [Code], [Name], [Name 2], [Address], [Address 2], [Post Code], [City], [Country_Region Code], [Contact], [Phone No_], [E-Mail], [Shipment Method Code], [Shipping Agent Code], [Shipping Agent Service Code] FROM ["+database.getTableName("Ship-to Address")+"] WHERE [Customer No_] = '"+this.customerNo+"' AND [Code] = '"+this.code+"'");
			if (dataReader.Read())
			{

				customerNo = dataReader.GetValue(0).ToString();
				code = dataReader.GetValue(1).ToString();
				name = dataReader.GetValue(2).ToString();
				name2 = dataReader.GetValue(3).ToString();
				address = dataReader.GetValue(4).ToString();
				address2 = dataReader.GetValue(5).ToString();
				postCode = dataReader.GetValue(6).ToString();
				city = dataReader.GetValue(7).ToString();
				countryCode = dataReader.GetValue(8).ToString();
				contactName = dataReader.GetValue(9).ToString();
				phoneNo = dataReader.GetValue(10).ToString();
				email = dataReader.GetValue(11).ToString();
				shipmentMethodCode = dataReader.GetValue(12).ToString();
				shippingAgentCode = dataReader.GetValue(13).ToString();
				shippingAgentServiceCode = dataReader.GetValue(14).ToString();
			}

			dataReader.Close();


		}

		public void refresh()
		{
			getFromDatabase();
		}

	}
}
