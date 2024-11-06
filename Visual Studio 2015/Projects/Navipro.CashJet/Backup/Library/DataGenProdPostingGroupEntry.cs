using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Navipro.Cashjet.Library
{
    public class DataGenProdPostingGroupEntry
    {
        private string _code;
        private string _description;
        private int _quantity;
        private decimal _amount;

        public string code { get { return _code; } set { _code = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public int quantity { get { return _quantity; } set { _quantity = value; } }
        public decimal amount { get { return _amount; } set { _amount = value; } }


        public static DataGenProdPostingGroupCollection getCollection(Database database, DataEntry dataEntry, int mode)
        {
            DataGenProdPostingGroupCollection dataProductCollection = new DataGenProdPostingGroupCollection();

            bool isVersion2013R2 = CashSite.isVersion2013R2(database);

            DatabaseQuery databaseQuery = null;


            if (isVersion2013R2)
            {
                string cashSiteFilter = "";
                if (dataEntry.cashSite != "") cashSiteFilter = "AND c.[POS Store Code] = @cashSite";
                string fieldName = "Quantity";
                if (mode == 1) fieldName = "Amount";

                databaseQuery = database.prepare("SELECT TOP 10 i.[Gen_ Prod_ Posting Group], SUM([Quantity]), SUM([Amount Incl_ VAT]), (SELECT [Description] FROM [" + database.getTableName("Gen_ Product Posting Group") + "] g WITH (NOLOCK) WHERE g.[Code] = i.[Gen_ Prod_ Posting Group]) as description FROM [" + database.getTableName("POS Transaction Line") + "] l WITH (NOLOCK), [" + database.getTableName("POS Device") + "] c WITH (NOLOCK), [" + database.getTableName("Item") + "] i WITH (NOLOCK) WHERE l.[Sales Type] = 2 AND l.[Sales No_] = i.[No_] AND l.[Void] = 0 AND c.[Code] = l.[POS Device ID] " + cashSiteFilter + " AND l.[Registered Date] >= @fromDate AND l.[Registered Date] <= @toDate GROUP BY i.[Gen_ Prod_ Posting Group] ORDER BY i.[Gen_ Prod_ Posting Group]");
            }
            else
            {

            }

            databaseQuery.addDateTimeParameter("fromDate", dataEntry.fromDate);
            databaseQuery.addDateTimeParameter("toDate", dataEntry.toDate);
            databaseQuery.addStringParameter("cashSite", dataEntry.cashSite, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                DataGenProdPostingGroupEntry dataProductEntry = new DataGenProdPostingGroupEntry();
                dataProductEntry.code = dataReader.GetValue(0).ToString();
                dataProductEntry.quantity = (int)dataReader.GetDecimal(1);
                dataProductEntry.amount = dataReader.GetDecimal(2);
                dataProductEntry.description = dataReader.GetValue(3).ToString();
                dataProductCollection.Add(dataProductEntry);

            }
            dataReader.Close();

            return dataProductCollection;
        }
    }


}
