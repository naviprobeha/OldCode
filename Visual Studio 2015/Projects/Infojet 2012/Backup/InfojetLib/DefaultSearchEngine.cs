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
                searchQuery = searchQuery.Replace("'", " ");
                searchQuery = searchQuery.Replace("\"", " ");
                
                if (searchQuery.Length > 30) searchQuery = searchQuery.Substring(1, 30);

                string alternativeSearchQuery = createAltSearchQuery(searchQuery);

                searchForItems(navigationItemCollection, infojetContext, searchQuery);


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

                    searchForItemCampains(navigationItemCollection, infojetContext, searchQuery, webPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), int.Parse(webPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString()));

                    Link link = new Link(infojetContext, webPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), webPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString(), webPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString());
                    string pageLink = link.toUrl();
                    //string pageLink = webPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() + "?pageCode=" + System.Web.HttpUtility.UrlEncode(webPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());

                    //Page Text Content
		            
                    //SqlDataAdapter sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT [Web Page Code], [Text] FROM [" + infojetContext.systemDatabase.getTableName("Web Page Line Text") + "] l WHERE l.[Web Site Code] = '" + infojetContext.webSite.code + "' AND l.[Web Page Code] = '" + webPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() + "' AND l.[Language Code] = '" + infojetContext.languageCode + "' AND l.[Version No_] = '" + webPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString() + "' AND (UPPER(l.[Text]) LIKE '% " + searchQuery.ToUpper() + " %' OR UPPER(l.[Text]) LIKE '% " + alternativeSearchQuery.ToUpper() + " %')");
                    DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT l.[Web Page Code], [Text], l.[Version No_] FROM [" + infojetContext.systemDatabase.getTableName("Web Page Line Text") + "] l, [" + infojetContext.systemDatabase.getTableName("Web Page Line") + "] pl WHERE l.[Web Site Code] = @webSiteCode AND l.[Web Page Code] = @webPageCode AND l.[Language Code] = @languageCode AND l.[Version No_] = @versionNo AND l.[Web Site Code] = pl.[Web Site Code] AND l.[Web Page Code] = pl.[Web Page Code] AND l.[Web Page Line No_] = pl.[Line No_] AND l.[Version No_] = pl.[Version No_] AND (UPPER(l.[Text]) LIKE '%'+ @searchQuery + '%' OR UPPER(l.[Text]) LIKE '%'+ @altSearchQuery + '%')");
                    databaseQuery.addStringParameter("webSiteCode", infojetContext.webSite.code, 20);
                    databaseQuery.addStringParameter("webPageCode", webPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString(), 20);
                    databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);
                    databaseQuery.addIntParameter("versionNo", int.Parse(webPageDataSet.Tables[0].Rows[i].ItemArray.GetValue(3).ToString()));
                    databaseQuery.addStringParameter("searchQuery", searchQuery.ToUpper(), 100);
                    databaseQuery.addStringParameter("altSearchQuery", alternativeSearchQuery.ToUpper(), 100);
                    

                    SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
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


                    i++;
                }


            }

            return navigationItemCollection;
        }


        public NavigationItemCollection searchForItemCampains(NavigationItemCollection navigationItemCollection, Navipro.Infojet.Lib.Infojet infojetContext, string searchQuery, string webPageCode, int versionNo)
        {
            //Items	Campains		
            //SqlDataAdapter sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT cm.[No_], c.[Display Web Page Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Page Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Web Item Campain") + "] c, [" + infojetContext.systemDatabase.getTableName("Web Item Campain Member") + "] cm LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item") + "] i ON i.[No_] = cm.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item Translation") + "] cmi ON cmi.[Item No_] = i.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Extended Text Line") + "] etl ON etl.[Table Name] = 2 AND i.[No_] = etl.[No_] WHERE cm.[Type] = 0 AND l.[Web Site Code] = '" + infojetContext.webSite.code + "' AND l.[Web Page Code] = '" + webPageCode + "' AND l.[Type] = '7' AND l.[Version No_] = '" + versionNo + "' AND l.[Code] = c.[Code] AND l.[Web Site Code] = c.[Web Site Code] AND l.[Code] = cm.[Web Item Campain Code] AND cm.[Web Site Code] = l.[Web Site Code] AND ((UPPER(cm.[No_]) LIKE '%" + searchQuery.ToUpper() + "%') OR (UPPER(cmi.[Description]) LIKE '%" + searchQuery.ToUpper() + "%') OR (UPPER(i.[Description]) LIKE '%" + searchQuery.ToUpper() + "%') OR (UPPER(etl.[Text]) LIKE '%" + searchQuery.ToUpper() + "%'))");
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT cm.[No_], c.[Display Web Page Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Page Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Web Item Campain") + "] c, [" + infojetContext.systemDatabase.getTableName("Web Item Campain Member") + "] cm LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item") + "] i ON i.[No_] = cm.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item Translation") + "] cmi ON cmi.[Item No_] = i.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Extended Text Line") + "] etl ON etl.[Table Name] = 2 AND i.[No_] = etl.[No_] WHERE cm.[Type] = 0 AND l.[Web Site Code] = @webSiteCode AND l.[Web Page Code] = @webPageCode AND l.[Type] = '7' AND l.[Version No_] = @versionNo AND l.[Code] = c.[Code] AND l.[Web Site Code] = c.[Web Site Code] AND l.[Code] = cm.[Web Item Campain Code] AND cm.[Web Site Code] = l.[Web Site Code] AND ((UPPER(cm.[No_]) LIKE '%' + @searchQuery + '%') OR (UPPER(cmi.[Description]) LIKE '%' + @searchQuery + '%') OR (UPPER(i.[Description]) LIKE '%' + @searchQuery + '%') OR (UPPER(etl.[Text]) LIKE '%' + @searchQuery + '%'))");
            databaseQuery.addStringParameter("webSiteCode", infojetContext.webSite.code, 20);
            databaseQuery.addStringParameter("webPageCode", webPageCode, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);
            databaseQuery.addIntParameter("versionNo", versionNo);
            databaseQuery.addStringParameter("searchQuery", searchQuery.ToUpper(), 100);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet campainDataSet = new DataSet();
            sqlDataAdapter.Fill(campainDataSet);

            int j = 0;
            string prevItemNo = "";

            while (j < campainDataSet.Tables[0].Rows.Count)
            {

                if (prevItemNo != campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString())
                {
                    WebPage campainDisplayPage = new WebPage(infojetContext, infojetContext.webSite.code, campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(1).ToString());

                    //Item item = new Item(infojetContext, campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());
                    Item item = Item.get(infojetContext, campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());

                    ItemTranslation itemTranslation = item.getItemTranslation(infojetContext.languageCode);
                    string itemText = item.getItemText(infojetContext.languageCode, false);

                    WebItemImages webItemImages = new WebItemImages(infojetContext);
                    WebItemImage webItemImage = webItemImages.getItemProductImage(item.no, infojetContext.webSite.code);

                    if (!itemsFound.Contains(campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString()))
                    {
                        ProductItem productItem = new ProductItem(infojetContext, item);
                        if (productItem.isAvailable(infojetContext.webSite.code))
                        {
                            NavigationItem navigationItem = new NavigationItem();
                            navigationItem.code = campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();
                            navigationItem.description = itemTranslation.description;
                            navigationItem.description2 = campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();

                            Link link = campainDisplayPage.getUrlLink();
                            link.addParameter("itemNo", campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());


                            navigationItem.text = itemText;
                            //navigationItem.link = campainDisplayPage.getUrl() + "&itemNo=" + System.Web.HttpUtility.UrlEncode(campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());
                            navigationItem.link = link.toUrl();

                            if (webItemImage != null) navigationItem.webImage = webItemImage.image;

                            navigationItemCollection.Add(navigationItem);
                        }

                        itemsFound.Add(campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString(), null);

                    }
                }

                prevItemNo = campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();

                j++;
            }


            //Model	Campains		
            //sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT cm.[No_], c.[Display Web Page Code], wmv.[Item No_] FROM [" + infojetContext.systemDatabase.getTableName("Web Page Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Web Item Campain") + "] c, [" + infojetContext.systemDatabase.getTableName("Web Item Campain Member") + "] cm LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model") + "] wm ON wm.[No_] = cm.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model Variant") + "] wmv ON wmv.[Web Model No_] = cm.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model Translation") + "] wmt ON wmt.[Web Model No_] = cm.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Extended Text Line") + "] etl ON etl.[Table Name] = 2 AND wmv.[Item No_] = etl.[No_] WHERE cm.[Type] = 1 AND l.[Web Site Code] = '" + infojetContext.webSite.code + "' AND l.[Web Page Code] = '" + webPageCode + "' AND l.[Type] = '7' AND l.[Version No_] = '" + versionNo + "' AND l.[Code] = c.[Code] AND l.[Web Site Code] = c.[Web Site Code] AND l.[Code] = cm.[Web Item Campain Code] AND cm.[Web Site Code] = l.[Web Site Code] AND wmv.[Primary] = 1 AND ((UPPER(cm.[No_]) LIKE '%" + searchQuery.ToUpper() + "%') OR (UPPER(wmt.[Description]) LIKE '%" + searchQuery.ToUpper() + "%') OR (UPPER(wm.[Description]) LIKE '%" + searchQuery.ToUpper() + "%') OR (UPPER(etl.[Text]) LIKE '%" + searchQuery.ToUpper() + "%'))");
            databaseQuery = infojetContext.systemDatabase.prepare("SELECT cm.[No_], c.[Display Web Page Code], wmv.[Item No_] FROM [" + infojetContext.systemDatabase.getTableName("Web Page Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Web Item Campain") + "] c, [" + infojetContext.systemDatabase.getTableName("Web Item Campain Member") + "] cm LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model") + "] wm ON wm.[No_] = cm.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model Variant") + "] wmv ON wmv.[Web Model No_] = cm.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model Translation") + "] wmt ON wmt.[Web Model No_] = cm.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Extended Text Line") + "] etl ON etl.[Table Name] = 2 AND wmv.[Item No_] = etl.[No_] WHERE cm.[Type] = 1 AND l.[Web Site Code] = @webSiteCode AND l.[Web Page Code] = @webPageCode AND l.[Type] = '7' AND l.[Version No_] = @versionNo AND l.[Code] = c.[Code] AND l.[Web Site Code] = c.[Web Site Code] AND l.[Code] = cm.[Web Item Campain Code] AND cm.[Web Site Code] = l.[Web Site Code] AND wmv.[Primary] = 1 AND ((UPPER(cm.[No_]) LIKE '%' + @searchQuery + '%') OR (UPPER(wmt.[Description]) LIKE '%' + @searchQuery + '%') OR (UPPER(wm.[Description]) LIKE '%' + @searchQuery + '%') OR (UPPER(etl.[Text]) LIKE '%' + @searchQuery + '%'))");
            databaseQuery.addStringParameter("webSiteCode", infojetContext.webSite.code, 20);
            databaseQuery.addStringParameter("webPageCode", webPageCode, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);
            databaseQuery.addIntParameter("versionNo", versionNo);
            databaseQuery.addStringParameter("searchQuery", searchQuery.ToUpper(), 100);

            sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
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
                    
                    //Item item = new Item(infojetContext, campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString());
                    Item item = Item.get(infojetContext, campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString());

                    WebModelTranslation webModelTranslation = webModel.getTranslation(infojetContext.languageCode);
                    string itemText = item.getItemText(infojetContext.languageCode, false);

                    WebItemImages webItemImages = new WebItemImages(infojetContext);
                    WebItemImage webItemImage = webItemImages.getItemProductImage(item.no, infojetContext.webSite.code);


                    if (!itemsFound.Contains(campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString()))
                    {
                        ProductItem productItem = new ProductItem(infojetContext, item);
                        if (productItem.isAvailable(infojetContext.webSite.code))
                        {

                            NavigationItem navigationItem = new NavigationItem();
                            navigationItem.code = campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();
                            navigationItem.description = webModelTranslation.description;
                            navigationItem.description2 = campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();

                            Link link = campainDisplayPage.getUrlLink();
                            link.addParameter("itemNo", campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString());
                            link.addParameter("webModelNo", campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());

                            navigationItem.text = itemText;
                            //navigationItem.link = campainDisplayPage.getUrl() + "&webModelNo=" + System.Web.HttpUtility.UrlEncode(campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString()) + "&itemNo=" + System.Web.HttpUtility.UrlEncode(campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString());
                            navigationItem.link = link.toUrl();
                            if (webItemImage != null) navigationItem.webImage = webItemImage.image;

                            navigationItemCollection.Add(navigationItem);
                        }
                        itemsFound.Add(campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString(), null);

                    }
                }

                prevItemNo = campainDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();

                j++;
            }

            return navigationItemCollection;

        }

        public NavigationItemCollection searchForCategories(NavigationItemCollection navigationItemCollection, Navipro.Infojet.Lib.Infojet infojetContext, string searchQuery)
        {

            //Items	Categories		
            //SqlDataAdapter sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT cm.[Code], c.[Item Info Web Page Code], cm.[Item Info Web Page Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category") + "] c, [" + infojetContext.systemDatabase.getTableName("Web Item Category Member") + "] cm, [" + infojetContext.systemDatabase.getTableName("Item") + "] i LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item Translation") + "] cmi ON cmi.[Item No_] = i.[No_] WHERE c.[Web Site Code] = '" + infojetContext.webSite.code + "'  AND c.[Code] = cm.[Web Item Category Code] AND cm.[Type] = '1' AND cm.[Web Site Code] = c.[Web Site Code] AND i.[No_] = cm.[Code] AND ((UPPER(cm.[Code]) LIKE '%" + searchQuery.ToUpper() + "%') OR (UPPER(cmi.[Description]) LIKE '%" + searchQuery.ToUpper() + "%') OR (UPPER(i.[Description]) LIKE '%" + searchQuery.ToUpper() + "%'))");
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT c.[Code], c.[Item List Web Page Code], ctr.[Description] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category") + "] c, [" + infojetContext.systemDatabase.getTableName("Web Item Category Translation") + "] ctr LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Item Category Text") + "] ct ON ct.[Web Site Code] = c.[Web Site Code] AND ct.[Web Item Category Code] = c.[Code] AND ct.[Language Code] = @languageCode WHERE c.[Web Site Code] = @webSiteCode AND c.[Code] = ctr.[Web Item Category Code] AND ctr.[Web Site Code] = c.[Web Site Code] AND ctr.[Language Code] = @languageCode AND c.[Protected] = 0 AND ((UPPER(ctr.[Description]) LIKE '%' + @searchQuery + '%') OR (UPPER(ct.[Text]) LIKE '%' + @searchQuery + '%'))");
            databaseQuery.addStringParameter("webSiteCode", infojetContext.webSite.code, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);
            databaseQuery.addStringParameter("searchQuery", searchQuery.ToUpper(), 100);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet categoryDataSet = new DataSet();
            sqlDataAdapter.Fill(categoryDataSet);

            int j = 0;
            string prevCategoryCode = "";

            while (j < categoryDataSet.Tables[0].Rows.Count)
            {

                if (prevCategoryCode != categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString())
                {
                    WebPage categoryDisplayPage = new WebPage(infojetContext, infojetContext.webSite.code, categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(1).ToString());

                    if (categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString() != "")
                    {
                        categoryDisplayPage = new WebPage(infojetContext, infojetContext.webSite.code, categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString());
                    }

                    if (!itemsFound.Contains(categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString()))
                    {

                        WebItemCategory webItemCategory = new WebItemCategory(infojetContext, categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());

                        Link link = categoryDisplayPage.getUrlLink();
                        link.setCategory(categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString(), categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString());

                        NavigationItem navigationItem = new NavigationItem();
                        navigationItem.code = categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();
                        navigationItem.description = categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString();
                        navigationItem.description2 = "";
                        navigationItem.text = webItemCategory.getDescription(); ;
                        //navigationItem.link = categoryDisplayPage.getUrl() + "&category=" + System.Web.HttpUtility.UrlEncode(categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());
                        navigationItem.link = link.toUrl();
                        //if (webItemImage != null) navigationItem.webImage = webItemImage.image;

                        navigationItemCollection.Add(navigationItem);
                        itemsFound.Add(categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString(), null);

                    }
                }

                prevCategoryCode = categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();

                j++;
            }

            return navigationItemCollection;
        }

        public NavigationItemCollection searchForItems(NavigationItemCollection navigationItemCollection, Navipro.Infojet.Lib.Infojet infojetContext, string searchQuery)
        {

            //Items	Categories		
            //SqlDataAdapter sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT cm.[Code], c.[Item Info Web Page Code], cm.[Item Info Web Page Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category") + "] c, [" + infojetContext.systemDatabase.getTableName("Web Item Category Member") + "] cm, [" + infojetContext.systemDatabase.getTableName("Item") + "] i LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item Translation") + "] cmi ON cmi.[Item No_] = i.[No_] WHERE c.[Web Site Code] = '" + infojetContext.webSite.code + "'  AND c.[Code] = cm.[Web Item Category Code] AND cm.[Type] = '1' AND cm.[Web Site Code] = c.[Web Site Code] AND i.[No_] = cm.[Code] AND ((UPPER(cm.[Code]) LIKE '%" + searchQuery.ToUpper() + "%') OR (UPPER(cmi.[Description]) LIKE '%" + searchQuery.ToUpper() + "%') OR (UPPER(i.[Description]) LIKE '%" + searchQuery.ToUpper() + "%'))");
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT cm.[Code], c.[Item Info Web Page Code], cm.[Item Info Web Page Code], c.[Require Item Translation] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category") + "] c, [" + infojetContext.systemDatabase.getTableName("Web Item Category Member") + "] cm, [" + infojetContext.systemDatabase.getTableName("Item") + "] i LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Item Translation") + "] cmi ON cmi.[Item No_] = i.[No_] LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Extended Text Line") + "] etl ON etl.[No_] = i.[No_] AND etl.[Table Name] = 2 WHERE c.[Web Site Code] = @webSiteCode AND c.[Code] = cm.[Web Item Category Code] AND cm.[Type] = '1' AND cm.[Web Site Code] = c.[Web Site Code] AND i.[No_] = cm.[Code] AND ((UPPER(cm.[Code]) LIKE '%' + @searchQuery + '%') OR (UPPER(cmi.[Description]) LIKE '%' + @searchQuery + '%') OR (UPPER(i.[Description]) LIKE '%' + @searchQuery + '%') OR (UPPER(etl.[Text]) LIKE '%' + @searchQuery + '%'))");
            databaseQuery.addStringParameter("webSiteCode", infojetContext.webSite.code, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);
            databaseQuery.addStringParameter("searchQuery", searchQuery.ToUpper(), 100);

            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
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

                    //Item item = new Item(infojetContext, categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());
                    Item item = Item.get(infojetContext, categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());

                    //ItemTranslation itemTranslation = item.getItemTranslation(infojetContext.languageCode);
                    ItemTranslation itemTranslation = new ItemTranslation(infojetContext.systemDatabase, item.no, "", infojetContext.languageCode);                    

                    string itemText = item.getItemText(infojetContext.languageCode, false);

                    WebItemImages webItemImages = new WebItemImages(infojetContext);
                    WebItemImage webItemImage = webItemImages.getItemProductImage(item.no, infojetContext.webSite.code);

                    if (!itemsFound.Contains(categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString()))
                    {
                        bool include = true;
                        if (categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(3).ToString() == "1")
                        {
                            if (itemTranslation.description == null) include = false;
                        }

                        if ((itemTranslation.description == null) || (itemTranslation.description == "")) itemTranslation.description = item.description;
                        if ((itemTranslation.description2 == null) || (itemTranslation.description2 == "")) itemTranslation.description2 = item.description2;


                        if (include)
                        {
                            ProductItem productItem = new ProductItem(infojetContext, item);
                            if (productItem.isAvailable(infojetContext.webSite.code))
                            {
                                Link link = categoryDisplayPage.getUrlLink();
                                link.setItem("", categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString(), itemTranslation.description);

                                NavigationItem navigationItem = new NavigationItem();
                                navigationItem.code = categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();
                                navigationItem.description = itemTranslation.description;
                                navigationItem.description2 = categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();
                                navigationItem.text = itemText;
                                //navigationItem.link = categoryDisplayPage.getUrl() + "&itemNo=" + System.Web.HttpUtility.UrlEncode(categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());
                                navigationItem.link = link.toUrl();
                                if (webItemImage != null) navigationItem.webImage = webItemImage.image;

                                navigationItemCollection.Add(navigationItem);
                            }
                            itemsFound.Add(categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString(), null);
                        }
                    }
                }

                prevItemNo = categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();

                j++;
            }

            //Model	Categories		
            //sqlDataAdapter = infojetContext.systemDatabase.dataAdapterQuery("SELECT cm.[Code], c.[Item Info Web Page Code], cm.[Item Info Web Page Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category") + "] c, [" + infojetContext.systemDatabase.getTableName("Web Item Category Member") + "] cm, [" + infojetContext.systemDatabase.getTableName("Web Model") + "] i LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model Translation") + "] cmi ON cmi.[Web Model No_] = i.[No_] WHERE c.[Web Site Code] = '" + infojetContext.webSite.code + "'  AND c.[Code] = cm.[Web Item Category Code] AND cm.[Type] = '2' AND cm.[Web Site Code] = c.[Web Site Code] AND i.[No_] = cm.[Code] AND ((UPPER(cm.[Code]) LIKE '%" + searchQuery.ToUpper() + "%') OR (UPPER(cmi.[Description]) LIKE '%" + searchQuery.ToUpper() + "%') OR (UPPER(i.[Description]) LIKE '%" + searchQuery.ToUpper() + "%'))");

            databaseQuery = infojetContext.systemDatabase.prepare("SELECT cm.[Code], c.[Item Info Web Page Code], cm.[Item Info Web Page Code], c.[Require Item Translation] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Category") + "] c, [" + infojetContext.systemDatabase.getTableName("Web Item Category Member") + "] cm, [" + infojetContext.systemDatabase.getTableName("Web Model") + "] i LEFT JOIN [" + infojetContext.systemDatabase.getTableName("Web Model Translation") + "] cmi ON cmi.[Web Model No_] = i.[No_] WHERE c.[Web Site Code] = @webSiteCode  AND c.[Code] = cm.[Web Item Category Code] AND cm.[Type] = '2' AND cm.[Web Site Code] = c.[Web Site Code] AND i.[No_] = cm.[Code] AND ((UPPER(cm.[Code]) LIKE '%' + @searchQuery + '%') OR (UPPER(cmi.[Description]) LIKE '%' + @searchQuery + '%') OR (UPPER(i.[Description]) LIKE '%' + @searchQuery + '%'))");
            databaseQuery.addStringParameter("webSiteCode", infojetContext.webSite.code, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);
            databaseQuery.addStringParameter("searchQuery", searchQuery.ToUpper(), 100);

            sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            categoryDataSet = new DataSet();
            sqlDataAdapter.Fill(categoryDataSet);

            j = 0;
            prevItemNo = "";

            while (j < categoryDataSet.Tables[0].Rows.Count)
            {

                if (prevItemNo != categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString())
                {
                    WebPage categoryDisplayPage = new WebPage(infojetContext, infojetContext.webSite.code, categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(1).ToString());

                    if (categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString() != "")
                    {
                        categoryDisplayPage = new WebPage(infojetContext, infojetContext.webSite.code, categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(2).ToString());
                    }

                    WebModel webModel = new WebModel(infojetContext, categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());
                    //WebModelTranslation webModelTranslation = webModel.getTranslation(infojetContext.languageCode);
                    
                    WebModelTranslation webModelTranslation = new WebModelTranslation(infojetContext, webModel.no, infojetContext.languageCode);

                    //Item item = new Item(infojetContext, webModel.getDefaultItemNo());
                    Item item = Item.get(infojetContext, webModel.getDefaultItemNo());

                    string itemText = item.getItemText(infojetContext.languageCode, false);

                    WebItemImages webItemImages = new WebItemImages(infojetContext);
                    WebItemImage webItemImage = webItemImages.getItemProductImage(item.no, infojetContext.webSite.code);

                    if (!itemsFound.Contains(categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString()))
                    {
                        bool include = true;
                        if (categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(3).ToString() == "1")
                        {
                            if (webModelTranslation.description == null) include = false;
                        }

                        if (webModelTranslation.description == null) webModelTranslation.description = webModel.description;
                        if (webModelTranslation.description2 == null) webModelTranslation.description2 = webModel.description2;

                        if (include)
                        {
                            ProductItem productItem = new ProductItem(infojetContext, webModel, item);
                            if (productItem.isAvailable(infojetContext.webSite.code))
                            {

                                Link link = categoryDisplayPage.getUrlLink();
                                link.setItem(categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString(), item.no, webModelTranslation.description);

                                NavigationItem navigationItem = new NavigationItem();
                                navigationItem.code = categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();
                                navigationItem.description = webModelTranslation.description;
                                navigationItem.description2 = categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString();
                                navigationItem.text = itemText;
                                //navigationItem.link = categoryDisplayPage.getUrl() + "&itemNo=" + System.Web.HttpUtility.UrlEncode(item.no) + "&webModelNo=" + System.Web.HttpUtility.UrlEncode(categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());
                                navigationItem.link = link.toUrl();
                                if (webItemImage != null) navigationItem.webImage = webItemImage.image;
                                navigationItemCollection.Add(navigationItem);

                                
                            }
                            itemsFound.Add(categoryDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString(), null);
                        }
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
