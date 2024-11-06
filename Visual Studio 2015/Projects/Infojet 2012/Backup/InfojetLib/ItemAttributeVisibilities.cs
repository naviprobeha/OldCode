using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
	/// <summary>
	/// Summary description for WebItemCampainMembers.
	/// </summary>
	public class ItemAttributeVisibilities
	{
		private Infojet infojetContext;

        public ItemAttributeVisibilities(Infojet infojetContext)
		{
			//
			// TODO: Add constructor logic here
			//
            this.infojetContext = infojetContext;
		}

		public DataSet getItemAttributes(string webSiteCode, string webItemListInfoCode)
		{
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT v.[Web Site Code], v.[Item Attribute Code], v.[Web Item List_Info Code], v.[Web Text Constant Code], v.[Visible] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Attribute Visibility") + "] v, [" + infojetContext.systemDatabase.getTableName("Web Item Attribute") + "] a WHERE v.[Web Site Code] = @webSiteCode AND v.[Web Item List_Info Code] = @webItemListInfoCode AND v.[Visible] = 1 AND v.[Item Attribute Code] = a.[Code] ORDER BY a.[Sort Order]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("webItemListInfoCode", webItemListInfoCode, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);

			return(dataSet);

		}

        public DataSet getItemAttributes(string webSiteCode, string webItemListInfoCode, string itemNo, string languageCode)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT v.[Web Site Code], v.[Item Attribute Code], v.[Web Item List_Info Code], v.[Web Text Constant Code], v.[Visible], t.[Value] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Attribute Visibility") + "] v, [" + infojetContext.systemDatabase.getTableName("Web Item Attribute") + "] a, [" + infojetContext.systemDatabase.getTableName("Web Item Attribute Value") + "] t WHERE v.[Web Site Code] = @webSiteCode AND v.[Web Item List_Info Code] = @webItemListInfoCode AND v.[Visible] = 1 AND v.[Item Attribute Code] = a.[Code] AND t.[Item Attribute Code] = a.[Code] AND t.[Item No_] = @itemNo AND t.[Language Code] = @languageCode ORDER BY a.[Sort Order]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("webItemListInfoCode", webItemListInfoCode, 20);
            databaseQuery.addStringParameter("itemNo", itemNo, 20);
            databaseQuery.addStringParameter("languageCode", languageCode, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return (dataSet);

        }


        public DataSet getFilteredItemAttributes(string webSiteCode, string webItemListInfoCode)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT v.[Web Site Code], v.[Item Attribute Code], v.[Web Item List_Info Code], v.[Web Text Constant Code], v.[Visible] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Attribute Visibility") + "] v, [" + infojetContext.systemDatabase.getTableName("Web Item Attribute") + "] a WHERE v.[Web Site Code] = @webSiteCode AND v.[Web Item List_Info Code] = @webItemListInfoCode AND v.[Filtered] = 1 AND v.[Item Attribute Code] = a.[Code] ORDER BY a.[Sort Order]");
            databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);
            databaseQuery.addStringParameter("webItemListInfoCode", webItemListInfoCode, 20);


            SqlDataAdapter sqlDataAdapter = databaseQuery.executeDataAdapterQuery();
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            return (dataSet);

        }

        public DataSet getExistingItemAttributeValues(string itemAttributeCode, string languageCode)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Item No_], [Value] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Attribute Value") + "] WHERE [Item Attribute Code] = @itemAttributeCode AND [Language Code] = @languageCode ORDER BY [Value]");
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

			ItemAttributeVisibilities itemAttributeVisibilities = new ItemAttributeVisibilities(infojetContext);
			DataSet dataSet = itemAttributeVisibilities.getItemAttributes(infojetContext.webSite.code, webItemListInfoCode, itemNo, infojetContext.languageCode);
			int i = 0;
			while (i < dataSet.Tables[0].Rows.Count)
			{
				ItemAttributeVisibility itemAttributeVisibility = new ItemAttributeVisibility(infojetContext.systemDatabase, dataSet.Tables[0].Rows[i]);
                itemAttributeCollection.Add(itemAttributeVisibility.getItemAttribute(infojetContext, itemNo));

				i++;
			}

            return itemAttributeCollection;
		}

        public static ItemAttributeCollection getFilteredItemAttributes(Infojet infojetContext, string webItemListInfoCode, DataSet itemDataSet)
        {
            ItemAttributeCollection itemAttributeCollection = new ItemAttributeCollection();

            ItemAttributeVisibilities itemAttributeVisibilities = new ItemAttributeVisibilities(infojetContext);
            DataSet dataSet = itemAttributeVisibilities.getFilteredItemAttributes(infojetContext.webSite.code, webItemListInfoCode);
            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {

                ItemAttributeVisibility itemAttributeVisibility = new ItemAttributeVisibility(infojetContext.systemDatabase, dataSet.Tables[0].Rows[i]);
                ItemAttribute itemAttribute = itemAttributeVisibility.getItemAttribute(infojetContext, "");
                itemAttribute.itemAttributeValueCollection = ItemAttributeVisibilities.getItemAttributeFilterValues(infojetContext, itemAttribute, itemDataSet);
                itemAttributeCollection.Add(itemAttribute);

                i++;
            }

            return itemAttributeCollection;
        }

        public static ItemAttributeCollection getItemAttributeFilterValues(Infojet infojetContext, ItemAttribute itemAttribute, DataSet itemDataSet)
        {
            ArrayList usedValues = new ArrayList();
            ArrayList itemList = new ArrayList();

            int j = 0;
            while (j < itemDataSet.Tables[0].Rows.Count)
            {
                itemList.Add(itemDataSet.Tables[0].Rows[j].ItemArray.GetValue(0).ToString());
                j++;
            }

            ItemAttributeCollection itemAttributeValueCollection = new ItemAttributeCollection();

            ItemAttribute itemAttributeDefaultValue = new ItemAttribute(itemAttribute, itemAttribute.code+"|");
            itemAttributeDefaultValue.text = infojetContext.translate("NOT CHOOSEN");
            itemAttributeValueCollection.Add(itemAttributeDefaultValue);

            ItemAttributeVisibilities itemAttributeVisibilities = new ItemAttributeVisibilities(infojetContext);
            DataSet valueDataSet = itemAttributeVisibilities.getExistingItemAttributeValues(itemAttribute.code, infojetContext.languageCode);
            int i = 0;
            while (i < valueDataSet.Tables[0].Rows.Count)
            {
                if (itemList.Contains(valueDataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString()))
                {
                    ItemAttribute itemAttributeValue = new ItemAttribute(itemAttribute, "");
                    itemAttributeValue.text = valueDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();
                    itemAttributeValue.itemValue = itemAttributeValue.code + "|" + valueDataSet.Tables[0].Rows[i].ItemArray.GetValue(1).ToString();

                    if (!itemAttributeValueCollection.Contains(itemAttributeValue.itemValue))
                    {
                        itemAttributeValueCollection.Add(itemAttributeValue);
                    }
                }

                i++;
            }

            return itemAttributeValueCollection;


        }

        public Hashtable getItemListAttributes(DataSet dataSet, string webSiteCode, string webItemListInfoCode, string languageCode)
        {
            Hashtable collectionTable = new Hashtable();

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                string whereQuery = "";

                int i = 0;
                while (i < dataSet.Tables[0].Rows.Count)
                {
                    string itemNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                    if (whereQuery != "")
                    {
                        whereQuery = whereQuery + " OR ";
                    }
                    whereQuery = whereQuery + "t.[Item No_] = '" + itemNo + "'";

                    i++;
                }



                DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT v.[Web Site Code], a.[Code], v.[Web Item List_Info Code], v.[Web Text Constant Code], v.[Visible], t.[Item No_], t.[Value] FROM [" + infojetContext.systemDatabase.getTableName("Web Item Attribute") + "] a, [" + infojetContext.systemDatabase.getTableName("Web Item Attribute Visibility") + "] v, [" + infojetContext.systemDatabase.getTableName("Web Item Attribute Value") + "] t WHERE (" + whereQuery + ") AND t.[Item Attribute Code] = a.[Code] AND a.[Code] = v.[Item Attribute Code] AND t.[Language Code] = @languageCode AND v.[Web Item List_Info Code] = @webItemListInfoCode AND v.[Web Site Code] = @webSiteCode AND [Visible] = '1' ORDER BY a.[Sort Order], a.[Code]");
                databaseQuery.addStringParameter("languageCode", languageCode, 20);
                databaseQuery.addStringParameter("webItemListInfoCode", webItemListInfoCode, 20);
                databaseQuery.addStringParameter("webSiteCode", webSiteCode, 20);


                SqlDataAdapter dataAdapter = databaseQuery.executeDataAdapterQuery();
                DataSet itemAttributeDataSet = new DataSet();
                dataAdapter.Fill(itemAttributeDataSet);

                i = 0;
                while (i < itemAttributeDataSet.Tables[0].Rows.Count)
                {
                    ItemAttributeVisibility itemAttributeVisibility = new ItemAttributeVisibility(infojetContext.systemDatabase, itemAttributeDataSet.Tables[0].Rows[i]);
                    ItemAttribute itemAttribute = new ItemAttribute(infojetContext, itemAttributeVisibility);

                    itemAttribute.itemValue = itemAttributeDataSet.Tables[0].Rows[i].ItemArray.GetValue(6).ToString();

                    string itemNo = itemAttributeDataSet.Tables[0].Rows[i].ItemArray.GetValue(5).ToString();
                    if (collectionTable[itemNo] == null)
                    {
                        collectionTable.Add(itemNo, new ItemAttributeCollection());
                    }
                    ((ItemAttributeCollection)collectionTable[itemNo]).Add(itemAttribute);

                    i++;
                }

            }

            return collectionTable;
        }
	}
}
