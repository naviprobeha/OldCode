using System;
using System.Xml;

namespace Navipro.SmartInventory
{
	/// <summary>
	/// Summary description for Error.
	/// </summary>
	public class Status
	{
		private string _code;
		private string _description;


		public Status(XmlElement errorElement)
		{
			//
			// TODO: Add constructor logic here
			//
			fromDOM(errorElement);
		}

		public string code { get { return _code; } }
        public string description { get { return _description; } }


		public void fromDOM(XmlElement statusElement)
		{
            _code = statusElement.GetAttribute("code");
            if (statusElement.HasChildNodes)
            {
                _description = statusElement.FirstChild.Value;
            }

		}
	}
}
