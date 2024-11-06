using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebPageLines.
	/// </summary>
	public class WebSiteCountries
	{
		private Database database;

		public WebSiteCountries(Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
		}

		public DataSet getWebSiteCountries(string webSiteCode)
		{
			SqlDataAdapter sqlDataAdapter = database.dataAdapterQuery("SELECT [Web Site Code], [Country Code], [Name], [Default] FROM ["+database.getTableName("Web Site Country")+"] WHERE [Web Site Code] = '"+webSiteCode+"'");
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);

		}

	}
}
