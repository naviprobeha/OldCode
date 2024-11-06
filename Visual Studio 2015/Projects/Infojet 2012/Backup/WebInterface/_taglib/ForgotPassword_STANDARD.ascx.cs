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
    public partial class ForgotPassword_STANDARD : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            submitBtn.Text = infojet.translate("SEND");
            submitBtn.Click += new EventHandler(submitBtn_Click);

            if ((Request["emailAddress"] != "") && (Request["emailAddress"] != null))
            {
                emailBox.Text = Request["emailAddress"];
                submitBtn_Click(null, null);
            }
        }

        void submitBtn_Click(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebLogin webLogin = new WebLogin(infojet, webPageLine.code);

            if (emailBox.Text == "")
            {
                emailValidation.Text = infojet.translate("NEW USER MSG 9");
            }
            else
            {
                if (webLogin.performUserAccountCheck(infojet, emailBox.Text))
                {
                    WebPage confirmWebPage = new WebPage(infojet, infojet.webSite.code, webLogin.forgotPasswordConfirmWebPageCode);
                    infojet.redirect(confirmWebPage.getUrl());
                }

                WebPage failedWebPage = new WebPage(infojet, infojet.webSite.code, webLogin.forgotPasswordFailedWebPageCode);
                infojet.redirect(failedWebPage.getUrl());
            }
        }


        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion

    }
}