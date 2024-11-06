using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for SalesPrices.
    /// </summary>
    public class SalesLineDiscountsUnique : SalesLineDiscountsCalculation
    {
        private Database database;
        private string campaignCode;

        public SalesLineDiscountsUnique(Database database)
        {
            //
            // TODO: Add constructor logic here
            //
            this.database = database;
        }

        public void setCampaignCode(string campaignCode)
        {
            this.campaignCode = campaignCode;
        }



        public Hashtable getItemDiscount(DataSet dataSet, string currencyCode, string customerNo, string customerDiscGroupCode)
        {
            Hashtable itemInfoTable = new Hashtable();

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

            DatabaseQuery databaseQuery = database.prepare("SELECT i.[No_], d.[Line Discount %] FROM [" + database.getTableName("Sales Line Discount") + "] d, [" + database.getTableName("Item") + "] i WHERE (" + whereQuery + ") AND ((d.[Code] = i.[No_] AND [Type] = '0') OR (d.[Code] = i.[Item Disc_ Group] AND [Type] = '1')) AND (([Sales Type] = '0' AND [Sales Code] = @customerNo) OR ([Sales Type] = '1' AND [Sales Code] = @customerDiscGroupCode) OR [Sales Type] = '2') AND ([Starting Date] <= @startingDate OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= @endingDate OR [Ending Date] = '1753-01-01 00:00:00') AND [Currency Code] = @currencyCode ORDER BY i.[No_], [Line Discount %] DESC");
            databaseQuery.addStringParameter("customerNo", customerNo, 20);
            databaseQuery.addStringParameter("customerDiscGroupCode", customerDiscGroupCode, 20);
            databaseQuery.addDateTimeParameter("startingDate", DateTime.Today);
            databaseQuery.addDateTimeParameter("endingDate", DateTime.Today);
            databaseQuery.addStringParameter("currencyCode", currencyCode, 20);

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

 
        public Hashtable getItemDiscount(DataSet dataSet, Customer customer, string currencyCode)
        {
            if (customer != null)
            {
                return getItemDiscount(dataSet, currencyCode, customer.no, customer.customerDiscGroup);
            }

            return new Hashtable();
        }

    }
}
