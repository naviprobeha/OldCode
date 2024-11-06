using Api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Api.Library
{
    public class OrderHistoryHelper
    {

        public static List<OrderHistory> GetOrderHistory(string customerNo, string no, DateTime fromDate, DateTime toDate, int offset = 0, int count = 0, string customerOrderNo = "", string marking = "")
        {
            Customer customer = CustomerHelper.GetCustomer(customerNo);
            if (customer == null) throw new Exception("Invalid customer: " + customerNo);

            Dictionary<string, OrderHistory> orderHistoryTable = new Dictionary<string, OrderHistory>();

            string query = "";
            if (fromDate == null) fromDate = DateTime.MinValue;
            if (toDate == null) toDate = DateTime.MinValue;
            if (fromDate.Year > 1990)
            {
                query = " AND [Order Date] >= '" + fromDate.ToString("yyyy-MM-dd") + "'";
            }
            if (toDate.Year > 1990)
            {
                query = query + " AND [Order Date] <= '" + toDate.ToString("yyyy-MM-dd") + "'";
            }

            string orderQuery = "";
            if (no != "")
            {
                orderQuery = orderQuery + " AND [No_] = @no";
            }
            if ((customerOrderNo != "") && (customerOrderNo != null))
            {
                orderQuery = orderQuery + " AND [External Document No_] = @extDocNo";
            }
            if ((marking != "") && (marking != null))
            {
                orderQuery = orderQuery + " AND UPPER([Note of Goods]) LIKE @marking ";
            }
            Configuration configuration = new Configuration();
            configuration.init();

            if (marking.Length > 20) marking = marking.Substring(0, 20);

            Database database = new Database(configuration);

            DatabaseQuery databaseQuery = database.prepare("SELECT h.[No_], [Sell-to Customer No_], [Currency Code], [Shipping Agent Code], [Shipping Agent Service Code 2], [Ship-to Name], [Ship-to Address], [Ship-to Address 2], [Ship-to Post Code], [Ship-to City], [Ship-to Country_Region Code], [Order Date], [Status], [Invt_ Sys Status], [Payment Method Code], [External Document No_], [Note of Goods], (SELECT SUM([Line Amount]) FROM [" + database.getTableName("Sales Line")+"] l WHERE l.[Document Type] = h.[Document Type] AND l.[Document No_] = h.[No_]) as LineAmount FROM [" + database.getTableName("Sales Header") + "] h WHERE h.[Document Type] = 1 AND [Sell-to Customer No_] = @customerNo "+query+orderQuery+" ORDER BY h.[Order Date] DESC");
            databaseQuery.addStringParameter("customerNo", customerNo, 20);
            databaseQuery.addStringParameter("no", no, 20);
            databaseQuery.addStringParameter("extDocNo", customerOrderNo, 20);
            databaseQuery.addStringParameter("marking", "%"+marking.ToUpper()+"%", 20);


            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                OrderHistory orderHistory = new OrderHistory(dataReader);
                orderHistoryTable.Add(orderHistory.no, orderHistory);
            }

            dataReader.Close();

            if (no != "")
            {
                orderQuery = " AND [Order No_] = @no";
            }

            DatabaseQuery databaseQuery2 = database.prepare("SELECT h.[No_], h.[Order No_], h.[Posting Date], h.[Package Tracking No_], h.[Sell-to Customer No_], h.[Currency Code], h.[Shipping Agent Code], h.[Shipping Agent Service Code 2], h.[Ship-to Name], h.[Ship-to Address], h.[Ship-to Address 2], h.[Ship-to Post Code], h.[Ship-to City], h.[Ship-to Country_Region Code], h.[Order Date], 1 as [Status], 3 as [Invt_ Sys Status], h.[Payment Method Code], h.[External Document No_], h.[Note of Goods], (SELECT SUM((l.[Unit Price] * l.[Quantity])* (1-([Line Discount %]/100))) FROM [" + database.getTableName("Sales Shipment Line") + "] l WHERE l.[Order No_] = h.[Order No_]) as LineAmount FROM [" + database.getTableName("Sales Shipment Header") + "] h WHERE h.[Sell-to Customer No_] = @customerNo "+query+orderQuery+" ORDER BY h.[Posting Date] DESC");
            databaseQuery2.addStringParameter("customerNo", customerNo, 20);
            databaseQuery2.addStringParameter("no", no, 20);
            databaseQuery2.addStringParameter("extDocNo", customerOrderNo, 20);
            databaseQuery2.addStringParameter("marking", "%" + marking.ToUpper() + "%", 20);

            SqlDataReader dataReader2 = databaseQuery2.executeQuery();
            while (dataReader2.Read())
            {
                if (!orderHistoryTable.ContainsKey(dataReader2["Order No_"].ToString()))
                {
                    OrderHistory orderHistory = new OrderHistory(dataReader2);
                    orderHistory.no = dataReader2["Order No_"].ToString();
                    orderHistoryTable.Add(orderHistory.no, orderHistory);
                }

                OrderHistoryShipment shipment = new OrderHistoryShipment(dataReader2);
                orderHistoryTable[dataReader2["Order No_"].ToString()].shipments.Add(shipment);
            }

            dataReader2.Close();

            database.close();

            if (count == 0) count = 100;
            if ((count+offset) > orderHistoryTable.Values.Count)
            {
                count = orderHistoryTable.Values.Count-offset;
                if (offset > orderHistoryTable.Values.Count) return new List<OrderHistory>();
            }
            return orderHistoryTable.Values.ToList<OrderHistory>().GetRange(offset, count);
        }

        public static OrderHistory GetOrder(string customerNo, string no)
        {
            Customer customer = CustomerHelper.GetCustomer(customerNo);
            if (customer == null) throw new Exception("Invalid customer: " + customerNo);

            OrderHistory orderHistory = null;

            Configuration configuration = new Configuration();
            configuration.init();

            Database database = new Database(configuration);

            DatabaseQuery databaseQuery = database.prepare("SELECT h.[No_], [Sell-to Customer No_], [Currency Code], [Shipping Agent Code], [Shipping Agent Service Code 2], [Ship-to Name], [Ship-to Address], [Ship-to Address 2], [Ship-to Post Code], [Ship-to City], [Ship-to Country_Region Code], [Order Date], [Status], [Invt_ Sys Status], [Payment Method Code], [External Document No_], [Note of Goods], (SELECT SUM([Line Amount]) FROM [" + database.getTableName("Sales Line") + "] l WHERE l.[Document Type] = h.[Document Type] AND l.[Document No_] = h.[No_]) as LineAmount FROM [" + database.getTableName("Sales Header") + "] h WHERE h.[Document Type] = 1 AND [Sell-to Customer No_] = @customerNo AND [No_] = @no");
            databaseQuery.addStringParameter("customerNo", customerNo, 20);
            databaseQuery.addStringParameter("no", no, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                orderHistory = new OrderHistory(dataReader);
            }

            dataReader.Close();

            DatabaseQuery databaseQuery2 = database.prepare("SELECT h.[No_], h.[Order No_], h.[Posting Date], h.[Package Tracking No_], h.[Sell-to Customer No_], h.[Currency Code], h.[Shipping Agent Code], h.[Shipping Agent Service Code 2], h.[Ship-to Name], h.[Ship-to Address], h.[Ship-to Address 2], h.[Ship-to Post Code], h.[Ship-to City], h.[Ship-to Country_Region Code], h.[Order Date], 9 as [Status], 3 as [Invt_ Sys Status], h.[Payment Method Code], h.[External Document No_], [Note of Goods], (SELECT SUM((l.[Unit Price] * l.[Quantity])* (1-([Line Discount %]/100))) FROM [" + database.getTableName("Sales Shipment Line") + "] l WHERE l.[Order No_] = h.[Order No_]) as LineAmount FROM [" + database.getTableName("Sales Shipment Header") + "] h WHERE h.[Sell-to Customer No_] = @customerNo AND h.[Order No_] = @orderNo");
            databaseQuery2.addStringParameter("customerNo", customerNo, 20);
            databaseQuery2.addStringParameter("orderNo", no, 20);

            SqlDataReader dataReader2 = databaseQuery2.executeQuery();
            while (dataReader2.Read())
            {
                if (orderHistory == null)
                {
                    orderHistory = new OrderHistory(dataReader2);
                    orderHistory.no = dataReader2["Order No_"].ToString();
                }

                OrderHistoryShipment shipment = new OrderHistoryShipment(dataReader2);
                orderHistory.shipments.Add(shipment);
            }

            dataReader2.Close();

            if (orderHistory != null)
            {
                DatabaseQuery databaseQuery3 = database.prepare("SELECT [Line No_], [Type], [No_], [Quantity], [Description], [Description 2], [Unit Price], [Line Discount %], [Line Discount Amount], [Line Amount], [Item Lot No_], [Quantity Shipped], (SELECT SUM([Quantity]) FROM [" + database.getTableName("Reservation Entry")+ "] WHERE [Source ID] = l.[Document No_] AND [Source Ref_ No_] = l.[Line No_] AND [Source Type] = 37 AND [Reservation Status] = 0) as quantityReserved FROM [" + database.getTableName("Sales Line") + "] l WHERE [Document Type] = 1 AND [Document No_] = @no");
                databaseQuery3.addStringParameter("no", no, 20);

                SqlDataReader dataReader3 = databaseQuery3.executeQuery();
                while (dataReader3.Read())
                {
                    OrderHistoryLine line = new OrderHistoryLine(dataReader3);
                    orderHistory.lines.Add(line);
                }

                dataReader3.Close();

                if (orderHistory.lines.Count == 0)
                {
                    databaseQuery3 = database.prepare("SELECT l.[Line No_], l.[Type], l.[No_], l.[Quantity], l.[Description], l.[Description 2], l.[Unit Price], l.[Line Discount %], ((l.[Unit Price] * l.[Quantity])* (1-([Line Discount %]/100))) as [Line Amount], ((l.[Unit Price] * l.[Quantity])* ([Line Discount %]/100)) as [Line Discount Amount], l.[Item Lot No_], (l.[Quantity]) as [Quantity Shipped], (l.[Quantity]*-1) as quantityReserved FROM [" + database.getTableName("Sales Shipment Line") + "] l WHERE l.[Order No_] = @no");
                    databaseQuery3.addStringParameter("no", no, 20);

                    dataReader3 = databaseQuery3.executeQuery();
                    while (dataReader3.Read())
                    {
                        OrderHistoryLine line = new OrderHistoryLine(dataReader3);
                        orderHistory.lines.Add(line);
                    }

                    dataReader3.Close();
                }
            }

            database.close();

            return orderHistory;
        }
    }
}