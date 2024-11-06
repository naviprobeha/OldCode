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
    public partial class UserDefined_SENDORDERS_OVERVIEW : System.Web.UI.UserControl, InfojetUserControl
    {
        protected WebPageLine webPageLine;
        protected Navipro.Infojet.Lib.Infojet infojet;

        protected float grandTotalQuantity;
        protected float grandTotalAmount;
        protected string prevWebPageUrl;
        protected string sendUrl;
        protected WebCartHeader webCartHeader;

        protected void Page_Load(object sender, EventArgs e)
        {
            infojet = new Navipro.Infojet.Lib.Infojet();

            if (infojet.userSession == null) Response.End();

            SalesIDSetup salesIdSetup = new SalesIDSetup(infojet.systemDatabase);
            SalesIDs salesIds = new SalesIDs();

            WebPage prevWebPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeOrders2);
            prevWebPageUrl = prevWebPage.getUrl();
            sendUrl = infojet.webPage.getUrl() + "&command=sendOrder";

            webCartHeader = WebCartHeader.get();
            if (webCartHeader == null) webCartHeader = new WebCartHeader(infojet, infojet.sessionId);


            SalesIDCollection salesIdList = (SalesIDCollection)Session["salesIdList"];

            salesIdList.calcAmount(out grandTotalQuantity, out grandTotalAmount);
            grandTotalAmount = grandTotalAmount + webCartHeader.freightFee;

            int i = 0;
            while (i < salesIdList.Count)
            {
                SalesID salesId = salesIdList[i];
                salesId.applySessionIdToSalesIdCart(infojet.sessionId);
                i++;
            }

            salesIds.checkCartLines(infojet, salesIdList);

            if (Request["command"] == "sendOrder")
            {
                WebCartLines webCartLines = new WebCartLines(infojet.systemDatabase);
                webCartHeader.setCartLines(webCartLines.getCartLines(infojet.sessionId));


                ServiceRequest serviceRequest = new ServiceRequest(infojet, "createOrder", webCartHeader);
                infojet.systemDatabase.nonQuery("INSERT INTO [" + infojet.systemDatabase.getTableName("Web Order Message Queue") + "] ([Xml Document], [Customer No_], [To Process], [Done], [Created DateTime]) VALUES ('" + serviceRequest.getDocument().OuterXml + "', '"+webCartHeader.customerNo+"', 0, 0, GETDATE())");

                int entryNo = (int)infojet.systemDatabase.getInsertedSeqNo();
                string orderNo = "W" + entryNo.ToString().PadLeft(10, '0');
                directSubmitOrder(orderNo, webCartHeader);

                infojet.systemDatabase.nonQuery("UPDATE [" + infojet.systemDatabase.getTableName("Web Order Message Queue") + "] SET [To Process] = 1 WHERE [Entry No_] = '" + entryNo + "'");

                i = 0;
                while (i < salesIdList.Count)
                {
                    SalesID salesId = salesIdList[i];
                    salesId.deleteCartLines();
                    i++;
                }

                webCartHeader.delete();
                
                WebPage webPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeOrders4);
                infojet.redirect(webPage.getUrl() + "&orderNo=" + orderNo + "&docType=0&docNo=" + orderNo);

                /*    
                    ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojet, new ServiceRequest(infojet, "createOrder", webCartHeader));
                    if (!appServerConnection.processRequest())
                    {
                        errorMessageLabel.Text = appServerConnection.getLastError();
                    }
                    else
                    {
                        string lastOrderNoReceived = appServerConnection.serviceResponse.orderNo;


                        i = 0;
                        while (i < salesIdList.Count)
                        {
                            SalesID salesId = salesIdList[i];
                            salesId.deleteCartLines();
                            i++;
                        }

                        webCartHeader.delete();

                        WebPage webPage = new WebPage(infojet, infojet.webSite.code, salesIdSetup.webPageCodeOrders4);
                        infojet.redirect(webPage.getUrl() + "&orderNo=" + lastOrderNoReceived + "&docType=0&docNo=" + lastOrderNoReceived);

                    }
                */


            }


            salesIdRepeater.DataSource = salesIdList;
            salesIdRepeater.DataBind();

        }

        private void directSubmitOrder(string orderNo, Navipro.Infojet.Lib.WebCartHeader webCartHeader)
        {

            DatabaseQuery databaseQuery = null;
            Navipro.Infojet.Lib.Infojet infojetContext = infojet;

            try
            {
                WebCartLines webCartLines = new WebCartLines(infojetContext.systemDatabase);
                DataSet webCartLinesDataSet = webCartLines.getCartLines(infojetContext.sessionId, infojetContext.webSite.code);

                int i = 0;
                while (i < webCartLinesDataSet.Tables[0].Rows.Count)
                {
                    WebCartLine webCartLine = new WebCartLine(infojetContext, webCartLinesDataSet.Tables[0].Rows[i]);

                    databaseQuery = infojetContext.systemDatabase.prepare("INSERT INTO [" + infojetContext.systemDatabase.getTableName("Web Sales Line Backup") + "] (" +
                        "[Document Type], [Sell-to Customer No_], [Document No_], [Line No_], [Item No_], [Unit Of Measure Code], [Quantity], [Unit Price], " +
                        "[Line Discount %], [Amount], [Reference No_], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No_], " +
                        "[Received From Date], [Received To Date]) VALUES (" +

                        "1, @sellToCustomerNo, @no, @lineNo, @itemNo, @unitOfMeasureCode, @quantity, @unitPrice, " +
                        "@lineDiscount, @amount, @reference, @extra1, @extra2, @extra3, @extra4, extra5, " +
                        "@webUserAccountNo, '1753-01-01 00:00:00', '1753-01-01 00:00:00')");

                    databaseQuery.addStringParameter("@sellToCustomerNo", webCartHeader.customerNo.ToUpper(), 20);
                    databaseQuery.addStringParameter("@no", orderNo, 20);
                    databaseQuery.addIntParameter("@lineNo", (i + 1) * 10000);
                    databaseQuery.addStringParameter("@itemNo", webCartLine.itemNo.ToUpper(), 20);
                    databaseQuery.addStringParameter("@unitOfMeasureCode", webCartLine.unitOfMeasureCode, 20);
                    databaseQuery.addDecimalParameter("@quantity", webCartLine.quantity);
                    databaseQuery.addDecimalParameter("@unitPrice", webCartLine.unitPrice);
                    databaseQuery.addDecimalParameter("@lineDiscount", 0);
                    databaseQuery.addDecimalParameter("@amount", webCartLine.unitPrice * webCartLine.quantity);
                    databaseQuery.addStringParameter("@reference", webCartLine.referenceNo, 30);
                    databaseQuery.addStringParameter("@extra1", webCartLine.extra1, 30);
                    databaseQuery.addStringParameter("@extra2", webCartLine.extra2, 30);
                    databaseQuery.addStringParameter("@extra3", webCartLine.extra3, 30);
                    databaseQuery.addStringParameter("@extra4", webCartLine.extra4, 30);
                    databaseQuery.addStringParameter("@extra5", webCartLine.extra5, 30);
                    databaseQuery.addStringParameter("@webUserAccountNo", webCartLine.webUserAccountNo, 20);

                    databaseQuery.execute();

                    /*
                    databaseQuery = infojetContext.systemDatabase.prepare("INSERT INTO [" + infojetContext.systemDatabase.getTableName("Web Sales Line") + "] (" +
                        "[Document Type], [Sell-to Customer No_], [Document No_], [Line No_], [Item No_], [Unit Of Measure Code], [Quantity], [Unit Price], " +
                        "[Line Discount %], [Amount], [Reference No_], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No_], " +
                        "[Received From Date], [Received To Date]) VALUES (" +

                        "1, @sellToCustomerNo, @no, @lineNo, @itemNo, @unitOfMeasureCode, @quantity, @unitPrice, " +
                        "@lineDiscount, @amount, @reference, @extra1, @extra2, @extra3, @extra4, extra5, " +
                        "@webUserAccountNo, '1753-01-01 00:00:00', '1753-01-01 00:00:00')");

                    databaseQuery.addStringParameter("@sellToCustomerNo", webCartHeader.customerNo.ToUpper(), 20);
                    databaseQuery.addStringParameter("@no", orderNo, 20);
                    databaseQuery.addIntParameter("@lineNo", (i + 1) * 10000);
                    databaseQuery.addStringParameter("@itemNo", webCartLine.itemNo.ToUpper(), 20);
                    databaseQuery.addStringParameter("@unitOfMeasureCode", webCartLine.unitOfMeasureCode, 20);
                    databaseQuery.addDecimalParameter("@quantity", webCartLine.quantity);
                    databaseQuery.addDecimalParameter("@unitPrice", webCartLine.unitPrice);
                    databaseQuery.addDecimalParameter("@lineDiscount", 0);
                    databaseQuery.addDecimalParameter("@amount", webCartLine.unitPrice * webCartLine.quantity);
                    databaseQuery.addStringParameter("@reference", webCartLine.referenceNo, 30);
                    databaseQuery.addStringParameter("@extra1", webCartLine.extra1, 30);
                    databaseQuery.addStringParameter("@extra2", webCartLine.extra2, 30);
                    databaseQuery.addStringParameter("@extra3", webCartLine.extra3, 30);
                    databaseQuery.addStringParameter("@extra4", webCartLine.extra4, 30);
                    databaseQuery.addStringParameter("@extra5", webCartLine.extra5, 30);
                    databaseQuery.addStringParameter("@webUserAccountNo", webCartLine.webUserAccountNo, 20);

                    databaseQuery.execute();
                    */

                    i++;
                }


                /*
                databaseQuery = infojetContext.systemDatabase.prepare("INSERT INTO [" + infojetContext.systemDatabase.getTableName("Web Sales Header") + "] (" +
                    "[Document Type], [Sell-to Customer No_], [No_], [Web Site Code], [User Account No_], [Order Date], [Bill-to Name], [Bill-to Name 2], " +
                    "[Bill-to Address], [Bill-to Address 2], [Bill-to Post Code], [Bill-to City], [Bill-to Country Code], [Ship-to Name], [Ship-to Name 2], " +
                    "[Ship-to Address], [Ship-to Address 2], [Ship-to Post Code], [Ship-to City], [Ship-to Country Code], [Add Ship-to Address], [Contact Name], " +
                    "[Phone No_], [E-Mail], [Customer Order No_], [Note Of Goods], [Client IP Address], [Client User Agent], [Order Created Date Time], " +
                    "[Language Code], [Currency Code], [Web Payment Method Code], [Payment Reference No_], [Campaign Code], [Freight Fee], [Administrative Fee], " +
                    "[Freight G_L Account No_], [Admin G_L Account No_], [Freight VAT Prod Posting Group], [Admin VAT Prod Posting Group], [Shipping Agent Code], " +
                    "[Shipment Method Code], [Web Shipment Method Code], [Shipping Agent Service Code], [Shipping Advice Option], [Shipment Date], [Message Text], " +
                    "[Transfered], [Pre-Payment Received], [Shipped], [Invoiced], [Pre-Payment Expected], [Deleted], [Delegated To Company], [Delegated]) VALUES (" +

                    "1, @sellToCustomerNo, @no, @webSiteCode, @userAccountNo, @orderDate, @billToName, @billToName2, " +
                    "@billToAddress, @billToAddress2, @billToPostCode, @billToCity, @billToCountryCode, @shipToName, @shipToName2, " +
                    "@shipToAddress, @shipToAddress2, @shipToPostCode, @shipToCity, @shipToCountryCode, @addShipToAddress, @contactName, " +
                    "@phoneNo, @email, @customerOrderNo, @noteOfGoods, @clientIpAddress, @clientUserAgent, @orderCreatedDateTime, " +
                    "@languageCode, @currencyCode, @webPaymentMethodCode, '', @campaignCode, @freightFee, @administrativeFee, " +
                    "@freightGLAccountNo, @adminGLAccountNo, @freightVATProdPostingGroup, @adminVATProdPostingGroup, @shippingAgentCode, " +
                    "@shipmentMethodCode, @webShipmentMethodCode, @shippingAgentServiceCode, @shippingAdviceOption, @shipmentDate, @messageText, " +
                    "0, 0, 0, 0, @prepaymentExptected, @deleted, '', 0)");


                databaseQuery.addStringParameter("@sellToCustomerNo", webCartHeader.customerNo.ToUpper(), 20);
                databaseQuery.addStringParameter("@no", orderNo, 20);
                databaseQuery.addStringParameter("@webSiteCode", "NEWBODY", 20);
                databaseQuery.addStringParameter("@userAccountNo", webCartHeader.userAccountNo.ToUpper(), 20);
                databaseQuery.addDateTimeParameter("@orderDate", DateTime.Today);
                databaseQuery.addStringParameter("@billToName", webCartHeader.billToName, 50);
                databaseQuery.addStringParameter("@billToName2", webCartHeader.billToName2, 50);
                databaseQuery.addStringParameter("@billToAddress", webCartHeader.billToAddress, 50);
                databaseQuery.addStringParameter("@billToAddress2", webCartHeader.billToAddress2, 50);
                databaseQuery.addStringParameter("@billToPostCode", webCartHeader.billToPostCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@billToCity", webCartHeader.billToCity, 50);
                databaseQuery.addStringParameter("@billToCountryCode", webCartHeader.billToCountryCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@shipToName", webCartHeader.shipToName, 50);
                databaseQuery.addStringParameter("@shipToName2", webCartHeader.shipToName2, 50);
                databaseQuery.addStringParameter("@shipToAddress", webCartHeader.shipToAddress, 50);
                databaseQuery.addStringParameter("@shipToAddress2", webCartHeader.shipToAddress2, 50);
                databaseQuery.addStringParameter("@shipToPostCode", webCartHeader.shipToPostCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@shipToCity", webCartHeader.shipToCity, 50);
                databaseQuery.addStringParameter("@shipToCountryCode", webCartHeader.shipToCountryCode.ToUpper(), 20);
                databaseQuery.addBoolParameter("@addShipToAddress", webCartHeader.addShipToAddress);
                databaseQuery.addStringParameter("@contactName", webCartHeader.contactName, 30);
                databaseQuery.addStringParameter("@phoneNo", webCartHeader.phoneNo, 20);
                databaseQuery.addStringParameter("@email", webCartHeader.email, 50);
                databaseQuery.addStringParameter("@customerOrderNo", webCartHeader.customerOrderNo.ToUpper(), 20);
                databaseQuery.addStringParameter("@noteOfGoods", webCartHeader.noteOfGoods, 100);
                databaseQuery.addStringParameter("@clientIpAddress", webCartHeader.clientIpAddress, 30);
                databaseQuery.addStringParameter("@clientUserAgent", webCartHeader.clientUserAgent, 250);
                databaseQuery.addDateTimeParameter("@orderCreatedDateTime", DateTime.Now);
                databaseQuery.addStringParameter("@languageCode", infojet.languageCode, 20);
                databaseQuery.addStringParameter("@currencyCode", webCartHeader.currencyCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@webPaymentMethodCode", "", 20);
                databaseQuery.addStringParameter("@campaignCode", "", 20);
                databaseQuery.addDecimalParameter("@freightFee", webCartHeader.freightFee);
                databaseQuery.addDecimalParameter("@administrativeFee", 0);
                databaseQuery.addStringParameter("@freightGLAccountNo", "", 20);
                databaseQuery.addStringParameter("@adminGLAccountNo", "", 20);
                databaseQuery.addStringParameter("@freightVATProdPostingGroup", "", 20);
                databaseQuery.addStringParameter("@adminVATProdPostingGroup", "", 20);
                databaseQuery.addStringParameter("@shippingAgentCode", "", 20);
                databaseQuery.addStringParameter("@shipmentMethodCode", "", 20);
                databaseQuery.addStringParameter("@webShipmentMethodCode", webCartHeader.webShipmentMethodCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@shippingAgentServiceCode", "", 20);

                int shippingAdvice = 0;
                if (webCartHeader.shippingAdvice == "1") shippingAdvice = 1;
                databaseQuery.addIntParameter("@shippingAdviceOption", shippingAdvice);


                databaseQuery.addDateTimeParameter("@shipmentDate", webCartHeader.shipmentDate);
                databaseQuery.addStringParameter("@messageText", webCartHeader.message, 250);

                int prepaymentExpected = 0;
                //if (webCartHeader.webPaymentMethod.type > 0) prepaymentExpected = 1;
                databaseQuery.addIntParameter("@prepaymentExptected", prepaymentExpected);
                databaseQuery.addIntParameter("@deleted", prepaymentExpected);

                databaseQuery.execute();
                */
            }
            catch (Exception e)
            {
                try
                {
                    databaseQuery = infojetContext.systemDatabase.prepare("DELETE FROM [" + infojetContext.systemDatabase.getTableName("Web Sales Header Extra Field") + "] WHERE [Document No_] = @no");
                    databaseQuery.addStringParameter("@no", orderNo, 20);
                    databaseQuery.execute();

                    databaseQuery = infojetContext.systemDatabase.prepare("DELETE FROM [" + infojetContext.systemDatabase.getTableName("Web Sales Line") + "] WHERE [Document No_] = @no");
                    databaseQuery.addStringParameter("@no", orderNo, 20);
                    databaseQuery.execute();
                }
                catch (Exception) { }

                throw new Exception(e.Message);
            }
            infojetContext.systemDatabase.close();

        }

        #region InfojetUserControl Members

        public void setWebPageLine(WebPageLine webPageLine)
        {
            this.webPageLine = webPageLine;

        }

        #endregion
    }
}