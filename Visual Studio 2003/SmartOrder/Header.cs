using System;
using System.Xml;

namespace SmartOrder
{
	/// <summary>
	/// Summary description for Header.
	/// </summary>
	public class Header
	{
		private string senderValue;
		private string receiverValue;
		
		public Header()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public Header(XmlElement headerElement)
		{
			fromDOM(headerElement);
		}

		public Header(string sender, string receiver)
		{
			this.senderValue = sender;
			this.receiverValue = receiver;
		}

		public string sender
		{
			get
			{
				return senderValue;
			}
			set
			{
				senderValue = value;
			}
		}

		public string receiver
		{
			get
			{
				return receiverValue;
			}
			set
			{
				receiverValue = value;
			}
		}

		public void fromDOM(XmlElement headerElement)
		{
			XmlNodeList senders = headerElement.GetElementsByTagName("SENDER");
			if (senders.Count > 0)
			{
				senderValue = senders.Item(0).FirstChild.Value;
			}

			XmlNodeList receivers = headerElement.GetElementsByTagName("RECEIVER");
			if (receivers.Count > 0)
			{
				receiverValue = receivers.Item(0).FirstChild.Value;
			}
		}

		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			XmlElement headerElement = xmlDocumentContext.CreateElement("HEADER");
			XmlElement senderElement = xmlDocumentContext.CreateElement("SENDER");
			XmlElement receiverElement = xmlDocumentContext.CreateElement("RECEIVER");
			senderElement.AppendChild(xmlDocumentContext.CreateTextNode(sender));
			receiverElement.AppendChild(xmlDocumentContext.CreateTextNode(receiver));
			headerElement.AppendChild(senderElement);
			headerElement.AppendChild(receiverElement);

			return headerElement;
		}
	}
}
