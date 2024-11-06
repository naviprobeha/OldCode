using System;
using System.Xml;

namespace Navipro.SmartInventory
{
	/// <summary>
	/// Summary description for ServiceResponse.
	/// </summary>
	public class ServiceResponse
	{
		private Status _status;
		private bool _hasErrors;
        private XmlElement _responseDocument;

		private Logger logger;

		public ServiceResponse()
		{
            _hasErrors = false;
			//
			// TODO: Add constructor logic here
			//
		}

		public ServiceResponse(XmlElement serviceResponseElement, SmartDatabase smartDatabase, Logger logger)
		{
			this.logger = logger;
            _hasErrors = false;
			fromDOM(serviceResponseElement, smartDatabase);
		}

        public Status status { get { return _status; } }
		public bool hasErrors {	get	{ return _hasErrors; } }
        public XmlElement responseDocument { get { return _responseDocument; } }

		public void fromDOM(XmlElement serviceResponseElement, SmartDatabase smartDatabase)
		{
            _responseDocument = serviceResponseElement;

			XmlNodeList errors = serviceResponseElement.GetElementsByTagName("status");
			if (errors.Count > 0)
			{
                _status = new Status((XmlElement)errors.Item(0));
                _hasErrors = true;
			}
		}

	}
}
