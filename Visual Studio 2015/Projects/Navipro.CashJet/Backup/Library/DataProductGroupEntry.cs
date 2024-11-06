using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Navipro.Cashjet.Library
{
    public class DataProductGroupEntry
    {
        private string _code;
        private string _description;
        private int _quantity;

        public string code { get { return _code; } set { _code = value; } }
        public string description { get { return _description; } set { _description = value; } }
        public int quantity { get { return _quantity; } set { _quantity = value; } }


        public static DataProductGroupCollection getCollection(Database database, DataEntry dataEntry, int mode)
        {
            DataProductGroupCollection dataProductCollection = new DataProductGroupCollection();

            bool isVersion2013R2 = CashSite.isVersion2013R2(database);

            DatabaseQuery databaseQuery = null;


            if (isVersion2013R2)
            {
                string cashSiteFilter = "";
                if (dataEntry.cashSite != "") cashSiteFilter = "AND c.[POS Store Code] = @cashSite";
                string fieldName = "Quantity";
                if (mode == 1) fieldName = "Amount";


                databaseQuery = database.prepare("SELECT TOP 10 [Item Category Code], [Product Group Code], SUM([" + fieldName + "]), (SELECT ic.[Description]+' '+pg.[Description] FROM [" + database.getTableName("Item Category") + "] ic WITH (NOLOCK), [" + database.getTableName("Product Group") + "] pg WITH (NOLOCK) WHERE pg.[Item Category Code] = ic.[Code] AND pg.[Item Category Code] = l.[Item Category Code] AND pg.[Code] = l.[Product Group Code]) as description FROM [" + database.getTableName("POS Transaction Line") + "] l WITH (NOLOCK), [" + database.getTableName("POS Device") + "] c WITH (NOLOCK) WHERE c.[Code] = l.[POS Device ID] " + cashSiteFilter + " AND l.[Registered Date] >= @fromDate AND l.[Registered Date] <= @toDate AND l.[Product Group Code] <> '' GROUP BY l.[Item Category Code], l.[Product Group Code] ORDER BY SUM([" + fieldName + "]) DESC");
            }
            else
            {
                string cashSiteFilter = "";
                if (dataEntry.cashSite != "") cashSiteFilter = "AND c.[Cash Site Code] = @cashSite";
                string fieldName = "Quantity";
                if (mode == 1) fieldName = "Amount";

                databaseQuery = database.prepare("SELECT TOP 10 [Item Category Code], [Product Group Code], SUM([" + fieldName + "]), (SELECT ic.[Description]+' '+pg.[Description] FROM [" + database.getTableName("Item Category") + "] ic WITH (NOLOCK), [" + database.getTableName("Product Group") + "] pg WITH (NOLOCK) WHERE pg.[Item Category Code] = ic.[Code] AND pg.[Item Category Code] = l.[Item Category Code] AND pg.[Code] = l.[Product Group Code]) as description FROM [" + database.getTableName("Posted Cash Receipt Line") + "] l WITH (NOLOCK), [" + database.getTableName("Cash Register") + "] c WITH (NOLOCK) WHERE c.[Cash Register ID] = l.[Cash Register ID] " + cashSiteFilter + " AND l.[Registered Date] >= @fromDate AND l.[Registered Date] <= @toDate AND l.[Product Group Code] <> '' GROUP BY l.[Item Category Code], l.[Product Group Code] ORDER BY SUM([" + fieldName + "]) DESC");
            }
            
            databaseQuery.addDateTimeParameter("fromDate", dataEntry.fromDate);
            databaseQuery.addDateTimeParameter("toDate", dataEntry.toDate);
            databaseQuery.addStringParameter("cashSite", dataEntry.cashSite, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                DataProductGroupEntry dataProductEntry = new DataProductGroupEntry();
                dataProductEntry.code = dataReader.GetValue(0).ToString() + "-" + dataReader.GetValue(1).ToString();
                dataProductEntry.quantity = (int)dataReader.GetDecimal(2);
                dataProductEntry.description = dataReader.GetValue(3).ToString();
                dataProductCollection.Add(dataProductEntry);

            }
            dataReader.Close();

            return dataProductCollection;
        }
    }


}
