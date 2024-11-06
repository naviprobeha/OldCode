using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for SalesPricesUnique.
    /// </summary>
    public class SalesPricesUnique : SalesPricesCalculation
    {
        private Database database;
        private Infojet infojetContext;

        public SalesPricesUnique(Infojet infojetContext)
        {
            //
            // TODO: Add constructor logic here
            //
            this.database = infojetContext.systemDatabase;
            this.infojetContext = infojetContext;
        }

        public SalesPrice getItemPrice(Item item, string currencyCode, string customerNo, string customerPriceGroupCode, string variantCode, float quantity)
        {
            SalesPrice salesPrice = null;

            SqlDataReader dataReader = database.query("SELECT [Item No_], [Sales Code], [Currency Code], [Starting Date], [Unit Price], [Price Includes VAT], [Sales Type], [Minimum Quantity], [Ending Date], [Unit of Measure Code], [Variant Code] FROM [" + database.getTableName("Sales Price") + "] WHERE [Item No_] = '" + item.no + "' AND [Sales Type] = '0' AND [Sales Code] = '" + customerNo + "' AND ([Starting Date] <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR [Ending Date] = '1753-01-01 00:00:00') AND [Currency Code] = '" + currencyCode + "' AND [Variant Code] = '" + variantCode + "' AND [Minimum Quantity] <= '" + quantity.ToString().Replace(",", ".") + "' ORDER BY [Unit Price]");
            if (dataReader.Read())
            {
                salesPrice = new SalesPrice(database, dataReader);
                dataReader.Close();

            }
            else
            {
                dataReader.Close();

                dataReader = database.query("SELECT [Item No_], [Sales Code], [Currency Code], [Starting Date], [Unit Price], [Price Includes VAT], [Sales Type], [Minimum Quantity], [Ending Date], [Unit of Measure Code], [Variant Code] FROM [" + database.getTableName("Sales Price") + "] WHERE [Item No_] = '" + item.no + "' AND [Sales Type] = '1' AND [Sales Code] = '" + customerPriceGroupCode + "' AND ([Starting Date] <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR [Ending Date] = '1753-01-01 00:00:00') AND [Currency Code] = '" + currencyCode + "' AND [Variant Code] = '" + variantCode + "' AND [Minimum Quantity] <= '" + quantity.ToString().Replace(",", ".") + "' ORDER BY [Unit Price]");
                if (dataReader.Read())
                {
                    salesPrice = new SalesPrice(database, dataReader);
                    dataReader.Close();

                }
                else
                {
                    dataReader.Close();

                    dataReader = database.query("SELECT [Item No_], [Sales Code], [Currency Code], [Starting Date], [Unit Price], [Price Includes VAT], [Sales Type], [Minimum Quantity], [Ending Date], [Unit of Measure Code], [Variant Code] FROM [" + database.getTableName("Sales Price") + "] WHERE [Item No_] = '" + item.no + "' AND [Sales Type] = '2' AND ([Starting Date] <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR [Ending Date] = '1753-01-01 00:00:00') AND [Currency Code] = '" + currencyCode + "' AND [Variant Code] = '" + variantCode + "' AND [Minimum Quantity] <= '" + quantity.ToString().Replace(",", ".") + "' ORDER BY [Unit Price]");
                    if (dataReader.Read())
                    {
                        salesPrice = new SalesPrice(database, dataReader);
                        dataReader.Close();

                    }
                    else
                    {
                        dataReader.Close();
                        salesPrice = new SalesPrice(database, null);

                    }

                }
            }

            return salesPrice;

        }

        public Hashtable getItemPrice(DataSet dataSet, string currencyCode, string customerNo, string customerPriceGroupCode, string variantCode, float quantity)
        {
            return null;
        }

        public Hashtable getItemPrice(DataSet dataSet, string currencyCode, string customerPriceGroupCode, string variantCode, float quantity)
        {
            return null;
        }

        public SalesPrice getItemPrice(Item item, string currencyCode, string customerPriceGroupCode, string variantCode, float quantity)
        {
            SalesPrice salesPrice = null;

            SqlDataReader dataReader = database.query("SELECT [Item No_], [Sales Code], [Currency Code], [Starting Date], [Unit Price], [Price Includes VAT], [Sales Type], [Minimum Quantity], [Ending Date], [Unit of Measure Code], [Variant Code] FROM [" + database.getTableName("Sales Price") + "] WHERE [Item No_] = '" + item.no + "' AND ([Sales Type] = '1' AND [Sales Code] = '" + customerPriceGroupCode + "') AND ([Starting Date] <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR [Ending Date] = '1753-01-01 00:00:00') AND [Currency Code] = '" + currencyCode + "' AND [Variant Code] = '" + variantCode + "' AND [Minimum Quantity] <= '" + quantity.ToString().Replace(",", ".") + "' ORDER BY [Unit Price]");
            if (dataReader.Read())
            {
                salesPrice = new SalesPrice(database, dataReader);
                dataReader.Close();

            }
            else
            {
                dataReader.Close();


                if (currencyCode != "")
                {
                    dataReader = database.query("SELECT [Item No_], [Sales Code], [Currency Code], [Starting Date], [Unit Price], [Price Includes VAT], [Sales Type], [Minimum Quantity], [Ending Date], [Unit of Measure Code], [Variant Code] FROM [" + database.getTableName("Sales Price") + "] WHERE [Item No_] = '" + item.no + "' AND ([Sales Type] = '1' AND [Sales Code] = '" + customerPriceGroupCode + "') AND ([Starting Date] <= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR [Starting Date] = '1753-01-01 00:00:00') AND ([Ending Date] >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' OR [Ending Date] = '1753-01-01 00:00:00') AND [Currency Code] = '' AND [Variant Code] = '" + variantCode + "' AND [Minimum Quantity] <= '" + quantity.ToString().Replace(",", ".") + "' ORDER BY [Unit Price]");
                    if (dataReader.Read())
                    {
                        salesPrice = new SalesPrice(database, dataReader);
                    }
                    else
                    {
                        salesPrice = new SalesPrice(database, null);
                    }

                    dataReader.Close();

                }
                else
                {
                    salesPrice = new SalesPrice(database, null);
                }
            }

            return salesPrice;

        }


        public SalesPrice getItemPrice(Item item)
        {
            return getItemPrice(item, "", "", "", "", 1);
        }



        public SalesPrice getItemPrice(Item item, UserSession userSession, string currencyCode)
        {
            return getItemPrice(item, userSession, currencyCode, 1);
        }



        public SalesPrice getItemPrice(Item item, UserSession userSession, string currencyCode, float quantity)
        {
            SalesPrice salesPrice;
            if (userSession != null)
            {
                salesPrice = getItemPrice(item, currencyCode, userSession.customer.no, userSession.customer.customerPriceGroup, "", quantity);
            }
            else
            {
                salesPrice = getItemPrice(item);
            }


            return salesPrice;


        }


        public SalesPrice getItemGroupPrice(Item item, string customerPriceGroupCode, string currencyCode)
        {
            SalesPrice salesPrice;
            salesPrice = getItemPrice(item, currencyCode, customerPriceGroupCode, "", 1);

            return salesPrice;

        }

        public Hashtable getItemGroupPrice(DataSet dataSet, string customerPriceGroupCode, string currencyCode)
        {
            return getItemPrice(dataSet, currencyCode, customerPriceGroupCode, "", 1);

        }

        public Hashtable getItemPrice(DataSet dataSet, UserSession userSession, string currencyCode)
        {
            return getItemPrice(dataSet, userSession, currencyCode, 1);
        }

        public Hashtable getItemPrice(DataSet dataSet, UserSession userSession, string currencyCode, float quantity)
        {
            if (userSession != null)
            {
                return getItemPrice(dataSet, currencyCode, userSession.customer.no, userSession.customer.customerPriceGroup, "", quantity);
            }
            else
            {
                return getItemPrice(dataSet, "", "", "", 1);
            }

        }
    }
}
