using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.SessionState;
using System.Collections;

namespace Navipro.Infojet.Lib
{
    public class URLRewriter : IHttpModule
    {
        private Configuration configuration;

        #region IHttpModule Members

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
            context.AuthorizeRequest += new EventHandler(context_AuthorizeRequest);
        }


        void context_BeginRequest(object sender, EventArgs e)
        {
            Rewrite(HttpContext.Current.Request.Path, (HttpApplication)sender);
        }

        void context_AuthorizeRequest(object sender, EventArgs e)
        {

        }

        #endregion

        public void Rewrite(string path, HttpApplication app)
        {
            if (path == "/") return;
            if ((app.Context.Request["pageCode"] != null) && (app.Context.Request["pageCode"] != "")) return;
            if ((app.Context.Request["webSiteCode"] != null) && (app.Context.Request["webSiteCode"] != "")) return;
            if (path.Contains(".")) return;

            string queryString = app.Context.Request.QueryString.ToString();

            configuration = new Configuration();
            if (!configuration.init())
            {
                throw new Exception("Configuration faild to init.");
            }

            Database database = new Database(null, configuration);

            string virtualDir = "";
            if (app.Context.Request.ApplicationPath != "/")
            {
                path = path.Substring(app.Context.Request.ApplicationPath.Length + 1);
                virtualDir = app.Context.Request.ApplicationPath + "/";

            }
            else
            {
                virtualDir = "/";
                path = path.Substring(1);
            }

            string[] parameterList = path.Split('/');
            if (parameterList.Length == 0) return;

            string webSiteCode = "";
            string webPageCode = "";
            string languageCode = "";
            string webItemCategoryCode = "";
            string itemNo = "";
            string itemModelNo = "";
            string webItemCategoryListPage = "";

            webSiteCode = configuration.webSiteCode;

            int nextParameter = 0;
            if (webSiteCode == "")
            {
                webSiteCode = webSiteCode = getWebSite(database, System.Web.HttpUtility.UrlDecode(parameterList[nextParameter]));
                nextParameter++;
            }

            if (parameterList.Length > nextParameter) languageCode = getLanguageCulture(database, webSiteCode, System.Web.HttpUtility.UrlDecode(parameterList[nextParameter]));
            if (languageCode != "") nextParameter++;

            if (parameterList.Length > nextParameter) webPageCode = getWebPage(database, webSiteCode, System.Web.HttpUtility.UrlDecode(parameterList[nextParameter]));
            if (webPageCode != "") nextParameter++;

            if (parameterList.Length > nextParameter) webItemCategoryCode = getWebItemCategory(database, webSiteCode, System.Web.HttpUtility.UrlDecode(parameterList[nextParameter]), out webItemCategoryListPage);
            if (webItemCategoryCode != "") nextParameter++;

            if (parameterList.Length > nextParameter) itemNo = getItemNo(database, webSiteCode, System.Web.HttpUtility.UrlDecode(parameterList[nextParameter]));
            if (itemNo != "")
            {
                itemModelNo = getItemModelNo(database, itemNo);
                nextParameter++;
            }



            if (webPageCode == "")
            {
                if (webItemCategoryCode != "")
                {
                    if ((itemModelNo == "") && (itemNo == "")) webPageCode = webItemCategoryListPage;
                }
                if (itemModelNo != "") webPageCode = getWebItemCategoryInfoPage(database, webSiteCode, webItemCategoryCode, 2, itemModelNo);
                if ((itemModelNo == "") && (itemNo != "")) webPageCode = getWebItemCategoryInfoPage(database, webSiteCode, webItemCategoryCode, 1, itemNo);

                if (webPageCode == "") webPageCode = getStartPage(database, webSiteCode);

            }

            string url = getWebPageUrl(database, webSiteCode, webPageCode, languageCode);
            
            if (webItemCategoryCode != "") url = url + "&category=" + webItemCategoryCode;
            if (itemNo != "") url = url + "&itemNo=" + itemNo;
            if (itemModelNo != "") url = url + "&webModelNo=" + itemModelNo;


            //throw new Exception("Url: " + url + queryString+", " + webSiteCode + ", " + checkRewriteMode(database, webSiteCode).ToString());

            url = url + "&" + queryString;

            if (checkRewriteMode(database, webSiteCode))
            {
                app.Context.RewritePath(virtualDir + url);
            }
            else
            {
                app.Context.Response.Redirect(virtualDir + url);
            }
        }

        private string getWebSite(Database database, string shortcut)
        {
            string webSite = checkCache(database, "WEBSITE#" + shortcut);
            if (webSite != null) return webSite;

            webSite = "";

            DatabaseQuery databaseQuery = database.prepare("SELECT [Code] FROM [" + database.getTableName("Web Site") + "] WHERE "+getReplacementChars("[Shortcut Name]")+" = @webSiteCode");
            databaseQuery.addStringParameter("webSiteCode", shortcut, 20);
            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                webSite = dataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                addCache(database, "WEBSITE#" + shortcut, webSite);
            }
            
            return webSite;
        }

        private string getLanguageCulture(Database database, string webSiteCode, string languageCulture)
        {
            string languageCode = checkCache(database, "CULTURE#" + webSiteCode + "#" + languageCulture);
            if (languageCode != null) return languageCode;

            languageCode = "";

            DatabaseQuery databaseQuery = database.prepare("SELECT [Code] FROM [" + database.getTableName("Web Site Language") + "] WHERE [Web Site Code] = @webSiteCode AND [Culture Value] = @languageCulture");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCulture", languageCulture, 20);
            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                languageCode = dataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                addCache(database, "CULTURE#" + webSiteCode + "#" + languageCulture, languageCode);
            }

            return languageCode;
        }

        private string getWebPage(Database database, string webSiteCode, string shortcut)
        {
            string webPageCode = checkCache(database, "WEBPAGE#" + webSiteCode + "#" + shortcut);
            if (webPageCode != null) return webPageCode;

            webPageCode = "";

            DatabaseQuery databaseQuery = database.prepare("SELECT p.[Code] FROM [" + database.getTableName("Web Page") + "] p LEFT JOIN [" + database.getTableName("Web Page Menu Text") + "] m ON m.[Web Site Code] = p.[Web Site Code] AND p.[Code] = m.[Web Page Code] WHERE p.[Web Site Code] = @webSiteCode AND ("+getReplacementChars("LOWER(m.[Menu Text])")+" = @shortcut OR "+getReplacementChars("LOWER(p.[Description])")+" = @shortcut)");
            databaseQuery.addStringParameter("shortcut", shortcut.ToLower(), 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                webPageCode = dataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                addCache(database, "WEBPAGE#" + webSiteCode + "#" + shortcut, webPageCode);
            }

            return webPageCode;
        }

        private string getWebPageUrl(Database database, string webSiteCode, string webPageCode, string languageCulture)
        {
            string webPageUrl = checkCache(database, "WEBPAGEURL#" + webSiteCode + "#" + webPageCode + "#" + languageCulture);
            if (webPageUrl != null) return webPageUrl;

            webPageUrl = "";

            DatabaseQuery databaseQuery = database.prepare("SELECT t.[Filename], s.[Location], s.[Default Language Code], st.[Location] FROM [" + database.getTableName("Web Site") + "] s LEFT JOIN [" + database.getTableName("Web Style") + "] st ON s.[Web Style Code] = st.[Code], [" + database.getTableName("Web Template") + "] t, [" + database.getTableName("Web Page") + "] p WHERE s.[Code] = p.[Web Site Code] AND p.[Web Site Code] = @webSiteCode AND t.[Web Site Code] = p.[Web Site Code] AND t.[Code] = p.[Web Template Code] AND p.[Code] = @webPageCode");
            databaseQuery.addStringParameter("webPageCode", webPageCode, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);


            if (dataSet.Tables[0].Rows.Count > 0)
            {
                string styleLocation = "";
                if (dataSet.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() != "") styleLocation = "styles/" + dataSet.Tables[0].Rows[0].ItemArray.GetValue(3).ToString() + "/";

                if (languageCulture == "") languageCulture = dataSet.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                webPageUrl = styleLocation+dataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "?webSiteCode=" + webSiteCode + "&pageCode=" + webPageCode + "&languageCode=" + languageCulture;
                addCache(database, "WEBPAGEURL#" + webSiteCode + "#" + webPageCode + "#" + languageCulture, webPageUrl);

            }

            return webPageUrl;
        }

        private string getStartPage(Database database, string webSiteCode)
        {
            string startPage = checkCache(database, "STARTPAGE#" + webSiteCode);
            if (startPage != null) return startPage;

            startPage = "";

            DatabaseQuery databaseQuery = database.prepare("SELECT c.[Web Page Code] FROM [" + database.getTableName("Web Site") + "] s, [" + database.getTableName("Web Page Category") + "] c WHERE c.[Web Site Code] = @webSiteCode AND c.[Web Site Code] = s.[Code] AND s.[Start Page Category Code] = c.[Web Category Code]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                startPage = dataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                addCache(database, "STARTPAGE#" + webSiteCode, startPage);
            }

            return startPage;
        }

        private string getWebItemCategory(Database database, string webSiteCode, string description, out string webItemCategoryListPage)
        {
            string webItemCategory = checkCache(database, "WEBITEMCATEGORY#" + webSiteCode + "#" + description);
            if (webItemCategory != null)
            {
                webItemCategoryListPage = checkCache(database, "WEBITEMCATEGORYLISTPAGE#" + webSiteCode + "#" + description);
                return webItemCategory;
            }

            webItemCategory = "";
            webItemCategoryListPage = "";

            DatabaseQuery databaseQuery = database.prepare("SELECT c.[Code], c.[Item List Web Page Code] FROM [" + database.getTableName("Web Item Category Translation") + "] t, [" + database.getTableName("Web Item Category") + "] c WHERE t.[Web Site Code] = @webSiteCode AND "+getReplacementChars("LOWER(t.[Description])")+" = @description AND c.[Code] = t.[Web Item Category Code] AND c.[Web Site Code] = t.[Web Site Code]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("description", description.ToLower(), 100);
            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                webItemCategoryListPage = dataSet.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                webItemCategory = dataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                addCache(database, "WEBITEMCATEGORY#" + webSiteCode + "#" + description, webItemCategory);
                addCache(database, "WEBITEMCATEGORYLISTPAGE#" + webSiteCode + "#" + description, webItemCategoryListPage);

            }

            return webItemCategory;
        }

        private string getWebItemCategoryInfoPage(Database database, string webSiteCode, string webItemCategoryCode, int type, string code)
        {

            string categoryQuery = "";
            if (webItemCategoryCode != "") categoryQuery = "AND c.[Code] = @webItemCategoryCode";

            DatabaseQuery databaseQuery = database.prepare("SELECT c.[Item Info Web Page Code], m.[Item Info Web Page Code] FROM [" + database.getTableName("Web Item Category") + "] c LEFT JOIN [" + database.getTableName("Web Item Category Member") + "] m ON c.[Web Site Code] = m.[Web Site Code] AND c.[Code] = m.[Web Item Category Code] WHERE c.[Web Site Code] = @webSiteCode "+categoryQuery+" AND m.[Type] = @type AND m.[Code] = @code");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("webItemCategoryCode", webItemCategoryCode, 20);
            databaseQuery.addIntParameter("type", type);
            databaseQuery.addStringParameter("code", code, 20);
            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                string webPageCode = dataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (dataSet.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "") webPageCode = dataSet.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                return webPageCode;
            }

            return "";
        }

        private string getItemNo(Database database, string webSiteCode, string description)
        {
            string itemNo = checkCache(database, "ITEM#" + webSiteCode + "#" + description);
            if (itemNo != null) return itemNo;

            itemNo = "";


            //throw new Exception("SELECT i.[No_] FROM [" + database.getTableName("Item") + "] i LEFT JOIN [" + database.getTableName("Item Translation") + "] t ON t.[Item No_] = i.[No_], [" + database.getTableName("Web Item Category Member") + "] c WHERE (LOWER(t.[Description]) = '" + description.ToLower()+ "' OR LOWER(i.[Description]) = '" + description.ToLower() + "') AND c.[Type] = 0 AND c.[Code] = i.[No_] AND c.[Web Site Code] = '"+webSiteCode+"'");

            DatabaseQuery databaseQuery = database.prepare("SELECT i.[No_] FROM [" + database.getTableName("Item") + "] i LEFT JOIN [" + database.getTableName("Item Translation") + "] t ON t.[Item No_] = i.[No_], [" + database.getTableName("Web Item Category Member") + "] c WHERE (" + getReplacementChars("LOWER(t.[Description])") + " = @description OR " + getReplacementChars("LOWER(t.[Description])") + " = @description) AND c.[Type] = 1 AND c.[Code] = i.[No_] AND c.[Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("description", description.ToLower(), 100);
            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                itemNo = dataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                addCache(database, "ITEM#" + webSiteCode + "#" + description, itemNo);
            }

            return itemNo;
        }

        private string getItemModelNo(Database database, string itemNo)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Model No_] FROM [" + database.getTableName("Web Model Variant") + "] WHERE [Item No_] = @itemNo");
            databaseQuery.addStringParameter("itemNo", itemNo, 20);
            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                return dataSet.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }

            return "";
        }

        private bool checkRewriteMode(Database database, string webSiteCode)
        {
            if (configuration.fancyLinks) return true;

            DatabaseQuery databaseQuery = database.prepare("SELECT [Fancy Links] FROM [" + database.getTableName("Web Site") + "] WHERE [Code] = @webSiteCode AND [Fancy Links] = 1");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }

            return false;
        }

        private string checkCache(Database database, string key)
        {
            object cache = WebApplicationCache.getCashedObject(database, key);
            if (cache == null) return null;

            return (string)cache;
            
            /*
            Hashtable cacheTable = (Hashtable)System.Web.HttpContext.Current.Application["urlRewriterCache"];
            if (cacheTable == null) return null;

            if (cacheTable.Contains(key))
            {
                return (string)cacheTable[key];
            }
            return null;
            */
        }

        private void addCache(Database database, string key, string value)
        {
            WebApplicationCache.cacheObject(database, key, value);

            /*
            Hashtable cacheTable = (Hashtable)System.Web.HttpContext.Current.Application["urlRewriterCache"];
            if (cacheTable == null) cacheTable = new Hashtable();

            try
            {
                cacheTable.Add(key, value);
            }
            catch (Exception) { }

            if (System.Web.HttpContext.Current.Application["urlRewriterCache"] == null)
            {
                System.Web.HttpContext.Current.Application.Add("urlRewriterCache", cacheTable);
            }
            else
            {
                System.Web.HttpContext.Current.Application["urlRewriterCache"] = cacheTable;
            }
            */
        }

        private string getReplacementChars(string fieldName)
        {
            return "REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE("+fieldName+", 'å', 'a'), 'ä', 'a'), 'ö', 'o'), ' ', '-'), '/', '-'), '&', '-')";
        }
    }
}
