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

namespace Navipro.Infojet.WebInterface._taglib
{
    public partial class Login_STANDARD : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            Login loginForm = ((Login)loginView.FindControl("LoginForm"));
            if (loginForm != null)
            {
                Label rememberMeLabel = ((Label)loginForm.FindControl("rememberMeLabel"));
                if (rememberMeLabel != null) rememberMeLabel.Text = infojet.translate("REMEMBER ME");

                Label userIdLabel = ((Label)loginForm.FindControl("userIdLabel"));
                if (userIdLabel != null) userIdLabel.Text = infojet.translate("USERID");

                Label passwordLabel = ((Label)loginForm.FindControl("passwordLabel"));
                if (passwordLabel != null) passwordLabel.Text = infojet.translate("PASSWORD");

                Button loginButton = ((Button)loginForm.FindControl("loginButton"));
                if (loginButton != null) loginButton.Text = infojet.translate("LOGIN");

                Label forgotPasswordLabel = ((Label)loginForm.FindControl("forgotPasswordLabel"));
                if (forgotPasswordLabel != null) forgotPasswordLabel.Text = infojet.translate("FORGOT PASSWORD");
                
            }
            
            
            if (Request.IsAuthenticated)
            {
                ((System.Web.UI.WebControls.Label)loginView.FindControl("customerName")).Text = infojet.userSession.customer.name;
                ((System.Web.UI.WebControls.Label)loginView.FindControl("customerCity")).Text = infojet.userSession.customer.city;
                ((System.Web.UI.WebControls.Label)loginView.FindControl("customerNoLabel")).Text = infojet.translate("CUSTOMER NO");
                ((System.Web.UI.WebControls.Label)loginView.FindControl("customerNo")).Text = infojet.userSession.customer.no;
                ((System.Web.UI.WebControls.Label)loginView.FindControl("ipAddressLabel")).Text = infojet.translate("IP ADDRESS");
                ((System.Web.UI.WebControls.Label)loginView.FindControl("ipAddress")).Text = infojet.userSession.clientIp;
                ((System.Web.UI.WebControls.HyperLink)loginView.FindControl("myProfileLink")).Text = infojet.translate("MY PROFILE");
                ((System.Web.UI.WebControls.HyperLink)loginView.FindControl("myProfileLink")).NavigateUrl = infojet.userSession.getProfilePageUrl(infojet);
                ((System.Web.UI.WebControls.LinkButton)loginView.FindControl("logoffLink")).Text = infojet.translate("LOGOFF");
                ((System.Web.UI.WebControls.LinkButton)loginView.FindControl("logoffLink")).Click += new EventHandler(Login_STANDARD_Click);


            }
        }

        void Login_STANDARD_Click(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            infojet.systemHandler.signOut();
        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;    
        }

        #endregion

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            Login login = (Login)sender;

            if (infojet.authenticate(login.UserName, login.Password))
            {
                e.Authenticated = true;
            }

        }

        protected void Login1_LoggedIn(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            infojet.authenticationRedirect();
        }

        protected void forgotPassword_Click(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebPage forgotPasswordPage = new WebPage(infojet, infojet.webSite.code, infojet.webSite.forgotPasswordPageCode);
            infojet.redirect(forgotPasswordPage.getUrl());
        }
    }
}