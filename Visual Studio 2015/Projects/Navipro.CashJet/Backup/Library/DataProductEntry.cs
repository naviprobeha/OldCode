using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Navipro.Cashjet.Library
{
    public class DataProductEntry
    {
        private string _no;
        private string _description;
        private int _quantity;

        public string no { get { return _no; } set { _no = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public int quantity { get { return _quantity; } set { _quantity = value; } }


        public static DataProductCollection getCollection(Database database, DataEntry dataEntry, int mode)
        {
            DataProductCollection dataProductCollection = new DataProductCollection();

            bool isVersion2013R2 = CashSite.isVersion2013R2(database);

            DatabaseQuery databaseQuery = null;

            if (isVersion2013R2)
            {
                string cashSiteFilter = "";
                if (dataEntry.cashSite != "") cashSiteFilter = "AND c.[POS Store Code] = @cashSite";
                string fieldName = "Quantity";
                if (mode == 1) fieldName = "Amount";

                databaseQuery = database.prepare("SELECT TOP 10 l.[Sales No_], (SELECT [Description] FROM [" + database.getTableName("Item") + "] WITH (NOLOCK) WHERE [No_] = l.[Sales No_]) as description, SUM([" + fieldName + "]) FROM [" + database.getTableName("POS Transaction Line") + "] l WITH (NOLOCK), [" + database.getTableName("POS Device") + "] c WITH (NOLOCK) WHERE c.[Code] = l.[POS Device ID] " + cashSiteFilter + " AND l.[Registered Date] >= @fromDate AND l.[Registered Date] <= @toDate AND l.[Line Type] = 0 AND l.[Sales Type] = 2 GROUP BY l.[Sales No_] ORDER BY SUM([" + fieldName + "]) DESC");

            }
            else
            {
                string cashSiteFilter = "";
                if (dataEntry.cashSite != "") cashSiteFilter = "AND c.[Cash Site Code] = @cashSite";

                string fieldName = "Quantity";
                if (mode == 1) fieldName = "Amount";

                databaseQuery = database.prepare("SELECT TOP 10 l.[Sales No_], (SELECT [Description] FROM [" + database.getTableName("Item") + "] WITH (NOLOCK) WHERE [No_] = l.[Sales No_]) as description, SUM([" + fieldName + "]) FROM [" + database.getTableName("Posted Cash Receipt Line") + "] l WITH (NOLOCK), [" + database.getTableName("Cash Register") + "] c WITH (NOLOCK) WHERE c.[Cash Register ID] = l.[Cash Register ID] " + cashSiteFilter + " AND l.[Registered Date] >= @fromDate AND l.[Registered Date] <= @toDate AND l.[Line Type] = 0 AND l.[Sales Type] = 2 GROUP BY l.[Sales No_] ORDER BY SUM([" + fieldName + "]) DESC");
            }
            databaseQuery.addDateTimeParameter("fromDate", dataEntry.fromDate);
            databaseQuery.addDateTimeParameter("toDate", dataEntry.toDate);
            databaseQuery.addStringParameter("cashSite", dataEntry.cashSite, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                DataProductEntry dataProductEntry = new DataProductEntry();
                dataProductEntry.no = dataReader.GetValue(0).ToString();
                dataProductEntry.description = dataReader.GetValue(1).ToString();
                dataProductEntry.quantity = (int)dataReader.GetDecimal(2);
                dataProductCollection.Add(dataProductEntry);

            }
            dataReader.Close();

            return dataProductCollection;
        }

        public static void log(string message)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter("C:\\temp\\dataproductcollection.log", true);
            writer.WriteLine(message);
            writer.Flush();
            writer.Close();
        }
    }


}
