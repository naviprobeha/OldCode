using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebSite.
	/// </summary>
	public class WebTemplate
	{
		public string webSiteCode;
		public string code;
		public string description;
		public string filename;
	
		private Database database;

		public WebTemplate(Database database, string webSiteCode, string code)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			this.webSiteCode = webSiteCode;
			this.code = code;

			getFromDatabase();
		}


		private void getFromDatabase()
		{

			SqlDataReader dataReader = database.query("SELECT [Web Site Code], [Code], [Description], [Filename] FROM ["+database.getTableName("Web Template")+"] WHERE [Web Site Code] = '"+this.webSiteCode+"' AND [Code] = '"+this.code+"'");
			if (dataReader.Read())
			{

				webSiteCode = dataReader.GetValue(0).ToString();
				code = dataReader.GetValue(1).ToString();
				description = dataReader.GetValue(2).ToString();
				filename = dataReader.GetValue(3).ToString();
				

			}

			dataReader.Close();
			
		}

		public DataSet getParts()
		{
			SqlDataAdapter sqlDataAdapter = database.dataAdapterQuery("SELECT [Web Site Code], [Web Template Code], [Code], [Description] FROM ["+database.getTableName("Web Template Part")+"] WHERE [Web Site Code] = '"+this.webSiteCode+"' AND [Web Template Code] = '"+this.code+"'");
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);

		}

	}
}
