using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for PickupLocations.
	/// </summary>
	public class Consumers
	{

		public Consumers()
		{

		}

		public Consumer getEntry(Database database, string no)
		{
			Consumer consumer = null;
			
			SqlDataReader dataReader = database.query("SELECT [No], [Name], [Consumer Type Code], [Contact Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Position X], [Position Y], [Enabled], [Inventory Capacity], [Inventory Shipment Point], [Presentation Unit], [Shipping Customer No] FROM [Consumer] WHERE [No] = '"+no+"'");
			if (dataReader.Read())
			{
				consumer = new Consumer(dataReader);
			}
			
			dataReader.Close();
			return consumer;
		}

		public Consumer getShippingCustomerEntry(Database database, string shippingCustomerNo)
		{
			Consumer consumer = null;
			
			SqlDataReader dataReader = database.query("SELECT [No], [Name], [Consumer Type Code], [Contact Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Position X], [Position Y], [Enabled], [Inventory Capacity], [Inventory Shipment Point], [Presentation Unit], [Shipping Customer No] FROM [Consumer] WHERE [Shipping Customer No] = '"+shippingCustomerNo+"'");
			if (dataReader.Read())
			{
				consumer = new Consumer(dataReader);
			}
			
			dataReader.Close();
			return consumer;
		}

		public Consumer findPhoneNo(Database database, string phoneNo)
		{
			Consumer consumer = null;
			
			SqlDataReader dataReader = database.query("SELECT [No], [Name], [Consumer Type Code], [Contact Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Position X], [Position Y], [Enabled], [Inventory Capacity], [Inventory Shipment Point], [Presentation Unit], [Shipping Customer No] FROM [Consumer] WHERE REPLACE(REPLACE([Phone No], ' ', ''), '-', '') = '"+phoneNo+"'");
			if (dataReader.Read())
			{
				consumer = new Consumer(dataReader);
			}
			
			dataReader.Close();
			return consumer;
		}


		public DataSet getDataSet(Database database)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Name], [Consumer Type Code], [Contact Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Position X], [Position Y], [Enabled], [Inventory Capacity], [Inventory Shipment Point], [Presentation Unit], [Shipping Customer No] FROM [Consumer] ORDER BY [No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "consumer");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getMapDataSet(Database database)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Name], [Position X], [Position Y] FROM [Consumer]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "consumer");
			adapter.Dispose();

			return dataSet;

		}

		public bool checkShippingCustomer(Database database, string shippingCustomerNo)
		{
			bool found = false;
			
			SqlDataReader dataReader = database.query("SELECT [No], [Name], [Consumer Type Code], [Contact Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Position X], [Position Y], [Enabled], [Inventory Capacity], [Inventory Shipment Point], [Presentation Unit], [Shipping Customer No] FROM [Consumer] WHERE [Shipping Customer No] = '"+shippingCustomerNo+"'");
			if (dataReader.Read())
			{
				found = true;
			}
			
			dataReader.Close();
			return found;
		}

		public Consumer getFromShippingCustomer(Database database, string shippingCustomerNo)
		{
			Consumer consumer = null;
			
			SqlDataReader dataReader = database.query("SELECT [No], [Name], [Consumer Type Code], [Contact Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Position X], [Position Y], [Enabled], [Inventory Capacity], [Inventory Shipment Point], [Presentation Unit], [Shipping Customer No] FROM [Consumer] WHERE [Shipping Customer No] = '"+shippingCustomerNo+"'");
			if (dataReader.Read())
			{
				consumer = new Consumer(dataReader);
			}
			
			dataReader.Close();
			return consumer;
		}


	}
}
