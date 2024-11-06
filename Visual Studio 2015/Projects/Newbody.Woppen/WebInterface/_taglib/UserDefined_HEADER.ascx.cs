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
using Navipro.Newbody.Woppen.Library;

namespace Navipro.Infojet.WebInterface._taglib
{
    public partial class UserDefined_HEADER : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Navipro.Infojet.Lib.Infojet infojet = new Navipro.Infojet.Lib.Infojet();

            if (infojet.webPage.getUrl() == infojet.webSite.getAuthenticatedStartPageUrl()) Session.Remove("currentSalesId");

            if (infojet.userSession == null) infojet.redirect(infojet.webSite.getStartPageUrl());

            SalesID salesId = null;

            SalesIDs salesIds = new SalesIDs();
            if ((Request["salesId"] != null) && (Request["salesId"] != ""))
            {
                salesId = new SalesID(infojet, Request["salesId"]);
                //role.Text = infojet.translate("CONTACTPERSON");
                Navipro.Infojet.Lib.WebPageMenuText webPageMenuText = infojet.webPage.getMenuText(infojet.languageCode);
                role.Text = webPageMenuText.menuText;

            }
            else
            {
                if (Session["currentSalesId"] != null)
                {
                    salesId = (SalesID)Session["currentSalesId"];
                    //role.Text = infojet.translate("CONTACTPERSON");
                    Navipro.Infojet.Lib.WebPageMenuText webPageMenuText = infojet.webPage.getMenuText(infojet.languageCode);
                    role.Text = webPageMenuText.menuText;
                }
                else
                {
                    salesId = salesIds.getSalesPersonSalesId(infojet, infojet.userSession.webUserAccount);
                    if (salesId == null)
                    {
                        salesId = salesIds.getUserRegSalesId(infojet, infojet.userSession.webUserAccount);
                        role.Text = infojet.translate("USER REGISTRATION");
                    }
                    else
                    {
                        if (salesId.contactWebUserAccountNo != infojet.userSession.webUserAccount.no)
                        {
                            role.Text = infojet.translate("SALESPERSON");
                        }
                        else
                        {

                            //role.Text = infojet.translate("CONTACTPERSON");
                            Navipro.Infojet.Lib.WebPageMenuText webPageMenuText = infojet.webPage.getMenuText(infojet.languageCode);
                            role.Text = webPageMenuText.menuText;
                            salesId = null;
                        }
                    }
                }
            }

            if (infojet.userSession != null)
            {
                userName.Text = infojet.userSession.webUserAccount.name;
            }
            if (salesId != null)
            {
                if (salesId.description == "") salesId.description = salesId.code;
                userName.Text = userName.Text + ", " + salesId.description;
            }

        }
    }
}