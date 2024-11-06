using System;
using System.Xml;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for ServiceResponse.
	/// </summary>
	public class ServiceResponse
	{
		private string _status;
		private string _orderNo;
        private int _versionNo;
        private string _errorMessage;
        private bool _containsErrors;

		public string xml;

		public ServiceResponse(XmlDocument xmlDoc)
		{
			//
			// TODO: Add constructor logic here
			//

			xml = xmlDoc.OuterXml;

			XmlElement documentElement = xmlDoc.DocumentElement;

			XmlElement statusElement = (XmlElement)documentElement.SelectSingleNode("serviceResponse/status");
			if (statusElement != null)
			{
				_status = statusElement.FirstChild.Value;
			}

            XmlElement errorMessageElement = (XmlElement)documentElement.SelectSingleNode("serviceResponse/errorMessage");
            if (errorMessageElement != null)
            {
                _errorMessage = errorMessageElement.FirstChild.Value;
                _containsErrors = true;
            }

			XmlElement orderNoElement = (XmlElement)documentElement.SelectSingleNode("serviceResponse/orderNo");
			if (orderNoElement != null)
			{
				_orderNo = orderNoElement.FirstChild.Value;
			}

            XmlElement versionNoElement = (XmlElement)documentElement.SelectSingleNode("serviceResponse/versionNo");
            if (versionNoElement != null)
            {
                _versionNo = int.Parse(versionNoElement.FirstChild.Value);
            }


		}

		public string status
		{
			get
			{
				return _status;
			}
		}

		public string orderNo
		{
			get
			{
				return _orderNo;
			}
		}

        public int versionNo
        {
            get
            {
                return _versionNo;
            }
        }

        public string errorMessage
        {
            get
            {
                return _errorMessage;
            }
        }

        public bool containsErrors
        {
            get
            {
                return _containsErrors;
            }
        }

	}
}
