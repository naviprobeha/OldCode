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

namespace WebInterface._taglib
{
    public partial class UserDefined_PROGRESS : System.Web.UI.UserControl, InfojetUserControl
    {
        protected WebPageLine webPageLine;
        protected Navipro.Infojet.Lib.Infojet infojet;
        protected int noOfContactSalesIds;

        protected string header1;
        protected string header2;
        protected string header3;
        protected string header4;
        protected string header5;
        protected string header6;
        protected string progressBarImg;
        protected string width1;
        protected string width2;
        protected string width3;
        protected string width4;
        protected string width5;
        protected string width6;

        protected string separatorWidth;

        protected void Page_Load(object sender, EventArgs e)
        {

            infojet = new Navipro.Infojet.Lib.Infojet();
            SalesIDSetup salesIdSetup = new SalesIDSetup(infojet.systemDatabase);

            SalesIDs salesIds = new SalesIDs();
            SalesID salesId = null;
            if ((Request["salesId"] != null) && (Request["salesId"] != ""))
            {
                salesId = new SalesID(infojet, Request["salesId"]);
            }
            else
            {
                salesId = (SalesID)Session["currentSalesId"];
                if (salesId == null)
                {
                    SalesIDCollection salesIdList = (SalesIDCollection)Session["salesIdList"];
                    if (salesIdList == null) Response.End();

                    salesId = (SalesID)salesIdList[0];
                }
            }


            SalesIDCollection salesIdCollection = salesIds.getContactSalesIdCollection(infojet, infojet.userSession.webUserAccount);
            noOfContactSalesIds = salesIdCollection.Count;

            if (noOfContactSalesIds == 1)
            {
                if ((salesId.showCase != "") && (salesId.nextOrderType < 3))
                {
                    progressBarPanel.Visible = true;
                    string pageNo = "";
                    if (Request["pageCode"] == salesIdSetup.webPageCodeCombine) pageNo = "1";
                    if (Request["pageCode"] == salesIdSetup.webPageCodeReOrders) pageNo = "2";
                    if (Request["pageCode"] == salesIdSetup.webPageCodeShowCase) pageNo = "3";
                    if (Request["pageCode"] == salesIdSetup.webPageCodeOrders2) pageNo = "4";
                    if (Request["pageCode"] == salesIdSetup.webPageCodeOrders3) pageNo = "5";
                    if (Request["pageCode"] == salesIdSetup.webPageCodeOrders4) pageNo = "6";

                    progressBarImg = "progressBar_bg_D_" + pageNo + ".jpg";

                    header1 = infojet.translate("BAR A1");
                    header2 = infojet.translate("BAR A2");
                    header3 = infojet.translate("BAR A3");
                    header4 = infojet.translate("BAR A4");
                    header5 = infojet.translate("BAR A5");
                    header6 = infojet.translate("BAR A6");

                    width1 = "150px";
                    width2 = "150px";
                    width3 = "150px";
                    width4 = "150px";
                    width5 = "150px";
                    width6 = "150px";

                    separatorWidth = "20px";
                }

                if ((salesId.showCase == "") && (salesId.nextOrderType < 3)) 
                {
                    progressBarPanel.Visible = true;
                    string pageNo = "";
                    
                    if (Request["pageCode"] == salesIdSetup.webPageCodeCombine) pageNo = "1";
                    if (Request["pageCode"] == salesIdSetup.webPageCodeReOrders) pageNo = "2";
                    if (Request["pageCode"] == salesIdSetup.webPageCodeOrders2) pageNo = "3";
                    if (Request["pageCode"] == salesIdSetup.webPageCodeOrders3) pageNo = "4";
                    if (Request["pageCode"] == salesIdSetup.webPageCodeOrders4) pageNo = "5";

                    progressBarImg = "progressBar_bg_C_" + pageNo + ".jpg";

                    header1 = infojet.translate("BAR A1");
                    header2 = infojet.translate("BAR A2");
                    header3 = infojet.translate("BAR A4");
                    header4 = infojet.translate("BAR A5");
                    header5 = infojet.translate("BAR A6");
                    header6 = "";

                    width1 = "150px";
                    width2 = "150px";
                    width3 = "150px";
                    width4 = "150px";
                    width5 = "150px";
                    width6 = "10px";

                    separatorWidth = "43px";

                }

                if (salesId.nextOrderType > 2)
                {
                    progressBarPanel.Visible = true;
                    string pageNo = "";

                    if (Request["pageCode"] == salesIdSetup.webPageCodeCombine) pageNo = "1";
                    if (Request["pageCode"] == salesIdSetup.webPageCodeOrders2) pageNo = "2";
                    if (Request["pageCode"] == salesIdSetup.webPageCodeOrders3) pageNo = "3";
                    if (Request["pageCode"] == salesIdSetup.webPageCodeOrders4) pageNo = "4";

                    progressBarImg = "progressBar_bg_B_" + pageNo + ".jpg";

                    header1 = infojet.translate("BAR A1");
                    header2 = infojet.translate("BAR A4");
                    header3 = infojet.translate("BAR A5");
                    header4 = infojet.translate("BAR A6");
                    header5 = "";
                    header6 = "";

                    width1 = "150px";
                    width2 = "150px";
                    width3 = "150px";
                    width4 = "150px";
                    width5 = "10px";
                    width6 = "10px";

                    separatorWidth = "43px";

                }

                if (salesId.isSubContactPerson(infojet.userSession.webUserAccount.no)) progressBarPanel.Visible = false;
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