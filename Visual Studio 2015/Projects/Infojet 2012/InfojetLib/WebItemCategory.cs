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
        public bool protectedCategory;
        public bool showEvenIfProtected;
        public bool viewOnly;
        public bool requireItemTranslation;
	
        private Infojet infojetContext;

		public WebItemCategory(Infojet infojetContext, string code)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
            this.webSiteCode = infojetContext.webSite.code;
			this.code = code.ToUpper();

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
            if (this.code == null) this.code = "";
            this.code = this.code.ToUpper();

            getFromDatabase();
        }


		private void getFromDatabase()
		{
            code = System.Web.HttpUtility.UrlDecode(code);

            //DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Code], [Description], [Item List Web Page Code], [Item Info Web Page Code], [Protected], [Show Even If Protected], [View Only], (SELECT t.[Description] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category Translation") + "] t WHERE t.[Web Site Code] = c.[Web Site Code] AND t.[Web Item Category Code] = c.[Code] AND t.[Language Code] = @languageCode) as translation FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category") + "] c WHERE c.[Web Site Code] = @webSiteCode AND " + infojetContext.systemDatabase.getReplacementChars("LOWER([Code])") + " = @code");
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Code], [Description], [Item List Web Page Code], [Item Info Web Page Code], [Protected], [Show Even If Protected], [View Only], (SELECT t.[Description] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category Translation") + "] t WHERE t.[Web Site Code] = c.[Web Site Code] AND t.[Web Item Category Code] = c.[Code] AND t.[Language Code] = @languageCode) as translation, [Require Item Translation] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category") + "] c WHERE c.[Web Site Code] = @webSiteCode AND [Code] = @code");
            databaseQuery.addStringParameter("code", code.ToUpper(), 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{

				webSiteCode = dataReader.GetValue(0).ToString();
				code = dataReader.GetValue(1).ToString();
				description = dataReader.GetValue(2).ToString();
				itemListWebPageCode = dataReader.GetValue(3).ToString();
				itemInfoWebPageCode = dataReader.GetValue(4).ToString();

                protectedCategory = false;
                if (dataReader.GetValue(5).ToString() == "1") protectedCategory = true;

                showEvenIfProtected = false;
                if (dataReader.GetValue(6).ToString() == "1") showEvenIfProtected = true;

                viewOnly = false;
                if (dataReader.GetValue(7).ToString() == "1") viewOnly = true;

                if (!dataReader.IsDBNull(8)) this.description = dataReader.GetValue(8).ToString();

                requireItemTranslation = false;
                if (dataReader.GetValue(9).ToString() == "1") requireItemTranslation = true;

			}

			dataReader.Close();
			
		}

		public DataSet getSubCategories()
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Web Item Category Code], [Type], [Code], [Item Info Web Page Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category Member") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Item Category Code] = @code AND [Type] = 0 ORDER BY [Sort Order]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("code", code.ToUpper(), 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);

		}

		public DataSet getItemsAndVariants()
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT i.[No_] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category Member") + "] c LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model Variant") + "] v ON v.[Web Model No_] = c.[Code], [" + infojetContext.systemDatabase.getTableName("Item") + "] i WHERE c.[Web Site Code] = @webSiteCode AND c.[Web Item Category Code] = @code AND c.[Type] > 0 AND (i.[No_] = c.[Code] OR i.[No_] = v.[Item No_])");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("code", code.ToUpper(), 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);

		}

        public DataSet getItems()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT i.[No_], i.[Description], i.[Description 2], i.[Unit Price], i.[Sales Unit of Measure], i.[Manufacturer Code], i.[Lead Time Calculation], i.[Unit List Price], i.[Item Disc_ Group], c.[Item Info Web Page Code], c.[Type], c.[Code], c.[Sort Order], t.[Description], t.[Description 2], mt.[Description], mt.[Description 2], m.[Description], m.[Description 2], mv.[Item No_] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category Member") + "] c LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item") + "] i ON c.[Code] = i.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item Translation") + "] t ON t.[Item No_] = c.[Code] AND t.[Language Code] = @languageCode LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model") + "] m ON m.[No_] = c.[Code] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model Variant") + "] mv ON mv.[Web Model No_] = c.[Code] AND mv.[Primary] = 1 LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model Translation") + "] mt ON mt.[Web Model No_] = c.[Code] AND mt.[Language Code] = '" + infojetContext.languageCode + "' WHERE c.[Web Site Code] = @webSiteCode AND c.[Web Item Category Code] = @code AND c.[Type] > 0 ORDER BY [Sort Order]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("code", code.ToUpper(), 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            
            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                int type = int.Parse(dataSet.Tables[0].Rows[i].ItemArray.GetValue(10).ToString());
                if (type == 2)
                {
                    //WebModel webModel = new WebModel(infojetContext, dataSet.Tables[0].Rows[i].ItemArray.GetValue(11).ToString());
                    //dataSet.Tables[0].Rows[i][0] = webModel.getDefaultItemNo();
                    //dataSet.Tables[0].Rows[i][1] = webModel.description;
                    //dataSet.Tables[0].Rows[i][2] = webModel.description2;

                    dataSet.Tables[0].Rows[i][0] = dataSet.Tables[0].Rows[i][19];
                    dataSet.Tables[0].Rows[i][1] = dataSet.Tables[0].Rows[i][17];
                    dataSet.Tables[0].Rows[i][2] = dataSet.Tables[0].Rows[i][18];

                }

                i++;
            }

            

            return (dataSet);

        }

 
	
		public bool hasSubCategories()
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT TOP 1 * FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category Member") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Item Category Code] = @code AND [Type] = 0 ORDER BY [Sort Order]");
            databaseQuery.addStringParameter("code", code.ToUpper(), 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
			bool hasChilds = false;

			if (dataReader.Read()) hasChilds = true;
			dataReader.Close();
				
			return hasChilds;

		}

		public bool hasMember(string webItemCategoryCode)
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category Member") + "] WHERE [Web Site Code] = @webSiteCode AND [Web Item Category Code] = @code AND [Type] = 0 AND [Code] = @webItemCategoryCode");
            databaseQuery.addStringParameter("code", code.ToUpper(), 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("webItemCategoryCode", webItemCategoryCode.ToUpper(), 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
			bool hasThisMember = false;

			if (dataReader.Read()) hasThisMember = true;
			dataReader.Close();
				
			return hasThisMember;

		}


		public WebItemCategoryTranslation getTranslation()
		{
            return new WebItemCategoryTranslation(infojetContext, this.webSiteCode, this.code, infojetContext.languageCode);
		}



		public string getDescription()
		{
            WebItemCategoryTexts webItemCategoryTexts = new WebItemCategoryTexts(infojetContext.systemDatabase);

            DataSet dataSet = webItemCategoryTexts.getCategoryTexts(this.webSiteCode, this.code, infojetContext.languageCode);
            int i = 0;
            string description = "";
            while (i < dataSet.Tables[0].Rows.Count)
            {
                description = description + " " + dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString().Replace("<br>", "<br/>");

                i++;
            }

            return description;
		}

        public NavigationItemCollection getProductCategoryTree()
        {
            return getProductCategoryTree(false);
        }

        public NavigationItemCollection getProductCategoryTree(bool overrideSecurityCheck)
        {
            string authenticatedUser = "";
            if (infojetContext.userSession != null)
            {
                authenticatedUser = infojetContext.userSession.webUserAccount.no;
            }

            NavigationItemCollection navigationItemCollection = null;

            bool securityCheck = true;
            if (!overrideSecurityCheck)
            {
                securityCheck = checkSecurity();
                if ((!securityCheck) && (!this.showEvenIfProtected)) return navigationItemCollection;
                if ((infojetContext.userSession != null) && (!securityCheck)) return navigationItemCollection;

                //Caching
                /*
                if (System.Web.HttpContext.Current.Application["categoryTree_" + infojetContext.webSite.code + "_" + code + "_" +infojetContext.languageCode + "_" + authenticatedUser] != null)
                {
                    navigationItemCollection = (NavigationItemCollection)System.Web.HttpContext.Current.Application["categoryTree_" + infojetContext.webSite.code + "_" + code + "_" + infojetContext.languageCode + "_" + authenticatedUser];
                    if (navigationItemCollection.createdDateTime > infojetContext.webSite.propertiesUpdated)
                    {
                        if (System.Web.HttpContext.Current.Session["categoryTree_" + infojetContext.webSite.code + "_" + code + "_" + infojetContext.languageCode + "_" + authenticatedUser] == null)
                        {
                            if (System.Web.HttpContext.Current.Request["category"] == null) navigationItemCollection.clearSelectedCategory();
                            System.Web.HttpContext.Current.Session.Add("categoryTree_" + infojetContext.webSite.code + "_" + code + "_" + infojetContext.languageCode + "_" + authenticatedUser, navigationItemCollection);
                        }
                        navigationItemCollection.setSelectedCategory(System.Web.HttpContext.Current.Request["category"]);
                        return navigationItemCollection;
                    }
                    navigationItemCollection = new NavigationItemCollection();
                }
                */

                
                if (System.Web.HttpContext.Current.Session["categoryTree_" + infojetContext.webSite.code + "_" + code + "_" + infojetContext.languageCode + "_" + authenticatedUser] == null)
                {
                    CacheableNavigationItemCollection cacheableNavigationItemCollection = (CacheableNavigationItemCollection)WebApplicationCache.getCashedObject(infojetContext, "categoryTree_" + infojetContext.webSite.code + "_" + code + "_" + infojetContext.languageCode + "_" + authenticatedUser);
                    if (cacheableNavigationItemCollection != null)
                    {
                        if (cacheableNavigationItemCollection.createdDateTime > infojetContext.webSite.propertiesUpdated)
                        {
                            navigationItemCollection = cacheableNavigationItemCollection.getNavigationItemCollection(infojetContext, null);

                            if (System.Web.HttpContext.Current.Session["categoryTree_" + infojetContext.webSite.code + "_" + code + "_" + infojetContext.languageCode + "_" + authenticatedUser] == null)
                            {
                                if (System.Web.HttpContext.Current.Request["category"] == null) navigationItemCollection.clearSelectedCategory();
                                System.Web.HttpContext.Current.Session.Add("categoryTree_" + infojetContext.webSite.code + "_" + code + "_" + infojetContext.languageCode + "_" + authenticatedUser, navigationItemCollection);
                            }
                            navigationItemCollection.setSelectedCategory(System.Web.HttpContext.Current.Request["category"]);
                            return navigationItemCollection;
                        }
                    }
                }
                else
                {
                    navigationItemCollection = (NavigationItemCollection)System.Web.HttpContext.Current.Session["categoryTree_" + infojetContext.webSite.code + "_" + code + "_" + infojetContext.languageCode + "_" + authenticatedUser];
                    if (System.Web.HttpContext.Current.Session["categoryTree_" + infojetContext.webSite.code + "_" + code + "_" + infojetContext.languageCode + "_" + authenticatedUser] == null)
                    {
                        if (System.Web.HttpContext.Current.Request["category"] == null) navigationItemCollection.clearSelectedCategory();
                        System.Web.HttpContext.Current.Session.Add("categoryTree_" + infojetContext.webSite.code + "_" + code + "_" + infojetContext.languageCode + "_" + authenticatedUser, navigationItemCollection);
                    }
                    navigationItemCollection.setSelectedCategory(System.Web.HttpContext.Current.Request["category"]);
                    return navigationItemCollection;
                }
                

               
                /*
                if (System.Web.HttpContext.Current.Session["categoryTree_" + code + "_" + infojetContext.languageCode+"_"+authenticatedUser] != null)
                {
                    navigationItemCollection = (NavigationItemCollection)System.Web.HttpContext.Current.Session["categoryTree_" + code + "_" + infojetContext.languageCode + "_" + authenticatedUser];
                    navigationItemCollection.setSelectedCategory(System.Web.HttpContext.Current.Request["category"]);
                    return navigationItemCollection;
                }
                */

            }
            navigationItemCollection = new NavigationItemCollection();

            DataSet categoryMemberDataSet = getSubCategories();

            int i = 0;
            while (i < categoryMemberDataSet.Tables[0].Rows.Count)
            {
                WebItemCategory webItemCategory = new WebItemCategory(infojetContext, categoryMemberDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString());

                bool include = true;

                if (!overrideSecurityCheck)
                {
                    securityCheck = webItemCategory.checkSecurity();
                    if ((!securityCheck) && (!webItemCategory.showEvenIfProtected)) include = false;
                    if ((infojetContext.userSession != null) && (!securityCheck)) include = false;
                }

                if (include)
                {

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
                        Link link = targetWebPage.getUrlLink();
                        link.setCategory(webItemCategory.code, webItemCategoryTranslation.description.Replace("&", "&amp;"));

                        NavigationItem productItem = new NavigationItem();
                        productItem.code = webItemCategory.code;
                        productItem.description = webItemCategory.description;
                        productItem.text = webItemCategoryTranslation.description.Replace("&", "&amp;");
                        //productItem.link = targetWebPage.getUrl() + "&category=" + webItemCategory.code.ToLower();
                        productItem.link = link.toUrl();
                        productItem.webImage = webItemCategoryTranslation.webImage;
                        if (System.Web.HttpContext.Current.Request["category"] != null)
                        {
                            if (System.Web.HttpContext.Current.Request["category"].ToUpper() == webItemCategory.code) productItem.selected = true;
                        }

                        productItem.subNavigationItems = getProductCategorySubTree(infojetContext, webItemCategory, productItem, overrideSecurityCheck);

                        navigationItemCollection.Add(productItem);
                    }
                }

                i++;
            }

            //if (!overrideSecurityCheck) System.Web.HttpContext.Current.Application.Add("categoryTree_" + infojetContext.webSite.code + "_" + code + "_" + infojetContext.languageCode + "_" + authenticatedUser, navigationItemCollection);
            
            if (!overrideSecurityCheck)
            {
                CacheableNavigationItemCollection cacheableNavigationItemCollection = navigationItemCollection.getCacheableNavigationItemCollection(null);

                WebApplicationCache.cacheObject(infojetContext, "categoryTree_" + infojetContext.webSite.code + "_" + code + "_" + infojetContext.languageCode + "_" + authenticatedUser, cacheableNavigationItemCollection);
            }

            return navigationItemCollection;

        }

        private NavigationItemCollection getProductCategorySubTree(Infojet infojetContext, WebItemCategory parentItemCategory, NavigationItem parentProductItem, bool overrideSecurityCheck)
        {
            NavigationItemCollection navigationItemCollection = new NavigationItemCollection();

            DataSet categoryMemberDataSet = parentItemCategory.getSubCategories();

            int i = 0;
            while (i < categoryMemberDataSet.Tables[0].Rows.Count)
            {
                WebItemCategory webItemCategory = new WebItemCategory(infojetContext, categoryMemberDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString());

                bool securityCheck = true;
                bool include = true;
                if (!overrideSecurityCheck)
                {
                    securityCheck = webItemCategory.checkSecurity();
                    if ((!securityCheck) && (!webItemCategory.showEvenIfProtected)) include = false;
                    if ((infojetContext.userSession != null) && (!securityCheck)) include = false;
                }

                if (include)
                {

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
                        Link link = targetWebPage.getUrlLink();
                        link.setCategory(webItemCategory.code, webItemCategoryTranslation.description.Replace("&", "&amp;"));

                        NavigationItem productItem = new NavigationItem();
                        productItem.code = webItemCategory.code;
                        productItem.description = webItemCategory.description;
                        productItem.text = webItemCategoryTranslation.description.Replace("&", "&amp;");
                        //productItem.link = targetWebPage.getUrl() + "&category=" + webItemCategory.code.ToLower();
                        productItem.link = link.toUrl();
                        productItem.parentItem = parentProductItem;
                        productItem.webImage = webItemCategoryTranslation.webImage;

                        if (System.Web.HttpContext.Current.Request["category"] != null)
                        {
                            if (System.Web.HttpContext.Current.Request["category"].ToUpper() == webItemCategory.code) productItem.selected = true;
                        }
                        productItem.subNavigationItems = getProductCategorySubTree(infojetContext, webItemCategory, productItem, overrideSecurityCheck);

                        navigationItemCollection.Add(productItem);

                    }
                }
                i++;
            }

            return navigationItemCollection;

        }

        public bool checkSecurity()
        {
            int permission = checkSecurityPermission();
            if (permission > 0) return true;
            return false;
        }

        public int checkSecurityPermission()
        {
            if ((this.protectedCategory == false) && (viewOnly == false)) return 1;

            if (infojetContext.userSession != null)
            {
                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT c.[Web User Group Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category User Group") + "] c, [" + infojetContext.systemDatabase.getTableName("Web User Account Group") + "] g WHERE c.[Web Site Code] = @webSiteCode AND c.[Web Item Category Code] = @code AND c.[Web User Group Code] = g.[Web User Group Code] AND g.[No_] = @webUserAccountNo");
                databaseQuery.addStringParameter("code", code.ToUpper(), 20);
                databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
                databaseQuery.addStringParameter("webUserAccountNo", infojetContext.userSession.webUserAccount.no, 20);


                SqlDataReader dataReader = databaseQuery.executeQuery();
                int permission = 0;

                if (dataReader.Read())
                {
                    permission = 1;
                }
                dataReader.Close();

                if (permission > 0) return permission;
            }

            if (this.protectedCategory == true) return 0;
            if (this.viewOnly == true) return 2;

            return 0;
        }

        public void doSecurityCheck()
        {
            bool securityCheck = checkSecurity();
            if ((!securityCheck) && (infojetContext.userSession == null)) infojetContext.redirectToSignInPage(infojetContext.webPage);
            if ((!securityCheck) && (infojetContext.userSession != null)) infojetContext.redirect(infojetContext.webSite.getStartPageUrl());
        }

	}
}