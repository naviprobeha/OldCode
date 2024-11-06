using System;
using System.Web;
using System.Data;
using System.IO;
using System.Collections;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Infojet.
	/// </summary>
	public class Infojet
	{

		private Database database;

		public SystemHandler systemHandler;
        private ContentHandler contentHandler;
        private GeneralLedgerSetup glSetup;

		private string currentPageCode;

		public Infojet()
		{
			//
			// TODO: Add constructor logic here
			//

			systemHandler = new SystemHandler(this);
            contentHandler = new ContentHandler(this);

			System.Web.HttpContext.Current.Response.Expires = 0;

            database = (Database)System.Web.HttpContext.Current.Session["database"];
		}

		public string languageCode { get { return (string)System.Web.HttpContext.Current.Session["languageCode"]; } }
		public WebSiteLanguage language { get { return new WebSiteLanguage(systemDatabase, webSite.code, (string)System.Web.HttpContext.Current.Session["languageCode"]); } }
        public int versionNo { get { return (int)System.Web.HttpContext.Current.Session["versionNo"]; } set { System.Web.HttpContext.Current.Session["versionNo"] = value; } }
		public Configuration configuration { get { return (Configuration)System.Web.HttpContext.Current.Session["configuration"]; } }	
		public WebSite webSite { get { return (WebSite)System.Web.HttpContext.Current.Session["webSite"]; } }
		public WebPage webPage { get { return (WebPage)System.Web.HttpContext.Current.Session["webPage"]; } }
		public CartHandler cartHandler { get { return (CartHandler)System.Web.HttpContext.Current.Session["cartHandler"]; }	}
		public string sessionId { get { return System.Web.HttpContext.Current.Session.SessionID; } }
		public string currencyCode { get { return (string)System.Web.HttpContext.Current.Session["currencyCode"]; } }
        public UserSession userSession { get { return (UserSession)System.Web.HttpContext.Current.Session["currentUserSession"]; } }
        public Database systemDatabase { get { return (Database)System.Web.HttpContext.Current.Session["database"]; } }
        
        public GeneralLedgerSetup generalLedgerSetup 
        { 
            get 
            {
                if (glSetup == null) glSetup = new GeneralLedgerSetup(database);
                return this.glSetup;
            }
        }

        public string presentationCurrencyCode
        {
            get
            {
                if ((string)System.Web.HttpContext.Current.Session["currencyCode"] == "")
                {
                    return generalLedgerSetup.lcyCode;
                }
                else
                {
                    return (string)System.Web.HttpContext.Current.Session["currencyCode"];
                }

            }
        }




		public void init()
		{



			Configuration configuration = new Configuration();
			if (!configuration.init())
			{
				throw new Exception("Configuration faild to init.");
			}

			database = new Database(null, configuration);
            System.Web.HttpContext.Current.Session.Add("database", database);

			WebSite webSite = new WebSite(this, configuration.webSiteCode);
			CartHandler cartHandler = new CartHandler(database, System.Web.HttpContext.Current.Session.SessionID, this);

			System.Web.HttpContext.Current.Session.Add("configuration", configuration);		
			System.Web.HttpContext.Current.Session.Add("webSite", webSite);
			System.Web.HttpContext.Current.Session.Add("cartHandler", cartHandler);
			System.Web.HttpContext.Current.Session.Add("currencyCode", "");

			if (System.Web.HttpContext.Current.Request["languageCode"] != null) System.Web.HttpContext.Current.Session.Add("languageCode", System.Web.HttpContext.Current.Request["languageCode"]);
			if ((System.Web.HttpContext.Current.Request["pageCode"] != null) && (System.Web.HttpContext.Current.Request["pageCode"] != ""))	this.currentPageCode = System.Web.HttpContext.Current.Request["pageCode"];

            if ((System.Web.HttpContext.Current.Request.IsAuthenticated) && (System.Web.HttpContext.Current.Session["currentUserSession"] == null))
            {
                systemHandler.createSession(this);
                authenticationRedirect();
            }

            if ((!System.Web.HttpContext.Current.Request.IsAuthenticated) && (System.Web.HttpContext.Current.Session["currentUserSession"] != null))
            {
                //System.Web.HttpContext.Current.Session.Remove("currentUserSession");
                //redirect(webSite.getStartPageUrl());
            }


			if ((currentPageCode == null) || (currentPageCode == ""))
			{
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated) authenticationRedirect();
                
				redirect(webSite.getStartPageUrl());
			}
		}


		public void setPage(string pageCode)
		{
			this.currentPageCode = pageCode;
		}


		public void loadPage()
		{

			checkLanguage();

			systemHandler.handleSystemRequests();


			if ((System.Web.HttpContext.Current.Request["pageCode"] != null) && (System.Web.HttpContext.Current.Request["pageCode"] != ""))	this.currentPageCode = System.Web.HttpContext.Current.Request["pageCode"];

			if ((currentPageCode != null) && (currentPageCode != ""))
			{
                WebPage currentWebPage = new WebPage(this, webSite.code, currentPageCode);
				System.Web.HttpContext.Current.Session.Add("webPage", currentWebPage);

				if ((!currentWebPage.checkSecurity(userSession)) && (System.Web.HttpContext.Current.Request["securityHash"] == null))
				{
					if (System.Web.HttpContext.Current.Session["targetWebPageCode"] != null)
					{
                        WebPage targetWebPage = new WebPage(this, webSite.code, (string)System.Web.HttpContext.Current.Session["targetWebPageCode"]);
						redirect(targetWebPage.getUrl());
					}
					else
					{
						redirect(webSite.getStartPageUrl());
					}
				}


				if (currentWebPage.group)
				{
					WebPage firstChild = currentWebPage.getFirstChild();
					if (firstChild != null)
					{
						redirect(firstChild.getUrl());
					}
					System.Web.HttpContext.Current.Response.End();
				}

				if (currentWebPage.externalPageLink != "")
				{
					redirect(currentWebPage.externalPageLink);
				}

				checkVersion();

				cartHandler.handleRequests();
                contentHandler.handleRequests();
				return;
			}

			System.Web.HttpContext.Current.Response.Redirect(webSite.getStartPageUrl(), true);

		}


		private void checkLanguage()
		{
			string currentLanguageCode = (string)System.Web.HttpContext.Current.Session["languageCode"];
			if (currentLanguageCode == null) currentLanguageCode = webSite.defaultLanguageCode;
			if ((System.Web.HttpContext.Current.Request["languageCode"] != null) && (System.Web.HttpContext.Current.Request["languageCode"] != "")) currentLanguageCode = System.Web.HttpContext.Current.Request["languageCode"];
			
			WebSiteLanguage webSiteLanguage = new WebSiteLanguage(database, webSite.code, currentLanguageCode);
			currentLanguageCode = webSiteLanguage.languageCode;
			string currentCurrencyCode = webSiteLanguage.currencyCode;

            if ((webSiteLanguage.cultureValue != "") && (webSiteLanguage.cultureValue != null))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(webSiteLanguage.cultureValue);
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(webSiteLanguage.cultureValue);
            }
			

			if (this.userSession != null)
			{				
				if (this.userSession.customer.currencyCode != "") currentCurrencyCode = this.userSession.customer.currencyCode;

                if (System.Web.HttpContext.Current.Session["languageCode"] != null)
                {
                    if (currentLanguageCode != System.Web.HttpContext.Current.Session["languageCode"].ToString())
                    {
                        cartHandler.reCalculateCart();
                    }
                }
			}

			System.Web.HttpContext.Current.Session["currencyCode"] = currentCurrencyCode;
			System.Web.HttpContext.Current.Session["languageCode"] = currentLanguageCode;

		}

		private void checkVersion()
		{
			int currentVersionNo = webPage.publishedVersionNo;

			if ((System.Web.HttpContext.Current.Request["versionNo"] != null) && (System.Web.HttpContext.Current.Request["versionNo"] != "")) 
			{
				currentVersionNo = int.Parse(System.Web.HttpContext.Current.Request["versionNo"]);
			}

            System.Web.HttpContext.Current.Session["versionNo"] = currentVersionNo;
		}

        public ArrayList getContent(string webTemplatePartCode)
        {
            return getContent(webPage.code, webTemplatePartCode);
        }

        public ArrayList getContent(string webPageCode, string webTemplatePartCode)
        {
            Database database = (Database)System.Web.HttpContext.Current.Session["database"];

            webTemplatePartCode = webTemplatePartCode.ToUpper();

            ArrayList webPageLineArray = new ArrayList();

            WebPageLines webPageLines = new WebPageLines(database);
            WebPage specificWebPage = new WebPage(this, webSite.code, webPageCode);

            DataSet webPageLineDataSet;

            //System.Web.HttpContext.Current.Response.Write("Get Content: " + webPageCode+" "+webPage.code+" "+this.versionNo);
            if (specificWebPage.code != webPage.code)
			{
                webPageLineDataSet = webPageLines.getWebPageLines(webSite.code, specificWebPage.code, specificWebPage.publishedVersionNo, webTemplatePartCode);
			}
			else
			{
                webPageLineDataSet = webPageLines.getWebPageLines(webSite.code, specificWebPage.code, versionNo, webTemplatePartCode);
			}
			
			
			int i = 0;
            while (i < webPageLineDataSet.Tables[0].Rows.Count)
            {
                WebPageLine webPageLine = new WebPageLine(this, webPageLineDataSet.Tables[0].Rows[i]);

                webPageLineArray.Add(webPageLine);
                i++;
            }

            return webPageLineArray;

        }

		public void renderSearchUrl()
		{
			WebPage searchResult = webSite.getWebPageByCategory("SEARCH RESULT", userSession);
			
			if (searchResult != null)
			{
				System.Web.HttpContext.Current.Response.Write(searchResult.getUrl());
			}
		}

        public string renderSignOutUrl()
        {
            return webSite.getAuthenticatedStartPageUrl() + "&systemCommand=signOut";
        }

		public string translate(string webTextConstantCode)
		{
			WebTextConstantValue webTextConstantValue = new WebTextConstantValue(database, webSite.code, webTextConstantCode, languageCode);
			return webTextConstantValue.textValue;
		}

		public void redirect(string target)
		{
			System.Web.HttpContext.Current.Response.Redirect(target, true);
			System.Web.HttpContext.Current.Response.End();
		}

		public void renderGoogleAnalytics()
		{
			if (this.webSite.enableGoogleAnalytics)
			{
				System.Web.HttpContext.Current.Response.Write("<script src=\"http://www.google-analytics.com/urchin.js\" type=\"text/javascript\">");
				System.Web.HttpContext.Current.Response.Write("</script>");
				System.Web.HttpContext.Current.Response.Write("<script type=\"text/javascript\">");
				System.Web.HttpContext.Current.Response.Write("_uacct = \""+this.webSite.googleAnalyticsId+"\";");
				System.Web.HttpContext.Current.Response.Write("urchinTracker();");
				System.Web.HttpContext.Current.Response.Write("</script>");
			}
		}

		public void release()
		{
			database = (Database)System.Web.HttpContext.Current.Session["database"];
			database.close();

		}

		public LanguageCollection getLanguages()
		{
			WebSiteLanguages webSiteLanguages = new WebSiteLanguages(this);
			return webSiteLanguages.getPublishedLanguages(this.webSite.code, this.languageCode);
			
		}

        public bool authenticate(string userName, string password)
        {
            return systemHandler.authenticate(this, userName, password);
        }

        public void authenticationRedirect()
        {
            systemHandler.authenticationRedirect(this);
        }

        public bool isDesignMode()
        {
            return contentHandler.isDesignMode();
        }

        public void performSearch(string searchQuery)
        {
            WebPage searchWebPage = webSite.getWebPageByCategory(webSite.searchPageCategoryCode, userSession);
            if (searchWebPage != null)
            {
                redirect(searchWebPage.getUrl() + "&searchQuery=" + searchQuery);
            }
        }
	}
}
