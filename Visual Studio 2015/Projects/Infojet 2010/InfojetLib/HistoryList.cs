using System;
using System.Xml;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for HistoryList.
	/// </summary>
	public class HistoryList : ServiceArgument
	{
		public string customerNo;
		public int pageNo;

		public HistoryList(string customerNo, int pageNo)
		{
			//
			// TODO: Add constructor logic here
			//
			this.customerNo = customerNo;
			this.pageNo = pageNo;
		}
		#region ServiceArgument Members

		public System.Xml.XmlElement toDOM(System.Xml.XmlDocument xmlDoc)
		{
			// TODO:  Add HistoryList.toDOM implementation
			XmlElement xmlDocElement = xmlDoc.CreateElement("list");

			XmlAttribute customerNoAttribute = xmlDoc.CreateAttribute("customerNo");
			customerNoAttribute.Value = customerNo;
			xmlDocElement.Attributes.Append(customerNoAttribute);

			XmlAttribute pageNoAttribute = xmlDoc.CreateAttribute("pageNo");
			pageNoAttribute.Value = pageNo.ToString();
			xmlDocElement.Attributes.Append(pageNoAttribute);

			return xmlDocElement;


		}

		#endregion
	}
}
