using System;
using System.Data;
using System.Data.SqlClient;


namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Organizations.
	/// </summary>
	public class ShippingCustomerSchedules
	{
		public ShippingCustomerSchedules()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public DataSet getShippingCustomerDataSet(Database database, string shippingCustomerNo)
		{
			SqlDataAdapter adapter = database.dataAdapterQuery("SELECT [Shipping Customer No], [Entry No], [Type], [Mondays], [Tuesdays], [Wednesdays], [Thursdays], [Fridays], [Saturdays], [Sundays], [Week], [Quantity], [Time] FROM [Shipping Customer Schedule] WHERE [Shipping Customer No] = '"+shippingCustomerNo+"'");

			DataSet dataSet = new DataSet();
			adapter.Fill(dataSet, "shippingCustomerSchedule");
			adapter.Dispose();

			return dataSet;

		}

		public ShippingCustomerSchedule getEntry(Database database, string shippingCustomerNo, int entryNo)
		{
			ShippingCustomerSchedule shippingCustomerSchedule = null;

			SqlDataReader dataReader = database.query("SELECT [Shipping Customer No], [Entry No], [Type], [Mondays], [Tuesdays], [Wednesdays], [Thursdays], [Fridays], [Saturdays], [Sundays], [Week], [Quantity], [Time] FROM [Shipping Customer Schedule] WHERE [Shipping Customer No] = '"+shippingCustomerNo+"' AND [Entry No] = '"+entryNo+"'");
			if (dataReader.Read())
			{
				shippingCustomerSchedule = new ShippingCustomerSchedule(dataReader);

			}

			dataReader.Close();

			return shippingCustomerSchedule;

		}

		public bool checkSchedule(Database database, string shippingCustomerNo, DateTime date)
		{
			string weekDayQuery = "";
			if (date.DayOfWeek == DayOfWeek.Monday) weekDayQuery = "[Mondays] = 1";
			if (date.DayOfWeek == DayOfWeek.Tuesday) weekDayQuery = "[Tuesdays] = 1";
			if (date.DayOfWeek == DayOfWeek.Wednesday) weekDayQuery = "[Wednesdays] = 1";
			if (date.DayOfWeek == DayOfWeek.Thursday) weekDayQuery = "[Thursdays] = 1";
			if (date.DayOfWeek == DayOfWeek.Friday) weekDayQuery = "[Fridays] = 1";
			if (date.DayOfWeek == DayOfWeek.Saturday) weekDayQuery = "[Saturdays] = 1";
			if (date.DayOfWeek == DayOfWeek.Sunday) weekDayQuery = "[Sundays] = 1";

			
			string weekQuery = "([Week] = 0 OR [Week] = 1)";
			if ((CalendarHelper.GetWeek(date) % 2) == 1) weekQuery = "([Week] = 0 OR [Week] = 2)";

			SqlDataReader dataReader = database.query("SELECT [Entry No] FROM [Shipping Customer Schedule] WHERE [Shipping Customer No] = '"+shippingCustomerNo+"' AND "+weekDayQuery+" AND "+weekQuery);
			if (dataReader.Read())
			{
				dataReader.Close();
				return true;
			}

			dataReader.Close();
			return false;
		}


		public ShippingCustomerSchedule findSchedule(Database database, string shippingCustomerNo, DateTime date)
		{
			string weekDayQuery = "";
			if (date.DayOfWeek == DayOfWeek.Monday) weekDayQuery = "[Mondays] = 1";
			if (date.DayOfWeek == DayOfWeek.Tuesday) weekDayQuery = "[Tuesdays] = 1";
			if (date.DayOfWeek == DayOfWeek.Wednesday) weekDayQuery = "[Wednesdays] = 1";
			if (date.DayOfWeek == DayOfWeek.Thursday) weekDayQuery = "[Thursdays] = 1";
			if (date.DayOfWeek == DayOfWeek.Friday) weekDayQuery = "[Fridays] = 1";
			if (date.DayOfWeek == DayOfWeek.Saturday) weekDayQuery = "[Saturdays] = 1";
			if (date.DayOfWeek == DayOfWeek.Sunday) weekDayQuery = "[Sundays] = 1";

			string weekQuery = "([Week] = 0 OR [Week] = 1)";
			if ((CalendarHelper.GetWeek(date) % 2) == 1) weekQuery = "([Week] = 0 OR [Week] = 2)";

			ShippingCustomerSchedule shippingCustomerSchedule = null;

			SqlDataReader dataReader = database.query("SELECT [Shipping Customer No], [Entry No], [Type], [Mondays], [Tuesdays], [Wednesdays], [Thursdays], [Fridays], [Saturdays], [Sundays], [Week], [Quantity], [Time] FROM [Shipping Customer Schedule] WHERE [Shipping Customer No] = '"+shippingCustomerNo+"' AND "+weekDayQuery+" AND "+weekQuery);
			if (dataReader.Read())
			{
				shippingCustomerSchedule = new ShippingCustomerSchedule(dataReader);	
				
			}

			dataReader.Close();

			return shippingCustomerSchedule;
		}
	}
}
