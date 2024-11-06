using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for DefaultSearchEngine.
	/// </summary>
	public class DefaultSearchEngine
	{
        private Hashtable itemsFound;

		public DefaultSearchEngine()
		{
			//
			// TODO: Add constructor logic here
			//
            itemsFound = new Hashtable();
		}
        
        public NavigationItemCollection getAllSearchResults(NavigationItemCollection navigationItemCollection, Navipro.Infojet.Lib.Infojet infojetContext, string searchQuery)
        {

            if ((searchQuery != null) && (searchQuery != ""))
            {
                string alternativeSearchQuery = createAltSearchQuery(searchQuery);

                WebPages webPages = new WebPages(infojetContext.systemDatabase);

                DataSet webPageDataSet = null;
                if (infojetContext.userSession != null)
                {
                    webPageDataSet = webPages.getAllPages(infojetContext.webSite.code, infojetContext.languageCode, infojetContext.userSession.webUserAccount.no);
                }
                else
                {
                    webPageDataSet = webPages.getAllPages(infojetContext.webSite.code, infojetContext.languageCode);
                }

                int i = 0;
                while (i < webPageDataSet.Tables[0].Rows.Count)
                {
                    string pageLink = webPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() + "?pageCode=" + webPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                    //Page Text Content		
                    SqlDataAdapter sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT [Web Page Code], [Text] FROM [" + infojetContext.systemDatabase.getTableName("Web Page Line Text") + "] l WHERE l.[Web Site Code] = '" + infojetContext.webSite.code + "' AND l.[Web Page Code] = '" + webPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() + "' AND l.[Language Code] = '" + infojetContext.languageCode + "' AND l.[Version No_] = '" + webPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() + "' AND (UPPER(l.[Text]) LIKE '%" + searchQuery.ToUpper() + "%' OR UPPER(l.[Text]) LIKE '%" + alternativeSearchQuery.ToUpper() + "%')");
                    DataSet textDataSet = new DataSet();
                    sqlDataAdapter.Fill(textDataSet);

                    if (textDataSet.Tables[0].Rows.Count > 0)
                    {
                        string textResult = textDataSet.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

                        textResult = cleanHtmlTags(textResult);

                        textResult = textResult.Replace(searchQuery, "<b>" + searchQuery + "</b>");

                        NavigationItem navigationItem = new NavigationItem();
                        navigationItem.description = webPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString();
                        navigationItem.text = textResult;
                        navigationItem.link = pageLink;

                        navigationItemCollection.Add(navigationItem);
                    }


                    searchForItemCampains(navigationItemCollection, infojetContext, searchQuery, webPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), int.Parse(webPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString()));

                    i++;
                }

                searchForItems(navigationItemCollection, infojetContext, searchQuery);

            }

            return navigationItemCollection;
        }


        public NavigationItemCollection searchForItemCampains(NavigationItemCollection navigationItemCollection, Navipro.Infojet.Lib.Infojet infojetContext, string searchQuery, string webPageCode, int versionNo)
        {
            //Items	Campains		
            SqlDataAdapter sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT cm.[No_], c.[Display Web Page Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Page Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Web Item Campain") + "] c, [" + infojetContext.systemDatabase.getTableName("Web Item Campain Member") + "] cm LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item") + "] i ON i.[No_] = cm.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item Translation") + "] cmi ON cmi.[Item No_] = i.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Extended Text Line") + "] etl ON etl.[Table Name] = 2 AND i.[No_] = etl.[No_] WHERE cm.[Type] = 0 AND l.[Web Site Code] = '" + infojetContext.webSite.code + "' AND l.[Web Page Code] = '" + webPageCode + "' AND l.[Type] = '7' AND l.[Version No_] = '" + versionNo + "' AND l.[Code] = c.[Code] AND l.[Web Site Code] = c.[Web Site Code] AND l.[Code] = cm.[Web Item Campain Code] AND cm.[Web Site Code] = l.[Web Site Code] AND ((UPPER(cm.[No_]) LIKE '%" + searchQuery.ToUpper() + "%') OR (UPPER(cmi.[Description]) LIKE '%" + searchQuery.ToUpper() + "%') OR (UPPER(i.[Description]) LIKE '%" + searchQuery.ToUpper() + "%') OR (UPPER(etl.[Text]) LIKE '%" + searchQuery.ToUpper() + "%'))");
            DataSet campainDataSet = new DataSet();
            sqlDataAdapter.Fill(campainDataSet);

            int j = 0;
            string prevItemNo = "";

            while (j < campainDataSet.Tables[0].Rows.Count)
            {

                if (prevItemNo != campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString())
                {
                    WebPage campainDisplayPage = new WebPage(infojetContext, infojetContext.webSite.code, campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(1).ToString());

                    Item item = new Item(infojetContext.systemDatabase, campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());
                    ItemTranslation itemTranslation = item.getItemTranslation(infojetContext.languageCode);
                    string itemText = item.getItemText(infojetContext.languageCode);

                    if (!itemsFound.Contains(campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString()))
                    {

                        NavigationItem navigationItem = new NavigationItem();
                        navigationItem.code = campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();
                        navigationItem.description = campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() + " " + itemTranslation.description;
                        navigationItem.text = itemText;
                        navigationItem.link = campainDisplayPage.getUrl() + "&itemNo=" + campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();

                        navigationItemCollection.Add(navigationItem);

                        itemsFound.Add(navigationItem.code, navigationItem);

                    }
                }

                prevItemNo = campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();

                j++;
            }


            //Model	Campains		
            sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT cm.[No_], c.[Display Web Page Code], wmv.[Item No_] FROM [" + infojetContext.systemDatabase.getTableName("Web Page Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Web Item Campain") + "] c, [" + infojetContext.systemDatabase.getTableName("Web Item Campain Member") + "] cm LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model") + "] wm ON wm.[No_] = cm.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model Variant") + "] wmv ON wmv.[Web Model No_] = cm.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model Translation") + "] wmt ON wmt.[Web Model No_] = cm.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Extended Text Line") + "] etl ON etl.[Table Name] = 2 AND wmv.[Item No_] = etl.[No_] WHERE cm.[Type] = 1 AND l.[Web Site Code] = '" + infojetContext.webSite.code + "' AND l.[Web Page Code] = '" + webPageCode + "' AND l.[Type] = '7' AND l.[Version No_] = '" + versionNo + "' AND l.[Code] = c.[Code] AND l.[Web Site Code] = c.[Web Site Code] AND l.[Code] = cm.[Web Item Campain Code] AND cm.[Web Site Code] = l.[Web Site Code] AND wmv.[Primary] = 1 AND ((UPPER(cm.[No_]) LIKE '%" + searchQuery.ToUpper() + "%') OR (UPPER(wmt.[Description]) LIKE '%" + searchQuery.ToUpper() + "%') OR (UPPER(wm.[Description]) LIKE '%" + searchQuery.ToUpper() + "%') OR (UPPER(etl.[Text]) LIKE '%" + searchQuery.ToUpper() + "%'))");
            campainDataSet = new DataSet();
            sqlDataAdapter.Fill(campainDataSet);

            j = 0;
            prevItemNo = "";

            while (j < campainDataSet.Tables[0].Rows.Count)
            {

                if (prevItemNo != campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString())
                {
                    WebPage campainDisplayPage = new WebPage(infojetContext, infojetContext.webSite.code, campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(1).ToString());

                    WebModel webModel = new WebModel(infojetContext, campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());
                    Item item = new Item(infojetContext.systemDatabase, campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString());
                    WebModelTranslation webModelTranslation = webModel.getTranslation(infojetContext.languageCode);
                    string itemText = item.getItemText(infojetContext.languageCode);

                    if (!itemsFound.Contains(campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString()))
                    {

                        NavigationItem navigationItem = new NavigationItem();
                        navigationItem.code = campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();
                        navigationItem.description = campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() + " " + webModelTranslation.description;
                        navigationItem.text = itemText;
                        navigationItem.link = campainDisplayPage.getUrl() + "&webModelNo=" + campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString()+"&itemNo="+ campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString();

                        navigationItemCollection.Add(navigationItem);

                        itemsFound.Add(navigationItem.code, navigationItem);

                    }
                }

                prevItemNo = campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();

                j++;
            }

            return navigationItemCollection;

        }

        public NavigationItemCollection searchForItems(NavigationItemCollection navigationItemCollection, Navipro.Infojet.Lib.Infojet infojetContext, string searchQuery)
        {

            //Items	Categories		
            SqlDataAdapter sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT cm.[Code], c.[Item Info Web Page Code], cm.[Item Info Web Page Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category") + "] c, [" + infojetContext.systemDatabase.getTableName("Web Item Category Member") + "] cm, [" + infojetContext.systemDatabase.getTableName("Item") + "] i LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item Translation") + "] cmi ON cmi.[Item No_] = i.[No_] WHERE c.[Web Site Code] = '" + infojetContext.webSite.code + "'  AND c.[Code] = cm.[Web Item Category Code] AND cm.[Type] = '1' AND cm.[Web Site Code] = c.[Web Site Code] AND i.[No_] = cm.[Code] AND ((UPPER(cm.[Code]) LIKE '%" + searchQuery.ToUpper() + "%') OR (UPPER(cmi.[Description]) LIKE '%" + searchQuery.ToUpper() + "%') OR (UPPER(i.[Description]) LIKE '%" + searchQuery.ToUpper() + "%'))");
            DataSet categoryDataSet = new DataSet();
            sqlDataAdapter.Fill(categoryDataSet);

            int j = 0;
            string prevItemNo = "";

            while (j < categoryDataSet.Tables[0].Rows.Count)
            {

                if (prevItemNo != categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString())
                {
                    WebPage categoryDisplayPage = new WebPage(infojetContext, infojetContext.webSite.code, categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(1).ToString());

                    if (categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString() != "")
                    {
                        categoryDisplayPage = new WebPage(infojetContext, infojetContext.webSite.code, categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString());
                    }

                    Item item = new Item(infojetContext.systemDatabase, categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());
                    ItemTranslation itemTranslation = item.getItemTranslation(infojetContext.languageCode);
                    string itemText = item.getItemText(infojetContext.languageCode);

                    if (!itemsFound.Contains(categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString()))
                    {
                        NavigationItem navigationItem = new NavigationItem();
                        navigationItem.code = categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();
                        navigationItem.description = categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString() + " " + itemTranslation.description;
                        navigationItem.text = itemText;
                        navigationItem.link = categoryDisplayPage.getUrl() + "&itemNo=" + categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();

                        navigationItemCollection.Add(navigationItem);

                        itemsFound.Add(navigationItem.code, navigationItem);
                    }
                }

                prevItemNo = categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();

                j++;
            }

            return navigationItemCollection;
        }

        private string cleanHtmlTags(string text)
        {

            while (text.IndexOf(">") > -1)
            {
                int endPos = text.IndexOf(">");
                int startPos = endPos - 1;

                while ((startPos > 0) && (text[startPos] != '<'))
                {
                    startPos--;
                }
                
                string buffer = text;
                text = "";
                if (startPos > 0)
                {
                    text = buffer.Substring(0, startPos);
                }
                text = text + buffer.Substring(endPos + 1);

            }

            if (text.IndexOf("<") > -1)
            {
                text = text.Substring(0, text.IndexOf("<"));

            }

            return text;
        }

        private string createAltSearchQuery(string searchQuery)
        {
            searchQuery = searchQuery.Replace("å", "&aring;");
            searchQuery = searchQuery.Replace("ä", "&auml;");
            searchQuery = searchQuery.Replace("ö", "&ouml;");
            searchQuery = searchQuery.Replace("Å", "&Aring;");
            searchQuery = searchQuery.Replace("Ä", "&Auml;");
            searchQuery = searchQuery.Replace("Ö", "&Ouml;");

            return searchQuery;
        }
	}
}
