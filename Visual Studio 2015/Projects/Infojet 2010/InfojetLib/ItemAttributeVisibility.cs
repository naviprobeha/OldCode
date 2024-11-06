using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class ItemAttributeVisibility
	{
		private Database database;

		public string webSiteCode;
		public string itemAttributeCode;
		public string webItemListInfoCode;
		public string webTextConstantCode;
		public int visible;

		public ItemAttributeVisibility(Database database, string webSiteCode, string itemAttributeCode, string webItemListInfoCode)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				 
			this.webSiteCode = webSiteCode;
			this.itemAttributeCode = itemAttributeCode;
			this.webItemListInfoCode = webItemListInfoCode;

			getFromDatabase();
		}

		public ItemAttributeVisibility(Database database, DataRow dataRow)
		{
			this.database = database;

			this.webSiteCode = dataRow.ItemArray.GetValue(0).ToString();
			this.itemAttributeCode = dataRow.ItemArray.GetValue(1).ToString();
			this.webItemListInfoCode = dataRow.ItemArray.GetValue(2).ToString();
			this.webTextConstantCode = dataRow.ItemArray.GetValue(3).ToString();
			this.visible = int.Parse(dataRow.ItemArray.GetValue(4).ToString());

		}

		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [Web Site Code], [Item Attribute Code], [Web Item List_Info Code], [Web Text Constant Code], [Visible] FROM [" + database.getTableName("Item Attribute Visibility") + "] WHERE [Web Site Code] = @webSiteCode AND [Item Attribute Code] = @itemAttributeCode AND [Web Item List_Info Code] = @webItemListInfoCode");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("itemAttributeCode", itemAttributeCode, 20);
            databaseQuery.addStringParameter("webItemListInfoCode", webItemListInfoCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{

				webSiteCode = dataReader.GetValue(0).ToString();
				itemAttributeCode = dataReader.GetValue(1).ToString();
				webItemListInfoCode = dataReader.GetValue(2).ToString();
				webTextConstantCode = dataReader.GetValue(3).ToString();
				visible = dataReader.GetInt32(4);

			}

			dataReader.Close();


		}

		public string getValue(string itemNo, string languageCode)
		{
			ItemAttributeValue itemAttributeValue = new ItemAttributeValue(database, itemNo, this.itemAttributeCode, languageCode);
			return itemAttributeValue.attributeValue;
		}

        public ItemAttribute getItemAttribute(Infojet infojetContext, string itemNo)
        {
            ItemAttribute itemAttribute = new ItemAttribute(infojetContext, this);

            itemAttribute.itemValue = getValue(itemNo, infojetContext.languageCode);

            return itemAttribute;
        }
	}
}
