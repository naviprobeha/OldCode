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

        public WebTemplate()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public WebTemplate(Infojet infojetContext, DataRow dataRow)
        {
            database = infojetContext.systemDatabase;

            webSiteCode = dataRow.ItemArray.GetValue(0).ToString();
            code = dataRow.ItemArray.GetValue(1).ToString();
            description = dataRow.ItemArray.GetValue(2).ToString();
            filename = dataRow.ItemArray.GetValue(3).ToString();

        }

		private void getFromDatabase()
		{

            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Site Code], [Code], [Description], [Filename] FROM [" + database.getTableName("Web Template") + "] WHERE [Web Site Code] = @webSiteCode AND [Code] = @code");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("code", code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
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
            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Site Code], [Web Template Code], [Code], [Description], [Sort Order], [Position X], [Position Y], [Width], [Height], [Component Placement] FROM [" + database.getTableName("Web Template Part") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Template Code] = @webTemplateCode");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("webTemplateCode", code, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);

		}

        public static DataSet getWebTemplates(Infojet infojetContext, string webSiteCode)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Code], [Description], [Filename] FROM [" + infojetContext.systemDatabase.getTableName("Web Template") + "] WHERE [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return (dataSet);

        }


	}
}
