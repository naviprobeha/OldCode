using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebSite.
	/// </summary>
	public class WebPage
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

        private Infojet infojetContext;

        public WebPage(Infojet infojetContext, string webSiteCode, string code)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
			this.webSiteCode = webSiteCode;
			this.code = code;

			getFromDatabase();
		}

        public WebPage(Infojet infojetContext, SqlDataReader dataReader)
		{
            this.infojetContext = infojetContext;

			readFromSql(dataReader);
		}

		private void getFromDatabase()
		{

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Site Code], [Code], [Web Template Code], [Description], [Parent Web Page Code], [Group], [Indent], [Order], [View Order], [Protected], [Published From], [Published To], [Published Version No_], [Working Version No_], [External Page Link], [Window Mode] FROM [" + infojetContext.systemDatabase.getTableName("Web Page") + "] WHERE [Web Site Code] = @webSiteCode AND [Code] = @code");
            databaseQuery.addStringParameter("code", code.ToUpper(), 20);
            databaseQuery.addStringParameter("webSiteCode", webSiteCode.ToUpper(), 20);

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
		}

		public string getUrl()
		{
			if (group)
			{
				if (firstChild == null)
				{
					firstChild = getFirstChild();

				}
				return firstChild.getUrl();
			}
			else
			{
                if (webTemplate == null)
                {
                    webTemplate = new WebTemplate(infojetContext.systemDatabase, this.webSiteCode, this.webTemplateCode);

                }

                WebSite webSite = new WebSite(infojetContext, this.webSiteCode);

				return webSite.location+ webTemplate.filename+"?pageCode="+this.code;
			}
		}

		public WebPage getFirstChild()
		{
			WebPage webPage = null;

            SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT [Web Site Code], [Code], [Web Template Code], [Description], [Parent Web Page Code], [Group], [Indent], [Order], [View Order], [Protected], [Published From], [Published To], [Published Version No_], [Working Version No_], [External Page Link], [Window Mode] FROM [" + infojetContext.systemDatabase.getTableName("Web Page") + "] WHERE [Web Site Code] = '" + this.webSiteCode + "' AND [Parent Web Page Code] = '" + this.code + "' AND [Published Version No_] > 0 ORDER BY [Order]");
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

                SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT [Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Page") + "] p, [" + infojetContext.systemDatabase.getTableName("Web Page User Group") + "] wpg, [" + infojetContext.systemDatabase.getTableName("Web User Account Group") + "] wug, [" + infojetContext.systemDatabase.getTableName("Web User Account Relation") + "] wur WHERE p.[Code] = '" + this.code + "' AND p.[Web Site Code] = '" + this.webSiteCode + "' AND p.Code = wpg.[Web Page Code] AND p.[Web Site Code] = wpg.[Web Site Code] AND wpg.[Web User Group Code] = wug.[Web User Group Code] AND wug.[No_] = '" + userSession.webUserAccount.no + "' AND wug.[No_] = wur.[No_] AND wur.[Web Site Code] = '" + this.webSiteCode + "' AND ((p.[Published From] <= GETDATE() AND p.[Published To] >= GETDATE()) OR (p.[Published From] = '1753-01-01' AND p.[Published To] = '1753-01-01') OR (p.[Published From] <= GETDATE() AND p.[Published To] = '1753-01-01')) AND p.[Published Version No_] > 0 ORDER BY p.[Order]");
				if (dataReader.Read())
				{
					accepted = true;
				}
		
				dataReader.Close();

			}
			else
			{
                SqlDataReader dataReader = infojetContext.systemDatabase.query("SELECT [Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Page") + "] p WHERE p.[Code] = '" + this.code + "' AND p.[Web Site Code] = '" + this.webSiteCode + "' AND p.[Protected] = '0' AND ((p.[Published From] <= GETDATE() AND p.[Published To] >= GETDATE()) OR (p.[Published From] = '1753-01-01' AND p.[Published To] = '1753-01-01') OR (p.[Published From] <= GETDATE() AND p.[Published To] = '1753-01-01')) AND p.[Published Version No_] > 0 ORDER BY p.[Order]");
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

                SqlDataReader sqlDataReader = infojetContext.systemDatabase.query("SELECT [Posting Date] FROM [" + infojetContext.systemDatabase.getTableName("Web Page Version") + "] WHERE [Web Page Code] = '" + this.code + "' AND [Web Site Code] = '" + this.webSiteCode + "' AND [Version No_] = '" + this.publishedVersionNo + "'");
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
            WebPageMenuText webMenuText = this.getMenuText(infojetContext.languageCode);
            string title = webMenuText.menuText;

            if (System.Web.HttpContext.Current.Request["newsEntryNo"] != null)
            {
                if (title != "") title = title + " - ";
                WebNewsEntry webNewsEntry = new WebNewsEntry(infojetContext, System.Web.HttpContext.Current.Request["newsEntryNo"]);
                title = title + webNewsEntry.header;
            }

            if (System.Web.HttpContext.Current.Request["itemNo"] != null)
            {
                if (title != "") title = title + " - ";
                Item item = new Item(infojetContext.systemDatabase, System.Web.HttpContext.Current.Request["itemNo"]);
                ItemTranslation itemTranslation = item.getItemTranslation(infojetContext.languageCode);
                title = title + itemTranslation.description;
            }

            if (System.Web.HttpContext.Current.Request["webModelNo"] != null)
            {
                if (title != "") title = title + " - ";
                WebModel webModel = new WebModel(infojetContext, System.Web.HttpContext.Current.Request["webModelNo"]);
                WebModelTranslation webModelTranslation = webModel.getTranslation(infojetContext.languageCode);
                title = title + webModelTranslation.description;
            }

            return title;
        }
	}
}
