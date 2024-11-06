using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for ContainerTypes.
	/// </summary>
	public class ContainerServiceEntries
	{

		public ContainerServiceEntries()
		{

		}


		public DataSet getDataSet(Database database, string containerNo)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Container No], [Entry Date], [Entry Time], [User ID], [Factory No] FROM [Container Service Entry] WHERE [Container No] = '"+containerNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "containerServiceEntry");
			adapter.Dispose();

			return dataSet;

		}

		public DataSet getDataSet(Database database)
		{
		
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Entry No], [Container No], [Entry Date], [Entry Time], [User ID], [Factory No] FROM [Container Service Entry] ORDER BY [Entry Date] DESC, [Entry Time] DESC");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "containerServiceEntry");
			adapter.Dispose();

			return dataSet;

		}

	}
}
