using System;
using System.Xml;
using System.Data;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for ItemListServiceArgument.
	/// </summary>
	public class ItemListServiceArgument : ServiceArgument
	{
		private DataSet dataSet;

		private string locationCode;
		private string customerNo;
		private string currencyCode;
		
		private int infoMode;

		public ItemListServiceArgument()	
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public void setItemDataSet(DataSet dataSet)
		{
			this.dataSet = dataSet;
			this.infoMode = 1;
		}

		public void setCartDataSet(DataSet dataSet)
		{
			this.dataSet = dataSet;
			this.infoMode = 2;
		}

		public void setLocationCode(string locationCode)
		{
			this.locationCode = locationCode;
		}

		public void setCustomerNo(string customerNo)
		{
			this.customerNo = customerNo;
		}

		public void setCurrencyCode(string currencyCode)
		{
			this.currencyCode = currencyCode;
		}

		#region ServiceArgument Members

		public System.Xml.XmlElement toDOM(System.Xml.XmlDocument xmlDoc)
		{
			// TODO:  Add ItemListServiceArgument.toDOM implementation
			XmlElement itemsElement = xmlDoc.CreateElement("items");


			if (locationCode != "") 
			{
				XmlAttribute locationAttribute = xmlDoc.CreateAttribute("locationCode");
				locationAttribute.Value = locationCode;

				itemsElement.Attributes.Append(locationAttribute);
			}

			if (customerNo != "") 
			{
				XmlAttribute customerNoAttribute = xmlDoc.CreateAttribute("customerNo");
				customerNoAttribute.Value = customerNo;

				itemsElement.Attributes.Append(customerNoAttribute);
			}

			if (currencyCode != "") 
			{
				XmlAttribute currencyAttribute = xmlDoc.CreateAttribute("currencyCode");
				currencyAttribute.Value = currencyCode;

				itemsElement.Attributes.Append(currencyAttribute);
			}
           
			if (infoMode == 1)
			{
				int i = 0;
				while (i < dataSet.Tables[0].Rows.Count)
				{
					XmlElement itemElement = xmlDoc.CreateElement("item");
					XmlElement itemNoElement = xmlDoc.CreateElement("no");

					XmlText itemNoText = xmlDoc.CreateTextNode(dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());

					itemNoElement.AppendChild(itemNoText);
					itemElement.AppendChild(itemNoElement);

					XmlAttribute quantityAttribute = xmlDoc.CreateAttribute("quantity");
					quantityAttribute.Value = "1";
					itemElement.Attributes.Append(quantityAttribute);

					itemsElement.AppendChild(itemElement);
                
					i++;
				}
			}

			if (infoMode == 2)
			{
				int i = 0;
				while (i < dataSet.Tables[0].Rows.Count)
				{
					XmlElement itemElement = xmlDoc.CreateElement("item");
					XmlElement itemNoElement = xmlDoc.CreateElement("no");

					XmlText itemNoText = xmlDoc.CreateTextNode(dataSet.Tables[0].Rows[i].ItemArray.GetValue(2).ToString());

					itemNoElement.AppendChild(itemNoText);
					itemElement.AppendChild(itemNoElement);

					XmlAttribute quantityAttribute = xmlDoc.CreateAttribute("quantity");
					quantityAttribute.Value = dataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString();
					itemElement.Attributes.Append(quantityAttribute);

					itemsElement.AppendChild(itemElement);
                
					i++;
				}

			}
			return itemsElement;
		}

		#endregion
	}
}
