using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;

namespace Navipro.Cashjet.Library
{
    public class DataReceipt
    {
        private string _no;
        private DateTime _registeredDate;
        private DateTime _registeredTime;
        private decimal _totalAmountInclVat;
        private decimal _noOfSalesItems;
        private decimal _noOfReturnItems;
        private decimal _totalDiscountAmount;
        private decimal _totalUnitCost;
        private decimal _totalAmountExclVat;
        private Database _database;

        public DataReceipt(Database database)
        {
            _database = database;
        }

        public string no { get { return _no; } set { _no = value; } }
        public DateTime registeredDate { get { return _registeredDate; } set { _registeredDate = value; } }
        public DateTime registeredTime { get { return _registeredTime; } set { _registeredTime = value; } }
        public decimal totalAmountInclVat { get { return _totalAmountInclVat; } set { _totalAmountInclVat = value; } }
        public decimal noOfSalesItems { get { return _noOfSalesItems; } set { _noOfSalesItems = value; } }
        public decimal noOfReturnItems { get { return _noOfReturnItems; } set { _noOfReturnItems = value; } }
        public decimal totalDiscountAmount { get { return _totalDiscountAmount; } set { _totalDiscountAmount = value; } }
        public decimal totalUnitCost { get { return _totalUnitCost; } set { _totalUnitCost = value; } }
        public decimal totalAmountExclVat { get { return _totalAmountExclVat; } set { _totalAmountExclVat = value; } }


        public static Hashtable getReceiptData(Database database, DataEntry dataEntry)
        {
            Hashtable receiptTable = new Hashtable();

            bool isVersion2013R2 = CashSite.isVersion2013R2(database);

            if (isVersion2013R2)
            {
                string cashSiteFilter = "";
                if (dataEntry.cashSite != "") cashSiteFilter = "AND c.[POS Store Code] = @cashSite";

                

                DatabaseQuery databaseQuery = database.prepare("SELECT l.[Transaction No_], l.[Quantity], l.[Amount Incl_ VAT], l.[Amount], l.[Line Discount Amount], l.[VAT _], i.[Unit Cost], h.[Registered Date], h.[Registered Time] FROM [" + database.getTableName("POS Transaction Header") + "] h WITH (NOLOCK), [" + database.getTableName("POS Transaction Line") + "] l WITH (NOLOCK), [" + database.getTableName("POS Device") + "] c WITH (NOLOCK), [" + database.getTableName("Item") + "] i WITH (NOLOCK) WHERE l.[Transaction No_] = h.[No_] AND l.[POS Device ID] = h.[POS Device ID] AND h.[POS Device ID] = c.[Code] "+cashSiteFilter+" AND ((h.[Registered Date] >= @fromDate AND h.[Registered Date] <= @toDate) OR (h.[Registered Date] >= @fromDateLastYear AND h.[Registered Date] <= @toDateLastYear)) AND l.[Line Type] = 0 AND l.[Sales Type] = 2 AND l.[Sales No_] = i.[No_] AND l.[Void] = 0 ");
                databaseQuery.addStringParameter("cashSite", dataEntry.cashSite, 20);
                databaseQuery.addDateTimeParameter("fromDate", dataEntry.fromDate);
                databaseQuery.addDateTimeParameter("toDate", dataEntry.toDate);
                databaseQuery.addDateTimeParameter("fromDateLastYear", dataEntry.fromDateLastYear);
                databaseQuery.addDateTimeParameter("toDateLastYear", dataEntry.toDateLastYear);

                SqlDataReader dataReader = databaseQuery.executeQuery();
                while (dataReader.Read())
                {
                    string receiptNo = dataReader.GetValue(0).ToString();

                    DataReceipt dataReceipt = (DataReceipt)receiptTable[receiptNo];
                    if (dataReceipt == null)
                    {
                        dataReceipt = new DataReceipt(database);
                        dataReceipt.no = receiptNo;
                        receiptTable.Add(receiptNo, dataReceipt);
                    }

                    decimal quantity = dataReader.GetDecimal(1);
                    if (dataReader.GetDecimal(5) > 0)
                    {
                        //Endast moms-pliktiga produkter
                        if (quantity > 0) dataReceipt.noOfSalesItems = dataReceipt.noOfSalesItems + quantity;
                        if (quantity < 0) dataReceipt.noOfReturnItems = dataReceipt.noOfReturnItems + (quantity * -1);
                    }
                    if (!dataReader.IsDBNull(2))
                    {
                        if (dataReader.GetDecimal(5) > 0) dataReceipt.totalAmountInclVat = dataReceipt.totalAmountInclVat + dataReader.GetDecimal(2);
                    }
                    if (!dataReader.IsDBNull(3))
                    {
                        if (dataReader.GetDecimal(5) > 0) dataReceipt.totalAmountExclVat = dataReceipt.totalAmountExclVat + dataReader.GetDecimal(3);
                    }
                    if (!dataReader.IsDBNull(4))
                    {
                        decimal vatFactor = (1 + (dataReader.GetDecimal(5) / 100));
                        dataReceipt.totalDiscountAmount = dataReceipt.totalDiscountAmount + (dataReader.GetDecimal(4) * vatFactor);
                    }
                    if (!dataReader.IsDBNull(6))
                    {
                        if (dataReader.GetDecimal(5) > 0) dataReceipt.totalUnitCost = dataReceipt.totalUnitCost + (dataReader.GetDecimal(6) * quantity);
                    }
                    dataReceipt.registeredDate = dataReader.GetDateTime(7);
                    dataReceipt.registeredTime = dataReader.GetDateTime(8);

                    receiptTable[receiptNo] = dataReceipt;
                }

                dataReader.Close();

            }
            else
            {
                string cashSiteFilter = "";
                if (dataEntry.cashSite != "") cashSiteFilter = "AND c.[Cash Site Code] = @cashSite";

                string voucherItemQuery = "";

                DatabaseQuery pluQuery = database.prepare("SELECT [Item No_] FROM [" + database.getTableName("PLU Register Line") + "] WITH (NOLOCK) WHERE [Sub Type] > 0");
                SqlDataReader pluReader = pluQuery.executeQuery();
                while (pluReader.Read())
                {
                    if (voucherItemQuery != "") voucherItemQuery = voucherItemQuery + " AND ";
                    voucherItemQuery = voucherItemQuery + "l.[Sales No_] <> '" + pluReader.GetValue(0).ToString() + "'";
                }
                pluReader.Close();

                if (voucherItemQuery != "")
                {
                    voucherItemQuery = "AND (" + voucherItemQuery + ")";
                }



                DatabaseQuery databaseQuery = database.prepare("SELECT l.[Receipt No_], l.[Quantity], l.[Amount Incl_ VAT], l.[Amount], l.[Line Discount], l.[VAT %], i.[Unit Cost], h.[Registered Date], h.[Registered Time] FROM [" + database.getTableName("Posted Cash Receipt") + "] h WITH (NOLOCK), [" + database.getTableName("Posted Cash Receipt Line") + "] l WITH (NOLOCK), [" + database.getTableName("Cash Register") + "] c WITH (NOLOCK), [" + database.getTableName("Item") + "] i WITH (NOLOCK) WHERE l.[Receipt No_] = h.[No_] AND l.[Cash Register ID] = h.[Cash Register ID] AND h.[Cash Register ID] = c.[Cash Register ID] "+cashSiteFilter+" AND ((h.[Registered Date] >= @fromDate AND h.[Registered Date] <= @toDate) OR (h.[Registered Date] >= @fromDateLastYear AND h.[Registered Date] <= @toDateLastYear)) AND l.[Line Type] = '0' AND l.[Sales Type] > 0 AND l.[Sales No_] = i.[No_] AND l.[Void] = '0' " + voucherItemQuery);

                databaseQuery.addStringParameter("cashSite", dataEntry.cashSite, 20);
                databaseQuery.addDateTimeParameter("fromDate", dataEntry.fromDate);
                databaseQuery.addDateTimeParameter("toDate", dataEntry.toDate);
                databaseQuery.addDateTimeParameter("fromDateLastYear", dataEntry.fromDateLastYear);
                databaseQuery.addDateTimeParameter("toDateLastYear", dataEntry.toDateLastYear);

                SqlDataReader dataReader = databaseQuery.executeQuery();
                while (dataReader.Read())
                {
                    string receiptNo = dataReader.GetValue(0).ToString();

                    DataReceipt dataReceipt = (DataReceipt)receiptTable[receiptNo];
                    if (dataReceipt == null)
                    {
                        dataReceipt = new DataReceipt(database);
                        dataReceipt.no = receiptNo;
                        receiptTable.Add(receiptNo, dataReceipt);
                    }

                    decimal quantity = dataReader.GetDecimal(1);

                    bool include = true;
                    if ((dataReader.GetDecimal(5) <= 0) && (!database.allowZeroVat())) include = false;

                    if (include)
                    {
                        //Endast moms-pliktiga produkter
                        if (quantity > 0) dataReceipt.noOfSalesItems = dataReceipt.noOfSalesItems + quantity;
                        if (quantity < 0) dataReceipt.noOfReturnItems = dataReceipt.noOfReturnItems + (quantity * -1);

                        if (!dataReader.IsDBNull(2))
                        {
                            dataReceipt.totalAmountInclVat = dataReceipt.totalAmountInclVat + dataReader.GetDecimal(2);
                        }
                        if (!dataReader.IsDBNull(3))
                        {
                            dataReceipt.totalAmountExclVat = dataReceipt.totalAmountExclVat + dataReader.GetDecimal(3);
                        }
                        if (!dataReader.IsDBNull(4))
                        {
                            decimal vatFactor = (1 + (dataReader.GetDecimal(5) / 100));
                            dataReceipt.totalDiscountAmount = dataReceipt.totalDiscountAmount + (dataReader.GetDecimal(4) * vatFactor);
                        }
                        if (!dataReader.IsDBNull(6))
                        {
                            dataReceipt.totalUnitCost = dataReceipt.totalUnitCost + (dataReader.GetDecimal(6) * quantity);
                        }
                    }
                    dataReceipt.registeredDate = dataReader.GetDateTime(7);
                    dataReceipt.registeredTime = dataReader.GetDateTime(8);

                    receiptTable[receiptNo] = dataReceipt;
                }

                dataReader.Close();


                databaseQuery = database.prepare("SELECT l.[Receipt No_], l.[Quantity], l.[Amount Incl_ VAT], l.[Amount], l.[Line Discount], l.[VAT %], i.[Unit Cost], h.[Registered Date], h.[Registered Time] FROM [" + database.getTableName("Cash Receipt") + "] h WITH (NOLOCK), [" + database.getTableName("Cash Receipt Line") + "] l WITH (NOLOCK), [" + database.getTableName("Cash Register") + "] c WITH (NOLOCK), [" + database.getTableName("Item") + "] i WITH (NOLOCK) WHERE l.[Receipt No_] = h.[No_] AND l.[Cash Register ID] = h.[Cash Register ID] AND h.[Cash Register ID] = c.[Cash Register ID] "+cashSiteFilter+" AND ((h.[Registered Date] >= @fromDate AND h.[Registered Date] <= @toDate) OR (h.[Registered Date] >= @fromDateLastYear AND h.[Registered Date] <= @toDateLastYear)) AND l.[Line Type] = '0' AND l.[Sales Type] > 0 AND l.[Sales No_] = i.[No_] AND l.[Void] = '0' " + voucherItemQuery);
                databaseQuery.addStringParameter("cashSite", dataEntry.cashSite, 20);
                databaseQuery.addDateTimeParameter("fromDate", dataEntry.fromDate);
                databaseQuery.addDateTimeParameter("toDate", dataEntry.toDate);
                databaseQuery.addDateTimeParameter("fromDateLastYear", dataEntry.fromDateLastYear);
                databaseQuery.addDateTimeParameter("toDateLastYear", dataEntry.toDateLastYear);

                dataReader = databaseQuery.executeQuery();
                while (dataReader.Read())
                {
                    string receiptNo = dataReader.GetValue(0).ToString();

                    DataReceipt dataReceipt = (DataReceipt)receiptTable[receiptNo];
                    if (dataReceipt == null)
                    {
                        dataReceipt = new DataReceipt(database);
                        dataReceipt.no = receiptNo;
                        receiptTable.Add(receiptNo, dataReceipt);
                    }

                    decimal quantity = dataReader.GetDecimal(1);

                    bool include = true;
                    if ((dataReader.GetDecimal(5) <= 0) && (!database.allowZeroVat())) include = false;

                    if (include)
                    {

                        //Endast moms-pliktiga produkter
                        if (quantity > 0) dataReceipt.noOfSalesItems = dataReceipt.noOfSalesItems + quantity;
                        if (quantity < 0) dataReceipt.noOfReturnItems = dataReceipt.noOfReturnItems + (quantity * -1);

                        if (!dataReader.IsDBNull(2))
                        {
                            dataReceipt.totalAmountInclVat = dataReceipt.totalAmountInclVat + dataReader.GetDecimal(2);
                        }
                        if (!dataReader.IsDBNull(3))
                        {
                            dataReceipt.totalAmountExclVat = dataReceipt.totalAmountExclVat + dataReader.GetDecimal(3);
                        }
                        if (!dataReader.IsDBNull(4))
                        {
                            decimal vatFactor = (1 + (dataReader.GetDecimal(5) / 100));
                            dataReceipt.totalDiscountAmount = dataReceipt.totalDiscountAmount + (dataReader.GetDecimal(4) * vatFactor);
                        }
                        if (!dataReader.IsDBNull(6))
                        {
                            dataReceipt.totalUnitCost = dataReceipt.totalUnitCost + (dataReader.GetDecimal(6) * quantity);
                        }
                    }
                    dataReceipt.registeredDate = dataReader.GetDateTime(7);
                    dataReceipt.registeredTime = dataReader.GetDateTime(8);

                    receiptTable[receiptNo] = dataReceipt;
                }

                dataReader.Close();

            }
            return receiptTable;
        }

    }
}
