using System;
using System.Xml;

namespace SmartShipment
{
	/// <summary>
	/// Summary description for Error.
	/// </summary>
	public class Error
	{
		private string codeValue;
		private string statusValue;
		private string descriptionValue;


		public Error(XmlElement errorElement)
		{
			//
			// TODO: Add constructor logic here
			//
			fromDOM(errorElement);
		}

		public string code
		{
			get
			{
				return codeValue;
			}
			set
			{
				codeValue = value;
			}
		}

		public string status
		{
			get
			{
				return statusValue;
			}
			set
			{
				statusValue = value;
			}
		}


		public string description
		{
			get
			{
				return descriptionValue;
			}
			set
			{
				descriptionValue = value;
			}
		}


		public void fromDOM(XmlElement errorElement)
		{
			XmlNodeList codes = errorElement.GetElementsByTagName("CODE");
			if (codes.Count > 0)
			{
				codeValue = codes.Item(0).FirstChild.Value;
			}

			XmlNodeList statuses = errorElement.GetElementsByTagName("STATUS");
			if (statuses.Count > 0)
			{
				statusValue = statuses.Item(0).FirstChild.Value;
			}

			XmlNodeList descriptions = errorElement.GetElementsByTagName("DESCRIPTION");
			if (descriptions.Count > 0)
			{
				descriptionValue = descriptions.Item(0).FirstChild.Value;
			}

		}
	}
}
