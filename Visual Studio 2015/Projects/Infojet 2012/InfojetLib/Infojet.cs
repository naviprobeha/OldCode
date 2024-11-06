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
    /// 

    [Serializable]
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

		public string languageCode { get { return (string)System.Web.HttpContext.Current.Session["languageCode"]; }	}
        public string marketCode { get { return (string)System.Web.HttpContext.Current.Session["marketCode"]; } }
        public string marketCountryCode { get { return (string)System.Web.HttpContext.Current.Session["marketCountryCode"]; } }
        public WebSiteLanguage language { get { return new WebSiteLanguage(systemDatabase, webSite.code, (string)System.Web.HttpContext.Current.Session["languageCode"]); } }
		public Configuration configuration { get { return (Configuration)System.Web.HttpContext.Current.Session["configuration"]; }	}	
		public WebSite webSite { get { return (WebSite)System.Web.HttpContext.Current.Session["webSite"]; } }
        public WebPage webPage { get { return (WebPage)System.Web.HttpContext.Current.Session["webPage"]; } }
		public CartHandler cartHandler { get { return (CartHandler)System.Web.HttpContext.Current.Session["cartHandler"]; } }
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
        
        public int versionNo 
        { 
            get 
            { 
                return (int)System.Web.HttpContext.Current.Session["versionNo"]; 
            }
            set
            {
                System.Web.HttpContext.Current.Session["versionNo"] = value;
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

        public bool punchOutMode
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["punchOutMode"] != null) return (bool)System.Web.HttpContext.Current.Session["punchOutMode"];
                return false;
            }
            set
            {
                System.Web.HttpContext.Current.Session["punchOutMode"] = value;
            }
        }

        public string punchOutCheckoutUrl
        {
            get
            {
                return (string)System.Web.HttpContext.Current.Session["punchOutCheckoutUrl"];
            }
            set
            {
                System.Web.HttpContext.Current.Session["punchOutCheckoutUrl"] = value;
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
            System.Web.HttpContext.Current.Session.Add("configuration", configuration);		

            WebSite webSite = getWebSite(configuration);
            if (webSite == null)
            {
                throw new Exception("No website could be found.");
            }

            if (webSite.restrictToIPAddresses)
            {
                if (!WebSiteIPAddress.checkAccess(this, webSite.code, System.Web.HttpContext.Current.Request.UserHostAddress))
                {
                    throw new HttpException(403, "IP address rejected ("+System.Web.HttpContext.Current.Request.UserHostAddress+").");
                }
            }

			CartHandler cartHandler = new CartHandler(database, System.Web.HttpContext.Current.Session.SessionID, this);

			System.Web.HttpContext.Current.Session.Add("webSite", webSite);
			System.Web.HttpContext.Current.Session.Add("cartHandler", cartHandler);
			System.Web.HttpContext.Current.Session.Add("currencyCode", "");

            if (System.Web.HttpContext.Current.Request["languageCode"] != null)
            {
                System.Web.HttpContext.Current.Session.Add("marketCode", System.Web.HttpContext.Current.Request["languageCode"]);
                WebSiteLanguage webSiteLanguage = new WebSiteLanguage(this.systemDatabase, webSite.code, System.Web.HttpContext.Current.Request["languageCode"]);

                System.Web.HttpContext.Current.Session.Add("languageCode", webSiteLanguage.languageCode);
                System.Web.HttpContext.Current.Session.Add("marketCountryCode", webSiteLanguage.marketCountryCode);

            }
            

            if ((System.Web.HttpContext.Current.Request["pageCode"] != null) && (System.Web.HttpContext.Current.Request["pageCode"] != "")) this.currentPageCode = System.Web.HttpContext.Current.Request["pageCode"];

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

                checkLanguage();
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

                contentHandler.handleRequests();

				if ((!currentWebPage.checkSecurity(userSession)) && (System.Web.HttpContext.Current.Request["securityHash"] == null) && (!contentHandler.isDesignMode()))
				{
					if (System.Web.HttpContext.Current.Session["targetWebPageCode"] != null)
					{
                        WebPage targetWebPage = new WebPage(this, webSite.code, (string)System.Web.HttpContext.Current.Session["targetWebPageCode"]);
						redirect(targetWebPage.getUrl());
					}
					else
					{
                        if ((webSite.signInPageCode != "") && (webPage.code != webSite.signInPageCode))
                        {
                            redirectToSignInPage(webPage);
                        }
                        else
                        {
                            redirect(webSite.getStartPageUrl());
                        }
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
				return;
			}

			System.Web.HttpContext.Current.Response.Redirect(webSite.getStartPageUrl(), true);

		}


		private void checkLanguage()
		{
			string currentLanguageCode = (string)System.Web.HttpContext.Current.Session["languageCode"];
            string currentMarketCode = (string)System.Web.HttpContext.Current.Session["marketCode"];

            if (currentMarketCode == null) currentMarketCode = webSite.defaultLanguageCode;
			if ((System.Web.HttpContext.Current.Request["languageCode"] != null) && (System.Web.HttpContext.Current.Request["languageCode"] != "")) currentMarketCode = System.Web.HttpContext.Current.Request["languageCode"]; //Market code


			WebSiteLanguage webSiteLanguage = new WebSiteLanguage(database, webSite.code, currentMarketCode);

            if (webSiteLanguage.cultureValue == null)
            {
                webSiteLanguage = new WebSiteLanguage(database, webSite.code, webSite.defaultLanguageCode); //Default market code
            }

            currentLanguageCode = webSiteLanguage.languageCode;
            string currentCurrencyCode = webSiteLanguage.currencyCode;


            if ((webSiteLanguage.cultureValue != "") && (webSiteLanguage.cultureValue != null))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(webSiteLanguage.cultureValue);
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(webSiteLanguage.cultureValue);
            }


            if (this.userSession != null)
            {
                currentCurrencyCode = this.userSession.customer.currencyCode;

                if (System.Web.HttpContext.Current.Session["marketCode"] != null)
                {
                    if (currentMarketCode != System.Web.HttpContext.Current.Session["marketCode"].ToString())
                    {
                        cartHandler.reCalculateCart(currentCurrencyCode);
                    }
                }

            }
 


            //System.Web.HttpContext.Current.Session.Add("currencyCode", currentCurrencyCode);
			System.Web.HttpContext.Current.Session["currencyCode"] = currentCurrencyCode;
			System.Web.HttpContext.Current.Session["languageCode"] = currentLanguageCode;
            System.Web.HttpContext.Current.Session["marketCode"] = currentMarketCode;
            System.Web.HttpContext.Current.Session["marketCountryCode"] = webSiteLanguage.marketCountryCode;

		}

		private void checkVersion()
		{
			int currentVersionNo = webPage.publishedVersionNo;

			if ((System.Web.HttpContext.Current.Request["versionNo"] != null) && (System.Web.HttpContext.Current.Request["versionNo"] != "")) 
			{
				currentVersionNo = int.Parse(System.Web.HttpContext.Current.Request["versionNo"]);
			}
            if (contentHandler.isDesignMode())
            {
                currentVersionNo = webPage.workingVersionNo;
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

                bool include = true;
                if (webPageLine.languageCode != "")
                {
                    if (webPageLine.languageCode != language.code) include = false;
                }

                if (include) webPageLineArray.Add(webPageLine);
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
            Hashtable translationTable = (Hashtable)System.Web.HttpContext.Current.Session["translationTable"];
            if (translationTable == null) translationTable = new Hashtable();

            if (translationTable[webTextConstantCode+"_"+languageCode] != null) return ((string)translationTable[webTextConstantCode+"_"+languageCode]);

			WebTextConstantValue webTextConstantValue = new WebTextConstantValue(database, webSite.code, webTextConstantCode, languageCode);
            webTextConstantValue.textValue = webTextConstantValue.textValue.Replace(" & ", " &amp; ");
            translationTable.Add(webTextConstantCode+"_"+languageCode, webTextConstantValue.textValue);
            
            System.Web.HttpContext.Current.Session["translationTable"] = translationTable;

			return webTextConstantValue.textValue;
		}

		public void redirect(string target)
		{
            target = target.Replace("&amp;", "&");
			System.Web.HttpContext.Current.Response.Redirect(target, true);
			System.Web.HttpContext.Current.Response.End();
		}

		public void renderGoogleAnalytics()
		{
			if (this.webSite.enableGoogleAnalytics)
			{

	            System.Web.HttpContext.Current.Response.Write("<script src=\"//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js\"></script>");
				System.Web.HttpContext.Current.Response.Write("<script type=\"text/javascript\">");            	
		        System.Web.HttpContext.Current.Response.Write("      var _gaq = _gaq || [];");
		        System.Web.HttpContext.Current.Response.Write("      _gaq.push(['_setAccount', '"+this.webSite.googleAnalyticsId+"']);");
		        System.Web.HttpContext.Current.Response.Write("      _gaq.push(['_trackPageview']);");

		        System.Web.HttpContext.Current.Response.Write("      (function() {");
		        System.Web.HttpContext.Current.Response.Write("        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;");
		        System.Web.HttpContext.Current.Response.Write("        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';");
		        System.Web.HttpContext.Current.Response.Write("        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);");
		        System.Web.HttpContext.Current.Response.Write("      })();");

		        System.Web.HttpContext.Current.Response.Write("    </script>");
				System.Web.HttpContext.Current.Response.Write("    <script type=\"text/javascript\">");            	
		        System.Web.HttpContext.Current.Response.Write("    $(document).ready(function() {");
		        System.Web.HttpContext.Current.Response.Write("    $('a[href*=\".pdf\"]').click(function() {");
		        System.Web.HttpContext.Current.Response.Write("        _gaq.push(['_trackPageview', \"/virtuell\" + $(this).attr(\"href\").replace(/^.*\\/\\/[^\\/]+/, \"\")]);");
		        System.Web.HttpContext.Current.Response.Write("    });");
		        System.Web.HttpContext.Current.Response.Write("    });");
	            System.Web.HttpContext.Current.Response.Write("</script>");

                    /*
				System.Web.HttpContext.Current.Response.Write("<script src=\"http://www.google-analytics.com/urchin.js\" type=\"text/javascript\">");
				System.Web.HttpContext.Current.Response.Write("</script>");
				System.Web.HttpContext.Current.Response.Write("<script type=\"text/javascript\">");
				System.Web.HttpContext.Current.Response.Write("_uacct = \""+this.webSite.googleAnalyticsId+"\";");
				System.Web.HttpContext.Current.Response.Write("urchinTracker();");
				System.Web.HttpContext.Current.Response.Write("</script>");
                     * */
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
			return webSiteLanguages.getPublishedLanguages(this.webSite.code, this.marketCode);
			
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

        public string getProperty(string webStylePropertyCode)
        {
            WebStyleProperty webStyleProperty = new WebStyleProperty(this, webSite.webStyleCode, webStylePropertyCode);
            WebSiteProperty webSiteProperty = new WebSiteProperty(this, webSite.code, webStylePropertyCode);
            if (webSiteProperty.value == "") webSiteProperty.value = webStyleProperty.defaultValue;
            if (webSiteProperty.value == "''") webSiteProperty.value = "";

            if (webStyleProperty.valueType == 2)
            {
                WebColor webColor = new WebColor(systemDatabase, webSiteProperty.value);
                return "#" + webColor.hexColor;
            }
            if (webStyleProperty.valueType == 3)
            {
                WebImage webImage = new WebImage(this, webSiteProperty.value);
                return webImage.getUrl();
            }
            
            return webSiteProperty.value;
        }

        public void performSearch(string searchQuery)
        {
            WebPage searchWebPage = webSite.getWebPageByCategory(webSite.searchPageCategoryCode, userSession);
            if (searchWebPage != null)
            {
                Link link = searchWebPage.getUrlLink();
                link.addParameter("searchQuery", searchQuery);

                redirect(link.toUrl());
            }
        }

        public WebSite getWebSite(Configuration configuration)
        {
            WebSite webSite = null;
            if (configuration.webSiteCode != "")
            {
                webSite = new WebSite(this, configuration.webSiteCode);
            }
            else
            {
                if ((System.Web.HttpContext.Current.Request["webSiteCode"] != null) && (System.Web.HttpContext.Current.Request["webSiteCode"] != ""))
                {
                    webSite = new WebSite(this, System.Web.HttpContext.Current.Request["webSiteCode"]);
                }
                else
                {
                    if (System.Web.HttpContext.Current.Session["webSite"] != null)
                    {
                        webSite = (WebSite)System.Web.HttpContext.Current.Session["webSite"];
                    }
                    else
                    {
                        webSite = WebSite.getDefaultSite(this);
                    }
                }
            }

            return webSite;
        }

        public void redirectToSignInPage(WebPage targetWebPage)
        {
            WebPage signInWebPage = new WebPage(this, webSite.code, webSite.signInPageCode);
            redirect(signInWebPage.getUrl() + "&signInToWebPage=" + targetWebPage.code);
        }

        public void initPunchOutMode(string webSiteCode, string targetUrl, string languageCode, string checkOutUrl)
        {
            punchOutMode = true;
            punchOutCheckoutUrl = checkOutUrl; 

            redirect(targetUrl + "&webSiteCode=" + webSiteCode + "&languageCode=" + languageCode);

        }

        public void initPunchOutMode(string webSiteCode, string languageCode, string checkOutUrl)
        {
            punchOutMode = true;
            punchOutCheckoutUrl = checkOutUrl;
            redirect("../?webSiteCode=" + webSiteCode + "&languageCode=" + languageCode);

        }

        public Customer getCurrentCustomer()
        {
            if (WebCartHeader.get() != null)
            {
                if (WebCartHeader.get().customerNo != "")
                {
                    Customer customer = new Customer(this, WebCartHeader.get().customerNo);
                    if (WebCartHeader.get().vatBusPostingGroup != "") customer.vatBusPostingGroup = WebCartHeader.get().vatBusPostingGroup;
                    return customer;
                }
            }

            if (userSession != null) return userSession.customer;
            if (webSite.commonCustomerNo != "") return new Customer(this, webSite.commonCustomerNo);
            return null;



        }
	}
}
