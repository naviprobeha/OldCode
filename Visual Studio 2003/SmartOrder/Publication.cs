using System;
using System.Xml;


namespace SmartOrder
{
	/// <summary>
	/// Summary description for Publication.
	/// </summary>
	public class Publication : ServiceArgument
	{
		private Header headerObject;	
		private Data dataObject;
		private Logger logger;
		private SmartDatabase smartDatabase;

		public Publication(SmartDatabase smartDatabase, Logger logger)
		{
			this.smartDatabase = smartDatabase;
			this.logger = logger;
			//
			// TODO: Add constructor logic here
			//
		}

		public Publication(XmlElement publicationElement, SmartDatabase smartDatabase, Logger logger)
		{
			this.logger = logger;
			fromDOM(publicationElement, smartDatabase);
		}

		public void fromDOM(XmlElement publicationElement, SmartDatabase smartDatabase)
		{
			XmlNodeList headers = publicationElement.GetElementsByTagName("HEADER");
			if (headers.Count > 0)
			{
				headerObject = new Header((XmlElement)headers.Item(0));
			}

			XmlNodeList dataItems = publicationElement.GetElementsByTagName("DATA");
			if (dataItems.Count > 0)
			{
				dataObject = new Data((XmlElement)dataItems.Item(0), smartDatabase, logger);
			}
			
		}

		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			Agent agent = new Agent(smartDatabase);
			DataSetup setup = new DataSetup(smartDatabase);

			XmlElement publicationElement = xmlDocumentContext.CreateElement("PUBLICATION");
			
			Data data = new Data(smartDatabase, logger);
			publicationElement.AppendChild(data.toDOM(xmlDocumentContext));

			Header header = new Header(agent.agentId, setup.receiver);
			publicationElement.AppendChild(header.toDOM(xmlDocumentContext));

			return publicationElement;
		}

	}
}
