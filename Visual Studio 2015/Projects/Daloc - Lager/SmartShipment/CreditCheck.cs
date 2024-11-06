using System;
using System.Xml;

namespace SmartShipment
{
	/// <summary>
	/// Summary description for Inventory.
	/// </summary>
	public class CreditCheck
	{
		private Logger logger;
		private SmartDatabase smartDatabase;
		private DataCustomer dataCustomer;
		private XmlElement xmlCreditElement;
		private bool statusValue;

		public CreditCheck(SmartDatabase smartDatabase, Logger logger)
		{
			this.smartDatabase = smartDatabase;
			this.logger = logger;
			//
			// TODO: Add constructor logic here
			//
			statusValue = false;
		}

		public CreditCheck(XmlElement creditElement, SmartDatabase smartDatabase, Logger logger)
		{
			this.logger = logger;
			fromDOM(creditElement, smartDatabase);
		}

		public DataCustomer customer
		{
			get
			{
				return dataCustomer;
			}
		}

		public bool status
		{
			get
			{
				return statusValue;
			}
		}

		public XmlElement xmlCreditData
		{
			get
			{
				return xmlCreditElement;
			}
		}


		public void fromDOM(XmlElement xmlCreditElement, SmartDatabase smartDatabase)
		{
			this.xmlCreditElement = xmlCreditElement;

			XmlNodeList customers = xmlCreditElement.GetElementsByTagName("CUSTOMER");
			if (customers.Count > 0)
			{
				dataCustomer = new DataCustomer((XmlElement)customers.Item(0), smartDatabase, false);
			}

			XmlNodeList status = xmlCreditElement.GetElementsByTagName("STATUS");
			if (status.Count > 0)
			{
				XmlText statusText = (XmlText)status.Item(0).FirstChild;
				if (statusText.Value == "1") this.statusValue = true;
			}

		}

	}
}
