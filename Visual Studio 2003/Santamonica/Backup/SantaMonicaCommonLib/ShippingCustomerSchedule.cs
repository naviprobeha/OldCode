using System;
using System.Data;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for UserOperator.
	/// </summary>
	public class ShippingCustomerSchedule
	{

		public string shippingCustomerNo;
		public int entryNo;
		public int type;
		public bool mondays;
		public bool tuesdays;
		public bool wednesdays;
		public bool thursdays;
		public bool fridays;
		public bool saturdays;
		public bool sundays;
		public int week;
		public float quantity;
		public DateTime time;

		public ShippingCustomerSchedule()
		{

		}

		public ShippingCustomerSchedule(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//

			this.shippingCustomerNo = dataReader.GetValue(0).ToString();
			this.entryNo = dataReader.GetInt32(1);
			this.type = int.Parse(dataReader.GetValue(2).ToString());

			this.mondays = false;
			if (dataReader.GetValue(3).ToString() == "1") this.mondays = true;

			this.tuesdays = false;
			if (dataReader.GetValue(4).ToString() == "1") this.tuesdays = true;

			this.wednesdays = false;
			if (dataReader.GetValue(5).ToString() == "1") this.wednesdays = true;

			this.thursdays = false;
			if (dataReader.GetValue(6).ToString() == "1") this.thursdays = true;

			this.fridays = false;
			if (dataReader.GetValue(7).ToString() == "1") this.fridays = true;

			this.saturdays = false;
			if (dataReader.GetValue(8).ToString() == "1") this.saturdays = true;

			this.sundays = false;
			if (dataReader.GetValue(9).ToString() == "1") this.sundays = true;
			
			this.week = int.Parse(dataReader.GetValue(10).ToString());
			this.quantity = float.Parse(dataReader.GetValue(11).ToString());

			this.time = dataReader.GetDateTime(12);
		}

		public ShippingCustomerSchedule(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//

			this.shippingCustomerNo = dataRow.ItemArray.GetValue(0).ToString();
			this.entryNo = int.Parse(dataRow.ItemArray.GetValue(1).ToString());
			this.type = int.Parse(dataRow.ItemArray.GetValue(2).ToString());

			this.mondays = false;
			if (dataRow.ItemArray.GetValue(3).ToString() == "1") this.mondays = true;

			this.tuesdays = false;
			if (dataRow.ItemArray.GetValue(4).ToString() == "1") this.tuesdays = true;

			this.wednesdays = false;
			if (dataRow.ItemArray.GetValue(5).ToString() == "1") this.wednesdays = true;

			this.thursdays = false;
			if (dataRow.ItemArray.GetValue(6).ToString() == "1") this.thursdays = true;

			this.fridays = false;
			if (dataRow.ItemArray.GetValue(7).ToString() == "1") this.fridays = true;

			this.saturdays = false;
			if (dataRow.ItemArray.GetValue(8).ToString() == "1") this.saturdays = true;

			this.sundays = false;
			if (dataRow.ItemArray.GetValue(9).ToString() == "1") this.sundays = true;
			
			this.week = int.Parse(dataRow.ItemArray.GetValue(10).ToString());
			this.quantity = float.Parse(dataRow.ItemArray.GetValue(11).ToString());

			this.time = DateTime.Parse(dataRow.ItemArray.GetValue(12).ToString());
		}

		public void save(Database database)
		{
			int mondaysVal = 0;
			int tuesdaysVal = 0;
			int wednesdaysVal = 0;
			int thursdaysVal = 0;
			int fridaysVal = 0;
			int saturdaysVal = 0;
			int sundaysVal = 0;

			if (mondays) mondaysVal = 1;
			if (tuesdays) tuesdaysVal = 1;
			if (wednesdays) wednesdaysVal = 1;
			if (thursdays) thursdaysVal = 1;
			if (fridays) fridaysVal = 1;
			if (saturdays) saturdaysVal = 1;
			if (sundays) sundaysVal = 1;

			SqlDataReader dataReader = database.query("SELECT [Shipping Customer No] FROM [Shipping Customer Schedule] WHERE [Shipping Customer No] = '"+shippingCustomerNo+"' AND [Entry No] = '"+entryNo+"'");

			if (dataReader.Read())
			{
				dataReader.Close();
				database.nonQuery("UPDATE [Shipping Customer Schedule] SET [Type] = '"+type+"', [Mondays] = '"+mondaysVal+"', [Tuesdays] = '"+tuesdaysVal+"', [Wednesdays] = '"+wednesdaysVal+"', [Thursdays] = '"+thursdaysVal+"', [Fridays] = '"+fridaysVal+"', [Saturdays] = '"+saturdaysVal+"', [Sundays] = '"+sundaysVal+"', [Week] = '"+week+"', [Quantity] = '"+quantity.ToString().Replace(",", ".")+"', [Time] = '"+time.ToString("1754-01-01 HH:mm:ss")+"' WHERE [Shipping Customer No] = '"+shippingCustomerNo+"' AND [Entry No] = '"+entryNo+"'");

			}
			else
			{
				dataReader.Close();
				database.nonQuery("INSERT INTO [Shipping Customer Schedule] ([Shipping Customer No], [Type], [Mondays], [Tuesdays], [Wednesdays], [Thursdays], [Fridays], [Saturdays], [Sundays], [Week], [Quantity], [Time]) VALUES ('"+shippingCustomerNo+"','"+type+"','"+mondaysVal+"','"+tuesdaysVal+"','"+wednesdaysVal+"', '"+thursdaysVal+"', '"+fridaysVal+"', '"+saturdaysVal+"', '"+sundaysVal+"', '"+week+"', '"+quantity+"', '"+time.ToString("1754-01-01 HH:mm:ss")+"')");
			}

			
		}

		public void delete(Database database)
		{
			database.nonQuery("DELETE FROM [Shipping Customer Schedule] WHERE [Shipping Customer No] = '"+shippingCustomerNo+"' AND [Entry No] = '"+entryNo+"'");
		}

		public string getType()
		{
			if (type == 1) return "Linjeorder";
			if (type == 1) return "Fabriksorder";
			return "";
		}

		public string getWeek()
		{
			if (week == 0) return "Varje vecka";
			if (week == 1) return "Jämna veckor";
			if (week == 2) return "Udda veckor";
			return "";
		}
	}
}
