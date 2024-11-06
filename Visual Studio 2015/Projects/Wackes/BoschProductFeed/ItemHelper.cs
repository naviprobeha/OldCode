using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoschProductFeed
{
    public class ItemHelper
    {
        private Configuration configuration;
        private System.Data.SqlClient.SqlConnection sqlConnection;

        public ItemHelper(Configuration configuration)
        {
            this.configuration = configuration;

            sqlConnection = new System.Data.SqlClient.SqlConnection(configuration.connectionString);
            sqlConnection.Open();

        }

        public List<Item> getItems(string webShopCode)
        {
            List<Item> itemList = new List<Item>();

            System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT [No_], [Base Unit of Measure], [Item Category Code], [Product Group Code], [Description], [Image Url], [Points] FROM ["+configuration.CompanyName+"$Item] WHERE [Web Shop] = '"+webShopCode+"'";

            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader();

            while (dataReader.Read())
            {
                Item item = new Item(dataReader);
                itemList.Add(item);
            }
            dataReader.Close();

            return itemList;
        }

        public List<Item> applyItemTranslations(List<Item> itemList, string webShopCode)
        {
            
            System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT t.[Item No_], t.[Language Code], t.[Description] FROM [" + configuration.CompanyName + "$Item Translation] t, [" + configuration.CompanyName + "$Item] i  WHERE i.[Web Shop] = '" + webShopCode + "' AND t.[Item No_] = i.[No_]";

            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader();

            while (dataReader.Read())
            {
                ItemTranslation trans = new ItemTranslation(dataReader);

                itemList.FirstOrDefault(i => i.no == dataReader.GetValue(0).ToString()).translations.Add(trans);
            }
            dataReader.Close();

            return itemList;
        }

        public List<Item> applyItemVariants(List<Item> itemList, string webShopCode)
        {

            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(configuration.connectionString);
            sqlConnection.Open();

            System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT v.[Item No_], v.[Code], v.[Description 2], v.[PFVertical Component], v.[PFHorizontal Component] FROM [" + configuration.CompanyName + "$Item Variant] v, [" + configuration.CompanyName + "$Item] i  WHERE i.[Web Shop] = '" + webShopCode + "' AND v.[Item No_] = i.[No_]";

            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader();

            while (dataReader.Read())
            {
                ItemVariant variant = new ItemVariant(dataReader);

                itemList.FirstOrDefault(i => i.no == dataReader.GetValue(0).ToString()).variants.Add(variant);
            }
            dataReader.Close();

            return itemList;
        }

        public List<Item> applyProductText(List<Item> itemList, string webShopCode)
        {


            System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT t.[No_], t.[Language Code], t.[Text] FROM [" + configuration.CompanyName + "$Extended Text Line] t, [" + configuration.CompanyName + "$Item] i  WHERE i.[Web Shop] = '" + webShopCode + "' AND t.[No_] = i.[No_]";

            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader();

            List<ProductText> productTexts = new List<ProductText>();

            while (dataReader.Read())
            {
                if (productTexts.FirstOrDefault(p => p.no == dataReader.GetValue(0).ToString() && p.languageCode == dataReader.GetValue(1).ToString()) != null)
                {
                    productTexts.FirstOrDefault(p => p.no == dataReader.GetValue(0).ToString() && p.languageCode == dataReader.GetValue(1).ToString()).text = productTexts.FirstOrDefault(p => p.no == dataReader.GetValue(0).ToString() && p.languageCode == dataReader.GetValue(1).ToString()).text + " " + dataReader.GetValue(2).ToString();
                }
                else
                {
                    productTexts.Add(new ProductText(dataReader));
                }
            }
            dataReader.Close();

            foreach(ProductText productText in productTexts)
            {
                itemList.FirstOrDefault(i => i.no == productText.no).productTexts.Add(productText);

            }

            return itemList;
        }

        public List<Item> applyInventory(List<Item> itemList, string webShopCode, string locationCode)
        {


            System.Data.SqlClient.SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT t.[Item No_], t.[Variant Code], SUM(t.[Quantity]) FROM [" + configuration.CompanyName + "$Item Ledger Entry] t, [" + configuration.CompanyName + "$Item] i  WHERE i.[Web Shop] = '" + webShopCode + "' AND t.[Item No_] = i.[No_] AND t.[Location Code] = '"+locationCode+"' GROUP BY t.[Item No_], t.[Variant Code]";

            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader();
            
            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(2))
                {
                    if ((!dataReader.IsDBNull(1)) && (dataReader.GetValue(1).ToString() != ""))
                    {
                        itemList.FirstOrDefault(i => i.no == dataReader.GetValue(0).ToString()).variants.FirstOrDefault(v => v.code == dataReader.GetValue(1).ToString()).stock = (int)dataReader.GetDecimal(2);
                    }
                    else
                    {
                        itemList.FirstOrDefault(i => i.no == dataReader.GetValue(0).ToString()).stock = (int)dataReader.GetDecimal(2);
                    }
                }

            }
            dataReader.Close();

            sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT t.[No_], t.[Variant Code], SUM(t.[Quantity]) FROM [" + configuration.CompanyName + "$Sales Line] t, [" + configuration.CompanyName + "$Item] i  WHERE i.[Web Shop] = '" + webShopCode + "' AND t.[No_] = i.[No_] AND t.[Location Code] = '" + locationCode + "' AND t.[Document Type] = 1 AND t.[Type] = 2 GROUP BY t.[No_], t.[Variant Code]";

            dataReader = sqlCommand.ExecuteReader();

            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(1))
                {
                    if ((!dataReader.IsDBNull(1)) && (dataReader.GetValue(1).ToString() != ""))
                    {
                        itemList.FirstOrDefault(i => i.no == dataReader.GetValue(0).ToString()).variants.FirstOrDefault(v => v.code == dataReader.GetValue(1).ToString()).stock -= (int)dataReader.GetDecimal(2);
                    }
                    else
                    {
                        itemList.FirstOrDefault(i => i.no == dataReader.GetValue(0).ToString()).stock -= (int)dataReader.GetDecimal(2);
                    }
                }


            }
            dataReader.Close();



            return itemList;
        }
    }
}
