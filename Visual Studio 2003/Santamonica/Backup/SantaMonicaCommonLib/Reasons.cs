using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Items.
	/// </summary>
	public class Reasons
	{

		public Reasons()
		{

		}

		public Reason getEntry(Database database, string code)
		{
			Reason reason = null;
			
			SqlDataReader dataReader = database.query("SELECT [Code], [Description] FROM [Reason] WHERE [Code] = '"+code+"'");
			if (dataReader.Read())
			{
				reason = new Reason(dataReader);
			}
			
			dataReader.Close();
			
			return reason;
		}

		public DataSet getDataSet(Database database)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Code], [Description] FROM [Reason] ORDER BY [Code]");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "reason");
			adapter.Dispose();

			return dataSet;

		}

	}
}
