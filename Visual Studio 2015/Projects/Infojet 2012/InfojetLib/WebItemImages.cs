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
        private Infojet infojetContext;

        public WebItemImages(Infojet infojetContext)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
		}

        public WebItemImageCollection getWebItemImages(string itemNo, int type, string webSiteCode)
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT w.[Item No_], w.[Web Image Code], w.[Type], w.[Web Site Code], w.[Web Item Campain Code], i.[Code], i.[Description], i.[Filename], i.[Image Type], i.[Last Modified Date], i.[Last Modified Time], i.[Public Description] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Image") + "] w, [" + infojetContext.systemDatabase.getTableName("Web Image") + "] i WHERE w.[Item No_] = @itemNo AND w.[Type] = @type AND w.[Web Site Code] = @webSiteCode AND w.[Web Image Code] = i.[Code]");
            databaseQuery.addStringParameter("itemNo", itemNo, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addIntParameter("type", type);


            SqlDataReader dataReader = databaseQuery.executeQuery();

            WebItemImageCollection webItemImageCollection = new WebItemImageCollection();

			while (dataReader.Read())
			{
                WebItemImage webItemImage = new WebItemImage(infojetContext, dataReader);
                webItemImageCollection.Add(webItemImage);
			}
			dataReader.Close();


            return webItemImageCollection;
		}

        public Hashtable getWebItemImages(int type, string webSiteCode)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT w.[Item No_], w.[Web Image Code], w.[Type], w.[Web Site Code], w.[Web Item Campain Code], i.[Code], i.[Description], i.[Filename], i.[Image Type], i.[Last Modified Date], i.[Last Modified Time], i.[Public Description] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Image") + "] w, [" + infojetContext.systemDatabase.getTableName("Web Image") + "] i WHERE w.[Type] = @type AND w.[Web Site Code] = @webSiteCode AND w.[Web Image Code] = i.[Code]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addIntParameter("type", type);


            SqlDataReader dataReader = databaseQuery.executeQuery();

            Hashtable webItemImageTable = new Hashtable();

            while (dataReader.Read())
            {
                WebItemImage webItemImage = new WebItemImage(infojetContext, dataReader);
                if (webItemImageTable[webItemImage.itemNo] == null)
                {
                    ProductImage productImage = new ProductImage(webItemImage, "");
                    webItemImageTable.Add(webItemImage.itemNo, productImage);
                }
            }
            dataReader.Close();


            return webItemImageTable;
        }


        public WebItemImageCollection getItemCampainImages(string itemNo, string webSiteCode, string webItemCampainCode)
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT w.[Item No_], w.[Web Image Code], w.[Type], w.[Web Site Code], w.[Web Item Campain Code], i.[Code], i.[Description], i.[Filename], i.[Image Type], i.[Last Modified Date], i.[Last Modified Time], i.[Public Description] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Image") + "] w, [" + infojetContext.systemDatabase.getTableName("Web Image") + "] i WHERE w.[Item No_] = @itemNo AND w.[Type] = '2' AND w.[Web Site Code] = @webSiteCode AND (w.[Web Item Campain Code] = @webItemCampainCode OR w.[Web Item Campain Code] = '') AND w.[Web Image Code] = i.[Code]");
            databaseQuery.addStringParameter("itemNo", itemNo, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("webItemCampainCode", webItemCampainCode, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();

            WebItemImageCollection webItemImageCollection = new WebItemImageCollection();

			while (dataReader.Read())
			{
                WebItemImage webItemImage = new WebItemImage(infojetContext, dataReader);
                webItemImageCollection.Add(webItemImage);
			}
			dataReader.Close();

            return webItemImageCollection;
		}

		public WebItemImage getItemProductImage(string itemNo, string webSiteCode)
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT w.[Item No_], w.[Web Image Code], w.[Type], w.[Web Site Code], w.[Web Item Campain Code], i.[Code], i.[Description], i.[Filename], i.[Image Type], i.[Last Modified Date], i.[Last Modified Time], i.[Public Description] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Image") + "] w, [" + infojetContext.systemDatabase.getTableName("Web Image") + "] i WHERE w.[Item No_] = @itemNo AND w.[Type] = '0' AND w.[Web Site Code] = @webSiteCode AND w.[Web Image Code] = i.[Code]");
            databaseQuery.addStringParameter("itemNo", itemNo, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
			WebItemImage webItemImage = null;

			if (dataReader.Read())
			{
                webItemImage = new WebItemImage(infojetContext, dataReader);
			}

			dataReader.Close();

			return webItemImage;
		}

		public WebItemImage getItemEnvironmentImage(string itemNo, string webSiteCode)
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT w.[Item No_], w.[Web Image Code], w.[Type], w.[Web Site Code], w.[Web Item Campain Code], i.[Code], i.[Description], i.[Filename], i.[Image Type], i.[Last Modified Date], i.[Last Modified Time], i.[Public Description] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Image") + "] w, [" + infojetContext.systemDatabase.getTableName("Web Image") + "] i WHERE w.[Item No_] = @itemNo AND w.[Type] = '1' AND w.[Web Site Code] = @webSiteCode AND w.[Web Image Code] = i.[Code]");
            databaseQuery.addStringParameter("itemNo", itemNo, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
			WebItemImage webItemImage = null;

			if (dataReader.Read())
			{
                webItemImage = new WebItemImage(infojetContext, dataReader);
			}

			dataReader.Close();

			return webItemImage;
		}
	}
}
