using System;
using System.Xml;

namespace SmartInventory
{
	/// <summary>
	/// Summary description for DataBinContent.
	/// </summary>
	public class DataBinContent : ServiceArgument
	{
		private string locationCodeValue;
		private string binCodeValue;
		private string itemNoValue;
		private string descriptionValue;
		private float quantityValue;
		private string handleUnitValue;
		private string statusValue;

		public DataBinContent()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataBinContent(XmlElement element, SmartDatabase smartDatabase)
		{
			//
			// TODO: Add constructor logic here
			//
			fromDOM(element);
		}


		public string locationCode
		{
			get
			{
				return locationCodeValue;
			}
			set
			{
				locationCodeValue = value;
			}
		}

		public string binCode
		{
			get
			{
				return binCodeValue;
			}
			set
			{
				binCodeValue = value;
			}
		}

		public string itemNo
		{
			get
			{
				return itemNoValue;
			}
			set
			{
				itemNoValue = value;
			}
		}

		public string handleUnit
		{
			get
			{
				return handleUnitValue;
			}
			set
			{
				handleUnitValue = value;
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

		public float quantity
		{
			get
			{
				return quantityValue;
			}
			set
			{
				quantityValue = value;
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

		public void fromDOM(XmlElement element)
		{
		
			XmlAttribute location = element.GetAttributeNode("LOCATION_CODE");
			this.locationCode = location.Value;

			XmlAttribute bin = element.GetAttributeNode("BIN_CODE");
			this.binCode = bin.Value;

			XmlAttribute item = element.GetAttributeNode("ITEM_NO");
			this.itemNo = item.Value;

			XmlAttribute description = element.GetAttributeNode("DESCRIPTION");
			this.description = description.Value;

			XmlAttribute handleUnit = element.GetAttributeNode("HANDLE_UNIT");
			this.handleUnit = handleUnit.Value;

			XmlAttribute status = element.GetAttributeNode("STATUS");
			this.status = status.Value;

			if (element.FirstChild != null)
				this.quantity = float.Parse(element.FirstChild.Value);
		
		}
		#region ServiceArgument Members

		public XmlElement toDOM(XmlDocument xmlDocumentContext)
		{
			// TODO:  Add DataBinContent.toDOM implementation
			XmlElement binContentElement = xmlDocumentContext.CreateElement("BIN_CONTENT");
			binContentElement.SetAttribute("BIN_CODE", this.binCode);
			binContentElement.SetAttribute("LOCATION_CODE", this.locationCode);
			binContentElement.SetAttribute("DESCRIPTION", this.description);
			binContentElement.SetAttribute("HANDLE_UNIT", this.handleUnit);
			binContentElement.SetAttribute("ITEM_NO", this.itemNo);
			
			binContentElement.AppendChild(xmlDocumentContext.CreateTextNode(quantity.ToString()));

			return binContentElement;
		}

		public void postDOM()
		{
			// TODO:  Add DataBinContent.postDOM implementation
		}

		#endregion
	}
}
