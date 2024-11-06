using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for PickupLocations.
	/// </summary>
	public class Factories
	{

		public Factories()
		{

		}

		public Factory getEntry(Database database, string no)
		{
			Factory factory = null;
			
			SqlDataReader dataReader = database.query("SELECT [No], [Name], [Factory Type Code], [Contact Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Position X], [Position Y], [Enabled], [Confirmation ID No], [Container Limit], [Inventory Capacity], [Drop Point], [Shipping Customer No] FROM [Factory] WHERE [No] = '"+no+"'");
			if (dataReader.Read())
			{
				factory = new Factory(dataReader);
			}
			
			dataReader.Close();
			return factory;
		}

		public Factory findPhoneNo(Database database, string phoneNo)
		{
			Factory factory = null;
			
			SqlDataReader dataReader = database.query("SELECT [No], [Name], [Factory Type Code], [Contact Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Position X], [Position Y], [Enabled], [Confirmation ID No], [Container Limit], [Inventory Capacity], [Drop Point], [Shipping Customer No] FROM [Factory] WHERE (REPLACE(REPLACE([Phone No], ' ', ''), '-', '') LIKE '%"+phoneNo+"%') OR (REPLACE(REPLACE([Contact Name], ' ', ''), '-', '') LIKE '%"+phoneNo+"%')");
			if (dataReader.Read())
			{
				factory = new Factory(dataReader);
			}
			
			dataReader.Close();
			return factory;
		}


		public Factory getFirstActiveFactory(Database database)
		{
			Factory factory = null;
			
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Name], [Factory Type Code], [Contact Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Position X], [Position Y], [Enabled], [Confirmation ID No], [Container Limit], [Inventory Capacity], [Drop Point], [Shipping Customer No] FROM [Factory] WHERE [Enabled] = '1'");
			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factory");
			adapter.Dispose();

			int i = 0;
			while (i < dataSet.Tables[0].Rows.Count)
			{
				i++;

				factory = new Factory(dataSet.Tables[0].Rows[i]);
				if (factory.checkConditions(database)) return factory;
			}
			
			return null;
		}


		public DataSet getDataSet(Database database)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Name], [Factory Type Code], [Contact Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Position X], [Position Y], [Enabled], [Confirmation ID No], [Container Limit], [Inventory Capacity], [Drop Point], [Shipping Customer No] FROM [Factory] ORDER BY [No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factory");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSet(Database database, string factoryTypeCode)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Name], [Factory Type Code], [Contact Name], [Address], [Address 2], [Post Code], [City], [Country Code], [Phone No], [Position X], [Position Y], [Enabled], [Confirmation ID No], [Container Limit], [Inventory Capacity], [Drop Point], [Shipping Customer No] FROM [Factory] WHERE [Factory Type Code] = '"+factoryTypeCode+"' ORDER BY [No]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factory");
			adapter.Dispose();

			return dataSet;

		}


		public DataSet getFactoryAgentDataSet(Database database, string factoryCode)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT a.[Code], a.[Description], a.[Last Updated Date], a.[Last Updated Timestamp], a.[Status], a.[Type], a.[Organization No], a.[No Of Containers] FROM [Agent] a, [Line Journal] j WHERE (j.[Arrival Factory Code] = '"+factoryCode+"' OR j.[Departure Factory Code] = '"+factoryCode+"') AND j.[Agent Code] = a.[Code] AND j.[Status] < 8");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "agent");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getMapDataSet(Database database)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [No], [Name], [Position X], [Position Y] FROM [Factory]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factory");
			adapter.Dispose();

			return dataSet;

		}
	}
}
