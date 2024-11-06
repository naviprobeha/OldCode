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
    public partial class UserDefined_SENDORDERS_DELIVERY : System.Web.UI.UserControl, InfojetUserControl
    {
        protected WebPageLine webPageLine;
        protected Navipro.Infojet.Lib.Infojet infojet;

        protected float totalQuantity;
        protected float totalAmount;
        protected string prevWebPageUrl;
        protected string nextWebPageUrl;
        protected string sendUrl;
        protected string packages;
        protected WebCartHeader webCartHeader;


        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();

            if (infojet.userSession == null) Response.End();

            SalesIDSetup salesIdSetup = new SalesIDSetup(infojet.systemDatabase);
            SalesIDs salesIds = new SalesIDs();

            SalesIDCollection salesIdCollection = salesIds.getContactSalesIdCollection(infojet, infojet.userSession.webUserAccount);
            if (salesIdCollection.Count > 1)
            {
                WebPage webPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeOrders1);
                prevWebPageUrl = webPage.getUrl();
            }
            else
            {
                SalesID salesId = (SalesID)salesIdCollection[0];
                if (salesId.nextOrderType < 3)
                {
                    if (salesId.showCase != "")
                    {
                        WebPage webPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeShowCase);
                        prevWebPageUrl = webPage.getUrl();
                    }
                    else
                    {
                        WebPage webPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeReOrders);
                        prevWebPageUrl = webPage.getUrl();
                    }
                }
                else
                {
                    WebPage webPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeCombine);
                    prevWebPageUrl = webPage.getUrl();
                }
            }

            WebPage nextWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeOrders3);
            nextWebPageUrl = nextWebPage.getUrl();

            sendUrl = infojet.webPage.getUrl() + "&command=applyDelivery";

            SalesIDCollection salesIdList = (SalesIDCollection)Session["salesIdList"];
            //int packageCount = salesIdList.calcSoldPackages();
            int packageCount = salesIdList.calcSoldPackagesInclGiftCards();
            //int packageCount = salesIdList.calcPackagesToOrder();
            this.packages = packageCount.ToString();



            WebShipmentMethods webShipmentMethods = new WebShipmentMethods(infojet);
            WebShipmentMethodCollection webShipmentMethodCollection = webShipmentMethods.getWebShipmentMethodCollection(infojet.webSite.code, packageCount, 0, 0, infojet.languageCode);


            if (salesIdCollection.Count == 1)
            {
                SalesID salesId = salesIdCollection[0];
                if (salesId.checkReorderingFreeFreight())
                {
                    int i = 0;
                    while (i < webShipmentMethodCollection.Count)
                    {
                        webShipmentMethodCollection[i].formatedAmount = "0,00";
                        webShipmentMethodCollection[i].amount = 0;
                        i++;
                    }
                }
            }


            webCartHeader = WebCartHeader.get();
            if (webCartHeader == null)
            {
                webCartHeader = new WebCartHeader(infojet, infojet.sessionId);
                webCartHeader.sessionId = infojet.sessionId;
                webCartHeader.setUserSession(infojet.userSession);
                if (webCartHeader.shipToName == "")
                {
                    webCartHeader.shipToName = webCartHeader.billToName;
                    webCartHeader.shipToName2 = webCartHeader.billToName2;
                    webCartHeader.shipToAddress = webCartHeader.billToAddress;
                    webCartHeader.shipToAddress2 = webCartHeader.billToAddress2;
                    webCartHeader.shipToPostCode = webCartHeader.billToPostCode;
                    webCartHeader.shipToCity = webCartHeader.billToCity;
                }
                webCartHeader.phoneNo = infojet.userSession.webUserAccount.cellPhoneNo;

            }
            
            //throw new Exception("Post code: " + infojet.userSession.customer.postCode);


            //webShipmentMethodCollection = checkPostCodeDayAndNight(webShipmentMethodCollection, webCartHeader.shipToPostCode);
            webShipmentMethodCollection = checkPostCodeDayAndNight(webShipmentMethodCollection, infojet.userSession.customer.postCode);
            shipmentMethodRepeater.DataSource = webShipmentMethodCollection;
            shipmentMethodRepeater.DataBind();

            if (Request["command"] == "applyDelivery")
            {
                webCartHeader = new WebCartHeader(infojet, infojet.sessionId);
                webCartHeader.sessionId = infojet.sessionId;
                webCartHeader.setUserSession(infojet.userSession);

                int i = 0;
                while (i < webShipmentMethodCollection.Count)
                {
                    if (Request["webShipmentMethodCode"] == webShipmentMethodCollection[i].code)
                    {
                        webCartHeader.applyWebShipmentMethod(webShipmentMethodCollection[i]);
                    }
                    i++;
                }

 
                webCartHeader.shipToName = Request["shipToName"];
                webCartHeader.shipToAddress = Request["shipToAddress"];
                webCartHeader.shipToAddress2 = Request["shipToAddress2"];
                webCartHeader.shipToPostCode = Request["shipToPostCode"];
                webCartHeader.shipToCity = Request["shipToCity"];                

                webCartHeader.phoneNo = Request["phoneNo"];
                webCartHeader.email = Request["email"];
                webCartHeader.message = Request["message"];
                webCartHeader.save();

                infojet.redirect(nextWebPageUrl);
            }

            if (Request["command"] == "updateDelivery")
            {
                webCartHeader = new WebCartHeader(infojet, infojet.sessionId);
                webCartHeader.sessionId = infojet.sessionId;
                webCartHeader.setUserSession(infojet.userSession);

                webCartHeader.shipToName = Request["shipToName"];
                webCartHeader.shipToAddress = Request["shipToAddress"];
                webCartHeader.shipToAddress2 = Request["shipToAddress2"];
                webCartHeader.shipToPostCode = Request["shipToPostCode"];
                webCartHeader.shipToCity = Request["shipToCity"];

                if (webCartHeader.shipToPostCode.Length == 5) webCartHeader.shipToPostCode = webCartHeader.shipToPostCode.Substring(0, 3) + " " + webCartHeader.shipToPostCode.Substring(3, 2);

                webCartHeader.phoneNo = Request["phoneNo"];
                webCartHeader.email = Request["email"];
                webCartHeader.message = Request["message"];
                webCartHeader.save();

                webShipmentMethodCollection = webShipmentMethods.getWebShipmentMethodCollection(infojet.webSite.code, packageCount, 0, 0, infojet.languageCode);
                
                //webShipmentMethodCollection = checkPostCodeDayAndNight(webShipmentMethodCollection, webCartHeader.shipToPostCode);
                webShipmentMethodCollection = checkPostCodeDayAndNight(webShipmentMethodCollection, infojet.userSession.customer.postCode);

                shipmentMethodRepeater.DataSource = webShipmentMethodCollection;
                shipmentMethodRepeater.DataBind();

                int i = 0;
                while (i < webShipmentMethodCollection.Count)
                {
                    if (Request["webShipmentMethodCode"] == webShipmentMethodCollection[i].code)
                    {
                        webCartHeader.applyWebShipmentMethod(webShipmentMethodCollection[i]);
                    }
                    i++;
                }


            }
           

        }

        private WebShipmentMethodCollection checkPostCodeDayAndNight(WebShipmentMethodCollection webShipmentMethodCollection, string shipToPostCode)
        {
            int preCount = webShipmentMethodCollection.Count;

            int i = 0;
            while (i < webShipmentMethodCollection.Count)
            {
                if (!checkShipmentMethod(infojet, webShipmentMethodCollection[i], shipToPostCode))
                {
                    webShipmentMethodCollection.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            //if (shipToPostCode == "984 95") throw new Exception("Före: " + preCount + ", Efter: " + webShipmentMethodCollection.Count);
            return webShipmentMethodCollection;
        }

        public static bool checkShipmentMethod(Navipro.Infojet.Lib.Infojet infojetContext, WebShipmentMethod webShipmentMethod, string postCode)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Daytime Delivery], [Nighttime Delivery], [Shipment Group Code] FROM [" + infojetContext.systemDatabase.getTableName("Web Shipment Method") + "] WHERE [Web Site Code] = @webSiteCode AND [Code] = @webShipmentMethodCode");
            databaseQuery.addStringParameter("@webSiteCode", webShipmentMethod.webSiteCode, 20);
            databaseQuery.addStringParameter("@webShipmentMethodCode", webShipmentMethod.code, 20);

            System.Data.SqlClient.SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            int i = 0;
            bool dayTime = false;
            bool nightTime = false;
            string shipmentGroupCode = "";

            while (i < dataSet.Tables[0].Rows.Count)
            {
                if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() == "1") dayTime = true;
                if (dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() == "1") nightTime = true;
                shipmentGroupCode = dataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString();

                i++;
            }

            if (shipmentGroupCode == "") return true;

            databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Day], [Night] FROM [" + infojetContext.systemDatabase.getTableName("Web ShipAgent Postcode Rel") + "] WHERE [AgentId] = @shippingAgentCode AND [PostCode] = @postCode AND [Shipment Group Code] = @shipmentGroupCode");
            databaseQuery.addStringParameter("@shippingAgentCode", webShipmentMethod.shippingAgentCode, 20);
            databaseQuery.addStringParameter("@postCode", postCode, 20);
            databaseQuery.addStringParameter("@shipmentGroupCode", shipmentGroupCode, 20);

            dataAdapter = databaseQuery.executeDataAdapterQuery();
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            bool success = false;

            i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                if ((dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() == "1") && (dayTime)) success = true;
                if ((dataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() == "1") && (nightTime)) success = true;

                i++;
            }

            //if ((postCode == "984 95") && (webShipmentMethod.code == "DHL ASP2")) throw new Exception("Group: " + shipmentGroupCode + ", day: " + dayTime + ", night: " + nightTime + ", i: " + i);

            if ((dayTime == false) && (nightTime == false) && (i == 0)) return true;

            //if (postCode == "984 95") throw new Exception("Metod: " + webShipmentMethod.code + ", Agent: "+webShipmentMethod.shippingAgentCode+", Postcode: "+postCode+", Dagtid: " + dayTime + ", Kvällstid: " + nightTime+", Success: "+success);


            return success;
        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;

        }

        #endregion
    }
}