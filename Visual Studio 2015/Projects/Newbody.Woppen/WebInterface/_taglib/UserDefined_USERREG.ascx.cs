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
using Navipro.Newbody.Woppen.Library;

namespace Navipro.Infojet.WebInterface._taglib
{
    public partial class UserDefined_USERREG : System.Web.UI.UserControl, InfojetUserControl
    {
        protected WebPageLine webPageLine;
        protected Navipro.Infojet.Lib.Infojet infojet;
        protected WebForm webForm;
        protected Panel formPanel;



        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();

            if (infojet.userSession == null) Response.End();

            SalesIDSetup salesIdSetup = new SalesIDSetup(infojet.systemDatabase);
            SalesIDs salesIds = new SalesIDs();
            SalesID salesId = salesIds.getUserRegSalesId(infojet, infojet.userSession.webUserAccount);

            if (salesId == null) Response.End();

            if (salesId.countRegisteredSalesPersons() == 0)
            {
                newUserForm.Visible = false;
                errorMessage.Text = infojet.translate("REG OVERFLOW");
            }

            if (Request["userRegistration"] == "complete")
            {
                newUserForm.Visible = false;
                errorMessage.Text = infojet.translate("USER REGISTERED") + " <a href=\"" + infojet.webPage.getUrl() + "&userRegistration=login\">" + infojet.translate("USER REGISTERED 2") + "</a>";

            }

            if (Request["userRegistration"] == "login")
            {
                WebUserAccounts webUserAccounts = new WebUserAccounts(infojet.systemDatabase);
                WebUserAccount webUserAccount = webUserAccounts.getUserAccount(Session["registeredUserId"].ToString());

                if (infojet.authenticate(webUserAccount.userId, webUserAccount.password))
                {
                    infojet.redirect(infojet.webSite.getAuthenticatedStartPageUrl());
                }

            }


            webForm = new WebForm(infojet, "USER_REG");
            Hashtable formValues = new Hashtable();
            formValues.Add("SALESID", salesId.code);
            webForm.setFormValues(formValues);
            formPanel = webForm.getFormPanel();
            userRegForm.Controls.Add(formPanel);

            //submitBtn.ImageUrl = "../_assets/img/" + infojet.translate("IMG NEXT BTN");
            //submitBtn.AlternateText = infojet.translate("NEXT");
            submitBtn.ValidationGroup = webForm.code;
            submitBtn.Click +=new EventHandler(submitBtn_Click);
            //submitBtn.Click += new EventHandler(submitBtn_Click);
               
        }

        void submitBtn_Click(object sender, EventArgs e)
        {
            WebFormDocument webFormDocument = webForm.readForm(formPanel);

            ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojet, new ServiceRequest(infojet, "createSalesUser", webFormDocument));
            appServerConnection.processRequest();

            if (appServerConnection.serviceResponse.containsErrors)
            {
                this.errorMessage.Text = appServerConnection.getLastError();
            }
            else
            {
                if (Session["registeredUserId"] == null)
                {
                    Session.Add("registeredUserId", webFormDocument.valueList[getIndexForUserName(webFormDocument.keyList)].ToString());
                }
                else
                {
                    Session["registeredUserId"] = webFormDocument.valueList[getIndexForUserName(webFormDocument.keyList)].ToString();
                }

                if (webForm.confirmWebCategoryCode != "")
                {
                    WebPage webPage = infojet.webSite.getWebPageByCategory(webForm.confirmWebCategoryCode, infojet.userSession);
                    if (webPage != null) infojet.redirect(webPage.getUrl() + "&userRegistration=complete");
                }
                else
                {
                    WebPage webPage = infojet.webSite.getWebPageByCategory(infojet.webSite.authenticatedCategoryCode, infojet.userSession);
                    if (webPage != null) infojet.redirect(webPage.getUrl()+"&userRegistration=complete");
                }
            }

            
        }

        private int getIndexForUserName(ArrayList key)
        {
            DataSet webFormFieldDataSet = webForm.getFields();
            int i = 0;
            while (i < webFormFieldDataSet.Tables[0].Rows.Count)
            {
                WebFormField webFormField = new WebFormField(infojet, webFormFieldDataSet.Tables[0].Rows[i]);
                if (webFormField.connectionType == 19)
                {
                    int j = 0;
                    while (j < key.Count)
                    {
                        if (webFormField.code == key[j].ToString()) return j;
                        j++;
                    }

                }

                i++;
            }

            return 0;

        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;
        }

        #endregion
    }
}