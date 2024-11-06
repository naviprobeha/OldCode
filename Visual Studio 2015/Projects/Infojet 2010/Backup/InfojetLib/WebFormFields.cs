using System;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebLayoutRows.
	/// </summary>
	public class WebFormFields
	{
		private Database database;


		public WebFormFields(Database database)
		{
			//
			// TODO: Add constructor logic here
			//

			this.database = database;

		}

		public DataSet getWebFormFields(string webFormCode)
		{
            SqlDataAdapter sqlDataAdapter = database.dataAdapterQuery("SELECT [Web Form Code], [Code], [Field Name], [Field Type], [Field Length], [Sort Order], [Placement], [Field Size], [Required], [Web Class Code], [Connection Type], [Value Table No_], [Value Table Name], [Value Code Field Name], [Value Description Field Name], [Value Description 2 Field Name], [Read Only] FROM [" + database.getTableName("Web Form Field") + "] WHERE [Web Form Code] = '" + webFormCode + "' ORDER BY [Sort Order]");
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return dataSet;

		}

	}
}
