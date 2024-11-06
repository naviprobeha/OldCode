using System;
using System.Xml;
using NavisionConnector.se.navipro.dev1;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.NavisionConnector
{
	/// <summary>
	/// Summary description for DataShipmentHeaders.
	/// </summary>
	public class DataFactoryOrders
	{
		private Configuration configuration;
		private Logger logger;

		public DataFactoryOrders(XmlElement tableElement, Configuration configuration, Logger logger)
		{
			this.configuration = configuration;
			this.logger = logger;
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
				int no = 0;

				XmlElement recordElement = (XmlElement)records.Item(i);
	
				XmlNodeList fields = recordElement.GetElementsByTagName("F");
				int j = 0;
				while (j < fields.Count)
				{
					XmlElement field = (XmlElement)fields.Item(j);
	
					XmlAttribute fieldNo = field.GetAttributeNode("NO");
					String fieldValue = "";

					if (field.HasChildNodes) fieldValue = field.FirstChild.Value;			
					if (fieldNo.FirstChild.Value.Equals("1")) no = int.Parse(fieldValue.Replace("'", ""));


					j++;
				}				

				if (no != 0)
				{
					Quiksilver quikSilver = new Quiksilver();
					quikSilver.Url = configuration.webServiceUrl;
					quikSilver.setFactoryOrderEntryStatus(no, 3);
				}
				

				i++;
			}
		}
	}
}
