using System;
using System.Xml;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for SalesDocument.
	/// </summary>
	public class HistoryDocument : ServiceArgument
	{
		private int type;
		private string no;
		private string customerNo;
        private string webUserAccountNo;

		public HistoryDocument(int type, string no, WebUserAccount webUserAccount)
		{
			//
			// TODO: Add constructor logic here
			//
			this.type = type;
			this.no = no;
			this.customerNo = webUserAccount.customerNo;
            this.webUserAccountNo = webUserAccount.no;
		}

 		#region ServiceArgument Members

		public System.Xml.XmlElement toDOM(System.Xml.XmlDocument xmlDoc)
		{
			// TODO:  Add SalesDocument.toDOM implementation

			XmlElement xmlDocElement = xmlDoc.CreateElement("document");

			XmlAttribute customerNoAttribute = xmlDoc.CreateAttribute("customerNo");
			customerNoAttribute.Value = customerNo;
			xmlDocElement.Attributes.Append(customerNoAttribute);

            XmlAttribute webUserAccountNoAttribute = xmlDoc.CreateAttribute("webUserAccountNo");
            webUserAccountNoAttribute.Value = webUserAccountNo;
            xmlDocElement.Attributes.Append(webUserAccountNoAttribute);

			if (type == 0)
			{
				XmlAttribute noAttribute = xmlDoc.CreateAttribute("orderNo");
				noAttribute.Value = no;
				xmlDocElement.Attributes.Append(noAttribute);
			}

			if (type == 1)
			{
				XmlAttribute noAttribute = xmlDoc.CreateAttribute("shipmentNo");
				noAttribute.Value = no;
				xmlDocElement.Attributes.Append(noAttribute);
			}

			if (type == 2)
			{
				XmlAttribute noAttribute = xmlDoc.CreateAttribute("invoiceNo");
				noAttribute.Value = no;
				xmlDocElement.Attributes.Append(noAttribute);
			}

            if (type == 3)
            {
                XmlAttribute noAttribute = xmlDoc.CreateAttribute("crMemoNo");
                noAttribute.Value = no;
                xmlDocElement.Attributes.Append(noAttribute);
            }


            XmlAttribute docTypeAttribute = xmlDoc.CreateAttribute("documentType");
            docTypeAttribute.Value = type.ToString();
            xmlDocElement.Attributes.Append(docTypeAttribute);

            XmlAttribute docNoAttribute = xmlDoc.CreateAttribute("documentNo");
            docNoAttribute.Value = no;
            xmlDocElement.Attributes.Append(docNoAttribute);


			return xmlDocElement;
		}

		#endregion
	}
}
