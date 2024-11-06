using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ContainerTypes.
	/// </summary>
	public class ContainerTypes
	{

		public ContainerTypes()
		{

		}

		public ContainerType getEntry(Database database, string Code)
		{
			ContainerType containerType = null;
			
			SqlDataReader dataReader = database.query("SELECT [Code], [Description], [Weight], [Volume], [Unit Code] FROM [Container Type] WHERE [Code] = '" + Code + "'");
			if (dataReader.Read())
			{
				containerType = new ContainerType(dataReader);
			}
			
			dataReader.Close();
			return containerType;
		}

		public DataSet getDataSet(Database database)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [Weight], [Volume], [Unit Code] FROM [Container Type] ORDER BY [Code]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "containerType");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSet(Database database, string unitCode)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [Weight], [Volume], [Unit Code] FROM [Container Type] WHERE [Unit Code] = '"+unitCode+"' ORDER BY [Code]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "containerType");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSetEntry(Database database, string Code)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description], [Weight], [Volume], [Unit Code] FROM [Container Type] WHERE [Code] = '" + Code + "'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "containerType");
			adapter.Dispose();

			return dataSet;

		}
	}
}
