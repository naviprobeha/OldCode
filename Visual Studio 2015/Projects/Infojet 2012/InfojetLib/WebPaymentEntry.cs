using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navipro.Infojet.Lib
{
    public class WebPaymentEntry
    {

        public static void create(Infojet infojetContext, string paymentReferenceNo, string orderNo, float amount, string currencyCode)
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("INSERT INTO [" + infojetContext.systemDatabase.getTableName("Web Payment Entry") + "] ([Date], [Time Of Day], [Payment Reference No_], [Order No_], [Amount], [Currency Code]) VALUES (@date, @timeOfDay, @paymentReferenceNo, @orderNo, @amount, @currencyCode)");
            databaseQuery.addStringParameter("paymentReferenceNo", paymentReferenceNo, 50);
            databaseQuery.addStringParameter("orderNo", orderNo, 20);
            databaseQuery.addDecimalParameter("amount", amount);
            databaseQuery.addStringParameter("currencyCode", currencyCode, 20);
            databaseQuery.addDateTimeParameter("date", DateTime.Today);
            databaseQuery.addDateTimeParameter("timeOfDay", new DateTime(1754, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
            databaseQuery.execute();

        }
    }
}
