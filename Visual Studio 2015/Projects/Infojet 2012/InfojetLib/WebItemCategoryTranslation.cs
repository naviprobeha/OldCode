using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebSite.
	/// </summary>
	public class WebItemCategoryTranslation
	{
		public string webSiteCode;
		public string webItemCategoryCode;
		public string languageCode;
		public string description = "";
		private string imageCode;
        public WebImage webImage = null;
	
		private Infojet infojetContext;

        public WebItemCategoryTranslation(Infojet infojetContext, string webSiteCode, string webItemCategoryCode, string languageCode)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
			this.webSiteCode = webSiteCode;
			this.webItemCategoryCode = webItemCategoryCode;
			this.languageCode = languageCode;

			getFromDatabase();
		}


		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Web Item Category Code], [Language Code], [Description], [Image Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category Translation") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Item Category Code] = @webItemCategoryCode AND [Language Code] = @languageCode");
            databaseQuery.addStringParameter("webItemCategoryCode", webItemCategoryCode, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{

				webSiteCode = dataReader.GetValue(0).ToString();
				webItemCategoryCode = dataReader.GetValue(1).ToString();
				languageCode = dataReader.GetValue(2).ToString();
				description = dataReader.GetValue(3).ToString();
				imageCode = dataReader.GetValue(4).ToString();

			}

			dataReader.Close();

            if (imageCode != "") webImage = new WebImage(infojetContext, imageCode);
		}


	}
}