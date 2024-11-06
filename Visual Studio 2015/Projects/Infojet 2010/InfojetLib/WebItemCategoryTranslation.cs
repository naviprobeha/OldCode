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
		public string description;
		private string imageCode;
        public WebImage webImage;
	
		private Database database;

		public WebItemCategoryTranslation(Database database, string webSiteCode, string webItemCategoryCode, string languageCode)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			this.webSiteCode = webSiteCode;
			this.webItemCategoryCode = webItemCategoryCode;
			this.languageCode = languageCode;

			getFromDatabase();
		}


		private void getFromDatabase()
		{

			SqlDataReader dataReader = database.query("SELECT [Web Site Code], [Web Item Category Code], [Language Code], [Description], [Image Code] FROM ["+database.getTableName("Web Item Category Translation")+"] WHERE [Web Site Code] = '"+this.webSiteCode+"' AND [Web Item Category Code] = '"+this.webItemCategoryCode+"' AND [Language Code] = '"+this.languageCode+"'");
			if (dataReader.Read())
			{

				webSiteCode = dataReader.GetValue(0).ToString();
				webItemCategoryCode = dataReader.GetValue(1).ToString();
				languageCode = dataReader.GetValue(2).ToString();
				description = dataReader.GetValue(3).ToString();
				imageCode = dataReader.GetValue(4).ToString();

			}

			dataReader.Close();
			
            if (imageCode != "") webImage = new WebImage(database, imageCode);
		}


	}
}