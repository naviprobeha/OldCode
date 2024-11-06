using System;
using System.IO;
using System.Xml;
using System.Collections;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for CustomerHistory.
	/// </summary>
	public class CustomerHistory
	{

		private int noOfPages;
		private int currentPageNo;

        private Infojet infojetContext;

		public CustomerHistory(Infojet infojetContext)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
		}

		public CustomerHistoryOrderCollection getOrderHistory(int currentPageNo)
		{
            this.currentPageNo = currentPageNo;

			if (infojetContext.userSession != null)
			{
				ApplicationServerConnection appServerConnection;

				appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "getOrderHistory", new HistoryList(infojetContext.userSession.customer.no, currentPageNo)));
				if (appServerConnection.processRequest())
				{
					return parseOrderHistoryResponse(appServerConnection.serviceResponse, infojetContext);
				}
			}

            return null;

		}

        public CustomerHistoryShipmentCollection getShipmentHistory(int currentPageNo)
        {
            this.currentPageNo = currentPageNo;

            if (infojetContext.userSession != null)
            {
                ApplicationServerConnection appServerConnection;

                appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "getShipmentHistory", new HistoryList(infojetContext.userSession.customer.no, currentPageNo)));
                if (appServerConnection.processRequest())
                {
                    return parseShipmentHistoryResponse(appServerConnection.serviceResponse, infojetContext);
                }
            }

            return null;

        }

        public CustomerHistoryInvoiceCollection getInvoiceHistory(int currentPageNo)
        {
            this.currentPageNo = currentPageNo;

            if (infojetContext.userSession != null)
            {
                ApplicationServerConnection appServerConnection;

                appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "getInvoiceHistory", new HistoryList(infojetContext.userSession.customer.no, currentPageNo)));
                if (appServerConnection.processRequest())
                {
                    return parseInvoiceHistoryResponse(appServerConnection.serviceResponse, infojetContext);
                }
            }

            return null;

        }

        public CustomerHistoryCrMemoCollection getCrMemoHistory(int currentPageNo)
        {
            this.currentPageNo = currentPageNo;

            if (infojetContext.userSession != null)
            {
                ApplicationServerConnection appServerConnection;

                appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "getCrMemoHistory", new HistoryList(infojetContext.userSession.customer.no, currentPageNo)));
                if (appServerConnection.processRequest())
                {
                    return parseCrMemoHistoryResponse(appServerConnection.serviceResponse, infojetContext);
                }
            }

            return null;

        }

        public CustomerHistoryInvoice getInvoiceInfo(string invoiceNo)
        {
            if (infojetContext.userSession != null)
            {
                ApplicationServerConnection appServerConnection;

                appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "getInvoiceInfo", new HistoryDocument(2, invoiceNo, infojetContext.userSession.webUserAccount)));
                if (appServerConnection.processRequest())
                {
                    return this.parseInvoiceInfoResponse(appServerConnection.serviceResponse, infojetContext);
                }
            }


            return null;

        }

        public CustomerHistoryShipment getShipmentInfo(string shipmentNo)
        {
            if (infojetContext.userSession != null)
            {
                ApplicationServerConnection appServerConnection;

                appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "getShipmentInfo", new HistoryDocument(1, shipmentNo, infojetContext.userSession.webUserAccount)));
                if (appServerConnection.processRequest())
                {
                    return this.parseShipmentInfoResponse(appServerConnection.serviceResponse, infojetContext);
                }
            }

            return null;

        }

        public CustomerHistoryOrder getOrderInfo(string orderNo)
        {
            if (infojetContext.userSession != null)
            {
                ApplicationServerConnection appServerConnection;
                appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "getOrderInfo", new HistoryDocument(0, orderNo, infojetContext.userSession.webUserAccount)));

                if (appServerConnection.processRequest())
                {
                    return this.parseOrderInfoResponse(appServerConnection.serviceResponse, infojetContext);
                }
            }

            return null;
        }

        public CustomerHistoryOrder getOrderInfo(string orderNo, string customerNo)
        {
            ApplicationServerConnection appServerConnection;

            WebUserAccount webUserAccount = new WebUserAccount(infojetContext.systemDatabase);
            webUserAccount.customerNo = customerNo;
            appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "getOrderInfo", new HistoryDocument(0, orderNo, webUserAccount)));

            if (appServerConnection.processRequest())
            {
                return this.parseOrderInfoResponse(appServerConnection.serviceResponse, infojetContext);
            }

            return null;
        }

        public CustomerHistoryCrMemo getCrMemoInfo(string invoiceNo)
        {
            if (infojetContext.userSession != null)
            {
                ApplicationServerConnection appServerConnection;

                appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "getCrMemoInfo", new HistoryDocument(2, invoiceNo, infojetContext.userSession.webUserAccount)));
                if (appServerConnection.processRequest())
                {
                    return this.parseCrMemoInfoResponse(appServerConnection.serviceResponse, infojetContext);
                }

            }

            return null;

        }

        public CustomerHistoryLedgerCollection getCustomerLedgerInfo()
        {
            if (infojetContext.userSession != null)
            {
                ApplicationServerConnection appServerConnection;

                appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "getCustomerLedgerInfo", new HistoryList(infojetContext.userSession.webUserAccount.customerNo, 0)));
                if (appServerConnection.processRequest())
                {
                    return this.parseCustomerLedgerInfoResponse(appServerConnection.serviceResponse, infojetContext);
                }
            }

            return null;

        }


		public int getNoOfPages()
		{
			return noOfPages;
		}

		public int getCurrentPageNo()
		{
			return currentPageNo;
		}

        public bool showDetailedInformation()
        {
            if ((System.Web.HttpContext.Current.Request["docType"] != null) &&
                (System.Web.HttpContext.Current.Request["docType"] != "") &&
                (System.Web.HttpContext.Current.Request["docNo"] != null) &&
                (System.Web.HttpContext.Current.Request["docNo"] != ""))
            {
                if (System.Web.HttpContext.Current.Request["pdf"] == "true")
                {
                    requestPdfDocument(int.Parse(System.Web.HttpContext.Current.Request["docType"]), System.Web.HttpContext.Current.Request["docNo"]);
                }
                return true;
            }

            return false;
        }

        public int getRequestedDocumentType()
        {
            return int.Parse(System.Web.HttpContext.Current.Request["docType"]);
        }

        public string getRequestedDocumentNo()
        {
            return System.Web.HttpContext.Current.Request["docNo"];
        }

		private CustomerHistoryOrderCollection parseOrderHistoryResponse(ServiceResponse serviceResponse, Infojet infojetContext)
		{

            CustomerHistoryOrderCollection orderCollection = new CustomerHistoryOrderCollection();

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(serviceResponse.xml);

			XmlElement documentElement = xmlDoc.DocumentElement;

			XmlElement ordersElement = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/orders");
            if (ordersElement == null) return null;
            if (ordersElement.GetAttribute("noOfPages") != null)
			{
				noOfPages = int.Parse(ordersElement.GetAttribute("noOfPages"));
			}

			XmlNodeList ordersNodeList = ordersElement.SelectNodes("order");
			int i = 0;

			while (i < ordersNodeList.Count)
			{
				XmlElement orderElement = (XmlElement)ordersNodeList.Item(i);

				CustomerHistoryOrder customerHistoryOrder = new CustomerHistoryOrder();


				XmlElement noElement = (XmlElement)orderElement.SelectSingleNode("no");
				if (noElement != null)
				{
					if (noElement.FirstChild != null) customerHistoryOrder.no = noElement.FirstChild.Value;
				}


				XmlElement customerNameElement = (XmlElement)orderElement.SelectSingleNode("customerName");
				if (customerNameElement != null)
				{
					if (customerNameElement.FirstChild != null) customerHistoryOrder.customerName = customerNameElement.FirstChild.Value;
				}

				XmlElement orderDateElement = (XmlElement)orderElement.SelectSingleNode("orderDate");
				if (orderDateElement != null)
				{
					if (orderDateElement.FirstChild != null) customerHistoryOrder.orderDate = orderDateElement.FirstChild.Value;
				}

				XmlElement referenceElement = (XmlElement)orderElement.SelectSingleNode("yourReference");
				if (referenceElement != null)
				{
					if (referenceElement.FirstChild != null) customerHistoryOrder.yourReference = referenceElement.FirstChild.Value;
				}

                XmlElement sellToContactElement = (XmlElement)orderElement.SelectSingleNode("sellToContact");
                if (sellToContactElement != null)
                {
                    if (sellToContactElement.FirstChild != null) customerHistoryOrder.sellToContact = sellToContactElement.FirstChild.Value;
                }

                XmlElement shipToContactElement = (XmlElement)orderElement.SelectSingleNode("shipToContact");
                if (shipToContactElement != null)
                {
                    if (shipToContactElement.FirstChild != null) customerHistoryOrder.shipToContact = shipToContactElement.FirstChild.Value;
                }

				XmlElement amountElement = (XmlElement)orderElement.SelectSingleNode("amount");
				if (amountElement != null)
				{
					if (amountElement.FirstChild != null) customerHistoryOrder.amount = amountElement.FirstChild.Value;
				}

				XmlElement amountInclVatElement = (XmlElement)orderElement.SelectSingleNode("amountInclVat");
				if (amountInclVatElement != null)
				{
					if (amountInclVatElement.FirstChild != null) customerHistoryOrder.amountIncludingVat = amountInclVatElement.FirstChild.Value;
				}

                XmlElement descriptionElement = (XmlElement)orderElement.SelectSingleNode("description");
                if (descriptionElement != null)
                {
                    if (descriptionElement.FirstChild != null) customerHistoryOrder.description = descriptionElement.FirstChild.Value;
                }

				customerHistoryOrder.link = infojetContext.webPage.getUrl()+"&docType=0&docNo="+customerHistoryOrder.no;



				XmlNodeList shipmentNodeList = orderElement.SelectNodes("shipments/shipment");
				int j = 0;
				while (j < shipmentNodeList.Count)
				{
					XmlElement shipmentElement = (XmlElement)shipmentNodeList.Item(j);

					CustomerHistoryShipment customerHistoryShipment = new CustomerHistoryShipment();

					XmlElement shipmentNoElement = (XmlElement)shipmentElement.SelectSingleNode("no");
					if (shipmentNoElement != null)
					{
						if (shipmentNoElement.FirstChild != null) customerHistoryShipment.no = shipmentNoElement.FirstChild.Value;
					}

					XmlElement shipmentDateElement = (XmlElement)shipmentElement.SelectSingleNode("shipmentDate");
					if (shipmentDateElement != null)
					{
						if (shipmentDateElement.FirstChild != null) customerHistoryShipment.shipmentDate = shipmentDateElement.FirstChild.Value;
					}

					XmlElement packageTrackingElement = (XmlElement)shipmentElement.SelectSingleNode("packageTrackingNo");
					if (packageTrackingElement != null)
					{
						if (packageTrackingElement.FirstChild != null) customerHistoryShipment.packageTrackingNo = packageTrackingElement.FirstChild.Value;
					}

                    XmlElement shippingAgentInetAddressElement = (XmlElement)shipmentElement.SelectSingleNode("shippingAgentInetAddress");
                    if (shippingAgentInetAddressElement != null)
					{
                        if (shippingAgentInetAddressElement.FirstChild != null) customerHistoryShipment.shippingAgentInternetAddress = shippingAgentInetAddressElement.FirstChild.Value;
					}

					customerHistoryShipment.link = infojetContext.webPage.getUrl()+"&docType=1&docNo="+customerHistoryShipment.no;


					customerHistoryOrder.shipments.Add(customerHistoryShipment);

					j++;
				}


				orderCollection.Add(customerHistoryOrder);

				i++;
			}

			return orderCollection;

		}

		private CustomerHistoryShipmentCollection parseShipmentHistoryResponse(ServiceResponse serviceResponse, Infojet infojetContext)
		{
			
			noOfPages = 1;

            CustomerHistoryShipmentCollection shipmentCollection = new CustomerHistoryShipmentCollection();

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(serviceResponse.xml);

			XmlElement documentElement = xmlDoc.DocumentElement;

			XmlElement shipmentsElement = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipments");
            if (shipmentsElement == null) return null;
            if (shipmentsElement.GetAttribute("noOfPages") != null)
			{
				noOfPages = int.Parse(shipmentsElement.GetAttribute("noOfPages"));
			}

			XmlNodeList shipmentsNodeList = shipmentsElement.SelectNodes("shipment");
			int i = 0;

	
			while (i < shipmentsNodeList.Count)
			{
				XmlElement shipmentElement = (XmlElement)shipmentsNodeList.Item(i);

				CustomerHistoryShipment customerHistoryShipment = new CustomerHistoryShipment();

				XmlElement noElement = (XmlElement)shipmentElement.SelectSingleNode("no");
				if (noElement != null)
				{
					if (noElement.FirstChild != null) customerHistoryShipment.no = noElement.FirstChild.Value;
				}

				XmlElement orderNoElement = (XmlElement)shipmentElement.SelectSingleNode("orderNo");
				if (orderNoElement != null)
				{
					if (orderNoElement.FirstChild != null) customerHistoryShipment.orderNo = orderNoElement.FirstChild.Value;
				}

				XmlElement customerNameElement = (XmlElement)shipmentElement.SelectSingleNode("customerName");
				if (customerNameElement != null)
				{
					if (customerNameElement.FirstChild != null) customerHistoryShipment.customerName = customerNameElement.FirstChild.Value;
				}

				XmlElement referenceElement = (XmlElement)shipmentElement.SelectSingleNode("yourReference");
				if (referenceElement != null)
				{
					if (referenceElement.FirstChild != null) customerHistoryShipment.yourReference = referenceElement.FirstChild.Value;
				}

				XmlElement shipmentDateElement = (XmlElement)shipmentElement.SelectSingleNode("shipmentDate");
				if (shipmentDateElement != null)
				{
					if (shipmentDateElement.FirstChild != null) customerHistoryShipment.shipmentDate = shipmentDateElement.FirstChild.Value;
				}

				XmlElement packageTrackingNoElement = (XmlElement)shipmentElement.SelectSingleNode("packageTrackingNo");
				if (shipmentDateElement != null)
				{
					if (packageTrackingNoElement.FirstChild != null) customerHistoryShipment.packageTrackingNo = packageTrackingNoElement.FirstChild.Value;
				}

                XmlElement sellToContactElement = (XmlElement)shipmentElement.SelectSingleNode("sellToContact");
                if (sellToContactElement != null)
                {
                    if (sellToContactElement.FirstChild != null) customerHistoryShipment.sellToContact = sellToContactElement.FirstChild.Value;
                }

                XmlElement shipToContactElement = (XmlElement)shipmentElement.SelectSingleNode("shipToContact");
                if (shipToContactElement != null)
                {
                    if (shipToContactElement.FirstChild != null) customerHistoryShipment.shipToContact = shipToContactElement.FirstChild.Value;
                }

                XmlElement shippingAgentInetAddressElement = (XmlElement)shipmentElement.SelectSingleNode("shippingAgentInetAddress");
                if (shippingAgentInetAddressElement != null)
                {
                    if (shippingAgentInetAddressElement.FirstChild != null) customerHistoryShipment.shippingAgentInternetAddress = shippingAgentInetAddressElement.FirstChild.Value;
                }

                XmlElement descriptionElement = (XmlElement)shipmentElement.SelectSingleNode("description");
                if (descriptionElement != null)
                {
                    if (descriptionElement.FirstChild != null) customerHistoryShipment.description = descriptionElement.FirstChild.Value;
                }

				customerHistoryShipment.link = infojetContext.webPage.getUrl()+"&docType=1&docNo="+customerHistoryShipment.no;


				shipmentCollection.Add(customerHistoryShipment);

				i++;
			}

			return shipmentCollection;
			
		}

        private CustomerHistoryInvoiceCollection parseInvoiceHistoryResponse(ServiceResponse serviceResponse, Infojet infojetContext)
		{
            CustomerHistoryInvoiceCollection invoiceCollection = new CustomerHistoryInvoiceCollection();

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(serviceResponse.xml);

			XmlElement documentElement = xmlDoc.DocumentElement;

			XmlElement invoicesElement = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoices");
            if (invoicesElement == null) return null;
            if (invoicesElement.GetAttribute("noOfPages") != null)
			{
				noOfPages = int.Parse(invoicesElement.GetAttribute("noOfPages"));
			}

			XmlNodeList invoicesNodeList = invoicesElement.SelectNodes("invoice");
			int i = 0;

			while (i < invoicesNodeList.Count)
			{
				XmlElement invoiceElement = (XmlElement)invoicesNodeList.Item(i);

				CustomerHistoryInvoice customerHistoryInvoice = new CustomerHistoryInvoice();

				XmlElement noElement = (XmlElement)invoiceElement.SelectSingleNode("no");
				if (noElement != null)
				{
					if (noElement.FirstChild != null) customerHistoryInvoice.no = noElement.FirstChild.Value;
				}

				XmlElement orderNoElement = (XmlElement)invoiceElement.SelectSingleNode("orderNo");
				if (orderNoElement != null)
				{
					if (orderNoElement.FirstChild != null) customerHistoryInvoice.orderNo = orderNoElement.FirstChild.Value;
				}

				XmlElement customerNameElement = (XmlElement)invoiceElement.SelectSingleNode("customerName");
				if (customerNameElement != null)
				{
					if (customerNameElement.FirstChild != null) customerHistoryInvoice.customerName = customerNameElement.FirstChild.Value;
				}

				XmlElement referenceElement = (XmlElement)invoiceElement.SelectSingleNode("yourReference");
				if (referenceElement != null)
				{
					if (referenceElement.FirstChild != null) customerHistoryInvoice.yourReference = referenceElement.FirstChild.Value;
				}

				XmlElement extDocNoElement = (XmlElement)invoiceElement.SelectSingleNode("externalDocumentNo");
				if (extDocNoElement != null)
				{
					if (extDocNoElement.FirstChild != null) customerHistoryInvoice.externalDocumentNo = extDocNoElement.FirstChild.Value;
				}

				XmlElement docDateElement = (XmlElement)invoiceElement.SelectSingleNode("documentDate");
				if (docDateElement != null)
				{
					if (docDateElement.FirstChild != null) customerHistoryInvoice.documentDate = docDateElement.FirstChild.Value;
				}

                XmlElement orderDateElement = (XmlElement)invoiceElement.SelectSingleNode("orderDate");
                if (orderDateElement != null)
                {
                    if (orderDateElement.FirstChild != null) customerHistoryInvoice.orderDate = orderDateElement.FirstChild.Value;
                }

				XmlElement dueDateElement = (XmlElement)invoiceElement.SelectSingleNode("dueDate");
				if (dueDateElement != null)
				{
					if (dueDateElement.FirstChild != null) customerHistoryInvoice.dueDate = dueDateElement.FirstChild.Value;
				}

				XmlElement amountElement = (XmlElement)invoiceElement.SelectSingleNode("amount");
				if (amountElement != null)
				{
					if (amountElement.FirstChild != null) customerHistoryInvoice.amount = amountElement.FirstChild.Value;
				}

				XmlElement amountInclVatElement = (XmlElement)invoiceElement.SelectSingleNode("amountInclVat");
				if (amountInclVatElement != null)
				{
					if (amountInclVatElement.FirstChild != null) customerHistoryInvoice.amountIncludingVat = amountInclVatElement.FirstChild.Value;
				}

                XmlElement sellToContactElement = (XmlElement)invoiceElement.SelectSingleNode("sellToContact");
                if (sellToContactElement != null)
                {
                    if (sellToContactElement.FirstChild != null) customerHistoryInvoice.sellToContact = sellToContactElement.FirstChild.Value;
                }

                XmlElement shipToContactElement = (XmlElement)invoiceElement.SelectSingleNode("shipToContact");
                if (shipToContactElement != null)
                {
                    if (shipToContactElement.FirstChild != null) customerHistoryInvoice.shipToContact = shipToContactElement.FirstChild.Value;
                }


				XmlElement openElement = (XmlElement)invoiceElement.SelectSingleNode("open");
				if (openElement != null)
				{
					if (openElement.FirstChild != null)
					{
						if (openElement.FirstChild.Value == "true") customerHistoryInvoice.open = true;
					}
				}

                XmlElement descriptionElement = (XmlElement)invoiceElement.SelectSingleNode("description");
                if (descriptionElement != null)
                {
                    if (descriptionElement.FirstChild != null) customerHistoryInvoice.description = descriptionElement.FirstChild.Value;
                }

				customerHistoryInvoice.link = infojetContext.webPage.getUrl()+"&docType=2&docNo="+customerHistoryInvoice.no;
                customerHistoryInvoice.pdfLink = infojetContext.webPage.getUrl() + "&docType=2&docNo=" + customerHistoryInvoice.no + "&pdf=true";

                customerHistoryInvoice.payed = infojetContext.translate("NO");
                if (!customerHistoryInvoice.open) customerHistoryInvoice.payed = infojetContext.translate("YES");


				XmlNodeList shipmentNodeList = invoiceElement.SelectNodes("shipments/shipment");
				int j = 0;
				while (j < shipmentNodeList.Count)
				{
					XmlElement shipmentElement = (XmlElement)shipmentNodeList.Item(j);

					CustomerHistoryShipment customerHistoryShipment = new CustomerHistoryShipment();

					XmlElement shipmentNoElement = (XmlElement)shipmentElement.SelectSingleNode("no");
					if (shipmentNoElement != null)
					{
						if (shipmentNoElement.FirstChild != null) customerHistoryShipment.no = shipmentNoElement.FirstChild.Value;
					}

					XmlElement shipmentDateElement = (XmlElement)shipmentElement.SelectSingleNode("shipmentDate");
					if (shipmentDateElement != null)
					{
						if (shipmentDateElement.FirstChild != null) customerHistoryShipment.shipmentDate = shipmentDateElement.FirstChild.Value;
					}

					XmlElement packageTrackingElement = (XmlElement)shipmentElement.SelectSingleNode("packageTrackingNo");
					if (packageTrackingElement != null)
					{
						if (packageTrackingElement.FirstChild != null) customerHistoryShipment.packageTrackingNo = packageTrackingElement.FirstChild.Value;
					}

                    XmlElement shippingAgentInetAddressElement = (XmlElement)shipmentElement.SelectSingleNode("shippingAgentInetAddress");
                    if (shippingAgentInetAddressElement != null)
                    {
                        if (shippingAgentInetAddressElement.FirstChild != null) customerHistoryShipment.shippingAgentInternetAddress = shippingAgentInetAddressElement.FirstChild.Value;
                    }


					customerHistoryShipment.link = infojetContext.webPage.getUrl()+"&docType=1&docNo="+customerHistoryShipment.no;

					customerHistoryInvoice.shipments.Add(customerHistoryShipment);

					j++;
				}


				invoiceCollection.Add(customerHistoryInvoice);

				i++;
			}

			return invoiceCollection;

		}

        private CustomerHistoryCrMemoCollection parseCrMemoHistoryResponse(ServiceResponse serviceResponse, Infojet infojetContext)
        {
            CustomerHistoryCrMemoCollection crMemoCollection = new CustomerHistoryCrMemoCollection();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(serviceResponse.xml);

            XmlElement documentElement = xmlDoc.DocumentElement;

            XmlElement invoicesElement = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemos");
            if (invoicesElement.GetAttribute("noOfPages") != null)
            {
                noOfPages = int.Parse(invoicesElement.GetAttribute("noOfPages"));
            }

            XmlNodeList invoicesNodeList = invoicesElement.SelectNodes("crMemo");
            int i = 0;

            while (i < invoicesNodeList.Count)
            {
                XmlElement invoiceElement = (XmlElement)invoicesNodeList.Item(i);

                CustomerHistoryCrMemo customerHistoryCrMemo = new CustomerHistoryCrMemo();

                XmlElement noElement = (XmlElement)invoiceElement.SelectSingleNode("no");
                if (noElement != null)
                {
                    if (noElement.FirstChild != null) customerHistoryCrMemo.no = noElement.FirstChild.Value;
                }

                XmlElement customerNameElement = (XmlElement)invoiceElement.SelectSingleNode("customerName");
                if (customerNameElement != null)
                {
                    if (customerNameElement.FirstChild != null) customerHistoryCrMemo.customerName = customerNameElement.FirstChild.Value;
                }

                XmlElement referenceElement = (XmlElement)invoiceElement.SelectSingleNode("yourReference");
                if (referenceElement != null)
                {
                    if (referenceElement.FirstChild != null) customerHistoryCrMemo.yourReference = referenceElement.FirstChild.Value;
                }

                XmlElement extDocNoElement = (XmlElement)invoiceElement.SelectSingleNode("externalDocumentNo");
                if (extDocNoElement != null)
                {
                    if (extDocNoElement.FirstChild != null) customerHistoryCrMemo.externalDocumentNo = extDocNoElement.FirstChild.Value;
                }

                XmlElement docDateElement = (XmlElement)invoiceElement.SelectSingleNode("documentDate");
                if (docDateElement != null)
                {
                    if (docDateElement.FirstChild != null) customerHistoryCrMemo.documentDate = docDateElement.FirstChild.Value;
                }

                XmlElement amountElement = (XmlElement)invoiceElement.SelectSingleNode("amount");
                if (amountElement != null)
                {
                    if (amountElement.FirstChild != null) customerHistoryCrMemo.amount = amountElement.FirstChild.Value;
                }

                XmlElement amountInclVatElement = (XmlElement)invoiceElement.SelectSingleNode("amountInclVat");
                if (amountInclVatElement != null)
                {
                    if (amountInclVatElement.FirstChild != null) customerHistoryCrMemo.amountIncludingVat = amountInclVatElement.FirstChild.Value;
                }

                XmlElement sellToContactElement = (XmlElement)invoiceElement.SelectSingleNode("sellToContact");
                if (sellToContactElement != null)
                {
                    if (sellToContactElement.FirstChild != null) customerHistoryCrMemo.sellToContact = sellToContactElement.FirstChild.Value;
                }

                XmlElement shipToContactElement = (XmlElement)invoiceElement.SelectSingleNode("shipToContact");
                if (shipToContactElement != null)
                {
                    if (shipToContactElement.FirstChild != null) customerHistoryCrMemo.shipToContact = shipToContactElement.FirstChild.Value;
                }



                XmlElement openElement = (XmlElement)invoiceElement.SelectSingleNode("open");
                if (openElement != null)
                {
                    if (openElement.FirstChild != null)
                    {
                        if (openElement.FirstChild.Value == "true") customerHistoryCrMemo.open = true;
                    }
                }

                customerHistoryCrMemo.link = infojetContext.webPage.getUrl() + "&docType=3&docNo=" + customerHistoryCrMemo.no;
                customerHistoryCrMemo.pdfLink = infojetContext.webPage.getUrl() + "&docType=3&docNo=" + customerHistoryCrMemo.no + "&pdf=true";

                customerHistoryCrMemo.payed = infojetContext.translate("NO");
                if (!customerHistoryCrMemo.open) customerHistoryCrMemo.payed = infojetContext.translate("YES");


                crMemoCollection.Add(customerHistoryCrMemo);

                i++;
            }

            return crMemoCollection;

        }

		private CustomerHistoryOrder parseOrderInfoResponse(ServiceResponse serviceResponse, Infojet infojetContext)
		{
            
			CustomerHistoryOrder order = new CustomerHistoryOrder();

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(serviceResponse.xml);

			XmlElement documentElement = xmlDoc.DocumentElement;

			XmlElement element;

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/no");
			if (element != null)
			{
				if (element.FirstChild != null) order.no = element.FirstChild.Value;
			}

			order.transfered = false;

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/transfered");
			if (element != null)
			{
				if (element.FirstChild != null)
				{
					if (element.FirstChild.Value == "true") order.transfered = true;
				}
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/customerNo");
			if (element != null)
			{
				if (element.FirstChild != null) order.customerNo = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/customerName");
			if (element != null)
			{
				if (element.FirstChild != null) order.customerName = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/customerName2");
			if (element != null)
			{
				if (element.FirstChild != null) order.customerName2 = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/address");
			if (element != null)
			{
				if (element.FirstChild != null) order.address = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/address2");
			if (element != null)
			{
				if (element.FirstChild != null) order.address2 = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/postCode");
			if (element != null)
			{
				if (element.FirstChild != null) order.postCode = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/city");
			if (element != null)
			{
				if (element.FirstChild != null) order.city = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/countryCode");
			if (element != null)
			{
				if (element.FirstChild != null) order.countryCode = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/shipToName");
			if (element != null)
			{
				if (element.FirstChild != null) order.shipToName = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/shipToName2");
			if (element != null)
			{
				if (element.FirstChild != null) order.shipToName2 = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/shipToAddress");
			if (element != null)
			{
				if (element.FirstChild != null) order.shipToAddress = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/shipToAddress2");
			if (element != null)
			{
				if (element.FirstChild != null) order.shipToAddress2 = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/shipToPostCode");
			if (element != null)
			{
				if (element.FirstChild != null) order.shipToPostCode = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/shipToCity");
			if (element != null)
			{
				if (element.FirstChild != null) order.shipToCity = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/shipToCountryCode");
			if (element != null)
			{
				if (element.FirstChild != null) order.shipToCountryCode = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/orderDate");
			if (element != null)
			{
				if (element.FirstChild != null) order.orderDate = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/yourReference");
			if (element != null)
			{
				if (element.FirstChild != null) order.yourReference = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/externalDocumentNo");
			if (element != null)
			{
				if (element.FirstChild != null) order.externalDocumentNo = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/amount");
			if (element != null)
			{
				if (element.FirstChild != null) order.amount = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/amountInclVat");
			if (element != null)
			{
				if (element.FirstChild != null) order.amountIncludingVat = element.FirstChild.Value;
			}

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/sellToContact");
            if (element != null)
            {
                if (element.FirstChild != null) order.sellToContact = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/shipToContact");
            if (element != null)
            {
                if (element.FirstChild != null) order.shipToContact = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/sellToContact");
            if (element != null)
            {
                if (element.FirstChild != null) order.sellToContact = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/billToContact");
            if (element != null)
            {
                if (element.FirstChild != null) order.billToContact = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/email");
            if (element != null)
            {
                if (element.FirstChild != null) order.email = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/billToPhoneNo");
            if (element != null)
            {
                if (element.FirstChild != null) order.billToPhoneNo = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/shipToPhoneNo");
            if (element != null)
            {
                if (element.FirstChild != null) order.shipToPhoneNo = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/shipmentMethodCode");
            if (element != null)
            {
                if (element.FirstChild != null) order.shipmentMethodCode = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/paymentMethodCode");
            if (element != null)
            {
                if (element.FirstChild != null) order.paymentMethodCode = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/shipmentDescription");
            if (element != null)
            {
                if (element.FirstChild != null) order.shipmentDescription = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/paymentDescription");
            if (element != null)
            {
                if (element.FirstChild != null) order.paymentDescription = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/shippingAgentCode");
            if (element != null)
            {
                if (element.FirstChild != null) order.shippingAgentCode = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/shippingAgentServiceCode");
            if (element != null)
            {
                if (element.FirstChild != null) order.shippingAgentServiceCode = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/shippingAgentName");
            if (element != null)
            {
                if (element.FirstChild != null) order.shippingAgentName = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/shippingAgentServiceName");
            if (element != null)
            {
                if (element.FirstChild != null) order.shippingAgentServiceName = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/externalDocumentNo");
            if (element != null)
            {
                if (element.FirstChild != null) order.externalDocumentNo = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/order/description");
            if (element != null)
            {
                if (element.FirstChild != null) order.description = element.FirstChild.Value;
            }

			XmlNodeList lineNodeList = documentElement.SelectNodes("serviceResponse/customerHistory/order/lines/line");
			int j = 0;
			while (j < lineNodeList.Count)
			{
				XmlElement lineElement = (XmlElement)lineNodeList.Item(j);

				CustomerHistoryLine line = new CustomerHistoryLine();

				element = (XmlElement)lineElement.SelectSingleNode("no");
				if (element != null)
				{
					if (element.FirstChild != null) line.no = element.FirstChild.Value;
				}

				element = (XmlElement)lineElement.SelectSingleNode("description");
				if (element != null)
				{
					if (element.FirstChild != null) line.description = element.FirstChild.Value;
				}

				element = (XmlElement)lineElement.SelectSingleNode("unitOfMeasureCode");
				if (element != null)
				{
					if (element.FirstChild != null) line.unitOfMeasureCode = element.FirstChild.Value;
				}

				element = (XmlElement)lineElement.SelectSingleNode("quantity");
				if (element != null)
				{
					if (element.FirstChild != null) line.quantity = element.FirstChild.Value;
				}

				element = (XmlElement)lineElement.SelectSingleNode("unitPrice");
				if (element != null)
				{
					if (element.FirstChild != null) line.unitPrice = element.FirstChild.Value;
				}

				element = (XmlElement)lineElement.SelectSingleNode("amount");
				if (element != null)
				{
					if (element.FirstChild != null) line.amount = element.FirstChild.Value;
				}

				element = (XmlElement)lineElement.SelectSingleNode("amountInclVat");
				if (element != null)
				{
					if (element.FirstChild != null) line.amountInclVat = element.FirstChild.Value;
				}

				element = (XmlElement)lineElement.SelectSingleNode("shipmentDate");
				if (element != null)
				{
					if (element.FirstChild != null) line.shipmentDate = element.FirstChild.Value;
				}

				order.lines.Add(line);

				j++;
			}

			return order;
            
   		}

		private CustomerHistoryShipment parseShipmentInfoResponse(ServiceResponse serviceResponse, Infojet infojetContext)
		{

			CustomerHistoryShipment shipment = new CustomerHistoryShipment();

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(serviceResponse.xml);

			XmlElement documentElement = xmlDoc.DocumentElement;

			XmlElement element;

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/no");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.no = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/customerNo");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.customerNo = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/customerName");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.customerName = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/customerName2");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.customerName2 = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/address");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.address = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/address2");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.address2 = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/postCode");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.postCode = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/city");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.city = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/countryCode");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.countryCode = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/shipToName");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.shipToName = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/shipToName2");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.shipToName2 = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/shipToAddress");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.shipToAddress = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/shipToAddress2");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.shipToAddress2 = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/shipToPostCode");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.shipToPostCode = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/shipToCity");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.shipToCity = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/shipToCountryCode");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.shipToCountryCode = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/shippingAgentCode");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.shippingAgentCode = element.FirstChild.Value;
			}

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/shippingAgentServiceCode");
            if (element != null)
            {
                if (element.FirstChild != null) shipment.shippingAgentServiceCode = element.FirstChild.Value;
            }

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/shippingAgentName");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.shippingAgentName = element.FirstChild.Value;
			}

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/shippingAgentServiceName");
            if (element != null)
            {
                if (element.FirstChild != null) shipment.shippingAgentServiceName = element.FirstChild.Value;
            }

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/shippingAgentInetAddress");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.shippingAgentInternetAddress = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/orderDate");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.orderDate = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/shipmentDate");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.shipmentDate = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/yourReference");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.yourReference = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/externalDocumentNo");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.externalDocumentNo = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/packageTrackingNo");
			if (element != null)
			{
				if (element.FirstChild != null) shipment.packageTrackingNo = element.FirstChild.Value;
			}

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/sellToContact");
            if (element != null)
            {
                if (element.FirstChild != null) shipment.sellToContact = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/shipToContact");
            if (element != null)
            {
                if (element.FirstChild != null) shipment.shipToContact = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/shipment/description");
            if (element != null)
            {
                if (element.FirstChild != null) shipment.description = element.FirstChild.Value;
            }


			XmlNodeList lineNodeList = documentElement.SelectNodes("serviceResponse/customerHistory/shipment/lines/line");
			int j = 0;
			while (j < lineNodeList.Count)
			{
				XmlElement lineElement = (XmlElement)lineNodeList.Item(j);

				CustomerHistoryLine line = new CustomerHistoryLine();

				element = (XmlElement)lineElement.SelectSingleNode("no");
				if (element != null)
				{
					if (element.FirstChild != null) line.no = element.FirstChild.Value;
				}

				element = (XmlElement)lineElement.SelectSingleNode("description");
				if (element != null)
				{
					if (element.FirstChild != null) line.description = element.FirstChild.Value;
				}

				element = (XmlElement)lineElement.SelectSingleNode("unitOfMeasureCode");
				if (element != null)
				{
					if (element.FirstChild != null) line.unitOfMeasureCode = element.FirstChild.Value;
				}

				element = (XmlElement)lineElement.SelectSingleNode("quantity");
				if (element != null)
				{
					if (element.FirstChild != null) line.quantity = element.FirstChild.Value;
				}

				element = (XmlElement)lineElement.SelectSingleNode("shipmentDate");
				if (element != null)
				{
					if (element.FirstChild != null) line.shipmentDate = element.FirstChild.Value;
				}

				shipment.lines.Add(line);

				j++;
			}

			return shipment;

		}

		private CustomerHistoryInvoice parseInvoiceInfoResponse(ServiceResponse serviceResponse, Infojet infojetContext)
		{

			CustomerHistoryInvoice invoice = new CustomerHistoryInvoice();

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(serviceResponse.xml);

			XmlElement documentElement = xmlDoc.DocumentElement;

			XmlElement element;

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/no");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.no = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/orderNo");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.orderNo = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/customerNo");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.customerNo = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/customerName");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.customerName = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/customerName2");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.customerName2 = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/address");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.address = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/address2");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.address2 = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/postCode");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.postCode = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/city");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.city = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/countryCode");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.countryCode = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/shipToName");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.shipToName = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/shipToName2");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.shipToName2 = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/shipToAddress");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.shipToAddress = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/shipToAddress2");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.shipToAddress2 = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/shipToPostCode");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.shipToPostCode = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/shipToCity");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.shipToCity = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/shipToCountryCode");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.shipToCountryCode = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/orderDate");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.orderDate = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/documentDate");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.documentDate = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/dueDate");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.dueDate = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/yourReference");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.yourReference = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/externalDocumentNo");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.externalDocumentNo = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/amount");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.amount = element.FirstChild.Value;
			}

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/open");
			if (element != null)
			{
                if ((element.FirstChild != null) && (element.FirstChild.Value == "true"))
                {
                    invoice.open = true;
                }
			}

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/sellToContact");
            if (element != null)
            {
                if (element.FirstChild != null) invoice.sellToContact = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/shipToContact");
            if (element != null)
            {
                if (element.FirstChild != null) invoice.shipToContact = element.FirstChild.Value;
            }

			element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/amountInclVat");
			if (element != null)
			{
				if (element.FirstChild != null) invoice.amountIncludingVat = element.FirstChild.Value;
			}

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/invoice/description");
            if (element != null)
            {
                if (element.FirstChild != null) invoice.description = element.FirstChild.Value;
            }

            invoice.payed = infojetContext.translate("NO");
            if (!invoice.open) invoice.payed = infojetContext.translate("YES");

            invoice.pdfLink = infojetContext.webPage.getUrl() + "&docType=2&docNo=" + invoice.no + "&pdf=true";

			XmlNodeList lineNodeList = documentElement.SelectNodes("serviceResponse/customerHistory/invoice/lines/line");
			int j = 0;
			while (j < lineNodeList.Count)
			{
				XmlElement lineElement = (XmlElement)lineNodeList.Item(j);

				CustomerHistoryLine line = new CustomerHistoryLine();

				element = (XmlElement)lineElement.SelectSingleNode("no");
				if (element != null)
				{
					if (element.FirstChild != null) line.no = element.FirstChild.Value;
				}

				element = (XmlElement)lineElement.SelectSingleNode("description");
				if (element != null)
				{
					if (element.FirstChild != null) line.description = element.FirstChild.Value;
				}

				element = (XmlElement)lineElement.SelectSingleNode("unitOfMeasureCode");
				if (element != null)
				{
					if (element.FirstChild != null) line.unitOfMeasureCode = element.FirstChild.Value;
				}

				element = (XmlElement)lineElement.SelectSingleNode("quantity");
				if (element != null)
				{
					if (element.FirstChild != null) line.quantity = element.FirstChild.Value;
				}

				element = (XmlElement)lineElement.SelectSingleNode("unitPrice");
				if (element != null)
				{
					if (element.FirstChild != null) line.unitPrice = element.FirstChild.Value;
				}

				element = (XmlElement)lineElement.SelectSingleNode("amount");
				if (element != null)
				{
					if (element.FirstChild != null) line.amount = element.FirstChild.Value;
				}

				element = (XmlElement)lineElement.SelectSingleNode("amountInclVat");
				if (element != null)
				{
					if (element.FirstChild != null) line.amountInclVat = element.FirstChild.Value;
				}

				invoice.lines.Add(line);

				j++;
			}

			return invoice;

		}

        private CustomerHistoryCrMemo parseCrMemoInfoResponse(ServiceResponse serviceResponse, Infojet infojetContext)
        {

            CustomerHistoryCrMemo crMemo = new CustomerHistoryCrMemo();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(serviceResponse.xml);

            XmlElement documentElement = xmlDoc.DocumentElement;

            XmlElement element;

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/no");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.no = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/customerNo");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.customerNo = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/customerName");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.customerName = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/customerName2");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.customerName2 = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/address");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.address = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/address2");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.address2 = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/postCode");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.postCode = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/city");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.city = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/countryCode");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.countryCode = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/shipToName");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.shipToName = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/shipToName2");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.shipToName2 = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/shipToAddress");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.shipToAddress = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/shipToAddress2");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.shipToAddress2 = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/shipToPostCode");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.shipToPostCode = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/shipToCity");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.shipToCity = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/shipToCountryCode");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.shipToCountryCode = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/documentDate");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.documentDate = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/yourReference");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.yourReference = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/externalDocumentNo");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.externalDocumentNo = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/amount");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.amount = element.FirstChild.Value;
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/open");
            if (element != null)
            {
                if ((element.FirstChild != null) && (element.FirstChild.Value == "true"))
                {
                    crMemo.open = true;
                }
            }

            element = (XmlElement)documentElement.SelectSingleNode("serviceResponse/customerHistory/crMemo/amountInclVat");
            if (element != null)
            {
                if (element.FirstChild != null) crMemo.amountIncludingVat = element.FirstChild.Value;
            }

            crMemo.payed = infojetContext.translate("NO");
            if (!crMemo.open) crMemo.payed = infojetContext.translate("YES");

            crMemo.pdfLink = infojetContext.webPage.getUrl() + "&docType=2&docNo=" + crMemo.no + "&pdf=true";

            XmlNodeList lineNodeList = documentElement.SelectNodes("serviceResponse/customerHistory/crMemo/lines/line");
            int j = 0;
            while (j < lineNodeList.Count)
            {
                XmlElement lineElement = (XmlElement)lineNodeList.Item(j);

                CustomerHistoryLine line = new CustomerHistoryLine();

                element = (XmlElement)lineElement.SelectSingleNode("no");
                if (element != null)
                {
                    if (element.FirstChild != null) line.no = element.FirstChild.Value;
                }

                element = (XmlElement)lineElement.SelectSingleNode("description");
                if (element != null)
                {
                    if (element.FirstChild != null) line.description = element.FirstChild.Value;
                }

                element = (XmlElement)lineElement.SelectSingleNode("unitOfMeasureCode");
                if (element != null)
                {
                    if (element.FirstChild != null) line.unitOfMeasureCode = element.FirstChild.Value;
                }

                element = (XmlElement)lineElement.SelectSingleNode("quantity");
                if (element != null)
                {
                    if (element.FirstChild != null) line.quantity = element.FirstChild.Value;
                }

                element = (XmlElement)lineElement.SelectSingleNode("unitPrice");
                if (element != null)
                {
                    if (element.FirstChild != null) line.unitPrice = element.FirstChild.Value;
                }

                element = (XmlElement)lineElement.SelectSingleNode("amount");
                if (element != null)
                {
                    if (element.FirstChild != null) line.amount = element.FirstChild.Value;
                }

                element = (XmlElement)lineElement.SelectSingleNode("amountInclVat");
                if (element != null)
                {
                    if (element.FirstChild != null) line.amountInclVat = element.FirstChild.Value;
                }

                crMemo.lines.Add(line);

                j++;
            }

            return crMemo;

        }

        private CustomerHistoryLedgerCollection parseCustomerLedgerInfoResponse(ServiceResponse serviceResponse, Infojet infojetContext)
        {

            CustomerHistoryLedgerCollection customerHistoryLedgerCollection = new CustomerHistoryLedgerCollection();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(serviceResponse.xml);

            XmlElement documentElement = xmlDoc.DocumentElement;


            XmlNodeList lineNodeList = documentElement.SelectNodes("serviceResponse/customerHistory/customerLedger/entry");
            int j = 0;
            while (j < lineNodeList.Count)
            {
                XmlElement lineElement = (XmlElement)lineNodeList.Item(j);

                CustomerHistoryLedger ledgerEntry = new CustomerHistoryLedger();

                XmlElement element = (XmlElement)lineElement.SelectSingleNode("documentType");
                if (element != null)
                {
                    if (element.FirstChild != null) ledgerEntry.documentType = int.Parse(element.FirstChild.Value);
                }

                element = (XmlElement)lineElement.SelectSingleNode("documentNo");
                if (element != null)
                {
                    if (element.FirstChild != null) ledgerEntry.documentNo = element.FirstChild.Value;
                }

                element = (XmlElement)lineElement.SelectSingleNode("description");
                if (element != null)
                {
                    if (element.FirstChild != null) ledgerEntry.description = element.FirstChild.Value;
                }

                element = (XmlElement)lineElement.SelectSingleNode("amount");
                if (element != null)
                {
                    if (element.FirstChild != null) ledgerEntry.amount = element.FirstChild.Value;
                }

                element = (XmlElement)lineElement.SelectSingleNode("remainingAmount");
                if (element != null)
                {
                    if (element.FirstChild != null) ledgerEntry.remainingAmount = element.FirstChild.Value;
                }

                element = (XmlElement)lineElement.SelectSingleNode("open");
                if (element != null)
                {
                    if (element.FirstChild != null)
                    {
                        if (element.FirstChild.Value == "1") ledgerEntry.open = true;
                    }
                }

                if (ledgerEntry.documentType == 1) ledgerEntry.documentTypeDescription = infojetContext.translate("PAYMENT");
                if (ledgerEntry.documentType == 2) ledgerEntry.documentTypeDescription = infojetContext.translate("INVOICE");
                if (ledgerEntry.documentType == 3) ledgerEntry.documentTypeDescription = infojetContext.translate("CREDIT MEMO");
                //if (ledgerEntry.documentType == 4) ledgerEntry.documentTypeDescription = infojetContext.translate("");
                if (ledgerEntry.documentType == 5) ledgerEntry.documentTypeDescription = infojetContext.translate("REMINDER");
                if (ledgerEntry.documentType == 6) ledgerEntry.documentTypeDescription = infojetContext.translate("REFUND");
                if (ledgerEntry.documentType == 99) ledgerEntry.documentTypeDescription = infojetContext.translate("TO PAY");

                customerHistoryLedgerCollection.Add(ledgerEntry);

                j++;
            }

            return customerHistoryLedgerCollection;

        }

        public void requestPdfDocument(int documentType, string documentNo)
        {
            ApplicationServerConnection appServerConnection;

            appServerConnection = new ApplicationServerConnection(infojetContext, new ServiceRequest(infojetContext, "requestDocument", new HistoryDocument(documentType, documentNo, infojetContext.userSession.webUserAccount)));
            if (appServerConnection.processRequest())
            {
                object scalarObject = infojetContext.systemDatabase.scalarQuery("SELECT [Document] FROM [" + infojetContext.systemDatabase.getTableName("Web Requested Document") + "] WHERE [Document Type] = '" + documentType + "' AND [Document No_] = '" + documentNo + "'");
                if (scalarObject.GetType() == typeof(System.DBNull)) return;
                byte[] imageByteArray = (byte[])scalarObject;
                if (imageByteArray != null)
                {
                        MemoryStream memoryStream = new MemoryStream(imageByteArray);

                        System.Web.HttpContext.Current.Response.Clear();
                        System.Web.HttpContext.Current.Response.AddHeader("Accept-Header", memoryStream.Length.ToString());
                        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                        System.Web.HttpContext.Current.Response.OutputStream.Write(memoryStream.ToArray(), 0, Convert.ToInt32(memoryStream.Length));
                        System.Web.HttpContext.Current.Response.Flush();

                        //memoryStream.WriteTo(System.Web.HttpContext.Current.Response.OutputStream);
                        System.Web.HttpContext.Current.Response.End();
                }
                else
                {
                    //throw new Exception("ImageNotFoundException: "+this.code);
                }
            }
        }

        public string composePageNavigator()
        {
            string pageStr = "";

            int pageInt = 2;

            if ((getCurrentPageNo() - pageInt) > 1)
            {
                pageStr = "<a href=\"" + infojetContext.webPage.getUrl() + "&pageNo=1\">1</a> .. ";
            }

            int downPages = pageInt;
            if (getCurrentPageNo() <= downPages) downPages = getCurrentPageNo() - 1;

            int j = downPages;
            while (j > 0)
            {
                pageStr = pageStr + "<a href=\"" + infojetContext.webPage.getUrl() + "&pageNo=" + (getCurrentPageNo() - j) + "\">" + (getCurrentPageNo() - j) + "</a> ";

                j--;
            }

            pageStr = pageStr + "<b>" + getCurrentPageNo() + "</b> ";

            int upPages = pageInt;
            if (upPages > (getNoOfPages() - getCurrentPageNo())) upPages = getNoOfPages() - getCurrentPageNo();

            j = 0;
            while (j < upPages)
            {
                pageStr = pageStr + "<a href=\"" + infojetContext.webPage.getUrl() + "&pageNo=" + (getCurrentPageNo() + j + 1) + "\">" + (getCurrentPageNo() + j + 1) + "</a> ";

                j++;
            }

            if ((getCurrentPageNo() + pageInt) < getNoOfPages())
            {
                pageStr = pageStr + " .. <a href=\"" + infojetContext.webPage.getUrl() + "&pageNo=" + getNoOfPages() + "\">" + getNoOfPages() + "</a>";
            }

            return pageStr;
        }

	}
}
