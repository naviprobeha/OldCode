using System;
using System.Xml;
using Navipro.SantaMonica.Common;

namespace Navipro.SantaMonica.NavisionConnector
{
	/// <summary>
	/// Summary description for Publication.
	/// </summary>
	public class Publication
	{
		private Header headerObject;	
		private Data dataObject;
		private Logger logger;
		private Configuration configuration;

		public Publication(Logger logger)
		{
			this.logger = logger;
			//
			// TODO: Add constructor logic here
			//
		}

		public Publication(XmlElement publicationElement, Logger logger, Configuration configuration)
		{
			this.configuration = configuration;
			this.logger = logger;
			//log("Reading publication...", 0);
			fromDOM(publicationElement);
		}

		public void fromDOM(XmlElement publicationElement)
		{
			XmlNodeList headers = publicationElement.GetElementsByTagName("HEADER");
			if (headers.Count > 0)
			{
				headerObject = new Header((XmlElement)headers.Item(0));
			}
			else
			{
				//log("No header object found...", 0);
			}

			XmlNodeList dataItems = publicationElement.GetElementsByTagName("DATA");
			if (dataItems.Count > 0)
			{
				if (headerObject != null)
				{
					dataObject = new Data((XmlElement)dataItems.Item(0), logger, headerObject, configuration);
				}
			}

			
		}

		private void log(string message, int type)
		{
			logger.write("[Publication] "+message, type);
		}
	}
}
