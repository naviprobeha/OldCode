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
		private Database database;

		public string itemNo;
		public string webImageCode;
		public int type;
		public string webSiteCode;
		public string webItemCampainCode;
		public WebImage image;


		public WebItemImage(Database database, string itemNo, string webImageCode)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			this.itemNo = itemNo;
			this.webImageCode = webImageCode;

			getFromDatabase();
		}

		public WebItemImage(Database database, SqlDataReader dataReader)
		{
			this.database = database;
			readData(dataReader);
		}

		private void readData(SqlDataReader dataReader)
		{
			itemNo = dataReader.GetValue(0).ToString();
			webImageCode = dataReader.GetValue(1).ToString();
			type = dataReader.GetInt32(2);
			webSiteCode = dataReader.GetValue(3).ToString();
			webItemCampainCode = dataReader.GetValue(4).ToString();	
			
			image = new WebImage(database, dataReader, true);
		}

 
		private void getFromDatabase()
		{

			SqlDataReader dataReader = database.query("SELECT w.[Item No_], w.[Web Image Code], w.[Type], w.[Web Site Code], w.[Web Item Campain Code], i.[Code], i.[Description], i.[Filename], i.[Image Type], i.[Last Modified] FROM ["+database.getTableName("Web Item Image")+"] w, ["+database.getTableName("Web Image")+"] i WHERE w.[Item No_] = '"+this.itemNo+"' AND w.[Web Image Code] = '"+this.webImageCode+"' AND w.[Web Image Code] = i.[Code]");
			if (dataReader.Read())
			{
				readData(dataReader);
			}

			dataReader.Close();
			
		}

		public string renderSwitchLink(Infojet infojetContext)
		{
			WebTemplate webTemplate = new WebTemplate(database, infojetContext.webSite.code, infojetContext.webPage.webTemplateCode);
			return webTemplate.filename+"?pageCode="+infojetContext.webPage.code+"&category="+System.Web.HttpContext.Current.Request["category"]+"&itemNo="+this.itemNo+"&imageCode="+this.image.code;
		}

	}
}
