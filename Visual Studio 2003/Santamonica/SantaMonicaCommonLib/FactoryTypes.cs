using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ContainerTypes.
	/// </summary>
	public class FactoryTypes
	{

		public FactoryTypes()
		{

		}

		public FactoryType getEntry(Database database, string Code)
		{
			FactoryType factoryType = null;
			
			SqlDataReader dataReader = database.query("SELECT [Code], [Description] FROM [Factory Type] WHERE [Code] = '" + Code + "'");
			if (dataReader.Read())
			{
				factoryType = new FactoryType(dataReader);
			}
			
			dataReader.Close();
			return factoryType;
		}

		public DataSet getDataSet(Database database)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description] FROM [Factory Type] ORDER BY [Code]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryType");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSetEntry(Database database, string Code)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description] FROM [Factory Type] WHERE [Code] = '" + Code + "'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "factoryType");
			adapter.Dispose();

			return dataSet;

		}
	}
}
