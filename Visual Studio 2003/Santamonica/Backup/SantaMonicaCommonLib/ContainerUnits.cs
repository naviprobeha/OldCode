using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ContainerTypes.
	/// </summary>
	public class ContainerUnits
	{

		public ContainerUnits()
		{

		}

		public ContainerUnit getEntry(Database database, string Code)
		{
			ContainerUnit containerUnit = null;
			
			SqlDataReader dataReader = database.query("SELECT [Code], [Description], [Volume Factor], [Calculation Type] FROM [Container Unit] WHERE [Code] = '" + Code + "'");
			if (dataReader.Read())
			{
				containerUnit = new ContainerUnit(dataReader);
			}
			
			dataReader.Close();
			return containerUnit;
		}

		public DataSet getDataSet(Database database)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [Volume Factor], [Calculation Type] FROM [Container Unit] ORDER BY [Code]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "containerUnit");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSet(Database database, int calculationType)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [Volume Factor], [Calculation Type] FROM [Container Unit] WHERE [Calculation Type] = '"+calculationType+"' ORDER BY [Code]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "containerUnit");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSetEntry(Database database, string Code)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [Volume Factor], [Calculation Type] FROM [Container Unit] WHERE [Code] = '" + Code + "'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "containerUnit");
			adapter.Dispose();

			return dataSet;

		}
	}
}
