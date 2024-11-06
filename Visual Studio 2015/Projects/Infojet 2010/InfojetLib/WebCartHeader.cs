using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class WebCartHeader : ServiceArgument
	{
		private Infojet infojetContext;

		public string sessionId;

		public string customerNo;
		public string userAccountNo;

		public string billToName;
		public string billToName2;
		public string billToAddress;
		public string billToAddress2;
		public string billToPostCode;
		public string billToCity;
		public string billToCountryCode;

		public bool addShipToAddress;
		public string shipToCode;
		public string shipToName;
		public string shipToName2;
		public string shipToAddress;
		public string shipToAddress2;
		public string shipToPostCode;
		public string shipToCity;
		public string shipToCountryCode;

		public string contactName;
		public string phoneNo;
		public string email;

		public string customerOrderNo;
		public string noteOfGoods;

		public string clientIpAddress;
		public string clientUserAgent;

		public string currencyCode;
		public string webPaymentMethodCode;
        public string webShipmentMethodCode;

		public bool salesTermsConfirmed;
		public string paymentReference;
        public string message;

		public string shippingAgentCode;
		public string shippingAgentServiceCode;
		public string shipmentMethodCode;
        public string shippingAgentServiceDescription;

        public string shippingAdvice;
        public DateTime shipmentDate;

		public float freightFee;
        public float adminFee;

        public WebShipmentMethod webShipmentMethod;
        public WebPaymentMethod webPaymentMethod;

        public Hashtable extraFields;

        private DataSet _webCartLinesDataSet;

		public WebCartHeader(Infojet infojetContext, string sessionId)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
				 
			this.sessionId = sessionId;

			customerNo = "";
			userAccountNo = "";

			billToName = "";
			billToName2 = "";
			billToAddress = "";
			billToAddress2 = "";
			billToPostCode = "";
			billToCity = "";
			billToCountryCode = "";

			addShipToAddress = false;
			shipToCode = "";
			shipToName = "";
			shipToName2 = "";
			shipToAddress = "";
			shipToAddress2 = "";
			shipToPostCode = "";
			shipToCity = "";
			shipToCountryCode = "";

			contactName = "";
			phoneNo = "";
			email = "";

			customerOrderNo = "";
			noteOfGoods = "";

			clientIpAddress = "";
			clientUserAgent = "";

			currencyCode = "";
			webPaymentMethodCode = "";
            webShipmentMethodCode = "";

			salesTermsConfirmed = false;
			paymentReference = "";
            message = "";

			shippingAgentCode = "";
			shipmentMethodCode = "";
			shippingAgentServiceCode = "";

			freightFee = 0;
            shipmentDate = DateTime.Today;

            extraFields = new Hashtable();



		}


		public void setUserSession(UserSession userSession)
		{

			this.customerNo = userSession.customer.no;
			this.userAccountNo = userSession.webUserAccount.no;

            this.billToName = userSession.webUserAccount.billToCompanyName;
            this.billToAddress = userSession.webUserAccount.billToAddress;
            this.billToAddress2 = userSession.webUserAccount.billToAddress2;
            this.billToPostCode = userSession.webUserAccount.billToPostCode;
            this.billToCity = userSession.webUserAccount.billToCity;
            this.billToCountryCode = userSession.webUserAccount.billToCountryCode;

            this.shipToName = userSession.webUserAccount.companyName;
            this.shipToAddress = userSession.webUserAccount.address;
            this.shipToAddress2 = userSession.webUserAccount.address2;
            this.shipToPostCode = userSession.webUserAccount.postCode;
            this.shipToCity = userSession.webUserAccount.city;
            this.shipToCountryCode = userSession.webUserAccount.countryCode;

			this.shippingAgentCode = userSession.customer.shippingAgentCode;
			this.shipmentMethodCode = userSession.customer.shipmentMethodCode;

			this.clientIpAddress = userSession.clientIp;
			this.clientUserAgent = userSession.clientAgent;

            this.contactName = userSession.webUserAccount.name;
            this.phoneNo = userSession.webUserAccount.phoneNo;
            this.email = userSession.webUserAccount.email;

            this.currencyCode = userSession.customer.currencyCode;
		}

        public void setCustomer(Customer customer)
        {
            this.customerNo = customer.no;

            this.billToName = customer.name;
            this.billToAddress = customer.address;
            this.billToAddress2 = customer.address2;
            this.billToPostCode = customer.postCode;
            this.billToCity = customer.city;
            this.billToCountryCode = customer.countryCode;

            this.shipToName = customer.name;
            this.shipToAddress = customer.address;
            this.shipToAddress2 = customer.address2;
            this.shipToPostCode = customer.postCode;
            this.shipToCity = customer.city;
            this.shipToCountryCode = customer.countryCode;

            this.shippingAgentCode = customer.shippingAgentCode;
            this.shipmentMethodCode = customer.shipmentMethodCode;

            this.contactName = customer.name;
            this.phoneNo = customer.phoneNo;
            this.email = customer.email;

            this.currencyCode = customer.currencyCode;

        }

		public void save()
		{
            if (System.Web.HttpContext.Current.Session["webCartHeader"] == null)
            {
                System.Web.HttpContext.Current.Session.Add("webCartHeader", this);
            }
            else
            {
                System.Web.HttpContext.Current.Session["webCartHeader"] = this;
            }
		}

        public void delete()
        {
            System.Web.HttpContext.Current.Session.Remove("webCartHeader");


        }

		public ShipmentMethod shipmentMethod
		{
			get
			{
                return new ShipmentMethod(this.infojetContext.systemDatabase, this.shipmentMethodCode);
			}
		}

		public ShippingAgent shippingAgent
		{
			get
			{
                return new ShippingAgent(this.infojetContext.systemDatabase, this.shippingAgentCode);
			}
		}

        public void setCartLines(DataSet webCartLinesDataSet)
        {
            this._webCartLinesDataSet = webCartLinesDataSet;
        }

		#region ServiceArgument Members

		public XmlElement toDOM(XmlDocument xmlDoc)
		{


			XmlElement orderElement = xmlDoc.CreateElement("salesOrder");

			XmlElement sessionNoElement = xmlDoc.CreateElement("sessionId");
			sessionNoElement.AppendChild(xmlDoc.CreateTextNode(sessionId));
			orderElement.AppendChild(sessionNoElement);

			XmlElement customerNoElement = xmlDoc.CreateElement("customerNo");
			customerNoElement.AppendChild(xmlDoc.CreateTextNode(customerNo));
			orderElement.AppendChild(customerNoElement);

			XmlElement userAccountNoElement = xmlDoc.CreateElement("userAccountNo");
			userAccountNoElement.AppendChild(xmlDoc.CreateTextNode(userAccountNo));
			orderElement.AppendChild(userAccountNoElement);

			XmlElement billToNameElement = xmlDoc.CreateElement("billToName");
			billToNameElement.AppendChild(xmlDoc.CreateTextNode(billToName));
			orderElement.AppendChild(billToNameElement);

			XmlElement billToName2Element = xmlDoc.CreateElement("billToName2");
			billToName2Element.AppendChild(xmlDoc.CreateTextNode(billToName2));
			orderElement.AppendChild(billToName2Element);

			XmlElement billToAddressElement = xmlDoc.CreateElement("billToAddress");
			billToAddressElement.AppendChild(xmlDoc.CreateTextNode(billToAddress));
			orderElement.AppendChild(billToAddressElement);

			XmlElement billToAddress2Element = xmlDoc.CreateElement("billToAddress2");
			billToAddress2Element.AppendChild(xmlDoc.CreateTextNode(billToAddress2));
			orderElement.AppendChild(billToAddress2Element);

			XmlElement billToPostCodeElement = xmlDoc.CreateElement("billToPostCode");
			billToPostCodeElement.AppendChild(xmlDoc.CreateTextNode(billToPostCode));
			orderElement.AppendChild(billToPostCodeElement);

			XmlElement billToCityElement = xmlDoc.CreateElement("billToCity");
			billToCityElement.AppendChild(xmlDoc.CreateTextNode(billToCity));
			orderElement.AppendChild(billToCityElement);

			XmlElement billToCountryCodeElement = xmlDoc.CreateElement("billToCountryCode");
			billToCountryCodeElement.AppendChild(xmlDoc.CreateTextNode(billToCountryCode));
			orderElement.AppendChild(billToCountryCodeElement);


			XmlElement addShipToAddressElement = xmlDoc.CreateElement("addShipToAddress");
			if (addShipToAddress)
			{
				addShipToAddressElement.AppendChild(xmlDoc.CreateTextNode("true"));
			}
			else
			{
				addShipToAddressElement.AppendChild(xmlDoc.CreateTextNode("false"));
			}
			orderElement.AppendChild(addShipToAddressElement);

			XmlElement shipToNameElement = xmlDoc.CreateElement("shipToName");
			shipToNameElement.AppendChild(xmlDoc.CreateTextNode(shipToName));
			orderElement.AppendChild(shipToNameElement);

			XmlElement shipToName2Element = xmlDoc.CreateElement("shipToName2");
			shipToName2Element.AppendChild(xmlDoc.CreateTextNode(shipToName2));
			orderElement.AppendChild(shipToName2Element);

			XmlElement shipToAddressElement = xmlDoc.CreateElement("shipToAddress");
			shipToAddressElement.AppendChild(xmlDoc.CreateTextNode(shipToAddress));
			orderElement.AppendChild(shipToAddressElement);

			XmlElement shipToAddress2Element = xmlDoc.CreateElement("shipToAddress2");
			shipToAddress2Element.AppendChild(xmlDoc.CreateTextNode(shipToAddress2));
			orderElement.AppendChild(shipToAddress2Element);

			XmlElement shipToPostCodeElement = xmlDoc.CreateElement("shipToPostCode");
			shipToPostCodeElement.AppendChild(xmlDoc.CreateTextNode(shipToPostCode));
			orderElement.AppendChild(shipToPostCodeElement);

			XmlElement shipToCityElement = xmlDoc.CreateElement("shipToCity");
			shipToCityElement.AppendChild(xmlDoc.CreateTextNode(shipToCity));
			orderElement.AppendChild(shipToCityElement);

			XmlElement shipToCountryCodeElement = xmlDoc.CreateElement("shipToCountryCode");
			shipToCountryCodeElement.AppendChild(xmlDoc.CreateTextNode(shipToCountryCode));
			orderElement.AppendChild(shipToCountryCodeElement);


			XmlElement contactNameElement = xmlDoc.CreateElement("contactName");
			contactNameElement.AppendChild(xmlDoc.CreateTextNode(contactName));
			orderElement.AppendChild(contactNameElement);

			XmlElement phoneNoElement = xmlDoc.CreateElement("phoneNo");
			phoneNoElement.AppendChild(xmlDoc.CreateTextNode(phoneNo));
			orderElement.AppendChild(phoneNoElement);

			XmlElement emailElement = xmlDoc.CreateElement("email");
			emailElement.AppendChild(xmlDoc.CreateTextNode(email));
			orderElement.AppendChild(emailElement);

			XmlElement clientIpAddressElement = xmlDoc.CreateElement("clientIpAddress");
			clientIpAddressElement.AppendChild(xmlDoc.CreateTextNode(clientIpAddress));
			orderElement.AppendChild(clientIpAddressElement);

			if (clientUserAgent.Length > 200) clientUserAgent = clientUserAgent.Substring(1, 200);
			XmlElement clientUserAgentElement = xmlDoc.CreateElement("clientUserAgent");
			clientUserAgentElement.AppendChild(xmlDoc.CreateTextNode(clientUserAgent));
			orderElement.AppendChild(clientUserAgentElement);

			XmlElement currencyCodeElement = xmlDoc.CreateElement("currencyCode");
			currencyCodeElement.AppendChild(xmlDoc.CreateTextNode(currencyCode));
			orderElement.AppendChild(currencyCodeElement);

			XmlElement webPaymentMethodCodeElement = xmlDoc.CreateElement("webPaymentMethodCode");
			webPaymentMethodCodeElement.AppendChild(xmlDoc.CreateTextNode(webPaymentMethodCode));
			orderElement.AppendChild(webPaymentMethodCodeElement);

			XmlElement customerOrderNoElement = xmlDoc.CreateElement("customerOrderNo");
			customerOrderNoElement.AppendChild(xmlDoc.CreateTextNode(customerOrderNo));
			orderElement.AppendChild(customerOrderNoElement);

			XmlElement noteOfGoodsElement = xmlDoc.CreateElement("noteOfGoods");
            noteOfGoodsElement.AppendChild(xmlDoc.CreateTextNode(noteOfGoods));
            orderElement.AppendChild(noteOfGoodsElement);

            XmlElement shipmentMethodCodeElement = xmlDoc.CreateElement("shipmentMethodCode");
            shipmentMethodCodeElement.AppendChild(xmlDoc.CreateTextNode(shipmentMethodCode));
            orderElement.AppendChild(shipmentMethodCodeElement);

			XmlElement shippingAgentCodeElement = xmlDoc.CreateElement("shippingAgentCode");
			shippingAgentCodeElement.AppendChild(xmlDoc.CreateTextNode(shippingAgentCode));
			orderElement.AppendChild(shippingAgentCodeElement);

			XmlElement shippingAgentServiceCodeElement = xmlDoc.CreateElement("shippingAgentServiceCode");
			shippingAgentServiceCodeElement.AppendChild(xmlDoc.CreateTextNode(shippingAgentServiceCode));
			orderElement.AppendChild(shippingAgentServiceCodeElement);

			XmlElement shipToCodeElement = xmlDoc.CreateElement("shipToCode");
			shipToCodeElement.AppendChild(xmlDoc.CreateTextNode(shipToCode));
			orderElement.AppendChild(shipToCodeElement);


            XmlElement webShipmentMethodElement = xmlDoc.CreateElement("webShipmentMethodCode");
            webShipmentMethodElement.AppendChild(xmlDoc.CreateTextNode(webShipmentMethodCode));
            orderElement.AppendChild(webShipmentMethodElement);

            XmlElement freightFeeElement = xmlDoc.CreateElement("freightFee");
            freightFeeElement.AppendChild(xmlDoc.CreateTextNode(freightFee.ToString()));
            orderElement.AppendChild(freightFeeElement);

            XmlElement adminFeeElement = xmlDoc.CreateElement("adminFee");
            adminFeeElement.AppendChild(xmlDoc.CreateTextNode(adminFee.ToString()));
            orderElement.AppendChild(adminFeeElement);

            if (webPaymentMethod != null)
            {
                XmlElement adminAccountElement = xmlDoc.CreateElement("adminAccount");
                adminAccountElement.AppendChild(xmlDoc.CreateTextNode(webPaymentMethod.glAccountNo));
                orderElement.AppendChild(adminAccountElement);
            }

            if (webShipmentMethod != null)
            {
                XmlElement freightAccountElement = xmlDoc.CreateElement("freightAccount");
                freightAccountElement.AppendChild(xmlDoc.CreateTextNode(webShipmentMethod.glAccountNo));
                orderElement.AppendChild(freightAccountElement);
            }

			XmlElement salesTermsConfirmed = xmlDoc.CreateElement("salesTermsConfirmed");
			if (this.salesTermsConfirmed)
			{
				salesTermsConfirmed.AppendChild(xmlDoc.CreateTextNode("true"));
			}
			else
			{
				salesTermsConfirmed.AppendChild(xmlDoc.CreateTextNode("false"));
			}
			orderElement.AppendChild(salesTermsConfirmed);

			XmlElement paymentReferenceElement = xmlDoc.CreateElement("paymentReference");
			paymentReferenceElement.AppendChild(xmlDoc.CreateTextNode(paymentReference));
			orderElement.AppendChild(paymentReferenceElement);

            XmlElement messageElement = xmlDoc.CreateElement("message");
            messageElement.AppendChild(xmlDoc.CreateTextNode(message));
            orderElement.AppendChild(messageElement);

            XmlElement shippingAdviceElement = xmlDoc.CreateElement("shippingAdvice");
            shippingAdviceElement.AppendChild(xmlDoc.CreateTextNode(shippingAdvice));
            orderElement.AppendChild(shippingAdviceElement);

            XmlElement shipmentDateElement = xmlDoc.CreateElement("shipmentDate");
            shipmentDateElement.AppendChild(xmlDoc.CreateTextNode(shipmentDate.ToString("yyyy-MM-dd")));
            orderElement.AppendChild(shipmentDateElement);

            XmlElement extraFieldsElement = xmlDoc.CreateElement("extraFields");

            foreach (string key in extraFields.Keys)
            {
                XmlElement extraFieldElement = xmlDoc.CreateElement("field");
                extraFieldElement.AppendChild(xmlDoc.CreateTextNode(extraFields[key].ToString()));
                extraFieldElement.SetAttribute("name", key);
                extraFieldsElement.AppendChild(extraFieldElement);


            }

            orderElement.AppendChild(extraFieldsElement);

			XmlElement orderLinesElement = xmlDoc.CreateElement("lines");

            WebCartLines webCartLines = new WebCartLines(infojetContext.systemDatabase);

            if (_webCartLinesDataSet == null)
            {
                infojetContext.cartHandler.updateSession();
                _webCartLinesDataSet = webCartLines.getCartLines(infojetContext.sessionId);
            }

			int i = 0;
			while (i < _webCartLinesDataSet.Tables[0].Rows.Count)
			{
                WebCartLine webCartLine = new WebCartLine(infojetContext, _webCartLinesDataSet.Tables[0].Rows[i]);

				XmlElement orderLineElement = xmlDoc.CreateElement("line");
		
				XmlElement itemNoElement = xmlDoc.CreateElement("itemNo");
				itemNoElement.AppendChild(xmlDoc.CreateTextNode(webCartLine.itemNo));
				orderLineElement.AppendChild(itemNoElement);

				XmlElement quantityElement = xmlDoc.CreateElement("quantity");
				quantityElement.AppendChild(xmlDoc.CreateTextNode(webCartLine.quantity.ToString()));
				orderLineElement.AppendChild(quantityElement);

				XmlElement unitOfMeasureElement = xmlDoc.CreateElement("unitOfMeasure");
				unitOfMeasureElement.AppendChild(xmlDoc.CreateTextNode(webCartLine.unitOfMeasureCode));
				orderLineElement.AppendChild(unitOfMeasureElement);

				XmlElement unitPriceElement = xmlDoc.CreateElement("unitPrice");
				unitPriceElement.AppendChild(xmlDoc.CreateTextNode(webCartLine.unitPrice.ToString()));
				orderLineElement.AppendChild(unitPriceElement);

				XmlElement amountElement = xmlDoc.CreateElement("amount");
				amountElement.AppendChild(xmlDoc.CreateTextNode((webCartLine.unitPrice*webCartLine.quantity).ToString()));
				orderLineElement.AppendChild(amountElement);

				XmlElement referenceElement = xmlDoc.CreateElement("reference");
				referenceElement.AppendChild(xmlDoc.CreateTextNode(webCartLine.referenceNo));
				orderLineElement.AppendChild(referenceElement);

                XmlElement extra1Element = xmlDoc.CreateElement("extra1");
                extra1Element.AppendChild(xmlDoc.CreateTextNode(webCartLine.extra1));
                orderLineElement.AppendChild(extra1Element);

                XmlElement extra2Element = xmlDoc.CreateElement("extra2");
                extra2Element.AppendChild(xmlDoc.CreateTextNode(webCartLine.extra2));
                orderLineElement.AppendChild(extra2Element);

                XmlElement extra3Element = xmlDoc.CreateElement("extra3");
                extra3Element.AppendChild(xmlDoc.CreateTextNode(webCartLine.extra3));
                orderLineElement.AppendChild(extra3Element);

                XmlElement extra4Element = xmlDoc.CreateElement("extra4");
                extra4Element.AppendChild(xmlDoc.CreateTextNode(webCartLine.extra4));
                orderLineElement.AppendChild(extra4Element);

                XmlElement extra5Element = xmlDoc.CreateElement("extra5");
                extra5Element.AppendChild(xmlDoc.CreateTextNode(webCartLine.extra5));
                orderLineElement.AppendChild(extra5Element);

                XmlElement webUserAccountElement = xmlDoc.CreateElement("webUserAccountNo");
                webUserAccountElement.AppendChild(xmlDoc.CreateTextNode(webCartLine.webUserAccountNo));
                orderLineElement.AppendChild(webUserAccountElement);

				orderLinesElement.AppendChild(orderLineElement);

				i++;
			}


			orderElement.AppendChild(orderLinesElement);

			return orderElement;
		}

		public void deleteLines()
		{
            infojetContext.systemDatabase.nonQuery("DELETE FROM [" + infojetContext.systemDatabase.getTableName("Web Cart Line") + "] WHERE [Session ID] = '" + infojetContext.sessionId + "'");
	
		}


        public void setExtraField(string key, string value)
        {
            if (extraFields.Contains(key)) extraFields.Remove(key);
            extraFields.Add(key, value);
        }

        public void applyWebShipmentMethod(WebShipmentMethod webShipmentMethod)
        {
            this.webShipmentMethod = webShipmentMethod;
            this.webShipmentMethodCode = webShipmentMethod.code;
            this.freightFee = webShipmentMethod.amount;
            this.shipmentMethodCode = webShipmentMethod.shipmentMethodCode;
            this.shippingAgentCode = webShipmentMethod.shippingAgentCode;
            this.shippingAgentServiceCode = webShipmentMethod.shippingAgentServiceCode;
            this.shippingAgentServiceDescription = webShipmentMethod.description;
            save();

        }

        public void applyWebPaymentMethod(WebPaymentMethod webPaymentMethod)
        {
            this.webPaymentMethod = webPaymentMethod;
            this.webPaymentMethodCode = webPaymentMethod.code;
            this.adminFee = webPaymentMethod.amount;
            save();
        }

		#endregion

        public static WebCartHeader get()
        {
            if (System.Web.HttpContext.Current.Session["webCartHeader"] != null)
            {
                return (WebCartHeader)System.Web.HttpContext.Current.Session["webCartHeader"];
            }
            return null;
        }
	}
}
