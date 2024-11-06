using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;

namespace Navipro.Cashjet.Library
{
    public class DataCounter
    {
        private string _cashSiteCode;
        private DateTime _registeredDate;
        private DateTime _timeOfDay;
        private int _quantity;
        private Database _database;

        public DataCounter(Database database)
        {
            _database = database;
        }

        public string cashSiteCode { get { return _cashSiteCode; } set { _cashSiteCode = value; } }
        public DateTime registeredDate { get { return _registeredDate; } set { _registeredDate = value; } }
        public DateTime timeOfDay { get { return _timeOfDay; } set { _timeOfDay = value; } }
        public int quantity { get { return _quantity; } set { _quantity = value; } }


        public static DataCounterCollection getCounterData(Database database, DataEntry dataEntry)
        {
            DataCounterCollection dataCounterCollection = new DataCounterCollection();

            bool isVersion2013R2 = CashSite.isVersion2013R2(database);

            if (!isVersion2013R2)
            {
                DatabaseQuery databaseQuery = database.prepare("SELECT [Cash Site Code_], [Date], [Time of Day], [Quantity] FROM [" + database.getTableName("Cash Site Counter") + "] WITH (NOLOCK) WHERE [Cash Site Code] = @cashSite AND [Date] >= @fromDate AND [Date] <= @toDate");
                databaseQuery.addStringParameter("cashSite", dataEntry.cashSite, 20);
                databaseQuery.addDateTimeParameter("fromDate", dataEntry.fromDate);
                databaseQuery.addDateTimeParameter("toDate", dataEntry.toDate);

                SqlDataReader dataReader = databaseQuery.executeQuery();
                while (dataReader.Read())
                {
                    DataCounter dataCounter = new DataCounter(database);
                    dataCounter.cashSiteCode = dataReader.GetValue(0).ToString();
                    dataCounter.registeredDate = dataReader.GetDateTime(1);
                    dataCounter.timeOfDay = dataReader.GetDateTime(2);
                    dataCounter.quantity = dataReader.GetInt32(3);

                    dataCounterCollection.Add(dataCounter);
                }

                dataReader.Close();
            }
            return dataCounterCollection;
        }
 
    }
}
