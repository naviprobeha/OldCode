using System;
using System.Xml;

namespace SmartShipment
{
	/// <summary>
	/// Summary description for DataReference.
	/// </summary>
	public class DataReference : ServiceArgument
	{
		private string noValue;
		private string customerNoValue;
		private string itemNoValue;
		private string descriptionValue;
		private string hangingValue;
		private float unitPriceValue;
		private float discountValue;
		private string baseUnitValue;

		public DataReference(string referenceValue)
		{
			this.noValue = referenceValue;
			//
			// TODO: Add constructor logic here
			//
		}

		public DataReference(XmlElement itemElement)
		{
			fromDOM(itemElement);				
		}

		public string customerNo
		{
			set
			{
				customerNoValue = value;
			}
			get
			{
				return customerNoValue;
			}
		}


		public string no
		{
			get
			{
				return noValue;
			}
		}

		public string itemNo
		{
			get
			{
				return itemNoValue;
			}
		}

		public string description
		{
			get
			{
				return descriptionValue;
			}
		}

		public string hanging
		{
			get
			{
				return hangingValue;
			}
		}

		public string baseUnit
		{
			get
			{
				return baseUnitValue;
			}
		}

		public float unitPrice
		{
			get
			{
				return unitPriceValue;
			}
		}

		public float discount
		{
			get
			{
				return discountValue;
			}
		}

		#region ServiceArgument Members

		public System.Xml.XmlElement toDOM(System.Xml.XmlDocument xmlDocumentContext)
		{
			// TODO:  Add DataReference.toDOM implementation
			XmlElement element = xmlDocumentContext.CreateElement("REFERENCE");
			
			XmlElement noElement = xmlDocumentContext.CreateElement("NO");
			noElement.AppendChild(xmlDocumentContext.CreateTextNode(noValue));

			XmlElement customerNoElement = xmlDocumentContext.CreateElement("CUSTOMER_NO");
			customerNoElement.AppendChild(xmlDocumentContext.CreateTextNode(customerNoValue));

			element.AppendChild(noElement);
			element.AppendChild(customerNoElement);
			
			return element;

		}

		#endregion


		public void fromDOM(XmlElement element)
		{
			XmlNodeList referenceNoList = element.GetElementsByTagName("NO");
			if (referenceNoList.Count > 0)
			{
				XmlElement valueElement = (XmlElement)referenceNoList.Item(0);
				if (valueElement.FirstChild != null)
					this.noValue = valueElement.FirstChild.Value;
			}

			XmlNodeList itemNoList = element.GetElementsByTagName("ITEM_NO");
			if (itemNoList.Count > 0)
			{
				XmlElement valueElement = (XmlElement)itemNoList.Item(0);
				if (valueElement.FirstChild != null)
					this.itemNoValue = valueElement.FirstChild.Value;
			}

			XmlNodeList descriptionList = element.GetElementsByTagName("DESCRIPTION");
			if (descriptionList.Count > 0)
			{
				XmlElement valueElement = (XmlElement)descriptionList.Item(0);
				if (valueElement.FirstChild != null)
					this.descriptionValue = valueElement.FirstChild.Value;
			}

			XmlNodeList hangingList = element.GetElementsByTagName("HANGING");
			if (hangingList.Count > 0)
			{
				XmlElement valueElement = (XmlElement)hangingList.Item(0);
				if (valueElement.FirstChild != null)
					this.hangingValue = valueElement.FirstChild.Value;
			}

			XmlNodeList baseUnitList = element.GetElementsByTagName("BASE_UNIT");
			if (baseUnitList.Count > 0)
			{
				XmlElement valueElement = (XmlElement)baseUnitList.Item(0);
				if (valueElement.FirstChild != null)
					this.baseUnitValue = valueElement.FirstChild.Value;
			}

			XmlNodeList unitPriceList = element.GetElementsByTagName("UNIT_PRICE");
			if (unitPriceList.Count > 0)
			{
				XmlElement valueElement = (XmlElement)unitPriceList.Item(0);
				if (valueElement.FirstChild != null)
					this.unitPriceValue = float.Parse(valueElement.FirstChild.Value);
			}


			XmlNodeList discountList = element.GetElementsByTagName("DISCOUNT");
			if (discountList.Count > 0)
			{
				XmlElement valueElement = (XmlElement)discountList.Item(0);
				if (valueElement.FirstChild != null)
					this.discountValue = float.Parse(valueElement.FirstChild.Value);
			}
			
		}

	}
}
