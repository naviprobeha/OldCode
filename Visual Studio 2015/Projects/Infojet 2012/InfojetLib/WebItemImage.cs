using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebItemImage.
	/// </summary>
	public class WebItemImage
	{
        private Infojet infojetContext;

		public string itemNo;
		public string webImageCode;
		public int type;
		public string webSiteCode;
		public string webItemCampainCode;
		public WebImage image;

        

        public WebItemImage(Infojet infojetContext, string itemNo, string webImageCode)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
			this.itemNo = itemNo;
			this.webImageCode = webImageCode;

			getFromDatabase();
		}

        public WebItemImage(Infojet infojetContext, SqlDataReader dataReader)
		{
            this.infojetContext = infojetContext;
			readData(dataReader);
		}

		private void readData(SqlDataReader dataReader)
		{
			itemNo = dataReader.GetValue(0).ToString();
			webImageCode = dataReader.GetValue(1).ToString();
			type = dataReader.GetInt32(2);
			webSiteCode = dataReader.GetValue(3).ToString();
			webItemCampainCode = dataReader.GetValue(4).ToString();

            image = new WebImage(infojetContext, dataReader, true);
		}

 
		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT w.[Item No_], w.[Web Image Code], w.[Type], w.[Web Site Code], w.[Web Item Campain Code], i.[Code], i.[Description], i.[Filename], i.[Image Type], i.[Last Modified] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Image") + "] w, [" + infojetContext.systemDatabase.getTableName("Web Image") + "] i WHERE w.[Item No_] = @itemNo AND w.[Web Image Code] = @webImageCode AND w.[Web Image Code] = i.[Code]");
            databaseQuery.addStringParameter("itemNo", itemNo, 20);
            databaseQuery.addStringParameter("webImageCode", webImageCode, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{
				readData(dataReader);
			}

			dataReader.Close();
			
		}

		public string renderSwitchLink(Infojet infojetContext)
		{
            WebTemplate webTemplate = new WebTemplate(infojetContext.systemDatabase, infojetContext.webSite.code, infojetContext.webPage.webTemplateCode);
			return webTemplate.filename+"?pageCode="+infojetContext.webPage.code+"&category="+System.Web.HttpContext.Current.Request["category"]+"&itemNo="+this.itemNo+"&imageCode="+this.image.code;
		}

	}
}
