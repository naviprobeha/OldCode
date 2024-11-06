using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using Navipro.Infojet.Lib;

namespace Navipro.Infojet.WebService
{
    /// <summary>
    /// Summary description for WebSiteService
    /// </summary>
    [WebService(Namespace = "http://infojet.navipro.se/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebCheckoutService : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public void submitOrder(string orderNo, Navipro.Infojet.Lib.WebCartHeader webCartHeader, System.Data.DataSet cartLineDataSet)
        {
            Global.init();

            Navipro.Infojet.Lib.Infojet infojetContext = new Navipro.Infojet.Lib.Infojet();

            DatabaseQuery databaseQuery = null;

            try
            {
                int i = 0;
                while (i < cartLineDataSet.Tables[0].Rows.Count)
                {
                    WebCartLine webCartLine = new WebCartLine(infojetContext, cartLineDataSet.Tables[0].Rows[i]);

                    databaseQuery = infojetContext.systemDatabase.prepare("INSERT INTO [" + infojetContext.systemDatabase.getTableName("Web Sales Line") + "] (" +
                        "[Document Type], [Sell-to Customer No_], [Document No_], [Line No_], [Item No_], [Unit Of Measure Code], [Quantity], [Unit Price], " +
                        "[Line Discount %], [Amount], [Reference], [Extra 1], [Extra 2], [Extra 3], [Extra 4], [Extra 5], [Web User Account No_], " +
                        "[Received From Date], [Received To Date]) VALUES (" +

                        "1, @sellToCustomerNo, @no, @lineNo, @itemNo, @unitOfMeasureCode, @quantity, @unitPrice, " +
                        "@lineDiscount, @amount, @reference, @extra1, @extra2, @extra3, @extra4, extra5, " +
                        "@webUserAccountNo, '1753-01-01 00:00:00', '1753-01-01 00:00:00')");

                    databaseQuery.addStringParameter("@sellToCustomerNo", webCartHeader.customerNo.ToUpper(), 20);
                    databaseQuery.addStringParameter("@no", orderNo, 20);
                    databaseQuery.addIntParameter("@lineNo", (i + 1) * 10000);
                    databaseQuery.addStringParameter("@itemNo", webCartLine.itemNo.ToUpper(), 20);
                    databaseQuery.addStringParameter("@unitOfMeasureCode", webCartLine.unitOfMeasureCode.ToUpper(), 20);
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
                    databaseQuery.addStringParameter("@webUserAccountNo", webCartLine.webUserAccountNo.ToUpper(), 20);

                    databaseQuery.execute();

                    i++;
                }

                i = 0;
                while (i < webCartHeader.extraFields.Count)
                {
                    databaseQuery = infojetContext.systemDatabase.prepare("INSERT INTO [" + infojetContext.systemDatabase.getTableName("Web Sales Header Extra Field") + "] (" +
                        "[Document Type], [Document No_], [Form Code], [Field Code], [Value]) VALUES (" +
                        "1, @no, @formCode, @fieldCode, @value)");

                    databaseQuery.addStringParameter("@no", orderNo, 20);
                    databaseQuery.addStringParameter("@formCode", webCartHeader.extraFields[i].webFormCode, 20);
                    databaseQuery.addStringParameter("@fieldCode", webCartHeader.extraFields[i].fieldCode, 20);
                    databaseQuery.addStringParameter("@value", webCartHeader.extraFields[i].value, 250);

                    databaseQuery.execute();

                    i++;
                }


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
                databaseQuery.addStringParameter("@webSiteCode", webCartHeader.webSiteCode.ToUpper(), 20);
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
                databaseQuery.addStringParameter("@languageCode", webCartHeader.languageCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@currencyCode", webCartHeader.currencyCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@webPaymentMethodCode", webCartHeader.webPaymentMethodCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@campaignCode", webCartHeader.campaignCode.ToUpper(), 20);
                databaseQuery.addDecimalParameter("@freightFee", webCartHeader.freightFee);
                databaseQuery.addDecimalParameter("@administrativeFee", webCartHeader.adminFee);
                databaseQuery.addStringParameter("@freightGLAccountNo", webCartHeader.webShipmentMethod.glAccountNo.ToUpper(), 20);
                databaseQuery.addStringParameter("@adminGLAccountNo", webCartHeader.webPaymentMethod.glAccountNo.ToUpper(), 20);
                databaseQuery.addStringParameter("@freightVATProdPostingGroup", webCartHeader.webShipmentMethod.vatProdPostingGroup.ToUpper(), 20);
                databaseQuery.addStringParameter("@adminVATProdPostingGroup", webCartHeader.webPaymentMethod.vatProdPostingGroup.ToUpper(), 20);
                databaseQuery.addStringParameter("@shippingAgentCode", webCartHeader.webShipmentMethod.shippingAgentCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@shipmentMethodCode", webCartHeader.webShipmentMethod.shipmentMethodCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@webShipmentMethodCode", webCartHeader.webShipmentMethodCode.ToUpper(), 20);
                databaseQuery.addStringParameter("@shippingAgentServiceCode", webCartHeader.webShipmentMethod.shippingAgentServiceCode.ToUpper(), 20);

                int shippingAdvice = 0;
                if (webCartHeader.shippingAdvice == "1") shippingAdvice = 1;
                databaseQuery.addIntParameter("@shippingAdviceOption", shippingAdvice);


                databaseQuery.addDateTimeParameter("@shipmentDate", webCartHeader.shipmentDate);
                databaseQuery.addStringParameter("@messageText", webCartHeader.message, 250);

                int prepaymentExpected = 0;
                if (webCartHeader.webPaymentMethod.type > 0) prepaymentExpected = 1;
                databaseQuery.addIntParameter("@prepaymentExptected", prepaymentExpected);
                databaseQuery.addIntParameter("@deleted", prepaymentExpected);

                databaseQuery.execute();

            }
            catch (Exception e)
            {
                databaseQuery = infojetContext.systemDatabase.prepare("DELETE FROM [" + infojetContext.systemDatabase.getTableName("Web Sales Header Extra Field") + "] WHERE [Document No_] = @no");
                databaseQuery.addStringParameter("@no", orderNo, 20);
                databaseQuery.execute();

                databaseQuery = infojetContext.systemDatabase.prepare("DELETE FROM [" + infojetContext.systemDatabase.getTableName("Web Sales Line") + "] WHERE [Document No_] = @no");
                databaseQuery.addStringParameter("@no", orderNo, 20);
                databaseQuery.execute();

                throw new Exception(e.Message);
            }
            infojetContext.systemDatabase.close();

        }

    }
}
