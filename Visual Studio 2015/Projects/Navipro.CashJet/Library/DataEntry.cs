using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;

namespace Navipro.Cashjet.Library
{
    public class DataEntry
    {
        private string _title;
        private string _cashSite;
        private DateTime _fromDate;
        private DateTime _toDate;
        private DateTime _fromDateLastYear;
        private DateTime _toDateLastYear;
        private DateTime _lastPostedJournalEndDate;
        private int _interval;
        private decimal _turnOver;
        private decimal _budget;
        private decimal _turnOverLastYear;
        private decimal _noOfReceipts;
        private decimal _noOfReceiptsLastYear;
        private decimal _noOfItems;
        private decimal _noOfItemsLastYear;
        private decimal _noOfReturnItems;
        private decimal _noOfReturnItemsLastYear;
        private decimal _totalDiscountAmount;
        private decimal _totalUnitCost;
        private decimal _profitPercent;
        private decimal _totalAmountExclVat;
        private Hashtable _receiptTable;
        private Database _database;
        private int _noOfVisitors;

        public DataEntry(Database database)
        {
            _database = database;
        }

        public string cashSite { get { return _cashSite; } set { _cashSite = value; } }
        public string title { get { return _title; } set { _title = value; } }
        public DateTime fromDate { get { return _fromDate; } set { _fromDate = value; } }
        public DateTime toDate { get { return _toDate; } set { _toDate = value; } }
        public DateTime fromDateLastYear { get { return _fromDateLastYear; } set { _fromDateLastYear = value; } }
        public DateTime toDateLastYear { get { return _toDateLastYear; } set { _toDateLastYear = value; } }
        public DateTime lastPostedJournalEndDate { get { return _lastPostedJournalEndDate; } set { _lastPostedJournalEndDate = value; } }
        public decimal turnOver { get { return _turnOver; } set { _turnOver = value; } }
        public decimal budget { get { return _budget; } set { _budget = value; } }
        public decimal turnOverLastYear { get { return _turnOverLastYear; } set { _turnOverLastYear = value; } }
        public decimal noOfReceipts { get { return _noOfReceipts; } set { _noOfReceipts = value; } }
        public decimal noOfReceiptsLastYear { get { return _noOfReceiptsLastYear; } set { _noOfReceiptsLastYear = value; } }
        public decimal noOfItems { get { return _noOfItems; } set { _noOfItems = value; } }
        public decimal noOfItemsLastYear { get { return _noOfItemsLastYear; } set { _noOfItemsLastYear = value; } }
        public decimal noOfReturnItems { get { return _noOfReturnItems; } set { _noOfReturnItems = value; } }
        public decimal noOfReturnItemsLastYear { get { return _noOfReturnItemsLastYear; } set { _noOfReturnItemsLastYear = value; } }
        public decimal totalDiscountAmount { get { return _totalDiscountAmount; } set { _totalDiscountAmount = value; } }
        public int noOfVisitors { get { return _noOfVisitors; } set { _noOfVisitors = value; } }
        public int interval { get { return _interval; } set { _interval = value; } }
        public Hashtable receiptTable { get { return _receiptTable; } }
        public decimal averageReceiptQuantity 
        { 
            get 
            {
                if (_noOfReceipts > 0) return (_noOfItems / _noOfReceipts);
                return 0;
            }
        }
        public decimal averageReceiptQuantityLastYear
        {
            get
            {
                if (_noOfReceiptsLastYear > 0) return (_noOfItemsLastYear / _noOfReceiptsLastYear);
                return 0;
            }
        }
        public int averageReceiptAmount
        {
            get
            {
                if (_noOfReceipts > 0) return (int)(_turnOver / _noOfReceipts);
                return 0;
            }
        }
        public int averageReceiptAmountLastYear
        {
            get
            {
                if (_noOfReceiptsLastYear > 0) return (int)(_turnOverLastYear / _noOfReceiptsLastYear);
                return 0;
            }
        }

        public int profitPercent
        {
            get
            {
                if (_totalAmountExclVat > 0) return (int)((profitAmount / _totalAmountExclVat) * 100);
                return 0;
            }
        }

        public decimal profitAmount
        {
            get
            {
                return _totalAmountExclVat - _totalUnitCost;
            }
        }

        public decimal totalUnitCost
        {
            get
            {
                return _totalUnitCost;
            }
        }

        public decimal profitAmountPerReceipt
        {
            get
            {
                if (_noOfReceipts > 0) return profitAmount / _noOfReceipts;
                return 0;
            }
        }


        public string turnOverImg { get { return getTrendImg(_budget, _turnOver, 2); } }
        public string noOfReceiptsImg { get { return getTrendImg(_noOfReceiptsLastYear, _noOfReceipts, 2); } }
        public string noOfItemsImg { get { return getTrendImg(_noOfItemsLastYear, _noOfItems, 2); } }
        public string averageReceiptQuantityImg { get { return getTrendImg(averageReceiptQuantityLastYear, averageReceiptQuantity, 2); } }
        public string averageReceiptAmountImg { get { return getTrendImg(averageReceiptAmountLastYear, averageReceiptAmount, 2); } }


        public DataHourCollection dataHourCollection { get { return DataHourEntry.getCollection(_database, this); } }
        public DataProductGroupCollection dataProductGroupCollection
        {
            get
            {
                return DataProductGroupEntry.getCollection(_database, this, 0);
            }
        }
        public DataProductCollection dataProductCollection { get { return DataProductEntry.getCollection(_database, this, 0); } }

        public DataProductGroupCollection dataProductGroupTurnOverCollection { get {
            return DataProductGroupEntry.getCollection(_database, this, 1);
        } }
        public DataProductCollection dataProductTurnOverCollection { get { return DataProductEntry.getCollection(_database, this, 1); } }

        public DataGenProdPostingGroupCollection dataGenProdPostingGroupCollection { get { return DataGenProdPostingGroupEntry.getCollection(_database, this, 0); } }


        public static DataCollection getCollection(Database database, string cashSite, DateTime dateTime, int interval, bool includeVat)
        {
            return getCollection(database, cashSite, dateTime, interval, dateTime, includeVat);
        }

        public static DataCollection getCollection(Database database, string cashSite, DateTime dateTime, int interval, DateTime endDateTime, bool includeVat)
        {

            DataCollection dataCollection = new DataCollection();
            if (cashSite != "-") createDateEntries(database, ref dataCollection, dateTime, interval, endDateTime, cashSite);
            if (cashSite == "-") createCashSiteEntries(database, ref dataCollection, dateTime, interval, endDateTime, CashSite.getCollection(database));
           
            int i = 0;
            while (i < dataCollection.Count)
            {
                DataEntry dataEntry = dataCollection[i];
                calculate(database, ref dataEntry, includeVat);

                
                dataCollection[i] = dataEntry;
                i++;
            }

            createTotals(ref dataCollection, database, includeVat, cashSite);

            return dataCollection;
        }

 
        private static void calculate(Database database, ref DataEntry dataEntry, bool includeVat)
        {
            bool isVersion2013R2 = CashSite.isVersion2013R2(database);

            dataEntry._receiptTable = DataReceipt.getReceiptData(database, dataEntry);
            IEnumerator enumerator = dataEntry._receiptTable.Values.GetEnumerator();
            //throw new Exception("COunt: "+dataEntry._receiptTable.Values.Count);
            while (enumerator.MoveNext())
            {
                DataReceipt dataReceipt = (DataReceipt)enumerator.Current;

                if ((dataReceipt.registeredDate >= dataEntry.fromDate) && (dataReceipt.registeredDate <= dataEntry.toDate))
                {
                    if ((dataReceipt.noOfSalesItems > 0) || (dataReceipt.noOfReturnItems > 0))
                    {
                        dataEntry.noOfReceipts = dataEntry.noOfReceipts + 1;
                    }

                    if (includeVat)
                    {
                        dataEntry.turnOver = dataEntry.turnOver + dataReceipt.totalAmountInclVat;
                    }
                    else
                    {
                        dataEntry.turnOver = dataEntry.turnOver + dataReceipt.totalAmountExclVat;
                    }
                    dataEntry.noOfItems = dataEntry.noOfItems + dataReceipt.noOfSalesItems;
                    dataEntry.totalDiscountAmount = dataEntry.totalDiscountAmount + dataReceipt.totalDiscountAmount;
                    dataEntry._totalAmountExclVat = dataEntry._totalAmountExclVat + dataReceipt.totalAmountExclVat;
                    dataEntry._totalUnitCost = dataEntry._totalUnitCost + dataReceipt.totalUnitCost;
                    dataEntry.noOfReturnItems = dataEntry.noOfReturnItems + dataReceipt.noOfReturnItems;

                }
                if ((dataReceipt.registeredDate >= dataEntry.fromDateLastYear) && (dataReceipt.registeredDate <= dataEntry.toDateLastYear))
                {
                    if ((dataReceipt.noOfSalesItems > 0) || (dataReceipt.noOfReturnItems > 0))
                    {
                        dataEntry.noOfReceiptsLastYear = dataEntry.noOfReceiptsLastYear + 1;
                    }

                    if (includeVat)
                    {
                        dataEntry.turnOverLastYear = dataEntry.turnOverLastYear + dataReceipt.totalAmountInclVat;
                    }
                    else
                    {
                        dataEntry.turnOverLastYear = dataEntry.turnOverLastYear + dataReceipt.totalAmountExclVat;
                    }

                    dataEntry.noOfItemsLastYear = dataEntry.noOfItemsLastYear + dataReceipt.noOfSalesItems;
                    dataEntry.noOfReturnItemsLastYear = dataEntry.noOfReturnItemsLastYear + dataReceipt.noOfReturnItems;

                }
                
            }

            if (isVersion2013R2)
            {
                DatabaseQuery budgetVatQuery = database.prepare("SELECT [Budget Incl_ VAT], [Budget VAT _] FROM [" + database.getTableName("POS Store") + "] WITH (NOLOCK) WHERE [Code] = @cashSite");
                budgetVatQuery.addStringParameter("cashSite", dataEntry.cashSite, 20);

                bool budgetInclVat = false;
                decimal budgetVat = 0;
                SqlDataReader dataReader = budgetVatQuery.executeQuery();
                if (dataReader.Read())
                {
                    if (dataReader.GetValue(0).ToString() == "1") budgetInclVat = true;
                    budgetVat = dataReader.GetDecimal(1);
                }
                dataReader.Close();

                DatabaseQuery budgetQuery = database.prepare("SELECT SUM([Budget Amount]) FROM [" + database.getTableName("POS Budget Entry") + "] WITH (NOLOCK) WHERE [Type] = 1 AND [POS Store Code] = @cashSite AND [Date] >= @fromDate AND [Date] <= @toDate");
                budgetQuery.addStringParameter("cashSite", dataEntry.cashSite, 20);
                budgetQuery.addDateTimeParameter("fromDate", dataEntry.fromDate);
                budgetQuery.addDateTimeParameter("toDate", dataEntry.toDate);

                dataReader = budgetQuery.executeQuery();
                if (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0)) dataEntry.budget = dataReader.GetDecimal(0);
                    if ((includeVat) && (!budgetInclVat))
                    {
                        dataEntry.budget = dataEntry.budget * ((budgetVat / 100) + 1);
                    }
                    if ((!includeVat) && (budgetInclVat))
                    {
                        dataEntry.budget = dataEntry.budget / ((budgetVat / 100) + 1);
                    }
                }
                dataReader.Close();


                DatabaseQuery journalQuery = database.prepare("SELECT [Ending Date] FROM [" + database.getTableName("POS Transaction Journal") + "] j, [" + database.getTableName("POS Device") + "] c WITH (NOLOCK) WHERE c.[POS Store Code] = @cashSite AND c.[Code] = j.[POS Device ID] AND [Posted] = '1' ORDER BY [Ending Date] DESC");
                journalQuery.addStringParameter("cashSite", dataEntry.cashSite, 20);

                dataReader = journalQuery.executeQuery();
                if (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0)) dataEntry.lastPostedJournalEndDate = dataReader.GetDateTime(0);
                }
                dataReader.Close();

            }
            else
            {
                DatabaseQuery budgetVatQuery = database.prepare("SELECT [Budget Incl_ VAT], [Budget VAT %] FROM [" + database.getTableName("Cash Site") + "] WITH (NOLOCK) WHERE [Code] = @cashSite");
                budgetVatQuery.addStringParameter("cashSite", dataEntry.cashSite, 20);

                bool budgetInclVat = false;
                decimal budgetVat = 0;
                SqlDataReader dataReader = budgetVatQuery.executeQuery();
                if (dataReader.Read())
                {
                    if (dataReader.GetValue(0).ToString() == "1") budgetInclVat = true;
                    budgetVat = dataReader.GetDecimal(1);
                }
                dataReader.Close();

                DatabaseQuery budgetQuery = database.prepare("SELECT SUM([Budget Amount]) FROM [" + database.getTableName("Cash Budget Line") + "] WITH (NOLOCK) WHERE [Cash Site Code] = @cashSite AND [Date] >= @fromDate AND [Date] <= @toDate");
                budgetQuery.addStringParameter("cashSite", dataEntry.cashSite, 20);
                budgetQuery.addDateTimeParameter("fromDate", dataEntry.fromDate);
                budgetQuery.addDateTimeParameter("toDate", dataEntry.toDate);

                dataReader = budgetQuery.executeQuery();
                if (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0)) dataEntry.budget = dataReader.GetDecimal(0);
                    if ((includeVat) && (!budgetInclVat))
                    {
                        dataEntry.budget = dataEntry.budget * ((budgetVat / 100) + 1);
                    }
                    if ((!includeVat) && (budgetInclVat))
                    {
                        dataEntry.budget = dataEntry.budget / ((budgetVat / 100) + 1);
                    }
                }
                dataReader.Close();

                DatabaseQuery counterQuery = database.prepare("SELECT SUM([Quantity]) FROM [" + database.getTableName("Cash Site Counter") + "] WITH (NOLOCK) WHERE [Cash Site Code] = @cashSite AND [Date] >= @fromDate AND [Date] <= @toDate");
                counterQuery.addStringParameter("cashSite", dataEntry.cashSite, 20);
                counterQuery.addDateTimeParameter("fromDate", dataEntry.fromDate);
                counterQuery.addDateTimeParameter("toDate", dataEntry.toDate);

                double quantity = 0;
                dataReader = counterQuery.executeQuery();
                if (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0)) quantity = double.Parse(dataReader.GetValue(0).ToString());
                }
                dataReader.Close();

                quantity = (quantity / 2);// *0.9;

                dataEntry.noOfVisitors = (int)quantity;


                DatabaseQuery journalQuery = database.prepare("SELECT [Ending Date] FROM [" + database.getTableName("Posted Cash Receipt Journal") + "] j, [" + database.getTableName("Cash Register") + "] c WITH (NOLOCK) WHERE c.[Cash Site Code] = @cashSite AND c.[Cash Register ID] = j.[Cash Register ID] AND [Posted] = '1' ORDER BY [Ending Date] DESC");
                journalQuery.addStringParameter("cashSite", dataEntry.cashSite, 20);

                dataReader = journalQuery.executeQuery();
                if (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0)) dataEntry.lastPostedJournalEndDate = dataReader.GetDateTime(0);
                }
                dataReader.Close();

            }


        }


        private static void createDateEntries(Database database, ref DataCollection dataCollection, DateTime dateTime, int interval, DateTime endDateTime, string cashSite)
        {
            CalendarHelper.SetWeekAdjustment(database.configuration.weekAdjustment);

            if (interval == 0)
            {
                int i = 0;
                while (i < 7)
                {
                    DateTime fromDate = dateTime.AddDays(i*-1);

                    DataEntry dataEntry = new DataEntry(database);
                    dataEntry.interval = interval;
                    dataEntry.cashSite = cashSite;
                    dataEntry.fromDate = fromDate;
                    dataEntry.toDate = fromDate;
                    dataEntry.fromDateLastYear = CalendarHelper.getLastYearDate(fromDate);
                    dataEntry.toDateLastYear = dataEntry.fromDateLastYear;
                    dataEntry.title = fromDate.ToString("yyyy-MM-dd");
                    dataCollection.Add(dataEntry);
                    i++;
                }
            }

            if (interval == 1)
            {
                int weekNo = CalendarHelper.GetWeek(dateTime);

                int i = 0;
                while (i < 7)
                {
                    int targetWeekNo = weekNo - i;
                    int year = dateTime.Year;
                    if (targetWeekNo < 1)
                    {
                        targetWeekNo = 52 + targetWeekNo;
                        year = year - 1;
                    }

                    DateTime fromDate = CalendarHelper.GetFirstDayOfWeek(year, targetWeekNo);
                    DateTime toDate = fromDate.AddDays(6);
                    
                    DataEntry dataEntry = new DataEntry(database);
                    dataEntry.interval = interval;
                    dataEntry.cashSite = cashSite;
                    dataEntry.fromDate = fromDate;
                    dataEntry.toDate = toDate;
                    dataEntry.fromDateLastYear = CalendarHelper.getLastYearDate(fromDate);
                    dataEntry.toDateLastYear = CalendarHelper.getLastYearDate(toDate);
                    dataEntry.title = "Vecka "+(targetWeekNo);
                    dataCollection.Add(dataEntry);
                    i++;
                }


            }

            if (interval == 2)
            {
                int i = 0;
                while (i < 12)
                {
                    DateTime fromDate = new DateTime(dateTime.AddMonths(i*-1).Year, dateTime.AddMonths(i*-1).Month, 1);
                    DateTime toDate = fromDate.AddMonths(1).AddDays(-1);

                    DataEntry dataEntry = new DataEntry(database);
                    dataEntry.interval = interval;
                    dataEntry.cashSite = cashSite;
                    dataEntry.fromDate = fromDate;
                    dataEntry.toDate = toDate;
                    dataEntry.fromDateLastYear = dataEntry.fromDate.AddYears(-1);
                    dataEntry.toDateLastYear = dataEntry.toDate.AddYears(-1);
                    dataEntry.title = fromDate.ToString("yyyy-MM");
                    dataCollection.Add(dataEntry);

                    i++;
                }
            }

            if (interval == 3)
            {
                int i = 0;
                while (i < 5)
                {
                    DateTime fromDate = new DateTime(dateTime.AddYears(i*-1).Year, 1, 1);
                    DateTime toDate = fromDate.AddYears(1).AddDays(-1);

                    DataEntry dataEntry = new DataEntry(database);
                    dataEntry.interval = interval;
                    dataEntry.cashSite = cashSite;
                    dataEntry.fromDate = fromDate;
                    dataEntry.toDate = toDate;
                    dataEntry.fromDateLastYear = dataEntry.fromDate.AddYears(-1);
                    dataEntry.toDateLastYear = dataEntry.toDate.AddYears(-1);
                    dataEntry.title = "År "+fromDate.ToString("yyyy");
                    dataCollection.Add(dataEntry);

                    i++;
                }
            }

            if (interval == 4)
            {
                DateTime fromDate = dateTime;

                while (fromDate <= endDateTime)
                {
                    DataEntry dataEntry = new DataEntry(database);
                    dataEntry.interval = interval;
                    dataEntry.cashSite = cashSite;
                    dataEntry.fromDate = fromDate;
                    dataEntry.toDate = fromDate;
                    dataEntry.fromDateLastYear = CalendarHelper.getLastYearDate(fromDate);
                    dataEntry.toDateLastYear = dataEntry.fromDateLastYear;
                    dataEntry.title = fromDate.ToString("yyyy-MM-dd");
                    dataCollection.Add(dataEntry);

                    fromDate = fromDate.AddDays(1);

                }
            }

        }
        
        private static void createCashSiteEntries(Database database, ref DataCollection dataCollection, DateTime dateTime, int interval, DateTime endDateTime, CashSiteCollection cashSiteCollection)
        {
            int i = 0;
            while (i < cashSiteCollection.Count)
            {
                DataEntry dataEntry = new DataEntry(database);
                dataEntry.cashSite = cashSiteCollection[i].code;
                dataEntry.title = cashSiteCollection[i].description;

                if (interval == 0)
                {
                    DateTime fromDate = dateTime;

                    dataEntry.fromDate = fromDate;
                    dataEntry.toDate = fromDate;
                    dataEntry.fromDateLastYear = CalendarHelper.getLastYearDate(fromDate);
                    dataEntry.toDateLastYear = dataEntry.fromDateLastYear;

                }

                if (interval == 1)
                {
                    int weekNo = CalendarHelper.GetWeek(dateTime);

                    DateTime fromDate = CalendarHelper.GetFirstDayOfWeek(dateTime.Year, weekNo);
                    DateTime toDate = fromDate.AddDays(6);

                    dataEntry.fromDate = fromDate;
                    dataEntry.toDate = toDate;
                    dataEntry.fromDateLastYear = CalendarHelper.getLastYearDate(fromDate);
                    dataEntry.toDateLastYear = CalendarHelper.getLastYearDate(toDate);
                }

                if (interval == 2)
                {
                    DateTime fromDate = new DateTime(dateTime.Year, dateTime.Month, 1);
                    DateTime toDate = fromDate.AddMonths(1).AddDays(-1);

                    dataEntry.fromDate = fromDate;
                    dataEntry.toDate = toDate;
                    dataEntry.fromDateLastYear = fromDate.AddYears(-1);
                    dataEntry.toDateLastYear = toDate.AddYears(-1);

                }

                if (interval == 3)
                {
                    DateTime fromDate = new DateTime(dateTime.Year, 1, 1);
                    DateTime toDate = fromDate.AddYears(1).AddDays(-1);

                    dataEntry.fromDate = fromDate;
                    dataEntry.toDate = toDate;
                    dataEntry.fromDateLastYear = fromDate.AddYears(-1);
                    dataEntry.toDateLastYear = toDate.AddYears(-1);

                }

                if (interval == 4)
                {
                    DateTime fromDate = dateTime;

                    dataEntry.fromDate = fromDate;
                    dataEntry.toDate = endDateTime;
                    dataEntry.fromDateLastYear = CalendarHelper.getLastYearDate(fromDate);
                    dataEntry.toDateLastYear = CalendarHelper.getLastYearDate(endDateTime);

                }

                dataCollection.Add(dataEntry);

                i++;
            }


        }

        private static void createTotals(ref DataCollection dataCollection, Database database, bool includeVat, string cashSite)
        {
            DataEntry dataEntry = new DataEntry(database);
            dataEntry.cashSite = cashSite;
            dataEntry.title = "Total";

            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.MinValue;
            DateTime lastPostedJournalEndDate = DateTime.Today;

            int i = 0;
            bool totalsAlreadyExists = false;

            while (i < dataCollection.Count)
            {
                if (dataCollection[i].title == "Total") totalsAlreadyExists = true;

                if (endDate == DateTime.MinValue) endDate = dataCollection[i].toDate;
                if (dataCollection[i].fromDate < startDate) startDate = dataCollection[i].fromDate;
                if (dataCollection[i].toDate > endDate) endDate = dataCollection[i].toDate;
                if (dataCollection[i].lastPostedJournalEndDate < lastPostedJournalEndDate) lastPostedJournalEndDate = dataCollection[i].lastPostedJournalEndDate;
                
                dataEntry.budget = dataEntry.budget + dataCollection[i].budget;
                dataEntry.turnOver = dataEntry.turnOver + dataCollection[i].turnOver;
                dataEntry.turnOverLastYear = dataEntry.turnOverLastYear + dataCollection[i].turnOverLastYear;
                dataEntry.noOfVisitors = dataEntry.noOfVisitors + dataCollection[i].noOfVisitors;
                dataEntry.noOfReceipts = dataEntry.noOfReceipts + dataCollection[i].noOfReceipts;
                dataEntry.noOfReceiptsLastYear = dataEntry.noOfReceiptsLastYear + dataCollection[i].noOfReceiptsLastYear;
                dataEntry.noOfItems = dataEntry.noOfItems + dataCollection[i].noOfItems;
                dataEntry.noOfItemsLastYear = dataEntry.noOfItemsLastYear + dataCollection[i].noOfItemsLastYear;
                dataEntry.noOfReturnItems = dataEntry.noOfReturnItems + dataCollection[i].noOfReturnItems;
                dataEntry.noOfReturnItemsLastYear = dataEntry.noOfReturnItemsLastYear + dataCollection[i].noOfReturnItemsLastYear;
                dataEntry._totalUnitCost = dataEntry._totalUnitCost + dataCollection[i]._totalUnitCost;
                dataEntry.totalDiscountAmount = dataEntry.totalDiscountAmount + dataCollection[i].totalDiscountAmount;
                dataEntry._totalAmountExclVat = dataEntry._totalAmountExclVat + dataCollection[i]._totalAmountExclVat;

                i++;
            }

            dataEntry.fromDate = startDate;
            dataEntry.toDate = endDate;
            dataEntry.fromDateLastYear = startDate;
            dataEntry.toDateLastYear = endDate;

            dataEntry.lastPostedJournalEndDate = lastPostedJournalEndDate;
            dataEntry._receiptTable = DataReceipt.getReceiptData(database, dataEntry);

            //calculate(database, ref dataEntry, includeVat);

            if (!totalsAlreadyExists) dataCollection.Add(dataEntry);




        }

        private string getTrendImg(decimal oldValue, decimal newValue, int marginal)
        {
            if (oldValue > 0)
            {
                decimal procent = ((newValue - oldValue) / oldValue) * 100;
                if ((procent <= marginal) && (procent >= (marginal*-1))) return "trend_yellow.jpg";
            }
            if (newValue >= oldValue) return "trend_green.jpg";
            if (newValue < oldValue) return "trend_red.jpg";
            return "";
        }
    }
}
