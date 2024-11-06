using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebSite.
	/// </summary>
	public class WebPage : ServiceArgument
	{
		public string webSiteCode;
		public string code;
		public string webTemplateCode;
		public string description;
		public string parentWebPageCode;
		public bool group;
		public int indent;
		public int order;
		public int viewOrder;
		public bool protectedPage;
		public DateTime publishedFrom;
		public DateTime publishedTo;
		public int publishedVersionNo;
		public int workingVersionNo;
		public string externalPageLink;
		public int windowMode;

		private WebPage firstChild;
		private WebTemplate webTemplate;
        //private WebPageMenuText webPageMenuText;

        private Infojet infojetContext;

        //private string _url;
        private Link _urlLink;


        public WebPage() { }

        public WebPage(Infojet infojetContext, string webSiteCode, string code)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
			this.webSiteCode = webSiteCode;
			this.code = code;

            if (code == null)
            {
                System.Web.HttpContext.Current.Response.StatusCode = 404;                
                System.Web.HttpContext.Current.Response.SuppressContent = true;
                //System.Web.HttpContext.Current.Response.Status = "The page could not be found.";
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                System.Web.HttpContext.Current.Response.End();
            }

			getFromDatabase();
		}

        public WebPage(Infojet infojetContext, SqlDataReader dataReader)
		{
            this.infojetContext = infojetContext;

			readFromSql(dataReader);
		}

		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Code], [Web Template Code], [Description], [Parent Web Page Code], [Group], [Indent], [Order], [View Order], [Protected], [Published From], [Published To], [Published Version No_], [Working Version No_], [External Page Link], [Window Mode], (SELECT m.[Menu Text] FROM [" + infojetContext.systemDatabase.getTableName("Web Page Menu Text") + "] m WHERE m.[Web Site Code] = p.[Web Site Code] AND m.[Web Page Code] = p.[Code] AND m.[Language Code] = @languageCode) as menuText FROM [" + infojetContext.systemDatabase.getTableName("Web Page") + "] p WHERE [Web Site Code] = @webSiteCode AND [Code] = @code");
            databaseQuery.addStringParameter("code", code.ToUpper(), 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode.ToUpper(), 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{
				readFromSql(dataReader);
			}

			dataReader.Close();
			
		}

		private void readFromSql(SqlDataReader dataReader)
		{
			group = false;
			protectedPage = false;

			webSiteCode = dataReader.GetValue(0).ToString();
			code = dataReader.GetValue(1).ToString();
			webTemplateCode = dataReader.GetValue(2).ToString();
			description = dataReader.GetValue(3).ToString();
			parentWebPageCode = dataReader.GetValue(4).ToString();
			if (dataReader.GetValue(5).ToString() == "1") group = true;
			indent = dataReader.GetInt32(6);
			order = dataReader.GetInt32(7);
			viewOrder = dataReader.GetInt32(8);
			if (dataReader.GetValue(9).ToString() == "1") protectedPage = true;
			publishedFrom = dataReader.GetDateTime(10);
			publishedTo = dataReader.GetDateTime(11);
			publishedVersionNo = dataReader.GetInt32(12);
			workingVersionNo = dataReader.GetInt32(13);
			externalPageLink = dataReader.GetValue(14).ToString();
			this.windowMode = dataReader.GetInt32(15);
            if (!dataReader.IsDBNull(16)) description = dataReader.GetValue(16).ToString();
		}

        /*
		public string getUrl()
		{
            if ((_url != null) && (_url != "")) return _url;

			if (group)
			{
				if (firstChild == null)
				{
					firstChild = getFirstChild();

				}
                _url = firstChild.getUrl();
                return _url;
			}
			else
			{
                if (webTemplate == null)
                {
                    webTemplate = new WebTemplate(infojetContext.systemDatabase, this.webSiteCode, this.webTemplateCode);

                }

                WebSite webSite = new WebSite(infojetContext, this.webSiteCode);

                if (infojetContext.configuration.webSiteCode == "")
                {
                    _url = webSite.location + webTemplate.filename + "?webSiteCode=" + this.webSiteCode.ToLower() + "&pageCode=" + this.code.ToLower();
                    if (webSite.fancyLinks) _url = webSite.siteLocation+webSite.shortcutName+"/"+"[languageCulture]"+"/"+this.description.ToLower();
                }
                else
                {
                    _url = webSite.location + webTemplate.filename + "?pageCode=" + this.code.ToLower();
                    if (webSite.fancyLinks) _url = webSite.siteLocation + "[languageCulture]" + "/" + this.description.ToLower();
                }
                return _url;
			}
		}
        */

        public string getUrl()
        {
            Link link = getUrlLink();
            return link.toUrl();
        }

        public Link getUrlLink()
        {
            if ((_urlLink != null) && (_urlLink.languageCulture == infojetContext.language.cultureValue)) return _urlLink;

            if (group)
            {
                if (firstChild == null)
                {
                    firstChild = getFirstChild();

                }
                _urlLink = firstChild.getUrlLink();
                return _urlLink;
            }
            else
            {
                if (webTemplate == null)
                {
                    webTemplate = new WebTemplate(infojetContext.systemDatabase, this.webSiteCode, this.webTemplateCode);

                }

                WebSite webSite = new WebSite(infojetContext, this.webSiteCode);

                Link link = new Link(infojetContext, this, webTemplate.filename);
                return link;
            }
        }


		public WebPage getFirstChild()
		{
			WebPage webPage = null;

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Code], [Web Template Code], [Description], [Parent Web Page Code], [Group], [Indent], [Order], [View Order], [Protected], [Published From], [Published To], [Published Version No_], [Working Version No_], [External Page Link], [Window Mode], (SELECT m.[Menu Text] FROM [" + infojetContext.systemDatabase.getTableName("Web Page Menu Text") + "] m WHERE m.[Web Site Code] = p.[Web Site Code] AND m.[Web Page Code] = p.[Code] AND m.[Language Code] = @languageCode) as menuText FROM [" + infojetContext.systemDatabase.getTableName("Web Page") + "] p WHERE [Web Site Code] = @webSiteCode AND [Parent Web Page Code] = @code AND [Published Version No_] > 0 ORDER BY [Order]");
            databaseQuery.addStringParameter("code", code, 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("languageCode", infojetContext.languageCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{
                webPage = new WebPage(infojetContext, dataReader);
			}

			dataReader.Close();
			
			return webPage;
		}


		public bool checkSecurity(UserSession userSession)
		{
			bool accepted = false;

			if ((userSession != null) && (userSession.webUserAccount != null))
			{

                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Page") + "] p, [" + infojetContext.systemDatabase.getTableName("Web Page User Group") + "] wpg, [" + infojetContext.systemDatabase.getTableName("Web User Account Group") + "] wug, [" + infojetContext.systemDatabase.getTableName("Web User Account Relation") + "] wur WHERE p.[Code] = @code AND p.[Web Site Code] = @webSiteCode AND p.Code = wpg.[Web Page Code] AND p.[Web Site Code] = wpg.[Web Site Code] AND wpg.[Web User Group Code] = wug.[Web User Group Code] AND wug.[No_] = @webUserAccountNo AND wug.[No_] = wur.[No_] AND wur.[Web Site Code] = @webSiteCode AND ((p.[Published From] <= GETDATE() AND p.[Published To] >= GETDATE()) OR (p.[Published From] = '1753-01-01' AND p.[Published To] = '1753-01-01') OR (p.[Published From] <= GETDATE() AND p.[Published To] = '1753-01-01')) AND p.[Published Version No_] > 0 ORDER BY p.[Order]");
                databaseQuery.addStringParameter("code", code, 20);
                databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
                databaseQuery.addStringParameter("webUserAccountNo", infojetContext.userSession.webUserAccount.no, 20);

                SqlDataReader dataReader = databaseQuery.executeQuery();
				if (dataReader.Read())
				{
					accepted = true;
				}
		
				dataReader.Close();

			}
			else
			{
                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Page") + "] p WHERE p.[Code] = @code AND p.[Web Site Code] = @webSiteCode AND p.[Protected] = '0' AND ((p.[Published From] <= GETDATE() AND p.[Published To] >= GETDATE()) OR (p.[Published From] = '1753-01-01' AND p.[Published To] = '1753-01-01') OR (p.[Published From] <= GETDATE() AND p.[Published To] = '1753-01-01')) AND p.[Published Version No_] > 0 ORDER BY p.[Order]");
                databaseQuery.addStringParameter("code", code, 20);
                databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);

                SqlDataReader dataReader = databaseQuery.executeQuery();
				if (dataReader.Read())
				{
					accepted = true;
				}

				dataReader.Close();

			}
			return accepted;
		}

		public WebPageMenuText getMenuText(string languageCode)
		{
            WebPageMenuText webPageMenuText = new WebPageMenuText(infojetContext, webSiteCode, code, languageCode);
            return webPageMenuText;

		}

		public DateTime getUpdatedDate()
		{
			if (this.publishedFrom.Year > 1753)
			{
				return this.publishedFrom;
			}
			else
			{
				DateTime dateTime = new DateTime(1753, 01, 01, 00, 00, 00);

                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Posting Date] FROM [" + infojetContext.systemDatabase.getTableName("Web Page Version") + "] WHERE [Web Page Code] = @code AND [Web Site Code] = @webSiteCode AND [Version No_] = @publishedVersionNo");
                databaseQuery.addStringParameter("code", code, 20);
                databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
                databaseQuery.addIntParameter("publishedVersionNo", publishedVersionNo);

                SqlDataReader sqlDataReader = databaseQuery.executeQuery();
				if (sqlDataReader.Read())
				{
					dateTime = sqlDataReader.GetDateTime(0);
				}

				sqlDataReader.Close();

				return dateTime;	
			}
		}

        public string getTitle()
        {
            string title = infojetContext.translate("TITLE");

            WebPageMenuText webMenuText = this.getMenuText(infojetContext.languageCode);

            if ((webMenuText != null) && (webMenuText.menuText != ""))
            {
                if (title != "")
                {
                    if (title.Substring(0, 2) != " |") title = " | " + title;
                }
                title = webMenuText.menuText + title;
            }
            

            if (System.Web.HttpContext.Current.Request["category"] != null)
            {
                if (title != "")
                {
                    if (title.Substring(0, 2) != " |") title = " | " + title;
                }
                WebItemCategory webItemCategory = new WebItemCategory(infojetContext, System.Web.HttpContext.Current.Request["category"]);
                WebItemCategoryTranslation translation = webItemCategory.getTranslation();
                title = translation.description + title;
            }

            if (System.Web.HttpContext.Current.Request["newsEntryNo"] != null)
            {
                if (title != "")
                {
                    if (title.Substring(0, 2) != " |") title = " | " + title;
                }
                WebNewsEntry webNewsEntry = new WebNewsEntry(infojetContext, System.Web.HttpContext.Current.Request["newsEntryNo"]);
                title = webNewsEntry.header + title;
            }

            if (System.Web.HttpContext.Current.Request["webModelNo"] != null)
            {

                if (title != "")
                {
                    if (title.Substring(0, 2) != " |") title = " | " + title;
                }
                WebModel webModel = new WebModel(infojetContext, System.Web.HttpContext.Current.Request["webModelNo"]);
                WebModelTranslation webModelTranslation = webModel.getTranslation(infojetContext.languageCode);
                title = webModelTranslation.description + title;
            }

            if (System.Web.HttpContext.Current.Request["itemNo"] != null)
            {
                if (title != "")
                {
                    if (title.Substring(0, 2) != " |") title = " | " + title;
                }

                //Item item = new Item(infojetContext, System.Web.HttpContext.Current.Request["itemNo"]);
                Item item = Item.get(infojetContext, System.Web.HttpContext.Current.Request["itemNo"]);

                ItemTranslation itemTranslation = item.getItemTranslation(infojetContext.languageCode);
                title = itemTranslation.description + title;
            }


            return System.Text.RegularExpressions.Regex.Replace(title, "<(.|\\n)*?>", string.Empty);

            //return title;
        }

        public static WebPage[] getWebPageArray(Infojet infojetContext, string webSiteCode)
        {

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Code], [Web Template Code], [Description], [Parent Web Page Code], [Group], [Indent], [Order], [View Order], [Protected], [Published From], [Published To], [Published Version No_], [Working Version No_], [External Page Link], [Window Mode], [Description] as menuText FROM [" + infojetContext.systemDatabase.getTableName("Web Page") + "] p WHERE [Web Site Code] = @webSiteCode ORDER BY [Order]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode.ToUpper(), 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();


            int i = 0;
            while (dataReader.Read())
            {
                i++;
            }

            dataReader.Close();

            WebPage[] webPageArray = new WebPage[i];
            i = 0;

            dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                webPageArray[i] = new WebPage(infojetContext, dataReader);
                i++;
            }

            dataReader.Close();

            return webPageArray;
        }


        #region ServiceArgument Members

        public System.Xml.XmlElement toDOM(System.Xml.XmlDocument xmlDoc)
        {
            XmlElement webPageElement = xmlDoc.CreateElement("webPage");

            XmlElement webSiteCodeElement = xmlDoc.CreateElement("webSiteCode");
            webSiteCodeElement.AppendChild(xmlDoc.CreateTextNode(webSiteCode));
            webPageElement.AppendChild(webSiteCodeElement);

            XmlElement codeElement = xmlDoc.CreateElement("code");
            codeElement.AppendChild(xmlDoc.CreateTextNode(code));
            webPageElement.AppendChild(codeElement);

            XmlElement descriptionElement = xmlDoc.CreateElement("description");
            descriptionElement.AppendChild(xmlDoc.CreateTextNode(description));
            webPageElement.AppendChild(descriptionElement);

            XmlElement webTemplateCodeElement = xmlDoc.CreateElement("webTemplateCode");
            webTemplateCodeElement.AppendChild(xmlDoc.CreateTextNode(webTemplateCode));
            webPageElement.AppendChild(webTemplateCodeElement);


            return webPageElement;
        }

        #endregion
    }
}
