using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.Infojet.Lib
{
    /// <summary>
    /// Summary description for Item.
    /// </summary>
    public class ItemCalendarBooking
    {
        private Infojet infojetContext;

        public int entryNo;
        public string itemNo;
        public string variantCode;
        public DateTime entryDate;
        public float quantity;
        public string orderNo;

        public ItemCalendarBooking(Infojet infojetContext, int entryNo)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

            getFromDatabase();
        }

        public ItemCalendarBooking(Infojet infojetContext)
        {
            //
            // TODO: Add constructor logic here
            //
            this.infojetContext = infojetContext;

        }

        private void getFromDatabase()
        {
            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Entry No_], [Item No_], [Variant Code], [Entry Date], [Quantity], [Order No_] FROM [" + infojetContext.systemDatabase.getTableName("Item Calendar Booking") + "] WHERE [Entry No_] = @entryNo");
            databaseQuery.addIntParameter("entryNo", entryNo);
            

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                entryNo = dataReader.GetInt32(0);
                itemNo = dataReader.GetValue(1).ToString();
                variantCode = dataReader.GetValue(2).ToString();
                entryDate = dataReader.GetDateTime(3);
                quantity = float.Parse(dataReader.GetValue(4).ToString());
                orderNo = dataReader.GetValue(5).ToString();

            }

            dataReader.Close();


        }

        public static float calcBookedQuantity(Infojet infojetContext, string itemNo, string variantCode, DateTime date)
        {
            float quantity = 0;
            DateTime currentDate = new DateTime(date.Year, date.Month, date.Day);

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT SUM([Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Item Calendar Booking") + "] WHERE [Item No_] = @itemNo AND [Variant Code] = @variantCode AND [Entry Date] = @entryDate");
            databaseQuery.addStringParameter("itemNo", itemNo, 20);
            databaseQuery.addStringParameter("variantCode", variantCode, 20);
            databaseQuery.addDateTimeParameter("entryDate", currentDate);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            if (dataReader.Read())
            {
                if (!dataReader.IsDBNull(0)) quantity = float.Parse(dataReader.GetValue(0).ToString());
            }
            dataReader.Close();


            return quantity;
        }

        public static ItemCalendarBookingCollection getCalendarBookingDates(Infojet infojetContext, string itemNo, string variantCode)
        {
            ItemCalendarBookingCollection itemCalendarBookingCollection = new ItemCalendarBookingCollection();

            DatabaseQuery databaseQuery = infojetContext.systemDatabase.prepare("SELECT [Entry Date], SUM([Quantity]) FROM [" + infojetContext.systemDatabase.getTableName("Item Calendar Booking") + "] WHERE [Item No_] = @itemNo AND [Variant Code] = @variantCode AND [Entry Date] > GETDATE() GROUP BY [Entry Date] ORDER BY [Entry Date]");
            databaseQuery.addStringParameter("itemNo", itemNo, 20);
            databaseQuery.addStringParameter("variantCode", variantCode, 20);

            SqlDataReader dataReader = databaseQuery.executeQuery();
            while (dataReader.Read())
            {
                ItemCalendarBooking itemCalendarBooking = new ItemCalendarBooking(infojetContext);
                itemCalendarBooking.entryDate = dataReader.GetDateTime(0);
                if (!dataReader.IsDBNull(1)) itemCalendarBooking.quantity = float.Parse(dataReader.GetValue(1).ToString());

                itemCalendarBookingCollection.Add(itemCalendarBooking);


            }
            dataReader.Close();

            return itemCalendarBookingCollection;
        }
    }
}
