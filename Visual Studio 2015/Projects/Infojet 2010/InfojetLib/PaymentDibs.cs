using System;
using System.IO;
using System.Data;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for PaymentDibs.
	/// </summary>
	public class PaymentDibs
	{
		private Database database;
		private Infojet infojetContext;

		public PaymentDibs(Database database, Infojet infojetContext)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
			this.infojetContext = infojetContext;
		}

		public void renderHtml(StringWriter stringWriter, WebCartHeader webCartHeader, WebPaymentMethod webPaymentMethod)
		{
		
			string firstName = "";
			string lastName = "";

			if (webCartHeader.billToName.IndexOf(" ") > -1)
			{
				firstName = webCartHeader.billToName.Substring(0, webCartHeader.billToName.IndexOf(" ")-1);
				lastName = webCartHeader.billToName.Substring(webCartHeader.billToName.IndexOf(" ")+1);
			}
			else
			{
				firstName = webCartHeader.billToName;
				lastName = " ";
			}

			string url = System.Configuration.ConfigurationSettings.AppSettings["DIBS_Url"] + 
				"?billingFirstName="+htmlEncode(firstName)+
				"&billingLastName="+htmlEncode(lastName)+
				"&eMail="+htmlEncode(webCartHeader.email)+
				"&pageSet="+webPaymentMethod.serviceParameter+
				"&billingAddress="+htmlEncode(webCartHeader.billToAddress)+
				"&billingPostCode="+htmlEncode(webCartHeader.billToPostCode)+
				"&billingCity="+htmlEncode(webCartHeader.billToCity)+
				"&billingCountry="+htmlEncode(webCartHeader.billToCountryCode)+
				"&currency="+webCartHeader.currencyCode+
				"&totalText="+htmlEncode(infojetContext.translate("SUBTOTAL"))+
				"&instructionText="+htmlEncode(infojetContext.translate("CARD INSTRUCTIONS"))+
				"&creditCardText="+htmlEncode(infojetContext.translate("CREDIT CARD NO"))+
				"&goodThruText="+htmlEncode(infojetContext.translate("GOOD THRU"))+
				"&cvcText="+htmlEncode(infojetContext.translate("CVC"))+
				"&nextText="+htmlEncode(infojetContext.translate("NEXT"))+
				"&nameText="+htmlEncode(infojetContext.translate("NAME"))+
				"&addressText="+htmlEncode(infojetContext.translate("ADDRESS"))+
				"&postalText="+htmlEncode(infojetContext.translate("POST ADDRESS"))+
				"&countryText="+htmlEncode(infojetContext.translate("COUNTRY"))+
				"&phoneText="+htmlEncode(infojetContext.translate("PHONE NO"))+
				"&emailText="+htmlEncode(infojetContext.translate("EMAIL"))+
				"&orderText="+htmlEncode(infojetContext.translate("PURCHASE"))+
				"&itemNoText="+htmlEncode(infojetContext.translate("ITEM NO"))+
				"&descriptionText="+htmlEncode(infojetContext.translate("DESCRIPTION"))+
				"&quantityText="+htmlEncode(infojetContext.translate("QUANTITY"))+
				"&unitPriceText="+htmlEncode(infojetContext.translate("UNIT PRICE"))+
				"&amountText="+htmlEncode(infojetContext.translate("AMOUNT"))+
				"&declineMessage="+htmlEncode(infojetContext.translate("CARD DECLINED"))+
				"&orderNoText="+htmlEncode(infojetContext.translate("ORDER NO"))+
				"&dibsReferenceText="+htmlEncode(infojetContext.translate("DIBS REFERENCE NO"))+
				"&errorMessageText="+htmlEncode(infojetContext.translate("ERROR MESSAGE"));

			string data = "";

			WebCartLines webCartLines = new WebCartLines(database);
			DataSet webCartLineDataSet = webCartLines.getCartLines(webCartHeader.sessionId);
			int i = 0;
			while (i < webCartLineDataSet.Tables[0].Rows.Count)
			{
				Item item = new Item(database, webCartLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());
				
				data = data + webCartLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString()+":"+htmlEncode(item.getItemTranslation(infojetContext.languageCode).description)+":"+((int)(float.Parse(webCartLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString()))).ToString()+":"+((int)(float.Parse(webCartLineDataSet.Tables[0].Rows[i].ItemArray.GetValue(4).ToString())*100)).ToString()+":";

				i++;
			}

			// Administrative fee
			string currencyCode = webCartHeader.currencyCode;

            if (infojetContext.generalLedgerSetup.lcyCode == currencyCode) currencyCode = "";

            DataSet paymentDetailDataSet = webPaymentMethod.getDetails(infojetContext.languageCode, infojetContext.userSession.customer, infojetContext.currencyCode);

            if (paymentDetailDataSet.Tables[0].Rows.Count > 0)
			{
                WebPaymentMethodDetail webPaymentMethodDetail = new WebPaymentMethodDetail(infojetContext.systemDatabase, paymentDetailDataSet.Tables[0].Rows[0]);

                data = data + "ADMIN:" + infojetContext.translate("ADMIN COST") + ":1:" + ((int)(webPaymentMethodDetail.amount * 100)).ToString() + ":";
			}

			//


			url = url + "&data="+data;


			stringWriter.WriteLine("<table cellspacing=\"0\" cellpadding=\"2\" width=\"100%\" border=\"0\">");
			stringWriter.WriteLine("<tr>");
			stringWriter.WriteLine("<td><iframe width=\"100%\" frameborder=\"true\" src=\""+url+"\" height=\"400\"></iframe></td>");
			stringWriter.WriteLine("</tr>");
			stringWriter.WriteLine("</table>");

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

	}
}
