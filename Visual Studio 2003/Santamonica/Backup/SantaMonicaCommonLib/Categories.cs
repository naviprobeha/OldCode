using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Categories.
	/// </summary>
	public class Categories
	{
		public Categories()
		{
			//
			// TODO: Add constructor logic here		
			//
		}

		public Category getEntry(Database database, string code)
		{
			Category category = null;
			
			SqlDataReader dataReader = database.query("SELECT [Code], [Description], [Sort Order], [Outgoing] FROM [Category] WHERE [Code] = '"+code+"'");
			if (dataReader.Read())
			{
				category = new Category(dataReader);
			}
			
			dataReader.Close();
			return category;
		}

		public DataSet getDataSet(Database database)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [Sort Order], [Outgoing] FROM [Category] ORDER BY [Sort Order]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "category");
			adapter.Dispose();

			return dataSet;

		}	
	
		public DataSet getDataSet(Database database, bool outgoing)
		{
		
			int outgoingVal = 0;
			if (outgoing) outgoingVal = 1;

			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [Sort Order], [Outgoing] FROM [Category] WHERE [Outgoing] = '"+outgoingVal+"' ORDER BY [Sort Order]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "category");
			adapter.Dispose();

			return dataSet;

		}	

		public DataSet getDataSetEntry(Database database, string code)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [Sort Order], [Outgoing] FROM [Category] WHERE [Code] = '"+code+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "category");
			adapter.Dispose();

			return dataSet;

		}	
	}
}
