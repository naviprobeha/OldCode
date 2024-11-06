using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebItemCampainMembers.
	/// </summary>
	public class ItemAttributeVisibilities
	{
		private Database database;

		public ItemAttributeVisibilities(Database database)
		{
			//
			// TODO: Add constructor logic here
			//
			this.database = database;
		}

		public DataSet getItemAttributes(string webSiteCode, string webItemListInfoCode)
		{
            DatabaseQuery databaseQuery = database.prepare("SELECT v.[Web Site Code], v.[Item Attribute Code], v.[Web Item List_Info Code], v.[Web Text Constant Code], v.[Visible] FROM [" + database.getTableName("Item Attribute Visibility") + "] v, [" + database.getTableName("Item Attribute") + "] a WHERE v.[Web Site Code] = @webSiteCode AND v.[Web Item List_Info Code] = @webItemListInfoCode AND v.[Visible] = 1 AND v.[Item Attribute Code] = a.[Code] ORDER BY a.[Sort Order]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("webItemListInfoCode", webItemListInfoCode, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);

		}

        public DataSet getFilteredItemAttributes(string webSiteCode, string webItemListInfoCode)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT v.[Web Site Code], v.[Item Attribute Code], v.[Web Item List_Info Code], v.[Web Text Constant Code], v.[Visible] FROM [" + database.getTableName("Item Attribute Visibility") + "] v, [" + database.getTableName("Item Attribute") + "] a WHERE v.[Web Site Code] = @webSiteCode AND v.[Web Item List_Info Code] = @webItemListInfoCode AND v.[Filtered] = 1 AND v.[Item Attribute Code] = a.[Code] ORDER BY a.[Sort Order]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("webItemListInfoCode", webItemListInfoCode, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return (dataSet);

        }

        public DataSet getExistingItemAttributeValues(string itemAttributeCode, string languageCode)
        {
            DatabaseQuery databaseQuery = database.prepare("SELECT DISTINCT [Value] FROM [" + database.getTableName("Item Attribute Value") + "] WHERE [Item Attribute Code] = @itemAttributeCode AND [Language Code] = @languageCode ORDER BY [Value]");
            databaseQuery.addStringParameter("itemAttributeCode", itemAttributeCode, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return (dataSet);

        }

		public static ItemAttributeCollection getItemAttributes(Infojet infojetContext, string webItemListInfoCode, string itemNo)
		{
            ItemAttributeCollection itemAttributeCollection = new ItemAttributeCollection();

			ItemAttributeVisibilities itemAttributeVisibilities = new ItemAttributeVisibilities(infojetContext.systemDatabase);
			DataSet dataSet = itemAttributeVisibilities.getItemAttributes(infojetContext.webSite.code, webItemListInfoCode);
			int i = 0;
			while (i < dataSet.Tables[0].Rows.Count)
			{
				ItemAttributeVisibility itemAttributeVisibility = new ItemAttributeVisibility(infojetContext.systemDatabase, dataSet.Tables[0].Rows[i]);
                itemAttributeCollection.Add(itemAttributeVisibility.getItemAttribute(infojetContext, itemNo));

				i++;
			}

            return itemAttributeCollection;
		}

        public static ItemAttributeCollection getFilteredItemAttributes(Infojet infojetContext, string webItemListInfoCode)
        {
            ItemAttributeCollection itemAttributeCollection = new ItemAttributeCollection();

            ItemAttributeVisibilities itemAttributeVisibilities = new ItemAttributeVisibilities(infojetContext.systemDatabase);
            DataSet dataSet = itemAttributeVisibilities.getFilteredItemAttributes(infojetContext.webSite.code, webItemListInfoCode);
            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                ItemAttributeVisibility itemAttributeVisibility = new ItemAttributeVisibility(infojetContext.systemDatabase, dataSet.Tables[0].Rows[i]);
                ItemAttribute itemAttribute = itemAttributeVisibility.getItemAttribute(infojetContext, "");
                itemAttribute.itemAttributeValueCollection = ItemAttributeVisibilities.getItemAttributeFilterValues(infojetContext, itemAttribute);
                itemAttributeCollection.Add(itemAttribute);

                i++;
            }

            return itemAttributeCollection;
        }

        public static ItemAttributeCollection getItemAttributeFilterValues(Infojet infojetContext, ItemAttribute itemAttribute)
        {
            ItemAttributeCollection itemAttributeValueCollection = new ItemAttributeCollection();

            ItemAttribute itemAttributeDefaultValue = new ItemAttribute(itemAttribute, itemAttribute.code+"|");
            itemAttributeDefaultValue.text = infojetContext.translate("NOT CHOOSEN");
            itemAttributeValueCollection.Add(itemAttributeDefaultValue);

            ItemAttributeVisibilities itemAttributeVisibilities = new ItemAttributeVisibilities(infojetContext.systemDatabase);
            DataSet dataSet = itemAttributeVisibilities.getExistingItemAttributeValues(itemAttribute.code, infojetContext.languageCode);
            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                ItemAttribute itemAttributeValue = new ItemAttribute(itemAttribute, "");
                itemAttributeValue.text = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                itemAttributeValue.itemValue = itemAttributeValue.code + "|" + dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();


                itemAttributeValueCollection.Add(itemAttributeValue);

                i++;
            }

            return itemAttributeValueCollection;


        }
	}
}
