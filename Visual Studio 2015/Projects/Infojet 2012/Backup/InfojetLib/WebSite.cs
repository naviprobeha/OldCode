using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebSite.
	/// </summary>
	public class WebSite
	{
		private string _code;
		private string _description;
		private string _location;
        private string _siteLocation;
		public string defaultLanguageCode;
		public string workingLanguageCode;
		public int priceInventoryCalcMethod;
		public string locationCode;
		public int visibility;
		public bool hideZeroInventoryItems;
		public float zeroInventoryValue;
		public bool allowPurchaseOfZeroInventoryItem;
		public bool allowPurchaseNotLoggedIn;
		public string inStockTextConstantCode;
		public string noInStockTextConstantCode;
		public string leadTimeTextConstantCode;
		public int priceCalcType;
		public bool dontAllowCustPriceAndDisc;
        public bool restrictToIPAddresses;

		public bool enableGoogleAnalytics;
		public string googleAnalyticsId;

		public bool saveCart;
		public bool reserveCart;

        public int submitContentMethod;
        public int submitOrderMethod;

        public string startPageCategoryCode;
        public string authenticatedCategoryCode;
        public string myProfileCategoryCode;
        public string changePasswordCategoryCode;
        public string searchPageCategoryCode;
        public string forgotPasswordPageCode;
        public string signedOutPageCode;
        public string newsPageCategoryCode;
        public string signInPageCode;
        public string newsletterResponseCatCode;

        public string shortcutName;
        public bool defaultSite;
        public bool fancyLinks;
        public string webStyleCode;
        public bool useSslWhenAuthenticated;
        public string inventoryCompanyName;
        public int orderSubmitMethod;

        public int availability;
        public string emailSenderAddress;
        public string commonCustomerNo;
        private bool _showPriceInclVAT;
        public string orderNoSeries;

        public DateTime propertiesUpdated;


        private Infojet infojetContext;

        public WebSite() { }

		public WebSite(Infojet infojetContext, string code)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
			this.code = code;

			getFromDatabase();

            if (webStyleCode != "")
            {
                WebStyle webStyle = new WebStyle(infojetContext, webStyleCode);
                location = siteLocation + "styles/"+webStyle.location + "/";
            }

		}
        
        public WebSite(Infojet infojetContext, SqlDataReader dataReader)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

            readData(dataReader);

        }
        
		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Code], [Description], [Location], [Default Language Code], [Working Language Code], [Location Code], [Visibility], [Hide Zero Inventory Items], [In Stock Text Constant Code], [No In Stock Text Constant Code], [Lead Time Text Constant Code], [Enable Google Analytics], [Google Analytics ID], [Price_Inventory Calc_ Method], [Allow Purch_ Of Zero Inv_ Item], [Price Calc_ Type], [Allow Purch_ Not Logged In], [Dont Allow Cust_ Price & Disc_], [Zero Inventory Value], [Save Cart], [Reserve Cart], [Start Page Category Code], [Authenticated Category Code], [My Profile Category Code], [Change Password Category Code], [Forgot Password Page Code], [Search Page Category Code], [Signed Out Page Code], [News Page Category Code], [Shortcut Name], [Default Site], [Fancy Links], [Web Style Code], [Properties Updated], [Sign In Page Code], [Availability], [E-mail Sender Address], [Use SSL When Authenticated], [Common Customer No_], [Restrict To IP Addresses], [Newsletter Response Cat_ Code], [Inventory Company Name], [Show Price Incl_ VAT], [Order No_ Series], [Order Submit Method] FROM [" + infojetContext.systemDatabase.getTableName("Web Site") + "] WHERE [Code] = @code");
            databaseQuery.addStringParameter("code", code.ToUpper(), 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{
                readData(dataReader);
			}

			dataReader.Close();

            WebSiteLanguages webSiteLanguages = new WebSiteLanguages(infojetContext);
            WebSiteLanguage hostnameLanguage = webSiteLanguages.getHostnameLanguage(code, System.Web.HttpContext.Current.Request.Url.Host);
            if (hostnameLanguage != null)
            {
                location = "http://" + System.Web.HttpContext.Current.Request.Url.Host+"/";
                siteLocation = location;
                defaultLanguageCode = hostnameLanguage.languageCode;
            }
		}

        private void readData(SqlDataReader dataReader)
        {
            code = dataReader.GetValue(0).ToString();
            description = dataReader.GetValue(1).ToString();
            location = dataReader.GetValue(2).ToString();
            defaultLanguageCode = dataReader.GetValue(3).ToString();
            workingLanguageCode = dataReader.GetValue(4).ToString();
            locationCode = dataReader.GetValue(5).ToString();

            visibility = int.Parse(dataReader.GetValue(6).ToString());

            hideZeroInventoryItems = false;
            if (dataReader.GetValue(7).ToString() == "1") hideZeroInventoryItems = true;

            inStockTextConstantCode = dataReader.GetValue(8).ToString();
            noInStockTextConstantCode = dataReader.GetValue(9).ToString();
            leadTimeTextConstantCode = dataReader.GetValue(10).ToString();

            this.enableGoogleAnalytics = false;
            if (dataReader.GetValue(11).ToString() == "1") this.enableGoogleAnalytics = true;

            googleAnalyticsId = dataReader.GetValue(12).ToString();

            try
            {
                priceInventoryCalcMethod = int.Parse(dataReader.GetValue(13).ToString());
            }
            catch (Exception)
            { }

            allowPurchaseOfZeroInventoryItem = false;
            if (dataReader.GetValue(14).ToString() == "1") allowPurchaseOfZeroInventoryItem = true;

            try
            {
                priceCalcType = int.Parse(dataReader.GetValue(15).ToString());
            }
            catch (Exception)
            { }

            allowPurchaseNotLoggedIn = false;
            if (dataReader.GetValue(16).ToString() == "1") allowPurchaseNotLoggedIn = true;

            dontAllowCustPriceAndDisc = false;
            if (dataReader.GetValue(17).ToString() == "1") dontAllowCustPriceAndDisc = true;

            this.zeroInventoryValue = float.Parse(dataReader.GetValue(18).ToString());

            saveCart = false;
            if (dataReader.GetValue(19).ToString() == "1") saveCart = true;

            reserveCart = false;
            if (dataReader.GetValue(20).ToString() == "1") reserveCart = true;

            startPageCategoryCode = dataReader.GetValue(21).ToString();
            authenticatedCategoryCode = dataReader.GetValue(22).ToString();
            myProfileCategoryCode = dataReader.GetValue(23).ToString();
            changePasswordCategoryCode = dataReader.GetValue(24).ToString();
            forgotPasswordPageCode = dataReader.GetValue(25).ToString();
            searchPageCategoryCode = dataReader.GetValue(26).ToString();
            signedOutPageCode = dataReader.GetValue(27).ToString();
            newsPageCategoryCode = dataReader.GetValue(28).ToString();

            shortcutName = dataReader.GetValue(29).ToString();

            defaultSite = false;
            if (dataReader.GetValue(30).ToString() == "1") defaultSite = true;

            fancyLinks = false;
            if (dataReader.GetValue(31).ToString() == "1") fancyLinks = true;

            webStyleCode = dataReader.GetValue(32).ToString();

            propertiesUpdated = dataReader.GetDateTime(33).AddMinutes(125);

            signInPageCode = dataReader.GetValue(34).ToString();

            availability = dataReader.GetInt32(35);
            emailSenderAddress = dataReader.GetValue(36).ToString();

            useSslWhenAuthenticated = false;
            if (dataReader.GetValue(37).ToString() == "1") useSslWhenAuthenticated = true;

            commonCustomerNo = dataReader.GetValue(38).ToString();

            restrictToIPAddresses = false;
            if (dataReader.GetValue(39).ToString() == "1") restrictToIPAddresses = true;

            newsletterResponseCatCode = dataReader.GetValue(40).ToString();

            inventoryCompanyName = dataReader.GetValue(41).ToString();

            _showPriceInclVAT = false;
            if (dataReader.GetValue(42).ToString() == "1") _showPriceInclVAT = true;

            orderNoSeries = dataReader.GetValue(43).ToString();

            orderSubmitMethod = int.Parse(dataReader.GetValue(44).ToString());

            if (infojetContext.configuration.webSiteAddress != null)
            {
                if (infojetContext.configuration.webSiteAddress != "") location = infojetContext.configuration.webSiteAddress;
            }
            if (infojetContext.configuration.fancyLinks) fancyLinks = true;

            if (infojetContext.userSession != null)
            {
                //if (infojetContext.userSession.customer.pricesIncludingVAT) showPriceInclVAT = true;
                //if (!infojetContext.userSession.customer.pricesIncludingVAT) showPriceInclVAT = false;
            }
            siteLocation = location;
        }

		public WebPage getWebPage(string webPageCode)
		{
            return new WebPage(infojetContext, this.code, webPageCode);
		}

		public ArrayList getWebPagesByCategory(string categoryCode, string userNo)
		{
			ArrayList arrayList = new ArrayList();
			ArrayList tempArrayList = new ArrayList();

            DatabaseQuery databaseQuery;
			
			if (userNo != null)
			{
                databaseQuery = infojetContext.systemDatabase.prepare("SELECT c.[Web Page Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Page Category") + "] c, [" + infojetContext.systemDatabase.getTableName("Web Page") + "] p, [" + infojetContext.systemDatabase.getTableName("Web Page User Group") + "] wpg, [" + infojetContext.systemDatabase.getTableName("Web User Account Group") + "] wug, [" + infojetContext.systemDatabase.getTableName("Web User Account Relation") + "] wur WHERE c.[Web Category Code] = @categoryCode AND c.[Web Site Code] = @code AND c.[Web Site Code] = p.[Web Site Code] AND p.[Code] = c.[Web Page Code] AND p.Code = wpg.[Web Page Code] AND p.[Web Site Code] = wpg.[Web Site Code] AND wpg.[Web User Group Code] = wug.[Web User Group Code] AND wug.[No_] = @userNo AND wug.[No_] = wur.[No_] AND wur.[Web Site Code] = @code AND ((p.[Published From] <= GETDATE() AND p.[Published To] >= GETDATE()) OR (p.[Published From] = '1753-01-01' AND p.[Published To] = '1753-01-01') OR (p.[Published From] <= GETDATE() AND p.[Published To] = '1753-01-01')) AND p.[Published Version No_] > 0 ORDER BY [Order], [Published From] DESC");
                databaseQuery.addStringParameter("userNo", userNo, 20);
            }
			else
			{
                databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Web Page Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Page Category") + "] c, [" + infojetContext.systemDatabase.getTableName("Web Page") + "] p WHERE c.[Web Category Code] = @categoryCode AND c.[Web Site Code] = @code AND p.[Protected] = 0 AND c.[Web Site Code] = p.[Web Site Code] AND p.[Code] = c.[Web Page Code] AND ((p.[Published From] <= GETDATE() AND p.[Published To] >= GETDATE()) OR (p.[Published From] = '1753-01-01' AND p.[Published To] = '1753-01-01') OR (p.[Published From] <= GETDATE() AND p.[Published To] = '1753-01-01')) AND p.[Published Version No_] > 0 ORDER BY [Order], [Published From] DESC");
			}

            databaseQuery.addStringParameter("categoryCode", categoryCode, 20);
            databaseQuery.addStringParameter("code", code, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
			{
                tempArrayList.Add(dataReader.GetValue(0).ToString());
			}

			dataReader.Close();

			if (tempArrayList.Count > 0)
			{
				int i = 0;
				while (i < tempArrayList.Count)
				{
                    WebPage webPage = new WebPage(infojetContext, this.code, (string)tempArrayList[i]);
					arrayList.Add(webPage);
			
					i++;
				}
			}

			return arrayList;
		}

        public ArrayList getWebPagesByCategory(string categoryCode, UserSession userSession)
        {
            if (userSession != null)
            {
                return getWebPagesByCategory(categoryCode, userSession.webUserAccount.no);
            }
            else
            {
                return getWebPagesByCategory(categoryCode, (string)null);
            }
        }

		public ArrayList getWebPagesByCategory(string categoryCode)
		{
			return getWebPagesByCategory(categoryCode, infojetContext.userSession);
		}

		public WebPage getWebPageByCategory(string categoryCode, UserSession userSession)
		{
			if (userSession != null)
			{
				return getWebPageByCategory(categoryCode, userSession.webUserAccount.no);
			}
			else
			{
				return getWebPageByCategory(categoryCode, (string)null);
			}
		}

		public WebPage getWebPageByCategory(string categoryCode, string userNo)
		{
			WebPage webPage = null; 

			if (userNo != null)
			{
				System.Collections.ArrayList pages = getWebPagesByCategory(categoryCode, userNo);
				if (pages.Count > 0) webPage = (WebPage)pages[0];
			}
			else
			{
				System.Collections.ArrayList pages = getWebPagesByCategory(categoryCode);
				if (pages.Count > 0) webPage = (WebPage)pages[0];
			}

			return webPage;

		}

		public string getStartPageUrl()
		{
			ArrayList webPageList = getWebPagesByCategory(this.startPageCategoryCode, infojetContext.userSession);
            
			if (webPageList.Count > 0)
			{
				WebPage webPage = (WebPage)webPageList[0];

				return webPage.getUrl();

			}

			return "";
		}

        public string getAuthenticatedStartPageUrl()
        {
            if (authenticatedCategoryCode == "") throw new Exception("Missing Authenticated Category Code on Web Site " + code);

            UserSession userSession = (UserSession)System.Web.HttpContext.Current.Session["currentUserSession"];

            if (userSession != null)
            {
                ArrayList webPageList = getWebPagesByCategory(authenticatedCategoryCode, userSession.webUserAccount.no);

                if (webPageList.Count > 0)
                {

                    WebPage webPage = (WebPage)webPageList[0];
                    System.Web.HttpContext.Current.Session["targetWebPageCode"] = webPage.code;

                    return webPage.getUrl();

                }
                else
                {
                    throw new Exception("No linked webpages found for current user.");
                }
            }

            return this.getStartPageUrl();
 
        }

		public DataSet getCountries()
		{
            WebSiteCountries webSiteCountries = new WebSiteCountries(infojetContext.systemDatabase);
			return webSiteCountries.getWebSiteCountries(this.code);

		}

		public DataSet getLanguages()
		{
            WebSiteLanguages webSiteLanguages = new WebSiteLanguages(infojetContext);
			return webSiteLanguages.getWebSiteLanguages(this.code);
		}

        public static WebSite getDefaultSite(Infojet infojetContext)
        {
            string webSiteCode = "";

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Site") + "] WHERE [Default Site] = '1'");
            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                webSiteCode = dataReader.GetValue(0).ToString();
            }

            dataReader.Close();

            if (webSiteCode == "") return null;

            WebSite webSite = new WebSite(infojetContext, webSiteCode);

            return webSite;
        }

        public string location
        {
            get
            {
                if (useSslWhenAuthenticated) return _location.Replace("http://", "https://");
                return _location;
            }
            set
            {
                _location = value;
            }
        }

        public string siteLocation
        {
            get
            {
                if (useSslWhenAuthenticated) return _siteLocation.Replace("http://", "https://");
                return _siteLocation;
            }
            set
            {
                _siteLocation = value;
            }
        }

        public string code { get { return _code; } set { _code = value; } }
        public string description { get { return _description; } set { _description = value; } }

        public static WebSite[] getWebSiteArray(Infojet infojetContext)
        {


            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Code], [Description], [Location], [Default Language Code], [Working Language Code], [Location Code], [Visibility], [Hide Zero Inventory Items], [In Stock Text Constant Code], [No In Stock Text Constant Code], [Lead Time Text Constant Code], [Enable Google Analytics], [Google Analytics ID], [Price_Inventory Calc_ Method], [Allow Purch_ Of Zero Inv_ Item], [Price Calc_ Type], [Allow Purch_ Not Logged In], [Dont Allow Cust_ Price & Disc_], [Zero Inventory Value], [Save Cart], [Reserve Cart], [Start Page Category Code], [Authenticated Category Code], [My Profile Category Code], [Change Password Category Code], [Forgot Password Page Code], [Search Page Category Code], [Signed Out Page Code], [News Page Category Code], [Shortcut Name], [Default Site], [Fancy Links], [Web Style Code], [Properties Updated], [Sign In Page Code], [Availability], [E-mail Sender Address], [Use SSL When Authenticated], [Common Customer No_], [Restrict To IP Addresses], [Newsletter Response Cat_ Code], [Inventory Company Name], [Show Price Incl_ VAT], [Order No_ Series], [Order Submit Method] FROM [" + infojetContext.systemDatabase.getTableName("Web Site") + "] ORDER BY [Description]");
         
            SqlDataReader dataReader = databaseQuery.executeQuery();

            int i = 0;
            while (dataReader.Read())
            {
                i++;
            }

            dataReader.Close();

            WebSite[] webSiteArray = new WebSite[i];
            i = 0;

            dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                webSiteArray[i] = new WebSite(infojetContext, dataReader);
                i++;
            }

            dataReader.Close();

            return webSiteArray;
        }

        public bool showPriceInclVAT
        {
            get
            {
                if (infojetContext.userSession != null)
                {
                    if (infojetContext.userSession.webUserAccount.pricesInclVAT == 1) return false;
                    if (infojetContext.userSession.webUserAccount.pricesInclVAT == 2) return true;
                }
                return _showPriceInclVAT;
            }
        }
	}
}