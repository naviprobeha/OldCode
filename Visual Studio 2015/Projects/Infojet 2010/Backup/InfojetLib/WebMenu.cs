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
            NavigationItemCollection navigationItemCollection = new NavigationItemCollection();
            
            string authenticatedUser = "";
            if (infojetContext.userSession != null)
            {
                authenticatedUser = infojetContext.userSession.webUserAccount.no;
            }

            if (System.Web.HttpContext.Current.Session["menu_" + code + "_" + infojetContext.languageCode+"_"+ authenticatedUser] != null)
            {
                navigationItemCollection = (NavigationItemCollection)System.Web.HttpContext.Current.Session["menu_" + code + "_" + infojetContext.languageCode + "_" + authenticatedUser];
                navigationItemCollection.setSelected(infojetContext.webPage.code);
                return navigationItemCollection;
            }


            WebPages webPages = new WebPages(database);

            DataSet topPageDataSet = null;

            //Main menu
            if (type == 0)
            {

                if (infojetContext.userSession != null)
                {
                    topPageDataSet = webPages.getTopPages(infojetContext.webSite.code, infojetContext.languageCode, infojetContext.userSession.webUserAccount.no);
                }
                else
                {
                    topPageDataSet = webPages.getTopPages(infojetContext.webSite.code, infojetContext.languageCode);
                }

            }

            //Sub menu
            if (type == 1)
            {

                if (infojetContext.userSession != null)
                {
                    topPageDataSet = webPages.getChildPages(infojetContext.webSite.code, infojetContext.webPage.parentWebPageCode, infojetContext.languageCode, infojetContext.userSession.webUserAccount.no);
                }
                else
                {
                    topPageDataSet = webPages.getChildPages(infojetContext.webSite.code, infojetContext.webPage.parentWebPageCode, infojetContext.languageCode);
                }
            }

            //Specific menu
            if (type == 2)
            {

                if (infojetContext.userSession != null)
                {
                    topPageDataSet = this.getSpecificMenuPages(infojetContext.webSite.code, infojetContext.languageCode, infojetContext.userSession.webUserAccount.no);
                }
                else
                {
                    topPageDataSet = this.getSpecificMenuPages(infojetContext.webSite.code, infojetContext.languageCode);
                }
            }


            int i = 0;
            while (i < topPageDataSet.Tables[0].Rows.Count)
            {
                NavigationItem topMenuItem = new NavigationItem();

                topMenuItem.code = topPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                topMenuItem.text = topPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString();
                topMenuItem.link = infojetContext.webSite.location + topPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() + "?pageCode=" + topPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                topMenuItem.parentItem = null;
                topMenuItem.windowMode = int.Parse(topPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString());
                if (infojetContext.webPage.code == topPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString()) topMenuItem.selected = true;

                if (infojetContext.webPage.code == topMenuItem.code) topMenuItem.selected = true;

                topMenuItem.subNavigationItems = this.getSubMenuItems(infojetContext, topPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), topMenuItem);

                topMenuItem.helpText = topPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString();

                navigationItemCollection.Add(topMenuItem);
                i++;
            }

            System.Web.HttpContext.Current.Session.Add("menu_" + code + "_" + infojetContext.languageCode + "_" + authenticatedUser, navigationItemCollection);

            return navigationItemCollection;
        }


    

        public NavigationItemCollection getSubMenuItems(Infojet infojetContext, string webPageCode, NavigationItem parentItem)
        {
            NavigationItemCollection navigationItemCollection = new NavigationItemCollection();

            WebPages webPages = new WebPages(database);

            DataSet childPageDataSet = null;

            if (infojetContext.userSession != null)
            {
                childPageDataSet = webPages.getChildPages(infojetContext.webSite.code, webPageCode, infojetContext.languageCode, infojetContext.userSession.webUserAccount.no);
            }
            else
            {
                childPageDataSet = webPages.getChildPages(infojetContext.webSite.code, webPageCode, infojetContext.languageCode);
            }

            int i = 0;
            while (i < childPageDataSet.Tables[0].Rows.Count)
            {
                NavigationItem menuItem = new NavigationItem();

                menuItem.code = childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                menuItem.text = childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString();
                menuItem.link = infojetContext.webSite.location + childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() + "?pageCode=" + childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                menuItem.parentItem = parentItem;
                menuItem.windowMode = int.Parse(childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString());
                if (infojetContext.webPage.code == childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString()) menuItem.selected = true;

                if (infojetContext.webPage.code == menuItem.code) menuItem.selected = true;

                menuItem.subNavigationItems = this.getSubMenuItems(infojetContext, childPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), menuItem);

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