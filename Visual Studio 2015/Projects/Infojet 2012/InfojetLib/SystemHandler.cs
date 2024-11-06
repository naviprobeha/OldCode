using System;
using System.IO;
using System.Web;
using System.Collections;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for SystemHandler.
	/// </summary>
    /// 
    
	public class SystemHandler
	{
		private WebSite currentWebSite;

		private HttpRequest Request;
		private HttpResponse Response;
		private System.Web.SessionState.HttpSessionState Session;
		private Infojet infojetContext;

		public SystemHandler(Infojet infojetContext)
		{
			//
			// TODO: Add constructor logic here
			//

			Session = HttpContext.Current.Session;
			Request = HttpContext.Current.Request;
			Response = HttpContext.Current.Response;
			this.infojetContext = infojetContext;
		}

		public void handleSystemRequests()
		{
            checkCss("design");
            checkCss("visuals");

            currentWebSite = this.infojetContext.webSite;

			if (Request["systemCommand"] != null)
			{
				handleSystemRequest(Request["systemCommand"]);		
			}

		}

		private void handleSystemRequest(string systemCommand)
		{
            if (systemCommand == "signOut") this.signOut();
            if (systemCommand == "signIn") this.signIn();
            if (systemCommand == "reportPayment") this.reportPayment();
            if (systemCommand == "downloadDocument") this.downloadDocument();
            if (systemCommand == "getSiteMap") this.getSiteMap();
            if (systemCommand == "getCaptchaImage") this.getCaptchaImage();
            if (systemCommand == "approveOrder") this.approveOrder();
        }

        public bool authenticate(Infojet infojetContext, string userId, string password)
        {
            userId = userId.Replace("'", "");
            userId = userId.Replace("\"", "");

            password = password.Replace("'", "");
            password = password.Replace("\"", "");

            WebUserAccounts webUserAccounts = new WebUserAccounts(infojetContext.systemDatabase);
            WebUserAccount webUserAccount = webUserAccounts.getUserAccount(userId, password);


            if (webUserAccount != null)
            {
                WebUserAccountRelations webUserAccountRelations = new WebUserAccountRelations(infojetContext.systemDatabase);

                WebUserAccountRelation webUserAccountRelation = webUserAccountRelations.getUserAccountRelation(webUserAccount.no, infojetContext.webSite.code);
                if (webUserAccountRelation != null)
                {
                    createSession(infojetContext, userId, webUserAccountRelation.webSiteCode);
                    return true;
                }

                webUserAccountRelation = webUserAccountRelations.getUserAccountRelationFromWebSiteGroup(webUserAccount.no, infojetContext.webSite.siteLocation);
                if (webUserAccountRelation != null)
                {
                    createSession(infojetContext, userId, webUserAccountRelation.webSiteCode);
                    return true;
                }

            }
            return false;

        }

        public void createSession(Infojet infojetContext)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                WebUserAccountRelations webUserAccountRelations = new WebUserAccountRelations(infojetContext.systemDatabase);
                WebUserAccountRelation webUserAccountRelation = webUserAccountRelations.findFirstRelation(System.Web.HttpContext.Current.User.Identity.Name);

                if (webUserAccountRelation != null)
                {
                    createSession(infojetContext, System.Web.HttpContext.Current.User.Identity.Name, webUserAccountRelation.webSiteCode);
                }
            }
        }

        public void createSession(Infojet infojetContext, string userId, string webSiteCode)
        {
            WebUserAccounts webUserAccounts = new WebUserAccounts(infojetContext.systemDatabase);
            WebUserAccount webUserAccount = webUserAccounts.getUserAccount(userId);

            if (webUserAccount != null)
            {
                createSession(infojetContext, webUserAccount, webSiteCode);  
            }
        }

        public void createSession(Infojet infojetContext, WebUserAccount webUserAccount, string webSiteCode)
        {

            if (webUserAccount != null)
            {
                WebUserAccountRelations relations = new WebUserAccountRelations(infojetContext.systemDatabase);
                WebUserAccountRelation relation = relations.getUserAccountRelation(webUserAccount.no, webSiteCode);
                if (relation.customerNo != "") webUserAccount.customerNo = relation.customerNo;

                
                UserSession userSession = new UserSession(infojetContext.systemDatabase, infojetContext.webSite, webUserAccount);
                userSession.customer = new Customer(infojetContext, webUserAccount.customerNo);
                userSession.clientIp = System.Web.HttpContext.Current.Request.UserHostAddress;
                userSession.clientAgent = System.Web.HttpContext.Current.Request.UserAgent;

                //infojetContext.cartHandler.applyWebUserAccount(webUserAccount.no);
                infojetContext.cartHandler.clearCartInfo();

                //if (infojetContext.webSite.saveCart) infojetContext.cartHandler.setCartOwner(webUserAccount.no);
                infojetContext.cartHandler.setCartOwner(webUserAccount.no);

                System.Web.HttpContext.Current.Session["currencyCode"] = userSession.customer.currencyCode;

                if (userSession.webUserAccount.languageCode != "")
                {
                    System.Web.HttpContext.Current.Session["marketCode"] = userSession.webUserAccount.languageCode;
                }


                WebSite webSite = new WebSite(infojetContext, webSiteCode);
                //if (userSession.customer.pricesIncludingVAT) webSite.showPriceInclVAT = true;
                
                //Borde vara bortkommenterad.
                //if (!userSession.customer.pricesIncludingVAT) webSite.showPriceInclVAT = false;

                System.Web.HttpContext.Current.Session["webSite"] = webSite;

                System.Web.HttpContext.Current.Session["currentUserSession"] = userSession;

                infojetContext.cartHandler.clearCartInfo();
                if (infojetContext.webSite.saveCart)
                {
                    infojetContext.cartHandler.updateSession();
                }
                else
                {
                    //Can not do. Does not work with not saving cart.
                    //infojetContext.cartHandler.deleteUserCartLines();
                }

                infojetContext.cartHandler.reCalculateCart(webUserAccount.customerNo, userSession.customer.currencyCode);

                if (infojetContext.webSite.reserveCart) infojetContext.cartHandler.reserveCart();

                //System.Web.Security.FormsAuthentication.SetAuthCookie(webUserAccount.userId, false);

            }
        }

        public void authenticationRedirect(Infojet infojetContext)
        {
            if ((System.Web.HttpContext.Current.Request["signInToWebPage"] != null) && (System.Web.HttpContext.Current.Request["signInToWebPage"] != ""))
            {
                WebPage targetWebPage = new WebPage(infojetContext, infojetContext.webSite.code, System.Web.HttpContext.Current.Request["signInToWebPage"]);
                infojetContext.redirect(targetWebPage.getUrl());
            }
            else
            {
                infojetContext.redirect(infojetContext.webSite.getAuthenticatedStartPageUrl());
            }
        }

        public void signOut()
        {
            System.Web.Security.FormsAuthentication.SignOut();

            System.Web.HttpContext.Current.Session.Abandon();

            HttpCookie authCookie = new HttpCookie(System.Web.Security.FormsAuthentication.FormsCookieName, "");
            authCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(authCookie);

            
            if (infojetContext.webSite.signedOutPageCode != "")
            {
                WebPage signedOutPage = new WebPage(infojetContext, infojetContext.webSite.code, infojetContext.webSite.signedOutPageCode);
                infojetContext.redirect(signedOutPage.getUrl());
            }

            infojetContext.redirect(infojetContext.webSite.getStartPageUrl());
            //infojetContext.redirect(infojetContext.webSite.location);
        }

        private void signIn()
        {

            if (authenticate(infojetContext, Request["userId"], Request["password"]))
            {
                authenticationRedirect(infojetContext);
            }

            infojetContext.redirect(infojetContext.webSite.getStartPageUrl());
        }

        private void checkCss(string filename)
        {
            string cssPath = System.Web.HttpContext.Current.Request.ApplicationPath + "_assets\\css";
            if (infojetContext.webSite.webStyleCode != "")
            {
                WebStyle webStyle = new WebStyle(infojetContext, infojetContext.webSite.webStyleCode);
                cssPath = System.Web.HttpContext.Current.Request.ApplicationPath + "styles\\" + webStyle.location + "\\_assets\\css";
            }
            string targetFileName = System.Web.HttpContext.Current.Server.MapPath(cssPath) + "\\" + infojetContext.webSite.code+ "_" + filename + ".css";
            string sourceFileName = System.Web.HttpContext.Current.Server.MapPath(cssPath) + "\\" + filename + ".def";

            
            FileInfo cssFile = new FileInfo(targetFileName);
            FileInfo defFile = new FileInfo(sourceFileName);

            if (!cssFile.Exists)
            {
                if (defFile.Exists) produceCss(sourceFileName, targetFileName);
            }
            else
            {
                if (cssFile.LastWriteTime.Ticks < infojetContext.webSite.propertiesUpdated.Ticks)
                {
                    produceCss(sourceFileName, targetFileName);
                }
                if (cssFile.LastWriteTime.Ticks < defFile.LastWriteTime.Ticks)
                {
                    produceCss(sourceFileName, targetFileName);
                }
            }
            
        }

        private void produceCss(string sourceFileName, string targetFileName)
        {
            StreamReader streamReader = new StreamReader(sourceFileName);
            string content = streamReader.ReadToEnd();
            streamReader.Close();

            ArrayList propertyList = WebStyleProperty.getArrayList(infojetContext);
            int i = 0;
            while (i < propertyList.Count)
            {
                WebStyleProperty webStyleProperty = (WebStyleProperty)propertyList[i];

                content = content.Replace("$[" + webStyleProperty.code + "]", infojetContext.getProperty(webStyleProperty.code));

                i++;
            }

            StreamWriter streamWriter = new StreamWriter(targetFileName);
            streamWriter.Write(content);
            streamWriter.Close();
            
        }


        private void reportPayment()
        {
            if (Request["provider"] == "dibs")
            {
                PaymentModuleDibs dibs = new PaymentModuleDibs(infojetContext, null);
                dibs.reportPayment();
            }

        }

        private void approveOrder()
        {
            WebCartHeader webCartHeader = new WebCartHeader(infojetContext, infojetContext.sessionId);
            webCartHeader.paymentOrderNo = Request["orderNo"];

            ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "reportOrderApproval", webCartHeader));
            if (appServerConnection.processRequest())
            {
                if (appServerConnection.serviceResponse.status == "200")
                {
                    System.Web.HttpContext.Current.Response.Write("Order approved!");
                    System.Web.HttpContext.Current.Response.End();
                }
            }

        }


        private void downloadDocument()
        {
            string fileName = Request["fileName"];

            WebDocument webDocument = WebDocument.getEntry(infojetContext, fileName);
            if (webDocument != null)
            {
                webDocument.downloadDocument();
            }

            Response.End();
        }

        private void getSiteMap()
        {
            Sitemap siteMap = new Sitemap();
            Response.ContentType = "text/xml";
            Response.Write(siteMap.getDocument(infojetContext).OuterXml);
            Response.End();

        }

        private void getCaptchaImage()
        {
            Captcha captcha = new Captcha();
            captcha.generateImage();
        }
	}


}
