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
		public string code;
		public string description;
		public string location;
		public string defaultLanguageCode;
		public string workingLanguageCode;
		public int priceInventoryCalcMethod;
		public string locationCode;
		public int showInventoryAs;
		public bool hideZeroInventoryItems;
		public float zeroInventoryValue;
		public bool allowPurchaseOfZeroInventoryItem;
		public bool allowPurchaseNotLoggedIn;
		public string inStockTextConstantCode;
		public string noInStockTextConstantCode;
		public string leadTimeTextConstantCode;
		public int priceCalcType;
		public bool dontAllowCustPriceAndDisc;

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

        private Infojet infojetContext;

		public WebSite(Infojet infojetContext, string code)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
			this.code = code;

			getFromDatabase();
		}


		private void getFromDatabase()
		{

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Code], [Description], [Location], [Default Language Code], [Working Language Code], [Location Code], [Show Inventory As], [Hide Zero Inventory Items], [In Stock Text Constant Code], [No In Stock Text Constant Code], [Lead Time Text Constant Code], [Enable Google Analytics], [Google Analytics ID], [Price_Inventory Calc_ Method], [Allow Purch_ Of Zero Inv_ Item], [Price Calc_ Type], [Allow Purch_ Not Logged In], [Dont Allow Cust_ Price & Disc_], [Zero Inventory Value], [Save Cart], [Reserve Cart], [Start Page Category Code], [Authenticated Category Code], [My Profile Category Code], [Change Password Category Code], [Forgot Password Page Code], [Search Page Category Code], [Signed Out Page Code], [News Page Category Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Site") + "] WHERE [Code] = '" + this.code + "'");
            databaseQuery.addStringParameter("code", code.ToUpper(), 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
			{
				code = dataReader.GetValue(0).ToString();
				description = dataReader.GetValue(1).ToString();
				location = dataReader.GetValue(2).ToString();
				defaultLanguageCode = dataReader.GetValue(3).ToString();
				workingLanguageCode = dataReader.GetValue(4).ToString();	
				locationCode = dataReader.GetValue(5).ToString();	

				showInventoryAs = int.Parse(dataReader.GetValue(6).ToString());

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
				catch(Exception)
				{}

				allowPurchaseOfZeroInventoryItem = false;
				if (dataReader.GetValue(14).ToString() == "1") allowPurchaseOfZeroInventoryItem = true;

				try
				{
					priceCalcType = int.Parse(dataReader.GetValue(15).ToString());
				}
				catch(Exception)
				{}

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
			}

			dataReader.Close();
			
		}


		public WebPage getWebPage(string webPageCode)
		{
            return new WebPage(infojetContext, this.code, webPageCode);
		}

		public ArrayList getWebPagesByCategory(string categoryCode, string userNo)
		{
			ArrayList arrayList = new ArrayList();
			ArrayList tempArrayList = new ArrayList();

			SqlDataReader dataReader;

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

            dataReader = databaseQuery.executeQuery();
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
            if (authenticatedCategoryCode != null)
            {
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
                }
                else
                {
                    return this.getStartPageUrl();
                }
            }

            return null;
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

		public bool checkItemVisibility(float inventory)
		{
			if ((hideZeroInventoryItems) && (inventory <= zeroInventoryValue)) return false;
			return true;
		}
	}
}