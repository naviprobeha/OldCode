using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Items.
	/// </summary>
	public class OperatorFactories
	{

		public OperatorFactories()
		{

		}

		public OperatorFactory getEntry(Database database, string userId, string factoryNo)
		{
			OperatorFactory operatorFactory = null;
			
			SqlDataReader dataReader = database.query("SELECT [User ID], [Factory No] FROM [Operator Factory] WHERE [User ID] = '"+userId+"' AND [Factory No] = '"+factoryNo+"'");
			if (dataReader.Read())
			{
				operatorFactory = new OperatorFactory(dataReader);
			}
			
			dataReader.Close();
			
			return operatorFactory;
		}

		public DataSet getDataSet(Database database, string userId)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT o.[User ID], o.[Factory No], f.[Name] FROM [Operator Factory] o, [Factory] f WHERE f.[No] = o.[Factory No] AND o.[User ID] = '"+userId+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "operatorFactory");
			adapter.Dispose();

			return dataSet;

		}

	}
}
