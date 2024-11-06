using System;
using System.IO;
using System.Web;
using System.Collections;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for SystemHandler.
	/// </summary>
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

                bool relationExists = false;
                if (webUserAccountRelations.getUserAccountRelation(webUserAccount.no, infojetContext.webSite.code) != null) relationExists = true;

                if (relationExists)
                {
                    createSession(infojetContext, userId);
                    return true;
                }
            }
            return false;

        }

        public void createSession(Infojet infojetContext)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //throw new Exception("User: " + System.Web.HttpContext.Current.User.Identity.Name);
                createSession(infojetContext, System.Web.HttpContext.Current.User.Identity.Name);
            }
        }

        public void createSession(Infojet infojetContext, string userId)
        {
            WebUserAccounts webUserAccounts = new WebUserAccounts(infojetContext.systemDatabase);
            WebUserAccount webUserAccount = webUserAccounts.getUserAccount(userId);

            if (webUserAccount != null)
            {
                UserSession userSession = new UserSession(infojetContext.systemDatabase, infojetContext.webSite, webUserAccount);
                userSession.customer = new Customer(infojetContext.systemDatabase, webUserAccount.customerNo);
                userSession.clientIp = System.Web.HttpContext.Current.Request.UserHostAddress;
                userSession.clientAgent = System.Web.HttpContext.Current.Request.UserAgent;

                //infojetContext.cartHandler.applyWebUserAccount(webUserAccount.no);
                infojetContext.cartHandler.clearCartInfo();

                //if (infojetContext.webSite.saveCart) infojetContext.cartHandler.setCartOwner(webUserAccount.no);
                infojetContext.cartHandler.setCartOwner(webUserAccount.no);

                System.Web.HttpContext.Current.Session["currencyCode"] = userSession.customer.currencyCode;

                if (userSession.webUserAccount.languageCode != "")
                {
                    System.Web.HttpContext.Current.Session["languageCode"] = userSession.webUserAccount.languageCode;
                }

                System.Web.HttpContext.Current.Session["currentUserSession"] = userSession;

                infojetContext.cartHandler.clearCartInfo();
                infojetContext.cartHandler.updateSession();

                infojetContext.cartHandler.reCalculateCart(webUserAccount.customerNo, userSession.customer.currencyCode);

                if (infojetContext.webSite.reserveCart) infojetContext.cartHandler.reserveCart();

                System.Web.Security.FormsAuthentication.SetAuthCookie(webUserAccount.userId, false);
            }
        }


        public void authenticationRedirect(Infojet infojetContext)
        {
            infojetContext.redirect(infojetContext.webSite.getAuthenticatedStartPageUrl());
        }

        public void signOut()
        {
            System.Web.Security.FormsAuthentication.SignOut();

            System.Web.HttpContext.Current.Session.Abandon();

            HttpCookie authCookie = new HttpCookie(System.Web.Security.FormsAuthentication.FormsCookieName, "");
            authCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(authCookie);


            if ((Request["targetPageCode"] != null) && (Request["targetPageCode"] != ""))
            {
                WebPage webPage = new WebPage(infojetContext, infojetContext.webSite.code, Request["targetPageCode"]);
                infojetContext.redirect(webPage.getUrl());
            }
            else
            {
                if (infojetContext.webSite.signedOutPageCode != "")
                {
                    WebPage signedOutPage = new WebPage(infojetContext, infojetContext.webSite.code, infojetContext.webSite.signedOutPageCode);
                    infojetContext.redirect(signedOutPage.getUrl());
                }

                infojetContext.redirect(infojetContext.webSite.getStartPageUrl());
                //infojetContext.redirect(infojetContext.webSite.location);
            }
        }

        private void signIn()
        {

            if (authenticate(infojetContext, Request["userId"], Request["password"]))
            {
                authenticationRedirect(infojetContext);
            }

            infojetContext.redirect(infojetContext.webSite.getStartPageUrl());
        }
	}
}
