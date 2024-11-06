using System;
using System.Xml;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.NavisionConnector
{
	/// <summary>
	/// Summary description for Data.
	/// </summary>
	public class Data
	{
		private Logger logger;
		private Organization organization;
		private Configuration configuration;

		public Data(Logger logger)
		{
			this.logger = logger;
					
			//
			// TODO: Add constructor logic here
			//
		}

		public Data(XmlElement dataElement, Logger logger, Header header, Configuration configuration)
		{
			this.logger = logger;
			this.configuration = configuration;
			organization = new Organization(header.receiver);
			fromDOM(dataElement);
		}

		public void fromDOM(XmlElement dataElement)
		{
			XmlNodeList tables = dataElement.GetElementsByTagName("T");
			int i = 0;
			while (i < tables.Count)
			{
				XmlElement table = (XmlElement)tables.Item(i);
				XmlAttribute attribute = table.GetAttributeNode("NO");

				if (attribute.FirstChild.Value.Equals("18"))
				{
					DataCustomers dataCustomers = new DataCustomers(table, organization, configuration);
				}

				if (attribute.FirstChild.Value.Equals("27"))
				{
					DataItems items = new DataItems(table, configuration);
				}

				if (attribute.FirstChild.Value.Equals("7002"))
				{
					DataItemPrices itemPrices = new DataItemPrices(table, configuration);
				}

				if (attribute.FirstChild.Value.Equals("7012"))
				{
					DataPurchasePrices purchasePrices = new DataPurchasePrices(table, configuration);
				}

				if (attribute.FirstChild.Value.Equals("50014"))
				{
					DataItemPricesExtended extItemPrices = new DataItemPricesExtended(table, configuration);
				}

				if (attribute.FirstChild.Value.Equals("50008"))
				{
					DataShipmentHeaders shipmentHeaders = new DataShipmentHeaders(table, configuration, logger);
				}

				if (attribute.FirstChild.Value.Equals("50012"))
				{
					DataShipmentHeaders shipmentHeaders = new DataShipmentHeaders(table, configuration, logger);
				}

				if (attribute.FirstChild.Value.Equals("50024"))
				{
					DataScaleEntries scaleEntries = new DataScaleEntries(table, configuration, logger);
				}

				if (attribute.FirstChild.Value.Equals("50025"))
				{
					DataFactoryOrders factoryOrders = new DataFactoryOrders(table, configuration, logger);
				}

				if (attribute.FirstChild.Value.Equals("84110"))
				{
					DataShippingCustomers shippingCustomers = new DataShippingCustomers(table, configuration);
				}

				i++;
			}
	
		}	

		private void log(string message, int type)
		{
			if (logger != null) logger.write(message, type);
		}

	}
	
}
