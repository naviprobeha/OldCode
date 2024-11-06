using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaviPro.PowerBIConnector;
using System.Data.SqlClient;

namespace PowerBISync
{
    public class SalesDashboardModule : PowerBIModule
    {
        private Configuration configuration;
        private Database database;
        private Logger logger;

        public SalesDashboardModule(Configuration configuration, Database database, Logger logger)
        {
            this.configuration = configuration;
            this.database = database;
            this.logger = logger;

        }

        public void run()
        {
            logger.write("SalesDashboard - Logging on to Power BI", 0);
            PowerBIConnector powerBiConnector = new PowerBIConnector(configuration.powerBiUserName, configuration.powerBiPassword, configuration.powerBiAppId, "");


            logger.write("Updating table structure", 0);
            PowerBITable dashboardTable = new PowerBITable("dashboard");

            dashboardTable.addField("turnOverCurrentMonth", "Int64");
            dashboardTable.addField("turnOverCurrentMonthLY", "Int64");
            dashboardTable.addField("budgetCurrentMonth", "Int64");
            dashboardTable.addField("outstandingOrderAmountCurrentMonth", "Int64");
            //dashboardTable.addField("outstandingOrderAmountLastMonth", "Int64");
            dashboardTable.addField("totalTurnOverCurrentMonth", "Int64");

            dashboardTable.addField("turnOverLastMonth", "Int64");
            dashboardTable.addField("turnOverLastMonthLY", "Int64");
            dashboardTable.addField("budgetLastMonth", "Int64");


            PowerBITable districtTable = new PowerBITable("district");

            districtTable.addField("code", "String");
            districtTable.addField("turnOverCurrentMonth", "Int64");
            districtTable.addField("turnOverCurrentMonthLY", "Int64");

            PowerBITable itemCategoryTable = new PowerBITable("itemCategory");

            itemCategoryTable.addField("code", "String");
            itemCategoryTable.addField("turnOverCurrentMonth", "Int64");
            itemCategoryTable.addField("turnOverCurrentMonthLY", "Int64");

            powerBiConnector.AddTable(dashboardTable);
            powerBiConnector.AddTable(districtTable);
            powerBiConnector.AddTable(itemCategoryTable);

            powerBiConnector.CreateTableStructure("salesDashboard");
            logger.write("Done!", 0);
            logger.write("Pushing data...", 0);

            powerBiConnector.ClearData("salesDashboard", "dashboard");
            powerBiConnector.ClearData("salesDashboard", "district");
            powerBiConnector.ClearData("salesDashboard", "itemCategory");

            DateTime fromDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime toDate = fromDate.AddMonths(1);

            decimal turnoverCurrentMonth = calcTurnOverAmount(fromDate, toDate);
            decimal turnoverCurrentMonthFY = calcTurnOverAmount(fromDate.AddYears(-1), toDate.AddYears(-1));
            decimal budgetCurrentMonth = calcBudgetAmount(fromDate, toDate);
            decimal outstandingOrderAmount = calcOutstandingOrderAmount(fromDate, toDate);
            decimal outstandingOrderAmountLastMonth = calcOutstandingOrderAmount(fromDate.AddMonths(-1), toDate.AddMonths(-1));


            decimal turnoverLastMonth = calcTurnOverAmount(fromDate.AddMonths(-1), toDate.AddMonths(-1));
            decimal turnoverLastMonthFY = calcTurnOverAmount(fromDate.AddMonths(-1).AddYears(-1), toDate.AddMonths(-1).AddYears(-1));
            decimal budgetLastMonth = calcBudgetAmount(fromDate.AddMonths(-1), toDate.AddMonths(-1));

            PowerBIRecord record = new PowerBIRecord();
            record.addFieldValue("turnOverCurrentMonth", ((int)turnoverCurrentMonth*-1).ToString());
            record.addFieldValue("turnOverCurrentMonthLY", ((int)turnoverCurrentMonthFY * -1).ToString());
            record.addFieldValue("budgetCurrentMonth", ((int)budgetCurrentMonth).ToString());
            record.addFieldValue("turnOverLastMonth", ((int)(outstandingOrderAmountLastMonth+(turnoverLastMonth * -1))).ToString());
            record.addFieldValue("turnOverLastMonthLY", ((int)(turnoverLastMonthFY * -1)).ToString());
            record.addFieldValue("budgetLastMonth", ((int)budgetLastMonth).ToString());
            //record.addFieldValue("outstandingOrderAmountLastMonth", ((int)outstandingOrderAmountLastMonth).ToString());
            record.addFieldValue("outstandingOrderAmountCurrentMonth", ((int)outstandingOrderAmount).ToString());
            record.addFieldValue("totalTurnOverCurrentMonth", ((int)(outstandingOrderAmount+(turnoverCurrentMonth*-1))).ToString());
            powerBiConnector.AddRecord(record);

            powerBiConnector.ClearData("salesDashboard", "dashboard");
            powerBiConnector.ClearData("salesDashboard", "district");
            powerBiConnector.ClearData("salesDashboard", "itemCategory");

            powerBiConnector.AddData("salesDashboard", "dashboard");

            Dictionary<string, PowerBIRecord> recordTable = new Dictionary<string, PowerBIRecord>();
            Dictionary<string, decimal> districtValues = new Dictionary<string, decimal>();

            DatabaseQuery districtQuery = database.prepare("SELECT [Salespers__Purch_ Code], SUM([Sales Amount (Actual)]) FROM [" + database.getTableName("Value Entry") + "] WHERE [Item Ledger Entry Type] = 1 AND [Posting Date] >= @fromDate AND [Posting Date] < @toDate GROUP BY [Salespers__Purch_ Code]");
            districtQuery.addDateTimeParameter("fromDate", fromDate);
            districtQuery.addDateTimeParameter("toDate", toDate);
            SqlDataReader dataReader = districtQuery.executeQuery();

            while(dataReader.Read())
            {
                if (districtValues.ContainsKey(dataReader.GetValue(0).ToString()))
                {
                    districtValues[dataReader.GetValue(0).ToString()] = districtValues[dataReader.GetValue(0).ToString()] + dataReader.GetDecimal(1);
                }
                else
                {
                    districtValues.Add(dataReader.GetValue(0).ToString(), dataReader.GetDecimal(1));
                }

            }
            dataReader.Close();
            
            districtQuery = database.prepare("SELECT h.[Salesperson Code], SUM(l.[Outstanding Amount]) FROM [" + database.getTableName("Sales Line") + "] l, [" + database.getTableName("Sales Header") + "] h WHERE h.[Document Type] = l.[Document Type] AND h.[No_] = l.[Document No_] AND  h.[Document Type] = 1 AND h.[Shipment Date] >= @fromDate AND h.[Shipment Date] < @toDate GROUP BY h.[Salesperson Code]");
            districtQuery.addDateTimeParameter("fromDate", fromDate);
            districtQuery.addDateTimeParameter("toDate", toDate);
            dataReader = districtQuery.executeQuery();

            while (dataReader.Read())
            {
                decimal amount = dataReader.GetDecimal(1);
                amount = amount * (decimal)0.8;

                if (districtValues.ContainsKey(dataReader.GetValue(0).ToString()))
                {
                    districtValues[dataReader.GetValue(0).ToString()] = districtValues[dataReader.GetValue(0).ToString()] + amount;
                }
                else
                {
                    districtValues.Add(dataReader.GetValue(0).ToString(), amount);
                }
            }
            dataReader.Close();
            
            foreach(string districtCode in districtValues.Keys)
            {
                
                PowerBIRecord districtRecord = new PowerBIRecord();
                districtRecord.addFieldValue("code", districtCode);
                districtRecord.addFieldValue("turnOverCurrentMonth", ((int)districtValues[districtCode]).ToString());
                districtRecord.addFieldValue("turnOverCurrentMonthLY", "0");
                recordTable.Add(districtCode, districtRecord);                

            }

            districtQuery = database.prepare("SELECT [Salespers__Purch_ Code], SUM([Sales Amount (Actual)]) FROM [" + database.getTableName("Value Entry") + "] WHERE [Item Ledger Entry Type] = 1 AND [Posting Date] >= @fromDate AND [Posting Date] < @toDate GROUP BY [Salespers__Purch_ Code]");
            districtQuery.addDateTimeParameter("fromDate", fromDate.AddYears(-1));
            districtQuery.addDateTimeParameter("toDate", toDate.AddYears(-1));
            dataReader = districtQuery.executeQuery();

            while (dataReader.Read())
            {
                if (recordTable.ContainsKey(dataReader.GetValue(0).ToString()))
                {
                    recordTable[dataReader.GetValue(0).ToString()].addFieldValue("turnOverCurrentMonthLY", ((int)dataReader.GetDecimal(1)).ToString());
                }
                else
                {
                    PowerBIRecord districtRecord = new PowerBIRecord();
                    districtRecord.addFieldValue("code", dataReader.GetValue(0).ToString());
                    districtRecord.addDefaultFieldValue("turnOverCurrentMonth", "0");
                    districtRecord.addFieldValue("turnOverCurrentMonthLY", ((int)dataReader.GetDecimal(1)).ToString());
                    recordTable.Add(dataReader.GetValue(0).ToString(), districtRecord);
                }
            }
            dataReader.Close();

            foreach(PowerBIRecord powerBiRecord in recordTable.Values)
            {
                powerBiConnector.AddRecord(powerBiRecord);
            }
            
            powerBiConnector.AddData("salesDashboard", "district");

            recordTable = new Dictionary<string, PowerBIRecord>();
            Dictionary<string, decimal> categoryValues = new Dictionary<string, decimal>();

            DatabaseQuery itemCategoryQuery = database.prepare("SELECT i.[Item Category Code], SUM(v.[Sales Amount (Actual)]) FROM [" + database.getTableName("Value Entry") + "] v, [" + database.getTableName("Item Ledger Entry") + "] i WHERE v.[Item Ledger Entry Type] = 1 AND v.[Posting Date] >= @fromDate AND v.[Posting Date] < @toDate AND i.[Entry No_] = v.[Item Ledger Entry No_] GROUP BY i.[Item Category Code]");
            itemCategoryQuery.addDateTimeParameter("fromDate", fromDate);
            itemCategoryQuery.addDateTimeParameter("toDate", toDate);
            dataReader = itemCategoryQuery.executeQuery();

            while (dataReader.Read())
            {
                decimal amount = dataReader.GetDecimal(1);
                amount = amount * (decimal)0.8;

                if (categoryValues.ContainsKey(dataReader.GetValue(0).ToString()))
                {
                    categoryValues[dataReader.GetValue(0).ToString()] = categoryValues[dataReader.GetValue(0).ToString()] + amount;
                }
                else
                {
                    categoryValues.Add(dataReader.GetValue(0).ToString(), amount);
                }

            }
            dataReader.Close();

            districtQuery = database.prepare("SELECT l.[Item Category Code], SUM(l.[Outstanding Amount]) FROM [" + database.getTableName("Sales Line") + "] l, [" + database.getTableName("Sales Header") + "] h WHERE h.[Document Type] = l.[Document Type] AND h.[No_] = l.[Document No_] AND h.[Document Type] = 1 AND h.[Shipment Date] >= @fromDate AND h.[Shipment Date] < @toDate GROUP BY l.[Item Category Code]");
            districtQuery.addDateTimeParameter("fromDate", fromDate);
            districtQuery.addDateTimeParameter("toDate", toDate);
            dataReader = districtQuery.executeQuery();

            while (dataReader.Read())
            {
                decimal amount = dataReader.GetDecimal(1);
                amount = amount * (decimal)0.8;

                if (categoryValues.ContainsKey(dataReader.GetValue(0).ToString()))
                {
                    categoryValues[dataReader.GetValue(0).ToString()] = categoryValues[dataReader.GetValue(0).ToString()] + amount;
                }
                else
                {
                    categoryValues.Add(dataReader.GetValue(0).ToString(), amount);
                }
            }
            dataReader.Close();

            foreach (string itemCategoryCode in categoryValues.Keys)
            {

                PowerBIRecord itemCategoryRecord = new PowerBIRecord();
                itemCategoryRecord.addFieldValue("code", itemCategoryCode);
                itemCategoryRecord.addFieldValue("turnOverCurrentMonth", ((int)categoryValues[itemCategoryCode]).ToString());
                recordTable.Add(itemCategoryCode, itemCategoryRecord);

            }

            itemCategoryQuery = database.prepare("SELECT i.[Item Category Code], SUM(v.[Sales Amount (Actual)]) FROM [" + database.getTableName("Value Entry") + "] v, [" + database.getTableName("Item Ledger Entry") + "] i WHERE v.[Item Ledger Entry Type] = 1 AND v.[Posting Date] >= @fromDate AND v.[Posting Date] < @toDate AND i.[Entry No_] = v.[Item Ledger Entry No_] GROUP BY i.[Item Category Code]");
            itemCategoryQuery.addDateTimeParameter("fromDate", fromDate.AddYears(-1));
            itemCategoryQuery.addDateTimeParameter("toDate", toDate.AddYears(-1));
            dataReader = itemCategoryQuery.executeQuery();

            while (dataReader.Read())
            {
                if (recordTable.ContainsKey(dataReader.GetValue(0).ToString()))
                {
                    recordTable[dataReader.GetValue(0).ToString()].addFieldValue("turnOverCurrentMonthLY", ((int)dataReader.GetDecimal(1)).ToString());
                }
                else
                {
                    PowerBIRecord itemCategoryRecord = new PowerBIRecord();
                    itemCategoryRecord.addFieldValue("code", dataReader.GetValue(0).ToString());
                    itemCategoryRecord.addFieldValue("turnOverCurrentMonthLY", ((int)dataReader.GetDecimal(1)).ToString());
                    recordTable.Add(dataReader.GetValue(0).ToString(), itemCategoryRecord);
                }
            }
            dataReader.Close();

            foreach (PowerBIRecord powerBiRecord in recordTable.Values)
            {
                powerBiConnector.AddRecord(powerBiRecord);
            }

            powerBiConnector.AddData("salesDashboard", "itemCategory");
            
            logger.write("Completed!", 0);
        }

        private decimal calcTurnOverAmount(DateTime fromDate, DateTime toDate)
        {
            decimal amount = 0;
            DatabaseQuery databaseQuery = database.prepare("SELECT SUM([Amount]) FROM ["+database.getTableName("G_L Entry")+"] WHERE [G_L Account No_] LIKE '30%' AND [Posting Date] >= @fromDate AND [Posting Date] < @toDate");
            databaseQuery.addDateTimeParameter("fromDate", fromDate);
            databaseQuery.addDateTimeParameter("toDate", toDate);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    amount = dataReader.GetDecimal(0);
                }
            }
            dataReader.Close();

            return amount;
        }

        private decimal calcBudgetAmount(DateTime fromDate, DateTime toDate)
        {
            decimal amount = 0;
            DatabaseQuery databaseQuery = database.prepare("SELECT SUM([Amount]) FROM [" + database.getTableName("G_L Budget Entry") + "] WHERE [G_L Account No_] LIKE '30%' AND [Date] >= @fromDate AND [Date] < @toDate");
            databaseQuery.addDateTimeParameter("fromDate", fromDate);
            databaseQuery.addDateTimeParameter("toDate", toDate);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    amount = dataReader.GetDecimal(0);
                }
            }
            dataReader.Close();

            return amount;
        }

        private decimal calcOutstandingOrderAmount(DateTime fromDate, DateTime toDate)
        {
            decimal amount = 0;
            DatabaseQuery databaseQuery = database.prepare("SELECT SUM([Outstanding Amount]) FROM [" + database.getTableName("Sales Line") + "] WHERE [Document Type] = 1 AND [Shipment Date] >= @fromDate AND [Shipment Date] < @toDate");
            databaseQuery.addDateTimeParameter("fromDate", fromDate);
            databaseQuery.addDateTimeParameter("toDate", toDate);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0))
                {
                    amount = dataReader.GetDecimal(0);
                }
            }
            dataReader.Close();

            amount = amount * (decimal)0.8;
            return amount;
        }
    }
}
