using System;
using System.Xml;
using System.Data;
using System.Collections;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for InventoryItemList.
	/// </summary>
	public class InventoryItemList : ServiceArgument
	{
		private string locationCode;
		private DataSet dataSet;

		public InventoryItemList()
		{
			//
			// TODO: Add constructor logic here
			//

		}

		public void setItemList(DataSet itemListDataSet, string locationCode)
		{
			this.dataSet = itemListDataSet;
			this.locationCode = locationCode;
		}



		#region ServiceArgument Members

		public XmlElement toDOM(XmlDocument xmlDoc)
		{
			// TODO:  Add InventoryItemList.toDOM implementation
			
			XmlElement inventoryElement = xmlDoc.CreateElement("inventory");

			XmlElement locationElement = xmlDoc.CreateElement("locationCode");
			XmlText locationText = xmlDoc.CreateTextNode(locationCode);
			locationElement.AppendChild(locationText);
			inventoryElement.AppendChild(locationElement);

			XmlElement itemsElement = xmlDoc.CreateElement("items");
            
			int i = 0;
			while (i < dataSet.Tables[0].Rows.Count)
			{
				XmlElement itemElement = xmlDoc.CreateElement("item");
				XmlElement itemNoElement = xmlDoc.CreateElement("itemNo");

				XmlText itemNoText = xmlDoc.CreateTextNode(dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString());

				itemNoElement.AppendChild(itemNoText);
				itemElement.AppendChild(itemNoElement);
				itemsElement.AppendChild(itemElement);
                
				i++;
			}

			inventoryElement.AppendChild(itemsElement);

			return inventoryElement;

		}


		#endregion
	}
}
