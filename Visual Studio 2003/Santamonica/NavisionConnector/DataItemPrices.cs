using System;
using System.Xml;
using Navipro.SantaMonica.Common;
using NavisionConnector.se.navipro.dev1;

namespace Navipro.SantaMonica.NavisionConnector
{
	/// <summary>
	/// Summary description for DataItemPrices.
	/// </summary>
	public class DataItemPrices
	{
		private Configuration configuration;

		public DataItemPrices(XmlElement tableElement, Configuration configuration)
		{
			this.configuration = configuration;
			//
			// TODO: Add constructor logic here
			//
			fromDOM(tableElement);
		}

		public void fromDOM(XmlElement tableElement)
		{
			XmlNodeList records = tableElement.GetElementsByTagName("R");
			int i = 0;
			while (i < records.Count)
			{
				XmlElement record = (XmlElement)records.Item(i);
	
				Quiksilver quikSilver = new Quiksilver();
				quikSilver.Url = configuration.webServiceUrl;
				quikSilver.updateItemPrice(record.OuterXml);

				//DataCustomer dataCustomer = new DataCustomer(record, organization, true);

				i++;
			}
		}
	}
}
