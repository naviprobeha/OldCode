using System;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebLayoutRows.
	/// </summary>
	public class WebFormFieldValues
	{
		private Database database;


		public WebFormFieldValues(Database database)
		{
			//
			// TODO: Add constructor logic here
			//

			this.database = database;

		}

		public DataSet getWebFormFieldValues(string webFormCode, string fieldCode)
		{
			SqlDataAdapter sqlDataAdapter = database.dataAdapterQuery("SELECT [Web Form Code], [Field Code], [Code], [Description], [Default] FROM ["+database.getTableName("Web Form Field Value")+"] WHERE [Web Form Code] = '"+webFormCode+"' AND [Field Code] = '"+fieldCode+"'");
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return dataSet;

		}

	}
}
