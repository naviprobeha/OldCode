using System;
using System.Xml;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for ServiceResponse.
	/// </summary>
	public class ServiceResponse
	{
		private Publication publicationObject;
		private Error errorObject;
		private Status statusObject;
		private bool hasErrorsValue;

		private Logger logger;

		public ServiceResponse()
		{
			hasErrorsValue = false;
			//
			// TODO: Add constructor logic here
			//
		}

		public ServiceResponse(XmlElement serviceResponseElement, SmartDatabase smartDatabase, Logger logger)
		{
			this.logger = logger;
			hasErrorsValue = false;
			fromDOM(serviceResponseElement, smartDatabase);

		}

		public Publication publication
		{
			get
			{
				return publicationObject;
			}
		}

		public Error error
		{
			get
			{
				return errorObject;
			}
		}

		public Status status
		{
			get
			{
				return statusObject;
			}
		}

		public bool hasErrors
		{
			get
			{
				return hasErrorsValue;
			}
		}

		public void fromDOM(XmlElement serviceResponseElement, SmartDatabase smartDatabase)
		{

			XmlNodeList publications = serviceResponseElement.GetElementsByTagName("PUBLICATION");
			if (publications.Count > 0)
			{
				publicationObject = new Publication((XmlElement)publications.Item(0), smartDatabase, logger);
			}

			XmlNodeList errors = serviceResponseElement.GetElementsByTagName("ERROR");
			if (errors.Count > 0)
			{
				errorObject = new Error((XmlElement)errors.Item(0));
				hasErrorsValue = true;
			}

			XmlNodeList statuses = serviceResponseElement.GetElementsByTagName("STATUS");
			if (statuses.Count > 0)
			{
				statusObject = new Status((XmlElement)statuses.Item(0), smartDatabase, logger);
			}

		}

	}
}
