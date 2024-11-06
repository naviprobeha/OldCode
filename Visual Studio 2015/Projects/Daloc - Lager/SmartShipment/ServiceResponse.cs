using System;
using System.Xml;

namespace SmartShipment
{
	/// <summary>
	/// Summary description for ServiceResponse.
	/// </summary>
	public class ServiceResponse
	{
		private Publication publicationObject;
		private CreditCheck creditData;
		private ItemPrice itemPriceData;
		private DataReference dataReference;
		private Error errorObject;
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

		public CreditCheck creditCheck
		{
			get
			{
				return creditData;
			}
		}

		public ItemPrice itemPrice
		{
			get
			{
				return itemPriceData;
			}
		}

		public DataReference reference
		{
			get
			{
				return dataReference;
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


			XmlNodeList creditCheck = serviceResponseElement.GetElementsByTagName("CREDITCHECK");
			if (creditCheck.Count > 0)
			{
				creditData = new CreditCheck((XmlElement)creditCheck.Item(0), smartDatabase, logger);
			}

			XmlNodeList itemPriceList = serviceResponseElement.GetElementsByTagName("ITEMPRICE");
			if (itemPriceList.Count > 0)
			{
				itemPriceData = new ItemPrice((XmlElement)itemPriceList.Item(0), smartDatabase, logger);
			}

			XmlNodeList referenceList = serviceResponseElement.GetElementsByTagName("REFERENCE");
			if (referenceList.Count > 0)
			{
				dataReference = new DataReference((XmlElement)referenceList.Item(0));
			}

			XmlNodeList errors = serviceResponseElement.GetElementsByTagName("ERROR");
			if (errors.Count > 0)
			{
				errorObject = new Error((XmlElement)errors.Item(0));
				hasErrorsValue = true;
			}
		}

	}
}
