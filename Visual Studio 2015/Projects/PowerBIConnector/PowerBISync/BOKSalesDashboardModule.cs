using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaviPro.PowerBIConnector;
using System.Data.SqlClient;

namespace PowerBISync
{
    public class BOKSalesDashboardModule : PowerBIModule
    {
        private Configuration configuration;
        private Database database;
        private Logger logger;
        private string databaseName = "salesDashboard_v2";

        public BOKSalesDashboardModule(Configuration configuration, Database database, Logger logger)
        {
            this.configuration = configuration;
            this.database = database;
            this.logger = logger;

            
        }

        public void run()
        {
            logger.write("BOKSalesDashboard - Logging on to Power BI", 0);
            PowerBIConnector powerBiConnector = new PowerBIConnector(configuration.powerBiUserName, configuration.powerBiPassword, configuration.powerBiAppId, "");


            logger.write("Updating table structure", 0);
            PowerBITable revenueTable = new PowerBITable("revenue");

            revenueTable.addField("market", "String");
            revenueTable.addField("department", "String");
            revenueTable.addField("salesAmount", "Int64");
            revenueTable.addField("budgetAmount", "Int64");
            revenueTable.addField("budgetCostAmount", "Int64");
            revenueTable.addField("budgetOrderCount", "Int64");
            revenueTable.addField("costAmount", "Int64");
            revenueTable.addField("orderCount", "Int64");


            PowerBITable itemCategoryTable = new PowerBITable("itemCategories");

            itemCategoryTable.addField("code", "String");
            itemCategoryTable.addField("description", "String");
            itemCategoryTable.addField("qty", "Int64");
            itemCategoryTable.addField("salesAmount", "Int64");
            itemCategoryTable.addField("costAmount", "Int64");

            PowerBITable itemTable = new PowerBITable("items");

            itemTable.addField("no", "String");
            itemTable.addField("description", "String");
            itemTable.addField("qty", "Int64");
            itemTable.addField("salesAmount", "Int64");
            itemTable.addField("costAmount", "Int64");

            powerBiConnector.AddTable(revenueTable);
            powerBiConnector.AddTable(itemCategoryTable);
            powerBiConnector.AddTable(itemTable);

            powerBiConnector.CreateTableStructure(databaseName);

            logger.write("Done!", 0);
            logger.write("Pushing data...", 0);

            powerBiConnector.ClearData(databaseName, "revenue");
            powerBiConnector.ClearData(databaseName, "itemCategories");
            powerBiConnector.ClearData(databaseName, "items");

            generateRevenueData(powerBiConnector, DateTime.Today);
            generateItemsData(powerBiConnector, DateTime.Today);




            logger.write("Completed!", 0);
        }

        private void generateRevenueData(PowerBIConnector powerBiConnector, DateTime date)
        {
            Dictionary<string, SalesTable> revenueTable = calcRevenuePerDim(date);
            revenueTable = applyBudget(date, revenueTable);

            addRevenueRecord(powerBiConnector, revenueTable, "101|109|401|409", "Kitchen SE", "Kitchen");
            addRevenueRecord(powerBiConnector, revenueTable, "102|402", "Kitchen NO", "Kitchen");
            addRevenueRecord(powerBiConnector, revenueTable, "103|403", "Kitchen DK", "Kitchen");
            addRevenueRecord(powerBiConnector, revenueTable, "404", "Kitchen FI", "Kitchen");

            addRevenueRecord(powerBiConnector, revenueTable, "501|509", "Robot SE", "Robot");
            addRevenueRecord(powerBiConnector, revenueTable, "502", "Robot NO", "Robot");
            addRevenueRecord(powerBiConnector, revenueTable, "503", "Robot DK", "Robot");
            addRevenueRecord(powerBiConnector, revenueTable, "504", "Robot FI", "Robot");

            powerBiConnector.AddData(databaseName, "revenue");
        }

        private void generateItemsData(PowerBIConnector powerBiConnector, DateTime date)
        {
            Dictionary<string, SalesTable> itemCategoryTable = new Dictionary<string, SalesTable>();
            Dictionary<string, SalesTable> itemTable = new Dictionary<string, SalesTable>();

            Dictionary<string, string> itemCategories = getItemCategories();

            calcItems(date, ref itemCategoryTable, ref itemTable);

            foreach(string key in itemCategoryTable.Keys)
            {
                SalesTable salesTable = itemCategoryTable[key];

                PowerBIRecord record = new PowerBIRecord();

                string itemCategoryDesc = "";
                if (itemCategories.ContainsKey(key)) itemCategoryDesc = itemCategories[key];

                record.addFieldValue("code", key);
                record.addFieldValue("description", itemCategoryDesc);
                record.addFieldValue("qty", Math.Round(salesTable.quantity).ToString());
                record.addFieldValue("salesAmount", Math.Round(salesTable.salesAmount).ToString());
                record.addFieldValue("costAmount", Math.Round(salesTable.costAmount).ToString());

                powerBiConnector.AddRecord(record);
            }

            powerBiConnector.AddData(databaseName, "itemCategories");

            foreach (string key in itemTable.Keys)
            {

                SalesTable salesTable = itemTable[key];

                PowerBIRecord record = new PowerBIRecord();


                record.addFieldValue("no", key);
                record.addFieldValue("description", salesTable.description);
                record.addFieldValue("qty", Math.Round(salesTable.quantity).ToString());
                record.addFieldValue("salesAmount", Math.Round(salesTable.salesAmount).ToString());
                record.addFieldValue("costAmount", Math.Round(salesTable.costAmount).ToString());

                powerBiConnector.AddRecord(record);

            }

            powerBiConnector.AddData(databaseName, "items");


        }

        private Dictionary<string, SalesTable> calcRevenuePerDim(DateTime date)
        {
            Dictionary<string, SalesTable> revenueTable = new Dictionary<string, SalesTable>();
            List<string> orderNoList = new List<string>();
             

            //Sales Orders
            DatabaseQuery databaseQuery = database.prepare("SELECT h.[No_], l.[Shortcut Dimension 1 Code], l.[Quantity], l.[Quantity Invoiced], h.[Currency Factor], h.[Prices Including VAT], l.[Line Amount], l.[VAT _], l.[Unit Cost (LCY)], h.[Prices Including VAT] FROM [" + database.getTableName("Sales Header") + "] h WITH (NOLOCK), [" + database.getTableName("Sales Line") + "] l WITH (NOLOCK) WHERE l.[Document Type] = h.[Document Type] AND l.[Document No_] = h.[No_] AND l.[Type] = 2 AND h.[Order Date (SiteDirect)] = @currentDate AND h.[Document Type] = 1");
            databaseQuery.addDateTimeParameter("currentDate", date);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                SalesTable salesTable = null;
                if (!revenueTable.ContainsKey(dataReader.GetString(dataReader.GetOrdinal("Shortcut Dimension 1 Code"))))
                {
                    salesTable = new SalesTable();
                    revenueTable.Add(dataReader.GetString(dataReader.GetOrdinal("Shortcut Dimension 1 Code")), salesTable);
                }
                else
                {
                    salesTable = revenueTable[dataReader.GetString(dataReader.GetOrdinal("Shortcut Dimension 1 Code"))];
                }

                decimal qty = dataReader.GetDecimal(dataReader.GetOrdinal("Quantity")) - dataReader.GetDecimal(dataReader.GetOrdinal("Quantity Invoiced"));
                if (qty != 0)
                {
                    decimal currencyFactor = dataReader.GetDecimal(dataReader.GetOrdinal("Currency Factor"));
                    if (currencyFactor == 0) currencyFactor = 1;
                    if (dataReader.GetValue(dataReader.GetOrdinal("Prices Including VAT")).ToString() == "1")
                    {
                        salesTable.salesAmount = salesTable.salesAmount + ((dataReader.GetDecimal(dataReader.GetOrdinal("Line Amount")) / ((dataReader.GetDecimal(dataReader.GetOrdinal("VAT _"))/100)+1)) / dataReader.GetDecimal(dataReader.GetOrdinal("Quantity")) * qty / currencyFactor);
                    }
                    else
                    {
                        salesTable.salesAmount = salesTable.salesAmount + (dataReader.GetDecimal(dataReader.GetOrdinal("Line Amount")) / dataReader.GetDecimal(dataReader.GetOrdinal("Quantity")) * qty / currencyFactor);
                    }
                    salesTable.costAmount = salesTable.costAmount + (dataReader.GetDecimal(dataReader.GetOrdinal("Unit Cost (LCY)"))  * qty);


                }

                if (!orderNoList.Contains(dataReader.GetString(dataReader.GetOrdinal("No_"))))
                {
                    orderNoList.Add(dataReader.GetString(dataReader.GetOrdinal("No_")));
                    salesTable.orderCount++;
                }

                revenueTable[dataReader.GetString(dataReader.GetOrdinal("Shortcut Dimension 1 Code"))] = salesTable;

            }
            dataReader.Close();


            //Sales Invoices
            databaseQuery = database.prepare("SELECT h.[No_], h.[Order No_], l.[Shortcut Dimension 1 Code], l.[Quantity], h.[Currency Factor], h.[Prices Including VAT], l.[Amount], l.[VAT _], l.[Unit Cost (LCY)], h.[Prices Including VAT], (SELECT SUM([Cost Amount (Actual)]) FROM [" + database.getTableName("Value Entry") + "] WITH (NOLOCK) WHERE [Document No_] = h.[No_] AND [Document Line No_] = l.[Line No_]) as CostAmount FROM [" + database.getTableName("Sales Invoice Header") + "] h WITH (NOLOCK), [" + database.getTableName("Sales Invoice Line") + "] l WITH (NOLOCK) WHERE l.[Document No_] = h.[No_] AND l.[Type] = 2 AND h.[Order Date (SiteDirect)] = @currentDate");
            databaseQuery.addDateTimeParameter("currentDate", date);

            dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                SalesTable salesTable = null;
                if (!revenueTable.ContainsKey(dataReader.GetString(dataReader.GetOrdinal("Shortcut Dimension 1 Code"))))
                {
                    salesTable = new SalesTable();
                    revenueTable.Add(dataReader.GetString(dataReader.GetOrdinal("Shortcut Dimension 1 Code")), salesTable);
                }
                else
                {
                    salesTable = revenueTable[dataReader.GetString(dataReader.GetOrdinal("Shortcut Dimension 1 Code"))];
                }

                decimal qty = dataReader.GetDecimal(dataReader.GetOrdinal("Quantity"));
                if (qty != 0)
                {
                    decimal currencyFactor = dataReader.GetDecimal(dataReader.GetOrdinal("Currency Factor"));
                    if (currencyFactor == 0) currencyFactor = 1;
                    salesTable.salesAmount = salesTable.salesAmount + (dataReader.GetDecimal(dataReader.GetOrdinal("Amount")) / dataReader.GetDecimal(dataReader.GetOrdinal("Quantity")) * qty / currencyFactor);

                    salesTable.costAmount = salesTable.costAmount + dataReader.GetDecimal(dataReader.GetOrdinal("CostAmount"));


                }

                if (!orderNoList.Contains(dataReader.GetString(dataReader.GetOrdinal("Order No_"))))
                {
                    orderNoList.Add(dataReader.GetString(dataReader.GetOrdinal("Order No_")));
                    salesTable.orderCount++;
                }

                revenueTable[dataReader.GetString(dataReader.GetOrdinal("Shortcut Dimension 1 Code"))] = salesTable;

            }
            dataReader.Close();

            return revenueTable;
        }

        private void calcItems(DateTime date, ref Dictionary<string, SalesTable> itemCategoryTable, ref Dictionary<string, SalesTable> itemTable)
        {
            itemCategoryTable = new Dictionary<string, SalesTable>();
            itemTable = new Dictionary<string, SalesTable>();
            

            //Sales Orders
            DatabaseQuery databaseQuery = database.prepare("SELECT h.[No_], l.[No_] as itemNo, l.[Item Category Code], l.[Quantity], l.[Quantity Invoiced], h.[Currency Factor], h.[Prices Including VAT], l.[Line Amount], l.[VAT _], l.[Unit Cost (LCY)], h.[Prices Including VAT], l.[Description] FROM [" + database.getTableName("Sales Header") + "] h WITH (NOLOCK), [" + database.getTableName("Sales Line") + "] l WITH (NOLOCK) WHERE l.[Document Type] = h.[Document Type] AND l.[Document No_] = h.[No_] AND l.[Type] = 2 AND h.[Order Date (SiteDirect)] = @currentDate AND h.[Document Type] = 1");
            databaseQuery.addDateTimeParameter("currentDate", date);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                SalesTable salesTable = null;

                //Item Categories
                if (!itemCategoryTable.ContainsKey(dataReader.GetString(dataReader.GetOrdinal("Item Category Code"))))
                {
                    salesTable = new SalesTable();
                    itemCategoryTable.Add(dataReader.GetString(dataReader.GetOrdinal("Item Category Code")), salesTable);
                }
                else
                {
                    salesTable = itemCategoryTable[dataReader.GetString(dataReader.GetOrdinal("Item Category Code"))];
                }

                decimal qty = dataReader.GetDecimal(dataReader.GetOrdinal("Quantity")) - dataReader.GetDecimal(dataReader.GetOrdinal("Quantity Invoiced"));
                if (qty != 0)
                {
                    decimal currencyFactor = dataReader.GetDecimal(dataReader.GetOrdinal("Currency Factor"));
                    if (currencyFactor == 0) currencyFactor = 1;
                    if (dataReader.GetValue(dataReader.GetOrdinal("Prices Including VAT")).ToString() == "1")
                    {
                        salesTable.salesAmount = salesTable.salesAmount + ((dataReader.GetDecimal(dataReader.GetOrdinal("Line Amount")) / ((dataReader.GetDecimal(dataReader.GetOrdinal("VAT _")) / 100) + 1)) / dataReader.GetDecimal(dataReader.GetOrdinal("Quantity")) * qty / currencyFactor);
                    }
                    else
                    {
                        salesTable.salesAmount = salesTable.salesAmount + (dataReader.GetDecimal(dataReader.GetOrdinal("Line Amount")) / dataReader.GetDecimal(dataReader.GetOrdinal("Quantity")) * qty / currencyFactor);
                    }
                    salesTable.costAmount = salesTable.costAmount + (dataReader.GetDecimal(dataReader.GetOrdinal("Unit Cost (LCY)")) * qty);
                    salesTable.quantity = salesTable.quantity + qty;

                }

                itemCategoryTable[dataReader.GetString(dataReader.GetOrdinal("Item Category Code"))] = salesTable;

                //Items
                if (!itemTable.ContainsKey(dataReader.GetString(dataReader.GetOrdinal("itemNo"))))
                {
                    salesTable = new SalesTable();
                    salesTable.description = dataReader.GetString(dataReader.GetOrdinal("Description"));
                    itemTable.Add(dataReader.GetString(dataReader.GetOrdinal("itemNo")), salesTable);
                }
                else
                {
                    salesTable = itemTable[dataReader.GetString(dataReader.GetOrdinal("itemNo"))];
                }

                qty = dataReader.GetDecimal(dataReader.GetOrdinal("Quantity")) - dataReader.GetDecimal(dataReader.GetOrdinal("Quantity Invoiced"));
                if (qty != 0)
                {
                    decimal currencyFactor = dataReader.GetDecimal(dataReader.GetOrdinal("Currency Factor"));
                    if (currencyFactor == 0) currencyFactor = 1;
                    if (dataReader.GetValue(dataReader.GetOrdinal("Prices Including VAT")).ToString() == "1")
                    {
                        salesTable.salesAmount = salesTable.salesAmount + ((dataReader.GetDecimal(dataReader.GetOrdinal("Line Amount")) / ((dataReader.GetDecimal(dataReader.GetOrdinal("VAT _")) / 100) + 1)) / dataReader.GetDecimal(dataReader.GetOrdinal("Quantity")) * qty / currencyFactor);
                    }
                    else
                    {
                        salesTable.salesAmount = salesTable.salesAmount + (dataReader.GetDecimal(dataReader.GetOrdinal("Line Amount")) / dataReader.GetDecimal(dataReader.GetOrdinal("Quantity")) * qty / currencyFactor);
                    }
                    salesTable.costAmount = salesTable.costAmount + (dataReader.GetDecimal(dataReader.GetOrdinal("Unit Cost (LCY)")) * qty);
                    salesTable.quantity = salesTable.quantity + qty;

                }

                itemTable[dataReader.GetString(dataReader.GetOrdinal("itemNo"))] = salesTable;



            }
            dataReader.Close();


            //Sales Invoices
            databaseQuery = database.prepare("SELECT h.[No_], h.[Order No_], l.[No_] as itemNo, l.[Item Category Code], l.[Quantity], h.[Currency Factor], h.[Prices Including VAT], l.[Amount], l.[VAT _], l.[Unit Cost (LCY)], h.[Prices Including VAT], (SELECT SUM([Cost Amount (Actual)]) FROM [" + database.getTableName("Value Entry") + "] WITH (NOLOCK) WHERE [Document No_] = h.[No_] AND [Document Line No_] = l.[Line No_]) as CostAmount FROM [" + database.getTableName("Sales Invoice Header") + "] h WITH (NOLOCK), [" + database.getTableName("Sales Invoice Line") + "] l WITH (NOLOCK) WHERE l.[Document No_] = h.[No_] AND l.[Type] = 2 AND h.[Order Date (SiteDirect)] = @currentDate");
            databaseQuery.addDateTimeParameter("currentDate", date);

            dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                SalesTable salesTable = null;

                //Item Categories
                if (!itemCategoryTable.ContainsKey(dataReader.GetString(dataReader.GetOrdinal("Item Category Code"))))
                {
                    salesTable = new SalesTable();
                    itemCategoryTable.Add(dataReader.GetString(dataReader.GetOrdinal("Item Category Code")), salesTable);
                }
                else
                {
                    salesTable = itemCategoryTable[dataReader.GetString(dataReader.GetOrdinal("Item Category Code"))];
                }

                decimal qty = dataReader.GetDecimal(dataReader.GetOrdinal("Quantity"));
                if (qty != 0)
                {
                    decimal currencyFactor = dataReader.GetDecimal(dataReader.GetOrdinal("Currency Factor"));
                    if (currencyFactor == 0) currencyFactor = 1;
                    salesTable.salesAmount = salesTable.salesAmount + (dataReader.GetDecimal(dataReader.GetOrdinal("Amount")) / dataReader.GetDecimal(dataReader.GetOrdinal("Quantity")) * qty / currencyFactor);

                    salesTable.costAmount = salesTable.costAmount + (dataReader.GetDecimal(dataReader.GetOrdinal("CostAmount") * -1));
                    salesTable.quantity = salesTable.quantity + qty;


                }

                itemCategoryTable[dataReader.GetString(dataReader.GetOrdinal("Item Category Code"))] = salesTable;


                //Items
                if (!itemTable.ContainsKey(dataReader.GetString(dataReader.GetOrdinal("itemNo"))))
                {
                    salesTable = new SalesTable();
                    salesTable.description = dataReader.GetString(dataReader.GetOrdinal("Description"));
                    itemTable.Add(dataReader.GetString(dataReader.GetOrdinal("itemNo")), salesTable);
                }
                else
                {
                    salesTable = itemTable[dataReader.GetString(dataReader.GetOrdinal("itemNo"))];
                }

                qty = dataReader.GetDecimal(dataReader.GetOrdinal("Quantity"));
                if (qty != 0)
                {
                    decimal currencyFactor = dataReader.GetDecimal(dataReader.GetOrdinal("Currency Factor"));
                    if (currencyFactor == 0) currencyFactor = 1;
                    salesTable.salesAmount = salesTable.salesAmount + (dataReader.GetDecimal(dataReader.GetOrdinal("Amount")) / dataReader.GetDecimal(dataReader.GetOrdinal("Quantity")) * qty / currencyFactor);

                    salesTable.costAmount = salesTable.costAmount + (dataReader.GetDecimal(dataReader.GetOrdinal("CostAmount") * -1));
                    salesTable.quantity = salesTable.quantity + qty;


                }

                itemTable[dataReader.GetString(dataReader.GetOrdinal("itemNo"))] = salesTable;

            }
            dataReader.Close();

        }

        private Dictionary<string, string> getItemCategories()
        {
            Dictionary<string, string> itemCategoryTable = new Dictionary<string, string>();

            DatabaseQuery databaseQuery = database.prepare("SELECT [Code], [Description] FROM [" + database.getTableName("Item Category") + "]");
            SqlDataReader dataReader = databaseQuery.executeQuery();
            while(dataReader.Read())
            {
                itemCategoryTable.Add(dataReader.GetValue(0).ToString(), dataReader.GetValue(1).ToString());

            }

            dataReader.Close();

            return itemCategoryTable;

        }

        private Dictionary<string, SalesTable> applyBudget(DateTime date, Dictionary<string, SalesTable> revenueTable)
        {
            var keyList = revenueTable.Keys;
            foreach(string key in keyList)
            {

                DatabaseQuery databaseQuery = database.prepare("SELECT [Sales Amount] FROM [" + database.getTableName("Item Budget Entry") + "] WITH (NOLOCK) WHERE [Date] = @currentDate AND [Global Dimension 1 Code] = @dimCode AND [Sales Amount] > 0");
                databaseQuery.addDateTimeParameter("currentDate", date);
                databaseQuery.addStringParameter("dimCode", key, 20);

                SqlDataReader dataReader = databaseQuery.executeQuery();
                if (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0))
                    {
                        revenueTable[key].budgetSalesAmount = revenueTable[key].budgetSalesAmount + dataReader.GetDecimal(0);
                    }
                }
                dataReader.Close();

                databaseQuery = database.prepare("SELECT [Cost Amount] FROM [" + database.getTableName("Item Budget Entry") + "] WITH (NOLOCK) WHERE [Date] = @currentDate AND [Global Dimension 1 Code] = @dimCode AND [Cost Amount] > 0");
                databaseQuery.addDateTimeParameter("currentDate", date);
                databaseQuery.addStringParameter("dimCode", key, 20);

                dataReader = databaseQuery.executeQuery();
                if (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0))
                    {
                        revenueTable[key].budgetCostAmount = revenueTable[key].budgetCostAmount + dataReader.GetDecimal(0);
                    }
                }
                dataReader.Close();

                databaseQuery = database.prepare("SELECT [Quantity] FROM [" + database.getTableName("Item Budget Entry") + "] WITH (NOLOCK) WHERE [Date] = @currentDate AND [Global Dimension 1 Code] = @dimCode AND [Quantity] > 0");
                databaseQuery.addDateTimeParameter("currentDate", date);
                databaseQuery.addStringParameter("dimCode", key, 20);

                dataReader = databaseQuery.executeQuery();
                if (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0))
                    {
                        revenueTable[key].budgetOrderCount = revenueTable[key].budgetOrderCount + dataReader.GetDecimal(0);
                    }
                }
                dataReader.Close();


            }

            return revenueTable;
        }


        private void addRevenueRecord(PowerBIConnector powerBiConnector, Dictionary<string, SalesTable> revenueTable, string departmentFilter, string name, string departmentName)
        {
            PowerBIRecord record = new PowerBIRecord();

            decimal salesAmount = 0;
            decimal costAmount = 0;
            decimal budgetSalesAmount = 0;
            decimal budgetCostAmount = 0;
            decimal orderCount = 0;
            decimal budgetOrderCount = 0;

            string[] departmentList = departmentFilter.Split('|');
            foreach(string department in departmentList)
            {
                
                if (revenueTable.ContainsKey(department))
                {
                    SalesTable salesTable = revenueTable[department];
                    salesAmount += salesTable.salesAmount;
                    costAmount += salesTable.costAmount;
                    budgetSalesAmount += salesTable.budgetSalesAmount;
                    budgetCostAmount += salesTable.budgetCostAmount;
                    orderCount += salesTable.orderCount;
                    budgetOrderCount += salesTable.budgetOrderCount;
                }
            }

            record.addFieldValue("market", name);
            record.addFieldValue("department", departmentName);
            record.addFieldValue("salesAmount", Math.Round(salesAmount).ToString());
            record.addFieldValue("costAmount", Math.Round(costAmount).ToString());
            record.addFieldValue("budgetAmount", Math.Round(budgetSalesAmount).ToString());
            record.addFieldValue("budgetCostAmount", Math.Round(budgetCostAmount).ToString());
            record.addFieldValue("budgetOrderCount", Math.Round(budgetOrderCount).ToString());
            record.addFieldValue("orderCount", Math.Round(orderCount).ToString());


            powerBiConnector.AddRecord(record);
        }

    }

    public class SalesTable
    {
        public string description { get; set; }
        public decimal quantity { get; set; }
        public decimal salesAmount { get; set; }
        public decimal costAmount { get; set; }
        public decimal budgetSalesAmount { get; set; }
        public decimal budgetCostAmount { get; set; }
        public decimal budgetOrderCount { get; set; }
        public int orderCount { get; set; }
    }

}
