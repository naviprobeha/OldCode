using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for SalesPrices.
    /// </summary>
    public class SalesLineDiscountsStandard : SalesLineDiscountsCalculation
    {
        private Database database;

        public SalesLineDiscountsStandard(Database database)
        {
            //
            // TODO: Add constructor logic here
            //
            this.database = database;
        }

        public SalesLineDiscount getItemDiscount(Item item, string currencyCode, string customerNo, string customerDiscGroupCode, string variantCode, float quantity)
        {
            if (currencyCode == null) currencyCode = "";

            SalesLineDiscount salesLineDiscount = null;

            DatabaseQuery databaseQuery = database.prepare("SELECT [Code], [Sales Code], [Currency Code], [Starting Date], [Line Discount %], [Sales Type], [Minimum Quantity], [Ending Date], [Type], [Unit of Measure Code], [Variant Code] FROM [" + database.getTableName("Sales Line Discount") + "] WHERE (([Code] = @itemNo AND [Type] = '0') OR ([Code] = @itemDiscGroup AND [Type] = '1')) AND (([Sales Type] = '0' AND [Sales Code] = @customerNo) OR ([Sales Type] = '1' AND [Sales Code] = @customerDiscGroupCode) OR [Sales Type] = '2') AND ([Starting Date] <= @startingDate OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= @endingDate OR [Ending Date] = '1753-01-01 00:00:00') AND [Currency Code] = @currencyCode AND [Variant Code] = @variantCode AND [Minimum Quantity] <= @quantity ORDER BY [Line Discount %] DESC");
            databaseQuery.addStringParameter("itemNo", item.no, 20);
            databaseQuery.addStringParameter("itemDiscGroup", item.itemDiscGroup, 20);
            databaseQuery.addStringParameter("customerNo", customerNo, 20);
            databaseQuery.addStringParameter("customerDiscGroupCode", customerDiscGroupCode, 20);
            databaseQuery.addDateTimeParameter("startingDate", DateTime.Today);
            databaseQuery.addDateTimeParameter("endingDate", DateTime.Today);
            databaseQuery.addStringParameter("currencyCode", currencyCode, 20);
            databaseQuery.addStringParameter("variantCode", variantCode, 20);
            databaseQuery.addDecimalParameter("quantity", quantity);

            SqlDataReader dataReader = databaseQuery.executeQuery();

            if (dataReader.Read())
            {
                salesLineDiscount = new SalesLineDiscount(database, dataReader);
                dataReader.Close();

            }
            else
            {
                dataReader.Close();

                if (currencyCode != "")
                {
                    databaseQuery = database.prepare("SELECT [Code], [Sales Code], [Currency Code], [Starting Date], [Line Discount %], [Sales Type], [Minimum Quantity], [Ending Date], [Type], [Unit of Measure Code], [Variant Code] FROM [" + database.getTableName("Sales Line Discount") + "] WHERE (([Code] = @itemNo AND [Type] = '0') OR ([Code] = @itemDiscGroup AND [Type] = '1')) AND (([Sales Type] = '0' AND [Sales Code] = @customerNo) OR ([Sales Type] = '1' AND [Sales Code] = @customerDiscGroupCode) OR [Sales Type] = '2') AND ([Starting Date] <= @startingDate OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= @endingDate OR [Ending Date] = '1753-01-01 00:00:00') AND [Currency Code] = '' AND [Variant Code] = @variantCode AND [Minimum Quantity] <= @quantity ORDER BY [Line Discount %] DESC");
                    databaseQuery.addStringParameter("itemNo", item.no, 20);
                    databaseQuery.addStringParameter("itemDiscGroup", item.itemDiscGroup, 20);
                    databaseQuery.addStringParameter("customerNo", customerNo, 20);
                    databaseQuery.addStringParameter("customerDiscGroupCode", customerDiscGroupCode, 20);
                    databaseQuery.addDateTimeParameter("startingDate", DateTime.Today);
                    databaseQuery.addDateTimeParameter("endingDate", DateTime.Today);
                    databaseQuery.addStringParameter("variantCode", variantCode, 20);
                    databaseQuery.addDecimalParameter("quantity", quantity);

                    dataReader = databaseQuery.executeQuery();
                    if (dataReader.Read())
                    {
                        salesLineDiscount = new SalesLineDiscount(database, dataReader);
                    }
                    else
                    {
                        salesLineDiscount = new SalesLineDiscount(database, null);
                    }

                    dataReader.Close();

                }
                else
                {
                    salesLineDiscount = new SalesLineDiscount(database, null);
                }
            }

            return salesLineDiscount;

        }

        public Hashtable getItemDiscount(DataSet dataSet, string currencyCode, string customerNo, string customerDiscGroupCode, string variantCode, float quantity)
        {
            Hashtable itemInfoTable = new Hashtable();

            if (currencyCode == null) currencyCode = "";

            string whereQuery = "";

            int i = 0;
            while (i < dataSet.Tables[0].Rows.Count)
            {
                string itemNo = dataSet.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                if (whereQuery != "")
                {
                    whereQuery = whereQuery + " OR ";
                }
                whereQuery = whereQuery + "i.[No_] = '" + itemNo + "'";

                i++;
            }

            DatabaseQuery databaseQuery = database.prepare("SELECT i.[No_], d.[Line Discount %] FROM [" + database.getTableName("Sales Line Discount") + "] d WITH (NOLOCK), [" + database.getTableName("Item") + "] i WITH (NOLOCK) WHERE (" + whereQuery + ") AND ((d.[Code] = i.[No_] AND [Type] = '0') OR (d.[Code] = i.[Item Disc_ Group] AND [Type] = '1')) AND (([Sales Type] = '0' AND [Sales Code] = @customerNo) OR ([Sales Type] = '1' AND [Sales Code] = @customerDiscGroupCode) OR [Sales Type] = '2') AND ([Starting Date] <= @startingDate OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= @endingDate OR [Ending Date] = '1753-01-01 00:00:00') AND [Currency Code] = @currencyCode AND [Variant Code] = @variantCode AND [Minimum Quantity] <= @quantity ORDER BY i.[No_], [Line Discount %] DESC");
            databaseQuery.addStringParameter("customerNo", customerNo, 20);
            databaseQuery.addStringParameter("customerDiscGroupCode", customerDiscGroupCode, 20);
            databaseQuery.addDateTimeParameter("startingDate", DateTime.Today);
            databaseQuery.addDateTimeParameter("endingDate", DateTime.Today);
            databaseQuery.addStringParameter("currencyCode", currencyCode, 20);
            databaseQuery.addStringParameter("variantCode", variantCode, 20);
            databaseQuery.addDecimalParameter("quantity", quantity);

            SqlDataReader dataReader = databaseQuery.executeQuery();

            while (dataReader.Read())
            {
                string itemNo = dataReader.GetValue(0).ToString();
                float lineDiscount = float.Parse(dataReader.GetValue(1).ToString());

                if (!itemInfoTable.Contains(itemNo))
                {
                    ItemInfo itemInfo = new ItemInfo();
                    itemInfo.no = itemNo;
                    itemInfo.lineDiscount = lineDiscount;
                    itemInfoTable.Add(itemNo, itemInfo);
                }

            }

            dataReader.Close();


            return itemInfoTable;

        }


        public SalesLineDiscount getItemDiscount(Item item, UserSession userSession, string currencyCode)
        {
            return getItemDiscount(item, userSession, currencyCode, 1);

        }

        public SalesLineDiscount getItemDiscount(Item item, UserSession userSession, string currencyCode, float quantity)
        {
            SalesLineDiscount salesLineDiscount = null;

            if (userSession != null)
            {
                salesLineDiscount = getItemDiscount(item, currencyCode, userSession.customer.no, userSession.customer.customerDiscGroup, "", quantity);
            }

            return salesLineDiscount;


        }

        public Hashtable getItemDiscount(DataSet dataSet, UserSession userSession, string currencyCode)
        {
            if (userSession != null)
            {
                return getItemDiscount(dataSet, currencyCode, userSession.customer.no, userSession.customer.customerDiscGroup, "", 1);
            }

            return new Hashtable();
        }

        public Hashtable getItemDiscount(DataSet dataSet, UserSession userSession, string currencyCode, float quantity)
        {
            if (userSession != null)
            {
                return getItemDiscount(dataSet, currencyCode, userSession.customer.no, userSession.customer.customerDiscGroup, "", quantity);
            }

            return new Hashtable();
        }
    }
}
