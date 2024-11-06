using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ShippingCustomers.
	/// </summary>
	public class ShippingCustomers
	{
		private string searchShippingCustomerNo = "";
		private string searchRegNo = "";
		private string searchName = "";
		private string searchProductionNo = "";
		private string searchPhoneNo = "";
		private string searchCity = "";
		
		public ShippingCustomers(XmlElement tableElement, Database database)
		
		{
			//
			// TODO: Add constructor logic here
			//

			fromDOM(tableElement, database);
		}

		public ShippingCustomers()
		{

		}

		public ShippingCustomer getEntry(Database database, string no)
		{
			ShippingCustomer shippingCustomer = null;
			
			SqlDataReader dataReader = database.query("SELECT [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Fax No], [E-mail], [Price Group Code], [Production Site], [Registration No], [Contact Name], [Hide], [Blocked], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Route Group Code], [Priority], [Prefered Factory No], [Reason Code] FROM [Shipping Customer] WHERE [No] = '"+no+"'");
			if (dataReader.Read())
			{
				//Console.WriteLine("hittade kund");
				shippingCustomer = new ShippingCustomer(dataReader);
			}
			
			dataReader.Close();
			return shippingCustomer;
		}

		public ShippingCustomer findPhoneNo(Database database, string phoneNo)
		{
			ShippingCustomer shippingCustomer = null;
			
			SqlDataReader dataReader = database.query("SELECT [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Fax No], [E-mail], [Price Group Code], [Production Site], [Registration No], [Contact Name], [Hide], [Blocked], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Route Group Code], [Priority], [Prefered Factory No], [Reason Code] FROM [Shipping Customer] WHERE (REPLACE(REPLACE([Phone No], ' ', ''), '-', '') LIKE '%"+phoneNo+"%') OR (REPLACE(REPLACE([Cell Phone No], ' ', ''), '-', '') LIKE '%"+phoneNo+"%') OR (REPLACE(REPLACE([Contact Name], ' ', ''), '-', '') LIKE '%"+phoneNo+"%')");
			if (dataReader.Read())
			{
				//Console.WriteLine("hittade kund");
				shippingCustomer = new ShippingCustomer(dataReader);
			}
			
			dataReader.Close();
			return shippingCustomer;
		}

		public DataSet getDataSet(Database database)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Fax No], [E-mail], [Price Group Code], [Production Site], [Registration No], [Contact Name], [Hide], [Blocked], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Route Group Code], [Priority], [Prefered Factory No], [Reason Code] FROM [Shipping Customer] WHERE [Hide] = 0 "+this.searchShippingCustomerNo+this.searchName+this.searchPhoneNo+this.searchProductionNo+this.searchRegNo+this.searchCity+" ORDER BY [Name]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shippingCustomer");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSet(Database database, int orderType)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT DISTINCT [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Fax No], [E-mail], [Price Group Code], [Production Site], [Registration No], [Contact Name], [Hide], [Blocked], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Route Group Code], [Priority], [Prefered Factory No], [Reason Code] FROM [Shipping Customer] c, [Shipping Customer Organization] o WHERE [Hide] = 0 AND c.[No] = o.[Shipping Customer No] AND o.[Order Type] = '"+orderType+"' ORDER BY [Name]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shippingCustomer");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSet(Database database, string reasonCode)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Fax No], [E-mail], [Price Group Code], [Production Site], [Registration No], [Contact Name], [Hide], [Blocked], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Route Group Code], [Priority], [Prefered Factory No], [Reason Code] FROM [Shipping Customer] WHERE [Hide] = 0 AND [Reason Code] = '"+reasonCode+"' ORDER BY [Name]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shippingCustomer");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getListDataSet(Database database)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Fax No], [E-mail], [Price Group Code], [Production Site], [Registration No], [Contact Name], [Hide], [Blocked], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Route Group Code], [Priority], [Prefered Factory No], [Reason Code] FROM [Shipping Customer] WHERE [Hide] = 0 AND [Blocked] = 0 "+this.searchShippingCustomerNo+this.searchName+this.searchPhoneNo+this.searchProductionNo+this.searchRegNo+this.searchCity+" ORDER BY [Name]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shippingCustomer");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getMapDataSet(Database database)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Fax No], [E-mail], [Price Group Code], [Production Site], [Registration No], [Contact Name], [Hide], [Blocked], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Route Group Code], [Priority], [Prefered Factory No], [Reason Code] FROM [Shipping Customer] WHERE [Hide] = 0 AND ([Position X] > 0 OR [Position Y] > 0)");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shippingCustomer");
			adapter.Dispose();

			return dataSet;

		}


		public void fromDOM(XmlElement tableElement, Database database)
		{
			XmlNodeList records = tableElement.GetElementsByTagName("R");
			int i = 0;
			while (i < records.Count)
			{
				XmlElement record = (XmlElement)records.Item(i);
	
				ShippingCustomer shippingCustomer = new ShippingCustomer(record, database, true);

				i++;
			}
		}

		public DataSet getDataSetEntry(Database database, string no)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Cell Phone No], [Fax No], [E-mail], [Price Group Code], [Production Site], [Registration No], [Contact Name], [Hide], [Blocked], [Direction Comment], [Direction Comment 2], [Position X], [Position Y], [Route Group Code], [Priority], [Prefered Factory No], [Reason Code] FROM [Shipping Customer] WHERE [No] = '"+no+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shippingCustomer");
			adapter.Dispose();

			return dataSet;

		}


		public void setSearchCriteria(string shippingCustomerNo, string registrationNo, string name, string city, string productionNo, string phoneNo)
		{
			if (shippingCustomerNo != "") this.searchShippingCustomerNo = " AND UPPER([No]) = UPPER('"+shippingCustomerNo+"')";
			if (registrationNo != "") this.searchRegNo = " AND UPPER([Registration No]) LIKE UPPER('%"+registrationNo+"%')";
			if (name != "") this.searchName = " AND UPPER([Name]) LIKE UPPER('%"+name+"%')";
			if (productionNo != "") this.searchProductionNo = " AND UPPER([Production Site]) LIKE UPPER('%"+productionNo+"%')";
			if (phoneNo != "") this.searchPhoneNo = " AND (UPPER([Phone No]) LIKE UPPER('%"+phoneNo+"%') OR UPPER([Cell Phone No]) LIKE UPPER('%"+phoneNo+"%') OR UPPER([Contact Name]) LIKE UPPER('%"+phoneNo+"%'))";
			if (city != "") this.searchCity = " AND UPPER([City]) LIKE UPPER('%"+city+"%')";
		}

	}
}