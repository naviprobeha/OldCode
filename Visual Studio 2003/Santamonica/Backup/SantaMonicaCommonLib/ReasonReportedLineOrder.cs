using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Navipro.SantaMonica.Common
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
	public class ReasonReportedLineOrder
	{
		public string reasonCode;
		public int lineOrderEntryNo;
		public DateTime entryDateTime;
		public string operatorNo;

		public string updateMethod;

		public ReasonReportedLineOrder()
		{
		}

		public ReasonReportedLineOrder(SqlDataReader dataReader)
		{
			//
			// TODO: Add constructor logic here
			//
			this.reasonCode = dataReader.GetValue(0).ToString();
			this.lineOrderEntryNo = dataReader.GetInt32(1);
			
			DateTime entryDate = dataReader.GetDateTime(2);
			DateTime entryTime = dataReader.GetDateTime(3);
			this.entryDateTime = new DateTime(entryDate.Year, entryDate.Month, entryDate.Day, entryTime.Hour, entryTime.Minute, entryTime.Second);
			this.operatorNo = dataReader.GetValue(4).ToString();
		}

		public ReasonReportedLineOrder(DataRow dataRow)
		{
			//
			// TODO: Add constructor logic here
			//
			this.reasonCode = dataRow.ItemArray.GetValue(0).ToString();
			this.lineOrderEntryNo = int.Parse(dataRow.ItemArray.GetValue(1).ToString());

			DateTime entryDate = DateTime.Parse(dataRow.ItemArray.GetValue(2).ToString());
			DateTime entryTime = DateTime.Parse(dataRow.ItemArray.GetValue(3).ToString());
			this.entryDateTime = new DateTime(entryDate.Year, entryDate.Month, entryDate.Day, entryTime.Hour, entryTime.Minute, entryTime.Second);
			this.operatorNo = dataRow.ItemArray.GetValue(4).ToString();
		}


		public void save(Database database)
		{
			if (updateMethod == "D")
			{
				database.nonQuery("DELETE FROM [Reason Reported Line Order] WHERE [Reason Code] = '"+reasonCode+"' AND [Line Order Entry No] = '"+this.lineOrderEntryNo+"'");

			}
			else
			{
				SqlDataReader dataReader = database.query("SELECT [Reason Code] FROM [Reason Reported Line Order] WHERE [Reason Code] = '"+reasonCode+"' AND [Line Order Entry No] = '"+this.lineOrderEntryNo+"'");

				if (dataReader.Read())
				{
					dataReader.Close();

				}
				else
				{
					dataReader.Close();
					database.nonQuery("INSERT INTO [Reason Reported Line Order] ([Reason Code], [Line Order Entry No], [Entry Date], [Entry Time], [Operator No]) VALUES ('"+reasonCode+"','"+lineOrderEntryNo+"','"+entryDateTime.ToString("yyyy-MM-dd")+"','"+entryDateTime.ToString("1754-01-01 HH:mm:ss")+"', '"+operatorNo+"')");
				}

			}
		}


	}
}
