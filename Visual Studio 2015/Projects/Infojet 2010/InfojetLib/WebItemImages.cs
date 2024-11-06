using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebPageLines.
	/// </summary>
	public class WebItemImages
	{
		private Database database;

		public WebItemImages(Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
		}

        public WebItemImageCollection getWebItemImages(string itemNo, int type, string webSiteCode)
		{
			SqlDataReader dataReader = database.query("SELECT w.[Item No_], w.[Web Image Code], w.[Type], w.[Web Site Code], w.[Web Item Campain Code], i.[Code], i.[Description], i.[Filename], i.[Image Type], i.[Last Modified Date], i.[Last Modified Time], i.[Public Description] FROM ["+database.getTableName("Web Item Image")+"] w, ["+database.getTableName("Web Image")+"] i WHERE w.[Item No_] = '"+itemNo+"' AND w.[Type] = '"+type+"' AND w.[Web Site Code] = '"+webSiteCode+"' AND w.[Web Image Code] = i.[Code]");

            WebItemImageCollection webItemImageCollection = new WebItemImageCollection();

			while (dataReader.Read())
			{
				WebItemImage webItemImage = new WebItemImage(database, dataReader);
                webItemImageCollection.Add(webItemImage);
			}
			dataReader.Close();


            return webItemImageCollection;
		}


        public WebItemImageCollection getItemCampainImages(string itemNo, string webSiteCode, string webItemCampainCode)
		{
			SqlDataReader dataReader = database.query("SELECT w.[Item No_], w.[Web Image Code], w.[Type], w.[Web Site Code], w.[Web Item Campain Code], i.[Code], i.[Description], i.[Filename], i.[Image Type], i.[Last Modified Date], i.[Last Modified Time], i.[Public Description] FROM ["+database.getTableName("Web Item Image")+"] w, ["+database.getTableName("Web Image")+"] i WHERE w.[Item No_] = '"+itemNo+"' AND w.[Type] = '2' AND w.[Web Site Code] = '"+webSiteCode+"' AND (w.[Web Item Campain Code] = '"+webItemCampainCode+"' OR w.[Web Item Campain Code] = '') AND w.[Web Image Code] = i.[Code]");

            WebItemImageCollection webItemImageCollection = new WebItemImageCollection();

			while (dataReader.Read())
			{
				WebItemImage webItemImage = new WebItemImage(database, dataReader);
                webItemImageCollection.Add(webItemImage);
			}
			dataReader.Close();

            return webItemImageCollection;
		}

		public WebItemImage getItemProductImage(string itemNo, string webSiteCode)
		{
			SqlDataReader dataReader = database.query("SELECT w.[Item No_], w.[Web Image Code], w.[Type], w.[Web Site Code], w.[Web Item Campain Code], i.[Code], i.[Description], i.[Filename], i.[Image Type], i.[Last Modified Date], i.[Last Modified Time], i.[Public Description] FROM ["+database.getTableName("Web Item Image")+"] w, ["+database.getTableName("Web Image")+"] i WHERE w.[Item No_] = '"+itemNo+"' AND w.[Type] = '0' AND w.[Web Site Code] = '"+webSiteCode+"' AND w.[Web Image Code] = i.[Code]");

			WebItemImage webItemImage = null;

			if (dataReader.Read())
			{
				webItemImage = new WebItemImage(database, dataReader);
			}

			dataReader.Close();

			return webItemImage;
		}

		public WebItemImage getItemEnvironmentImage(string itemNo, string webSiteCode)
		{
			SqlDataReader dataReader = database.query("SELECT w.[Item No_], w.[Web Image Code], w.[Type], w.[Web Site Code], w.[Web Item Campain Code], i.[Code], i.[Description], i.[Filename], i.[Image Type], i.[Last Modified Date], i.[Last Modified Time], i.[Public Description] FROM ["+database.getTableName("Web Item Image")+"] w, ["+database.getTableName("Web Image")+"] i WHERE w.[Item No_] = '"+itemNo+"' AND w.[Type] = '1' AND w.[Web Site Code] = '"+webSiteCode+"' AND w.[Web Image Code] = i.[Code]");

			WebItemImage webItemImage = null;

			if (dataReader.Read())
			{
				webItemImage = new WebItemImage(database, dataReader);
			}

			dataReader.Close();

			return webItemImage;
		}
	}
}
