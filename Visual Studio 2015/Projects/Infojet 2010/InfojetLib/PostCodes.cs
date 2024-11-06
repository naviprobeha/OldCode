using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for PostCodes.
	/// </summary>
	public class PostCodes
	{
		private Database database;

		public PostCodes(Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
		}

		public DataSet getPostCodes()
		{
			SqlDataAdapter sqlDataAdapter = database.dataAdapterQuery("SELECT [Code], [City] FROM ["+database.getTableName("Post Code")+"] ORDER BY [City]");
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return dataSet;

		}
	}
}
