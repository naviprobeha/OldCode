using System;
using System.IO;
using System.Data;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for PaymentDibs.
	/// </summary>
	public class PaymentModuleDibs : PaymentModule
	{
		private Infojet infojetContext;
        private WebCheckout webCheckout;
        

        public PaymentModuleDibs(Infojet infojetContext, WebCheckout webCheckout)
		{
			//
			// TODO: Add constructor logic here
			//
			this.infojetContext = infojetContext;
            this.webCheckout = webCheckout;
		}

		public string getUrl()
		{
            if (System.Web.HttpContext.Current.Request["paymentStatus"] == "OK")
            {
                string paymentOrderNo = webCheckout.webCartHeader.paymentOrderNo;
                webCheckout.webCartHeader.deleteLines();
                webCheckout.webCartHeader.delete();

                WebPage webPage = new WebPage(infojetContext, infojetContext.webSite.code, webCheckout.orderConfirmationWebPageCode);
                infojetContext.redirect(webPage.getUrl() + "&orderNo=" + paymentOrderNo + "&docType=0&docNo="+ paymentOrderNo);
            }

			string firstName = "";
			string lastName = "";

			if (webCheckout.webCartHeader.billToName.IndexOf(" ") > -1)
			{
                firstName = webCheckout.webCartHeader.billToName.Substring(0, webCheckout.webCartHeader.billToName.IndexOf(" ") - 1);
                lastName = webCheckout.webCartHeader.billToName.Substring(webCheckout.webCartHeader.billToName.IndexOf(" ") + 1);
			}
			else
			{
                firstName = webCheckout.webCartHeader.billToName;
				lastName = " ";
			}

            if (webCheckout.webCartHeader.currencyCode == "") webCheckout.webCartHeader.currencyCode = infojetContext.generalLedgerSetup.lcyCode;

            string url = System.Configuration.ConfigurationSettings.AppSettings["DIBS_Url"] +
                "?billingFirstName=" + htmlEncode(firstName) +
                "&billingLastName=" + htmlEncode(lastName) +
                "&eMail=" + htmlEncode(webCheckout.webCartHeader.email) +
                "&pageSet=" + webCheckout.webCartHeader.webPaymentMethod.serviceParameter +
                "&billingAddress=" + htmlEncode(webCheckout.webCartHeader.billToAddress) +
                "&billingPostCode=" + htmlEncode(webCheckout.webCartHeader.billToPostCode) +
                "&billingCity=" + htmlEncode(webCheckout.webCartHeader.billToCity) +
                "&billingCountry=" + htmlEncode(webCheckout.webCartHeader.billToCountryCode) +
                "&currency=" + webCheckout.webCartHeader.currencyCode +
                "&totalText=" + htmlEncode(infojetContext.translate("SUBTOTAL")) +
                "&instructionText=" + htmlEncode(infojetContext.translate("CARD INSTRUCTIONS")) +
                "&creditCardText=" + htmlEncode(infojetContext.translate("CREDIT CARD NO")) +
                "&goodThruText=" + htmlEncode(infojetContext.translate("GOOD THRU")) +
                "&cvcText=" + htmlEncode(infojetContext.translate("CVC")) +
                "&nextText=" + htmlEncode(infojetContext.translate("PROCESS PAYMENT")) +
                "&nameText=" + htmlEncode(infojetContext.translate("NAME")) +
                "&addressText=" + htmlEncode(infojetContext.translate("ADDRESS")) +
                "&postalText=" + htmlEncode(infojetContext.translate("POST ADDRESS")) +
                "&countryText=" + htmlEncode(infojetContext.translate("COUNTRY")) +
                "&phoneText=" + htmlEncode(infojetContext.translate("PHONE NO")) +
                "&emailText=" + htmlEncode(infojetContext.translate("EMAIL")) +
                "&orderText=" + htmlEncode(infojetContext.translate("PURCHASE")) +
                "&itemNoText=" + htmlEncode(infojetContext.translate("ITEM NO")) +
                "&descriptionText=" + htmlEncode(infojetContext.translate("DESCRIPTION")) +
                "&quantityText=" + htmlEncode(infojetContext.translate("QUANTITY")) +
                "&unitPriceText=" + htmlEncode(infojetContext.translate("UNIT PRICE")) +
                "&amountText=" + htmlEncode(infojetContext.translate("AMOUNT")) +
                "&declineMessage=" + htmlEncode(infojetContext.translate("CARD DECLINED")) +
                "&orderNoText=" + htmlEncode(infojetContext.translate("ORDER NO")) +
                "&dibsReferenceText=" + htmlEncode(infojetContext.translate("DIBS REFERENCE NO")) +
                "&errorMessageText=" + htmlEncode(infojetContext.translate("ERROR MESSAGE")) +
                "&referenceNo=" + htmlEncode(webCheckout.webCartHeader.paymentOrderNo) +
                "&orderNo=" + htmlEncode(webCheckout.webCartHeader.paymentOrderNo) +
                "&customerNo=" + htmlEncode(webCheckout.webCartHeader.customerNo) +
                "&returnUrl=" + htmlEncode(infojetContext.webPage.getUrl())+
                "&webSiteCode=" + htmlEncode(infojetContext.webSite.code) +
                "&pageCode=" + htmlEncode(infojetContext.webPage.code) +
                "&method=" + webCheckout.webCartHeader.webPaymentMethod.serviceCode;

            System.Web.HttpContext.Current.Session.Add("cardPayment_returnUrl", infojetContext.webPage.getUrl());

			string data = "";
            int totalPrice = 0;
			WebCartLines webCartLines = new WebCartLines(infojetContext);
            DataSet webCartLineDataSet = webCartLines.getCartLines(webCheckout.webCartHeader.sessionId, infojetContext.webSite.code);
			int i = 0;
			while (i < webCartLineDataSet.Tables[0].Rows.Count)
			{
                WebCartLine webCartLine = new WebCartLine(infojetContext, webCartLineDataSet.Tables[0].Rows[i]);

				//Item item = new Item(infojetContext, webCartLine.itemNo);
                Item item = Item.get(infojetContext, webCartLine.itemNo);

                float vatFactor = 0;

                Customer customer = infojetContext.getCurrentCustomer();
                if (customer != null)
                {
                    vatFactor = item.getVatFactor(customer);
                }
                else
                {
                    throw new Exception("No default customer configurered. Unable to calculate VAT.");
                }

                int encodedUnitPrice = (int)(Math.Round(((int)(webCartLine.quantity)) * (decimal)webCartLine.unitPrice * (decimal)vatFactor, 2) * 100);

                if (webCheckout.getDiscountAmount() > 0)
                {
                    encodedUnitPrice = encodedUnitPrice - (int)(encodedUnitPrice * (webCheckout.getDiscountPercent() / 100));
                }

                data = data + webCartLine.itemNo + ":" + htmlEncode(item.getItemTranslation(infojetContext.languageCode).description) + ":1:" + encodedUnitPrice + ":";
                totalPrice = totalPrice + encodedUnitPrice;
				i++;
			}

			// Administrative fee
            if (webCheckout.webCartHeader.webPaymentMethod != null)
            {
                //if ((webCheckout.webCartHeader.webPaymentMethod.upperOrderAmountLimit > 0) && (infojetContext.cartHandler.getTotalCartAmount() >= webCheckout.webCartHeader.webPaymentMethod.upperOrderAmountLimit)) webCheckout.webCartHeader.adminFee = 0;

                data = data + "ADMIN:" + infojetContext.translate("ADMIN COST") + ":1:" + ((int)(Math.Round((decimal)webCheckout.webCartHeader.adminFeeInclVat, 2) * 100)).ToString() + ":";
			}

            if (webCheckout.webCartHeader.webShipmentMethod != null)
            {
                data = data + "FREIGHT:" + infojetContext.translate("FREIGHT COST") + ":1:" + ((int)(Math.Round((decimal)webCheckout.webCartHeader.webShipmentMethod.amountInclVat, 2) * 100)).ToString() + ":";
            }

			//

			url = url + "&data="+data;

            return url;
		}

		private string htmlEncode(string text)
		{
			text = text.Replace("å", "&aring;");
			text = text.Replace("ä", "&auml;");
			text = text.Replace("ö", "&ouml;");
			text = text.Replace("Å", "&Aring;");
			text = text.Replace("Ä", "&Äuml;");
			text = text.Replace("Ö", "&Ouml;");
			return System.Web.HttpUtility.UrlEncode(text);
		}

        public void reportPayment()
        {
            string dibsRefNo = System.Web.HttpContext.Current.Request.QueryString["DTrefNo"];
            string sum = System.Web.HttpContext.Current.Request.QueryString["sum"];
            string reply = System.Web.HttpContext.Current.Request.QueryString["reply"];
            string currencyCode = System.Web.HttpContext.Current.Request.QueryString["currency"];
            string referenceData = System.Web.HttpContext.Current.Request.QueryString["referenceData"];
            string verifyId = System.Web.HttpContext.Current.Request.QueryString["verifyId"];
            string mac = System.Web.HttpContext.Current.Request.QueryString["MAC"];
            string method = System.Web.HttpContext.Current.Request.QueryString["method"];

            string key = System.Configuration.ConfigurationSettings.AppSettings["DIBS_Key"];
            string returnString = string.Format("{0}&{1}&{2}&{3}&{4}&{5}&", sum, currencyCode, reply, verifyId, referenceData, key);
            string returnMac = Md5(returnString);

            if (returnMac == mac)
            {
                WebPaymentEntry.create(infojetContext, dibsRefNo, referenceData, float.Parse(sum)/100, currencyCode);
                //throw new Exception("Report payment: " + dibsRefNo);

                WebCartHeader webCartHeader = new WebCartHeader(infojetContext, infojetContext.sessionId);
                webCartHeader.paymentReference = dibsRefNo;
                webCartHeader.paymentOrderNo = referenceData;

                ApplicationServerConnection appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "reportOrderPayment", webCartHeader));
                if (appServerConnection.processRequest())
                {
                    if (appServerConnection.serviceResponse.status == "200")
                    {
                        System.Web.HttpContext.Current.Response.Write("<!-- VerifyEasy_response:_OK -->");
                    }
                }

            }

            System.Web.HttpContext.Current.Response.End();
        }

        private string Md5(string strChange)
        {
            System.Text.Encoding encoding = System.Text.Encoding.ASCII;
            byte[] pass = encoding.GetBytes(strChange);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            byte[] hash = md5.ComputeHash(pass);
            foreach (byte item in hash)
            {
                sb.Append(item.ToString("x2").ToUpper());
            }
            return sb.ToString();

        }
        
	}
}
