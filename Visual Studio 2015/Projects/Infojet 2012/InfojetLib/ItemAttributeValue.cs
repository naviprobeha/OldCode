using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class ItemAttributeValue
	{
		private Database database;

		public string itemNo;
		public string itemAttributeCode;
		public string attributeValue;
		public string languageCode;

		public ItemAttributeValue(Database database, string itemNo, string itemAttributeCode, string languageCode)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
				 
			this.itemNo = itemNo;
			this.itemAttributeCode = itemAttributeCode;
			this.languageCode = languageCode;
			
			getFromDatabase();
		}

		public ItemAttributeValue(Database database, DataRow dataRow)
		{
			this.database = database;

			this.itemNo = dataRow.ItemArray.GetValue(0).ToString();
			this.itemAttributeCode = dataRow.ItemArray.GetValue(1).ToString();
			this.attributeValue = dataRow.ItemArray.GetValue(2).ToString();
			this.languageCode = dataRow.ItemArray.GetValue(3).ToString();

		}

		private void getFromDatabase()
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT [Item No_], [Item Attribute Code], [Value], [Language Code] FROM [" + database.getTableName("Web Item Attribute Value") + "] WHERE [Item No_] = @itemNo AND [Item Attribute Code] = @itemAttributeCode AND [Language Code] = @languageCode");
            databaseQuery.addStringParameter("itemNo", itemNo, 20);
            databaseQuery.addStringParameter("itemAttributeCode", itemAttributeCode, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
			if (dataReader.Read())
			{

				itemNo = dataReader.GetValue(0).ToString();
				itemAttributeCode = dataReader.GetValue(1).ToString();
				attributeValue = dataReader.GetValue(2).ToString();
				languageCode = dataReader.GetValue(3).ToString();

			}

			dataReader.Close();


		}

	}
}
