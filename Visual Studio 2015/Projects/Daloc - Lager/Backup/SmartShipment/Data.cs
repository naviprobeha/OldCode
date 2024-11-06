using System;
using System.Collections;
using System.Xml;

namespace SmartShipment
{
	/// <summary>
	/// Summary description for Data.
	/// </summary>
	public class Data
	{
		private Logger logger;
		private SmartDatabase smartDatabase;

		public Data(SmartDatabase smartDatabase, Logger logger)
		{
			this.smartDatabase = smartDatabase;
			this.logger = logger;
					
			//
			// TODO: Add constructor logic here
			//
		}

		public Data(XmlElement dataElement, SmartDatabase smartDatabase, Logger logger)
		{
			this.logger = logger;
			fromDOM(dataElement, smartDatabase);
		}

		public void fromDOM(XmlElement dataElement, SmartDatabase smartDatabase)
		{
			XmlNodeList tables = dataElement.GetElementsByTagName("T");
			int i = 0;
			while (i < tables.Count)
			{
				XmlElement table = (XmlElement)tables.Item(i);
				XmlAttribute attribute = table.GetAttributeNode("NO");

				if (attribute.FirstChild.Value.Equals("18"))
				{
					log("Läser in kunder.");
					DataCollection dataCollection = new DataCustomers(table, smartDatabase);
				}

				if (attribute.FirstChild.Value.Equals("27"))
				{
					log("Läser in artiklar.");
					DataCollection dataCollection = new DataItems(table, smartDatabase);
				}

				i++;
			}
	
		}

		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			XmlElement dataElement = xmlDocumentContext.CreateElement("DATA");
			
			DataSalesHeaders salesHeaders = new DataSalesHeaders(smartDatabase);
			dataElement.AppendChild(salesHeaders.toDOM(xmlDocumentContext));
	
			DataSalesLines salesLines = new DataSalesLines(smartDatabase);
			dataElement.AppendChild(salesLines.toDOM(xmlDocumentContext));

			return dataElement;
		}

		private void log(string message)
		{
			if (logger != null) logger.write(message);
		}
	}
}
