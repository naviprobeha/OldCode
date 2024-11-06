using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Navipro.Infojet.Lib;

namespace WebInterface
{
    public partial class adminLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Infojet infojet = new Infojet();
            infojet.init();

            if (Request["scommand"] == "login")
            {

                
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {

                    System.Web.Security.FormsAuthentication.SignOut();

                    System.Web.HttpContext.Current.Session.Abandon();

                    HttpCookie authCookie = new HttpCookie(System.Web.Security.FormsAuthentication.FormsCookieName, "");
                    authCookie.Expires = DateTime.Now.AddYears(-1);
                    Response.Cookies.Add(authCookie);

                    infojet.redirect(infojet.webSite.location+"adminLogin.aspx?pageCode=STARTPAGE&scommand=login&userNo=" + Request["userNo"]);
                }

                WebUserAccount webUserAccount = new WebUserAccount(infojet.systemDatabase, Request["userNo"]);

                if (checkRelation(webUserAccount.no))
                {
                    infojet.redirect("http://partner.newbody.se/account/InternLogin?u=" + webUserAccount.userId + "&p=" + webUserAccount.password);
                }
                else
                {
                    if (infojet.authenticate(webUserAccount.userId, webUserAccount.password))
                    {
                        infojet.redirect(infojet.webSite.getAuthenticatedStartPageUrl());
                    }
                }
            }
        }

        private bool checkRelation(string userNo)
        {
            bool exists = false;

            Infojet infojet = new Infojet();

            DatabaseQuery databaseQuery = infojet.systemDatabase.prepare("SELECT [No_], [Web Site Code] FROM [" + infojet.systemDatabase.getTableName("Web User Account Relation") + "] WHERE [No_] = @no AND [Web Site Code] = @webSiteCode");
            databaseQuery.addStringParameter("webSiteCode", "PARTNER", 20);
            databaseQuery.addStringParameter("no", userNo, 20);

            System.Data.SqlClient.SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                exists = true;
            }

            dataReader.Close();

            return exists;
        }
    }
}
