using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebSite.
	/// </summary>
	public class WebItemCategory
	{
		public string webSiteCode;
		public string code;
		public string description;
		public string itemListWebPageCode;
		public string itemInfoWebPageCode;
	
        private Infojet infojetContext;

		public WebItemCategory(Infojet infojetContext, string code)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
            this.webSiteCode = infojetContext.webSite.code;
			this.code = code;

			getFromDatabase();
		}

        public WebItemCategory(Infojet infojetContext)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;
            this.webSiteCode = infojetContext.webSite.code;
            this.code = System.Web.HttpContext.Current.Request["category"];

            getFromDatabase();
        }


		private void getFromDatabase()
		{

			SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT [Web Site Code], [Code], [Description], [Item List Web Page Code], [Item Info Web Page Code] FROM ["+infojetContext.systemDatabase.getTableName("Web Item Category")+"] WHERE [Web Site Code] = '"+this.webSiteCode+"' AND [Code] = '"+this.code+"'");
			if (dataReader.Read())
			{

				webSiteCode = dataReader.GetValue(0).ToString();
				code = dataReader.GetValue(1).ToString();
				description = dataReader.GetValue(2).ToString();
				itemListWebPageCode = dataReader.GetValue(3).ToString();
				itemInfoWebPageCode = dataReader.GetValue(4).ToString();

			}

			dataReader.Close();
			
		}

		public DataSet getSubCategories()
		{
            SqlDataAdapter sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT [Web Site Code], [Web Item Category Code], [Type], [Code], [Item Info Web Page Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category Member") + "] WHERE [Web Site Code] = '" + this.webSiteCode + "' AND [Web Item Category Code] = '" + this.code + "' AND [Type] = 0 ORDER BY [Sort Order]");
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);

		}

		public DataSet getItems()
		{
			// Used to fetch items and convert them to the class Item.
            SqlDataAdapter sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT i.[No_], i.[Description], i.[Description 2], i.[Unit Price], i.[Sales Unit of Measure], i.[Manufacturer Code], i.[Lead Time Calculation], i.[Unit List Price], i.[Item Disc_ Group], c.[Item Info Web Page Code], c.[Type], c.[Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category Member") + "] c LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item") + "] i ON c.[Code] = i.[No_] WHERE c.[Web Site Code] = '" + this.webSiteCode + "' AND c.[Web Item Category Code] = '" + this.code + "' AND c.[Type] > 0 ORDER BY [Sort Order]");
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                int type = int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(10).ToString());
                if (type == 2)
                {
                    WebModel webModel = new WebModel(infojetContext, dataSet.Tables[0].Rows[i].ItemArray.GetValue(11).ToString());
                    dataSet.Tables[0].Rows[i][0] = webModel.getDefaultItemNo();
                    dataSet.Tables[0].Rows[i][1] = webModel.description;
                    dataSet.Tables[0].Rows[i][2] = webModel.description2;

                }

                i++;
            }


			return(dataSet);

		}

 
	
		public bool hasSubCategories()
		{
            SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT TOP 1 * FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category Member") + "] WHERE [Web Site Code] = '" + this.webSiteCode + "' AND [Web Item Category Code] = '" + this.code + "' AND [Type] = 0 ORDER BY [Sort Order]");

			bool hasChilds = false;

			if (dataReader.Read()) hasChilds = true;
			dataReader.Close();
				
			return hasChilds;

		}

		public bool hasMember(string webItemCategoryCode)
		{
            SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT [Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category Member") + "] WHERE [Web Site Code] = '" + this.webSiteCode + "' AND [Web Item Category Code] = '" + this.code + "' AND [Type] = 0 AND [Code] = '" + webItemCategoryCode + "'");

			bool hasThisMember = false;

			if (dataReader.Read()) hasThisMember = true;
			dataReader.Close();
				
			return hasThisMember;

		}


		public WebItemCategoryTranslation getTranslation()
		{
            return new WebItemCategoryTranslation(infojetContext.systemDatabase, this.webSiteCode, this.code, infojetContext.languageCode);
		}



		public string getDescription()
		{
            WebItemCategoryTexts webItemCategoryTexts = new WebItemCategoryTexts(infojetContext.systemDatabase);

            DataSet dataSet = webItemCategoryTexts.getCategoryTexts(this.webSiteCode, this.code, infojetContext.languageCode);
            int i = 0;
            string description = "";
            while (i < dataSet.Tables[0].Rows.Count)
            {
                description = description + " " + dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                i++;
            }

            return description;
		}

        public NavigationItemCollection getProductCategoryTree()
        {
            NavigationItemCollection navigationItemCollection = new NavigationItemCollection();

            DataSet categoryMemberDataSet = getSubCategories();

            int i = 0;
            while (i < categoryMemberDataSet.Tables[0].Rows.Count)
            {
                WebItemCategory webItemCategory = new WebItemCategory(infojetContext, categoryMemberDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString());
                WebItemCategoryTranslation webItemCategoryTranslation = webItemCategory.getTranslation();

                WebPage targetWebPage;
                if (webItemCategory.itemListWebPageCode != null)
                {
                    targetWebPage = new WebPage(infojetContext, infojetContext.webSite.code, webItemCategory.itemListWebPageCode);
                }
                else
                {
                    targetWebPage = infojetContext.webPage;
                }

                if ((webItemCategoryTranslation.description != null) && (webItemCategoryTranslation.description != ""))
                {
                    NavigationItem productItem = new NavigationItem();
                    productItem.code = webItemCategory.code;
                    productItem.text = webItemCategoryTranslation.description;
                    productItem.link = targetWebPage.getUrl() + "&category=" + webItemCategory.code;

                    if (System.Web.HttpContext.Current.Request["category"] == webItemCategory.code) productItem.selected = true;

                    productItem.subNavigationItems = getProductCategorySubTree(infojetContext, webItemCategory, productItem);

                    navigationItemCollection.Add(productItem);
                }

                i++;
            }

            return navigationItemCollection;

        }

        private NavigationItemCollection getProductCategorySubTree(Infojet infojetContext, WebItemCategory parentItemCategory, NavigationItem parentProductItem)
        {
            NavigationItemCollection navigationItemCollection = new NavigationItemCollection();

            DataSet categoryMemberDataSet = parentItemCategory.getSubCategories();

            int i = 0;
            while (i < categoryMemberDataSet.Tables[0].Rows.Count)
            {
                WebItemCategory webItemCategory = new WebItemCategory(infojetContext, categoryMemberDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString());
                WebItemCategoryTranslation webItemCategoryTranslation = webItemCategory.getTranslation();

                WebPage targetWebPage;
                if (webItemCategory.itemListWebPageCode != null)
                {
                    targetWebPage = new WebPage(infojetContext, infojetContext.webSite.code, webItemCategory.itemListWebPageCode);
                }
                else
                {
                    targetWebPage = infojetContext.webPage;
                }

                if ((webItemCategoryTranslation.description != null) && (webItemCategoryTranslation.description != ""))
                {

                    NavigationItem productItem = new NavigationItem();
                    productItem.code = webItemCategory.code;
                    productItem.text = webItemCategoryTranslation.description;
                    productItem.link = targetWebPage.getUrl() + "&category=" + webItemCategory.code;
                    productItem.parentItem = parentProductItem;

                    if (System.Web.HttpContext.Current.Request["category"] == webItemCategory.code) productItem.selected = true;

                    productItem.subNavigationItems = getProductCategorySubTree(infojetContext, webItemCategory, productItem);

                    navigationItemCollection.Add(productItem);

                }
                i++;
            }

            return navigationItemCollection;

        }
	}
}