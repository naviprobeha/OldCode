using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for AgentTransaction.
	/// </summary>
	public class LineOrderHistory
	{
		public int lineOrderEntryNo;
		public DateTime shipDate;
		public string shippingCustomerNo;
		public string shippingCustomerName;

		public string details;
		public string comments;

		public DateTime shipTime;
		public DateTime creationDate;

		public int createdByType;
		public string createdByCode;
		public int deletedByType;
		public string deletedByCode;

		public DateTime confirmedToDateTime;

		public DateTime deletedDateTime;


		public LineOrderHistory(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.lineOrderEntryNo = dataReader.GetInt32(0);
			this.shipDate = dataReader.GetDateTime(3);
		
			this.shippingCustomerNo = dataReader.GetValue(4).ToString();
			this.shippingCustomerName = dataReader.GetValue(5).ToString();
			this.details = dataReader.GetValue(13).ToString();
			this.comments = dataReader.GetValue(14).ToString();
			
			this.shipTime = dataReader.GetDateTime(17);
			this.creationDate = dataReader.GetDateTime(18);

			this.createdByType = dataReader.GetInt32(25);
			this.createdByCode = dataReader.GetValue(26).ToString();
			this.deletedByType = dataReader.GetInt32(25);
			this.deletedByCode = dataReader.GetValue(26).ToString();

			DateTime confirmedToDate = dataReader.GetDateTime(27);
			DateTime confirmedToTime = dataReader.GetDateTime(28);
			this.confirmedToDateTime = new DateTime(confirmedToDate.Year, confirmedToDate.Month, confirmedToDate.Day, confirmedToTime.Hour, confirmedToTime.Minute, confirmedToTime.Second);

			DateTime deletedDate = dataReader.GetDateTime(29);
			DateTime deletedTime = dataReader.GetDateTime(30);
			this.deletedDateTime = new DateTime(deletedDate.Year, deletedDate.Month, deletedDate.Day, deletedTime.Hour, deletedTime.Minute, deletedTime.Second);
		}

		public LineOrderHistory(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.lineOrderEntryNo = int.Parse(dataRow.ItemArray.GetValue(0).ToString());
			this.shipDate = DateTime.Parse(dataRow.ItemArray.GetValue(3).ToString());
		
			this.shippingCustomerNo = dataRow.ItemArray.GetValue(4).ToString();
			this.shippingCustomerName = dataRow.ItemArray.GetValue(5).ToString();
			this.details = dataRow.ItemArray.GetValue(13).ToString();
			this.comments = dataRow.ItemArray.GetValue(14).ToString();
			
			this.shipTime = DateTime.Parse(dataRow.ItemArray.GetValue(17).ToString());
			this.creationDate = DateTime.Parse(dataRow.ItemArray.GetValue(18).ToString());

			this.createdByType = int.Parse(dataRow.ItemArray.GetValue(25).ToString());
			this.createdByCode = dataRow.ItemArray.GetValue(26).ToString();
			this.deletedByType = int.Parse(dataRow.ItemArray.GetValue(25).ToString());
			this.deletedByCode = dataRow.ItemArray.GetValue(26).ToString();

			DateTime confirmedToDate = DateTime.Parse(dataRow.ItemArray.GetValue(27).ToString());
			DateTime confirmedToTime = DateTime.Parse(dataRow.ItemArray.GetValue(28).ToString());
			this.confirmedToDateTime = new DateTime(confirmedToDate.Year, confirmedToDate.Month, confirmedToDate.Day, confirmedToTime.Hour, confirmedToTime.Minute, confirmedToTime.Second);

			DateTime deletedDate = DateTime.Parse(dataRow.ItemArray.GetValue(29).ToString());
			DateTime deletedTime = DateTime.Parse(dataRow.ItemArray.GetValue(30).ToString());
			this.deletedDateTime = new DateTime(deletedDate.Year, deletedDate.Month, deletedDate.Day, deletedTime.Hour, deletedTime.Minute, deletedTime.Second);

		}


		public LineOrderHistory(LineOrder lineOrder)
		{
			this.lineOrderEntryNo = lineOrder.entryNo;
			this.shipDate = lineOrder.shipDate;
			this.shippingCustomerName = lineOrder.shippingCustomerName;
			this.shippingCustomerNo = lineOrder.shippingCustomerNo;

			this.details = lineOrder.details;
			this.comments = lineOrder.comments;
			this.creationDate = lineOrder.creationDate;
			this.shipTime = lineOrder.shipTime;
			this.confirmedToDateTime = lineOrder.confirmedToDateTime;

			this.createdByCode = lineOrder.createdByCode;
			this.createdByType = lineOrder.createdByType;
			
			this.deletedByType = lineOrder.deletedByType;
			this.deletedByCode = lineOrder.deletedByCode;

			this.deletedDateTime = DateTime.Now;
		}

		public void save(Database database)
		{
			this.shipTime = new DateTime(1754, 01, 01, shipTime.Hour, shipTime.Minute, shipTime.Second, shipTime.Millisecond);

			SqlDataReader dataReader = database.query("SELECT [Line Order Entry No] FROM [Line Order History] WHERE [Line Order Entry No] = '"+lineOrderEntryNo+"'");
			if (!dataReader.Read())
			{
				dataReader.Close();

				database.nonQuery("INSERT INTO [Line Order History] ([Line Order Entry No], [Ship Date], [Shipping Customer No], [Shipping Customer Name], [Details], [Comments], [Ship Time], [Creation Date], [Created By Type], [Created By Code], [Deleted By Type], [Deleted By Code], [Confirmed To Date], [Confirmed To Time], [Deleted Date], [Deleted Time]) VALUES ('"+lineOrderEntryNo+"', '"+shipDate.ToString("yyyy-MM-dd")+"','"+this.shippingCustomerNo+"','"+this.shippingCustomerName+"','"+this.details+"','"+this.comments+"', '"+shipTime.ToString("yyyy-MM-dd HH:mm:ss")+"','"+creationDate.ToString("yyyy-MM-dd 00:00:00")+"', '"+createdByType+"', '"+createdByCode+"', '"+deletedByType+"', '"+deletedByCode+"', '"+this.confirmedToDateTime.ToString("yyyy-MM-dd 00:00:00")+"', '"+this.confirmedToDateTime.ToString("1754-01-01 HH:mm:ss")+"', '"+this.deletedDateTime.ToString("yyyy-MM-dd")+"', '"+this.deletedDateTime.ToString("1754-01-01 HH:mm:ss")+"')");
				
				
			}
			else
			{
				dataReader.Close();
				database.nonQuery("UPDATE [Line Order History] SET [Ship Date] = '"+shipDate.ToString("yyyy-MM-dd")+"', [Shipping Customer No] = '"+shippingCustomerNo+"', [Shipping Customer Name] = '"+shippingCustomerName+"', [Details] = '"+details+"', [Comments] = '"+comments+"', [Ship Time] = '"+shipTime.ToString("yyyy-MM-dd HH:mm:ss")+"', [Creation Date] = '"+creationDate.ToString("yyyy-MM-dd")+"', [Created By Type] = '"+this.createdByType+"', [Created By Code] = '"+this.createdByCode+"', [Deleted By Type] = '"+this.deletedByType+"', [Deleted By Code] = '"+this.deletedByCode+"', [Confirmed To Date] = '"+this.confirmedToDateTime.ToString("yyyy-MM-dd 00:00:00")+"', [Confirmed To Time] = '"+confirmedToDateTime.ToString("1754-01-01 HH:mm:ss")+"', [Deleted Date] = '"+this.deletedDateTime.ToString("yyyy-MM-dd 00:00:00")+"', [Deleted Time] = '"+deletedDateTime.ToString("1754-01-01 HH:mm:ss")+"' WHERE [Line Order Entry No] = '"+lineOrderEntryNo+"'");

			}


		}



		public string getCreatedByType()
		{
			if (createdByType == 0) return "Automatplanering";
			if (createdByType == 1) return "Operator";
			if (createdByType == 2) return "Kund";

			return "";
		}

		public string getDeletedByType()
		{
			if (deletedByType == 0) return "Automatplanering";
			if (deletedByType == 1) return "Operator";
			if (deletedByType == 2) return "Kund";

			return "";
		}

		public string getCreatedByName(Database database)
		{
			if (this.createdByType == 0) return "AUTO";
			if (this.createdByType == 1)
			{
				UserOperators userOperators = new UserOperators();
				UserOperator userOperator = userOperators.getOperator(database, this.createdByCode);
				return userOperator.name;
			}
			if (this.createdByType == 2)
			{
				ShippingCustomerUsers shippingCustomerUsers = new ShippingCustomerUsers();
				ShippingCustomerUser shippingCustomerUser = shippingCustomerUsers.getEntry(database, this.createdByCode);
				return shippingCustomerUser.name;
			}
			return "";

		}

		public string getDeletedyName(Database database)
		{
			if (this.deletedByType == 0) return "AUTO";
			if (this.deletedByType == 1)
			{
				UserOperators userOperators = new UserOperators();
				UserOperator userOperator = userOperators.getOperator(database, this.deletedByCode);
				return userOperator.name;
			}
			if (this.deletedByType == 2)
			{
				ShippingCustomerUsers shippingCustomerUsers = new ShippingCustomerUsers();
				ShippingCustomerUser shippingCustomerUser = shippingCustomerUsers.getEntry(database, this.deletedByCode);
				return shippingCustomerUser.name;
			}
			return "";

		}

	}
}
