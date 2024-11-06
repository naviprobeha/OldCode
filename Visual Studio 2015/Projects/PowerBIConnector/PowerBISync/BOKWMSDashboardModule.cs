using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaviPro.PowerBIConnector;
using System.Data.SqlClient;

namespace PowerBISync
{
    public class BOKWMSDashboardModule : PowerBIModule
    {
        private Configuration configuration;
        private Database database;
        private Logger logger;
        private string databaseName = "warehouseDashboard_v3";

        public BOKWMSDashboardModule(Configuration configuration, Database database, Logger logger)
        {
            this.configuration = configuration;
            this.database = database;
            this.logger = logger;


        }

        public void run()
        {
            logger.write("BOKWMSDashboard - Logging on to Power BI", 0);
            PowerBIConnector powerBiConnector = new PowerBIConnector(configuration.powerBiUserName, configuration.powerBiPassword, configuration.powerBiAppId, "");


            logger.write("Updating table structure", 0);
            PowerBITable kpiTable = new PowerBITable("kpi");

            kpiTable.addField("todaysShipments", "Int64");
            kpiTable.addField("yesterdaysShipments", "Int64");
            kpiTable.addField("ordersToShip", "Int64");
            kpiTable.addField("todaysOrdersToShip", "Int64");
            kpiTable.addField("backlogOrders", "Int64");
            kpiTable.addField("avgPickTime", "Int64");
            kpiTable.addField("plannedReceiptsToday", "Int64");
            kpiTable.addField("delayedReceipts", "Int64");
            kpiTable.addField("plannedReceiptLinesToday", "Int64");
            kpiTable.addField("receiptQuantityToday", "Int64");
            kpiTable.addField("plannedReceiptLinesTodayNotReceived", "Int64");
            kpiTable.addField("receiptQuantityTodayNotReceived", "Int64");
            kpiTable.addField("postedWhseReceiptLinesToday", "Int64");
            kpiTable.addField("postedWhseReceiptQtyToday", "Int64");

            kpiTable.addField("putAwayLinesTodayNotRegistered", "Int64");
            kpiTable.addField("putAwayQtyTodayNotRegistered", "Int64");
            kpiTable.addField("putAwayLinesTodayRegistered", "Int64");
            kpiTable.addField("putAwayQtyTodayRegistered", "Int64");
            kpiTable.addField("pickLinesTodayRegistered", "Int64");
            kpiTable.addField("pickLinesTodayTarget", "Int64");

            PowerBITable itemsPerHourTable = new PowerBITable("itemsPerHour");

            itemsPerHourTable.addField("hour", "String");
            itemsPerHourTable.addField("qty", "Int64");

            PowerBITable topPicksTable = new PowerBITable("marketPickTime");

            topPicksTable.addField("market", "String");
            topPicksTable.addField("durationMinutes", "Int64");

            PowerBITable itemCategoryTable = new PowerBITable("itemCategories");

            itemCategoryTable.addField("description", "String");
            itemCategoryTable.addField("qty", "Int64");

            powerBiConnector.AddTable(kpiTable);
            powerBiConnector.AddTable(itemsPerHourTable);
            powerBiConnector.AddTable(topPicksTable);
            powerBiConnector.AddTable(itemCategoryTable);

            powerBiConnector.CreateTableStructure(databaseName);

            logger.write("Done!", 0);
            logger.write("Pushing data...", 0);

            powerBiConnector.ClearData(databaseName, "kpi");
            powerBiConnector.ClearData(databaseName, "itemsPerHour");
            powerBiConnector.ClearData(databaseName, "marketPickTime");
            powerBiConnector.ClearData(databaseName, "itemCategories");

            generateItemCategoryData(powerBiConnector, DateTime.Today, 10);
            generatePickPerMarketData(powerBiConnector, DateTime.Today, 10);
            generateKPIData(powerBiConnector, DateTime.Today);
            generateItemsPerHourData(powerBiConnector, DateTime.Today, DateTime.Today.AddDays(1));

            logger.write("Completed!", 0);
        }

        private void generateKPIData(PowerBIConnector powerBiConnector, DateTime date)
        {

            PowerBIRecord record = new PowerBIRecord();

            DatabaseQuery databaseQuery = database.prepare("SELECT COUNT(*) FROM [" + database.getTableName("Sales Shipment Line") + "] WITH (NOLOCK) WHERE [Posting Date] = @currentDate AND [Type] = 2");
            databaseQuery.addDateTimeParameter("currentDate", date);

            int todaysShipments = 0;
            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read()) todaysShipments = (int)dataReader.GetInt32(0);
            dataReader.Close();

            record.addFieldValue("todaysShipments", todaysShipments.ToString());


            databaseQuery = database.prepare("SELECT COUNT(*) FROM [" + database.getTableName("Sales Shipment Line") + "] WITH (NOLOCK) WHERE [Posting Date] = @currentDate AND [Type] = 2");
            databaseQuery.addDateTimeParameter("currentDate", date.AddDays(-1));

            int yesterdaysShipments = 0;
            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read()) yesterdaysShipments = (int)dataReader.GetInt32(0);
            dataReader.Close();

            record.addFieldValue("yesterdaysShipments", yesterdaysShipments.ToString());


            databaseQuery = database.prepare("SELECT COUNT(*) FROM [" + database.getTableName("Sales Header") + "] WITH (NOLOCK) WHERE [Document Type] = 1 AND [Delivery Status] = 1");

            int ordersToShip = 0;
            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read()) ordersToShip = (int)dataReader.GetInt32(0);
            dataReader.Close();

            record.addFieldValue("ordersToShip", ordersToShip.ToString());


            databaseQuery = database.prepare("SELECT COUNT(*) FROM [" + database.getTableName("Sales Header") + "] WITH (NOLOCK) WHERE [Document Type] = 1 AND [Complete Deliverystatus Date] = @currentDate");
            databaseQuery.addDateTimeParameter("currentDate", date);

            int todayOrdersToShip = 0;
            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read()) todayOrdersToShip = (int)dataReader.GetInt32(0);
            dataReader.Close();

            record.addFieldValue("todaysOrdersToShip", todayOrdersToShip.ToString());
            record.addFieldValue("backlogOrders", (ordersToShip - todayOrdersToShip).ToString());

            record.addFieldValue("avgPickTime", "0");



            databaseQuery = database.prepare("SELECT COUNT(*), SUM([Outstanding Quantity]) FROM [" + database.getTableName("Purchase Line") + "] WITH (NOLOCK) WHERE [Document Type] = 1 AND [Expected Receipt Date] = @currentDate AND [Outstanding Quantity] > 0");
            databaseQuery.addDateTimeParameter("currentDate", date);

            int receiptQty = 0;
            int receiptLines = 0;
            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                receiptLines = (int)dataReader.GetInt32(0);
                if (!dataReader.IsDBNull(1))
                    receiptQty = (int)dataReader.GetDecimal(1);
            }
            dataReader.Close();

            databaseQuery = database.prepare("SELECT COUNT(*) FROM [" + database.getTableName("Purchase Line") + "] WITH (NOLOCK) WHERE [Document Type] = 1 AND [Expected Receipt Date] = @currentDate AND [Outstanding Quantity] > 0 GROUP BY [Document No_]");
            databaseQuery.addDateTimeParameter("currentDate", date);

            int receiptOrders = 0;
            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read()) receiptOrders = (int)dataReader.GetInt32(0);
            dataReader.Close();

            databaseQuery = database.prepare("SELECT COUNT(*) as noOfLines, SUM([Qty_ to Receive]) as qty FROM [" + database.getTableName("Warehouse Receipt Line") + "] WITH (NOLOCK) WHERE [Source Type] = 39 AND [Qty_ Outstanding] > 0");

            int linesNotReceived = 0;
            int qtyNotReceived = 0;
            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                linesNotReceived = (int)dataReader.GetInt32(0);
                qtyNotReceived = (int)dataReader.GetDecimal(1);
            }
            dataReader.Close();

            record.addFieldValue("plannedReceiptsToday", receiptOrders.ToString());
            record.addFieldValue("plannedReceiptLinesToday", receiptLines.ToString());
            record.addFieldValue("receiptQuantityToday", receiptQty.ToString());
            record.addFieldValue("plannedReceiptLinesTodayNotReceived", linesNotReceived.ToString());
            record.addFieldValue("receiptQuantityTodayNotReceived", qtyNotReceived.ToString());



            databaseQuery = database.prepare("SELECT COUNT(*) FROM [" + database.getTableName("Purchase Line") + "] WITH (NOLOCK) WHERE [Document Type] = 1 AND [Expected Receipt Date] < @currentDate AND [Outstanding Quantity] > 0 GROUP BY [Document No_]");
            databaseQuery.addDateTimeParameter("currentDate", date);

            int delayedReceipts = 0;
            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read()) delayedReceipts = (int)dataReader.GetInt32(0);
            dataReader.Close();

            record.addFieldValue("delayedReceipts", delayedReceipts.ToString());


            databaseQuery = database.prepare("SELECT COUNT(*), SUM(Quantity) FROM [" + database.getTableName("Posted Whse_ Receipt Line") + "] WITH (NOLOCK) WHERE [Posting Date] = @currentDate");
            databaseQuery.addDateTimeParameter("currentDate", date);

            int receivedLines = 0;
            int receivedQty = 0;
            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                receivedLines = (int)dataReader.GetInt32(0);
                if (!dataReader.IsDBNull(1))
                    receivedQty = (int)dataReader.GetDecimal(1);
            }
            dataReader.Close();

            databaseQuery = database.prepare("SELECT COUNT(*), SUM(Quantity) FROM [" + database.getTableName("Warehouse Activity Line") + "] WITH (NOLOCK) WHERE [Activity Type] = 1 AND [Action Type] = 2");

            int putAwayNotRegged = 0;
            int putAwayQtyNotRegged = 0;
            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                putAwayNotRegged = (int)dataReader.GetInt32(0);
                putAwayQtyNotRegged = (int)dataReader.GetDecimal(1);
            }
            dataReader.Close();

            databaseQuery = database.prepare("SELECT COUNT(*), SUM(Quantity) FROM [" + database.getTableName("Registered Whse_ Activity Hdr_") + "] h WITH (NOLOCK), [" + database.getTableName("Registered Whse_ Activity Line") + "] l WITH (NOLOCK) WHERE h.[Type] = l.[Activity Type] AND h.[No_] = l.[No_] AND l.[Activity Type] = 1 AND l.[Action Type] = 2 AND h.[Registering Date] = @currentDate");
            databaseQuery.addDateTimeParameter("currentDate", date);

            int putAwayRegged = 0;
            int putAwayQtyRegged = 0;
            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                putAwayRegged = (int)dataReader.GetInt32(0);
                if (!dataReader.IsDBNull(1))
                    putAwayQtyRegged = (int)dataReader.GetDecimal(1);
            }
            dataReader.Close();

            record.addFieldValue("postedWhseReceiptLinesToday", receivedLines.ToString());
            record.addFieldValue("postedWhseReceiptQtyToday", receivedQty.ToString());


            record.addFieldValue("putAwayLinesTodayNotRegistered", putAwayNotRegged.ToString());
            record.addFieldValue("putAwayQtyTodayNotRegistered", putAwayQtyNotRegged.ToString());
            record.addFieldValue("putAwayLinesTodayRegistered", putAwayRegged.ToString());
            record.addFieldValue("putAwayQtyTodayRegistered", putAwayQtyRegged.ToString());



            databaseQuery = database.prepare("SELECT [Picked Rows], [Put Away Rows] FROM [" + database.getTableName("Target Power BI Whse") + "] WITH (NOLOCK) WHERE [Date] = @currentDate");
            databaseQuery.addDateTimeParameter("currentDate", date);

            int pickedRowsTarget = 0;
            int putAwayRowsTarget = 0;
            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                pickedRowsTarget = (int)dataReader.GetInt32(0);
                putAwayRowsTarget = (int)dataReader.GetInt32(1);
            }
            dataReader.Close();

            databaseQuery = database.prepare("SELECT COUNT(*) FROM [" + database.getTableName("Registered Whse_ Activity Hdr_") + "] h WITH (NOLOCK), [" + database.getTableName("Registered Whse_ Activity Line") + "] l WITH (NOLOCK) WHERE h.[Type] = l.[Activity Type] AND h.[No_] = l.[No_] AND l.[Activity Type] = 2 AND l.[Action Type] = 2 AND h.[Registering Date] = @currentDate");
            databaseQuery.addDateTimeParameter("currentDate", date);

            int pickedRegged = 0;
            dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                pickedRegged = (int)dataReader.GetInt32(0);
            }
            dataReader.Close();

            record.addFieldValue("pickLinesTodayRegistered", pickedRegged.ToString());
            record.addFieldValue("pickLinesTodayTarget", pickedRowsTarget.ToString());


            powerBiConnector.AddRecord(record);


            powerBiConnector.AddData(databaseName, "kpi");

        }

        private void generateItemCategoryData(PowerBIConnector powerBiConnector, DateTime date, int noOfDaysBack)
        {

            DatabaseQuery databaseQuery = database.prepare("SELECT [Product Group Code], SUM(Quantity) FROM [" + database.getTableName("Sales Shipment Line") + "] WITH (NOLOCK) WHERE [Posting Date] >= @fromDate AND [Posting Date] <= @toDate AND [Type] = 2 GROUP BY [Product Group Code]");
            databaseQuery.addDateTimeParameter("fromDate", date.AddDays(noOfDaysBack * -1));
            databaseQuery.addDateTimeParameter("toDate", date);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                PowerBIRecord record = new PowerBIRecord();

                record.addFieldValue("description", dataReader.GetValue(0).ToString());
                record.addFieldValue("qty", ((int)dataReader.GetDecimal(1)).ToString());
                powerBiConnector.AddRecord(record);

            }
            dataReader.Close();

            powerBiConnector.AddData(databaseName, "itemCategories");



        }


        private void generatePickPerMarketData(PowerBIConnector powerBiConnector, DateTime date, int noOfDaysBack)
        {
            Dictionary<string, MarketTable> marketDict = new Dictionary<string, MarketTable>();


            DatabaseQuery databaseQuery = database.prepare("SELECT [Whse Market Code], [Assignment Date], [Assignment Time], [Registering DateTime] FROM [" + database.getTableName("Registered Whse_ Activity Hdr_") + "] WITH (NOLOCK) WHERE [Type] = 2 AND [Registering Date] >= @fromDate AND [Registering Date] <= @toDate");
            databaseQuery.addDateTimeParameter("fromDate", date.AddDays(noOfDaysBack * -1));
            databaseQuery.addDateTimeParameter("toDate", date.AddDays(-1));

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                MarketTable market = null;
                if (!marketDict.ContainsKey(dataReader.GetValue(0).ToString()))
                {
                    market = new MarketTable();
                    market.description = dataReader.GetValue(0).ToString();
                    marketDict.Add(dataReader.GetValue(0).ToString(), market);
                }
                market = marketDict[dataReader.GetValue(0).ToString()];

                DateTime start = DateTime.Parse(dataReader.GetDateTime(1).ToString("yyyy-MM-dd") + " " + dataReader.GetDateTime(2).ToString("HH:mm:ss"));
                DateTime end = dataReader.GetDateTime(3);

                market.duration = market.duration + start.Subtract(end).Minutes;
                market.quantity = market.quantity + 1;

                marketDict[dataReader.GetValue(0).ToString()] = market;

            }
            dataReader.Close();

            foreach (string key in marketDict.Keys)
            {
                PowerBIRecord record = new PowerBIRecord();

                record.addFieldValue("market", key);
                record.addFieldValue("durationMinutes", ((int)(marketDict[key].duration / marketDict[key].quantity)).ToString());
                powerBiConnector.AddRecord(record);
            }

            powerBiConnector.AddData(databaseName, "marketPickTime");



        }


        private void generateItemsPerHourData(PowerBIConnector powerBiConnector, DateTime fromDate, DateTime toDate)
        {
            Dictionary<string, MarketTable> marketDict = new Dictionary<string, MarketTable>();


            DatabaseQuery databaseQuery = database.prepare("SELECT [Registering DateTime], (SELECT COUNT(*) FROM [" + database.getTableName("Registered Whse_ Activity Line") + "] l WITH (NOLOCK) WHERE l.[Activity Type] = 2 AND l.[No_] = h.[No_]) as noOfLines FROM [" + database.getTableName("Registered Whse_ Activity Hdr_") + "] h WITH (NOLOCK) WHERE [Type] = 2 AND h.[Registering Date] >= @fromDate AND h.[Registering Date] <= @toDate");
            databaseQuery.addDateTimeParameter("fromDate", fromDate);
            databaseQuery.addDateTimeParameter("toDate", toDate);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                MarketTable market = null;
                if (!marketDict.ContainsKey(dataReader.GetDateTime(0).Hour.ToString()))
                {
                    market = new MarketTable();
                    market.description = dataReader.GetDateTime(0).Hour.ToString();
                    marketDict.Add(dataReader.GetDateTime(0).Hour.ToString(), market);
                }
                market = marketDict[dataReader.GetDateTime(0).Hour.ToString()];

                market.quantity = market.quantity + (int)dataReader.GetInt32(1);

                marketDict[dataReader.GetDateTime(0).Hour.ToString()] = market;

            }
            dataReader.Close();

            foreach (string key in marketDict.Keys)
            {
                PowerBIRecord record = new PowerBIRecord();

                record.addFieldValue("hour", key);
                record.addFieldValue("qty", marketDict[key].quantity.ToString());
                powerBiConnector.AddRecord(record);
            }

            powerBiConnector.AddData(databaseName, "itemsPerHour");



        }
    }
 

    public class MarketTable
    {
        public string description { get; set; }
        public decimal quantity { get; set; }
        public int duration { get; set; }
    }

}
