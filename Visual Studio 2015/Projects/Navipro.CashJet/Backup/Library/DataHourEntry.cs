using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;

namespace Navipro.Cashjet.Library
{
    public class DataHourEntry
    {
        private int _hour;
        private int _count;
        private decimal _turnOver;

        public int hour { get { return _hour; } set { _hour = value; } }
        public int count { get { return _count; } set { _count = value; } }
        public decimal turnOver { get { return _turnOver; } set { _turnOver = value; } }


        public static DataHourCollection getCollection(Database database, DataEntry dataEntry)
        {
            DataHourCollection dataHourCollection = new DataHourCollection();
            /*
            DatabaseQuery databaseQuery = database.prepare("SELECT COUNT(*), SUM([Amount Incl_ VAT]), case when datepart(mi,h.[Registered Time]) < 30 then dateadd(hh, datediff(hh, 0, h.[Registered Time])+0, 0) else dateadd(mi,60,dateadd(hh, datediff(hh, 0, h.[Registered Time])+0, 0)) end FROM ["+database.getTableName("Posted Cash Receipt")+"] h, ["+database.getTableName("Posted Cash Receipt Line")+"] l WHERE h.[Registered Date] >= @fromDate AND h.[Registered Date] <= @toDate AND h.[Cash Register ID] IN (SELECT [Cash Register ID] FROM ["+database.getTableName("Cash Register")+"] WHERE [Cash Site Code] = @cashSite) AND l.[Receipt No_] = h.[No_] AND l.[Cash Register ID] = h.[Cash Register ID] AND l.[Line Type] = 0 GROUP BY case when datepart(mi,h.[Registered Time]) < 30 then dateadd(hh, datediff(hh, 0, h.[Registered Time])+0, 0) else dateadd(mi,60,dateadd(hh, datediff(hh, 0, h.[Registered Time])+0, 0)) end ORDER BY case when datepart(mi,h.[Registered Time]) < 30 then dateadd(hh, datediff(hh, 0, h.[Registered Time])+0, 0) else dateadd(mi,60,dateadd(hh, datediff(hh, 0, h.[Registered Time])+0, 0)) end");
            databaseQuery.addDateTimeParameter("fromDate", dataEntry.fromDate);
            databaseQuery.addDateTimeParameter("toDate", dataEntry.toDate);
            databaseQuery.addStringParameter("cashSite", dataEntry.cashSite, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                DataHourEntry dataHourEntry = new DataHourEntry();
                dataHourEntry.hour = dataReader.GetDateTime(2).Hour;
                dataHourEntry.count = int.Parse(dataReader.GetValue(0).ToString());
                dataHourEntry.turnOver = dataReader.GetDecimal(1);
                dataHourCollection.Add(dataHourEntry);

            }
            dataReader.Close();

            databaseQuery = database.prepare("SELECT COUNT(*), SUM([Amount Incl_ VAT]), case when datepart(mi,h.[Registered Time]) < 30 then dateadd(hh, datediff(hh, 0, h.[Registered Time])+0, 0) else dateadd(mi,60,dateadd(hh, datediff(hh, 0, h.[Registered Time])+0, 0)) end FROM [" + database.getTableName("Cash Receipt") + "] h, [" + database.getTableName("Cash Receipt Line") + "] l WHERE h.[Registered Date] >= @fromDate AND h.[Registered Date] <= @toDate AND h.[Cash Register ID] IN (SELECT [Cash Register ID] FROM [" + database.getTableName("Cash Register") + "] WHERE [Cash Site Code] = @cashSite) AND l.[Receipt No_] = h.[No_] AND l.[Cash Register ID] = h.[Cash Register ID] AND l.[Line Type] = 0 GROUP BY case when datepart(mi,h.[Registered Time]) < 30 then dateadd(hh, datediff(hh, 0, h.[Registered Time])+0, 0) else dateadd(mi,60,dateadd(hh, datediff(hh, 0, h.[Registered Time])+0, 0)) end ORDER BY case when datepart(mi,h.[Registered Time]) < 30 then dateadd(hh, datediff(hh, 0, h.[Registered Time])+0, 0) else dateadd(mi,60,dateadd(hh, datediff(hh, 0, h.[Registered Time])+0, 0)) end");
            databaseQuery.addDateTimeParameter("fromDate", dataEntry.fromDate);
            databaseQuery.addDateTimeParameter("toDate", dataEntry.toDate);
            databaseQuery.addStringParameter("cashSite", dataEntry.cashSite, 20);

            dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                bool found = false;
                int i = 0;
                while (i < dataHourCollection.Count)
                {
                    DataHourEntry dataHourEntry = dataHourCollection[i];
                    if (dataHourEntry.hour == dataReader.GetDateTime(2).Hour)
                    {
                        dataHourEntry.count = dataHourEntry.count + int.Parse(dataReader.GetValue(0).ToString());
                        dataHourEntry.turnOver = dataHourEntry.turnOver + dataReader.GetDecimal(1);
                        found = true;
                    }
                    dataHourCollection[i] = dataHourEntry;

                    i++;
                }
                if (!found)
                {
                    DataHourEntry dataHourEntry = new DataHourEntry();
                    dataHourEntry.hour = dataReader.GetDateTime(2).Hour;
                    dataHourEntry.count = int.Parse(dataReader.GetValue(0).ToString());
                    dataHourEntry.turnOver = dataReader.GetDecimal(1);
                    dataHourCollection.Add(dataHourEntry);
                }
            }
            dataReader.Close();
            */

            if (dataEntry.receiptTable != null)
            {
                Hashtable hourTable = new Hashtable();

                IEnumerator enumerator = dataEntry.receiptTable.Values.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    DataReceipt dataReceipt = (DataReceipt)enumerator.Current;

                    if ((dataReceipt.registeredDate >= dataEntry.fromDate) && (dataReceipt.registeredDate <= dataEntry.toDate))
                    {
                        int hour = dataReceipt.registeredTime.Hour;
                        DataHourEntry dataHourEntry = (DataHourEntry)hourTable[hour];
                        if (dataHourEntry == null)
                        {
                            dataHourEntry = new DataHourEntry();
                            dataHourEntry.hour = hour;
                            hourTable.Add(hour, dataHourEntry);
                        }
                        dataHourEntry.count = dataHourEntry.count + 1;
                        hourTable[hour] = dataHourEntry;
                    }
                }
                enumerator = hourTable.Values.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    DataHourEntry dataHourEntry = (DataHourEntry)enumerator.Current;

                    dataHourCollection.Add(dataHourEntry);
                }
            }

            return dataHourCollection;
        }
    }

    
}
