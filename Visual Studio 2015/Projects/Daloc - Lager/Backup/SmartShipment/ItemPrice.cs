using System;
using System.Xml;

namespace SmartShipment
{
	/// <summary>
	/// Summary description for ItemPrice.
	/// </summary>
	public class ItemPrice : ServiceArgument
	{
		private Logger logger;
		private SmartDatabase smartDatabase;
		private DataCustomer dataCustomer;
		private DataItem dataItem;
		private XmlElement xmlItemPriceElement;
		private float unitPriceValue;
		private float discountValue;

		public ItemPrice(SmartDatabase smartDatabase, DataCustomer dataCustomer, DataItem dataItem)
		{
			this.smartDatabase = smartDatabase;
			this.dataCustomer = dataCustomer;
			this.dataItem = dataItem;
		}

		public ItemPrice(SmartDatabase smartDatabase, Logger logger)
		{
			this.smartDatabase = smartDatabase;
			this.logger = logger;
			//
			// TODO: Add constructor logic here
			//
		}

		public ItemPrice(XmlElement itemPriceElement, SmartDatabase smartDatabase, Logger logger)
		{
			this.logger = logger;
			fromDOM(itemPriceElement, smartDatabase);
		}

		public DataItem item
		{
			get
			{
				return dataItem;
			}
		}

		public float unitPrice
		{
			get
			{
				return unitPriceValue;
			}
		}

		public XmlElement xmlItemPrice
		{
			get
			{
				return xmlItemPriceElement;
			}
		}

		public float discount
		{
			get
			{
				return discountValue;
			}
		}

		public void fromDOM(XmlElement xmlItemPriceElement, SmartDatabase smartDatabase)
		{
			this.xmlItemPriceElement = xmlItemPriceElement;

			XmlNodeList unitPrice = xmlItemPriceElement.GetElementsByTagName("UNITPRICE");
			if (unitPrice.Count > 0)
			{
				XmlText unitPriceText = (XmlText)unitPrice.Item(0).FirstChild;
				this.unitPriceValue = float.Parse(unitPriceText.Value);
			}

		}
		#region ServiceArgument Members

		public System.Xml.XmlElement toDOM(System.Xml.XmlDocument xmlDocumentContext)
		{
			// TODO:  Add ItemPrice.toDOM implementation
			XmlElement topElement = xmlDocumentContext.CreateElement("ITEMPRICE");
			
			XmlElement customerElement = xmlDocumentContext.CreateElement("CUSTOMER");
			XmlElement customerNoElement = xmlDocumentContext.CreateElement("NO");
			customerNoElement.AppendChild(xmlDocumentContext.CreateTextNode(dataCustomer.no));
			customerElement.AppendChild(customerNoElement);

			XmlElement itemElement = xmlDocumentContext.CreateElement("ITEM");
			XmlElement itemNoElement = xmlDocumentContext.CreateElement("NO");
			itemNoElement.AppendChild(xmlDocumentContext.CreateTextNode(dataItem.no));
			itemElement.AppendChild(itemNoElement);

			topElement.AppendChild(customerElement);
			topElement.AppendChild(itemElement);
			
			return topElement;
		}

		#endregion
	}
}
