using System;
using System.Collections;
using System.Xml;

namespace SmartInventory
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

				if (attribute.FirstChild.Value.Equals("58006"))
				{
					log("Läser in lagerplatser.");
					DataCollection dataCollection = new DataBins(table, smartDatabase);
				}
				if (attribute.FirstChild.Value.Equals("58010"))
				{
					log("Läser in uppdrag.");
					DataCollection dataCollection = new DataWhseActivityHeaders(table, smartDatabase);
				}
				if (attribute.FirstChild.Value.Equals("58011"))
				{
					log("Läser in uppdragsrader.");
					DataCollection dataCollection = new DataWhseActivityLines(table, smartDatabase);
				}

				i++;
			}
	
		}

		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			XmlElement dataElement = xmlDocumentContext.CreateElement("DATA");
			
			//DataWhseActivityLines dataWhseActivityLines = new DataWhseActivityLines(smartDatabase);
			//dataElement.AppendChild(dataWhseActivityLines.toDOM(xmlDocumentContext));

			DataItemUnits dataItemUnits = new DataItemUnits(smartDatabase);
			dataElement.AppendChild(dataItemUnits.toDOM(xmlDocumentContext));

			//DataWhseItemStores dataWhseItemStores = new DataWhseItemStores(smartDatabase);
			//dataElement.AppendChild(dataWhseItemStores.toDOM(xmlDocumentContext));
	
			return dataElement;
		}

		public void postDOM()
		{
			DataItemUnits dataItemUnits = new DataItemUnits(smartDatabase);
			dataItemUnits.deleteAll();

			//DataWhseItemStores dataWhseItemStores = new DataWhseItemStores(smartDatabase);
			//dataWhseItemStores.deleteAll();

			DataWhseActivityHeaders dataWhseActivityHeaders = new DataWhseActivityHeaders(smartDatabase);
			dataWhseActivityHeaders.deleteAll(DataWhseActivityHeaders.WHSE_TYPE_ARRIVAL);
			dataWhseActivityHeaders.deleteAll(DataWhseActivityHeaders.WHSE_TYPE_MOVEMENT);

		}

		private void log(string message)
		{
			if (logger != null) logger.write(message);
		}
	}
}
