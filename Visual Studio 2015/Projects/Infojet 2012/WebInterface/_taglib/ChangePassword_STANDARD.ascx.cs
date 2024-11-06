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
    public partial class ChangePassword_STANDARD : System.Web.UI.UserControl, InfojetUserControl
    {
        private WebPageLine webPageLine;

        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();
            WebLogin webLogin = new WebLogin(infojet, webPageLine.code);

            submitBtn.Text = infojet.translate("CHANGE PASSWORD");
            cancelBtn.Text = infojet.translate("CANCEL");

            submitBtn.Click += new EventHandler(submitBtn_Click);
            cancelBtn.Click += new EventHandler(cancelBtn_Click);

        }


        void cancelBtn_Click(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            WebPage myProfilePage = infojet.webSite.getWebPageByCategory(infojet.webSite.myProfileCategoryCode, infojet.userSession);
            if (myProfilePage != null) infojet.redirect(myProfilePage.getUrl());

        }

        void submitBtn_Click(object sender, EventArgs e)
        {
            oldPasswordValidation.Text = "";
            newPasswordValidation.Text = "";
            retypePasswordValidation.Text = "";

            bool performPasswordChange = true;

            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            if (oldPasswordBox.Text != infojet.userSession.webUserAccount.password)
            {
                oldPasswordValidation.Text = infojet.translate("WRONG PASSWORD");
                performPasswordChange = false;
            }

            if (newPasswordBox.Text == "")
            {
                newPasswordValidation.Text = infojet.translate("NEW USER MSG 10");
                performPasswordChange = false;
            }

            if (newPasswordBox.Text != retypePasswordBox.Text)
            {
                retypePasswordValidation.Text = infojet.translate("NEW USER MSG 11");
                performPasswordChange = false;
            }

            if (performPasswordChange)
            {
                WebLogin webLogin = new WebLogin(infojet, webPageLine.code);
                if (webLogin.setPassword(infojet, newPasswordBox.Text))
                {
                    WebPage myProfilePage = infojet.webSite.getWebPageByCategory(infojet.webSite.myProfileCategoryCode, infojet.userSession);
                    if (myProfilePage != null) infojet.redirect(myProfilePage.getUrl());
                }
                else
                {
                    oldPasswordValidation.Text = webLogin.getLastErrorMessage();
                }
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