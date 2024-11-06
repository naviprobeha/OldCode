using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;


namespace Navipro.Infojet.Lib
{
    public class Sitemap
    {
        public Sitemap()
        {
        }

        public XmlDocument getDocument(Infojet infojetContext)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"/>");

            XmlElement docElement = xmlDoc.DocumentElement;

            Hashtable webPageModify = new Hashtable();
            
            //Web Pages
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT p.[Code], v.[Posting Date] FROM ["+infojetContext.systemDatabase.getTableName("Web Page")+"] p, ["+infojetContext.systemDatabase.getTableName("Web Page Version")+"] v WHERE p.[Code] = v.[Web Page Code] AND p.[Web Site Code] = v.[Web Site Code] AND v.[Version No_] = p.[Published Version No_] AND p.[Web Site Code] = @webSiteCode AND p.[Protected] = 0");
            databaseQuery.addStringParameter("webSiteCode", infojetContext.webSite.code, 20);
            System.Data.SqlClient.SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                webPageModify.Add(dataReader.GetValue(0).ToString(), dataReader.GetDateTime(1));
            }
            dataReader.Close();

            //Menu
            databaseQuery = infojetContext.systemDatabase.prepare("SELECT DISTINCT l.[Code] FROM ["+infojetContext.systemDatabase.getTableName("Web Page Line")+"] l, ["+infojetContext.systemDatabase.getTableName("Web Page")+"] p WHERE l.[Web Page Code] = p.[Code] AND l.[Web Site Code] = p.[Web Site Code] AND l.[Version No_] = p.[Published Version No_] AND l.[Type] = 2 AND p.[Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webSiteCode", infojetContext.webSite.code, 20);

            System.Data.SqlClient.SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataAdapter.Fill(dataSet);

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                string menu = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                WebMenu webMenu = new WebMenu(infojetContext, menu);
                NavigationItemCollection navigationItemCollection = webMenu.getMenuItems(infojetContext);

                renderNavigationItemCollection(ref xmlDoc, ref docElement, navigationItemCollection, "monthly");

                i++;
            }

            //Product Categories
            databaseQuery = infojetContext.systemDatabase.prepare("SELECT DISTINCT l.[Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Page Line") + "] l, [" + infojetContext.systemDatabase.getTableName("Web Page") + "] p WHERE l.[Web Page Code] = p.[Code] AND l.[Web Site Code] = p.[Web Site Code] AND l.[Version No_] = p.[Published Version No_] AND l.[Type] = 4 AND p.[Web Site Code] = @webSiteCode AND p.[Protected] = 0");
            databaseQuery.addStringParameter("webSiteCode", infojetContext.webSite.code, 20);

            dataAdapter = databaseQuery.executeDataAdapterQuery();
            dataSet = new System.Data.DataSet();
            dataAdapter.Fill(dataSet);

            i = 0;

            while (i < dataSet.Tables[0].Rows.Count)
            {
                string categoryCode = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();

                WebItemCategory webItemCategory = new WebItemCategory(infojetContext, categoryCode);
                NavigationItemCollection navigationItemCollection = webItemCategory.getProductCategoryTree();

                renderNavigationItemCollection(ref xmlDoc, ref docElement, navigationItemCollection, "daily");

                i++;
            }

            return xmlDoc;
        }

        private void renderNavigationItemCollection(ref XmlDocument xmlDoc, ref XmlElement urlSetElement, NavigationItemCollection navigationItemCollection, string freq)
        {
            int j = 0;
            while (j < navigationItemCollection.Count)
            {
                NavigationItem navigationItem = navigationItemCollection[j];

                //DateTime lastDateModified = (DateTime)webPageModify[navigationItem.code];
                DateTime lastDateModified = DateTime.Today.AddDays(-3);
                //if (lastDateModified == null) lastDateModified = DateTime.Today;

                addUrl(ref xmlDoc, ref urlSetElement, navigationItem.link, lastDateModified, freq, "0.5");

                if (navigationItem.subNavigationItems != null) renderNavigationItemCollection(ref xmlDoc, ref urlSetElement, navigationItem.subNavigationItems, freq);

                j++;
            }


        }

        private XmlElement addUrl(ref XmlDocument xmlDoc, ref XmlElement urlSetElement, string location, DateTime lastModified, string changeFreq, string priority)
        {
            XmlElement urlElement = xmlDoc.CreateElement("url", "http://www.sitemaps.org/schemas/sitemap/0.9");
            XmlElement locationElement = xmlDoc.CreateElement("loc", "http://www.sitemaps.org/schemas/sitemap/0.9");
            XmlElement lastModElement = xmlDoc.CreateElement("lastmod", "http://www.sitemaps.org/schemas/sitemap/0.9");
            XmlElement changeFreqElement = xmlDoc.CreateElement("changefreq", "http://www.sitemaps.org/schemas/sitemap/0.9");
            XmlElement priorityElement = xmlDoc.CreateElement("priority", "http://www.sitemaps.org/schemas/sitemap/0.9");

            XmlText locationText = xmlDoc.CreateTextNode(location);
            locationElement.AppendChild(locationText);

            XmlText lastModText = xmlDoc.CreateTextNode(lastModified.ToString("yyyy-MM-dd"));
            lastModElement.AppendChild(lastModText);

            XmlText changeFreqText = xmlDoc.CreateTextNode(changeFreq);
            changeFreqElement.AppendChild(changeFreqText);

            XmlText priorityText = xmlDoc.CreateTextNode(priority);
            priorityElement.AppendChild(priorityText);

            urlElement.AppendChild(locationElement);
            urlElement.AppendChild(lastModElement);
            urlElement.AppendChild(changeFreqElement);
            urlElement.AppendChild(priorityElement);

            urlSetElement.AppendChild(urlElement);

            return urlElement;

        }

    }
}
