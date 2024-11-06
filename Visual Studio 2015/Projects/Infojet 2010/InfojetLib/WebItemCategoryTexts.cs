using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebItemCampainMembers.
	/// </summary>
	public class WebItemCategoryTexts
	{
		private Database database;

		public WebItemCategoryTexts(Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
		}

		public DataSet getCategoryTexts(string webSiteCode, string webItemCategoryCode, string languageCode)
		{
			SqlDataAdapter sqlDataAdapter = database.dataAdapterQuery("SELECT [Text] FROM ["+database.getTableName("Web Item Category Text")+"] WHERE [Web Site Code] = '"+webSiteCode+"' AND [Web Item Category Code] = '"+webItemCategoryCode+"' AND [Language Code] = '"+languageCode+"' ORDER BY [Line No_]");
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);

		}

	}
}
