using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebSite.
	/// </summary>
	public class WebMenu
	{
		public string webSiteCode;
		public string code;
		public string description;
		public int type;
	
		private Database database;

		public WebMenu(Infojet infojetContext, string code)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = infojetContext.systemDatabase;
			this.webSiteCode = infojetContext.webSite.code;
			this.code = code;

			getFromDatabase();
		}


		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Site Code], [Code], [Description], [Type] FROM [" + database.getTableName("Web Menu") + "] WHERE [Web Site Code] = @webSiteCode AND [Code] = @code");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("code", code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{

				webSiteCode = dataReader.GetValue(0).ToString();
				code = dataReader.GetValue(1).ToString();
				description = dataReader.GetValue(2).ToString();
				type = dataReader.GetInt32(3);

			}

			dataReader.Close();
			
		}


        public NavigationItemCollection getMenuItems(Infojet infojetContext)
        {
            if (infojetContext.userSession != null)
            {
                return getMenuItems(infojetContext, infojetContext.userSession.webUserAccount);
            }
            return getMenuItems(infojetContext, null);
        }

        public NavigationItemCollection getMenuItems(Infojet infojetContext, WebUserAccount webUserAccount)
        {
            return getMenuItems(infojetContext, webUserAccount, infojetContext.languageCode);
        }

        public NavigationItemCollection getMenuItems(Infojet infojetContext, WebUserAccount webUserAccount, string languageCode)
        {

            NavigationItemCollection navigationItemCollection = new NavigationItemCollection();


            WebPages webPages = new WebPages(database);

            DataSet topPageDataSet = null;

            //Main menu
            if (type == 0)
            {
                if (webUserAccount != null)
                {
                    topPageDataSet = webPages.getTopPages(infojetContext.webSite.code, languageCode, webUserAccount.no);
                }
                else
                {
                    topPageDataSet = webPages.getTopPages(infojetContext.webSite.code, languageCode);
                }

            }

            //Sub menu
            if (type == 1)
            {
                if (webUserAccount != null)
                {
                    string parentPageCode = "";
                    if (infojetContext.webPage != null) parentPageCode = infojetContext.webPage.parentWebPageCode;

                    topPageDataSet = webPages.getChildPages(infojetContext.webSite.code, parentPageCode, languageCode, webUserAccount.no);
                }
                else
                {
                    string parentPageCode = "";
                    if (infojetContext.webPage != null) parentPageCode = infojetContext.webPage.parentWebPageCode;

                    topPageDataSet = webPages.getChildPages(infojetContext.webSite.code, parentPageCode, languageCode);
                }
            }

            //Specific menu
            if (type == 2)
            {

                if (webUserAccount != null)
                {
                    topPageDataSet = this.getSpecificMenuPages(infojetContext.webSite.code, languageCode, webUserAccount.no);
                }
                else
                {
                    topPageDataSet = this.getSpecificMenuPages(infojetContext.webSite.code, languageCode);
                }
            }


            int i = 0;
            while (i < topPageDataSet.Tables[0].Rows.Count)
            {
                NavigationItem topMenuItem = new NavigationItem();

                topMenuItem.code = topPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                topMenuItem.text = topPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString().Replace("&", "&amp;");

                Link link = new Link(infojetContext, topPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), topPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString().Replace("&", "&amp;"), topPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
                topMenuItem.link = link.toUrl();

                topMenuItem.parentItem = null;
                topMenuItem.windowMode = int.Parse(topPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString());

                if (infojetContext.webPage != null)
                {
                    if (infojetContext.webPage.code == topPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString()) topMenuItem.selected = true;
                    if (infojetContext.webPage.code == topMenuItem.code) topMenuItem.selected = true;
                }

                if (type != 2) topMenuItem.subNavigationItems = this.getSubMenuItems(infojetContext, webUserAccount, topPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), topMenuItem, languageCode);

                topMenuItem.helpText = topPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString();

                navigationItemCollection.Add(topMenuItem);
                i++;
            }

            return navigationItemCollection;
        }


    

        public NavigationItemCollection getSubMenuItems(Infojet infojetContext, WebUserAccount webUserAccount, string webPageCode, NavigationItem parentItem, string languageCode)
        {
            NavigationItemCollection navigationItemCollection = new NavigationItemCollection();

            WebPages webPages = new WebPages(database);

            DataSet childPageDataSet = null;

            if (webUserAccount != null)
            {
                childPageDataSet = webPages.getChildPages(infojetContext.webSite.code, webPageCode, languageCode, webUserAccount.no);
            }
            else
            {
                childPageDataSet = webPages.getChildPages(infojetContext.webSite.code, webPageCode, languageCode);
            }

            int i = 0;
            while (i < childPageDataSet.Tables[0].Rows.Count)
            {
                NavigationItem menuItem = new NavigationItem();

                menuItem.code = childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                menuItem.text = childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString().Replace("&", "&amp;");

                Link link = new Link(infojetContext, childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString().Replace("&", "&amp;"), childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
                menuItem.link = link.toUrl();

                /*
                if (infojetContext.configuration.webSiteCode == "")
                {
                    menuItem.link = infojetContext.webSite.location + childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() + "?webSiteCode="+infojetContext.webSite.code.ToLower()+"&pageCode=" + childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString().ToLower();
                    if (infojetContext.webSite.fancyLinks) menuItem.link = infojetContext.webSite.siteLocation + infojetContext.webSite.shortcutName + "/" + infojetContext.language.cultureValue + "/" + childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString().Replace("&", "").ToLower();
                }
                else
                {
                    menuItem.link = infojetContext.webSite.location + childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() + "?pageCode=" + childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString().ToLower();
                    if (infojetContext.webSite.fancyLinks) menuItem.link = infojetContext.webSite.siteLocation + infojetContext.language.cultureValue + "/" + childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString().Replace("&", "").ToLower();
                }
                */

                menuItem.parentItem = parentItem;
                menuItem.windowMode = int.Parse(childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString());
                
                if (infojetContext.webPage != null)
                {
                    if (infojetContext.webPage.code == childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString()) menuItem.selected = true;
                    if (infojetContext.webPage.code == menuItem.code) menuItem.selected = true;
                }

                menuItem.subNavigationItems = this.getSubMenuItems(infojetContext, webUserAccount, childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), menuItem, languageCode);

                navigationItemCollection.Add(menuItem);

                i++;
            }

            return navigationItemCollection;

        }


        public DataSet getSpecificMenuPages(string webSiteCode, string languageCode)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT DISTINCT t.[Web Page Code], [Filename], [Menu Text], p.[Window Mode], p.[Order], [Help Text] FROM [" + database.getTableName("Web Page Menu Text") + "] t, [" + database.getTableName("Web Page") + "] p, [" + database.getTableName("Web Template") + "] wt, [" + database.getTableName("Web Menu Page") + "] mp WHERE p.[Code] = t.[Web Page Code] AND p.[Web Site Code] = @webSiteCode AND p.[Web Site Code] = t.[Web Site Code] AND t.[Language Code] = @languageCode AND wt.Code = p.[Web Template Code] AND wt.[Web Site Code] = p.[Web Site Code] AND p.[Protected] = '0' AND ((p.[Published From] <= GETDATE() AND p.[Published To] >= GETDATE()) OR (p.[Published From] = '1753-01-01' AND p.[Published To] = '1753-01-01') OR (p.[Published From] <= GETDATE() AND p.[Published To] = '1753-01-01')) AND p.[Published Version No_] > 0 AND p.[Code] = mp.[Web Page Code] AND mp.[Web Site Code] = p.[Web Site Code] AND mp.[Web Menu Code] = @code ORDER BY p.[Order]");
            databaseQuery.addStringParameter("code", code, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return (dataSet);

        }

        public DataSet getSpecificMenuPages(string webSiteCode, string languageCode, string userNo)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT DISTINCT t.[Web Page Code], [Filename], [Menu Text], p.[Window Mode], p.[Order], [Help Text] FROM [" + database.getTableName("Web Page Menu Text") + "] t, [" + database.getTableName("Web Page") + "] p, [" + database.getTableName("Web Template") + "] wt, [" + database.getTableName("Web Page User Group") + "] wpg, [" + database.getTableName("Web User Account Group") + "] wug, [" + database.getTableName("Web User Account Relation") + "] wur, [" + database.getTableName("Web Menu Page") + "] mp WHERE p.[Code] = t.[Web Page Code] AND p.[Web Site Code] = @webSiteCode AND p.[Web Site Code] = t.[Web Site Code] AND t.[Language Code] = @languageCode AND wt.Code = p.[Web Template Code] AND wt.[Web Site Code] = p.[Web Site Code] AND p.Code = wpg.[Web Page Code] AND p.[Web Site Code] = wpg.[Web Site Code] AND wpg.[Web User Group Code] = wug.[Web User Group Code] AND wug.[No_] = @userNo AND wug.[No_] = wur.[No_] AND wur.[Web Site Code] = @webSiteCode AND ((p.[Published From] <= GETDATE() AND p.[Published To] >= GETDATE()) OR (p.[Published From] = '1753-01-01' AND p.[Published To] = '1753-01-01') OR (p.[Published From] <= GETDATE() AND p.[Published To] = '1753-01-01')) AND p.[Published Version No_] > 0 AND p.[Code] = mp.[Web Page Code] AND mp.[Web Site Code] = p.[Web Site Code] AND mp.[Web Menu Code] = @code ORDER BY p.[Order]");
            databaseQuery.addStringParameter("code", code, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("userNo", userNo, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return (dataSet);

        }

	}
}